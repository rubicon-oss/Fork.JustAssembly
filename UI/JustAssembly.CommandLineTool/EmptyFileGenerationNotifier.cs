using JustDecompile.External.JustAssembly;

namespace JustAssembly.CommandLineTool
{
  public class EmptyFileGenerationNotifier : IFileGenerationNotifier
  {
    private uint totalFileCount;

    public void OnProjectFileGenerated (IFileGeneratedInfo args)
    {
    }

    public uint TotalFileCount
    {
      get { return totalFileCount; }
      set { totalFileCount = value; }
    }
  }
}