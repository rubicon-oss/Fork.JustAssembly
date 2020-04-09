using System;
using System.Collections.Generic;
using JustAssembly.CommandLineTool.Nodes;
using JustAssembly.CommandLineTool.XML;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool
{
  internal class ChangedNodesNodeVisitor : NodeVisitorBase
  {
    private readonly IgnoredChangesSet _ignoreChangeSet;
    private readonly List<MemberNodeBase> _changedNodes = new List<MemberNodeBase>();

    public bool IncludeSourceCode { get; }

    public ChangedNodesNodeVisitor (IgnoredChangesSet ignoreChangeSet, bool includeSourceCode)
    {
      _ignoreChangeSet = ignoreChangeSet;
      IncludeSourceCode = includeSourceCode;
    }

    public IReadOnlyList<MemberNodeBase> GetChangedNodes ()
    {
      return _changedNodes;
    }

    public override void VisitAssemblyNode (AssemblyNode node)
    {
      if (node.DifferenceDecoration == DifferenceDecoration.NoDifferences)
        return;

      foreach (var module in node.Modules)
        module.Accept (this);
    }


    public override void VisitModuleNode (ModuleNode node)
    {
      if (node.DifferenceDecoration == DifferenceDecoration.NoDifferences)
        return;

      foreach (var namespaceNode in node.Namespaces)
        namespaceNode.Accept (this);
    }

    public override void VisitNamespaceNode (NamespaceNode node)
    {
      if (node.DifferenceDecoration == DifferenceDecoration.NoDifferences)
        return;

      foreach (var module in node.Types)
        module.Accept (this);
    }

    public override void VisitTypeNode (TypeNode node)
    {
      if (node.DifferenceDecoration == DifferenceDecoration.NoDifferences)
        return;

      foreach (var member in node.Members)
        member.Accept (this);
    }

    public override void VisitNestedTypeNode (NestedTypeNode node)
    {
      VisitTypeNode (node);
    }

    public override void VisitMemberNode (MemberNode node)
    {
      if (node.DifferenceDecoration == DifferenceDecoration.NoDifferences)
        return;

      Change change = new MemberChange (
          node.Namespace,
          node.Name,
          node.DifferenceDecoration,
          IncludeSourceCode ? node.OldSource : null,
          IncludeSourceCode ? node.NewSource : null);

      if (_ignoreChangeSet.Contains (change))
        return;

      _changedNodes.Add (node);
    }
  }
}