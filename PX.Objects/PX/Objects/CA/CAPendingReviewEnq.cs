// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAPendingReviewEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.Standalone;
using PX.Objects.CS;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.CA;

public class CAPendingReviewEnq : PXGraph<
#nullable disable
CAPendingReviewEnq>
{
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<CAPendingReviewEnq.ARPaymentInfo, LeftJoin<PX.Objects.AR.ExternalTransaction, On<PX.Objects.AR.ARPayment.cCActualExternalTransactionID, Equal<PX.Objects.AR.ExternalTransaction.transactionID>>>> Documents;
  public PXAction<CAPendingReviewEnq.ARPaymentInfo> RedirectToDoc;
  public PXAction<CAPendingReviewEnq.ARPaymentInfo> RedirectToProcCenter;
  public PXAction<CAPendingReviewEnq.ARPaymentInfo> RedirectToPaymentMethod;
  public PXAction<PX.Objects.AR.Customer> RedirectToCustomer;

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false, Required = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<FeaturesSet.branch>>.Or<FeatureInstalled<FeaturesSet.multiCompany>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CAPendingReviewEnq.ARPaymentInfo.branchID> e)
  {
  }

  protected virtual IEnumerable documents()
  {
    CAPendingReviewEnq graph = this;
    foreach (PXResult<CAPendingReviewEnq.ARPaymentInfo, PX.Objects.AR.Customer, PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.ExternalTransaction> pxResult in ((PXSelectBase<CAPendingReviewEnq.ARPaymentInfo>) new PXSelectJoin<CAPendingReviewEnq.ARPaymentInfo, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.ARPayment.customerID>>, LeftJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<PX.Objects.AR.ARPayment.pMInstanceID>>, LeftJoin<PX.Objects.AR.ExternalTransaction, On<PX.Objects.AR.ExternalTransaction.transactionID, Equal<PX.Objects.AR.ARPayment.cCActualExternalTransactionID>>>>>, Where<PX.Objects.AR.ARPayment.isCCUserAttention, Equal<True>, And<PX.Objects.AR.ARPayment.hold, Equal<False>, And<PX.Objects.AR.ARPayment.status, NotIn3<ARDocStatus.voided, ARDocStatus.closed>, And<PX.Objects.AR.ARPayment.isMigratedRecord, NotEqual<True>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>, OrderBy<Asc<PX.Objects.AR.ARPayment.refNbr>>>((PXGraph) graph)).SelectWithViewContext(Array.Empty<object>()))
    {
      CAPendingReviewEnq.ARPaymentInfo arPaymentInfo = PXResult<CAPendingReviewEnq.ARPaymentInfo, PX.Objects.AR.Customer, PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult);
      PX.Objects.AR.Customer customer = PXResult<CAPendingReviewEnq.ARPaymentInfo, PX.Objects.AR.Customer, PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult);
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PXResult<CAPendingReviewEnq.ARPaymentInfo, PX.Objects.AR.Customer, PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult);
      PX.Objects.AR.ExternalTransaction extTran = PXResult<CAPendingReviewEnq.ARPaymentInfo, PX.Objects.AR.Customer, PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult);
      if (customerPaymentMethod != null && customerPaymentMethod.PMInstanceID.HasValue)
        arPaymentInfo.PMInstanceDescr = customerPaymentMethod.Descr;
      arPaymentInfo.CustomerCD = customer?.AcctCD;
      ExternalTransactionState transactionState = new ExternalTransactionState();
      if (extTran != null && extTran.TransactionID.HasValue)
        transactionState = ExternalTranHelper.GetTransactionState((PXGraph) graph, (IExternalTransaction) extTran);
      arPaymentInfo.CCPaymentStateDescr = transactionState.Description;
      arPaymentInfo.IsOpenedForReview = new bool?(transactionState.IsOpenForReview);
      if (!string.IsNullOrEmpty(extTran?.SyncStatus))
        arPaymentInfo.ValidationStatus = extTran.SyncMessage;
      yield return (object) arPaymentInfo;
    }
    PXView.StartRow = 0;
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable redirectToDoc(PXAdapter adapter)
  {
    CAPendingReviewEnq.ARPaymentInfo current = ((PXSelectBase<CAPendingReviewEnq.ARPaymentInfo>) this.Documents).Current;
    if (current.DocType == "CSL" || current.DocType == "RCS")
    {
      ARCashSaleEntry instance = PXGraph.CreateInstance<ARCashSaleEntry>();
      ((PXSelectBase<ARCashSale>) instance.Document).Current = PXResultset<ARCashSale>.op_Implicit(((PXSelectBase<ARCashSale>) instance.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) current.RefNbr, new object[1]
      {
        (object) current.DocType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "AR Payment");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    ARPaymentEntry instance1 = PXGraph.CreateInstance<ARPaymentEntry>();
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance1.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) instance1.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) current.RefNbr, new object[1]
    {
      (object) current.DocType
    }));
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, true, "AR Payment");
    ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException1;
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable redirectToProcCenter(PXAdapter adapter)
  {
    PX.Objects.AR.ARPayment current = (PX.Objects.AR.ARPayment) ((PXSelectBase<CAPendingReviewEnq.ARPaymentInfo>) this.Documents).Current;
    CCProcessingCenterMaint instance = PXGraph.CreateInstance<CCProcessingCenterMaint>();
    ((PXSelectBase<CCProcessingCenter>) instance.ProcessingCenter).Current = PXResultset<CCProcessingCenter>.op_Implicit(((PXSelectBase<CCProcessingCenter>) instance.ProcessingCenter).Search<CCProcessingCenter.processingCenterID>((object) current.ProcessingCenterID, Array.Empty<object>()));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Processing Center");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable redirectToPaymentMethod(PXAdapter adapter)
  {
    CAPendingReviewEnq.ARPaymentInfo current = ((PXSelectBase<CAPendingReviewEnq.ARPaymentInfo>) this.Documents).Current;
    if (string.IsNullOrEmpty(current.PMInstanceDescr))
    {
      PaymentMethodMaint instance = PXGraph.CreateInstance<PaymentMethodMaint>();
      ((PXSelectBase<PaymentMethod>) instance.PaymentMethod).Current = PXResultset<PaymentMethod>.op_Implicit(((PXSelectBase<PaymentMethod>) instance.PaymentMethod).Search<PaymentMethod.paymentMethodID>((object) current.PaymentMethodID, Array.Empty<object>()));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Payment Method");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    CustomerPaymentMethodMaint instance1 = PXGraph.CreateInstance<CustomerPaymentMethodMaint>();
    ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) instance1.CustomerPaymentMethod).Current = PXResultset<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) instance1.CustomerPaymentMethod).Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>((object) current.PMInstanceID, new object[1]
    {
      (object) current.CustomerCD
    }));
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, true, "Customer Payment Method");
    ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException1;
  }

  [PXSuppressActionValidation]
  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable redirectToCustomer(PXAdapter adapter)
  {
    CAPendingReviewEnq.ARPaymentInfo current = ((PXSelectBase<CAPendingReviewEnq.ARPaymentInfo>) this.Documents).Current;
    CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
    ((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Current = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Search<PX.Objects.AR.Customer.bAccountID>((object) current.CustomerID, Array.Empty<object>()));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Customer");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  protected virtual void ARPaymentInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ((PXSelectBase) this.Documents).AllowUpdate = false;
    ((PXSelectBase) this.Documents).AllowInsert = false;
    ((PXSelectBase) this.Documents).AllowDelete = false;
    if (!(e.Row is CAPendingReviewEnq.ARPaymentInfo row) || !row.IsOpenedForReview.GetValueOrDefault())
      return;
    sender.RaiseExceptionHandling<PX.Objects.AR.ARPayment.cCPaymentStateDescr>((object) row, (object) row.CCPaymentStateDescr, (Exception) new PXSetPropertyException("The transaction is held for review by the processing center. Use the processing center interface to approve or reject the transaction.", (PXErrorLevel) 3));
  }

  [PXHidden]
  [Serializable]
  public class ARPaymentInfo : PX.Objects.AR.ARPayment
  {
    [PXString(255 /*0xFF*/)]
    [PXUIField(DisplayName = "Card/Account Nbr.", Enabled = false)]
    public virtual string PMInstanceDescr { get; set; }

    [PXString(255 /*0xFF*/)]
    [PXUIField(DisplayName = "Validation Status")]
    public virtual string ValidationStatus { get; set; }

    [PXString(255 /*0xFF*/)]
    public virtual string CustomerCD { get; set; }

    [PXBool]
    public virtual bool? IsOpenedForReview { get; set; }

    public abstract class pMInstanceDescr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAPendingReviewEnq.ARPaymentInfo.pMInstanceDescr>
    {
    }

    public abstract class validationStatus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAPendingReviewEnq.ARPaymentInfo.validationStatus>
    {
    }

    public abstract class customerCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAPendingReviewEnq.ARPaymentInfo.customerCD>
    {
    }

    public abstract class isOpenedForReview : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAPendingReviewEnq.ARPaymentInfo.isOpenedForReview>
    {
    }

    public new abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CAPendingReviewEnq.ARPaymentInfo.branchID>
    {
    }
  }
}
