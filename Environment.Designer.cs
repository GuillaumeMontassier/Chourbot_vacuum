
namespace Chourbot_vacuum
{
    partial class form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.start_vacuum = new System.Windows.Forms.Button();
            this.breadth_first_search = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.a_star = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.bfs_iteration_number = new System.Windows.Forms.Label();
            this.astar_iteration_number = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.electricity_number = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dust = new System.Windows.Forms.Label();
            this.jewel_pick_up = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.max_depth_selector = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.max_depth_selector)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(872, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeColumns = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.ColumnHeadersVisible = false;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.grid.Enabled = false;
            this.grid.Location = new System.Drawing.Point(25, 27);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grid.RowHeadersVisible = false;
            this.grid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.grid.ShowEditingIcon = false;
            this.grid.Size = new System.Drawing.Size(400, 400);
            this.grid.TabIndex = 26;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // start_vacuum
            // 
            this.start_vacuum.AllowDrop = true;
            this.start_vacuum.Location = new System.Drawing.Point(599, 203);
            this.start_vacuum.Name = "start_vacuum";
            this.start_vacuum.Size = new System.Drawing.Size(96, 23);
            this.start_vacuum.TabIndex = 35;
            this.start_vacuum.Text = "Start Vacuum";
            this.start_vacuum.UseVisualStyleBackColor = true;
            this.start_vacuum.Click += new System.EventHandler(this.start_vacuum_Click);
            // 
            // breadth_first_search
            // 
            this.breadth_first_search.AutoSize = true;
            this.breadth_first_search.Location = new System.Drawing.Point(6, 19);
            this.breadth_first_search.Name = "breadth_first_search";
            this.breadth_first_search.Size = new System.Drawing.Size(121, 17);
            this.breadth_first_search.TabIndex = 36;
            this.breadth_first_search.TabStop = true;
            this.breadth_first_search.Text = "Breadth-First Search";
            this.breadth_first_search.UseVisualStyleBackColor = true;
            this.breadth_first_search.CheckedChanged += new System.EventHandler(this.breadth_first_search_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.a_star);
            this.groupBox1.Controls.Add(this.breadth_first_search);
            this.groupBox1.Location = new System.Drawing.Point(446, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 80);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose an algorithm";
            // 
            // a_star
            // 
            this.a_star.AutoSize = true;
            this.a_star.Location = new System.Drawing.Point(6, 49);
            this.a_star.Name = "a_star";
            this.a_star.Size = new System.Drawing.Size(36, 17);
            this.a_star.TabIndex = 37;
            this.a_star.TabStop = true;
            this.a_star.Text = "A*";
            this.a_star.UseVisualStyleBackColor = true;
            this.a_star.CheckedChanged += new System.EventHandler(this.a_star_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "BFS";
            // 
            // bfs_iteration_number
            // 
            this.bfs_iteration_number.AutoSize = true;
            this.bfs_iteration_number.Location = new System.Drawing.Point(73, 28);
            this.bfs_iteration_number.Name = "bfs_iteration_number";
            this.bfs_iteration_number.Size = new System.Drawing.Size(13, 13);
            this.bfs_iteration_number.TabIndex = 40;
            this.bfs_iteration_number.Text = "0";
            // 
            // astar_iteration_number
            // 
            this.astar_iteration_number.AutoSize = true;
            this.astar_iteration_number.Location = new System.Drawing.Point(73, 58);
            this.astar_iteration_number.Name = "astar_iteration_number";
            this.astar_iteration_number.Size = new System.Drawing.Size(13, 13);
            this.astar_iteration_number.TabIndex = 42;
            this.astar_iteration_number.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "A*";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.bfs_iteration_number);
            this.groupBox2.Controls.Add(this.astar_iteration_number);
            this.groupBox2.Location = new System.Drawing.Point(599, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(135, 80);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Iteration Number";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.electricity_number);
            this.groupBox3.Location = new System.Drawing.Point(599, 124);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(135, 57);
            this.groupBox3.TabIndex = 46;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Electricity Consummed";
            // 
            // electricity_number
            // 
            this.electricity_number.AutoSize = true;
            this.electricity_number.Location = new System.Drawing.Point(57, 25);
            this.electricity_number.Name = "electricity_number";
            this.electricity_number.Size = new System.Drawing.Size(13, 13);
            this.electricity_number.TabIndex = 0;
            this.electricity_number.Text = "0";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.dust);
            this.groupBox4.Controls.Add(this.jewel_pick_up);
            this.groupBox4.Location = new System.Drawing.Point(446, 124);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(147, 57);
            this.groupBox4.TabIndex = 47;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Objects Collected";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Jewel";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Dust";
            // 
            // dust
            // 
            this.dust.AutoSize = true;
            this.dust.Location = new System.Drawing.Point(114, 25);
            this.dust.Name = "dust";
            this.dust.Size = new System.Drawing.Size(13, 13);
            this.dust.TabIndex = 1;
            this.dust.Text = "0";
            // 
            // jewel_pick_up
            // 
            this.jewel_pick_up.AutoSize = true;
            this.jewel_pick_up.Location = new System.Drawing.Point(47, 25);
            this.jewel_pick_up.Name = "jewel_pick_up";
            this.jewel_pick_up.Size = new System.Drawing.Size(13, 13);
            this.jewel_pick_up.TabIndex = 0;
            this.jewel_pick_up.Text = "0";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.max_depth_selector);
            this.groupBox5.Location = new System.Drawing.Point(477, 187);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(95, 51);
            this.groupBox5.TabIndex = 49;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Max Depth A*";
            // 
            // max_depth_selector
            // 
            this.max_depth_selector.Location = new System.Drawing.Point(18, 19);
            this.max_depth_selector.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.max_depth_selector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.max_depth_selector.Name = "max_depth_selector";
            this.max_depth_selector.Size = new System.Drawing.Size(60, 20);
            this.max_depth_selector.TabIndex = 50;
            this.max_depth_selector.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 460);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.start_vacuum);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chourbot_vacuum";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.max_depth_selector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button start_vacuum;
        private System.Windows.Forms.RadioButton breadth_first_search;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton a_star;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label bfs_iteration_number;
        private System.Windows.Forms.Label astar_iteration_number;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label electricity_number;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label dust;
        private System.Windows.Forms.Label jewel_pick_up;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown max_depth_selector;
    }
}

