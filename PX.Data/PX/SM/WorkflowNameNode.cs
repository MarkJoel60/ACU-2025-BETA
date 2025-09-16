// Decompiled with JetBrains decompiler
// Type: PX.SM.WorkflowNameNode
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Extensions;
using PX.Common.Parser;
using PX.Data;
using PX.Data.ProjectDefinition.Workflow;

#nullable disable
namespace PX.SM;

public class WorkflowNameNode(ExpressionNode node, string name, WorkflowExpressionContext context) : 
  NameNode(node, name, (ParserContext) context)
{
  public virtual object Eval(object row)
  {
    if (this.RawName.StartsWith("@"))
    {
      switch (this.RawName)
      {
        case "@me":
          return WorkflowFieldExpressionEvaluator.GetCurrentUserOrContact(((WorkflowExpressionContext) this.context).Cache, ((WorkflowExpressionContext) this.context).FieldName);
        case "@mygroups":
          return (object) UserGroupLazyCache.Current.GetUserGroupIds(PXAccess.GetUserID());
        case "@myworktree":
          return (object) UserGroupLazyCache.Current.GetUserWorkTreeIds(PXAccess.GetUserID());
        case "@branch":
          return (object) WorkflowFieldExpressionEvaluator.GetCurrentBranch();
        case "@company":
          return (object) PXAccess.GetBranchIDsForCurrentOrganization();
        default:
          return ((WorkflowExpressionContext) this.context).GetGraphParameterValue(this.RawName.Substring(1));
      }
    }
    else
    {
      if (this.RawName.StartsWith("[") && this.RawName.EndsWith("]") && this.RawName.Contains("."))
        return ((WorkflowExpressionContext) this.context).GetFormValue(StringExtensions.Segment(this.Name, '.', (ushort) 1));
      return this.RawName.StartsWith("[") && this.RawName.EndsWith("]") ? ((WorkflowExpressionContext) this.context).GetFormCache(this.Name, row) : base.Eval(row);
    }
  }
}
