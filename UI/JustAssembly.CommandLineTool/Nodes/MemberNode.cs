using System;
using JustAssembly.Interfaces;
using JustAssembly.Nodes;
using JustDecompile.External.JustAssembly;

namespace JustAssembly.CommandLineTool.Nodes
{
  internal class MemberNode : MemberNodeBase
  {
    public IOldToNewTupleMap<MemberMetadata> Map { get; }

    public MemberNode (TypeNode parent, IOldToNewTupleMap<MemberMetadata> map)
        : base (parent, map)
    {
      Map = map;
    }

    public MemberNode (NestedTypeNode parent, IOldToNewTupleMap<MemberMetadata> map)
        : base (parent, map)
    {
      Map = map;
    }

    public override void Accept (NodeVisitorBase visitor)
    {
      visitor.VisitMemberNode (this);
    }

    public override DifferenceDecoration GetDifferenceDecoration ()
    {
      if (Map.OldType == null)
        return DifferenceDecoration.Added;

      if (Map.NewType == null)
        return DifferenceDecoration.Deleted;

      if (Parent.DifferenceDecoration == DifferenceDecoration.Modified)
      {
        IOffsetSpan offsetSpanL;
        IOffsetSpan offsetSpanR;

        var containsValues = OldDecompileResult.MemberTokenToDecompiledCodeMap.TryGetValue (Map.OldType.TokenId, out offsetSpanL) &&
                             NewDecompileResult.MemberTokenToDecompiledCodeMap.TryGetValue (Map.NewType.TokenId, out offsetSpanR);

        if (containsValues)
        {
          return GetMemberSource (OldDecompileResult, Map.OldType) == GetMemberSource (NewDecompileResult, Map.NewType)
              ? DifferenceDecoration.NoDifferences
              : DifferenceDecoration.Modified;
        }
      }

      return DifferenceDecoration.NoDifferences;
    }
  }
}