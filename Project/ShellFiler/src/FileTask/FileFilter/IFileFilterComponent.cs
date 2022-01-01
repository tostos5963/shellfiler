﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using ShellFiler.Api;
using ShellFiler.Document;
using ShellFiler.Document.Setting;
using ShellFiler.FileSystem;
using ShellFiler.Properties;
using ShellFiler.UI.FileList;
using ShellFiler.Util;

namespace ShellFiler.FileTask.FileFilter {

    //=========================================================================================
    // クラス：フィルターコンポーネントの定義
    //=========================================================================================
    public interface IFileFilterComponent {

        //=========================================================================================
        // 機　能：現在の設定に対する表示用のパラメータを取得する
        // 引　数：[in]single  パラメータ情報を1行で作成するときtrue
        // 　　　　[in]param   パラメータ
        // 戻り値：表示用のパラメータ
        //=========================================================================================
        string[] GetDisplayParameter(bool single, Dictionary<string, object> param);

        //=========================================================================================
        // 機　能：フィルター設定用の項目を作成する
        // 引　数：なし
        // 戻り値：フィルター設定用の項目
        //=========================================================================================
        FileFilterItem CreateFileFilterItem();

        //=========================================================================================
        // 機　能：フィルター設定用の項目を作成する
        // 引　数：なし
        // 戻り値：フィルター設定用の項目
        //=========================================================================================
        List<SettingUIItem> GetSettingUI();

        //=========================================================================================
        // 機　能：パラメータが正しいかどうかを確認する
        // 引　数：[in]param   確認するパラメータ
        // 戻り値：エラーメッセージ（エラーがないときnull）
        //=========================================================================================
        string CheckParameter(Dictionary<string, object> param);

        //=========================================================================================
        // 機　能：変換を実行する
        // 引　数：[in]orgFileName 元のファイルパス（クリップボードのときnull）
        // 　　　　[in]src         変換元のバイト列
        // 　　　　[out]dest       変換先のバイト列を返す変数（変換元と同一になる可能性あり）
        // 　　　　[in]param       変換に使用するパラメータ
        // 　　　　[in]cancelEvent キャンセル時にシグナル状態になるイベント
        // 戻り値：ステータス
        //=========================================================================================
        FileOperationStatus Convert(string orgFileName, byte[] src, out byte[] dest, Dictionary<string, object> param, WaitHandle cancelEvent);

        //=========================================================================================
        // プロパティ：フィルターの表示名
        //=========================================================================================
        string FilterName {
            get;
        }

        //=========================================================================================
        // プロパティ：フィルターの説明文
        //=========================================================================================
        string FilterExplain {
            get;
        }
    }
}
