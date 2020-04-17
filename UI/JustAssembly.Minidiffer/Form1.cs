using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml.Serialization;
using JustAssembly.CommandLineTool.Utility;
using JustAssembly.CommandLineTool.XML;

namespace JustAssembly.Minidiffer
{
  public partial class Form1 : Form
  {
    public Form1 ()
    {
      InitializeComponent();
    }

    private void OpenChangeSetFromDisk (object sender, EventArgs args)
    {
      if (openFileDialog1.ShowDialog() != DialogResult.OK)
        return;

      try
      {
        using (var textReader = File.OpenText (openFileDialog1.FileName))
        {
          var xmlReader = new XmlSerializer (typeof (ChangeSet));
          var changeSet = (ChangeSet) xmlReader.Deserialize (textReader);

          PendingChangesListBox.Items.Clear();
          IgnoredChangesListBox.Items.Clear();
          CompletedListBox.Items.Clear();

          var changes = loadOrderedCheckbox.Checked
              ? changeSet.MemberChanges.Where (e => e is MemberChange).OrderBy (e => $"{e.Namespace}__{e.Name}")
              : changeSet.MemberChanges.Where (e => e is MemberChange);

          var index = 0;
          foreach (var change in changes)
          {
            var changeEntry = new ChangeEntry (index++, change);
            PendingChangesListBox.Items.Add (changeEntry);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show (
            "Could not open change set:\r\n" + ex,
            "Error while opening change set",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
      }
    }


    private void copyAsXmlButton_Click (object sender, EventArgs args)
    {
      Change[] changes;
      if (IgnoredChangesListBox.SelectedItems.Count > 0)
      {
        changes = IgnoredChangesListBox.SelectedItems
            .Cast<ChangeEntry>()
            .Select (e => e.Change)
            .ToArray();
      }
      else
      {
        changes = IgnoredChangesListBox.Items
            .Cast<ChangeEntry>()
            .Select (e => e.Change)
            .ToArray();
      }

      if (changes.Length == 0)
        return;

      var includeOld = includeOldSourceCheckbox.Checked;
      var includeNew = includeNewSourceCheckbox.Checked;

      if (!includeOld || !includeNew)
      {
        using (var shaUtility = new ShaUtility (SHA256.Create()))
        {
          changes = changes.Select (
              e =>
              {
                if (!(e is MemberChange))
                  return e;

                var memberChange = (MemberChange) e.Clone();

                var oldSource = memberChange.OldSource;
                if (!includeOld && oldSource != null)
                {
                  if (oldSource.Hash == null)
                    oldSource.Hash = shaUtility.ComputeHashAsString (oldSource.Text);

                  oldSource.Text = null;
                }

                var newSource = memberChange.NewSource;
                if (!includeNew && newSource != null)
                {
                  if (newSource.Hash == null)
                    newSource.Hash = shaUtility.ComputeHashAsString (newSource.Text);

                  newSource.Text = null;
                }

                return memberChange;
              }).ToArray();
        }
      }

      var changeSet = new ChangeSet (changes);
      var xmlSerializer = new XmlSerializer (typeof (ChangeSet));

      using (var stringWriter = new StringWriter())
      {
        xmlSerializer.Serialize (stringWriter, changeSet);

        var result = stringWriter.ToString();
        var parts = result.Split (new[] { Environment.NewLine }, StringSplitOptions.None).ToArray();


        Clipboard.SetText (string.Join (Environment.NewLine, parts.Skip (3).Take (parts.Length - 5)));
      }
    }

    private void CompletePendingChangeButton_Click (object sender, EventArgs e)
        => MoveChangesBetweenListBoxes (PendingChangesListBox, CompletedListBox);

    private void IgnorePendingChangesButton_Click (object sender, EventArgs e)
        => MoveChangesBetweenListBoxes (PendingChangesListBox, IgnoredChangesListBox);

    private void PendIgnoredChangeButton_Click (object sender, EventArgs e)
        => MoveChangesBetweenListBoxes (IgnoredChangesListBox, PendingChangesListBox);

    private void CompleteIgnoredChangeButton_Click (object sender, EventArgs e)
        => MoveChangesBetweenListBoxes (IgnoredChangesListBox, CompletedListBox);

    private void IgnoreCompletedChangeButton_Click (object sender, EventArgs e)
        => MoveChangesBetweenListBoxes (CompletedListBox, IgnoredChangesListBox);

    private void PendCompletedChangeButton_Click (object sender, EventArgs e) => MoveChangesBetweenListBoxes (CompletedListBox, PendingChangesListBox);

    private void MoveChangesBetweenListBoxes (ListBox from, ListBox to)
    {
      var changes = from.SelectedItems.Cast<ChangeEntry>().ToArray();
      foreach (var entry in changes)
      {
        from.Items.Remove (entry);
        InsertAtCorrectPosition (to, entry);
      }
    }

    private void InsertAtCorrectPosition (ListBox listBox, ChangeEntry entry)
    {
      var position = 0;
      while (position < listBox.Items.Count && ((ChangeEntry) listBox.Items[position]).Index < entry.Index)
      {
        position++;
      }

      listBox.Items.Insert (position, entry);
    }


    private void MarkAllPendingChangesButton_Click (object sender, EventArgs e) => SelectAllItems (PendingChangesListBox, true);

    private void MarkAllIgnoredChangesButton_Click (object sender, EventArgs e) => SelectAllItems (IgnoredChangesListBox, true);

    private void MarkAllCompletedChangesButton_Click (object sender, EventArgs e) => SelectAllItems (CompletedListBox, true);

    private void SelectAllItems (ListBox listBox, bool state)
    {
      for (var i = 0; i < listBox.Items.Count; i++)
        listBox.SetSelected (i, state);
    }
  }
}