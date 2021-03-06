namespace AI_Task
{
    partial class TemplatesForm
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
            this.panel_signs = new System.Windows.Forms.FlowLayoutPanel();
            this.pb_myImg = new System.Windows.Forms.PictureBox();
            this.b_Erase = new System.Windows.Forms.Button();
            this.b_ChooseColor = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.b_SortTemplates = new System.Windows.Forms.Button();
            this.b_ResetSorting = new System.Windows.Forms.Button();
            this.cb_ConsiderInvreted = new System.Windows.Forms.CheckBox();
            this.b_LoadTemplates = new System.Windows.Forms.Button();
            this.cb_SortAuto = new System.Windows.Forms.CheckBox();
            this.pb_DebugImage = new System.Windows.Forms.PictureBox();
            this.pb_DebugImage2 = new System.Windows.Forms.PictureBox();
            this.l_Answer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_myImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DebugImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DebugImage2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_signs
            // 
            this.panel_signs.AutoScroll = true;
            this.panel_signs.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.panel_signs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_signs.Location = new System.Drawing.Point(13, 14);
            this.panel_signs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel_signs.Name = "panel_signs";
            this.panel_signs.Size = new System.Drawing.Size(920, 416);
            this.panel_signs.TabIndex = 6;
            // 
            // pb_myImg
            // 
            this.pb_myImg.BackColor = System.Drawing.SystemColors.Window;
            this.pb_myImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_myImg.Location = new System.Drawing.Point(222, 451);
            this.pb_myImg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_myImg.Name = "pb_myImg";
            this.pb_myImg.Size = new System.Drawing.Size(160, 160);
            this.pb_myImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb_myImg.TabIndex = 7;
            this.pb_myImg.TabStop = false;
            this.pb_myImg.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_MyImg_Paint);
            this.pb_myImg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_MyImg_MouseDown);
            this.pb_myImg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_MyImg_MouseMove);
            this.pb_myImg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_MyImg_MouseUp);
            // 
            // b_Erase
            // 
            this.b_Erase.Location = new System.Drawing.Point(390, 576);
            this.b_Erase.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.b_Erase.Name = "b_Erase";
            this.b_Erase.Size = new System.Drawing.Size(200, 39);
            this.b_Erase.TabIndex = 8;
            this.b_Erase.Text = "Erase";
            this.b_Erase.UseVisualStyleBackColor = true;
            this.b_Erase.Click += new System.EventHandler(this.b_Erase_Click);
            // 
            // b_ChooseColor
            // 
            this.b_ChooseColor.Location = new System.Drawing.Point(390, 531);
            this.b_ChooseColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.b_ChooseColor.Name = "b_ChooseColor";
            this.b_ChooseColor.Size = new System.Drawing.Size(200, 39);
            this.b_ChooseColor.TabIndex = 8;
            this.b_ChooseColor.Text = "Choose color";
            this.b_ChooseColor.UseVisualStyleBackColor = true;
            this.b_ChooseColor.Click += new System.EventHandler(this.b_ChooseColor_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(390, 451);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 39);
            this.button1.TabIndex = 8;
            this.button1.Text = "Load file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.b_LoadFile_Click);
            // 
            // b_SortTemplates
            // 
            this.b_SortTemplates.Location = new System.Drawing.Point(13, 531);
            this.b_SortTemplates.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.b_SortTemplates.Name = "b_SortTemplates";
            this.b_SortTemplates.Size = new System.Drawing.Size(200, 39);
            this.b_SortTemplates.TabIndex = 8;
            this.b_SortTemplates.Text = "Sort by Possibility";
            this.b_SortTemplates.UseVisualStyleBackColor = true;
            this.b_SortTemplates.Click += new System.EventHandler(this.b_SortTemplates_Click);
            // 
            // b_ResetSorting
            // 
            this.b_ResetSorting.Location = new System.Drawing.Point(13, 576);
            this.b_ResetSorting.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.b_ResetSorting.Name = "b_ResetSorting";
            this.b_ResetSorting.Size = new System.Drawing.Size(200, 39);
            this.b_ResetSorting.TabIndex = 9;
            this.b_ResetSorting.Text = "Reset Sorting";
            this.b_ResetSorting.UseVisualStyleBackColor = true;
            this.b_ResetSorting.Click += new System.EventHandler(this.b_ResetSorting_Click);
            // 
            // cb_ConsiderInvreted
            // 
            this.cb_ConsiderInvreted.AutoSize = true;
            this.cb_ConsiderInvreted.Location = new System.Drawing.Point(390, 494);
            this.cb_ConsiderInvreted.Name = "cb_ConsiderInvreted";
            this.cb_ConsiderInvreted.Size = new System.Drawing.Size(138, 21);
            this.cb_ConsiderInvreted.TabIndex = 11;
            this.cb_ConsiderInvreted.Text = "Consider inverted";
            this.cb_ConsiderInvreted.UseVisualStyleBackColor = true;
            this.cb_ConsiderInvreted.CheckedChanged += new System.EventHandler(this.cb_ConsiderInvreted_CheckedChanged);
            // 
            // b_LoadTemplates
            // 
            this.b_LoadTemplates.Location = new System.Drawing.Point(13, 451);
            this.b_LoadTemplates.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.b_LoadTemplates.Name = "b_LoadTemplates";
            this.b_LoadTemplates.Size = new System.Drawing.Size(201, 39);
            this.b_LoadTemplates.TabIndex = 8;
            this.b_LoadTemplates.Text = "Load templates";
            this.b_LoadTemplates.UseVisualStyleBackColor = true;
            this.b_LoadTemplates.Click += new System.EventHandler(this.b_LoadTemplates_Click);
            // 
            // cb_SortAuto
            // 
            this.cb_SortAuto.AutoSize = true;
            this.cb_SortAuto.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cb_SortAuto.Location = new System.Drawing.Point(13, 494);
            this.cb_SortAuto.Name = "cb_SortAuto";
            this.cb_SortAuto.Size = new System.Drawing.Size(145, 22);
            this.cb_SortAuto.TabIndex = 11;
            this.cb_SortAuto.Text = "Sort automatically";
            this.cb_SortAuto.UseVisualStyleBackColor = true;
            this.cb_SortAuto.CheckedChanged += new System.EventHandler(this.cb_SortAuto_CheckedChanged);
            // 
            // pb_DebugImage
            // 
            this.pb_DebugImage.BackColor = System.Drawing.SystemColors.Window;
            this.pb_DebugImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_DebugImage.Location = new System.Drawing.Point(605, 451);
            this.pb_DebugImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_DebugImage.Name = "pb_DebugImage";
            this.pb_DebugImage.Size = new System.Drawing.Size(160, 160);
            this.pb_DebugImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb_DebugImage.TabIndex = 7;
            this.pb_DebugImage.TabStop = false;
            // 
            // pb_DebugImage2
            // 
            this.pb_DebugImage2.BackColor = System.Drawing.SystemColors.Window;
            this.pb_DebugImage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_DebugImage2.Location = new System.Drawing.Point(773, 451);
            this.pb_DebugImage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_DebugImage2.Name = "pb_DebugImage2";
            this.pb_DebugImage2.Size = new System.Drawing.Size(160, 160);
            this.pb_DebugImage2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb_DebugImage2.TabIndex = 7;
            this.pb_DebugImage2.TabStop = false;
            // 
            // l_Answer
            // 
            this.l_Answer.AutoSize = true;
            this.l_Answer.Location = new System.Drawing.Point(820, 616);
            this.l_Answer.Name = "l_Answer";
            this.l_Answer.Size = new System.Drawing.Size(53, 17);
            this.l_Answer.TabIndex = 12;
            this.l_Answer.Text = "answer";
            // 
            // TemplatesForm
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ClientSize = new System.Drawing.Size(946, 637);
            this.Controls.Add(this.l_Answer);
            this.Controls.Add(this.cb_SortAuto);
            this.Controls.Add(this.cb_ConsiderInvreted);
            this.Controls.Add(this.b_ResetSorting);
            this.Controls.Add(this.b_ChooseColor);
            this.Controls.Add(this.b_LoadTemplates);
            this.Controls.Add(this.b_SortTemplates);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.b_Erase);
            this.Controls.Add(this.pb_DebugImage2);
            this.Controls.Add(this.pb_DebugImage);
            this.Controls.Add(this.pb_myImg);
            this.Controls.Add(this.panel_signs);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TemplatesForm";
            this.Text = "Templates form";
            this.Activated += new System.EventHandler(this.TemplatesForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TemplatesForm_FormClosed);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.TemplatesForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.TemplatesForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pb_myImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DebugImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DebugImage2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel panel_signs;
        private System.Windows.Forms.PictureBox pb_myImg;
        private System.Windows.Forms.Button b_Erase;
        private System.Windows.Forms.Button b_ChooseColor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button b_SortTemplates;
        private System.Windows.Forms.Button b_ResetSorting;
        private System.Windows.Forms.CheckBox cb_ConsiderInvreted;
        private System.Windows.Forms.Button b_LoadTemplates;
        private System.Windows.Forms.CheckBox cb_SortAuto;
        private System.Windows.Forms.PictureBox pb_DebugImage;
        private System.Windows.Forms.PictureBox pb_DebugImage2;
        private System.Windows.Forms.Label l_Answer;
    }
}