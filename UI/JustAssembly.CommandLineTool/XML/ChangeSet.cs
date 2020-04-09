using System;
using System.Xml.Serialization;

namespace JustAssembly.CommandLineTool.XML
{
  [XmlRoot ("ChangeSet")]
  public class ChangeSet
  {
    [XmlArray ("Changes")]
    [XmlArrayItem ("MemberChange", typeof (MemberChange))]
    [XmlArrayItem ("ResourceChange", typeof (ResourceChange))]
    public Change[] MemberChanges { get; set; }

    public ChangeSet ()
    {
    }

    public ChangeSet (Change[] memberChanges)
    {
      MemberChanges = memberChanges;
    }
  }
}