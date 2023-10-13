namespace INFOIBV
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
            this.LoadImageButton1 = new System.Windows.Forms.Button();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.ImageFileName1 = new System.Windows.Forms.TextBox();
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.Make_Image_A_button = new System.Windows.Forms.Button();
            this.Make_Image_B_Button = new System.Windows.Forms.Button();
            this.Make_Image_C_button = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.StrucElemSize = new System.Windows.Forms.NumericUpDown();
            this.SetStrucElemShape = new System.Windows.Forms.ListBox();
            this.StructureElementBuildButton = new System.Windows.Forms.Button();
            this.FunctionList = new System.Windows.Forms.ListBox();
            this.DoSelectedFunction = new System.Windows.Forms.Button();
            this.AND_BinaryImage = new System.Windows.Forms.Button();
            this.OR_BinaryImage = new System.Windows.Forms.Button();
            this.LoadImage2 = new System.Windows.Forms.Button();
            this.ImageFileName2 = new System.Windows.Forms.TextBox();
            this.Image2 = new System.Windows.Forms.Label();
            this.Image1 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.Image3Label = new System.Windows.Forms.Label();
            this.Image4Label = new System.Windows.Forms.Label();
            this.CountValues = new System.Windows.Forms.Button();
            this.Real_traceBoundary = new System.Windows.Forms.Button();
            this.ExtractLargestShapeButton = new System.Windows.Forms.Button();
            this.Floodfill = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StrucElemSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadImageButton1
            // 
            this.LoadImageButton1.Location = new System.Drawing.Point(12, 3);
            this.LoadImageButton1.Name = "LoadImageButton1";
            this.LoadImageButton1.Size = new System.Drawing.Size(98, 23);
            this.LoadImageButton1.TabIndex = 0;
            this.LoadImageButton1.Text = "Load image-1";
            this.LoadImageButton1.UseVisualStyleBackColor = true;
            this.LoadImageButton1.Click += new System.EventHandler(this.LoadImageButton1_Click);
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "Bitmap files (*.bmp;*.gif;*.jpg;*.png;*.tiff;*.jpeg)|*.bmp;*.gif;*.jpg;*.png;*.ti" +
    "ff;*.jpeg";
            this.openImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // ImageFileName1
            // 
            this.ImageFileName1.Location = new System.Drawing.Point(116, 5);
            this.ImageFileName1.Name = "ImageFileName1";
            this.ImageFileName1.ReadOnly = true;
            this.ImageFileName1.Size = new System.Drawing.Size(316, 20);
            this.ImageFileName1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 326);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // Conver_to_Gray
            // 
            this.Conver_to_Gray.Location = new System.Drawing.Point(478, 3);
            this.Conver_to_Gray.Name = "Conver_to_Gray";
            this.Conver_to_Gray.Size = new System.Drawing.Size(103, 23);
            this.Conver_to_Gray.TabIndex = 3;
            this.Conver_to_Gray.Text = "Convert to Gray";
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
            this.saveButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.saveButton.Location = new System.Drawing.Point(1573, 2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(95, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save as BMP...";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(530, 326);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(512, 512);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.PictureBox2_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(478, 29);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(103, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;
            // 
            // Invers
            // 
            this.Invers.Location = new System.Drawing.Point(587, 5);
            this.Invers.Name = "Invers";
            this.Invers.Size = new System.Drawing.Size(50, 23);
            this.Invers.TabIndex = 7;
            this.Invers.Text = "Inverse";
            this.Invers.UseVisualStyleBackColor = true;
            this.Invers.Click += new System.EventHandler(this.Invers_Click);
            // 
            // Contrast_Adjustment
            // 
            this.Contrast_Adjustment.Location = new System.Drawing.Point(587, 29);
            this.Contrast_Adjustment.Name = "Contrast_Adjustment";
            this.Contrast_Adjustment.Size = new System.Drawing.Size(52, 23);
            this.Contrast_Adjustment.TabIndex = 8;
            this.Contrast_Adjustment.Text = "ContAdj";
            this.Contrast_Adjustment.UseVisualStyleBackColor = true;
            this.Contrast_Adjustment.Click += new System.EventHandler(this.Contrast_Adjustment_Click);
            // 
            // GausFilt
            // 
            this.GausFilt.Location = new System.Drawing.Point(642, 7);
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
            this.MedianFilt.Location = new System.Drawing.Point(707, 7);
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
            this.EdgeDed.Location = new System.Drawing.Point(844, 7);
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
            this.Threshold.Location = new System.Drawing.Point(954, 2);
            this.Threshold.Margin = new System.Windows.Forms.Padding(2);
            this.Threshold.Name = "Threshold";
            this.Threshold.Size = new System.Drawing.Size(88, 23);
            this.Threshold.TabIndex = 12;
            this.Threshold.Text = "Threshold";
            this.Threshold.UseVisualStyleBackColor = true;
            this.Threshold.Click += new System.EventHandler(this.Threshold_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(1047, 582);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(256, 256);
            this.pictureBox3.TabIndex = 14;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.PictureBox3_Click);
            // 
            // HistogramButton
            // 
            this.HistogramButton.Location = new System.Drawing.Point(775, 7);
            this.HistogramButton.Name = "HistogramButton";
            this.HistogramButton.Size = new System.Drawing.Size(64, 23);
            this.HistogramButton.TabIndex = 0;
            this.HistogramButton.Text = "Histogram";
            this.HistogramButton.Click += new System.EventHandler(this.HistogramButton_Click);
            // 
            // HistogramEqualization
            // 
            this.HistogramEqualization.Location = new System.Drawing.Point(775, 32);
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
            this.EdgeSharpinnig.Location = new System.Drawing.Point(844, 32);
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
            this.label1.Location = new System.Drawing.Point(1045, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(468, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "To get Image A: 1. Load image 2. Convert to Gray 3.ContAdj    /   1. Load image 2" +
    ". Make Image A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1045, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(623, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "To get Image B: 1. Load image 2. Convert to Gray 3. Cont Adj 4. GausFilt 5. EdgeD" +
    "e 6. Thres    /   1. Load image 2. Make Image B";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1045, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(624, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "To get Image C: 1. Load image 2. Convert to Gray 3. ContAdj 4. MedianFilt 5. Edge" +
    "De 6. Thres  /   1. Load image 2. Make Image C";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1047, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(305, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Filter and threshold parameters can be changed in the program.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label5.Location = new System.Drawing.Point(1045, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(363, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Convert to Gray, Inverse, and Floodfill buttons work on the loaded Image-1. ";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(642, 32);
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
            this.Make_Image_A_button.Location = new System.Drawing.Point(954, 26);
            this.Make_Image_A_button.Name = "Make_Image_A_button";
            this.Make_Image_A_button.Size = new System.Drawing.Size(88, 21);
            this.Make_Image_A_button.TabIndex = 24;
            this.Make_Image_A_button.Text = "Make Image A";
            this.Make_Image_A_button.UseVisualStyleBackColor = true;
            this.Make_Image_A_button.Click += new System.EventHandler(this.Make_Image_A_button_Click);
            // 
            // Make_Image_B_Button
            // 
            this.Make_Image_B_Button.Location = new System.Drawing.Point(955, 49);
            this.Make_Image_B_Button.Name = "Make_Image_B_Button";
            this.Make_Image_B_Button.Size = new System.Drawing.Size(87, 21);
            this.Make_Image_B_Button.TabIndex = 25;
            this.Make_Image_B_Button.Text = "Make Image B";
            this.Make_Image_B_Button.UseVisualStyleBackColor = true;
            this.Make_Image_B_Button.Click += new System.EventHandler(this.Make_Image_B_Button_Click);
            // 
            // Make_Image_C_button
            // 
            this.Make_Image_C_button.Location = new System.Drawing.Point(955, 75);
            this.Make_Image_C_button.Name = "Make_Image_C_button";
            this.Make_Image_C_button.Size = new System.Drawing.Size(87, 23);
            this.Make_Image_C_button.TabIndex = 26;
            this.Make_Image_C_button.Text = "Make Image C";
            this.Make_Image_C_button.UseVisualStyleBackColor = true;
            this.Make_Image_C_button.Click += new System.EventHandler(this.Make_Image_C_button_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(470, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(565, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "_________________________________________________________________________________" +
    "____________";
            // 
            // StrucElemSize
            // 
            this.StrucElemSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.StrucElemSize.Location = new System.Drawing.Point(475, 168);
            this.StrucElemSize.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.StrucElemSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.StrucElemSize.Name = "StrucElemSize";
            this.StrucElemSize.Size = new System.Drawing.Size(49, 20);
            this.StrucElemSize.TabIndex = 28;
            this.StrucElemSize.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // SetStrucElemShape
            // 
            this.SetStrucElemShape.FormattingEnabled = true;
            this.SetStrucElemShape.Items.AddRange(new object[] {
            "Plus",
            "Square"});
            this.SetStrucElemShape.Location = new System.Drawing.Point(473, 144);
            this.SetStrucElemShape.Name = "SetStrucElemShape";
            this.SetStrucElemShape.Size = new System.Drawing.Size(51, 17);
            this.SetStrucElemShape.TabIndex = 29;
            // 
            // StructureElementBuildButton
            // 
            this.StructureElementBuildButton.Location = new System.Drawing.Point(473, 119);
            this.StructureElementBuildButton.Name = "StructureElementBuildButton";
            this.StructureElementBuildButton.Size = new System.Drawing.Size(51, 23);
            this.StructureElementBuildButton.TabIndex = 34;
            this.StructureElementBuildButton.Text = "StrElm";
            this.StructureElementBuildButton.UseVisualStyleBackColor = true;
            this.StructureElementBuildButton.Click += new System.EventHandler(this.StructureElementBuildButton_Click);
            // 
            // FunctionList
            // 
            this.FunctionList.FormattingEnabled = true;
            this.FunctionList.Items.AddRange(new object[] {
            "Erode",
            "Dilate",
            "Open",
            "Close"});
            this.FunctionList.Location = new System.Drawing.Point(530, 119);
            this.FunctionList.Name = "FunctionList";
            this.FunctionList.Size = new System.Drawing.Size(109, 17);
            this.FunctionList.TabIndex = 35;
            // 
            // DoSelectedFunction
            // 
            this.DoSelectedFunction.Location = new System.Drawing.Point(530, 142);
            this.DoSelectedFunction.Name = "DoSelectedFunction";
            this.DoSelectedFunction.Size = new System.Drawing.Size(109, 23);
            this.DoSelectedFunction.TabIndex = 36;
            this.DoSelectedFunction.Text = "Do Function Above";
            this.DoSelectedFunction.UseVisualStyleBackColor = true;
            this.DoSelectedFunction.Click += new System.EventHandler(this.DoSelectedFunction_Click);
            // 
            // AND_BinaryImage
            // 
            this.AND_BinaryImage.Location = new System.Drawing.Point(645, 118);
            this.AND_BinaryImage.Name = "AND_BinaryImage";
            this.AND_BinaryImage.Size = new System.Drawing.Size(62, 23);
            this.AND_BinaryImage.TabIndex = 37;
            this.AND_BinaryImage.Text = "AND-B.I.";
            this.AND_BinaryImage.UseVisualStyleBackColor = true;
            this.AND_BinaryImage.Click += new System.EventHandler(this.AND_BinaryImage_Click_1);
            // 
            // OR_BinaryImage
            // 
            this.OR_BinaryImage.Location = new System.Drawing.Point(645, 142);
            this.OR_BinaryImage.Name = "OR_BinaryImage";
            this.OR_BinaryImage.Size = new System.Drawing.Size(62, 23);
            this.OR_BinaryImage.TabIndex = 38;
            this.OR_BinaryImage.Text = "OR-B.I.";
            this.OR_BinaryImage.UseVisualStyleBackColor = true;
            this.OR_BinaryImage.Click += new System.EventHandler(this.OR_BinaryImage_Click_1);
            // 
            // LoadImage2
            // 
            this.LoadImage2.Location = new System.Drawing.Point(12, 32);
            this.LoadImage2.Name = "LoadImage2";
            this.LoadImage2.Size = new System.Drawing.Size(98, 23);
            this.LoadImage2.TabIndex = 39;
            this.LoadImage2.Text = "Load image-2";
            this.LoadImage2.UseVisualStyleBackColor = true;
            this.LoadImage2.Click += new System.EventHandler(this.LoadImage2_Click);
            // 
            // ImageFileName2
            // 
            this.ImageFileName2.Location = new System.Drawing.Point(116, 35);
            this.ImageFileName2.Name = "ImageFileName2";
            this.ImageFileName2.ReadOnly = true;
            this.ImageFileName2.Size = new System.Drawing.Size(316, 20);
            this.ImageFileName2.TabIndex = 40;
            // 
            // Image2
            // 
            this.Image2.AutoSize = true;
            this.Image2.Location = new System.Drawing.Point(748, 850);
            this.Image2.Name = "Image2";
            this.Image2.Size = new System.Drawing.Size(45, 13);
            this.Image2.TabIndex = 41;
            this.Image2.Text = "Image-2";
            // 
            // Image1
            // 
            this.Image1.AutoSize = true;
            this.Image1.Location = new System.Drawing.Point(203, 850);
            this.Image1.Name = "Image1";
            this.Image1.Size = new System.Drawing.Size(45, 13);
            this.Image1.TabIndex = 42;
            this.Image1.Text = "Image-1";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(1308, 326);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(512, 512);
            this.pictureBox4.TabIndex = 43;
            this.pictureBox4.TabStop = false;
            // 
            // Image3Label
            // 
            this.Image3Label.AutoSize = true;
            this.Image3Label.Location = new System.Drawing.Point(1160, 850);
            this.Image3Label.Name = "Image3Label";
            this.Image3Label.Size = new System.Drawing.Size(45, 13);
            this.Image3Label.TabIndex = 44;
            this.Image3Label.Text = "Image-3";
            // 
            // Image4Label
            // 
            this.Image4Label.AutoSize = true;
            this.Image4Label.Location = new System.Drawing.Point(1540, 850);
            this.Image4Label.Name = "Image4Label";
            this.Image4Label.Size = new System.Drawing.Size(45, 13);
            this.Image4Label.TabIndex = 45;
            this.Image4Label.Text = "Image-4";
            // 
            // CountValues
            // 
            this.CountValues.Location = new System.Drawing.Point(713, 119);
            this.CountValues.Name = "CountValues";
            this.CountValues.Size = new System.Drawing.Size(80, 23);
            this.CountValues.TabIndex = 46;
            this.CountValues.Text = "CountValues";
            this.CountValues.UseVisualStyleBackColor = true;
            this.CountValues.Click += new System.EventHandler(this.CountValues_Click);
            // 
            // Real_traceBoundary
            // 
            this.Real_traceBoundary.Location = new System.Drawing.Point(714, 144);
            this.Real_traceBoundary.Name = "Real_traceBoundary";
            this.Real_traceBoundary.Size = new System.Drawing.Size(79, 23);
            this.Real_traceBoundary.TabIndex = 48;
            this.Real_traceBoundary.Text = "traceBoundary";
            this.Real_traceBoundary.UseVisualStyleBackColor = true;
            this.Real_traceBoundary.Click += new System.EventHandler(this.Real_traceBoundary_Click);
            // 
            // ExtractLargestShapeButton
            // 
            this.ExtractLargestShapeButton.Location = new System.Drawing.Point(799, 144);
            this.ExtractLargestShapeButton.Name = "ExtractLargestShapeButton";
            this.ExtractLargestShapeButton.Size = new System.Drawing.Size(116, 24);
            this.ExtractLargestShapeButton.TabIndex = 49;
            this.ExtractLargestShapeButton.Text = "ExtractLargestShape";
            this.ExtractLargestShapeButton.UseVisualStyleBackColor = true;
            this.ExtractLargestShapeButton.Click += new System.EventHandler(this.ExtractLargestShapeButton_Click);
            // 
            // Floodfill
            // 
            this.Floodfill.Location = new System.Drawing.Point(800, 118);
            this.Floodfill.Name = "Floodfill";
            this.Floodfill.Size = new System.Drawing.Size(75, 23);
            this.Floodfill.TabIndex = 50;
            this.Floodfill.Text = "Floodfill";
            this.Floodfill.UseVisualStyleBackColor = true;
            this.Floodfill.Click += new System.EventHandler(this.Floodfill_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(1045, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(312, 13);
            this.label6.TabIndex = 51;
            this.label6.Text = "CountVlaues works on Image-4. Other buttons work on Image-2. ";
            // 
            // INFOIBV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Floodfill);
            this.Controls.Add(this.ExtractLargestShapeButton);
            this.Controls.Add(this.Real_traceBoundary);
            this.Controls.Add(this.CountValues);
            this.Controls.Add(this.Image4Label);
            this.Controls.Add(this.Image3Label);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.Image1);
            this.Controls.Add(this.Image2);
            this.Controls.Add(this.ImageFileName2);
            this.Controls.Add(this.LoadImage2);
            this.Controls.Add(this.OR_BinaryImage);
            this.Controls.Add(this.AND_BinaryImage);
            this.Controls.Add(this.DoSelectedFunction);
            this.Controls.Add(this.FunctionList);
            this.Controls.Add(this.StructureElementBuildButton);
            this.Controls.Add(this.SetStrucElemShape);
            this.Controls.Add(this.StrucElemSize);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Make_Image_C_button);
            this.Controls.Add(this.Make_Image_B_Button);
            this.Controls.Add(this.Make_Image_A_button);
            this.Controls.Add(this.numericUpDown1);
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
            this.Controls.Add(this.ImageFileName1);
            this.Controls.Add(this.LoadImageButton1);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "INFOIBV";
            this.ShowIcon = false;
            this.Text = "INFOIBV";
            this.Load += new System.EventHandler(this.INFOIBV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StrucElemSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadImageButton1;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.TextBox ImageFileName1;
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
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button Make_Image_A_button;
        private System.Windows.Forms.Button Make_Image_B_Button;
        private System.Windows.Forms.Button Make_Image_C_button;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown StrucElemSize;
        private System.Windows.Forms.ListBox SetStrucElemShape;
        private System.Windows.Forms.Button StructureElementBuildButton;
        private System.Windows.Forms.ListBox FunctionList;
        private System.Windows.Forms.Button DoSelectedFunction;
        private System.Windows.Forms.Button AND_BinaryImage;
        private System.Windows.Forms.Button OR_BinaryImage;
        private System.Windows.Forms.Button LoadImage2;
        private System.Windows.Forms.TextBox ImageFileName2;
        private System.Windows.Forms.Label Image2;
        private System.Windows.Forms.Label Image1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label Image3Label;
        private System.Windows.Forms.Label Image4Label;
        private System.Windows.Forms.Button CountValues;
        private System.Windows.Forms.Button Real_traceBoundary;
        private System.Windows.Forms.Button ExtractLargestShapeButton;
        private System.Windows.Forms.Button Floodfill;
        private System.Windows.Forms.Label label6;
    }
}

