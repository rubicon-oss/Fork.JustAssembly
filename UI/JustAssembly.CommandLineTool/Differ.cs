using System;
using System.IO;
using System.Threading;
using JustAssembly.CommandLineTool.Nodes;
using JustAssembly.Interfaces;
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

    public string DoDiff (IOldToNewTupleMap<string> assemblyMap, CancellationToken cancellationToken)
    {
      try
      {
        Console.Write ("Loading old assembly...");
        var assemblyDecompilationResultOld = GetAssemblyDecompilationResult (assemblyMap.OldType, cancellationToken);
        GlobalDecompilationResultsRepository.Instance.AddDecompilationResult (assemblyMap.OldType, assemblyDecompilationResultOld);
        Console.WriteLine ("done.");

        Console.Write ("Loading new assembly...");
        var assemblyDecompilationResultNew = GetAssemblyDecompilationResult (assemblyMap.NewType, cancellationToken);
        GlobalDecompilationResultsRepository.Instance.AddDecompilationResult (assemblyMap.NewType, assemblyDecompilationResultNew);
        Console.WriteLine ("done.");

        var assemblyNode = AssemblyNode.Create (assemblyMap);

        var visitor = new XmlOutputNodeVisitor();
        visitor.VisitAssemblyNode (assemblyNode);

        return visitor.AsString();

        //using (StringWriter stringWriter = new StringWriter())
        //{
        //  using (XmlWriter xmlWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented })
        //  {
        //    DiffAssembly (assemblyMap);
        //  }

        //  return stringWriter.ToString();
        //}
      }
      catch (Exception ex)
      {
        Console.WriteLine ("Could not decompiler: \r\n" + ex);
        throw ex;
      }
    }

    private IAssemblyDecompilationResults GetAssemblyDecompilationResult (string path, CancellationToken cancellationToken)
    {
      var assembly = AssemblyDefinition.ReadAssembly (path, new ReaderParameters (GlobalAssemblyResolver.Instance));

      return Decompiler.GenerateFiles (
          path,
          assembly,
          GetTempPath (path),
          SupportedLanguage.CSharp,
          cancellationToken,
          true,
          progressNotifier);
    }

    private string GetTempPath (string fileName)
    {
      string path;
      do
      {
        path = string.Format (
            "{0}\\{1}_{2}",
            Path.Combine (Path.GetTempPath(), "JustAssembly"),
            Path.GetFileNameWithoutExtension (fileName),
            Path.GetRandomFileName());
      } while (Directory.Exists (path));

      return path;
    }
  }
}