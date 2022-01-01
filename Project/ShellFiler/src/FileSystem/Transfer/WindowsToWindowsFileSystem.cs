﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using ShellFiler.Api;
using ShellFiler.Document.Setting;
using ShellFiler.FileSystem.Windows;
using ShellFiler.FileTask.DataObject;
using ShellFiler.UI.Log;

namespace ShellFiler.FileSystem.Transfer {

    //=========================================================================================
    // クラス：Windows同士でのファイル操作API
    //=========================================================================================
    class WindowsToWindowsFileSystem : IFileSystemToFileSystem {
        // 転送先のWindowsファイルシステム
        private WindowsFileSystem m_windowsFileSystem;

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：[in]winFileSystem  Windows用のファイルシステム
        // 戻り値：なし
        //=========================================================================================
        public WindowsToWindowsFileSystem(WindowsFileSystem winFileSystem) {
            m_windowsFileSystem = winFileSystem;
        }

        //=========================================================================================
        // 機　能：ファイルをコピーする
        // 引　数：[in]context         コンテキスト情報
        // 　　　　[in]srcFilePath     転送元ファイル名のフルパス
        // 　　　　[in]srcFileInfoAttr 属性コピーを行うとき、srcFilePathの属性（まだ取得できていないときnull）
        // 　　　　[in]destFilePath    転送先ファイル名のフルパス
        // 　　　　[in]overwrite       上書きするときtrue
        // 　　　　[in]attrMode        属性をコピーするかどうかの設定（属性をコピーしないときnull）
        // 　　　　[in]fileFilter      転送時に使用するフィルター（使用しないときはnull）
        // 　　　　[in]progress        進捗状態を通知するdelegate
        // 戻り値：ステータス
        //=========================================================================================
        public FileOperationStatus CopyFile(FileOperationRequestContext context, string srcFilePath, IFile srcFileInfoAttr, string destFilePath, bool overwrite, AttributeSetMode attrMode, FileFilterTransferSetting fileFilter, FileProgressEventHandler progress) {
            FileOperationStatus status;
            FileOperationStatus status2;
            bool attrSet = false;
            if (attrMode != null) {
                attrSet = attrMode.IsSetAttribute(m_windowsFileSystem.FileSystemId);
            }

            // 属性を取得
            if (attrSet && srcFileInfoAttr == null) {
                status2 = m_windowsFileSystem.GetFileInfo(context, srcFilePath, true, out srcFileInfoAttr);
                if (!status2.Succeeded) {
                    return status2;
                }
            }

            // コピー
            status = m_windowsFileSystem.CopyWindowsFile(context, srcFilePath, destFilePath, overwrite, fileFilter, progress);
            if (!status.Succeeded || status.Skipped) {
                return status;
            }

            // 属性を設定
            if (attrSet) {
                status2 = m_windowsFileSystem.SetFileInfo(context, srcFileInfoAttr, destFilePath, false, true);
                if (!status2.Succeeded) {
                    return status2;
                }
            }

            return status;
        }

        //=========================================================================================
        // 機　能：ファイルを移動する
        // 引　数：[in]context         コンテキスト情報
        // 　　　　[in]srcFilePath     転送元ファイル名のフルパス
        // 　　　　[in]srcFileInfoAttr 属性コピーを行うとき、srcFilePathの属性（属性をコピーしないときnull）
        // 　　　　[in]destFilePath    転送先ファイル名のフルパス
        // 　　　　[in]overwrite       上書きするときtrue
        // 　　　　[in]attrMode        属性をコピーするかどうかの設定（属性をコピーしないときnull）
        // 　　　　[in]progress        進捗状態を通知するdelegate
        // 戻り値：ステータス
        //=========================================================================================
        public FileOperationStatus MoveFileDirectory(FileOperationRequestContext context, string srcFilePath, IFile srcFileInfoAttr, string destFilePath, bool overwrite, AttributeSetMode attrMode, FileProgressEventHandler progress) {
            bool attrSet;
            // コピー
            FileOperationStatus status = m_windowsFileSystem.MoveWindowsFile(context, srcFilePath, destFilePath, overwrite, progress, out attrSet);
            if (!status.Succeeded) {
                return status;
            }

            // 属性を設定
            if (srcFileInfoAttr != null && attrMode.IsSetAttribute(m_windowsFileSystem.FileSystemId)) {
                FileOperationStatus status2 = m_windowsFileSystem.SetFileInfo(context, srcFileInfoAttr, destFilePath, false, true);
                if (!status2.Succeeded) {
                    return status2;
                }
            }

            return status;
        }

        //=========================================================================================
        // 機　能：ディレクトリの直接コピー／移動をサポートするかどうかを確認する
        // 引　数：[in]srcDirPath   転送元ディレクトリ名のフルパス
        // 　　　　[in]destDirPath  転送先ディレクトリ名のフルパス
        // 　　　　[in]isCopy       コピーのときtrue、移動のときfalse
        // 戻り値：直接の移動ができるときtrue（trueになっても失敗することはある）
        //=========================================================================================
        public bool CanMoveDirectoryDirect(string srcDirPath, string destDirPath, bool isCopy) {
            // 一括コピーはサポートしない
            if (isCopy) {
                return false;
            }

            Regex regexDrive = new Regex("[a-zA-Z]:\\\\");
            Match matchDriveSrc = regexDrive.Match(srcDirPath);
            Match matchDriveDest = regexDrive.Match(destDirPath);
            if (matchDriveSrc.Success && matchDriveDest.Success) {
                // 同一ドライブならサポート
                if (srcDirPath[0] == destDirPath[0]) {
                    return true;
                } else {
                    return false;
                }
            } else if (!matchDriveSrc.Success && !matchDriveDest.Success) {
                // ネットワークドライブ同士の場合はトライ
                return true;
            } else {
                // ローカルとネットワークはNG
                return false;
            }
        }

        //=========================================================================================
        // 機　能：ディレクトリの直接コピーを行う
        // 引　数：[in]context       コンテキスト情報
        // 　　　　[in]srcDirPath    転送元ディレクトリ名のフルパス
        // 　　　　[in]destDirPath   転送先ディレクトリ名のフルパス
        // 　　　　[in]attrMode      属性をコピーするかどうかの設定（属性をコピーしないときnull）
        // 　　　　[in]progress      進捗状態を通知するdelegate
        // 戻り値：ステータス（CopyRetryのとき再試行が必要）
        //=========================================================================================
        public FileOperationStatus CopyDirectoryDirect(FileOperationRequestContext context, string srcPath, string destPath, AttributeSetMode attrMode, FileProgressEventHandler progress) {
            return FileOperationStatus.CopyRetry;
        }

        //=========================================================================================
        // 機　能：ショートカットを作成する
        // 引　数：[in]cache         キャッシュ情報
        // 　　　　[in]srcFilePath   転送元ファイル名のフルパス
        // 　　　　[in]destFilePath  転送先ファイル名のフルパス
        // 　　　　[in]overwrite     上書きするときtrue
        // 　　　　[in]type          ショートカットの種類
        // 戻り値：エラーコード
        //=========================================================================================
        public FileOperationStatus CreateShortcut(FileOperationRequestContext cache, string srcFilePath, string destFilePath, bool overwrite, ShortcutType type) {
            return m_windowsFileSystem.CreateShortcut(cache, srcFilePath, destFilePath, overwrite);
        }

        //=========================================================================================
        // 機　能：ファイル属性をコピーする
        // 引　数：[in]context       コンテキスト情報
        // 　　　　[in]isDir         ディレクトリを転送するときtrue
        // 　　　　[in]srcFilePath   転送先のフルパス
        // 　　　　[in]srcFileInfo   転送元のファイル情報（まだ取得できていないときnull）
        // 　　　　[in]destFilePath  転送先のフルパス
        // 　　　　[in]attrMode      設定する属性
        // 戻り値：エラーコード
        //=========================================================================================
        public FileOperationStatus CopyFileInfo(FileOperationRequestContext context, bool isDir, string srcFilePath, IFile srcFileInfo, string destFilePath, AttributeSetMode attrMode) {
            FileOperationStatus status;
            bool copyAttr = attrMode.IsSetAttribute(m_windowsFileSystem.FileSystemId);
            if (!isDir && !copyAttr) {
                return FileOperationStatus.Success;
            }

            // 転送元の属性を取得
            if (srcFileInfo == null) {
                status = m_windowsFileSystem.GetFileInfo(context, srcFilePath, true, out srcFileInfo);
                if (!status.Succeeded) {
                    return status;
                }
            }

            // 属性を設定
            bool copyBase = (isDir || copyAttr);
            bool copyAll = copyAttr;
            status = m_windowsFileSystem.SetFileInfo(context, srcFileInfo, destFilePath, copyBase, copyAll);
            return status;
        }

        //=========================================================================================
        // 機　能：ファイルを結合する
        // 引　数：[in]context         コンテキスト情報
        // 　　　　[in]srcFilePath     転送元ファイル名のフルパス
        // 　　　　[in]destFilePath    転送先ファイル名のフルパス
        // 　　　　[in]taskLogger      ログ出力クラス
        // 戻り値：ステータス
        //=========================================================================================
        public FileOperationStatus CombineFile(FileOperationRequestContext context, List<string> srcFilePath, string destFilePath, ITaskLogger taskLogger) {
            CombineFileProcedure procedure = new CombineFileProcedure();
            FileOperationStatus status = procedure.Execute(context, srcFilePath, destFilePath, taskLogger);
            return status;
        }

        //=========================================================================================
        // 機　能：ファイルを分割する
        // 引　数：[in]context         コンテキスト情報
        // 　　　　[in]srcFilePath     転送元ファイル名のフルパス
        // 　　　　[in]destFolderPath  転送先フォルダ名のフルパス（最後は「\」）
        // 　　　　[in]numberingInfo   ファイルの連番の命名規則
        // 　　　　[in]splitInfo       ファイルの分割方法
        // 　　　　[in]taskLogger      ログ出力クラス
        // 戻り値：ステータス
        //=========================================================================================
        public FileOperationStatus SplitFile(FileOperationRequestContext context, string srcFilePath, string destFolderPath, RenameNumberingInfo numberingInfo, SplitFileInfo splitInfo, ITaskLogger taskLogger) {
            SplitFileProcedure procedure = new SplitFileProcedure();
            FileOperationStatus status = procedure.Execute(context, srcFilePath, srcFilePath, destFolderPath, numberingInfo, splitInfo, null, taskLogger);
            return status;
        }
    }
}
