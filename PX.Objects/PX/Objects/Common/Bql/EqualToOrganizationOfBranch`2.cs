// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Bql.EqualToOrganizationOfBranch`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Objects.Common.Bql;

public class EqualToOrganizationOfBranch<TOrganizationIDField, TBranchIDParameter> : 
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where TOrganizationIDField : IBqlOperand
  where TBranchIDParameter : IBqlParameter, new()
{
  private IBqlParameter branchIDParameter;

  protected IBqlParameter BranchIDParameter
  {
    get
    {
      return LazyInitializer.EnsureInitialized<IBqlParameter>(ref this.branchIDParameter, (Func<IBqlParameter>) (() => (IBqlParameter) new TBranchIDParameter()));
    }
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = BqlHelper.GetOperandValue<TOrganizationIDField>(cache, item);
    ref bool? local = ref result;
    int? nullable1 = (int?) value;
    int? organizationId = this.GetOrganizationID(cache.Graph);
    bool? nullable2 = new bool?(nullable1.GetValueOrDefault() == organizationId.GetValueOrDefault() & nullable1.HasValue == organizationId.HasValue);
    local = nullable2;
    pars?.Add(BqlHelper.GetParameterValue(cache.Graph, this.BranchIDParameter));
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (graph == null || !info.BuildExpression)
      return true;
    SQLExpression singleExpression = BqlCommand.GetSingleExpression(typeof (TOrganizationIDField), graph, info.Tables, selection, (BqlCommand.FieldPlace) 0);
    int? organizationId = this.GetOrganizationID(graph);
    exp = SQLExpressionExt.EQ(singleExpression, (object) organizationId);
    return true;
  }

  protected int? GetOrganizationID(PXGraph graph)
  {
    return PXAccess.GetParentOrganizationID((int?) BqlHelper.GetParameterValue(graph, this.BranchIDParameter));
  }
}
