﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ShellFiler.Api;
using ShellFiler.Document.SSH;
using ShellFiler.Properties;
using Tamir.SharpSsh;
using Tamir.SharpSsh.jsch;

namespace ShellFiler.FileSystem.SFTP {

    //=========================================================================================
    // クラス：ファイル情報を設定するプロシージャ
    //=========================================================================================
    class SFTPSetFileInfoProcedure : SFTPProcedure {
        // SSH内部処理のコントローラ
        private SFTPProcedureControler m_controler;
        
        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：[in]connection 接続
        // 　　　　[in]context    コンテキスト情報
        // 戻り値：なし
        //=========================================================================================
        public SFTPSetFileInfoProcedure(SSHConnection connection, FileOperationRequestContext context) {
            m_controler = new SFTPProcedureControler(connection, context);
        }

        //=========================================================================================
        // 機　能：コマンドを実行する
        // 引　数：[in]srcFileInfo  転送元ファイル/ディレクトリの情報
        // 　　　　[in]destFilePath 転送先ファイル/ディレクトリのパス名
        // 戻り値：ステータスコード
        //=========================================================================================
        public FileOperationStatus Execute(SFTPFile srcFileInfo, string destFilePath) {
            FileOperationStatus status = m_controler.Initialize();
            if (!status.Succeeded) {
                return status;
            }

            string directory = destFilePath;

            // フルパスディレクトリを分解
            SSHProtocolType protocol;
            string user, server, path;
            int portNo;
            bool success = SSHUtils.SeparateUserServer(directory, out protocol, out user, out server, out portNo, out path);
            if (!success) {
                throw new SfException(SfException.SSHCanNotParsePath);
            }

            // 内部形式に変換
            int mtime = SFTPFile.DateTimeToSSH(srcFileInfo.ModifiedDate);
            int permissions = srcFileInfo.Permissions;          // 不明なとき-1

            // ファイル情報を設定
            try {
                ChannelSftp channel = m_controler.Context.SFTPRequestContext.GetChannelSftp(m_controler.Connection);
                channel.setMtime(path, mtime);
                if (permissions != -1) {
                    channel.chmod(permissions, path);
                }
            } catch (SftpException) {
                return FileOperationStatus.Fail;
            } catch (Exception e) {
                return m_controler.Connection.OnException(e, Resources.Log_SSHExecFail);
            }
            return FileOperationStatus.Success;
        }
    }
}
