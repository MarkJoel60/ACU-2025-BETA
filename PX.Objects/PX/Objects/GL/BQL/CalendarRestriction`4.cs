// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BQL.CalendarRestriction`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.Common;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Objects.GL.BQL;

[Obsolete]
public class CalendarRestriction<TOrganizationIDField, TBranchIDParameter, TOrganizationIDParameter, TUseMasterCalendarParameter> : 
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where TOrganizationIDField : IBqlOperand
  where TBranchIDParameter : IBqlParameter, new()
  where TOrganizationIDParameter : IBqlParameter, new()
  where TUseMasterCalendarParameter : IBqlParameter, new()
{
  private IBqlParameter branchIDParameter;
  private IBqlParameter organizationIDParameter;
  private IBqlParameter useMasterCalendarParameter;

  protected IBqlParameter BranchIDParameter
  {
    get
    {
      return LazyInitializer.EnsureInitialized<IBqlParameter>(ref this.branchIDParameter, (Func<IBqlParameter>) (() => (IBqlParameter) new TBranchIDParameter()));
    }
  }

  protected IBqlParameter OrganizationIDParameter
  {
    get
    {
      return LazyInitializer.EnsureInitialized<IBqlParameter>(ref this.organizationIDParameter, (Func<IBqlParameter>) (() => (IBqlParameter) new TOrganizationIDParameter()));
    }
  }

  protected IBqlParameter UseMasterCalendarParameter
  {
    get
    {
      return LazyInitializer.EnsureInitialized<IBqlParameter>(ref this.useMasterCalendarParameter, (Func<IBqlParameter>) (() => (IBqlParameter) new TUseMasterCalendarParameter()));
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
    int? idRestrictinValue = this.GetOrganizationIDRestrictinValue(cache.Graph);
    bool? nullable2 = new bool?(nullable1.GetValueOrDefault() == idRestrictinValue.GetValueOrDefault() & nullable1.HasValue == idRestrictinValue.HasValue);
    local = nullable2;
    if (pars == null)
      return;
    pars.Add(BqlHelper.GetParameterValue(cache.Graph, this.BranchIDParameter));
    pars.Add(BqlHelper.GetParameterValue(cache.Graph, this.OrganizationIDParameter));
    pars.Add(BqlHelper.GetParameterValue(cache.Graph, this.UseMasterCalendarParameter));
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    throw new NotImplementedException();
  }

  protected int? GetOrganizationIDRestrictinValue(PXGraph graph)
  {
    return ((bool?) BqlHelper.GetParameterValue(graph, this.UseMasterCalendarParameter)).GetValueOrDefault() ? new int?(0) : (int?) BqlHelper.GetParameterValue(graph, this.BranchIDParameter) ?? (int?) BqlHelper.GetParameterValue(graph, this.OrganizationIDParameter);
  }
}
