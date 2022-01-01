﻿using System;
using System.Threading;

namespace ShellFiler.Api {

    //=========================================================================================
    // クラス：IDの実装
    //=========================================================================================
    public class IdImpl {
        // ID
        int m_id;

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：[in]taskId    設定するID
        // 戻り値：なし
        //=========================================================================================
        protected IdImpl(int id) {
            m_id = id;
        }

        //=========================================================================================
        // 機　能：比較演算子
        // 引　数：[in]a  比較対象1
        // 　　　　[in]b  比較対象2
        // 戻り値：等しいときtrue
        //=========================================================================================
        public static bool operator==(IdImpl a, IdImpl b) {
            return a.m_id == b.m_id;
        }

        //=========================================================================================
        // 機　能：比較演算子
        // 引　数：[in]a  比較対象1
        // 　　　　[in]b  比較対象2
        // 戻り値：等しくないときtrue
        //=========================================================================================
        public static bool operator!=(IdImpl a, IdImpl b) {
            return a.m_id != b.m_id;
        }

        //=========================================================================================
        // 機　能：比較する
        // 引　数：[in]other  比較対象
        // 戻り値：等しいときtrue
        //=========================================================================================
        public override bool Equals(object other) {
            return this.m_id == ((IdImpl)other).m_id;
        }

        //=========================================================================================
        // 機　能：ハッシュコードを計算する
        // 引　数：なし
        // 戻り値：ハッシュコード
        //=========================================================================================
        public override int GetHashCode() {
            return m_id;
        }

        //=========================================================================================
        // 機　能：格納されているIDの数値を返す
        // 引　数：なし
        // 戻り値：IDの数値
        //=========================================================================================
        public int GetAsInt() {
            return m_id;
        }
    }
}
