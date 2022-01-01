﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ShellFiler.Api;
using ShellFiler.Document;
using ShellFiler.GraphicsViewer;
using ShellFiler.Util;
using ShellFiler.UI;
using ShellFiler.UI.Dialog;

namespace ShellFiler.Command.GraphicsViewer.Edit {

    //=========================================================================================
    // クラス：コマンドを実行する
    // 現在表示中のファイルをマークNo.2がセットされた状態にします。
    //   書式 　 G_MarkFile2()
    //   引数  　なし
    //   戻り値　なし
    //   対応Ver 1.0.0
    //=========================================================================================
    class G_MarkFile2Command : GraphicsViewerActionCommand {

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        public G_MarkFile2Command() {
        }

        //=========================================================================================
        // 機　能：機能を実行する
        // 引　数：なし
        // 戻り値：実行結果
        //=========================================================================================
        public override object Execute() {
            G_MarkFile1Command.MarkFile(this, 2);
            return null;
        }

        //=========================================================================================
        // プロパティ：コマンドのUI表現
        //=========================================================================================
        public override UIResource UIResource {
            get {
                return UIResource.G_MarkFile2Command;
            }
        }
    }
}
