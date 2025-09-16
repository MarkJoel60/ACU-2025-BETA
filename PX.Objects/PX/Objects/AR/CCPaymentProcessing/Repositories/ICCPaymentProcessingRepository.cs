// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Repositories.ICCPaymentProcessingRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using PX.Objects.CC;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Repositories;

public interface ICCPaymentProcessingRepository
{
  PXGraph Graph { get; }

  CCProcTran InsertCCProcTran(CCProcTran transaction);

  CCProcTran UpdateCCProcTran(CCProcTran transaction);

  CCProcTran InsertOrUpdateTransaction(CCProcTran procTran);

  CCProcTran InsertOrUpdateTransaction(CCProcTran procTran, PX.Objects.AR.ExternalTransaction extTran);

  CCProcTran InsertTransaction(CCProcTran procTran, PX.Objects.AR.ExternalTransaction extTran);

  CCProcTran UpdateTransaction(CCProcTran procTran, PX.Objects.AR.ExternalTransaction extTran);

  PX.Objects.AR.ExternalTransaction UpdateExternalTransaction(PX.Objects.AR.ExternalTransaction extTran);

  IEnumerable<CCProcTran> GetCCProcTranByTranID(int? transactionId);

  PX.Objects.AR.ExternalTransaction FindCapturedExternalTransaction(
    int? pMInstanceID,
    string refTranNbr);

  PX.Objects.AR.ExternalTransaction FindCapturedExternalTransaction(
    string procCenterId,
    string tranNbr);

  IEnumerable<PX.Objects.AR.ExternalTransaction> GetExternalTransactionsByPayLinkID(int? payLinkId);

  Tuple<PX.Objects.AR.ExternalTransaction, PX.Objects.AR.ARPayment> GetExternalTransactionWithPayment(
    string tranNbr,
    string procCenterId);

  Tuple<PX.Objects.AR.ExternalTransaction, PX.Objects.AR.ARPayment> GetExternalTransactionWithPaymentByApiNumber(
    string tranApiNbr,
    string procCenterId);

  Tuple<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail> GetCustomerPaymentMethodWithProfileDetail(
    string procCenter,
    string custProfileId,
    string paymentProfileId);

  Tuple<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail> GetCustomerPaymentMethodWithProfileDetail(
    int? pmInstanceId);

  PX.Objects.AR.CustomerPaymentMethod UpdateCustomerPaymentMethod(
    PX.Objects.AR.CustomerPaymentMethod paymentMethod);

  CustomerPaymentMethodDetail GetCustomerPaymentMethodDetail(int? pMInstanceId, string detailID);

  CustomerProcessingCenterID GetCustomerProcessingCenterByAccountAndProcCenterIDs(
    int? baccountId,
    string procCenterId);

  (CCProcessingCenter, CCProcessingCenterBranch) GetProcessingCenterBranchByBranchAndProcCenterIDs(
    int? branchId,
    string procCenterId);

  void DeletePaymentMethodDetail(CustomerPaymentMethodDetail detail);

  CCProcTran FindVerifyingCCProcTran(int? pMInstanceID);

  CCProcessingCenter GetCCProcessingCenter(string processingCenterID);

  CCProcessingCenter FindProcessingCenter(int? pMInstanceID, string aCuryId);

  CCProcessingCenter GetProcessingCenterByID(string procCenterId);

  PXResultset<CCProcessingCenterDetail> FindAllProcessingCenterDetails(string processingCenterID);

  PXResult<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.Customer> FindCustomerAndPaymentMethod(
    int? pMInstanceID);

  PX.Objects.CA.PaymentMethod GetPaymentMethod(string paymentMehtod);

  void Save();
}
