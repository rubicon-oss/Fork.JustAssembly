using System.Collections.Generic;

namespace JustAssembly.CommandLineTool
{
  public interface INode
  {
    IReadOnlyList<INode> Nodes { get; set; }
  }
}