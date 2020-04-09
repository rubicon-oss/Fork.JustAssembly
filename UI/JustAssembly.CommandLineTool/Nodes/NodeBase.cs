using System;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.Nodes
{
  internal abstract class NodeBase
  {
    private DifferenceDecoration? _difference;

    public NodeBase Parent { get; }

    public string Name { get; }

    public string Namespace { get; }

    public DifferenceDecoration DifferenceDecoration => _difference ?? (_difference = GetDifferenceDecoration()).Value;

    protected NodeBase (string @namespace, string name)
        : this (null, @namespace, name)
    {
    }

    protected NodeBase (NodeBase parent, string @namespace, string name)
    {
      Parent = parent;
      Namespace = @namespace;
      Name = name;
    }

    public abstract void Accept (NodeVisitorBase visitor);

    public abstract DifferenceDecoration GetDifferenceDecoration ();
  }
}