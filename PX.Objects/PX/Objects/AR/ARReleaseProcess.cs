// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARReleaseProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR.BQL;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.Overrides.ARDocumentRelease;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.EntityInUse;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.AR;
using PX.Objects.GL;
using PX.Objects.GL.DAC.Abstract;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.InventoryRelease.Accumulators.Statistics.ItemCustomer;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.AR;

public class ARReleaseProcess : PXGraph<
#nullable disable
ARReleaseProcess>
{
  public PXSelect<ARRegister> ARDocument;
  public PXSelectJoin<ARTran, LeftJoin<ARTax, On<ARTax.tranType, Equal<ARTran.tranType>, And<ARTax.refNbr, Equal<ARTran.refNbr>, And<ARTax.lineNbr, Equal<ARTran.lineNbr>>>>, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTax.taxID>>, LeftJoin<DRDeferredCode, On<DRDeferredCode.deferredCodeID, Equal<ARTran.deferredCode>>, LeftJoin<PX.Objects.SO.SOOrderType, On<PX.Objects.SO.SOOrderType.orderType, Equal<ARTran.sOOrderType>>, LeftJoin<ARTaxTran, On<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<ARTax.tranType>, And<ARTaxTran.refNbr, Equal<ARTax.refNbr>, And<ARTaxTran.taxID, Equal<ARTax.taxID>>>>>>>>>>, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>>>, OrderBy<Asc<ARTran.lineNbr, Asc<PX.Objects.TX.Tax.taxCalcLevel>>>> ARTran_TranType_RefNbr;
  public PXSelectJoin<ARTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTaxTran.taxID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<ARTaxTran.accountID>>>>, Where<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<Required<ARTaxTran.tranType>>, And<ARTaxTran.refNbr, Equal<Required<ARTaxTran.refNbr>>>>>, OrderBy<Asc<PX.Objects.TX.Tax.taxCalcLevel>>> ARTaxTran_TranType_RefNbr;
  public PXSelect<SVATConversionHist> SVATConversionHistory;
  public FbqlSelect<SelectFromBase<SVATConversionHist, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SVATConversionHist.module, 
  #nullable disable
  Equal<BatchModule.moduleAR>>>>, And<BqlOperand<
  #nullable enable
  SVATConversionHist.adjdDocType, IBqlString>.IsEqual<
  #nullable disable
  P.AsString.ASCII>>>, And<BqlOperand<
  #nullable enable
  SVATConversionHist.adjdRefNbr, IBqlString>.IsEqual<
  #nullable disable
  P.AsString>>>, And<BqlOperand<
  #nullable enable
  SVATConversionHist.taxID, IBqlString>.IsEqual<
  #nullable disable
  P.AsString>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SVATConversionHist.adjdDocType, 
  #nullable disable
  NotEqual<SVATConversionHist.adjgDocType>>>>>.Or<BqlOperand<
  #nullable enable
  SVATConversionHist.adjdRefNbr, IBqlString>.IsNotEqual<
  #nullable disable
  SVATConversionHist.adjgRefNbr>>>>>, SVATConversionHist>.View SVATRecognitionRecords;
  public PXSelect<PX.Objects.GL.Batch> Batch;
  public ARInvoice_CurrencyInfo_Terms_Customer ARInvoice_DocType_RefNbr;
  public ARPayment_CurrencyInfo_Currency_Customer ARPayment_DocType_RefNbr;
  public PXSelect<PX.Objects.AR.Standalone.ARInvoice, Where<PX.Objects.AR.Standalone.ARInvoice.docType, Equal<Required<PX.Objects.AR.Standalone.ARInvoice.docType>>, And<PX.Objects.AR.Standalone.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.Standalone.ARInvoice.refNbr>>>>> StandaloneARInvoice;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  public PXSelectJoin<ARAdjust, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARAdjust.adjdCuryInfoID>>, InnerJoin<PX.Objects.CM.Extensions.Currency, On<PX.Objects.CM.Extensions.Currency.curyID, Equal<PX.Objects.CM.Extensions.CurrencyInfo.curyID>>, InnerJoin<ARRegister, On<ARRegister.docType, Equal<ARAdjust.adjdDocType>, And<ARRegister.refNbr, Equal<ARAdjust.adjdRefNbr>>>, LeftJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARAdjust.adjdDocType>, And<ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>, LeftJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARAdjust.adjdDocType>, And<ARPayment.refNbr, Equal<ARAdjust.adjdRefNbr>>>, LeftJoin<ARTran, On<ARRegister.paymentsByLinesAllowed, Equal<True>, And<ARTran.tranType, Equal<ARAdjust.adjdDocType>, And<ARTran.refNbr, Equal<ARAdjust.adjdRefNbr>, And<ARTran.lineNbr, Equal<ARAdjust.adjdLineNbr>>>>>, LeftJoin<CurrencyInfo2, On<CurrencyInfo2.curyInfoID, Equal<ARRegister.curyInfoID>>>>>>>>>, Where<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<Where<Switch<Case<Where<Required<ARAdjust.released>, Equal<True>>, IIf<Where<ARAdjust.adjNbr, Equal<Required<ARAdjust.adjNbr>>>, True, False>>, IIf<Where<ARAdjust.released, NotEqual<True>>, True, False>>, Equal<True>>>>>, OrderBy<Asc<ARAdjust.adjdDocType, Asc<ARAdjust.adjdRefNbr, Asc<ARAdjust.adjdLineNbr>>>>> ARAdjust_AdjgDocType_RefNbr_CustomerID;
  public PXSelectJoin<ARAdjust, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARAdjust.adjdCuryInfoID>>, InnerJoin<PX.Objects.CM.Extensions.Currency, On<PX.Objects.CM.Extensions.Currency.curyID, Equal<PX.Objects.CM.Extensions.CurrencyInfo.curyID>>, LeftJoin<ARPayment, On<ARPayment.docType, Equal<ARAdjust.adjgDocType>, And<ARPayment.refNbr, Equal<ARAdjust.adjgRefNbr>>>>>>, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<Where<ARAdjust.released, NotEqual<True>, Or<Required<ARAdjust.released>, Equal<True>, And<ARAdjust.adjNbr, Equal<Required<ARAdjust.adjNbr>>>>>>>>> ARAdjust_AdjdDocType_RefNbr_CustomerID;
  public PXSelect<ARPaymentChargeTran, Where<ARPaymentChargeTran.docType, Equal<Required<ARPaymentChargeTran.docType>>, And<ARPaymentChargeTran.refNbr, Equal<Required<ARPaymentChargeTran.refNbr>>>>> ARPaymentChargeTran_DocType_RefNbr;
  public PXSelect<ARSalesPerTran, Where<ARSalesPerTran.docType, Equal<Required<ARSalesPerTran.docType>>, And<ARSalesPerTran.refNbr, Equal<Required<ARSalesPerTran.refNbr>>>>> ARDoc_SalesPerTrans;
  public PXSelect<ARTranPost, Where<ARTranPost.docType, Equal<Required<ARRegister.docType>>, And<ARTranPost.refNbr, Equal<Required<ARRegister.refNbr>>>>> TranPost;
  public PXSelect<CATran> CashTran;
  public PXSetup<GLSetup> glsetup;
  public PXSetup<DRSetup> drSetup;
  public PXSelect<PX.Objects.TX.Tax> taxes;
  public PXSelect<PX.Objects.SO.SOOrder> soOrder;
  public PXSelect<SOAdjust> soAdjust;
  private ARSetup _arsetup;
  public PXSelect<CADailySummary> caDailySummary;
  public Dictionary<ARReleaseProcess.ARTranPostKey, ARTranPost> tranPostRetainagePayments;
  protected ARInvoiceEntry _ie;
  protected ARPaymentEntry _pe;
  protected DRProcess _dr;
  public PXSelect<ARStatementDetail, Where<ARStatementDetail.docType, Equal<Required<ARStatementDetail.docType>>, And<ARStatementDetail.refNbr, Equal<Required<ARStatementDetail.refNbr>>>>> StatementDetailsView;
  private OldInvoiceDateRefresher _oldInvoiceRefresher = new OldInvoiceDateRefresher();
  protected bool _IsIntegrityCheck;
  protected string _IntegrityCheckStartingPeriod;

  public ARSetup arsetup
  {
    get
    {
      this._arsetup = this._arsetup ?? PXResultset<ARSetup>.op_Implicit(PXSetup<ARSetup>.Select((PXGraph) this, Array.Empty<object>()));
      return this._arsetup;
    }
  }

  public bool AutoPost => this.arsetup.AutoPost.GetValueOrDefault();

  public bool SummPost => this.arsetup.TransactionPosting == "S";

  public string InvoiceRounding => this.arsetup.InvoiceRounding;

  public Decimal? InvoicePrecision => this.arsetup.InvoicePrecision;

  public bool? IsMigrationMode => this.arsetup.MigrationMode;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  public bool IsMigratedDocumentForProcessing(ARRegister doc)
  {
    bool flag = doc.DocType == "CSL" || doc.DocType == "RCS";
    bool? nullable = doc.IsMigratedRecord;
    if (nullable.GetValueOrDefault())
    {
      nullable = doc.Released;
      if (!nullable.GetValueOrDefault())
      {
        Decimal? curyInitDocBal = doc.CuryInitDocBal;
        Decimal? curyOrigDocAmt = doc.CuryOrigDocAmt;
        if (!(curyInitDocBal.GetValueOrDefault() == curyOrigDocAmt.GetValueOrDefault() & curyInitDocBal.HasValue == curyOrigDocAmt.HasValue))
          return !flag;
      }
    }
    return false;
  }

  public ARInvoiceEntry InvoiceEntryGraph
  {
    get
    {
      this._ie = this._ie ?? PXGraph.CreateInstance<ARInvoiceEntry>();
      return this._ie;
    }
  }

  public ARPaymentEntry pe
  {
    get
    {
      this._pe = this._pe ?? PXGraph.CreateInstance<ARPaymentEntry>();
      return this._pe;
    }
  }

  public DRProcess dr
  {
    get
    {
      this._dr = this._dr ?? this.CreateDRProcess();
      return this._dr;
    }
  }

  protected virtual DRProcess CreateDRProcess() => PXGraph.CreateInstance<DRProcess>();

  protected virtual void ReplaceCADailySummaryCache(JournalEntry je)
  {
    if (!(((PXGraph) je).Caches[typeof (CADailySummary)].Current is CADailySummary current))
      return;
    ((PXSelectBase<CADailySummary>) this.caDailySummary).Insert(current);
    ((PXGraph) je).Views.Caches.Remove(typeof (CADailySummary));
  }

  [PXDBString(1, IsFixed = true)]
  public virtual void Tax_TaxType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(1, IsFixed = true)]
  public virtual void Tax_TaxCalcLevel_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  protected virtual void ARInvoice_CuryApplicationBalance_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  protected virtual void ARInvoice_ApplicationBalance_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  protected virtual void ARTranPost_AccountID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  protected virtual void ARTranPost_SubID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  protected virtual void ARTranPost_CustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  protected virtual void ARTranPost_BranchID_CacheAttached(PXCache sender)
  {
  }

  public ARReleaseProcess()
  {
    ((PXGraph) this).Defaults.Remove(typeof (ARSetup));
    OpenPeriodAttribute.SetValidatePeriod<ARRegister.finPeriodID>(((PXSelectBase) this.ARDocument).Cache, (object) null, PeriodValidation.Nothing);
    OpenPeriodAttribute.SetValidatePeriod<ARPayment.adjFinPeriodID>(((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache, (object) null, PeriodValidation.Nothing);
    PXCache cach1 = ((PXGraph) this).Caches[typeof (ARAdjust)];
    PXCacheEx.AttributeAdjuster<FinPeriodIDAttribute> attributeAdjuster = PXCacheEx.Adjust<FinPeriodIDAttribute>(cach1, (object) null);
    attributeAdjuster.For<ARAdjust.adjgFinPeriodID>((Action<FinPeriodIDAttribute>) (attr =>
    {
      attr.AutoCalculateMasterPeriod = false;
      attr.CalculatePeriodByHeader = false;
      attr.HeaderFindingMode = FinPeriodIDAttribute.HeaderFindingModes.Parent;
    })).SameFor<ARAdjust.adjdFinPeriodID>();
    PXDBDefaultAttribute.SetDefaultForUpdate<ARAdjust.customerID>(cach1, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ARAdjust.adjgDocType>(cach1, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ARAdjust.adjgRefNbr>(cach1, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ARAdjust.adjgCuryInfoID>(cach1, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ARAdjust.adjgDocDate>(cach1, (object) null, false);
    PXCache cach2 = ((PXGraph) this).Caches[typeof (ARTran)];
    attributeAdjuster = PXCacheEx.Adjust<FinPeriodIDAttribute>(cach2, (object) null);
    attributeAdjuster.For<ARTran.finPeriodID>((Action<FinPeriodIDAttribute>) (attr => attr.HeaderFindingMode = FinPeriodIDAttribute.HeaderFindingModes.Parent));
    PXDBDefaultAttribute.SetDefaultForInsert<ARTran.tranType>(cach2, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForInsert<ARTran.refNbr>(cach2, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ARTran.tranType>(cach2, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ARTran.refNbr>(cach2, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ARTran.curyInfoID>(cach2, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ARTran.tranDate>(cach2, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ARTran.customerID>(cach2, (object) null, false);
    PXCache cach3 = ((PXGraph) this).Caches[typeof (ARTaxTran)];
    PXDBDefaultAttribute.SetDefaultForUpdate<ARTaxTran.tranType>(cach3, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ARTaxTran.refNbr>(cach3, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ARTaxTran.curyInfoID>(cach3, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ARTaxTran.tranDate>(cach3, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ARTaxTran.taxZoneID>(cach3, (object) null, false);
    PXFormulaAttribute.SetAggregate<ARAdjust.curyAdjgAmt>(cach1, (System.Type) null, (System.Type) null);
    PXFormulaAttribute.SetAggregate<ARAdjust.curyAdjdAmt>(cach1, (System.Type) null, (System.Type) null);
    PXFormulaAttribute.SetAggregate<ARAdjust.adjAmt>(cach1, (System.Type) null, (System.Type) null);
    if (!this.IsMigrationMode.GetValueOrDefault())
      return;
    PXDBDefaultAttribute.SetDefaultForInsert<ARAdjust.adjgDocDate>(cach1, (object) null, false);
  }

  public virtual void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    map.Add(typeof (PX.Objects.AP.Vendor), typeof (PX.Objects.AP.Vendor));
  }

  protected virtual void ARPayment_CashAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARPayment_PMInstanceID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARPayment_PaymentMethodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARPayment_ExtRefNbr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARRegister_FinPeriodID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != 1)
      return;
    e.ExcludeFromInsertUpdate();
  }

  protected virtual void ARRegister_TranPeriodID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != 1)
      return;
    e.ExcludeFromInsertUpdate();
  }

  protected virtual void ARRegister_DocDate_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != 1)
      return;
    e.ExcludeFromInsertUpdate();
  }

  protected virtual void ARAdjust_AdjdRefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARTran_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
  }

  protected virtual void ARTran_TaxCategoryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARTran_SalesPersonID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected void _(PX.Data.Events.RowPersisting<ARTranPost> e)
  {
    if (e.Operation != 2 || !(e.Row.Type == "R") && !(e.Row.Type == "X"))
      return;
    Decimal? nullable = e.Row.CuryAmt;
    Decimal num1 = 0M;
    if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
      return;
    nullable = e.Row.Amt;
    Decimal num2 = 0M;
    if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
      return;
    nullable = e.Row.RGOLAmt;
    Decimal num3 = 0M;
    if (!(nullable.GetValueOrDefault() == num3 & nullable.HasValue))
      return;
    nullable = e.Row.WOAmt;
    Decimal num4 = 0M;
    if (!(nullable.GetValueOrDefault() == num4 & nullable.HasValue))
      return;
    nullable = e.Row.DiscAmt;
    Decimal num5 = 0M;
    if (!(nullable.GetValueOrDefault() == num5 & nullable.HasValue))
      return;
    e.Cancel = true;
  }

  private ARHist CreateHistory(
    int? BranchID,
    int? AccountID,
    int? SubID,
    int? CustomerID,
    string PeriodID)
  {
    ARHist arHist = new ARHist();
    arHist.BranchID = BranchID;
    arHist.AccountID = AccountID;
    arHist.SubID = SubID;
    arHist.CustomerID = CustomerID;
    arHist.FinPeriodID = PeriodID;
    return (ARHist) ((PXGraph) this).Caches[typeof (ARHist)].Insert((object) arHist);
  }

  private CuryARHist CreateHistory(
    int? BranchID,
    int? AccountID,
    int? SubID,
    int? CustomerID,
    string CuryID,
    string PeriodID)
  {
    CuryARHist curyArHist = new CuryARHist();
    curyArHist.BranchID = BranchID;
    curyArHist.AccountID = AccountID;
    curyArHist.SubID = SubID;
    curyArHist.CustomerID = CustomerID;
    curyArHist.CuryID = CuryID;
    curyArHist.FinPeriodID = PeriodID;
    return (CuryARHist) ((PXGraph) this).Caches[typeof (CuryARHist)].Insert((object) curyArHist);
  }

  private void UpdateHist<History>(
    History accthist,
    ARReleaseProcess.ARHistBucket bucket,
    bool FinFlag,
    PX.Objects.GL.GLTran tran)
    where History : class, IBaseARHist
  {
    if (this._IsIntegrityCheck)
    {
      bool? detDeleted = accthist.DetDeleted;
      bool flag = false;
      if (!(detDeleted.GetValueOrDefault() == flag & detDeleted.HasValue))
        return;
    }
    Decimal? nullable1 = tran.DebitAmt;
    Decimal? nullable2 = tran.CreditAmt;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    accthist.FinFlag = new bool?(FinFlag);
    ref History local1 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local2 = (object) local1;
    nullable2 = local1.PtdPayments;
    Decimal signPayments = bucket.SignPayments;
    Decimal? nullable4 = nullable3;
    nullable1 = nullable4.HasValue ? new Decimal?(signPayments * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local2.PtdPayments = nullable5;
    ref History local3 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local4 = (object) local3;
    nullable1 = local3.PtdSales;
    Decimal signSales = bucket.SignSales;
    Decimal? nullable6 = nullable3;
    nullable2 = nullable6.HasValue ? new Decimal?(signSales * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable7 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    local4.PtdSales = nullable7;
    ref History local5 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local6 = (object) local5;
    nullable2 = local5.PtdDrAdjustments;
    Decimal signDrMemos = bucket.SignDrMemos;
    Decimal? nullable8 = nullable3;
    nullable1 = nullable8.HasValue ? new Decimal?(signDrMemos * nullable8.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable9 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local6.PtdDrAdjustments = nullable9;
    ref History local7 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local8 = (object) local7;
    nullable1 = local7.PtdCrAdjustments;
    Decimal signCrMemos = bucket.SignCrMemos;
    Decimal? nullable10 = nullable3;
    nullable2 = nullable10.HasValue ? new Decimal?(signCrMemos * nullable10.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable11 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    local8.PtdCrAdjustments = nullable11;
    ref History local9 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local10 = (object) local9;
    nullable2 = local9.PtdFinCharges;
    Decimal signFinCharges = bucket.SignFinCharges;
    Decimal? nullable12 = nullable3;
    nullable1 = nullable12.HasValue ? new Decimal?(signFinCharges * nullable12.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable13 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local10.PtdFinCharges = nullable13;
    ref History local11 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local12 = (object) local11;
    nullable1 = local11.PtdDiscounts;
    Decimal signDiscTaken = bucket.SignDiscTaken;
    Decimal? nullable14 = nullable3;
    nullable2 = nullable14.HasValue ? new Decimal?(signDiscTaken * nullable14.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable15 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    local12.PtdDiscounts = nullable15;
    ref History local13 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local14 = (object) local13;
    nullable2 = local13.PtdRGOL;
    Decimal signRgol = bucket.SignRGOL;
    Decimal? nullable16 = nullable3;
    nullable1 = nullable16.HasValue ? new Decimal?(signRgol * nullable16.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable17 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local14.PtdRGOL = nullable17;
    ref History local15 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local16 = (object) local15;
    nullable1 = local15.YtdBalance;
    Decimal signPtd = bucket.SignPtd;
    Decimal? nullable18 = nullable3;
    nullable2 = nullable18.HasValue ? new Decimal?(signPtd * nullable18.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable19 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    local16.YtdBalance = nullable19;
    ref History local17 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local18 = (object) local17;
    nullable2 = local17.PtdDeposits;
    Decimal signDeposits1 = bucket.SignDeposits;
    Decimal? nullable20 = nullable3;
    nullable1 = nullable20.HasValue ? new Decimal?(signDeposits1 * nullable20.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable21 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local18.PtdDeposits = nullable21;
    ref History local19 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local20 = (object) local19;
    nullable1 = local19.YtdDeposits;
    Decimal signDeposits2 = bucket.SignDeposits;
    Decimal? nullable22 = nullable3;
    nullable2 = nullable22.HasValue ? new Decimal?(signDeposits2 * nullable22.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable23 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    local20.YtdDeposits = nullable23;
    ref History local21 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local22 = (object) local21;
    nullable2 = local21.PtdItemDiscounts;
    Decimal ptdItemDiscounts = bucket.SignPtdItemDiscounts;
    Decimal? nullable24 = nullable3;
    nullable1 = nullable24.HasValue ? new Decimal?(ptdItemDiscounts * nullable24.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable25 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local22.PtdItemDiscounts = nullable25;
    ref History local23 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local24 = (object) local23;
    nullable1 = local23.YtdRetainageReleased;
    Decimal retainageReleased1 = bucket.SignRetainageReleased;
    Decimal? nullable26 = nullable3;
    nullable2 = nullable26.HasValue ? new Decimal?(retainageReleased1 * nullable26.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable27 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    local24.YtdRetainageReleased = nullable27;
    ref History local25 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local26 = (object) local25;
    nullable2 = local25.PtdRetainageReleased;
    Decimal retainageReleased2 = bucket.SignRetainageReleased;
    Decimal? nullable28 = nullable3;
    nullable1 = nullable28.HasValue ? new Decimal?(retainageReleased2 * nullable28.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable29 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local26.PtdRetainageReleased = nullable29;
    ref History local27 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local28 = (object) local27;
    nullable1 = local27.YtdRetainageWithheld;
    Decimal retainageWithheld1 = bucket.SignRetainageWithheld;
    Decimal? nullable30 = nullable3;
    nullable2 = nullable30.HasValue ? new Decimal?(retainageWithheld1 * nullable30.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable31 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    local28.YtdRetainageWithheld = nullable31;
    ref History local29 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local30 = (object) local29;
    nullable2 = local29.PtdRetainageWithheld;
    Decimal retainageWithheld2 = bucket.SignRetainageWithheld;
    Decimal? nullable32 = nullable3;
    nullable1 = nullable32.HasValue ? new Decimal?(retainageWithheld2 * nullable32.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable33 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local30.PtdRetainageWithheld = nullable33;
  }

  private void UpdateFinHist<History>(
    History accthist,
    ARReleaseProcess.ARHistBucket bucket,
    PX.Objects.GL.GLTran tran)
    where History : class, IBaseARHist
  {
    this.UpdateHist<History>(accthist, bucket, true, tran);
  }

  private void UpdateTranHist<History>(
    History accthist,
    ARReleaseProcess.ARHistBucket bucket,
    PX.Objects.GL.GLTran tran)
    where History : class, IBaseARHist
  {
    this.UpdateHist<History>(accthist, bucket, false, tran);
  }

  private void CuryUpdateHist<History>(
    History accthist,
    ARReleaseProcess.ARHistBucket bucket,
    bool FinFlag,
    PX.Objects.GL.GLTran tran)
    where History : class, ICuryARHist, IBaseARHist
  {
    if (this._IsIntegrityCheck)
    {
      bool? detDeleted = accthist.DetDeleted;
      bool flag = false;
      if (!(detDeleted.GetValueOrDefault() == flag & detDeleted.HasValue))
        return;
    }
    this.UpdateHist<History>(accthist, bucket, FinFlag, tran);
    Decimal? curyDebitAmt = tran.CuryDebitAmt;
    Decimal? nullable1 = tran.CuryCreditAmt;
    Decimal? nullable2 = curyDebitAmt.HasValue & nullable1.HasValue ? new Decimal?(curyDebitAmt.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    accthist.FinFlag = new bool?(FinFlag);
    ref History local1 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local2 = (object) local1;
    nullable1 = local1.CuryPtdPayments;
    Decimal signPayments = bucket.SignPayments;
    Decimal? nullable3 = nullable2;
    Decimal? nullable4 = nullable3.HasValue ? new Decimal?(signPayments * nullable3.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    local2.CuryPtdPayments = nullable5;
    ref History local3 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local4 = (object) local3;
    nullable4 = local3.CuryPtdSales;
    Decimal signSales = bucket.SignSales;
    Decimal? nullable6 = nullable2;
    nullable1 = nullable6.HasValue ? new Decimal?(signSales * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable7 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local4.CuryPtdSales = nullable7;
    ref History local5 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local6 = (object) local5;
    nullable1 = local5.CuryPtdDrAdjustments;
    Decimal signDrMemos = bucket.SignDrMemos;
    Decimal? nullable8 = nullable2;
    nullable4 = nullable8.HasValue ? new Decimal?(signDrMemos * nullable8.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable9 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    local6.CuryPtdDrAdjustments = nullable9;
    ref History local7 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local8 = (object) local7;
    nullable4 = local7.CuryPtdCrAdjustments;
    Decimal signCrMemos = bucket.SignCrMemos;
    Decimal? nullable10 = nullable2;
    nullable1 = nullable10.HasValue ? new Decimal?(signCrMemos * nullable10.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable11 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local8.CuryPtdCrAdjustments = nullable11;
    ref History local9 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local10 = (object) local9;
    nullable1 = local9.CuryPtdFinCharges;
    Decimal signFinCharges = bucket.SignFinCharges;
    Decimal? nullable12 = nullable2;
    nullable4 = nullable12.HasValue ? new Decimal?(signFinCharges * nullable12.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable13 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    local10.CuryPtdFinCharges = nullable13;
    ref History local11 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local12 = (object) local11;
    nullable4 = local11.CuryPtdDiscounts;
    Decimal signDiscTaken = bucket.SignDiscTaken;
    Decimal? nullable14 = nullable2;
    nullable1 = nullable14.HasValue ? new Decimal?(signDiscTaken * nullable14.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable15 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local12.CuryPtdDiscounts = nullable15;
    ref History local13 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local14 = (object) local13;
    nullable1 = local13.CuryYtdBalance;
    Decimal signPtd = bucket.SignPtd;
    Decimal? nullable16 = nullable2;
    nullable4 = nullable16.HasValue ? new Decimal?(signPtd * nullable16.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable17 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    local14.CuryYtdBalance = nullable17;
    ref History local15 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local16 = (object) local15;
    nullable4 = local15.CuryPtdDeposits;
    Decimal signDeposits1 = bucket.SignDeposits;
    Decimal? nullable18 = nullable2;
    nullable1 = nullable18.HasValue ? new Decimal?(signDeposits1 * nullable18.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable19 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local16.CuryPtdDeposits = nullable19;
    ref History local17 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local18 = (object) local17;
    nullable1 = local17.CuryYtdDeposits;
    Decimal signDeposits2 = bucket.SignDeposits;
    Decimal? nullable20 = nullable2;
    nullable4 = nullable20.HasValue ? new Decimal?(signDeposits2 * nullable20.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable21 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    local18.CuryYtdDeposits = nullable21;
    ref History local19 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local20 = (object) local19;
    nullable4 = local19.CuryYtdRetainageReleased;
    Decimal retainageReleased1 = bucket.SignRetainageReleased;
    Decimal? nullable22 = nullable2;
    nullable1 = nullable22.HasValue ? new Decimal?(retainageReleased1 * nullable22.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable23 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local20.CuryYtdRetainageReleased = nullable23;
    ref History local21 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local22 = (object) local21;
    nullable1 = local21.CuryPtdRetainageReleased;
    Decimal retainageReleased2 = bucket.SignRetainageReleased;
    Decimal? nullable24 = nullable2;
    nullable4 = nullable24.HasValue ? new Decimal?(retainageReleased2 * nullable24.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable25 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    local22.CuryPtdRetainageReleased = nullable25;
    ref History local23 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local24 = (object) local23;
    nullable4 = local23.CuryYtdRetainageWithheld;
    Decimal retainageWithheld1 = bucket.SignRetainageWithheld;
    Decimal? nullable26 = nullable2;
    nullable1 = nullable26.HasValue ? new Decimal?(retainageWithheld1 * nullable26.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable27 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local24.CuryYtdRetainageWithheld = nullable27;
    ref History local25 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local26 = (object) local25;
    nullable1 = local25.CuryPtdRetainageWithheld;
    Decimal retainageWithheld2 = bucket.SignRetainageWithheld;
    Decimal? nullable28 = nullable2;
    nullable4 = nullable28.HasValue ? new Decimal?(retainageWithheld2 * nullable28.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable29 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    local26.CuryPtdRetainageWithheld = nullable29;
  }

  private void CuryUpdateFinHist<History>(
    History accthist,
    ARReleaseProcess.ARHistBucket bucket,
    PX.Objects.GL.GLTran tran)
    where History : class, ICuryARHist, IBaseARHist
  {
    this.CuryUpdateHist<History>(accthist, bucket, true, tran);
  }

  private void CuryUpdateTranHist<History>(
    History accthist,
    ARReleaseProcess.ARHistBucket bucket,
    PX.Objects.GL.GLTran tran)
    where History : class, ICuryARHist, IBaseARHist
  {
    this.CuryUpdateHist<History>(accthist, bucket, false, tran);
  }

  private bool IsNeedUpdateHistoryForTransaction(string TranPeriodID)
  {
    return !this._IsIntegrityCheck || string.Compare(TranPeriodID, this._IntegrityCheckStartingPeriod) >= 0;
  }

  protected void UpdateItemDiscountsHistory(ARTran tran, ARRegister ardoc)
  {
    if (!this.IsNeedUpdateHistoryForTransaction(tran.TranPeriodID))
      return;
    ARReleaseProcess.ARHistBucket bucket = (ARReleaseProcess.ARHistBucket) new ARReleaseProcess.ARHistItemDiscountsBucket(tran);
    ARHist history1 = this.CreateHistory(ardoc.BranchID, ardoc.ARAccountID, ardoc.ARSubID, ardoc.CustomerID, ardoc.FinPeriodID);
    if (history1 != null)
      this.UpdateFinHist<ARHist>(history1, bucket, new PX.Objects.GL.GLTran()
      {
        DebitAmt = tran.DiscAmt,
        CreditAmt = new Decimal?(0M)
      });
    ARHist history2 = this.CreateHistory(ardoc.BranchID, ardoc.ARAccountID, ardoc.ARSubID, ardoc.CustomerID, ardoc.TranPeriodID);
    if (history2 == null)
      return;
    this.UpdateTranHist<ARHist>(history2, bucket, new PX.Objects.GL.GLTran()
    {
      DebitAmt = tran.DiscAmt,
      CreditAmt = new Decimal?(0M)
    });
  }

  private void UpdateHistory(PX.Objects.GL.GLTran tran, Customer cust)
  {
    ARReleaseProcess.ARHistBucket bucket = new ARReleaseProcess.ARHistBucket(tran, this.GetHistTranType(tran.TranType, tran.RefNbr));
    this.UpdateHistory(tran, cust, bucket);
  }

  private void UpdateHistory(PX.Objects.GL.GLTran tran, Customer cust, ARReleaseProcess.ARHistBucket bucket)
  {
    if (!this.IsNeedUpdateHistoryForTransaction(tran.TranPeriodID))
      return;
    ARHist history1 = this.CreateHistory(tran.BranchID, bucket.arAccountID, bucket.arSubID, cust.BAccountID, tran.FinPeriodID);
    if (history1 != null)
      this.UpdateFinHist<ARHist>(history1, bucket, tran);
    ARHist history2 = this.CreateHistory(tran.BranchID, bucket.arAccountID, bucket.arSubID, cust.BAccountID, tran.TranPeriodID);
    if (history2 == null)
      return;
    this.UpdateTranHist<ARHist>(history2, bucket, tran);
  }

  private void UpdateHistory(PX.Objects.GL.GLTran tran, Customer cust, PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    ARReleaseProcess.ARHistBucket bucket = new ARReleaseProcess.ARHistBucket(tran, this.GetHistTranType(tran.TranType, tran.RefNbr));
    this.UpdateHistory(tran, cust, info, bucket);
  }

  private void UpdateHistory(
    PX.Objects.GL.GLTran tran,
    Customer cust,
    PX.Objects.CM.Extensions.CurrencyInfo info,
    ARReleaseProcess.ARHistBucket bucket)
  {
    if (!this.IsNeedUpdateHistoryForTransaction(tran.TranPeriodID))
      return;
    CuryARHist history1 = this.CreateHistory(tran.BranchID, bucket.arAccountID, bucket.arSubID, cust.BAccountID, info.CuryID, tran.FinPeriodID);
    if (history1 != null)
      this.CuryUpdateFinHist<CuryARHist>(history1, bucket, tran);
    CuryARHist history2 = this.CreateHistory(tran.BranchID, bucket.arAccountID, bucket.arSubID, cust.BAccountID, info.CuryID, tran.TranPeriodID);
    if (history2 == null)
      return;
    this.CuryUpdateTranHist<CuryARHist>(history2, bucket, tran);
  }

  private string GetHistTranType(string tranType, string refNbr)
  {
    string histTranType = tranType;
    if (tranType == "RPM")
    {
      ARRegister arRegister = PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>, And<Where<ARRegister.docType, Equal<ARDocType.payment>, Or<ARRegister.docType, Equal<ARDocType.prepayment>>>>>, OrderBy<Asc<Switch<Case<Where<ARRegister.docType, Equal<ARDocType.payment>>, int0>, int1>, Asc<ARRegister.docType, Asc<ARRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) refNbr
      }));
      if (arRegister != null)
        histTranType = arRegister.DocType;
    }
    if (tranType == "VRF")
      histTranType = "REF";
    return histTranType;
  }

  public virtual List<ARRegister> CreateInstallments(
    PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer> res)
  {
    ARInvoice arInvoice1 = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>.op_Implicit(res);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>.op_Implicit(res);
    PX.Objects.CS.Terms terms = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>.op_Implicit(res);
    Customer customer = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>.op_Implicit(res);
    List<ARRegister> installments = new List<ARRegister>();
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
    PXResultset<TermsInstallments> pxResultset = TermsAttribute.SelectInstallments((PXGraph) this, terms, arInvoice1.DueDate.Value);
    foreach (PXResult<TermsInstallments> pxResult1 in pxResultset)
    {
      TermsInstallments termsInstallments = PXResult<TermsInstallments>.op_Implicit(pxResult1);
      ((PXSelectBase<Customer>) instance.customer).Current = customer;
      ((PXSelectBase) this.ARInvoice_DocType_RefNbr).Cache.GetValueExt((object) arInvoice1, "CuryOrigDocAmt");
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = ((PXGraph) instance).GetExtension<ARInvoiceEntry.MultiCurrency>().CloneCurrencyInfo(currencyInfo1);
      ARInvoice copy1 = PXCache<ARInvoice>.CreateCopy(arInvoice1);
      copy1.CuryInfoID = currencyInfo2.CuryInfoID;
      copy1.DueDate = new DateTime?(copy1.DueDate.Value.AddDays((double) termsInstallments.InstDays.Value));
      copy1.DiscDate = copy1.DueDate;
      copy1.InstallmentNbr = termsInstallments.InstallmentNbr;
      copy1.MasterRefNbr = copy1.RefNbr;
      copy1.RefNbr = (string) null;
      copy1.NoteID = new Guid?();
      copy1.ProjectID = ProjectDefaultAttribute.NonProject();
      copy1.CuryDetailExtPriceTotal = new Decimal?(0M);
      copy1.DetailExtPriceTotal = new Decimal?(0M);
      copy1.CuryLineDiscTotal = new Decimal?(0M);
      copy1.LineDiscTotal = new Decimal?(0M);
      copy1.CuryMiscExtPriceTotal = new Decimal?(0M);
      copy1.MiscExtPriceTotal = new Decimal?(0M);
      copy1.CuryGoodsExtPriceTotal = new Decimal?(0M);
      copy1.GoodsExtPriceTotal = new Decimal?(0M);
      copy1.GoodsTotal = new Decimal?(0M);
      copy1.CuryGoodsTotal = new Decimal?(0M);
      copy1.PremiumFreightAmt = new Decimal?(0M);
      copy1.CuryPremiumFreightAmt = new Decimal?(0M);
      copy1.FreightAmt = new Decimal?(0M);
      copy1.CuryFreightAmt = new Decimal?(0M);
      copy1.FreightTot = new Decimal?(0M);
      copy1.CuryFreightTot = new Decimal?(0M);
      copy1.MiscTot = new Decimal?(0M);
      copy1.CuryMiscTot = new Decimal?(0M);
      copy1.DocumentDiscTotal = new Decimal?(0M);
      copy1.CuryDocumentDiscTotal = new Decimal?(0M);
      copy1.FreightCost = new Decimal?(0M);
      copy1.CuryFreightCost = new Decimal?(0M);
      copy1.GroupDiscTotal = new Decimal?(0M);
      copy1.CuryGroupDiscTotal = new Decimal?(0M);
      copy1.CuryCommnblAmt = new Decimal?(0M);
      copy1.CuryCommnAmt = new Decimal?(0M);
      TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) instance.Transactions).Cache, (object) null, TaxCalc.NoCalc);
      Decimal? nullable1 = new Decimal?();
      short? installmentNbr1 = termsInstallments.InstallmentNbr;
      int? nullable2 = installmentNbr1.HasValue ? new int?((int) installmentNbr1.GetValueOrDefault()) : new int?();
      int count = pxResultset.Count;
      Decimal? nullable3;
      if (nullable2.GetValueOrDefault() == count & nullable2.HasValue)
      {
        ARInvoice arInvoice2 = copy1;
        Decimal? curyOrigDocAmt = copy1.CuryOrigDocAmt;
        Decimal num3 = num1;
        Decimal? nullable4 = curyOrigDocAmt.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() - num3) : new Decimal?();
        arInvoice2.CuryOrigDocAmt = nullable4;
        Decimal? origDocAmt = copy1.OrigDocAmt;
        Decimal num4 = num2;
        nullable1 = origDocAmt.HasValue ? new Decimal?(origDocAmt.GetValueOrDefault() - num4) : new Decimal?();
      }
      else if (terms.InstallmentMthd == "A")
      {
        ARInvoice arInvoice3 = copy1;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo2;
        Decimal? curyOrigDocAmt1 = arInvoice1.CuryOrigDocAmt;
        Decimal? nullable5 = arInvoice1.CuryTaxTotal;
        Decimal? nullable6 = curyOrigDocAmt1.HasValue & nullable5.HasValue ? new Decimal?(curyOrigDocAmt1.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable7 = termsInstallments.InstPercent;
        Decimal? nullable8;
        if (!(nullable6.HasValue & nullable7.HasValue))
        {
          nullable5 = new Decimal?();
          nullable8 = nullable5;
        }
        else
          nullable8 = new Decimal?(nullable6.GetValueOrDefault() * nullable7.GetValueOrDefault() / 100M);
        nullable5 = nullable8;
        Decimal val = nullable5.Value;
        Decimal? nullable9 = new Decimal?(currencyInfo3.RoundCury(val));
        arInvoice3.CuryOrigDocAmt = nullable9;
        short? installmentNbr2 = termsInstallments.InstallmentNbr;
        int? nullable10;
        if (!installmentNbr2.HasValue)
        {
          nullable2 = new int?();
          nullable10 = nullable2;
        }
        else
          nullable10 = new int?((int) installmentNbr2.GetValueOrDefault());
        nullable2 = nullable10;
        if (nullable2.GetValueOrDefault() == 1)
        {
          ARInvoice arInvoice4 = copy1;
          Decimal? curyOrigDocAmt2 = arInvoice4.CuryOrigDocAmt;
          nullable7 = arInvoice1.CuryTaxTotal;
          Decimal num5 = nullable7.Value;
          Decimal? nullable11;
          if (!curyOrigDocAmt2.HasValue)
          {
            nullable7 = new Decimal?();
            nullable11 = nullable7;
          }
          else
            nullable11 = new Decimal?(curyOrigDocAmt2.GetValueOrDefault() + num5);
          arInvoice4.CuryOrigDocAmt = nullable11;
        }
      }
      else
      {
        ARInvoice arInvoice5 = copy1;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo4 = currencyInfo2;
        nullable3 = arInvoice1.CuryOrigDocAmt;
        Decimal? instPercent = termsInstallments.InstPercent;
        Decimal val = (nullable3.HasValue & instPercent.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * instPercent.GetValueOrDefault() / 100M) : new Decimal?()).Value;
        Decimal? nullable12 = new Decimal?(currencyInfo4.RoundCury(val));
        arInvoice5.CuryOrigDocAmt = nullable12;
      }
      copy1.CuryDocBal = copy1.CuryOrigDocAmt;
      copy1.CuryLineTotal = copy1.CuryOrigDocAmt;
      copy1.CuryTaxTotal = new Decimal?(0M);
      copy1.CuryOrigDiscAmt = new Decimal?(0M);
      copy1.CuryVatTaxableTotal = new Decimal?(0M);
      copy1.CuryDiscTot = new Decimal?(0M);
      copy1.OrigModule = "AR";
      copy1.Hold = new bool?(true);
      ARInvoice arInvoice6 = ((PXSelectBase<ARInvoice>) instance.Document).Insert(copy1);
      ((PXGraph) instance).GetExtension<ARInvoiceEntry.ARInvoiceEntryDocumentExtension>().SuppressApproval();
      arInvoice6.Hold = new bool?(false);
      if (nullable1.HasValue)
      {
        arInvoice6.OrigDocAmt = nullable1;
        arInvoice6.DocBal = nullable1;
        arInvoice6.LineTotal = nullable1;
      }
      ((PXSelectBase<ARInvoice>) instance.Document).Update(arInvoice6);
      Decimal num6 = num1;
      nullable3 = arInvoice6.CuryOrigDocAmt;
      Decimal num7 = nullable3.Value;
      num1 = num6 + num7;
      Decimal num8 = num2;
      nullable3 = arInvoice6.OrigDocAmt;
      Decimal num9 = nullable3.Value;
      num2 = num8 + num9;
      TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) instance.Transactions).Cache, (object) null, TaxCalc.NoCalc);
      ARTran arTran = ((PXSelectBase<ARTran>) instance.Transactions).Insert(new ARTran()
      {
        AccountID = arInvoice6.ARAccountID,
        SubID = arInvoice6.ARSubID,
        CuryTranAmt = arInvoice6.CuryOrigDocAmt,
        TranDesc = this.LocalizeForCustomer(customer, "Multiple Installment Split")
      });
      foreach (PXResult<ARSalesPerTran> pxResult2 in ((PXSelectBase<ARSalesPerTran>) instance.salesPerTrans).Select(Array.Empty<object>()))
      {
        ARSalesPerTran arSalesPerTran = PXResult<ARSalesPerTran>.op_Implicit(pxResult2);
        ((PXSelectBase<ARSalesPerTran>) instance.salesPerTrans).Delete(arSalesPerTran);
      }
      foreach (PXResult<ARSalesPerTran> pxResult3 in PXSelectBase<ARSalesPerTran, PXSelect<ARSalesPerTran, Where<ARSalesPerTran.docType, Equal<Required<ARSalesPerTran.docType>>, And<ARSalesPerTran.refNbr, Equal<Required<ARSalesPerTran.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) arInvoice1.DocType,
        (object) arInvoice1.RefNbr
      }))
      {
        ARSalesPerTran arSalesPerTran1 = PXResult<ARSalesPerTran>.op_Implicit(pxResult3);
        ARSalesPerTran copy2 = PXCache<ARSalesPerTran>.CreateCopy(arSalesPerTran1);
        copy2.RefNbr = (string) null;
        copy2.CuryInfoID = currencyInfo2.CuryInfoID;
        copy2.RefCntr = new int?(999);
        ARSalesPerTran arSalesPerTran2 = copy2;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo5 = currencyInfo2;
        Decimal? curyCommnblAmt = arSalesPerTran1.CuryCommnblAmt;
        Decimal? instPercent = termsInstallments.InstPercent;
        Decimal val1 = (curyCommnblAmt.HasValue & instPercent.HasValue ? new Decimal?(curyCommnblAmt.GetValueOrDefault() * instPercent.GetValueOrDefault() / 100M) : new Decimal?()).Value;
        Decimal? nullable13 = new Decimal?(currencyInfo5.RoundCury(val1));
        arSalesPerTran2.CuryCommnblAmt = nullable13;
        ARSalesPerTran arSalesPerTran3 = copy2;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo6 = currencyInfo2;
        Decimal? curyCommnAmt = arSalesPerTran1.CuryCommnAmt;
        instPercent = termsInstallments.InstPercent;
        Decimal val2 = (curyCommnAmt.HasValue & instPercent.HasValue ? new Decimal?(curyCommnAmt.GetValueOrDefault() * instPercent.GetValueOrDefault() / 100M) : new Decimal?()).Value;
        Decimal? nullable14 = new Decimal?(currencyInfo6.RoundCury(val2));
        arSalesPerTran3.CuryCommnAmt = nullable14;
        ((PXSelectBase<ARSalesPerTran>) instance.salesPerTrans).Insert(copy2);
      }
      if (nullable1.HasValue)
      {
        arTran.TranAmt = nullable1;
        ((PXSelectBase<ARTran>) instance.Transactions).Update(arTran);
        if (currencyInfo2.BaseCuryID != currencyInfo2.CuryID)
          currencyInfo2.BaseCalc = new bool?(false);
      }
      ((PXAction) instance.Save).Press();
      installments.Add((ARRegister) ((PXSelectBase<ARInvoice>) instance.Document).Current);
      ((PXGraph) instance).Clear();
    }
    if (pxResultset.Count > 0)
      PXDatabase.Update<ARInvoice>(new PXDataFieldParam[3]
      {
        (PXDataFieldParam) new PXDataFieldAssign<ARInvoice.installmentCntr>((PXDbType) 16 /*0x10*/, (object) Convert.ToInt16(pxResultset.Count)),
        (PXDataFieldParam) new PXDataFieldRestrict<ARInvoice.docType>((PXDbType) 22, (object) arInvoice1.DocType),
        (PXDataFieldParam) new PXDataFieldRestrict<ARInvoice.refNbr>((PXDbType) 12, (object) arInvoice1.RefNbr)
      });
    return installments;
  }

  protected virtual bool HasAnyUnreleasedAdjustment(ARRegister doc)
  {
    return ((IQueryable<PXResult<ARAdjust>>) PXSelectBase<ARAdjust, PXViewOf<ARAdjust>.BasedOn<SelectFromBase<ARAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<ARAdjust.adjgRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<ARAdjust.released, IBqlBool>.IsNotEqual<True>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    })).Any<PXResult<ARAdjust>>();
  }

  protected virtual void SetClosedPeriodsFromLatestApplication(ARRegister doc)
  {
    ARTranPost arTranPost1 = PXResultset<ARTranPost>.op_Implicit(PXSelectBase<ARTranPost, PXSelect<ARTranPost, Where<ARTranPost.docType, Equal<Required<ARTranPost.docType>>, And<ARTranPost.refNbr, Equal<Required<ARTranPost.refNbr>>>>, OrderBy<Desc<ARTranPost.tranPeriodID, Desc<ARTranPost.iD>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }));
    ARTranPost arTranPost2 = PXResultset<ARTranPost>.op_Implicit(PXSelectBase<ARTranPost, PXSelect<ARTranPost, Where<ARTranPost.docType, Equal<Required<ARTranPost.docType>>, And<ARTranPost.refNbr, Equal<Required<ARTranPost.refNbr>>>>, OrderBy<Desc<ARTranPost.docDate, Desc<ARTranPost.iD>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }));
    doc.ClosedTranPeriodID = PX.Objects.GL.FinPeriods.FinPeriodUtils.Max(arTranPost1?.TranPeriodID, doc.TranPeriodID);
    FinPeriodIDAttribute.SetPeriodsByMaster<ARRegister.closedFinPeriodID>(((PXSelectBase) this.ARDocument).Cache, (object) doc, doc.ClosedTranPeriodID);
    doc.ClosedDate = PX.Objects.GL.FinPeriods.FinPeriodUtils.Max((DateTime?) arTranPost2?.DocDate, doc.DocDate);
  }

  private void SetAdjgPeriodsFromLatestApplication(ARRegister doc, ARAdjust adj)
  {
    if (adj.VoidAppl.GetValueOrDefault())
    {
      foreach (string originalDocumentType in doc.PossibleOriginalDocumentTypes())
      {
        ARAdjust arAdjust1 = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<ARAdjust.adjNbr, Equal<Required<ARAdjust.voidAdjNbr>>, And<ARAdjust.released, Equal<True>>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[5]
        {
          (object) adj.AdjdDocType,
          (object) adj.AdjdRefNbr,
          (object) originalDocumentType,
          (object) adj.AdjgRefNbr,
          (object) adj.VoidAdjNbr
        }));
        if (arAdjust1 != null)
        {
          FinPeriodIDAttribute.SetPeriodsByMaster<ARAdjust.adjgFinPeriodID>(((PXSelectBase) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Cache, (object) adj, PX.Objects.GL.FinPeriods.FinPeriodUtils.Max(arAdjust1.AdjgTranPeriodID, adj.AdjgTranPeriodID));
          ARAdjust arAdjust2 = adj;
          DateTime? adjgDocDate = arAdjust1.AdjgDocDate;
          DateTime date1 = adjgDocDate.Value;
          adjgDocDate = adj.AdjgDocDate;
          DateTime date2 = adjgDocDate.Value;
          DateTime? nullable = new DateTime?(PX.Objects.GL.FinPeriods.FinPeriodUtils.Max(date1, date2));
          arAdjust2.AdjgDocDate = nullable;
          break;
        }
      }
    }
    string str = PX.Objects.GL.FinPeriods.FinPeriodUtils.Max(adj.AdjdTranPeriodID, adj.AdjgTranPeriodID);
    if (adj.AdjdDocType == "PPI" && adj.AdjgDocType == "REF")
    {
      foreach (PXResult<ARAdjust> pxResult in PXSelectBase<ARAdjust, PXViewOf<ARAdjust>.BasedOn<SelectFromBase<ARAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjdDocType, Equal<ARDocType.prepaymentInvoice>>>>, And<BqlOperand<ARAdjust.adjdRefNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsNotEqual<ARDocType.creditMemo>>>, And<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsNotEqual<ARDocType.refund>>>, And<BqlOperand<ARAdjust.released, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) adj.AdjdRefNbr
      }))
      {
        ARAdjust arAdjust3 = PXResult<ARAdjust>.op_Implicit(pxResult);
        str = PX.Objects.GL.FinPeriods.FinPeriodUtils.Max(adj.AdjdTranPeriodID, str);
        ARAdjust arAdjust4 = adj;
        DateTime? adjgDocDate = arAdjust3.AdjgDocDate;
        DateTime date1 = adjgDocDate.Value;
        adjgDocDate = adj.AdjgDocDate;
        DateTime date2 = adjgDocDate.Value;
        DateTime? nullable = new DateTime?(PX.Objects.GL.FinPeriods.FinPeriodUtils.Max(date1, date2));
        arAdjust4.AdjgDocDate = nullable;
      }
    }
    FinPeriodIDAttribute.SetPeriodsByMaster<ARAdjust.adjgFinPeriodID>(((PXSelectBase) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Cache, (object) adj, str);
    ARAdjust arAdjust = adj;
    DateTime? nullable1 = adj.AdjdDocDate;
    DateTime date1_1 = nullable1.Value;
    nullable1 = adj.AdjgDocDate;
    DateTime date2_1 = nullable1.Value;
    DateTime? nullable2 = new DateTime?(PX.Objects.GL.FinPeriods.FinPeriodUtils.Max(date1_1, date2_1));
    arAdjust.AdjgDocDate = nullable2;
  }

  public static void AdjustTaxCalculationLevelForNetGrossEntryMode(
    ARRegister document,
    ARTran documentLine,
    ref PX.Objects.TX.Tax taxCorrespondingToLine)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>())
      return;
    string taxCalcMode = document.TaxCalcMode;
    switch (taxCalcMode)
    {
      case "G":
        taxCorrespondingToLine.TaxCalcLevel = "0";
        break;
      case "N":
        taxCorrespondingToLine.TaxCalcLevel = "1";
        break;
      default:
        int num = taxCalcMode == "T" ? 1 : 0;
        break;
    }
  }

  private void UpdateARBalancesDates(ARRegister ardoc)
  {
    ARBalances arBalances = (ARBalances) ((PXGraph) this).Caches[typeof (ARBalances)].Insert((object) new ARBalances()
    {
      BranchID = ardoc.BranchID,
      CustomerID = ardoc.CustomerID,
      CustomerLocationID = ardoc.CustomerLocationID
    });
    this._oldInvoiceRefresher.RecordDocument(ardoc.BranchID, ardoc.CustomerID, ardoc.CustomerLocationID);
  }

  public static Decimal? RoundAmount(Decimal? amount, string RoundType, Decimal? precision)
  {
    Decimal? nullable1 = amount;
    Decimal? nullable2 = precision;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?();
    switch (RoundType)
    {
      case "F":
        Decimal num1 = Math.Floor(nullable3.Value);
        Decimal? nullable4 = precision;
        return !nullable4.HasValue ? new Decimal?() : new Decimal?(num1 * nullable4.GetValueOrDefault());
      case "C":
        Decimal num2 = Math.Ceiling(nullable3.Value);
        Decimal? nullable5 = precision;
        return !nullable5.HasValue ? new Decimal?() : new Decimal?(num2 * nullable5.GetValueOrDefault());
      case "R":
        Decimal num3 = Math.Round(nullable3.Value, 0, MidpointRounding.AwayFromZero);
        Decimal? nullable6 = precision;
        return !nullable6.HasValue ? new Decimal?() : new Decimal?(num3 * nullable6.GetValueOrDefault());
      default:
        return amount;
    }
  }

  protected virtual Decimal? RoundAmount(Decimal? amount)
  {
    return ARReleaseProcess.RoundAmount(amount, this.InvoiceRounding, this.InvoicePrecision);
  }

  /// <summary>
  /// The method to create a self document application (the same adjusted and adjusting documents)
  /// with amount equal to <see cref="P:PX.Objects.AR.ARRegister.CuryOrigDocAmt" /> value.
  /// </summary>
  public virtual ARAdjust CreateSelfApplicationForDocument(ARRegister doc)
  {
    return (ARAdjust) ((PXSelectBase) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Cache.Insert((object) new ARAdjust()
    {
      AdjgDocType = doc.DocType,
      AdjgRefNbr = doc.RefNbr,
      AdjdDocType = doc.DocType,
      AdjdRefNbr = doc.RefNbr,
      AdjNbr = doc.AdjCntr,
      AdjgBranchID = doc.BranchID,
      AdjdBranchID = doc.BranchID,
      CustomerID = doc.CustomerID,
      AdjdCustomerID = doc.CustomerID,
      AdjdARAcct = doc.ARAccountID,
      AdjdARSub = doc.ARSubID,
      AdjgCuryInfoID = doc.CuryInfoID,
      AdjdCuryInfoID = doc.CuryInfoID,
      AdjdOrigCuryInfoID = doc.CuryInfoID,
      AdjgDocDate = doc.DocDate,
      AdjdDocDate = doc.DocDate,
      AdjgFinPeriodID = doc.FinPeriodID,
      AdjdFinPeriodID = doc.FinPeriodID,
      AdjgTranPeriodID = doc.TranPeriodID,
      AdjdTranPeriodID = doc.TranPeriodID,
      CuryAdjgAmt = doc.CuryOrigDocAmt,
      CuryAdjdAmt = doc.CuryOrigDocAmt,
      AdjAmt = doc.OrigDocAmt,
      RGOLAmt = new Decimal?(0M),
      CuryAdjgDiscAmt = doc.CuryOrigDiscAmt,
      CuryAdjdDiscAmt = doc.CuryOrigDiscAmt,
      AdjDiscAmt = doc.OrigDiscAmt,
      Released = new bool?(false),
      InvoiceID = doc.NoteID,
      PaymentID = (doc.DocType != "CRM" ? doc.NoteID : new Guid?()),
      MemoID = (doc.DocType == "CRM" ? doc.NoteID : new Guid?())
    });
  }

  /// <summary>
  /// The method to process migrated document. A special self application with amount equal to
  /// difference between <see cref="P:PX.Objects.AR.ARRegister.CuryOrigDocAmt" /> and <see cref="P:PX.Objects.AR.ARRegister.CuryInitDocBal" />
  /// will be created for the document. Note, that all logic around <see cref="T:PX.Objects.AR.ARBalances" />, <see cref="T:PX.Objects.AR.ARHistory" /> and
  /// document balances is implemented inside this method, so we don't need to update any balances somewhere else.
  /// This is the reason why all special applications should be excluded from the adjustments processing.
  /// </summary>
  protected virtual void ProcessMigratedDocument(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARRegister doc,
    bool isDebit,
    Customer customer,
    PX.Objects.CM.Extensions.CurrencyInfo currencyinfo)
  {
    ARAdjust applicationForDocument = this.CreateSelfApplicationForDocument(doc);
    applicationForDocument.RGOLAmt = new Decimal?(0M);
    applicationForDocument.CuryAdjgDiscAmt = new Decimal?(0M);
    applicationForDocument.CuryAdjdDiscAmt = new Decimal?(0M);
    applicationForDocument.AdjDiscAmt = new Decimal?(0M);
    ARAdjust arAdjust1 = applicationForDocument;
    Decimal? curyAdjgAmt = arAdjust1.CuryAdjgAmt;
    Decimal? curyInitDocBal1 = doc.CuryInitDocBal;
    arAdjust1.CuryAdjgAmt = curyAdjgAmt.HasValue & curyInitDocBal1.HasValue ? new Decimal?(curyAdjgAmt.GetValueOrDefault() - curyInitDocBal1.GetValueOrDefault()) : new Decimal?();
    ARAdjust arAdjust2 = applicationForDocument;
    Decimal? curyAdjdAmt = arAdjust2.CuryAdjdAmt;
    Decimal? curyInitDocBal2 = doc.CuryInitDocBal;
    arAdjust2.CuryAdjdAmt = curyAdjdAmt.HasValue & curyInitDocBal2.HasValue ? new Decimal?(curyAdjdAmt.GetValueOrDefault() - curyInitDocBal2.GetValueOrDefault()) : new Decimal?();
    ARAdjust arAdjust3 = applicationForDocument;
    Decimal? adjAmt1 = arAdjust3.AdjAmt;
    Decimal? initDocBal = doc.InitDocBal;
    arAdjust3.AdjAmt = adjAmt1.HasValue & initDocBal.HasValue ? new Decimal?(adjAmt1.GetValueOrDefault() - initDocBal.GetValueOrDefault()) : new Decimal?();
    applicationForDocument.Released = new bool?(true);
    applicationForDocument.IsInitialApplication = new bool?(true);
    ARAdjust adj = (ARAdjust) ((PXSelectBase) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Cache.Update((object) applicationForDocument);
    ARRegister ardoc = doc;
    Decimal num = (Decimal) -1;
    Decimal? adjAmt2 = adj.AdjAmt;
    Decimal? nullable = adjAmt2.HasValue ? new Decimal?(num * adjAmt2.GetValueOrDefault()) : new Decimal?();
    Decimal? signBalance = doc.SignBalance;
    Decimal? CurrentBal = nullable.HasValue & signBalance.HasValue ? new Decimal?(nullable.GetValueOrDefault() * signBalance.GetValueOrDefault()) : new Decimal?();
    Decimal? UnreleasedBal = new Decimal?(0M);
    ARReleaseProcess.UpdateARBalances((PXGraph) this, ardoc, CurrentBal, UnreleasedBal);
    if (!adj.VoidAppl.GetValueOrDefault())
    {
      this.UpdateBalances(adj, doc);
      this.VerifyAdjustedDocumentAndClose(doc);
    }
    PX.Objects.GL.GLTran copy = (PX.Objects.GL.GLTran) ((PXSelectBase) je.GLTranModuleBatNbr).Cache.CreateCopy((object) tran);
    copy.TranClass = "N";
    copy.TranType = "CRM";
    copy.DebitAmt = isDebit ? adj.AdjAmt : new Decimal?(0M);
    copy.CuryDebitAmt = isDebit ? adj.CuryAdjgAmt : new Decimal?(0M);
    copy.CreditAmt = isDebit ? new Decimal?(0M) : adj.AdjAmt;
    copy.CuryCreditAmt = isDebit ? new Decimal?(0M) : adj.CuryAdjgAmt;
    this.UpdateHistory(copy, customer);
    this.UpdateHistory(copy, customer, currencyinfo);
    this.ProcessAdjustmentTranPost(adj, doc, doc, true);
    ARReleaseProcess.ARHistBucket arHistBucket = new ARReleaseProcess.ARHistBucket(tran, this.GetHistTranType(tran.TranType, tran.RefNbr));
    if (!(arHistBucket.SignDeposits != 0M))
      return;
    ARReleaseProcess.ARHistBucket bucket = new ARReleaseProcess.ARHistBucket();
    Decimal signDeposits = arHistBucket.SignDeposits;
    bucket.arAccountID = tran.AccountID;
    bucket.arSubID = tran.SubID;
    bucket.SignDeposits = signDeposits;
    bucket.SignPayments = -signDeposits;
    bucket.SignPtd = signDeposits;
    this.UpdateHistory(copy, customer, bucket);
    this.UpdateHistory(copy, customer, currencyinfo, bucket);
  }

  public virtual List<ARRegister> ReleaseInvoice(
    JournalEntry je,
    ARRegister doc,
    PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account> res,
    List<PMRegister> pmDocs)
  {
    ARInvoice arInvoice = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account>.op_Implicit(res);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account>.op_Implicit(res);
    PX.Objects.CS.Terms terms = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account>.op_Implicit(res);
    Customer customer = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account>.op_Implicit(res);
    PX.Objects.GL.Account account = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account>.op_Implicit(res);
    bool masterInstallment = arInvoice.InstallmentCntr.HasValue;
    ((PXSelectBase<ARInvoice>) this.ARInvoice_DocType_RefNbr).Current = arInvoice;
    ((PXSelectBase) this.ARDocument).Cache.Current = (object) doc;
    if (currencyInfo.BaseCuryID != currencyInfo.CuryID && arInvoice.InstallmentNbr.HasValue)
      currencyInfo.BaseCalc = new bool?(false);
    ((PXGraph) this).GetExtension<ARReleaseProcess.MultiCurrency>().StoreResult(currencyInfo);
    List<ARRegister> arRegisterList1 = new List<ARRegister>();
    if (!doc.Released.GetValueOrDefault())
    {
      bool? nullable1;
      if (!this._IsIntegrityCheck && doc.DocType != "SMC")
      {
        nullable1 = this.arsetup.PrintBeforeRelease;
        if (nullable1.Value)
        {
          nullable1 = arInvoice.Printed;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = arInvoice.DontPrint;
            if (!nullable1.GetValueOrDefault())
              throw new PXException("Invoice/Memo document was not printed and cannot be released.");
          }
        }
        nullable1 = this.arsetup.EmailBeforeRelease;
        if (nullable1.Value)
        {
          nullable1 = arInvoice.Emailed;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = arInvoice.DontEmail;
            if (!nullable1.GetValueOrDefault())
              throw new PXException("Invoice/Memo document was not emailed and cannot be released.");
          }
        }
      }
      nullable1 = arInvoice.CreditHold;
      if (nullable1.GetValueOrDefault())
        throw new PXException("The {0} {1} is on credit hold and cannot be released.", new object[2]
        {
          (object) GetLabel.For<ARDocType>(arInvoice.DocType),
          (object) arInvoice.RefNbr
        });
      string str1 = terms.InstallmentType;
      if (this._IsIntegrityCheck && !arInvoice.InstallmentNbr.HasValue)
      {
        string str2;
        string str3;
        if (!arInvoice.InstallmentCntr.HasValue)
          str3 = str2 = "S";
        else
          str2 = str3 = "M";
        str1 = str3;
      }
      if (str1 == "M" && (arInvoice.DocType == "CSL" || arInvoice.DocType == "RCS"))
        throw new PXException("Multiple Installments are not allowed for Cash Sale.");
      Decimal? nullable2;
      if (str1 == "M" && !arInvoice.InstallmentNbr.HasValue)
      {
        if (!this._IsIntegrityCheck)
          arRegisterList1 = this.CreateInstallments((PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>) res);
        masterInstallment = true;
        doc.CuryDocBal = new Decimal?(0M);
        doc.DocBal = new Decimal?(0M);
        doc.CuryDiscBal = new Decimal?(0M);
        doc.DiscBal = new Decimal?(0M);
        doc.CuryDiscTaken = new Decimal?(0M);
        doc.DiscTaken = new Decimal?(0M);
        doc.OpenDoc = new bool?(false);
        doc.ClosedDate = doc.DocDate;
        doc.ClosedFinPeriodID = doc.FinPeriodID;
        doc.ClosedTranPeriodID = doc.TranPeriodID;
        this.RaiseInvoiceEvent(doc, (SelectedEntityEvent<ARInvoice>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events>>>) (ev => ev.CloseDocument)));
        ARRegister ardoc = doc;
        Decimal num = -1M;
        nullable2 = doc.OrigDocAmt;
        Decimal? CurrentBal = nullable2.HasValue ? new Decimal?(num * nullable2.GetValueOrDefault()) : new Decimal?();
        Decimal? UnreleasedBal = new Decimal?(0M);
        ARReleaseProcess.UpdateARBalances((PXGraph) this, ardoc, CurrentBal, UnreleasedBal);
      }
      else
      {
        doc.CuryDocBal = doc.CuryOrigDocAmt;
        doc.DocBal = doc.OrigDocAmt;
        doc.CuryRetainageUnreleasedAmt = doc.CuryRetainageTotal;
        doc.RetainageUnreleasedAmt = doc.RetainageTotal;
        doc.CuryRetainageReleased = new Decimal?(0M);
        doc.RetainageReleased = new Decimal?(0M);
        doc.CuryRetainageUnpaidTotal = doc.CuryRetainageTotal;
        doc.RetainageUnpaidTotal = doc.RetainageTotal;
        doc.CuryRetainagePaidTotal = new Decimal?(0M);
        doc.RetainagePaidTotal = new Decimal?(0M);
        doc.CuryDiscBal = doc.CuryOrigDiscAmt;
        doc.DiscBal = doc.OrigDiscAmt;
        doc.CuryDiscTaken = new Decimal?(0M);
        doc.DiscTaken = new Decimal?(0M);
        doc.RGOLAmt = new Decimal?(0M);
        doc.OpenDoc = new bool?(true);
        doc.ClosedDate = new DateTime?();
        doc.ClosedFinPeriodID = (string) null;
        doc.ClosedTranPeriodID = (string) null;
        this.RaiseInvoiceEvent(doc, (SelectedEntityEvent<ARInvoice>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events>>>) (ev => ev.OpenDocument)));
        this.UpdateARBalancesDates((ARRegister) arInvoice);
      }
      PXCache<ARRegister>.RestoreCopy((ARRegister) arInvoice, doc);
      JournalEntry je1 = je;
      PX.Objects.CM.Extensions.CurrencyInfo info = currencyInfo;
      nullable1 = new bool?();
      bool? baseCalc = nullable1;
      PX.Objects.CM.Extensions.CurrencyInfo ex1 = PX.Objects.CM.Extensions.CurrencyInfo.GetEX(this.GetCurrencyInfoCopyForGL(je1, info, baseCalc));
      bool flag1 = arInvoice.DrCr == "D";
      if ((arInvoice.DocType == "CSL" ? 1 : (arInvoice.DocType == "RCS" ? 1 : 0)) != 0)
      {
        this.CheckOpenForReviewTrans(doc);
        PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
        glTran1.SummPost = new bool?(true);
        glTran1.ZeroPost = new bool?(false);
        glTran1.BranchID = arInvoice.BranchID;
        glTran1.AccountID = arInvoice.ARAccountID;
        glTran1.ReclassificationProhibited = new bool?(true);
        glTran1.SubID = arInvoice.ARSubID;
        PX.Objects.GL.GLTran glTran2 = glTran1;
        Decimal? nullable3;
        if (!flag1)
        {
          nullable2 = arInvoice.CuryOrigDocAmt;
          Decimal? curyOrigDiscAmt = arInvoice.CuryOrigDiscAmt;
          nullable3 = nullable2.HasValue & curyOrigDiscAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + curyOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
        }
        else
          nullable3 = new Decimal?(0M);
        glTran2.CuryDebitAmt = nullable3;
        PX.Objects.GL.GLTran glTran3 = glTran1;
        Decimal? nullable4;
        if (!flag1)
        {
          Decimal? origDocAmt = arInvoice.OrigDocAmt;
          nullable2 = arInvoice.OrigDiscAmt;
          nullable4 = origDocAmt.HasValue & nullable2.HasValue ? new Decimal?(origDocAmt.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        }
        else
          nullable4 = new Decimal?(0M);
        glTran3.DebitAmt = nullable4;
        PX.Objects.GL.GLTran glTran4 = glTran1;
        Decimal? nullable5;
        if (!flag1)
        {
          nullable5 = new Decimal?(0M);
        }
        else
        {
          nullable2 = arInvoice.CuryOrigDocAmt;
          Decimal? curyOrigDiscAmt = arInvoice.CuryOrigDiscAmt;
          nullable5 = nullable2.HasValue & curyOrigDiscAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + curyOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
        }
        glTran4.CuryCreditAmt = nullable5;
        PX.Objects.GL.GLTran glTran5 = glTran1;
        Decimal? nullable6;
        if (!flag1)
        {
          nullable6 = new Decimal?(0M);
        }
        else
        {
          Decimal? origDocAmt = arInvoice.OrigDocAmt;
          nullable2 = arInvoice.OrigDiscAmt;
          nullable6 = origDocAmt.HasValue & nullable2.HasValue ? new Decimal?(origDocAmt.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        }
        glTran5.CreditAmt = nullable6;
        glTran1.TranType = arInvoice.DocType;
        glTran1.TranClass = "N";
        glTran1.RefNbr = arInvoice.RefNbr;
        glTran1.TranDesc = arInvoice.DocDesc;
        FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran1, arInvoice.TranPeriodID);
        glTran1.TranDate = arInvoice.DocDate;
        glTran1.CuryInfoID = ex1.CuryInfoID;
        glTran1.Released = new bool?(true);
        glTran1.ReferenceID = arInvoice.CustomerID;
        this.SetProjectAndTaxID(glTran1, account, arInvoice);
        this.InsertInvoiceTransaction(je, glTran1, new ARReleaseProcess.GLTranInsertionContext()
        {
          ARRegisterRecord = doc
        });
      }
      else if (arInvoice.DocType != "SMC")
      {
        string empty = string.Empty;
        nullable1 = arInvoice.IsRetainageDocument;
        string str4 = !nullable1.GetValueOrDefault() ? (!arInvoice.IsPrepaymentInvoiceDocument() ? "N" : "P") : "F";
        PX.Objects.GL.GLTran glTran6 = new PX.Objects.GL.GLTran();
        glTran6.SummPost = new bool?(true);
        glTran6.BranchID = arInvoice.BranchID;
        glTran6.AccountID = arInvoice.ARAccountID;
        glTran6.ReclassificationProhibited = new bool?(true);
        glTran6.SubID = arInvoice.ARSubID;
        glTran6.CuryDebitAmt = flag1 ? new Decimal?(0M) : arInvoice.CuryOrigDocAmt;
        PX.Objects.GL.GLTran glTran7 = glTran6;
        Decimal? nullable7;
        if (!flag1)
        {
          nullable2 = arInvoice.OrigDocAmt;
          Decimal? rgolAmt = arInvoice.RGOLAmt;
          nullable7 = nullable2.HasValue & rgolAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - rgolAmt.GetValueOrDefault()) : new Decimal?();
        }
        else
          nullable7 = new Decimal?(0M);
        glTran7.DebitAmt = nullable7;
        glTran6.CuryCreditAmt = flag1 ? arInvoice.CuryOrigDocAmt : new Decimal?(0M);
        PX.Objects.GL.GLTran glTran8 = glTran6;
        Decimal? nullable8;
        if (!flag1)
        {
          nullable8 = new Decimal?(0M);
        }
        else
        {
          Decimal? origDocAmt = arInvoice.OrigDocAmt;
          nullable2 = arInvoice.RGOLAmt;
          nullable8 = origDocAmt.HasValue & nullable2.HasValue ? new Decimal?(origDocAmt.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
        }
        glTran8.CreditAmt = nullable8;
        glTran6.TranType = arInvoice.DocType;
        glTran6.TranClass = str4;
        glTran6.RefNbr = arInvoice.RefNbr;
        glTran6.TranDesc = arInvoice.DocDesc;
        FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran6, arInvoice.TranPeriodID);
        glTran6.TranDate = arInvoice.DocDate;
        glTran6.CuryInfoID = ex1.CuryInfoID;
        glTran6.Released = new bool?(true);
        glTran6.ReferenceID = arInvoice.CustomerID;
        this.SetProjectAndTaxID(glTran6, account, arInvoice);
        nullable1 = doc.OpenDoc;
        if (nullable1.GetValueOrDefault())
        {
          this.UpdateHistory(glTran6, customer);
          this.UpdateHistory(glTran6, customer, currencyInfo);
        }
        this.InsertInvoiceTransaction(je, glTran6, new ARReleaseProcess.GLTranInsertionContext()
        {
          ARRegisterRecord = doc
        });
        if (arInvoice.IsPrepaymentInvoiceDocument() || arInvoice.IsPrepaymentInvoiceDocumentReverse())
        {
          PX.Objects.GL.GLTran copy = (PX.Objects.GL.GLTran) ((PXSelectBase) je.GLTranModuleBatNbr).Cache.CreateCopy((object) glTran6);
          copy.SummPost = new bool?(true);
          copy.ReclassificationProhibited = new bool?(true);
          copy.AccountID = arInvoice.PrepaymentAccountID;
          copy.SubID = arInvoice.PrepaymentSubID;
          copy.BranchID = arInvoice.BranchID;
          copy.CreditAmt = glTran6.DebitAmt;
          copy.CuryCreditAmt = glTran6.CuryDebitAmt;
          copy.DebitAmt = glTran6.CreditAmt;
          copy.CuryDebitAmt = glTran6.CuryCreditAmt;
          copy.TranClass = "Y";
          nullable1 = doc.OpenDoc;
          if (nullable1.GetValueOrDefault())
          {
            this.UpdateHistory(copy, customer);
            this.UpdateHistory(copy, customer, currencyInfo);
          }
          this.InsertInvoiceTransaction(je, copy, new ARReleaseProcess.GLTranInsertionContext()
          {
            ARRegisterRecord = doc
          });
        }
        if (arInvoice.IsOriginalRetainageDocument())
        {
          PX.Objects.GL.GLTran copy = (PX.Objects.GL.GLTran) ((PXSelectBase) je.GLTranModuleBatNbr).Cache.CreateCopy((object) glTran6);
          copy.ReclassificationProhibited = new bool?(true);
          copy.AccountID = arInvoice.RetainageAcctID;
          copy.SubID = arInvoice.RetainageSubID;
          copy.CuryDebitAmt = flag1 ? new Decimal?(0M) : arInvoice.CuryRetainageTotal;
          copy.DebitAmt = flag1 ? new Decimal?(0M) : arInvoice.RetainageTotal;
          copy.CuryCreditAmt = flag1 ? arInvoice.CuryRetainageTotal : new Decimal?(0M);
          copy.CreditAmt = flag1 ? arInvoice.RetainageTotal : new Decimal?(0M);
          copy.OrigAccountID = glTran6.AccountID;
          copy.OrigSubID = glTran6.SubID;
          copy.TranClass = "E";
          this.UpdateHistory(copy, customer);
          this.UpdateHistory(copy, customer, currencyInfo);
          ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(copy);
          nullable1 = arInvoice.IsRetainageReversing;
          if (nullable1.GetValueOrDefault())
            this.ClearRetainageAmount(doc);
        }
        nullable1 = arInvoice.IsRetainageDocument;
        if (nullable1.GetValueOrDefault())
          this.ReleaseRetainageAmount(doc);
        if (this.IsMigratedDocumentForProcessing(doc))
        {
          this.ProcessMigratedDocument(je, glTran6, doc, flag1, customer, currencyInfo);
          doc = (ARRegister) ((PXSelectBase) this.ARDocument).Cache.Locate((object) doc) ?? doc;
          PXCache<ARRegister>.RestoreCopy((ARRegister) arInvoice, doc);
        }
      }
      IEqualityComparer<ARTaxTran> comparer1 = (IEqualityComparer<ARTaxTran>) new FieldSubsetEqualityComparer<ARTaxTran>(((PXGraph) this).Caches[typeof (ARTaxTran)], new System.Type[1]
      {
        typeof (ARTaxTran.recordID)
      });
      nullable1 = doc.PaymentsByLinesAllowed;
      int num1;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = doc.IsRetainageDocument;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = doc.RetainageApply;
          if (!nullable1.GetValueOrDefault())
            goto label_67;
        }
        nullable1 = doc.IsRetainageReversing;
        if (nullable1.GetValueOrDefault())
        {
          ARRegister retainageDocument = this.GetOriginalRetainageDocument(doc);
          if (retainageDocument == null)
          {
            num1 = 0;
            goto label_68;
          }
          nullable1 = retainageDocument.PaymentsByLinesAllowed;
          num1 = nullable1.GetValueOrDefault() ? 1 : 0;
          goto label_68;
        }
      }
label_67:
      num1 = 0;
label_68:
      bool flag2 = num1 != 0;
      nullable1 = doc.PaymentsByLinesAllowed;
      bool flag3 = nullable1.GetValueOrDefault() | flag2;
      List<int?> nullableList = new List<int?>();
      ARReleaseProcess.Amount itemDiscount = new ARReleaseProcess.Amount();
      foreach (IGrouping<ARTaxTran, PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>> grouping in ((IEnumerable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) this.ARTran_TranType_RefNbr).Select(new object[2]
      {
        (object) arInvoice.DocType,
        (object) arInvoice.RefNbr
      })).AsEnumerable<PXResult<ARTran>>().Cast<PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>>().GroupBy<PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>, ARTaxTran>((Func<PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>, ARTaxTran>) (row => PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(row)), comparer1))
      {
        ARTaxTran key = grouping.Key;
        List<ARTax> source = new List<ARTax>();
        foreach (PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran> pxResult in (IEnumerable<PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>>) grouping)
        {
          ARTran arTran = PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(pxResult);
          ARTax arTax = PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(pxResult);
          PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(pxResult);
          PX.Objects.SO.SOOrderType sotype = PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(pxResult);
          arTran.TranDate = arInvoice.DocDate;
          PXParentAttribute.SetParent(((PXSelectBase) this.ARTran_TranType_RefNbr).Cache, (object) arTran, typeof (ARRegister), (object) arInvoice);
          ((PXSelectBase) this.ARTran_TranType_RefNbr).Cache.SetDefaultExt<ARTran.finPeriodID>((object) arTran);
          FinPeriodIDAttribute.SetMasterPeriodID<ARTran.finPeriodID>(((PXSelectBase) this.ARTran_TranType_RefNbr).Cache, (object) arTran);
          if (!this._IsIntegrityCheck && !nullableList.Contains(arTran.LineNbr))
          {
            this.ProcessInvoiceDetailDiscount(doc, customer, arTran, sotype, arInvoice);
            nullableList.Add(arTran.LineNbr);
          }
          if (arTax.TranType != null && arTax.RefNbr != null && arTax.LineNbr.HasValue)
            source.Add(arTax);
        }
        if (source.Count<ARTax>() > 0 & flag3)
        {
          ARTaxAttribute arTaxAttribute = new ARTaxAttribute(typeof (ARRegister), typeof (ARTax), typeof (ARTaxTran));
          List<ARTax> taxDetList1 = source;
          Decimal? nullable9 = key.CuryTaxAmt;
          Decimal CuryTaxAmt1 = nullable9.Value;
          arTaxAttribute.DistributeTaxDiscrepancy<ARTax, ARTax.curyTaxAmt, ARTax.taxAmt>((PXGraph) this, (IEnumerable<ARTax>) taxDetList1, CuryTaxAmt1, true);
          nullable9 = key.CuryRetainedTaxAmt;
          Decimal num2 = 0M;
          if (!(nullable9.GetValueOrDefault() == num2 & nullable9.HasValue))
          {
            ARRetainedTaxAttribute retainedTaxAttribute = new ARRetainedTaxAttribute(typeof (ARRegister), typeof (ARTax), typeof (ARTaxTran));
            List<ARTax> taxDetList2 = source;
            nullable9 = key.CuryRetainedTaxAmt;
            Decimal CuryTaxAmt2 = nullable9.Value;
            retainedTaxAttribute.DistributeTaxDiscrepancy<ARTax, ARTax.curyRetainedTaxAmt, ARTax.retainedTaxAmt>((PXGraph) this, (IEnumerable<ARTax>) taxDetList2, CuryTaxAmt2, true);
          }
        }
      }
      List<PXResult<ARTran>> list = ((IEnumerable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) this.ARTran_TranType_RefNbr).Select(new object[2]
      {
        (object) arInvoice.DocType,
        (object) arInvoice.RefNbr
      })).ToList<PXResult<ARTran>>();
      this.FinPeriodUtils.AllowPostToUnlockedPeriodAnyway = this._IsIntegrityCheck;
      if (!this._IsIntegrityCheck)
        this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) list.Select<PXResult<ARTran>, ARTran>((Func<PXResult<ARTran>, ARTran>) (line => PXResult<ARTran>.op_Implicit(line))), typeof (OrganizationFinPeriod.aRClosed));
      this.FinPeriodUtils.AllowPostToUnlockedPeriodAnyway = false;
      if (!this._IsIntegrityCheck)
      {
        foreach (PXResult<ARTran> pxResult in list)
        {
          ARTran row = PXResult<ARTran>.op_Implicit(pxResult);
          if (!row.Released.GetValueOrDefault())
            ConvertedInventoryItemAttribute.ValidateRow(((PXSelectBase) this.ARTran_TranType_RefNbr).Cache, (object) row);
        }
      }
      bool flag4 = PXAccess.FeatureInstalled<FeaturesSet.aSC606>();
      ARReleaseProcess.Amount amount1 = new ARReleaseProcess.Amount(new Decimal?(0M), new Decimal?(0M));
      Decimal deferredNetDiscountRate = 0M;
      bool flag5 = false;
      int? nullable10;
      if (flag4)
      {
        int? defScheduleID;
        ARReleaseProcess.Amount netAmount = ASC606Helper.CalculateNetAmount((PXGraph) this, arInvoice, out deferredNetDiscountRate, out defScheduleID);
        if (!this._IsIntegrityCheck)
        {
          Decimal? cury = netAmount.Cury;
          Decimal num3 = 0M;
          if (!(cury.GetValueOrDefault() == num3 & cury.HasValue))
          {
            ((PXGraph) this.dr).Clear();
            try
            {
              (this.dr as DRSingleProcess).CreateSingleSchedule(arInvoice, netAmount, defScheduleID, false);
              ((PXGraph) this.dr).Actions.PressSave();
            }
            catch (ScheduleCuryTotalAmtLessOrEqualZeroException ex2)
            {
              flag5 = true;
              doc.DRSchedCntr = new int?(0);
              nullable10 = ((PXSelectBase<DRSetup>) this.dr.Setup).Current.SuspenseAccountID;
              if (!nullable10.HasValue)
                throw new PXException("The document cannot be released because the suspense account is not specified. To proceed, specify the account on the Deferred Revenue Preferences (DR101000) form.");
              nullable10 = ((PXSelectBase<DRSetup>) this.dr.Setup).Current.SuspenseSubID;
              if (!nullable10.HasValue)
                throw new PXException("The document cannot be released because the suspense subaccount is not specified. To proceed, specify the subaccount on the Deferred Revenue Preferences (DR101000) form.");
            }
          }
        }
      }
      APReleaseProcess.LineBalances lineBalances1 = new APReleaseProcess.LineBalances(new Decimal?(0M));
      ARTran arTran1 = (ARTran) null;
      ARTran tran1 = (ARTran) null;
      IEqualityComparer<ARTran> comparer2 = (IEqualityComparer<ARTran>) new FieldSubsetEqualityComparer<ARTran>(((PXSelectBase) this.ARTran_TranType_RefNbr).Cache, new System.Type[3]
      {
        typeof (ARTran.tranType),
        typeof (ARTran.refNbr),
        typeof (ARTran.lineNbr)
      });
      foreach (IGrouping<ARTran, PXResult<ARTran>> grouping in list.AsEnumerable<PXResult<ARTran>>().GroupBy<PXResult<ARTran>, ARTran>((Func<PXResult<ARTran>, ARTran>) (row => PXResult<ARTran>.op_Implicit(row)), comparer2))
      {
        ARTran key = grouping.Key;
        PXCache<ARTran>.StoreOriginal((PXGraph) this, key);
        if (!this._IsIntegrityCheck)
          key.ClearInvoiceDetailsBalance();
        if (!this._IsIntegrityCheck && key.Released.GetValueOrDefault())
          throw new PXException("Document Status is invalid for processing.");
        if (flag4)
        {
          nullable10 = key.InventoryID;
          if (!nullable10.HasValue && key.DeferredCode != null)
            throw new PXException("Inventory ID cannot be empty.");
        }
        if ((!flag4 || !(key.LineType == "DS") || !(deferredNetDiscountRate == 1M)) && !arInvoice.IsPrepaymentInvoiceDocument() && !arInvoice.IsPrepaymentInvoiceDocumentReverse())
        {
          bool flag6 = true;
          bool flag7 = str1 == "M" && !arInvoice.InstallmentNbr.HasValue;
          Decimal? nullable11;
          Decimal? nullable12;
          foreach (PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran> r in (IEnumerable<PXResult<ARTran>>) grouping)
          {
            ARTax arTax = PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(r);
            PX.Objects.TX.Tax taxCorrespondingToLine = PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(r);
            DRDeferredCode drDeferredCode = PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(r);
            ARTaxTran arTaxTran = PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(r);
            if (arTax != null && arTax.TaxID != null)
            {
              ARReleaseProcess.AdjustTaxCalculationLevelForNetGrossEntryMode((ARRegister) arInvoice, key, ref taxCorrespondingToLine);
              if (arInvoice.TaxCalcMode == "T" && arTaxTran.IsTaxInclusive.GetValueOrDefault())
                taxCorrespondingToLine.TaxCalcLevel = "0";
            }
            if (flag6)
            {
              PX.Objects.GL.GLTran glTran9 = new PX.Objects.GL.GLTran();
              glTran9.ReclassificationProhibited = new bool?(arInvoice.IsRetainageDocument.GetValueOrDefault());
              int num4;
              if (this.SummPost)
              {
                nullable10 = key.TaskID;
                if (!nullable10.HasValue)
                {
                  num4 = key.PMDeltaOption != "U" ? 1 : 0;
                  goto label_125;
                }
              }
              num4 = 0;
label_125:
              glTran9.SummPost = new bool?(num4 != 0);
              glTran9.BranchID = key.BranchID;
              glTran9.CuryInfoID = ex1.CuryInfoID;
              glTran9.TranType = key.TranType;
              glTran9.TranClass = arInvoice.DocClass;
              glTran9.RefNbr = key.RefNbr;
              glTran9.InventoryID = key.InventoryID;
              glTran9.UOM = key.UOM;
              Decimal? nullable13;
              if (!(key.DrCr == "C"))
              {
                Decimal num5 = (Decimal) -1;
                nullable11 = key.Qty;
                if (!nullable11.HasValue)
                {
                  nullable12 = new Decimal?();
                  nullable13 = nullable12;
                }
                else
                  nullable13 = new Decimal?(num5 * nullable11.GetValueOrDefault());
              }
              else
                nullable13 = key.Qty;
              glTran9.Qty = nullable13;
              glTran9.TranDate = key.TranDate;
              glTran9.ProjectID = arInvoice.IsRetainageDocument.GetValueOrDefault() ? ProjectDefaultAttribute.NonProject() : key.ProjectID;
              int? nullable14;
              if (!arInvoice.IsRetainageDocument.GetValueOrDefault())
              {
                nullable14 = key.TaskID;
              }
              else
              {
                nullable10 = new int?();
                nullable14 = nullable10;
              }
              glTran9.TaskID = nullable14;
              int? nullable15;
              if (!arInvoice.IsRetainageDocument.GetValueOrDefault())
              {
                nullable15 = key.CostCodeID;
              }
              else
              {
                nullable10 = new int?();
                nullable15 = nullable10;
              }
              glTran9.CostCodeID = nullable15;
              glTran9.AccountID = flag5 ? ((PXSelectBase<DRSetup>) this.dr.Setup).Current.SuspenseAccountID : key.AccountID;
              glTran9.SubID = flag5 ? ((PXSelectBase<DRSetup>) this.dr.Setup).Current.SuspenseSubID : this.GetValueInt<ARTran.subID>((PXGraph) je, (object) key);
              glTran9.TranDesc = key.TranDesc;
              glTran9.Released = new bool?(true);
              glTran9.ReferenceID = arInvoice.CustomerID;
              PX.Objects.GL.GLTran tran = glTran9;
              PX.Objects.GL.GLTran glTran10 = tran;
              int? nullable16;
              if (!tran.SummPost.GetValueOrDefault())
              {
                nullable16 = key.LineNbr;
              }
              else
              {
                nullable10 = new int?();
                nullable16 = nullable10;
              }
              glTran10.TranLineNbr = nullable16;
              ARReleaseProcess.Amount amount2 = ARReleaseProcess.GetSalesPostingAmount((PXGraph) this, doc, key, arTax, taxCorrespondingToLine, (Func<Decimal, Decimal>) (amount => PX.Objects.CM.PXDBCurrencyAttribute.Round(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) tran, amount, CMPrecision.TRANCURY)), (Func<Decimal, Decimal>) (amount => PX.Objects.CM.PXDBCurrencyAttribute.Round(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) tran, amount, CMPrecision.BASECURY)));
              if (flag4 && key.LineType == "DS")
              {
                Decimal num6 = 1M - deferredNetDiscountRate;
                PXCache cache1 = ((PXSelectBase) je.GLTranModuleBatNbr).Cache;
                PX.Objects.GL.GLTran row1 = tran;
                nullable11 = amount2.Cury;
                Decimal num7 = num6;
                Decimal? nullable17;
                if (!nullable11.HasValue)
                {
                  nullable12 = new Decimal?();
                  nullable17 = nullable12;
                }
                else
                  nullable17 = new Decimal?(nullable11.GetValueOrDefault() * num7);
                nullable12 = nullable17;
                Decimal val1 = nullable12.Value;
                Decimal? cury = new Decimal?(PX.Objects.CM.PXDBCurrencyAttribute.Round(cache1, (object) row1, val1, CMPrecision.TRANCURY));
                PXCache cache2 = ((PXSelectBase) je.GLTranModuleBatNbr).Cache;
                PX.Objects.GL.GLTran row2 = tran;
                nullable12 = amount2.Base;
                Decimal num8 = num6;
                Decimal val2 = (nullable12.HasValue ? new Decimal?(nullable12.GetValueOrDefault() * num8) : new Decimal?()).Value;
                Decimal? baaase = new Decimal?(PX.Objects.CM.PXDBCurrencyAttribute.Round(cache2, (object) row2, val2, CMPrecision.TRANCURY));
                amount2 = new ARReleaseProcess.Amount(cury, baaase);
              }
              tran.CuryDebitAmt = key.DrCr == "D" ? amount2.Cury : new Decimal?(0M);
              tran.DebitAmt = key.DrCr == "D" ? amount2.Base : new Decimal?(0M);
              tran.CuryCreditAmt = key.DrCr == "D" ? new Decimal?(0M) : amount2.Cury;
              tran.CreditAmt = key.DrCr == "D" ? new Decimal?(0M) : amount2.Base;
              bool? nullable18 = key.IsStockItem;
              nullable10 = key.OrigLineNbr;
              bool? nullable19;
              if (nullable10.HasValue)
              {
                PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, key.InventoryID);
                int num9;
                if (inventoryItem == null)
                {
                  num9 = 0;
                }
                else
                {
                  nullable19 = inventoryItem.IsConverted;
                  num9 = nullable19.GetValueOrDefault() ? 1 : 0;
                }
                if (num9 != 0)
                {
                  nullable19 = (bool?) ARTran.PK.Find((PXGraph) this, doc.OrigDocType, doc.OrigRefNbr, key.OrigLineNbr)?.IsStockItem;
                  nullable18 = nullable19 ?? nullable18;
                  key.IsStockItem = nullable18;
                }
              }
              if (!nullable18.GetValueOrDefault())
              {
                nullable19 = key.AccrueCost;
                if (nullable19.GetValueOrDefault())
                  this.CreateExpenseAccrualTransactions(je, doc, key, tran);
              }
              this.ReleaseInvoiceTransactionPostProcessing(je, arInvoice, r, tran);
              if (!this._IsIntegrityCheck)
              {
                IEnumerable<PX.Objects.GL.GLTran> source = (IEnumerable<PX.Objects.GL.GLTran>) new List<PX.Objects.GL.GLTran>();
                if (drDeferredCode != null && drDeferredCode.DeferredCodeID != null && !flag5)
                {
                  if (!flag4)
                  {
                    DRProcess dr = this.dr;
                    ARTran arTransaction = key;
                    DRDeferredCode deferralCode = drDeferredCode;
                    ARInvoice originalDocument = arInvoice;
                    nullable11 = amount2.Base;
                    Decimal amount3 = nullable11.Value;
                    dr.CreateSchedule(arTransaction, deferralCode, (ARRegister) originalDocument, amount3, false);
                    ((PXGraph) this.dr).Actions.PressSave();
                    source = je.CreateTransBySchedule(this.dr, tran);
                    JournalEntry journalEntry = je;
                    IEnumerable<PX.Objects.GL.GLTran> transactions = source;
                    PX.Objects.GL.GLTran templateTran = tran;
                    nullable11 = amount2.Cury;
                    Decimal curyExpectedTotal = nullable11.Value;
                    journalEntry.CorrectCuryAmountsDueToRounding(transactions, templateTran, curyExpectedTotal);
                  }
                  else if (((PXSelectBase<DRSchedule>) this.dr.Schedule).Current != null)
                    source = je.CreateTransBySchedule(this.dr, key, tran);
                  foreach (PX.Objects.GL.GLTran tran2 in source)
                    this.InsertInvoiceDetailsScheduleTransaction(je, tran2, new ARReleaseProcess.GLTranInsertionContext()
                    {
                      ARRegisterRecord = doc,
                      ARTranRecord = key
                    });
                }
                if ((source != null ? (!source.Any<PX.Objects.GL.GLTran>() ? 1 : 0) : 1) != 0)
                  this.InsertInvoiceDetailsTransaction(je, tran, new ARReleaseProcess.GLTranInsertionContext()
                  {
                    ARRegisterRecord = doc,
                    ARTranRecord = key
                  });
              }
              this.UpdateItemDiscountsHistory(key, (ARRegister) arInvoice);
              ARReleaseProcess.Amount amount4 = itemDiscount;
              nullable11 = key.CuryDiscAmt;
              Decimal? cury1 = new Decimal?(nullable11.GetValueOrDefault());
              nullable11 = key.DiscAmt;
              Decimal? baaase1 = new Decimal?(nullable11.GetValueOrDefault());
              ARReleaseProcess.Amount amount5 = new ARReleaseProcess.Amount(cury1, baaase1);
              itemDiscount = amount4 + amount5;
              this.ReleaseInvoiceTransactionPostProcessed(je, arInvoice, key);
              key.Released = new bool?(true);
              key.ReleasedToVerify = doc.ReleasedToVerify;
              flag6 = false;
            }
            if (!this._IsIntegrityCheck & flag3 && key.LineType != "DS" && !flag7)
            {
              APReleaseProcess.LineBalances lineBalances2 = this.AdjustInvoiceDetailsBalanceByTax(doc, key, arTax, taxCorrespondingToLine);
              lineBalances1 += lineBalances2;
            }
          }
          if (flag3 && key.LineType != "DS" && !flag7)
          {
            if (!this._IsIntegrityCheck)
            {
              APReleaseProcess.LineBalances lineBalances3 = this.AdjustInvoiceDetailsBalanceByLine(doc, key);
              lineBalances1 += lineBalances3;
            }
            key.RecoverInvoiceDetailsBalance();
            lineBalances1 += this.CalculateInvoiceDetailsCashDiscBalance(key, doc);
            ARTran arTran2;
            if (arTran1 != null)
            {
              Decimal? origRetainageAmt = arTran1.CuryOrigRetainageAmt;
              nullable12 = key.CuryOrigRetainageAmt;
              if (!(origRetainageAmt.GetValueOrDefault() < nullable12.GetValueOrDefault() & origRetainageAmt.HasValue & nullable12.HasValue))
              {
                arTran2 = arTran1;
                goto label_182;
              }
            }
            arTran2 = key;
label_182:
            arTran1 = arTran2;
            ARTran arTran3;
            if (tran1 != null)
            {
              nullable12 = tran1.CuryOrigTranAmt;
              nullable11 = key.CuryOrigTranAmt;
              if (!(nullable12.GetValueOrDefault() < nullable11.GetValueOrDefault() & nullable12.HasValue & nullable11.HasValue))
              {
                arTran3 = tran1;
                goto label_186;
              }
            }
            arTran3 = key;
label_186:
            tran1 = arTran3;
            if (arInvoice.IsOriginalRetainageDocument() && arInvoice.IsRetainageReversing.GetValueOrDefault())
            {
              ARTran originalRetainageLine = this.GetOriginalRetainageLine((ARRegister) arInvoice, key);
              if (originalRetainageLine != null)
              {
                key.CuryRetainageBal = new Decimal?(0M);
                key.RetainageBal = new Decimal?(0M);
                originalRetainageLine.CuryRetainageBal = new Decimal?(0M);
                originalRetainageLine.RetainageBal = new Decimal?(0M);
                ((PXSelectBase<ARTran>) this.ARTran_TranType_RefNbr).Update(originalRetainageLine);
              }
            }
            if (arInvoice.IsRetainageDocument.GetValueOrDefault())
              this.AdjustOriginalRetainageLineBalance((ARRegister) arInvoice, key, key.CuryOrigTranAmt, key.OrigTranAmt);
            if (flag2)
            {
              key.CuryTranBal = new Decimal?(0M);
              key.TranBal = new Decimal?(0M);
              key.CuryRetainageBal = new Decimal?(0M);
              key.RetainageBal = new Decimal?(0M);
            }
          }
          GraphHelper.MarkUpdated(((PXSelectBase) this.ARTran_TranType_RefNbr).Cache, (object) key);
        }
      }
      Decimal? nullable20;
      Decimal? nullable21;
      if (flag3 && !doc.Released.GetValueOrDefault())
      {
        nullable20 = lineBalances1.CashDiscountBalance.Cury;
        Decimal? curyOrigDiscAmt = doc.CuryOrigDiscAmt;
        if (nullable20.GetValueOrDefault() == curyOrigDiscAmt.GetValueOrDefault() & nullable20.HasValue == curyOrigDiscAmt.HasValue)
        {
          nullable21 = lineBalances1.CashDiscountBalance.Base;
          nullable20 = doc.OrigDiscAmt;
          if (nullable21.GetValueOrDefault() == nullable20.GetValueOrDefault() & nullable21.HasValue == nullable20.HasValue)
            goto label_206;
        }
        if (tran1 != null)
        {
          ARTran arTran4 = tran1;
          nullable20 = arTran4.CuryCashDiscBal;
          Decimal? cury = lineBalances1.CashDiscountBalance.Cury;
          Decimal? nullable22 = doc.CuryOrigDiscAmt;
          nullable21 = cury.HasValue & nullable22.HasValue ? new Decimal?(cury.GetValueOrDefault() - nullable22.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable23;
          if (!(nullable20.HasValue & nullable21.HasValue))
          {
            nullable22 = new Decimal?();
            nullable23 = nullable22;
          }
          else
            nullable23 = new Decimal?(nullable20.GetValueOrDefault() - nullable21.GetValueOrDefault());
          arTran4.CuryCashDiscBal = nullable23;
          ARTran arTran5 = tran1;
          nullable21 = arTran5.CashDiscBal;
          nullable22 = lineBalances1.CashDiscountBalance.Base;
          Decimal? origDiscAmt = doc.OrigDiscAmt;
          nullable20 = nullable22.HasValue & origDiscAmt.HasValue ? new Decimal?(nullable22.GetValueOrDefault() - origDiscAmt.GetValueOrDefault()) : new Decimal?();
          arTran5.CashDiscBal = nullable21.HasValue & nullable20.HasValue ? new Decimal?(nullable21.GetValueOrDefault() - nullable20.GetValueOrDefault()) : new Decimal?();
          ((PXSelectBase<ARTran>) this.ARTran_TranType_RefNbr).Update(tran1);
        }
label_206:
        if (!this._IsIntegrityCheck)
        {
          nullable20 = lineBalances1.RetainageBalance.Cury;
          nullable21 = doc.CuryRetainageTotal;
          if (!(nullable20.GetValueOrDefault() == nullable21.GetValueOrDefault() & nullable20.HasValue == nullable21.HasValue))
            throw new PXException("The sum of retainage balances of all detail lines is not equal to the document original retainage.");
          nullable21 = lineBalances1.RetainageBalance.Base;
          nullable20 = doc.RetainageTotal;
          if (!(nullable21.GetValueOrDefault() == nullable20.GetValueOrDefault() & nullable21.HasValue == nullable20.HasValue) && arTran1 != null)
          {
            nullable20 = lineBalances1.RetainageBalance.Base;
            nullable21 = doc.RetainageTotal;
            Decimal? nullable24 = nullable20.HasValue & nullable21.HasValue ? new Decimal?(nullable20.GetValueOrDefault() - nullable21.GetValueOrDefault()) : new Decimal?();
            ARTran arTran6 = tran1;
            nullable21 = arTran6.OrigRetainageAmt;
            nullable20 = nullable24;
            arTran6.OrigRetainageAmt = nullable21.HasValue & nullable20.HasValue ? new Decimal?(nullable21.GetValueOrDefault() - nullable20.GetValueOrDefault()) : new Decimal?();
            nullable20 = tran1.RetainageBal;
            Decimal num10 = 0M;
            if (!(nullable20.GetValueOrDefault() == num10 & nullable20.HasValue))
            {
              ARTran arTran7 = tran1;
              nullable20 = arTran7.RetainageBal;
              nullable21 = nullable24;
              arTran7.RetainageBal = nullable20.HasValue & nullable21.HasValue ? new Decimal?(nullable20.GetValueOrDefault() - nullable21.GetValueOrDefault()) : new Decimal?();
            }
            ((PXSelectBase<ARTran>) this.ARTran_TranType_RefNbr).Update(tran1);
          }
          nullable21 = lineBalances1.TranBalance.Cury;
          nullable20 = doc.CuryDocBal;
          if (!(nullable21.GetValueOrDefault() == nullable20.GetValueOrDefault() & nullable21.HasValue == nullable20.HasValue))
            throw new PXException("The sum of balances of all detail lines is not equal to the document balance.");
          nullable20 = lineBalances1.TranBalance.Base;
          nullable21 = doc.DocBal;
          if (!(nullable20.GetValueOrDefault() == nullable21.GetValueOrDefault() & nullable20.HasValue == nullable21.HasValue) && tran1 != null)
          {
            nullable21 = lineBalances1.TranBalance.Base;
            nullable20 = doc.DocBal;
            Decimal? baseAmount = nullable21.HasValue & nullable20.HasValue ? new Decimal?(nullable21.GetValueOrDefault() - nullable20.GetValueOrDefault()) : new Decimal?();
            ARTran arTran8 = tran1;
            nullable20 = arTran8.OrigTranAmt;
            nullable21 = baseAmount;
            arTran8.OrigTranAmt = nullable20.HasValue & nullable21.HasValue ? new Decimal?(nullable20.GetValueOrDefault() - nullable21.GetValueOrDefault()) : new Decimal?();
            nullable21 = tran1.TranBal;
            Decimal num11 = 0M;
            if (!(nullable21.GetValueOrDefault() == num11 & nullable21.HasValue))
            {
              ARTran arTran9 = tran1;
              nullable21 = arTran9.TranBal;
              nullable20 = baseAmount;
              arTran9.TranBal = nullable21.HasValue & nullable20.HasValue ? new Decimal?(nullable21.GetValueOrDefault() - nullable20.GetValueOrDefault()) : new Decimal?();
            }
            ((PXSelectBase<ARTran>) this.ARTran_TranType_RefNbr).Update(tran1);
            if (arInvoice.IsRetainageDocument.GetValueOrDefault())
              this.AdjustOriginalRetainageLineBalance((ARRegister) arInvoice, tran1, new Decimal?(0M), baseAmount);
          }
        }
      }
      Decimal docInclTaxDiscrepancy = 0.0M;
      foreach (PXResult<ARTaxTran, PX.Objects.TX.Tax, PX.Objects.GL.Account> pxResult in ((PXSelectBase<ARTaxTran>) this.ARTaxTran_TranType_RefNbr).Select(new object[2]
      {
        (object) arInvoice.DocType,
        (object) arInvoice.RefNbr
      }))
      {
        ARTaxTran arTaxTran = PXResult<ARTaxTran, PX.Objects.TX.Tax, PX.Objects.GL.Account>.op_Implicit(pxResult);
        PX.Objects.TX.Tax tax = PXResult<ARTaxTran, PX.Objects.TX.Tax, PX.Objects.GL.Account>.op_Implicit(pxResult);
        PX.Objects.GL.Account taxAccount = PXResult<ARTaxTran, PX.Objects.TX.Tax, PX.Objects.GL.Account>.op_Implicit(pxResult);
        if (arInvoice.TaxCalcMode == "G" || tax.TaxCalcLevel == "0" && arInvoice.TaxCalcMode != "N")
        {
          Decimal num12 = docInclTaxDiscrepancy;
          nullable20 = arTaxTran.CuryTaxAmtSumm;
          Decimal num13 = nullable20 ?? 0.0M;
          nullable20 = arTaxTran.CuryTaxAmt;
          Decimal num14 = nullable20 ?? 0.0M;
          Decimal num15 = num13 - num14;
          nullable20 = arTaxTran.CuryRetainedTaxAmtSumm;
          Decimal num16 = nullable20 ?? 0.0M;
          Decimal num17 = num15 + num16;
          nullable20 = arTaxTran.CuryRetainedTaxAmt;
          Decimal num18 = nullable20 ?? 0.0M;
          Decimal num19 = (num17 - num18) * (tax.ReverseTax.GetValueOrDefault() ? -1.0M : 1.0M);
          docInclTaxDiscrepancy = num12 + num19;
        }
        if (tax.TaxType == "Q")
          this.PostPerUnitTaxAmounts(je, arInvoice, ex1, arTaxTran, tax, flag1);
        else if (!doc.IsPrepaymentInvoiceDocument())
          this.PostGeneralTax(je, arInvoice, doc, tax, arTaxTran, taxAccount, ex1, flag1, customer);
        if (arInvoice.DocType == "CSL" || arInvoice.DocType == "RCS")
          this.PostReduceOnEarlyPaymentTran(je, arInvoice, arTaxTran.CuryTaxDiscountAmt, arTaxTran.TaxDiscountAmt, customer, ex1, flag1);
        arTaxTran.Released = new bool?(true);
        ((PXSelectBase) this.ARTaxTran_TranType_RefNbr).Cache.SetStatus((object) arTaxTran, (PXEntryStatus) 1);
        if (PXAccess.FeatureInstalled<FeaturesSet.vATReporting>() && !this._IsIntegrityCheck && (arTaxTran.TaxType == "B" || arTaxTran.TaxType == "A"))
        {
          PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
          {
            (object) arTaxTran.VendorID
          }));
          Decimal multByTranType = ReportTaxProcess.GetMultByTranType("AR", arTaxTran.TranType);
          string empty = string.Empty;
          string str5 = arTaxTran.TranType == "CSL" || arTaxTran.TranType == "RCS" ? "D" : (!(arTaxTran.TranType == "PPI") ? vendor?.SVATReversalMethod : "Y");
          List<ARRegister> arRegisterList2 = new List<ARRegister>()
          {
            doc
          };
          if (str1 == "M")
            arRegisterList2 = arRegisterList1;
          Decimal num20 = 0M;
          Decimal num21 = 0M;
          Decimal num22 = 0M;
          Decimal num23 = 0M;
          SVATConversionHist svatConversionHist1 = (SVATConversionHist) null;
          for (int index = 0; index < arRegisterList2.Count; ++index)
          {
            ARRegister arRegister = arRegisterList2[index];
            SVATConversionHist svatConversionHist2 = new SVATConversionHist()
            {
              Module = "AR",
              AdjdBranchID = arTaxTran.BranchID,
              AdjdDocType = arTaxTran.TranType,
              AdjdRefNbr = arRegister.RefNbr,
              AdjgDocType = arTaxTran.TranType,
              AdjgRefNbr = arRegister.RefNbr,
              AdjdDocDate = arRegister.DocDate,
              TaxID = arTaxTran.TaxID,
              TaxType = arTaxTran.TaxType,
              TaxRate = arTaxTran.TaxRate,
              VendorID = arTaxTran.VendorID,
              ReversalMethod = str5,
              CuryInfoID = arTaxTran.CuryInfoID
            };
            nullable20 = doc.CuryOrigDocAmt;
            Decimal num24 = 0M;
            Decimal? nullable25;
            if (nullable20.GetValueOrDefault() == num24 & nullable20.HasValue)
            {
              nullable25 = new Decimal?(0M);
            }
            else
            {
              nullable20 = arRegister.CuryOrigDocAmt;
              nullable21 = doc.CuryOrigDocAmt;
              nullable25 = nullable20.HasValue & nullable21.HasValue ? new Decimal?(nullable20.GetValueOrDefault() / nullable21.GetValueOrDefault()) : new Decimal?();
            }
            nullable21 = nullable25;
            Decimal valueOrDefault = nullable21.GetValueOrDefault();
            svatConversionHist2.FillAmounts(((PXGraph) this).GetExtension<ARReleaseProcess.MultiCurrency>().GetCurrencyInfo(arTaxTran.CuryInfoID), arTaxTran.CuryTaxableAmt, arTaxTran.CuryTaxAmt, valueOrDefault * multByTranType);
            FinPeriodIDAttribute.SetPeriodsByMaster<SVATConversionHist.adjdFinPeriodID>(((PXSelectBase) this.SVATConversionHistory).Cache, (object) svatConversionHist2, doc.TranPeriodID);
            Decimal num25 = num20;
            nullable21 = svatConversionHist2.TaxableAmt;
            Decimal num26 = nullable21.Value;
            num20 = num25 + num26;
            Decimal num27 = num21;
            nullable21 = svatConversionHist2.TaxAmt;
            Decimal num28 = nullable21.Value;
            num21 = num27 + num28;
            Decimal num29 = num22;
            nullable21 = svatConversionHist2.CuryTaxableAmt;
            Decimal num30 = nullable21.Value;
            num22 = num29 + num30;
            Decimal num31 = num23;
            nullable21 = svatConversionHist2.CuryTaxAmt;
            Decimal num32 = nullable21.Value;
            num23 = num31 + num32;
            SVATConversionHist svatConversionHist3 = ((PXSelectBase<SVATConversionHist>) this.SVATConversionHistory).Insert(svatConversionHist2);
            SVATConversionHist svatConversionHist4;
            if (svatConversionHist1 != null)
            {
              nullable21 = svatConversionHist3.CuryTaxAmt;
              nullable20 = svatConversionHist1.CuryTaxAmt;
              if (!(nullable21.GetValueOrDefault() > nullable20.GetValueOrDefault() & nullable21.HasValue & nullable20.HasValue))
              {
                svatConversionHist4 = svatConversionHist1;
                goto label_241;
              }
            }
            svatConversionHist4 = svatConversionHist3;
label_241:
            svatConversionHist1 = svatConversionHist4;
          }
          nullable21 = arTaxTran.TaxableAmt;
          Decimal num33 = multByTranType;
          nullable20 = nullable21.HasValue ? new Decimal?(nullable21.GetValueOrDefault() * num33) : new Decimal?();
          Decimal num34 = num20;
          Decimal? nullable26;
          if (!nullable20.HasValue)
          {
            nullable21 = new Decimal?();
            nullable26 = nullable21;
          }
          else
            nullable26 = new Decimal?(nullable20.GetValueOrDefault() - num34);
          Decimal? nullable27 = nullable26;
          nullable21 = arTaxTran.TaxAmt;
          Decimal num35 = multByTranType;
          nullable20 = nullable21.HasValue ? new Decimal?(nullable21.GetValueOrDefault() * num35) : new Decimal?();
          Decimal num36 = num21;
          Decimal? nullable28;
          if (!nullable20.HasValue)
          {
            nullable21 = new Decimal?();
            nullable28 = nullable21;
          }
          else
            nullable28 = new Decimal?(nullable20.GetValueOrDefault() - num36);
          Decimal? nullable29 = nullable28;
          nullable20 = nullable27;
          Decimal num37 = 0M;
          if (nullable20.GetValueOrDefault() == num37 & nullable20.HasValue)
          {
            nullable20 = nullable29;
            Decimal num38 = 0M;
            if (nullable20.GetValueOrDefault() == num38 & nullable20.HasValue)
              goto label_252;
          }
          SVATConversionHist svatConversionHist5 = svatConversionHist1;
          nullable20 = svatConversionHist5.TaxableAmt;
          nullable21 = nullable27;
          svatConversionHist5.TaxableAmt = nullable20.HasValue & nullable21.HasValue ? new Decimal?(nullable20.GetValueOrDefault() + nullable21.GetValueOrDefault()) : new Decimal?();
          SVATConversionHist svatConversionHist6 = svatConversionHist1;
          nullable21 = svatConversionHist6.TaxAmt;
          nullable20 = nullable29;
          svatConversionHist6.TaxAmt = nullable21.HasValue & nullable20.HasValue ? new Decimal?(nullable21.GetValueOrDefault() + nullable20.GetValueOrDefault()) : new Decimal?();
          svatConversionHist1.UnrecognizedTaxAmt = svatConversionHist1.TaxAmt;
          svatConversionHist1 = ((PXSelectBase<SVATConversionHist>) this.SVATConversionHistory).Update(svatConversionHist1);
label_252:
          nullable21 = arTaxTran.CuryTaxableAmt;
          Decimal num39 = multByTranType;
          nullable20 = nullable21.HasValue ? new Decimal?(nullable21.GetValueOrDefault() * num39) : new Decimal?();
          Decimal num40 = num22;
          Decimal? nullable30;
          if (!nullable20.HasValue)
          {
            nullable21 = new Decimal?();
            nullable30 = nullable21;
          }
          else
            nullable30 = new Decimal?(nullable20.GetValueOrDefault() - num40);
          Decimal? nullable31 = nullable30;
          nullable21 = arTaxTran.CuryTaxAmt;
          Decimal num41 = multByTranType;
          nullable20 = nullable21.HasValue ? new Decimal?(nullable21.GetValueOrDefault() * num41) : new Decimal?();
          Decimal num42 = num23;
          Decimal? nullable32;
          if (!nullable20.HasValue)
          {
            nullable21 = new Decimal?();
            nullable32 = nullable21;
          }
          else
            nullable32 = new Decimal?(nullable20.GetValueOrDefault() - num42);
          Decimal? nullable33 = nullable32;
          nullable20 = nullable31;
          Decimal num43 = 0M;
          if (nullable20.GetValueOrDefault() == num43 & nullable20.HasValue)
          {
            nullable20 = nullable33;
            Decimal num44 = 0M;
            if (nullable20.GetValueOrDefault() == num44 & nullable20.HasValue)
              goto label_261;
          }
          SVATConversionHist svatConversionHist7 = svatConversionHist1;
          nullable20 = svatConversionHist7.CuryTaxableAmt;
          nullable21 = nullable31;
          svatConversionHist7.CuryTaxableAmt = nullable20.HasValue & nullable21.HasValue ? new Decimal?(nullable20.GetValueOrDefault() + nullable21.GetValueOrDefault()) : new Decimal?();
          SVATConversionHist svatConversionHist8 = svatConversionHist1;
          nullable21 = svatConversionHist8.CuryTaxAmt;
          nullable20 = nullable33;
          svatConversionHist8.CuryTaxAmt = nullable21.HasValue & nullable20.HasValue ? new Decimal?(nullable21.GetValueOrDefault() + nullable20.GetValueOrDefault()) : new Decimal?();
          svatConversionHist1.CuryUnrecognizedTaxAmt = svatConversionHist1.CuryTaxAmt;
          ((PXSelectBase<SVATConversionHist>) this.SVATConversionHistory).Update(svatConversionHist1);
        }
label_261:
        if (!this._IsIntegrityCheck && arInvoice.IsPrepaymentInvoiceDocument())
        {
          string str6 = "Y";
          List<ARRegister> arRegisterList3 = new List<ARRegister>()
          {
            doc
          };
          for (int index = 0; index < arRegisterList3.Count; ++index)
          {
            ARRegister arRegister = arRegisterList3[index];
            SVATConversionHist svatConversionHist = new SVATConversionHist()
            {
              Module = "AR",
              AdjdBranchID = arTaxTran.BranchID,
              AdjdDocType = arTaxTran.TranType,
              AdjdRefNbr = arRegister.RefNbr,
              AdjgDocType = arTaxTran.TranType,
              AdjgRefNbr = arRegister.RefNbr,
              AdjdDocDate = arRegister.DocDate,
              TaxID = arTaxTran.TaxID,
              TaxType = arTaxTran.TaxType,
              TaxRate = arTaxTran.TaxRate,
              VendorID = arTaxTran.VendorID,
              ReversalMethod = str6,
              CuryInfoID = arTaxTran.CuryInfoID
            };
            svatConversionHist.FillAmounts(((PXGraph) this).GetExtension<ARReleaseProcess.MultiCurrency>().GetCurrencyInfo(arTaxTran.CuryInfoID), arTaxTran.CuryTaxableAmt, arTaxTran.CuryTaxAmt, 1M);
            FinPeriodIDAttribute.SetPeriodsByMaster<SVATConversionHist.adjdFinPeriodID>(((PXSelectBase) this.SVATConversionHistory).Cache, (object) svatConversionHist, doc.TranPeriodID);
            ((PXSelectBase<SVATConversionHist>) this.SVATConversionHistory).Insert(svatConversionHist);
          }
        }
      }
      foreach (PXResult<ARSalesPerTran> pxResult in ((PXSelectBase<ARSalesPerTran>) this.ARDoc_SalesPerTrans).Select(new object[2]
      {
        (object) doc.DocType,
        (object) doc.RefNbr
      }))
      {
        ARSalesPerTran arSalesPerTran = PXResult<ARSalesPerTran>.op_Implicit(pxResult);
        PXCache<ARSalesPerTran>.StoreOriginal((PXGraph) this, arSalesPerTran);
        arSalesPerTran.Released = doc.OpenDoc;
        ((PXSelectBase) this.ARDoc_SalesPerTrans).Cache.Update((object) arSalesPerTran);
      }
      this.ProcessOriginTranPost(arInvoice, itemDiscount, masterInstallment);
      if (arInvoice.IsPrepaymentInvoiceDocument())
        this.ProcessPrepaymentTranPost(arInvoice);
      if (arInvoice.IsRetainageDocument.GetValueOrDefault())
        this.ProcessRetainageTranPost(arInvoice);
      if (arInvoice.DocType == "SMC" && arInvoice.Voided.GetValueOrDefault())
      {
        ARAdjust lastAdjustment = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjdDocType, Equal<Current<ARRegister.docType>>, And<ARAdjust.adjdRefNbr, Equal<Current<ARRegister.refNbr>>>>, OrderBy<Desc<ARAdjust.adjNbr>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
        {
          (object) arInvoice
        }, Array.Empty<object>()));
        this.ProcessVoidWOTranPost((ARRegister) arInvoice, lastAdjustment);
      }
      if (!this._IsIntegrityCheck)
      {
        foreach (PXResult<ARAdjust, ARPayment> pxResult in PXSelectBase<ARAdjust, PXSelectJoin<ARAdjust, InnerJoin<ARPayment, On<ARPayment.docType, Equal<ARAdjust.adjgDocType>, And<ARPayment.refNbr, Equal<ARAdjust.adjgRefNbr>>>>, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.released, Equal<False>, And<ARAdjust.voided, Equal<False>>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) doc.DocType,
          (object) doc.RefNbr
        }))
        {
          ARAdjust row3 = PXResult<ARAdjust, ARPayment>.op_Implicit(pxResult);
          ARPayment row4 = PXResult<ARAdjust, ARPayment>.op_Implicit(pxResult);
          Decimal? nullable34 = row3.CuryAdjdAmt;
          Decimal num45 = 0M;
          bool? nullable35;
          if (!(nullable34.GetValueOrDefault() > num45 & nullable34.HasValue))
          {
            nullable35 = row3.Hold;
            if (!nullable35.GetValueOrDefault())
              continue;
          }
          if (str1 != null && str1 != "S")
            throw new PXException("No applications can be created for documents with multiple installment credit terms specified.");
          if (row3.AdjdDocType != "SMC" && string.Compare(row4.AdjTranPeriodID, row3.AdjgTranPeriodID) >= 0)
          {
            row3.AdjgDocDate = row4.AdjDate;
            FinPeriodIDAttribute.SetPeriodsByMaster<ARAdjust.adjgFinPeriodID>(((PXSelectBase) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Cache, (object) row3, row4.AdjTranPeriodID);
          }
          nullable35 = row4.Released;
          if (nullable35.GetValueOrDefault())
          {
            if (DateTime.Compare(row4.AdjDate.Value, row3.AdjdDocDate.Value) < 0)
            {
              row4.AdjDate = row3.AdjdDocDate;
              FinPeriodIDAttribute.SetPeriodsByMaster<ARPayment.adjFinPeriodID>(((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache, (object) row4, row3.AdjdTranPeriodID);
            }
            else if (string.CompareOrdinal(row4.AdjTranPeriodID, row3.AdjdTranPeriodID) < 0)
              FinPeriodIDAttribute.SetPeriodsByMaster<ARPayment.adjFinPeriodID>(((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache, (object) row4, row3.AdjdTranPeriodID);
            arRegisterList1.Add((ARRegister) row4);
            ARRegister arRegister = (ARRegister) ((PXSelectBase) this.ARDocument).Cache.Locate((object) row4);
            if (arRegister != null)
            {
              PXCache<ARRegister>.RestoreCopy((ARRegister) row4, arRegister);
              ((PXSelectBase) this.ARDocument).Cache.SetStatus((object) arRegister, (PXEntryStatus) 0);
            }
            using (new DisableFormulaCalculationScope(((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache, new System.Type[1]
            {
              typeof (ARRegister.curyRetainageReleased)
            }))
              ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.Update((object) row4);
          }
          Decimal? nullable36 = row3.RGOLAmt;
          nullable34 = row3.CuryAdjdWOAmt;
          Decimal num46 = 0M;
          Decimal? nullable37;
          if (!(nullable34.GetValueOrDefault() == num46 & nullable34.HasValue))
          {
            ARInvoiceEntry.MultiCurrency extension = ((PXGraph) this.InvoiceEntryGraph).GetExtension<ARInvoiceEntry.MultiCurrency>();
            Decimal? rgolAmt = new RGOLCalculator(extension.GetCurrencyInfo(row3.AdjdCuryInfoID), extension.GetCurrencyInfo(row3.AdjgCuryInfoID), extension.GetCurrencyInfo(row4.CuryInfoID)).CalcRGOL(row3.CuryAdjdWhTaxAmt, row3.AdjWhTaxAmt).RgolAmt;
            ARAdjust arAdjust = row3;
            nullable34 = arAdjust.AdjWOAmt;
            nullable37 = rgolAmt;
            arAdjust.AdjWOAmt = nullable34.HasValue & nullable37.HasValue ? new Decimal?(nullable34.GetValueOrDefault() + nullable37.GetValueOrDefault()) : new Decimal?();
            nullable37 = nullable36;
            nullable34 = rgolAmt;
            nullable36 = nullable37.HasValue & nullable34.HasValue ? new Decimal?(nullable37.GetValueOrDefault() - nullable34.GetValueOrDefault()) : new Decimal?();
          }
          nullable35 = ARDocType.Payable(doc.DocType);
          if (nullable35.GetValueOrDefault())
          {
            ARAdjust arAdjust1 = row3;
            nullable34 = arAdjust1.AdjAmt;
            nullable37 = nullable36;
            arAdjust1.AdjAmt = nullable34.HasValue & nullable37.HasValue ? new Decimal?(nullable34.GetValueOrDefault() + nullable37.GetValueOrDefault()) : new Decimal?();
            ARAdjust arAdjust2 = row3;
            nullable37 = row3.RGOLAmt;
            Decimal? nullable38;
            if (!nullable37.HasValue)
            {
              nullable34 = new Decimal?();
              nullable38 = nullable34;
            }
            else
              nullable38 = new Decimal?(-nullable37.GetValueOrDefault());
            arAdjust2.RGOLAmt = nullable38;
          }
          else
          {
            ARAdjust arAdjust3 = row3;
            nullable37 = arAdjust3.AdjAmt;
            nullable34 = nullable36;
            arAdjust3.AdjAmt = nullable37.HasValue & nullable34.HasValue ? new Decimal?(nullable37.GetValueOrDefault() - nullable34.GetValueOrDefault()) : new Decimal?();
            ARAdjust arAdjust4 = row3;
            nullable34 = row3.RGOLAmt;
            Decimal? nullable39;
            if (!nullable34.HasValue)
            {
              nullable37 = new Decimal?();
              nullable39 = nullable37;
            }
            else
              nullable39 = new Decimal?(-nullable34.GetValueOrDefault());
            arAdjust4.RGOLAmt = nullable39;
          }
          row3.Hold = new bool?(false);
          ((PXSelectBase) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Cache.SetStatus((object) row3, (PXEntryStatus) 1);
        }
        if (doc.DocType == "CRM")
        {
          doc.AdjCntr = new int?(0);
          arRegisterList1.Insert(0, doc);
        }
      }
      PX.Objects.GL.Batch current = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current;
      this.ReleaseInvoiceBatchPostProcessing(je, arInvoice, current);
      Decimal? curyDebitTotal1 = current.CuryDebitTotal;
      Decimal? nullable40 = current.CuryCreditTotal;
      Decimal? nullable41 = curyDebitTotal1.HasValue & nullable40.HasValue ? new Decimal?(curyDebitTotal1.GetValueOrDefault() - nullable40.GetValueOrDefault()) : new Decimal?();
      Decimal curyCreditDiff = Math.Round(nullable41.Value, 4);
      Decimal? debitTotal1 = current.DebitTotal;
      nullable40 = current.CreditTotal;
      Decimal? nullable42;
      if (!(debitTotal1.HasValue & nullable40.HasValue))
      {
        nullable41 = new Decimal?();
        nullable42 = nullable41;
      }
      else
        nullable42 = new Decimal?(debitTotal1.GetValueOrDefault() - nullable40.GetValueOrDefault());
      nullable41 = nullable42;
      Decimal creditDiff = Math.Round(nullable41.Value, 4);
      if (docInclTaxDiscrepancy != 0M && !arInvoice.IsPrepaymentInvoiceDocument() && !arInvoice.IsPrepaymentInvoiceDocumentReverse())
      {
        this.ProcessTaxDiscrepancy(je, current, arInvoice, ex1, docInclTaxDiscrepancy);
        Decimal? curyDebitTotal2 = current.CuryDebitTotal;
        nullable40 = current.CuryCreditTotal;
        Decimal? nullable43;
        if (!(curyDebitTotal2.HasValue & nullable40.HasValue))
        {
          nullable41 = new Decimal?();
          nullable43 = nullable41;
        }
        else
          nullable43 = new Decimal?(curyDebitTotal2.GetValueOrDefault() - nullable40.GetValueOrDefault());
        nullable41 = nullable43;
        curyCreditDiff = Math.Round(nullable41.Value, 4);
        Decimal? debitTotal2 = current.DebitTotal;
        nullable40 = current.CreditTotal;
        Decimal? nullable44;
        if (!(debitTotal2.HasValue & nullable40.HasValue))
        {
          nullable41 = new Decimal?();
          nullable44 = nullable41;
        }
        else
          nullable44 = new Decimal?(debitTotal2.GetValueOrDefault() - nullable40.GetValueOrDefault());
        nullable41 = nullable44;
        creditDiff = Math.Round(nullable41.Value, 4);
      }
      if (Math.Abs(curyCreditDiff) >= 0.00005M)
        this.VerifyRoundingAllowed(arInvoice, current, ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Current.BaseCuryID);
      if (Math.Abs(curyCreditDiff) >= 0.00005M || Math.Abs(creditDiff) >= 0.00005M)
        this.ProcessInvoiceRounding(je, current, arInvoice, curyCreditDiff, creditDiff, currencyInfo);
      if (doc.HasZeroBalance<ARRegister.curyDocBal, ARTran.curyTranBal>((PXGraph) this) && (!doc.IsOriginalRetainageDocument() || doc.HasZeroBalance<ARRegister.curyRetainageUnreleasedAmt, ARTran.curyRetainageBal>((PXGraph) this)) && (!doc.IsOriginalRetainageDocument() || doc.DocType != "CRM"))
      {
        doc.DocBal = new Decimal?(0M);
        doc.CuryDiscBal = new Decimal?(0M);
        doc.DiscBal = new Decimal?(0M);
        doc.OpenDoc = new bool?(false);
        doc.ClosedDate = doc.DocDate;
        doc.ClosedFinPeriodID = doc.FinPeriodID;
        doc.ClosedTranPeriodID = doc.TranPeriodID;
        this.RaiseInvoiceEvent(doc, (SelectedEntityEvent<ARInvoice>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events>>>) (ev => ev.CloseDocument)));
      }
    }
    return arRegisterList1;
  }

  protected virtual void AdjustOriginalRetainageLineBalance(
    ARRegister document,
    ARTran tran,
    Decimal? curyAmount,
    Decimal? baseAmount)
  {
    ARTran originalRetainageLine = this.GetOriginalRetainageLine(tran);
    if (originalRetainageLine == null)
      return;
    Decimal? nullable1 = ARDocType.SignAmount(originalRetainageLine.TranType);
    Decimal? nullable2 = document.SignAmount;
    Decimal valueOrDefault = (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
    ARTran arTran1 = originalRetainageLine;
    Decimal? nullable3 = arTran1.CuryRetainageBal;
    Decimal num1 = curyAmount.GetValueOrDefault() * valueOrDefault;
    Decimal? nullable4;
    if (!nullable3.HasValue)
    {
      nullable2 = new Decimal?();
      nullable4 = nullable2;
    }
    else
      nullable4 = new Decimal?(nullable3.GetValueOrDefault() - num1);
    arTran1.CuryRetainageBal = nullable4;
    ARTran arTran2 = originalRetainageLine;
    nullable3 = arTran2.RetainageBal;
    Decimal num2 = baseAmount.GetValueOrDefault() * valueOrDefault;
    Decimal? nullable5;
    if (!nullable3.HasValue)
    {
      nullable2 = new Decimal?();
      nullable5 = nullable2;
    }
    else
      nullable5 = new Decimal?(nullable3.GetValueOrDefault() - num2);
    arTran2.RetainageBal = nullable5;
    ARTran arTran3 = ((PXSelectBase<ARTran>) this.ARTran_TranType_RefNbr).Update(originalRetainageLine);
    nullable3 = arTran3.CuryOrigRetainageAmt;
    Decimal num3 = 0M;
    Sign sign1 = nullable3.GetValueOrDefault() < num3 & nullable3.HasValue ? Sign.Minus : Sign.Plus;
    if (!this._IsIntegrityCheck)
    {
      nullable2 = arTran3.CuryRetainageBal;
      Sign sign2 = sign1;
      nullable3 = nullable2.HasValue ? new Decimal?(Sign.op_Multiply(nullable2.GetValueOrDefault(), sign2)) : new Decimal?();
      Decimal num4 = 0M;
      if (!(nullable3.GetValueOrDefault() < num4 & nullable3.HasValue))
      {
        Decimal? nullable6 = arTran3.CuryRetainageBal;
        Sign sign3 = sign1;
        nullable3 = nullable6.HasValue ? new Decimal?(Sign.op_Multiply(nullable6.GetValueOrDefault(), sign3)) : new Decimal?();
        nullable6 = arTran3.CuryOrigRetainageAmt;
        Sign sign4 = sign1;
        nullable2 = nullable6.HasValue ? new Decimal?(Sign.op_Multiply(nullable6.GetValueOrDefault(), sign4)) : new Decimal?();
        if (!(nullable3.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable3.HasValue & nullable2.HasValue))
          return;
      }
      throw new PXException("The document cannot be released because the retainage has been fully released for the related original document.");
    }
  }

  protected virtual APReleaseProcess.LineBalances AdjustInvoiceDetailsBalanceByLine(
    ARRegister doc,
    ARTran tran)
  {
    ARTran arTran1 = tran;
    Decimal? nullable1 = arTran1.CuryOrigRetainageAmt;
    Decimal? curyRetainageAmt = tran.CuryRetainageAmt;
    arTran1.CuryOrigRetainageAmt = nullable1.HasValue & curyRetainageAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + curyRetainageAmt.GetValueOrDefault()) : new Decimal?();
    ARTran arTran2 = tran;
    Decimal? nullable2 = arTran2.OrigRetainageAmt;
    nullable1 = tran.RetainageAmt;
    arTran2.OrigRetainageAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    ARTran arTran3 = tran;
    nullable1 = arTran3.CuryOrigTranAmt;
    nullable2 = tran.CuryTranAmt;
    arTran3.CuryOrigTranAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    ARTran arTran4 = tran;
    nullable2 = arTran4.OrigTranAmt;
    nullable1 = tran.TranAmt;
    arTran4.OrigTranAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    ARReleaseProcess.Amount cashDiscountBalance = new ARReleaseProcess.Amount();
    nullable1 = tran.CuryRetainageAmt;
    Decimal? cury1 = new Decimal?(nullable1.GetValueOrDefault());
    nullable1 = tran.RetainageAmt;
    Decimal? baaase1 = new Decimal?(nullable1.GetValueOrDefault());
    ARReleaseProcess.Amount retainageBalance = new ARReleaseProcess.Amount(cury1, baaase1);
    nullable1 = tran.CuryTranAmt;
    Decimal? cury2 = new Decimal?(nullable1.GetValueOrDefault());
    nullable1 = tran.TranAmt;
    Decimal? baaase2 = new Decimal?(nullable1.GetValueOrDefault());
    ARReleaseProcess.Amount tranBalance = new ARReleaseProcess.Amount(cury2, baaase2);
    return new APReleaseProcess.LineBalances(cashDiscountBalance, retainageBalance, tranBalance);
  }

  protected virtual APReleaseProcess.LineBalances CalculateInvoiceDetailsCashDiscBalance(
    ARTran tran,
    ARRegister doc)
  {
    Decimal? nullable1;
    Decimal num1;
    if (!(doc.CuryOrigDocAmt.GetValueOrDefault() != 0M))
    {
      num1 = 0M;
    }
    else
    {
      nullable1 = tran.CuryOrigTranAmt;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = doc.CuryOrigDocAmt;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      num1 = valueOrDefault1 / valueOrDefault2;
    }
    Decimal num2 = num1;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXGraph) this).GetExtension<ARReleaseProcess.MultiCurrency>().GetCurrencyInfo(tran.CuryInfoID);
    ARTran arTran1 = tran;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = currencyInfo1;
    nullable1 = doc.CuryOrigDiscAmt;
    Decimal val1 = nullable1.GetValueOrDefault() * num2;
    Decimal? nullable2 = new Decimal?(currencyInfo2.RoundCury(val1));
    arTran1.CuryCashDiscBal = nullable2;
    ARTran arTran2 = tran;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo1;
    nullable1 = doc.OrigDiscAmt;
    Decimal val2 = nullable1.GetValueOrDefault() * num2;
    Decimal? nullable3 = new Decimal?(currencyInfo3.RoundCury(val2));
    arTran2.CashDiscBal = nullable3;
    nullable1 = tran.CuryCashDiscBal;
    Decimal? cury = new Decimal?(nullable1.GetValueOrDefault());
    nullable1 = tran.CashDiscBal;
    Decimal? baaase = new Decimal?(nullable1.GetValueOrDefault());
    return new APReleaseProcess.LineBalances(new ARReleaseProcess.Amount(cury, baaase), new ARReleaseProcess.Amount(), new ARReleaseProcess.Amount());
  }

  public static bool IncludeTaxInLineBalance(PX.Objects.TX.Tax tax)
  {
    return tax != null && tax.TaxType != "P" && tax.TaxType != "W" && tax.TaxCalcLevel != "0";
  }

  protected virtual APReleaseProcess.LineBalances AdjustInvoiceDetailsBalanceByTax(
    ARRegister doc,
    ARTran tran,
    ARTax artax,
    PX.Objects.TX.Tax tax)
  {
    int num1 = artax == null || artax.TaxID == null ? 0 : (ARReleaseProcess.IncludeTaxInLineBalance(tax) ? 1 : 0);
    bool flag = artax != null && artax.TaxID != null && tax != null && tax.TaxType != "P";
    Decimal num2 = tax.ReverseTax.GetValueOrDefault() ? -1M : 1M;
    Decimal? nullable = artax.CuryTaxAmt;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = artax.CuryExpenseAmt;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    Decimal num3 = valueOrDefault1 + valueOrDefault2;
    nullable = artax.TaxAmt;
    Decimal valueOrDefault3 = nullable.GetValueOrDefault();
    nullable = artax.ExpenseAmt;
    Decimal valueOrDefault4 = nullable.GetValueOrDefault();
    Decimal num4 = valueOrDefault3 + valueOrDefault4;
    Decimal num5 = num3 * num2;
    Decimal num6 = num4 * num2;
    nullable = artax.CuryRetainedTaxAmt;
    Decimal valueOrDefault5 = nullable.GetValueOrDefault();
    nullable = artax.RetainedTaxAmt;
    Decimal valueOrDefault6 = nullable.GetValueOrDefault();
    Decimal num7 = valueOrDefault5 * num2;
    Decimal num8 = valueOrDefault6 * num2;
    APReleaseProcess.LineBalances lineBalances = num1 != 0 ? new APReleaseProcess.LineBalances(new ARReleaseProcess.Amount(new Decimal?(0M), new Decimal?(0M)), new ARReleaseProcess.Amount(new Decimal?(num7), new Decimal?(num8)), new ARReleaseProcess.Amount(new Decimal?(num5), new Decimal?(num6))) : new APReleaseProcess.LineBalances(new Decimal?(0M));
    ARTran arTran1 = tran;
    nullable = arTran1.CuryRetainedTaxableAmt;
    Decimal valueOrDefault7 = flag ? artax.CuryRetainedTaxableAmt.GetValueOrDefault() : 0M;
    arTran1.CuryRetainedTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault7) : new Decimal?();
    ARTran arTran2 = tran;
    nullable = arTran2.RetainedTaxableAmt;
    Decimal valueOrDefault8 = flag ? artax.RetainedTaxableAmt.GetValueOrDefault() : 0M;
    arTran2.RetainedTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault8) : new Decimal?();
    ARTran arTran3 = tran;
    nullable = arTran3.CuryRetainedTaxAmt;
    Decimal num9 = flag ? num7 : 0M;
    arTran3.CuryRetainedTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num9) : new Decimal?();
    ARTran arTran4 = tran;
    nullable = arTran4.RetainedTaxAmt;
    Decimal num10 = flag ? num8 : 0M;
    arTran4.RetainedTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num10) : new Decimal?();
    ARTran arTran5 = tran;
    nullable = arTran5.CuryOrigRetainageAmt;
    Decimal? cury1 = lineBalances.RetainageBalance.Cury;
    arTran5.CuryOrigRetainageAmt = nullable.HasValue & cury1.HasValue ? new Decimal?(nullable.GetValueOrDefault() + cury1.GetValueOrDefault()) : new Decimal?();
    ARTran arTran6 = tran;
    Decimal? origRetainageAmt = arTran6.OrigRetainageAmt;
    nullable = lineBalances.RetainageBalance.Base;
    arTran6.OrigRetainageAmt = origRetainageAmt.HasValue & nullable.HasValue ? new Decimal?(origRetainageAmt.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
    ARTran arTran7 = tran;
    nullable = arTran7.CuryOrigTaxableAmt;
    Decimal valueOrDefault9 = flag ? artax.CuryTaxableAmt.GetValueOrDefault() : 0M;
    arTran7.CuryOrigTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault9) : new Decimal?();
    ARTran arTran8 = tran;
    nullable = arTran8.OrigTaxableAmt;
    Decimal valueOrDefault10 = flag ? artax.TaxableAmt.GetValueOrDefault() : 0M;
    arTran8.OrigTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault10) : new Decimal?();
    ARTran arTran9 = tran;
    nullable = arTran9.CuryOrigTaxAmt;
    Decimal num11 = flag ? num5 : 0M;
    arTran9.CuryOrigTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num11) : new Decimal?();
    ARTran arTran10 = tran;
    nullable = arTran10.OrigTaxAmt;
    Decimal num12 = flag ? num6 : 0M;
    arTran10.OrigTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num12) : new Decimal?();
    ARTran arTran11 = tran;
    nullable = arTran11.CuryOrigTranAmt;
    Decimal? cury2 = lineBalances.TranBalance.Cury;
    arTran11.CuryOrigTranAmt = nullable.HasValue & cury2.HasValue ? new Decimal?(nullable.GetValueOrDefault() + cury2.GetValueOrDefault()) : new Decimal?();
    ARTran arTran12 = tran;
    Decimal? origTranAmt = arTran12.OrigTranAmt;
    nullable = lineBalances.TranBalance.Base;
    arTran12.OrigTranAmt = origTranAmt.HasValue & nullable.HasValue ? new Decimal?(origTranAmt.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
    return lineBalances;
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2024R2.")]
  protected virtual void PostGeneralTax(
    JournalEntry je,
    ARInvoice ardoc,
    ARRegister doc,
    PX.Objects.TX.Tax salestax,
    ARTaxTran x,
    PX.Objects.GL.Account taxAccount,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    bool isDebit)
  {
    this.PostGeneralTax(je, ardoc, doc, salestax, x, taxAccount, new_info, isDebit, (Customer) null);
  }

  protected virtual void PostGeneralTax(
    JournalEntry je,
    ARInvoice ardoc,
    ARRegister doc,
    PX.Objects.TX.Tax salestax,
    ARTaxTran x,
    PX.Objects.GL.Account taxAccount,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    bool isDebit,
    Customer customer)
  {
    PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran();
    glTran.SummPost = new bool?(this.SummPost);
    glTran.BranchID = x.BranchID;
    glTran.CuryInfoID = new_info.CuryInfoID;
    glTran.TranType = x.TranType;
    glTran.TranClass = "T";
    glTran.RefNbr = x.RefNbr;
    glTran.TranDate = x.TranDate;
    glTran.AccountID = x.AccountID;
    glTran.SubID = x.SubID;
    glTran.TranDesc = x.TaxID;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran, doc.TranPeriodID);
    glTran.CuryDebitAmt = isDebit ? x.CuryTaxAmt : new Decimal?(0M);
    glTran.DebitAmt = isDebit ? x.TaxAmt : new Decimal?(0M);
    glTran.CuryCreditAmt = isDebit ? new Decimal?(0M) : x.CuryTaxAmt;
    glTran.CreditAmt = isDebit ? new Decimal?(0M) : x.TaxAmt;
    glTran.Released = new bool?(true);
    glTran.ReferenceID = ardoc.CustomerID;
    this.SetProjectAndTaxID(glTran, taxAccount, ardoc);
    this.InsertInvoiceTaxTransaction(je, glTran, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = doc,
      ARTaxTranRecord = x
    });
    this.PostRetainedTax(je, ardoc, glTran, x, salestax);
  }

  public void SetProjectAndTaxID(PX.Objects.GL.GLTran tran, PX.Objects.GL.Account account, ARInvoice ardoc)
  {
    int? nullable1;
    int num;
    if (account == null)
    {
      num = 1;
    }
    else
    {
      nullable1 = account.AccountGroupID;
      num = !nullable1.HasValue ? 1 : 0;
    }
    if (num == 0)
    {
      nullable1 = ardoc.ProjectID;
      if (nullable1.HasValue && !ProjectDefaultAttribute.IsNonProject(ardoc.ProjectID))
      {
        PMAccountTask pmAccountTask = PXResultset<PMAccountTask>.op_Implicit(PXSelectBase<PMAccountTask, PXSelect<PMAccountTask, Where<PMAccountTask.projectID, Equal<Required<PMAccountTask.projectID>>, And<PMAccountTask.accountID, Equal<Required<PMAccountTask.accountID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) ardoc.ProjectID,
          (object) account.AccountID
        }));
        if (pmAccountTask == null)
          throw new PXException("Tax account {0} is included in an account group but not mapped to a default task. Use the Account Task Mapping tab on the Projects (PM301000) form to associate the account with a project task and then try releasing the document again.", new object[1]
          {
            (object) account.AccountCD
          });
        tran.ProjectID = ardoc.ProjectID;
        tran.TaskID = pmAccountTask.TaskID;
        return;
      }
    }
    tran.ProjectID = ProjectDefaultAttribute.NonProject();
    PX.Objects.GL.GLTran glTran = tran;
    nullable1 = new int?();
    int? nullable2 = nullable1;
    glTran.TaskID = nullable2;
  }

  private void ProcessInvoiceRounding(
    JournalEntry je,
    PX.Objects.GL.Batch arbatch,
    ARInvoice ardoc,
    Decimal curyCreditDiff,
    Decimal creditDiff,
    PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    PX.Objects.CM.Extensions.Currency currency = PXResultset<PX.Objects.CM.Extensions.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.Currency, PXSelect<PX.Objects.CM.Extensions.Currency, Where<PX.Objects.CM.Extensions.Currency.curyID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ardoc.CuryID
    }));
    PX.Objects.CM.Extensions.CurrencyInfo ex = PX.Objects.CM.Extensions.CurrencyInfo.GetEX(this.GetCurrencyInfoCopyForGL(je, info));
    if (!currency.RoundingGainAcctID.HasValue || !currency.RoundingGainSubID.HasValue)
      throw new PXException("Rounding gain or loss account or subaccount is not specified for {0} currency.", new object[1]
      {
        (object) currency.CuryID
      });
    if (curyCreditDiff != 0M)
    {
      PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran();
      glTran.SummPost = new bool?(true);
      glTran.BranchID = ardoc.BranchID;
      if (Math.Sign(curyCreditDiff) == 1)
      {
        glTran.AccountID = currency.RoundingGainAcctID;
        glTran.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingGainSubID>((PXGraph) je, glTran.BranchID, currency);
        glTran.CuryCreditAmt = new Decimal?(Math.Abs(curyCreditDiff));
        glTran.CuryDebitAmt = new Decimal?(0M);
      }
      else
      {
        glTran.AccountID = currency.RoundingLossAcctID;
        glTran.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingLossSubID>((PXGraph) je, glTran.BranchID, currency);
        glTran.CuryCreditAmt = new Decimal?(0M);
        glTran.CuryDebitAmt = new Decimal?(Math.Abs(curyCreditDiff));
      }
      glTran.CreditAmt = new Decimal?(0M);
      glTran.DebitAmt = new Decimal?(0M);
      glTran.TranType = ardoc.DocType;
      glTran.RefNbr = ardoc.RefNbr;
      glTran.TranClass = "N";
      glTran.TranDesc = "Rounding difference";
      glTran.LedgerID = arbatch.LedgerID;
      FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran, arbatch.TranPeriodID);
      glTran.TranDate = ardoc.DocDate;
      glTran.ReferenceID = ardoc.CustomerID;
      glTran.Released = new bool?(true);
      glTran.CuryInfoID = ex.CuryInfoID;
      this.InsertInvoiceRoundingTransaction(je, glTran, new ARReleaseProcess.GLTranInsertionContext()
      {
        ARRegisterRecord = (ARRegister) ardoc
      });
    }
    if (!(creditDiff != 0M))
      return;
    PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran()
    {
      SummPost = new bool?(true),
      BranchID = ardoc.BranchID
    };
    if (Math.Sign(creditDiff) == 1)
    {
      glTran1.AccountID = currency.RoundingGainAcctID;
      glTran1.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingGainSubID>((PXGraph) je, glTran1.BranchID, currency);
      glTran1.CreditAmt = new Decimal?(Math.Abs(creditDiff));
      glTran1.DebitAmt = new Decimal?(0M);
    }
    else
    {
      glTran1.AccountID = currency.RoundingLossAcctID;
      glTran1.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingLossSubID>((PXGraph) je, glTran1.BranchID, currency);
      glTran1.CreditAmt = new Decimal?(0M);
      glTran1.DebitAmt = new Decimal?(Math.Abs(creditDiff));
    }
    glTran1.CuryCreditAmt = new Decimal?(0M);
    glTran1.CuryDebitAmt = new Decimal?(0M);
    glTran1.TranType = ardoc.DocType;
    glTran1.RefNbr = ardoc.RefNbr;
    glTran1.TranClass = "N";
    glTran1.TranDesc = "Rounding difference";
    glTran1.LedgerID = arbatch.LedgerID;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran1, arbatch.TranPeriodID);
    glTran1.TranDate = ardoc.DocDate;
    glTran1.ReferenceID = ardoc.CustomerID;
    glTran1.Released = new bool?(true);
    glTran1.CuryInfoID = ex.CuryInfoID;
    this.InsertInvoiceRoundingTransaction(je, glTran1, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = (ARRegister) ardoc
    });
  }

  protected virtual void ProcessTaxDiscrepancy(
    JournalEntry je,
    PX.Objects.GL.Batch arbatch,
    ARInvoice ardoc,
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo,
    Decimal docInclTaxDiscrepancy)
  {
    if (docInclTaxDiscrepancy == 0M)
      return;
    Decimal num1 = Math.Abs(docInclTaxDiscrepancy);
    Decimal? roundingLimit = CurrencyCollection.GetCurrency(currencyInfo.BaseCuryID).RoundingLimit;
    Decimal valueOrDefault = roundingLimit.GetValueOrDefault();
    if (num1 > valueOrDefault & roundingLimit.HasValue && (PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() || PXAccess.FeatureInstalled<FeaturesSet.invoiceRounding>()))
      throw new PXException("The amount to be posted to the rounding account ({1} {0}) exceeds the limit ({2} {0}) specified on the Currencies (CM202000) form.", new object[3]
      {
        (object) ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Current.BaseCuryID,
        (object) Math.Abs(Math.Round(docInclTaxDiscrepancy, (int) (currencyInfo.CuryPrecision ?? (short) 4))),
        (object) PXDBQuantityAttribute.Round(CurrencyCollection.GetCurrency(currencyInfo.BaseCuryID).RoundingLimit)
      });
    TXSetup txSetup = PXResultset<TXSetup>.op_Implicit(PXSetup<TXSetup>.Select((PXGraph) this, Array.Empty<object>()));
    int? nullable1;
    int num2;
    if (txSetup == null)
    {
      num2 = 1;
    }
    else
    {
      nullable1 = txSetup.TaxRoundingGainAcctID;
      num2 = !nullable1.HasValue ? 1 : 0;
    }
    if (num2 == 0)
    {
      int num3;
      if (txSetup == null)
      {
        num3 = 1;
      }
      else
      {
        nullable1 = txSetup.TaxRoundingLossAcctID;
        num3 = !nullable1.HasValue ? 1 : 0;
      }
      if (num3 == 0)
      {
        int? nullable2 = docInclTaxDiscrepancy > 0M ? txSetup.TaxRoundingGainAcctID : txSetup.TaxRoundingLossAcctID;
        int? nullable3 = docInclTaxDiscrepancy > 0M ? txSetup.TaxRoundingGainSubID : txSetup.TaxRoundingLossSubID;
        bool flag = ardoc.DrCr == "D";
        PX.Objects.GL.GLTran tran = new PX.Objects.GL.GLTran()
        {
          SummPost = new bool?(this.SummPost),
          BranchID = ardoc.BranchID,
          CuryInfoID = currencyInfo.CuryInfoID,
          TranType = ardoc.DocType,
          TranClass = "R",
          RefNbr = ardoc.RefNbr,
          TranDate = ardoc.DocDate,
          AccountID = nullable2,
          SubID = nullable3,
          TranDesc = "Tax rounding difference",
          CuryDebitAmt = new Decimal?(flag ? docInclTaxDiscrepancy : 0M),
          DebitAmt = new Decimal?(flag ? currencyInfo.CuryConvBase(docInclTaxDiscrepancy) : 0M),
          CuryCreditAmt = new Decimal?(flag ? 0M : docInclTaxDiscrepancy),
          CreditAmt = new Decimal?(flag ? 0M : currencyInfo.CuryConvBase(docInclTaxDiscrepancy)),
          Released = new bool?(true),
          ReferenceID = ardoc.CustomerID
        };
        this.InsertInvoiceTransaction(je, tran, new ARReleaseProcess.GLTranInsertionContext()
        {
          ARRegisterRecord = (ARRegister) ardoc
        });
        return;
      }
    }
    throw new PXException("Tax rounding gain and loss accounts cannot be empty. Specify these accounts on the Tax Preferences (TX103000) form.");
  }

  public virtual void CreateExpenseAccrualTransactions(
    JournalEntry je,
    ARRegister doc,
    ARTran n,
    PX.Objects.GL.GLTran origTran)
  {
    PX.Objects.GL.GLTran copy1 = (PX.Objects.GL.GLTran) ((PXSelectBase) je.GLTranModuleBatNbr).Cache.CreateCopy((object) origTran);
    PX.Objects.GL.GLTran glTran1 = copy1;
    Decimal? qty;
    Decimal? nullable1;
    if (!(n.DrCr == "D"))
    {
      nullable1 = n.Qty;
    }
    else
    {
      Decimal num = (Decimal) -1;
      qty = n.Qty;
      nullable1 = qty.HasValue ? new Decimal?(num * qty.GetValueOrDefault()) : new Decimal?();
    }
    glTran1.Qty = nullable1;
    copy1.AccountID = n.ExpenseAccrualAccountID;
    copy1.SubID = this.GetValueInt<ARTran.expenseAccrualSubID>((PXGraph) je, (object) n);
    copy1.CuryDebitAmt = n.DrCr == "D" ? n.CuryAccruedCost : new Decimal?(0M);
    copy1.DebitAmt = n.DrCr == "D" ? n.AccruedCost : new Decimal?(0M);
    copy1.CuryCreditAmt = n.DrCr == "D" ? new Decimal?(0M) : n.CuryAccruedCost;
    copy1.CreditAmt = n.DrCr == "D" ? new Decimal?(0M) : n.AccruedCost;
    copy1.ProjectID = ProjectDefaultAttribute.NonProject();
    copy1.TaskID = new int?();
    copy1.CostCodeID = new int?();
    this.InsertInvoiceDetailsTransaction(je, copy1, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = doc,
      ARTranRecord = n
    });
    PX.Objects.GL.GLTran copy2 = (PX.Objects.GL.GLTran) ((PXSelectBase) je.GLTranModuleBatNbr).Cache.CreateCopy((object) origTran);
    PX.Objects.GL.GLTran glTran2 = copy2;
    Decimal? nullable2;
    if (!(n.DrCr == "D"))
    {
      Decimal num = (Decimal) -1;
      qty = n.Qty;
      nullable2 = qty.HasValue ? new Decimal?(num * qty.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable2 = n.Qty;
    glTran2.Qty = nullable2;
    copy2.AccountID = n.ExpenseAccountID;
    copy2.SubID = this.GetValueInt<ARTran.expenseSubID>((PXGraph) je, (object) n);
    copy2.CuryDebitAmt = n.DrCr == "D" ? new Decimal?(0M) : n.CuryAccruedCost;
    copy2.DebitAmt = n.DrCr == "D" ? new Decimal?(0M) : n.AccruedCost;
    copy2.CuryCreditAmt = n.DrCr == "D" ? n.CuryAccruedCost : new Decimal?(0M);
    copy2.CreditAmt = n.DrCr == "D" ? n.AccruedCost : new Decimal?(0M);
    this.InsertInvoiceDetailsTransaction(je, copy2, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = doc,
      ARTranRecord = n
    });
    ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Current = origTran;
  }

  public virtual void ProcessOriginTranPost(
    ARInvoice doc,
    ARReleaseProcess.Amount itemDiscount,
    bool masterInstallment)
  {
    ARTranPost arTranPost1 = this.CreateTranPost((ARRegister) doc);
    arTranPost1.Type = "S";
    arTranPost1.CuryAmt = doc.CuryOrigDocAmt;
    arTranPost1.Amt = doc.OrigDocAmt;
    arTranPost1.CuryRetainageAmt = doc.CuryRetainageTotal;
    arTranPost1.RetainageAmt = doc.RetainageTotal;
    if (this.IsNeedUpdateHistoryForTransaction(arTranPost1.TranPeriodID))
      arTranPost1 = ((PXSelectBase<ARTranPost>) this.TranPost).Insert(arTranPost1);
    arTranPost1.CuryItemDiscAmt = itemDiscount.Cury;
    arTranPost1.ItemDiscAmt = itemDiscount.Base;
    arTranPost1.TranRefNbr = doc.MasterRefNbr ?? arTranPost1.TranRefNbr;
    if (masterInstallment)
    {
      arTranPost1.AccountID = new int?();
      arTranPost1.SubID = new int?();
      this.ProcessInstallmentTranPost(doc);
    }
    if (doc.IsPrepaymentInvoiceDocument())
      arTranPost1.TranClass = "PPM" + "P";
    if (doc.IsRetainageReversing.GetValueOrDefault())
    {
      ARTranPost tranPost = this.CreateTranPost((ARRegister) doc);
      tranPost.SourceDocType = doc.OrigDocType;
      tranPost.SourceRefNbr = doc.OrigRefNbr;
      tranPost.Type = "U";
      tranPost.CuryRetainageAmt = doc.CuryRetainageTotal;
      tranPost.RetainageAmt = doc.RetainageTotal;
      ((PXSelectBase<ARTranPost>) this.TranPost).Insert(tranPost);
      tranPost.DocType = doc.OrigDocType;
      tranPost.RefNbr = doc.OrigRefNbr;
      tranPost.SourceDocType = doc.DocType;
      tranPost.SourceRefNbr = doc.RefNbr;
      ((PXSelectBase<ARTranPost>) this.TranPost).Insert(tranPost);
    }
    if (!doc.IsPrepaymentInvoiceDocumentReverse())
      return;
    ARTranPost copy = (ARTranPost) ((PXSelectBase) this.TranPost).Cache.CreateCopy((object) arTranPost1);
    copy.ID = new int?();
    copy.AccountID = doc.PrepaymentAccountID;
    copy.SubID = doc.PrepaymentSubID;
    ARTranPost arTranPost2 = copy;
    short? glSign = arTranPost1.GLSign;
    short? nullable = glSign.HasValue ? new short?(-glSign.GetValueOrDefault()) : new short?();
    arTranPost2.GLSign = nullable;
    ((PXSelectBase<ARTranPost>) this.TranPost).Insert(copy);
  }

  public virtual void ProcessOriginTranPost(ARPayment doc)
  {
    if (doc.DocType == "CSL" || doc.DocType == "RCS")
      return;
    ARTranPost tranPost = this.CreateTranPost((ARRegister) doc);
    tranPost.Type = "S";
    tranPost.CuryAmt = doc.CuryOrigDocAmt;
    tranPost.Amt = doc.OrigDocAmt;
    if (!this.IsNeedUpdateHistoryForTransaction(tranPost.TranPeriodID))
      return;
    ((PXSelectBase<ARTranPost>) this.TranPost).Insert(tranPost);
  }

  public virtual void ProcessVoidWOTranPost(ARRegister doc, ARAdjust lastAdjustment)
  {
    ARTranPost tranPost = this.CreateTranPost(doc);
    tranPost.FinPeriodID = lastAdjustment.AdjgFinPeriodID;
    tranPost.TranPeriodID = lastAdjustment.AdjgTranPeriodID;
    tranPost.BatchNbr = lastAdjustment.AdjBatchNbr;
    tranPost.DocDate = lastAdjustment.AdjgDocDate;
    tranPost.Type = "V";
    ARTranPost arTranPost1 = tranPost;
    Decimal? curyOrigDocAmt = doc.CuryOrigDocAmt;
    Decimal? nullable1 = curyOrigDocAmt.HasValue ? new Decimal?(-curyOrigDocAmt.GetValueOrDefault()) : new Decimal?();
    arTranPost1.CuryAmt = nullable1;
    ARTranPost arTranPost2 = tranPost;
    Decimal? origDocAmt = doc.OrigDocAmt;
    Decimal? nullable2 = origDocAmt.HasValue ? new Decimal?(-origDocAmt.GetValueOrDefault()) : new Decimal?();
    arTranPost2.Amt = nullable2;
    if (!this.IsNeedUpdateHistoryForTransaction(tranPost.TranPeriodID))
      return;
    ((PXSelectBase<ARTranPost>) this.TranPost).Insert(tranPost);
  }

  public virtual void ProcessVoidPaymentTranPost(ARRegister doc, ARReleaseProcess.Amount docBal)
  {
    ARTranPost tranPost1 = this.CreateTranPost(doc);
    ARTranPost tranPost2 = this.CreateTranPost(doc);
    tranPost1.DocType = doc.OrigDocType ?? this.GetHistTranType(doc.DocType, doc.RefNbr);
    tranPost1.RefNbr = doc.OrigRefNbr ?? doc.RefNbr;
    tranPost2.SourceDocType = tranPost1.DocType;
    tranPost2.SourceRefNbr = tranPost1.RefNbr;
    tranPost1.Type = tranPost2.Type = "V";
    ARTranPost arTranPost1 = tranPost1;
    Decimal? cury = docBal.Cury;
    Decimal? nullable1 = cury.HasValue ? new Decimal?(-cury.GetValueOrDefault()) : new Decimal?();
    arTranPost1.CuryAmt = nullable1;
    ARTranPost arTranPost2 = tranPost1;
    cury = docBal.Base;
    Decimal? nullable2 = cury.HasValue ? new Decimal?(-cury.GetValueOrDefault()) : new Decimal?();
    arTranPost2.Amt = nullable2;
    tranPost2.CuryAmt = docBal.Cury;
    tranPost2.Amt = docBal.Base;
    if (tranPost1.DocType != null && this.IsNeedUpdateHistoryForTransaction(tranPost1.TranPeriodID))
      ((PXSelectBase<ARTranPost>) this.TranPost).Insert(tranPost1);
    if (tranPost2.DocType == null || !this.IsNeedUpdateHistoryForTransaction(tranPost1.TranPeriodID))
      return;
    ((PXSelectBase<ARTranPost>) this.TranPost).Insert(tranPost2);
  }

  public virtual void ProcessPrepaymentTranPost(ARInvoice doc)
  {
    ARTranPost tranPost = this.CreateTranPost((ARRegister) doc);
    tranPost.TranClass = doc.DocType + "P";
    tranPost.Type = "S";
    tranPost.AccountID = doc.PrepaymentAccountID;
    tranPost.SubID = doc.PrepaymentSubID;
    tranPost.CuryAmt = doc.CuryOrigDocAmt;
    tranPost.Amt = doc.OrigDocAmt;
    tranPost.GLSign = new short?((short) -1);
    if (!this.IsNeedUpdateHistoryForTransaction(tranPost.TranPeriodID))
      return;
    ((PXSelectBase<ARTranPost>) this.TranPost).Insert(tranPost);
  }

  public virtual void ProcessRetainageTranPost(ARInvoice doc)
  {
    if (doc.PaymentsByLinesAllowed.GetValueOrDefault())
    {
      Dictionary<ARReleaseProcess.ARTranPostKey, ARTranPost> dictionary = new Dictionary<ARReleaseProcess.ARTranPostKey, ARTranPost>();
      IEqualityComparer<ARTran> comparer = (IEqualityComparer<ARTran>) new FieldSubsetEqualityComparer<ARTran>(((PXSelectBase) this.ARTran_TranType_RefNbr).Cache, new System.Type[3]
      {
        typeof (ARTran.tranType),
        typeof (ARTran.refNbr),
        typeof (ARTran.lineNbr)
      });
      foreach (IGrouping<ARTran, PXResult<ARTran>> grouping in ((IEnumerable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) this.ARTran_TranType_RefNbr).Select(new object[2]
      {
        (object) doc.DocType,
        (object) doc.RefNbr
      })).AsEnumerable<PXResult<ARTran>>().GroupBy<PXResult<ARTran>, ARTran>((Func<PXResult<ARTran>, ARTran>) (row => PXResult<ARTran>.op_Implicit(row)), comparer))
      {
        ARTran key1 = grouping.Key;
        ARReleaseProcess.ARTranPostKey key2 = new ARReleaseProcess.ARTranPostKey(key1.OrigDocType, key1.OrigRefNbr, 0);
        ARTranPost arTranPost1;
        dictionary.TryGetValue(key2, out arTranPost1);
        Decimal? nullable1;
        Decimal? nullable2;
        if (arTranPost1 != null)
        {
          ARTranPost arTranPost2 = arTranPost1;
          nullable1 = arTranPost2.CuryRetainageAmt;
          nullable2 = key1.CuryOrigTranAmt;
          arTranPost2.CuryRetainageAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          ARTranPost arTranPost3 = arTranPost1;
          nullable2 = arTranPost3.RetainageAmt;
          nullable1 = key1.OrigTranAmt;
          arTranPost3.RetainageAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
        }
        else
        {
          ARTranPost tranPost = this.CreateTranPost((ARRegister) doc);
          tranPost.DocType = key2.DocType;
          tranPost.RefNbr = key2.RefNbr;
          tranPost.Type = "F";
          ARTranPost arTranPost4 = tranPost;
          nullable1 = key1.CuryOrigTranAmt;
          Decimal? nullable3;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable3 = nullable2;
          }
          else
            nullable3 = new Decimal?(-nullable1.GetValueOrDefault());
          arTranPost4.CuryRetainageAmt = nullable3;
          ARTranPost arTranPost5 = tranPost;
          nullable1 = key1.OrigTranAmt;
          Decimal? nullable4;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable4 = nullable2;
          }
          else
            nullable4 = new Decimal?(-nullable1.GetValueOrDefault());
          arTranPost5.RetainageAmt = nullable4;
          dictionary.Add(key2, tranPost);
        }
      }
      foreach (KeyValuePair<ARReleaseProcess.ARTranPostKey, ARTranPost> keyValuePair in dictionary)
      {
        if (this.IsNeedUpdateHistoryForTransaction(keyValuePair.Value.TranPeriodID))
          ((PXSelectBase<ARTranPost>) this.TranPost).Insert(keyValuePair.Value);
      }
    }
    else
    {
      ARTran arTran = PXResult<ARTran>.op_Implicit(((IQueryable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) this.ARTran_TranType_RefNbr).Select(new object[2]
      {
        (object) doc.DocType,
        (object) doc.RefNbr
      })).FirstOrDefault<PXResult<ARTran>>());
      ARTranPost tranPost = this.CreateTranPost((ARRegister) doc);
      tranPost.DocType = arTran.OrigDocType;
      tranPost.RefNbr = arTran.OrigRefNbr;
      tranPost.Type = "F";
      ARTranPost arTranPost6 = tranPost;
      Decimal? curyOrigDocAmt = doc.CuryOrigDocAmt;
      Decimal? nullable5 = curyOrigDocAmt.HasValue ? new Decimal?(-curyOrigDocAmt.GetValueOrDefault()) : new Decimal?();
      arTranPost6.CuryRetainageAmt = nullable5;
      ARTranPost arTranPost7 = tranPost;
      Decimal? origDocAmt = doc.OrigDocAmt;
      Decimal? nullable6 = origDocAmt.HasValue ? new Decimal?(-origDocAmt.GetValueOrDefault()) : new Decimal?();
      arTranPost7.RetainageAmt = nullable6;
      if (!this.IsNeedUpdateHistoryForTransaction(tranPost.TranPeriodID))
        return;
      ((PXSelectBase<ARTranPost>) this.TranPost).Insert(tranPost);
    }
  }

  public virtual void ProcessInstallmentTranPost(ARInvoice doc)
  {
    ARTranPost tranPost = this.CreateTranPost((ARRegister) doc);
    tranPost.Type = "I";
    ARTranPost arTranPost1 = tranPost;
    Decimal? curyOrigDocAmt = doc.CuryOrigDocAmt;
    Decimal? nullable1 = curyOrigDocAmt.HasValue ? new Decimal?(-curyOrigDocAmt.GetValueOrDefault()) : new Decimal?();
    arTranPost1.CuryAmt = nullable1;
    ARTranPost arTranPost2 = tranPost;
    Decimal? origDocAmt = doc.OrigDocAmt;
    Decimal? nullable2 = origDocAmt.HasValue ? new Decimal?(-origDocAmt.GetValueOrDefault()) : new Decimal?();
    arTranPost2.Amt = nullable2;
    if (!this.IsNeedUpdateHistoryForTransaction(tranPost.TranPeriodID))
      return;
    ((PXSelectBase<ARTranPost>) this.TranPost).Insert(tranPost);
  }

  public virtual void ProcessAdjustmentTranPost(
    ARAdjust adj,
    ARRegister doc,
    ARRegister pmt,
    bool adjustedOnly)
  {
    this.ProcessAdjustmentTranPost((ARTran) null, adj, doc, pmt, adjustedOnly);
  }

  public virtual void ProcessAdjustmentTranPost(
    ARTran tran,
    ARAdjust adj,
    ARRegister doc,
    ARRegister pmt,
    bool adjustedOnly = false)
  {
    ARTranPost row1 = new ARTranPost();
    ARTranPost row2 = new ARTranPost();
    row1.Type = "D";
    row2.Type = "G";
    row1.AdjNbr = row2.AdjNbr = adj.AdjNbr;
    row1.RefNoteID = row2.RefNoteID = adj.NoteID;
    row1.DocType = row2.SourceDocType = adj.AdjdDocType;
    row1.RefNbr = row2.SourceRefNbr = adj.AdjdRefNbr;
    row1.ReferenceID = row2.ReferenceID = adj.CustomerID ?? adj.AdjdCustomerID;
    row1.IsMigratedRecord = row2.IsMigratedRecord = adj.IsMigratedRecord;
    row1.SourceDocType = row2.DocType = adj.AdjgDocType;
    row1.SourceRefNbr = row2.RefNbr = adj.AdjgRefNbr;
    row1.BatchNbr = row2.BatchNbr = adj.AdjBatchNbr;
    row1.LineNbr = row2.LineNbr = adj.AdjdLineNbr;
    row1.VoidAdjNbr = row2.VoidAdjNbr = adj.VoidAdjNbr;
    row1.TranType = row1.DocType == "SMC" ? row1.DocType : row1.SourceDocType;
    row1.TranRefNbr = row1.DocType == "SMC" ? row1.RefNbr : row1.SourceRefNbr;
    row1.AccountID = adj.AdjdARAcct;
    row1.SubID = adj.AdjdARSub;
    row1.BranchID = adj.AdjdBranchID;
    row1.CustomerID = doc.CustomerID;
    FinPeriodIDAttribute.SetPeriodsByMaster<ARTranPost.finPeriodID>(((PXSelectBase) this.TranPost).Cache, (object) row1, adj.AdjgTranPeriodID);
    row1.DocDate = adj.AdjgDocDate;
    row1.CuryInfoID = adj.AdjdCuryInfoID;
    row1.CuryAmt = adj.CuryAdjdAmt;
    row1.CuryPPDAmt = EnumerableExtensions.IsNotIn<string>(adj.AdjdDocType, "CSL", "RCS") ? adj.CuryAdjdPPDAmt : new Decimal?(0M);
    row1.CuryDiscAmt = EnumerableExtensions.IsNotIn<string>(adj.AdjdDocType, "CSL", "RCS") ? adj.CuryAdjdDiscAmt : new Decimal?(0M);
    row1.CuryWOAmt = adj.CuryAdjdWOAmt;
    row1.Amt = adj.AdjAmt;
    row1.PPDAmt = EnumerableExtensions.IsNotIn<string>(adj.AdjdDocType, "CSL", "RCS") ? adj.AdjPPDAmt : new Decimal?(0M);
    row1.DiscAmt = EnumerableExtensions.IsNotIn<string>(adj.AdjdDocType, "CSL", "RCS") ? adj.AdjDiscAmt : new Decimal?(0M);
    row1.WOAmt = adj.AdjWOAmt;
    row1.RGOLAmt = adj.RGOLAmt;
    row2.TranType = adj.IsOrigSmallCreditWOApp() ? adj.AdjdDocType : adj.AdjgDocType;
    row2.TranRefNbr = adj.IsOrigSmallCreditWOApp() ? adj.AdjdRefNbr : adj.AdjgRefNbr;
    row2.AccountID = pmt.IsPrepaymentInvoiceDocument() ? pmt.PrepaymentAccountID : pmt.ARAccountID;
    row2.SubID = pmt.IsPrepaymentInvoiceDocument() ? pmt.PrepaymentSubID : pmt.ARSubID;
    row2.BranchID = adj.AdjgBranchID;
    row2.CustomerID = pmt.CustomerID;
    FinPeriodIDAttribute.SetPeriodsByMaster<ARTranPost.finPeriodID>(((PXSelectBase) this.TranPost).Cache, (object) row2, adj.AdjgTranPeriodID);
    row2.DocDate = adj.AdjgDocDate;
    row2.CuryInfoID = adj.AdjgCuryInfoID;
    row2.CuryAmt = adj.CuryAdjgAmt;
    row2.CuryPPDAmt = adj.CuryAdjgPPDAmt;
    row2.CuryDiscAmt = adj.CuryAdjgDiscAmt;
    row2.CuryWOAmt = adj.CuryAdjgWOAmt;
    row2.Amt = adj.AdjAmt;
    row2.PPDAmt = adj.AdjPPDAmt;
    row2.DiscAmt = adj.AdjDiscAmt;
    row2.WOAmt = adj.AdjWOAmt;
    row2.RGOLAmt = adj.RGOLAmt;
    bool? nullable1 = doc.IsMigratedRecord;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = pmt.IsMigratedRecord;
      if (nullable1.GetValueOrDefault())
      {
        row1.TranType = "CRM";
        row1.IsMigratedRecord = new bool?(true);
      }
    }
    ARTranPost arTranPost1 = row1;
    ARTranPost arTranPost2 = row2;
    nullable1 = new bool?(this.GetHistTranType(row2.TranType, row2.TranRefNbr) == "PPM");
    bool? nullable2 = nullable1;
    arTranPost2.IsVoidPrepayment = nullable2;
    bool? nullable3 = nullable1;
    arTranPost1.IsVoidPrepayment = nullable3;
    if (pmt.IsPrepaymentInvoiceDocumentReverse())
      row1.TranClass = row2.DocType + "Y";
    else if (pmt.DocType == "VRF" && doc.DocType == "PPI")
    {
      row1.GLSign = new short?((short) -1);
      row2.GLSign = new short?((short) -1);
    }
    else if (doc.IsPrepaymentInvoiceDocument())
      row1.TranClass = "PPM" + "P";
    int? baccountId = (int?) this.CustomerIntegrityCheck?.BAccountID;
    int? nullable4;
    if (baccountId.HasValue)
    {
      nullable4 = row1.CustomerID;
      int? nullable5 = baccountId;
      if (!(nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue))
        goto label_14;
    }
    if (this.IsNeedUpdateHistoryForTransaction(row1.TranPeriodID))
    {
      ((PXSelectBase<ARTranPost>) this.TranPost).Insert(row1);
      if (pmt.IsPrepaymentInvoiceDocumentReverse())
      {
        ARTranPost copy1 = (ARTranPost) ((PXSelectBase) this.TranPost).Cache.CreateCopy((object) row1);
        copy1.ID = new int?();
        copy1.AccountID = pmt.PrepaymentAccountID;
        copy1.SubID = pmt.PrepaymentSubID;
        copy1.GLSign = new short?((short) -1);
        ((PXSelectBase<ARTranPost>) this.TranPost).Insert(copy1);
        ARTranPost copy2 = (ARTranPost) ((PXSelectBase) this.TranPost).Cache.CreateCopy((object) row2);
        copy2.ID = new int?();
        copy2.AccountID = pmt.PrepaymentAccountID;
        copy2.SubID = pmt.PrepaymentSubID;
        copy2.GLSign = new short?((short) -1);
        ((PXSelectBase<ARTranPost>) this.TranPost).Insert(copy2);
      }
    }
label_14:
    if (!adjustedOnly)
    {
      if (baccountId.HasValue)
      {
        int? customerId = row2.CustomerID;
        nullable4 = baccountId;
        if (!(customerId.GetValueOrDefault() == nullable4.GetValueOrDefault() & customerId.HasValue == nullable4.HasValue))
          goto label_22;
      }
      if (this.IsNeedUpdateHistoryForTransaction(row2.TranPeriodID))
      {
        if (!EnumerableExtensions.IsIn<string>(row1.DocType, "CSL", "RCS"))
          ((PXSelectBase<ARTranPost>) this.TranPost).Insert(row2);
        ARTranPost copy3 = (ARTranPost) ((PXSelectBase) this.TranPost).Cache.CreateCopy((object) row2);
        copy3.BranchID = adj.IsOrigSmallCreditWOApp() ? pmt.BranchID : doc.BranchID;
        FinPeriodIDAttribute.SetPeriodsByMaster<ARTranPost.finPeriodID>(((PXSelectBase) this.TranPost).Cache, (object) copy3, copy3.TranPeriodID);
        copy3.Type = "R";
        copy3.CuryInfoID = adj.AdjdCuryInfoID;
        copy3.CuryAmt = new Decimal?(0M);
        copy3.Amt = new Decimal?(0M);
        copy3.CuryPPDAmt = adj.CuryAdjdPPDAmt;
        copy3.CuryDiscAmt = adj.CuryAdjdDiscAmt;
        copy3.PPDAmt = adj.AdjPPDAmt;
        copy3.DiscAmt = adj.AdjDiscAmt;
        copy3.CuryWOAmt = adj.CuryAdjdWOAmt;
        copy3.WOAmt = adj.AdjWOAmt;
        copy3.TranType = adj.AdjgDocType;
        copy3.TranRefNbr = adj.AdjgRefNbr;
        copy3.AccountID = doc.ARAccountID;
        copy3.SubID = doc.ARSubID;
        ((PXSelectBase<ARTranPost>) this.TranPost).Insert(copy3);
        if (adj.AdjdDocType == "SMC")
        {
          ARTranPost copy4 = (ARTranPost) ((PXSelectBase) this.TranPost).Cache.CreateCopy((object) copy3);
          copy4.AccountID = pmt.ARAccountID;
          copy4.SubID = pmt.ARSubID;
          copy4.Type = "X";
          ((PXSelectBase<ARTranPost>) this.TranPost).Insert(copy4);
        }
      }
    }
label_22:
    nullable1 = doc.IsRetainageDocument;
    if (!nullable1.GetValueOrDefault())
      return;
    ARRegister retainageDocument;
    if (tran != null && tran.OrigRefNbr != null)
    {
      nullable1 = doc.PaymentsByLinesAllowed;
      if (nullable1.GetValueOrDefault())
      {
        retainageDocument = this.GetOriginalRetainageDocument(tran);
        goto label_27;
      }
    }
    retainageDocument = this.GetOriginalRetainageDocument(doc);
label_27:
    if (retainageDocument == null)
      return;
    Decimal? nullable6 = retainageDocument.SignAmount;
    Decimal? signAmount = doc.SignAmount;
    Decimal? nullable7 = nullable6.HasValue & signAmount.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * signAmount.GetValueOrDefault()) : new Decimal?();
    Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
    nullable6 = nullable7;
    Decimal? nullable8 = curyAdjdAmt.HasValue & nullable6.HasValue ? new Decimal?(curyAdjdAmt.GetValueOrDefault() * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal valueOrDefault1 = nullable8.GetValueOrDefault();
    Decimal? nullable9 = adj.AdjAmt;
    nullable6 = nullable7;
    Decimal? nullable10;
    if (!(nullable9.HasValue & nullable6.HasValue))
    {
      nullable8 = new Decimal?();
      nullable10 = nullable8;
    }
    else
      nullable10 = new Decimal?(nullable9.GetValueOrDefault() * nullable6.GetValueOrDefault());
    nullable8 = nullable10;
    Decimal valueOrDefault2 = nullable8.GetValueOrDefault();
    nullable9 = adj.CuryAdjdDiscAmt;
    nullable6 = nullable7;
    Decimal? nullable11;
    if (!(nullable9.HasValue & nullable6.HasValue))
    {
      nullable8 = new Decimal?();
      nullable11 = nullable8;
    }
    else
      nullable11 = new Decimal?(nullable9.GetValueOrDefault() * nullable6.GetValueOrDefault());
    nullable8 = nullable11;
    Decimal valueOrDefault3 = nullable8.GetValueOrDefault();
    nullable9 = adj.AdjDiscAmt;
    nullable6 = nullable7;
    Decimal? nullable12;
    if (!(nullable9.HasValue & nullable6.HasValue))
    {
      nullable8 = new Decimal?();
      nullable12 = nullable8;
    }
    else
      nullable12 = new Decimal?(nullable9.GetValueOrDefault() * nullable6.GetValueOrDefault());
    nullable8 = nullable12;
    Decimal valueOrDefault4 = nullable8.GetValueOrDefault();
    nullable9 = adj.CuryAdjdWOAmt;
    nullable6 = nullable7;
    Decimal? nullable13;
    if (!(nullable9.HasValue & nullable6.HasValue))
    {
      nullable8 = new Decimal?();
      nullable13 = nullable8;
    }
    else
      nullable13 = new Decimal?(nullable9.GetValueOrDefault() * nullable6.GetValueOrDefault());
    nullable8 = nullable13;
    Decimal valueOrDefault5 = nullable8.GetValueOrDefault();
    nullable9 = adj.AdjWOAmt;
    nullable6 = nullable7;
    Decimal? nullable14;
    if (!(nullable9.HasValue & nullable6.HasValue))
    {
      nullable8 = new Decimal?();
      nullable14 = nullable8;
    }
    else
      nullable14 = new Decimal?(nullable9.GetValueOrDefault() * nullable6.GetValueOrDefault());
    nullable8 = nullable14;
    Decimal valueOrDefault6 = nullable8.GetValueOrDefault();
    if (tran != null && tran.OrigRefNbr != null)
    {
      nullable1 = doc.PaymentsByLinesAllowed;
      if (nullable1.GetValueOrDefault())
        goto label_47;
    }
    nullable1 = pmt.IsRetainageReversing;
    if (nullable1.GetValueOrDefault())
      return;
    nullable1 = doc.IsRetainageReversing;
    if (nullable1.GetValueOrDefault())
      return;
label_47:
    ARRegister arRegister1 = retainageDocument;
    nullable9 = arRegister1.CuryRetainageUnpaidTotal;
    Decimal num1 = valueOrDefault1 + valueOrDefault3 + valueOrDefault5;
    Decimal? nullable15;
    if (!nullable9.HasValue)
    {
      nullable6 = new Decimal?();
      nullable15 = nullable6;
    }
    else
      nullable15 = new Decimal?(nullable9.GetValueOrDefault() - num1);
    arRegister1.CuryRetainageUnpaidTotal = nullable15;
    ARRegister arRegister2 = retainageDocument;
    nullable9 = arRegister2.RetainageUnpaidTotal;
    Decimal num2 = valueOrDefault2 + valueOrDefault4 + valueOrDefault6;
    Decimal? nullable16;
    if (!nullable9.HasValue)
    {
      nullable6 = new Decimal?();
      nullable16 = nullable6;
    }
    else
      nullable16 = new Decimal?(nullable9.GetValueOrDefault() - num2);
    arRegister2.RetainageUnpaidTotal = nullable16;
    ((PXSelectBase) this.ARDocument).Cache.Update((object) retainageDocument);
    nullable1 = doc.IsMigratedRecord;
    if (nullable1.GetValueOrDefault())
      return;
    nullable1 = doc.PaymentsByLinesAllowed;
    ARReleaseProcess.ARTranPostKey key = nullable1.GetValueOrDefault() ? new ARReleaseProcess.ARTranPostKey(tran.OrigDocType, tran.OrigRefNbr, 0) : new ARReleaseProcess.ARTranPostKey(doc.OrigDocType, doc.OrigRefNbr, 0);
    ARTranPost arTranPost3;
    this.tranPostRetainagePayments.TryGetValue(key, out arTranPost3);
    if (arTranPost3 != null)
    {
      ARTranPost arTranPost4 = arTranPost3;
      nullable9 = arTranPost4.CuryAmt;
      Decimal num3 = valueOrDefault1;
      Decimal? nullable17;
      if (!nullable9.HasValue)
      {
        nullable6 = new Decimal?();
        nullable17 = nullable6;
      }
      else
        nullable17 = new Decimal?(nullable9.GetValueOrDefault() + num3);
      arTranPost4.CuryAmt = nullable17;
      ARTranPost arTranPost5 = arTranPost3;
      nullable9 = arTranPost5.Amt;
      Decimal num4 = valueOrDefault2;
      Decimal? nullable18;
      if (!nullable9.HasValue)
      {
        nullable6 = new Decimal?();
        nullable18 = nullable6;
      }
      else
        nullable18 = new Decimal?(nullable9.GetValueOrDefault() + num4);
      arTranPost5.Amt = nullable18;
      ARTranPost arTranPost6 = arTranPost3;
      nullable9 = arTranPost6.CuryDiscAmt;
      Decimal num5 = valueOrDefault3;
      Decimal? nullable19;
      if (!nullable9.HasValue)
      {
        nullable6 = new Decimal?();
        nullable19 = nullable6;
      }
      else
        nullable19 = new Decimal?(nullable9.GetValueOrDefault() + num5);
      arTranPost6.CuryDiscAmt = nullable19;
      ARTranPost arTranPost7 = arTranPost3;
      nullable9 = arTranPost7.DiscAmt;
      Decimal num6 = valueOrDefault4;
      Decimal? nullable20;
      if (!nullable9.HasValue)
      {
        nullable6 = new Decimal?();
        nullable20 = nullable6;
      }
      else
        nullable20 = new Decimal?(nullable9.GetValueOrDefault() + num6);
      arTranPost7.DiscAmt = nullable20;
      ARTranPost arTranPost8 = arTranPost3;
      nullable9 = arTranPost8.CuryWOAmt;
      Decimal num7 = valueOrDefault5;
      Decimal? nullable21;
      if (!nullable9.HasValue)
      {
        nullable6 = new Decimal?();
        nullable21 = nullable6;
      }
      else
        nullable21 = new Decimal?(nullable9.GetValueOrDefault() + num7);
      arTranPost8.CuryWOAmt = nullable21;
      ARTranPost arTranPost9 = arTranPost3;
      nullable9 = arTranPost9.WOAmt;
      Decimal num8 = valueOrDefault6;
      Decimal? nullable22;
      if (!nullable9.HasValue)
      {
        nullable6 = new Decimal?();
        nullable22 = nullable6;
      }
      else
        nullable22 = new Decimal?(nullable9.GetValueOrDefault() + num8);
      arTranPost9.WOAmt = nullable22;
    }
    else
    {
      ARTranPost tranPost = this.CreateTranPost(doc);
      tranPost.SourceDocType = pmt.DocType;
      tranPost.SourceRefNbr = pmt.RefNbr;
      tranPost.DocType = key.DocType;
      tranPost.RefNbr = key.RefNbr;
      tranPost.Type = "T";
      tranPost.CuryAmt = new Decimal?(valueOrDefault1);
      tranPost.Amt = new Decimal?(valueOrDefault2);
      tranPost.CuryDiscAmt = new Decimal?(valueOrDefault3);
      tranPost.DiscAmt = new Decimal?(valueOrDefault4);
      tranPost.CuryWOAmt = new Decimal?(valueOrDefault5);
      tranPost.WOAmt = new Decimal?(valueOrDefault6);
      this.tranPostRetainagePayments.Add(key, tranPost);
    }
  }

  protected virtual void ProcessRGOLTranPost(ARTranPost adjg)
  {
  }

  protected virtual ARTranPost CreateTranPost(ARRegister doc)
  {
    ARTranPost tranPost = new ARTranPost()
    {
      RefNoteID = doc.NoteID
    };
    tranPost.TranType = tranPost.DocType = tranPost.SourceDocType = doc.DocType;
    tranPost.TranRefNbr = tranPost.RefNbr = tranPost.SourceRefNbr = doc.RefNbr;
    tranPost.ReferenceID = tranPost.CustomerID = doc.CustomerID;
    tranPost.FinPeriodID = doc.FinPeriodID;
    tranPost.TranPeriodID = doc.TranPeriodID;
    tranPost.AccountID = doc.ARAccountID;
    tranPost.SubID = doc.ARSubID;
    tranPost.BranchID = doc.BranchID;
    tranPost.BatchNbr = doc.BatchNbr;
    tranPost.DocDate = doc.DocDate;
    tranPost.CuryInfoID = doc.CuryInfoID;
    tranPost.IsMigratedRecord = doc.IsMigratedRecord;
    tranPost.IsVoidPrepayment = new bool?(this.GetHistTranType(tranPost.TranType, tranPost.TranRefNbr) == "PPM");
    return tranPost;
  }

  private bool IsVoidPrepayment(ARRegister pmt)
  {
    if (pmt.DocType != "RPM")
      return false;
    switch (pmt.OrigDocType)
    {
      case "PPM":
        return true;
      case "PMT":
        return false;
      default:
        return ((IQueryable<PXResult<ARRegister>>) PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>, And<ARRegister.docType, Equal<Required<ARRegister.docType>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) pmt.RefNbr,
          (object) "PPM"
        })).Any<PXResult<ARRegister>>();
    }
  }

  /// <summary>Returns account id for trade discount transaction</summary>
  /// <param name="sotype">Order type of linked SO Order</param>
  /// <param name="origTran">Original <see cref="T:PX.Objects.AR.ARTran">ARTran</see> record, that has line level discount applied to it. Required for customization</param>
  /// <param name="newTran">New <see cref="T:PX.Objects.AR.ARTran">ARTran</see> record, that is being created for trade discount</param>
  /// <param name="ardoc"> <see cref="T:PX.Objects.AR.ARTran">ARInvoice</see> record that is being released</param>
  /// <returns>Account ID</returns>
  public virtual int? GetDefaultTradeDiscountAccountID(
    PX.Objects.SO.SOOrderType sotype,
    ARTran origTran,
    ARTran newTran,
    ARInvoice ardoc)
  {
    switch (sotype.DiscAcctDefault)
    {
      case "T":
        return sotype.DiscountAcctID;
      case "L":
        PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<ARInvoice.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<ARInvoice.customerLocationID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) ardoc.CustomerID,
          (object) ardoc.CustomerLocationID
        }));
        if (location == null)
          return new int?();
        if (location.CDiscountAcctID.HasValue)
          return location.CDiscountAcctID;
        if (PXAccess.FeatureInstalled<FeaturesSet.accountLocations>())
          throw new PXException("Discount Account is not set up. See Location \"{0}\" for Customer \"{1}\" ", new object[2]
          {
            (object) location.LocationCD,
            (object) ((PXGraph) this).Caches[typeof (ARInvoice)].GetValueExt<ARInvoice.customerID>((object) ardoc).ToString()
          });
        throw new PXException("Discount Account is not set up. See Customer \"{0}\" ", new object[1]
        {
          (object) ((PXGraph) this).Caches[typeof (ARInvoice)].GetValueExt<ARInvoice.customerID>((object) ardoc).ToString()
        });
      default:
        return new int?();
    }
  }

  /// <summary>
  /// Extension point for AR Release Invoice process. This method is called after GL Batch was created and all main GL Transactions have been
  /// inserted, but before Invoice rounding transaction, or RGOL transaction has been inserted.
  /// </summary>
  /// <param name="je">Journal Entry graph used for posting</param>
  /// <param name="ardoc">Original AR Invoice</param>
  /// <param name="arbatch">GL Batch that was created for Invoice</param>
  public virtual void ReleaseInvoiceBatchPostProcessing(
    JournalEntry je,
    ARInvoice ardoc,
    PX.Objects.GL.Batch arbatch)
  {
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public virtual void ReleaseInvoiceTransactionPostProcessing(
    JournalEntry je,
    ARInvoice ardoc,
    PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType> r,
    PX.Objects.GL.GLTran tran)
  {
  }

  public virtual void ReleaseInvoiceTransactionPostProcessing(
    JournalEntry je,
    ARInvoice ardoc,
    PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran> r,
    PX.Objects.GL.GLTran tran)
  {
    PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType> r1 = new PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType>(PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(r), PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(r), PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(r), PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(r), PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(r));
    this.ReleaseInvoiceTransactionPostProcessing(je, ardoc, r1, tran);
  }

  /// <summary>
  /// Extension point for AR Release Invoice process. This method is called after transaction was inserted.
  /// </summary>
  /// <param name="je">Journal Entry graph used for posting</param>
  /// <param name="ardoc">Original AR Invoice</param>
  /// <param name="n">Document line</param>
  public virtual void ReleaseInvoiceTransactionPostProcessed(
    JournalEntry je,
    ARInvoice ardoc,
    ARTran n)
  {
  }

  private string LocalizeForCustomer(Customer customer, string message)
  {
    using (new PXLocaleScope(customer.LocaleName))
      return PXMessages.LocalizeNoPrefix(message);
  }

  private ARTran CreateDiscountTran(
    ARTran originalTran,
    Decimal curyAmount,
    Decimal baseAmount,
    string tranDescr,
    ARRegister parentDocument)
  {
    ARTran copy = PXCache<ARTran>.CreateCopy(originalTran);
    copy.InventoryID = new int?();
    copy.SubItemID = new int?();
    copy.SOOrderSortOrder = new int?();
    copy.TaxCategoryID = (string) null;
    copy.SalesPersonID = new int?();
    copy.UOM = (string) null;
    copy.LineType = "DS";
    copy.TranDesc = tranDescr;
    copy.LineNbr = (int?) PXLineNbrAttribute.NewLineNbr<ARTran.lineNbr>(((PXSelectBase) this.ARTran_TranType_RefNbr).Cache, (object) parentDocument);
    Decimal? nullable = new Decimal?(curyAmount);
    copy.CuryExtPrice = nullable;
    copy.CuryTranAmt = nullable;
    nullable = new Decimal?(baseAmount);
    copy.ExtPrice = nullable;
    copy.TranAmt = nullable;
    copy.CuryDiscAmt = new Decimal?(0M);
    copy.Qty = new Decimal?(0M);
    copy.DiscPct = new Decimal?(0M);
    copy.CuryUnitPrice = new Decimal?(0M);
    copy.DiscountID = (string) null;
    copy.DiscountSequenceID = (string) null;
    copy.NoteID = new Guid?();
    copy.CuryAccruedCost = new Decimal?(0M);
    copy.AccruedCost = new Decimal?(0M);
    copy.AccrueCost = new bool?(false);
    return copy;
  }

  /// <summary>
  /// The method to process discount for each invoice detail inside the
  /// <see cref="!:ReleaseDocProc(JournalEntry, ref ARRegister, PXResult&lt;ARInvoice, CurrencyInfo, Terms, Customer, Account&gt;, out PMRegister)" /> method,
  /// depending on <see cref="T:PX.Objects.SO.SOOrderType.postLineDiscSeparately" /> flag
  /// and <see cref="T:PX.Objects.AR.ARTran.curyDiscAmt" /> amount.
  /// </summary>
  public virtual void ProcessInvoiceDetailDiscount(
    ARRegister doc,
    Customer customer,
    ARTran tran,
    PX.Objects.SO.SOOrderType sotype,
    ARInvoice ardoc)
  {
    Decimal? nullable;
    int num1;
    if (sotype.PostLineDiscSeparately.GetValueOrDefault() && sotype.DiscountAcctID.HasValue)
    {
      nullable = tran.CuryDiscAmt;
      Decimal num2 = 0.00005M;
      num1 = nullable.GetValueOrDefault() > num2 & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    if (num1 == 0)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) this.InvoiceEntryGraph).GetExtension<ARInvoiceEntry.MultiCurrency>().GetCurrencyInfo(tran.CuryInfoID);
    nullable = tran.TaxableAmt;
    Decimal num3 = 0M;
    Decimal num4;
    if (nullable.GetValueOrDefault() == num3 & nullable.HasValue)
    {
      num4 = 0M;
    }
    else
    {
      nullable = tran.CuryTaxAmt;
      Decimal num5 = nullable.Value;
      nullable = tran.CuryTaxableAmt;
      Decimal num6 = nullable.Value;
      num4 = num5 / num6;
    }
    Decimal num7 = num4;
    nullable = tran.CuryDiscAmt;
    Decimal num8 = currencyInfo.RoundCury(nullable.Value / (1M + num7));
    Decimal baseAmount = currencyInfo.CuryConvBase(num8);
    string tranDescr = this.LocalizeForCustomer(customer, "Item Discount");
    ((PXSelectBase) this.ARTran_TranType_RefNbr).Cache.Insert((object) this.CreateDiscountTran(tran, num8, baseAmount, tranDescr, doc));
    ARTran discountTran = this.CreateDiscountTran(tran, -num8, -baseAmount, tranDescr, doc);
    discountTran.AccountID = this.GetDefaultTradeDiscountAccountID(sotype, tran, discountTran, ardoc);
    bool? discountSubFromSalesSub = sotype.UseDiscountSubFromSalesSub;
    bool flag = false;
    if (discountSubFromSalesSub.GetValueOrDefault() == flag & discountSubFromSalesSub.HasValue)
    {
      PX.Objects.CR.Location data1 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<ARInvoice.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<ARInvoice.customerLocationID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) doc.CustomerID,
        (object) doc.CustomerLocationID
      }));
      PX.Objects.CR.Standalone.Location data2 = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<ARRegister.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) doc.BranchID
      }));
      object obj1 = this.GetValue<PX.Objects.SO.SOOrderType.discountSubID>((object) sotype);
      object obj2 = this.GetValue<PX.Objects.CR.Location.cDiscountSubID>((object) data1);
      object obj3 = this.GetValue<PX.Objects.CR.Standalone.Location.cMPDiscountSubID>((object) data2);
      object obj4 = (object) SODiscSubAccountMaskAttribute.MakeSub<PX.Objects.SO.SOOrderType.discSubMask>((PXGraph) this, sotype.DiscSubMask, new object[3]
      {
        obj1,
        obj2,
        obj3
      }, new System.Type[3]
      {
        typeof (PX.Objects.SO.SOOrderType.discountSubID),
        typeof (PX.Objects.CR.Location.cDiscountSubID),
        typeof (PX.Objects.CR.Location.cMPDiscountSubID)
      });
      ((PXSelectBase) this.ARTran_TranType_RefNbr).Cache.RaiseFieldUpdating<ARTran.subID>((object) discountTran, ref obj4);
      discountTran.SubID = (int?) obj4;
    }
    ((PXSelectBase) this.ARTran_TranType_RefNbr).Cache.Insert((object) discountTran);
  }

  protected virtual void PostRetainedTax(
    JournalEntry je,
    ARInvoice ardoc,
    PX.Objects.GL.GLTran origTran,
    ARTaxTran x,
    PX.Objects.TX.Tax salestax)
  {
    salestax.ReverseTax.GetValueOrDefault();
    bool flag1 = salestax.TaxType == "P";
    Decimal? nullable;
    int num1;
    if (ardoc.IsOriginalRetainageDocument())
    {
      nullable = x.CuryRetainedTaxAmt;
      Decimal num2 = 0M;
      if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
      {
        num1 = !flag1 ? 1 : 0;
        goto label_4;
      }
    }
    num1 = 0;
label_4:
    int num3;
    if (ardoc.IsRetainageDocument.GetValueOrDefault())
    {
      nullable = x.CuryTaxAmt;
      Decimal num4 = 0M;
      if (!(nullable.GetValueOrDefault() == num4 & nullable.HasValue))
      {
        num3 = !flag1 ? 1 : 0;
        goto label_8;
      }
    }
    num3 = 0;
label_8:
    bool flag2 = num3 != 0;
    if ((num1 | (flag2 ? 1 : 0)) != 0)
      this.RetainageTaxCheck(salestax);
    if (num1 != 0)
    {
      bool flag3 = ardoc.DrCr == "D";
      PX.Objects.GL.GLTran copy = PXCache<PX.Objects.GL.GLTran>.CreateCopy(origTran);
      copy.ReclassificationProhibited = new bool?(true);
      copy.AccountID = salestax.RetainageTaxPayableAcctID;
      copy.SubID = salestax.RetainageTaxPayableSubID;
      copy.CuryDebitAmt = flag3 ? x.CuryRetainedTaxAmt : new Decimal?(0M);
      copy.DebitAmt = flag3 ? x.RetainedTaxAmt : new Decimal?(0M);
      copy.CuryCreditAmt = flag3 ? new Decimal?(0M) : x.CuryRetainedTaxAmt;
      copy.CreditAmt = flag3 ? new Decimal?(0M) : x.RetainedTaxAmt;
      ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(copy);
    }
    else
    {
      if (!flag2)
        return;
      PX.Objects.GL.GLTran copy1 = PXCache<PX.Objects.GL.GLTran>.CreateCopy(origTran);
      copy1.ReclassificationProhibited = new bool?(true);
      copy1.AccountID = salestax.RetainageTaxPayableAcctID;
      copy1.SubID = salestax.RetainageTaxPayableSubID;
      copy1.CuryDebitAmt = origTran.CuryCreditAmt;
      copy1.DebitAmt = origTran.CreditAmt;
      copy1.CuryCreditAmt = origTran.CuryDebitAmt;
      copy1.CreditAmt = origTran.DebitAmt;
      ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(copy1);
      PX.Objects.GL.GLTran copy2 = PXCache<PX.Objects.GL.GLTran>.CreateCopy(origTran);
      copy1.ReclassificationProhibited = new bool?(true);
      copy2.SummPost = new bool?(true);
      copy2.AccountID = ardoc.RetainageAcctID;
      copy2.SubID = ardoc.RetainageSubID;
      copy2.CuryDebitAmt = origTran.CuryDebitAmt;
      copy2.DebitAmt = origTran.DebitAmt;
      copy2.CuryCreditAmt = origTran.CuryCreditAmt;
      copy2.CreditAmt = origTran.CreditAmt;
      ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(copy2);
    }
  }

  protected virtual void RetainageTaxCheck(PX.Objects.TX.Tax tax)
  {
    this.TaxAccountCheck<PX.Objects.TX.Tax.retainageTaxPayableAcctID>(tax);
    this.TaxAccountCheck<PX.Objects.TX.Tax.retainageTaxPayableSubID>(tax);
  }

  private void TaxAccountCheck<Field>(PX.Objects.TX.Tax tax) where Field : IBqlField
  {
    if (((PXGraph) this).Caches[BqlCommand.GetItemType(typeof (Field))].GetValue((object) tax, typeof (Field).Name) == null)
      throw new ReleaseException("The document cannot be released because the {0} is not specified for the {1} tax. To proceed, specify the account on the Taxes (TX205000) form.", new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName<Field>(((PXGraph) this).Caches[typeof (PX.Objects.TX.Tax)]),
        (object) tax.TaxID
      });
  }

  public static ARReleaseProcess.Amount GetSalesPostingAmount(PXGraph graph, ARTran documentLine)
  {
    PXResult<ARTran, ARTax, PX.Objects.TX.Tax, ARRegister> pxResult = ((IEnumerable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) new PXSelectJoin<ARTran, LeftJoin<ARTax, On<ARTax.tranType, Equal<ARTran.tranType>, And<ARTax.refNbr, Equal<ARTran.refNbr>, And<ARTax.lineNbr, Equal<ARTran.lineNbr>>>>, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTax.taxID>>, LeftJoin<ARRegister, On<ARRegister.docType, Equal<ARTran.tranType>, And<ARRegister.refNbr, Equal<ARTran.refNbr>>>>>>, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<ARTran.lineNbr, Equal<Required<ARTran.lineNbr>>>>>>(graph)).Select(new object[3]
    {
      (object) documentLine.TranType,
      (object) documentLine.RefNbr,
      (object) documentLine.LineNbr
    })).AsEnumerable<PXResult<ARTran>>().Cast<PXResult<ARTran, ARTax, PX.Objects.TX.Tax, ARRegister>>().First<PXResult<ARTran, ARTax, PX.Objects.TX.Tax, ARRegister>>();
    Func<Decimal, Decimal> round = (Func<Decimal, Decimal>) (amount => PX.Objects.CM.PXDBCurrencyAttribute.Round(graph.Caches[typeof (ARTran)], (object) documentLine, amount, CMPrecision.TRANCURY));
    return ARReleaseProcess.GetSalesPostingAmount(graph, PXResult<ARTran, ARTax, PX.Objects.TX.Tax, ARRegister>.op_Implicit(pxResult), documentLine, PXResult<ARTran, ARTax, PX.Objects.TX.Tax, ARRegister>.op_Implicit(pxResult), PXResult<ARTran, ARTax, PX.Objects.TX.Tax, ARRegister>.op_Implicit(pxResult), round);
  }

  /// <summary>
  /// Gets the amount to be posted to the sales acount
  /// for the given document line.
  /// </summary>
  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public static ARReleaseProcess.Amount GetSalesPostingAmount(
    PXGraph graph,
    ARRegister document,
    ARTran documentLine,
    ARTax lineTax,
    PX.Objects.TX.Tax salesTax,
    Func<Decimal, Decimal> round)
  {
    return ARReleaseProcess.GetSalesPostingAmount(graph, document, documentLine, lineTax, salesTax, round, round);
  }

  /// <summary>
  /// Gets the amount to be posted to the sales acount
  /// for the given document line.
  /// </summary>
  public static ARReleaseProcess.Amount GetSalesPostingAmount(
    PXGraph graph,
    ARRegister document,
    ARTran documentLine,
    ARTax lineTax,
    PX.Objects.TX.Tax salesTax,
    Func<Decimal, Decimal> roundCury,
    Func<Decimal, Decimal> roundBase)
  {
    bool flag1 = document.PendingPPD.GetValueOrDefault() && (document.DocType == "CRM" || document.DocType == "DRM");
    if (!flag1 && lineTax != null && lineTax.TaxID == null && document.DocType == "DRM" && document.OrigDocType == "CRM" && document.OrigRefNbr != null)
      flag1 = PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>, And<ARRegister.docType, Equal<Required<ARRegister.docType>>, And<ARRegister.pendingPPD, Equal<True>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[2]
      {
        (object) document.OrigRefNbr,
        (object) document.OrigDocType
      }).Count > 0;
    int num1 = !PXAccess.FeatureInstalled<FeaturesSet.aSC606>() || !documentLine.InventoryID.HasValue ? 0 : (documentLine.DeferredCode != null ? 1 : 0);
    bool flag2 = salesTax?.TaxCalcLevel == "0";
    if (num1 != 0)
    {
      if (!flag1 && lineTax != null && lineTax.TaxID != null && (flag2 || document.TaxCalcMode == "G"))
        return new ARReleaseProcess.Amount(lineTax.CuryTaxableAmt, lineTax.TaxableAmt);
      Func<Decimal, Decimal> func1 = roundCury;
      Decimal? curyTranAmt = documentLine.CuryTranAmt;
      Decimal? nullable1 = documentLine.OrigGroupDiscountRate;
      Decimal? nullable2 = curyTranAmt.HasValue & nullable1.HasValue ? new Decimal?(curyTranAmt.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable3 = documentLine.OrigDocumentDiscountRate;
      Decimal? nullable4;
      if (!(nullable2.HasValue & nullable3.HasValue))
      {
        nullable1 = new Decimal?();
        nullable4 = nullable1;
      }
      else
        nullable4 = new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault());
      nullable1 = nullable4;
      Decimal num2 = nullable1.Value;
      Decimal num3 = func1(num2);
      Func<Decimal, Decimal> func2 = roundBase;
      nullable1 = documentLine.TranAmt;
      Decimal? nullable5 = documentLine.OrigGroupDiscountRate;
      Decimal? nullable6 = nullable1.HasValue & nullable5.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
      nullable3 = documentLine.OrigDocumentDiscountRate;
      Decimal? nullable7;
      if (!(nullable6.HasValue & nullable3.HasValue))
      {
        nullable5 = new Decimal?();
        nullable7 = nullable5;
      }
      else
        nullable7 = new Decimal?(nullable6.GetValueOrDefault() * nullable3.GetValueOrDefault());
      nullable5 = nullable7;
      Decimal num4 = nullable5.Value;
      Decimal num5 = func2(num4);
      Func<Decimal, Decimal> func3 = roundCury;
      nullable5 = documentLine.CuryTranAmt;
      nullable1 = documentLine.DocumentDiscountRate;
      Decimal? nullable8 = nullable5.HasValue & nullable1.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
      nullable3 = documentLine.GroupDiscountRate;
      Decimal? nullable9;
      if (!(nullable8.HasValue & nullable3.HasValue))
      {
        nullable1 = new Decimal?();
        nullable9 = nullable1;
      }
      else
        nullable9 = new Decimal?(nullable8.GetValueOrDefault() * nullable3.GetValueOrDefault());
      nullable1 = nullable9;
      Decimal num6 = nullable1.Value;
      Decimal num7 = func3(num6);
      Func<Decimal, Decimal> func4 = roundBase;
      nullable1 = documentLine.TranAmt;
      nullable5 = documentLine.DocumentDiscountRate;
      Decimal? nullable10 = nullable1.HasValue & nullable5.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
      nullable3 = documentLine.GroupDiscountRate;
      Decimal? nullable11;
      if (!(nullable10.HasValue & nullable3.HasValue))
      {
        nullable5 = new Decimal?();
        nullable11 = nullable5;
      }
      else
        nullable11 = new Decimal?(nullable10.GetValueOrDefault() * nullable3.GetValueOrDefault());
      nullable5 = nullable11;
      Decimal num8 = nullable5.Value;
      Decimal num9 = func4(num8);
      Decimal? nullable12 = documentLine.CuryTaxableAmt;
      Decimal num10 = 0M;
      Decimal? cury;
      if (!(nullable12.GetValueOrDefault() == num10 & nullable12.HasValue))
      {
        cury = documentLine.CuryTaxableAmt;
      }
      else
      {
        Decimal num11 = num7 + num3;
        nullable12 = documentLine.CuryTranAmt;
        if (!nullable12.HasValue)
        {
          nullable3 = new Decimal?();
          cury = nullable3;
        }
        else
          cury = new Decimal?(num11 - nullable12.GetValueOrDefault());
      }
      nullable12 = documentLine.TaxableAmt;
      Decimal num12 = 0M;
      Decimal? nullable13;
      if (!(nullable12.GetValueOrDefault() == num12 & nullable12.HasValue))
      {
        nullable13 = documentLine.TaxableAmt;
      }
      else
      {
        Decimal num13 = num9 + num5;
        nullable12 = documentLine.TranAmt;
        if (!nullable12.HasValue)
        {
          nullable3 = new Decimal?();
          nullable13 = nullable3;
        }
        else
          nullable13 = new Decimal?(num13 - nullable12.GetValueOrDefault());
      }
      Decimal? baaase = nullable13;
      return new ARReleaseProcess.Amount(cury, baaase);
    }
    if (!flag1 && lineTax != null && lineTax.TaxID != null && (flag2 || document.TaxCalcMode == "G"))
    {
      if (salesTax.TaxType == "Q")
        return new ARReleaseProcess.Amount(documentLine.CuryTaxableAmt, documentLine.TaxableAmt);
      Decimal? nullable14 = documentLine.CuryTranAmt;
      Func<Decimal, Decimal> func5 = roundCury;
      Decimal? nullable15 = documentLine.CuryTranAmt;
      Decimal? nullable16 = documentLine.OrigGroupDiscountRate;
      Decimal? nullable17 = nullable15.HasValue & nullable16.HasValue ? new Decimal?(nullable15.GetValueOrDefault() * nullable16.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable18 = documentLine.OrigDocumentDiscountRate;
      Decimal? nullable19;
      if (!(nullable17.HasValue & nullable18.HasValue))
      {
        nullable16 = new Decimal?();
        nullable19 = nullable16;
      }
      else
        nullable19 = new Decimal?(nullable17.GetValueOrDefault() * nullable18.GetValueOrDefault());
      nullable16 = nullable19;
      Decimal num14 = nullable16.Value;
      Decimal num15 = func5(num14);
      Decimal? nullable20;
      if (!nullable14.HasValue)
      {
        nullable16 = new Decimal?();
        nullable20 = nullable16;
      }
      else
        nullable20 = new Decimal?(nullable14.GetValueOrDefault() - num15);
      Decimal? nullable21 = nullable20;
      nullable14 = documentLine.CuryTranAmt;
      Func<Decimal, Decimal> func6 = roundCury;
      Decimal? nullable22 = documentLine.CuryTranAmt;
      Decimal? nullable23 = documentLine.GroupDiscountRate;
      nullable16 = nullable22.HasValue & nullable23.HasValue ? new Decimal?(nullable22.GetValueOrDefault() * nullable23.GetValueOrDefault()) : new Decimal?();
      nullable15 = documentLine.DocumentDiscountRate;
      Decimal? nullable24;
      if (!(nullable16.HasValue & nullable15.HasValue))
      {
        nullable23 = new Decimal?();
        nullable24 = nullable23;
      }
      else
        nullable24 = new Decimal?(nullable16.GetValueOrDefault() * nullable15.GetValueOrDefault());
      nullable23 = nullable24;
      Decimal num16 = nullable23.Value;
      Decimal num17 = func6(num16);
      Decimal? nullable25;
      if (!nullable14.HasValue)
      {
        nullable23 = new Decimal?();
        nullable25 = nullable23;
      }
      else
        nullable25 = new Decimal?(nullable14.GetValueOrDefault() - num17);
      Decimal? nullable26 = nullable25;
      Decimal? nullable27;
      if (!(nullable21.HasValue & nullable26.HasValue))
      {
        nullable14 = new Decimal?();
        nullable27 = nullable14;
      }
      else
        nullable27 = new Decimal?(nullable21.GetValueOrDefault() + nullable26.GetValueOrDefault());
      nullable16 = documentLine.TranAmt;
      Func<Decimal, Decimal> func7 = roundBase;
      Decimal? nullable28 = documentLine.TranAmt;
      nullable14 = documentLine.OrigGroupDiscountRate;
      Decimal? nullable29;
      if (!(nullable28.HasValue & nullable14.HasValue))
      {
        nullable23 = new Decimal?();
        nullable29 = nullable23;
      }
      else
        nullable29 = new Decimal?(nullable28.GetValueOrDefault() * nullable14.GetValueOrDefault());
      nullable15 = nullable29;
      nullable26 = documentLine.OrigDocumentDiscountRate;
      Decimal? nullable30;
      if (!(nullable15.HasValue & nullable26.HasValue))
      {
        nullable14 = new Decimal?();
        nullable30 = nullable14;
      }
      else
        nullable30 = new Decimal?(nullable15.GetValueOrDefault() * nullable26.GetValueOrDefault());
      nullable14 = nullable30;
      Decimal num18 = nullable14.Value;
      Decimal num19 = func7(num18);
      Decimal? nullable31;
      if (!nullable16.HasValue)
      {
        nullable14 = new Decimal?();
        nullable31 = nullable14;
      }
      else
        nullable31 = new Decimal?(nullable16.GetValueOrDefault() - num19);
      Decimal? nullable32 = nullable31;
      nullable16 = documentLine.TranAmt;
      Func<Decimal, Decimal> func8 = roundBase;
      nullable23 = documentLine.TranAmt;
      nullable22 = documentLine.GroupDiscountRate;
      nullable14 = nullable23.HasValue & nullable22.HasValue ? new Decimal?(nullable23.GetValueOrDefault() * nullable22.GetValueOrDefault()) : new Decimal?();
      nullable28 = documentLine.DocumentDiscountRate;
      Decimal? nullable33;
      if (!(nullable14.HasValue & nullable28.HasValue))
      {
        nullable22 = new Decimal?();
        nullable33 = nullable22;
      }
      else
        nullable33 = new Decimal?(nullable14.GetValueOrDefault() * nullable28.GetValueOrDefault());
      nullable22 = nullable33;
      Decimal num20 = nullable22.Value;
      Decimal num21 = func8(num20);
      Decimal? nullable34;
      if (!nullable16.HasValue)
      {
        nullable22 = new Decimal?();
        nullable34 = nullable22;
      }
      else
        nullable34 = new Decimal?(nullable16.GetValueOrDefault() - num21);
      nullable18 = nullable34;
      Decimal? nullable35;
      if (!(nullable32.HasValue & nullable18.HasValue))
      {
        nullable16 = new Decimal?();
        nullable35 = nullable16;
      }
      else
        nullable35 = new Decimal?(nullable32.GetValueOrDefault() + nullable18.GetValueOrDefault());
      Decimal? nullable36 = nullable35;
      Decimal? curyTaxableAmt = lineTax.CuryTaxableAmt;
      nullable16 = lineTax.CuryExemptedAmt;
      Decimal? nullable37;
      if (!(curyTaxableAmt.HasValue & nullable16.HasValue))
      {
        nullable22 = new Decimal?();
        nullable37 = nullable22;
      }
      else
        nullable37 = new Decimal?(curyTaxableAmt.GetValueOrDefault() + nullable16.GetValueOrDefault());
      nullable18 = nullable37;
      nullable16 = lineTax.CuryRetainedTaxableAmt;
      Decimal valueOrDefault1 = nullable16.GetValueOrDefault();
      Decimal? nullable38;
      if (!nullable18.HasValue)
      {
        nullable16 = new Decimal?();
        nullable38 = nullable16;
      }
      else
        nullable38 = new Decimal?(nullable18.GetValueOrDefault() + valueOrDefault1);
      nullable14 = nullable38;
      nullable28 = nullable27;
      Decimal? nullable39;
      if (!(nullable14.HasValue & nullable28.HasValue))
      {
        nullable18 = new Decimal?();
        nullable39 = nullable18;
      }
      else
        nullable39 = new Decimal?(nullable14.GetValueOrDefault() + nullable28.GetValueOrDefault());
      nullable15 = nullable39;
      nullable26 = lineTax.CuryTaxableDiscountAmt;
      Decimal? cury;
      if (!(nullable15.HasValue & nullable26.HasValue))
      {
        nullable28 = new Decimal?();
        cury = nullable28;
      }
      else
        cury = new Decimal?(nullable15.GetValueOrDefault() + nullable26.GetValueOrDefault());
      nullable16 = lineTax.TaxableAmt;
      Decimal? nullable40 = lineTax.ExemptedAmt;
      Decimal? nullable41;
      if (!(nullable16.HasValue & nullable40.HasValue))
      {
        nullable22 = new Decimal?();
        nullable41 = nullable22;
      }
      else
        nullable41 = new Decimal?(nullable16.GetValueOrDefault() + nullable40.GetValueOrDefault());
      nullable18 = nullable41;
      nullable40 = lineTax.RetainedTaxableAmt;
      Decimal valueOrDefault2 = nullable40.GetValueOrDefault();
      Decimal? nullable42;
      if (!nullable18.HasValue)
      {
        nullable40 = new Decimal?();
        nullable42 = nullable40;
      }
      else
        nullable42 = new Decimal?(nullable18.GetValueOrDefault() + valueOrDefault2);
      nullable28 = nullable42;
      nullable14 = nullable36;
      Decimal? nullable43;
      if (!(nullable28.HasValue & nullable14.HasValue))
      {
        nullable18 = new Decimal?();
        nullable43 = nullable18;
      }
      else
        nullable43 = new Decimal?(nullable28.GetValueOrDefault() + nullable14.GetValueOrDefault());
      nullable26 = nullable43;
      nullable15 = lineTax.TaxableDiscountAmt;
      Decimal? baaase;
      if (!(nullable26.HasValue & nullable15.HasValue))
      {
        nullable14 = new Decimal?();
        baaase = nullable14;
      }
      else
        baaase = new Decimal?(nullable26.GetValueOrDefault() + nullable15.GetValueOrDefault());
      return new ARReleaseProcess.Amount(cury, baaase);
    }
    if (flag1)
      return new ARReleaseProcess.Amount(documentLine.CuryTaxableAmt, documentLine.TaxableAmt);
    Decimal? curyTranAmt1 = documentLine.CuryTranAmt;
    Decimal valueOrDefault3 = documentLine.CuryRetainageAmt.GetValueOrDefault();
    Decimal? cury1 = curyTranAmt1.HasValue ? new Decimal?(curyTranAmt1.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
    Decimal? tranAmt = documentLine.TranAmt;
    Decimal valueOrDefault4 = documentLine.RetainageAmt.GetValueOrDefault();
    Decimal? baaase1 = tranAmt.HasValue ? new Decimal?(tranAmt.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
    return new ARReleaseProcess.Amount(cury1, baaase1);
  }

  protected virtual void VerifyRoundingAllowed(ARInvoice document, PX.Objects.GL.Batch batch, string baseCuryID)
  {
    this.VerifyRoundingAllowed(document, batch, baseCuryID, 0M);
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2020 R2.")]
  protected virtual void VerifyRoundingAllowed(
    ARInvoice document,
    PX.Objects.GL.Batch batch,
    string baseCuryID,
    Decimal taxesDiff)
  {
    PX.Objects.CM.Currency currency = PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelect<PX.Objects.CM.Currency, Where<PX.Objects.CM.Currency.curyID, Equal<Required<ARInvoice.curyID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) document.CuryID
    }));
    Decimal? debitTotal = batch.DebitTotal;
    Decimal? creditTotal = batch.CreditTotal;
    Decimal num1 = Math.Abs(Math.Round((debitTotal.HasValue & creditTotal.HasValue ? new Decimal?(debitTotal.GetValueOrDefault() - creditTotal.GetValueOrDefault()) : new Decimal?()).Value, 4));
    if (!currency.UseARPreferencesSettings.GetValueOrDefault() ? currency.ARInvoiceRounding == "N" : this.InvoiceRounding == "N")
      throw new PXException("The document is out of the balance.");
    Decimal num2 = num1;
    Decimal? roundingLimit = CurrencyCollection.GetCurrency(baseCuryID).RoundingLimit;
    Decimal valueOrDefault = roundingLimit.GetValueOrDefault();
    if (num2 > valueOrDefault & roundingLimit.HasValue)
      throw new PXException("The amount to be posted to the rounding account ({1} {0}) exceeds the limit ({2} {0}) specified on the Currencies (CM202000) form.", new object[3]
      {
        (object) baseCuryID,
        (object) num1,
        (object) PXDBQuantityAttribute.Round(CurrencyCollection.GetCurrency(baseCuryID).RoundingLimit)
      });
  }

  protected object GetValue<Field>(object data) where Field : IBqlField
  {
    return ((PXGraph) this).Caches[typeof (Field).DeclaringType].GetValue(data, typeof (Field).Name);
  }

  public int? GetValueInt<SourceField>(PXGraph target, object item) where SourceField : IBqlField
  {
    PXCache cach1 = ((PXGraph) this).Caches[typeof (SourceField).DeclaringType];
    PXCache cach2 = target.Caches[typeof (SourceField).DeclaringType];
    object obj = item;
    object valueExt = cach1.GetValueExt<SourceField>(obj);
    if (valueExt is PXFieldState)
      valueExt = ((PXFieldState) valueExt).Value;
    if (valueExt != null)
      cach2.RaiseFieldUpdating<SourceField>(item, ref valueExt);
    return (int?) valueExt;
  }

  public static void UpdateARBalances(PXGraph graph, ARRegister ardoc, Decimal? BalanceAmt)
  {
    Decimal? signBalance = ardoc.SignBalance;
    Decimal num1 = 1M;
    Decimal? nullable1;
    Decimal? nullable2;
    if (!(signBalance.GetValueOrDefault() == num1 & signBalance.HasValue))
    {
      nullable1 = BalanceAmt;
      nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable2 = BalanceAmt;
    Decimal? nullable3 = nullable2;
    bool? nullable4 = ardoc.Released;
    if (nullable4.GetValueOrDefault())
    {
      nullable4 = ardoc.Voided;
      if (!nullable4.GetValueOrDefault())
      {
        nullable1 = ardoc.SignBalance;
        Decimal num2 = 0M;
        if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
        {
          ARReleaseProcess.UpdateARBalances(graph, ardoc, nullable3, new Decimal?(0M));
          return;
        }
      }
    }
    nullable4 = ardoc.Hold;
    if (nullable4.GetValueOrDefault())
      return;
    nullable4 = ardoc.Scheduled;
    if (nullable4.GetValueOrDefault())
      return;
    nullable4 = ardoc.Voided;
    if (nullable4.GetValueOrDefault())
      return;
    nullable1 = ardoc.SignBalance;
    Decimal num3 = 0M;
    if (nullable1.GetValueOrDefault() == num3 & nullable1.HasValue)
      return;
    ARReleaseProcess.UpdateARBalances(graph, ardoc, new Decimal?(0M), nullable3);
  }

  public static void UpdateARBalancesLastDocDate(ARBalances arbal, DateTime? date)
  {
    arbal.StatementRequired = new bool?(true);
    if (arbal.LastDocDate.HasValue)
    {
      DateTime? lastDocDate = arbal.LastDocDate;
      DateTime? nullable = date;
      if ((lastDocDate.HasValue & nullable.HasValue ? (lastDocDate.GetValueOrDefault() < nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
    }
    arbal.LastDocDate = date;
  }

  public static void UpdateARBalances(
    PXGraph graph,
    ARRegister ardoc,
    Decimal? CurrentBal,
    Decimal? UnreleasedBal)
  {
    if (!ardoc.CustomerID.HasValue || !ardoc.CustomerLocationID.HasValue)
      return;
    ARBalances arbal = (ARBalances) graph.Caches[typeof (ARBalances)].Insert((object) new ARBalances()
    {
      BranchID = ardoc.BranchID,
      CustomerID = ardoc.CustomerID,
      CustomerLocationID = ardoc.CustomerLocationID
    });
    if (!ardoc.IsPrepaymentInvoiceDocument() && !ardoc.IsPrepaymentInvoiceDocumentReverse())
    {
      ARBalances arBalances1 = arbal;
      Decimal? currentBal = arBalances1.CurrentBal;
      Decimal? nullable1 = CurrentBal;
      arBalances1.CurrentBal = currentBal.HasValue & nullable1.HasValue ? new Decimal?(currentBal.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      ARBalances arBalances2 = arbal;
      nullable1 = arBalances2.UnreleasedBal;
      Decimal? nullable2 = UnreleasedBal;
      arBalances2.UnreleasedBal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    }
    if (!ardoc.Released.GetValueOrDefault())
      return;
    ARReleaseProcess.UpdateARBalancesLastDocDate(arbal, ardoc.DocDate);
  }

  public static void UpdateARBalances(PXGraph graph, ARInvoice ardoc, Decimal? BalanceAmt)
  {
    Decimal? signBalance = ardoc.SignBalance;
    Decimal num1 = 1M;
    Decimal? nullable1;
    Decimal? nullable2;
    if (!(signBalance.GetValueOrDefault() == num1 & signBalance.HasValue))
    {
      nullable1 = BalanceAmt;
      nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable2 = BalanceAmt;
    Decimal? nullable3 = nullable2;
    bool? nullable4 = ardoc.Released;
    if (nullable4.GetValueOrDefault())
    {
      nullable4 = ardoc.Voided;
      if (!nullable4.GetValueOrDefault())
      {
        nullable1 = ardoc.SignBalance;
        Decimal num2 = 0M;
        if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
        {
          ARReleaseProcess.UpdateARBalances(graph, (ARRegister) ardoc, nullable3, new Decimal?(0M));
          return;
        }
      }
    }
    nullable4 = ardoc.Hold;
    if (nullable4.GetValueOrDefault())
      return;
    nullable4 = ardoc.PendingProcessing;
    if (nullable4.GetValueOrDefault())
      return;
    nullable4 = ardoc.CreditHold;
    if (nullable4.GetValueOrDefault())
      return;
    nullable4 = ardoc.Scheduled;
    if (nullable4.GetValueOrDefault())
      return;
    nullable4 = ardoc.Voided;
    if (nullable4.GetValueOrDefault())
      return;
    nullable1 = ardoc.SignBalance;
    Decimal num3 = 0M;
    if (nullable1.GetValueOrDefault() == num3 & nullable1.HasValue)
      return;
    ARReleaseProcess.UpdateARBalances(graph, (ARRegister) ardoc, new Decimal?(0M), nullable3);
  }

  public static void UpdateARBalances(
    PXGraph graph,
    PX.Objects.SO.SOOrder order,
    Decimal? UnbilledAmount,
    Decimal? UnshippedAmount)
  {
    if (!order.CustomerID.HasValue || !order.CustomerLocationID.HasValue)
      return;
    ARBalances arBalances1 = (ARBalances) graph.Caches[typeof (ARBalances)].Insert((object) new ARBalances()
    {
      BranchID = order.BranchID,
      CustomerID = order.CustomerID,
      CustomerLocationID = order.CustomerLocationID
    });
    Decimal? nullable1 = ARDocType.SignBalance(order.ARDocType);
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue || !ARDocType.SignBalance(order.ARDocType).HasValue)
      return;
    int? shipmentCntr = order.ShipmentCntr;
    int num2 = 0;
    Decimal? nullable2;
    Decimal? nullable3;
    Decimal? nullable4;
    Decimal? nullable5;
    if (shipmentCntr.GetValueOrDefault() == num2 & shipmentCntr.HasValue)
    {
      nullable2 = UnbilledAmount;
    }
    else
    {
      nullable2 = UnshippedAmount;
      ARBalances arBalances2 = arBalances1;
      Decimal? totalOpenOrders = arBalances2.TotalOpenOrders;
      Decimal? nullable6 = ARDocType.SignBalance(order.ARDocType);
      Decimal num3 = 1M;
      Decimal? nullable7;
      if (!(nullable6.GetValueOrDefault() == num3 & nullable6.HasValue))
      {
        Decimal? nullable8 = UnbilledAmount;
        nullable3 = UnshippedAmount;
        nullable7 = nullable8.HasValue & nullable3.HasValue ? new Decimal?(-(nullable8.GetValueOrDefault() - nullable3.GetValueOrDefault())) : new Decimal?();
      }
      else
      {
        Decimal? nullable9 = UnbilledAmount;
        nullable4 = UnshippedAmount;
        nullable7 = nullable9.HasValue & nullable4.HasValue ? new Decimal?(nullable9.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
      }
      nullable5 = nullable7;
      Decimal? nullable10;
      if (!(totalOpenOrders.HasValue & nullable5.HasValue))
      {
        nullable4 = new Decimal?();
        nullable10 = nullable4;
      }
      else
        nullable10 = new Decimal?(totalOpenOrders.GetValueOrDefault() + nullable5.GetValueOrDefault());
      arBalances2.TotalOpenOrders = nullable10;
    }
    if (!order.InclCustOpenOrders.GetValueOrDefault())
      return;
    bool? nullable11 = order.Cancelled;
    bool flag1 = false;
    if (!(nullable11.GetValueOrDefault() == flag1 & nullable11.HasValue))
      return;
    nullable11 = order.Hold;
    bool flag2 = false;
    if (!(nullable11.GetValueOrDefault() == flag2 & nullable11.HasValue))
      return;
    nullable11 = order.CreditHold;
    bool flag3 = false;
    if (!(nullable11.GetValueOrDefault() == flag3 & nullable11.HasValue))
      return;
    ARBalances arBalances3 = arBalances1;
    nullable5 = arBalances3.TotalOpenOrders;
    nullable4 = ARDocType.SignBalance(order.ARDocType);
    Decimal num4 = 1M;
    Decimal? nullable12;
    if (!(nullable4.GetValueOrDefault() == num4 & nullable4.HasValue))
    {
      nullable4 = nullable2;
      if (!nullable4.HasValue)
      {
        nullable3 = new Decimal?();
        nullable12 = nullable3;
      }
      else
        nullable12 = new Decimal?(-nullable4.GetValueOrDefault());
    }
    else
      nullable12 = nullable2;
    Decimal? nullable13 = nullable12;
    Decimal? nullable14;
    if (!(nullable5.HasValue & nullable13.HasValue))
    {
      nullable4 = new Decimal?();
      nullable14 = nullable4;
    }
    else
      nullable14 = new Decimal?(nullable5.GetValueOrDefault() + nullable13.GetValueOrDefault());
    arBalances3.TotalOpenOrders = nullable14;
  }

  private void UpdateARBalances(ARAdjust adj, ARRegister ardoc)
  {
    if (adj.AdjgDocType == "CRM" && adj.AdjdDocType == "PPI" || ardoc.DocType == "SMC" && adj.Voided.GetValueOrDefault() && adj.VoidAdjNbr.HasValue)
      return;
    if (string.Equals(ardoc.DocType, adj.AdjdDocType) && string.Equals(ardoc.RefNbr, adj.AdjdRefNbr, StringComparison.OrdinalIgnoreCase))
    {
      if (!ardoc.CustomerID.HasValue || !ardoc.CustomerLocationID.HasValue)
        return;
      ARBalances arbal = (ARBalances) ((PXGraph) this).Caches[typeof (ARBalances)].Insert((object) new ARBalances()
      {
        BranchID = ardoc.BranchID,
        CustomerID = ardoc.CustomerID,
        CustomerLocationID = ardoc.CustomerLocationID
      });
      Decimal num1 = 1M;
      bool? nullable1;
      if (adj.AdjgDocType == "PPI")
      {
        nullable1 = ((ARRegister) ((PXSelectBase) this.ARDocument).Cache.Locate((object) new ARRegister()
        {
          DocType = adj.AdjgDocType,
          RefNbr = adj.AdjgRefNbr
        })).PendingPayment;
        bool flag = false;
        if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
          num1 = -1M;
      }
      ARBalances arBalances = arbal;
      Decimal? currentBal = arBalances.CurrentBal;
      Decimal num2 = num1;
      Decimal? nullable2 = adj.AdjdTBSign;
      Decimal? nullable3 = nullable2.HasValue ? new Decimal?(num2 * nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal? adjAmt = adj.AdjAmt;
      Decimal? nullable4 = adj.AdjDiscAmt;
      nullable2 = adjAmt.HasValue & nullable4.HasValue ? new Decimal?(adjAmt.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      Decimal? adjWoAmt = adj.AdjWOAmt;
      Decimal? nullable5;
      if (!(nullable2.HasValue & adjWoAmt.HasValue))
      {
        nullable4 = new Decimal?();
        nullable5 = nullable4;
      }
      else
        nullable5 = new Decimal?(nullable2.GetValueOrDefault() + adjWoAmt.GetValueOrDefault());
      Decimal? nullable6 = nullable5;
      Decimal? nullable7 = nullable3.HasValue & nullable6.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable6.GetValueOrDefault()) : new Decimal?();
      Decimal? rgolAmt = adj.RGOLAmt;
      Decimal? nullable8 = nullable7.HasValue & rgolAmt.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - rgolAmt.GetValueOrDefault()) : new Decimal?();
      arBalances.CurrentBal = currentBal.HasValue & nullable8.HasValue ? new Decimal?(currentBal.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
      nullable1 = ardoc.Released;
      if (!nullable1.GetValueOrDefault())
        return;
      ARReleaseProcess.UpdateARBalancesLastDocDate(arbal, adj.AdjgDocDate);
    }
    else if (string.Equals(ardoc.DocType, adj.AdjgDocType) && string.Equals(ardoc.RefNbr, adj.AdjgRefNbr, StringComparison.OrdinalIgnoreCase))
    {
      if (!ardoc.CustomerID.HasValue || !ardoc.CustomerLocationID.HasValue)
        return;
      ARBalances arbal = (ARBalances) ((PXGraph) this).Caches[typeof (ARBalances)].Insert((object) new ARBalances()
      {
        BranchID = ardoc.BranchID,
        CustomerID = ardoc.CustomerID,
        CustomerLocationID = ardoc.CustomerLocationID
      });
      ARBalances arBalances = arbal;
      Decimal? currentBal = arBalances.CurrentBal;
      Decimal? adjgTbSign = adj.AdjgTBSign;
      Decimal? nullable9 = adj.AdjAmt;
      Decimal? nullable10 = adjgTbSign.HasValue & nullable9.HasValue ? new Decimal?(adjgTbSign.GetValueOrDefault() * nullable9.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable11;
      if (!(currentBal.HasValue & nullable10.HasValue))
      {
        nullable9 = new Decimal?();
        nullable11 = nullable9;
      }
      else
        nullable11 = new Decimal?(currentBal.GetValueOrDefault() + nullable10.GetValueOrDefault());
      arBalances.CurrentBal = nullable11;
      if (!ardoc.Released.GetValueOrDefault())
        return;
      ARReleaseProcess.UpdateARBalancesLastDocDate(arbal, adj.AdjgDocDate);
    }
    else
      throw new PXException("Failed to process an application between documents {0} {1} and {2} {3} -\tone of these documents cannot be found. Check whether both documents exist in the system.", new object[4]
      {
        (object) adj.AdjgDocType,
        (object) adj.AdjgRefNbr,
        (object) adj.AdjdDocType,
        (object) adj.AdjdRefNbr
      });
  }

  private void UpdateARBalances(ARRegister ardoc)
  {
    ARReleaseProcess.UpdateARBalances((PXGraph) this, ardoc, ardoc.OrigDocAmt);
  }

  private void UpdateARBalances(ARRegister ardoc, Decimal? BalanceAmt)
  {
    ARReleaseProcess.UpdateARBalances((PXGraph) this, ardoc, BalanceAmt);
  }

  public virtual void VoidOrigAdjustment(ARAdjust adj)
  {
    string[] strArray = ARPaymentType.GetVoidedARDocType(adj.AdjgDocType);
    if (strArray.Length == 0)
      strArray = new string[1]{ adj.AdjgDocType };
    ARAdjust arAdjust1 = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjgDocType, In<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.adjNbr, Equal<Required<ARAdjust.adjNbr>>, And<ARAdjust.adjdLineNbr, Equal<Required<ARAdjust.adjdLineNbr>>>>>>>>>.Config>.Select((PXGraph) this, new object[6]
    {
      (object) strArray,
      (object) adj.AdjgRefNbr,
      (object) adj.AdjdDocType,
      (object) adj.AdjdRefNbr,
      (object) adj.VoidAdjNbr,
      (object) adj.AdjdLineNbr
    }));
    if (arAdjust1 == null)
      return;
    if (arAdjust1.Voided.Value)
      throw new PXException("This document application is already voided. Document will not be released.");
    PXCache<ARAdjust>.StoreOriginal((PXGraph) this, arAdjust1);
    arAdjust1.Voided = new bool?(true);
    ARAdjust voidadj = (ARAdjust) ((PXGraph) this).Caches[typeof (ARAdjust)].Update((object) arAdjust1);
    ARAdjust arAdjust2 = adj;
    Decimal? adjAmt = voidadj.AdjAmt;
    Decimal? nullable1 = adjAmt.HasValue ? new Decimal?(-adjAmt.GetValueOrDefault()) : new Decimal?();
    arAdjust2.AdjAmt = nullable1;
    ARAdjust arAdjust3 = adj;
    Decimal? adjDiscAmt = voidadj.AdjDiscAmt;
    Decimal? nullable2 = adjDiscAmt.HasValue ? new Decimal?(-adjDiscAmt.GetValueOrDefault()) : new Decimal?();
    arAdjust3.AdjDiscAmt = nullable2;
    ARAdjust arAdjust4 = adj;
    Decimal? adjWoAmt = voidadj.AdjWOAmt;
    Decimal? nullable3 = adjWoAmt.HasValue ? new Decimal?(-adjWoAmt.GetValueOrDefault()) : new Decimal?();
    arAdjust4.AdjWOAmt = nullable3;
    ARAdjust arAdjust5 = adj;
    Decimal? rgolAmt = voidadj.RGOLAmt;
    Decimal? nullable4 = rgolAmt.HasValue ? new Decimal?(-rgolAmt.GetValueOrDefault()) : new Decimal?();
    arAdjust5.RGOLAmt = nullable4;
    ARAdjust arAdjust6 = adj;
    Decimal? curyAdjdAmt = voidadj.CuryAdjdAmt;
    Decimal? nullable5 = curyAdjdAmt.HasValue ? new Decimal?(-curyAdjdAmt.GetValueOrDefault()) : new Decimal?();
    arAdjust6.CuryAdjdAmt = nullable5;
    ARAdjust arAdjust7 = adj;
    Decimal? curyAdjdDiscAmt = voidadj.CuryAdjdDiscAmt;
    Decimal? nullable6 = curyAdjdDiscAmt.HasValue ? new Decimal?(-curyAdjdDiscAmt.GetValueOrDefault()) : new Decimal?();
    arAdjust7.CuryAdjdDiscAmt = nullable6;
    ARAdjust arAdjust8 = adj;
    Decimal? curyAdjdWoAmt = voidadj.CuryAdjdWOAmt;
    Decimal? nullable7 = curyAdjdWoAmt.HasValue ? new Decimal?(-curyAdjdWoAmt.GetValueOrDefault()) : new Decimal?();
    arAdjust8.CuryAdjdWOAmt = nullable7;
    ARAdjust arAdjust9 = adj;
    Decimal? curyAdjgAmt = voidadj.CuryAdjgAmt;
    Decimal? nullable8 = curyAdjgAmt.HasValue ? new Decimal?(-curyAdjgAmt.GetValueOrDefault()) : new Decimal?();
    arAdjust9.CuryAdjgAmt = nullable8;
    ARAdjust arAdjust10 = adj;
    Decimal? curyAdjgDiscAmt = voidadj.CuryAdjgDiscAmt;
    Decimal? nullable9 = curyAdjgDiscAmt.HasValue ? new Decimal?(-curyAdjgDiscAmt.GetValueOrDefault()) : new Decimal?();
    arAdjust10.CuryAdjgDiscAmt = nullable9;
    ARAdjust arAdjust11 = adj;
    Decimal? curyAdjgWoAmt = voidadj.CuryAdjgWOAmt;
    Decimal? nullable10 = curyAdjgWoAmt.HasValue ? new Decimal?(-curyAdjgWoAmt.GetValueOrDefault()) : new Decimal?();
    arAdjust11.CuryAdjgWOAmt = nullable10;
    adj = (ARAdjust) ((PXGraph) this).Caches[typeof (ARAdjust)].Update((object) adj);
    this.OnAdjustVoided(voidadj);
  }

  protected virtual void OnAdjustVoided(ARAdjust voidadj)
  {
    if (voidadj.AdjgDocType == "CRM" && voidadj.AdjdHasPPDTaxes.GetValueOrDefault())
    {
      ARRegister arRegister = PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.docType, Equal<ARDocType.creditMemo>, And<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) voidadj.AdjgRefNbr
      }));
      if (arRegister != null && arRegister.PendingPPD.GetValueOrDefault())
        PXUpdate<Set<ARAdjust.pPDVATAdjRefNbr, Null>, ARAdjust, Where<ARAdjust.pendingPPD, Equal<True>, And<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.pPDVATAdjRefNbr, Equal<Required<ARAdjust.pPDVATAdjRefNbr>>>>>>>.Update((PXGraph) this, new object[3]
        {
          (object) voidadj.AdjdDocType,
          (object) voidadj.AdjdRefNbr,
          (object) voidadj.AdjgRefNbr
        });
    }
    if (!EnumerableExtensions.IsIn<string>(voidadj.AdjdDocType, "INV", "DRM", "FCH", "SMC", "CRM", Array.Empty<string>()))
      return;
    PX.Objects.AR.Standalone.ARInvoice arInvoice1 = PXResultset<PX.Objects.AR.Standalone.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.Standalone.ARInvoice>) this.StandaloneARInvoice).Select(new object[2]
    {
      (object) voidadj.AdjdDocType,
      (object) voidadj.AdjdRefNbr
    }));
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXGraph) this).GetExtension<ARReleaseProcess.MultiCurrency>().GetCurrencyInfo(voidadj.AdjdCuryInfoID);
    PX.Objects.AR.Standalone.ARInvoice arInvoice2 = arInvoice1;
    Decimal? nullable1 = arInvoice2.CuryBalanceWOTotal;
    Decimal? nullable2 = voidadj.CuryAdjdWOAmt;
    arInvoice2.CuryBalanceWOTotal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    PX.Objects.AR.Standalone.ARInvoice arInvoice3 = arInvoice1;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = currencyInfo1;
    nullable2 = arInvoice1.CuryBalanceWOTotal;
    Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(currencyInfo2.CuryConvBase(valueOrDefault1));
    arInvoice3.BalanceWOTotal = nullable3;
    PX.Objects.AR.Standalone.ARInvoice arInvoice4 = arInvoice1;
    nullable2 = arInvoice4.CuryDiscAppliedAmt;
    nullable1 = voidadj.CuryAdjdDiscAmt;
    arInvoice4.CuryDiscAppliedAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    PX.Objects.AR.Standalone.ARInvoice arInvoice5 = arInvoice1;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo1;
    nullable1 = arInvoice1.CuryDiscAppliedAmt;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal? nullable4 = new Decimal?(currencyInfo3.CuryConvBase(valueOrDefault2));
    arInvoice5.DiscAppliedAmt = nullable4;
    PX.Objects.AR.Standalone.ARInvoice arInvoice6 = arInvoice1;
    nullable1 = arInvoice6.CuryPaymentTotal;
    nullable2 = voidadj.CuryAdjdAmt;
    arInvoice6.CuryPaymentTotal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    PX.Objects.AR.Standalone.ARInvoice arInvoice7 = arInvoice1;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo4 = currencyInfo1;
    nullable2 = arInvoice1.CuryPaymentTotal;
    Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
    Decimal? nullable5 = new Decimal?(currencyInfo4.CuryConvBase(valueOrDefault3));
    arInvoice7.PaymentTotal = nullable5;
    PX.Objects.AR.Standalone.ARInvoice arInvoice8 = arInvoice1;
    nullable2 = arInvoice8.CuryUnpaidBalance;
    Decimal? curyAdjdWoAmt = voidadj.CuryAdjdWOAmt;
    Decimal? curyAdjdDiscAmt = voidadj.CuryAdjdDiscAmt;
    Decimal? nullable6 = curyAdjdWoAmt.HasValue & curyAdjdDiscAmt.HasValue ? new Decimal?(curyAdjdWoAmt.GetValueOrDefault() + curyAdjdDiscAmt.GetValueOrDefault()) : new Decimal?();
    Decimal? curyAdjdAmt = voidadj.CuryAdjdAmt;
    nullable1 = nullable6.HasValue & curyAdjdAmt.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + curyAdjdAmt.GetValueOrDefault()) : new Decimal?();
    arInvoice8.CuryUnpaidBalance = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    PX.Objects.AR.Standalone.ARInvoice arInvoice9 = arInvoice1;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo5 = currencyInfo1;
    nullable1 = arInvoice1.CuryUnpaidBalance;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    Decimal? nullable7 = new Decimal?(currencyInfo5.CuryConvBase(valueOrDefault4));
    arInvoice9.UnpaidBalance = nullable7;
    ((PXSelectBase<PX.Objects.AR.Standalone.ARInvoice>) this.StandaloneARInvoice).Update(arInvoice1);
  }

  public virtual void UpdateBalances(ARAdjust adj, ARRegister adjddoc)
  {
    this.UpdateBalances(adj, adjddoc, (ARTran) null);
  }

  public virtual void UpdateBalances(ARAdjust adj, ARRegister adjddoc, ARTran adjdtran)
  {
    ARRegister doc = adjddoc;
    ARRegister arRegister1 = (ARRegister) ((PXSelectBase) this.ARDocument).Cache.Locate((object) doc);
    if (arRegister1 != null)
      PXCache<ARRegister>.RestoreCopy(doc, arRegister1);
    else if (this._IsIntegrityCheck)
      return;
    int? voidAdjNbr;
    if (!this._IsIntegrityCheck)
    {
      voidAdjNbr = adj.VoidAdjNbr;
      if (voidAdjNbr.HasValue)
        this.VoidOrigAdjustment(adj);
    }
    bool? nullable1;
    if (adjddoc.DocType == "SMC")
    {
      nullable1 = adj.Voided;
      if (nullable1.GetValueOrDefault())
      {
        voidAdjNbr = adj.VoidAdjNbr;
        if (voidAdjNbr.HasValue)
        {
          nullable1 = doc.Voided;
          if (nullable1.GetValueOrDefault())
            return;
          doc.Voided = new bool?(true);
          this.RaiseInvoiceEvent(doc, (SelectedEntityEvent<ARInvoice>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events>>>) (ev => ev.VoidDocument)));
          this.ProcessVoidWOTranPost(doc, ((PXGraph) this).Caches[typeof (ARAdjust)].Current as ARAdjust);
          return;
        }
      }
    }
    Decimal? nullable2 = new Decimal?(0M);
    Decimal? nullable3 = new Decimal?(0M);
    Decimal? nullable4;
    Decimal? nullable5;
    Decimal? nullable6;
    if (string.Equals(adj.AdjdDocType, doc.DocType) && string.Equals(adj.AdjdRefNbr, doc.RefNbr, StringComparison.OrdinalIgnoreCase))
    {
      Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
      nullable4 = adj.CuryAdjdDiscAmt;
      Decimal? nullable7 = curyAdjdAmt.HasValue & nullable4.HasValue ? new Decimal?(curyAdjdAmt.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      nullable5 = adj.CuryAdjdWOAmt;
      Decimal? nullable8;
      if (!(nullable7.HasValue & nullable5.HasValue))
      {
        nullable4 = new Decimal?();
        nullable8 = nullable4;
      }
      else
        nullable8 = new Decimal?(nullable7.GetValueOrDefault() + nullable5.GetValueOrDefault());
      nullable2 = nullable8;
      Decimal? adjAmt = adj.AdjAmt;
      Decimal? nullable9 = adj.AdjDiscAmt;
      nullable4 = adjAmt.HasValue & nullable9.HasValue ? new Decimal?(adjAmt.GetValueOrDefault() + nullable9.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable10 = adj.AdjWOAmt;
      Decimal? nullable11;
      if (!(nullable4.HasValue & nullable10.HasValue))
      {
        nullable9 = new Decimal?();
        nullable11 = nullable9;
      }
      else
        nullable11 = new Decimal?(nullable4.GetValueOrDefault() + nullable10.GetValueOrDefault());
      nullable5 = nullable11;
      nullable1 = adj.ReverseGainLoss;
      bool flag = false;
      Decimal? nullable12;
      if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue))
      {
        nullable10 = adj.RGOLAmt;
        if (!nullable10.HasValue)
        {
          nullable4 = new Decimal?();
          nullable12 = nullable4;
        }
        else
          nullable12 = new Decimal?(-nullable10.GetValueOrDefault());
      }
      else
        nullable12 = adj.RGOLAmt;
      nullable6 = nullable12;
      Decimal? nullable13;
      if (!(nullable5.HasValue & nullable6.HasValue))
      {
        nullable10 = new Decimal?();
        nullable13 = nullable10;
      }
      else
        nullable13 = new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault());
      nullable3 = nullable13;
      if (!this._IsIntegrityCheck)
      {
        nullable1 = adj.IsInitialApplication;
        if (!nullable1.GetValueOrDefault() && adj.AdjgDocType != "REF" && adj.AdjgDocType != "VRF" && !adj.IsSelfAdjustment())
        {
          Decimal? nullable14 = new PaymentBalanceAjuster((IPXCurrencyHelper) ((PXGraph) this.InvoiceEntryGraph).GetExtension<ARInvoiceEntry.MultiCurrency>()).AdjustWhenTheSameCuryAndRate((IAdjustment) adj, doc.CuryDocBal, doc.DocBal);
          ARAdjust arAdjust = adj;
          nullable6 = arAdjust.RGOLAmt;
          nullable10 = doc.DocBal;
          nullable4 = nullable14;
          Decimal? nullable15;
          if (!(nullable10.HasValue & nullable4.HasValue))
          {
            nullable9 = new Decimal?();
            nullable15 = nullable9;
          }
          else
            nullable15 = new Decimal?(nullable10.GetValueOrDefault() - nullable4.GetValueOrDefault());
          nullable5 = nullable15;
          Decimal? nullable16;
          if (!(nullable6.HasValue & nullable5.HasValue))
          {
            nullable4 = new Decimal?();
            nullable16 = nullable4;
          }
          else
            nullable16 = new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault());
          arAdjust.RGOLAmt = nullable16;
          doc.DocBal = nullable14;
        }
      }
      ARRegister arRegister2 = doc;
      nullable5 = arRegister2.CuryDiscBal;
      nullable6 = adj.CuryAdjdDiscAmt;
      Decimal? nullable17;
      if (!(nullable5.HasValue & nullable6.HasValue))
      {
        nullable4 = new Decimal?();
        nullable17 = nullable4;
      }
      else
        nullable17 = new Decimal?(nullable5.GetValueOrDefault() - nullable6.GetValueOrDefault());
      arRegister2.CuryDiscBal = nullable17;
      ARRegister arRegister3 = doc;
      nullable6 = arRegister3.DiscBal;
      nullable5 = adj.AdjDiscAmt;
      Decimal? nullable18;
      if (!(nullable6.HasValue & nullable5.HasValue))
      {
        nullable4 = new Decimal?();
        nullable18 = nullable4;
      }
      else
        nullable18 = new Decimal?(nullable6.GetValueOrDefault() - nullable5.GetValueOrDefault());
      arRegister3.DiscBal = nullable18;
      ARRegister arRegister4 = doc;
      nullable5 = arRegister4.CuryDiscTaken;
      nullable6 = adj.CuryAdjdDiscAmt;
      Decimal? nullable19;
      if (!(nullable5.HasValue & nullable6.HasValue))
      {
        nullable4 = new Decimal?();
        nullable19 = nullable4;
      }
      else
        nullable19 = new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault());
      arRegister4.CuryDiscTaken = nullable19;
      ARRegister arRegister5 = doc;
      nullable6 = arRegister5.DiscTaken;
      nullable5 = adj.AdjDiscAmt;
      Decimal? nullable20;
      if (!(nullable6.HasValue & nullable5.HasValue))
      {
        nullable4 = new Decimal?();
        nullable20 = nullable4;
      }
      else
        nullable20 = new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault());
      arRegister5.DiscTaken = nullable20;
      ARRegister arRegister6 = doc;
      nullable5 = arRegister6.RGOLAmt;
      nullable6 = adj.RGOLAmt;
      Decimal? nullable21;
      if (!(nullable5.HasValue & nullable6.HasValue))
      {
        nullable4 = new Decimal?();
        nullable21 = nullable4;
      }
      else
        nullable21 = new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault());
      arRegister6.RGOLAmt = nullable21;
    }
    else if (string.Equals(adj.AdjgDocType, doc.DocType) && string.Equals(adj.AdjgRefNbr, doc.RefNbr, StringComparison.OrdinalIgnoreCase))
    {
      nullable2 = adj.CuryAdjgAmt;
      nullable3 = adj.AdjAmt;
    }
    if (doc.IsPrepaymentInvoiceDocument() && doc.Status == "U")
    {
      nullable6 = nullable3;
      Decimal num = 0M;
      if (nullable6.GetValueOrDefault() < num & nullable6.HasValue && adj.AdjgDocType != "VRF" && adj.AdjgDocType != "REF")
      {
        doc.CuryDocBal = new Decimal?(0M);
        doc.DocBal = new Decimal?(0M);
        doc.PendingPayment = new bool?(true);
      }
    }
    ARRegister arRegister7 = doc;
    nullable6 = arRegister7.CuryDocBal;
    nullable5 = nullable2;
    Decimal? nullable22;
    if (!(nullable6.HasValue & nullable5.HasValue))
    {
      nullable4 = new Decimal?();
      nullable22 = nullable4;
    }
    else
      nullable22 = new Decimal?(nullable6.GetValueOrDefault() - nullable5.GetValueOrDefault());
    arRegister7.CuryDocBal = nullable22;
    ARRegister arRegister8 = doc;
    nullable5 = arRegister8.DocBal;
    nullable6 = nullable3;
    Decimal? nullable23;
    if (!(nullable5.HasValue & nullable6.HasValue))
    {
      nullable4 = new Decimal?();
      nullable23 = nullable4;
    }
    else
      nullable23 = new Decimal?(nullable5.GetValueOrDefault() - nullable6.GetValueOrDefault());
    arRegister8.DocBal = nullable23;
    nullable6 = doc.CuryDocBal;
    Decimal num1 = 0M;
    if (nullable6.GetValueOrDefault() == num1 & nullable6.HasValue)
    {
      nullable6 = doc.DocBal;
      Decimal num2 = 0M;
      if (!(nullable6.GetValueOrDefault() == num2 & nullable6.HasValue))
      {
        nullable1 = adj.ReverseGainLoss;
        bool flag = false;
        Decimal? nullable24;
        if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue))
        {
          nullable6 = doc.DocBal;
          if (!nullable6.HasValue)
          {
            nullable5 = new Decimal?();
            nullable24 = nullable5;
          }
          else
            nullable24 = new Decimal?(-nullable6.GetValueOrDefault());
        }
        else
          nullable24 = doc.DocBal;
        Decimal? nullable25 = nullable24;
        ARAdjust arAdjust = adj;
        nullable6 = arAdjust.RGOLAmt;
        nullable5 = nullable25;
        Decimal? nullable26;
        if (!(nullable6.HasValue & nullable5.HasValue))
        {
          nullable4 = new Decimal?();
          nullable26 = nullable4;
        }
        else
          nullable26 = new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault());
        arAdjust.RGOLAmt = nullable26;
        ARRegister arRegister9 = doc;
        nullable5 = arRegister9.RGOLAmt;
        nullable6 = nullable25;
        Decimal? nullable27;
        if (!(nullable5.HasValue & nullable6.HasValue))
        {
          nullable4 = new Decimal?();
          nullable27 = nullable4;
        }
        else
          nullable27 = new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault());
        arRegister9.RGOLAmt = nullable27;
      }
    }
    if (!this._IsIntegrityCheck)
    {
      DateTime? adjgDocDate = adj.AdjgDocDate;
      DateTime? docDate = adjddoc.DocDate;
      if ((adjgDocDate.HasValue & docDate.HasValue ? (adjgDocDate.GetValueOrDefault() < docDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        throw new PXException("{0} cannot be less than Document Date.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<ARPayment.adjDate>(((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache)
        });
    }
    if (!this._IsIntegrityCheck && string.Compare(adj.AdjgTranPeriodID, adjddoc.TranPeriodID) < 0)
      throw new PXException("{0} cannot be less than Document Financial Period.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<ARPayment.adjFinPeriodID>(((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache)
      });
    if (adjdtran != null && adjdtran.AreAllKeysFilled<ARTran>(((PXSelectBase) this.ARTran_TranType_RefNbr).Cache))
    {
      ARTran arTran1 = adjdtran;
      ARTran arTran2 = (ARTran) ((PXSelectBase) this.ARTran_TranType_RefNbr).Cache.Locate((object) arTran1);
      if (arTran2 != null)
        arTran1 = arTran2;
      else if (this._IsIntegrityCheck)
        return;
      ARTran arTran3 = arTran1;
      nullable6 = arTran3.CuryTranBal;
      nullable5 = nullable2;
      Decimal? nullable28;
      if (!(nullable6.HasValue & nullable5.HasValue))
      {
        nullable4 = new Decimal?();
        nullable28 = nullable4;
      }
      else
        nullable28 = new Decimal?(nullable6.GetValueOrDefault() - nullable5.GetValueOrDefault());
      arTran3.CuryTranBal = nullable28;
      ARTran arTran4 = arTran1;
      nullable5 = arTran4.TranBal;
      nullable6 = nullable3;
      Decimal? nullable29;
      if (!(nullable5.HasValue & nullable6.HasValue))
      {
        nullable4 = new Decimal?();
        nullable29 = nullable4;
      }
      else
        nullable29 = new Decimal?(nullable5.GetValueOrDefault() - nullable6.GetValueOrDefault());
      arTran4.TranBal = nullable29;
      ARTran arTran5 = arTran1;
      nullable6 = arTran5.CuryCashDiscBal;
      nullable5 = adj.CuryAdjdDiscAmt;
      Decimal? nullable30;
      if (!(nullable6.HasValue & nullable5.HasValue))
      {
        nullable4 = new Decimal?();
        nullable30 = nullable4;
      }
      else
        nullable30 = new Decimal?(nullable6.GetValueOrDefault() - nullable5.GetValueOrDefault());
      arTran5.CuryCashDiscBal = nullable30;
      ARTran arTran6 = arTran1;
      nullable5 = arTran6.CashDiscBal;
      nullable6 = adj.AdjDiscAmt;
      Decimal? nullable31;
      if (!(nullable5.HasValue & nullable6.HasValue))
      {
        nullable4 = new Decimal?();
        nullable31 = nullable4;
      }
      else
        nullable31 = new Decimal?(nullable5.GetValueOrDefault() - nullable6.GetValueOrDefault());
      arTran6.CashDiscBal = nullable31;
      nullable6 = arTran1.CuryCashDiscBal;
      Decimal num3 = 0M;
      if (nullable6.GetValueOrDefault() == num3 & nullable6.HasValue)
        arTran1.CashDiscBal = new Decimal?(0M);
      nullable6 = arTran1.CuryTranBal;
      Decimal num4 = 0M;
      if (nullable6.GetValueOrDefault() == num4 & nullable6.HasValue)
        arTran1.TranBal = new Decimal?(0M);
      nullable6 = arTran1.CuryOrigTranAmt;
      Decimal num5 = 0M;
      Sign sign1 = nullable6.GetValueOrDefault() < num5 & nullable6.HasValue ? Sign.Minus : Sign.Plus;
      if (!this._IsIntegrityCheck)
      {
        nullable5 = arTran1.CuryTranBal;
        Sign sign2 = sign1;
        Decimal? nullable32;
        if (!nullable5.HasValue)
        {
          nullable4 = new Decimal?();
          nullable32 = nullable4;
        }
        else
          nullable32 = new Decimal?(Sign.op_Multiply(nullable5.GetValueOrDefault(), sign2));
        nullable6 = nullable32;
        Decimal num6 = 0M;
        if (!(nullable6.GetValueOrDefault() < num6 & nullable6.HasValue))
        {
          nullable5 = arTran1.CuryCashDiscBal;
          Sign sign3 = sign1;
          Decimal? nullable33;
          if (!nullable5.HasValue)
          {
            nullable4 = new Decimal?();
            nullable33 = nullable4;
          }
          else
            nullable33 = new Decimal?(Sign.op_Multiply(nullable5.GetValueOrDefault(), sign3));
          nullable6 = nullable33;
          Decimal num7 = 0M;
          if (!(nullable6.GetValueOrDefault() < num7 & nullable6.HasValue))
            goto label_113;
        }
        throw new PXException(Sign.op_Equality(sign1, Sign.Plus) ? "The line balance will go negative. The document will not be released." : "The line balance will go positive. The document will not be released.");
      }
label_113:
      ((PXSelectBase<ARTran>) this.ARTran_TranType_RefNbr).Update(arTran1);
    }
    ((PXGraph) this).Caches[typeof (ARAdjust)].Update((object) adj);
    PXSelectorAttribute.StoreResult<ARRegister.curyID>(((PXSelectBase) this.ARDocument).Cache, (object) doc, (IBqlTable) CurrencyCollection.GetCurrency(doc.CuryID));
    ARRegister arRegister10 = (ARRegister) ((PXSelectBase) this.ARDocument).Cache.Update((object) doc);
  }

  public virtual void CloseInvoiceAndClearBalances(ARRegister ardoc)
  {
    ardoc.CuryDiscBal = new Decimal?(0M);
    ardoc.DiscBal = new Decimal?(0M);
    ardoc.DocBal = new Decimal?(0M);
    int num = this.HasAnyUnreleasedAdjustment(ardoc) ? 1 : 0;
    if (num == 0)
      ardoc.OpenDoc = new bool?(false);
    this.SetClosedPeriodsFromLatestApplication(ardoc);
    ((PXSelectBase) this.ARDocument).Cache.Update((object) ardoc);
    if (num != 0)
      return;
    this.RaiseInvoiceEvent(ardoc, (SelectedEntityEvent<ARInvoice>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events>>>) (ev => ev.CloseDocument)));
    this.RaisePaymentEvent(ardoc, (SelectedEntityEvent<ARPayment>) PXEntityEventBase<ARPayment>.Container<ARPayment.Events>.Select((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment.Events>>>) (ev => ev.CloseDocument)));
  }

  public virtual void OpenInvoiceAndRecoverBalances(ARRegister ardoc)
  {
    Decimal? curyDocBal = ardoc.CuryDocBal;
    Decimal? curyOrigDocAmt = ardoc.CuryOrigDocAmt;
    if (curyDocBal.GetValueOrDefault() == curyOrigDocAmt.GetValueOrDefault() & curyDocBal.HasValue == curyOrigDocAmt.HasValue)
    {
      ardoc.CuryDiscBal = ardoc.CuryOrigDiscAmt;
      ardoc.DiscBal = ardoc.OrigDiscAmt;
      ardoc.CuryDiscTaken = new Decimal?(0M);
      ardoc.DiscTaken = new Decimal?(0M);
    }
    ardoc.OpenDoc = new bool?(true);
    ardoc.ClosedDate = new DateTime?();
    ardoc.ClosedTranPeriodID = (string) null;
    ardoc.ClosedFinPeriodID = (string) null;
    ((PXSelectBase) this.ARDocument).Cache.Update((object) ardoc);
    this.RaiseInvoiceEvent(ardoc, (SelectedEntityEvent<ARInvoice>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events>>>) (ev => ev.OpenDocument)));
    this.RaisePaymentEvent(ardoc, (SelectedEntityEvent<ARPayment>) PXEntityEventBase<ARPayment>.Container<ARPayment.Events>.Select((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment.Events>>>) (ev => ev.OpenDocument)));
  }

  public virtual void CloseInvoiceAndRecoverBalances(ARRegister ardoc)
  {
    ARAdjust2 arAdjust2 = PXResultset<ARAdjust2>.op_Implicit(PXSelectBase<ARAdjust2, PXSelectGroupBy<ARAdjust2, Where<ARAdjust2.adjdDocType, Equal<Required<ARAdjust2.adjdDocType>>, And<ARAdjust2.adjdRefNbr, Equal<Required<ARAdjust2.adjdRefNbr>>, And<Where<ARAdjust2.adjgDocType, Equal<ARDocType.creditMemo>>>>>, Aggregate<GroupBy<ARAdjust2.adjdDocType, GroupBy<ARAdjust2.adjdRefNbr, Sum<ARAdjust2.curyAdjdAmt, Sum<ARAdjust2.adjAmt>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ardoc.DocType,
      (object) ardoc.RefNbr
    }));
    Decimal? nullable1;
    ref Decimal? local1 = ref nullable1;
    Decimal? nullable2 = (Decimal?) arAdjust2?.AdjAmt;
    Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
    local1 = new Decimal?(valueOrDefault1);
    Decimal? nullable3;
    ref Decimal? local2 = ref nullable3;
    Decimal? nullable4;
    if (arAdjust2 == null)
    {
      nullable2 = new Decimal?();
      nullable4 = nullable2;
    }
    else
      nullable4 = arAdjust2.CuryAdjdAmt;
    nullable2 = nullable4;
    Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
    local2 = new Decimal?(valueOrDefault2);
    ARRegister arRegister1 = ardoc;
    nullable2 = ardoc.CuryOrigDocAmt;
    Decimal? nullable5 = nullable3;
    Decimal? nullable6 = nullable2.HasValue & nullable5.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
    arRegister1.CuryDocBal = nullable6;
    ARRegister arRegister2 = ardoc;
    nullable5 = ardoc.OrigDocAmt;
    nullable2 = nullable1;
    Decimal? nullable7 = nullable5.HasValue & nullable2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    arRegister2.DocBal = nullable7;
    ardoc.CuryDiscBal = ardoc.CuryOrigDiscAmt;
    ardoc.DiscBal = ardoc.OrigDiscAmt;
    ardoc.CuryDiscTaken = new Decimal?(0M);
    ardoc.DiscTaken = new Decimal?(0M);
    ardoc.OpenDoc = new bool?(true);
    if (ardoc.PendingPayment.GetValueOrDefault())
    {
      nullable2 = ardoc.DocBal;
      Decimal num1 = 0M;
      if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
      {
        nullable2 = ardoc.CuryDocBal;
        Decimal num2 = 0M;
        if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
          goto label_7;
      }
      else
        goto label_7;
    }
    this.SetClosedPeriodsFromLatestApplication(ardoc);
label_7:
    ((PXSelectBase) this.ARDocument).Cache.Update((object) ardoc);
    nullable2 = ardoc.DocBal;
    Decimal num3 = 0M;
    if (nullable2.GetValueOrDefault() == num3 & nullable2.HasValue)
    {
      nullable2 = ardoc.CuryDocBal;
      Decimal num4 = 0M;
      if (nullable2.GetValueOrDefault() == num4 & nullable2.HasValue)
        goto label_10;
    }
    if (!ardoc.Voided.GetValueOrDefault())
    {
      this.RaiseInvoiceEvent(ardoc, (SelectedEntityEvent<ARInvoice>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events>>>) (ev => ev.CloseDocument)));
      this.RaisePaymentEvent(ardoc, (SelectedEntityEvent<ARPayment>) PXEntityEventBase<ARPayment>.Container<ARPayment.Events>.Select((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment.Events>>>) (ev => ev.CloseDocument)));
      return;
    }
label_10:
    foreach (PXResult<ARAdjust> pxResult in PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<ARAdjust.released, NotEqual<True>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ardoc.DocType,
      (object) ardoc.RefNbr
    }))
      ((PXGraph) this).Caches[typeof (ARAdjust)].Delete((object) PXResult<ARAdjust>.op_Implicit(pxResult));
    this.RaiseInvoiceEvent(ardoc, (SelectedEntityEvent<ARInvoice>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events>>>) (ev => ev.VoidDocument)));
  }

  public virtual void VerifyOriginalRetainageDocumentBalanceAndClose(ARRegister origRetainageDoc)
  {
    if (origRetainageDoc == null)
      return;
    if (this.IsFullyProcessedOriginalRetainageDocument(origRetainageDoc))
      this.CloseInvoiceAndClearBalances(origRetainageDoc);
    else
      this.OpenInvoiceAndRecoverBalances(origRetainageDoc);
    ((PXSelectBase) this.ARDocument).Cache.Update((object) origRetainageDoc);
  }

  private void UpdateVoidedCheck(ARRegister voidcheck)
  {
    foreach (string originalDocumentType in voidcheck.PossibleOriginalDocumentTypes())
    {
      foreach (PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer> pxResult1 in ((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Select(new object[3]
      {
        (object) originalDocumentType,
        (object) voidcheck.RefNbr,
        (object) voidcheck.CustomerID
      }))
      {
        ARRegister doc = (ARRegister) PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer>.op_Implicit(pxResult1);
        bool? voided = doc.Voided;
        ARRegister arRegister = (ARRegister) ((PXSelectBase) this.ARDocument).Cache.Locate((object) doc);
        if (arRegister != null)
          PXCache<ARRegister>.RestoreCopy(doc, arRegister);
        doc.Voided = new bool?(true);
        doc.OpenDoc = new bool?(false);
        doc.Hold = new bool?(false);
        doc.CuryDocBal = new Decimal?(0M);
        doc.DocBal = new Decimal?(0M);
        this.SetClosedPeriodsFromLatestApplication(doc);
        ((PXSelectBase) this.ARDocument).Cache.Update((object) doc);
        this.RaisePaymentEvent(doc, (SelectedEntityEvent<ARPayment>) PXEntityEventBase<ARPayment>.Container<ARPayment.Events>.Select((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment.Events>>>) (ev => ev.VoidDocument)));
        bool? nullable = voided;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer>.op_Implicit(pxResult1).PostponeVoidedFlag = new bool?(true);
        Guid? noteId1 = voidcheck.NoteID;
        Guid? noteId2 = doc.NoteID;
        if ((noteId1.HasValue == noteId2.HasValue ? (noteId1.HasValue ? (noteId1.GetValueOrDefault() == noteId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          ((PXSelectBase) this.ARDocument).Cache.RestoreCopy((object) voidcheck, (object) doc);
        if (!this._IsIntegrityCheck)
        {
          foreach (PXResult<ARAdjust> pxResult2 in PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<ARAdjust.released, NotEqual<True>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) doc.DocType,
            (object) doc.RefNbr
          }))
            ((PXGraph) this).Caches[typeof (ARAdjust)].Delete((object) PXResult<ARAdjust>.op_Implicit(pxResult2));
        }
      }
    }
  }

  private void VerifyVoidCheckNumberMatchesOriginalPayment(ARPayment voidcheck)
  {
    foreach (string originalDocumentType in voidcheck.PossibleOriginalDocumentTypes())
    {
      foreach (PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer> pxResult in ((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Select(new object[3]
      {
        (object) originalDocumentType,
        (object) voidcheck.RefNbr,
        (object) voidcheck.CustomerID
      }))
      {
        ARPayment arPayment = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer>.op_Implicit(pxResult);
        if (!this._IsIntegrityCheck && !string.Equals(voidcheck.ExtRefNbr, arPayment.ExtRefNbr, StringComparison.OrdinalIgnoreCase))
        {
          if (EnumerableExtensions.IsNotIn<string>(PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) voidcheck.PaymentMethodID
          })).PaymentType, "CCD", "EFT", "POS"))
            throw new PXException("Void Payment must have the same Reference Number as the voided payment.");
        }
      }
    }
  }

  /// <summary>
  /// Ensures that no unreleased voiding document exists for the specified payment.
  /// (If the applications of the voided and the voiding document are not
  /// synchronized, it can lead to a balance discrepancy, see AC-78131).
  /// </summary>
  public static void EnsureNoUnreleasedVoidPaymentExists(
    PXGraph selectGraph,
    ARRegister payment,
    string actionDescription)
  {
    ARRegister arRegister = HasUnreleasedVoidPayment<ARRegister.docType, ARRegister.refNbr>.Select(selectGraph, payment);
    if (arRegister != null)
      throw new PXException("The {0} {1} cannot be {2} because an unreleased {3} document exists for this {4}. To proceed, delete or release the {5} {6} document.", new object[7]
      {
        (object) PXLocalizer.Localize(GetLabel.For<ARDocType>(payment.DocType)),
        (object) payment.RefNbr,
        (object) PXLocalizer.Localize(actionDescription),
        (object) PXLocalizer.Localize(GetLabel.For<ARDocType>(arRegister.DocType)),
        (object) PXLocalizer.Localize(GetLabel.For<ARDocType>(payment.DocType)),
        (object) PXLocalizer.Localize(GetLabel.For<ARDocType>(arRegister.DocType)),
        (object) arRegister.RefNbr
      });
  }

  public virtual void ProcessPayment(
    JournalEntry je,
    ARRegister doc,
    PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount> res)
  {
    ARPayment arPayment = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(res);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(res);
    PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(res);
    Customer customer = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(res);
    PX.Objects.CA.CashAccount cashAccount = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(res);
    ARReleaseProcess.EnsureNoUnreleasedVoidPaymentExists((PXGraph) this, (ARRegister) arPayment, "released");
    PXResultset<CustomerClass>.op_Implicit(PXSelectBase<CustomerClass, PXSelectJoin<CustomerClass, InnerJoin<ARSetup, On<ARSetup.dfltCustomerClassID, Equal<CustomerClass.customerClassID>>>>.Config>.Select((PXGraph) this, (object[]) null));
    bool flag1 = doc.DocType == "CSL" || doc.DocType == "RCS";
    bool? nullable1;
    if (!doc.Released.GetValueOrDefault())
    {
      PXCache<ARRegister>.RestoreCopy((ARRegister) arPayment, doc);
      doc.CuryDocBal = doc.CuryOrigDocAmt;
      doc.DocBal = doc.OrigDocAmt;
      doc.RGOLAmt = new Decimal?(0M);
      if (doc.DocType != "SMB")
      {
        bool isDebit = arPayment.DrCr == "D";
        PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
        glTran1.SummPost = new bool?(true);
        glTran1.BranchID = cashAccount.BranchID;
        glTran1.AccountID = cashAccount.AccountID;
        glTran1.SubID = cashAccount.SubID;
        glTran1.CuryDebitAmt = isDebit ? arPayment.CuryOrigDocAmt : new Decimal?(0M);
        glTran1.DebitAmt = isDebit ? arPayment.OrigDocAmt : new Decimal?(0M);
        glTran1.CuryCreditAmt = isDebit ? new Decimal?(0M) : arPayment.CuryOrigDocAmt;
        glTran1.CreditAmt = isDebit ? new Decimal?(0M) : arPayment.OrigDocAmt;
        glTran1.TranType = arPayment.DocType;
        glTran1.TranClass = arPayment.DocClass;
        glTran1.RefNbr = arPayment.RefNbr;
        glTran1.TranDesc = arPayment.DocDesc;
        glTran1.TranDate = arPayment.DocDate;
        FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran1, arPayment.TranPeriodID);
        glTran1.CuryInfoID = currencyInfo.CuryInfoID;
        glTran1.Released = new bool?(true);
        glTran1.CATranID = arPayment.CATranID;
        glTran1.ReferenceID = arPayment.CustomerID;
        glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
        this.InsertPaymentTransaction(je, glTran1, new ARReleaseProcess.GLTranInsertionContext()
        {
          ARRegisterRecord = doc
        });
        PX.Objects.GL.GLTran glTran2 = new PX.Objects.GL.GLTran();
        glTran2.SummPost = new bool?(true);
        if (!ARPaymentType.CanHaveBalance(arPayment.DocType))
          glTran2.ZeroPost = new bool?(false);
        glTran2.BranchID = arPayment.BranchID;
        glTran2.AccountID = arPayment.ARAccountID;
        glTran2.ReclassificationProhibited = new bool?(true);
        glTran2.SubID = arPayment.ARSubID;
        glTran2.CuryDebitAmt = arPayment.DrCr == "D" ? new Decimal?(0M) : arPayment.CuryOrigDocAmt;
        glTran2.DebitAmt = arPayment.DrCr == "D" ? new Decimal?(0M) : arPayment.OrigDocAmt;
        glTran2.CuryCreditAmt = arPayment.DrCr == "D" ? arPayment.CuryOrigDocAmt : new Decimal?(0M);
        glTran2.CreditAmt = arPayment.DrCr == "D" ? arPayment.OrigDocAmt : new Decimal?(0M);
        glTran2.TranType = arPayment.DocType;
        glTran2.TranClass = "P";
        glTran2.RefNbr = arPayment.RefNbr;
        glTran2.TranDesc = arPayment.DocDesc;
        glTran2.TranDate = arPayment.DocDate;
        FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran2, arPayment.TranPeriodID);
        glTran2.CuryInfoID = currencyInfo.CuryInfoID;
        glTran2.Released = new bool?(true);
        glTran2.ReferenceID = arPayment.CustomerID;
        glTran2.ProjectID = ProjectDefaultAttribute.NonProject();
        this.UpdateHistory(glTran2, customer);
        this.UpdateHistory(glTran2, customer, currencyInfo);
        this.InsertPaymentTransaction(je, glTran2, new ARReleaseProcess.GLTranInsertionContext()
        {
          ARRegisterRecord = doc
        });
        if (this.IsMigratedDocumentForProcessing(doc))
        {
          this.ProcessMigratedDocument(je, glTran2, doc, isDebit, customer, currencyInfo);
          doc = (ARRegister) ((PXSelectBase) this.ARDocument).Cache.Locate((object) doc) ?? doc;
          PXCache<ARRegister>.RestoreCopy((ARRegister) arPayment, doc);
        }
      }
      foreach (PXResult<ARPaymentChargeTran> pxResult in ((PXSelectBase<ARPaymentChargeTran>) this.ARPaymentChargeTran_DocType_RefNbr).Select(new object[2]
      {
        (object) doc.DocType,
        (object) doc.RefNbr
      }))
      {
        ARPaymentChargeTran paymentChargeTran = PXResult<ARPaymentChargeTran>.op_Implicit(pxResult);
        bool flag2 = paymentChargeTran.GetCASign() == 1;
        PX.Objects.GL.GLTran glTran3 = new PX.Objects.GL.GLTran();
        glTran3.SummPost = new bool?(true);
        glTran3.BranchID = cashAccount.BranchID;
        glTran3.AccountID = cashAccount.AccountID;
        glTran3.SubID = cashAccount.SubID;
        glTran3.CuryDebitAmt = flag2 ? paymentChargeTran.CuryTranAmt : new Decimal?(0M);
        glTran3.DebitAmt = flag2 ? paymentChargeTran.TranAmt : new Decimal?(0M);
        glTran3.CuryCreditAmt = flag2 ? new Decimal?(0M) : paymentChargeTran.CuryTranAmt;
        glTran3.CreditAmt = flag2 ? new Decimal?(0M) : paymentChargeTran.TranAmt;
        glTran3.TranType = paymentChargeTran.DocType;
        glTran3.TranClass = arPayment.DocClass;
        glTran3.RefNbr = paymentChargeTran.RefNbr;
        glTran3.TranLineNbr = paymentChargeTran.LineNbr;
        glTran3.TranDesc = paymentChargeTran.Consolidate.GetValueOrDefault() ? arPayment.DocDesc : paymentChargeTran.TranDesc;
        glTran3.TranDate = paymentChargeTran.TranDate;
        FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran3, glTran3.TranPeriodID);
        glTran3.CuryInfoID = currencyInfo.CuryInfoID;
        glTran3.Released = new bool?(true);
        glTran3.CATranID = paymentChargeTran.CashTranID ?? arPayment.CATranID;
        glTran3.ReferenceID = arPayment.CustomerID;
        this.InsertPaymentChargeTransaction(je, glTran3, new ARReleaseProcess.GLTranInsertionContext()
        {
          ARRegisterRecord = doc,
          ARPaymentChargeTranRecord = paymentChargeTran
        });
        PX.Objects.GL.GLTran glTran4 = new PX.Objects.GL.GLTran();
        glTran4.SummPost = new bool?(true);
        glTran4.ZeroPost = new bool?(false);
        glTran4.BranchID = arPayment.BranchID;
        glTran4.AccountID = paymentChargeTran.AccountID;
        glTran4.SubID = paymentChargeTran.SubID;
        glTran4.ProjectID = paymentChargeTran.ProjectID;
        glTran4.TaskID = paymentChargeTran.TaskID;
        glTran4.CostCodeID = paymentChargeTran.CostCodeID;
        glTran4.CuryDebitAmt = flag2 ? new Decimal?(0M) : paymentChargeTran.CuryTranAmt;
        glTran4.DebitAmt = flag2 ? new Decimal?(0M) : paymentChargeTran.TranAmt;
        glTran4.CuryCreditAmt = flag2 ? paymentChargeTran.CuryTranAmt : new Decimal?(0M);
        glTran4.CreditAmt = flag2 ? paymentChargeTran.TranAmt : new Decimal?(0M);
        glTran4.TranType = paymentChargeTran.DocType;
        glTran4.TranClass = "U";
        glTran4.RefNbr = paymentChargeTran.RefNbr;
        glTran4.TranLineNbr = paymentChargeTran.LineNbr;
        glTran4.TranDesc = paymentChargeTran.TranDesc;
        glTran4.TranDate = paymentChargeTran.TranDate;
        FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran4, glTran4.TranPeriodID);
        glTran4.CuryInfoID = currencyInfo.CuryInfoID;
        glTran4.Released = new bool?(true);
        glTran4.ReferenceID = arPayment.CustomerID;
        this.InsertPaymentChargeTransaction(je, glTran4, new ARReleaseProcess.GLTranInsertionContext()
        {
          ARRegisterRecord = doc,
          ARPaymentChargeTranRecord = paymentChargeTran
        });
        paymentChargeTran.Released = new bool?(true);
        ((PXSelectBase<ARPaymentChargeTran>) this.ARPaymentChargeTran_DocType_RefNbr).Update(paymentChargeTran);
      }
      this.ProcessOriginTranPost(arPayment);
      doc.Voided = new bool?(false);
      doc.OpenDoc = new bool?(true);
      doc.ClosedDate = new DateTime?();
      doc.ClosedFinPeriodID = (string) null;
      doc.ClosedTranPeriodID = (string) null;
      this.RaisePaymentEvent(doc, (SelectedEntityEvent<ARPayment>) PXEntityEventBase<ARPayment>.Container<ARPayment.Events>.Select((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment.Events>>>) (ev => ev.OpenDocument)));
      nullable1 = arPayment.VoidAppl;
      if (nullable1.GetValueOrDefault())
      {
        doc.OpenDoc = new bool?(false);
        doc.ClosedDate = doc.DocDate;
        doc.ClosedFinPeriodID = doc.FinPeriodID;
        doc.ClosedTranPeriodID = doc.TranPeriodID;
        this.SetClosedPeriodsFromLatestApplication(doc);
        this.RaiseInvoiceEvent(doc, (SelectedEntityEvent<ARInvoice>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events>>>) (ev => ev.CloseDocument)));
        this.VerifyVoidCheckNumberMatchesOriginalPayment(arPayment);
      }
    }
    if (flag1)
    {
      if (!this._IsIntegrityCheck)
        this.CreateSelfApplicationForDocument(doc);
      ARRegister arRegister1 = doc;
      Decimal? curyDocBal = arRegister1.CuryDocBal;
      Decimal? nullable2 = doc.CuryOrigDiscAmt;
      arRegister1.CuryDocBal = curyDocBal.HasValue & nullable2.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      ARRegister arRegister2 = doc;
      nullable2 = arRegister2.DocBal;
      Decimal? origDiscAmt = doc.OrigDiscAmt;
      arRegister2.DocBal = nullable2.HasValue & origDiscAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + origDiscAmt.GetValueOrDefault()) : new Decimal?();
      doc.ClosedDate = doc.DocDate;
      doc.ClosedFinPeriodID = doc.FinPeriodID;
      doc.ClosedTranPeriodID = doc.TranPeriodID;
    }
    doc.PendingProcessing = new bool?(false);
    doc.Released = new bool?(true);
    nullable1 = arPayment.Released;
    bool flag3 = false;
    if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue)
      arPayment.PostponeReleasedFlag = new bool?(true);
    if (!arPayment.IsPrepaymentInvoiceDocument())
      return;
    nullable1 = arPayment.PendingPayment;
    bool flag4 = false;
    if (!(nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue))
      return;
    nullable1 = arPayment.Released;
    bool flag5 = false;
    if (!(nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue))
      return;
    arPayment.PostponePendingPaymentFlag = new bool?(true);
  }

  /// <summary>
  /// The method to verify invoice balances and close it if needed.
  /// This verification should be called after
  /// release process of invoice applications.
  /// </summary>
  public virtual void VerifyAdjustedDocumentAndClose(ARRegister adjddoc)
  {
    ARRegister arRegister1 = adjddoc;
    ARRegister arRegister2 = (ARRegister) ((PXSelectBase) this.ARDocument).Cache.Locate((object) arRegister1);
    if (arRegister2 != null)
      PXCache<ARRegister>.RestoreCopy(arRegister1, arRegister2);
    else if (this._IsIntegrityCheck)
      return;
    Decimal? curyDiscBal = arRegister1.CuryDiscBal;
    Decimal num1 = 0M;
    if (curyDiscBal.GetValueOrDefault() == num1 & curyDiscBal.HasValue)
      arRegister1.DiscBal = new Decimal?(0M);
    if (!this._IsIntegrityCheck)
    {
      Decimal? curyDocBal1 = arRegister1.CuryDocBal;
      Decimal num2 = 0M;
      Decimal? nullable;
      if (!(curyDocBal1.GetValueOrDefault() < num2 & curyDocBal1.HasValue))
      {
        Decimal? curyDocBal2 = arRegister1.CuryDocBal;
        nullable = arRegister1.CuryOrigDocAmt;
        if (!(curyDocBal2.GetValueOrDefault() > nullable.GetValueOrDefault() & curyDocBal2.HasValue & nullable.HasValue))
          goto label_10;
      }
      string localizedLabel = PXStringListAttribute.GetLocalizedLabel<ARRegister.docType>(((PXSelectBase) this.ARDocument).Cache, (object) arRegister1);
      nullable = arRegister1.CuryDocBal;
      Decimal num3 = 0M;
      throw new PXException(nullable.GetValueOrDefault() < num3 & nullable.HasValue ? "The balance of {0}: {1} will go negative. The document will not be released." : "The balance of {0}: {1} will exceed the document's total amount. The document will not be released.", new object[2]
      {
        (object) localizedLabel,
        (object) arRegister1.RefNbr
      });
    }
label_10:
    bool? nullable1;
    if (arRegister1.IsOriginalRetainageDocument())
    {
      if (this.IsFullyProcessedOriginalRetainageDocument(arRegister1))
        this.CloseInvoiceAndClearBalances(arRegister1);
      else
        this.OpenInvoiceAndRecoverBalances(arRegister1);
    }
    else
    {
      if (arRegister1.IsPrepaymentInvoiceDocument())
      {
        nullable1 = arRegister1.PendingPayment;
        if (nullable1.GetValueOrDefault() && arRegister1.HasZeroBalance<ARRegister.curyDocBal, ARTran.curyTranBal>((PXGraph) this))
        {
          this.CloseInvoiceAndRecoverBalances(arRegister1);
          goto label_20;
        }
      }
      if (arRegister1.HasZeroBalance<ARRegister.curyDocBal, ARTran.curyTranBal>((PXGraph) this))
        this.CloseInvoiceAndClearBalances(arRegister1);
      else
        this.OpenInvoiceAndRecoverBalances(arRegister1);
    }
label_20:
    nullable1 = arRegister1.IsRetainageDocument;
    if (!nullable1.GetValueOrDefault())
      return;
    nullable1 = arRegister1.PaymentsByLinesAllowed;
    if (nullable1.GetValueOrDefault())
    {
      IEqualityComparer<ARTran> comparer = (IEqualityComparer<ARTran>) new FieldSubsetEqualityComparer<ARTran>(((PXSelectBase) this.ARTran_TranType_RefNbr).Cache, new System.Type[3]
      {
        typeof (ARTran.tranType),
        typeof (ARTran.refNbr),
        typeof (ARTran.lineNbr)
      });
      foreach (IGrouping<ARTran, PXResult<ARTran>> grouping in ((IEnumerable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) this.ARTran_TranType_RefNbr).Select(new object[2]
      {
        (object) arRegister1.DocType,
        (object) arRegister1.RefNbr
      })).AsEnumerable<PXResult<ARTran>>().GroupBy<PXResult<ARTran>, ARTran>((Func<PXResult<ARTran>, ARTran>) (row => PXResult<ARTran>.op_Implicit(row)), comparer))
        this.VerifyOriginalRetainageDocumentBalanceAndClose(this.GetOriginalRetainageDocument(grouping.Key));
    }
    else
      this.VerifyOriginalRetainageDocumentBalanceAndClose(this.GetOriginalRetainageDocument(arRegister1));
  }

  /// <summary>
  /// The method to verify payment balances and close it if needed.
  /// This verification should be called after
  /// release process of payment and applications.
  /// </summary>
  public virtual void VerifyPaymentRoundAndClose(
    JournalEntry je,
    ARRegister paymentRegister,
    ARPayment payment,
    Customer paymentCustomer,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    PX.Objects.CM.Extensions.Currency paycury,
    Tuple<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo> lastAdjustment)
  {
    ARAdjust prev_adj = lastAdjustment.Item1;
    PX.Objects.CM.Extensions.CurrencyInfo prev_info = lastAdjustment.Item2;
    Decimal? nullable;
    if (!this._IsIntegrityCheck && !payment.VoidAppl.GetValueOrDefault())
    {
      Decimal? curyDocBal = paymentRegister.CuryDocBal;
      Decimal num1 = 0M;
      if (!(curyDocBal.GetValueOrDefault() < num1 & curyDocBal.HasValue))
      {
        curyDocBal = paymentRegister.CuryDocBal;
        nullable = paymentRegister.CuryOrigDocAmt;
        if (!(curyDocBal.GetValueOrDefault() > nullable.GetValueOrDefault() & curyDocBal.HasValue & nullable.HasValue))
          goto label_4;
      }
      string localizedLabel = PXStringListAttribute.GetLocalizedLabel<ARRegister.docType>(((PXSelectBase) this.ARDocument).Cache, (object) paymentRegister);
      nullable = paymentRegister.CuryDocBal;
      Decimal num2 = 0M;
      throw new PXException(nullable.GetValueOrDefault() < num2 & nullable.HasValue ? "The balance of {0}: {1} will go negative. The document will not be released." : "The balance of {0}: {1} will exceed the document's total amount. The document will not be released.", new object[2]
      {
        (object) localizedLabel,
        (object) paymentRegister.RefNbr
      });
    }
label_4:
    nullable = paymentRegister.CuryDocBal;
    Decimal num3 = 0M;
    int num4;
    if (nullable.GetValueOrDefault() > num3 & nullable.HasValue)
    {
      nullable = paymentRegister.DocBal;
      Decimal num5 = 0M;
      num4 = nullable.GetValueOrDefault() < num5 & nullable.HasValue ? 1 : 0;
    }
    else
      num4 = 0;
    bool flag1 = num4 != 0;
    if (prev_adj.AdjdRefNbr != null)
    {
      nullable = paymentRegister.CuryDocBal;
      Decimal num6 = 0M;
      int num7;
      if (nullable.GetValueOrDefault() == num6 & nullable.HasValue)
      {
        nullable = paymentRegister.DocBal;
        Decimal num8 = 0M;
        num7 = !(nullable.GetValueOrDefault() == num8 & nullable.HasValue) ? 1 : 0;
      }
      else
        num7 = 0;
      int num9 = flag1 ? 1 : 0;
      if ((num7 | num9) != 0)
        this.ProcessAdjustmentsRounding(je, paymentRegister, prev_adj, payment, paymentCustomer, paycury, prev_info, new_info, paymentRegister.DocBal, prev_adj.ReverseGainLoss);
    }
    bool flag2 = paymentRegister.IsOriginalRetainageDocument() ? this.IsFullyProcessedOriginalRetainageDocument(paymentRegister) : paymentRegister.HasZeroBalance<ARRegister.curyDocBal, ARTran.curyTranBal>((PXGraph) this);
    bool flag3 = payment.VoidAppl.GetValueOrDefault() || payment.SelfVoidingDoc.GetValueOrDefault() && prev_adj.Voided.GetValueOrDefault();
    bool flag4 = prev_adj.AdjdRefNbr != null;
    if ((((!paymentRegister.IsOriginalRetainageDocument() ? 1 : (paymentRegister.DocType != "CRM" ? 1 : 0)) | (flag4 ? 1 : 0)) != 0 || !paymentRegister.IsRetainageReversing.GetValueOrDefault()) && flag2 | flag3)
    {
      paymentRegister.CuryDocBal = new Decimal?(0M);
      paymentRegister.DocBal = new Decimal?(0M);
      paymentRegister.OpenDoc = new bool?(false);
      this.SetClosedPeriodsFromLatestApplication(paymentRegister);
      if (flag3 && paymentRegister.DocType != "RCS")
        this.UpdateVoidedCheck(paymentRegister);
      if (!payment.VoidAppl.GetValueOrDefault())
        this.DeactivateOneTimeCustomerIfAllDocsIsClosed(paymentCustomer);
      this.RaiseInvoiceEvent(paymentRegister, (SelectedEntityEvent<ARInvoice>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events>>>) (e => e.CloseDocument)));
      this.RaisePaymentEvent(paymentRegister, (SelectedEntityEvent<ARPayment>) PXEntityEventBase<ARPayment>.Container<ARPayment.Events>.Select((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment.Events>>>) (e => e.CloseDocument)));
    }
    else
    {
      if (flag1)
        paymentRegister.DocBal = new Decimal?(0M);
      paymentRegister.OpenDoc = new bool?(true);
      paymentRegister.ClosedDate = new DateTime?();
      paymentRegister.ClosedTranPeriodID = (string) null;
      paymentRegister.ClosedFinPeriodID = (string) null;
      this.RaiseInvoiceEvent(paymentRegister, (SelectedEntityEvent<ARInvoice>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events>>>) (e => e.OpenDocument)));
      this.RaisePaymentEvent(paymentRegister, (SelectedEntityEvent<ARPayment>) PXEntityEventBase<ARPayment>.Container<ARPayment.Events>.Select((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment.Events>>>) (e => e.OpenDocument)));
    }
    if (!paymentRegister.IsRetainageDocument.GetValueOrDefault())
      return;
    this.VerifyOriginalRetainageDocumentBalanceAndClose(this.GetOriginalRetainageDocument(paymentRegister));
  }

  protected void DeactivateOneTimeCustomerIfAllDocsIsClosed(Customer customer)
  {
    if (customer.Status != "T")
      return;
    if (PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.customerID, Equal<Required<ARRegister.customerID>>, And<ARRegister.released, Equal<boolTrue>, And<ARRegister.openDoc, Equal<boolTrue>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) customer.BAccountID
    })) != null)
      return;
    customer.Status = "I";
    ((PXGraph) this).Caches[typeof (Customer)].Update((object) customer);
    ((PXGraph) this).Caches[typeof (Customer)].Persist((PXDBOperation) 1);
    ((PXGraph) this).Caches[typeof (Customer)].Persisted(false);
  }

  protected virtual PX.Objects.CM.CurrencyInfo GetCurrencyInfoCopyForGL(
    JournalEntry je,
    PX.Objects.CM.Extensions.CurrencyInfo info,
    bool? baseCalc = null)
  {
    PX.Objects.CM.CurrencyInfo cm = info.GetCM();
    cm.CuryInfoID = new long?();
    cm.ModuleCode = "GL";
    cm.BaseCalc = baseCalc ?? cm.BaseCalc;
    return ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Insert(cm) ?? cm;
  }

  private ARStatementDetail GetRelatedStatementDetail(ARTranPost tranPost)
  {
    if (!EnumerableExtensions.IsIn<string>(tranPost.Type, "S", "D", "G"))
      return (ARStatementDetail) null;
    return GraphHelper.RowCast<ARStatementDetail>((IEnumerable) ((PXSelectBase<ARStatementDetail>) this.StatementDetailsView).Select(new object[2]
    {
      (object) tranPost.DocType,
      (object) tranPost.RefNbr
    })).Where<ARStatementDetail>((Func<ARStatementDetail, bool>) (_ =>
    {
      Guid? refNoteId1 = _.RefNoteID;
      Guid? refNoteId2 = tranPost.RefNoteID;
      if (refNoteId1.HasValue != refNoteId2.HasValue)
        return false;
      return !refNoteId1.HasValue || refNoteId1.GetValueOrDefault() == refNoteId2.GetValueOrDefault();
    })).OrderBy<ARStatementDetail, DateTime?>((Func<ARStatementDetail, DateTime?>) (_ => _.StatementDate)).FirstOrDefault<ARStatementDetail>();
  }

  protected virtual void _(PX.Data.Events.RowInserting<ARTranPost> e)
  {
    if (!this.IsIntegrityCheck)
      return;
    e.Row.StatementDate = (DateTime?) this.GetRelatedStatementDetail(e.Row)?.StatementDate;
  }

  protected virtual void _(PX.Data.Events.RowPersisted<ARTranPost> e)
  {
    if (!this.IsIntegrityCheck)
      return;
    ARStatementDetail relatedStatementDetail = this.GetRelatedStatementDetail(e.Row);
    if (relatedStatementDetail == null)
      return;
    relatedStatementDetail.TranPostID = e.Row.ID;
    ((PXSelectBase<ARStatementDetail>) this.StatementDetailsView).Update(relatedStatementDetail);
  }

  protected virtual Tuple<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo> ProcessAdjustments(
    JournalEntry je,
    PXResultset<ARAdjust> adjustments,
    ARRegister paymentRegister,
    ARPayment payment,
    Customer paymentCustomer,
    PX.Objects.CM.CurrencyInfo new_info,
    PX.Objects.CM.Extensions.Currency paycury)
  {
    ARAdjust arAdjust1 = new ARAdjust();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = new PX.Objects.CM.Extensions.CurrencyInfo();
    this.tranPostRetainagePayments = new Dictionary<ARReleaseProcess.ARTranPostKey, ARTranPost>();
    foreach (IGrouping<object, PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>> grouping in ((IEnumerable<PXResult<ARAdjust>>) adjustments).AsEnumerable<PXResult<ARAdjust>>().Where<PXResult<ARAdjust>>((Func<PXResult<ARAdjust>, bool>) (res => !PXResult<ARAdjust>.op_Implicit(res).IsInitialApplication.GetValueOrDefault())).Cast<PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>>().GroupBy<PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>, object>((Func<PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>, object>) (res => !PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>.op_Implicit(res).AreAllKeysFilled<ARRegister>(((PXSelectBase) this.ARDocument).Cache) ? (object) PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>.op_Implicit(res) : (object) PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>.op_Implicit(res)), PXCacheEx.GetComparer(((PXSelectBase) this.ARDocument).Cache)))
    {
      ARRegister key = (ARRegister) grouping.Key;
      ARRegister arRegister1 = (ARRegister) null;
      bool flag1 = false;
      foreach (PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran> pxResult1 in (IEnumerable<PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>>) grouping)
      {
        ARAdjust adj = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>.op_Implicit(pxResult1);
        ARRegister arRegister2 = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>.op_Implicit(pxResult1);
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>.op_Implicit(pxResult1);
        PX.Objects.CM.Extensions.Currency cury = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>.op_Implicit(pxResult1);
        ARInvoice arInvoice1 = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>.op_Implicit(pxResult1);
        ARPayment arPayment = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>.op_Implicit(pxResult1);
        PX.Objects.CM.Extensions.CurrencyInfo info = PX.Objects.Common.Utilities.Clone<CurrencyInfo2, PX.Objects.CM.Extensions.CurrencyInfo>((PXGraph) this, PXResult.Unwrap<CurrencyInfo2>((object) pxResult1));
        ARTran arTran = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>.op_Implicit(pxResult1);
        bool? nullable1 = arRegister2.Released;
        if (nullable1.GetValueOrDefault() || adj.IsSelfAdjustment())
        {
          if (arInvoice1 != null && arInvoice1.RefNbr != null)
          {
            PXCache<ARRegister>.RestoreCopy((ARRegister) arInvoice1, arRegister2);
            arRegister1 = (ARRegister) arInvoice1;
          }
          else if (arPayment != null && arPayment.RefNbr != null)
          {
            PXCache<ARRegister>.RestoreCopy((ARRegister) arPayment, arRegister2);
            arRegister1 = (ARRegister) arPayment;
          }
          PXCache<ARRegister>.StoreOriginal((PXGraph) this, (ARRegister) arInvoice1);
          PXCache<ARAdjust>.StoreOriginal((PXGraph) this, adj);
          PXCache<ARTran>.StoreOriginal((PXGraph) this, arTran);
          ((PXGraph) this).GetExtension<ARReleaseProcess.MultiCurrency>().StoreResult(currencyInfo2);
          ((PXGraph) this).GetExtension<ARReleaseProcess.MultiCurrency>().StoreResult(info);
          ((PXGraph) this._ie)?.GetExtension<ARInvoiceEntry.MultiCurrency>().StoreResult(currencyInfo2);
          ((PXGraph) this._ie)?.GetExtension<ARInvoiceEntry.MultiCurrency>().StoreResult(info);
          PXParentAttribute.SetParent((PXCache) GraphHelper.Caches<ARAdjust>((PXGraph) this), (object) adj, typeof (ARRegister), (object) arPayment);
          if (adj.AdjdDocType == "SMC" && string.CompareOrdinal(payment.AdjFinPeriodID, arInvoice1.FinPeriodID) > 0)
          {
            nullable1 = adj.Voided;
            if (!nullable1.GetValueOrDefault())
            {
              nullable1 = adj.VoidAppl;
              if (!nullable1.GetValueOrDefault())
                this.SegregateBatch(je, arInvoice1.BranchID, arInvoice1.CuryID, arInvoice1.DocDate, arInvoice1.FinPeriodID, arInvoice1.DocDesc, currencyInfo2, (PX.Objects.GL.Batch) null);
            }
          }
          ARReleaseProcess.EnsureNoUnreleasedVoidPaymentExists((PXGraph) this, (ARRegister) arPayment, payment.DocType == "REF" ? "refunded" : "adjusted");
          Decimal? nullable2 = adj.CuryAdjgAmt;
          Decimal num1 = 0M;
          if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
          {
            nullable2 = adj.CuryAdjgDiscAmt;
            Decimal num2 = 0M;
            if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
            {
              nullable2 = adj.CuryAdjgPPDAmt;
              Decimal num3 = 0M;
              if (nullable2.GetValueOrDefault() == num3 & nullable2.HasValue)
              {
                nullable1 = arInvoice1.IsRetainageDocument;
                if (nullable1.GetValueOrDefault())
                {
                  nullable2 = arInvoice1.RetainageUnreleasedAmt;
                  Decimal num4 = 0M;
                  if (nullable2.GetValueOrDefault() == num4 & nullable2.HasValue)
                  {
                    nullable2 = arInvoice1.RetainageReleased;
                    Decimal num5 = 0M;
                    if (nullable2.GetValueOrDefault() == num5 & nullable2.HasValue)
                      goto label_24;
                  }
                }
                ((PXSelectBase) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Cache.Delete((object) adj);
                continue;
              }
            }
          }
label_24:
          nullable1 = adj.Hold;
          if (nullable1.GetValueOrDefault())
            throw new PXException("Document is On Hold and cannot be released.");
          if (arInvoice1 != null)
          {
            nullable1 = arInvoice1.PaymentsByLinesAllowed;
            if (nullable1.GetValueOrDefault())
            {
              int? adjdLineNbr = adj.AdjdLineNbr;
              int num6 = 0;
              if (adjdLineNbr.GetValueOrDefault() == num6 & adjdLineNbr.HasValue)
                continue;
            }
          }
          if (!this._IsIntegrityCheck)
          {
            nullable1 = adj.PendingPPD;
            if (nullable1.GetValueOrDefault())
            {
              ARInvoice arInvoice2 = arInvoice1;
              nullable1 = adj.Voided;
              bool? nullable3 = nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?();
              arInvoice2.PendingPPD = nullable3;
              ((PXSelectBase) this.ARDocument).Cache.Update((object) arInvoice1);
            }
          }
          if (this._IsIntegrityCheck && adj.AdjdDocType == "SMC")
          {
            nullable2 = adj.RGOLAmt;
            Decimal num7 = 0M;
            if (!(nullable2.GetValueOrDefault() == num7 & nullable2.HasValue))
            {
              ARAdjust arAdjust2 = adj;
              nullable2 = arAdjust2.AdjAmt;
              Decimal? signedRgolAmt = adj.SignedRGOLAmt;
              arAdjust2.AdjAmt = nullable2.HasValue & signedRgolAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + signedRgolAmt.GetValueOrDefault()) : new Decimal?();
              adj.RGOLAmt = new Decimal?(0M);
              ((PXSelectBase<ARAdjust>) this.ARAdjust_AdjdDocType_RefNbr_CustomerID).Update(adj);
            }
          }
          int num8;
          if (this._IsIntegrityCheck)
          {
            int? customerId = adj.CustomerID;
            int? adjdCustomerId = adj.AdjdCustomerID;
            num8 = customerId.GetValueOrDefault() == adjdCustomerId.GetValueOrDefault() & customerId.HasValue == adjdCustomerId.HasValue ? 1 : 0;
          }
          else
            num8 = 1;
          bool flag2 = num8 != 0;
          this.ProcessAdjustmentSalesPersonCommission(adj, payment, (ARRegister) arInvoice1, cury, arTran);
          if (arInvoice1.RefNbr != null)
          {
            if (flag2)
            {
              flag1 = true;
              DateTime date2 = arInvoice1.DocDate.Value;
              if (adj.AdjdDocType == "PPI" && adj.AdjgDocType == "REF")
              {
                foreach (PXResult<ARAdjust> pxResult2 in PXSelectBase<ARAdjust, PXViewOf<ARAdjust>.BasedOn<SelectFromBase<ARAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjdDocType, Equal<ARDocType.prepaymentInvoice>>>>, And<BqlOperand<ARAdjust.adjdRefNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsNotEqual<ARDocType.creditMemo>>>, And<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsNotEqual<ARDocType.refund>>>, And<BqlOperand<ARAdjust.released, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>>.Config>.Select((PXGraph) this, new object[1]
                {
                  (object) adj.AdjdRefNbr
                }))
                  date2 = PX.Objects.GL.FinPeriods.FinPeriodUtils.Max(PXResult<ARAdjust>.op_Implicit(pxResult2).AdjgDocDate.Value, date2);
              }
              if (!this._IsIntegrityCheck)
              {
                nullable1 = payment.OrigReleased;
                if (nullable1.GetValueOrDefault())
                {
                  DateTime? adjDate = payment.AdjDate;
                  DateTime dateTime = date2;
                  if ((adjDate.HasValue ? (adjDate.GetValueOrDefault() < dateTime ? 1 : 0) : 0) != 0)
                    throw new PXException("{0} cannot be less than Document Date.", new object[1]
                    {
                      (object) PXUIFieldAttribute.GetDisplayName<ARPayment.adjDate>(((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache)
                    });
                }
              }
              this.UpdateBalances(adj, (ARRegister) arInvoice1, arTran);
              this.UpdateARBalances(adj, (ARRegister) arInvoice1);
            }
            this.UpdateARBalances(adj, paymentRegister);
            if (flag2)
              this._oldInvoiceRefresher.RecordDocument(arInvoice1.BranchID, arInvoice1.CustomerID, arInvoice1.CustomerLocationID);
          }
          else
          {
            flag1 = true;
            this.UpdateBalances(adj, (ARRegister) arPayment);
            this.UpdateARBalances(adj, paymentRegister);
            this.UpdateARBalances(adj, (ARRegister) arPayment);
          }
          PX.Objects.GL.Batch copy = (PX.Objects.GL.Batch) ((PXSelectBase) je.BatchModule).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current);
          this.ProcessAdjustmentAdjusting(je, adj, payment, (ARRegister) arInvoice1, paymentCustomer, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(new_info));
          if (flag2)
            this.ProcessAdjustmentAdjusted(je, adj, (ARRegister) arInvoice1, payment, currencyInfo2, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(new_info));
          this.ProcessAdjustmentCashDiscount(je, adj, payment, paymentCustomer, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(new_info), currencyInfo2);
          this.ProcessAdjustmentWriteOff(je, adj, payment, paymentCustomer, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(new_info), currencyInfo2);
          this.ProcessAdjustmentGOL(je, adj, payment, paymentCustomer, (ARRegister) arInvoice1, paycury, cury, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(new_info), currencyInfo2);
          if (adj.AdjgDocType != adj.AdjdDocType || adj.AdjgRefNbr != adj.AdjdRefNbr)
          {
            Decimal? curyAdjgAmt = adj.CuryAdjgAmt;
            Decimal? adjAmt = adj.AdjAmt;
            ARRegister arRegister3 = paymentRegister;
            Decimal? nullable4 = arRegister3.CuryDocBal;
            Decimal? adjgBalSign1 = adj.AdjgBalSign;
            Decimal? nullable5 = curyAdjgAmt;
            nullable2 = adjgBalSign1.HasValue & nullable5.HasValue ? new Decimal?(adjgBalSign1.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
            arRegister3.CuryDocBal = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
            ARRegister arRegister4 = paymentRegister;
            nullable2 = arRegister4.DocBal;
            Decimal? adjgBalSign2 = adj.AdjgBalSign;
            Decimal? nullable6 = adjAmt;
            nullable4 = adjgBalSign2.HasValue & nullable6.HasValue ? new Decimal?(adjgBalSign2.GetValueOrDefault() * nullable6.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable7;
            if (!(nullable2.HasValue & nullable4.HasValue))
            {
              nullable6 = new Decimal?();
              nullable7 = nullable6;
            }
            else
              nullable7 = new Decimal?(nullable2.GetValueOrDefault() - nullable4.GetValueOrDefault());
            arRegister4.DocBal = nullable7;
            nullable1 = paymentRegister.PaymentsByLinesAllowed;
            if (nullable1.GetValueOrDefault())
              this.ProcessPayByLineCreditMemoAdjustment(paymentRegister, adj);
          }
          this.ProcessSVATAdjustments(je, adj, arInvoice1, paymentRegister, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(new_info));
          if (!this._IsIntegrityCheck)
          {
            PXSelectorAttribute.StoreCached<ARAdjust.adjdRefNbr>(((PXGraph) this).Caches[typeof (ARAdjust)], (object) adj, (object) arInvoice1);
            nullable1 = payment.Released;
            bool flag3 = false;
            bool saveBatch = nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue || !((PXSelectBase) je.BatchModule).Cache.ObjectsEqual<PX.Objects.GL.Batch.module, PX.Objects.GL.Batch.batchNbr, PX.Objects.GL.Batch.lineCntr, PX.Objects.GL.Batch.creditTotal, PX.Objects.GL.Batch.debitTotal, PX.Objects.GL.Batch.curyCreditTotal, PX.Objects.GL.Batch.curyDebitTotal>((object) copy, (object) ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current);
            this.SaveBatchForAdjustment(je, adj, arRegister1, saveBatch);
            adj.Released = new bool?(true);
            adj = (ARAdjust) ((PXGraph) this).Caches[typeof (ARAdjust)].Update((object) adj);
          }
          arAdjust1 = adj;
          currencyInfo1 = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran>.op_Implicit(pxResult1);
          this.ProcessAdjustmentTranPost(arTran, adj, arRegister1, paymentRegister);
        }
      }
      if (flag1 && arRegister1 != null)
        this.VerifyAdjustedDocumentAndClose(arRegister1);
    }
    foreach (KeyValuePair<ARReleaseProcess.ARTranPostKey, ARTranPost> retainagePayment in this.tranPostRetainagePayments)
    {
      if (this.IsNeedUpdateHistoryForTransaction(retainagePayment.Value.TranPeriodID))
        ((PXSelectBase<ARTranPost>) this.TranPost).Insert(retainagePayment.Value);
    }
    return new Tuple<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo>(arAdjust1, currencyInfo1);
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  private void ProcessPayByLineCreditMemoAdjustment(ARRegister paymentRegister, ARAdjust adj)
  {
    Decimal? nullable1 = adj.CuryAdjgAmt;
    Decimal? nullable2 = adj.AdjAmt;
    IEnumerable<PXResult<ARTran>> source = (IEnumerable<PXResult<ARTran>>) PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARRegister.docType>>, And<ARTran.refNbr, Equal<Required<ARRegister.refNbr>>>>, OrderBy<Asc<ARTran.lineNbr>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) paymentRegister.DocType,
      (object) paymentRegister.RefNbr
    });
    Decimal? nullable3 = nullable1;
    Decimal num1 = 0M;
    if (nullable3.GetValueOrDefault() < num1 & nullable3.HasValue)
      source = (IEnumerable<PXResult<ARTran>>) source.OrderByDescending<PXResult<ARTran>, int?>((Func<PXResult<ARTran>, int?>) (_ => PXResult.Unwrap<ARTran>((object) _).LineNbr));
    foreach (PXResult<ARTran> pxResult in source)
    {
      ARTran copy = PXCache<ARTran>.CreateCopy(PXResult<ARTran>.op_Implicit(pxResult));
      bool? voided = adj.Voided;
      Decimal? nullable4;
      Decimal? nullable5;
      Decimal? nullable6;
      if (!voided.GetValueOrDefault())
      {
        nullable4 = copy.CuryTranBal;
        nullable5 = nullable1;
        if (nullable4.GetValueOrDefault() <= nullable5.GetValueOrDefault() & nullable4.HasValue & nullable5.HasValue)
        {
          nullable6 = copy.CuryTranBal;
          goto label_13;
        }
      }
      voided = adj.Voided;
      Decimal? nullable7;
      Decimal? nullable8;
      if (voided.GetValueOrDefault())
      {
        nullable7 = copy.CuryTranBal;
        nullable8 = copy.CuryOrigTranAmt;
        nullable5 = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
        nullable4 = nullable1;
        if (nullable5.GetValueOrDefault() > nullable4.GetValueOrDefault() & nullable5.HasValue & nullable4.HasValue)
        {
          nullable4 = copy.CuryTranBal;
          nullable5 = copy.CuryOrigTranAmt;
          if (!(nullable4.HasValue & nullable5.HasValue))
          {
            nullable8 = new Decimal?();
            nullable6 = nullable8;
            goto label_13;
          }
          nullable6 = new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault());
          goto label_13;
        }
      }
      nullable6 = nullable1;
label_13:
      Decimal? nullable9 = nullable6;
      voided = adj.Voided;
      Decimal? nullable10;
      if (!voided.GetValueOrDefault())
      {
        nullable5 = copy.TranBal;
        nullable4 = nullable2;
        if (nullable5.GetValueOrDefault() <= nullable4.GetValueOrDefault() & nullable5.HasValue & nullable4.HasValue)
        {
          nullable10 = copy.TranBal;
          goto label_22;
        }
      }
      voided = adj.Voided;
      if (voided.GetValueOrDefault())
      {
        nullable8 = copy.TranBal;
        nullable7 = copy.OrigTranAmt;
        nullable4 = nullable8.HasValue & nullable7.HasValue ? new Decimal?(nullable8.GetValueOrDefault() - nullable7.GetValueOrDefault()) : new Decimal?();
        nullable5 = nullable2;
        if (nullable4.GetValueOrDefault() > nullable5.GetValueOrDefault() & nullable4.HasValue & nullable5.HasValue)
        {
          nullable5 = copy.TranBal;
          nullable4 = copy.OrigTranAmt;
          if (!(nullable5.HasValue & nullable4.HasValue))
          {
            nullable7 = new Decimal?();
            nullable10 = nullable7;
            goto label_22;
          }
          nullable10 = new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault());
          goto label_22;
        }
      }
      nullable10 = nullable2;
label_22:
      Decimal? nullable11 = nullable10;
      nullable4 = nullable1;
      nullable5 = nullable9;
      Decimal? nullable12;
      if (!(nullable4.HasValue & nullable5.HasValue))
      {
        nullable7 = new Decimal?();
        nullable12 = nullable7;
      }
      else
        nullable12 = new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault());
      nullable1 = nullable12;
      nullable5 = nullable2;
      nullable4 = nullable11;
      Decimal? nullable13;
      if (!(nullable5.HasValue & nullable4.HasValue))
      {
        nullable7 = new Decimal?();
        nullable13 = nullable7;
      }
      else
        nullable13 = new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault());
      nullable2 = nullable13;
      ARTran arTran1 = copy;
      nullable4 = arTran1.CuryTranBal;
      nullable5 = nullable9;
      Decimal? nullable14;
      if (!(nullable4.HasValue & nullable5.HasValue))
      {
        nullable7 = new Decimal?();
        nullable14 = nullable7;
      }
      else
        nullable14 = new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault());
      arTran1.CuryTranBal = nullable14;
      ARTran arTran2 = copy;
      nullable5 = arTran2.TranBal;
      nullable4 = nullable11;
      Decimal? nullable15;
      if (!(nullable5.HasValue & nullable4.HasValue))
      {
        nullable7 = new Decimal?();
        nullable15 = nullable7;
      }
      else
        nullable15 = new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault());
      arTran2.TranBal = nullable15;
      ((PXSelectBase<ARTran>) this.ARTran_TranType_RefNbr).Update(copy);
      nullable4 = nullable1;
      Decimal num2 = 0M;
      if (nullable4.GetValueOrDefault() == num2 & nullable4.HasValue)
        break;
    }
  }

  protected virtual void ProcessSVATAdjustments(
    JournalEntry je,
    ARAdjust adj,
    ARInvoice adjddoc,
    ARRegister adjgdoc,
    PX.Objects.CM.Extensions.CurrencyInfo new_info)
  {
    if (this._IsIntegrityCheck || !PXAccess.FeatureInstalled<FeaturesSet.vATReporting>())
      return;
    foreach (PXResult<SVATConversionHist> pxResult in PXSelectBase<SVATConversionHist, PXSelect<SVATConversionHist, Where<SVATConversionHist.module, Equal<BatchModule.moduleAR>, And2<Where<SVATConversionHist.adjdDocType, Equal<Current<ARAdjust.adjdDocType>>, And<SVATConversionHist.adjdRefNbr, Equal<Current<ARAdjust.adjdRefNbr>>, Or<SVATConversionHist.adjdDocType, Equal<Current<ARAdjust.adjgDocType>>, And<SVATConversionHist.adjdRefNbr, Equal<Current<ARAdjust.adjgRefNbr>>>>>>, And<SVATConversionHist.reversalMethod, In3<SVATTaxReversalMethods.onPayments, SVATTaxReversalMethods.onPrepayment>, And<Where<SVATConversionHist.adjdDocType, Equal<SVATConversionHist.adjgDocType>, And<SVATConversionHist.adjdRefNbr, Equal<SVATConversionHist.adjgRefNbr>>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) adj
    }, Array.Empty<object>()))
    {
      SVATConversionHist docSVAT = PXResult<SVATConversionHist>.op_Implicit(pxResult);
      bool flag = adj.AdjgDocType == docSVAT.AdjdDocType && adj.AdjgRefNbr == docSVAT.AdjdRefNbr;
      Decimal? nullable;
      Decimal num1;
      if (!flag)
      {
        nullable = adj.CuryAdjdAmt;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        nullable = adj.CuryAdjdDiscAmt;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        Decimal num2 = valueOrDefault1 + valueOrDefault2;
        nullable = adj.CuryAdjdWOAmt;
        Decimal valueOrDefault3 = nullable.GetValueOrDefault();
        Decimal num3 = num2 + valueOrDefault3;
        nullable = adjddoc.CuryOrigDocAmt;
        Decimal valueOrDefault4 = nullable.GetValueOrDefault();
        num1 = num3 / valueOrDefault4;
      }
      else
      {
        nullable = adj.CuryAdjgAmt;
        Decimal valueOrDefault5 = nullable.GetValueOrDefault();
        nullable = adj.CuryAdjgDiscAmt;
        Decimal valueOrDefault6 = nullable.GetValueOrDefault();
        Decimal num4 = valueOrDefault5 + valueOrDefault6;
        nullable = adj.CuryAdjgWOAmt;
        Decimal valueOrDefault7 = nullable.GetValueOrDefault();
        Decimal num5 = num4 + valueOrDefault7;
        nullable = adjgdoc.CuryOrigDocAmt;
        Decimal valueOrDefault8 = nullable.GetValueOrDefault();
        num1 = num5 / valueOrDefault8;
      }
      Decimal num6 = num1;
      SVATConversionHist svatConversionHist = new SVATConversionHist()
      {
        Module = "AR",
        AdjdBranchID = adj.AdjdBranchID,
        AdjdDocType = flag ? adj.AdjgDocType : adj.AdjdDocType,
        AdjdRefNbr = flag ? adj.AdjgRefNbr : adj.AdjdRefNbr,
        AdjdLineNbr = adj.AdjdLineNbr,
        AdjgDocType = flag ? adj.AdjdDocType : adj.AdjgDocType,
        AdjgRefNbr = flag ? adj.AdjdRefNbr : adj.AdjgRefNbr,
        AdjNbr = adj.AdjNbr,
        AdjdDocDate = adj.AdjgDocDate,
        TaxID = docSVAT.TaxID,
        TaxType = docSVAT.TaxType,
        TaxRate = docSVAT.TaxRate,
        VendorID = docSVAT.VendorID,
        CuryInfoID = docSVAT.CuryInfoID
      };
      svatConversionHist.ReversalMethod = svatConversionHist.AdjdDocType == "PPI" ? "Y" : "P";
      svatConversionHist.FillAmounts(((PXGraph) this).GetExtension<ARReleaseProcess.MultiCurrency>().GetCurrencyInfo(docSVAT.CuryInfoID), docSVAT.CuryTaxableAmt, docSVAT.CuryTaxAmt, num6);
      FinPeriodIDAttribute.SetPeriodsByMaster<SVATConversionHist.adjdFinPeriodID>(((PXSelectBase) this.SVATConversionHistory).Cache, (object) svatConversionHist, adj.AdjdTranPeriodID);
      ARRegister adjddoc1 = flag ? adjgdoc : (ARRegister) adjddoc;
      ARRegister arRegister = (ARRegister) ((PXSelectBase) this.ARDocument).Cache.Locate((object) adjddoc1);
      if (arRegister != null)
        PXCache<ARRegister>.RestoreCopy(adjddoc1, arRegister);
      nullable = adjddoc1.CuryDocBal;
      Decimal num7 = 0M;
      if (nullable.GetValueOrDefault() == num7 & nullable.HasValue && !adjddoc.IsMigratedRecord.GetValueOrDefault())
        svatConversionHist = this.ProcessLastSVATRecord(adjddoc1, docSVAT, svatConversionHist, num6);
      SVATConversionHist adjSVAT = (SVATConversionHist) ((PXSelectBase) this.SVATConversionHistory).Cache.Insert((object) svatConversionHist);
      docSVAT.Processed = new bool?(false);
      docSVAT.AdjgFinPeriodID = (string) null;
      PXTimeStampScope.PutPersisted(((PXSelectBase) this.SVATConversionHistory).Cache, (object) docSVAT, new object[1]
      {
        (object) PXDatabase.SelectTimeStamp()
      });
      ((PXSelectBase) this.SVATConversionHistory).Cache.Update((object) docSVAT);
      if (!this._IsIntegrityCheck)
        this.AfterSVATConversionHistoryInserted(je, adj, adjddoc, adjgdoc, docSVAT, adjSVAT);
    }
  }

  protected virtual SVATConversionHist ProcessLastSVATRecord(
    ARRegister adjddoc,
    SVATConversionHist docSVAT,
    SVATConversionHist adjSVAT,
    Decimal percent)
  {
    int num = percent != 1M ? 1 : 0;
    adjSVAT.CuryTaxableAmt = docSVAT.CuryTaxableAmt;
    adjSVAT.TaxableAmt = docSVAT.TaxableAmt;
    adjSVAT.CuryTaxAmt = docSVAT.CuryTaxAmt;
    adjSVAT.TaxAmt = docSVAT.TaxAmt;
    if (num != 0)
    {
      foreach (PXResult<SVATConversionHist> pxResult in ((PXSelectBase<SVATConversionHist>) this.SVATRecognitionRecords).Select(new object[3]
      {
        (object) docSVAT.AdjdDocType,
        (object) docSVAT.AdjdRefNbr,
        (object) docSVAT.TaxID
      }))
      {
        SVATConversionHist svatConversionHist1 = PXResult<SVATConversionHist>.op_Implicit(pxResult);
        SVATConversionHist svatConversionHist2 = adjSVAT;
        Decimal? nullable = svatConversionHist2.CuryTaxableAmt;
        Decimal valueOrDefault1 = svatConversionHist1.CuryTaxableAmt.GetValueOrDefault();
        svatConversionHist2.CuryTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - valueOrDefault1) : new Decimal?();
        SVATConversionHist svatConversionHist3 = adjSVAT;
        nullable = svatConversionHist3.TaxableAmt;
        Decimal valueOrDefault2 = svatConversionHist1.TaxableAmt.GetValueOrDefault();
        svatConversionHist3.TaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - valueOrDefault2) : new Decimal?();
        SVATConversionHist svatConversionHist4 = adjSVAT;
        nullable = svatConversionHist4.CuryTaxAmt;
        Decimal valueOrDefault3 = svatConversionHist1.CuryTaxAmt.GetValueOrDefault();
        svatConversionHist4.CuryTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - valueOrDefault3) : new Decimal?();
        SVATConversionHist svatConversionHist5 = adjSVAT;
        nullable = svatConversionHist5.TaxAmt;
        Decimal valueOrDefault4 = svatConversionHist1.TaxAmt.GetValueOrDefault();
        svatConversionHist5.TaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - valueOrDefault4) : new Decimal?();
      }
    }
    adjSVAT.CuryUnrecognizedTaxAmt = adjSVAT.CuryTaxAmt;
    adjSVAT.UnrecognizedTaxAmt = adjSVAT.TaxAmt;
    return adjSVAT;
  }

  protected virtual void AfterSVATConversionHistoryInserted(
    JournalEntry je,
    ARAdjust adj,
    ARInvoice adjddoc,
    ARRegister adjgdoc,
    SVATConversionHist docSVAT,
    SVATConversionHist adjSVAT)
  {
  }

  protected virtual void ProcessAdjustmentsOnlyAdjusted(
    JournalEntry je,
    PXResultset<ARAdjust> adjustments)
  {
    foreach (IGrouping<object, PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARInvoice, ARPayment, ARRegister>> grouping in ((IEnumerable<PXResult<ARAdjust>>) adjustments).AsEnumerable<PXResult<ARAdjust>>().Where<PXResult<ARAdjust>>((Func<PXResult<ARAdjust>, bool>) (res => !PXResult<ARAdjust>.op_Implicit(res).IsInitialApplication.GetValueOrDefault())).Cast<PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARInvoice, ARPayment, ARRegister>>().GroupBy<PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARInvoice, ARPayment, ARRegister>, object>((Func<PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARInvoice, ARPayment, ARRegister>, object>) (res => (object) PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARInvoice, ARPayment, ARRegister>.op_Implicit(res)), PXCacheEx.GetComparer(((PXSelectBase) this.ARDocument).Cache)))
    {
      ARRegister key = (ARRegister) grouping.Key;
      ARRegister adjddoc = (ARRegister) null;
      bool flag = false;
      foreach (PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARInvoice, ARPayment, ARRegister> pxResult in (IEnumerable<PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARInvoice, ARPayment, ARRegister>>) grouping)
      {
        ARAdjust adj = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARInvoice, ARPayment, ARRegister>.op_Implicit(pxResult);
        PX.Objects.CM.Extensions.CurrencyInfo vouch_info = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARInvoice, ARPayment, ARRegister>.op_Implicit(pxResult);
        PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARInvoice, ARPayment, ARRegister>.op_Implicit(pxResult);
        ARInvoice arInvoice = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARInvoice, ARPayment, ARRegister>.op_Implicit(pxResult);
        ARPayment arPayment = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARInvoice, ARPayment, ARRegister>.op_Implicit(pxResult);
        if (arInvoice != null && arInvoice.RefNbr != null)
        {
          PXCache<ARRegister>.RestoreCopy((ARRegister) arInvoice, PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARInvoice, ARPayment, ARRegister>.op_Implicit(pxResult));
          adjddoc = (ARRegister) arInvoice;
        }
        else if (arPayment != null && arPayment.RefNbr != null)
          adjddoc = (ARRegister) arPayment;
        Decimal? nullable = adj.CuryAdjgAmt;
        Decimal num1 = 0M;
        if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
        {
          nullable = adj.CuryAdjgDiscAmt;
          Decimal num2 = 0M;
          if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
          {
            ((PXSelectBase) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Cache.Delete((object) adj);
            continue;
          }
        }
        if (adj.Hold.GetValueOrDefault())
          throw new PXException("Document is On Hold and cannot be released.");
        if (key.RefNbr != null)
        {
          flag = true;
          if (!this._IsIntegrityCheck && arPayment.Released.GetValueOrDefault())
          {
            DateTime? adjDate = arPayment.AdjDate;
            DateTime? docDate = key.DocDate;
            if ((adjDate.HasValue & docDate.HasValue ? (adjDate.GetValueOrDefault() < docDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              throw new PXException("{0} cannot be less than Document Date.", new object[1]
              {
                (object) PXUIFieldAttribute.GetDisplayName<ARPayment.adjDate>(((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache)
              });
          }
          this.UpdateBalances(adj, key);
          this.UpdateARBalances(adj, key);
          this._oldInvoiceRefresher.RecordDocument(key.BranchID, key.CustomerID, key.CustomerLocationID);
        }
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) this).GetExtension<ARReleaseProcess.MultiCurrency>().GetCurrencyInfo(arPayment.CuryInfoID);
        this.ProcessAdjustmentAdjusted(je, adj, key, arPayment, vouch_info, currencyInfo);
        this.ProcessAdjustmentTranPost(adj, key, (ARRegister) arPayment, true);
      }
      if (flag)
        this.VerifyAdjustedDocumentAndClose(adjddoc);
    }
  }

  private void ProcessAdjustmentSalesPersonCommission(
    ARAdjust adj,
    ARPayment payment,
    ARRegister adjustedInvoice,
    PX.Objects.CM.Extensions.Currency cury,
    ARTran tran)
  {
    if (!(payment.DocType != "CRM"))
      return;
    if (adjustedInvoice.PaymentsByLinesAllowed.GetValueOrDefault())
    {
      PXResult<ARSalesPerTran, PX.Objects.CM.Extensions.CurrencyInfo> pxResult1 = (PXResult<ARSalesPerTran, PX.Objects.CM.Extensions.CurrencyInfo>) PXResultset<ARSalesPerTran>.op_Implicit(PXSelectBase<ARSalesPerTran, PXSelectJoin<ARSalesPerTran, LeftJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARSalesPerTran.curyInfoID>>>, Where<ARSalesPerTran.docType, Equal<Required<ARSalesPerTran.docType>>, And<ARSalesPerTran.refNbr, Equal<Required<ARSalesPerTran.refNbr>>, And<ARSalesPerTran.salespersonID, Equal<Required<ARSalesPerTran.salespersonID>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
      {
        (object) adjustedInvoice.DocType,
        (object) adjustedInvoice.RefNbr,
        (object) tran.SalesPersonID
      }));
      ARSalesPerTran iSPT = PXResult<ARSalesPerTran, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult1);
      if (iSPT == null)
        return;
      PXResult<ARSalesPerTran, PX.Objects.CM.Extensions.CurrencyInfo> pxResult2 = (PXResult<ARSalesPerTran, PX.Objects.CM.Extensions.CurrencyInfo>) PXResultset<ARSalesPerTran>.op_Implicit(PXSelectBase<ARSalesPerTran, PXSelectJoin<ARSalesPerTran, LeftJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARSalesPerTran.curyInfoID>>>, Where<ARSalesPerTran.docType, Equal<Required<ARSalesPerTran.docType>>, And<ARSalesPerTran.refNbr, Equal<Required<ARSalesPerTran.refNbr>>, And<ARSalesPerTran.salespersonID, Equal<Required<ARSalesPerTran.salespersonID>>, And<ARSalesPerTran.adjNbr, Equal<Required<ARSalesPerTran.adjNbr>>, And<ARSalesPerTran.adjdDocType, Equal<Required<ARSalesPerTran.adjdDocType>>, And<ARSalesPerTran.adjdRefNbr, Equal<Required<ARSalesPerTran.adjdRefNbr>>>>>>>>>.Config>.Select((PXGraph) this, new object[6]
      {
        (object) payment.DocType,
        (object) payment.RefNbr,
        (object) tran.SalesPersonID,
        (object) adj.AdjNbr,
        (object) adj.AdjdDocType,
        (object) adj.AdjdRefNbr
      }));
      ARSalesPerTran arSalesPerTran1 = PXResult<ARSalesPerTran, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult2);
      if (arSalesPerTran1 == null)
      {
        ARSalesPerTran paymentSpt = this.CreatePaymentSPT((ARRegister) payment, adj, iSPT);
        ((PXGraph) this).GetExtension<ARReleaseProcess.MultiCurrency>().StoreResult(PXResult<ARSalesPerTran, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult1));
        ARSalesPerTran arSalesPerTran2 = paymentSpt;
        Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
        Decimal? nullable1 = adj.CuryAdjdDiscAmt;
        Decimal? nullable2 = curyAdjdAmt.HasValue & nullable1.HasValue ? new Decimal?(curyAdjdAmt.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        arSalesPerTran2.CuryCommnblAmt = nullable2;
        ARSalesPerTran arSalesPerTran3 = paymentSpt;
        Decimal? curyCommnblAmt = paymentSpt.CuryCommnblAmt;
        Decimal? nullable3 = iSPT.CommnPct;
        nullable1 = curyCommnblAmt.HasValue & nullable3.HasValue ? new Decimal?(curyCommnblAmt.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
        Decimal num = (Decimal) 100;
        Decimal? nullable4;
        if (!nullable1.HasValue)
        {
          nullable3 = new Decimal?();
          nullable4 = nullable3;
        }
        else
          nullable4 = new Decimal?(nullable1.GetValueOrDefault() / num);
        arSalesPerTran3.CuryCommnAmt = nullable4;
        ((PXSelectBase<ARSalesPerTran>) this.ARDoc_SalesPerTrans).Insert(paymentSpt);
      }
      else
      {
        PXCache<ARSalesPerTran>.StoreOriginal((PXGraph) this, arSalesPerTran1);
        ((PXGraph) this).GetExtension<ARReleaseProcess.MultiCurrency>().StoreResult(PXResult<ARSalesPerTran, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult2));
        ARSalesPerTran copy = PXCache<ARSalesPerTran>.CreateCopy(arSalesPerTran1);
        ARSalesPerTran arSalesPerTran4 = copy;
        Decimal? curyCommnblAmt1 = arSalesPerTran4.CuryCommnblAmt;
        Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
        Decimal? curyAdjdDiscAmt = adj.CuryAdjdDiscAmt;
        Decimal? nullable5 = curyAdjdAmt.HasValue & curyAdjdDiscAmt.HasValue ? new Decimal?(curyAdjdAmt.GetValueOrDefault() + curyAdjdDiscAmt.GetValueOrDefault()) : new Decimal?();
        arSalesPerTran4.CuryCommnblAmt = curyCommnblAmt1.HasValue & nullable5.HasValue ? new Decimal?(curyCommnblAmt1.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
        ARSalesPerTran arSalesPerTran5 = copy;
        Decimal? curyCommnblAmt2 = copy.CuryCommnblAmt;
        Decimal? nullable6 = iSPT.CommnPct;
        Decimal? nullable7 = curyCommnblAmt2.HasValue & nullable6.HasValue ? new Decimal?(curyCommnblAmt2.GetValueOrDefault() * nullable6.GetValueOrDefault()) : new Decimal?();
        Decimal num = (Decimal) 100;
        Decimal? nullable8;
        if (!nullable7.HasValue)
        {
          nullable6 = new Decimal?();
          nullable8 = nullable6;
        }
        else
          nullable8 = new Decimal?(nullable7.GetValueOrDefault() / num);
        arSalesPerTran5.CuryCommnAmt = nullable8;
        ((PXSelectBase<ARSalesPerTran>) this.ARDoc_SalesPerTrans).Update(copy);
      }
    }
    else
    {
      foreach (PXResult<ARSalesPerTran> pxResult in ((PXSelectBase<ARSalesPerTran>) this.ARDoc_SalesPerTrans).Select(new object[2]
      {
        (object) adjustedInvoice.DocType,
        (object) adjustedInvoice.RefNbr
      }))
      {
        ARSalesPerTran arSalesPerTran = PXResult<ARSalesPerTran>.op_Implicit(pxResult);
        ARSalesPerTran paymentSpt = this.CreatePaymentSPT((ARRegister) payment, adj, arSalesPerTran);
        Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
        Decimal? nullable9 = adj.CuryAdjdDiscAmt;
        Decimal? nullable10 = curyAdjdAmt.HasValue & nullable9.HasValue ? new Decimal?(curyAdjdAmt.GetValueOrDefault() + nullable9.GetValueOrDefault()) : new Decimal?();
        nullable9 = (Decimal?) tran?.CuryOrigTranAmt;
        Decimal? nullable11 = nullable9 ?? adjustedInvoice.CuryOrigDocAmt;
        Decimal? nullable12;
        if (!(nullable10.HasValue & nullable11.HasValue))
        {
          nullable9 = new Decimal?();
          nullable12 = nullable9;
        }
        else
          nullable12 = new Decimal?(nullable10.GetValueOrDefault() / nullable11.GetValueOrDefault());
        nullable9 = nullable12;
        Decimal aRatio = nullable9.Value;
        if (payment.DocType == "CSL" || payment.DocType == "RCS")
          aRatio = 1M;
        ARReleaseProcess.CopyShare(paymentSpt, arSalesPerTran, aRatio, cury.DecimalPlaces ?? (short) 2);
        ((PXSelectBase<ARSalesPerTran>) this.ARDoc_SalesPerTrans).Insert(paymentSpt);
      }
    }
  }

  private void ProcessAdjustmentAdjusting(
    JournalEntry je,
    ARAdjust adj,
    ARPayment payment,
    ARRegister adjustedDocument,
    Customer paymentCustomer,
    PX.Objects.CM.Extensions.CurrencyInfo new_info)
  {
    int num1 = adj.AdjdDocType == "SMC" ? 1 : 0;
    bool flag1 = adj.AdjgDocType == "PPI";
    bool flag2 = adj.AdjdDocType == "PPI" && payment.OrigDocType == "PPI";
    string str = !adj.IsOrigSmallCreditWOApp() ? "P" : "N";
    PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
    glTran1.SummPost = new bool?(true);
    glTran1.ZeroPost = new bool?(false);
    glTran1.BranchID = adj.AdjgBranchID;
    glTran1.AccountID = flag1 ? payment.PrepaymentAccountID : payment.ARAccountID;
    glTran1.ReclassificationProhibited = new bool?(true);
    glTran1.SubID = flag1 ? payment.PrepaymentSubID : payment.ARSubID;
    Decimal? adjgGlSign1 = adj.AdjgGLSign;
    Decimal num2 = 1M;
    glTran1.DebitAmt = adjgGlSign1.GetValueOrDefault() == num2 & adjgGlSign1.HasValue ? adj.AdjAmt : new Decimal?(0M);
    Decimal? adjgGlSign2 = adj.AdjgGLSign;
    Decimal num3 = 1M;
    glTran1.CuryDebitAmt = adjgGlSign2.GetValueOrDefault() == num3 & adjgGlSign2.HasValue ? adj.CuryAdjgAmt : new Decimal?(0M);
    Decimal? adjgGlSign3 = adj.AdjgGLSign;
    Decimal num4 = 1M;
    glTran1.CreditAmt = adjgGlSign3.GetValueOrDefault() == num4 & adjgGlSign3.HasValue ? new Decimal?(0M) : adj.AdjAmt;
    Decimal? adjgGlSign4 = adj.AdjgGLSign;
    Decimal num5 = 1M;
    glTran1.CuryCreditAmt = adjgGlSign4.GetValueOrDefault() == num5 & adjgGlSign4.HasValue ? new Decimal?(0M) : adj.CuryAdjgAmt;
    glTran1.TranType = adj.IsOrigSmallCreditWOApp() ? adj.AdjdDocType : adj.AdjgDocType;
    glTran1.TranClass = str;
    glTran1.RefNbr = adj.IsOrigSmallCreditWOApp() ? adj.AdjdRefNbr : adj.AdjgRefNbr;
    glTran1.TranDesc = adj.IsOrigSmallCreditWOApp() ? adjustedDocument.DocDesc : payment.DocDesc;
    glTran1.TranDate = adj.AdjgDocDate;
    PX.Objects.GL.GLTran glTran2 = glTran1;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran2, adj.AdjgTranPeriodID);
    glTran2.CuryInfoID = new_info.CuryInfoID;
    glTran2.Released = new bool?(true);
    glTran2.ReferenceID = payment.CustomerID;
    glTran2.OrigAccountID = adj.IsOrigSmallCreditWOApp() ? new int?() : adj.AdjdARAcct;
    glTran2.OrigSubID = adj.IsOrigSmallCreditWOApp() ? new int?() : adj.AdjdARSub;
    glTran2.ProjectID = ProjectDefaultAttribute.NonProject();
    this.UpdateHistory(glTran2, paymentCustomer);
    this.UpdateHistory(glTran2, paymentCustomer, new_info);
    if (num1 != 0)
    {
      bool flag3 = adj.AdjgDocType == "PPM";
      if (adj.AdjgDocType == "RPM")
      {
        ARRegister arRegister = PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>, And<Where<ARRegister.docType, Equal<ARDocType.payment>, Or<ARRegister.docType, Equal<ARDocType.prepayment>>>>>, OrderBy<Asc<Switch<Case<Where<ARRegister.docType, Equal<ARDocType.payment>>, int0>, int1>, Asc<ARRegister.docType, Asc<ARRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) glTran2.RefNbr
        }));
        flag3 = arRegister != null && arRegister.DocType == "PPM";
      }
      if (flag3)
      {
        ARReleaseProcess.ARHistBucket bucket = new ARReleaseProcess.ARHistBucket();
        bucket.arAccountID = glTran2.AccountID;
        bucket.arSubID = glTran2.SubID;
        bucket.SignPayments = 1M;
        bucket.SignDeposits = -1M;
        bucket.SignPtd = -1M;
        this.UpdateHistory(glTran2, paymentCustomer, bucket);
        this.UpdateHistory(glTran2, paymentCustomer, new_info, bucket);
      }
    }
    this.InsertAdjustmentsAdjustingTransaction(je, glTran2, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = (ARRegister) payment,
      ARAdjustRecord = adj
    });
    if (!flag2)
      return;
    PX.Objects.GL.GLTran copy = (PX.Objects.GL.GLTran) ((PXSelectBase) je.GLTranModuleBatNbr).Cache.CreateCopy((object) glTran2);
    copy.AccountID = payment.PrepaymentAccountID;
    copy.SubID = payment.PrepaymentSubID;
    copy.TranClass = "Y";
    this.InsertAdjustmentsAdjustingTransaction(je, copy, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = (ARRegister) payment,
      ARAdjustRecord = adj
    });
  }

  private void ProcessAdjustmentAdjusted(
    JournalEntry je,
    ARAdjust adj,
    ARRegister adjustedDocument,
    ARPayment payment,
    PX.Objects.CM.Extensions.CurrencyInfo vouch_info,
    PX.Objects.CM.Extensions.CurrencyInfo new_info)
  {
    bool flag1 = adj.AdjdDocType == "SMC";
    bool flag2 = adj.AdjdDocType == "PPI";
    bool flag3 = adj.AdjdDocType == "PPI" && payment.OrigDocType == "PPI";
    Customer cust = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) adj.AdjdCustomerID
    }));
    string str = !adj.IsOrigSmallCreditWOApp() ? (!flag2 ? ARDocType.DocClass(adj.AdjdDocType) : "Y") : "P";
    PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
    glTran1.SummPost = new bool?(true);
    glTran1.ZeroPost = new bool?(false);
    glTran1.BranchID = adj.AdjdBranchID;
    glTran1.AccountID = flag1 ? adjustedDocument.ARAccountID : adj.AdjdARAcct;
    glTran1.ReclassificationProhibited = new bool?(true);
    glTran1.SubID = flag1 ? adjustedDocument.ARSubID : adj.AdjdARSub;
    glTran1.OrigAccountID = adj.AdjdARAcct;
    glTran1.OrigSubID = adj.AdjdARSub;
    PX.Objects.GL.GLTran glTran2 = glTran1;
    Decimal? nullable1 = adj.AdjgGLSign;
    Decimal num1 = 1M;
    Decimal? nullable2;
    Decimal? nullable3;
    if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
    {
      nullable3 = new Decimal?(0M);
    }
    else
    {
      Decimal? adjAmt = adj.AdjAmt;
      Decimal? adjDiscAmt = adj.AdjDiscAmt;
      nullable2 = adjAmt.HasValue & adjDiscAmt.HasValue ? new Decimal?(adjAmt.GetValueOrDefault() + adjDiscAmt.GetValueOrDefault()) : new Decimal?();
      Decimal? adjWoAmt = adj.AdjWOAmt;
      nullable1 = nullable2.HasValue & adjWoAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + adjWoAmt.GetValueOrDefault()) : new Decimal?();
      Decimal? rgolAmt = adj.RGOLAmt;
      nullable3 = nullable1.HasValue & rgolAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + rgolAmt.GetValueOrDefault()) : new Decimal?();
    }
    glTran2.CreditAmt = nullable3;
    PX.Objects.GL.GLTran glTran3 = glTran1;
    Decimal? adjgGlSign = adj.AdjgGLSign;
    Decimal num2 = 1M;
    Decimal? nullable4;
    Decimal? nullable5;
    if (!(adjgGlSign.GetValueOrDefault() == num2 & adjgGlSign.HasValue))
      nullable5 = new Decimal?(0M);
    else if (!object.Equals((object) new_info.CuryID, (object) new_info.BaseCuryID))
    {
      Decimal? curyAdjgAmt = adj.CuryAdjgAmt;
      nullable2 = adj.CuryAdjgDiscAmt;
      nullable4 = curyAdjgAmt.HasValue & nullable2.HasValue ? new Decimal?(curyAdjgAmt.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      nullable1 = adj.CuryAdjgWOAmt;
      if (!(nullable4.HasValue & nullable1.HasValue))
      {
        nullable2 = new Decimal?();
        nullable5 = nullable2;
      }
      else
        nullable5 = new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault());
    }
    else
      nullable5 = glTran1.CreditAmt;
    glTran3.CuryCreditAmt = nullable5;
    PX.Objects.GL.GLTran glTran4 = glTran1;
    nullable1 = adj.AdjgGLSign;
    Decimal num3 = 1M;
    Decimal? nullable6;
    if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue))
    {
      Decimal? adjAmt = adj.AdjAmt;
      Decimal? adjDiscAmt = adj.AdjDiscAmt;
      nullable2 = adjAmt.HasValue & adjDiscAmt.HasValue ? new Decimal?(adjAmt.GetValueOrDefault() + adjDiscAmt.GetValueOrDefault()) : new Decimal?();
      Decimal? adjWoAmt = adj.AdjWOAmt;
      nullable1 = nullable2.HasValue & adjWoAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + adjWoAmt.GetValueOrDefault()) : new Decimal?();
      nullable4 = adj.RGOLAmt;
      nullable6 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable6 = new Decimal?(0M);
    glTran4.DebitAmt = nullable6;
    PX.Objects.GL.GLTran glTran5 = glTran1;
    nullable4 = adj.AdjgGLSign;
    Decimal num4 = 1M;
    Decimal? nullable7;
    if (!(nullable4.GetValueOrDefault() == num4 & nullable4.HasValue))
    {
      if (!object.Equals((object) new_info.CuryID, (object) new_info.BaseCuryID))
      {
        Decimal? curyAdjgAmt = adj.CuryAdjgAmt;
        nullable2 = adj.CuryAdjgDiscAmt;
        nullable4 = curyAdjgAmt.HasValue & nullable2.HasValue ? new Decimal?(curyAdjgAmt.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        nullable1 = adj.CuryAdjgWOAmt;
        if (!(nullable4.HasValue & nullable1.HasValue))
        {
          nullable2 = new Decimal?();
          nullable7 = nullable2;
        }
        else
          nullable7 = new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault());
      }
      else
        nullable7 = glTran1.DebitAmt;
    }
    else
      nullable7 = new Decimal?(0M);
    glTran5.CuryDebitAmt = nullable7;
    glTran1.TranType = adj.IsOrigSmallCreditWOApp() ? adj.AdjdDocType : adj.AdjgDocType;
    glTran1.TranClass = str;
    glTran1.RefNbr = adj.IsOrigSmallCreditWOApp() ? adj.AdjdRefNbr : adj.AdjgRefNbr;
    glTran1.TranDesc = adj.IsOrigSmallCreditWOApp() ? adjustedDocument.DocDesc : payment.DocDesc;
    glTran1.TranDate = adj.AdjgDocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran1, adj.AdjgTranPeriodID);
    glTran1.CuryInfoID = new_info.CuryInfoID;
    glTran1.Released = new bool?(true);
    glTran1.ReferenceID = payment.CustomerID;
    glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
    this.UpdateHistory(glTran1, cust);
    this.InsertAdjustmentsAdjustedTransaction(je, glTran1, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = (ARRegister) payment,
      ARAdjustRecord = adj
    });
    PX.Objects.GL.GLTran glTran6 = glTran1;
    nullable1 = adj.AdjgGLSign;
    Decimal num5 = 1M;
    Decimal? nullable8;
    if (!(nullable1.GetValueOrDefault() == num5 & nullable1.HasValue))
      nullable8 = new Decimal?(0M);
    else if (!object.Equals((object) vouch_info.CuryID, (object) vouch_info.BaseCuryID))
    {
      nullable2 = adj.CuryAdjdAmt;
      Decimal? curyAdjdDiscAmt = adj.CuryAdjdDiscAmt;
      nullable1 = nullable2.HasValue & curyAdjdDiscAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + curyAdjdDiscAmt.GetValueOrDefault()) : new Decimal?();
      nullable4 = adj.CuryAdjdWOAmt;
      nullable8 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable8 = glTran1.CreditAmt;
    glTran6.CuryCreditAmt = nullable8;
    PX.Objects.GL.GLTran glTran7 = glTran1;
    nullable4 = adj.AdjgGLSign;
    Decimal num6 = 1M;
    Decimal? nullable9;
    if (!(nullable4.GetValueOrDefault() == num6 & nullable4.HasValue))
    {
      if (!object.Equals((object) vouch_info.CuryID, (object) vouch_info.BaseCuryID))
      {
        Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
        nullable2 = adj.CuryAdjdDiscAmt;
        nullable4 = curyAdjdAmt.HasValue & nullable2.HasValue ? new Decimal?(curyAdjdAmt.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        nullable1 = adj.CuryAdjdWOAmt;
        if (!(nullable4.HasValue & nullable1.HasValue))
        {
          nullable2 = new Decimal?();
          nullable9 = nullable2;
        }
        else
          nullable9 = new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault());
      }
      else
        nullable9 = glTran1.DebitAmt;
    }
    else
      nullable9 = new Decimal?(0M);
    glTran7.CuryDebitAmt = nullable9;
    this.UpdateHistory(glTran1, cust, vouch_info);
    if (!flag3)
      return;
    PX.Objects.GL.GLTran copy = (PX.Objects.GL.GLTran) ((PXSelectBase) je.GLTranModuleBatNbr).Cache.CreateCopy((object) glTran1);
    copy.AccountID = payment.PrepaymentAccountID;
    copy.SubID = payment.PrepaymentSubID;
    this.InsertAdjustmentsAdjustedTransaction(je, copy, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = (ARRegister) payment,
      ARAdjustRecord = adj
    });
  }

  private void PostReduceOnEarlyPaymentTran(
    JournalEntry je,
    ARInvoice ardoc,
    Decimal? curyAmount,
    Decimal? amount,
    Customer customer,
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo,
    bool isDebit)
  {
    PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran();
    glTran.SummPost = new bool?(this.SummPost);
    glTran.BranchID = ardoc.BranchID;
    glTran.AccountID = customer.DiscTakenAcctID;
    glTran.SubID = customer.DiscTakenSubID;
    glTran.OrigAccountID = ardoc.ARAccountID;
    glTran.OrigSubID = ardoc.ARSubID;
    glTran.DebitAmt = isDebit ? amount : new Decimal?(0M);
    glTran.CuryDebitAmt = isDebit ? curyAmount : new Decimal?(0M);
    glTran.CreditAmt = isDebit ? new Decimal?(0M) : amount;
    glTran.CuryCreditAmt = isDebit ? new Decimal?(0M) : curyAmount;
    glTran.TranType = ardoc.DocType;
    glTran.TranClass = "D";
    glTran.RefNbr = ardoc.RefNbr;
    glTran.TranDesc = ardoc.DocDesc;
    glTran.TranDate = ardoc.DocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran, ardoc.TranPeriodID);
    glTran.CuryInfoID = currencyInfo.CuryInfoID;
    glTran.Released = new bool?(true);
    glTran.ReferenceID = ardoc.CustomerID;
    glTran.ProjectID = ProjectDefaultAttribute.NonProject();
    this.UpdateHistory(glTran, customer);
    this.InsertAdjustmentsCashDiscountTransaction(je, glTran, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = (ARRegister) ardoc
    });
    glTran.CuryDebitAmt = isDebit ? curyAmount : new Decimal?(0M);
    glTran.CuryCreditAmt = isDebit ? new Decimal?(0M) : curyAmount;
    this.UpdateHistory(glTran, customer, currencyInfo);
  }

  private void ProcessAdjustmentCashDiscount(
    JournalEntry je,
    ARAdjust adj,
    ARPayment payment,
    Customer paymentCustomer,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    PX.Objects.CM.Extensions.CurrencyInfo vouch_info)
  {
    PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
    glTran1.SummPost = new bool?(this.SummPost);
    glTran1.BranchID = adj.AdjdBranchID;
    glTran1.AccountID = paymentCustomer.DiscTakenAcctID;
    glTran1.SubID = paymentCustomer.DiscTakenSubID;
    glTran1.OrigAccountID = adj.AdjdARAcct;
    glTran1.OrigSubID = adj.AdjdARSub;
    PX.Objects.GL.GLTran glTran2 = glTran1;
    Decimal? adjgGlSign1 = adj.AdjgGLSign;
    Decimal num1 = 1M;
    Decimal? nullable1 = adjgGlSign1.GetValueOrDefault() == num1 & adjgGlSign1.HasValue ? adj.AdjDiscAmt : new Decimal?(0M);
    glTran2.DebitAmt = nullable1;
    PX.Objects.GL.GLTran glTran3 = glTran1;
    Decimal? adjgGlSign2 = adj.AdjgGLSign;
    Decimal num2 = 1M;
    Decimal? nullable2 = adjgGlSign2.GetValueOrDefault() == num2 & adjgGlSign2.HasValue ? adj.CuryAdjgDiscAmt : new Decimal?(0M);
    glTran3.CuryDebitAmt = nullable2;
    PX.Objects.GL.GLTran glTran4 = glTran1;
    Decimal? adjgGlSign3 = adj.AdjgGLSign;
    Decimal num3 = 1M;
    Decimal? nullable3 = adjgGlSign3.GetValueOrDefault() == num3 & adjgGlSign3.HasValue ? new Decimal?(0M) : adj.AdjDiscAmt;
    glTran4.CreditAmt = nullable3;
    PX.Objects.GL.GLTran glTran5 = glTran1;
    Decimal? adjgGlSign4 = adj.AdjgGLSign;
    Decimal num4 = 1M;
    Decimal? nullable4 = adjgGlSign4.GetValueOrDefault() == num4 & adjgGlSign4.HasValue ? new Decimal?(0M) : adj.CuryAdjgDiscAmt;
    glTran5.CuryCreditAmt = nullable4;
    glTran1.TranType = adj.AdjgDocType;
    glTran1.TranClass = "D";
    glTran1.RefNbr = adj.AdjgRefNbr;
    glTran1.TranDesc = payment.DocDesc;
    glTran1.TranDate = adj.AdjgDocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran1, adj.AdjgTranPeriodID);
    glTran1.CuryInfoID = new_info.CuryInfoID;
    glTran1.Released = new bool?(true);
    glTran1.ReferenceID = payment.CustomerID;
    glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
    this.UpdateHistory(glTran1, paymentCustomer);
    this.InsertAdjustmentsCashDiscountTransaction(je, glTran1, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = (ARRegister) payment,
      ARAdjustRecord = adj
    });
    PX.Objects.GL.GLTran glTran6 = glTran1;
    Decimal? adjgGlSign5 = adj.AdjgGLSign;
    Decimal num5 = 1M;
    Decimal? nullable5 = adjgGlSign5.GetValueOrDefault() == num5 & adjgGlSign5.HasValue ? adj.CuryAdjdDiscAmt : new Decimal?(0M);
    glTran6.CuryDebitAmt = nullable5;
    PX.Objects.GL.GLTran glTran7 = glTran1;
    Decimal? adjgGlSign6 = adj.AdjgGLSign;
    Decimal num6 = 1M;
    Decimal? nullable6 = adjgGlSign6.GetValueOrDefault() == num6 & adjgGlSign6.HasValue ? new Decimal?(0M) : adj.CuryAdjdDiscAmt;
    glTran7.CuryCreditAmt = nullable6;
    this.UpdateHistory(glTran1, paymentCustomer, vouch_info);
  }

  private void ProcessAdjustmentWriteOff(
    JournalEntry je,
    ARAdjust adj,
    ARPayment payment,
    Customer paymentCustomer,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    PX.Objects.CM.Extensions.CurrencyInfo vouch_info)
  {
    Decimal? adjWoAmt = adj.AdjWOAmt;
    Decimal num1 = 0M;
    if (adjWoAmt.GetValueOrDefault() == num1 & adjWoAmt.HasValue)
    {
      Decimal? curyAdjgWoAmt = adj.CuryAdjgWOAmt;
      Decimal num2 = 0M;
      if (curyAdjgWoAmt.GetValueOrDefault() == num2 & curyAdjgWoAmt.HasValue)
        return;
    }
    ARInvoice arInvoice = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this.pe, new object[2]
    {
      (object) adj.AdjdDocType,
      (object) adj.AdjdRefNbr
    }));
    PX.Objects.CS.ReasonCode reasonCode = PXResultset<PX.Objects.CS.ReasonCode>.op_Implicit(PXSelectBase<PX.Objects.CS.ReasonCode, PXSelect<PX.Objects.CS.ReasonCode, Where<PX.Objects.CS.ReasonCode.reasonCodeID, Equal<Required<PX.Objects.CS.ReasonCode.reasonCodeID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) adj.WriteOffReasonCode
    }));
    if (reasonCode == null)
      throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("No reason code with the given id was found in the system. Code: {0}.", new object[1]
      {
        (object) adj.WriteOffReasonCode
      }));
    PX.Objects.CR.Location location1 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this.pe, new object[2]
    {
      (object) arInvoice.CustomerID,
      (object) arInvoice.CustomerLocationID
    }));
    PX.Objects.CR.Standalone.Location location2 = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this.pe, new object[1]
    {
      (object) arInvoice.BranchID
    }));
    if (!(reasonCode.Usage == "B") && !(reasonCode.Usage == "C"))
      throw new PXException("Invalid Reason Code Usage. Only Balance Write-Off or Credit Write-Off codes are expected.");
    object obj = (object) PX.Objects.CS.ReasonCodeSubAccountMaskAttribute.MakeSub<PX.Objects.CS.ReasonCode.subMask>((PXGraph) this.pe, reasonCode.SubMask, new object[3]
    {
      (object) reasonCode.SubID,
      (object) location1.CSalesSubID,
      (object) location2.CMPSalesSubID
    }, new System.Type[3]
    {
      typeof (PX.Objects.CS.ReasonCode.subID),
      typeof (PX.Objects.CR.Location.cSalesSubID),
      typeof (PX.Objects.CR.Location.cMPSalesSubID)
    });
    PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
    glTran1.SummPost = new bool?(this.SummPost);
    glTran1.BranchID = adj.AdjdBranchID;
    glTran1.AccountID = reasonCode.AccountID;
    glTran1.SubID = reasonCode.SubID;
    glTran1.OrigAccountID = adj.AdjdARAcct;
    glTran1.OrigSubID = adj.AdjdARSub;
    PX.Objects.GL.GLTran glTran2 = glTran1;
    Decimal? adjgGlSign1 = adj.AdjgGLSign;
    Decimal num3 = 1M;
    Decimal? nullable1 = adjgGlSign1.GetValueOrDefault() == num3 & adjgGlSign1.HasValue ? adj.AdjWOAmt : new Decimal?(0M);
    glTran2.DebitAmt = nullable1;
    PX.Objects.GL.GLTran glTran3 = glTran1;
    Decimal? adjgGlSign2 = adj.AdjgGLSign;
    Decimal num4 = 1M;
    Decimal? nullable2 = adjgGlSign2.GetValueOrDefault() == num4 & adjgGlSign2.HasValue ? adj.CuryAdjgWOAmt : new Decimal?(0M);
    glTran3.CuryDebitAmt = nullable2;
    PX.Objects.GL.GLTran glTran4 = glTran1;
    Decimal? adjgGlSign3 = adj.AdjgGLSign;
    Decimal num5 = 1M;
    Decimal? nullable3 = adjgGlSign3.GetValueOrDefault() == num5 & adjgGlSign3.HasValue ? new Decimal?(0M) : adj.AdjWOAmt;
    glTran4.CreditAmt = nullable3;
    PX.Objects.GL.GLTran glTran5 = glTran1;
    Decimal? adjgGlSign4 = adj.AdjgGLSign;
    Decimal num6 = 1M;
    Decimal? nullable4 = adjgGlSign4.GetValueOrDefault() == num6 & adjgGlSign4.HasValue ? new Decimal?(0M) : adj.CuryAdjgWOAmt;
    glTran5.CuryCreditAmt = nullable4;
    glTran1.TranType = adj.AdjgDocType;
    glTran1.TranClass = "B";
    glTran1.RefNbr = adj.AdjgRefNbr;
    glTran1.TranDesc = payment.DocDesc;
    glTran1.TranDate = adj.AdjgDocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran1, adj.AdjgTranPeriodID);
    glTran1.CuryInfoID = new_info.CuryInfoID;
    glTran1.Released = new bool?(true);
    glTran1.ReferenceID = payment.CustomerID;
    glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
    this.UpdateHistory(glTran1, paymentCustomer);
    ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).SetValueExt<PX.Objects.GL.GLTran.subID>(glTran1, obj);
    this.InsertAdjustmentsWriteOffTransaction(je, glTran1, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = (ARRegister) payment,
      ARAdjustRecord = adj
    });
    PX.Objects.GL.GLTran glTran6 = glTran1;
    Decimal? adjgGlSign5 = adj.AdjgGLSign;
    Decimal num7 = 1M;
    Decimal? nullable5 = adjgGlSign5.GetValueOrDefault() == num7 & adjgGlSign5.HasValue ? adj.CuryAdjdWOAmt : new Decimal?(0M);
    glTran6.CuryDebitAmt = nullable5;
    PX.Objects.GL.GLTran glTran7 = glTran1;
    adjgGlSign5 = adj.AdjgGLSign;
    Decimal num8 = 1M;
    Decimal? nullable6 = adjgGlSign5.GetValueOrDefault() == num8 & adjgGlSign5.HasValue ? new Decimal?(0M) : adj.CuryAdjdWOAmt;
    glTran7.CuryCreditAmt = nullable6;
    this.UpdateHistory(glTran1, paymentCustomer, vouch_info);
  }

  private void ProcessAdjustmentGOL(
    JournalEntry je,
    ARAdjust adj,
    ARPayment payment,
    Customer paymentCustomer,
    ARRegister adjustedInvoice,
    PX.Objects.CM.Extensions.Currency paycury,
    PX.Objects.CM.Extensions.Currency cury,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    PX.Objects.CM.Extensions.CurrencyInfo vouch_info)
  {
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CurrencyInfo_CuryInfoID).Select(new object[1]
    {
      (object) adj.AdjdOrigCuryInfoID
    }));
    Decimal? nullable1;
    Decimal? nullable2;
    int num1;
    if (currencyInfo.CuryID == new_info.CuryID)
    {
      Decimal? curyRate = currencyInfo.CuryRate;
      nullable1 = new_info.CuryRate;
      if (curyRate.GetValueOrDefault() == nullable1.GetValueOrDefault() & curyRate.HasValue == nullable1.HasValue)
      {
        nullable1 = currencyInfo.RecipRate;
        nullable2 = new_info.RecipRate;
        num1 = nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue ? 1 : 0;
        goto label_4;
      }
    }
    num1 = 0;
label_4:
    bool flag = num1 != 0;
    int? nullable3 = cury.RealGainAcctID;
    if (nullable3.HasValue)
    {
      nullable3 = cury.RealLossAcctID;
      if (nullable3.HasValue && !flag)
      {
        PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
        glTran1.SummPost = new bool?(this.SummPost);
        glTran1.BranchID = adj.AdjdDocType == "SMC" ? adjustedInvoice.BranchID : adj.AdjdBranchID;
        PX.Objects.GL.GLTran glTran2 = glTran1;
        nullable2 = adj.RGOLAmt;
        Decimal num2 = 0M;
        bool? voidAppl;
        if (nullable2.GetValueOrDefault() > num2 & nullable2.HasValue)
        {
          voidAppl = adj.VoidAppl;
          if (!voidAppl.Value)
            goto label_11;
        }
        nullable2 = adj.RGOLAmt;
        Decimal num3 = 0M;
        if (nullable2.GetValueOrDefault() < num3 & nullable2.HasValue)
        {
          voidAppl = adj.VoidAppl;
          if (voidAppl.Value)
            goto label_11;
        }
        int? nullable4 = cury.RealGainAcctID;
        goto label_12;
label_11:
        nullable4 = cury.RealLossAcctID;
label_12:
        glTran2.AccountID = nullable4;
        PX.Objects.GL.GLTran glTran3 = glTran1;
        nullable2 = adj.RGOLAmt;
        Decimal num4 = 0M;
        if (nullable2.GetValueOrDefault() > num4 & nullable2.HasValue)
        {
          voidAppl = adj.VoidAppl;
          if (!voidAppl.Value)
            goto label_17;
        }
        nullable2 = adj.RGOLAmt;
        Decimal num5 = 0M;
        if (nullable2.GetValueOrDefault() < num5 & nullable2.HasValue)
        {
          voidAppl = adj.VoidAppl;
          if (voidAppl.Value)
            goto label_17;
        }
        int? subId = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.realGainSubID>((PXGraph) je, glTran1.BranchID, cury);
        goto label_18;
label_17:
        subId = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.realLossSubID>((PXGraph) je, glTran1.BranchID, cury);
label_18:
        glTran3.SubID = subId;
        glTran1.OrigAccountID = adj.AdjdDocType == "SMC" ? adjustedInvoice.ARAccountID : adj.AdjdARAcct;
        glTran1.OrigSubID = adj.AdjdDocType == "SMC" ? adjustedInvoice.ARSubID : adj.AdjdARSub;
        PX.Objects.GL.GLTran glTran4 = glTran1;
        nullable2 = adj.RGOLAmt;
        Decimal num6 = 0M;
        Decimal? nullable5;
        if (!(nullable2.GetValueOrDefault() < num6 & nullable2.HasValue))
        {
          nullable5 = new Decimal?(0M);
        }
        else
        {
          Decimal num7 = -1M;
          nullable2 = adj.RGOLAmt;
          if (!nullable2.HasValue)
          {
            nullable1 = new Decimal?();
            nullable5 = nullable1;
          }
          else
            nullable5 = new Decimal?(num7 * nullable2.GetValueOrDefault());
        }
        glTran4.CreditAmt = nullable5;
        glTran1.CuryCreditAmt = !object.Equals((object) new_info.CuryID, (object) new_info.BaseCuryID) || object.Equals((object) vouch_info.CuryID, (object) vouch_info.BaseCuryID) ? new Decimal?(0M) : glTran1.CreditAmt;
        PX.Objects.GL.GLTran glTran5 = glTran1;
        nullable2 = adj.RGOLAmt;
        Decimal num8 = 0M;
        Decimal? nullable6 = nullable2.GetValueOrDefault() > num8 & nullable2.HasValue ? adj.RGOLAmt : new Decimal?(0M);
        glTran5.DebitAmt = nullable6;
        glTran1.CuryDebitAmt = !object.Equals((object) new_info.CuryID, (object) new_info.BaseCuryID) || object.Equals((object) vouch_info.CuryID, (object) vouch_info.BaseCuryID) ? new Decimal?(0M) : glTran1.DebitAmt;
        glTran1.TranType = adj.AdjgDocType;
        glTran1.TranClass = "R";
        glTran1.RefNbr = adj.AdjgRefNbr;
        glTran1.TranDesc = payment.DocDesc;
        glTran1.TranDate = adj.AdjgDocDate;
        FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran1, adj.AdjgTranPeriodID);
        glTran1.CuryInfoID = new_info.CuryInfoID;
        glTran1.Released = new bool?(true);
        glTran1.ReferenceID = payment.CustomerID;
        glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
        this.UpdateHistory(glTran1, paymentCustomer);
        this.InsertAdjustmentsGOLTransaction(je, glTran1, new ARReleaseProcess.GLTranInsertionContext()
        {
          ARRegisterRecord = (ARRegister) payment,
          ARAdjustRecord = adj
        });
        glTran1.CuryDebitAmt = new Decimal?(0M);
        glTran1.CuryCreditAmt = new Decimal?(0M);
        this.UpdateHistory(glTran1, paymentCustomer, vouch_info);
        return;
      }
    }
    nullable3 = paycury.RoundingGainAcctID;
    if (!nullable3.HasValue)
      return;
    nullable3 = paycury.RoundingLossAcctID;
    if (!nullable3.HasValue)
      return;
    PX.Objects.GL.GLTran glTran6 = new PX.Objects.GL.GLTran();
    glTran6.SummPost = new bool?(this.SummPost);
    glTran6.BranchID = adj.AdjdDocType == "SMC" ? adjustedInvoice.BranchID : adj.AdjdBranchID;
    PX.Objects.GL.GLTran glTran7 = glTran6;
    nullable2 = adj.RGOLAmt;
    Decimal num9 = 0M;
    bool? voidAppl1;
    if (nullable2.GetValueOrDefault() > num9 & nullable2.HasValue)
    {
      voidAppl1 = adj.VoidAppl;
      if (!voidAppl1.Value)
        goto label_31;
    }
    nullable2 = adj.RGOLAmt;
    Decimal num10 = 0M;
    if (nullable2.GetValueOrDefault() < num10 & nullable2.HasValue)
    {
      voidAppl1 = adj.VoidAppl;
      if (voidAppl1.Value)
        goto label_31;
    }
    int? nullable7 = paycury.RoundingGainAcctID;
    goto label_32;
label_31:
    nullable7 = paycury.RoundingLossAcctID;
label_32:
    glTran7.AccountID = nullable7;
    PX.Objects.GL.GLTran glTran8 = glTran6;
    nullable2 = adj.RGOLAmt;
    Decimal num11 = 0M;
    if (nullable2.GetValueOrDefault() > num11 & nullable2.HasValue)
    {
      voidAppl1 = adj.VoidAppl;
      if (!voidAppl1.Value)
        goto label_37;
    }
    nullable2 = adj.RGOLAmt;
    Decimal num12 = 0M;
    if (nullable2.GetValueOrDefault() < num12 & nullable2.HasValue)
    {
      voidAppl1 = adj.VoidAppl;
      if (voidAppl1.Value)
        goto label_37;
    }
    int? subId1 = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingGainSubID>((PXGraph) je, glTran6.BranchID, paycury);
    goto label_38;
label_37:
    subId1 = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingLossSubID>((PXGraph) je, glTran6.BranchID, paycury);
label_38:
    glTran8.SubID = subId1;
    glTran6.OrigAccountID = adj.AdjdDocType == "SMC" ? adjustedInvoice.ARAccountID : adj.AdjdARAcct;
    glTran6.OrigSubID = adj.AdjdDocType == "SMC" ? adjustedInvoice.ARSubID : adj.AdjdARSub;
    PX.Objects.GL.GLTran glTran9 = glTran6;
    nullable2 = adj.RGOLAmt;
    Decimal num13 = 0M;
    Decimal? nullable8;
    if (!(nullable2.GetValueOrDefault() < num13 & nullable2.HasValue))
    {
      nullable8 = new Decimal?(0M);
    }
    else
    {
      Decimal num14 = -1M;
      nullable2 = adj.RGOLAmt;
      if (!nullable2.HasValue)
      {
        nullable1 = new Decimal?();
        nullable8 = nullable1;
      }
      else
        nullable8 = new Decimal?(num14 * nullable2.GetValueOrDefault());
    }
    glTran9.CreditAmt = nullable8;
    glTran6.CuryCreditAmt = !object.Equals((object) new_info.CuryID, (object) new_info.BaseCuryID) || object.Equals((object) vouch_info.CuryID, (object) vouch_info.BaseCuryID) ? new Decimal?(0M) : glTran6.CreditAmt;
    PX.Objects.GL.GLTran glTran10 = glTran6;
    nullable2 = adj.RGOLAmt;
    Decimal num15 = 0M;
    Decimal? nullable9 = nullable2.GetValueOrDefault() > num15 & nullable2.HasValue ? adj.RGOLAmt : new Decimal?(0M);
    glTran10.DebitAmt = nullable9;
    glTran6.CuryDebitAmt = !object.Equals((object) new_info.CuryID, (object) new_info.BaseCuryID) || object.Equals((object) vouch_info.CuryID, (object) vouch_info.BaseCuryID) ? new Decimal?(0M) : glTran6.DebitAmt;
    glTran6.TranType = adj.AdjgDocType;
    glTran6.TranClass = "R";
    glTran6.RefNbr = adj.AdjgRefNbr;
    glTran6.TranDesc = payment.DocDesc;
    glTran6.TranDate = adj.AdjgDocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran6, adj.AdjgTranPeriodID);
    glTran6.CuryInfoID = new_info.CuryInfoID;
    glTran6.Released = new bool?(true);
    glTran6.ReferenceID = payment.CustomerID;
    glTran6.ProjectID = ProjectDefaultAttribute.NonProject();
    this.UpdateHistory(glTran6, paymentCustomer);
    this.InsertAdjustmentsGOLTransaction(je, glTran6, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = (ARRegister) payment,
      ARAdjustRecord = adj
    });
    glTran6.CuryDebitAmt = new Decimal?(0M);
    glTran6.CuryCreditAmt = new Decimal?(0M);
    this.UpdateHistory(glTran6, paymentCustomer, vouch_info);
  }

  private void ProcessAdjustmentsRounding(
    JournalEntry je,
    ARRegister doc,
    ARAdjust prev_adj,
    ARPayment ardoc,
    Customer vend,
    PX.Objects.CM.Extensions.Currency paycury,
    PX.Objects.CM.Extensions.CurrencyInfo prev_info,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    Decimal? amount,
    bool? isReversed = false)
  {
    if (prev_adj.VoidAppl.GetValueOrDefault() || object.Equals((object) new_info.CuryID, (object) new_info.BaseCuryID))
      throw new PXException("The document cannot be released because unexpected rounding difference has appeared.");
    ARReleaseProcess.UpdateARBalances((PXGraph) this, doc, amount, new Decimal?(0M));
    ARAdjust arAdjust1 = prev_adj;
    Decimal? adjAmt = arAdjust1.AdjAmt;
    Decimal? nullable1 = amount;
    arAdjust1.AdjAmt = adjAmt.HasValue & nullable1.HasValue ? new Decimal?(adjAmt.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable2;
    if (isReversed.GetValueOrDefault())
    {
      Decimal? nullable3 = amount;
      nullable2 = nullable3.HasValue ? new Decimal?(-nullable3.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable2 = amount;
    Decimal? nullable4 = nullable2;
    ARAdjust arAdjust2 = prev_adj;
    Decimal? nullable5 = arAdjust2.RGOLAmt;
    Decimal? nullable6 = nullable4;
    arAdjust2.RGOLAmt = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
    prev_adj = (ARAdjust) ((PXGraph) this).Caches[typeof (ARAdjust)].Update((object) prev_adj);
    foreach (ARTranPost arTranPost1 in ((PXCache) GraphHelper.Caches<ARTranPost>((PXGraph) this)).Inserted.Cast<ARTranPost>().Where<ARTranPost>((Func<ARTranPost, bool>) (d =>
    {
      Guid? refNoteId = d.RefNoteID;
      Guid? noteId = prev_adj.NoteID;
      if (refNoteId.HasValue != noteId.HasValue)
        return false;
      return !refNoteId.HasValue || refNoteId.GetValueOrDefault() == noteId.GetValueOrDefault();
    })))
    {
      arTranPost1.Amt = arTranPost1.Type == "R" || arTranPost1.Type == "X" ? new Decimal?(0M) : prev_adj.AdjAmt;
      ARTranPost arTranPost2 = arTranPost1;
      Decimal? nullable7;
      if (!(arTranPost1.Type == "X"))
      {
        nullable7 = prev_adj.RGOLAmt;
      }
      else
      {
        Decimal? rgolAmt = prev_adj.RGOLAmt;
        if (!rgolAmt.HasValue)
        {
          nullable5 = new Decimal?();
          nullable7 = nullable5;
        }
        else
          nullable7 = new Decimal?(-rgolAmt.GetValueOrDefault());
      }
      arTranPost2.RGOLAmt = nullable7;
    }
    ARRegister arRegister1 = (ARRegister) ((PXSelectBase) this.ARDocument).Cache.Locate((object) new ARRegister()
    {
      DocType = prev_adj.AdjdDocType,
      RefNbr = prev_adj.AdjdRefNbr
    });
    Decimal? nullable8;
    if (arRegister1 != null)
    {
      ARRegister arRegister2 = arRegister1;
      Decimal? rgolAmt = arRegister2.RGOLAmt;
      nullable8 = nullable4;
      arRegister2.RGOLAmt = rgolAmt.HasValue & nullable8.HasValue ? new Decimal?(rgolAmt.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase) this.ARDocument).Cache.Update((object) arRegister1);
    }
    PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
    glTran1.SummPost = new bool?(this.SummPost);
    glTran1.BranchID = ardoc.BranchID;
    PX.Objects.GL.GLTran glTran2 = glTran1;
    nullable8 = nullable4;
    Decimal num1 = 0M;
    int? nullable9 = nullable8.GetValueOrDefault() < num1 & nullable8.HasValue ? paycury.RoundingLossAcctID : paycury.RoundingGainAcctID;
    glTran2.AccountID = nullable9;
    PX.Objects.GL.GLTran glTran3 = glTran1;
    nullable8 = nullable4;
    Decimal num2 = 0M;
    int? nullable10 = nullable8.GetValueOrDefault() < num2 & nullable8.HasValue ? GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingLossSubID>((PXGraph) je, glTran1.BranchID, paycury) : GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingGainSubID>((PXGraph) je, glTran1.BranchID, paycury);
    glTran3.SubID = nullable10;
    glTran1.OrigAccountID = prev_adj.AdjdARAcct;
    glTran1.OrigSubID = prev_adj.AdjdARSub;
    PX.Objects.GL.GLTran glTran4 = glTran1;
    nullable8 = nullable4;
    Decimal num3 = 0M;
    Decimal? nullable11 = nullable8.GetValueOrDefault() > num3 & nullable8.HasValue ? nullable4 : new Decimal?(0M);
    glTran4.CreditAmt = nullable11;
    glTran1.CuryCreditAmt = new Decimal?(0M);
    PX.Objects.GL.GLTran glTran5 = glTran1;
    nullable8 = nullable4;
    Decimal num4 = 0M;
    Decimal? nullable12;
    if (!(nullable8.GetValueOrDefault() < num4 & nullable8.HasValue))
    {
      nullable12 = new Decimal?(0M);
    }
    else
    {
      nullable8 = nullable4;
      nullable12 = nullable8.HasValue ? new Decimal?(-nullable8.GetValueOrDefault()) : new Decimal?();
    }
    glTran5.DebitAmt = nullable12;
    glTran1.CuryDebitAmt = new Decimal?(0M);
    glTran1.TranType = prev_adj.AdjgDocType;
    glTran1.TranClass = "R";
    glTran1.RefNbr = prev_adj.AdjgRefNbr;
    glTran1.TranDesc = ardoc.DocDesc;
    glTran1.TranDate = prev_adj.AdjgDocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran1, prev_adj.AdjgTranPeriodID);
    glTran1.CuryInfoID = new_info.CuryInfoID;
    glTran1.Released = new bool?(true);
    glTran1.ReferenceID = ardoc.CustomerID;
    glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
    this.UpdateHistory(glTran1, vend);
    this.UpdateHistory(glTran1, vend, prev_info);
    this.InsertAdjustmentsRoundingTransaction(je, glTran1, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = doc,
      ARAdjustRecord = prev_adj
    });
    PX.Objects.GL.GLTran glTran6 = new PX.Objects.GL.GLTran();
    glTran6.SummPost = new bool?(true);
    glTran6.ZeroPost = new bool?(false);
    glTran6.BranchID = ardoc.BranchID;
    glTran6.AccountID = ardoc.ARAccountID;
    glTran6.SubID = ardoc.ARSubID;
    glTran6.ReclassificationProhibited = new bool?(true);
    PX.Objects.GL.GLTran glTran7 = glTran6;
    nullable8 = nullable4;
    Decimal num5 = 0M;
    Decimal? nullable13 = nullable8.GetValueOrDefault() > num5 & nullable8.HasValue ? nullable4 : new Decimal?(0M);
    glTran7.DebitAmt = nullable13;
    glTran6.CuryDebitAmt = new Decimal?(0M);
    PX.Objects.GL.GLTran glTran8 = glTran6;
    nullable8 = nullable4;
    Decimal num6 = 0M;
    Decimal? nullable14;
    if (!(nullable8.GetValueOrDefault() < num6 & nullable8.HasValue))
    {
      nullable14 = new Decimal?(0M);
    }
    else
    {
      nullable8 = nullable4;
      nullable14 = nullable8.HasValue ? new Decimal?(-nullable8.GetValueOrDefault()) : new Decimal?();
    }
    glTran8.CreditAmt = nullable14;
    glTran6.CuryCreditAmt = new Decimal?(0M);
    glTran6.TranType = prev_adj.AdjgDocType;
    glTran6.TranClass = "P";
    glTran6.RefNbr = prev_adj.AdjgRefNbr;
    glTran6.TranDesc = ardoc.DocDesc;
    glTran6.TranDate = prev_adj.AdjgDocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran6, prev_adj.AdjgTranPeriodID);
    glTran6.CuryInfoID = new_info.CuryInfoID;
    glTran6.Released = new bool?(true);
    glTran6.ReferenceID = ardoc.CustomerID;
    glTran6.OrigAccountID = prev_adj.AdjdARAcct;
    glTran6.OrigSubID = prev_adj.AdjdARSub;
    glTran6.ProjectID = ProjectDefaultAttribute.NonProject();
    this.UpdateHistory(glTran6, vend);
    this.UpdateHistory(glTran6, vend, new_info);
    this.InsertAdjustmentsRoundingTransaction(je, glTran6, new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = doc,
      ARAdjustRecord = prev_adj
    });
  }

  private void SegregateBatch(
    JournalEntry je,
    int? branchID,
    string curyID,
    DateTime? docDate,
    string finPeriodID,
    string description,
    PX.Objects.CM.Extensions.CurrencyInfo curyInfo,
    PX.Objects.GL.Batch consolidatingBatch)
  {
    if (((PXGraph) je).Caches[typeof (PX.Objects.GL.GLTran)].IsInsertedUpdatedDeleted)
      ((PXAction) je.Save).Press();
    JournalEntry.SegregateBatch(je, "AR", branchID, curyID, docDate, finPeriodID, description, curyInfo.GetCM(), consolidatingBatch);
  }

  public virtual List<ARRegister> ReleaseDocProc(
    JournalEntry je,
    ARRegister ardoc,
    List<PX.Objects.GL.Batch> pmBatchList)
  {
    return this.ReleaseDocProc(je, ardoc, pmBatchList, (PX.Objects.AR.ARDocumentRelease.ARMassProcessReleaseTransactionScopeDelegate) null);
  }

  /// <summary>
  /// Performs basic checks that the document is releasable.
  /// Otherwise, throws error-specific exceptions.
  /// </summary>
  protected virtual void PerformBasicReleaseChecks(PXGraph selectGraph, ARRegister document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (document.Hold.GetValueOrDefault())
      throw new ReleaseException("Document is On Hold and cannot be released.", Array.Empty<object>());
    if (document.Status == "D" || document.Status == "J")
      throw new ReleaseException("The document cannot be created on this form because an approval map is active on the Accounts Receivable Preferences (AR101000) form.", Array.Empty<object>());
    if (document.IsMigratedRecord.GetValueOrDefault() && !document.Released.GetValueOrDefault() && !this.IsMigrationMode.GetValueOrDefault())
      throw new ReleaseException("The document cannot be released because it has been created in migration mode but now migration mode is deactivated. Delete the document or activate migration mode on the Accounts Receivable Preferences (AR101000) form.", Array.Empty<object>());
    if (!document.IsMigratedRecord.GetValueOrDefault() && this.IsMigrationMode.GetValueOrDefault())
      throw new ReleaseException("The document cannot be released because it was created when migration mode was deactivated. To release the document, clear the Activate Migration Mode check box on the Accounts Receivable Preferences (AR101000) form.", Array.Empty<object>());
    if (document.RetainageApply.GetValueOrDefault() && !PXAccess.FeatureInstalled<FeaturesSet.retainage>())
      throw new ReleaseException("A document with nonzero retainage amount cannot be processed if the Retainage Support feature is disabled on the Enable/Disable Features (CS100000) form.", Array.Empty<object>());
    if (AccountAttribute.GetAccount((PXGraph) this, document.ARAccountID).IsCashAccount.GetValueOrDefault())
      throw new ReleaseException("Specify a correct {0} account. It must not be a cash account.", new object[1]
      {
        (object) "AR"
      });
    ARPayment arPayment = PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelect<ARPayment, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>.Config>.Select(selectGraph, new object[2]
    {
      (object) document.DocType,
      (object) document.RefNbr
    }));
    bool? nullable;
    if (arPayment != null && arPayment.DocType != "CRM" && arPayment.DocType != "SMB" && arPayment.DocType != "PPI" && arPayment.DocType != "RPM")
    {
      nullable = this.arsetup.RequireExtRef;
      if (nullable.GetValueOrDefault() && string.IsNullOrEmpty(arPayment.ExtRefNbr))
        throw new ReleaseException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<ARPayment.extRefNbr>(selectGraph.Caches[typeof (ARPayment)])
        });
    }
    if (arPayment != null && arPayment.CashAccountID.HasValue)
    {
      PX.Objects.GL.Account account = AccountAttribute.GetAccount((PXGraph) this, PX.Objects.CA.CashAccount.PK.Find((PXGraph) this, arPayment.CashAccountID).AccountID);
      nullable = account.IsCashAccount;
      if (!nullable.GetValueOrDefault())
        throw new PXException("This cash account is mapped to the {0} GL account for which the Cash Account check box is cleared on the Chart of Accounts (GL202500) form.", new object[1]
        {
          (object) account.AccountCD
        });
    }
    if (CCProcessingHelper.IntegratedProcessingActivated(this.arsetup) && arPayment != null)
    {
      nullable = arPayment.SyncLock;
      if (nullable.GetValueOrDefault())
        throw new ReleaseException("The payment is locked for editing. Please wait for the external transaction result.\r\nIf the payment does not get unlocked, click Actions > Validate Card Payment to request the transaction result from the processing center.", Array.Empty<object>());
    }
    List<PX.Objects.GL.Branch> list = GraphHelper.RowCast<PX.Objects.GL.Branch>((IEnumerable) PXSelectBase<PX.Objects.GL.Branch, PXViewOf<PX.Objects.GL.Branch>.BasedOn<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ARAdjust>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Branch.branchID, Equal<ARAdjust.adjdBranchID>>>>>.Or<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<ARAdjust.adjgBranchID>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<P.AsString>>>>, And<BqlOperand<ARAdjust.adjgRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.GL.Branch.active, IBqlBool>.IsNotEqual<True>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) document.DocType,
      (object) document.RefNbr
    })).ToList<PX.Objects.GL.Branch>();
    if (list.Any<PX.Objects.GL.Branch>())
    {
      string empty = string.Empty;
      throw new ReleaseException("The application cannot be released because the following branches are not active: {0}.", new object[1]
      {
        (object) string.Join(", ", list.Select<PX.Objects.GL.Branch, string>((Func<PX.Objects.GL.Branch, string>) (x => x.BranchCD.Trim())).Distinct<string>())
      });
    }
  }

  private void UpdateCCSpecificFields(ARRegister doc)
  {
    bool flag = false;
    if (!(doc.DocType == "PMT") && !(doc.DocType == "PPM") && !(doc.DocType == "CSL"))
      return;
    ARPayment arPayment1 = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Select(new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }));
    if (arPayment1 != null)
    {
      DateTime? nullable1 = arPayment1.CCReauthDate;
      if (nullable1.HasValue)
      {
        arPayment1 = (ARPayment) ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.Extend<ARRegister>(doc);
        ARPayment arPayment2 = arPayment1;
        nullable1 = new DateTime?();
        DateTime? nullable2 = nullable1;
        arPayment2.CCReauthDate = nullable2;
        arPayment1.CCReauthTriesLeft = new int?(0);
        flag = true;
      }
    }
    if (arPayment1 != null && arPayment1.IsCCUserAttention.GetValueOrDefault())
    {
      if (!flag)
        arPayment1 = (ARPayment) ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.Extend<ARRegister>(doc);
      arPayment1.IsCCUserAttention = new bool?(false);
      flag = true;
    }
    if (!flag)
      return;
    ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.Update((object) arPayment1);
    ((PXSelectBase) this.ARDocument).Cache.SetStatus((object) doc, (PXEntryStatus) 0);
  }

  private void CheckOpenForReviewTrans(ARRegister doc)
  {
    bool? integratedCcProcessing = this.arsetup.IntegratedCCProcessing;
    bool flag = false;
    if (!(integratedCcProcessing.GetValueOrDefault() == flag & integratedCcProcessing.HasValue) && this.DocWithOpenForReviewTrans(doc))
      throw new PXException("The transaction is held for review by the processing center. Use the processing center interface to approve or reject the transaction. After the transaction is processed by the processing center, use the Validate Card Payment action to update the processing status.");
  }

  private bool DocWithOpenForReviewTrans(ARRegister doc)
  {
    return ExternalTranHelper.GetActiveTransactionState((PXGraph) this, (IEnumerable<IExternalTransaction>) GraphHelper.RowCast<ExternalTransaction>((IEnumerable) ((PXSelectBase<ExternalTransaction>) new PXSelect<ExternalTransaction, Where<ExternalTransaction.refNbr, Equal<Required<ARRegister.refNbr>>, And<ExternalTransaction.docType, Equal<Required<ARRegister.docType>>>>, OrderBy<Desc<ExternalTransaction.transactionID>>>((PXGraph) this)).Select(new object[2]
    {
      (object) doc.RefNbr,
      (object) doc.DocType
    }))).IsOpenForReview;
  }

  public virtual ARRegister OnBeforeRelease(ARRegister ardoc) => ardoc;

  /// <summary>
  /// Common entry point.
  /// The method to release both types of documents - invoices and payments.
  /// </summary>
  public virtual List<ARRegister> ReleaseDocProc(
    JournalEntry je,
    ARRegister ardoc,
    List<PX.Objects.GL.Batch> pmBatchList,
    PX.Objects.AR.ARDocumentRelease.ARMassProcessReleaseTransactionScopeDelegate onreleasecomplete)
  {
    List<ARRegister> arRegisterList = (List<ARRegister>) null;
    this.PerformBasicReleaseChecks((PXGraph) je, ardoc);
    Decimal? origDocAmt;
    if (EnumerableExtensions.IsIn<string>(ardoc.DocType, "INV", "DRM", "CRM"))
    {
      origDocAmt = ardoc.OrigDocAmt;
      Decimal num = 0M;
      if (origDocAmt.GetValueOrDefault() < num & origDocAmt.HasValue)
        throw new PXException("The document amount cannot be less than zero.");
    }
    if (this.IsMigrationMode.GetValueOrDefault())
      ((PXGraph) je).SetOffline();
    bool flag1 = false;
    this._oldInvoiceRefresher = new OldInvoiceDateRefresher();
    bool flag2 = ardoc.DocType == "CRM" && !ardoc.Released.GetValueOrDefault();
    ARRegister arRegister1 = PXCache<ARRegister>.CreateCopy(ardoc);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      PXCache<ARRegister>.StoreOriginal((PXGraph) this, arRegister1);
      ((PXSelectBase) this.ARDocument).Cache.SetStatus((object) arRegister1, (PXEntryStatus) 1);
      ARRegister ardoc1 = arRegister1;
      origDocAmt = arRegister1.OrigDocAmt;
      Decimal? BalanceAmt = origDocAmt.HasValue ? new Decimal?(-origDocAmt.GetValueOrDefault()) : new Decimal?();
      this.UpdateARBalances(ardoc1, BalanceAmt);
      List<PMRegister> pmRegisterList = new List<PMRegister>();
      foreach (PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account> res in ((PXSelectBase<ARInvoice>) this.ARInvoice_DocType_RefNbr).Select(new object[2]
      {
        (object) arRegister1.DocType,
        (object) arRegister1.RefNbr
      }))
      {
        Customer customer = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account>.op_Implicit(res);
        PX.Objects.CM.Extensions.CurrencyInfo curyInfo = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account>.op_Implicit(res);
        if (customer.Status == "I" || customer.Status == "H" || customer.Status == "C" && arRegister1.DocType != "FCH" && arRegister1.DocType != "CRM")
          throw new PXSetPropertyException("The customer status is '{0}'.", new object[1]
          {
            (object) new CustomerStatus.ListAttribute().ValueLabelDic[customer.Status]
          });
        ARInvoice arInvoice = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account>.op_Implicit(res);
        PX.Objects.CS.Terms terms = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account>.op_Implicit(res);
        bool? nullable = arRegister1.Released;
        bool flag3 = false;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
          this.SegregateBatch(je, arRegister1.BranchID, arRegister1.CuryID, arRegister1.DocDate, arRegister1.FinPeriodID, arRegister1.DocDesc, curyInfo, (PX.Objects.GL.Batch) null);
        if (!this._IsIntegrityCheck && arInvoice.DocType != "CSL" && arInvoice.DocType != "RCS" && arInvoice.DocType != "SMC")
        {
          nullable = customer.AutoApplyPayments;
          if (nullable.GetValueOrDefault())
          {
            nullable = arInvoice.Released;
            bool flag4 = false;
            if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
            {
              nullable = arInvoice.IsMigratedRecord;
              if (!nullable.GetValueOrDefault() && terms.InstallmentType == "S")
              {
                ((PXGraph) this.InvoiceEntryGraph).Clear();
                ((PXSelectBase<ARInvoice>) this.InvoiceEntryGraph.Document).Current = arInvoice;
                if (((PXSelectBase) this.InvoiceEntryGraph.Adjustments_Inv).View.SelectSingle(Array.Empty<object>()) == null)
                  this.InvoiceEntryGraph.LoadInvoicesProc();
                ((PXAction) this.InvoiceEntryGraph.Save).Press();
                arRegister1 = (ARRegister) ((PXSelectBase<ARInvoice>) this.InvoiceEntryGraph.Document).Current;
                arRegister1.ReleasedToVerify = new bool?(false);
                arRegisterList = this.ReleaseInvoice(je, arRegister1, new PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account>(((PXSelectBase<ARInvoice>) this.InvoiceEntryGraph.Document).Current, curyInfo, terms, customer, PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account>.op_Implicit(res)), pmRegisterList);
                goto label_20;
              }
            }
          }
        }
        arRegisterList = this.ReleaseInvoice(je, arRegister1, res, pmRegisterList);
label_20:
        ((PXSelectBase<ARInvoice>) this.ARInvoice_DocType_RefNbr).Current = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account>.op_Implicit(res);
      }
      ARReleaseProcess.Amount amount = new ARReleaseProcess.Amount();
      foreach (PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount> pxResult1 in ((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Select(new object[3]
      {
        (object) arRegister1.DocType,
        (object) arRegister1.RefNbr,
        (object) arRegister1.CustomerID
      }))
      {
        bool? nullable1;
        if (arRegister1.DocType == "PPI")
        {
          nullable1 = arRegister1.Released;
          bool flag5 = false;
          if (nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue)
            continue;
        }
        ARPayment payment = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(pxResult1);
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(pxResult1);
        PX.Objects.CM.Extensions.Currency paycury = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(pxResult1);
        Customer paymentCustomer = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(pxResult1);
        PX.Objects.CA.CashAccount cashAccount = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(pxResult1);
        ((PXGraph) this).GetExtension<ARReleaseProcess.MultiCurrency>().StoreResult(PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(pxResult1));
        payment.OrigReleased = payment.Released;
        ((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Current = payment;
        Tuple<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo> lastAdjustment = new Tuple<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo>(new ARAdjust(), new PX.Objects.CM.Extensions.CurrencyInfo());
        if (paymentCustomer.Status == "I")
          throw new PXSetPropertyException("The customer status is '{0}'.", new object[1]
          {
            (object) new CustomerStatus.ListAttribute().ValueLabelDic[paymentCustomer.Status]
          });
        nullable1 = arRegister1.Released;
        PX.Objects.CM.CurrencyInfo currencyInfoCopyForGl;
        if (!nullable1.GetValueOrDefault())
        {
          if (EnumerableExtensions.IsIn<string>(arRegister1.DocType, "PMT", "PPM", "RPM", "REF", "VRF", new string[1]
          {
            "PPI"
          }))
          {
            this.SegregateBatch(je, arRegister1.BranchID, arRegister1.CuryID, payment.DocDate, payment.FinPeriodID, arRegister1.DocDesc, currencyInfo, (PX.Objects.GL.Batch) null);
            JournalEntry je1 = je;
            PX.Objects.CM.Extensions.CurrencyInfo info1 = currencyInfo;
            nullable1 = new bool?();
            bool? baseCalc = nullable1;
            currencyInfoCopyForGl = this.GetCurrencyInfoCopyForGL(je1, info1, baseCalc);
            PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount> res = new PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>(payment, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(currencyInfoCopyForGl), paycury, paymentCustomer, cashAccount);
            if (!this._IsIntegrityCheck)
            {
              nullable1 = paymentCustomer.AutoApplyPayments;
              if (nullable1.GetValueOrDefault() && payment.DocType == "PMT")
              {
                nullable1 = payment.Released;
                if (!nullable1.GetValueOrDefault())
                {
                  nullable1 = payment.IsMigratedRecord;
                  if (!nullable1.GetValueOrDefault())
                  {
                    ((PXGraph) this.pe).Clear();
                    ((PXGraph) this.pe).SelectTimeStamp();
                    bool flag6 = false;
                    if (PXTransactionScope.IsScoped)
                    {
                      PX.Objects.CM.Extensions.CurrencyInfo info2 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.pe.CurrencyInfo_CuryInfoID).Select(new object[1]
                      {
                        (object) payment.CuryInfoID
                      }));
                      ((PXGraph) this).GetExtension<ARReleaseProcess.MultiCurrency>().StoreResult(info2);
                      foreach (PXResult<ARAdjust> pxResult2 in ((PXSelectBase<ARAdjust>) this.pe.Adjustments_Raw).Select(new object[2]
                      {
                        (object) payment.DocType,
                        (object) payment.RefNbr
                      }))
                      {
                        ARAdjust arAdjust = PXResult<ARAdjust>.op_Implicit(pxResult2);
                        if (!flag6)
                        {
                          flag6 = true;
                          ((PXSelectBase) this.pe.CurrencyInfo_CuryInfoID).View.Clear();
                        }
                        PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.pe.CurrencyInfo_CuryInfoID).Select(new object[1]
                        {
                          (object) arAdjust.AdjdCuryInfoID
                        }));
                      }
                      PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.pe.CurrencyInfo_CuryInfoID).Select(new object[1]
                      {
                        (object) payment.CuryInfoID
                      }));
                    }
                    else
                      flag6 = ((PXSelectBase) this.pe.Adjustments_Raw).View.SelectSingle(Array.Empty<object>()) != null;
                    ((PXSelectBase<ARPayment>) this.pe.Document).Current = payment;
                    if (!flag6)
                      this.pe.LoadInvoicesProc(false);
                    ((PXSelectBase) this.pe.ARInvoice_DocType_RefNbr).Cache.Updated.Cast<ARInvoice>().ToList<ARInvoice>();
                    ((PXAction) this.pe.Save).Press();
                    arRegister1 = (ARRegister) ((PXSelectBase<ARPayment>) this.pe.Document).Current;
                    arRegister1.ReleasedToVerify = new bool?(false);
                    res = new PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>(((PXSelectBase<ARPayment>) this.pe.Document).Current, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(currencyInfoCopyForGl), paycury, paymentCustomer, cashAccount);
                  }
                }
              }
            }
            this.ProcessPayment(je, arRegister1, res);
            this.SaveBatchForDocument(je, arRegister1);
            this.ReplaceCADailySummaryCache(je);
            SortedDictionary<string, List<PXResult<ARAdjust>>> sortedDictionary1 = new SortedDictionary<string, List<PXResult<ARAdjust>>>();
            SortedDictionary<string, DateTime?> sortedDictionary2 = new SortedDictionary<string, DateTime?>();
            this.InsertCurrencyInfoIntoCache(arRegister1, currencyInfo);
            foreach (PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran, CurrencyInfo2> pxResult3 in ((PXSelectBase<ARAdjust>) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Select(new object[4]
            {
              (object) arRegister1.DocType,
              (object) arRegister1.RefNbr,
              (object) this._IsIntegrityCheck,
              (object) arRegister1.AdjCntr
            }))
            {
              ARAdjust adj = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran, CurrencyInfo2>.op_Implicit(pxResult3);
              ARRegister arRegister2 = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, ARRegister, ARInvoice, ARPayment, ARTran, CurrencyInfo2>.op_Implicit(pxResult3);
              this.SetAdjgPeriodsFromLatestApplication(arRegister1, adj);
              nullable1 = arRegister2.Released;
              if (nullable1.GetValueOrDefault() || adj.IsSelfAdjustment())
              {
                List<PXResult<ARAdjust>> pxResultList;
                if (!sortedDictionary1.TryGetValue(adj.AdjgTranPeriodID, out pxResultList))
                  sortedDictionary1[adj.AdjgTranPeriodID] = pxResultList = new List<PXResult<ARAdjust>>();
                pxResultList.Add((PXResult<ARAdjust>) pxResult3);
                DateTime? adjgDocDate;
                sortedDictionary2[adj.AdjgTranPeriodID] = adjgDocDate = adj.AdjgDocDate;
                if (DateTime.Compare(adj.AdjgDocDate.Value, adjgDocDate.Value) > 0)
                  sortedDictionary2[adj.AdjgTranPeriodID] = adj.AdjgDocDate;
                nullable1 = arRegister1.OpenDoc;
                bool flag7 = false;
                if (nullable1.GetValueOrDefault() == flag7 & nullable1.HasValue && arRegister1.DocType == "RPM")
                {
                  arRegister1.OpenDoc = new bool?(true);
                  arRegister1.CuryDocBal = arRegister1.CuryOrigDocAmt;
                  arRegister1.DocBal = arRegister1.OrigDocAmt;
                  this.RaisePaymentEvent(arRegister1, (SelectedEntityEvent<ARPayment>) PXEntityEventBase<ARPayment>.Container<ARPayment.Events>.Select((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment.Events>>>) (e => e.OpenDocument)));
                }
              }
            }
            this.CheckVoidedDoucmentAmountDiscrepancies(arRegister1);
            PX.Objects.GL.Batch current1 = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current;
            using (SortedDictionary<string, List<PXResult<ARAdjust>>>.Enumerator enumerator = sortedDictionary1.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                KeyValuePair<string, List<PXResult<ARAdjust>>> current2 = enumerator.Current;
                FinPeriod valueOrRaiseError = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(arRegister1.BranchID), current2.Key).GetValueOrRaiseError();
                this.SegregateBatch(je, arRegister1.BranchID, arRegister1.CuryID, sortedDictionary2[current2.Key], valueOrRaiseError.FinPeriodID, arRegister1.DocDesc, currencyInfo, current1);
                PXResultset<ARAdjust> adjustments = new PXResultset<ARAdjust>();
                adjustments.AddRange((IEnumerable<PXResult<ARAdjust>>) current2.Value);
                currencyInfoCopyForGl = this.GetCurrencyInfoCopyForGL(je, currencyInfo);
                lastAdjustment = this.ProcessAdjustments(je, adjustments, arRegister1, payment, paymentCustomer, currencyInfoCopyForGl, paycury);
              }
              goto label_80;
            }
          }
        }
        if (arRegister1.DocType != "CSL" && arRegister1.DocType != "RCS")
          this.SegregateBatch(je, arRegister1.BranchID, arRegister1.CuryID, payment.AdjDate, payment.AdjFinPeriodID, payment.DocDesc, currencyInfo, (PX.Objects.GL.Batch) null);
        JournalEntry je2 = je;
        PX.Objects.CM.Extensions.CurrencyInfo info = currencyInfo;
        nullable1 = new bool?();
        bool? baseCalc1 = nullable1;
        currencyInfoCopyForGl = this.GetCurrencyInfoCopyForGL(je2, info, baseCalc1);
        PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount> res1 = new PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>(payment, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(currencyInfoCopyForGl), paycury, paymentCustomer, cashAccount);
        this.ProcessPayment(je, arRegister1, res1);
        foreach (PXResult<ARAdjust> pxResult4 in ((PXSelectBase<ARAdjust>) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Select(new object[4]
        {
          (object) arRegister1.DocType,
          (object) arRegister1.RefNbr,
          (object) this._IsIntegrityCheck,
          (object) arRegister1.AdjCntr
        }))
        {
          ARAdjust adj = PXResult<ARAdjust>.op_Implicit(pxResult4);
          this.SetAdjgPeriodsFromLatestApplication(arRegister1, adj);
        }
        PXResultset<ARAdjust> adjustments1 = ((PXSelectBase<ARAdjust>) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Select(new object[4]
        {
          (object) arRegister1.DocType,
          (object) arRegister1.RefNbr,
          (object) this._IsIntegrityCheck,
          (object) arRegister1.AdjCntr
        });
        this.CheckVoidedDoucmentAmountDiscrepancies(arRegister1);
        lastAdjustment = this.ProcessAdjustments(je, adjustments1, arRegister1, payment, paymentCustomer, currencyInfoCopyForGl, paycury);
label_80:
        ARReleaseProcess.Amount docBal = !arRegister1.IsMigratedRecord.GetValueOrDefault() ? new ARReleaseProcess.Amount(arRegister1.CuryDocBal, arRegister1.DocBal) : new ARReleaseProcess.Amount(arRegister1.CuryInitDocBal, arRegister1.InitDocBal);
        if (arRegister1.DocType == "SMB" && lastAdjustment.Item1.Voided.GetValueOrDefault())
          this.ProcessVoidWOTranPost(arRegister1, lastAdjustment.Item1);
        Decimal? nullable2;
        if (arRegister1.DocType == "VRF" || arRegister1.DocType == "RPM")
        {
          nullable2 = docBal.Base;
          Decimal num = 0M;
          if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
            this.ProcessVoidPaymentTranPost(arRegister1, docBal);
        }
        if (arRegister1.IsRetainageDocument.GetValueOrDefault() && !arRegister1.IsRetainageReversing.GetValueOrDefault())
        {
          ARRegister retainageDocument = this.GetOriginalRetainageDocument(arRegister1);
          ARAdjust arAdjust = lastAdjustment.Item1;
          if (retainageDocument != null && arAdjust.AdjdDocType == "INV")
          {
            nullable2 = retainageDocument.SignAmount;
            Decimal? adjdTbSign = arAdjust.AdjdTBSign;
            Decimal? nullable3 = nullable2.HasValue & adjdTbSign.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * adjdTbSign.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable4 = arAdjust.CuryAdjdAmt;
            nullable2 = nullable3;
            Decimal? nullable5 = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
            Decimal valueOrDefault1 = nullable5.GetValueOrDefault();
            nullable4 = arAdjust.AdjAmt;
            nullable2 = nullable3;
            Decimal? nullable6;
            if (!(nullable4.HasValue & nullable2.HasValue))
            {
              nullable5 = new Decimal?();
              nullable6 = nullable5;
            }
            else
              nullable6 = new Decimal?(nullable4.GetValueOrDefault() * nullable2.GetValueOrDefault());
            nullable5 = nullable6;
            Decimal valueOrDefault2 = nullable5.GetValueOrDefault();
            ARRegister arRegister3 = retainageDocument;
            nullable4 = arRegister3.CuryRetainageUnpaidTotal;
            Decimal num1 = valueOrDefault1;
            Decimal? nullable7;
            if (!nullable4.HasValue)
            {
              nullable2 = new Decimal?();
              nullable7 = nullable2;
            }
            else
              nullable7 = new Decimal?(nullable4.GetValueOrDefault() - num1);
            arRegister3.CuryRetainageUnpaidTotal = nullable7;
            ARRegister arRegister4 = retainageDocument;
            nullable4 = arRegister4.RetainageUnpaidTotal;
            Decimal num2 = valueOrDefault2;
            Decimal? nullable8;
            if (!nullable4.HasValue)
            {
              nullable2 = new Decimal?();
              nullable8 = nullable2;
            }
            else
              nullable8 = new Decimal?(nullable4.GetValueOrDefault() - num2);
            arRegister4.RetainageUnpaidTotal = nullable8;
            ((PXSelectBase) this.ARDocument).Cache.Update((object) retainageDocument);
            ARTranPost tranPost = this.CreateTranPost(arRegister1);
            tranPost.SourceDocType = payment.DocType;
            tranPost.SourceRefNbr = payment.RefNbr;
            tranPost.DocType = retainageDocument.DocType;
            tranPost.RefNbr = retainageDocument.RefNbr;
            tranPost.Type = "T";
            tranPost.CuryAmt = new Decimal?(valueOrDefault1);
            tranPost.Amt = new Decimal?(valueOrDefault2);
            if (this.IsNeedUpdateHistoryForTransaction(tranPost.TranPeriodID))
              ((PXSelectBase<ARTranPost>) this.TranPost).Insert(tranPost);
          }
        }
        this.VerifyPaymentRoundAndClose(je, arRegister1, payment, paymentCustomer, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(currencyInfoCopyForGl), paycury, lastAdjustment);
        ARRegister arRegister5 = arRegister1;
        int? adjCntr = arRegister5.AdjCntr;
        arRegister5.AdjCntr = adjCntr.HasValue ? new int?(adjCntr.GetValueOrDefault() + 1) : new int?();
        ((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Current = payment;
      }
      if (arRegister1.DocType == "RPM")
      {
        ARPayment arPayment1 = PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelect<ARPayment, Where<ARPayment.docType, Equal<ARDocType.voidPayment>, And<ARPayment.refNbr, Equal<Required<ARRegister.refNbr>>>>>.Config>.Select((PXGraph) je, new object[1]
        {
          (object) arRegister1.RefNbr
        }));
        ARPayment arPayment2 = PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelect<ARPayment, Where<ARPayment.docType, Equal<ARDocType.payment>, And<ARPayment.refNbr, Equal<Required<ARRegister.refNbr>>>>>.Config>.Select((PXGraph) je, new object[1]
        {
          (object) arRegister1.RefNbr
        }));
        if (arPayment2 != null && arPayment2.Deposited.GetValueOrDefault())
        {
          CADeposit caDeposit = PXResultset<CADeposit>.op_Implicit(PXSelectBase<CADeposit, PXSelect<CADeposit, Where<CADeposit.refNbr, Equal<Required<ARPayment.depositNbr>>>>.Config>.Select((PXGraph) je, new object[1]
          {
            (object) arPayment2.DepositNbr
          }));
          if (caDeposit != null)
          {
            CADepositDetail caDepositDetail = PXResultset<CADepositDetail>.op_Implicit(PXSelectBase<CADepositDetail, PXSelect<CADepositDetail, Where<CADepositDetail.refNbr, Equal<Required<CADeposit.refNbr>>, And<CADepositDetail.origRefNbr, Equal<Required<ARPayment.refNbr>>, And<CADepositDetail.origDocType, Equal<ARDocType.payment>>>>>.Config>.Select((PXGraph) je, new object[2]
            {
              (object) caDeposit.RefNbr,
              (object) arPayment2.RefNbr
            }));
            if (caDepositDetail != null)
            {
              Decimal num3 = Math.Round(caDepositDetail.OrigAmtSigned.Value - caDepositDetail.TranAmt.Value, 3);
              if (num3 != 0M)
              {
                int? cashAccountId1 = caDeposit.CashAccountID;
                int? cashAccountId2 = arPayment1.CashAccountID;
                if (cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue)
                {
                  PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) je, new object[1]
                  {
                    (object) caDeposit.CashAccountID
                  }));
                  PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
                  glTran1.DebitAmt = new Decimal?(0M);
                  glTran1.CreditAmt = new Decimal?(0M);
                  glTran1.AccountID = cashAccount.AccountID;
                  glTran1.SubID = cashAccount.SubID;
                  glTran1.BranchID = cashAccount.BranchID;
                  glTran1.TranDate = arRegister1.DocDate;
                  FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran1, arRegister1.TranPeriodID);
                  glTran1.TranType = "CTG";
                  glTran1.RefNbr = arRegister1.RefNbr;
                  glTran1.TranDesc = "Reverse Deposit RGOL";
                  glTran1.Released = new bool?(true);
                  glTran1.CuryInfoID = arRegister1.CuryInfoID;
                  PX.Objects.GL.GLTran glTran2 = glTran1;
                  Decimal? nullable = glTran2.DebitAmt;
                  Decimal num4 = arPayment2.DrCr == "C" == num3 > 0M ? 0M : Math.Abs(num3);
                  glTran2.DebitAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num4) : new Decimal?();
                  PX.Objects.GL.GLTran glTran3 = glTran1;
                  nullable = glTran3.CreditAmt;
                  Decimal num5 = arPayment2.DrCr == "C" == num3 > 0M ? Math.Abs(num3) : 0M;
                  glTran3.CreditAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num5) : new Decimal?();
                  PX.Objects.CM.Extensions.Currency currency = PXResultset<PX.Objects.CM.Extensions.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.Currency, PXSelect<PX.Objects.CM.Extensions.Currency, Where<PX.Objects.CM.Extensions.Currency.curyID, Equal<Required<PX.Objects.CM.Extensions.Currency.curyID>>>>.Config>.Select((PXGraph) je, new object[1]
                  {
                    (object) caDeposit.CuryID
                  }));
                  nullable = glTran1.DebitAmt;
                  Decimal? creditAmt = glTran1.CreditAmt;
                  Decimal num6 = (nullable.HasValue & creditAmt.HasValue ? new Decimal?(nullable.GetValueOrDefault() - creditAmt.GetValueOrDefault()) : new Decimal?()).Value;
                  int num7 = Math.Sign(num6);
                  Decimal num8 = Math.Abs(num6);
                  if (num8 != 0M)
                  {
                    PX.Objects.GL.GLTran copy = (PX.Objects.GL.GLTran) ((PXGraph) je).Caches[typeof (PX.Objects.GL.GLTran)].CreateCopy((object) glTran1);
                    copy.CuryDebitAmt = new Decimal?(0M);
                    copy.CuryCreditAmt = new Decimal?(0M);
                    if (arRegister1.DocType == "CDT")
                    {
                      copy.AccountID = num7 < 0 ? currency.RealLossAcctID : currency.RealGainAcctID;
                      copy.SubID = num7 < 0 ? GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.realLossSubID>((PXGraph) je, glTran1.BranchID, currency) : GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.realGainSubID>((PXGraph) je, glTran1.BranchID, currency);
                    }
                    else
                    {
                      copy.AccountID = num7 < 0 ? currency.RealGainAcctID : currency.RealLossAcctID;
                      copy.SubID = num7 < 0 ? GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.realGainSubID>((PXGraph) je, glTran1.BranchID, currency) : GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.realLossSubID>((PXGraph) je, glTran1.BranchID, currency);
                    }
                    copy.DebitAmt = new Decimal?(num7 < 0 ? num8 : 0M);
                    copy.CreditAmt = new Decimal?(num7 < 0 ? 0M : num8);
                    copy.TranType = "CTG";
                    copy.RefNbr = arRegister1.RefNbr;
                    copy.TranDesc = "Reverse Deposit RGOL";
                    copy.TranDate = glTran1.TranDate;
                    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) copy, glTran1.TranPeriodID);
                    copy.Released = new bool?(true);
                    copy.CuryInfoID = arPayment2.CuryInfoID;
                    this.InsertPaymentCADepositTransaction(je, copy, new ARReleaseProcess.GLTranInsertionContext()
                    {
                      ARRegisterRecord = arRegister1,
                      CADepositRecord = caDeposit,
                      CADepositDetailRecord = caDepositDetail
                    });
                    glTran1.CuryDebitAmt = new Decimal?(0M);
                    glTran1.DebitAmt = new Decimal?(num7 > 0 ? num8 : 0M);
                    glTran1.CreditAmt = new Decimal?(num7 > 0 ? 0M : num8);
                    this.InsertPaymentCADepositTransaction(je, glTran1, new ARReleaseProcess.GLTranInsertionContext()
                    {
                      ARRegisterRecord = arRegister1,
                      CADepositRecord = caDeposit,
                      CADepositDetailRecord = caDepositDetail
                    });
                  }
                }
              }
            }
          }
        }
      }
      arRegister1.Selected = new bool?(true);
      arRegister1.Released = new bool?(true);
      bool? nullable9;
      if (ardoc.IsPrepaymentInvoiceDocument())
      {
        nullable9 = ardoc.PendingPayment;
        bool flag8 = false;
        if (nullable9.GetValueOrDefault() == flag8 & nullable9.HasValue)
        {
          nullable9 = ardoc.Released;
          bool flag9 = false;
          if (nullable9.GetValueOrDefault() == flag9 & nullable9.HasValue)
            arRegister1.PostponePendingPaymentFlag = new bool?(true);
        }
      }
      this.UpdateARBalances(arRegister1);
      if (!this._IsIntegrityCheck)
      {
        this.SaveBatchForDocument(je, arRegister1);
        this.ReplaceCADailySummaryCache(je);
      }
      nullable9 = arRegister1.Released;
      if (nullable9.GetValueOrDefault())
        this.RaiseReleaseEvent(arRegister1);
      PXCache<ARRegister>.RestoreCopy(ardoc, arRegister1);
      if (arRegister1.DocType == "CRM")
      {
        if (!flag2)
        {
          ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetStatus((object) ((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Current, (PXEntryStatus) 0);
        }
        else
        {
          PXSelectorAttribute.StoreResult<ARRegister.curyID>(((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache, (object) arRegister1, (IBqlTable) CurrencyCollection.GetCurrency(arRegister1.CuryID));
          ARPayment data = (ARPayment) ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.Extend<ARRegister>(arRegister1);
          data.AdjTranPeriodID = (string) null;
          data.AdjFinPeriodID = (string) null;
          data.CuryInfoID = arRegister1.CuryInfoID;
          ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.Update((object) data);
          data.CreatedByID = arRegister1.CreatedByID;
          data.CreatedByScreenID = arRegister1.CreatedByScreenID;
          data.CreatedDateTime = arRegister1.CreatedDateTime;
          data.CashAccountID = new int?();
          data.AdjDate = data.DocDate;
          data.AdjTranPeriodID = data.TranPeriodID;
          data.AdjFinPeriodID = data.FinPeriodID;
          OpenPeriodAttribute.SetValidatePeriod<ARPayment.adjFinPeriodID>(((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache, (object) data, PeriodValidation.DefaultSelectUpdate);
          ((PXSelectBase) this.ARDocument).Cache.SetStatus((object) arRegister1, (PXEntryStatus) 0);
          arRegister1 = (ARRegister) ((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Update(data);
        }
        this.CreditMemoProcessingBeforeSave(ardoc);
      }
      else if (((PXSelectBase) this.ARDocument).Cache.ObjectsEqual((object) arRegister1, (object) ((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Current))
        ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetStatus((object) ((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Current, (PXEntryStatus) 0);
      this.UpdateCCSpecificFields(arRegister1);
      this.ProcessPostponedFlags();
      List<ProcessInfo<PX.Objects.GL.Batch>> infoList;
      RegisterRelease.ReleaseWithoutPost(pmRegisterList, false, out infoList);
      foreach (ProcessInfo<PX.Objects.GL.Batch> processInfo in infoList)
        pmBatchList.AddRange((IEnumerable<PX.Objects.GL.Batch>) processInfo.Batches);
      flag1 = ((PXSelectBase) this.ARDocument).Cache.GetStatus((object) arRegister1) == 1;
      ((PXGraph) this).Actions.PressSave();
      if (flag1)
        arRegister1 = (ARRegister) ((PXSelectBase) this.ARDocument).Cache.Locate((object) arRegister1);
      if (!this._IsIntegrityCheck)
        EntityInUseHelper.MarkEntityAsInUse<CurrencyInUse>((object) arRegister1.CuryID);
      if (onreleasecomplete != null)
        onreleasecomplete(ardoc);
      transactionScope.Complete((PXGraph) this);
    }
    bool flag10 = false;
    ARInvoice doc = arRegister1 as ARInvoice;
    if (flag1 && doc != null)
    {
      bool? isTaxPosted = doc.IsTaxPosted;
      bool flag11 = false;
      if (isTaxPosted.GetValueOrDefault() == flag11 & isTaxPosted.HasValue)
      {
        ARInvoice arInvoice = this.CommitExternalTax(doc);
        if (arInvoice != null)
        {
          isTaxPosted = arInvoice.IsTaxPosted;
          if (isTaxPosted.GetValueOrDefault())
          {
            arRegister1.IsTaxPosted = arInvoice.IsTaxPosted;
            PXDatabase.Update<ARRegister>(new PXDataFieldParam[3]
            {
              (PXDataFieldParam) new PXDataFieldAssign("IsTaxPosted", (object) true),
              (PXDataFieldParam) new PXDataFieldRestrict("DocType", (PXDbType) 3, new int?(3), (object) arRegister1.DocType, (PXComp) 0),
              (PXDataFieldParam) new PXDataFieldRestrict("RefNbr", (PXDbType) 12, new int?(15), (object) arRegister1.RefNbr, (PXComp) 0)
            });
            flag10 = true;
          }
        }
      }
    }
    if (flag10)
      ((PXSelectBase) this.ARDocument).Cache.Persisted(false);
    this._oldInvoiceRefresher.CommitRefresh((PXGraph) this);
    PXCache<ARRegister>.RestoreCopy(ardoc, arRegister1);
    if (flag2)
    {
      ARRegister arRegister6 = ((PXSelectBase<ARRegister>) this.ARDocument).Locate(arRegister1);
      if (arRegister6 != null)
        PXCache<ARRegister>.RestoreCopy(arRegister6, arRegister1);
      // ISSUE: explicit non-virtual call
      if (arRegisterList != null && __nonvirtual (arRegisterList.Count) > 0 && arRegisterList[0].DocType == arRegister1.DocType && arRegisterList[0].RefNbr == arRegister1.RefNbr)
        PXCache<ARRegister>.RestoreCopy(arRegisterList[0], arRegister1);
    }
    return arRegisterList;
  }

  protected virtual void CreditMemoProcessingBeforeSave(ARRegister ardoc)
  {
  }

  protected void CheckVoidedDoucmentAmountDiscrepancies(ARRegister document)
  {
    if (!(document.DocType == "RPM") && !(document.DocType == "VRF"))
      return;
    ARRegister arRegister = ARRegister.PK.Find((PXGraph) this, document.OrigDocType, document.OrigRefNbr);
    Decimal? origDocAmt1 = document.OrigDocAmt;
    Decimal? origDocAmt2 = arRegister.OrigDocAmt;
    Decimal? nullable = origDocAmt2.HasValue ? new Decimal?(-origDocAmt2.GetValueOrDefault()) : new Decimal?();
    if (!(origDocAmt1.GetValueOrDefault() == nullable.GetValueOrDefault() & origDocAmt1.HasValue == nullable.HasValue))
      throw new ReleaseException("The {0} {1} cannot be released because its amount differs from the amount of the document being voided.", new object[2]
      {
        (object) document.RefNbr,
        (object) ARDocType.GetDisplayName(document.DocType)
      });
  }

  protected virtual void ProcessPostponedFlags()
  {
    foreach (ARPayment row in ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.Cached.Cast<ARPayment>().Where<ARPayment>((Func<ARPayment, bool>) (p => p.PostponeReleasedFlag.GetValueOrDefault() || p.PostponeVoidedFlag.GetValueOrDefault() || p.PostponePendingPaymentFlag.GetValueOrDefault())))
    {
      PXCache cache1 = ((PXSelectBase) this.soAdjust).Cache;
      PXCache cache2 = ((PXSelectBase) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Cache;
      bool? nullable = row.PostponeReleasedFlag;
      if (nullable.GetValueOrDefault())
      {
        ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.RaiseEventsOnFieldChanging<ARPayment.released>((object) row, (object) true);
        row.PostponeReleasedFlag = new bool?(false);
      }
      nullable = row.PostponeVoidedFlag;
      if (nullable.GetValueOrDefault())
      {
        row.Voided = new bool?(false);
        ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.RaiseEventsOnFieldChanging<ARPayment.voided>((object) row, (object) true);
        row.PostponeVoidedFlag = new bool?(false);
      }
      nullable = row.PostponePendingPaymentFlag;
      if (nullable.GetValueOrDefault())
      {
        ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.RaiseEventsOnFieldChanging<ARPayment.pendingPayment>((object) row, (object) true);
        row.PostponePendingPaymentFlag = new bool?(false);
      }
    }
  }

  public virtual ARInvoice CommitExternalTax(ARInvoice doc) => doc;

  /// <summary>
  /// Workaround for AC-167924. To prevent selection of outdated currencyinfo record from DB
  /// 1. When we generate ar doc through the voucher from, we create new currencyinfo in the voucher graph.
  /// 2. We are persisting changes but they are not committed in the db
  /// 3. When we are in the ar release graph, we select the currencyinfo from db and get outdated commited one.
  /// 4. This workaround is that to put the currencyinfo to the cache to avoid quieting the db
  /// </summary>
  /// <param name="doc"></param>
  /// <param name="info"></param>
  protected virtual void InsertCurrencyInfoIntoCache(ARRegister doc, PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    if (!(doc.OrigModule == "GL"))
      return;
    ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CurrencyInfo_CuryInfoID).Insert(info);
  }

  public virtual void SaveBatchForDocument(JournalEntry je, ARRegister doc)
  {
    if (((PXSelectBase) je.GLTranModuleBatNbr).Cache.IsInsertedUpdatedDeleted)
      ((PXAction) je.Save).Press();
    if (((PXSelectBase) je.BatchModule).Cache.IsDirty || !string.IsNullOrEmpty(doc.BatchNbr))
      return;
    string batchNbr = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current.BatchNbr;
    doc.BatchNbr = !doc.IsMigratedRecord.GetValueOrDefault() ? batchNbr : (string) null;
    foreach (ARTranPost arTranPost in ((PXSelectBase) this.TranPost).Cache.Inserted.Cast<ARTranPost>().Where<ARTranPost>((Func<ARTranPost, bool>) (d => d.TranType == doc.DocType && d.TranRefNbr == doc.RefNbr && d.BatchNbr == null)))
      arTranPost.BatchNbr = batchNbr;
  }

  public virtual void SaveBatchForAdjustment(
    JournalEntry je,
    ARAdjust adj,
    ARRegister adjustedDocument)
  {
    this.SaveBatchForAdjustment(je, adj, adjustedDocument, true);
  }

  public virtual void SaveBatchForAdjustment(
    JournalEntry je,
    ARAdjust adj,
    ARRegister adjustedDocument,
    bool saveBatch)
  {
    if (((PXSelectBase) je.GLTranModuleBatNbr).Cache.IsInsertedUpdatedDeleted)
      ((PXAction) je.Save).Press();
    if (!(!((PXSelectBase) je.BatchModule).Cache.IsDirty & saveBatch))
      return;
    adj.AdjBatchNbr = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current.BatchNbr;
    if (adj.AdjdDocType == "SMC" && adj.Voided.GetValueOrDefault())
    {
      foreach (ARTranPost arTranPost in ((PXSelectBase) this.TranPost).Cache.Inserted.Cast<ARTranPost>().Where<ARTranPost>((Func<ARTranPost, bool>) (d => d.DocType == adj.AdjdDocType && d.RefNbr == adj.AdjdRefNbr && d.BatchNbr == null)))
        arTranPost.BatchNbr = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current.BatchNbr;
    }
    if (!adj.IsOrigSmallCreditWOApp())
      return;
    adjustedDocument = (ARRegister) ((PXSelectBase) this.ARDocument).Cache.Locate((object) adjustedDocument) ?? adjustedDocument;
    ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current.Description = adjustedDocument.DocDesc;
    ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Update(((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current);
    adjustedDocument.BatchNbr = adj.AdjBatchNbr;
    ((PXSelectBase<ARRegister>) this.ARDocument).Update(adjustedDocument);
  }

  protected virtual void PerformPersist(PXGraph.IPersistPerformer persister)
  {
    persister.Insert(((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache);
    persister.Update(((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache);
    persister.Update(((PXSelectBase) this.ARDocument).Cache);
    persister.Insert(((PXSelectBase) this.ARTran_TranType_RefNbr).Cache);
    persister.Update(((PXSelectBase) this.ARTran_TranType_RefNbr).Cache);
    persister.Update(((PXSelectBase) this.ARTaxTran_TranType_RefNbr).Cache);
    persister.Insert(((PXSelectBase) this.SVATConversionHistory).Cache);
    persister.Update(((PXSelectBase) this.SVATConversionHistory).Cache);
    persister.Update<INTran>();
    persister.Update(((PXSelectBase) this.ARPaymentChargeTran_DocType_RefNbr).Cache);
    persister.Insert(((PXSelectBase) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Cache);
    persister.Update(((PXSelectBase) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Cache);
    persister.Delete(((PXSelectBase) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Cache);
    ((PXSelectBase) this.StandaloneARInvoice).Cache.Persist((PXDBOperation) 1);
    persister.Insert(((PXSelectBase) this.ARDoc_SalesPerTrans).Cache);
    persister.Update(((PXSelectBase) this.ARDoc_SalesPerTrans).Cache);
    ((PXSelectBase) this.StandaloneARInvoice).Cache.Persisted(false);
    persister.Insert<ARHist>();
    persister.Insert<CuryARHist>();
    persister.Insert<ARBalances>();
    persister.Insert<ARTranPost>();
    persister.Insert<PMCommitment>();
    persister.Update<PMCommitment>();
    persister.Delete<PMCommitment>();
    persister.Insert<PMHistoryAccum>();
    persister.Insert<PMBudgetAccum>();
    persister.Insert<PMForecastHistoryAccum>();
    persister.Update<ARTax>();
    persister.Insert<ItemCustSalesStats>();
    persister.Update(((PXSelectBase) this.soOrder).Cache);
    persister.Update(((PXSelectBase) this.soAdjust).Cache);
    persister.Insert<CADailySummary>();
  }

  /// <summary>Returns True if this is a Validate Balances context.</summary>
  public bool IsIntegrityCheck => this._IsIntegrityCheck;

  protected Customer CustomerIntegrityCheck
  {
    get
    {
      return !this.IsIntegrityCheck ? (Customer) null : ((PXGraph) this).Caches[typeof (Customer)].Current as Customer;
    }
  }

  public virtual void IntegrityCheckProc(Customer cust, string startPeriod)
  {
    this._IsIntegrityCheck = true;
    this._IntegrityCheckStartingPeriod = startPeriod;
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    ((PXGraph) instance).SetOffline();
    ((PXGraph) this).Caches[typeof (Customer)].Current = (object) cust;
    using (new PXConnectionScope())
    {
      this._oldInvoiceRefresher = new OldInvoiceDateRefresher();
      bool flag1 = false;
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        string str = "190001";
        ARHistoryDetDeleted historyDetDeleted = PXResultset<ARHistoryDetDeleted>.op_Implicit(PXSelectBase<ARHistoryDetDeleted, PXSelectGroupBy<ARHistoryDetDeleted, Where<ARHistoryDetDeleted.customerID, Equal<Current<Customer.bAccountID>>>, Aggregate<Max<ARHistoryDetDeleted.finPeriodID>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
        if (historyDetDeleted != null && historyDetDeleted.FinPeriodID != null)
          str = this.FinPeriodRepository.GetOffsetPeriodId(historyDetDeleted.FinPeriodID, 1, new int?(0));
        if (!string.IsNullOrEmpty(startPeriod) && string.Compare(startPeriod, str) > 0)
          str = startPeriod;
        FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(new int?(0), str);
        PXUpdateJoin<Set<ARHistory.finBegBalance, IsNull<ARHistory2.finYtdBalance, Zero>, Set<ARHistory.finPtdSales, Zero, Set<ARHistory.finPtdPayments, Zero, Set<ARHistory.finPtdCrAdjustments, Zero, Set<ARHistory.finPtdDrAdjustments, Zero, Set<ARHistory.finPtdDiscounts, Zero, Set<ARHistory.finPtdCOGS, Zero, Set<ARHistory.finPtdRGOL, Zero, Set<ARHistory.finPtdFinCharges, Zero, Set<ARHistory.finYtdBalance, IsNull<ARHistory2.finYtdBalance, Zero>, Set<ARHistory.finPtdDeposits, Zero, Set<ARHistory.finYtdDeposits, IsNull<ARHistory2.finYtdDeposits, Zero>, Set<ARHistory.finPtdItemDiscounts, Zero, Set<ARHistory.finYtdRetainageReleased, IsNull<ARHistory2.finYtdRetainageReleased, Zero>, Set<ARHistory.finPtdRetainageReleased, Zero, Set<ARHistory.finYtdRetainageWithheld, IsNull<ARHistory2.finYtdRetainageWithheld, Zero>, Set<ARHistory.finPtdRetainageWithheld, Zero, Set<ARHistory.finPtdRevalued, ARHistory.finPtdRevalued, Set<ARHistory.numberInvoicePaid, Zero, Set<ARHistory.paidInvoiceDays, Zero>>>>>>>>>>>>>>>>>>>>, ARHistory, LeftJoin<PX.Objects.GL.Branch, On<ARHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FinPeriod, On<ARHistory.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>, LeftJoin<OrganizationFinPeriodExt, On<OrganizationFinPeriodExt.masterFinPeriodID, Equal<Required<OrganizationFinPeriodExt.masterFinPeriodID>>, And<PX.Objects.GL.Branch.organizationID, Equal<OrganizationFinPeriodExt.organizationID>>>, LeftJoin<ARHistory2ByPeriod, On<ARHistory2ByPeriod.branchID, Equal<ARHistory.branchID>, And<ARHistory2ByPeriod.accountID, Equal<ARHistory.accountID>, And<ARHistory2ByPeriod.subID, Equal<ARHistory.subID>, And<ARHistory2ByPeriod.customerID, Equal<ARHistory.customerID>, And<ARHistory2ByPeriod.finPeriodID, Equal<OrganizationFinPeriodExt.prevFinPeriodID>>>>>>, LeftJoin<ARHistory2, On<ARHistory2.branchID, Equal<ARHistory.branchID>, And<ARHistory2.accountID, Equal<ARHistory.accountID>, And<ARHistory2.subID, Equal<ARHistory.subID>, And<ARHistory2.customerID, Equal<ARHistory.customerID>, And<ARHistory2.finPeriodID, Equal<ARHistory2ByPeriod.lastActivityPeriod>>>>>>>>>>>, Where<ARHistory.customerID, Equal<Required<ARHist.customerID>>, And<FinPeriod.masterFinPeriodID, GreaterEqual<Required<FinPeriod.masterFinPeriodID>>>>>.Update((PXGraph) this, new object[3]
        {
          (object) str,
          (object) cust.BAccountID,
          (object) str
        });
        PXUpdateJoin<Set<ARHistory.tranBegBalance, IsNull<ARHistory2.tranYtdBalance, Zero>, Set<ARHistory.tranPtdSales, Zero, Set<ARHistory.tranPtdPayments, Zero, Set<ARHistory.tranPtdCrAdjustments, Zero, Set<ARHistory.tranPtdDrAdjustments, Zero, Set<ARHistory.tranPtdDiscounts, Zero, Set<ARHistory.tranPtdRGOL, Zero, Set<ARHistory.tranPtdCOGS, Zero, Set<ARHistory.tranPtdFinCharges, Zero, Set<ARHistory.tranYtdBalance, IsNull<ARHistory2.tranYtdBalance, Zero>, Set<ARHistory.tranPtdDeposits, Zero, Set<ARHistory.tranYtdDeposits, IsNull<ARHistory2.tranYtdDeposits, Zero>, Set<ARHistory.tranPtdItemDiscounts, Zero, Set<ARHistory.tranYtdRetainageReleased, IsNull<ARHistory2.tranYtdRetainageReleased, Zero>, Set<ARHistory.tranPtdRetainageReleased, Zero, Set<ARHistory.tranYtdRetainageWithheld, IsNull<ARHistory2.tranYtdRetainageWithheld, Zero>, Set<ARHistory.tranPtdRetainageWithheld, Zero>>>>>>>>>>>>>>>>>, ARHistory, LeftJoin<ARHistory2ByPeriod, On<ARHistory2ByPeriod.branchID, Equal<ARHistory.branchID>, And<ARHistory2ByPeriod.accountID, Equal<ARHistory.accountID>, And<ARHistory2ByPeriod.subID, Equal<ARHistory.subID>, And<ARHistory2ByPeriod.customerID, Equal<ARHistory.customerID>, And<ARHistory2ByPeriod.finPeriodID, Equal<Required<FinPeriod.masterFinPeriodID>>>>>>>, LeftJoin<ARHistory2, On<ARHistory2.branchID, Equal<ARHistory.branchID>, And<ARHistory2.accountID, Equal<ARHistory.accountID>, And<ARHistory2.subID, Equal<ARHistory.subID>, And<ARHistory2.customerID, Equal<ARHistory.customerID>, And<ARHistory2.finPeriodID, Equal<ARHistory2ByPeriod.lastActivityPeriod>>>>>>>>, Where<ARHistory.customerID, Equal<Required<ARHist.customerID>>, And<ARHistory.finPeriodID, GreaterEqual<Required<ARHistory.finPeriodID>>>>>.Update((PXGraph) this, new object[3]
        {
          (object) prevPeriod?.FinPeriodID,
          (object) cust.BAccountID,
          (object) str
        });
        PXUpdateJoin<Set<CuryARHistory.finBegBalance, IsNull<CuryARHistory2.finYtdBalance, Zero>, Set<CuryARHistory.finPtdSales, Zero, Set<CuryARHistory.finPtdPayments, Zero, Set<CuryARHistory.finPtdDrAdjustments, Zero, Set<CuryARHistory.finPtdCrAdjustments, Zero, Set<CuryARHistory.finPtdDiscounts, Zero, Set<CuryARHistory.finPtdRGOL, Zero, Set<CuryARHistory.finPtdCOGS, Zero, Set<CuryARHistory.finPtdFinCharges, Zero, Set<CuryARHistory.finYtdBalance, IsNull<CuryARHistory2.finYtdBalance, Zero>, Set<CuryARHistory.finPtdDeposits, Zero, Set<CuryARHistory.finYtdDeposits, IsNull<CuryARHistory2.finYtdDeposits, Zero>, Set<CuryARHistory.curyFinBegBalance, IsNull<CuryARHistory2.curyFinYtdBalance, Zero>, Set<CuryARHistory.curyFinPtdSales, Zero, Set<CuryARHistory.curyFinPtdPayments, Zero, Set<CuryARHistory.curyFinPtdDrAdjustments, Zero, Set<CuryARHistory.curyFinPtdCrAdjustments, Zero, Set<CuryARHistory.curyFinPtdDiscounts, Zero, Set<CuryARHistory.curyFinPtdFinCharges, Zero, Set<CuryARHistory.curyFinYtdBalance, IsNull<CuryARHistory2.curyFinYtdBalance, Zero>, Set<CuryARHistory.curyFinPtdDeposits, Zero, Set<CuryARHistory.curyFinYtdDeposits, IsNull<CuryARHistory2.curyFinYtdDeposits, Zero>, Set<CuryARHistory.curyFinPtdRetainageWithheld, Zero, Set<CuryARHistory.finPtdRetainageWithheld, Zero, Set<CuryARHistory.curyFinYtdRetainageWithheld, IsNull<CuryARHistory2.curyFinYtdRetainageWithheld, Zero>, Set<CuryARHistory.finYtdRetainageWithheld, IsNull<CuryARHistory2.finYtdRetainageWithheld, Zero>, Set<CuryARHistory.curyFinPtdRetainageReleased, Zero, Set<CuryARHistory.finPtdRetainageReleased, Zero, Set<CuryARHistory.curyFinYtdRetainageReleased, IsNull<CuryARHistory2.curyFinYtdRetainageReleased, Zero>, Set<CuryARHistory.finYtdRetainageReleased, IsNull<CuryARHistory2.finYtdRetainageReleased, Zero>, Set<CuryARHistory.finPtdRevalued, CuryARHistory.finPtdRevalued>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>, CuryARHistory, LeftJoin<PX.Objects.GL.Branch, On<CuryARHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FinPeriod, On<CuryARHistory.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>, LeftJoin<OrganizationFinPeriodExt, On<OrganizationFinPeriodExt.masterFinPeriodID, Equal<Required<OrganizationFinPeriodExt.masterFinPeriodID>>, And<PX.Objects.GL.Branch.organizationID, Equal<OrganizationFinPeriodExt.organizationID>>>, LeftJoin<ARHistoryByPeriod, On<ARHistoryByPeriod.branchID, Equal<CuryARHistory.branchID>, And<ARHistoryByPeriod.accountID, Equal<CuryARHistory.accountID>, And<ARHistoryByPeriod.subID, Equal<CuryARHistory.subID>, And<ARHistoryByPeriod.customerID, Equal<CuryARHistory.customerID>, And<ARHistoryByPeriod.curyID, Equal<CuryARHistory.curyID>, And<ARHistoryByPeriod.finPeriodID, Equal<OrganizationFinPeriodExt.prevFinPeriodID>>>>>>>, LeftJoin<CuryARHistory2, On<CuryARHistory2.branchID, Equal<CuryARHistory.branchID>, And<CuryARHistory2.accountID, Equal<CuryARHistory.accountID>, And<CuryARHistory2.subID, Equal<CuryARHistory.subID>, And<CuryARHistory2.customerID, Equal<CuryARHistory.customerID>, And<CuryARHistory2.curyID, Equal<CuryARHistory.curyID>, And<CuryARHistory2.finPeriodID, Equal<ARHistoryByPeriod.lastActivityPeriod>>>>>>>>>>>>, Where<CuryARHistory.customerID, Equal<Required<CuryARHist.customerID>>, And<FinPeriod.masterFinPeriodID, GreaterEqual<Required<FinPeriod.finPeriodID>>>>>.Update((PXGraph) this, new object[3]
        {
          (object) str,
          (object) cust.BAccountID,
          (object) str
        });
        PXUpdateJoin<Set<CuryARHistory.tranBegBalance, IsNull<CuryARHistory2.tranYtdBalance, Zero>, Set<CuryARHistory.tranPtdSales, Zero, Set<CuryARHistory.tranPtdPayments, Zero, Set<CuryARHistory.tranPtdDrAdjustments, Zero, Set<CuryARHistory.tranPtdCrAdjustments, Zero, Set<CuryARHistory.tranPtdDiscounts, Zero, Set<CuryARHistory.tranPtdRGOL, Zero, Set<CuryARHistory.tranPtdCOGS, Zero, Set<CuryARHistory.tranPtdFinCharges, Zero, Set<CuryARHistory.tranYtdBalance, IsNull<CuryARHistory2.tranYtdBalance, Zero>, Set<CuryARHistory.tranPtdDeposits, Zero, Set<CuryARHistory.tranYtdDeposits, IsNull<CuryARHistory2.tranYtdDeposits, Zero>, Set<CuryARHistory.curyTranBegBalance, IsNull<CuryARHistory2.curyTranYtdBalance, Zero>, Set<CuryARHistory.curyTranPtdSales, Zero, Set<CuryARHistory.curyTranPtdPayments, Zero, Set<CuryARHistory.curyTranPtdDrAdjustments, Zero, Set<CuryARHistory.curyTranPtdCrAdjustments, Zero, Set<CuryARHistory.curyTranPtdDiscounts, Zero, Set<CuryARHistory.curyTranPtdFinCharges, Zero, Set<CuryARHistory.curyTranYtdBalance, IsNull<CuryARHistory2.curyTranYtdBalance, Zero>, Set<CuryARHistory.curyTranPtdDeposits, Zero, Set<CuryARHistory.curyTranYtdDeposits, IsNull<CuryARHistory2.curyTranYtdDeposits, Zero>, Set<CuryARHistory.curyTranPtdRetainageWithheld, Zero, Set<CuryARHistory.tranPtdRetainageWithheld, Zero, Set<CuryARHistory.curyTranYtdRetainageWithheld, IsNull<CuryARHistory2.curyTranYtdRetainageWithheld, Zero>, Set<CuryARHistory.tranYtdRetainageWithheld, IsNull<CuryARHistory2.tranYtdRetainageWithheld, Zero>, Set<CuryARHistory.curyTranPtdRetainageReleased, Zero, Set<CuryARHistory.tranPtdRetainageReleased, Zero, Set<CuryARHistory.curyTranYtdRetainageReleased, IsNull<CuryARHistory2.curyTranYtdRetainageReleased, Zero>, Set<CuryARHistory.tranYtdRetainageReleased, IsNull<CuryARHistory2.tranYtdRetainageReleased, Zero>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>, CuryARHistory, LeftJoin<ARHistoryByPeriod, On<ARHistoryByPeriod.branchID, Equal<CuryARHistory.branchID>, And<ARHistoryByPeriod.accountID, Equal<CuryARHistory.accountID>, And<ARHistoryByPeriod.subID, Equal<CuryARHistory.subID>, And<ARHistoryByPeriod.customerID, Equal<CuryARHistory.customerID>, And<ARHistoryByPeriod.curyID, Equal<CuryARHistory.curyID>, And<ARHistoryByPeriod.finPeriodID, Equal<Required<CuryARHistory.finPeriodID>>>>>>>>, LeftJoin<CuryARHistory2, On<CuryARHistory2.branchID, Equal<CuryARHistory.branchID>, And<CuryARHistory2.accountID, Equal<CuryARHistory.accountID>, And<CuryARHistory2.subID, Equal<CuryARHistory.subID>, And<CuryARHistory2.customerID, Equal<CuryARHistory.customerID>, And<CuryARHistory2.curyID, Equal<CuryARHistory.curyID>, And<CuryARHistory2.finPeriodID, Equal<ARHistoryByPeriod.lastActivityPeriod>>>>>>>>>, Where<CuryARHistory.customerID, Equal<Required<CuryARHist.customerID>>, And<CuryARHistory.finPeriodID, GreaterEqual<Required<CuryARHistory.finPeriodID>>>>>.Update((PXGraph) this, new object[3]
        {
          (object) prevPeriod?.FinPeriodID,
          (object) cust.BAccountID,
          (object) str
        });
        PXDatabase.Update<ARBalances>(new PXDataFieldParam[7]
        {
          (PXDataFieldParam) new PXDataFieldAssign<ARBalances.totalOpenOrders>((object) 0M),
          (PXDataFieldParam) new PXDataFieldAssign<ARBalances.unreleasedBal>((object) 0M),
          (PXDataFieldParam) new PXDataFieldAssign<ARBalances.currentBal>((object) 0M),
          (PXDataFieldParam) new PXDataFieldAssign<ARBalances.oldInvoiceDate>((object) null),
          (PXDataFieldParam) new PXDataFieldAssign<ARBalances.lastDocDate>((object) null),
          (PXDataFieldParam) new PXDataFieldAssign<ARBalances.statementRequired>((object) true),
          (PXDataFieldParam) new PXDataFieldRestrict<ARBalances.customerID>((PXDbType) 8, new int?(4), (object) cust.BAccountID, (PXComp) 0)
        });
        PXDatabase.Delete<ARTranPost>(new PXDataFieldRestrict[2]
        {
          (PXDataFieldRestrict) new PXDataFieldRestrict<ARTranPost.customerID>((PXDbType) 8, new int?(4), (object) cust.BAccountID, (PXComp) 0),
          (PXDataFieldRestrict) new PXDataFieldRestrict<ARTranPost.tranPeriodID>((PXDbType) 22, new int?(str.Length), (object) str, (PXComp) 3)
        });
        HashedList<ARRegister> integrityCheckProc = this.GetDocumentsForIntegrityCheckProc(str);
        List<ARRegister> first = new List<ARRegister>();
        List<ARRegister> second = new List<ARRegister>();
        ((PXSelectBase) this.ARDocument).Cache.Clear();
        foreach (ARRegister arRegister in integrityCheckProc)
        {
          if (arRegister.Payable.GetValueOrDefault() || ARDocType.HasBothInvoiceAndPaymentParts(arRegister.DocType))
            first.Add(arRegister);
          if (arRegister.Paying.GetValueOrDefault() || ARDocType.HasBothInvoiceAndPaymentParts(arRegister.DocType))
            second.Add(arRegister);
        }
        first.Sort((Comparison<ARRegister>) ((docA, docB) =>
        {
          Func<ARRegister, short?> func = (Func<ARRegister, short?>) (doc => !doc.RetainageApply.GetValueOrDefault() && !doc.IsRetainageDocument.GetValueOrDefault() ? doc.SortOrder : new short?(!doc.IsRetainageReversing.GetValueOrDefault() ? (short) !doc.RetainageApply.GetValueOrDefault() : (doc.IsRetainageDocument.GetValueOrDefault() ? (short) 2 : (short) 3)));
          return ((IComparable) func(docA)).CompareTo((object) func(docB));
        }));
        foreach (ARRegister doc in first)
        {
          ((PXGraph) instance).Clear();
          ((PXSelectBase) this.ARDocument).Cache.SetStatus((object) doc, (PXEntryStatus) 1);
          doc.Released = new bool?(false);
          foreach (PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account> res in ((PXSelectBase<ARInvoice>) this.ARInvoice_DocType_RefNbr).Select(new object[2]
          {
            (object) doc.DocType,
            (object) doc.RefNbr
          }))
          {
            bool? released = doc.Released;
            bool flag2 = false;
            if (released.GetValueOrDefault() == flag2 & released.HasValue)
              this.SegregateBatch(instance, doc.BranchID, doc.CuryID, doc.DocDate, doc.FinPeriodID, doc.DocDesc, PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer, PX.Objects.GL.Account>.op_Implicit(res), (PX.Objects.GL.Batch) null);
            List<PMRegister> pmDocs = new List<PMRegister>();
            this.ReleaseInvoice(instance, doc, res, pmDocs);
            doc.Released = new bool?(true);
          }
          ((PXSelectBase) this.ARDocument).Cache.Update((object) doc);
        }
        second.Sort((Comparison<ARRegister>) ((docA, docB) => ((IComparable) docA.SortOrder).CompareTo((object) docB.SortOrder)));
        foreach (ARRegister arRegister1 in second)
        {
          ARRegister arRegister2 = arRegister1;
          ((PXGraph) instance).Clear();
          ((PXSelectBase) this.ARDocument).Cache.SetStatus((object) arRegister2, (PXEntryStatus) 1);
          arRegister2.Released = new bool?(ARDocType.HasBothInvoiceAndPaymentParts(arRegister1.DocType));
          bool flag3 = false;
          foreach (PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount> pxResult in ((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Select(new object[3]
          {
            (object) arRegister2.DocType,
            (object) arRegister2.RefNbr,
            (object) arRegister2.CustomerID
          }))
          {
            ARPayment payment = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(pxResult);
            PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(pxResult);
            PX.Objects.CM.Extensions.Currency paycury = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(pxResult);
            Customer paymentCustomer = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(pxResult);
            PX.Objects.CA.CashAccount cashAccount = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>.op_Implicit(pxResult);
            this.SegregateBatch(instance, arRegister2.BranchID, arRegister2.CuryID, payment.AdjDate, payment.AdjFinPeriodID, payment.DocDesc, currencyInfo, (PX.Objects.GL.Batch) null);
            int num1 = arRegister2.AdjCntr.Value;
            arRegister2.AdjCntr = new int?(-1);
            ARReleaseProcess.Amount docBal = new ARReleaseProcess.Amount();
            Tuple<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo> lastAdjustment = (Tuple<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo>) null;
            while (true)
            {
              int? adjCntr = arRegister2.AdjCntr;
              int num2 = num1;
              if (adjCntr.GetValueOrDefault() < num2 & adjCntr.HasValue)
              {
                PX.Objects.CM.CurrencyInfo currencyInfoCopyForGl = this.GetCurrencyInfoCopyForGL(instance, currencyInfo);
                adjCntr = arRegister2.AdjCntr;
                if (adjCntr.GetValueOrDefault() == -1 || arRegister2.DocType != "CSL" && arRegister2.DocType != "RCS")
                  this.ProcessPayment(instance, arRegister2, new PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer, PX.Objects.CA.CashAccount>(payment, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(currencyInfoCopyForGl), paycury, paymentCustomer, cashAccount));
                PXResultset<ARAdjust> adjustments = ((PXSelectBase<ARAdjust>) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Select(new object[4]
                {
                  (object) arRegister2.DocType,
                  (object) arRegister2.RefNbr,
                  (object) this._IsIntegrityCheck,
                  (object) arRegister2.AdjCntr
                });
                Tuple<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo> tuple = this.ProcessAdjustments(instance, adjustments, arRegister2, payment, paymentCustomer, currencyInfoCopyForGl, paycury);
                if (!this._IsIntegrityCheck || lastAdjustment == null || lastAdjustment.Item1.AdjdDocType != "SMC")
                  lastAdjustment = tuple;
                docBal += new ARReleaseProcess.Amount(arRegister2.CuryDocBal, arRegister2.DocBal);
                this.VerifyPaymentRoundAndClose(instance, arRegister2, payment, paymentCustomer, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(currencyInfoCopyForGl), paycury, lastAdjustment);
                ARRegister arRegister3 = arRegister2;
                adjCntr = arRegister3.AdjCntr;
                int? nullable = adjCntr;
                arRegister3.AdjCntr = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?();
                arRegister2.Released = new bool?(true);
              }
              else
                break;
            }
            Decimal? nullable1 = docBal.Base;
            Decimal num3 = 0M;
            if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue) && (arRegister2.DocType == "RPM" || arRegister2.DocType == "VRF"))
              this.ProcessVoidPaymentTranPost(arRegister2, docBal);
            if (arRegister2.Voided.GetValueOrDefault() && arRegister2.DocType == "SMB")
              this.ProcessVoidWOTranPost(arRegister2, lastAdjustment.Item1);
            ARAdjust arAdjust = PXResultset<ARAdjust>.op_Implicit(((PXSelectBase<ARAdjust>) this.ARAdjust_AdjgDocType_RefNbr_CustomerID).Select(new object[4]
            {
              (object) arRegister2.DocType,
              (object) arRegister2.RefNbr,
              (object) this._IsIntegrityCheck,
              (object) num1
            }));
            if (arAdjust != null && !arAdjust.IsInitialApplication.GetValueOrDefault())
              flag3 = true;
          }
          if (flag3 && !arRegister2.OpenDoc.GetValueOrDefault())
          {
            arRegister2.OpenDoc = new bool?(true);
            this.RaisePaymentEvent(arRegister2, (SelectedEntityEvent<ARPayment>) PXEntityEventBase<ARPayment>.Container<ARPayment.Events>.Select((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment.Events>>>) (e => e.OpenDocument)));
          }
          ((PXSelectBase) this.ARDocument).Cache.Update((object) arRegister2);
        }
        foreach (ARRegister arRegister4 in first.Union<ARRegister>((IEnumerable<ARRegister>) second))
        {
          if (arRegister4.PaymentsByLinesAllowed.GetValueOrDefault())
            flag1 = true;
          ((PXGraph) instance).Clear();
          if (!(((PXSelectBase) this.ARDocument).Cache.Locate((object) arRegister4) is ARRegister arRegister5))
            arRegister5 = arRegister4;
          ARRegister arRegister6 = arRegister5;
          ((PXSelectBase) this.ARDocument).Cache.SetStatus((object) arRegister6, (PXEntryStatus) 1);
          PXResultset<ARAdjust> adjustments = PXSelectBase<ARAdjust, PXSelectJoin<ARAdjust, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARAdjust.adjdCuryInfoID>>, InnerJoin<PX.Objects.CM.Extensions.Currency, On<PX.Objects.CM.Extensions.Currency.curyID, Equal<PX.Objects.CM.Extensions.CurrencyInfo.curyID>>, LeftJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARAdjust.adjdDocType>, And<ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>, LeftJoin<ARPayment, On<ARPayment.docType, Equal<ARAdjust.adjgDocType>, And<ARPayment.refNbr, Equal<ARAdjust.adjgRefNbr>>>, LeftJoin<ARRegister, On<ARRegister.docType, Equal<ARAdjust.adjdDocType>, And<ARRegister.refNbr, Equal<ARAdjust.adjdRefNbr>>>>>>>>, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.adjdCustomerID, Equal<Required<ARAdjust.adjdCustomerID>>, And<ARAdjust.adjdCustomerID, NotEqual<ARAdjust.customerID>, And<ARAdjust.released, Equal<True>>>>>>>.Config>.Select((PXGraph) this, new object[3]
          {
            (object) arRegister6.DocType,
            (object) arRegister6.RefNbr,
            (object) arRegister6.CustomerID
          });
          if (adjustments.Count > 0)
            this.ProcessAdjustmentsOnlyAdjusted(instance, adjustments);
          ((PXSelectBase) this.ARDocument).Cache.Update((object) arRegister6);
        }
        List<PXResult<ARBalances>> list = ((IEnumerable<PXResult<ARBalances>>) PXSelectBase<ARBalances, PXSelectJoin<ARBalances, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARBalances.branchID>>>, Where<ARBalances.customerID, Equal<Current<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToList<PXResult<ARBalances>>();
        ((PXGraph) this).Caches[typeof (ARBalances)].Clear();
        foreach (PXResult<ARBalances> pxResult in list)
        {
          ARBalances arBalances = PXResult<ARBalances>.op_Implicit(pxResult);
          ((PXGraph) this).Caches[typeof (ARBalances)].Insert((object) new ARBalances()
          {
            BranchID = arBalances.BranchID,
            CustomerID = arBalances.CustomerID,
            CustomerLocationID = arBalances.CustomerLocationID,
            LastDocDate = arBalances.LastDocDate,
            StatementRequired = new bool?(true)
          });
        }
        foreach (ARRegister arRegister in ((PXSelectBase) this.ARDocument).Cache.Updated)
          ((PXSelectBase) this.ARDocument).Cache.PersistUpdated((object) arRegister);
        PXSelectReadonly<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.customerID, Equal<Required<PX.Objects.SO.SOOrder.customerID>>, And<PX.Objects.SO.SOOrder.inclCustOpenOrders, Equal<True>, And<PX.Objects.SO.SOOrder.cancelled, Equal<False>, And<PX.Objects.SO.SOOrder.hold, Equal<False>, And<PX.Objects.SO.SOOrder.creditHold, Equal<False>>>>>>> pxSelectReadonly1 = new PXSelectReadonly<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.customerID, Equal<Required<PX.Objects.SO.SOOrder.customerID>>, And<PX.Objects.SO.SOOrder.inclCustOpenOrders, Equal<True>, And<PX.Objects.SO.SOOrder.cancelled, Equal<False>, And<PX.Objects.SO.SOOrder.hold, Equal<False>, And<PX.Objects.SO.SOOrder.creditHold, Equal<False>>>>>>>((PXGraph) this);
        using (new PXFieldScope(((PXSelectBase) pxSelectReadonly1).View, new System.Type[15]
        {
          typeof (PX.Objects.SO.SOOrder.orderType),
          typeof (PX.Objects.SO.SOOrder.orderNbr),
          typeof (PX.Objects.SO.SOOrder.orderDate),
          typeof (PX.Objects.SO.SOOrder.customerID),
          typeof (PX.Objects.SO.SOOrder.customerLocationID),
          typeof (PX.Objects.SO.SOOrder.branchID),
          typeof (PX.Objects.SO.SOOrder.aRDocType),
          typeof (PX.Objects.SO.SOOrder.shipmentCntr),
          typeof (PX.Objects.SO.SOOrder.inclCustOpenOrders),
          typeof (PX.Objects.SO.SOOrder.cancelled),
          typeof (PX.Objects.SO.SOOrder.hold),
          typeof (PX.Objects.SO.SOOrder.creditHold),
          typeof (PX.Objects.SO.SOOrder.noteID),
          typeof (PX.Objects.SO.SOOrder.unbilledOrderTotal),
          typeof (PX.Objects.SO.SOOrder.openOrderTotal)
        }))
        {
          foreach (PXResult<PX.Objects.SO.SOOrder> pxResult in ((PXSelectBase<PX.Objects.SO.SOOrder>) pxSelectReadonly1).Select(new object[1]
          {
            (object) cust.BAccountID
          }))
          {
            PX.Objects.SO.SOOrder order = PXResult<PX.Objects.SO.SOOrder>.op_Implicit(pxResult);
            ARReleaseProcess.UpdateARBalances((PXGraph) this, order, order.UnbilledOrderTotal, order.OpenOrderTotal);
          }
        }
        PXSelectReadonly<ARRegister, Where<ARRegister.customerID, Equal<Required<ARRegister.customerID>>, And<ARRegister.released, Equal<True>, And<ARRegister.openDoc, Equal<True>>>>> pxSelectReadonly2 = new PXSelectReadonly<ARRegister, Where<ARRegister.customerID, Equal<Required<ARRegister.customerID>>, And<ARRegister.released, Equal<True>, And<ARRegister.openDoc, Equal<True>>>>>((PXGraph) this);
        using (new PXFieldScope(((PXSelectBase) pxSelectReadonly2).View, new System.Type[13]
        {
          typeof (ARRegister.docType),
          typeof (ARRegister.refNbr),
          typeof (ARRegister.docDate),
          typeof (ARRegister.customerID),
          typeof (ARRegister.customerLocationID),
          typeof (ARRegister.branchID),
          typeof (ARRegister.released),
          typeof (ARRegister.voided),
          typeof (ARRegister.hold),
          typeof (ARRegister.scheduled),
          typeof (ARRegister.noteID),
          typeof (ARRegister.docBal),
          typeof (ARRegister.origDocAmt)
        }))
        {
          foreach (PXResult<ARRegister> pxResult in ((PXSelectBase<ARRegister>) pxSelectReadonly2).Select(new object[1]
          {
            (object) cust.BAccountID
          }))
          {
            ARRegister ardoc = PXResult<ARRegister>.op_Implicit(pxResult);
            ARReleaseProcess.UpdateARBalances((PXGraph) this, ardoc, ardoc.DocBal);
            this.UpdateARBalancesDates(ardoc);
          }
        }
        PXSelectReadonly<ARRegister, Where<ARRegister.customerID, Equal<Required<ARRegister.customerID>>, And<ARRegister.released, Equal<False>, And<ARRegister.hold, Equal<False>, And<ARRegister.voided, Equal<False>, And<ARRegister.scheduled, Equal<False>>>>>>> pxSelectReadonly3 = new PXSelectReadonly<ARRegister, Where<ARRegister.customerID, Equal<Required<ARRegister.customerID>>, And<ARRegister.released, Equal<False>, And<ARRegister.hold, Equal<False>, And<ARRegister.voided, Equal<False>, And<ARRegister.scheduled, Equal<False>>>>>>>((PXGraph) this);
        using (new PXFieldScope(((PXSelectBase) pxSelectReadonly3).View, new System.Type[13]
        {
          typeof (ARRegister.docType),
          typeof (ARRegister.refNbr),
          typeof (ARRegister.docDate),
          typeof (ARRegister.customerID),
          typeof (ARRegister.customerLocationID),
          typeof (ARRegister.branchID),
          typeof (ARRegister.released),
          typeof (ARRegister.voided),
          typeof (ARRegister.hold),
          typeof (ARRegister.scheduled),
          typeof (ARRegister.noteID),
          typeof (ARRegister.docBal),
          typeof (ARRegister.origDocAmt)
        }))
        {
          foreach (PXResult<ARRegister> pxResult in ((PXSelectBase<ARRegister>) pxSelectReadonly3).Select(new object[1]
          {
            (object) cust.BAccountID
          }))
          {
            ARRegister ardoc = PXResult<ARRegister>.op_Implicit(pxResult);
            ARReleaseProcess.UpdateARBalances((PXGraph) this, ardoc, ardoc.OrigDocAmt);
          }
        }
        PXSelectReadonly<ARInvoice, Where<ARInvoice.customerID, Equal<Required<ARInvoice.customerID>>, And<ARInvoice.creditHold, Equal<True>, And<ARInvoice.released, Equal<False>, And<ARInvoice.hold, Equal<False>, And<ARInvoice.voided, Equal<False>, And<ARInvoice.scheduled, Equal<False>>>>>>>> pxSelectReadonly4 = new PXSelectReadonly<ARInvoice, Where<ARInvoice.customerID, Equal<Required<ARInvoice.customerID>>, And<ARInvoice.creditHold, Equal<True>, And<ARInvoice.released, Equal<False>, And<ARInvoice.hold, Equal<False>, And<ARInvoice.voided, Equal<False>, And<ARInvoice.scheduled, Equal<False>>>>>>>>((PXGraph) this);
        using (new PXFieldScope(((PXSelectBase) pxSelectReadonly4).View, new System.Type[14]
        {
          typeof (ARInvoice.docType),
          typeof (ARInvoice.refNbr),
          typeof (ARInvoice.docDate),
          typeof (ARInvoice.customerID),
          typeof (ARInvoice.customerLocationID),
          typeof (ARInvoice.branchID),
          typeof (ARInvoice.released),
          typeof (ARInvoice.voided),
          typeof (ARInvoice.hold),
          typeof (ARInvoice.creditHold),
          typeof (ARInvoice.scheduled),
          typeof (ARInvoice.noteID),
          typeof (ARInvoice.docBal),
          typeof (ARInvoice.origDocAmt)
        }))
        {
          foreach (PXResult<ARInvoice> pxResult in ((PXSelectBase<ARInvoice>) pxSelectReadonly4).Select(new object[1]
          {
            (object) cust.BAccountID
          }))
          {
            ARInvoice arInvoice = PXResult<ARInvoice>.op_Implicit(pxResult);
            arInvoice.CreditHold = new bool?(false);
            ARInvoice ardoc = arInvoice;
            Decimal? origDocAmt = arInvoice.OrigDocAmt;
            Decimal? BalanceAmt = origDocAmt.HasValue ? new Decimal?(-origDocAmt.GetValueOrDefault()) : new Decimal?();
            ARReleaseProcess.UpdateARBalances((PXGraph) this, ardoc, BalanceAmt);
          }
        }
        ((PXSelectBase) this.TranPost).Cache.Persist((PXDBOperation) 2);
        ((PXSelectBase) this.StatementDetailsView).Cache.Persist((PXDBOperation) 1);
        if (flag1)
          ((PXSelectBase) this.ARTran_TranType_RefNbr).Cache.Persist((PXDBOperation) 1);
        ((PXGraph) this).Caches[typeof (ARAdjust)].Persist((PXDBOperation) 1);
        ((PXGraph) this).Caches[typeof (ARHist)].Persist((PXDBOperation) 2);
        ((PXGraph) this).Caches[typeof (CuryARHist)].Persist((PXDBOperation) 2);
        ((PXGraph) this).Caches[typeof (ARBalances)].Persist((PXDBOperation) 2);
        transactionScope.Complete((PXGraph) this);
      }
      this._oldInvoiceRefresher.CommitRefresh((PXGraph) this);
      ((PXSelectBase) this.ARDocument).Cache.Persisted(false);
      ((PXGraph) this).Caches[typeof (ARHist)].Persisted(false);
      ((PXGraph) this).Caches[typeof (CuryARHist)].Persisted(false);
      ((PXGraph) this).Caches[typeof (ARBalances)].Persisted(false);
      ((PXSelectBase) this.TranPost).Cache.Persisted(false);
      if (flag1)
        ((PXSelectBase) this.ARTran_TranType_RefNbr).Cache.Persisted(false);
      ((PXGraph) this).Caches[typeof (ARAdjust)].Persisted(false);
    }
  }

  protected virtual HashedList<ARRegister> GetDocumentsForIntegrityCheckProc(string minPeriod)
  {
    HashedList<ARRegister> documents = new HashedList<ARRegister>((IEqualityComparer<ARRegister>) PXCacheEx.GetComparer(((PXSelectBase) this.ARDocument).Cache));
    foreach (PXResult<ARRegister, ARInvoice, ARPayment> rec in PXSelectBase<ARRegister, PXSelectJoin<ARRegister, LeftJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARRegister.docType>, And<ARInvoice.refNbr, Equal<ARRegister.refNbr>>>, LeftJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegister.docType>, And<ARPayment.refNbr, Equal<ARRegister.refNbr>>>>>, Where<ARRegister.customerID, Equal<Current<Customer.bAccountID>>, And<ARRegister.released, Equal<True>, And<Where<ARRegister.tranPeriodID, GreaterEqual<Required<ARRegister.tranPeriodID>>, Or<ARRegister.closedTranPeriodID, GreaterEqual<Required<ARRegister.closedTranPeriodID>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) minPeriod,
      (object) minPeriod
    }))
    {
      ARRegister fullDocument = this.GetFullDocument(rec);
      if (fullDocument != null)
        documents.Add(fullDocument);
    }
    PXResultset<ARRegister> integrityCheckProc = this.GetDirectAdjustmentsForIntegrityCheckProc(minPeriod);
    documents.AddRange((IEnumerable) GraphHelper.RowCast<ARRegister>((IEnumerable) integrityCheckProc));
    this.GetAllReleasedAdjustments(documents, integrityCheckProc, minPeriod);
    if (PXAccess.FeatureInstalled<FeaturesSet.retainage>())
    {
      this.GetReleasedOriginalDocumentsWithAdjustments(documents, minPeriod);
      this.GetReleasedRetainageDocumentsWithAdjustments(documents, minPeriod);
    }
    return documents;
  }

  private ARRegister GetFullDocument(PXResult<ARRegister, ARInvoice, ARPayment> rec)
  {
    ARInvoice arInvoice = PXResult<ARRegister, ARInvoice, ARPayment>.op_Implicit(rec);
    ARPayment arPayment = PXResult<ARRegister, ARInvoice, ARPayment>.op_Implicit(rec);
    ARRegister fullDocument = (ARRegister) null;
    if (arInvoice != null && arInvoice.RefNbr != null)
    {
      PXCache<ARRegister>.RestoreCopy((ARRegister) arInvoice, PXResult<ARRegister, ARInvoice, ARPayment>.op_Implicit(rec));
      fullDocument = (ARRegister) arInvoice;
    }
    else if (arPayment != null && arPayment.RefNbr != null)
    {
      PXCache<ARRegister>.RestoreCopy((ARRegister) arPayment, PXResult<ARRegister, ARInvoice, ARPayment>.op_Implicit(rec));
      fullDocument = (ARRegister) arPayment;
    }
    return fullDocument;
  }

  public virtual ARRegister GetFullDocumentFromDB(ARRegister document)
  {
    PXResult<ARRegister, ARInvoice, ARPayment> pxResult = (PXResult<ARRegister, ARInvoice, ARPayment>) PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelectJoin<ARRegister, LeftJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARRegister.docType>, And<ARInvoice.refNbr, Equal<ARRegister.refNbr>>>, LeftJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegister.docType>, And<ARPayment.refNbr, Equal<ARRegister.refNbr>>>>>, Where<Where<ARRegister.docType, Equal<Required<ARRegister.docType>>, And<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) document.DocType,
      (object) document.RefNbr
    }));
    ARInvoice arInvoice = PXResult<ARRegister, ARInvoice, ARPayment>.op_Implicit(pxResult);
    ARPayment arPayment = PXResult<ARRegister, ARInvoice, ARPayment>.op_Implicit(pxResult);
    ARRegister fullDocumentFromDb = (ARRegister) null;
    if (arInvoice?.RefNbr != null)
    {
      if (arPayment?.RefNbr != null)
      {
        if (arPayment?.RefNbr != null)
        {
          bool? released = document.Released;
          bool flag = false;
          if (!(released.GetValueOrDefault() == flag & released.HasValue))
            goto label_5;
        }
        else
          goto label_5;
      }
      PXCache<ARRegister>.RestoreCopy((ARRegister) arInvoice, PXResult<ARRegister, ARInvoice, ARPayment>.op_Implicit(pxResult));
      fullDocumentFromDb = (ARRegister) arInvoice;
      goto label_7;
    }
label_5:
    if (arPayment?.RefNbr != null)
    {
      PXCache<ARRegister>.RestoreCopy((ARRegister) arPayment, PXResult<ARRegister, ARInvoice, ARPayment>.op_Implicit(pxResult));
      fullDocumentFromDb = (ARRegister) arPayment;
    }
label_7:
    return fullDocumentFromDb;
  }

  protected virtual void GetReleasedOriginalDocumentsWithAdjustments(
    HashedList<ARRegister> documents,
    string minPeriod)
  {
    foreach ((string, string) hash in GraphHelper.RowCast<ARRegister>((IEnumerable) documents).Where<ARRegister>((Func<ARRegister, bool>) (adj => adj.IsRetainageDocument.GetValueOrDefault())).Select<ARRegister, (string, string)>((Func<ARRegister, (string, string)>) (_ => (_.OrigDocType, _.OrigRefNbr))).ToHashSet<(string, string)>())
    {
      PXResultset<ARRegister> startFrom = ((PXSelectBase<ARRegister>) new FbqlSelect<SelectFromBase<ARRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<ARInvoice>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.refNbr, Equal<ARRegister.refNbr>>>>>.And<BqlOperand<ARInvoice.docType, IBqlString>.IsEqual<ARRegister.docType>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<ARRegister.refNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<ARRegister.retainageApply, IBqlBool>.IsEqual<True>>>, And<BqlOperand<ARRegister.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<ARRegister.tranPeriodID, IBqlString>.IsLess<P.AsString.ASCII>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.closedTranPeriodID, IsNull>>>>.Or<BqlOperand<ARRegister.closedTranPeriodID, IBqlString>.IsLess<P.AsString.ASCII>>>>, ARRegister>.View((PXGraph) this)).Select(new object[4]
      {
        (object) hash.Item1,
        (object) hash.Item2,
        (object) minPeriod,
        (object) minPeriod
      });
      foreach (PXResult<ARRegister> pxResult in startFrom)
      {
        ARInvoice arInvoice = PXResult.Unwrap<ARInvoice>((object) pxResult);
        PXCache<ARRegister>.RestoreCopy((ARRegister) arInvoice, PXResult.Unwrap<ARRegister>((object) pxResult));
        if (!documents.Contains((ARRegister) arInvoice))
          documents.Add(PXResult<ARRegister>.op_Implicit(new PXResult<ARRegister>((ARRegister) arInvoice)));
      }
      this.GetAllReleasedAdjustments(documents, startFrom, minPeriod);
    }
  }

  protected virtual void GetReleasedRetainageDocumentsWithAdjustments(
    HashedList<ARRegister> documents,
    string minPeriod)
  {
    foreach ((string, string) hash in GraphHelper.RowCast<ARRegister>((IEnumerable) documents).Where<ARRegister>((Func<ARRegister, bool>) (adj => adj.RetainageApply.GetValueOrDefault())).Select<ARRegister, (string, string)>((Func<ARRegister, (string, string)>) (_ => (_.DocType, _.RefNbr))).ToHashSet<(string, string)>())
    {
      PXResultset<ARRegister> startFrom = ((PXSelectBase<ARRegister>) new FbqlSelect<SelectFromBase<ARRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<ARInvoice>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.refNbr, Equal<ARRegister.refNbr>>>>>.And<BqlOperand<ARInvoice.docType, IBqlString>.IsEqual<ARRegister.docType>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.origDocType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<ARRegister.origRefNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<ARRegister.isRetainageDocument, IBqlBool>.IsEqual<True>>>, And<BqlOperand<ARRegister.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<ARRegister.tranPeriodID, IBqlString>.IsLess<P.AsString.ASCII>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.closedTranPeriodID, IsNull>>>>.Or<BqlOperand<ARRegister.closedTranPeriodID, IBqlString>.IsLess<P.AsString.ASCII>>>>, ARRegister>.View((PXGraph) this)).Select(new object[4]
      {
        (object) hash.Item1,
        (object) hash.Item2,
        (object) minPeriod,
        (object) minPeriod
      });
      foreach (PXResult<ARRegister> pxResult in startFrom)
      {
        ARInvoice arInvoice = PXResult.Unwrap<ARInvoice>((object) pxResult);
        PXCache<ARRegister>.RestoreCopy((ARRegister) arInvoice, PXResult.Unwrap<ARRegister>((object) pxResult));
        if (!documents.Contains((ARRegister) arInvoice))
          documents.Add(PXResult<ARRegister>.op_Implicit(new PXResult<ARRegister>((ARRegister) arInvoice)));
      }
      this.GetAllReleasedAdjustments(documents, startFrom, minPeriod);
    }
  }

  private IEnumerable<PXResult<ARRegister>> GetFullDocuments(IEnumerable<PXResult<ARRegister>> list)
  {
    foreach (PXResult<ARRegister, ARInvoice, ARPayment> rec in list)
    {
      ARRegister fullDocument = this.GetFullDocument(rec);
      if (fullDocument != null)
        yield return new PXResult<ARRegister>(fullDocument);
    }
  }

  protected virtual PXResultset<ARRegister> GetDirectAdjustmentsForIntegrityCheckProc(
    string minPeriod)
  {
    PXResultset<ARRegister> list1 = PXSelectBase<ARRegister, PXSelectJoin<ARRegister, LeftJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARRegister.docType>, And<ARInvoice.refNbr, Equal<ARRegister.refNbr>>>, LeftJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegister.docType>, And<ARPayment.refNbr, Equal<ARRegister.refNbr>>>>>, Where<ARRegister.customerID, Equal<Current<Customer.bAccountID>>, And<ARRegister.tranPeriodID, Less<Required<ARRegister.tranPeriodID>>, And<ARRegister.released, Equal<True>, And2<Where<ARRegister.closedTranPeriodID, Less<Required<ARRegister.closedTranPeriodID>>, Or<ARRegister.closedTranPeriodID, IsNull>>, And<Exists<Select2<ARAdjust, InnerJoin<PX.Objects.AR.Standalone.ARRegister2, On<PX.Objects.AR.Standalone.ARRegister2.docType, Equal<ARAdjust.adjdDocType>, And<PX.Objects.AR.Standalone.ARRegister2.refNbr, Equal<ARAdjust.adjdRefNbr>>>>, Where2<Where<ARAdjust.adjgDocType, Equal<ARRegister.docType>, Or<ARAdjust.adjgDocType, Equal<ARDocType.payment>, And<ARRegister.docType, Equal<ARDocType.voidPayment>, Or<ARAdjust.adjgDocType, Equal<ARDocType.prepayment>, And<ARRegister.docType, Equal<ARDocType.voidPayment>>>>>>, And<ARAdjust.adjgRefNbr, Equal<ARRegister.refNbr>, And2<Where<PX.Objects.AR.Standalone.ARRegister2.closedTranPeriodID, GreaterEqual<Required<PX.Objects.AR.Standalone.ARRegister2.closedTranPeriodID>>, Or<ARAdjust.adjgTranPeriodID, GreaterEqual<Required<ARAdjust.adjdTranPeriodID>>>>, And<ARAdjust.released, Equal<True>>>>>>>>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) minPeriod,
      (object) minPeriod,
      (object) minPeriod,
      (object) minPeriod
    });
    PXResultset<ARRegister> list2 = PXSelectBase<ARRegister, PXSelectJoin<ARRegister, LeftJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARRegister.docType>, And<ARInvoice.refNbr, Equal<ARRegister.refNbr>>>, LeftJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegister.docType>, And<ARPayment.refNbr, Equal<ARRegister.refNbr>>>>>, Where<ARRegister.customerID, Equal<Current<Customer.bAccountID>>, And<ARRegister.tranPeriodID, Less<Required<ARRegister.tranPeriodID>>, And<ARRegister.released, Equal<True>, And2<Where<ARRegister.closedTranPeriodID, Less<Required<ARRegister.closedTranPeriodID>>, Or<ARRegister.closedTranPeriodID, IsNull>>, And<Exists<Select2<ARAdjust, InnerJoin<PX.Objects.AR.Standalone.ARRegister2, On<PX.Objects.AR.Standalone.ARRegister2.docType, Equal<ARAdjust.adjgDocType>, And<PX.Objects.AR.Standalone.ARRegister2.refNbr, Equal<ARAdjust.adjgRefNbr>>>>, Where2<Where<ARAdjust.adjdDocType, Equal<ARRegister.docType>, Or<ARAdjust.adjdDocType, Equal<ARDocType.payment>, And<ARRegister.docType, Equal<ARDocType.voidPayment>, Or<ARAdjust.adjdDocType, Equal<ARDocType.prepayment>, And<ARRegister.docType, Equal<ARDocType.voidPayment>>>>>>, And<ARAdjust.adjdRefNbr, Equal<ARRegister.refNbr>, And2<Where<PX.Objects.AR.Standalone.ARRegister2.closedTranPeriodID, GreaterEqual<Required<PX.Objects.AR.Standalone.ARRegister2.closedTranPeriodID>>, Or<ARAdjust.adjgTranPeriodID, GreaterEqual<Required<ARAdjust.adjgTranPeriodID>>>>, And<ARAdjust.released, Equal<True>>>>>>>>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) minPeriod,
      (object) minPeriod,
      (object) minPeriod,
      (object) minPeriod
    });
    PXResultset<ARRegister> integrityCheckProc = new PXResultset<ARRegister>();
    integrityCheckProc.AddRange(this.GetFullDocuments((IEnumerable<PXResult<ARRegister>>) list1));
    integrityCheckProc.AddRange(this.GetFullDocuments((IEnumerable<PXResult<ARRegister>>) list2));
    return integrityCheckProc;
  }

  protected virtual void GetAllReleasedAdjustments(
    HashedList<ARRegister> documents,
    PXResultset<ARRegister> startFrom,
    string minPeriod)
  {
    HashSet<\u003C\u003Ef__AnonymousType0<string, string>> hashSet = GraphHelper.RowCast<ARRegister>((IEnumerable) startFrom).Select(_ => new
    {
      DocType = _.DocType,
      RefNbr = _.RefNbr
    }).ToHashSet();
    while (hashSet.Any())
    {
      List<\u003C\u003Ef__AnonymousType0<string, string>> list = hashSet.ToList();
      hashSet.Clear();
      foreach (var data in list)
      {
        foreach (PXResult<ARRegister> fullDocument in this.GetFullDocuments((IEnumerable<PXResult<ARRegister>>) PXSelectBase<ARRegister, PXSelectJoin<ARRegister, LeftJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARRegister.docType>, And<ARInvoice.refNbr, Equal<ARRegister.refNbr>>>, LeftJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegister.docType>, And<ARPayment.refNbr, Equal<ARRegister.refNbr>>>>>, Where<ARRegister.customerID, Equal<Current<Customer.bAccountID>>, And2<Exists<Select<ARAdjust, Where2<Where<ARAdjust.adjgDocType, Equal<ARRegister.docType>, Or<ARAdjust.adjgDocType, Equal<ARDocType.payment>, And<ARRegister.docType, Equal<ARDocType.voidPayment>, Or<ARAdjust.adjgDocType, Equal<ARDocType.prepayment>, And<ARRegister.docType, Equal<ARDocType.voidPayment>>>>>>, And<ARAdjust.adjgRefNbr, Equal<ARRegister.refNbr>, And<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.released, Equal<True>>>>>>>>, And<ARRegister.tranPeriodID, Less<Required<ARRegister.tranPeriodID>>, And<ARRegister.released, Equal<True>, And<Where<ARRegister.closedTranPeriodID, Less<Required<ARRegister.closedTranPeriodID>>, Or<ARRegister.closedTranPeriodID, IsNull>>>>>>>>.Config>.Select((PXGraph) this, new object[4]
        {
          (object) data.DocType,
          (object) data.RefNbr,
          (object) minPeriod,
          (object) minPeriod
        })))
        {
          ARRegister arRegister = PXResult<ARRegister>.op_Implicit(fullDocument);
          if (!documents.Contains(arRegister))
          {
            hashSet.Add(new
            {
              DocType = arRegister.DocType,
              RefNbr = arRegister.RefNbr
            });
            documents.Add(PXResult<ARRegister>.op_Implicit(fullDocument));
          }
        }
        foreach (PXResult<ARRegister> fullDocument in this.GetFullDocuments((IEnumerable<PXResult<ARRegister>>) PXSelectBase<ARRegister, PXSelectJoin<ARRegister, LeftJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARRegister.docType>, And<ARInvoice.refNbr, Equal<ARRegister.refNbr>>>, LeftJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegister.docType>, And<ARPayment.refNbr, Equal<ARRegister.refNbr>>>>>, Where<ARRegister.customerID, Equal<Current<Customer.bAccountID>>, And2<Exists<Select<ARAdjust, Where2<Where<ARAdjust.adjdDocType, Equal<ARRegister.docType>, Or<ARAdjust.adjdDocType, Equal<ARDocType.payment>, And<ARRegister.docType, Equal<ARDocType.voidPayment>, Or<ARAdjust.adjdDocType, Equal<ARDocType.prepayment>, And<ARRegister.docType, Equal<ARDocType.voidPayment>>>>>>, And<ARAdjust.adjdRefNbr, Equal<ARRegister.refNbr>, And<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<ARAdjust.released, Equal<True>>>>>>>>, And<ARRegister.tranPeriodID, Less<Required<ARRegister.tranPeriodID>>, And<ARRegister.released, Equal<True>, And<Where<ARRegister.closedTranPeriodID, Less<Required<ARRegister.closedTranPeriodID>>, Or<ARRegister.closedTranPeriodID, IsNull>>>>>>>>.Config>.Select((PXGraph) this, new object[4]
        {
          (object) data.DocType,
          (object) data.RefNbr,
          (object) minPeriod,
          (object) minPeriod
        })))
        {
          ARRegister arRegister = PXResult<ARRegister>.op_Implicit(fullDocument);
          if (!documents.Contains(arRegister))
          {
            hashSet.Add(new
            {
              DocType = arRegister.DocType,
              RefNbr = arRegister.RefNbr
            });
            documents.Add(PXResult<ARRegister>.op_Implicit(fullDocument));
          }
        }
      }
    }
  }

  protected static void Copy(ARSalesPerTran aDest, ARAdjust aAdj)
  {
    aDest.AdjdDocType = aAdj.AdjdDocType;
    aDest.AdjdRefNbr = aAdj.AdjdRefNbr;
    aDest.AdjNbr = aAdj.AdjNbr;
    aDest.BranchID = aAdj.AdjdBranchID;
    aDest.Released = new bool?(true);
  }

  protected static void Copy(ARSalesPerTran aDest, ARRegister aReg)
  {
    aDest.DocType = aReg.DocType;
    aDest.RefNbr = aReg.RefNbr;
  }

  protected static void CopyShare(
    ARSalesPerTran aDest,
    ARSalesPerTran aSrc,
    Decimal aRatio,
    short aPrecision)
  {
    ARSalesPerTran arSalesPerTran1 = aDest;
    Decimal num1 = aRatio;
    Decimal? curyCommnblAmt = aSrc.CuryCommnblAmt;
    Decimal? nullable1 = new Decimal?(Math.Round((curyCommnblAmt.HasValue ? new Decimal?(num1 * curyCommnblAmt.GetValueOrDefault()) : new Decimal?()).Value, (int) aPrecision));
    arSalesPerTran1.CuryCommnblAmt = nullable1;
    ARSalesPerTran arSalesPerTran2 = aDest;
    Decimal num2 = aRatio;
    Decimal? curyCommnAmt = aSrc.CuryCommnAmt;
    Decimal? nullable2 = new Decimal?(Math.Round((curyCommnAmt.HasValue ? new Decimal?(num2 * curyCommnAmt.GetValueOrDefault()) : new Decimal?()).Value, (int) aPrecision));
    arSalesPerTran2.CuryCommnAmt = nullable2;
  }

  protected ARSalesPerTran CreatePaymentSPT(ARRegister payment, ARAdjust adj, ARSalesPerTran iSPT)
  {
    ARSalesPerTran aDest = new ARSalesPerTran();
    ARReleaseProcess.Copy(aDest, payment);
    ARReleaseProcess.Copy(aDest, adj);
    aDest.SalespersonID = iSPT.SalespersonID;
    aDest.CuryInfoID = iSPT.CuryInfoID;
    aDest.BaseCuryID = iSPT.BaseCuryID;
    aDest.CommnPct = iSPT.CommnPct;
    return aDest;
  }

  public virtual void ClearRetainageAmount(ARRegister childRetainageDoc)
  {
    ARRegister retainageDocument = this.GetOriginalRetainageDocument(childRetainageDoc);
    if (retainageDocument == null)
      return;
    Decimal? nullable1 = retainageDocument.RetainageUnreleasedAmt;
    Decimal? nullable2 = retainageDocument.CuryRetainageTotal;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      throw new PXException("The {0} credit memo cannot be released because its original document has a released retainage invoice.", new object[1]
      {
        (object) childRetainageDoc.RefNbr
      });
    childRetainageDoc.CuryRetainageUnreleasedAmt = new Decimal?(0M);
    childRetainageDoc.CuryRetainageReleased = new Decimal?(0M);
    childRetainageDoc.CuryRetainageUnpaidTotal = new Decimal?(0M);
    retainageDocument.CuryRetainageUnreleasedAmt = new Decimal?(0M);
    retainageDocument.CuryRetainageReleased = new Decimal?(0M);
    ARRegister arRegister = retainageDocument;
    nullable2 = retainageDocument.CuryRetainagePaidTotal;
    Decimal? nullable3;
    if (!nullable2.HasValue)
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = new Decimal?(-nullable2.GetValueOrDefault());
    arRegister.CuryRetainageUnpaidTotal = nullable3;
    using (new DisableFormulaCalculationScope(((PXSelectBase) this.ARDocument).Cache, new System.Type[1]
    {
      typeof (ARRegister.curyRetainageReleased)
    }))
      ((PXSelectBase<ARRegister>) this.ARDocument).Update(retainageDocument);
  }

  public virtual void ReleaseRetainageAmount(ARRegister childRetainageDoc)
  {
    IEqualityComparer<ARTran> comparer = (IEqualityComparer<ARTran>) new FieldSubsetEqualityComparer<ARTran>(((PXSelectBase) this.ARTran_TranType_RefNbr).Cache, new System.Type[3]
    {
      typeof (ARTran.tranType),
      typeof (ARTran.refNbr),
      typeof (ARTran.lineNbr)
    });
    HashSet<ARRegister> arRegisterSet = new HashSet<ARRegister>();
    HashSet<string> stringSet = new HashSet<string>();
    foreach (IGrouping<ARTran, PXResult<ARTran>> grouping in ((IEnumerable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) this.ARTran_TranType_RefNbr).Select(new object[2]
    {
      (object) childRetainageDoc.DocType,
      (object) childRetainageDoc.RefNbr
    })).AsEnumerable<PXResult<ARTran>>().GroupBy<PXResult<ARTran>, ARTran>((Func<PXResult<ARTran>, ARTran>) (row => PXResult<ARTran>.op_Implicit(row)), comparer))
    {
      ARTran key = grouping.Key;
      ARRegister retainageDocument = this.GetOriginalRetainageDocument(key);
      if (retainageDocument != null)
      {
        Decimal num1 = 0M;
        bool? nullable1;
        Decimal? nullable2;
        foreach (PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran> pxResult in (IEnumerable<PXResult<ARTran>>) grouping)
        {
          ARTax arTax = PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(pxResult);
          PX.Objects.TX.Tax tax = PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(pxResult);
          ARTaxTran arTaxTran = PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(pxResult);
          if (!(retainageDocument.TaxCalcMode == "G") && (!(tax.TaxCalcLevel == "0") || !(retainageDocument.TaxCalcMode != "N")))
          {
            nullable1 = childRetainageDoc.PaymentsByLinesAllowed;
            if (nullable1.GetValueOrDefault())
            {
              Decimal num2 = num1;
              nullable2 = arTax.CuryTaxAmt;
              Decimal valueOrDefault = nullable2.GetValueOrDefault();
              num1 = num2 + valueOrDefault;
            }
            else if (!stringSet.Contains(arTaxTran.TaxID))
            {
              Decimal num3 = num1;
              nullable2 = arTaxTran.CuryTaxAmt;
              Decimal valueOrDefault = nullable2.GetValueOrDefault();
              num1 = num3 + valueOrDefault;
              stringSet.Add(arTaxTran.TaxID);
            }
          }
        }
        nullable2 = retainageDocument.SignAmount;
        Decimal? nullable3 = childRetainageDoc.SignAmount;
        Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
        Decimal valueOrDefault1 = nullable4.GetValueOrDefault();
        nullable3 = key.CuryTranAmt;
        Decimal num4 = num1;
        Decimal? nullable5;
        if (!nullable3.HasValue)
        {
          nullable4 = new Decimal?();
          nullable5 = nullable4;
        }
        else
          nullable5 = new Decimal?(nullable3.GetValueOrDefault() + num4);
        nullable2 = nullable5;
        Decimal num5 = valueOrDefault1;
        Decimal? nullable6;
        if (!nullable2.HasValue)
        {
          nullable3 = new Decimal?();
          nullable6 = nullable3;
        }
        else
          nullable6 = new Decimal?(nullable2.GetValueOrDefault() * num5);
        nullable3 = nullable6;
        Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
        ARRegister arRegister1 = retainageDocument;
        nullable2 = arRegister1.CuryRetainageUnreleasedAmt;
        Decimal num6 = valueOrDefault2;
        Decimal? nullable7;
        if (!nullable2.HasValue)
        {
          nullable3 = new Decimal?();
          nullable7 = nullable3;
        }
        else
          nullable7 = new Decimal?(nullable2.GetValueOrDefault() - num6);
        arRegister1.CuryRetainageUnreleasedAmt = nullable7;
        ARRegister arRegister2 = retainageDocument;
        nullable2 = arRegister2.CuryRetainageReleased;
        Decimal num7 = valueOrDefault2;
        Decimal? nullable8;
        if (!nullable2.HasValue)
        {
          nullable3 = new Decimal?();
          nullable8 = nullable3;
        }
        else
          nullable8 = new Decimal?(nullable2.GetValueOrDefault() + num7);
        arRegister2.CuryRetainageReleased = nullable8;
        nullable1 = retainageDocument.Released;
        if (!nullable1.GetValueOrDefault())
        {
          ARRegister arRegister3 = retainageDocument;
          nullable2 = retainageDocument.CuryRetainageUnreleasedAmt;
          nullable3 = retainageDocument.CuryRetainageReleased;
          Decimal? nullable9;
          if (!(nullable2.HasValue & nullable3.HasValue))
          {
            nullable4 = new Decimal?();
            nullable9 = nullable4;
          }
          else
            nullable9 = new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault());
          arRegister3.CuryRetainageUnpaidTotal = nullable9;
        }
        arRegisterSet.Add(((PXSelectBase<ARRegister>) this.ARDocument).Update(retainageDocument));
      }
    }
    foreach (ARRegister arRegister in arRegisterSet)
    {
      if (!this._IsIntegrityCheck && !arRegister.PaymentsByLinesAllowed.GetValueOrDefault())
      {
        Decimal? retainageUnreleasedAmt = arRegister.CuryRetainageUnreleasedAmt;
        Decimal num = 0M;
        if (!(retainageUnreleasedAmt.GetValueOrDefault() < num & retainageUnreleasedAmt.HasValue))
        {
          retainageUnreleasedAmt = arRegister.CuryRetainageUnreleasedAmt;
          Decimal? curyRetainageTotal = arRegister.CuryRetainageTotal;
          if (!(retainageUnreleasedAmt.GetValueOrDefault() > curyRetainageTotal.GetValueOrDefault() & retainageUnreleasedAmt.HasValue & curyRetainageTotal.HasValue))
            continue;
        }
        throw new PXException("The document cannot be released because the retainage has been fully released for the related original document.");
      }
    }
  }

  public virtual ARRegister GetOriginalRetainageDocument(ARRegister childRetainageDoc)
  {
    return this.GetOriginalRetainageDocument(childRetainageDoc.OrigDocType, childRetainageDoc.OrigRefNbr);
  }

  public virtual ARRegister GetOriginalRetainageDocument(ARTran childRetainageLine)
  {
    return this.GetOriginalRetainageDocument(childRetainageLine.OrigDocType, childRetainageLine.OrigRefNbr);
  }

  public virtual ARRegister GetOriginalRetainageDocument(string origDocType, string origRefNbr)
  {
    ARInvoice retainageDocument = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>, And<ARInvoice.retainageApply, Equal<True>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) origDocType,
      (object) origRefNbr
    }));
    if (((PXSelectBase) this.ARDocument).Cache.Locate((object) retainageDocument) is ARRegister arRegister && retainageDocument != null)
      ((PXSelectBase) this.ARDocument).Cache.RestoreCopy((object) retainageDocument, (object) arRegister);
    return (ARRegister) retainageDocument;
  }

  public virtual ARTran GetOriginalRetainageLine(
    ARRegister childRetainageDoc,
    ARTran childRetainageTran)
  {
    return PXResultset<ARTran>.op_Implicit(PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<ARTran.lineNbr, Equal<Required<ARTran.lineNbr>>, And<ARTran.curyRetainageAmt, NotEqual<decimal0>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
    {
      (object) childRetainageDoc.OrigDocType,
      (object) childRetainageDoc.OrigRefNbr,
      (object) childRetainageTran.OrigLineNbr
    }));
  }

  public virtual ARTran GetOriginalRetainageLine(ARTran childRetainageTran)
  {
    return PXResultset<ARTran>.op_Implicit(PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<ARTran.lineNbr, Equal<Required<ARTran.lineNbr>>, And<ARTran.curyRetainageAmt, NotEqual<decimal0>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
    {
      (object) childRetainageTran.OrigDocType,
      (object) childRetainageTran.OrigRefNbr,
      (object) childRetainageTran.OrigLineNbr
    }));
  }

  public virtual bool IsFullyProcessedOriginalRetainageDocument(ARRegister origRetainageInvoice)
  {
    bool flag = true;
    foreach (PXResult<ARRegister> pxResult in PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.isRetainageDocument, Equal<True>, And<ARRegister.origDocType, Equal<Required<ARRegister.docType>>, And<ARRegister.origRefNbr, Equal<Required<ARRegister.refNbr>>, And<ARRegister.released, Equal<True>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) origRetainageInvoice.DocType,
      (object) origRetainageInvoice.RefNbr
    }))
    {
      if (!PXResult<ARRegister>.op_Implicit(pxResult).HasZeroBalance<ARRegister.curyDocBal, ARTran.curyTranBal>((PXGraph) this))
      {
        flag = false;
        break;
      }
    }
    return ((!origRetainageInvoice.HasZeroBalance<ARRegister.curyDocBal, ARTran.curyTranBal>((PXGraph) this) ? 0 : (origRetainageInvoice.HasZeroBalance<ARRegister.curyRetainageUnreleasedAmt, ARTran.curyRetainageBal>((PXGraph) this) ? 1 : 0)) & (flag ? 1 : 0)) != 0;
  }

  /// <summary>
  /// Posts per-unit tax amounts to document lines' accounts.
  /// This is an extension point, actual posting is done by graph extension <see cref="T:PX.Objects.AR.ARReleaseProcessPerUnitTaxPoster" />
  /// which overrides this method if the festure "Per-unit Tax Support" is turned on.
  /// </summary>
  protected virtual void PostPerUnitTaxAmounts(
    JournalEntry journalEntry,
    ARInvoice invoice,
    PX.Objects.CM.Extensions.CurrencyInfo newCurrencyInfo,
    ARTaxTran perUnitAggregatedTax,
    PX.Objects.TX.Tax perUnitTax,
    bool isDebitTaxTran)
  {
  }

  /// <summary>
  /// The method to insert invoice GL transactions
  /// for the <see cref="T:PX.Objects.AR.ARInvoice" /> entity inside the
  /// <see cref="!:ReleaseInvoice(JournalEntry, ref ARRegister, PXResult&lt;ARInvoice, CurrencyInfo, Terms, Customer, Account&gt;, out PMRegister)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert invoice tax GL transactions
  /// for the <see cref="T:PX.Objects.AR.ARTaxTran" /> entity inside the
  /// <see cref="!:ReleaseInvoice(JournalEntry, ref ARRegister, PXResult&lt;ARInvoice, CurrencyInfo, Terms, Customer, Account&gt;, out PMRegister)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARTaxTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceTaxTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert tax expense GL transactions for the <see cref="T:PX.Objects.AR.ARTaxTran" /> entity inside the
  /// <see cref="!:PerUnitTaxesPostOnRelease.PostPerUnitTaxAmountsToItemAccounts(APInvoice, CurrencyInfo, ARTaxTran, Tax, bool, bool)" /> helper method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARTranRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARTaxTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoicePerUnitTaxAmountsToItemAccountsTransaction(
    JournalEntry journalEntryGraph,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    ExceptionExtensions.ThrowOnNull<JournalEntry>(journalEntryGraph, nameof (journalEntryGraph), (string) null);
    return ((PXSelectBase<PX.Objects.GL.GLTran>) journalEntryGraph.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert invoice rounding GL transactions
  /// for the <see cref="T:PX.Objects.AR.ARInvoice" /> entity inside the
  /// <see cref="!:ReleaseInvoice(JournalEntry, ref ARRegister, PXResult&lt;ARInvoice, CurrencyInfo, Terms, Customer, Account&gt;, out PMRegister)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceRoundingTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert invoice details GL transactions
  /// for the <see cref="T:PX.Objects.AR.ARTran" /> entity inside the
  /// <see cref="!:ReleaseInvoice(JournalEntry, ref ARRegister, PXResult&lt;ARInvoice, CurrencyInfo, Terms, Customer, Account&gt;, out PMRegister)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceDetailsTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert invoice details schedule GL transactions
  /// for the <see cref="T:PX.Objects.AR.ARTran" /> entity inside the
  /// <see cref="!:ReleaseInvoice(JournalEntry, ref ARRegister, PXResult&lt;ARInvoice, CurrencyInfo, Terms, Customer, Account&gt;, out PMRegister)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceDetailsScheduleTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert invoice details INCost GL transactions
  /// for the <see cref="T:PX.Objects.IN.INTranCost" /> entity inside the
  /// <see cref="!:ReleaseInvoice(JournalEntry, ref ARRegister, PXResult&lt;ARInvoice, CurrencyInfo, Terms, Customer, Account&gt;, out PMRegister)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARTranRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.INTranRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.INTranCostRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceDetailsINTranCostTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert payment CADeposit GL transactions
  /// for the <see cref="T:PX.Objects.CA.CADeposit" /> entity inside the
  /// <see cref="M:PX.Objects.AR.ARReleaseProcess.ReleaseDocProc(PX.Objects.GL.JournalEntry,PX.Objects.AR.ARRegister,System.Collections.Generic.List{PX.Objects.GL.Batch},PX.Objects.AR.ARDocumentRelease.ARMassProcessReleaseTransactionScopeDelegate)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.CADepositRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.CADepositDetailRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertPaymentCADepositTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert payment GL transactions
  /// for the <see cref="T:PX.Objects.AR.ARPayment" /> entity inside the
  /// <see cref="!:ProcessPayment(JournalEntry, ARRegister, PXResult&lt;ARPayment, CurrencyInfo, CM.Currency, Customer, CashAccount&gt;)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertPaymentTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert payment charge GL transactions
  /// for the <see cref="T:PX.Objects.AR.ARPaymentChargeTran" /> entity inside the
  /// <see cref="!:ProcessPayment(JournalEntry, ARRegister, PXResult&lt;ARPayment, CurrencyInfo, CM.Currency, Customer, CashAccount&gt;)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARPaymentChargeTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertPaymentChargeTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert adjustments adjusting GL transactions
  /// for the <see cref="T:PX.Objects.AR.ARAdjust" /> entity inside the
  /// <see cref="M:PX.Objects.AR.ARReleaseProcess.ProcessAdjustmentAdjusting(PX.Objects.GL.JournalEntry,PX.Objects.AR.ARAdjust,PX.Objects.AR.ARPayment,PX.Objects.AR.ARRegister,PX.Objects.AR.Customer,PX.Objects.CM.Extensions.CurrencyInfo)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARAdjustRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertAdjustmentsAdjustingTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert adjustments adjusted GL transactions
  /// for the <see cref="T:PX.Objects.AR.ARAdjust" /> entity inside the
  /// <see cref="M:PX.Objects.AR.ARReleaseProcess.ProcessAdjustmentAdjusted(PX.Objects.GL.JournalEntry,PX.Objects.AR.ARAdjust,PX.Objects.AR.ARRegister,PX.Objects.AR.ARPayment,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.CM.Extensions.CurrencyInfo)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARAdjustRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertAdjustmentsAdjustedTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert adjustments GOL GL transactions
  /// for the <see cref="T:PX.Objects.AR.ARAdjust" /> entity inside the
  /// <see cref="!:ProcessAdjustmentGOL(JournalEntry, ARAdjust, ARPayment, Customer, ARRegister, CM.Currency, CM.Currency, CurrencyInfo, CurrencyInfo)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARAdjustRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertAdjustmentsGOLTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert adjustments cash discount GL transactions
  /// for the <see cref="T:PX.Objects.AR.ARAdjust" /> entity inside the
  /// <see cref="M:PX.Objects.AR.ARReleaseProcess.ProcessAdjustmentCashDiscount(PX.Objects.GL.JournalEntry,PX.Objects.AR.ARAdjust,PX.Objects.AR.ARPayment,PX.Objects.AR.Customer,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.CM.Extensions.CurrencyInfo)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARAdjustRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertAdjustmentsCashDiscountTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert adjustments write off GL transactions
  /// for the <see cref="T:PX.Objects.AR.ARAdjust" /> entity inside the
  /// <see cref="M:PX.Objects.AR.ARReleaseProcess.ProcessAdjustmentWriteOff(PX.Objects.GL.JournalEntry,PX.Objects.AR.ARAdjust,PX.Objects.AR.ARPayment,PX.Objects.AR.Customer,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.CM.Extensions.CurrencyInfo)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARAdjustRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertAdjustmentsWriteOffTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert adjustments rounding GL transactions
  /// for the <see cref="T:PX.Objects.AR.ARAdjust" /> entity inside the
  /// <see cref="!:ProcessAdjustmentsRounding(JournalEntry, ARRegister, ARAdjust, ARPayment, Customer, CM.Currency, CurrencyInfo, CurrencyInfo, decimal?, bool?)" /> method.
  /// <see cref="T:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARRegisterRecord" />,
  /// <see cref="P:PX.Objects.AR.ARReleaseProcess.GLTranInsertionContext.ARAdjustRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertAdjustmentsRoundingTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    ARReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  public virtual void VerifyInterBranchTransactions(ARRegister doc)
  {
    if (this.IsMigrationMode.GetValueOrDefault() || PXAccess.FeatureInstalled<FeaturesSet.interBranch>())
      return;
    PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) doc.BranchID
    }));
    if (((IQueryable<PXResult<ARAdjust>>) PXSelectBase<ARAdjust, PXSelectJoin<ARAdjust, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARAdjust.adjdBranchID>>>, Where<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<ARAdjust.adjgBranchID, NotEqual<ARAdjust.adjdBranchID>, And<PX.Objects.GL.Branch.organizationID, NotEqual<Required<PX.Objects.GL.Branch.organizationID>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
    {
      (object) doc.DocType,
      (object) doc.RefNbr,
      (object) branch.OrganizationID
    })).Any<PXResult<ARAdjust>>())
      throw new PXException("The application cannot be released, because the documents are related to different branches and the Inter-Branch Transactions feature is disabled.");
    if (((IQueryable<PXResult<ARAdjust>>) PXSelectBase<ARAdjust, PXSelectJoin<ARAdjust, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARAdjust.adjgBranchID>>>, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.adjgBranchID, NotEqual<ARAdjust.adjdBranchID>, And<PX.Objects.GL.Branch.organizationID, NotEqual<Required<PX.Objects.GL.Branch.organizationID>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
    {
      (object) doc.DocType,
      (object) doc.RefNbr,
      (object) branch.OrganizationID
    })).Any<PXResult<ARAdjust>>())
      throw new PXException("The application cannot be released, because the documents are related to different branches and the Inter-Branch Transactions feature is disabled.");
  }

  protected virtual void RaiseInvoiceEvent(ARRegister doc, SelectedEntityEvent<ARInvoice> invEvent)
  {
    if (!(doc is ARInvoice))
      return;
    ((PXSelectBase) this.ARDocument).Cache.Remove((object) doc);
    invEvent.FireOn((PXGraph) this, (ARInvoice) doc);
    ((PXSelectBase) this.ARDocument).Cache.Update((object) doc);
    ((PXSelectBase) this.ARDocument).Cache.RestoreCopy((object) doc, ((PXSelectBase) this.ARDocument).Cache.Locate((object) doc));
  }

  protected virtual void RaisePaymentEvent(ARRegister doc, SelectedEntityEvent<ARPayment> pntEvent)
  {
    if (!(doc is ARPayment))
      return;
    ((PXSelectBase) this.ARDocument).Cache.Remove((object) doc);
    pntEvent.FireOn((PXGraph) this, (ARPayment) doc);
    ((PXSelectBase) this.ARDocument).Cache.Update((object) doc);
    ((PXSelectBase) this.ARDocument).Cache.RestoreCopy((object) doc, ((PXSelectBase) this.ARDocument).Cache.Locate((object) doc));
  }

  protected virtual void RaiseReleaseEvent(ARRegister doc)
  {
    if (((PXSelectBase) this.ARDocument).Cache.ObjectsEqual((object) doc, (object) ((PXSelectBase<ARInvoice>) this.ARInvoice_DocType_RefNbr).Current))
    {
      this.StoreOriginalAsNotReleased((ARRegister) ((PXSelectBase<ARInvoice>) this.ARInvoice_DocType_RefNbr).Current);
      ARInvoice copy = PXCache<ARInvoice>.CreateCopy(((PXSelectBase<ARInvoice>) this.ARInvoice_DocType_RefNbr).Current);
      ((PXSelectBase) this.ARDocument).Cache.RestoreCopy((object) copy, (object) doc);
      ((PXSelectBase) this.ARDocument).Cache.Remove((object) doc);
      ((SelectedEntityEvent<ARInvoice>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events>>>) (e => e.ReleaseDocument))).FireOn((PXGraph) this, copy);
      if (((PXSelectBase) this.ARDocument).Cache.GetStatus((object) copy) != 1)
        ((PXSelectBase) this.ARDocument).Cache.SetStatus((object) copy, (PXEntryStatus) 1);
      ((PXSelectBase) this.ARDocument).Cache.RestoreCopy((object) doc, ((PXSelectBase) this.ARDocument).Cache.Locate((object) doc));
    }
    else
    {
      if (!((PXSelectBase) this.ARDocument).Cache.ObjectsEqual((object) doc, (object) ((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Current))
        return;
      this.StoreOriginalAsNotReleased((ARRegister) ((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Current);
      ARPayment copy = PXCache<ARPayment>.CreateCopy(((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Current);
      ((PXSelectBase) this.ARDocument).Cache.RestoreCopy((object) copy, (object) doc);
      ((PXSelectBase) this.ARDocument).Cache.Remove((object) doc);
      ((SelectedEntityEvent<ARPayment>) PXEntityEventBase<ARPayment>.Container<ARPayment.Events>.Select((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment.Events>>>) (e => e.ReleaseDocument))).FireOn((PXGraph) this, copy);
      if (((PXSelectBase) this.ARDocument).Cache.GetStatus((object) copy) != 1)
        ((PXSelectBase) this.ARDocument).Cache.SetStatus((object) copy, (PXEntryStatus) 1);
      ((PXSelectBase) this.ARDocument).Cache.RestoreCopy((object) doc, ((PXSelectBase) this.ARDocument).Cache.Locate((object) doc));
    }
  }

  private void StoreOriginalAsNotReleased(ARRegister doc)
  {
    ARRegister copy = PXCache<ARRegister>.CreateCopy(doc);
    copy.Released = new bool?(false);
    PXCache<ARRegister>.StoreOriginal((PXGraph) this, copy);
  }

  public class MultiCurrency : ARMultiCurrencyGraph<ARReleaseProcess, ARRegister>
  {
    protected override string DocumentStatus
    {
      get => ((PXSelectBase<ARRegister>) this.Base.ARDocument).Current?.Status;
    }

    protected override CurySource CurrentSourceSelect() => (CurySource) null;

    protected override MultiCurrencyGraph<ARReleaseProcess, ARRegister>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<ARReleaseProcess, ARRegister>.DocumentMapping(typeof (ARRegister))
      {
        DocumentDate = typeof (ARRegister.docDate),
        BAccountID = typeof (ARRegister.customerID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[5]
      {
        (PXSelectBase) this.Base.ARTaxTran_TranType_RefNbr,
        (PXSelectBase) this.Base.ARInvoice_DocType_RefNbr,
        (PXSelectBase) this.Base.ARTran_TranType_RefNbr,
        (PXSelectBase) this.Base.ARDoc_SalesPerTrans,
        (PXSelectBase) this.Base.ARDocument
      };
    }

    protected override IEnumerable<System.Type> FieldWhichShouldBeRecalculatedAnyway
    {
      get
      {
        yield return typeof (ARInvoice.curyDiscBal);
      }
    }

    protected override void CuryRowInserting(
      PXCache sender,
      PXRowInsertingEventArgs e,
      List<CuryField> fields,
      Dictionary<System.Type, string> topCuryInfoIDs)
    {
    }

    public void UpdateCurrencyInfoForPrepayment(
      ARPayment prepayment,
      PX.Objects.CM.Extensions.CurrencyInfo origCuryInfoToUse)
    {
      this.TrackedItems[((PXSelectBase) this.Base.ARPayment_DocType_RefNbr).Cache.GetItemType()].Single<CuryField>((Func<CuryField, bool>) (f => f.CuryName.Equals("curyDocBal", StringComparison.OrdinalIgnoreCase))).BaseCalc = true;
      PX.Objects.CM.Extensions.CurrencyInfo copy = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(origCuryInfoToUse);
      copy.CuryInfoID = prepayment.CuryInfoID;
      copy.IsReadOnly = new bool?(false);
      ((PXSelectBase) this.currencyinfo).Cache.Update((object) copy);
    }
  }

  public struct ARTranPostKey(string docType, string refNbr, int lineNbr)
  {
    public string DocType = docType;
    public string RefNbr = refNbr;
    public int LineNbr = lineNbr;

    public override int GetHashCode()
    {
      return Tuple.Create<string, string, int>(this.DocType, this.RefNbr, this.LineNbr).GetHashCode();
    }

    public override bool Equals(object obj)
    {
      ARReleaseProcess.ARTranPostKey arTranPostKey = (ARReleaseProcess.ARTranPostKey) obj;
      return this.DocType == arTranPostKey.DocType && this.RefNbr == arTranPostKey.RefNbr && this.LineNbr == arTranPostKey.LineNbr;
    }
  }

  private class ARHistItemDiscountsBucket : ARReleaseProcess.ARHistBucket
  {
    public ARHistItemDiscountsBucket(ARTran tran)
    {
      switch (tran.TranType)
      {
        case "INV":
        case "DRM":
        case "CSL":
          this.SignPtdItemDiscounts = 1M;
          break;
        case "CRM":
        case "RCS":
          this.SignPtdItemDiscounts = -1M;
          break;
      }
    }
  }

  private class ARHistBucket
  {
    public int? arAccountID;
    public int? arSubID;
    public Decimal SignPayments;
    public Decimal SignDeposits;
    public Decimal SignSales;
    public Decimal SignFinCharges;
    public Decimal SignCrMemos;
    public Decimal SignDrMemos;
    public Decimal SignDiscTaken;
    public Decimal SignRGOL;
    public Decimal SignPtd;
    public Decimal SignPtdItemDiscounts;
    public Decimal SignRetainageWithheld;
    public Decimal SignRetainageReleased;

    public ARHistBucket(PX.Objects.GL.GLTran tran, string TranType)
    {
      this.arAccountID = tran.AccountID;
      this.arSubID = tran.SubID;
      string s = TranType + tran.TranClass;
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(s))
      {
        case 24996121:
          if (!(s == "SMBN"))
            return;
          this.SignCrMemos = -1M;
          this.SignPtd = 1M;
          return;
        case 71211040:
          if (!(s == "REFB"))
            return;
          goto label_161;
        case 125661835:
          if (!(s == "SMBD"))
            return;
          this.arAccountID = tran.OrigAccountID;
          this.arSubID = tran.OrigSubID;
          return;
        case 171876754:
          if (!(s == "REFD"))
            return;
          goto label_156;
        case 226327549:
          if (!(s == "SMBB"))
            return;
          goto label_161;
        case 272542468:
          if (!(s == "REFN"))
            return;
          goto label_153;
        case 339652944:
          if (!(s == "REFR"))
            return;
          goto label_155;
        case 373208182:
          if (!(s == "REFP"))
            return;
          goto label_153;
        case 457096277:
          if (!(s == "REFU"))
            return;
          goto label_154;
        case 461214215:
          int num1 = s == "SMBP" ? 1 : 0;
          return;
        case 494769453:
          if (!(s == "SMBR"))
            return;
          this.arAccountID = tran.OrigAccountID;
          this.arSubID = tran.OrigSubID;
          this.SignCrMemos = -1M;
          this.SignRGOL = 1M;
          return;
        case 524206753:
          if (!(s == "REFY"))
            return;
          goto label_163;
        case 657665241:
          if (!(s == "CSLN"))
            return;
          this.SignSales = -1M;
          this.SignPayments = -1M;
          this.SignPtd = 0M;
          return;
        case 736032930:
          if (!(s == "INVN"))
            return;
          this.SignSales = 1M;
          this.SignPtd = 1M;
          return;
        case 758330955:
          if (!(s == "CSLD"))
            return;
          goto label_156;
        case 853476263:
          if (!(s == "INVE"))
            return;
          this.arAccountID = tran.OrigAccountID;
          this.arSubID = tran.OrigSubID;
          this.SignRetainageWithheld = 1M;
          return;
        case 858996669:
          if (!(s == "CSLB"))
            return;
          goto label_161;
        case 860648500:
          if (!(s == "FCHN"))
            return;
          this.SignFinCharges = 1M;
          this.SignPtd = 1M;
          return;
        case 870253882:
          if (!(s == "INVF"))
            return;
          this.SignSales = 1M;
          this.SignRetainageReleased = 1M;
          this.SignPtd = 1M;
          return;
        case 1069499225:
          if (!(s == "PMTU"))
            return;
          this.arAccountID = tran.OrigAccountID;
          this.arSubID = tran.OrigSubID;
          this.SignPayments = -1M;
          this.SignDrMemos = -1M;
          return;
        case 1071655590:
          if (!(s == "PPMB"))
            return;
          goto label_161;
        case 1086276844:
          if (!(s == "PMTR"))
            return;
          goto label_155;
        case 1105210828:
          if (!(s == "PPMD"))
            return;
          goto label_156;
        case 1119832082:
          if (!(s == "PMTP"))
            return;
          goto label_153;
        case 1127438573:
          if (!(s == "CSLR"))
            return;
          goto label_155;
        case 1220497796:
          int num2 = s == "PMTZ" ? 1 : 0;
          return;
        case 1270830653:
          if (!(s == "PMTY"))
            return;
          goto label_163;
        case 1272987018:
          if (!(s == "PPMN"))
            return;
          goto label_153;
        case 1273678566:
          if (!(s == "PPIN"))
            return;
          this.SignPayments = -1M;
          this.SignPtd = 1M;
          return;
        case 1306542256:
          if (!(s == "PPMP"))
            return;
          this.SignDeposits = -1M;
          return;
        case 1321163510:
          if (!(s == "PMTD"))
            return;
          goto label_156;
        case 1340097494:
          if (!(s == "PPMR"))
            return;
          goto label_155;
        case 1354718748:
          if (!(s == "PMTB"))
            return;
          goto label_161;
        case 1374344280:
          if (!(s == "PPID"))
            return;
          goto label_156;
        case 1390430351:
          if (!(s == "PPMU"))
            return;
          this.arAccountID = tran.OrigAccountID;
          this.arSubID = tran.OrigSubID;
          this.SignDeposits = -1M;
          this.SignDrMemos = -1M;
          this.SignPtd = -1M;
          return;
        case 1404850195:
          if (!(s == "RCSB"))
            return;
          goto label_161;
        case 1421829224:
          if (!(s == "PMTN"))
            return;
          goto label_153;
        case 1457540827:
          if (!(s == "PPMY"))
            return;
          goto label_163;
        case 1474318446:
          int num3 = s == "PPMZ" ? 1 : 0;
          return;
        case 1505515909:
          if (!(s == "RCSD"))
            return;
          goto label_156;
        case 1592453327:
          if (!(s == "PPIY"))
            return;
          goto label_163;
        case 1606181623:
          if (!(s == "RCSN"))
            return;
          this.SignSales = -1M;
          this.SignPayments = -1M;
          this.SignPtd = 0M;
          return;
        case 1673292099:
          if (!(s == "RCSR"))
            return;
          goto label_155;
        case 1709896660:
          if (!(s == "PPIP"))
            return;
          goto label_163;
        case 1743451898:
          if (!(s == "PPIR"))
            return;
          goto label_155;
        case 1847964908:
          if (!(s == "RPMR"))
            return;
          goto label_155;
        case 1881520146:
          if (!(s == "RPMP"))
            return;
          goto label_153;
        case 1961706867:
          if (!(s == "VRFD"))
            return;
          goto label_156;
        case 2062372581:
          if (!(s == "VRFB"))
            return;
          goto label_161;
        case 2082851574:
          if (!(s == "RPMD"))
            return;
          goto label_156;
        case 2116406812:
          if (!(s == "RPMB"))
            return;
          goto label_161;
        case 2129483057:
          if (!(s == "VRFN"))
            return;
          goto label_153;
        case 2183517288:
          if (!(s == "RPMN"))
            return;
          goto label_153;
        case 2213371152:
          if (!(s == "VRFU"))
            return;
          goto label_154;
        case 2253266399:
          if (!(s == "CRMR"))
            return;
          this.arAccountID = tran.OrigAccountID;
          this.arSubID = tran.OrigSubID;
          this.SignCrMemos = -1M;
          this.SignRGOL = 1M;
          return;
        case 2286821637:
          if (!(s == "CRMP"))
            return;
          break;
        case 2297259247:
          if (!(s == "VRFP"))
            return;
          goto label_153;
        case 2330814485:
          if (!(s == "VRFR"))
            return;
          goto label_155;
        case 2404264970:
          if (!(s == "CRMY"))
            return;
          goto label_163;
        case 2454597827:
          if (!(s == "CRMF"))
            return;
          this.SignCrMemos = -1M;
          this.SignRetainageReleased = 1M;
          this.SignPtd = 1M;
          return;
        case 2471375446:
          if (!(s == "CRME"))
            return;
          this.arAccountID = tran.OrigAccountID;
          this.arSubID = tran.OrigSubID;
          this.SignRetainageWithheld = 1M;
          return;
        case 2488153065:
          if (!(s == "CRMD"))
            return;
          this.arAccountID = tran.OrigAccountID;
          this.arSubID = tran.OrigSubID;
          this.SignCrMemos = -1M;
          this.SignDiscTaken = 1M;
          return;
        case 2521708303:
          if (!(s == "CRMB"))
            return;
          this.arAccountID = tran.OrigAccountID;
          this.arSubID = tran.OrigSubID;
          this.SignCrMemos = 0M;
          return;
        case 2588818779:
          if (!(s == "CRMN"))
            return;
          break;
        case 2831576988:
          if (!(s == "DRMN"))
            return;
          this.SignDrMemos = 1M;
          this.SignPtd = 1M;
          return;
        case 2965797940:
          if (!(s == "DRMF"))
            return;
          this.SignDrMemos = 1M;
          this.SignRetainageReleased = 1M;
          this.SignPtd = 1M;
          return;
        case 3016130797:
          if (!(s == "DRME"))
            return;
          this.arAccountID = tran.OrigAccountID;
          this.arSubID = tran.OrigSubID;
          this.SignRetainageWithheld = 1M;
          return;
        case 3581601086:
          int num4 = s == "SMCP" ? 1 : 0;
          return;
        case 3749377276:
          if (!(s == "SMCN"))
            return;
          this.SignDrMemos = 1M;
          this.SignPtd = 1M;
          return;
        default:
          return;
      }
      this.SignCrMemos = -1M;
      this.SignPtd = 1M;
      return;
label_153:
      this.SignPayments = -1M;
      this.SignPtd = 1M;
      return;
label_154:
      this.SignDeposits = -1M;
      return;
label_155:
      this.arAccountID = tran.OrigAccountID;
      this.arSubID = tran.OrigSubID;
      this.SignPayments = -1M;
      this.SignRGOL = 1M;
      return;
label_156:
      this.arAccountID = tran.OrigAccountID;
      this.arSubID = tran.OrigSubID;
      this.SignPayments = -1M;
      this.SignDiscTaken = 1M;
      return;
label_161:
      this.arAccountID = tran.OrigAccountID;
      this.arSubID = tran.OrigSubID;
      this.SignPayments = -1M;
      this.SignCrMemos = 1M;
      return;
label_163:
      this.SignDeposits = -1M;
    }

    public ARHistBucket()
    {
    }
  }

  public class Amount : Tuple<Decimal?, Decimal?>
  {
    public Decimal? Cury => this.Item1;

    public Decimal? Base => this.Item2;

    public Amount()
      : base(new Decimal?(0M), new Decimal?(0M))
    {
    }

    public Amount(Decimal? cury, Decimal? baaase)
      : base(cury, baaase)
    {
    }

    public static ARReleaseProcess.Amount operator +(
      ARReleaseProcess.Amount a,
      ARReleaseProcess.Amount b)
    {
      Decimal? cury1 = a.Cury;
      Decimal? cury2 = b.Cury;
      Decimal? cury3 = cury1.HasValue & cury2.HasValue ? new Decimal?(cury1.GetValueOrDefault() + cury2.GetValueOrDefault()) : new Decimal?();
      cury2 = a.Base;
      Decimal? nullable = b.Base;
      Decimal? baaase = cury2.HasValue & nullable.HasValue ? new Decimal?(cury2.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
      return new ARReleaseProcess.Amount(cury3, baaase);
    }

    public static ARReleaseProcess.Amount operator -(
      ARReleaseProcess.Amount a,
      ARReleaseProcess.Amount b)
    {
      Decimal? cury1 = a.Cury;
      Decimal? cury2 = b.Cury;
      Decimal? cury3 = cury1.HasValue & cury2.HasValue ? new Decimal?(cury1.GetValueOrDefault() - cury2.GetValueOrDefault()) : new Decimal?();
      cury2 = a.Base;
      Decimal? nullable = b.Base;
      Decimal? baaase = cury2.HasValue & nullable.HasValue ? new Decimal?(cury2.GetValueOrDefault() - nullable.GetValueOrDefault()) : new Decimal?();
      return new ARReleaseProcess.Amount(cury3, baaase);
    }

    public static ARReleaseProcess.Amount operator *(ARReleaseProcess.Amount a, Decimal? mult)
    {
      Decimal? cury1 = a.Cury;
      Decimal? nullable1 = mult;
      Decimal? cury2 = cury1.HasValue & nullable1.HasValue ? new Decimal?(cury1.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
      nullable1 = a.Base;
      Decimal? nullable2 = mult;
      Decimal? baaase = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
      return new ARReleaseProcess.Amount(cury2, baaase);
    }

    public static ARReleaseProcess.Amount operator /(ARReleaseProcess.Amount a, Decimal? mult)
    {
      Decimal? cury1 = a.Cury;
      Decimal? nullable1 = mult;
      Decimal? cury2 = cury1.HasValue & nullable1.HasValue ? new Decimal?(cury1.GetValueOrDefault() / nullable1.GetValueOrDefault()) : new Decimal?();
      nullable1 = a.Base;
      Decimal? nullable2 = mult;
      Decimal? baaase = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?();
      return new ARReleaseProcess.Amount(cury2, baaase);
    }

    public static ARReleaseProcess.Amount operator *(ARReleaseProcess.Amount a, Decimal mult)
    {
      Decimal? cury1 = a.Cury;
      Decimal num1 = mult;
      Decimal? cury2 = cury1.HasValue ? new Decimal?(cury1.GetValueOrDefault() * num1) : new Decimal?();
      Decimal? nullable = a.Base;
      Decimal num2 = mult;
      Decimal? baaase = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num2) : new Decimal?();
      return new ARReleaseProcess.Amount(cury2, baaase);
    }

    public static ARReleaseProcess.Amount operator /(ARReleaseProcess.Amount a, Decimal mult)
    {
      Decimal? cury1 = a.Cury;
      Decimal num1 = mult;
      Decimal? cury2 = cury1.HasValue ? new Decimal?(cury1.GetValueOrDefault() / num1) : new Decimal?();
      Decimal? nullable = a.Base;
      Decimal num2 = mult;
      Decimal? baaase = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() / num2) : new Decimal?();
      return new ARReleaseProcess.Amount(cury2, baaase);
    }

    public static bool operator <(ARReleaseProcess.Amount a, ARReleaseProcess.Amount b)
    {
      Decimal? cury1 = a.Cury;
      Decimal? cury2 = b.Cury;
      return cury1.GetValueOrDefault() < cury2.GetValueOrDefault() & cury1.HasValue & cury2.HasValue;
    }

    public static bool operator >(ARReleaseProcess.Amount a, ARReleaseProcess.Amount b)
    {
      Decimal? cury1 = a.Cury;
      Decimal? cury2 = b.Cury;
      return cury1.GetValueOrDefault() > cury2.GetValueOrDefault() & cury1.HasValue & cury2.HasValue;
    }

    public override bool Equals(object obj) => this.Equals(obj as ARReleaseProcess.Amount);

    public bool Equals(ARReleaseProcess.Amount a)
    {
      Decimal? cury1 = (Decimal?) a?.Cury;
      Decimal? cury2 = this.Cury;
      if (!(cury1.GetValueOrDefault() == cury2.GetValueOrDefault() & cury1.HasValue == cury2.HasValue))
        return false;
      cury2 = a.Base;
      cury1 = this.Base;
      return cury2.GetValueOrDefault() == cury1.GetValueOrDefault() & cury2.HasValue == cury1.HasValue;
    }

    public override int GetHashCode()
    {
      Decimal? cury = this.Cury;
      ref Decimal? local = ref cury;
      return !local.HasValue ? 0 : local.GetValueOrDefault().GetHashCode();
    }
  }

  public class GLTranInsertionContext
  {
    public virtual ARRegister ARRegisterRecord { get; set; }

    public virtual ARTran ARTranRecord { get; set; }

    public virtual ARTaxTran ARTaxTranRecord { get; set; }

    public virtual ARPaymentChargeTran ARPaymentChargeTranRecord { get; set; }

    public virtual ARAdjust ARAdjustRecord { get; set; }

    public virtual CADeposit CADepositRecord { get; set; }

    public virtual CADepositDetail CADepositDetailRecord { get; set; }

    public virtual INTran INTranRecord { get; set; }

    public virtual INTranCost INTranCostRecord { get; set; }

    public virtual PMTran PMTranRecord { get; set; }
  }
}
