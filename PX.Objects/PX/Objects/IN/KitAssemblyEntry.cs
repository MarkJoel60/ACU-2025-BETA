// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.KitAssemblyEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public class KitAssemblyEntry : PXGraph<KitAssemblyEntry, INKitRegister>
{
  public PXSelect<INTran> dummy_intran;
  public PXSelect<INKitRegister, Where<INKitRegister.docType, Equal<Optional<INKitRegister.docType>>>> Document;
  public PXSelect<INKitRegister, Where<INKitRegister.docType, Equal<Current<INKitRegister.docType>>, And<INKitRegister.refNbr, Equal<Current<INKitRegister.refNbr>>>>> DocumentProperties;
  public PXSelectJoin<INComponentTran, InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<INComponentTran.inventoryID>>, LeftJoin<INKitSpecStkDet, On<INKitSpecStkDet.kitInventoryID, Equal<Current<INKitRegister.kitInventoryID>>, And2<Where<INKitSpecStkDet.compSubItemID, Equal<INComponentTran.subItemID>, Or<Where<INKitSpecStkDet.compSubItemID, IsNull, And<INComponentTran.subItemID, IsNull>>>>, And<INKitSpecStkDet.revisionID, Equal<Current<INKitRegister.kitRevisionID>>, And<INKitSpecStkDet.compInventoryID, Equal<INComponentTran.inventoryID>>>>>>>, Where<INComponentTran.docType, Equal<Current<INKitRegister.docType>>, And<INComponentTran.refNbr, Equal<Current<INKitRegister.refNbr>>, And<InventoryItem.stkItem, Equal<boolTrue>, And<INComponentTran.lineNbr, NotEqual<Current<INKitRegister.kitLineNbr>>>>>>> Components;
  public PXSelectJoin<INOverheadTran, InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<INOverheadTran.inventoryID>>, LeftJoin<INKitSpecNonStkDet, On<INKitSpecNonStkDet.kitInventoryID, Equal<Current<INKitRegister.kitInventoryID>>, And<INKitSpecNonStkDet.revisionID, Equal<Current<INKitRegister.kitRevisionID>>, And<INKitSpecNonStkDet.compInventoryID, Equal<INOverheadTran.inventoryID>>>>>>, Where<INOverheadTran.docType, Equal<Current<INKitRegister.docType>>, And<INOverheadTran.refNbr, Equal<Current<INKitRegister.refNbr>>, And<InventoryItem.stkItem, Equal<False>>>>> Overhead;
  public PXSelect<INKitSpecHdr, Where<INKitSpecHdr.kitInventoryID, Equal<Current<INKitRegister.kitInventoryID>>, And<INKitSpecHdr.revisionID, Equal<Current<INKitRegister.kitRevisionID>>>>> Spec;
  public PXSetup<INSetup> Setup;
  public PXInitializeState<INKitRegister> initializeState;
  public PXAction<INKitRegister> putOnHold;
  public PXAction<INKitRegister> releaseFromHold;
  public PXAction<INKitRegister> release;
  public PXAction<INKitRegister> viewBatch;
  protected bool _InternalCall;

  public INComponentLineSplittingExtension ComponentLineSplittingExt
  {
    get => ((PXGraph) this).FindImplementation<INComponentLineSplittingExtension>();
  }

  public INKitLineSplittingExtension KitLineSplittingExt
  {
    get => ((PXGraph) this).FindImplementation<INKitLineSplittingExtension>();
  }

  public KitAssemblyEntry()
  {
    ((PXSelectBase) this.Spec).Cache.AllowInsert = false;
    ((PXSelectBase) this.Spec).Cache.AllowDelete = false;
    ((PXSelectBase) this.Spec).Cache.AllowUpdate = false;
    OpenPeriodAttribute.SetValidatePeriod<INKitRegister.finPeriodID>(((PXSelectBase) this.Document).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter.negAvailQty>(KitAssemblyEntry.\u003C\u003Ec.\u003C\u003E9__12_0 ?? (KitAssemblyEntry.\u003C\u003Ec.\u003C\u003E9__12_0 = new PXFieldDefaulting((object) KitAssemblyEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__12_0))));
  }

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault(typeof (INKitRegister.docType))]
  protected virtual void _(PX.Data.Events.CacheAttached<INKitSerialPart.docType> e)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (INKitRegister.refNbr))]
  [PXParent(typeof (Select<INKitRegister, Where<INKitRegister.docType, Equal<Current<INKitSerialPart.docType>>, And<INKitRegister.refNbr, Equal<Current<INKitSerialPart.refNbr>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INKitSerialPart.refNbr> e)
  {
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
    KitAssemblyEntry.\u003C\u003Ec__DisplayClass21_0 cDisplayClass210 = new KitAssemblyEntry.\u003C\u003Ec__DisplayClass21_0();
    List<INKitRegister> inKitRegisterList = new List<INKitRegister>();
    foreach (INKitRegister inKitRegister in adapter.Get<INKitRegister>())
    {
      bool? nullable = inKitRegister.Hold;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = inKitRegister.Released;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          inKitRegisterList.Add(((PXSelectBase<INKitRegister>) this.Document).Update(inKitRegister));
      }
    }
    if (inKitRegisterList.Count == 0)
      throw new PXException("Document Status is invalid for processing.");
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.list = new List<INRegister>();
    foreach (INKitRegister inKitRegister in inKitRegisterList)
    {
      INRegister inRegister = PXResultset<INRegister>.op_Implicit(PXSelectBase<INRegister, PXSelect<INRegister, Where<INRegister.docType, Equal<Required<INRegister.docType>>, And<INRegister.refNbr, Equal<Required<INRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) inKitRegister.DocType,
        (object) inKitRegister.RefNbr
      }));
      // ISSUE: reference to a compiler-generated field
      cDisplayClass210.list.Add(inRegister);
    }
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass210, __methodptr(\u003CRelease\u003Eb__0)));
    return (IEnumerable) inKitRegisterList;
  }

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    if (((PXSelectBase<INKitRegister>) this.Document).Current != null && !string.IsNullOrEmpty(((PXSelectBase<INKitRegister>) this.Document).Current.BatchNbr))
    {
      JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
      ((PXSelectBase<Batch>) instance.BatchModule).Current = PXResultset<Batch>.op_Implicit(((PXSelectBase<Batch>) instance.BatchModule).Search<Batch.batchNbr>((object) ((PXSelectBase<INKitRegister>) this.Document).Current.BatchNbr, new object[1]
      {
        (object) "IN"
      }));
      throw new PXRedirectRequiredException((PXGraph) instance, "Current batch record");
    }
    return adapter.Get();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INKitRegister, INKitRegister.invtMult> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INKitRegister, INKitRegister.invtMult>, INKitRegister, object>) e).NewValue = (object) (e.Row.DocType == "D" ? new short?((short) -1) : new short?((short) 1));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INKitRegister, INKitRegister.projectID> e)
  {
    Contract nonProject;
    if (e.Row == null || !this.TryGetNonProject(out nonProject))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INKitRegister, INKitRegister.projectID>, INKitRegister, object>) e).NewValue = (object) nonProject.ContractID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INKitRegister, INKitRegister.kitInventoryID> e)
  {
    if (e.Row == null)
      return;
    PXResultset<INKitSpecHdr> pxResultset = PXSelectBase<INKitSpecHdr, PXSelect<INKitSpecHdr, Where<INKitSpecHdr.kitInventoryID, Equal<Current<INKitRegister.kitInventoryID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 2, Array.Empty<object>());
    if (pxResultset.Count == 1)
    {
      e.Row.KitRevisionID = PXResultset<INKitSpecHdr>.op_Implicit(pxResultset).RevisionID;
      e.Row.SubItemID = PXResultset<INKitSpecHdr>.op_Implicit(pxResultset).KitSubItemID;
    }
    else
      e.Row.KitRevisionID = (string) null;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INKitRegister, INKitRegister.kitInventoryID>>) e).Cache.SetDefaultExt<INKitRegister.uOM>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INKitRegister, INKitRegister.kitRevisionID> e)
  {
    if (e.Row == null)
      return;
    PXResultset<INKitSpecHdr> pxResultset = PXSelectBase<INKitSpecHdr, PXSelect<INKitSpecHdr, Where<INKitSpecHdr.kitInventoryID, Equal<Current<INKitRegister.kitInventoryID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 2, Array.Empty<object>());
    if (pxResultset == null)
      return;
    e.Row.SubItemID = PXResultset<INKitSpecHdr>.op_Implicit(pxResultset).KitSubItemID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INKitRegister, INKitRegister.kitRequestDate> e)
  {
    if (e.NewValue == null)
      return;
    e.Row.TranDate = (DateTime?) e.NewValue;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INKitRegister, INKitRegister.siteID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INKitRegister, INKitRegister.siteID>>) e).Cache.SetDefaultExt<INKitRegister.branchID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<INKitRegister> e)
  {
    if (e.Row == null || e.Row.TotalCostStock.HasValue && e.Row.TotalCostNonStock.HasValue)
      return;
    PXFormulaAttribute.CalcAggregate<INComponentTran.tranCost>(((PXSelectBase) this.Components).Cache, (object) e.Row, true);
    ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<INKitRegister>>) e).Cache.RaiseFieldUpdated<INKitRegister.totalCostStock>((object) e.Row, (object) null);
    PXFormulaAttribute.CalcAggregate<INOverheadTran.tranCost>(((PXSelectBase) this.Overhead).Cache, (object) e.Row, true);
    ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<INKitRegister>>) e).Cache.RaiseFieldUpdated<INKitRegister.totalCostNonStock>((object) e.Row, (object) null);
  }

  protected virtual void _(PX.Data.Events.RowSelected<INKitRegister> e)
  {
    if (e.Row == null)
      return;
    INKitSpecHdr inKitSpecHdr = PXResultset<INKitSpecHdr>.op_Implicit(((PXSelectBase<INKitSpecHdr>) this.Spec).Select(Array.Empty<object>()));
    bool? nullable;
    int num1;
    if (inKitSpecHdr == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = inKitSpecHdr.AllowCompAddition;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag = num1 != 0;
    PXSelectBase<INComponentTran> lsselect1 = this.ComponentLineSplittingExt.lsselect;
    nullable = e.Row.Released;
    int? kitInventoryId;
    int num2;
    if (!nullable.GetValueOrDefault())
    {
      kitInventoryId = e.Row.KitInventoryID;
      num2 = kitInventoryId.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    int num3 = flag ? 1 : 0;
    int num4 = num2 & num3;
    ((PXSelectBase) lsselect1).AllowInsert = num4 != 0;
    PXSelectBase<INComponentTran> lsselect2 = this.ComponentLineSplittingExt.lsselect;
    nullable = e.Row.Released;
    int num5 = !nullable.GetValueOrDefault() ? 1 : 0;
    ((PXSelectBase) lsselect2).AllowUpdate = num5 != 0;
    PXSelectBase<INComponentTran> lsselect3 = this.ComponentLineSplittingExt.lsselect;
    nullable = e.Row.Released;
    int num6 = !nullable.GetValueOrDefault() & flag ? 1 : 0;
    ((PXSelectBase) lsselect3).AllowDelete = num6 != 0;
    PXSelectBase<INKitRegister> lsselect4 = this.KitLineSplittingExt.lsselect;
    nullable = e.Row.Released;
    int num7 = !nullable.GetValueOrDefault() ? 1 : 0;
    ((PXSelectBase) lsselect4).AllowUpdate = num7 != 0;
    PXSelectBase<INKitRegister> lsselect5 = this.KitLineSplittingExt.lsselect;
    nullable = e.Row.Released;
    int num8 = !nullable.GetValueOrDefault() ? 1 : 0;
    ((PXSelectBase) lsselect5).AllowDelete = num8 != 0;
    PXCache cache1 = ((PXSelectBase) this.Overhead).Cache;
    nullable = e.Row.Released;
    int num9;
    if (!nullable.GetValueOrDefault())
    {
      kitInventoryId = e.Row.KitInventoryID;
      num9 = kitInventoryId.HasValue ? 1 : 0;
    }
    else
      num9 = 0;
    int num10 = flag ? 1 : 0;
    int num11 = num9 & num10;
    cache1.AllowInsert = num11 != 0;
    PXCache cache2 = ((PXSelectBase) this.Overhead).Cache;
    nullable = e.Row.Released;
    int num12 = !nullable.GetValueOrDefault() ? 1 : 0;
    cache2.AllowUpdate = num12 != 0;
    PXCache cache3 = ((PXSelectBase) this.Overhead).Cache;
    nullable = e.Row.Released;
    int num13 = !nullable.GetValueOrDefault() & flag ? 1 : 0;
    cache3.AllowDelete = num13 != 0;
    PXUIFieldAttribute.SetEnabled<INKitRegister.kitInventoryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INKitRegister>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INKitRegister>>) e).Cache.GetStatus((object) e.Row) == 2);
    PXUIFieldAttribute.SetEnabled<INKitRegister.kitRevisionID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INKitRegister>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INKitRegister>>) e).Cache.GetStatus((object) e.Row) == 2);
    PXUIFieldAttribute.SetEnabled<INKitRegister.reasonCode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INKitRegister>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INKitRegister>>) e).Cache.AllowUpdate && e.Row.DocType == "D");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<INKitRegister> e)
  {
    if (e.Row == null || e.Row.Hold.GetValueOrDefault())
      return;
    Decimal? qty1 = e.Row.Qty;
    Decimal num1 = 0M;
    if (!(qty1.GetValueOrDefault() == num1 & qty1.HasValue) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INKitRegister>>) e).Cache.ObjectsEqual<INKitRegister.hold, INKitRegister.qty>((object) e.Row, (object) e.OldRow) || !((IEnumerable<INComponentTran>) ((PXSelectBase<INComponentTran>) this.Components).SelectMain(Array.Empty<object>())).Any<INComponentTran>((Func<INComponentTran, bool>) (c =>
    {
      Decimal? qty2 = c.Qty;
      Decimal num2 = 0M;
      return qty2.GetValueOrDefault() > num2 & qty2.HasValue;
    })) && !((IEnumerable<INOverheadTran>) ((PXSelectBase<INOverheadTran>) this.Overhead).SelectMain(Array.Empty<object>())).Any<INOverheadTran>((Func<INOverheadTran, bool>) (c =>
    {
      Decimal? qty3 = c.Qty;
      Decimal num3 = 0M;
      return qty3.GetValueOrDefault() > num3 & qty3.HasValue;
    })))
      return;
    ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INKitRegister>>) e).Cache.RaiseExceptionHandling<INKitRegister.qty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
    {
      (object) 0M
    }));
  }

  protected virtual void _(PX.Data.Events.RowDeleted<INKitRegister> e)
  {
    e.Row.LineCntr = new int?(1);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INKitRegister> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    if (e.Row != null && !e.Row.Hold.GetValueOrDefault())
    {
      Decimal? qty1 = e.Row.Qty;
      Decimal num1 = 0M;
      if (qty1.GetValueOrDefault() == num1 & qty1.HasValue && (((IEnumerable<INComponentTran>) ((PXSelectBase<INComponentTran>) this.Components).SelectMain(Array.Empty<object>())).Any<INComponentTran>((Func<INComponentTran, bool>) (c =>
      {
        Decimal? qty2 = c.Qty;
        Decimal num2 = 0M;
        return qty2.GetValueOrDefault() > num2 & qty2.HasValue;
      })) || ((IEnumerable<INOverheadTran>) ((PXSelectBase<INOverheadTran>) this.Overhead).SelectMain(Array.Empty<object>())).Any<INOverheadTran>((Func<INOverheadTran, bool>) (c =>
      {
        Decimal? qty3 = c.Qty;
        Decimal num3 = 0M;
        return qty3.GetValueOrDefault() > num3 & qty3.HasValue;
      }))))
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INKitRegister>>) e).Cache.RaiseExceptionHandling<INKitRegister.qty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
        {
          (object) 0M
        }));
    }
    PXDefaultAttribute.SetPersistingCheck<INKitRegister.reasonCode>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INKitRegister>>) e).Cache, (object) e.Row, e.Row.DocType == "D" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  protected virtual void _(PX.Data.Events.RowInserted<INKitRegister> e)
  {
    Guid? noteId1 = e.Row.NoteID;
    if (!noteId1.HasValue)
      return;
    PXCache cach1 = ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<INKitRegister>>) e).Cache.Graph.Caches[typeof (Note)];
    foreach (Note note in cach1.Cached)
    {
      Guid? noteId2 = note.NoteID;
      Guid? nullable = noteId1;
      if ((noteId2.HasValue == nullable.HasValue ? (noteId2.HasValue ? (noteId2.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        if (cach1.GetStatus((object) note) == 3)
          GraphHelper.MarkUpdated(cach1, (object) note, true);
        if (cach1.GetStatus((object) note) == 4)
          cach1.SetStatus((object) note, (PXEntryStatus) 2);
      }
    }
    PXCache cach2 = ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<INKitRegister>>) e).Cache.Graph.Caches[typeof (NoteDoc)];
    foreach (NoteDoc noteDoc in cach2.Cached)
    {
      Guid? noteId3 = noteDoc.NoteID;
      Guid? nullable = noteId1;
      if ((noteId3.HasValue == nullable.HasValue ? (noteId3.HasValue ? (noteId3.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        if (cach2.GetStatus((object) noteDoc) == 3)
          GraphHelper.MarkUpdated(cach2, (object) noteDoc, true);
        if (cach2.GetStatus((object) noteDoc) == 4)
          cach2.SetStatus((object) noteDoc, (PXEntryStatus) 2);
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.unitCost> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.unitCost>, INComponentTran, object>) e).NewValue = (object) 0M;
    if (e.Row == null || !e.Row.InventoryID.HasValue || e.Row.UOM == null || !e.Row.SiteID.HasValue)
      return;
    PXResult<INItemSite, InventoryItem> pxResult = (PXResult<INItemSite, InventoryItem>) PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXViewOf<INItemSite>.BasedOn<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<InventoryItem>.On<INItemSite.FK.InventoryItem>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.inventoryID, Equal<BqlField<INComponentTran.inventoryID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INItemSite.siteID, IBqlInt>.IsEqual<BqlField<INComponentTran.siteID, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) e.Row
    }, Array.Empty<object>()));
    if (pxResult == null)
      return;
    INItemSite inItemSite1;
    InventoryItem inventoryItem1;
    pxResult.Deconstruct(ref inItemSite1, ref inventoryItem1);
    INItemSite inItemSite2 = inItemSite1;
    InventoryItem inventoryItem2 = inventoryItem1;
    if (inventoryItem2 == null || !inventoryItem2.InventoryID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.unitCost>, INComponentTran, object>) e).NewValue = (object) POItemCostManager.ConvertUOM((PXGraph) this, inventoryItem2, inventoryItem2.BaseUnit, inItemSite2.TranUnitCost.GetValueOrDefault(), e.Row.UOM);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.projectID> e)
  {
    Contract nonProject;
    if (e.Row == null || !this.TryGetNonProject(out nonProject))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.projectID>, INComponentTran, object>) e).NewValue = (object) nonProject.ContractID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.invtMult> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.invtMult>, INComponentTran, object>) e).NewValue = (object) this.GetInvtMult((INTran) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.lineNbr> e)
  {
    if (this._InternalCall)
      return;
    this._InternalCall = true;
    object obj;
    try
    {
      ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.lineNbr>>) e).Cache.RaiseFieldDefaulting<INComponentTran.lineNbr>((object) e.Row, ref obj);
    }
    finally
    {
      this._InternalCall = false;
    }
    foreach (INOverheadTran inOverheadTran in ((PXSelectBase) this.Overhead).Cache.Deleted)
    {
      int? lineNbr = inOverheadTran.LineNbr;
      int num = (int) obj;
      if (lineNbr.GetValueOrDefault() == num & lineNbr.HasValue)
        obj = (object) ((int) (short) obj + 1);
    }
    foreach (INOverheadTran inOverheadTran in ((PXSelectBase) this.Overhead).Cache.Updated)
    {
      int? lineNbr = inOverheadTran.LineNbr;
      int num = (int) (short) obj;
      if (lineNbr.GetValueOrDefault() == num & lineNbr.HasValue)
        obj = (object) ((int) (short) obj + 1);
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.lineNbr>, INComponentTran, object>) e).NewValue = obj;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.lineNbr>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.locationID> e)
  {
    if (e.Row != null && e.Row.InventoryID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.locationID>, INComponentTran, object>) e).NewValue = (object) null;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INComponentTran, INComponentTran.locationID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INComponentTran, INComponentTran.inventoryID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INComponentTran, INComponentTran.inventoryID>>) e).Cache.SetDefaultExt<INComponentTran.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INComponentTran, INComponentTran.inventoryID>>) e).Cache.SetDefaultExt<INComponentTran.tranDesc>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INComponentTran, INComponentTran.inventoryID>>) e).Cache.SetDefaultExt<INComponentTran.unitCost>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INComponentTran, INComponentTran.inventoryID>>) e).Cache.SetDefaultExt<INComponentTran.locationID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INComponentTran, INComponentTran.siteID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INComponentTran, INComponentTran.siteID>>) e).Cache.SetDefaultExt<INComponentTran.unitCost>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INComponentTran, INComponentTran.inventoryID> e)
  {
    if (e.Row == null)
      return;
    INKitSpecStkDet componentSpecById = this.GetComponentSpecByID(e.Row.InventoryID, e.Row.SubItemID);
    if (componentSpecById == null)
      return;
    bool? allowSubstitution = componentSpecById.AllowSubstitution;
    bool flag = false;
    if (!(allowSubstitution.GetValueOrDefault() == flag & allowSubstitution.HasValue))
      return;
    int? compInventoryId = componentSpecById.CompInventoryID;
    int int32 = Convert.ToInt32(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INComponentTran, INComponentTran.inventoryID>, INComponentTran, object>) e).NewValue);
    if (!(compInventoryId.GetValueOrDefault() == int32 & compInventoryId.HasValue))
      throw new PXSetPropertyException("Manual Component substitution is not allowed by the Kit specification.", (PXErrorLevel) 4)
      {
        ErrorValue = (object) InventoryItem.PK.Find((PXGraph) this, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INComponentTran, INComponentTran.inventoryID>, INComponentTran, object>) e).NewValue)?.InventoryCD
      };
  }

  protected virtual void _(PX.Data.Events.RowSelected<INComponentTran> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<INComponentTran.unitCost>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INComponentTran>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INComponentTran>>) e).Cache.AllowUpdate && e.Row.DocType == "D");
  }

  protected virtual void _(PX.Data.Events.RowInserting<INComponentTran> e)
  {
    if (e.Row == null || !e.Row.InventoryID.HasValue)
      return;
    ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<INComponentTran>>) e).Cache.SetDefaultExt<INComponentTran.unitCost>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INComponentTran> e)
  {
    if (e.Row == null || PXDBOperationExt.Command(e.Operation) == 3)
      return;
    INKitSpecStkDet componentSpecById = this.GetComponentSpecByID(e.Row.InventoryID, e.Row.SubItemID);
    INKitRegister current = ((PXSelectBase<INKitRegister>) this.Document).Current;
    if (!this.VerifyQtyVariance(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INComponentTran>>) e).Cache, (INTran) e.Row, componentSpecById, current))
    {
      if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INComponentTran>>) e).Cache.RaiseExceptionHandling<INComponentTran.qty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException("Quantity is dictated by the Kit specification and cannot be changed manualy for the given component.")))
        throw new PXSetPropertyException(typeof (INComponentTran.qty).Name, new object[2]
        {
          null,
          (object) "Quantity is dictated by the Kit specification and cannot be changed manualy for the given component."
        });
    }
    else
    {
      if (this.VerifyQtyBounds(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INComponentTran>>) e).Cache, (INTran) e.Row, componentSpecById, current))
        return;
      PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INComponentTran>>) e).Cache;
      INComponentTran row = e.Row;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> qty = (ValueType) e.Row.Qty;
      object[] objArray1 = new object[3];
      Decimal? nullable1 = componentSpecById.MinCompQty;
      Decimal? nullable2 = ((PXSelectBase<INKitRegister>) this.Document).Current.Qty;
      objArray1[0] = (object) (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?());
      nullable2 = componentSpecById.MaxCompQty;
      nullable1 = ((PXSelectBase<INKitRegister>) this.Document).Current.Qty;
      objArray1[1] = (object) (nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?());
      objArray1[2] = (object) componentSpecById.UOM;
      PXSetPropertyException propertyException = new PXSetPropertyException("Quantity is out of bounds. Specification dictates that it should be within [{0}-{1}] {2}.", objArray1);
      if (cache.RaiseExceptionHandling<INComponentTran.qty>((object) row, (object) qty, (Exception) propertyException))
      {
        string name = typeof (INComponentTran.qty).Name;
        object[] objArray2 = new object[5]
        {
          null,
          (object) "Quantity is out of bounds. Specification dictates that it should be within [{0}-{1}] {2}.",
          null,
          null,
          null
        };
        nullable1 = componentSpecById.MinCompQty;
        nullable2 = ((PXSelectBase<INKitRegister>) this.Document).Current.Qty;
        objArray2[2] = (object) (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?());
        nullable2 = componentSpecById.MaxCompQty;
        nullable1 = ((PXSelectBase<INKitRegister>) this.Document).Current.Qty;
        objArray2[3] = (object) (nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?());
        objArray2[4] = (object) componentSpecById.UOM;
        throw new PXSetPropertyException(name, objArray2);
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INOverheadTran, INOverheadTran.unitCost> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INOverheadTran, INOverheadTran.unitCost>, INOverheadTran, object>) e).NewValue = (object) 0M;
    if (e.Row == null || !e.Row.InventoryID.HasValue || e.Row.UOM == null || !e.Row.BranchID.HasValue)
      return;
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, e.Row.InventoryID);
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this, e.Row.BranchID);
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, e.Row.InventoryID, branch.BaseCuryID);
    if (inventoryItem == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INOverheadTran, INOverheadTran.unitCost>, INOverheadTran, object>) e).NewValue = (object) POItemCostManager.ConvertUOM((PXGraph) this, inventoryItem, inventoryItem.BaseUnit, ((Decimal?) itemCurySettings?.StdCost).GetValueOrDefault(), e.Row.UOM);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INOverheadTran, INOverheadTran.projectID> e)
  {
    Contract nonProject;
    if (e.Row == null || !this.TryGetNonProject(out nonProject))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INOverheadTran, INOverheadTran.projectID>, INOverheadTran, object>) e).NewValue = (object) nonProject.ContractID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INOverheadTran, INOverheadTran.invtMult> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INOverheadTran, INOverheadTran.invtMult>, INOverheadTran, object>) e).NewValue = (object) this.GetInvtMult(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INOverheadTran, INOverheadTran.lineNbr> e)
  {
    if (this._InternalCall)
      return;
    this._InternalCall = true;
    object obj;
    try
    {
      ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<INOverheadTran, INOverheadTran.lineNbr>>) e).Cache.RaiseFieldDefaulting<INOverheadTran.lineNbr>((object) e.Row, ref obj);
    }
    finally
    {
      this._InternalCall = false;
    }
    foreach (INTran inTran in ((PXSelectBase) this.Components).Cache.Deleted)
    {
      int? lineNbr = inTran.LineNbr;
      int num = (int) (short) obj;
      if (lineNbr.GetValueOrDefault() == num & lineNbr.HasValue)
        obj = (object) ((int) (short) obj + 1);
    }
    foreach (INTran inTran in ((PXSelectBase) this.Components).Cache.Updated)
    {
      int? lineNbr = inTran.LineNbr;
      int num = (int) (short) obj;
      if (lineNbr.GetValueOrDefault() == num & lineNbr.HasValue)
        obj = (object) ((int) (short) obj + 1);
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INOverheadTran, INOverheadTran.lineNbr>, INOverheadTran, object>) e).NewValue = obj;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INOverheadTran, INOverheadTran.lineNbr>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INOverheadTran, INOverheadTran.inventoryID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INOverheadTran, INOverheadTran.inventoryID>>) e).Cache.SetDefaultExt<INOverheadTran.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INOverheadTran, INOverheadTran.inventoryID>>) e).Cache.SetDefaultExt<INOverheadTran.tranDesc>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INOverheadTran, INOverheadTran.inventoryID>>) e).Cache.SetDefaultExt<INOverheadTran.unitCost>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INOverheadTran, INOverheadTran.branchID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INOverheadTran, INOverheadTran.branchID>>) e).Cache.SetDefaultExt<INOverheadTran.unitCost>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<INOverheadTran> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<INOverheadTran.reasonCode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INOverheadTran>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INOverheadTran>>) e).Cache.AllowUpdate && e.Row.DocType == "D");
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INOverheadTran> e)
  {
    if (e.Row == null || PXDBOperationExt.Command(e.Operation) == 3)
      return;
    INKitSpecNonStkDet componentSpecById = this.GetNonStockComponentSpecByID(e.Row.InventoryID);
    INKitRegister current = ((PXSelectBase<INKitRegister>) this.Document).Current;
    if (!this.VerifyQtyVariance(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INOverheadTran>>) e).Cache, e.Row, componentSpecById, current))
    {
      if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INOverheadTran>>) e).Cache.RaiseExceptionHandling<INOverheadTran.qty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException("Quantity is dictated by the Kit specification and cannot be changed manualy for the given component.")))
        throw new PXSetPropertyException(typeof (INOverheadTran.qty).Name, new object[2]
        {
          null,
          (object) "Quantity is dictated by the Kit specification and cannot be changed manualy for the given component."
        });
    }
    else
    {
      if (this.VerifyQtyBounds(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INOverheadTran>>) e).Cache, e.Row, componentSpecById, current))
        return;
      PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INOverheadTran>>) e).Cache;
      INOverheadTran row = e.Row;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> qty = (ValueType) e.Row.Qty;
      object[] objArray1 = new object[3];
      Decimal? minCompQty = componentSpecById.MinCompQty;
      Decimal? nullable1 = ((PXSelectBase<INKitRegister>) this.Document).Current.Qty;
      objArray1[0] = (object) (minCompQty.HasValue & nullable1.HasValue ? new Decimal?(minCompQty.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?());
      nullable1 = componentSpecById.MaxCompQty;
      Decimal? nullable2 = ((PXSelectBase<INKitRegister>) this.Document).Current.Qty;
      objArray1[1] = (object) (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?());
      objArray1[2] = (object) componentSpecById.UOM;
      PXSetPropertyException propertyException = new PXSetPropertyException("Quantity is out of bounds. Specification dictates that it should be within [{0}-{1}] {2}.", objArray1);
      if (cache.RaiseExceptionHandling<INOverheadTran.qty>((object) row, (object) qty, (Exception) propertyException))
      {
        string name = typeof (INOverheadTran.qty).Name;
        object[] objArray2 = new object[5]
        {
          null,
          (object) "Quantity is out of bounds. Specification dictates that it should be within [{0}-{1}] {2}.",
          null,
          null,
          null
        };
        nullable2 = componentSpecById.MinCompQty;
        nullable1 = ((PXSelectBase<INKitRegister>) this.Document).Current.Qty;
        objArray2[2] = (object) (nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?());
        nullable1 = componentSpecById.MaxCompQty;
        nullable2 = ((PXSelectBase<INKitRegister>) this.Document).Current.Qty;
        objArray2[3] = (object) (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?());
        objArray2[4] = (object) componentSpecById.UOM;
        throw new PXSetPropertyException(name, objArray2);
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<INKitTranSplit> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<INKitTranSplit.lotSerialNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INKitTranSplit>>) e).Cache, (object) e.Row, e.Row.DocType == "D");
    PXUIFieldAttribute.SetEnabled<INKitTranSplit.subItemID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INKitTranSplit>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INKitTranSplit>>) e).Cache.GetStatus((object) e.Row) == 2);
    PXUIFieldAttribute.SetEnabled<INKitTranSplit.locationID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INKitTranSplit>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INKitTranSplit>>) e).Cache.GetStatus((object) e.Row) == 2);
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (!((PXGraph) this).IsCopyPasteContext)
      return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
    bool allowInsert1 = ((PXSelectBase) this.Components).Cache.AllowInsert;
    bool allowUpdate1 = ((PXSelectBase) this.Components).Cache.AllowUpdate;
    bool allowDelete1 = ((PXSelectBase) this.Components).Cache.AllowDelete;
    bool allowInsert2 = ((PXSelectBase) this.Overhead).Cache.AllowInsert;
    bool allowUpdate2 = ((PXSelectBase) this.Overhead).Cache.AllowUpdate;
    bool allowDelete2 = ((PXSelectBase) this.Overhead).Cache.AllowDelete;
    try
    {
      ((PXSelectBase) this.Components).Cache.AllowInsert = true;
      ((PXSelectBase) this.Components).Cache.AllowUpdate = true;
      ((PXSelectBase) this.Components).Cache.AllowDelete = true;
      ((PXSelectBase) this.Overhead).Cache.AllowInsert = true;
      ((PXSelectBase) this.Overhead).Cache.AllowUpdate = true;
      ((PXSelectBase) this.Overhead).Cache.AllowDelete = true;
      return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
    }
    finally
    {
      ((PXSelectBase) this.Components).Cache.AllowInsert = allowInsert1;
      ((PXSelectBase) this.Components).Cache.AllowUpdate = allowUpdate1;
      ((PXSelectBase) this.Components).Cache.AllowDelete = allowDelete1;
      ((PXSelectBase) this.Overhead).Cache.AllowInsert = allowInsert2;
      ((PXSelectBase) this.Overhead).Cache.AllowUpdate = allowUpdate2;
      ((PXSelectBase) this.Overhead).Cache.AllowDelete = allowDelete2;
    }
  }

  public virtual bool VerifyQtyVariance(
    PXCache sender,
    INTran row,
    INKitSpecStkDet spec,
    INKitRegister assembly)
  {
    if (((PXSelectBase<INKitRegister>) this.Document).Current != null && row != null && spec != null)
    {
      bool? allowQtyVariation = spec.AllowQtyVariation;
      bool flag = false;
      if (allowQtyVariation.GetValueOrDefault() == flag & allowQtyVariation.HasValue)
      {
        Decimal num1 = INUnitAttribute.ConvertToBase(sender, row.InventoryID, row.UOM, row.Qty.GetValueOrDefault(), INPrecision.QUANTITY);
        PXCache sender1 = sender;
        int? kitInventoryId = assembly.KitInventoryID;
        string uom = assembly.UOM;
        Decimal? nullable = assembly.Qty;
        Decimal valueOrDefault = nullable.GetValueOrDefault();
        Decimal num2 = INUnitAttribute.ConvertToBase(sender1, kitInventoryId, uom, valueOrDefault, INPrecision.QUANTITY);
        nullable = spec.DfltCompQty;
        Decimal d = nullable.GetValueOrDefault() * num2;
        if (this.IsSerialNumbered(row.InventoryID))
          d = Math.Ceiling(d);
        Decimal num3 = INUnitAttribute.ConvertToBase(sender, row.InventoryID, spec.UOM, d, INPrecision.QUANTITY);
        if (((PXSelectBase<INKitRegister>) this.Document).Current.DocType != "D")
          return num1 == num3;
      }
    }
    return true;
  }

  public virtual bool VerifyQtyVariance(
    PXCache sender,
    INOverheadTran row,
    INKitSpecNonStkDet spec,
    INKitRegister assembly)
  {
    if (((PXSelectBase<INKitRegister>) this.Document).Current != null && row != null && spec != null)
    {
      bool? allowQtyVariation = spec.AllowQtyVariation;
      bool flag = false;
      if (allowQtyVariation.GetValueOrDefault() == flag & allowQtyVariation.HasValue)
      {
        Decimal num1 = INUnitAttribute.ConvertToBase(sender, row.InventoryID, row.UOM, row.Qty.GetValueOrDefault(), INPrecision.QUANTITY);
        PXCache sender1 = sender;
        int? kitInventoryId = assembly.KitInventoryID;
        string uom1 = assembly.UOM;
        Decimal? nullable = assembly.Qty;
        Decimal valueOrDefault = nullable.GetValueOrDefault();
        Decimal num2 = INUnitAttribute.ConvertToBase(sender1, kitInventoryId, uom1, valueOrDefault, INPrecision.QUANTITY);
        PXCache sender2 = sender;
        int? inventoryId = row.InventoryID;
        string uom2 = spec.UOM;
        nullable = spec.DfltCompQty;
        Decimal num3 = nullable.GetValueOrDefault() * num2;
        Decimal num4 = INUnitAttribute.ConvertToBase(sender2, inventoryId, uom2, num3, INPrecision.QUANTITY);
        if (((PXSelectBase<INKitRegister>) this.Document).Current.DocType != "D")
          return num1 == num4;
      }
    }
    return true;
  }

  public virtual bool VerifyQtyBounds(
    PXCache sender,
    INTran row,
    INKitSpecStkDet spec,
    INKitRegister assembly)
  {
    if (((PXSelectBase<INKitRegister>) this.Document).Current != null && row != null && spec != null && spec.AllowQtyVariation.GetValueOrDefault() && ((PXSelectBase<INKitRegister>) this.Document).Current.DocType != "D")
    {
      Decimal num1 = INUnitAttribute.ConvertToBase(sender, row.InventoryID, row.UOM, row.Qty.GetValueOrDefault(), INPrecision.QUANTITY);
      PXCache sender1 = sender;
      int? kitInventoryId = assembly.KitInventoryID;
      string uom1 = assembly.UOM;
      Decimal? nullable = assembly.Qty;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      Decimal num2 = INUnitAttribute.ConvertToBase(sender1, kitInventoryId, uom1, valueOrDefault, INPrecision.QUANTITY);
      nullable = spec.MinCompQty;
      if (nullable.HasValue)
      {
        PXCache sender2 = sender;
        int? inventoryId = row.InventoryID;
        string uom2 = spec.UOM;
        nullable = spec.MinCompQty;
        Decimal num3 = nullable.Value * num2;
        Decimal num4 = INUnitAttribute.ConvertToBase(sender2, inventoryId, uom2, num3, INPrecision.QUANTITY);
        if (num1 < num4)
          return false;
      }
      nullable = spec.MaxCompQty;
      if (nullable.HasValue)
      {
        PXCache sender3 = sender;
        int? inventoryId = row.InventoryID;
        string uom3 = spec.UOM;
        nullable = spec.MaxCompQty;
        Decimal num5 = nullable.Value * num2;
        Decimal num6 = INUnitAttribute.ConvertToBase(sender3, inventoryId, uom3, num5, INPrecision.QUANTITY);
        if (num1 > num6)
          return false;
      }
    }
    return true;
  }

  public virtual bool VerifyQtyBounds(
    PXCache sender,
    INOverheadTran row,
    INKitSpecNonStkDet spec,
    INKitRegister assembly)
  {
    if (((PXSelectBase<INKitRegister>) this.Document).Current != null && row != null && spec != null && spec.AllowQtyVariation.GetValueOrDefault() && ((PXSelectBase<INKitRegister>) this.Document).Current.DocType != "D")
    {
      Decimal num1 = INUnitAttribute.ConvertToBase(sender, row.InventoryID, row.UOM, row.Qty.GetValueOrDefault(), INPrecision.QUANTITY);
      PXCache sender1 = sender;
      int? kitInventoryId = assembly.KitInventoryID;
      string uom1 = assembly.UOM;
      Decimal? nullable = assembly.Qty;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      Decimal num2 = INUnitAttribute.ConvertToBase(sender1, kitInventoryId, uom1, valueOrDefault, INPrecision.QUANTITY);
      nullable = spec.MinCompQty;
      if (nullable.HasValue)
      {
        PXCache sender2 = sender;
        int? inventoryId = row.InventoryID;
        string uom2 = spec.UOM;
        nullable = spec.MinCompQty;
        Decimal num3 = nullable.Value * num2;
        Decimal num4 = INUnitAttribute.ConvertToBase(sender2, inventoryId, uom2, num3, INPrecision.QUANTITY);
        if (num1 < num4)
          return false;
      }
      nullable = spec.MaxCompQty;
      if (nullable.HasValue)
      {
        PXCache sender3 = sender;
        int? inventoryId = row.InventoryID;
        string uom3 = spec.UOM;
        nullable = spec.MaxCompQty;
        Decimal num5 = nullable.Value * num2;
        Decimal num6 = INUnitAttribute.ConvertToBase(sender3, inventoryId, uom3, num5, INPrecision.QUANTITY);
        if (num1 > num6)
          return false;
      }
    }
    return true;
  }

  public virtual short? GetInvtMult(INTran tran)
  {
    short? invtMult = new short?();
    if (((PXSelectBase<INKitRegister>) this.Document).Current != null)
    {
      if (((PXSelectBase<INKitRegister>) this.Document).Current.DocType == "D")
      {
        int? lineNbr = tran.LineNbr;
        int? kitLineNbr = ((PXSelectBase<INKitRegister>) this.Document).Current.KitLineNbr;
        invtMult = lineNbr.GetValueOrDefault() == kitLineNbr.GetValueOrDefault() & lineNbr.HasValue == kitLineNbr.HasValue ? new short?((short) -1) : new short?((short) 1);
      }
      else
      {
        int? lineNbr = tran.LineNbr;
        int? kitLineNbr = ((PXSelectBase<INKitRegister>) this.Document).Current.KitLineNbr;
        invtMult = lineNbr.GetValueOrDefault() == kitLineNbr.GetValueOrDefault() & lineNbr.HasValue == kitLineNbr.HasValue ? new short?((short) 1) : new short?((short) -1);
      }
    }
    return invtMult;
  }

  public virtual short? GetInvtMult(INOverheadTran tran)
  {
    short? invtMult = new short?();
    if (((PXSelectBase<INKitRegister>) this.Document).Current != null)
    {
      if (((PXSelectBase<INKitRegister>) this.Document).Current.DocType == "D")
      {
        int? lineNbr = tran.LineNbr;
        int? kitLineNbr = ((PXSelectBase<INKitRegister>) this.Document).Current.KitLineNbr;
        invtMult = lineNbr.GetValueOrDefault() == kitLineNbr.GetValueOrDefault() & lineNbr.HasValue == kitLineNbr.HasValue ? new short?((short) -1) : new short?((short) 1);
      }
      else
      {
        int? lineNbr = tran.LineNbr;
        int? kitLineNbr = ((PXSelectBase<INKitRegister>) this.Document).Current.KitLineNbr;
        invtMult = lineNbr.GetValueOrDefault() == kitLineNbr.GetValueOrDefault() & lineNbr.HasValue == kitLineNbr.HasValue ? new short?((short) 1) : new short?((short) -1);
      }
    }
    return invtMult;
  }

  protected virtual bool TryGetNonProject(out Contract nonProject)
  {
    nonProject = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.nonProject, Equal<True>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    return nonProject != null;
  }

  public virtual bool IsSerialNumbered(int? inventoryID)
  {
    bool flag = false;
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this, InventoryItem.PK.Find((PXGraph) this, inventoryID)?.LotSerClassID);
    if (inLotSerClass != null && inLotSerClass.LotSerTrack == "S")
      flag = true;
    return flag;
  }

  public virtual INKitSpecStkDet GetComponentSpecByID(int? inventoryID, int? subItemID)
  {
    return PXResultset<INKitSpecStkDet>.op_Implicit(PXSelectBase<INKitSpecStkDet, PXSelect<INKitSpecStkDet, Where<INKitSpecStkDet.kitInventoryID, Equal<Current<INKitRegister.kitInventoryID>>, And<INKitSpecStkDet.revisionID, Equal<Current<INKitRegister.kitRevisionID>>, And<INKitSpecStkDet.compInventoryID, Equal<Required<INKitSpecStkDet.compInventoryID>>, And<Where<INKitSpecStkDet.compSubItemID, Equal<Required<INKitSpecStkDet.compSubItemID>>, Or<Required<INKitSpecStkDet.compSubItemID>, IsNull>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) inventoryID,
      (object) subItemID,
      (object) subItemID
    }));
  }

  public virtual INKitSpecNonStkDet GetNonStockComponentSpecByID(int? inventoryID)
  {
    return PXResultset<INKitSpecNonStkDet>.op_Implicit(PXSelectBase<INKitSpecNonStkDet, PXSelect<INKitSpecNonStkDet, Where<INKitSpecNonStkDet.kitInventoryID, Equal<Current<INKitRegister.kitInventoryID>>, And<INKitSpecNonStkDet.revisionID, Equal<Current<INKitRegister.kitRevisionID>>, And<INKitSpecNonStkDet.compInventoryID, Equal<Required<INKitSpecStkDet.compInventoryID>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) inventoryID
    }));
  }

  public virtual InventoryItem GetInventoryItemByID(int? inventoryID)
  {
    return InventoryItem.PK.Find((PXGraph) this, inventoryID);
  }

  public class SortLineSplitting : 
    SortExtensionsBy<TypeArrayOf<PXGraphExtension<KitAssemblyEntry>>.FilledWith<INComponentLineSplittingExtension, INComponentItemAvailabilityExtension, INKitLineSplittingExtension, KitAssemblyEntry.ComponentRebuilder>>
  {
  }

  public class ComponentRebuilder : PXGraphExtension<KitAssemblyEntry>
  {
    public PXSelectJoin<INKitSpecStkDet, InnerJoin<InventoryItem, On<INKitSpecStkDet.FK.ComponentInventoryItem>>, Where<INKitSpecStkDet.kitInventoryID, Equal<Current<INKitRegister.kitInventoryID>>, And<INKitSpecStkDet.revisionID, Equal<Current<INKitRegister.kitRevisionID>>>>> SpecComponents;
    public PXSelectJoin<INKitSpecNonStkDet, InnerJoin<InventoryItem, On<INKitSpecNonStkDet.FK.ComponentInventoryItem>>, Where<INKitSpecNonStkDet.kitInventoryID, Equal<Current<INKitRegister.kitInventoryID>>, And<INKitSpecNonStkDet.revisionID, Equal<Current<INKitRegister.kitRevisionID>>>>> SpecOverhead;
    [PXCopyPasteHiddenView]
    public PXSelect<INComponentTranSplit, Where<INComponentTranSplit.docType, Equal<Current<INComponentTran.docType>>, And<INComponentTranSplit.refNbr, Equal<Current<INComponentTran.refNbr>>, And<INComponentTranSplit.lineNbr, Equal<Current<INComponentTran.lineNbr>>>>>> ComponentSplits;
    [PXCopyPasteHiddenView]
    public PXSelect<INKitTranSplit, Where<INKitTranSplit.docType, Equal<Current<INKitRegister.docType>>, And<INKitTranSplit.refNbr, Equal<Current<INKitRegister.refNbr>>, And<INKitTranSplit.lineNbr, Equal<Current<INKitRegister.kitLineNbr>>>>>> MasterSplits;
    public PXSelect<INKitSerialPart, Where<INKitSerialPart.docType, Equal<Current<INKitRegister.docType>>, And<INKitSerialPart.refNbr, Equal<Current<INKitRegister.refNbr>>>>> SerialNumberedParts;

    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      ((PXSelectBase) this.SpecComponents).Cache.AllowInsert = false;
      ((PXSelectBase) this.SpecComponents).Cache.AllowDelete = false;
      ((PXSelectBase) this.SpecComponents).Cache.AllowUpdate = false;
      ((PXSelectBase) this.SpecOverhead).Cache.AllowInsert = false;
      ((PXSelectBase) this.SpecOverhead).Cache.AllowDelete = false;
      ((PXSelectBase) this.SpecOverhead).Cache.AllowUpdate = false;
      ((RowInsertedEvents) ((PXGraph) this.Base).RowInserted).AddAbstractHandler<INKitRegister>(new Action<AbstractEvents.IRowInserted<INKitRegister>>(this.EventHandler));
      ((RowUpdatedEvents) ((PXGraph) this.Base).RowUpdated).AddAbstractHandler<INKitRegister>(new Action<AbstractEvents.IRowUpdated<INKitRegister>>(this.EventHandler));
      ((RowInsertedEvents) ((PXGraph) this.Base).RowInserted).AddAbstractHandler<INKitTranSplit>(new Action<AbstractEvents.IRowInserted<INKitTranSplit>>(this.EventHandler));
      ((RowUpdatedEvents) ((PXGraph) this.Base).RowUpdated).AddAbstractHandler<INKitTranSplit>(new Action<AbstractEvents.IRowUpdated<INKitTranSplit>>(this.EventHandler));
      ((RowDeletedEvents) ((PXGraph) this.Base).RowDeleted).AddAbstractHandler<INKitTranSplit>(new Action<AbstractEvents.IRowDeleted<INKitTranSplit>>(this.EventHandler));
    }

    protected virtual void EventHandler(AbstractEvents.IRowInserted<INKitRegister> e)
    {
      if (e.Row == null || !e.Row.KitInventoryID.HasValue || e.Row.KitRevisionID == null)
        return;
      this.RebuildComponents(INUnitAttribute.ConvertToBase(((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache, e.Row.KitInventoryID, e.Row.UOM, e.Row.Qty.GetValueOrDefault(), INPrecision.QUANTITY));
    }

    protected virtual void EventHandler(AbstractEvents.IRowUpdated<INKitRegister> e)
    {
      if (e.Row == null)
        return;
      if (!(e.Row.DocType == "D") || !this.IsWhenReceivedSerialNumbered(e.Row.KitInventoryID))
      {
        if (!((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.ObjectsEqual<INKitRegister.kitInventoryID, INKitRegister.kitRevisionID>((object) e.Row, (object) e.OldRow))
          this.RebuildComponents(INUnitAttribute.ConvertToBase(((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache, e.Row.KitInventoryID, e.Row.UOM, e.Row.Qty.GetValueOrDefault(), INPrecision.QUANTITY));
        else if (!((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.ObjectsEqual<INKitRegister.qty, INKitRegister.uOM>((object) e.Row, (object) e.OldRow))
          this.RecountComponents(INUnitAttribute.ConvertToBase(((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache, e.Row.KitInventoryID, e.Row.UOM, e.Row.Qty.GetValueOrDefault(), INPrecision.QUANTITY), INUnitAttribute.ConvertToBase(((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache, e.Row.KitInventoryID, e.OldRow.UOM, e.OldRow.Qty.GetValueOrDefault(), INPrecision.QUANTITY));
      }
      if (!((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.ObjectsEqual<INKitRegister.siteID>((object) e.Row, (object) e.OldRow))
      {
        foreach (PXResult<INComponentTran> pxResult in ((PXSelectBase<INComponentTran>) this.Base.Components).Select(Array.Empty<object>()))
        {
          INComponentTran copy = (INComponentTran) ((PXSelectBase) this.Base.Components).Cache.CreateCopy((object) PXResult<INComponentTran>.op_Implicit(pxResult));
          copy.BranchID = e.Row.BranchID;
          copy.SiteID = e.Row.SiteID;
          ((PXSelectBase<INComponentTran>) this.Base.Components).Update(copy);
        }
        foreach (PXResult<INOverheadTran> pxResult in ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Select(Array.Empty<object>()))
        {
          INOverheadTran copy = (INOverheadTran) ((PXSelectBase) this.Base.Overhead).Cache.CreateCopy((object) PXResult<INOverheadTran>.op_Implicit(pxResult));
          copy.BranchID = e.Row.BranchID;
          copy.SiteID = e.Row.SiteID;
          ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Update(copy);
        }
      }
      if (((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.ObjectsEqual<INKitRegister.tranDate>((object) e.Row, (object) e.OldRow))
        return;
      foreach (PXResult<INComponentTran> pxResult in ((PXSelectBase<INComponentTran>) this.Base.Components).Select(Array.Empty<object>()))
      {
        INComponentTran inComponentTran = PXResult<INComponentTran>.op_Implicit(pxResult);
        GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Components).Cache, (object) inComponentTran);
        foreach (INComponentTranSplit componentTranSplit in ((PXSelectBase) this.ComponentSplits).View.SelectMultiBound((object[]) new INComponentTran[1]
        {
          inComponentTran
        }, Array.Empty<object>()))
          GraphHelper.MarkUpdated(((PXSelectBase) this.ComponentSplits).Cache, (object) componentTranSplit);
      }
      foreach (PXResult<INOverheadTran> pxResult in ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Select(Array.Empty<object>()))
        GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Overhead).Cache, (object) PXResult<INOverheadTran>.op_Implicit(pxResult));
      foreach (PXResult<INKitTranSplit> pxResult in ((PXSelectBase<INKitTranSplit>) this.MasterSplits).Select(Array.Empty<object>()))
        GraphHelper.MarkUpdated(((PXSelectBase) this.MasterSplits).Cache, (object) PXResult<INKitTranSplit>.op_Implicit(pxResult));
    }

    protected virtual void EventHandler(AbstractEvents.IRowInserted<INKitTranSplit> e)
    {
      if (e.Row == null || !(e.Row.DocType == "D") || !this.IsWhenReceivedSerialNumbered(e.Row.InventoryID))
        return;
      Decimal? qty = e.Row.Qty;
      Decimal num = 0M;
      if (!(qty.GetValueOrDefault() > num & qty.HasValue))
        return;
      this.AddKit(e.Row.LotSerialNbr, e.Row.InventoryID);
    }

    protected virtual void EventHandler(AbstractEvents.IRowUpdated<INKitTranSplit> e)
    {
      if (e.Row == null || !(e.Row.DocType == "D") || !this.IsWhenReceivedSerialNumbered(e.Row.InventoryID))
        return;
      if (!((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.ObjectsEqual<INKitTranSplit.qty>((object) e.Row, (object) e.OldRow))
      {
        Decimal? qty = e.Row.Qty;
        Decimal num = 0M;
        if (qty.GetValueOrDefault() == num & qty.HasValue)
          this.RemoveKit(e.Row.LotSerialNbr, e.Row.InventoryID);
        else
          this.AddKit(e.Row.LotSerialNbr, e.Row.InventoryID);
      }
      else
      {
        if (((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.ObjectsEqual<INKitTranSplit.lotSerialNbr>((object) e.Row, (object) e.OldRow))
          return;
        this.RemoveKit(e.OldRow.LotSerialNbr, e.OldRow.InventoryID);
        this.AddKit(e.Row.LotSerialNbr, e.OldRow.InventoryID);
      }
    }

    protected virtual void EventHandler(AbstractEvents.IRowDeleted<INKitTranSplit> e)
    {
      if (e.Row == null || !(e.Row.DocType == "D") || !this.IsWhenReceivedSerialNumbered(e.Row.InventoryID))
        return;
      Decimal? qty = e.Row.Qty;
      Decimal num = 0M;
      if (!(qty.GetValueOrDefault() > num & qty.HasValue))
        return;
      this.RemoveKit(e.Row.LotSerialNbr, e.Row.InventoryID);
    }

    [PXOverride]
    public virtual void Persist(System.Action base_Persist)
    {
      if (((PXSelectBase<INKitRegister>) this.Base.Document).Current != null)
        this.DistributeParts();
      base_Persist();
    }

    public virtual bool IsWhenReceivedSerialNumbered(int? inventoryID)
    {
      bool flag = false;
      INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this.Base, InventoryItem.PK.Find((PXGraph) this.Base, inventoryID)?.LotSerClassID);
      if (inLotSerClass != null && inLotSerClass.LotSerTrack == "S" && inLotSerClass.LotSerAssign == "R")
        flag = true;
      return flag;
    }

    public virtual void DistributeParts()
    {
      Dictionary<string, INKitSerialPart> partsDistribution = this.GetPartsDistribution();
      foreach (PXResult<INKitSerialPart> pxResult in ((PXSelectBase<INKitSerialPart>) this.SerialNumberedParts).Select(Array.Empty<object>()))
      {
        INKitSerialPart inKitSerialPart = PXResult<INKitSerialPart>.op_Implicit(pxResult);
        if (!partsDistribution.ContainsKey($"{inKitSerialPart.KitLineNbr}.{inKitSerialPart.KitSplitLineNbr}-{inKitSerialPart.PartLineNbr}.{inKitSerialPart.PartSplitLineNbr}"))
          ((PXSelectBase<INKitSerialPart>) this.SerialNumberedParts).Delete(inKitSerialPart);
      }
      foreach (INKitSerialPart inKitSerialPart in partsDistribution.Values)
      {
        if (((PXSelectBase<INKitSerialPart>) this.SerialNumberedParts).Locate(inKitSerialPart) == null)
          ((PXSelectBase<INKitSerialPart>) this.SerialNumberedParts).Insert(inKitSerialPart);
      }
    }

    public virtual Dictionary<string, INKitSerialPart> GetPartsDistribution()
    {
      Dictionary<string, INKitSerialPart> partsDistribution = new Dictionary<string, INKitSerialPart>();
      if (this.Base.IsSerialNumbered(((PXSelectBase<INKitRegister>) this.Base.Document).Current.KitInventoryID))
      {
        PXResultset<INKitTranSplit> pxResultset1 = ((PXSelectBase<INKitTranSplit>) this.MasterSplits).Select(Array.Empty<object>());
        PXSelectBase<INComponentTranSplit> pxSelectBase = (PXSelectBase<INComponentTranSplit>) new PXSelect<INComponentTranSplit, Where<INComponentTranSplit.docType, Equal<Required<INComponentTran.docType>>, And<INComponentTranSplit.refNbr, Equal<Required<INComponentTran.refNbr>>, And<INComponentTranSplit.lineNbr, Equal<Required<INComponentTran.lineNbr>>>>>>((PXGraph) this.Base);
        for (int index1 = 0; index1 < pxResultset1.Count; ++index1)
        {
          foreach (PXResult<INComponentTran> pxResult in ((PXSelectBase<INComponentTran>) this.Base.Components).Select(Array.Empty<object>()))
          {
            INComponentTran inComponentTran = PXResult<INComponentTran>.op_Implicit(pxResult);
            if (this.IsWhenReceivedSerialNumbered(inComponentTran.InventoryID))
            {
              PXResultset<INComponentTranSplit> pxResultset2 = pxSelectBase.Select(new object[3]
              {
                (object) inComponentTran.DocType,
                (object) inComponentTran.RefNbr,
                (object) inComponentTran.LineNbr
              });
              if (pxResultset2.Count % pxResultset1.Count != 0)
              {
                if (((PXSelectBase) this.Base.Components).Cache.GetStatus((object) inComponentTran) == null)
                  ((PXSelectBase) this.Base.Components).Cache.SetStatus((object) inComponentTran, (PXEntryStatus) 6);
                if (((PXSelectBase) this.Base.Components).Cache.RaiseExceptionHandling<INComponentTran.qty>((object) inComponentTran, (object) inComponentTran.Qty, (Exception) new PXSetPropertyException("Quantity of Components is not valid. Quantity must be such that it can be uniformly distributed among the kits produced.")))
                  throw new PXSetPropertyException(typeof (INComponentTran.qty).Name, new object[2]
                  {
                    null,
                    (object) "Quantity of Components is not valid. Quantity must be such that it can be uniformly distributed among the kits produced."
                  });
              }
              int num1 = pxResultset2.Count / pxResultset1.Count;
              int num2 = index1 * num1;
              for (int index2 = num2; index2 < num2 + num1; ++index2)
              {
                INKitTranSplit inKitTranSplit = PXResult<INKitTranSplit>.op_Implicit(pxResultset1[index1]);
                INComponentTranSplit componentTranSplit = PXResult<INComponentTranSplit>.op_Implicit(pxResultset2[index2]);
                INKitSerialPart inKitSerialPart = new INKitSerialPart();
                inKitSerialPart.DocType = inKitTranSplit.DocType;
                inKitSerialPart.RefNbr = inKitTranSplit.RefNbr;
                inKitSerialPart.KitLineNbr = inKitTranSplit.LineNbr;
                inKitSerialPart.KitSplitLineNbr = inKitTranSplit.SplitLineNbr;
                inKitSerialPart.PartLineNbr = componentTranSplit.LineNbr;
                inKitSerialPart.PartSplitLineNbr = componentTranSplit.SplitLineNbr;
                partsDistribution.Add($"{inKitSerialPart.KitLineNbr}.{inKitSerialPart.KitSplitLineNbr}-{inKitSerialPart.PartLineNbr}.{inKitSerialPart.PartSplitLineNbr}", inKitSerialPart);
              }
            }
          }
        }
      }
      return partsDistribution;
    }

    public virtual void AddKit(string serialNumber, int? inventoryID)
    {
      INTranSplit originalSplit;
      INTran originalMasterTran;
      INRegister originalDoc;
      this.SearchOriginalAssembySplitLine(serialNumber, inventoryID, out originalSplit, out originalMasterTran, out originalDoc);
      if (originalSplit != null)
      {
        Decimal num1 = originalMasterTran.Qty.Value;
        foreach (PXResult<INTran, InventoryItem, INKitSpecStkDet> pxResult in PXSelectBase<INTran, PXSelectJoin<INTran, InnerJoin<InventoryItem, On2<INTran.FK.InventoryItem, And<InventoryItem.stkItem, Equal<True>>>, LeftJoin<INKitSpecStkDet, On<INTran.inventoryID, Equal<INKitSpecStkDet.compInventoryID>, And<INKitSpecStkDet.kitInventoryID, Equal<Required<INKitSpecStkDet.kitInventoryID>>, And<INKitSpecStkDet.revisionID, Equal<Required<INKitSpecStkDet.revisionID>>>>>>>, Where<INTran.docType, Equal<INDocType.production>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>, And<INTran.invtMult, Equal<shortMinus1>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
        {
          (object) originalDoc.KitInventoryID,
          (object) originalDoc.KitRevisionID,
          (object) originalDoc.RefNbr
        }))
        {
          InventoryItem inventoryItem = PXResult<INTran, InventoryItem, INKitSpecStkDet>.op_Implicit(pxResult);
          INTran originalComponent = PXResult<INTran, InventoryItem, INKitSpecStkDet>.op_Implicit(pxResult);
          INKitSpecStkDet inKitSpecStkDet = PXResult<INTran, InventoryItem, INKitSpecStkDet>.op_Implicit(pxResult);
          INComponentTran componentByInventoryId = this.GetComponentByInventoryID(inventoryItem.InventoryID);
          Decimal? nullable1;
          Decimal? nullable2;
          Decimal? nullable3;
          if (componentByInventoryId != null)
          {
            ((PXSelectBase<INComponentTran>) this.Base.Components).Current = componentByInventoryId;
            if (this.IsWhenReceivedSerialNumbered(inventoryItem.InventoryID))
            {
              foreach (PXResult<INTranSplit> componentSplit in this.GetComponentSplits(originalSplit, originalComponent))
              {
                INTranSplit inTranSplit = PXResult<INTranSplit>.op_Implicit(componentSplit);
                INComponentTranSplit copy = PXCache<INComponentTranSplit>.CreateCopy(((PXSelectBase<INComponentTranSplit>) this.ComponentSplits).Insert(new INComponentTranSplit()));
                copy.LotSerialNbr = inTranSplit.LotSerialNbr;
                copy.Qty = inTranSplit.Qty;
                ((PXSelectBase<INComponentTranSplit>) this.ComponentSplits).Update(copy);
              }
            }
            else
            {
              INComponentTran copy = PXCache<INComponentTran>.CreateCopy(componentByInventoryId);
              INComponentTran inComponentTran = componentByInventoryId;
              nullable1 = inComponentTran.Qty;
              Decimal? nullable4 = originalComponent.Qty;
              Decimal num2 = num1;
              nullable2 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() / num2) : new Decimal?();
              nullable4 = inKitSpecStkDet.DisassemblyCoeff;
              Decimal num3 = nullable4 ?? 1M;
              Decimal? nullable5;
              if (!nullable2.HasValue)
              {
                nullable4 = new Decimal?();
                nullable5 = nullable4;
              }
              else
                nullable5 = new Decimal?(nullable2.GetValueOrDefault() * num3);
              nullable3 = nullable5;
              Decimal? nullable6;
              if (!(nullable1.HasValue & nullable3.HasValue))
              {
                nullable2 = new Decimal?();
                nullable6 = nullable2;
              }
              else
                nullable6 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
              inComponentTran.Qty = nullable6;
              ((PXSelectBase) this.Base.Components).Cache.SetValueExt<INComponentTran.qty>((object) componentByInventoryId, (object) componentByInventoryId.Qty);
              GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Components).Cache, (object) componentByInventoryId, true);
              ((PXSelectBase) this.Base.Components).Cache.RaiseRowUpdated((object) componentByInventoryId, (object) copy);
            }
          }
          else
          {
            INComponentTran tran = new INComponentTran();
            tran.DocType = ((PXSelectBase<INKitRegister>) this.Base.Document).Current.DocType;
            tran.TranType = tran.DocType == "D" ? "DSY" : "ASY";
            tran.InvtMult = this.Base.GetInvtMult((INTran) tran);
            tran.IsStockItem = originalComponent.IsStockItem;
            tran.InventoryID = originalComponent.InventoryID;
            tran.SubItemID = originalComponent.SubItemID;
            tran.UOM = originalComponent.UOM;
            if (this.IsWhenReceivedSerialNumbered(inventoryItem.InventoryID))
            {
              ((PXSelectBase<INComponentTran>) this.Base.Components).Insert(tran);
              foreach (PXResult<INTranSplit> componentSplit in this.GetComponentSplits(originalSplit, originalComponent))
              {
                INTranSplit inTranSplit = PXResult<INTranSplit>.op_Implicit(componentSplit);
                INComponentTranSplit copy = PXCache<INComponentTranSplit>.CreateCopy(((PXSelectBase<INComponentTranSplit>) this.ComponentSplits).Insert(new INComponentTranSplit()));
                copy.LotSerialNbr = inTranSplit.LotSerialNbr;
                copy.Qty = inTranSplit.Qty;
                ((PXSelectBase<INComponentTranSplit>) this.ComponentSplits).Update(copy);
              }
            }
            else
            {
              INComponentTran inComponentTran = tran;
              nullable1 = originalComponent.Qty;
              Decimal num4 = num1;
              Decimal? nullable7;
              if (!nullable1.HasValue)
              {
                nullable2 = new Decimal?();
                nullable7 = nullable2;
              }
              else
                nullable7 = new Decimal?(nullable1.GetValueOrDefault() / num4);
              nullable3 = nullable7;
              nullable1 = inKitSpecStkDet.DisassemblyCoeff;
              Decimal num5 = nullable1 ?? 1M;
              Decimal? nullable8;
              if (!nullable3.HasValue)
              {
                nullable1 = new Decimal?();
                nullable8 = nullable1;
              }
              else
                nullable8 = new Decimal?(nullable3.GetValueOrDefault() * num5);
              inComponentTran.Qty = nullable8;
              ((PXSelectBase<INComponentTran>) this.Base.Components).Insert(tran);
            }
          }
        }
        foreach (PXResult<INTran, InventoryItem, INKitSpecNonStkDet> pxResult in PXSelectBase<INTran, PXSelectJoin<INTran, InnerJoin<InventoryItem, On2<INTran.FK.InventoryItem, And<InventoryItem.stkItem, Equal<boolFalse>>>, LeftJoin<INKitSpecNonStkDet, On<INTran.inventoryID, Equal<INKitSpecNonStkDet.compInventoryID>, And<INKitSpecNonStkDet.kitInventoryID, Equal<Required<INKitSpecNonStkDet.kitInventoryID>>, And<INKitSpecNonStkDet.revisionID, Equal<Required<INKitSpecNonStkDet.revisionID>>>>>>>, Where<INTran.docType, Equal<INDocType.production>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>, And<INTran.invtMult, Equal<shortMinus1>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
        {
          (object) originalDoc.KitInventoryID,
          (object) originalDoc.KitRevisionID,
          (object) originalDoc.RefNbr
        }))
        {
          InventoryItem inventoryItem = PXResult<INTran, InventoryItem, INKitSpecNonStkDet>.op_Implicit(pxResult);
          INTran inTran = PXResult<INTran, InventoryItem, INKitSpecNonStkDet>.op_Implicit(pxResult);
          PXResult<INTran, InventoryItem, INKitSpecNonStkDet>.op_Implicit(pxResult);
          INOverheadTran overheadByInventoryId = this.GetOverheadByInventoryID(inventoryItem.InventoryID);
          Decimal? nullable9;
          Decimal? nullable10;
          if (overheadByInventoryId != null)
          {
            INOverheadTran inOverheadTran = overheadByInventoryId;
            nullable9 = inOverheadTran.Qty;
            Decimal? nullable11 = inTran.Qty;
            Decimal num6 = num1;
            nullable10 = nullable11.HasValue ? new Decimal?(nullable11.GetValueOrDefault() / num6) : new Decimal?();
            Decimal? nullable12;
            if (!(nullable9.HasValue & nullable10.HasValue))
            {
              nullable11 = new Decimal?();
              nullable12 = nullable11;
            }
            else
              nullable12 = new Decimal?(nullable9.GetValueOrDefault() + nullable10.GetValueOrDefault());
            inOverheadTran.Qty = nullable12;
            ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Update(overheadByInventoryId);
          }
          else
          {
            INOverheadTran tran = new INOverheadTran()
            {
              DocType = ((PXSelectBase<INKitRegister>) this.Base.Document).Current.DocType
            };
            tran.TranType = tran.DocType == "D" ? "DSY" : "ASY";
            tran.InvtMult = this.Base.GetInvtMult(tran);
            tran.InventoryID = inTran.InventoryID;
            INOverheadTran inOverheadTran = tran;
            nullable10 = inTran.Qty;
            Decimal num7 = num1;
            Decimal? nullable13;
            if (!nullable10.HasValue)
            {
              nullable9 = new Decimal?();
              nullable13 = nullable9;
            }
            else
              nullable13 = new Decimal?(nullable10.GetValueOrDefault() / num7);
            inOverheadTran.Qty = nullable13;
            tran.UOM = inTran.UOM;
            tran.SiteID = inTran.SiteID;
            ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Insert(tran);
          }
        }
      }
      else
      {
        foreach (PXResult<INKitSpecStkDet, InventoryItem> pxResult in ((PXSelectBase<INKitSpecStkDet>) this.SpecComponents).Select(Array.Empty<object>()))
        {
          INKitSpecStkDet inKitSpecStkDet = PXResult<INKitSpecStkDet, InventoryItem>.op_Implicit(pxResult);
          INComponentTran componentByInventoryId = this.GetComponentByInventoryID(PXResult<INKitSpecStkDet, InventoryItem>.op_Implicit(pxResult).InventoryID);
          Decimal? nullable14;
          Decimal? nullable15;
          if (componentByInventoryId != null)
          {
            INComponentTran copy = PXCache<INComponentTran>.CreateCopy(componentByInventoryId);
            INComponentTran inComponentTran = componentByInventoryId;
            nullable14 = inComponentTran.Qty;
            Decimal? nullable16 = inKitSpecStkDet.DfltCompQty;
            Decimal num = inKitSpecStkDet.DisassemblyCoeff ?? 1M;
            nullable15 = nullable16.HasValue ? new Decimal?(nullable16.GetValueOrDefault() * num) : new Decimal?();
            Decimal? nullable17;
            if (!(nullable14.HasValue & nullable15.HasValue))
            {
              nullable16 = new Decimal?();
              nullable17 = nullable16;
            }
            else
              nullable17 = new Decimal?(nullable14.GetValueOrDefault() + nullable15.GetValueOrDefault());
            inComponentTran.Qty = nullable17;
            ((PXSelectBase) this.Base.Components).Cache.SetValueExt<INComponentTran.qty>((object) componentByInventoryId, (object) componentByInventoryId.Qty);
            GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Components).Cache, (object) componentByInventoryId, true);
            ((PXSelectBase) this.Base.Components).Cache.RaiseRowUpdated((object) componentByInventoryId, (object) copy);
          }
          else
          {
            INComponentTran tran = new INComponentTran();
            tran.DocType = ((PXSelectBase<INKitRegister>) this.Base.Document).Current.DocType;
            tran.TranType = tran.DocType == "D" ? "DSY" : "ASY";
            tran.InvtMult = this.Base.GetInvtMult((INTran) tran);
            tran.IsStockItem = new bool?(true);
            tran.InventoryID = inKitSpecStkDet.CompInventoryID;
            tran.SubItemID = inKitSpecStkDet.CompSubItemID;
            tran.UOM = inKitSpecStkDet.UOM;
            tran.SiteID = ((PXSelectBase<INKitRegister>) this.Base.Document).Current.SiteID;
            INComponentTran inComponentTran = tran;
            nullable15 = inKitSpecStkDet.DfltCompQty;
            nullable14 = inKitSpecStkDet.DisassemblyCoeff;
            Decimal num = nullable14 ?? 1M;
            Decimal? nullable18;
            if (!nullable15.HasValue)
            {
              nullable14 = new Decimal?();
              nullable18 = nullable14;
            }
            else
              nullable18 = new Decimal?(nullable15.GetValueOrDefault() * num);
            inComponentTran.Qty = nullable18;
            ((PXSelectBase<INComponentTran>) this.Base.Components).Insert(tran);
          }
        }
        foreach (PXResult<INKitSpecNonStkDet, InventoryItem> pxResult in ((PXSelectBase<INKitSpecNonStkDet>) this.SpecOverhead).Select(Array.Empty<object>()))
        {
          INKitSpecNonStkDet kitSpecNonStkDet = PXResult<INKitSpecNonStkDet, InventoryItem>.op_Implicit(pxResult);
          INOverheadTran overheadByInventoryId = this.GetOverheadByInventoryID(PXResult<INKitSpecNonStkDet, InventoryItem>.op_Implicit(pxResult).InventoryID);
          if (overheadByInventoryId != null)
          {
            INOverheadTran inOverheadTran = overheadByInventoryId;
            Decimal? qty = inOverheadTran.Qty;
            Decimal? dfltCompQty = kitSpecNonStkDet.DfltCompQty;
            inOverheadTran.Qty = qty.HasValue & dfltCompQty.HasValue ? new Decimal?(qty.GetValueOrDefault() + dfltCompQty.GetValueOrDefault()) : new Decimal?();
            ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Update(overheadByInventoryId);
          }
          else
          {
            INOverheadTran tran = new INOverheadTran()
            {
              DocType = ((PXSelectBase<INKitRegister>) this.Base.Document).Current.DocType
            };
            tran.TranType = tran.DocType == "D" ? "DSY" : "ASY";
            tran.InvtMult = this.Base.GetInvtMult(tran);
            tran.InventoryID = kitSpecNonStkDet.CompInventoryID;
            tran.Qty = kitSpecNonStkDet.DfltCompQty;
            tran.UOM = kitSpecNonStkDet.UOM;
            tran.SiteID = ((PXSelectBase<INKitRegister>) this.Base.Document).Current.SiteID;
            ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Insert(tran);
          }
        }
      }
    }

    public virtual void RemoveKit(string serialNumber, int? inventoryID)
    {
      INTranSplit originalSplit;
      INTran originalMasterTran;
      INRegister originalDoc;
      this.SearchOriginalAssembySplitLine(serialNumber, inventoryID, out originalSplit, out originalMasterTran, out originalDoc);
      if (originalSplit != null)
      {
        Decimal num1 = originalMasterTran.Qty.Value;
        foreach (PXResult<INTran, InventoryItem, INKitSpecStkDet> pxResult in PXSelectBase<INTran, PXSelectJoin<INTran, InnerJoin<InventoryItem, On<INTran.FK.InventoryItem>, LeftJoin<INKitSpecStkDet, On<INTran.inventoryID, Equal<INKitSpecStkDet.compInventoryID>>>>, Where<INTran.docType, Equal<INDocType.production>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>, And<INTran.invtMult, Equal<shortMinus1>, And<INKitSpecStkDet.kitInventoryID, Equal<Required<INKitSpecStkDet.kitInventoryID>>, And<INKitSpecStkDet.revisionID, Equal<Required<INKitSpecStkDet.revisionID>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
        {
          (object) originalDoc.RefNbr,
          (object) originalDoc.KitInventoryID,
          (object) originalDoc.KitRevisionID
        }))
        {
          InventoryItem inventoryItem = PXResult<INTran, InventoryItem, INKitSpecStkDet>.op_Implicit(pxResult);
          INTran originalComponent = PXResult<INTran, InventoryItem, INKitSpecStkDet>.op_Implicit(pxResult);
          INKitSpecStkDet inKitSpecStkDet = PXResult<INTran, InventoryItem, INKitSpecStkDet>.op_Implicit(pxResult);
          INComponentTran componentByInventoryId = this.GetComponentByInventoryID(inventoryItem.InventoryID);
          if (componentByInventoryId != null)
          {
            ((PXSelectBase<INComponentTran>) this.Base.Components).Current = componentByInventoryId;
            Decimal? qty;
            Decimal? nullable1;
            if (this.Base.IsSerialNumbered(inventoryItem.InventoryID))
            {
              foreach (PXResult<INTranSplit> componentSplit in this.GetComponentSplits(originalSplit, originalComponent))
              {
                INTranSplit inTranSplit = PXResult<INTranSplit>.op_Implicit(componentSplit);
                INComponentTranSplit row = PXResultset<INComponentTranSplit>.op_Implicit(PXSelectBase<INComponentTranSplit, PXSelect<INComponentTranSplit, Where<INComponentTranSplit.docType, Equal<Current<INKitRegister.docType>>, And<INComponentTranSplit.refNbr, Equal<Current<INKitRegister.refNbr>>, And<INComponentTranSplit.lineNbr, Equal<Required<INComponentTranSplit.lineNbr>>, And<INComponentTranSplit.lotSerialNbr, Equal<Required<INComponentTranSplit.lotSerialNbr>>>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
                {
                  (object) componentByInventoryId.LineNbr,
                  (object) inTranSplit.LotSerialNbr
                }));
                INComponentTran inComponentTran1 = (INComponentTran) LSParentAttribute.SelectParent(((PXSelectBase) this.ComponentSplits).Cache, (object) row, typeof (INComponentTran));
                ((PXSelectBase<INComponentTranSplit>) this.ComponentSplits).Delete(row);
                INComponentTran copy = PXCache<INComponentTran>.CreateCopy(inComponentTran1);
                INComponentTran inComponentTran2 = copy;
                qty = inComponentTran2.Qty;
                Decimal? nullable2;
                if (!qty.HasValue)
                {
                  nullable1 = new Decimal?();
                  nullable2 = nullable1;
                }
                else
                  nullable2 = new Decimal?(Decimal.op_Decrement(qty.GetValueOrDefault()));
                inComponentTran2.Qty = nullable2;
                ((PXSelectBase) this.Base.Components).Cache.Update((object) copy);
              }
            }
            else
            {
              INComponentTran copy = PXCache<INComponentTran>.CreateCopy(componentByInventoryId);
              INComponentTran inComponentTran = componentByInventoryId;
              qty = inComponentTran.Qty;
              Decimal? nullable3 = originalComponent.Qty;
              Decimal num2 = num1;
              nullable1 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / num2) : new Decimal?();
              Decimal? disassemblyCoeff = inKitSpecStkDet.DisassemblyCoeff;
              Decimal? nullable4;
              if (!(nullable1.HasValue & disassemblyCoeff.HasValue))
              {
                nullable3 = new Decimal?();
                nullable4 = nullable3;
              }
              else
                nullable4 = new Decimal?(nullable1.GetValueOrDefault() * disassemblyCoeff.GetValueOrDefault());
              nullable3 = nullable4;
              Decimal num3 = nullable3 ?? 1M;
              Decimal? nullable5;
              if (!qty.HasValue)
              {
                nullable3 = new Decimal?();
                nullable5 = nullable3;
              }
              else
                nullable5 = new Decimal?(qty.GetValueOrDefault() - num3);
              inComponentTran.Qty = nullable5;
              nullable1 = componentByInventoryId.Qty;
              Decimal num4 = 0M;
              if (nullable1.GetValueOrDefault() < num4 & nullable1.HasValue)
                componentByInventoryId.Qty = new Decimal?(0M);
              ((PXSelectBase) this.Base.Components).Cache.SetValueExt<INComponentTran.qty>((object) componentByInventoryId, (object) componentByInventoryId.Qty);
              GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Components).Cache, (object) componentByInventoryId, true);
              ((PXSelectBase) this.Base.Components).Cache.RaiseRowUpdated((object) componentByInventoryId, (object) copy);
            }
          }
        }
        foreach (PXResult<INTran, InventoryItem, INKitSpecNonStkDet> pxResult in PXSelectBase<INTran, PXSelectJoin<INTran, InnerJoin<InventoryItem, On<INTran.FK.InventoryItem>, LeftJoin<INKitSpecNonStkDet, On<INTran.inventoryID, Equal<INKitSpecNonStkDet.compInventoryID>>>>, Where<INTran.docType, Equal<INDocType.production>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>, And<INTran.invtMult, Equal<shortMinus1>, And<INKitSpecNonStkDet.kitInventoryID, Equal<Required<INKitSpecNonStkDet.kitInventoryID>>, And<INKitSpecNonStkDet.revisionID, Equal<Required<INKitSpecNonStkDet.revisionID>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
        {
          (object) originalDoc.RefNbr,
          (object) originalDoc.KitInventoryID,
          (object) originalDoc.KitRevisionID
        }))
        {
          InventoryItem inventoryItem = PXResult<INTran, InventoryItem, INKitSpecNonStkDet>.op_Implicit(pxResult);
          INTran inTran = PXResult<INTran, InventoryItem, INKitSpecNonStkDet>.op_Implicit(pxResult);
          PXResult<INTran, InventoryItem, INKitSpecNonStkDet>.op_Implicit(pxResult);
          INOverheadTran overheadByInventoryId = this.GetOverheadByInventoryID(inventoryItem.InventoryID);
          if (overheadByInventoryId != null)
          {
            INOverheadTran inOverheadTran = overheadByInventoryId;
            Decimal? qty = inOverheadTran.Qty;
            Decimal? nullable6 = inTran.Qty;
            Decimal num5 = num1;
            Decimal? nullable7 = nullable6.HasValue ? new Decimal?(nullable6.GetValueOrDefault() / num5) : new Decimal?();
            Decimal? nullable8;
            if (!(qty.HasValue & nullable7.HasValue))
            {
              nullable6 = new Decimal?();
              nullable8 = nullable6;
            }
            else
              nullable8 = new Decimal?(qty.GetValueOrDefault() - nullable7.GetValueOrDefault());
            inOverheadTran.Qty = nullable8;
            nullable7 = overheadByInventoryId.Qty;
            Decimal num6 = 0M;
            if (nullable7.GetValueOrDefault() < num6 & nullable7.HasValue)
              overheadByInventoryId.Qty = new Decimal?(0M);
            ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Update(overheadByInventoryId);
          }
        }
      }
      else
      {
        foreach (PXResult<INKitSpecStkDet, InventoryItem> pxResult in ((PXSelectBase<INKitSpecStkDet>) this.SpecComponents).Select(Array.Empty<object>()))
        {
          INKitSpecStkDet inKitSpecStkDet = PXResult<INKitSpecStkDet, InventoryItem>.op_Implicit(pxResult);
          INComponentTran componentByInventoryId = this.GetComponentByInventoryID(PXResult<INKitSpecStkDet, InventoryItem>.op_Implicit(pxResult).InventoryID);
          if (componentByInventoryId != null)
          {
            INComponentTran copy = PXCache<INComponentTran>.CreateCopy(componentByInventoryId);
            INComponentTran inComponentTran = componentByInventoryId;
            Decimal? qty = inComponentTran.Qty;
            Decimal? nullable = inKitSpecStkDet.DfltCompQty;
            Decimal? disassemblyCoeff = inKitSpecStkDet.DisassemblyCoeff;
            Decimal num7 = (nullable.HasValue & disassemblyCoeff.HasValue ? new Decimal?(nullable.GetValueOrDefault() * disassemblyCoeff.GetValueOrDefault()) : new Decimal?()) ?? 1M;
            inComponentTran.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() - num7) : new Decimal?();
            nullable = componentByInventoryId.Qty;
            Decimal num8 = 0M;
            if (nullable.GetValueOrDefault() < num8 & nullable.HasValue)
              componentByInventoryId.Qty = new Decimal?(0M);
            ((PXSelectBase) this.Base.Components).Cache.SetValueExt<INComponentTran.qty>((object) componentByInventoryId, (object) componentByInventoryId.Qty);
            GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Components).Cache, (object) componentByInventoryId, true);
            ((PXSelectBase) this.Base.Components).Cache.RaiseRowUpdated((object) componentByInventoryId, (object) copy);
          }
        }
        foreach (PXResult<INKitSpecNonStkDet, InventoryItem> pxResult in ((PXSelectBase<INKitSpecNonStkDet>) this.SpecOverhead).Select(Array.Empty<object>()))
        {
          INKitSpecNonStkDet kitSpecNonStkDet = PXResult<INKitSpecNonStkDet, InventoryItem>.op_Implicit(pxResult);
          INOverheadTran overheadByInventoryId = this.GetOverheadByInventoryID(PXResult<INKitSpecNonStkDet, InventoryItem>.op_Implicit(pxResult).InventoryID);
          if (overheadByInventoryId != null)
          {
            INOverheadTran inOverheadTran = overheadByInventoryId;
            Decimal? qty = inOverheadTran.Qty;
            Decimal? nullable = kitSpecNonStkDet.DfltCompQty;
            inOverheadTran.Qty = qty.HasValue & nullable.HasValue ? new Decimal?(qty.GetValueOrDefault() - nullable.GetValueOrDefault()) : new Decimal?();
            nullable = overheadByInventoryId.Qty;
            Decimal num = 0M;
            if (nullable.GetValueOrDefault() < num & nullable.HasValue)
              overheadByInventoryId.Qty = new Decimal?(0M);
            ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Update(overheadByInventoryId);
          }
        }
      }
    }

    public virtual void SearchOriginalAssembySplitLine(
      string serialNumber,
      int? inventoryID,
      out INTranSplit originalSplit,
      out INTran originalMasterTran,
      out INRegister originalDoc)
    {
      originalSplit = (INTranSplit) null;
      originalMasterTran = (INTran) null;
      originalDoc = (INRegister) null;
      if (string.IsNullOrEmpty(serialNumber))
        return;
      PXResultset<INTranSplit> pxResultset = ((PXSelectBase<INTranSplit>) new PXSelectJoin<INTranSplit, InnerJoin<INTran, On<INTranSplit.FK.Tran>, InnerJoin<INRegister, On<INRegister.FK.KitTran>>>, Where<INRegister.docType, Equal<INDocType.production>, And<INTranSplit.lotSerialNbr, Equal<Required<INTranSplit.lotSerialNbr>>, And<INTranSplit.inventoryID, Equal<Required<INTranSplit.inventoryID>>, And<INTran.qty, NotEqual<decimal0>>>>>>((PXGraph) this.Base)).Select(new object[2]
      {
        (object) serialNumber,
        (object) inventoryID
      });
      if (pxResultset == null || pxResultset.Count <= 0)
        return;
      PXResult<INTranSplit, INTran, INRegister> pxResult = (PXResult<INTranSplit, INTran, INRegister>) pxResultset[0];
      originalSplit = PXResult<INTranSplit, INTran, INRegister>.op_Implicit(pxResult);
      originalMasterTran = PXResult<INTranSplit, INTran, INRegister>.op_Implicit(pxResult);
      originalDoc = PXResult<INTranSplit, INTran, INRegister>.op_Implicit(pxResult);
    }

    public virtual INComponentTran GetComponentByInventoryID(int? inventoryID)
    {
      return PXResultset<INComponentTran>.op_Implicit(PXSelectBase<INComponentTran, PXSelect<INComponentTran, Where<INComponentTran.docType, Equal<Current<INKitRegister.docType>>, And<INComponentTran.refNbr, Equal<Current<INKitRegister.refNbr>>, And<INComponentTran.inventoryID, Equal<Required<INComponentTran.inventoryID>>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) inventoryID
      }));
    }

    public virtual INOverheadTran GetOverheadByInventoryID(int? inventoryID)
    {
      return PXResultset<INOverheadTran>.op_Implicit(PXSelectBase<INOverheadTran, PXSelect<INOverheadTran, Where<INOverheadTran.docType, Equal<Current<INKitRegister.docType>>, And<INOverheadTran.refNbr, Equal<Current<INKitRegister.refNbr>>, And<INOverheadTran.inventoryID, Equal<Required<INOverheadTran.inventoryID>>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) inventoryID
      }));
    }

    public virtual PXResultset<INTranSplit> GetComponentSplits(
      INTranSplit originalKitSplit,
      INTran originalComponent)
    {
      return PXSelectBase<INTranSplit, PXSelectJoin<INTranSplit, InnerJoin<INKitSerialPart, On<INKitSerialPart.docType, Equal<INTranSplit.docType>, And<INKitSerialPart.refNbr, Equal<INTranSplit.refNbr>, And<INKitSerialPart.partLineNbr, Equal<INTranSplit.lineNbr>, And<INKitSerialPart.partSplitLineNbr, Equal<INTranSplit.splitLineNbr>>>>>>, Where<INKitSerialPart.docType, Equal<Required<INKitSerialPart.docType>>, And<INKitSerialPart.refNbr, Equal<Required<INKitSerialPart.refNbr>>, And<INKitSerialPart.kitLineNbr, Equal<Required<INKitSerialPart.kitLineNbr>>, And<INKitSerialPart.kitSplitLineNbr, Equal<Required<INKitSerialPart.kitSplitLineNbr>>, And<INKitSerialPart.partLineNbr, Equal<Required<INKitSerialPart.partLineNbr>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[5]
      {
        (object) originalKitSplit.DocType,
        (object) originalKitSplit.RefNbr,
        (object) originalKitSplit.LineNbr,
        (object) originalKitSplit.SplitLineNbr,
        (object) originalComponent.LineNbr
      });
    }

    public virtual void RebuildComponents(Decimal numberOfKits)
    {
      if (((PXGraph) this.Base).IsCopyPasteContext)
        return;
      foreach (PXResult<INComponentTran> pxResult in ((PXSelectBase<INComponentTran>) this.Base.Components).Select(Array.Empty<object>()))
        ((PXSelectBase<INComponentTran>) this.Base.Components).Delete(PXResult<INComponentTran>.op_Implicit(pxResult));
      foreach (PXResult<INOverheadTran> pxResult in ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Select(Array.Empty<object>()))
        ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Delete(PXResult<INOverheadTran>.op_Implicit(pxResult));
      if (((PXSelectBase<INKitRegister>) this.Base.Document).Current == null)
        return;
      foreach (PXResult<INKitSpecStkDet, InventoryItem> pxResult in ((PXSelectBase<INKitSpecStkDet>) this.SpecComponents).Select(Array.Empty<object>()))
      {
        INKitSpecStkDet inKitSpecStkDet = PXResult<INKitSpecStkDet, InventoryItem>.op_Implicit(pxResult);
        PXResult<INKitSpecStkDet, InventoryItem>.op_Implicit(pxResult);
        INComponentTran tran = new INComponentTran();
        tran.DocType = ((PXSelectBase<INKitRegister>) this.Base.Document).Current.DocType;
        tran.TranType = tran.DocType == "D" ? "DSY" : "ASY";
        tran.InvtMult = this.Base.GetInvtMult((INTran) tran);
        tran.IsStockItem = new bool?(true);
        tran.InventoryID = inKitSpecStkDet.CompInventoryID;
        INComponentTran copy = PXCache<INComponentTran>.CreateCopy(((PXSelectBase<INComponentTran>) this.Base.Components).Insert(tran));
        copy.SubItemID = inKitSpecStkDet.CompSubItemID;
        Decimal? nullable1;
        Decimal? nullable2;
        if (copy.DocType == "D")
        {
          INComponentTran inComponentTran = copy;
          Decimal? nullable3 = inKitSpecStkDet.DfltCompQty;
          Decimal num = numberOfKits;
          nullable1 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num) : new Decimal?();
          nullable2 = inKitSpecStkDet.DisassemblyCoeff;
          Decimal? nullable4;
          if (!(nullable1.HasValue & nullable2.HasValue))
          {
            nullable3 = new Decimal?();
            nullable4 = nullable3;
          }
          else
            nullable4 = new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault());
          inComponentTran.Qty = nullable4;
        }
        else
        {
          INComponentTran inComponentTran = copy;
          nullable2 = inKitSpecStkDet.DfltCompQty;
          Decimal num = numberOfKits;
          Decimal? nullable5;
          if (!nullable2.HasValue)
          {
            nullable1 = new Decimal?();
            nullable5 = nullable1;
          }
          else
            nullable5 = new Decimal?(nullable2.GetValueOrDefault() * num);
          inComponentTran.Qty = nullable5;
        }
        copy.UOM = inKitSpecStkDet.UOM;
        copy.SiteID = ((PXSelectBase<INKitRegister>) this.Base.Document).Current.SiteID;
        if (((PXSelectBase<INKitRegister>) this.Base.Document).Current.DocType == "D")
          copy.LocationID = ((PXSelectBase<INKitRegister>) this.Base.Document).Current.LocationID;
        ((PXSelectBase<INComponentTran>) this.Base.Components).Update(copy);
      }
      foreach (PXResult<INKitSpecNonStkDet, InventoryItem> pxResult in ((PXSelectBase<INKitSpecNonStkDet>) this.SpecOverhead).Select(Array.Empty<object>()))
      {
        INKitSpecNonStkDet kitSpecNonStkDet = PXResult<INKitSpecNonStkDet, InventoryItem>.op_Implicit(pxResult);
        PXResult<INKitSpecNonStkDet, InventoryItem>.op_Implicit(pxResult);
        INOverheadTran tran = new INOverheadTran()
        {
          DocType = ((PXSelectBase<INKitRegister>) this.Base.Document).Current.DocType
        };
        tran.TranType = tran.DocType == "D" ? "DSY" : "ASY";
        tran.InvtMult = this.Base.GetInvtMult(tran);
        tran.InventoryID = kitSpecNonStkDet.CompInventoryID;
        INOverheadTran inOverheadTran = tran;
        Decimal? dfltCompQty = kitSpecNonStkDet.DfltCompQty;
        Decimal num = numberOfKits;
        Decimal? nullable = dfltCompQty.HasValue ? new Decimal?(dfltCompQty.GetValueOrDefault() * num) : new Decimal?();
        inOverheadTran.Qty = nullable;
        tran.UOM = kitSpecNonStkDet.UOM;
        tran.SiteID = ((PXSelectBase<INKitRegister>) this.Base.Document).Current.SiteID;
        ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Insert(tran);
      }
    }

    public virtual void RecountComponents(Decimal numberOfKits, Decimal oldNumberOfKits)
    {
      foreach (PXResult<INComponentTran, InventoryItem, INKitSpecStkDet> pxResult in ((PXSelectBase<INComponentTran>) this.Base.Components).Select(Array.Empty<object>()))
      {
        INComponentTran copy = (INComponentTran) ((PXSelectBase) this.Base.Components).Cache.CreateCopy((object) PXResult<INComponentTran, InventoryItem, INKitSpecStkDet>.op_Implicit(pxResult));
        INKitSpecStkDet inKitSpecStkDet = PXResult<INComponentTran, InventoryItem, INKitSpecStkDet>.op_Implicit(pxResult);
        Decimal? nullable1 = inKitSpecStkDet.DfltCompQty;
        Decimal? nullable2;
        Decimal? nullable3;
        if (nullable1.HasValue)
        {
          if (copy.DocType == "D")
          {
            INComponentTran inComponentTran = copy;
            nullable2 = inKitSpecStkDet.DfltCompQty;
            Decimal num = numberOfKits;
            nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num) : new Decimal?();
            nullable3 = inKitSpecStkDet.DisassemblyCoeff;
            Decimal? nullable4;
            if (!(nullable1.HasValue & nullable3.HasValue))
            {
              nullable2 = new Decimal?();
              nullable4 = nullable2;
            }
            else
              nullable4 = new Decimal?(nullable1.GetValueOrDefault() * nullable3.GetValueOrDefault());
            inComponentTran.Qty = nullable4;
          }
          else
          {
            INComponentTran inComponentTran = copy;
            nullable3 = inKitSpecStkDet.DfltCompQty;
            Decimal num = numberOfKits;
            Decimal? nullable5;
            if (!nullable3.HasValue)
            {
              nullable1 = new Decimal?();
              nullable5 = nullable1;
            }
            else
              nullable5 = new Decimal?(nullable3.GetValueOrDefault() * num);
            inComponentTran.Qty = nullable5;
          }
        }
        else if (oldNumberOfKits > 0M)
        {
          INComponentTran inComponentTran = copy;
          nullable1 = copy.Qty;
          Decimal num1 = numberOfKits;
          Decimal? nullable6;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable6 = nullable2;
          }
          else
            nullable6 = new Decimal?(nullable1.GetValueOrDefault() * num1);
          nullable3 = nullable6;
          Decimal num2 = oldNumberOfKits;
          Decimal? nullable7;
          if (!nullable3.HasValue)
          {
            nullable1 = new Decimal?();
            nullable7 = nullable1;
          }
          else
            nullable7 = new Decimal?(nullable3.GetValueOrDefault() / num2);
          inComponentTran.Qty = nullable7;
        }
        else
          copy.Qty = new Decimal?(numberOfKits);
        if (inKitSpecStkDet.UOM != null)
          copy.UOM = inKitSpecStkDet.UOM;
        ((PXSelectBase<INComponentTran>) this.Base.Components).Update(copy);
      }
      foreach (PXResult<INOverheadTran, InventoryItem, INKitSpecNonStkDet> pxResult in ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Select(Array.Empty<object>()))
      {
        INOverheadTran copy = (INOverheadTran) ((PXSelectBase) this.Base.Overhead).Cache.CreateCopy((object) PXResult<INOverheadTran, InventoryItem, INKitSpecNonStkDet>.op_Implicit(pxResult));
        INKitSpecNonStkDet kitSpecNonStkDet = PXResult<INOverheadTran, InventoryItem, INKitSpecNonStkDet>.op_Implicit(pxResult);
        Decimal? nullable8 = kitSpecNonStkDet.DfltCompQty;
        if (nullable8.HasValue)
        {
          INOverheadTran inOverheadTran = copy;
          nullable8 = kitSpecNonStkDet.DfltCompQty;
          Decimal? nullable9 = new Decimal?((nullable8 ?? 1M) * numberOfKits);
          inOverheadTran.Qty = nullable9;
        }
        else if (oldNumberOfKits > 0M)
        {
          INOverheadTran inOverheadTran = copy;
          Decimal? nullable10 = copy.Qty;
          Decimal num3 = numberOfKits;
          nullable8 = nullable10.HasValue ? new Decimal?(nullable10.GetValueOrDefault() * num3) : new Decimal?();
          Decimal num4 = oldNumberOfKits;
          Decimal? nullable11;
          if (!nullable8.HasValue)
          {
            nullable10 = new Decimal?();
            nullable11 = nullable10;
          }
          else
            nullable11 = new Decimal?(nullable8.GetValueOrDefault() / num4);
          inOverheadTran.Qty = nullable11;
        }
        else
          copy.Qty = new Decimal?(numberOfKits);
        if (kitSpecNonStkDet.UOM != null)
          copy.UOM = kitSpecNonStkDet.UOM;
        ((PXSelectBase<INOverheadTran>) this.Base.Overhead).Update(copy);
      }
    }
  }
}
