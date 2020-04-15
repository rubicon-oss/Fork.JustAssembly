using JustAssembly.CommandLineTool.XML;

namespace JustAssembly.Minidiffer
{
  public class ChangeEntry
  {
    public int Index { get; }

    public Change Change { get; }

    public ChangeEntry (int index, Change change)
    {
      Index = index;
      Change = change;
    }

    public override string ToString ()
    {
      return $"{Change.Namespace}::{Change.Name}";
    }
  }
}