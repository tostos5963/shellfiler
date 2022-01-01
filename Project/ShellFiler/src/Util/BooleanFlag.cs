﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShellFiler.Util {

    //=========================================================================================
    // クラス：boolの書き換え可能なフラグ
    //=========================================================================================
    public class BooleanFlag : ICloneable {
        // フラグの値
        public bool Value;

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：[in]initial  初期値
        // 戻り値：なし
        //=========================================================================================
        public BooleanFlag(bool initial) {
            Value = initial;
        }

        //=========================================================================================
        // 機　能：クローンを作成する
        // 引　数：なし
        // 戻り値：作成したクローン
        //=========================================================================================
        public object Clone() {
            return MemberwiseClone();
        }

        //=========================================================================================
        // 機　能：クローンを作成する
        // 引　数：[in]src   元のオブジェクト
        // 戻り値：作成したクローン
        //=========================================================================================
        public static BooleanFlag CreateClone(BooleanFlag src) {
            if (src == null) {
                return null;
            } else {
                return (BooleanFlag)(src.Clone());
            }
        }

        //=========================================================================================
        // 機　能：値が同じかどうかを返す
        // 引　数：[in]objA   比較対象1
        // 　　　　[in]objB   比較対象2
        // 戻り値：値が同じときtrue
        //=========================================================================================
        public static bool Equals(BooleanFlag objA, BooleanFlag objB) {
            if (objA == null && objB == null) {
                return true;
            } else if (objA == null || objB == null) {
                return false;
            } else if (objA.Value == objB.Value) {
                return true;
            } else {
                return false;
            }
        }
    }
}
