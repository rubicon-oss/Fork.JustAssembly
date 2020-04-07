using JustAssembly.CommandLineTool.Nodes;

namespace JustAssembly.CommandLineTool
{
  abstract class NodeVisitorBase
  {
    public virtual void VisitAssemblyNode (AssemblyNode node) => DefaultVisit (node);

    public virtual void VisitMemberNode (MemberNode node) => DefaultVisit (node);

    public virtual void VisitModuleNode (ModuleNode node) => DefaultVisit (node);

    public virtual void VisitNamespaceNode (NamespaceNode node) => DefaultVisit (node);

    public virtual void VisitNestedTypeNode (NestedTypeNode node) => DefaultVisit (node);

    public virtual void VisitResourceNode (ResourceNode node) => DefaultVisit (node);

    public virtual void VisitTypeNode (TypeNode node) => DefaultVisit (node);

    private void DefaultVisit (NodeBase node)
    {
    }
  }
}