// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.PaymentDocCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.Common.Abstractions;
using PX.Objects.Extensions.PaymentTransaction;
using System;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing;

public class PaymentDocCreator
{
  protected ICCPaymentProcessingRepository repo;
  protected PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing paymentProc;

  public PaymentDocCreator()
  {
    this.repo = (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository((PXGraph) PXGraph.CreateInstance<CCPaymentHelperGraph>());
    this.paymentProc = new PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing(this.repo);
  }

  public PaymentDocCreator(PXGraph graph)
  {
    this.repo = (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository(graph);
    this.paymentProc = new PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing(this.repo);
  }

  public virtual IDocumentKey CreateDoc(PaymentDocCreator.InputParams inputParams)
  {
    this.CheckInput(inputParams);
    TransactionData transactionById = this.paymentProc.GetTransactionById(inputParams.TransactionID, inputParams.ProcessingCenterID);
    this.ExecValidations(transactionById, inputParams);
    return this.SaveDocWithTransaction(transactionById, inputParams);
  }

  protected virtual void CheckInput(PaymentDocCreator.InputParams inputParams)
  {
    if (inputParams.DocType != "PMT" && inputParams.DocType != "REF")
      throw new PXException("The document type is not supported or implemented.");
    if (string.IsNullOrEmpty(inputParams.TransactionID))
      throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) "TransactionID"
      });
    if (!inputParams.Customer.HasValue)
      throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) "Customer"
      });
    if (string.IsNullOrEmpty(inputParams.PaymentMethodID))
      throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) "PaymentMethodID"
      });
    if (string.IsNullOrEmpty(inputParams.ProcessingCenterID))
      throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) "ProcessingCenterID"
      });
    if (!inputParams.CashAccountID.HasValue)
      throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) "CashAccountID"
      });
  }

  protected virtual void ExecValidations(
    TransactionData tranData,
    PaymentDocCreator.InputParams inputParams)
  {
    TranValidationHelper.CheckRecordedTranStatus(tranData);
    CCTranType? nullable = !EnumerableExtensions.IsIn<CCTranType?>(tranData.TranType, new CCTranType?((CCTranType) 5), new CCTranType?((CCTranType) 9)) ? tranData.TranType : throw new PXException("The {0} transaction is voided.", new object[1]
    {
      (object) tranData.TranID
    });
    if (nullable.GetValueOrDefault() == 4 && inputParams.DocType != "REF")
      throw new PXException("The {0} transaction is refunded.", new object[1]
      {
        (object) tranData.TranID
      });
    nullable = tranData.TranType;
    if (nullable.GetValueOrDefault() != 4 && inputParams.DocType == "REF")
      throw new PXException("The {0} transaction has an invalid transaction type.", new object[1]
      {
        (object) tranData.TranID
      });
    PX.Objects.CA.PaymentMethod paymentMethod = this.repo.GetPaymentMethod(inputParams.PaymentMethodID);
    MeansOfPayment? paymentMethodType;
    if (paymentMethod.PaymentType == "EFT")
    {
      paymentMethodType = tranData.PaymentMethodType;
      if (paymentMethodType.GetValueOrDefault() == 1)
        goto label_12;
    }
    if (paymentMethod.PaymentType == "CCD")
    {
      paymentMethodType = tranData.PaymentMethodType;
      MeansOfPayment meansOfPayment = (MeansOfPayment) 0;
      if (!(paymentMethodType.GetValueOrDefault() == meansOfPayment & paymentMethodType.HasValue))
      {
        paymentMethodType = tranData.PaymentMethodType;
        if (!paymentMethodType.HasValue)
          goto label_12;
      }
      else
        goto label_12;
    }
    throw new PXException("The payment method with the {0} means of payment is not correct for this transaction. Select a payment method with the {1} means of payment.", new object[2]
    {
      (object) paymentMethod.PaymentType,
      (object) tranData.PaymentMethodType
    });
label_12:
    TranValidationHelper.AdditionalParams additionalParams = new TranValidationHelper.AdditionalParams();
    additionalParams.PMInstanceId = inputParams.PMInstanceID;
    additionalParams.ProcessingCenter = inputParams.ProcessingCenterID;
    additionalParams.CustomerID = inputParams.Customer;
    additionalParams.Repo = this.repo;
    TranValidationHelper.CheckPaymentProfile(tranData, additionalParams);
    TranValidationHelper.CheckTranAlreadyRecorded(tranData, additionalParams);
  }

  protected virtual IDocumentKey SaveDocWithTransaction(
    TransactionData tranData,
    PaymentDocCreator.InputParams inputParams)
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    ARSetup current = ((PXSelectBase<ARSetup>) instance.arsetup).Current;
    if (this.ResetHold(tranData))
      current.HoldEntry = new bool?(false);
    PX.Objects.AR.ARPayment arPayment1 = new PX.Objects.AR.ARPayment();
    arPayment1.DocType = inputParams.DocType;
    PX.Objects.AR.ARPayment arPayment2 = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Insert(arPayment1);
    arPayment2.CustomerID = inputParams.Customer;
    arPayment2.PaymentMethodID = inputParams.PaymentMethodID;
    arPayment2.CuryOrigDocAmt = new Decimal?(tranData.Amount);
    PX.Objects.AR.ARPayment arPayment3 = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Update(arPayment2);
    int? pmInstanceId = inputParams.PMInstanceID;
    int num = 0;
    if (pmInstanceId.GetValueOrDefault() > num & pmInstanceId.HasValue)
    {
      arPayment3.PMInstanceID = inputParams.PMInstanceID;
    }
    else
    {
      arPayment3.PMInstanceID = PaymentTranExtConstants.NewPaymentProfile;
      arPayment3.ProcessingCenterID = inputParams.ProcessingCenterID;
    }
    arPayment3.CashAccountID = inputParams.CashAccountID;
    PX.Objects.AR.ARPayment arPayment4 = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Update(arPayment3);
    if (arPayment4.DocType == "REF" && tranData.RefTranID != null)
      this.UpdateOrigTranNumber(instance, tranData, inputParams);
    ARPaymentEntryPaymentTransaction extension = ((PXGraph) instance).GetExtension<ARPaymentEntryPaymentTransaction>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXAction) instance.Save).Press();
      CCPaymentEntry paymentEntry = new CCPaymentEntry((PXGraph) instance);
      extension.CheckSaveCardOption(tranData);
      extension.RecordTransaction(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current, tranData, paymentEntry);
      transactionScope.Complete();
    }
    return (IDocumentKey) arPayment4;
  }

  protected virtual void UpdateOrigTranNumber(
    ARPaymentEntry paymentGraph,
    TransactionData tranData,
    PaymentDocCreator.InputParams inputParams)
  {
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentGraph.Document).Current;
    current.RefTranExtNbr = tranData.RefTranID;
    PX.Objects.AR.ARPayment arPayment = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentGraph.Document).Update(current);
    bool flag = false;
    int? pmInstanceId = arPayment.PMInstanceID;
    int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
    if (pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue && inputParams.ProcessingCenterID != arPayment.ProcessingCenterID)
    {
      arPayment.ProcessingCenterID = inputParams.ProcessingCenterID;
      flag = true;
    }
    int? cashAccountId1 = inputParams.CashAccountID;
    int? cashAccountId2 = arPayment.CashAccountID;
    if (!(cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue))
    {
      arPayment.CashAccountID = inputParams.CashAccountID;
      flag = true;
    }
    if (!flag)
      return;
    ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentGraph.Document).Update(arPayment);
  }

  protected virtual PX.Objects.AR.CustomerPaymentMethod GetCustomerPaymentMethod(
    PXGraph graph,
    int? pmInstanceId)
  {
    return PX.Objects.AR.CustomerPaymentMethod.PK.Find(graph, pmInstanceId);
  }

  private bool ResetHold(TransactionData tranData)
  {
    bool flag = false;
    if (CCProcessingHelper.GetProcessingStatusByTranData(tranData) == ProcessingStatus.CaptureSuccess)
      flag = true;
    return flag;
  }

  public class InputParams
  {
    public string DocType { get; set; } = "PMT";

    public string TransactionID { get; set; }

    public int? Customer { get; set; }

    public int? CashAccountID { get; set; }

    public int? PMInstanceID { get; set; }

    public string PaymentMethodID { get; set; }

    public string ProcessingCenterID { get; set; }
  }
}
