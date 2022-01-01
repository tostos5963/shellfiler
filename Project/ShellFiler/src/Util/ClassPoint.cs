﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ShellFiler.Util {

    //=========================================================================================
    // クラス：Pointクラス（Pointをnull可能なclassとして扱う）
    //=========================================================================================
    public class ClassPoint {
        // X座標
        public int X;

        // Y座標
        public int Y;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：[in]point  格納する点の情報
        // 戻り値：なし
        //=========================================================================================
        public ClassPoint(Point point) {
            X = point.X;
            Y = point.Y;
        }

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：[in]x  X座標
        // 　　　　[in]y  Y座標
        // 戻り値：なし
        //=========================================================================================
        public ClassPoint(int x, int y) {
            X = x;
            Y = y;
        }

        //=========================================================================================
        // 機　能：Pointに変換する
        // 引　数：なし
        // 戻り値：Point
        //=========================================================================================
        public Point ToPoint() {
            return new Point(X, Y);
        }
    }
}
