using System;
using System.Xml.Serialization;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool.XML
{
  public abstract class Change
  {
    [XmlAttribute ("Namespace")]
    public string Namespace { get; set; }

    [XmlAttribute ("Name")]
    public string Name { get; set; }

    [XmlAttribute ("ChangeType")]
    public DifferenceDecoration Type { get; set; }

    [XmlElement ("OldSource")]
    public SourceText OldSource { get; set; }

    [XmlElement ("NewSource")]
    public SourceText NewSource { get; set; }

    public Change ()
    {
    }

    protected Change (string @namespace, string name, DifferenceDecoration type, SourceText oldSource, SourceText newSource)
    {
      Namespace = @namespace;
      Name = name;
      Type = type;
      OldSource = oldSource;
      NewSource = newSource;
    }

    public abstract Change Clone ();

    protected bool Equals (Change other)
    {
      return Namespace == other.Namespace && Name == other.Name && Type == other.Type && OldSource == other.OldSource && NewSource == other.NewSource;
    }

    public override bool Equals (object obj)
    {
      if (ReferenceEquals (null, obj)) return false;
      if (ReferenceEquals (this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals ((Change) obj);
    }

    public override int GetHashCode ()
    {
      unchecked
      {
        var hashCode = Namespace.GetHashCode();
        hashCode = (hashCode * 397) ^ Name.GetHashCode();
        hashCode = (hashCode * 397) ^ (int) Type;
        hashCode = (hashCode * 397) ^ (OldSource != null ? OldSource.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (NewSource != null ? NewSource.GetHashCode() : 0);
        return hashCode;
      }
    }

    public static bool operator == (Change left, Change right)
    {
      return Equals (left, right);
    }

    public static bool operator != (Change left, Change right)
    {
      return !Equals (left, right);
    }
  }
}