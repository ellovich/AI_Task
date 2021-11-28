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
            this.pb_RecognitionElement = new System.Windows.Forms.PictureBox();
            this.b_Erase = new System.Windows.Forms.Button();
            this.b_ChooseColor = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.b_SortTemplates = new System.Windows.Forms.Button();
            this.b_ResetSorting = new System.Windows.Forms.Button();
            this.cb_ConsiderInvreted = new System.Windows.Forms.CheckBox();
            this.b_LoadTemplates = new System.Windows.Forms.Button();
            this.cb_SortAuto = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_RecognitionElement)).BeginInit();
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
            this.panel_signs.Size = new System.Drawing.Size(918, 419);
            this.panel_signs.TabIndex = 6;
            // 
            // pb_RecognitionElement
            // 
            this.pb_RecognitionElement.BackColor = System.Drawing.SystemColors.Window;
            this.pb_RecognitionElement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_RecognitionElement.Location = new System.Drawing.Point(347, 443);
            this.pb_RecognitionElement.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_RecognitionElement.Name = "pb_RecognitionElement";
            this.pb_RecognitionElement.Size = new System.Drawing.Size(180, 180);
            this.pb_RecognitionElement.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb_RecognitionElement.TabIndex = 7;
            this.pb_RecognitionElement.TabStop = false;
            this.pb_RecognitionElement.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_RecognitionElement_Paint);
            this.pb_RecognitionElement.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_RecognitionElement_MouseDown);
            this.pb_RecognitionElement.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_RecognitionElement_MouseMove);
            this.pb_RecognitionElement.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_RecognitionElement_MouseUp);
            // 
            // b_Erase
            // 
            this.b_Erase.Location = new System.Drawing.Point(535, 543);
            this.b_Erase.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.b_Erase.Name = "b_Erase";
            this.b_Erase.Size = new System.Drawing.Size(173, 35);
            this.b_Erase.TabIndex = 8;
            this.b_Erase.Text = "Erase";
            this.b_Erase.UseVisualStyleBackColor = true;
            this.b_Erase.Click += new System.EventHandler(this.b_Erase_Click);
            // 
            // b_ChooseColor
            // 
            this.b_ChooseColor.Location = new System.Drawing.Point(535, 502);
            this.b_ChooseColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.b_ChooseColor.Name = "b_ChooseColor";
            this.b_ChooseColor.Size = new System.Drawing.Size(173, 35);
            this.b_ChooseColor.TabIndex = 8;
            this.b_ChooseColor.Text = "Choose color";
            this.b_ChooseColor.UseVisualStyleBackColor = true;
            this.b_ChooseColor.Click += new System.EventHandler(this.b_ChooseColor_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(535, 443);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 35);
            this.button1.TabIndex = 8;
            this.button1.Text = "Load file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.b_LoadFile_Click);
            // 
            // b_SortTemplates
            // 
            this.b_SortTemplates.Location = new System.Drawing.Point(13, 543);
            this.b_SortTemplates.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.b_SortTemplates.Name = "b_SortTemplates";
            this.b_SortTemplates.Size = new System.Drawing.Size(173, 35);
            this.b_SortTemplates.TabIndex = 8;
            this.b_SortTemplates.Text = "Sort by Possibility";
            this.b_SortTemplates.UseVisualStyleBackColor = true;
            this.b_SortTemplates.Click += new System.EventHandler(this.b_SortTemplates_Click);
            // 
            // b_ResetSorting
            // 
            this.b_ResetSorting.Location = new System.Drawing.Point(13, 588);
            this.b_ResetSorting.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.b_ResetSorting.Name = "b_ResetSorting";
            this.b_ResetSorting.Size = new System.Drawing.Size(173, 35);
            this.b_ResetSorting.TabIndex = 9;
            this.b_ResetSorting.Text = "Reset Sorting";
            this.b_ResetSorting.UseVisualStyleBackColor = true;
            this.b_ResetSorting.Click += new System.EventHandler(this.b_ResetSorting_Click);
            // 
            // cb_ConsiderInvreted
            // 
            this.cb_ConsiderInvreted.AutoSize = true;
            this.cb_ConsiderInvreted.Location = new System.Drawing.Point(535, 592);
            this.cb_ConsiderInvreted.Name = "cb_ConsiderInvreted";
            this.cb_ConsiderInvreted.Size = new System.Drawing.Size(187, 29);
            this.cb_ConsiderInvreted.TabIndex = 11;
            this.cb_ConsiderInvreted.Text = "Consider inverted";
            this.cb_ConsiderInvreted.UseVisualStyleBackColor = true;
            this.cb_ConsiderInvreted.CheckedChanged += new System.EventHandler(this.cb_ConsiderInvreted_CheckedChanged);
            // 
            // b_LoadTemplates
            // 
            this.b_LoadTemplates.Location = new System.Drawing.Point(13, 443);
            this.b_LoadTemplates.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.b_LoadTemplates.Name = "b_LoadTemplates";
            this.b_LoadTemplates.Size = new System.Drawing.Size(173, 35);
            this.b_LoadTemplates.TabIndex = 8;
            this.b_LoadTemplates.Text = "Load templates";
            this.b_LoadTemplates.UseVisualStyleBackColor = true;
            this.b_LoadTemplates.Click += new System.EventHandler(this.b_LoadTemplates_Click);
            // 
            // cb_SortAuto
            // 
            this.cb_SortAuto.AutoSize = true;
            this.cb_SortAuto.Location = new System.Drawing.Point(13, 506);
            this.cb_SortAuto.Name = "cb_SortAuto";
            this.cb_SortAuto.Size = new System.Drawing.Size(188, 29);
            this.cb_SortAuto.TabIndex = 11;
            this.cb_SortAuto.Text = "Sort automatically";
            this.cb_SortAuto.UseVisualStyleBackColor = true;
            this.cb_SortAuto.CheckedChanged += new System.EventHandler(this.cb_SortAuto_CheckedChanged);
            // 
            // TemplatesForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ClientSize = new System.Drawing.Size(944, 637);
            this.Controls.Add(this.cb_SortAuto);
            this.Controls.Add(this.cb_ConsiderInvreted);
            this.Controls.Add(this.b_ResetSorting);
            this.Controls.Add(this.b_ChooseColor);
            this.Controls.Add(this.b_LoadTemplates);
            this.Controls.Add(this.b_SortTemplates);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.b_Erase);
            this.Controls.Add(this.pb_RecognitionElement);
            this.Controls.Add(this.panel_signs);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TemplatesForm";
            this.Text = "Templates form";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.SignsForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.SignsForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pb_RecognitionElement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel panel_signs;
        private System.Windows.Forms.PictureBox pb_RecognitionElement;
        private System.Windows.Forms.Button b_Erase;
        private System.Windows.Forms.Button b_ChooseColor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button b_SortTemplates;
        private System.Windows.Forms.Button b_ResetSorting;
        private System.Windows.Forms.CheckBox cb_ConsiderInvreted;
        private System.Windows.Forms.Button b_LoadTemplates;
        private System.Windows.Forms.CheckBox cb_SortAuto;
    }
}