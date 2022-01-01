﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ShellFiler.Api;
using ShellFiler.Document;

namespace ShellFiler.UI.Log {

    //=========================================================================================
    // クラス：LogViewImplの親となるコンポーネントが実装するインターフェイス
    //=========================================================================================
    public interface ILogViewContainer {

        //=========================================================================================
        // 機　能：マウスの選択が開始されたときの処理を行う
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        void OnMouseSelectStart();

        //=========================================================================================
        // 機　能：マウスの選択が変更されたときの処理を行う
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        void OnMouseSelectionMove();

        //=========================================================================================
        // 機　能：マウスの選択が終了したときの処理を行う
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        void OnMouseSelectEnd();

        //=========================================================================================
        // 機　能：マウスの選択が変更されたときの処理を行う
        // 引　数：[in]type    通知の種類
        // 　　　　[in]param   通知に対するパラメータ
        // 戻り値：なし
        //=========================================================================================
        void OnStatusChanged(LogViewContainerStatusType type, object param);

        //=========================================================================================
        // 機　能：選択範囲の情報を補正する
        // 引　数：なし
        // 戻り値：新しい選択範囲（選択がないときnull）
        //=========================================================================================
        LogViewSelectionRange ModifySelection();

        //=========================================================================================
        // プロパティ：親となるパネルのWindowsコンポーネント
        //=========================================================================================
        ScrollableControl View {
            get;
        }

        //=========================================================================================
        // プロパティ：ログ1行文の高さ
        //=========================================================================================
        int LogLineHeight {
            get;
        }

        //=========================================================================================
        // プロパティ：背景色の表示モード
        //=========================================================================================
        LogGraphics.BackColorMode BackColorMode {
            get;
        }
    }
}
