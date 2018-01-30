namespace KirbyRandomizer
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
            this.components = new System.ComponentModel.Container();
            this.randSettingsGroup = new System.Windows.Forms.GroupBox();
            this.randSeed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.overwriteROM = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.randOneElement = new System.Windows.Forms.RadioButton();
            this.randElementsEach = new System.Windows.Forms.RadioButton();
            this.randElements = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.randBossAbilities = new System.Windows.Forms.CheckBox();
            this.randMiniBossAbilities = new System.Windows.Forms.CheckBox();
            this.includeMinorEnemies = new System.Windows.Forms.CheckBox();
            this.randEnemies = new System.Windows.Forms.CheckBox();
            this.loadROM = new System.Windows.Forms.Button();
            this.filePath = new System.Windows.Forms.TextBox();
            this.randomize = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.romRegion = new System.Windows.Forms.Label();
            this.randSettingsGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // randSettingsGroup
            // 
            this.randSettingsGroup.Controls.Add(this.randSeed);
            this.randSettingsGroup.Controls.Add(this.label1);
            this.randSettingsGroup.Controls.Add(this.groupBox1);
            this.randSettingsGroup.Controls.Add(this.groupBox3);
            this.randSettingsGroup.Controls.Add(this.groupBox2);
            this.randSettingsGroup.Enabled = false;
            this.randSettingsGroup.Location = new System.Drawing.Point(12, 62);
            this.randSettingsGroup.Name = "randSettingsGroup";
            this.randSettingsGroup.Size = new System.Drawing.Size(292, 335);
            this.randSettingsGroup.TabIndex = 0;
            this.randSettingsGroup.TabStop = false;
            this.randSettingsGroup.Text = "Randomizer Settings";
            // 
            // randSeed
            // 
            this.randSeed.Location = new System.Drawing.Point(45, 303);
            this.randSeed.Name = "randSeed";
            this.randSeed.Size = new System.Drawing.Size(241, 20);
            this.randSeed.TabIndex = 4;
            this.toolTip.SetToolTip(this.randSeed, "Leave blank for a random seed");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 306);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Seed ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.overwriteROM);
            this.groupBox1.Location = new System.Drawing.Point(7, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 48);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ROM Settings";
            // 
            // overwriteROM
            // 
            this.overwriteROM.AutoSize = true;
            this.overwriteROM.Location = new System.Drawing.Point(6, 19);
            this.overwriteROM.Name = "overwriteROM";
            this.overwriteROM.Size = new System.Drawing.Size(116, 17);
            this.overwriteROM.TabIndex = 2;
            this.overwriteROM.Text = "Overwrite old ROM";
            this.overwriteROM.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.randOneElement);
            this.groupBox3.Controls.Add(this.randElementsEach);
            this.groupBox3.Controls.Add(this.randElements);
            this.groupBox3.Location = new System.Drawing.Point(7, 204);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(279, 95);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Copy Ability Settings";
            // 
            // randOneElement
            // 
            this.randOneElement.AutoSize = true;
            this.randOneElement.Checked = true;
            this.randOneElement.Enabled = false;
            this.randOneElement.Location = new System.Drawing.Point(26, 44);
            this.randOneElement.Name = "randOneElement";
            this.randOneElement.Size = new System.Drawing.Size(71, 17);
            this.randOneElement.TabIndex = 2;
            this.randOneElement.TabStop = true;
            this.randOneElement.Text = "Per-Ability";
            this.randOneElement.UseVisualStyleBackColor = true;
            // 
            // randElementsEach
            // 
            this.randElementsEach.AutoSize = true;
            this.randElementsEach.Enabled = false;
            this.randElementsEach.Location = new System.Drawing.Point(26, 67);
            this.randElementsEach.Name = "randElementsEach";
            this.randElementsEach.Size = new System.Drawing.Size(75, 17);
            this.randElementsEach.TabIndex = 1;
            this.randElementsEach.Text = "Per-Attack";
            this.randElementsEach.UseVisualStyleBackColor = true;
            // 
            // randElements
            // 
            this.randElements.AutoSize = true;
            this.randElements.Location = new System.Drawing.Point(6, 21);
            this.randElements.Name = "randElements";
            this.randElements.Size = new System.Drawing.Size(182, 17);
            this.randElements.TabIndex = 0;
            this.randElements.Text = "Randomize Copy Ability Elements";
            this.randElements.UseVisualStyleBackColor = true;
            this.randElements.CheckedChanged += new System.EventHandler(this.randElements_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.randBossAbilities);
            this.groupBox2.Controls.Add(this.randMiniBossAbilities);
            this.groupBox2.Controls.Add(this.includeMinorEnemies);
            this.groupBox2.Controls.Add(this.randEnemies);
            this.groupBox2.Location = new System.Drawing.Point(7, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 124);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enemy Settings";
            // 
            // randBossAbilities
            // 
            this.randBossAbilities.AutoSize = true;
            this.randBossAbilities.Enabled = false;
            this.randBossAbilities.Location = new System.Drawing.Point(26, 91);
            this.randBossAbilities.Name = "randBossAbilities";
            this.randBossAbilities.Size = new System.Drawing.Size(98, 17);
            this.randBossAbilities.TabIndex = 3;
            this.randBossAbilities.Text = "Include Bosses";
            this.randBossAbilities.UseVisualStyleBackColor = true;
            // 
            // randMiniBossAbilities
            // 
            this.randMiniBossAbilities.AutoSize = true;
            this.randMiniBossAbilities.Enabled = false;
            this.randMiniBossAbilities.Location = new System.Drawing.Point(26, 67);
            this.randMiniBossAbilities.Name = "randMiniBossAbilities";
            this.randMiniBossAbilities.Size = new System.Drawing.Size(120, 17);
            this.randMiniBossAbilities.TabIndex = 2;
            this.randMiniBossAbilities.Text = "Include Mini Bosses";
            this.randMiniBossAbilities.UseVisualStyleBackColor = true;
            // 
            // includeMinorEnemies
            // 
            this.includeMinorEnemies.AutoSize = true;
            this.includeMinorEnemies.Enabled = false;
            this.includeMinorEnemies.Location = new System.Drawing.Point(26, 43);
            this.includeMinorEnemies.Name = "includeMinorEnemies";
            this.includeMinorEnemies.Size = new System.Drawing.Size(131, 17);
            this.includeMinorEnemies.TabIndex = 1;
            this.includeMinorEnemies.Text = "Include minor enemies";
            this.toolTip.SetToolTip(this.includeMinorEnemies, "Randomizes the Copy Abilities for Enemies that don\'t normally have them");
            this.includeMinorEnemies.UseVisualStyleBackColor = true;
            // 
            // randEnemies
            // 
            this.randEnemies.AutoSize = true;
            this.randEnemies.Location = new System.Drawing.Point(6, 19);
            this.randEnemies.Name = "randEnemies";
            this.randEnemies.Size = new System.Drawing.Size(179, 17);
            this.randEnemies.TabIndex = 0;
            this.randEnemies.Text = "Randomize Enemy Copy Abilities";
            this.randEnemies.UseVisualStyleBackColor = true;
            this.randEnemies.CheckedChanged += new System.EventHandler(this.randEnemies_CheckedChanged);
            // 
            // loadROM
            // 
            this.loadROM.Location = new System.Drawing.Point(12, 12);
            this.loadROM.Name = "loadROM";
            this.loadROM.Size = new System.Drawing.Size(108, 22);
            this.loadROM.TabIndex = 1;
            this.loadROM.Text = "Load KSS ROM";
            this.loadROM.UseVisualStyleBackColor = true;
            this.loadROM.Click += new System.EventHandler(this.loadMINT_Click);
            // 
            // filePath
            // 
            this.filePath.Location = new System.Drawing.Point(126, 13);
            this.filePath.Name = "filePath";
            this.filePath.ReadOnly = true;
            this.filePath.Size = new System.Drawing.Size(178, 20);
            this.filePath.TabIndex = 2;
            // 
            // randomize
            // 
            this.randomize.Enabled = false;
            this.randomize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.randomize.Location = new System.Drawing.Point(12, 403);
            this.randomize.Name = "randomize";
            this.randomize.Size = new System.Drawing.Size(292, 35);
            this.randomize.TabIndex = 3;
            this.randomize.Text = "Randomize";
            this.randomize.UseVisualStyleBackColor = true;
            this.randomize.Click += new System.EventHandler(this.randomize_Click);
            // 
            // romRegion
            // 
            this.romRegion.AutoSize = true;
            this.romRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.romRegion.Location = new System.Drawing.Point(9, 42);
            this.romRegion.Name = "romRegion";
            this.romRegion.Size = new System.Drawing.Size(87, 13);
            this.romRegion.TabIndex = 4;
            this.romRegion.Text = "ROM Region: ";
            this.toolTip.SetToolTip(this.romRegion, "Region of the ROM, affects file reading");
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 451);
            this.Controls.Add(this.romRegion);
            this.Controls.Add(this.randomize);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.loadROM);
            this.Controls.Add(this.randSettingsGroup);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kirby Super Star Randomizer";
            this.randSettingsGroup.ResumeLayout(false);
            this.randSettingsGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox randSettingsGroup;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton randOneElement;
        private System.Windows.Forms.RadioButton randElementsEach;
        private System.Windows.Forms.CheckBox randElements;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox randEnemies;
        private System.Windows.Forms.Button loadROM;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Button randomize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox overwriteROM;
        private System.Windows.Forms.CheckBox includeMinorEnemies;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox randBossAbilities;
        private System.Windows.Forms.CheckBox randMiniBossAbilities;
        private System.Windows.Forms.TextBox randSeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label romRegion;
    }
}

