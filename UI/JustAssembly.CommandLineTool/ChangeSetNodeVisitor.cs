﻿using System;
using System.Collections.Generic;
using JustAssembly.CommandLineTool.Nodes;
using JustAssembly.CommandLineTool.XML;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool
{
  internal class ChangeSetNodeVisitor : NodeVisitorBase
  {
    private readonly ChangeSetBuilder _changeSetBuilder;

    public bool IncludeSourceCode { get; }

    public ChangeSetNodeVisitor (ChangeSet ignoreChangeSet, bool includeSourceCode)
    {
      _changeSetBuilder = new ChangeSetBuilder (ignoreChangeSet);
      IncludeSourceCode = includeSourceCode;
    }

    public ChangeSet AsChangeSet ()
    {
      return _changeSetBuilder.Build();
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

      _changeSetBuilder.AddChange (change);
    }

    public override void VisitResourceNode (ResourceNode node)
    {
      if (node.DifferenceDecoration == DifferenceDecoration.NoDifferences)
        return;

      Change change = new ResourceChange (
          node.Namespace,
          node.Name,
          node.DifferenceDecoration);

      _changeSetBuilder.AddChange (change);
    }
  }
}