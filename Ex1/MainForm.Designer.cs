namespace Ex1
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            this.btnCreateFootings = new System.Windows.Forms.Button();
            this.lbResult = new System.Windows.Forms.Label();
            this.txtPadFootingSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateRebars = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ColumnsProfileTextBox = new System.Windows.Forms.TextBox();
            this.profileCatalog1 = new Tekla.Structures.Dialog.UIControls.ProfileCatalog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BendingRadiusTextBox = new System.Windows.Forms.TextBox();
            this.GradeTextBox = new System.Windows.Forms.TextBox();
            this.SizeTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.reinforcementCatalog1 = new Tekla.Structures.Dialog.UIControls.ReinforcementCatalog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbMaterialList = new System.Windows.Forms.ComboBox();
            this.btnSelectMaterial = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnInsertDrawing = new System.Windows.Forms.Button();
            this.txtDrawingText = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnSetActiveDrawing = new System.Windows.Forms.Button();
            this.lswDrawings = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateFootings
            // 
            this.structuresExtender.SetAttributeName(this.btnCreateFootings, null);
            this.structuresExtender.SetAttributeTypeName(this.btnCreateFootings, null);
            this.structuresExtender.SetBindPropertyName(this.btnCreateFootings, null);
            this.btnCreateFootings.Location = new System.Drawing.Point(596, 310);
            this.btnCreateFootings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCreateFootings.Name = "btnCreateFootings";
            this.btnCreateFootings.Size = new System.Drawing.Size(184, 28);
            this.btnCreateFootings.TabIndex = 0;
            this.btnCreateFootings.Text = "Create Pad Footings";
            this.btnCreateFootings.UseVisualStyleBackColor = true;
            this.btnCreateFootings.Click += new System.EventHandler(this.btnCreateFootings_Click);
            // 
            // lbResult
            // 
            this.structuresExtender.SetAttributeName(this.lbResult, null);
            this.structuresExtender.SetAttributeTypeName(this.lbResult, null);
            this.lbResult.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.lbResult, null);
            this.lbResult.Location = new System.Drawing.Point(12, 351);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(0, 17);
            this.lbResult.TabIndex = 3;
            // 
            // txtPadFootingSize
            // 
            this.structuresExtender.SetAttributeName(this.txtPadFootingSize, "FootinSize");
            this.structuresExtender.SetAttributeTypeName(this.txtPadFootingSize, "String");
            this.structuresExtender.SetBindPropertyName(this.txtPadFootingSize, "AccessibleName");
            this.txtPadFootingSize.Location = new System.Drawing.Point(697, 278);
            this.txtPadFootingSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPadFootingSize.Name = "txtPadFootingSize";
            this.txtPadFootingSize.Size = new System.Drawing.Size(81, 22);
            this.txtPadFootingSize.TabIndex = 4;
            this.txtPadFootingSize.Text = "1500";
            this.txtPadFootingSize.Leave += new System.EventHandler(this.txtPadFootingSize_Leave);
            // 
            // label2
            // 
            this.structuresExtender.SetAttributeName(this.label2, null);
            this.structuresExtender.SetAttributeTypeName(this.label2, null);
            this.label2.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label2, null);
            this.label2.Location = new System.Drawing.Point(593, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Footing Size : ";
            // 
            // btnCreateRebars
            // 
            this.structuresExtender.SetAttributeName(this.btnCreateRebars, null);
            this.structuresExtender.SetAttributeTypeName(this.btnCreateRebars, null);
            this.structuresExtender.SetBindPropertyName(this.btnCreateRebars, null);
            this.btnCreateRebars.Location = new System.Drawing.Point(596, 345);
            this.btnCreateRebars.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreateRebars.Name = "btnCreateRebars";
            this.btnCreateRebars.Size = new System.Drawing.Size(184, 28);
            this.btnCreateRebars.TabIndex = 6;
            this.btnCreateRebars.Text = "Create Rebars";
            this.btnCreateRebars.UseVisualStyleBackColor = true;
            this.btnCreateRebars.Click += new System.EventHandler(this.btnCreateRebars_Click);
            // 
            // groupBox1
            // 
            this.structuresExtender.SetAttributeName(this.groupBox1, null);
            this.structuresExtender.SetAttributeTypeName(this.groupBox1, null);
            this.structuresExtender.SetBindPropertyName(this.groupBox1, null);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ColumnsProfileTextBox);
            this.groupBox1.Controls.Add(this.profileCatalog1);
            this.groupBox1.Location = new System.Drawing.Point(357, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(423, 78);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Columns";
            // 
            // label3
            // 
            this.structuresExtender.SetAttributeName(this.label3, null);
            this.structuresExtender.SetAttributeTypeName(this.label3, null);
            this.label3.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label3, null);
            this.label3.Location = new System.Drawing.Point(17, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Profile : ";
            // 
            // ColumnsProfileTextBox
            // 
            this.structuresExtender.SetAttributeName(this.ColumnsProfileTextBox, "Profile");
            this.structuresExtender.SetAttributeTypeName(this.ColumnsProfileTextBox, "String");
            this.structuresExtender.SetBindPropertyName(this.ColumnsProfileTextBox, "AccessibleName");
            this.ColumnsProfileTextBox.Location = new System.Drawing.Point(77, 37);
            this.ColumnsProfileTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ColumnsProfileTextBox.Name = "ColumnsProfileTextBox";
            this.ColumnsProfileTextBox.Size = new System.Drawing.Size(208, 22);
            this.ColumnsProfileTextBox.TabIndex = 10;
            // 
            // profileCatalog1
            // 
            this.structuresExtender.SetAttributeName(this.profileCatalog1, null);
            this.structuresExtender.SetAttributeTypeName(this.profileCatalog1, null);
            this.profileCatalog1.BackColor = System.Drawing.Color.Transparent;
            this.structuresExtender.SetBindPropertyName(this.profileCatalog1, null);
            this.profileCatalog1.Location = new System.Drawing.Point(292, 31);
            this.profileCatalog1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.profileCatalog1.Name = "profileCatalog1";
            this.profileCatalog1.SelectedProfile = "";
            this.profileCatalog1.Size = new System.Drawing.Size(117, 33);
            this.profileCatalog1.TabIndex = 9;
            this.profileCatalog1.SelectClicked += new System.EventHandler(this.profileCatalog1_SelectClicked);
            this.profileCatalog1.SelectionDone += new System.EventHandler(this.profileCatalog1_SelectionDone);
            // 
            // groupBox2
            // 
            this.structuresExtender.SetAttributeName(this.groupBox2, null);
            this.structuresExtender.SetAttributeTypeName(this.groupBox2, null);
            this.structuresExtender.SetBindPropertyName(this.groupBox2, null);
            this.groupBox2.Controls.Add(this.BendingRadiusTextBox);
            this.groupBox2.Controls.Add(this.GradeTextBox);
            this.groupBox2.Controls.Add(this.SizeTextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.reinforcementCatalog1);
            this.groupBox2.Location = new System.Drawing.Point(357, 91);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(423, 119);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rebars";
            // 
            // BendingRadiusTextBox
            // 
            this.structuresExtender.SetAttributeName(this.BendingRadiusTextBox, null);
            this.structuresExtender.SetAttributeTypeName(this.BendingRadiusTextBox, null);
            this.structuresExtender.SetBindPropertyName(this.BendingRadiusTextBox, null);
            this.BendingRadiusTextBox.Location = new System.Drawing.Point(77, 78);
            this.BendingRadiusTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BendingRadiusTextBox.Name = "BendingRadiusTextBox";
            this.BendingRadiusTextBox.Size = new System.Drawing.Size(204, 22);
            this.BendingRadiusTextBox.TabIndex = 6;
            // 
            // GradeTextBox
            // 
            this.structuresExtender.SetAttributeName(this.GradeTextBox, null);
            this.structuresExtender.SetAttributeTypeName(this.GradeTextBox, null);
            this.structuresExtender.SetBindPropertyName(this.GradeTextBox, null);
            this.GradeTextBox.Location = new System.Drawing.Point(77, 49);
            this.GradeTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GradeTextBox.Name = "GradeTextBox";
            this.GradeTextBox.Size = new System.Drawing.Size(204, 22);
            this.GradeTextBox.TabIndex = 5;
            // 
            // SizeTextBox
            // 
            this.structuresExtender.SetAttributeName(this.SizeTextBox, null);
            this.structuresExtender.SetAttributeTypeName(this.SizeTextBox, null);
            this.structuresExtender.SetBindPropertyName(this.SizeTextBox, null);
            this.SizeTextBox.Location = new System.Drawing.Point(77, 20);
            this.SizeTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SizeTextBox.Name = "SizeTextBox";
            this.SizeTextBox.Size = new System.Drawing.Size(204, 22);
            this.SizeTextBox.TabIndex = 4;
            // 
            // label6
            // 
            this.structuresExtender.SetAttributeName(this.label6, null);
            this.structuresExtender.SetAttributeTypeName(this.label6, null);
            this.label6.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label6, null);
            this.label6.Location = new System.Drawing.Point(7, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "Bending : ";
            // 
            // label5
            // 
            this.structuresExtender.SetAttributeName(this.label5, null);
            this.structuresExtender.SetAttributeTypeName(this.label5, null);
            this.label5.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label5, null);
            this.label5.Location = new System.Drawing.Point(7, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Grade : ";
            // 
            // label4
            // 
            this.structuresExtender.SetAttributeName(this.label4, null);
            this.structuresExtender.SetAttributeTypeName(this.label4, null);
            this.label4.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label4, null);
            this.label4.Location = new System.Drawing.Point(7, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Size : ";
            // 
            // reinforcementCatalog1
            // 
            this.structuresExtender.SetAttributeName(this.reinforcementCatalog1, null);
            this.structuresExtender.SetAttributeTypeName(this.reinforcementCatalog1, null);
            this.reinforcementCatalog1.BackColor = System.Drawing.Color.Transparent;
            this.structuresExtender.SetBindPropertyName(this.reinforcementCatalog1, null);
            this.reinforcementCatalog1.Location = new System.Drawing.Point(292, 22);
            this.reinforcementCatalog1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.reinforcementCatalog1.Name = "reinforcementCatalog1";
            this.reinforcementCatalog1.SelectedRebarBendingRadius = 0D;
            this.reinforcementCatalog1.SelectedRebarGrade = "";
            this.reinforcementCatalog1.SelectedRebarSize = "";
            this.reinforcementCatalog1.Size = new System.Drawing.Size(117, 33);
            this.reinforcementCatalog1.TabIndex = 0;
            this.reinforcementCatalog1.SelectionDone += new System.EventHandler(this.reinforcementCatalog1_SelectionDone);
            // 
            // groupBox3
            // 
            this.structuresExtender.SetAttributeName(this.groupBox3, null);
            this.structuresExtender.SetAttributeTypeName(this.groupBox3, null);
            this.structuresExtender.SetBindPropertyName(this.groupBox3, null);
            this.groupBox3.Controls.Add(this.cbMaterialList);
            this.groupBox3.Controls.Add(this.btnSelectMaterial);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(15, 274);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(561, 60);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Materials";
            // 
            // cbMaterialList
            // 
            this.structuresExtender.SetAttributeName(this.cbMaterialList, null);
            this.structuresExtender.SetAttributeTypeName(this.cbMaterialList, null);
            this.structuresExtender.SetBindPropertyName(this.cbMaterialList, null);
            this.cbMaterialList.DisplayMember = "MaterialName";
            this.cbMaterialList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMaterialList.FormattingEnabled = true;
            this.cbMaterialList.Location = new System.Drawing.Point(73, 18);
            this.cbMaterialList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbMaterialList.Name = "cbMaterialList";
            this.cbMaterialList.Size = new System.Drawing.Size(292, 24);
            this.cbMaterialList.TabIndex = 3;
            this.cbMaterialList.SelectedValueChanged += new System.EventHandler(this.cbMaterialList_SelectedValueChanged);
            // 
            // btnSelectMaterial
            // 
            this.structuresExtender.SetAttributeName(this.btnSelectMaterial, null);
            this.structuresExtender.SetAttributeTypeName(this.btnSelectMaterial, null);
            this.structuresExtender.SetBindPropertyName(this.btnSelectMaterial, null);
            this.btnSelectMaterial.Location = new System.Drawing.Point(371, 17);
            this.btnSelectMaterial.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectMaterial.Name = "btnSelectMaterial";
            this.btnSelectMaterial.Size = new System.Drawing.Size(184, 28);
            this.btnSelectMaterial.TabIndex = 2;
            this.btnSelectMaterial.Text = "Select Material";
            this.btnSelectMaterial.UseVisualStyleBackColor = true;
            this.btnSelectMaterial.Click += new System.EventHandler(this.btnSelectMaterial_Click);
            // 
            // label7
            // 
            this.structuresExtender.SetAttributeName(this.label7, null);
            this.structuresExtender.SetAttributeTypeName(this.label7, null);
            this.label7.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label7, null);
            this.label7.Location = new System.Drawing.Point(7, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Material : ";
            // 
            // groupBox4
            // 
            this.structuresExtender.SetAttributeName(this.groupBox4, null);
            this.structuresExtender.SetAttributeTypeName(this.groupBox4, null);
            this.structuresExtender.SetBindPropertyName(this.groupBox4, null);
            this.groupBox4.Controls.Add(this.btnInsertDrawing);
            this.groupBox4.Controls.Add(this.txtDrawingText);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(15, 215);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(764, 55);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Drawings";
            // 
            // btnInsertDrawing
            // 
            this.structuresExtender.SetAttributeName(this.btnInsertDrawing, null);
            this.structuresExtender.SetAttributeTypeName(this.btnInsertDrawing, null);
            this.structuresExtender.SetBindPropertyName(this.btnInsertDrawing, null);
            this.btnInsertDrawing.Location = new System.Drawing.Point(567, 18);
            this.btnInsertDrawing.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnInsertDrawing.Name = "btnInsertDrawing";
            this.btnInsertDrawing.Size = new System.Drawing.Size(184, 28);
            this.btnInsertDrawing.TabIndex = 2;
            this.btnInsertDrawing.Text = "Edit Drawing";
            this.btnInsertDrawing.UseVisualStyleBackColor = true;
            this.btnInsertDrawing.Click += new System.EventHandler(this.btnInsertDrawing_Click);
            // 
            // txtDrawingText
            // 
            this.structuresExtender.SetAttributeName(this.txtDrawingText, null);
            this.structuresExtender.SetAttributeTypeName(this.txtDrawingText, null);
            this.structuresExtender.SetBindPropertyName(this.txtDrawingText, null);
            this.txtDrawingText.Location = new System.Drawing.Point(85, 20);
            this.txtDrawingText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDrawingText.Name = "txtDrawingText";
            this.txtDrawingText.Size = new System.Drawing.Size(476, 22);
            this.txtDrawingText.TabIndex = 1;
            // 
            // label8
            // 
            this.structuresExtender.SetAttributeName(this.label8, null);
            this.structuresExtender.SetAttributeTypeName(this.label8, null);
            this.label8.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label8, null);
            this.label8.Location = new System.Drawing.Point(7, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "View Title : ";
            // 
            // groupBox5
            // 
            this.structuresExtender.SetAttributeName(this.groupBox5, null);
            this.structuresExtender.SetAttributeTypeName(this.groupBox5, null);
            this.structuresExtender.SetBindPropertyName(this.groupBox5, null);
            this.groupBox5.Controls.Add(this.btnSetActiveDrawing);
            this.groupBox5.Controls.Add(this.lswDrawings);
            this.groupBox5.Location = new System.Drawing.Point(15, 14);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Size = new System.Drawing.Size(336, 197);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Drawings";
            // 
            // btnSetActiveDrawing
            // 
            this.structuresExtender.SetAttributeName(this.btnSetActiveDrawing, null);
            this.structuresExtender.SetAttributeTypeName(this.btnSetActiveDrawing, null);
            this.structuresExtender.SetBindPropertyName(this.btnSetActiveDrawing, null);
            this.btnSetActiveDrawing.Location = new System.Drawing.Point(141, 159);
            this.btnSetActiveDrawing.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetActiveDrawing.Name = "btnSetActiveDrawing";
            this.btnSetActiveDrawing.Size = new System.Drawing.Size(184, 28);
            this.btnSetActiveDrawing.TabIndex = 1;
            this.btnSetActiveDrawing.Text = "Activate Drawing";
            this.btnSetActiveDrawing.UseVisualStyleBackColor = true;
            this.btnSetActiveDrawing.Click += new System.EventHandler(this.btnSetActiveDrawing_Click);
            // 
            // lswDrawings
            // 
            this.structuresExtender.SetAttributeName(this.lswDrawings, null);
            this.structuresExtender.SetAttributeTypeName(this.lswDrawings, null);
            this.structuresExtender.SetBindPropertyName(this.lswDrawings, null);
            this.lswDrawings.HideSelection = false;
            this.lswDrawings.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lswDrawings.Location = new System.Drawing.Point(17, 21);
            this.lswDrawings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lswDrawings.Name = "lswDrawings";
            this.lswDrawings.Size = new System.Drawing.Size(308, 132);
            this.lswDrawings.TabIndex = 0;
            this.lswDrawings.UseCompatibleStateImageBehavior = false;
            this.lswDrawings.View = System.Windows.Forms.View.List;
            this.lswDrawings.SelectedIndexChanged += new System.EventHandler(this.lswDrawings_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.structuresExtender.SetAttributeName(this, null);
            this.structuresExtender.SetAttributeTypeName(this, null);
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.structuresExtender.SetBindPropertyName(this, null);
            this.ClientSize = new System.Drawing.Size(791, 383);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCreateRebars);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPadFootingSize);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.btnCreateFootings);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateFootings;
        private System.Windows.Forms.Label lbResult;
        private System.Windows.Forms.TextBox txtPadFootingSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateRebars;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ColumnsProfileTextBox;
        private Tekla.Structures.Dialog.UIControls.ProfileCatalog profileCatalog1;
        private Tekla.Structures.Dialog.UIControls.ReinforcementCatalog reinforcementCatalog1;
        private System.Windows.Forms.TextBox BendingRadiusTextBox;
        private System.Windows.Forms.TextBox GradeTextBox;
        private System.Windows.Forms.TextBox SizeTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSelectMaterial;
        private System.Windows.Forms.ComboBox cbMaterialList;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnInsertDrawing;
        private System.Windows.Forms.TextBox txtDrawingText;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListView lswDrawings;
        private System.Windows.Forms.Button btnSetActiveDrawing;
    }
}

