﻿namespace ELTE.Forms.Sudoku.View
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this._menuFileNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this._menuFileLoadGame = new System.Windows.Forms.ToolStripMenuItem();
            this._menuFileSaveGame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this._menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this._menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this._menuGameEasy = new System.Windows.Forms.ToolStripMenuItem();
            this._menuGameMedium = new System.Windows.Forms.ToolStripMenuItem();
            this._menuGameHard = new System.Windows.Forms.ToolStripMenuItem();
            this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this._toolLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._toolLabelGameSteps = new System.Windows.Forms.ToolStripStatusLabel();
            this._toolLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this._toolLabelGameTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yesEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._menuStrip.SuspendLayout();
            this._statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuFile,
            this._menuSettings,
            this.editToolStripMenuItem});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(492, 24);
            this._menuStrip.TabIndex = 0;
            this._menuStrip.Text = "menuStrip1";
            // 
            // _menuFile
            // 
            this._menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuFileNewGame,
            this.toolStripMenuItem1,
            this._menuFileLoadGame,
            this._menuFileSaveGame,
            this.toolStripMenuItem2,
            this._menuFileExit});
            this._menuFile.Name = "_menuFile";
            this._menuFile.Size = new System.Drawing.Size(37, 20);
            this._menuFile.Text = "File";
            // 
            // _menuFileNewGame
            // 
            this._menuFileNewGame.Name = "_menuFileNewGame";
            this._menuFileNewGame.Size = new System.Drawing.Size(160, 22);
            this._menuFileNewGame.Text = "Új játék";
            this._menuFileNewGame.Click += new System.EventHandler(this.MenuFileNewGame_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
            // 
            // _menuFileLoadGame
            // 
            this._menuFileLoadGame.Name = "_menuFileLoadGame";
            this._menuFileLoadGame.Size = new System.Drawing.Size(160, 22);
            this._menuFileLoadGame.Text = "Játék betöltése...";
            this._menuFileLoadGame.Click += new System.EventHandler(this.MenuFileLoadGame_Click);
            // 
            // _menuFileSaveGame
            // 
            this._menuFileSaveGame.Name = "_menuFileSaveGame";
            this._menuFileSaveGame.Size = new System.Drawing.Size(160, 22);
            this._menuFileSaveGame.Text = "Játék mentése...";
            this._menuFileSaveGame.Click += new System.EventHandler(this.MenuFileSaveGame_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 6);
            // 
            // _menuFileExit
            // 
            this._menuFileExit.Name = "_menuFileExit";
            this._menuFileExit.Size = new System.Drawing.Size(160, 22);
            this._menuFileExit.Text = "Kilépés";
            this._menuFileExit.Click += new System.EventHandler(this.MenuFileExit_Click);
            // 
            // _menuSettings
            // 
            this._menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuGameEasy,
            this._menuGameMedium,
            this._menuGameHard});
            this._menuSettings.Name = "_menuSettings";
            this._menuSettings.Size = new System.Drawing.Size(75, 20);
            this._menuSettings.Text = "Beállítások";
            // 
            // _menuGameEasy
            // 
            this._menuGameEasy.Name = "_menuGameEasy";
            this._menuGameEasy.Size = new System.Drawing.Size(180, 22);
            this._menuGameEasy.Text = "Könnyű játék";
            this._menuGameEasy.Click += new System.EventHandler(this.MenuGameEasy_Click);
            // 
            // _menuGameMedium
            // 
            this._menuGameMedium.Name = "_menuGameMedium";
            this._menuGameMedium.Size = new System.Drawing.Size(180, 22);
            this._menuGameMedium.Text = "Közepes játék";
            this._menuGameMedium.Click += new System.EventHandler(this.MenuGameMedium_Click);
            // 
            // _menuGameHard
            // 
            this._menuGameHard.Name = "_menuGameHard";
            this._menuGameHard.Size = new System.Drawing.Size(180, 22);
            this._menuGameHard.Text = "Nehéz játék";
            this._menuGameHard.Click += new System.EventHandler(this.MenuGameHard_Click);
            // 
            // _openFileDialog
            // 
            this._openFileDialog.Filter = "Sudoku tábla (*.stl)|*.stl";
            this._openFileDialog.Title = "Sudoku játék betöltése";
            // 
            // _saveFileDialog
            // 
            this._saveFileDialog.Filter = "Sudoku tábla (*.stl)|*.stl";
            this._saveFileDialog.Title = "Sudoku játék mentése";
            // 
            // _statusStrip
            // 
            this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolLabel1,
            this._toolLabelGameSteps,
            this._toolLabel2,
            this._toolLabelGameTime});
            this._statusStrip.Location = new System.Drawing.Point(0, 510);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Size = new System.Drawing.Size(492, 24);
            this._statusStrip.TabIndex = 1;
            this._statusStrip.Text = "statusStrip1";
            // 
            // _toolLabel1
            // 
            this._toolLabel1.Name = "_toolLabel1";
            this._toolLabel1.Size = new System.Drawing.Size(67, 19);
            this._toolLabel1.Text = "Lépésszám:";
            // 
            // _toolLabelGameSteps
            // 
            this._toolLabelGameSteps.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this._toolLabelGameSteps.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this._toolLabelGameSteps.Name = "_toolLabelGameSteps";
            this._toolLabelGameSteps.Size = new System.Drawing.Size(17, 19);
            this._toolLabelGameSteps.Text = "0";
            // 
            // _toolLabel2
            // 
            this._toolLabel2.Name = "_toolLabel2";
            this._toolLabel2.Size = new System.Drawing.Size(53, 19);
            this._toolLabel2.Text = "Játékidő:";
            // 
            // _toolLabelGameTime
            // 
            this._toolLabelGameTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this._toolLabelGameTime.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this._toolLabelGameTime.Name = "_toolLabelGameTime";
            this._toolLabelGameTime.Size = new System.Drawing.Size(47, 19);
            this._toolLabelGameTime.Text = "0:00:00";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yesEditToolStripMenuItem,
            this.noEditToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.editToolStripMenuItem.Text = "Szerkesztés";
            // 
            // yesEditToolStripMenuItem
            // 
            this.yesEditToolStripMenuItem.Name = "yesEditToolStripMenuItem";
            this.yesEditToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.yesEditToolStripMenuItem.Text = "Igen";
            this.yesEditToolStripMenuItem.Click += new System.EventHandler(this.yesEditToolStripMenuItem_Click);
            // 
            // noEditToolStripMenuItem
            // 
            this.noEditToolStripMenuItem.Checked = true;
            this.noEditToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noEditToolStripMenuItem.Name = "noEditToolStripMenuItem";
            this.noEditToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.noEditToolStripMenuItem.Text = "Nem";
            this.noEditToolStripMenuItem.Click += new System.EventHandler(this.noEditToolStripMenuItem_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 534);
            this.Controls.Add(this._statusStrip);
            this.Controls.Add(this._menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this._menuStrip;
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.Text = "Sudoku játék";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this._statusStrip.ResumeLayout(false);
            this._statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _menuFile;
        private System.Windows.Forms.ToolStripMenuItem _menuFileNewGame;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _menuFileLoadGame;
        private System.Windows.Forms.ToolStripMenuItem _menuFileSaveGame;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem _menuFileExit;
        private System.Windows.Forms.OpenFileDialog _openFileDialog;
        private System.Windows.Forms.SaveFileDialog _saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem _menuSettings;
        private System.Windows.Forms.ToolStripMenuItem _menuGameEasy;
        private System.Windows.Forms.ToolStripMenuItem _menuGameMedium;
        private System.Windows.Forms.ToolStripMenuItem _menuGameHard;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _toolLabel1;
        private System.Windows.Forms.ToolStripStatusLabel _toolLabelGameSteps;
        private System.Windows.Forms.ToolStripStatusLabel _toolLabel2;
        private System.Windows.Forms.ToolStripStatusLabel _toolLabelGameTime;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yesEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noEditToolStripMenuItem;
    }
}

