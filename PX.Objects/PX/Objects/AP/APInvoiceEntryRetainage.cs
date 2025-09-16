// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceEntryRetainage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

[Serializable]
public class APInvoiceEntryRetainage : PXGraphExtension<APInvoiceEntry>
{
  [PXReadOnlyView]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<APRetainageInvoice, InnerJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APRetainageInvoice.docType>, And<APInvoice.refNbr, Equal<APRetainageInvoice.refNbr>>>>, Where<APRetainageInvoice.isRetainageDocument, Equal<True>, And<APRetainageInvoice.origDocType, Equal<Optional<APInvoice.docType>>, And<APRetainageInvoice.origRefNbr, Equal<Optional<APInvoice.refNbr>>>>>> RetainageDocuments;
  public PXAction<APInvoice> releaseRetainage;
  public PXAction<APInvoice> ViewRetainageDocument;

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>();

  public override void Initialize() => base.Initialize();

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [APRetainedTax(typeof (APRegister), typeof (APTax), typeof (APTaxTran), typeof (APInvoice.taxCalcMode), typeof (APRegister.branchID))]
  protected virtual void APTran_TaxCategoryID_CacheAttached(PXCache sender)
  {
  }

  [DBRetainagePercent(typeof (APInvoice.retainageApply), typeof (APRegister.defRetainagePct), typeof (Sub<Current<APTran.curyLineAmt>, Current<APTran.curyDiscAmt>>), typeof (APTran.curyRetainageAmt), typeof (APTran.retainagePct))]
  protected virtual void APTran_RetainagePct_CacheAttached(PXCache sender)
  {
  }

  [DBRetainageAmount(typeof (APTran.curyInfoID), typeof (Switch<Case<Where<APTran.tranType, Equal<APDocType.prepayment>>, APTran.curyPrepaymentAmt>, Sub<APTran.curyLineAmt, APTran.curyDiscAmt>>), typeof (APTran.curyRetainageAmt), typeof (APTran.retainageAmt), typeof (APTran.retainagePct))]
  protected virtual void APTran_CuryRetainageAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (Selector<APRegister.vendorID, Vendor.retainagePct>))]
  protected virtual void APInvoice_DefRetainagePct_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (APRegister.curyRetainageTotal))]
  protected virtual void APInvoice_CuryRetainageUnreleasedAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (Switch<Case<Where2<FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>, And<APRegister.retainageApply, Equal<True>, And<APRegister.released, NotEqual<True>>>>, APRegister.curyRetainageTotal>, APRegister.curyRetainageUnpaidTotal>))]
  protected virtual void APInvoice_CuryRetainageUnpaidTotal_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (IIf<Where2<FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>, And2<Where<APRegister.docType, Equal<APDocType.invoice>, Or<APRegister.docType, Equal<APDocType.debitAdj>>>, And<APRegister.origModule, Equal<BatchModule.moduleAP>, And<Current<APSetup.migrationMode>, Equal<False>>>>>, IsNull<Selector<APRegister.vendorID, Vendor.retainageApply>, False>, False>))]
  [PXUIVerify(typeof (Where<APRegister.retainageApply, NotEqual<True>, And<APRegister.isRetainageDocument, NotEqual<True>, Or<Selector<APInvoice.termsID, PX.Objects.CS.Terms.installmentType>, NotEqual<TermsInstallmentType.multiple>>>>), PXErrorLevel.Error, "The document cannot be processed because retainage cannot be applied to documents with the multiple installment credit terms specified.", new System.Type[] {})]
  [PXUIVerify(typeof (Where<APRegister.retainageApply, NotEqual<True>, Or<APRegister.curyID, Equal<Current<PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID>>>>), PXErrorLevel.Error, "Retainage cannot be applied to the documents in a foreign currency.", new System.Type[] {})]
  protected virtual void APInvoice_RetainageApply_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIVerify(typeof (Where<APRegister.curyRetainageTotal, GreaterEqual<decimal0>, And<APRegister.hold, NotEqual<True>, Or<APRegister.hold, Equal<True>>>>), PXErrorLevel.Error, "An original retainage amount cannot be negative.", new System.Type[] {})]
  protected virtual void APInvoice_CuryRetainageTotal_CacheAttached(PXCache sender)
  {
  }

  [PXOverride]
  public bool IsInvoiceNbrRequired(
    APInvoice doc,
    APInvoiceEntryRetainage.IsInvoiceNbrRequiredDelegate baseMethod)
  {
    return !doc.IsChildRetainageDocument() && baseMethod(doc);
  }

  protected virtual void APInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is APInvoice row))
      return;
    bool? nullable = row.Released;
    int num1;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.Prebooked;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 1;
    nullable = row.Voided;
    bool valueOrDefault = nullable.GetValueOrDefault();
    this.releaseRetainage.SetEnabled(false);
    int num2 = valueOrDefault ? 1 : 0;
    if ((num1 | num2) == 0)
      return;
    PXAction<APInvoice> releaseRetainage = this.releaseRetainage;
    nullable = row.Released;
    int num3;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.RetainageApply;
      if (nullable.GetValueOrDefault())
      {
        num3 = !row.HasZeroBalance<APRegister.curyRetainageUnreleasedAmt, APTran.curyRetainageBal>(cache.Graph) ? 1 : 0;
        goto label_10;
      }
    }
    num3 = 0;
label_10:
    releaseRetainage.SetEnabled(num3 != 0);
  }

  protected virtual void APInvoice_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!(e.Row is APInvoice row))
      return;
    bool? nullable1 = row.RetainageApply;
    if (!nullable1.GetValueOrDefault())
      return;
    nullable1 = row.Released;
    if (!nullable1.GetValueOrDefault())
      return;
    using (new PXConnectionScope())
    {
      APRetainageInvoice document = new APRetainageInvoice();
      document.CuryRetainageUnpaidTotal = new Decimal?(0M);
      document.CuryRetainagePaidTotal = new Decimal?(0M);
      foreach (APRetainageInvoice retainageInvoice1 in this.RetainageDocuments.Select((object) row.DocType, (object) row.RefNbr).RowCast<APRetainageInvoice>().Where<APRetainageInvoice>((Func<APRetainageInvoice, bool>) (res => res.Released.GetValueOrDefault())))
      {
        document.DocType = retainageInvoice1.DocType;
        document.RefNbr = retainageInvoice1.RefNbr;
        document.CuryOrigDocAmt = retainageInvoice1.CuryOrigDocAmt;
        document.CuryDocBal = retainageInvoice1.CuryOrigDocAmt;
        foreach (PXResult<APAdjust> pxResult in PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.released, Equal<True>, And<APAdjust.voided, NotEqual<True>, And<APAdjust.adjgDocType, NotEqual<APDocType.debitAdj>>>>>>>.Config>.Select((PXGraph) this.Base, (object) retainageInvoice1.DocType, (object) retainageInvoice1.RefNbr))
        {
          APAdjust application = (APAdjust) pxResult;
          document.AdjustBalance<APRetainageInvoice, APAdjust>(application);
        }
        Decimal? nullable2 = row.SignAmount;
        Decimal? signAmount = retainageInvoice1.SignAmount;
        Decimal valueOrDefault = (nullable2.HasValue & signAmount.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signAmount.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
        APRetainageInvoice retainageInvoice2 = document;
        nullable2 = retainageInvoice2.CuryRetainageUnpaidTotal;
        Decimal? docBal = retainageInvoice1.DocBal;
        Decimal num1 = valueOrDefault;
        Decimal? nullable3 = docBal.HasValue ? new Decimal?(docBal.GetValueOrDefault() * num1) : new Decimal?();
        retainageInvoice2.CuryRetainageUnpaidTotal = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        APRetainageInvoice retainageInvoice3 = document;
        nullable3 = retainageInvoice3.CuryRetainagePaidTotal;
        Decimal? curyOrigDocAmt = document.CuryOrigDocAmt;
        Decimal? curyDocBal = document.CuryDocBal;
        Decimal? nullable4 = curyOrigDocAmt.HasValue & curyDocBal.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() - curyDocBal.GetValueOrDefault()) : new Decimal?();
        Decimal num2 = valueOrDefault;
        nullable2 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * num2) : new Decimal?();
        Decimal? nullable5;
        if (!(nullable3.HasValue & nullable2.HasValue))
        {
          nullable4 = new Decimal?();
          nullable5 = nullable4;
        }
        else
          nullable5 = new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault());
        retainageInvoice3.CuryRetainagePaidTotal = nullable5;
      }
      APInvoice apInvoice = row;
      Decimal? retainageUnreleasedAmt = row.CuryRetainageUnreleasedAmt;
      Decimal? retainageUnpaidTotal = document.CuryRetainageUnpaidTotal;
      Decimal? nullable6 = retainageUnreleasedAmt.HasValue & retainageUnpaidTotal.HasValue ? new Decimal?(retainageUnreleasedAmt.GetValueOrDefault() + retainageUnpaidTotal.GetValueOrDefault()) : new Decimal?();
      apInvoice.CuryRetainageUnpaidTotal = nullable6;
      row.CuryRetainagePaidTotal = document.CuryRetainagePaidTotal;
    }
  }

  protected virtual void APInvoice_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    APInvoice row = (APInvoice) e.Row;
    PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) PXSelectorAttribute.Select<APInvoice.termsID>(this.Base.Document.Cache, (object) row);
    bool? retainageApply;
    if (terms != null)
    {
      retainageApply = row.RetainageApply;
      if (retainageApply.GetValueOrDefault() && terms.InstallmentType == "M")
        sender.RaiseExceptionHandling<APInvoice.termsID>((object) row, (object) row.TermsID, (Exception) new PXSetPropertyException("The document cannot be processed because retainage cannot be applied to documents with the multiple installment credit terms specified."));
    }
    retainageApply = row.RetainageApply;
    bool flag = !retainageApply.GetValueOrDefault();
    PXDefaultAttribute.SetPersistingCheck<APRegister.retainageAcctID>(sender, (object) row, flag ? PXPersistingCheck.Nothing : PXPersistingCheck.NullOrBlank);
    PXDefaultAttribute.SetPersistingCheck<APRegister.retainageSubID>(sender, (object) row, flag ? PXPersistingCheck.Nothing : PXPersistingCheck.NullOrBlank);
  }

  protected virtual void APInvoice_RetainageAcctID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(this.Base.location.View.SelectSingleBound(new object[1]
    {
      e.Row
    }) is PX.Objects.CR.Location data))
      return;
    e.NewValue = this.Base.GetAcctSub<PX.Objects.CR.Location.aPRetainageAcctID>(this.Base.location.Cache, (object) data);
  }

  protected virtual void APInvoice_RetainageSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    if (!(this.Base.location.View.SelectSingleBound(new object[1]
    {
      e.Row
    }) is PX.Objects.CR.Location data) || e.Row == null)
      return;
    e.NewValue = this.Base.GetAcctSub<PX.Objects.CR.Location.aPRetainageSubID>(this.Base.location.Cache, (object) data);
  }

  protected virtual void APInvoice_VendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<APRegister.retainageAcctID>(e.Row);
    sender.SetDefaultExt<APRegister.retainageSubID>(e.Row);
  }

  protected virtual void APInvoice_RetainageApply_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    APInvoice row = (APInvoice) e.Row;
    bool? newValue = (bool?) e.NewValue;
    if (row == null)
      return;
    bool? nullable = row.RetainageApply;
    if (nullable.GetValueOrDefault())
    {
      nullable = newValue;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        IEnumerable<APTran> source = this.Base.Transactions.Select().AsEnumerable<PXResult<APTran>>().Where<PXResult<APTran>>((Func<PXResult<APTran>, bool>) (tran =>
        {
          Decimal? curyRetainageAmt = ((APTran) tran).CuryRetainageAmt;
          Decimal num1 = 0M;
          if (!(curyRetainageAmt.GetValueOrDefault() == num1 & curyRetainageAmt.HasValue))
            return true;
          Decimal? retainagePct = ((APTran) tran).RetainagePct;
          Decimal num2 = 0M;
          return !(retainagePct.GetValueOrDefault() == num2 & retainagePct.HasValue);
        })).RowCast<APTran>();
        if (!source.Any<APTran>())
          return;
        if (this.Base.Document.Ask("Warning", "If you clear the Apply Retainage check box, the retainage amount and retainage percent will be set to zero. Do you want to proceed?", MessageButtons.YesNo, MessageIcon.Warning) == WebDialogResult.Yes)
        {
          using (IEnumerator<APTran> enumerator = source.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              APTran current = enumerator.Current;
              current.CuryRetainageAmt = new Decimal?(0M);
              current.RetainagePct = new Decimal?(0M);
              this.Base.Transactions.Update(current);
            }
            return;
          }
        }
        e.Cancel = true;
        e.NewValue = (object) true;
        return;
      }
    }
    nullable = row.RetainageApply;
    if (nullable.GetValueOrDefault() || !newValue.GetValueOrDefault())
      return;
    this.ClearCurrentDocumentDiscountDetails();
  }

  protected virtual void APTran_SubID_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
  }

  protected virtual void ClearCurrentDocumentDiscountDetails()
  {
    EnumerableExtensions.ForEach<APInvoiceDiscountDetail>(this.Base.DiscountDetails.Select().RowCast<APInvoiceDiscountDetail>(), (System.Action<APInvoiceDiscountDetail>) (discountDetail => this.Base.DiscountDetails.Cache.Delete((object) discountDetail)));
    EnumerableExtensions.ForEach<APTran>(this.Base.Discount_Row.Select().RowCast<APTran>(), (System.Action<APTran>) (tran => this.Base.Discount_Row.Cache.Delete((object) tran)));
  }

  [PXOverride]
  public virtual void APInvoiceDiscountDetail_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e)
  {
    APInvoice current = this.Base.Document.Current;
    if ((current == null || !current.RetainageApply.GetValueOrDefault()) && (current == null || !current.IsRetainageDocument.GetValueOrDefault()))
      return;
    e.Cancel = true;
  }

  [PXOverride]
  public void AddDiscount(
    PXCache sender,
    APInvoice row,
    APInvoiceEntryRetainage.AddDiscountDelegate baseMethod)
  {
    if ((row.RetainageApply.GetValueOrDefault() ? 1 : (row.IsRetainageDocument.GetValueOrDefault() ? 1 : 0)) != 0)
      return;
    baseMethod(sender, row);
  }

  [PXUIField(DisplayName = "Release Retainage", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable ReleaseRetainage(PXAdapter adapter)
  {
    APInvoice current1 = this.Base.Document.Current;
    APRegister reversingDoc;
    if (this.Base.CheckReversingRetainageDocumentAlreadyExists(this.Base.Document.Current, out reversingDoc))
      throw new PXException("The retainage {0} cannot be released because the reversing document {1} {2} exists in the system.", new object[3]
      {
        (object) PXMessages.LocalizeNoPrefix(APInvoiceEntry.APDocTypeDict[current1.DocType]),
        (object) PXMessages.LocalizeNoPrefix(APInvoiceEntry.APDocTypeDict[reversingDoc.DocType]),
        (object) reversingDoc.RefNbr
      });
    this.Base.Save.Press();
    APRetainageRelease instance = PXGraph.CreateInstance<APRetainageRelease>();
    APRetainageFilter current2 = instance.Filter.Current;
    System.DateTime? businessDate = this.Base.Accessinfo.BusinessDate;
    System.DateTime? docDate = current1.DocDate;
    System.DateTime? nullable = (businessDate.HasValue & docDate.HasValue ? (businessDate.GetValueOrDefault() > docDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? this.Base.Accessinfo.BusinessDate : current1.DocDate;
    current2.DocDate = nullable;
    instance.Filter.Current.FinPeriodID = this.Base.FinPeriodRepository.GetPeriodIDFromDate(instance.Filter.Current.DocDate, new int?(0));
    instance.Filter.Current.BranchID = current1.BranchID;
    instance.Filter.Current.OrgBAccountID = new int?(PXAccess.GetBranch(current1.BranchID).BAccountID);
    instance.Filter.Current.VendorID = current1.VendorID;
    instance.Filter.Current.RefNbr = current1.RefNbr;
    instance.Filter.Current.ShowBillsWithOpenBalance = new bool?(current1.OpenDoc.GetValueOrDefault());
    if (instance.DocumentList.SelectSingle() == null)
    {
      APRetainageInvoice retainageInvoice = this.RetainageDocuments.Select().RowCast<APRetainageInvoice>().FirstOrDefault<APRetainageInvoice>((Func<APRetainageInvoice, bool>) (row => !row.Released.GetValueOrDefault()));
      throw new PXException("The retainage cannot be released because {0} {1} associated with this {2} has not been released yet. To proceed, delete or release the retainage document.", new object[3]
      {
        (object) PXMessages.LocalizeNoPrefix(APInvoiceEntry.APDocTypeDict[retainageInvoice.DocType]),
        (object) retainageInvoice.RefNbr,
        (object) PXMessages.LocalizeNoPrefix(APInvoiceEntry.APDocTypeDict[current1.DocType])
      });
    }
    throw new PXRedirectRequiredException((PXGraph) instance, nameof (ReleaseRetainage));
  }

  public virtual void ReleaseRetainageProc(
    List<APInvoiceExt> list,
    RetainageOptions retainageOpts,
    bool isAutoRelease = false)
  {
    this.Base.FieldDefaulting.AddHandler<APTran.subID>((PXFieldDefaulting) ((sender, e) => e.Cancel = true));
    bool flag1 = false;
    List<APInvoice> apInvoiceList = new List<APInvoice>();
    foreach (IGrouping<\u003C\u003Ef__AnonymousType0<string, string>, APInvoiceExt> source in list.GroupBy(row => new
    {
      DocType = row.DocType,
      RefNbr = row.RefNbr
    }))
    {
      APInvoiceExt currentItem1 = source.First<APInvoiceExt>();
      PXProcessing<APInvoiceExt>.SetCurrentItem((object) currentItem1);
      Decimal num1 = source.Sum<APInvoiceExt>((Func<APInvoiceExt, Decimal>) (row => row.CuryRetainageReleasedAmt.GetValueOrDefault()));
      try
      {
        this.Base.Clear(PXClearOption.ClearAll);
        PXUIFieldAttribute.SetError(this.Base.Document.Cache, (object) null, (string) null, (string) null);
        APTran tran = (APTran) null;
        TaxCalc taxCalc = TaxBaseAttribute.GetTaxCalc<APTran.taxCategoryID>(this.Base.Transactions.Cache, (object) null);
        this.Base.Clear(PXClearOption.PreserveTimeStamp);
        this.Base.CurrentDocument.Cache.AllowUpdate = true;
        PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Vendor> pxResult1 = PXSelectBase<APInvoice, PXSelectJoin<APInvoice, LeftJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>, LeftJoin<PX.Objects.CS.Terms, On<PX.Objects.CS.Terms.termsID, Equal<APInvoice.termsID>>, LeftJoin<Vendor, On<Vendor.bAccountID, Equal<APInvoice.vendorID>>>>>, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) currentItem1.DocType, (object) currentItem1.RefNbr, (object) currentItem1.VendorID).AsEnumerable<PXResult<APInvoice>>().Cast<PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Vendor>>().First<PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Vendor>>();
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) pxResult1;
        APInvoice apInvoice1 = (APInvoice) pxResult1;
        Vendor vendor = (Vendor) pxResult1;
        PX.Objects.CM.Extensions.CurrencyInfo copy1 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo1);
        copy1.CuryInfoID = new long?();
        copy1.IsReadOnly = new bool?(false);
        PX.Objects.CM.Extensions.CurrencyInfo copy2 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(this.Base.currencyinfo.Insert(copy1));
        APInvoice copy3 = PXCache<APInvoice>.CreateCopy(apInvoice1);
        object newValue = this.Base.CurrentDocument.Cache.GetValue<APRegister.employeeID>((object) apInvoice1);
        try
        {
          this.Base.CurrentDocument.Cache.RaiseFieldVerifying<APRegister.employeeID>((object) apInvoice1, ref newValue);
        }
        catch (Exception ex)
        {
          copy3.EmployeeID = new int?();
        }
        copy3.CuryInfoID = copy2.CuryInfoID;
        if (num1 != 0M)
          copy3.DocType = num1 < 0M ? "ADR" : "INV";
        copy3.RefNbr = (string) null;
        copy3.LineCntr = new int?();
        copy3.InvoiceNbr = apInvoice1.InvoiceNbr;
        copy3.OpenDoc = new bool?(true);
        copy3.Released = new bool?(false);
        this.Base.Document.Cache.SetDefaultExt<APInvoice.isMigratedRecord>((object) copy3);
        copy3.BatchNbr = (string) null;
        copy3.PrebookBatchNbr = (string) null;
        copy3.Prebooked = new bool?(false);
        copy3.ScheduleID = (string) null;
        copy3.Scheduled = new bool?(false);
        copy3.NoteID = new Guid?();
        if (copy3.DocType == "ADR")
          copy3.TermsID = (string) null;
        copy3.DueDate = new System.DateTime?();
        copy3.DiscDate = new System.DateTime?();
        copy3.PayDate = new System.DateTime?();
        copy3.CuryOrigDiscAmt = new Decimal?(0M);
        copy3.OrigDocType = apInvoice1.DocType;
        copy3.OrigRefNbr = apInvoice1.RefNbr;
        copy3.OrigDocDate = apInvoice1.DocDate;
        copy3.PaySel = new bool?(false);
        copy3.CuryLineTotal = new Decimal?(0M);
        copy3.IsTaxPosted = new bool?(false);
        copy3.IsTaxValid = new bool?(false);
        copy3.CuryVatTaxableTotal = new Decimal?(0M);
        copy3.CuryVatExemptTotal = new Decimal?(0M);
        copy3.CuryTaxRoundDiff = new Decimal?(0M);
        copy3.CuryRoundDiff = new Decimal?(0M);
        copy3.CuryDetailExtPriceTotal = new Decimal?(0M);
        copy3.DetailExtPriceTotal = new Decimal?(0M);
        copy3.CuryLineDiscTotal = new Decimal?(0M);
        copy3.LineDiscTotal = new Decimal?(0M);
        copy3.CuryDocBal = new Decimal?(0M);
        copy3.CuryOrigDocAmt = new Decimal?(System.Math.Abs(num1));
        bool? nullable1 = copy3.IsMigratedRecord;
        if (nullable1.GetValueOrDefault())
          copy3.CuryInitDocBal = copy3.CuryOrigDocAmt;
        APInvoice apInvoice2 = copy3;
        int num2;
        if (!isAutoRelease)
        {
          nullable1 = this.Base.apsetup.Current.HoldEntry;
          if (nullable1.GetValueOrDefault())
          {
            num2 = 1;
            goto label_16;
          }
        }
        num2 = this.Base.IsApprovalRequired(copy3) ? 1 : 0;
label_16:
        bool? nullable2 = new bool?(num2 != 0);
        apInvoice2.Hold = nullable2;
        copy3.DocDate = retainageOpts.DocDate;
        FinPeriodIDAttribute.SetPeriodsByMaster<APInvoice.finPeriodID>(this.Base.Document.Cache, (object) copy3, retainageOpts.MasterFinPeriodID);
        this.Base.ClearRetainageSummary(copy3);
        copy3.RetainageApply = new bool?(false);
        copy3.IsRetainageDocument = new bool?(true);
        APInvoice apInvoice3 = this.Base.Document.Insert(copy3);
        Decimal? nullable3 = currentItem1.SignAmount;
        Decimal? signAmount1 = apInvoice3.SignAmount;
        Decimal valueOrDefault1 = (nullable3.HasValue & signAmount1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * signAmount1.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
        if (copy2 != null)
        {
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<APInvoice.curyInfoID>>>>.Config>.Select((PXGraph) this.Base);
          currencyInfo2.CuryID = copy2.CuryID;
          currencyInfo2.CuryEffDate = copy2.CuryEffDate;
          currencyInfo2.CuryRateTypeID = copy2.CuryRateTypeID;
          currencyInfo2.CuryRate = copy2.CuryRate;
          currencyInfo2.RecipRate = copy2.RecipRate;
          currencyInfo2.CuryMultDiv = copy2.CuryMultDiv;
          this.Base.currencyinfo.Update(currencyInfo2);
        }
        int? lineNbr1 = currentItem1.LineNbr;
        int num3 = 0;
        bool flag2 = !(lineNbr1.GetValueOrDefault() == num3 & lineNbr1.HasValue);
        int num4;
        if (!flag2)
        {
          nullable3 = currentItem1.CuryRetainageUnreleasedCalcAmt;
          Decimal num5 = 0M;
          num4 = nullable3.GetValueOrDefault() == num5 & nullable3.HasValue ? 1 : 0;
        }
        else
          num4 = 0;
        bool flag3 = num4 != 0;
        Dictionary<(string, string, int?), APInvoiceEntryRetainage.APTranRetainageData> dictionary1 = new Dictionary<(string, string, int?), APInvoiceEntryRetainage.APTranRetainageData>();
        foreach (APInvoiceExt currentItem2 in (IEnumerable<APInvoiceExt>) source)
        {
          PXProcessing<APInvoiceExt>.SetCurrentItem((object) currentItem2);
          PXResultset<APTran> pxResultset1;
          if (!flag2)
            pxResultset1 = PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>, And<APTran.curyRetainageAmt, NotEqual<decimal0>>>>, OrderBy<Asc<APTran.taxCategoryID, Asc<APTran.projectID, Asc<APTran.taskID, Asc<APTran.costCodeID, Asc<APTran.accountID, Asc<APTran.pONbr, Asc<APTran.pOLineNbr>>>>>>>>>.Config>.Select((PXGraph) this.Base, (object) currentItem2.DocType, (object) currentItem2.RefNbr);
          else
            pxResultset1 = PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>, And<APTran.lineNbr, Equal<Required<APTran.lineNbr>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) currentItem2.DocType, (object) currentItem2.RefNbr, (object) currentItem2.LineNbr);
          TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Base.Transactions.Cache, (object) null, TaxCalc.ManualCalc);
          foreach (PXResult<APTran> pxResult2 in pxResultset1)
          {
            APTran detail = (APTran) pxResult2;
            APTran apTran1 = new APTran()
            {
              CuryUnitCost = new Decimal?(0M),
              CuryLineAmt = new Decimal?(0M),
              BranchID = apInvoice1.BranchID,
              AccountID = apInvoice1.RetainageAcctID,
              SubID = apInvoice1.RetainageSubID,
              ProjectID = ProjectDefaultAttribute.NonProject()
            };
            if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.projectAccounting>())
              apTran1.ProjectID = detail.ProjectID ?? apInvoice1.ProjectID;
            APTran tranNew = this.Base.Transactions.Insert(apTran1);
            tranNew.TaxCategoryID = detail.TaxCategoryID;
            this.CopyExtensionValues(detail, tranNew);
            tranNew.Box1099 = detail.Box1099;
            tranNew.Qty = new Decimal?(0M);
            tranNew.CuryUnitCost = new Decimal?(0M);
            tranNew.ManualDisc = new bool?(true);
            tranNew.DiscPct = new Decimal?(0M);
            tranNew.CuryDiscAmt = new Decimal?(0M);
            tranNew.RetainagePct = new Decimal?(0M);
            tranNew.CuryRetainageAmt = new Decimal?(0M);
            tranNew.CuryTaxableAmt = new Decimal?(0M);
            tranNew.CuryTaxAmt = new Decimal?(0M);
            tranNew.CuryExpenseAmt = new Decimal?(0M);
            tranNew.GroupDiscountRate = new Decimal?(1M);
            tranNew.DocumentDiscountRate = new Decimal?(1M);
            tranNew.OrigLineNbr = detail.LineNbr;
            using (new PXLocaleScope(vendor.LocaleName))
              tranNew.TranDesc = PXMessages.LocalizeFormatNoPrefix("Retainage for {0} {1}", (object) APInvoiceEntry.APDocTypeDict[apInvoice1.DocType], (object) apInvoice1.RefNbr);
            Decimal? unreleasedCalcAmt = currentItem2.CuryRetainageUnreleasedCalcAmt;
            Decimal num6 = 0M;
            bool flag4 = unreleasedCalcAmt.GetValueOrDefault() == num6 & unreleasedCalcAmt.HasValue;
            Decimal num7;
            Decimal? nullable4;
            if (flag4)
            {
              PXResultset<APTran> pxResultset2 = PXSelectBase<APTran, PXSelectJoin<APTran, InnerJoin<APRegister, On<APRegister.docType, Equal<APTran.tranType>, And<APRegister.refNbr, Equal<APTran.refNbr>>>>, Where<APRegister.isRetainageDocument, Equal<True>, And<APRegister.released, Equal<True>, And<APRegister.origDocType, Equal<Required<APRegister.origDocType>>, And<APRegister.origRefNbr, Equal<Required<APRegister.origRefNbr>>, And<APTran.origLineNbr, Equal<Required<APTran.origLineNbr>>>>>>>>.Config>.Select((PXGraph) this.Base, (object) currentItem2.DocType, (object) currentItem2.RefNbr, (object) (flag2 ? currentItem2.LineNbr : detail.LineNbr));
              Decimal num8 = 0M;
              foreach (PXResult<APTran, APRegister> pxResult3 in pxResultset2)
              {
                APTran apTran2 = (APTran) pxResult3;
                APRegister apRegister = (APRegister) pxResult3;
                Decimal? nullable5 = currentItem1.SignAmount;
                Decimal? signAmount2 = apRegister.SignAmount;
                Decimal valueOrDefault2 = (nullable5.HasValue & signAmount2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * signAmount2.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
                Decimal num9 = num8;
                nullable5 = apTran2.CuryTranAmt;
                Decimal num10 = nullable5.GetValueOrDefault() * valueOrDefault2;
                num8 = num9 + num10;
              }
              num7 = (detail.CuryRetainageAmt.GetValueOrDefault() - num8) * valueOrDefault1;
            }
            else
            {
              nullable4 = currentItem2.CuryRetainageReleasedAmt;
              Decimal? nullable6 = flag2 ? detail.CuryOrigRetainageAmt : currentItem1.CuryRetainageTotal;
              Decimal num11 = System.Math.Abs((nullable4.HasValue & nullable6.HasValue ? new Decimal?(nullable4.GetValueOrDefault() / nullable6.GetValueOrDefault()) : new Decimal?()).Value);
              IPXCurrencyHelper implementation = this.Base.FindImplementation<IPXCurrencyHelper>();
              nullable4 = detail.CuryRetainageAmt;
              Decimal val = nullable4.GetValueOrDefault() * num11 * valueOrDefault1;
              num7 = implementation.RoundCury(val);
            }
            tranNew.CuryLineAmt = new Decimal?(num7);
            APTran apTran3 = this.Base.Transactions.Update(tranNew);
            apTran3.IsStockItem = detail.IsStockItem;
            apTran3.InventoryID = detail.InventoryID;
            apTran3.ProjectID = detail.ProjectID;
            apTran3.TaskID = detail.TaskID;
            apTran3.CostCodeID = detail.CostCodeID;
            if (flag2)
            {
              Dictionary<(string, string, int?), APInvoiceEntryRetainage.APTranRetainageData> dictionary2 = dictionary1;
              (string, string, int?) key = (apTran3.TranType, apTran3.RefNbr, apTran3.LineNbr);
              APInvoiceEntryRetainage.APTranRetainageData tranRetainageData = new APInvoiceEntryRetainage.APTranRetainageData();
              tranRetainageData.Detail = apTran3;
              Decimal? retainageReleasedAmt = currentItem2.CuryRetainageReleasedAmt;
              Decimal? nullable7 = apInvoice3.SignAmount;
              nullable4 = retainageReleasedAmt.HasValue & nullable7.HasValue ? new Decimal?(retainageReleasedAmt.GetValueOrDefault() * nullable7.GetValueOrDefault()) : new Decimal?();
              Decimal? curyLineAmt = apTran3.CuryLineAmt;
              Decimal? nullable8;
              if (!(nullable4.HasValue & curyLineAmt.HasValue))
              {
                nullable7 = new Decimal?();
                nullable8 = nullable7;
              }
              else
                nullable8 = new Decimal?(nullable4.GetValueOrDefault() - curyLineAmt.GetValueOrDefault());
              tranRetainageData.RemainAmt = nullable8;
              tranRetainageData.IsFinal = flag4;
              dictionary2.Add(key, tranRetainageData);
            }
            else if (tran == null || System.Math.Abs(tran.CuryLineAmt.GetValueOrDefault()) < System.Math.Abs(apTran3.CuryLineAmt.GetValueOrDefault()))
              tran = apTran3;
          }
          PXProcessing<APInvoiceExt>.SetProcessed();
        }
        this.ClearCurrentDocumentDiscountDetails();
        PXResultset<APTaxTran> resultSet = PXSelectBase<APTaxTran, PXSelectJoin<APTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Required<APTaxTran.tranType>>, And<APTaxTran.refNbr, Equal<Required<APTaxTran.refNbr>>, And<APTaxTran.curyRetainedTaxAmt, NotEqual<decimal0>>>>>>.Config>.Select((PXGraph) this.Base, (object) source.Key.DocType, (object) source.Key.RefNbr);
        Dictionary<string, APTaxTran> insertedTaxes = (Dictionary<string, APTaxTran>) null;
        insertedTaxes = new Dictionary<string, APTaxTran>();
        EnumerableExtensions.ForEach<APTaxTran>(resultSet.RowCast<APTaxTran>(), (System.Action<APTaxTran>) (tax =>
        {
          Dictionary<string, APTaxTran> dictionary3 = insertedTaxes;
          string taxId = tax.TaxID;
          PXSelectJoin<APTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Current<APInvoice.docType>>, And<APTaxTran.refNbr, Equal<Current<APInvoice.refNbr>>>>>> taxes = this.Base.Taxes;
          APTaxTran apTaxTran = taxes.Insert(new APTaxTran()
          {
            TaxID = tax.TaxID
          });
          dictionary3.Add(taxId, apTaxTran);
        }));
        foreach (PXResult<APTaxTran, PX.Objects.TX.Tax> pxResult4 in resultSet)
        {
          APTaxTran origAPTaxTran = (APTaxTran) pxResult4;
          PX.Objects.TX.Tax taxCorrespondingToLine = (PX.Objects.TX.Tax) pxResult4;
          APTaxTran new_aptaxtran = insertedTaxes[origAPTaxTran.TaxID];
          if (new_aptaxtran != null)
          {
            Decimal? nullable9 = new_aptaxtran.CuryTaxableAmt;
            Decimal num12 = 0M;
            if (nullable9.GetValueOrDefault() == num12 & nullable9.HasValue)
            {
              nullable9 = new_aptaxtran.CuryTaxAmt;
              Decimal num13 = 0M;
              if (nullable9.GetValueOrDefault() == num13 & nullable9.HasValue)
              {
                nullable9 = new_aptaxtran.CuryExpenseAmt;
                Decimal num14 = 0M;
                if (nullable9.GetValueOrDefault() == num14 & nullable9.HasValue)
                  continue;
              }
            }
            APReleaseProcess.AdjustTaxCalculationLevelForNetGrossEntryMode(apInvoice3, (APTran) null, ref taxCorrespondingToLine);
            Decimal num15 = 0M;
            if (flag2)
            {
              foreach (APTax apTax1 in this.Base.Tax_Rows.Select().RowCast<APTax>().Where<APTax>((Func<APTax, bool>) (row => row.TaxID == origAPTaxTran.TaxID)))
              {
                APInvoiceEntryRetainage.APTranRetainageData tranRetainageData1 = dictionary1[(apTax1.TranType, apTax1.RefNbr, apTax1.LineNbr)];
                APTax apTax2 = (APTax) PXSelectBase<APTax, PXSelect<APTax, Where<APTax.tranType, Equal<Required<APTax.tranType>>, And<APTax.refNbr, Equal<Required<APTax.refNbr>>, And<APTax.lineNbr, Equal<Required<APTax.lineNbr>>, And<APTax.taxID, Equal<Required<APTax.taxID>>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) source.Key.DocType, (object) source.Key.RefNbr, (object) tranRetainageData1.Detail.OrigLineNbr, (object) apTax1.TaxID);
                Decimal num16;
                if (tranRetainageData1.IsFinal)
                {
                  PXResultset<APTax> pxResultset = PXSelectBase<APTax, PXSelectJoin<APTax, InnerJoin<APRegister, On<APRegister.docType, Equal<APTax.tranType>, And<APRegister.refNbr, Equal<APTax.refNbr>>>, InnerJoin<APTran, On<APTran.tranType, Equal<APTax.tranType>, And<APTran.refNbr, Equal<APTax.refNbr>, And<APTran.lineNbr, Equal<APTax.lineNbr>>>>>>, Where<APRegister.isRetainageDocument, Equal<True>, And<APRegister.released, Equal<True>, And<APRegister.origDocType, Equal<Required<APRegister.origDocType>>, And<APRegister.origRefNbr, Equal<Required<APRegister.origRefNbr>>, And<APTran.origLineNbr, Equal<Required<APTran.origLineNbr>>, And<APTax.taxID, Equal<Required<APTax.taxID>>>>>>>>>.Config>.Select((PXGraph) this.Base, (object) apInvoice3.OrigDocType, (object) apInvoice3.OrigRefNbr, (object) tranRetainageData1.Detail.OrigLineNbr, (object) apTax1.TaxID);
                  Decimal num17 = 0M;
                  foreach (PXResult<APTax, APRegister> pxResult5 in pxResultset)
                  {
                    APTax apTax3 = (APTax) pxResult5;
                    APRegister apRegister = (APRegister) pxResult5;
                    nullable9 = currentItem1.SignAmount;
                    Decimal? signAmount3 = apRegister.SignAmount;
                    Decimal valueOrDefault3 = (nullable9.HasValue & signAmount3.HasValue ? new Decimal?(nullable9.GetValueOrDefault() * signAmount3.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
                    Decimal num18 = num17;
                    nullable9 = apTax3.CuryTaxAmt;
                    Decimal valueOrDefault4 = nullable9.GetValueOrDefault();
                    nullable9 = apTax3.CuryExpenseAmt;
                    Decimal valueOrDefault5 = nullable9.GetValueOrDefault();
                    Decimal num19 = (valueOrDefault4 + valueOrDefault5) * valueOrDefault3;
                    num17 = num18 + num19;
                  }
                  num16 = (apTax2.CuryRetainedTaxAmt.GetValueOrDefault() - num17) * valueOrDefault1;
                }
                else
                {
                  nullable9 = apTax2.CuryRetainedTaxAmt;
                  Decimal num20 = nullable9.Value;
                  nullable9 = apTax2.CuryRetainedTaxableAmt;
                  Decimal num21 = nullable9.Value;
                  Decimal num22 = System.Math.Abs(num20 / num21);
                  IPXCurrencyHelper implementation = this.Base.FindImplementation<IPXCurrencyHelper>();
                  nullable9 = apTax1.CuryTaxableAmt;
                  Decimal val = nullable9.Value * num22;
                  num16 = implementation.RoundCury(val);
                }
                num15 += num16;
                APTax copy4 = PXCache<APTax>.CreateCopy(apTax1);
                nullable9 = copy4.NonDeductibleTaxRate;
                Decimal num23 = 100M - (nullable9 ?? 100M);
                copy4.CuryExpenseAmt = new Decimal?(this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(num16 * num23 / 100M));
                APTax apTax4 = copy4;
                Decimal num24 = num16;
                nullable9 = copy4.CuryExpenseAmt;
                Decimal? nullable10 = nullable9.HasValue ? new Decimal?(num24 - nullable9.GetValueOrDefault()) : new Decimal?();
                apTax4.CuryTaxAmt = nullable10;
                this.Base.Tax_Rows.Update(copy4);
                if (APReleaseProcess.IncludeTaxInLineBalance(taxCorrespondingToLine))
                {
                  APInvoiceEntryRetainage.APTranRetainageData tranRetainageData2 = tranRetainageData1;
                  nullable9 = tranRetainageData2.RemainAmt;
                  Decimal num25 = num16;
                  tranRetainageData2.RemainAmt = nullable9.HasValue ? new Decimal?(nullable9.GetValueOrDefault() - num25) : new Decimal?();
                }
              }
            }
            else if (flag3)
            {
              PXResultset<APTaxTran> pxResultset = PXSelectBase<APTaxTran, PXSelectJoin<APTaxTran, InnerJoin<APRegister, On<APRegister.docType, Equal<APTaxTran.tranType>, And<APRegister.refNbr, Equal<APTaxTran.refNbr>>>>, Where<APRegister.isRetainageDocument, Equal<True>, And<APRegister.released, Equal<True>, And<APRegister.origDocType, Equal<Required<APRegister.origDocType>>, And<APRegister.origRefNbr, Equal<Required<APRegister.origRefNbr>>, And<APTaxTran.taxID, Equal<Required<APTaxTran.taxID>>>>>>>>.Config>.Select((PXGraph) this.Base, (object) origAPTaxTran.TranType, (object) origAPTaxTran.RefNbr, (object) origAPTaxTran.TaxID);
              Decimal num26 = 0M;
              foreach (PXResult<APTaxTran, APRegister> pxResult6 in pxResultset)
              {
                APTaxTran apTaxTran = (APTaxTran) pxResult6;
                APRegister apRegister = (APRegister) pxResult6;
                nullable9 = currentItem1.SignAmount;
                Decimal? signAmount4 = apRegister.SignAmount;
                Decimal valueOrDefault6 = (nullable9.HasValue & signAmount4.HasValue ? new Decimal?(nullable9.GetValueOrDefault() * signAmount4.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
                Decimal num27 = num26;
                nullable9 = apTaxTran.CuryTaxAmt;
                Decimal valueOrDefault7 = nullable9.GetValueOrDefault();
                nullable9 = apTaxTran.CuryExpenseAmt;
                Decimal valueOrDefault8 = nullable9.GetValueOrDefault();
                Decimal num28 = (valueOrDefault7 + valueOrDefault8) * valueOrDefault6;
                num26 = num27 + num28;
              }
              num15 = (origAPTaxTran.CuryRetainedTaxAmt.GetValueOrDefault() - num26) * valueOrDefault1;
            }
            else
            {
              APTax apTax = (APTax) PXSelectBase<APTax, PXSelectGroupBy<APTax, Where<APTax.tranType, Equal<Required<APTax.tranType>>, And<APTax.refNbr, Equal<Required<APTax.refNbr>>, And<APTax.taxID, Equal<Required<APTax.taxID>>>>>, Aggregate<GroupBy<APTax.tranType, GroupBy<APTax.refNbr, GroupBy<APTax.taxID, Sum<APTax.curyRetainedTaxableAmt>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) origAPTaxTran.TranType, (object) origAPTaxTran.RefNbr, (object) origAPTaxTran.TaxID);
              nullable9 = origAPTaxTran.CuryRetainedTaxAmt;
              Decimal num29 = nullable9.Value;
              nullable9 = apTax.CuryRetainedTaxableAmt;
              Decimal num30 = nullable9.Value;
              Decimal num31 = System.Math.Abs(num29 / num30);
              IPXCurrencyHelper implementation = this.Base.FindImplementation<IPXCurrencyHelper>();
              nullable9 = new_aptaxtran.CuryTaxableAmt;
              Decimal val = nullable9.Value * num31;
              num15 = implementation.RoundCury(val);
            }
            new_aptaxtran = PXCache<APTaxTran>.CreateCopy(new_aptaxtran);
            nullable9 = new_aptaxtran.CuryTaxAmt;
            Decimal valueOrDefault9 = nullable9.GetValueOrDefault();
            nullable9 = new_aptaxtran.CuryExpenseAmt;
            Decimal valueOrDefault10 = nullable9.GetValueOrDefault();
            Decimal num32 = valueOrDefault9 + valueOrDefault10 - num15;
            if (taxCorrespondingToLine != null && taxCorrespondingToLine.IsRegularInclusiveTax() && num32 != 0M)
            {
              APTaxTran apTaxTran1 = new_aptaxtran;
              nullable9 = apTaxTran1.CuryTaxableAmt;
              Decimal num33 = num32;
              apTaxTran1.CuryTaxableAmt = nullable9.HasValue ? new Decimal?(nullable9.GetValueOrDefault() + num33) : new Decimal?();
              foreach (APTax apTax5 in this.Base.Tax_Rows.Select().AsEnumerable<PXResult<APTax>>().RowCast<APTax>().Where<APTax>((Func<APTax, bool>) (row => row.TaxID == new_aptaxtran.TaxID)))
              {
                APTax roundAPTax = apTax5;
                APTax copy5 = PXCache<APTax>.CreateCopy(roundAPTax);
                APTax apTax6 = copy5;
                nullable9 = apTax6.CuryTaxableAmt;
                Decimal num34 = num32;
                apTax6.CuryTaxableAmt = nullable9.HasValue ? new Decimal?(nullable9.GetValueOrDefault() + num34) : new Decimal?();
                this.Base.Tax_Rows.Update(copy5);
                foreach (APTax apTax7 in this.Base.Tax_Rows.Select().AsEnumerable<PXResult<APTax>>().RowCast<APTax>().Where<APTax>((Func<APTax, bool>) (row =>
                {
                  if (!(row.TaxID != roundAPTax.TaxID))
                    return false;
                  int? lineNbr2 = row.LineNbr;
                  int? lineNbr3 = roundAPTax.LineNbr;
                  return lineNbr2.GetValueOrDefault() == lineNbr3.GetValueOrDefault() & lineNbr2.HasValue == lineNbr3.HasValue;
                })))
                {
                  APTaxTran apTaxTran2 = insertedTaxes[apTax7.TaxID];
                  APTaxTran apTaxTran3 = apTaxTran2;
                  nullable9 = apTaxTran3.CuryTaxableAmt;
                  Decimal num35 = num32;
                  apTaxTran3.CuryTaxableAmt = nullable9.HasValue ? new Decimal?(nullable9.GetValueOrDefault() + num35) : new Decimal?();
                  this.Base.Taxes.Update(apTaxTran2);
                  APTax copy6 = PXCache<APTax>.CreateCopy(apTax7);
                  APTax apTax8 = copy6;
                  nullable9 = apTax8.CuryTaxableAmt;
                  Decimal num36 = num32;
                  apTax8.CuryTaxableAmt = nullable9.HasValue ? new Decimal?(nullable9.GetValueOrDefault() + num36) : new Decimal?();
                  this.Base.Tax_Rows.Update(copy6);
                }
              }
            }
            new_aptaxtran.TaxRate = origAPTaxTran.TaxRate;
            nullable9 = new_aptaxtran.NonDeductibleTaxRate;
            Decimal num37 = 100M - (nullable9 ?? 100M);
            new_aptaxtran.CuryExpenseAmt = new Decimal?(this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(num15 * num37 / 100M));
            APTaxTran apTaxTran4 = new_aptaxtran;
            Decimal num38 = num15;
            nullable9 = new_aptaxtran.CuryExpenseAmt;
            Decimal? nullable11 = nullable9.HasValue ? new Decimal?(num38 - nullable9.GetValueOrDefault()) : new Decimal?();
            apTaxTran4.CuryTaxAmt = nullable11;
            new_aptaxtran.CuryTaxAmtSumm = new_aptaxtran.CuryTaxAmt;
            new_aptaxtran = this.Base.Taxes.Update(new_aptaxtran);
          }
        }
        if (flag2)
          EnumerableExtensions.ForEach<APInvoiceEntryRetainage.APTranRetainageData>(dictionary1.Values.Where<APInvoiceEntryRetainage.APTranRetainageData>((Func<APInvoiceEntryRetainage.APTranRetainageData, bool>) (value =>
          {
            Decimal? remainAmt = value.RemainAmt;
            Decimal num39 = 0M;
            return !(remainAmt.GetValueOrDefault() == num39 & remainAmt.HasValue);
          })), (System.Action<APInvoiceEntryRetainage.APTranRetainageData>) (value => this.ProcessRoundingDiff(value.RemainAmt.GetValueOrDefault(), value.Detail)));
        else if (tran != null)
        {
          Decimal diff = System.Math.Abs(num1) - apInvoice3.CuryDocBal.GetValueOrDefault();
          if (diff != 0M)
            this.ProcessRoundingDiff(diff, tran);
        }
        Decimal? curyTaxAmt = apInvoice3.CuryTaxAmt;
        Decimal? curyTaxTotal = apInvoice3.CuryTaxTotal;
        if (!(curyTaxAmt.GetValueOrDefault() == curyTaxTotal.GetValueOrDefault() & curyTaxAmt.HasValue == curyTaxTotal.HasValue))
        {
          apInvoice3.CuryTaxAmt = apInvoice3.CuryTaxTotal;
          apInvoice3 = this.Base.Document.Update(apInvoice3);
        }
        TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Base.Transactions.Cache, (object) null, taxCalc);
        bool? nullable12;
        if (!isAutoRelease)
        {
          nullable12 = this.Base.apsetup.Current.HoldEntry;
          bool flag5 = false;
          if (nullable12.GetValueOrDefault() == flag5 & nullable12.HasValue && this.Base.IsApprovalRequired(apInvoice3))
            this.Base.releaseFromHold.Press();
        }
        this.Base.Save.Press();
        if (isAutoRelease)
        {
          nullable12 = apInvoice3.Hold;
          if (!nullable12.GetValueOrDefault())
          {
            using (new PXTimeStampScope((byte[]) null))
              APDocumentRelease.ReleaseDoc(new List<APRegister>()
              {
                (APRegister) apInvoice3
              }, false);
          }
        }
      }
      catch (PXException ex)
      {
        PXProcessing<APInvoiceExt>.SetError((Exception) ex);
        flag1 = true;
      }
    }
    if (flag1)
      throw new PXOperationCompletedWithErrorException("One or more documents could not be released.");
  }

  public virtual void CopyExtensionValues(APTran detail, APTran tranNew)
  {
  }

  private void ProcessRoundingDiff(Decimal diff, APTran tran)
  {
    APTran apTran = tran;
    Decimal? curyLineAmt = apTran.CuryLineAmt;
    Decimal num1 = diff;
    apTran.CuryLineAmt = curyLineAmt.HasValue ? new Decimal?(curyLineAmt.GetValueOrDefault() + num1) : new Decimal?();
    tran = this.Base.Transactions.Update(tran);
    foreach (IGrouping<\u003C\u003Ef__AnonymousType6<string, string, string>, APTax> grouping in this.Base.Tax_Rows.Select().AsEnumerable<PXResult<APTax>>().RowCast<APTax>().Where<APTax>((Func<APTax, bool>) (row =>
    {
      int? lineNbr1 = row.LineNbr;
      int? lineNbr2 = tran.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    })).GroupBy(row => new
    {
      TranType = row.TranType,
      RefNbr = row.RefNbr,
      TaxID = row.TaxID
    }))
    {
      Decimal? curyTaxableAmt;
      foreach (APTax apTax1 in (IEnumerable<APTax>) grouping)
      {
        APTax copy = PXCache<APTax>.CreateCopy(apTax1);
        APTax apTax2 = copy;
        curyTaxableAmt = apTax2.CuryTaxableAmt;
        Decimal num2 = diff;
        apTax2.CuryTaxableAmt = curyTaxableAmt.HasValue ? new Decimal?(curyTaxableAmt.GetValueOrDefault() + num2) : new Decimal?();
        this.Base.Tax_Rows.Update(copy);
      }
      APTaxTran apTaxTran1 = (APTaxTran) PXSelectBase<APTaxTran, PXSelect<APTaxTran, Where<APTaxTran.tranType, Equal<Required<APTaxTran.tranType>>, And<APTaxTran.refNbr, Equal<Required<APTaxTran.refNbr>>, And<APTaxTran.taxID, Equal<Required<APTaxTran.taxID>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) grouping.Key.TranType, (object) grouping.Key.RefNbr, (object) grouping.Key.TaxID);
      if (apTaxTran1 != null)
      {
        APTaxTran copy = PXCache<APTaxTran>.CreateCopy(apTaxTran1);
        APTaxTran apTaxTran2 = copy;
        curyTaxableAmt = apTaxTran2.CuryTaxableAmt;
        Decimal num3 = diff;
        apTaxTran2.CuryTaxableAmt = curyTaxableAmt.HasValue ? new Decimal?(curyTaxableAmt.GetValueOrDefault() + num3) : new Decimal?();
        this.Base.Taxes.Update(copy);
      }
    }
  }

  [PXUIField(Visible = false, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLookupButton]
  protected virtual IEnumerable viewRetainageDocument(PXAdapter adapter)
  {
    RedirectionToOrigDoc.TryRedirect(this.RetainageDocuments.Current.DocType, this.RetainageDocuments.Current.RefNbr, this.RetainageDocuments.Current.OrigModule, true);
    return adapter.Get();
  }

  public delegate bool IsInvoiceNbrRequiredDelegate(APInvoice doc);

  public delegate void AddDiscountDelegate(PXCache sender, APInvoice row);

  public class APTranRetainageData
  {
    public APTran Detail;
    public Decimal? RemainAmt;
    public bool IsFinal;
  }
}
