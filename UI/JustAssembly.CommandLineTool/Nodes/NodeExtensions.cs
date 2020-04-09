namespace JustAssembly.CommandLineTool.Nodes
{
  internal static class NodeExtensions
  {
    public static string GetNamespaceIncludingName (this NodeBase node)
    {
      if (string.IsNullOrEmpty (node.Namespace))
        return node.Name;

      return $"{node.Namespace}.{node.Name}";
    }
  }
}