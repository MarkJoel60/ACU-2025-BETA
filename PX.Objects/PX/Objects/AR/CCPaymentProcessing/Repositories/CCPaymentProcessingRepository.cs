// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Repositories.CCPaymentProcessingRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.Repositories;
using PX.Objects.CA;
using PX.Objects.CA.Repositories;
using PX.Objects.CC;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Repositories;

public class CCPaymentProcessingRepository : ICCPaymentProcessingRepository
{
  private CCProcTranRepository _cctranRepository;
  private ExternalTransactionRepository _externalTran;
  private CustomerPaymentMethodRepository _cpmRepository;
  private CustomerPaymentMethodDetailRepository _cpmDetailRepository;
  private CCProcessingCenterRepository _processingCenterRepository;
  private CCProcessingCenterDetailRepository _processingCenterDetailRepository;
  public PXSelect<CCProcTran> CCProcTrans;
  public PXSelect<PX.Objects.AR.CustomerPaymentMethod> CustomerPaymentMethods;
  public PXSelect<CustomerPaymentMethodDetail> CustomerPaymentMethodDetails;

  public PXGraph Graph { get; private set; }

  public bool NeedPersist { get; set; } = true;

  public bool KeepNewTranDeactivated { get; set; }

  public CCPaymentProcessingRepository(PXGraph graph)
  {
    this.Graph = graph ?? throw new ArgumentNullException(nameof (graph));
    this.InitializeRepositories(graph);
  }

  public static ICCPaymentProcessingRepository GetCCPaymentProcessingRepository()
  {
    return (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository((PXGraph) CCPaymentProcessingRepository.GetCCPaymentProcessingGraph());
  }

  public static CCPaymentHelperGraph GetCCPaymentProcessingGraph()
  {
    return PXGraph.CreateInstance<CCPaymentHelperGraph>();
  }

  public CustomerProcessingCenterID GetCustomerProcessingCenterByAccountAndProcCenterIDs(
    int? bAccountId,
    string procCenterId)
  {
    return this._cpmRepository.GetCustomerProcessingCenterByAccountAndProcCenterIDs(bAccountId, procCenterId);
  }

  public (CCProcessingCenter, CCProcessingCenterBranch) GetProcessingCenterBranchByBranchAndProcCenterIDs(
    int? branchId,
    string procCenterId)
  {
    return this._processingCenterRepository.GetProcessingCenterBranchByBranchAndProcCenterIDs(branchId, procCenterId);
  }

  public CCProcTran GetCCProcTran(int? tranID) => CCProcTran.PK.Find(this.Graph, tranID);

  public CCProcTran InsertCCProcTran(CCProcTran transaction)
  {
    return this._cctranRepository.InsertCCProcTran(transaction);
  }

  public CCProcTran InsertOrUpdateTransaction(CCProcTran procTran)
  {
    int num = 0;
    int? nullable = procTran != null ? procTran.TransactionID : throw new ArgumentNullException("CCProcTran");
    if (nullable.GetValueOrDefault() != 0)
    {
      nullable = procTran.TransactionID;
      num = nullable.Value;
    }
    if (num == 0)
    {
      PX.Objects.AR.ExternalTransaction extTransaction = new PX.Objects.AR.ExternalTransaction();
      procTran = this.InsertOrUpdateTransaction(procTran, extTransaction);
    }
    else
      procTran = this.InsertOrUpdateTransaction(procTran, PX.Objects.AR.ExternalTransaction.PK.Find(this.Graph, procTran.TransactionID) ?? throw new Exception($"Could not find External transaction record by TransactionID = {procTran.TransactionID}"));
    return procTran;
  }

  public CCProcTran InsertOrUpdateTransaction(
    CCProcTran procTran,
    PX.Objects.AR.ExternalTransaction extTransaction)
  {
    if (procTran == null)
      throw new ArgumentNullException("CCProcTran");
    if (extTransaction == null)
      throw new ArgumentNullException("ExternalTransaction");
    int? transactionId1 = procTran.TransactionID;
    int? transactionId2 = extTransaction.TransactionID;
    if (!(transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue))
      throw new Exception("External transaction record does not match CCProcTran.");
    int num = 0;
    if (procTran.TransactionID.GetValueOrDefault() != 0)
      num = procTran.TransactionID.Value;
    procTran = num != 0 ? this.UpdateTransaction(procTran, extTransaction) : this.InsertTransaction(procTran, extTransaction);
    return procTran;
  }

  public CCProcTran InsertTransaction(CCProcTran procTran, PX.Objects.AR.ExternalTransaction extTran)
  {
    if (procTran == null)
      throw new ArgumentNullException("CCProcTran");
    if (extTran == null)
      throw new ArgumentNullException("ExternalTransaction");
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      extTran.DocType = procTran.DocType;
      extTran.RefNbr = procTran.RefNbr;
      if (procTran.OrigDocType != "PMT" && procTran.TranType != "IAA")
      {
        extTran.OrigDocType = procTran.OrigDocType;
        extTran.OrigRefNbr = procTran.OrigRefNbr;
      }
      extTran.Amount = procTran.Amount;
      extTran.Direction = "D";
      extTran.PMInstanceID = procTran.PMInstanceID;
      extTran.ProcessingCenterID = procTran.ProcessingCenterID;
      extTran.CVVVerification = procTran.CVVVerificationStatus;
      extTran.ExpirationDate = procTran.ExpirationDate;
      extTran.TerminalID = procTran.TerminalID;
      if (procTran.TranType == "CDT")
      {
        extTran.Direction = "C";
        if (procTran.RefTranNbr.HasValue)
        {
          CCProcTran ccProcTran = CCProcTran.PK.Find(this.Graph, procTran.RefTranNbr);
          extTran.ParentTranID = ccProcTran.TransactionID;
        }
      }
      if (procTran.ProcStatus == "OPN")
      {
        extTran.ProcStatus = "UKN";
      }
      else
      {
        extTran.ProcStatus = ExtTransactionProcStatusCode.GetStatusByTranStatusTranType(procTran.TranStatus, procTran.TranType);
        extTran.Active = new bool?(!this.KeepNewTranDeactivated && CCProcTranHelper.IsActiveTran(procTran));
        extTran.Completed = new bool?(CCProcTranHelper.IsCompletedTran(procTran));
        extTran.TranNumber = procTran.PCTranNumber;
        extTran.TranApiNumber = procTran.PCTranApiNumber;
        extTran.CommerceTranNumber = procTran.CommerceTranNumber;
        extTran.AuthNumber = procTran.AuthNumber;
        extTran.ExpirationDate = procTran.ExpirationDate;
        extTran.LastActivityDate = procTran.EndTime;
      }
      extTran = this._externalTran.InsertExternalTransaction(extTran);
      this.UpdateExternalTransactionForReAuth(extTran);
      procTran.TransactionID = extTran.TransactionID;
      procTran = this._cctranRepository.InsertCCProcTran(procTran);
      if (this.NeedPersist)
        this.Save();
      transactionScope.Complete();
      return procTran;
    }
  }

  public CCProcTran UpdateTransaction(CCProcTran procTran, PX.Objects.AR.ExternalTransaction extTran)
  {
    if (procTran == null)
      throw new ArgumentNullException("CCProcTran");
    if (extTran == null)
      throw new ArgumentNullException("ExternalTransaction");
    if (procTran.TransactionID.GetValueOrDefault() == 0)
      throw new ArgumentNullException("TransactionID");
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (procTran.ProcStatus != "OPN")
      {
        if (procTran.AuthNumber != null)
          extTran.AuthNumber = procTran.AuthNumber;
        if (procTran.CVVVerificationStatus != null)
          extTran.CVVVerification = procTran.CVVVerificationStatus;
        if (procTran.PCTranNumber != null)
          extTran.TranNumber = procTran.PCTranNumber;
        if (procTran.PCTranApiNumber != null)
          extTran.TranApiNumber = procTran.PCTranApiNumber;
        if (procTran.Amount.HasValue && procTran.ProcStatus != "ERR")
          extTran.Amount = procTran.Amount;
        if (procTran.TranType == "CDT")
          extTran.Direction = "C";
        if (procTran.TerminalID != null)
          extTran.TerminalID = procTran.TerminalID;
        extTran.PMInstanceID = procTran.PMInstanceID;
        extTran.ProcessingCenterID = procTran.ProcessingCenterID;
        extTran.ProcStatus = ExtTransactionProcStatusCode.GetStatusByTranStatusTranType(procTran.TranStatus, procTran.TranType);
        extTran.LastActivityDate = procTran.EndTime;
        bool? nullable = extTran.NeedSync;
        bool flag1 = false;
        if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
          extTran.Active = new bool?(CCProcTranHelper.IsActiveTran(procTran));
        else
          extTran.SyncStatus = "N";
        extTran.Completed = new bool?(CCProcTranHelper.IsCompletedTran(procTran));
        if (procTran.TranType != "IAA")
        {
          if (procTran.ProcStatus == "ERR")
          {
            nullable = extTran.Active;
            bool flag2 = false;
            if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
              goto label_29;
          }
          if (CCProcTranHelper.IsFailedTran(procTran))
            goto label_30;
label_29:
          extTran.ExpirationDate = procTran.ExpirationDate;
        }
label_30:
        if (procTran.TranType != "IAA")
          this.UpdateExternalTransactionForReAuth(extTran);
        this._externalTran.UpdateExternalTransaction(extTran);
      }
      procTran = this._cctranRepository.UpdateCCProcTran(procTran);
      if (this.NeedPersist)
        this.Save();
      transactionScope.Complete();
      return procTran;
    }
  }

  private void UpdateExternalTransactionForReAuth(PX.Objects.AR.ExternalTransaction extTran)
  {
    CCProcessingCenterPmntMethod centerPmntMethod = this.GetProcessingCenterPmntMethod(extTran);
    switch (extTran.ProcStatus)
    {
      case "AUS":
        if (centerPmntMethod == null)
          break;
        int? fundHoldPeriod = centerPmntMethod.FundHoldPeriod;
        if (!fundHoldPeriod.HasValue)
          break;
        PX.Objects.AR.ExternalTransaction externalTransaction = extTran;
        DateTime? lastActivityDate = extTran.LastActivityDate;
        ref DateTime? local1 = ref lastActivityDate;
        DateTime? nullable;
        if (!local1.HasValue)
        {
          nullable = new DateTime?();
        }
        else
        {
          DateTime valueOrDefault = local1.GetValueOrDefault();
          ref DateTime local2 = ref valueOrDefault;
          fundHoldPeriod = centerPmntMethod.FundHoldPeriod;
          double num = (double) fundHoldPeriod.Value;
          nullable = new DateTime?(local2.AddDays(num));
        }
        externalTransaction.FundHoldExpDate = nullable;
        break;
      case "AUF":
      case "AUD":
      case "VDS":
      case "REJ":
      case "CAS":
        extTran.FundHoldExpDate = new DateTime?();
        break;
    }
  }

  public PX.Objects.AR.ExternalTransaction UpdateExternalTransaction(PX.Objects.AR.ExternalTransaction extTran)
  {
    return this._externalTran.UpdateExternalTransaction(extTran);
  }

  public CCProcTran UpdateCCProcTran(CCProcTran transaction)
  {
    return this._cctranRepository.UpdateCCProcTran(transaction);
  }

  public PX.Objects.AR.CustomerPaymentMethod GetCustomerPaymentMethod(int? pMInstanceId)
  {
    return PX.Objects.AR.CustomerPaymentMethod.PK.Find(this.Graph, pMInstanceId);
  }

  public Tuple<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail> GetCustomerPaymentMethodWithProfileDetail(
    string procCenter,
    string custProfileId,
    string paymentProfileId)
  {
    return this._cpmRepository.GetCustomerPaymentMethodWithProfileDetail(procCenter, custProfileId, paymentProfileId);
  }

  public Tuple<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail> GetCustomerPaymentMethodWithProfileDetail(
    int? pmInstanceId)
  {
    return this._cpmRepository.GetCustomerPaymentMethodWithProfileDetail(pmInstanceId);
  }

  public PX.Objects.AR.CustomerPaymentMethod UpdateCustomerPaymentMethod(
    PX.Objects.AR.CustomerPaymentMethod paymentMethod)
  {
    return this._cpmRepository.UpdateCustomerPaymentMethod(paymentMethod);
  }

  public CustomerPaymentMethodDetail GetCustomerPaymentMethodDetail(
    int? pMInstanceId,
    string detailID)
  {
    return this._cpmDetailRepository.GetCustomerPaymentMethodDetail(pMInstanceId, detailID);
  }

  public void DeletePaymentMethodDetail(CustomerPaymentMethodDetail detail)
  {
    this._cpmDetailRepository.DeletePaymentMethodDetail(detail);
  }

  private CCProcessingCenterPmntMethod GetProcessingCenterPmntMethod(PX.Objects.AR.ExternalTransaction extTran)
  {
    using (IEnumerator<PXResult<PX.Objects.AR.ARPayment>> enumerator = ((PXSelectBase<PX.Objects.AR.ARPayment>) new FbqlSelect<SelectFromBase<PX.Objects.AR.ARPayment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CA.PaymentMethod>.On<BqlOperand<PX.Objects.AR.ARPayment.paymentMethodID, IBqlString>.IsEqual<PX.Objects.CA.PaymentMethod.paymentMethodID>>>, FbqlJoins.Inner<CCProcessingCenterPmntMethod>.On<BqlOperand<CCProcessingCenterPmntMethod.paymentMethodID, IBqlString>.IsEqual<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARPayment.docType, Equal<P.AsString>>>>, And<BqlOperand<PX.Objects.AR.ARPayment.refNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<CCProcessingCenterPmntMethod.processingCenterID, IBqlString>.IsEqual<P.AsString>>>, PX.Objects.AR.ARPayment>.View(this.Graph)).Select(new object[3]
    {
      (object) extTran.DocType,
      (object) extTran.RefNbr,
      (object) extTran.ProcessingCenterID
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
        return PXResult<PX.Objects.AR.ARPayment, PX.Objects.CA.PaymentMethod, CCProcessingCenterPmntMethod>.op_Implicit((PXResult<PX.Objects.AR.ARPayment, PX.Objects.CA.PaymentMethod, CCProcessingCenterPmntMethod>) enumerator.Current);
    }
    return (CCProcessingCenterPmntMethod) null;
  }

  public void Save()
  {
    PXAction action = this.Graph.Actions[nameof (Save)];
    if (action != null)
      action.Press();
    else
      this.Graph.Actions.PressSave();
  }

  public CCProcessingCenter GetCCProcessingCenter(string processingCenterID)
  {
    return CCProcessingCenter.PK.Find(this.Graph, processingCenterID);
  }

  public CCProcessingCenter FindProcessingCenter(int? pMInstanceID, string aCuryId)
  {
    return this._processingCenterRepository.FindProcessingCenter(pMInstanceID, aCuryId);
  }

  public CCProcTran FindVerifyingCCProcTran(int? pMInstanceID)
  {
    return this._cctranRepository.FindVerifyingCCProcTran(pMInstanceID);
  }

  public PXResult<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.Customer> FindCustomerAndPaymentMethod(
    int? pMInstanceID)
  {
    return this._cpmRepository.FindCustomerAndPaymentMethod(pMInstanceID);
  }

  public PXResultset<CCProcessingCenterDetail> FindAllProcessingCenterDetails(
    string processingCenterID)
  {
    return this._processingCenterDetailRepository.FindAllProcessingCenterDetails(processingCenterID);
  }

  private void InitializeRepositories(PXGraph graph)
  {
    this._cctranRepository = new CCProcTranRepository(graph);
    this._cpmRepository = new CustomerPaymentMethodRepository(graph);
    this._cpmDetailRepository = new CustomerPaymentMethodDetailRepository(graph);
    this._processingCenterRepository = new CCProcessingCenterRepository(graph);
    this._processingCenterDetailRepository = new CCProcessingCenterDetailRepository(graph);
    this._externalTran = new ExternalTransactionRepository(graph);
  }

  public IEnumerable<CCProcTran> GetCCProcTranByTranID(int? transactionId)
  {
    return this._cctranRepository.GetCCProcTranByTranID(transactionId);
  }

  public PX.Objects.AR.ExternalTransaction FindCapturedExternalTransaction(
    int? pMInstanceID,
    string refTranNbr)
  {
    return this._externalTran.FindCapturedExternalTransaction(pMInstanceID, refTranNbr);
  }

  public IEnumerable<PX.Objects.AR.ExternalTransaction> GetExternalTransactionsByPayLinkID(
    int? payLinkId)
  {
    return this._externalTran.GetExternalTransactionsByPayLinkID(payLinkId);
  }

  public PX.Objects.AR.ExternalTransaction FindCapturedExternalTransaction(
    string procCenterId,
    string tranNbr)
  {
    return this._externalTran.FindCapturedExternalTransaction(procCenterId, tranNbr);
  }

  public Tuple<PX.Objects.AR.ExternalTransaction, PX.Objects.AR.ARPayment> GetExternalTransactionWithPayment(
    string tranNbr,
    string procCenterId)
  {
    return this._externalTran.GetExternalTransactionWithPayment(tranNbr, procCenterId);
  }

  public Tuple<PX.Objects.AR.ExternalTransaction, PX.Objects.AR.ARPayment> GetExternalTransactionWithPaymentByApiNumber(
    string tranApiNbr,
    string procCenterId)
  {
    return this._externalTran.GetExternalTransactionWithPaymentByApiNumber(tranApiNbr, procCenterId);
  }

  public PX.Objects.CA.PaymentMethod GetPaymentMethod(string paymentMehtodID)
  {
    return PX.Objects.CA.PaymentMethod.PK.Find(this.Graph, paymentMehtodID);
  }

  public CCProcessingCenter GetProcessingCenterByID(string procCenterId)
  {
    return this._processingCenterRepository.GetProcessingCenterByID(procCenterId);
  }
}
