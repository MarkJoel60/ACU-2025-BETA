// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTransferEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions.Periods;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Interfaces;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Descriptor;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.DAC.Projections;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.PO;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.IN;

public class INTransferEntry : INRegisterEntryBase
{
  internal bool SuppressLocationDefaultingForWMS;
  public 
  #nullable disable
  PXSelectJoin<INRegister, LeftJoin<INSite, On<INSite.siteID, Equal<INRegister.siteID>>>, Where<INRegister.docType, Equal<INDocType.transfer>, And<Where<INSite.siteID, IsNull, Or<Match<INSite, Current<AccessInfo.userName>>>>>>> transfer;
  public PXSelect<INRegister, Where<INRegister.docType, Equal<INDocType.transfer>, And<INRegister.refNbr, Equal<Current<INRegister.refNbr>>>>> CurrentDocument;
  [PXImport(typeof (INRegister))]
  [PXCopyPasteHiddenFields(new Type[] {typeof (INTran.iNTransitQty), typeof (INTran.receiptedQty), typeof (INTran.iNTransitBaseQty), typeof (INTran.receiptedBaseQty)})]
  public PXSelect<INTran, Where<INTran.docType, Equal<INDocType.transfer>, And<INTran.refNbr, Equal<Current<INRegister.refNbr>>, And<INTran.invtMult, In3<InventoryMultiplicator.decrease, InventoryMultiplicator.noUpdate>>>>> transactions;
  [PXCopyPasteHiddenView]
  public PXSelect<INTranSplit, Where<INTranSplit.docType, Equal<INDocType.transfer>, And<INTranSplit.refNbr, Equal<Current<INTran.refNbr>>, And<INTranSplit.lineNbr, Equal<Current<INTran.lineNbr>>>>>> splits;
  public PXSelect<INItemSite, Where<INItemSite.siteID, Equal<Required<INItemSite.siteID>>, And<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>>>> itemsite;

  [PXMergeAttributes]
  [PXSelector(typeof (Search2<INRegister.refNbr, LeftJoin<INSite, On<INSite.siteID, Equal<INRegister.siteID>>>, Where<INRegister.docType, Equal<BqlField<INRegister.docType, IBqlString>.AsOptional>, And<Match<INSite, Current<AccessInfo.userName>>>>, OrderBy<Desc<INRegister.refNbr>>>), Filterable = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<INRegister.refNbr> e)
  {
  }

  [Branch(typeof (Search<INSite.branchID, Where<INSite.siteID, Equal<Current<INRegister.siteID>>>>), null, true, true, true, IsDetail = false, Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INRegister.branchID> e)
  {
  }

  [Site(DisplayName = "Warehouse ID", DescriptionField = typeof (INSite.descr))]
  [PXDefault]
  [PXRestrictor(typeof (Where<INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new Type[] {typeof (INSite.siteCD)})]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<INSite.branchID, Current<AccessInfo.branchID>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INRegister.siteID> e)
  {
  }

  [ToSite(typeof (INRegister.transferType), DisplayName = "To Warehouse ID", DescriptionField = typeof (INSite.descr))]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<INRegister.toSiteID> e)
  {
  }

  [PXMergeAttributes]
  [INTransferEntry.INOpenPeriodTransfer(IsHeader = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<INRegister.finPeriodID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  protected virtual void _(PX.Data.Events.CacheAttached<INRegister.pOReceiptNbr> e)
  {
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault("TRX")]
  [PXUIField(Enabled = false, Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.tranType> e)
  {
  }

  [Branch(typeof (INRegister.branchID), null, true, true, true)]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.branchID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Line Number", Enabled = false, Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.lineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Selector<INTran.toSiteID, INSite.branchID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.destBranchID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (INRegister.toSiteID))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.toSiteID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (LocationAvailAttribute), "Enabled", true)]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.toLocationID> e)
  {
  }

  [PXDefault(typeof (SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<INTran.inventoryID, IBqlInt>.FromCurrent>>), SourceField = typeof (InventoryItem.baseUnit), CacheGlobal = true)]
  [INUnit(typeof (INTran.inventoryID))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.uOM> e)
  {
  }

  [PXMergeAttributes]
  [PXDBScalar(typeof (Search5<INLocationStatusInTransit.qtyOnHand, InnerJoin<INTransitLine, On<INTransitLine.costSiteID, Equal<INLocationStatusInTransit.locationID>>>, Where<INLocationStatusInTransit.siteID, Equal<SiteAnyAttribute.transitSiteID>, And<INTransitLine.transferLineNbr, Equal<INTran.lineNbr>, And<INTransitLine.transferNbr, Equal<INTran.refNbr>>>>, Aggregate<GroupBy<INTransitLine.transferNbr, GroupBy<INTransitLine.transferLineNbr, Sum<INLocationStatusInTransit.qtyOnHand>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.iNTransitBaseQty> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Sub<INTran.baseQty, INTran.iNTransitBaseQty>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.receiptedBaseQty> e)
  {
  }

  public INTransferEntry()
  {
    INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
    PXUIFieldAttribute.SetVisible<INTran.tranType>(((PXSelectBase) this.transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<INTran.tranType>(((PXSelectBase) this.transactions).Cache, (object) null, false);
    OpenPeriodAttribute.SetValidatePeriod<INRegister.finPeriodID>(((PXSelectBase) this.transfer).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter.negAvailQty>(INTransferEntry.\u003C\u003Ec.\u003C\u003E9__21_0 ?? (INTransferEntry.\u003C\u003Ec.\u003C\u003E9__21_0 = new PXFieldDefaulting((object) INTransferEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__21_0))));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INRegister, INRegister.docType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INRegister, INRegister.docType>, INRegister, object>) e).NewValue = (object) "T";
  }

  protected virtual void Set1Step(INRegister row)
  {
    int? siteId = row.SiteID;
    int? toSiteId = row.ToSiteID;
    if (!(siteId.GetValueOrDefault() == toSiteId.GetValueOrDefault() & siteId.HasValue == toSiteId.HasValue))
      return;
    row.TransferType = "1";
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INRegister, INRegister.siteID> e)
  {
    this.Set1Step(e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INRegister, INRegister.siteID>>) e).Cache.SetDefaultExt<INRegister.branchID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INRegister, INRegister.toSiteID> e)
  {
    if (e.Row != null)
    {
      foreach (PXResult<INTran> pxResult in ((PXSelectBase<INTran>) this.transactions).Select(Array.Empty<object>()))
      {
        INTran copy = (INTran) ((PXSelectBase) this.transactions).Cache.CreateCopy((object) PXResult<INTran>.op_Implicit(pxResult));
        copy.ToSiteID = e.Row.ToSiteID;
        ((PXSelectBase) this.transactions).Cache.Update((object) copy);
      }
    }
    this.Set1Step(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INRegister, INRegister.transferType> e)
  {
    object toSiteId = (object) e.Row.ToSiteID;
    try
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INRegister, INRegister.transferType>>) e).Cache.RaiseFieldVerifying<INRegister.toSiteID>((object) e.Row, ref toSiteId);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INRegister, INRegister.transferType>>) e).Cache.RaiseExceptionHandling<INRegister.toSiteID>((object) e.Row, toSiteId, (Exception) null);
    }
    catch (PXSetPropertyException ex)
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INRegister, INRegister.transferType>>) e).Cache.RaiseExceptionHandling<INRegister.toSiteID>((object) e.Row, toSiteId, (Exception) new PXSetPropertyException((Exception) ex, (PXErrorLevel) 4, "Selected Warehouse is not allowed in {0} transfer", new object[1]
      {
        (object) "1-Step"
      }));
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INRegister> e)
  {
    if (PXDBOperationExt.Command(e.Operation) == 3)
      return;
    object obj = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INRegister>>) e).Cache.GetValue<INRegister.toSiteID>((object) e.Row);
    try
    {
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INRegister>>) e).Cache.RaiseFieldVerifying<INRegister.toSiteID>((object) e.Row, ref obj);
    }
    catch (PXSetPropertyException ex)
    {
      PXCache cach = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INRegister>>) e).Cache.Graph.Caches[typeof (INRegister)];
      if (!((string) cach.GetValue<INRegister.transferType>(cach.Current) == "1"))
        return;
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INRegister>>) e).Cache.RaiseExceptionHandling<INRegister.toSiteID>((object) e.Row, obj, (Exception) new PXSetPropertyException((Exception) ex, (PXErrorLevel) 4, "Selected Warehouse is not allowed in {0} transfer", new object[1]
      {
        (object) "1-Step"
      }));
    }
  }

  protected override void _(PX.Data.Events.RowUpdated<INRegister> e)
  {
    base._(e);
    bool? requireControlTotal = ((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal;
    bool flag1 = false;
    if (requireControlTotal.GetValueOrDefault() == flag1 & requireControlTotal.HasValue)
      this.FillControlValue<INRegister.controlQty, INRegister.totalQty>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INRegister>>) e).Cache, e.Row);
    else if (((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal.GetValueOrDefault())
    {
      bool? nullable = e.Row.Hold;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      {
        nullable = e.Row.Released;
        bool flag3 = false;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
          this.RaiseControlValueError<INRegister.controlQty, INRegister.totalQty>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INRegister>>) e).Cache, e.Row);
      }
    }
    this.ValidateTransferTrans(e.Row, e.OldRow);
  }

  protected virtual void ValidateTransferTrans(INRegister row, INRegister oldRow)
  {
    if (((PXSelectBase) this.transfer).Cache.ObjectsEqual<INRegister.transferType>((object) row, (object) oldRow))
      return;
    foreach (PXResult<INTran> pxResult in ((PXSelectBase<INTran>) this.transactions).Select(Array.Empty<object>()))
    {
      INTran tran = PXResult<INTran>.op_Implicit(pxResult);
      if (!this.IsTransferTranValid(tran))
        GraphHelper.MarkUpdated(((PXSelectBase) this.transactions).Cache, (object) tran);
    }
  }

  protected virtual bool IsTransferTranValid(INTran tran)
  {
    return !(((PXSelectBase<INRegister>) this.transfer).Current.TransferType == "1") || tran.ToLocationID.HasValue;
  }

  protected virtual void _(PX.Data.Events.RowSelected<INRegister> e)
  {
    if (e.Row == null)
      return;
    bool? nullable = e.Row.Released;
    bool flag1 = false;
    bool flag2 = nullable.GetValueOrDefault() == flag1 & nullable.HasValue && e.Row.OrigModule == "IN";
    if (!flag2)
      PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache, (object) e.Row, false);
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowInsert = true;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache;
    nullable = e.Row.Released;
    bool flag3 = false;
    int num1 = nullable.GetValueOrDefault() == flag3 & nullable.HasValue ? 1 : 0;
    cache1.AllowUpdate = num1 != 0;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowDelete = flag2;
    PXSelectBase<INTran> lsselect1 = this.LineSplittingExt.lsselect;
    int? siteId;
    int num2;
    if (flag2)
    {
      siteId = e.Row.SiteID;
      num2 = siteId.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    ((PXSelectBase) lsselect1).AllowInsert = num2 != 0;
    PXSelectBase<INTran> lsselect2 = this.LineSplittingExt.lsselect;
    nullable = e.Row.Released;
    bool flag4 = false;
    int num3 = nullable.GetValueOrDefault() == flag4 & nullable.HasValue ? 1 : 0;
    ((PXSelectBase) lsselect2).AllowUpdate = num3 != 0;
    ((PXSelectBase) this.LineSplittingExt.lsselect).AllowDelete = flag2;
    PXUIFieldAttribute.SetEnabled<INTran.toLocationID>(((PXSelectBase) this.transactions).Cache, (object) null, flag2);
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache;
    INRegister row1 = e.Row;
    nullable = ((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal;
    int num4 = nullable.Value ? 1 : 0;
    PXUIFieldAttribute.SetVisible<INRegister.controlQty>(cache2, (object) row1, num4 != 0);
    PXCache cache3 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache;
    INRegister row2 = e.Row;
    nullable = e.Row.Released;
    bool flag5 = false;
    int num5;
    if (nullable.GetValueOrDefault() == flag5 & nullable.HasValue)
    {
      siteId = e.Row.SiteID;
      num5 = !siteId.HasValue ? 1 : (((PXSelectBase<INTran>) this.transactions).Select(Array.Empty<object>()).Count == 0 ? 1 : 0);
    }
    else
      num5 = 0;
    PXUIFieldAttribute.SetEnabled<INRegister.siteID>(cache3, (object) row2, num5 != 0);
    PXCache cache4 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache;
    INRegister row3 = e.Row;
    int num6;
    if (flag2)
    {
      siteId = e.Row.SiteID;
      if (siteId.HasValue)
      {
        siteId = e.Row.SiteID;
        int? toSiteId = e.Row.ToSiteID;
        num6 = !(siteId.GetValueOrDefault() == toSiteId.GetValueOrDefault() & siteId.HasValue == toSiteId.HasValue) ? 1 : 0;
      }
      else
        num6 = 1;
    }
    else
      num6 = 0;
    PXUIFieldAttribute.SetEnabled<INRegister.transferType>(cache4, (object) row3, num6 != 0);
    PXUIFieldAttribute.SetVisible<INTran.toLocationID>(((PXSelectBase) this.transactions).Cache, (object) null, e.Row.TransferType == "1");
    PXUIFieldAttribute.SetVisible<INTran.receiptedQty>(((PXSelectBase) this.transactions).Cache, (object) null, e.Row.TransferType != "1");
    PXUIFieldAttribute.SetVisible<INTran.iNTransitQty>(((PXSelectBase) this.transactions).Cache, (object) null, e.Row.TransferType != "1");
    PXDefaultAttribute.SetPersistingCheck<INTran.toLocationID>(((PXSelectBase) this.transactions).Cache, (object) null, e.Row.TransferType == "1" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetVisible<INTran.toCostLayerType>(((PXSelectBase) this.transactions).Cache, (object) null, e.Row.TransferType == "1");
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.docType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.docType>, INTran, object>) e).NewValue = (object) "T";
  }

  public virtual void _(
    PX.Data.Events.FieldSelecting<INTran, INTran.iNTransitQty> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<INTran, INTran.iNTransitQty>>) e).ReturnValue = (object) INUnitAttribute.ConvertFromBase<INTran.inventoryID, INTran.uOM>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<INTran, INTran.iNTransitQty>>) e).Cache, (object) e.Row, e.Row.INTransitBaseQty.GetValueOrDefault(), INPrecision.QUANTITY);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<INTran, INTran.receiptedQty> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<INTran, INTran.receiptedQty>>) e).ReturnValue = (object) INUnitAttribute.ConvertFromBase<INTran.inventoryID, INTran.uOM>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<INTran, INTran.receiptedQty>>) e).Cache, (object) e.Row, e.Row.ReceiptedBaseQty.GetValueOrDefault(), INPrecision.QUANTITY);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INTran, INTran.toLocationID> e)
  {
    if (e.Row == null || this.SuppressLocationDefaultingForWMS)
      return;
    INItemSite inItemSite = PXResultset<INItemSite>.op_Implicit(((PXSelectBase<INItemSite>) this.itemsite).SelectWindowed(0, 1, new object[2]
    {
      (object) e.Row.ToSiteID,
      (object) e.Row.InventoryID
    }));
    if (inItemSite != null)
    {
      PX.Data.Events.FieldDefaulting<INTran, INTran.toLocationID> fieldDefaulting = e;
      int? siteId = e.Row.SiteID;
      int? toSiteId = e.Row.ToSiteID;
      // ISSUE: variable of a boxed type
      __Boxed<int?> local = (ValueType) (siteId.GetValueOrDefault() == toSiteId.GetValueOrDefault() & siteId.HasValue == toSiteId.HasValue ? inItemSite.DfltShipLocationID : inItemSite.DfltReceiptLocationID);
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toLocationID>, INTran, object>) fieldDefaulting).NewValue = (object) local;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toLocationID>>) e).Cancel = true;
    }
    else
    {
      INSite inSite = INSite.PK.Find((PXGraph) this, e.Row.ToSiteID);
      if (inSite == null)
        return;
      PX.Data.Events.FieldDefaulting<INTran, INTran.toLocationID> fieldDefaulting = e;
      int? siteId = e.Row.SiteID;
      int? toSiteId = e.Row.ToSiteID;
      // ISSUE: variable of a boxed type
      __Boxed<int?> local = (ValueType) (siteId.GetValueOrDefault() == toSiteId.GetValueOrDefault() & siteId.HasValue == toSiteId.HasValue ? inSite.ShipLocationID : inSite.ReceiptLocationID);
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toLocationID>, INTran, object>) fieldDefaulting).NewValue = (object) local;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toLocationID>>) e).Cancel = true;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INTran, INTran.locationID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.locationID>>) e).Cancel || this.SuppressLocationDefaultingForWMS)
      return;
    INItemSite inItemSite = PXResultset<INItemSite>.op_Implicit(((PXSelectBase<INItemSite>) this.itemsite).SelectWindowed(0, 1, new object[2]
    {
      (object) e.Row.SiteID,
      (object) e.Row.InventoryID
    }));
    if (inItemSite != null)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.locationID>, INTran, object>) e).NewValue = (object) inItemSite.DfltReceiptLocationID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.locationID>>) e).Cancel = true;
    }
    else
    {
      INSite inSite = INSite.PK.Find((PXGraph) this, e.Row.SiteID);
      if (inSite == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.locationID>, INTran, object>) e).NewValue = (object) inSite.ReceiptLocationID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.locationID>>) e).Cancel = true;
    }
  }

  protected override void _(PX.Data.Events.FieldUpdated<INTran, INTran.inventoryID> e)
  {
    base._(e);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.inventoryID>>) e).Cache.SetDefaultExt<INTran.toLocationID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.inventoryID>>) e).Cache.SetDefaultExt<INTran.locationID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.siteID> e)
  {
    if (((PXSelectBase<INRegister>) this.transfer).Current == null || !((PXSelectBase<INRegister>) this.transfer).Current.SiteID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.siteID>, INTran, object>) e).NewValue = (object) ((PXSelectBase<INRegister>) this.transfer).Current.SiteID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.siteID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.toSiteID> e)
  {
    if (((PXSelectBase<INRegister>) this.transfer).Current == null || !((PXSelectBase<INRegister>) this.transfer).Current.ToSiteID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toSiteID>, INTran, object>) e).NewValue = (object) ((PXSelectBase<INRegister>) this.transfer).Current.ToSiteID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toSiteID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.toSiteID> e)
  {
    if (e.Row == null)
      return;
    foreach (INTranSplit inTranSplit in ((PXSelectBase) this.splits).View.SelectMultiBound((object[]) new INTran[1]
    {
      e.Row
    }, Array.Empty<object>()))
    {
      INTranSplit copy = (INTranSplit) ((PXSelectBase) this.splits).Cache.CreateCopy((object) inTranSplit);
      copy.ToSiteID = e.Row.ToSiteID;
      ((PXSelectBase) this.splits).Cache.Update((object) copy);
    }
  }

  protected override void _(PX.Data.Events.RowInserted<INTran> e)
  {
    base._(e);
    if (e.Row == null || !EnumerableExtensions.IsIn<string>(e.Row.OrigModule, "SO", "PO"))
      return;
    this.OnForeignTranInsert(e.Row);
  }

  protected override void _(PX.Data.Events.RowPersisting<INTran> e)
  {
    base._(e);
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    INRegister current = ((PXSelectBase<INRegister>) this.CurrentDocument).Current;
    if (current == null)
      return;
    PXDefaultAttribute.SetPersistingCheck<INTran.toLocationID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache, (object) e.Row, current.TransferType == "1" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    int? nullable1 = current.SiteID;
    int? nullable2 = e.Row.SiteID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
    {
      nullable2 = current.ToSiteID;
      nullable1 = e.Row.ToSiteID;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        return;
    }
    if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache.RaiseExceptionHandling<INTran.locationID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("The warehouse in the document differs from the warehouse in the line. Remove the line and add it again to update the warehouse.", (PXErrorLevel) 5)))
      throw new PXRowPersistingException("locationID", (object) null, "The warehouse in the document differs from the warehouse in the line. Remove the line and add it again to update the warehouse.");
  }

  public override PXSelectBase<INRegister> INRegisterDataMember
  {
    get => (PXSelectBase<INRegister>) this.transfer;
  }

  public override PXSelectBase<INTran> INTranDataMember => (PXSelectBase<INTran>) this.transactions;

  public override PXSelectBase<INTran> LSSelectDataMember => this.LineSplittingExt.lsselect;

  public override PXSelectBase<INTranSplit> INTranSplitDataMember
  {
    get => (PXSelectBase<INTranSplit>) this.splits;
  }

  protected override string ScreenID => "IN304000";

  public INTransferEntry.LineSplittingExtension LineSplittingExt
  {
    get => ((PXGraph) this).FindImplementation<INTransferEntry.LineSplittingExtension>();
  }

  public INTransferEntry.ItemAvailabilityExtension ItemAvailabilityExt
  {
    get => ((PXGraph) this).FindImplementation<INTransferEntry.ItemAvailabilityExtension>();
  }

  protected override Type[] GetAlternativeKeyFields()
  {
    return new List<Type>()
    {
      typeof (INTran.inventoryID),
      typeof (INTran.locationID),
      typeof (INTran.toLocationID),
      typeof (INTran.lotSerialNbr)
    }.ToArray();
  }

  public class SiteStatusLookup : 
    INSiteStatusLookupExt<INTransferEntry, INTransferEntry.SiteStatusLookup.INSiteStatusSelected>
  {
    protected override bool IsAddItemEnabled(INRegister doc)
    {
      return ((PXSelectBase) this.Transactions).AllowInsert;
    }

    protected override INTran InitTran(
      INTran newTran,
      INTransferEntry.SiteStatusLookup.INSiteStatusSelected selected)
    {
      newTran.ToSiteID = this.Document.Current.ToSiteID;
      newTran.SiteID = selected.SiteID ?? newTran.SiteID;
      newTran.InventoryID = selected.InventoryID;
      newTran.SubItemID = selected.SubItemID;
      newTran.UOM = selected.BaseUnit;
      newTran = PXCache<INTran>.CreateCopy(this.Transactions.Update(newTran));
      if (selected.LocationID.HasValue)
      {
        newTran.LocationID = selected.LocationID;
        newTran = PXCache<INTran>.CreateCopy(this.Transactions.Update(newTran));
      }
      return newTran;
    }

    [PXCustomizeBaseAttribute]
    protected virtual void _(PX.Data.Events.CacheAttached<INSiteStatusFilter.siteID> e)
    {
    }

    [PXMergeAttributes]
    [Location(typeof (INSiteStatusFilter.siteID))]
    protected virtual void _(
      PX.Data.Events.CacheAttached<INSiteStatusFilter.locationID> e)
    {
    }

    protected override void OnSelectedUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      bool? nullable1 = (bool?) sender.GetValue(e.Row, this.Selected);
      Decimal? nullable2 = (Decimal?) sender.GetValue(e.Row, this.QtySelected);
      Decimal? nullable3 = (Decimal?) sender.GetValue(e.Row, "qtyOnHand");
      if (nullable1.GetValueOrDefault())
      {
        Decimal? nullable4;
        if (nullable2.HasValue)
        {
          nullable4 = nullable2;
          Decimal num = 0M;
          if (!(nullable4.GetValueOrDefault() == num & nullable4.HasValue))
            return;
        }
        nullable4 = nullable3;
        Decimal num1 = 0M;
        if (!(nullable4.GetValueOrDefault() > num1 & nullable4.HasValue))
          return;
        sender.SetValue(e.Row, this.QtySelected, (object) nullable3);
      }
      else
        sender.SetValue(e.Row, this.QtySelected, (object) 0M);
    }

    public sealed class INTransferStatusFilter : PXCacheExtension<INSiteStatusFilter>
    {
      [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
      [PXUIField]
      [PXSelector(typeof (Search2<PX.Objects.PO.POReceipt.receiptNbr, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.PO.POReceipt.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>, Where<PX.Objects.PO.POReceipt.receiptType, Equal<POReceiptType.poreceipt>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>), Filterable = true)]
      public string ReceiptNbr { get; set; }

      public abstract class receiptNbr : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INTransferStatusFilter.receiptNbr>
      {
      }
    }

    [PXHidden]
    [INTransferSiteStatusProjection(Persistent = false)]
    public class INSiteStatusSelected : 
      PXBqlTable,
      IPXSelectable,
      IQtySelectable,
      IBqlTable,
      IBqlTableSystemDataStorage,
      IFTSSelectable
    {
      protected Decimal? _QtySelected;

      [PXBool]
      [PXUnboundDefault(false)]
      [PXUIField(DisplayName = "Selected")]
      public virtual bool? Selected { get; set; }

      [Inventory(BqlField = typeof (InventoryItem.inventoryID), IsKey = true)]
      [PXDefault]
      public virtual int? InventoryID { get; set; }

      [PXDefault]
      [InventoryRaw(BqlField = typeof (InventoryItem.inventoryCD))]
      public virtual string InventoryCD { get; set; }

      [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (InventoryItem.descr), IsProjection = true)]
      [PXUIField]
      public virtual string Descr { get; set; }

      [PXDBInt(BqlField = typeof (InventoryItem.itemClassID))]
      [PXUIField(DisplayName = "Item Class ID", Visible = false)]
      [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassID), typeof (INItemClass.itemClassCD), ValidComboRequired = true)]
      public virtual int? ItemClassID { get; set; }

      [PXDBString(30, IsUnicode = true, BqlField = typeof (INItemClass.itemClassCD))]
      public virtual string ItemClassCD { get; set; }

      [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INItemClass.descr), IsProjection = true)]
      [PXUIField(DisplayName = "Item Class Description", Visible = false)]
      public virtual string ItemClassDescription { get; set; }

      [PXDBString(10, IsUnicode = true, BqlField = typeof (InventoryItem.priceClassID))]
      [PXUIField(DisplayName = "Price Class ID", Visible = false)]
      public virtual string PriceClassID { get; set; }

      [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INPriceClass.description))]
      [PXUIField(DisplayName = "Price Class Description", Visible = false)]
      public virtual string PriceClassDescription { get; set; }

      [PXDBString(255 /*0xFF*/, BqlField = typeof (INItemXRef.alternateID), IsUnicode = true)]
      public virtual string BarCode { get; set; }

      [PXUIField(DisplayName = "Warehouse")]
      [Site(BqlField = typeof (INLocationStatusByCostCenter.siteID), IsKey = true)]
      public virtual int? SiteID { get; set; }

      [PXDBString(IsUnicode = true, BqlField = typeof (INSite.siteCD))]
      [PXDimension("INSITE")]
      public virtual string SiteCD { get; set; }

      [Location(typeof (INTransferEntry.SiteStatusLookup.INSiteStatusSelected.siteID), BqlField = typeof (INLocationStatusByCostCenter.locationID), IsKey = true)]
      [PXDefault]
      public virtual int? LocationID { get; set; }

      [PXDBString(BqlField = typeof (INLocation.locationCD), IsUnicode = true)]
      [PXDimension("INLOCATION")]
      [PXDefault]
      public virtual string LocationCD { get; set; }

      [SubItem(typeof (INTransferEntry.SiteStatusLookup.INSiteStatusSelected.inventoryID), BqlField = typeof (INSubItem.subItemID), IsKey = true)]
      public virtual int? SubItemID { get; set; }

      [PXDBString(IsUnicode = true, BqlField = typeof (INSubItem.subItemCD))]
      [PXDimension("INSUBITEM")]
      public virtual string SubItemCD { get; set; }

      [PXDefault(typeof (Search<INItemClass.baseUnit, Where<INItemClass.itemClassID, Equal<Current<InventoryItem.itemClassID>>>>))]
      [INUnit]
      public virtual string BaseUnit { get; set; }

      [PXQuantity]
      [PXUnboundDefault(TypeCode.Decimal, "0.0")]
      [PXUIField(DisplayName = "Qty. Selected")]
      public virtual Decimal? QtySelected
      {
        get => new Decimal?(this._QtySelected.GetValueOrDefault());
        set
        {
          if (value.HasValue)
          {
            Decimal? nullable = value;
            Decimal num = 0M;
            if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
              this.Selected = new bool?(true);
          }
          this._QtySelected = value;
        }
      }

      [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyOnHand))]
      [PXDefault(TypeCode.Decimal, "0.0")]
      [PXUIField(DisplayName = "Qty. On Hand")]
      public virtual Decimal? QtyOnHand { get; set; }

      [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyAvail))]
      [PXDefault(TypeCode.Decimal, "0.0")]
      [PXUIField(DisplayName = "Qty. Available")]
      public virtual Decimal? QtyAvail { get; set; }

      [PXNote(BqlField = typeof (InventoryItem.noteID))]
      public virtual Guid? NoteID { get; set; }

      /// <exclude />
      [DBRankTop(BqlField = typeof (InventorySearchIndexAlternateIDTop.rank))]
      public virtual int? Rank { get; set; }

      /// <exclude />
      [PXString]
      [DBCombinedSearchString(typeof (InventorySearchIndexAlternateIDTop.contentIDDesc), BqlTable = typeof (INTransferEntry.SiteStatusLookup.INSiteStatusSelected))]
      public virtual string CombinedSearchString { get; set; }

      public abstract class selected : 
        BqlType<
        #nullable enable
        IBqlBool, bool>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.selected>
      {
      }

      public abstract class inventoryID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.inventoryID>
      {
      }

      public abstract class inventoryCD : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.inventoryCD>
      {
      }

      public abstract class descr : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.descr>
      {
      }

      public abstract class itemClassID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.itemClassID>
      {
      }

      public abstract class itemClassCD : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.itemClassCD>
      {
      }

      public abstract class itemClassDescription : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.itemClassDescription>
      {
      }

      public abstract class priceClassID : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.priceClassID>
      {
      }

      public abstract class priceClassDescription : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.priceClassDescription>
      {
      }

      public abstract class barCode : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.barCode>
      {
      }

      public abstract class siteID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.siteID>
      {
      }

      public abstract class siteCD : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.siteCD>
      {
      }

      public abstract class locationID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.locationID>
      {
      }

      public abstract class locationCD : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.locationCD>
      {
      }

      public abstract class subItemID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.subItemID>
      {
      }

      public abstract class subItemCD : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.subItemCD>
      {
      }

      public abstract class baseUnit : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.baseUnit>
      {
      }

      public abstract class qtySelected : 
        BqlType<
        #nullable enable
        IBqlDecimal, Decimal>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.qtySelected>
      {
      }

      public abstract class qtyOnHand : 
        BqlType<
        #nullable enable
        IBqlDecimal, Decimal>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.qtyOnHand>
      {
      }

      public abstract class qtyAvail : 
        BqlType<
        #nullable enable
        IBqlDecimal, Decimal>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.qtyAvail>
      {
      }

      public abstract class noteID : 
        BqlType<
        #nullable enable
        IBqlGuid, Guid>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.noteID>
      {
      }

      public abstract class rank : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.rank>
      {
      }

      public abstract class combinedSearchString : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        INTransferEntry.SiteStatusLookup.INSiteStatusSelected.combinedSearchString>
      {
      }
    }
  }

  public class LineSplittingExtension : INRegisterLineSplittingExtension<INTransferEntry>
  {
  }

  public class ItemAvailabilityExtension : INRegisterItemAvailabilityExtension<INTransferEntry>
  {
  }

  public class INOpenPeriodTransferAttribute : INOpenPeriodAttribute
  {
    public INOpenPeriodTransferAttribute()
      : base(typeof (INRegister.tranDate), typeof (AccessInfo.branchID), selectionModeWithRestrictions: FinPeriodSelectorAttribute.SelectionModesWithRestrictions.All, masterFinPeriodIDType: typeof (INRegister.tranPeriodID))
    {
      if (!PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>() || !PXAccess.FeatureInstalled<FeaturesSet.branch>())
        return;
      this.PeriodKeyProvider = (IPeriodKeyProvider<OrganizationDependedPeriodKey, PeriodKeyProviderBase.SourcesSpecificationCollectionBase>) (this.CalendarOrganizationIDProvider = (ICalendarOrganizationIDProvider) new INTransferEntry.INOpenPeriodTransferAttribute.InTransferCalendarOrganizationIDProvider());
    }

    protected class InTransferCalendarOrganizationIDProvider : CalendarOrganizationIDProvider
    {
      protected PeriodKeyProviderBase.SourceSpecificationItem SiteSpecification { get; set; }

      protected PeriodKeyProviderBase.SourceSpecificationItem ToSiteSpecification { get; set; }

      public InTransferCalendarOrganizationIDProvider()
        : base()
      {
        this.SiteSpecification = new PeriodKeyProviderBase.SourceSpecificationItem()
        {
          BranchSourceType = typeof (INRegister.siteID),
          BranchSourceFormulaType = typeof (Selector<INRegister.siteID, INSite.branchID>),
          IsMain = true
        }.Initialize();
        this.ToSiteSpecification = new PeriodKeyProviderBase.SourceSpecificationItem()
        {
          BranchSourceType = typeof (INRegister.toSiteID),
          BranchSourceFormulaType = typeof (Selector<INRegister.toSiteID, INSite.branchID>)
        }.Initialize();
      }

      public override PeriodKeyProviderBase.SourcesSpecificationCollection GetSourcesSpecification(
        PXCache cache,
        object row)
      {
        PeriodKeyProviderBase.SourcesSpecificationCollection sourcesSpecification = new PeriodKeyProviderBase.SourcesSpecificationCollection();
        sourcesSpecification.SpecificationItems.Add(this.SiteSpecification);
        sourcesSpecification.MainSpecificationItem = this.SiteSpecification;
        INRegister inRegister = (INRegister) row;
        if (inRegister == null || inRegister.TransferType == "1")
          sourcesSpecification.SpecificationItems.Add(this.ToSiteSpecification);
        sourcesSpecification.DependsOnFields.Add(typeof (INRegister.transferType));
        return sourcesSpecification;
      }
    }
  }
}
