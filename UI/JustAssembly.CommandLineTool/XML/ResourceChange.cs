using System;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.XML
{
  public class ResourceChange : Change
  {
    public ResourceChange ()
    {
    }

    public ResourceChange (string @namespace, string name, DifferenceDecoration type)
        : base (@namespace, name, type)
    {
    }

    /// <inheritdoc />
    public override Change Clone ()
    {
      return new ResourceChange (
          Namespace,
          Name,
          Type);
    }
  }
}