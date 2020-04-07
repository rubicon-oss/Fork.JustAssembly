﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using JustAssembly.CommandLineTool.Nodes;
using JustAssembly.Core.DiffItems;
using JustAssembly.Nodes;
using JustAssembly.Nodes.APIDiff;

namespace JustAssembly.CommandLineTool
{
  internal class XmlOutputNodeVisitor : NodeVisitorBase, IDisposable
  {
    private readonly StringBuilder stringBuilder;
    private readonly StringWriter stringWriter;
    private readonly XmlTextWriter xmlWriter;

    public XmlOutputNodeVisitor ()
    {
      stringBuilder = new StringBuilder();
      stringWriter = new StringWriter (stringBuilder);
      xmlWriter = new XmlTextWriter (stringWriter);
      xmlWriter.Formatting = Formatting.Indented;
    }

    public string AsString ()
    {
      xmlWriter.Flush();

      return stringBuilder.ToString();
    }

    public override void VisitAssemblyNode (AssemblyNode node)
    {
      if (node.DifferenceDecoration == DifferenceDecoration.NoDifferences)
        return;

      xmlWriter.WriteStartElement ("Assembly");
      xmlWriter.WriteAttributeString ("Name", node.Name);
      xmlWriter.WriteAttributeString ("DiffType", node.DifferenceDecoration.ToString());

      foreach (var module in node.Modules)
        module.Accept (this);

      xmlWriter.WriteEndElement();
    }


    public override void VisitModuleNode (ModuleNode node)
    {
      if (node.DifferenceDecoration == DifferenceDecoration.NoDifferences)
        return;

      xmlWriter.WriteStartElement ("Module");
      xmlWriter.WriteAttributeString ("Name", node.Name);
      xmlWriter.WriteAttributeString ("DiffType", node.DifferenceDecoration.ToString());

      foreach (var namespaceNode in node.Namespaces)
        namespaceNode.Accept (this);

      xmlWriter.WriteEndElement();
    }

    public override void VisitNamespaceNode (NamespaceNode node)
    {
      if (node.DifferenceDecoration == DifferenceDecoration.NoDifferences)
        return;

      xmlWriter.WriteStartElement ("Namespace");
      xmlWriter.WriteAttributeString ("Name", node.Name);
      xmlWriter.WriteAttributeString ("DiffType", node.DifferenceDecoration.ToString());

      foreach (var module in node.Types)
        module.Accept (this);

      xmlWriter.WriteEndElement();
    }

    public override void VisitTypeNode (TypeNode node)
    {
      if (node.DifferenceDecoration == DifferenceDecoration.NoDifferences)
        return;

      xmlWriter.WriteStartElement ("Type");
      xmlWriter.WriteAttributeString ("Name", node.Name);
      xmlWriter.WriteAttributeString ("DiffType", node.DifferenceDecoration.ToString());

      foreach (var member in node.Members)
        member.Accept (this);

      xmlWriter.WriteEndElement();
    }

    public override void VisitNestedTypeNode (NestedTypeNode node)
    {
      VisitTypeNode (node);
    }

    public override void VisitMemberNode (MemberNode node)
    {
      if (node.DifferenceDecoration == DifferenceDecoration.NoDifferences)
        return;

      xmlWriter.WriteStartElement ("Member");
      xmlWriter.WriteAttributeString ("Name", node.Name);
      xmlWriter.WriteAttributeString ("DiffType", node.DifferenceDecoration.ToString());

      xmlWriter.WriteEndElement();
    }

    public void Dispose ()
    {
      xmlWriter?.Dispose();
    }
  }
}