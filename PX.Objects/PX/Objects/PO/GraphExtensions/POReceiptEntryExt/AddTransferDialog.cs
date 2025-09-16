// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.AddTransferDialog
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

#nullable enable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class AddTransferDialog : PXGraphExtension<
#nullable disable
POReceiptEntry>
{
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<PX.Objects.SO.SOOrderShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOOrderShipment.FK.Order>>, FbqlJoins.Inner<INTransferInTransitSO>.On<BqlOperand<
  #nullable enable
  INTransferInTransitSO.sOShipmentNbr, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOOrderShipment.shipmentNbr>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INTransferInTransitSO.origModule, 
  #nullable disable
  Equal<BatchModule.moduleSO>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.invtDocType, IBqlString>.IsEqual<
  #nullable disable
  INDocType.transfer>>>, PX.Objects.SO.SOOrderShipment>.View openTransfers;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<PX.Objects.IN.INRegister, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.IN.INRegister>.View dummyOpenTransfers;
  public FbqlSelect<SelectFromBase<INCostSubItemXRef, TypeArrayOf<IFbqlJoin>.Empty>, INCostSubItemXRef>.View costsubitemxref;
  public PXAction<PX.Objects.PO.POReceipt> addTransfer;
  public PXAction<PX.Objects.PO.POReceipt> addTransfer2;
  private Lazy<AddTransferDialog.SubItemInfo> _subItemInfo;

  public virtual void Initialize()
  {
    this._subItemInfo = new Lazy<AddTransferDialog.SubItemInfo>((Func<AddTransferDialog.SubItemInfo>) (() => new AddTransferDialog.SubItemInfo((PXGraph) this.Base)));
    ((PXSelectBase) this.openTransfers).Cache.AllowInsert = false;
    ((PXSelectBase) this.openTransfers).Cache.AllowDelete = false;
    ((PXSelectBase) this.openTransfers).Cache.AllowUpdate = true;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.AdjustUIReadonly(((PXSelectBase) this.openTransfers).Cache, (object) null);
    attributeAdjuster = attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
    attributeAdjuster.For<PX.Objects.SO.SOOrderShipment.selected>((Action<PXUIFieldAttribute>) (a => a.Enabled = true)).For<PX.Objects.SO.SOOrderShipment.orderType>((Action<PXUIFieldAttribute>) (a => a.Visible = true)).SameFor<PX.Objects.SO.SOOrderShipment.orderNbr>().SameFor<PX.Objects.SO.SOOrderShipment.shipmentNbr>();
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (SiteAttribute), "DisplayName", "From Warehouse")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.INRegister.siteID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (ToSiteAttribute), "DisplayName", "To Warehouse")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.INRegister.toSiteID> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXMergeAttributes]
  [PXDefault(typeof (PX.Objects.PO.POReceipt.receiptType))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POReceiptToShipmentLink.receiptType> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXMergeAttributes]
  [PXDBDefault(typeof (PX.Objects.PO.POReceipt.receiptNbr))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POReceiptToShipmentLink.receiptNbr> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.addTransfer).SetVisible(e.Row.ReceiptType == "RX");
    ((PXAction) this.addTransfer).SetEnabled(e.Row.ReceiptType == "RX" && e.Row.SiteID.HasValue);
  }

  public IEnumerable OpenTransfers()
  {
    AddTransferDialog addTransferDialog = this;
    if (!(((PXSelectBase<PX.Objects.PO.POReceipt>) addTransferDialog.Base.Document).Current?.ReceiptType != "RX"))
    {
      Dictionary<PX.Objects.PO.POReceiptLine, int> usedTransfer = new Dictionary<PX.Objects.PO.POReceiptLine, int>((IEqualityComparer<PX.Objects.PO.POReceiptLine>) new AddTransferDialog.SOOrderShipmentComparer());
      int num1;
      foreach (PX.Objects.PO.POReceiptLine key in ((PXSelectBase) addTransferDialog.Base.transactions).Cache.Inserted)
      {
        if (key.OrigRefNbr != null)
        {
          usedTransfer.TryGetValue(key, out num1);
          usedTransfer[key] = num1 + 1;
        }
      }
      foreach (PX.Objects.PO.POReceiptLine key in ((PXSelectBase) addTransferDialog.Base.transactions).Cache.Deleted)
      {
        if (key.OrigRefNbr != null && ((PXSelectBase) addTransferDialog.Base.transactions).Cache.GetStatus((object) key) != 4)
        {
          usedTransfer.TryGetValue(key, out num1);
          usedTransfer[key] = num1 - 1;
        }
      }
      BqlCommand selectSites = (BqlCommand) new SelectFromBase<PX.Objects.IN.INSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<MatchWithBranch<PX.Objects.IN.INSite.branchID>>();
      PXResultset<PX.Objects.SO.SOOrderShipment> pxResultset;
      using (new PXReadBranchRestrictedScope())
        pxResultset = PXSelectBase<PX.Objects.SO.SOOrderShipment, PXViewOf<PX.Objects.SO.SOOrderShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrderShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTransitLineStatus>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTransitLineStatus.sOShipmentNbr, Equal<PX.Objects.SO.SOOrderShipment.shipmentNbr>>>>, And<BqlOperand<INTransitLineStatus.sOOrderType, IBqlString>.IsEqual<PX.Objects.SO.SOOrderShipment.orderType>>>>.And<BqlOperand<INTransitLineStatus.sOOrderNbr, IBqlString>.IsEqual<PX.Objects.SO.SOOrderShipment.orderNbr>>>>, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<BqlOperand<PX.Objects.IN.INSite.siteID, IBqlInt>.IsEqual<INTransitLineStatus.toSiteID>>>, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOOrderShipment.FK.Order>>, FbqlJoins.Left<PX.Objects.PO.POReceiptLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.origDocType, Equal<INDocType.transfer>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.origRefNbr, IBqlString>.IsEqual<INTransitLineStatus.transferNbr>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.origLineNbr, IBqlInt>.IsEqual<INTransitLineStatus.transferLineNbr>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.released, NotEqual<True>>>>>.Or<BqlOperand<PX.Objects.PO.POReceiptLine.iNReleased, IBqlBool>.IsNotEqual<True>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptNbr, IsNull>>>, And<BqlOperand<INTransitLineStatus.qtyOnHand, IBqlDecimal>.IsGreater<decimal0>>>, And<BqlOperand<INTransitLineStatus.origModule, IBqlString>.IsEqual<BatchModule.moduleSO>>>, And<BqlOperand<PX.Objects.SO.SOOrderShipment.invtDocType, IBqlString>.IsEqual<INDocType.transfer>>>, And<BqlOperand<PX.Objects.SO.SOOrderShipment.shipmentType, IBqlString>.IsEqual<INDocType.transfer>>>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>, And<BqlOperand<INTransitLineStatus.toSiteID, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceipt.siteID, IBqlInt>.FromCurrent>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<POReceiptEntry.POOrderFilter.shipFromSiteID>, IsNull>>>>.Or<BqlOperand<INTransitLineStatus.fromSiteID, IBqlInt>.IsEqual<BqlField<POReceiptEntry.POOrderFilter.shipFromSiteID, IBqlInt>.FromCurrent>>>>.Aggregate<To<GroupBy<PX.Objects.SO.SOOrderShipment.shipmentNbr>, GroupBy<PX.Objects.SO.SOOrderShipment.orderNbr>, GroupBy<PX.Objects.SO.SOOrderShipment.orderType>, Count>>>.Config>.Select((PXGraph) addTransferDialog.Base, Array.Empty<object>());
      PXCache siteCache = (PXCache) GraphHelper.Caches<PX.Objects.IN.INSite>((PXGraph) addTransferDialog.Base);
      foreach (PXResult<PX.Objects.SO.SOOrderShipment, INTransitLineStatus, PX.Objects.IN.INSite, PX.Objects.SO.SOOrder> pxResult in pxResultset)
      {
        PX.Objects.SO.SOOrderShipment soOrderShipment1;
        INTransitLineStatus transitLineStatus1;
        PX.Objects.IN.INSite inSite1;
        PX.Objects.SO.SOOrder soOrder1;
        pxResult.Deconstruct(ref soOrderShipment1, ref transitLineStatus1, ref inSite1, ref soOrder1);
        PX.Objects.SO.SOOrderShipment soOrderShipment2 = soOrderShipment1;
        INTransitLineStatus transitLineStatus2 = transitLineStatus1;
        PX.Objects.IN.INSite inSite2 = inSite1;
        PX.Objects.SO.SOOrder soOrder2 = soOrder1;
        if (selectSites.Meet(siteCache, (object) inSite2, Array.Empty<object>()))
        {
          INTransferInTransitSO transferInTransitSo1 = new INTransferInTransitSO();
          int? nullable1;
          int? nullable2;
          if (transitLineStatus2 == null)
          {
            nullable1 = new int?();
            nullable2 = nullable1;
          }
          else
            nullable2 = transitLineStatus2.FromSiteID;
          transferInTransitSo1.FromSiteID = nullable2;
          int? nullable3;
          if (transitLineStatus2 == null)
          {
            nullable1 = new int?();
            nullable3 = nullable1;
          }
          else
            nullable3 = transitLineStatus2.ToSiteID;
          transferInTransitSo1.ToSiteID = nullable3;
          transferInTransitSo1.SOOrderType = transitLineStatus2.SOOrderNbr;
          transferInTransitSo1.SOOrderNbr = transitLineStatus2.SOOrderType;
          transferInTransitSo1.SOShipmentNbr = transitLineStatus2.SOShipmentNbr;
          transferInTransitSo1.TranDate = transitLineStatus2.TranDate;
          INTransferInTransitSO transferInTransitSo2 = transferInTransitSo1;
          PX.Objects.PO.POReceiptLine key = new PX.Objects.PO.POReceiptLine()
          {
            SOOrderType = soOrderShipment2.OrderType,
            SOOrderNbr = soOrderShipment2.OrderNbr,
            SOShipmentNbr = soOrderShipment2.ShipmentNbr
          };
          if (usedTransfer.TryGetValue(key, out num1))
          {
            usedTransfer.Remove(key);
            int num2 = num1;
            nullable1 = ((PXResult) pxResult).RowCount;
            int valueOrDefault = nullable1.GetValueOrDefault();
            if (num2 < valueOrDefault & nullable1.HasValue)
              yield return (object) new PXResult<PX.Objects.SO.SOOrderShipment, INTransferInTransitSO, PX.Objects.SO.SOOrder>(soOrderShipment2, transferInTransitSo2, soOrder2);
          }
          else
            yield return (object) new PXResult<PX.Objects.SO.SOOrderShipment, INTransferInTransitSO, PX.Objects.SO.SOOrder>(soOrderShipment2, transferInTransitSo2, soOrder2);
        }
      }
      foreach (PX.Objects.PO.POReceiptLine poReceiptLine in usedTransfer.Where<KeyValuePair<PX.Objects.PO.POReceiptLine, int>>((Func<KeyValuePair<PX.Objects.PO.POReceiptLine, int>, bool>) (_ => _.Value < 0)).Select<KeyValuePair<PX.Objects.PO.POReceiptLine, int>, PX.Objects.PO.POReceiptLine>((Func<KeyValuePair<PX.Objects.PO.POReceiptLine, int>, PX.Objects.PO.POReceiptLine>) (_ => _.Key)))
        yield return (object) (PXResult<PX.Objects.SO.SOOrderShipment, INTransferInTransitSO, PX.Objects.SO.SOOrder>) PXResultset<PX.Objects.SO.SOOrderShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrderShipment, PXViewOf<PX.Objects.SO.SOOrderShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrderShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTransferInTransitSO>.On<BqlOperand<INTransferInTransitSO.sOShipmentNbr, IBqlString>.IsEqual<PX.Objects.SO.SOOrderShipment.shipmentNbr>>>, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOOrderShipment.FK.Order>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrderShipment.invtRefNbr, Equal<P.AsString>>>>, And<BqlOperand<PX.Objects.SO.SOOrderShipment.invtDocType, IBqlString>.IsEqual<INDocType.transfer>>>, And<BqlOperand<PX.Objects.SO.SOOrderShipment.orderNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.SO.SOOrderShipment.orderType, IBqlString>.IsEqual<P.AsString.Fixed.ASCII>>>>.Config>.Select((PXGraph) addTransferDialog.Base, new object[3]
        {
          (object) poReceiptLine.OrigRefNbr,
          (object) poReceiptLine.SOOrderNbr,
          (object) poReceiptLine.SOOrderType
        }));
    }
  }

  [PXLookupButton]
  [PXUIField]
  public virtual IEnumerable AddTransfer(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current.Released;
      if (!nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<POReceiptEntry.POOrderFilter>) this.Base.filter).Current.ResetFilter;
        if (nullable.GetValueOrDefault())
        {
          ((PXSelectBase) this.Base.filter).Cache.Remove((object) ((PXSelectBase<POReceiptEntry.POOrderFilter>) this.Base.filter).Current);
          ((PXSelectBase) this.Base.filter).Cache.Insert((object) new POReceiptEntry.POOrderFilter());
        }
        else
          ((PXSelectBase) this.Base.filter).Cache.RaiseRowSelected((object) ((PXSelectBase<POReceiptEntry.POOrderFilter>) this.Base.filter).Current);
        if (((PXSelectBase<PX.Objects.SO.SOOrderShipment>) this.openTransfers).AskExt(true) == 1)
          return this.AddTransfer2(adapter);
      }
    }
    ((PXSelectBase) this.openTransfers).Cache.Clear();
    return adapter.Get();
  }

  [PXLookupButton]
  [PXUIField]
  public virtual IEnumerable AddTransfer2(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current.Released;
      if (!nullable.GetValueOrDefault())
      {
        bool flag = false;
        foreach (PX.Objects.SO.SOOrderShipment link in ((PXSelectBase) this.openTransfers).Cache.Updated)
        {
          nullable = link.Selected;
          if (nullable.GetValueOrDefault())
          {
            using (new PXReadBranchRestrictedScope())
              this.AddTransferDoc(link);
            link.Selected = new bool?(false);
          }
        }
        if (flag)
          throw new PXException("Selected Purchase Orders cannot be added. Selected Orders must have same Currency, Type and Shipping Destinations.");
      }
    }
    return adapter.Get();
  }

  public virtual void AddTransferDoc(PX.Objects.SO.SOOrderShipment link)
  {
    this.AddTransferDoc(link, new (int, int)?());
  }

  public virtual void AddTransferDoc(PX.Objects.SO.SOOrderShipment link, (int ID, int SubID)? itemKey)
  {
    this.AddTransferDoc(new (string, string)?(link.With<PX.Objects.SO.SOOrderShipment, (string, string)>((Func<PX.Objects.SO.SOOrderShipment, (string, string)>) (o => (o.ShipmentType, o.ShipmentNbr)))), new (string, string)?(link.With<PX.Objects.SO.SOOrderShipment, (string, string)>((Func<PX.Objects.SO.SOOrderShipment, (string, string)>) (o => (o.OrderType, o.OrderNbr)))), new (string, string)?(link.With<PX.Objects.SO.SOOrderShipment, (string, string)>((Func<PX.Objects.SO.SOOrderShipment, (string, string)>) (o => (o.InvtDocType, o.InvtRefNbr)))), itemKey);
  }

  public virtual bool AddTransferDoc(
    (string Type, string Nbr)? shipmentKey,
    (string Type, string Nbr)? orderKey,
    (string Type, string Nbr)? transferKey,
    (int ID, int SubID)? itemKey)
  {
    if (!shipmentKey.HasValue && !orderKey.HasValue)
      throw new InvalidOperationException();
    bool flag = false;
    using (this.Base.GetSkipUIUpdateScope())
    {
      List<object> objectList = new List<object>();
      PXSelectBase<INTran> pxSelectBase = (PXSelectBase<INTran>) new FbqlSelect<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTransitLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTransitLine.transferNbr, Equal<INTran.refNbr>>>>>.And<BqlOperand<INTransitLine.transferLineNbr, IBqlInt>.IsEqual<INTran.lineNbr>>>>, FbqlJoins.Inner<INTransitLineStatus>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTransitLineStatus.transferNbr, Equal<INTransitLine.transferNbr>>>>>.And<BqlOperand<INTransitLineStatus.transferLineNbr, IBqlInt>.IsEqual<INTransitLine.transferLineNbr>>>>, FbqlJoins.Left<INLotSerialStatusInTransit>.On<BqlOperand<INLotSerialStatusInTransit.locationID, IBqlInt>.IsEqual<INTransitLine.costSiteID>>>, FbqlJoins.Inner<INLocationStatusInTransit>.On<BqlOperand<INLocationStatusInTransit.locationID, IBqlInt>.IsEqual<INTransitLine.costSiteID>>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INLocationStatusInTransit.FK.InventoryItem>>, FbqlJoins.Left<PX.Objects.PO.POReceiptLine>.On<KeysRelation<CompositeKey<Field<PX.Objects.PO.POReceiptLine.origDocType>.IsRelatedTo<INTran.docType>, Field<PX.Objects.PO.POReceiptLine.origRefNbr>.IsRelatedTo<INTran.refNbr>, Field<PX.Objects.PO.POReceiptLine.origLineNbr>.IsRelatedTo<INTran.lineNbr>>.WithTablesOf<INTran, PX.Objects.PO.POReceiptLine>, INTran, PX.Objects.PO.POReceiptLine>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.released, NotEqual<True>>>>>.Or<BqlOperand<PX.Objects.PO.POReceiptLine.iNReleased, IBqlBool>.IsNotEqual<True>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptNbr, IsNull>>>>.And<BqlOperand<INTransitLineStatus.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>>.Order<By<BqlField<INTransitLine.transferNbr, IBqlString>.Asc, BqlField<INTransitLine.transferLineNbr, IBqlInt>.Asc>>, INTran>.View((PXGraph) this.Base);
      if (shipmentKey.HasValue)
      {
        (string Type, string Nbr) valueOrDefault = shipmentKey.GetValueOrDefault();
        objectList.Add((object) valueOrDefault.Type);
        objectList.Add((object) valueOrDefault.Nbr);
        pxSelectBase.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.sOShipmentType, Equal<P.AsString.Fixed.ASCII>>>>>.And<BqlOperand<INTran.sOShipmentNbr, IBqlString>.IsEqual<P.AsString>>>>();
      }
      if (orderKey.HasValue)
      {
        (string Type, string Nbr) valueOrDefault = orderKey.GetValueOrDefault();
        objectList.Add((object) valueOrDefault.Type);
        objectList.Add((object) valueOrDefault.Nbr);
        pxSelectBase.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.sOOrderType, Equal<P.AsString.Fixed.ASCII>>>>>.And<BqlOperand<INTran.sOOrderNbr, IBqlString>.IsEqual<P.AsString>>>>();
      }
      if (transferKey.HasValue)
      {
        (string Type, string Nbr) valueOrDefault = transferKey.GetValueOrDefault();
        objectList.Add((object) valueOrDefault.Type);
        objectList.Add((object) valueOrDefault.Nbr);
        pxSelectBase.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.docType, Equal<P.AsString.Fixed.ASCII>>>>>.And<BqlOperand<INTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>();
      }
      if (itemKey.HasValue)
      {
        (int ID, int SubID) valueOrDefault = itemKey.GetValueOrDefault();
        objectList.Add((object) valueOrDefault.ID);
        objectList.Add((object) valueOrDefault.SubID);
        pxSelectBase.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.inventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<INTran.subItemID, IBqlInt>.IsEqual<P.AsInt>>>>();
      }
      foreach (IGrouping<INTran, PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem, INTransitLineStatus>> grouping in pxSelectBase.Select(objectList.ToArray()).Cast<PXResult<INTran, INTransitLine, INTransitLineStatus, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>>().ToLookup<PXResult<INTran, INTransitLine, INTransitLineStatus, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>, INTran, PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem, INTransitLineStatus>>((Func<PXResult<INTran, INTransitLine, INTransitLineStatus, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>, INTran>) (t => ((PXResult) t).GetItem<INTran>()), (Func<PXResult<INTran, INTransitLine, INTransitLineStatus, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>, PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem, INTransitLineStatus>>) (t => new PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem, INTransitLineStatus>(PXResult<INTran, INTransitLine, INTransitLineStatus, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>.op_Implicit(t), PXResult<INTran, INTransitLine, INTransitLineStatus, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>.op_Implicit(t), PXResult<INTran, INTransitLine, INTransitLineStatus, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>.op_Implicit(t), PXResult<INTran, INTransitLine, INTransitLineStatus, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>.op_Implicit(t), PXResult<INTran, INTransitLine, INTransitLineStatus, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>.op_Implicit(t))), PXCacheEx.GetComparer<INTran>(GraphHelper.Caches<INTran>((PXGraph) this.Base))).Where<IGrouping<INTran, PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem, INTransitLineStatus>>>((Func<IGrouping<INTran, PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem, INTransitLineStatus>>, bool>) (r => r.Any<PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem, INTransitLineStatus>>())))
      {
        StoreTranForOptimization(grouping.Key);
        StoreStatusForOptimization(PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem, INTransitLineStatus>.op_Implicit(grouping.First<PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem, INTransitLineStatus>>()));
        flag |= this.AddTransferLine(grouping.Key, (IEnumerable<PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>>) grouping) != null;
      }
      return flag;
    }

    void StoreStatusForOptimization(INTransitLineStatus status)
    {
      PXSelectBase<INTransitLineStatus, PXViewOf<INTransitLineStatus>.BasedOn<SelectFromBase<INTransitLineStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTransitLineStatus.transferNbr, Equal<BqlField<PX.Objects.PO.POReceiptLine.origRefNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<INTransitLineStatus.transferLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origLineNbr, IBqlInt>.FromCurrent>>>>.Config>.StoreResult((PXGraph) this.Base, (IBqlTable) status);
    }

    void StoreTranForOptimization(INTran tran)
    {
      PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.tranType, Equal<INTranType.transfer>>>>, And<BqlOperand<INTran.refNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origRefNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<INTran.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origLineNbr, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INTran.docType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origDocType, IBqlString>.FromCurrent>>>>.Config>.StoreResult((PXGraph) this.Base, (IBqlTable) tran);
    }
  }

  public virtual PX.Objects.PO.POReceiptLine AddTransferLine(INTran inTran)
  {
    IEnumerable<PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>> transitLines = (IEnumerable<PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>>) PXSelectBase<INTransitLine, PXViewOf<INTransitLine>.BasedOn<SelectFromBase<INTransitLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INLotSerialStatusInTransit>.On<BqlOperand<INLotSerialStatusInTransit.locationID, IBqlInt>.IsEqual<INTransitLine.costSiteID>>>, FbqlJoins.Inner<INLocationStatusInTransit>.On<BqlOperand<INLocationStatusInTransit.locationID, IBqlInt>.IsEqual<INTransitLine.costSiteID>>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<INLocationStatusInTransit.inventoryID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTransitLine.transferNbr, Equal<P.AsString>>>>>.And<BqlOperand<INTransitLine.transferLineNbr, IBqlInt>.IsEqual<P.AsInt>>>.Order<By<BqlField<INTransitLine.transferNbr, IBqlString>.Asc, BqlField<INTransitLine.transferLineNbr, IBqlInt>.Asc>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) inTran.RefNbr,
      (object) inTran.LineNbr
    }).Cast<PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>>();
    return this.AddTransferLine(inTran, transitLines);
  }

  protected virtual PX.Objects.PO.POReceiptLine AddTransferLine(
    INTran inTran,
    IEnumerable<PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem>> transitLines)
  {
    PX.Objects.PO.POReceipt current = ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current;
    if (current == null || current.ReceiptType != "RX")
      return (PX.Objects.PO.POReceiptLine) null;
    if (((IEnumerable<PXResult<PX.Objects.PO.POReceiptLine>>) PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<P.AsString.Fixed.ASCII>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.origDocType, IBqlString>.IsEqual<P.AsString.Fixed.ASCII>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.origRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.origLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[5]
    {
      (object) current.ReceiptType,
      (object) current.ReceiptNbr,
      (object) inTran.DocType,
      (object) inTran.RefNbr,
      (object) inTran.LineNbr
    })).AsEnumerable<PXResult<PX.Objects.PO.POReceiptLine>>().Any<PXResult<PX.Objects.PO.POReceiptLine>>())
      return (PX.Objects.PO.POReceiptLine) null;
    PX.Objects.PO.POReceiptLine poReceiptLine = (PX.Objects.PO.POReceiptLine) null;
    Decimal newLineBaseQty = 0M;
    Decimal newLineCost = 0M;
    int? nullable1 = new int?();
    INLocationStatusInTransit locationStatusInTransit1 = (INLocationStatusInTransit) null;
    foreach (PXResult<INTransitLine, INLotSerialStatusInTransit, INLocationStatusInTransit, PX.Objects.IN.InventoryItem> transitLine in transitLines)
    {
      INTransitLine inTransitLine1;
      INLotSerialStatusInTransit serialStatusInTransit1;
      INLocationStatusInTransit locationStatusInTransit2;
      PX.Objects.IN.InventoryItem inventoryItem1;
      transitLine.Deconstruct(ref inTransitLine1, ref serialStatusInTransit1, ref locationStatusInTransit2, ref inventoryItem1);
      INTransitLine inTransitLine2 = inTransitLine1;
      INLotSerialStatusInTransit serialStatusInTransit2 = serialStatusInTransit1;
      INLocationStatusInTransit locationStatusInTransit3 = locationStatusInTransit2;
      PX.Objects.IN.InventoryItem inventoryItem2 = inventoryItem1;
      Decimal? nullable2 = locationStatusInTransit3.QtyOnHand;
      Decimal num1 = 0M;
      if (!(nullable2.GetValueOrDefault() == num1 & nullable2.HasValue))
      {
        if (serialStatusInTransit2 != null)
        {
          nullable2 = serialStatusInTransit2.QtyOnHand;
          Decimal num2 = 0M;
          if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
            continue;
        }
        int? nullable3 = nullable1;
        int? transferLineNbr = inTransitLine2.TransferLineNbr;
        if (!(nullable3.GetValueOrDefault() == transferLineNbr.GetValueOrDefault() & nullable3.HasValue == transferLineNbr.HasValue))
        {
          this.UpdateTranCostQty(poReceiptLine, newLineBaseQty, newLineCost);
          newLineBaseQty = 0M;
          poReceiptLine = PXCache<PX.Objects.PO.POReceiptLine>.CreateCopy(((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Base.transactions).Insert(new PX.Objects.PO.POReceiptLine()));
          this.Copy(poReceiptLine, inTran);
          poReceiptLine.InvtMult = new short?((short) 1);
          poReceiptLine.OrigPlanType = inTransitLine2.IsFixedInTransit.GetValueOrDefault() ? "44" : "42";
          poReceiptLine.OrigIsFixedInTransit = inTransitLine2.IsFixedInTransit;
          poReceiptLine.OrigIsLotSerial = inTransitLine2.IsLotSerial;
          poReceiptLine.OrigToLocationID = inTransitLine2.ToLocationID;
          poReceiptLine.OrigNoteID = inTransitLine2.NoteID;
          ((PXSelectBase<POReceiptLineSplit>) this.Base.splits).Current = (POReceiptLineSplit) null;
          poReceiptLine.ManualPrice = new bool?(true);
          poReceiptLine = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Base.transactions).Update(poReceiptLine);
          if (((PXSelectBase<POReceiptLineSplit>) this.Base.splits).Current != null)
            ((PXSelectBase<POReceiptLineSplit>) this.Base.splits).Delete(((PXSelectBase<POReceiptLineSplit>) this.Base.splits).Current);
          nullable1 = inTransitLine2.TransferLineNbr;
        }
        if (!GraphHelper.Caches<INLocationStatusInTransit>((PXGraph) this.Base).ObjectsEqual(locationStatusInTransit1, locationStatusInTransit3))
        {
          Decimal num3 = newLineBaseQty;
          nullable2 = locationStatusInTransit3.QtyOnHand;
          Decimal num4 = nullable2.Value;
          newLineBaseQty = num3 + num4;
          locationStatusInTransit1 = locationStatusInTransit3;
        }
        nullable2 = serialStatusInTransit2.QtyOnHand;
        POReceiptLineSplit split;
        Decimal num5;
        if (!nullable2.HasValue)
        {
          split = new POReceiptLineSplit()
          {
            InventoryID = locationStatusInTransit3.InventoryID,
            IsStockItem = new bool?(true),
            SubItemID = locationStatusInTransit3.SubItemID,
            LotSerialNbr = (string) null
          };
          nullable2 = locationStatusInTransit3.QtyOnHand;
          num5 = nullable2.Value;
        }
        else
        {
          split = new POReceiptLineSplit()
          {
            InventoryID = serialStatusInTransit2.InventoryID,
            IsStockItem = new bool?(true),
            SubItemID = serialStatusInTransit2.SubItemID,
            LotSerialNbr = serialStatusInTransit2.LotSerialNbr
          };
          nullable2 = serialStatusInTransit2.QtyOnHand;
          num5 = nullable2.Value;
        }
        int? costSubItemId = this.GetCostSubItemID(split, inventoryItem2);
        split.ReceiptType = poReceiptLine.ReceiptType;
        split.ReceiptNbr = poReceiptLine.ReceiptNbr;
        split.LineNbr = poReceiptLine.LineNbr;
        POReceiptLineSplit copy = PXCache<POReceiptLineSplit>.CreateCopy(((PXSelectBase<POReceiptLineSplit>) this.Base.splits).Insert(split));
        copy.InvtMult = new short?((short) 1);
        copy.SiteID = inTransitLine2.ToSiteID;
        copy.MaxTransferBaseQty = new Decimal?(num5);
        copy.BaseQty = new Decimal?(num5);
        copy.Qty = new Decimal?(num5);
        Decimal num6 = newLineCost;
        nullable2 = copy.BaseQty;
        Decimal num7 = nullable2.Value * this.GetTransitSplitUnitCost(poReceiptLine, copy, inventoryItem2, costSubItemId, inTran.RefNbr);
        newLineCost = num6 + num7;
        ((PXSelectBase<POReceiptLineSplit>) this.Base.splits).Update(copy);
      }
    }
    this.UpdateTranCostQty(poReceiptLine, newLineBaseQty, newLineCost);
    if (poReceiptLine != null)
      this.TryLinkTransferShipment(poReceiptLine);
    return poReceiptLine;
  }

  public virtual void Copy(PX.Objects.PO.POReceiptLine aDest, INTran aSrc)
  {
    aDest.POType = (string) null;
    aDest.PONbr = (string) null;
    aDest.POLineNbr = new int?();
    aDest.IsStockItem = aSrc.IsStockItem;
    aDest.InventoryID = aSrc.InventoryID;
    aDest.SubItemID = aSrc.SubItemID;
    aDest.SiteID = aSrc.ToSiteID;
    aDest.LocationID = aSrc.ToLocationID;
    aDest.LineType = "GI";
    aDest.UOM = aSrc.UOM;
    aDest.Qty = new Decimal?(0M);
    aDest.BaseQty = new Decimal?(0M);
    aDest.OrigDocType = aSrc.DocType;
    aDest.OrigTranType = aSrc.TranType;
    aDest.OrigRefNbr = aSrc.RefNbr;
    aDest.OrigLineNbr = aSrc.LineNbr;
    aDest.SOOrderType = aSrc.SOOrderType;
    aDest.SOOrderNbr = aSrc.SOOrderNbr;
    aDest.SOOrderLineNbr = aSrc.SOOrderLineNbr;
    aDest.SOShipmentType = aSrc.SOShipmentType;
    aDest.SOShipmentNbr = aSrc.SOShipmentNbr;
    aDest.ExpenseAcctID = new int?();
    aDest.ExpenseSubID = new int?();
    aDest.ReasonCode = (string) null;
    aDest.TranDesc = aSrc.TranDesc;
    aDest.CuryUnitCost = new Decimal?(0M);
    aDest.AllowComplete = new bool?(true);
    aDest.AllowOpen = new bool?(true);
    aDest.CostCodeID = aSrc.CostCodeID;
    aDest.AllowEditUnitCost = new bool?(false);
    if (aDest.LineType != "GI")
    {
      aDest.ProjectID = aSrc.ProjectID;
      aDest.TaskID = aSrc.TaskID;
    }
    aDest.IsSpecialOrder = aSrc.IsSpecialOrder;
    if (!(aSrc.DocType == "T"))
      return;
    aDest.CostCenterID = aSrc.ToCostCenterID;
  }

  public virtual POReceiptToShipmentLink TryLinkTransferShipment(PX.Objects.PO.POReceiptLine receiptLine)
  {
    return this.TryLinkTransferShipment(receiptLine.With<PX.Objects.PO.POReceiptLine, (string, string)>((Func<PX.Objects.PO.POReceiptLine, (string, string)>) (o => (o.SOShipmentType, o.SOShipmentNbr))), receiptLine.With<PX.Objects.PO.POReceiptLine, (string, string)>((Func<PX.Objects.PO.POReceiptLine, (string, string)>) (o => (o.SOOrderType, o.SOOrderNbr))));
  }

  public virtual POReceiptToShipmentLink TryLinkTransferShipment(PX.Objects.SO.SOOrderShipment soLink)
  {
    return this.TryLinkTransferShipment(soLink.With<PX.Objects.SO.SOOrderShipment, (string, string)>((Func<PX.Objects.SO.SOOrderShipment, (string, string)>) (o => (o.ShipmentType, o.ShipmentNbr))), soLink.With<PX.Objects.SO.SOOrderShipment, (string, string)>((Func<PX.Objects.SO.SOOrderShipment, (string, string)>) (o => (o.OrderType, o.OrderNbr))));
  }

  public virtual POReceiptToShipmentLink TryLinkTransferShipment(
    (string Type, string Nbr) shipmentKey,
    (string Type, string Nbr) orderKey)
  {
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current == null || ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current.Released.GetValueOrDefault() || GraphHelper.RowCast<POReceiptToShipmentLink>((IEnumerable) ((PXSelectBase<POReceiptToShipmentLink>) this.Base.RelatedShipments).Select(Array.Empty<object>())).Any<POReceiptToShipmentLink>((Func<POReceiptToShipmentLink, bool>) (l =>
    {
      string soShipmentType = l.SOShipmentType;
      string soShipmentNbr = l.SOShipmentNbr;
      (string Type, string Nbr) tuple1 = shipmentKey;
      string type1 = tuple1.Type;
      if (!(soShipmentType == type1) || !(soShipmentNbr == tuple1.Nbr))
        return false;
      string soOrderType = l.SOOrderType;
      string soOrderNbr = l.SOOrderNbr;
      (string Type, string Nbr) tuple2 = orderKey;
      string type2 = tuple2.Type;
      return soOrderType == type2 && soOrderNbr == tuple2.Nbr;
    })))
      return (POReceiptToShipmentLink) null;
    return ((PXSelectBase<POReceiptToShipmentLink>) this.Base.RelatedShipments).Insert(new POReceiptToShipmentLink()
    {
      SOShipmentType = shipmentKey.Type,
      SOShipmentNbr = shipmentKey.Nbr,
      SOOrderType = orderKey.Type,
      SOOrderNbr = orderKey.Nbr
    });
  }

  protected virtual void UpdateTranCostQty(
    PX.Objects.PO.POReceiptLine newLine,
    Decimal newLineBaseQty,
    Decimal newLineCost)
  {
    if (newLine == null)
      return;
    newLine.BaseQty = new Decimal?(newLineBaseQty);
    newLine.MaxTransferBaseQty = new Decimal?(newLineBaseQty);
    newLine.Qty = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) this.Base.transactions).Cache, newLine.InventoryID, newLine.UOM, newLine.BaseQty.Value, INPrecision.QUANTITY));
    newLine.CuryUnitCost = new Decimal?(PXDBPriceCostAttribute.Round(newLineCost / newLine.Qty.Value));
    string baseCuryId = ((PXSelectBase<Company>) new PXSetup<Company>((PXGraph) this.Base)).Current.BaseCuryID;
    ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Base.transactions).Update(newLine);
  }

  protected virtual Decimal GetTransitSplitUnitCost(
    PX.Objects.PO.POReceiptLine line,
    POReceiptLineSplit split,
    PX.Objects.IN.InventoryItem item,
    int? costSubItemId,
    string transferNbr)
  {
    Decimal? baseQty = split.BaseQty;
    Decimal num1 = 0M;
    if (baseQty.GetValueOrDefault() == num1 & baseQty.HasValue || !split.BaseQty.HasValue)
      return 0M;
    object[] parameters;
    INCostStatus inCostStatus = (INCostStatus) this.GetCostStatusCommand(line, split, item, transferNbr, costSubItemId, out parameters).SelectSingle(parameters);
    Decimal? nullable = inCostStatus.TotalCost;
    Decimal num2 = nullable.Value;
    nullable = inCostStatus.QtyOnHand;
    Decimal num3 = nullable.Value;
    return num2 / num3;
  }

  protected virtual PXView GetCostStatusCommand(
    PX.Objects.PO.POReceiptLine line,
    POReceiptLineSplit split,
    PX.Objects.IN.InventoryItem item,
    string transferNbr,
    int? costSubItemId,
    out object[] parameters)
  {
    int? commandCostSiteId = this.GetCostStatusCommandCostSiteID(line);
    BqlCommand bqlCommand;
    switch (item.ValMethod)
    {
      case "A":
      case "T":
      case "F":
        bqlCommand = (BqlCommand) new SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INCostStatus.costSiteID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INCostStatus.costSubItemID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INCostStatus.layerType, IBqlString>.IsEqual<INLayerType.normal>>>>.And<BqlOperand<INCostStatus.receiptNbr, IBqlString>.IsEqual<P.AsString>>>.OrderBy<BqlField<INCostStatus.receiptDate, IBqlDateTime>.Asc, BqlField<INCostStatus.receiptNbr, IBqlString>.Asc>();
        parameters = new object[4]
        {
          (object) split.InventoryID,
          (object) commandCostSiteId,
          (object) costSubItemId,
          (object) transferNbr
        };
        break;
      case "S":
        bqlCommand = (BqlCommand) new SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INCostStatus.costSiteID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INCostStatus.costSubItemID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INCostStatus.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<INCostStatus.layerType, IBqlString>.IsEqual<INLayerType.normal>>>>.And<BqlOperand<INCostStatus.receiptNbr, IBqlString>.IsEqual<P.AsString>>>();
        parameters = new object[5]
        {
          (object) split.InventoryID,
          (object) commandCostSiteId,
          (object) costSubItemId,
          (object) split.LotSerialNbr,
          (object) transferNbr
        };
        break;
      default:
        throw new PXException();
    }
    return new PXView((PXGraph) this.Base, false, bqlCommand);
  }

  protected virtual int? GetCostStatusCommandCostSiteID(PX.Objects.PO.POReceiptLine line)
  {
    return this.INTransitSiteID;
  }

  protected int? INTransitSiteID
  {
    get
    {
      return ((PXSelectBase<INSetup>) this.Base.insetup).Current.TransitSiteID ?? throw new PXSetupNotEnteredException<INSetup>();
    }
  }

  protected virtual int? GetCostSubItemID(POReceiptLineSplit split, PX.Objects.IN.InventoryItem item)
  {
    INCostSubItemXRef data = new INCostSubItemXRef()
    {
      SubItemID = split.SubItemID,
      CostSubItemID = split.SubItemID
    };
    string valueExt = (string) GetValueExt<INCostSubItemXRef.costSubItemID>(((PXSelectBase) this.costsubitemxref).Cache, (object) data);
    data.CostSubItemID = new int?();
    string str = PXAccess.FeatureInstalled<FeaturesSet.subItem>() ? this._subItemInfo.Value.MakeCostSubItemCD(valueExt) : valueExt;
    ((PXSelectBase) this.costsubitemxref).Cache.SetValueExt<INCostSubItemXRef.costSubItemID>((object) data, (object) str);
    INCostSubItemXRef inCostSubItemXref = ((PXSelectBase<INCostSubItemXRef>) this.costsubitemxref).Update(data);
    if (((PXSelectBase) this.costsubitemxref).Cache.GetStatus((object) inCostSubItemXref) == 1)
      ((PXSelectBase) this.costsubitemxref).Cache.SetStatus((object) inCostSubItemXref, (PXEntryStatus) 0);
    return inCostSubItemXref.CostSubItemID;

    static object GetValueExt<TField>(PXCache cache, object data) where TField : class, IBqlField
    {
      object valueExt = cache.GetValueExt<TField>(data);
      return !(valueExt is PXFieldState pxFieldState) ? valueExt : pxFieldState.Value;
    }
  }

  [PXOverride]
  public int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    object[] parameters,
    Func<string, IDictionary, IDictionary, object[], int> base_ExecuteUpdate)
  {
    if (!(viewName == "openTransfers"))
      return base_ExecuteUpdate(viewName, keys, values, parameters);
    using (new PXReadBranchRestrictedScope())
      return base_ExecuteUpdate(viewName, keys, values, parameters);
  }

  private class SubItemInfo
  {
    public ImmutableList<Segment> Segments { get; }

    public ImmutableDictionary<short?, string> SegmentValues { get; }

    public SubItemInfo(PXGraph graph)
    {
      SegmentValue[] array = GraphHelper.RowCast<SegmentValue>((IEnumerable) PXSelectBase<SegmentValue, PXViewOf<SegmentValue>.BasedOn<SelectFromBase<SegmentValue, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Segment>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Segment.dimensionID, Equal<SegmentValue.dimensionID>>>>>.And<BqlOperand<Segment.segmentID, IBqlShort>.IsEqual<SegmentValue.segmentID>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SegmentValue.dimensionID, Equal<SubItemAttribute.dimensionName>>>>, And<BqlOperand<Segment.isCosted, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<SegmentValue.isConsolidatedValue, IBqlBool>.IsEqual<True>>>>.Config>.Select(graph, Array.Empty<object>())).ToArray<SegmentValue>();
      IGrouping<short?, SegmentValue> source = ((IEnumerable<SegmentValue>) array).GroupBy<SegmentValue, short?>((Func<SegmentValue, short?>) (s => s.SegmentID)).FirstOrDefault<IGrouping<short?, SegmentValue>>((Func<IGrouping<short?, SegmentValue>, bool>) (g => g.Count<SegmentValue>() > 1));
      if (source != null)
        throw new PXException("The '{0}' segment of the '{1}' segmented key has more than one value with the Aggregation check box selected  on the Segment Values (CS203000) form.", new object[2]
        {
          (object) source.Key,
          (object) source.Skip<SegmentValue>(1).First<SegmentValue>().DimensionID
        });
      this.SegmentValues = ImmutableDictionary.ToImmutableDictionary<SegmentValue, short?, string>((IEnumerable<SegmentValue>) array, (Func<SegmentValue, short?>) (v => v.SegmentID), (Func<SegmentValue, string>) (v => v.Value));
      this.Segments = ImmutableList.ToImmutableList<Segment>(GraphHelper.RowCast<Segment>((IEnumerable) PXSelectBase<Segment, PXViewOf<Segment>.BasedOn<SelectFromBase<Segment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Segment.dimensionID, IBqlString>.IsEqual<SubItemAttribute.dimensionName>>>.Config>.Select(graph, Array.Empty<object>())));
    }

    public string MakeCostSubItemCD(string subItemCD)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      int num1 = 0;
      foreach (Segment segment in this.Segments)
      {
        string str1 = subItemCD;
        int startIndex = num1;
        short? length1 = segment.Length;
        int length2 = (int) length1.Value;
        string str2 = str1.Substring(startIndex, length2);
        if (segment.IsCosted.GetValueOrDefault() || str2.TrimEnd() == string.Empty)
        {
          stringBuilder1.Append(str2);
        }
        else
        {
          if (!this.SegmentValues.TryGetValue(segment.SegmentID, ref str2))
            throw new PXException("Subitem Segmented Key missing one or more Consolidated values.");
          StringBuilder stringBuilder2 = stringBuilder1;
          string str3 = str2;
          length1 = segment.Length;
          int valueOrDefault = (int) length1.GetValueOrDefault();
          string str4 = str3.PadRight(valueOrDefault);
          stringBuilder2.Append(str4);
        }
        int num2 = num1;
        length1 = segment.Length;
        int num3 = (int) length1.Value;
        num1 = num2 + num3;
      }
      return stringBuilder1.ToString();
    }
  }

  public class SOOrderShipmentComparer : IEqualityComparer<PX.Objects.PO.POReceiptLine>
  {
    public bool Equals(PX.Objects.PO.POReceiptLine x, PX.Objects.PO.POReceiptLine y)
    {
      return x.SOOrderType == y.SOOrderType && x.SOOrderNbr == y.SOOrderNbr && x.SOShipmentNbr == y.SOShipmentNbr;
    }

    public int GetHashCode(PX.Objects.PO.POReceiptLine obj)
    {
      return obj.SOShipmentNbr.GetHashCode() + 37 * obj.SOOrderType.GetHashCode() + 109 * obj.SOOrderNbr.GetHashCode();
    }
  }
}
