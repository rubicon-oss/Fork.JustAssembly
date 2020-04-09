using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using JustAssembly.CommandLineTool.Nodes;
using JustAssembly.CommandLineTool.XML;
using JustAssembly.Interfaces;
using JustAssembly.MergeUtilities;
using JustAssembly.Nodes;
using JustDecompile.External.JustAssembly;
using Mono.Cecil;

namespace JustAssembly.CommandLineTool
{
  public class Differ
  {
    private readonly IFileGenerationNotifier progressNotifier;

    public Differ (IFileGenerationNotifier progressNotifier)
    {
      this.progressNotifier = progressNotifier;
    }

    public string CreateXMLDiff (IOldToNewTupleMap<string> assemblyMap, CancellationToken cancellationToken)
    {
      try
      {
        var generationProjectInfoMap = new OldToNewTupleMap<GeneratedProjectOutputInfo>
            (
            new GeneratedProjectOutputInfo (assemblyMap.OldType),
            new GeneratedProjectOutputInfo (assemblyMap.NewType)
            );

        Console.Write ("Loading old assembly...");
        var assemblyDecompilationResultOld = GetAssemblyDecompilationResult (
            assemblyMap.OldType,
            generationProjectInfoMap.OldType.OutputPath,
            cancellationToken);
        GlobalDecompilationResultsRepository.Instance.AddDecompilationResult (assemblyMap.OldType, assemblyDecompilationResultOld);
        Console.WriteLine ("done.");

        Console.Write ("Loading new assembly...");
        var assemblyDecompilationResultNew = GetAssemblyDecompilationResult (
            assemblyMap.NewType,
            generationProjectInfoMap.NewType.OutputPath,
            cancellationToken);
        GlobalDecompilationResultsRepository.Instance.AddDecompilationResult (assemblyMap.NewType, assemblyDecompilationResultNew);
        Console.WriteLine ("done.");

        var assemblyNode = AssemblyNode.Create (assemblyMap, generationProjectInfoMap);

        var visitor = new ChangeSetNodeVisitor (true);
        visitor.VisitAssemblyNode (assemblyNode);

        var changeSet = visitor.AsChangeSet();

        var xmlSerializer = new XmlSerializer (typeof (ChangeSet));
        using (var stringWriter = new StringWriter())
        {
          xmlSerializer.Serialize (stringWriter, changeSet);
          return stringWriter.ToString();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine ("Could not decompiler: \r\n" + ex);
        throw ex;
      }
    }

    private IAssemblyDecompilationResults GetAssemblyDecompilationResult (string path, string outputPath, CancellationToken cancellationToken)
    {
      var assembly = AssemblyDefinition.ReadAssembly (path, new ReaderParameters (GlobalAssemblyResolver.Instance));

      return Decompiler.GenerateFiles (
          path,
          assembly,
          outputPath,
          SupportedLanguage.CSharp,
          cancellationToken,
          true,
          progressNotifier);
    }
  }
}