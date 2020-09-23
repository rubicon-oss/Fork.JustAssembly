using System;
using System.Security.Cryptography;
using JustAssembly.CommandLineTool.Nodes;
using JustAssembly.CommandLineTool.Utility;
using JustAssembly.CommandLineTool.XML;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool
{
  internal class ChangeSetNodeVisitor : NodeVisitorBase
  {
    private readonly IgnoredChangesSet _ignoreChangeSet;
    private readonly ChangeSetBuilder _changeSetBuilder = new ChangeSetBuilder();

    private readonly ShaUtility _shaUtility = new ShaUtility (SHA256.Create());

    public bool IncludeSourceCode { get; }

    public ChangeSetNodeVisitor (IgnoredChangesSet ignoreChangeSet, bool includeSourceCode)
    {
      _ignoreChangeSet = ignoreChangeSet;
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

      foreach (var resource in node.Resources)
        resource.Accept (this);
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

      SourceText oldSourceText = null, newSourceText = null;
      if (IncludeSourceCode)
      {
        var oldSource = node.OldSource;
        if (oldSource != null)
          oldSourceText = new SourceText (oldSource, _shaUtility.ComputeHashAsString (oldSource));

        var newSource = node.NewSource;
        if (newSource != null)
          newSourceText = new SourceText (newSource, _shaUtility.ComputeHashAsString (newSource));
      }

      Change change = new MemberChange (
          node.Namespace,
          node.Name,
          node.DifferenceDecoration,
          oldSourceText,
          newSourceText);

      if (_ignoreChangeSet.Contains (change))
        return;

      _changeSetBuilder.AddChange (change);
    }

    public override void VisitResourceNode (ResourceNode node)
    {
      if (node.DifferenceDecoration == DifferenceDecoration.NoDifferences)
        return;

      SourceText oldSourceText = null, newSourceText = null;
      if (IncludeSourceCode)
      {
        var oldSource = node.OldText;
        if (oldSource != null)
          oldSourceText = new SourceText (oldSource, _shaUtility.ComputeHashAsString (oldSource));

        var newSource = node.NewText;
        if (newSource != null)
          newSourceText = new SourceText (newSource, _shaUtility.ComputeHashAsString (newSource));
      }

      Change change = new ResourceChange (
          node.Namespace,
          node.Name,
          node.DifferenceDecoration,
          oldSourceText,
          newSourceText);

      if (_ignoreChangeSet.Contains (change))
        return;

      _changeSetBuilder.AddChange (change);
    }
  }
}