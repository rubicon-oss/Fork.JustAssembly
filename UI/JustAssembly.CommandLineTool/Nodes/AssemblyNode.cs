using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JustAssembly.Interfaces;
using JustAssembly.MergeUtilities;
using JustAssembly.Nodes;
using JustDecompile.External.JustAssembly;

namespace JustAssembly.CommandLineTool.Nodes
{
  internal class AssemblyNode : NodeBase
  {
    public static AssemblyNode Create (IOldToNewTupleMap<string> assemblyMap, IOldToNewTupleMap<GeneratedProjectOutputInfo> generationProjectInfoMap)
    {
      var assemblyNode = new AssemblyNode (assemblyMap, new ModuleNode[0], new ResourceNode[0]);

      var moduleMergeManager = new ModuleManager (assemblyMap);
      var modulesNodes = moduleMergeManager.GetMergedCollection()
          .Select (tuple => ModuleNode.CreateFromModuleMap (assemblyNode, tuple))
          .ToArray();
      assemblyNode.Modules = modulesNodes;

      ICollection<string> oldResources = new Collection<string>();
      ICollection<string> newResources = new Collection<string>();

      IAssemblyDecompilationResults oldAssemblyResult;
      IAssemblyDecompilationResults newAssemblyResult;

      if (GlobalDecompilationResultsRepository.Instance.TryGetAssemblyDecompilationResult(assemblyMap.OldType, out oldAssemblyResult))
      {
        oldResources = oldAssemblyResult.ResourcesFilePaths;
      }

      if (GlobalDecompilationResultsRepository.Instance.TryGetAssemblyDecompilationResult(assemblyMap.NewType, out newAssemblyResult))
      {
        newResources = newAssemblyResult.ResourcesFilePaths;
      }

      var resourceMergeManager = new ResourceMergeManager(
          new OldToNewTupleMap<ICollection<string>>(oldResources, newResources), 
          (a, b) => string.Compare(generationProjectInfoMap.OldType.GetRelativePath(a), generationProjectInfoMap.OldType.GetRelativePath(b), true),
          (a, b) => string.Compare(generationProjectInfoMap.NewType.GetRelativePath(a), generationProjectInfoMap.NewType.GetRelativePath(b), true),
          (a, b) => ResourceNameComparer(generationProjectInfoMap, a, b));

      var resourceNodes = resourceMergeManager.GetMergedCollection()
          .Select(e => new ResourceNode(assemblyNode, e))
          .ToArray();
      assemblyNode.Resources = resourceNodes;

      return assemblyNode;
    }

    public IOldToNewTupleMap<string> Map { get; }

    public IReadOnlyList<ModuleNode> Modules { get; private set; }

    public IReadOnlyList<ResourceNode> Resources { get; private set; }

    public AssemblyNode (IOldToNewTupleMap<string> map, IReadOnlyList<ModuleNode> modules, IReadOnlyList<ResourceNode> resources)
        : base (map.GetFirstNotNullItem())
    {
      Map = map;
      Modules = modules;
      Resources = resources;
    }

    public override void Accept (NodeVisitorBase visitor) => visitor.VisitAssemblyNode (this);

    public override DifferenceDecoration GetDifferenceDecoration ()
    {
      if (string.IsNullOrWhiteSpace (Map.OldType))
        return DifferenceDecoration.Added;

      if (string.IsNullOrWhiteSpace (Map.NewType))
        return DifferenceDecoration.Deleted;

      if (Modules.Any (moduleNode => moduleNode.DifferenceDecoration == DifferenceDecoration.Modified))
        return DifferenceDecoration.Modified;

      return DifferenceDecoration.NoDifferences;
    }

    private static int ResourceNameComparer(IOldToNewTupleMap<GeneratedProjectOutputInfo> generationProjectInfoMap, string oldName, string newName)
    {
      bool isOldNameEmpty = string.IsNullOrWhiteSpace(oldName);

      bool isNewNameEmpty = string.IsNullOrWhiteSpace(newName);

      if (!isOldNameEmpty && !isNewNameEmpty)
      {
        oldName = generationProjectInfoMap.OldType.GetRelativePath(oldName);
        newName = generationProjectInfoMap.NewType.GetRelativePath(newName);

        return string.Compare(oldName, newName, true);
      }
      else if (isOldNameEmpty && isNewNameEmpty)
      {
        return 0;
      }
      else if (isOldNameEmpty && !isNewNameEmpty)
      {
        return 1;
      }
      else
      {
        return -1;
      }
    }
  }
}