﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ShellFiler.Util {

    //=========================================================================================
    // クラス：バッファ付きのイメージ（読込の高速化のためにはストリームを開いたままにしなければならないため）
    //=========================================================================================
    public class BufferedImage : IDisposable {
        // 画像読み込み元のストリーム（まだ開いていないときnull）
        private Stream m_stream = null;
        
        // 画像データ（まだ開いていないときnull）
        private Image m_image = null;

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        public BufferedImage() {
        }

        //=========================================================================================
        // 機　能：後処理を行う
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        public void Dispose() {
            if (m_image != null) {
                m_image.Dispose();
            }
            if (m_stream != null) {
                m_stream.Close();
                m_stream.Dispose();
            }
        }
        //=========================================================================================
        // プロパティ：画像読み込み元のストリーム（まだ開いていないときnull）
        //=========================================================================================
        public Stream Stream {
            get {
                return m_stream;
            }
            set {
                m_stream = value;
            }
        }
        
        //=========================================================================================
        // プロパティ：画像データ（まだ開いていないときnull）
        //=========================================================================================
        public Image Image {
            get {
                return m_image;
            }
            set {
                m_image = value;
            }
        }
    }
}