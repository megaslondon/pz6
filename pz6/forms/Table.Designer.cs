namespace pz6.forms
{
    partial class Table
    {
        
        
        
        private System.ComponentModel.IContainer components = null;

        
        
        
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        
        
        
        
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Table));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.InsTable = new System.Windows.Forms.ToolStripButton();
            this.EditTable = new System.Windows.Forms.ToolStripButton();
            this.DelTable = new System.Windows.Forms.ToolStripButton();
            this.CloseTable = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            
            
            
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InsTable,
            this.EditTable,
            this.DelTable,
            this.CloseTable});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(453, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            
            
            
            this.InsTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InsTable.Image = ((System.Drawing.Image)(resources.GetObject("InsTable.Image")));
            this.InsTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InsTable.Name = "InsTable";
            this.InsTable.Size = new System.Drawing.Size(23, 22);
            this.InsTable.Text = "Добавление данных";
            this.InsTable.Click += new System.EventHandler(this.toolStripButton1_Click);
            
            
            
            this.EditTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditTable.Image = ((System.Drawing.Image)(resources.GetObject("EditTable.Image")));
            this.EditTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditTable.Name = "EditTable";
            this.EditTable.Size = new System.Drawing.Size(23, 22);
            this.EditTable.Text = "Редактирование данных";
            
            
            
            this.DelTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DelTable.Image = ((System.Drawing.Image)(resources.GetObject("DelTable.Image")));
            this.DelTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DelTable.Name = "DelTable";
            this.DelTable.Size = new System.Drawing.Size(23, 22);
            this.DelTable.Text = "Удаление данных";
            this.DelTable.Click += new System.EventHandler(this.DelTable_Click);
            
            
            
            this.CloseTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CloseTable.Image = ((System.Drawing.Image)(resources.GetObject("CloseTable.Image")));
            this.CloseTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseTable.Name = "CloseTable";
            this.CloseTable.Size = new System.Drawing.Size(23, 22);
            this.CloseTable.Text = "Выход";
            
            
            
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(453, 214);
            this.dataGridView1.TabIndex = 1;
            
            
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 239);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Table";
            this.Text = "Table";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton InsTable;
        private System.Windows.Forms.ToolStripButton EditTable;
        private System.Windows.Forms.ToolStripButton DelTable;
        private System.Windows.Forms.ToolStripButton CloseTable;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
