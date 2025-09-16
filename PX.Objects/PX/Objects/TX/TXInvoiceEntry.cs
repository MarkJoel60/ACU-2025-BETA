// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TXInvoiceEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.AP;
using PX.Objects.AP.InvoiceRecognition;
using PX.Objects.AP.Standalone;
using PX.Objects.CM.Extensions;
using PX.Objects.CN.ProjectAccounting.AP.GraphExtensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.TX;

public class TXInvoiceEntry : APInvoiceEntry
{
  public PXFilter<AddBillFilter> BillFilter;
  public PXSelect<PX.Objects.AP.APInvoice> DocumentList;
  public PXAction<PX.Objects.AP.APInvoice> addInvoices;
  public PXAction<PX.Objects.AP.APInvoice> addInvoicesOK;

  [PXUIField]
  [PXButton(ImageKey = "Refresh")]
  public virtual IEnumerable AddInvoices(PXAdapter adapter)
  {
    if (((PXSelectBase) this.DocumentList).View.AskExt() == 1)
      this.AddInvoiceProc();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Add")]
  [PXButton]
  public virtual IEnumerable AddInvoicesOK(PXAdapter adapter)
  {
    this.AddInvoiceProc();
    return adapter.Get();
  }

  protected virtual void AddInvoiceProc()
  {
    foreach (PX.Objects.AP.APInvoice apInvoice in ((PXSelectBase) this.DocumentList).Cache.Updated)
    {
      try
      {
        if (apInvoice.Selected.GetValueOrDefault())
        {
          APTaxTran apTaxTran = new APTaxTran();
          apTaxTran.TranType = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Document).Current.DocType;
          apTaxTran.RefNbr = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Document).Current.RefNbr;
          apTaxTran.OrigTranType = apInvoice.DocType;
          apTaxTran.OrigRefNbr = apInvoice.RefNbr;
          apTaxTran.TaxID = ((PXSelectBase<AddBillFilter>) this.BillFilter).Current.TaxID;
          ((PXSelectBase<APTaxTran>) this.Taxes).Insert(apTaxTran);
        }
      }
      finally
      {
        ((PXSelectBase) this.DocumentList).Cache.SetStatus((object) apInvoice, (PXEntryStatus) 0);
        ((PXSelectBase) this.DocumentList).Cache.Remove((object) apInvoice);
      }
    }
  }

  public IEnumerable documentlist()
  {
    TXInvoiceEntry txInvoiceEntry = this;
    PXSelectBase<PX.Objects.AP.APInvoice> pxSelectBase = (PXSelectBase<PX.Objects.AP.APInvoice>) new PXSelectJoin<PX.Objects.AP.APInvoice, LeftJoin<APTaxTran, On<APTaxTran.tranType, Equal<PX.Objects.AP.APInvoice.docType>, And<APTaxTran.refNbr, Equal<PX.Objects.AP.APInvoice.refNbr>, And<APTaxTran.taxID, Equal<Current<AddBillFilter.taxID>>>>>>, Where<PX.Objects.AP.APInvoice.released, Equal<True>, And<PX.Objects.AP.APRegister.origModule, NotEqual<BatchModule.moduleTX>, And<APTaxTran.refNbr, IsNull, And<Where<PX.Objects.AP.APInvoice.docType, Equal<APDocType.invoice>, Or<PX.Objects.AP.APInvoice.docType, Equal<APDocType.debitAdj>, Or<PX.Objects.AP.APInvoice.docType, Equal<APDocType.creditAdj>>>>>>>>>((PXGraph) txInvoiceEntry);
    if (!string.IsNullOrEmpty(((PXSelectBase<AddBillFilter>) txInvoiceEntry.BillFilter).Current.TaxID))
    {
      DateTime? nullable = ((PXSelectBase<AddBillFilter>) txInvoiceEntry.BillFilter).Current.StartDate;
      if (nullable.HasValue)
        pxSelectBase.WhereAnd<Where<PX.Objects.AP.APInvoice.docDate, GreaterEqual<Current<AddBillFilter.startDate>>>>();
      nullable = ((PXSelectBase<AddBillFilter>) txInvoiceEntry.BillFilter).Current.EndDate;
      if (nullable.HasValue)
        pxSelectBase.WhereAnd<Where<PX.Objects.AP.APInvoice.docDate, LessEqual<Current<AddBillFilter.endDate>>>>();
      if (((PXSelectBase<AddBillFilter>) txInvoiceEntry.BillFilter).Current.VendorID.HasValue)
        pxSelectBase.WhereAnd<Where<PX.Objects.AP.APInvoice.vendorID, Equal<Current<AddBillFilter.vendorID>>>>();
      if (!string.IsNullOrEmpty(((PXSelectBase<AddBillFilter>) txInvoiceEntry.BillFilter).Current.InvoiceNbr))
        pxSelectBase.WhereAnd<Where<PX.Objects.AP.APInvoice.invoiceNbr, Equal<Current<AddBillFilter.invoiceNbr>>>>();
      foreach (PXResult<PX.Objects.AP.APInvoice> pxResult in pxSelectBase.Select(Array.Empty<object>()))
        yield return (object) PXResult<PX.Objects.AP.APInvoice>.op_Implicit(pxResult);
    }
  }

  public TXInvoiceEntry()
  {
    ((PXSelectBase) this.Document).View = new PXView((PXGraph) this, false, (BqlCommand) new Select2<PX.Objects.AP.APInvoice, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.APInvoice.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>, Where<PX.Objects.AP.APInvoice.docType, Equal<Optional<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.AP.APRegister.origModule, Equal<BatchModule.moduleTX>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>>>());
    ((PXGraph) this).Views["Document"] = ((PXSelectBase) this.Document).View;
    PXUIFieldAttribute.SetVisible<TaxRev.taxID>(((PXGraph) this).Caches[typeof (TaxRev)], (object) null);
    PXUIFieldAttribute.SetEnabled<TaxTran.origTranType>(((PXGraph) this).Caches[typeof (TaxTran)], (object) null, false);
    ((PXAction) this.prebook).SetEnabled(false);
    ((PXAction) this.prebook).SetVisible(false);
    ((PXAction) this.voidInvoice).SetVisible(false);
    ((PXAction) this.voidInvoice).SetEnabled(false);
    APInvoiceEntryExt extension1 = ((PXGraph) this).GetExtension<APInvoiceEntryExt>();
    if (extension1 != null)
    {
      ((PXAction) extension1.viewSourceDocument).SetVisible(false);
      ((PXAction) extension1.viewSourceDocument).SetEnabled(false);
    }
    APInvoiceEntryReclassifyingExt extension2 = ((PXGraph) this).GetExtension<APInvoiceEntryReclassifyingExt>();
    ((PXAction) extension2?.reclassify).SetVisible(false);
    ((PXAction) extension2?.reclassify).SetEnabled(false);
    TaxBaseAttribute.IncludeDirectTaxLine<APTran.taxCategoryID>(((PXSelectBase) this.AllTransactions).Cache, (object) null, false);
    ((OrderedDictionary) ((PXGraph) this).Actions).Remove((object) "ReassignApproval");
  }

  public virtual IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Document).Current;
    try
    {
      return ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
    }
    finally
    {
      if (viewName == "DocumentList")
        ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Document).Current = current;
    }
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Document).Current;
    try
    {
      return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
    }
    finally
    {
      if (viewName == "DocumentList")
        ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Document).Current = current;
    }
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [APInvoiceType.TaxInvoiceList]
  [PXDefault("INV")]
  [PXUIField]
  [PXFieldDescription]
  protected virtual void APInvoice_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [APInvoiceType.RefNbr(typeof (Search2<APRegisterAlias.refNbr, InnerJoinSingleTable<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<APRegisterAlias.docType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<APRegisterAlias.refNbr>>>, InnerJoin<PX.Objects.AP.Vendor, On<APRegisterAlias.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>, Where<APRegisterAlias.docType, Equal<Current<PX.Objects.AP.APInvoice.docType>>, And<APRegisterAlias.origModule, Equal<BatchModule.moduleTX>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>, OrderBy<Desc<APRegisterAlias.refNbr>>>), Filterable = true)]
  [APInvoiceType.Numbering]
  [PXFieldDescription]
  protected virtual void APInvoice_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXFormula(typeof (PX.Objects.AP.APInvoice.curyTaxTotal))]
  [PXDBCurrency(typeof (PX.Objects.AP.APRegister.curyInfoID), typeof (PX.Objects.AP.APRegister.docBal), BaseCalc = false)]
  [PXUIField]
  protected virtual void APInvoice_CuryDocBal_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsFixed = true, IsKey = true)]
  [PXUIField(DisplayName = "Orig. Tran. Type")]
  [APInvoiceType.TaxInvoiceList]
  [PXDefault("INV")]
  protected virtual void APTaxTran_OrigTranType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Orig. Doc. Number")]
  [PXSelector(typeof (Search<PX.Objects.AP.APInvoice.refNbr, Where<PX.Objects.AP.APInvoice.docType, Equal<APDocType.invoice>, And<PX.Objects.AP.APInvoice.released, Equal<True>, And<PX.Objects.AP.APRegister.origModule, NotEqual<BatchModule.moduleTX>>>>>))]
  [PXDefault]
  protected virtual void APTaxTran_OrigRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  [PXFormula(typeof (Selector<APTaxTran.origRefNbr, PX.Objects.AP.APInvoice.taxZoneID>))]
  [PXSelector(typeof (Search<TaxZone.taxZoneID>))]
  [PXUIField(DisplayName = "Tax Zone")]
  protected virtual void APTaxTran_TaxZoneID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search2<TaxRev.taxID, InnerJoin<Tax, On<Tax.taxID, Equal<TaxRev.taxID>>>, Where<Tax.directTax, Equal<True>, And<TaxRev.taxType, Equal<TaxType.purchase>, And<Current<PX.Objects.AP.APInvoice.docDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>), new Type[] {typeof (TaxRev.taxID), typeof (Tax.descr)})]
  protected virtual void APTaxTran_TaxID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal(6)]
  [PXFormula(typeof (Selector<APTaxTran.taxID, TaxRev.taxRate>))]
  [PXUIField]
  protected virtual void APTaxTran_TaxRate_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.taxableAmt))]
  [PXUIField(DisplayName = "Taxable Amount")]
  [PXFormula(typeof (Switch<Case<Where<APTaxTran.origRefNbr, IsNotNull>, CuryConvert<Selector<APTaxTran.origRefNbr, PX.Objects.AP.APInvoice.curyOrigDocAmt>, Selector<APTaxTran.origRefNbr, PX.Objects.AP.APInvoice.curyInfoID>, Parent<PX.Objects.AP.APInvoice.curyInfoID>>>, decimal0>))]
  protected virtual void APTaxTran_CuryTaxableAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.taxAmt))]
  [PXUIField(DisplayName = "Tax Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Mult<APTaxTran.curyTaxableAmt, Div<TaxTran.taxRate, decimal100>>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<APTaxTran.tranType, NotEqual<APDocType.debitAdj>, And<APTaxTran.origTranType, Equal<APDocType.debitAdj>>>, Minus<APTaxTran.curyTaxAmt>>, APTaxTran.curyTaxAmt>), typeof (SumCalc<PX.Objects.AP.APInvoice.curyTaxTotal>))]
  protected virtual void APTaxTran_CuryTaxAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  protected override void APTran_TaxCategoryID_CacheAttached(PXCache sender)
  {
  }

  protected override void APInvoice_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PX.Objects.AP.APInvoice row = (PX.Objects.AP.APInvoice) e.Row;
    if (row.OrigModule == "TX")
    {
      base.APInvoice_RowSelected(sender, e);
      PXAction<PX.Objects.AP.APInvoice> addInvoices = this.addInvoices;
      int? nullable = row.VendorID;
      int num;
      if (nullable.HasValue)
      {
        nullable = row.VendorLocationID;
        if (nullable.HasValue)
        {
          bool? released = row.Released;
          bool flag1 = false;
          if (released.GetValueOrDefault() == flag1 & released.HasValue)
          {
            bool? voided = row.Voided;
            bool flag2 = false;
            num = voided.GetValueOrDefault() == flag2 & voided.HasValue ? 1 : 0;
            goto label_7;
          }
        }
      }
      num = 0;
label_7:
      ((PXAction) addInvoices).SetEnabled(num != 0);
      ((PXAction) this.prebook).SetEnabled(false);
      ((PXAction) this.prebook).SetVisible(false);
      ((PXAction) this.voidInvoice).SetVisible(false);
      ((PXAction) this.voidInvoice).SetEnabled(false);
      APInvoiceEntryExt extension = ((PXGraph) this).GetExtension<APInvoiceEntryExt>();
      if (extension != null)
      {
        ((PXAction) extension.viewSourceDocument).SetVisible(false);
        ((PXAction) extension.viewSourceDocument).SetEnabled(false);
      }
    }
    else
    {
      PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.DocumentList).Cache, e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APInvoice.selected>(((PXSelectBase) this.DocumentList).Cache, e.Row, true);
    }
    ((PXSelectBase) this.Transactions).Cache.AllowInsert = false;
    ((PXSelectBase) this.Transactions).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Transactions).Cache.AllowDelete = false;
  }

  protected virtual void APTaxTran_TaxID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<APTaxTran.vendorID>(e.Row);
    sender.SetDefaultExt<APTaxTran.taxType>(e.Row);
    sender.SetDefaultExt<APTaxTran.taxBucketID>(e.Row);
    sender.SetDefaultExt<APTaxTran.accountID>(e.Row);
    sender.SetDefaultExt<APTaxTran.subID>(e.Row);
    APTaxTran row = (APTaxTran) e.Row;
    if (PXResultset<APTaxTran>.op_Implicit(PXSelectBase<APTaxTran, PXSelect<APTaxTran, Where<APTaxTran.tranType, Equal<Required<APTaxTran.tranType>>, And<APTaxTran.refNbr, Equal<Required<APTaxTran.refNbr>>, And<APTaxTran.taxID, Equal<Required<APTaxTran.taxID>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) row.OrigTranType,
      (object) row.OrigRefNbr,
      (object) row.TaxID
    })) == null)
      return;
    sender.RaiseExceptionHandling<APTaxTran.taxID>(e.Row, (object) null, (Exception) new PXSetPropertyException("Original document already contains tax record.", (PXErrorLevel) 2));
    sender.SetValueExt<APTaxTran.curyTaxableAmt>(e.Row, (object) 0M);
  }

  protected override void APTaxTran_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
  }

  protected override void APTaxTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    APTaxTran row = (APTaxTran) e.Row;
    row.CuryTaxAmtSumm = row.CuryTaxAmt;
    if (e.Operation != 2 && e.Operation != 1 || !string.IsNullOrEmpty(row.TaxZoneID))
      return;
    sender.RaiseExceptionHandling<APTaxTran.taxZoneID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
  }

  protected virtual void APInvoice_OrigModule_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "TX";
    ((CancelEventArgs) e).Cancel = true;
  }
}
