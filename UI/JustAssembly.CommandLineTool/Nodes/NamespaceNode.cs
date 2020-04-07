using System;
using System.Collections.Generic;
using System.Linq;
using JustAssembly.Interfaces;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.Nodes
{
  internal class NamespaceNode : NodeBase
  {
    public static NamespaceNode Create (
        ModuleNode parent,
        string name,
        IReadOnlyList<IOldToNewTupleMap<TypeMetadata>> typeMaps)
    {
      var namespaceNode = new NamespaceNode (parent, name, new TypeNode[0]);

      var typeNodes = typeMaps
          .Select (e => TypeNode.Create (namespaceNode, e))
          .ToArray();
      namespaceNode.Types = typeNodes;

      return namespaceNode;
    }

    public new ModuleNode Parent { get; }

    public IReadOnlyList<TypeNode> Types { get; private set; }

    public NamespaceNode (ModuleNode parent, string name, IReadOnlyList<TypeNode> types)
        : base (parent, name)
    {
      Parent = parent;
      Types = types;
    }

    public override void Accept (NodeVisitorBase visitor) => visitor.VisitNamespaceNode (this);

    public override DifferenceDecoration GetDifferenceDecoration ()
    {
      var isNew = true;
      var isDeleted = true;
      var isModified = false;

      foreach (var typeMap in Types)
      {
        if (typeMap.Map.OldType != null)
        {
          isNew = false;
        }
        else
        {
          isModified = true;
        }
        if (typeMap.Map.NewType != null)
        {
          isDeleted = false;
        }
        else
        {
          isModified = true;
        }
        if (typeMap.DifferenceDecoration == DifferenceDecoration.Modified)
        {
          return DifferenceDecoration.Modified;
        }
      }
      if (isNew)
      {
        return DifferenceDecoration.Added;
      }
      if (isDeleted)
      {
        return DifferenceDecoration.Deleted;
      }
      if (isModified)
      {
        return DifferenceDecoration.Modified;
      }
      return DifferenceDecoration.NoDifferences;
    }
  }
}