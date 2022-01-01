﻿using System;
using System.Collections.Generic;
using System.Text;
using ShellFiler.Api;
using ShellFiler.Document;
using ShellFiler.FileSystem;
using ShellFiler.FileSystem.Virtual;

namespace ShellFiler.FileTask.Provider {

    //=========================================================================================
    // クラス：指定されたファイルの一覧による転送元ファイルの表現
    //=========================================================================================
    class FileProviderSrcSpecified : IFileProviderSrc {
        // 転送元ディレクトリのファイルシステム
        private IFileSystem m_fileSystem;

        // 転送元のファイルとディレクトリの一覧
        private List<SimpleFileDirectoryPath> m_pathList;

        // ファイル一覧のコンテキスト情報
        private IFileListContext m_fileListContext;

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：[in]directory    対象ディレクトリ
        // 　　　　[in]fileNameList ファイル名一覧（null:指定なし）
        // 　　　　[in]dirNameList  ディレクトリ名一覧（null:指定なし）
        // 　　　　[in]fileSystem   ファイルシステム
        // 　　　　[in]fileListCtx  ファイル一覧のコンテキスト情報
        // 戻り値：なし
        //=========================================================================================
        public FileProviderSrcSpecified(List<SimpleFileDirectoryPath> pathList, IFileSystem fileSystem, IFileListContext fileListCtx) {
            m_fileSystem = fileSystem;
            m_fileListContext = fileListCtx;
            m_pathList = new List<SimpleFileDirectoryPath>();
            foreach (SimpleFileDirectoryPath file in pathList) {
                m_pathList.Add(file);
            }
        }

        //=========================================================================================
        // プロパティ：転送元ディレクトリのファイルシステム
        //=========================================================================================
        public IFileSystem SrcFileSystem {
            get {
                return m_fileSystem;
            }
        }

        //=========================================================================================
        // プロパティ：ファイル一覧のコンテキスト情報
        //=========================================================================================
        public IFileListContext FileListContext {
            get {
                return m_fileListContext;
            }
            set {
                m_fileListContext = value;
            }
        }

        //=========================================================================================
        // プロパティ：転送元ファイルとディレクトリの数
        //=========================================================================================
        public int SrcItemCount {
            get {
                return m_pathList.Count;
            }
        }

        //=========================================================================================
        // 機　能：転送元のフルパスを返す
        // 引　数：[in]index    インデックス
        // 戻り値：転送元のファイル情報
        //=========================================================================================
        public SimpleFileDirectoryPath GetSrcPath(int index) {
            return m_pathList[index];
        }
    }
}
