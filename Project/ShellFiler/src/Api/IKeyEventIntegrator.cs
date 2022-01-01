﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShellFiler.Api {

    //=========================================================================================
    // インターフェース：キー入力を統合するためのインターフェース
    // サブコンポーネントで受信したキーイベントをこのインターフェース経由でまとめる。
    //=========================================================================================
    public interface IKeyEventIntegrator {

        //=========================================================================================
        // 機　能：キー入力時の処理を行う
        // 引　数：[in]key  キーコマンド
        // 戻り値：キー入力したときtrue
        //=========================================================================================
        bool OnKeyDown(KeyCommand key);

        //=========================================================================================
        // 機　能：キー入力時の処理を行う（OnKeyDown処理後）
        // 引　数：[in]key  入力した文字
        // 戻り値：キー入力したときtrue
        //=========================================================================================
        bool OnKeyChar(char key);

        //=========================================================================================
        // 機　能：キー入力時の処理を行う
        // 引　数：[in]key  キーコマンド
        // 戻り値：なし
        //=========================================================================================
        void OnKeyUp(KeyCommand key);
    }
}
