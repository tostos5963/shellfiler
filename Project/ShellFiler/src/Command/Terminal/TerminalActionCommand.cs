﻿using System;
using ShellFiler.Api;
using ShellFiler.Command;
using ShellFiler.Terminal;
using ShellFiler.Terminal.UI;

namespace ShellFiler.Command.Terminal {
    
    //=========================================================================================
    // クラス：キー、ツールバーなどのイベントを受けて実行するターミナル用のコマンド
    //=========================================================================================
    public abstract class TerminalActionCommand : ActionCommand {
        // ターミナルのパネル
        private TerminalPanel m_terminalPanel;
        
        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        public TerminalActionCommand() {
        }

        //=========================================================================================
        // 機　能：パラメータをセットする
        // 引　数：[in]param  セットするパラメータ
        // 戻り値：なし
        //=========================================================================================
        public virtual void SetParameter(params object[] param) {
        }

        //=========================================================================================
        // 機　能：初期化する
        // 引　数：[in]panel   ターミナルのパネル
        // 戻り値：なし
        //=========================================================================================
        public void Initialize(TerminalPanel panel) {
            m_terminalPanel = panel;
        }

        //=========================================================================================
        // 機　能：機能を実行する
        // 引　数：[in]param パラメータ
        // 戻り値：実行結果
        //=========================================================================================
        public abstract object Execute();

        //=========================================================================================
        // プロパティ：ターミナルのパネル
        //=========================================================================================
        public TerminalPanel TerminalPanel {
            get {
                return m_terminalPanel;
            }
        }

        //=========================================================================================
        // プロパティ：コマンドのUI表現
        //=========================================================================================
        public abstract UIResource UIResource {
            get;
        }
    }
}
