// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMReportMaintPM
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

public class RMReportMaintPM : PXGraphExtension<RMReportMaintGL, RMReportMaint>
{
  protected void RMReport_RowSelected(PXCache sender, PXRowSelectedEventArgs e, PXRowSelected del)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<RMReportPM.requestStartAccountGroup>(sender, e.Row, ((RMReport) e.Row).Type == "PM");
    PXUIFieldAttribute.SetVisible<RMReportPM.requestEndAccountGroup>(sender, e.Row, ((RMReport) e.Row).Type == "PM");
    PXUIFieldAttribute.SetVisible<RMReportPM.requestStartProject>(sender, e.Row, ((RMReport) e.Row).Type == "PM");
    PXUIFieldAttribute.SetVisible<RMReportPM.requestEndProject>(sender, e.Row, ((RMReport) e.Row).Type == "PM");
    PXUIFieldAttribute.SetVisible<RMReportPM.requestStartProjectTask>(sender, e.Row, ((RMReport) e.Row).Type == "PM");
    PXUIFieldAttribute.SetVisible<RMReportPM.requestEndProjectTask>(sender, e.Row, ((RMReport) e.Row).Type == "PM");
    PXUIFieldAttribute.SetVisible<RMReportPM.requestStartInventory>(sender, e.Row, ((RMReport) e.Row).Type == "PM");
    PXUIFieldAttribute.SetVisible<RMReportPM.requestEndInventory>(sender, e.Row, ((RMReport) e.Row).Type == "PM");
    del.Invoke(sender, e);
  }

  [PXOverride]
  public virtual bool IsFieldVisible(
    string field,
    RMReport report,
    Func<string, RMReport, bool> baseMethod)
  {
    return (!(report?.Type == "PM") || !field.Equals(typeof (RMDataSourceGL.ledgerID).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourceGL.accountClassID).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourceGL.startAccount).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourceGL.endAccount).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourceGL.startSub).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourceGL.endSub).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourceGL.organizationID).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourceGL.useMasterCalendar).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourceGL.startBranch).Name, StringComparison.InvariantCultureIgnoreCase) && !field.Equals(typeof (RMDataSourceGL.endBranch).Name, StringComparison.InvariantCultureIgnoreCase)) && baseMethod(field, report);
  }

  [PXOverride]
  public virtual PXFieldState CreateAmountTypeState(
    object returnValue,
    Func<object, PXFieldState> baseMethod)
  {
    if (((PXSelectBase<RMReport>) ((PXGraphExtension<RMReportMaint>) this).Base.Report).Current.Type != "PM")
      return this.Base1.CreateAmountTypeState(returnValue, baseMethod);
    int[] array = new int[18]
    {
      0,
      6,
      7,
      12,
      13,
      8,
      9,
      10,
      11,
      36,
      37,
      26,
      27,
      28,
      29,
      31 /*0x1F*/,
      32 /*0x20*/,
      33
    };
    return PXIntState.CreateInstance(returnValue == null || Array.IndexOf<int>(array, int.Parse(returnValue.ToString())) < 0 ? (object) (short) 0 : returnValue, typeof (RMDataSource.amountType).Name, new bool?(false), new int?(0), new int?(), new int?(), array, new string[18]
    {
      Messages.GetLocal("Not Set"),
      Messages.GetLocal("Actual Amount"),
      Messages.GetLocal("Actual Quantity"),
      Messages.GetLocal("Amount Turnover"),
      Messages.GetLocal("Quantity Turnover"),
      Messages.GetLocal("Original Budgeted Amount"),
      Messages.GetLocal("Original Budgeted Quantity"),
      Messages.GetLocal("Revised Budgeted Amount"),
      Messages.GetLocal("Revised Budgeted Quantity"),
      Messages.GetLocal("Original Committed Amount"),
      Messages.GetLocal("Original Committed Quantity"),
      Messages.GetLocal("Revised Committed Amount"),
      Messages.GetLocal("Revised Committed Quantity"),
      Messages.GetLocal("Committed Open Amount"),
      Messages.GetLocal("Committed Open Quantity"),
      Messages.GetLocal("Committed Received Quantity"),
      Messages.GetLocal("Committed Invoiced Amount"),
      Messages.GetLocal("Committed Invoiced Quantity")
    }, typeof (short), new int?(0), (string[]) null, new bool?());
  }

  [PXOverride]
  public virtual void dataSourceFieldSelecting(PXFieldSelectingEventArgs e, string field)
  {
    RMReport row = (RMReport) e.Row;
    PXResultset<RMDataSource> pxResultset;
    if (row == null)
      pxResultset = (PXResultset<RMDataSource>) null;
    else
      pxResultset = ((PXSelectBase<RMDataSource>) ((PXGraphExtension<RMReportMaint>) this).Base.DataSourceByID).Select(new object[1]
      {
        (object) row.DataSourceID
      });
    RMDataSource rmDataSource = PXResultset<RMDataSource>.op_Implicit(pxResultset);
    if (rmDataSource == null)
    {
      object obj;
      if (((PXSelectBase) ((PXGraphExtension<RMReportMaint>) this).Base.DataSourceByID).Cache.RaiseFieldDefaulting(field, (object) null, ref obj))
        ((PXSelectBase) ((PXGraphExtension<RMReportMaint>) this).Base.DataSourceByID).Cache.RaiseFieldUpdating(field, (object) null, ref obj);
      ((PXSelectBase) ((PXGraphExtension<RMReportMaint>) this).Base.DataSourceByID).Cache.RaiseFieldSelecting(field, (object) null, ref obj, true);
      e.ReturnState = obj;
    }
    else
      e.ReturnState = ((PXSelectBase) ((PXGraphExtension<RMReportMaint>) this).Base.DataSourceByID).Cache.GetStateExt((object) rmDataSource, field);
    if (row != null && row.Type == "PM" && field.Equals(typeof (RMDataSource.amountType).Name, StringComparison.InvariantCultureIgnoreCase))
    {
      e.ReturnState = (object) this.CreateAmountTypeState(e.ReturnValue, (Func<object, PXFieldState>) null);
      ((PXFieldState) e.ReturnState).DisplayName = Messages.GetLocal("Amount Type");
    }
    this.Base1.dataSourceFieldSelecting(e, field);
    if (!(e.ReturnState is PXFieldState))
      return;
    ((PXFieldState) e.ReturnState).SetFieldName("DataSource" + field);
    ((PXFieldState) e.ReturnState).Visible = ((PXGraphExtension<RMReportMaint>) this).Base.IsFieldVisible(field, row);
  }
}
