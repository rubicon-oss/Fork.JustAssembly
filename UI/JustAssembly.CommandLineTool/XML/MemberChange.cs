using System;
using System.Xml;
using System.Xml.Serialization;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.XML
{
  public class MemberChange : Change
  {
    [XmlAttribute ("Namespace")]
    public string Namespace { get; set; }

    [XmlIgnore]
    public string OldSource { get; set; }

    [XmlElement ("OldSource")]
    public XmlCDataSection OldSourceCData
    {
      get { return new XmlDocument().CreateCDataSection (OldSource); }
      set { OldSource = value.Value; }
    }

    [XmlIgnore]
    public string NewSource { get; set; }

    [XmlElement ("NewSource")]
    public XmlCDataSection NewSourceCData
    {
      get { return new XmlDocument().CreateCDataSection (NewSource); }
      set { NewSource = value.Value; }
    }

    public MemberChange ()
    {
    }

    public MemberChange (string ns, string name, DifferenceDecoration type, string oldSource, string newSource)
        : base (name, type)
    {
      Namespace = ns;
      OldSource = oldSource;
      NewSource = newSource;
    }
  }
}