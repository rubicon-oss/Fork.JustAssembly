namespace JustAssembly.Minidiffer
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose (bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose ();
      }
      base.Dispose (disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent ()
    {
      this.button1 = new System.Windows.Forms.Button();
      this.changesListBox = new System.Windows.Forms.ListBox();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.panel1 = new System.Windows.Forms.Panel();
      this.loadOrderedCheckbox = new System.Windows.Forms.CheckBox();
      this.includeOldSourceCheckbox = new System.Windows.Forms.CheckBox();
      this.includeNewSourceCheckbox = new System.Windows.Forms.CheckBox();
      this.markAllIgnoredButton = new System.Windows.Forms.Button();
      this.markAllDoneButton = new System.Windows.Forms.Button();
      this.undoneButton = new System.Windows.Forms.Button();
      this.doneButton = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.doneListBox = new System.Windows.Forms.ListBox();
      this.copyAsXmlButton = new System.Windows.Forms.Button();
      this.unignoreButton = new System.Windows.Forms.Button();
      this.ignoreButton = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.ignoredChangesListBox = new System.Windows.Forms.ListBox();
      this.label1 = new System.Windows.Forms.Label();
      this.markAllCompletedButton = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(417, 3);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(125, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "Open change set";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // changesListBox
      // 
      this.changesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.changesListBox.FormattingEnabled = true;
      this.changesListBox.Location = new System.Drawing.Point(0, 32);
      this.changesListBox.Name = "changesListBox";
      this.changesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.changesListBox.Size = new System.Drawing.Size(673, 199);
      this.changesListBox.TabIndex = 1;
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      this.openFileDialog1.Filter = "Xml change set (*.xml)|*.xml";
      // 
      // panel1
      // 
      this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panel1.Controls.Add(this.markAllCompletedButton);
      this.panel1.Controls.Add(this.loadOrderedCheckbox);
      this.panel1.Controls.Add(this.includeOldSourceCheckbox);
      this.panel1.Controls.Add(this.includeNewSourceCheckbox);
      this.panel1.Controls.Add(this.markAllIgnoredButton);
      this.panel1.Controls.Add(this.markAllDoneButton);
      this.panel1.Controls.Add(this.undoneButton);
      this.panel1.Controls.Add(this.doneButton);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.doneListBox);
      this.panel1.Controls.Add(this.copyAsXmlButton);
      this.panel1.Controls.Add(this.unignoreButton);
      this.panel1.Controls.Add(this.ignoreButton);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.button1);
      this.panel1.Controls.Add(this.ignoredChangesListBox);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.changesListBox);
      this.panel1.Location = new System.Drawing.Point(12, 12);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(673, 702);
      this.panel1.TabIndex = 2;
      // 
      // loadOrderedCheckbox
      // 
      this.loadOrderedCheckbox.AutoSize = true;
      this.loadOrderedCheckbox.Checked = true;
      this.loadOrderedCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.loadOrderedCheckbox.Location = new System.Drawing.Point(278, 7);
      this.loadOrderedCheckbox.Name = "loadOrderedCheckbox";
      this.loadOrderedCheckbox.Size = new System.Drawing.Size(133, 17);
      this.loadOrderedCheckbox.TabIndex = 15;
      this.loadOrderedCheckbox.Text = "Load changes ordered";
      this.loadOrderedCheckbox.UseVisualStyleBackColor = true;
      // 
      // includeOldSourceCheckbox
      // 
      this.includeOldSourceCheckbox.AutoSize = true;
      this.includeOldSourceCheckbox.Checked = true;
      this.includeOldSourceCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.includeOldSourceCheckbox.Location = new System.Drawing.Point(173, 279);
      this.includeOldSourceCheckbox.Name = "includeOldSourceCheckbox";
      this.includeOldSourceCheckbox.Size = new System.Drawing.Size(113, 17);
      this.includeOldSourceCheckbox.TabIndex = 15;
      this.includeOldSourceCheckbox.Text = "Include old source";
      this.includeOldSourceCheckbox.UseVisualStyleBackColor = true;
      // 
      // includeNewSourceCheckbox
      // 
      this.includeNewSourceCheckbox.AutoSize = true;
      this.includeNewSourceCheckbox.Checked = true;
      this.includeNewSourceCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.includeNewSourceCheckbox.Location = new System.Drawing.Point(292, 279);
      this.includeNewSourceCheckbox.Name = "includeNewSourceCheckbox";
      this.includeNewSourceCheckbox.Size = new System.Drawing.Size(119, 17);
      this.includeNewSourceCheckbox.TabIndex = 14;
      this.includeNewSourceCheckbox.Text = "Include new source";
      this.includeNewSourceCheckbox.UseVisualStyleBackColor = true;
      // 
      // markAllIgnoredButton
      // 
      this.markAllIgnoredButton.Location = new System.Drawing.Point(548, 275);
      this.markAllIgnoredButton.Name = "markAllIgnoredButton";
      this.markAllIgnoredButton.Size = new System.Drawing.Size(125, 23);
      this.markAllIgnoredButton.TabIndex = 13;
      this.markAllIgnoredButton.Text = "Mark all";
      this.markAllIgnoredButton.UseVisualStyleBackColor = true;
      this.markAllIgnoredButton.Click += new System.EventHandler(this.markAllIgnoredButton_Click);
      // 
      // markAllDoneButton
      // 
      this.markAllDoneButton.Location = new System.Drawing.Point(548, 513);
      this.markAllDoneButton.Name = "markAllDoneButton";
      this.markAllDoneButton.Size = new System.Drawing.Size(125, 23);
      this.markAllDoneButton.TabIndex = 12;
      this.markAllDoneButton.Text = "Mark all";
      this.markAllDoneButton.UseVisualStyleBackColor = true;
      this.markAllDoneButton.Click += new System.EventHandler(this.markAllDoneButton_Click);
      // 
      // undoneButton
      // 
      this.undoneButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.undoneButton.Location = new System.Drawing.Point(339, 481);
      this.undoneButton.Name = "undoneButton";
      this.undoneButton.Size = new System.Drawing.Size(75, 23);
      this.undoneButton.TabIndex = 11;
      this.undoneButton.Text = "undone /\\";
      this.undoneButton.UseVisualStyleBackColor = true;
      this.undoneButton.Click += new System.EventHandler(this.undoneButton_Click);
      // 
      // doneButton
      // 
      this.doneButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.doneButton.Location = new System.Drawing.Point(258, 481);
      this.doneButton.Name = "doneButton";
      this.doneButton.Size = new System.Drawing.Size(75, 23);
      this.doneButton.TabIndex = 10;
      this.doneButton.Text = "\\/ done";
      this.doneButton.UseVisualStyleBackColor = true;
      this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 518);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(101, 13);
      this.label3.TabIndex = 9;
      this.label3.Text = "Completed changes";
      // 
      // doneListBox
      // 
      this.doneListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.doneListBox.FormattingEnabled = true;
      this.doneListBox.Location = new System.Drawing.Point(0, 542);
      this.doneListBox.Name = "doneListBox";
      this.doneListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.doneListBox.Size = new System.Drawing.Size(673, 160);
      this.doneListBox.TabIndex = 8;
      // 
      // copyAsXmlButton
      // 
      this.copyAsXmlButton.Location = new System.Drawing.Point(417, 275);
      this.copyAsXmlButton.Name = "copyAsXmlButton";
      this.copyAsXmlButton.Size = new System.Drawing.Size(125, 23);
      this.copyAsXmlButton.TabIndex = 7;
      this.copyAsXmlButton.Text = "Copy as XML";
      this.copyAsXmlButton.UseVisualStyleBackColor = true;
      this.copyAsXmlButton.Click += new System.EventHandler(this.copyAsXmlButton_Click);
      // 
      // unignoreButton
      // 
      this.unignoreButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.unignoreButton.Location = new System.Drawing.Point(339, 243);
      this.unignoreButton.Name = "unignoreButton";
      this.unignoreButton.Size = new System.Drawing.Size(75, 23);
      this.unignoreButton.TabIndex = 6;
      this.unignoreButton.Text = "unignore /\\";
      this.unignoreButton.UseVisualStyleBackColor = true;
      this.unignoreButton.Click += new System.EventHandler(this.unignoreButton_Click);
      // 
      // ignoreButton
      // 
      this.ignoreButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.ignoreButton.Location = new System.Drawing.Point(258, 243);
      this.ignoreButton.Name = "ignoreButton";
      this.ignoreButton.Size = new System.Drawing.Size(75, 23);
      this.ignoreButton.TabIndex = 5;
      this.ignoreButton.Text = "\\/ ignore";
      this.ignoreButton.UseVisualStyleBackColor = true;
      this.ignoreButton.Click += new System.EventHandler(this.ignoreButton_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 280);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(87, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Ignored changes";
      // 
      // ignoredChangesListBox
      // 
      this.ignoredChangesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ignoredChangesListBox.FormattingEnabled = true;
      this.ignoredChangesListBox.Location = new System.Drawing.Point(0, 304);
      this.ignoredChangesListBox.Name = "ignoredChangesListBox";
      this.ignoredChangesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.ignoredChangesListBox.Size = new System.Drawing.Size(673, 160);
      this.ignoredChangesListBox.TabIndex = 3;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(90, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Pending changes";
      // 
      // markAllCompletedButton
      // 
      this.markAllCompletedButton.Location = new System.Drawing.Point(548, 3);
      this.markAllCompletedButton.Name = "markAllCompletedButton";
      this.markAllCompletedButton.Size = new System.Drawing.Size(125, 23);
      this.markAllCompletedButton.TabIndex = 16;
      this.markAllCompletedButton.Text = "Mark all";
      this.markAllCompletedButton.UseVisualStyleBackColor = true;
      this.markAllCompletedButton.Click += new System.EventHandler(this.markAllCompletedButton_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(697, 726);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.Name = "Form1";
      this.Text = "Form1";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.ListBox changesListBox;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button unignoreButton;
    private System.Windows.Forms.Button ignoreButton;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ListBox ignoredChangesListBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button copyAsXmlButton;
    private System.Windows.Forms.Button markAllIgnoredButton;
    private System.Windows.Forms.Button markAllDoneButton;
    private System.Windows.Forms.Button undoneButton;
    private System.Windows.Forms.Button doneButton;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ListBox doneListBox;
    private System.Windows.Forms.CheckBox includeOldSourceCheckbox;
    private System.Windows.Forms.CheckBox includeNewSourceCheckbox;
    private System.Windows.Forms.CheckBox loadOrderedCheckbox;
    private System.Windows.Forms.Button markAllCompletedButton;
  }
}

