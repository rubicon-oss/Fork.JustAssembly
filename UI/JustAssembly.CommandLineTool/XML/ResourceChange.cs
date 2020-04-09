using System;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.XML
{
  public class ResourceChange : Change
  {
    public ResourceChange ()
    {
    }

    public ResourceChange (string name, DifferenceDecoration type)
        : base (name, type)
    {
    }
  }
}