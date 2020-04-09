using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JustAssembly.Interfaces;
using JustAssembly.MergeUtilities;
using JustAssembly.Nodes;
using JustDecompile.External.JustAssembly;

namespace JustAssembly.CommandLineTool.Nodes
{
  internal class TypeNode : MemberNodeBase
  {
    public static TypeNode Create (NamespaceNode parent, IOldToNewTupleMap<TypeMetadata> map)
    {
      var oldTypeMetadata = map.OldType;
      var newTypeMetadata = map.NewType;

      IDecompilationResults oldResult = null;
      IDecompilationResults newResult = null;

      IDecompilationResults result;

      if (oldTypeMetadata != null &&
          GlobalDecompilationResultsRepository.Instance.TryGetDecompilationResult (
              oldTypeMetadata.AssemblyPath,
              oldTypeMetadata.Module.TokenId,
              oldTypeMetadata.TokenId,
              out result))
      {
        oldResult = result;
      }

      if (newTypeMetadata != null &&
          GlobalDecompilationResultsRepository.Instance.TryGetDecompilationResult (
              newTypeMetadata.AssemblyPath,
              newTypeMetadata.Module.TokenId,
              newTypeMetadata.TokenId,
              out result))
      {
        newResult = result;
      }

      var typeNode = new TypeNode (parent, map, new MemberNodeBase[0], oldResult, newResult);

      var memberManager = new MemberMergeManager (map);
      var members = memberManager.GetMergedCollection().Select<IOldToNewTupleMap<MemberDefinitionMetadataBase>, MemberNodeBase> (
          e =>
          {
            if (e.GetFirstNotNullItem().MemberType == MemberType.Type)
            {
              return NestedTypeNode.Create (
                  typeNode,
                  new OldToNewTupleMap<TypeMetadata> ((TypeMetadata) e.OldType, (TypeMetadata) e.NewType));
            }
            return new MemberNode (
                typeNode,
                new OldToNewTupleMap<MemberMetadata> ((MemberMetadata) e.OldType, (MemberMetadata) e.NewType));
          }).ToArray();
      typeNode.Members = members;


      return typeNode;
    }

    public new NodeBase Parent { get; }

    public IOldToNewTupleMap<TypeMetadata> Map { get; }

    public IReadOnlyList<MemberNodeBase> Members { get; protected set; }

    public override string OldSource
    {
      get { return GetFullSource (OldDecompileResult); }
    }

    public override string NewSource
    {
      get { return GetFullSource (NewDecompileResult); }
    }

    public TypeNode (
        NamespaceNode parent,
        IOldToNewTupleMap<TypeMetadata> map,
        IReadOnlyList<MemberNodeBase> members,
        IDecompilationResults oldDecompileResult,
        IDecompilationResults newDecompileResult)
        : base (parent, map, oldDecompileResult, newDecompileResult)
    {
      Parent = parent;
      Map = map;
      Members = members;
    }

    public TypeNode (
        TypeNode parent,
        IOldToNewTupleMap<TypeMetadata> map,
        IReadOnlyList<MemberNodeBase> members)
        : base (parent, map)
    {
      Parent = parent;
      Map = map;
      Members = members;
    }

    /// <inheritdoc />
    public override void Accept (NodeVisitorBase visitor) => visitor.VisitTypeNode (this);

    /// <inheritdoc />
    public override DifferenceDecoration GetDifferenceDecoration ()
    {
      if (Map.OldType == null)
      {
        return DifferenceDecoration.Added;
      }
      if (Map.NewType == null)
      {
        return DifferenceDecoration.Deleted;
      }
      if (OldDecompileResult == null && NewDecompileResult == null)
      {
        return DifferenceDecoration.NoDifferences;
      }

      var oldCleanSource = CleanExceptionSource (OldDecompileResult, OldSource);

      var newCleanSource = CleanExceptionSource (NewDecompileResult, NewSource);

      return oldCleanSource == newCleanSource ? DifferenceDecoration.NoDifferences : DifferenceDecoration.Modified;
    }

    protected string GetFullSource (IDecompilationResults result)
    {
      if (result == null)
      {
        return string.Empty;
      }

      try
      {
        return File.ReadAllText (result.FilePath);
      }
      catch
      {
        return string.Empty;
      }
    }

    private string CleanExceptionSource (IDecompilationResults decompilationResult, string sourceCode)
    {
      var spansForRemoving = new List<IOffsetSpan>();

      foreach (var memberId in decompilationResult.MembersWithExceptions)
      {
        IOffsetSpan memberOffset;

        if (decompilationResult.MemberTokenToDecompiledCodeMap.TryGetValue (memberId, out memberOffset))
        {
          spansForRemoving.Add (memberOffset);
        }
      }
      spansForRemoving.Sort ((i1, i2) => i2.StartOffset.CompareTo (i1.StartOffset));

      for (var i = 0; i < spansForRemoving.Count; i++)
      {
        var memberOffset = spansForRemoving[i];

        sourceCode = sourceCode.Remove (memberOffset.StartOffset, memberOffset.EndOffset - memberOffset.StartOffset + 1);
      }
      return sourceCode;
    }
  }
}