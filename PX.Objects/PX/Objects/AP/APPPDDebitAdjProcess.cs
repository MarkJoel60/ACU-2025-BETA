// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPPDDebitAdjProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.AP.MigrationMode;
using PX.Objects.AP.Standalone;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Abstractions;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.GL.Exceptions;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class APPPDDebitAdjProcess : PXGraph<APPPDDebitAdjProcess>
{
  private const bool AutoReleaseDebitAdjustments = true;
  private const bool AutoReleaseCreditAdjustments = true;
  public PXCancel<APPPDVATAdjParameters> Cancel;
  public PXFilter<APPPDVATAdjParameters> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<PendingPPDVATAdjApp, APPPDVATAdjParameters> Applications;
  public APSetupNoMigrationMode apsetup;
  public PXAction<APPPDVATAdjParameters> viewPayment;
  public PXAction<APPPDVATAdjParameters> viewInvoice;

  public override bool IsDirty => false;

  [PXUIField(MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable ViewPayment(PXAdapter adapter)
  {
    if (this.Applications.Current != null)
    {
      APPayment row = (APPayment) PXSelectBase<APPayment, PXSelect<APPayment, Where<APPayment.refNbr, Equal<Current<PendingPPDVATAdjApp.payRefNbr>>, And<APPayment.docType, Equal<Current<PendingPPDVATAdjApp.payDocType>>>>>.Config>.Select((PXGraph) this).First<PXResult<APPayment>>();
      if (row != null)
        PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<APPaymentEntry>(), (object) row, PXRedirectHelper.WindowMode.NewWindow);
    }
    return adapter.Get();
  }

  [PXUIField(MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable ViewInvoice(PXAdapter adapter)
  {
    if (this.Applications.Current != null)
    {
      APInvoice row = (APInvoice) PXSelectBase<APInvoice, PXSelect<APInvoice, Where<APInvoice.refNbr, Equal<Current<PendingPPDVATAdjApp.invRefNbr>>, And<APInvoice.docType, Equal<Current<PendingPPDVATAdjApp.invDocType>>>>>.Config>.Select((PXGraph) this).First<PXResult<APInvoice>>();
      if (row != null)
        PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<APInvoiceEntry>(), (object) row, PXRedirectHelper.WindowMode.NewWindow);
    }
    return adapter.Get();
  }

  [Vendor]
  protected virtual void PendingPPDVATAdjApp_VendorID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
  [APInvoiceType.RefNbr(typeof (Search2<APRegisterAlias.refNbr, InnerJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APRegisterAlias.docType>, And<APInvoice.refNbr, Equal<APRegisterAlias.refNbr>>>, InnerJoinSingleTable<Vendor, On<APRegisterAlias.vendorID, Equal<Vendor.bAccountID>>>>, Where<APRegisterAlias.docType, Equal<Optional<PendingPPDVATAdjApp.invDocType>>, And2<Where<APRegisterAlias.origModule, Equal<BatchModule.moduleAP>, Or<APRegisterAlias.released, Equal<True>>>, And<Match<Vendor, Current<AccessInfo.userName>>>>>, OrderBy<Desc<APRegisterAlias.refNbr>>>))]
  [APInvoiceType.Numbering]
  [PXFieldDescription]
  protected virtual void PendingPPDVATAdjApp_AdjdRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIField(DisplayName = "Doc. Date")]
  protected virtual void PendingPPDVATAdjApp_AdjdDocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (APAdjust.adjdCuryInfoID), typeof (APAdjust.adjPPDAmt))]
  [PXUIField(DisplayName = "Cash Discount")]
  protected virtual void PendingPPDVATAdjApp_CuryAdjdPPDAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Payment Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
  [APPaymentType.RefNbr(typeof (Search2<APRegisterAlias.refNbr, InnerJoinSingleTable<APPayment, On<APPayment.docType, Equal<APRegisterAlias.docType>, And<APPayment.refNbr, Equal<APRegisterAlias.refNbr>>>, InnerJoinSingleTable<Vendor, On<APRegisterAlias.vendorID, Equal<Vendor.bAccountID>>>>, Where<APRegisterAlias.docType, Equal<Current<PendingPPDVATAdjApp.payDocType>>, And<Match<Vendor, Current<AccessInfo.userName>>>>, OrderBy<Desc<APRegisterAlias.refNbr>>>))]
  [APPaymentType.Numbering]
  [PXFieldDescription]
  protected virtual void PendingPPDVATAdjApp_AdjgRefNbr_CacheAttached(PXCache sender)
  {
  }

  public APPPDDebitAdjProcess()
  {
    this.Applications.AllowDelete = true;
    this.Applications.AllowInsert = false;
    this.Applications.SetSelected<PendingPPDVATAdjApp.selected>();
  }

  public virtual IEnumerable applications(PXAdapter adapter)
  {
    APPPDDebitAdjProcess graph = this;
    APPPDVATAdjParameters current = graph.Filter.Current;
    if (current != null && current.ApplicationDate.HasValue && current.BranchID.HasValue)
    {
      PXSelectBase<PendingPPDVATAdjApp> pxSelectBase = (PXSelectBase<PendingPPDVATAdjApp>) new PXSelect<PendingPPDVATAdjApp, Where<APAdjust.adjgDocDate, LessEqual<Current<APPPDVATAdjParameters.applicationDate>>, And<APAdjust.adjdBranchID, Equal<Current<APPPDVATAdjParameters.branchID>>>>>((PXGraph) graph);
      if (current.VendorID.HasValue)
        pxSelectBase.WhereAnd<Where<APAdjust.vendorID, Equal<Current<APPPDVATAdjParameters.vendorID>>>>();
      foreach (PXResult<PendingPPDVATAdjApp> pxResult in pxSelectBase.Select())
        yield return (object) (PendingPPDVATAdjApp) pxResult;
      graph.Filter.Cache.IsDirty = false;
    }
  }

  protected virtual void APPPDVATAdjParameters_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    APPPDVATAdjParameters filter = (APPPDVATAdjParameters) e.Row;
    if (filter == null)
      return;
    APSetup setup = this.apsetup.Current;
    this.Applications.SetProcessDelegate((PXProcessingBase<PendingPPDVATAdjApp>.ProcessListDelegate) (list => APPPDDebitAdjProcess.CreatePPDVATAdjs(sender, filter, setup, list)));
    bool valueOrDefault = filter.GenerateOnePerVendor.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<APPPDVATAdjParameters.debitAdjDate>(sender, (object) filter, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<APPPDVATAdjParameters.finPeriodID>(sender, (object) filter, valueOrDefault);
    PXUIFieldAttribute.SetRequired<APPPDVATAdjParameters.debitAdjDate>(sender, valueOrDefault);
    PXUIFieldAttribute.SetRequired<APPPDVATAdjParameters.finPeriodID>(sender, valueOrDefault);
  }

  public virtual void APPPDVATAdjParameters_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    APPPDVATAdjParameters row = (APPPDVATAdjParameters) e.Row;
    APPPDVATAdjParameters oldRow = (APPPDVATAdjParameters) e.OldRow;
    if (row == null || oldRow == null || sender.ObjectsEqual<APPPDVATAdjParameters.applicationDate, APPPDVATAdjParameters.branchID, APPPDVATAdjParameters.vendorID>((object) oldRow, (object) row))
      return;
    this.Applications.Cache.Clear();
    this.Applications.Cache.ClearQueryCacheObsolete();
  }

  protected virtual void APPPDVATAdjParameters_GenerateOnePerVendor_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    APPPDVATAdjParameters row = (APPPDVATAdjParameters) e.Row;
    if (row == null)
      return;
    bool? nullable = row.GenerateOnePerVendor;
    if (nullable.GetValueOrDefault())
      return;
    nullable = (bool?) e.OldValue;
    if (!nullable.GetValueOrDefault())
      return;
    row.DebitAdjDate = new System.DateTime?();
    row.FinPeriodID = (string) null;
    sender.SetValuePending<APPPDVATAdjParameters.debitAdjDate>((object) row, (object) null);
    sender.SetValuePending<APPPDVATAdjParameters.finPeriodID>((object) row, (object) null);
  }

  public static void CreatePPDVATAdjs(
    PXCache cache,
    APPPDVATAdjParameters filter,
    APSetup setup,
    List<PendingPPDVATAdjApp> docs)
  {
    APPPDDebitAdjProcess.CreatePPDDebitAdjs(cache, filter, setup, docs);
    APPPDDebitAdjProcess.CreatePPDCreditAdjs(cache, filter, setup, docs);
  }

  public static void CreatePPDDebitAdjs(
    PXCache cache,
    APPPDVATAdjParameters filter,
    APSetup setup,
    List<PendingPPDVATAdjApp> docs)
  {
    int num = 0;
    bool flag = false;
    APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
    instance.APSetup.Current = setup;
    IEnumerable<PendingPPDVATAdjApp> pendingPpdvatAdjApps = docs.Where<PendingPPDVATAdjApp>((Func<PendingPPDVATAdjApp, bool>) (doc => doc.InvDocType != "ADR"));
    if (filter.GenerateOnePerVendor.GetValueOrDefault())
    {
      if (!filter.DebitAdjDate.HasValue)
        throw new PXSetPropertyException("'{0}' may not be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<APPPDVATAdjParameters.debitAdjDate>(cache)
        });
      if (filter.FinPeriodID == null)
        throw new PXSetPropertyException("'{0}' may not be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<APPPDVATAdjParameters.finPeriodID>(cache)
        });
      Dictionary<PPDApplicationKey, List<PendingPPDVATAdjApp>> dictionary = new Dictionary<PPDApplicationKey, List<PendingPPDVATAdjApp>>();
      foreach (PendingPPDVATAdjApp pendingPpdvatAdjApp in pendingPpdvatAdjApps)
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) instance, (object) pendingPpdvatAdjApp.InvCuryInfoID);
        PPDApplicationKey key = new PPDApplicationKey();
        pendingPpdvatAdjApp.Index = num++;
        key.BranchID = pendingPpdvatAdjApp.AdjdBranchID;
        key.BAccountID = pendingPpdvatAdjApp.VendorID;
        key.LocationID = pendingPpdvatAdjApp.InvVendorLocationID;
        key.CuryID = currencyInfo.CuryID;
        key.CuryRate = currencyInfo.CuryRate;
        key.AccountID = pendingPpdvatAdjApp.AdjdAPAcct;
        key.SubID = pendingPpdvatAdjApp.AdjdAPSub;
        key.TaxZoneID = pendingPpdvatAdjApp.InvTaxZoneID;
        List<PendingPPDVATAdjApp> pendingPpdvatAdjAppList;
        if (!dictionary.TryGetValue(key, out pendingPpdvatAdjAppList))
          dictionary[key] = pendingPpdvatAdjAppList = new List<PendingPPDVATAdjApp>();
        pendingPpdvatAdjAppList.Add(pendingPpdvatAdjApp);
      }
      foreach (List<PendingPPDVATAdjApp> list in dictionary.Values)
      {
        if (APPPDDebitAdjProcess.CreateAndReleasePPDDebitAdj(instance, filter, list, true) == null)
          flag = true;
      }
    }
    else
    {
      foreach (PendingPPDVATAdjApp pendingPpdvatAdjApp in pendingPpdvatAdjApps)
      {
        List<PendingPPDVATAdjApp> list = new List<PendingPPDVATAdjApp>(1);
        pendingPpdvatAdjApp.Index = num++;
        list.Add(pendingPpdvatAdjApp);
        if (APPPDDebitAdjProcess.CreateAndReleasePPDDebitAdj(instance, filter, list, true) == null)
          flag = true;
      }
    }
    if (flag)
      throw new PXException("One or more documents could not be released.");
  }

  public static APInvoice CreateAndReleasePPDDebitAdj(
    APInvoiceEntry ie,
    APPPDVATAdjParameters filter,
    List<PendingPPDVATAdjApp> list,
    bool autoReleaseDebitAdjustments)
  {
    APInvoice releasePpdDebitAdj;
    try
    {
      ie.Clear(PXClearOption.ClearAll);
      PXUIFieldAttribute.SetError(ie.Document.Cache, (object) null, (string) null, (string) null);
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        try
        {
          ie.IsPPDCreateContext = true;
          releasePpdDebitAdj = ie.CreatePPDDebitAdj(filter, list);
        }
        finally
        {
          ie.IsPPDCreateContext = false;
        }
        if (releasePpdDebitAdj != null)
        {
          if (autoReleaseDebitAdjustments)
          {
            using (new PXTimeStampScope((byte[]) null))
            {
              APDocumentRelease.ReleaseDoc(new List<APRegister>()
              {
                (APRegister) releasePpdDebitAdj
              }, false);
              APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
              APPayment apPayment = (APPayment) PXSelectBase<APPayment, PXSelect<APPayment, Where<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>.Config>.Select((PXGraph) instance, (object) releasePpdDebitAdj.DocType, (object) releasePpdDebitAdj.RefNbr).First<PXResult<APPayment>>();
              instance.Document.Current = apPayment;
              instance.SelectTimeStamp();
              instance.GetExtension<APPaymentEntry.MultiCurrency>().currencyinfo.Insert(ie.GetExtension<APInvoiceEntry.MultiCurrency>().GetCurrencyInfo(releasePpdDebitAdj.CuryInfoID));
              APPPDDebitAdjProcess.CreatePPDApplications(instance, list, apPayment);
              if (apPayment.AdjFinPeriodID == null)
                throw new FinancialPeriodNotDefinedForDateException(apPayment.AdjDate);
              instance.Persist();
              PXCache<APRegister>.RestoreCopy((APRegister) releasePpdDebitAdj, (APRegister) apPayment);
              APDocumentRelease.ReleaseDoc(new List<APRegister>()
              {
                (APRegister) releasePpdDebitAdj
              }, false);
            }
          }
          foreach (PendingPPDVATAdjApp pendingPpdvatAdjApp in list)
            PXProcessing<PendingPPDVATAdjApp>.SetInfo(pendingPpdvatAdjApp.Index, "The record has been processed successfully.");
        }
        transactionScope.Complete();
      }
    }
    catch (Exception ex)
    {
      foreach (PendingPPDVATAdjApp pendingPpdvatAdjApp in list)
        PXProcessing<PendingPPDVATAdjApp>.SetError(pendingPpdvatAdjApp.Index, ex);
      releasePpdDebitAdj = (APInvoice) null;
    }
    return releasePpdDebitAdj;
  }

  public static void CreatePPDCreditAdjs(
    PXCache cache,
    APPPDVATAdjParameters filter,
    APSetup setup,
    List<PendingPPDVATAdjApp> docs)
  {
    int num = 0;
    bool flag = false;
    APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
    instance.APSetup.Current = setup;
    IEnumerable<PendingPPDVATAdjApp> pendingPpdvatAdjApps = docs.Where<PendingPPDVATAdjApp>((Func<PendingPPDVATAdjApp, bool>) (doc => doc.InvDocType == "ADR"));
    if (filter.GenerateOnePerVendor.GetValueOrDefault())
    {
      if (!filter.DebitAdjDate.HasValue)
        throw new PXSetPropertyException("'{0}' may not be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<APPPDVATAdjParameters.debitAdjDate>(cache)
        });
      if (filter.FinPeriodID == null)
        throw new PXSetPropertyException("'{0}' may not be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<APPPDVATAdjParameters.finPeriodID>(cache)
        });
      Dictionary<PPDApplicationKey, List<PendingPPDVATAdjApp>> dictionary = new Dictionary<PPDApplicationKey, List<PendingPPDVATAdjApp>>();
      foreach (PendingPPDVATAdjApp pendingPpdvatAdjApp in pendingPpdvatAdjApps)
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) instance, (object) pendingPpdvatAdjApp.InvCuryInfoID);
        PPDApplicationKey key = new PPDApplicationKey();
        pendingPpdvatAdjApp.Index = num++;
        key.BranchID = pendingPpdvatAdjApp.AdjdBranchID;
        key.BAccountID = pendingPpdvatAdjApp.VendorID;
        key.LocationID = pendingPpdvatAdjApp.InvVendorLocationID;
        key.CuryID = currencyInfo.CuryID;
        key.CuryRate = currencyInfo.CuryRate;
        key.AccountID = pendingPpdvatAdjApp.AdjdAPAcct;
        key.SubID = pendingPpdvatAdjApp.AdjdAPSub;
        key.TaxZoneID = pendingPpdvatAdjApp.InvTaxZoneID;
        List<PendingPPDVATAdjApp> pendingPpdvatAdjAppList;
        if (!dictionary.TryGetValue(key, out pendingPpdvatAdjAppList))
          dictionary[key] = pendingPpdvatAdjAppList = new List<PendingPPDVATAdjApp>();
        pendingPpdvatAdjAppList.Add(pendingPpdvatAdjApp);
      }
      foreach (List<PendingPPDVATAdjApp> list in dictionary.Values)
      {
        if (APPPDDebitAdjProcess.CreateAndReleasePPDCreditAdj(instance, filter, list, true) == null)
          flag = true;
      }
    }
    else
    {
      foreach (PendingPPDVATAdjApp pendingPpdvatAdjApp in pendingPpdvatAdjApps)
      {
        List<PendingPPDVATAdjApp> list = new List<PendingPPDVATAdjApp>(1);
        pendingPpdvatAdjApp.Index = num++;
        list.Add(pendingPpdvatAdjApp);
        if (APPPDDebitAdjProcess.CreateAndReleasePPDCreditAdj(instance, filter, list, true) == null)
          flag = true;
      }
    }
    if (flag)
      throw new PXException("One or more documents could not be released.");
  }

  public static APInvoice CreateAndReleasePPDCreditAdj(
    APInvoiceEntry ie,
    APPPDVATAdjParameters filter,
    List<PendingPPDVATAdjApp> list,
    bool autoReleaseCreditAdjustments)
  {
    APInvoice releasePpdCreditAdj;
    try
    {
      ie.Clear(PXClearOption.ClearAll);
      PXUIFieldAttribute.SetError(ie.Document.Cache, (object) null, (string) null, (string) null);
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        try
        {
          ie.IsPPDCreateContext = true;
          releasePpdCreditAdj = ie.CreatePPDCreditAdj(filter, list);
        }
        finally
        {
          ie.IsPPDCreateContext = false;
        }
        if (releasePpdCreditAdj != null)
        {
          if (autoReleaseCreditAdjustments)
          {
            using (new PXTimeStampScope((byte[]) null))
            {
              APDocumentRelease.ReleaseDoc(new List<APRegister>()
              {
                (APRegister) releasePpdCreditAdj
              }, false);
              APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
              foreach (PendingPPDVATAdjApp pendingPpdvatAdjApp in list)
                APPPDDebitAdjProcess.UpdateAPAdjustPPDVatAdjRef((PXGraph) instance, releasePpdCreditAdj.DocType, releasePpdCreditAdj.RefNbr, pendingPpdvatAdjApp.AdjdDocType, pendingPpdvatAdjApp.AdjdRefNbr, pendingPpdvatAdjApp.AdjgDocType, pendingPpdvatAdjApp.AdjgRefNbr);
            }
          }
          foreach (PendingPPDVATAdjApp pendingPpdvatAdjApp in list)
            PXProcessing<PendingPPDVATAdjApp>.SetInfo(pendingPpdvatAdjApp.Index, "The record has been processed successfully.");
        }
        transactionScope.Complete();
      }
    }
    catch (Exception ex)
    {
      foreach (PendingPPDVATAdjApp pendingPpdvatAdjApp in list)
        PXProcessing<PendingPPDVATAdjApp>.SetError(pendingPpdvatAdjApp.Index, ex);
      releasePpdCreditAdj = (APInvoice) null;
    }
    return releasePpdCreditAdj;
  }

  protected static void CreatePPDApplications(
    APPaymentEntry paymentEntry,
    List<PendingPPDVATAdjApp> list,
    APPayment debitAdj)
  {
    foreach (PendingPPDVATAdjApp pendingPpdvatAdjApp in list)
    {
      APAdjust apAdjust = paymentEntry.Adjustments_Raw.Insert(new APAdjust()
      {
        AdjdDocType = pendingPpdvatAdjApp.AdjdDocType,
        AdjdRefNbr = pendingPpdvatAdjApp.AdjdRefNbr
      });
      apAdjust.CuryAdjgAmt = pendingPpdvatAdjApp.InvCuryDocBal;
      paymentEntry.Adjustments_Raw.Update(apAdjust);
      string refNbr = debitAdj.RefNbr;
      string docType = debitAdj.DocType;
      APPPDDebitAdjProcess.UpdateAPAdjustPPDVatAdjRef((PXGraph) paymentEntry, docType, refNbr, pendingPpdvatAdjApp.AdjdDocType, pendingPpdvatAdjApp.AdjdRefNbr, pendingPpdvatAdjApp.AdjgDocType, pendingPpdvatAdjApp.AdjgRefNbr);
    }
  }

  private static void UpdateAPAdjustPPDVatAdjRef(
    PXGraph graph,
    string docType,
    string refNbr,
    string adjdDocType,
    string adjdRefNbr,
    string adjgDocType,
    string adjgRefNbr)
  {
    PXUpdate<Set<APAdjust.pPDVATAdjRefNbr, Required<APAdjust.pPDVATAdjRefNbr>, Set<APAdjust.pPDVATAdjDocType, Required<APAdjust.pPDVATAdjDocType>>>, APAdjust, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>, And<APAdjust.released, Equal<True>, And<APAdjust.voided, NotEqual<True>, And<APAdjust.pendingPPD, Equal<True>>>>>>>>>.Update(graph, (object) refNbr, (object) docType, (object) adjdDocType, (object) adjdRefNbr, (object) adjgDocType, (object) adjgRefNbr);
  }

  public static bool CalculateDiscountedTaxes(
    PXCache cache,
    APTaxTran aptaxTran,
    Decimal cashDiscPercent)
  {
    bool? nullable1 = new bool?();
    object obj = (object) null;
    IBqlCreator instance = (IBqlCreator) Activator.CreateInstance(typeof (WhereAPPPDTaxable<Required<APTaxTran.taxID>>));
    PXCache cache1 = cache;
    APTaxTran apTaxTran1 = aptaxTran;
    List<object> pars = new List<object>();
    pars.Add((object) aptaxTran.TaxID);
    ref bool? local1 = ref nullable1;
    ref object local2 = ref obj;
    instance.Verify(cache1, (object) apTaxTran1, pars, ref local1, ref local2);
    IPXCurrencyHelper implementation = cache.Graph.FindImplementation<IPXCurrencyHelper>();
    APTaxTran apTaxTran2 = aptaxTran;
    Decimal? nullable2;
    Decimal num;
    Decimal? nullable3;
    Decimal? nullable4;
    if (!(cashDiscPercent == 0M))
    {
      IPXCurrencyHelper pXCurrencyHelper = implementation;
      nullable2 = aptaxTran.CuryTaxableAmt;
      num = 1M - cashDiscPercent;
      nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num) : new Decimal?();
      Decimal val = nullable3.Value;
      nullable4 = new Decimal?(pXCurrencyHelper.RoundCury(val));
    }
    else
      nullable4 = aptaxTran.CuryTaxableAmt;
    apTaxTran2.CuryDiscountedTaxableAmt = nullable4;
    APTaxTran apTaxTran3 = aptaxTran;
    Decimal? nullable5;
    if (!(cashDiscPercent == 0M))
    {
      IPXCurrencyHelper pXCurrencyHelper = implementation;
      Decimal? nullable6 = aptaxTran.TaxRate;
      num = 100M;
      nullable2 = nullable6.HasValue ? new Decimal?(nullable6.GetValueOrDefault() / num) : new Decimal?();
      nullable3 = aptaxTran.CuryDiscountedTaxableAmt;
      Decimal? nullable7;
      if (!(nullable2.HasValue & nullable3.HasValue))
      {
        nullable6 = new Decimal?();
        nullable7 = nullable6;
      }
      else
        nullable7 = new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault());
      nullable6 = nullable7;
      Decimal val = nullable6.Value;
      nullable5 = new Decimal?(pXCurrencyHelper.RoundCury(val));
    }
    else
      nullable5 = aptaxTran.CuryTaxAmt;
    apTaxTran3.CuryDiscountedPrice = nullable5;
    return nullable1.GetValueOrDefault();
  }
}
