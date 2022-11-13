using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ShellFiler.Api;
using ShellFiler.Document;
using ShellFiler.FileSystem;
using ShellFiler.Util;
using ShellFiler.UI;

namespace ShellFiler.Command.FileList.MoveCursor {

    //=========================================================================================
    // クラス：コマンドを実行する
    // カーソルを反対ウィンドウに移動します。
    //   書式 　 CursorLeftRightWindow()
    //   引数  　なし
    //   戻り値　なし
    //   対応Ver 2.2.0
    //=========================================================================================
    class CursorLeftRightWindowCommand : FileListActionCommand {

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        public CursorLeftRightWindowCommand() {
        }

        //=========================================================================================
        // 機　能：機能を実行する
        // 引　数：なし
        // 戻り値：実行結果
        //=========================================================================================
        public override object Execute() {
            FileListViewTarget.FileListViewComponent.CursorLeftRight(true, MarkOperation.Clear);
            return null;
        }

        //=========================================================================================
        // プロパティ：コマンドのUI表現
        //=========================================================================================
        public override UIResource UIResource {
            get {
                return UIResource.CursorLeftRightWindowCommand;
            }
        }
    }
}
