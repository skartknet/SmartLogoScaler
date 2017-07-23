namespace ImageNormalizer
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lstViewImages = new System.Windows.Forms.ListView();
            this.sliderScale = new System.Windows.Forms.TrackBar();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(90, 90);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Images|*.jpg";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(56, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(229, 53);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open Images";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Height (in pixels)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Width (in pixels)";
            // 
            // numHeight
            // 
            this.numHeight.Location = new System.Drawing.Point(230, 67);
            this.numHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(120, 35);
            this.numHeight.TabIndex = 5;
            this.numHeight.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numHeight.ValueChanged += new System.EventHandler(this.numHeight_ValueChanged);
            // 
            // numWidth
            // 
            this.numWidth.Location = new System.Drawing.Point(230, 121);
            this.numWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(120, 35);
            this.numWidth.TabIndex = 6;
            this.numWidth.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numWidth.ValueChanged += new System.EventHandler(this.numWidth_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numWidth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numHeight);
            this.groupBox1.Location = new System.Drawing.Point(56, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 200);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Container Dimensions";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(56, 341);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(201, 88);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save Results";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // lstViewImages
            // 
            this.lstViewImages.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.lstViewImages.CheckBoxes = true;
            this.lstViewImages.LabelEdit = true;
            this.lstViewImages.LargeImageList = this.imageList1;
            this.lstViewImages.Location = new System.Drawing.Point(56, 457);
            this.lstViewImages.MultiSelect = false;
            this.lstViewImages.Name = "lstViewImages";
            this.lstViewImages.Size = new System.Drawing.Size(623, 649);
            this.lstViewImages.SmallImageList = this.imageList1;
            this.lstViewImages.TabIndex = 9;
            this.lstViewImages.UseCompatibleStateImageBehavior = false;
            this.lstViewImages.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstViewImages_ItemChecked);
            this.lstViewImages.SelectedIndexChanged += new System.EventHandler(this.lstViewImages_SelectedIndexChanged);
            // 
            // sliderScale
            // 
            this.sliderScale.Enabled = false;
            this.sliderScale.Location = new System.Drawing.Point(742, 992);
            this.sliderScale.Maximum = 100;
            this.sliderScale.Name = "sliderScale";
            this.sliderScale.Size = new System.Drawing.Size(354, 101);
            this.sliderScale.TabIndex = 11;
            this.sliderScale.Value = 100;
            this.sliderScale.Scroll += new System.EventHandler(this.sliderZoom_Scroll);
            // 
            // txtScale
            // 
            this.txtScale.Enabled = false;
            this.txtScale.Location = new System.Drawing.Point(1122, 1005);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(100, 35);
            this.txtScale.TabIndex = 12;
            this.txtScale.Text = "100";
            this.txtScale.TextChanged += new System.EventHandler(this.txtScale_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(771, 952);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 29);
            this.label1.TabIndex = 13;
            this.label1.Text = "Scale";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2048, 1231);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtScale);
            this.Controls.Add(this.sliderScale);
            this.Controls.Add(this.lstViewImages);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "frmMain";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "Image Processor";
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListView lstViewImages;
        private System.Windows.Forms.TrackBar sliderScale;
        private System.Windows.Forms.TextBox txtScale;
        private System.Windows.Forms.Label label1;
        private System.Diagnostics.PerformanceCounter performanceCounter1;
    }
}

