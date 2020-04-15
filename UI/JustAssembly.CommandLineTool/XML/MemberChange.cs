using System;
using System.Xml.Serialization;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.XML
{
  public class MemberChange : Change
  {
    [XmlElement ("OldSource")]
    public SourceText OldSource { get; set; }

    [XmlElement ("NewSource")]
    public SourceText NewSource { get; set; }

    public MemberChange ()
    {
    }

    public MemberChange (string @namespace, string name, DifferenceDecoration type, SourceText oldSource, SourceText newSource)
        : base (@namespace, name, type)
    {
      Namespace = @namespace;
      OldSource = oldSource;
      NewSource = newSource;
    }

    protected bool Equals (MemberChange other)
    {
      return base.Equals (other) && OldSource == other.OldSource && NewSource == other.NewSource;
    }

    public override bool Equals (object obj)
    {
      if (ReferenceEquals (null, obj)) return false;
      if (ReferenceEquals (this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals ((MemberChange) obj);
    }

    public override int GetHashCode ()
    {
      unchecked
      {
        var hashCode = base.GetHashCode();
        hashCode = (hashCode * 397) ^ (OldSource != null ? OldSource.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (NewSource != null ? NewSource.GetHashCode() : 0);
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