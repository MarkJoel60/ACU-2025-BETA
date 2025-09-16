// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel.AddPOReceiptExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.PO.DAC.Unbound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel;

/// <summary>
/// This class implements graph extension to use special dialogs called Smart Panel to perform "ADD PO RECEIPT" (Screen AP301000)
/// </summary>
[Serializable]
public class AddPOReceiptExtension : PXGraphExtension<LinkLineExtension, APInvoiceEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.PO.POReceipt> poreceiptslist;
  public PXAction<PX.Objects.AP.APInvoice> addPOReceipt;
  public PXAction<PX.Objects.AP.APInvoice> addPOReceipt2;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase) this.poreceiptslist).Cache.AllowInsert = false;
    ((PXSelectBase) this.poreceiptslist).Cache.AllowDelete = false;
  }

  [PXUIField]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AddPOReceipt(PXAdapter adapter)
  {
    ((PXGraphExtension<APInvoiceEntry>) this).Base.checkTaxCalcMode();
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current != null)
    {
      bool? released = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Released;
      bool flag1 = false;
      if (released.GetValueOrDefault() == flag1 & released.HasValue)
      {
        bool? prebooked = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Prebooked;
        bool flag2 = false;
        // ISSUE: method pointer
        if (prebooked.GetValueOrDefault() == flag2 & prebooked.HasValue && ((PXSelectBase<PX.Objects.PO.POReceipt>) this.poreceiptslist).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CAddPOReceipt\u003Eb__5_0)), true) == 1)
        {
          ((PXGraphExtension<APInvoiceEntry>) this).Base.updateTaxCalcMode();
          this.AddPOReceipt2(adapter);
        }
      }
    }
    ((PXSelectBase) this.poreceiptslist).View.Clear();
    ((PXSelectBase) this.poreceiptslist).Cache.Clear();
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AddPOReceipt2(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Released;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Prebooked;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          using (new APInvoiceEntry.SkipUpdAdjustments(((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.DocType + ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.RefNbr))
          {
            List<PX.Objects.PO.POReceipt> list = GraphHelper.RowCast<PX.Objects.PO.POReceipt>(((PXSelectBase) this.poreceiptslist).Cache.Updated).Where<PX.Objects.PO.POReceipt>((Func<PX.Objects.PO.POReceipt, bool>) (rc => rc.Selected.GetValueOrDefault())).ToList<PX.Objects.PO.POReceipt>();
            if (list.Any<PX.Objects.PO.POReceipt>())
            {
              foreach (PX.Objects.PO.POReceipt receipt in list)
                ((PXGraphExtension<APInvoiceEntry>) this).Base.InvoicePOReceipt(receipt, (DocumentList<PX.Objects.AP.APInvoice>) null, usePOParameters: false, errorIfUnreleasedAPExists: false);
              ((PXGraphExtension<APInvoiceEntry>) this).Base.AttachPrepayment(GraphHelper.RowCast<PX.Objects.PO.POOrder>((IEnumerable) PXSelectBase<PX.Objects.PO.POOrder, PXSelectJoinGroupBy<PX.Objects.PO.POOrder, InnerJoin<POOrderReceipt, On<POOrderReceipt.pOType, Equal<PX.Objects.PO.POOrder.orderType>, And<POOrderReceipt.pONbr, Equal<PX.Objects.PO.POOrder.orderNbr>>>>, Where<POOrderReceipt.receiptType, Equal<Required<POOrderReceipt.receiptType>>, And<POOrderReceipt.receiptNbr, In<Required<POOrderReceipt.receiptNbr>>>>, Aggregate<GroupBy<PX.Objects.PO.POOrder.orderType, GroupBy<PX.Objects.PO.POOrder.orderNbr>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[2]
              {
                (object) list[0].ReceiptType,
                (object) list.Select<PX.Objects.PO.POReceipt, string>((Func<PX.Objects.PO.POReceipt, string>) (r => r.ReceiptNbr)).ToArray<string>()
              })).ToList<PX.Objects.PO.POOrder>());
            }
          }
        }
      }
    }
    ((PXSelectBase) this.poreceiptslist).View.Clear();
    ((PXSelectBase) this.poreceiptslist).Cache.Clear();
    return adapter.Get();
  }

  protected virtual void APInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.AP.APInvoice row))
      return;
    APInvoiceState documentState = ((PXGraphExtension<APInvoiceEntry>) this).Base.GetDocumentState(cache, row);
    ((PXAction) this.addPOReceipt).SetVisible(documentState.IsDocumentInvoice || documentState.IsDocumentDebitAdjustment);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.poreceiptslist).Cache, (string) null, false);
    bool flag = documentState.IsDocumentEditable && !documentState.IsDocumentScheduled && ((PXSelectBase<PX.Objects.AP.Vendor>) ((PXGraphExtension<APInvoiceEntry>) this).Base.vendor).Current != null && !documentState.IsRetainageDebAdj;
    ((PXAction) this.addPOReceipt).SetEnabled(flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POReceipt.selected>(((PXSelectBase) this.poreceiptslist).Cache, (object) null, flag);
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (LocationIDAttribute), "Visible", false)]
  public virtual void POReceipt_VendorLocationID_CacheAttached(PXCache sender)
  {
  }

  public virtual IEnumerable pOreceiptslist()
  {
    AddPOReceiptExtension receiptExtension = this;
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) receiptExtension).Base.Document).Current;
    if ((current != null ? (!current.VendorID.HasValue ? 1 : 0) : 1) == 0 && current.VendorLocationID.HasValue && (!(current.DocType != "INV") || !(current.DocType != "ADR")))
    {
      string str = current.DocType == "INV" ? "RT" : "RN";
      Dictionary<PX.Objects.AP.APTran, int> usedReceipt = new Dictionary<PX.Objects.AP.APTran, int>((IEqualityComparer<PX.Objects.AP.APTran>) new POReceiptComparer());
      int num1;
      foreach (PX.Objects.AP.APTran key in ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptExtension).Base.Transactions).Cache.Inserted)
      {
        if (key.ReceiptNbr != null)
        {
          usedReceipt.TryGetValue(key, out num1);
          usedReceipt[key] = num1 + 1;
        }
      }
      foreach (PX.Objects.AP.APTran key in ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptExtension).Base.Transactions).Cache.Deleted)
      {
        if (key.ReceiptNbr != null && ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptExtension).Base.Transactions).Cache.GetStatus((object) key) != 4)
        {
          usedReceipt.TryGetValue(key, out num1);
          usedReceipt[key] = num1 - 1;
        }
      }
      foreach (PX.Objects.AP.APTran key1 in ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptExtension).Base.Transactions).Cache.Updated)
      {
        string valueOriginal = (string) ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptExtension).Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.receiptNbr>((object) key1);
        if (key1.ReceiptNbr != valueOriginal)
        {
          if (valueOriginal != null)
          {
            PX.Objects.AP.APTran key2 = new PX.Objects.AP.APTran()
            {
              ReceiptNbr = valueOriginal
            };
            usedReceipt.TryGetValue(key2, out num1);
            usedReceipt[key2] = num1 - 1;
          }
          if (key1.ReceiptNbr != null)
          {
            usedReceipt.TryGetValue(key1, out num1);
            usedReceipt[key1] = num1 + 1;
          }
        }
      }
      PXSelectBase<PX.Objects.PO.POReceipt> pxSelectBase = (PXSelectBase<PX.Objects.PO.POReceipt>) new PXSelectJoinGroupBy<PX.Objects.PO.POReceipt, InnerJoin<POReceiptLineS, On<POReceiptLineS.FK.Receipt>, LeftJoin<PX.Objects.AP.APTran, On<PX.Objects.AP.APTran.released, Equal<False>, And<PX.Objects.AP.APTran.pOAccrualType, Equal<POReceiptLineS.pOAccrualType>, And<PX.Objects.AP.APTran.pOAccrualRefNoteID, Equal<POReceiptLineS.pOAccrualRefNoteID>, And<PX.Objects.AP.APTran.pOAccrualLineNbr, Equal<POReceiptLineS.pOAccrualLineNbr>>>>>>>, Where<PX.Objects.PO.POReceipt.hold, Equal<False>, And<PX.Objects.PO.POReceipt.released, Equal<True>, And<PX.Objects.PO.POReceipt.receiptType, Equal<Required<PX.Objects.PO.POReceipt.receiptType>>, And<PX.Objects.AP.APTran.refNbr, IsNull, And<POReceiptLineS.unbilledQty, Greater<decimal0>, And2<Where<POReceiptLineS.pONbr, Equal<Current<POReceiptFilter.orderNbr>>, Or<Current<POReceiptFilter.orderNbr>, IsNull>>, And<PX.Objects.PO.POReceipt.canceled, Equal<False>, And<PX.Objects.PO.POReceipt.isUnderCorrection, Equal<False>>>>>>>>>, Aggregate<GroupBy<PX.Objects.PO.POReceipt.receiptType, GroupBy<PX.Objects.PO.POReceipt.receiptNbr, Sum<POReceiptLineS.receiptQty, Sum<POReceiptLineS.unbilledQty, Count<POReceiptLineS.lineNbr>>>>>>>((PXGraph) ((PXGraphExtension<APInvoiceEntry>) receiptExtension).Base);
      if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
      {
        pxSelectBase.Join<LeftJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderType, Equal<POReceiptLineS.pOType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<POReceiptLineS.pONbr>>>>>();
        pxSelectBase.WhereAnd<Where<PX.Objects.PO.POReceipt.vendorID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorID>>, And<PX.Objects.PO.POReceipt.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorLocationID>>, And<Where<PX.Objects.PO.POOrder.payToVendorID, IsNull, Or<PX.Objects.PO.POOrder.payToVendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>>>>>>>();
      }
      else
        pxSelectBase.WhereAnd<Where<PX.Objects.PO.POReceipt.vendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>, And<PX.Objects.PO.POReceipt.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.vendorLocationID>>>>>();
      PXView view = ((PXSelectBase) pxSelectBase).View;
      object[] objArray1 = new object[1]{ (object) current };
      object[] objArray2 = new object[1]{ (object) str };
      foreach (PXResult<PX.Objects.PO.POReceipt, POReceiptLineS, PX.Objects.AP.APTran> pxResult in view.SelectMultiBound(objArray1, objArray2))
      {
        PX.Objects.PO.POReceipt poReceipt = PXResult<PX.Objects.PO.POReceipt, POReceiptLineS, PX.Objects.AP.APTran>.op_Implicit(pxResult);
        PX.Objects.AP.APTran key = new PX.Objects.AP.APTran()
        {
          ReceiptNbr = poReceipt.ReceiptNbr
        };
        if (usedReceipt.TryGetValue(key, out num1))
        {
          usedReceipt.Remove(key);
          int num2 = num1;
          int? rowCount = ((PXResult) pxResult).RowCount;
          int valueOrDefault = rowCount.GetValueOrDefault();
          if (num2 < valueOrDefault & rowCount.HasValue)
            yield return (object) poReceipt;
        }
        else
          yield return (object) poReceipt;
      }
      foreach (PX.Objects.AP.APTran apTran in usedReceipt.Where<KeyValuePair<PX.Objects.AP.APTran, int>>((Func<KeyValuePair<PX.Objects.AP.APTran, int>, bool>) (_ => _.Value < 0)).Select<KeyValuePair<PX.Objects.AP.APTran, int>, PX.Objects.AP.APTran>((Func<KeyValuePair<PX.Objects.AP.APTran, int>, PX.Objects.AP.APTran>) (_ => _.Key)))
        yield return (object) GraphHelper.RowCast<PX.Objects.PO.POReceipt>((IEnumerable) PXSelectBase<PX.Objects.PO.POReceipt, PXSelect<PX.Objects.PO.POReceipt, Where<PX.Objects.PO.POReceipt.receiptNbr, Equal<Required<PX.Objects.AP.APTran.receiptNbr>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<APInvoiceEntry>) receiptExtension).Base, new object[0], new object[1]
        {
          (object) apTran.ReceiptNbr
        })).First<PX.Objects.PO.POReceipt>();
    }
  }

  public virtual void POReceipt_ReceiptNbr_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    PX.Objects.PO.POReceipt row = (PX.Objects.PO.POReceipt) e.Row;
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current;
    if (row == null || current == null)
      return;
    PXResultset<POOrderReceiptLink> source = PXSelectBase<POOrderReceiptLink, PXSelectGroupBy<POOrderReceiptLink, Where<POOrderReceiptLink.receiptType, Equal<Required<PX.Objects.PO.POReceipt.receiptType>>, And<POOrderReceiptLink.receiptNbr, Equal<Required<PX.Objects.PO.POReceipt.receiptNbr>>>>, Aggregate<GroupBy<POOrderReceiptLink.curyID>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[2]
    {
      (object) row.ReceiptType,
      (object) row.ReceiptNbr
    });
    if (source.Count == 0 || source.Count <= 1 && !(PXResult<POOrderReceiptLink>.op_Implicit(((IQueryable<PXResult<POOrderReceiptLink>>) source).First<PXResult<POOrderReceiptLink>>()).CuryID != current.CuryID))
      return;
    string name = typeof (PX.Objects.PO.POReceipt.curyID).Name;
    PXErrorLevel pxErrorLevel = (PXErrorLevel) 3;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(), new int?(), new int?(), new int?(), (object) null, name, (string) null, (string) null, "The currency of one or more purchase orders in the source document is different from the one of this document. The value may be recalculated or require correction.", pxErrorLevel, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    e.IsAltered = true;
  }
}
