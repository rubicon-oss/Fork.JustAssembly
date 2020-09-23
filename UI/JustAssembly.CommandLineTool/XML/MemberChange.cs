using System;
using System.Xml.Serialization;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.XML
{
  public class MemberChange : Change
  {
    public MemberChange ()
    {
    }

    public MemberChange (string @namespace, string name, DifferenceDecoration type, SourceText oldSource, SourceText newSource)
        : base (@namespace, name, type, oldSource, newSource)
    {
    }

    /// <inheritdoc />
    public override Change Clone ()
    {
      return new MemberChange (
          Namespace,
          Name,
          Type,
          OldSource?.Clone(),
          NewSource?.Clone());
    }
  }
}