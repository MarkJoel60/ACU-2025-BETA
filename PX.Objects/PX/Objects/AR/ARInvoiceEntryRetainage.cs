// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceEntryRetainage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CC;
using PX.Objects.CN.Common.Services.DataProviders;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AR;

[Serializable]
public class ARInvoiceEntryRetainage : PXGraphExtension<ARInvoiceEntry>
{
  [PXReadOnlyView]
  [PXCopyPasteHiddenView]
  public PXSelect<ARRetainageWithApplications, Where<ARRetainageWithApplications.origRefNbr, Equal<Current<ARInvoice.refNbr>>, And<ARRetainageWithApplications.origDocType, Equal<Current<ARInvoice.docType>>>>> RetainageDocuments;
  [PXCopyPasteHiddenView]
  public PXAction<ARInvoice> releaseRetainage;
  public PXAction<ARInvoice> ViewRetainageDocument;
  public PXAction<ARInvoice> viewOrigRetainageDocument;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.retainage>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXGraph) this.Base).Actions["action"]?.AddMenuAction((PXAction) this.releaseRetainage);
  }

  [PXMergeAttributes]
  [ARRetainedTax(typeof (ARInvoice), typeof (ARTax), typeof (ARTaxTran))]
  protected virtual void ARTran_TaxCategoryID_CacheAttached(PXCache sender)
  {
  }

  [DBRetainagePercent(typeof (ARInvoice.retainageApply), typeof (ARRegister.defRetainagePct), typeof (Sub<Current<ARTran.curyExtPrice>, Current<ARTran.curyDiscAmt>>), typeof (ARTran.curyRetainageAmt), typeof (ARTran.retainagePct))]
  protected virtual void ARTran_RetainagePct_CacheAttached(PXCache sender)
  {
  }

  [DBRetainageAmount(typeof (ARTran.curyInfoID), typeof (Sub<ARTran.curyExtPrice, ARTran.curyDiscAmt>), typeof (ARTran.curyRetainageAmt), typeof (ARTran.retainageAmt), typeof (ARTran.retainagePct))]
  protected virtual void ARTran_CuryRetainageAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (IIf<Where<ARInvoice.projectID, NotEqual<NonProject>, And<Selector<ARInvoice.projectID, PMProject.baseType>, Equal<CTPRType.project>>>, Selector<ARInvoice.projectID, PMProject.retainagePct>, Selector<ARRegister.customerID, Customer.retainagePct>>))]
  protected virtual void ARInvoice_DefRetainagePct_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (ARRegister.curyRetainageTotal))]
  protected virtual void ARInvoice_CuryRetainageUnreleasedAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where2<FeatureInstalled<FeaturesSet.retainage>, And<ARRegister.retainageApply, Equal<True>, And<ARRegister.released, NotEqual<True>>>>, ARRegister.curyRetainageTotal>, ARRegister.curyRetainageUnpaidTotal>))]
  protected virtual void ARInvoice_CuryRetainageUnpaidTotal_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (IIf<Where2<FeatureInstalled<FeaturesSet.retainage>, And2<Where<ARRegister.docType, Equal<ARDocType.invoice>, Or<ARRegister.docType, Equal<ARDocType.creditMemo>>>, And<ARRegister.origModule, Equal<BatchModule.moduleAR>, And<Current<ARSetup.migrationMode>, Equal<False>>>>>, IsNull<Selector<ARRegister.customerID, Customer.retainageApply>, False>, False>))]
  [PXUIVerify]
  [PXUIVerify]
  protected virtual void ARInvoice_RetainageApply_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIVerify]
  protected virtual void ARInvoice_CuryRetainageTotal_CacheAttached(PXCache sender)
  {
  }

  protected virtual void ARInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARInvoice row))
      return;
    PXAction<ARInvoice> releaseRetainage = this.releaseRetainage;
    bool? nullable = row.Released;
    int num;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.RetainageApply;
      if (nullable.GetValueOrDefault())
      {
        num = !row.HasZeroBalance<ARRegister.curyRetainageUnreleasedAmt, ARTran.curyRetainageBal>(cache.Graph) ? 1 : 0;
        goto label_5;
      }
    }
    num = 0;
label_5:
    ((PXAction) releaseRetainage).SetEnabled(num != 0);
    ((PXSelectBase) this.RetainageDocuments).Cache.AllowUpdate = false;
    ((PXSelectBase) this.RetainageDocuments).Cache.AllowInsert = false;
    ((PXSelectBase) this.RetainageDocuments).Cache.AllowDelete = false;
  }

  protected virtual void ARInvoice_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
  }

  protected virtual void ARInvoice_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) PXSelectorAttribute.Select<ARInvoice.termsID>(((PXSelectBase) this.Base.Document).Cache, (object) row);
    bool? retainageApply;
    if (terms != null)
    {
      retainageApply = row.RetainageApply;
      if (retainageApply.GetValueOrDefault() && terms.InstallmentType == "M")
        sender.RaiseExceptionHandling<ARInvoice.termsID>((object) row, (object) row.TermsID, (Exception) new PXSetPropertyException("The document cannot be processed because retainage cannot be applied to documents with the multiple installment credit terms specified."));
    }
    retainageApply = row.RetainageApply;
    bool flag = !retainageApply.GetValueOrDefault();
    PXDefaultAttribute.SetPersistingCheck<ARRegister.retainageAcctID>(sender, (object) row, flag ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<ARRegister.retainageSubID>(sender, (object) row, flag ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
  }

  protected virtual void ARInvoice_RetainageAcctID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<PX.Objects.CR.Location>) this.Base.location).Current == null || e.Row == null)
      return;
    e.NewValue = this.Base.GetAcctSub<PX.Objects.CR.Location.aRRetainageAcctID>(((PXSelectBase) this.Base.location).Cache, (object) ((PXSelectBase<PX.Objects.CR.Location>) this.Base.location).Current);
  }

  protected virtual void ARInvoice_RetainageSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<PX.Objects.CR.Location>) this.Base.location).Current == null || e.Row == null)
      return;
    e.NewValue = this.Base.GetAcctSub<PX.Objects.CR.Location.aRRetainageSubID>(((PXSelectBase) this.Base.location).Cache, (object) ((PXSelectBase<PX.Objects.CR.Location>) this.Base.location).Current);
  }

  protected virtual void ARInvoice_CustomerLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    if (row == null || !row.RetainageApply.GetValueOrDefault())
      return;
    sender.SetDefaultExt<ARRegister.retainageAcctID>((object) row);
    sender.SetDefaultExt<ARRegister.retainageSubID>((object) row);
  }

  protected virtual void ARInvoice_RetainageApply_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    if (row == null)
      return;
    bool? nullable = row.RetainageApply;
    if (nullable.GetValueOrDefault())
    {
      sender.SetDefaultExt<ARRegister.retainageAcctID>((object) row);
      sender.SetDefaultExt<ARRegister.retainageSubID>((object) row);
    }
    else
    {
      nullable = row.IsRetainageDocument;
      bool flag = false;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      sender.SetValueExt<ARRegister.retainageAcctID>((object) row, (object) null);
      sender.SetValueExt<ARRegister.retainageSubID>((object) row, (object) null);
    }
  }

  protected virtual void ARInvoice_RetainageApply_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
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
        IEnumerable<ARTran> source = GraphHelper.RowCast<ARTran>((IEnumerable) ((IEnumerable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) this.Base.Transactions).Select(Array.Empty<object>())).AsEnumerable<PXResult<ARTran>>().Where<PXResult<ARTran>>((Func<PXResult<ARTran>, bool>) (tran =>
        {
          Decimal? curyRetainageAmt = PXResult<ARTran>.op_Implicit(tran).CuryRetainageAmt;
          Decimal num1 = 0M;
          if (!(curyRetainageAmt.GetValueOrDefault() == num1 & curyRetainageAmt.HasValue))
            return true;
          Decimal? retainagePct = PXResult<ARTran>.op_Implicit(tran).RetainagePct;
          Decimal num2 = 0M;
          return !(retainagePct.GetValueOrDefault() == num2 & retainagePct.HasValue);
        })));
        if (!source.Any<ARTran>())
          return;
        if (((PXSelectBase<ARInvoice>) this.Base.Document).Ask("Warning", "If you clear the Apply Retainage check box, the retainage amount and retainage percent will be set to zero. Do you want to proceed?", (MessageButtons) 4, (MessageIcon) 3) == 6)
        {
          using (IEnumerator<ARTran> enumerator = source.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              ARTran current = enumerator.Current;
              current.CuryRetainageAmt = new Decimal?(0M);
              current.RetainagePct = new Decimal?(0M);
              ((PXSelectBase<ARTran>) this.Base.Transactions).Update(current);
            }
            return;
          }
        }
        ((CancelEventArgs) e).Cancel = true;
        e.NewValue = (object) true;
        return;
      }
    }
    nullable = row.RetainageApply;
    if (nullable.GetValueOrDefault() || !newValue.GetValueOrDefault())
      return;
    this.ClearCurrentDocumentDiscountDetails();
  }

  protected virtual void ARTran_SubID_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
  }

  protected virtual void ClearCurrentDocumentDiscountDetails()
  {
    EnumerableExtensions.ForEach<ARInvoiceDiscountDetail>(GraphHelper.RowCast<ARInvoiceDiscountDetail>((IEnumerable) ((PXSelectBase<ARInvoiceDiscountDetail>) this.Base.ARDiscountDetails).Select(Array.Empty<object>())), (Action<ARInvoiceDiscountDetail>) (discountDetail => ((PXSelectBase) this.Base.ARDiscountDetails).Cache.Delete((object) discountDetail)));
    EnumerableExtensions.ForEach<ARTran>(GraphHelper.RowCast<ARTran>((IEnumerable) ((PXSelectBase<ARTran>) this.Base.Discount_Row).Select(Array.Empty<object>())), (Action<ARTran>) (tran => ((PXSelectBase) this.Base.Discount_Row).Cache.Delete((object) tran)));
  }

  [PXOverride]
  public virtual void ARInvoiceDiscountDetail_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e)
  {
    ARInvoice current = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
    if ((current == null || !current.RetainageApply.GetValueOrDefault()) && (current == null || !current.IsRetainageDocument.GetValueOrDefault()))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  [PXOverride]
  public void AddDiscount(
    PXCache sender,
    ARInvoice row,
    ARInvoiceEntryRetainage.AddDiscountDelegate baseMethod)
  {
    if ((row.RetainageApply.GetValueOrDefault() ? 1 : (row.IsRetainageDocument.GetValueOrDefault() ? 1 : 0)) != 0)
      return;
    baseMethod(sender, row);
  }

  [PXUIField]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable ReleaseRetainage(PXAdapter adapter)
  {
    ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
    ARRegister reversingDoc;
    if (this.Base.CheckReversingRetainageDocumentAlreadyExists(((PXSelectBase<ARInvoice>) this.Base.Document).Current, out reversingDoc))
      throw new PXException("The retainage {0} cannot be released because the reversing document {1} {2} exists in the system.", new object[3]
      {
        (object) PXMessages.LocalizeNoPrefix(ARInvoiceEntry.ARDocTypeDict[current1.DocType]),
        (object) PXMessages.LocalizeNoPrefix(ARInvoiceEntry.ARDocTypeDict[reversingDoc.DocType]),
        (object) reversingDoc.RefNbr
      });
    if (current1.ProformaExists.GetValueOrDefault())
    {
      PMProforma pmProforma = PXResultset<PMProforma>.op_Implicit(PXSelectBase<PMProforma, PXSelect<PMProforma, Where<PMProforma.aRInvoiceDocType, Equal<Current<ARInvoice.docType>>, And<PMProforma.aRInvoiceRefNbr, Equal<Current<ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
      if (pmProforma != null && pmProforma.Corrected.GetValueOrDefault())
        throw new PXException("The system cannot release retainage from the invoice {0} because the related pro forma invoice {1} is under correction. To be able to release the invoice {0}, release the pro forma invoice {1} on the Pro Forma Invoices (PM307000) form first.", new object[2]
        {
          (object) current1.RefNbr,
          (object) pmProforma.RefNbr
        });
    }
    ((PXAction) this.Base.Save).Press();
    ARRetainageRelease instance = PXGraph.CreateInstance<ARRetainageRelease>();
    ARRetainageFilter current2 = ((PXSelectBase<ARRetainageFilter>) instance.Filter).Current;
    DateTime? businessDate = ((PXGraph) this.Base).Accessinfo.BusinessDate;
    DateTime? docDate = current1.DocDate;
    DateTime? nullable = (businessDate.HasValue & docDate.HasValue ? (businessDate.GetValueOrDefault() > docDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? ((PXGraph) this.Base).Accessinfo.BusinessDate : current1.DocDate;
    current2.DocDate = nullable;
    ((PXSelectBase<ARRetainageFilter>) instance.Filter).Current.FinPeriodID = this.Base.FinPeriodRepository.GetPeriodIDFromDate(((PXSelectBase<ARRetainageFilter>) instance.Filter).Current.DocDate, new int?(0));
    ((PXSelectBase<ARRetainageFilter>) instance.Filter).Current.BranchID = current1.BranchID;
    ((PXSelectBase<ARRetainageFilter>) instance.Filter).Current.OrgBAccountID = new int?(PXAccess.GetBranch(current1.BranchID).BAccountID);
    ((PXSelectBase<ARRetainageFilter>) instance.Filter).Current.CustomerID = current1.CustomerID;
    ((PXSelectBase<ARRetainageFilter>) instance.Filter).Current.RefNbr = current1.RefNbr;
    ((PXSelectBase<ARRetainageFilter>) instance.Filter).Current.ShowBillsWithOpenBalance = new bool?(current1.OpenDoc.GetValueOrDefault());
    if (((PXSelectBase<ARInvoiceExt>) instance.DocumentList).SelectSingle(Array.Empty<object>()) == null)
    {
      ARRetainageWithApplications withApplications = PXResult<ARRetainageWithApplications>.op_Implicit(((IQueryable<PXResult<ARRetainageWithApplications>>) ((PXSelectBase<ARRetainageWithApplications>) this.RetainageDocuments).Select(Array.Empty<object>())).FirstOrDefault<PXResult<ARRetainageWithApplications>>((Expression<Func<PXResult<ARRetainageWithApplications>, bool>>) (row => ((ARRetainageWithApplications) row).Released != (bool?) true)));
      if (withApplications != null)
        throw new PXException("The retainage cannot be released because {0} {1} associated with this {2} has not been released yet. To proceed, delete or release the retainage document.", new object[3]
        {
          (object) PXMessages.LocalizeNoPrefix(ARInvoiceEntry.ARDocTypeDict[withApplications.DocType]),
          (object) withApplications.RefNbr,
          (object) PXMessages.LocalizeNoPrefix(ARInvoiceEntry.ARDocTypeDict[current1.DocType])
        });
    }
    throw new PXRedirectRequiredException((PXGraph) instance, nameof (ReleaseRetainage));
  }

  public virtual void ReleaseRetainageProc(
    List<ARInvoiceExt> list,
    RetainageOptions retainageOpts,
    bool isAutoRelease = false)
  {
    bool flag1 = false;
    List<ARInvoice> arInvoiceList = new List<ARInvoice>();
    IProjectDataProvider service = ((PXGraph) this.Base).GetService<IProjectDataProvider>();
    foreach (List<ARInvoiceExt> source1 in PXAccess.FeatureInstalled<FeaturesSet.paymentsByLines>() ? list.GroupBy(row => new
    {
      CustomerID = row.CustomerID,
      ProjectID = row.ProjectID,
      CustomerLocationID = row.CustomerLocationID,
      TaxZoneID = row.TaxZoneID,
      BranchID = row.BranchID,
      ARAccountID = row.ARAccountID,
      ARSubID = row.ARSubID,
      TaxCalcMode = row.TaxCalcMode,
      ExternalTaxExemptionNumber = row.ExternalTaxExemptionNumber,
      AvalaraCustomerUsageType = row.AvalaraCustomerUsageType,
      CuryID = row.CuryID
    }).Select<IGrouping<\u003C\u003Ef__AnonymousType16<int?, int?, int?, string, int?, int?, int?, string, string, string, string>, ARInvoiceExt>, List<ARInvoiceExt>>(x => x.ToList<ARInvoiceExt>()).ToList<List<ARInvoiceExt>>() : list.GroupBy(row => new
    {
      DocType = row.DocType,
      RefNbr = row.RefNbr
    }).Select<IGrouping<\u003C\u003Ef__AnonymousType0<string, string>, ARInvoiceExt>, List<ARInvoiceExt>>(x => x.ToList<ARInvoiceExt>()).ToList<List<ARInvoiceExt>>())
    {
      try
      {
        bool flag2 = source1.GroupBy(x => new
        {
          DocType = x.DocType,
          RefNbr = x.RefNbr
        }).Count<IGrouping<\u003C\u003Ef__AnonymousType0<string, string>, ARInvoiceExt>>() > 1;
        Decimal num1 = source1.Sum<ARInvoiceExt>((Func<ARInvoiceExt, Decimal>) (row => row.CuryRetainageReleasedAmt.GetValueOrDefault()));
        Dictionary<int?, ARTran> dictionary1 = new Dictionary<int?, ARTran>();
        Dictionary<int?, Decimal> signList = new Dictionary<int?, Decimal>();
        List<ARTaxTran> arTaxTranList = new List<ARTaxTran>();
        List<ARTax> arTaxList = new List<ARTax>();
        ARInvoice invoice = new ARInvoice();
        TaxCalc taxCalc = TaxBaseAttribute.GetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null);
        foreach (IGrouping<\u003C\u003Ef__AnonymousType0<string, string>, ARInvoiceExt> source2 in source1.GroupBy(row => new
        {
          DocType = row.DocType,
          RefNbr = row.RefNbr
        }))
        {
          ARInvoiceExt arInvoiceExt1 = source2.First<ARInvoiceExt>();
          PXProcessing<ARInvoiceExt>.SetCurrentItem((object) arInvoiceExt1);
          Decimal num2 = source2.Sum<ARInvoiceExt>((Func<ARInvoiceExt, Decimal>) (row => row.CuryRetainageReleasedAmt.GetValueOrDefault()));
          ((PXGraph) this.Base).Clear((PXClearOption) 3);
          PXUIFieldAttribute.SetError(((PXSelectBase) this.Base.Document).Cache, (object) null, (string) null, (string) null);
          ARTran tran1 = (ARTran) null;
          taxCalc = TaxBaseAttribute.GetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null);
          ((PXGraph) this.Base).Clear((PXClearOption) 1);
          ((PXSelectBase) this.Base.CurrentDocument).Cache.AllowUpdate = true;
          PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer> pxResult1 = ((IEnumerable<PXResult<ARInvoice>>) PXSelectBase<ARInvoice, PXSelectJoin<ARInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARInvoice.curyInfoID>>, LeftJoin<PX.Objects.CS.Terms, On<PX.Objects.CS.Terms.termsID, Equal<ARInvoice.termsID>>, LeftJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, LeftJoin<PX.Objects.GL.Account, On<ARInvoice.aRAccountID, Equal<PX.Objects.GL.Account.accountID>>>>>>, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[2]
          {
            (object) arInvoiceExt1.DocType,
            (object) arInvoiceExt1.RefNbr
          })).AsEnumerable<PXResult<ARInvoice>>().Cast<PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>>().First<PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>>();
          ARInvoice arInvoice1 = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>.op_Implicit(pxResult1);
          Customer customer = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>.op_Implicit(pxResult1);
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXGraph) this.Base).GetExtension<ARInvoiceEntry.MultiCurrency>().CloneCurrencyInfo(PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>.op_Implicit(pxResult1));
          currencyInfo1.IsReadOnly = new bool?(false);
          invoice = PXCache<ARInvoice>.CreateCopy(arInvoice1);
          object obj = ((PXSelectBase) this.Base.CurrentDocument).Cache.GetValue<ARInvoice.ownerID>((object) arInvoice1);
          try
          {
            ((PXSelectBase) this.Base.CurrentDocument).Cache.RaiseFieldVerifying<ARInvoice.ownerID>((object) arInvoice1, ref obj);
          }
          catch (Exception ex)
          {
            invoice.OwnerID = new int?();
          }
          invoice.CuryInfoID = currencyInfo1.CuryInfoID;
          if (num2 != 0M)
            invoice.DocType = num2 < 0M ? "CRM" : "INV";
          invoice.RefNbr = (string) null;
          invoice.LineCntr = new int?();
          PMProject project = service.GetProject((PXGraph) this.Base, arInvoice1.ProjectID);
          invoice.DocDesc = flag2 ? $"Retainage for project: {project.ContractCD.TrimEnd()} - {project.Description.TrimEnd()}" : arInvoice1.DocDesc;
          invoice.InvoiceNbr = arInvoice1.InvoiceNbr;
          invoice.OpenDoc = new bool?(true);
          invoice.Released = new bool?(false);
          ((PXSelectBase) this.Base.Document).Cache.SetDefaultExt<ARInvoice.isMigratedRecord>((object) invoice);
          ((PXSelectBase) this.Base.Document).Cache.SetDefaultExt<ARInvoice.hold>((object) invoice);
          invoice.BatchNbr = (string) null;
          invoice.ScheduleID = (string) null;
          invoice.Scheduled = new bool?(false);
          invoice.NoteID = new Guid?();
          invoice.TermsID = invoice.DocType == "CRM" ? (string) null : customer.TermsID;
          invoice.DueDate = new DateTime?();
          invoice.DiscDate = new DateTime?();
          invoice.CuryOrigDiscAmt = new Decimal?(0M);
          invoice.OrigDocType = arInvoice1.DocType;
          invoice.OrigRefNbr = arInvoice1.RefNbr;
          invoice.OrigDocDate = arInvoice1.DocDate;
          invoice.OrigRefNbr = flag2 ? (string) null : arInvoice1.RefNbr;
          invoice.OrigDocType = flag2 ? (string) null : arInvoice1.DocType;
          invoice.CuryLineTotal = new Decimal?(0M);
          invoice.IsTaxPosted = new bool?(false);
          invoice.IsTaxValid = new bool?(false);
          invoice.CuryVatTaxableTotal = new Decimal?(0M);
          invoice.CuryVatExemptTotal = new Decimal?(0M);
          invoice.CuryDetailExtPriceTotal = new Decimal?(0M);
          invoice.DetailExtPriceTotal = new Decimal?(0M);
          invoice.CuryLineDiscTotal = new Decimal?(0M);
          invoice.LineDiscTotal = new Decimal?(0M);
          invoice.CuryMiscExtPriceTotal = new Decimal?(0M);
          invoice.MiscExtPriceTotal = new Decimal?(0M);
          invoice.CuryGoodsExtPriceTotal = new Decimal?(0M);
          invoice.GoodsExtPriceTotal = new Decimal?(0M);
          invoice.CuryDocBal = new Decimal?(0M);
          ((PXSelectBase<ARInvoice>) this.Base.Document).SetValueExt<ARInvoice.curyOrigDocAmt>(invoice, (object) Math.Abs(num2));
          bool? nullable1 = invoice.IsMigratedRecord;
          if (nullable1.GetValueOrDefault())
            invoice.CuryInitDocBal = invoice.CuryOrigDocAmt;
          ARInvoice arInvoice2 = invoice;
          int num3;
          if (!isAutoRelease)
          {
            nullable1 = ((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.HoldEntry;
            if (nullable1.GetValueOrDefault())
            {
              num3 = 1;
              goto label_16;
            }
          }
          num3 = this.Base.IsApprovalRequired(invoice) ? 1 : 0;
label_16:
          bool? nullable2 = new bool?(num3 != 0);
          arInvoice2.Hold = nullable2;
          invoice.DocDate = retainageOpts.DocDate;
          FinPeriodIDAttribute.SetPeriodsByMaster<ARInvoice.finPeriodID>(((PXSelectBase) this.Base.Document).Cache, (object) invoice, retainageOpts.MasterFinPeriodID);
          this.Base.ClearRetainageSummary(invoice);
          invoice.RetainageApply = new bool?(false);
          invoice.IsRetainageDocument = new bool?(true);
          ((PXSelectBase) this.Base.Document).Cache.SetDefaultExt<ARInvoicePayLink.deliveryMethod>((object) invoice);
          ((PXSelectBase) this.Base.Document).Cache.SetDefaultExt<ARInvoicePayLink.payLinkID>((object) invoice);
          ((PXSelectBase) this.Base.Document).Cache.SetDefaultExt<ARInvoicePayLink.processingCenterID>((object) invoice);
          invoice = ((PXSelectBase<ARInvoice>) this.Base.Document).Insert(invoice);
          Decimal? signAmount1 = arInvoiceExt1.SignAmount;
          Decimal? signAmount2 = invoice.SignAmount;
          Decimal valueOrDefault1 = (signAmount1.HasValue & signAmount2.HasValue ? new Decimal?(signAmount1.GetValueOrDefault() * signAmount2.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
          if (currencyInfo1 != null)
          {
            PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<ARInvoice.curyInfoID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
            currencyInfo2.CuryID = currencyInfo1.CuryID;
            currencyInfo2.CuryEffDate = currencyInfo1.CuryEffDate;
            currencyInfo2.CuryRateTypeID = currencyInfo1.CuryRateTypeID;
            currencyInfo2.CuryRate = currencyInfo1.CuryRate;
            currencyInfo2.RecipRate = currencyInfo1.RecipRate;
            currencyInfo2.CuryMultDiv = currencyInfo1.CuryMultDiv;
            ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.Base.currencyinfo).Update(currencyInfo2);
          }
          int? nullable3 = arInvoiceExt1.LineNbr;
          int num4 = 0;
          bool flag3 = !(nullable3.GetValueOrDefault() == num4 & nullable3.HasValue);
          int num5;
          if (!flag3)
          {
            Decimal? unreleasedCalcAmt = arInvoiceExt1.CuryRetainageUnreleasedCalcAmt;
            Decimal num6 = 0M;
            num5 = unreleasedCalcAmt.GetValueOrDefault() == num6 & unreleasedCalcAmt.HasValue ? 1 : 0;
          }
          else
            num5 = 0;
          bool flag4 = num5 != 0;
          Dictionary<(string, string, int?), ARInvoiceEntryRetainage.ARTranRetainageData> dictionary2 = new Dictionary<(string, string, int?), ARInvoiceEntryRetainage.ARTranRetainageData>();
          foreach (ARInvoiceExt arInvoiceExt2 in (IEnumerable<ARInvoiceExt>) source2)
          {
            PXProcessing<ARInvoiceExt>.SetCurrentItem((object) arInvoiceExt2);
            PXResultset<ARTran> pxResultset1;
            if (!flag3)
              pxResultset1 = PXSelectBase<ARTran, PXSelectGroupBy<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<ARTran.curyRetainageAmt, NotEqual<decimal0>>>>, Aggregate<GroupBy<ARTran.taxCategoryID, Sum<ARTran.curyRetainageAmt>>>, OrderBy<Asc<ARTran.taxCategoryID>>>.Config>.Select((PXGraph) this.Base, new object[2]
              {
                (object) arInvoiceExt2.DocType,
                (object) arInvoiceExt2.RefNbr
              });
            else
              pxResultset1 = PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<ARTran.lineNbr, Equal<Required<ARTran.lineNbr>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[3]
              {
                (object) arInvoiceExt2.DocType,
                (object) arInvoiceExt2.RefNbr,
                (object) arInvoiceExt2.LineNbr
              });
            TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
            foreach (PXResult<ARTran> pxResult2 in pxResultset1)
            {
              ARTran arTran1 = PXResult<ARTran>.op_Implicit(pxResult2);
              ARTran arTran2 = ((PXSelectBase<ARTran>) this.Base.Transactions).Insert(new ARTran()
              {
                CuryUnitPrice = new Decimal?(0M),
                CuryExtPrice = new Decimal?(0M),
                BranchID = arInvoice1.BranchID,
                AccountID = arInvoice1.RetainageAcctID,
                SubID = arInvoice1.RetainageSubID,
                ProjectID = ProjectDefaultAttribute.NonProject()
              });
              arTran2.TaxCategoryID = arTran1.TaxCategoryID;
              ARTran arTran3 = arTran2;
              nullable3 = new int?();
              int? nullable4 = nullable3;
              arTran3.TaskID = nullable4;
              ARTran arTran4 = arTran2;
              nullable3 = new int?();
              int? nullable5 = nullable3;
              arTran4.CostCodeID = nullable5;
              arTran2.Qty = new Decimal?(0M);
              arTran2.ManualDisc = new bool?(true);
              arTran2.DiscPct = new Decimal?(0M);
              arTran2.CuryDiscAmt = new Decimal?(0M);
              arTran2.RetainagePct = new Decimal?(0M);
              arTran2.CuryRetainageAmt = new Decimal?(0M);
              arTran2.CuryTaxableAmt = new Decimal?(0M);
              arTran2.CuryTaxAmt = new Decimal?(0M);
              arTran2.GroupDiscountRate = new Decimal?(1M);
              arTran2.DocumentDiscountRate = new Decimal?(1M);
              arTran2.OrigLineNbr = arInvoiceExt2.LineNbr;
              arTran2.OrigDocType = arTran1.TranType;
              arTran2.OrigRefNbr = arTran1.RefNbr;
              using (new PXLocaleScope(customer.LocaleName))
                arTran2.TranDesc = PXMessages.LocalizeFormatNoPrefix("Retainage for {0} {1}", new object[2]
                {
                  (object) ARInvoiceEntry.ARDocTypeDict[arTran1.TranType],
                  (object) arTran1.RefNbr
                });
              Decimal? nullable6 = arInvoiceExt2.CuryRetainageUnreleasedCalcAmt;
              Decimal num7 = 0M;
              bool flag5 = nullable6.GetValueOrDefault() == num7 & nullable6.HasValue;
              Decimal num8;
              if (flag5)
              {
                PXResultset<ARTran> pxResultset2;
                if (!flag3)
                  pxResultset2 = PXSelectBase<ARTran, PXSelectJoin<ARTran, InnerJoin<ARRegister, On<ARRegister.docType, Equal<ARTran.tranType>, And<ARRegister.refNbr, Equal<ARTran.refNbr>>>>, Where<ARRegister.isRetainageDocument, Equal<True>, And<ARRegister.released, Equal<True>, And<ARTran.origDocType, Equal<Required<ARRegister.origDocType>>, And<ARTran.origRefNbr, Equal<Required<ARRegister.origRefNbr>>, And<Where<ARTran.taxCategoryID, Equal<Required<ARTran.taxCategoryID>>, Or<Required<ARTran.taxCategoryID>, IsNull>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[4]
                  {
                    (object) arInvoiceExt2.DocType,
                    (object) arInvoiceExt2.RefNbr,
                    (object) arTran1.TaxCategoryID,
                    (object) arTran1.TaxCategoryID
                  });
                else
                  pxResultset2 = PXSelectBase<ARTran, PXSelectJoin<ARTran, InnerJoin<ARRegister, On<ARRegister.docType, Equal<ARTran.tranType>, And<ARRegister.refNbr, Equal<ARTran.refNbr>>>>, Where<ARRegister.isRetainageDocument, Equal<True>, And<ARRegister.released, Equal<True>, And<ARTran.origDocType, Equal<Required<ARRegister.origDocType>>, And<ARTran.origRefNbr, Equal<Required<ARRegister.origRefNbr>>, And<ARTran.origLineNbr, Equal<Required<ARTran.origLineNbr>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
                  {
                    (object) arInvoiceExt2.DocType,
                    (object) arInvoiceExt2.RefNbr,
                    (object) arInvoiceExt2.LineNbr
                  });
                Decimal num9 = 0M;
                foreach (PXResult<ARTran, ARRegister> pxResult3 in pxResultset2)
                {
                  ARTran arTran5 = PXResult<ARTran, ARRegister>.op_Implicit(pxResult3);
                  ARRegister arRegister = PXResult<ARTran, ARRegister>.op_Implicit(pxResult3);
                  nullable6 = arInvoiceExt1.SignAmount;
                  Decimal? signAmount3 = arRegister.SignAmount;
                  Decimal valueOrDefault2 = (nullable6.HasValue & signAmount3.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * signAmount3.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
                  Decimal num10 = num9;
                  nullable6 = arTran5.CuryTranAmt;
                  Decimal num11 = nullable6.GetValueOrDefault() * valueOrDefault2;
                  num9 = num10 + num11;
                }
                nullable6 = arTran1.CuryRetainageAmt;
                num8 = (nullable6.GetValueOrDefault() - num9) * valueOrDefault1;
              }
              else
              {
                nullable6 = arInvoiceExt2.CuryRetainageReleasedAmt;
                Decimal? nullable7 = flag3 ? arTran1.CuryOrigRetainageAmt : arInvoiceExt1.CuryRetainageTotal;
                Decimal num12 = Math.Abs((nullable6.HasValue & nullable7.HasValue ? new Decimal?(nullable6.GetValueOrDefault() / nullable7.GetValueOrDefault()) : new Decimal?()).Value);
                IPXCurrencyHelper implementation = ((PXGraph) this.Base).FindImplementation<IPXCurrencyHelper>();
                nullable6 = arTran1.CuryRetainageAmt;
                Decimal val = nullable6.GetValueOrDefault() * num12 * valueOrDefault1;
                num8 = implementation.RoundCury(val);
              }
              arTran2.CuryExtPrice = new Decimal?(num8);
              ARTran arTran6 = ((PXSelectBase<ARTran>) this.Base.Transactions).Update(arTran2);
              if (invoice.PaymentsByLinesAllowed.GetValueOrDefault())
              {
                arTran6.IsStockItem = arTran1.IsStockItem;
                arTran6.InventoryID = arTran1.InventoryID;
                arTran6.ProjectID = arTran1.ProjectID;
                arTran6.TaskID = arTran1.TaskID;
                arTran6.CostCodeID = arTran1.CostCodeID;
              }
              if (flag3)
              {
                Dictionary<(string, string, int?), ARInvoiceEntryRetainage.ARTranRetainageData> dictionary3 = dictionary2;
                (string, string, int?) key = (arTran6.TranType, arTran6.RefNbr, arTran6.LineNbr);
                ARInvoiceEntryRetainage.ARTranRetainageData tranRetainageData = new ARInvoiceEntryRetainage.ARTranRetainageData();
                tranRetainageData.Detail = arTran6;
                Decimal? retainageReleasedAmt = arInvoiceExt2.CuryRetainageReleasedAmt;
                Decimal? signAmount4 = invoice.SignAmount;
                nullable6 = retainageReleasedAmt.HasValue & signAmount4.HasValue ? new Decimal?(retainageReleasedAmt.GetValueOrDefault() * signAmount4.GetValueOrDefault()) : new Decimal?();
                Decimal? curyExtPrice = arTran6.CuryExtPrice;
                tranRetainageData.RemainAmt = nullable6.HasValue & curyExtPrice.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - curyExtPrice.GetValueOrDefault()) : new Decimal?();
                tranRetainageData.IsFinal = flag5;
                dictionary3.Add(key, tranRetainageData);
              }
              else
              {
                if (tran1 != null)
                {
                  Decimal? curyExtPrice = tran1.CuryExtPrice;
                  Decimal num13 = Math.Abs(curyExtPrice.GetValueOrDefault());
                  curyExtPrice = arTran6.CuryExtPrice;
                  Decimal num14 = Math.Abs(curyExtPrice.GetValueOrDefault());
                  if (!(num13 < num14))
                    continue;
                }
                tran1 = arTran6;
              }
            }
            PXProcessing<ARInvoiceExt>.SetProcessed();
          }
          this.ClearCurrentDocumentDiscountDetails();
          PXResultset<ARTaxTran> pxResultset3 = PXSelectBase<ARTaxTran, PXSelectJoin<ARTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTaxTran.taxID>>>, Where<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<Required<ARTaxTran.tranType>>, And<ARTaxTran.refNbr, Equal<Required<ARTaxTran.refNbr>>, And<ARTaxTran.curyRetainedTaxAmt, NotEqual<decimal0>>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
          {
            (object) source2.Key.DocType,
            (object) source2.Key.RefNbr
          });
          Dictionary<string, ARTaxTran> insertedTaxes = (Dictionary<string, ARTaxTran>) null;
          insertedTaxes = new Dictionary<string, ARTaxTran>();
          EnumerableExtensions.ForEach<ARTaxTran>(GraphHelper.RowCast<ARTaxTran>((IEnumerable) pxResultset3), (Action<ARTaxTran>) (tax =>
          {
            Dictionary<string, ARTaxTran> dictionary4 = insertedTaxes;
            string taxId = tax.TaxID;
            PXSelectJoin<ARTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTaxTran.taxID>>>, Where<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARTaxTran.refNbr, Equal<Current<ARInvoice.refNbr>>>>>> taxes = this.Base.Taxes;
            ARTaxTran arTaxTran = ((PXSelectBase<ARTaxTran>) taxes).Insert(new ARTaxTran()
            {
              TaxID = tax.TaxID
            });
            dictionary4.Add(taxId, arTaxTran);
          }));
          foreach (PXResult<ARTaxTran, PX.Objects.TX.Tax> pxResult4 in pxResultset3)
          {
            ARTaxTran origARTaxTran = PXResult<ARTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult4);
            PX.Objects.TX.Tax tax = PXResult<ARTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult4);
            ARTaxTran new_artaxtran = insertedTaxes[origARTaxTran.TaxID];
            if (new_artaxtran != null)
            {
              Decimal? nullable8 = new_artaxtran.CuryTaxableAmt;
              Decimal num15 = 0M;
              if (nullable8.GetValueOrDefault() == num15 & nullable8.HasValue)
              {
                nullable8 = new_artaxtran.CuryTaxAmt;
                Decimal num16 = 0M;
                if (nullable8.GetValueOrDefault() == num16 & nullable8.HasValue)
                {
                  nullable8 = new_artaxtran.CuryExpenseAmt;
                  Decimal num17 = 0M;
                  if (nullable8.GetValueOrDefault() == num17 & nullable8.HasValue)
                    continue;
                }
              }
              Decimal num18 = 0M;
              if (flag3)
              {
                foreach (ARTax arTax1 in GraphHelper.RowCast<ARTax>((IEnumerable) ((PXSelectBase<ARTax>) this.Base.Tax_Rows).Select(Array.Empty<object>())).Where<ARTax>((Func<ARTax, bool>) (row => row.TaxID == origARTaxTran.TaxID)))
                {
                  ARInvoiceEntryRetainage.ARTranRetainageData tranRetainageData1 = dictionary2[(arTax1.TranType, arTax1.RefNbr, arTax1.LineNbr)];
                  ARTax arTax2 = PXResultset<ARTax>.op_Implicit(PXSelectBase<ARTax, PXSelect<ARTax, Where<ARTax.tranType, Equal<Required<ARTax.tranType>>, And<ARTax.refNbr, Equal<Required<ARTax.refNbr>>, And<ARTax.lineNbr, Equal<Required<ARTax.lineNbr>>, And<ARTax.taxID, Equal<Required<ARTax.taxID>>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[4]
                  {
                    (object) source2.Key.DocType,
                    (object) source2.Key.RefNbr,
                    (object) tranRetainageData1.Detail.OrigLineNbr,
                    (object) arTax1.TaxID
                  }));
                  Decimal num19;
                  if (tranRetainageData1.IsFinal)
                  {
                    PXResultset<ARTax> pxResultset4 = PXSelectBase<ARTax, PXSelectJoin<ARTax, InnerJoin<ARTranPost, On<ARTranPost.sourceDocType, Equal<ARTax.tranType>, And<ARTranPost.sourceRefNbr, Equal<ARTax.refNbr>, And<ARTranPost.type, Equal<ARTranPost.type.retainage>>>>, InnerJoin<ARRegister, On<ARRegister.docType, Equal<ARTranPost.sourceDocType>, And<ARRegister.refNbr, Equal<ARTranPost.sourceRefNbr>>>, InnerJoin<ARTran, On<ARTran.tranType, Equal<ARTax.tranType>, And<ARTran.refNbr, Equal<ARTax.refNbr>, And<ARTran.lineNbr, Equal<ARTax.lineNbr>>>>>>>, Where<ARRegister.isRetainageDocument, Equal<True>, And<ARRegister.released, Equal<True>, And<ARTranPost.docType, Equal<Required<ARTranPost.docType>>, And<ARTranPost.refNbr, Equal<Required<ARTranPost.refNbr>>, And<ARTran.origDocType, Equal<Required<ARTranPost.docType>>, And<ARTran.origRefNbr, Equal<Required<ARTranPost.refNbr>>, And<ARTran.origLineNbr, Equal<Required<ARTran.origLineNbr>>, And<ARTax.taxID, Equal<Required<ARTax.taxID>>>>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[6]
                    {
                      (object) tranRetainageData1.Detail.OrigDocType,
                      (object) tranRetainageData1.Detail.OrigRefNbr,
                      (object) tranRetainageData1.Detail.OrigDocType,
                      (object) tranRetainageData1.Detail.OrigRefNbr,
                      (object) tranRetainageData1.Detail.OrigLineNbr,
                      (object) arTax1.TaxID
                    });
                    Decimal num20 = 0M;
                    foreach (PXResult<ARTax, ARTranPost, ARRegister> pxResult5 in pxResultset4)
                    {
                      ARTax arTax3 = PXResult<ARTax, ARTranPost, ARRegister>.op_Implicit(pxResult5);
                      ARRegister arRegister = PXResult<ARTax, ARTranPost, ARRegister>.op_Implicit(pxResult5);
                      nullable8 = arInvoiceExt1.SignAmount;
                      Decimal? signAmount5 = arRegister.SignAmount;
                      Decimal valueOrDefault3 = (nullable8.HasValue & signAmount5.HasValue ? new Decimal?(nullable8.GetValueOrDefault() * signAmount5.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
                      Decimal num21 = num20;
                      nullable8 = arTax3.CuryTaxAmt;
                      Decimal valueOrDefault4 = nullable8.GetValueOrDefault();
                      nullable8 = arTax3.CuryExpenseAmt;
                      Decimal valueOrDefault5 = nullable8.GetValueOrDefault();
                      Decimal num22 = (valueOrDefault4 + valueOrDefault5) * valueOrDefault3;
                      num20 = num21 + num22;
                    }
                    nullable8 = arTax2.CuryRetainedTaxAmt;
                    num19 = (nullable8.GetValueOrDefault() - num20) * valueOrDefault1;
                  }
                  else
                  {
                    nullable8 = arTax2.CuryRetainedTaxAmt;
                    Decimal num23 = nullable8.Value;
                    nullable8 = arTax2.CuryRetainedTaxableAmt;
                    Decimal num24 = nullable8.Value;
                    Decimal num25 = Math.Abs(num23 / num24);
                    IPXCurrencyHelper implementation = ((PXGraph) this.Base).FindImplementation<IPXCurrencyHelper>();
                    nullable8 = arTax1.CuryTaxableAmt;
                    Decimal val = nullable8.Value * num25;
                    num19 = implementation.RoundCury(val);
                  }
                  num18 += num19;
                  ARTax copy = PXCache<ARTax>.CreateCopy(arTax1);
                  nullable8 = copy.NonDeductibleTaxRate;
                  Decimal num26 = 100M - (nullable8 ?? 100M);
                  copy.CuryExpenseAmt = new Decimal?(((PXGraph) this.Base).FindImplementation<IPXCurrencyHelper>().RoundCury(num19 * num26 / 100M));
                  ARTax arTax4 = copy;
                  Decimal num27 = num19;
                  nullable8 = copy.CuryExpenseAmt;
                  Decimal? nullable9 = nullable8.HasValue ? new Decimal?(num27 - nullable8.GetValueOrDefault()) : new Decimal?();
                  arTax4.CuryTaxAmt = nullable9;
                  ((PXSelectBase<ARTax>) this.Base.Tax_Rows).Update(copy);
                  if (tax != null && tax.TaxType != "P" && tax.TaxType != "W" && !(invoice.TaxCalcMode == "G") && (!(tax.TaxCalcLevel == "0") || !(invoice.TaxCalcMode != "N")))
                  {
                    ARInvoiceEntryRetainage.ARTranRetainageData tranRetainageData2 = tranRetainageData1;
                    nullable8 = tranRetainageData2.RemainAmt;
                    Decimal num28 = num19;
                    tranRetainageData2.RemainAmt = nullable8.HasValue ? new Decimal?(nullable8.GetValueOrDefault() - num28) : new Decimal?();
                  }
                }
              }
              else if (flag4)
              {
                PXResultset<ARTax> pxResultset5 = PXSelectBase<ARTax, PXSelectJoin<ARTax, InnerJoin<ARTranPost, On<ARTax.tranType, Equal<ARTranPost.sourceDocType>, And<ARTranPost.sourceRefNbr, Equal<ARTax.refNbr>, And<ARTranPost.type, Equal<ARTranPost.type.retainage>>>>, LeftJoin<ARRegister, On<ARRegister.docType, Equal<ARTranPost.sourceDocType>, And<ARRegister.refNbr, Equal<ARTranPost.sourceRefNbr>>>, InnerJoin<ARTran, On<ARTran.tranType, Equal<ARTax.tranType>, And<ARTran.refNbr, Equal<ARTax.refNbr>, And<ARTran.lineNbr, Equal<ARTax.lineNbr>>>>>>>, Where<ARRegister.isRetainageDocument, Equal<True>, And<ARRegister.released, Equal<True>, And<ARTranPost.docType, Equal<Required<ARTranPost.docType>>, And<ARTranPost.refNbr, Equal<Required<ARTranPost.refNbr>>, And<ARTran.origDocType, Equal<Required<ARTranPost.docType>>, And<ARTran.origRefNbr, Equal<Required<ARTranPost.refNbr>>, And<ARTax.taxID, Equal<Required<ARTax.taxID>>>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[5]
                {
                  (object) origARTaxTran.TranType,
                  (object) origARTaxTran.RefNbr,
                  (object) origARTaxTran.TranType,
                  (object) origARTaxTran.RefNbr,
                  (object) origARTaxTran.TaxID
                });
                Decimal num29 = 0M;
                foreach (PXResult<ARTax, ARTranPost, ARRegister> pxResult6 in pxResultset5)
                {
                  ARTax arTax = PXResult<ARTax, ARTranPost, ARRegister>.op_Implicit(pxResult6);
                  ARRegister arRegister = PXResult<ARTax, ARTranPost, ARRegister>.op_Implicit(pxResult6);
                  nullable8 = arInvoiceExt1.SignAmount;
                  Decimal? signAmount6 = arRegister.SignAmount;
                  Decimal valueOrDefault6 = (nullable8.HasValue & signAmount6.HasValue ? new Decimal?(nullable8.GetValueOrDefault() * signAmount6.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
                  Decimal num30 = num29;
                  nullable8 = arTax.CuryTaxAmt;
                  Decimal valueOrDefault7 = nullable8.GetValueOrDefault();
                  nullable8 = arTax.CuryExpenseAmt;
                  Decimal valueOrDefault8 = nullable8.GetValueOrDefault();
                  Decimal num31 = (valueOrDefault7 + valueOrDefault8) * valueOrDefault6;
                  num29 = num30 + num31;
                }
                num18 = (origARTaxTran.CuryRetainedTaxAmt.GetValueOrDefault() - num29) * valueOrDefault1;
              }
              else
              {
                ARTax arTax = PXResultset<ARTax>.op_Implicit(PXSelectBase<ARTax, PXSelectGroupBy<ARTax, Where<ARTax.tranType, Equal<Required<ARTax.tranType>>, And<ARTax.refNbr, Equal<Required<ARTax.refNbr>>, And<ARTax.taxID, Equal<Required<ARTax.taxID>>>>>, Aggregate<GroupBy<ARTax.tranType, GroupBy<ARTax.refNbr, GroupBy<ARTax.taxID, Sum<ARTax.curyRetainedTaxableAmt>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[3]
                {
                  (object) origARTaxTran.TranType,
                  (object) origARTaxTran.RefNbr,
                  (object) origARTaxTran.TaxID
                }));
                nullable8 = origARTaxTran.CuryRetainedTaxAmt;
                Decimal num32 = nullable8.Value;
                nullable8 = arTax.CuryRetainedTaxableAmt;
                Decimal num33 = nullable8.Value;
                Decimal num34 = Math.Abs(num32 / num33);
                IPXCurrencyHelper implementation = ((PXGraph) this.Base).FindImplementation<IPXCurrencyHelper>();
                nullable8 = new_artaxtran.CuryTaxableAmt;
                Decimal val = nullable8.Value * num34;
                num18 = implementation.RoundCury(val);
              }
              new_artaxtran = PXCache<ARTaxTran>.CreateCopy(new_artaxtran);
              nullable8 = new_artaxtran.CuryTaxAmt;
              Decimal valueOrDefault9 = nullable8.GetValueOrDefault();
              nullable8 = new_artaxtran.CuryExpenseAmt;
              Decimal valueOrDefault10 = nullable8.GetValueOrDefault();
              Decimal num35 = valueOrDefault9 + valueOrDefault10 - num18;
              if ((tax?.TaxCalcLevel == "0" && invoice.TaxCalcMode != "N" || invoice.TaxCalcMode == "G") && num35 != 0M)
              {
                ARTaxTran arTaxTran1 = new_artaxtran;
                nullable8 = arTaxTran1.CuryTaxableAmt;
                Decimal num36 = num35;
                arTaxTran1.CuryTaxableAmt = nullable8.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + num36) : new Decimal?();
                foreach (ARTax arTax5 in GraphHelper.RowCast<ARTax>((IEnumerable) ((IEnumerable<PXResult<ARTax>>) ((PXSelectBase<ARTax>) this.Base.Tax_Rows).Select(Array.Empty<object>())).AsEnumerable<PXResult<ARTax>>()).Where<ARTax>((Func<ARTax, bool>) (row => row.TaxID == new_artaxtran.TaxID)))
                {
                  ARTax roundARTax = arTax5;
                  ARTax copy1 = PXCache<ARTax>.CreateCopy(roundARTax);
                  ARTax arTax6 = copy1;
                  nullable8 = arTax6.CuryTaxableAmt;
                  Decimal num37 = num35;
                  arTax6.CuryTaxableAmt = nullable8.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + num37) : new Decimal?();
                  ((PXSelectBase<ARTax>) this.Base.Tax_Rows).Update(copy1);
                  foreach (ARTax arTax7 in GraphHelper.RowCast<ARTax>((IEnumerable) ((IEnumerable<PXResult<ARTax>>) ((PXSelectBase<ARTax>) this.Base.Tax_Rows).Select(Array.Empty<object>())).AsEnumerable<PXResult<ARTax>>()).Where<ARTax>((Func<ARTax, bool>) (row =>
                  {
                    if (!(row.TaxID != roundARTax.TaxID))
                      return false;
                    int? lineNbr1 = row.LineNbr;
                    int? lineNbr2 = roundARTax.LineNbr;
                    return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
                  })))
                  {
                    ARTaxTran arTaxTran2 = insertedTaxes[arTax7.TaxID];
                    ARTaxTran arTaxTran3 = arTaxTran2;
                    nullable8 = arTaxTran3.CuryTaxableAmt;
                    Decimal num38 = num35;
                    arTaxTran3.CuryTaxableAmt = nullable8.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + num38) : new Decimal?();
                    ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Update(arTaxTran2);
                    ARTax copy2 = PXCache<ARTax>.CreateCopy(arTax7);
                    ARTax arTax8 = copy2;
                    nullable8 = arTax8.CuryTaxableAmt;
                    Decimal num39 = num35;
                    arTax8.CuryTaxableAmt = nullable8.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + num39) : new Decimal?();
                    ((PXSelectBase<ARTax>) this.Base.Tax_Rows).Update(copy2);
                  }
                }
              }
              new_artaxtran.TaxRate = origARTaxTran.TaxRate;
              nullable8 = new_artaxtran.NonDeductibleTaxRate;
              Decimal num40 = 100M - (nullable8 ?? 100M);
              new_artaxtran.CuryExpenseAmt = new Decimal?(((PXGraph) this.Base).FindImplementation<IPXCurrencyHelper>().RoundCury(num18 * num40 / 100M));
              ARTaxTran arTaxTran = new_artaxtran;
              Decimal num41 = num18;
              nullable8 = new_artaxtran.CuryExpenseAmt;
              Decimal? nullable10 = nullable8.HasValue ? new Decimal?(num41 - nullable8.GetValueOrDefault()) : new Decimal?();
              arTaxTran.CuryTaxAmt = nullable10;
              new_artaxtran.CuryTaxAmtSumm = new_artaxtran.CuryTaxAmt;
              new_artaxtran = ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Update(new_artaxtran);
            }
          }
          Decimal? nullable11;
          if (flag3)
            EnumerableExtensions.ForEach<ARInvoiceEntryRetainage.ARTranRetainageData>(dictionary2.Values.Where<ARInvoiceEntryRetainage.ARTranRetainageData>((Func<ARInvoiceEntryRetainage.ARTranRetainageData, bool>) (value =>
            {
              Decimal? remainAmt = value.RemainAmt;
              Decimal num42 = 0M;
              return !(remainAmt.GetValueOrDefault() == num42 & remainAmt.HasValue);
            })), (Action<ARInvoiceEntryRetainage.ARTranRetainageData>) (value => this.ProcessRoundingDiff(value.RemainAmt.GetValueOrDefault(), value.Detail)));
          else if (tran1 != null)
          {
            Decimal num43 = Math.Abs(num2);
            nullable11 = invoice.CuryDocBal;
            Decimal valueOrDefault11 = nullable11.GetValueOrDefault();
            Decimal diff = num43 - valueOrDefault11;
            if (diff != 0M)
              this.ProcessRoundingDiff(diff, tran1);
          }
          if (flag2)
          {
            List<ARTax> tempArTax = new List<ARTax>();
            List<ARTaxTran> tempArTaxTran = new List<ARTaxTran>();
            EnumerableExtensions.ForEach<PXResult<ARTaxTran>>((IEnumerable<PXResult<ARTaxTran>>) ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Select(Array.Empty<object>()), (Action<PXResult<ARTaxTran>>) (a => tempArTaxTran.Add(PXResult<ARTaxTran>.op_Implicit(a))));
            tempArTaxTran.ForEach((Action<ARTaxTran>) (a => arTaxTranList.Add(a)));
            EnumerableExtensions.ForEach<ARTax>(GraphHelper.RowCast<ARTax>((IEnumerable) ((PXSelectBase<ARTax>) this.Base.Tax_Rows).Select(Array.Empty<object>())), (Action<ARTax>) (a => tempArTax.Add(a)));
            if (!invoice.PaymentsByLinesAllowed.GetValueOrDefault())
            {
              foreach (ARTax arTax9 in tempArTax)
              {
                ARTax tax = arTax9;
                ARTax arTax10 = tax;
                nullable11 = tempArTaxTran.Where<ARTaxTran>((Func<ARTaxTran, bool>) (a => a.TaxID == tax.TaxID)).First<ARTaxTran>().CuryTaxAmt;
                Decimal? nullable12 = new Decimal?(nullable11.GetValueOrDefault());
                arTax10.CuryTaxAmt = nullable12;
              }
            }
            foreach (ARTran arTran in GraphHelper.RowCast<ARTran>((IEnumerable) ((PXSelectBase<ARTran>) this.Base.Transactions).Select(Array.Empty<object>())))
            {
              ARTran tran = arTran;
              if (dictionary1.Keys.Contains<int?>(tran.LineNbr))
              {
                int ind = dictionary1.Keys.Count<int?>() + 1;
                EnumerableExtensions.ForEach<ARTax>(tempArTax.Where<ARTax>((Func<ARTax, bool>) (a =>
                {
                  int? lineNbr3 = a.LineNbr;
                  int? lineNbr4 = tran.LineNbr;
                  return lineNbr3.GetValueOrDefault() == lineNbr4.GetValueOrDefault() & lineNbr3.HasValue == lineNbr4.HasValue;
                })), (Action<ARTax>) (a =>
                {
                  ARTax copy = PXCache<ARTax>.CreateCopy(a);
                  copy.LineNbr = new int?(ind);
                  arTaxList.Add(copy);
                }));
                tran.LineNbr = new int?(ind);
                dictionary1.Add(new int?(ind), tran);
                Dictionary<int?, Decimal> dictionary5 = signList;
                int? lineNbr = tran.LineNbr;
                nullable11 = invoice.SignAmount;
                Decimal num44 = nullable11 ?? 1M;
                dictionary5.Add(lineNbr, num44);
              }
              else
              {
                dictionary1.Add(tran.LineNbr, tran);
                EnumerableExtensions.ForEach<ARTax>(tempArTax.Where<ARTax>((Func<ARTax, bool>) (a =>
                {
                  int? lineNbr5 = a.LineNbr;
                  int? lineNbr6 = tran.LineNbr;
                  return lineNbr5.GetValueOrDefault() == lineNbr6.GetValueOrDefault() & lineNbr5.HasValue == lineNbr6.HasValue;
                })), (Action<ARTax>) (a => arTaxList.Add(a)));
                Dictionary<int?, Decimal> dictionary6 = signList;
                int? lineNbr = tran.LineNbr;
                nullable11 = invoice.SignAmount;
                Decimal num45 = nullable11 ?? 1M;
                dictionary6.Add(lineNbr, num45);
              }
            }
          }
        }
        if (flag2)
        {
          ARInvoiceExt arInvoiceExt = source1.First<ARInvoiceExt>();
          ((PXGraph) this.Base).Clear((PXClearOption) 3);
          PXUIFieldAttribute.SetError(((PXSelectBase) this.Base.Document).Cache, (object) null, (string) null, (string) null);
          taxCalc = TaxBaseAttribute.GetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null);
          ((PXGraph) this.Base).Clear((PXClearOption) 1);
          ((PXSelectBase) this.Base.CurrentDocument).Cache.AllowUpdate = true;
          PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer> pxResult7 = ((IEnumerable<PXResult<ARInvoice>>) PXSelectBase<ARInvoice, PXSelectJoin<ARInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARInvoice.curyInfoID>>, LeftJoin<PX.Objects.CS.Terms, On<PX.Objects.CS.Terms.termsID, Equal<ARInvoice.termsID>>, LeftJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, LeftJoin<PX.Objects.GL.Account, On<ARInvoice.aRAccountID, Equal<PX.Objects.GL.Account.accountID>>>>>>, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[2]
          {
            (object) arInvoiceExt.DocType,
            (object) arInvoiceExt.RefNbr
          })).AsEnumerable<PXResult<ARInvoice>>().Cast<PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>>().First<PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>>();
          ARInvoice arInvoice = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>.op_Implicit(pxResult7);
          Customer customer = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>.op_Implicit(pxResult7);
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = ((PXGraph) this.Base).GetExtension<ARInvoiceEntry.MultiCurrency>().CloneCurrencyInfo(PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>.op_Implicit(pxResult7));
          currencyInfo3.IsReadOnly = new bool?(false);
          invoice = PXCache<ARInvoice>.CreateCopy(arInvoice);
          object obj = ((PXSelectBase) this.Base.CurrentDocument).Cache.GetValue<ARInvoice.ownerID>((object) arInvoice);
          try
          {
            ((PXSelectBase) this.Base.CurrentDocument).Cache.RaiseFieldVerifying<ARInvoice.ownerID>((object) arInvoice, ref obj);
          }
          catch (Exception ex)
          {
            invoice.OwnerID = new int?();
          }
          invoice.CuryInfoID = currencyInfo3.CuryInfoID;
          if (num1 != 0M)
            invoice.DocType = num1 < 0M ? "CRM" : "INV";
          invoice.RefNbr = (string) null;
          invoice.LineCntr = new int?();
          PMProject project = service.GetProject((PXGraph) this.Base, arInvoice.ProjectID);
          invoice.DocDesc = $"Retainage for project: {project.ContractCD.TrimEnd()} - {project.Description.TrimEnd()}";
          invoice.InvoiceNbr = arInvoice.InvoiceNbr;
          invoice.OpenDoc = new bool?(true);
          invoice.Released = new bool?(false);
          ((PXSelectBase) this.Base.Document).Cache.SetDefaultExt<ARInvoice.isMigratedRecord>((object) invoice);
          ((PXSelectBase) this.Base.Document).Cache.SetDefaultExt<ARInvoice.hold>((object) invoice);
          invoice.BatchNbr = (string) null;
          invoice.ScheduleID = (string) null;
          invoice.Scheduled = new bool?(false);
          invoice.NoteID = new Guid?();
          invoice.TermsID = invoice.DocType == "CRM" ? (string) null : customer.TermsID;
          invoice.DueDate = new DateTime?();
          invoice.DiscDate = new DateTime?();
          invoice.CuryOrigDiscAmt = new Decimal?(0M);
          invoice.OrigDocType = arInvoice.DocType;
          invoice.OrigRefNbr = arInvoice.RefNbr;
          invoice.OrigDocDate = arInvoice.DocDate;
          invoice.OrigRefNbr = (string) null;
          invoice.OrigDocType = (string) null;
          invoice.CuryLineTotal = new Decimal?(0M);
          invoice.IsTaxPosted = new bool?(false);
          invoice.IsTaxValid = new bool?(false);
          invoice.CuryVatTaxableTotal = new Decimal?(0M);
          invoice.CuryVatExemptTotal = new Decimal?(0M);
          invoice.CuryDetailExtPriceTotal = new Decimal?(0M);
          invoice.DetailExtPriceTotal = new Decimal?(0M);
          invoice.CuryLineDiscTotal = new Decimal?(0M);
          invoice.LineDiscTotal = new Decimal?(0M);
          invoice.CuryMiscExtPriceTotal = new Decimal?(0M);
          invoice.MiscExtPriceTotal = new Decimal?(0M);
          invoice.CuryGoodsExtPriceTotal = new Decimal?(0M);
          invoice.GoodsExtPriceTotal = new Decimal?(0M);
          invoice.CuryDocBal = new Decimal?(0M);
          ((PXSelectBase<ARInvoice>) this.Base.Document).SetValueExt<ARInvoice.curyOrigDocAmt>(invoice, (object) Math.Abs(num1));
          invoice.Hold = new bool?(!isAutoRelease && ((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.HoldEntry.GetValueOrDefault() || this.Base.IsApprovalRequired(invoice));
          invoice.DocDate = retainageOpts.DocDate;
          FinPeriodIDAttribute.SetPeriodsByMaster<ARInvoice.finPeriodID>(((PXSelectBase) this.Base.Document).Cache, (object) invoice, retainageOpts.MasterFinPeriodID);
          this.Base.ClearRetainageSummary(invoice);
          invoice.RetainageApply = new bool?(false);
          invoice.IsRetainageDocument = new bool?(true);
          invoice = ((PXSelectBase<ARInvoice>) this.Base.Document).Insert(invoice);
          Decimal origSign = invoice.SignAmount.GetValueOrDefault();
          if (currencyInfo3 != null)
          {
            PX.Objects.CM.Extensions.CurrencyInfo currencyInfo4 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<ARInvoice.curyInfoID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
            currencyInfo4.CuryID = currencyInfo3.CuryID;
            currencyInfo4.CuryEffDate = currencyInfo3.CuryEffDate;
            currencyInfo4.CuryRateTypeID = currencyInfo3.CuryRateTypeID;
            currencyInfo4.CuryRate = currencyInfo3.CuryRate;
            currencyInfo4.RecipRate = currencyInfo3.RecipRate;
            currencyInfo4.CuryMultDiv = currencyInfo3.CuryMultDiv;
            ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.Base.currencyinfo).Update(currencyInfo4);
          }
          invoice.PaymentsByLinesAllowed = new bool?(true);
          foreach (ARTran arTran7 in dictionary1.Values)
          {
            ARTran arTran8 = ((PXSelectBase<ARTran>) this.Base.Transactions).Insert(new ARTran()
            {
              CuryUnitPrice = new Decimal?(0M),
              CuryExtPrice = new Decimal?(0M),
              BranchID = arTran7.BranchID,
              AccountID = arTran7.AccountID,
              SubID = arTran7.SubID,
              ProjectID = ProjectDefaultAttribute.NonProject(),
              InventoryID = arTran7.InventoryID,
              IsStockItem = arTran7.IsStockItem
            });
            arTran8.TaxCategoryID = arTran7.TaxCategoryID;
            arTran8.TaskID = new int?();
            arTran8.CostCodeID = new int?();
            arTran8.Qty = new Decimal?(0M);
            arTran8.ManualDisc = new bool?(true);
            arTran8.DiscPct = new Decimal?(0M);
            arTran8.CuryDiscAmt = new Decimal?(0M);
            arTran8.RetainagePct = new Decimal?(0M);
            arTran8.CuryRetainageAmt = new Decimal?(0M);
            arTran8.CuryTaxableAmt = new Decimal?(0M);
            arTran8.CuryTaxAmt = new Decimal?(0M);
            arTran8.GroupDiscountRate = new Decimal?(1M);
            arTran8.DocumentDiscountRate = new Decimal?(1M);
            arTran8.OrigLineNbr = arTran7.OrigLineNbr;
            arTran8.OrigDocType = arTran7.OrigDocType;
            arTran8.OrigRefNbr = arTran7.OrigRefNbr;
            arTran8.TranDesc = arTran7.TranDesc;
            arTran8.ProjectID = arTran7.ProjectID;
            arTran8.TaskID = arTran7.TaskID;
            arTran8.CostCodeID = arTran7.CostCodeID;
            ARTran arTran9 = arTran8;
            Decimal? curyExtPrice = arTran7.CuryExtPrice;
            Decimal num46 = origSign;
            Decimal? nullable13 = curyExtPrice.HasValue ? new Decimal?(curyExtPrice.GetValueOrDefault() * num46) : new Decimal?();
            Decimal num47 = signList[arTran7.LineNbr];
            Decimal? nullable14 = nullable13.HasValue ? new Decimal?(nullable13.GetValueOrDefault() * num47) : new Decimal?();
            arTran9.CuryExtPrice = nullable14;
            ((PXSelectBase<ARTran>) this.Base.Transactions).Update(arTran8);
          }
          foreach (List<ARTaxTran> source3 in arTaxTranList.GroupBy(x => new
          {
            TaxID = x.TaxID
          }).Select<IGrouping<\u003C\u003Ef__AnonymousType17<string>, ARTaxTran>, List<ARTaxTran>>(x => x.ToList<ARTaxTran>()))
          {
            PXSelectJoin<ARTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTaxTran.taxID>>>, Where<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARTaxTran.refNbr, Equal<Current<ARInvoice.refNbr>>>>>> taxes = this.Base.Taxes;
            ARTaxTran arTaxTran = new ARTaxTran();
            arTaxTran.TaxID = source3.First<ARTaxTran>().TaxID;
            ((PXSelectBase<ARTaxTran>) taxes).Insert(arTaxTran);
          }
          ((PXSelectBase) this.Base.Tax_Rows).Cache.Clear();
          arTaxList.ForEach((Action<ARTax>) (a =>
          {
            ARTax arTax11 = a;
            Decimal? nullable15 = a.CuryTaxableAmt;
            Decimal num48 = origSign;
            Decimal? nullable16 = nullable15.HasValue ? new Decimal?(nullable15.GetValueOrDefault() * num48) : new Decimal?();
            Decimal num49 = signList[a.LineNbr];
            Decimal? nullable17;
            if (!nullable16.HasValue)
            {
              nullable15 = new Decimal?();
              nullable17 = nullable15;
            }
            else
              nullable17 = new Decimal?(nullable16.GetValueOrDefault() * num49);
            arTax11.CuryTaxableAmt = nullable17;
            ARTax arTax12 = a;
            nullable15 = a.CuryTaxAmt;
            Decimal num50 = origSign;
            nullable16 = nullable15.HasValue ? new Decimal?(nullable15.GetValueOrDefault() * num50) : new Decimal?();
            Decimal num51 = signList[a.LineNbr];
            Decimal? nullable18;
            if (!nullable16.HasValue)
            {
              nullable15 = new Decimal?();
              nullable18 = nullable15;
            }
            else
              nullable18 = new Decimal?(nullable16.GetValueOrDefault() * num51);
            arTax12.CuryTaxAmt = nullable18;
            a.TranType = invoice.DocType;
            ((PXSelectBase<ARTax>) this.Base.Tax_Rows).Insert(a);
          }));
          foreach (PXResult<ARTaxTran> pxResult8 in ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Select(Array.Empty<object>()))
          {
            ARTaxTran taxtran = PXResult<ARTaxTran>.op_Implicit(pxResult8);
            Decimal num52 = arTaxList.Where<ARTax>((Func<ARTax, bool>) (a => a.TaxID == taxtran.TaxID)).Sum<ARTax>((Func<ARTax, Decimal>) (row => row.CuryTaxableAmt.GetValueOrDefault()));
            Decimal num53 = arTaxList.Where<ARTax>((Func<ARTax, bool>) (a => a.TaxID == taxtran.TaxID)).Sum<ARTax>((Func<ARTax, Decimal>) (row => row.CuryTaxAmt.GetValueOrDefault()));
            taxtran.CuryTaxableAmt = new Decimal?(num52);
            taxtran.CuryTaxAmt = new Decimal?(num53);
            taxtran.CuryTaxAmtSumm = new Decimal?(num53);
            ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Update(taxtran);
          }
        }
        TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, taxCalc);
        if (!isAutoRelease)
        {
          bool? holdEntry = ((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.HoldEntry;
          bool flag6 = false;
          if (holdEntry.GetValueOrDefault() == flag6 & holdEntry.HasValue && this.Base.IsApprovalRequired(invoice))
            ((PXAction) this.Base.releaseFromHold).Press();
        }
        ((PXAction) this.Base.Save).Press();
        if (isAutoRelease)
        {
          if (!invoice.Hold.GetValueOrDefault())
          {
            using (new PXTimeStampScope((byte[]) null))
              ARDocumentRelease.ReleaseDoc(new List<ARRegister>()
              {
                (ARRegister) invoice
              }, false);
          }
        }
      }
      catch (PXException ex)
      {
        PXProcessing<ARInvoiceExt>.SetError((Exception) ex);
        flag1 = true;
      }
    }
    if (flag1)
      throw new PXOperationCompletedWithErrorException("One or more documents could not be released.");
  }

  private void ProcessRoundingDiff(Decimal diff, ARTran tran)
  {
    ARTran arTran = tran;
    Decimal? curyExtPrice = arTran.CuryExtPrice;
    Decimal num1 = diff;
    arTran.CuryExtPrice = curyExtPrice.HasValue ? new Decimal?(curyExtPrice.GetValueOrDefault() + num1) : new Decimal?();
    tran = ((PXSelectBase<ARTran>) this.Base.Transactions).Update(tran);
    foreach (IGrouping<\u003C\u003Ef__AnonymousType6<string, string, string>, ARTax> grouping in GraphHelper.RowCast<ARTax>((IEnumerable) ((IEnumerable<PXResult<ARTax>>) ((PXSelectBase<ARTax>) this.Base.Tax_Rows).Select(Array.Empty<object>())).AsEnumerable<PXResult<ARTax>>()).Where<ARTax>((Func<ARTax, bool>) (row =>
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
      foreach (ARTax arTax1 in (IEnumerable<ARTax>) grouping)
      {
        ARTax copy = PXCache<ARTax>.CreateCopy(arTax1);
        ARTax arTax2 = copy;
        curyTaxableAmt = arTax2.CuryTaxableAmt;
        Decimal num2 = diff;
        arTax2.CuryTaxableAmt = curyTaxableAmt.HasValue ? new Decimal?(curyTaxableAmt.GetValueOrDefault() + num2) : new Decimal?();
        ((PXSelectBase<ARTax>) this.Base.Tax_Rows).Update(copy);
      }
      ARTaxTran arTaxTran1 = PXResultset<ARTaxTran>.op_Implicit(PXSelectBase<ARTaxTran, PXSelect<ARTaxTran, Where<ARTaxTran.tranType, Equal<Required<ARTaxTran.tranType>>, And<ARTaxTran.refNbr, Equal<Required<ARTaxTran.refNbr>>, And<ARTaxTran.taxID, Equal<Required<ARTaxTran.taxID>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[3]
      {
        (object) grouping.Key.TranType,
        (object) grouping.Key.RefNbr,
        (object) grouping.Key.TaxID
      }));
      if (arTaxTran1 != null)
      {
        ARTaxTran copy = PXCache<ARTaxTran>.CreateCopy(arTaxTran1);
        ARTaxTran arTaxTran2 = copy;
        curyTaxableAmt = arTaxTran2.CuryTaxableAmt;
        Decimal num3 = diff;
        arTaxTran2.CuryTaxableAmt = curyTaxableAmt.HasValue ? new Decimal?(curyTaxableAmt.GetValueOrDefault() + num3) : new Decimal?();
        ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Update(copy);
      }
    }
  }

  [PXUIField]
  [PXLookupButton]
  protected virtual IEnumerable viewRetainageDocument(PXAdapter adapter)
  {
    RedirectionToOrigDoc.TryRedirect(((PXSelectBase<ARRetainageWithApplications>) this.RetainageDocuments).Current.DocType, ((PXSelectBase<ARRetainageWithApplications>) this.RetainageDocuments).Current.RefNbr, ((PXSelectBase<ARRetainageWithApplications>) this.RetainageDocuments).Current.OrigModule, true);
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewOrigRetainageDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<ARTran>) this.Base.Transactions).Current != null)
      RedirectionToOrigDoc.TryRedirect(((PXSelectBase<ARTran>) this.Base.Transactions).Current.OrigDocType, ((PXSelectBase<ARTran>) this.Base.Transactions).Current.OrigRefNbr, ((PXSelectBase<ARInvoice>) this.Base.Document).Current.OrigModule, true);
    return adapter.Get();
  }

  public delegate void AddDiscountDelegate(PXCache sender, ARInvoice row);

  public class ARTranRetainageData
  {
    public ARTran Detail;
    public Decimal? RemainAmt;
    public bool IsFinal;
  }
}
