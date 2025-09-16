// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickingWorksheetProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CS.Attributes;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable enable
namespace PX.Objects.SO;

public class SOPickingWorksheetProcess : 
  PXGraph<
  #nullable disable
  SOPickingWorksheetProcess>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  public PXFilter<SOPickingWorksheetProcess.HeaderFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<SOShipment, SOPickingWorksheetProcess.HeaderFilter> Shipments;
  public PXCancel<SOPickingWorksheetProcess.HeaderFilter> Cancel;
  public FbqlSelect<SelectFromBase<PX.Objects.IN.INSite, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.IN.INSite>.View INSites;
  public FbqlSelect<SelectFromBase<PX.Objects.CS.Carrier, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CS.Carrier>.View Carriers;
  public FbqlSelect<SelectFromBase<PX.Objects.CR.Location, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CR.Location>.View DummyLocation;

  public SOPickingWorksheetProcess()
  {
    ((PXProcessingBase<SOShipment>) this.Shipments).SuppressUpdate = true;
  }

  public IEnumerable shipments()
  {
    return !EnumerableExtensions.IsIn<string>(((PXSelectBase<SOPickingWorksheetProcess.HeaderFilter>) this.Filter).Current?.Action, (string) null, "N") ? (IEnumerable) this.GetShipments() : (IEnumerable) Array.Empty<SOShipment>();
  }

  [PXSuppressActionValidation]
  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<SOShipment>) this.Shipments).Current != null)
    {
      SOShipmentEntry instance = PXGraph.CreateInstance<SOShipmentEntry>();
      ((PXSelectBase<SOShipment>) instance.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) instance.Document).Search<SOShipment.shipmentNbr>((object) ((PXSelectBase<SOShipment>) this.Shipments).Current.ShipmentNbr, Array.Empty<object>()));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Shipment");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.INSite.descr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CS.Carrier.description> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PX.Objects.CS.LocationRawAttribute), "DisplayName", "Customer Location ID")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.locationCD> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Customer Location Name")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.descr> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (SOPickingWorksheet.worksheetNbr))]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingWorksheetProcess.HeaderFilter.action>, IBqlString>.IsNotEqual<SOPickingWorksheetProcess.ProcessAction.createSinglePickList>))]
  protected void _(
    PX.Data.Events.CacheAttached<SOShipment.currentWorksheetNbr> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<SOPickingWorksheetProcess.HeaderFilter> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOPickingWorksheetProcess.\u003C\u003Ec__DisplayClass17_0 cDisplayClass170 = new SOPickingWorksheetProcess.\u003C\u003Ec__DisplayClass17_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass170.action = e.Row.Action;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass170.settings = PXCacheEx.GetExtension<SOPickingWorksheetProcess.HeaderSettings>((IBqlTable) e.Row);
    // ISSUE: method pointer
    ((PXProcessingBase<SOShipment>) this.Shipments).SetProcessDelegate(new PXProcessingBase<SOShipment>.ProcessListDelegate((object) cDisplayClass170, __methodptr(\u003C_\u003Eb__0)));
    if (PXContext.GetSlot<AUSchedule>() != null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOPickingWorksheetProcess.HeaderFilter>>) e).Cache;
    SOPickingWorksheetProcess.HeaderFilter row = e.Row;
    // ISSUE: reference to a compiler-generated field
    bool? nullable = cDisplayClass170.settings.PrintWithDeviceHub;
    int num;
    if (nullable.GetValueOrDefault())
    {
      // ISSUE: reference to a compiler-generated field
      nullable = cDisplayClass170.settings.DefinePrinterManually;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    PXUIFieldAttribute.SetEnabled<SOPickingWorksheetProcess.HeaderSettings.printerID>(cache, (object) row, num != 0);
  }

  protected virtual void _(
    PX.Data.Events.RowUpdated<SOPickingWorksheetProcess.HeaderFilter> e)
  {
    SOPickingWorksheetProcess.HeaderSettings extension1 = PXCacheEx.GetExtension<SOPickingWorksheetProcess.HeaderSettings>((IBqlTable) e.Row);
    SOPickingWorksheetProcess.HeaderSettings extension2 = PXCacheEx.GetExtension<SOPickingWorksheetProcess.HeaderSettings>((IBqlTable) e.OldRow);
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOPickingWorksheetProcess.HeaderFilter>>) e).Cache.ObjectsEqual<SOPickingWorksheetProcess.HeaderFilter.action, SOPickingWorksheetProcess.HeaderSettings.definePrinterManually, SOPickingWorksheetProcess.HeaderSettings.printWithDeviceHub>((object) e.Row, (object) e.OldRow) || !PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() || extension1 == null)
      return;
    bool? nullable = extension1.PrintWithDeviceHub;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = extension1.DefinePrinterManually;
    if (!nullable.GetValueOrDefault())
      return;
    if (PXContext.GetSlot<AUSchedule>() != null)
    {
      Guid? printerId = extension1.PrinterID;
      if (printerId.HasValue)
      {
        printerId = extension2.PrinterID;
        if (!printerId.HasValue)
          return;
      }
    }
    extension1.PrinterID = new NotificationUtility((PXGraph) this).SearchPrinter("Customer", "SO644000", ((PXGraph) this).Accessinfo.BranchID);
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOShipment> e)
  {
    if (e.Row == null || !this.HasNonStockLinesWithEmptyLocation(e.Row.ShipmentNbr))
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOShipment>>) e).Cache.RaiseExceptionHandling<SOShipment.shipmentNbr>((object) e.Row, (object) e.Row.ShipmentNbr, (Exception) new PXSetPropertyException("The {0} shipment cannot be added to the pick list because it contains non-stock item with empty location. This shipment should be processed manually via the Shipments (SO302000) form.", (PXErrorLevel) 5, new object[1]
    {
      (object) e.Row.ShipmentNbr
    }));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<SOShipment, SOShipment.selected> e)
  {
    if (e.Row == null || !((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOShipment, SOShipment.selected>, SOShipment, object>) e).NewValue).GetValueOrDefault() || !this.HasNonStockLinesWithEmptyLocation(e.Row.ShipmentNbr))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOShipment, SOShipment.selected>, SOShipment, object>) e).NewValue = (object) false;
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

  protected SOPickingWorksheetProcess.HeaderSettings ProcessSettings { get; set; }

  protected virtual IEnumerable<SOShipment> GetShipments()
  {
    SOPickingWorksheetProcess worksheetProcess = this;
    SOPickingWorksheetProcess.HeaderFilter current = ((PXSelectBase<SOPickingWorksheetProcess.HeaderFilter>) worksheetProcess.Filter).Current;
    BqlCommand cmd = (BqlCommand) new SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<SOShipment.FK.Site>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<BqlOperand<SOShipment.customerID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.bAccountID>>.SingleTableOnly>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<MatchUserFor<PX.Objects.IN.INSite>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<MatchUserFor<PX.Objects.AR.Customer>>>>, And<BqlOperand<SOShipment.shipDate, IBqlDateTime>.IsLessEqual<BqlField<SOPickingWorksheetProcess.HeaderFilter.endDate, IBqlDateTime>.FromCurrent>>>, And<BqlOperand<SOShipment.siteID, IBqlInt>.IsEqual<BqlField<SOPickingWorksheetProcess.HeaderFilter.siteID, IBqlInt>.FromCurrent>>>, And<BqlOperand<SOShipment.status, IBqlString>.IsEqual<SOShipmentStatus.open>>>, And<BqlOperand<SOShipment.operation, IBqlString>.IsEqual<SOOperation.issue>>>>.And<Not<Exists<SelectFromBase<SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<SOShipLineSplit.shipmentNbr>.IsRelatedTo<SOShipment.shipmentNbr>.AsSimpleKey.WithTablesOf<SOShipment, SOShipLineSplit>, SOShipment, SOShipLineSplit>.And<BqlOperand<SOShipLineSplit.pickedQty, IBqlDecimal>.IsGreater<decimal0>>>>>>>();
    List<object> parameters = new List<object>();
    BqlCommand bqlCommand = worksheetProcess.AppendFilter(cmd, (IList<object>) parameters, current);
    PXView pxView = new PXView((PXGraph) worksheetProcess, true, bqlCommand);
    int startRow = PXView.StartRow;
    int num = 0;
    foreach (object obj in pxView.Select((object[]) null, parameters.ToArray(), PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
    {
      SOShipment shipment = PXResult.Unwrap<SOShipment>(obj);
      SOShipment soShipment = ((PXSelectBase<SOShipment>) worksheetProcess.Shipments).Locate(shipment);
      if (soShipment != null)
        shipment.Selected = soShipment.Selected;
      yield return shipment;
    }
    PXView.StartRow = 0;
    ((PXSelectBase) worksheetProcess.Shipments).Cache.IsDirty = false;
  }

  public virtual BqlCommand AppendFilter(
    BqlCommand cmd,
    IList<object> parameters,
    SOPickingWorksheetProcess.HeaderFilter filter)
  {
    if (!string.IsNullOrEmpty(filter.CarrierPluginID))
    {
      cmd = BqlCommand.AppendJoin<LeftJoin<PX.Objects.CS.Carrier, On<SOShipment.FK.Carrier>>>(cmd);
      cmd = cmd.WhereAnd<Where<BqlOperand<PX.Objects.CS.Carrier.carrierPluginID, IBqlString>.IsEqual<BqlField<SOPickingWorksheetProcess.HeaderFilter.carrierPluginID, IBqlString>.FromCurrent>>>();
    }
    if (!string.IsNullOrEmpty(filter.ShipVia))
      cmd = cmd.WhereAnd<Where<BqlOperand<SOShipment.shipVia, IBqlString>.IsEqual<BqlField<SOPickingWorksheetProcess.HeaderFilter.shipVia, IBqlString>.FromCurrent>>>();
    if (filter.StartDate.HasValue)
      cmd = cmd.WhereAnd<Where<BqlOperand<SOShipment.shipDate, IBqlDateTime>.IsGreaterEqual<BqlField<SOPickingWorksheetProcess.HeaderFilter.startDate, IBqlDateTime>.FromCurrent>>>();
    int? nullable1 = filter.CustomerID;
    if (nullable1.HasValue)
      cmd = cmd.WhereAnd<Where<BqlOperand<SOShipment.customerID, IBqlInt>.IsEqual<BqlField<SOPickingWorksheetProcess.HeaderFilter.customerID, IBqlInt>.FromCurrent>>>();
    if (filter.PackagingType == "M")
      cmd = cmd.WhereAnd<Where<BqlOperand<SOShipment.isManualPackage, IBqlBool>.IsEqual<True>>>();
    else if (filter.PackagingType == "A")
      cmd = cmd.WhereAnd<Where<BqlOperand<SOShipment.isManualPackage, IBqlBool>.IsNotEqual<True>>>();
    if (EnumerableExtensions.IsIn<string>(filter.Action, "B", "W", "S"))
      cmd = cmd.WhereAnd<Where<BqlOperand<SOShipment.currentWorksheetNbr, IBqlString>.IsNull>>();
    int? ofLinesInShipment1 = filter.MaxNumberOfLinesInShipment;
    nullable1 = new int?();
    int? nullable2 = nullable1;
    int? nullable3 = new int?(0);
    if (!EnumerableExtensions.IsNotIn<int?>(ofLinesInShipment1, nullable2, nullable3))
    {
      int? maxQtyInLines = filter.MaxQtyInLines;
      nullable1 = new int?();
      int? nullable4 = nullable1;
      int? nullable5 = new int?(0);
      if (!EnumerableExtensions.IsNotIn<int?>(maxQtyInLines, nullable4, nullable5))
        goto label_23;
    }
    cmd = cmd.WhereAnd<Where<Exists<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SOShipLine.shipmentNbr, IBqlString>.IsEqual<SOShipment.shipmentNbr>>.Aggregate<To<Count, Max<SOShipLine.shippedQty>>>.Having<BqlChainableConditionHavingBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FunctionWrapper<Count>, LessEqual<FunctionWrapper<P.AsInt>>>>>>.And<BqlAggregatedOperand<Max<SOShipLine.shippedQty>, IBqlDecimal>.IsLessEqual<P.AsDecimal>>>>>>();
    IList<object> objectList1 = parameters;
    int? ofLinesInShipment2 = filter.MaxNumberOfLinesInShipment;
    nullable1 = new int?();
    int? nullable6 = nullable1;
    int? nullable7 = new int?(0);
    int maxValue1;
    if (!EnumerableExtensions.IsNotIn<int?>(ofLinesInShipment2, nullable6, nullable7))
    {
      maxValue1 = int.MaxValue;
    }
    else
    {
      nullable1 = filter.MaxNumberOfLinesInShipment;
      maxValue1 = nullable1.Value;
    }
    // ISSUE: variable of a boxed type
    __Boxed<int> local1 = (ValueType) maxValue1;
    objectList1.Add((object) local1);
    IList<object> objectList2 = parameters;
    int? maxQtyInLines1 = filter.MaxQtyInLines;
    nullable1 = new int?();
    int? nullable8 = nullable1;
    int? nullable9 = new int?(0);
    int maxValue2;
    if (!EnumerableExtensions.IsNotIn<int?>(maxQtyInLines1, nullable8, nullable9))
    {
      maxValue2 = int.MaxValue;
    }
    else
    {
      nullable1 = filter.MaxQtyInLines;
      maxValue2 = nullable1.Value;
    }
    // ISSUE: variable of a boxed type
    __Boxed<int> local2 = (ValueType) maxValue2;
    objectList2.Add((object) local2);
label_23:
    return cmd;
  }

  private static void ProcessShipmentsHandler(
    string action,
    SOPickingWorksheetProcess.HeaderSettings settings,
    IEnumerable<SOShipment> shipments)
  {
    PXGraph.CreateInstance<SOPickingWorksheetProcess>().ProcessShipments(action, settings, shipments);
  }

  protected virtual void ProcessShipments(
    string action,
    SOPickingWorksheetProcess.HeaderSettings settings,
    IEnumerable<SOShipment> shipments)
  {
    foreach (SOShipment shipment in shipments)
    {
      if (!this.ValidateShipment(shipment, action))
        throw new PXInvalidOperationException("The pick list for the {0} shipment cannot be created because this shipment is already being processed on the Pick, Pack, and Ship (SO302020) form.", new object[1]
        {
          (object) shipment.ShipmentNbr
        });
    }
    this.ProcessSettings = settings;
    switch (action)
    {
      case "W":
        this.CreateWavePickList(settings, shipments);
        break;
      case "B":
        this.CreateBatchPickList(settings, shipments);
        break;
      case "S":
        this.CreateSinglePickList(settings, shipments);
        break;
    }
  }

  protected virtual bool ValidateShipment(SOShipment shipment, [SOPickingWorksheetProcess.ProcessAction.List] string processAction)
  {
    return NonGenericIEnumerableExtensions.Any_((IEnumerable) PXSelectBase<SOShipment, PXViewOf<SOShipment>.BasedOn<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.shipmentNbr, Equal<P.AsString>>>>, And<BqlOperand<SOShipment.status, IBqlString>.IsEqual<SOShipmentStatus.open>>>, And<BqlOperand<SOShipment.operation, IBqlString>.IsEqual<SOOperation.issue>>>, And<BqlOperand<SOShipment.currentWorksheetNbr, IBqlString>.IsNull>>>.And<NotExists<SelectFromBase<SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLineSplit.shipmentNbr, Equal<SOShipment.shipmentNbr>>>>>.And<BqlOperand<SOShipLineSplit.pickedQty, IBqlDecimal>.IsGreater<decimal0>>>>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) shipment.ShipmentNbr
    }));
  }

  private PXAdapter CreateAdapter<TRow>(
    PXGraph graph,
    SOPickingWorksheetProcess.HeaderSettings settings,
    IEnumerable<TRow> rows)
    where TRow : class, IBqlTable, new()
  {
    return new PXAdapter((PXView) PXView.Dummy.For<TRow>(graph, rows))
    {
      MassProcess = true,
      Arguments = {
        ["PrintWithDeviceHub"] = (object) settings.PrintWithDeviceHub,
        ["DefinePrinterManually"] = (object) settings.DefinePrinterManually,
        ["PrinterID"] = (object) settings.PrinterID,
        ["NumberOfCopies"] = (object) 1
      }
    };
  }

  protected virtual void CreateWavePickList(
    SOPickingWorksheetProcess.HeaderSettings settings,
    IEnumerable<SOShipment> shipments)
  {
    if (!shipments.Any<SOShipment>())
      return;
    if (this.HasAnyShipmentContainsNonStockShipLinesWithEmptyLocation(shipments))
      throw new PXInvalidOperationException("The picking worksheet cannot be created because one or multiple shipments contain non-stock items with empty location. For more details see Trace.");
    if (shipments.GroupBy<SOShipment, int?>((Func<SOShipment, int?>) (s => s.SiteID)).Count<IGrouping<int?, SOShipment>>() > 1)
      throw new PXInvalidOperationException("All selected shipments should have same Site ID.");
    int? nullable = settings.NumberOfTotesPerPicker;
    int num1;
    if (nullable.GetValueOrDefault() == 0)
    {
      Decimal d1 = (Decimal) shipments.Count<SOShipment>();
      nullable = settings.NumberOfPickers;
      Decimal valueOrDefault = (Decimal) nullable.GetValueOrDefault();
      num1 = (int) Math.Ceiling(Decimal.Divide(d1, valueOrDefault));
    }
    else
    {
      nullable = settings.NumberOfTotesPerPicker;
      num1 = nullable.GetValueOrDefault();
    }
    int num2 = num1;
    int pickerCount = (int) Math.Ceiling(Decimal.Divide((Decimal) shipments.Count<SOShipment>(), (Decimal) num2));
    int num3 = pickerCount;
    nullable = settings.NumberOfPickers;
    int valueOrDefault1 = nullable.GetValueOrDefault();
    if (num3 > valueOrDefault1)
      throw new PXInvalidOperationException("Overall number of totes should be at least as many as number of shipments.");
    IEnumerable<SOPickingWorksheetLineSplit> worksheetLineSplits = this.MergeSplits(shipments);
    if (!worksheetLineSplits.Any<SOPickingWorksheetLineSplit>())
      return;
    SOPickingWorksheetReview instance = PXGraph.CreateInstance<SOPickingWorksheetReview>();
    SOPickingWorksheet worksheet = this.CreateWorksheet(instance, "WV", shipments.First<SOShipment>().SiteID, worksheetLineSplits).worksheet;
    this.LinkShipments(instance, shipments, worksheet);
    this.SpreadShipmentsAmongPickers(instance, shipments, pickerCount, num2);
    ((PXAction) instance.Save).Press();
    if (!settings.PrintPickLists.GetValueOrDefault())
      return;
    EnumerableExtensions.Consume(((PXAction) instance.PrintPickList).Press(this.CreateAdapter<SOPickingWorksheet>((PXGraph) instance, settings, (IEnumerable<SOPickingWorksheet>) new SOPickingWorksheet[1]
    {
      worksheet
    })));
  }

  protected virtual void SpreadShipmentsAmongPickers(
    SOPickingWorksheetReview worksheetGraph,
    IEnumerable<SOShipment> shipments,
    int pickerCount,
    int shipmentCountPerPicker)
  {
    int? siteId = shipments.ToArray<SOShipment>()[0].SiteID;
    NonStockKitSpecHelper kitSpecHelper = new NonStockKitSpecHelper((PXGraph) worksheetGraph);
    IReadOnlyDictionary<string, IEnumerable<(SOShipLineSplit, SOShipLine)>> source1 = (IReadOnlyDictionary<string, IEnumerable<(SOShipLineSplit, SOShipLine)>>) EnumerableExtensions.AsReadOnly<string, IEnumerable<(SOShipLineSplit, SOShipLine)>>((IDictionary<string, IEnumerable<(SOShipLineSplit, SOShipLine)>>) shipments.ToDictionary<SOShipment, string, IEnumerable<(SOShipLineSplit, SOShipLine)>>((Func<SOShipment, string>) (s => s.ShipmentNbr), (Func<SOShipment, IEnumerable<(SOShipLineSplit, SOShipLine)>>) (s => ((IEnumerable<(SOShipLineSplit, SOShipLine)>) ((IEnumerable<PXResult<SOShipLineSplit, SOShipLine>>) this.GetShipmentSplits(s)).Select<PXResult<SOShipLineSplit, SOShipLine>, (SOShipLineSplit, SOShipLine)>((Func<PXResult<SOShipLineSplit, SOShipLine>, (SOShipLineSplit, SOShipLine)>) (ss => (((PXResult) ss).GetItem<SOShipLineSplit>(), ((PXResult) ss).GetItem<SOShipLine>()))).ToArray<(SOShipLineSplit, SOShipLine)>()).AsEnumerable<(SOShipLineSplit, SOShipLine)>())));
    WMSPath fullPath = new WMSPath("FullPath", (IEnumerable<INLocation>) source1.SelectMany<KeyValuePair<string, IEnumerable<(SOShipLineSplit, SOShipLine)>>, (SOShipLineSplit, SOShipLine)>((Func<KeyValuePair<string, IEnumerable<(SOShipLineSplit, SOShipLine)>>, IEnumerable<(SOShipLineSplit, SOShipLine)>>) (kvp => kvp.Value)).Select<(SOShipLineSplit, SOShipLine), int?>((Func<(SOShipLineSplit, SOShipLine), int?>) (s => s.Split.LocationID)).With<IEnumerable<int?>, INLocation[]>(new Func<IEnumerable<int?>, INLocation[]>(this.GetSortedLocations)));
    IEnumerable<WMSPath> shipmentPaths = (IEnumerable<WMSPath>) source1.Select<KeyValuePair<string, IEnumerable<(SOShipLineSplit, SOShipLine)>>, WMSPath>((Func<KeyValuePair<string, IEnumerable<(SOShipLineSplit, SOShipLine)>>, WMSPath>) (sh => fullPath.GetIntersectionWith(sh.Value.Select<(SOShipLineSplit, SOShipLine), int?>((Func<(SOShipLineSplit, SOShipLine), int?>) (s => s.Split.LocationID)), sh.Key))).With<IEnumerable<WMSPath>, IEnumerable<WMSPath>>((Func<IEnumerable<WMSPath>, IEnumerable<WMSPath>>) (paths => WMSPath.SortPaths(paths, true))).ToArray<WMSPath>();
    IReadOnlyList<List<WMSPath>> wmsPathListList = (IReadOnlyList<List<WMSPath>>) Enumerable.Range(0, pickerCount).Select<int, List<WMSPath>>((Func<int, List<WMSPath>>) (n => shipmentPaths.Skip<WMSPath>(n * shipmentCountPerPicker).Take<WMSPath>(shipmentCountPerPicker).ToList<WMSPath>())).ToArray<List<WMSPath>>().With<List<WMSPath>[], ReadOnlyCollection<List<WMSPath>>>(new Func<List<WMSPath>[], ReadOnlyCollection<List<WMSPath>>>(Array.AsReadOnly<List<WMSPath>>));
    List<WMSPath> wmsPathList = wmsPathListList.Last<List<WMSPath>>();
    if (wmsPathList.Count < shipmentCountPerPicker)
      wmsPathList.AddRange(Enumerable.Range(0, shipmentCountPerPicker - wmsPathList.Count).Select<int, WMSPath>((Func<int, WMSPath>) (n => WMSPath.MakeFake($"fake{n}"))));
    WMSPath[] array = wmsPathListList.Select<List<WMSPath>, WMSPath>((Func<List<WMSPath>, int, WMSPath>) ((shs, i) => WMSPath.MergePaths((IEnumerable<WMSPath>) shs, $"picker{i}"))).ToArray<WMSPath>();
    if (shipmentCountPerPicker != 1)
      this.BalancePathsAmongPickers(wmsPathListList, array);
    for (int index = 0; index < wmsPathListList.Count; ++index)
    {
      WMSPath source2 = array[index];
      List<WMSPath> source3 = wmsPathListList[index];
      ((PXSelectBase<SOPicker>) worksheetGraph.pickers).Insert(new SOPicker()
      {
        NumberOfTotes = new int?(source3.Count),
        PathLength = new int?(source2.PathLength),
        FirstLocationID = source2.First<INLocation>().LocationID,
        LastLocationID = source2.Last<INLocation>().LocationID
      });
      if (PXAccess.FeatureInstalled<FeaturesSet.wMSPaperlessPicking>())
      {
        FbqlSelect<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOPickingJob.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickingJob.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickingJob>, SOPicker, SOPickingJob>.SameAsCurrent>, SOPickingJob>.View pickerJob = worksheetGraph.pickerJob;
        SOPickingJob soPickingJob = new SOPickingJob();
        soPickingJob.Status = this.ProcessSettings.SendToQueue.GetValueOrDefault() ? "ENQ" : (string) null;
        soPickingJob.AutomaticShipmentConfirmation = this.ProcessSettings.AutomaticShipmentConfirmation;
        ((PXSelectBase<SOPickingJob>) pickerJob).Insert(soPickingJob);
      }
      foreach (WMSPath wmsPath in source3.Where<WMSPath>((Func<WMSPath, bool>) (sh => !sh.IsFake)))
      {
        PXCache<SOPickerToShipmentLink>.Insert((PXGraph) worksheetGraph, new SOPickerToShipmentLink()
        {
          ShipmentNbr = wmsPath.Name,
          SiteID = siteId
        });
        foreach ((SOShipLineSplit split, SOShipLine line) in source1[wmsPath.Name])
          ((PXSelectBase<SOPickerListEntry>) worksheetGraph.pickerList).Insert(this.PickerListEntryFrom(split, this.GetLineUOM(kitSpecHelper, split, line), wmsPath.Name));
      }
    }
    GraphHelper.EnsureCachePersistence<SOPickerToShipmentLink>((PXGraph) worksheetGraph);
  }

  private string GetLineUOM(
    NonStockKitSpecHelper kitSpecHelper,
    SOShipLineSplit split,
    SOShipLine line)
  {
    return !kitSpecHelper.IsNonStockKit(line.InventoryID) ? line.UOM : split.UOM;
  }

  protected virtual void BalancePathsAmongPickers(
    IReadOnlyList<List<WMSPath>> pathsOfPicker,
    WMSPath[] fullPathOfPicker)
  {
    bool flag;
    do
    {
      flag = false;
      for (int pickerNbr = 0; pickerNbr < pathsOfPicker.Count - 1; pickerNbr++)
      {
        foreach (int num in (IEnumerable<int>) Enumerable.Range(0, fullPathOfPicker.Length).Skip<int>(pickerNbr + 1).Where<int>((Func<int, bool>) (pn => pn == pickerNbr + 1 || fullPathOfPicker[pickerNbr].GetIntersectionWith(fullPathOfPicker[pn], (string) null).With<WMSPath, bool>((Func<WMSPath, bool>) (inter => !inter.IsEmpty)))).ToArray<int>().With<int[], ReadOnlyCollection<int>>(new Func<int[], ReadOnlyCollection<int>>(Array.AsReadOnly<int>)))
        {
          int pickerToExchange = num;
          (WMSPath, WMSPath)[] array1 = pathsOfPicker[pickerNbr].Where<WMSPath>((Func<WMSPath, bool>) (lp => lp.IsFake || fullPathOfPicker[pickerNbr].EndsWith(lp) || fullPathOfPicker[pickerToExchange].Contains(lp))).SelectMany((Func<WMSPath, IEnumerable<WMSPath>>) (leftPath => pathsOfPicker[pickerToExchange].Where<WMSPath>((Func<WMSPath, bool>) (rp => rp.IsFake || fullPathOfPicker[pickerToExchange].EndsWith(rp) || fullPathOfPicker[pickerNbr].Contains(rp)))), (leftPath, rightPath) => new
          {
            leftPath = leftPath,
            rightPath = rightPath
          }).Where(_param1 =>
          {
            if (_param1.leftPath == null || _param1.rightPath == null)
              return false;
            return !_param1.leftPath.IsFake || !_param1.rightPath.IsFake;
          }).Select(_param1 => (_param1.leftPath, _param1.rightPath)).ToArray<(WMSPath, WMSPath)>();
          \u003C\u003Ef__AnonymousType111<WMSPath, WMSPath>[] array2 = ((IEnumerable<(WMSPath, WMSPath)>) array1).Select(pair => new
          {
            LeftPickerNewFullPath = EnumerableExtensions.Except<WMSPath>((IEnumerable<WMSPath>) pathsOfPicker[pickerNbr], pair.leftPath).Append<WMSPath>(pair.rightPath).With<IEnumerable<WMSPath>, WMSPath>((Func<IEnumerable<WMSPath>, WMSPath>) (ps => WMSPath.MergePaths(ps, $"picker{pickerNbr}"))),
            RightPickerNewFullPath = EnumerableExtensions.Except<WMSPath>((IEnumerable<WMSPath>) pathsOfPicker[pickerToExchange], pair.rightPath).Append<WMSPath>(pair.leftPath).With<IEnumerable<WMSPath>, WMSPath>((Func<IEnumerable<WMSPath>, WMSPath>) (ps => WMSPath.MergePaths(ps, $"picker{pickerToExchange}")))
          }).ToArray();
          int[] array3 = array2.Select(pair => fullPathOfPicker[pickerNbr].PathLength - pair.LeftPickerNewFullPath.PathLength + fullPathOfPicker[pickerToExchange].PathLength - pair.RightPickerNewFullPath.PathLength).ToArray<int>();
          if (((IEnumerable<int>) array3).Any<int>((Func<int, bool>) (profit => profit > 0)))
          {
            int maxProfit = ((IEnumerable<int>) array3).Max();
            int index = EnumerableExtensions.SelectIndexesWhere<int>((IEnumerable<int>) array3, (Func<int, bool>) (profit => profit == maxProfit)).First<int>();
            (WMSPath, WMSPath) tuple = array1[index];
            var data = array2[index];
            pathsOfPicker[pickerNbr].Remove(tuple.Item1);
            pathsOfPicker[pickerNbr].Add(tuple.Item2);
            fullPathOfPicker[pickerNbr] = data.LeftPickerNewFullPath;
            pathsOfPicker[pickerToExchange].Remove(tuple.Item2);
            pathsOfPicker[pickerToExchange].Add(tuple.Item1);
            fullPathOfPicker[pickerToExchange] = data.RightPickerNewFullPath;
            flag = ((flag ? 1 : 0) | 1) != 0;
          }
        }
      }
    }
    while (flag);
  }

  protected virtual void CreateBatchPickList(
    SOPickingWorksheetProcess.HeaderSettings settings,
    IEnumerable<SOShipment> shipments)
  {
    if (!shipments.Any<SOShipment>())
      return;
    if (this.HasAnyShipmentContainsNonStockShipLinesWithEmptyLocation(shipments))
      throw new PXInvalidOperationException("The picking worksheet cannot be created because one or multiple shipments contain non-stock items with empty location. For more details see Trace.");
    if (shipments.GroupBy<SOShipment, int?>((Func<SOShipment, int?>) (s => s.SiteID)).Count<IGrouping<int?, SOShipment>>() > 1)
      throw new PXInvalidOperationException("All selected shipments should have same Site ID.");
    IEnumerable<SOPickingWorksheetLineSplit> worksheetLineSplits1 = this.MergeSplits(shipments);
    if (!worksheetLineSplits1.Any<SOPickingWorksheetLineSplit>())
      return;
    SOPickingWorksheetReview instance = PXGraph.CreateInstance<SOPickingWorksheetReview>();
    (SOPickingWorksheet worksheet, IEnumerable<SOPickingWorksheetLine> pickingWorksheetLines, IEnumerable<SOPickingWorksheetLineSplit> worksheetLineSplits2) = this.CreateWorksheet(instance, "BT", shipments.First<SOShipment>().SiteID, worksheetLineSplits1);
    this.LinkShipments(instance, shipments, worksheet);
    this.SpreadSplitsAmongPickers(instance, (IEnumerable<(SOPickingWorksheetLineSplit, string)>) worksheetLineSplits2.Join<SOPickingWorksheetLineSplit, SOPickingWorksheetLine, int?, (SOPickingWorksheetLineSplit, string)>(pickingWorksheetLines, (Func<SOPickingWorksheetLineSplit, int?>) (s => s.LineNbr), (Func<SOPickingWorksheetLine, int?>) (l => l.LineNbr), (Func<SOPickingWorksheetLineSplit, SOPickingWorksheetLine, (SOPickingWorksheetLineSplit, string)>) ((s, l) => (s, l.UOM))).ToArray<(SOPickingWorksheetLineSplit, string)>(), settings.NumberOfPickers.GetValueOrDefault());
    ((PXAction) instance.Save).Press();
    if (!settings.PrintPickLists.GetValueOrDefault())
      return;
    EnumerableExtensions.Consume(((PXAction) instance.PrintPickList).Press(this.CreateAdapter<SOPickingWorksheet>((PXGraph) instance, settings, (IEnumerable<SOPickingWorksheet>) new SOPickingWorksheet[1]
    {
      worksheet
    })));
  }

  protected virtual void SpreadSplitsAmongPickers(
    SOPickingWorksheetReview worksheetGraph,
    IEnumerable<(SOPickingWorksheetLineSplit Split, string UOM)> splits,
    int maxPickersCount)
  {
    if (maxPickersCount == 1)
    {
      this.GiveAllSplitsToSinglePicker(worksheetGraph, splits);
    }
    else
    {
      INLocation[] sortedLocations = this.GetSortedLocations(splits.Select<(SOPickingWorksheetLineSplit, string), int?>((Func<(SOPickingWorksheetLineSplit, string), int?>) (s => s.Split.LocationID)));
      if (sortedLocations.Length == 1)
        this.SpreadSplitsByItems(worksheetGraph, splits, maxPickersCount);
      else if (sortedLocations.Length <= maxPickersCount)
      {
        this.SpreadSplitsByLocations(worksheetGraph, splits, sortedLocations);
      }
      else
      {
        int pickersCount = Math.Min(maxPickersCount, sortedLocations.Length);
        INLocation[] separatingLocations = this.GetSeparatingLocations(sortedLocations, pickersCount);
        WMSPath[] pathsOfPickers = this.GetPathsOfPickers(sortedLocations, separatingLocations);
        this.SpreadSplitsByPath(worksheetGraph, splits, sortedLocations, separatingLocations, pathsOfPickers);
      }
    }
  }

  protected virtual WMSPath[] GetPathsOfPickers(
    INLocation[] locations,
    INLocation[] separatingLocations)
  {
    int index = 0;
    WMSPath[] pathsOfPickers = new WMSPath[separatingLocations.Length + 1];
    List<INLocation> locations1 = new List<INLocation>();
    foreach (INLocation location in locations)
    {
      if (index != separatingLocations.Length)
      {
        int? locationId1 = location.LocationID;
        int? locationId2 = separatingLocations[index].LocationID;
        if (locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue)
        {
          pathsOfPickers[index] = new WMSPath($"picker{index}", (IEnumerable<INLocation>) locations1);
          locations1.Clear();
          locations1.Add(location);
          ++index;
          continue;
        }
      }
      locations1.Add(location);
    }
    pathsOfPickers[index] = new WMSPath($"picker{index}", (IEnumerable<INLocation>) locations1);
    return pathsOfPickers;
  }

  protected virtual INLocation[] GetSeparatingLocations(INLocation[] locations, int pickersCount)
  {
    (INLocation source, INLocation target, int distance)[] array1 = this.GetSortedDistancesBetween((IEnumerable<INLocation>) locations).ToArray<(INLocation, INLocation, int)>();
    Decimal num1 = ((IEnumerable<(INLocation, INLocation, int)>) array1).Average<(INLocation, INLocation, int)>((Func<(INLocation, INLocation, int), Decimal>) (d => (Decimal) d.distance));
    Decimal distanceMedian = (Decimal) ((IEnumerable<(INLocation, INLocation, int)>) array1).OrderBy<(INLocation, INLocation, int), int>((Func<(INLocation, INLocation, int), int>) (d => d.distance)).Skip<(INLocation, INLocation, int)>((array1.Length % 2 == 0 ? array1.Length : array1.Length - 1) / 2).First<(INLocation, INLocation, int)>().Item3;
    IEnumerable<(INLocation, INLocation, int)> source = ((IEnumerable<(INLocation, INLocation, int)>) array1).Where<(INLocation, INLocation, int)>((Func<(INLocation, INLocation, int), bool>) (d => (Decimal) d.distance > 4M * distanceMedian));
    Decimal num2 = 2M * distanceMedian;
    if ((!(num1 > num2) ? 0 : (source.Any<(INLocation, INLocation, int)>() ? 1 : 0)) != 0 && source.Count<(INLocation, INLocation, int)>() > pickersCount - 2)
      return ((IEnumerable<(INLocation, INLocation, int)>) array1).Take<(INLocation, INLocation, int)>(pickersCount - 1).OrderBy<(INLocation, INLocation, int), int?>((Func<(INLocation, INLocation, int), int?>) (d => d.source.PathPriority)).ThenBy<(INLocation, INLocation, int), string>((Func<(INLocation, INLocation, int), string>) (d => d.source.LocationCD)).Select<(INLocation, INLocation, int), INLocation>((Func<(INLocation, INLocation, int), INLocation>) (d => d.target)).ToArray<INLocation>();
    Decimal num3 = 1.333M * (Decimal) ((IEnumerable<(INLocation, INLocation, int)>) array1).Sum<(INLocation, INLocation, int)>((Func<(INLocation, INLocation, int), int>) (d => d.distance)) / (Decimal) pickersCount;
    List<INLocation> inLocationList = new List<INLocation>(pickersCount - 1);
    (INLocation, INLocation, int)[] array2 = ((IEnumerable<(INLocation, INLocation, int)>) array1).OrderBy<(INLocation, INLocation, int), int?>((Func<(INLocation, INLocation, int), int?>) (l => l.source.PathPriority)).ThenBy<(INLocation, INLocation, int), string>((Func<(INLocation, INLocation, int), string>) (l => l.source.LocationCD)).ToArray<(INLocation, INLocation, int)>();
    int index = 0;
    Decimal num4 = 0M;
    while (inLocationList.Count != pickersCount - 1)
    {
      do
      {
        num4 += (Decimal) array2[index].Item3;
        ++index;
      }
      while (index < array2.Length && num4 + (Decimal) array2[index].Item3 < num3);
      if (index < array2.Length)
      {
        inLocationList.Add(array2[index].Item2);
        num4 = 0M;
      }
      else
        break;
    }
    return inLocationList.ToArray();
  }

  protected virtual void SpreadSplitsByPath(
    SOPickingWorksheetReview worksheetGraph,
    IEnumerable<(SOPickingWorksheetLineSplit Split, string UOM)> splits,
    INLocation[] locations,
    INLocation[] separatingLocations,
    WMSPath[] pathsOfPickers)
  {
    (SOPickingWorksheetLineSplit, string)[] sortedSplits = EnumerableExtensions.OrderBy<(SOPickingWorksheetLineSplit, string), int?>(splits, (Func<(SOPickingWorksheetLineSplit, string), int?>) (s => s.Split.LocationID), ((IEnumerable<INLocation>) locations).Select<INLocation, int?>((Func<INLocation, int?>) (loc => loc.LocationID)).ToArray<int?>()).ToArray<(SOPickingWorksheetLineSplit, string)>();
    int[] array = ((IEnumerable<INLocation>) separatingLocations).Select<INLocation, int>((Func<INLocation, int>) (loc => EnumerableExtensions.SelectIndexesWhere<(SOPickingWorksheetLineSplit, string)>((IEnumerable<(SOPickingWorksheetLineSplit, string)>) sortedSplits, (Func<(SOPickingWorksheetLineSplit, string), bool>) (s =>
    {
      int? locationId1 = s.Split.LocationID;
      int? locationId2 = loc.LocationID;
      return locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue;
    })).First<int>())).ToArray<int>();
    int num = 0;
    for (int index = 0; index < sortedSplits.Length; ++index)
    {
      if (index == 0 || EnumerableExtensions.IsIn<int>(index, (IEnumerable<int>) array))
        this.InsertPath(worksheetGraph, pathsOfPickers[num++]);
      ((PXSelectBase<SOPickerListEntry>) worksheetGraph.pickerList).Insert(this.PickerListEntryFrom(sortedSplits[index].Item1, sortedSplits[index].Item2));
    }
  }

  protected virtual void GiveAllSplitsToSinglePicker(
    SOPickingWorksheetReview worksheetGraph,
    IEnumerable<(SOPickingWorksheetLineSplit Split, string UOM)> splits)
  {
    INLocation[] sortedLocations = this.GetSortedLocations(splits.Select<(SOPickingWorksheetLineSplit, string), int?>((Func<(SOPickingWorksheetLineSplit, string), int?>) (s => s.Split.LocationID)));
    WMSPath path = new WMSPath("fullPath", (IEnumerable<INLocation>) sortedLocations);
    (SOPickingWorksheetLineSplit, string)[] array = EnumerableExtensions.OrderBy<(SOPickingWorksheetLineSplit, string), int?>(splits, (Func<(SOPickingWorksheetLineSplit, string), int?>) (s => s.Split.LocationID), ((IEnumerable<INLocation>) sortedLocations).Select<INLocation, int?>((Func<INLocation, int?>) (loc => loc.LocationID)).ToArray<int?>()).ToArray<(SOPickingWorksheetLineSplit, string)>();
    for (int index = 0; index < array.Length; ++index)
    {
      if (index == 0)
        this.InsertPath(worksheetGraph, path);
      ((PXSelectBase<SOPickerListEntry>) worksheetGraph.pickerList).Insert(this.PickerListEntryFrom(array[index].Item1, array[index].Item2));
    }
  }

  protected virtual void SpreadSplitsByItems(
    SOPickingWorksheetReview worksheetGraph,
    IEnumerable<(SOPickingWorksheetLineSplit Split, string UOM)> splits,
    int maxPickersCount)
  {
    int?[] array1 = splits.Select<(SOPickingWorksheetLineSplit, string), int?>((Func<(SOPickingWorksheetLineSplit, string), int?>) (s => s.Split.InventoryID)).Distinct<int?>().ToArray<int?>();
    (SOPickingWorksheetLineSplit, string)[] sortedSplits = splits.OrderBy<(SOPickingWorksheetLineSplit, string), int?>((Func<(SOPickingWorksheetLineSplit, string), int?>) (s => s.Split.InventoryID)).ToArray<(SOPickingWorksheetLineSplit, string)>();
    int d2 = Math.Min(maxPickersCount, array1.Length);
    int size = (int) Math.Ceiling(Decimal.Divide((Decimal) array1.Length, (Decimal) d2));
    int[] array2 = ((IEnumerable<int?>) ((IEnumerable<int?>) array1).Batch<int?, int?>(size, (Func<IEnumerable<int?>, int?>) (batch => batch.First<int?>())).Skip<int?>(1).ToArray<int?>()).Select<int?, int>((Func<int?, int>) (item => EnumerableExtensions.SelectIndexesWhere<(SOPickingWorksheetLineSplit, string)>((IEnumerable<(SOPickingWorksheetLineSplit, string)>) sortedSplits, (Func<(SOPickingWorksheetLineSplit, string), bool>) (s =>
    {
      int? inventoryId = s.Split.InventoryID;
      int? nullable = item;
      return inventoryId.GetValueOrDefault() == nullable.GetValueOrDefault() & inventoryId.HasValue == nullable.HasValue;
    })).First<int>())).ToArray<int>();
    INLocation inLocation = new INLocation()
    {
      PathPriority = new int?(0)
    };
    WMSPath path = new WMSPath("path", (IEnumerable<INLocation>) new INLocation[1]
    {
      inLocation
    });
    for (int index = 0; index < sortedSplits.Length; ++index)
    {
      if (index == 0 || EnumerableExtensions.IsIn<int>(index, (IEnumerable<int>) array2))
      {
        inLocation.LocationID = sortedSplits[index].Item1.LocationID;
        this.InsertPath(worksheetGraph, path);
      }
      ((PXSelectBase<SOPickerListEntry>) worksheetGraph.pickerList).Insert(this.PickerListEntryFrom(sortedSplits[index].Item1, sortedSplits[index].Item2));
    }
  }

  protected virtual void SpreadSplitsByLocations(
    SOPickingWorksheetReview worksheetGraph,
    IEnumerable<(SOPickingWorksheetLineSplit Split, string UOM)> splits,
    INLocation[] locations)
  {
    (SOPickingWorksheetLineSplit, string)[] array = EnumerableExtensions.OrderBy<(SOPickingWorksheetLineSplit, string), int?>(splits, (Func<(SOPickingWorksheetLineSplit, string), int?>) (s => s.Split.LocationID), ((IEnumerable<INLocation>) locations).Select<INLocation, int?>((Func<INLocation, int?>) (loc => loc.LocationID)).ToArray<int?>()).ToArray<(SOPickingWorksheetLineSplit, string)>();
    INLocation inLocation = new INLocation()
    {
      PathPriority = new int?(0)
    };
    WMSPath path = new WMSPath("path", (IEnumerable<INLocation>) new INLocation[1]
    {
      inLocation
    });
    for (int index = 0; index < array.Length; ++index)
    {
      if (index != 0)
      {
        int? locationId1 = array[index].Item1.LocationID;
        int? locationId2 = array[index - 1].Item1.LocationID;
        if (locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue)
          goto label_4;
      }
      inLocation.LocationID = array[index].Item1.LocationID;
      this.InsertPath(worksheetGraph, path);
label_4:
      ((PXSelectBase<SOPickerListEntry>) worksheetGraph.pickerList).Insert(this.PickerListEntryFrom(array[index].Item1, array[index].Item2));
    }
  }

  protected virtual void InsertPath(SOPickingWorksheetReview worksheetGraph, WMSPath path)
  {
    ((PXSelectBase<SOPicker>) worksheetGraph.pickers).Insert(new SOPicker()
    {
      PathLength = new int?(path.PathLength),
      FirstLocationID = path.First<INLocation>().LocationID,
      LastLocationID = path.Last<INLocation>().LocationID
    });
    if (!PXAccess.FeatureInstalled<FeaturesSet.wMSPaperlessPicking>())
      return;
    FbqlSelect<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOPickingJob.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickingJob.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickingJob>, SOPicker, SOPickingJob>.SameAsCurrent>, SOPickingJob>.View pickerJob = worksheetGraph.pickerJob;
    SOPickingJob soPickingJob = new SOPickingJob();
    soPickingJob.Status = this.ProcessSettings.SendToQueue.GetValueOrDefault() ? "ENQ" : (string) null;
    soPickingJob.AutomaticShipmentConfirmation = this.ProcessSettings.AutomaticShipmentConfirmation;
    ((PXSelectBase<SOPickingJob>) pickerJob).Insert(soPickingJob);
  }

  protected virtual void CreateSinglePickList(
    SOPickingWorksheetProcess.HeaderSettings settings,
    IEnumerable<SOShipment> shipments)
  {
    if (!shipments.Any<SOShipment>())
      return;
    if (shipments.GroupBy<SOShipment, int?>((Func<SOShipment, int?>) (s => s.SiteID)).Count<IGrouping<int?, SOShipment>>() > 1)
      throw new PXInvalidOperationException("All selected shipments should have same Site ID.");
    SOPickingWorksheetReview instance = PXGraph.CreateInstance<SOPickingWorksheetReview>();
    List<SOPickingWorksheet> rows = new List<SOPickingWorksheet>();
    int num = 0;
    foreach (SOShipment shipment in shipments)
    {
      ++num;
      if (this.HasNonStockLinesWithEmptyLocation(shipment.ShipmentNbr))
      {
        PXProcessing<SOShipment>.SetError(num - 1, PXMessages.LocalizeFormat("The picking worksheet cannot be created because one or multiple shipments contain non-stock items with empty location: {0}.", new object[1]
        {
          (object) shipment.ShipmentNbr
        }));
      }
      else
      {
        IEnumerable<SOShipment> shipments1 = EnumerableExtensions.AsSingleEnumerable<SOShipment>((SOShipment) PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.Find((PXGraph) instance, (SOShipment.shipmentNbr) shipment, (PKFindOptions) 0));
        IEnumerable<SOPickingWorksheetLineSplit> worksheetLineSplits = this.MergeSplits(shipments1);
        if (worksheetLineSplits.Any<SOPickingWorksheetLineSplit>())
        {
          SOPickingWorksheet worksheet = this.CreateWorksheet(instance, "SS", shipment.SiteID, worksheetLineSplits).worksheet;
          ((PXSelectBase<SOPickingWorksheet>) instance.worksheet).SetValueExt<SOPickingWorksheet.singleShipmentNbr>(worksheet, (object) shipment.ShipmentNbr);
          this.LinkShipments(instance, shipments1, worksheet);
          this.SpreadShipmentsAmongPickers(instance, shipments1, 1, 1);
          ((PXAction) instance.Save).Press();
          rows.Add(((PXSelectBase<SOPickingWorksheet>) instance.worksheet).Current);
          ((PXGraph) instance).Clear();
          ((PXGraph) instance).SelectTimeStamp();
        }
      }
    }
    if (!settings.PrintPickLists.GetValueOrDefault())
      return;
    EnumerableExtensions.Consume(((PXAction) instance.PrintPickList).Press(this.CreateAdapter<SOPickingWorksheet>((PXGraph) instance, settings, (IEnumerable<SOPickingWorksheet>) rows)));
  }

  private bool HasAnyShipmentContainsNonStockShipLinesWithEmptyLocation(
    IEnumerable<SOShipment> shipments)
  {
    bool flag = false;
    foreach (SOShipment shipment in shipments)
    {
      if (this.HasNonStockLinesWithEmptyLocation(shipment.ShipmentNbr))
      {
        PXTrace.WriteError("The picking worksheet cannot be created because one or multiple shipments contain non-stock items with empty location: {0}.", new object[1]
        {
          (object) shipment.ShipmentNbr
        });
        flag = true;
      }
    }
    return flag;
  }

  public virtual bool HasNonStockLinesWithEmptyLocation(string shipmentNbr)
  {
    return PXResultset<SOShipLine>.op_Implicit(PXSelectBase<SOShipLine, PXViewOf<SOShipLine>.BasedOn<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<SOShipLine.FK.InventoryItem>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.kitItem, IBqlBool>.IsEqual<False>>>, And<BqlOperand<SOShipLine.locationID, IBqlInt>.IsNull>>>.And<BqlOperand<SOShipLine.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) shipmentNbr
    })) != null;
  }

  protected virtual IEnumerable<SOPickingWorksheetLineSplit> MergeSplits(
    IEnumerable<SOShipment> shipments)
  {
    List<SOPickingWorksheetLineSplit> source = new List<SOPickingWorksheetLineSplit>();
    NonStockKitSpecHelper kitSpecHelper = new NonStockKitSpecHelper((PXGraph) PXGraph.CreateInstance<SOPickingWorksheetReview>());
    foreach (SOShipment shipment in shipments)
    {
      foreach (PXResult<SOShipLineSplit, SOShipLine> shipmentSplit in this.GetShipmentSplits(shipment))
      {
        SOShipLineSplit soShipLineSplit;
        SOShipLine soShipLine;
        shipmentSplit.Deconstruct(ref soShipLineSplit, ref soShipLine);
        SOShipLineSplit split = soShipLineSplit;
        SOShipLine line = soShipLine;
        string lineUOM = this.GetLineUOM(kitSpecHelper, split, line);
        SOPickingWorksheetLineSplit worksheetLineSplit1 = split.IsUnassigned.GetValueOrDefault() ? (SOPickingWorksheetLineSplit) null : source.FirstOrDefault<SOPickingWorksheetLineSplit>((Func<SOPickingWorksheetLineSplit, bool>) (s =>
        {
          int? inventoryId1 = s.InventoryID;
          int? inventoryId2 = split.InventoryID;
          if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
          {
            int? subItemId1 = s.SubItemID;
            int? subItemId2 = split.SubItemID;
            if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
            {
              int? siteId1 = s.SiteID;
              int? siteId2 = split.SiteID;
              if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
              {
                int? locationId1 = s.LocationID;
                int? locationId2 = split.LocationID;
                if (locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue && s.UOM == lineUOM)
                  return string.Equals(s.LotSerialNbr, split.LotSerialNbr, StringComparison.OrdinalIgnoreCase);
              }
            }
          }
          return false;
        }));
        if (worksheetLineSplit1 == null)
          worksheetLineSplit1 = new SOPickingWorksheetLineSplit()
          {
            InventoryID = split.InventoryID,
            SubItemID = split.SubItemID,
            SiteID = split.SiteID,
            LocationID = split.LocationID,
            UOM = lineUOM,
            LotSerialNbr = split.LotSerialNbr,
            ExpireDate = split.ExpireDate,
            Qty = new Decimal?(0M),
            IsUnassigned = split.IsUnassigned,
            HasGeneratedLotSerialNbr = split.HasGeneratedLotSerialNbr
          };
        SOPickingWorksheetLineSplit worksheetLineSplit2 = worksheetLineSplit1;
        SOPickingWorksheetLineSplit worksheetLineSplit3 = worksheetLineSplit2;
        Decimal? qty1 = worksheetLineSplit3.Qty;
        Decimal? qty2 = split.Qty;
        worksheetLineSplit3.Qty = qty1.HasValue & qty2.HasValue ? new Decimal?(qty1.GetValueOrDefault() + qty2.GetValueOrDefault()) : new Decimal?();
        if (worksheetLineSplit1 == null)
          source.Add(worksheetLineSplit2);
      }
    }
    return (IEnumerable<SOPickingWorksheetLineSplit>) source;
  }

  protected virtual (SOPickingWorksheet worksheet, IEnumerable<SOPickingWorksheetLine> lines, IEnumerable<SOPickingWorksheetLineSplit> splits) CreateWorksheet(
    SOPickingWorksheetReview worksheetGraph,
    string worksheetType,
    int? siteID,
    IEnumerable<SOPickingWorksheetLineSplit> worksheetLineSplits)
  {
    SOPickingWorksheet pickingWorksheet = ((PXSelectBase<SOPickingWorksheet>) worksheetGraph.worksheet).Insert(new SOPickingWorksheet()
    {
      WorksheetType = worksheetType,
      SiteID = siteID
    });
    List<SOPickingWorksheetLine> pickingWorksheetLineList = new List<SOPickingWorksheetLine>();
    List<SOPickingWorksheetLineSplit> worksheetLineSplitList = new List<SOPickingWorksheetLineSplit>();
    foreach (IGrouping<\u003C\u003Ef__AnonymousType112<int?, int?, int?, string>, SOPickingWorksheetLineSplit> source in worksheetLineSplits.GroupBy(s => new
    {
      SiteID = s.SiteID,
      InventoryID = s.InventoryID,
      SubItemID = s.SubItemID,
      UOM = s.UOM
    }).ToArray<IGrouping<\u003C\u003Ef__AnonymousType112<int?, int?, int?, string>, SOPickingWorksheetLineSplit>>())
    {
      PX.Objects.IN.InventoryItem o = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, source.Key.InventoryID);
      INLotSerClass inLotSerClass = o.With<PX.Objects.IN.InventoryItem, INLotSerClass>((Func<PX.Objects.IN.InventoryItem, INLotSerClass>) (ii => (INLotSerClass) PrimaryKeyOf<INLotSerClass>.By<INLotSerClass.lotSerClassID>.ForeignKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.lotSerClassID>.FindParent((PXGraph) this, (PX.Objects.IN.InventoryItem.lotSerClassID) ii, (PKFindOptions) 0)));
      bool flag1 = source.Select<SOPickingWorksheetLineSplit, int?>((Func<SOPickingWorksheetLineSplit, int?>) (s => s.LocationID)).Distinct<int?>().Count<int?>() == 1;
      bool flag2 = source.Select<SOPickingWorksheetLineSplit, string>((Func<SOPickingWorksheetLineSplit, string>) (s => s.LotSerialNbr)).Distinct<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).Count<string>() == 1;
      SOPickingWorksheetLine pickingWorksheetLine = ((PXSelectBase<SOPickingWorksheetLine>) worksheetGraph.worksheetLines).Insert(new SOPickingWorksheetLine()
      {
        InventoryID = source.Key.InventoryID,
        SubItemID = source.Key.SubItemID,
        SiteID = source.Key.SiteID,
        UOM = source.Key.UOM,
        LocationID = flag1 ? source.First<SOPickingWorksheetLineSplit>().LocationID : new int?(),
        LotSerialNbr = inLotSerClass == null || inLotSerClass.LotSerTrack == "N" ? "" : (flag2 ? source.First<SOPickingWorksheetLineSplit>().LotSerialNbr : (string) null),
        ExpireDate = flag2 ? source.First<SOPickingWorksheetLineSplit>().ExpireDate : new DateTime?(),
        Qty = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) worksheetGraph.worksheetLines).Cache, source.Key.InventoryID, source.Key.UOM, source.Sum<SOPickingWorksheetLineSplit>((Func<SOPickingWorksheetLineSplit, Decimal>) (s => s.Qty.GetValueOrDefault())), INPrecision.NOROUND))
      });
      pickingWorksheetLineList.Add(pickingWorksheetLine);
      foreach (SOPickingWorksheetLineSplit worksheetLineSplit1 in (IEnumerable<SOPickingWorksheetLineSplit>) source)
      {
        worksheetLineSplit1.UOM = o.BaseUnit;
        SOPickingWorksheetLineSplit worksheetLineSplit2 = ((PXSelectBase<SOPickingWorksheetLineSplit>) worksheetGraph.worksheetLineSplits).Insert(worksheetLineSplit1);
        worksheetLineSplitList.Add(worksheetLineSplit2);
      }
    }
    return (pickingWorksheet, (IEnumerable<SOPickingWorksheetLine>) pickingWorksheetLineList, (IEnumerable<SOPickingWorksheetLineSplit>) worksheetLineSplitList);
  }

  protected virtual void LinkShipments(
    SOPickingWorksheetReview worksheetGraph,
    IEnumerable<SOShipment> shipments,
    SOPickingWorksheet worksheet)
  {
    foreach (SOShipment shipment in shipments)
    {
      SOPickingWorksheet pickingWorksheet1 = worksheet;
      Decimal? nullable1 = pickingWorksheet1.Qty;
      Decimal? nullable2 = shipment.ShipmentQty;
      Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
      Decimal? nullable3;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault1);
      pickingWorksheet1.Qty = nullable3;
      SOPickingWorksheet pickingWorksheet2 = worksheet;
      nullable1 = pickingWorksheet2.ShipmentWeight;
      nullable2 = shipment.ShipmentWeight;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      Decimal? nullable4;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault2);
      pickingWorksheet2.ShipmentWeight = nullable4;
      SOPickingWorksheet pickingWorksheet3 = worksheet;
      nullable1 = pickingWorksheet3.ShipmentVolume;
      nullable2 = shipment.ShipmentVolume;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      Decimal? nullable5;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable5 = nullable2;
      }
      else
        nullable5 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault3);
      pickingWorksheet3.ShipmentVolume = nullable5;
      shipment.CurrentWorksheetNbr = ((PXSelectBase<SOPickingWorksheet>) worksheetGraph.worksheet).Current.WorksheetNbr;
      ((PXSelectBase<SOShipment>) worksheetGraph.shipments).Update(shipment);
      ((PXSelectBase<SOPickingWorksheetShipment>) worksheetGraph.shipmentLinks).Insert(new SOPickingWorksheetShipment()
      {
        ShipmentNbr = shipment.ShipmentNbr
      });
    }
    ((PXSelectBase<SOPickingWorksheet>) worksheetGraph.worksheet).Update(worksheet);
  }

  protected virtual PXResult<SOShipLineSplit, SOShipLine>[] GetShipmentSplits(SOShipment shipment)
  {
    return ((IEnumerable<PXResult<SOShipLineSplit>>) PXSelectBase<SOShipLineSplit, PXViewOf<SOShipLineSplit>.BasedOn<SelectFromBase<SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<SOShipLineSplit.FK.ShipmentLine>>>.Where<KeysRelation<CompositeKey<Field<SOShipLine.shipmentType>.IsRelatedTo<SOShipment.shipmentType>, Field<SOShipLine.shipmentNbr>.IsRelatedTo<SOShipment.shipmentNbr>>.WithTablesOf<SOShipment, SOShipLine>, SOShipment, SOShipLine>.SameAsCurrent>>.Config>.SelectMultiBound((PXGraph) this, (object[]) new SOShipment[1]
    {
      shipment
    }, Array.Empty<object>())).AsEnumerable<PXResult<SOShipLineSplit>>().Cast<PXResult<SOShipLineSplit, SOShipLine>>().Concat<PXResult<SOShipLineSplit, SOShipLine>>(((IEnumerable<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>) PXSelectBase<PX.Objects.SO.Unassigned.SOShipLineSplit, PXViewOf<PX.Objects.SO.Unassigned.SOShipLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.Unassigned.SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<PX.Objects.SO.Unassigned.SOShipLineSplit.FK.ShipmentLine>>>.Where<KeysRelation<CompositeKey<Field<SOShipLine.shipmentType>.IsRelatedTo<SOShipment.shipmentType>, Field<SOShipLine.shipmentNbr>.IsRelatedTo<SOShipment.shipmentNbr>>.WithTablesOf<SOShipment, SOShipLine>, SOShipment, SOShipLine>.SameAsCurrent>>.Config>.SelectMultiBound((PXGraph) this, (object[]) new SOShipment[1]
    {
      shipment
    }, Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>().Cast<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLine>>().Select<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLine>, PXResult<SOShipLineSplit, SOShipLine>>((Func<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLine>, PXResult<SOShipLineSplit, SOShipLine>>) (r => new PXResult<SOShipLineSplit, SOShipLine>(this.MakeAssigned(PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLine>.op_Implicit(r)), PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLine>.op_Implicit(r))))).ToArray<PXResult<SOShipLineSplit, SOShipLine>>();
  }

  protected virtual INLocation[] GetSortedLocations(IEnumerable<int?> locationIDs)
  {
    INLocation[] array = ((IEnumerable<INLocation>) locationIDs.Distinct<int?>().Select<int?, INLocation>((Func<int?, INLocation>) (id => INLocation.PK.Find((PXGraph) this, id))).OrderBy<INLocation, int?>((Func<INLocation, int?>) (l => l.PathPriority)).ThenBy<INLocation, string>((Func<INLocation, string>) (l => l.LocationCD)).ToArray<INLocation>()).Select<INLocation, INLocation>(new Func<INLocation, INLocation>(PXCache<INLocation>.CreateCopy)).ToArray<INLocation>();
    int num = 0;
    foreach (INLocation inLocation in array)
    {
      if (inLocation.PathPriority.HasValue)
        num = inLocation.PathPriority.Value;
      else
        inLocation.PathPriority = new int?(++num);
    }
    return array;
  }

  protected virtual SOPickerListEntry PickerListEntryFrom(
    SOPickingWorksheetLineSplit split,
    string orderLineUom)
  {
    return new SOPickerListEntry()
    {
      SiteID = split.SiteID,
      LocationID = split.LocationID,
      InventoryID = split.InventoryID,
      SubItemID = split.SubItemID,
      LotSerialNbr = split.LotSerialNbr,
      ExpireDate = split.ExpireDate,
      UOM = split.UOM,
      Qty = split.Qty,
      IsUnassigned = split.IsUnassigned,
      HasGeneratedLotSerialNbr = split.HasGeneratedLotSerialNbr,
      OrderLineUOM = orderLineUom
    };
  }

  protected virtual SOPickerListEntry PickerListEntryFrom(
    SOShipLineSplit split,
    string orderLineUom,
    string shipmentNbr)
  {
    return new SOPickerListEntry()
    {
      ShipmentNbr = shipmentNbr,
      SiteID = split.SiteID,
      LocationID = split.LocationID,
      InventoryID = split.InventoryID,
      SubItemID = split.SubItemID,
      LotSerialNbr = split.LotSerialNbr,
      ExpireDate = split.ExpireDate,
      UOM = split.UOM,
      Qty = split.Qty,
      IsUnassigned = split.IsUnassigned,
      HasGeneratedLotSerialNbr = split.HasGeneratedLotSerialNbr,
      OrderLineUOM = orderLineUom
    };
  }

  protected IEnumerable<(INLocation source, INLocation target, int distance)> GetSortedDistancesBetween(
    IEnumerable<INLocation> locations)
  {
    List<(INLocation, INLocation, int)> source = new List<(INLocation, INLocation, int)>();
    using (IEnumerator<INLocation> enumerator = locations.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        INLocation current1;
        INLocation inLocation1 = current1 = enumerator.Current;
        while (enumerator.MoveNext())
        {
          List<(INLocation, INLocation, int)> valueTupleList = source;
          INLocation inLocation2 = inLocation1;
          INLocation current2 = enumerator.Current;
          int? pathPriority = enumerator.Current.PathPriority;
          int num1 = 1 + pathPriority.Value;
          pathPriority = inLocation1.PathPriority;
          int num2 = pathPriority.Value;
          int num3 = num1 - num2;
          (INLocation, INLocation, int) valueTuple = (inLocation2, current2, num3);
          valueTupleList.Add(valueTuple);
          inLocation1 = enumerator.Current;
        }
      }
    }
    return (IEnumerable<(INLocation, INLocation, int)>) source.OrderByDescending<(INLocation, INLocation, int), int>((Func<(INLocation, INLocation, int), int>) (d => d.distance)).ThenBy<(INLocation, INLocation, int), string>((Func<(INLocation, INLocation, int), string>) (d => d.source.LocationCD)).ToArray<(INLocation, INLocation, int)>();
  }

  private SOShipLineSplit MakeAssigned(PX.Objects.SO.Unassigned.SOShipLineSplit unassignedSplit)
  {
    return PropertyTransfer.Transfer<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLineSplit>(unassignedSplit, new SOShipLineSplit());
  }

  public class ProcessAction
  {
    public const string None = "N";
    public const string CreateWavePickList = "W";
    public const string CreateBatchPickList = "B";
    public const string CreateSinglePickList = "S";

    public class none : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickingWorksheetProcess.ProcessAction.none>
    {
      public none()
        : base("N")
      {
      }
    }

    public class createWavePickList : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickingWorksheetProcess.ProcessAction.createWavePickList>
    {
      public createWavePickList()
        : base("W")
      {
      }
    }

    public class createBatchPickList : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickingWorksheetProcess.ProcessAction.createBatchPickList>
    {
      public createBatchPickList()
        : base("B")
      {
      }
    }

    public class createSinglePickList : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickingWorksheetProcess.ProcessAction.createSinglePickList>
    {
      public createSinglePickList()
        : base("S")
      {
      }
    }

    [PXLocalizable]
    public abstract class DisplayNames
    {
      public const string None = "<SELECT>";
      public const string CreateWavePickList = "Create Wave Pick Lists";
      public const string CreateBatchPickList = "Create Batch Pick Lists";
      public const string CreateSinglePickList = "Create Single-Shipment Pick Lists";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[4]
        {
          PXStringListAttribute.Pair("N", "<SELECT>"),
          PXStringListAttribute.Pair("S", "Create Single-Shipment Pick Lists"),
          PXStringListAttribute.Pair("W", "Create Wave Pick Lists"),
          PXStringListAttribute.Pair("B", "Create Batch Pick Lists")
        })
      {
      }
    }
  }

  [PXCacheName("Filter")]
  public class HeaderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Action", Required = true)]
    [PXUnboundDefault(typeof (BqlOperand<SOPickingWorksheetProcess.ProcessAction.none, IBqlString>.When<Where<FeatureInstalled<FeaturesSet.wMSAdvancedPicking>>>.Else<SOPickingWorksheetProcess.ProcessAction.createSinglePickList>))]
    [SOPickingWorksheetProcess.ProcessAction.List]
    public virtual string Action { get; set; }

    [Site(Required = true)]
    [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<AccessInfo.branchID>>>))]
    public virtual int? SiteID { get; set; }

    [PXDate]
    [PXUIField(DisplayName = "Start Date")]
    public virtual DateTime? StartDate { get; set; }

    [PXDate]
    [PXUnboundDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "End Date")]
    public virtual DateTime? EndDate { get; set; }

    [Customer]
    public virtual int? CustomerID { get; set; }

    [PXUIField]
    [PXActiveCarrierPluginSelectorUnbound(typeof (Search<CarrierPlugin.carrierPluginID>))]
    public virtual string CarrierPluginID { get; set; }

    [PXUIField(DisplayName = "Ship Via")]
    [PXActiveCarrierSelectorUnbound(typeof (Search<PX.Objects.CS.Carrier.carrierID, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOPickingWorksheetProcess.HeaderFilter.carrierPluginID>, IsNull>>>>.Or<BqlOperand<PX.Objects.CS.Carrier.carrierPluginID, IBqlString>.IsEqual<BqlField<SOPickingWorksheetProcess.HeaderFilter.carrierPluginID, IBqlString>.FromCurrent>>>>), DescriptionField = typeof (PX.Objects.CS.Carrier.description), CacheGlobal = true)]
    public virtual string ShipVia { get; set; }

    [PXString(1, IsFixed = true)]
    [PXUnboundDefault("B")]
    [SOPackageType.ForFiltering.List]
    [PXUIField(DisplayName = "Packaging Type")]
    public virtual string PackagingType { get; set; }

    [Inventory]
    public virtual int? InventoryID { get; set; }

    [Location(typeof (SOPickingWorksheetProcess.HeaderFilter.siteID))]
    public virtual int? LocationID { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Max. Number of Lines in Shipment")]
    public int? MaxNumberOfLinesInShipment { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Max. Quantity in Lines")]
    public int? MaxQtyInLines { get; set; }

    public abstract class action : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderFilter.action>
    {
    }

    public abstract class siteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderFilter.siteID>
    {
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderFilter.startDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderFilter.endDate>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderFilter.customerID>
    {
    }

    public abstract class carrierPluginID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderFilter.carrierPluginID>
    {
    }

    public abstract class shipVia : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderFilter.shipVia>
    {
    }

    public abstract class packagingType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderFilter.packagingType>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderFilter.inventoryID>
    {
    }

    public abstract class locationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderFilter.locationID>
    {
    }

    public abstract class maxNumberOfLinesInShipment : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderFilter.maxNumberOfLinesInShipment>
    {
    }

    public abstract class maxQtyInLines : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderFilter.maxQtyInLines>
    {
    }
  }

  public sealed class HeaderSettings : 
    PXCacheExtension<SOPickingWorksheetProcess.HeaderFilter>,
    IPrintable
  {
    [PXInt(MinValue = 1)]
    [PXUnboundDefault(1)]
    [PXUIField(DisplayName = "Max. Number of Pickers")]
    [PXUIVisible(typeof (BqlOperand<SOPickingWorksheetProcess.HeaderFilter.action, IBqlString>.IsIn<SOPickingWorksheetProcess.ProcessAction.createWavePickList, SOPickingWorksheetProcess.ProcessAction.createBatchPickList>))]
    public int? NumberOfPickers { get; set; }

    [PXInt(MinValue = 1)]
    [PXUnboundDefault(0)]
    [PXUIField(DisplayName = "Max. Number of Totes per Picker")]
    [PXUIVisible(typeof (BqlOperand<SOPickingWorksheetProcess.HeaderFilter.action, IBqlString>.IsEqual<SOPickingWorksheetProcess.ProcessAction.createWavePickList>))]
    public int? NumberOfTotesPerPicker { get; set; }

    [PXBool]
    [PXUnboundDefault(false)]
    [PXUIField(DisplayName = "Send to Picking Queue")]
    [PXUIVisible(typeof (BqlOperand<SOPickingWorksheetProcess.HeaderFilter.action, IBqlString>.IsIn<SOPickingWorksheetProcess.ProcessAction.createWavePickList, SOPickingWorksheetProcess.ProcessAction.createBatchPickList, SOPickingWorksheetProcess.ProcessAction.createSinglePickList>))]
    public bool? SendToQueue { get; set; }

    [PXBool]
    [PXUnboundDefault(false)]
    [PXUIField(DisplayName = "Print Pick Lists")]
    [PXUIVisible(typeof (BqlOperand<SOPickingWorksheetProcess.HeaderFilter.action, IBqlString>.IsIn<SOPickingWorksheetProcess.ProcessAction.createWavePickList, SOPickingWorksheetProcess.ProcessAction.createBatchPickList, SOPickingWorksheetProcess.ProcessAction.createSinglePickList>))]
    public bool? PrintPickLists { get; set; }

    [PXBool]
    [PXUnboundDefault(false)]
    [PXFormula(typeof (BqlOperand<False, IBqlBool>.When<BqlOperand<SOPickingWorksheetProcess.HeaderFilter.action, IBqlString>.IsNotEqual<SOPickingWorksheetProcess.ProcessAction.createSinglePickList>>.Else<SOPickingWorksheetProcess.HeaderSettings.automaticShipmentConfirmation>))]
    [PXUIField(DisplayName = "Confirm Shipment on Pick List Confirmation")]
    [PXUIVisible(typeof (BqlOperand<SOPickingWorksheetProcess.HeaderFilter.action, IBqlString>.IsEqual<SOPickingWorksheetProcess.ProcessAction.createSinglePickList>))]
    public bool? AutomaticShipmentConfirmation { get; set; }

    [PXBool]
    [PXUnboundDefault(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingWorksheetProcess.HeaderSettings.printPickLists, Equal<True>>>>>.And<FeatureInstalled<FeaturesSet.deviceHub>>))]
    [PXFormula(typeof (BqlOperand<False, IBqlBool>.When<BqlOperand<SOPickingWorksheetProcess.HeaderSettings.printPickLists, IBqlBool>.IsEqual<False>>.Else<SOPickingWorksheetProcess.HeaderSettings.printWithDeviceHub>))]
    [PXUIField(DisplayName = "Print with DeviceHub", FieldClass = "DeviceHub")]
    [PXUIVisible(typeof (SOPickingWorksheetProcess.HeaderSettings.printPickLists))]
    public bool? PrintWithDeviceHub { get; set; }

    [PXBool]
    [PXUnboundDefault(true)]
    [PXUIVisible(typeof (SOPickingWorksheetProcess.HeaderSettings.printWithDeviceHub))]
    [PXUIEnabled(typeof (SOPickingWorksheetProcess.HeaderSettings.printWithDeviceHub))]
    [PXFormula(typeof (BqlOperand<False, IBqlBool>.When<BqlOperand<SOPickingWorksheetProcess.HeaderSettings.printWithDeviceHub, IBqlBool>.IsEqual<False>>.Else<SOPickingWorksheetProcess.HeaderSettings.definePrinterManually>))]
    [PXUIField(DisplayName = "Define Printer Manually", FieldClass = "DeviceHub")]
    public bool? DefinePrinterManually { get; set; } = new bool?(false);

    [PXPrinterSelector]
    [PXUIVisible(typeof (SOPickingWorksheetProcess.HeaderSettings.definePrinterManually))]
    [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlOperand<SOPickingWorksheetProcess.HeaderSettings.definePrinterManually, IBqlBool>.IsEqual<False>>.Else<SOPickingWorksheetProcess.HeaderSettings.printerID>))]
    public Guid? PrinterID { get; set; }

    [PXInt]
    public int? NumberOfCopies { get; set; }

    public abstract class numberOfPickers : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderSettings.numberOfPickers>
    {
    }

    public abstract class numberOfTotesPerPicker : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderSettings.numberOfTotesPerPicker>
    {
    }

    public abstract class sendToQueue : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderSettings.sendToQueue>
    {
    }

    public abstract class printPickLists : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderSettings.printPickLists>
    {
    }

    public abstract class automaticShipmentConfirmation : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderSettings.automaticShipmentConfirmation>
    {
    }

    public abstract class printWithDeviceHub : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderSettings.printWithDeviceHub>
    {
    }

    public abstract class definePrinterManually : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderSettings.definePrinterManually>
    {
    }

    public abstract class printerID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderSettings.printerID>
    {
    }

    public abstract class numberOfCopies : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      SOPickingWorksheetProcess.HeaderSettings.numberOfCopies>
    {
    }
  }

  public class InventoryFilterExt : 
    InventoryLinkFilterExtensionBase<SOPickingWorksheetProcess, SOPickingWorksheetProcess.HeaderFilter, SOPickingWorksheetProcess.HeaderFilter.inventoryID>
  {
    [PXMergeAttributes]
    [Inventory(IsKey = true)]
    protected override void _(
      PX.Data.Events.CacheAttached<InventoryLinkFilter.inventoryID> e)
    {
    }

    /// Overrides <see cref="M:PX.Objects.SO.SOPickingWorksheetProcess.AppendFilter(PX.Data.BqlCommand,System.Collections.Generic.IList{System.Object},PX.Objects.SO.SOPickingWorksheetProcess.HeaderFilter)" />
    [PXOverride]
    public BqlCommand AppendFilter(
      BqlCommand cmd,
      IList<object> parameters,
      SOPickingWorksheetProcess.HeaderFilter filter,
      Func<BqlCommand, IList<object>, SOPickingWorksheetProcess.HeaderFilter, BqlCommand> base_AppendFilter)
    {
      cmd = base_AppendFilter(cmd, parameters, filter);
      int?[] array = this.GetSelectedEntities(filter).ToArray<int?>();
      if (array.Length != 0)
      {
        cmd = cmd.WhereAnd<Where<Not<Exists<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.shipmentNbr, Equal<SOShipment.shipmentNbr>>>>>.And<BqlOperand<SOShipLine.inventoryID, IBqlInt>.IsNotIn<P.AsInt>>>>>>>();
        parameters.Add((object) array);
      }
      return cmd;
    }

    public class descr : 
      InventoryLinkFilterExtensionBase<SOPickingWorksheetProcess, SOPickingWorksheetProcess.HeaderFilter, SOPickingWorksheetProcess.HeaderFilter.inventoryID>.AttachedInventoryDescription<SOPickingWorksheetProcess.InventoryFilterExt.descr>
    {
    }
  }

  public class LocationFilterExt : 
    LocationLinkFilterExtensionBase<SOPickingWorksheetProcess, SOPickingWorksheetProcess.HeaderFilter, SOPickingWorksheetProcess.HeaderFilter.locationID>
  {
    [PXMergeAttributes]
    [Location(typeof (SOPickingWorksheetProcess.HeaderFilter.siteID), IsKey = true)]
    protected override void _(
      PX.Data.Events.CacheAttached<LocationLinkFilter.locationID> e)
    {
    }

    /// Overrides <see cref="M:PX.Objects.SO.SOPickingWorksheetProcess.AppendFilter(PX.Data.BqlCommand,System.Collections.Generic.IList{System.Object},PX.Objects.SO.SOPickingWorksheetProcess.HeaderFilter)" />
    [PXOverride]
    public BqlCommand AppendFilter(
      BqlCommand cmd,
      IList<object> parameters,
      SOPickingWorksheetProcess.HeaderFilter filter,
      Func<BqlCommand, IList<object>, SOPickingWorksheetProcess.HeaderFilter, BqlCommand> base_AppendFilter)
    {
      cmd = base_AppendFilter(cmd, parameters, filter);
      int?[] array = this.GetSelectedEntities(filter).ToArray<int?>();
      if (array.Length != 0)
      {
        cmd = cmd.WhereAnd<Where<Not<Exists<SelectFromBase<SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLineSplit.shipmentNbr, Equal<SOShipment.shipmentNbr>>>>>.And<BqlOperand<SOShipLineSplit.locationID, IBqlInt>.IsNotIn<P.AsInt>>>>>>>();
        parameters.Add((object) array);
      }
      return cmd;
    }

    public class descr : 
      LocationLinkFilterExtensionBase<SOPickingWorksheetProcess, SOPickingWorksheetProcess.HeaderFilter, SOPickingWorksheetProcess.HeaderFilter.locationID>.AttachedLocationDescription<SOPickingWorksheetProcess.LocationFilterExt.descr>
    {
    }
  }

  [PXLocalizable]
  public abstract class Msg
  {
    public const string SingleSiteIDForAllShipmentsRequired = "All selected shipments should have same Site ID.";
    public const string OverallTotesShouldBeNotLessThanShipments = "Overall number of totes should be at least as many as number of shipments.";
    public const string ShipmentContainsNonStockItemWithEmptyLocation = "The {0} shipment cannot be added to the pick list because it contains non-stock item with empty location. This shipment should be processed manually via the Shipments (SO302000) form.";
    public const string OneOrMultipleShipmentContainsNonStockItemWithEmptyLocationSummary = "The picking worksheet cannot be created because one or multiple shipments contain non-stock items with empty location. For more details see Trace.";
    public const string OneOrMultipleShipmentContainsNonStockItemWithEmptyLocationInfo = "The picking worksheet cannot be created because one or multiple shipments contain non-stock items with empty location: {0}.";
    public const string ShipmentIsAlreadyProcessedOnPickPackShip = "The pick list for the {0} shipment cannot be created because this shipment is already being processed on the Pick, Pack, and Ship (SO302020) form.";
  }

  [PXLocalizable]
  public abstract class CacheNames
  {
    public const string Filter = "Filter";
  }
}
