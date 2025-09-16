// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADepositEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.BQLConstants;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.CA;

[Serializable]
public class CADepositEntry : PXGraph<
#nullable disable
CADepositEntry, CADeposit>
{
  public PXInitializeState<CADeposit> initializeState;
  public PXAction<CADeposit> putOnHold;
  public PXAction<CADeposit> releaseFromHold;
  public PXAction<CADeposit> printDepositSlip;
  public PXWorkflowEventHandler<CADeposit> OnReleaseDocument;
  public PXWorkflowEventHandler<CADeposit> OnVoidDocument;
  public PXWorkflowEventHandler<CADeposit> OnUpdateStatus;
  public PXAction<CADeposit> Release;
  public PXAction<CADeposit> VoidDocument;
  public PXAction<CADeposit> addPayment;
  public PXAction<CADeposit> viewBatch;
  public PXAction<PX.Objects.Extensions.MultiCurrency.Document> viewBAccount;
  public PXAction<CADeposit> viewDocument;
  public PXSelect<PX.Objects.AR.ARRegister> dummy_for_correct_navigation;
  public PXSelect<CADeposit, Where<CADeposit.tranType, Equal<Optional<CADeposit.tranType>>>> Document;
  public PXSelect<CADeposit, Where<CADeposit.tranType, Equal<Current<CADeposit.tranType>>, And<CADeposit.refNbr, Equal<Current<CADeposit.refNbr>>>>> DocumentCurrent;
  public PXSelect<CADepositDetail, Where<CADepositDetail.refNbr, Equal<Current<CADeposit.refNbr>>, And<CADepositDetail.tranType, Equal<Current<CADeposit.tranType>>>>> Details;
  public PXSelect<CADepositDetail, Where<CADepositDetail.tranType, Equal<Required<CADeposit.tranType>>, And<CADepositDetail.refNbr, Equal<Required<CADeposit.refNbr>>, And<CADepositDetail.origModule, Equal<Required<CADepositDetail.origModule>>, And<CADepositDetail.origDocType, Equal<Required<CADepositDetail.origDocType>>, And<CADepositDetail.origRefNbr, Equal<Required<CADepositDetail.origRefNbr>>>>>>>> CADepositDetail_OrigModuleDocTypeRefNbr;
  public PXSelectJoin<CADepositDetail, LeftJoin<PaymentInfo, On<CADepositDetail.origDocType, Equal<PaymentInfo.docType>, And<CADepositDetail.origRefNbr, Equal<PaymentInfo.refNbr>>>, InnerJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PaymentInfo.bAccountID>>, InnerJoin<PX.Objects.CA.Light.Location, On<PX.Objects.CA.Light.Location.locationID, Equal<PaymentInfo.locationID>>>>>, Where<CADepositDetail.refNbr, Equal<Current<CADeposit.refNbr>>, And<CADepositDetail.tranType, Equal<Current<CADeposit.tranType>>>>> DepositPayments;
  public PXFilter<CADepositEntry.PaymentFilter> filter;
  public PXSelectJoin<PaymentInfo, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PaymentInfo.bAccountID>>, LeftJoin<PX.Objects.CA.Light.Location, On<PX.Objects.CA.Light.Location.locationID, Equal<PaymentInfo.locationID>>, LeftJoin<CADepositDetail, On<CADepositDetail.origDocType, Equal<PaymentInfo.docType>, And<CADepositDetail.origRefNbr, Equal<PaymentInfo.refNbr>, And<CADepositDetail.origModule, Equal<PaymentInfo.module>>>>, InnerJoin<CashAccountDeposit, On<CashAccountDeposit.depositAcctID, Equal<PaymentInfo.cashAccountID>, And<Where<CashAccountDeposit.paymentMethodID, Equal<PaymentInfo.paymentMethodID>, Or<CashAccountDeposit.paymentMethodID, Equal<EmptyString>>>>>>>>>, Where<CashAccountDeposit.cashAccountID, Equal<Current<CADeposit.cashAccountID>>, And<PaymentInfo.depositNbr, IsNull, And<PaymentInfo.depositAfter, LessEqual<Current<CADeposit.tranDate>>, And<CADepositDetail.refNbr, IsNull, And<Where<PaymentInfo.paymentMethodID, Equal<Current<CADepositEntry.PaymentFilter.paymentMethodID>>, Or<Current<CADepositEntry.PaymentFilter.paymentMethodID>, IsNull>>>>>>>> AvailablePayments;
  public PXSelect<CADepositEntry.ARPaymentUpdate, Where<CADepositEntry.ARPaymentUpdate.docType, Equal<Required<CADepositEntry.ARPaymentUpdate.docType>>, And<CADepositEntry.ARPaymentUpdate.refNbr, Equal<Required<CADepositEntry.ARPaymentUpdate.refNbr>>>>> paymentUpdate;
  public PXSelect<CADepositEntry.APPaymentUpdate, Where<CADepositEntry.APPaymentUpdate.docType, Equal<Required<CADepositEntry.APPaymentUpdate.docType>>, And<CADepositEntry.APPaymentUpdate.refNbr, Equal<Required<CADepositEntry.APPaymentUpdate.refNbr>>>>> paymentUpdateAP;
  public PXSelect<CADepositEntry.CAAdjUpdate, Where<CADepositEntry.CAAdjUpdate.adjTranType, Equal<Required<CADepositEntry.CAAdjUpdate.adjTranType>>, And<CADepositEntry.CAAdjUpdate.adjRefNbr, Equal<Required<CADepositEntry.CAAdjUpdate.adjRefNbr>>>>> paymentUpdateCA;
  public PXSelect<CCBatch, Where<CCBatch.depositNbr, Equal<Current<CADeposit.refNbr>>, And<Current<CADeposit.tranType>, Equal<CCBatch.depositType>>>> ccBatch;
  public PXSelect<CADepositCharge, Where<CADepositCharge.tranType, Equal<Current<CADeposit.tranType>>, And<CADepositCharge.refNbr, Equal<Current<CADeposit.refNbr>>>>> Charges;
  public PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Optional<CADeposit.cashAccountID>>>> cashAccount;
  public PXSetup<CASetup> casetup;
  public PXSelect<CATran, Where<CATran.origModule, Equal<BatchModule.moduleCA>, And<CATran.origTranType, Equal<Current<CADeposit.tranType>>, And<CATran.origRefNbr, Equal<Current<CADeposit.refNbr>>>>>> cATransHeaderAndDetails;
  public PXSelect<PX.Objects.CA.Light.BAccount> bAccountDummy;
  public PXSelect<PX.Objects.CA.Light.Location> locationDummy;
  private bool _isMassDelete;
  private bool _IsVoidCheckInProgress;

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintDepositSlip(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter, reportID ?? "CA656500", false);
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CADepositEntry.\u003C\u003Ec__DisplayClass16_0 cDisplayClass160 = new CADepositEntry.\u003C\u003Ec__DisplayClass16_0();
    ((PXAction) this.Save).Press();
    List<CADeposit> caDepositList = new List<CADeposit>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass160.doc = ((PXSelectBase<CADeposit>) this.Document).Current;
    // ISSUE: reference to a compiler-generated field
    bool? released = cDisplayClass160.doc.Released;
    bool flag1 = false;
    if (released.GetValueOrDefault() == flag1 & released.HasValue)
    {
      // ISSUE: reference to a compiler-generated field
      bool? hold = cDisplayClass160.doc.Hold;
      bool flag2 = false;
      if (hold.GetValueOrDefault() == flag2 & hold.HasValue)
      {
        // ISSUE: reference to a compiler-generated field
        caDepositList.Add(cDisplayClass160.doc);
        this.CheckIfUnreleasedVoidedEntries();
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass160, __methodptr(\u003Crelease\u003Eb__0)));
        return (IEnumerable) caDepositList;
      }
    }
    return adapter.Get();
  }

  protected virtual void CheckIfUnreleasedVoidedEntries()
  {
    bool flag1 = false;
    CAReleaseProcess instance = PXGraph.CreateInstance<CAReleaseProcess>();
    bool flag2 = false;
    bool flag3 = false;
    foreach (PXResult<CADepositDetail> pxResult in ((PXSelectBase<CADepositDetail>) this.DepositPayments).Select(Array.Empty<object>()))
    {
      CADepositDetail detail = PXResult<CADepositDetail>.op_Implicit(pxResult);
      try
      {
        this.CheckIfUnreleasedVoidingEntryExist(detail);
      }
      catch (PXException ex)
      {
        ((PXSelectBase) this.DepositPayments).Cache.RaiseExceptionHandling<CADepositDetail.origModule>((object) detail, (object) detail.OrigModule, (Exception) new PXSetPropertyException<CADepositDetail.origModule>(((Exception) ex).Message, (PXErrorLevel) 5));
        flag1 = true;
        flag3 = true;
      }
      if (!string.IsNullOrEmpty(detail.OrigRefNbr) && detail.OrigModule == "AR" && detail.DetailType == "VCD" && instance.IsVoidedEntryWithoutPair(detail))
      {
        ((PXSelectBase) this.DepositPayments).Cache.RaiseExceptionHandling<CADepositDetail.origModule>((object) detail, (object) detail.OrigModule, (Exception) new PXSetPropertyException<CADepositDetail.origModule>("The voided deposit cannot be released because of the invalid status of the included payment. Delete the voided payment before releasing the deposit.", (PXErrorLevel) 5));
        flag1 = true;
        flag2 = true;
      }
    }
    if (!flag1)
      return;
    if (((PXSelectBase<CADeposit>) this.Document).Current.DocType == "CDT")
      throw new PXException("The deposit cannot be released because at least one included payment has an unreleased voiding entry. Delete the unreleased voided payments before releasing the deposit.");
    if (flag3)
      throw new PXException("The voided deposit cannot be released because at least one included payment has an unreleased voiding entry. Delete the unreleased voided payments before releasing the voided deposit.");
    if (flag2)
      throw new PXException("The voided deposit cannot be released due to an invalid status of at least one included payment.");
  }

  protected virtual void CheckIfUnreleasedVoidingEntryExist(CADepositDetail detail)
  {
    bool flag1 = false;
    bool flag2 = false;
    switch (detail.OrigModule)
    {
      case "AR":
        flag2 = ARPaymentType.AllVoidingTypes.Contains(detail.OrigDocType);
        string str1 = flag2 ? detail.OrigDocType : ARPaymentType.GetVoidingARDocType(detail.OrigDocType);
        if (!string.IsNullOrEmpty(str1))
        {
          flag1 = ((IQueryable<PXResult<PX.Objects.AR.ARRegister>>) PXSelectBase<PX.Objects.AR.ARRegister, PXSelect<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.released, NotEqual<True>, And<PX.Objects.AR.ARRegister.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) str1,
            (object) detail.OrigRefNbr
          })).Any<PXResult<PX.Objects.AR.ARRegister>>();
          break;
        }
        break;
      case "AP":
        flag2 = APPaymentType.AllVoidingTypes.Contains(detail.OrigDocType);
        string str2 = flag2 ? detail.OrigDocType : APPaymentType.GetVoidingAPDocType(detail.OrigDocType);
        if (!string.IsNullOrEmpty(str2))
        {
          flag1 = ((IQueryable<PXResult<PX.Objects.AP.APRegister>>) PXSelectBase<PX.Objects.AP.APRegister, PXSelect<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.released, NotEqual<True>, And<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) str2,
            (object) detail.OrigRefNbr
          })).Any<PXResult<PX.Objects.AP.APRegister>>();
          break;
        }
        break;
    }
    if (!flag1)
      return;
    switch (detail.OrigModule)
    {
      case "AR":
        if (detail.OrigDocType == "REF")
        {
          if (!flag2)
            throw new PXException("The refund {0} has an unreleased voiding entry. Delete the unreleased voiding entry before releasing the deposit or exclude the refund from the deposit.", new object[1]
            {
              (object) detail.OrigRefNbr
            });
          throw new PXException("The refund {0} has an unreleased voiding entry. Delete the unreleased voided entry before releasing the deposit or exclude the refund from the deposit.", new object[1]
          {
            (object) detail.OrigRefNbr
          });
        }
        if (!flag2)
          throw new PXException("The payment {0} has an unreleased voiding entry. Delete the unreleased voiding entry before releasing the deposit or exclude the payment from the deposit.", new object[1]
          {
            (object) detail.OrigRefNbr
          });
        throw new PXException("The payment {0} has an unreleased voiding entry. Delete the unreleased voided entry before releasing the deposit or exclude the payment from the deposit.", new object[1]
        {
          (object) detail.OrigRefNbr
        });
      case "AP":
        if (detail.OrigDocType == "REF")
        {
          if (!flag2)
            throw new PXException("The refund {0} has an unreleased voiding entry. Delete the unreleased voiding entry before releasing the deposit or exclude the refund from the deposit.", new object[1]
            {
              (object) detail.OrigRefNbr
            });
          throw new PXException("The refund {0} has an unreleased voiding entry. Delete the unreleased voided entry before releasing the deposit or exclude the refund from the deposit.", new object[1]
          {
            (object) detail.OrigRefNbr
          });
        }
        if (!flag2)
          throw new PXException("The payment {0} has an unreleased voiding entry. Delete the unreleased voiding entry before releasing the deposit or exclude the payment from the deposit.", new object[1]
          {
            (object) detail.OrigRefNbr
          });
        throw new PXException("The payment {0} has an unreleased voiding entry. Delete the unreleased voided entry before releasing the deposit or exclude the payment from the deposit.", new object[1]
        {
          (object) detail.OrigRefNbr
        });
    }
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable voidDocument(PXAdapter adapter)
  {
    CADeposit current = ((PXSelectBase<CADeposit>) this.Document).Current;
    if (current != null && current.Released.GetValueOrDefault())
    {
      if (current.TranType == "CDT")
      {
        try
        {
          this._IsVoidCheckInProgress = true;
          this.VoidDepositProc(current);
          return (IEnumerable) new List<CADeposit>()
          {
            ((PXSelectBase<CADeposit>) this.Document).Current
          };
        }
        finally
        {
          this._IsVoidCheckInProgress = false;
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPayment(PXAdapter adapter)
  {
    if (((PXSelectBase<CADeposit>) this.Document).Current != null && ((PXSelectBase<CADeposit>) this.Document).Current.TranType == "CDT" && !((PXSelectBase<CADeposit>) this.Document).Current.Released.GetValueOrDefault())
    {
      if (((PXSelectBase<CADepositEntry.PaymentFilter>) this.filter).Current != null)
      {
        ((PXSelectBase) this.filter).Cache.RaiseRowSelected((object) ((PXSelectBase<CADepositEntry.PaymentFilter>) this.filter).Current);
        ((PXSelectBase<CADepositEntry.PaymentFilter>) this.filter).Current.SelectionTotal = new Decimal?(0M);
        ((PXSelectBase<CADepositEntry.PaymentFilter>) this.filter).Current.NumberOfDocuments = new int?(0);
      }
      bool flag = false;
      if (((PXSelectBase<PaymentInfo>) this.AvailablePayments).AskExt() == 1)
      {
        ((PXSelectBase) this.AvailablePayments).Cache.AllowInsert = true;
        IEnumerable<PaymentInfo> payments = ((PXSelectBase) this.AvailablePayments).Cache.Inserted.Cast<PaymentInfo>().Where<PaymentInfo>((Func<PaymentInfo, bool>) (payment => payment.Selected.GetValueOrDefault()));
        this.AddPaymentInfoBatch(payments);
        foreach (PaymentInfo paymentInfo in payments)
        {
          paymentInfo.Selected = new bool?(false);
          flag = true;
        }
      }
      else
      {
        foreach (PaymentInfo paymentInfo in ((PXSelectBase) this.AvailablePayments).Cache.Inserted)
          paymentInfo.Selected = new bool?(false);
      }
      ((PXSelectBase) this.AvailablePayments).Cache.AllowInsert = false;
      ((PXSelectBase) this.AvailablePayments).Cache.Clear();
      if (flag)
        ((PXSelectBase) this.DepositPayments).View.RequestRefresh();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    if (((PXSelectBase<CADeposit>) this.Document).Current != null)
    {
      string valueExt = (string) ((PXSelectBase<CADeposit>) this.Document).GetValueExt<CADeposit.tranID_CATran_batchNbr>(((PXSelectBase<CADeposit>) this.Document).Current);
      if (valueExt != null)
      {
        JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
        ((PXGraph) instance).Clear();
        PX.Objects.GL.Batch batch = new PX.Objects.GL.Batch();
        ((PXSelectBase<PX.Objects.GL.Batch>) instance.BatchModule).Current = PXResultset<PX.Objects.GL.Batch>.op_Implicit(PXSelectBase<PX.Objects.GL.Batch, PXSelect<PX.Objects.GL.Batch, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleCA>, And<PX.Objects.GL.Batch.batchNbr, Equal<Required<PX.Objects.GL.Batch.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) valueExt
        }));
        throw new PXRedirectRequiredException((PXGraph) instance, "Batch Record");
      }
    }
    return adapter.Get();
  }

  protected virtual IEnumerable Report(PXAdapter adapter, [PXString(8, InputMask = "CC.CC.CC.CC")] string reportID, [PXBool] bool refresh)
  {
    if (!string.IsNullOrEmpty(reportID))
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (CADeposit caDeposit in adapter.Get<CADeposit>())
      {
        dictionary["TranType"] = caDeposit.TranType;
        dictionary["RefNbr"] = caDeposit.RefNbr;
        if (refresh)
          ((PXSelectBase<CADeposit>) this.Document).Search<CADeposit.refNbr>((object) caDeposit.TranType, new object[1]
          {
            (object) caDeposit.RefNbr
          });
      }
      throw new PXReportRequiredException(dictionary, reportID, "Report " + reportID, (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewBAccount()
  {
    CADepositDetail current = ((PXSelectBase<CADepositDetail>) this.DepositPayments).Current;
    foreach (PXResult<CADepositDetail, PaymentInfo, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location> pxResult in ((PXSelectBase<CADepositDetail>) this.DepositPayments).Select(Array.Empty<object>()))
    {
      PaymentInfo paymentInfo = PXResult<CADepositDetail, PaymentInfo, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult);
      if (paymentInfo.DocType == current.OrigDocType && paymentInfo.RefNbr == current.OrigRefNbr)
      {
        PXRedirectHelper.TryRedirect(((PXGraph) this).Caches[typeof (PX.Objects.CR.BAccount)], (object) PX.Objects.CR.BAccount.PK.Find((PXGraph) this, PXResult<CADepositDetail, PaymentInfo, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult).BAccountID), "Business Account", (PXRedirectHelper.WindowMode) 3);
        break;
      }
    }
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<CADepositDetail>) this.DepositPayments).Current != null)
    {
      CADepositDetail current = ((PXSelectBase<CADepositDetail>) this.DepositPayments).Current;
      IDocGraphCreator docGraphCreator = (IDocGraphCreator) null;
      switch (current.OrigModule)
      {
        case "AP":
          docGraphCreator = (IDocGraphCreator) new APDocGraphCreator();
          break;
        case "AR":
          docGraphCreator = (IDocGraphCreator) new ARDocGraphCreator();
          break;
        case "CA":
          docGraphCreator = (IDocGraphCreator) new CADocGraphCreator();
          break;
      }
      if (docGraphCreator != null)
      {
        PXGraph pxGraph = docGraphCreator.Create(current.OrigDocType, current.OrigRefNbr, new int?());
        if (pxGraph != null)
        {
          PXRedirectRequiredException requiredException = new PXRedirectRequiredException(pxGraph, true, nameof (ViewDocument));
          ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException;
        }
      }
      throw new PXException("The system cannot redirect the user to a document of this type.");
    }
    return adapter.Get();
  }

  public CADepositEntry()
  {
    CASetup current = ((PXSelectBase<CASetup>) this.casetup).Current;
    OpenPeriodAttribute.SetValidatePeriod<CADeposit.finPeriodID>(((PXSelectBase) this.Document).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    PXGraph.RowUpdatedEvents rowUpdated = ((PXGraph) this).RowUpdated;
    CADepositEntry caDepositEntry1 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) caDepositEntry1, __vmethodptr(caDepositEntry1, ParentFieldUpdated));
    rowUpdated.AddHandler<CADeposit>(pxRowUpdated);
    ((PXSelectBase) this.AvailablePayments).Cache.AllowDelete = false;
    ((PXSelectBase) this.AvailablePayments).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.AvailablePayments).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARPayment.selected>(((PXSelectBase) this.AvailablePayments).Cache, (object) null, true);
    PXGraph.FieldSelectingEvents fieldSelecting = ((PXGraph) this).FieldSelecting;
    CADepositEntry caDepositEntry2 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) caDepositEntry2, __vmethodptr(caDepositEntry2, CADeposit_TranID_CATran_BatchNbr_FieldSelecting));
    fieldSelecting.AddHandler<CADeposit.tranID_CATran_batchNbr>(pxFieldSelecting);
  }

  public virtual IEnumerable availablePayments()
  {
    CADepositEntry caDepositEntry = this;
    PXCache cache = ((PXGraph) caDepositEntry).Caches[typeof (PaymentInfo)];
    foreach (PaymentInfo availablePayment in caDepositEntry.GetAvailablePayments())
    {
      availablePayment.Selected = ((PaymentInfo) cache.Locate((object) availablePayment) ?? availablePayment).Selected;
      GraphHelper.Hold(cache, (object) availablePayment);
      yield return (object) availablePayment;
    }
  }

  protected virtual IEnumerable GetAvailablePayments()
  {
    CADepositEntry caDepositEntry = this;
    CADepositEntry.PaymentFilter flt = ((PXSelectBase<CADepositEntry.PaymentFilter>) caDepositEntry.filter).Current;
    CADeposit current = ((PXSelectBase<CADeposit>) caDepositEntry.Document).Current;
    if (current != null && current.CashAccountID.HasValue && !current.Released.GetValueOrDefault())
    {
      DateTime? nullable = ((PXSelectBase<CADepositEntry.PaymentFilter>) caDepositEntry.filter).Current.StartDate;
      if (nullable.HasValue)
      {
        nullable = ((PXSelectBase<CADepositEntry.PaymentFilter>) caDepositEntry.filter).Current.EndDate;
        if (nullable.HasValue)
        {
          PXCache cache = ((PXGraph) caDepositEntry).Caches[typeof (PaymentInfo)];
          PXSelectBase<CADepositEntry.ARPaymentUpdate> arPaymentsQuery = caDepositEntry.CreateARPaymentsQuery(flt);
          foreach (object processArPaymentRecord in caDepositEntry.ProcessARPaymentRecords(cache, arPaymentsQuery))
            yield return processArPaymentRecord;
          PXSelectBase<CADepositEntry.APPaymentUpdate> apPaymentsQuery = caDepositEntry.CreateAPPaymentsQuery(flt);
          foreach (object processApPaymentRecord in caDepositEntry.ProcessAPPaymentRecords(cache, apPaymentsQuery))
            yield return processApPaymentRecord;
          PXSelectBase<CADepositEntry.CAAdjUpdate> caPaymentsQuery = caDepositEntry.CreateCAPaymentsQuery(flt);
          foreach (object procesCaPaymentRecord in caDepositEntry.ProcesCAPaymentRecords(cache, caPaymentsQuery))
            yield return procesCaPaymentRecord;
        }
      }
    }
  }

  protected virtual IEnumerable<PaymentInfo> ProcessARPaymentRecords(
    PXCache cache,
    PXSelectBase<CADepositEntry.ARPaymentUpdate> query)
  {
    CADepositEntry.ARPaymentUpdate last = (CADepositEntry.ARPaymentUpdate) null;
    foreach (PXResult<CADepositEntry.ARPaymentUpdate, CADepositDetail, CADeposit, CashAccountDeposit, PaymentMethod> pxResult1 in query.Select(Array.Empty<object>()))
    {
      CADepositEntry.ARPaymentUpdate source = PXResult<CADepositEntry.ARPaymentUpdate, CADepositDetail, CADeposit, CashAccountDeposit, PaymentMethod>.op_Implicit(pxResult1);
      PXResult<CADepositEntry.ARPaymentUpdate, CADepositDetail, CADeposit, CashAccountDeposit, PaymentMethod>.op_Implicit(pxResult1);
      bool? nullable = PXResult<CADepositEntry.ARPaymentUpdate, CADepositDetail, CADeposit, CashAccountDeposit, PaymentMethod>.op_Implicit(pxResult1).ARVoidOnDepositAccount;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = source.Voided;
        if (nullable.GetValueOrDefault() || ARPaymentType.AllVoidingTypes.Contains(source.DocType))
          continue;
      }
      bool flag2 = false;
      if (last == null || !(last.DocType == source.DocType) || !(last.RefNbr == source.RefNbr))
      {
        last = source;
        foreach (PXResult<CADepositDetail> pxResult2 in ((PXSelectBase<CADepositDetail>) this.Details).Select(Array.Empty<object>()))
        {
          CADepositDetail caDepositDetail = PXResult<CADepositDetail>.op_Implicit(pxResult2);
          if (caDepositDetail.OrigDocType == source.DocType && caDepositDetail.OrigRefNbr == source.RefNbr && caDepositDetail.OrigModule == "AR")
          {
            flag2 = true;
            break;
          }
        }
        if (!flag2)
        {
          PaymentInfo paymentInfo = this.Copy(source, new PaymentInfo());
          try
          {
            cache.Insert((object) paymentInfo);
          }
          catch (PXException ex)
          {
            cache.SetStatus((object) paymentInfo, (PXEntryStatus) 2);
            cache.RaiseExceptionHandling<PaymentInfo.refNbr>((object) paymentInfo, (object) paymentInfo.Selected, (Exception) new PXSetPropertyException(((Exception) ex).Message, (PXErrorLevel) 3));
          }
          yield return paymentInfo;
        }
      }
    }
  }

  protected virtual IEnumerable<PaymentInfo> ProcessAPPaymentRecords(
    PXCache cache,
    PXSelectBase<CADepositEntry.APPaymentUpdate> query)
  {
    CADepositEntry.APPaymentUpdate last = (CADepositEntry.APPaymentUpdate) null;
    foreach (PXResult<CADepositEntry.APPaymentUpdate, CADepositDetail, CADeposit, CashAccountDeposit, PaymentMethod> pxResult1 in query.Select(Array.Empty<object>()))
    {
      CADepositEntry.APPaymentUpdate source = PXResult<CADepositEntry.APPaymentUpdate, CADepositDetail, CADeposit, CashAccountDeposit, PaymentMethod>.op_Implicit(pxResult1);
      PXResult<CADepositEntry.APPaymentUpdate, CADepositDetail, CADeposit, CashAccountDeposit, PaymentMethod>.op_Implicit(pxResult1);
      PXResult<CADepositEntry.APPaymentUpdate, CADepositDetail, CADeposit, CashAccountDeposit, PaymentMethod>.op_Implicit(pxResult1);
      if (!(source.DocType != "REF") || !(source.DocType != "VRF") || !(source.DocType != "RQC"))
      {
        bool flag = false;
        if (last == null || !(last.DocType == source.DocType) || !(last.RefNbr == source.RefNbr))
        {
          last = source;
          foreach (PXResult<CADepositDetail> pxResult2 in ((PXSelectBase<CADepositDetail>) this.Details).Select(Array.Empty<object>()))
          {
            CADepositDetail caDepositDetail = PXResult<CADepositDetail>.op_Implicit(pxResult2);
            if (caDepositDetail.OrigDocType == source.DocType && caDepositDetail.OrigRefNbr == source.RefNbr && caDepositDetail.OrigModule == "AP")
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            PaymentInfo paymentInfo = this.Copy(source, new PaymentInfo());
            try
            {
              cache.Insert((object) paymentInfo);
            }
            catch (PXException ex)
            {
              cache.SetStatus((object) paymentInfo, (PXEntryStatus) 2);
              cache.RaiseExceptionHandling<PaymentInfo.refNbr>((object) paymentInfo, (object) paymentInfo.Selected, (Exception) new PXSetPropertyException(((Exception) ex).Message, (PXErrorLevel) 3));
            }
            yield return paymentInfo;
          }
        }
      }
    }
  }

  protected virtual IEnumerable<PaymentInfo> ProcesCAPaymentRecords(
    PXCache cache,
    PXSelectBase<CADepositEntry.CAAdjUpdate> query)
  {
    CADepositEntry.CAAdjUpdate last = (CADepositEntry.CAAdjUpdate) null;
    foreach (PXResult<CADepositEntry.CAAdjUpdate, CADepositDetail, CADeposit, CashAccountDeposit> pxResult1 in query.Select(Array.Empty<object>()))
    {
      CADepositEntry.CAAdjUpdate source = PXResult<CADepositEntry.CAAdjUpdate, CADepositDetail, CADeposit, CashAccountDeposit>.op_Implicit(pxResult1);
      PXResult<CADepositEntry.CAAdjUpdate, CADepositDetail, CADeposit, CashAccountDeposit>.op_Implicit(pxResult1);
      bool flag = false;
      if (last == null || !(last.AdjTranType == source.AdjTranType) || !(last.AdjRefNbr == source.AdjRefNbr))
      {
        last = source;
        foreach (PXResult<CADepositDetail> pxResult2 in ((PXSelectBase<CADepositDetail>) this.Details).Select(Array.Empty<object>()))
        {
          CADepositDetail caDepositDetail = PXResult<CADepositDetail>.op_Implicit(pxResult2);
          if (caDepositDetail.OrigDocType == source.AdjTranType && caDepositDetail.OrigRefNbr == source.AdjRefNbr && caDepositDetail.OrigModule == "CA")
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          PaymentInfo paymentInfo = this.Copy((PX.Objects.CA.Light.CAAdj) source, new PaymentInfo());
          try
          {
            cache.Insert((object) paymentInfo);
          }
          catch (PXException ex)
          {
            cache.SetStatus((object) paymentInfo, (PXEntryStatus) 2);
            cache.RaiseExceptionHandling<PaymentInfo.refNbr>((object) paymentInfo, (object) paymentInfo.Selected, (Exception) new PXSetPropertyException(((Exception) ex).Message, (PXErrorLevel) 3));
          }
          yield return paymentInfo;
        }
      }
    }
  }

  protected virtual PXSelectBase<CADepositEntry.ARPaymentUpdate> CreateARPaymentsQuery(
    CADepositEntry.PaymentFilter filter)
  {
    PXSelectBase<CADepositEntry.ARPaymentUpdate> arPaymentsQuery = (PXSelectBase<CADepositEntry.ARPaymentUpdate>) new PXSelectJoin<CADepositEntry.ARPaymentUpdate, LeftJoin<CADepositDetail, On<CADepositDetail.origDocType, Equal<CADepositEntry.ARPaymentUpdate.docType>, And<CADepositDetail.origRefNbr, Equal<CADepositEntry.ARPaymentUpdate.refNbr>, And<CADepositDetail.origModule, Equal<BatchModule.moduleAR>, And<CADepositDetail.tranType, Equal<CATranType.cADeposit>>>>>, LeftJoin<CADeposit, On<CADeposit.tranType, Equal<CADepositDetail.tranType>, And<CADeposit.refNbr, Equal<CADepositDetail.refNbr>>>, InnerJoin<CashAccountDeposit, On<CashAccountDeposit.depositAcctID, Equal<PX.Objects.CA.Light.ARPayment.cashAccountID>, And<Where<CashAccountDeposit.paymentMethodID, Equal<PX.Objects.CA.Light.ARPayment.paymentMethodID>, Or<CashAccountDeposit.paymentMethodID, Equal<EmptyString>>>>>, InnerJoin<PaymentMethod, On<PaymentMethod.paymentMethodID, Equal<PX.Objects.CA.Light.ARPayment.paymentMethodID>>>>>>, Where<CashAccountDeposit.cashAccountID, Equal<Current<CADeposit.cashAccountID>>, And<PX.Objects.CA.Light.ARPayment.released, Equal<boolTrue>, And<PX.Objects.CA.Light.ARPayment.curyOrigDocAmt, NotEqual<Zero>, And<PX.Objects.CA.Light.ARPayment.depositAsBatch, Equal<boolTrue>, And<CADepositEntry.ARPaymentUpdate.depositNbr, IsNull, And<Where<CADepositDetail.refNbr, IsNull, Or<CADeposit.voided, Equal<boolTrue>>>>>>>>>, OrderBy<Asc<CADepositEntry.ARPaymentUpdate.docType, Asc<CADepositEntry.ARPaymentUpdate.refNbr, Desc<CashAccountDeposit.paymentMethodID>>>>>((PXGraph) this);
    if (filter.CashAccountID.HasValue)
      arPaymentsQuery.WhereAnd<Where<PX.Objects.CA.Light.ARPayment.cashAccountID, Equal<Current<CADepositEntry.PaymentFilter.cashAccountID>>>>();
    if (!string.IsNullOrEmpty(filter.PaymentMethodID))
      arPaymentsQuery.WhereAnd<Where<PX.Objects.CA.Light.ARPayment.paymentMethodID, Equal<Current<CADepositEntry.PaymentFilter.paymentMethodID>>>>();
    if (filter.EndDate.HasValue)
      arPaymentsQuery.WhereAnd<Where<PX.Objects.CA.Light.ARPayment.depositAfter, LessEqual<Current<CADepositEntry.PaymentFilter.endDate>>>>();
    if (filter.StartDate.HasValue)
      arPaymentsQuery.WhereAnd<Where<PX.Objects.CA.Light.ARPayment.depositAfter, GreaterEqual<Current<CADepositEntry.PaymentFilter.startDate>>>>();
    return arPaymentsQuery;
  }

  protected virtual PXSelectBase<CADepositEntry.APPaymentUpdate> CreateAPPaymentsQuery(
    CADepositEntry.PaymentFilter filter)
  {
    PXSelectBase<CADepositEntry.APPaymentUpdate> apPaymentsQuery = (PXSelectBase<CADepositEntry.APPaymentUpdate>) new PXSelectJoin<CADepositEntry.APPaymentUpdate, LeftJoin<CADepositDetail, On<CADepositDetail.origDocType, Equal<CADepositEntry.APPaymentUpdate.docType>, And<CADepositDetail.origRefNbr, Equal<CADepositEntry.APPaymentUpdate.refNbr>, And<CADepositDetail.origModule, Equal<BatchModule.moduleAP>, And<CADepositDetail.tranType, Equal<CATranType.cADeposit>>>>>, LeftJoin<CADeposit, On<CADeposit.tranType, Equal<CADepositDetail.tranType>, And<CADeposit.refNbr, Equal<CADepositDetail.refNbr>>>, InnerJoin<CashAccountDeposit, On<CashAccountDeposit.depositAcctID, Equal<PX.Objects.CA.Light.APPayment.cashAccountID>, And<Where<CashAccountDeposit.paymentMethodID, Equal<PX.Objects.CA.Light.APPayment.paymentMethodID>, Or<CashAccountDeposit.paymentMethodID, Equal<EmptyString>>>>>, InnerJoin<PaymentMethod, On<PaymentMethod.paymentMethodID, Equal<PX.Objects.CA.Light.APPayment.paymentMethodID>>>>>>, Where<CashAccountDeposit.cashAccountID, Equal<Current<CADeposit.cashAccountID>>, And<CADepositEntry.APPaymentUpdate.docType, In3<APDocType.refund, APDocType.voidRefund, APDocType.cashReturn>, And<PX.Objects.CA.Light.APPayment.released, Equal<boolTrue>, And<CADepositEntry.APPaymentUpdate.docType, In3<APDocType.refund, APDocType.voidRefund, APDocType.cashReturn>, And<PX.Objects.CA.Light.APPayment.curyOrigDocAmt, NotEqual<Zero>, And<PX.Objects.CA.Light.APPayment.depositAsBatch, Equal<boolTrue>, And<CADepositEntry.APPaymentUpdate.depositNbr, IsNull, And<Where<CADepositDetail.refNbr, IsNull, Or<CADeposit.voided, Equal<boolTrue>>>>>>>>>>>, OrderBy<Asc<CADepositEntry.APPaymentUpdate.docType, Asc<CADepositEntry.APPaymentUpdate.refNbr, Desc<CashAccountDeposit.paymentMethodID>>>>>((PXGraph) this);
    if (filter.CashAccountID.HasValue)
      apPaymentsQuery.WhereAnd<Where<PX.Objects.CA.Light.APPayment.cashAccountID, Equal<Current<CADepositEntry.PaymentFilter.cashAccountID>>>>();
    if (!string.IsNullOrEmpty(filter.PaymentMethodID))
      apPaymentsQuery.WhereAnd<Where<PX.Objects.CA.Light.APPayment.paymentMethodID, Equal<Current<CADepositEntry.PaymentFilter.paymentMethodID>>>>();
    if (filter.EndDate.HasValue)
      apPaymentsQuery.WhereAnd<Where<PX.Objects.CA.Light.APPayment.depositAfter, LessEqual<Current<CADepositEntry.PaymentFilter.endDate>>>>();
    if (filter.StartDate.HasValue)
      apPaymentsQuery.WhereAnd<Where<PX.Objects.CA.Light.APPayment.depositAfter, GreaterEqual<Current<CADepositEntry.PaymentFilter.startDate>>>>();
    return apPaymentsQuery;
  }

  protected virtual PXSelectBase<CADepositEntry.CAAdjUpdate> CreateCAPaymentsQuery(
    CADepositEntry.PaymentFilter filter)
  {
    PXSelectBase<CADepositEntry.CAAdjUpdate> caPaymentsQuery = (PXSelectBase<CADepositEntry.CAAdjUpdate>) new PXSelectJoin<CADepositEntry.CAAdjUpdate, LeftJoin<CADepositDetail, On<CADepositDetail.origDocType, Equal<CADepositEntry.CAAdjUpdate.adjTranType>, And<CADepositDetail.origRefNbr, Equal<CADepositEntry.CAAdjUpdate.adjRefNbr>, And<CADepositDetail.origModule, Equal<BatchModule.moduleCA>, And<CADepositDetail.tranType, Equal<CATranType.cADeposit>>>>>, LeftJoin<CADeposit, On<CADeposit.tranType, Equal<CADepositDetail.tranType>, And<CADeposit.refNbr, Equal<CADepositDetail.refNbr>>>, InnerJoin<CashAccountDeposit, On<CashAccountDeposit.depositAcctID, Equal<PX.Objects.CA.Light.CAAdj.cashAccountID>>>>>, Where<CashAccountDeposit.cashAccountID, Equal<Current<CADeposit.cashAccountID>>, And<CADepositEntry.CAAdjUpdate.adjTranType, Equal<CATranType.cAAdjustment>, And<PX.Objects.CA.Light.CAAdj.released, Equal<boolTrue>, And<PX.Objects.CA.Light.CAAdj.curyTranAmt, NotEqual<Zero>, And<PX.Objects.CA.Light.CAAdj.depositAsBatch, Equal<boolTrue>, And<CADepositEntry.CAAdjUpdate.depositNbr, IsNull, And<Where<CADepositDetail.refNbr, IsNull, Or<CADeposit.voided, Equal<boolTrue>>>>>>>>>>, OrderBy<Asc<CAAdj.adjRefNbr, Desc<CashAccountDeposit.paymentMethodID>>>>((PXGraph) this);
    if (filter.CashAccountID.HasValue)
      caPaymentsQuery.WhereAnd<Where<PX.Objects.CA.Light.CAAdj.cashAccountID, Equal<Current<CADepositEntry.PaymentFilter.cashAccountID>>>>();
    if (filter.EndDate.HasValue)
      caPaymentsQuery.WhereAnd<Where<PX.Objects.CA.Light.CAAdj.depositAfter, LessEqual<Current<CADepositEntry.PaymentFilter.endDate>>>>();
    if (filter.StartDate.HasValue)
      caPaymentsQuery.WhereAnd<Where<PX.Objects.CA.Light.CAAdj.depositAfter, GreaterEqual<Current<CADepositEntry.PaymentFilter.startDate>>>>();
    return caPaymentsQuery;
  }

  public virtual IEnumerable depositPayments()
  {
    CADepositEntry caDepositEntry = this;
    foreach (PXResult<CADepositDetail, PX.Objects.CA.Light.ARPayment, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location> pxResult in PXSelectBase<CADepositDetail, PXSelectJoin<CADepositDetail, LeftJoin<PX.Objects.CA.Light.ARPayment, On<CADepositDetail.origDocType, Equal<PX.Objects.CA.Light.ARPayment.docType>, And<CADepositDetail.origRefNbr, Equal<PX.Objects.CA.Light.ARPayment.refNbr>, And<CADepositDetail.origModule, Equal<BatchModule.moduleAR>>>>, InnerJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.Light.ARPayment.customerID>>, InnerJoin<PX.Objects.CA.Light.Location, On<PX.Objects.CA.Light.Location.locationID, Equal<PX.Objects.CA.Light.ARPayment.customerLocationID>>>>>, Where<CADepositDetail.refNbr, Equal<Current<CADeposit.refNbr>>, And<CADepositDetail.origModule, Equal<BatchModule.moduleAR>, And<CADepositDetail.tranType, Equal<Current<CADeposit.tranType>>>>>>.Config>.Select((PXGraph) caDepositEntry, Array.Empty<object>()))
    {
      CADepositDetail caDepositDetail = PXResult<CADepositDetail, PX.Objects.CA.Light.ARPayment, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult);
      PX.Objects.CA.Light.ARPayment source = PXResult<CADepositDetail, PX.Objects.CA.Light.ARPayment, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult);
      PaymentInfo destination = new PaymentInfo();
      PX.Objects.CA.Light.BAccount bAccount = PXResult<CADepositDetail, PX.Objects.CA.Light.ARPayment, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult);
      PX.Objects.CA.Light.Location location = PXResult<CADepositDetail, PX.Objects.CA.Light.ARPayment, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(source.RefNbr))
        destination = caDepositEntry.Copy(source, bAccount, location, destination);
      yield return (object) new PXResult<CADepositDetail, PaymentInfo, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>(caDepositDetail, destination, bAccount, location);
    }
    foreach (PXResult<CADepositDetail, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location> pxResult in PXSelectBase<CADepositDetail, PXSelectJoin<CADepositDetail, LeftJoin<PX.Objects.CA.Light.APPayment, On<CADepositDetail.origDocType, Equal<PX.Objects.CA.Light.APPayment.docType>, And<CADepositDetail.origRefNbr, Equal<PX.Objects.CA.Light.APPayment.refNbr>, And<CADepositDetail.origModule, Equal<BatchModule.moduleAP>>>>, InnerJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.Light.APPayment.vendorID>>, InnerJoin<PX.Objects.CA.Light.Location, On<PX.Objects.CA.Light.Location.locationID, Equal<PX.Objects.CA.Light.APPayment.vendorLocationID>>>>>, Where<CADepositDetail.refNbr, Equal<Current<CADeposit.refNbr>>, And<CADepositDetail.origModule, Equal<BatchModule.moduleAP>, And<CADepositDetail.tranType, Equal<Current<CADeposit.tranType>>>>>>.Config>.Select((PXGraph) caDepositEntry, Array.Empty<object>()))
    {
      CADepositDetail caDepositDetail = PXResult<CADepositDetail, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult);
      PX.Objects.CA.Light.APPayment source = PXResult<CADepositDetail, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult);
      PaymentInfo destination = new PaymentInfo();
      PX.Objects.CA.Light.BAccount bAccount = PXResult<CADepositDetail, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult);
      PX.Objects.CA.Light.Location location = PXResult<CADepositDetail, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(source.RefNbr))
        destination = caDepositEntry.Copy(source, bAccount, location, destination);
      yield return (object) new PXResult<CADepositDetail, PaymentInfo, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>(caDepositDetail, destination, bAccount, location);
    }
    foreach (PXResult<CADepositDetail, PX.Objects.CA.Light.CAAdj> pxResult in PXSelectBase<CADepositDetail, PXSelectJoin<CADepositDetail, LeftJoin<PX.Objects.CA.Light.CAAdj, On<CADepositDetail.origDocType, Equal<PX.Objects.CA.Light.CAAdj.adjTranType>, And<CADepositDetail.origRefNbr, Equal<PX.Objects.CA.Light.CAAdj.adjRefNbr>, And<CADepositDetail.origModule, Equal<BatchModule.moduleCA>>>>>, Where<CADepositDetail.refNbr, Equal<Current<CADeposit.refNbr>>, And<CADepositDetail.origModule, Equal<BatchModule.moduleCA>, And<CADepositDetail.tranType, Equal<Current<CADeposit.tranType>>>>>>.Config>.Select((PXGraph) caDepositEntry, Array.Empty<object>()))
    {
      CADepositDetail caDepositDetail = PXResult<CADepositDetail, PX.Objects.CA.Light.CAAdj>.op_Implicit(pxResult);
      PX.Objects.CA.Light.CAAdj source = PXResult<CADepositDetail, PX.Objects.CA.Light.CAAdj>.op_Implicit(pxResult);
      PaymentInfo destination = new PaymentInfo();
      if (!string.IsNullOrEmpty(source.AdjRefNbr))
        destination = caDepositEntry.Copy(source, destination);
      yield return (object) new PXResult<CADepositDetail, PaymentInfo, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>(caDepositDetail, destination, (PX.Objects.CA.Light.BAccount) null, (PX.Objects.CA.Light.Location) null);
    }
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
    if (viewName == "_CADeposit_CurrencyInfo_" && !((PXGraph) this).Views[viewName].IsReadOnly)
    {
      CADeposit current = ((PXSelectBase<CADeposit>) this.Document).Current;
      if (current != null && current.TranType == "CVD")
        ((PXGraph) this).Views[viewName].IsReadOnly = true;
    }
    return ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Number", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleCA>>>))]
  public virtual void CATran_BatchNbr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void CADeposit_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CADeposit row))
      return;
    bool flag1 = row.DocType == "CVD";
    bool valueOrDefault = row.Released.GetValueOrDefault();
    if (!this._IsVoidCheckInProgress)
    {
      ((PXSelectBase) this.Details).Cache.AllowInsert = false;
      ((PXSelectBase) this.Details).Cache.AllowUpdate = false;
      ((PXSelectBase) this.Details).Cache.AllowDelete = !valueOrDefault && !flag1;
      ((PXSelectBase) this.Charges).Cache.AllowInsert = !valueOrDefault && !flag1;
      ((PXSelectBase) this.Charges).Cache.AllowUpdate = !valueOrDefault && !flag1;
      ((PXSelectBase) this.Charges).Cache.AllowDelete = !valueOrDefault && !flag1;
    }
    PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CADeposit.refNbr>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<CADeposit.tranType>(sender, (object) row, true);
    PXUIFieldAttribute.SetRequired<CADeposit.extRefNbr>(sender, ((PXSelectBase<CASetup>) this.casetup).Current.RequireExtRefNbr.GetValueOrDefault());
    sender.AllowUpdate = !valueOrDefault;
    sender.AllowDelete = !valueOrDefault;
    CashAccount cashAccount = (CashAccount) PXSelectorAttribute.Select<CADeposit.cashAccountID>(sender, (object) row);
    bool? nullable = ((PXSelectBase<CASetup>) this.casetup).Current.RequireControlTotal;
    bool flag2 = nullable.Value;
    nullable = row.Released;
    int num1;
    if (!nullable.GetValueOrDefault() && cashAccount != null)
    {
      nullable = cashAccount.Reconcile;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag3 = num1 != 0;
    bool hasValue = row.ExtraCashAccountID.HasValue;
    if (!valueOrDefault)
    {
      if (!flag1)
      {
        PXUIFieldAttribute.SetEnabled<CADeposit.hold>(sender, (object) row, !valueOrDefault);
        bool flag4 = ((PXSelectBase<CADepositDetail>) this.Details).Any<CADepositDetail>();
        PXUIFieldAttribute.SetEnabled<CADeposit.cashAccountID>(sender, (object) row, !(row.CashAccountID.HasValue & flag4));
        PXUIFieldAttribute.SetEnabled<CADeposit.extRefNbr>(sender, (object) row);
        PXUIFieldAttribute.SetEnabled<CADeposit.tranDate>(sender, (object) row, !valueOrDefault);
        PXUIFieldAttribute.SetEnabled<CADeposit.finPeriodID>(sender, (object) row);
        PXUIFieldAttribute.SetEnabled<CADeposit.tranDesc>(sender, (object) row);
        PXUIFieldAttribute.SetEnabled<CADeposit.curyControlAmt>(sender, (object) row, flag2);
        PXUIFieldAttribute.SetEnabled<CADeposit.cleared>(sender, (object) row, flag3);
        PXCache pxCache = sender;
        CADeposit caDeposit = row;
        int num2;
        if (flag3)
        {
          nullable = row.Cleared;
          num2 = nullable.GetValueOrDefault() ? 1 : 0;
        }
        else
          num2 = 0;
        PXUIFieldAttribute.SetEnabled<CADeposit.clearDate>(pxCache, (object) caDeposit, num2 != 0);
        PXUIFieldAttribute.SetEnabled<CADeposit.chargesSeparate>(sender, (object) row);
        PXUIFieldAttribute.SetEnabled<CADeposit.extraCashAccountID>(sender, (object) row);
        PXUIFieldAttribute.SetEnabled<CADeposit.curyExtraCashTotal>(sender, (object) row, hasValue);
      }
      else
      {
        PXUIFieldAttribute.SetEnabled<CADeposit.hold>(sender, (object) row, !valueOrDefault);
        PXUIFieldAttribute.SetEnabled<CADeposit.tranDesc>(sender, (object) row, !valueOrDefault);
        PXUIFieldAttribute.SetEnabled<CADeposit.curyControlAmt>(sender, (object) row, !valueOrDefault);
      }
    }
    PXUIFieldAttribute.SetVisible<CADeposit.curyControlAmt>(sender, (object) null, flag2);
    PXUIFieldAttribute.SetRequired<CADeposit.curyControlAmt>(sender, flag2);
  }

  protected virtual void CADeposit_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CADeposit row = (CADeposit) e.Row;
    PXDefaultAttribute.SetPersistingCheck<CADeposit.extRefNbr>(sender, (object) row, ((PXSelectBase<CASetup>) this.casetup).Current.RequireExtRefNbr.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    if ((row != null ? (!row.CashAccountID.HasValue ? 1 : 0) : 1) != 0)
      throw new PXRowPersistingException(typeof (CADeposit.cashAccountID).Name, (object) null, "'{0}' cannot be empty.");
    if ((row != null ? (!row.TranDate.HasValue ? 1 : 0) : 1) != 0)
      throw new PXRowPersistingException(typeof (CADeposit.tranDate).Name, (object) null, "'{0}' cannot be empty.");
  }

  protected virtual void CADeposit_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is CADeposit row) || ((PXSelectBase<CADepositEntry.PaymentFilter>) this.filter).Current == null)
      return;
    ((PXSelectBase) this.filter).Cache.SetValue<CADepositEntry.PaymentFilter.endDate>((object) ((PXSelectBase<CADepositEntry.PaymentFilter>) this.filter).Current.EndDate, (object) row.TranDate);
  }

  protected virtual void CADeposit_TranDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CADeposit))
      return;
    ((PXSelectBase) this.filter).Cache.SetDefaultExt<CADepositEntry.PaymentFilter.startDate>((object) ((PXSelectBase<CADepositEntry.PaymentFilter>) this.filter).Current);
    ((PXSelectBase) this.filter).Cache.SetDefaultExt<CADepositEntry.PaymentFilter.endDate>((object) ((PXSelectBase<CADepositEntry.PaymentFilter>) this.filter).Current);
  }

  protected virtual void CADeposit_CashAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CADeposit row = (CADeposit) e.Row;
    row.Cleared = new bool?(false);
    row.ClearDate = new DateTime?();
    if (((PXSelectBase<CashAccount>) this.cashAccount).Current != null)
    {
      int? cashAccountId1 = ((PXSelectBase<CashAccount>) this.cashAccount).Current.CashAccountID;
      int? cashAccountId2 = row.CashAccountID;
      if (cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue)
        goto label_3;
    }
    ((PXSelectBase<CashAccount>) this.cashAccount).Current = (CashAccount) PXSelectorAttribute.Select<CADeposit.cashAccountID>(sender, (object) row);
    ((PXSelectBase) this.AvailablePayments).Cache.Clear();
    ((PXSelectBase) this.paymentUpdate).Cache.Clear();
    ((PXSelectBase) this.paymentUpdateAP).Cache.Clear();
    ((PXSelectBase) this.paymentUpdateCA).Cache.Clear();
label_3:
    if (((PXSelectBase<CashAccount>) this.cashAccount).Current == null || ((PXSelectBase<CashAccount>) this.cashAccount).Current.Reconcile.GetValueOrDefault())
      return;
    row.Cleared = new bool?(true);
    row.ClearDate = row.TranDate;
  }

  protected virtual void CADeposit_ExtraCashAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CADeposit row = (CADeposit) e.Row;
    if (row.ExtraCashAccountID.HasValue)
      return;
    sender.SetDefaultExt<CADeposit.curyExtraCashTotal>((object) row);
  }

  protected virtual void CADeposit_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CADeposit row = (CADeposit) e.Row;
    bool? released = row.Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue))
      return;
    bool? requireControlTotal = ((PXSelectBase<CASetup>) this.casetup).Current.RequireControlTotal;
    Decimal? nullable1;
    Decimal? nullable2;
    if ((requireControlTotal.HasValue ? new bool?(!requireControlTotal.GetValueOrDefault()) : new bool?()).GetValueOrDefault())
    {
      nullable1 = row.CuryControlAmt;
      nullable2 = row.CuryTranAmt;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      {
        nullable2 = row.CuryTranAmt;
        if (nullable2.HasValue)
        {
          nullable2 = row.CuryTranAmt;
          Decimal num = 0M;
          if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
          {
            sender.SetValueExt<CADeposit.curyControlAmt>(e.Row, (object) row.CuryTranAmt);
            goto label_7;
          }
        }
        sender.SetValueExt<CADeposit.curyControlAmt>(e.Row, (object) 0M);
      }
    }
label_7:
    if (row.Hold.Value)
      return;
    nullable2 = row.CuryControlAmt;
    nullable1 = row.CuryTranAmt;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      sender.RaiseExceptionHandling<CADeposit.curyControlAmt>(e.Row, (object) row.CuryControlAmt, (Exception) new PXSetPropertyException("The document is out of the balance."));
    else
      sender.RaiseExceptionHandling<CADeposit.curyControlAmt>(e.Row, (object) row.CuryControlAmt, (Exception) null);
  }

  protected virtual void CADeposit_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    CADeposit row = (CADeposit) e.Row;
    if (row == null || !(row.TranType == "CDT"))
      return;
    CCBatch ccBatch = PXResultset<CCBatch>.op_Implicit(PXSelectBase<CCBatch, PXSelect<CCBatch, Where<CCBatch.depositType, Equal<Required<CCBatch.depositType>>, And<CCBatch.depositNbr, Equal<Required<CCBatch.depositNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.TranType,
      (object) row.RefNbr
    }));
    if (ccBatch == null || !ccBatch.BatchID.HasValue)
      return;
    ccBatch.DepositType = (string) null;
    ccBatch.DepositNbr = (string) null;
    ccBatch.Status = "PRD";
    ((PXSelectBase<CCBatch>) this.ccBatch).Update(ccBatch);
  }

  protected virtual void CADeposit_ChargesSeparate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CADeposit row = (CADeposit) e.Row;
    sender.RaiseFieldUpdated<CADeposit.chargeMult>(e.Row, (object) -1M);
  }

  protected virtual void CADeposit_RefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsVoidCheckInProgress)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CADeposit_FinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsVoidCheckInProgress)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CADeposit_TranID_CATran_BatchNbr_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (e.Row != null && !e.IsAltered)
      return;
    string str = (string) null;
    if (sender.Graph.Caches[typeof (CATran)].GetStateExt<CATran.batchNbr>((object) null) is PXFieldState stateExt)
      str = stateExt.ViewName;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, new bool?(false), new bool?(false), new int?(0), new int?(0), new int?(), (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(false), new bool?(true), new bool?(true), (PXUIVisibility) 3, str, (string[]) null, (string[]) null);
  }

  protected virtual void CADepositDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    CADepositDetail row = (CADepositDetail) e.Row;
    if (row.DetailType == "CHD" && row.OrigModule == "AR" && !string.IsNullOrEmpty(row.OrigDocType) && !string.IsNullOrEmpty(row.OrigRefNbr))
    {
      CADepositEntry.ARPaymentUpdate arPaymentUpdate = PXResultset<CADepositEntry.ARPaymentUpdate>.op_Implicit(((PXSelectBase<CADepositEntry.ARPaymentUpdate>) this.paymentUpdate).Select(new object[2]
      {
        (object) row.OrigDocType,
        (object) row.OrigRefNbr
      }));
      if (arPaymentUpdate != null)
      {
        arPaymentUpdate.ClearDepositReference();
        ((PXSelectBase) this.paymentUpdate).Cache.Update((object) arPaymentUpdate);
      }
    }
    if (row.DetailType == "CHD" && row.OrigModule == "AP" && !string.IsNullOrEmpty(row.OrigDocType) && !string.IsNullOrEmpty(row.OrigRefNbr))
    {
      CADepositEntry.APPaymentUpdate apPaymentUpdate = PXResultset<CADepositEntry.APPaymentUpdate>.op_Implicit(((PXSelectBase<CADepositEntry.APPaymentUpdate>) this.paymentUpdateAP).Select(new object[2]
      {
        (object) row.OrigDocType,
        (object) row.OrigRefNbr
      }));
      if (apPaymentUpdate != null)
      {
        apPaymentUpdate.ClearDepositReference();
        ((PXSelectBase) this.paymentUpdateAP).Cache.Update((object) apPaymentUpdate);
      }
    }
    if (row.DetailType == "CHD" && row.OrigModule == "CA" && !string.IsNullOrEmpty(row.OrigDocType) && !string.IsNullOrEmpty(row.OrigRefNbr))
    {
      CADepositEntry.CAAdjUpdate caAdjUpdate = PXResultset<CADepositEntry.CAAdjUpdate>.op_Implicit(((PXSelectBase<CADepositEntry.CAAdjUpdate>) this.paymentUpdateCA).Select(new object[2]
      {
        (object) row.OrigDocType,
        (object) row.OrigRefNbr
      }));
      if (caAdjUpdate != null)
      {
        caAdjUpdate.ClearDepositReference();
        ((PXSelectBase) this.paymentUpdateCA).Cache.Update((object) caAdjUpdate);
      }
    }
    if (this._IsVoidCheckInProgress || this._isMassDelete || string.IsNullOrEmpty(row.ChargeEntryTypeID))
      return;
    CADepositCharge caDepositCharge1 = (CADepositCharge) null;
    foreach (PXResult<CADepositCharge> pxResult in ((PXSelectBase<CADepositCharge>) this.Charges).Select(Array.Empty<object>()))
    {
      CADepositCharge caDepositCharge2 = PXResult<CADepositCharge>.op_Implicit(pxResult);
      if (row.ChargeEntryTypeID == caDepositCharge2.EntryTypeID)
      {
        int? cashAccountId = row.CashAccountID;
        int? depositAcctId = caDepositCharge2.DepositAcctID;
        if (cashAccountId.GetValueOrDefault() == depositAcctId.GetValueOrDefault() & cashAccountId.HasValue == depositAcctId.HasValue && row.PaymentMethodID == caDepositCharge2.PaymentMethodID)
        {
          caDepositCharge1 = caDepositCharge2;
          break;
        }
      }
    }
    if (caDepositCharge1 == null)
      return;
    CADepositCharge copy = (CADepositCharge) ((PXSelectBase) this.Charges).Cache.CreateCopy((object) caDepositCharge1);
    CADepositCharge caDepositCharge3 = copy;
    Decimal? curyChargeableAmt1 = caDepositCharge3.CuryChargeableAmt;
    Decimal? curyTranAmt = row.CuryTranAmt;
    caDepositCharge3.CuryChargeableAmt = curyChargeableAmt1.HasValue & curyTranAmt.HasValue ? new Decimal?(curyChargeableAmt1.GetValueOrDefault() - curyTranAmt.GetValueOrDefault()) : new Decimal?();
    Decimal? curyChargeableAmt2 = copy.CuryChargeableAmt;
    Decimal num = 0M;
    if (curyChargeableAmt2.GetValueOrDefault() == num & curyChargeableAmt2.HasValue)
      ((PXSelectBase<CADepositCharge>) this.Charges).Delete(caDepositCharge1);
    else
      ((PXSelectBase<CADepositCharge>) this.Charges).Update(copy);
  }

  protected virtual void CADepositDetail_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CADepositDetail row = (CADepositDetail) e.Row;
    if (string.IsNullOrEmpty(row.OrigDocType) || string.IsNullOrEmpty(row.OrigRefNbr) || !(row.DetailType == "CHD") || (e.Operation & 3) != 2)
      return;
    if (row.OrigModule == "AR")
    {
      CADepositEntry.ARPaymentUpdate arPaymentUpdate = PXResultset<CADepositEntry.ARPaymentUpdate>.op_Implicit(((PXSelectBase<CADepositEntry.ARPaymentUpdate>) this.paymentUpdate).Select(new object[2]
      {
        (object) row.OrigDocType,
        (object) row.OrigRefNbr
      }));
      CADeposit current = ((PXSelectBase<CADeposit>) this.Document).Current;
      if (arPaymentUpdate != null)
      {
        arPaymentUpdate.SetReferenceTo(current);
        ((PXSelectBase) this.paymentUpdate).Cache.Update((object) arPaymentUpdate);
      }
    }
    if (row.OrigModule == "AP")
    {
      CADepositEntry.APPaymentUpdate apPaymentUpdate = PXResultset<CADepositEntry.APPaymentUpdate>.op_Implicit(((PXSelectBase<CADepositEntry.APPaymentUpdate>) this.paymentUpdateAP).Select(new object[2]
      {
        (object) row.OrigDocType,
        (object) row.OrigRefNbr
      }));
      CADeposit current = ((PXSelectBase<CADeposit>) this.Document).Current;
      if (apPaymentUpdate != null)
      {
        apPaymentUpdate.SetReferenceTo(current);
        ((PXSelectBase) this.paymentUpdateAP).Cache.Update((object) apPaymentUpdate);
      }
    }
    if (!(row.OrigModule == "CA"))
      return;
    CADepositEntry.CAAdjUpdate caAdjUpdate = PXResultset<CADepositEntry.CAAdjUpdate>.op_Implicit(((PXSelectBase<CADepositEntry.CAAdjUpdate>) this.paymentUpdateCA).Select(new object[2]
    {
      (object) row.OrigDocType,
      (object) row.OrigRefNbr
    }));
    CADeposit current1 = ((PXSelectBase<CADeposit>) this.Document).Current;
    if (caAdjUpdate == null)
      return;
    caAdjUpdate.SetReferenceTo(current1);
    ((PXSelectBase) this.paymentUpdateCA).Cache.Update((object) caAdjUpdate);
  }

  protected virtual void CADepositDetail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    CADepositDetail row = (CADepositDetail) e.Row;
    if (this._IsVoidCheckInProgress || string.IsNullOrEmpty(row.ChargeEntryTypeID))
      return;
    CADepositCharge caDepositCharge1 = (CADepositCharge) null;
    foreach (PXResult<CADepositCharge> pxResult in ((PXSelectBase<CADepositCharge>) this.Charges).Select(Array.Empty<object>()))
    {
      CADepositCharge caDepositCharge2 = PXResult<CADepositCharge>.op_Implicit(pxResult);
      if (row.ChargeEntryTypeID == caDepositCharge2.EntryTypeID)
      {
        int? cashAccountId = row.CashAccountID;
        int? depositAcctId = caDepositCharge2.DepositAcctID;
        if (cashAccountId.GetValueOrDefault() == depositAcctId.GetValueOrDefault() & cashAccountId.HasValue == depositAcctId.HasValue && row.PaymentMethodID == caDepositCharge2.PaymentMethodID)
        {
          caDepositCharge1 = caDepositCharge2;
          break;
        }
      }
    }
    CADepositCharge caDepositCharge3;
    if (caDepositCharge1 == null)
    {
      caDepositCharge3 = ((PXSelectBase<CADepositCharge>) this.Charges).Insert(new CADepositCharge()
      {
        EntryTypeID = row.ChargeEntryTypeID,
        DepositAcctID = row.CashAccountID,
        PaymentMethodID = row.PaymentMethodID,
        CuryChargeableAmt = row.CuryTranAmt
      });
    }
    else
    {
      CADepositCharge copy = (CADepositCharge) ((PXSelectBase) this.Charges).Cache.CreateCopy((object) caDepositCharge1);
      CADepositCharge caDepositCharge4 = copy;
      Decimal? curyChargeableAmt = caDepositCharge4.CuryChargeableAmt;
      Decimal? curyTranAmt = row.CuryTranAmt;
      caDepositCharge4.CuryChargeableAmt = curyChargeableAmt.HasValue & curyTranAmt.HasValue ? new Decimal?(curyChargeableAmt.GetValueOrDefault() + curyTranAmt.GetValueOrDefault()) : new Decimal?();
      caDepositCharge3 = ((PXSelectBase<CADepositCharge>) this.Charges).Update(copy);
    }
  }

  protected virtual void CADepositDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CADepositDetail row = (CADepositDetail) e.Row;
    CADepositDetail oldRow = (CADepositDetail) e.OldRow;
    if (this._IsVoidCheckInProgress || !(row.ChargeEntryTypeID != oldRow.ChargeEntryTypeID))
      return;
    CADepositCharge caDepositCharge1 = (CADepositCharge) null;
    CADepositCharge caDepositCharge2 = (CADepositCharge) null;
    foreach (PXResult<CADepositCharge> pxResult in ((PXSelectBase<CADepositCharge>) this.Charges).Select(Array.Empty<object>()))
    {
      CADepositCharge caDepositCharge3 = PXResult<CADepositCharge>.op_Implicit(pxResult);
      if (row.ChargeEntryTypeID == caDepositCharge3.EntryTypeID)
      {
        int? cashAccountId = row.CashAccountID;
        int? depositAcctId = caDepositCharge3.DepositAcctID;
        if (cashAccountId.GetValueOrDefault() == depositAcctId.GetValueOrDefault() & cashAccountId.HasValue == depositAcctId.HasValue && row.PaymentMethodID == caDepositCharge3.PaymentMethodID)
          caDepositCharge1 = caDepositCharge3;
      }
      if (oldRow.ChargeEntryTypeID == caDepositCharge3.EntryTypeID)
      {
        int? cashAccountId = oldRow.CashAccountID;
        int? depositAcctId = caDepositCharge3.DepositAcctID;
        if (cashAccountId.GetValueOrDefault() == depositAcctId.GetValueOrDefault() & cashAccountId.HasValue == depositAcctId.HasValue && oldRow.PaymentMethodID == caDepositCharge3.PaymentMethodID)
          caDepositCharge2 = caDepositCharge3;
      }
    }
    CADepositCharge caDepositCharge4;
    Decimal? nullable;
    if (caDepositCharge1 == null)
    {
      caDepositCharge4 = ((PXSelectBase<CADepositCharge>) this.Charges).Insert(new CADepositCharge()
      {
        EntryTypeID = row.ChargeEntryTypeID,
        DepositAcctID = row.CashAccountID,
        PaymentMethodID = row.PaymentMethodID,
        CuryChargeableAmt = row.CuryTranAmt
      });
    }
    else
    {
      CADepositCharge caDepositCharge5 = caDepositCharge1;
      nullable = caDepositCharge5.CuryChargeableAmt;
      Decimal? curyTranAmt = row.CuryTranAmt;
      caDepositCharge5.CuryChargeableAmt = nullable.HasValue & curyTranAmt.HasValue ? new Decimal?(nullable.GetValueOrDefault() + curyTranAmt.GetValueOrDefault()) : new Decimal?();
      caDepositCharge4 = ((PXSelectBase<CADepositCharge>) this.Charges).Update(caDepositCharge1);
    }
    if (caDepositCharge2 == null)
      return;
    CADepositCharge caDepositCharge6 = caDepositCharge2;
    Decimal? curyChargeableAmt = caDepositCharge6.CuryChargeableAmt;
    nullable = row.CuryTranAmt;
    caDepositCharge6.CuryChargeableAmt = curyChargeableAmt.HasValue & nullable.HasValue ? new Decimal?(curyChargeableAmt.GetValueOrDefault() - nullable.GetValueOrDefault()) : new Decimal?();
    nullable = caDepositCharge2.CuryChargeableAmt;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      ((PXSelectBase<CADepositCharge>) this.Charges).Delete(caDepositCharge2);
    else
      ((PXSelectBase<CADepositCharge>) this.Charges).Update(caDepositCharge4);
  }

  protected virtual void CADepositDetail_PaymentMethodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CADepositCharge_ChargeRate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CADepositCharge row = (CADepositCharge) e.Row;
    CADeposit current = ((PXSelectBase<CADeposit>) this.Document).Current;
    if (current == null)
      return;
    int? nullable = current.CashAccountID;
    if (!nullable.HasValue)
      return;
    nullable = row.DepositAcctID;
    if (!nullable.HasValue)
      return;
    CashAccountDeposit cashAccountDeposit = PXResultset<CashAccountDeposit>.op_Implicit(PXSelectBase<CashAccountDeposit, PXSelect<CashAccountDeposit, Where<CashAccountDeposit.cashAccountID, Equal<Required<CashAccountDeposit.cashAccountID>>, And<CashAccountDeposit.depositAcctID, Equal<Required<CashAccountDeposit.depositAcctID>>, And<CashAccountDeposit.paymentMethodID, Equal<Required<CashAccountDeposit.paymentMethodID>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) current.CashAccountID,
      (object) row.DepositAcctID,
      (object) row.PaymentMethodID
    }));
    if (cashAccountDeposit == null)
      return;
    e.NewValue = (object) cashAccountDeposit.ChargeRate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CADepositCharge_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is CADepositCharge row))
      return;
    string error1 = PXUIFieldAttribute.GetError<CADepositCharge.paymentMethodID>(sender, (object) row);
    string error2 = PXUIFieldAttribute.GetError<CADepositCharge.entryTypeID>(sender, (object) row);
    if (!string.IsNullOrEmpty(error1))
      sender.RaiseExceptionHandling<CADepositCharge.paymentMethodID>((object) row, (object) row.PaymentMethodID, (Exception) new PXSetPropertyException(error1, (PXErrorLevel) 4));
    if (string.IsNullOrEmpty(error2))
      return;
    sender.RaiseExceptionHandling<CADepositCharge.entryTypeID>((object) row, (object) row.EntryTypeID, (Exception) new PXSetPropertyException(error2, (PXErrorLevel) 4));
  }

  protected virtual void PaymentInfo_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PaymentInfo_BAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PaymentInfo_LocationID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PaymentInfo_PMInstanceID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PaymentInfo_CashAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PaymentInfo_CashSubID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PaymentInfo_PaymentMethodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PaymentInfo_CuryID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARPaymentUpdate_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    CADepositEntry.ARPaymentUpdate row = (CADepositEntry.ARPaymentUpdate) e.Row;
    if (row == null)
      return;
    GraphHelper.Hold(((PXSelectBase) this.paymentUpdate).Cache, (object) row);
  }

  protected virtual void APPaymentUpdate_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    CADepositEntry.APPaymentUpdate row = (CADepositEntry.APPaymentUpdate) e.Row;
    if (row == null)
      return;
    GraphHelper.Hold(((PXSelectBase) this.paymentUpdateAP).Cache, (object) row);
  }

  protected virtual void CAAdjUpdate_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    CADepositEntry.CAAdjUpdate row = (CADepositEntry.CAAdjUpdate) e.Row;
    if (row == null)
      return;
    GraphHelper.Hold(((PXSelectBase) this.paymentUpdateCA).Cache, (object) row);
  }

  protected virtual void PaymentFilter_StartDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<CADeposit>) this.Document).Current == null)
      return;
    DateTime? tranDate = ((PXSelectBase<CADeposit>) this.Document).Current.TranDate;
    if (!tranDate.HasValue)
      return;
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    tranDate = ((PXSelectBase<CADeposit>) this.Document).Current.TranDate;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime> local = (ValueType) tranDate.Value.AddDays(-7.0);
    defaultingEventArgs.NewValue = (object) local;
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CA.Light.BAccount.acctCD> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CA.Light.BAccount.acctName> e)
  {
  }

  [PXMergeAttributes]
  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CA.Light.Location.locationCD> e)
  {
  }

  public virtual PaymentInfo Copy(CADepositEntry.ARPaymentUpdate source, PaymentInfo destination)
  {
    destination.Module = "AR";
    destination.DocType = source.DocType;
    destination.RefNbr = source.RefNbr;
    destination.BAccountID = source.CustomerID;
    destination.BAccountCD = source.CustomerCD;
    destination.BAccountName = source.CustomerAcctName;
    destination.LocationID = source.CustomerLocationID;
    destination.LocationCD = source.CustomerLocationCD;
    destination.ExtRefNbr = source.ExtRefNbr;
    destination.Status = source.Status;
    destination.PaymentMethodID = source.PaymentMethodID;
    destination.CuryID = source.CuryID;
    destination.CuryInfoID = source.CuryInfoID;
    destination.CuryGrossPaymentAmount = source.CuryOrigDocAmt;
    destination.GrossPaymentAmount = source.OrigDocAmt;
    PaymentInfo paymentInfo1 = destination;
    Decimal? curyOrigDocAmt = source.CuryOrigDocAmt;
    Decimal? nullable1 = source.CuryChargeAmt;
    Decimal? nullable2 = curyOrigDocAmt.HasValue & nullable1.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    paymentInfo1.CuryOrigDocAmt = nullable2;
    PaymentInfo paymentInfo2 = destination;
    nullable1 = source.OrigDocAmt;
    Decimal? chargeAmt = source.ChargeAmt;
    Decimal? nullable3 = nullable1.HasValue & chargeAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - chargeAmt.GetValueOrDefault()) : new Decimal?();
    paymentInfo2.OrigDocAmt = nullable3;
    destination.CuryChargeTotal = source.CuryChargeAmt;
    destination.ChargeTotal = source.ChargeAmt;
    destination.DocDate = source.DocDate;
    destination.DepositAfter = source.DepositAfter;
    destination.CashAccountID = source.CashAccountID;
    destination.DrCr = ARPaymentType.DrCr(source.DocType);
    destination.PMInstanceID = source.PMInstanceID;
    return destination;
  }

  public virtual PaymentInfo Copy(
    PX.Objects.CA.Light.ARPayment source,
    PX.Objects.CA.Light.BAccount bAccount,
    PX.Objects.CA.Light.Location location,
    PaymentInfo destination)
  {
    destination.Module = "AR";
    destination.DocType = source.DocType;
    destination.RefNbr = source.RefNbr;
    destination.BAccountID = source.CustomerID;
    destination.BAccountCD = bAccount.AcctCD;
    destination.BAccountName = bAccount.AcctName;
    destination.LocationID = source.CustomerLocationID;
    destination.LocationCD = location.LocationCD;
    destination.ExtRefNbr = source.ExtRefNbr;
    destination.Status = source.Status;
    destination.PaymentMethodID = source.PaymentMethodID;
    destination.CuryID = source.CuryID;
    destination.CuryInfoID = source.CuryInfoID;
    destination.CuryGrossPaymentAmount = source.CuryOrigDocAmt;
    destination.GrossPaymentAmount = source.OrigDocAmt;
    PaymentInfo paymentInfo1 = destination;
    Decimal? curyOrigDocAmt = source.CuryOrigDocAmt;
    Decimal? nullable1 = source.CuryChargeAmt;
    Decimal? nullable2 = curyOrigDocAmt.HasValue & nullable1.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    paymentInfo1.CuryOrigDocAmt = nullable2;
    PaymentInfo paymentInfo2 = destination;
    nullable1 = source.OrigDocAmt;
    Decimal? chargeAmt = source.ChargeAmt;
    Decimal? nullable3 = nullable1.HasValue & chargeAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - chargeAmt.GetValueOrDefault()) : new Decimal?();
    paymentInfo2.OrigDocAmt = nullable3;
    destination.CuryChargeTotal = source.CuryChargeAmt;
    destination.ChargeTotal = source.ChargeAmt;
    destination.DocDate = source.DocDate;
    destination.DepositAfter = source.DepositAfter;
    destination.CashAccountID = source.CashAccountID;
    destination.DrCr = ARPaymentType.DrCr(source.DocType);
    destination.PMInstanceID = source.PMInstanceID;
    return destination;
  }

  public virtual PaymentInfo Copy(CADepositEntry.APPaymentUpdate source, PaymentInfo destination)
  {
    destination.Module = "AP";
    destination.DocType = source.DocType;
    destination.RefNbr = source.RefNbr;
    destination.BAccountID = source.VendorID;
    destination.BAccountCD = source.VendorCD;
    destination.BAccountName = source.VendorAcctName;
    destination.LocationID = source.VendorLocationID;
    destination.LocationCD = source.VendorLocationCD;
    destination.ExtRefNbr = source.ExtRefNbr;
    destination.Status = source.Status;
    destination.PaymentMethodID = source.PaymentMethodID;
    destination.CuryID = source.CuryID;
    destination.CuryInfoID = source.CuryInfoID;
    destination.CuryOrigDocAmt = source.CuryOrigDocAmt;
    destination.OrigDocAmt = source.OrigDocAmt;
    destination.DocDate = source.DocDate;
    destination.DepositAfter = source.DepositAfter;
    destination.CashAccountID = source.CashAccountID;
    destination.DrCr = APPaymentType.DrCr(source.DocType);
    destination.PMInstanceID = new int?();
    return destination;
  }

  public virtual PaymentInfo Copy(
    PX.Objects.CA.Light.APPayment source,
    PX.Objects.CA.Light.BAccount bAccount,
    PX.Objects.CA.Light.Location location,
    PaymentInfo destination)
  {
    destination.Module = "AP";
    destination.DocType = source.DocType;
    destination.RefNbr = source.RefNbr;
    destination.BAccountID = source.VendorID;
    destination.BAccountCD = bAccount.AcctCD;
    destination.BAccountName = bAccount.AcctName;
    destination.LocationID = source.VendorLocationID;
    destination.LocationCD = location.LocationCD;
    destination.ExtRefNbr = source.ExtRefNbr;
    destination.Status = source.Status;
    destination.PaymentMethodID = source.PaymentMethodID;
    destination.CuryID = source.CuryID;
    destination.CuryInfoID = source.CuryInfoID;
    destination.CuryOrigDocAmt = source.CuryOrigDocAmt;
    destination.OrigDocAmt = source.OrigDocAmt;
    destination.DocDate = source.DocDate;
    destination.DepositAfter = source.DepositAfter;
    destination.CashAccountID = source.CashAccountID;
    destination.DrCr = APPaymentType.DrCr(source.DocType);
    destination.PMInstanceID = new int?();
    return destination;
  }

  public virtual PaymentInfo Copy(PX.Objects.CA.Light.CAAdj source, PaymentInfo destination)
  {
    destination.Module = "CA";
    destination.DocType = source.AdjTranType;
    destination.RefNbr = source.AdjRefNbr;
    destination.ExtRefNbr = source.ExtRefNbr;
    destination.Status = this.ConvertCAStatusToDepositStatus(source.Status);
    destination.CuryID = source.CuryID;
    destination.CuryInfoID = source.CuryInfoID;
    destination.CuryGrossPaymentAmount = source.CuryTranAmt;
    destination.GrossPaymentAmount = source.TranAmt;
    destination.CuryOrigDocAmt = source.CuryTranAmt;
    destination.OrigDocAmt = source.TranAmt;
    destination.DocDate = source.TranDate;
    destination.DepositAfter = source.DepositAfter;
    destination.CashAccountID = source.CashAccountID;
    destination.DrCr = source.DrCr;
    return destination;
  }

  protected virtual string ConvertCAStatusToDepositStatus(string status)
  {
    switch (status)
    {
      case "R":
        return "D";
      case "J":
        return "R";
      default:
        return status;
    }
  }

  public virtual int AddPaymentInfoBatch(IEnumerable<PaymentInfo> payments)
  {
    int num = 0;
    PXResultset<CADepositDetail> pxResultset = ((PXSelectBase<CADepositDetail>) this.DepositPayments).Select(Array.Empty<object>());
    HashSet<string> stringSet = new HashSet<string>();
    foreach (PXResult<CADepositDetail> pxResult in pxResultset)
    {
      CADepositDetail caDepositDetail = PXResult<CADepositDetail>.op_Implicit(pxResult);
      stringSet.Add(caDepositDetail.OrigModule + caDepositDetail.OrigDocType + caDepositDetail.OrigRefNbr);
    }
    foreach (PaymentInfo payment in payments)
    {
      CashAccountDeposit accountDepositSettings = this.GetCashAccountDepositSettings(payment);
      if (accountDepositSettings != null)
      {
        if (!stringSet.Contains(payment.Module + payment.DocType + payment.RefNbr))
        {
          try
          {
            this.AddPaymentInfo(payment, accountDepositSettings, true);
            ++num;
          }
          catch (PXException ex)
          {
            PXGraph.ThrowWithoutRollback((Exception) ex);
          }
        }
      }
    }
    return num;
  }

  protected virtual CashAccountDeposit GetCashAccountDepositSettings(PaymentInfo payment)
  {
    CashAccountDeposit accountDepositSettings = (CashAccountDeposit) null;
    if (payment.Module == "AP" || payment.Module == "AR")
      accountDepositSettings = PXResultset<CashAccountDeposit>.op_Implicit(PXSelectBase<CashAccountDeposit, PXSelect<CashAccountDeposit, Where<CashAccountDeposit.cashAccountID, Equal<Current<CADeposit.cashAccountID>>, And<CashAccountDeposit.depositAcctID, Equal<Required<CashAccountDeposit.depositAcctID>>, And<Where<CashAccountDeposit.paymentMethodID, Equal<EmptyString>, Or<CashAccountDeposit.paymentMethodID, Equal<Required<CashAccountDeposit.paymentMethodID>>>>>>>, OrderBy<Desc<CashAccountDeposit.paymentMethodID>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) payment.CashAccountID,
        (object) payment.PaymentMethodID
      }));
    else if (payment.Module == "CA")
    {
      accountDepositSettings = PXResultset<CashAccountDeposit>.op_Implicit(PXSelectBase<CashAccountDeposit, PXSelect<CashAccountDeposit, Where<CashAccountDeposit.cashAccountID, Equal<Current<CADeposit.cashAccountID>>, And<CashAccountDeposit.depositAcctID, Equal<Required<CashAccountDeposit.depositAcctID>>, And<CashAccountDeposit.paymentMethodID, Equal<EmptyString>>>>, OrderBy<Desc<CashAccountDeposit.paymentMethodID>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) payment.CashAccountID
      }));
      if (accountDepositSettings == null)
      {
        accountDepositSettings = new CashAccountDeposit();
        accountDepositSettings.CashAccountID = ((PXSelectBase<CADeposit>) this.DocumentCurrent).Current.CashAccountID;
      }
    }
    return accountDepositSettings;
  }

  public virtual CADepositDetail AddPaymentInfo(
    PaymentInfo aPayment,
    CashAccountDeposit settings,
    bool skipCheck)
  {
    if (!skipCheck)
    {
      foreach (PXResult<CADepositDetail> pxResult in ((PXSelectBase<CADepositDetail>) this.DepositPayments).Select(Array.Empty<object>()))
      {
        CADepositDetail caDepositDetail = PXResult<CADepositDetail>.op_Implicit(pxResult);
        if (caDepositDetail.OrigDocType == aPayment.DocType && caDepositDetail.OrigRefNbr == aPayment.RefNbr)
          return caDepositDetail;
      }
    }
    CADepositDetail aDest = new CADepositDetail();
    this.Copy(aDest, aPayment);
    CADepositEntry.Copy(aDest, settings);
    return ((PXSelectBase<CADepositDetail>) this.Details).Insert(aDest);
  }

  protected void Copy(CADepositDetail aDest, PaymentInfo aPayment)
  {
    aDest.OrigModule = aPayment.Module;
    aDest.OrigDocType = aPayment.DocType;
    aDest.OrigRefNbr = aPayment.RefNbr;
    aDest.OrigCuryID = aPayment.CuryID;
    aDest.OrigCuryInfoID = aPayment.CuryInfoID;
    aDest.OrigDrCr = aPayment.DrCr;
    aDest.CuryOrigAmt = aPayment.CuryOrigDocAmt;
    aDest.OrigAmt = aPayment.OrigDocAmt;
    aDest.CashAccountID = aPayment.CashAccountID;
    aDest.CuryTranAmt = aDest.CuryOrigAmtSigned;
    aDest.TranAmt = aDest.OrigAmtSigned;
  }

  protected static void Copy(CADepositDetail aDest, CashAccountDeposit aSettings)
  {
    aDest.ChargeEntryTypeID = aSettings.ChargeEntryTypeID;
    aDest.PaymentMethodID = aSettings.PaymentMethodID;
  }

  protected virtual void ParentFieldUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<CADeposit.tranDate, CADeposit.finPeriodID, CADeposit.curyID>(e.Row, e.OldRow))
      return;
    foreach (PXResult<CADepositDetail> pxResult in ((PXSelectBase<CADepositDetail>) this.Details).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.Details).Cache, (object) PXResult<CADepositDetail>.op_Implicit(pxResult));
    foreach (PXResult<CADepositCharge> pxResult in ((PXSelectBase<CADepositCharge>) this.Charges).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.Charges).Cache, (object) PXResult<CADepositCharge>.op_Implicit(pxResult));
  }

  public virtual void Persist() => ((PXGraph) this).Persist();

  /// <summary>
  /// Releases CADeposit and optionally posts it dependong on <see cref="P:PX.Objects.CA.CASetup.AutoPostOption" />.
  /// </summary>
  /// <param name="doc">Deposit to release.</param>
  /// <param name="externalPostList">List of Batches to post.
  /// If this parameter is not null batches will not be posted.
  /// These batches will be included to the list instead. </param>
  public static void ReleaseDoc(CADeposit doc, List<PX.Objects.GL.Batch> externalPostList = null)
  {
    bool flag = externalPostList != null;
    CAReleaseProcess instance1 = PXGraph.CreateInstance<CAReleaseProcess>();
    JournalEntry instance2 = PXGraph.CreateInstance<JournalEntry>();
    List<PX.Objects.GL.Batch> batchlist = new List<PX.Objects.GL.Batch>();
    ((PXGraph) instance1).Clear();
    instance1.ReleaseDeposit(instance2, ref batchlist, doc);
    CADepositEntry.UpdateCCBatch(doc);
    if (!instance1.AutoPost)
      return;
    if (!flag)
    {
      PostGraph instance3 = PXGraph.CreateInstance<PostGraph>();
      List<int> intList = new List<int>();
      for (int count = intList.Count; count < batchlist.Count; ++count)
        intList.Add(count);
      for (int index = 0; index < batchlist.Count; ++index)
      {
        PX.Objects.GL.Batch b = batchlist[index];
        try
        {
          ((PXGraph) instance3).Clear();
          ((PXGraph) instance3).TimeStamp = b.tstamp;
          instance3.PostBatchProc(b);
        }
        catch (Exception ex)
        {
          throw new PX.Objects.Common.PXMassProcessException(intList[index], ex);
        }
      }
    }
    else
    {
      foreach (PX.Objects.GL.Batch batch in batchlist)
        externalPostList.Add(batch);
    }
  }

  public virtual void VoidDepositProc(CADeposit doc)
  {
    bool? released = doc.Released;
    bool flag = false;
    if (released.GetValueOrDefault() == flag & released.HasValue)
      throw new PXException("An unreleased document can't be voided.");
    PXResult<CADeposit, PX.Objects.CM.Extensions.CurrencyInfo> pxResult1 = (PXResult<CADeposit, PX.Objects.CM.Extensions.CurrencyInfo>) PXResultset<CADeposit>.op_Implicit(PXSelectBase<CADeposit, PXSelectReadonly2<CADeposit, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<CADeposit.curyInfoID>>>, Where<CADeposit.tranType, Equal<Required<CADeposit.tranType>>, And<CADeposit.refNbr, Equal<Required<CADeposit.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }));
    ((PXGraph) this).Clear();
    ((PXSelectBase) this.Details).Cache.AllowUpdate = true;
    ((PXSelectBase) this.Details).Cache.AllowInsert = true;
    ((PXSelectBase) this.Details).Cache.AllowDelete = true;
    ((PXSelectBase) this.Charges).Cache.AllowInsert = true;
    ((PXSelectBase) this.Charges).Cache.AllowUpdate = true;
    ((PXSelectBase) this.Charges).Cache.AllowDelete = true;
    CADeposit copy1 = (CADeposit) ((PXSelectBase) this.Document).Cache.CreateCopy((object) PXResult<CADeposit, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult1));
    copy1.NoteID = new Guid?();
    copy1.TranType = "CVD";
    copy1.TranID = new long?();
    copy1.CashTranID = new long?();
    copy1.ChargeTranID = new long?();
    copy1.CuryInfoID = new long?();
    copy1.Released = new bool?(false);
    copy1.Hold = new bool?(true);
    copy1.ClearDate = new DateTime?();
    copy1.Cleared = new bool?(false);
    copy1.LineCntr = new int?(0);
    copy1.DrCr = doc.DrCr == "D" ? "C" : "D";
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) this).GetExtension<CADepositEntry.MultiCurrency>().CloneCurrencyInfo(PXResult<CADeposit, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult1));
    copy1.CuryInfoID = currencyInfo.CuryInfoID;
    copy1.CuryDetailTotal = new Decimal?(0M);
    copy1.CuryChargeTotal = new Decimal?(0M);
    copy1.CuryTranAmt = new Decimal?(0M);
    if (copy1.ExtraCashAccountID.HasValue)
    {
      int? extraCashAccountId = copy1.ExtraCashAccountID;
      int? cashAccountId = copy1.CashAccountID;
      if (extraCashAccountId.GetValueOrDefault() == cashAccountId.GetValueOrDefault() & extraCashAccountId.HasValue == cashAccountId.HasValue)
        copy1.ExtraCashAccountID = new int?();
    }
    CADeposit caDeposit = ((PXSelectBase<CADeposit>) this.Document).Insert(copy1);
    ((PXSelectBase<CADeposit>) this.Document).Current = caDeposit;
    foreach (PXResult<CADepositDetail, PX.Objects.AR.ARPayment, PX.Objects.AP.APPayment> pxResult2 in PXSelectBase<CADepositDetail, PXSelectReadonly2<CADepositDetail, LeftJoin<PX.Objects.AR.ARPayment, On<PX.Objects.AR.ARPayment.docType, Equal<CADepositDetail.origDocType>, And<PX.Objects.AR.ARPayment.refNbr, Equal<CADepositDetail.origRefNbr>, And<CADepositDetail.origModule, Equal<BatchModule.moduleAR>>>>, LeftJoin<PX.Objects.AP.APPayment, On<PX.Objects.AP.APPayment.docType, Equal<CADepositDetail.origDocType>, And<PX.Objects.AP.APPayment.refNbr, Equal<CADepositDetail.origRefNbr>, And<CADepositDetail.origModule, Equal<BatchModule.moduleAP>>>>>>, Where<CADepositDetail.tranType, Equal<Required<CADepositDetail.tranType>>, And<CADepositDetail.refNbr, Equal<Required<CADepositDetail.refNbr>>>>, OrderBy<Asc<Switch<Case<Where<PX.Objects.AR.ARPayment.docType, Equal<ARDocType.voidPayment>>, int0>, int1>, Asc<Switch<Case<Where<PX.Objects.AP.APPayment.docType, Equal<APDocType.voidCheck>>, int0>, int1>, Asc<CADepositDetail.tranType, Asc<CADepositDetail.refNbr, Asc<CADepositDetail.lineNbr>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }))
    {
      CADepositDetail caDepositDetail = PXResult<CADepositDetail, PX.Objects.AR.ARPayment, PX.Objects.AP.APPayment>.op_Implicit(pxResult2);
      PX.Objects.AR.ARPayment arPayment = PXResult<CADepositDetail, PX.Objects.AR.ARPayment, PX.Objects.AP.APPayment>.op_Implicit(pxResult2);
      PX.Objects.AP.APPayment apPayment = PXResult<CADepositDetail, PX.Objects.AR.ARPayment, PX.Objects.AP.APPayment>.op_Implicit(pxResult2);
      int? cashAccountId1;
      int? cashAccountId2;
      if (!string.IsNullOrEmpty(arPayment.RefNbr) && arPayment.Voided.GetValueOrDefault())
      {
        cashAccountId1 = (int?) ((PXSelectBase<CADepositDetail>) this.CADepositDetail_OrigModuleDocTypeRefNbr).SelectSingle(new object[5]
        {
          (object) "CDT",
          (object) doc.RefNbr,
          (object) "AR",
          (object) ARPaymentType.GetVoidingARDocType(arPayment.DocType),
          (object) arPayment.RefNbr
        })?.CashAccountID;
        cashAccountId2 = arPayment.CashAccountID;
        if (!(cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue))
          continue;
      }
      if (!string.IsNullOrEmpty(apPayment.RefNbr) && apPayment.Voided.GetValueOrDefault())
      {
        cashAccountId2 = (int?) ((PXSelectBase<CADepositDetail>) this.CADepositDetail_OrigModuleDocTypeRefNbr).SelectSingle(new object[5]
        {
          (object) "CDT",
          (object) doc.RefNbr,
          (object) "AP",
          (object) APPaymentType.GetVoidingAPDocType(apPayment.DocType),
          (object) apPayment.RefNbr
        })?.CashAccountID;
        cashAccountId1 = apPayment.CashAccountID;
        if (!(cashAccountId2.GetValueOrDefault() == cashAccountId1.GetValueOrDefault() & cashAccountId2.HasValue == cashAccountId1.HasValue))
          continue;
      }
      CADepositDetail copy2 = (CADepositDetail) ((PXSelectBase) this.Details).Cache.CreateCopy((object) caDepositDetail);
      copy2.TranType = caDeposit.TranType;
      copy2.DetailType = caDepositDetail.DetailType == "CHD" ? "VCD" : (caDepositDetail.DetailType == "CSD" ? "VSD" : string.Empty);
      if (string.IsNullOrEmpty(copy2.DetailType))
        throw new PXException("The {0} detail type is unknown.", new object[1]
        {
          (object) caDepositDetail.DetailType
        });
      copy2.DrCr = caDepositDetail.DrCr == "D" ? "C" : "D";
      copy2.CuryInfoID = currencyInfo.CuryInfoID;
      copy2.TranID = new long?();
      ((PXSelectBase<CADepositDetail>) this.Details).Insert(copy2);
    }
    foreach (PXResult<CADepositCharge> pxResult3 in PXSelectBase<CADepositCharge, PXSelectReadonly<CADepositCharge, Where<CADepositCharge.tranType, Equal<Required<CADepositCharge.tranType>>, And<CADepositCharge.refNbr, Equal<Required<CADepositCharge.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }))
    {
      CADepositCharge caDepositCharge = PXResult<CADepositCharge>.op_Implicit(pxResult3);
      CADepositCharge copy3 = (CADepositCharge) ((PXSelectBase) this.Charges).Cache.CreateCopy((object) caDepositCharge);
      copy3.TranType = caDeposit.TranType;
      copy3.DrCr = caDepositCharge.DrCr == "D" ? "C" : "D";
      copy3.CuryInfoID = currencyInfo.CuryInfoID;
      ((PXSelectBase<CADepositCharge>) this.Charges).Insert(copy3);
    }
    ((PXSelectBase) this.Document).View.RequestRefresh();
  }

  private static void UpdateCCBatch(CADeposit doc)
  {
    switch (doc?.TranType)
    {
      case "CDT":
        CADepositEntry instance1 = PXGraph.CreateInstance<CADepositEntry>();
        ((PXSelectBase<CADeposit>) instance1.Document).Current = doc;
        foreach (PXResult<CCBatch> pxResult in ((PXSelectBase<CCBatch>) instance1.ccBatch).Select(Array.Empty<object>()))
        {
          CCBatch ccBatch = PXResult<CCBatch>.op_Implicit(pxResult);
          ccBatch.Status = "DPD";
          ((PXSelectBase<CCBatch>) instance1.ccBatch).Update(ccBatch);
        }
        ((PXAction) instance1.Save).Press();
        break;
      case "CVD":
        CADepositEntry instance2 = PXGraph.CreateInstance<CADepositEntry>();
        CADeposit caDeposit = CADeposit.PK.Find((PXGraph) instance2, "CDT", doc.RefNbr);
        ((PXSelectBase<CADeposit>) instance2.Document).Current = caDeposit;
        foreach (PXResult<CCBatch> pxResult in ((PXSelectBase<CCBatch>) instance2.ccBatch).Select(Array.Empty<object>()))
        {
          CCBatch ccBatch = PXResult<CCBatch>.op_Implicit(pxResult);
          ccBatch.Status = "PRD";
          ccBatch.DepositType = (string) null;
          ccBatch.DepositNbr = (string) null;
          ((PXSelectBase<CCBatch>) instance2.ccBatch).Update(ccBatch);
        }
        ((PXAction) instance2.Save).Press();
        break;
    }
  }

  public class MultiCurrency : MultiCurrencyGraph<CADepositEntry, CADeposit>
  {
    protected override string Module => "CA";

    protected override MultiCurrencyGraph<CADepositEntry, CADeposit>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<CADepositEntry, CADeposit>.CurySourceMapping(typeof (CashAccount))
      {
        CuryID = typeof (CashAccount.curyID),
        CuryRateTypeID = typeof (CashAccount.curyRateTypeID)
      };
    }

    protected override MultiCurrencyGraph<CADepositEntry, CADeposit>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<CADepositEntry, CADeposit>.DocumentMapping(typeof (CADeposit))
      {
        DocumentDate = typeof (CADeposit.tranDate),
        BAccountID = typeof (CADeposit.cashAccountID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[4]
      {
        (PXSelectBase) this.Base.Document,
        (PXSelectBase) this.Base.DepositPayments,
        (PXSelectBase) this.Base.Charges,
        (PXSelectBase) this.Base.cATransHeaderAndDetails
      };
    }
  }

  [Serializable]
  public class PaymentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(10)]
    [PXSelector(typeof (Search<PaymentMethod.paymentMethodID, Where<PaymentMethod.useForAR, Equal<True>>>))]
    [PXUIField(DisplayName = "Payment Method", Enabled = true)]
    public virtual string PaymentMethodID { get; set; }

    [CashAccount(null, typeof (Search5<CashAccount.cashAccountID, InnerJoin<CashAccountDeposit, On<CashAccountDeposit.depositAcctID, Equal<CashAccount.cashAccountID>>>, Where<CashAccountDeposit.cashAccountID, Equal<Current<CADeposit.cashAccountID>>, And<Match<Current<AccessInfo.userName>>>>, Aggregate<GroupBy<CashAccount.cashAccountID>>>), DisplayName = "Clearing Account")]
    public virtual int? CashAccountID { get; set; }

    [PXDBDate]
    [PXDefault(typeof (CADeposit.tranDate))]
    [PXUIField(DisplayName = "Start Date")]
    public virtual DateTime? StartDate { get; set; }

    [PXDBDate]
    [PXDefault(typeof (CADeposit.tranDate))]
    [PXUIField(DisplayName = "End Date")]
    public virtual DateTime? EndDate { get; set; }

    [PXDecimal]
    [PXUnboundDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Selection Total", Enabled = false)]
    public virtual Decimal? SelectionTotal { get; set; }

    [PXInt]
    [PXUnboundDefault(TypeCode.Int32, "0")]
    [PXUIField(DisplayName = "Number of Documents", Enabled = false)]
    public virtual int? NumberOfDocuments { get; set; }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CADepositEntry.PaymentFilter.paymentMethodID>
    {
    }

    public abstract class cashAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CADepositEntry.PaymentFilter.cashAccountID>
    {
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CADepositEntry.PaymentFilter.startDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CADepositEntry.PaymentFilter.endDate>
    {
    }

    public abstract class selectionTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CADepositEntry.PaymentFilter.selectionTotal>
    {
    }

    public abstract class numberOfDocuments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CADepositEntry.PaymentFilter.numberOfDocuments>
    {
    }
  }

  [Serializable]
  public class ARPaymentUpdate : PX.Objects.CA.Light.ARPayment
  {
    public virtual void SetReferenceTo(CADeposit doc)
    {
      this.DepositType = doc.TranType;
      this.DepositNbr = doc.RefNbr;
    }

    public virtual void ClearDepositReference()
    {
      this.DepositType = (string) null;
      this.DepositNbr = (string) null;
    }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.CA.Light.BAccount" /> of the Customer.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.CA.Light.BAccount.AcctCD" /> field.
    /// </value>
    [PXString]
    [PXDBScalar(typeof (Search<PX.Objects.CA.Light.BAccount.acctCD, Where<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.AR.ARRegister.customerID>>>))]
    public virtual string CustomerCD { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.CA.Light.BAccount" /> of the Customer.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.CA.Light.BAccount.AcctName" /> field.
    /// </value>
    [PXString]
    [PXDBScalar(typeof (Search<PX.Objects.CA.Light.BAccount.acctName, Where<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.AR.ARRegister.customerID>>>))]
    public virtual string CustomerAcctName { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.CA.Light.Location" /> of the Customer.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.CA.Light.Location.LocationCD" /> field.
    /// </value>
    [PXString]
    [PXDBScalar(typeof (Search<PX.Objects.CA.Light.Location.locationCD, Where<PX.Objects.CA.Light.Location.locationID, Equal<PX.Objects.AR.ARRegister.customerLocationID>>>))]
    public virtual string CustomerLocationCD { get; set; }

    public new abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CADepositEntry.ARPaymentUpdate.docType>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CADepositEntry.ARPaymentUpdate.refNbr>
    {
    }

    public abstract class depositDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CADepositEntry.ARPaymentUpdate.depositDate>
    {
    }

    public new abstract class depositType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CADepositEntry.ARPaymentUpdate.depositType>
    {
    }

    public new abstract class depositNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CADepositEntry.ARPaymentUpdate.depositNbr>
    {
    }

    public new abstract class deposited : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CADepositEntry.ARPaymentUpdate.deposited>
    {
    }

    public abstract class customerCD : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CADepositEntry.ARPaymentUpdate.customerCD>
    {
    }

    public abstract class customerAcctName : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CADepositEntry.ARPaymentUpdate.customerAcctName>
    {
    }

    public abstract class customerLocationCD : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PX.Objects.AR.ARRegister.customerID>
    {
    }
  }

  [Serializable]
  public class APPaymentUpdate : PX.Objects.CA.Light.APPayment
  {
    public virtual void SetReferenceTo(CADeposit doc)
    {
      this.DepositType = doc.TranType;
      this.DepositNbr = doc.RefNbr;
    }

    public virtual void ClearDepositReference()
    {
      this.DepositType = (string) null;
      this.DepositNbr = (string) null;
    }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.CA.Light.BAccount" /> of the Vendor.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.CA.Light.BAccount.AcctCD" /> field.
    /// </value>
    [PXString]
    [PXDBScalar(typeof (Search<PX.Objects.CA.Light.BAccount.acctCD, Where<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.AP.APRegister.vendorID>>>))]
    public virtual string VendorCD { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.CA.Light.BAccount" /> of the Vendor.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.CA.Light.BAccount.AcctName" /> field.
    /// </value>
    [PXString]
    [PXDBScalar(typeof (Search<PX.Objects.CA.Light.BAccount.acctName, Where<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.AP.APRegister.vendorID>>>))]
    public virtual string VendorAcctName { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.CA.Light.Location" /> of the Vendor.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.CA.Light.Location.LocationCD" /> field.
    /// </value>
    [PXString]
    [PXDBScalar(typeof (Search<PX.Objects.CA.Light.Location.locationCD, Where<PX.Objects.CA.Light.Location.locationID, Equal<PX.Objects.AP.APRegister.vendorLocationID>>>))]
    public virtual string VendorLocationCD { get; set; }

    public new abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CADepositEntry.APPaymentUpdate.docType>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CADepositEntry.APPaymentUpdate.refNbr>
    {
    }

    public abstract class depositDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CADepositEntry.APPaymentUpdate.depositDate>
    {
    }

    public new abstract class depositType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CADepositEntry.APPaymentUpdate.depositType>
    {
    }

    public new abstract class depositNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CADepositEntry.APPaymentUpdate.depositNbr>
    {
    }

    public new abstract class deposited : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CADepositEntry.APPaymentUpdate.deposited>
    {
    }

    public abstract class vendorCD : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CADepositEntry.APPaymentUpdate.vendorCD>
    {
    }

    public abstract class vendorAcctName : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CADepositEntry.APPaymentUpdate.vendorAcctName>
    {
    }

    public abstract class vendorLocationCD : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CADepositEntry.APPaymentUpdate.vendorLocationCD>
    {
    }
  }

  [Serializable]
  public class CAAdjUpdate : PX.Objects.CA.Light.CAAdj
  {
    public virtual void SetReferenceTo(CADeposit doc)
    {
      this.DepositType = doc.TranType;
      this.DepositNbr = doc.RefNbr;
    }

    public virtual void ClearDepositReference()
    {
      this.DepositType = (string) null;
      this.DepositNbr = (string) null;
    }

    public new abstract class adjTranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CADepositEntry.CAAdjUpdate.adjTranType>
    {
    }

    public new abstract class adjRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CADepositEntry.CAAdjUpdate.adjRefNbr>
    {
    }

    public abstract class depositDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CADepositEntry.CAAdjUpdate.depositDate>
    {
    }

    public new abstract class depositType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CADepositEntry.CAAdjUpdate.depositType>
    {
    }

    public new abstract class depositNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CADepositEntry.CAAdjUpdate.depositNbr>
    {
    }

    public new abstract class deposited : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CADepositEntry.CAAdjUpdate.deposited>
    {
    }
  }
}
