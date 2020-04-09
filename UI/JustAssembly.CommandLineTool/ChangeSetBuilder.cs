using System;
using System.Collections.Generic;
using System.Linq;
using JustAssembly.CommandLineTool.XML;

namespace JustAssembly.CommandLineTool
{
  public class ChangeSetBuilder
  {
    private struct IgnoreSetKey
    {
      public readonly string Namespace;
      public readonly string Name;

      public IgnoreSetKey (string @namespace, string name)
      {
        Namespace = @namespace;
        Name = name;
      }

      public bool Equals (IgnoreSetKey other)
      {
        return string.Equals (Namespace, other.Namespace) && string.Equals (Name, other.Name);
      }

      public override bool Equals (object obj)
      {
        if (ReferenceEquals (null, obj)) return false;
        return obj is IgnoreSetKey && Equals ((IgnoreSetKey) obj);
      }

      public override int GetHashCode ()
      {
        unchecked
        {
          return (Namespace.GetHashCode() * 397) ^ Name.GetHashCode();
        }
      }
    }

    private readonly ChangeSet ignoreChangeSet;
    private readonly Dictionary<IgnoreSetKey, Change> _ignoredChangesLookup;

    private readonly List<Change> _changes = new List<Change>();

    public ChangeSetBuilder (ChangeSet ignoreChangeSet)
    {
      this.ignoreChangeSet = ignoreChangeSet;
      _ignoredChangesLookup = ignoreChangeSet.MemberChanges.ToDictionary (e => new IgnoreSetKey (e.Namespace, e.Name), e => e);
    }

    public void AddChange (Change change)
    {
      Change ignoreChange;
      if (_ignoredChangesLookup.TryGetValue (new IgnoreSetKey (change.Namespace, change.Name), out ignoreChange) && ignoreChange == change)
        return;

      _changes.Add (change);
    }

    public ChangeSet Build ()
    {
      return new ChangeSet (_changes.ToArray());
    }
  }
}