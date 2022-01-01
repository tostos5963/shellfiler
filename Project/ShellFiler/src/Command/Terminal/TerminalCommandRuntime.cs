﻿using System;
using System.Windows.Forms;
using ShellFiler.Api;
using ShellFiler.Command;
using ShellFiler.UI;
using ShellFiler.UI.FileList;

namespace ShellFiler.Command.Terminal {
    
    //=========================================================================================
    // クラス：ファイル一覧でのランタイム
    //=========================================================================================
    public class TerminalCommandRuntime {
        // 実行するコマンド
        private TerminalActionCommand m_command;
        
        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：[in]command  実行するコマンド
        // 戻り値：なし
        //=========================================================================================
        public TerminalCommandRuntime(TerminalActionCommand command) {
            m_command = command;
        }

        //=========================================================================================
        // 機　能：コマンドを実行する
        // 引　数：なし
        // 戻り値：コマンドの戻り値
        //=========================================================================================
        public object Execute() {
            object retVal = m_command.Execute();
            return retVal;
        }
    }
}
