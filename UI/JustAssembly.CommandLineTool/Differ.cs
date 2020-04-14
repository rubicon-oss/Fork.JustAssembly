using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
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
  internal class Differ
  {
    private readonly IgnoredChangesSet ignoreChangeSet;
    private readonly IFileGenerationNotifier progressNotifier;

    public Differ (IgnoredChangesSet ignoreChangeSet, IFileGenerationNotifier progressNotifier)
    {
      this.ignoreChangeSet = ignoreChangeSet;
      this.progressNotifier = progressNotifier;
    }

    public AssemblyNode CreateAssemblyNode (IOldToNewTupleMap<string> assemblyMap)
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
          CancellationToken.None);
      GlobalDecompilationResultsRepository.Instance.AddDecompilationResult (assemblyMap.OldType, assemblyDecompilationResultOld);
      Console.WriteLine ("done.");

      Console.Write ("Loading new assembly...");
      var assemblyDecompilationResultNew = GetAssemblyDecompilationResult (
          assemblyMap.NewType,
          generationProjectInfoMap.NewType.OutputPath,
          CancellationToken.None);
      GlobalDecompilationResultsRepository.Instance.AddDecompilationResult (assemblyMap.NewType, assemblyDecompilationResultNew);
      Console.WriteLine ("done.");

      return AssemblyNode.Create (assemblyMap, generationProjectInfoMap);
    }

    public string CreateXMLDiff (AssemblyNode assemblyNode)
    {
      try
      {
        var visitor = new ChangeSetNodeVisitor (ignoreChangeSet, true);
        assemblyNode.Accept (visitor);

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

    public void CreatePatchFileAndSources (AssemblyNode assemblyNode, string patchFilePath, string sourcesPath)
    {
      try
      {
        var visitor = new ChangedNodesNodeVisitor (ignoreChangeSet, true);
        assemblyNode.Accept (visitor);

        var changedNodes = visitor.GetChangedNodes();

        var tempFolder = Path.Combine (Path.GetTempPath(), "JustAssembly_" + Path.GetRandomFileName());
        Directory.CreateDirectory (tempFolder);

        var oldFolder = Path.Combine (tempFolder, "old");
        Directory.CreateDirectory (oldFolder);

        var newFolder = Path.Combine (tempFolder, "new");
        Directory.CreateDirectory (newFolder);


        foreach (var node in changedNodes)
        {
          var fileName = GetNormalizedName (node);

          if (!string.IsNullOrEmpty (node.OldSource))
          {
            using (var writer = File.CreateText (Path.Combine (oldFolder, fileName)))
            {
              writer.WriteLine ("# Namespace: " + node.Namespace);
              writer.WriteLine ("# Name: " + node.Name);
              writer.Write (node.OldSource);
            }
          }

          if (!string.IsNullOrEmpty (node.NewSource))
          {
            using (var writer = File.CreateText (Path.Combine (newFolder, fileName)))
            {
              writer.WriteLine ("# Namespace: " + node.Namespace);
              writer.WriteLine ("# Name: " + node.Name);
              writer.Write (node.NewSource);
            }
          }
        }

        File.Delete (sourcesPath);
        ZipFile.CreateFromDirectory (tempFolder, sourcesPath);

        var processStartInfo = new ProcessStartInfo ("git.exe");
        processStartInfo.CreateNoWindow = true;
        processStartInfo.FileName = "cmd.exe";
        processStartInfo.Arguments = $"/c \"git diff --no-index old new > out.patch";
        processStartInfo.WorkingDirectory = tempFolder;

        var process = Process.Start (processStartInfo);
        process.WaitForExit();

        File.Delete (patchFilePath);
        File.Move (Path.Combine (tempFolder, "out.patch"), patchFilePath);

        try
        {
          Directory.Delete (tempFolder, true);
        }
        catch
        {
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine ("Could not create path file: \r\n" + ex);
        throw;
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

    private static string GetNormalizedName (MemberNodeBase node)
    {
      string ns = node.Namespace;
      if (ns.Length > 120)
        ns = ns.Substring (0, 120);

      string name = node.Name;
      if (name.Length > 50)
        name = name.Substring (0, 50);

      var unsanitizedPath = $"{ns}__{name}__{Path.GetRandomFileName()}.cs";
      return string.Join ("_", unsanitizedPath.Split (Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
    }
  }
}