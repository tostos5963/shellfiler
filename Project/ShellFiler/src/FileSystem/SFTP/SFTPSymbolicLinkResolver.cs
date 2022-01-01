﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Tamir.SharpSsh;
using Tamir.SharpSsh.jsch;
using ShellFiler.Api;
using ShellFiler.Document.SSH;
using ShellFiler.Properties;
using ShellFiler.Util;

namespace ShellFiler.FileSystem.SFTP {

    //=========================================================================================
    // クラス：SFTPでシンボリックリンクのリンク先情報を確認するクラス
    //=========================================================================================
    class SFTPSymbolicLinkResolver {

        //=========================================================================================
        // 機　能：ファイル一覧にシンボリックリンクの情報を付与する
        // 引　数：[in]controler   SSH接続のコントローラ
        // 　　　　[in]baseDir     取得対象のディレクトリ
        // 　　　　[in]fileList    取得対象/結果格納先のファイル一覧
        // 戻り値：ステータスコード
        //=========================================================================================
        public static FileOperationStatus SetSymbolicLinkFileList(SFTPProcedureControler controler, string baseDir, List<IFile> fileList) {
            FileOperationStatus status;

            int sameTimeCount = controler.Connection.ShellCommandDictionary.ValueLinkTargetSameTimeCount;
            List<string> linkFileList = new List<string>();
            Dictionary<string, IFile> linkFileTarget = new Dictionary<string,IFile>();
            for (int i = 0; i < fileList.Count; i++) {
                // シンボリックリンクの一覧を作成
                IFile file = fileList[i];
                if (file.Attribute.IsSymbolicLink) {
                    string linkFile = file.FileName;
                    linkFileList.Add(linkFile);
                    linkFileTarget.Add(linkFile, file);
                }

                // 一定量たまるか最後でリンク先を確認
                if (linkFileList.Count >= sameTimeCount || (linkFileList.Count > 0 && i == fileList.Count - 1)) {
                    Dictionary<string, SymbolicLinkTarget> linkDest;
                    status = ExecuteLinkTest(controler, baseDir, linkFileList, out linkDest);
                    if (!status.Succeeded) {
                        return status;
                    }
                    foreach (string resultLinkFile in linkDest.Keys) {
                        SymbolicLinkTarget resultInfo = linkDest[resultLinkFile];
                        SFTPFile resultFile = (SFTPFile)(linkFileTarget[resultLinkFile]);
                        resultFile.SetLinkTarget(resultInfo.LinkTarget, resultInfo.ExistTarget, resultInfo.IsDirectory);
                    }
                    linkFileList.Clear();
                    linkFileTarget.Clear();
                }
            }
            return FileOperationStatus.Success;
        }

        //=========================================================================================
        // 機　能：シンボリックリンクのリンク先相対パスを返す
        // 引　数：[in]controler    SSH接続のコントローラ
        // 　　　　[in]basePath     シンボリックリンクのファイルがあるパス名
        // 　　　　[in]linkFile     シンボリックリンクファイル本体のパス名
        // 　　　　[out]linkTarget  リンクの参照先
        // 　　　　[out]exist       リンク先が存在するときtrueを返す変数
        // 　　　　[out]isDirectory リンク先がディレクトリのときtrueを返す変数
        // 戻り値：ステータス
        //=========================================================================================
        public static FileOperationStatus GetLinkTarget(SFTPProcedureControler controler, string basePath, string linkFile, out string linkTarget, out bool exist, out bool isDirectory) {
            FileOperationStatus status;
            linkTarget = null;
            exist = false;
            isDirectory = false;

            List<string> linkFileList = new List<string>();
            linkFileList.Add(linkFile);
            Dictionary<string, SymbolicLinkTarget> linkDest;
            status = ExecuteLinkTest(controler, basePath, linkFileList, out linkDest);
            if (!status.Succeeded) {
                return status;
            }

            SymbolicLinkTarget linkInfo = linkDest[linkFile];
            linkTarget = linkInfo.LinkTarget;
            exist = linkInfo.ExistTarget;
            isDirectory = linkInfo.IsDirectory;
            return FileOperationStatus.Success;
        }
        
        //=========================================================================================
        // 機　能：リンクのテストと情報取得を行う
        // 引　数：[in]controler   SSH接続のコントローラ
        // 　　　　[in]baseDir     取得対象のディレクトリ
        // 　　　　[in]files       取得対象のファイル名一覧（パス名なし）
        // 　　　　[out]linkDest   リンク先の取得結果を返す変数
        // 戻り値：ステータスコード
        //=========================================================================================
        private static FileOperationStatus ExecuteLinkTest(SFTPProcedureControler controler, string baseDir, List<string> linkFiles, out Dictionary<string, SymbolicLinkTarget> linkDest) {
            FileOperationStatus status;
            linkDest = new Dictionary<string, SymbolicLinkTarget>();

            // コマンドを実行
            List<string> existMarker = new List<string>();
            List<string> dirMarker = new List<string>();
            for (int i = 0; i < linkFiles.Count; i++) {
                existMarker.Add("e:" + i);
                dirMarker.Add("d:" + i);
            }
            string command = controler.Connection.ShellCommandDictionary.GetLsSymbolicLinkCommand(baseDir, linkFiles, existMarker, dirMarker);
            ChannelCommandExec exec = new ChannelCommandExec(controler.Connection);
            byte[] resultBuffer;
            status = exec.Execute(controler.Context.SFTPRequestContext, command, out resultBuffer);
            if (status != FileOperationStatus.Success) {
                return status;
            }
            string result = controler.Connection.ByteToString(resultBuffer);
            string[] resultLine = StringUtils.SeparateLine(result);

            // 結果を解析
            int index = 0;
            for (int i = 0; i < linkFiles.Count; i++) {
                if (index > resultLine.Length) {
                    return FileOperationStatus.Fail;
                }

                // 取得されたパスの「-> 」以降を返す
                string lsLine = resultLine[index];
                int idxLink = lsLine.LastIndexOf("-> ");
                if (idxLink == -1) {
                    return FileOperationStatus.Fail;
                }
                string linkTargetCurrent = lsLine.Substring(idxLink + 3);
                index++;

                // リンク先が存在するかどうかを返す
                bool existCurrent = false;
                string existMarkerCurrent = "e:" + i;
                if (index < resultLine.Length && resultLine[index] == existMarkerCurrent) {
                    existCurrent = true;
                    index++;
                }

                // リンク先がディレクトリかどうかを返す
                bool isDirectoryCurrent = false;
                string isDirectoryMarkerCurrent = "d:" + i;
                if (index < resultLine.Length && resultLine[index] == isDirectoryMarkerCurrent) {
                    isDirectoryCurrent = true;
                    index++;
                }

                linkDest.Add(linkFiles[i], new SymbolicLinkTarget(linkTargetCurrent, existCurrent, isDirectoryCurrent));
            }

            return FileOperationStatus.Success;
        }
    }
}
