// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentsAutoProcessing
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.AR.Repositories;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.Extensions.PaymentTransaction;
using PX.Objects.GL;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
[Serializable]
public class ARPaymentsAutoProcessing : PXGraph<
#nullable disable
ARPaymentsAutoProcessing>
{
  private CCProcTranRepository tranRepo;
  public PXFilter<ARPaymentsAutoProcessing.PaymentFilter> Filter;
  public PXCancel<ARPaymentsAutoProcessing.PaymentFilter> Cancel;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXAction<ARPaymentsAutoProcessing.PaymentFilter> EditDetail;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<ARPaymentInfo, ARPaymentsAutoProcessing.PaymentFilter> ARDocumentList;
  public ToggleCurrency<ARPaymentsAutoProcessing.PaymentFilter> CurrencyView;
  public PXSelect<PX.Objects.CM.CurrencyInfo> currencyinfo;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  public CMSetupSelect CMSetup;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<FeaturesSet.branch>>.Or<FeatureInstalled<FeaturesSet.multiCompany>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARPaymentInfo.branchID> e)
  {
  }

  public ARPaymentsAutoProcessing()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    PXCurrencyAttribute.SetBaseCalc<ARPaymentsAutoProcessing.PaymentFilter.curySelTotal>(((PXSelectBase) this.Filter).Cache, (object) null, false);
    ((PXProcessingBase<ARPaymentInfo>) this.ARDocumentList).SetSelected<ARPayment.selected>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<ARPaymentInfo>) this.ARDocumentList).SetProcessDelegate<ARPaymentCCProcessing>(ARPaymentsAutoProcessing.\u003C\u003Ec.\u003C\u003E9__3_0 ?? (ARPaymentsAutoProcessing.\u003C\u003Ec.\u003C\u003E9__3_0 = new PXProcessingBase<ARPaymentInfo>.ProcessItemDelegate<ARPaymentCCProcessing>((object) ARPaymentsAutoProcessing.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__3_0))));
    ((PXSelectBase) this.ARDocumentList).Cache.AllowInsert = false;
    ((PXProcessingBase<ARPaymentInfo>) this.ARDocumentList).ParallelProcessingOptions = (Action<PXParallelProcessingOptions>) (settings => settings.IsEnabled = true);
    this.tranRepo = new CCProcTranRepository((PXGraph) this);
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable editDetail(PXAdapter adapter)
  {
    ARPayment current = (ARPayment) ((PXSelectBase<ARPaymentInfo>) this.ARDocumentList).Current;
    if (current != null)
    {
      ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
      ((PXSelectBase<ARPayment>) instance.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) current.RefNbr, new object[1]
      {
        (object) current.DocType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Payment");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  protected virtual IEnumerable ardocumentlist()
  {
    ARPaymentsAutoProcessing graph = this;
    DateTime now = DateTime.Now.Date;
    foreach (PXResult<ARPaymentInfo, Customer, PX.Objects.CA.PaymentMethod, CustomerPaymentMethod, ExternalTransaction> pxResult in ((PXSelectBase<ARPaymentInfo>) new PXSelectJoinGroupBy<ARPaymentInfo, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARPayment.customerID>>, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<ARPayment.paymentMethodID>, And<PX.Objects.CA.PaymentMethod.paymentType, In3<PaymentMethodType.creditCard, PaymentMethodType.eft>, And<PX.Objects.CA.PaymentMethod.aRIsProcessingRequired, Equal<True>>>>, LeftJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.pMInstanceID, Equal<ARPayment.pMInstanceID>>, LeftJoin<ExternalTransaction, On<ExternalTransaction.transactionID, Equal<ARPayment.cCActualExternalTransactionID>>, LeftJoin<ARAdjust, On<ARAdjust.adjgDocType, Equal<ARPayment.docType>, And<ARAdjust.adjgRefNbr, Equal<ARPayment.refNbr>>>, LeftJoin<PX.Objects.AR.Standalone.ARRegister, On<PX.Objects.AR.Standalone.ARRegister.docType, Equal<ARAdjust.adjdDocType>, And<PX.Objects.AR.Standalone.ARRegister.refNbr, Equal<ARAdjust.adjdRefNbr>, And<PX.Objects.AR.Standalone.ARRegister.released, Equal<False>>>>, LeftJoin<SOAdjust, On<SOAdjust.adjgDocType, Equal<ARPayment.docType>, And<SOAdjust.adjgRefNbr, Equal<ARPayment.refNbr>>>>>>>>>>, Where<ARPayment.released, Equal<False>, And<ARPayment.hold, Equal<False>, And<ARPayment.voided, Equal<False>, And<ARPayment.isCCCaptured, Equal<False>, And<ARPayment.docDate, LessEqual<Current<ARPaymentsAutoProcessing.PaymentFilter.payDate>>, And<ARPayment.isMigratedRecord, NotEqual<True>, And<PX.Objects.AR.Standalone.ARRegister.refNbr, IsNull, And<SOAdjust.adjdOrderNbr, IsNull, And2<Where<ARPayment.docType, Equal<ARDocType.payment>, Or<ARPayment.docType, Equal<ARDocType.prepayment>>>, And2<Where<Customer.statementCycleId, Equal<Current<ARPaymentsAutoProcessing.PaymentFilter.statementCycleId>>, Or<Current<ARPaymentsAutoProcessing.PaymentFilter.statementCycleId>, IsNull>>, And2<Where<Customer.bAccountID, Equal<Current<ARPaymentsAutoProcessing.PaymentFilter.customerID>>, Or<Current<ARPaymentsAutoProcessing.PaymentFilter.customerID>, IsNull>>, And2<Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<ARPaymentsAutoProcessing.PaymentFilter.paymentMethodID>>, Or<Current<ARPaymentsAutoProcessing.PaymentFilter.paymentMethodID>, IsNull>>, And2<Where<ARPayment.processingCenterID, Equal<Current<ARPaymentsAutoProcessing.PaymentFilter.processingCenterID>>, Or<CustomerPaymentMethod.cCProcessingCenterID, Equal<Current<ARPaymentsAutoProcessing.PaymentFilter.processingCenterID>>, Or<Current<ARPaymentsAutoProcessing.PaymentFilter.processingCenterID>, IsNull>>>, And<Match<Customer, Current<AccessInfo.userName>>>>>>>>>>>>>>>>, Aggregate<GroupBy<ARPayment.docType, GroupBy<ARPayment.refNbr>>>, OrderBy<Asc<ARPayment.refNbr>>>((PXGraph) graph)).Select(Array.Empty<object>()))
    {
      ARPaymentInfo aDoc = PXResult<ARPaymentInfo, Customer, PX.Objects.CA.PaymentMethod, CustomerPaymentMethod, ExternalTransaction>.op_Implicit(pxResult);
      CustomerPaymentMethod customerPaymentMethod = PXResult<ARPaymentInfo, Customer, PX.Objects.CA.PaymentMethod, CustomerPaymentMethod, ExternalTransaction>.op_Implicit(pxResult);
      ExternalTransaction extTran = PXResult<ARPaymentInfo, Customer, PX.Objects.CA.PaymentMethod, CustomerPaymentMethod, ExternalTransaction>.op_Implicit(pxResult);
      ARDocKey arDocKey = new ARDocKey((ARRegister) aDoc);
      int? nullable1;
      if (customerPaymentMethod != null)
      {
        nullable1 = customerPaymentMethod.PMInstanceID;
        if (nullable1.HasValue)
        {
          aDoc.PMInstanceDescr = customerPaymentMethod.Descr;
          ARPaymentInfo arPaymentInfo = aDoc;
          DateTime? expirationDate = customerPaymentMethod.ExpirationDate;
          DateTime dateTime = now;
          bool? nullable2 = new bool?(expirationDate.HasValue && expirationDate.GetValueOrDefault() < dateTime);
          arPaymentInfo.IsCCExpired = nullable2;
        }
      }
      aDoc.ProcessingCenterID = customerPaymentMethod != null ? customerPaymentMethod.CCProcessingCenterID : aDoc.ProcessingCenterID;
      ExternalTransactionState transactionState = new ExternalTransactionState();
      if (extTran != null && extTran.Active.GetValueOrDefault())
        transactionState = ExternalTranHelper.GetTransactionState((PXGraph) graph, (IExternalTransaction) extTran);
      ExternalTransaction externalTransaction1 = (ExternalTransaction) transactionState.ExternalTransaction;
      aDoc.CCPaymentStateDescr = transactionState.Description;
      int? nullable3;
      if (transactionState == null)
      {
        nullable1 = new int?();
        nullable3 = nullable1;
      }
      else
      {
        IExternalTransaction externalTransaction2 = transactionState.ExternalTransaction;
        if (externalTransaction2 == null)
        {
          nullable1 = new int?();
          nullable3 = nullable1;
        }
        else
          nullable3 = externalTransaction2.TransactionID;
      }
      int? transactionId = nullable3;
      if (transactionState.HasErrors && transactionId.HasValue)
      {
        string str = graph.tranRepo.GetCCProcTranByTranID(transactionId).OrderByDescending<CCProcTran, int?>((Func<CCProcTran, int?>) (i => i.TranNbr)).Select<CCProcTran, string>((Func<CCProcTran, string>) (i => i.ErrorText)).FirstOrDefault<string>();
        aDoc.CCTranDescr = str;
      }
      if (aDoc.IsCCExpired.GetValueOrDefault() && string.IsNullOrEmpty(aDoc.CCTranDescr))
        aDoc.CCTranDescr = "CC Expired";
      nullable1 = aDoc.PMInstanceID;
      int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
      if (!(nullable1.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & nullable1.HasValue == newPaymentProfile.HasValue) || transactionState.IsCaptured && transactionState.IsOpenForReview || transactionState.IsPreAuthorized)
        yield return (object) aDoc;
    }
  }

  public static void ProcessPayment(ARPaymentCCProcessing aProcessingGraph, ARPaymentInfo aDoc)
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    ((PXSelectBase<ARPayment>) instance.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) aDoc.RefNbr, new object[1]
    {
      (object) aDoc.DocType
    }));
    if (((PXSelectBase<ARPayment>) instance.Document).Current.IsCCUserAttention.GetValueOrDefault())
      throw new Exception("The document cannot be processed automatically. Please review the document.");
    ((PXGraph) instance).GetExtension<ARPaymentEntryPaymentTransaction>().CaptureCCPayment(new PXAdapter((PXView) new PXView.Dummy((PXGraph) instance, ((PXSelectBase) instance.Document).View.BqlSelect, new List<object>()
    {
      (object) ((PXSelectBase<ARPayment>) instance.Document).Current
    })));
  }

  protected virtual void PaymentFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARPaymentsAutoProcessing.PaymentFilter row))
      return;
    PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.CurrencyInfo_CuryInfoID).Select(new object[1]
    {
      (object) row.CuryInfoID
    }));
    int count = ((PXSelectBase<ARPaymentInfo>) this.ARDocumentList).Select(Array.Empty<object>()).Count;
  }

  protected virtual void PaymentFilter_CustomerID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.ARDocumentList).Cache.Clear();
  }

  protected virtual void PaymentFilter_PayDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    foreach (PXResult<PX.Objects.CM.CurrencyInfo> pxResult in PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<ARPaymentsAutoProcessing.PaymentFilter.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null))
      ((PXSelectBase) this.currencyinfo).Cache.SetDefaultExt<PX.Objects.CM.CurrencyInfo.curyEffDate>((object) PXResult<PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult));
    ((PXSelectBase) this.ARDocumentList).Cache.Clear();
  }

  protected virtual void PaymentFilter_PaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.ARDocumentList).Cache.Clear();
  }

  protected virtual void PaymentFilter_ProcessingCenterID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.ARDocumentList).Cache.Clear();
  }

  protected virtual void PaymentFilter_StatementCycleID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.ARDocumentList).Cache.Clear();
  }

  protected virtual void ARPaymentInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARPaymentInfo row1))
      return;
    PXUIFieldAttribute.SetEnabled<ARPayment.docType>(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<ARPayment.refNbr>(sender, e.Row, false);
    PXCache pxCache = sender;
    object row2 = e.Row;
    bool? isCcUserAttention = row1.IsCCUserAttention;
    bool flag = false;
    int num = isCcUserAttention.GetValueOrDefault() == flag & isCcUserAttention.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<ARPayment.selected>(pxCache, row2, num != 0);
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (row1 != null)
    {
      isCcUserAttention = row1.IsCCUserAttention;
      if (isCcUserAttention.GetValueOrDefault())
        propertyException = new PXSetPropertyException("The document cannot be processed automatically. Please review the document.", (PXErrorLevel) 3);
    }
    sender.RaiseExceptionHandling<ARPayment.refNbr>((object) row1, (object) row1.CCPaymentStateDescr, (Exception) propertyException);
  }

  [Serializable]
  public class PaymentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _PayDate;
    protected string _StatementCycleId;
    protected int? _CustomerID;
    protected Decimal? _Balance;
    protected Decimal? _CurySelTotal;
    protected Decimal? _SelTotal;
    protected string _CuryID;
    protected long? _CuryInfoID;
    protected string _PaymentMethodID;
    protected string _ProcessingCenterID;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? PayDate
    {
      get => this._PayDate;
      set => this._PayDate = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXUIField(DisplayName = "Statement Cycle ID")]
    [PXSelector(typeof (ARStatementCycle.statementCycleId))]
    public virtual string StatementCycleId
    {
      get => this._StatementCycleId;
      set => this._StatementCycleId = value;
    }

    [Customer]
    [PXDefault]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBDecimal(4)]
    [PXUIField]
    public virtual Decimal? Balance
    {
      get => this._Balance;
      set => this._Balance = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (ARPaymentsAutoProcessing.PaymentFilter.curyInfoID), typeof (ARPaymentsAutoProcessing.PaymentFilter.selTotal))]
    [PXUIField]
    public virtual Decimal? CurySelTotal
    {
      get => this._CurySelTotal;
      set => this._CurySelTotal = value;
    }

    [PXDBDecimal(4)]
    public virtual Decimal? SelTotal
    {
      get => this._SelTotal;
      set => this._SelTotal = value;
    }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXUIField]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXDBLong]
    [CurrencyInfo(ModuleCode = "AP")]
    public virtual long? CuryInfoID
    {
      get => this._CuryInfoID;
      set => this._CuryInfoID = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXUIField]
    [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.isActive, Equal<True>, And<PX.Objects.CA.PaymentMethod.aRIsProcessingRequired, Equal<True>, And<PX.Objects.CA.PaymentMethod.paymentType, In3<PaymentMethodType.creditCard, PaymentMethodType.eft>>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
    public virtual string PaymentMethodID
    {
      get => this._PaymentMethodID;
      set => this._PaymentMethodID = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (Search<CCProcessingCenter.processingCenterID>), DescriptionField = typeof (CCProcessingCenter.name))]
    [PXUIField(DisplayName = "Processing Center ID")]
    [DeprecatedProcessing(ChckVal = DeprecatedProcessingAttribute.CheckVal.ProcessingCenterId)]
    [DisabledProcCenter(CheckFieldValue = DisabledProcCenterAttribute.CheckFieldVal.ProcessingCenterId)]
    public virtual string ProcessingCenterID
    {
      get => this._ProcessingCenterID;
      set => this._ProcessingCenterID = value;
    }

    public abstract class payDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARPaymentsAutoProcessing.PaymentFilter.payDate>
    {
    }

    public abstract class statementCycleId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPaymentsAutoProcessing.PaymentFilter.statementCycleId>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARPaymentsAutoProcessing.PaymentFilter.customerID>
    {
    }

    public abstract class balance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARPaymentsAutoProcessing.PaymentFilter.balance>
    {
    }

    public abstract class curySelTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARPaymentsAutoProcessing.PaymentFilter.curySelTotal>
    {
    }

    public abstract class selTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARPaymentsAutoProcessing.PaymentFilter.selTotal>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPaymentsAutoProcessing.PaymentFilter.curyID>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      ARPaymentsAutoProcessing.PaymentFilter.curyInfoID>
    {
    }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPaymentsAutoProcessing.PaymentFilter.paymentMethodID>
    {
    }

    public abstract class processingCenterID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPaymentsAutoProcessing.PaymentFilter.processingCenterID>
    {
    }
  }
}
