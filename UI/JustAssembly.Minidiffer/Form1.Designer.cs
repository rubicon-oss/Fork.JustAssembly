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
      this.PendingChangesListBox = new System.Windows.Forms.ListBox();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.panel1 = new System.Windows.Forms.Panel();
      this.PendCompletedChangeButton = new System.Windows.Forms.Button();
      this.CompletePendingChangeButton = new System.Windows.Forms.Button();
      this.MarkAllPendingChangesButton = new System.Windows.Forms.Button();
      this.loadOrderedCheckbox = new System.Windows.Forms.CheckBox();
      this.includeOldSourceCheckbox = new System.Windows.Forms.CheckBox();
      this.includeNewSourceCheckbox = new System.Windows.Forms.CheckBox();
      this.MarkAllIgnoredChangesButton = new System.Windows.Forms.Button();
      this.MarkAllCompletedChangesButton = new System.Windows.Forms.Button();
      this.IgnoreCompletedChangeButton = new System.Windows.Forms.Button();
      this.CompleteIgnoredChangeButton = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.CompletedListBox = new System.Windows.Forms.ListBox();
      this.copyAsXmlButton = new System.Windows.Forms.Button();
      this.PendIgnoredChangeButton = new System.Windows.Forms.Button();
      this.IgnorePendingChangesButton = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.IgnoredChangesListBox = new System.Windows.Forms.ListBox();
      this.label1 = new System.Windows.Forms.Label();
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
      this.button1.Click += new System.EventHandler(this.OpenChangeSetFromDisk);
      // 
      // PendingChangesListBox
      // 
      this.PendingChangesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.PendingChangesListBox.FormattingEnabled = true;
      this.PendingChangesListBox.Location = new System.Drawing.Point(0, 32);
      this.PendingChangesListBox.Name = "PendingChangesListBox";
      this.PendingChangesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.PendingChangesListBox.Size = new System.Drawing.Size(673, 199);
      this.PendingChangesListBox.TabIndex = 1;
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
      this.panel1.Controls.Add(this.PendCompletedChangeButton);
      this.panel1.Controls.Add(this.CompletePendingChangeButton);
      this.panel1.Controls.Add(this.MarkAllPendingChangesButton);
      this.panel1.Controls.Add(this.loadOrderedCheckbox);
      this.panel1.Controls.Add(this.includeOldSourceCheckbox);
      this.panel1.Controls.Add(this.includeNewSourceCheckbox);
      this.panel1.Controls.Add(this.MarkAllIgnoredChangesButton);
      this.panel1.Controls.Add(this.MarkAllCompletedChangesButton);
      this.panel1.Controls.Add(this.IgnoreCompletedChangeButton);
      this.panel1.Controls.Add(this.CompleteIgnoredChangeButton);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.CompletedListBox);
      this.panel1.Controls.Add(this.copyAsXmlButton);
      this.panel1.Controls.Add(this.PendIgnoredChangeButton);
      this.panel1.Controls.Add(this.IgnorePendingChangesButton);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.button1);
      this.panel1.Controls.Add(this.IgnoredChangesListBox);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.PendingChangesListBox);
      this.panel1.Location = new System.Drawing.Point(12, 12);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(673, 702);
      this.panel1.TabIndex = 2;
      // 
      // PendCompletedChangeButton
      // 
      this.PendCompletedChangeButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.PendCompletedChangeButton.Location = new System.Drawing.Point(380, 481);
      this.PendCompletedChangeButton.Name = "PendCompletedChangeButton";
      this.PendCompletedChangeButton.Size = new System.Drawing.Size(75, 23);
      this.PendCompletedChangeButton.TabIndex = 18;
      this.PendCompletedChangeButton.Text = "pending /\\";
      this.PendCompletedChangeButton.UseVisualStyleBackColor = true;
      this.PendCompletedChangeButton.Click += new System.EventHandler(this.PendCompletedChangeButton_Click);
      // 
      // CompletePendingChangeButton
      // 
      this.CompletePendingChangeButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.CompletePendingChangeButton.Location = new System.Drawing.Point(218, 243);
      this.CompletePendingChangeButton.Name = "CompletePendingChangeButton";
      this.CompletePendingChangeButton.Size = new System.Drawing.Size(75, 23);
      this.CompletePendingChangeButton.TabIndex = 17;
      this.CompletePendingChangeButton.Text = "\\/ complete";
      this.CompletePendingChangeButton.UseVisualStyleBackColor = true;
      this.CompletePendingChangeButton.Click += new System.EventHandler(this.CompletePendingChangeButton_Click);
      // 
      // MarkAllPendingChangesButton
      // 
      this.MarkAllPendingChangesButton.Location = new System.Drawing.Point(548, 3);
      this.MarkAllPendingChangesButton.Name = "MarkAllPendingChangesButton";
      this.MarkAllPendingChangesButton.Size = new System.Drawing.Size(125, 23);
      this.MarkAllPendingChangesButton.TabIndex = 16;
      this.MarkAllPendingChangesButton.Text = "Mark all";
      this.MarkAllPendingChangesButton.UseVisualStyleBackColor = true;
      this.MarkAllPendingChangesButton.Click += new System.EventHandler(this.MarkAllPendingChangesButton_Click);
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
      // MarkAllIgnoredChangesButton
      // 
      this.MarkAllIgnoredChangesButton.Location = new System.Drawing.Point(548, 275);
      this.MarkAllIgnoredChangesButton.Name = "MarkAllIgnoredChangesButton";
      this.MarkAllIgnoredChangesButton.Size = new System.Drawing.Size(125, 23);
      this.MarkAllIgnoredChangesButton.TabIndex = 13;
      this.MarkAllIgnoredChangesButton.Text = "Mark all";
      this.MarkAllIgnoredChangesButton.UseVisualStyleBackColor = true;
      this.MarkAllIgnoredChangesButton.Click += new System.EventHandler(this.MarkAllIgnoredChangesButton_Click);
      // 
      // MarkAllCompletedChangesButton
      // 
      this.MarkAllCompletedChangesButton.Location = new System.Drawing.Point(548, 513);
      this.MarkAllCompletedChangesButton.Name = "MarkAllCompletedChangesButton";
      this.MarkAllCompletedChangesButton.Size = new System.Drawing.Size(125, 23);
      this.MarkAllCompletedChangesButton.TabIndex = 12;
      this.MarkAllCompletedChangesButton.Text = "Mark all";
      this.MarkAllCompletedChangesButton.UseVisualStyleBackColor = true;
      this.MarkAllCompletedChangesButton.Click += new System.EventHandler(this.MarkAllCompletedChangesButton_Click);
      // 
      // IgnoreCompletedChangeButton
      // 
      this.IgnoreCompletedChangeButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.IgnoreCompletedChangeButton.Location = new System.Drawing.Point(299, 481);
      this.IgnoreCompletedChangeButton.Name = "IgnoreCompletedChangeButton";
      this.IgnoreCompletedChangeButton.Size = new System.Drawing.Size(75, 23);
      this.IgnoreCompletedChangeButton.TabIndex = 11;
      this.IgnoreCompletedChangeButton.Text = "ignore /\\";
      this.IgnoreCompletedChangeButton.UseVisualStyleBackColor = true;
      this.IgnoreCompletedChangeButton.Click += new System.EventHandler(this.IgnoreCompletedChangeButton_Click);
      // 
      // CompleteIgnoredChangeButton
      // 
      this.CompleteIgnoredChangeButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.CompleteIgnoredChangeButton.Location = new System.Drawing.Point(218, 481);
      this.CompleteIgnoredChangeButton.Name = "CompleteIgnoredChangeButton";
      this.CompleteIgnoredChangeButton.Size = new System.Drawing.Size(75, 23);
      this.CompleteIgnoredChangeButton.TabIndex = 10;
      this.CompleteIgnoredChangeButton.Text = "\\/ done";
      this.CompleteIgnoredChangeButton.UseVisualStyleBackColor = true;
      this.CompleteIgnoredChangeButton.Click += new System.EventHandler(this.CompleteIgnoredChangeButton_Click);
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
      // CompletedListBox
      // 
      this.CompletedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.CompletedListBox.FormattingEnabled = true;
      this.CompletedListBox.Location = new System.Drawing.Point(0, 542);
      this.CompletedListBox.Name = "CompletedListBox";
      this.CompletedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.CompletedListBox.Size = new System.Drawing.Size(673, 160);
      this.CompletedListBox.TabIndex = 8;
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
      // PendIgnoredChangeButton
      // 
      this.PendIgnoredChangeButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.PendIgnoredChangeButton.Location = new System.Drawing.Point(380, 243);
      this.PendIgnoredChangeButton.Name = "PendIgnoredChangeButton";
      this.PendIgnoredChangeButton.Size = new System.Drawing.Size(75, 23);
      this.PendIgnoredChangeButton.TabIndex = 6;
      this.PendIgnoredChangeButton.Text = "unignore /\\";
      this.PendIgnoredChangeButton.UseVisualStyleBackColor = true;
      this.PendIgnoredChangeButton.Click += new System.EventHandler(this.PendIgnoredChangeButton_Click);
      // 
      // IgnorePendingChangesButton
      // 
      this.IgnorePendingChangesButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.IgnorePendingChangesButton.Location = new System.Drawing.Point(299, 243);
      this.IgnorePendingChangesButton.Name = "IgnorePendingChangesButton";
      this.IgnorePendingChangesButton.Size = new System.Drawing.Size(75, 23);
      this.IgnorePendingChangesButton.TabIndex = 5;
      this.IgnorePendingChangesButton.Text = "\\/ ignore";
      this.IgnorePendingChangesButton.UseVisualStyleBackColor = true;
      this.IgnorePendingChangesButton.Click += new System.EventHandler(this.IgnorePendingChangesButton_Click);
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
      // IgnoredChangesListBox
      // 
      this.IgnoredChangesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.IgnoredChangesListBox.FormattingEnabled = true;
      this.IgnoredChangesListBox.Location = new System.Drawing.Point(0, 304);
      this.IgnoredChangesListBox.Name = "IgnoredChangesListBox";
      this.IgnoredChangesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.IgnoredChangesListBox.Size = new System.Drawing.Size(673, 160);
      this.IgnoredChangesListBox.TabIndex = 3;
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
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(697, 726);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.Name = "Form1";
      this.ShowIcon = false;
      this.Text = "Minidiffer";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.ListBox PendingChangesListBox;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button PendIgnoredChangeButton;
    private System.Windows.Forms.Button IgnorePendingChangesButton;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ListBox IgnoredChangesListBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button copyAsXmlButton;
    private System.Windows.Forms.Button MarkAllIgnoredChangesButton;
    private System.Windows.Forms.Button MarkAllCompletedChangesButton;
    private System.Windows.Forms.Button IgnoreCompletedChangeButton;
    private System.Windows.Forms.Button CompleteIgnoredChangeButton;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ListBox CompletedListBox;
    private System.Windows.Forms.CheckBox includeOldSourceCheckbox;
    private System.Windows.Forms.CheckBox includeNewSourceCheckbox;
    private System.Windows.Forms.CheckBox loadOrderedCheckbox;
    private System.Windows.Forms.Button MarkAllPendingChangesButton;
    private System.Windows.Forms.Button PendCompletedChangeButton;
    private System.Windows.Forms.Button CompletePendingChangeButton;
  }
}

