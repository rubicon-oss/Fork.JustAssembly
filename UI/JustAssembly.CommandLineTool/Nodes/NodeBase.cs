using System;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.Nodes
{
  internal abstract class NodeBase
  {
    private DifferenceDecoration? _difference;

    public NodeBase Parent { get; }

    public string Name { get; }

    public bool BreakingChangesOnly => false;

    public DifferenceDecoration DifferenceDecoration => _difference.HasValue ? _difference.Value : (_difference = GetDifferenceDecoration()).Value;

    protected NodeBase (string name)
        : this (null, name)
    {
    }

    protected NodeBase (NodeBase parent, string name)
    {
      Parent = parent;
      Name = name;
    }

    public abstract void Accept (NodeVisitorBase visitor);

    public abstract DifferenceDecoration GetDifferenceDecoration ();
  }
}