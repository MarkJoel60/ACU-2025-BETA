// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUpdateMCAssignment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class INUpdateMCAssignment : 
  PXGraph<INUpdateMCAssignment>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  public PXCancel<UpdateMCAssignmentSettings> Cancel;
  public PXFilter<UpdateMCAssignmentSettings> UpdateSettings;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingOrderBy<UpdateMCAssignmentResult, UpdateMCAssignmentSettings, OrderBy<Asc<UpdateMCAssignmentResult.inventoryID>>> ResultPreview;
  public PXSelect<INItemSite> itemsite;
  public PXSetup<PX.Objects.IN.INSetup> INSetup;

  protected virtual IEnumerable resultPreview() => this.GetMCAssignments();

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public INUpdateMCAssignment()
  {
    ((PXSelectBase) this.ResultPreview).Cache.AllowInsert = false;
    ((PXSelectBase) this.ResultPreview).Cache.AllowDelete = false;
    ((PXSelectBase) this.ResultPreview).Cache.AllowUpdate = true;
    ((PXProcessingBase<UpdateMCAssignmentResult>) this.ResultPreview).SetSelected<UpdateMCAssignmentResult.selected>();
  }

  protected string CalculateNewMCAssignment(INItemSite currentItemSite)
  {
    if (currentItemSite.MovementClassIsFixed.GetValueOrDefault())
      return currentItemSite.MovementClassID;
    PXSelectJoinGroupBy<INItemSiteHistDay, InnerJoin<INItemSiteHistByLastDayInPeriod, On<INItemSiteHistDay.inventoryID, Equal<INItemSiteHistByLastDayInPeriod.inventoryID>, And<INItemSiteHistDay.siteID, Equal<INItemSiteHistByLastDayInPeriod.siteID>, And<INItemSiteHistDay.subItemID, Equal<INItemSiteHistByLastDayInPeriod.subItemID>, And<INItemSiteHistDay.locationID, Equal<INItemSiteHistByLastDayInPeriod.locationID>, And<INItemSiteHistDay.sDate, Equal<INItemSiteHistByLastDayInPeriod.lastActivityDate>>>>>>>, Where<INItemSiteHistDay.siteID, Equal<Current<UpdateMCAssignmentSettings.siteID>>, And<INItemSiteHistDay.inventoryID, Equal<Required<INItemSiteHistDay.inventoryID>>, And<INItemSiteHistByLastDayInPeriod.finPeriodID, LessEqual<Current<UpdateMCAssignmentSettings.endFinPeriodID>>, And<Where<Current<UpdateMCAssignmentSettings.startDateForAvailableQuantity>, IsNull, Or<INItemSiteHistDay.sDate, Greater<Current<UpdateMCAssignmentSettings.startDateForAvailableQuantity>>>>>>>>, Aggregate<Sum<INItemSiteHistDay.endQty, Count<INItemSiteHistByLastDayInPeriod.finPeriodID>>>> selectJoinGroupBy1 = new PXSelectJoinGroupBy<INItemSiteHistDay, InnerJoin<INItemSiteHistByLastDayInPeriod, On<INItemSiteHistDay.inventoryID, Equal<INItemSiteHistByLastDayInPeriod.inventoryID>, And<INItemSiteHistDay.siteID, Equal<INItemSiteHistByLastDayInPeriod.siteID>, And<INItemSiteHistDay.subItemID, Equal<INItemSiteHistByLastDayInPeriod.subItemID>, And<INItemSiteHistDay.locationID, Equal<INItemSiteHistByLastDayInPeriod.locationID>, And<INItemSiteHistDay.sDate, Equal<INItemSiteHistByLastDayInPeriod.lastActivityDate>>>>>>>, Where<INItemSiteHistDay.siteID, Equal<Current<UpdateMCAssignmentSettings.siteID>>, And<INItemSiteHistDay.inventoryID, Equal<Required<INItemSiteHistDay.inventoryID>>, And<INItemSiteHistByLastDayInPeriod.finPeriodID, LessEqual<Current<UpdateMCAssignmentSettings.endFinPeriodID>>, And<Where<Current<UpdateMCAssignmentSettings.startDateForAvailableQuantity>, IsNull, Or<INItemSiteHistDay.sDate, Greater<Current<UpdateMCAssignmentSettings.startDateForAvailableQuantity>>>>>>>>, Aggregate<Sum<INItemSiteHistDay.endQty, Count<INItemSiteHistByLastDayInPeriod.finPeriodID>>>>((PXGraph) this);
    Decimal stockYtdQty = 0M;
    object[] objArray1 = new object[1]
    {
      (object) currentItemSite.InventoryID
    };
    PXResult<INItemSiteHistDay> pxResult1 = PXResultset<INItemSiteHistDay>.op_Implicit(((PXSelectBase<INItemSiteHistDay>) selectJoinGroupBy1).Select(objArray1));
    INItemSiteHistDay inItemSiteHistDay1 = PXResult<INItemSiteHistDay>.op_Implicit(pxResult1);
    int? rowCount = ((PXResult) pxResult1).RowCount;
    int num1 = 0;
    if (!(rowCount.GetValueOrDefault() == num1 & rowCount.HasValue))
    {
      Decimal num2 = stockYtdQty;
      Decimal valueOrDefault = inItemSiteHistDay1.EndQty.GetValueOrDefault();
      rowCount = ((PXResult) pxResult1).RowCount;
      Decimal num3 = (Decimal) rowCount.Value;
      Decimal num4 = valueOrDefault / num3;
      stockYtdQty = num2 + num4;
    }
    PXSelectJoinGroupBy<INItemSiteHistDay, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.startDate, LessEqual<INItemSiteHistDay.sDate>, And<INItemSiteHistDay.sDate, Less<MasterFinPeriod.endDate>>>>, Where<INItemSiteHistDay.siteID, Equal<Current<UpdateMCAssignmentSettings.siteID>>, And<INItemSiteHistDay.inventoryID, Equal<Required<INItemCostHist.inventoryID>>, And<MasterFinPeriod.finPeriodID, LessEqual<Current<UpdateMCAssignmentSettings.endFinPeriodID>>, And<Where<Current<UpdateMCAssignmentSettings.penultimateFinPeriodID>, IsNull, Or<MasterFinPeriod.finPeriodID, Greater<Current<UpdateMCAssignmentSettings.penultimateFinPeriodID>>>>>>>>, Aggregate<Sum<INItemSiteHistDay.qtySales, Count<MasterFinPeriod.finPeriodID>>>> selectJoinGroupBy2 = new PXSelectJoinGroupBy<INItemSiteHistDay, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.startDate, LessEqual<INItemSiteHistDay.sDate>, And<INItemSiteHistDay.sDate, Less<MasterFinPeriod.endDate>>>>, Where<INItemSiteHistDay.siteID, Equal<Current<UpdateMCAssignmentSettings.siteID>>, And<INItemSiteHistDay.inventoryID, Equal<Required<INItemCostHist.inventoryID>>, And<MasterFinPeriod.finPeriodID, LessEqual<Current<UpdateMCAssignmentSettings.endFinPeriodID>>, And<Where<Current<UpdateMCAssignmentSettings.penultimateFinPeriodID>, IsNull, Or<MasterFinPeriod.finPeriodID, Greater<Current<UpdateMCAssignmentSettings.penultimateFinPeriodID>>>>>>>>, Aggregate<Sum<INItemSiteHistDay.qtySales, Count<MasterFinPeriod.finPeriodID>>>>((PXGraph) this);
    Decimal salesByPeriod = 0M;
    object[] objArray2 = new object[1]
    {
      (object) currentItemSite.InventoryID
    };
    PXResult<INItemSiteHistDay> pxResult2 = PXResultset<INItemSiteHistDay>.op_Implicit(((PXSelectBase<INItemSiteHistDay>) selectJoinGroupBy2).Select(objArray2));
    INItemSiteHistDay inItemSiteHistDay2 = PXResult<INItemSiteHistDay>.op_Implicit(pxResult2);
    rowCount = ((PXResult) pxResult2).RowCount;
    int num5 = 0;
    if (!(rowCount.GetValueOrDefault() == num5 & rowCount.HasValue))
    {
      Decimal num6 = salesByPeriod;
      Decimal valueOrDefault = inItemSiteHistDay2.QtySales.GetValueOrDefault();
      rowCount = ((PXResult) pxResult2).RowCount;
      Decimal num7 = (Decimal) rowCount.Value;
      Decimal num8 = valueOrDefault / num7;
      salesByPeriod = num6 + num8;
    }
    if (!(stockYtdQty != 0M) && !(salesByPeriod != 0M))
      return (string) null;
    return PXResultset<INMovementClass>.op_Implicit(((PXSelectBase<INMovementClass>) new PXSelectReadonly<INMovementClass, Where<INMovementClass.maxTurnoverPct, GreaterEqual<Required<INMovementClass.maxTurnoverPct>>>, OrderBy<Asc<INMovementClass.maxTurnoverPct>>>((PXGraph) this)).Select(new object[1]
    {
      (object) INUpdateMCAssignment.MovementToStockRatio(stockYtdQty, salesByPeriod)
    }))?.MovementClassID;
  }

  protected virtual IEnumerable GetMCAssignments()
  {
    UpdateMCAssignmentSettings current = ((PXSelectBase<UpdateMCAssignmentSettings>) this.UpdateSettings).Current;
    PXDelegateResult mcAssignments1 = new PXDelegateResult();
    if ((current != null ? (!current.SiteID.HasValue ? 1 : 0) : 1) != 0 || current == null || current.EndFinPeriodID == null)
      return (IEnumerable) mcAssignments1;
    BqlCommand bqlSelect = ((PXSelectBase) this.ResultPreview).View.BqlSelect;
    List<object> parameters = new List<object>();
    BqlCommand bqlCommand = this.AppendFilter(bqlSelect, (IList<object>) parameters, current);
    int num = 0;
    int startRow = PXView.StartRow;
    List<object> mcAssignments2 = new PXView((PXGraph) this, ((PXSelectBase) this.ResultPreview).View.IsReadOnly, bqlCommand).Select(PXView.Currents, parameters.ToArray(), PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    return (IEnumerable) mcAssignments2;
  }

  public virtual BqlCommand AppendFilter(
    BqlCommand cmd,
    IList<object> parameters,
    UpdateMCAssignmentSettings filter)
  {
    if (filter.ItemClassID.HasValue)
    {
      cmd = cmd.WhereAnd<Where<BqlOperand<UpdateMCAssignmentResult.itemClassID, IBqlInt>.IsEqual<P.AsInt>>>();
      parameters.Add((object) filter.ItemClassID);
    }
    return cmd;
  }

  private static Decimal MovementToStockRatio(Decimal stockYtdQty, Decimal salesByPeriod)
  {
    if (stockYtdQty < 0M)
      stockYtdQty = 0M;
    return !(salesByPeriod == 0M) || !(stockYtdQty != 0M) ? (!(stockYtdQty == 0M) ? salesByPeriod * 100M / stockYtdQty : Decimal.MaxValue) : 0M;
  }

  public virtual void _(PX.Data.Events.RowSelected<UpdateMCAssignmentSettings> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<UpdateMCAssignmentResult>) this.ResultPreview).SetProcessDelegate<INUpdateMCAssignment>(new PXProcessingBase<UpdateMCAssignmentResult>.ProcessItemDelegate<INUpdateMCAssignment>((object) new INUpdateMCAssignment.\u003C\u003Ec__DisplayClass15_0()
    {
      filter = e.Row
    }, __methodptr(\u003C_\u003Eb__0)));
  }

  /// <summary>
  /// Calculates and sets new projected movement class for an inventory item in a warehouse for selected financial period.
  /// </summary>
  protected virtual void SetNewMCAssignment(UpdateMCAssignmentResult item)
  {
    INItemSite currentItemSite = INItemSite.PK.Find((PXGraph) this, item.InventoryID, ((PXSelectBase<UpdateMCAssignmentSettings>) this.UpdateSettings).Current.SiteID, (PKFindOptions) 1);
    string newMcAssignment = this.CalculateNewMCAssignment(currentItemSite);
    currentItemSite.PendingMovementClassID = newMcAssignment;
    currentItemSite.PendingMovementClassPeriodID = ((PXSelectBase<UpdateMCAssignmentSettings>) this.UpdateSettings).Current.EndFinPeriodID;
    currentItemSite.PendingMovementClassUpdateDate = ((PXGraph) this).Accessinfo.BusinessDate;
    ((PXSelectBase<INItemSite>) this.itemsite).Update(currentItemSite);
    item.NewMC = currentItemSite.PendingMovementClassID;
    ((PXSelectBase<UpdateMCAssignmentResult>) this.ResultPreview).Update(item);
  }

  /// <summary>
  /// Saves projected movement class as current movement class for an inventory item in a warehouse.
  /// </summary>
  protected virtual void UpdateMCAssignment(UpdateMCAssignmentResult item)
  {
    INItemSite inItemSite = INItemSite.PK.Find((PXGraph) this, item.InventoryID, ((PXSelectBase<UpdateMCAssignmentSettings>) this.UpdateSettings).Current.SiteID, (PKFindOptions) 1);
    inItemSite.MovementClassID = inItemSite.PendingMovementClassID;
    ((PXSelectBase<INItemSite>) this.itemsite).Update(inItemSite);
    item.OldMC = inItemSite.MovementClassID;
    ((PXSelectBase<UpdateMCAssignmentResult>) this.ResultPreview).Update(item);
  }

  public virtual void _(PX.Data.Events.RowUpdated<UpdateMCAssignmentSettings> e)
  {
    UpdateMCAssignmentSettings row = e.Row;
    UpdateMCAssignmentSettings oldRow = e.OldRow;
    if (row == null || oldRow == null || ((PXSelectBase) this.UpdateSettings).Cache.ObjectsEqual<UpdateMCAssignmentSettings.siteID, UpdateMCAssignmentSettings.endFinPeriodID>((object) row, (object) oldRow))
      return;
    ((PXSelectBase) this.ResultPreview).Cache.Clear();
    ((PXSelectBase) this.ResultPreview).Cache.ClearQueryCache();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<UpdateMCAssignmentSettings, UpdateMCAssignmentSettings.startDateForAvailableQuantity> e)
  {
    if (e == null || e.Row?.EndFinPeriodID == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<UpdateMCAssignmentSettings, UpdateMCAssignmentSettings.startDateForAvailableQuantity>, UpdateMCAssignmentSettings, object>) e).NewValue = (object) (DateTime?) this.FinPeriodRepository.FindOffsetPeriod(e.Row.EndFinPeriodID, -12, new int?(0))?.EndDate;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<UpdateMCAssignmentSettings, UpdateMCAssignmentSettings.penultimateFinPeriodID> e)
  {
    if (e == null || e.Row?.EndFinPeriodID == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<UpdateMCAssignmentSettings, UpdateMCAssignmentSettings.penultimateFinPeriodID>, UpdateMCAssignmentSettings, object>) e).NewValue = (object) this.FinPeriodRepository.FindOffsetPeriod(e.Row.EndFinPeriodID, -1, new int?(0))?.FinPeriodID;
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<UpdateMCAssignmentSettings, UpdateMCAssignmentSettings.endFinPeriodID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<UpdateMCAssignmentSettings, UpdateMCAssignmentSettings.endFinPeriodID>, UpdateMCAssignmentSettings, object>) e).OldValue == e.NewValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<UpdateMCAssignmentSettings, UpdateMCAssignmentSettings.endFinPeriodID>>) e).Cache.SetDefaultExt<UpdateMCAssignmentSettings.startDateForAvailableQuantity>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<UpdateMCAssignmentSettings, UpdateMCAssignmentSettings.endFinPeriodID>>) e).Cache.SetDefaultExt<UpdateMCAssignmentSettings.penultimateFinPeriodID>((object) e.Row);
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    return true;
  }

  public virtual bool RowImporting(string viewName, object row) => row == null;

  public virtual bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public virtual void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
  }
}
