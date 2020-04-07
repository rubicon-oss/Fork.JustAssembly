using System;
using System.Collections.Generic;
using System.Linq;
using JustAssembly.Interfaces;
using JustAssembly.MergeUtilities;
using JustAssembly.Nodes;
using JustDecompile.External.JustAssembly;

namespace JustAssembly.CommandLineTool.Nodes
{
  internal class NestedTypeNode : TypeNode
  {
    public static NestedTypeNode Create (TypeNode parent, IOldToNewTupleMap<TypeMetadata> map)
    {
      var nestedTypeNode = new NestedTypeNode (parent, map, new MemberNodeBase[0]);

      var memberManager = new MemberMergeManager (map);
      var members = memberManager.GetMergedCollection().Select<IOldToNewTupleMap<MemberDefinitionMetadataBase>, MemberNodeBase> (
          e =>
          {
            if (e.GetFirstNotNullItem().MemberType == MemberType.Type)
            {
              return Create (
                  parent,
                  new OldToNewTupleMap<TypeMetadata> ((TypeMetadata) e.OldType, (TypeMetadata) e.NewType));
            }
            return new MemberNode (
                nestedTypeNode,
                new OldToNewTupleMap<MemberMetadata> ((MemberMetadata) e.OldType, (MemberMetadata) e.NewType));
          }).ToArray();
      nestedTypeNode.Members = members;

      return nestedTypeNode;
    }

    public NestedTypeNode (TypeNode parent, IOldToNewTupleMap<TypeMetadata> map, IReadOnlyList<MemberNodeBase> members)
        : base (parent, map, members)
    {
    }

    public override void Accept (NodeVisitorBase visitor)
    {
      visitor.VisitNestedTypeNode (this);
    }

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
      if (Parent.DifferenceDecoration == DifferenceDecoration.Modified)
      {
        if (OldDecompileResult.MemberTokenToDecompiledCodeMap.ContainsKey (Map.OldType.TokenId))
        {
          return GetMemberSource (OldDecompileResult, Map.OldType)
                 == GetMemberSource (NewDecompileResult, Map.NewType)
              ? DifferenceDecoration.NoDifferences
              : DifferenceDecoration.Modified;
        }
      }
      return DifferenceDecoration.NoDifferences;
    }
  }
}