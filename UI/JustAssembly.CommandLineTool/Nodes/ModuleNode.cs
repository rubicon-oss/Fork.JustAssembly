using System;
using System.Collections.Generic;
using System.Linq;
using JustAssembly.Core;
using JustAssembly.Interfaces;
using JustAssembly.MergeUtilities;
using JustAssembly.Nodes;
using JustAssembly.Nodes.APIDiff;

namespace JustAssembly.CommandLineTool.Nodes
{
  internal class ModuleNode : NodeBase
  {
    public static ModuleNode CreateFromModuleMap (AssemblyNode parent, IOldToNewTupleMap<ModuleMetadata> moduleMap)
    {
      var module = new ModuleNode (parent, moduleMap, new NamespaceNode[0]);

      var types = new TypesMergeManager (moduleMap)
          .GetMergedCollection()
          .ToList();

      var namespaces = types.GroupBy (e => e.GetFirstNotNullItem().GetNamespace())
          .OrderBy (e => e.Key)
          .Select (e => NamespaceNode.Create (module, e.Key, e.ToArray()))
          .ToArray();
      module.Namespaces = namespaces;

      return module;
    }

    public new AssemblyNode Parent { get; }

    public IOldToNewTupleMap<ModuleMetadata> Map { get; }

    public IReadOnlyList<NamespaceNode> Namespaces { get; private set; }

    public ModuleNode (AssemblyNode parent, IOldToNewTupleMap<ModuleMetadata> map, IReadOnlyList<NamespaceNode> namespaces)
        : base (parent, map.GetFirstNotNullItem().GetName())
    {
      Parent = parent;
      Map = map;
      Namespaces = namespaces;
    }

    public override void Accept (NodeVisitorBase visitor) => visitor.VisitModuleNode (this);

    public override DifferenceDecoration GetDifferenceDecoration ()
    {
      if (Map.OldType == null)
        return DifferenceDecoration.Added;

      if (Map.NewType == null)
        return DifferenceDecoration.Deleted;

      return ModuleManager.AreModulesEquals (Map.OldType, Map.NewType) ? DifferenceDecoration.NoDifferences : DifferenceDecoration.Modified;
    }

    private static IReadOnlyList<IMetadataDiffItem> GetDiffItemsList (
        IEnumerable<IOldToNewTupleMap<TypeMetadata>> metadataList,
        LoadAPIItemsContext context)
    {
      return context == null ? null : metadataList.Select (context.GetDiffItem).ToList();
    }
  }
}