// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMReportMaintGL
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

public class RMReportMaintGL : PXGraphExtension<RMReportMaint>
{
  [PXOverride]
  public virtual bool IsFieldVisible(string field, RMReport report)
  {
    string type = report?.Type;
    if (field.Equals(typeof (RMDataSourceGL.useMasterCalendar).Name, StringComparison.InvariantCultureIgnoreCase))
      return PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>();
    return !(type == "GL") || !field.Equals(typeof (RMDataSourcePM.startAccountGroup).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourcePM.endAccountGroup).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourcePM.startProject).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourcePM.endProject).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourcePM.startProjectTask).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourcePM.endProjectTask).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourcePM.startInventory).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourcePM.endInventory).Name, StringComparison.InvariantCultureIgnoreCase);
  }

  protected void RMReport_RowSelected(PXCache sender, PXRowSelectedEventArgs e, PXRowSelected del)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<RMReportGL.requestAccountClassID>(sender, e.Row, ((RMReport) e.Row).Type == "GL");
    PXUIFieldAttribute.SetVisible<RMReportGL.requestEndAccount>(sender, e.Row, ((RMReport) e.Row).Type == "GL");
    PXUIFieldAttribute.SetVisible<RMReportGL.requestEndSub>(sender, e.Row, ((RMReport) e.Row).Type == "GL");
    PXUIFieldAttribute.SetVisible<RMReportGL.requestLedgerID>(sender, e.Row, ((RMReport) e.Row).Type == "GL");
    PXUIFieldAttribute.SetVisible<RMReportGL.requestStartAccount>(sender, e.Row, ((RMReport) e.Row).Type == "GL");
    PXUIFieldAttribute.SetVisible<RMReportGL.requestStartSub>(sender, e.Row, ((RMReport) e.Row).Type == "GL");
    PXUIFieldAttribute.SetVisible<RMReportGL.requestStartBranch>(sender, e.Row, ((RMReport) e.Row).Type == "GL");
    PXUIFieldAttribute.SetVisible<RMReportGL.requestEndBranch>(sender, e.Row, ((RMReport) e.Row).Type == "GL");
    PXUIFieldAttribute.SetVisible<RMReportGL.requestOrganizationID>(sender, e.Row, ((RMReport) e.Row).Type == "GL");
    PXUIFieldAttribute.SetVisible<RMReportGL.requestUseMasterCalendar>(sender, e.Row, ((RMReport) e.Row).Type == "GL" && PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>());
    del.Invoke(sender, e);
  }

  [PXOverride]
  public virtual PXFieldState CreateAmountTypeState(
    object returnValue,
    Func<object, PXFieldState> baseMethod)
  {
    if (((PXSelectBase<RMReport>) this.Base.Report).Current.Type != "GL")
      return baseMethod(returnValue);
    int[] array = new int[11]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      21,
      22,
      23,
      24,
      25
    };
    return PXIntState.CreateInstance(returnValue == null || Array.IndexOf<int>(array, int.Parse(returnValue.ToString())) < 0 ? (object) (short) 0 : returnValue, typeof (RMDataSource.amountType).Name, new bool?(false), new int?(0), new int?(), new int?(), array, new string[11]
    {
      Messages.GetLocal("Not Set"),
      Messages.GetLocal("Turnover"),
      Messages.GetLocal("Credit"),
      Messages.GetLocal("Debit"),
      Messages.GetLocal("Beg. Balance"),
      Messages.GetLocal("Ending Balance"),
      Messages.GetLocal("Curr. Turnover"),
      Messages.GetLocal("Curr. Credit"),
      Messages.GetLocal("Curr. Debit"),
      Messages.GetLocal("Curr. Beg. Balance"),
      Messages.GetLocal("Curr. Ending Balance")
    }, typeof (short), new int?(0), (string[]) null, new bool?());
  }

  [PXOverride]
  public virtual void dataSourceFieldSelecting(PXFieldSelectingEventArgs e, string field)
  {
    RMReport row = (RMReport) e.Row;
    if (row == null || !(row.Type == "GL") || !field.Equals(typeof (RMDataSource.amountType).Name, StringComparison.InvariantCultureIgnoreCase))
      return;
    e.ReturnState = (object) this.CreateAmountTypeState(e.ReturnValue, (Func<object, PXFieldState>) null);
    ((PXFieldState) e.ReturnState).DisplayName = Messages.GetLocal("Amount Type");
  }
}
