using System;
using System.Xml.Serialization;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.XML
{
  public class ResourceChange : Change
  {
    public ResourceChange ()
    {
    }

    public ResourceChange (string @namespace, string name, DifferenceDecoration type, SourceText oldSource, SourceText newSource)
        : base (@namespace, name, type, oldSource, newSource)
    {
    }

    /// <inheritdoc />
    public override Change Clone ()
    {
      return new ResourceChange (
          Namespace,
          Name,
          Type,
          OldSource?.Clone(),
          NewSource?.Clone());
    }
  }
}