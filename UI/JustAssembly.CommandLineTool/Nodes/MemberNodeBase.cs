﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JustAssembly.Interfaces;
using JustAssembly.Nodes.APIDiff;
using JustDecompile.External.JustAssembly;

namespace JustAssembly.CommandLineTool.Nodes
{
  internal abstract class MemberNodeBase : NodeBase
  {
    public IOldToNewTupleMap<MemberDefinitionMetadataBase> Map { get; }

    public IDecompilationResults OldDecompileResult { get; set; }

    public IDecompilationResults NewDecompileResult { get; set; }


    protected MemberNodeBase (NamespaceNode parent, IOldToNewTupleMap<MemberDefinitionMetadataBase> map, IDecompilationResults oldDecompileResult, IDecompilationResults newDecompileResult)
        : base (parent, map.GetFirstNotNullItem().GetName())
    {
      Map = map;
      OldDecompileResult = oldDecompileResult;
      NewDecompileResult = newDecompileResult;
    }

    protected MemberNodeBase (TypeNode parent, IOldToNewTupleMap<MemberDefinitionMetadataBase> map)
        : base (parent, map.GetFirstNotNullItem().GetName())
    {
      Map = map;

      if (parent != null)
      {
        OldDecompileResult = parent.OldDecompileResult;
        NewDecompileResult = parent.NewDecompileResult;
      }
    }

    protected MemberNodeBase (NestedTypeNode parent, IOldToNewTupleMap<MemberDefinitionMetadataBase> map)
        : base (parent, map.GetFirstNotNullItem().GetName())
    {
      Map = map;

      if (parent != null)
      {
        OldDecompileResult = parent.OldDecompileResult;
        NewDecompileResult = parent.NewDecompileResult;
      }
    }

    protected string GetMemberSource(IDecompilationResults result, MemberDefinitionMetadataBase metadata)
    {
      if (metadata == null || result == null)
      {
        return string.Empty;
      }

      IOffsetSpan offsetSpan;
      if (result.MemberTokenToDecompiledCodeMap.TryGetValue(metadata.TokenId, out offsetSpan))
      {
        try
        {
          string source = File.ReadAllText(result.FilePath);
          return GetMemberSource(source, offsetSpan, metadata.TokenId, result);
        }
        catch
        {
          return string.Empty;
        }
      }
      return string.Empty;
    }

    private string GetMemberSource(string sourceCode, IOffsetSpan memberOffset, uint memberId, IDecompilationResults decompilationResult)
    {
      StringBuilder memberSourceBuilder = new StringBuilder();

      memberSourceBuilder.Append(GetMemberSourceFromMap(sourceCode, decompilationResult.MemberTokenToAttributesMap, memberId))
          .Append(GetMemberSourceFromMap(sourceCode, decompilationResult.MemberTokenToDocumentationMap, memberId))
          .Append(sourceCode.Substring(memberOffset.StartOffset, memberOffset.EndOffset - memberOffset.StartOffset + 1));

      return memberSourceBuilder.ToString();
    }

    private string GetMemberSourceFromMap(string sourceCode, IDictionary<uint, IOffsetSpan> memberIdToOffsetMap, uint memberId)
    {
      IOffsetSpan offset;

      if (memberIdToOffsetMap.TryGetValue(memberId, out offset))
      {
        return sourceCode.Substring(offset.StartOffset, offset.EndOffset - offset.StartOffset + 1);
      }
      return string.Empty;
    }
  }
}