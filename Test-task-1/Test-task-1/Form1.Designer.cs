namespace Test_task_1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            generateFilesButton = new Button();
            progressTextBox = new TextBox();
            deleteFilesButton = new Button();
            progressMergedTextBox = new TextBox();
            mergeButton = new Button();
            searchTextBox = new TextBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            databaseTransferedInfoTextBox = new TextBox();
            databaseTransferButton = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // generateFilesButton
            // 
            generateFilesButton.Location = new Point(9, 26);
            generateFilesButton.Name = "generateFilesButton";
            generateFilesButton.Size = new Size(275, 46);
            generateFilesButton.TabIndex = 0;
            generateFilesButton.Text = "Generate files";
            generateFilesButton.UseVisualStyleBackColor = true;
            generateFilesButton.Click += generateFilesButton_Click;
            // 
            // progressTextBox
            // 
            progressTextBox.Location = new Point(9, 78);
            progressTextBox.Multiline = true;
            progressTextBox.Name = "progressTextBox";
            progressTextBox.ScrollBars = ScrollBars.Both;
            progressTextBox.Size = new Size(275, 221);
            progressTextBox.TabIndex = 1;
            // 
            // deleteFilesButton
            // 
            deleteFilesButton.Location = new Point(9, 305);
            deleteFilesButton.Name = "deleteFilesButton";
            deleteFilesButton.Size = new Size(275, 46);
            deleteFilesButton.TabIndex = 2;
            deleteFilesButton.Text = "Delete files";
            deleteFilesButton.UseVisualStyleBackColor = true;
            deleteFilesButton.Click += deleteFilesButton_Click;
            // 
            // progressMergedTextBox
            // 
            progressMergedTextBox.Location = new Point(290, 132);
            progressMergedTextBox.Multiline = true;
            progressMergedTextBox.Name = "progressMergedTextBox";
            progressMergedTextBox.ScrollBars = ScrollBars.Both;
            progressMergedTextBox.Size = new Size(283, 219);
            progressMergedTextBox.TabIndex = 4;
            // 
            // mergeButton
            // 
            mergeButton.Location = new Point(290, 78);
            mergeButton.Name = "mergeButton";
            mergeButton.Size = new Size(283, 46);
            mergeButton.TabIndex = 3;
            mergeButton.Text = "Merge files";
            mergeButton.UseVisualStyleBackColor = true;
            mergeButton.Click += mergeButton_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            searchTextBox.Location = new Point(290, 26);
            searchTextBox.Multiline = true;
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Enter symbols in lines to delete";
            searchTextBox.Size = new Size(283, 48);
            searchTextBox.TabIndex = 7;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(mergeButton);
            groupBox1.Controls.Add(deleteFilesButton);
            groupBox1.Controls.Add(searchTextBox);
            groupBox1.Controls.Add(progressTextBox);
            groupBox1.Controls.Add(progressMergedTextBox);
            groupBox1.Controls.Add(generateFilesButton);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(586, 364);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Create and merge files";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(databaseTransferedInfoTextBox);
            groupBox2.Controls.Add(databaseTransferButton);
            groupBox2.Location = new Point(604, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(348, 364);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Adding to database";
            // 
            // databaseTransferedInfoTextBox
            // 
            databaseTransferedInfoTextBox.Location = new Point(9, 78);
            databaseTransferedInfoTextBox.Multiline = true;
            databaseTransferedInfoTextBox.Name = "databaseTransferedInfoTextBox";
            databaseTransferedInfoTextBox.ScrollBars = ScrollBars.Both;
            databaseTransferedInfoTextBox.Size = new Size(333, 273);
            databaseTransferedInfoTextBox.TabIndex = 1;
            // 
            // databaseTransferButton
            // 
            databaseTransferButton.Location = new Point(9, 26);
            databaseTransferButton.Name = "databaseTransferButton";
            databaseTransferButton.Size = new Size(333, 46);
            databaseTransferButton.TabIndex = 0;
            databaseTransferButton.Text = "Transfer to database";
            databaseTransferButton.UseVisualStyleBackColor = true;
            databaseTransferButton.Click += databaseTransferButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(964, 515);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button generateFilesButton;
        private TextBox progressTextBox;
        private Button deleteFilesButton;
        private TextBox progressMergedTextBox;
        private Button mergeButton;
        private TextBox searchTextBox;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox databaseTransferedInfoTextBox;
        private Button databaseTransferButton;
    }
}