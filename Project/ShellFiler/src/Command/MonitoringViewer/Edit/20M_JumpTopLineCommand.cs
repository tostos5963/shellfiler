﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ShellFiler.Api;
using ShellFiler.Document;
using ShellFiler.FileSystem;
using ShellFiler.MonitoringViewer;
using ShellFiler.Properties;
using ShellFiler.Util;
using ShellFiler.UI;
using ShellFiler.UI.Dialog.MonitoringViewer;

namespace ShellFiler.Command.MonitoringViewer.Edit {

    //=========================================================================================
    // クラス：コマンドを実行する
    // 先頭行に移動します。
    //   書式 　 M_JumpTopLine()
    //   引数  　なし
    //   戻り値　なし
    //   対応Ver 1.3.0
    //=========================================================================================
    class M_JumpTopLineCommand : MonitoringViewerActionCommand {

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        public M_JumpTopLineCommand() {
        }

        //=========================================================================================
        // 機　能：機能を実行する
        // 引　数：なし
        // 戻り値：実行結果
        //=========================================================================================
        public override object Execute() {
            if (!MonitoringViewer.Available) {
                return null;
            }
            MonitoringViewer.MatrixDataView.CursorLine = 0;
            return null;
        }

        //=========================================================================================
        // プロパティ：コマンドのUI表現
        //=========================================================================================
        public override UIResource UIResource {
            get {
                return UIResource.M_JumpTopLineCommand;
            }
        }
    }
}
