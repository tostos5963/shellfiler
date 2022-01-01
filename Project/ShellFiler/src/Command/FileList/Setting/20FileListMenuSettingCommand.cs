﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ShellFiler.Api;
using ShellFiler.Document;
using ShellFiler.Document.Setting;
using ShellFiler.Properties;
using ShellFiler.Util;
using ShellFiler.UI;
using ShellFiler.UI.Dialog.KeyOption;
using ShellFiler.UI.Dialog.Option;

namespace ShellFiler.Command.FileList.Setting {

    //=========================================================================================
    // クラス：コマンドを実行する
    // ファイル一覧でのキー割り当ての変更を行います。
    //   書式 　 FileListMenuSetting()
    //   引数  　なし
    //   戻り値　なし
    //   対応Ver 1.3.0
    //=========================================================================================
    class FileListMenuSettingCommand : FileListActionCommand {

        //=========================================================================================
        // 機　能：コンストラクタ
        // 引　数：なし
        // 戻り値：なし
        //=========================================================================================
        public FileListMenuSettingCommand() {
        }

        //=========================================================================================
        // 機　能：機能を実行する
        // 引　数：なし
        // 戻り値：実行結果
        //=========================================================================================
        public override object Execute() {
            ExecuteKeySetting(CommandUsingSceneType.FileList);
            return null;
        }

        //=========================================================================================
        // 機　能：キー設定を実行する
        // 引　数：[in]scene  キーの利用シーン（ファイル一覧/ファイルビューア/グラフィックビューア）
        // 戻り値：なし
        //=========================================================================================
        public static void ExecuteKeySetting(CommandUsingSceneType scene) {
            // 外部で更新されているかチェック
            MenuSetting saved = new MenuSetting();
            saved.LoadSetting(null);
            if (saved.LastFileWriteTime > Program.Document.MenuSetting.LastFileWriteTime) {
                ConfirmSettingUpdatedDialog confirm = new ConfirmSettingUpdatedDialog(Resources.DlgConfirmSetting_MenuSetting);
                DialogResult resultConfirm = confirm.ShowDialog(Program.MainWindow);
                if (resultConfirm != DialogResult.OK) {
                    return;
                }
                if (confirm.LoadExternalConfig) {
                    Program.Document.MenuSetting = saved;
                }
            }

            // API一覧を取得
            CommandApiLoader loader = new CommandApiLoader();
            CommandSpec commandSpec = loader.Load();
            if (commandSpec == null) {
                return;
            }

            // オプションを編集
            MenuSetting prev = (MenuSetting)(Program.Document.MenuSetting.Clone());
            MenuSettingDialog dialog = new MenuSettingDialog(Program.Document.MenuSetting, commandSpec, scene);
            DialogResult resultSetting = dialog.ShowDialog(Program.MainWindow);
            if (resultSetting != DialogResult.OK) {
                Program.Document.MenuSetting = prev;
                return;
            }
            if (!MenuSetting.EqualsConfig(prev, Program.Document.MenuSetting)) {
                // 更新されていれば保存して更新
                Program.Document.MenuSetting.SaveSetting();
                Program.MainWindow.ResetUIItems();
            }

            return;
        }

        //=========================================================================================
        // プロパティ：コマンドのUI表現
        //=========================================================================================
        public override UIResource UIResource {
            get {
                return UIResource.FileListMenuSettingCommand;
            }
        }
    }
}
