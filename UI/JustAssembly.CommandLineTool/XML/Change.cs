using System;
using System.Xml.Serialization;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.XML
{
  public abstract class Change
  {
    [XmlAttribute ("Name")]
    public string Name { get; set; }

    [XmlAttribute ("ChangeType")]
    public DifferenceDecoration Type { get; set; }

    public Change ()
    {
    }
    
    protected Change (string name, DifferenceDecoration type)
    {
      Name = name;
      Type = type;
    }
  }
}