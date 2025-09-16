// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.CCPaymentEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.Extensions.PaymentTransaction;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing;

public class CCPaymentEntry
{
  private PXGraph graph;
  private ICCTransactionsProcessor transactionProcessor;

  public AfterProcessingManager AfterProcessingManager { get; set; }

  private ICCTransactionsProcessor TransactionProcessor
  {
    get
    {
      if (this.transactionProcessor == null)
        this.transactionProcessor = CCTransactionsProcessor.GetCCTransactionsProcessor();
      return this.transactionProcessor;
    }
    set => this.transactionProcessor = value;
  }

  public bool NeedPersistAfterRecord { get; set; } = true;

  public CCPaymentEntry(PXGraph graph) => this.graph = graph;

  public void AuthorizeCCpayment(ICCPayment doc, IExternalTransactionAdapter paymentTransaction)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CCPaymentEntry.\u003C\u003Ec__DisplayClass14_0 cDisplayClass140 = new CCPaymentEntry.\u003C\u003Ec__DisplayClass14_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.doc = doc;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass140.doc == null || !cDisplayClass140.doc.CuryDocBal.HasValue)
      return;
    IEnumerable<IExternalTransaction> externalTransactions = paymentTransaction.Select();
    ExternalTransactionState transactionState = !ExternalTranHelper.HasOpenCCProcTran(this.graph, externalTransactions.FirstOrDefault<IExternalTransaction>()) ? ExternalTranHelper.GetActiveTransactionState(this.graph, externalTransactions) : throw new PXException("This document has one or more transaction under processing.");
    if (transactionState.IsCaptured || transactionState.IsPreAuthorized)
      throw new PXException("This payment has been pre-authorized already.");
    // ISSUE: reference to a compiler-generated field
    bool? released = cDisplayClass140.doc.Released;
    bool flag = false;
    if (released.GetValueOrDefault() == flag & released.HasValue)
      this.graph.Actions.PressSave();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.toProc = this.graph.Caches[cDisplayClass140.doc.GetType()].CreateCopy((object) cDisplayClass140.doc) as ICCPayment;
    // ISSUE: method pointer
    PXLongOperation.StartOperation(this.graph, new PXToggleAsyncDelegate((object) cDisplayClass140, __methodptr(\u003CAuthorizeCCpayment\u003Eb__0)));
  }

  public void IncreaseAuthorizedAmountCCpayment(
    ICCPayment doc,
    IExternalTransactionAdapter paymentTransaction)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CCPaymentEntry.\u003C\u003Ec__DisplayClass15_0 cDisplayClass150 = new CCPaymentEntry.\u003C\u003Ec__DisplayClass15_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.doc = doc;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass150.doc == null || !cDisplayClass150.doc.CuryDocBal.HasValue)
      return;
    IEnumerable<IExternalTransaction> externalTransactions = paymentTransaction.Select();
    ExternalTransactionState transactionState = !ExternalTranHelper.HasOpenCCProcTran(this.graph, externalTransactions.FirstOrDefault<IExternalTransaction>()) ? ExternalTranHelper.GetActiveTransactionState(this.graph, externalTransactions) : throw new PXException("This document has one or more transaction under processing.");
    if (transactionState.ExternalTransaction == null)
      throw new PXException("The {0} transaction has an invalid status.", new object[1]
      {
        (object) externalTransactions.FirstOrDefault<IExternalTransaction>().TranNumber
      });
    if (ExternalTranHelper.IsExpired(transactionState.ExternalTransaction))
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      throw new PXException("Authorization for card transaction in the {0} document with the {1} ref. number is expired. Void the authorization and re-authorize the card payment.", new object[2]
      {
        (object) ARDocType.GetDisplayName(cDisplayClass150.doc.DocType),
        (object) cDisplayClass150.doc.RefNbr
      });
    }
    if (!transactionState.IsPreAuthorized)
      throw new PXException("The {0} transaction has an invalid status.", new object[1]
      {
        (object) transactionState.ExternalTransaction.RefNbr
      });
    // ISSUE: reference to a compiler-generated field
    bool? released = cDisplayClass150.doc.Released;
    bool flag = false;
    if (released.GetValueOrDefault() == flag & released.HasValue)
      this.graph.Actions.PressSave();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.toProc = this.graph.Caches[cDisplayClass150.doc.GetType()].CreateCopy((object) cDisplayClass150.doc) as ICCPayment;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.tranCopy = this.graph.Caches[transactionState.ExternalTransaction.GetType()].CreateCopy((object) transactionState.ExternalTransaction) as IExternalTransaction;
    // ISSUE: method pointer
    PXLongOperation.StartOperation(this.graph, new PXToggleAsyncDelegate((object) cDisplayClass150, __methodptr(\u003CIncreaseAuthorizedAmountCCpayment\u003Eb__0)));
  }

  public void CaptureCCpayment(ICCPayment doc, IExternalTransactionAdapter paymentTransaction)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CCPaymentEntry.\u003C\u003Ec__DisplayClass16_0 cDisplayClass160 = new CCPaymentEntry.\u003C\u003Ec__DisplayClass16_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass160.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass160.doc = doc;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass160.doc == null || !cDisplayClass160.doc.CuryDocBal.HasValue)
      return;
    IEnumerable<IExternalTransaction> externalTransactions = paymentTransaction.Select();
    ExternalTransactionState transactionState = !ExternalTranHelper.HasOpenCCProcTran(this.graph, externalTransactions.FirstOrDefault<IExternalTransaction>()) ? ExternalTranHelper.GetActiveTransactionState(this.graph, externalTransactions) : throw new PXException("This document has one or more transaction under processing.");
    if (transactionState.IsCaptured)
      throw new PXException("This payment has been captured already.");
    IExternalTransaction importedNeedSyncTran = ExternalTranHelper.GetImportedNeedSyncTran(this.graph, externalTransactions);
    if (importedNeedSyncTran != null)
      throw new PXException("The {0} transaction requires validation.", new object[1]
      {
        (object) importedNeedSyncTran.TranNumber
      });
    // ISSUE: reference to a compiler-generated field
    bool? released = cDisplayClass160.doc.Released;
    bool flag = false;
    if (released.GetValueOrDefault() == flag & released.HasValue)
      this.graph.Actions.PressSave();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass160.toProc = this.graph.Caches[cDisplayClass160.doc.GetType()].CreateCopy((object) cDisplayClass160.doc) as ICCPayment;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass160.tranCopy = (IExternalTransaction) null;
    if (transactionState.IsPreAuthorized && !ExternalTranHelper.IsExpired(transactionState.ExternalTransaction))
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass160.tranCopy = this.graph.Caches[transactionState.ExternalTransaction.GetType()].CreateCopy((object) transactionState.ExternalTransaction) as IExternalTransaction;
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass160.operation = cDisplayClass160.tranCopy != null ? CCTranType.PriorAuthorizedCapture : CCTranType.AuthorizeAndCapture;
    // ISSUE: method pointer
    PXLongOperation.StartOperation(this.graph, new PXToggleAsyncDelegate((object) cDisplayClass160, __methodptr(\u003CCaptureCCpayment\u003Eb__0)));
  }

  public void CaptureOnlyCCPayment(
    InputPaymentInfo paymentInfo,
    ICCPayment doc,
    IExternalTransactionAdapter paymentTransaction)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CCPaymentEntry.\u003C\u003Ec__DisplayClass17_0 cDisplayClass170 = new CCPaymentEntry.\u003C\u003Ec__DisplayClass17_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass170.paymentInfo = paymentInfo;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass170.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass170.doc = doc;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass170.doc == null || !cDisplayClass170.doc.CuryDocBal.HasValue)
      return;
    IEnumerable<IExternalTransaction> externalTransactions = paymentTransaction.Select();
    if (ExternalTranHelper.HasOpenCCProcTran(this.graph, externalTransactions.FirstOrDefault<IExternalTransaction>()))
      throw new PXException("This document has one or more transaction under processing.");
    ExternalTranHelper.GetActiveTransactionState(this.graph, externalTransactions);
    // ISSUE: reference to a compiler-generated field
    if (string.IsNullOrEmpty(cDisplayClass170.paymentInfo.AuthNumber))
      throw new PXException("Authorization Number, received from Processing Center is required for this type of transaction.");
    // ISSUE: reference to a compiler-generated field
    bool? released = cDisplayClass170.doc.Released;
    bool flag = false;
    if (released.GetValueOrDefault() == flag & released.HasValue)
      this.graph.Actions.PressSave();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass170.toProc = this.graph.Caches[cDisplayClass170.doc.GetType()].CreateCopy((object) cDisplayClass170.doc) as ICCPayment;
    // ISSUE: method pointer
    PXLongOperation.StartOperation(this.graph, new PXToggleAsyncDelegate((object) cDisplayClass170, __methodptr(\u003CCaptureOnlyCCPayment\u003Eb__0)));
  }

  public void VoidCCPayment(ICCPayment doc, IExternalTransactionAdapter paymentTransaction)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CCPaymentEntry.\u003C\u003Ec__DisplayClass18_0 cDisplayClass180 = new CCPaymentEntry.\u003C\u003Ec__DisplayClass18_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass180.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass180.doc = doc;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass180.doc == null)
      return;
    // ISSUE: reference to a compiler-generated field
    Decimal? nullable = cDisplayClass180.doc.CuryDocBal;
    if (!nullable.HasValue)
      return;
    IEnumerable<IExternalTransaction> externalTransactions = paymentTransaction.Select();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass180.state = !ExternalTranHelper.HasOpenCCProcTran(this.graph, externalTransactions.FirstOrDefault<IExternalTransaction>()) ? ExternalTranHelper.GetActiveTransactionState(this.graph, externalTransactions) : throw new PXException("This document has one or more transaction under processing.");
    // ISSUE: reference to a compiler-generated field
    if (!cDisplayClass180.state.IsActive)
      throw new PXException("There is no successful transaction to void.");
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass180.state.IsRefunded)
      throw new PXException("This type of transaction cannot be voided");
    // ISSUE: reference to a compiler-generated field
    if (ExternalTranHelper.IsExpired(cDisplayClass180.state.ExternalTransaction))
      throw new PXException("Transaction has already expired.");
    IExternalTransaction importedNeedSyncTran = ExternalTranHelper.GetImportedNeedSyncTran(this.graph, externalTransactions);
    if (importedNeedSyncTran != null)
      throw new PXException("The {0} transaction requires validation.", new object[1]
      {
        (object) importedNeedSyncTran.TranNumber
      });
    // ISSUE: reference to a compiler-generated field
    bool? released = cDisplayClass180.doc.Released;
    bool flag1 = false;
    if (released.GetValueOrDefault() == flag1 & released.HasValue)
    {
      IExternalTransaction activeTransaction = ExternalTranHelper.GetActiveTransaction(externalTransactions);
      if (activeTransaction?.ProcStatus == "CAS")
      {
        nullable = (Decimal?) activeTransaction?.Amount;
        // ISSUE: reference to a compiler-generated field
        Decimal num = Math.Abs(cDisplayClass180.doc.CuryDocBal.GetValueOrDefault());
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        {
          // ISSUE: reference to a compiler-generated field
          string documentName = TranValidationHelper.GetDocumentName(cDisplayClass180.doc.DocType);
          // ISSUE: reference to a compiler-generated field
          throw new PXException("The amount of the {0} {1} does not match the amount of the {2} original transaction. The Void action can be used if these amounts are equal.", new object[3]
          {
            (object) cDisplayClass180.doc.RefNbr,
            (object) documentName,
            (object) activeTransaction.TranNumber
          });
        }
      }
    }
    // ISSUE: reference to a compiler-generated field
    released = cDisplayClass180.doc.Released;
    bool flag2 = false;
    if (released.GetValueOrDefault() == flag2 & released.HasValue)
      this.graph.Actions.PressSave();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass180.toProc = this.graph.Caches[cDisplayClass180.doc.GetType()].CreateCopy((object) cDisplayClass180.doc) as ICCPayment;
    // ISSUE: method pointer
    PXLongOperation.StartOperation(this.graph, new PXToggleAsyncDelegate((object) cDisplayClass180, __methodptr(\u003CVoidCCPayment\u003Eb__0)));
  }

  public void CreditCCPayment(
    ICCPayment doc,
    IExternalTransactionAdapter paymentTransaction,
    string processingCenter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CCPaymentEntry.\u003C\u003Ec__DisplayClass19_0 cDisplayClass190 = new CCPaymentEntry.\u003C\u003Ec__DisplayClass19_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.doc = doc;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.processingCenter = processingCenter;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass190.doc == null || !cDisplayClass190.doc.CuryDocBal.HasValue)
      return;
    IEnumerable<IExternalTransaction> externalTransactions = paymentTransaction.Select();
    IExternalTransaction externalTransaction = !ExternalTranHelper.HasOpenCCProcTran(this.graph, externalTransactions.FirstOrDefault<IExternalTransaction>()) ? ExternalTranHelper.GetImportedNeedSyncTran(this.graph, externalTransactions) : throw new PXException("This document has one or more transaction under processing.");
    if (externalTransaction != null)
      throw new PXException("The {0} transaction requires validation.", new object[1]
      {
        (object) externalTransaction.TranNumber
      });
    // ISSUE: reference to a compiler-generated field
    bool? released = cDisplayClass190.doc.Released;
    bool flag = false;
    if (released.GetValueOrDefault() == flag & released.HasValue)
      this.graph.Actions.PressSave();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.toProc = this.graph.Caches[cDisplayClass190.doc.GetType()].CreateCopy((object) cDisplayClass190.doc) as ICCPayment;
    // ISSUE: method pointer
    PXLongOperation.StartOperation(this.graph, new PXToggleAsyncDelegate((object) cDisplayClass190, __methodptr(\u003CCreditCCPayment\u003Eb__0)));
  }

  public void RecordVoid(ICCPayment doc, TranRecordData tranRecord)
  {
    bool success = true;
    try
    {
      CCPaymentProcessingGraph instance = PXGraph.CreateInstance<CCPaymentProcessingGraph>();
      instance.Repository = (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository(this.graph)
      {
        NeedPersist = this.NeedPersistAfterRecord
      };
      this.SetResponseTextIfNeeded(tranRecord);
      instance.RecordVoid(doc, tranRecord);
    }
    catch
    {
      success = false;
      throw;
    }
    finally
    {
      if (this.AfterProcessingManager != null)
      {
        this.AfterProcessingManager.RunVoidActions((IBqlTable) doc, success);
        if (this.NeedPersistAfterRecord)
          this.AfterProcessingManager.PersistData();
      }
    }
  }

  public void RecordVoid(
    ICCPayment doc,
    TranRecordData tranRecord,
    IExternalTransactionAdapter paymentTransaction)
  {
    if (doc == null || !doc.CuryDocBal.HasValue)
      return;
    this.CommonRecordChecks(paymentTransaction, tranRecord);
    IEnumerable<IExternalTransaction> extTrans = paymentTransaction.Select();
    ExternalTransactionState transactionState = ExternalTranHelper.GetActiveTransactionState(this.graph, extTrans);
    if (!transactionState.IsActive)
      throw new PXException("There is no successful transaction to void.");
    if (transactionState.IsRefunded)
      throw new PXException("This type of transaction cannot be voided");
    if (ExternalTranHelper.IsExpired(transactionState.ExternalTransaction))
      throw new PXException("Transaction has already expired.");
    IExternalTransaction importedNeedSyncTran = ExternalTranHelper.GetImportedNeedSyncTran(this.graph, extTrans);
    if (importedNeedSyncTran != null)
      throw new PXException("The {0} transaction requires validation.", new object[1]
      {
        (object) importedNeedSyncTran.TranNumber
      });
    this.RecordVoid(doc, tranRecord);
  }

  public void RecordUnknown(
    ICCPayment doc,
    TranRecordData recordData,
    IExternalTransactionAdapter paymentTransaction)
  {
    if (doc == null || !doc.CuryDocBal.HasValue)
      return;
    IEnumerable<IExternalTransaction> externalTransactions = paymentTransaction.Select();
    IExternalTransaction externalTransaction = !ExternalTranHelper.HasOpenCCProcTran(this.graph, externalTransactions.FirstOrDefault<IExternalTransaction>()) ? ExternalTranHelper.GetImportedNeedSyncTran(this.graph, externalTransactions) : throw new PXException("This document has one or more transaction under processing.");
    if (externalTransaction != null)
      throw new PXException("The {0} transaction requires validation.", new object[1]
      {
        (object) externalTransaction.TranNumber
      });
    this.RecordUnknown(doc, recordData);
  }

  public void RecordUnknown(ICCPayment doc, TranRecordData tranRecord)
  {
    bool success = true;
    try
    {
      CCPaymentProcessingGraph instance = PXGraph.CreateInstance<CCPaymentProcessingGraph>();
      instance.Repository = (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository(this.graph)
      {
        NeedPersist = this.NeedPersistAfterRecord,
        KeepNewTranDeactivated = tranRecord.KeepNewTranDeactivated
      };
      this.SetResponseTextIfNeeded(tranRecord);
      instance.RecordUnknown(doc, tranRecord);
    }
    catch
    {
      success = false;
      throw;
    }
    finally
    {
      if (this.AfterProcessingManager != null)
      {
        this.AfterProcessingManager.RunUnknownActions((IBqlTable) doc, success);
        if (this.NeedPersistAfterRecord)
          this.AfterProcessingManager.PersistData();
      }
    }
  }

  public void RecordPriorAuthCapture(ICCPayment doc, TranRecordData tranRecord)
  {
    bool success = true;
    try
    {
      CCPaymentProcessingGraph instance = PXGraph.CreateInstance<CCPaymentProcessingGraph>();
      instance.Repository = (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository(this.graph)
      {
        NeedPersist = this.NeedPersistAfterRecord,
        KeepNewTranDeactivated = tranRecord.KeepNewTranDeactivated
      };
      this.SetResponseTextIfNeeded(tranRecord);
      instance.RecordPriorAuthorizedCapture(doc, tranRecord);
    }
    catch
    {
      success = false;
      throw;
    }
    finally
    {
      if (this.AfterProcessingManager != null)
      {
        this.AfterProcessingManager.RunPriorAuthorizedCaptureActions((IBqlTable) doc, success);
        if (this.NeedPersistAfterRecord)
          this.AfterProcessingManager.PersistData();
      }
    }
  }

  public void RecordPriorAuthCapture(
    ICCPayment doc,
    TranRecordData tranRecord,
    IExternalTransactionAdapter paymentTransaction)
  {
    if (doc == null || !doc.CuryDocBal.HasValue)
      return;
    this.CommonRecordChecks(paymentTransaction, tranRecord);
    IEnumerable<IExternalTransaction> extTrans = paymentTransaction.Select();
    if (ExternalTranHelper.GetActiveTransactionState(this.graph, extTrans).IsCaptured)
      throw new PXException("This payment has been captured already.");
    IExternalTransaction importedNeedSyncTran = ExternalTranHelper.GetImportedNeedSyncTran(this.graph, extTrans);
    if (importedNeedSyncTran != null)
      throw new PXException("The {0} transaction requires validation.", new object[1]
      {
        (object) importedNeedSyncTran.TranNumber
      });
    this.RecordPriorAuthCapture(doc, tranRecord);
  }

  public void RecordAuthCapture(ICCPayment doc, TranRecordData tranRecord)
  {
    bool success = true;
    try
    {
      CCPaymentProcessingGraph instance = PXGraph.CreateInstance<CCPaymentProcessingGraph>();
      instance.Repository = (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository(this.graph)
      {
        NeedPersist = this.NeedPersistAfterRecord
      };
      this.SetResponseTextIfNeeded(tranRecord);
      instance.RecordCapture(doc, tranRecord);
    }
    catch
    {
      success = false;
      throw;
    }
    finally
    {
      if (this.AfterProcessingManager != null)
      {
        this.AfterProcessingManager.RunCaptureActions((IBqlTable) doc, success);
        if (this.NeedPersistAfterRecord)
          this.AfterProcessingManager.PersistData();
      }
    }
  }

  public void RecordAuthCapture(
    ICCPayment doc,
    TranRecordData tranRecord,
    IExternalTransactionAdapter paymentTransaction)
  {
    if (doc == null || !doc.CuryDocBal.HasValue)
      return;
    this.CommonRecordChecks(paymentTransaction, tranRecord);
    if (ExternalTranHelper.GetActiveTransactionState(this.graph, paymentTransaction.Select()).IsCaptured)
      throw new PXException("This payment has been captured already.");
    this.RecordAuthCapture(doc, tranRecord);
  }

  public void RecordCaptureOnly(
    ICCPayment doc,
    TranRecordData tranRecord,
    IExternalTransactionAdapter paymentTransaction)
  {
    if (doc == null || !doc.CuryDocBal.HasValue)
      return;
    this.CommonRecordChecks(paymentTransaction, tranRecord);
    if (ExternalTranHelper.GetActiveTransactionState(this.graph, paymentTransaction.Select()).IsCaptured)
      throw new PXException("This payment has been captured already.");
    bool? released = doc.Released;
    bool flag = false;
    if (released.GetValueOrDefault() == flag & released.HasValue)
      this.graph.Actions.PressSave();
    this.RecordCaptureOnly(doc, tranRecord);
  }

  public void RecordCaptureOnly(ICCPayment doc, TranRecordData tranRecord)
  {
    bool success = true;
    try
    {
      CCPaymentProcessingGraph instance = PXGraph.CreateInstance<CCPaymentProcessingGraph>();
      instance.Repository = (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository(this.graph)
      {
        NeedPersist = this.NeedPersistAfterRecord
      };
      this.SetResponseTextIfNeeded(tranRecord);
      instance.RecordCaptureOnly(doc, tranRecord);
    }
    catch
    {
      success = false;
      throw;
    }
    finally
    {
      if (this.AfterProcessingManager != null)
      {
        this.AfterProcessingManager.RunCaptureOnlyActions((IBqlTable) doc, success);
        if (this.NeedPersistAfterRecord)
          this.AfterProcessingManager.PersistData();
      }
    }
  }

  public void RecordAuthorization(ICCPayment doc, TranRecordData tranRecord)
  {
    bool success = true;
    try
    {
      CCPaymentProcessingGraph instance = PXGraph.CreateInstance<CCPaymentProcessingGraph>();
      instance.Repository = (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository(this.graph)
      {
        NeedPersist = this.NeedPersistAfterRecord
      };
      this.SetResponseTextIfNeeded(tranRecord);
      instance.RecordAuthorization(doc, tranRecord);
    }
    catch
    {
      success = false;
      throw;
    }
    finally
    {
      if (this.AfterProcessingManager != null)
      {
        this.AfterProcessingManager.RunAuthorizeActions((IBqlTable) doc, success);
        if (this.NeedPersistAfterRecord)
          this.AfterProcessingManager.PersistData();
      }
    }
  }

  public void RecordAuthorization(
    ICCPayment doc,
    TranRecordData tranRecord,
    IExternalTransactionAdapter paymentTransaction)
  {
    if (doc == null || !doc.CuryDocBal.HasValue)
      return;
    this.CommonRecordChecks(paymentTransaction, tranRecord);
    ExternalTransactionState transactionState = ExternalTranHelper.GetActiveTransactionState(this.graph, paymentTransaction.Select());
    if (transactionState.IsCaptured)
      throw new PXException("This payment has been captured already.");
    if (transactionState.IsPreAuthorized)
      throw new PXException("This payment has been pre-authorized already.");
    this.RecordAuthorization(doc, tranRecord);
  }

  public void RecordCredit(ICCPayment doc, TranRecordData tranRecord)
  {
    bool success = true;
    try
    {
      CCPaymentProcessingGraph instance = PXGraph.CreateInstance<CCPaymentProcessingGraph>();
      instance.Repository = (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository(this.graph)
      {
        NeedPersist = this.NeedPersistAfterRecord,
        KeepNewTranDeactivated = tranRecord.KeepNewTranDeactivated
      };
      this.SetResponseTextIfNeeded(tranRecord);
      instance.RecordCredit(doc, tranRecord);
    }
    catch
    {
      success = false;
      throw;
    }
    finally
    {
      if (this.AfterProcessingManager != null)
      {
        this.AfterProcessingManager.RunCreditActions((IBqlTable) doc, success);
        if (this.NeedPersistAfterRecord)
          this.AfterProcessingManager.PersistData();
      }
    }
  }

  public void RecordCCCredit(
    ICCPayment doc,
    TranRecordData tranRecord,
    IExternalTransactionAdapter paymentTransaction)
  {
    if (doc == null || !doc.CuryDocBal.HasValue)
      return;
    this.CommonRecordChecks(paymentTransaction, tranRecord);
    IEnumerable<IExternalTransaction> extTrans = paymentTransaction.Select();
    if (ExternalTranHelper.GetActiveTransactionState(this.graph, extTrans).IsRefunded)
      throw new PXException("This payment has been refunded already.");
    IExternalTransaction importedNeedSyncTran = ExternalTranHelper.GetImportedNeedSyncTran(this.graph, extTrans);
    if (importedNeedSyncTran != null)
      throw new PXException("The {0} transaction requires validation.", new object[1]
      {
        (object) importedNeedSyncTran.TranNumber
      });
    this.RecordCredit(doc, tranRecord);
  }

  private void CommonRecordChecks(IExternalTransactionAdapter adapter, TranRecordData info)
  {
    if (ExternalTranHelper.HasOpenCCProcTran(this.graph, adapter.Select().FirstOrDefault<IExternalTransaction>()))
      throw new PXException("This document has one or more transaction under processing.");
    if (string.IsNullOrEmpty(info.ExternalTranId) && (string.IsNullOrEmpty(info.ExternalTranApiId) || !info.NeedSync))
      throw new PXException("A valid PC Transaction number of the original payment is required");
  }

  private void SetResponseTextIfNeeded(TranRecordData recordData)
  {
    if (recordData.ResponseText != null)
      return;
    recordData.ResponseText = "Imported External Transaction";
  }
}
