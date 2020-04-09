using System;
using System.Collections.Generic;
using System.Linq;
using JustAssembly.CommandLineTool.XML;

namespace JustAssembly.CommandLineTool
{
  public class IgnoredChangesSet
  {
    private readonly Dictionary<ChangeKey, Change> _ignoredChangesLookup;

    public IgnoredChangesSet (ChangeSet changeSet)
    {
      _ignoredChangesLookup = ToDictionary (changeSet);
    }

    public bool Contains (Change change)
    {
      Change ignoreChange;
      return _ignoredChangesLookup.TryGetValue (new ChangeKey (change.Namespace, change.Name), out ignoreChange) && ignoreChange == change;
    }

    private static Dictionary<ChangeKey, Change> ToDictionary (ChangeSet changeSet)
    {
      //return changeSet.MemberChanges.ToDictionary (e => new ChangeKey (e.Namespace, e.Name), e => e);


      ChangeKey key = default(ChangeKey);
      try
      {
        var result = new Dictionary<ChangeKey, Change>();
        foreach (var change in changeSet.MemberChanges)
        {
          key = new ChangeKey (change.Namespace, change.Name);
          result.Add (key, change);
        }

        return result;
      }
      catch (ArgumentException ex)
      {
        throw new InvalidOperationException ($"Change set contains duplicate entry: '{key}'.", ex);
      }
    }
  }
}