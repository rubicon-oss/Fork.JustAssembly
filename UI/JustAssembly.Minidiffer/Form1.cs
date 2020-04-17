using System;
using System.Collections.Generic;
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

    private void button1_Click (object sender, EventArgs e)
    {
      if (openFileDialog1.ShowDialog() != DialogResult.OK)
        return;

      try
      {
        using (var textReader = File.OpenText (openFileDialog1.FileName))
        {
          var xmlReader = new XmlSerializer (typeof (ChangeSet));
          var changeSet = (ChangeSet) xmlReader.Deserialize (textReader);
          LoadChangeSet (changeSet, loadOrderedCheckbox.Checked);
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

    private void LoadChangeSet (ChangeSet changeSet, bool ordered)
    {
      changesListBox.Items.Clear();
      ignoredChangesListBox.Items.Clear();
      doneListBox.Items.Clear();

      var changes = ordered
          ? changeSet.MemberChanges.Where (e => e is MemberChange).OrderBy (e => $"{e.Namespace}__{e.Name}")
          : changeSet.MemberChanges.Where (e => e is MemberChange);

      var index = 0;
      foreach (var change in changes)
      {
        var changeEntry = new ChangeEntry (index++, change);
        changesListBox.Items.Add (changeEntry);
      }
    }

    private void ignoreButton_Click (object sender, EventArgs e)
    {
      var changes = changesListBox.SelectedItems.Cast<ChangeEntry>().ToArray();
      foreach (var entry in changes)
      {
        changesListBox.Items.Remove (entry);
        InsertAtCorrectPosition (ignoredChangesListBox, entry);
      }
    }

    private void unignoreButton_Click (object sender, EventArgs e)
    {
      var changes = ignoredChangesListBox.SelectedItems.Cast<ChangeEntry>().ToArray();
      foreach (var entry in changes)
      {
        ignoredChangesListBox.Items.Remove (entry);
        InsertAtCorrectPosition (changesListBox, entry);
      }
    }

    private void doneButton_Click (object sender, EventArgs e)
    {
      var changes = ignoredChangesListBox.SelectedItems.Cast<ChangeEntry>().ToArray();
      foreach (var entry in changes)
      {
        ignoredChangesListBox.Items.Remove (entry);
        InsertAtCorrectPosition (doneListBox, entry);
      }
    }

    private void undoneButton_Click (object sender, EventArgs e)
    {
      var changes = doneListBox.SelectedItems.Cast<ChangeEntry>().ToArray();
      foreach (var entry in changes)
      {
        doneListBox.Items.Remove (entry);
        InsertAtCorrectPosition (ignoredChangesListBox, entry);
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

    private void copyAsXmlButton_Click (object sender, EventArgs args)
    {
      Change[] changes;
      if (ignoredChangesListBox.SelectedItems.Count > 0)
      {
        changes = ignoredChangesListBox.SelectedItems
            .Cast<ChangeEntry>()
            .Select (e => e.Change)
            .ToArray();
      }
      else
      {
        changes = ignoredChangesListBox.Items
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
        using (var shaUtility = new ShaUtility(SHA256.Create()))
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

    private void markAllIgnoredButton_Click (object sender, EventArgs e)
    {
      SelectAllItems(ignoredChangesListBox, true);
    }

    private void markAllDoneButton_Click (object sender, EventArgs e)
    {
      SelectAllItems(doneListBox, true);
    }

    private void markAllCompletedButton_Click (object sender, EventArgs e)
    {
      SelectAllItems(changesListBox, true);
    }

    private void SelectAllItems (ListBox listBox, bool state)
    {
      for (var i = 0; i < listBox.Items.Count; i++)
        listBox.SetSelected (i, state);
    }
  }
}