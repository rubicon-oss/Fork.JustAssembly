using System;
using System.Collections.Generic;
using JustAssembly.CommandLineTool.XML;

namespace JustAssembly.CommandLineTool
{
  public class ChangeSetBuilder
  {
    private readonly List<Change> _changes = new List<Change>();

    public ChangeSetBuilder ()
    {
    }

    public void AddChange (Change change)
    {
      _changes.Add (change);
    }

    public ChangeSet Build ()
    {
      return new ChangeSet (_changes.ToArray());
    }
  }
}