// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABatchEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.ACHPlugInBase;
using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AP.MigrationMode;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

#nullable enable
namespace PX.Objects.CA;

public class CABatchEntry : PXGraph<
#nullable disable
CABatchEntry>, IACHDataProvider
{
  public PXSave<CABatch> Save;
  public PXInsert<CABatch> Insert;
  public PXCancel<CABatch> Cancel;
  public PXDelete<CABatch> Delete;
  public PXFirst<CABatch> First;
  public PXPrevious<CABatch> Previous;
  public PXNext<CABatch> Next;
  public PXLast<CABatch> Last;
  public PXInitializeState<CABatch> initializeState;
  public PXAction<CABatch> putOnHold;
  public PXAction<CABatch> releaseFromHold;
  public PXAction<CABatch> cancelBatch;
  public PXAction<CABatch> release;
  public PXAction<CABatch> setBalanced;
  public PXAction<CABatch> export;
  public PXAction<CABatch> voidBatch;
  public PXAction<CABatch> ViewAPDocument;
  public PXAction<CABatch> addPayments;
  public PXSelect<CABatch, Where<CABatch.origModule, Equal<BatchModule.moduleAP>>> Document;
  public PXSelect<CABatchDetail, Where<CABatchDetail.batchNbr, Equal<Current<CABatch.batchNbr>>>> Details;
  public PXSelectJoin<CABatchDetail, LeftJoin<PX.Objects.AP.APPayment, On<CABatchDetail.origDocType, Equal<PX.Objects.AP.APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>>>, LeftJoin<APRegisterAlias, On<APRegisterAlias.origDocType, Equal<CABatchDetail.origDocType>, And<APRegisterAlias.origRefNbr, Equal<CABatchDetail.origRefNbr>>>>>, Where<CABatchDetail.batchNbr, Equal<Current<CABatch.batchNbr>>>> BatchPayments;
  public PXSelect<PX.Objects.AP.APPayment> APPayments;
  public PXSelect<APRegisterAlias> APRegisterStandalone;
  public PXSelectJoin<PX.Objects.AP.APPayment, InnerJoin<CABatchDetail, On<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<PX.Objects.AP.APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>>>>, InnerJoin<PaymentMethod, On<PaymentMethod.paymentMethodID, Equal<PX.Objects.AP.APPayment.paymentMethodID>>>>, Where<CABatchDetail.batchNbr, Equal<Current<CABatch.batchNbr>>, And<Where<PX.Objects.AP.APPayment.curyOrigDocAmt, NotEqual<decimal0>, Or<PaymentMethod.skipPaymentsWithZeroAmt, NotEqual<True>>>>>> APPaymentList;
  public PXSelectJoin<PX.Objects.AP.APPayment, InnerJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.AP.APPayment.vendorID>>, LeftJoin<CABatchDetail, On<CABatchDetail.origDocType, Equal<PX.Objects.AP.APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>, And<CABatchDetail.origModule, Equal<BatchModule.moduleAP>>>>>>, Where2<Where<PX.Objects.AP.APPayment.status, Equal<APDocStatus.pendingPrint>, And<PX.Objects.AP.APPayment.cashAccountID, Equal<Current<CABatch.cashAccountID>>, And<PX.Objects.AP.APPayment.paymentMethodID, Equal<Current<CABatch.paymentMethodID>>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>, And<PX.Objects.AP.APPayment.docType, In3<APDocType.check, APDocType.prepayment, APDocType.quickCheck>>>> AvailablePayments;
  public PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.vRemitAddressID, Equal<PX.Objects.CR.Address.addressID>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PX.Objects.AP.APPayment.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<PX.Objects.AP.APPayment.vendorLocationID>>>>> VendorRemitAddress;
  public PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.vRemitContactID, Equal<PX.Objects.CR.Contact.contactID>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PX.Objects.AP.APPayment.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<PX.Objects.AP.APPayment.vendorLocationID>>>>> VendorRemitContact;
  public PXSelectJoin<PX.Objects.AP.APInvoice, InnerJoin<PX.Objects.AP.APAdjust, On<PX.Objects.AP.APInvoice.docType, Equal<PX.Objects.AP.APAdjust.adjdDocType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<PX.Objects.AP.APAdjust.adjdRefNbr>>>, InnerJoin<PX.Objects.AP.APPayment, On<PX.Objects.AP.APAdjust.adjgDocType, Equal<PX.Objects.AP.APPayment.docType>, And<PX.Objects.AP.APAdjust.adjgRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>>>>>, Where<PX.Objects.AP.APAdjust.adjgDocType, Equal<Current<PX.Objects.AP.APPayment.docType>>, And<PX.Objects.AP.APAdjust.adjgRefNbr, Equal<Current<PX.Objects.AP.APPayment.refNbr>>>>> APPaymentApplications;
  public PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Current<CABatch.cashAccountID>>>> cashAccount;
  public PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Current<CABatch.paymentMethodID>>>> paymentMethod;
  public PXSelect<ACHPlugInParameter, Where<ACHPlugInParameter.paymentMethodID, Equal<Required<ACHPlugInParameter.paymentMethodID>>, And<ACHPlugInParameter.plugInTypeName, Equal<Required<ACHPlugInParameter.plugInTypeName>>>>> PlugInParameters;
  public PXSelect<ACHPlugInParameter, Where<ACHPlugInParameter.paymentMethodID, Equal<Required<ACHPlugInParameter.paymentMethodID>>, And<ACHPlugInParameter.plugInTypeName, Equal<Required<ACHPlugInParameter.plugInTypeName>>, And<ACHPlugInParameter.parameterID, Equal<Required<ACHPlugInParameter.parameterID>>>>>> PlugInParameterByName;
  public PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>> vendor;
  public PXFilter<AddPaymentsFilter> filter;
  public PXFilter<VoidFilter> voidFilter;
  public PXSetup<PX.Objects.CA.CASetup> CASetup;
  public APSetupNoMigrationMode APSetup;
  public PXSetup<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Current<CABatch.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<CABatch.paymentMethodID>>>>> paymentMethodAccount;
  public PXSelectJoin<CABatchDetail, InnerJoin<PX.Objects.AP.APPayment, On<PX.Objects.AP.APPayment.docType, Equal<CABatchDetail.origDocType>, And<PX.Objects.AP.APPayment.refNbr, Equal<CABatchDetail.origRefNbr>, And<PX.Objects.AP.APPayment.released, Equal<True>>>>>, Where<CABatchDetail.batchNbr, Equal<Current<CABatch.batchNbr>>>> ReleasedPayments;
  public PXSelectReadonly<CashAccountPaymentMethodDetail, Where<CashAccountPaymentMethodDetail.paymentMethodID, Equal<Current<CABatch.paymentMethodID>>, And<Current<PX.Objects.AP.APPayment.docType>, IsNotNull, And<Current<PX.Objects.AP.APPayment.refNbr>, IsNotNull, And<CashAccountPaymentMethodDetail.cashAccountID, Equal<Current<CABatch.cashAccountID>>, And<CashAccountPaymentMethodDetail.detailID, Equal<Required<CashAccountPaymentMethodDetail.detailID>>>>>>>> cashAccountSettings;
  public PXSelectReadonly2<VendorPaymentMethodDetail, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<VendorPaymentMethodDetail.bAccountID>, And<PX.Objects.CR.Location.vPaymentInfoLocationID, Equal<VendorPaymentMethodDetail.locationID>>>>, Where<VendorPaymentMethodDetail.paymentMethodID, Equal<Current<CABatch.paymentMethodID>>, And<Current<PX.Objects.AP.APPayment.docType>, IsNotNull, And<Current<PX.Objects.AP.APPayment.refNbr>, IsNotNull, And<PX.Objects.CR.Location.bAccountID, Equal<Current<PX.Objects.AP.APPayment.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<PX.Objects.AP.APPayment.vendorLocationID>>, And<VendorPaymentMethodDetail.detailID, Equal<Required<VendorPaymentMethodDetail.detailID>>>>>>>>> vendorPaymentSettings;
  public PXSelectJoin<PX.Objects.AP.APPayment, InnerJoin<PX.Objects.AP.APAdjust, On<PX.Objects.AP.APPayment.docType, Equal<PX.Objects.AP.APAdjust.adjgDocType>, And<PX.Objects.AP.APPayment.refNbr, Equal<PX.Objects.AP.APAdjust.adjgRefNbr>>>, InnerJoin<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<PX.Objects.AP.APAdjust.adjdDocType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<PX.Objects.AP.APAdjust.adjdRefNbr>>>, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.APRegister.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>>, Where<PX.Objects.AP.APPayment.docType, Equal<Optional<CABatchDetail.origDocType>>, And<PX.Objects.AP.APPayment.refNbr, Equal<Optional<CABatchDetail.origRefNbr>>>>> AddendaInfo;
  private bool _isMassDelete;
  private readonly SyFormulaProcessor _formulaProcessor = new SyFormulaProcessor();

  private bool IsPrintProcess { get; set; }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable CancelBatch(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABatchEntry.\u003C\u003Ec__DisplayClass20_0 cDisplayClass200 = new CABatchEntry.\u003C\u003Ec__DisplayClass20_0();
    ((PXAction) this.Cancel).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass200.document = ((PXSelectBase<CABatch>) this.Document).Current;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass200, __methodptr(\u003CCancelBatch\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABatchEntry.\u003C\u003Ec__DisplayClass22_0 cDisplayClass220 = new CABatchEntry.\u003C\u003Ec__DisplayClass22_0();
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass220.document = ((PXSelectBase<CABatch>) this.Document).Current;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass220, __methodptr(\u003CRelease\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable SetBalanced(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable Export(PXAdapter adapter)
  {
    ((PXAction) this.Save).Press();
    this.ExportBatch(((PXSelectBase<CABatch>) this.Document).Current);
    return adapter.Get();
  }

  private void ExportBatch(CABatch document)
  {
    PXResult<PaymentMethod, SYMapping> pxResult = (PXResult<PaymentMethod, SYMapping>) PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelectJoin<PaymentMethod, LeftJoin<SYMapping, On<SYMapping.mappingID, Equal<PaymentMethod.aPBatchExportSYMappingID>>>, Where<PaymentMethod.paymentMethodID, Equal<Optional<CABatch.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) document.PaymentMethodID
    }));
    PaymentMethod pt = PXResult<PaymentMethod, SYMapping>.op_Implicit(pxResult);
    SYMapping map = PXResult<PaymentMethod, SYMapping>.op_Implicit(pxResult);
    if (pt == null || !pt.APCreateBatchPayment.GetValueOrDefault())
      throw new PXException("The batch cannot be exported. An export scenario is not specified for the payment method.");
    short? sequenceNumber = document.DateSeqNbr;
    bool? nullable = document.Exported;
    if (!nullable.GetValueOrDefault() || !sequenceNumber.HasValue)
      sequenceNumber = new short?(CABatchEntry.GetNextDateSeqNbr((PXGraph) this, document));
    if (pt.APBatchExportMethod == "P")
    {
      nullable = pt.APCreateBatchPayment;
      if (!nullable.GetValueOrDefault() || string.IsNullOrEmpty(pt.APBatchExportPlugInTypeName))
        throw new PXException("The batch cannot be exported. An export scenario is not specified for the payment method.");
      IACHPlugIn plugin = pt.GetPlugIn();
      if (plugin == null)
        throw new PXException("The batch cannot be exported. An export scenario is not specified for the payment method.");
      plugin.RunWithVerifications((System.Action) (() => this.ExportBatchByPlugIn(document, sequenceNumber, plugin)), this, document);
    }
    if (pt.APBatchExportMethod == "E")
    {
      this.VerifySettings();
      if (pt != null)
      {
        nullable = pt.APCreateBatchPayment;
        if (nullable.GetValueOrDefault() && pt.APBatchExportSYMappingID.HasValue && map != null)
        {
          string defaultFileName = this.GenerateFileName(document);
          ((PXGraph) this).LongOperationManager.StartOperation((Action<CancellationToken>) (cancellationToken => SYExportProcess.RunScenario(map.Name, (SYMapping.RepeatingOption) 1, true, true, cancellationToken, new PXSYParameter[3]
          {
            new PXSYParameter("FileName", defaultFileName),
            new PXSYParameter("BatchNbr", document.BatchNbr),
            new PXSYParameter("FileSeqNumber", sequenceNumber.ToString())
          })));
          return;
        }
      }
      throw new PXException("The batch cannot be exported. An export scenario is not specified for the payment method.");
    }
  }

  protected virtual void ExportBatchByPlugIn(
    CABatch document,
    short? sequenceNumber,
    IACHPlugIn plugin)
  {
    CABatchEntry instance = PXGraph.CreateInstance<CABatchEntry>();
    ((PXSelectBase<CABatch>) instance.Document).Current = document;
    plugin.Export((IACHDataProvider) instance, document.BatchNbr, sequenceNumber);
    ((PXAction) instance.Save).Press();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable VoidBatch(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABatchEntry.\u003C\u003Ec__DisplayClass30_0 cDisplayClass300 = new CABatchEntry.\u003C\u003Ec__DisplayClass30_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.document = ((PXSelectBase<CABatch>) this.Document).Current;
    // ISSUE: method pointer
    if (((PXSelectBase<VoidFilter>) this.voidFilter).AskExt(new PXView.InitializePanel((object) cDisplayClass300, __methodptr(\u003CVoidBatch\u003Eb__0))) == 1)
    {
      if (((PXSelectBase<VoidFilter>) this.voidFilter).Current.VoidDateOption == "S")
      {
        DateTime? voidDate = (DateTime?) ((PXSelectBase<VoidFilter>) this.voidFilter).Current?.VoidDate;
        this.VerifyIfVoidDateIsEmpty(voidDate);
        // ISSUE: reference to a compiler-generated field
        this.VerifyIfPaymentsDateEarlierThenVoidDate(cDisplayClass300.document, voidDate);
        // ISSUE: reference to a compiler-generated field
        cDisplayClass300.document.VoidDate = voidDate;
        // ISSUE: reference to a compiler-generated field
        ((PXSelectBase<CABatch>) this.Document).Update(cDisplayClass300.document);
        ((PXAction) this.Save).Press();
      }
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass300, __methodptr(\u003CVoidBatch\u003Eb__1)));
    }
    return adapter.Get();
  }

  private void InitializeVoidPanel()
  {
    ((PXSelectBase) this.voidFilter).Cache.SetDefaultExt<VoidFilter.voidDateOption>((object) ((PXSelectBase<VoidFilter>) this.voidFilter).Current);
    ((PXSelectBase) this.voidFilter).Cache.SetDefaultExt<VoidFilter.voidDate>((object) ((PXSelectBase<VoidFilter>) this.voidFilter).Current);
  }

  private DateTime? GetLastPaymentDate()
  {
    return PXResultset<PX.Objects.AP.APRegister>.op_Implicit(PXSelectBase<PX.Objects.AP.APRegister, PXSelectReadonly2<PX.Objects.AP.APRegister, InnerJoin<CABatchDetail, On<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<PX.Objects.AP.APRegister.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APRegister.refNbr>>>>>, Where<CABatchDetail.batchNbr, Equal<Current<CABatch.batchNbr>>, And<PX.Objects.AP.APRegister.released, Equal<boolTrue>>>, OrderBy<Desc<PX.Objects.AP.APRegister.docDate>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))?.DocDate;
  }

  private void VerifyIfVoidDateIsEmpty(DateTime? voidDate)
  {
    if (!voidDate.HasValue)
    {
      ((PXSelectBase) this.voidFilter).Cache.RaiseExceptionHandling<VoidFilter.voidDate>((object) ((PXSelectBase<VoidFilter>) this.voidFilter).Current, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "Void Date"
      }));
      throw new PXSetPropertyException<AddPaymentsFilter.nextPaymentRefNumber>("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "Void Date"
      });
    }
  }

  private void VerifyIfPaymentsDateEarlierThenVoidDate(CABatch document, DateTime? voidDate)
  {
    if (!((IQueryable<PXResult<PX.Objects.AP.APRegister>>) ((PXSelectBase<PX.Objects.AP.APRegister>) new PXSelectReadonly2<PX.Objects.AP.APRegister, InnerJoin<CABatchDetail, On<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<PX.Objects.AP.APRegister.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APRegister.refNbr>>>>>, Where<CABatchDetail.batchNbr, Equal<Required<CABatch.batchNbr>>, And<PX.Objects.AP.APRegister.released, Equal<boolTrue>, And<PX.Objects.AP.APRegister.docDate, Greater<Required<VoidFilter.voidDate>>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) document.BatchNbr,
      (object) voidDate
    })).Any<PXResult<PX.Objects.AP.APRegister>>())
      return;
    DateTime? lastPaymentDate = this.GetLastPaymentDate();
    if (lastPaymentDate.HasValue)
    {
      string shortDateString = lastPaymentDate.Value.ToShortDateString();
      ((PXSelectBase) this.voidFilter).Cache.RaiseExceptionHandling<VoidFilter.voidDate>((object) ((PXSelectBase<VoidFilter>) this.voidFilter).Current, (object) voidDate, (Exception) new PXSetPropertyException("The void date cannot be earlier than the date of the last payment in the batch ({0}).", (PXErrorLevel) 4, new object[1]
      {
        (object) shortDateString
      }));
      throw new PXSetPropertyException<AddPaymentsFilter.nextPaymentRefNumber>("The void date cannot be earlier than the date of the last payment in the batch ({0}).", (PXErrorLevel) 4, new object[1]
      {
        (object) shortDateString
      });
    }
  }

  protected virtual void VerifySettings()
  {
    this.VerifyCashAccountPMDetailsSettings();
    this.VerifyVendorPaymentMethodDetailsSettings();
  }

  private void VerifyCashAccountPMDetailsSettings()
  {
    foreach (KeyValuePair<string, PaymentMethodDetail> remittanceDetail in this.SelectRemittanceDetails())
    {
      PaymentMethodDetail paymentMethodDetail1 = remittanceDetail.Value;
      if (string.IsNullOrEmpty(paymentMethodDetail1.Value) && paymentMethodDetail1.Required.GetValueOrDefault())
      {
        CashAccount cashAccount = ((PXSelectBase<CashAccount>) this.cashAccount).SelectSingle(Array.Empty<object>());
        PaymentMethodDetail paymentMethodDetail2 = PaymentMethodDetail.PK.Find((PXGraph) this, ((PXSelectBase<CABatch>) this.Document).Current?.PaymentMethodID, paymentMethodDetail1.DetailID);
        ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<CABatch.cashAccountID>((object) ((PXSelectBase<CABatch>) this.Document).Current, (object) cashAccount?.CashAccountCD, (Exception) new PXSetPropertyException("The {0} remittance detail is empty. Check the remittance settings of the {1} cash account and the {2} payment method on the Cash Accounts (CA202000) form.", (PXErrorLevel) 4, new object[3]
        {
          (object) paymentMethodDetail2.Descr,
          (object) cashAccount?.CashAccountCD,
          (object) ((PXSelectBase<CABatch>) this.Document).Current?.PaymentMethodID
        }));
        throw new PXException("Some remittance details are empty. Check the remittance settings of the {0} cash account and the {1} payment method on the Cash Accounts (CA202000) form.", new object[2]
        {
          (object) cashAccount?.CashAccountCD?.Trim(),
          (object) ((PXSelectBase<CABatch>) this.Document).Current?.PaymentMethodID
        });
      }
    }
  }

  private void VerifyVendorPaymentMethodDetailsSettings()
  {
    Dictionary<string, PXSetPropertyException> dictionary = new Dictionary<string, PXSetPropertyException>();
    bool flag = false;
    foreach (PXResult<CABatchDetail, PX.Objects.AP.APPayment> pxResult in ((PXSelectBase<CABatchDetail>) this.BatchPayments).Select(Array.Empty<object>()))
    {
      CABatchDetail caBatchDetail = PXResult<CABatchDetail, PX.Objects.AP.APPayment>.op_Implicit(pxResult);
      PX.Objects.AP.APPayment apPayment = PXResult<CABatchDetail, PX.Objects.AP.APPayment>.op_Implicit(pxResult);
      string key = string.Format($"{apPayment.PaymentMethodID}_{apPayment.VendorID}_{apPayment.VendorLocationID}");
      if (dictionary.ContainsKey(key))
      {
        if (dictionary[key] != null)
          ((PXSelectBase) this.BatchPayments).Cache.RaiseExceptionHandling<CABatchDetail.origRefNbr>((object) caBatchDetail, (object) caBatchDetail.OrigRefNbr, (Exception) dictionary[key]);
      }
      else
      {
        PX.Objects.CR.Location.PK.Find((PXGraph) this, apPayment.VendorID, apPayment.VendorLocationID);
        foreach (PXResult<PaymentMethodDetail, PX.Objects.CR.Location, VendorPaymentMethodDetail> paymentMethodDetail1 in this.SelectVendorPaymentMethodDetails(apPayment.VendorID, apPayment.VendorLocationID))
        {
          if (string.IsNullOrEmpty(PXResult<PaymentMethodDetail, PX.Objects.CR.Location, VendorPaymentMethodDetail>.op_Implicit(paymentMethodDetail1)?.DetailValue))
          {
            PaymentMethodDetail paymentMethodDetail2 = PXResult<PaymentMethodDetail, PX.Objects.CR.Location, VendorPaymentMethodDetail>.op_Implicit(paymentMethodDetail1);
            PX.Objects.AP.Vendor vendor = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).SelectSingle(new object[1]
            {
              (object) apPayment.VendorID
            });
            dictionary[key] = new PXSetPropertyException("The {0} payment instruction is empty. Check the payment instructions of the {1} vendor and the {2} payment method on the Vendors (AP303000) form.", (PXErrorLevel) 5, new object[3]
            {
              (object) paymentMethodDetail2.Descr,
              (object) vendor?.AcctCD,
              (object) ((PXSelectBase<CABatch>) this.Document).Current?.PaymentMethodID
            });
            flag = true;
            break;
          }
        }
        if (dictionary.ContainsKey(key) && dictionary[key] != null)
          ((PXSelectBase) this.BatchPayments).Cache.RaiseExceptionHandling<CABatchDetail.origRefNbr>((object) caBatchDetail, (object) caBatchDetail.OrigRefNbr, (Exception) dictionary[key]);
      }
    }
    if (flag)
      throw new PXException("Some payment instructions are empty. Check the payment instructions of the vendors that use the {0} payment method on the Vendors (AP303000) form.", new object[1]
      {
        (object) ((PXSelectBase<CABatch>) this.Document).Current?.PaymentMethodID
      });
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable viewAPDocument(PXAdapter adapter)
  {
    CABatchDetail current = ((PXSelectBase<CABatchDetail>) this.BatchPayments).Current;
    if (current == null)
      return adapter.Get();
    PX.Objects.AP.APRegister apRegister = PXResultset<PX.Objects.AP.APRegister>.op_Implicit(PXSelectBase<PX.Objects.AP.APRegister, PXSelect<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) current.OrigDocType,
      (object) current.OrigRefNbr
    }));
    if (apRegister == null)
      return adapter.Get();
    PXGraph pxGraph = new APDocGraphCreator().Create(apRegister.DocType, apRegister.RefNbr, apRegister.VendorID);
    if (pxGraph != null)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException(pxGraph, true, "ViewDocument");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPayments(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABatchEntry.\u003C\u003Ec__DisplayClass41_0 cDisplayClass410 = new CABatchEntry.\u003C\u003Ec__DisplayClass41_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass410.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass410.batch = ((PXSelectBase<CABatch>) this.Document).Current;
    // ISSUE: reference to a compiler-generated field
    this.VerifyPaymentInfo(cDisplayClass410.batch);
    // ISSUE: reference to a compiler-generated field
    CABatch batch = cDisplayClass410.batch;
    // ISSUE: reference to a compiler-generated field
    if ((batch != null ? (!batch.CashAccountID.HasValue ? 1 : 0) : 1) != 0 || cDisplayClass410.batch?.PaymentMethodID == null)
      return adapter.Get();
    if (NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.BatchPayments).Cache.Deleted))
      throw new PXException("Some details were removed from the current batch payment. Save or cancel changes before performing any modifications.");
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass410.batch != null)
    {
      // ISSUE: reference to a compiler-generated field
      bool? nullable = cDisplayClass410.batch.Released;
      if (!nullable.GetValueOrDefault())
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        CABatchEntry.\u003C\u003Ec__DisplayClass41_1 cDisplayClass411 = new CABatchEntry.\u003C\u003Ec__DisplayClass41_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass411.CS\u0024\u003C\u003E8__locals1 = cDisplayClass410;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass411.filterRow = ((PXSelectBase<AddPaymentsFilter>) this.filter).Current;
        bool flag = false;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        if (((PXSelectBase<PX.Objects.AP.APPayment>) this.AvailablePayments).AskExt(new PXView.InitializePanel((object) cDisplayClass411.CS\u0024\u003C\u003E8__locals1, __methodptr(\u003CAddPayments\u003Eb__0))) == 1)
        {
          // ISSUE: reference to a compiler-generated field
          cDisplayClass411.paymentList = new List<PX.Objects.AP.APPayment>();
          foreach (PXResult<PX.Objects.AP.APPayment> pxResult in ((PXSelectBase<PX.Objects.AP.APPayment>) this.AvailablePayments).Select(Array.Empty<object>()))
          {
            PX.Objects.AP.APPayment apPayment = PXResult<PX.Objects.AP.APPayment>.op_Implicit(pxResult);
            nullable = apPayment.Selected;
            if (nullable.GetValueOrDefault())
            {
              // ISSUE: reference to a compiler-generated field
              if (string.IsNullOrEmpty(cDisplayClass411.filterRow.NextPaymentRefNumber))
              {
                // ISSUE: reference to a compiler-generated field
                ((PXSelectBase) this.filter).Cache.RaiseExceptionHandling<AddPaymentsFilter.nextPaymentRefNumber>((object) cDisplayClass411.filterRow, (object) null, (Exception) new PXSetPropertyException("Next Check Number is required to print AP Payments with 'Payment Ref.' empty"));
                throw new PXSetPropertyException<AddPaymentsFilter.nextPaymentRefNumber>("Next Check Number is required to print AP Payments with 'Payment Ref.' empty");
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              if (!string.IsNullOrEmpty(cDisplayClass411.filterRow.NextPaymentRefNumber) && !AutoNumberAttribute.TryToGetNextNumber(cDisplayClass411.filterRow.NextPaymentRefNumber))
              {
                // ISSUE: reference to a compiler-generated field
                throw new PXSetPropertyException<AddPaymentsFilter.nextPaymentRefNumber>(AutoNumberAttribute.CheckIfNumberEndsDigit(cDisplayClass411.filterRow.NextPaymentRefNumber) ? "The {0} value cannot be incremented because the last possible value of the sequence has been reached." : "The value in the {0} box must end with a number.", new object[1]
                {
                  (object) PXUIFieldAttribute.GetDisplayName<AddPaymentsFilter.nextPaymentRefNumber>(((PXSelectBase) this.filter).Cache)
                });
              }
              // ISSUE: reference to a compiler-generated field
              if (this.IsNextNumberDuplicated(cDisplayClass411.filterRow.NextPaymentRefNumber))
              {
                // ISSUE: reference to a compiler-generated field
                throw new PXSetPropertyException<AddPaymentsFilter.nextPaymentRefNumber>("A check with the number '{0}' already exists in the system. Please enter another number.", new object[1]
                {
                  (object) cDisplayClass411.filterRow.NextPaymentRefNumber
                });
              }
              // ISSUE: reference to a compiler-generated field
              cDisplayClass411.paymentList.Add(apPayment);
              flag = true;
            }
          }
          try
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            PaymentMethodAccountHelper.VerifyAPLastReferenceNumberSettings((PXGraph) this, cDisplayClass411.CS\u0024\u003C\u003E8__locals1.batch.PaymentMethodID, cDisplayClass411.CS\u0024\u003C\u003E8__locals1.batch.CashAccountID, cDisplayClass411.paymentList.Count, cDisplayClass411.filterRow.NextPaymentRefNumber);
          }
          catch (PXException ex)
          {
            // ISSUE: reference to a compiler-generated field
            ((PXSelectBase) this.filter).Cache.RaiseExceptionHandling<AddPaymentsFilter.nextPaymentRefNumber>((object) cDisplayClass411.filterRow, (object) null, (Exception) new PXSetPropertyException(((Exception) ex).Message));
            throw;
          }
          // ISSUE: reference to a compiler-generated field
          if (cDisplayClass411.paymentList.Any<PX.Objects.AP.APPayment>())
          {
            ((PXAction) this.Save).Press();
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            cDisplayClass411.CS\u0024\u003C\u003E8__locals1.batch = ((PXSelectBase<CABatch>) this.Document).Current;
          }
          // ISSUE: method pointer
          PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass411, __methodptr(\u003CAddPayments\u003Eb__1)));
        }
        else
        {
          foreach (PX.Objects.AP.APRegister apRegister in ((PXSelectBase) this.AvailablePayments).Cache.Inserted)
            apRegister.Selected = new bool?(false);
        }
        ((PXSelectBase) this.AvailablePayments).Cache.Clear();
        if (flag)
          ((PXSelectBase) this.BatchPayments).View.RequestRefresh();
        ((PXSelectBase) this.AvailablePayments).View.RequestRefresh();
      }
    }
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) new List<CABatch>()
    {
      cDisplayClass410.batch
    };
  }

  private void InitializeAddPaymentsPanel()
  {
    ((PXSelectBase) this.filter).Cache.SetDefaultExt<AddPaymentsFilter.nextPaymentRefNumber>((object) ((PXSelectBase<AddPaymentsFilter>) this.filter).Current);
  }

  protected virtual bool IsNextNumberDuplicated(string nextNumber)
  {
    CABatch current = ((PXSelectBase<CABatch>) this.Document).Current;
    return PX.Objects.AP.PaymentRefAttribute.IsNextNumberDuplicated((PXGraph) this, current.CashAccountID, current.PaymentMethodID, nextNumber);
  }

  protected virtual void VerifyPaymentInfo(CABatch batch)
  {
    if ((batch != null ? (!batch.CashAccountID.HasValue ? 1 : 0) : 1) != 0)
      ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<CABatch.cashAccountID>((object) batch, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "Cash Account"
      }));
    if (batch != null && batch.PaymentMethodID != null)
      return;
    ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<CABatch.paymentMethodID>((object) batch, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
    {
      (object) "Payment Method"
    }));
  }

  public static void AddSelectedPayments(
    List<PX.Objects.AP.APPayment> docList,
    CABatch batch,
    string expectingCheckNumber)
  {
    CABatchEntry instance1 = PXGraph.CreateInstance<CABatchEntry>();
    APPrintChecks instance2 = PXGraph.CreateInstance<APPrintChecks>();
    APPaymentEntry instance3 = PXGraph.CreateInstance<APPaymentEntry>();
    ((PXSelectBase<CABatch>) instance1.Document).Current = PXResultset<CABatch>.op_Implicit(((PXSelectBase<CABatch>) instance1.Document).Search<CABatch.batchNbr>((object) batch.BatchNbr, Array.Empty<object>()));
    string NextCheckNbr = expectingCheckNumber;
    foreach (PX.Objects.AP.APPayment doc in docList)
      instance1.PrintPaymentAndAddToBatch(instance1, instance2, instance3, batch, doc, ref NextCheckNbr);
  }

  protected virtual void PrintPaymentAndAddToBatch(
    CABatchEntry graph,
    APPrintChecks printChecks,
    APPaymentEntry pe,
    CABatch batch,
    PX.Objects.AP.APPayment pmt,
    ref string NextCheckNbr)
  {
    PX.Objects.AP.APPayment apPayment1 = pmt;
    int? cashAccountId1 = pmt.CashAccountID;
    int? cashAccountId2 = batch.CashAccountID;
    if (!(cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue) || pmt.PaymentMethodID != batch.PaymentMethodID)
      throw new PXException("One of the Payments in selection have wrong Cash Account or PaymentMethod");
    if (string.IsNullOrEmpty(pmt.ExtRefNbr) && string.IsNullOrEmpty(NextCheckNbr))
      throw new PXException("Next Check Number is required to print AP Payments with 'Payment Ref.' empty");
    PX.Objects.AP.APPayment apPayment2 = PXResultset<PX.Objects.AP.APPayment>.op_Implicit(((PXSelectBase<PX.Objects.AP.APPayment>) pe.Document).Search<PX.Objects.AP.APPayment.refNbr>((object) apPayment1.RefNbr, new object[1]
    {
      (object) apPayment1.DocType
    }));
    if (!apPayment2.PrintCheck.GetValueOrDefault())
      throw new PXException("The check cannot be printed because the Print Check check box is not selected on the Remittance Information tab of the Checks and Payments (AP302000) form");
    if (EnumerableExtensions.IsIn<string>(apPayment2.DocType, "CHK", "QCK", "PPM") && apPayment2.Status != "G")
      throw new PXException("The check cannot be printed for this document. Checks can be printed only for documents that have the Pending Print status.");
    printChecks.AssignNumbers(pe, apPayment2, ref NextCheckNbr, true);
    if (apPayment2.Passed.GetValueOrDefault())
      ((PXGraph) pe).TimeStamp = apPayment2.tstamp;
    graph.AddPayment(apPayment2, true);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXAction) pe.Save).Press();
      apPayment2.tstamp = ((PXGraph) pe).TimeStamp;
      ((PXGraph) pe).Clear();
      ((PXAction) graph.Save).Press();
      transactionScope.Complete();
    }
  }

  public CABatchEntry()
  {
    PX.Objects.CA.CASetup current1 = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current;
    PX.Objects.AP.APSetup current2 = ((PXSelectBase<PX.Objects.AP.APSetup>) this.APSetup).Current;
    PXGraph.RowUpdatedEvents rowUpdated = ((PXGraph) this).RowUpdated;
    CABatchEntry caBatchEntry = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) caBatchEntry, __vmethodptr(caBatchEntry, ParentFieldUpdated));
    rowUpdated.AddHandler<CABatch>(pxRowUpdated);
    ((PXSelectBase) this.APPaymentApplications).Cache.AllowInsert = false;
    ((PXSelectBase) this.APPaymentApplications).Cache.AllowDelete = false;
    ((PXSelectBase) this.APPaymentApplications).Cache.AllowUpdate = false;
    ((PXSelectBase) this.BatchPayments).AllowInsert = true;
  }

  public virtual IEnumerable availablePayments()
  {
    CABatch current1 = ((PXSelectBase<CABatch>) this.Document).Current;
    AddPaymentsFilter current2 = ((PXSelectBase<AddPaymentsFilter>) this.filter).Current;
    if (current1 != null && current1.CashAccountID.HasValue && current1.PaymentMethodID != null && !current1.Released.GetValueOrDefault())
    {
      PXSelectBase<PX.Objects.AP.APPayment> apPaymentSelect = this.GetAPPaymentSelect();
      if (current2.EndDate.HasValue)
        apPaymentSelect.WhereAnd<Where<PX.Objects.AP.APPayment.docDate, LessEqual<Current<AddPaymentsFilter.endDate>>>>();
      if (current2.StartDate.HasValue)
        apPaymentSelect.WhereAnd<Where<PX.Objects.AP.APPayment.docDate, GreaterEqual<Current<AddPaymentsFilter.startDate>>>>();
      foreach (PXResult<PX.Objects.AP.APPayment, PX.Objects.AP.Vendor, CABatchDetail> pxResult in apPaymentSelect.Select(Array.Empty<object>()))
      {
        if (!this.CheckIfPaymentAdded(PXResult<PX.Objects.AP.APPayment, PX.Objects.AP.Vendor, CABatchDetail>.op_Implicit(pxResult)))
          yield return (object) pxResult;
      }
    }
  }

  protected virtual PXSelectBase<PX.Objects.AP.APPayment> GetAPPaymentSelect()
  {
    return (PXSelectBase<PX.Objects.AP.APPayment>) new PXSelectJoin<PX.Objects.AP.APPayment, InnerJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.AP.APPayment.vendorID>>, LeftJoin<CABatchDetail, On<CABatchDetail.origDocType, Equal<PX.Objects.AP.APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>, And<CABatchDetail.origModule, Equal<BatchModule.moduleAP>>>>>>, Where2<Where<PX.Objects.AP.APPayment.status, Equal<APDocStatus.pendingPrint>, And<PX.Objects.AP.APPayment.cashAccountID, Equal<Current<CABatch.cashAccountID>>, And<PX.Objects.AP.APPayment.paymentMethodID, Equal<Current<CABatch.paymentMethodID>>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>, And<PX.Objects.AP.APPayment.docType, In3<APDocType.check, APDocType.prepayment, APDocType.quickCheck>>>>((PXGraph) this);
  }

  private bool CheckIfPaymentAdded(PX.Objects.AP.APPayment payment)
  {
    CABatchDetail caBatchDetail = ((PXSelectBase<CABatchDetail>) this.BatchPayments).Locate(this.CreateDetail(payment.DocType, payment.RefNbr));
    return caBatchDetail != null && caBatchDetail.OrigRefNbr != null && ((PXSelectBase) this.BatchPayments).Cache.GetStatus((object) caBatchDetail) != 3;
  }

  private CABatchDetail CreateDetail(string docType, string refNbr)
  {
    return new CABatchDetail()
    {
      BatchNbr = ((PXSelectBase<CABatch>) this.Document).Current.BatchNbr,
      OrigModule = "AP",
      OrigDocType = docType,
      OrigRefNbr = refNbr,
      OrigLineNbr = new int?(0)
    };
  }

  protected virtual void _(PX.Data.Events.RowSelected<AddPaymentsFilter> e)
  {
    PaymentMethodAccount current = ((PXSelectBase<PaymentMethodAccount>) this.paymentMethodAccount).Current;
    PXUIFieldAttribute.SetEnabled<AddPaymentsFilter.nextPaymentRefNumber>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddPaymentsFilter>>) e).Cache, (object) e.Row, current == null || !current.APAutoNextNbr.GetValueOrDefault());
    bool flag = this.CheckSelectedPayments();
    if (string.IsNullOrEmpty(e.Row.NextPaymentRefNumber))
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddPaymentsFilter>>) e).Cache.RaiseExceptionHandling<AddPaymentsFilter.nextPaymentRefNumber>((object) e.Row, (object) e.Row.NextPaymentRefNumber, (Exception) new PXSetPropertyException("Next Check Number is required to print AP Payments with 'Payment Ref.' empty", flag ? (PXErrorLevel) 4 : (PXErrorLevel) 2));
    else if (!string.IsNullOrEmpty(e.Row.NextPaymentRefNumber) && !AutoNumberAttribute.TryToGetNextNumber(e.Row.NextPaymentRefNumber))
    {
      string str = AutoNumberAttribute.CheckIfNumberEndsDigit(e.Row.NextPaymentRefNumber) ? "The {0} value cannot be incremented because the last possible value of the sequence has been reached." : "The value in the {0} box must end with a number.";
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddPaymentsFilter>>) e).Cache.RaiseExceptionHandling<AddPaymentsFilter.nextPaymentRefNumber>((object) e.Row, (object) e.Row.NextPaymentRefNumber, (Exception) new PXSetPropertyException(str, (PXErrorLevel) (flag ? 4 : 2), new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<AddPaymentsFilter.nextPaymentRefNumber>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddPaymentsFilter>>) e).Cache)
      }));
    }
    else if (this.IsNextNumberDuplicated(e.Row.NextPaymentRefNumber))
    {
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddPaymentsFilter>>) e).Cache.RaiseExceptionHandling<AddPaymentsFilter.nextPaymentRefNumber>((object) e.Row, (object) e.Row.NextPaymentRefNumber, (Exception) new PXSetPropertyException("A check with the number '{0}' already exists in the system. Please enter another number.", new object[1]
      {
        (object) e.Row.NextPaymentRefNumber
      }));
    }
    else
    {
      int? selectedCount = ((PXSelectBase<AddPaymentsFilter>) this.filter).Current.SelectedCount;
      int num = 0;
      if (selectedCount.GetValueOrDefault() > num & selectedCount.HasValue)
      {
        string paymentMethodId = ((PXSelectBase<CABatch>) this.Document).Current.PaymentMethodID;
        int? cashAccountId = ((PXSelectBase<CABatch>) this.Document).Current.CashAccountID;
        string str;
        ref string local = ref str;
        selectedCount = e.Row.SelectedCount;
        int count = selectedCount.Value - 1;
        string paymentRefNumber = e.Row.NextPaymentRefNumber;
        if (!PaymentMethodAccountHelper.TryToVerifyAPLastReferenceNumber((PXGraph) this, paymentMethodId, cashAccountId, out local, count, paymentRefNumber))
        {
          ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddPaymentsFilter>>) e).Cache.RaiseExceptionHandling<PrintChecksFilter.nextCheckNbr>((object) e.Row, (object) ((PXSelectBase<AddPaymentsFilter>) this.filter).Current.NextPaymentRefNumber, (Exception) new PXSetPropertyException(str));
          return;
        }
      }
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddPaymentsFilter>>) e).Cache.RaiseExceptionHandling<AddPaymentsFilter.nextPaymentRefNumber>((object) e.Row, (object) e.Row.NextPaymentRefNumber, (Exception) null);
    }
  }

  private bool CheckSelectedPayments()
  {
    bool flag = false;
    foreach (PXResult<PX.Objects.AP.APPayment> pxResult in ((PXSelectBase<PX.Objects.AP.APPayment>) this.AvailablePayments).Select(Array.Empty<object>()))
    {
      if (PXResult<PX.Objects.AP.APPayment>.op_Implicit(pxResult).Selected.GetValueOrDefault())
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<AddPaymentsFilter.nextPaymentRefNumber> e)
  {
    PaymentMethodAccount current = ((PXSelectBase<PaymentMethodAccount>) this.paymentMethodAccount).Current;
    if (current == null)
      return;
    if (!current.APAutoNextNbr.GetValueOrDefault())
      return;
    try
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AddPaymentsFilter.nextPaymentRefNumber>, object, object>) e).NewValue = (object) PX.Objects.AP.PaymentRefAttribute.GetNextPaymentRef((PXGraph) this, ((PXSelectBase<PaymentMethodAccount>) this.paymentMethodAccount).Current.CashAccountID, ((PXSelectBase<PaymentMethodAccount>) this.paymentMethodAccount).Current.PaymentMethodID);
    }
    catch
    {
    }
  }

  [PXDBTimestamp]
  protected virtual void APPayment_tstamp_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.AP.APPayment> e)
  {
    AddPaymentsFilter current = ((PXSelectBase<AddPaymentsFilter>) this.filter).Current;
    if (this.filter == null)
      return;
    PX.Objects.AP.APPayment oldRow = e.OldRow;
    PX.Objects.AP.APPayment row = e.Row;
    AddPaymentsFilter addPaymentsFilter1 = current;
    int? selectedCount1 = addPaymentsFilter1.SelectedCount;
    bool? selected = oldRow.Selected;
    int num1 = selected.GetValueOrDefault() ? 1 : 0;
    addPaymentsFilter1.SelectedCount = selectedCount1.HasValue ? new int?(selectedCount1.GetValueOrDefault() - num1) : new int?();
    AddPaymentsFilter addPaymentsFilter2 = current;
    int? selectedCount2 = addPaymentsFilter2.SelectedCount;
    selected = row.Selected;
    int num2 = selected.GetValueOrDefault() ? 1 : 0;
    addPaymentsFilter2.SelectedCount = selectedCount2.HasValue ? new int?(selectedCount2.GetValueOrDefault() + num2) : new int?();
  }

  protected virtual void CABatch_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CABatch row) || this.IsPrintProcess)
      return;
    bool? nullable = row.SkipExport;
    bool valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = row.Exported;
    bool valueOrDefault2 = nullable.GetValueOrDefault();
    nullable = row.Released;
    bool valueOrDefault3 = nullable.GetValueOrDefault();
    nullable = row.Voided;
    bool valueOrDefault4 = nullable.GetValueOrDefault();
    nullable = row.Canceled;
    bool valueOrDefault5 = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CABatch.batchNbr>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<CABatch.exportFileName>(sender, (object) row, ((PXGraph) this).IsExport);
    PXUIFieldAttribute.SetEnabled<CABatch.exportTime>(sender, (object) row, ((PXGraph) this).IsExport);
    bool flag1 = ((valueOrDefault1 ? (!valueOrDefault3 ? 1 : 0) : (!valueOrDefault2 ? 1 : 0)) | (valueOrDefault5 ? 1 : 0)) != 0;
    if (flag1)
      flag1 = !((IQueryable<PXResult<CABatchDetail>>) ((PXSelectBase<CABatchDetail>) this.ReleasedPayments).Select(Array.Empty<object>())).Any<PXResult<CABatchDetail>>();
    PXCache pxCache1 = sender;
    nullable = row.Released;
    int num1;
    if (nullable.HasValue)
    {
      nullable = row.Exported;
      num1 = nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    pxCache1.AllowInsert = num1 != 0;
    sender.AllowDelete = flag1;
    CashAccount cashAccount = (CashAccount) PXSelectorAttribute.Select<CABatch.cashAccountID>(sender, (object) row);
    nullable = row.Released;
    int num2;
    if (!nullable.GetValueOrDefault())
    {
      if (cashAccount == null)
      {
        num2 = 0;
      }
      else
      {
        nullable = cashAccount.Reconcile;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<CABatch.hold>(sender, (object) row, !valueOrDefault3 && !valueOrDefault5);
    PXUIFieldAttribute.SetEnabled<CABatch.tranDesc>(sender, (object) row, !valueOrDefault3 && !valueOrDefault5);
    PXUIFieldAttribute.SetEnabled<CABatch.tranDate>(sender, (object) row, !valueOrDefault3 && !valueOrDefault5);
    PXUIFieldAttribute.SetEnabled<CABatch.batchSeqNbr>(sender, (object) row, !valueOrDefault3 && !valueOrDefault5);
    PaymentMethod paymentMethod1 = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.PaymentMethodID
    }));
    if (paymentMethod1 != null)
    {
      PXCache pxCache2 = sender;
      nullable = paymentMethod1.RequireBatchSeqNum;
      int num3 = nullable.Value ? 1 : 0;
      PXUIFieldAttribute.SetRequired<CABatch.batchSeqNbr>(pxCache2, num3 != 0);
    }
    PXUIFieldAttribute.SetEnabled<CABatch.extRefNbr>(sender, (object) row, !valueOrDefault3 && !valueOrDefault5);
    if (!valueOrDefault3 && !valueOrDefault5)
    {
      int? countOfPayments = row.CountOfPayments;
      int num4 = 0;
      bool flag2 = countOfPayments.GetValueOrDefault() > num4 & countOfPayments.HasValue;
      PXUIFieldAttribute.SetEnabled<CABatch.paymentMethodID>(sender, (object) row, !flag2);
      PXUIFieldAttribute.SetEnabled<CABatch.cashAccountID>(sender, (object) row, !flag2);
    }
    PXUIFieldAttribute.SetVisible<CABatch.dateSeqNbr>(sender, (object) row, valueOrDefault1 ? valueOrDefault3 : valueOrDefault2);
    ((PXSelectBase) this.BatchPayments).Cache.AllowDelete = !valueOrDefault4 || !valueOrDefault2;
    ((PXSelectBase) this.AvailablePayments).Cache.AllowInsert = false;
    ((PXSelectBase) this.AvailablePayments).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.AvailablePayments).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APPayment.selected>(((PXSelectBase) this.AvailablePayments).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<APRegisterAlias.docDate>(((PXSelectBase) this.APRegisterStandalone).Cache, (object) null, valueOrDefault4);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.BatchPayments).Cache, (string) null, false);
    bool flag3 = this.IsAddendaEnabled();
    PXUIFieldAttribute.SetEnabled<CABatchDetail.addendaPaymentRelatedInfo>(((PXSelectBase) this.Details).Cache, (object) null, flag3 && !valueOrDefault2 && !valueOrDefault3);
    PXUIFieldAttribute.SetVisible<CABatchDetail.addendaPaymentRelatedInfo>(((PXSelectBase) this.Details).Cache, (object) null, flag3);
    PaymentMethod paymentMethod2 = ((PXSelectBase<PaymentMethod>) this.paymentMethod).SelectSingle(Array.Empty<object>());
    if ((!(paymentMethod2?.APBatchExportMethod == "P") ? 0 : (paymentMethod2?.APBatchExportPlugInTypeName == "PX.ACHPlugIn.ACHPlugIn" ? 1 : 0)) != 0)
      PXUIFieldAttribute.SetDisplayName<CABatchDetail.addendaPaymentRelatedInfo>(((PXSelectBase) this.Details).Cache, "Payment-Related Info (Addenda)");
    ((PXSelectBase) this.AddendaInfo).AllowSelect = false;
    this.VerifyServiceClassCodeSettings(sender, row);
  }

  public virtual bool IsAddendaEnabled()
  {
    PaymentMethod pt = ((PXSelectBase<PaymentMethod>) this.paymentMethod).SelectSingle(Array.Empty<object>());
    bool flag = false;
    if (pt?.APBatchExportMethod == "P" && !string.IsNullOrEmpty(pt?.APBatchExportPlugInTypeName))
    {
      IACHPlugIn plugIn = pt.GetPlugIn();
      flag = plugIn != null && plugIn.IsAddendaRecordsEnabled((IACHDataProvider) this).GetValueOrDefault();
    }
    return flag;
  }

  protected virtual void CABatch_CashAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CABatch row = (CABatch) e.Row;
    row.Cleared = new bool?(false);
    row.ClearDate = new DateTime?();
    if (((PXSelectBase<CashAccount>) this.cashAccount).Current != null)
    {
      int? cashAccountId1 = ((PXSelectBase<CashAccount>) this.cashAccount).Current.CashAccountID;
      int? cashAccountId2 = row.CashAccountID;
      if (cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue)
        goto label_3;
    }
    ((PXSelectBase<CashAccount>) this.cashAccount).Current = (CashAccount) PXSelectorAttribute.Select<CABatch.cashAccountID>(sender, (object) row);
label_3:
    CashAccount current = ((PXSelectBase<CashAccount>) this.cashAccount).Current;
    if ((current != null ? (!current.Reconcile.GetValueOrDefault() ? 1 : 0) : 1) != 0)
    {
      row.Cleared = new bool?(true);
      row.ClearDate = row.TranDate;
    }
    sender.SetDefaultExt<CABatch.referenceID>(e.Row);
    sender.SetDefaultExt<CABatch.paymentMethodID>(e.Row);
    ((PXSelectBase) this.AvailablePayments).Cache.Clear();
    ((PXSelectBase) this.AvailablePayments).Cache.ClearQueryCache();
    ((PXSelectBase) this.AvailablePayments).View.RequestRefresh();
  }

  protected virtual void CABatch_PaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CABatch row = (CABatch) e.Row;
    sender.SetDefaultExt<CABatch.batchSeqNbr>(e.Row);
    ((PXSelectBase) this.AvailablePayments).Cache.Clear();
    ((PXSelectBase) this.AvailablePayments).Cache.ClearQueryCache();
    ((PXSelectBase) this.AvailablePayments).View.RequestRefresh();
  }

  protected virtual void CABatch_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    this._isMassDelete = true;
  }

  protected virtual void CABatch_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    this._isMassDelete = false;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CABatch> e)
  {
    if (!e.Row.Exported.GetValueOrDefault() || e.OldRow.Exported.GetValueOrDefault() || e.Row.Released.GetValueOrDefault() || e.Row.SkipExport.GetValueOrDefault())
      return;
    CABatch copy = (CABatch) ((PXSelectBase) this.Document).Cache.CreateCopy((object) e.Row);
    copy.DateSeqNbr = new short?(CABatchEntry.GetNextDateSeqNbr((PXGraph) this, copy));
    ((PXSelectBase) this.Document).Cache.Update((object) copy);
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Type")]
  [APDocType.List]
  public virtual void CABatchDetail_OrigDocType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  public virtual void _(PX.Data.Events.CacheAttached<APRegisterAlias.docDate> e)
  {
  }

  protected virtual void CABatchDetail_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    CABatchDetail row = (CABatchDetail) e.Row;
    bool flag = false;
    if (row.OrigModule == "AP")
      flag = PXResultset<PX.Objects.AP.APRegister>.op_Implicit(PXSelectBase<PX.Objects.AP.APRegister, PXSelect<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.OrigDocType,
        (object) row.OrigRefNbr
      })).Released.Value;
    if (row.OrigModule == "AR")
      flag = PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.docType, Equal<Required<ARRegister.docType>>, And<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.OrigDocType,
        (object) row.OrigRefNbr
      })).Released.Value;
    if (flag)
      throw new PXException("This document is released and can not be added to the batch");
  }

  protected virtual void CABatchDetail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.UpdateDocAmount((CABatchDetail) e.Row, false);
  }

  protected virtual void CABatchDetail_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    CABatchDetail row = (CABatchDetail) e.Row;
    if ((!((PXSelectBase<CABatch>) this.Document).Current.Exported.GetValueOrDefault() ? 0 : (!((PXSelectBase<CABatch>) this.Document).Current.Released.GetValueOrDefault() ? 1 : 0)) != 0)
      throw new PXException("The {0} payment is included in a batch with the Exported status. To remove the payment, set the batch status to Balanced.", new object[1]
      {
        (object) row.OrigRefNbr
      });
    bool isReleased = false;
    bool isVoided = false;
    if (!((PXSelectBase<CABatch>) this.Document).Current.Canceled.GetValueOrDefault())
      this.GetOrigDocState(row, ref isReleased, ref isVoided);
    if (isReleased && !isVoided)
      throw new PXException("This document is released and can not be deleted from the batch");
  }

  private void GetOrigDocState(CABatchDetail row, ref bool isReleased, ref bool isVoided)
  {
    if (row.OrigModule == "AP")
    {
      PX.Objects.AP.APRegister apRegister = PXResultset<PX.Objects.AP.APRegister>.op_Implicit(PXSelectBase<PX.Objects.AP.APRegister, PXSelect<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.OrigDocType,
        (object) row.OrigRefNbr
      }));
      isReleased = apRegister != null && apRegister.Released.GetValueOrDefault();
      isVoided = apRegister != null && apRegister.Voided.GetValueOrDefault();
    }
    if (!(row.OrigModule == "AR"))
      return;
    ARRegister arRegister = PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.docType, Equal<Required<ARRegister.docType>>, And<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.OrigDocType,
      (object) row.OrigRefNbr
    }));
    isReleased = arRegister != null && arRegister.Released.GetValueOrDefault();
    isVoided = arRegister != null && arRegister.Voided.GetValueOrDefault();
  }

  protected virtual void CABatchDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    CABatchDetail row = (CABatchDetail) e.Row;
    if (!this._isMassDelete)
    {
      this.UpdateDocAmount(row, true);
      this.ChangeCountOfPayments(-1);
    }
    ((PXSelectBase) this.AvailablePayments).View.RequestRefresh();
  }

  protected virtual void CABatchDetail_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CABatchDetail row = (CABatchDetail) e.Row;
    if (sender.GetStatus((object) row) != 3)
      return;
    PX.Objects.AP.APPayment apPayment = PXResultset<PX.Objects.AP.APPayment>.op_Implicit(PXSelectBase<PX.Objects.AP.APPayment, PXSelect<PX.Objects.AP.APPayment, Where<PX.Objects.AP.APPayment.docType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<PX.Objects.AP.APPayment.refNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.OrigDocType,
      (object) row.OrigRefNbr
    }));
    if (apPayment == null)
      return;
    PXCache<PX.Objects.AP.APPayment> pxCache = GraphHelper.Caches<PX.Objects.AP.APPayment>((PXGraph) this);
    ((SelectedEntityEvent<PX.Objects.AP.APPayment>) PXEntityEventBase<PX.Objects.AP.APPayment>.Container<PX.Objects.AP.APPayment.Events>.Select((Expression<Func<PX.Objects.AP.APPayment.Events, PXEntityEvent<PX.Objects.AP.APPayment.Events>>>) (se => se.CancelPrintCheck))).FireOn((PXGraph) this, apPayment);
    ((PXCache) pxCache).PersistUpdated((object) apPayment);
  }

  private CABatch UpdateDocAmount(CABatchDetail row, bool negative)
  {
    CABatch caBatch1 = ((PXSelectBase<CABatch>) this.Document).Current;
    if (row.OrigDocType != null && row.OrigRefNbr != null)
    {
      Decimal? nullable1 = new Decimal?();
      Decimal? nullable2 = new Decimal?();
      if (row.OrigModule == "AP")
      {
        PX.Objects.AP.APPayment apPayment = PXResultset<PX.Objects.AP.APPayment>.op_Implicit(PXSelectBase<PX.Objects.AP.APPayment, PXSelect<PX.Objects.AP.APPayment, Where<PX.Objects.AP.APPayment.docType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<PX.Objects.AP.APPayment.refNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) row.OrigDocType,
          (object) row.OrigRefNbr
        }));
        if (apPayment != null)
        {
          nullable1 = apPayment.CuryOrigDocAmt;
          nullable2 = apPayment.OrigDocAmt;
        }
      }
      else
      {
        PX.Objects.AR.ARPayment arPayment = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(PXSelectBase<PX.Objects.AR.ARPayment, PXSelect<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.docType, Equal<Required<PX.Objects.AR.ARPayment.docType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Required<PX.Objects.AR.ARPayment.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) row.OrigDocType,
          (object) row.OrigRefNbr
        }));
        if (arPayment != null)
        {
          nullable1 = arPayment.CuryOrigDocAmt;
          nullable2 = arPayment.OrigDocAmt;
        }
      }
      Decimal? nullable3;
      if (nullable1.HasValue)
      {
        CABatch caBatch2 = caBatch1;
        Decimal? curyDetailTotal = caBatch2.CuryDetailTotal;
        Decimal? nullable4;
        if (!negative)
        {
          nullable4 = nullable1;
        }
        else
        {
          Decimal? nullable5 = nullable1;
          nullable4 = nullable5.HasValue ? new Decimal?(-nullable5.GetValueOrDefault()) : new Decimal?();
        }
        nullable3 = nullable4;
        caBatch2.CuryDetailTotal = curyDetailTotal.HasValue & nullable3.HasValue ? new Decimal?(curyDetailTotal.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      }
      if (nullable2.HasValue)
      {
        CABatch caBatch3 = caBatch1;
        nullable3 = caBatch3.DetailTotal;
        Decimal? nullable6;
        if (!negative)
        {
          nullable6 = nullable2;
        }
        else
        {
          Decimal? nullable7 = nullable2;
          nullable6 = nullable7.HasValue ? new Decimal?(-nullable7.GetValueOrDefault()) : new Decimal?();
        }
        Decimal? nullable8 = nullable6;
        caBatch3.DetailTotal = nullable3.HasValue & nullable8.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
      }
      caBatch1 = ((PXSelectBase<CABatch>) this.Document).Update(caBatch1);
    }
    return caBatch1;
  }

  protected virtual void _(PX.Data.Events.RowSelected<VoidFilter> e)
  {
    bool flag = e.Row?.VoidDateOption == "S";
    PXUIFieldAttribute.SetVisible<VoidFilter.voidDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<VoidFilter>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<VoidFilter.voidDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<VoidFilter>>) e).Cache, (object) e.Row, flag);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<VoidFilter.voidDate> e)
  {
    DateTime? lastPaymentDate = this.GetLastPaymentDate();
    if (!lastPaymentDate.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<VoidFilter.voidDate>, object, object>) e).NewValue = (object) lastPaymentDate;
  }

  public virtual CABatchDetail AddPayment(PX.Objects.AP.APPayment aPayment, bool skipCheck)
  {
    if (!skipCheck)
    {
      foreach (PXResult<CABatchDetail> pxResult in ((PXSelectBase<CABatchDetail>) this.BatchPayments).Select(Array.Empty<object>()))
      {
        CABatchDetail detail = PXResult<CABatchDetail>.op_Implicit(pxResult);
        if (CABatchEntry.IsKeyEqual(aPayment, detail))
          return detail;
      }
    }
    CABatchDetail caBatchDetail1 = new CABatchDetail();
    caBatchDetail1.Copy(aPayment);
    CABatchDetail caBatchDetail2 = ((PXSelectBase<CABatchDetail>) this.BatchPayments).Insert(caBatchDetail1);
    try
    {
      caBatchDetail2.AddendaPaymentRelatedInfo = this.BuildAddendaInfo(aPayment);
    }
    catch (AddendaCalculationException ex)
    {
      ((PXSelectBase<CABatchDetail>) this.BatchPayments).Delete(caBatchDetail2);
      throw;
    }
    CABatchDetail caBatchDetail3 = ((PXSelectBase<CABatchDetail>) this.BatchPayments).Update(caBatchDetail2);
    this.ChangeCountOfPayments(1);
    return caBatchDetail3;
  }

  private void ChangeCountOfPayments(int i)
  {
    CABatch current = ((PXSelectBase<CABatch>) this.Document).Current;
    CABatch caBatch = current;
    int? countOfPayments = caBatch.CountOfPayments;
    int num = i;
    caBatch.CountOfPayments = countOfPayments.HasValue ? new int?(countOfPayments.GetValueOrDefault() + num) : new int?();
    ((PXSelectBase<CABatch>) this.Document).Update(current);
  }

  public virtual CABatchDetail AddPayment(PX.Objects.AR.ARPayment aPayment, bool skipCheck)
  {
    if (!skipCheck)
    {
      foreach (PXResult<CABatchDetail> pxResult in ((PXSelectBase<CABatchDetail>) this.BatchPayments).Select(Array.Empty<object>()))
      {
        CABatchDetail detail = PXResult<CABatchDetail>.op_Implicit(pxResult);
        if (CABatchEntry.IsKeyEqual(aPayment, detail))
          return detail;
      }
    }
    CABatchDetail caBatchDetail1 = new CABatchDetail();
    caBatchDetail1.Copy(aPayment);
    CABatchDetail caBatchDetail2 = ((PXSelectBase<CABatchDetail>) this.BatchPayments).Insert(caBatchDetail1);
    this.ChangeCountOfPayments(1);
    return caBatchDetail2;
  }

  protected virtual string BuildAddendaInfo(PX.Objects.AP.APPayment aPayment)
  {
    PaymentMethod pt = ((PXSelectBase<PaymentMethod>) this.paymentMethod).SelectSingle(Array.Empty<object>());
    if (pt.APBatchExportMethod != "P")
      return (string) null;
    if (string.IsNullOrEmpty(pt.APBatchExportPlugInTypeName))
      return (string) null;
    IACHPlugIn plugIn = pt.GetPlugIn();
    if (plugIn == null || (plugIn != null ? (!plugIn.IsAddendaRecordsEnabled((IACHDataProvider) this).GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return (string) null;
    string addendaRecordTemplate = plugIn.GetAddendaRecordTemplate((IACHDataProvider) this);
    if (string.IsNullOrEmpty(addendaRecordTemplate))
      return (string) null;
    try
    {
      return this.CalculateFormula(aPayment.DocType, aPayment.RefNbr, addendaRecordTemplate, 80 /*0x50*/, true);
    }
    catch (AddendaCalculationException ex)
    {
      throw new AddendaCalculationException("The {0} value cannot be calculated. Check the formula in the {1} box on the Plug-in Settings tab of the Payment Methods (CA204000) form. {2}", new object[3]
      {
        (object) "Payment-Related Info (Addenda)",
        (object) "Payment-Related Information",
        (object) ((Exception) ex).Message
      });
    }
  }

  public virtual string CalculateFormula(
    string docType,
    string refNbr,
    string formula,
    int length,
    bool cutByPhrase)
  {
    string empty = string.Empty;
    CABatch current = ((PXSelectBase<CABatch>) this.Document).Current;
    CABatchDetail currentDetail = PXResultset<CABatchDetail>.op_Implicit(PXSelectBase<CABatchDetail, PXSelect<CABatchDetail, Where<CABatchDetail.batchNbr, Equal<Required<CABatchDetail.batchNbr>>, And<CABatchDetail.origModule, Equal<Required<CABatchDetail.origModule>>, And<CABatchDetail.origDocType, Equal<Required<CABatchDetail.origDocType>>, And<CABatchDetail.origRefNbr, Equal<Required<CABatchDetail.origRefNbr>>, And<CABatchDetail.origLineNbr, Equal<Required<CABatchDetail.origLineNbr>>>>>>>>.Config>.Select((PXGraph) this, new object[5]
    {
      (object) current.BatchNbr,
      (object) current.OrigModule,
      (object) docType,
      (object) refNbr,
      (object) 0
    }));
    using (CAFormulaCalculationContext calculationContext = new CAFormulaCalculationContext(this))
    {
      try
      {
        return this.ProcessFormulaEvaluation(formula, currentDetail, length, cutByPhrase);
      }
      catch (Exception ex)
      {
        calculationContext.Dispose();
        throw new AddendaCalculationException(PXLocalizer.Localize(ex.Message), Array.Empty<object>());
      }
    }
  }

  private string ProcessFormulaEvaluation(
    string formula,
    CABatchDetail currentDetail,
    int length,
    bool cutByPhrase)
  {
    ((PXSelectBase<PX.Objects.AP.APPayment>) this.AddendaInfo).SelectSingle(Array.Empty<object>());
    string str1 = string.Empty;
    string empty = string.Empty;
    HashSet<string> values = new HashSet<string>();
    int num = 0;
    foreach (PXResult<PX.Objects.AP.APPayment, PX.Objects.AP.APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.Vendor> pxResult in ((PXSelectBase<PX.Objects.AP.APPayment>) this.AddendaInfo).Select(new object[2]
    {
      (object) currentDetail.OrigDocType,
      (object) currentDetail.OrigRefNbr
    }))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      SyFormulaFinalDelegate formulaFinalDelegate = new SyFormulaFinalDelegate((object) new CABatchEntry.\u003C\u003Ec__DisplayClass106_0()
      {
        \u003C\u003E4__this = this,
        payment = PXResult<PX.Objects.AP.APPayment, PX.Objects.AP.APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.Vendor>.op_Implicit(pxResult),
        adjust = PXResult<PX.Objects.AP.APPayment, PX.Objects.AP.APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.Vendor>.op_Implicit(pxResult),
        invoice = PXResult<PX.Objects.AP.APPayment, PX.Objects.AP.APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.Vendor>.op_Implicit(pxResult),
        vendor = PXResult<PX.Objects.AP.APPayment, PX.Objects.AP.APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.Vendor>.op_Implicit(pxResult)
      }, __methodptr(\u003CProcessFormulaEvaluation\u003Eb__0));
      empty = this._formulaProcessor.Evaluate(formula, formulaFinalDelegate).ToString();
      values.Add(empty);
      string str2 = string.Join("", (IEnumerable<string>) values);
      if (str2.Length > length)
      {
        if (num == 0)
          str1 = str2.Substring(0, length);
        if (!cutByPhrase)
        {
          str1 = str2.Substring(0, length);
          break;
        }
        break;
      }
      str1 = str2;
      ++num;
    }
    if (string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(empty))
      str1 = empty.Substring(0, length);
    return str1;
  }

  protected virtual void ParentFieldUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<CABatch.tranDate>(e.Row, e.OldRow))
      return;
    foreach (PXResult<CABatchDetail> pxResult in ((PXSelectBase<CABatchDetail>) this.Details).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.Details).Cache, (object) PXResult<CABatchDetail>.op_Implicit(pxResult));
  }

  public virtual string GenerateFileName(CABatch aBatch)
  {
    if (aBatch.CashAccountID.HasValue && !string.IsNullOrEmpty(aBatch.PaymentMethodID))
    {
      CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) aBatch.CashAccountID
      }));
      if (cashAccount != null)
        return $"{aBatch.PaymentMethodID}-{cashAccount.CashAccountCD}-{aBatch.TranDate.Value:yyyyMMdd}{aBatch.DateSeqNbr:00000}.txt";
    }
    return string.Empty;
  }

  private void VerifyServiceClassCodeSettings(PXCache sender, CABatch row)
  {
    PaymentMethod paymentMethod = ((PXSelectBase<PaymentMethod>) this.paymentMethod).SelectSingle(Array.Empty<object>());
    if (paymentMethod == null || !paymentMethod.APCreateBatchPayment.GetValueOrDefault() || !(paymentMethod.APBatchExportMethod == "P") || string.IsNullOrEmpty(paymentMethod.APBatchExportPlugInTypeName))
      return;
    IACHPlugInParameter plugInParameter = this.GetPlugInParameter("ServiceClassCode");
    if (plugInParameter == null)
      return;
    bool flag = ((IQueryable<PXResult<PX.Objects.AP.APPayment>>) PXSelectBase<PX.Objects.AP.APPayment, PXSelectJoin<PX.Objects.AP.APPayment, InnerJoin<CABatchDetail, On<CABatchDetail.origDocType, Equal<PX.Objects.AP.APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>>>>, Where<CABatchDetail.batchNbr, Equal<Current<CABatch.batchNbr>>, And<PX.Objects.AP.APPayment.docType, In3<APDocType.refund, APDocType.voidRefund, APDocType.voidQuickCheck, APDocType.cashReturn>>>>.Config>.Select(sender.Graph, Array.Empty<object>())).Any<PXResult<PX.Objects.AP.APPayment>>();
    if (!(plugInParameter?.Value == "200") || flag)
      return;
    sender.RaiseExceptionHandling<CABatch.paymentMethodID>((object) row, (object) row.PaymentMethodID, (Exception) new PXSetPropertyException<CABatch.paymentMethodID>("The batch contains only credit transactions. For successful processing, a different service class code may be required in the payment method settings.", (PXErrorLevel) 2));
  }

  public static void ReleaseDoc(CABatch aDocument)
  {
    if (aDocument.Released.Value || aDocument.Hold.Value)
      throw new PXException("Document status is not valid for processing");
    CABatchEntry.CABatchUpdate instance = PXGraph.CreateInstance<CABatchEntry.CABatchUpdate>();
    CABatch document = PXResultset<CABatch>.op_Implicit(((PXSelectBase<CABatch>) instance.Document).Select(new object[1]
    {
      (object) aDocument.BatchNbr
    }));
    ((PXSelectBase<CABatch>) instance.Document).Current = document;
    bool? nullable = document.Released;
    if (!nullable.Value)
    {
      nullable = document.Hold;
      if (!nullable.Value)
      {
        PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) document.PaymentMethodID
        }));
        if (string.IsNullOrEmpty(document.BatchSeqNbr))
        {
          nullable = paymentMethod.RequireBatchSeqNum;
          if (nullable.GetValueOrDefault())
          {
            ((PXSelectBase) instance.Document).Cache.RaiseExceptionHandling<CABatch.batchSeqNbr>((object) document, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
            {
              (object) PXUIFieldAttribute.GetDisplayName<CABatch.batchSeqNbr>(((PXSelectBase) instance.Document).Cache)
            }));
            throw new PXException("{0} '{1}' record raised at least one error. Please review the errors.", new object[2]
            {
              (object) "Releasing",
              (object) ((PXSelectBase) instance.Document).Cache.DisplayName
            });
          }
        }
        PX.Objects.AP.APRegister apRegister = PXResultset<PX.Objects.AP.APRegister>.op_Implicit(PXSelectBase<PX.Objects.AP.APRegister, PXSelectReadonly2<PX.Objects.AP.APRegister, InnerJoin<CABatchDetail, On<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<PX.Objects.AP.APRegister.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APRegister.refNbr>>>>>, Where<CABatchDetail.batchNbr, Equal<Required<CABatch.batchNbr>>, And<PX.Objects.AP.APRegister.voided, Equal<True>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) document.BatchNbr
        }));
        if (apRegister != null && !string.IsNullOrEmpty(apRegister.RefNbr))
          throw new PXException("This Batch Payments contains voided documents. You must remove them to be able to release the Batch");
        List<PX.Objects.AP.APRegister> list = new List<PX.Objects.AP.APRegister>();
        PXSelectBase<PX.Objects.AP.APPayment> pxSelectBase = (PXSelectBase<PX.Objects.AP.APPayment>) new PXSelectReadonly2<PX.Objects.AP.APPayment, InnerJoin<CABatchDetail, On<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<PX.Objects.AP.APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>>>>>, Where<CABatchDetail.batchNbr, Equal<Optional<CABatch.batchNbr>>, And<PX.Objects.AP.APPayment.released, Equal<boolFalse>>>>((PXGraph) instance);
        foreach (PXResult<PX.Objects.AP.APPayment> pxResult in pxSelectBase.Select(new object[1]
        {
          (object) document.BatchNbr
        }))
        {
          PX.Objects.AP.APPayment apPayment = PXResult<PX.Objects.AP.APPayment>.op_Implicit(pxResult);
          nullable = apPayment.Released;
          if (!nullable.GetValueOrDefault())
            list.Add((PX.Objects.AP.APRegister) apPayment);
        }
        if (list.Count > 0)
          APDocumentRelease.ReleaseDoc(list, true);
        ((PXSelectBase) pxSelectBase).View.Clear();
        if (PXResultset<PX.Objects.AP.APPayment>.op_Implicit(pxSelectBase.Select(new object[1]
        {
          (object) document.BatchNbr
        })) != null)
          throw new PXException("This  batch contains unreleased payments. It can'not be released until all the payments are released successfully");
        CABatch caBatch = instance.ReleaseCABatch(document);
        ((PXSelectBase) instance.Document).Cache.RestoreCopy((object) aDocument, (object) caBatch);
        return;
      }
    }
    throw new PXException("Document status is not valid for processing");
  }

  public static void VoidBatchProc(CABatch aDocument)
  {
    if (!aDocument.Released.Value || aDocument.Hold.Value)
      throw new PXException("Document status is not valid for processing");
    CABatchEntry.CABatchUpdate instance = PXGraph.CreateInstance<CABatchEntry.CABatchUpdate>();
    CABatch document = PXResultset<CABatch>.op_Implicit(((PXSelectBase<CABatch>) instance.Document).Select(new object[1]
    {
      (object) aDocument.BatchNbr
    }));
    ((PXSelectBase<CABatch>) instance.Document).Current = document;
    bool? nullable = document.Released;
    if (nullable.Value)
    {
      nullable = document.Hold;
      if (!nullable.Value)
      {
        PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) document.PaymentMethodID
        }));
        if (string.IsNullOrEmpty(document.BatchSeqNbr))
        {
          nullable = paymentMethod.RequireBatchSeqNum;
          if (nullable.GetValueOrDefault())
          {
            ((PXSelectBase) instance.Document).Cache.RaiseExceptionHandling<CABatch.batchSeqNbr>((object) document, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
            {
              (object) PXUIFieldAttribute.GetDisplayName<CABatch.batchSeqNbr>(((PXSelectBase) instance.Document).Cache)
            }));
            throw new PXException("{0} '{1}' record raised at least one error. Please review the errors.", new object[2]
            {
              (object) "Releasing",
              (object) ((PXSelectBase) instance.Document).Cache.DisplayName
            });
          }
        }
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          instance.VoidAPPayments(document);
          document = instance.VoidCABatch(document);
          transactionScope.Complete();
        }
        ((PXSelectBase) instance.Document).Cache.RestoreCopy((object) aDocument, (object) document);
        return;
      }
    }
    throw new PXException("Document status is not valid for processing");
  }

  public static bool IsKeyEqual(PX.Objects.AP.APPayment payment, CABatchDetail detail)
  {
    return detail.OrigModule == "AP" && payment.DocType == detail.OrigDocType && payment.RefNbr == detail.OrigRefNbr;
  }

  public static bool IsKeyEqual(PX.Objects.AR.ARPayment payment, CABatchDetail detail)
  {
    return detail.OrigModule == "AR" && payment.DocType == detail.OrigDocType && payment.RefNbr == detail.OrigRefNbr;
  }

  public static short GetNextDateSeqNbr(PXGraph graph, CABatch aDocument)
  {
    short nextDateSeqNbr = 0;
    CABatch caBatch = ((PXSelectBase<CABatch>) new PXSelectReadonly<CABatch, Where<CABatch.cashAccountID, Equal<Required<CABatch.cashAccountID>>, And<CABatch.paymentMethodID, Equal<Required<CABatch.paymentMethodID>>, And<CABatch.tranDate, Equal<Required<CABatch.tranDate>>, And<Where<CABatch.skipExport, Equal<True>, And<CABatch.released, Equal<True>, Or<CABatch.skipExport, NotEqual<True>, And<CABatch.exported, Equal<True>>>>>>>>>, OrderBy<Desc<CABatch.dateSeqNbr>>>(graph)).SelectSingle(new object[3]
    {
      (object) aDocument.CashAccountID,
      (object) aDocument.PaymentMethodID,
      (object) aDocument.TranDate
    });
    if (caBatch != null)
    {
      short valueOrDefault = caBatch.DateSeqNbr.GetValueOrDefault();
      if (valueOrDefault >= short.MaxValue || valueOrDefault < short.MinValue)
        throw new PXException("Date Sequence Number is out of range");
      nextDateSeqNbr = (short) ((int) valueOrDefault + 1);
    }
    return nextDateSeqNbr;
  }

  public BatchPayment GetBatchPayment(string batchNumber)
  {
    CABatch caBatch = PXResultset<CABatch>.op_Implicit(PXSelectBase<CABatch, PXSelectReadonly<CABatch, Where<CABatch.origModule, Equal<BatchModule.moduleAP>, And<CABatch.batchNbr, Equal<Required<CABatch.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) batchNumber
    }));
    return new BatchPayment()
    {
      BatchNumber = caBatch.BatchNbr,
      CashAccountID = caBatch.CashAccountID,
      CurrencyID = caBatch.CuryID,
      PaymentMethodID = caBatch.PaymentMethodID,
      DateSequenceNumber = (int) caBatch.DateSeqNbr.GetValueOrDefault(),
      TransactionDate = caBatch.TranDate.Value,
      TransactionDescription = caBatch.TranDesc,
      DateSeqNbr = caBatch.DateSeqNbr,
      BatchSequenceNumber = caBatch.BatchSeqNbr,
      TotalAmount = caBatch.CuryDetailTotal
    };
  }

  public IEnumerable<PX.ACHPlugInBase.Payment> GetPayments(string batchNumber)
  {
    object[] objArray = new object[1]
    {
      (object) batchNumber
    };
    foreach (PXResult<PX.Objects.AP.APPayment, CABatchDetail> pxResult in PXSelectBase<PX.Objects.AP.APPayment, PXSelectJoin<PX.Objects.AP.APPayment, InnerJoin<CABatchDetail, On<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<PX.Objects.AP.APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>>>>, InnerJoin<PaymentMethod, On<PaymentMethod.paymentMethodID, Equal<PX.Objects.AP.APPayment.paymentMethodID>>>>, Where<CABatchDetail.batchNbr, Equal<Required<CABatch.batchNbr>>, And<Where<PX.Objects.AP.APPayment.curyOrigDocAmt, NotEqual<decimal0>, Or<PaymentMethod.skipPaymentsWithZeroAmt, NotEqual<True>>>>>>.Config>.Select((PXGraph) this, objArray))
    {
      PX.Objects.AP.APPayment apPayment = PXResult<PX.Objects.AP.APPayment, CABatchDetail>.op_Implicit(pxResult);
      CABatchDetail caBatchDetail = PXResult<PX.Objects.AP.APPayment, CABatchDetail>.op_Implicit(pxResult);
      yield return new PX.ACHPlugInBase.Payment()
      {
        DocType = apPayment.DocType,
        RefNbr = apPayment.RefNbr,
        ExtRefNbr = apPayment.ExtRefNbr,
        Amount = apPayment.CuryOrigDocAmt.GetValueOrDefault(),
        VendorID = apPayment.VendorID,
        VendorLocationID = apPayment.VendorLocationID,
        AddendaPaymentRelatedInfo = caBatchDetail.AddendaPaymentRelatedInfo,
        AdjustmentDate = apPayment.AdjDate,
        Description = apPayment.DocDesc,
        DrCr = apPayment.DrCr
      };
    }
  }

  public Dictionary<string, PaymentMethodDetail> GetRemittanceDetails()
  {
    return this.SelectRemittanceDetails();
  }

  protected virtual Dictionary<string, PaymentMethodDetail> SelectRemittanceDetails()
  {
    Dictionary<string, PaymentMethodDetail> dictionary = new Dictionary<string, PaymentMethodDetail>();
    foreach (PXResult<PaymentMethodDetail> pxResult in PXSelectBase<PaymentMethodDetail, PXSelectReadonly<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<CABatch.paymentMethodID>>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      PaymentMethodDetail paymentMethodDetail = PXResult<PaymentMethodDetail>.op_Implicit(pxResult);
      dictionary.Add(paymentMethodDetail.DetailID.Trim(), new PaymentMethodDetail()
      {
        DetailID = paymentMethodDetail.DetailID,
        Description = paymentMethodDetail.Descr,
        Required = paymentMethodDetail.IsRequired
      });
    }
    foreach (PXResult<CashAccountPaymentMethodDetail> pxResult in PXSelectBase<CashAccountPaymentMethodDetail, PXSelectReadonly<CashAccountPaymentMethodDetail, Where<CashAccountPaymentMethodDetail.paymentMethodID, Equal<Current<CABatch.paymentMethodID>>, And<CashAccountPaymentMethodDetail.cashAccountID, Equal<Current<CABatch.cashAccountID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      CashAccountPaymentMethodDetail paymentMethodDetail = PXResult<CashAccountPaymentMethodDetail>.op_Implicit(pxResult);
      if (dictionary.ContainsKey(paymentMethodDetail.DetailID))
        dictionary[paymentMethodDetail.DetailID].Value = paymentMethodDetail.DetailValue;
    }
    return dictionary;
  }

  public Dictionary<string, PaymentMethodDetail> GetVendorDetails(int? vendorID, int? locationID)
  {
    Dictionary<string, PaymentMethodDetail> vendorDetails = new Dictionary<string, PaymentMethodDetail>();
    foreach (PXResult<PaymentMethodDetail, PX.Objects.CR.Location, VendorPaymentMethodDetail> paymentMethodDetail1 in this.SelectVendorPaymentMethodDetails(vendorID, locationID))
    {
      PaymentMethodDetail paymentMethodDetail2 = PXResult<PaymentMethodDetail, PX.Objects.CR.Location, VendorPaymentMethodDetail>.op_Implicit(paymentMethodDetail1);
      VendorPaymentMethodDetail paymentMethodDetail3 = PXResult<PaymentMethodDetail, PX.Objects.CR.Location, VendorPaymentMethodDetail>.op_Implicit(paymentMethodDetail1);
      vendorDetails.Add(paymentMethodDetail2.DetailID.Trim(), new PaymentMethodDetail()
      {
        DetailID = paymentMethodDetail2.DetailID,
        Description = paymentMethodDetail2.Descr,
        Value = paymentMethodDetail3.DetailValue,
        Required = paymentMethodDetail2.IsRequired
      });
    }
    return vendorDetails;
  }

  protected virtual PXResultset<PaymentMethodDetail> SelectVendorPaymentMethodDetails(
    int? vendorID,
    int? locationID)
  {
    PX.Objects.CR.Location location = PX.Objects.CR.Location.PK.Find((PXGraph) this, vendorID, locationID);
    return PXSelectBase<PaymentMethodDetail, PXSelectReadonly2<PaymentMethodDetail, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.vPaymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>, And<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>, LeftJoin<VendorPaymentMethodDetail, On<VendorPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>, And<VendorPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>, And<VendorPaymentMethodDetail.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<VendorPaymentMethodDetail.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>>>>, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<CABatch.paymentMethodID>>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) vendorID,
      (object) location.VPaymentInfoLocationID,
      (object) vendorID,
      (object) location.VPaymentInfoLocationID
    });
  }

  public string GetCashAccountCode()
  {
    return ((PXSelectBase<CashAccount>) this.cashAccount).SelectSingle(Array.Empty<object>()).CashAccountCD?.Trim();
  }

  public string GetVendorName(int? vendorID)
  {
    return PX.Objects.AP.Vendor.PK.Find((PXGraph) this, vendorID).AcctCD?.Trim();
  }

  public bool SaveFile(byte[] data, string fileName)
  {
    return this.SaveFile(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<CABatch>) this.Document).Current, fileName, data, true);
  }

  protected virtual bool SaveFile(
    PXCache cache,
    object cacheRow,
    string fileName,
    byte[] data,
    bool createRevision)
  {
    if (string.IsNullOrWhiteSpace(fileName))
      throw new PXArgumentException(nameof (fileName));
    if (data == null)
      throw new PXArgumentException(nameof (data));
    if (data.Length == 0)
      return false;
    try
    {
      UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
      FileInfo fileInfo1 = new FileInfo(fileName, (string) null, data);
      FileInfo fileInfo2 = fileInfo1;
      int num = createRevision ? 1 : 0;
      instance.SaveFile(fileInfo2, (FileExistsAction) num);
      Guid? uid = fileInfo1.UID;
      if (uid.HasValue)
      {
        PXCache pxCache = cache;
        object obj = cacheRow;
        Guid[] guidArray = new Guid[1];
        uid = fileInfo1.UID;
        guidArray[0] = uid.Value;
        PXNoteAttribute.SetFileNotes(pxCache, obj, guidArray);
        return true;
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteWarning($"Unable to save file '{fileName}'. {ex.Message}");
    }
    return false;
  }

  public virtual IEnumerable<IACHPlugInParameter> GetPlugInParameters()
  {
    PaymentMethod paymentMethod = ((PXSelectBase<PaymentMethod>) this.paymentMethod).SelectSingle(Array.Empty<object>());
    PXSelect<ACHPlugInParameter, Where<ACHPlugInParameter.paymentMethodID, Equal<Required<ACHPlugInParameter.paymentMethodID>>, And<ACHPlugInParameter.plugInTypeName, Equal<Required<ACHPlugInParameter.plugInTypeName>>>>> plugInParameters = this.PlugInParameters;
    object[] objArray = new object[2]
    {
      (object) paymentMethod.PaymentMethodID,
      (object) paymentMethod.APBatchExportPlugInTypeName
    };
    foreach (PXResult<ACHPlugInParameter> pxResult in ((PXSelectBase<ACHPlugInParameter>) plugInParameters).Select(objArray))
      yield return (IACHPlugInParameter) PXResult<ACHPlugInParameter>.op_Implicit(pxResult);
  }

  public IACHPlugInParameter GetPlugInParameter(string parameterName)
  {
    PaymentMethod paymentMethod = ((PXSelectBase<PaymentMethod>) this.paymentMethod).SelectSingle(Array.Empty<object>());
    return (IACHPlugInParameter) ((PXSelectBase<ACHPlugInParameter>) this.PlugInParameterByName).SelectSingle(new object[3]
    {
      (object) paymentMethod.PaymentMethodID,
      (object) paymentMethod.APBatchExportPlugInTypeName,
      (object) parameterName
    });
  }

  public static class ExportProviderParams
  {
    public const string FileName = "FileName";
    public const string BatchNbr = "BatchNbr";
    public const string BatchSequenceStartingNbr = "BatchStartNumber";
    public const string FileSeqNumber = "FileSeqNumber";
  }

  public class PrintProcessContext : IDisposable
  {
    private CABatchEntry BatchEntryGraph { get; set; }

    public PrintProcessContext(CABatchEntry graph)
    {
      graph.IsPrintProcess = true;
      this.BatchEntryGraph = graph;
    }

    public void Dispose() => this.BatchEntryGraph.IsPrintProcess = false;
  }

  [PXHidden]
  public class CABatchUpdate : PXGraph<CABatchEntry.CABatchUpdate>
  {
    public PXSelect<CABatch, Where<CABatch.batchNbr, Equal<Required<CABatch.batchNbr>>>> Document;
    public PXSelect<CABatchDetail, Where<CABatchDetail.batchNbr, Equal<Current<CABatch.batchNbr>>>> Details;
    public PXSelectJoin<PX.Objects.AP.APPayment, InnerJoin<CABatchDetail, On<CABatchDetail.origDocType, Equal<PX.Objects.AP.APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>, And<CABatchDetail.origModule, Equal<BatchModule.moduleAP>>>>>, Where<CABatchDetail.batchNbr, Equal<Optional<CABatch.batchNbr>>>> APPaymentList;
    public FbqlSelect<SelectFromBase<PX.Objects.AP.APPayment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CABatchDetail>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    CABatchDetail.origDocType, 
    #nullable disable
    Equal<PX.Objects.AP.APPayment.docType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    CABatchDetail.origRefNbr, 
    #nullable disable
    Equal<PX.Objects.AP.APPayment.refNbr>>>>>.And<BqlOperand<
    #nullable enable
    CABatchDetail.origModule, IBqlString>.IsEqual<
    #nullable disable
    BatchModule.moduleAP>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    CABatchDetail.batchNbr, 
    #nullable disable
    Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PX.Objects.AP.APPayment.printed, 
    #nullable disable
    Equal<True>>>>>.And<BqlOperand<
    #nullable enable
    PX.Objects.AP.APPayment.released, IBqlBool>.IsNotEqual<
    #nullable disable
    True>>>>, PX.Objects.AP.APPayment>.View PrintedAPPayments;
    public PXSetup<PX.Objects.CA.CASetup> casetup;

    public bool AutoPost => ((PXSelectBase<PX.Objects.CA.CASetup>) this.casetup).Current.AutoPostOption.Value;

    public virtual CABatch ReleaseCABatch(CABatch document)
    {
      document.Released = new bool?(true);
      document.Exported = new bool?(true);
      if (document.SkipExport.GetValueOrDefault())
        document.DateSeqNbr = new short?(CABatchEntry.GetNextDateSeqNbr((PXGraph) this, document));
      this.RecalcTotals();
      document = ((PXSelectBase<CABatch>) this.Document).Update(document);
      ((PXGraph) this).Actions.PressSave();
      return document;
    }

    public virtual CABatch VoidCABatch(CABatch document)
    {
      document.Voided = new bool?(true);
      document = ((PXSelectBase<CABatch>) this.Document).Update(document);
      ((PXGraph) this).Actions.PressSave();
      return document;
    }

    public void CancelBatchProc(CABatch document)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        this.VoidAPPayments(document);
        this.UnprintAPPayments(document);
        document.Canceled = new bool?(true);
        document.CuryDetailTotal = new Decimal?(0M);
        document.DetailTotal = new Decimal?(0M);
        document.CountOfPayments = new int?(0);
        ((PXSelectBase<CABatch>) this.Document).Update(document);
        foreach (PXResult<CABatchDetail> pxResult in ((PXSelectBase<CABatchDetail>) this.Details).Select(Array.Empty<object>()))
          ((PXSelectBase<CABatchDetail>) this.Details).Delete(PXResult<CABatchDetail>.op_Implicit(pxResult));
        ((PXGraph) this).Actions.PressSave();
        transactionScope.Complete((PXGraph) this);
      }
    }

    private void UnprintAPPayments(CABatch document)
    {
      foreach (PXResult<PX.Objects.AP.APPayment> pxResult in ((PXSelectBase<PX.Objects.AP.APPayment>) this.PrintedAPPayments).Select(new object[1]
      {
        (object) document.BatchNbr
      }))
      {
        PX.Objects.AP.APPayment apPayment = PXResult<PX.Objects.AP.APPayment>.op_Implicit(pxResult);
        ((SelectedEntityEvent<PX.Objects.AP.APPayment>) PXEntityEventBase<PX.Objects.AP.APPayment>.Container<PX.Objects.AP.APPayment.Events>.Select((Expression<Func<PX.Objects.AP.APPayment.Events, PXEntityEvent<PX.Objects.AP.APPayment.Events>>>) (se => se.CancelPrintCheck))).FireOn((PXGraph) this, apPayment);
      }
    }

    public virtual void RecalcTotals()
    {
      CABatch current = ((PXSelectBase<CABatch>) this.Document).Current;
      if (current == null)
        return;
      CABatch caBatch1 = current;
      CABatch caBatch2 = current;
      CABatch caBatch3 = current;
      Decimal? nullable1 = new Decimal?(0M);
      Decimal? nullable2 = nullable1;
      caBatch3.Total = nullable2;
      Decimal? nullable3;
      Decimal? nullable4 = nullable3 = nullable1;
      caBatch2.CuryDetailTotal = nullable3;
      Decimal? nullable5 = nullable4;
      caBatch1.DetailTotal = nullable5;
      foreach (PXResult<PX.Objects.AP.APPayment, CABatchDetail> pxResult in ((PXSelectBase<PX.Objects.AP.APPayment>) this.APPaymentList).Select(Array.Empty<object>()))
      {
        PX.Objects.AP.APPayment apPayment = PXResult<PX.Objects.AP.APPayment, CABatchDetail>.op_Implicit(pxResult);
        if (!string.IsNullOrEmpty(apPayment.RefNbr))
        {
          CABatch caBatch4 = current;
          Decimal? nullable6 = caBatch4.CuryDetailTotal;
          nullable1 = apPayment.CuryOrigDocAmt;
          caBatch4.CuryDetailTotal = nullable6.HasValue & nullable1.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
          CABatch caBatch5 = current;
          nullable1 = caBatch5.DetailTotal;
          nullable6 = apPayment.OrigDocAmt;
          caBatch5.DetailTotal = nullable1.HasValue & nullable6.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
        }
      }
    }

    public virtual void VoidAPPayments(CABatch document)
    {
      List<PX.Objects.AP.APRegister> unreleasedList = new List<PX.Objects.AP.APRegister>();
      APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
      PXSelectBase<PX.Objects.AP.APPayment> pxSelectBase = (PXSelectBase<PX.Objects.AP.APPayment>) new PXSelectReadonly2<PX.Objects.AP.APPayment, InnerJoin<CABatchDetail, On<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<PX.Objects.AP.APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>>>>>, Where<CABatchDetail.batchNbr, Equal<Required<CABatch.batchNbr>>, And<PX.Objects.AP.APPayment.released, Equal<boolTrue>>>>((PXGraph) this);
      foreach (PXResult<PX.Objects.AP.APPayment> pxResult in pxSelectBase.Select(new object[1]
      {
        (object) document.BatchNbr
      }))
      {
        PX.Objects.AP.APPayment payment = PXResult<PX.Objects.AP.APPayment>.op_Implicit(pxResult);
        if (payment.Released.GetValueOrDefault())
        {
          PX.Objects.AP.APRegister apRegister = this.VoidDetail(instance, payment, document.VoidDate);
          if (apRegister != null)
          {
            PXResultset<PX.Objects.AP.APRegister> pxResultset = PXSelectBase<PX.Objects.AP.APRegister, PXSelectReadonly<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
            {
              (object) apRegister.DocType,
              (object) apRegister.RefNbr
            });
            unreleasedList.Add(PXResultset<PX.Objects.AP.APRegister>.op_Implicit(pxResultset));
          }
        }
      }
      if (unreleasedList.Count > 0)
        this.ReleaseVoidPayments(unreleasedList);
      ((PXSelectBase) pxSelectBase).View.Clear();
    }

    private void ReleaseVoidPayments(List<PX.Objects.AP.APRegister> unreleasedList)
    {
      List<PX.Objects.GL.Batch> externalPostList = new List<PX.Objects.GL.Batch>();
      APDocumentRelease.ReleaseDoc(unreleasedList, true, externalPostList);
      if (externalPostList.Count <= 0 || !this.AutoPost)
        return;
      List<PX.Objects.GL.Batch> batchList = new List<PX.Objects.GL.Batch>();
      PostGraph instance = PXGraph.CreateInstance<PostGraph>();
      foreach (PX.Objects.GL.Batch b in externalPostList)
      {
        try
        {
          ((PXGraph) instance).Clear();
          instance.PostBatchProc(b);
        }
        catch (Exception ex)
        {
          batchList.Add(b);
        }
      }
      if (batchList.Count > 0)
        throw new PXException("Documents were successfully created, but {0} of {1} were not posted", new object[2]
        {
          (object) batchList.Count,
          (object) batchList.Count
        });
    }

    protected virtual PX.Objects.AP.APRegister VoidDetail(
      APPaymentEntry graph,
      PX.Objects.AP.APPayment payment,
      DateTime? voidDate)
    {
      ((PXGraph) graph).Clear();
      ((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current = payment;
      ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) graph.currencyinfo).Select(Array.Empty<object>());
      if (((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current != null)
      {
        bool? nullable = ((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current.Released;
        if (nullable.GetValueOrDefault())
        {
          nullable = ((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current.Voided;
          bool flag = false;
          if (nullable.GetValueOrDefault() == flag & nullable.HasValue && APPaymentType.VoidEnabled(((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current.DocType))
          {
            PX.Objects.AP.APAdjust adj = PXResultset<PX.Objects.AP.APAdjust>.op_Implicit(PXSelectBase<PX.Objects.AP.APAdjust, PXSelect<PX.Objects.AP.APAdjust, Where<PX.Objects.AP.APAdjust.adjdDocType, Equal<Required<PX.Objects.AP.APAdjust.adjdDocType>>, And<PX.Objects.AP.APAdjust.adjdRefNbr, Equal<Required<PX.Objects.AP.APAdjust.adjdRefNbr>>, And<Where<PX.Objects.AP.APAdjust.adjgDocType, In3<APDocType.check, APDocType.prepayment>, Or<PX.Objects.AP.APAdjust.adjgDocType, Equal<APDocType.refund>, And<PX.Objects.AP.APAdjust.voided, NotEqual<True>>>>>>>>.Config>.Select((PXGraph) graph, new object[2]
            {
              (object) ((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current.DocType,
              (object) ((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current.RefNbr
            }));
            if (adj != null && !adj.IsSelfAdjustment() && (adj.AdjdDocType == "CHK" || adj.AdjdDocType == "PPM"))
              throw new PXException("The prepayment cannot be voided. Void the {0} payment instead.", new object[1]
              {
                (object) adj.AdjgRefNbr
              });
            if (adj != null && !adj.IsSelfAdjustment() && adj.AdjdDocType == "REF")
            {
              nullable = adj.Voided;
              if (nullable.HasValue)
                throw new PXException("{0} {1} has been refunded with {2} {3}.", new object[4]
                {
                  (object) GetLabel.For<APDocType>(((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current.DocType),
                  (object) ((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current.RefNbr,
                  (object) GetLabel.For<APDocType>(adj.AdjgDocType),
                  (object) adj.AdjgRefNbr
                });
            }
            PX.Objects.AP.APPayment apPayment = PXResultset<PX.Objects.AP.APPayment>.op_Implicit(((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Search<PX.Objects.AP.APPayment.refNbr>((object) ((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current.RefNbr, new object[1]
            {
              (object) APPaymentType.GetVoidingAPDocType(((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current.DocType)
            }));
            if (apPayment != null)
              return (PX.Objects.AP.APRegister) apPayment;
            foreach (PXResult<PX.Objects.AP.APAdjust> pxResult in ((PXSelectBase<PX.Objects.AP.APAdjust>) graph.Adjustments_Raw).Select(Array.Empty<object>()))
            {
              PX.Objects.AP.APAdjust apAdjust = PXResult<PX.Objects.AP.APAdjust>.op_Implicit(pxResult);
              ((PXSelectBase) graph.Adjustments).Cache.Delete((object) apAdjust);
            }
            ((PXAction) graph.Save).Press();
            PX.Objects.AP.APPayment copy = PXCache<PX.Objects.AP.APPayment>.CreateCopy(((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current);
            graph.FinPeriodUtils.VerifyAndSetFirstOpenedFinPeriod<PX.Objects.AP.APPayment.finPeriodID, PX.Objects.AP.APPayment.branchID>(((PXSelectBase) graph.Document).Cache, (object) copy, (PXSelectBase<OrganizationFinPeriod>) graph.finperiod, typeof (OrganizationFinPeriod.aPClosed));
            graph.TryToVoidCheck(copy);
            if (voidDate.HasValue)
            {
              PX.Objects.AP.APPayment current = ((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current;
              current.AdjDate = voidDate;
              ((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Update(current);
            }
            ((PXSelectBase) graph.Document).Cache.RaiseExceptionHandling<PX.Objects.AP.APPayment.finPeriodID>((object) ((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current, (object) ((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current.FinPeriodID, (Exception) null);
            if (((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current.Hold.GetValueOrDefault())
              ((PXAction) graph.releaseFromHold).Press();
            ((PXAction) graph.Save).Press();
            return (PX.Objects.AP.APRegister) ((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Current;
          }
        }
      }
      return (PX.Objects.AP.APRegister) null;
    }
  }
}
