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

namespace ShellFiler.Command.FileViewer.Cursor {

    //=========================================================================================
    // クラス：コマンドを実行する
    // カーソルを１ページ分下に移動します。
    //   書式 　 V_CursorRolldown()
    //   引数  　なし
    //   戻り値　なし
    //   対応Ver 0.0.1
    //=========================================================================================
    class V_CursorRolldownCommand : FileViewerActionCommand {

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        public V_CursorRolldownCommand() {
        }

        //=========================================================================================
        // 機　能：機能を実行する
        // 引　数：なし
        // 戻り値：実行結果
        //=========================================================================================
        public override object Execute() {
            if (!Abailable) {
                return null;
            }
            ViewerComponent.MoveDisplayPosition(ViewerComponent.CompleteLineSize);
            return null;
        }

        //=========================================================================================
        // プロパティ：コマンドのUI表現
        //=========================================================================================
        public override UIResource UIResource {
            get {
                return UIResource.V_CursorRolldownCommand;
            }
        }
    }
}
