// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.InterBranchRestrictorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.Common;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true)]
public class InterBranchRestrictorAttribute : PXRestrictorAttribute
{
  private static readonly Type EmptyWhere = typeof (Where<True, Equal<True>>);
  protected Type _interBranchWhere;

  public InterBranchRestrictorAttribute(Type where)
    : base(InterBranchRestrictorAttribute.EmptyWhere, "Inter-Branch Transactions feature is disabled.", Array.Empty<Type>())
  {
    this._interBranchWhere = where;
  }

  protected virtual BqlCommand WhereAnd(PXCache sender, PXSelectorAttribute selattr, Type Where)
  {
    Type type = this.IsReportOrInterBranchFeatureEnabled(sender) ? InterBranchRestrictorAttribute.EmptyWhere : this._interBranchWhere;
    return base.WhereAnd(sender, selattr, type);
  }

  private bool IsReportOrInterBranchFeatureEnabled(PXCache sender)
  {
    return this.IsReportGraph(sender.Graph) || PXAccess.FeatureInstalled<FeaturesSet.interBranch>();
  }

  protected bool IsReportGraph(PXGraph graph) => graph.GetType() == typeof (PXGraph);
}
