﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ShellFiler.Document;
using ShellFiler.Util;

namespace ShellFiler.FileViewer {

    //=========================================================================================
    // クラス：テキストビューアでの選択範囲
    //=========================================================================================
    public class TextViewerSelectionRange {
        // 選択開始行（選択していないとき-1） ※必ずm_selectStartがm_selectEndより左上にある
        private int m_startLine = -1;
        
        // 選択開始桁（常にタブ文字1文字は表示に関係なく1と数える）
        private int m_startColumn = -1;
        
        // 選択終了行
        private int m_endLine = -1;

        // 選択終了桁（実際の選択終了桁位置の次）
        private int m_endColumn = -1;

        // 選択ではじめにクリックした位置の行
        private int m_firstClickLine = -1;
        
        // 選択ではじめにクリックした位置の桁
        private int m_firstClickColumn = -1;

        // 直前の選択位置の行
        private int m_prevLine = -1;
        
        // 直前の選択位置の桁
        private int m_prevColumn = -1;
        
        // 一度でも選択状態になったときtrue
        private bool m_selectedFlag = false;

        //=========================================================================================
        // 機　能：範囲がはじめに選択状態になったかどうかをチェックする
        // 引　数：なし
        // 戻り値：はじめに選択状態になったときtrue
        //=========================================================================================
        public bool CheckFirstSelected() {
            bool current = Selected;
            bool prev = m_selectedFlag;
            m_selectedFlag |= current;
            return (prev == false && current == true);
        }

        //=========================================================================================
        // プロパティ：選択中のときtrue
        //=========================================================================================
        public bool Selected {
            get {
                if (m_startLine == -1) {
                    return false;
                }
                if (m_startLine == m_endLine && m_startColumn == m_endColumn) {
                    return false;
                }
                return true;
            }
        }

        //=========================================================================================
        // プロパティ：選択開始行（選択していないとき-1）
        //=========================================================================================
        public int StartLine {
            get {
                return m_startLine;
            }
            set {
                m_startLine = value;
            }
        }
        
        //=========================================================================================
        // プロパティ：選択開始桁（選択していないとき-1）
        //=========================================================================================
        public int StartColumn {
            get {
                return m_startColumn;
            }
            set {
                m_startColumn = value;
            }
        }
        
        //=========================================================================================
        // プロパティ：選択終了行（選択していないとき-1）
        //=========================================================================================
        public int EndLine {
            get {
                return m_endLine;
            }
            set {
                m_endLine = value;
            }
        }

        //=========================================================================================
        // プロパティ：選択終了桁（実際の選択終了桁位置の次、選択していないとき-1）
        //=========================================================================================
        public int EndColumn {
            get {
                return m_endColumn;
            }
            set {
                m_endColumn = value;
            }
        }

        //=========================================================================================
        // プロパティ：選択ではじめにクリックした位置の行（選択していないとき-1）
        //=========================================================================================
        public int FirstClickLine {
            get {
                return m_firstClickLine;
            }
            set {
                m_firstClickLine = value;
            }
        }
        
        //=========================================================================================
        // プロパティ：選択ではじめにクリックした位置の桁（選択していないとき-1）
        //=========================================================================================
        public int FirstClickColumn {
            get {
                return m_firstClickColumn;
            }
            set {
                m_firstClickColumn = value;
            }
        }

        //=========================================================================================
        // プロパティ：直前の選択位置の行
        //=========================================================================================
        public int PrevLine {
            get {
                return m_prevLine;
            }
            set {
                m_prevLine = value;
            }
        }

        //=========================================================================================
        // プロパティ：直前の選択位置の桁
        //=========================================================================================
        public int PrevColumn {
            get {
                return m_prevColumn;
            }
            set {
                m_prevColumn = value;
            }
        }
    }
}
