namespace AI_Task
{
    partial class ChartForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.b_CalcForEachFunc = new System.Windows.Forms.Button();
            this.tb_Value = new System.Windows.Forms.TextBox();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.b_CalcForUnion = new System.Windows.Forms.Button();
            this.b_CalcForDiff = new System.Windows.Forms.Button();
            this.tb_Res = new System.Windows.Forms.RichTextBox();
            this.b_CalcCenterOfMax = new System.Windows.Forms.Button();
            this.cb_Show = new System.Windows.Forms.CheckedListBox();
            this.b_ClearAll = new System.Windows.Forms.Button();
            this.l_X = new System.Windows.Forms.Label();
            this.b_EditFuncs = new System.Windows.Forms.Button();
            this.b_EndEditing = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // b_CalcForEachFunc
            // 
            this.b_CalcForEachFunc.BackColor = System.Drawing.Color.LightSteelBlue;
            this.b_CalcForEachFunc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.b_CalcForEachFunc.Location = new System.Drawing.Point(440, 50);
            this.b_CalcForEachFunc.Name = "b_CalcForEachFunc";
            this.b_CalcForEachFunc.Size = new System.Drawing.Size(191, 28);
            this.b_CalcForEachFunc.TabIndex = 0;
            this.b_CalcForEachFunc.Text = "Calculate for each func";
            this.b_CalcForEachFunc.UseVisualStyleBackColor = false;
            this.b_CalcForEachFunc.Click += new System.EventHandler(this.b_CalcForEachFunc_Click);
            // 
            // tb_Value
            // 
            this.tb_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_Value.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_Value.Location = new System.Drawing.Point(43, 33);
            this.tb_Value.Name = "tb_Value";
            this.tb_Value.Size = new System.Drawing.Size(85, 30);
            this.tb_Value.TabIndex = 1;
            this.tb_Value.Text = "0";
            this.tb_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Value.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_Value_KeyPress);
            this.tb_Value.Leave += new System.EventHandler(this.tb_Value_Leave);
            // 
            // chart
            // 
            this.chart.BackColor = System.Drawing.Color.Silver;
            this.chart.BorderlineColor = System.Drawing.Color.Black;
            this.chart.BorderlineWidth = 2;
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chart.Location = new System.Drawing.Point(13, 86);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.chart.Size = new System.Drawing.Size(921, 347);
            this.chart.TabIndex = 3;
            this.chart.Text = "chart";
            // 
            // b_CalcForUnion
            // 
            this.b_CalcForUnion.BackColor = System.Drawing.Color.LightSteelBlue;
            this.b_CalcForUnion.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.b_CalcForUnion.Location = new System.Drawing.Point(440, 16);
            this.b_CalcForUnion.Name = "b_CalcForUnion";
            this.b_CalcForUnion.Size = new System.Drawing.Size(191, 28);
            this.b_CalcForUnion.TabIndex = 4;
            this.b_CalcForUnion.Text = "Calculate for union";
            this.b_CalcForUnion.UseVisualStyleBackColor = false;
            this.b_CalcForUnion.Click += new System.EventHandler(this.b_CalcForUnion_Click);
            // 
            // b_CalcForDiff
            // 
            this.b_CalcForDiff.BackColor = System.Drawing.Color.LightSteelBlue;
            this.b_CalcForDiff.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.b_CalcForDiff.Location = new System.Drawing.Point(243, 16);
            this.b_CalcForDiff.Name = "b_CalcForDiff";
            this.b_CalcForDiff.Size = new System.Drawing.Size(191, 28);
            this.b_CalcForDiff.TabIndex = 4;
            this.b_CalcForDiff.Text = "Calculate for diff";
            this.b_CalcForDiff.UseVisualStyleBackColor = false;
            this.b_CalcForDiff.Click += new System.EventHandler(this.b_CalcForDiff_Click);
            // 
            // tb_Res
            // 
            this.tb_Res.BackColor = System.Drawing.Color.Silver;
            this.tb_Res.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_Res.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_Res.Location = new System.Drawing.Point(342, 450);
            this.tb_Res.Name = "tb_Res";
            this.tb_Res.Size = new System.Drawing.Size(592, 175);
            this.tb_Res.TabIndex = 5;
            this.tb_Res.Text = "";
            // 
            // b_CalcCenterOfMax
            // 
            this.b_CalcCenterOfMax.BackColor = System.Drawing.Color.LightSteelBlue;
            this.b_CalcCenterOfMax.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.b_CalcCenterOfMax.Location = new System.Drawing.Point(243, 50);
            this.b_CalcCenterOfMax.Name = "b_CalcCenterOfMax";
            this.b_CalcCenterOfMax.Size = new System.Drawing.Size(191, 28);
            this.b_CalcCenterOfMax.TabIndex = 4;
            this.b_CalcCenterOfMax.Text = "Calc center of max";
            this.b_CalcCenterOfMax.UseVisualStyleBackColor = false;
            this.b_CalcCenterOfMax.Click += new System.EventHandler(this.b_CalcCenterOfMax_Click);
            // 
            // cb_Show
            // 
            this.cb_Show.BackColor = System.Drawing.Color.Silver;
            this.cb_Show.CheckOnClick = true;
            this.cb_Show.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Show.FormattingEnabled = true;
            this.cb_Show.Location = new System.Drawing.Point(12, 450);
            this.cb_Show.Name = "cb_Show";
            this.cb_Show.Size = new System.Drawing.Size(316, 175);
            this.cb_Show.TabIndex = 6;
            this.cb_Show.SelectedIndexChanged += new System.EventHandler(this.cb_Show_SelectedIndexChanged);
            // 
            // b_ClearAll
            // 
            this.b_ClearAll.BackColor = System.Drawing.Color.LightSteelBlue;
            this.b_ClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.b_ClearAll.Location = new System.Drawing.Point(93, 751);
            this.b_ClearAll.Name = "b_ClearAll";
            this.b_ClearAll.Size = new System.Drawing.Size(200, 28);
            this.b_ClearAll.TabIndex = 4;
            this.b_ClearAll.Text = "ClearAll";
            this.b_ClearAll.UseVisualStyleBackColor = false;
            this.b_ClearAll.Click += new System.EventHandler(this.b_HideAllButFuncs_Click);
            // 
            // l_X
            // 
            this.l_X.AutoSize = true;
            this.l_X.Location = new System.Drawing.Point(12, 40);
            this.l_X.Name = "l_X";
            this.l_X.Size = new System.Drawing.Size(25, 20);
            this.l_X.TabIndex = 7;
            this.l_X.Text = "X:";
            // 
            // b_EditFuncs
            // 
            this.b_EditFuncs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.b_EditFuncs.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.b_EditFuncs.Location = new System.Drawing.Point(684, 50);
            this.b_EditFuncs.Name = "b_EditFuncs";
            this.b_EditFuncs.Size = new System.Drawing.Size(202, 28);
            this.b_EditFuncs.TabIndex = 4;
            this.b_EditFuncs.Text = "Edit funcs";
            this.b_EditFuncs.UseVisualStyleBackColor = false;
            this.b_EditFuncs.Click += new System.EventHandler(this.b_EditFuncs_Click);
            // 
            // b_EndEditing
            // 
            this.b_EndEditing.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.b_EndEditing.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.b_EndEditing.Location = new System.Drawing.Point(892, 50);
            this.b_EndEditing.Name = "b_EndEditing";
            this.b_EndEditing.Size = new System.Drawing.Size(42, 28);
            this.b_EndEditing.TabIndex = 4;
            this.b_EndEditing.Text = "OK";
            this.b_EndEditing.UseVisualStyleBackColor = false;
            this.b_EndEditing.Click += new System.EventHandler(this.b_EndEditing_Click);
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(946, 637);
            this.Controls.Add(this.l_X);
            this.Controls.Add(this.cb_Show);
            this.Controls.Add(this.tb_Res);
            this.Controls.Add(this.b_EndEditing);
            this.Controls.Add(this.b_EditFuncs);
            this.Controls.Add(this.b_CalcForUnion);
            this.Controls.Add(this.b_CalcCenterOfMax);
            this.Controls.Add(this.b_ClearAll);
            this.Controls.Add(this.b_CalcForDiff);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.tb_Value);
            this.Controls.Add(this.b_CalcForEachFunc);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ChartForm";
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_CalcForEachFunc;
        private System.Windows.Forms.TextBox tb_Value;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Button b_CalcForUnion;
        private System.Windows.Forms.Button b_CalcForDiff;
        private System.Windows.Forms.RichTextBox tb_Res;
        private System.Windows.Forms.Button b_CalcCenterOfMax;
        private System.Windows.Forms.CheckedListBox cb_Show;
        private System.Windows.Forms.Button b_ClearAll;
        private System.Windows.Forms.Label l_X;
        private System.Windows.Forms.Button b_EditFuncs;
        private System.Windows.Forms.Button b_EndEditing;
    }
}

