namespace MiniGis
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolSelect = new System.Windows.Forms.ToolStripButton();
            this.toolPan = new System.Windows.Forms.ToolStripButton();
            this.toolZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolSumPolygons = new System.Windows.Forms.ToolStripButton();
            this.toolZoomAll = new System.Windows.Forms.ToolStripButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.MyMap = new MiniGis.Core.Map();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSelect,
            this.toolPan,
            this.toolZoomIn,
            this.toolZoomOut,
            this.toolSumPolygons,
            this.toolZoomAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1623, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolSelect
            // 
            this.toolSelect.Checked = true;
            this.toolSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSelect.Enabled = false;
            this.toolSelect.Image = ((System.Drawing.Image)(resources.GetObject("toolSelect.Image")));
            this.toolSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSelect.Name = "toolSelect";
            this.toolSelect.Size = new System.Drawing.Size(29, 24);
            this.toolSelect.Text = "Select";
            this.toolSelect.ToolTipText = "toolSelect\r\n";
            this.toolSelect.Click += new System.EventHandler(this.toolSelect_Click);
            // 
            // toolPan
            // 
            this.toolPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolPan.Image = ((System.Drawing.Image)(resources.GetObject("toolPan.Image")));
            this.toolPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPan.Name = "toolPan";
            this.toolPan.Size = new System.Drawing.Size(29, 24);
            this.toolPan.Text = "Pan";
            this.toolPan.Click += new System.EventHandler(this.toolPan_Click);
            // 
            // toolZoomIn
            // 
            this.toolZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("toolZoomIn.Image")));
            this.toolZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolZoomIn.Name = "toolZoomIn";
            this.toolZoomIn.Size = new System.Drawing.Size(29, 24);
            this.toolZoomIn.Text = "Zoom in";
            this.toolZoomIn.Click += new System.EventHandler(this.toolZoomIn_Click);
            // 
            // toolZoomOut
            // 
            this.toolZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("toolZoomOut.Image")));
            this.toolZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolZoomOut.Name = "toolZoomOut";
            this.toolZoomOut.Size = new System.Drawing.Size(29, 24);
            this.toolZoomOut.Text = "Zoom out";
            this.toolZoomOut.Click += new System.EventHandler(this.toolZoomOut_Click);
            // 
            // toolSumPolygons
            // 
            this.toolSumPolygons.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSumPolygons.Image = ((System.Drawing.Image)(resources.GetObject("toolSumPolygons.Image")));
            this.toolSumPolygons.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSumPolygons.Name = "toolSumPolygons";
            this.toolSumPolygons.Size = new System.Drawing.Size(29, 24);
            this.toolSumPolygons.Text = "toolSumPolygons";
          
            // 
            // toolZoomAll
            // 
            this.toolZoomAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolZoomAll.Image = ((System.Drawing.Image)(resources.GetObject("toolZoomAll.Image")));
            this.toolZoomAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolZoomAll.Name = "toolZoomAll";
            this.toolZoomAll.Size = new System.Drawing.Size(29, 24);
            this.toolZoomAll.Text = "toolZoomAll";
            this.toolZoomAll.Click += new System.EventHandler(this.toolZoomAll_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(301, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 37);
            this.button1.TabIndex = 4;
            this.button1.Text = "SumPolygons";
            this.button1.UseVisualStyleBackColor = true;
           
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(571, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
         
            // 
            // MyMap
            // 
            this.MyMap.ActiveTool = MiniGis.Core.MapToolType.Select;
            this.MyMap.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.MyMap.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MyMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyMap.Location = new System.Drawing.Point(0, 0);
            this.MyMap.MapScale = 1D;
            this.MyMap.Margin = new System.Windows.Forms.Padding(5);
            this.MyMap.Name = "MyMap";
            this.MyMap.Size = new System.Drawing.Size(1623, 752);
            this.MyMap.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1623, 752);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.MyMap);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MiniGis";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Core.Map MyMap;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolSelect;
        private System.Windows.Forms.ToolStripButton toolPan;
        private System.Windows.Forms.ToolStripButton toolZoomIn;
        private System.Windows.Forms.ToolStripButton toolZoomOut;
        private System.Windows.Forms.ToolStripButton toolSumPolygons;
        private System.Windows.Forms.ToolStripButton toolZoomAll;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

