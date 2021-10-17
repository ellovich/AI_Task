﻿namespace AI_Task
{
    partial class MainForm
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
            this.calcForEachFuncB = new System.Windows.Forms.Button();
            this.valueTB = new System.Windows.Forms.TextBox();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.calcForUnionB = new System.Windows.Forms.Button();
            this.calcForDiffB = new System.Windows.Forms.Button();
            this.resTB = new System.Windows.Forms.RichTextBox();
            this.calcCenterOfMaxB = new System.Windows.Forms.Button();
            this.showChB = new System.Windows.Forms.CheckedListBox();
            this.clearAllB = new System.Windows.Forms.Button();
            this.showL = new System.Windows.Forms.Label();
            this.debugL = new System.Windows.Forms.Label();
            this.xLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // calcForEachFuncB
            // 
            this.calcForEachFuncB.BackColor = System.Drawing.Color.LightSteelBlue;
            this.calcForEachFuncB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.calcForEachFuncB.Location = new System.Drawing.Point(183, 77);
            this.calcForEachFuncB.Name = "calcForEachFuncB";
            this.calcForEachFuncB.Size = new System.Drawing.Size(162, 25);
            this.calcForEachFuncB.TabIndex = 0;
            this.calcForEachFuncB.Text = "Calculate for each func";
            this.calcForEachFuncB.UseVisualStyleBackColor = false;
            this.calcForEachFuncB.Click += new System.EventHandler(this.calcForEachFuncB_Click);
            // 
            // valueTB
            // 
            this.valueTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.valueTB.Location = new System.Drawing.Point(119, 11);
            this.valueTB.Name = "valueTB";
            this.valueTB.Size = new System.Drawing.Size(116, 26);
            this.valueTB.TabIndex = 1;
            this.valueTB.Text = "0";
            this.valueTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.valueTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.valueTB_KeyPress);
            // 
            // chart
            // 
            this.chart.BackColor = System.Drawing.Color.Silver;
            this.chart.BorderlineColor = System.Drawing.Color.Black;
            this.chart.BorderlineWidth = 2;
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chart.Location = new System.Drawing.Point(12, 117);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.chart.Size = new System.Drawing.Size(938, 359);
            this.chart.TabIndex = 3;
            this.chart.Text = "chart";
            // 
            // calcForUnionB
            // 
            this.calcForUnionB.BackColor = System.Drawing.Color.LightSteelBlue;
            this.calcForUnionB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.calcForUnionB.Location = new System.Drawing.Point(183, 46);
            this.calcForUnionB.Name = "calcForUnionB";
            this.calcForUnionB.Size = new System.Drawing.Size(162, 25);
            this.calcForUnionB.TabIndex = 4;
            this.calcForUnionB.Text = "Calculate for union";
            this.calcForUnionB.UseVisualStyleBackColor = false;
            this.calcForUnionB.Click += new System.EventHandler(this.calcForUnionB_Click);
            // 
            // calcForDiffB
            // 
            this.calcForDiffB.BackColor = System.Drawing.Color.LightSteelBlue;
            this.calcForDiffB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.calcForDiffB.Location = new System.Drawing.Point(15, 46);
            this.calcForDiffB.Name = "calcForDiffB";
            this.calcForDiffB.Size = new System.Drawing.Size(162, 25);
            this.calcForDiffB.TabIndex = 4;
            this.calcForDiffB.Text = "Calculate for diff";
            this.calcForDiffB.UseVisualStyleBackColor = false;
            this.calcForDiffB.Click += new System.EventHandler(this.calcForDiffB_Click);
            // 
            // resTB
            // 
            this.resTB.BackColor = System.Drawing.Color.Silver;
            this.resTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resTB.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resTB.Location = new System.Drawing.Point(310, 500);
            this.resTB.Name = "resTB";
            this.resTB.Size = new System.Drawing.Size(640, 200);
            this.resTB.TabIndex = 5;
            this.resTB.Text = "";
            // 
            // calcCenterOfMaxB
            // 
            this.calcCenterOfMaxB.BackColor = System.Drawing.Color.LightSteelBlue;
            this.calcCenterOfMaxB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.calcCenterOfMaxB.Location = new System.Drawing.Point(15, 77);
            this.calcCenterOfMaxB.Name = "calcCenterOfMaxB";
            this.calcCenterOfMaxB.Size = new System.Drawing.Size(162, 25);
            this.calcCenterOfMaxB.TabIndex = 4;
            this.calcCenterOfMaxB.Text = "Calc center of max";
            this.calcCenterOfMaxB.UseVisualStyleBackColor = false;
            this.calcCenterOfMaxB.Click += new System.EventHandler(this.calcCenterOfMaxB_Click);
            // 
            // showChB
            // 
            this.showChB.BackColor = System.Drawing.Color.Silver;
            this.showChB.CheckOnClick = true;
            this.showChB.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showChB.FormattingEnabled = true;
            this.showChB.Location = new System.Drawing.Point(15, 500);
            this.showChB.Name = "showChB";
            this.showChB.Size = new System.Drawing.Size(279, 164);
            this.showChB.TabIndex = 6;
            this.showChB.SelectedIndexChanged += new System.EventHandler(this.showChB_SelectedIndexChanged);
            // 
            // clearAllB
            // 
            this.clearAllB.BackColor = System.Drawing.Color.LightSteelBlue;
            this.clearAllB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clearAllB.Location = new System.Drawing.Point(75, 676);
            this.clearAllB.Name = "clearAllB";
            this.clearAllB.Size = new System.Drawing.Size(160, 25);
            this.clearAllB.TabIndex = 4;
            this.clearAllB.Text = "ClearAll";
            this.clearAllB.UseVisualStyleBackColor = false;
            this.clearAllB.Click += new System.EventHandler(this.clearAllB_Click);
            // 
            // showL
            // 
            this.showL.AutoSize = true;
            this.showL.Location = new System.Drawing.Point(12, 479);
            this.showL.Name = "showL";
            this.showL.Size = new System.Drawing.Size(39, 14);
            this.showL.TabIndex = 7;
            this.showL.Text = "Show:";
            // 
            // debugL
            // 
            this.debugL.AutoSize = true;
            this.debugL.Location = new System.Drawing.Point(307, 479);
            this.debugL.Name = "debugL";
            this.debugL.Size = new System.Drawing.Size(31, 14);
            this.debugL.TabIndex = 7;
            this.debugL.Text = "Info:";
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(93, 20);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(17, 14);
            this.xLabel.TabIndex = 7;
            this.xLabel.Text = "X:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Pink;
            this.ClientSize = new System.Drawing.Size(962, 713);
            this.Controls.Add(this.debugL);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.showL);
            this.Controls.Add(this.showChB);
            this.Controls.Add(this.resTB);
            this.Controls.Add(this.calcForUnionB);
            this.Controls.Add(this.calcCenterOfMaxB);
            this.Controls.Add(this.clearAllB);
            this.Controls.Add(this.calcForDiffB);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.valueTB);
            this.Controls.Add(this.calcForEachFuncB);
            this.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "Считалочка";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button calcForEachFuncB;
        private System.Windows.Forms.TextBox valueTB;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Button calcForUnionB;
        private System.Windows.Forms.Button calcForDiffB;
        private System.Windows.Forms.RichTextBox resTB;
        private System.Windows.Forms.Button calcCenterOfMaxB;
        private System.Windows.Forms.CheckedListBox showChB;
        private System.Windows.Forms.Button clearAllB;
        private System.Windows.Forms.Label showL;
        private System.Windows.Forms.Label debugL;
        private System.Windows.Forms.Label xLabel;
    }
}

