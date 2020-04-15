using System;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace JustAssembly.CommandLineTool.XML
{
  public class SourceText
  {
    [XmlIgnore]
    public string Text { get; set; }

    [XmlText]
    public XmlNode[] TextCData
    {
      get { return new XmlNode[] { string.IsNullOrEmpty (Text) ? null : new XmlDocument().CreateCDataSection (Text) }; }
      set { Text = value?.First().Value; }
    }

    [XmlAttribute ("Hash")]
    public string Hash { get; set; }


    public SourceText ()
    {
    }

    public SourceText (string text, string hash)
    {
      Text = text;
      Hash = hash;
    }

    public SourceText Clone ()
    {
      return new SourceText (Text, Hash);
    }

    public static bool operator == (SourceText left, SourceText right)
    {
      if (ReferenceEquals (left, right))
        return true;

      if (ReferenceEquals (left, null) || ReferenceEquals (right, null))
        return false;

      if (left.Hash != null && right.Hash != null)
        return left.Hash == right.Hash;

      return string.Equals (left.Text, right.Text);
    }

    public static bool operator != (SourceText left, SourceText right)
    {
      return !Equals (left, right);
    }
  }
}