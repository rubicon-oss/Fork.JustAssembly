using System;
using System.Collections.Generic;
using System.Linq;
using JustAssembly.CommandLineTool.XML;

namespace JustAssembly.CommandLineTool
{
  public class IgnoredChangesSet
  {
    private readonly Dictionary<ChangeKey, List<Change>> _ignoredChangesLookup;

    public IgnoredChangesSet (ChangeSet changeSet)
    {
      _ignoredChangesLookup = ToDictionary (changeSet);
    }

    public bool Contains (Change change)
    {
      List<Change> ignoredChanges;
      if (!_ignoredChangesLookup.TryGetValue (new ChangeKey (change.Namespace, change.Name), out ignoredChanges))
        return false;

      return ignoredChanges.Any (e => e == change);
    }

    private static Dictionary<ChangeKey, List<Change>> ToDictionary (ChangeSet changeSet)
    {
      var result = new Dictionary<ChangeKey, List<Change>>();
      foreach (var change in changeSet.MemberChanges)
      {
        var key = new ChangeKey (change.Namespace, change.Name);

        List<Change> ignoredChanges;
        if (result.TryGetValue (key, out ignoredChanges))
        {
          if (ignoredChanges.Any (e => e == change))
            throw new InvalidOperationException ($"The ignore change set contains the duplicate entry '{key}'.");

          ignoredChanges.Add (change);
          continue;
        }

        result.Add (key, new List<Change> (1) { change });
      }

      return result;
    }
  }
}