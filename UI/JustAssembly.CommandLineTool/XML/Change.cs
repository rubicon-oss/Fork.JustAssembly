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

    public Change ()
    {
    }

    protected Change (string @namespace, string name, DifferenceDecoration type)
    {
      Namespace = @namespace;
      Name = name;
      Type = type;
    }

    public abstract Change Clone ();

    protected bool Equals (Change other)
    {
      return string.Equals (Name, other.Name) && Type == other.Type && string.Equals (Namespace, other.Namespace);
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
        var hashCode = Name.GetHashCode();
        hashCode = (hashCode * 397) ^ (int) Type;
        hashCode = (hashCode * 397) ^ Namespace.GetHashCode();
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