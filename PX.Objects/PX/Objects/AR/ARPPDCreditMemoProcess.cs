// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPPDCreditMemoProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.AR.MigrationMode;
using PX.Objects.AR.Standalone;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Abstractions;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARPPDCreditMemoProcess : PXGraph<ARPPDCreditMemoProcess>
{
  public PXCancel<ARPPDTaxAdjustmentParameters> Cancel;
  public PXFilter<ARPPDTaxAdjustmentParameters> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<PendingPPDARTaxAdjApp, ARPPDTaxAdjustmentParameters> Applications;
  public ARSetupNoMigrationMode arsetup;

  public virtual bool IsDirty => false;

  [PXMergeAttributes]
  [ARInvoiceType.TaxAdjdList]
  protected virtual void PendingPPDARTaxAdjApp_AdjdDocType_CacheAttached(PXCache sender)
  {
  }

  [Customer]
  protected virtual void PendingPPDARTaxAdjApp_AdjdCustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [ARInvoiceType.RefNbr(typeof (Search2<ARRegisterAlias.refNbr, InnerJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARRegisterAlias.docType>, And<ARInvoice.refNbr, Equal<ARRegisterAlias.refNbr>>>, InnerJoinSingleTable<Customer, On<ARRegisterAlias.customerID, Equal<Customer.bAccountID>>>>, Where<ARRegisterAlias.docType, Equal<Optional<PendingPPDARTaxAdjApp.invDocType>>, And2<Where<ARRegisterAlias.origModule, Equal<BatchModule.moduleAR>, Or<ARRegisterAlias.released, Equal<True>>>, And<Match<Customer, Current<AccessInfo.userName>>>>>, OrderBy<Desc<ARRegisterAlias.refNbr>>>))]
  [ARInvoiceType.Numbering]
  [ARInvoiceNbr]
  [PXFieldDescription]
  protected virtual void PendingPPDARTaxAdjApp_AdjdRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Doc. Date")]
  protected virtual void PendingPPDARTaxAdjApp_AdjdDocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (ARAdjust.adjdCuryInfoID), typeof (ARAdjust.adjPPDAmt))]
  [PXUIField(DisplayName = "Cash Discount")]
  protected virtual void PendingPPDARTaxAdjApp_CuryAdjdPPDAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [ARPaymentType.RefNbr(typeof (Search2<ARRegisterAlias.refNbr, InnerJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegisterAlias.docType>, And<ARPayment.refNbr, Equal<ARRegisterAlias.refNbr>>>, InnerJoinSingleTable<Customer, On<ARRegisterAlias.customerID, Equal<Customer.bAccountID>>>>, Where<ARRegisterAlias.docType, Equal<Current<PendingPPDARTaxAdjApp.payDocType>>, And<Match<Customer, Current<AccessInfo.userName>>>>, OrderBy<Desc<ARRegisterAlias.refNbr>>>))]
  [ARPaymentType.Numbering]
  [PXFieldDescription]
  protected virtual void PendingPPDARTaxAdjApp_AdjgRefNbr_CacheAttached(PXCache sender)
  {
  }

  public ARPPDCreditMemoProcess()
  {
    ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    ((PXSelectBase) this.Applications).AllowDelete = true;
    ((PXSelectBase) this.Applications).AllowInsert = false;
    ((PXProcessingBase<PendingPPDARTaxAdjApp>) this.Applications).SetSelected<PendingPPDARTaxAdjApp.selected>();
  }

  public virtual IEnumerable applications(PXAdapter adapter)
  {
    ARPPDCreditMemoProcess creditMemoProcess = this;
    ARPPDTaxAdjustmentParameters current = ((PXSelectBase<ARPPDTaxAdjustmentParameters>) creditMemoProcess.Filter).Current;
    if (current != null && current.ApplicationDate.HasValue && current.BranchID.HasValue)
    {
      PXSelectBase<PendingPPDARTaxAdjApp> pxSelectBase = (PXSelectBase<PendingPPDARTaxAdjApp>) new PXSelect<PendingPPDARTaxAdjApp, Where<ARAdjust.adjgDocDate, LessEqual<Current<ARPPDTaxAdjustmentParameters.applicationDate>>, And<ARAdjust.adjdBranchID, Equal<Current<ARPPDTaxAdjustmentParameters.branchID>>>>>((PXGraph) creditMemoProcess);
      if (current.CustomerID.HasValue)
        pxSelectBase.WhereAnd<Where<ARAdjust.customerID, Equal<Current<ARPPDTaxAdjustmentParameters.customerID>>>>();
      foreach (PXResult<PendingPPDARTaxAdjApp> pxResult in pxSelectBase.Select(Array.Empty<object>()))
        yield return (object) PXResult<PendingPPDARTaxAdjApp>.op_Implicit(pxResult);
      ((PXSelectBase) creditMemoProcess.Filter).Cache.IsDirty = false;
    }
  }

  protected virtual void ARPPDTaxAdjustmentParameters_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARPPDCreditMemoProcess.\u003C\u003Ec__DisplayClass14_0 cDisplayClass140 = new ARPPDCreditMemoProcess.\u003C\u003Ec__DisplayClass14_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.sender = sender;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.filter = (ARPPDTaxAdjustmentParameters) e.Row;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass140.filter == null)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.setup = ((PXSelectBase<ARSetup>) this.arsetup).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<PendingPPDARTaxAdjApp>) this.Applications).SetProcessDelegate(new PXProcessingBase<PendingPPDARTaxAdjApp>.ProcessListDelegate((object) cDisplayClass140, __methodptr(\u003CARPPDTaxAdjustmentParameters_RowSelected\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    bool valueOrDefault = cDisplayClass140.filter.GenerateOnePerCustomer.GetValueOrDefault();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetEnabled<ARPPDTaxAdjustmentParameters.taxAdjustmentDate>(cDisplayClass140.sender, (object) cDisplayClass140.filter, valueOrDefault);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetEnabled<ARPPDTaxAdjustmentParameters.finPeriodID>(cDisplayClass140.sender, (object) cDisplayClass140.filter, valueOrDefault);
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetRequired<ARPPDTaxAdjustmentParameters.taxAdjustmentDate>(cDisplayClass140.sender, valueOrDefault);
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetRequired<ARPPDTaxAdjustmentParameters.finPeriodID>(cDisplayClass140.sender, valueOrDefault);
  }

  public virtual void ARPPDTaxAdjustmentParameters_RowUpdated(
    PXCache sender,
    PXRowUpdatedEventArgs e)
  {
    ARPPDTaxAdjustmentParameters row = (ARPPDTaxAdjustmentParameters) e.Row;
    ARPPDTaxAdjustmentParameters oldRow = (ARPPDTaxAdjustmentParameters) e.OldRow;
    if (row == null || oldRow == null || sender.ObjectsEqual<ARPPDTaxAdjustmentParameters.applicationDate, ARPPDTaxAdjustmentParameters.branchID, ARPPDTaxAdjustmentParameters.customerID>((object) oldRow, (object) row))
      return;
    ((PXSelectBase) this.Applications).Cache.Clear();
    ((PXSelectBase) this.Applications).Cache.ClearQueryCacheObsolete();
  }

  protected virtual void ARPPDTaxAdjustmentParameters_GenerateOnePerCustomer_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARPPDTaxAdjustmentParameters row = (ARPPDTaxAdjustmentParameters) e.Row;
    if (row == null)
      return;
    bool? nullable = row.GenerateOnePerCustomer;
    if (nullable.GetValueOrDefault())
      return;
    nullable = (bool?) e.OldValue;
    if (!nullable.GetValueOrDefault())
      return;
    row.TaxAdjustmentDate = new DateTime?();
    row.FinPeriodID = (string) null;
    sender.SetValuePending<ARPPDTaxAdjustmentParameters.taxAdjustmentDate>((object) row, (object) null);
    sender.SetValuePending<ARPPDTaxAdjustmentParameters.finPeriodID>((object) row, (object) null);
  }

  public static void CreatePPDTaxAdjustments(
    PXCache cache,
    ARPPDTaxAdjustmentParameters filter,
    ARSetup setup,
    List<PendingPPDARTaxAdjApp> docs)
  {
    ARPPDCreditMemoProcess.CreatePPDCreditMemos(cache, filter, setup, docs);
    ARPPDCreditMemoProcess.CreatePPDDebitMemos(cache, filter, setup, docs);
  }

  public static void CreatePPDCreditMemos(
    PXCache cache,
    ARPPDTaxAdjustmentParameters filter,
    ARSetup setup,
    List<PendingPPDARTaxAdjApp> docs)
  {
    docs = docs.Where<PendingPPDARTaxAdjApp>((Func<PendingPPDARTaxAdjApp, bool>) (doc => doc.InvDocType != "CRM")).ToList<PendingPPDARTaxAdjApp>();
    ARPPDCreditMemoProcess.ProcessAdjustmentDocuments(cache, filter, setup, docs, "CRM");
  }

  public static void CreatePPDDebitMemos(
    PXCache cache,
    ARPPDTaxAdjustmentParameters filter,
    ARSetup setup,
    List<PendingPPDARTaxAdjApp> docs)
  {
    docs = docs.Where<PendingPPDARTaxAdjApp>((Func<PendingPPDARTaxAdjApp, bool>) (doc => doc.InvDocType == "CRM")).ToList<PendingPPDARTaxAdjApp>();
    ARPPDCreditMemoProcess.ProcessAdjustmentDocuments(cache, filter, setup, docs, "DRM");
  }

  private static void ProcessAdjustmentDocuments(
    PXCache cache,
    ARPPDTaxAdjustmentParameters filter,
    ARSetup setup,
    List<PendingPPDARTaxAdjApp> docs,
    string adjustingDocType)
  {
    int num = 0;
    bool flag = false;
    List<ARRegister> list1 = new List<ARRegister>();
    ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
    ((PXSelectBase<ARSetup>) instance.ARSetup).Current = setup;
    if (filter.GenerateOnePerCustomer.GetValueOrDefault())
    {
      if (!filter.TaxAdjustmentDate.HasValue)
        throw new PXSetPropertyException("'{0}' may not be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<ARPPDTaxAdjustmentParameters.taxAdjustmentDate>(cache)
        });
      if (filter.FinPeriodID == null)
        throw new PXSetPropertyException("'{0}' may not be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<ARPPDTaxAdjustmentParameters.finPeriodID>(cache)
        });
      Dictionary<PPDApplicationKey, List<PendingPPDARTaxAdjApp>> dictionary = new Dictionary<PPDApplicationKey, List<PendingPPDARTaxAdjApp>>();
      foreach (PendingPPDARTaxAdjApp doc in docs)
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) instance).FindImplementation<IPXCurrencyHelper>().GetCurrencyInfo(doc.InvCuryInfoID);
        PPDApplicationKey key = new PPDApplicationKey();
        doc.Index = new int?(num++);
        key.BranchID = doc.AdjdBranchID;
        key.BAccountID = doc.AdjdCustomerID;
        key.LocationID = doc.InvCustomerLocationID;
        key.CuryID = currencyInfo.CuryID;
        key.CuryRate = currencyInfo.CuryRate;
        key.AccountID = doc.AdjdARAcct;
        key.SubID = doc.AdjdARSub;
        key.TaxZoneID = doc.InvTaxZoneID;
        List<PendingPPDARTaxAdjApp> pendingPpdarTaxAdjAppList;
        if (!dictionary.TryGetValue(key, out pendingPpdarTaxAdjAppList))
          dictionary[key] = pendingPpdarTaxAdjAppList = new List<PendingPPDARTaxAdjApp>();
        pendingPpdarTaxAdjAppList.Add(doc);
      }
      foreach (List<PendingPPDARTaxAdjApp> list2 in dictionary.Values)
      {
        ARInvoice adjustmentDocument = ARPPDCreditMemoProcess.CreatePPDTaxAdjustmentDocument(instance, filter, list2, adjustingDocType);
        if (adjustmentDocument != null)
          list1.Add((ARRegister) adjustmentDocument);
        else
          flag = true;
      }
    }
    else
    {
      foreach (PendingPPDARTaxAdjApp doc in docs)
      {
        List<PendingPPDARTaxAdjApp> list3 = new List<PendingPPDARTaxAdjApp>(1);
        doc.Index = new int?(num++);
        list3.Add(doc);
        ARInvoice adjustmentDocument = ARPPDCreditMemoProcess.CreatePPDTaxAdjustmentDocument(instance, filter, list3, adjustingDocType);
        if (adjustmentDocument != null)
          list1.Add((ARRegister) adjustmentDocument);
        else
          flag = true;
      }
    }
    if (setup.AutoReleasePPDCreditMemo.GetValueOrDefault() && list1.Count > 0)
    {
      using (new PXTimeStampScope((byte[]) null))
        ARDocumentRelease.ReleaseDoc(list1, true);
    }
    if (flag)
      throw new PXException("One or more documents could not be released.");
  }

  private static ARInvoice CreatePPDTaxAdjustmentDocument(
    ARInvoiceEntry ie,
    ARPPDTaxAdjustmentParameters filter,
    List<PendingPPDARTaxAdjApp> list,
    string docType)
  {
    int index = 0;
    ARInvoice adjustmentDocument;
    try
    {
      ((PXGraph) ie).Clear((PXClearOption) 3);
      PXUIFieldAttribute.SetError(((PXSelectBase) ie.Document).Cache, (object) null, (string) null, (string) null);
      adjustmentDocument = !(docType == "CRM") ? ie.CreatePPDDebitMemo(filter, list, ref index) : ie.CreatePPDCreditMemo(filter, list, ref index);
      PXProcessing<PendingPPDARTaxAdjApp>.SetInfo(index, "The record has been processed successfully.");
    }
    catch (Exception ex)
    {
      PXProcessing<PendingPPDARTaxAdjApp>.SetError(index, ex);
      adjustmentDocument = (ARInvoice) null;
    }
    return adjustmentDocument;
  }

  public static bool CalculateDiscountedTaxes(
    PXCache cache,
    ARTaxTran artax,
    Decimal cashDiscPercent)
  {
    bool? nullable1 = new bool?();
    object obj = (object) null;
    IBqlCreator instance = (IBqlCreator) Activator.CreateInstance(typeof (WhereTaxable<Required<ARTaxTran.taxID>>));
    PXCache pxCache = cache;
    ARTaxTran arTaxTran1 = artax;
    List<object> objectList = new List<object>();
    objectList.Add((object) artax.TaxID);
    ref bool? local1 = ref nullable1;
    ref object local2 = ref obj;
    ((IBqlVerifier) instance).Verify(pxCache, (object) arTaxTran1, objectList, ref local1, ref local2);
    if (cashDiscPercent == 0M)
    {
      artax.CuryDiscountedTaxableAmt = artax.CuryTaxableAmt;
      artax.CuryDiscountedPrice = artax.CuryTaxAmt;
    }
    else
    {
      IPXCurrencyHelper implementation = cache.Graph.FindImplementation<IPXCurrencyHelper>();
      ARTaxTran arTaxTran2 = artax;
      IPXCurrencyHelper pXCurrencyHelper1 = implementation;
      Decimal? curyTaxableAmt = artax.CuryTaxableAmt;
      Decimal num = 1M - cashDiscPercent;
      Decimal? nullable2 = curyTaxableAmt.HasValue ? new Decimal?(curyTaxableAmt.GetValueOrDefault() * num) : new Decimal?();
      Decimal val1 = nullable2.Value;
      Decimal? nullable3 = new Decimal?(pXCurrencyHelper1.RoundCury(val1));
      arTaxTran2.CuryDiscountedTaxableAmt = nullable3;
      ARTaxTran arTaxTran3 = artax;
      IPXCurrencyHelper pXCurrencyHelper2 = implementation;
      Decimal? nullable4 = artax.TaxRate;
      num = 100M;
      Decimal? nullable5 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() / num) : new Decimal?();
      nullable2 = artax.CuryDiscountedTaxableAmt;
      Decimal? nullable6;
      if (!(nullable5.HasValue & nullable2.HasValue))
      {
        nullable4 = new Decimal?();
        nullable6 = nullable4;
      }
      else
        nullable6 = new Decimal?(nullable5.GetValueOrDefault() * nullable2.GetValueOrDefault());
      nullable4 = nullable6;
      Decimal val2 = nullable4.Value;
      Decimal? nullable7 = new Decimal?(pXCurrencyHelper2.RoundCury(val2));
      arTaxTran3.CuryDiscountedPrice = nullable7;
    }
    return nullable1.GetValueOrDefault();
  }
}
