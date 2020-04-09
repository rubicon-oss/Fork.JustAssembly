using System;
using System.Xml;
using System.Xml.Serialization;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.XML
{
  public class MemberChange : Change
  {
    [XmlIgnore]
    public string OldSource { get; set; }

    [XmlElement ("OldSource")]
    public XmlCDataSection OldSourceCData
    {
      get { return OldSource == null ? null :new XmlDocument().CreateCDataSection (OldSource); }
      set { OldSource = value.Value; }
    }

    [XmlIgnore]
    public string NewSource { get; set; }

    [XmlElement ("NewSource")]
    public XmlCDataSection NewSourceCData
    {
      get { return NewSource == null ? null : new XmlDocument().CreateCDataSection (NewSource); }
      set { NewSource = value.Value; }
    }

    public MemberChange ()
    {
    }

    public MemberChange (string @namespace, string name, DifferenceDecoration type, string oldSource, string newSource)
        : base (@namespace, name, type)
    {
      Namespace = @namespace;
      OldSource = oldSource;
      NewSource = newSource;
    }

    protected bool Equals (MemberChange other)
    {
      return base.Equals (other) && string.Equals (OldSource, other.OldSource) && string.Equals (NewSource, other.NewSource);
    }

    public override bool Equals (object obj)
    {
      if (ReferenceEquals (null, obj)) return false;
      if (ReferenceEquals (this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals ((MemberChange) obj);
    }

    public override int GetHashCode ()
    {
      unchecked
      {
        int hashCode = base.GetHashCode();
        hashCode = (hashCode * 397) ^ OldSource.GetHashCode();
        hashCode = (hashCode * 397) ^ NewSource.GetHashCode();
        return hashCode;
      }
    }

    public static bool operator == (MemberChange left, MemberChange right)
    {
      return Equals (left, right);
    }

    public static bool operator != (MemberChange left, MemberChange right)
    {
      return !Equals (left, right);
    }
  }
}