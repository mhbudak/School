﻿namespace INFOIBV
{
    partial class INFOIBV
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
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageFileName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Conver_to_Gray = new System.Windows.Forms.Button();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.Invers = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Contrast_Adjustment = new System.Windows.Forms.Button();
            this.GausFilt = new System.Windows.Forms.Button();
            this.MedianFilt = new System.Windows.Forms.Button();
            this.EdgeDed = new System.Windows.Forms.Button();
            this.Threshold = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.HistogramButton = new System.Windows.Forms.Button();
            this.HistogramEqualization = new System.Windows.Forms.Button();
            this.EdgeSharpinnig = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.Make_Image_A_button = new System.Windows.Forms.Button();
            this.Make_Image_B_Button = new System.Windows.Forms.Button();
            this.Make_Image_C_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Location = new System.Drawing.Point(12, 12);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(98, 23);
            this.LoadImageButton.TabIndex = 0;
            this.LoadImageButton.Text = "Load image...";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "Bitmap files (*.bmp;*.gif;*.jpg;*.png;*.tiff;*.jpeg)|*.bmp;*.gif;*.jpg;*.png;*.ti" +
    "ff;*.jpeg";
            this.openImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // imageFileName
            // 
            this.imageFileName.Location = new System.Drawing.Point(116, 14);
            this.imageFileName.Name = "imageFileName";
            this.imageFileName.ReadOnly = true;
            this.imageFileName.Size = new System.Drawing.Size(316, 20);
            this.imageFileName.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 70);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // Conver_to_Gray
            // 
            this.Conver_to_Gray.Location = new System.Drawing.Point(478, 12);
            this.Conver_to_Gray.Name = "Conver_to_Gray";
            this.Conver_to_Gray.Size = new System.Drawing.Size(103, 23);
            this.Conver_to_Gray.TabIndex = 3;
            this.Conver_to_Gray.Text = "Conver to Gray";
            this.Conver_to_Gray.UseVisualStyleBackColor = true;
            this.Conver_to_Gray.Click += new System.EventHandler(this.Conver_to_Gray_Click);
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.Filter = "Bitmap file (*.bmp)|*.bmp";
            this.saveImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(1069, 11);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(95, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save as BMP...";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(531, 70);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(512, 512);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.PictureBox2_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(478, 40);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(103, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;
            // 
            // Invers
            // 
            this.Invers.Location = new System.Drawing.Point(589, 12);
            this.Invers.Name = "Invers";
            this.Invers.Size = new System.Drawing.Size(31, 23);
            this.Invers.TabIndex = 7;
            this.Invers.Text = "Inv";
            this.Invers.UseVisualStyleBackColor = true;
            this.Invers.Click += new System.EventHandler(this.Invers_Click);
            // 
            // Contrast_Adjustment
            // 
            this.Contrast_Adjustment.Location = new System.Drawing.Point(626, 12);
            this.Contrast_Adjustment.Name = "Contrast_Adjustment";
            this.Contrast_Adjustment.Size = new System.Drawing.Size(55, 23);
            this.Contrast_Adjustment.TabIndex = 8;
            this.Contrast_Adjustment.Text = "ContAdj";
            this.Contrast_Adjustment.UseVisualStyleBackColor = true;
            this.Contrast_Adjustment.Click += new System.EventHandler(this.Contrast_Adjustment_Click);
            // 
            // GausFilt
            // 
            this.GausFilt.Location = new System.Drawing.Point(686, 12);
            this.GausFilt.Margin = new System.Windows.Forms.Padding(2);
            this.GausFilt.Name = "GausFilt";
            this.GausFilt.Size = new System.Drawing.Size(61, 23);
            this.GausFilt.TabIndex = 9;
            this.GausFilt.Text = "GausFilt";
            this.GausFilt.UseVisualStyleBackColor = true;
            this.GausFilt.Click += new System.EventHandler(this.GausFilt_Click);
            // 
            // MedianFilt
            // 
            this.MedianFilt.Location = new System.Drawing.Point(751, 12);
            this.MedianFilt.Margin = new System.Windows.Forms.Padding(2);
            this.MedianFilt.Name = "MedianFilt";
            this.MedianFilt.Size = new System.Drawing.Size(63, 23);
            this.MedianFilt.TabIndex = 10;
            this.MedianFilt.Text = "MedianFilt";
            this.MedianFilt.UseVisualStyleBackColor = true;
            this.MedianFilt.Click += new System.EventHandler(this.MedianFilt_Click);
            // 
            // EdgeDed
            // 
            this.EdgeDed.Location = new System.Drawing.Point(818, 12);
            this.EdgeDed.Margin = new System.Windows.Forms.Padding(2);
            this.EdgeDed.Name = "EdgeDed";
            this.EdgeDed.Size = new System.Drawing.Size(57, 23);
            this.EdgeDed.TabIndex = 11;
            this.EdgeDed.Text = "EdgeDe";
            this.EdgeDed.UseVisualStyleBackColor = true;
            this.EdgeDed.Click += new System.EventHandler(this.EdgeDed_Click);
            // 
            // Threshold
            // 
            this.Threshold.Location = new System.Drawing.Point(879, 12);
            this.Threshold.Margin = new System.Windows.Forms.Padding(2);
            this.Threshold.Name = "Threshold";
            this.Threshold.Size = new System.Drawing.Size(44, 23);
            this.Threshold.TabIndex = 12;
            this.Threshold.Text = "Thres";
            this.Threshold.UseVisualStyleBackColor = true;
            this.Threshold.Click += new System.EventHandler(this.Threshold_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(1048, 326);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(256, 256);
            this.pictureBox3.TabIndex = 14;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.PictureBox3_Click);
            // 
            // HistogramButton
            // 
            this.HistogramButton.Location = new System.Drawing.Point(587, 37);
            this.HistogramButton.Name = "HistogramButton";
            this.HistogramButton.Size = new System.Drawing.Size(75, 23);
            this.HistogramButton.TabIndex = 0;
            this.HistogramButton.Text = "Histogram";
            this.HistogramButton.Click += new System.EventHandler(this.HistogramButton_Click);
            // 
            // HistogramEqualization
            // 
            this.HistogramEqualization.Location = new System.Drawing.Point(927, 12);
            this.HistogramEqualization.Margin = new System.Windows.Forms.Padding(2);
            this.HistogramEqualization.Name = "HistogramEqualization";
            this.HistogramEqualization.Size = new System.Drawing.Size(59, 23);
            this.HistogramEqualization.TabIndex = 15;
            this.HistogramEqualization.Text = "Hist Equ";
            this.HistogramEqualization.UseVisualStyleBackColor = true;
            this.HistogramEqualization.Click += new System.EventHandler(this.HistogramEqualization_Click);
            // 
            // EdgeSharpinnig
            // 
            this.EdgeSharpinnig.Location = new System.Drawing.Point(990, 12);
            this.EdgeSharpinnig.Margin = new System.Windows.Forms.Padding(2);
            this.EdgeSharpinnig.Name = "EdgeSharpinnig";
            this.EdgeSharpinnig.Size = new System.Drawing.Size(74, 23);
            this.EdgeSharpinnig.TabIndex = 16;
            this.EdgeSharpinnig.Text = "Edge Sharp";
            this.EdgeSharpinnig.UseVisualStyleBackColor = true;
            this.EdgeSharpinnig.Click += new System.EventHandler(this.EdgeSharpinnig_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1045, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(468, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "To get Image A: 1. Load image 2. Convert to Gray 3.ContAdj    /   1. Load image 2" +
    ". Make Image A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1045, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(623, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "To get Image B: 1. Load image 2. Convert to Gray 3. Cont Adj 4. GausFilt 5. EdgeD" +
    "e 6. Thres    /   1. Load image 2. Make Image B";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1045, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(624, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "To get Image C: 1. Load image 2. Convert to Gray 3. ContAdj 4. MedianFilt 5. Edge" +
    "De 6. Thres  /   1. Load image 2. Make Image C";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1045, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(305, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Filter and threshold parameters can be changed in the program.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1045, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(431, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Each button works separetly. Convert to Gray and Inv buttons work on the Loaded I" +
    "mage.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1045, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(186, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Other buttons work on resulted image.";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(686, 40);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown1.TabIndex = 23;
            this.numericUpDown1.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // Make_Image_A_button
            // 
            this.Make_Image_A_button.Location = new System.Drawing.Point(1048, 204);
            this.Make_Image_A_button.Name = "Make_Image_A_button";
            this.Make_Image_A_button.Size = new System.Drawing.Size(142, 55);
            this.Make_Image_A_button.TabIndex = 24;
            this.Make_Image_A_button.Text = "Make Image A";
            this.Make_Image_A_button.UseVisualStyleBackColor = true;
            this.Make_Image_A_button.Click += new System.EventHandler(this.Make_Image_A_button_Click);
            // 
            // Make_Image_B_Button
            // 
            this.Make_Image_B_Button.Location = new System.Drawing.Point(1196, 204);
            this.Make_Image_B_Button.Name = "Make_Image_B_Button";
            this.Make_Image_B_Button.Size = new System.Drawing.Size(140, 58);
            this.Make_Image_B_Button.TabIndex = 25;
            this.Make_Image_B_Button.Text = "Make Image B";
            this.Make_Image_B_Button.UseVisualStyleBackColor = true;
            this.Make_Image_B_Button.Click += new System.EventHandler(this.Make_Image_B_Button_Click);
            // 
            // Make_Image_C_button
            // 
            this.Make_Image_C_button.Location = new System.Drawing.Point(1342, 206);
            this.Make_Image_C_button.Name = "Make_Image_C_button";
            this.Make_Image_C_button.Size = new System.Drawing.Size(142, 56);
            this.Make_Image_C_button.TabIndex = 26;
            this.Make_Image_C_button.Text = "Make Image C";
            this.Make_Image_C_button.UseVisualStyleBackColor = true;
            this.Make_Image_C_button.Click += new System.EventHandler(this.Make_Image_C_button_Click);
            // 
            // INFOIBV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1672, 587);
            this.Controls.Add(this.Make_Image_C_button);
            this.Controls.Add(this.Make_Image_B_Button);
            this.Controls.Add(this.Make_Image_A_button);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EdgeSharpinnig);
            this.Controls.Add(this.HistogramEqualization);
            this.Controls.Add(this.HistogramButton);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.Threshold);
            this.Controls.Add(this.EdgeDed);
            this.Controls.Add(this.MedianFilt);
            this.Controls.Add(this.GausFilt);
            this.Controls.Add(this.Contrast_Adjustment);
            this.Controls.Add(this.Invers);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.Conver_to_Gray);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.imageFileName);
            this.Controls.Add(this.LoadImageButton);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "INFOIBV";
            this.ShowIcon = false;
            this.Text = "INFOIBV";
            this.Load += new System.EventHandler(this.INFOIBV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.TextBox imageFileName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Conver_to_Gray;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button Invers;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button Contrast_Adjustment;
        private System.Windows.Forms.Button GausFilt;
        private System.Windows.Forms.Button MedianFilt;
        private System.Windows.Forms.Button EdgeDed;
        private System.Windows.Forms.Button Threshold;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button HistogramButton;
        private System.Windows.Forms.Button HistogramEqualization;
        private System.Windows.Forms.Button EdgeSharpinnig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button Make_Image_A_button;
        private System.Windows.Forms.Button Make_Image_B_Button;
        private System.Windows.Forms.Button Make_Image_C_button;
    }
}
