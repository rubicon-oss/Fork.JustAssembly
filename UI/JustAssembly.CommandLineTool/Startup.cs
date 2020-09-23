using JustAssembly.API.Analytics;
using JustAssembly.Core;
using JustAssembly.Infrastructure.Analytics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using JustAssembly.CommandLineTool.Nodes;
using JustAssembly.CommandLineTool.XML;
using JustAssembly.MergeUtilities;
using JustAssembly.Nodes;

namespace JustAssembly.CommandLineTool
{
    public class Startup
    {
        private static IAnalyticsService analytics;

        static void Main(string[] args)
        {
            analytics = AnalyticsServiceImporter.Instance.Import();

            analytics.Start();
            analytics.TrackFeature("Mode.CommandLine");

            try
            {
                RunMain(args);
            }
            catch (Exception ex)
            {
                analytics.TrackException(ex);
                analytics.Stop();

                throw;
            }

            analytics.Stop();
        }

        private static void RunMain(string[] args)
        {
            if (args.Length < 3 || args.Length > 4)
            {
                WriteErrorAndSetErrorCode("Wrong number of arguments." + Environment.NewLine + Environment.NewLine + "Sample:" + Environment.NewLine + "justassembly.commandlinetool Path\\To\\Assembly1 Path\\To\\Assembly2 Path\\To\\XMLOutput [Path\\To\\IgnoreXML]");
                return;
            }

            var oldAssemblyPath = args[0];
            if (!FilePathValidater.ValidateInputFile(oldAssemblyPath))
            {
                WriteErrorAndSetErrorCode("First assembly path is in incorrect format or file not found.");
                return;
            }

            var newAssemblyPath = args[1];
            if (!FilePathValidater.ValidateInputFile(newAssemblyPath))
            {
                WriteErrorAndSetErrorCode("Second assembly path is in incorrect format or file not found.");
                return;
            }

            var outputPath = args[2];
            if (!FilePathValidater.ValidateOutputFile(outputPath))
            {
                WriteErrorAndSetErrorCode("Output file path is in incorrect format.");
                return;
            }

            var ignoreFile = args.Length > 3 ? args[3] : null;
            IgnoredChangesSet ignoreChangeSet = new IgnoredChangesSet(new ChangeSet(new Change[0]));
            if (ignoreFile != null)
            {
                if (!FilePathValidater.ValidateOutputFile(ignoreFile))
                {
                    WriteErrorAndSetErrorCode("Ignore file path is in incorrect format.");
                    return;
                }

                try
                {
                    using (var textReader = File.OpenText (ignoreFile))
                    {
                        var xmlReader = new XmlSerializer (typeof (ChangeSet));
                        var rawChangeSet = (ChangeSet) xmlReader.Deserialize (textReader);
                        ignoreChangeSet = new IgnoredChangesSet (rawChangeSet);
                    }

                }
                catch (Exception ex)
                {
                    WriteExceptionAndSetErrorCode("A problem occurred while parsing the ignore file.", ex);
                    return;
                }
            }

            var differ = new Differ(ignoreChangeSet, new EmptyFileGenerationNotifier());

            ChangeSet changeSet;
            try
            {
                var typesMap = new OldToNewTupleMap<string> (oldAssemblyPath, newAssemblyPath);
                var assemblyNode = differ.CreateAssemblyNode (typesMap);
                changeSet = differ.CreateChangeSet (assemblyNode);
            }
            catch (Exception ex)
            {
                WriteExceptionAndSetErrorCode("A problem occurred while creating the change set.", ex);
                return;
            }

            try
            {
                differ.CreatePatchFileAndSources (
                    changeSet, 
                    Path.ChangeExtension (outputPath, ".patch"),
                    Path.ChangeExtension (outputPath, ".zip"));
            }
            catch (Exception ex)
            {
                WriteExceptionAndSetErrorCode("There was a problem while writing the patch file.", ex);
                return;
            }

            string xml = string.Empty;
            try
            {
                foreach (var change in changeSet.MemberChanges)
                {
                    if (change is ResourceChange)
                    {
                        if (change.OldSource != null)
                            change.OldSource.Text = null;
                        if (change.NewSource != null)
                            change.NewSource.Text = null;
                    }
                }
                xml = differ.CreateXMLDiff(changeSet);
            }
            catch (Exception ex)
            {
                WriteExceptionAndSetErrorCode("A problem occurred while creating the API diff.", ex);
                return;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    writer.Write(xml);
                }
            }
            catch (Exception ex)
            {
                WriteExceptionAndSetErrorCode("There was a problem while writing output file.", ex);
                return;
            }
      
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("API differences calculated successfully.");
            Console.ResetColor();
        }

        private static void WriteErrorAndSetErrorCode(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();

            Environment.ExitCode = 1;
        }

        private static void WriteExceptionAndSetErrorCode(string message, Exception ex)
        {
            analytics.TrackException(ex);

            WriteErrorAndSetErrorCode(string.Format("{0}{1}{2}", message, Environment.NewLine, ex));
        }
    }
}
