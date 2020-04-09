using System;

namespace JustAssembly.CommandLineTool
{
  public struct ChangeKey
  {
    public readonly string Namespace;
    public readonly string Name;

    public ChangeKey (string @namespace, string name)
    {
      Namespace = @namespace;
      Name = name;
    }

    public bool Equals (ChangeKey other)
    {
      return string.Equals (Namespace, other.Namespace) && string.Equals (Name, other.Name);
    }

    public override bool Equals (object obj)
    {
      if (ReferenceEquals (null, obj)) return false;
      return obj is ChangeKey && Equals ((ChangeKey) obj);
    }

    public override int GetHashCode ()
    {
      unchecked
      {
        return (Namespace.GetHashCode() * 397) ^ Name.GetHashCode();
      }
    }

    public override string ToString ()
    {
      return $"{Namespace}::{Name}";
    }
  }
}