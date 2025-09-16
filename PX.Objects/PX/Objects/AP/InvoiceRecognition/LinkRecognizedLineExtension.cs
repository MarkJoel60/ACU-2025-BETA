// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.LinkRecognizedLineExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.Objects.PM;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

[Serializable]
public class LinkRecognizedLineExtension : PXGraphExtension<APInvoiceRecognitionEntry>
{
  internal static string[] APTranPOFields = new string[18]
  {
    "POAccrualType",
    "POAccrualRefNoteID",
    "POAccrualLineNbr",
    "ReceiptType",
    "ReceiptNbr",
    "ReceiptLineNbr",
    "SubItemID",
    "POOrderType",
    "PONbr",
    "POLineNbr",
    "BranchID",
    "LineType",
    "AccountID",
    "SubID",
    "SiteID",
    "ProjectID",
    "TaskID",
    "CostCodeID"
  };
  [PXCopyPasteHiddenView]
  public PXSelectJoin<POLineS, LeftJoin<PX.Objects.PO.POOrder, On<POLineS.orderNbr, Equal<PX.Objects.PO.POOrder.orderNbr>, And<POLineS.orderType, Equal<PX.Objects.PO.POOrder.orderType>>>>, Where<POLineS.pOAccrualType, Equal<POAccrualType.order>, And<POLineS.orderNbr, Equal<Required<POLineS.orderNbr>>, And<POLineS.orderType, Equal<Required<POLineS.orderType>>, And<POLineS.lineNbr, Equal<Required<POLineS.lineNbr>>>>>>> POLineLink;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<POReceiptLineS, LeftJoin<PX.Objects.PO.POReceipt, On<POReceiptLineS.FK.Receipt>>, Where<POReceiptLineS.receiptType, Equal<Required<LinkLineReceipt.receiptType>>, And<POReceiptLineS.receiptNbr, Equal<Required<LinkLineReceipt.receiptNbr>>, And<POReceiptLineS.lineNbr, Equal<Required<LinkLineReceipt.receiptLineNbr>>>>>> ReceipLineLinked;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<POReceiptLineS, LeftJoin<PX.Objects.PO.POReceipt, On<POReceiptLineS.FK.Receipt>>> linkLineReceiptTran;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<POLineS, LeftJoin<PX.Objects.PO.POOrder, On<POLineS.orderNbr, Equal<PX.Objects.PO.POOrder.orderNbr>, And<POLineS.orderType, Equal<PX.Objects.PO.POOrder.orderType>>>>> linkLineOrderTran;
  public PXFilter<LinkLineFilter> linkLineFilter;
  public PXAction<APRecognizedInvoice> linkLine;

  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Order Nbr.")]
  [PXSelector(typeof (Search5<POOrderRS.orderNbr, LeftJoin<LinkLineReceipt, On<POOrderRS.orderNbr, Equal<LinkLineReceipt.orderNbr>, And<PX.Objects.PO.POOrder.orderType, Equal<LinkLineReceipt.orderType>, And<Current<LinkLineFilter.selectedMode>, Equal<LinkLineFilter.selectedMode.receipt>>>>, LeftJoin<LinkLineOrder, On<POOrderRS.orderNbr, Equal<LinkLineOrder.orderNbr>, And<PX.Objects.PO.POOrder.orderType, Equal<LinkLineOrder.orderType>, And<Current<LinkLineFilter.selectedMode>, Equal<LinkLineFilter.selectedMode.order>>>>>>, Where2<Where<LinkLineReceipt.orderNbr, PX.Data.IsNotNull, Or2<Where<LinkLineOrder.orderType, PX.Data.IsNotNull>, And<LinkLineOrder.orderType, NotEqual<POOrderType.regularSubcontract>>>>, PX.Data.And<Where<PX.Objects.PO.POOrder.vendorID, Equal<Current<APRecognizedInvoice.vendorID>>, And<PX.Objects.PO.POOrder.vendorLocationID, Equal<Current<APRecognizedInvoice.vendorLocationID>>, And2<Not<FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>>, Or2<FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>, And<PX.Objects.PO.POOrder.vendorID, Equal<Current<APRecognizedInvoice.suppliedByVendorID>>, And<PX.Objects.PO.POOrder.vendorLocationID, Equal<Current<APRecognizedInvoice.suppliedByVendorLocationID>>, And<PX.Objects.PO.POOrder.payToVendorID, Equal<Current<APRecognizedInvoice.vendorID>>>>>>>>>>>, Aggregate<GroupBy<POOrderRS.orderNbr, GroupBy<PX.Objects.PO.POOrder.orderType>>>>))]
  protected virtual void LinkLineFilter_pOOrderNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [PXString(1)]
  [PXUIField(DisplayName = "Selected Mode")]
  [PXStringList(new string[] {"O", "R"}, new string[] {"Purchase Order", "Purchase Receipt"})]
  protected virtual void LinkLineFilter_SelectedMode_CacheAttached(PXCache sender)
  {
  }

  public override void Initialize()
  {
    base.Initialize();
    this.linkLineReceiptTran.Cache.AllowDelete = false;
    this.linkLineReceiptTran.Cache.AllowInsert = false;
    this.linkLineOrderTran.Cache.AllowDelete = false;
    this.linkLineOrderTran.Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(this.linkLineReceiptTran.Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<POReceiptLineS.selected>(this.linkLineReceiptTran.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled(this.linkLineOrderTran.Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<LinkLineOrder.selected>(this.linkLineOrderTran.Cache, (object) null, true);
  }

  [PXUIField(DisplayName = "Link PO Line", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Select, FieldClass = "DISTR", Visible = true)]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable LinkLine(PXAdapter adapter)
  {
    if (this.Base.Transactions.Current == null || this.Base.Transactions.Current.TranType != "INV")
      return adapter.Get();
    if (!this.Base.Transactions.Current.InventoryID.HasValue)
    {
      PXUIFieldAttribute.SetWarning<PX.Objects.AP.APTran.inventoryID>(this.Base.Transactions.Cache, (object) this.Base.Transactions.Current, PXMessages.LocalizeNoPrefix("The line cannot be linked to a PO line. Inventory ID should not be empty."));
    }
    else
    {
      this.Base.Transactions.Cache.ClearQueryCache();
      WebDialogResult webDialogResult;
      if ((webDialogResult = this.linkLineFilter.AskExt((PXView.InitializePanel) ((graph, view) =>
      {
        this.linkLineFilter.Cache.SetValueExt<LinkLineFilter.inventoryID>((object) this.linkLineFilter.Current, (object) this.Base.Transactions.Current.InventoryID);
        this.linkLineFilter.Current.UOM = this.Base.Transactions.Current?.UOM;
        this.linkLineFilter.Current.POOrderNbr = (string) null;
        this.linkLineFilter.Current.SiteID = new int?();
        PX.Objects.CR.Location location = (PX.Objects.CR.Location) null;
        APRecognizedInvoice current1 = this.Base.Document.Current;
        if ((current1 != null ? (current1.VendorLocationID.HasValue ? 1 : 0) : 0) != 0)
          location = (PX.Objects.CR.Location) this.Base.VendorLocation.Select();
        if (location != null)
          this.linkLineFilter.Cache.SetValueExt<LinkLineFilter.selectedMode>((object) this.linkLineFilter.Current, location.VAllowAPBillBeforeReceipt.GetValueOrDefault() ? (object) "O" : (object) "R");
        APRecognizedTran current2 = this.Base.Transactions.Current;
        this.linkLineOrderTran.Cache.Clear();
        this.linkLineReceiptTran.Cache.Clear();
        this.linkLineOrderTran.View.Clear();
        this.linkLineReceiptTran.View.Clear();
        this.linkLineOrderTran.Cache.ClearQueryCache();
        this.linkLineReceiptTran.Cache.ClearQueryCache();
      }), true)) != WebDialogResult.None && webDialogResult == WebDialogResult.Yes && (this.linkLineReceiptTran.Cache.Updated.Count() > 0L || this.linkLineOrderTran.Cache.Updated.Count() > 0L))
      {
        APRecognizedTran apRecognizedTran = this.ClearAPTranReferences((APRecognizedTran) this.Base.Transactions.Cache.CreateCopy((object) this.Base.Transactions.Current));
        if (this.linkLineFilter.Current.SelectedMode == "R")
        {
          foreach (POReceiptLineS receipt in this.linkLineReceiptTran.Cache.Updated)
          {
            if (receipt.Selected.GetValueOrDefault())
            {
              LinkRecognizedLineExtension.LinkToReceipt(apRecognizedTran, receipt);
              break;
            }
          }
        }
        if (this.linkLineFilter.Current.SelectedMode == "O")
        {
          foreach (POLineS order in this.linkLineOrderTran.Cache.Updated)
          {
            if (order.Selected.GetValueOrDefault())
            {
              this.LinkToOrder(apRecognizedTran, order);
              break;
            }
          }
        }
        this.Base.Transactions.Cache.Update((object) apRecognizedTran);
        if (string.IsNullOrEmpty(apRecognizedTran.ReceiptNbr) && string.IsNullOrEmpty(apRecognizedTran.PONbr))
        {
          this.Base.Transactions.Cache.SetDefaultExt<PX.Objects.AP.APTran.accountID>((object) apRecognizedTran);
          this.Base.Transactions.Cache.SetDefaultExt<PX.Objects.AP.APTran.subID>((object) apRecognizedTran);
        }
      }
    }
    return adapter.Get();
  }

  public virtual APRecognizedTran ClearAPTranReferences(APRecognizedTran apTran)
  {
    apTran.ReceiptType = (string) null;
    apTran.ReceiptNbr = (string) null;
    apTran.ReceiptLineNbr = new int?();
    apTran.POOrderType = (string) null;
    apTran.PONbr = (string) null;
    apTran.POLineNbr = new int?();
    apTran.POAccrualType = (string) null;
    apTran.POAccrualRefNoteID = new Guid?();
    apTran.POAccrualLineNbr = new int?();
    apTran.AccountID = new int?();
    apTran.SubID = new int?();
    apTran.SiteID = new int?();
    apTran.RecognizedPONumber = (string) null;
    apTran.ProjectID = new int?();
    apTran.TaskID = new int?();
    apTran.CostCodeID = new int?();
    apTran.POLinkStatus = "N";
    return apTran;
  }

  private static void LinkToReceipt(APRecognizedTran apTran, POReceiptLineS receipt)
  {
    receipt.SetReferenceKeyTo((PX.Objects.AP.APTran) apTran);
    apTran.RecognizedPONumber = apTran.PONbr;
    apTran.BranchID = receipt.BranchID;
    apTran.LineType = receipt.LineType;
    apTran.AccountID = receipt.POAccrualAcctID ?? receipt.ExpenseAcctID;
    apTran.SubID = receipt.POAccrualSubID ?? receipt.ExpenseSubID;
    apTran.SiteID = receipt.SiteID;
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.projectAccounting>())
    {
      apTran.ProjectID = receipt.ProjectID;
      apTran.TaskID = receipt.TaskID;
      apTran.CostCodeID = receipt.CostCodeID;
    }
    apTran.POLinkStatus = "L";
  }

  public virtual void LinkToOrder(APRecognizedTran apTran, POLineS order)
  {
    order.SetReferenceKeyTo((PX.Objects.AP.APTran) apTran);
    apTran.RecognizedPONumber = apTran.PONbr;
    apTran.BranchID = order.BranchID;
    apTran.LineType = order.LineType;
    apTran.AccountID = order.POAccrualAcctID ?? order.ExpenseAcctID;
    apTran.SubID = order.POAccrualSubID ?? order.ExpenseSubID;
    apTran.SiteID = order.SiteID;
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.projectAccounting>())
    {
      apTran.ProjectID = order.ProjectID;
      apTran.TaskID = order.TaskID;
      apTran.CostCodeID = order.CostCodeID;
    }
    apTran.POLinkStatus = "L";
  }

  public virtual IEnumerable LinkLineReceiptTran()
  {
    return (IEnumerable) this.GetAvailableReceiptLines(this.Base.Transactions.Current, this.linkLineFilter.Current?.POOrderNbr, (int?) this.linkLineFilter.Current?.InventoryID, this.linkLineFilter.Current?.UOM);
  }

  public virtual List<PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>> GetAvailableReceiptLines(
    APRecognizedTran currentRecognizedTran,
    string pONbr,
    int? inventoryID,
    string uOM)
  {
    List<PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>> availableReceiptLines = new List<PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>>();
    if (currentRecognizedTran == null)
      return availableReceiptLines;
    POAccrualComparer comparer = new POAccrualComparer();
    HashSet<APRecognizedTran> apRecognizedTranSet = new HashSet<APRecognizedTran>((IEqualityComparer<APRecognizedTran>) comparer);
    HashSet<APRecognizedTran> source = new HashSet<APRecognizedTran>((IEqualityComparer<APRecognizedTran>) comparer);
    foreach (APRecognizedTran apRecognizedTran in this.Base.Transactions.Cache.Inserted)
    {
      int? inventoryId1 = currentRecognizedTran.InventoryID;
      int? inventoryId2 = apRecognizedTran.InventoryID;
      if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue && currentRecognizedTran.UOM == apRecognizedTran.UOM)
        apRecognizedTranSet.Add(apRecognizedTran);
    }
    foreach (APRecognizedTran apRecognizedTran in this.Base.Transactions.Cache.Deleted)
    {
      int? inventoryId3 = currentRecognizedTran.InventoryID;
      int? inventoryId4 = apRecognizedTran.InventoryID;
      if (inventoryId3.GetValueOrDefault() == inventoryId4.GetValueOrDefault() & inventoryId3.HasValue == inventoryId4.HasValue && currentRecognizedTran.UOM == apRecognizedTran.UOM && this.Base.Transactions.Cache.GetStatus((object) apRecognizedTran) != PXEntryStatus.InsertedDeleted && !apRecognizedTranSet.Remove(apRecognizedTran))
        source.Add(apRecognizedTran);
    }
    foreach (APRecognizedTran data in this.Base.Transactions.Cache.Updated)
    {
      int? inventoryId5 = currentRecognizedTran.InventoryID;
      int? inventoryId6 = data.InventoryID;
      if (inventoryId5.GetValueOrDefault() == inventoryId6.GetValueOrDefault() & inventoryId5.HasValue == inventoryId6.HasValue && currentRecognizedTran.UOM == data.UOM)
      {
        APRecognizedTran apRecognizedTran1 = new APRecognizedTran();
        apRecognizedTran1.POAccrualType = (string) this.Base.Transactions.Cache.GetValueOriginal<PX.Objects.AP.APTran.pOAccrualType>((object) data);
        apRecognizedTran1.POAccrualRefNoteID = (Guid?) this.Base.Transactions.Cache.GetValueOriginal<PX.Objects.AP.APTran.pOAccrualRefNoteID>((object) data);
        apRecognizedTran1.POAccrualLineNbr = (int?) this.Base.Transactions.Cache.GetValueOriginal<PX.Objects.AP.APTran.pOAccrualLineNbr>((object) data);
        apRecognizedTran1.POOrderType = (string) this.Base.Transactions.Cache.GetValueOriginal<PX.Objects.AP.APTran.pOOrderType>((object) data);
        apRecognizedTran1.PONbr = (string) this.Base.Transactions.Cache.GetValueOriginal<PX.Objects.AP.APTran.pONbr>((object) data);
        apRecognizedTran1.POLineNbr = (int?) this.Base.Transactions.Cache.GetValueOriginal<PX.Objects.AP.APTran.pOLineNbr>((object) data);
        apRecognizedTran1.ReceiptNbr = (string) this.Base.Transactions.Cache.GetValueOriginal<PX.Objects.AP.APTran.receiptNbr>((object) data);
        apRecognizedTran1.ReceiptType = (string) this.Base.Transactions.Cache.GetValueOriginal<PX.Objects.AP.APTran.receiptType>((object) data);
        apRecognizedTran1.ReceiptLineNbr = (int?) this.Base.Transactions.Cache.GetValueOriginal<PX.Objects.AP.APTran.receiptLineNbr>((object) data);
        APRecognizedTran apRecognizedTran2 = apRecognizedTran1;
        if (!apRecognizedTranSet.Remove(apRecognizedTran2))
          source.Add(apRecognizedTran2);
        if (!source.Remove(data))
          apRecognizedTranSet.Add(data);
      }
    }
    source.Add(currentRecognizedTran);
    foreach (PXResult<LinkLineReceipt> linkLineReceipt1 in this.GetLinkLineReceipts(pONbr, inventoryID, uOM, (int?) this.Base.Document.Current?.VendorLocationID))
    {
      LinkLineReceipt linkLineReceipt2 = (LinkLineReceipt) linkLineReceipt1;
      APRecognizedTran apRecognizedTran3 = new APRecognizedTran();
      apRecognizedTran3.POAccrualType = linkLineReceipt2.POAccrualType;
      apRecognizedTran3.POAccrualRefNoteID = linkLineReceipt2.POAccrualRefNoteID;
      apRecognizedTran3.POAccrualLineNbr = linkLineReceipt2.POAccrualLineNbr;
      apRecognizedTran3.POOrderType = linkLineReceipt2.OrderType;
      apRecognizedTran3.PONbr = linkLineReceipt2.OrderNbr;
      apRecognizedTran3.POLineNbr = linkLineReceipt2.OrderLineNbr;
      apRecognizedTran3.ReceiptType = linkLineReceipt2.ReceiptType;
      apRecognizedTran3.ReceiptNbr = linkLineReceipt2.ReceiptNbr;
      apRecognizedTran3.ReceiptLineNbr = linkLineReceipt2.ReceiptLineNbr;
      APRecognizedTran apRecognizedTran4 = apRecognizedTran3;
      if (!apRecognizedTranSet.Contains(apRecognizedTran4))
      {
        PXResult<POReceiptLineS, PX.Objects.PO.POReceipt> data = (PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>) (PXResult<POReceiptLineS>) this.ReceipLineLinked.Select((object) linkLineReceipt2.ReceiptType, (object) linkLineReceipt2.ReceiptNbr, (object) linkLineReceipt2.ReceiptLineNbr);
        if (this.linkLineReceiptTran.Cache.GetStatus((object) (POReceiptLineS) data) != PXEntryStatus.Updated && ((POReceiptLineS) data).CompareReferenceKey((PX.Objects.AP.APTran) currentRecognizedTran))
        {
          this.linkLineReceiptTran.Cache.SetValue<POReceiptLineS.selected>((object) (POReceiptLineS) data, (object) true);
          this.linkLineReceiptTran.Cache.SetStatus((object) (POReceiptLineS) data, PXEntryStatus.Updated);
        }
        availableReceiptLines.Add(data);
      }
    }
    foreach (APRecognizedTran apRecognizedTran in source.Where<APRecognizedTran>((Func<APRecognizedTran, bool>) (t => t.POAccrualType != null)))
    {
      foreach (PXResult<POReceiptLineS, PX.Objects.PO.POReceipt> data in PXSelectBase<POReceiptLineS, PXSelectJoin<POReceiptLineS, LeftJoin<PX.Objects.PO.POReceipt, On<POReceiptLineS.FK.Receipt>>, Where<POReceiptLineS.pOAccrualType, Equal<Required<LinkLineReceipt.pOAccrualType>>, And<POReceiptLineS.pOAccrualRefNoteID, Equal<Required<LinkLineReceipt.pOAccrualRefNoteID>>, And<POReceiptLineS.pOAccrualLineNbr, Equal<Required<LinkLineReceipt.pOAccrualLineNbr>>>>>>.Config>.Select((PXGraph) this.Base, (object) apRecognizedTran.POAccrualType, (object) apRecognizedTran.POAccrualRefNoteID, (object) apRecognizedTran.POAccrualLineNbr))
      {
        int? inventoryId7 = currentRecognizedTran.InventoryID;
        int? inventoryId8 = ((POReceiptLineS) data).InventoryID;
        if (inventoryId7.GetValueOrDefault() == inventoryId8.GetValueOrDefault() & inventoryId7.HasValue == inventoryId8.HasValue)
        {
          if (this.linkLineReceiptTran.Cache.GetStatus((object) (POReceiptLineS) data) != PXEntryStatus.Updated && ((POReceiptLineS) data).CompareReferenceKey((PX.Objects.AP.APTran) currentRecognizedTran))
          {
            this.linkLineReceiptTran.Cache.SetValue<POReceiptLineS.selected>((object) (POReceiptLineS) data, (object) true);
            this.linkLineReceiptTran.Cache.SetStatus((object) (POReceiptLineS) data, PXEntryStatus.Updated);
          }
          availableReceiptLines.Add(data);
        }
      }
    }
    return availableReceiptLines;
  }

  private PXResultset<LinkLineReceipt> GetLinkLineReceipts(
    string pONbr,
    int? inventoryID,
    string uOM,
    int? vendorLocationID)
  {
    PXSelectBase<LinkLineReceipt> pxSelectBase = (PXSelectBase<LinkLineReceipt>) new PXSelect<LinkLineReceipt, Where2<Where<Required<LinkLineFilter.pOOrderNbr>, Equal<LinkLineReceipt.orderNbr>, Or<Required<LinkLineFilter.pOOrderNbr>, PX.Data.IsNull>>, And<LinkLineReceipt.inventoryID, Equal<Required<PX.Objects.AP.APTran.inventoryID>>, And2<Where<LinkLineReceipt.uOM, Equal<Required<PX.Objects.AP.APTran.uOM>>, Or<Required<PX.Objects.AP.APTran.uOM>, PX.Data.IsNull>>, And<LinkLineReceipt.receiptType, Equal<POReceiptType.poreceipt>>>>>>((PXGraph) this.Base);
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>())
      pxSelectBase.WhereAnd<Where<LinkLineReceipt.vendorID, Equal<Current<APRecognizedInvoice.suppliedByVendorID>>, And<LinkLineReceipt.vendorLocationID, Equal<Current<APRecognizedInvoice.suppliedByVendorLocationID>>, PX.Data.And<Where<LinkLineReceipt.payToVendorID, PX.Data.IsNull, Or<LinkLineReceipt.payToVendorID, Equal<Current<APRecognizedInvoice.vendorID>>>>>>>>();
    else
      pxSelectBase.WhereAnd<Where<LinkLineReceipt.vendorID, Equal<Current<APRecognizedInvoice.vendorID>>, And<LinkLineReceipt.vendorLocationID, Equal<Required<APRecognizedInvoice.vendorLocationID>>>>>();
    return pxSelectBase.Select((object) pONbr, (object) pONbr, (object) inventoryID, (object) uOM, (object) uOM, (object) vendorLocationID);
  }

  public virtual IEnumerable LinkLineOrderTran()
  {
    return (IEnumerable) this.GetAvailableOrderLines(this.Base.Transactions.Current, this.linkLineFilter.Current?.POOrderNbr, (int?) this.linkLineFilter.Current?.InventoryID, this.linkLineFilter.Current?.UOM, new POOrderType.ListAttribute().ValueLabelDic.Keys.ToArray<string>());
  }

  public virtual List<PXResult<POLineS, PX.Objects.PO.POOrder>> GetAvailableOrderLines(
    APRecognizedTran currentRecognizedTran,
    string pONbr,
    int? inventoryID,
    string uOM,
    string[] poTypes = null)
  {
    List<PXResult<POLineS, PX.Objects.PO.POOrder>> availableOrderLines1 = new List<PXResult<POLineS, PX.Objects.PO.POOrder>>();
    if (currentRecognizedTran == null)
      return availableOrderLines1;
    Lazy<POAccrualSet> usedPOAccrual = new Lazy<POAccrualSet>((Func<POAccrualSet>) (() =>
    {
      POAccrualSet availableOrderLines2 = new POAccrualSet((IEnumerable<PX.Objects.AP.APTran>) this.Base.Transactions.Select().RowCast<APRecognizedTran>(), this.Base.Document.Current?.DocType == "PPM" ? (IEqualityComparer<PX.Objects.AP.APTran>) new POLineComparer() : (IEqualityComparer<PX.Objects.AP.APTran>) new POAccrualComparer());
      availableOrderLines2.Remove((PX.Objects.AP.APTran) currentRecognizedTran);
      return availableOrderLines2;
    }));
    foreach (LinkLineOrder linkLineOrder in this.GetLinkLineOrders(pONbr, inventoryID, uOM, (int?) this.Base.Document.Current?.VendorLocationID, poTypes).RowCast<LinkLineOrder>().AsEnumerable<LinkLineOrder>().Where<LinkLineOrder>((Func<LinkLineOrder, bool>) (l => !usedPOAccrual.Value.Contains(l))))
    {
      PXResult<POLineS, PX.Objects.PO.POOrder> data = (PXResult<POLineS, PX.Objects.PO.POOrder>) (PXResult<POLineS>) this.POLineLink.Select((object) linkLineOrder.OrderNbr, (object) linkLineOrder.OrderType, (object) linkLineOrder.OrderLineNbr);
      if (this.linkLineOrderTran.Cache.GetStatus((object) (POLineS) data) != PXEntryStatus.Updated && ((POLineS) data).CompareReferenceKey((PX.Objects.AP.APTran) currentRecognizedTran))
      {
        this.linkLineOrderTran.Cache.SetValue<POLineS.selected>((object) (POLineS) data, (object) true);
        this.linkLineOrderTran.Cache.SetStatus((object) (POLineS) data, PXEntryStatus.Updated);
      }
      availableOrderLines1.Add(data);
    }
    return availableOrderLines1;
  }

  public PXResultset<LinkLineOrder> GetLinkLineOrders(
    string pONbr,
    int? inventoryID,
    string uOM,
    int? vendorLocationID,
    string[] orderTypes)
  {
    PXSelectBase<LinkLineOrder> pxSelectBase = (PXSelectBase<LinkLineOrder>) new PXSelect<LinkLineOrder, Where2<Where<Required<LinkLineFilter.pOOrderNbr>, Equal<LinkLineOrder.orderNbr>, Or<Required<LinkLineFilter.pOOrderNbr>, PX.Data.IsNull>>, And2<Where<LinkLineOrder.inventoryID, Equal<Required<PX.Objects.AP.APTran.inventoryID>>, Or2<Where<Required<PX.Objects.AP.APTran.inventoryID>, PX.Data.IsNull>, And2<Where<LinkLineOrder.inventoryID, PX.Data.IsNull>, Or<LinkLineOrder.inventoryID, Equal<Required<LinkLineOrder.inventoryID>>>>>>, And2<Where<LinkLineOrder.uOM, Equal<Required<PX.Objects.AP.APTran.uOM>>, Or<Required<PX.Objects.AP.APTran.uOM>, PX.Data.IsNull>>, And<LinkLineOrder.orderCuryID, Equal<Required<PX.Objects.AP.APInvoice.curyID>>>>>>>((PXGraph) this.Base);
    string curyId = this.Base.Document.Current?.CuryID;
    List<object> objectList = new List<object>()
    {
      (object) pONbr,
      (object) pONbr,
      (object) inventoryID,
      (object) inventoryID,
      (object) PMInventorySelectorAttribute.EmptyInventoryID,
      (object) uOM,
      (object) uOM,
      (object) curyId
    };
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>())
    {
      pxSelectBase.WhereAnd<Where<LinkLineOrder.vendorID, Equal<Current<APRecognizedInvoice.suppliedByVendorID>>, And<LinkLineOrder.vendorLocationID, Equal<Current<APRecognizedInvoice.suppliedByVendorLocationID>>, And<LinkLineOrder.payToVendorID, Equal<Current<APRecognizedInvoice.vendorID>>>>>>();
    }
    else
    {
      pxSelectBase.WhereAnd<Where<LinkLineOrder.vendorID, Equal<Current<APRecognizedInvoice.vendorID>>, And<LinkLineOrder.vendorLocationID, Equal<Required<APRecognizedInvoice.vendorLocationID>>>>>();
      objectList.Add((object) vendorLocationID);
    }
    if (orderTypes != null && orderTypes.Length != 0)
    {
      pxSelectBase.WhereAnd<Where<BqlOperand<LinkLineOrder.orderType, IBqlString>.IsIn<P.AsString>>>();
      objectList.Add((object) orderTypes);
    }
    return pxSelectBase.Select(objectList.ToArray());
  }

  [PXOverride]
  public virtual void AutoLinkAPAndPO(
    APRecognizedTran tran,
    string poNumber,
    Action<APRecognizedTran, string> baseFunction)
  {
    if (tran == null || tran.TranType != "INV")
      return;
    PX.Objects.CR.Location location = (PX.Objects.CR.Location) null;
    APRecognizedInvoice current = this.Base.Document.Current;
    int? nullable;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      nullable = current.VendorLocationID;
      num = nullable.HasValue ? 1 : 0;
    }
    if (num != 0)
      location = (PX.Objects.CR.Location) this.Base.VendorLocation.Select();
    if (location != null)
    {
      POReceiptLineS receipt = (POReceiptLineS) null;
      POLineS order = (POLineS) null;
      bool flag1 = false;
      bool flag2 = false;
      if (!location.VAllowAPBillBeforeReceipt.GetValueOrDefault())
      {
        foreach (PXResult<POReceiptLineS, PX.Objects.PO.POReceipt> availableReceiptLine in this.GetAvailableReceiptLines(tran, poNumber, tran.InventoryID, tran.UOM))
        {
          if (receipt == null)
          {
            receipt = (POReceiptLineS) availableReceiptLine;
          }
          else
          {
            if (!(receipt.ReceiptType != ((POReceiptLineS) availableReceiptLine).ReceiptType) && !(receipt.ReceiptNbr != ((POReceiptLineS) availableReceiptLine).ReceiptNbr))
            {
              nullable = receipt.LineNbr;
              int? lineNbr = ((POReceiptLineS) availableReceiptLine).LineNbr;
              if (nullable.GetValueOrDefault() == lineNbr.GetValueOrDefault() & nullable.HasValue == lineNbr.HasValue)
                continue;
            }
            flag1 = true;
          }
        }
      }
      string[] allowedOrderTypes = this.GetAutoLinkAllowedOrderTypes(location);
      if (allowedOrderTypes != null)
      {
        foreach (PXResult<POLineS, PX.Objects.PO.POOrder> availableOrderLine in this.GetAvailableOrderLines(tran, poNumber, tran.InventoryID, tran.UOM, allowedOrderTypes))
        {
          if (order == null)
            order = (POLineS) availableOrderLine;
          else
            flag2 = true;
        }
      }
      if (flag1 | flag2)
      {
        this.ClearAPTranReferences(tran);
        this.Base.Transactions.Cache.SetValueExt<APRecognizedTran.recognizedPONumber>((object) tran, (object) poNumber);
        if (poNumber != null)
        {
          if (flag1)
            this.Base.Transactions.Cache.SetValueExt<APRecognizedTran.pOLinkStatus>((object) tran, (object) "P");
          else
            this.Base.Transactions.Cache.SetValueExt<APRecognizedTran.pOLinkStatus>((object) tran, (object) "M");
        }
        else
          this.Base.Transactions.Cache.SetValueExt<APRecognizedTran.pOLinkStatus>((object) tran, (object) "N");
      }
      else if (receipt != null && order != null)
      {
        this.ClearAPTranReferences(tran);
        this.Base.Transactions.Cache.SetValueExt<APRecognizedTran.pOLinkStatus>((object) tran, (object) "N");
      }
      else if (receipt == null && order == null)
      {
        this.ClearAPTranReferences(tran);
      }
      else
      {
        if (receipt == null && order == null)
          return;
        this.ClearAPTranReferences(tran);
        if (receipt != null)
          LinkRecognizedLineExtension.LinkToReceipt(tran, receipt);
        else
          this.LinkToOrder(tran, order);
        if (!string.IsNullOrEmpty(tran.ReceiptNbr) || !string.IsNullOrEmpty(tran.PONbr))
          return;
        this.Base.Transactions.Cache.SetDefaultExt<PX.Objects.AP.APTran.accountID>((object) tran);
        this.Base.Transactions.Cache.SetDefaultExt<PX.Objects.AP.APTran.subID>((object) tran);
      }
    }
    else
      this.ClearAPTranReferences(tran);
  }

  public virtual string[] GetAutoLinkAllowedOrderTypes(PX.Objects.CR.Location location)
  {
    return !location.VAllowAPBillBeforeReceipt.GetValueOrDefault() ? (string[]) null : new POOrderType.ListAttribute().ValueLabelDic.Keys.ToArray<string>();
  }

  protected virtual void _(PX.Data.Events.RowSelected<APRecognizedInvoice> e)
  {
    if (e.Row == null)
      return;
    PXAction<APRecognizedInvoice> linkLine = this.linkLine;
    int num;
    if (e.Row.DocType == "INV")
    {
      int? nullable = e.Row.VendorID;
      if (nullable.HasValue)
      {
        nullable = e.Row.VendorLocationID;
        if (nullable.HasValue)
        {
          num = EnumerableExtensions.IsNotIn<string>(e.Row.RecognitionStatus, "P", "N") ? 1 : 0;
          goto label_6;
        }
      }
    }
    num = 0;
label_6:
    linkLine.SetEnabled(num != 0);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<APRecognizedInvoice> e)
  {
    if (e.Row == null || e.OldRow == null || this.Base.Document.Cache.ObjectsEqual<APRecognizedInvoice.docType>((object) e.Row, (object) e.OldRow) || !(e.Row.DocType != "INV"))
      return;
    foreach (PXResult<APRecognizedTran> apTran in this.Base.Transactions.Select())
      this.ClearAPTranReferences((APRecognizedTran) apTran);
  }

  protected virtual void _(PX.Data.Events.RowSelected<APRecognizedTran> e)
  {
    if (e.Row == null || this.Base.Document.Current == null || this.Base.Document.Current.RecognitionStatus == "P")
      return;
    bool flag = false;
    if (this.Base.Document.Current.DocType == "INV" && e.Row.InventoryID.HasValue)
    {
      PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this.Base, (object) e.Row.InventoryID);
      flag = inventoryItem != null && inventoryItem.StkItem.GetValueOrDefault();
    }
    if (flag)
    {
      switch (e.Row.POLinkStatus)
      {
        case "N":
          PXUIFieldAttribute.SetWarning<APRecognizedTran.recognizedPONumber>(e.Cache, (object) e.Row, PXMessages.LocalizeNoPrefix("The line is not linked to a purchase order line. You can click the Link PO Line button to select a purchase order line."));
          break;
        case "P":
          PXUIFieldAttribute.SetWarning<APRecognizedTran.recognizedPONumber>(e.Cache, (object) e.Row, PXMessages.LocalizeNoPrefix("Multiple purchase receipt lines have been found. You can click the Link PO Line button to select a purchase receipt line."));
          break;
        case "M":
          PXUIFieldAttribute.SetWarning<APRecognizedTran.recognizedPONumber>(e.Cache, (object) e.Row, PXMessages.LocalizeNoPrefix("Multiple purchase order lines have been found. You can click the Link PO Line button to select a purchase order line."));
          break;
        default:
          PXUIFieldAttribute.SetWarning<APRecognizedTran.recognizedPONumber>(e.Cache, (object) e.Row, (string) null);
          break;
      }
    }
    else
      PXUIFieldAttribute.SetWarning<APRecognizedTran.recognizedPONumber>(e.Cache, (object) e.Row, (string) null);
  }

  protected virtual void _(PX.Data.Events.RowSelected<LinkLineFilter> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = this.linkLineReceiptTran.Cache;
    this.linkLineReceiptTran.View.AllowSelect = e.Row.SelectedMode == "R";
    this.linkLineOrderTran.View.AllowSelect = e.Row.SelectedMode == "O";
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<POLineS.selected> e)
  {
    POLineS row = (POLineS) e.Row;
    if (row == null || (bool) e.OldValue)
      return;
    bool? selected = row.Selected;
    if (!selected.Value)
      return;
    foreach (POLineS data in e.Cache.Updated)
    {
      selected = data.Selected;
      if (selected.GetValueOrDefault() && data != row)
      {
        e.Cache.SetValue<POLineS.selected>((object) data, (object) false);
        this.linkLineOrderTran.View.RequestRefresh();
      }
    }
    foreach (POReceiptLineS data in this.linkLineReceiptTran.Cache.Updated)
    {
      if (data.Selected.GetValueOrDefault())
      {
        this.linkLineReceiptTran.Cache.SetValue<POReceiptLineS.selected>((object) data, (object) false);
        this.linkLineReceiptTran.View.RequestRefresh();
      }
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<POReceiptLineS.selected> e)
  {
    POReceiptLineS row = (POReceiptLineS) e.Row;
    if (row == null || (bool) e.OldValue)
      return;
    bool? selected = row.Selected;
    if (!selected.Value)
      return;
    foreach (POReceiptLineS data in this.linkLineReceiptTran.Cache.Updated)
    {
      selected = data.Selected;
      if (selected.GetValueOrDefault() && data != row)
      {
        e.Cache.SetValue<POReceiptLineS.selected>((object) data, (object) false);
        this.linkLineReceiptTran.View.RequestRefresh();
      }
    }
    foreach (POLineS data in this.linkLineOrderTran.Cache.Updated)
    {
      if (data.Selected.GetValueOrDefault())
      {
        this.linkLineOrderTran.Cache.SetValue<POLineS.selected>((object) data, (object) false);
        this.linkLineOrderTran.View.RequestRefresh();
      }
    }
  }
}
