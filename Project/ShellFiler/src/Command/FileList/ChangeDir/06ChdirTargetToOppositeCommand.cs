﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ShellFiler.Api;
using ShellFiler.Document;
using ShellFiler.FileSystem;
using ShellFiler.Util;
using ShellFiler.UI;

namespace ShellFiler.Command.FileList.ChangeDir {

    //=========================================================================================
    // クラス：コマンドを実行する
    // 対象パスのフォルダを反対パスと同じにします。
    //   書式 　 ChdirTargetToOpposite()
    //   引数  　なし
    //   戻り値　bool:フォルダの変更に成功または変更を開始できたときtrue、変更できなかったときfalseを返します。
    //   対応Ver 0.0.1
    //=========================================================================================
    class ChdirTargetToOppositeCommand : FileListActionCommand {

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        public ChdirTargetToOppositeCommand() {
        }

        //=========================================================================================
        // 機　能：機能を実行する
        // 引　数：なし
        // 戻り値：実行結果
        //=========================================================================================
        public override object Execute() {
            bool result = ChdirCommand.ChangeDirectory(FileListViewTarget, new ChangeDirectoryParam.Direct(FileListViewOpposite.FileList.DisplayDirectoryName));
            return result;
        }

        //=========================================================================================
        // プロパティ：コマンドのUI表現
        //=========================================================================================
        public override UIResource UIResource {
            get {
                return UIResource.ChdirTargetToOppositeCommand;
            }
        }
    }
}
