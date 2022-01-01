﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using ShellFiler.Api;
using ShellFiler.Document;
using ShellFiler.Document.Setting;
using ShellFiler.Document.OSSpec;
using ShellFiler.FileSystem.Virtual;
using ShellFiler.FileTask.DataObject;
using ShellFiler.Locale;
using ShellFiler.UI.FileList;
using ShellFiler.Util;

namespace ShellFiler.FileSystem.Windows {

    //=========================================================================================
    // クラス：ダミーのファイル操作API（ID等の返却用）
    //=========================================================================================
    class DummyFileSystem : IFileSystem {
        // このクラスはsingletonで動作する

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        public DummyFileSystem() {
        }

        //=========================================================================================
        // 機　能：ファイル操作を開始する
        // 引　数：[in]cache     キャッシュ情報
        // 　　　　[in]dirRoot   ルートディレクトリを含むディレクトリ
        // 戻り値：なし
        //=========================================================================================
        public void BeginFileOperation(FileOperationRequestContext cache, string dirRoot) {
        }

        //=========================================================================================
        // 機　能：ファイル操作を終了する
        // 引　数：[in]cache     キャッシュ情報
        // 戻り値：なし
        //=========================================================================================
        public void EndFileOperation(FileOperationRequestContext cache) {
        }

        //=========================================================================================
        // 機　能：ファイル一覧を取得する
        // 引　数：[in]cache       キャッシュ情報
        // 　　　　[in]directory   取得ディレクトリ
        // 　　　　[out]fileList   ファイル一覧を取得する変数への参照
        // 戻り値：ステータス（成功のときSuccess）
        //=========================================================================================
        public FileOperationStatus GetFileList(FileOperationRequestContext cache, string directory, out List<IFile> fileList) {
            fileList = null;
            return FileOperationStatus.Fail;
        }

        //=========================================================================================
        // 機　能：このファイルシステムの新しいファイル一覧を作成する
        // 引　数：[in]directory     一覧を作成するディレクトリ
        // 　　　　[in]isLeftWindow  左画面の一覧を作成するときtrue
        // 　　　　[in]fileListCtx   使用中のファイル一覧のコンテキスト情報
        // 戻り値：ファイル一覧（作成できなかったときnull）
        //=========================================================================================
        public IFileList CreateFileListFromExisting(string directory, bool isLeftWindow, IFileListContext fileListCtx) {
            return null;
        }

        //=========================================================================================
        // 機　能：ファイルアイコンを取得する
        // 引　数：[in]filePath  ファイルパス
        // 　　　　[in]isDir     ディレクトリのときtrue
        // 　　　　[in]tryReal   実ファイルを取得するときtrue
        // 　　　　[in]width     取得するアイコンの幅
        // 　　　　[in]height    取得するアイコンの高さ
        // 戻り値：アイコン（失敗したとき、デフォルトアイコンを使用するときnull）
        //=========================================================================================
        public Icon ExtractFileIcon(string filePath, bool isDir, bool tryReal, int width, int height) {
            return null;
        }

        //=========================================================================================
        // 機　能：ファイル転送用に転送元ファイルをダウンロードする
        // 引　数：[in]context           コンテキスト情報
        // 　　　　[in]srcLogicalPath    転送元ファイルのファイルパス
        // 　　　　[in]destLogicalPath   転送先ファイルのファイルパス
        // 　　　　[in]destPhysicalPath  転送先ファイルとしてWindows上にダウンロードするときの物理パス
        // 　　　　[in]srcFileInfo       転送元のファイル情報
        // 　　　　[in]progress          進捗状態を通知するdelegate
        // 戻り値：ステータスコード（成功のときSuccess）
        //=========================================================================================
        public FileOperationStatus TransferDownload(FileOperationRequestContext context, string srcLogicalPath, string destLogicalPath, string destPhysicalPath, IFile srcFileInfo, FileProgressEventHandler progress) {
            return FileOperationStatus.Fail;
        }

        //=========================================================================================
        // 機　能：ファイル転送用に転送元ファイルをアップロードする
        // 引　数：[in]context           コンテキスト情報
        // 　　　　[in]srcLogicalPath    転送元ファイルのファイルパス
        // 　　　　[in]destLogicalPath   転送先ファイルのファイルパス
        // 　　　　[in]srcPhysicalPath   転送元ファイルとしてWindows上に用意されているファイルの物理パス
        // 　　　　[in]progress          進捗状態を通知するdelegate
        // 戻り値：ステータスコード（成功のときSuccess）
        //=========================================================================================
        public FileOperationStatus TransferUpload(FileOperationRequestContext context, string srcLogicalPath, string destLogicalPath, string srcPhysicalPath, FileProgressEventHandler progress) {
            return FileOperationStatus.Fail;
        }

        //=========================================================================================
        // 機　能：ファイルの情報を返す
        // 引　数：[in]cache      キャッシュ情報
        // 　　　　[in]directory  ファイルパス
        // 　　　　[in]isTarget   対象パスの一覧のときtrue、反対パスのときfalse
        // 　　　　[out]fileInfo  ファイルの情報（失敗したときはnull）
        // 戻り値：ステータス（成功のときSuccess、存在しないときはSuccessでfileInfo=null）
        //=========================================================================================
        public FileOperationStatus GetFileInfo(FileOperationRequestContext cache, string directory, bool isTarget, out IFile fileInfo) {
            fileInfo = null;
            return FileOperationStatus.Fail;
        }

        //=========================================================================================
        // 機　能：ファイル属性を設定する
        // 引　数：[in]context       コンテキスト情報
        // 　　　　[in]srcFileInfo   転送元のファイル情報
        // 　　　　[in]destFilePath  転送先のフルパス
        // 　　　　[in]baseAttr      属性の基本部分を設定するときtrue
        // 　　　　[in]allAttr       すべての属性を設定するときtrue
        // 戻り値：エラーコード
        //=========================================================================================
        public FileOperationStatus SetFileInfo(FileOperationRequestContext context, IFile srcFileInfo, string destFilePath, bool baseAttr, bool allAttr) {
            return FileOperationStatus.Fail;
        }

        //=========================================================================================
        // 機　能：ファイルの存在を確認する
        // 引　数：[in]cache     コンテキスト情報
        // 　　　　[in]filePath  ファイルパス
        // 　　　　[in]isTarget  対象パスの一覧のときtrue、反対パスのときfalse
        // 　　　　[in]isFile    ファイルの存在を調べるときtrue、フォルダはfalse、両方はnull
        // 　　　　[out]isExist  ファイルが存在するときtrueを返す領域への参照
        // 戻り値：ステータス（成功のときSuccess、存在しないときはSuccessでisExist=false）
        //=========================================================================================
        public FileOperationStatus CheckFileExist(FileOperationRequestContext cache, string filePath, bool isTarget, BooleanFlag isFile, out bool isExist) {
            isExist = false;
            return FileOperationStatus.Fail;
        }

        //=========================================================================================
        // 機　能：ディレクトリを作成する
        // 引　数：[in]context    コンテキスト情報
        // 　　　　[in]basePath   ディレクトリを作成する場所のパス
        // 　　　　[in]newName    作成するディレクトリ名
        // 　　　　[in]isTarget   対象パスの一覧のときtrue、反対パスのときfalse
        // 戻り値：ステータス（成功のときSuccessMkDir）
        //=========================================================================================
        public FileOperationStatus CreateDirectory(FileOperationRequestContext context, string basePath, string newName, bool isTarget) {
            return FileOperationStatus.Fail;
        }
        
        //=========================================================================================
        // 機　能：ファイル/フォルダを削除する
        // 引　数：[in]context     コンテキスト情報
        // 　　　　[in]filePath    削除するファイルのパス
        // 　　　　[in]isTarget    対象パスを削除するときtrue、反対パスのときfalse
        // 　　　　[in]flag        削除フラグ
        // 戻り値：ステータス（成功のときSuccessDelete）
        //=========================================================================================
        public FileOperationStatus DeleteFileFolder(FileOperationRequestContext context, string filePath, bool isTarget, DeleteFileFolderFlag flag) {
            return FileOperationStatus.Fail;
        }
        
        //=========================================================================================
        // 機　能：ファイルの名前と属性を変更する
        // 引　数：[in]cache      キャッシュ情報
        // 　　　　[in]path       属性を変更するファイルやディレクトリのフルパス
        // 　　　　[in]orgInfo    変更前のファイル情報
        // 　　　　[in]newInfo    変更後のファイル情報
        // 戻り値：ステータス（成功のときSuccessRename）
        //=========================================================================================
        public FileOperationStatus Rename(FileOperationRequestContext cache, string path, RenameFileInfo orgInfo, RenameFileInfo newInfo) {
            return FileOperationStatus.Fail;
        }

        //=========================================================================================
        // 機　能：ファイルの名前と属性を一括変更のルールで変更する
        // 引　数：[in]cache      キャッシュ情報
        // 　　　　[in]path       属性を変更するファイルやディレクトリのフルパス
        // 　　　　[in]renameInfo 変更ルール
        // 　　　　[in]modifyCtx  名前変更のコンテキスト情報
        // 戻り値：ステータス（成功のときSuccessRename）
        //=========================================================================================
        public FileOperationStatus ModifyFileInfo(FileOperationRequestContext cache, string path, RenameSelectedFileInfo renameInfo, ModifyFileInfoContext modifyCtx) {
            return FileOperationStatus.Fail;
        }

        //=========================================================================================
        // 機　能：画像を読み込む
        // 引　数：[in]cache     キャッシュ情報
        // 　　　　[in]filePath  読み込み対象のファイルパス
        // 　　　　[in]maxSize   読み込む最大サイズ
        // 　　　　[out]image    読み込んだ画像を返す変数
        // 戻り値：ステータス（成功のときSuccess）
        //=========================================================================================
        public FileOperationStatus RetrieveImage(FileOperationRequestContext cache, string filePath, long maxSize, out BufferedImage image) {
            image = null;
            return FileOperationStatus.Fail;
        }

        //=========================================================================================
        // 機　能：ファイルアクセスのためファイルを準備する（チャンクで読み込み）
        // 引　数：[in]cache  キャッシュ情報
        // 　　　　[in]file   アクセスしたいファイルの情報
        // 戻り値：ステータス（成功のときSuccess）
        //=========================================================================================
        public FileOperationStatus RetrieveFileChunk(FileOperationRequestContext cache, AccessibleFile file) {
            return FileOperationStatus.Fail;
        }

        //=========================================================================================
        // 機　能：リモートでコマンドを実行する
        // 引　数：[in]cache       キャッシュ情報
        // 　　　　[in]dirName     カレントディレクトリ名
        // 　　　　[in]command     コマンドライン
        // 　　　　[in]errorExpect エラーの期待値
        // 　　　　[in]relayOutLog 標準出力の結果をログ出力するときtrue
        // 　　　　[in]relayErrLog 標準エラーの結果をログ出力するときtrue
        // 　　　　[in]dataTarget  取得データの格納先
        // 戻り値：ステータス（成功のときSuccess）
        //=========================================================================================
        public FileOperationStatus RemoteExecute(FileOperationRequestContext cache, string dirName, string command, List<OSSpecLineExpect> errorExpect, bool relayOutLog, bool relayErrLog, IRetrieveFileDataTarget dataTarget) {
            return FileOperationStatus.Fail;
        }

        //=========================================================================================
        // 機　能：ファイルを関連づけ実行する
        // 引　数：[in]filePath      実行するファイルのローカルパス
        // 　　　　[in]currentDir    カレントパス
        // 　　　　[in]allFile       すべてのファイルを実行するときtrue、実行ファイルだけのときfalse
        // 　　　　[in]fileListCtx   ファイル一覧のコンテキスト情報
        // 戻り値：ステータス（成功のときSuccess）
        //=========================================================================================
        public FileOperationStatus OpenShellFile(string filePath, string currentDir, bool allFile, IFileListContext fileListCtx) {
            return FileOperationStatus.Fail;
        }

        //=========================================================================================
        // 機　能：指定したフォルダ以下のファイルサイズ合計を取得する
        // 引　数：[in]context     コンテキスト情報
        // 　　　　[in]directory   対象ディレクトリのルート
        // 　　　　[in]condition   取得条件
        // 　　　　[in]result      取得結果を返す変数
        // 　　　　[in]progress    進捗状態を通知するdelegate
        // 戻り値：ステータス（成功のときSuccess）
        //=========================================================================================
        public FileOperationStatus RetrieveFolderSize(FileOperationRequestContext context, string directory, RetrieveFolderSizeCondition condition, RetrieveFolderSizeResult result, FileProgressEventHandler progress) {
            result = null;
            return FileOperationStatus.Fail;
        }

        //=========================================================================================
        // 機　能：このファイルシステムのパス同士で、同じサーバ空間のパスかどうかを調べる
        // 引　数：[in]path1  パス１
        // 　　　　[in]path2  パス２
        // 戻り値：パス１とパス２が同じサーバ空間にあるときtrue
        //=========================================================================================
        public bool IsSameServerSpace(string path1, string path2) {
            return false;
        }

        //=========================================================================================
        // 機　能：パスとファイルを連結する
        // 引　数：[in]dir  ディレクトリ名
        // 　　　　[in]file ファイル名
        // 戻り値：連結したファイル名
        //=========================================================================================
        public string CombineFilePath(string dir, string file) {
            return null;
        }

        //=========================================================================================
        // 機　能：ディレクトリ名の最後を'\'または'/'にする
        // 引　数：[in]dir  ディレクトリ名
        // 戻り値：'\'または'/'を補完したディレクトリ名
        //=========================================================================================
        public string CompleteDirectoryName(string dir) {
            return null;
        }

        //=========================================================================================
        // 機　能：このファイルシステムの絶対パス表現かどうかを調べる
        // 引　数：[in]directory     ディレクトリ名
        // 　　　　[in]fileListCtx   ファイル一覧のコンテキスト情報
        // 戻り値：絶対パスのときtrue(trueでも実際にファイルアクセスできるかどうかは不明)
        //=========================================================================================
        public bool IsAbsolutePath(string directory, IFileListContext fileListCtx) {
            return false;
        }

        //=========================================================================================
        // 機　能：指定されたパス名をルートとそれ以外に分割する
        // 引　数：[in]path   パス名
        // 　　　　[out]root  ルート部分を返す文字列（最後はセパレータ）
        // 　　　　[out]sub   サブディレクトリ部分を返す文字列
        // 戻り値：なし
        //=========================================================================================
        public void SplitRootPath(string path, out string root, out string sub) {
            root = null;
            sub = null;
        }
        
        //=========================================================================================
        // 機　能：指定されたパス名のホームディレクトリを取得する
        // 引　数：[in]path  パス名
        // 戻り値：ホームディレクトリ（取得できないときnull）
        //=========================================================================================
        public string GetHomePath(string path) {
            return null;
        }

        //=========================================================================================
        // 機　能：ファイルパスからファイル名を返す
        // 引　数：[in]filePath  ファイルパス
        // 戻り値：ファイルパス中のファイル名
        //=========================================================================================
        public string GetFileName(string filePath) {
            return null;
        }

        //=========================================================================================
        // 機　能：指定されたパス名の絶対パス表現を取得する
        // 引　数：[in]path  パス名
        // 戻り値：絶対パス
        //=========================================================================================
        public string GetFullPath(string path) {
            return null;
        }

        //=========================================================================================
        // 機　能：指定されたパスからディレクトリ名部分を返す
        // 引　数：[in]path     パス名
        // 戻り値：パス名のディレクトリ部分
        //=========================================================================================
        public string GetDirectoryName(string path) {
            return null;
        }

        //=========================================================================================
        // 機　能：パスの区切り文字を返す
        // 引　数：[in]fileListCtx   ファイル一覧のコンテキスト情報
        // 戻り値：パスの区切り文字
        //=========================================================================================
        public string GetPathSeparator(IFileListContext fileListCtx) {
            return null;
        }

        //=========================================================================================
        // 機　能：後始末を行う
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        public void Dispose() {
        }

        //=========================================================================================
        // プロパティ：ファイルシステムID
        //=========================================================================================
        public FileSystemID FileSystemId {
            get {
                return FileSystemID.None;
            }
        }

        //=========================================================================================
        // プロパティ：サポートするショートカットの種類
        //=========================================================================================
        public ShortcutType[] SupportedShortcutType {
            get {
                ShortcutType[] list = new ShortcutType[0];
                return list;
            }
        }
        
        //=========================================================================================
        // プロパティ：ローカル実行の際、ダウンロードとアップロードが必要なときtrue
        //=========================================================================================
        public bool LocalExecuteDownloadRequired {
            get {
                return false;
            }
        }

        //=========================================================================================
        // プロパティ：表示の際の項目一覧
        //=========================================================================================
        public FileListHeaderItem[] FileListHeaderItemList {
            get {
                FileListHeaderItem[] list = new FileListHeaderItem[0];
                return list;
            }
        }

        //=========================================================================================
        // プロパティ：通常使用するエンコード方式
        //=========================================================================================
        public EncodingType DefaultEncoding {
            get {
                return EncodingType.SJIS;
            }
        }
    }
}
