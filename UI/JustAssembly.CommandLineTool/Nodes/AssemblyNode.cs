using System;
using System.Collections.Generic;
using System.Linq;
using JustAssembly.Interfaces;
using JustAssembly.MergeUtilities;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.Nodes
{
  internal class AssemblyNode : NodeBase
  {
    public static AssemblyNode Create (IOldToNewTupleMap<string> assemblyMap)
    {
      var assemblyNode = new AssemblyNode (assemblyMap, new ModuleNode[0]);

      var moduleMergeManager = new ModuleManager (assemblyMap);
      var modulesNodes = moduleMergeManager.GetMergedCollection()
          .Select (tuple => ModuleNode.CreateFromModuleMap (assemblyNode, tuple))
          .ToArray();
      assemblyNode.Modules = modulesNodes;

      return assemblyNode;
    }

    public IOldToNewTupleMap<string> Map { get; }

    public IReadOnlyList<ModuleNode> Modules { get; private set; }

    public AssemblyNode (IOldToNewTupleMap<string> map, IReadOnlyList<ModuleNode> modules)
        : base (map.GetFirstNotNullItem())
    {
      Map = map;
      Modules = modules;
    }

    public override void Accept (NodeVisitorBase visitor) => visitor.VisitAssemblyNode (this);

    public override DifferenceDecoration GetDifferenceDecoration ()
    {
      if (string.IsNullOrWhiteSpace (Map.OldType))
        return DifferenceDecoration.Added;

      if (string.IsNullOrWhiteSpace (Map.NewType))
        return DifferenceDecoration.Deleted;

      if (Modules.Any (moduleNode => moduleNode.DifferenceDecoration == DifferenceDecoration.Modified))
        return DifferenceDecoration.Modified;

      return DifferenceDecoration.NoDifferences;
    }
  }
}