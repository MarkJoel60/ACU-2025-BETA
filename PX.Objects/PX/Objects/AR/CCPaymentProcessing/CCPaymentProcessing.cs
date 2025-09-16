// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.CCPaymentProcessing.Wrappers;
using PX.Objects.CA;
using PX.Objects.CC;
using PX.Objects.CC.GraphExtensions;
using PX.Objects.CC.Utility;
using PX.Objects.Extensions.PaymentTransaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing;

public class CCPaymentProcessing
{
  private ICCPaymentProcessingRepository _repository;
  private Func<object, CCProcessingContext, ICardTransactionProcessingWrapper> _transactionProcessingWrapper;
  private Func<object, ICardProcessingReadersProvider, IHostedPaymentFormProcessingWrapper> _hostedPaymentFormProcessingWrapper;

  public ICCPaymentProcessingRepository Repository => this._repository;

  public Func<object, ICardProcessingReadersProvider, IHostedPaymentFormProcessingWrapper> HostedPaymnetFormProcessingWrapper
  {
    get
    {
      return this._hostedPaymentFormProcessingWrapper == null ? (Func<object, ICardProcessingReadersProvider, IHostedPaymentFormProcessingWrapper>) ((plugin, provider) => HostedFromProcessingWrapper.GetPaymentFormProcessingWrapper(plugin, provider, (CCProcessingContext) null)) : this._hostedPaymentFormProcessingWrapper;
    }
    set => this._hostedPaymentFormProcessingWrapper = value;
  }

  public CCPaymentProcessing()
  {
    this._repository = CCPaymentProcessingRepository.GetCCPaymentProcessingRepository();
    this._transactionProcessingWrapper = (Func<object, CCProcessingContext, ICardTransactionProcessingWrapper>) ((plugin, context) => CardTransactionProcessingWrapper.GetTransactionProcessingWrapper(plugin, (ICardProcessingReadersProvider) new CardProcessingReadersProvider(context)));
  }

  public CCPaymentProcessing(PXGraph contextGraph)
  {
    this._repository = contextGraph != null ? (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository(contextGraph) : throw new ArgumentNullException(nameof (contextGraph));
    this._transactionProcessingWrapper = (Func<object, CCProcessingContext, ICardTransactionProcessingWrapper>) ((plugin, context) => CardTransactionProcessingWrapper.GetTransactionProcessingWrapper(plugin, (ICardProcessingReadersProvider) new CardProcessingReadersProvider(context)));
  }

  public CCPaymentProcessing(ICCPaymentProcessingRepository repo)
  {
    this._repository = repo ?? throw new ArgumentNullException(nameof (repo));
    this._transactionProcessingWrapper = (Func<object, CCProcessingContext, ICardTransactionProcessingWrapper>) ((plugin, context) => CardTransactionProcessingWrapper.GetTransactionProcessingWrapper(plugin, (ICardProcessingReadersProvider) new CardProcessingReadersProvider(context)));
  }

  public static ICCPaymentProcessor GetCCPaymentProcessing()
  {
    return (ICCPaymentProcessor) PXGraph.CreateInstance<CCPaymentProcessingGraph>();
  }

  public virtual bool TestCredentials(PXGraph callerGraph, string processingCenterID)
  {
    CCProcessingContext processingContext = new CCProcessingContext()
    {
      callerGraph = callerGraph
    };
    CCProcessingFeatureHelper.CheckProcessing(PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>.Config>.Select(callerGraph, new object[1]
    {
      (object) processingCenterID
    })), CCProcessingFeature.Base, processingContext);
    ICardTransactionProcessingWrapper processingWrapper = processingContext.processingCenter != null ? this.GetProcessingWrapper(processingContext) : throw new PXException("Processing center can't be found");
    APIResponse apiResponse1 = new APIResponse();
    APIResponse apiResponse2 = apiResponse1;
    processingWrapper.TestCredentials(apiResponse2);
    this.ProcessAPIResponse(apiResponse1);
    return apiResponse1.isSucess;
  }

  public virtual void ValidateSettings(
    PXGraph callerGraph,
    string processingCenterID,
    PluginSettingDetail settingDetail)
  {
    CCProcessingContext processingContext = new CCProcessingContext()
    {
      callerGraph = callerGraph
    };
    CCProcessingFeatureHelper.CheckProcessing(PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>.Config>.Select(callerGraph, new object[1]
    {
      (object) processingCenterID
    })), CCProcessingFeature.Base, processingContext);
    CCError ccError = processingContext.processingCenter != null ? this.GetProcessingWrapper(processingContext).ValidateSettings(settingDetail) : throw new PXException("Processing center can't be found");
    if (ccError.source != CCError.CCErrorSource.None)
      throw new PXSetPropertyException(ccError.ErrorMessage, (PXErrorLevel) 4);
  }

  public virtual IList<PluginSettingDetail> ExportSettings(
    PXGraph callerGraph,
    string processingCenterID)
  {
    CCProcessingContext processingContext = new CCProcessingContext()
    {
      callerGraph = callerGraph
    };
    CCProcessingFeatureHelper.CheckProcessing(PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>.Config>.Select(callerGraph, new object[1]
    {
      (object) processingCenterID
    })), CCProcessingFeature.Base, processingContext);
    ICardTransactionProcessingWrapper processingWrapper = processingContext.processingCenter != null ? this.GetProcessingWrapper(processingContext) : throw new PXException("Processing center can't be found");
    List<PluginSettingDetail> pluginSettingDetailList = new List<PluginSettingDetail>();
    List<PluginSettingDetail> aSettings = pluginSettingDetailList;
    processingWrapper.ExportSettings((IList<PluginSettingDetail>) aSettings);
    return (IList<PluginSettingDetail>) pluginSettingDetailList;
  }

  public virtual TranOperationResult Authorize(ICCPayment payment, bool aCapture)
  {
    CCProcessingCenter processingCenter;
    PX.Objects.AR.Customer customer;
    this.FindProcessingCenterAndCustomerByPayment(payment, out processingCenter, out customer);
    if (processingCenter.IsExternalAuthorizationOnly.GetValueOrDefault())
      throw new PXException("The {0} processing center does not support the Authorize action. The Capture action is supported only for payments that were pre-authorized externally.", new object[1]
      {
        (object) processingCenter.ProcessingCenterID
      });
    string paymentType = this._repository.GetPaymentMethod(payment.PaymentMethodID)?.PaymentType;
    if (paymentType == "POS" && !processingCenter.AcceptPOSPayments.GetValueOrDefault())
      throw new PXException("Payments from POS Terminals are disabled for the {0} processing center. To enable them, on the Processing Centers (CA205000) form, select the Accept Payments from POS Terminals check box.", new object[1]
      {
        (object) processingCenter.ProcessingCenterID
      });
    CCTranType ccTranType = aCapture ? CCTranType.AuthorizeAndCapture : CCTranType.AuthorizeOnly;
    CCProcTran ccProcTran = new CCProcTran();
    ccProcTran.Copy(payment);
    string externalTransactionId = this.CopyL2DataToProcTran(payment, ccProcTran)?.PreviousExternalTransactionID;
    bool flag = this.AuthorizeBasedOnPreviousTransaction(ccTranType, externalTransactionId, processingCenter);
    if (flag)
      ccProcTran.RefPCTranNumber = externalTransactionId;
    return this.DoTransaction(ccTranType, ccProcTran, flag ? externalTransactionId : (string) null, customer.AcctCD, paymentType);
  }

  public virtual TranOperationResult IncreaseAuthorizedAmount(
    ICCPayment payment,
    int? transactionId)
  {
    PX.Objects.AR.ExternalTransaction externalTransaction = PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, transactionId);
    this.CheckExternalTransactionNotNullAndValid(externalTransaction, transactionId);
    CCProcessingCenter processingCenter;
    PX.Objects.AR.Customer customer;
    this.FindProcessingCenterAndCustomerByPayment(payment, out processingCenter, out customer);
    this.CheckProcessingCenterNotNullAndActive(processingCenter);
    if (!processingCenter.AllowAuthorizedIncrement.GetValueOrDefault())
      throw new PXException("The {0} processing center does not support the Increase Authorized Amount action.", new object[1]
      {
        (object) processingCenter.ProcessingCenterID
      });
    if (this._repository.GetPaymentMethod(payment.PaymentMethodID)?.PaymentType == "POS" && !processingCenter.AcceptPOSPayments.GetValueOrDefault())
      throw new PXException("Payments from POS Terminals are disabled for the {0} processing center. To enable them, on the Processing Centers (CA205000) form, select the Accept Payments from POS Terminals check box.", new object[1]
      {
        (object) processingCenter.ProcessingCenterID
      });
    CCProcTran successfulCcProcTran = this.GetSuccessfulCCProcTran(externalTransaction);
    CCTranType aTranType = CCTranType.IncreaseAuthorizedAmount;
    CCProcTran ccProcTran = new CCProcTran();
    ccProcTran.Copy(payment);
    ccProcTran.RefTranNbr = successfulCcProcTran.TranNbr;
    ccProcTran.TransactionID = externalTransaction.TransactionID;
    ccProcTran.ExpirationDate = externalTransaction.ExpirationDate;
    this.CopyIncreaseDataToProcTran(payment, ccProcTran);
    this.CopyL2DataToProcTran(payment, ccProcTran);
    return this.DoTransaction(aTranType, ccProcTran, externalTransaction.TranNumber, customer.AcctCD);
  }

  public virtual TranOperationResult Capture(ICCPayment payment, int? transactionId)
  {
    PX.Objects.AR.ExternalTransaction externalTransaction = PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, transactionId);
    this.IsValidPmInstance(payment.PMInstanceID);
    this.CheckExternalTransactionNotNullAndValid(externalTransaction, transactionId);
    CCProcessingCenter processingCenter1;
    PX.Objects.AR.Customer customer1;
    this.GetProcCenterAndCustomer(payment, externalTransaction).Deconstruct<CCProcessingCenter, PX.Objects.AR.Customer>(out processingCenter1, out customer1);
    CCProcessingCenter processingCenter2 = processingCenter1;
    PX.Objects.AR.Customer customer2 = customer1;
    string paymentType = this._repository.GetPaymentMethod(payment.PaymentMethodID)?.PaymentType;
    if (paymentType == "POS" && !processingCenter2.AcceptPOSPayments.GetValueOrDefault())
      throw new PXException("Payments from POS Terminals are disabled for the {0} processing center. To enable them, on the Processing Centers (CA205000) form, select the Accept Payments from POS Terminals check box.", new object[1]
      {
        (object) processingCenter2.ProcessingCenterID
      });
    CCProcTran successfulCcProcTran = this.GetSuccessfulCCProcTran(externalTransaction);
    CCProcTran ccProcTran = new CCProcTran()
    {
      PMInstanceID = payment.PMInstanceID,
      RefTranNbr = successfulCcProcTran.TranNbr,
      TransactionID = externalTransaction.TransactionID,
      DocType = externalTransaction.DocType,
      RefNbr = externalTransaction.RefNbr,
      CuryID = payment.CuryID,
      Amount = payment.CuryDocBal,
      OrigDocType = externalTransaction.OrigDocType,
      OrigRefNbr = externalTransaction.OrigRefNbr,
      ProcessingCenterID = processingCenter2.ProcessingCenterID
    };
    this.CopyL2DataToProcTran(payment, ccProcTran);
    return this.DoTransaction(CCTranType.PriorAuthorizedCapture, ccProcTran, externalTransaction.TranNumber, customer2.AcctCD, paymentType);
  }

  public virtual TranOperationResult CaptureOnly(ICCPayment payment, string authNbr)
  {
    this.IsValidPmInstance(payment.PMInstanceID);
    PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod1;
    PX.Objects.AR.Customer customer1;
    this.FindCpmAndCustomer(payment.PMInstanceID).Deconstruct<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.Customer>(out customerPaymentMethod1, out customer1);
    PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod2 = customerPaymentMethod1;
    PX.Objects.AR.Customer customer2 = customer1;
    if (string.IsNullOrEmpty(authNbr))
      throw new PXException("Authorization Number, received from Processing Center is required for this type of transaction.", new object[1]
      {
        (object) authNbr
      });
    CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find(this._repository.Graph, customerPaymentMethod2.CCProcessingCenterID);
    this.CheckProcessingCenterNotNullAndActive(processingCenter);
    CCProcTran ccProcTran = new CCProcTran();
    ccProcTran.Copy(payment);
    this.CopyL2DataToProcTran(payment, ccProcTran);
    ccProcTran.ProcessingCenterID = processingCenter.ProcessingCenterID;
    return this.DoTransaction(CCTranType.CaptureOnly, ccProcTran, authNbr, customer2.AcctCD);
  }

  public virtual TranOperationResult Void(ICCPayment payment, int? transactionId)
  {
    PX.Objects.AR.ExternalTransaction externalTransaction = PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, transactionId);
    this.IsValidPmInstance(payment.PMInstanceID);
    CCProcTran aOrigTran = externalTransaction != null ? this.GetSuccessfulCCProcTran(externalTransaction) : throw new PXException("Transaction to be Void {0} is not found", new object[1]
    {
      (object) transactionId
    });
    if (!PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing.MayBeVoided(aOrigTran))
      throw new PXException("Transaction of {0} type cannot be voided", new object[1]
      {
        (object) transactionId
      });
    if (!externalTransaction.Active.GetValueOrDefault())
      throw new PXException("Transaction {0} failed authorization", new object[1]
      {
        (object) transactionId
      });
    CCProcessingCenter processingCenter1;
    PX.Objects.AR.Customer customer1;
    this.GetProcCenterAndCustomer(payment, externalTransaction).Deconstruct<CCProcessingCenter, PX.Objects.AR.Customer>(out processingCenter1, out customer1);
    CCProcessingCenter processingCenter2 = processingCenter1;
    PX.Objects.AR.Customer customer2 = customer1;
    string paymentType = this._repository.GetPaymentMethod(payment.PaymentMethodID)?.PaymentType;
    CCProcTran aTran = new CCProcTran()
    {
      PMInstanceID = payment.PMInstanceID,
      RefTranNbr = aOrigTran.TranNbr,
      TransactionID = externalTransaction.TransactionID,
      DocType = payment.DocType,
      RefNbr = payment.RefNbr,
      CuryID = aOrigTran.CuryID,
      Amount = externalTransaction.Amount,
      OrigDocType = externalTransaction.OrigDocType,
      OrigRefNbr = externalTransaction.OrigRefNbr,
      ProcessingCenterID = processingCenter2.ProcessingCenterID
    };
    if (externalTransaction.DocType != payment.DocType)
    {
      externalTransaction.VoidDocType = aTran.DocType;
      externalTransaction.VoidRefNbr = aTran.RefNbr;
    }
    return this.DoTransaction(CCTranType.Void, aTran, externalTransaction.TranNumber, customer2.AcctCD, paymentType);
  }

  public virtual TranOperationResult VoidOrCredit(ICCPayment payment, int? transactionId)
  {
    TranOperationResult tranOperationResult = this.Void(payment, transactionId);
    if (!tranOperationResult.Success && PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing.MayBeCredited(this.GetSuccessfulCCProcTran(PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, transactionId))))
      tranOperationResult = this.Credit(payment, transactionId, (string) null, new Decimal?());
    return tranOperationResult;
  }

  public virtual TranOperationResult Credit(
    ICCPayment payment,
    string extRefTranNbr,
    string procCenterId)
  {
    this.IsValidPmInstance(payment.PMInstanceID);
    PX.Objects.AR.ExternalTransaction externalTransaction = (PX.Objects.AR.ExternalTransaction) null;
    int? nullable1;
    int? nullable2;
    if (extRefTranNbr != null)
    {
      nullable1 = payment.PMInstanceID;
      nullable2 = PaymentTranExtConstants.NewPaymentProfile;
      externalTransaction = !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) ? this._repository.FindCapturedExternalTransaction(payment.PMInstanceID, extRefTranNbr) : this._repository.FindCapturedExternalTransaction(procCenterId, extRefTranNbr);
    }
    if (externalTransaction != null)
    {
      ICCPayment payment1 = payment;
      nullable2 = externalTransaction.TransactionID;
      int? transactionId = new int?(nullable2.Value);
      return this.Credit(payment1, transactionId);
    }
    if (extRefTranNbr == null)
    {
      nullable2 = payment.PMInstanceID;
      nullable1 = PaymentTranExtConstants.NewPaymentProfile;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      {
        CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find(this._repository.Graph, procCenterId);
        int num;
        if (processingCenter == null)
        {
          num = 0;
        }
        else
        {
          bool? allowUnlinkedRefund = processingCenter.AllowUnlinkedRefund;
          bool flag = false;
          num = allowUnlinkedRefund.GetValueOrDefault() == flag & allowUnlinkedRefund.HasValue ? 1 : 0;
        }
        if (num != 0)
          throw new PXException("The {0} card is associated with the {1} processing center that does not allow processing unlinked refunds. Select another card to process the unlinked refund.", new object[2]
          {
            (object) PX.Objects.AR.CustomerPaymentMethod.PK.Find(this._repository.Graph, payment.PMInstanceID).Descr,
            (object) procCenterId
          });
        goto label_17;
      }
    }
    if (extRefTranNbr == null && payment.TerminalID != null)
    {
      CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find(this._repository.Graph, procCenterId);
      int num;
      if (processingCenter == null)
      {
        num = 0;
      }
      else
      {
        bool? allowUnlinkedRefund = processingCenter.AllowUnlinkedRefund;
        bool flag = false;
        num = allowUnlinkedRefund.GetValueOrDefault() == flag & allowUnlinkedRefund.HasValue ? 1 : 0;
      }
      if (num != 0)
        throw new PXException("The {0} terminal is associated with the {1} processing center that does not allow processing unlinked refunds. Select another terminal to process the unlinked refund.", new object[2]
        {
          (object) CCProcessingCenterTerminal.PK.Find(this._repository.Graph, payment.TerminalID, procCenterId)?.DisplayName,
          (object) procCenterId
        });
    }
label_17:
    PX.Objects.AR.Customer customer = this.GetCustomer(payment);
    CCProcTran aTran = new CCProcTran();
    aTran.Copy(payment);
    CCProcTran ccProcTran = aTran;
    nullable1 = new int?();
    int? nullable3 = nullable1;
    ccProcTran.RefTranNbr = nullable3;
    aTran.RefPCTranNumber = extRefTranNbr;
    nullable1 = aTran.PMInstanceID;
    nullable2 = PaymentTranExtConstants.NewPaymentProfile;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      aTran.ProcessingCenterID = procCenterId;
    string paymentType = this._repository.GetPaymentMethod(payment.PaymentMethodID)?.PaymentType;
    return this.DoTransaction(CCTranType.Credit, aTran, extRefTranNbr, customer.AcctCD, paymentType);
  }

  public virtual TranOperationResult Credit(ICCPayment payment, int? transactionId)
  {
    this.IsValidPmInstance(payment.PMInstanceID);
    PX.Objects.AR.ExternalTransaction externalTransaction = PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, transactionId);
    CCProcTran aOrigTran = externalTransaction != null ? this.GetSuccessfulCCProcTran(externalTransaction) : throw new PXException("Transaction to be Credited {0} is not found", new object[1]
    {
      (object) transactionId
    });
    if (!PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing.MayBeCredited(aOrigTran))
      throw new PXException("Transaction {0} type cannot not be credited", new object[1]
      {
        (object) aOrigTran.TranType
      });
    if (!externalTransaction.Active.GetValueOrDefault())
      throw new PXException("Transaction {0} failed authorization", new object[1]
      {
        (object) transactionId
      });
    CCProcessingCenter processingCenter1;
    PX.Objects.AR.Customer customer1;
    this.GetProcCenterAndCustomer(payment, externalTransaction).Deconstruct<CCProcessingCenter, PX.Objects.AR.Customer>(out processingCenter1, out customer1);
    CCProcessingCenter processingCenter2 = processingCenter1;
    PX.Objects.AR.Customer customer2 = customer1;
    string paymentType = this._repository.GetPaymentMethod(payment.PaymentMethodID)?.PaymentType;
    CCProcTran aTran = new CCProcTran();
    aTran.Copy(payment);
    aTran.RefTranNbr = aOrigTran.TranNbr;
    aTran.ProcessingCenterID = processingCenter2.ProcessingCenterID;
    if (!payment.CuryDocBal.HasValue)
    {
      aTran.CuryID = aOrigTran.CuryID;
      aTran.Amount = aOrigTran.Amount;
    }
    return this.DoTransaction(CCTranType.Credit, aTran, externalTransaction.TranNumber, customer2.AcctCD, paymentType);
  }

  public virtual TranOperationResult Credit(
    ICCPayment payment,
    int? transactionId,
    string curyId,
    Decimal? amount)
  {
    this.IsValidPmInstance(payment.PMInstanceID);
    PX.Objects.AR.ExternalTransaction externalTransaction = PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, transactionId);
    CCProcTran aOrigTran = externalTransaction != null ? this.GetSuccessfulCCProcTran(externalTransaction) : throw new PXException("Transaction to be Credited {0} is not found", new object[1]
    {
      (object) transactionId
    });
    if (!PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing.MayBeCredited(aOrigTran))
      throw new PXException("Transaction {0} type cannot not be credited", new object[1]
      {
        (object) aOrigTran.TranType
      });
    if (!externalTransaction.Active.GetValueOrDefault())
      throw new PXException("Transaction {0} failed authorization", new object[1]
      {
        (object) transactionId
      });
    CCProcessingCenter processingCenter1;
    PX.Objects.AR.Customer customer1;
    this.GetProcCenterAndCustomer(payment, externalTransaction).Deconstruct<CCProcessingCenter, PX.Objects.AR.Customer>(out processingCenter1, out customer1);
    CCProcessingCenter processingCenter2 = processingCenter1;
    PX.Objects.AR.Customer customer2 = customer1;
    string paymentType = this._repository.GetPaymentMethod(payment.PaymentMethodID)?.PaymentType;
    CCProcTran aTran = new CCProcTran()
    {
      PMInstanceID = payment.PMInstanceID,
      DocType = payment.DocType,
      RefNbr = payment.RefNbr,
      OrigDocType = externalTransaction.OrigDocType,
      OrigRefNbr = externalTransaction.OrigRefNbr,
      ProcessingCenterID = processingCenter2.ProcessingCenterID,
      RefTranNbr = aOrigTran.TranNbr
    };
    if (amount.HasValue)
    {
      aTran.CuryID = curyId;
      aTran.Amount = amount;
    }
    else
    {
      aTran.CuryID = aOrigTran.CuryID;
      aTran.Amount = aOrigTran.Amount;
    }
    return this.DoTransaction(CCTranType.Credit, aTran, externalTransaction.TranNumber, customer2.AcctCD, paymentType);
  }

  private void ValidateRecordTran(ICCPayment payment, TranRecordData recordData)
  {
    int? pmInstanceId1 = payment.PMInstanceID;
    int? newPaymentProfile1 = PaymentTranExtConstants.NewPaymentProfile;
    CCProcessingCenter procCenter = !(pmInstanceId1.GetValueOrDefault() == newPaymentProfile1.GetValueOrDefault() & pmInstanceId1.HasValue == newPaymentProfile1.HasValue) ? this._repository.FindProcessingCenter(payment.PMInstanceID, payment.CuryID) : CCProcessingCenter.PK.Find(this._repository.Graph, recordData.ProcessingCenterId);
    if (procCenter == null || string.IsNullOrEmpty(procCenter.ProcessingTypeName))
      throw new PXException("Processing center for this card type is not configured properly.");
    if (recordData.ProcessingCenterId != null)
    {
      int? pmInstanceId2 = payment.PMInstanceID;
      int? newPaymentProfile2 = PaymentTranExtConstants.NewPaymentProfile;
      if (!(pmInstanceId2.GetValueOrDefault() == newPaymentProfile2.GetValueOrDefault() & pmInstanceId2.HasValue == newPaymentProfile2.HasValue) && recordData.ProcessingCenterId != procCenter.ProcessingCenterID)
      {
        PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PX.Objects.AR.CustomerPaymentMethod.PK.Find(this._repository.Graph, payment.PMInstanceID);
        throw new PXException("The {0} processing center is not associated with the {1} customer payment method.", new object[2]
        {
          (object) recordData.ProcessingCenterId,
          (object) customerPaymentMethod?.Descr
        });
      }
    }
    this.CheckProcCenterCashAccountCury(procCenter, payment.CuryID);
  }

  private void ValidateRelativelyRefTran(
    ICCPayment payment,
    TranRecordData recordData,
    CCProcTran refProcTran,
    CCProcTran historyTran)
  {
    string trimValue1 = this.GetTrimValue(refProcTran.DocType);
    string trimValue2 = this.GetTrimValue(refProcTran.RefNbr);
    string trimValue3 = this.GetTrimValue(refProcTran.OrigDocType);
    string trimValue4 = this.GetTrimValue(refProcTran.OrigRefNbr);
    string str1 = (string) null;
    string str2 = (string) null;
    if (historyTran != null)
    {
      str1 = this.GetTrimValue(historyTran.DocType);
      str2 = this.GetTrimValue(historyTran.RefNbr);
    }
    string trimValue5 = this.GetTrimValue(payment.DocType);
    string trimValue6 = this.GetTrimValue(payment.RefNbr);
    string trimValue7 = this.GetTrimValue(payment.OrigDocType);
    string trimValue8 = this.GetTrimValue(payment.OrigRefNbr);
    string str3 = trimValue5;
    if ((!(trimValue1 != str3) && !(trimValue2 != trimValue6) || !(str1 != trimValue5) && !(str2 != trimValue6)) && (trimValue7 == null || !(trimValue3 != trimValue7) && !(trimValue4 != trimValue8)))
    {
      int? pmInstanceId1 = payment.PMInstanceID;
      int? pmInstanceId2 = refProcTran.PMInstanceID;
      if (pmInstanceId1.GetValueOrDefault() == pmInstanceId2.GetValueOrDefault() & pmInstanceId1.HasValue == pmInstanceId2.HasValue)
        return;
      pmInstanceId2 = refProcTran.PMInstanceID;
      int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
      if (pmInstanceId2.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId2.HasValue == newPaymentProfile.HasValue)
        return;
    }
    TranValidationHelper.AdditionalParams validationHelperParamsObj = this.GetValidationHelperParamsObj(payment, recordData);
    throw new PXException(TranValidationHelper.GenerateTranAlreadyRecordedErrMsg(this.GetNonEmptyProcCenterTranNumber(recordData), refProcTran, validationHelperParamsObj));
  }

  private TranValidationHelper.AdditionalParams GetValidationHelperParamsObj(
    ICCPayment payment,
    TranRecordData recordData)
  {
    return new TranValidationHelper.AdditionalParams()
    {
      PMInstanceId = payment.PMInstanceID,
      ProcessingCenter = recordData.ProcessingCenterId,
      Repo = this.Repository
    };
  }

  private string GetTrimValue(string input) => input?.Trim();

  private CCProcTran FormatCCProcTran(ICCPayment payment, TranRecordData recordData)
  {
    PXResult<PX.Objects.AR.ExternalTransaction, CCProcTran> pxResult = (PXResult<PX.Objects.AR.ExternalTransaction, CCProcTran>) PXResultset<PX.Objects.AR.ExternalTransaction>.op_Implicit(PXSelectBase<PX.Objects.AR.ExternalTransaction, PXViewOf<PX.Objects.AR.ExternalTransaction>.BasedOn<SelectFromBase<PX.Objects.AR.ExternalTransaction, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CCProcTran>.On<BqlOperand<PX.Objects.AR.ExternalTransaction.transactionID, IBqlInt>.IsEqual<CCProcTran.transactionID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ExternalTransaction.noteID, Equal<P.AsGuid>>>>>.And<BqlOperand<CCProcTran.procStatus, IBqlString>.IsEqual<CCProcStatus.opened>>>.Order<By<BqlField<CCProcTran.tranNbr, IBqlInt>.Desc>>>.Config>.Select(this._repository.Graph, new object[1]
    {
      (object) recordData.TranUID
    }));
    CCProcTran ccProcTran = new CCProcTran();
    if (pxResult != null)
      ccProcTran = PXResult<PX.Objects.AR.ExternalTransaction, CCProcTran>.op_Implicit(pxResult);
    ccProcTran.PMInstanceID = payment.PMInstanceID;
    ccProcTran.OrigDocType = payment.OrigDocType;
    ccProcTran.DocType = payment.DocType;
    ccProcTran.TranStatus = recordData.TranStatus;
    ccProcTran.AuthNumber = recordData.AuthCode;
    ccProcTran.CuryID = payment.CuryID;
    ccProcTran.Amount = recordData.Amount ?? payment.CuryDocBal;
    ccProcTran.CVVVerificationStatus = recordData.CvvVerificationCode;
    ccProcTran.StartTime = recordData.TransactionDate;
    ccProcTran.EndTime = recordData.TransactionDate;
    ccProcTran.Imported = new bool?(recordData.Imported);
    ccProcTran.PCResponseReasonCode = recordData.ResponseCode;
    ccProcTran.PCResponseReasonText = recordData.ResponseText;
    ccProcTran.PCTranNumber = recordData.ExternalTranId;
    ccProcTran.PCTranApiNumber = recordData.ExternalTranApiId;
    ccProcTran.CommerceTranNumber = recordData.CommerceTranNumber;
    ccProcTran.SubtotalAmount = new Decimal?(recordData.Subtotal.GetValueOrDefault());
    ccProcTran.Tax = new Decimal?(recordData.Tax.GetValueOrDefault());
    ccProcTran.TerminalID = recordData.TerminalID;
    if (!recordData.NewDoc)
    {
      ccProcTran.OrigRefNbr = payment.OrigRefNbr;
      ccProcTran.RefNbr = payment.RefNbr;
    }
    return ccProcTran;
  }

  private void CheckProcCenterCashAccountCury(CCProcessingCenter procCenter, string curyId)
  {
    PX.Objects.CA.CashAccount cashAccount = PX.Objects.CA.CashAccount.PK.Find(this._repository.Graph, procCenter.CashAccountID);
    if (cashAccount.CuryID != curyId)
      throw new PXException("The currency of the transaction ({0}) differs from the currency of the processing center ({1}).", new object[2]
      {
        (object) curyId,
        (object) cashAccount.CuryID
      });
  }

  private CCPayLink GetPayLinkByExternalId(string externalId, string procCenter)
  {
    return PXResultset<CCPayLink>.op_Implicit(PXSelectBase<CCPayLink, PXSelect<CCPayLink, Where<CCPayLink.processingCenterID, Equal<Required<CCPayLink.processingCenterID>>, And<CCPayLink.externalID, Equal<Required<CCPayLink.externalID>>>>>.Config>.Select(this._repository.Graph, new object[2]
    {
      (object) procCenter,
      (object) externalId
    }));
  }

  private PX.Objects.AR.Customer GetCustomerFromDoc(ICCPayment payment)
  {
    PX.Objects.AR.Customer customerFromDoc = (PX.Objects.AR.Customer) null;
    if (((IEnumerable<string>) ARDocType.Values).Any<string>((Func<string, bool>) (i => payment.DocType == i)))
      customerFromDoc = ((PXSelectBase<PX.Objects.AR.Customer>) new PXSelectJoin<PX.Objects.AR.Customer, InnerJoin<ARRegister, On<ARRegister.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where<ARRegister.docType, Equal<Required<ARRegister.docType>>, And<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>>>>(this._repository.Graph)).SelectSingle(new object[2]
      {
        (object) payment.DocType,
        (object) payment.RefNbr
      });
    return customerFromDoc;
  }

  private PX.Objects.AR.Customer GetCustomer(ICCPayment payment)
  {
    int? pmInstanceId = payment.PMInstanceID;
    int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
    return !(pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue) ? this.FindCpmAndCustomer(payment.PMInstanceID).Item2 : this.GetCustomerFromDoc(payment);
  }

  private Tuple<CCProcessingCenter, PX.Objects.AR.Customer> GetProcCenterAndCustomer(
    ICCPayment payment,
    PX.Objects.AR.ExternalTransaction transaction)
  {
    int? pmInstanceId = payment.PMInstanceID;
    int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
    PX.Objects.AR.Customer customerFromDoc;
    string processingCenterId;
    CCProcessingCenter processingCenter;
    if (pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue)
    {
      CCProcTran successfulCcProcTran = this.GetSuccessfulCCProcTran(transaction);
      customerFromDoc = this.GetCustomerFromDoc(payment);
      processingCenterId = successfulCcProcTran.ProcessingCenterID;
      processingCenter = CCProcessingCenter.PK.Find(this._repository.Graph, successfulCcProcTran.ProcessingCenterID);
      if (processingCenter == null)
        throw new PXException("Processing center {0}, specified in referenced transaction {1} can't be found", new object[2]
        {
          (object) processingCenterId,
          (object) transaction.TransactionID
        });
    }
    else
    {
      Tuple<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.Customer> cpmAndCustomer = this.FindCpmAndCustomer(payment.PMInstanceID);
      customerFromDoc = cpmAndCustomer.Item2;
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = cpmAndCustomer.Item1;
      processingCenterId = customerPaymentMethod.CCProcessingCenterID;
      processingCenter = CCProcessingCenter.PK.Find(this._repository.Graph, processingCenterId);
      if (processingCenter == null)
        throw new PXException("Processing center for payment method {0} is not specified", new object[1]
        {
          (object) customerPaymentMethod.Descr
        });
    }
    return processingCenter.IsActive.GetValueOrDefault() ? new Tuple<CCProcessingCenter, PX.Objects.AR.Customer>(processingCenter, customerFromDoc) : throw new PXException("Processing center {0} is inactive", new object[1]
    {
      (object) processingCenterId
    });
  }

  /// <summary>
  /// After successful operation the property <see cref="P:PX.Objects.AR.CCPaymentProcessing.Common.TranRecordData.InnerTranId" /> stores <see cref="P:PX.Objects.AR.ExternalTransaction.TransactionID" /> value.
  /// This value is not used in the client code, but could be processed by customizations.
  /// </summary>
  public virtual bool RecordAuthorization(ICCPayment payment, TranRecordData recordData)
  {
    bool flag = false;
    CCProcTran ccProcTran = this.PrepeareRecord(payment, recordData);
    ccProcTran.ExpirationDate = recordData.ExpirationDate;
    int? nullable1 = new int?();
    int? nullable2;
    if (!ccProcTran.TransactionID.HasValue)
    {
      PX.Objects.AR.ExternalTransaction externalTransaction = new PX.Objects.AR.ExternalTransaction();
      this.PopulateExtTranFromTranRecordObj(externalTransaction, recordData);
      if (recordData.Level3Support)
        Level3Helper.SetL3StatusExternalTransaction(externalTransaction, new L3TranStatus?(), (string) null);
      nullable2 = this.RecordTransaction(CCTranType.AuthorizeOnly, ccProcTran, externalTransaction);
    }
    else
    {
      this.UpdateExtTranInfo(ccProcTran, recordData);
      nullable2 = this.RecordTransaction(CCTranType.AuthorizeOnly, ccProcTran);
    }
    if (nullable2.HasValue)
    {
      recordData.InnerTranId = nullable2;
      flag = true;
    }
    return flag;
  }

  /// <summary>
  /// After successful operation the property <see cref="P:PX.Objects.AR.CCPaymentProcessing.Common.TranRecordData.InnerTranId" /> stores <see cref="P:PX.Objects.AR.ExternalTransaction.TransactionID" /> value.
  /// This value is not used in the client code, but could be processed by customizations.
  /// </summary>
  public virtual bool RecordCapture(ICCPayment payment, TranRecordData recordData)
  {
    bool flag = false;
    int? nullable1 = new int?();
    CCProcTran ccProcTran = this.PrepeareRecord(payment, recordData);
    ccProcTran.ExpirationDate = recordData.ExpirationDate;
    int? nullable2;
    if (!ccProcTran.TransactionID.HasValue)
    {
      PX.Objects.AR.ExternalTransaction externalTransaction = new PX.Objects.AR.ExternalTransaction();
      this.PopulateExtTranFromTranRecordObj(externalTransaction, recordData);
      if (recordData.Level3Support)
        Level3Helper.SetL3StatusExternalTransaction(externalTransaction, new L3TranStatus?(), (string) null);
      nullable2 = this.RecordTransaction(CCTranType.AuthorizeAndCapture, ccProcTran, externalTransaction);
    }
    else
    {
      this.UpdateExtTranInfo(ccProcTran, recordData);
      nullable2 = this.RecordTransaction(CCTranType.AuthorizeAndCapture, ccProcTran);
    }
    if (nullable2.HasValue)
    {
      recordData.InnerTranId = nullable2;
      flag = true;
    }
    return flag;
  }

  /// <summary>
  /// After successful operation the property <see cref="P:PX.Objects.AR.CCPaymentProcessing.Common.TranRecordData.InnerTranId" /> stores <see cref="P:PX.Objects.AR.ExternalTransaction.TransactionID" /> value.
  /// This value is not used in the client code, but could be processed by customizations.
  /// </summary>
  public virtual bool RecordPriorAuthorizedCapture(ICCPayment payment, TranRecordData recordData)
  {
    bool flag = false;
    CCProcTran tran = this.PrepeareRecord(payment, recordData);
    int? nullable1 = new int?();
    PX.Objects.AR.ExternalTransaction externalTransaction = tran.TransactionID.HasValue ? PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, tran.TransactionID) : new PX.Objects.AR.ExternalTransaction();
    this.PopulateExtTranFromTranRecordObj(externalTransaction, recordData);
    if (recordData.Level3Support)
      Level3Helper.SetL3StatusExternalTransaction(externalTransaction, new L3TranStatus?(), (string) null);
    int? nullable2 = this.RecordTransaction(CCTranType.PriorAuthorizedCapture, tran, externalTransaction);
    if (nullable2.HasValue)
    {
      recordData.InnerTranId = nullable2;
      flag = true;
    }
    return flag;
  }

  /// <summary>
  /// After successful operation the property <see cref="P:PX.Objects.AR.CCPaymentProcessing.Common.TranRecordData.InnerTranId" /> stores <see cref="P:PX.Objects.AR.ExternalTransaction.TransactionID" /> value.
  /// This value is not used in the client code, but could be processed by customizations.
  /// </summary>
  public virtual bool RecordVoid(ICCPayment payment, TranRecordData recordData)
  {
    bool flag = false;
    int? nullable1 = new int?();
    CCProcTran tran = this.PrepeareRecord(payment, recordData);
    PX.Objects.AR.ExternalTransaction extTran = tran.TransactionID.HasValue ? PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, tran.TransactionID) : new PX.Objects.AR.ExternalTransaction();
    if (recordData.AllowFillVoidRef && tran.TransactionID.HasValue)
    {
      if (extTran.VoidDocType != null && (extTran.VoidDocType != payment.DocType || extTran.VoidRefNbr != payment.RefNbr))
      {
        TranValidationHelper.AdditionalParams validationHelperParamsObj = this.GetValidationHelperParamsObj(payment, recordData);
        throw new PXException(TranValidationHelper.GenerateTranAlreadyRecordedErrMsg(extTran.TranNumber, extTran.VoidDocType, extTran.RefNbr, validationHelperParamsObj));
      }
      extTran.VoidDocType = payment.DocType;
      extTran.VoidRefNbr = payment.RefNbr;
    }
    this.PopulateExtTranFromTranRecordObj(extTran, recordData);
    int? nullable2 = this.RecordTransaction(recordData.TranType == "REJ" ? CCTranType.Reject : CCTranType.Void, tran, extTran);
    if (nullable2.HasValue)
    {
      recordData.InnerTranId = nullable2;
      flag = true;
    }
    return flag;
  }

  /// <summary>
  /// After successful operation the property <see cref="P:PX.Objects.AR.CCPaymentProcessing.Common.TranRecordData.InnerTranId" /> stores <see cref="P:PX.Objects.AR.ExternalTransaction.TransactionID" /> value.
  /// This value is not used in the client code, but could be processed by customizations.
  /// </summary>
  public virtual bool RecordCaptureOnly(ICCPayment payment, TranRecordData recordData)
  {
    bool flag = false;
    int? nullable = this.RecordTransaction(CCTranType.CaptureOnly, this.PrepeareRecord(payment, recordData));
    if (nullable.HasValue)
    {
      recordData.InnerTranId = nullable;
      flag = true;
    }
    return flag;
  }

  public virtual bool RecordUnknown(ICCPayment payment, TranRecordData recordData)
  {
    bool flag = false;
    CCProcTran tran = this.PrepeareRecord(payment, recordData);
    int? nullable1 = new int?();
    PX.Objects.AR.ExternalTransaction extTran = tran.TransactionID.HasValue ? PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, tran.TransactionID) : new PX.Objects.AR.ExternalTransaction();
    if (recordData.AllowFillVoidRef && tran.TransactionID.HasValue)
    {
      if (extTran.VoidDocType != null && (extTran.VoidDocType != payment.DocType || extTran.VoidRefNbr != payment.RefNbr))
      {
        TranValidationHelper.AdditionalParams validationHelperParamsObj = this.GetValidationHelperParamsObj(payment, recordData);
        throw new PXException(TranValidationHelper.GenerateTranAlreadyRecordedErrMsg(extTran.TranNumber, extTran.VoidDocType, extTran.RefNbr, validationHelperParamsObj));
      }
      extTran.VoidDocType = payment.DocType;
      extTran.VoidRefNbr = payment.RefNbr;
    }
    this.PopulateExtTranFromTranRecordObj(extTran, recordData);
    int? nullable2 = this.RecordTransaction(CCTranType.Unknown, tran, extTran);
    if (nullable2.HasValue)
    {
      recordData.InnerTranId = nullable2;
      flag = true;
    }
    return flag;
  }

  /// <summary>
  /// After successful operation the property <see cref="P:PX.Objects.AR.CCPaymentProcessing.Common.TranRecordData.InnerTranId" /> stores <see cref="P:PX.Objects.AR.ExternalTransaction.TransactionID" /> value.
  /// This value is not used in the client code, but could be processed by customizations.
  /// </summary>
  public virtual bool RecordCredit(ICCPayment payment, TranRecordData recordData)
  {
    PX.Objects.AR.ExternalTransaction externalTransaction = (PX.Objects.AR.ExternalTransaction) null;
    int? nullable1;
    int? nullable2;
    if (!string.IsNullOrEmpty(recordData.RefExternalTranId))
    {
      nullable1 = payment.PMInstanceID;
      nullable2 = PaymentTranExtConstants.NewPaymentProfile;
      externalTransaction = !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) ? this._repository.FindCapturedExternalTransaction(payment.PMInstanceID, recordData.RefExternalTranId) : this._repository.FindCapturedExternalTransaction(recordData.ProcessingCenterId, recordData.RefExternalTranId);
    }
    string str = (string) null;
    int? nullable3 = new int?();
    if (externalTransaction != null)
    {
      nullable2 = payment.PMInstanceID;
      nullable1 = externalTransaction.PMInstanceID;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      {
        nullable3 = this._repository.GetCCProcTranByTranID(externalTransaction.TransactionID).Where<CCProcTran>((Func<CCProcTran, bool>) (i => i.TranType == "AAC" || i.TranType == "PAC")).Select<CCProcTran, int?>((Func<CCProcTran, int?>) (i => i.TranNbr)).FirstOrDefault<int?>();
        goto label_7;
      }
    }
    if (!string.IsNullOrEmpty(recordData.RefExternalTranId))
      str = recordData.RefExternalTranId;
label_7:
    CCProcTran procTran = this.PrepeareRecord(payment, recordData);
    procTran.TranType = "CDT";
    procTran.ProcStatus = "FIN";
    procTran.RefPCTranNumber = str;
    nullable1 = procTran.RefTranNbr;
    if (!nullable1.HasValue)
      procTran.RefTranNbr = nullable3;
    nullable1 = procTran.TransactionID;
    PX.Objects.AR.ExternalTransaction extTran1;
    if (!nullable1.HasValue)
    {
      extTran1 = new PX.Objects.AR.ExternalTransaction();
      extTran1.NoteID = recordData.TranUID;
      extTran1.LastDigits = string.Empty;
    }
    else
    {
      PX.Objects.AR.ExternalTransaction extTran2 = PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, procTran.TransactionID);
      nullable1 = extTran2.ParentTranID;
      if (!nullable1.HasValue && externalTransaction != null)
        extTran2.ParentTranID = externalTransaction.TransactionID;
      extTran1 = this._repository.UpdateExternalTransaction(extTran2);
    }
    extTran1.NeedSync = new bool?(recordData.NeedSync);
    extTran1.ExtProfileId = recordData.ExtProfileId;
    this.SetCardTypeValue(extTran1, recordData.CardType, recordData.ProcCenterCardTypeCode);
    CCProcTran ccProcTran = this._repository.InsertOrUpdateTransaction(procTran, extTran1);
    recordData.InnerTranId = ccProcTran.TranNbr;
    return true;
  }

  public virtual CCProcTran PrepeareRecord(ICCPayment payment, TranRecordData recordData)
  {
    if (recordData.ValidateDoc)
    {
      this.IsValidPmInstance(payment.PMInstanceID);
      this.ValidateRecordTran(payment, recordData);
    }
    int? pmInstanceId1 = payment.PMInstanceID;
    int? nullable1 = PaymentTranExtConstants.NewPaymentProfile;
    CCProcessingCenter processingCenter = !(pmInstanceId1.GetValueOrDefault() == nullable1.GetValueOrDefault() & pmInstanceId1.HasValue == nullable1.HasValue) ? this._repository.FindProcessingCenter(payment.PMInstanceID, payment.CuryID) : CCProcessingCenter.PK.Find(this._repository.Graph, recordData.ProcessingCenterId);
    CCProcTran tran = this.FormatCCProcTran(payment, recordData);
    tran.ProcessingCenterID = processingCenter.ProcessingCenterID;
    if (!string.IsNullOrEmpty(recordData.ExternalTranId) || !string.IsNullOrEmpty(recordData.ExternalTranApiId))
    {
      PXSelectJoin<CCProcTran, InnerJoin<PX.Objects.AR.ExternalTransaction, On<CCProcTran.transactionID, Equal<PX.Objects.AR.ExternalTransaction.transactionID>>>, Where<Not<PX.Objects.AR.ExternalTransaction.syncStatus, Equal<CCSyncStatusCode.error>, And<PX.Objects.AR.ExternalTransaction.active, Equal<False>>>>, OrderBy<Desc<CCProcTran.tranNbr>>> pxSelectJoin = new PXSelectJoin<CCProcTran, InnerJoin<PX.Objects.AR.ExternalTransaction, On<CCProcTran.transactionID, Equal<PX.Objects.AR.ExternalTransaction.transactionID>>>, Where<Not<PX.Objects.AR.ExternalTransaction.syncStatus, Equal<CCSyncStatusCode.error>, And<PX.Objects.AR.ExternalTransaction.active, Equal<False>>>>, OrderBy<Desc<CCProcTran.tranNbr>>>(this._repository.Graph);
      nullable1 = recordData.RefInnerTranId;
      PXResultset<CCProcTran> pxResultset;
      if (nullable1.HasValue)
      {
        ((PXSelectBase<CCProcTran>) pxSelectJoin).WhereAnd<Where<PX.Objects.AR.ExternalTransaction.transactionID, Equal<Required<PX.Objects.AR.ExternalTransaction.transactionID>>>>();
        pxResultset = ((PXSelectBase<CCProcTran>) pxSelectJoin).Select(new object[1]
        {
          (object) recordData.RefInnerTranId
        });
      }
      else if (!string.IsNullOrEmpty(recordData.ExternalTranId))
      {
        ((PXSelectBase<CCProcTran>) pxSelectJoin).WhereAnd<Where<PX.Objects.AR.ExternalTransaction.tranNumber, Equal<Required<PX.Objects.AR.ExternalTransaction.tranNumber>>>>();
        pxResultset = ((PXSelectBase<CCProcTran>) pxSelectJoin).Select(new object[1]
        {
          (object) recordData.ExternalTranId
        });
      }
      else
      {
        ((PXSelectBase<CCProcTran>) pxSelectJoin).WhereAnd<Where<PX.Objects.AR.ExternalTransaction.tranApiNumber, Equal<Required<PX.Objects.AR.ExternalTransaction.tranApiNumber>>>>();
        pxResultset = ((PXSelectBase<CCProcTran>) pxSelectJoin).Select(new object[1]
        {
          (object) recordData.ExternalTranApiId
        });
      }
      IEnumerable<CCProcTran> ccProcTrans = GraphHelper.RowCast<CCProcTran>((IEnumerable) pxResultset);
      IEnumerable<PX.Objects.AR.ExternalTransaction> extTrans = GraphHelper.RowCast<PX.Objects.AR.ExternalTransaction>((IEnumerable) pxResultset);
      CCProcTran ccProcTran1 = (CCProcTran) null;
      nullable1 = tran.PMInstanceID;
      int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
      if (!(nullable1.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & nullable1.HasValue == newPaymentProfile.HasValue))
        ccProcTran1 = ccProcTrans.Where<CCProcTran>((Func<CCProcTran, bool>) (i =>
        {
          int? pmInstanceId2 = i.PMInstanceID;
          int? pmInstanceId3 = tran.PMInstanceID;
          return pmInstanceId2.GetValueOrDefault() == pmInstanceId3.GetValueOrDefault() & pmInstanceId2.HasValue == pmInstanceId3.HasValue;
        })).FirstOrDefault<CCProcTran>();
      if (ccProcTran1 == null)
        ccProcTran1 = ccProcTrans.Where<CCProcTran>((Func<CCProcTran, bool>) (i => i.ProcessingCenterID == tran.ProcessingCenterID)).FirstOrDefault<CCProcTran>();
      if (ccProcTran1 != null)
      {
        int? tranNbr = ccProcTran1.TranNbr;
        nullable1 = tran.TranNbr;
        if (!(tranNbr.GetValueOrDefault() == nullable1.GetValueOrDefault() & tranNbr.HasValue == nullable1.HasValue))
        {
          if (!recordData.AllowFillVoidRef)
          {
            CCProcTran otherDocInHistory = this.GetProcTranFromOtherDocInHistory(ccProcTran1, extTrans, ccProcTrans);
            this.ValidateRelativelyRefTran(payment, recordData, ccProcTran1, otherDocInHistory);
          }
          CCProcTran ccProcTran2 = tran;
          int? nullable2;
          if (ccProcTran1 == null)
          {
            nullable1 = new int?();
            nullable2 = nullable1;
          }
          else
            nullable2 = ccProcTran1.TranNbr;
          ccProcTran2.RefTranNbr = nullable2;
        }
      }
    }
    nullable1 = tran.RefTranNbr;
    if (nullable1.HasValue)
    {
      CCProcTran ccProcTran = CCProcTran.PK.Find(this._repository.Graph, tran.RefTranNbr);
      nullable1 = tran.TransactionID;
      if (!nullable1.HasValue)
        tran.TransactionID = ccProcTran.TransactionID;
      tran.OrigDocType = ccProcTran.OrigDocType;
      tran.OrigRefNbr = ccProcTran.OrigRefNbr;
    }
    return tran;
  }

  private void CheckProcessingCenter(CCProcessingCenter procCenter)
  {
    CCPluginTypeHelper.GetPluginTypeWithCheck(procCenter);
  }

  public static bool MayBeVoided(CCProcTran aOrigTran)
  {
    string tranType = aOrigTran.TranType;
    return tranType == "AUT" || tranType == "IAA" || tranType == "AAC" || tranType == "PAC" || tranType == "CAP";
  }

  public static bool MayBeCredited(CCProcTran aOrigTran)
  {
    string tranType = aOrigTran.TranType;
    return tranType == "AAC" || tranType == "PAC" || tranType == "CAP";
  }

  public static bool IsExpired(PX.Objects.AR.CustomerPaymentMethod aPMInstance)
  {
    return aPMInstance.ExpirationDate.HasValue && aPMInstance.ExpirationDate.Value < DateTime.Now;
  }

  protected static void FillRecordedTran(CCProcTran tran, string aReasonText = "Imported External Transaction")
  {
    tran.PCResponseReasonText = aReasonText;
    tran.TranNbr = new int?();
    DateTime? nullable1 = tran.StartTime;
    if (!nullable1.HasValue)
    {
      CCProcTran ccProcTran1 = tran;
      CCProcTran ccProcTran2 = tran;
      nullable1 = new DateTime?(PXTimeZoneInfo.Now);
      DateTime? nullable2 = nullable1;
      ccProcTran2.EndTime = nullable2;
      DateTime? nullable3 = nullable1;
      ccProcTran1.StartTime = nullable3;
    }
    CCProcTran ccProcTran = tran;
    nullable1 = new DateTime?();
    DateTime? nullable4 = nullable1;
    ccProcTran.ExpirationDate = nullable4;
    tran.TranStatus = "APR";
    tran.ProcStatus = "FIN";
  }

  protected virtual void ProcessAPIResponse(APIResponse apiResponse)
  {
    if (!apiResponse.isSucess && apiResponse.ErrorSource != CCError.CCErrorSource.None)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, string> message in apiResponse.Messages)
      {
        stringBuilder.Append(message.Key);
        stringBuilder.Append(": ");
        stringBuilder.Append(message.Value.Trim(' '));
        if (stringBuilder.Length > 0)
        {
          if (stringBuilder[stringBuilder.Length - 1] != '.')
            stringBuilder.Append(".");
          stringBuilder.Append(" ");
        }
      }
      throw new PXException("Credit card processing error. {0} : {1}", new object[2]
      {
        (object) CCError.GetDescription(apiResponse.ErrorSource),
        (object) stringBuilder.ToString().TrimEnd(' ')
      });
    }
  }

  protected virtual TranOperationResult DoTransaction(
    CCTranType aTranType,
    CCProcTran aTran,
    string origRefNbr,
    string customerCd,
    string meansOfPayment = "")
  {
    TranOperationResult tranOperationResult1 = new TranOperationResult();
    CCProcessingCenter centerFromTransaction = this.GetAndCheckProcessingCenterFromTransaction(aTran);
    aTran = this.StartCreditCardTransaction(aTranType, aTran, centerFromTransaction);
    PX.Objects.AR.ExternalTransaction externalTransaction = PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, aTran.TransactionID);
    bool flag1 = false;
    bool flag2 = PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing.isCvvVerificationRequired(aTranType) && aTran.TerminalID == null;
    int? nullable1;
    if (flag2)
    {
      bool aIsStored = false;
      CCProcTran aTran1 = (CCProcTran) null;
      int? pmInstanceId = aTran.PMInstanceID;
      nullable1 = PaymentTranExtConstants.NewPaymentProfile;
      if (!(pmInstanceId.GetValueOrDefault() == nullable1.GetValueOrDefault() & pmInstanceId.HasValue == nullable1.HasValue))
        aTran1 = this.findCVVVerifyingTran(aTran.PMInstanceID, out aIsStored);
      if (aTran1 != null)
      {
        flag1 = true;
        if (!aIsStored)
          this.UpdateCvvVerificationStatus(aTran1);
      }
      if (!flag1)
        aTran.CVVVerificationStatus = "NOV";
    }
    TranOperationResult tranOperationResult2 = tranOperationResult1;
    nullable1 = aTran.TransactionID;
    int? nullable2 = new int?(nullable1.Value);
    tranOperationResult2.TransactionId = nullable2;
    TranProcessingInput tranProcessingInput = new TranProcessingInput();
    PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing.Copy(tranProcessingInput, aTran);
    if (!string.IsNullOrEmpty(customerCd))
      tranProcessingInput.CustomerCD = customerCd;
    if (!string.IsNullOrEmpty(origRefNbr))
      tranProcessingInput.OrigRefNbr = origRefNbr;
    if (flag2)
      tranProcessingInput.VerifyCVV = !flag1;
    if (externalTransaction != null)
      tranProcessingInput.TranUID = externalTransaction.NoteID;
    tranProcessingInput.MeansOfPayment = meansOfPayment;
    ICardTransactionProcessingWrapper processingWrapper = this.GetProcessingWrapper(new CCProcessingContext()
    {
      callerGraph = this._repository.Graph,
      processingCenter = centerFromTransaction,
      aCustomerCD = customerCd,
      aPMInstanceID = tranProcessingInput.PMInstanceID,
      aDocType = tranProcessingInput.DocType,
      aRefNbr = tranProcessingInput.DocRefNbr
    });
    TranProcessingResult processingResult = new TranProcessingResult();
    bool hasError = false;
    try
    {
      processingResult = processingWrapper.DoTransaction(aTranType, tranProcessingInput);
      PXTrace.WriteInformation($"CCPaymentProcessing.DoTransaction. PCTranNumber:{processingResult.PCTranNumber}; TranType: {aTranType}; PCResponseCode:{processingResult.PCResponseCode}; PCResponseReasonCode:{processingResult.PCResponseReasonCode}; PCResponseReasonText:{processingResult.PCResponseReasonText}; ErrorText:{processingResult.ErrorText}");
    }
    catch (CCProcessingException ex) when (!(((Exception) ex).InnerException is WebException))
    {
      if (ex.ProcessingResult != null)
      {
        processingResult = V2Converter.ConvertTranProcessingResult(ex.ProcessingResult);
        processingResult.Success = false;
        processingResult.TranStatus = CCTranStatus.Error;
      }
      int? nullable3 = new int?();
      if (ex.Reason == 1 && aTranType != CCTranType.IncreaseAuthorizedAmount)
      {
        processingResult.TranStatus = CCTranStatus.Declined;
        nullable3 = ex.ReasonCode;
        hasError = false;
      }
      else if (ex.Reason == 4)
      {
        hasError = true;
        this.DisablePOSTerminal(centerFromTransaction.ProcessingCenterID, tranProcessingInput.POSTerminalID);
      }
      else
        hasError = true;
      processingResult.ErrorSource = CCError.CCErrorSource.ProcessingCenter;
      string str = string.Empty;
      if (!((Exception) ex).Message.Equals(processingResult.PCResponseReasonText))
        str = ((Exception) ex).Message.Equals(((Exception) ex).InnerException?.Message) || ((Exception) ex).InnerException == null ? ((Exception) ex).Message : $"{((Exception) ex).Message}; {((Exception) ex)?.InnerException?.Message}";
      if (nullable3.HasValue)
        processingResult.PCResponseReasonCode = nullable3.ToString();
      processingResult.ErrorText = str;
      processingResult.PCResponseReasonText += str;
      if (ex.ProcessingResult == null)
      {
        processingResult.CardType = CardType.GetCardTypeEnumByCode(externalTransaction.CardType);
        processingResult.ProcCenterCardTypeCode = externalTransaction.ProcCenterCardTypeCode;
      }
      PXTrace.WriteInformation($"CCPaymentProcessing.DoTransaction.V2.CCProcessingException. ErrorSource:{processingResult.ErrorSource}; ErrorText:{processingResult.ErrorText}");
    }
    catch (Exception ex) when (ex is WebException || ex.InnerException is WebException)
    {
      hasError = true;
      processingResult.ErrorSource = CCError.CCErrorSource.Network;
      processingResult.ErrorText = ex.Message;
      PXTrace.WriteInformation($"CCPaymentProcessing.DoTransaction.WebException. ErrorSource:{processingResult.ErrorSource}; ErrorText:{processingResult.ErrorText}");
    }
    catch (Exception ex)
    {
      hasError = true;
      processingResult.ErrorSource = CCError.CCErrorSource.Internal;
      processingResult.ErrorText = ex.Message;
      throw new PXException("Error during request processing. Transaction ID:{0}, Error:{1}", new object[2]
      {
        (object) aTran.TranNbr,
        (object) ex.Message
      });
    }
    finally
    {
      CCProcTran aTran2 = this.EndTransaction(aTran.TranNbr.Value, processingResult, this.GetCCProcStatus(processingResult, hasError));
      if (!hasError)
        this.ProcessTranResult(aTran2, processingResult);
    }
    tranOperationResult1.Success = processingResult.Success;
    return tranOperationResult1;
  }

  public virtual CCProcTran StartCreditCardTransaction(
    CCTranType aTranType,
    CCProcTran ccProcTran,
    CCProcessingCenter procCenter)
  {
    ccProcTran.ProcessingCenterID = procCenter.ProcessingCenterID;
    ccProcTran.TranType = CCTranTypeCode.GetTypeCode(aTranType);
    ccProcTran.ProcStatus = "OPN";
    ccProcTran.CVVVerificationStatus = "RPV";
    ccProcTran.PCTranNumber = string.Empty;
    ccProcTran = this.StartTransaction(ccProcTran, procCenter.OpenTranTimeout);
    return ccProcTran;
  }

  public virtual CCProcessingCenter GetAndCheckProcessingCenterFromTransaction(CCProcTran ccProcTran)
  {
    return this.GetAndCheckProcessingCenterFromTransaction(ccProcTran.ProcessingCenterID, ccProcTran.PMInstanceID, ccProcTran.CuryID);
  }

  private CCProcessingCenter GetAndCheckProcessingCenterFromTransaction(
    string processingCenterID,
    int? pMInstanceID,
    string curyID)
  {
    CCProcessingCenter procCenter = string.IsNullOrEmpty(processingCenterID) ? this._repository.FindProcessingCenter(pMInstanceID, curyID) : CCProcessingCenter.PK.Find(this._repository.Graph, processingCenterID);
    this.CheckProcessingCenter(procCenter);
    if (procCenter == null || string.IsNullOrEmpty(procCenter.ProcessingTypeName))
      throw new PXException("Processing center for this card type is not configured properly.");
    this.CheckProcCenterCashAccountCury(procCenter, curyID);
    return procCenter;
  }

  public virtual void ShowAcceptPaymentForm(
    CCTranType tranType,
    ICCPayment paymentDoc,
    string procCenterId,
    int? bAccountId,
    Guid? TranUID)
  {
    this.CheckProcCenterCashAccountCury(CCProcessingCenter.PK.Find(this._repository.Graph, procCenterId), paymentDoc.CuryID);
    CCProcessingContext context = new CCProcessingContext();
    context.processingCenter = CCProcessingCenter.PK.Find(this._repository.Graph, procCenterId);
    context.callerGraph = this._repository.Graph;
    context.aRefNbr = paymentDoc.RefNbr;
    context.aDocType = paymentDoc.DocType;
    context.aCustomerID = bAccountId;
    ICardProcessingReadersProvider provider = (ICardProcessingReadersProvider) new CardProcessingReadersProvider(context);
    this.HostedPaymnetFormProcessingWrapper = (Func<object, ICardProcessingReadersProvider, IHostedPaymentFormProcessingWrapper>) ((p, w) => HostedFromProcessingWrapper.GetPaymentFormProcessingWrapper(p, w, context));
    IHostedPaymentFormProcessingWrapper processingWrapper = this.GetHostedPaymentFormProcessingWrapper(procCenterId, provider);
    ProcessingInput processingInput = new V2ProcessingInputGenerator(provider)
    {
      FillCardData = false,
      FillCustomerData = true,
      FillAdressData = true
    }.GetProcessingInput(tranType, paymentDoc);
    processingInput.TranUID = TranUID;
    this.CopyL2DataToProcessingInput(paymentDoc, processingInput);
    ProcessingInput inputData = processingInput;
    processingWrapper.GetPaymentForm(inputData);
  }

  public virtual HostedFormResponse ParsePaymentFormResponse(string response, string procCenterId)
  {
    ICardProcessingReadersProvider provider = (ICardProcessingReadersProvider) new CardProcessingReadersProvider(new CCProcessingContext()
    {
      callerGraph = this._repository.Graph,
      processingCenter = CCProcessingCenter.PK.Find(this._repository.Graph, procCenterId)
    });
    return this.GetHostedPaymentFormProcessingWrapper(procCenterId, provider).ParsePaymentFormResponse(response);
  }

  private ICardTransactionProcessingWrapper GetProcessingWrapperForProcessingCenterId(
    string processingCenterId)
  {
    return this.GetProcessingWrapper(new CCProcessingContext()
    {
      callerGraph = this._repository.Graph,
      processingCenter = CCProcessingCenter.PK.Find(this._repository.Graph, processingCenterId)
    });
  }

  public virtual TransactionData GetTransactionById(string transactionId, string processingCenterId)
  {
    return this.GetProcessingWrapperForProcessingCenterId(processingCenterId).GetTransaction(transactionId);
  }

  public virtual IEnumerable<TransactionData> GetTransactionsByBatch(
    string batchId,
    string processingCenterId)
  {
    return this.GetProcessingWrapperForProcessingCenterId(processingCenterId).GetTransactionsByBatch(batchId);
  }

  public virtual IEnumerable<TransactionData> GetTransactionsByTypedBatch(
    string batchId,
    string processingCenterId,
    BatchType batchType)
  {
    return this.GetProcessingWrapperForProcessingCenterId(processingCenterId).GetTransactionsByTypedBatch(batchId, batchType);
  }

  public virtual IEnumerable<TransactionData> GetUnsettledTransactions(
    string processingCenterId,
    TransactionSearchParams searchParams = null)
  {
    return this.GetProcessingWrapperForProcessingCenterId(processingCenterId).GetUnsettledTransactions(searchParams);
  }

  public virtual PXPluginRedirectOptions PreparePaymentForm(
    ICCPayment paymentDoc,
    string processingCenterId,
    int? bAccountId,
    bool saveCard,
    CCTranType tranType,
    Guid? tranUid,
    string meansOfPayment)
  {
    ICardProcessingReadersProvider provider = (ICardProcessingReadersProvider) new CardProcessingReadersProvider(new CCProcessingContext()
    {
      processingCenter = CCProcessingCenter.PK.Find(this._repository.Graph, processingCenterId),
      callerGraph = this._repository.Graph,
      aRefNbr = paymentDoc.RefNbr,
      aDocType = paymentDoc.DocType,
      aCustomerID = bAccountId
    });
    IHostedPaymentFormProcessingWrapper processingWrapper = this.GetHostedPaymentFormProcessingWrapper(processingCenterId, provider);
    PaymentFormPrepareOptions formPrepareOptions = new V2ProcessingInputGenerator(provider)
    {
      FillCardData = false,
      FillCustomerData = false,
      FillAdressData = true
    }.GetPaymentFormPrepareOptions(tranType, paymentDoc);
    ((ProcessingInput) formPrepareOptions).SaveCard = saveCard;
    ((ProcessingInput) formPrepareOptions).TranUID = tranUid;
    ((ProcessingInput) formPrepareOptions).MeansOfPayment = meansOfPayment == "EFT" ? (MeansOfPayment) 1 : (MeansOfPayment) 0;
    this.CopyL2DataToProcessingInput(paymentDoc, (ProcessingInput) formPrepareOptions);
    PaymentFormPrepareOptions inputData = formPrepareOptions;
    return processingWrapper.PreparePaymentForm(inputData);
  }

  public virtual PaymentFormResponseProcessResult ProcessPaymentFormResponse(
    ICCPayment paymentDoc,
    string processingCenterId,
    int? bAccountId,
    string response)
  {
    ICardProcessingReadersProvider provider = (ICardProcessingReadersProvider) new CardProcessingReadersProvider(new CCProcessingContext()
    {
      callerGraph = this._repository.Graph,
      processingCenter = CCProcessingCenter.PK.Find(this._repository.Graph, processingCenterId),
      aRefNbr = paymentDoc.RefNbr,
      aDocType = paymentDoc.DocType,
      aCustomerID = bAccountId
    });
    IHostedPaymentFormProcessingWrapper processingWrapper = this.GetHostedPaymentFormProcessingWrapper(processingCenterId, provider);
    PaymentFormPrepareOptions formPrepareOptions = new V2ProcessingInputGenerator(provider)
    {
      FillCardData = false,
      FillCustomerData = true,
      FillAdressData = true
    }.GetPaymentFormPrepareOptions((CCTranType) 1, paymentDoc);
    this.CopyL2DataToProcessingInput(paymentDoc, (ProcessingInput) formPrepareOptions);
    PaymentFormPrepareOptions inputData = formPrepareOptions;
    string response1 = response;
    return processingWrapper.ProcessPaymentFormResponse(inputData, response1);
  }

  public virtual IEnumerable<BatchData> GetSettledBatches(
    string processingCenterId,
    BatchSearchParams batchSearchParams)
  {
    return this.GetProcessingWrapperForProcessingCenterId(processingCenterId).GetSettledBatches(batchSearchParams);
  }

  public virtual TransactionData FindTransaction(string transactionApiId, string procCenterId)
  {
    return this.GetProcessingWrapperForProcessingCenterId(procCenterId).FindTransaction(new TransactionSearchParams()
    {
      TransactionApiId = transactionApiId
    });
  }

  public virtual TransactionData FindTransaction(Guid transactionGuid, string procCenterId)
  {
    return this.GetProcessingWrapperForProcessingCenterId(procCenterId).FindTransaction(new TransactionSearchParams()
    {
      TransactionGuid = transactionGuid,
      TransactionApiId = transactionGuid.ToString()
    });
  }

  public virtual void FinalizeTransaction(int? tranId, string message)
  {
    CCProcTran procTran = CCProcTran.PK.Find(this._repository.Graph, tranId);
    if (procTran == null)
      return;
    procTran.ProcStatus = "FIN";
    procTran.TranStatus = "ERR";
    procTran.PCResponseReasonText = message;
    procTran.ErrorText = "The transaction is not found in the processing center.";
    this._repository.InsertOrUpdateTransaction(procTran);
  }

  protected virtual CCProcTran StartTransaction(CCProcTran aTran, int? aAutoExpTimeout)
  {
    aTran.TranNbr = new int?();
    aTran.StartTime = new DateTime?(PXTimeZoneInfo.Now);
    if (aAutoExpTimeout.HasValue)
      aTran.ExpirationDate = new DateTime?(aTran.StartTime.Value.AddSeconds((double) aAutoExpTimeout.Value));
    aTran = this._repository.InsertOrUpdateTransaction(aTran);
    return aTran;
  }

  protected virtual CCProcTran EndTransaction(
    int aTranID,
    TranProcessingResult aRes,
    string aProcStatus)
  {
    CCProcTran ccProcTran = CCProcTran.PK.Find(this._repository.Graph, new int?(aTranID));
    PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing.Copy(ccProcTran, aRes);
    ccProcTran.ProcStatus = aProcStatus;
    ccProcTran.EndTime = new DateTime?(PXTimeZoneInfo.Now);
    PX.Objects.AR.ExternalTransaction externalTransaction = PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, ccProcTran.TransactionID);
    ccProcTran.ExpirationDate = !(ccProcTran.TranType != "IAA") ? externalTransaction.ExpirationDate : (!aRes.ExpireAfterDays.HasValue ? new DateTime?() : new DateTime?(ccProcTran.EndTime.Value.AddDays((double) aRes.ExpireAfterDays.Value)));
    externalTransaction.TerminalID = aRes.POSTerminalID;
    externalTransaction.LastDigits = !string.IsNullOrEmpty(aRes.LastDigits) ? aRes.LastDigits.Substring(aRes.LastDigits.Length - 4) : string.Empty;
    this.SetCardTypeValue(externalTransaction, aRes.CardType, aRes.ProcCenterCardTypeCode);
    CCProcTran aTran = this._repository.UpdateTransaction(ccProcTran, externalTransaction);
    if (aRes.Level3Support && EnumerableExtensions.IsIn<string>(externalTransaction.ProcStatus, "CAS", "AUS", "AUH"))
    {
      Level3Helper.SetL3StatusExternalTransaction(externalTransaction, new L3TranStatus?(), (string) null);
      this._repository.UpdateExternalTransaction(externalTransaction);
      this._repository.Save();
    }
    this.UpdateCvvVerificationStatus(aTran);
    return aTran;
  }

  protected virtual void ProcessTranResult(CCProcTran aTran, TranProcessingResult aResult)
  {
  }

  protected virtual void CopyIncreaseDataToProcTran(ICCPayment doc, CCProcTran procTran)
  {
    PX.Objects.Extensions.PaymentTransaction.Payment extension = PXCacheEx.GetExtension<PX.Objects.Extensions.PaymentTransaction.Payment>((IBqlTable) doc);
    if (extension == null)
      return;
    procTran.OrigDocType = extension.TransactionOrigDocType;
    procTran.OrigRefNbr = extension.TransactionOrigDocRefNbr;
    procTran.Amount = extension.CuryDocBalIncrease;
  }

  protected virtual bool AuthorizeBasedOnPreviousTransaction(
    CCTranType tranType,
    string PreviousExternalTransactionID,
    CCProcessingCenter procCenter)
  {
    return tranType == CCTranType.AuthorizeOnly && PreviousExternalTransactionID != null && CCProcessingFeatureHelper.IsFeatureSupported(procCenter, CCProcessingFeature.AuthorizeBasedOnPrevious);
  }

  protected virtual PX.Objects.Extensions.PaymentTransaction.Payment CopyL2DataToProcTran(
    ICCPayment doc,
    CCProcTran procTran)
  {
    PX.Objects.Extensions.PaymentTransaction.Payment extension = PXCacheEx.GetExtension<PX.Objects.Extensions.PaymentTransaction.Payment>((IBqlTable) doc);
    if (extension != null)
    {
      procTran.Tax = extension.Tax;
      procTran.SubtotalAmount = extension.SubtotalAmount;
    }
    return extension;
  }

  protected virtual void CopyL2DataToProcessingInput(
    ICCPayment doc,
    ProcessingInput processingInput)
  {
    PX.Objects.Extensions.PaymentTransaction.Payment extension = PXCacheEx.GetExtension<PX.Objects.Extensions.PaymentTransaction.Payment>((IBqlTable) doc);
    if (extension == null)
      return;
    processingInput.Tax = extension.Tax;
    processingInput.SubtotalAmount = extension.SubtotalAmount;
  }

  protected virtual int? RecordTransaction(CCTranType tranType, CCProcTran tran)
  {
    PX.Objects.AR.ExternalTransaction extTran = new PX.Objects.AR.ExternalTransaction();
    if (tran.TransactionID.GetValueOrDefault() > 0)
      extTran = PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, tran.TransactionID);
    return this.RecordTransaction(tranType, tran, extTran);
  }

  protected virtual int? RecordTransaction(
    CCTranType tranType,
    CCProcTran tran,
    PX.Objects.AR.ExternalTransaction extTran)
  {
    tran.TranType = CCTranTypeCode.GetTypeCode(tranType);
    if (string.IsNullOrEmpty(tran.CVVVerificationStatus))
    {
      tran.CVVVerificationStatus = "RPV";
      bool flag = false;
      if (PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing.isCvvVerificationRequired(tranType))
      {
        CCProcTran ccProcTran = (CCProcTran) null;
        int? pmInstanceId = tran.PMInstanceID;
        int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
        if (!(pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue))
          ccProcTran = this.findCVVVerifyingTran(tran.PMInstanceID, out bool _);
        if (ccProcTran != null)
          flag = true;
        if (!flag)
          tran.CVVVerificationStatus = "NOV";
      }
    }
    DateTime? nullable1 = tran.StartTime;
    if (!nullable1.HasValue)
    {
      CCProcTran ccProcTran1 = tran;
      CCProcTran ccProcTran2 = tran;
      nullable1 = new DateTime?(PXTimeZoneInfo.Now);
      DateTime? nullable2 = nullable1;
      ccProcTran2.EndTime = nullable2;
      DateTime? nullable3 = nullable1;
      ccProcTran1.StartTime = nullable3;
    }
    switch (tranType)
    {
      case CCTranType.AuthorizeAndCapture:
        if (tran.TranStatus == "HFR")
          goto case CCTranType.AuthorizeOnly;
        goto default;
      case CCTranType.AuthorizeOnly:
      case CCTranType.IncreaseAuthorizedAmount:
        if (tran.TranStatus == null)
          tran.TranStatus = "APR";
        tran.ProcStatus = "FIN";
        tran = this._repository.InsertOrUpdateTransaction(tran, extTran);
        return tran.TransactionID;
      default:
        CCProcTran ccProcTran3 = tran;
        nullable1 = new DateTime?();
        DateTime? nullable4 = nullable1;
        ccProcTran3.ExpirationDate = nullable4;
        goto case CCTranType.AuthorizeOnly;
    }
  }

  protected virtual void DisablePOSTerminal(string processingCenterID, string terminalID)
  {
    CCProcessingCenterMaint instance = PXGraph.CreateInstance<CCProcessingCenterMaint>();
    CCProcessingCenterMaintTerminal extension = ((PXGraph) instance).GetExtension<CCProcessingCenterMaintTerminal>();
    CCProcessingCenterTerminal processingCenterTerminal = CCProcessingCenterTerminal.PK.Find((PXGraph) instance, terminalID, processingCenterID);
    if (processingCenterTerminal == null)
      return;
    processingCenterTerminal.IsActive = new bool?(false);
    processingCenterTerminal.CanBeEnabled = new bool?(false);
    ((PXSelectBase<CCProcessingCenterTerminal>) extension.POSTerminals).Update(processingCenterTerminal);
    ((PXAction) instance.Save).Press();
  }

  private CCProcTran GetProcTranFromOtherDocInHistory(
    CCProcTran sTran,
    IEnumerable<PX.Objects.AR.ExternalTransaction> extTrans,
    IEnumerable<CCProcTran> procTrans)
  {
    string targetDocType = (string) null;
    string targetRefNbr = (string) null;
    CCProcTran otherDocInHistory = (CCProcTran) null;
    PX.Objects.AR.ExternalTransaction sExtTran = extTrans.Where<PX.Objects.AR.ExternalTransaction>((Func<PX.Objects.AR.ExternalTransaction, bool>) (i =>
    {
      int? transactionId1 = i.TransactionID;
      int? transactionId2 = sTran.TransactionID;
      return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
    })).FirstOrDefault<PX.Objects.AR.ExternalTransaction>();
    if (sTran.DocType == sExtTran.DocType && sTran.RefNbr == sExtTran.RefNbr && sExtTran.VoidDocType != null)
    {
      targetDocType = sExtTran.VoidDocType;
      targetRefNbr = sExtTran.VoidRefNbr;
    }
    else if (sTran.DocType == sExtTran.VoidDocType && sTran.RefNbr == sExtTran.VoidRefNbr)
    {
      targetDocType = sExtTran.DocType;
      targetRefNbr = sExtTran.RefNbr;
    }
    if (targetDocType != null)
      otherDocInHistory = procTrans.Where<CCProcTran>((Func<CCProcTran, bool>) (i =>
      {
        int? transactionId3 = i.TransactionID;
        int? transactionId4 = sExtTran.TransactionID;
        return transactionId3.GetValueOrDefault() == transactionId4.GetValueOrDefault() & transactionId3.HasValue == transactionId4.HasValue && i.DocType == targetDocType && i.RefNbr == targetRefNbr;
      })).FirstOrDefault<CCProcTran>();
    return otherDocInHistory;
  }

  private void PopulateExtTranFromTranRecordObj(
    PX.Objects.AR.ExternalTransaction extTran,
    TranRecordData tranRecordData)
  {
    extTran.SaveProfile = new bool?(tranRecordData.CreateProfile);
    extTran.NeedSync = new bool?(tranRecordData.NeedSync);
    extTran.ExtProfileId = tranRecordData.ExtProfileId;
    this.SetCardTypeValue(extTran, tranRecordData.CardType, tranRecordData.ProcCenterCardTypeCode);
    if (tranRecordData.TranUID.HasValue)
      extTran.NoteID = tranRecordData.TranUID;
    if (tranRecordData.PayLinkExternalId != null && !extTran.PayLinkID.HasValue)
    {
      CCPayLink linkByExternalId = this.GetPayLinkByExternalId(tranRecordData.PayLinkExternalId, tranRecordData.ProcessingCenterId);
      if (linkByExternalId != null)
        extTran.PayLinkID = linkByExternalId.PayLinkID;
    }
    extTran.LastDigits = tranRecordData.LastDigits;
  }

  private void UpdateExtTranInfo(CCProcTran ccTran, TranRecordData recordData)
  {
    PX.Objects.AR.ExternalTransaction externalTransaction = new PX.Objects.AR.ExternalTransaction();
    if (ccTran.TransactionID.GetValueOrDefault() > 0)
      externalTransaction = PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, ccTran.TransactionID);
    if (recordData.Level3Support)
      Level3Helper.SetL3StatusExternalTransaction(externalTransaction, new L3TranStatus?(), (string) null);
    this.SetCardTypeValue(externalTransaction, recordData.CardType, recordData.ProcCenterCardTypeCode);
    externalTransaction.LastDigits = recordData.LastDigits;
  }

  private void SetCardTypeValue(
    PX.Objects.AR.ExternalTransaction extTran,
    CCCardType cardType,
    string crocCenterCardTypeCode)
  {
    extTran.CardType = CardType.GetCardTypeCode(cardType);
    extTran.ProcCenterCardTypeCode = extTran.CardType == "OTH" ? crocCenterCardTypeCode : (string) null;
  }

  private string GetCCProcStatus(TranProcessingResult result, bool hasError)
  {
    if (result.ErrorSource == CCError.CCErrorSource.Network)
      return "OPN";
    return !hasError ? "FIN" : "ERR";
  }

  protected static bool isCvvVerificationRequired(CCTranType aType)
  {
    return aType == CCTranType.AuthorizeOnly || aType == CCTranType.AuthorizeAndCapture || aType == CCTranType.Unknown;
  }

  protected virtual CCProcTran findCVVVerifyingTran(int? aPMInstanceID, out bool aIsStored)
  {
    PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PX.Objects.AR.CustomerPaymentMethod.PK.Find(this._repository.Graph, aPMInstanceID);
    if (customerPaymentMethod.CVVVerifyTran.HasValue)
    {
      aIsStored = true;
      return CCProcTran.PK.Find(this._repository.Graph, customerPaymentMethod.CVVVerifyTran);
    }
    aIsStored = false;
    return this._repository.FindVerifyingCCProcTran(aPMInstanceID);
  }

  protected virtual Tuple<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.Customer> FindCpmAndCustomer(
    int? aPMInstanceID)
  {
    PXResult<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.Customer> andPaymentMethod = this._repository.FindCustomerAndPaymentMethod(aPMInstanceID);
    if (andPaymentMethod != null)
      return new Tuple<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.Customer>(PXResult<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.Customer>.op_Implicit(andPaymentMethod), PXResult<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.Customer>.op_Implicit(andPaymentMethod));
    int? nullable = aPMInstanceID;
    int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
    if (nullable.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & nullable.HasValue == newPaymentProfile.HasValue)
      throw new PXException("The credit card is not defined.");
    throw new PXException("Credit Card with ID {0} is not defined", new object[1]
    {
      (object) aPMInstanceID
    });
  }

  protected virtual void UpdateCvvVerificationStatus(CCProcTran aTran)
  {
    if (!(aTran.TranStatus == "APR") || !(aTran.CVVVerificationStatus == "MTH") || !(aTran.TranType == "AAC") && !(aTran.TranType == "AUT"))
      return;
    PX.Objects.AR.CustomerPaymentMethod paymentMethod = PX.Objects.AR.CustomerPaymentMethod.PK.Find(this._repository.Graph, aTran.PMInstanceID);
    if (paymentMethod == null || paymentMethod.CVVVerifyTran.HasValue)
      return;
    paymentMethod.CVVVerifyTran = aTran.TranNbr;
    CustomerPaymentMethodDetail paymentMethodDetail = this._repository.GetCustomerPaymentMethodDetail(aTran.PMInstanceID, "CVV");
    if (paymentMethodDetail != null)
      this._repository.DeletePaymentMethodDetail(paymentMethodDetail);
    this._repository.UpdateCustomerPaymentMethod(paymentMethod);
    this._repository.Save();
  }

  protected object GetProcessor(CCProcessingCenter processingCenter)
  {
    if (processingCenter == null)
      throw new PXException("Processing center can't be found");
    try
    {
      return CCPluginTypeHelper.CreatePluginInstance(processingCenter);
    }
    catch (PXException ex)
    {
      throw;
    }
    catch
    {
      throw new PXException("Cannot instantiate processing object of {0} type for the processing center {1}.", new object[2]
      {
        (object) processingCenter.ProcessingTypeName,
        (object) processingCenter.ProcessingCenterID
      });
    }
  }

  protected ICardTransactionProcessingWrapper GetProcessingWrapper(CCProcessingContext context)
  {
    return this._transactionProcessingWrapper(this.GetProcessor(context.processingCenter), context);
  }

  protected IHostedPaymentFormProcessingWrapper GetHostedPaymentFormProcessingWrapper(
    string procCenterId,
    ICardProcessingReadersProvider provider)
  {
    return this.HostedPaymnetFormProcessingWrapper(this.GetProcessor(CCProcessingCenter.PK.Find(this._repository.Graph, procCenterId)), provider);
  }

  protected virtual void IsValidPmInstance(int? pmInstanceId)
  {
    int? nullable = pmInstanceId;
    int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
    if (nullable.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & nullable.HasValue == newPaymentProfile.HasValue)
      return;
    PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod1;
    this.FindCpmAndCustomer(pmInstanceId).Deconstruct<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.Customer>(out customerPaymentMethod1, out PX.Objects.AR.Customer _);
    PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod2 = customerPaymentMethod1;
    if (customerPaymentMethod2 == null)
      throw new PXException("Credit Card with ID {0} is not defined", new object[1]
      {
        (object) pmInstanceId
      });
    if (customerPaymentMethod2 == null)
      return;
    bool? isActive = customerPaymentMethod2.IsActive;
    bool flag = false;
    if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      throw new PXException("The {0} card/account number is inactive on the Customer Payment Methods (AR303010) form and cannot be processed.", new object[1]
      {
        (object) customerPaymentMethod2.Descr
      });
  }

  private CCProcTran GetSuccessfulCCProcTran(PX.Objects.AR.ExternalTransaction extTran)
  {
    if (ExternalTranHelper.LastSuccessfulCCProcTran((IExternalTransaction) extTran, this._repository) is CCProcTran successfulCcProcTran)
      return successfulCcProcTran;
    throw new Exception("Could not get CCProcTran record by TransactionId.");
  }

  private void CheckProcessingCenterNotNullAndActive(CCProcessingCenter processingCenter)
  {
    if (processingCenter == null)
      throw new PXException("Processing center can't be found");
    if (!processingCenter.IsActive.GetValueOrDefault())
      throw new PXException("Processing center {0} is inactive", new object[1]
      {
        (object) processingCenter.ProcessingCenterID
      });
  }

  private void CheckExternalTransactionNotNullAndValid(
    PX.Objects.AR.ExternalTransaction externalTransaction,
    int? transactionId)
  {
    if (externalTransaction == null)
      throw new PXException("Priorly Authorized Transaction {0} is not found", new object[1]
      {
        (object) transactionId
      });
    if (ExternalTranHelper.IsExpired((IExternalTransaction) externalTransaction))
      throw new PXException("Authorizing Transaction {0} has already expired. Authorization must be redone", new object[1]
      {
        (object) transactionId
      });
    if (!externalTransaction.Active.GetValueOrDefault())
      throw new PXException("Transaction {0} failed authorization", new object[1]
      {
        (object) transactionId
      });
  }

  private void FindProcessingCenterAndCustomerByPayment(
    ICCPayment payment,
    out CCProcessingCenter processingCenter,
    out PX.Objects.AR.Customer customer)
  {
    if (payment.PMInstanceID.HasValue)
    {
      this.IsValidPmInstance(payment.PMInstanceID);
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod1;
      PX.Objects.AR.Customer customer1;
      this.FindCpmAndCustomer(payment.PMInstanceID).Deconstruct<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.Customer>(out customerPaymentMethod1, out customer1);
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod2 = customerPaymentMethod1;
      PX.Objects.AR.Customer customer2 = customer1;
      customer = customer2;
      processingCenter = this._repository.FindProcessingCenter(customerPaymentMethod2.PMInstanceID, (string) null);
      if (processingCenter == null)
        throw new PXException("Processing center for payment method {0} is not specified", new object[1]
        {
          (object) customerPaymentMethod2.Descr
        });
    }
    else
    {
      customer = this.GetCustomer(payment);
      processingCenter = CCProcessingCenter.PK.Find(this._repository.Graph, payment.ProcessingCenterID);
    }
  }

  public static void Copy(TranProcessingInput aDst, CCProcTran aSrc)
  {
    aDst.TranID = aSrc.TranNbr.Value;
    aDst.PMInstanceID = aSrc.PMInstanceID;
    bool flag = string.IsNullOrEmpty(aSrc.DocType);
    aDst.DocType = flag ? aSrc.OrigDocType : aSrc.DocType;
    aDst.DocRefNbr = flag ? aSrc.OrigRefNbr : aSrc.RefNbr;
    aDst.Amount = aSrc.Amount.Value;
    aDst.CuryID = aSrc.CuryID;
    aDst.SubtotalAmount = aSrc.SubtotalAmount;
    aDst.Tax = aSrc.Tax;
    aDst.POSTerminalID = aSrc.TerminalID;
  }

  public static void Copy(CCProcTran aDst, TranProcessingResult aSrc)
  {
    aDst.PCTranNumber = aSrc.PCTranNumber;
    aDst.PCResponseCode = aSrc.PCResponseCode;
    aDst.PCResponseReasonCode = aSrc.PCResponseReasonCode;
    aDst.AuthNumber = aSrc.AuthorizationNbr;
    aDst.PCResponse = PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing.TrimIfLonger(aSrc.PCResponse, 2048 /*0x0800*/);
    aDst.PCResponseReasonText = PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing.TrimIfLonger(aSrc.PCResponseReasonText, 512 /*0x0200*/);
    aDst.CVVVerificationStatus = CVVVerificationStatusCode.GetCCVCode(aSrc.CcvVerificatonStatus);
    aDst.TranStatus = CCTranStatusCode.GetCode(aSrc.TranStatus);
    aDst.TerminalID = aSrc.POSTerminalID;
    if (aSrc.ErrorSource != CCError.CCErrorSource.None)
    {
      aDst.ErrorSource = CCError.GetCode(aSrc.ErrorSource);
      aDst.ErrorText = PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing.TrimIfLonger(aSrc.ErrorText, (int) byte.MaxValue);
      if (aSrc.ErrorSource != CCError.CCErrorSource.ProcessingCenter)
        aDst.PCResponseReasonText = PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing.TrimIfLonger(aSrc.ErrorText, 512 /*0x0200*/);
    }
    CCTranType? tranType = aSrc.TranType;
    if (tranType.HasValue)
    {
      CCProcTran ccProcTran = aDst;
      tranType = aSrc.TranType;
      string typeCode = CCTranTypeCode.GetTypeCode(tranType.Value);
      ccProcTran.TranType = typeCode;
    }
    DateTime? tranDateTimeUtc = aSrc.TranDateTimeUTC;
    if (!tranDateTimeUtc.HasValue)
      return;
    CCProcTran ccProcTran1 = aDst;
    tranDateTimeUtc = aSrc.TranDateTimeUTC;
    DateTime? nullable = new DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(tranDateTimeUtc.Value, LocaleInfo.GetTimeZone()));
    ccProcTran1.StartTime = nullable;
  }

  private static string TrimIfLonger(string source, int length)
  {
    return source != null && source.Length > length ? source.Substring(0, length) : source;
  }

  internal CCProcTran GetCCProcTran(PX.Objects.AR.ExternalTransaction externalTransaction)
  {
    return this.GetSuccessfulCCProcTran(externalTransaction);
  }

  private string GetNonEmptyProcCenterTranNumber(TranRecordData tran)
  {
    return string.IsNullOrEmpty(tran.ExternalTranId) ? tran.ExternalTranApiId : tran.ExternalTranId;
  }
}
