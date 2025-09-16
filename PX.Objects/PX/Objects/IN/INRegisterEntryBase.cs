// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INRegisterEntryBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.DependencyInjection;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.GL;
using PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public abstract class INRegisterEntryBase : 
  PXGraph<PXGraph, INRegister>,
  IGraphWithInitialization,
  PXImportAttribute.IPXPrepareItems
{
  private PXImportAttribute.ImportMode.Value _importMode;
  public PXSetup<INSetup> insetup;
  public PXInitializeState<INRegister> initializeState;
  public PXAction<INRegister> putOnHold;
  public PXAction<INRegister> releaseFromHold;
  public PXAction<INRegister> release;
  public PXAction<INRegister> viewBatch;
  public PXAction<INRegister> inventorySummary;
  public PXAction<INRegister> iNEdit;
  public PXAction<INRegister> iNRegisterDetails;
  public PXWorkflowEventHandler<INRegister> OnDocumentReleased;

  protected INRegisterEntryBase()
  {
    ((PXSelectBase) this.INTranDataMember).View.Attributes.OfType<PXImportAttribute>().First<PXImportAttribute>().RowImporting += new EventHandler<PXImportAttribute.RowImportingEventArgs>(this.ImportAttributeRowImporting);
  }

  protected virtual void ImportAttributeRowImporting(
    object sender,
    PXImportAttribute.RowImportingEventArgs e)
  {
    this._importMode = e.Mode;
  }

  public INTranSplitPlan TranSplitPlanExt => ((PXGraph) this).FindImplementation<INTranSplitPlan>();

  public abstract PXSelectBase<INRegister> INRegisterDataMember { get; }

  public abstract PXSelectBase<INTran> INTranDataMember { get; }

  public abstract PXSelectBase<INTranSplit> INTranSplitDataMember { get; }

  public abstract PXSelectBase<INTran> LSSelectDataMember { get; }

  protected abstract string ScreenID { get; }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<INRegister>(new TableQuery[2]
    {
      new TableQuery((TransactionTypes) 108, typeof (INTran), (Func<PXGraph, PXDataFieldValue[]>) (graph =>
      {
        INRegisterEntryBase registerEntryBase = (INRegisterEntryBase) graph;
        return new PXDataFieldValue[3]
        {
          (PXDataFieldValue) new PXDataFieldValue<INTran.docType>((PXDbType) 3, (object) registerEntryBase.INRegisterDataMember.Current?.DocType),
          (PXDataFieldValue) new PXDataFieldValue<INTran.refNbr>((object) registerEntryBase.INRegisterDataMember.Current?.RefNbr),
          (PXDataFieldValue) new PXDataFieldValue<INTran.createdByScreenID>((PXDbType) 3, (object) registerEntryBase.ScreenID)
        };
      })),
      new TableQuery((TransactionTypes) 115, typeof (INTranSplit), (Func<PXGraph, PXDataFieldValue[]>) (graph =>
      {
        INRegisterEntryBase registerEntryBase = (INRegisterEntryBase) graph;
        return new PXDataFieldValue[3]
        {
          (PXDataFieldValue) new PXDataFieldValue<INTranSplit.docType>((PXDbType) 3, (object) registerEntryBase.INRegisterDataMember.Current?.DocType),
          (PXDataFieldValue) new PXDataFieldValue<INTranSplit.refNbr>((object) registerEntryBase.INRegisterDataMember.Current?.RefNbr),
          (PXDataFieldValue) new PXDataFieldValue<INTranSplit.createdByScreenID>((PXDbType) 3, (object) registerEntryBase.ScreenID)
        };
      }))
    });
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXProcessButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    INRegisterEntryBase.\u003C\u003Ec__DisplayClass27_0 cDisplayClass270 = new INRegisterEntryBase.\u003C\u003Ec__DisplayClass27_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass270.list = new List<INRegister>();
    foreach (INRegister inRegister in adapter.Get<INRegister>())
    {
      bool? nullable = inRegister.Hold;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = inRegister.Released;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          // ISSUE: reference to a compiler-generated field
          cDisplayClass270.list.Add(inRegister);
        }
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass270.list.Count == 0)
      throw new PXException("Document Status is invalid for processing.");
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass270.quickProcessFlow = adapter.QuickProcessFlow;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass270, __methodptr(\u003CRelease\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass270.list;
  }

  [PXLookupButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    INRegister current = this.INRegisterDataMember.Current;
    if (current != null && !string.IsNullOrEmpty(current.BatchNbr))
    {
      JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
      ((PXSelectBase<Batch>) instance.BatchModule).Current = PXResultset<Batch>.op_Implicit(((PXSelectBase<Batch>) instance.BatchModule).Search<Batch.batchNbr>((object) current.BatchNbr, new object[1]
      {
        (object) "IN"
      }));
      throw new PXRedirectRequiredException((PXGraph) instance, "Current batch record");
    }
    return adapter.Get();
  }

  [PXLookupButton(CommitChanges = true, VisibleOnDataSource = false)]
  [PXUIField]
  protected virtual IEnumerable InventorySummary(PXAdapter adapter)
  {
    PXCache cache = ((PXSelectBase) this.INTranDataMember).Cache;
    INTran current = this.INTranDataMember.Current;
    if (current == null)
      return adapter.Get();
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, current.InventoryID);
    if (inventoryItem != null && inventoryItem.StkItem.GetValueOrDefault())
    {
      INSubItem inSubItem = (INSubItem) PXSelectorAttribute.Select<INTran.subItemID>(cache, (object) current);
      InventorySummaryEnq.Redirect(inventoryItem.InventoryID, inSubItem?.SubItemCD, current.SiteID, current.LocationID);
    }
    return adapter.Get();
  }

  [PXLookupButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable INEdit(PXAdapter adapter)
  {
    INRegister current = this.INRegisterDataMember.Current;
    if (current != null)
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["DocType"] = current.DocType,
        ["RefNbr"] = current.RefNbr,
        ["PeriodTo"] = (string) null,
        ["PeriodFrom"] = (string) null
      }, "IN611000", (PXBaseRedirectException.WindowMode) 2, "Inventory Edit Details", (CurrentLocalization) null);
    return adapter.Get();
  }

  [PXLookupButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable INRegisterDetails(PXAdapter adapter)
  {
    INRegister current = this.INRegisterDataMember.Current;
    if (current != null)
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["DocType"] = current.DocType,
        ["RefNbr"] = current.RefNbr,
        ["PeriodID"] = (string) this.INRegisterDataMember.GetValueExt<INRegister.finPeriodID>(current)
      }, "IN614000", (PXBaseRedirectException.WindowMode) 2, "Inventory Register Detailed", (CurrentLocalization) null);
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowUpdated<INRegister> e)
  {
    bool flag1 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INRegister>>) e).Cache.ObjectsEqual<INRegister.tranDate>((object) e.Row, (object) e.OldRow);
    int num;
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INRegister>>) e).Cache.ObjectsEqual<INRegister.hold>((object) e.Row, (object) e.OldRow))
    {
      bool? hold = e.Row.Hold;
      bool flag2 = false;
      num = hold.GetValueOrDefault() == flag2 & hold.HasValue ? 1 : 0;
    }
    else
      num = 0;
    bool flag3 = num != 0;
    if (flag1 || flag3 && e.Row.OrigModule == "IN")
    {
      foreach (INTran selectChild in PXParentAttribute.SelectChildren(((PXSelectBase) this.INTranDataMember).Cache, (object) e.Row, typeof (INRegister)))
      {
        if (flag1 || this.IsNotAllowedZeroTran(selectChild))
          GraphHelper.MarkUpdated(((PXSelectBase) this.INTranDataMember).Cache, (object) selectChild);
      }
    }
    if (!flag1)
      return;
    foreach (INTranSplit selectChild in PXParentAttribute.SelectChildren(((PXSelectBase) this.INTranSplitDataMember).Cache, (object) e.Row, typeof (INRegister)))
      GraphHelper.MarkUpdated(((PXSelectBase) this.INTranSplitDataMember).Cache, (object) selectChild);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.invtMult> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.invtMult>, INTran, object>) e).NewValue = (object) INTranType.InvtMult(e.Row.TranType);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.uOM> e)
  {
    this.DefaultUnitCost(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.uOM>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.siteID> e)
  {
    this.DefaultUnitCost(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.siteID>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.inventoryID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.inventoryID>>) e).Cache.SetDefaultExt<INTran.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.inventoryID>>) e).Cache.SetDefaultExt<INTran.tranDesc>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<INTran> e)
  {
    if (e.Row.SortOrder.HasValue)
      return;
    e.Row.SortOrder = e.Row.LineNbr;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INTran> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) || !this.IsNotAllowedZeroTran(e.Row))
      return;
    bool? hold = (this.INRegisterDataMember.Current ?? PXParentAttribute.SelectParent<INRegister>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache, (object) e.Row)).Hold;
    bool flag = false;
    if (!(hold.GetValueOrDefault() == flag & hold.HasValue))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache.RaiseExceptionHandling<INTran.qty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
    {
      (object) 0.ToString()
    }));
  }

  protected virtual void SetProjectEnabled(PX.Data.Events.RowSelected<INTran> e)
  {
    if (e.Row == null)
      return;
    (bool? Project, bool? Task) tuple = this.IsProjectTaskEnabled(e.Row);
    if (tuple.Project.HasValue)
      PXUIFieldAttribute.SetEnabled<INTran.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, tuple.Project.Value);
    if (tuple.Task.HasValue)
      PXUIFieldAttribute.SetEnabled<INTran.taskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, tuple.Task.Value);
    (bool? ToProject, bool? ToTask) projectTaskEnabled = this.IsToProjectTaskEnabled(e.Row);
    if (projectTaskEnabled.ToProject.HasValue)
      PXUIFieldAttribute.SetEnabled<INTran.toProjectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, projectTaskEnabled.ToProject.Value);
    if (!projectTaskEnabled.ToTask.HasValue)
      return;
    PXUIFieldAttribute.SetEnabled<INTran.toTaskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, projectTaskEnabled.ToTask.Value);
  }

  protected virtual (bool? Project, bool? Task) IsProjectTaskEnabled(INTran row)
  {
    return (new bool?(), new bool?());
  }

  protected virtual (bool? ToProject, bool? ToTask) IsToProjectTaskEnabled(INTran row)
  {
    return (new bool?(), new bool?());
  }

  protected virtual bool IsNotAllowedZeroTran(INTran row)
  {
    if (row?.OrigModule == "IN")
    {
      Decimal? qty = row.Qty;
      Decimal num = 0M;
      if (qty.GetValueOrDefault() <= num & qty.HasValue)
      {
        if (EnumerableExtensions.IsIn<string>(row.DocType, "I", "T"))
          return true;
        return row.DocType == "R" && row.OrigDocType != "T";
      }
    }
    return false;
  }

  protected virtual void OnForeignTranInsert(INTran foreignTran)
  {
    INRegister objA = PXParentAttribute.SelectParent<INRegister>(((PXSelectBase) this.INTranDataMember).Cache, (object) foreignTran);
    if (objA == null)
      return;
    PXCache cache = ((PXSelectBase) this.INRegisterDataMember).Cache;
    object copy = cache.CreateCopy((object) objA);
    objA.SOShipmentType = foreignTran.SOShipmentType;
    objA.SOShipmentNbr = foreignTran.SOShipmentNbr;
    objA.SOOrderType = foreignTran.SOOrderType;
    objA.SOOrderNbr = foreignTran.SOOrderNbr;
    objA.POReceiptType = foreignTran.POReceiptType;
    objA.POReceiptNbr = foreignTran.POReceiptNbr;
    if (object.Equals((object) objA, cache.Current))
    {
      if (EnumerableExtensions.IsIn<PXEntryStatus>(cache.GetStatus((object) objA), (PXEntryStatus) 0, (PXEntryStatus) 5))
        GraphHelper.MarkUpdated(cache, (object) objA, true);
      cache.RaiseRowUpdated((object) objA, copy);
    }
    else
      cache.Update((object) objA);
  }

  protected void FillControlValue<TControlField, TTotalField>(PXCache cache, INRegister document)
    where TControlField : IBqlField, IImplement<IBqlDecimal>
    where TTotalField : IBqlField, IImplement<IBqlDecimal>
  {
    Decimal? val = (Decimal?) cache.GetValue<TTotalField>((object) document);
    if (PXCurrencyAttribute.IsNullOrEmpty(val))
      cache.SetValue<TControlField>((object) document, (object) 0M);
    else
      cache.SetValue<TControlField>((object) document, (object) val);
  }

  protected void RaiseControlValueError<TControlField, TTotalField>(
    PXCache cache,
    INRegister document)
    where TControlField : IBqlField, IImplement<IBqlDecimal>
    where TTotalField : IBqlField, IImplement<IBqlDecimal>
  {
    Decimal? nullable1 = (Decimal?) cache.GetValue<TControlField>((object) document);
    Decimal? nullable2 = (Decimal?) cache.GetValue<TTotalField>((object) document);
    Decimal? nullable3 = nullable1;
    if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue))
      cache.RaiseExceptionHandling<TControlField>((object) document, (object) nullable1, (Exception) new PXSetPropertyException("Document is out of balance."));
    else
      cache.RaiseExceptionHandling<TControlField>((object) document, (object) nullable1, (Exception) null);
  }

  public virtual void DefaultUnitCost(PXCache cache, INTran tran, bool setZero = false)
  {
    this.DefaultUnitAmount<INTran.unitCost>(cache, tran, setZero);
  }

  public virtual void DefaultUnitPrice(PXCache cache, INTran tran)
  {
    this.DefaultUnitAmount<INTran.unitPrice>(cache, tran);
  }

  protected virtual void DefaultUnitAmount<TUnitAmount>(PXCache cache, INTran tran, bool setZero = false) where TUnitAmount : IBqlField, IImplement<IBqlDecimal>
  {
    object obj;
    cache.RaiseFieldDefaulting<TUnitAmount>((object) tran, ref obj);
    if (!(obj is Decimal num) || !(num != 0M | setZero))
      return;
    Decimal? nullable = new Decimal?(INUnitAttribute.ConvertToBase<INTran.inventoryID>(cache, (object) tran, tran.UOM, num, INPrecision.UNITCOST));
    cache.SetValueExt<TUnitAmount>((object) tran, (object) nullable);
  }

  public CostCenterDispatcher CostCenterDispatcherExt
  {
    get => ((PXGraph) this).FindImplementation<CostCenterDispatcher>();
  }

  public virtual bool PrepareImportRow(
    string viewName,
    IDictionary dacKeys,
    IDictionary keyValuesInImport)
  {
    if (string.Compare(viewName, ((PXSelectBase) this.INTranDataMember).Name, StringComparison.OrdinalIgnoreCase) != 0 || !this.ShouldImportInventoryItem(keyValuesInImport))
      return false;
    if (((PXGraph) this).IsImportFromExcel && this._importMode == null || this._importMode == 1)
    {
      if (this.DuplicateFinder == null)
        this.DuplicateFinder = new MultiDuplicatesSearchEngine<INTran>(((PXSelectBase) this.INTranDataMember).Cache, (IEnumerable<Type>) this.GetAlternativeKeyFields(), (ICollection<INTran>) this.INTranDataMember.SelectMain(Array.Empty<object>()));
      INTran inTran = this.DuplicateFinder.Find(keyValuesInImport);
      if (inTran != null)
      {
        this.DuplicateFinder.RemoveItem(inTran);
        if (this._importMode != null)
          return false;
        if (dacKeys.Contains((object) "lineNbr"))
          dacKeys[(object) "LineNbr"] = (object) inTran.LineNbr;
        else
          dacKeys.Add((object) "LineNbr", (object) inTran.LineNbr);
      }
      else if (dacKeys.Contains((object) "lineNbr"))
      {
        bool flag = false;
        object dacKey = dacKeys[(object) "lineNbr"];
        if (((PXSelectBase) this.INTranDataMember).Cache.RaiseFieldUpdating<INTran.lineNbr>((object) null, ref dacKey) && dacKey is int num)
          flag = ((PXSelectBase) this.INTranDataMember).Cache.Locate((object) new INTran()
          {
            DocType = this.INRegisterDataMember.Current.DocType,
            RefNbr = this.INRegisterDataMember.Current.RefNbr,
            LineNbr = new int?(num)
          }) != null;
        if (flag)
          dacKeys.Remove((object) "lineNbr");
      }
    }
    return true;
  }

  public MultiDuplicatesSearchEngine<INTran> DuplicateFinder { get; set; }

  protected virtual Type[] GetAlternativeKeyFields()
  {
    return new List<Type>()
    {
      typeof (INTran.inventoryID),
      typeof (INTran.branchID),
      typeof (INTran.siteID),
      typeof (INTran.locationID),
      typeof (INTran.lotSerialNbr)
    }.ToArray();
  }

  public virtual bool ShouldImportInventoryItem(IDictionary values)
  {
    string key = "inventoryID";
    if (!values.Contains((object) key) || values[(object) key] == null)
      return false;
    InventoryItem inventoryItem = PXResultset<InventoryItem>.op_Implicit(PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItem.inventoryCD, Equal<Required<InventoryItem.inventoryCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) values[(object) key].ToString()
    }));
    return inventoryItem != null && EnumerableExtensions.IsNotIn<string>(inventoryItem.ItemStatus, "IN", "DE");
  }

  public virtual bool RowImporting(string viewName, object row) => row == null;

  public virtual bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }
}
