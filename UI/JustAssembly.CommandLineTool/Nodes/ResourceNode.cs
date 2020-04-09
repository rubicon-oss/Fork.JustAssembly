using System;
using System.IO;
using System.Linq;
using JustAssembly.Interfaces;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.Nodes
{
  internal class ResourceNode : NodeBase
  {
    public IOldToNewTupleMap<string> Map { get; }

    public ResourceNode (AssemblyNode parent, IOldToNewTupleMap<string> map)
        : base (parent, Path.GetFileName (map.GetFirstNotNullItem()))
    {
      Map = map;
    }

    /// <inheritdoc />
    public override void Accept (NodeVisitorBase visitor)
    {
      visitor.VisitResourceNode (this);
    }

    /// <inheritdoc />
    public override DifferenceDecoration GetDifferenceDecoration ()
    {
      if (string.IsNullOrWhiteSpace (Map.OldType))
        return DifferenceDecoration.Added;

      if (string.IsNullOrWhiteSpace (Map.NewType))
        return DifferenceDecoration.Deleted;

      var oldResourceBytes = File.ReadAllBytes (Map.OldType);
      var newResourceBytes = File.ReadAllBytes (Map.NewType);

      if (oldResourceBytes.Length != newResourceBytes.Length)
        return DifferenceDecoration.Modified;

      if (oldResourceBytes.Where ((t, j) => t != newResourceBytes[j]).Any())
        return DifferenceDecoration.Modified;

      return DifferenceDecoration.NoDifferences;
    }
  }
}