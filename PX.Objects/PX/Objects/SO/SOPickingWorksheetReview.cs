// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickingWorksheetReview
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Objects.SO;

public class SOPickingWorksheetReview : PXGraph<
#nullable disable
SOPickingWorksheetReview>
{
  public PXSetup<SOSetup> setup;
  public PXSetup<SOPickPackShipSetup>.Where<BqlOperand<
  #nullable enable
  SOPickPackShipSetup.branchID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.branchID, IBqlInt>.FromCurrent>> pickSetup;
  public 
  #nullable disable
  FbqlSelect<SelectFromBase<SOPickingWorksheet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  SOPickingWorksheet.worksheetType, IBqlString>.IsNotEqual<
  #nullable disable
  SOPickingWorksheet.worksheetType.single>>, SOPickingWorksheet>.View worksheet;
  public FbqlSelect<SelectFromBase<SOPickingWorksheetLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INLocation>.On<SOPickingWorksheetLine.FK.Location>>>.Where<KeysRelation<Field<SOPickingWorksheetLine.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPickingWorksheetLine>, SOPickingWorksheet, SOPickingWorksheetLine>.SameAsCurrent>.Order<By<BqlField<
  #nullable enable
  INLocation.pathPriority, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  INLocation.locationCD, IBqlString>.Asc>>, 
  #nullable disable
  SOPickingWorksheetLine>.View worksheetLines;
  public FbqlSelect<SelectFromBase<SOPickingWorksheetLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INLocation>.On<SOPickingWorksheetLineSplit.FK.Location>>>.Where<KeysRelation<CompositeKey<Field<SOPickingWorksheetLineSplit.worksheetNbr>.IsRelatedTo<SOPickingWorksheetLine.worksheetNbr>, Field<SOPickingWorksheetLineSplit.lineNbr>.IsRelatedTo<SOPickingWorksheetLine.lineNbr>>.WithTablesOf<SOPickingWorksheetLine, SOPickingWorksheetLineSplit>, SOPickingWorksheetLine, SOPickingWorksheetLineSplit>.SameAsCurrent.And<BqlOperand<
  #nullable enable
  SOPickingWorksheetLineSplit.isUnassigned, IBqlBool>.IsEqual<
  #nullable disable
  False>>>.Order<By<BqlField<
  #nullable enable
  INLocation.pathPriority, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  INLocation.locationCD, IBqlString>.Asc>>, 
  #nullable disable
  SOPickingWorksheetLineSplit>.View worksheetLineSplits;
  public FbqlSelect<SelectFromBase<SOPickingWorksheetShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<SOPickingWorksheetShipment.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPickingWorksheetShipment>, SOPickingWorksheet, SOPickingWorksheetShipment>.SameAsCurrent>, SOPickingWorksheetShipment>.View shipmentLinks;
  public FbqlSelect<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<SOShipment.currentWorksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOShipment>, SOPickingWorksheet, SOShipment>.SameAsCurrent>, SOShipment>.View shipments;
  public FbqlSelect<SelectFromBase<SOPicker, TypeArrayOf<IFbqlJoin>.Empty>, SOPicker>.View shipmentPickers;
  public FbqlSelect<SelectFromBase<SOPicker, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPickerListEntry>.On<SOPickerListEntry.FK.Picker>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOPickerListEntry.shipmentNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  SOPickingWorksheetShipment.shipmentNbr, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  KeysRelation<Field<SOPicker.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPicker>, SOPickingWorksheet, SOPicker>.SameAsCurrent>>.Aggregate<To<GroupBy<SOPicker.worksheetNbr>, GroupBy<SOPicker.pickerNbr>>>, SOPicker>.View shipmentPickersForWave;
  public FbqlSelect<SelectFromBase<SOPicker, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPickerListEntry>.On<SOPickerListEntry.FK.Picker>>, FbqlJoins.Inner<SOShipLineSplit>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOShipLineSplit.inventoryID, 
  #nullable disable
  Equal<SOPickerListEntry.inventoryID>>>>, And<BqlOperand<
  #nullable enable
  SOShipLineSplit.subItemID, IBqlInt>.IsEqual<
  #nullable disable
  SOPickerListEntry.subItemID>>>, And<BqlOperand<
  #nullable enable
  SOShipLineSplit.siteID, IBqlInt>.IsEqual<
  #nullable disable
  SOPickerListEntry.siteID>>>, And<BqlOperand<
  #nullable enable
  SOShipLineSplit.locationID, IBqlInt>.IsEqual<
  #nullable disable
  SOPickerListEntry.locationID>>>>.And<AreSame<SOShipLineSplit.lotSerialNbr, SOPickerListEntry.lotSerialNbr>>>>>.Where<KeysRelation<Field<SOPicker.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPicker>, SOPickingWorksheet, SOPicker>.SameAsCurrent.And<BqlOperand<
  #nullable enable
  SOShipLineSplit.shipmentNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SOPickingWorksheetShipment.shipmentNbr, IBqlString>.FromCurrent>>>.Aggregate<
  #nullable disable
  To<GroupBy<SOPicker.worksheetNbr>, GroupBy<SOPicker.pickerNbr>>>, SOPicker>.View shipmentPickersForBatch;
  public FbqlSelect<SelectFromBase<SOPicker, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<SOPicker.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPicker>, SOPickingWorksheet, SOPicker>.SameAsCurrent>, SOPicker>.View pickers;
  public FbqlSelect<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocation>.On<SOPickerListEntry.FK.Location>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<SOPickerListEntry.FK.InventoryItem>>>.Where<KeysRelation<CompositeKey<Field<SOPickerListEntry.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerListEntry.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerListEntry>, SOPicker, SOPickerListEntry>.SameAsCurrent>.Aggregate<To<GroupBy<SOPickerListEntry.siteID>, GroupBy<SOPickerListEntry.locationID>, GroupBy<SOPickerListEntry.inventoryID>, GroupBy<SOPickerListEntry.subItemID>, GroupBy<SOPickerListEntry.lotSerialNbr>, Sum<SOPickerListEntry.qty>, Sum<SOPickerListEntry.baseQty>, Sum<SOPickerListEntry.pickedQty>, Sum<SOPickerListEntry.basePickedQty>>>.Order<By<BqlField<
  #nullable enable
  INLocation.pathPriority, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  INLocation.locationCD, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryCD, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  SOPickerListEntry.lotSerialNbr, IBqlString>.Asc>>, 
  #nullable disable
  SOPickerListEntry>.View pickerList;
  public FbqlSelect<SelectFromBase<SOPickerToShipmentLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOPickerToShipmentLink.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerToShipmentLink.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerToShipmentLink>, SOPicker, SOPickerToShipmentLink>.SameAsCurrent>.Aggregate<To<GroupBy<SOPickerToShipmentLink.worksheetNbr>, GroupBy<SOPickerToShipmentLink.pickerNbr>, GroupBy<SOPickerToShipmentLink.siteID>, GroupBy<SOPickerToShipmentLink.shipmentNbr>>>, SOPickerToShipmentLink>.View pickerShipments;
  public FbqlSelect<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocation>.On<SOPickerListEntry.FK.Location>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<SOPickerListEntry.FK.InventoryItem>>>.Where<KeysRelation<CompositeKey<Field<SOPickerListEntry.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerListEntry.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerListEntry>, SOPicker, SOPickerListEntry>.SameAsCurrent.And<BqlOperand<
  #nullable enable
  SOPickerListEntry.shipmentNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SOPickerToShipmentLink.shipmentNbr, IBqlString>.FromCurrent>>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  INLocation.pathPriority, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  INLocation.locationCD, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryCD, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  SOPickerListEntry.lotSerialNbr, IBqlString>.Asc>>, 
  #nullable disable
  SOPickerListEntry>.View pickerListByShipment;
  public FbqlSelect<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOPickingJob.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickingJob.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickingJob>, SOPicker, SOPickingJob>.SameAsCurrent>, SOPickingJob>.View pickerJob;
  public PXCancel<SOPickingWorksheet> Cancel;
  public PXSave<SOPickingWorksheet> Save;
  public PXDelete<SOPickingWorksheet> Delete;
  public PXFirst<SOPickingWorksheet> First;
  public PXPrevious<SOPickingWorksheet> Prev;
  public PXNext<SOPickingWorksheet> Next;
  public PXLast<SOPickingWorksheet> Last;
  public PXInitializeState<SOPickingWorksheet> InitializeState;
  public PXAction<SOPickingWorksheet> PutOnHold;
  public PXAction<SOPickingWorksheet> ReleaseFromHold;
  public PXAction<SOPickingWorksheet> ShowSplits;
  public PXAction<SOPickingWorksheet> ShowPickers;
  public PXAction<SOPickingWorksheet> ShowShipments;
  public PXAction<SOPickingWorksheet> ShowPickList;
  public PXAction<SOPickingWorksheet> UnlinkAllShipments;
  public PXAction<SOPickingWorksheet> CancelWorksheet;
  public PXAction<SOPickingWorksheet> PrintPickList;
  public PXAction<SOPickingWorksheet> PrintPackSlips;

  public virtual IEnumerable ShipmentPickers()
  {
    if (((PXSelectBase<SOPickingWorksheet>) this.worksheet).Current?.WorksheetType == "BT")
      return (IEnumerable) GraphHelper.RowCast<SOPicker>((IEnumerable) ((IEnumerable<PXResult<SOPicker>>) ((PXSelectBase<SOPicker>) this.shipmentPickersForBatch).Select(Array.Empty<object>())).AsEnumerable<PXResult<SOPicker>>()).ToArray<SOPicker>();
    return ((PXSelectBase<SOPickingWorksheet>) this.worksheet).Current?.WorksheetType == "WV" ? (IEnumerable) GraphHelper.RowCast<SOPicker>((IEnumerable) ((IEnumerable<PXResult<SOPicker>>) ((PXSelectBase<SOPicker>) this.shipmentPickersForWave).Select(Array.Empty<object>())).AsEnumerable<PXResult<SOPicker>>()).ToArray<SOPicker>() : (IEnumerable) Array.Empty<SOPicker>();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable putOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected virtual IEnumerable releaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXButton]
  [PXUIField(DisplayName = "Line Details")]
  public virtual void showSplits()
  {
    ((PXSelectBase<SOPickingWorksheetLineSplit>) this.worksheetLineSplits).AskExt();
  }

  [PXButton]
  [PXUIField(DisplayName = "View Pickers")]
  public virtual void showPickers() => ((PXSelectBase<SOPicker>) this.shipmentPickers).AskExt();

  [PXButton]
  [PXUIField(DisplayName = "View Shipments")]
  public virtual void showShipments()
  {
    ((PXSelectBase<SOPickerToShipmentLink>) this.pickerShipments).AskExt();
  }

  [PXButton]
  [PXUIField(DisplayName = "View Pick List")]
  public virtual void showPickList()
  {
    ((PXSelectBase<SOPickerListEntry>) this.pickerList).AskExt();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Unlink All Shipments")]
  public virtual IEnumerable unlinkAllShipments(PXAdapter a)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOPickingWorksheetReview.\u003C\u003Ec__DisplayClass36_0 cDisplayClass360 = new SOPickingWorksheetReview.\u003C\u003Ec__DisplayClass36_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass360.worksheets = a.Get<SOPickingWorksheet>().ToArray<SOPickingWorksheet>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass360, __methodptr(\u003CunlinkAllShipments\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass360.worksheets;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Cancel Worksheet")]
  public virtual IEnumerable cancelWorksheet(PXAdapter a)
  {
    if (((PXSelectBase<SOPickingWorksheet>) this.worksheet).Ask("Warning", "You are about to cancel the picking worksheet. All the related pick lists will be canceled, and the picked quantity will be set to zero in the related shipments. The picked items should be moved to their initial locations manually by the warehouse staff; there is no automated guidance for this operation.", (MessageButtons) 1, (MessageIcon) 3) == 1)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SOPickingWorksheetReview.\u003C\u003Ec__DisplayClass38_0 cDisplayClass380 = new SOPickingWorksheetReview.\u003C\u003Ec__DisplayClass38_0();
      ((PXAction) this.Save).Press();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass380.worksheetNbr = ((PXSelectBase<SOPickingWorksheet>) this.worksheet).Current.WorksheetNbr;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass380, __methodptr(\u003CcancelWorksheet\u003Eb__0)));
    }
    return a.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Print Pick Lists")]
  public virtual IEnumerable printPickList(PXAdapter a)
  {
    SOPickingWorksheet[] worksheets = a.Get<SOPickingWorksheet>().ToArray<SOPickingWorksheet>();
    if (!((IEnumerable<SOPickingWorksheet>) worksheets).Any<SOPickingWorksheet>())
      return (IEnumerable) worksheets;
    PrintSettings printerSettings = PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() ? SMPrintJobMaint.GetPrintSettings(a, new Func<string, string, int?, Guid?>(new NotificationUtility((PXGraph) this).SearchPrinter), "Customer", "SO644006", ((PXGraph) this).Accessinfo.BranchID) : (PrintSettings) null;
    ((PXGraph) this).LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (async ct =>
    {
      PXReportRequiredException report = (PXReportRequiredException) null;
      SOPickingWorksheetReview worksheetReview = PXGraph.CreateInstance<SOPickingWorksheetReview>();
      SOPickingWorksheet[] pickingWorksheetArray = worksheets;
      for (int index = 0; index < pickingWorksheetArray.Length; ++index)
      {
        PXReportRequiredException requiredException = await worksheetReview.PrintPickListImpl(pickingWorksheetArray[index], printerSettings, ct);
        if (requiredException != null)
        {
          if (report == null)
            report = requiredException;
          else
            report.AddSibling(requiredException.ReportID, requiredException.Parameters);
        }
      }
      pickingWorksheetArray = (SOPickingWorksheet[]) null;
      report = report == null ? (PXReportRequiredException) null : throw report;
      worksheetReview = (SOPickingWorksheetReview) null;
    }));
    return a.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Print Packing Slips")]
  public virtual IEnumerable printPackSlips(PXAdapter a)
  {
    SOPickingWorksheet[] worksheets = a.Get<SOPickingWorksheet>().ToArray<SOPickingWorksheet>();
    if (!((IEnumerable<SOPickingWorksheet>) worksheets).Any<SOPickingWorksheet>())
      return (IEnumerable) worksheets;
    ((PXGraph) this).LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (async ct =>
    {
      PXReportRequiredException report = (PXReportRequiredException) null;
      SOPickingWorksheetReview worksheetReview = PXGraph.CreateInstance<SOPickingWorksheetReview>();
      SOPickingWorksheet[] pickingWorksheetArray = worksheets;
      for (int index = 0; index < pickingWorksheetArray.Length; ++index)
      {
        PXReportRequiredException requiredException = await worksheetReview.PrintPackSlipsImpl(pickingWorksheetArray[index], ct);
        if (requiredException != null)
        {
          if (report == null)
            report = requiredException;
          else
            report.AddSibling(requiredException.ReportID, requiredException.Parameters);
        }
      }
      pickingWorksheetArray = (SOPickingWorksheet[]) null;
      report = report == null ? (PXReportRequiredException) null : throw report;
      worksheetReview = (SOPickingWorksheetReview) null;
    }));
    return a.Get();
  }

  public SOPickingWorksheetReview()
  {
    ((PXSelectBase) this.worksheet).Cache.AllowInsert = false;
    ((PXSelectBase) this.worksheet).Cache.AllowDelete = false;
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [PXParent(typeof (SOShipment.FK.Worksheet), LeaveChildren = true)]
  [PXDBDefault(typeof (SOPickingWorksheet.worksheetNbr))]
  protected void _(
    PX.Data.Events.CacheAttached<SOShipment.currentWorksheetNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (INTote.UnassignableToteAttribute), "IsKey", false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOPickerToShipmentLink.toteID> e)
  {
  }

  protected void _(PX.Data.Events.RowSelected<SOPickingWorksheet> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase) this.worksheet).Cache.AllowDelete = EnumerableExtensions.IsIn<string>(e.Row.Status, "H", "N") || EnumerableExtensions.IsIn<string>(e.Row.Status, "C", "L") && ((IEnumerable<SOPickingWorksheetShipment>) ((PXSelectBase<SOPickingWorksheetShipment>) this.shipmentLinks).SelectMain(Array.Empty<object>())).All<SOPickingWorksheetShipment>((Func<SOPickingWorksheetShipment, bool>) (sh => sh.Unlinked.GetValueOrDefault()));
    PXCacheEx.AdjustUI(((PXSelectBase) this.worksheetLineSplits).Cache, (object) null).For<SOPickingWorksheetLineSplit.sortingLocationID>((Action<PXUIFieldAttribute>) (a => a.Visible = e.Row.WorksheetType == "BT"));
    PXCacheEx.AdjustUI(((PXSelectBase) this.pickers).Cache, (object) null).For<SOPicker.sortingLocationID>((Action<PXUIFieldAttribute>) (a => a.Visible = e.Row.WorksheetType == "BT"));
    ((PXAction) this.ShowShipments).SetVisible(e.Row.WorksheetType == "WV");
    ((PXAction) this.UnlinkAllShipments).SetVisible(e.Row.WorksheetType == "WV");
    ((PXAction) this.CancelWorksheet).SetEnabled(e.Row.WorksheetType == "WV" || e.Row.WorksheetType == "BT" && ((IEnumerable<SOPickingWorksheetShipment>) ((PXSelectBase<SOPickingWorksheetShipment>) this.shipmentLinks).SelectMain(Array.Empty<object>())).All<SOPickingWorksheetShipment>((Func<SOPickingWorksheetShipment, bool>) (sh =>
    {
      bool? picked = sh.Picked;
      bool flag = false;
      return picked.GetValueOrDefault() == flag & picked.HasValue || sh.Unlinked.GetValueOrDefault();
    })));
    ((PXAction) this.PrintPackSlips).SetEnabled(e.Row.WorksheetType == "WV" || e.Row.WorksheetType == "BT" && ((IEnumerable<SOPickingWorksheetShipment>) ((PXSelectBase<SOPickingWorksheetShipment>) this.shipmentLinks).SelectMain(Array.Empty<object>())).Any<SOPickingWorksheetShipment>((Func<SOPickingWorksheetShipment, bool>) (sh =>
    {
      if (!sh.Picked.GetValueOrDefault())
        return false;
      bool? unlinked = sh.Unlinked;
      bool flag = false;
      return unlinked.GetValueOrDefault() == flag & unlinked.HasValue;
    })));
  }

  private async Task<PXReportRequiredException> PrintPickListImpl(
    SOPickingWorksheet ws,
    PrintSettings printerSettings,
    CancellationToken cancellationToken)
  {
    SOPickingWorksheetReview graph = this;
    ((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current = (SOPickingWorksheet) PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.Find((PXGraph) graph, (SOPickingWorksheet.worksheetNbr) ws, (PKFindOptions) 1);
    PXReportRequiredException report = (PXReportRequiredException) null;
    bool? nullable;
    if (((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current.WorksheetType == "BT")
      report = new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["WorksheetNbr"] = ((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current.WorksheetNbr
      }, "SO644006", (CurrentLocalization) null);
    else if (((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current.WorksheetType == "WV")
    {
      nullable = ((PXSelectBase<SOPickPackShipSetup>) graph.pickSetup).Current.PrintPickListsAndPackSlipsTogether;
      if (nullable.GetValueOrDefault())
      {
        foreach (PXResult<SOPicker> pxResult1 in ((PXSelectBase<SOPicker>) graph.pickers).Select(Array.Empty<object>()))
        {
          SOPicker soPicker = PXResult<SOPicker>.op_Implicit(pxResult1);
          ((PXSelectBase<SOPicker>) graph.pickers).Current = soPicker;
          Dictionary<string, string> dictionary = new Dictionary<string, string>()
          {
            ["WorksheetNbr"] = ((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current.WorksheetNbr,
            ["PickerNbr"] = soPicker.PickerNbr.ToString()
          };
          if (report == null)
            report = new PXReportRequiredException(dictionary, "SO644006", (CurrentLocalization) null);
          else
            report.AddSibling("SO644006", dictionary);
          foreach (PXResult<SOPickerToShipmentLink> pxResult2 in ((PXSelectBase<SOPickerToShipmentLink>) graph.pickerShipments).Select(Array.Empty<object>()))
          {
            SOPickerToShipmentLink pickerToShipmentLink = PXResult<SOPickerToShipmentLink>.op_Implicit(pxResult2);
            report.AddSibling("SO644007", new Dictionary<string, string>()
            {
              ["WorksheetNbr"] = ((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current.WorksheetNbr,
              ["ShipmentNbr"] = pickerToShipmentLink.ShipmentNbr
            });
          }
        }
      }
      else
        report = new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["WorksheetNbr"] = ((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current.WorksheetNbr
        }, "SO644006", (CurrentLocalization) null);
    }
    else if (((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current.WorksheetType == "SS")
      report = new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["WorksheetNbr"] = ((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current.WorksheetNbr
      }, "SO644006", (CurrentLocalization) null);
    if (report != null && printerSettings != null)
    {
      nullable = printerSettings.PrintWithDeviceHub;
      if (!nullable.GetValueOrDefault())
      {
        printerSettings.PrintWithDeviceHub = new bool?(true);
        printerSettings.DefinePrinterManually = new bool?(true);
        printerSettings.PrinterID = new NotificationUtility((PXGraph) graph).SearchPrinter("Customer", "SO644006", ((PXGraph) graph).Accessinfo.BranchID);
        printerSettings.NumberOfCopies = new int?(1);
      }
      int num = await SMPrintJobMaint.CreatePrintJobGroup(printerSettings, report, (string) null, cancellationToken) ? 1 : 0;
    }
    PXReportRequiredException requiredException = report;
    report = (PXReportRequiredException) null;
    return requiredException;
  }

  private async Task<PXReportRequiredException> PrintPackSlipsImpl(
    SOPickingWorksheet ws,
    CancellationToken cancellationToken)
  {
    SOPickingWorksheetReview graph = this;
    ((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current = (SOPickingWorksheet) PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.Find((PXGraph) graph, (SOPickingWorksheet.worksheetNbr) ws, (PKFindOptions) 1);
    PXReportRequiredException report = (PXReportRequiredException) null;
    if (((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current.WorksheetType == "BT")
    {
      foreach (PXResult<SOPickingWorksheetShipment> pxResult in ((PXSelectBase<SOPickingWorksheetShipment>) graph.shipmentLinks).Select(Array.Empty<object>()))
      {
        SOPickingWorksheetShipment worksheetShipment = PXResult<SOPickingWorksheetShipment>.op_Implicit(pxResult);
        bool? picked = worksheetShipment.Picked;
        bool flag = false;
        if (!(picked.GetValueOrDefault() == flag & picked.HasValue))
        {
          Dictionary<string, string> dictionary = new Dictionary<string, string>()
          {
            ["WorksheetNbr"] = ((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current.WorksheetNbr,
            ["ShipmentNbr"] = worksheetShipment.ShipmentNbr
          };
          if (report == null)
            report = new PXReportRequiredException(dictionary, "SO644005", (CurrentLocalization) null);
          else
            report.AddSibling("SO644005", dictionary);
        }
      }
      if (report != null && PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
      {
        int num = await SMPrintJobMaint.CreatePrintJobGroup(new PrintSettings()
        {
          PrintWithDeviceHub = new bool?(true),
          DefinePrinterManually = new bool?(true),
          PrinterID = new NotificationUtility((PXGraph) graph).SearchPrinter("Customer", "SO644005", ((PXGraph) graph).Accessinfo.BranchID),
          NumberOfCopies = new int?(1)
        }, report, (string) null, cancellationToken) ? 1 : 0;
      }
    }
    else if (((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current.WorksheetType == "WV")
    {
      report = new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["WorksheetNbr"] = ((PXSelectBase<SOPickingWorksheet>) graph.worksheet).Current.WorksheetNbr
      }, "SO644007", (CurrentLocalization) null);
      if (report != null && PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
      {
        int num = await SMPrintJobMaint.CreatePrintJobGroup(new PrintSettings()
        {
          PrintWithDeviceHub = new bool?(true),
          DefinePrinterManually = new bool?(true),
          PrinterID = new NotificationUtility((PXGraph) graph).SearchPrinter("Customer", "SO644007", ((PXGraph) graph).Accessinfo.BranchID),
          NumberOfCopies = new int?(1)
        }, report, (string) null, cancellationToken) ? 1 : 0;
      }
    }
    PXReportRequiredException requiredException = report;
    report = (PXReportRequiredException) null;
    return requiredException;
  }

  private static void UnlinkAllShipmentsImpl(IEnumerable<SOPickingWorksheet> worksheets)
  {
    Lazy<SOShipmentEntry> lazy = Lazy.By<SOShipmentEntry>((Func<SOShipmentEntry>) (() => PXGraph.CreateInstance<SOShipmentEntry>()));
    foreach (SOPickingWorksheet worksheet in worksheets)
    {
      foreach (SOShipment selectChild in KeysRelation<Field<SOShipment.currentWorksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOShipment>, SOPickingWorksheet, SOShipment>.SelectChildren((PXGraph) lazy.Value, worksheet))
      {
        ((PXSelectBase<SOShipment>) lazy.Value.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) lazy.Value.Document).Search<SOShipment.shipmentNbr>((object) selectChild.ShipmentNbr, Array.Empty<object>()));
        SOShipmentEntryUnlinkWorksheetExt implementation = ((PXGraph) lazy.Value).FindImplementation<SOShipmentEntryUnlinkWorksheetExt>();
        if (((PXAction) implementation.UnlinkFromWorksheet).GetEnabled())
          ((PXAction) implementation.UnlinkFromWorksheet).Press();
      }
    }
  }

  public virtual bool TryCancelWorksheet()
  {
    if (!(((PXSelectBase<SOPickingWorksheet>) this.worksheet).Current.Status == "I"))
      return false;
    ((PXSelectBase<SOPickingWorksheet>) this.worksheet).Current.Status = "L";
    ((PXSelectBase<SOPickingWorksheet>) this.worksheet).UpdateCurrent();
    foreach (PXResult<SOPickingJob> pxResult in PXSelectBase<SOPickingJob, PXViewOf<SOPickingJob>.BasedOn<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<SOPickingJob.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPickingJob>, SOPickingWorksheet, SOPickingJob>.SameAsCurrent>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      SOPickingJob soPickingJob = PXResult<SOPickingJob>.op_Implicit(pxResult);
      soPickingJob.Status = "CNL";
      ((PXSelectBase<SOPickingJob>) this.pickerJob).Update(soPickingJob);
    }
    return true;
  }

  public SOPickingWorksheetPickListConfirmation PickListConfirmation
  {
    get => ((PXGraph) this).FindImplementation<SOPickingWorksheetPickListConfirmation>();
  }

  public class Workflow : PXGraphExtension<SOPickingWorksheetReview>
  {
    public virtual void Configure(PXScreenConfiguration config)
    {
      SOPickingWorksheetReview.Workflow.Configure(config.GetScreenConfigurationContext<SOPickingWorksheetReview, SOPickingWorksheet>());
    }

    protected static void Configure(
      WorkflowContext<SOPickingWorksheetReview, SOPickingWorksheet> context)
    {
      SOPickingWorksheetReview.Workflow.Conditions conditions = context.Conditions.GetPack<SOPickingWorksheetReview.Workflow.Conditions>();
      CommonActionCategories.Categories<SOPickingWorksheetReview, SOPickingWorksheet> categories1 = CommonActionCategories.Get<SOPickingWorksheetReview, SOPickingWorksheet>(context);
      BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionCategory.IConfigured processingCategory = categories1.Processing;
      BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionCategory.IConfigured printingAndMailingCategory = categories1.PrintingAndEmailing;
      BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionCategory.IConfigured otherCategory = categories1.Other;
      context.AddScreenConfigurationFor((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ScreenConfiguration.IStartConfigScreen, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ScreenConfiguration.IConfigured) ((BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<SOPickingWorksheet.status>().AddDefaultFlow((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Workflow.INeedStatesFlow, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Workflow.IConfigured>) (flow => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IContainerFillerStates>) (fss =>
      {
        fss.Add("_", (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<SOPickingWorksheetReview, PXAutoAction<SOPickingWorksheet>>>) (g => g.InitializeState))));
        fss.Add<SOPickingWorksheet.status.hold>((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured>) (fs => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured) fs.WithActions((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.ReleaseFromHold), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPickList), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPackSlips), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        }))));
        fss.Add<SOPickingWorksheet.status.open>((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured>) (fs => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured) fs.WithActions((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PutOnHold), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPickList), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPackSlips), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        }))));
        fss.Add<SOPickingWorksheet.status.picking>((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured>) (fs => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured) fs.WithActions((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPickList), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPackSlips), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.CancelWorksheet), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) null);
        }))));
        fss.Add<SOPickingWorksheet.status.picked>((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured>) (fs => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured) fs.WithActions((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPickList), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPackSlips), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.UnlinkAllShipments), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) null);
        }))));
        fss.Add<SOPickingWorksheet.status.completed>((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured>) (fs => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured) fs.WithActions((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPickList), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPackSlips), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        }))));
        fss.Add<SOPickingWorksheet.status.cancelled>((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured>) (fs => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.BaseFlowStep.IConfigured) fs.WithActions((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPickList), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionState.IConfigured>) null)))));
      })).WithTransitions((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.IContainerFillerTransitions>) (trans =>
      {
        trans.AddGroupFrom("_", (Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.ISourceContainerFillerTransitions>) (gts =>
        {
          gts.Add((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.INeedTarget, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.IConfigured>) (t => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.IConfigured) t.To<SOPickingWorksheet.status.hold>().IsTriggeredOn((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.InitializeState)).When((BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ISharedCondition) conditions.IsOnHold)));
          gts.Add((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.INeedTarget, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.IConfigured>) (t => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.IConfigured) t.To<SOPickingWorksheet.status.open>().IsTriggeredOn((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.InitializeState))));
        }));
        trans.AddGroupFrom<SOPickingWorksheet.status.hold>((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.ISourceContainerFillerTransitions>) (gts => gts.Add((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.INeedTarget, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.IConfigured>) (t => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.IConfigured) t.To<SOPickingWorksheet.status.open>().IsTriggeredOn((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.ReleaseFromHold))))));
        trans.AddGroupFrom<SOPickingWorksheet.status.open>((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.ISourceContainerFillerTransitions>) (gts => gts.Add((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.INeedTarget, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.IConfigured>) (t => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Transition.IConfigured) t.To<SOPickingWorksheet.status.hold>().IsTriggeredOn((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PutOnHold))))));
      })))).WithCategories((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionCategory.IContainerFillerCategories>) (categories =>
      {
        categories.Add(processingCategory);
        categories.Add(printingAndMailingCategory);
        categories.Add(otherCategory);
      })).WithActions((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.ReleaseFromHold), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOPickingWorksheet.hold>(new bool?(false))))));
        actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PutOnHold), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOPickingWorksheet.hold>(new bool?(true))))));
        actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPickList), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured) a.WithCategory(printingAndMailingCategory)));
        actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPackSlips), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured) a.WithCategory(printingAndMailingCategory)));
        actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.UnlinkAllShipments), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
        actions.Add((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.CancelWorksheet), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      }))));
    }

    public class Conditions : BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Condition.Pack
    {
      public BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Condition IsOnHold
      {
        get
        {
          return this.GetOrCreate((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Condition.ConditionBuilder, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.Condition>) (b => b.FromBql<BqlOperand<SOPickingWorksheet.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
        }
      }
    }
  }

  [PXLocalizable]
  public static class Msg
  {
    public const string CancellationConfirmation = "You are about to cancel the picking worksheet. All the related pick lists will be canceled, and the picked quantity will be set to zero in the related shipments. The picked items should be moved to their initial locations manually by the warehouse staff; there is no automated guidance for this operation.";
  }
}
