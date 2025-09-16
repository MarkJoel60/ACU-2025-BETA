// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APReleaseProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP.BQL;
using PX.Objects.AP.Overrides.APDocumentRelease;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.EntityInUse;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.AP;
using PX.Objects.GL;
using PX.Objects.GL.DAC.Abstract;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.Services;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.PO.GraphExtensions.APReleaseProcessExt;
using PX.Objects.TX;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.AP;

[PXHidden]
public class APReleaseProcess : PXGraph<
#nullable disable
APReleaseProcess>
{
  public PXSelect<APRegister> APDocument;
  public PXSelectJoin<APTran, LeftJoin<APTax, On<APTax.tranType, Equal<APTran.tranType>, And<APTax.refNbr, Equal<APTran.refNbr>, And<APTax.lineNbr, Equal<APTran.lineNbr>>>>, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTax.taxID>>, LeftJoin<DRDeferredCode, On<DRDeferredCode.deferredCodeID, Equal<APTran.deferredCode>>, LeftJoin<LandedCostCode, On<LandedCostCode.landedCostCodeID, Equal<APTran.landedCostCodeID>>, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<APTran.inventoryID>>, LeftJoin<APTaxTran, On<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<APTax.tranType>, And<APTaxTran.refNbr, Equal<APTax.refNbr>, And<APTaxTran.taxID, Equal<APTax.taxID>>>>>>>>>>>, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>>>, PX.Data.OrderBy<Asc<APTran.lineNbr, Asc<PX.Objects.TX.Tax.taxCalcLevel, Asc<PX.Objects.TX.Tax.taxType>>>>> APTran_TranType_RefNbr;
  public PXSelectJoin<APTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>, LeftJoin<APInvoice, On<APInvoice.docType, Equal<APTaxTran.origTranType>, And<APInvoice.refNbr, Equal<APTaxTran.origRefNbr>>>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Required<APTaxTran.tranType>>, And<APTaxTran.refNbr, Equal<Required<APTaxTran.refNbr>>>>>, PX.Data.OrderBy<Asc<PX.Objects.TX.Tax.taxCalcLevel>>> APTaxTran_TranType_RefNbr;
  public PXSelect<SVATConversionHist> SVATConversionHistory;
  public FbqlSelect<SelectFromBase<SVATConversionHist, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  SVATConversionHist.module, 
  #nullable disable
  Equal<BatchModule.moduleAP>>>>, PX.Data.And<BqlOperand<
  #nullable enable
  SVATConversionHist.adjdDocType, IBqlString>.IsEqual<
  #nullable disable
  P.AsString.ASCII>>>, PX.Data.And<BqlOperand<
  #nullable enable
  SVATConversionHist.adjdRefNbr, IBqlString>.IsEqual<
  #nullable disable
  P.AsString>>>, PX.Data.And<BqlOperand<
  #nullable enable
  SVATConversionHist.taxID, IBqlString>.IsEqual<
  #nullable disable
  P.AsString>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  SVATConversionHist.adjdDocType, 
  #nullable disable
  NotEqual<SVATConversionHist.adjgDocType>>>>>.Or<BqlOperand<
  #nullable enable
  SVATConversionHist.adjdRefNbr, IBqlString>.IsNotEqual<
  #nullable disable
  SVATConversionHist.adjgRefNbr>>>>>, SVATConversionHist>.View SVATRecognitionRecords;
  public PXSelect<PX.Objects.GL.Batch> Batch;
  public APInvoice_CurrencyInfo_Terms_Vendor APInvoice_DocType_RefNbr;
  public APPayment_CurrencyInfo_Currency_Vendor APPayment_DocType_RefNbr;
  public PXSelectJoin<APAdjust, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APAdjust.adjdCuryInfoID>>, InnerJoin<PX.Objects.CM.Extensions.Currency, On<PX.Objects.CM.Extensions.Currency.curyID, Equal<PX.Objects.CM.Extensions.CurrencyInfo.curyID>>, LeftJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>, LeftJoinSingleTable<APPayment, On<APPayment.docType, Equal<APAdjust.adjdDocType>, And<APPayment.refNbr, Equal<APAdjust.adjdRefNbr>>>, InnerJoin<APRegisterAlias, On<APRegisterAlias.docType, Equal<APAdjust.adjdDocType>, And<APRegisterAlias.refNbr, Equal<APAdjust.adjdRefNbr>>>, LeftJoin<APTran, On<APRegisterAlias.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APAdjust.adjdDocType>, And<APTran.refNbr, Equal<APAdjust.adjdRefNbr>, And<APTran.lineNbr, Equal<APAdjust.adjdLineNbr>>>>>, LeftJoin<CurrencyInfo2, On<CurrencyInfo2.curyInfoID, Equal<APRegisterAlias.curyInfoID>>>>>>>>>, Where<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>, And2<Where<Switch<Case<Where<Required<APAdjust.released>, Equal<True>>, IIf<Where<APAdjust.adjNbr, Equal<Required<APAdjust.adjNbr>>>, True, False>>, IIf<Where<APAdjust.released, NotEqual<True>>, True, False>>, Equal<True>>, PX.Data.And<Where<APAdjust.adjgDocType, Equal<APAdjust.adjdDocType>, And<APAdjust.adjgRefNbr, Equal<APAdjust.adjdRefNbr>, Or<APRegisterAlias.released, Equal<True>, Or<APRegisterAlias.prebooked, Equal<True>>>>>>>>>> APAdjust_AdjgDocType_RefNbr_VendorID;
  public PXSelect<APPaymentChargeTran, Where<APPaymentChargeTran.docType, Equal<Required<APPaymentChargeTran.docType>>, And<APPaymentChargeTran.refNbr, Equal<Required<APPaymentChargeTran.refNbr>>>>> APPaymentChargeTran_DocType_RefNbr;
  public PXSelectReadonly<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>, And<APTran.box1099, PX.Data.IsNotNull>>>> AP1099Tran_Select;
  public PXSelect<AP1099Hist> AP1099History_Select;
  public PXSelect<AP1099Yr> AP1099Year_Select;
  public PXSelectJoin<APTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Required<APTaxTran.tranType>>, And<APTaxTran.refNbr, Equal<Required<APTaxTran.refNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>>> WHTax_TranType_RefNbr;
  public PXSelect<APTranPost, Where<APTranPost.docType, Equal<Required<APRegister.docType>>, And<APTranPost.refNbr, Equal<Required<APRegister.refNbr>>>>> TranPost;
  public PXSelect<CATran> CashTran;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  public PXSetup<GLSetup> glsetup;
  public PXSelect<PX.Objects.TX.Tax> taxes;
  private APSetup _apsetup;
  private POSetup _posetup;
  protected APInvoiceEntry _ie;
  protected bool _IsIntegrityCheck;
  protected string _IntegrityCheckStartingPeriod;

  public APSetup apsetup
  {
    get
    {
      return this._apsetup ?? (this._apsetup = (APSetup) PXSetup<APSetup>.Select((PXGraph) this, Array.Empty<object>()));
    }
  }

  public POSetup posetup
  {
    get
    {
      return this._posetup ?? (this._posetup = (POSetup) PXSetup<POSetup>.Select((PXGraph) this, Array.Empty<object>()));
    }
  }

  public bool AutoPost => this.apsetup.AutoPost.GetValueOrDefault();

  public bool SummPost => this.apsetup.TransactionPosting == "S";

  public string InvoiceRounding => this.apsetup.InvoiceRounding;

  public Decimal? InvoicePrecision => this.apsetup.InvoicePrecision;

  public bool? IsMigrationMode => this.apsetup.MigrationMode;

  public bool IsMigratedDocumentForProcessing(APRegister doc)
  {
    bool flag = doc.DocType == "QCK" || doc.DocType == "VQC" || doc.DocType == "RQC";
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

  public bool? RequireControlTaxTotal
  {
    get
    {
      return new bool?(this.apsetup.RequireControlTaxTotal.GetValueOrDefault() && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>());
    }
  }

  public APInvoiceEntry InvoiceEntryGraph
  {
    get => this._ie ?? (this._ie = PXGraph.CreateInstance<APInvoiceEntry>());
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  /// <summary>
  /// The formula that calculates <see cref="P:PX.Objects.AP.APPayment.CuryApplAmt" /> needs to be removed
  /// to prevent premature updates of the documents by the <see cref="T:PX.Data.PXUnboundFormulaAttribute" />
  /// upon applications' delete, thus avoiding the lock violation exceptions. This does no harm
  /// as the application amounts are not visible in the context of release process, the <see cref="P:PX.Objects.AP.APPayment.CuryApplAmt" /> is neither DB-bound or visible during release.
  /// </summary>
  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRemoveBaseAttribute(typeof (PXUnboundFormulaAttribute))]
  protected virtual void APAdjust_CuryAdjgAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(1, IsFixed = true)]
  public virtual void Tax_TaxType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(1, IsFixed = true)]
  public virtual void Tax_TaxCalcLevel_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [PXDBInt]
  protected virtual void APTranPost_AccountID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [PXDBInt]
  protected virtual void APTranPost_SubID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [PXDBInt]
  protected virtual void APTranPost_VendorID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [PXDBInt]
  protected virtual void APTranPost_BranchID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXParent(typeof (POOrderPrepayment.FK.Order))]
  protected virtual void _(PX.Data.Events.CacheAttached<POOrderPrepayment.orderNbr> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (PX.Objects.PO.POOrder.curyInfoID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POOrderPrepayment.curyInfoID> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRemoveBaseAttribute(typeof (OwnerAttribute))]
  protected virtual void APPayment_EmployeeID_CacheAttached(PXCache sender)
  {
  }

  public APReleaseProcess()
  {
    this.Defaults.Remove(typeof (APSetup));
    this.Defaults.Remove(typeof (POSetup));
    OpenPeriodAttribute.SetValidatePeriod<APRegister.finPeriodID>(this.APDocument.Cache, (object) null, PeriodValidation.Nothing);
    OpenPeriodAttribute.SetValidatePeriod<APPayment.adjFinPeriodID>(this.APPayment_DocType_RefNbr.Cache, (object) null, PeriodValidation.Nothing);
    PXCache cach1 = this.Caches[typeof (APAdjust)];
    PXCacheEx.AttributeAdjuster<FinPeriodIDAttribute> attributeAdjuster = cach1.Adjust<FinPeriodIDAttribute>();
    attributeAdjuster.For<APAdjust.adjgFinPeriodID>((System.Action<FinPeriodIDAttribute>) (attr =>
    {
      attr.AutoCalculateMasterPeriod = false;
      attr.CalculatePeriodByHeader = false;
      attr.HeaderFindingMode = FinPeriodIDAttribute.HeaderFindingModes.Parent;
    })).SameFor<APAdjust.adjdFinPeriodID>();
    PXDBDefaultAttribute.SetDefaultForUpdate<APAdjust.vendorID>(cach1, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<APAdjust.adjgDocType>(cach1, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<APAdjust.adjgRefNbr>(cach1, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<APAdjust.adjgCuryInfoID>(cach1, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<APAdjust.adjgDocDate>(cach1, (object) null, false);
    PXCache cach2 = this.Caches[typeof (APTran)];
    attributeAdjuster = cach2.Adjust<FinPeriodIDAttribute>();
    attributeAdjuster.For<APTran.finPeriodID>((System.Action<FinPeriodIDAttribute>) (attr => attr.HeaderFindingMode = FinPeriodIDAttribute.HeaderFindingModes.Parent));
    PXDBDefaultAttribute.SetDefaultForUpdate<APTran.tranType>(cach2, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<APTran.refNbr>(cach2, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<APTran.curyInfoID>(cach2, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<APTran.tranDate>(cach2, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<APTran.vendorID>(cach2, (object) null, false);
    PXCache cach3 = this.Caches[typeof (APTaxTran)];
    attributeAdjuster = cach3.Adjust<FinPeriodIDAttribute>();
    attributeAdjuster.For<APTaxTran.finPeriodID>((System.Action<FinPeriodIDAttribute>) (attr => attr.HeaderFindingMode = FinPeriodIDAttribute.HeaderFindingModes.Parent));
    PXDBDefaultAttribute.SetDefaultForInsert<APTaxTran.tranType>(cach3, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForInsert<APTaxTran.refNbr>(cach3, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForInsert<APTaxTran.curyInfoID>(cach3, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForInsert<APTaxTran.tranDate>(cach3, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForInsert<APTaxTran.taxZoneID>(cach3, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<APTaxTran.tranType>(cach3, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<APTaxTran.refNbr>(cach3, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<APTaxTran.curyInfoID>(cach3, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<APTaxTran.tranDate>(cach3, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<APTaxTran.taxZoneID>(cach3, (object) null, false);
    if (!this.IsMigrationMode.GetValueOrDefault())
      return;
    PXDBDefaultAttribute.SetDefaultForInsert<APAdjust.vendorID>(cach1, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForInsert<APAdjust.adjgDocDate>(cach1, (object) null, false);
  }

  protected virtual void APPayment_CashAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void APPayment_PaymentMethodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void APPayment_ExtRefNbr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void APPayment_FinPeriodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void APRegister_FinPeriodID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
      return;
    e.ExcludeFromInsertUpdate();
  }

  protected virtual void APRegister_TranPeriodID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
      return;
    e.ExcludeFromInsertUpdate();
  }

  protected virtual void APRegister_DocDate_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
      return;
    e.ExcludeFromInsertUpdate();
  }

  protected virtual void APPayment_FinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void CATran_ReferenceID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void APAdjust_AdjdRefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void APTran_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
  }

  protected void _(PX.Data.Events.RowPersisting<APTranPost> e)
  {
    if (e.Operation != PXDBOperation.Insert || !(e.Row.Type == "R"))
      return;
    Decimal? curyAmt = e.Row.CuryAmt;
    Decimal num1 = 0M;
    if (!(curyAmt.GetValueOrDefault() == num1 & curyAmt.HasValue))
      return;
    Decimal? amt = e.Row.Amt;
    Decimal num2 = 0M;
    if (!(amt.GetValueOrDefault() == num2 & amt.HasValue))
      return;
    Decimal? rgolAmt = e.Row.RGOLAmt;
    Decimal num3 = 0M;
    if (!(rgolAmt.GetValueOrDefault() == num3 & rgolAmt.HasValue))
      return;
    Decimal? whTaxAmt = e.Row.WhTaxAmt;
    Decimal num4 = 0M;
    if (!(whTaxAmt.GetValueOrDefault() == num4 & whTaxAmt.HasValue))
      return;
    Decimal? ppdAmt = e.Row.PPDAmt;
    Decimal num5 = 0M;
    if (!(ppdAmt.GetValueOrDefault() == num5 & ppdAmt.HasValue))
      return;
    Decimal? discAmt = e.Row.DiscAmt;
    Decimal num6 = 0M;
    if (!(discAmt.GetValueOrDefault() == num6 & discAmt.HasValue))
      return;
    e.Cancel = true;
  }

  private APHist CreateHistory(
    int? BranchID,
    int? AccountID,
    int? SubID,
    int? VendorID,
    string PeriodID)
  {
    APHist apHist = new APHist();
    apHist.BranchID = BranchID;
    apHist.AccountID = AccountID;
    apHist.SubID = SubID;
    apHist.VendorID = VendorID;
    apHist.FinPeriodID = PeriodID;
    return (APHist) this.Caches[typeof (APHist)].Insert((object) apHist);
  }

  private CuryAPHist CreateHistory(
    int? BranchID,
    int? AccountID,
    int? SubID,
    int? VendorID,
    string CuryID,
    string PeriodID)
  {
    CuryAPHist curyApHist = new CuryAPHist();
    curyApHist.BranchID = BranchID;
    curyApHist.AccountID = AccountID;
    curyApHist.SubID = SubID;
    curyApHist.VendorID = VendorID;
    curyApHist.CuryID = CuryID;
    curyApHist.FinPeriodID = PeriodID;
    return (CuryAPHist) this.Caches[typeof (CuryAPHist)].Insert((object) curyApHist);
  }

  private void UpdateHist<History>(
    History accthist,
    APReleaseProcess.APHistBucket bucket,
    bool FinFlag,
    PX.Objects.GL.GLTran tran)
    where History : class, IBaseAPHist
  {
    if (this._IsIntegrityCheck || this.IsInvoiceReclassification)
    {
      bool? detDeleted = accthist.DetDeleted;
      bool flag = false;
      if (!(detDeleted.GetValueOrDefault() == flag & detDeleted.HasValue))
        return;
    }
    Decimal? debitAmt = tran.DebitAmt;
    Decimal? nullable1 = tran.CreditAmt;
    Decimal? nullable2 = debitAmt.HasValue & nullable1.HasValue ? new Decimal?(debitAmt.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    accthist.FinFlag = new bool?(FinFlag);
    ref History local1 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local2 = (object) local1;
    nullable1 = local1.PtdPayments;
    Decimal signPayments = bucket.SignPayments;
    Decimal? nullable3 = nullable2;
    Decimal? nullable4 = nullable3.HasValue ? new Decimal?(signPayments * nullable3.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    local2.PtdPayments = nullable5;
    ref History local3 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local4 = (object) local3;
    nullable4 = local3.PtdPurchases;
    Decimal signPurchases = bucket.SignPurchases;
    Decimal? nullable6 = nullable2;
    nullable1 = nullable6.HasValue ? new Decimal?(signPurchases * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable7;
    if (!(nullable4.HasValue & nullable1.HasValue))
    {
      nullable6 = new Decimal?();
      nullable7 = nullable6;
    }
    else
      nullable7 = new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local4.PtdPurchases = nullable7;
    ref History local5 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local6 = (object) local5;
    nullable1 = local5.PtdCrAdjustments;
    Decimal signCrAdjustments = bucket.SignCrAdjustments;
    nullable6 = nullable2;
    nullable4 = nullable6.HasValue ? new Decimal?(signCrAdjustments * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable8;
    if (!(nullable1.HasValue & nullable4.HasValue))
    {
      nullable6 = new Decimal?();
      nullable8 = nullable6;
    }
    else
      nullable8 = new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault());
    local6.PtdCrAdjustments = nullable8;
    ref History local7 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local8 = (object) local7;
    nullable4 = local7.PtdDrAdjustments;
    Decimal signDrAdjustments = bucket.SignDrAdjustments;
    nullable6 = nullable2;
    nullable1 = nullable6.HasValue ? new Decimal?(signDrAdjustments * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable9;
    if (!(nullable4.HasValue & nullable1.HasValue))
    {
      nullable6 = new Decimal?();
      nullable9 = nullable6;
    }
    else
      nullable9 = new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local8.PtdDrAdjustments = nullable9;
    ref History local9 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local10 = (object) local9;
    nullable1 = local9.PtdDiscTaken;
    Decimal signDiscTaken = bucket.SignDiscTaken;
    nullable6 = nullable2;
    nullable4 = nullable6.HasValue ? new Decimal?(signDiscTaken * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable10;
    if (!(nullable1.HasValue & nullable4.HasValue))
    {
      nullable6 = new Decimal?();
      nullable10 = nullable6;
    }
    else
      nullable10 = new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault());
    local10.PtdDiscTaken = nullable10;
    ref History local11 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local12 = (object) local11;
    nullable4 = local11.PtdWhTax;
    Decimal signWhTax = bucket.SignWhTax;
    nullable6 = nullable2;
    nullable1 = nullable6.HasValue ? new Decimal?(signWhTax * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable11;
    if (!(nullable4.HasValue & nullable1.HasValue))
    {
      nullable6 = new Decimal?();
      nullable11 = nullable6;
    }
    else
      nullable11 = new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local12.PtdWhTax = nullable11;
    ref History local13 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local14 = (object) local13;
    nullable1 = local13.PtdRGOL;
    Decimal signRgol = bucket.SignRGOL;
    nullable6 = nullable2;
    nullable4 = nullable6.HasValue ? new Decimal?(signRgol * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable12;
    if (!(nullable1.HasValue & nullable4.HasValue))
    {
      nullable6 = new Decimal?();
      nullable12 = nullable6;
    }
    else
      nullable12 = new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault());
    local14.PtdRGOL = nullable12;
    ref History local15 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local16 = (object) local15;
    nullable4 = local15.YtdBalance;
    Decimal signPtd = bucket.SignPtd;
    nullable6 = nullable2;
    nullable1 = nullable6.HasValue ? new Decimal?(signPtd * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable13;
    if (!(nullable4.HasValue & nullable1.HasValue))
    {
      nullable6 = new Decimal?();
      nullable13 = nullable6;
    }
    else
      nullable13 = new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local16.YtdBalance = nullable13;
    ref History local17 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local18 = (object) local17;
    nullable1 = local17.PtdDeposits;
    Decimal signDeposits1 = bucket.SignDeposits;
    nullable6 = nullable2;
    nullable4 = nullable6.HasValue ? new Decimal?(signDeposits1 * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable14;
    if (!(nullable1.HasValue & nullable4.HasValue))
    {
      nullable6 = new Decimal?();
      nullable14 = nullable6;
    }
    else
      nullable14 = new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault());
    local18.PtdDeposits = nullable14;
    ref History local19 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local20 = (object) local19;
    nullable4 = local19.YtdDeposits;
    Decimal signDeposits2 = bucket.SignDeposits;
    nullable6 = nullable2;
    nullable1 = nullable6.HasValue ? new Decimal?(signDeposits2 * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable15;
    if (!(nullable4.HasValue & nullable1.HasValue))
    {
      nullable6 = new Decimal?();
      nullable15 = nullable6;
    }
    else
      nullable15 = new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local20.YtdDeposits = nullable15;
    ref History local21 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local22 = (object) local21;
    nullable1 = local21.YtdRetainageReleased;
    Decimal retainageReleased1 = bucket.SignRetainageReleased;
    nullable6 = nullable2;
    nullable4 = nullable6.HasValue ? new Decimal?(retainageReleased1 * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable16;
    if (!(nullable1.HasValue & nullable4.HasValue))
    {
      nullable6 = new Decimal?();
      nullable16 = nullable6;
    }
    else
      nullable16 = new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault());
    local22.YtdRetainageReleased = nullable16;
    ref History local23 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local24 = (object) local23;
    nullable4 = local23.PtdRetainageReleased;
    Decimal retainageReleased2 = bucket.SignRetainageReleased;
    nullable6 = nullable2;
    nullable1 = nullable6.HasValue ? new Decimal?(retainageReleased2 * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable17;
    if (!(nullable4.HasValue & nullable1.HasValue))
    {
      nullable6 = new Decimal?();
      nullable17 = nullable6;
    }
    else
      nullable17 = new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local24.PtdRetainageReleased = nullable17;
    ref History local25 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local26 = (object) local25;
    nullable1 = local25.YtdRetainageWithheld;
    Decimal retainageWithheld1 = bucket.SignRetainageWithheld;
    nullable6 = nullable2;
    nullable4 = nullable6.HasValue ? new Decimal?(retainageWithheld1 * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable18;
    if (!(nullable1.HasValue & nullable4.HasValue))
    {
      nullable6 = new Decimal?();
      nullable18 = nullable6;
    }
    else
      nullable18 = new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault());
    local26.YtdRetainageWithheld = nullable18;
    ref History local27 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local28 = (object) local27;
    nullable4 = local27.PtdRetainageWithheld;
    Decimal retainageWithheld2 = bucket.SignRetainageWithheld;
    nullable6 = nullable2;
    nullable1 = nullable6.HasValue ? new Decimal?(retainageWithheld2 * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable19;
    if (!(nullable4.HasValue & nullable1.HasValue))
    {
      nullable6 = new Decimal?();
      nullable19 = nullable6;
    }
    else
      nullable19 = new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local28.PtdRetainageWithheld = nullable19;
  }

  private void UpdateFinHist<History>(
    History accthist,
    APReleaseProcess.APHistBucket bucket,
    PX.Objects.GL.GLTran tran)
    where History : class, IBaseAPHist
  {
    this.UpdateHist<History>(accthist, bucket, true, tran);
  }

  private void UpdateTranHist<History>(
    History accthist,
    APReleaseProcess.APHistBucket bucket,
    PX.Objects.GL.GLTran tran)
    where History : class, IBaseAPHist
  {
    this.UpdateHist<History>(accthist, bucket, false, tran);
  }

  private void CuryUpdateHist<History>(
    History accthist,
    APReleaseProcess.APHistBucket bucket,
    bool FinFlag,
    PX.Objects.GL.GLTran tran)
    where History : class, ICuryAPHist, IBaseAPHist
  {
    if (this._IsIntegrityCheck || this.IsInvoiceReclassification)
    {
      bool? detDeleted = accthist.DetDeleted;
      bool flag = false;
      if (!(detDeleted.GetValueOrDefault() == flag & detDeleted.HasValue))
        return;
    }
    this.UpdateHist<History>(accthist, bucket, FinFlag, tran);
    Decimal? nullable1 = tran.CuryDebitAmt;
    Decimal? nullable2 = tran.CuryCreditAmt;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    accthist.FinFlag = new bool?(FinFlag);
    ref History local1 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local2 = (object) local1;
    nullable2 = local1.CuryPtdPayments;
    Decimal signPayments = bucket.SignPayments;
    Decimal? nullable4 = nullable3;
    nullable1 = nullable4.HasValue ? new Decimal?(signPayments * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5;
    if (!(nullable2.HasValue & nullable1.HasValue))
    {
      nullable4 = new Decimal?();
      nullable5 = nullable4;
    }
    else
      nullable5 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local2.CuryPtdPayments = nullable5;
    ref History local3 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local4 = (object) local3;
    nullable1 = local3.CuryPtdPurchases;
    Decimal signPurchases = bucket.SignPurchases;
    nullable4 = nullable3;
    nullable2 = nullable4.HasValue ? new Decimal?(signPurchases * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable6;
    if (!(nullable1.HasValue & nullable2.HasValue))
    {
      nullable4 = new Decimal?();
      nullable6 = nullable4;
    }
    else
      nullable6 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
    local4.CuryPtdPurchases = nullable6;
    ref History local5 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local6 = (object) local5;
    nullable2 = local5.CuryPtdCrAdjustments;
    Decimal signCrAdjustments = bucket.SignCrAdjustments;
    nullable4 = nullable3;
    nullable1 = nullable4.HasValue ? new Decimal?(signCrAdjustments * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable7;
    if (!(nullable2.HasValue & nullable1.HasValue))
    {
      nullable4 = new Decimal?();
      nullable7 = nullable4;
    }
    else
      nullable7 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local6.CuryPtdCrAdjustments = nullable7;
    ref History local7 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local8 = (object) local7;
    nullable1 = local7.CuryPtdDrAdjustments;
    Decimal signDrAdjustments = bucket.SignDrAdjustments;
    nullable4 = nullable3;
    nullable2 = nullable4.HasValue ? new Decimal?(signDrAdjustments * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable8;
    if (!(nullable1.HasValue & nullable2.HasValue))
    {
      nullable4 = new Decimal?();
      nullable8 = nullable4;
    }
    else
      nullable8 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
    local8.CuryPtdDrAdjustments = nullable8;
    ref History local9 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local10 = (object) local9;
    nullable2 = local9.CuryPtdDiscTaken;
    Decimal signDiscTaken = bucket.SignDiscTaken;
    nullable4 = nullable3;
    nullable1 = nullable4.HasValue ? new Decimal?(signDiscTaken * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable9;
    if (!(nullable2.HasValue & nullable1.HasValue))
    {
      nullable4 = new Decimal?();
      nullable9 = nullable4;
    }
    else
      nullable9 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local10.CuryPtdDiscTaken = nullable9;
    ref History local11 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local12 = (object) local11;
    nullable1 = local11.CuryPtdWhTax;
    Decimal signWhTax = bucket.SignWhTax;
    nullable4 = nullable3;
    nullable2 = nullable4.HasValue ? new Decimal?(signWhTax * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable10;
    if (!(nullable1.HasValue & nullable2.HasValue))
    {
      nullable4 = new Decimal?();
      nullable10 = nullable4;
    }
    else
      nullable10 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
    local12.CuryPtdWhTax = nullable10;
    ref History local13 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local14 = (object) local13;
    nullable2 = local13.CuryYtdBalance;
    Decimal signPtd = bucket.SignPtd;
    nullable4 = nullable3;
    nullable1 = nullable4.HasValue ? new Decimal?(signPtd * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable11;
    if (!(nullable2.HasValue & nullable1.HasValue))
    {
      nullable4 = new Decimal?();
      nullable11 = nullable4;
    }
    else
      nullable11 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local14.CuryYtdBalance = nullable11;
    ref History local15 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local16 = (object) local15;
    nullable1 = local15.CuryPtdDeposits;
    Decimal signDeposits1 = bucket.SignDeposits;
    nullable4 = nullable3;
    nullable2 = nullable4.HasValue ? new Decimal?(signDeposits1 * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable12;
    if (!(nullable1.HasValue & nullable2.HasValue))
    {
      nullable4 = new Decimal?();
      nullable12 = nullable4;
    }
    else
      nullable12 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
    local16.CuryPtdDeposits = nullable12;
    ref History local17 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local18 = (object) local17;
    nullable2 = local17.CuryYtdDeposits;
    Decimal signDeposits2 = bucket.SignDeposits;
    nullable4 = nullable3;
    nullable1 = nullable4.HasValue ? new Decimal?(signDeposits2 * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable13;
    if (!(nullable2.HasValue & nullable1.HasValue))
    {
      nullable4 = new Decimal?();
      nullable13 = nullable4;
    }
    else
      nullable13 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local18.CuryYtdDeposits = nullable13;
    ref History local19 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local20 = (object) local19;
    nullable1 = local19.CuryYtdRetainageReleased;
    Decimal retainageReleased1 = bucket.SignRetainageReleased;
    nullable4 = nullable3;
    nullable2 = nullable4.HasValue ? new Decimal?(retainageReleased1 * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable14;
    if (!(nullable1.HasValue & nullable2.HasValue))
    {
      nullable4 = new Decimal?();
      nullable14 = nullable4;
    }
    else
      nullable14 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
    local20.CuryYtdRetainageReleased = nullable14;
    ref History local21 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local22 = (object) local21;
    nullable2 = local21.CuryPtdRetainageReleased;
    Decimal retainageReleased2 = bucket.SignRetainageReleased;
    nullable4 = nullable3;
    nullable1 = nullable4.HasValue ? new Decimal?(retainageReleased2 * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable15;
    if (!(nullable2.HasValue & nullable1.HasValue))
    {
      nullable4 = new Decimal?();
      nullable15 = nullable4;
    }
    else
      nullable15 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local22.CuryPtdRetainageReleased = nullable15;
    ref History local23 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local24 = (object) local23;
    nullable1 = local23.CuryYtdRetainageWithheld;
    Decimal retainageWithheld1 = bucket.SignRetainageWithheld;
    nullable4 = nullable3;
    nullable2 = nullable4.HasValue ? new Decimal?(retainageWithheld1 * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable16;
    if (!(nullable1.HasValue & nullable2.HasValue))
    {
      nullable4 = new Decimal?();
      nullable16 = nullable4;
    }
    else
      nullable16 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
    local24.CuryYtdRetainageWithheld = nullable16;
    ref History local25 = ref accthist;
    // ISSUE: variable of a boxed type
    __Boxed<History> local26 = (object) local25;
    nullable2 = local25.CuryPtdRetainageWithheld;
    Decimal retainageWithheld2 = bucket.SignRetainageWithheld;
    nullable4 = nullable3;
    nullable1 = nullable4.HasValue ? new Decimal?(retainageWithheld2 * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable17;
    if (!(nullable2.HasValue & nullable1.HasValue))
    {
      nullable4 = new Decimal?();
      nullable17 = nullable4;
    }
    else
      nullable17 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
    local26.CuryPtdRetainageWithheld = nullable17;
  }

  private void CuryUpdateFinHist<History>(
    History accthist,
    APReleaseProcess.APHistBucket bucket,
    PX.Objects.GL.GLTran tran)
    where History : class, ICuryAPHist, IBaseAPHist
  {
    this.CuryUpdateHist<History>(accthist, bucket, true, tran);
  }

  private void CuryUpdateTranHist<History>(
    History accthist,
    APReleaseProcess.APHistBucket bucket,
    PX.Objects.GL.GLTran tran)
    where History : class, ICuryAPHist, IBaseAPHist
  {
    this.CuryUpdateHist<History>(accthist, bucket, false, tran);
  }

  private bool IsNeedUpdateHistoryForTransaction(string TranPeriodID)
  {
    if (this.IsInvoiceReclassification)
      return false;
    return !this._IsIntegrityCheck || string.Compare(TranPeriodID, this._IntegrityCheckStartingPeriod) >= 0;
  }

  private void UpdateHistory(PX.Objects.GL.GLTran tran, Vendor vend)
  {
    this.UpdateHistory(tran, vend.BAccountID);
  }

  private void UpdateHistory(PX.Objects.GL.GLTran tran, int? vendorID)
  {
    APReleaseProcess.APHistBucket bucket = new APReleaseProcess.APHistBucket(tran, this.GetHistTranType(tran.TranType, tran.RefNbr));
    this.UpdateHistory(tran, vendorID, bucket);
  }

  private void UpdateHistory(PX.Objects.GL.GLTran tran, int? vendorID, APReleaseProcess.APHistBucket bucket)
  {
    if (!this.IsNeedUpdateHistoryForTransaction(tran.TranPeriodID))
      return;
    APHist history1 = this.CreateHistory(tran.BranchID, bucket.apAccountID, bucket.apSubID, vendorID, tran.FinPeriodID);
    if (history1 != null)
      this.UpdateFinHist<APHist>(history1, bucket, tran);
    APHist history2 = this.CreateHistory(tran.BranchID, bucket.apAccountID, bucket.apSubID, vendorID, tran.TranPeriodID);
    if (history2 == null)
      return;
    this.UpdateTranHist<APHist>(history2, bucket, tran);
  }

  private void UpdateHistory(PX.Objects.GL.GLTran tran, Vendor vend, PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    this.UpdateHistory(tran, vend.BAccountID, info.CuryID);
  }

  private void UpdateHistory(PX.Objects.GL.GLTran tran, int? vendorID, string aCuryID)
  {
    APReleaseProcess.APHistBucket bucket = new APReleaseProcess.APHistBucket(tran, this.GetHistTranType(tran.TranType, tran.RefNbr));
    this.UpdateHistory(tran, vendorID, aCuryID, bucket);
  }

  private void UpdateHistory(
    PX.Objects.GL.GLTran tran,
    int? vendorID,
    string aCuryID,
    APReleaseProcess.APHistBucket bucket)
  {
    if (!this.IsNeedUpdateHistoryForTransaction(tran.TranPeriodID))
      return;
    CuryAPHist history1 = this.CreateHistory(tran.BranchID, bucket.apAccountID, bucket.apSubID, vendorID, aCuryID, tran.FinPeriodID);
    if (history1 != null)
      this.CuryUpdateFinHist<CuryAPHist>(history1, bucket, tran);
    CuryAPHist history2 = this.CreateHistory(tran.BranchID, bucket.apAccountID, bucket.apSubID, vendorID, aCuryID, tran.TranPeriodID);
    if (history2 == null)
      return;
    this.CuryUpdateTranHist<CuryAPHist>(history2, bucket, tran);
  }

  private string GetHistTranType(string tranType, string refNbr)
  {
    string histTranType = tranType;
    if (tranType == "VCK")
    {
      APRegister apRegister = (APRegister) PXSelectBase<APRegister, PXSelect<APRegister, Where<APRegister.refNbr, Equal<Required<APRegister.refNbr>>, PX.Data.And<Where<APRegister.docType, Equal<APDocType.check>, Or<APRegister.docType, Equal<APDocType.prepayment>>>>>, PX.Data.OrderBy<Asc<Switch<Case<Where<APRegister.docType, Equal<APDocType.check>>, PX.Objects.CS.int0>, int1>, Asc<APRegister.docType, Asc<APRegister.refNbr>>>>>.Config>.Select((PXGraph) this, (object) refNbr);
      if (apRegister != null)
        histTranType = apRegister.DocType;
    }
    return histTranType;
  }

  public virtual List<APRegister> CreateInstallments(
    PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Vendor> res)
  {
    APInvoice data = (APInvoice) res;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) res;
    PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) res;
    Vendor vendor = (Vendor) res;
    List<APRegister> installments = new List<APRegister>();
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
    PXResultset<TermsInstallments> pxResultset = TermsAttribute.SelectInstallments((PXGraph) this, terms, data.DueDate.Value);
    foreach (PXResult<TermsInstallments> pxResult in pxResultset)
    {
      TermsInstallments termsInstallments = (TermsInstallments) pxResult;
      instance.vendor.Current = vendor;
      this.APInvoice_DocType_RefNbr.Cache.GetValueExt((object) data, "CuryOrigDocAmt");
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = instance.GetExtension<APInvoiceEntry.MultiCurrency>().CloneCurrencyInfo(currencyInfo1);
      APInvoice copy = PXCache<APInvoice>.CreateCopy(data);
      copy.CuryInfoID = currencyInfo2.CuryInfoID;
      APInvoice apInvoice1 = copy;
      System.DateTime? nullable1 = copy.DueDate;
      System.DateTime? nullable2 = new System.DateTime?(nullable1.Value.AddDays((double) termsInstallments.InstDays.Value));
      apInvoice1.DueDate = nullable2;
      copy.DiscDate = copy.DueDate;
      copy.InstallmentNbr = termsInstallments.InstallmentNbr;
      copy.MasterRefNbr = copy.RefNbr;
      copy.RefNbr = (string) null;
      copy.NoteID = new Guid?();
      APInvoice apInvoice2 = copy;
      nullable1 = new System.DateTime?();
      System.DateTime? nullable3 = nullable1;
      apInvoice2.PayDate = nullable3;
      copy.IntercompanyInvoiceNoteID = new Guid?();
      copy.CuryDetailExtPriceTotal = new Decimal?(0M);
      copy.DetailExtPriceTotal = new Decimal?(0M);
      copy.CuryLineDiscTotal = new Decimal?(0M);
      copy.LineDiscTotal = new Decimal?(0M);
      TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(instance.Transactions.Cache, (object) null, TaxCalc.NoCalc);
      Decimal? nullable4 = new Decimal?();
      short? installmentNbr = termsInstallments.InstallmentNbr;
      int? nullable5 = installmentNbr.HasValue ? new int?((int) installmentNbr.GetValueOrDefault()) : new int?();
      int count = pxResultset.Count;
      Decimal? nullable6;
      if (nullable5.GetValueOrDefault() == count & nullable5.HasValue)
      {
        APInvoice apInvoice3 = copy;
        Decimal? curyOrigDocAmt = copy.CuryOrigDocAmt;
        Decimal num3 = num1;
        Decimal? nullable7 = curyOrigDocAmt.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() - num3) : new Decimal?();
        apInvoice3.CuryOrigDocAmt = nullable7;
        Decimal? origDocAmt = copy.OrigDocAmt;
        Decimal num4 = num2;
        nullable4 = origDocAmt.HasValue ? new Decimal?(origDocAmt.GetValueOrDefault() - num4) : new Decimal?();
      }
      else if (terms.InstallmentMthd == "A")
      {
        APInvoice apInvoice4 = copy;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo2;
        Decimal? curyOrigDocAmt1 = data.CuryOrigDocAmt;
        Decimal? nullable8 = data.CuryTaxTotal;
        Decimal? nullable9 = curyOrigDocAmt1.HasValue & nullable8.HasValue ? new Decimal?(curyOrigDocAmt1.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable10 = termsInstallments.InstPercent;
        Decimal? nullable11;
        if (!(nullable9.HasValue & nullable10.HasValue))
        {
          nullable8 = new Decimal?();
          nullable11 = nullable8;
        }
        else
          nullable11 = new Decimal?(nullable9.GetValueOrDefault() * nullable10.GetValueOrDefault() / 100M);
        nullable8 = nullable11;
        Decimal val = nullable8.Value;
        Decimal? nullable12 = new Decimal?(currencyInfo3.RoundCury(val));
        apInvoice4.CuryOrigDocAmt = nullable12;
        installmentNbr = termsInstallments.InstallmentNbr;
        int? nullable13;
        if (!installmentNbr.HasValue)
        {
          nullable5 = new int?();
          nullable13 = nullable5;
        }
        else
          nullable13 = new int?((int) installmentNbr.GetValueOrDefault());
        nullable5 = nullable13;
        if (nullable5.GetValueOrDefault() == 1)
        {
          APInvoice apInvoice5 = copy;
          Decimal? curyOrigDocAmt2 = apInvoice5.CuryOrigDocAmt;
          nullable10 = data.CuryTaxTotal;
          Decimal num5 = nullable10.Value;
          Decimal? nullable14;
          if (!curyOrigDocAmt2.HasValue)
          {
            nullable10 = new Decimal?();
            nullable14 = nullable10;
          }
          else
            nullable14 = new Decimal?(curyOrigDocAmt2.GetValueOrDefault() + num5);
          apInvoice5.CuryOrigDocAmt = nullable14;
        }
      }
      else
      {
        APInvoice apInvoice6 = copy;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo4 = currencyInfo2;
        nullable6 = data.CuryOrigDocAmt;
        Decimal? instPercent = termsInstallments.InstPercent;
        Decimal val = (nullable6.HasValue & instPercent.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * instPercent.GetValueOrDefault() / 100M) : new Decimal?()).Value;
        Decimal? nullable15 = new Decimal?(currencyInfo4.RoundCury(val));
        apInvoice6.CuryOrigDocAmt = nullable15;
      }
      copy.CuryDocBal = copy.CuryOrigDocAmt;
      copy.CuryLineTotal = copy.CuryOrigDocAmt;
      copy.CuryTaxTotal = new Decimal?(0M);
      copy.CuryTaxAmt = new Decimal?(0M);
      copy.CuryOrigDiscAmt = new Decimal?(0M);
      copy.CuryVatTaxableTotal = new Decimal?(0M);
      copy.CuryDiscTot = new Decimal?(0M);
      copy.OrigModule = "AP";
      copy.Hold = new bool?(true);
      APInvoice apInvoice7 = instance.Document.Insert(copy);
      instance.Approval.SuppressApproval = true;
      apInvoice7.Hold = new bool?(false);
      if (nullable4.HasValue)
      {
        apInvoice7.OrigDocAmt = nullable4;
        apInvoice7.DocBal = nullable4;
        apInvoice7.LineTotal = nullable4;
      }
      instance.Document.Update(apInvoice7);
      Decimal num6 = num1;
      nullable6 = apInvoice7.CuryOrigDocAmt;
      Decimal num7 = nullable6.Value;
      num1 = num6 + num7;
      Decimal num8 = num2;
      nullable6 = apInvoice7.OrigDocAmt;
      Decimal num9 = nullable6.Value;
      num2 = num8 + num9;
      TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(instance.Transactions.Cache, (object) null, TaxCalc.NoCalc);
      APTran apTran1 = new APTran();
      apTran1.AccountID = apInvoice7.APAccountID;
      apTran1.SubID = apInvoice7.APSubID;
      apTran1.CuryTranAmt = apInvoice7.CuryOrigDocAmt;
      using (new PXLocaleScope(vendor.LocaleName))
        apTran1.TranDesc = PXMessages.LocalizeNoPrefix("Multiple Installment Split");
      APTran apTran2 = instance.Transactions.Insert(apTran1);
      if (nullable4.HasValue)
      {
        apTran2.TranAmt = nullable4;
        instance.Transactions.Update(apTran2);
        if (currencyInfo2.BaseCuryID != currencyInfo2.CuryID)
          currencyInfo2.BaseCalc = new bool?(false);
      }
      instance.Save.Press();
      installments.Add((APRegister) instance.Document.Current);
      instance.Clear();
    }
    if (pxResultset.Count > 0)
      PXDatabase.Update<APInvoice>((PXDataFieldParam) new PXDataFieldAssign<APInvoice.installmentCntr>(PXDbType.SmallInt, (object) Convert.ToInt16(pxResultset.Count)), (PXDataFieldParam) new PXDataFieldRestrict<APInvoice.docType>(PXDbType.VarChar, (object) data.DocType), (PXDataFieldParam) new PXDataFieldRestrict<APInvoice.refNbr>(PXDbType.NVarChar, (object) data.RefNbr));
    return installments;
  }

  public static Decimal? RoundAmount(Decimal? amount, string RoundType, Decimal? precision)
  {
    Decimal? nullable1 = amount;
    Decimal? nullable2 = precision;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?();
    switch (RoundType)
    {
      case "F":
        Decimal num1 = System.Math.Floor(nullable3.Value);
        Decimal? nullable4 = precision;
        return !nullable4.HasValue ? new Decimal?() : new Decimal?(num1 * nullable4.GetValueOrDefault());
      case "C":
        Decimal num2 = System.Math.Ceiling(nullable3.Value);
        Decimal? nullable5 = precision;
        return !nullable5.HasValue ? new Decimal?() : new Decimal?(num2 * nullable5.GetValueOrDefault());
      case "R":
        Decimal num3 = System.Math.Round(nullable3.Value, 0, MidpointRounding.AwayFromZero);
        Decimal? nullable6 = precision;
        return !nullable6.HasValue ? new Decimal?() : new Decimal?(num3 * nullable6.GetValueOrDefault());
      default:
        return amount;
    }
  }

  public virtual Decimal? RoundAmount(Decimal? amount)
  {
    return APReleaseProcess.RoundAmount(amount, this.InvoiceRounding, this.InvoicePrecision);
  }

  public virtual APAdjust FillSelfApplicationForDocument(APRegister doc)
  {
    return new APAdjust()
    {
      AdjgDocType = doc.DocType,
      AdjgRefNbr = doc.RefNbr,
      AdjdDocType = doc.DocType,
      AdjdRefNbr = doc.RefNbr,
      AdjNbr = doc.AdjCntr,
      AdjgBranchID = doc.BranchID,
      AdjdBranchID = doc.BranchID,
      VendorID = doc.VendorID,
      AdjdAPAcct = doc.APAccountID,
      AdjdAPSub = doc.APSubID,
      AdjgCuryInfoID = doc.CuryInfoID,
      AdjdCuryInfoID = doc.CuryInfoID,
      AdjdOrigCuryInfoID = doc.CuryInfoID,
      AdjgDocDate = doc.DocDate,
      AdjdDocDate = doc.DocDate,
      AdjgFinPeriodID = doc.FinPeriodID,
      AdjgTranPeriodID = doc.TranPeriodID,
      AdjdFinPeriodID = doc.FinPeriodID,
      AdjdTranPeriodID = doc.TranPeriodID,
      CuryAdjgAmt = doc.CuryOrigDocAmt,
      CuryAdjdAmt = doc.CuryOrigDocAmt,
      AdjAmt = doc.OrigDocAmt,
      RGOLAmt = new Decimal?(0M),
      CuryAdjgDiscAmt = doc.CuryOrigDiscAmt,
      CuryAdjdDiscAmt = doc.CuryOrigDiscAmt,
      AdjDiscAmt = doc.OrigDiscAmt,
      CuryAdjgWhTaxAmt = doc.CuryOrigWhTaxAmt,
      CuryAdjdWhTaxAmt = doc.CuryOrigWhTaxAmt,
      AdjWhTaxAmt = doc.OrigWhTaxAmt,
      Released = new bool?(false)
    };
  }

  public virtual APAdjust CreateSelfApplicationForDocument(APRegister doc)
  {
    return (APAdjust) this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache.Insert((object) this.FillSelfApplicationForDocument(doc));
  }

  protected virtual void ProcessMigratedDocument(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APRegister doc,
    bool isDebit,
    Vendor vendor,
    PX.Objects.CM.Extensions.CurrencyInfo currencyinfo)
  {
    APAdjust apAdjust1 = this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache.InitNewRow<APAdjust>(this.FillSelfApplicationForDocument(doc));
    if (this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache.Locate((object) apAdjust1) is APAdjust apAdjust2)
      this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache.SetStatus((object) apAdjust2, PXEntryStatus.Inserted);
    else
      apAdjust2 = this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache.Insert((object) apAdjust1) as APAdjust;
    apAdjust2.RGOLAmt = new Decimal?(0M);
    apAdjust2.CuryAdjgDiscAmt = new Decimal?(0M);
    apAdjust2.CuryAdjdDiscAmt = new Decimal?(0M);
    apAdjust2.AdjDiscAmt = new Decimal?(0M);
    apAdjust2.CuryAdjgWhTaxAmt = new Decimal?(0M);
    apAdjust2.CuryAdjdWhTaxAmt = new Decimal?(0M);
    apAdjust2.AdjWhTaxAmt = new Decimal?(0M);
    APAdjust apAdjust3 = apAdjust2;
    Decimal? curyAdjgAmt = apAdjust3.CuryAdjgAmt;
    Decimal? curyInitDocBal = doc.CuryInitDocBal;
    apAdjust3.CuryAdjgAmt = curyAdjgAmt.HasValue & curyInitDocBal.HasValue ? new Decimal?(curyAdjgAmt.GetValueOrDefault() - curyInitDocBal.GetValueOrDefault()) : new Decimal?();
    APAdjust apAdjust4 = apAdjust2;
    Decimal? curyAdjdAmt = apAdjust4.CuryAdjdAmt;
    Decimal? nullable = doc.CuryInitDocBal;
    apAdjust4.CuryAdjdAmt = curyAdjdAmt.HasValue & nullable.HasValue ? new Decimal?(curyAdjdAmt.GetValueOrDefault() - nullable.GetValueOrDefault()) : new Decimal?();
    APAdjust apAdjust5 = apAdjust2;
    nullable = apAdjust5.AdjAmt;
    Decimal? initDocBal = doc.InitDocBal;
    apAdjust5.AdjAmt = nullable.HasValue & initDocBal.HasValue ? new Decimal?(nullable.GetValueOrDefault() - initDocBal.GetValueOrDefault()) : new Decimal?();
    apAdjust2.Released = new bool?(true);
    apAdjust2.IsInitialApplication = new bool?(true);
    APAdjust adj = (APAdjust) this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache.Update((object) apAdjust2);
    if (!adj.VoidAppl.GetValueOrDefault())
    {
      this.UpdateBalances(adj, doc, vendor);
      this.VerifyAdjustedDocumentAndClose(doc);
    }
    PX.Objects.GL.GLTran copy = (PX.Objects.GL.GLTran) je.GLTranModuleBatNbr.Cache.CreateCopy((object) tran);
    copy.TranClass = "N";
    copy.TranType = "ADR";
    copy.DebitAmt = isDebit ? adj.AdjAmt : new Decimal?(0M);
    copy.CuryDebitAmt = isDebit ? adj.CuryAdjgAmt : new Decimal?(0M);
    copy.CreditAmt = isDebit ? new Decimal?(0M) : adj.AdjAmt;
    copy.CuryCreditAmt = isDebit ? new Decimal?(0M) : adj.CuryAdjgAmt;
    this.UpdateHistory(copy, vendor);
    this.UpdateHistory(copy, vendor, currencyinfo);
    this.ProcessAdjustmentTranPost(adj, doc, doc, true);
    APReleaseProcess.APHistBucket apHistBucket = new APReleaseProcess.APHistBucket(tran, this.GetHistTranType(tran.TranType, tran.RefNbr));
    if (!(apHistBucket.SignDeposits != 0M))
      return;
    APReleaseProcess.APHistBucket bucket = new APReleaseProcess.APHistBucket();
    Decimal signDeposits = apHistBucket.SignDeposits;
    bucket.apAccountID = tran.AccountID;
    bucket.apSubID = tran.SubID;
    bucket.SignDeposits = signDeposits;
    bucket.SignPayments = -signDeposits;
    bucket.SignPtd = signDeposits;
    this.UpdateHistory(copy, vendor.BAccountID, bucket);
    this.UpdateHistory(copy, vendor.BAccountID, currencyinfo.CuryID, bucket);
  }

  public virtual void VerifyStockItemLineHasReceipt(APRegister doc)
  {
    if (this.IsMigrationMode.GetValueOrDefault() || doc.IsMigratedRecord.GetValueOrDefault() || doc.IsRetainageDocument.GetValueOrDefault())
      return;
    if ((APTran) PXSelectBase<APTran, PXSelectJoin<APTran, InnerJoin<PX.Objects.IN.InventoryItem, On<APTran.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>, Where<APTran.refNbr, Equal<Required<APInvoice.refNbr>>, And<APTran.tranType, Equal<Required<APInvoice.docType>>, And2<Where<APTran.pOAccrualType, PX.Data.IsNull, Or<APTran.pOAccrualType, NotEqual<POAccrualType.order>>>, And<APTran.receiptNbr, PX.Data.IsNull, And<PX.Objects.IN.InventoryItem.stkItem, Equal<True>, And<APTran.tranType, NotEqual<APDocType.prepayment>>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) doc.RefNbr, (object) doc.DocType) != null)
      throw new PXException("All lines of the \"Goods for IN\" type must be linked to a purchase receipt lines.");
  }

  public virtual void VerifyInterBranchTransactions(APRegister doc)
  {
    if (this.IsMigrationMode.GetValueOrDefault() || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.interBranch>())
      return;
    PX.Objects.GL.Branch branch = (PX.Objects.GL.Branch) PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) doc.BranchID);
    if (PXSelectBase<APAdjust, PXSelectJoin<APAdjust, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<APAdjust.adjdBranchID>>>, Where<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>, And<APAdjust.adjgBranchID, NotEqual<APAdjust.adjdBranchID>, And<PX.Objects.GL.Branch.organizationID, NotEqual<Required<PX.Objects.GL.Branch.organizationID>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) doc.DocType, (object) doc.RefNbr, (object) branch.OrganizationID).Any<PXResult<APAdjust>>())
      throw new PXException("The application cannot be released, because the documents are related to different branches and the Inter-Branch Transactions feature is disabled.");
    if (PXSelectBase<APAdjust, PXSelectJoin<APAdjust, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<APAdjust.adjgBranchID>>>, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.adjgBranchID, NotEqual<APAdjust.adjdBranchID>, And<PX.Objects.GL.Branch.organizationID, NotEqual<Required<PX.Objects.GL.Branch.organizationID>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) doc.DocType, (object) doc.RefNbr, (object) branch.OrganizationID).Any<PXResult<APAdjust>>())
      throw new PXException("The application cannot be released, because the documents are related to different branches and the Inter-Branch Transactions feature is disabled.");
  }

  /// <summary>
  /// The method to release invoices.
  /// The maintenance screen is "Bills And Adjustments" (AP301000).
  /// </summary>
  public virtual List<APRegister> ReleaseInvoice(
    JournalEntry je,
    ref APRegister doc,
    PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Vendor> res,
    bool isPrebooking,
    out List<PX.Objects.IN.INRegister> inDocs)
  {
    APInvoice apInvoice1 = (APInvoice) res;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) res;
    PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) res;
    Vendor vendor1 = (Vendor) res;
    if (currencyInfo1.BaseCuryID != currencyInfo1.CuryID && apInvoice1.InstallmentNbr.HasValue)
      currencyInfo1.BaseCalc = new bool?(false);
    this.APInvoice_DocType_RefNbr.Current = apInvoice1;
    this.APDocument.Cache.Current = (object) doc;
    this.GetExtension<APReleaseProcess.MultiCurrency>().StoreResult(currencyInfo1);
    List<APRegister> apRegisterList1 = new List<APRegister>();
    inDocs = new List<PX.Objects.IN.INRegister>();
    if (!doc.Released.GetValueOrDefault())
    {
      bool? nullable1;
      if (isPrebooking)
      {
        nullable1 = doc.Prebooked;
        if (nullable1.GetValueOrDefault())
          goto label_385;
      }
      string str1 = terms.InstallmentType;
      bool masterInstallment = apInvoice1.InstallmentCntr.HasValue;
      if (this._IsIntegrityCheck && !apInvoice1.InstallmentNbr.HasValue)
        str1 = apInvoice1.InstallmentCntr.HasValue ? "M" : "S";
      nullable1 = doc.Prebooked;
      bool flag1 = nullable1.GetValueOrDefault();
      if (doc.DocType == "INV" || doc.DocType == "ACR")
      {
        nullable1 = doc.Voided;
        if (nullable1.GetValueOrDefault())
          flag1 = true;
      }
      bool flag2 = doc.DocType == "VQC" && !string.IsNullOrEmpty(apInvoice1.PrebookBatchNbr);
      if (flag1 && string.IsNullOrEmpty(apInvoice1.PrebookBatchNbr))
        throw new PXException("The document {0} {1} is marked as pre-released, but the link to the Pre-releasing batch is missed", new object[2]
        {
          (object) doc.DocType,
          (object) doc.RefNbr
        });
      if (str1 == "M" & isPrebooking)
        throw new PXException("Invoices with multiple installments terms may not be pre-released");
      if (str1 == "M" && (apInvoice1.DocType == "QCK" || apInvoice1.DocType == "VQC"))
        throw new PXException("Multiple installments are not allowed for cash purchases.");
      if (str1 == "M" && !apInvoice1.InstallmentNbr.HasValue)
      {
        if (!this._IsIntegrityCheck && !this.IsInvoiceReclassification)
          apRegisterList1 = this.CreateInstallments(res);
        masterInstallment = true;
        doc.CuryDocBal = new Decimal?(0M);
        doc.DocBal = new Decimal?(0M);
        doc.CuryDiscBal = new Decimal?(0M);
        doc.DiscBal = new Decimal?(0M);
        doc.CuryDiscTaken = new Decimal?(0M);
        doc.DiscTaken = new Decimal?(0M);
        doc.CuryWhTaxBal = new Decimal?(0M);
        doc.WhTaxBal = new Decimal?(0M);
        doc.CuryTaxWheld = new Decimal?(0M);
        doc.TaxWheld = new Decimal?(0M);
        doc.OpenDoc = new bool?(false);
        doc.ClosedDate = doc.DocDate;
        doc.ClosedFinPeriodID = doc.FinPeriodID;
        this.RaiseInvoiceEvent(doc, PXEntityEventBase<APInvoice>.Container<APInvoice.Events>.Select((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (ev => ev.CloseDocument)));
      }
      else if (!flag1)
      {
        if (!this.IsInvoiceReclassification)
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
          doc.CuryWhTaxBal = doc.CuryOrigWhTaxAmt;
          doc.WhTaxBal = doc.OrigWhTaxAmt;
          doc.CuryDiscTaken = new Decimal?(0M);
          doc.DiscTaken = new Decimal?(0M);
          doc.CuryTaxWheld = new Decimal?(0M);
          doc.TaxWheld = new Decimal?(0M);
          doc.RGOLAmt = new Decimal?(0M);
          doc.OpenDoc = new bool?(true);
          doc.ClosedDate = new System.DateTime?();
          doc.ClosedFinPeriodID = (string) null;
          doc.ClosedTranPeriodID = (string) null;
        }
        this.RaiseInvoiceEvent(doc, PXEntityEventBase<APInvoice>.Container<APInvoice.Events>.Select((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (ev => ev.OpenDocument)));
      }
      PXCache<APRegister>.RestoreCopy((APRegister) apInvoice1, doc);
      PX.Objects.CM.Extensions.CurrencyInfo ex = PX.Objects.CM.Extensions.CurrencyInfo.GetEX(this.GetCurrencyInfoCopyForGL(je, currencyInfo1));
      bool isDebit = apInvoice1.DrCr == "D";
      bool flag3 = apInvoice1.DocType == "QCK" || apInvoice1.DocType == "VQC" || apInvoice1.DocType == "RQC";
      if (!flag1)
      {
        if (flag3)
        {
          PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
          glTran1.SummPost = new bool?(true);
          glTran1.ZeroPost = new bool?(false);
          glTran1.BranchID = apInvoice1.BranchID;
          glTran1.AccountID = apInvoice1.APAccountID;
          glTran1.SubID = apInvoice1.APSubID;
          glTran1.ReclassificationProhibited = new bool?(true);
          PX.Objects.GL.GLTran glTran2 = glTran1;
          Decimal? nullable2;
          Decimal? nullable3;
          if (!isDebit)
          {
            Decimal? curyOrigDocAmt = apInvoice1.CuryOrigDocAmt;
            Decimal? curyOrigDiscAmt = apInvoice1.CuryOrigDiscAmt;
            nullable2 = curyOrigDocAmt.HasValue & curyOrigDiscAmt.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() + curyOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
            Decimal? curyOrigWhTaxAmt = apInvoice1.CuryOrigWhTaxAmt;
            nullable3 = nullable2.HasValue & curyOrigWhTaxAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + curyOrigWhTaxAmt.GetValueOrDefault()) : new Decimal?();
          }
          else
            nullable3 = new Decimal?(0M);
          glTran2.CuryDebitAmt = nullable3;
          PX.Objects.GL.GLTran glTran3 = glTran1;
          Decimal? nullable4;
          Decimal? nullable5;
          if (!isDebit)
          {
            Decimal? origDocAmt = apInvoice1.OrigDocAmt;
            Decimal? origDiscAmt = apInvoice1.OrigDiscAmt;
            nullable4 = origDocAmt.HasValue & origDiscAmt.HasValue ? new Decimal?(origDocAmt.GetValueOrDefault() + origDiscAmt.GetValueOrDefault()) : new Decimal?();
            nullable2 = apInvoice1.OrigWhTaxAmt;
            nullable5 = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          }
          else
            nullable5 = new Decimal?(0M);
          glTran3.DebitAmt = nullable5;
          PX.Objects.GL.GLTran glTran4 = glTran1;
          Decimal? nullable6;
          if (!isDebit)
          {
            nullable6 = new Decimal?(0M);
          }
          else
          {
            Decimal? curyOrigDocAmt = apInvoice1.CuryOrigDocAmt;
            Decimal? curyOrigDiscAmt = apInvoice1.CuryOrigDiscAmt;
            nullable2 = curyOrigDocAmt.HasValue & curyOrigDiscAmt.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() + curyOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
            nullable4 = apInvoice1.CuryOrigWhTaxAmt;
            nullable6 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          }
          glTran4.CuryCreditAmt = nullable6;
          PX.Objects.GL.GLTran glTran5 = glTran1;
          Decimal? nullable7;
          if (!isDebit)
          {
            nullable7 = new Decimal?(0M);
          }
          else
          {
            Decimal? origDocAmt = apInvoice1.OrigDocAmt;
            Decimal? origDiscAmt = apInvoice1.OrigDiscAmt;
            nullable4 = origDocAmt.HasValue & origDiscAmt.HasValue ? new Decimal?(origDocAmt.GetValueOrDefault() + origDiscAmt.GetValueOrDefault()) : new Decimal?();
            nullable2 = apInvoice1.OrigWhTaxAmt;
            nullable7 = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          }
          glTran5.CreditAmt = nullable7;
          glTran1.TranType = apInvoice1.DocType;
          glTran1.TranClass = apInvoice1.DocClass;
          glTran1.RefNbr = apInvoice1.RefNbr;
          glTran1.TranDesc = apInvoice1.DocDesc;
          FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran1, apInvoice1.TranPeriodID);
          glTran1.TranDate = apInvoice1.DocDate;
          glTran1.CuryInfoID = ex.CuryInfoID;
          glTran1.Released = new bool?(true);
          glTran1.ReferenceID = apInvoice1.VendorID;
          glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
          this.InsertInvoiceTransaction(je, glTran1, new APReleaseProcess.GLTranInsertionContext()
          {
            APRegisterRecord = doc
          });
        }
        else if (apInvoice1.DocType != "PPM")
        {
          string empty = string.Empty;
          string str2 = !apInvoice1.IsChildRetainageDocument() ? (!apInvoice1.IsPrepaymentInvoiceDocument() ? apInvoice1.DocClass : "P") : "F";
          PX.Objects.GL.GLTran glTran6 = new PX.Objects.GL.GLTran();
          glTran6.SummPost = new bool?(true);
          glTran6.BranchID = apInvoice1.BranchID;
          glTran6.AccountID = apInvoice1.APAccountID;
          glTran6.SubID = apInvoice1.APSubID;
          glTran6.ReclassificationProhibited = new bool?(true);
          glTran6.CuryDebitAmt = isDebit ? new Decimal?(0M) : apInvoice1.CuryOrigDocAmt;
          glTran6.CuryCreditAmt = isDebit ? apInvoice1.CuryOrigDocAmt : new Decimal?(0M);
          if (!this.IsInvoiceReclassification)
          {
            PX.Objects.GL.GLTran glTran7 = glTran6;
            Decimal? nullable8;
            if (!isDebit)
            {
              Decimal? origDocAmt = apInvoice1.OrigDocAmt;
              Decimal? rgolAmt = apInvoice1.RGOLAmt;
              nullable8 = origDocAmt.HasValue & rgolAmt.HasValue ? new Decimal?(origDocAmt.GetValueOrDefault() - rgolAmt.GetValueOrDefault()) : new Decimal?();
            }
            else
              nullable8 = new Decimal?(0M);
            glTran7.DebitAmt = nullable8;
            PX.Objects.GL.GLTran glTran8 = glTran6;
            Decimal? nullable9;
            if (!isDebit)
            {
              nullable9 = new Decimal?(0M);
            }
            else
            {
              Decimal? origDocAmt = apInvoice1.OrigDocAmt;
              Decimal? rgolAmt = apInvoice1.RGOLAmt;
              nullable9 = origDocAmt.HasValue & rgolAmt.HasValue ? new Decimal?(origDocAmt.GetValueOrDefault() - rgolAmt.GetValueOrDefault()) : new Decimal?();
            }
            glTran8.CreditAmt = nullable9;
          }
          else
          {
            glTran6.DebitAmt = isDebit ? new Decimal?(0M) : apInvoice1.OrigDocAmt;
            glTran6.CreditAmt = isDebit ? apInvoice1.OrigDocAmt : new Decimal?(0M);
          }
          glTran6.TranType = apInvoice1.DocType;
          glTran6.TranClass = str2;
          glTran6.RefNbr = apInvoice1.RefNbr;
          glTran6.TranDesc = apInvoice1.DocDesc;
          FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran6, apInvoice1.TranPeriodID);
          glTran6.TranDate = apInvoice1.DocDate;
          glTran6.CuryInfoID = ex.CuryInfoID;
          glTran6.Released = new bool?(true);
          glTran6.ReferenceID = apInvoice1.VendorID;
          glTran6.ProjectID = ProjectDefaultAttribute.NonProject();
          glTran6.NonBillable = new bool?(true);
          nullable1 = doc.OpenDoc;
          if (nullable1.GetValueOrDefault())
          {
            this.UpdateHistory(glTran6, vendor1);
            this.UpdateHistory(glTran6, vendor1, currencyInfo1);
          }
          this.InsertInvoiceTransaction(je, glTran6, new APReleaseProcess.GLTranInsertionContext()
          {
            APRegisterRecord = doc
          });
          if (apInvoice1.IsPrepaymentInvoiceDocument() || apInvoice1.IsPrepaymentInvoiceDocumentReverse())
          {
            PX.Objects.GL.GLTran copy = (PX.Objects.GL.GLTran) je.GLTranModuleBatNbr.Cache.CreateCopy((object) glTran6);
            copy.SummPost = new bool?(true);
            copy.ReclassificationProhibited = new bool?(true);
            copy.AccountID = apInvoice1.PrepaymentAccountID;
            copy.SubID = apInvoice1.PrepaymentSubID;
            copy.BranchID = apInvoice1.BranchID;
            copy.CreditAmt = glTran6.DebitAmt;
            copy.CuryCreditAmt = glTran6.CuryDebitAmt;
            copy.DebitAmt = glTran6.CreditAmt;
            copy.CuryDebitAmt = glTran6.CuryCreditAmt;
            copy.TranClass = "Y";
            nullable1 = doc.OpenDoc;
            if (nullable1.GetValueOrDefault())
            {
              this.UpdateHistory(copy, vendor1);
              this.UpdateHistory(copy, vendor1, currencyInfo1);
            }
            this.InsertInvoiceTransaction(je, copy, new APReleaseProcess.GLTranInsertionContext()
            {
              APRegisterRecord = doc
            });
          }
          if (apInvoice1.IsOriginalRetainageDocument())
          {
            PX.Objects.GL.GLTran copy = (PX.Objects.GL.GLTran) je.GLTranModuleBatNbr.Cache.CreateCopy((object) glTran6);
            copy.ReclassificationProhibited = new bool?(true);
            copy.AccountID = apInvoice1.RetainageAcctID;
            copy.SubID = apInvoice1.RetainageSubID;
            copy.CuryDebitAmt = isDebit ? new Decimal?(0M) : apInvoice1.CuryRetainageTotal;
            copy.DebitAmt = isDebit ? new Decimal?(0M) : apInvoice1.RetainageTotal;
            copy.CuryCreditAmt = isDebit ? apInvoice1.CuryRetainageTotal : new Decimal?(0M);
            copy.CreditAmt = isDebit ? apInvoice1.RetainageTotal : new Decimal?(0M);
            copy.OrigAccountID = glTran6.AccountID;
            copy.OrigSubID = glTran6.SubID;
            copy.TranClass = "E";
            this.UpdateHistory(copy, vendor1);
            this.UpdateHistory(copy, vendor1, currencyInfo1);
            je.GLTranModuleBatNbr.Insert(copy);
          }
          if (apInvoice1.IsOriginalRetainageDocument())
          {
            nullable1 = apInvoice1.IsRetainageReversing;
            if (nullable1.GetValueOrDefault())
            {
              APRegister retainageDocument = this.GetOriginalRetainageDocument((APRegister) apInvoice1);
              if (retainageDocument != null)
              {
                doc.CuryRetainageUnreleasedAmt = new Decimal?(0M);
                doc.CuryRetainageReleased = new Decimal?(0M);
                retainageDocument.CuryRetainageUnreleasedAmt = new Decimal?(0M);
                retainageDocument.CuryRetainageReleased = new Decimal?(0M);
                using (new DisableFormulaCalculationScope(this.APDocument.Cache, new System.Type[1]
                {
                  typeof (APRegister.curyRetainageReleased)
                }))
                  this.APDocument.Update(retainageDocument);
              }
            }
          }
          if (apInvoice1.IsChildRetainageDocument())
          {
            APRegister retainageDocument = this.GetOriginalRetainageDocument((APRegister) apInvoice1);
            if (retainageDocument != null)
            {
              Decimal? signAmount1 = retainageDocument.SignAmount;
              Decimal? signAmount2 = apInvoice1.SignAmount;
              Decimal? nullable10 = signAmount1.HasValue & signAmount2.HasValue ? new Decimal?(signAmount1.GetValueOrDefault() * signAmount2.GetValueOrDefault()) : new Decimal?();
              Decimal valueOrDefault = nullable10.GetValueOrDefault();
              APRegister apRegister1 = retainageDocument;
              Decimal? nullable11 = apRegister1.CuryRetainageUnreleasedAmt;
              Decimal? curyOrigDocAmt = apInvoice1.CuryOrigDocAmt;
              Decimal? nullable12 = apInvoice1.CuryRoundDiff;
              nullable10 = curyOrigDocAmt.HasValue & nullable12.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() + nullable12.GetValueOrDefault()) : new Decimal?();
              Decimal num1 = valueOrDefault;
              Decimal? nullable13;
              if (!nullable10.HasValue)
              {
                nullable12 = new Decimal?();
                nullable13 = nullable12;
              }
              else
                nullable13 = new Decimal?(nullable10.GetValueOrDefault() * num1);
              Decimal? nullable14 = nullable13;
              Decimal? nullable15;
              if (!(nullable11.HasValue & nullable14.HasValue))
              {
                nullable10 = new Decimal?();
                nullable15 = nullable10;
              }
              else
                nullable15 = new Decimal?(nullable11.GetValueOrDefault() - nullable14.GetValueOrDefault());
              apRegister1.CuryRetainageUnreleasedAmt = nullable15;
              APRegister apRegister2 = this.APDocument.Update(retainageDocument);
              if (!this._IsIntegrityCheck)
              {
                nullable1 = apRegister2.PaymentsByLinesAllowed;
                if (!nullable1.GetValueOrDefault())
                {
                  Decimal? retainageUnreleasedAmt1 = apRegister2.CuryRetainageUnreleasedAmt;
                  Decimal num2 = 0M;
                  if (!(retainageUnreleasedAmt1.GetValueOrDefault() < num2 & retainageUnreleasedAmt1.HasValue))
                  {
                    Decimal? retainageUnreleasedAmt2 = apRegister2.CuryRetainageUnreleasedAmt;
                    nullable11 = apRegister2.CuryRetainageTotal;
                    if (!(retainageUnreleasedAmt2.GetValueOrDefault() > nullable11.GetValueOrDefault() & retainageUnreleasedAmt2.HasValue & nullable11.HasValue))
                      goto label_78;
                  }
                  throw new PXException("The document cannot be released because the retainage has been fully released for the related original document.");
                }
              }
            }
          }
label_78:
          if (this.IsMigratedDocumentForProcessing(doc))
            this.ProcessMigratedDocument(je, glTran6, doc, isDebit, vendor1, currencyInfo1);
        }
      }
      Decimal docInclTaxDiscrepancy = 0.0M;
      if (apInvoice1.DocType == "PPM")
      {
        APTran b = (APTran) null;
        foreach (PXResult<APTran, APTax, PX.Objects.TX.Tax> pxResult in this.APTran_TranType_RefNbr.Select((object) apInvoice1.DocType, (object) apInvoice1.RefNbr))
        {
          APTran a = (APTran) pxResult;
          if (!this.APTran_TranType_RefNbr.Cache.ObjectsEqual((object) a, (object) b))
            this.InvoiceTransactionReleasing(new InvoiceTransactionReleasingArgs()
            {
              Invoice = apInvoice1,
              Transaction = a,
              IsPrebooking = isPrebooking
            });
          b = a;
        }
        this.InvoiceTransactionsReleased(new InvoiceTransactionsReleasedArgs()
        {
          Invoice = apInvoice1,
          IsPrebooking = isPrebooking
        });
      }
      else
      {
        PX.Objects.GL.GLTran glTran9 = (PX.Objects.GL.GLTran) null;
        if (isPrebooking | flag1 | flag2)
        {
          glTran9 = new PX.Objects.GL.GLTran()
          {
            SummPost = new bool?(true),
            ZeroPost = new bool?(false),
            CuryCreditAmt = new Decimal?(0M),
            CuryDebitAmt = new Decimal?(0M),
            CreditAmt = new Decimal?(0M),
            DebitAmt = new Decimal?(0M),
            BranchID = apInvoice1.BranchID,
            AccountID = apInvoice1.PrebookAcctID,
            SubID = apInvoice1.PrebookSubID,
            ReclassificationProhibited = new bool?(true),
            TranType = apInvoice1.DocType,
            TranClass = apInvoice1.DocClass,
            RefNbr = apInvoice1.RefNbr,
            TranDesc = flag1 ? PXMessages.LocalizeNoPrefix("Preliminary AP Expense Booking Adjustment") : PXMessages.LocalizeNoPrefix("Preliminary AP Expense Booking"),
            TranDate = apInvoice1.DocDate,
            CuryInfoID = ex.CuryInfoID,
            Released = new bool?(true),
            ReferenceID = apInvoice1.VendorID
          };
          FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran9, apInvoice1.TranPeriodID);
        }
        bool flag4 = !isPrebooking;
        PXResultset<APTran> pxResultset = this.APTran_TranType_RefNbr.Select((object) apInvoice1.DocType, (object) apInvoice1.RefNbr);
        IEqualityComparer<APTaxTran> comparer1 = (IEqualityComparer<APTaxTran>) new FieldSubsetEqualityComparer<APTaxTran>(this.Caches[typeof (APTaxTran)], new System.Type[1]
        {
          typeof (APTaxTran.recordID)
        });
        bool flag5 = this.IsPayByLineRetainageDebitAdj(doc);
        nullable1 = doc.PaymentsByLinesAllowed;
        bool flag6 = nullable1.GetValueOrDefault() | flag5;
        foreach (IGrouping<APTaxTran, PXResult<APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem, APTaxTran>> grouping in pxResultset.AsEnumerable<PXResult<APTran>>().Cast<PXResult<APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem, APTaxTran>>().GroupBy<PXResult<APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem, APTaxTran>, APTaxTran>((Func<PXResult<APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem, APTaxTran>, APTaxTran>) (row => (APTaxTran) row), comparer1))
        {
          APTaxTran key = grouping.Key;
          List<APTax> source = new List<APTax>();
          PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) null;
          foreach (PXResult<APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem, APTaxTran> pxResult in (IEnumerable<PXResult<APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem, APTaxTran>>) grouping)
          {
            APTran data = (APTran) pxResult;
            APTax apTax = (APTax) pxResult;
            tax = (PX.Objects.TX.Tax) pxResult;
            data.TranDate = apInvoice1.DocDate;
            this.APTran_TranType_RefNbr.Cache.SetDefaultExt<APTran.finPeriodID>((object) data);
            if (apTax.TranType != null && apTax.RefNbr != null && apTax.LineNbr.HasValue)
              source.Add(apTax);
          }
          if (source.Count<APTax>() > 0 & flag6)
          {
            APTaxAttribute apTaxAttribute1 = new APTaxAttribute(typeof (APRegister), typeof (APTax), typeof (APTaxTran));
            apTaxAttribute1.Inventory = typeof (APTran.inventoryID);
            apTaxAttribute1.UOM = typeof (APTran.uOM);
            apTaxAttribute1.LineQty = typeof (APTran.qty);
            APTaxAttribute apTaxAttribute2 = apTaxAttribute1;
            APTaxAttribute apTaxAttribute3 = apTaxAttribute2;
            List<APTax> taxDetList1 = source;
            Decimal? nullable16 = key.CuryTaxAmt;
            Decimal CuryTaxAmt1 = nullable16.Value;
            apTaxAttribute3.DistributeTaxDiscrepancy<APTax, APTax.curyTaxAmt, APTax.taxAmt>((PXGraph) this, (IEnumerable<APTax>) taxDetList1, CuryTaxAmt1, true);
            nullable16 = key.CuryRetainedTaxAmt;
            Decimal num = 0M;
            if (!(nullable16.GetValueOrDefault() == num & nullable16.HasValue))
            {
              APRetainedTaxAttribute retainedTaxAttribute = new APRetainedTaxAttribute(typeof (APRegister), typeof (APTax), typeof (APTaxTran));
              List<APTax> taxDetList2 = source;
              nullable16 = key.CuryRetainedTaxAmt;
              Decimal CuryTaxAmt2 = nullable16.Value;
              retainedTaxAttribute.DistributeTaxDiscrepancy<APTax, APTax.curyRetainedTaxAmt, APTax.retainedTaxAmt>((PXGraph) this, (IEnumerable<APTax>) taxDetList2, CuryTaxAmt2, true);
            }
            if (tax != null && tax.DeductibleVAT.GetValueOrDefault())
            {
              APTaxAttribute apTaxAttribute4 = apTaxAttribute2;
              List<APTax> taxDetList3 = source;
              nullable16 = key.CuryExpenseAmt;
              Decimal CuryTaxAmt3 = nullable16.Value;
              apTaxAttribute4.DistributeTaxDiscrepancy<APTax, APTax.curyExpenseAmt, APTax.expenseAmt>((PXGraph) this, (IEnumerable<APTax>) taxDetList3, CuryTaxAmt3, true);
            }
          }
        }
        this.FinPeriodUtils.AllowPostToUnlockedPeriodAnyway = this._IsIntegrityCheck;
        if (!this._IsIntegrityCheck)
          this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) pxResultset.Select<PXResult<APTran>, APTran>((Expression<Func<PXResult<APTran>, APTran>>) (line => (APTran) line)), typeof (OrganizationFinPeriod.aPClosed));
        this.FinPeriodUtils.AllowPostToUnlockedPeriodAnyway = false;
        this.ValidateLandedCostTran(doc, pxResultset);
        this.CheckVoidQuickCheckAmountDiscrepancies(doc);
        if (!this._IsIntegrityCheck)
        {
          foreach (PXResult<APTran> pxResult in pxResultset)
          {
            APTran row = (APTran) pxResult;
            if (!row.Released.GetValueOrDefault())
              ConvertedInventoryItemAttribute.ValidateRow(this.APTran_TranType_RefNbr.Cache, (object) row);
          }
        }
        IComparer<PX.Objects.TX.Tax> taxComparer = this.GetTaxComparer();
        ExceptionExtensions.ThrowOnNull<IComparer<PX.Objects.TX.Tax>>(taxComparer, "taxComparer", (string) null);
        pxResultset.Sort((Comparison<PXResult<APTran>>) ((x, y) =>
        {
          APTran apTran1 = (APTran) x;
          APTran apTran2 = (APTran) y;
          PX.Objects.TX.Tax x1 = x.GetItem<PX.Objects.TX.Tax>();
          PX.Objects.TX.Tax y1 = y.GetItem<PX.Objects.TX.Tax>();
          int? lineNbr1 = apTran1.LineNbr;
          int? lineNbr2 = apTran2.LineNbr;
          return !(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue) ? apTran1.LineNbr.Value - apTran2.LineNbr.Value : taxComparer.Compare(x1, y1);
        }));
        APReleaseProcess.LineBalances lineBalances = new APReleaseProcess.LineBalances(new Decimal?(0M));
        APTran apTran3 = (APTran) null;
        APTran tran1 = (APTran) null;
        IEqualityComparer<APTran> comparer2 = (IEqualityComparer<APTran>) new FieldSubsetEqualityComparer<APTran>(this.APTran_TranType_RefNbr.Cache, new System.Type[3]
        {
          typeof (APTran.tranType),
          typeof (APTran.refNbr),
          typeof (APTran.lineNbr)
        });
        HashSet<int> source1 = new HashSet<int>();
        foreach (IGrouping<APTran, PXResult<APTran>> grouping in pxResultset.AsEnumerable<PXResult<APTran>>().GroupBy<PXResult<APTran>, APTran>((Func<PXResult<APTran>, APTran>) (row => (APTran) row), comparer2))
        {
          APTran key = grouping.Key;
          PXCache<APTran>.StoreOriginal((PXGraph) this, key);
          HashSet<int> intSet = source1;
          int? nullable17 = key.ProjectID;
          int num3 = nullable17.Value;
          intSet.Add(num3);
          if (!this._IsIntegrityCheck && !this.IsInvoiceReclassification)
            key.ClearInvoiceDetailsBalance();
          if (!this._IsIntegrityCheck && !this.IsInvoiceReclassification && key.Released.GetValueOrDefault())
            throw new PXException("Document Status is invalid for processing.");
          if (!apInvoice1.IsPrepaymentInvoiceDocument() && !apInvoice1.IsPrepaymentInvoiceDocumentReverse())
          {
            bool flag7 = true;
            bool flag8 = str1 == "M" && !apInvoice1.InstallmentNbr.HasValue;
            Decimal? nullable18;
            Decimal? nullable19;
            foreach (PXResult<APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem, APTaxTran> r in (IEnumerable<PXResult<APTran>>) grouping)
            {
              APTax apTax = (APTax) r;
              PX.Objects.TX.Tax taxCorrespondingToLine = (PX.Objects.TX.Tax) r;
              DRDeferredCode drDeferredCode = (DRDeferredCode) r;
              PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) r;
              LandedCostCode landedCostCode = (LandedCostCode) r;
              APTaxTran apTaxTran = (APTaxTran) r;
              if (apTax != null && apTax.TaxID != null)
              {
                APReleaseProcess.AdjustTaxCalculationLevelForNetGrossEntryMode(apInvoice1, key, ref taxCorrespondingToLine);
                if (apInvoice1.TaxCalcMode == "T" && apTaxTran.IsTaxInclusive.GetValueOrDefault())
                  taxCorrespondingToLine.TaxCalcLevel = "0";
              }
              if (flag7)
              {
                PX.Objects.GL.GLTran tran = new PX.Objects.GL.GLTran();
                tran.ReclassificationProhibited = new bool?(doc.IsChildRetainageDocument());
                PX.Objects.GL.GLTran glTran10 = tran;
                int num4;
                if (this.SummPost)
                {
                  nullable17 = key.TaskID;
                  num4 = !nullable17.HasValue ? 1 : 0;
                }
                else
                  num4 = 0;
                bool? nullable20 = new bool?(num4 != 0);
                glTran10.SummPost = nullable20;
                tran.BranchID = key.BranchID;
                tran.CuryInfoID = ex.CuryInfoID;
                tran.TranType = key.TranType;
                tran.TranClass = apInvoice1.DocClass;
                tran.InventoryID = key.InventoryID;
                tran.UOM = key.UOM;
                PX.Objects.GL.GLTran glTran11 = tran;
                Decimal? nullable21;
                if (!(key.DrCr == "D"))
                {
                  Decimal num5 = (Decimal) -1;
                  nullable18 = key.Qty;
                  if (!nullable18.HasValue)
                  {
                    nullable19 = new Decimal?();
                    nullable21 = nullable19;
                  }
                  else
                    nullable21 = new Decimal?(num5 * nullable18.GetValueOrDefault());
                }
                else
                  nullable21 = key.Qty;
                glTran11.Qty = nullable21;
                tran.RefNbr = key.RefNbr;
                tran.TranDate = key.TranDate;
                tran.ProjectID = ProjectDefaultAttribute.NonProject();
                tran.CostCodeID = CostCodeAttribute.DefaultCostCode;
                tran.AccountID = key.AccountID;
                tran.SubID = key.SubID;
                tran.TranDesc = key.TranDesc;
                tran.Released = new bool?(true);
                tran.ReferenceID = apInvoice1.VendorID;
                PX.Objects.GL.GLTran glTran12 = tran;
                int? nullable22;
                if (!tran.SummPost.GetValueOrDefault())
                {
                  nullable22 = key.LineNbr;
                }
                else
                {
                  nullable17 = new int?();
                  nullable22 = nullable17;
                }
                glTran12.TranLineNbr = nullable22;
                tran.NonBillable = key.NonBillable;
                ARReleaseProcess.Amount expensePostingAmount = APReleaseProcess.GetExpensePostingAmount((PXGraph) this, key, apTax, taxCorrespondingToLine, apInvoice1, (Func<Decimal, Decimal>) (amount => PX.Objects.CM.PXDBCurrencyAttribute.Round(je.GLTranModuleBatNbr.Cache, (object) tran, amount, CMPrecision.TRANCURY)), (Func<Decimal, Decimal>) (amount => PX.Objects.CM.PXDBCurrencyAttribute.Round(je.GLTranModuleBatNbr.Cache, (object) tran, amount, CMPrecision.BASECURY)));
                tran.CuryDebitAmt = key.DrCr == "D" ? expensePostingAmount.Cury : new Decimal?(0M);
                tran.DebitAmt = key.DrCr == "D" ? expensePostingAmount.Base : new Decimal?(0M);
                tran.CuryCreditAmt = key.DrCr == "D" ? new Decimal?(0M) : expensePostingAmount.Cury;
                tran.CreditAmt = key.DrCr == "D" ? new Decimal?(0M) : expensePostingAmount.Base;
                this.ReleaseInvoiceTransactionPostProcessing(je, apInvoice1, r, tran);
                this.InvoiceTransactionReleasing(new InvoiceTransactionReleasingArgs()
                {
                  Invoice = apInvoice1,
                  Register = doc,
                  TransactionResult = r,
                  GLTransaction = tran,
                  PostedAmount = expensePostingAmount,
                  JournalEntry = je,
                  CurrencyInfo = ex,
                  IsPrebooking = isPrebooking
                });
                IEnumerable<PX.Objects.GL.GLTran> source2 = (IEnumerable<PX.Objects.GL.GLTran>) null;
                if (((this._IsIntegrityCheck || this.IsInvoiceReclassification || drDeferredCode == null ? 0 : (drDeferredCode.DeferredCodeID != null ? 1 : 0)) & (flag4 ? 1 : 0)) != 0)
                {
                  DRProcess instance = PXGraph.CreateInstance<DRProcess>();
                  DRProcess drProcess = instance;
                  APTran tran2 = key;
                  DRDeferredCode defCode = drDeferredCode;
                  APInvoice document = apInvoice1;
                  nullable18 = expensePostingAmount.Base;
                  Decimal amount = nullable18.Value;
                  drProcess.CreateSchedule(tran2, defCode, document, amount, false);
                  instance.Actions.PressSave();
                  source2 = je.CreateTransBySchedule(instance, tran);
                  JournalEntry journalEntry = je;
                  IEnumerable<PX.Objects.GL.GLTran> transactions = source2;
                  PX.Objects.GL.GLTran templateTran = tran;
                  nullable18 = expensePostingAmount.Cury;
                  Decimal curyExpectedTotal = nullable18.Value;
                  journalEntry.CorrectCuryAmountsDueToRounding(transactions, templateTran, curyExpectedTotal);
                }
                if (source2 == null || !source2.Any<PX.Objects.GL.GLTran>())
                  source2 = (IEnumerable<PX.Objects.GL.GLTran>) new PX.Objects.GL.GLTran[1]
                  {
                    tran
                  };
                if (isPrebooking || flag2)
                {
                  foreach (PX.Objects.GL.GLTran aSrc in source2)
                    APReleaseProcess.Append(glTran9, aSrc);
                }
                else
                {
                  foreach (PX.Objects.GL.GLTran glTran13 in source2)
                  {
                    this.InsertInvoiceDetailsScheduleTransaction(je, glTran13, new APReleaseProcess.GLTranInsertionContext()
                    {
                      APRegisterRecord = doc,
                      APTranRecord = key
                    });
                    if (flag1)
                      APReleaseProcess.Append(glTran9, glTran13);
                  }
                  key.Released = new bool?(true);
                }
                flag7 = false;
              }
              if (((this._IsIntegrityCheck ? 0 : (!this.IsInvoiceReclassification ? 1 : 0)) & (flag6 ? 1 : 0)) != 0 && key.LineType != "DS" && !flag8)
                lineBalances += this.AdjustInvoiceDetailsBalanceByTax(doc, key, apTax, taxCorrespondingToLine);
            }
            if (flag6 && key.LineType != "DS" && !this.IsInvoiceReclassification && !flag8)
            {
              if (!this._IsIntegrityCheck)
                lineBalances += this.AdjustInvoiceDetailsBalanceByLine(doc, key);
              key.RecoverInvoiceDetailsBalance();
              lineBalances += this.CalculateInvoiceDetailsCashDiscBalance(key, doc);
              APTran apTran4;
              if (apTran3 != null)
              {
                Decimal? origRetainageAmt = apTran3.CuryOrigRetainageAmt;
                nullable19 = key.CuryOrigRetainageAmt;
                if (!(origRetainageAmt.GetValueOrDefault() < nullable19.GetValueOrDefault() & origRetainageAmt.HasValue & nullable19.HasValue))
                {
                  apTran4 = apTran3;
                  goto label_182;
                }
              }
              apTran4 = key;
label_182:
              apTran3 = apTran4;
              APTran apTran5;
              if (tran1 != null)
              {
                nullable19 = tran1.CuryOrigTranAmt;
                nullable18 = key.CuryOrigTranAmt;
                if (!(nullable19.GetValueOrDefault() < nullable18.GetValueOrDefault() & nullable19.HasValue & nullable18.HasValue))
                {
                  apTran5 = tran1;
                  goto label_186;
                }
              }
              apTran5 = key;
label_186:
              tran1 = apTran5;
              if (apInvoice1.IsOriginalRetainageDocument() && apInvoice1.IsRetainageReversing.GetValueOrDefault())
              {
                APTran originalRetainageLine = this.GetOriginalRetainageLine((APRegister) apInvoice1, key);
                PXCache<APTran>.StoreOriginal((PXGraph) this, originalRetainageLine);
                if (originalRetainageLine != null)
                {
                  PXCache<APTran>.StoreOriginal((PXGraph) this, originalRetainageLine);
                  key.CuryRetainageBal = new Decimal?(0M);
                  key.RetainageBal = new Decimal?(0M);
                  originalRetainageLine.CuryRetainageBal = new Decimal?(0M);
                  originalRetainageLine.RetainageBal = new Decimal?(0M);
                  this.APTran_TranType_RefNbr.Update(originalRetainageLine);
                }
              }
              if (apInvoice1.IsChildRetainageDocument())
                this.AdjustOriginalRetainageLineBalance((APRegister) apInvoice1, key, key.CuryOrigTranAmt, key.OrigTranAmt);
              if (flag5)
              {
                key.CuryTranBal = new Decimal?(0M);
                key.TranBal = new Decimal?(0M);
                key.CuryRetainageBal = new Decimal?(0M);
                key.RetainageBal = new Decimal?(0M);
              }
            }
            this.APTran_TranType_RefNbr.Update(key);
          }
        }
        if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.projectAccounting>() && !this._IsIntegrityCheck)
        {
          doc.ProjectID = new int?(source1.Count == 1 ? source1.First<int>() : ProjectDefaultAttribute.NonProject().Value);
          this.APDocument.Update(doc);
        }
        bool? nullable23;
        Decimal? nullable24;
        Decimal? nullable25;
        if (!this.IsInvoiceReclassification & flag6)
        {
          nullable23 = doc.Released;
          if (!nullable23.GetValueOrDefault())
          {
            nullable23 = doc.Prebooked;
            if (!nullable23.GetValueOrDefault())
            {
              nullable24 = lineBalances.CashDiscountBalance.Cury;
              nullable25 = doc.CuryOrigDiscAmt;
              if (nullable24.GetValueOrDefault() == nullable25.GetValueOrDefault() & nullable24.HasValue == nullable25.HasValue)
              {
                nullable25 = lineBalances.CashDiscountBalance.Base;
                nullable24 = doc.OrigDiscAmt;
                if (nullable25.GetValueOrDefault() == nullable24.GetValueOrDefault() & nullable25.HasValue == nullable24.HasValue)
                  goto label_207;
              }
              if (tran1 != null)
              {
                APTran apTran6 = tran1;
                nullable24 = apTran6.CuryCashDiscBal;
                Decimal? cury = lineBalances.CashDiscountBalance.Cury;
                Decimal? curyOrigDiscAmt = doc.CuryOrigDiscAmt;
                nullable25 = cury.HasValue & curyOrigDiscAmt.HasValue ? new Decimal?(cury.GetValueOrDefault() - curyOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
                apTran6.CuryCashDiscBal = nullable24.HasValue & nullable25.HasValue ? new Decimal?(nullable24.GetValueOrDefault() - nullable25.GetValueOrDefault()) : new Decimal?();
                APTran apTran7 = tran1;
                nullable25 = apTran7.CashDiscBal;
                Decimal? nullable26 = lineBalances.CashDiscountBalance.Base;
                Decimal? origDiscAmt = doc.OrigDiscAmt;
                nullable24 = nullable26.HasValue & origDiscAmt.HasValue ? new Decimal?(nullable26.GetValueOrDefault() - origDiscAmt.GetValueOrDefault()) : new Decimal?();
                apTran7.CashDiscBal = nullable25.HasValue & nullable24.HasValue ? new Decimal?(nullable25.GetValueOrDefault() - nullable24.GetValueOrDefault()) : new Decimal?();
                this.APTran_TranType_RefNbr.Update(tran1);
              }
label_207:
              if (!this._IsIntegrityCheck)
              {
                nullable24 = lineBalances.RetainageBalance.Cury;
                nullable25 = doc.CuryRetainageTotal;
                if (!(nullable24.GetValueOrDefault() == nullable25.GetValueOrDefault() & nullable24.HasValue == nullable25.HasValue))
                  throw new PXException("The sum of retainage balances of all detail lines is not equal to the document original retainage.");
                nullable25 = lineBalances.RetainageBalance.Base;
                nullable24 = doc.RetainageTotal;
                if (!(nullable25.GetValueOrDefault() == nullable24.GetValueOrDefault() & nullable25.HasValue == nullable24.HasValue) && apTran3 != null)
                {
                  nullable24 = lineBalances.RetainageBalance.Base;
                  nullable25 = doc.RetainageTotal;
                  Decimal? nullable27 = nullable24.HasValue & nullable25.HasValue ? new Decimal?(nullable24.GetValueOrDefault() - nullable25.GetValueOrDefault()) : new Decimal?();
                  APTran apTran8 = tran1;
                  nullable25 = apTran8.OrigRetainageAmt;
                  nullable24 = nullable27;
                  apTran8.OrigRetainageAmt = nullable25.HasValue & nullable24.HasValue ? new Decimal?(nullable25.GetValueOrDefault() - nullable24.GetValueOrDefault()) : new Decimal?();
                  nullable24 = tran1.RetainageBal;
                  Decimal num = 0M;
                  if (!(nullable24.GetValueOrDefault() == num & nullable24.HasValue))
                  {
                    APTran apTran9 = tran1;
                    nullable24 = apTran9.RetainageBal;
                    nullable25 = nullable27;
                    apTran9.RetainageBal = nullable24.HasValue & nullable25.HasValue ? new Decimal?(nullable24.GetValueOrDefault() - nullable25.GetValueOrDefault()) : new Decimal?();
                  }
                  this.APTran_TranType_RefNbr.Update(tran1);
                }
                nullable25 = lineBalances.TranBalance.Cury;
                nullable24 = doc.CuryDocBal;
                if (!(nullable25.GetValueOrDefault() == nullable24.GetValueOrDefault() & nullable25.HasValue == nullable24.HasValue))
                  throw new PXException("The sum of balances of all detail lines is not equal to the document balance.");
                nullable24 = lineBalances.TranBalance.Base;
                nullable25 = doc.DocBal;
                if (!(nullable24.GetValueOrDefault() == nullable25.GetValueOrDefault() & nullable24.HasValue == nullable25.HasValue) && tran1 != null)
                {
                  nullable25 = lineBalances.TranBalance.Base;
                  nullable24 = doc.DocBal;
                  Decimal? baseAmount = nullable25.HasValue & nullable24.HasValue ? new Decimal?(nullable25.GetValueOrDefault() - nullable24.GetValueOrDefault()) : new Decimal?();
                  APTran apTran10 = tran1;
                  nullable24 = apTran10.OrigTranAmt;
                  nullable25 = baseAmount;
                  apTran10.OrigTranAmt = nullable24.HasValue & nullable25.HasValue ? new Decimal?(nullable24.GetValueOrDefault() - nullable25.GetValueOrDefault()) : new Decimal?();
                  nullable25 = tran1.TranBal;
                  Decimal num = 0M;
                  if (!(nullable25.GetValueOrDefault() == num & nullable25.HasValue))
                  {
                    APTran apTran11 = tran1;
                    nullable25 = apTran11.TranBal;
                    nullable24 = baseAmount;
                    apTran11.TranBal = nullable25.HasValue & nullable24.HasValue ? new Decimal?(nullable25.GetValueOrDefault() - nullable24.GetValueOrDefault()) : new Decimal?();
                  }
                  this.APTran_TranType_RefNbr.Update(tran1);
                  if (apInvoice1.IsChildRetainageDocument())
                    this.AdjustOriginalRetainageLineBalance((APRegister) apInvoice1, tran1, new Decimal?(0M), baseAmount);
                }
              }
            }
          }
        }
        InvoiceTransactionsReleasedArgs doc1 = new InvoiceTransactionsReleasedArgs()
        {
          Invoice = apInvoice1,
          IsPrebooking = isPrebooking
        };
        this.InvoiceTransactionsReleased(doc1);
        if (doc1.INDocuments.Any<PX.Objects.IN.INRegister>())
          inDocs.AddRange((IEnumerable<PX.Objects.IN.INRegister>) doc1.INDocuments);
        if (flag1)
        {
          foreach (PXResult<APTaxTran, PX.Objects.TX.Tax, APInvoice> pxResult in this.APTaxTran_TranType_RefNbr.Select((object) apInvoice1.DocType, (object) apInvoice1.RefNbr))
          {
            APTaxTran apTaxTran = (APTaxTran) pxResult;
            PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) pxResult;
            APInvoice apInvoice2 = (APInvoice) pxResult;
            nullable23 = tax.DeductibleVAT;
            if (nullable23.GetValueOrDefault())
            {
              nullable23 = tax.ReportExpenseToSingleAccount;
              if (!nullable23.GetValueOrDefault() && tax.TaxCalcType == "I")
              {
                foreach (PX.Objects.GL.GLTran itemAccount in this.PostTaxExpenseToItemAccounts(je, apInvoice1, ex, apTaxTran, tax, true))
                  APReleaseProcess.Append(glTran9, itemAccount);
              }
            }
            if ((tax.TaxType == "P" || tax.TaxType == "S") && this.IsPostUseAndSalesTaxesByProjectKey((PXGraph) this, tax))
            {
              foreach (PX.Objects.GL.GLTran aSrc in this.PostTaxAmountByProjectKey(je, apInvoice1, ex, apTaxTran, tax, true, true))
                APReleaseProcess.Append(glTran9, aSrc);
            }
          }
          APReleaseProcess.Invert(glTran9);
          PostGraph.NormalizeAmounts(glTran9);
          this.InsertInvoiceTaxTransaction(je, glTran9, new APReleaseProcess.GLTranInsertionContext()
          {
            APRegisterRecord = doc
          });
        }
        else
        {
          foreach (PXResult<APTaxTran, PX.Objects.TX.Tax, APInvoice> pxResult in this.APTaxTran_TranType_RefNbr.Select((object) apInvoice1.DocType, (object) apInvoice1.RefNbr))
          {
            APTaxTran apTaxTran1 = (APTaxTran) pxResult;
            PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) pxResult;
            APInvoice orig_doc = (APInvoice) pxResult;
            if (!(tax.TaxType == "W"))
            {
              nullable23 = tax.DirectTax;
              if (!nullable23.GetValueOrDefault() && (apInvoice1.TaxCalcMode == "G" || tax.TaxCalcLevel == "0" && apInvoice1.TaxCalcMode != "N"))
                docInclTaxDiscrepancy += APReleaseProcess.CalcDocInclTaxDiscrepancyForTran(apTaxTran1, tax);
              nullable23 = tax.DirectTax;
              if (nullable23.GetValueOrDefault() && !string.IsNullOrEmpty(apTaxTran1.OrigRefNbr))
              {
                if (!this._IsIntegrityCheck && !this.IsInvoiceReclassification)
                {
                  if (!orig_doc.CuryInfoID.HasValue)
                    throw new PXException("'{0}' cannot be found in the system.", new object[1]
                    {
                      (object) apTaxTran1.OrigRefNbr
                    });
                  this.PostDirectTax(currencyInfo1, apTaxTran1, orig_doc);
                }
                else
                  continue;
              }
              if (tax.TaxType == "P" || tax.TaxType == "S")
              {
                int num6;
                if (apInvoice1.IsChildRetainageDocument())
                {
                  nullable24 = apTaxTran1.CuryTaxAmt;
                  Decimal num7 = 0M;
                  num6 = !(nullable24.GetValueOrDefault() == num7 & nullable24.HasValue) ? 1 : 0;
                }
                else
                  num6 = 0;
                if (num6 == 0)
                {
                  if (this.IsPostUseAndSalesTaxesByProjectKey((PXGraph) this, tax))
                  {
                    IEnumerable<PX.Objects.GL.GLTran> glTrans = this.PostTaxAmountByProjectKey(je, apInvoice1, ex, apTaxTran1, tax, !isPrebooking && !flag2, true);
                    if (isPrebooking | flag2)
                    {
                      foreach (PX.Objects.GL.GLTran aSrc in glTrans)
                        APReleaseProcess.Append(glTran9, aSrc);
                    }
                  }
                  else
                    this.PostGeneralTax(je, apInvoice1, ex, apTaxTran1, tax, tax.ExpenseAccountID, tax.ExpenseSubID, true);
                }
                else if (tax.TaxType == "S")
                {
                  nullable24 = apTaxTran1.TaxAmt;
                  if (nullable24.GetValueOrDefault() != 0M)
                    this.PostGeneralTax(je, apInvoice1, ex, apTaxTran1, tax, doc.RetainageAcctID, doc.RetainageSubID);
                }
                if (tax.TaxType == "P")
                  this.PostReverseTax(je, apInvoice1, ex, apTaxTran1, tax);
              }
              else if (tax.TaxType == "Q")
              {
                bool isDebitTaxTran = this.IsDebitTaxTran(apTaxTran1);
                this.PostPerUnitTaxAmounts(je, apInvoice1, ex, apTaxTran1, tax, isDebitTaxTran);
              }
              else if (!apInvoice1.IsPrepaymentInvoiceDocument())
              {
                nullable23 = tax.ReverseTax;
                if (!nullable23.GetValueOrDefault())
                  this.PostGeneralTax(je, apInvoice1, ex, apTaxTran1, tax);
                else
                  this.PostReverseTax(je, apInvoice1, ex, apTaxTran1, tax);
                nullable23 = tax.DeductibleVAT;
                if (nullable23.GetValueOrDefault())
                {
                  nullable23 = tax.ReportExpenseToSingleAccount;
                  if (nullable23.GetValueOrDefault())
                    this.PostTaxExpenseToSingleAccount(je, apInvoice1, ex, apTaxTran1, tax);
                  else if (tax.TaxCalcType == "I")
                  {
                    IEnumerable<PX.Objects.GL.GLTran> itemAccounts = this.PostTaxExpenseToItemAccounts(je, apInvoice1, ex, apTaxTran1, tax, !isPrebooking && !flag2);
                    if (isPrebooking | flag2)
                    {
                      foreach (PX.Objects.GL.GLTran aSrc in itemAccounts)
                        APReleaseProcess.Append(glTran9, aSrc);
                    }
                  }
                }
              }
              if (apInvoice1.DocType == "QCK" || apInvoice1.DocType == "VQC" || apInvoice1.DocType == "RQC")
              {
                bool flag9 = !this.IsDebitTaxTran(apTaxTran1);
                JournalEntry je1 = je;
                APInvoice doc2 = apInvoice1;
                Vendor vend = vendor1;
                PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = ex;
                int num = flag9 ? 1 : 0;
                nullable24 = apTaxTran1.CuryTaxDiscountAmt;
                Decimal valueOrDefault1 = nullable24.GetValueOrDefault();
                nullable24 = apTaxTran1.TaxDiscountAmt;
                Decimal valueOrDefault2 = nullable24.GetValueOrDefault();
                this.PostReduceOnEarlyPaymentTran(je1, doc2, vend, currencyInfo2, num != 0, valueOrDefault1, valueOrDefault2);
              }
              apTaxTran1.Released = new bool?(true);
              APTaxTran apTaxTran2 = this.APTaxTran_TranType_RefNbr.Update(apTaxTran1);
              if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vATReporting>() && !this._IsIntegrityCheck && !this.IsInvoiceReclassification && (apTaxTran2.TaxType == "B" || apTaxTran2.TaxType == "A"))
              {
                Vendor vendor2 = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) apTaxTran2.VendorID);
                Decimal multByTranType = ReportTaxProcess.GetMultByTranType("AP", apTaxTran2.TranType);
                string empty = string.Empty;
                string str3;
                switch (apTaxTran2.TranType)
                {
                  case "QCK":
                  case "VQC":
                  case "RQC":
                    str3 = "D";
                    break;
                  case "PPI":
                    str3 = "Y";
                    break;
                  default:
                    str3 = vendor2?.SVATReversalMethod;
                    break;
                }
                List<APRegister> apRegisterList2 = new List<APRegister>()
                {
                  doc
                };
                if (str1 == "M")
                  apRegisterList2 = apRegisterList1;
                Decimal num8 = 0M;
                Decimal num9 = 0M;
                Decimal num10 = 0M;
                Decimal num11 = 0M;
                SVATConversionHist svatConversionHist1 = (SVATConversionHist) null;
                for (int index = 0; index < apRegisterList2.Count; ++index)
                {
                  APRegister apRegister = apRegisterList2[index];
                  SVATConversionHist svatConversionHist2 = new SVATConversionHist()
                  {
                    Module = "AP",
                    AdjdBranchID = apTaxTran2.BranchID,
                    AdjdDocType = apTaxTran2.TranType,
                    AdjdRefNbr = apRegister.RefNbr,
                    AdjgDocType = apTaxTran2.TranType,
                    AdjgRefNbr = apRegister.RefNbr,
                    AdjdDocDate = apRegister.DocDate,
                    TaxID = apTaxTran2.TaxID,
                    TaxType = apTaxTran2.TaxType,
                    TaxRate = apTaxTran2.TaxRate,
                    VendorID = apTaxTran2.VendorID,
                    ReversalMethod = str3,
                    CuryInfoID = apTaxTran2.CuryInfoID
                  };
                  nullable24 = doc.CuryOrigDocAmt;
                  Decimal num12 = 0M;
                  Decimal? nullable28;
                  if (nullable24.GetValueOrDefault() == num12 & nullable24.HasValue)
                  {
                    nullable28 = new Decimal?(0M);
                  }
                  else
                  {
                    nullable24 = apRegister.CuryOrigDocAmt;
                    nullable25 = doc.CuryOrigDocAmt;
                    nullable28 = nullable24.HasValue & nullable25.HasValue ? new Decimal?(nullable24.GetValueOrDefault() / nullable25.GetValueOrDefault()) : new Decimal?();
                  }
                  nullable25 = nullable28;
                  Decimal valueOrDefault = nullable25.GetValueOrDefault();
                  svatConversionHist2.FillAmounts(this.GetExtension<APReleaseProcess.MultiCurrency>().GetCurrencyInfo(apTaxTran2.CuryInfoID), apTaxTran2.CuryTaxableAmt, apTaxTran2.CuryTaxAmt, valueOrDefault * multByTranType);
                  FinPeriodIDAttribute.SetPeriodsByMaster<SVATConversionHist.adjdFinPeriodID>(this.SVATConversionHistory.Cache, (object) svatConversionHist2, doc.TranPeriodID);
                  Decimal num13 = num8;
                  nullable25 = svatConversionHist2.TaxableAmt;
                  Decimal num14 = nullable25.Value;
                  num8 = num13 + num14;
                  Decimal num15 = num9;
                  nullable25 = svatConversionHist2.TaxAmt;
                  Decimal num16 = nullable25.Value;
                  num9 = num15 + num16;
                  Decimal num17 = num10;
                  nullable25 = svatConversionHist2.CuryTaxableAmt;
                  Decimal num18 = nullable25.Value;
                  num10 = num17 + num18;
                  Decimal num19 = num11;
                  nullable25 = svatConversionHist2.CuryTaxAmt;
                  Decimal num20 = nullable25.Value;
                  num11 = num19 + num20;
                  SVATConversionHist svatConversionHist3 = this.SVATConversionHistory.Insert(svatConversionHist2);
                  SVATConversionHist svatConversionHist4;
                  if (svatConversionHist1 != null)
                  {
                    nullable25 = svatConversionHist3.CuryTaxAmt;
                    nullable24 = svatConversionHist1.CuryTaxAmt;
                    if (!(nullable25.GetValueOrDefault() > nullable24.GetValueOrDefault() & nullable25.HasValue & nullable24.HasValue))
                    {
                      svatConversionHist4 = svatConversionHist1;
                      goto label_313;
                    }
                  }
                  svatConversionHist4 = svatConversionHist3;
label_313:
                  svatConversionHist1 = svatConversionHist4;
                }
                nullable25 = apTaxTran2.TaxableAmt;
                Decimal num21 = multByTranType;
                nullable24 = nullable25.HasValue ? new Decimal?(nullable25.GetValueOrDefault() * num21) : new Decimal?();
                Decimal num22 = num8;
                Decimal? nullable29;
                if (!nullable24.HasValue)
                {
                  nullable25 = new Decimal?();
                  nullable29 = nullable25;
                }
                else
                  nullable29 = new Decimal?(nullable24.GetValueOrDefault() - num22);
                Decimal? nullable30 = nullable29;
                nullable25 = apTaxTran2.TaxAmt;
                Decimal num23 = multByTranType;
                nullable24 = nullable25.HasValue ? new Decimal?(nullable25.GetValueOrDefault() * num23) : new Decimal?();
                Decimal num24 = num9;
                Decimal? nullable31;
                if (!nullable24.HasValue)
                {
                  nullable25 = new Decimal?();
                  nullable31 = nullable25;
                }
                else
                  nullable31 = new Decimal?(nullable24.GetValueOrDefault() - num24);
                Decimal? nullable32 = nullable31;
                nullable24 = nullable30;
                Decimal num25 = 0M;
                if (nullable24.GetValueOrDefault() == num25 & nullable24.HasValue)
                {
                  nullable24 = nullable32;
                  Decimal num26 = 0M;
                  if (nullable24.GetValueOrDefault() == num26 & nullable24.HasValue)
                    goto label_324;
                }
                SVATConversionHist svatConversionHist5 = svatConversionHist1;
                nullable24 = svatConversionHist5.TaxableAmt;
                nullable25 = nullable30;
                svatConversionHist5.TaxableAmt = nullable24.HasValue & nullable25.HasValue ? new Decimal?(nullable24.GetValueOrDefault() + nullable25.GetValueOrDefault()) : new Decimal?();
                SVATConversionHist svatConversionHist6 = svatConversionHist1;
                nullable25 = svatConversionHist6.TaxAmt;
                nullable24 = nullable32;
                svatConversionHist6.TaxAmt = nullable25.HasValue & nullable24.HasValue ? new Decimal?(nullable25.GetValueOrDefault() + nullable24.GetValueOrDefault()) : new Decimal?();
                svatConversionHist1.UnrecognizedTaxAmt = svatConversionHist1.TaxAmt;
                svatConversionHist1 = this.SVATConversionHistory.Update(svatConversionHist1);
label_324:
                nullable25 = apTaxTran2.CuryTaxableAmt;
                Decimal num27 = multByTranType;
                nullable24 = nullable25.HasValue ? new Decimal?(nullable25.GetValueOrDefault() * num27) : new Decimal?();
                Decimal num28 = num10;
                Decimal? nullable33;
                if (!nullable24.HasValue)
                {
                  nullable25 = new Decimal?();
                  nullable33 = nullable25;
                }
                else
                  nullable33 = new Decimal?(nullable24.GetValueOrDefault() - num28);
                Decimal? nullable34 = nullable33;
                nullable25 = apTaxTran2.CuryTaxAmt;
                Decimal num29 = multByTranType;
                nullable24 = nullable25.HasValue ? new Decimal?(nullable25.GetValueOrDefault() * num29) : new Decimal?();
                Decimal num30 = num11;
                Decimal? nullable35;
                if (!nullable24.HasValue)
                {
                  nullable25 = new Decimal?();
                  nullable35 = nullable25;
                }
                else
                  nullable35 = new Decimal?(nullable24.GetValueOrDefault() - num30);
                Decimal? nullable36 = nullable35;
                nullable24 = nullable34;
                Decimal num31 = 0M;
                if (nullable24.GetValueOrDefault() == num31 & nullable24.HasValue)
                {
                  nullable24 = nullable36;
                  Decimal num32 = 0M;
                  if (nullable24.GetValueOrDefault() == num32 & nullable24.HasValue)
                    goto label_333;
                }
                SVATConversionHist svatConversionHist7 = svatConversionHist1;
                nullable24 = svatConversionHist7.CuryTaxableAmt;
                nullable25 = nullable34;
                svatConversionHist7.CuryTaxableAmt = nullable24.HasValue & nullable25.HasValue ? new Decimal?(nullable24.GetValueOrDefault() + nullable25.GetValueOrDefault()) : new Decimal?();
                SVATConversionHist svatConversionHist8 = svatConversionHist1;
                nullable25 = svatConversionHist8.CuryTaxAmt;
                nullable24 = nullable36;
                svatConversionHist8.CuryTaxAmt = nullable25.HasValue & nullable24.HasValue ? new Decimal?(nullable25.GetValueOrDefault() + nullable24.GetValueOrDefault()) : new Decimal?();
                svatConversionHist1.CuryUnrecognizedTaxAmt = svatConversionHist1.CuryTaxAmt;
                this.SVATConversionHistory.Update(svatConversionHist1);
              }
label_333:
              if (!this._IsIntegrityCheck && apInvoice1.IsPrepaymentInvoiceDocument())
              {
                string str4 = "Y";
                List<APRegister> apRegisterList3 = new List<APRegister>()
                {
                  doc
                };
                for (int index = 0; index < apRegisterList3.Count; ++index)
                {
                  APRegister apRegister = apRegisterList3[index];
                  SVATConversionHist svatConversionHist = new SVATConversionHist()
                  {
                    Module = "AP",
                    AdjdBranchID = apTaxTran2.BranchID,
                    AdjdDocType = apTaxTran2.TranType,
                    AdjdRefNbr = apRegister.RefNbr,
                    AdjgDocType = apTaxTran2.TranType,
                    AdjgRefNbr = apRegister.RefNbr,
                    AdjdDocDate = apRegister.DocDate,
                    TaxID = apTaxTran2.TaxID,
                    TaxType = apTaxTran2.TaxType,
                    TaxRate = apTaxTran2.TaxRate,
                    VendorID = apTaxTran2.VendorID,
                    ReversalMethod = str4,
                    CuryInfoID = apTaxTran2.CuryInfoID
                  };
                  svatConversionHist.FillAmounts(this.GetExtension<APReleaseProcess.MultiCurrency>().GetCurrencyInfo(apTaxTran2.CuryInfoID), apTaxTran2.CuryTaxableAmt, apTaxTran2.CuryTaxAmt, 1M);
                  FinPeriodIDAttribute.SetPeriodsByMaster<SVATConversionHist.adjdFinPeriodID>(this.SVATConversionHistory.Cache, (object) svatConversionHist, doc.TranPeriodID);
                  this.SVATConversionHistory.Insert(svatConversionHist);
                }
              }
            }
          }
          if (isPrebooking | flag2)
          {
            PostGraph.NormalizeAmounts(glTran9);
            this.InsertInvoiceTaxTransaction(je, glTran9, new APReleaseProcess.GLTranInsertionContext()
            {
              APRegisterRecord = doc
            });
          }
        }
      }
      this.ProcessOriginTranPost(apInvoice1, masterInstallment);
      if (apInvoice1.IsPrepaymentInvoiceDocument())
        this.ProcessPrepaymentTranPost(apInvoice1);
      if (apInvoice1.IsChildRetainageDocument())
        this.ProcessRetainageTranPost(apInvoice1);
      if (!this._IsIntegrityCheck && !this.IsInvoiceReclassification)
      {
        if (doc.IsPrepaymentInvoiceDocumentReverse())
        {
          foreach (PXResult<APAdjust> pxResult in PXSelectBase<APAdjust, PXViewOf<APAdjust>.BasedOn<SelectFromBase<APAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjust.adjgDocType, Equal<P.AsString.ASCII>>>>, PX.Data.And<BqlOperand<APAdjust.adjgRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<APAdjust.released, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr))
          {
            APAdjust apAdjust = (APAdjust) pxResult;
            apAdjust.Hold = new bool?(false);
            this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache.SetStatus((object) apAdjust, PXEntryStatus.Updated);
          }
        }
        else
        {
          foreach (PXResult<APAdjust, APPayment> pxResult in PXSelectBase<APAdjust, PXSelectJoin<APAdjust, InnerJoin<APPayment, On<APPayment.docType, Equal<APAdjust.adjgDocType>, And<APPayment.refNbr, Equal<APAdjust.adjgRefNbr>>>>, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.released, Equal<False>, And<APPayment.released, Equal<True>>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr))
          {
            APAdjust adj = (APAdjust) pxResult;
            APPayment payment = (APPayment) pxResult;
            PXCache<APAdjust>.StoreOriginal((PXGraph) this, adj);
            PXCache<APPayment>.StoreOriginal((PXGraph) this, payment);
            Decimal? curyAdjdAmt = ((APAdjust) pxResult).CuryAdjdAmt;
            Decimal num = 0M;
            if (curyAdjdAmt.GetValueOrDefault() > num & curyAdjdAmt.HasValue)
            {
              if (str1 != null && str1 != "S")
                throw new PXException("No applications can be created for documents with multiple installment credit terms specified.");
              APPayment apPayment = this.SetLatestDateAndPeriodForPayment(adj, payment);
              apRegisterList1.Add((APRegister) apPayment);
              this.APPayment_DocType_RefNbr.Cache.Update((object) apPayment);
              APAdjust apAdjust1 = adj;
              Decimal? adjAmt = apAdjust1.AdjAmt;
              Decimal? rgolAmt1 = adj.RGOLAmt;
              apAdjust1.AdjAmt = adjAmt.HasValue & rgolAmt1.HasValue ? new Decimal?(adjAmt.GetValueOrDefault() + rgolAmt1.GetValueOrDefault()) : new Decimal?();
              APAdjust apAdjust2 = adj;
              Decimal? rgolAmt2 = adj.RGOLAmt;
              Decimal? nullable37 = rgolAmt2.HasValue ? new Decimal?(-rgolAmt2.GetValueOrDefault()) : new Decimal?();
              apAdjust2.RGOLAmt = nullable37;
              adj.Hold = new bool?(false);
              this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache.SetStatus((object) adj, PXEntryStatus.Updated);
            }
          }
        }
        if (doc.DocType == "ADR" && str1 != "M")
          apRegisterList1.Add(doc);
      }
      PX.Objects.GL.Batch current = je.BatchModule.Current;
      this.ReleaseInvoiceBatchPostProcessing(je, apInvoice1, current);
      Decimal? curyDebitTotal1 = current.CuryDebitTotal;
      Decimal? curyCreditTotal = current.CuryCreditTotal;
      Decimal? nullable38 = curyDebitTotal1.HasValue & curyCreditTotal.HasValue ? new Decimal?(curyDebitTotal1.GetValueOrDefault() - curyCreditTotal.GetValueOrDefault()) : new Decimal?();
      Decimal curyCreditDiff = System.Math.Round(nullable38.Value, 4);
      Decimal? debitTotal1 = current.DebitTotal;
      Decimal? creditTotal = current.CreditTotal;
      Decimal? nullable39;
      if (!(debitTotal1.HasValue & creditTotal.HasValue))
      {
        nullable38 = new Decimal?();
        nullable39 = nullable38;
      }
      else
        nullable39 = new Decimal?(debitTotal1.GetValueOrDefault() - creditTotal.GetValueOrDefault());
      nullable38 = nullable39;
      Decimal creditDiff = System.Math.Round(nullable38.Value, 4, MidpointRounding.AwayFromZero);
      if (docInclTaxDiscrepancy != 0M && !apInvoice1.IsPrepaymentInvoiceDocument() && !apInvoice1.IsPrepaymentInvoiceDocumentReverse())
      {
        this.ProcessTaxDiscrepancy(je, current, apInvoice1, ex, docInclTaxDiscrepancy);
        Decimal? curyDebitTotal2 = current.CuryDebitTotal;
        Decimal? nullable40 = current.CuryCreditTotal;
        Decimal? nullable41;
        if (!(curyDebitTotal2.HasValue & nullable40.HasValue))
        {
          nullable38 = new Decimal?();
          nullable41 = nullable38;
        }
        else
          nullable41 = new Decimal?(curyDebitTotal2.GetValueOrDefault() - nullable40.GetValueOrDefault());
        nullable38 = nullable41;
        curyCreditDiff = System.Math.Round(nullable38.Value, 4);
        Decimal? debitTotal2 = current.DebitTotal;
        nullable40 = current.CreditTotal;
        Decimal? nullable42;
        if (!(debitTotal2.HasValue & nullable40.HasValue))
        {
          nullable38 = new Decimal?();
          nullable42 = nullable38;
        }
        else
          nullable42 = new Decimal?(debitTotal2.GetValueOrDefault() - nullable40.GetValueOrDefault());
        nullable38 = nullable42;
        creditDiff = System.Math.Round(nullable38.Value, 4, MidpointRounding.AwayFromZero);
      }
      if (System.Math.Abs(curyCreditDiff) >= 0.00005M)
        this.VerifyRoundingAllowed(apInvoice1, current, je.currencyInfo.BaseCuryID);
      if (System.Math.Abs(curyCreditDiff) >= 0.00005M || System.Math.Abs(creditDiff) >= 0.00005M)
        this.ProcessInvoiceRounding(je, current, apInvoice1, curyCreditDiff, creditDiff, currencyInfo1);
      if (doc.HasZeroBalance<APRegister.curyDocBal, APTran.curyTranBal>((PXGraph) this) && (!doc.IsOriginalRetainageDocument() || doc.HasZeroBalance<APRegister.curyRetainageUnreleasedAmt, APTran.curyRetainageBal>((PXGraph) this)) && (!doc.IsOriginalRetainageDocument() || doc.DocType != "ADR"))
      {
        doc.DocBal = new Decimal?(0M);
        doc.CuryDiscBal = new Decimal?(0M);
        doc.DiscBal = new Decimal?(0M);
        doc.OpenDoc = new bool?(false);
        doc.ClosedDate = doc.DocDate;
        doc.ClosedFinPeriodID = doc.FinPeriodID;
        doc.ClosedTranPeriodID = doc.TranPeriodID;
        this.RaiseInvoiceEvent(doc, PXEntityEventBase<APInvoice>.Container<APInvoice.Events>.Select((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (ev => ev.CloseDocument)));
      }
    }
label_385:
    return apRegisterList1;
  }

  protected virtual APPayment SetLatestDateAndPeriodForPayment(APAdjust adj, APPayment payment)
  {
    payment.AdjDate = ((IEnumerable<System.DateTime?>) new System.DateTime?[2]
    {
      payment.DocDate,
      adj.AdjdDocDate
    }).OrderByDescending<System.DateTime?, System.DateTime?>((Func<System.DateTime?, System.DateTime?>) (_ => _)).First<System.DateTime?>();
    FinPeriodIDAttribute.SetPeriodsByMaster<APPayment.adjFinPeriodID>(this.APPayment_DocType_RefNbr.Cache, (object) payment, ((IEnumerable<string>) new string[2]
    {
      payment.TranPeriodID,
      adj.AdjdTranPeriodID
    }).OrderByDescending<string, string>((Func<string, string>) (_ => _)).First<string>());
    payment = (APPayment) this.APPayment_DocType_RefNbr.Cache.Update((object) payment);
    return payment;
  }

  private static Decimal CalcDocInclTaxDiscrepancyForTran(APTaxTran x, PX.Objects.TX.Tax salestax)
  {
    Decimal? curyTaxAmt = x.CuryTaxAmt;
    Decimal? nullable1 = x.CuryTaxAmtSumm;
    Decimal? nullable2 = curyTaxAmt.HasValue & nullable1.HasValue ? new Decimal?(curyTaxAmt.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = x.CuryRetainedTaxAmt;
    Decimal? nullable4;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable1 = new Decimal?();
      nullable4 = nullable1;
    }
    else
      nullable4 = new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault());
    Decimal? nullable5 = nullable4;
    Decimal? retainedTaxAmtSumm = x.CuryRetainedTaxAmtSumm;
    Decimal? nullable6;
    if (!(nullable5.HasValue & retainedTaxAmtSumm.HasValue))
    {
      nullable3 = new Decimal?();
      nullable6 = nullable3;
    }
    else
      nullable6 = new Decimal?(nullable5.GetValueOrDefault() - retainedTaxAmtSumm.GetValueOrDefault());
    Decimal? nullable7 = nullable6;
    return salestax.ReverseTax.GetValueOrDefault() ? -nullable7.Value : nullable7.Value;
  }

  private void AdjustOriginalRetainageLineBalance(
    APRegister document,
    APTran tran,
    Decimal? curyAmount,
    Decimal? baseAmount)
  {
    APTran originalRetainageLine = this.GetOriginalRetainageLine(document, tran);
    if (originalRetainageLine == null)
      return;
    PXCache<APTran>.StoreOriginal((PXGraph) this, originalRetainageLine);
    Decimal? nullable1 = APDocType.SignAmount(originalRetainageLine.TranType);
    Decimal? signAmount = document.SignAmount;
    Decimal valueOrDefault = (nullable1.HasValue & signAmount.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signAmount.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
    APTran apTran1 = originalRetainageLine;
    Decimal? nullable2 = apTran1.CuryRetainageBal;
    Decimal num1 = curyAmount.GetValueOrDefault() * valueOrDefault;
    apTran1.CuryRetainageBal = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - num1) : new Decimal?();
    APTran apTran2 = originalRetainageLine;
    nullable2 = apTran2.RetainageBal;
    Decimal num2 = baseAmount.GetValueOrDefault() * valueOrDefault;
    apTran2.RetainageBal = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - num2) : new Decimal?();
    APTran apTran3 = this.APTran_TranType_RefNbr.Update(originalRetainageLine);
    if (!this.IsIntegrityCheck)
    {
      APTran tran1 = apTran3;
      nullable2 = tran.CuryOrigTranAmt;
      Decimal num3 = nullable2.GetValueOrDefault() * valueOrDefault;
      this.DecreaseRetainedAmount(tran1, num3);
    }
    nullable2 = apTran3.CuryOrigRetainageAmt;
    Decimal num4 = 0M;
    Sign sign1 = nullable2.GetValueOrDefault() < num4 & nullable2.HasValue ? Sign.Minus : Sign.Plus;
    if (!this._IsIntegrityCheck)
    {
      Decimal? curyRetainageBal1 = apTran3.CuryRetainageBal;
      Sign sign2 = sign1;
      nullable2 = curyRetainageBal1.HasValue ? new Decimal?(Sign.op_Multiply(curyRetainageBal1.GetValueOrDefault(), sign2)) : new Decimal?();
      Decimal num5 = 0M;
      if (!(nullable2.GetValueOrDefault() < num5 & nullable2.HasValue))
      {
        Decimal? curyRetainageBal2 = apTran3.CuryRetainageBal;
        Sign sign3 = sign1;
        nullable2 = curyRetainageBal2.HasValue ? new Decimal?(Sign.op_Multiply(curyRetainageBal2.GetValueOrDefault(), sign3)) : new Decimal?();
        Decimal? origRetainageAmt = apTran3.CuryOrigRetainageAmt;
        Sign sign4 = sign1;
        Decimal? nullable3 = origRetainageAmt.HasValue ? new Decimal?(Sign.op_Multiply(origRetainageAmt.GetValueOrDefault(), sign4)) : new Decimal?();
        if (!(nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue))
          return;
      }
      throw new PXException("The document cannot be released because the retainage has been fully released for the related original document.");
    }
  }

  protected virtual void AdjustmentProcessingOnApplication(APRegister paymentRegister, APAdjust adj)
  {
    if (!paymentRegister.PaymentsByLinesAllowed.GetValueOrDefault())
      return;
    this.ProcessPayByLineDebitAdjAdjustment(paymentRegister, adj);
  }

  protected virtual IComparer<PX.Objects.TX.Tax> GetTaxComparer()
  {
    return (IComparer<PX.Objects.TX.Tax>) TaxByCalculationLevelAndTypeComparer.Instance;
  }

  protected virtual APReleaseProcess.LineBalances AdjustInvoiceDetailsBalanceByLine(
    APRegister doc,
    APTran tran)
  {
    APTran apTran1 = tran;
    Decimal? nullable1 = apTran1.CuryOrigRetainageAmt;
    Decimal? curyRetainageAmt = tran.CuryRetainageAmt;
    apTran1.CuryOrigRetainageAmt = nullable1.HasValue & curyRetainageAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + curyRetainageAmt.GetValueOrDefault()) : new Decimal?();
    APTran apTran2 = tran;
    Decimal? nullable2 = apTran2.OrigRetainageAmt;
    nullable1 = tran.RetainageAmt;
    apTran2.OrigRetainageAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    APTran apTran3 = tran;
    nullable1 = apTran3.CuryOrigTranAmt;
    nullable2 = tran.CuryTranAmt;
    apTran3.CuryOrigTranAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    APTran apTran4 = tran;
    nullable2 = apTran4.OrigTranAmt;
    nullable1 = tran.TranAmt;
    apTran4.OrigTranAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    nullable1 = tran.CuryCashDiscBal;
    Decimal? cury1 = new Decimal?(nullable1.GetValueOrDefault());
    nullable1 = tran.CashDiscBal;
    Decimal? baaase1 = new Decimal?(nullable1.GetValueOrDefault());
    ARReleaseProcess.Amount cashDiscountBalance = new ARReleaseProcess.Amount(cury1, baaase1);
    nullable1 = tran.CuryRetainageAmt;
    Decimal? cury2 = new Decimal?(nullable1.GetValueOrDefault());
    nullable1 = tran.RetainageAmt;
    Decimal? baaase2 = new Decimal?(nullable1.GetValueOrDefault());
    ARReleaseProcess.Amount retainageBalance = new ARReleaseProcess.Amount(cury2, baaase2);
    nullable1 = tran.CuryTranAmt;
    Decimal? cury3 = new Decimal?(nullable1.GetValueOrDefault());
    nullable1 = tran.TranAmt;
    Decimal? baaase3 = new Decimal?(nullable1.GetValueOrDefault());
    ARReleaseProcess.Amount tranBalance = new ARReleaseProcess.Amount(cury3, baaase3);
    return new APReleaseProcess.LineBalances(cashDiscountBalance, retainageBalance, tranBalance);
  }

  protected virtual APReleaseProcess.LineBalances CalculateInvoiceDetailsCashDiscBalance(
    APTran tran,
    APRegister doc)
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
    APTran apTran1 = tran;
    PXCache cache1 = this.APTran_TranType_RefNbr.Cache;
    APTran row1 = tran;
    nullable1 = doc.CuryOrigDiscAmt;
    Decimal val1 = nullable1.GetValueOrDefault() * num2;
    Decimal? nullable2 = new Decimal?(PX.Objects.CM.PXCurrencyAttribute.RoundCury(cache1, (object) row1, val1));
    apTran1.CuryCashDiscBal = nullable2;
    APTran apTran2 = tran;
    PXCache cache2 = this.APTran_TranType_RefNbr.Cache;
    APTran row2 = tran;
    nullable1 = doc.OrigDiscAmt;
    Decimal val2 = nullable1.GetValueOrDefault() * num2;
    Decimal? nullable3 = new Decimal?(PX.Objects.CM.PXCurrencyAttribute.RoundCury(cache2, (object) row2, val2));
    apTran2.CashDiscBal = nullable3;
    nullable1 = tran.CuryCashDiscBal;
    Decimal? cury = new Decimal?(nullable1.GetValueOrDefault());
    nullable1 = tran.CashDiscBal;
    Decimal? baaase = new Decimal?(nullable1.GetValueOrDefault());
    return new APReleaseProcess.LineBalances(new ARReleaseProcess.Amount(cury, baaase), new ARReleaseProcess.Amount(), new ARReleaseProcess.Amount());
  }

  protected virtual void DecreaseRetainedAmount(APTran tran, Decimal value)
  {
    if (!tran.TaskID.HasValue || !(value != 0M))
      return;
    PX.Objects.GL.Account account = (PX.Objects.GL.Account) PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, (object) tran.AccountID);
    if (account == null || !account.AccountGroupID.HasValue)
      return;
    this.AddRetainedAmount(tran, account.AccountGroupID, value * -1M);
  }

  protected virtual void AddRetainedAmount(APTran tran, int? accountGroupID, int mult = 1)
  {
    Decimal num = (Decimal) mult * tran.CuryRetainageAmt.GetValueOrDefault();
    this.AddRetainedAmount(tran, accountGroupID, num);
  }

  protected virtual void AddRetainedAmount(APTran tran, int? accountGroupID, Decimal value)
  {
    if (!(value != 0M))
      return;
    PMBudgetAccum pmBudgetAccum1 = (PMBudgetAccum) this.Caches[typeof (PMBudgetAccum)].Insert((object) this.GetTargetBudget(accountGroupID, tran));
    PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
    Decimal? curyRetainedAmount = pmBudgetAccum2.CuryRetainedAmount;
    Decimal num1 = value;
    pmBudgetAccum2.CuryRetainedAmount = curyRetainedAmount.HasValue ? new Decimal?(curyRetainedAmount.GetValueOrDefault() + num1) : new Decimal?();
    PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
    Decimal? retainedAmount = pmBudgetAccum3.RetainedAmount;
    Decimal num2 = value;
    pmBudgetAccum3.RetainedAmount = retainedAmount.HasValue ? new Decimal?(retainedAmount.GetValueOrDefault() + num2) : new Decimal?();
  }

  private PMBudgetAccum GetTargetBudget(int? accountGroupID, APTran line)
  {
    PMAccountGroup ag = (PMAccountGroup) PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup, Where<PMAccountGroup.groupID, Equal<Required<PMAccountGroup.groupID>>>>.Config>.Select((PXGraph) this, (object) accountGroupID);
    PX.Objects.PM.PMProject project = (PX.Objects.PM.PMProject) PXSelectBase<PX.Objects.PM.PMProject, PXSelect<PX.Objects.PM.PMProject, Where<PX.Objects.PM.PMProject.contractID, Equal<Required<PX.Objects.PM.PMProject.contractID>>>>.Config>.Select((PXGraph) this, (object) line.ProjectID);
    PX.Objects.PM.Lite.PMBudget pmBudget = new BudgetService((PXGraph) this).SelectProjectBalance(ag, project, line.TaskID, line.InventoryID, line.CostCodeID, out bool _);
    PMBudgetAccum targetBudget = new PMBudgetAccum();
    targetBudget.Type = pmBudget.Type;
    targetBudget.ProjectID = pmBudget.ProjectID;
    targetBudget.ProjectTaskID = pmBudget.TaskID;
    targetBudget.AccountGroupID = pmBudget.AccountGroupID;
    targetBudget.InventoryID = pmBudget.InventoryID;
    targetBudget.CostCodeID = pmBudget.CostCodeID;
    targetBudget.UOM = pmBudget.UOM;
    targetBudget.Description = pmBudget.Description;
    targetBudget.CuryInfoID = project.CuryInfoID;
    if (targetBudget.Type == "E")
    {
      PX.Objects.PM.PMTask pmTask = PX.Objects.PM.PMTask.PK.Find((PXGraph) this, targetBudget.ProjectID, targetBudget.TaskID);
      if (pmTask != null && pmTask.Type == "CostRev")
        targetBudget.RevenueTaskID = targetBudget.ProjectTaskID;
    }
    return targetBudget;
  }

  public static bool IncludeTaxInLineBalance(PX.Objects.TX.Tax tax)
  {
    return tax != null && tax.TaxType != "P" && tax.TaxType != "W" && tax.TaxCalcLevel != "0";
  }

  protected virtual APReleaseProcess.LineBalances AdjustInvoiceDetailsBalanceByTax(
    APRegister doc,
    APTran tran,
    APTax aptax,
    PX.Objects.TX.Tax tax)
  {
    int num1 = aptax == null || aptax.TaxID == null ? 0 : (APReleaseProcess.IncludeTaxInLineBalance(tax) ? 1 : 0);
    bool flag = aptax != null && aptax.TaxID != null && tax != null && tax.TaxType != "P";
    Decimal num2 = tax.ReverseTax.GetValueOrDefault() ? -1M : 1M;
    Decimal? nullable = aptax.CuryTaxAmt;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = aptax.CuryExpenseAmt;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    Decimal num3 = valueOrDefault1 + valueOrDefault2;
    nullable = aptax.TaxAmt;
    Decimal valueOrDefault3 = nullable.GetValueOrDefault();
    nullable = aptax.ExpenseAmt;
    Decimal valueOrDefault4 = nullable.GetValueOrDefault();
    Decimal num4 = valueOrDefault3 + valueOrDefault4;
    Decimal num5 = num3 * num2;
    Decimal num6 = num4 * num2;
    nullable = aptax.CuryRetainedTaxAmt;
    Decimal valueOrDefault5 = nullable.GetValueOrDefault();
    nullable = aptax.RetainedTaxAmt;
    Decimal valueOrDefault6 = nullable.GetValueOrDefault();
    Decimal num7 = valueOrDefault5 * num2;
    Decimal num8 = valueOrDefault6 * num2;
    APReleaseProcess.LineBalances lineBalances = num1 != 0 ? new APReleaseProcess.LineBalances(new ARReleaseProcess.Amount(new Decimal?(0M), new Decimal?(0M)), new ARReleaseProcess.Amount(new Decimal?(num7), new Decimal?(num8)), new ARReleaseProcess.Amount(new Decimal?(num5), new Decimal?(num6))) : new APReleaseProcess.LineBalances(new Decimal?(0M));
    APTran apTran1 = tran;
    nullable = apTran1.CuryRetainedTaxableAmt;
    Decimal valueOrDefault7 = flag ? aptax.CuryRetainedTaxableAmt.GetValueOrDefault() : 0M;
    apTran1.CuryRetainedTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault7) : new Decimal?();
    APTran apTran2 = tran;
    nullable = apTran2.RetainedTaxableAmt;
    Decimal valueOrDefault8 = flag ? aptax.RetainedTaxableAmt.GetValueOrDefault() : 0M;
    apTran2.RetainedTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault8) : new Decimal?();
    APTran apTran3 = tran;
    nullable = apTran3.CuryRetainedTaxAmt;
    Decimal num9 = flag ? num7 : 0M;
    apTran3.CuryRetainedTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num9) : new Decimal?();
    APTran apTran4 = tran;
    nullable = apTran4.RetainedTaxAmt;
    Decimal num10 = flag ? num8 : 0M;
    apTran4.RetainedTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num10) : new Decimal?();
    APTran apTran5 = tran;
    nullable = apTran5.CuryOrigRetainageAmt;
    Decimal? cury1 = lineBalances.RetainageBalance.Cury;
    apTran5.CuryOrigRetainageAmt = nullable.HasValue & cury1.HasValue ? new Decimal?(nullable.GetValueOrDefault() + cury1.GetValueOrDefault()) : new Decimal?();
    APTran apTran6 = tran;
    Decimal? origRetainageAmt = apTran6.OrigRetainageAmt;
    nullable = lineBalances.RetainageBalance.Base;
    apTran6.OrigRetainageAmt = origRetainageAmt.HasValue & nullable.HasValue ? new Decimal?(origRetainageAmt.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
    APTran apTran7 = tran;
    nullable = apTran7.CuryOrigTaxableAmt;
    Decimal valueOrDefault9 = flag ? aptax.CuryTaxableAmt.GetValueOrDefault() : 0M;
    apTran7.CuryOrigTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault9) : new Decimal?();
    APTran apTran8 = tran;
    nullable = apTran8.OrigTaxableAmt;
    Decimal valueOrDefault10 = flag ? aptax.TaxableAmt.GetValueOrDefault() : 0M;
    apTran8.OrigTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault10) : new Decimal?();
    APTran apTran9 = tran;
    nullable = apTran9.CuryOrigTaxAmt;
    Decimal num11 = flag ? num5 : 0M;
    apTran9.CuryOrigTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num11) : new Decimal?();
    APTran apTran10 = tran;
    nullable = apTran10.OrigTaxAmt;
    Decimal num12 = flag ? num6 : 0M;
    apTran10.OrigTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num12) : new Decimal?();
    APTran apTran11 = tran;
    nullable = apTran11.CuryOrigTranAmt;
    Decimal? cury2 = lineBalances.TranBalance.Cury;
    apTran11.CuryOrigTranAmt = nullable.HasValue & cury2.HasValue ? new Decimal?(nullable.GetValueOrDefault() + cury2.GetValueOrDefault()) : new Decimal?();
    APTran apTran12 = tran;
    Decimal? origTranAmt = apTran12.OrigTranAmt;
    nullable = lineBalances.TranBalance.Base;
    apTran12.OrigTranAmt = origTranAmt.HasValue & nullable.HasValue ? new Decimal?(origTranAmt.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
    return lineBalances;
  }

  [Obsolete("This validation is needed for landed cost upgrade case, when AP Bill with LC trans is not released (see AC-111467, remove in 2020R2).")]
  protected virtual void ValidateLandedCostTran(APRegister doc, PXResultset<APTran> apTranResultSet)
  {
    if (!apTranResultSet.RowCast<APTran>().Any<APTran>((Func<APTran, bool>) (t => !string.IsNullOrEmpty(t.LCRefNbr))))
      return;
    if (new PXSelectJoin<POLandedCostDoc, InnerJoin<APTran, On<APTran.lCDocType, Equal<POLandedCostDoc.docType>, And<APTran.lCRefNbr, Equal<POLandedCostDoc.refNbr>>>>, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>, And<POLandedCostDoc.released, Equal<False>>>>>((PXGraph) this).SelectWindowed(0, 1, (object) doc.DocType, (object) doc.RefNbr).Count > 0)
      throw new PXException("The landed cost document must be released.");
  }

  protected virtual void CheckVoidQuickCheckAmountDiscrepancies(APRegister document)
  {
    if (!(document.DocType == "VQC"))
      return;
    APRegister apRegister = APRegister.PK.Find((PXGraph) this, "QCK", document.RefNbr);
    Decimal? origDocAmt1 = document.OrigDocAmt;
    Decimal? origDocAmt2 = apRegister.OrigDocAmt;
    if (!(origDocAmt1.GetValueOrDefault() == origDocAmt2.GetValueOrDefault() & origDocAmt1.HasValue == origDocAmt2.HasValue))
      throw new ReleaseException("The {0} {1} cannot be released because its amount differs from the amount of the document being voided.", new object[2]
      {
        (object) document.RefNbr,
        (object) APDocType.GetDisplayName(document.DocType)
      });
  }

  private void ProcessInvoiceRounding(
    JournalEntry je,
    PX.Objects.GL.Batch apbatch,
    APInvoice apdoc,
    Decimal curyCreditDiff,
    Decimal creditDiff,
    PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    PX.Objects.CM.Extensions.Currency currency = (PX.Objects.CM.Extensions.Currency) PXSelectBase<PX.Objects.CM.Extensions.Currency, PXSelect<PX.Objects.CM.Extensions.Currency, Where<PX.Objects.CM.Extensions.Currency.curyID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyID>>>>.Config>.Select((PXGraph) this, (object) apdoc.CuryID);
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
      glTran.BranchID = apdoc.BranchID;
      if (System.Math.Sign(curyCreditDiff) == 1)
      {
        glTran.AccountID = currency.RoundingGainAcctID;
        glTran.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingGainSubID>((PXGraph) je, glTran.BranchID, currency);
        glTran.CuryDebitAmt = new Decimal?(0M);
        glTran.CuryCreditAmt = new Decimal?(System.Math.Abs(curyCreditDiff));
      }
      else
      {
        glTran.AccountID = currency.RoundingLossAcctID;
        glTran.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingLossSubID>((PXGraph) je, glTran.BranchID, currency);
        glTran.CuryDebitAmt = new Decimal?(System.Math.Abs(curyCreditDiff));
        glTran.CuryCreditAmt = new Decimal?(0M);
      }
      glTran.CreditAmt = new Decimal?(0M);
      glTran.DebitAmt = new Decimal?(0M);
      glTran.TranType = apdoc.DocType;
      glTran.RefNbr = apdoc.RefNbr;
      glTran.TranClass = "N";
      glTran.TranDesc = "Rounding difference";
      glTran.LedgerID = apbatch.LedgerID;
      FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran, apbatch.TranPeriodID);
      glTran.TranDate = apdoc.DocDate;
      glTran.ReferenceID = apdoc.VendorID;
      glTran.Released = new bool?(true);
      glTran.CuryInfoID = ex.CuryInfoID;
      this.InsertInvoiceRoundingTransaction(je, glTran, new APReleaseProcess.GLTranInsertionContext()
      {
        APRegisterRecord = (APRegister) apdoc
      });
    }
    if (!(creditDiff != 0M))
      return;
    PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
    glTran1.SummPost = new bool?(true);
    glTran1.BranchID = apdoc.BranchID;
    if (System.Math.Sign(creditDiff) == 1)
    {
      glTran1.AccountID = currency.RoundingGainAcctID;
      glTran1.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingGainSubID>((PXGraph) je, glTran1.BranchID, currency);
      glTran1.CreditAmt = new Decimal?(System.Math.Abs(creditDiff));
      glTran1.DebitAmt = new Decimal?(0M);
    }
    else
    {
      glTran1.AccountID = currency.RoundingLossAcctID;
      glTran1.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingLossSubID>((PXGraph) je, glTran1.BranchID, currency);
      glTran1.CreditAmt = new Decimal?(0M);
      glTran1.DebitAmt = new Decimal?(System.Math.Abs(creditDiff));
    }
    glTran1.CuryCreditAmt = new Decimal?(0M);
    glTran1.CuryDebitAmt = new Decimal?(0M);
    glTran1.TranType = apdoc.DocType;
    glTran1.RefNbr = apdoc.RefNbr;
    glTran1.TranClass = "N";
    glTran1.TranDesc = "Rounding difference";
    glTran1.LedgerID = apbatch.LedgerID;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran1, apbatch.TranPeriodID);
    glTran1.TranDate = apdoc.DocDate;
    glTran1.ReferenceID = apdoc.VendorID;
    glTran1.Released = new bool?(true);
    glTran1.CuryInfoID = ex.CuryInfoID;
    this.InsertInvoiceRoundingTransaction(je, glTran1, new APReleaseProcess.GLTranInsertionContext()
    {
      APRegisterRecord = (APRegister) apdoc
    });
  }

  protected virtual void ProcessTaxDiscrepancy(
    JournalEntry je,
    PX.Objects.GL.Batch arbatch,
    APInvoice apdoc,
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo,
    Decimal docInclTaxDiscrepancy)
  {
    if (docInclTaxDiscrepancy == 0M)
      return;
    Decimal? roundingLimit = CurrencyCollection.GetCurrency(currencyInfo.BaseCuryID).RoundingLimit;
    Decimal num1 = System.Math.Abs(docInclTaxDiscrepancy);
    Decimal? nullable1 = roundingLimit;
    Decimal valueOrDefault = nullable1.GetValueOrDefault();
    if (num1 > valueOrDefault & nullable1.HasValue && (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>() || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.invoiceRounding>()))
      throw new PXException("The amount to be posted to the rounding account ({1} {0}) exceeds the limit ({2} {0}) specified on the Currencies (CM202000) form.", new object[3]
      {
        (object) je.currencyinfo.Current.BaseCuryID,
        (object) System.Math.Abs(System.Math.Round(docInclTaxDiscrepancy, (int) (currencyInfo.CuryPrecision ?? (short) 4))),
        (object) PXDBQuantityAttribute.Round(roundingLimit)
      });
    TXSetup txSetup = (TXSetup) PXSetup<TXSetup>.Select((PXGraph) this, Array.Empty<object>());
    int? nullable2;
    int num2;
    if (txSetup == null)
    {
      num2 = 1;
    }
    else
    {
      nullable2 = txSetup.TaxRoundingGainAcctID;
      num2 = !nullable2.HasValue ? 1 : 0;
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
        nullable2 = txSetup.TaxRoundingLossAcctID;
        num3 = !nullable2.HasValue ? 1 : 0;
      }
      if (num3 == 0)
      {
        int? nullable3 = docInclTaxDiscrepancy > 0M ? txSetup.TaxRoundingGainAcctID : txSetup.TaxRoundingLossAcctID;
        int? nullable4 = docInclTaxDiscrepancy > 0M ? txSetup.TaxRoundingGainSubID : txSetup.TaxRoundingLossSubID;
        bool flag = apdoc.DrCr == "D";
        PX.Objects.GL.GLTran tran = new PX.Objects.GL.GLTran()
        {
          SummPost = new bool?(this.SummPost),
          BranchID = apdoc.BranchID,
          CuryInfoID = currencyInfo.CuryInfoID,
          TranType = apdoc.DocType,
          TranClass = "R",
          RefNbr = apdoc.RefNbr,
          TranDate = apdoc.DocDate,
          AccountID = nullable3,
          SubID = nullable4,
          TranDesc = "Tax rounding difference",
          CuryDebitAmt = new Decimal?(!flag ? docInclTaxDiscrepancy : 0M),
          DebitAmt = new Decimal?(!flag ? currencyInfo.CuryConvBase(docInclTaxDiscrepancy) : 0M),
          CuryCreditAmt = new Decimal?(!flag ? 0M : docInclTaxDiscrepancy),
          CreditAmt = new Decimal?(!flag ? 0M : currencyInfo.CuryConvBase(docInclTaxDiscrepancy)),
          Released = new bool?(true),
          ReferenceID = apdoc.VendorID
        };
        this.InsertInvoiceTransaction(je, tran, new APReleaseProcess.GLTranInsertionContext()
        {
          APRegisterRecord = (APRegister) apdoc
        });
        return;
      }
    }
    throw new PXException("Tax rounding gain and loss accounts cannot be empty. Specify these accounts on the Tax Preferences (TX103000) form.");
  }

  /// <summary>
  /// Extension point for AP Release Invoice process. This method is called after GL Batch was created and all main GL Transactions have been
  /// inserted, but before Invoice rounding transaction, or RGOL transaction has been inserted.
  /// </summary>
  /// <param name="je">Journal Entry graph used for posting</param>
  /// <param name="apdoc">Orginal AP Invoice</param>
  /// <param name="apbatch">GL Batch that was created for Invoice</param>
  public virtual void ReleaseInvoiceBatchPostProcessing(
    JournalEntry je,
    APInvoice apdoc,
    PX.Objects.GL.Batch apbatch)
  {
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public virtual void ReleaseInvoiceTransactionPostProcessing(
    JournalEntry je,
    APInvoice apdoc,
    PXResult<APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem> r,
    PX.Objects.GL.GLTran tran)
  {
  }

  /// <summary>
  /// Extension point for AP Release Invoice process. This method is called after transaction amounts have been calculated, but before it was inserted.
  /// </summary>
  /// <param name="je">Journal Entry graph used for posting</param>
  /// <param name="apdoc">Orginal AP Invoice</param>
  /// <param name="r">Document line with joined supporting entities</param>
  /// <param name="tran">Transaction that was created for APTran. This transaction has not been inserted yet.</param>
  public virtual void ReleaseInvoiceTransactionPostProcessing(
    JournalEntry je,
    APInvoice apdoc,
    PXResult<APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem, APTaxTran> r,
    PX.Objects.GL.GLTran tran)
  {
    PXResult<APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem> r1 = new PXResult<APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem>((APTran) r, (APTax) r, (PX.Objects.TX.Tax) r, (DRDeferredCode) r, (LandedCostCode) r, (PX.Objects.IN.InventoryItem) r);
    this.ReleaseInvoiceTransactionPostProcessing(je, apdoc, r1, tran);
  }

  /// <summary>
  /// Extension point for AP Release Invoice process. This method is called after transaction amounts have been calculated, but before it was inserted.
  /// </summary>
  public virtual void InvoiceTransactionReleasing(
    InvoiceTransactionReleasingArgs invoiceTransactionReleasing)
  {
  }

  /// <summary>
  /// Extension point for AP Release Invoice process. This method is called after transactions have been processed for release
  /// </summary>
  public virtual void InvoiceTransactionsReleased(InvoiceTransactionsReleasedArgs doc)
  {
  }

  protected virtual IEnumerable<PX.Objects.GL.GLTran> PostTaxExpenseToItemAccounts(
    JournalEntry je,
    APInvoice apdoc,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    APTaxTran x,
    PX.Objects.TX.Tax salestax,
    bool doInsert)
  {
    Decimal? nullable1;
    if (apdoc.IsOriginalRetainageDocument())
    {
      nullable1 = apdoc.CuryRetainedTaxTotal;
      Decimal num = 0M;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
        throw new PXException("Partially deductible VATs with the cleared Use Tax Expense Account check box on the Taxes (TX205000) form are not supported in Accounts Payable bills with retainage.");
    }
    PXResultset<APTax> deductibleLines = this.GetDeductibleLines(salestax, x);
    if (!apdoc.PaymentsByLinesAllowed.GetValueOrDefault())
    {
      APTaxAttribute apTaxAttribute = new APTaxAttribute(typeof (APRegister), typeof (APTax), typeof (APTaxTran));
      apTaxAttribute.Inventory = typeof (APTran.inventoryID);
      apTaxAttribute.UOM = typeof (APTran.uOM);
      apTaxAttribute.LineQty = typeof (APTran.qty);
      IEnumerable<APTax> firstTableItems = deductibleLines.FirstTableItems;
      nullable1 = x.CuryExpenseAmt;
      Decimal CuryTaxAmt = nullable1.Value;
      apTaxAttribute.DistributeTaxDiscrepancy<APTax, APTax.curyExpenseAmt, APTax.expenseAmt>((PXGraph) this, firstTableItems, CuryTaxAmt);
    }
    List<PX.Objects.GL.GLTran> itemAccounts = new List<PX.Objects.GL.GLTran>();
    bool flag = this.IsDebitTaxTran(x);
    foreach (PXResult<APTax, APTran> pxResult in deductibleLines)
    {
      APTax apTax = (APTax) pxResult;
      APTran apTran1 = (APTran) pxResult;
      PX.Objects.PM.Lite.PMProject project;
      PX.Objects.PM.Lite.PMTask task;
      this.TryToGetProjectAndTask((PXResult) pxResult, out project, out task);
      PX.Objects.GL.GLTran tran = new PX.Objects.GL.GLTran();
      tran.SummPost = new bool?(this.SummPost);
      tran.BranchID = apTran1.BranchID;
      tran.CuryInfoID = new_info.CuryInfoID;
      tran.TranType = x.TranType;
      tran.TranClass = "T";
      tran.RefNbr = x.RefNbr;
      tran.TranDate = x.TranDate;
      int? accountID;
      int? subID;
      this.GetItemCostTaxAccount((APRegister) apdoc, salestax, apTran1, x, project, task, out accountID, out subID);
      tran.AccountID = accountID;
      tran.SubID = subID;
      tran.TranDesc = salestax.TaxID;
      tran.TranLineNbr = apTran1.LineNbr;
      tran.CuryDebitAmt = flag ? apTax.CuryExpenseAmt : new Decimal?(0M);
      tran.DebitAmt = flag ? apTax.ExpenseAmt : new Decimal?(0M);
      tran.CuryCreditAmt = flag ? new Decimal?(0M) : apTax.CuryExpenseAmt;
      tran.CreditAmt = flag ? new Decimal?(0M) : apTax.ExpenseAmt;
      tran.Released = new bool?(true);
      tran.ReferenceID = apdoc.VendorID;
      tran.ProjectID = apTran1.ProjectID;
      tran.TaskID = apTran1.TaskID;
      tran.CostCodeID = apTran1.CostCodeID;
      itemAccounts.Add(tran);
      if (doInsert)
      {
        this.InsertInvoiceTaxExpenseItemAccountsTransaction(je, tran, new APReleaseProcess.GLTranInsertionContext()
        {
          APRegisterRecord = (APRegister) apdoc,
          APTranRecord = apTran1,
          APTaxTranRecord = x
        });
        APTran apTran2 = this.APTran_TranType_RefNbr.Locate(apTran1) ?? apTran1;
        APTran apTran3 = apTran2;
        nullable1 = apTran3.ExpenseAmt;
        Decimal? nullable2 = apTax.ExpenseAmt;
        apTran3.ExpenseAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        APTran apTran4 = apTran2;
        nullable2 = apTran4.CuryExpenseAmt;
        nullable1 = apTax.CuryExpenseAmt;
        apTran4.CuryExpenseAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        this.APTran_TranType_RefNbr.Update(apTran2);
      }
    }
    return (IEnumerable<PX.Objects.GL.GLTran>) itemAccounts;
  }

  private bool IsPostUseAndSalesTaxesByProjectKey(PXGraph graph, PX.Objects.TX.Tax tax)
  {
    bool flag = true;
    if (tax.ReportExpenseToSingleAccount.GetValueOrDefault())
    {
      PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find(graph, tax.ExpenseAccountID);
      flag = account != null && account.AccountGroupID.HasValue;
    }
    return flag;
  }

  protected virtual IEnumerable<PX.Objects.GL.GLTran> PostTaxAmountByProjectKey(
    JournalEntry je,
    APInvoice apDoc,
    PX.Objects.CM.Extensions.CurrencyInfo curyInfo,
    APTaxTran apTaxTran,
    PX.Objects.TX.Tax tax,
    bool doInsert,
    bool addRetTaxAmt = false)
  {
    bool flag = this.IsDebitTaxTran(apTaxTran);
    PXResultset<APTax> deductibleLines = this.GetDeductibleLines(tax, apTaxTran);
    Decimal? nullable1;
    if (!apDoc.PaymentsByLinesAllowed.GetValueOrDefault())
    {
      APTaxAttribute apTaxAttribute = new APTaxAttribute(typeof (APRegister), typeof (APTax), typeof (APTaxTran));
      apTaxAttribute.Inventory = typeof (APTran.inventoryID);
      apTaxAttribute.UOM = typeof (APTran.uOM);
      apTaxAttribute.LineQty = typeof (APTran.qty);
      IEnumerable<APTax> firstTableItems = deductibleLines.FirstTableItems;
      nullable1 = apTaxTran.CuryTaxAmt;
      Decimal CuryTaxAmt = nullable1.Value;
      apTaxAttribute.DistributeTaxDiscrepancy<APTax, APTax.curyTaxAmt, APTax.taxAmt>((PXGraph) this, firstTableItems, CuryTaxAmt);
    }
    bool? nullable2;
    if (addRetTaxAmt)
    {
      nullable2 = apDoc.PaymentsByLinesAllowed;
      if (!nullable2.GetValueOrDefault())
      {
        APRetainedTaxAttribute retainedTaxAttribute = new APRetainedTaxAttribute(typeof (APRegister), typeof (APTax), typeof (APTaxTran));
        IEnumerable<APTax> firstTableItems = deductibleLines.FirstTableItems;
        nullable1 = apTaxTran.CuryRetainedTaxAmt;
        Decimal CuryTaxAmt = nullable1.Value;
        retainedTaxAttribute.DistributeTaxDiscrepancy<APTax, APTax.curyRetainedTaxAmt, APTax.retainedTaxAmt>((PXGraph) this, firstTableItems, CuryTaxAmt, true);
      }
    }
    Dictionary<ProjectKey, PX.Objects.GL.GLTran> dictionary1 = new Dictionary<ProjectKey, PX.Objects.GL.GLTran>();
    Dictionary<int?, APTran> dictionary2 = new Dictionary<int?, APTran>();
    foreach (PXResult<APTax, APTran> pxResult in deductibleLines)
    {
      APTax apTax = (APTax) pxResult;
      APTran apTran = (APTran) pxResult;
      PX.Objects.PM.Lite.PMProject project;
      PX.Objects.PM.Lite.PMTask task;
      this.TryToGetProjectAndTask((PXResult) pxResult, out project, out task);
      int? accountID = tax.ExpenseAccountID;
      int? subID = tax.ExpenseSubID;
      nullable2 = tax.ReportExpenseToSingleAccount;
      if (!nullable2.GetValueOrDefault())
        this.GetItemCostTaxAccount((APRegister) apDoc, tax, apTran, apTaxTran, project, task, out accountID, out subID);
      ProjectKey key = new ProjectKey(apTran.BranchID, accountID, subID, apTran.ProjectID, apTran.TaskID, apTran.CostCodeID, apTran.InventoryID, apTran.NonBillable);
      nullable1 = apTax.CuryTaxAmt;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      Decimal num1;
      if (!addRetTaxAmt)
      {
        num1 = 0M;
      }
      else
      {
        nullable1 = apTax.CuryRetainedTaxAmt;
        num1 = nullable1.GetValueOrDefault();
      }
      Decimal num2 = valueOrDefault1 + num1;
      nullable1 = apTax.TaxAmt;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal num3;
      if (!addRetTaxAmt)
      {
        num3 = 0M;
      }
      else
      {
        nullable1 = apTax.RetainedTaxAmt;
        num3 = nullable1.GetValueOrDefault();
      }
      Decimal num4 = valueOrDefault2 + num3;
      PX.Objects.GL.GLTran glTran1;
      if (dictionary1.TryGetValue(key, out glTran1))
      {
        glTran1.TranLineNbr = new int?();
        PX.Objects.GL.GLTran glTran2 = glTran1;
        nullable1 = glTran1.CuryDebitAmt;
        Decimal? nullable3 = new Decimal?(nullable1.GetValueOrDefault() + (flag ? num2 : 0M));
        glTran2.CuryDebitAmt = nullable3;
        PX.Objects.GL.GLTran glTran3 = glTran1;
        nullable1 = glTran1.DebitAmt;
        Decimal? nullable4 = new Decimal?(nullable1.GetValueOrDefault() + (flag ? num4 : 0M));
        glTran3.DebitAmt = nullable4;
        PX.Objects.GL.GLTran glTran4 = glTran1;
        nullable1 = glTran1.CuryCreditAmt;
        Decimal? nullable5 = new Decimal?(nullable1.GetValueOrDefault() + (flag ? 0M : num2));
        glTran4.CuryCreditAmt = nullable5;
        PX.Objects.GL.GLTran glTran5 = glTran1;
        nullable1 = glTran1.CreditAmt;
        Decimal? nullable6 = new Decimal?(nullable1.GetValueOrDefault() + (flag ? 0M : num4));
        glTran5.CreditAmt = nullable6;
      }
      else
      {
        glTran1 = new PX.Objects.GL.GLTran();
        glTran1.SummPost = new bool?(this.SummPost);
        glTran1.BranchID = apTran.BranchID;
        glTran1.CuryInfoID = curyInfo.CuryInfoID;
        glTran1.TranType = apTaxTran.TranType;
        glTran1.TranClass = "T";
        glTran1.RefNbr = apTaxTran.RefNbr;
        glTran1.TranDate = apTaxTran.TranDate;
        glTran1.AccountID = accountID;
        glTran1.SubID = subID;
        glTran1.TranDesc = tax.TaxID;
        glTran1.TranLineNbr = apTran.LineNbr;
        glTran1.CuryDebitAmt = new Decimal?(flag ? num2 : 0M);
        glTran1.DebitAmt = new Decimal?(flag ? num4 : 0M);
        glTran1.CuryCreditAmt = new Decimal?(flag ? 0M : num2);
        glTran1.CreditAmt = new Decimal?(flag ? 0M : num4);
        glTran1.Released = new bool?(true);
        glTran1.ReferenceID = apDoc.VendorID;
        glTran1.ProjectID = apTran.ProjectID;
        glTran1.TaskID = apTran.TaskID;
        glTran1.CostCodeID = apTran.CostCodeID;
        glTran1.NonBillable = apTran.NonBillable;
        glTran1.InventoryID = apTran.InventoryID;
        dictionary1.Add(key, glTran1);
        dictionary2.Add(apTran.LineNbr, apTran);
      }
    }
    if (doInsert)
    {
      foreach (ProjectKey key in dictionary1.Keys.ToList<ProjectKey>())
      {
        PX.Objects.GL.GLTran tran = dictionary1[key];
        APTran apTran;
        dictionary2.TryGetValue(new int?(tran.TranLineNbr ?? -1), out apTran);
        dictionary1[key] = this.InsertInvoiceTaxByProjectKeyTransaction(je, tran, new APReleaseProcess.GLTranInsertionContext()
        {
          APRegisterRecord = (APRegister) apDoc,
          APTaxTranRecord = apTaxTran,
          APTranRecord = apTran
        });
      }
    }
    return (IEnumerable<PX.Objects.GL.GLTran>) dictionary1.Values;
  }

  protected virtual PXResultset<APTax> GetDeductibleLines(PX.Objects.TX.Tax salestax, APTaxTran x)
  {
    return PXSelectBase<APTax, PXSelectJoin<APTax, InnerJoin<APTran, On<APTax.tranType, Equal<APTran.tranType>, And<APTax.refNbr, Equal<APTran.refNbr>, And<APTax.lineNbr, Equal<APTran.lineNbr>>>>>, Where<APTax.taxID, Equal<Required<APTax.taxID>>, And<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>>>>, PX.Data.OrderBy<Desc<APTax.curyTaxAmt>>>.Config>.Select((PXGraph) this, (object) salestax.TaxID, (object) x.TranType, (object) x.RefNbr);
  }

  protected virtual void TryToGetProjectAndTask(
    PXResult item,
    out PX.Objects.PM.Lite.PMProject project,
    out PX.Objects.PM.Lite.PMTask task)
  {
    project = (PX.Objects.PM.Lite.PMProject) null;
    task = (PX.Objects.PM.Lite.PMTask) null;
  }

  protected virtual void PostTaxExpenseToSingleAccount(
    JournalEntry je,
    APInvoice apdoc,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    APTaxTran x,
    PX.Objects.TX.Tax salestax)
  {
    bool flag = this.IsDebitTaxTran(x);
    PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran()
    {
      SummPost = new bool?(this.SummPost),
      BranchID = x.BranchID,
      CuryInfoID = new_info.CuryInfoID,
      TranType = x.TranType,
      TranClass = "T",
      RefNbr = x.RefNbr,
      TranDate = x.TranDate,
      AccountID = salestax.ExpenseAccountID,
      SubID = salestax.ExpenseSubID,
      TranDesc = salestax.TaxID,
      CuryDebitAmt = flag ? x.CuryExpenseAmt : new Decimal?(0M),
      DebitAmt = flag ? x.ExpenseAmt : new Decimal?(0M),
      CuryCreditAmt = flag ? new Decimal?(0M) : x.CuryExpenseAmt,
      CreditAmt = flag ? new Decimal?(0M) : x.ExpenseAmt,
      Released = new bool?(true),
      ReferenceID = apdoc.VendorID,
      ProjectID = ProjectDefaultAttribute.NonProject(),
      CostCodeID = CostCodeAttribute.DefaultCostCode
    };
    this.InsertInvoiceTaxExpenseSingeAccountTransaction(je, glTran, new APReleaseProcess.GLTranInsertionContext()
    {
      APRegisterRecord = (APRegister) apdoc,
      APTaxTranRecord = x
    });
    if (!apdoc.IsChildRetainageDocument())
      return;
    this.PostRetainedTax(je, apdoc, glTran, x, salestax);
  }

  protected virtual void PostReverseTax(
    JournalEntry je,
    APInvoice apdoc,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    APTaxTran x,
    PX.Objects.TX.Tax salestax)
  {
    bool flag = this.IsDebitTaxTran(x);
    PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran()
    {
      SummPost = new bool?(this.SummPost),
      BranchID = x.BranchID,
      CuryInfoID = new_info.CuryInfoID,
      TranType = x.TranType,
      TranClass = "T",
      RefNbr = x.RefNbr,
      TranDate = x.TranDate,
      AccountID = x.AccountID,
      SubID = x.SubID,
      TranDesc = salestax.TaxID,
      CuryDebitAmt = flag ? new Decimal?(0M) : x.CuryTaxAmt,
      DebitAmt = flag ? new Decimal?(0M) : x.TaxAmt,
      CuryCreditAmt = flag ? x.CuryTaxAmt : new Decimal?(0M),
      CreditAmt = flag ? x.TaxAmt : new Decimal?(0M),
      Released = new bool?(true),
      ReferenceID = apdoc.VendorID,
      ProjectID = ProjectDefaultAttribute.NonProject(),
      CostCodeID = CostCodeAttribute.DefaultCostCode
    };
    this.InsertInvoiceReverseTaxTransaction(je, glTran, new APReleaseProcess.GLTranInsertionContext()
    {
      APRegisterRecord = (APRegister) apdoc,
      APTaxTranRecord = x
    });
    this.PostRetainedTax(je, apdoc, glTran, x, salestax, true);
  }

  protected virtual void PostGeneralTax(
    JournalEntry je,
    APInvoice apdoc,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    APTaxTran x,
    PX.Objects.TX.Tax salestax)
  {
    this.PostGeneralTax(je, apdoc, new_info, x, salestax, x.AccountID, x.SubID);
  }

  protected virtual void PostGeneralTax(
    JournalEntry je,
    APInvoice apdoc,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    APTaxTran x,
    PX.Objects.TX.Tax salestax,
    int? accountID = null,
    int? subID = null,
    bool addRetTaxAmount = false)
  {
    bool flag = this.IsDebitTaxTran(x);
    Decimal? nullable = x.CuryTaxAmt;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    Decimal num1;
    if (!addRetTaxAmount)
    {
      num1 = 0M;
    }
    else
    {
      nullable = x.CuryRetainedTaxAmt;
      num1 = nullable.GetValueOrDefault();
    }
    Decimal num2 = valueOrDefault1 + num1;
    nullable = x.TaxAmt;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    Decimal num3;
    if (!addRetTaxAmount)
    {
      num3 = 0M;
    }
    else
    {
      nullable = x.RetainedTaxAmt;
      num3 = nullable.GetValueOrDefault();
    }
    Decimal num4 = valueOrDefault2 + num3;
    PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran()
    {
      SummPost = new bool?(this.SummPost),
      BranchID = x.BranchID,
      CuryInfoID = new_info.CuryInfoID,
      TranType = x.TranType,
      TranClass = "T",
      RefNbr = x.RefNbr,
      TranDate = x.TranDate,
      AccountID = accountID ?? x.AccountID,
      SubID = subID ?? x.SubID,
      TranDesc = salestax.TaxID,
      CuryDebitAmt = new Decimal?(flag ? num2 : 0M),
      DebitAmt = new Decimal?(flag ? num4 : 0M),
      CuryCreditAmt = new Decimal?(flag ? 0M : num2),
      CreditAmt = new Decimal?(flag ? 0M : num4),
      Released = new bool?(true),
      ReferenceID = apdoc.VendorID,
      ProjectID = ProjectDefaultAttribute.NonProject(),
      CostCodeID = CostCodeAttribute.DefaultCostCode
    };
    this.InsertInvoiceGeneralTaxTransaction(je, glTran, new APReleaseProcess.GLTranInsertionContext()
    {
      APRegisterRecord = (APRegister) apdoc,
      APTaxTranRecord = x
    });
    this.PostRetainedTax(je, apdoc, glTran, x, salestax);
  }

  public virtual bool IsDebitTaxTran(APTaxTran x)
  {
    return APReleaseProcess.GetTaxDrCr(x.OrigTranType, x.TranType) == "D";
  }

  protected virtual void PostRetainedTax(
    JournalEntry je,
    APInvoice apdoc,
    PX.Objects.GL.GLTran origTran,
    APTaxTran x,
    PX.Objects.TX.Tax salestax,
    bool isReversedTran = false)
  {
    bool valueOrDefault = salestax.ReverseTax.GetValueOrDefault();
    bool flag1 = salestax.TaxType == "P";
    bool flag2 = salestax.TaxType == "S";
    int num1;
    if (apdoc.IsOriginalRetainageDocument())
    {
      Decimal? curyRetainedTaxAmt = x.CuryRetainedTaxAmt;
      Decimal num2 = 0M;
      if (!(curyRetainedTaxAmt.GetValueOrDefault() == num2 & curyRetainedTaxAmt.HasValue))
      {
        num1 = !flag2 ? 1 : 0;
        goto label_4;
      }
    }
    num1 = 0;
label_4:
    int num3;
    if (apdoc.IsChildRetainageDocument())
    {
      Decimal? curyTaxAmt = x.CuryTaxAmt;
      Decimal num4 = 0M;
      if (!(curyTaxAmt.GetValueOrDefault() == num4 & curyTaxAmt.HasValue))
      {
        num3 = !flag2 ? 1 : 0;
        goto label_8;
      }
    }
    num3 = 0;
label_8:
    bool flag3 = num3 != 0;
    if ((num1 | (flag3 ? 1 : 0)) != 0)
      this.RetainageTaxCheck(salestax);
    if (num1 != 0)
    {
      if (flag1 & isReversedTran)
      {
        bool flag4 = this.IsDebitTaxTran(x) && !valueOrDefault;
        PX.Objects.GL.GLTran copy = PXCache<PX.Objects.GL.GLTran>.CreateCopy(origTran);
        copy.ReclassificationProhibited = new bool?(true);
        copy.AccountID = salestax.RetainageTaxPayableAcctID;
        copy.SubID = salestax.RetainageTaxPayableSubID;
        copy.CuryDebitAmt = flag4 ? new Decimal?(0M) : x.CuryRetainedTaxAmt;
        copy.DebitAmt = flag4 ? new Decimal?(0M) : x.RetainedTaxAmt;
        copy.CuryCreditAmt = flag4 ? x.CuryRetainedTaxAmt : new Decimal?(0M);
        copy.CreditAmt = flag4 ? x.RetainedTaxAmt : new Decimal?(0M);
        je.GLTranModuleBatNbr.Insert(copy);
      }
      else
      {
        if (flag1)
          return;
        bool flag5 = this.IsDebitTaxTran(x) && !valueOrDefault;
        PX.Objects.GL.GLTran copy = PXCache<PX.Objects.GL.GLTran>.CreateCopy(origTran);
        copy.ReclassificationProhibited = new bool?(true);
        copy.AccountID = !valueOrDefault ? salestax.RetainageTaxClaimableAcctID : salestax.RetainageTaxPayableAcctID;
        copy.SubID = !valueOrDefault ? salestax.RetainageTaxClaimableSubID : salestax.RetainageTaxPayableSubID;
        copy.CuryDebitAmt = flag5 ? x.CuryRetainedTaxAmt : new Decimal?(0M);
        copy.DebitAmt = flag5 ? x.RetainedTaxAmt : new Decimal?(0M);
        copy.CuryCreditAmt = flag5 ? new Decimal?(0M) : x.CuryRetainedTaxAmt;
        copy.CreditAmt = flag5 ? new Decimal?(0M) : x.RetainedTaxAmt;
        je.GLTranModuleBatNbr.Insert(copy);
      }
    }
    else
    {
      if (!flag3)
        return;
      if (flag1 & isReversedTran)
      {
        PX.Objects.GL.GLTran copy = PXCache<PX.Objects.GL.GLTran>.CreateCopy(origTran);
        copy.ReclassificationProhibited = new bool?(true);
        copy.AccountID = salestax.RetainageTaxPayableAcctID;
        copy.SubID = salestax.RetainageTaxPayableSubID;
        copy.CuryDebitAmt = origTran.CuryCreditAmt;
        copy.DebitAmt = origTran.CreditAmt;
        copy.CuryCreditAmt = origTran.CuryDebitAmt;
        copy.CreditAmt = origTran.DebitAmt;
        je.GLTranModuleBatNbr.Insert(copy);
      }
      else
      {
        PX.Objects.GL.GLTran copy1 = PXCache<PX.Objects.GL.GLTran>.CreateCopy(origTran);
        copy1.ReclassificationProhibited = new bool?(true);
        copy1.AccountID = !valueOrDefault ? salestax.RetainageTaxClaimableAcctID : salestax.RetainageTaxPayableAcctID;
        copy1.SubID = !valueOrDefault ? salestax.RetainageTaxClaimableSubID : salestax.RetainageTaxPayableSubID;
        copy1.CuryDebitAmt = origTran.CuryCreditAmt;
        copy1.DebitAmt = origTran.CreditAmt;
        copy1.CuryCreditAmt = origTran.CuryDebitAmt;
        copy1.CreditAmt = origTran.DebitAmt;
        je.GLTranModuleBatNbr.Insert(copy1);
        PX.Objects.GL.GLTran glTran = je.GLTranModuleBatNbr.Cache.Inserted.Cast<PX.Objects.GL.GLTran>().FirstOrDefault<PX.Objects.GL.GLTran>((Func<PX.Objects.GL.GLTran, bool>) (d =>
        {
          if (!(d.RefNbr == apdoc.RefNbr))
            return false;
          int? accountId = d.AccountID;
          int? retainageAcctId = apdoc.RetainageAcctID;
          return accountId.GetValueOrDefault() == retainageAcctId.GetValueOrDefault() & accountId.HasValue == retainageAcctId.HasValue;
        }));
        Decimal? nullable = je.Caches[typeof (PX.Objects.GL.Batch)].Current is PX.Objects.GL.Batch current ? current.DebitTotal : new Decimal?();
        Decimal? creditTotal = (Decimal?) current?.CreditTotal;
        Decimal num5 = (nullable.HasValue & creditTotal.HasValue ? new Decimal?(nullable.GetValueOrDefault() - creditTotal.GetValueOrDefault()) : new Decimal?()).Value;
        if (glTran != null && glTran.ReclassificationProhibited.GetValueOrDefault())
        {
          nullable = apdoc.CuryTaxRoundDiff;
          if (!(System.Math.Abs(System.Math.Round(nullable.Value - num5, 4)) >= 0.00005M))
            return;
        }
        PX.Objects.GL.GLTran copy2 = PXCache<PX.Objects.GL.GLTran>.CreateCopy(origTran);
        copy2.ReclassificationProhibited = new bool?(true);
        copy2.SummPost = new bool?(true);
        copy2.AccountID = apdoc.RetainageAcctID;
        copy2.SubID = apdoc.RetainageSubID;
        copy2.CuryDebitAmt = origTran.CuryDebitAmt;
        copy2.DebitAmt = origTran.DebitAmt;
        copy2.CuryCreditAmt = origTran.CuryCreditAmt;
        copy2.CreditAmt = origTran.CreditAmt;
        je.GLTranModuleBatNbr.Insert(copy2);
      }
    }
  }

  protected virtual void PostDirectTax(PX.Objects.CM.Extensions.CurrencyInfo info, APTaxTran x, APInvoice orig_doc)
  {
    APTaxTran data = (APTaxTran) PXSelectBase<APTaxTran, PXSelect<APTaxTran, Where<APTaxTran.tranType, Equal<Required<APTaxTran.tranType>>, And<APTaxTran.refNbr, Equal<Required<APTaxTran.refNbr>>, And<APTaxTran.taxID, Equal<Required<APTaxTran.taxID>>, And<APTaxTran.module, Equal<BatchModule.moduleAP>>>>>>.Config>.Select((PXGraph) this, (object) x.OrigTranType, (object) x.OrigRefNbr, (object) x.TaxID);
    if (data == null)
    {
      APTaxTran copy = PXCache<APTaxTran>.CreateCopy(x);
      copy.TranType = x.OrigTranType;
      copy.RefNbr = x.OrigRefNbr;
      copy.OrigTranType = (string) null;
      copy.OrigRefNbr = (string) null;
      copy.CuryInfoID = orig_doc.CuryInfoID;
      copy.TaxableAmt = new Decimal?(0M);
      copy.CuryTaxableAmt = new Decimal?(0M);
      copy.TaxAmt = new Decimal?(0M);
      copy.CuryTaxAmt = new Decimal?(0M);
      copy.Released = new bool?(true);
      copy.TranDate = x.TranDate;
      copy.FinPeriodID = x.FinPeriodID;
      data = PXCache<APTaxTran>.CreateCopy(this.APTaxTran_TranType_RefNbr.Insert(copy));
    }
    if (!string.IsNullOrEmpty(data.TaxPeriodID))
      throw new PXException("Cannot adjust tax for Closed or Prepared period '{0}'.", new object[1]
      {
        this.APTaxTran_TranType_RefNbr.Cache.GetValueExt<APTaxTran.taxPeriodID>((object) data)
      });
    Decimal num1 = data.TranType == "ADR" && x.TranType != "ADR" || data.TranType != "ADR" && x.TranType == "ADR" ? -1M : 1M;
    data.TaxZoneID = x.TaxZoneID;
    APTaxTran apTaxTran1 = data;
    Decimal? nullable1 = apTaxTran1.CuryTaxableAmt;
    Decimal? nullable2 = x.CuryTaxableAmt;
    Decimal num2 = num1;
    Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num2) : new Decimal?();
    Decimal? nullable4;
    if (!(nullable1.HasValue & nullable3.HasValue))
    {
      nullable2 = new Decimal?();
      nullable4 = nullable2;
    }
    else
      nullable4 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
    apTaxTran1.CuryTaxableAmt = nullable4;
    APTaxTran apTaxTran2 = data;
    nullable3 = apTaxTran2.CuryTaxAmt;
    nullable2 = x.CuryTaxAmt;
    Decimal num3 = num1;
    nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num3) : new Decimal?();
    Decimal? nullable5;
    if (!(nullable3.HasValue & nullable1.HasValue))
    {
      nullable2 = new Decimal?();
      nullable5 = nullable2;
    }
    else
      nullable5 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
    apTaxTran2.CuryTaxAmt = nullable5;
    APTaxTran apTaxTran3 = data;
    nullable1 = apTaxTran3.TaxableAmt;
    nullable2 = x.TaxableAmt;
    Decimal num4 = num1;
    nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num4) : new Decimal?();
    Decimal? nullable6;
    if (!(nullable1.HasValue & nullable3.HasValue))
    {
      nullable2 = new Decimal?();
      nullable6 = nullable2;
    }
    else
      nullable6 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
    apTaxTran3.TaxableAmt = nullable6;
    APTaxTran apTaxTran4 = data;
    nullable3 = apTaxTran4.TaxAmt;
    nullable2 = x.TaxAmt;
    Decimal num5 = num1;
    nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num5) : new Decimal?();
    Decimal? nullable7;
    if (!(nullable3.HasValue & nullable1.HasValue))
    {
      nullable2 = new Decimal?();
      nullable7 = nullable2;
    }
    else
      nullable7 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
    apTaxTran4.TaxAmt = nullable7;
    APTaxTran apTaxTran5 = data;
    nullable1 = apTaxTran5.CuryRetainedTaxableAmt;
    nullable2 = x.CuryRetainedTaxableAmt;
    Decimal num6 = num1;
    nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num6) : new Decimal?();
    Decimal? nullable8;
    if (!(nullable1.HasValue & nullable3.HasValue))
    {
      nullable2 = new Decimal?();
      nullable8 = nullable2;
    }
    else
      nullable8 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
    apTaxTran5.CuryRetainedTaxableAmt = nullable8;
    APTaxTran apTaxTran6 = data;
    nullable3 = apTaxTran6.CuryRetainedTaxAmt;
    nullable2 = x.CuryRetainedTaxAmt;
    Decimal num7 = num1;
    nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num7) : new Decimal?();
    Decimal? nullable9;
    if (!(nullable3.HasValue & nullable1.HasValue))
    {
      nullable2 = new Decimal?();
      nullable9 = nullable2;
    }
    else
      nullable9 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
    apTaxTran6.CuryRetainedTaxAmt = nullable9;
    APTaxTran apTaxTran7 = data;
    nullable1 = apTaxTran7.RetainedTaxableAmt;
    nullable2 = x.RetainedTaxableAmt;
    Decimal num8 = num1;
    nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num8) : new Decimal?();
    Decimal? nullable10;
    if (!(nullable1.HasValue & nullable3.HasValue))
    {
      nullable2 = new Decimal?();
      nullable10 = nullable2;
    }
    else
      nullable10 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
    apTaxTran7.RetainedTaxableAmt = nullable10;
    APTaxTran apTaxTran8 = data;
    nullable3 = apTaxTran8.RetainedTaxAmt;
    nullable2 = x.RetainedTaxAmt;
    Decimal num9 = num1;
    nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num9) : new Decimal?();
    Decimal? nullable11;
    if (!(nullable3.HasValue & nullable1.HasValue))
    {
      nullable2 = new Decimal?();
      nullable11 = nullable2;
    }
    else
      nullable11 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
    apTaxTran8.RetainedTaxAmt = nullable11;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) this.CurrencyInfo_CuryInfoID.Select((object) data.CuryInfoID);
    if (currencyInfo1 != null && !string.Equals(currencyInfo1.CuryID, info.CuryID))
    {
      APTaxTran apTaxTran9 = data;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = currencyInfo1;
      nullable1 = data.TaxableAmt;
      Decimal baseval1 = nullable1.Value;
      Decimal? nullable12 = new Decimal?(currencyInfo2.CuryConvCury(baseval1));
      apTaxTran9.CuryTaxableAmt = nullable12;
      APTaxTran apTaxTran10 = data;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo1;
      nullable1 = data.TaxAmt;
      Decimal baseval2 = nullable1.Value;
      Decimal? nullable13 = new Decimal?(currencyInfo3.CuryConvCury(baseval2));
      apTaxTran10.CuryTaxAmt = nullable13;
    }
    this.APTaxTran_TranType_RefNbr.Update(data);
  }

  protected virtual void RetainageTaxCheck(PX.Objects.TX.Tax tax)
  {
    if (tax.TaxType == "P")
    {
      this.TaxAccountCheck<PX.Objects.TX.Tax.retainageTaxPayableAcctID>(tax);
      this.TaxAccountCheck<PX.Objects.TX.Tax.retainageTaxPayableSubID>(tax);
    }
    else
    {
      this.TaxAccountCheck<PX.Objects.TX.Tax.retainageTaxClaimableAcctID>(tax);
      this.TaxAccountCheck<PX.Objects.TX.Tax.retainageTaxClaimableSubID>(tax);
    }
  }

  private void TaxAccountCheck<Field>(PX.Objects.TX.Tax tax) where Field : IBqlField
  {
    if (this.Caches[BqlCommand.GetItemType(typeof (Field))].GetValue((object) tax, typeof (Field).Name) == null)
      throw new ReleaseException("The document cannot be released because the {0} is not specified for the {1} tax. To proceed, specify the account on the Taxes (TX205000) form.", new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName<Field>(this.Caches[typeof (PX.Objects.TX.Tax)]),
        (object) tax.TaxID
      });
  }

  public virtual void ProcessOriginTranPost(APInvoice doc, bool masterInstallment)
  {
    if (doc.DocType == "PPM")
      return;
    APTranPost apTranPost1 = this.CreateTranPost((APRegister) doc);
    apTranPost1.Type = "S";
    apTranPost1.CuryAmt = doc.CuryOrigDocAmt;
    apTranPost1.Amt = doc.OrigDocAmt;
    apTranPost1.CuryRetainageAmt = doc.CuryRetainageTotal;
    apTranPost1.RetainageAmt = doc.RetainageTotal;
    apTranPost1.CuryWhTaxAmt = doc.CuryOrigWhTaxAmt;
    apTranPost1.CuryDiscAmt = doc.CuryOrigDiscAmt;
    apTranPost1.WhTaxAmt = doc.OrigWhTaxAmt;
    apTranPost1.DiscAmt = doc.OrigDiscAmt;
    apTranPost1.RGOLAmt = doc.RGOLAmt;
    apTranPost1.TranRefNbr = doc.DocType == "PPM" ? (string) null : doc.MasterRefNbr ?? apTranPost1.TranRefNbr;
    if (this.IsNeedUpdateHistoryForTransaction(apTranPost1.TranPeriodID))
      apTranPost1 = this.TranPost.Insert(apTranPost1);
    if (masterInstallment)
    {
      apTranPost1.AccountID = new int?();
      apTranPost1.SubID = new int?();
      this.ProcessInstallmentTranPost(doc);
    }
    if ((doc.DocType == "INV" || doc.DocType == "ACR") && doc.Voided.GetValueOrDefault())
      this.ProcessVoidTranPost((APRegister) doc);
    if (doc.IsPrepaymentInvoiceDocument())
      apTranPost1.TranClass = "PPM" + "P";
    if (doc.IsRetainageReversing.GetValueOrDefault())
    {
      APTranPost tranPost = this.CreateTranPost((APRegister) doc);
      tranPost.SourceDocType = doc.OrigDocType;
      tranPost.SourceRefNbr = doc.OrigRefNbr;
      tranPost.Type = "U";
      tranPost.CuryRetainageAmt = doc.CuryRetainageTotal;
      tranPost.RetainageAmt = doc.RetainageTotal;
      this.TranPost.Insert(tranPost);
      tranPost.DocType = doc.OrigDocType;
      tranPost.RefNbr = doc.OrigRefNbr;
      tranPost.SourceDocType = doc.DocType;
      tranPost.SourceRefNbr = doc.RefNbr;
      this.TranPost.Insert(tranPost);
    }
    if (!doc.IsPrepaymentInvoiceDocumentReverse())
      return;
    APTranPost copy = (APTranPost) this.TranPost.Cache.CreateCopy((object) apTranPost1);
    copy.ID = new int?();
    copy.AccountID = doc.PrepaymentAccountID;
    copy.SubID = doc.PrepaymentSubID;
    APTranPost apTranPost2 = copy;
    short? glSign = apTranPost1.GLSign;
    short? nullable = glSign.HasValue ? new short?(-glSign.GetValueOrDefault()) : new short?();
    apTranPost2.GLSign = nullable;
    this.TranPost.Insert(copy);
  }

  public virtual void ProcessOriginTranPost(APPayment doc)
  {
    APTranPost tranPost = this.CreateTranPost((APRegister) doc);
    tranPost.Type = "S";
    tranPost.CuryAmt = doc.CuryOrigDocAmt;
    tranPost.Amt = doc.OrigDocAmt;
    if (!this.IsNeedUpdateHistoryForTransaction(tranPost.TranPeriodID))
      return;
    this.TranPost.Insert(tranPost);
  }

  public virtual void ProcessPrepaymentTranPost(APInvoice doc)
  {
    APTranPost tranPost = this.CreateTranPost((APRegister) doc);
    tranPost.TranClass = doc.DocType + "P";
    tranPost.Type = "S";
    tranPost.AccountID = doc.PrepaymentAccountID;
    tranPost.SubID = doc.PrepaymentSubID;
    tranPost.CuryAmt = doc.CuryOrigDocAmt;
    tranPost.Amt = doc.OrigDocAmt;
    tranPost.GLSign = new short?((short) -1);
    if (!this.IsNeedUpdateHistoryForTransaction(tranPost.FinPeriodID))
      return;
    this.TranPost.Insert(tranPost);
  }

  public virtual void ProcessRetainageTranPost(APInvoice doc)
  {
    if (doc.DocType == "PPM")
      return;
    if (doc.PaymentsByLinesAllowed.GetValueOrDefault())
    {
      IEqualityComparer<APTran> comparer = (IEqualityComparer<APTran>) new FieldSubsetEqualityComparer<APTran>(this.APTran_TranType_RefNbr.Cache, new System.Type[3]
      {
        typeof (APTran.tranType),
        typeof (APTran.refNbr),
        typeof (APTran.lineNbr)
      });
      foreach (IGrouping<APTran, PXResult<APTran>> grouping in this.APTran_TranType_RefNbr.Select((object) doc.DocType, (object) doc.RefNbr).AsEnumerable<PXResult<APTran>>().GroupBy<PXResult<APTran>, APTran>((Func<PXResult<APTran>, APTran>) (row => (APTran) row), comparer))
      {
        APTran key = grouping.Key;
        APTranPost tranPost = this.CreateTranPost((APRegister) doc);
        tranPost.DocType = doc.OrigDocType;
        tranPost.RefNbr = doc.OrigRefNbr;
        tranPost.LineNbr = key.OrigLineNbr;
        tranPost.Type = "F";
        APTranPost apTranPost1 = tranPost;
        Decimal? nullable1 = key.CuryTranBal;
        Decimal? nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
        apTranPost1.CuryRetainageAmt = nullable2;
        APTranPost apTranPost2 = tranPost;
        nullable1 = key.TranBal;
        Decimal? nullable3 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
        apTranPost2.RetainageAmt = nullable3;
        if (this.IsNeedUpdateHistoryForTransaction(tranPost.TranPeriodID))
          this.TranPost.Insert(tranPost);
      }
    }
    else
    {
      APTranPost tranPost = this.CreateTranPost((APRegister) doc);
      tranPost.DocType = doc.OrigDocType;
      tranPost.RefNbr = doc.OrigRefNbr;
      tranPost.Type = "F";
      APTranPost apTranPost3 = tranPost;
      Decimal? nullable4 = doc.CuryOrigDocAmt;
      Decimal? nullable5 = nullable4.HasValue ? new Decimal?(-nullable4.GetValueOrDefault()) : new Decimal?();
      apTranPost3.CuryRetainageAmt = nullable5;
      APTranPost apTranPost4 = tranPost;
      nullable4 = doc.OrigDocAmt;
      Decimal? nullable6 = nullable4.HasValue ? new Decimal?(-nullable4.GetValueOrDefault()) : new Decimal?();
      apTranPost4.RetainageAmt = nullable6;
      if (!this.IsNeedUpdateHistoryForTransaction(tranPost.TranPeriodID))
        return;
      this.TranPost.Insert(tranPost);
    }
  }

  public virtual void ProcessInstallmentTranPost(APInvoice doc)
  {
    APTranPost tranPost = this.CreateTranPost((APRegister) doc);
    tranPost.Type = "I";
    APTranPost apTranPost1 = tranPost;
    Decimal? curyOrigDocAmt = doc.CuryOrigDocAmt;
    Decimal? nullable1 = curyOrigDocAmt.HasValue ? new Decimal?(-curyOrigDocAmt.GetValueOrDefault()) : new Decimal?();
    apTranPost1.CuryAmt = nullable1;
    APTranPost apTranPost2 = tranPost;
    Decimal? origDocAmt = doc.OrigDocAmt;
    Decimal? nullable2 = origDocAmt.HasValue ? new Decimal?(-origDocAmt.GetValueOrDefault()) : new Decimal?();
    apTranPost2.Amt = nullable2;
    if (!this.IsNeedUpdateHistoryForTransaction(tranPost.TranPeriodID))
      return;
    this.TranPost.Insert(tranPost);
  }

  public virtual void ProcessVoidPaymentTranPost(APRegister doc, ARReleaseProcess.Amount docBal)
  {
    APTranPost tranPost1 = this.CreateTranPost(doc);
    APTranPost tranPost2 = this.CreateTranPost(doc);
    tranPost1.DocType = doc.OrigDocType ?? this.GetHistTranType(doc.DocType, doc.RefNbr);
    tranPost1.RefNbr = doc.OrigRefNbr ?? doc.RefNbr;
    tranPost2.SourceDocType = tranPost1.DocType;
    tranPost2.SourceRefNbr = tranPost1.RefNbr;
    tranPost1.Type = tranPost2.Type = "V";
    APTranPost apTranPost1 = tranPost1;
    Decimal? cury = docBal.Cury;
    Decimal? nullable1 = cury.HasValue ? new Decimal?(-cury.GetValueOrDefault()) : new Decimal?();
    apTranPost1.CuryAmt = nullable1;
    APTranPost apTranPost2 = tranPost1;
    cury = docBal.Base;
    Decimal? nullable2 = cury.HasValue ? new Decimal?(-cury.GetValueOrDefault()) : new Decimal?();
    apTranPost2.Amt = nullable2;
    tranPost2.CuryAmt = docBal.Cury;
    tranPost2.Amt = docBal.Base;
    if (tranPost1.DocType != null && this.IsNeedUpdateHistoryForTransaction(tranPost1.TranPeriodID))
      this.TranPost.Insert(tranPost1);
    if (tranPost2.DocType == null || !this.IsNeedUpdateHistoryForTransaction(tranPost1.TranPeriodID))
      return;
    this.TranPost.Insert(tranPost2);
  }

  public virtual void ProcessVoidTranPost(APRegister doc)
  {
    APTranPost tranPost = this.CreateTranPost(doc);
    tranPost.Type = "V";
    APTranPost apTranPost1 = tranPost;
    Decimal? curyOrigDocAmt = doc.CuryOrigDocAmt;
    Decimal? nullable1 = curyOrigDocAmt.HasValue ? new Decimal?(-curyOrigDocAmt.GetValueOrDefault()) : new Decimal?();
    apTranPost1.CuryAmt = nullable1;
    APTranPost apTranPost2 = tranPost;
    Decimal? origDocAmt = doc.OrigDocAmt;
    Decimal? nullable2 = origDocAmt.HasValue ? new Decimal?(-origDocAmt.GetValueOrDefault()) : new Decimal?();
    apTranPost2.Amt = nullable2;
    APTranPost apTranPost3 = tranPost;
    Decimal? curyRetainageTotal = doc.CuryRetainageTotal;
    Decimal? nullable3 = curyRetainageTotal.HasValue ? new Decimal?(-curyRetainageTotal.GetValueOrDefault()) : new Decimal?();
    apTranPost3.CuryRetainageAmt = nullable3;
    APTranPost apTranPost4 = tranPost;
    Decimal? retainageTotal = doc.RetainageTotal;
    Decimal? nullable4 = retainageTotal.HasValue ? new Decimal?(-retainageTotal.GetValueOrDefault()) : new Decimal?();
    apTranPost4.RetainageAmt = nullable4;
    APTranPost apTranPost5 = tranPost;
    Decimal? curyOrigWhTaxAmt = doc.CuryOrigWhTaxAmt;
    Decimal? nullable5 = curyOrigWhTaxAmt.HasValue ? new Decimal?(-curyOrigWhTaxAmt.GetValueOrDefault()) : new Decimal?();
    apTranPost5.CuryWhTaxAmt = nullable5;
    APTranPost apTranPost6 = tranPost;
    Decimal? curyOrigDiscAmt = doc.CuryOrigDiscAmt;
    Decimal? nullable6 = curyOrigDiscAmt.HasValue ? new Decimal?(-curyOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
    apTranPost6.CuryDiscAmt = nullable6;
    APTranPost apTranPost7 = tranPost;
    Decimal? origWhTaxAmt = doc.OrigWhTaxAmt;
    Decimal? nullable7 = origWhTaxAmt.HasValue ? new Decimal?(-origWhTaxAmt.GetValueOrDefault()) : new Decimal?();
    apTranPost7.WhTaxAmt = nullable7;
    APTranPost apTranPost8 = tranPost;
    Decimal? origDiscAmt = doc.OrigDiscAmt;
    Decimal? nullable8 = origDiscAmt.HasValue ? new Decimal?(-origDiscAmt.GetValueOrDefault()) : new Decimal?();
    apTranPost8.DiscAmt = nullable8;
    APTranPost apTranPost9 = tranPost;
    Decimal? rgolAmt = doc.RGOLAmt;
    Decimal? nullable9 = rgolAmt.HasValue ? new Decimal?(-rgolAmt.GetValueOrDefault()) : new Decimal?();
    apTranPost9.RGOLAmt = nullable9;
    if (!this.IsNeedUpdateHistoryForTransaction(tranPost.TranPeriodID))
      return;
    this.TranPost.Insert(tranPost);
  }

  public virtual void ProcessAdjustmentTranPost(
    APAdjust adj,
    APRegister doc,
    APRegister pmt,
    bool adjustedOnly = false)
  {
    APTranPost row1 = new APTranPost();
    APTranPost row2 = new APTranPost();
    row1.Type = "D";
    row2.Type = "G";
    row1.LineNbr = row2.LineNbr = adj.AdjdLineNbr;
    APTranPost apTranPost1 = row1;
    bool? nullable1;
    row2.IsMigratedRecord = nullable1 = adj.IsMigratedRecord;
    bool? nullable2 = nullable1;
    apTranPost1.IsMigratedRecord = nullable2;
    row1.DocType = row2.SourceDocType = adj.AdjdDocType;
    row1.RefNbr = row2.SourceRefNbr = adj.AdjdRefNbr;
    row1.SourceDocType = row2.DocType = adj.AdjgDocType;
    row1.SourceRefNbr = row2.RefNbr = adj.AdjgRefNbr;
    row1.BatchNbr = row2.BatchNbr = adj.AdjBatchNbr;
    row1.RefNoteID = row2.RefNoteID = adj.NoteID;
    row1.TranType = row2.TranType = adj.AdjgDocType;
    row1.TranRefNbr = row2.TranRefNbr = adj.AdjgRefNbr;
    APTranPost apTranPost2 = row1;
    APTranPost apTranPost3 = row2;
    nullable1 = new bool?(this.GetHistTranType(row2.TranType, row2.TranRefNbr) == "PPM");
    bool? nullable3 = nullable1;
    apTranPost3.IsVoidPrepayment = nullable3;
    bool? nullable4 = nullable1;
    apTranPost2.IsVoidPrepayment = nullable4;
    row1.VendorID = row2.VendorID = doc.VendorID ?? pmt.VendorID;
    row1.DocDate = row2.DocDate = adj.AdjgDocDate;
    APTranPost apTranPost4 = row1;
    int? nullable5 = doc.APAccountID;
    int? nullable6 = nullable5 ?? adj.AdjdAPAcct;
    apTranPost4.AccountID = nullable6;
    APTranPost apTranPost5 = row1;
    nullable5 = doc.APSubID;
    int? nullable7 = nullable5 ?? adj.AdjdAPSub;
    apTranPost5.SubID = nullable7;
    row1.BranchID = adj.AdjdBranchID;
    row1.CuryInfoID = adj.AdjdCuryInfoID;
    row1.CuryAmt = adj.CuryAdjdAmt;
    row1.CuryPPDAmt = adj.CuryAdjdPPDAmt;
    row1.CuryDiscAmt = adj.CuryAdjdDiscAmt;
    row1.CuryWhTaxAmt = adj.CuryAdjdWhTaxAmt;
    row1.Amt = adj.AdjAmt;
    row1.PPDAmt = adj.AdjPPDAmt;
    row1.DiscAmt = adj.AdjDiscAmt;
    row1.WhTaxAmt = adj.AdjWhTaxAmt;
    row1.RGOLAmt = adj.RGOLAmt;
    FinPeriodIDAttribute.SetPeriodsByMaster<APTranPost.finPeriodID>(this.TranPost.Cache, (object) row1, adj.AdjgTranPeriodID);
    row2.AccountID = pmt.IsPrepaymentInvoiceDocument() ? pmt.PrepaymentAccountID : pmt.APAccountID;
    row2.SubID = pmt.IsPrepaymentInvoiceDocument() ? pmt.PrepaymentSubID : pmt.APSubID;
    row2.BranchID = adj.AdjgBranchID;
    row2.CuryInfoID = adj.AdjgCuryInfoID;
    row2.CuryAmt = adj.CuryAdjgAmt;
    row2.CuryPPDAmt = adj.CuryAdjgPPDAmt;
    row2.CuryDiscAmt = adj.CuryAdjgDiscAmt;
    row2.CuryWhTaxAmt = adj.CuryAdjgWhTaxAmt;
    row2.Amt = adj.AdjAmt;
    row2.PPDAmt = adj.AdjPPDAmt;
    row2.DiscAmt = adj.AdjDiscAmt;
    row2.WhTaxAmt = adj.AdjWhTaxAmt;
    row2.RGOLAmt = adj.RGOLAmt;
    FinPeriodIDAttribute.SetPeriodsByMaster<APTranPost.finPeriodID>(this.TranPost.Cache, (object) row2, adj.AdjgTranPeriodID);
    bool? isMigratedRecord = doc.IsMigratedRecord;
    if (isMigratedRecord.GetValueOrDefault())
    {
      isMigratedRecord = pmt.IsMigratedRecord;
      if (isMigratedRecord.GetValueOrDefault())
        row1.IsMigratedRecord = new bool?(true);
    }
    if (pmt.IsPrepaymentInvoiceDocumentReverse())
      row1.TranClass = row2.DocType + "Y";
    else if (doc.IsPrepaymentInvoiceDocument())
      row1.TranClass = "PPM" + "P";
    if (this.IsNeedUpdateHistoryForTransaction(row1.TranPeriodID))
    {
      this.TranPost.Insert(row1);
      if (pmt.IsPrepaymentInvoiceDocumentReverse())
      {
        APTranPost copy1 = (APTranPost) this.TranPost.Cache.CreateCopy((object) row1);
        APTranPost apTranPost6 = copy1;
        nullable5 = new int?();
        int? nullable8 = nullable5;
        apTranPost6.ID = nullable8;
        copy1.AccountID = pmt.PrepaymentAccountID;
        copy1.SubID = pmt.PrepaymentSubID;
        copy1.GLSign = new short?((short) -1);
        this.TranPost.Insert(copy1);
        APTranPost copy2 = (APTranPost) this.TranPost.Cache.CreateCopy((object) row2);
        APTranPost apTranPost7 = copy2;
        nullable5 = new int?();
        int? nullable9 = nullable5;
        apTranPost7.ID = nullable9;
        copy2.AccountID = pmt.PrepaymentAccountID;
        copy2.SubID = pmt.PrepaymentSubID;
        copy2.GLSign = new short?((short) -1);
        this.TranPost.Insert(copy2);
      }
    }
    if (!this.IsNeedUpdateHistoryForTransaction(row2.TranPeriodID) || adjustedOnly)
      return;
    if (!EnumerableExtensions.IsIn<string>(row1.DocType, "QCK", "VQC", "RQC"))
      this.TranPost.Insert(row2);
    APTranPost copy = (APTranPost) this.TranPost.Cache.CreateCopy((object) row1);
    copy.Type = "R";
    copy.CuryInfoID = adj.AdjdCuryInfoID;
    copy.CuryAmt = new Decimal?(0M);
    copy.Amt = new Decimal?(0M);
    copy.CuryPPDAmt = adj.CuryAdjdPPDAmt;
    copy.PPDAmt = adj.AdjPPDAmt;
    copy.CuryDiscAmt = adj.CuryAdjdDiscAmt;
    copy.DiscAmt = adj.AdjDiscAmt;
    copy.CuryWhTaxAmt = adj.CuryAdjdWhTaxAmt;
    copy.WhTaxAmt = adj.AdjWhTaxAmt;
    copy.TranType = adj.AdjgDocType;
    copy.TranRefNbr = adj.AdjgRefNbr;
    this.TranPost.Insert(copy);
  }

  protected virtual APTranPost CreateTranPost(APRegister doc)
  {
    APTranPost tranPost = new APTranPost()
    {
      CuryInfoID = doc.CuryInfoID
    };
    tranPost.DocType = tranPost.SourceDocType = doc.DocType;
    tranPost.RefNbr = tranPost.SourceRefNbr = doc.RefNbr;
    tranPost.VendorID = doc.VendorID;
    tranPost.FinPeriodID = doc.FinPeriodID;
    tranPost.TranPeriodID = doc.TranPeriodID;
    tranPost.AccountID = doc.APAccountID;
    tranPost.SubID = doc.APSubID;
    tranPost.BranchID = doc.BranchID;
    tranPost.BatchNbr = doc.BatchNbr;
    tranPost.DocDate = doc.DocDate;
    tranPost.CuryInfoID = doc.CuryInfoID;
    tranPost.RefNoteID = doc.NoteID;
    tranPost.TranType = doc.DocType;
    tranPost.TranRefNbr = doc.RefNbr;
    tranPost.IsMigratedRecord = doc.IsMigratedRecord;
    tranPost.IsVoidPrepayment = new bool?(this.GetHistTranType(tranPost.TranType, tranPost.TranRefNbr) == "PPM");
    return tranPost;
  }

  public static void GetPPVAccountSub(
    ref int? aAccountID,
    ref int? aSubID,
    PXGraph aGraph,
    PX.Objects.PO.POReceiptLine aRow,
    PX.Objects.CS.ReasonCode reasonCode,
    PX.Objects.PM.Lite.PMProject project,
    PX.Objects.PM.Lite.PMTask task,
    bool getPOAccrual = false)
  {
    if (aRow.InventoryID.HasValue)
    {
      PXResult<PX.Objects.IN.InventoryItem, INPostClass> pxResult = (PXResult<PX.Objects.IN.InventoryItem, INPostClass>) (PXResult<PX.Objects.IN.InventoryItem>) PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, LeftJoin<INPostClass, On<INPostClass.postClassID, Equal<PX.Objects.IN.InventoryItem.postClassID>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.PO.POLine.inventoryID>>>>.Config>.Select(aGraph, (object) aRow.InventoryID);
      PX.Objects.IN.InventoryItem inventoryItem = pxResult != null ? (PX.Objects.IN.InventoryItem) pxResult : throw new PXException("Inventory Item '{0}' used in PO Receipt# '{1}' line {2} is not found in the system", new object[3]
      {
        (object) aRow.InventoryID,
        (object) aRow.ReceiptNbr,
        (object) aRow.LineNbr
      });
      INPostClass postclass = (INPostClass) pxResult;
      if (postclass == null)
        throw new PXException("Posting class is not defined for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}", new object[3]
        {
          (object) inventoryItem.InventoryCD,
          (object) aRow.ReceiptNbr,
          (object) aRow.LineNbr
        });
      INSite site = (INSite) PXSelectBase<INSite, PXSelect<INSite, Where<INSite.siteID, Equal<Required<PX.Objects.PO.POReceiptLine.siteID>>>>.Config>.Select(aGraph, (object) aRow.SiteID);
      if (getPOAccrual)
      {
        aAccountID = INReleaseProcess.GetAcctID<INPostClass.pOAccrualAcctID>(aGraph, postclass.POAccrualAcctDefault, InventoryAccountServiceHelper.Params(inventoryItem, site, postclass));
        try
        {
          aSubID = INReleaseProcess.GetSubID<INPostClass.pOAccrualSubID>(aGraph, postclass.POAccrualAcctDefault, postclass.POAccrualSubMask, InventoryAccountServiceHelper.Params(inventoryItem, site, postclass));
        }
        catch (PXException ex)
        {
          if (!postclass.POAccrualSubID.HasValue || string.IsNullOrEmpty(postclass.POAccrualSubMask) || !inventoryItem.POAccrualSubID.HasValue || site == null || !site.POAccrualSubID.HasValue)
            throw new PXException("PO Accrual Subaccount can't be found for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}. Please, check the settings in the IN Post Class '{3}', Inventory Item and Warehouse '{4}'", new object[5]
            {
              (object) inventoryItem.InventoryCD,
              (object) aRow.ReceiptNbr,
              (object) aRow.LineNbr,
              (object) postclass.PostClassID,
              site != null ? (object) site.SiteCD : (object) string.Empty
            });
          throw ex;
        }
      }
      else if (inventoryItem.StkItem.Value)
      {
        if (aRow.LineType == "GP")
        {
          aAccountID = INReleaseProcess.GetAcctID<INPostClass.cOGSAcctID>(aGraph, postclass.COGSAcctDefault, inventoryItem, site, postclass);
          if (!aAccountID.HasValue)
            throw new PXException("GOGS Account can't be found for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}. Please, check the settings in the IN Post Class '{3}', Inventory Item and Warehouse '{4}'", new object[5]
            {
              (object) inventoryItem.InventoryCD,
              (object) aRow.ReceiptNbr,
              (object) aRow.LineNbr,
              (object) postclass.PostClassID,
              site != null ? (object) site.SiteCD : (object) string.Empty
            });
          try
          {
            aSubID = INReleaseProcess.GetSubID<INPostClass.cOGSSubID>(aGraph, postclass.COGSAcctDefault, postclass.COGSSubMask, inventoryItem, site, postclass);
          }
          catch (PXException ex)
          {
            if (!postclass.COGSSubID.HasValue || string.IsNullOrEmpty(postclass.COGSSubMask) || !inventoryItem.COGSSubID.HasValue || site == null || !site.COGSSubID.HasValue)
              throw new PXException("COGS Subaccount can't be found for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}. Please, check the settings in the IN Post Class '{3}', Inventory Item and Warehouse '{4}'", new object[5]
              {
                (object) inventoryItem.InventoryCD,
                (object) aRow.ReceiptNbr,
                (object) aRow.LineNbr,
                (object) postclass.PostClassID,
                site != null ? (object) site.SiteCD : (object) string.Empty
              });
            throw ex;
          }
          if (!aSubID.HasValue)
            throw new PXException("COGS Subaccount can't be found for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}. Please, check the settings in the IN Post Class '{3}', Inventory Item and Warehouse '{4}'", new object[5]
            {
              (object) inventoryItem.InventoryCD,
              (object) aRow.ReceiptNbr,
              (object) aRow.LineNbr,
              (object) postclass.PostClassID,
              site != null ? (object) site.SiteCD : (object) string.Empty
            });
        }
        else
        {
          aAccountID = reasonCode.AccountID;
          if (!aAccountID.HasValue)
            throw new PXException("Purchase Price Variance Account can't be found for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}. Please, check the settings in the Reason Code '{3}'", new object[4]
            {
              (object) inventoryItem.InventoryCD,
              (object) aRow.ReceiptNbr,
              (object) aRow.LineNbr,
              (object) reasonCode.ReasonCodeID
            });
          try
          {
            aSubID = INReleaseProcess.GetReasonCodeSubID(aGraph, reasonCode, InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
          }
          catch (PXException ex)
          {
            if (!reasonCode.SubID.HasValue || string.IsNullOrEmpty(reasonCode.SubMaskInventory) || inventoryItem == null || site == null)
              throw new PXException("Purchase Price Variance Subaccount can't be found for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}. Please, check the settings in the Reason Code '{3}', Inventory Item and Warehouse '{4}'", new object[5]
              {
                (object) inventoryItem.InventoryCD,
                (object) aRow.ReceiptNbr,
                (object) aRow.LineNbr,
                (object) reasonCode.ReasonCodeID,
                site != null ? (object) site.SiteCD : (object) string.Empty
              });
            throw ex;
          }
          if (!aSubID.HasValue)
            throw new PXException("Purchase Price Variance Subaccount can't be found for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}. Please, check the settings in the Reason Code '{3}', Inventory Item and Warehouse '{4}'", new object[5]
            {
              (object) inventoryItem.InventoryCD,
              (object) aRow.ReceiptNbr,
              (object) aRow.LineNbr,
              (object) reasonCode.ReasonCodeID,
              site != null ? (object) site.SiteCD : (object) string.Empty
            });
        }
      }
      else
      {
        aAccountID = INReleaseProcess.GetAcctID<INPostClass.cOGSAcctID>(aGraph, postclass.COGSAcctDefault, InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
        try
        {
          aSubID = INReleaseProcess.GetSubID<INPostClass.cOGSSubID>(aGraph, postclass.COGSAcctDefault, postclass.COGSSubMask, InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, (INTran) null, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
        }
        catch (PXException ex)
        {
          throw new PXException("Expense Subaccount cannot be assembled correctly. Please check settings for the Inventory Posting Class");
        }
      }
    }
    else
    {
      aAccountID = aRow.ExpenseAcctID;
      aSubID = aRow.ExpenseSubID;
    }
  }

  public virtual bool IsPPVCalcNeeded(PX.Objects.PO.POReceiptLine rctLine, APTran tran)
  {
    return rctLine.LineType == "GI" || rctLine.LineType == "GP" || rctLine.LineType == "NO" || rctLine.LineType == "NF" || rctLine.LineType == "GS" || rctLine.LineType == "GF" || rctLine.LineType == "NP" || rctLine.LineType == "GR" || rctLine.LineType == "NS" || rctLine.LineType == "GM" || rctLine.LineType == "NM";
  }

  /// <summary>
  /// Gets the amount to be posted to the expense account for the given document line.
  /// </summary>
  public static ARReleaseProcess.Amount GetExpensePostingAmount(PXGraph graph, PX.Objects.PO.POLine documentLine)
  {
    PXSelectJoin<PX.Objects.PO.POLine, LeftJoin<POTax, On<POTax.orderType, Equal<PX.Objects.PO.POLine.orderType>, And<POTax.orderNbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<POTax.lineNbr, Equal<PX.Objects.PO.POLine.lineNbr>>>>, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<POTax.taxID>>, LeftJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderType, Equal<PX.Objects.PO.POLine.orderType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<PX.Objects.PO.POLine.orderNbr>>>>>>, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<PX.Objects.PO.POLine.lineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>> pxSelectJoin = new PXSelectJoin<PX.Objects.PO.POLine, LeftJoin<POTax, On<POTax.orderType, Equal<PX.Objects.PO.POLine.orderType>, And<POTax.orderNbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<POTax.lineNbr, Equal<PX.Objects.PO.POLine.lineNbr>>>>, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<POTax.taxID>>, LeftJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderType, Equal<PX.Objects.PO.POLine.orderType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<PX.Objects.PO.POLine.orderNbr>>>>>>, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<PX.Objects.PO.POLine.lineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>>(graph);
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = graph.FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
    object[] objArray = new object[3]
    {
      (object) documentLine.OrderType,
      (object) documentLine.OrderNbr,
      (object) documentLine.LineNbr
    };
    PXResult<PX.Objects.PO.POLine, POTax, PX.Objects.TX.Tax, PX.Objects.PO.POOrder> pxResult = pxSelectJoin.Select(objArray).AsEnumerable<PXResult<PX.Objects.PO.POLine>>().Cast<PXResult<PX.Objects.PO.POLine, POTax, PX.Objects.TX.Tax, PX.Objects.PO.POOrder>>().First<PXResult<PX.Objects.PO.POLine, POTax, PX.Objects.TX.Tax, PX.Objects.PO.POOrder>>();
    return APReleaseProcess.GetExpensePostingAmountBase((ITaxableDetail) (PX.Objects.PO.POLine) pxResult, (ITaxDetailWithAmounts) (POTax) pxResult, (PX.Objects.TX.Tax) pxResult, pxResult.GetItem<PX.Objects.PO.POOrder>()?.TaxCalcMode, new Func<Decimal, Decimal>(defaultCurrencyInfo.RoundCury), new Func<Decimal, Decimal>(defaultCurrencyInfo.RoundBase));
  }

  /// <summary>
  /// Gets the amount to be posted to the expense account
  /// for the given document line.
  /// </summary>
  public static ARReleaseProcess.Amount GetExpensePostingAmount(PXGraph graph, APTran documentLine)
  {
    PXResult<APTran, APTax, PX.Objects.TX.Tax, APInvoice> pxResult = new PXSelectJoin<APTran, LeftJoin<APTax, On<APTax.tranType, Equal<APTran.tranType>, And<APTax.refNbr, Equal<APTran.refNbr>, And<APTax.lineNbr, Equal<APTran.lineNbr>>>>, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTax.taxID>>, LeftJoin<APInvoice, On<APInvoice.docType, Equal<APTran.tranType>, And<APInvoice.refNbr, Equal<APTran.refNbr>>>>>>, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>, And<APTran.lineNbr, Equal<Required<APTran.lineNbr>>>>>>(graph).Select((object) documentLine.TranType, (object) documentLine.RefNbr, (object) documentLine.LineNbr).AsEnumerable<PXResult<APTran>>().Cast<PXResult<APTran, APTax, PX.Objects.TX.Tax, APInvoice>>().First<PXResult<APTran, APTax, PX.Objects.TX.Tax, APInvoice>>();
    Func<Decimal, Decimal> func = (Func<Decimal, Decimal>) (amount => PX.Objects.CM.PXDBCurrencyAttribute.Round(graph.Caches[typeof (APTran)], (object) documentLine, amount, CMPrecision.TRANCURY));
    return APReleaseProcess.GetExpensePostingAmount(graph, documentLine, (APTax) pxResult, (PX.Objects.TX.Tax) pxResult, (APInvoice) pxResult, func, func);
  }

  /// <summary>
  /// If <see cref="T:PX.Objects.CS.FeaturesSet.netGrossEntryMode" /> is enabled, overrides the tax
  /// calculation level of the specified sales tax based on the document-level settings, e.g. to
  /// correctly calculate the expense posting amount (<see cref="M:PX.Objects.AP.APReleaseProcess.GetExpensePostingAmount(PX.Data.PXGraph,PX.Objects.AP.APTran)" />).
  /// </summary>
  /// <returns>A copy of the <see cref="T:PX.Objects.TX.Tax" /> with potentially adjusted calculation level.</returns>
  public static void AdjustTaxCalculationLevelForNetGrossEntryMode(
    APInvoice document,
    APTran documentLine,
    ref PX.Objects.TX.Tax taxCorrespondingToLine)
  {
    if (!(taxCorrespondingToLine?.TaxCalcType == "I") || !PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>() || taxCorrespondingToLine.DirectTax.GetValueOrDefault())
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

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public static ARReleaseProcess.Amount GetExpensePostingAmount(
    PXGraph graph,
    APTran documentLine,
    APTax lineTax,
    PX.Objects.TX.Tax salesTax,
    APInvoice document,
    Func<Decimal, Decimal> round)
  {
    return APReleaseProcess.GetExpensePostingAmount(graph, documentLine, lineTax, salesTax, document, round, round);
  }

  public static ARReleaseProcess.Amount GetExpensePostingAmount(
    PXGraph graph,
    APTran documentLine,
    APTax lineTax,
    PX.Objects.TX.Tax salesTax,
    APInvoice document,
    Func<Decimal, Decimal> roundCury,
    Func<Decimal, Decimal> roundBase)
  {
    APReleaseProcess.AdjustTaxCalculationLevelForNetGrossEntryMode(document, documentLine, ref salesTax);
    bool flag1 = document != null && document.PendingPPD.GetValueOrDefault() && (document.DocType == "ADR" || document.DocType == "ACR");
    if (!flag1 && lineTax != null && lineTax.TaxID == null && document != null && document.OrigDocType == "ADR" && document.OrigRefNbr != null)
      flag1 = PXSelectBase<APRegister, PXSelect<APRegister, Where<APRegister.refNbr, Equal<Required<APRegister.refNbr>>, And<APRegister.docType, Equal<Required<APRegister.docType>>, And<APRegister.pendingPPD, Equal<True>>>>>.Config>.SelectSingleBound(graph, (object[]) null, (object) document.OrigRefNbr, (object) document.OrigDocType).Count > 0;
    bool flag2 = salesTax?.TaxType == "Q" && salesTax.TaxCalcLevel == "0";
    if (flag1 || flag2)
      return new ARReleaseProcess.Amount(documentLine.CuryTaxableAmt, documentLine.TaxableAmt);
    ARReleaseProcess.Amount postingAmountBase = APReleaseProcess.GetExpensePostingAmountBase((ITaxableDetail) documentLine, (ITaxDetailWithAmounts) lineTax, salesTax, document?.TaxCalcMode, roundCury, roundBase);
    if (lineTax != null && lineTax.TaxID != null && (salesTax != null && salesTax.IsRegularInclusiveTax() || document?.TaxCalcMode == "G"))
      postingAmountBase += new ARReleaseProcess.Amount(new Decimal?(lineTax.CuryTaxableDiscountAmt.GetValueOrDefault()), lineTax.TaxableDiscountAmt);
    return postingAmountBase;
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public static ARReleaseProcess.Amount GetExpensePostingAmountBase(
    PXGraph graph,
    ITaxableDetail documentLine,
    ITaxDetailWithAmounts lineTax,
    PX.Objects.TX.Tax salesTax,
    APInvoice document,
    Func<Decimal, Decimal> round)
  {
    return APReleaseProcess.GetExpensePostingAmountBase(documentLine, lineTax, salesTax, document?.TaxCalcMode, round, round);
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public static ARReleaseProcess.Amount GetExpensePostingAmountBase(
    PXGraph graph,
    ITaxableDetail documentLine,
    ITaxDetailWithAmounts lineTax,
    PX.Objects.TX.Tax salesTax,
    APInvoice document,
    Func<Decimal, Decimal> roundCury,
    Func<Decimal, Decimal> roundBase)
  {
    return APReleaseProcess.GetExpensePostingAmountBase(documentLine, lineTax, salesTax, document?.TaxCalcMode, roundCury, roundBase);
  }

  /// <summary>
  /// Calculates the amount to be posted to the expense account for the given document line and Tax Detail.
  /// </summary>
  /// <param name="documentLine">APTran/POLine</param>
  /// <param name="lineTax">APTax/POTax record</param>
  /// <param name="salesTax">Tax</param>
  /// <param name="TaxCalcMode">TaxCalcMode from APInvoice/POOrder</param>
  /// <param name="roundCury">Method to round amount in current currency (CurrencyInfo.RoundCury is fine)</param>
  /// <param name="roundBase">Method to round amount in base currency (CurrencyInfo.RoundBase is fine)</param>
  /// <returns></returns>
  public static ARReleaseProcess.Amount GetExpensePostingAmountBase(
    ITaxableDetail documentLine,
    ITaxDetailWithAmounts lineTax,
    PX.Objects.TX.Tax salesTax,
    string TaxCalcMode,
    Func<Decimal, Decimal> roundCury,
    Func<Decimal, Decimal> roundBase)
  {
    if (lineTax != null && lineTax.TaxID != null && (salesTax != null && salesTax.IsRegularInclusiveTax() || TaxCalcMode == "G"))
    {
      Decimal? curyTranAmt1 = documentLine.CuryTranAmt;
      Func<Decimal, Decimal> func1 = roundCury;
      Decimal? curyTranAmt2 = documentLine.CuryTranAmt;
      Decimal? groupDiscountRate1 = documentLine.GroupDiscountRate;
      Decimal? nullable1 = curyTranAmt2.HasValue & groupDiscountRate1.HasValue ? new Decimal?(curyTranAmt2.GetValueOrDefault() * groupDiscountRate1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable2 = documentLine.DocumentDiscountRate;
      Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal num1 = nullable3.Value;
      Decimal num2 = func1(num1);
      Decimal? nullable4;
      if (!curyTranAmt1.HasValue)
      {
        nullable3 = new Decimal?();
        nullable4 = nullable3;
      }
      else
        nullable4 = new Decimal?(curyTranAmt1.GetValueOrDefault() - num2);
      nullable1 = documentLine.TranAmt;
      Func<Decimal, Decimal> func2 = roundBase;
      nullable3 = documentLine.TranAmt;
      Decimal? groupDiscountRate2 = documentLine.GroupDiscountRate;
      nullable2 = nullable3.HasValue & groupDiscountRate2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * groupDiscountRate2.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable5 = documentLine.DocumentDiscountRate;
      Decimal? nullable6 = nullable2.HasValue & nullable5.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
      Decimal num3 = nullable6.Value;
      Decimal num4 = func2(num3);
      Decimal? nullable7;
      if (!nullable1.HasValue)
      {
        nullable6 = new Decimal?();
        nullable7 = nullable6;
      }
      else
        nullable7 = new Decimal?(nullable1.GetValueOrDefault() - num4);
      Decimal? nullable8 = nullable7;
      nullable1 = lineTax.CuryTaxableAmt;
      nullable6 = lineTax.CuryRetainedTaxableAmt;
      Decimal valueOrDefault1 = nullable6.GetValueOrDefault();
      Decimal? nullable9;
      if (!nullable1.HasValue)
      {
        nullable6 = new Decimal?();
        nullable9 = nullable6;
      }
      else
        nullable9 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault1);
      nullable2 = nullable9;
      nullable5 = nullable4;
      Decimal? cury;
      if (!(nullable2.HasValue & nullable5.HasValue))
      {
        nullable1 = new Decimal?();
        cury = nullable1;
      }
      else
        cury = new Decimal?(nullable2.GetValueOrDefault() + nullable5.GetValueOrDefault());
      nullable1 = lineTax.TaxableAmt;
      nullable6 = lineTax.RetainedTaxableAmt;
      Decimal valueOrDefault2 = nullable6.GetValueOrDefault();
      Decimal? nullable10;
      if (!nullable1.HasValue)
      {
        nullable6 = new Decimal?();
        nullable10 = nullable6;
      }
      else
        nullable10 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault2);
      nullable5 = nullable10;
      nullable2 = nullable8;
      Decimal? baaase;
      if (!(nullable5.HasValue & nullable2.HasValue))
      {
        nullable1 = new Decimal?();
        baaase = nullable1;
      }
      else
        baaase = new Decimal?(nullable5.GetValueOrDefault() + nullable2.GetValueOrDefault());
      return new ARReleaseProcess.Amount(cury, baaase);
    }
    Decimal? nullable11 = documentLine.CuryTranAmt;
    Decimal? nullable12 = documentLine.CuryRetainageAmt;
    Decimal valueOrDefault3 = nullable12.GetValueOrDefault();
    Decimal? cury1;
    if (!nullable11.HasValue)
    {
      nullable12 = new Decimal?();
      cury1 = nullable12;
    }
    else
      cury1 = new Decimal?(nullable11.GetValueOrDefault() + valueOrDefault3);
    nullable11 = documentLine.TranAmt;
    nullable12 = documentLine.RetainageAmt;
    Decimal valueOrDefault4 = nullable12.GetValueOrDefault();
    Decimal? baaase1;
    if (!nullable11.HasValue)
    {
      nullable12 = new Decimal?();
      baaase1 = nullable12;
    }
    else
      baaase1 = new Decimal?(nullable11.GetValueOrDefault() + valueOrDefault4);
    return new ARReleaseProcess.Amount(cury1, baaase1);
  }

  protected virtual void VerifyRoundingAllowed(APInvoice document, PX.Objects.GL.Batch batch, string baseCuryID)
  {
    PX.Objects.CM.Currency currency = (PX.Objects.CM.Currency) PXSelectBase<PX.Objects.CM.Currency, PXSelect<PX.Objects.CM.Currency, Where<PX.Objects.CM.Extensions.Currency.curyID, Equal<Required<APInvoice.curyID>>>>.Config>.Select((PXGraph) this, (object) document.CuryID);
    Decimal? debitTotal = batch.DebitTotal;
    Decimal? creditTotal = batch.CreditTotal;
    Decimal d = (debitTotal.HasValue & creditTotal.HasValue ? new Decimal?(debitTotal.GetValueOrDefault() - creditTotal.GetValueOrDefault()) : new Decimal?()).Value;
    if ((!currency.UseAPPreferencesSettings.GetValueOrDefault() ? currency.APInvoiceRounding == "N" : this.InvoiceRounding == "N") && System.Math.Abs(System.Math.Round(document.CuryTaxRoundDiff.Value - d, 4)) >= 0.00005M)
      throw new PXException("The document is out of the balance.");
    Decimal num1 = System.Math.Abs(System.Math.Round(d, 4));
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

  private static string GetTaxDrCr(string origTranType, string tranType)
  {
    string taxDrCr = (string) null;
    if (!string.IsNullOrWhiteSpace(origTranType) && !string.IsNullOrWhiteSpace(tranType) && origTranType == "INV" && tranType == "ADR")
      taxDrCr = APInvoiceType.DrCr(tranType);
    if (taxDrCr == null)
      taxDrCr = string.IsNullOrWhiteSpace(origTranType) ? APInvoiceType.DrCr(tranType) : APInvoiceType.DrCr(origTranType);
    return taxDrCr;
  }

  private static void Append(PX.Objects.GL.GLTran aDest, PX.Objects.GL.GLTran aSrc)
  {
    PX.Objects.GL.GLTran glTran1 = aDest;
    Decimal? nullable = glTran1.CuryCreditAmt;
    Decimal valueOrDefault1 = aSrc.CuryCreditAmt.GetValueOrDefault();
    glTran1.CuryCreditAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    PX.Objects.GL.GLTran glTran2 = aDest;
    nullable = glTran2.CreditAmt;
    Decimal valueOrDefault2 = aSrc.CreditAmt.GetValueOrDefault();
    glTran2.CreditAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
    PX.Objects.GL.GLTran glTran3 = aDest;
    nullable = glTran3.CuryDebitAmt;
    Decimal valueOrDefault3 = aSrc.CuryDebitAmt.GetValueOrDefault();
    glTran3.CuryDebitAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
    PX.Objects.GL.GLTran glTran4 = aDest;
    nullable = glTran4.DebitAmt;
    Decimal valueOrDefault4 = aSrc.DebitAmt.GetValueOrDefault();
    glTran4.DebitAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
  }

  private static void Invert(PX.Objects.GL.GLTran aRow)
  {
    Decimal? curyDebitAmt = aRow.CuryDebitAmt;
    Decimal? debitAmt = aRow.DebitAmt;
    aRow.CuryDebitAmt = aRow.CuryCreditAmt;
    aRow.DebitAmt = aRow.CreditAmt;
    aRow.CuryCreditAmt = curyDebitAmt;
    aRow.CreditAmt = debitAmt;
  }

  private void UpdateWithholding(
    JournalEntry je,
    APAdjust adj,
    APRegister adjddoc,
    APPayment adjgdoc,
    Vendor vend,
    PX.Objects.CM.Extensions.CurrencyInfo vouch_info)
  {
    APRegister apRegister1 = adjddoc;
    APRegister apRegister2 = (APRegister) this.APDocument.Cache.Locate((object) apRegister1);
    if (apRegister2 != null)
      apRegister1 = apRegister2;
    if (adjgdoc.DocType == "ADR" || PX.Objects.CM.PXCurrencyAttribute.IsNullOrEmpty(apRegister1.CuryOrigWhTaxAmt))
      return;
    if (je.currencyinfo.Current == null)
      throw new PXException();
    PXResultset<APTaxTran> pxResultset = this.WHTax_TranType_RefNbr.Select((object) apRegister1.DocType, (object) apRegister1.RefNbr);
    int num1 = 0;
    Decimal num2 = adj.CuryAdjgWhTaxAmt.Value;
    Decimal? nullable1 = adj.AdjWhTaxAmt;
    Decimal num3 = nullable1.Value;
    foreach (PXResult<APTaxTran, PX.Objects.TX.Tax> pxResult in pxResultset)
    {
      PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) pxResult;
      APTaxTran apTaxTran1 = (APTaxTran) pxResult;
      if (apRegister1.DocType == "QCK" || apRegister1.DocType == "VQC" || apRegister1.DocType == "RQC")
      {
        apTaxTran1.Released = new bool?(true);
        this.WHTax_TranType_RefNbr.Update(apTaxTran1);
        this.CreateGLTranForWhTaxTran(je, adj, adjgdoc, apTaxTran1, vend, vouch_info, num1 == pxResultset.Count - 1);
      }
      else
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = this.GetExtension<APReleaseProcess.MultiCurrency>().GetCurrencyInfo(adj.AdjgCuryInfoID);
        APTaxTran apTaxTran2 = new APTaxTran();
        apTaxTran2.Module = apTaxTran1.Module;
        apTaxTran2.BranchID = adj.AdjgBranchID;
        apTaxTran2.TranType = adj.AdjgDocType;
        apTaxTran2.RefNbr = adj.AdjgRefNbr;
        apTaxTran2.AdjdDocType = adj.AdjdDocType;
        apTaxTran2.AdjdRefNbr = adj.AdjdRefNbr;
        apTaxTran2.AdjNbr = adj.AdjNbr;
        apTaxTran2.VendorID = apTaxTran1.VendorID;
        apTaxTran2.TaxZoneID = apTaxTran1.TaxZoneID;
        apTaxTran2.TaxID = apTaxTran1.TaxID;
        apTaxTran2.TaxRate = apTaxTran1.TaxRate;
        apTaxTran2.AccountID = apTaxTran1.AccountID;
        apTaxTran2.SubID = apTaxTran1.SubID;
        apTaxTran2.TaxType = apTaxTran1.TaxType;
        apTaxTran2.TaxBucketID = apTaxTran1.TaxBucketID;
        apTaxTran2.TranDate = adj.AdjgDocDate;
        apTaxTran2.FinPeriodID = adj.AdjgFinPeriodID;
        apTaxTran2.CuryInfoID = adj.AdjgCuryInfoID;
        apTaxTran2.Released = new bool?(true);
        apTaxTran2.CuryID = currencyInfo1.CuryID;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = currencyInfo1;
        nullable1 = adj.CuryAdjgAmt;
        Decimal num4 = nullable1.Value;
        nullable1 = adj.CuryAdjgWhTaxAmt;
        Decimal num5 = nullable1.Value;
        Decimal num6 = num4 + num5;
        nullable1 = apTaxTran1.CuryTaxableAmt;
        Decimal num7 = nullable1.Value;
        Decimal num8 = num6 * num7;
        nullable1 = apRegister1.CuryOrigDocAmt;
        Decimal num9 = nullable1.Value;
        Decimal val1 = num8 / num9;
        apTaxTran2.CuryTaxableAmt = new Decimal?(currencyInfo2.RoundCury(val1));
        APTaxTran apTaxTran3 = apTaxTran2;
        if (num1 < pxResultset.Count - 1)
        {
          APTaxTran apTaxTran4 = apTaxTran3;
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo1;
          nullable1 = adj.CuryAdjgWhTaxAmt;
          Decimal num10 = nullable1.Value;
          nullable1 = apTaxTran1.CuryTaxAmt;
          Decimal num11 = nullable1.Value;
          Decimal num12 = num10 * num11;
          nullable1 = apRegister1.CuryOrigWhTaxAmt;
          Decimal num13 = nullable1.Value;
          Decimal val2 = num12 / num13;
          Decimal? nullable2 = new Decimal?(currencyInfo3.RoundCury(val2));
          apTaxTran4.CuryTaxAmt = nullable2;
          if (this.APTaxTran_TranType_RefNbr.Cache.ObjectsEqual((object) apTaxTran3, (object) apTaxTran1))
          {
            apTaxTran3.CreatedByID = apTaxTran1.CreatedByID;
            apTaxTran3.CreatedByScreenID = apTaxTran1.CreatedByScreenID;
            apTaxTran3.CreatedDateTime = apTaxTran1.CreatedDateTime;
            apTaxTran3 = (APTaxTran) this.APTaxTran_TranType_RefNbr.Cache.Update((object) apTaxTran3);
          }
          else
            apTaxTran3 = (APTaxTran) this.APTaxTran_TranType_RefNbr.Cache.Insert((object) apTaxTran3);
          Decimal num14 = num2;
          nullable1 = apTaxTran3.CuryTaxAmt;
          Decimal num15 = nullable1.Value;
          num2 = num14 - num15;
          Decimal num16 = num3;
          nullable1 = apTaxTran3.TaxAmt;
          Decimal num17 = nullable1.Value;
          num3 = num16 - num17;
        }
        else
        {
          apTaxTran3.CuryTaxAmt = new Decimal?(num2);
          apTaxTran3.TaxAmt = new Decimal?(num3);
          if (this.APTaxTran_TranType_RefNbr.Cache.ObjectsEqual((object) apTaxTran3, (object) apTaxTran1))
          {
            apTaxTran3.CreatedByID = apTaxTran1.CreatedByID;
            apTaxTran3.CreatedByScreenID = apTaxTran1.CreatedByScreenID;
            apTaxTran3.CreatedDateTime = apTaxTran1.CreatedDateTime;
            this.APTaxTran_TranType_RefNbr.Cache.Update((object) apTaxTran3);
          }
          else
            this.APTaxTran_TranType_RefNbr.Cache.Insert((object) apTaxTran3);
          num2 = 0M;
          num3 = 0M;
        }
        this.CreateGLTranForWhTaxTran(je, adj, adjgdoc, apTaxTran3, vend, vouch_info, num1 == pxResultset.Count - 1);
      }
      ++num1;
    }
  }

  protected virtual void CreateGLTranForWhTaxTran(
    JournalEntry je,
    APAdjust adj,
    APPayment adjgdoc,
    APTaxTran whtran,
    Vendor vend,
    PX.Objects.CM.Extensions.CurrencyInfo vouch_info,
    bool updateHistory)
  {
    PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
    glTran1.SummPost = new bool?(this.SummPost);
    glTran1.BranchID = whtran.BranchID;
    glTran1.AccountID = whtran.AccountID;
    glTran1.SubID = whtran.SubID;
    glTran1.OrigAccountID = adj.AdjdAPAcct;
    glTran1.OrigSubID = adj.AdjdAPSub;
    PX.Objects.GL.GLTran glTran2 = glTran1;
    Decimal? adjgGlSign1 = adj.AdjgGLSign;
    Decimal num1 = 1M;
    Decimal? nullable1 = adjgGlSign1.GetValueOrDefault() == num1 & adjgGlSign1.HasValue ? new Decimal?(0M) : whtran.TaxAmt;
    glTran2.DebitAmt = nullable1;
    PX.Objects.GL.GLTran glTran3 = glTran1;
    Decimal? adjgGlSign2 = adj.AdjgGLSign;
    Decimal num2 = 1M;
    Decimal? nullable2 = adjgGlSign2.GetValueOrDefault() == num2 & adjgGlSign2.HasValue ? new Decimal?(0M) : whtran.CuryTaxAmt;
    glTran3.CuryDebitAmt = nullable2;
    PX.Objects.GL.GLTran glTran4 = glTran1;
    Decimal? adjgGlSign3 = adj.AdjgGLSign;
    Decimal num3 = 1M;
    Decimal? nullable3 = adjgGlSign3.GetValueOrDefault() == num3 & adjgGlSign3.HasValue ? whtran.TaxAmt : new Decimal?(0M);
    glTran4.CreditAmt = nullable3;
    PX.Objects.GL.GLTran glTran5 = glTran1;
    Decimal? adjgGlSign4 = adj.AdjgGLSign;
    Decimal num4 = 1M;
    Decimal? nullable4 = adjgGlSign4.GetValueOrDefault() == num4 & adjgGlSign4.HasValue ? whtran.CuryTaxAmt : new Decimal?(0M);
    glTran5.CuryCreditAmt = nullable4;
    glTran1.TranType = adj.AdjgDocType;
    glTran1.TranClass = "W";
    glTran1.RefNbr = adj.AdjgRefNbr;
    glTran1.TranDesc = whtran.TaxID;
    glTran1.TranDate = adj.AdjgDocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran1, adj.AdjgTranPeriodID);
    glTran1.CuryInfoID = je.currencyinfo.Current.CuryInfoID;
    glTran1.Released = new bool?(true);
    glTran1.ReferenceID = adjgdoc.VendorID;
    this.InsertAdjustmentsWhTaxTransaction(je, glTran1, new APReleaseProcess.GLTranInsertionContext()
    {
      APRegisterRecord = (APRegister) adjgdoc,
      APAdjustRecord = adj,
      APTaxTranRecord = whtran
    });
    if (!updateHistory)
      return;
    PX.Objects.GL.GLTran glTran6 = glTran1;
    Decimal? adjgGlSign5 = adj.AdjgGLSign;
    Decimal num5 = 1M;
    Decimal? nullable5 = adjgGlSign5.GetValueOrDefault() == num5 & adjgGlSign5.HasValue ? new Decimal?(0M) : adj.AdjWhTaxAmt;
    glTran6.DebitAmt = nullable5;
    PX.Objects.GL.GLTran glTran7 = glTran1;
    adjgGlSign5 = adj.AdjgGLSign;
    Decimal num6 = 1M;
    Decimal? nullable6 = adjgGlSign5.GetValueOrDefault() == num6 & adjgGlSign5.HasValue ? adj.AdjWhTaxAmt : new Decimal?(0M);
    glTran7.CreditAmt = nullable6;
    this.UpdateHistory(glTran1, vend);
    PX.Objects.GL.GLTran glTran8 = glTran1;
    adjgGlSign5 = adj.AdjgGLSign;
    Decimal num7 = 1M;
    Decimal? nullable7 = adjgGlSign5.GetValueOrDefault() == num7 & adjgGlSign5.HasValue ? new Decimal?(0M) : adj.CuryAdjdWhTaxAmt;
    glTran8.CuryDebitAmt = nullable7;
    PX.Objects.GL.GLTran glTran9 = glTran1;
    adjgGlSign5 = adj.AdjgGLSign;
    Decimal num8 = 1M;
    Decimal? nullable8 = adjgGlSign5.GetValueOrDefault() == num8 & adjgGlSign5.HasValue ? adj.CuryAdjdWhTaxAmt : new Decimal?(0M);
    glTran9.CuryCreditAmt = nullable8;
    this.UpdateHistory(glTran1, vend, vouch_info);
  }

  protected virtual void AP1099Hist_FinYear_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsIntegrityCheck && !this.IsInvoiceReclassification)
      return;
    e.Cancel = true;
  }

  protected virtual void AP1099Hist_BoxNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsIntegrityCheck && !this.IsInvoiceReclassification)
      return;
    e.Cancel = true;
  }

  protected virtual void AP1099Hist_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (((AP1099History) e.Row).BoxNbr.HasValue)
      return;
    e.Cancel = true;
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public static void Update1099Hist(
    PXGraph graph,
    Decimal histMult,
    APAdjust adj,
    APTran tran,
    APRegister apdoc)
  {
    APPayment payment = (APPayment) PXSelectBase<APPayment, PXViewOf<APPayment>.BasedOn<SelectFromBase<APPayment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPayment.docType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<APPayment.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select(graph, (object) adj.AdjgDocType, (object) adj.AdjgRefNbr);
    APReleaseProcess.Update1099Hist(graph, histMult, adj, tran, apdoc, (APRegister) payment);
  }

  public static void Update1099Hist(
    PXGraph graph,
    Decimal histMult,
    APAdjust adj,
    APTran tran,
    APRegister apdoc,
    APRegister payment)
  {
    if (adj.AdjdDocType == "PPM" || adj.AdjgDocType == "ADR")
      return;
    if (adj.AdjgDocType == "VQC" || adj.AdjgDocType == "REF" || adj.AdjgDocType == "VRF" || adj.AdjdDocType == "ADR" || adj.AdjgDocType == "RQC")
      histMult = -histMult;
    APReleaseProcess.Update1099Hist(graph, histMult, tran, apdoc, payment.DocDate, adj.AdjgBranchID, adj.AdjAmt);
  }

  public static void Update1099Hist(
    PXGraph graph,
    Decimal histMult,
    APTran tran,
    APRegister apdoc,
    System.DateTime? docDate,
    int? branchID,
    Decimal? adjAmt)
  {
    PXCache cach = graph.Caches[typeof (AP1099Hist)];
    string str = docDate.Value.Year.ToString();
    if (apdoc == null)
      return;
    Decimal? nullable = tran.CuryLineAmt;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      return;
    AP1099Yr ap1099Yr1 = new AP1099Yr();
    ap1099Yr1.FinYear = str;
    ap1099Yr1.OrganizationID = PXAccess.GetParentOrganizationID(branchID);
    AP1099Yr ap1099Yr2 = ap1099Yr1;
    AP1099Yr ap1099Yr3 = (AP1099Yr) graph.Caches[typeof (AP1099Yr)].Insert((object) ap1099Yr2);
    AP1099Hist ap1099Hist1 = new AP1099Hist();
    ap1099Hist1.BranchID = branchID;
    ap1099Hist1.VendorID = apdoc.VendorID;
    ap1099Hist1.FinYear = str;
    ap1099Hist1.BoxNbr = tran.Box1099;
    AP1099Hist ap1099Hist2 = (AP1099Hist) cach.Insert((object) ap1099Hist1);
    if (ap1099Hist2 == null)
      return;
    Decimal lineWhTaxAmount = APReleaseProcess.GetLineWhTaxAmount(graph, tran);
    nullable = tran.GroupDiscountRate;
    Decimal num2 = nullable.Value;
    nullable = tran.DocumentDiscountRate;
    Decimal num3 = nullable.Value;
    Decimal num4 = num2 * num3;
    nullable = tran.TranAmt;
    Decimal num5 = nullable.Value * num4;
    nullable = apdoc.OrigDocAmt;
    Decimal num6 = 0M;
    Decimal num7;
    if (nullable.GetValueOrDefault() == num6 & nullable.HasValue)
    {
      num7 = num5 - lineWhTaxAmount;
    }
    else
    {
      Decimal num8;
      if (!(apdoc.DocType == "QCK") && !(apdoc.DocType == "VQC") && !(apdoc.DocType == "RQC"))
      {
        num8 = 0M;
      }
      else
      {
        nullable = apdoc.OrigDiscAmt;
        num8 = nullable.Value;
      }
      Decimal num9 = num8;
      Decimal num10 = (num5 - lineWhTaxAmount) * adjAmt.Value;
      nullable = apdoc.OrigDocAmt;
      Decimal num11 = nullable.Value + num9;
      nullable = apdoc.OrigWhTaxAmt;
      Decimal num12 = nullable.Value;
      Decimal num13 = num11 - num12;
      num7 = num10 / num13;
    }
    AP1099Hist ap1099Hist3 = ap1099Hist2;
    nullable = ap1099Hist3.HistAmt;
    Decimal num14 = PX.Objects.CM.PXCurrencyAttribute.BaseRound(graph, histMult * num7);
    ap1099Hist3.HistAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num14) : new Decimal?();
  }

  private static Decimal GetLineWhTaxAmount(PXGraph graph, APTran tran)
  {
    return PXSelectBase<APTax, PXViewOf<APTax>.BasedOn<SelectFromBase<APTax, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.TX.Tax>.On<BqlOperand<APTax.taxID, IBqlString>.IsEqual<PX.Objects.TX.Tax.taxID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTax.refNbr, Equal<P.AsString>>>>, PX.Data.And<BqlOperand<APTax.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>, PX.Data.And<BqlOperand<APTax.tranType, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.TX.Tax.taxType, IBqlString>.IsEqual<CSTaxType.withholding>>>>.ReadOnly.Config>.Select(graph, (object) tran.RefNbr, (object) tran.LineNbr, (object) tran.TranType).RowCast<APTax>().Sum<APTax>((Func<APTax, Decimal>) (a => a.TaxAmt.Value));
  }

  private void Update1099(APAdjust adj, APRegister apdoc, APRegister payment)
  {
    string str = payment.DocDate.Value.Year.ToString();
    int? parentOrganizationId = PXAccess.GetParentOrganizationID(adj.AdjgBranchID);
    AP1099Year ap1099Year1 = (AP1099Year) PXSelectBase<AP1099Year, PXSelect<AP1099Year, Where<AP1099Year.finYear, Equal<Required<AP1099Year.finYear>>, And<AP1099Year.organizationID, Equal<Required<AP1099Year.organizationID>>>>>.Config>.Select((PXGraph) this, (object) str, (object) parentOrganizationId);
    if (ap1099Year1 == null)
    {
      AP1099Yr ap1099Yr = new AP1099Yr();
      ap1099Yr.FinYear = str;
      ap1099Yr.Status = "N";
      ap1099Yr.OrganizationID = parentOrganizationId;
      AP1099Year ap1099Year2 = (AP1099Year) this.AP1099Year_Select.Cache.Insert((object) ap1099Yr);
    }
    else if (!this._IsIntegrityCheck && ap1099Year1.Status != "N")
      throw new PXException("The payment date must be within an open 1099 year.");
    foreach (PXResult<APTran> pxResult in this.AP1099Tran_Select.Select((object) apdoc.DocType, (object) apdoc.RefNbr))
    {
      APTran tran = (APTran) pxResult;
      APReleaseProcess.Update1099Hist((PXGraph) this, 1M, adj, tran, apdoc, payment);
    }
  }

  public virtual void UpdateBalances(APAdjust adj, APRegister adjddoc, Vendor vendor)
  {
    this.UpdateBalances(adj, adjddoc, vendor, (APTran) null);
  }

  private bool ShouldProcessAsPrepaymentRequestApplication(APRegister apdoc, APAdjust adj)
  {
    if (apdoc.DocType != "PPM" || adj.AdjgDocType == "REF" || adj.AdjgDocType == "VRF")
      return false;
    int num1 = adj.AdjgDocType == "VCK" ? 1 : (adj.VoidAdjNbr.HasValue ? 1 : 0);
    bool flag1 = adj.AdjgDocType == "CHK";
    bool flag2 = adj.AdjgDocType == "PPM" && !string.Equals(adj.AdjgRefNbr, apdoc.RefNbr, StringComparison.OrdinalIgnoreCase);
    int num2 = flag1 ? 1 : 0;
    return (num1 | num2 | (flag2 ? 1 : 0)) != 0 && string.Equals(adj.AdjdDocType, apdoc.DocType, StringComparison.Ordinal) && string.Equals(adj.AdjdRefNbr, apdoc.RefNbr, StringComparison.OrdinalIgnoreCase);
  }

  public virtual void UpdateBalances(
    APAdjust adj,
    APRegister adjddoc,
    Vendor vendor,
    APTran adjdtran)
  {
    APRegister apRegister1 = adjddoc;
    APRegister copy = (APRegister) this.APDocument.Cache.Locate((object) apRegister1);
    if (copy != null)
      this.APDocument.Cache.RestoreCopy((object) apRegister1, (object) copy);
    else if (this._IsIntegrityCheck)
      return;
    if (!this._IsIntegrityCheck && adj.VoidAdjNbr.HasValue)
      this.VoidOrigAdjustment(adj);
    if (this.ShouldProcessAsPrepaymentRequestApplication(apRegister1, adj))
    {
      this.ProcessPrepaymentRequestApplication(apRegister1, adj);
    }
    else
    {
      Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
      Decimal? nullable1 = adj.CuryAdjdDiscAmt;
      Decimal? nullable2 = curyAdjdAmt.HasValue & nullable1.HasValue ? new Decimal?(curyAdjdAmt.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable3 = adj.CuryAdjdWhTaxAmt;
      Decimal? nullable4;
      if (!(nullable2.HasValue & nullable3.HasValue))
      {
        nullable1 = new Decimal?();
        nullable4 = nullable1;
      }
      else
        nullable4 = new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault());
      Decimal? nullable5 = nullable4;
      Decimal? adjAmt = adj.AdjAmt;
      Decimal? nullable6 = adj.AdjDiscAmt;
      nullable1 = adjAmt.HasValue & nullable6.HasValue ? new Decimal?(adjAmt.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable7 = adj.AdjWhTaxAmt;
      Decimal? nullable8;
      if (!(nullable1.HasValue & nullable7.HasValue))
      {
        nullable6 = new Decimal?();
        nullable8 = nullable6;
      }
      else
        nullable8 = new Decimal?(nullable1.GetValueOrDefault() + nullable7.GetValueOrDefault());
      nullable3 = nullable8;
      bool? reverseGainLoss = adj.ReverseGainLoss;
      bool flag1 = false;
      Decimal? nullable9;
      if (!(reverseGainLoss.GetValueOrDefault() == flag1 & reverseGainLoss.HasValue))
      {
        nullable7 = adj.RGOLAmt;
        if (!nullable7.HasValue)
        {
          nullable1 = new Decimal?();
          nullable9 = nullable1;
        }
        else
          nullable9 = new Decimal?(-nullable7.GetValueOrDefault());
      }
      else
        nullable9 = adj.RGOLAmt;
      Decimal? nullable10 = nullable9;
      Decimal? nullable11;
      if (!(nullable3.HasValue & nullable10.HasValue))
      {
        nullable7 = new Decimal?();
        nullable11 = nullable7;
      }
      else
        nullable11 = new Decimal?(nullable3.GetValueOrDefault() + nullable10.GetValueOrDefault());
      Decimal? nullable12 = nullable11;
      if (apRegister1.IsPrepaymentInvoiceDocument() && apRegister1.Status == "U")
      {
        apRegister1.CuryDocBal = new Decimal?(0M);
        apRegister1.DocBal = new Decimal?(0M);
      }
      APRegister apRegister2 = apRegister1;
      nullable10 = apRegister2.CuryDocBal;
      nullable3 = nullable5;
      Decimal? nullable13;
      if (!(nullable10.HasValue & nullable3.HasValue))
      {
        nullable7 = new Decimal?();
        nullable13 = nullable7;
      }
      else
        nullable13 = new Decimal?(nullable10.GetValueOrDefault() - nullable3.GetValueOrDefault());
      apRegister2.CuryDocBal = nullable13;
      APRegister apRegister3 = apRegister1;
      nullable3 = apRegister3.DocBal;
      nullable10 = nullable12;
      Decimal? nullable14;
      if (!(nullable3.HasValue & nullable10.HasValue))
      {
        nullable7 = new Decimal?();
        nullable14 = nullable7;
      }
      else
        nullable14 = new Decimal?(nullable3.GetValueOrDefault() - nullable10.GetValueOrDefault());
      apRegister3.DocBal = nullable14;
      APRegister apRegister4 = apRegister1;
      nullable10 = apRegister4.CuryDiscBal;
      nullable3 = adj.CuryAdjdDiscAmt;
      Decimal? nullable15;
      if (!(nullable10.HasValue & nullable3.HasValue))
      {
        nullable7 = new Decimal?();
        nullable15 = nullable7;
      }
      else
        nullable15 = new Decimal?(nullable10.GetValueOrDefault() - nullable3.GetValueOrDefault());
      apRegister4.CuryDiscBal = nullable15;
      APRegister apRegister5 = apRegister1;
      nullable3 = apRegister5.DiscBal;
      nullable10 = adj.AdjDiscAmt;
      Decimal? nullable16;
      if (!(nullable3.HasValue & nullable10.HasValue))
      {
        nullable7 = new Decimal?();
        nullable16 = nullable7;
      }
      else
        nullable16 = new Decimal?(nullable3.GetValueOrDefault() - nullable10.GetValueOrDefault());
      apRegister5.DiscBal = nullable16;
      APRegister apRegister6 = apRegister1;
      nullable10 = apRegister6.CuryWhTaxBal;
      nullable3 = adj.CuryAdjdWhTaxAmt;
      Decimal? nullable17;
      if (!(nullable10.HasValue & nullable3.HasValue))
      {
        nullable7 = new Decimal?();
        nullable17 = nullable7;
      }
      else
        nullable17 = new Decimal?(nullable10.GetValueOrDefault() - nullable3.GetValueOrDefault());
      apRegister6.CuryWhTaxBal = nullable17;
      APRegister apRegister7 = apRegister1;
      nullable3 = apRegister7.WhTaxBal;
      nullable10 = adj.AdjWhTaxAmt;
      Decimal? nullable18;
      if (!(nullable3.HasValue & nullable10.HasValue))
      {
        nullable7 = new Decimal?();
        nullable18 = nullable7;
      }
      else
        nullable18 = new Decimal?(nullable3.GetValueOrDefault() - nullable10.GetValueOrDefault());
      apRegister7.WhTaxBal = nullable18;
      APRegister apRegister8 = apRegister1;
      nullable10 = apRegister8.CuryDiscTaken;
      nullable3 = adj.CuryAdjdDiscAmt;
      Decimal? nullable19;
      if (!(nullable10.HasValue & nullable3.HasValue))
      {
        nullable7 = new Decimal?();
        nullable19 = nullable7;
      }
      else
        nullable19 = new Decimal?(nullable10.GetValueOrDefault() + nullable3.GetValueOrDefault());
      apRegister8.CuryDiscTaken = nullable19;
      APRegister apRegister9 = apRegister1;
      nullable3 = apRegister9.DiscTaken;
      nullable10 = adj.AdjDiscAmt;
      Decimal? nullable20;
      if (!(nullable3.HasValue & nullable10.HasValue))
      {
        nullable7 = new Decimal?();
        nullable20 = nullable7;
      }
      else
        nullable20 = new Decimal?(nullable3.GetValueOrDefault() + nullable10.GetValueOrDefault());
      apRegister9.DiscTaken = nullable20;
      APRegister apRegister10 = apRegister1;
      nullable10 = apRegister10.CuryTaxWheld;
      nullable3 = adj.CuryAdjdWhTaxAmt;
      Decimal? nullable21;
      if (!(nullable10.HasValue & nullable3.HasValue))
      {
        nullable7 = new Decimal?();
        nullable21 = nullable7;
      }
      else
        nullable21 = new Decimal?(nullable10.GetValueOrDefault() + nullable3.GetValueOrDefault());
      apRegister10.CuryTaxWheld = nullable21;
      APRegister apRegister11 = apRegister1;
      nullable3 = apRegister11.TaxWheld;
      nullable10 = adj.AdjWhTaxAmt;
      Decimal? nullable22;
      if (!(nullable3.HasValue & nullable10.HasValue))
      {
        nullable7 = new Decimal?();
        nullable22 = nullable7;
      }
      else
        nullable22 = new Decimal?(nullable3.GetValueOrDefault() + nullable10.GetValueOrDefault());
      apRegister11.TaxWheld = nullable22;
      APRegister apRegister12 = apRegister1;
      nullable10 = apRegister12.RGOLAmt;
      nullable3 = adj.RGOLAmt;
      Decimal? nullable23;
      if (!(nullable10.HasValue & nullable3.HasValue))
      {
        nullable7 = new Decimal?();
        nullable23 = nullable7;
      }
      else
        nullable23 = new Decimal?(nullable10.GetValueOrDefault() + nullable3.GetValueOrDefault());
      apRegister12.RGOLAmt = nullable23;
      nullable3 = apRegister1.CuryDiscBal;
      Decimal num1 = 0M;
      if (nullable3.GetValueOrDefault() == num1 & nullable3.HasValue)
        apRegister1.DiscBal = new Decimal?(0M);
      nullable3 = apRegister1.CuryWhTaxBal;
      Decimal num2 = 0M;
      if (nullable3.GetValueOrDefault() == num2 & nullable3.HasValue)
        apRegister1.WhTaxBal = new Decimal?(0M);
      nullable3 = apRegister1.CuryDocBal;
      Decimal num3 = 0M;
      if (nullable3.GetValueOrDefault() == num3 & nullable3.HasValue)
      {
        nullable3 = apRegister1.DocBal;
        Decimal num4 = 0M;
        if (!(nullable3.GetValueOrDefault() == num4 & nullable3.HasValue))
        {
          reverseGainLoss = adj.ReverseGainLoss;
          bool flag2 = false;
          Decimal? nullable24;
          if (!(reverseGainLoss.GetValueOrDefault() == flag2 & reverseGainLoss.HasValue))
          {
            nullable3 = apRegister1.DocBal;
            if (!nullable3.HasValue)
            {
              nullable10 = new Decimal?();
              nullable24 = nullable10;
            }
            else
              nullable24 = new Decimal?(-nullable3.GetValueOrDefault());
          }
          else
            nullable24 = apRegister1.DocBal;
          Decimal? nullable25 = nullable24;
          APAdjust apAdjust = adj;
          nullable3 = apAdjust.RGOLAmt;
          nullable10 = nullable25;
          Decimal? nullable26;
          if (!(nullable3.HasValue & nullable10.HasValue))
          {
            nullable7 = new Decimal?();
            nullable26 = nullable7;
          }
          else
            nullable26 = new Decimal?(nullable3.GetValueOrDefault() + nullable10.GetValueOrDefault());
          apAdjust.RGOLAmt = nullable26;
          APRegister apRegister13 = apRegister1;
          nullable10 = apRegister13.RGOLAmt;
          nullable3 = nullable25;
          Decimal? nullable27;
          if (!(nullable10.HasValue & nullable3.HasValue))
          {
            nullable7 = new Decimal?();
            nullable27 = nullable7;
          }
          else
            nullable27 = new Decimal?(nullable10.GetValueOrDefault() + nullable3.GetValueOrDefault());
          apRegister13.RGOLAmt = nullable27;
        }
      }
      if (!this._IsIntegrityCheck)
      {
        System.DateTime? adjgDocDate = adj.AdjgDocDate;
        System.DateTime? docDate = adjddoc.DocDate;
        if ((adjgDocDate.HasValue & docDate.HasValue ? (adjgDocDate.GetValueOrDefault() < docDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          throw new PXException("{0} cannot be less than Document Date.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<APPayment.adjDate>(this.APPayment_DocType_RefNbr.Cache)
          });
      }
      if (!this._IsIntegrityCheck && string.Compare(adj.AdjgTranPeriodID, adjddoc.TranPeriodID) < 0)
        throw new PXException("{0} cannot be less than Document Financial Period.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<APPayment.adjFinPeriodID>(this.APPayment_DocType_RefNbr.Cache)
        });
      if (adjdtran != null && adjdtran.AreAllKeysFilled<APTran>(this.APTran_TranType_RefNbr.Cache))
      {
        APTran apTran1 = adjdtran;
        APTran apTran2 = (APTran) this.APTran_TranType_RefNbr.Cache.Locate((object) apTran1);
        if (apTran2 != null)
          apTran1 = apTran2;
        else if (this._IsIntegrityCheck)
          return;
        APTran apTran3 = apTran1;
        nullable3 = apTran3.CuryTranBal;
        nullable10 = nullable5;
        Decimal? nullable28;
        if (!(nullable3.HasValue & nullable10.HasValue))
        {
          nullable7 = new Decimal?();
          nullable28 = nullable7;
        }
        else
          nullable28 = new Decimal?(nullable3.GetValueOrDefault() - nullable10.GetValueOrDefault());
        apTran3.CuryTranBal = nullable28;
        APTran apTran4 = apTran1;
        nullable10 = apTran4.TranBal;
        nullable3 = nullable12;
        Decimal? nullable29;
        if (!(nullable10.HasValue & nullable3.HasValue))
        {
          nullable7 = new Decimal?();
          nullable29 = nullable7;
        }
        else
          nullable29 = new Decimal?(nullable10.GetValueOrDefault() - nullable3.GetValueOrDefault());
        apTran4.TranBal = nullable29;
        APTran apTran5 = apTran1;
        nullable3 = apTran5.CuryCashDiscBal;
        nullable10 = adj.CuryAdjdDiscAmt;
        Decimal? nullable30;
        if (!(nullable3.HasValue & nullable10.HasValue))
        {
          nullable7 = new Decimal?();
          nullable30 = nullable7;
        }
        else
          nullable30 = new Decimal?(nullable3.GetValueOrDefault() - nullable10.GetValueOrDefault());
        apTran5.CuryCashDiscBal = nullable30;
        APTran apTran6 = apTran1;
        nullable10 = apTran6.CashDiscBal;
        nullable3 = adj.AdjDiscAmt;
        Decimal? nullable31;
        if (!(nullable10.HasValue & nullable3.HasValue))
        {
          nullable7 = new Decimal?();
          nullable31 = nullable7;
        }
        else
          nullable31 = new Decimal?(nullable10.GetValueOrDefault() - nullable3.GetValueOrDefault());
        apTran6.CashDiscBal = nullable31;
        nullable3 = apTran1.CuryCashDiscBal;
        Decimal num5 = 0M;
        if (nullable3.GetValueOrDefault() == num5 & nullable3.HasValue)
          apTran1.CashDiscBal = new Decimal?(0M);
        nullable3 = apTran1.CuryTranBal;
        Decimal num6 = 0M;
        if (nullable3.GetValueOrDefault() == num6 & nullable3.HasValue)
          apTran1.TranBal = new Decimal?(0M);
        nullable3 = apTran1.CuryOrigTranAmt;
        Decimal num7 = 0M;
        Sign sign1 = nullable3.GetValueOrDefault() < num7 & nullable3.HasValue ? Sign.Minus : Sign.Plus;
        if (!this._IsIntegrityCheck)
        {
          nullable10 = apTran1.CuryTranBal;
          Sign sign2 = sign1;
          Decimal? nullable32;
          if (!nullable10.HasValue)
          {
            nullable7 = new Decimal?();
            nullable32 = nullable7;
          }
          else
            nullable32 = new Decimal?(Sign.op_Multiply(nullable10.GetValueOrDefault(), sign2));
          nullable3 = nullable32;
          Decimal num8 = 0M;
          if (!(nullable3.GetValueOrDefault() < num8 & nullable3.HasValue))
          {
            nullable10 = apTran1.CuryCashDiscBal;
            Sign sign3 = sign1;
            Decimal? nullable33;
            if (!nullable10.HasValue)
            {
              nullable7 = new Decimal?();
              nullable33 = nullable7;
            }
            else
              nullable33 = new Decimal?(Sign.op_Multiply(nullable10.GetValueOrDefault(), sign3));
            nullable3 = nullable33;
            Decimal num9 = 0M;
            if (!(nullable3.GetValueOrDefault() < num9 & nullable3.HasValue))
              goto label_110;
          }
          throw new PXException(Sign.op_Equality(sign1, Sign.Plus) ? "The line balance will go negative. The document will not be released." : "The line balance will go positive. The document will not be released.");
        }
label_110:
        this.APTran_TranType_RefNbr.Update(apTran1);
      }
      this.Caches[typeof (APAdjust)].Update((object) adj);
      APRegister apRegister14 = (APRegister) this.APDocument.Cache.Update((object) apRegister1);
    }
  }

  public virtual void CloseInvoiceAndClearBalances(APRegister apdoc)
  {
    apdoc.CuryDiscBal = new Decimal?(0M);
    apdoc.DiscBal = new Decimal?(0M);
    apdoc.CuryWhTaxBal = new Decimal?(0M);
    apdoc.WhTaxBal = new Decimal?(0M);
    apdoc.DocBal = new Decimal?(0M);
    int num = this.HasAnyUnreleasedAdjustment(apdoc) ? 1 : 0;
    if (num == 0)
      apdoc.OpenDoc = new bool?(false);
    this.SetClosedPeriodsFromLatestApplication(apdoc);
    this.APDocument.Cache.Update((object) apdoc);
    if (num != 0)
      return;
    this.RaiseInvoiceEvent(apdoc, PXEntityEventBase<APInvoice>.Container<APInvoice.Events>.Select((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (ev => ev.CloseDocument)));
    this.RaisePaymentEvent(apdoc, PXEntityEventBase<APPayment>.Container<APPayment.Events>.Select((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (ev => ev.CloseDocument)));
  }

  public virtual void OpenInvoiceAndRecoverBalances(APRegister apdoc)
  {
    Decimal? curyDocBal = apdoc.CuryDocBal;
    Decimal? curyOrigDocAmt = apdoc.CuryOrigDocAmt;
    if (curyDocBal.GetValueOrDefault() == curyOrigDocAmt.GetValueOrDefault() & curyDocBal.HasValue == curyOrigDocAmt.HasValue)
    {
      apdoc.CuryDiscBal = apdoc.CuryOrigDiscAmt;
      apdoc.DiscBal = apdoc.OrigDiscAmt;
      apdoc.CuryWhTaxBal = apdoc.CuryOrigWhTaxAmt;
      apdoc.WhTaxBal = apdoc.OrigWhTaxAmt;
      apdoc.CuryDiscTaken = new Decimal?(0M);
      apdoc.DiscTaken = new Decimal?(0M);
      apdoc.CuryTaxWheld = new Decimal?(0M);
      apdoc.TaxWheld = new Decimal?(0M);
    }
    apdoc.OpenDoc = new bool?(true);
    apdoc.ClosedDate = new System.DateTime?();
    apdoc.ClosedFinPeriodID = (string) null;
    apdoc.ClosedTranPeriodID = (string) null;
    this.APDocument.Cache.Update((object) apdoc);
    this.RaiseInvoiceEvent(apdoc, PXEntityEventBase<APInvoice>.Container<APInvoice.Events>.Select((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (ev => ev.OpenDocument)));
    this.RaisePaymentEvent(apdoc, PXEntityEventBase<APPayment>.Container<APPayment.Events>.Select((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (ev => ev.OpenDocument)));
  }

  public virtual void CloseInvoiceAndRecoverBalances(APRegister apdoc)
  {
    APAdjust2 apAdjust2 = (APAdjust2) PXSelectBase<APAdjust2, PXViewOf<APAdjust2>.BasedOn<SelectFromBase<APAdjust2, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjust2.adjdDocType, Equal<P.AsString.ASCII>>>>, PX.Data.And<BqlOperand<APAdjust2.adjdRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<APAdjust2.adjgDocType, IBqlString>.IsEqual<APDocType.debitAdj>>>.Aggregate<To<Sum<APAdjust.adjAmt>, Sum<APAdjust2.curyAdjdAmt>>>>.Config>.Select((PXGraph) this, (object) apdoc.DocType, (object) apdoc.RefNbr);
    Decimal? nullable1;
    ref Decimal? local1 = ref nullable1;
    Decimal? nullable2 = (Decimal?) apAdjust2?.AdjAmt;
    Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
    local1 = new Decimal?(valueOrDefault1);
    Decimal? nullable3;
    ref Decimal? local2 = ref nullable3;
    Decimal? nullable4;
    if (apAdjust2 == null)
    {
      nullable2 = new Decimal?();
      nullable4 = nullable2;
    }
    else
      nullable4 = apAdjust2.CuryAdjdAmt;
    nullable2 = nullable4;
    Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
    local2 = new Decimal?(valueOrDefault2);
    APRegister apRegister1 = apdoc;
    nullable2 = apdoc.CuryOrigDocAmt;
    Decimal? nullable5 = nullable3;
    Decimal? nullable6 = nullable2.HasValue & nullable5.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
    apRegister1.CuryDocBal = nullable6;
    APRegister apRegister2 = apdoc;
    nullable5 = apdoc.OrigDocAmt;
    nullable2 = nullable1;
    Decimal? nullable7 = nullable5.HasValue & nullable2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    apRegister2.DocBal = nullable7;
    apdoc.CuryDiscBal = apdoc.CuryOrigDiscAmt;
    apdoc.DiscBal = apdoc.OrigDiscAmt;
    apdoc.CuryDiscTaken = new Decimal?(0M);
    apdoc.DiscTaken = new Decimal?(0M);
    apdoc.OpenDoc = new bool?(true);
    nullable2 = apdoc.DocBal;
    Decimal num1 = 0M;
    if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
    {
      nullable2 = apdoc.CuryDocBal;
      Decimal num2 = 0M;
      if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
        this.SetClosedPeriodsFromLatestApplication(apdoc);
    }
    this.APDocument.Cache.Update((object) apdoc);
    nullable2 = apdoc.DocBal;
    Decimal num3 = 0M;
    if (nullable2.GetValueOrDefault() == num3 & nullable2.HasValue)
    {
      nullable2 = apdoc.CuryDocBal;
      Decimal num4 = 0M;
      if (nullable2.GetValueOrDefault() == num4 & nullable2.HasValue)
        goto label_9;
    }
    if (!apdoc.Voided.GetValueOrDefault())
      goto label_17;
label_9:
    foreach (PXResult<APAdjust> pxResult in PXSelectBase<APAdjust, PXViewOf<APAdjust>.BasedOn<SelectFromBase<APAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjust.adjdDocType, Equal<P.AsString.ASCII>>>>, PX.Data.And<BqlOperand<APAdjust.adjdRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<APAdjust.released, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) this, (object) apdoc.DocType, (object) apdoc.RefNbr))
      this.Caches[typeof (APAdjust)].Delete((object) (APAdjust) pxResult);
    this.RaiseInvoiceEvent(apdoc, PXEntityEventBase<APInvoice>.Container<APInvoice.Events>.Select((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (ev => ev.VoidDocument)));
label_17:
    this.RaiseInvoiceEvent(apdoc, PXEntityEventBase<APInvoice>.Container<APInvoice.Events>.Select((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (ev => ev.CloseDocument)));
    this.RaisePaymentEvent(apdoc, PXEntityEventBase<APPayment>.Container<APPayment.Events>.Select((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (ev => ev.CloseDocument)));
  }

  /// <summary>
  /// The method to verify invoice balances and close it if needed.
  /// This verification should be called after
  /// release process of invoice applications.
  /// </summary>
  public virtual void VerifyAdjustedDocumentAndClose(APRegister adjddoc)
  {
    APRegister apRegister1 = adjddoc;
    APRegister copy = (APRegister) this.APDocument.Cache.Locate((object) apRegister1);
    if (copy != null)
      PXCache<APRegister>.RestoreCopy(apRegister1, copy);
    else if (this._IsIntegrityCheck)
      return;
    Decimal? curyDiscBal = apRegister1.CuryDiscBal;
    Decimal num1 = 0M;
    if (curyDiscBal.GetValueOrDefault() == num1 & curyDiscBal.HasValue)
      apRegister1.DiscBal = new Decimal?(0M);
    if (!this._IsIntegrityCheck)
    {
      Decimal? curyDocBal1 = apRegister1.CuryDocBal;
      Decimal num2 = 0M;
      Decimal? nullable;
      if (!(curyDocBal1.GetValueOrDefault() < num2 & curyDocBal1.HasValue))
      {
        Decimal? curyDocBal2 = apRegister1.CuryDocBal;
        nullable = apRegister1.CuryOrigDocAmt;
        if (!(curyDocBal2.GetValueOrDefault() > nullable.GetValueOrDefault() & curyDocBal2.HasValue & nullable.HasValue))
          goto label_10;
      }
      string localizedLabel = PXStringListAttribute.GetLocalizedLabel<APRegister.docType>(this.APDocument.Cache, (object) apRegister1);
      nullable = apRegister1.CuryDocBal;
      Decimal num3 = 0M;
      throw new PXException(nullable.GetValueOrDefault() < num3 & nullable.HasValue ? "The balance of {0}: {1} will go negative. The document will not be released." : "The balance of {0}: {1} will exceed the document's total amount. The document will not be released.", new object[2]
      {
        (object) localizedLabel,
        (object) apRegister1.RefNbr
      });
    }
label_10:
    if (apRegister1.IsOriginalRetainageDocument())
    {
      if (this.IsFullyProcessedOriginalRetainageDocument(apRegister1))
        this.CloseInvoiceAndClearBalances(apRegister1);
      else
        this.OpenInvoiceAndRecoverBalances(apRegister1);
    }
    else if (apRegister1.IsPrepaymentInvoiceDocument() && apRegister1.PendingPayment.GetValueOrDefault() && apRegister1.HasZeroBalance<APRegister.curyDocBal, APTran.curyTranBal>((PXGraph) this))
      this.CloseInvoiceAndRecoverBalances(apRegister1);
    else if (apRegister1.HasZeroBalance<APRegister.curyDocBal, APTran.curyTranBal>((PXGraph) this))
      this.CloseInvoiceAndClearBalances(apRegister1);
    else
      this.OpenInvoiceAndRecoverBalances(apRegister1);
    APRegister apRegister2 = (APRegister) this.APDocument.Cache.Update((object) apRegister1);
    APRegister retainageDocument;
    if (!apRegister2.IsChildRetainageDocument() || (retainageDocument = this.GetOriginalRetainageDocument(apRegister2)) == null)
      return;
    if (this.IsFullyProcessedOriginalRetainageDocument(retainageDocument))
      this.CloseInvoiceAndClearBalances(retainageDocument);
    else
      this.OpenInvoiceAndRecoverBalances(retainageDocument);
    this.APDocument.Cache.Update((object) retainageDocument);
  }

  public virtual void VoidOrigAdjustment(APAdjust adj)
  {
    string[] strArray = APPaymentType.GetVoidedAPDocType(adj.AdjgDocType);
    if (strArray.Length == 0)
      strArray = new string[1]{ adj.AdjgDocType };
    APAdjust apAdjust1 = (APAdjust) PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.adjgDocType, PX.Data.In<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>, And<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.adjNbr, Equal<Required<APAdjust.adjNbr>>, And<APAdjust.adjdLineNbr, Equal<Required<APAdjust.adjdLineNbr>>>>>>>>>.Config>.Select((PXGraph) this, (object) strArray, (object) adj.AdjgRefNbr, (object) adj.AdjdDocType, (object) adj.AdjdRefNbr, (object) adj.VoidAdjNbr, (object) adj.AdjdLineNbr);
    if (apAdjust1 == null)
      return;
    bool? nullable1 = apAdjust1.Voided;
    if (nullable1.Value)
      throw new PXException("This document application is already voided. Document will not be released.");
    apAdjust1.Voided = new bool?(true);
    this.Caches[typeof (APAdjust)].Update((object) apAdjust1);
    APAdjust apAdjust2 = adj;
    Decimal? nullable2 = apAdjust1.AdjAmt;
    Decimal? nullable3 = nullable2.HasValue ? new Decimal?(-nullable2.GetValueOrDefault()) : new Decimal?();
    apAdjust2.AdjAmt = nullable3;
    APAdjust apAdjust3 = adj;
    nullable2 = apAdjust1.RGOLAmt;
    Decimal? nullable4 = nullable2.HasValue ? new Decimal?(-nullable2.GetValueOrDefault()) : new Decimal?();
    apAdjust3.RGOLAmt = nullable4;
    this.Caches[typeof (APAdjust)].Update((object) adj);
    if (!(apAdjust1.AdjgDocType == "ADR"))
      return;
    nullable1 = apAdjust1.AdjdHasPPDTaxes;
    if (!nullable1.GetValueOrDefault())
      return;
    APRegister apRegister = (APRegister) PXSelectBase<APRegister, PXSelect<APRegister, Where<APRegister.docType, Equal<APDocType.debitAdj>, And<APRegister.refNbr, Equal<Required<APRegister.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) apAdjust1.AdjgRefNbr);
    if (apRegister == null)
      return;
    nullable1 = apRegister.PendingPPD;
    if (!nullable1.GetValueOrDefault())
      return;
    PXUpdate<Set<APAdjust.pPDVATAdjRefNbr, Null, Set<APAdjust.pPDVATAdjDocType, Null>>, APAdjust, Where<APAdjust.pendingPPD, Equal<True>, And<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.pPDVATAdjRefNbr, Equal<Required<APAdjust.pPDVATAdjRefNbr>>>>>>>.Update((PXGraph) this, (object) apAdjust1.AdjdDocType, (object) apAdjust1.AdjdRefNbr, (object) apAdjust1.AdjgRefNbr);
  }

  /// <summary>
  /// Processes the prepayment request applied to Check or Void Check.
  /// </summary>
  /// <param name="prepaymentRequest">The prepayment request.</param>
  /// <param name="prepaymentAdj">The prepayment application.</param>
  protected virtual void ProcessPrepaymentRequestApplication(
    APRegister prepaymentRequest,
    APAdjust prepaymentAdj)
  {
    if (prepaymentAdj.AdjgDocType == "VCK" || prepaymentAdj.VoidAdjNbr.HasValue && prepaymentAdj.AdjgDocType != "VRF")
    {
      Decimal? curyOrigDocAmt = prepaymentRequest.CuryOrigDocAmt;
      Decimal? curyDocBal = prepaymentRequest.CuryDocBal;
      if (System.Math.Abs((curyOrigDocAmt.HasValue & curyDocBal.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() - curyDocBal.GetValueOrDefault()) : new Decimal?()).Value) > 0M)
        throw new PXException("The {0} payment cannot be voided. The {1} prepayment that is paid with this payment has applications. To void the payment, first reverse the released applications, and then delete the unreleased applications of the prepayment.", new object[2]
        {
          (object) prepaymentAdj.AdjgRefNbr,
          (object) prepaymentRequest.RefNbr
        });
      using (IEnumerator<PXResult<APAdjust>> enumerator = this.APAdjust_AdjgDocType_RefNbr_VendorID.Select((object) prepaymentRequest.DocType, (object) prepaymentRequest.RefNbr, (object) this._IsIntegrityCheck, (object) prepaymentRequest.AdjCntr).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          APAdjust current = (APAdjust) enumerator.Current;
          throw new PXException("The {0} payment cannot be voided. The {1} prepayment that is paid with this payment has applications. To void the payment, first reverse the released applications, and then delete the unreleased applications of the prepayment.", new object[2]
          {
            (object) prepaymentAdj.AdjgRefNbr,
            (object) prepaymentRequest.RefNbr
          });
        }
      }
      if (PXSelectBase<APAdjust, PXViewOf<APAdjust>.BasedOn<SelectFromBase<APAdjust, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<APInvoice>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjust.adjdDocType, Equal<APInvoice.docType>>>>>.And<BqlOperand<APAdjust.adjdRefNbr, IBqlString>.IsEqual<APInvoice.refNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjust.adjgDocType, Equal<P.AsString.ASCII>>>>, PX.Data.And<BqlOperand<APAdjust.adjgRefNbr, IBqlString>.IsEqual<P.AsString>>>, PX.Data.And<BqlOperand<APInvoice.released, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<APAdjust.released, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) this, (object) prepaymentRequest.DocType, (object) prepaymentRequest.RefNbr).Any<PXResult<APAdjust>>())
        throw new PXException("The {0} payment cannot be voided. The {1} prepayment that is paid with this payment has applications. To void the payment, first reverse the released applications, and then delete the unreleased applications of the prepayment.", new object[2]
        {
          (object) prepaymentAdj.AdjgRefNbr,
          (object) prepaymentRequest.RefNbr
        });
      prepaymentRequest.OpenDoc = new bool?(false);
      prepaymentRequest.Voided = new bool?(true);
      FinPeriodIDAttribute.SetPeriodsByMaster<APRegister.closedFinPeriodID>(this.APDocument.Cache, (object) prepaymentRequest, prepaymentAdj.AdjgTranPeriodID);
      prepaymentRequest.ClosedDate = prepaymentAdj.AdjgDocDate;
      prepaymentRequest.CuryDocBal = new Decimal?(0M);
      prepaymentRequest.DocBal = new Decimal?(0M);
      prepaymentRequest = (APRegister) this.APDocument.Cache.Update((object) prepaymentRequest);
      this.RaiseInvoiceEvent(prepaymentRequest, PXEntityEventBase<APInvoice>.Container<APInvoice.Events>.Select((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (ev => ev.VoidDocument)));
    }
    else if (prepaymentAdj.AdjgDocType == "VRF")
    {
      prepaymentRequest.OpenDoc = new bool?(true);
      APRegister apRegister1 = prepaymentRequest;
      Decimal? curyDocBal = apRegister1.CuryDocBal;
      Decimal? curyAdjdAmt = prepaymentAdj.CuryAdjdAmt;
      apRegister1.CuryDocBal = curyDocBal.HasValue & curyAdjdAmt.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - curyAdjdAmt.GetValueOrDefault()) : new Decimal?();
      APRegister apRegister2 = prepaymentRequest;
      Decimal? docBal = apRegister2.DocBal;
      Decimal? adjAmt = prepaymentAdj.AdjAmt;
      apRegister2.DocBal = docBal.HasValue & adjAmt.HasValue ? new Decimal?(docBal.GetValueOrDefault() - adjAmt.GetValueOrDefault()) : new Decimal?();
      this.APDocument.Cache.Update((object) prepaymentRequest);
      this.RaiseInvoiceEvent(prepaymentRequest, PXEntityEventBase<APInvoice>.Container<APInvoice.Events>.Select((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (ev => ev.OpenDocument)));
    }
    else
    {
      if (!EnumerableExtensions.IsIn<string>(prepaymentAdj.AdjgDocType, "CHK", "PPM"))
        return;
      if (this._IsIntegrityCheck)
      {
        prepaymentRequest.AdjustBalance<APRegister, APAdjust>(prepaymentAdj, -1M);
        APRegister apRegister = prepaymentRequest;
        Decimal? docBal = apRegister.DocBal;
        Decimal? rgolAmt = prepaymentAdj.RGOLAmt;
        apRegister.DocBal = docBal.HasValue & rgolAmt.HasValue ? new Decimal?(docBal.GetValueOrDefault() - rgolAmt.GetValueOrDefault()) : new Decimal?();
        this.APDocument.Cache.Update((object) prepaymentRequest);
        this.VerifyAdjustedDocumentAndClose(prepaymentRequest);
      }
      else
        this.ProcessPrepaymentRequestAppliedToCheck(prepaymentRequest, prepaymentAdj);
    }
  }

  /// <summary>
  /// Processes the prepayment request applied to check. Verifies that prepayment is paid in full, if neccessary checks for discrepancy and moves it to RGOL account.
  /// Then creates the Payment part of prepayment request which is shown on the "Checks and Payments" screen.
  /// </summary>
  /// <param name="prepaymentRequest">The prepayment request.</param>
  /// <param name="prepaymentAdj">The prepayment adjustment.</param>
  protected virtual void ProcessPrepaymentRequestAppliedToCheck(
    APRegister prepaymentRequest,
    APAdjust prepaymentAdj)
  {
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) this.CurrencyInfo_CuryInfoID.Select((object) prepaymentRequest.CuryInfoID);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = (PX.Objects.CM.Extensions.CurrencyInfo) this.CurrencyInfo_CuryInfoID.Select((object) prepaymentAdj.AdjgCuryInfoID);
    FullBalanceDelta fullBalanceDelta = prepaymentAdj.GetFullBalanceDelta();
    Decimal adjustedBalanceDelta1 = fullBalanceDelta.CurrencyAdjustedBalanceDelta;
    if (System.Math.Abs(prepaymentRequest.CuryOrigDocAmt.Value - adjustedBalanceDelta1) != 0M)
      throw new PXException("Prepayment '{0}' is not paid in full. Document will not be released.", new object[1]
      {
        (object) prepaymentRequest.RefNbr
      });
    PX.Objects.CM.Extensions.CurrencyInfo origCuryInfoToUse;
    if (currencyInfo2.CuryID == currencyInfo1.CuryID)
    {
      origCuryInfoToUse = currencyInfo2;
    }
    else
    {
      origCuryInfoToUse = (PX.Objects.CM.Extensions.CurrencyInfo) this.CurrencyInfo_CuryInfoID.Select((object) prepaymentAdj.AdjdCuryInfoID);
      if (currencyInfo1.CuryID == currencyInfo1.BaseCuryID)
      {
        Decimal adjustedBalanceDelta2 = fullBalanceDelta.BaseAdjustedBalanceDelta;
        Decimal num1 = prepaymentRequest.OrigDocAmt.Value - adjustedBalanceDelta2;
        if (System.Math.Abs(num1) != 0M)
        {
          Decimal? nullable1;
          ref Decimal? local = ref nullable1;
          bool? reverseGainLoss = prepaymentAdj.ReverseGainLoss;
          bool flag = false;
          Decimal num2 = reverseGainLoss.GetValueOrDefault() == flag & reverseGainLoss.HasValue ? num1 : -num1;
          local = new Decimal?(num2);
          APAdjust apAdjust = prepaymentAdj;
          Decimal? rgolAmt = apAdjust.RGOLAmt;
          Decimal? nullable2 = nullable1;
          apAdjust.RGOLAmt = rgolAmt.HasValue & nullable2.HasValue ? new Decimal?(rgolAmt.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        }
      }
    }
    APPayment apPayment = (APPayment) this.APPayment_DocType_RefNbr.Cache.Extend<APRegister>(prepaymentRequest);
    apPayment.CreatedByID = prepaymentRequest.CreatedByID;
    apPayment.CreatedByScreenID = prepaymentRequest.CreatedByScreenID;
    apPayment.CreatedDateTime = prepaymentRequest.CreatedDateTime;
    apPayment.CashAccountID = new int?();
    apPayment.PaymentMethodID = (string) null;
    apPayment.ExtRefNbr = (string) null;
    apPayment.DocDate = prepaymentAdj.AdjgDocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<APPayment.finPeriodID>(this.APPayment_DocType_RefNbr.Cache, (object) apPayment, prepaymentAdj.AdjgTranPeriodID);
    apPayment.AdjDate = apPayment.DocDate;
    apPayment.AdjFinPeriodID = apPayment.FinPeriodID;
    apPayment.AdjTranPeriodID = apPayment.TranPeriodID;
    apPayment.Printed = new bool?(true);
    SharedRecordAttribute.DefaultRecord<APPayment.remitAddressID>(this.APPayment_DocType_RefNbr.Cache, (object) apPayment);
    SharedRecordAttribute.DefaultRecord<APPayment.remitContactID>(this.APPayment_DocType_RefNbr.Cache, (object) apPayment);
    OpenPeriodAttribute.SetValidatePeriod<APPayment.adjFinPeriodID>(this.APPayment_DocType_RefNbr.Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    this.APPayment_DocType_RefNbr.Cache.Update((object) apPayment);
    TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.APTran_TranType_RefNbr.Cache, (object) null, TaxCalc.NoCalc);
    this.GetExtension<APReleaseProcess.MultiCurrency>().UpdateCurrencyInfoForPrepayment(apPayment, origCuryInfoToUse);
    this.APDocument.Cache.SetStatus((object) prepaymentRequest, PXEntryStatus.Notchanged);
    if (!(prepaymentAdj.AdjgDocType == "PPM"))
      return;
    prepaymentAdj.RGOLAmt = new Decimal?(0M);
  }

  private void UpdateVoidedCheck(APRegister voidcheck)
  {
    foreach (string originalDocumentType in voidcheck.PossibleOriginalDocumentTypes())
    {
      foreach (PXResult<APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Vendor> pxResult1 in this.APPayment_DocType_RefNbr.Search<APPayment.vendorID>((object) voidcheck.VendorID, (object) originalDocumentType, (object) voidcheck.RefNbr))
      {
        APRegister apRegister = (APRegister) (APPayment) pxResult1;
        APRegister copy = (APRegister) this.APDocument.Cache.Locate((object) apRegister);
        if (copy != null)
          this.APDocument.Cache.RestoreCopy((object) apRegister, (object) copy);
        apRegister.Voided = new bool?(true);
        apRegister.OpenDoc = new bool?(false);
        apRegister.Hold = new bool?(false);
        apRegister.CuryDocBal = new Decimal?(0M);
        apRegister.DocBal = new Decimal?(0M);
        this.SetClosedPeriodsFromLatestApplication(apRegister);
        this.APDocument.Cache.Update((object) apRegister);
        this.RaisePaymentEvent(apRegister, PXEntityEventBase<APPayment>.Container<APPayment.Events>.Select((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (g => g.VoidDocument)));
        this.RaiseInvoiceEvent(apRegister, PXEntityEventBase<APInvoice>.Container<APInvoice.Events>.Select((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (g => g.VoidDocument)));
        Guid? noteId1 = voidcheck.NoteID;
        Guid? noteId2 = apRegister.NoteID;
        if ((noteId1.HasValue == noteId2.HasValue ? (noteId1.HasValue ? (noteId1.GetValueOrDefault() == noteId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          this.APDocument.Cache.RestoreCopy((object) voidcheck, (object) apRegister);
        PXCache cach = this.Caches[typeof (APAdjust)];
        if (!this._IsIntegrityCheck)
        {
          foreach (PXResult<APAdjust> pxResult2 in PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>, And<APAdjust.released, NotEqual<True>>>>>.Config>.Select((PXGraph) this, (object) apRegister.DocType, (object) apRegister.RefNbr))
          {
            APAdjust apAdjust = (APAdjust) pxResult2;
            cach.Delete((object) apAdjust);
          }
        }
      }
    }
  }

  private void VerifyVoidCheckNumberMatchesOriginalPayment(APPayment voidcheck)
  {
    if (this._IsIntegrityCheck)
      return;
    bool flag = false;
    foreach (string originalDocumentType in voidcheck.PossibleOriginalDocumentTypes())
    {
      foreach (PXResult<APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Vendor> pxResult in this.APPayment_DocType_RefNbr.Search<APPayment.vendorID>((object) voidcheck.VendorID, (object) originalDocumentType, (object) voidcheck.RefNbr))
      {
        APPayment apPayment = (APPayment) pxResult;
        if (string.Equals(voidcheck.ExtRefNbr, apPayment.ExtRefNbr, StringComparison.OrdinalIgnoreCase))
        {
          flag = true;
          break;
        }
      }
    }
    if (!flag)
      throw new PXException("Void Check must have the same Reference Number as the voided payment.");
  }

  protected void DeactivateOneTimeVendorIfAllDocsIsClosed(Vendor vendor)
  {
    if (vendor.VStatus != "T")
      return;
    if ((APRegister) PXSelectBase<APRegister, PXSelect<APRegister, Where<APRegister.vendorID, Equal<Required<APRegister.vendorID>>, And<APRegister.released, Equal<True>, And<APRegister.openDoc, Equal<True>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) vendor.BAccountID) != null)
      return;
    vendor.VStatus = "I";
    this.Caches[typeof (Vendor)].Update((object) vendor);
    this.Caches[typeof (Vendor)].Persist(PXDBOperation.Update);
    this.Caches[typeof (Vendor)].Persisted(false);
  }

  /// <summary>
  /// Ensures that no unreleased voiding document exists for the specified payment.
  /// (If the applications of the voided and the voiding document are not
  /// synchronized, it can lead to a balance discrepancy, see AC-78131).
  /// </summary>
  public static void EnsureNoUnreleasedVoidPaymentExists(
    PXGraph selectGraph,
    APRegister doc,
    APRegister payment,
    string actionDescription)
  {
    APRegister apRegister = HasUnreleasedVoidPayment<APRegister.docType, APRegister.refNbr>.Select(selectGraph, payment);
    if (apRegister != null && (!(apRegister.DocType == doc?.DocType) || !(apRegister.RefNbr == doc?.RefNbr)))
      throw new PXException("The {0} {1} cannot be {2} because an unreleased {3} document exists for this {4}. To proceed, delete or release the {5} {6} document.", new object[7]
      {
        (object) PXLocalizer.Localize(GetLabel.For<APDocType>(payment.DocType)),
        (object) payment.RefNbr,
        (object) PXLocalizer.Localize(actionDescription),
        (object) PXLocalizer.Localize(GetLabel.For<APDocType>(apRegister.DocType)),
        (object) PXLocalizer.Localize(GetLabel.For<APDocType>(payment.DocType)),
        (object) PXLocalizer.Localize(GetLabel.For<APDocType>(apRegister.DocType)),
        (object) apRegister.RefNbr
      });
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public static void EnsureNoUnreleasedVoidPaymentExists(
    PXGraph selectGraph,
    APRegister payment,
    string actionDescription)
  {
    APReleaseProcess.EnsureNoUnreleasedVoidPaymentExists(selectGraph, (APRegister) null, payment, actionDescription);
  }

  /// <summary>
  /// The method to release payment part.
  /// The maintenance screen is "Checks And Payments" (AP302000).
  /// </summary>
  public virtual void ProcessPayment(
    JournalEntry je,
    APRegister doc,
    PXResult<APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Vendor, PX.Objects.CA.CashAccount> res)
  {
    APPayment apPayment = (APPayment) res;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) res;
    PX.Objects.CM.Extensions.Currency currency = (PX.Objects.CM.Extensions.Currency) res;
    Vendor vendor = (Vendor) res;
    PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) res;
    APReleaseProcess.EnsureNoUnreleasedVoidPaymentExists((PXGraph) this, doc, (APRegister) apPayment, "released");
    VendorClass vendorClass = (VendorClass) PXSelectBase<VendorClass, PXSelectJoin<VendorClass, InnerJoin<APSetup, On<APSetup.dfltVendorClassID, Equal<VendorClass.vendorClassID>>>>.Config>.Select((PXGraph) this, (object[]) null);
    bool flag1 = apPayment.DocType == "QCK" || apPayment.DocType == "VQC" || apPayment.DocType == "RQC";
    if (!doc.Released.GetValueOrDefault())
    {
      PXCache<APRegister>.RestoreCopy((APRegister) apPayment, doc);
      doc.CuryDocBal = doc.CuryOrigDocAmt;
      doc.DocBal = doc.OrigDocAmt;
      bool isDebit = apPayment.DrCr == "D";
      PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
      glTran1.SummPost = new bool?(this.SummPost);
      glTran1.BranchID = cashAccount.BranchID;
      glTran1.AccountID = cashAccount.AccountID;
      glTran1.SubID = cashAccount.SubID;
      glTran1.CuryDebitAmt = isDebit ? apPayment.CuryOrigDocAmt : new Decimal?(0M);
      glTran1.DebitAmt = isDebit ? apPayment.OrigDocAmt : new Decimal?(0M);
      glTran1.CuryCreditAmt = isDebit ? new Decimal?(0M) : apPayment.CuryOrigDocAmt;
      glTran1.CreditAmt = isDebit ? new Decimal?(0M) : apPayment.OrigDocAmt;
      glTran1.TranType = apPayment.DocType;
      glTran1.TranClass = apPayment.DocClass;
      glTran1.RefNbr = apPayment.RefNbr;
      glTran1.TranDesc = apPayment.DocDesc;
      glTran1.TranDate = apPayment.DocDate;
      FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran1, apPayment.TranPeriodID);
      glTran1.CuryInfoID = currencyInfo.CuryInfoID;
      glTran1.Released = new bool?(true);
      glTran1.CATranID = apPayment.CATranID;
      glTran1.ReferenceID = apPayment.VendorID;
      glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
      glTran1.NonBillable = new bool?(true);
      this.InsertPaymentTransaction(je, glTran1, new APReleaseProcess.GLTranInsertionContext()
      {
        APRegisterRecord = doc
      });
      PX.Objects.GL.GLTran glTran2 = new PX.Objects.GL.GLTran();
      glTran2.SummPost = new bool?(true);
      if (!APPaymentType.CanHaveBalance(apPayment.DocType))
        glTran2.ZeroPost = new bool?(false);
      glTran2.BranchID = apPayment.BranchID;
      glTran2.AccountID = apPayment.APAccountID;
      glTran2.ReclassificationProhibited = new bool?(true);
      glTran2.SubID = apPayment.APSubID;
      glTran2.CuryDebitAmt = isDebit ? new Decimal?(0M) : apPayment.CuryOrigDocAmt;
      glTran2.DebitAmt = isDebit ? new Decimal?(0M) : apPayment.OrigDocAmt;
      glTran2.CuryCreditAmt = isDebit ? apPayment.CuryOrigDocAmt : new Decimal?(0M);
      glTran2.CreditAmt = isDebit ? apPayment.OrigDocAmt : new Decimal?(0M);
      glTran2.TranType = apPayment.DocType;
      glTran2.TranClass = "P";
      glTran2.RefNbr = apPayment.RefNbr;
      glTran2.TranDesc = apPayment.DocDesc;
      glTran2.TranDate = apPayment.DocDate;
      FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran2, apPayment.TranPeriodID);
      glTran2.CuryInfoID = currencyInfo.CuryInfoID;
      glTran2.Released = new bool?(true);
      glTran2.ReferenceID = apPayment.VendorID;
      glTran2.ProjectID = ProjectDefaultAttribute.NonProject();
      glTran2.NonBillable = new bool?(true);
      this.UpdateHistory(glTran2, vendor);
      this.UpdateHistory(glTran2, vendor, currencyInfo);
      this.InsertPaymentTransaction(je, glTran2, new APReleaseProcess.GLTranInsertionContext()
      {
        APRegisterRecord = doc
      });
      if (this.IsMigratedDocumentForProcessing(doc))
        this.ProcessMigratedDocument(je, glTran2, (APRegister) apPayment, isDebit, vendor, currencyInfo);
      foreach (PXResult<APPaymentChargeTran> pxResult in this.APPaymentChargeTran_DocType_RefNbr.Select((object) doc.DocType, (object) doc.RefNbr))
      {
        APPaymentChargeTran paymentChargeTran = (APPaymentChargeTran) pxResult;
        bool flag2 = doc.DocType == "RQC" ? paymentChargeTran.DrCr == "D" : paymentChargeTran.GetCASign() == 1;
        PX.Objects.GL.GLTran glTran3 = new PX.Objects.GL.GLTran();
        glTran3.SummPost = new bool?(this.SummPost);
        glTran3.BranchID = cashAccount.BranchID;
        glTran3.AccountID = cashAccount.AccountID;
        glTran3.SubID = cashAccount.SubID;
        glTran3.CuryDebitAmt = flag2 ? paymentChargeTran.CuryTranAmt : new Decimal?(0M);
        glTran3.DebitAmt = flag2 ? paymentChargeTran.TranAmt : new Decimal?(0M);
        glTran3.CuryCreditAmt = flag2 ? new Decimal?(0M) : paymentChargeTran.CuryTranAmt;
        glTran3.CreditAmt = flag2 ? new Decimal?(0M) : paymentChargeTran.TranAmt;
        glTran3.TranType = paymentChargeTran.DocType;
        glTran3.TranClass = apPayment.DocClass;
        glTran3.RefNbr = paymentChargeTran.RefNbr;
        glTran3.TranLineNbr = paymentChargeTran.LineNbr;
        glTran3.TranDesc = paymentChargeTran.TranDesc;
        glTran3.TranDate = paymentChargeTran.TranDate;
        FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran3, paymentChargeTran.TranPeriodID);
        glTran3.CuryInfoID = currencyInfo.CuryInfoID;
        glTran3.Released = new bool?(true);
        glTran3.CATranID = paymentChargeTran.CashTranID;
        glTran3.ReferenceID = apPayment.VendorID;
        this.InsertPaymentChargeTransaction(je, glTran3, new APReleaseProcess.GLTranInsertionContext()
        {
          APRegisterRecord = doc,
          APPaymentChargeTranRecord = paymentChargeTran
        });
        PX.Objects.GL.GLTran glTran4 = new PX.Objects.GL.GLTran();
        glTran4.SummPost = new bool?(true);
        glTran4.ZeroPost = new bool?(false);
        glTran4.BranchID = apPayment.BranchID;
        glTran4.AccountID = paymentChargeTran.AccountID;
        glTran4.SubID = paymentChargeTran.SubID;
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
        FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran4, paymentChargeTran.TranPeriodID);
        glTran4.CuryInfoID = currencyInfo.CuryInfoID;
        glTran4.Released = new bool?(true);
        glTran4.ReferenceID = apPayment.VendorID;
        this.InsertPaymentChargeTransaction(je, glTran4, new APReleaseProcess.GLTranInsertionContext()
        {
          APRegisterRecord = doc,
          APPaymentChargeTranRecord = paymentChargeTran
        });
        paymentChargeTran.Released = new bool?(true);
        this.APPaymentChargeTran_DocType_RefNbr.Update(paymentChargeTran);
      }
      if (!flag1)
        this.ProcessOriginTranPost(apPayment);
      doc.Voided = new bool?(false);
      doc.OpenDoc = new bool?(true);
      doc.ClosedDate = new System.DateTime?();
      doc.ClosedFinPeriodID = (string) null;
      doc.ClosedTranPeriodID = (string) null;
      if (apPayment.VoidAppl.GetValueOrDefault())
      {
        this.VerifyVoidCheckNumberMatchesOriginalPayment(apPayment);
      }
      else
      {
        PX.Objects.CA.PaymentMethod paymentMethod = (PX.Objects.CA.PaymentMethod) PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, (object) apPayment.PaymentMethodID);
        if (!this._IsIntegrityCheck && APPaymentEntry.MustPrintCheck((IPrintCheckControlable) apPayment, paymentMethod))
          throw new PXException("Check is not Printed and cannot be released.");
      }
    }
    else if (this._IsIntegrityCheck && doc.DocType == "PPM")
    {
      int? adjCntr = doc.AdjCntr;
      int num = 0;
      if (adjCntr.GetValueOrDefault() == num & adjCntr.HasValue)
      {
        doc.CuryDocBal = new Decimal?(0M);
        doc.DocBal = new Decimal?(0M);
      }
    }
    if (flag1)
    {
      if (!this._IsIntegrityCheck)
      {
        foreach (System.Type table in this.Caches<APAdjust>().GetExtensionTables() ?? new List<System.Type>())
        {
          PXDataFieldRestrict[] dataFieldRestrictArray = new PXDataFieldRestrict[2]
          {
            new PXDataFieldRestrict("adjgDocType", (object) doc.DocType),
            new PXDataFieldRestrict("adjgRefNbr", (object) doc.RefNbr)
          };
          PXDatabase.ForceDelete(table, dataFieldRestrictArray);
        }
        PXDatabase.Delete<APAdjust>((PXDataFieldRestrict) new PXDataFieldRestrict<APAdjust.adjgDocType>(PXDbType.Char, new int?(3), (object) doc.DocType, PXComp.EQ), (PXDataFieldRestrict) new PXDataFieldRestrict<APAdjust.adjgRefNbr>(PXDbType.VarChar, new int?(15), (object) doc.RefNbr, PXComp.EQ));
        this.CreateSelfApplicationForDocument(doc);
      }
      if (doc.DocType == "VQC")
        this.VerifyVoidCheckNumberMatchesOriginalPayment(apPayment);
      APRegister apRegister1 = doc;
      Decimal? curyDocBal = apRegister1.CuryDocBal;
      Decimal? curyOrigDiscAmt = doc.CuryOrigDiscAmt;
      Decimal? nullable1 = doc.CuryOrigWhTaxAmt;
      Decimal? nullable2 = curyOrigDiscAmt.HasValue & nullable1.HasValue ? new Decimal?(curyOrigDiscAmt.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable3;
      if (!(curyDocBal.HasValue & nullable2.HasValue))
      {
        nullable1 = new Decimal?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new Decimal?(curyDocBal.GetValueOrDefault() + nullable2.GetValueOrDefault());
      apRegister1.CuryDocBal = nullable3;
      APRegister apRegister2 = doc;
      nullable2 = apRegister2.DocBal;
      nullable1 = doc.OrigDiscAmt;
      Decimal? nullable4 = doc.OrigWhTaxAmt;
      Decimal? nullable5 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable6;
      if (!(nullable2.HasValue & nullable5.HasValue))
      {
        nullable4 = new Decimal?();
        nullable6 = nullable4;
      }
      else
        nullable6 = new Decimal?(nullable2.GetValueOrDefault() + nullable5.GetValueOrDefault());
      apRegister2.DocBal = nullable6;
      doc.ClosedDate = doc.DocDate;
      doc.ClosedFinPeriodID = doc.FinPeriodID;
      doc.ClosedTranPeriodID = doc.TranPeriodID;
    }
    doc.Released = new bool?(true);
  }

  /// <summary>
  /// The method to verify invoice balances and close it if needed.
  /// This verification should be called after
  /// release process of payment and applications.
  /// </summary>
  public virtual void VerifyDocumentBalanceAndClose(APRegister apdoc)
  {
    if ((apdoc.IsOriginalRetainageDocument() ? (this.IsFullyProcessedOriginalRetainageDocument(apdoc) ? 1 : 0) : (apdoc.HasZeroBalance<APRegister.curyDocBal, APTran.curyTranBal>((PXGraph) this) ? 1 : 0)) != 0)
      this.CloseInvoiceAndClearBalances(apdoc);
    else
      this.OpenInvoiceAndRecoverBalances(apdoc);
  }

  /// <summary>
  /// The method to verify payment balances and close it if needed.
  /// This verification should be called after
  /// release process of payment and applications.
  /// </summary>
  public virtual void VerifyPaymentRoundAndClose(
    JournalEntry je,
    APRegister paymentRegister,
    APPayment payment,
    Vendor paymentVendor,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    PX.Objects.CM.Extensions.Currency paycury,
    Tuple<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo> lastAdjustment)
  {
    APAdjust prev_adj = lastAdjustment.Item1;
    PX.Objects.CM.Extensions.CurrencyInfo info = lastAdjustment.Item2;
    Decimal? nullable1;
    if (!this._IsIntegrityCheck)
    {
      int num1;
      if (!payment.VoidAppl.GetValueOrDefault())
      {
        nullable1 = paymentRegister.CuryDocBal;
        Decimal num2 = 0M;
        num1 = nullable1.GetValueOrDefault() < num2 & nullable1.HasValue ? 1 : 0;
      }
      else
      {
        nullable1 = paymentRegister.CuryDocBal;
        Decimal num3 = 0M;
        num1 = nullable1.GetValueOrDefault() > num3 & nullable1.HasValue ? 1 : 0;
      }
      if (num1 != 0)
        throw new PXException("Document balance will become negative. The document will not be released.");
    }
    nullable1 = paymentRegister.CuryDocBal;
    Decimal num4 = 0M;
    if (nullable1.GetValueOrDefault() == num4 & nullable1.HasValue)
    {
      nullable1 = paymentRegister.DocBal;
      Decimal num5 = 0M;
      if (!(nullable1.GetValueOrDefault() == num5 & nullable1.HasValue) && prev_adj.AdjdRefNbr != null)
      {
        if (prev_adj.VoidAppl.GetValueOrDefault() || object.Equals((object) new_info.CuryID, (object) new_info.BaseCuryID))
          throw new PXException("The document balance has not been recalculated in the base currency. Please report a bug.");
        APAdjust apAdjust1 = prev_adj;
        nullable1 = apAdjust1.AdjAmt;
        Decimal? docBal1 = paymentRegister.DocBal;
        apAdjust1.AdjAmt = nullable1.HasValue & docBal1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + docBal1.GetValueOrDefault()) : new Decimal?();
        bool? reverseGainLoss = prev_adj.ReverseGainLoss;
        bool flag = false;
        Decimal? nullable2;
        if (!(reverseGainLoss.GetValueOrDefault() == flag & reverseGainLoss.HasValue))
        {
          Decimal? docBal2 = paymentRegister.DocBal;
          if (!docBal2.HasValue)
          {
            nullable1 = new Decimal?();
            nullable2 = nullable1;
          }
          else
            nullable2 = new Decimal?(-docBal2.GetValueOrDefault());
        }
        else
          nullable2 = paymentRegister.DocBal;
        Decimal? nullable3 = nullable2;
        APAdjust apAdjust2 = prev_adj;
        Decimal? rgolAmt1 = apAdjust2.RGOLAmt;
        nullable1 = nullable3;
        apAdjust2.RGOLAmt = rgolAmt1.HasValue & nullable1.HasValue ? new Decimal?(rgolAmt1.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
        prev_adj = (APAdjust) this.Caches[typeof (APAdjust)].Update((object) prev_adj);
        foreach (APTranPost apTranPost in this.Caches<APTranPost>().Inserted.Cast<APTranPost>().Where<APTranPost>((Func<APTranPost, bool>) (d =>
        {
          Guid? refNoteId = d.RefNoteID;
          Guid? noteId = prev_adj.NoteID;
          if (refNoteId.HasValue != noteId.HasValue)
            return false;
          return !refNoteId.HasValue || refNoteId.GetValueOrDefault() == noteId.GetValueOrDefault();
        })))
        {
          apTranPost.Amt = apTranPost.Type == "R" ? new Decimal?(0M) : prev_adj.AdjAmt;
          apTranPost.RGOLAmt = prev_adj.RGOLAmt;
        }
        PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
        glTran1.SummPost = new bool?(this.SummPost);
        glTran1.BranchID = payment.BranchID;
        PX.Objects.GL.GLTran glTran2 = glTran1;
        Decimal? nullable4 = nullable3;
        Decimal num6 = 0M;
        int? nullable5 = nullable4.GetValueOrDefault() < num6 & nullable4.HasValue ? paycury.RoundingGainAcctID : paycury.RoundingLossAcctID;
        glTran2.AccountID = nullable5;
        PX.Objects.GL.GLTran glTran3 = glTran1;
        nullable4 = nullable3;
        Decimal num7 = 0M;
        int? nullable6 = nullable4.GetValueOrDefault() < num7 & nullable4.HasValue ? GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingGainSubID>((PXGraph) je, glTran1.BranchID, paycury) : GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingLossSubID>((PXGraph) je, glTran1.BranchID, paycury);
        glTran3.SubID = nullable6;
        glTran1.OrigAccountID = prev_adj.AdjdAPAcct;
        glTran1.OrigSubID = prev_adj.AdjdAPSub;
        PX.Objects.GL.GLTran glTran4 = glTran1;
        nullable4 = nullable3;
        Decimal num8 = 0M;
        Decimal? nullable7 = nullable4.GetValueOrDefault() > num8 & nullable4.HasValue ? nullable3 : new Decimal?(0M);
        glTran4.DebitAmt = nullable7;
        glTran1.CuryDebitAmt = new Decimal?(0M);
        PX.Objects.GL.GLTran glTran5 = glTran1;
        nullable4 = nullable3;
        Decimal num9 = 0M;
        Decimal? nullable8;
        if (!(nullable4.GetValueOrDefault() < num9 & nullable4.HasValue))
        {
          nullable8 = new Decimal?(0M);
        }
        else
        {
          nullable4 = nullable3;
          nullable8 = nullable4.HasValue ? new Decimal?(-nullable4.GetValueOrDefault()) : new Decimal?();
        }
        glTran5.CreditAmt = nullable8;
        glTran1.CuryCreditAmt = new Decimal?(0M);
        glTran1.TranType = prev_adj.AdjgDocType;
        glTran1.TranClass = "R";
        glTran1.RefNbr = prev_adj.AdjgRefNbr;
        glTran1.TranDesc = payment.DocDesc;
        glTran1.TranDate = prev_adj.AdjgDocDate;
        FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran1, prev_adj.AdjgTranPeriodID);
        glTran1.CuryInfoID = new_info.CuryInfoID;
        glTran1.Released = new bool?(true);
        glTran1.ReferenceID = payment.VendorID;
        glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
        this.UpdateHistory(glTran1, paymentVendor);
        this.UpdateHistory(glTran1, paymentVendor, info);
        PXCache cache = this.APPayment_DocType_RefNbr.Cache;
        APPayment apPayment1 = new APPayment();
        apPayment1.DocType = prev_adj.AdjdDocType;
        apPayment1.RefNbr = prev_adj.AdjdRefNbr;
        APPayment apPayment2 = (APPayment) cache.Locate((object) apPayment1);
        Decimal? nullable9;
        if (apPayment2 != null)
        {
          APPayment apPayment3 = apPayment2;
          nullable4 = apPayment3.RGOLAmt;
          nullable9 = nullable3;
          apPayment3.RGOLAmt = nullable4.HasValue & nullable9.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable9.GetValueOrDefault()) : new Decimal?();
          this.APPayment_DocType_RefNbr.Cache.Update((object) apPayment2);
        }
        else
        {
          APRegister apRegister1 = (APRegister) this.APDocument.Cache.Locate((object) new APRegister()
          {
            DocType = prev_adj.AdjdDocType,
            RefNbr = prev_adj.AdjdRefNbr
          });
          if (apRegister1 != null)
          {
            APRegister apRegister2 = apRegister1;
            Decimal? rgolAmt2 = apRegister2.RGOLAmt;
            nullable4 = nullable3;
            apRegister2.RGOLAmt = rgolAmt2.HasValue & nullable4.HasValue ? new Decimal?(rgolAmt2.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            this.APDocument.Cache.Update((object) apRegister1);
          }
        }
        this.InsertAdjustmentsRoundingTransaction(je, glTran1, new APReleaseProcess.GLTranInsertionContext()
        {
          APRegisterRecord = paymentRegister,
          APAdjustRecord = prev_adj
        });
        PX.Objects.GL.GLTran glTran6 = new PX.Objects.GL.GLTran();
        glTran6.SummPost = new bool?(true);
        glTran6.ZeroPost = new bool?(false);
        glTran6.BranchID = payment.BranchID;
        glTran6.AccountID = payment.APAccountID;
        glTran6.ReclassificationProhibited = new bool?(true);
        glTran6.SubID = payment.APSubID;
        PX.Objects.GL.GLTran glTran7 = glTran6;
        nullable4 = nullable3;
        Decimal num10 = 0M;
        Decimal? nullable10 = nullable4.GetValueOrDefault() > num10 & nullable4.HasValue ? nullable3 : new Decimal?(0M);
        glTran7.CreditAmt = nullable10;
        glTran6.CuryCreditAmt = new Decimal?(0M);
        PX.Objects.GL.GLTran glTran8 = glTran6;
        nullable4 = nullable3;
        Decimal num11 = 0M;
        Decimal? nullable11;
        if (!(nullable4.GetValueOrDefault() < num11 & nullable4.HasValue))
        {
          nullable11 = new Decimal?(0M);
        }
        else
        {
          nullable4 = nullable3;
          if (!nullable4.HasValue)
          {
            nullable9 = new Decimal?();
            nullable11 = nullable9;
          }
          else
            nullable11 = new Decimal?(-nullable4.GetValueOrDefault());
        }
        glTran8.DebitAmt = nullable11;
        glTran6.CuryDebitAmt = new Decimal?(0M);
        glTran6.TranType = prev_adj.AdjgDocType;
        glTran6.TranClass = "P";
        glTran6.RefNbr = prev_adj.AdjgRefNbr;
        glTran6.TranDesc = payment.DocDesc;
        glTran6.TranDate = prev_adj.AdjgDocDate;
        FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran6, prev_adj.AdjgTranPeriodID);
        glTran6.CuryInfoID = new_info.CuryInfoID;
        glTran6.Released = new bool?(true);
        glTran6.ReferenceID = payment.VendorID;
        glTran6.OrigAccountID = prev_adj.AdjdAPAcct;
        glTran6.OrigSubID = prev_adj.AdjdAPSub;
        glTran6.ProjectID = ProjectDefaultAttribute.NonProject();
        this.UpdateHistory(glTran6, paymentVendor);
        this.UpdateHistory(glTran6, paymentVendor, new_info);
        this.InsertAdjustmentsRoundingTransaction(je, glTran6, new APReleaseProcess.GLTranInsertionContext()
        {
          APRegisterRecord = paymentRegister,
          APAdjustRecord = prev_adj
        });
      }
    }
    bool flag1 = paymentRegister.IsOriginalRetainageDocument() ? this.IsFullyProcessedOriginalRetainageDocument(paymentRegister) : paymentRegister.HasZeroBalance<APRegister.curyDocBal, APTran.curyTranBal>((PXGraph) this);
    bool flag2 = prev_adj.AdjdRefNbr != null;
    if ((((!paymentRegister.IsOriginalRetainageDocument() ? 1 : (paymentRegister.DocType != "ADR" ? 1 : 0)) | (flag2 ? 1 : 0)) != 0 || !paymentRegister.IsRetainageReversing.GetValueOrDefault()) && (flag1 || payment.VoidAppl.GetValueOrDefault()))
      this.ClosePayment(paymentRegister, payment, paymentVendor);
    else
      this.ReopenPayment(paymentRegister, payment);
    APRegister retainageDocument;
    if (!paymentRegister.IsChildRetainageDocument() || (retainageDocument = this.GetOriginalRetainageDocument(paymentRegister)) == null)
      return;
    if (this.IsFullyProcessedOriginalRetainageDocument(retainageDocument))
      this.CloseInvoiceAndClearBalances(retainageDocument);
    else
      this.OpenInvoiceAndRecoverBalances(retainageDocument);
    this.APDocument.Cache.Update((object) retainageDocument);
  }

  protected virtual PX.Objects.CM.CurrencyInfo GetCurrencyInfoCopyForGL(
    JournalEntry je,
    PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    PX.Objects.CM.CurrencyInfo cm = info.GetCM();
    cm.CuryInfoID = new long?();
    cm.ModuleCode = "GL";
    return je.currencyinfo.Insert(cm) ?? cm;
  }

  /// <summary>
  /// The method to release applications only
  /// without payment part.
  /// </summary>
  protected virtual Tuple<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo> ProcessAdjustments(
    JournalEntry je,
    PXResultset<APAdjust> adjustments,
    APRegister paymentRegister,
    APPayment payment,
    Vendor paymentVendor,
    PX.Objects.CM.CurrencyInfo new_info,
    PX.Objects.CM.Extensions.Currency paycury)
  {
    APAdjust apAdjust = new APAdjust();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = new PX.Objects.CM.Extensions.CurrencyInfo();
    foreach (IGrouping<object, PXResult<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, APInvoice, APPayment, APRegisterAlias, APTran>> grouping in adjustments.AsEnumerable<PXResult<APAdjust>>().Where<PXResult<APAdjust>>((Func<PXResult<APAdjust>, bool>) (res => !((APAdjust) res).IsInitialApplication.GetValueOrDefault())).Cast<PXResult<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, APInvoice, APPayment, APRegisterAlias, APTran>>().GroupBy<PXResult<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, APInvoice, APPayment, APRegisterAlias, APTran>, object>((Func<PXResult<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, APInvoice, APPayment, APRegisterAlias, APTran>, object>) (res => (object) (APInvoice) res), this.APDocument.Cache.GetComparer()))
    {
      APRegister key = (APRegister) grouping.Key;
      bool flag = false;
      foreach (PXResult<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, APInvoice, APPayment, APRegisterAlias, APTran> pxResult in (IEnumerable<PXResult<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, APInvoice, APPayment, APRegisterAlias, APTran>>) grouping)
      {
        APAdjust adj = (APAdjust) pxResult;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = (PX.Objects.CM.Extensions.CurrencyInfo) pxResult;
        PX.Objects.CM.Extensions.Currency cury = (PX.Objects.CM.Extensions.Currency) pxResult;
        APInvoice apInvoice1 = (APInvoice) pxResult;
        APPayment apPayment1 = (APPayment) pxResult;
        APTran adjdtran = (APTran) pxResult;
        PX.Objects.CM.Extensions.CurrencyInfo info = PX.Objects.Common.Utilities.Clone<CurrencyInfo2, PX.Objects.CM.Extensions.CurrencyInfo>((PXGraph) this, PXResult.Unwrap<CurrencyInfo2>((object) pxResult));
        if (apInvoice1 != null && apInvoice1.RefNbr != null)
          PXCache<APRegister>.RestoreCopy((APRegister) apInvoice1, (APRegister) (APRegisterAlias) pxResult);
        else if (apPayment1 != null && apPayment1.RefNbr != null)
          PXCache<APRegister>.RestoreCopy((APRegister) apPayment1, (APRegister) (APRegisterAlias) pxResult);
        if (apInvoice1 != null && apInvoice1.PaymentsByLinesAllowed.GetValueOrDefault())
        {
          int? adjdLineNbr = adj.AdjdLineNbr;
          int num = 0;
          if (adjdLineNbr.GetValueOrDefault() == num & adjdLineNbr.HasValue)
            continue;
        }
        PXCache<APAdjust>.StoreOriginal((PXGraph) this, adj);
        PXCache<APInvoice>.StoreOriginal((PXGraph) this, apInvoice1);
        PXCache<APTran>.StoreOriginal((PXGraph) this, adjdtran);
        this.GetExtension<APReleaseProcess.MultiCurrency>().StoreResult(currencyInfo2);
        this.GetExtension<APReleaseProcess.MultiCurrency>().StoreResult(info);
        if (!this._IsIntegrityCheck)
        {
          bool? nullable1 = adj.PendingPPD;
          if (nullable1.GetValueOrDefault())
          {
            APInvoice apInvoice2 = apInvoice1;
            nullable1 = adj.Voided;
            bool? nullable2 = nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?();
            apInvoice2.PendingPPD = nullable2;
            this.APDocument.Cache.Update((object) apInvoice1);
          }
        }
        APReleaseProcess.EnsureNoUnreleasedVoidPaymentExists((PXGraph) this, paymentRegister, (APRegister) apPayment1, paymentRegister.DocType == "REF" ? "refunded" : "adjusted");
        if (apInvoice1.RefNbr == null || this._IsIntegrityCheck || !paymentVendor.Vendor1099.GetValueOrDefault())
        {
          if (!this.AP1099Tran_Select.Any<APTran>((object) apInvoice1.DocType, (object) apInvoice1.RefNbr))
            goto label_16;
        }
        this.Update1099(adj, (APRegister) apInvoice1, (APRegister) payment);
label_16:
        Decimal? nullable3 = adj.CuryAdjgAmt;
        Decimal num1 = 0M;
        if (nullable3.GetValueOrDefault() == num1 & nullable3.HasValue)
        {
          nullable3 = adj.CuryAdjgDiscAmt;
          Decimal num2 = 0M;
          if (nullable3.GetValueOrDefault() == num2 & nullable3.HasValue)
          {
            nullable3 = adj.CuryAdjgWhTaxAmt;
            Decimal num3 = 0M;
            if (nullable3.GetValueOrDefault() == num3 & nullable3.HasValue)
            {
              nullable3 = adj.CuryAdjgPPDAmt;
              Decimal num4 = 0M;
              if (nullable3.GetValueOrDefault() == num4 & nullable3.HasValue)
              {
                if (apInvoice1.IsOriginalRetainageDocument())
                {
                  nullable3 = apInvoice1.RetainageUnreleasedAmt;
                  Decimal num5 = 0M;
                  if (nullable3.GetValueOrDefault() == num5 & nullable3.HasValue)
                  {
                    nullable3 = apInvoice1.RetainageReleased;
                    Decimal num6 = 0M;
                    if (nullable3.GetValueOrDefault() == num6 & nullable3.HasValue)
                      goto label_24;
                  }
                }
                this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache.Delete((object) adj);
                continue;
              }
            }
          }
        }
label_24:
        bool? nullable4 = adj.Hold;
        if (nullable4.GetValueOrDefault())
          throw new PXException("Document is On Hold and cannot be released.");
        if (apInvoice1.RefNbr != null)
        {
          nullable4 = apInvoice1.ReleasedOrPrebooked;
          if (!nullable4.GetValueOrDefault() && apInvoice1.DocType != "QCK" && apInvoice1.DocType != "VQC" && apInvoice1.DocType != "RQC")
            throw new PXException("The {0} document with the {1} type cannot be released because it is applied to the unreleased {2} document with the {3} type.", new object[4]
            {
              (object) payment.RefNbr,
              (object) GetLabel.For<APDocType>(payment.DocType),
              (object) apInvoice1.RefNbr,
              (object) GetLabel.For<APDocType>(apInvoice1.DocType)
            });
          this.UpdateBalances(adj, (APRegister) apInvoice1, paymentVendor, adjdtran);
          this.UpdateWithholding(je, adj, (APRegister) apInvoice1, payment, paymentVendor, currencyInfo2);
        }
        else
          this.UpdateBalances(adj, (APRegister) apPayment1, paymentVendor);
        this.ProcessAdjustmentAdjusting(je, adj, payment, paymentVendor, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(new_info));
        this.ProcessAdjustmentAdjusted(je, adj, payment, paymentVendor, currencyInfo2, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(new_info));
        this.ProcessAdjustmentCashDiscount(je, adj, payment, paymentVendor, currencyInfo2, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(new_info));
        this.ProcessAdjustmentGOL(je, adj, payment, paymentVendor, paycury, cury, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(new_info), currencyInfo2);
        if (adj.AdjgDocType != adj.AdjdDocType || adj.AdjgRefNbr != adj.AdjdRefNbr)
        {
          APRegister apRegister1 = paymentRegister;
          nullable3 = apRegister1.CuryDocBal;
          Decimal? adjgBalSign1 = adj.AdjgBalSign;
          Decimal? curyAdjgAmt = adj.CuryAdjgAmt;
          Decimal? nullable5 = adjgBalSign1.HasValue & curyAdjgAmt.HasValue ? new Decimal?(adjgBalSign1.GetValueOrDefault() * curyAdjgAmt.GetValueOrDefault()) : new Decimal?();
          apRegister1.CuryDocBal = nullable3.HasValue & nullable5.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
          APRegister apRegister2 = paymentRegister;
          nullable5 = apRegister2.DocBal;
          Decimal? adjgBalSign2 = adj.AdjgBalSign;
          Decimal? adjAmt = adj.AdjAmt;
          nullable3 = adjgBalSign2.HasValue & adjAmt.HasValue ? new Decimal?(adjgBalSign2.GetValueOrDefault() * adjAmt.GetValueOrDefault()) : new Decimal?();
          apRegister2.DocBal = nullable5.HasValue & nullable3.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
          this.AdjustmentProcessingOnApplication(paymentRegister, adj);
        }
        this.ProcessSVATAdjustments(je, adj, apInvoice1, paymentRegister);
        if (!this._IsIntegrityCheck)
        {
          if (je.GLTranModuleBatNbr.Cache.IsInsertedUpdatedDeleted)
            je.Save.Press();
          if (!je.BatchModule.Cache.IsDirty)
            adj.AdjBatchNbr = je.BatchModule.Current.BatchNbr;
          adj.Released = new bool?(true);
          adj = (APAdjust) this.Caches[typeof (APAdjust)].Update((object) adj);
        }
        apAdjust = adj;
        currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) pxResult;
        this.ProcessAdjustmentTranPost(adj, (APRegister) apInvoice1, paymentRegister);
        PXCache cache = this.APPayment_DocType_RefNbr.Cache;
        APPayment apPayment2 = new APPayment();
        apPayment2.DocType = apInvoice1.DocType ?? apPayment1.DocType;
        apPayment2.RefNbr = apInvoice1.RefNbr ?? apPayment1.RefNbr;
        object obj = cache.Locate((object) apPayment2);
        if ((payment == null || this.APPayment_DocType_RefNbr.Cache.GetStatus(obj) != PXEntryStatus.Inserted ? (this.APPayment_DocType_RefNbr.Cache.GetStatus(obj) == PXEntryStatus.Updated ? 1 : 0) : 1) == 0)
        {
          if (apInvoice1.RefNbr != null)
            flag = true;
          else if (apPayment1.RefNbr != null)
            this.VerifyDocumentBalanceAndClose((APRegister) apPayment1);
        }
      }
      if (flag && key.RefNbr != null)
        this.VerifyAdjustedDocumentAndClose(key);
    }
    return new Tuple<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo>(apAdjust, currencyInfo1);
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  private void ProcessPayByLineDebitAdjAdjustment(APRegister paymentRegister, APAdjust adj)
  {
    Decimal? nullable1 = adj.CuryAdjgAmt;
    Decimal? nullable2 = adj.AdjAmt;
    IEnumerable<PXResult<APTran>> source = (IEnumerable<PXResult<APTran>>) PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Required<APPayment.docType>>, And<APTran.refNbr, Equal<Required<APPayment.refNbr>>>>, PX.Data.OrderBy<Asc<APTran.lineNbr>>>.Config>.Select((PXGraph) this, (object) paymentRegister.DocType, (object) paymentRegister.RefNbr);
    Decimal? nullable3 = nullable1;
    Decimal num1 = 0M;
    if (nullable3.GetValueOrDefault() < num1 & nullable3.HasValue)
      source = (IEnumerable<PXResult<APTran>>) source.OrderByDescending<PXResult<APTran>, int?>((Func<PXResult<APTran>, int?>) (_ => PXResult.Unwrap<APTran>((object) _).LineNbr));
    foreach (PXResult<APTran> pxResult in source)
    {
      APTran apTran1 = (APTran) pxResult;
      PXCache<APTran>.StoreOriginal((PXGraph) this, apTran1);
      APTran copy = PXCache<APTran>.CreateCopy(apTran1);
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
      APTran apTran2 = copy;
      nullable4 = apTran2.CuryTranBal;
      nullable5 = nullable9;
      Decimal? nullable14;
      if (!(nullable4.HasValue & nullable5.HasValue))
      {
        nullable7 = new Decimal?();
        nullable14 = nullable7;
      }
      else
        nullable14 = new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault());
      apTran2.CuryTranBal = nullable14;
      APTran apTran3 = copy;
      nullable5 = apTran3.TranBal;
      nullable4 = nullable11;
      Decimal? nullable15;
      if (!(nullable5.HasValue & nullable4.HasValue))
      {
        nullable7 = new Decimal?();
        nullable15 = nullable7;
      }
      else
        nullable15 = new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault());
      apTran3.TranBal = nullable15;
      this.APTran_TranType_RefNbr.Update(copy);
      nullable4 = nullable1;
      Decimal num2 = 0M;
      if (nullable4.GetValueOrDefault() == num2 & nullable4.HasValue)
        break;
    }
  }

  private void ProcessAdjustmentAdjusting(
    JournalEntry je,
    APAdjust adj,
    APPayment payment,
    Vendor paymentVendor,
    PX.Objects.CM.Extensions.CurrencyInfo new_info)
  {
    bool flag = adj.AdjgDocType == "PPI";
    PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
    glTran1.SummPost = new bool?(true);
    glTran1.ZeroPost = new bool?(false);
    glTran1.BranchID = adj.AdjgBranchID;
    glTran1.AccountID = flag ? payment.PrepaymentAccountID : payment.APAccountID;
    glTran1.ReclassificationProhibited = new bool?(true);
    glTran1.SubID = flag ? payment.PrepaymentSubID : payment.APSubID;
    PX.Objects.GL.GLTran glTran2 = glTran1;
    Decimal? adjgGlSign1 = adj.AdjgGLSign;
    Decimal num1 = 1M;
    Decimal? nullable1 = adjgGlSign1.GetValueOrDefault() == num1 & adjgGlSign1.HasValue ? new Decimal?(0M) : adj.AdjAmt;
    glTran2.DebitAmt = nullable1;
    PX.Objects.GL.GLTran glTran3 = glTran1;
    Decimal? adjgGlSign2 = adj.AdjgGLSign;
    Decimal num2 = 1M;
    Decimal? nullable2 = adjgGlSign2.GetValueOrDefault() == num2 & adjgGlSign2.HasValue ? new Decimal?(0M) : adj.CuryAdjgAmt;
    glTran3.CuryDebitAmt = nullable2;
    PX.Objects.GL.GLTran glTran4 = glTran1;
    Decimal? adjgGlSign3 = adj.AdjgGLSign;
    Decimal num3 = 1M;
    Decimal? nullable3 = adjgGlSign3.GetValueOrDefault() == num3 & adjgGlSign3.HasValue ? adj.AdjAmt : new Decimal?(0M);
    glTran4.CreditAmt = nullable3;
    PX.Objects.GL.GLTran glTran5 = glTran1;
    Decimal? adjgGlSign4 = adj.AdjgGLSign;
    Decimal num4 = 1M;
    Decimal? nullable4 = adjgGlSign4.GetValueOrDefault() == num4 & adjgGlSign4.HasValue ? adj.CuryAdjgAmt : new Decimal?(0M);
    glTran5.CuryCreditAmt = nullable4;
    glTran1.TranType = adj.AdjgDocType;
    glTran1.TranClass = "P";
    glTran1.RefNbr = adj.AdjgRefNbr;
    glTran1.TranDesc = payment.DocDesc;
    glTran1.TranDate = adj.AdjgDocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran1, adj.AdjgTranPeriodID);
    glTran1.CuryInfoID = new_info.CuryInfoID;
    glTran1.Released = new bool?(true);
    glTran1.ReferenceID = payment.VendorID;
    glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
    this.UpdateHistory(glTran1, paymentVendor);
    this.UpdateHistory(glTran1, paymentVendor, new_info);
    this.InsertAdjustmentsAdjustingTransaction(je, glTran1, new APReleaseProcess.GLTranInsertionContext()
    {
      APRegisterRecord = (APRegister) payment,
      APAdjustRecord = adj
    });
  }

  private void ProcessAdjustmentAdjusted(
    JournalEntry je,
    APAdjust adj,
    APPayment payment,
    Vendor paymentVendor,
    PX.Objects.CM.Extensions.CurrencyInfo vouch_info,
    PX.Objects.CM.Extensions.CurrencyInfo new_info)
  {
    int num1 = adj.AdjdDocType == "PPI" ? 1 : 0;
    PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
    glTran1.SummPost = new bool?(true);
    glTran1.ZeroPost = new bool?(false);
    glTran1.BranchID = adj.AdjdBranchID;
    glTran1.AccountID = adj.AdjdAPAcct;
    glTran1.ReclassificationProhibited = new bool?(true);
    glTran1.SubID = adj.AdjdAPSub;
    PX.Objects.GL.GLTran glTran2 = glTran1;
    Decimal? adjgGlSign1 = adj.AdjgGLSign;
    Decimal num2 = 1M;
    Decimal? nullable1;
    Decimal? nullable2;
    Decimal? nullable3;
    Decimal? nullable4;
    if (!(adjgGlSign1.GetValueOrDefault() == num2 & adjgGlSign1.HasValue))
    {
      nullable1 = adj.AdjAmt;
      Decimal? adjDiscAmt = adj.AdjDiscAmt;
      nullable2 = nullable1.HasValue & adjDiscAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + adjDiscAmt.GetValueOrDefault()) : new Decimal?();
      Decimal? adjWhTaxAmt = adj.AdjWhTaxAmt;
      nullable3 = nullable2.HasValue & adjWhTaxAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + adjWhTaxAmt.GetValueOrDefault()) : new Decimal?();
      Decimal? rgolAmt = adj.RGOLAmt;
      nullable4 = nullable3.HasValue & rgolAmt.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - rgolAmt.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable4 = new Decimal?(0M);
    glTran2.CreditAmt = nullable4;
    PX.Objects.GL.GLTran glTran3 = glTran1;
    Decimal? adjgGlSign2 = adj.AdjgGLSign;
    Decimal num3 = 1M;
    Decimal? nullable5;
    Decimal? nullable6;
    Decimal? nullable7;
    if (!(adjgGlSign2.GetValueOrDefault() == num3 & adjgGlSign2.HasValue))
    {
      if (!object.Equals((object) new_info.CuryID, (object) new_info.BaseCuryID))
      {
        nullable5 = adj.CuryAdjgAmt;
        nullable2 = adj.CuryAdjgDiscAmt;
        nullable6 = nullable5.HasValue & nullable2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        nullable3 = adj.CuryAdjgWhTaxAmt;
        if (!(nullable6.HasValue & nullable3.HasValue))
        {
          nullable2 = new Decimal?();
          nullable7 = nullable2;
        }
        else
          nullable7 = new Decimal?(nullable6.GetValueOrDefault() + nullable3.GetValueOrDefault());
      }
      else
        nullable7 = glTran1.CreditAmt;
    }
    else
      nullable7 = new Decimal?(0M);
    glTran3.CuryCreditAmt = nullable7;
    PX.Objects.GL.GLTran glTran4 = glTran1;
    nullable3 = adj.AdjgGLSign;
    Decimal num4 = 1M;
    Decimal? nullable8;
    if (!(nullable3.GetValueOrDefault() == num4 & nullable3.HasValue))
    {
      nullable8 = new Decimal?(0M);
    }
    else
    {
      Decimal? adjAmt = adj.AdjAmt;
      nullable1 = adj.AdjDiscAmt;
      nullable2 = adjAmt.HasValue & nullable1.HasValue ? new Decimal?(adjAmt.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      nullable5 = adj.AdjWhTaxAmt;
      Decimal? nullable9;
      if (!(nullable2.HasValue & nullable5.HasValue))
      {
        nullable1 = new Decimal?();
        nullable9 = nullable1;
      }
      else
        nullable9 = new Decimal?(nullable2.GetValueOrDefault() + nullable5.GetValueOrDefault());
      nullable3 = nullable9;
      nullable6 = adj.RGOLAmt;
      if (!(nullable3.HasValue & nullable6.HasValue))
      {
        nullable5 = new Decimal?();
        nullable8 = nullable5;
      }
      else
        nullable8 = new Decimal?(nullable3.GetValueOrDefault() + nullable6.GetValueOrDefault());
    }
    glTran4.DebitAmt = nullable8;
    PX.Objects.GL.GLTran glTran5 = glTran1;
    nullable6 = adj.AdjgGLSign;
    Decimal num5 = 1M;
    Decimal? nullable10;
    if (!(nullable6.GetValueOrDefault() == num5 & nullable6.HasValue))
      nullable10 = new Decimal?(0M);
    else if (!object.Equals((object) new_info.CuryID, (object) new_info.BaseCuryID))
    {
      nullable5 = adj.CuryAdjgAmt;
      nullable2 = adj.CuryAdjgDiscAmt;
      Decimal? nullable11;
      if (!(nullable5.HasValue & nullable2.HasValue))
      {
        nullable1 = new Decimal?();
        nullable11 = nullable1;
      }
      else
        nullable11 = new Decimal?(nullable5.GetValueOrDefault() + nullable2.GetValueOrDefault());
      nullable6 = nullable11;
      nullable3 = adj.CuryAdjgWhTaxAmt;
      if (!(nullable6.HasValue & nullable3.HasValue))
      {
        nullable2 = new Decimal?();
        nullable10 = nullable2;
      }
      else
        nullable10 = new Decimal?(nullable6.GetValueOrDefault() + nullable3.GetValueOrDefault());
    }
    else
      nullable10 = glTran1.DebitAmt;
    glTran5.CuryDebitAmt = nullable10;
    glTran1.TranType = adj.AdjgDocType;
    glTran1.TranClass = num1 == 0 ? APDocType.DocClass(adj.AdjdDocType) : "Y";
    glTran1.RefNbr = adj.AdjgRefNbr;
    glTran1.TranDesc = payment.DocDesc;
    glTran1.TranDate = adj.AdjgDocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran1, adj.AdjgTranPeriodID);
    glTran1.CuryInfoID = new_info.CuryInfoID;
    glTran1.Released = new bool?(true);
    glTran1.ReferenceID = payment.VendorID;
    glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
    this.UpdateHistory(glTran1, paymentVendor);
    this.InsertAdjustmentsAdjustedTransaction(je, glTran1, new APReleaseProcess.GLTranInsertionContext()
    {
      APRegisterRecord = (APRegister) payment,
      APAdjustRecord = adj
    });
    PX.Objects.GL.GLTran glTran6 = glTran1;
    nullable3 = adj.AdjgGLSign;
    Decimal num6 = 1M;
    Decimal? nullable12;
    if (!(nullable3.GetValueOrDefault() == num6 & nullable3.HasValue))
    {
      if (!object.Equals((object) vouch_info.CuryID, (object) vouch_info.BaseCuryID))
      {
        nullable2 = adj.CuryAdjdAmt;
        nullable5 = adj.CuryAdjdDiscAmt;
        Decimal? nullable13;
        if (!(nullable2.HasValue & nullable5.HasValue))
        {
          nullable1 = new Decimal?();
          nullable13 = nullable1;
        }
        else
          nullable13 = new Decimal?(nullable2.GetValueOrDefault() + nullable5.GetValueOrDefault());
        nullable3 = nullable13;
        nullable6 = adj.CuryAdjdWhTaxAmt;
        if (!(nullable3.HasValue & nullable6.HasValue))
        {
          nullable5 = new Decimal?();
          nullable12 = nullable5;
        }
        else
          nullable12 = new Decimal?(nullable3.GetValueOrDefault() + nullable6.GetValueOrDefault());
      }
      else
        nullable12 = glTran1.CreditAmt;
    }
    else
      nullable12 = new Decimal?(0M);
    glTran6.CuryCreditAmt = nullable12;
    PX.Objects.GL.GLTran glTran7 = glTran1;
    nullable6 = adj.AdjgGLSign;
    Decimal num7 = 1M;
    Decimal? nullable14;
    if (!(nullable6.GetValueOrDefault() == num7 & nullable6.HasValue))
      nullable14 = new Decimal?(0M);
    else if (!object.Equals((object) vouch_info.CuryID, (object) vouch_info.BaseCuryID))
    {
      nullable5 = adj.CuryAdjdAmt;
      nullable2 = adj.CuryAdjdDiscAmt;
      Decimal? nullable15;
      if (!(nullable5.HasValue & nullable2.HasValue))
      {
        nullable1 = new Decimal?();
        nullable15 = nullable1;
      }
      else
        nullable15 = new Decimal?(nullable5.GetValueOrDefault() + nullable2.GetValueOrDefault());
      nullable6 = nullable15;
      nullable3 = adj.CuryAdjdWhTaxAmt;
      if (!(nullable6.HasValue & nullable3.HasValue))
      {
        nullable2 = new Decimal?();
        nullable14 = nullable2;
      }
      else
        nullable14 = new Decimal?(nullable6.GetValueOrDefault() + nullable3.GetValueOrDefault());
    }
    else
      nullable14 = glTran1.DebitAmt;
    glTran7.CuryDebitAmt = nullable14;
    this.UpdateHistory(glTran1, paymentVendor, vouch_info);
  }

  private void PostReduceOnEarlyPaymentTran(
    JournalEntry je,
    APInvoice doc,
    Vendor vend,
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo,
    bool isCredit,
    Decimal curyAmount,
    Decimal amount)
  {
    PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran();
    glTran.SummPost = new bool?(this.SummPost);
    glTran.BranchID = doc.BranchID;
    glTran.AccountID = vend.DiscTakenAcctID;
    glTran.SubID = vend.DiscTakenSubID;
    glTran.OrigAccountID = doc.APAccountID;
    glTran.OrigSubID = doc.APSubID;
    glTran.DebitAmt = new Decimal?(isCredit ? 0M : amount);
    glTran.CuryDebitAmt = new Decimal?(isCredit ? 0M : curyAmount);
    glTran.CreditAmt = new Decimal?(isCredit ? amount : 0M);
    glTran.CuryCreditAmt = new Decimal?(isCredit ? curyAmount : 0M);
    glTran.TranType = doc.DocType;
    glTran.TranClass = "D";
    glTran.RefNbr = doc.RefNbr;
    glTran.TranDesc = doc.DocDesc;
    glTran.TranDate = doc.DocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran, doc.TranPeriodID);
    glTran.CuryInfoID = currencyInfo.CuryInfoID;
    glTran.Released = new bool?(true);
    glTran.ReferenceID = doc.VendorID;
    glTran.ProjectID = ProjectDefaultAttribute.NonProject();
    this.UpdateHistory(glTran, vend);
    this.InsertAdjustmentsCashDiscountTransaction(je, glTran, new APReleaseProcess.GLTranInsertionContext()
    {
      APRegisterRecord = (APRegister) doc
    });
    glTran.CuryDebitAmt = new Decimal?(isCredit ? 0M : curyAmount);
    glTran.CuryCreditAmt = new Decimal?(isCredit ? curyAmount : 0M);
    this.UpdateHistory(glTran, vend, currencyInfo);
  }

  private void ProcessAdjustmentCashDiscount(
    JournalEntry je,
    APAdjust adj,
    APPayment payment,
    Vendor paymentVendor,
    PX.Objects.CM.Extensions.CurrencyInfo vouch_info,
    PX.Objects.CM.Extensions.CurrencyInfo new_info)
  {
    PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
    glTran1.SummPost = new bool?(this.SummPost);
    glTran1.BranchID = adj.AdjdBranchID;
    glTran1.AccountID = paymentVendor.DiscTakenAcctID;
    glTran1.SubID = paymentVendor.DiscTakenSubID;
    glTran1.OrigAccountID = adj.AdjdAPAcct;
    glTran1.OrigSubID = adj.AdjdAPSub;
    PX.Objects.GL.GLTran glTran2 = glTran1;
    Decimal? adjgGlSign1 = adj.AdjgGLSign;
    Decimal num1 = 1M;
    Decimal? nullable1 = adjgGlSign1.GetValueOrDefault() == num1 & adjgGlSign1.HasValue ? new Decimal?(0M) : adj.AdjDiscAmt;
    glTran2.DebitAmt = nullable1;
    PX.Objects.GL.GLTran glTran3 = glTran1;
    Decimal? adjgGlSign2 = adj.AdjgGLSign;
    Decimal num2 = 1M;
    Decimal? nullable2 = adjgGlSign2.GetValueOrDefault() == num2 & adjgGlSign2.HasValue ? new Decimal?(0M) : adj.CuryAdjgDiscAmt;
    glTran3.CuryDebitAmt = nullable2;
    PX.Objects.GL.GLTran glTran4 = glTran1;
    Decimal? adjgGlSign3 = adj.AdjgGLSign;
    Decimal num3 = 1M;
    Decimal? nullable3 = adjgGlSign3.GetValueOrDefault() == num3 & adjgGlSign3.HasValue ? adj.AdjDiscAmt : new Decimal?(0M);
    glTran4.CreditAmt = nullable3;
    PX.Objects.GL.GLTran glTran5 = glTran1;
    Decimal? adjgGlSign4 = adj.AdjgGLSign;
    Decimal num4 = 1M;
    Decimal? nullable4 = adjgGlSign4.GetValueOrDefault() == num4 & adjgGlSign4.HasValue ? adj.CuryAdjgDiscAmt : new Decimal?(0M);
    glTran5.CuryCreditAmt = nullable4;
    glTran1.TranType = adj.AdjgDocType;
    glTran1.TranClass = "D";
    glTran1.RefNbr = adj.AdjgRefNbr;
    glTran1.TranDesc = payment.DocDesc;
    glTran1.TranDate = adj.AdjgDocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran1, adj.AdjgTranPeriodID);
    glTran1.CuryInfoID = new_info.CuryInfoID;
    glTran1.Released = new bool?(true);
    glTran1.ReferenceID = payment.VendorID;
    glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
    this.UpdateHistory(glTran1, paymentVendor);
    this.InsertAdjustmentsCashDiscountTransaction(je, glTran1, new APReleaseProcess.GLTranInsertionContext()
    {
      APRegisterRecord = (APRegister) payment,
      APAdjustRecord = adj
    });
    PX.Objects.GL.GLTran glTran6 = glTran1;
    Decimal? adjgGlSign5 = adj.AdjgGLSign;
    Decimal num5 = 1M;
    Decimal? nullable5 = adjgGlSign5.GetValueOrDefault() == num5 & adjgGlSign5.HasValue ? new Decimal?(0M) : adj.CuryAdjdDiscAmt;
    glTran6.CuryDebitAmt = nullable5;
    PX.Objects.GL.GLTran glTran7 = glTran1;
    Decimal? adjgGlSign6 = adj.AdjgGLSign;
    Decimal num6 = 1M;
    Decimal? nullable6 = adjgGlSign6.GetValueOrDefault() == num6 & adjgGlSign6.HasValue ? adj.CuryAdjdDiscAmt : new Decimal?(0M);
    glTran7.CuryCreditAmt = nullable6;
    this.UpdateHistory(glTran1, paymentVendor, vouch_info);
  }

  private void ProcessAdjustmentGOL(
    JournalEntry je,
    APAdjust adj,
    APPayment payment,
    Vendor vendor,
    PX.Objects.CM.Extensions.Currency paycury,
    PX.Objects.CM.Extensions.Currency cury,
    PX.Objects.CM.Extensions.CurrencyInfo new_info,
    PX.Objects.CM.Extensions.CurrencyInfo vouch_info)
  {
    if ((!cury.RealGainAcctID.HasValue || !cury.RealLossAcctID.HasValue) && (!paycury.RoundingGainAcctID.HasValue || !paycury.RoundingLossAcctID.HasValue))
      return;
    Decimal? rgolAmt = adj.RGOLAmt;
    Decimal num1 = 0M;
    int num2;
    if (!(rgolAmt.GetValueOrDefault() > num1 & rgolAmt.HasValue) || adj.VoidAppl.Value)
    {
      rgolAmt = adj.RGOLAmt;
      Decimal num3 = 0M;
      num2 = !(rgolAmt.GetValueOrDefault() < num3 & rgolAmt.HasValue) ? 0 : (adj.VoidAppl.Value ? 1 : 0);
    }
    else
      num2 = 1;
    bool flag1 = num2 != 0;
    rgolAmt = adj.RGOLAmt;
    Decimal num4 = 0M;
    Decimal? nullable1;
    if (!(rgolAmt.GetValueOrDefault() < num4 & rgolAmt.HasValue))
    {
      nullable1 = new Decimal?(0M);
    }
    else
    {
      Decimal num5 = -1M;
      rgolAmt = adj.RGOLAmt;
      nullable1 = rgolAmt.HasValue ? new Decimal?(num5 * rgolAmt.GetValueOrDefault()) : new Decimal?();
    }
    Decimal? nullable2 = nullable1;
    rgolAmt = adj.RGOLAmt;
    Decimal num6 = 0M;
    Decimal? nullable3 = rgolAmt.GetValueOrDefault() > num6 & rgolAmt.HasValue ? adj.RGOLAmt : new Decimal?(0M);
    bool flag2 = object.Equals((object) new_info.CuryID, (object) new_info.BaseCuryID) && !object.Equals((object) vouch_info.CuryID, (object) vouch_info.BaseCuryID);
    PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran()
    {
      SummPost = new bool?(this.SummPost),
      BranchID = adj.AdjdBranchID,
      OrigAccountID = adj.AdjdAPAcct,
      OrigSubID = adj.AdjdAPSub,
      DebitAmt = nullable2,
      CuryDebitAmt = flag2 ? nullable2 : new Decimal?(0M),
      CreditAmt = nullable3,
      CuryCreditAmt = flag2 ? nullable3 : new Decimal?(0M),
      TranType = adj.AdjgDocType,
      TranClass = "R",
      RefNbr = adj.AdjgRefNbr,
      TranDesc = payment.DocDesc,
      TranDate = adj.AdjgDocDate,
      CuryInfoID = new_info.CuryInfoID,
      Released = new bool?(true),
      ReferenceID = payment.VendorID,
      ProjectID = ProjectDefaultAttribute.NonProject()
    };
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(je.GLTranModuleBatNbr.Cache, (object) glTran, adj.AdjgTranPeriodID);
    int? nullable4 = cury.RealGainAcctID;
    if (nullable4.HasValue)
    {
      nullable4 = cury.RealLossAcctID;
      if (nullable4.HasValue)
      {
        glTran.AccountID = flag1 ? cury.RealGainAcctID : cury.RealLossAcctID;
        glTran.SubID = flag1 ? GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.realGainSubID>((PXGraph) je, adj.AdjdBranchID, cury) : GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.realLossSubID>((PXGraph) je, adj.AdjdBranchID, cury);
        goto label_11;
      }
    }
    glTran.AccountID = flag1 ? paycury.RoundingGainAcctID : paycury.RoundingLossAcctID;
    glTran.SubID = flag1 ? GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingGainSubID>((PXGraph) je, adj.AdjdBranchID, paycury) : GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Extensions.Currency.roundingLossSubID>((PXGraph) je, adj.AdjdBranchID, paycury);
label_11:
    this.UpdateHistory(glTran, vendor);
    this.InsertAdjustmentsGOLTransaction(je, glTran, new APReleaseProcess.GLTranInsertionContext()
    {
      APRegisterRecord = (APRegister) payment,
      APAdjustRecord = adj
    });
    glTran.CuryDebitAmt = new Decimal?(0M);
    glTran.CuryCreditAmt = new Decimal?(0M);
    this.UpdateHistory(glTran, vendor, vouch_info);
  }

  protected virtual void ProcessSVATAdjustments(
    JournalEntry je,
    APAdjust adj,
    APInvoice adjddoc,
    APRegister adjgdoc)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vATReporting>() || this._IsIntegrityCheck)
      return;
    foreach (PXResult<SVATConversionHist> pxResult in PXSelectBase<SVATConversionHist, PXSelect<SVATConversionHist, Where<SVATConversionHist.module, Equal<BatchModule.moduleAP>, And2<Where<SVATConversionHist.adjdDocType, Equal<Current<APAdjust.adjdDocType>>, And<SVATConversionHist.adjdRefNbr, Equal<Current<APAdjust.adjdRefNbr>>, Or<SVATConversionHist.adjdDocType, Equal<Current<APAdjust.adjgDocType>>, And<SVATConversionHist.adjdRefNbr, Equal<Current<APAdjust.adjgRefNbr>>>>>>, And<SVATConversionHist.reversalMethod, In3<SVATTaxReversalMethods.onPayments, SVATTaxReversalMethods.onPrepayment>, PX.Data.And<Where<SVATConversionHist.adjdDocType, Equal<SVATConversionHist.adjgDocType>, And<SVATConversionHist.adjdRefNbr, Equal<SVATConversionHist.adjgRefNbr>>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) adj
    }))
    {
      SVATConversionHist docSVAT = (SVATConversionHist) pxResult;
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
        nullable = adj.CuryAdjdWhTaxAmt;
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
        nullable = adj.CuryAdjgWhTaxAmt;
        Decimal valueOrDefault7 = nullable.GetValueOrDefault();
        Decimal num5 = num4 + valueOrDefault7;
        nullable = adjgdoc.CuryOrigDocAmt;
        Decimal valueOrDefault8 = nullable.GetValueOrDefault();
        num1 = num5 / valueOrDefault8;
      }
      Decimal num6 = num1;
      SVATConversionHist svatConversionHist = new SVATConversionHist()
      {
        Module = "AP",
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
      svatConversionHist.FillAmounts(this.GetExtension<APReleaseProcess.MultiCurrency>().GetCurrencyInfo(docSVAT.CuryInfoID), docSVAT.CuryTaxableAmt, docSVAT.CuryTaxAmt, num6);
      FinPeriodIDAttribute.SetPeriodsByMaster<SVATConversionHist.adjdFinPeriodID>(this.SVATConversionHistory.Cache, (object) svatConversionHist, adj.AdjdTranPeriodID);
      APRegister adjddoc1 = flag ? adjgdoc : (APRegister) adjddoc;
      APRegister apRegister = (APRegister) this.APDocument.Cache.Locate((object) adjddoc1);
      if (apRegister != null)
        adjddoc1 = apRegister;
      nullable = adjddoc1.CuryDocBal;
      Decimal num7 = 0M;
      if (nullable.GetValueOrDefault() == num7 & nullable.HasValue && !adjddoc.IsMigratedRecord.GetValueOrDefault())
        svatConversionHist = this.ProcessLastSVATRecord(adjddoc1, docSVAT, svatConversionHist, num6);
      SVATConversionHist adjSVAT = (SVATConversionHist) this.SVATConversionHistory.Cache.Insert((object) svatConversionHist);
      docSVAT.Processed = new bool?(false);
      docSVAT.AdjgFinPeriodID = (string) null;
      docSVAT.AdjgTranPeriodID = (string) null;
      PXTimeStampScope.PutPersisted(this.SVATConversionHistory.Cache, (object) docSVAT, (object) PXDatabase.SelectTimeStamp());
      this.SVATConversionHistory.Cache.Update((object) docSVAT);
      if (!this._IsIntegrityCheck)
        this.AfterSVATConversionHistoryInserted(je, adj, adjddoc, adjgdoc, docSVAT, adjSVAT);
    }
  }

  protected virtual SVATConversionHist ProcessLastSVATRecord(
    APRegister adjddoc,
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
      foreach (PXResult<SVATConversionHist> pxResult in this.SVATRecognitionRecords.Select((object) docSVAT.AdjdDocType, (object) docSVAT.AdjdRefNbr, (object) docSVAT.TaxID))
      {
        SVATConversionHist svatConversionHist1 = (SVATConversionHist) pxResult;
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
    APAdjust adj,
    APInvoice adjddoc,
    APRegister adjgdoc,
    SVATConversionHist docSVAT,
    SVATConversionHist adjSVAT)
  {
  }

  private void SegregateBatch(
    JournalEntry je,
    int? branchID,
    string curyID,
    System.DateTime? docDate,
    string finPeriodID,
    string description,
    PX.Objects.CM.Extensions.CurrencyInfo curyInfo)
  {
    JournalEntry.SegregateBatch(je, "AP", branchID, curyID, docDate, finPeriodID, description, curyInfo.GetCM(), (PX.Objects.GL.Batch) null);
  }

  protected virtual void PerformBasicReleaseChecks(APRegister document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (document.Hold.GetValueOrDefault())
      throw new ReleaseException("Document is On Hold and cannot be released.", Array.Empty<object>());
    if (document.Status == "E" || document.Status == "R")
      throw new ReleaseException("The document cannot be created on this form because an approval map is active on the Accounts Payable Preferences (AP101000) form.", Array.Empty<object>());
    if (document.IsMigratedRecord.GetValueOrDefault() && !document.Released.GetValueOrDefault() && !this.IsMigrationMode.GetValueOrDefault())
      throw new ReleaseException("The document cannot be released because it has been created in migration mode but now migration mode is deactivated. Delete the document or activate migration mode on the Accounts Payable Preferences (AP101000) form.", Array.Empty<object>());
    if (!document.IsMigratedRecord.GetValueOrDefault() && this.IsMigrationMode.GetValueOrDefault())
      throw new ReleaseException("The document cannot be released because it was created when migration mode was deactivated. To release the document, clear the Activate Migration Mode check box on the Accounts Payable Preferences (AP101000) form.", Array.Empty<object>());
    if (document.RetainageApply.GetValueOrDefault() && !PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>())
      throw new ReleaseException("A document with nonzero retainage amount cannot be processed if the Retainage Support feature is disabled on the Enable/Disable Features (CS100000) form.", Array.Empty<object>());
    if (AccountAttribute.GetAccount((PXGraph) this, document.APAccountID).IsCashAccount.GetValueOrDefault())
      throw new ReleaseException("Specify a correct {0} account. It must not be a cash account.", new object[1]
      {
        (object) "AP"
      });
    List<PX.Objects.GL.Branch> list = PXSelectBase<PX.Objects.GL.Branch, PXViewOf<PX.Objects.GL.Branch>.BasedOn<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<APAdjust>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.branchID, Equal<APAdjust.adjdBranchID>>>>>.Or<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<APAdjust.adjgBranchID>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjust.adjgDocType, Equal<P.AsString>>>>, PX.Data.And<BqlOperand<APAdjust.adjgRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.GL.Branch.active, IBqlBool>.IsNotEqual<True>>>>.Config>.Select((PXGraph) this, (object) document.DocType, (object) document.RefNbr).RowCast<PX.Objects.GL.Branch>().ToList<PX.Objects.GL.Branch>();
    if (list.Any<PX.Objects.GL.Branch>())
    {
      string empty = string.Empty;
      throw new ReleaseException("The application cannot be released because the following branches are not active: {0}.", new object[1]
      {
        (object) string.Join(", ", list.Select<PX.Objects.GL.Branch, string>((Func<PX.Objects.GL.Branch, string>) (x => x.BranchCD.Trim())).Distinct<string>())
      });
    }
  }

  public virtual APRegister OnBeforeRelease(APRegister apdoc)
  {
    if (apdoc.DocType == "INV")
    {
      foreach (PXResult<APTaxTran, PX.Objects.TX.Tax> pxResult in PXSelectBase<APTaxTran, PXSelectJoin<APTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Required<APInvoice.docType>>, And<APTaxTran.refNbr, Equal<Required<APInvoice.refNbr>>>>>>.Config>.Select((PXGraph) this, (object) apdoc.DocType, (object) apdoc.RefNbr))
      {
        PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) pxResult;
        APTaxTran apTaxTran = (APTaxTran) pxResult;
        if (tax.DeductibleVAT.GetValueOrDefault() && tax.TaxApplyTermsDisc == "P")
          throw new PXException("The document cannot be released because the system does not support processing of a partially deductible VAT with a cash discount that reduces taxable amount on early payment.");
      }
    }
    this.RefillAPPrintCheckDetail(apdoc);
    return apdoc;
  }

  protected virtual void RefillAPPrintCheckDetail(APRegister apdoc)
  {
    if (!(apdoc is APPayment payment))
      return;
    if (EnumerableExtensions.IsNotIn<string>(payment.DocType, "CHK", "PPM", "ADR", "REF", "VRF", new string[1]
    {
      "VCK"
    }) || string.IsNullOrEmpty(payment.ExtRefNbr))
      return;
    APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
    instance.SelectTimeStamp();
    instance.RefillAPPrintCheckDetail(payment);
    instance.Save.Press();
  }

  /// <summary>
  /// Common entry point.
  /// The method to release both types of documents - invoices and payments.
  /// </summary>
  public virtual List<APRegister> ReleaseDocProc(
    JournalEntry je,
    APRegister doc,
    bool isPrebooking,
    out List<PX.Objects.IN.INRegister> inDocs)
  {
    List<APRegister> apRegisterList = (List<APRegister>) null;
    inDocs = (List<PX.Objects.IN.INRegister>) null;
    if (isPrebooking)
    {
      foreach (PXResult<APTran> pxResult in this.APTran_TranType_RefNbr.Select((object) doc.DocType, (object) doc.RefNbr))
      {
        if (((APTran) pxResult).PONbr != null)
          throw new PXException("Accounts Payable document associated with purchase orders or purchase receipts cannot be pre-released.");
      }
    }
    this.PerformBasicReleaseChecks(doc);
    if (EnumerableExtensions.IsIn<string>(doc.DocType, "INV", "ACR", "ADR"))
    {
      Decimal? origDocAmt = doc.OrigDocAmt;
      Decimal num = 0M;
      if (origDocAmt.GetValueOrDefault() < num & origDocAmt.HasValue)
        throw new PXException("The document amount cannot be less than zero.");
    }
    if (doc.DocType == "INV")
    {
      if (PXSelectBase<PX.Objects.TX.Tax, PXSelectJoin<PX.Objects.TX.Tax, InnerJoin<APTaxTran, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<PX.Objects.TX.Tax.deductibleVAT, Equal<True>, And<PX.Objects.TX.Tax.taxApplyTermsDisc, Equal<CSTaxTermsDiscount.toPromtPayment>, And<APTaxTran.tranType, Equal<Required<APInvoice.docType>>, And<APTaxTran.refNbr, Equal<Required<APInvoice.refNbr>>>>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr).Any<PXResult<PX.Objects.TX.Tax>>())
        throw new PXException("The document cannot be released because the system does not support processing of a partially deductible VAT with a cash discount that reduces taxable amount on early payment.");
    }
    if (this.IsMigrationMode.GetValueOrDefault())
      je.SetOffline();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (!(doc is APInvoice apInvoice1))
      {
        if (doc is APPayment apPayment1)
          PXCache<APPayment>.StoreOriginal((PXGraph) this, apPayment1);
        else
          PXCache<APRegister>.StoreOriginal((PXGraph) this, doc);
      }
      else
        PXCache<APInvoice>.StoreOriginal((PXGraph) this, apInvoice1);
      this.APDocument.Cache.SetStatus((object) doc, PXEntryStatus.Updated);
      foreach (PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Vendor> pxResult in this.APInvoice_DocType_RefNbr.Select((object) doc.DocType, (object) doc.RefNbr))
      {
        if (!((PX.Objects.CM.Extensions.CurrencyInfo) pxResult).CuryInfoID.HasValue)
          throw new PXException("The document cannot be released. You can try to delete it and create a new one, or you can contact your Acumatica support provider.");
        Vendor vendor = (Vendor) pxResult;
        string vstatus = vendor.VStatus;
        if (vstatus == "I" || vstatus == "H")
          throw new PXSetPropertyException("The vendor status is '{0}'.", new object[1]
          {
            (object) new VendorStatus.ListAttribute().ValueLabelDic[vendor.VStatus]
          });
        if (!doc.Released.Value)
          this.SegregateBatch(je, doc.BranchID, doc.CuryID, doc.DocDate, doc.FinPeriodID, doc.DocDesc, (PX.Objects.CM.Extensions.CurrencyInfo) pxResult);
        apRegisterList = this.ReleaseInvoice(je, ref doc, pxResult, isPrebooking, out inDocs);
        this.APInvoice_DocType_RefNbr.Current = (APInvoice) pxResult;
      }
      ARReleaseProcess.Amount amount = new ARReleaseProcess.Amount();
      foreach (PXResult<APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Vendor, PX.Objects.CA.CashAccount> info in this.APPayment_DocType_RefNbr.Select((object) doc.DocType, (object) doc.RefNbr))
      {
        bool? nullable1;
        if (doc.DocType == "PPI")
        {
          nullable1 = doc.Released;
          bool flag = false;
          if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
            continue;
        }
        APPayment apPayment2 = (APPayment) info;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) info;
        PX.Objects.CM.Extensions.Currency currency = (PX.Objects.CM.Extensions.Currency) info;
        Vendor vendor = (Vendor) info;
        PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) info;
        this.GetExtension<APReleaseProcess.MultiCurrency>().StoreResult((PX.Objects.CM.Extensions.CurrencyInfo) info);
        Decimal? nullable2 = APDocType.SignBalance(doc.DocType);
        Decimal num1 = 0M;
        if (!(nullable2.GetValueOrDefault() == num1 & nullable2.HasValue) && !(doc is APPayment) && !(doc is APInvoice))
        {
          this.APDocument.Cache.Remove((object) doc);
          APPayment copy = PXCache<APPayment>.CreateCopy(apPayment2);
          PXCache<APRegister>.RestoreCopy((APRegister) copy, doc);
          this.APDocument.Cache.Remove((object) doc);
          doc = (APRegister) copy;
          this.APDocument.Cache.SetStatus((object) doc, PXEntryStatus.Updated);
        }
        this.APPayment_DocType_RefNbr.Current = apPayment2;
        if (cashAccount != null && cashAccount.CashAccountID.HasValue)
          this.VerifyCashAccountInGL(cashAccount);
        if (apPayment2 != null)
          this.VerifyPaymentData(apPayment2);
        Tuple<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo> lastAdjustment = new Tuple<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo>(new APAdjust(), new PX.Objects.CM.Extensions.CurrencyInfo());
        string vstatus = vendor.VStatus;
        if (vstatus == "I" || vstatus == "H" || vstatus == "P")
          throw new PXSetPropertyException("The vendor status is '{0}'.", new object[1]
          {
            (object) new VendorStatus.ListAttribute().ValueLabelDic[vendor.VStatus]
          });
        nullable1 = doc.Prebooked;
        if (nullable1.GetValueOrDefault())
        {
          nullable1 = doc.Released;
          if (!nullable1.GetValueOrDefault() && (doc.DocType == "QCK" || doc.DocType == "VQC" || doc.DocType == "ADR" || doc.DocType == "RQC"))
            continue;
        }
        nullable1 = doc.Released;
        PX.Objects.CM.CurrencyInfo currencyInfoCopyForGl;
        if (!nullable1.GetValueOrDefault() && (doc.DocType == "CHK" || doc.DocType == "VCK" || doc.DocType == "PPM"))
        {
          this.SegregateBatch(je, doc.BranchID, doc.CuryID, apPayment2.DocDate, apPayment2.FinPeriodID, doc.DocDesc, currencyInfo);
          currencyInfoCopyForGl = this.GetCurrencyInfoCopyForGL(je, currencyInfo);
          this.ProcessPayment(je, doc, new PXResult<APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Vendor, PX.Objects.CA.CashAccount>(apPayment2, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(currencyInfoCopyForGl), currency, vendor, cashAccount));
          if (je.GLTranModuleBatNbr.Cache.IsInsertedUpdatedDeleted)
            je.Save.Press();
          if (!je.BatchModule.Cache.IsDirty && string.IsNullOrEmpty(doc.BatchNbr))
          {
            doc.BatchNbr = je.BatchModule.Current.BatchNbr;
            foreach (APTranPost apTranPost in this.TranPost.Cache.Inserted.Cast<APTranPost>().Where<APTranPost>((Func<APTranPost, bool>) (d => d.TranType == doc.DocType && d.TranRefNbr == doc.RefNbr && d.BatchNbr == null)))
              apTranPost.BatchNbr = doc.BatchNbr;
          }
          APReleaseProcess.APDocTypePeriodSorting typePeriodSorting = new APReleaseProcess.APDocTypePeriodSorting();
          SortedDictionary<Tuple<string, string>, List<PXResult<APAdjust>>> sortedDictionary1 = new SortedDictionary<Tuple<string, string>, List<PXResult<APAdjust>>>((IComparer<Tuple<string, string>>) typePeriodSorting);
          SortedDictionary<Tuple<string, string>, System.DateTime?> sortedDictionary2 = new SortedDictionary<Tuple<string, string>, System.DateTime?>((IComparer<Tuple<string, string>>) typePeriodSorting);
          this.InsertCurrencyInfoIntoCache(doc, currencyInfo);
          foreach (PXResult<APAdjust> pxResult in this.APAdjust_AdjgDocType_RefNbr_VendorID.Select((object) doc.DocType, (object) doc.RefNbr, (object) this._IsIntegrityCheck, (object) doc.AdjCntr))
          {
            APAdjust adj = (APAdjust) pxResult;
            this.SetAdjgPeriodsFromLatestApplication(doc, adj);
            Tuple<string, string> key = new Tuple<string, string>(adj.AdjdDocType, adj.AdjgTranPeriodID);
            List<PXResult<APAdjust>> pxResultList;
            if (!sortedDictionary1.TryGetValue(key, out pxResultList))
              sortedDictionary1[key] = pxResultList = new List<PXResult<APAdjust>>();
            pxResultList.Add(pxResult);
            System.DateTime? adjgDocDate;
            if (!sortedDictionary2.TryGetValue(key, out adjgDocDate))
              sortedDictionary2[key] = adjgDocDate = adj.AdjgDocDate;
            if (System.DateTime.Compare(adj.AdjgDocDate.Value, adjgDocDate.Value) > 0)
              sortedDictionary2[key] = adj.AdjgDocDate;
            nullable1 = doc.OpenDoc;
            bool flag = false;
            if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue && doc.DocType == "VCK")
            {
              doc.OpenDoc = new bool?(true);
              doc.CuryDocBal = doc.CuryOrigDocAmt;
              doc.DocBal = doc.OrigDocAmt;
            }
          }
          PX.Objects.GL.Batch current = je.BatchModule.Current;
          foreach (KeyValuePair<Tuple<string, string>, List<PXResult<APAdjust>>> keyValuePair in sortedDictionary1)
          {
            Tuple<string, string> key = keyValuePair.Key;
            FinPeriod valueOrRaiseError = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(doc.BranchID), key.Item2).GetValueOrRaiseError();
            JournalEntry.SegregateBatch(je, "AP", doc.BranchID, doc.CuryID, sortedDictionary2[key], valueOrRaiseError.FinPeriodID, doc.DocDesc, currencyInfo.GetCM(), current);
            PXResultset<APAdjust> adjustments = new PXResultset<APAdjust>();
            adjustments.AddRange((IEnumerable<PXResult<APAdjust>>) keyValuePair.Value);
            currencyInfoCopyForGl = this.GetCurrencyInfoCopyForGL(je, currencyInfo);
            this.GetExtension<APReleaseProcess.MultiCurrency>().currencyinfo.Insert(currencyInfo);
            lastAdjustment = this.ProcessAdjustments(je, adjustments, doc, apPayment2, vendor, currencyInfoCopyForGl, currency);
          }
        }
        else
        {
          if (doc.DocType != "QCK" && doc.DocType != "VQC" && doc.DocType != "RQC")
            this.SegregateBatch(je, doc.BranchID, doc.CuryID, apPayment2.AdjDate, apPayment2.AdjFinPeriodID, doc.DocDesc, currencyInfo);
          currencyInfoCopyForGl = this.GetCurrencyInfoCopyForGL(je, currencyInfo);
          this.ProcessPayment(je, doc, new PXResult<APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Vendor, PX.Objects.CA.CashAccount>(apPayment2, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(currencyInfoCopyForGl), currency, vendor, cashAccount));
          PXResultset<APAdjust> adjustments = this.APAdjust_AdjgDocType_RefNbr_VendorID.Select((object) doc.DocType, (object) doc.RefNbr, (object) this._IsIntegrityCheck, (object) doc.AdjCntr);
          lastAdjustment = this.ProcessAdjustments(je, adjustments, doc, apPayment2, vendor, currencyInfoCopyForGl, currency);
        }
        ARReleaseProcess.Amount docBal = new ARReleaseProcess.Amount(doc.CuryDocBal, doc.DocBal);
        if (doc.DocType == "VCK")
        {
          nullable2 = docBal.Base;
          Decimal num2 = 0M;
          if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
          {
            nullable1 = doc.IsMigratedRecord;
            if (nullable1.GetValueOrDefault())
              docBal = new ARReleaseProcess.Amount(doc.CuryInitDocBal, doc.InitDocBal);
            this.ProcessVoidPaymentTranPost(doc, docBal);
          }
        }
        this.VerifyPaymentRoundAndClose(je, doc, apPayment2, vendor, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(currencyInfoCopyForGl), currency, lastAdjustment);
        APRegister apRegister = doc;
        int? adjCntr = apRegister.AdjCntr;
        apRegister.AdjCntr = adjCntr.HasValue ? new int?(adjCntr.GetValueOrDefault() + 1) : new int?();
        this.APPayment_DocType_RefNbr.Current = apPayment2;
      }
      if (!this._IsIntegrityCheck)
      {
        if (je.GLTranModuleBatNbr.Cache.IsInsertedUpdatedDeleted)
          je.Save.Press();
        if (!je.BatchModule.Cache.IsDirty && string.IsNullOrEmpty(doc.BatchNbr) && (this.APInvoice_DocType_RefNbr.Current == null || this.APInvoice_DocType_RefNbr.Current.DocType != "PPM"))
        {
          if (!isPrebooking)
          {
            string batchNbr = je.BatchModule.Current?.BatchNbr;
            doc.BatchNbr = !doc.IsMigratedRecord.GetValueOrDefault() ? batchNbr : (string) null;
            foreach (APTranPost apTranPost in this.TranPost.Cache.Inserted.Cast<APTranPost>().Where<APTranPost>((Func<APTranPost, bool>) (d => d.TranType == doc.DocType && d.TranRefNbr == doc.RefNbr && d.BatchNbr == null)))
              apTranPost.BatchNbr = batchNbr;
            if (doc.DocType == "VQC")
              doc.PrebookBatchNbr = (string) null;
          }
          else
            doc.PrebookBatchNbr = je.BatchModule.Current?.BatchNbr;
        }
      }
      bool? nullable3;
      if (doc is APInvoice doc1)
      {
        APInvoice apInvoice2 = this.CommitExternalTax(doc1);
        APRegister apRegister = doc;
        nullable3 = apInvoice2.IsTaxPosted;
        bool? nullable4 = new bool?(nullable3.GetValueOrDefault());
        apRegister.IsTaxPosted = nullable4;
      }
      nullable3 = doc.Released;
      bool flag1 = nullable3.Value;
      if (doc.DocType == "QCK" & isPrebooking)
        flag1 = false;
      if (((flag1 ? 0 : (APDocType.IsPrebookingAllowedForType(doc.DocType) ? 1 : 0)) & (isPrebooking ? 1 : 0)) != 0)
      {
        doc.Released = new bool?(false);
        doc.Prebooked = new bool?(true);
      }
      else
        doc.Released = new bool?(true);
      doc = (APRegister) this.APDocument.Cache.Update((object) doc);
      nullable3 = doc.Released;
      if (nullable3.GetValueOrDefault())
        this.RaiseReleaseEvent(doc);
      if (doc.DocType == "ADR")
      {
        if (flag1)
        {
          this.APPayment_DocType_RefNbr.Cache.SetStatus((object) this.APPayment_DocType_RefNbr.Current, PXEntryStatus.Notchanged);
        }
        else
        {
          APPayment data = (APPayment) this.APPayment_DocType_RefNbr.Cache.Extend<APRegister>(doc);
          data.AdjTranPeriodID = (string) null;
          data.AdjFinPeriodID = (string) null;
          data.CuryInfoID = doc.CuryInfoID;
          this.APPayment_DocType_RefNbr.Cache.Update((object) data);
          data.CreatedByID = doc.CreatedByID;
          data.CreatedByScreenID = doc.CreatedByScreenID;
          data.CreatedDateTime = doc.CreatedDateTime;
          data.CashAccountID = new int?();
          data.PaymentMethodID = (string) null;
          data.DepositAsBatch = new bool?(false);
          data.ExtRefNbr = (string) null;
          data.AdjDate = data.DocDate;
          data.AdjFinPeriodID = data.FinPeriodID;
          data.AdjTranPeriodID = data.TranPeriodID;
          data.Printed = new bool?(true);
          SharedRecordAttribute.DefaultRecord<APPayment.remitAddressID>(this.APPayment_DocType_RefNbr.Cache, (object) data);
          SharedRecordAttribute.DefaultRecord<APPayment.remitContactID>(this.APPayment_DocType_RefNbr.Cache, (object) data);
          OpenPeriodAttribute.SetValidatePeriod<APPayment.adjFinPeriodID>(this.APPayment_DocType_RefNbr.Cache, (object) data, PeriodValidation.DefaultSelectUpdate);
          this.APPayment_DocType_RefNbr.Cache.Update((object) data);
          this.APDocument.Cache.SetStatus((object) doc, PXEntryStatus.Notchanged);
        }
      }
      else if (this.APDocument.Cache.ObjectsEqual((object) doc, (object) this.APPayment_DocType_RefNbr.Current))
        this.APPayment_DocType_RefNbr.Cache.SetStatus((object) this.APPayment_DocType_RefNbr.Current, PXEntryStatus.Notchanged);
      this.Actions.PressSave();
      if (!flag1)
      {
        Guid? noteId1 = (Guid?) this.APPayment_DocType_RefNbr.Current?.NoteID;
        Guid? noteId2 = doc.NoteID;
        if ((noteId1.HasValue == noteId2.HasValue ? (noteId1.HasValue ? (noteId1.GetValueOrDefault() == noteId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && doc.DocType == "ADR")
          PXCache<APRegister>.RestoreCopy(doc, (APRegister) this.APPayment_DocType_RefNbr.Current);
      }
      if (!this._IsIntegrityCheck)
        EntityInUseHelper.MarkEntityAsInUse<CurrencyInUse>((object) doc.CuryID);
      transactionScope.Complete((PXGraph) this);
    }
    return apRegisterList;
  }

  protected virtual void VerifyPaymentData(APPayment payment)
  {
    APReleaseProcess.VerifyExtRefNbr(this.Caches[typeof (APPayment)], payment, (System.Action<PXCache>) (_ =>
    {
      throw new ReleaseException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<APPayment.extRefNbr>(_)
      });
    }));
  }

  public static void VerifyExtRefNbr(
    PXCache cache,
    APPayment payment,
    System.Action<PXCache> raiseFieldIsEmptyAction)
  {
    if (payment == null || !string.IsNullOrEmpty(payment.ExtRefNbr) || !PaymentRefAttribute.PaymentRefMustBeUnique(PX.Objects.CA.PaymentMethod.PK.Find(cache.Graph, payment.PaymentMethodID)))
      return;
    int num;
    if (payment.DocType == "PPM")
      num = new PXSelectReadonly<APInvoice, Where<APInvoice.docType, Equal<Required<APPayment.docType>>, And<APInvoice.refNbr, Equal<Required<APPayment.refNbr>>, And<APInvoice.docType, Equal<APDocType.prepayment>>>>>(cache.Graph).SelectSingle((object) payment.DocType, (object) payment.RefNbr) != null ? 1 : 0;
    else
      num = 0;
    bool flag1 = num != 0;
    if (!(payment.DocType == "CHK") && (!(payment.DocType == "PPM") || flag1) && !(payment.DocType == "REF") && !(payment.DocType == "VCK") && !(payment.DocType == "RQC"))
      return;
    bool flag2 = true;
    if (payment.DocType == "VCK" || payment.DocType == "REF")
    {
      APPayment apPayment = (APPayment) PXSelectBase<APPayment, PXSelectReadonly<APPayment, Where<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>.Config>.SelectSingleBound(cache.Graph, (object[]) null, (object) payment.OrigDocType, (object) payment.OrigRefNbr);
      if (apPayment != null && string.IsNullOrEmpty(apPayment.ExtRefNbr))
        flag2 = false;
    }
    if (!flag2 || payment.Released.GetValueOrDefault())
      return;
    raiseFieldIsEmptyAction(cache);
  }

  protected virtual void VerifyCashAccountInGL(PX.Objects.CA.CashAccount cashAccount)
  {
    PX.Objects.GL.Account account = AccountAttribute.GetAccount((PXGraph) this, cashAccount.AccountID);
    if (!account.IsCashAccount.GetValueOrDefault())
      throw new PXException("This cash account is mapped to the {0} GL account for which the Cash Account check box is cleared on the Chart of Accounts (GL202500) form.", new object[1]
      {
        (object) account.AccountCD
      });
  }

  public virtual APInvoice CommitExternalTax(APInvoice doc) => doc;

  /// <summary>
  /// Workaround for AC-167924. To prevent selection of outdated currencyinfo record from DB:
  /// 1. When we generate ap doc through the voucher from, we create new currencyinfo in the voucher graph.
  /// 2. We are persisting changes but they are not committed in the db
  /// 3. When we are in the ap release graph, we select the currencyinfo from db and get outdated commited one.
  /// 4. This workaround is that to put the currencyinfo to the cache to avoid quieting the db
  /// </summary>
  /// <param name="doc"></param>
  /// <param name="info"></param>
  protected virtual void InsertCurrencyInfoIntoCache(APRegister doc, PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    if (!(doc.OrigModule == "GL"))
      return;
    this.CurrencyInfo_CuryInfoID.Insert(info);
  }

  protected virtual void RaiseInvoiceEvent(APRegister doc, SelectedEntityEvent<APInvoice> invEvent)
  {
    if (!(doc is APInvoice))
      return;
    this.APDocument.Cache.Remove((object) doc);
    invEvent.FireOn((PXGraph) this, (APInvoice) doc);
    this.APDocument.Cache.Update((object) doc);
    this.APDocument.Cache.RestoreCopy((object) doc, this.APDocument.Cache.Locate((object) doc));
  }

  protected virtual void RaisePaymentEvent(APRegister doc, SelectedEntityEvent<APPayment> pntEvent)
  {
    if (!(doc is APPayment))
      return;
    this.APDocument.Cache.Remove((object) doc);
    pntEvent.FireOn((PXGraph) this, (APPayment) doc);
    this.APDocument.Cache.Update((object) doc);
    this.APDocument.Cache.RestoreCopy((object) doc, this.APDocument.Cache.Locate((object) doc));
  }

  protected virtual void RaiseReleaseEvent(APRegister doc)
  {
    if (this.APDocument.Cache.ObjectsEqual((object) doc, (object) this.APInvoice_DocType_RefNbr.Current))
    {
      APInvoice copy = PXCache<APInvoice>.CreateCopy(this.APInvoice_DocType_RefNbr.Current);
      this.APDocument.Cache.RestoreCopy((object) copy, (object) doc);
      this.APDocument.Cache.Remove((object) doc);
      PXCache<APInvoice>.StoreOriginal((PXGraph) this, copy);
      PXEntityEventBase<APInvoice>.Container<APInvoice.Events>.Select((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (e => e.ReleaseDocument)).FireOn((PXGraph) this, copy);
      if (this.APDocument.Cache.GetStatus((object) copy) != PXEntryStatus.Updated)
        this.APDocument.Cache.SetStatus((object) copy, PXEntryStatus.Updated);
      this.APDocument.Cache.RestoreCopy((object) doc, this.APDocument.Cache.Locate((object) doc));
    }
    else
    {
      if (!this.APDocument.Cache.ObjectsEqual((object) doc, (object) this.APPayment_DocType_RefNbr.Current))
        return;
      APPayment copy = PXCache<APPayment>.CreateCopy(this.APPayment_DocType_RefNbr.Current);
      this.APDocument.Cache.RestoreCopy((object) copy, (object) doc);
      this.APDocument.Cache.Remove((object) doc);
      PXCache<APPayment>.StoreOriginal((PXGraph) this, copy);
      PXEntityEventBase<APPayment>.Container<APPayment.Events>.Select((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (e => e.ReleaseDocument)).FireOn((PXGraph) this, copy);
      if (this.APDocument.Cache.GetStatus((object) copy) != PXEntryStatus.Updated)
        this.APDocument.Cache.SetStatus((object) copy, PXEntryStatus.Updated);
      this.APDocument.Cache.RestoreCopy((object) doc, this.APDocument.Cache.Locate((object) doc));
    }
  }

  protected virtual bool HasAnyUnreleasedAdjustment(APRegister doc)
  {
    return PXSelectBase<APAdjust, PXViewOf<APAdjust>.BasedOn<SelectFromBase<APAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjust.adjgDocType, Equal<P.AsString.ASCII>>>>, PX.Data.And<BqlOperand<APAdjust.adjgRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<APAdjust.released, IBqlBool>.IsNotEqual<True>>>>.ReadOnly.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr).Any<PXResult<APAdjust>>();
  }

  private void SetClosedPeriodsFromLatestApplication(APRegister doc)
  {
    APTranPost apTranPost1 = (APTranPost) PXSelectBase<APTranPost, PXSelect<APTranPost, Where<APTranPost.docType, Equal<Required<APTranPost.docType>>, And<APTranPost.refNbr, Equal<Required<APTranPost.refNbr>>>>, PX.Data.OrderBy<Desc<APTranPost.tranPeriodID, Desc<APTranPost.iD>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], (object) doc.DocType, (object) doc.RefNbr);
    APTranPost apTranPost2 = (APTranPost) PXSelectBase<APTranPost, PXSelect<APTranPost, Where<APTranPost.docType, Equal<Required<APTranPost.docType>>, And<APTranPost.refNbr, Equal<Required<APTranPost.refNbr>>>>, PX.Data.OrderBy<Desc<APTranPost.docDate, Desc<APTranPost.iD>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], (object) doc.DocType, (object) doc.RefNbr);
    doc.ClosedTranPeriodID = PX.Objects.GL.FinPeriods.FinPeriodUtils.Max(apTranPost1?.TranPeriodID, doc.TranPeriodID);
    FinPeriodIDAttribute.SetPeriodsByMaster<APRegister.closedFinPeriodID>(this.APDocument.Cache, (object) doc, doc.ClosedTranPeriodID);
    doc.ClosedDate = PX.Objects.GL.FinPeriods.FinPeriodUtils.Max((System.DateTime?) apTranPost2?.DocDate, doc.DocDate);
  }

  private void SetAdjgPeriodsFromLatestApplication(APRegister doc, APAdjust adj)
  {
    if (adj.VoidAppl.GetValueOrDefault())
    {
      foreach (string originalDocumentType in doc.PossibleOriginalDocumentTypes())
      {
        APAdjust apAdjust1 = (APAdjust) PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>, And<APAdjust.adjNbr, Equal<Required<APAdjust.voidAdjNbr>>, And<APAdjust.released, Equal<True>>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) adj.AdjdDocType, (object) adj.AdjdRefNbr, (object) originalDocumentType, (object) adj.AdjgRefNbr, (object) adj.VoidAdjNbr);
        if (apAdjust1 != null)
        {
          FinPeriodIDAttribute.SetPeriodsByMaster<APAdjust.adjgFinPeriodID>(this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache, (object) adj, PX.Objects.GL.FinPeriods.FinPeriodUtils.Max(apAdjust1.AdjgTranPeriodID, adj.AdjgTranPeriodID));
          APAdjust apAdjust2 = adj;
          System.DateTime? adjgDocDate = apAdjust1.AdjgDocDate;
          System.DateTime date1 = adjgDocDate.Value;
          adjgDocDate = adj.AdjgDocDate;
          System.DateTime date2 = adjgDocDate.Value;
          System.DateTime? nullable = new System.DateTime?(PX.Objects.GL.FinPeriods.FinPeriodUtils.Max(date1, date2));
          apAdjust2.AdjgDocDate = nullable;
          break;
        }
      }
    }
    FinPeriodIDAttribute.SetPeriodsByMaster<APAdjust.adjgFinPeriodID>(this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache, (object) adj, PX.Objects.GL.FinPeriods.FinPeriodUtils.Max(adj.AdjdTranPeriodID, adj.AdjgTranPeriodID));
    APAdjust apAdjust = adj;
    System.DateTime? nullable1 = adj.AdjdDocDate;
    System.DateTime date1_1 = nullable1.Value;
    nullable1 = adj.AdjgDocDate;
    System.DateTime date2_1 = nullable1.Value;
    System.DateTime? nullable2 = new System.DateTime?(PX.Objects.GL.FinPeriods.FinPeriodUtils.Max(date1_1, date2_1));
    apAdjust.AdjgDocDate = nullable2;
  }

  private void ClosePayment(APRegister doc, APPayment apdoc, Vendor vendor)
  {
    if (!apdoc.VoidAppl.GetValueOrDefault())
    {
      Decimal? curyDocBal = doc.CuryDocBal;
      Decimal num = 0M;
      if (!(curyDocBal.GetValueOrDefault() == num & curyDocBal.HasValue))
        return;
    }
    doc.CuryDocBal = new Decimal?(0M);
    doc.DocBal = new Decimal?(0M);
    doc.OpenDoc = new bool?(false);
    this.SetClosedPeriodsFromLatestApplication(doc);
    bool? voidAppl = apdoc.VoidAppl;
    if (voidAppl.GetValueOrDefault() || apdoc.DocType == "VQC")
      this.UpdateVoidedCheck(doc);
    voidAppl = apdoc.VoidAppl;
    if (voidAppl.GetValueOrDefault())
      return;
    this.RaisePaymentEvent(doc, PXEntityEventBase<APPayment>.Container<APPayment.Events>.Select((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (ev => ev.CloseDocument)));
    this.RaiseInvoiceEvent(doc, PXEntityEventBase<APInvoice>.Container<APInvoice.Events>.Select((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (ev => ev.CloseDocument)));
    this.DeactivateOneTimeVendorIfAllDocsIsClosed(vendor);
  }

  private void ReopenPayment(APRegister doc, APPayment apdoc)
  {
    bool? voidAppl = apdoc.VoidAppl;
    bool flag = false;
    if (!(voidAppl.GetValueOrDefault() == flag & voidAppl.HasValue))
      return;
    doc.OpenDoc = new bool?(true);
    doc.ClosedDate = new System.DateTime?();
    doc.ClosedFinPeriodID = (string) null;
    doc.ClosedTranPeriodID = (string) null;
    this.RaisePaymentEvent(doc, PXEntityEventBase<APPayment>.Container<APPayment.Events>.Select((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (ev => ev.OpenDocument)));
    this.RaiseInvoiceEvent(doc, PXEntityEventBase<APInvoice>.Container<APInvoice.Events>.Select((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (ev => ev.OpenDocument)));
  }

  protected override void PerformPersist(PXGraph.IPersistPerformer persister)
  {
    persister.Insert(this.APPayment_DocType_RefNbr.Cache);
    persister.Update(this.APPayment_DocType_RefNbr.Cache);
    persister.Update(this.APDocument.Cache);
    persister.Update(this.APTran_TranType_RefNbr.Cache);
    persister.Update(this.APPaymentChargeTran_DocType_RefNbr.Cache);
    persister.Insert(this.APTaxTran_TranType_RefNbr.Cache);
    persister.Update(this.APTaxTran_TranType_RefNbr.Cache);
    persister.Insert(this.SVATConversionHistory.Cache);
    persister.Update(this.SVATConversionHistory.Cache);
    persister.Insert(this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache);
    persister.Update(this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache);
    persister.Delete(this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache);
    persister.Insert<APHist>();
    persister.Insert<CuryAPHist>();
    persister.Insert<APTranPost>();
    persister.Insert(this.AP1099Year_Select.Cache);
    persister.Insert(this.AP1099History_Select.Cache);
    persister.Update(this.CurrencyInfo_CuryInfoID.Cache);
    persister.Insert<CADailySummary>();
    persister.Insert<PMCommitment>();
    persister.Update<PMCommitment>();
    persister.Delete<PMCommitment>();
    persister.Insert<PMHistoryAccum>();
    persister.Insert<PMBudgetAccum>();
    persister.Insert<PMForecastHistoryAccum>();
    persister.Update<APTax>();
  }

  /// <summary>
  /// Is turned on when releasing of invoice runs secondary after reclassifying action. Need to create only new gl batch in this case.
  /// </summary>
  public bool IsInvoiceReclassification { get; set; }

  public bool IsIntegrityCheck => this._IsIntegrityCheck;

  protected virtual int IntegrityCheckPaymentsSortOrder(APRegister docA, APRegister docB)
  {
    int num1 = ((IComparable) docA.SortOrder).CompareTo((object) docB.SortOrder);
    if (num1 == 0)
    {
      if (docA is APInvoice && docB is APPayment)
        return -1;
      if (docA is APPayment && docB is APInvoice)
        return 1;
    }
    if (docA.DocType == "PPM" && docB.DocType == "PPM")
    {
      int? lineCntr = docA.LineCntr;
      int num2 = 0;
      if (!(lineCntr.GetValueOrDefault() > num2 & lineCntr.HasValue))
      {
        lineCntr = docB.LineCntr;
        int num3 = 0;
        if (!(lineCntr.GetValueOrDefault() > num3 & lineCntr.HasValue))
          goto label_13;
      }
      lineCntr = docA.LineCntr;
      int num4 = 0;
      int num5;
      if (lineCntr.GetValueOrDefault() > num4 & lineCntr.HasValue)
      {
        lineCntr = docB.LineCntr;
        int num6 = 0;
        if (lineCntr.GetValueOrDefault() == num6 & lineCntr.HasValue)
        {
          num5 = -1;
          goto label_12;
        }
      }
      num5 = 1;
label_12:
      num1 = num5;
    }
label_13:
    if (num1 != 0)
      return num1;
    return !(docA.DocType == "ADR") ? ((IComparable) ((object) (IComparable) docA.IsRetainageDocument ?? (object) false)).CompareTo((object) docB.IsRetainageDocument.GetValueOrDefault()) : ((IComparable) ((object) (IComparable) docA.RetainageApply ?? (object) false)).CompareTo((object) docB.RetainageApply.GetValueOrDefault());
  }

  public virtual void IntegrityCheckProc(Vendor vend, string startPeriod)
  {
    this._IsIntegrityCheck = true;
    this._IntegrityCheckStartingPeriod = startPeriod;
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    instance.SetOffline();
    DocumentList<PX.Objects.GL.Batch> documentList = new DocumentList<PX.Objects.GL.Batch>((PXGraph) instance);
    this.Caches[typeof (Vendor)].Current = (object) vend;
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        string str = "190001";
        APHistory apHistory = (APHistory) PXSelectBase<APHistory, PXSelectGroupBy<APHistory, Where<APHistory.vendorID, Equal<Current<Vendor.bAccountID>>, And<APHistory.detDeleted, Equal<True>>>, PX.Data.Aggregate<Max<APHistory.finPeriodID>>>.Config>.Select((PXGraph) this);
        if (apHistory != null && apHistory.FinPeriodID != null)
          str = this.FinPeriodRepository.GetOffsetPeriodId(apHistory.FinPeriodID, 1, new int?(0));
        if (!string.IsNullOrEmpty(startPeriod) && string.Compare(startPeriod, str) > 0)
          str = startPeriod;
        FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(new int?(0), str);
        this.IntegrityCheckCleanValues(vend, startPeriod);
        PXDatabase.Delete<AP1099History>(new PXDataFieldRestrict("VendorID", PXDbType.Int, new int?(4), (object) vend.BAccountID, PXComp.EQ), new PXDataFieldRestrict("FinYear", PXDbType.Char, new int?(4), (object) str.Substring(0, 4), PXComp.GE));
        PXUpdateJoin<Set<APHistory.finBegBalance, IsNull<APHistory2.finYtdBalance, PX.Data.Zero>, Set<APHistory.finPtdPayments, PX.Data.Zero, Set<APHistory.finPtdPurchases, PX.Data.Zero, Set<APHistory.finPtdCrAdjustments, PX.Data.Zero, Set<APHistory.finPtdDrAdjustments, PX.Data.Zero, Set<APHistory.finPtdDiscTaken, PX.Data.Zero, Set<APHistory.finPtdWhTax, PX.Data.Zero, Set<APHistory.finPtdRGOL, PX.Data.Zero, Set<APHistory.finYtdBalance, IsNull<APHistory2.finYtdBalance, PX.Data.Zero>, Set<APHistory.finPtdDeposits, PX.Data.Zero, Set<APHistory.finYtdDeposits, IsNull<APHistory2.finYtdDeposits, PX.Data.Zero>, Set<APHistory.finYtdRetainageReleased, IsNull<APHistory2.finYtdRetainageReleased, PX.Data.Zero>, Set<APHistory.finPtdRetainageReleased, PX.Data.Zero, Set<APHistory.finYtdRetainageWithheld, IsNull<APHistory2.finYtdRetainageWithheld, PX.Data.Zero>, Set<APHistory.finPtdRetainageWithheld, PX.Data.Zero, Set<APHistory.finPtdRevalued, APHistory.finPtdRevalued>>>>>>>>>>>>>>>>, APHistory, LeftJoin<PX.Objects.GL.Branch, On<APHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FinPeriod, On<APHistory.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>, LeftJoin<OrganizationFinPeriodExt, On<OrganizationFinPeriodExt.masterFinPeriodID, Equal<Required<OrganizationFinPeriodExt.masterFinPeriodID>>, And<PX.Objects.GL.Branch.organizationID, Equal<OrganizationFinPeriodExt.organizationID>>>, LeftJoin<APHistory2ByPeriod, On<APHistory2ByPeriod.branchID, Equal<APHistory.branchID>, And<APHistory2ByPeriod.accountID, Equal<APHistory.accountID>, And<APHistory2ByPeriod.subID, Equal<APHistory.subID>, And<APHistory2ByPeriod.vendorID, Equal<APHistory.vendorID>, And<APHistory2ByPeriod.finPeriodID, Equal<OrganizationFinPeriodExt.prevFinPeriodID>>>>>>, LeftJoin<APHistory2, On<APHistory2.branchID, Equal<APHistory.branchID>, And<APHistory2.accountID, Equal<APHistory.accountID>, And<APHistory2.subID, Equal<APHistory.subID>, And<APHistory2.vendorID, Equal<APHistory.vendorID>, And<APHistory2.finPeriodID, Equal<APHistory2ByPeriod.lastActivityPeriod>>>>>>>>>>>, Where<APHistory.vendorID, Equal<Required<APHist.vendorID>>, And<FinPeriod.masterFinPeriodID, GreaterEqual<Required<FinPeriod.masterFinPeriodID>>>>>.Update((PXGraph) this, (object) str, (object) vend.BAccountID, (object) str);
        PXUpdateJoin<Set<APHistory.tranBegBalance, IsNull<APHistory2.tranYtdBalance, PX.Data.Zero>, Set<APHistory.tranPtdPayments, PX.Data.Zero, Set<APHistory.tranPtdPurchases, PX.Data.Zero, Set<APHistory.tranPtdCrAdjustments, PX.Data.Zero, Set<APHistory.tranPtdDrAdjustments, PX.Data.Zero, Set<APHistory.tranPtdDiscTaken, PX.Data.Zero, Set<APHistory.tranPtdWhTax, PX.Data.Zero, Set<APHistory.tranPtdRGOL, PX.Data.Zero, Set<APHistory.tranYtdBalance, IsNull<APHistory2.tranYtdBalance, PX.Data.Zero>, Set<APHistory.tranPtdDeposits, PX.Data.Zero, Set<APHistory.tranYtdDeposits, IsNull<APHistory2.tranYtdDeposits, PX.Data.Zero>, Set<APHistory.tranYtdRetainageReleased, IsNull<APHistory2.tranYtdRetainageReleased, PX.Data.Zero>, Set<APHistory.tranPtdRetainageReleased, PX.Data.Zero, Set<APHistory.tranYtdRetainageWithheld, IsNull<APHistory2.tranYtdRetainageWithheld, PX.Data.Zero>, Set<APHistory.tranPtdRetainageWithheld, PX.Data.Zero>>>>>>>>>>>>>>>, APHistory, LeftJoin<APHistory2ByPeriod, On<APHistory2ByPeriod.branchID, Equal<APHistory.branchID>, And<APHistory2ByPeriod.accountID, Equal<APHistory.accountID>, And<APHistory2ByPeriod.subID, Equal<APHistory.subID>, And<APHistory2ByPeriod.vendorID, Equal<APHistory.vendorID>, And<APHistory2ByPeriod.finPeriodID, Equal<Required<FinPeriod.masterFinPeriodID>>>>>>>, LeftJoin<APHistory2, On<APHistory2.branchID, Equal<APHistory.branchID>, And<APHistory2.accountID, Equal<APHistory.accountID>, And<APHistory2.subID, Equal<APHistory.subID>, And<APHistory2.vendorID, Equal<APHistory.vendorID>, And<APHistory2.finPeriodID, Equal<APHistory2ByPeriod.lastActivityPeriod>>>>>>>>, Where<APHistory.vendorID, Equal<Required<APHist.vendorID>>, And<APHistory.finPeriodID, GreaterEqual<Required<APHistory.finPeriodID>>>>>.Update((PXGraph) this, (object) prevPeriod?.FinPeriodID, (object) vend.BAccountID, (object) str);
        PXUpdateJoin<Set<CuryAPHistory.curyFinBegBalance, IsNull<CuryAPHistory2.curyFinYtdBalance, PX.Data.Zero>, Set<CuryAPHistory.curyFinPtdCrAdjustments, PX.Data.Zero, Set<CuryAPHistory.curyFinPtdDeposits, PX.Data.Zero, Set<CuryAPHistory.curyFinPtdDiscTaken, PX.Data.Zero, Set<CuryAPHistory.curyFinPtdDrAdjustments, PX.Data.Zero, Set<CuryAPHistory.curyFinPtdPayments, PX.Data.Zero, Set<CuryAPHistory.curyFinPtdPurchases, PX.Data.Zero, Set<CuryAPHistory.curyFinPtdRetainageReleased, PX.Data.Zero, Set<CuryAPHistory.curyFinPtdRetainageWithheld, PX.Data.Zero, Set<CuryAPHistory.curyFinPtdWhTax, PX.Data.Zero, Set<CuryAPHistory.curyFinYtdBalance, IsNull<CuryAPHistory2.curyFinYtdBalance, PX.Data.Zero>, Set<CuryAPHistory.curyFinYtdDeposits, IsNull<CuryAPHistory2.curyFinYtdDeposits, PX.Data.Zero>, Set<CuryAPHistory.curyFinYtdRetainageReleased, IsNull<CuryAPHistory2.curyFinYtdRetainageReleased, PX.Data.Zero>, Set<CuryAPHistory.curyFinYtdRetainageWithheld, IsNull<CuryAPHistory2.curyFinYtdRetainageWithheld, PX.Data.Zero>, Set<CuryAPHistory.finBegBalance, IsNull<CuryAPHistory2.finYtdBalance, PX.Data.Zero>, Set<CuryAPHistory.finPtdCrAdjustments, PX.Data.Zero, Set<CuryAPHistory.finPtdDeposits, PX.Data.Zero, Set<CuryAPHistory.finPtdDiscTaken, PX.Data.Zero, Set<CuryAPHistory.finPtdDrAdjustments, PX.Data.Zero, Set<CuryAPHistory.finPtdPayments, PX.Data.Zero, Set<CuryAPHistory.finPtdPurchases, PX.Data.Zero, Set<CuryAPHistory.finPtdRetainageReleased, PX.Data.Zero, Set<CuryAPHistory.finPtdRetainageWithheld, PX.Data.Zero, Set<CuryAPHistory.finPtdRGOL, PX.Data.Zero, Set<CuryAPHistory.finPtdWhTax, PX.Data.Zero, Set<CuryAPHistory.finYtdBalance, IsNull<CuryAPHistory2.finYtdBalance, PX.Data.Zero>, Set<CuryAPHistory.finYtdDeposits, IsNull<CuryAPHistory2.finYtdDeposits, PX.Data.Zero>, Set<CuryAPHistory.finYtdRetainageReleased, IsNull<CuryAPHistory2.finYtdRetainageReleased, PX.Data.Zero>, Set<CuryAPHistory.finYtdRetainageWithheld, IsNull<CuryAPHistory2.finYtdRetainageWithheld, PX.Data.Zero>, Set<CuryAPHistory.finPtdRevalued, CuryAPHistory.finPtdRevalued>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>, CuryAPHistory, LeftJoin<PX.Objects.GL.Branch, On<CuryAPHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FinPeriod, On<CuryAPHistory.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>, LeftJoin<OrganizationFinPeriodExt, On<OrganizationFinPeriodExt.masterFinPeriodID, Equal<Required<OrganizationFinPeriodExt.masterFinPeriodID>>, And<PX.Objects.GL.Branch.organizationID, Equal<OrganizationFinPeriodExt.organizationID>>>, LeftJoin<APHistoryByPeriod, On<APHistoryByPeriod.branchID, Equal<CuryAPHistory.branchID>, And<APHistoryByPeriod.accountID, Equal<CuryAPHistory.accountID>, And<APHistoryByPeriod.subID, Equal<CuryAPHistory.subID>, And<APHistoryByPeriod.vendorID, Equal<CuryAPHistory.vendorID>, And<APHistoryByPeriod.curyID, Equal<CuryAPHistory.curyID>, And<APHistoryByPeriod.finPeriodID, Equal<OrganizationFinPeriodExt.prevFinPeriodID>>>>>>>, LeftJoin<CuryAPHistory2, On<CuryAPHistory2.branchID, Equal<CuryAPHistory.branchID>, And<CuryAPHistory2.accountID, Equal<CuryAPHistory.accountID>, And<CuryAPHistory2.subID, Equal<CuryAPHistory.subID>, And<CuryAPHistory2.vendorID, Equal<CuryAPHistory.vendorID>, And<CuryAPHistory2.curyID, Equal<CuryAPHistory.curyID>, And<CuryAPHistory2.finPeriodID, Equal<APHistoryByPeriod.lastActivityPeriod>>>>>>>>>>>>, Where<CuryAPHistory.vendorID, Equal<Required<CuryAPHist.vendorID>>, And<FinPeriod.masterFinPeriodID, GreaterEqual<Required<FinPeriod.finPeriodID>>>>>.Update((PXGraph) this, (object) str, (object) vend.BAccountID, (object) str);
        PXUpdateJoin<Set<CuryAPHistory.curyTranBegBalance, IsNull<CuryAPHistory2.curyTranYtdBalance, PX.Data.Zero>, Set<CuryAPHistory.curyTranPtdCrAdjustments, PX.Data.Zero, Set<CuryAPHistory.curyTranPtdDeposits, PX.Data.Zero, Set<CuryAPHistory.curyTranPtdDiscTaken, PX.Data.Zero, Set<CuryAPHistory.curyTranPtdDrAdjustments, PX.Data.Zero, Set<CuryAPHistory.curyTranPtdPayments, PX.Data.Zero, Set<CuryAPHistory.curyTranPtdPurchases, PX.Data.Zero, Set<CuryAPHistory.curyTranPtdRetainageReleased, PX.Data.Zero, Set<CuryAPHistory.curyTranPtdRetainageWithheld, PX.Data.Zero, Set<CuryAPHistory.curyTranPtdWhTax, PX.Data.Zero, Set<CuryAPHistory.curyTranYtdBalance, IsNull<CuryAPHistory2.curyTranYtdBalance, PX.Data.Zero>, Set<CuryAPHistory.curyTranYtdDeposits, IsNull<CuryAPHistory2.curyTranYtdDeposits, PX.Data.Zero>, Set<CuryAPHistory.curyTranYtdRetainageReleased, IsNull<CuryAPHistory2.curyTranYtdRetainageReleased, PX.Data.Zero>, Set<CuryAPHistory.curyTranYtdRetainageWithheld, IsNull<CuryAPHistory2.curyTranYtdRetainageWithheld, PX.Data.Zero>, Set<CuryAPHistory.tranBegBalance, IsNull<CuryAPHistory2.tranYtdBalance, PX.Data.Zero>, Set<CuryAPHistory.tranPtdCrAdjustments, PX.Data.Zero, Set<CuryAPHistory.tranPtdDeposits, PX.Data.Zero, Set<CuryAPHistory.tranPtdDiscTaken, PX.Data.Zero, Set<CuryAPHistory.tranPtdDrAdjustments, PX.Data.Zero, Set<CuryAPHistory.tranPtdPayments, PX.Data.Zero, Set<CuryAPHistory.tranPtdPurchases, PX.Data.Zero, Set<CuryAPHistory.tranPtdRetainageReleased, PX.Data.Zero, Set<CuryAPHistory.tranPtdRetainageWithheld, PX.Data.Zero, Set<CuryAPHistory.tranPtdRGOL, PX.Data.Zero, Set<CuryAPHistory.tranPtdWhTax, PX.Data.Zero, Set<CuryAPHistory.tranYtdBalance, IsNull<CuryAPHistory2.tranYtdBalance, PX.Data.Zero>, Set<CuryAPHistory.tranYtdDeposits, IsNull<CuryAPHistory2.tranYtdDeposits, PX.Data.Zero>, Set<CuryAPHistory.tranYtdRetainageReleased, IsNull<CuryAPHistory2.tranYtdRetainageReleased, PX.Data.Zero>, Set<CuryAPHistory.tranYtdRetainageWithheld, IsNull<CuryAPHistory2.tranYtdRetainageWithheld, PX.Data.Zero>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>, CuryAPHistory, LeftJoin<APHistoryByPeriod, On<APHistoryByPeriod.branchID, Equal<CuryAPHistory.branchID>, And<APHistoryByPeriod.accountID, Equal<CuryAPHistory.accountID>, And<APHistoryByPeriod.subID, Equal<CuryAPHistory.subID>, And<APHistoryByPeriod.vendorID, Equal<CuryAPHistory.vendorID>, And<APHistoryByPeriod.curyID, Equal<CuryAPHistory.curyID>, And<APHistoryByPeriod.finPeriodID, Equal<Required<CuryAPHistory.finPeriodID>>>>>>>>, LeftJoin<CuryAPHistory2, On<CuryAPHistory2.branchID, Equal<CuryAPHistory.branchID>, And<CuryAPHistory2.accountID, Equal<CuryAPHistory.accountID>, And<CuryAPHistory2.subID, Equal<CuryAPHistory.subID>, And<CuryAPHistory2.vendorID, Equal<CuryAPHistory.vendorID>, And<CuryAPHistory2.curyID, Equal<CuryAPHistory.curyID>, And<CuryAPHistory2.finPeriodID, Equal<APHistoryByPeriod.lastActivityPeriod>>>>>>>>>, Where<CuryAPHistory.vendorID, Equal<Required<CuryAPHist.vendorID>>, And<CuryAPHistory.finPeriodID, GreaterEqual<Required<CuryAPHistory.finPeriodID>>>>>.Update((PXGraph) this, (object) prevPeriod?.FinPeriodID, (object) vend.BAccountID, (object) str);
        PXDatabase.Delete<APTranPost>((PXDataFieldRestrict) new PXDataFieldRestrict<APTranPost.vendorID>(PXDbType.Int, new int?(4), (object) vend.BAccountID, PXComp.EQ), (PXDataFieldRestrict) new PXDataFieldRestrict<APTranPost.tranPeriodID>(PXDbType.VarChar, new int?(str.Length), (object) str, PXComp.GE));
        HashedList<APRegister> integrityCheckProc = this.GetDocumentsForIntegrityCheckProc(str);
        this.APDocument.Cache.Clear();
        List<APRegister> apRegisterList1 = new List<APRegister>();
        List<APRegister> apRegisterList2 = new List<APRegister>();
        foreach (APRegister apRegister in integrityCheckProc)
        {
          if (apRegister.Payable.GetValueOrDefault() || APDocType.HasBothInvoiceAndPaymentParts(apRegister.DocType))
            apRegisterList1.Add(apRegister);
          if (apRegister.Paying.GetValueOrDefault() || APDocType.HasBothInvoiceAndPaymentParts(apRegister.DocType))
            apRegisterList2.Add(apRegister);
        }
        apRegisterList1.Sort((Comparison<APRegister>) ((docA, docB) =>
        {
          Func<APRegister, short?> func = (Func<APRegister, short?>) (doc => !doc.RetainageApply.GetValueOrDefault() && !doc.IsRetainageDocument.GetValueOrDefault() ? doc.SortOrder : new short?(!doc.IsRetainageReversing.GetValueOrDefault() ? (short) !doc.RetainageApply.GetValueOrDefault() : (doc.IsRetainageDocument.GetValueOrDefault() ? (short) 2 : (short) 3)));
          return ((IComparable) func(docA)).CompareTo((object) func(docB));
        }));
        foreach (APRegister apRegister1 in apRegisterList1)
        {
          if (!(this.APDocument.Cache.Locate((object) apRegister1) is APRegister apRegister2))
            apRegister2 = apRegister1;
          APRegister doc = apRegister2;
          instance.Clear();
          this.APDocument.Cache.SetStatus((object) doc, PXEntryStatus.Updated);
          bool? nullable = doc.Prebooked;
          bool valueOrDefault1 = nullable.GetValueOrDefault();
          nullable = doc.Released;
          bool valueOrDefault2 = nullable.GetValueOrDefault();
          if (valueOrDefault1)
            doc.Prebooked = new bool?(false);
          foreach (PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Vendor> pxResult in this.APInvoice_DocType_RefNbr.Select((object) doc.DocType, (object) doc.RefNbr))
          {
            doc.Released = new bool?(false);
            nullable = doc.PaymentsByLinesAllowed;
            if (!nullable.GetValueOrDefault() && !this.IsPayByLineRetainageDebitAdj(doc))
              this.APTran_TranType_RefNbr.StoreResult(new List<object>(), PXQueryParameters.ExplicitParameters((object) doc.DocType, (object) doc.RefNbr));
            nullable = doc.Released;
            bool flag1 = false;
            if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
            {
              nullable = doc.Prebooked;
              bool flag2 = false;
              if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
                goto label_29;
            }
            this.SegregateBatch(instance, doc.BranchID, doc.CuryID, doc.DocDate, doc.FinPeriodID, doc.DocDesc, (PX.Objects.CM.Extensions.CurrencyInfo) pxResult);
label_29:
            this.ReleaseInvoice(instance, ref doc, pxResult, valueOrDefault1, out List<PX.Objects.IN.INRegister> _);
            doc.Released = new bool?(valueOrDefault2);
            doc.Prebooked = new bool?(valueOrDefault1);
          }
          this.APDocument.Cache.Update((object) doc);
        }
        apRegisterList2.Sort(new Comparison<APRegister>(this.IntegrityCheckPaymentsSortOrder));
        foreach (APRegister apRegister3 in apRegisterList2)
        {
          APRegister apRegister4 = apRegister3;
          if (apRegister4.DocType == "PPI")
          {
            APRegister copy = (APRegister) PXSelectBase<APRegister, PXViewOf<APRegister>.BasedOn<SelectFromBase<APRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.docType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<APRegister.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, (object) apRegister3.DocType, (object) apRegister3.RefNbr);
            apRegister4 = (APRegister) (APPayment) PXSelectBase<APPayment, PXViewOf<APPayment>.BasedOn<SelectFromBase<APPayment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPayment.docType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<APPayment.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, (object) apRegister3.DocType, (object) apRegister3.RefNbr);
            if (copy != null)
              PXCache<APRegister>.RestoreCopy(apRegister4, copy);
          }
          instance.Clear();
          this.APDocument.Cache.SetStatus((object) apRegister4, PXEntryStatus.Updated);
          bool? nullable1 = apRegister4.Prebooked;
          bool valueOrDefault3 = nullable1.GetValueOrDefault();
          nullable1 = apRegister4.Released;
          bool valueOrDefault4 = nullable1.GetValueOrDefault();
          apRegister4.Released = new bool?(apRegister3 is APInvoice);
          foreach (PXResult<APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Vendor, PX.Objects.CA.CashAccount> pxResult1 in this.APPayment_DocType_RefNbr.Select((object) apRegister4.DocType, (object) apRegister4.RefNbr, (object) apRegister4.VendorID))
          {
            APPayment apPayment = (APPayment) pxResult1;
            PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) pxResult1;
            PX.Objects.CM.Extensions.Currency currency = (PX.Objects.CM.Extensions.Currency) pxResult1;
            Vendor vendor = (Vendor) pxResult1;
            PX.Objects.CA.CashAccount i4 = (PX.Objects.CA.CashAccount) pxResult1;
            nullable1 = apRegister4.Released;
            bool flag3 = false;
            if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue && apRegister4.DocType == "ADR" || apRegister4.DocType == "ACR")
            {
              nullable1 = apRegister4.Prebooked;
              if (nullable1.GetValueOrDefault())
                continue;
            }
            this.SegregateBatch(instance, apRegister4.BranchID, apRegister4.CuryID, apPayment.AdjDate, apPayment.AdjFinPeriodID, apRegister4.DocDesc, currencyInfo);
            int num1 = apRegister4.AdjCntr.Value;
            ARReleaseProcess.Amount docBal = new ARReleaseProcess.Amount();
            apRegister4.AdjCntr = new int?(0);
            while (true)
            {
              int? nullable2 = apRegister4.AdjCntr;
              int num2 = num1;
              if (nullable2.GetValueOrDefault() < num2 & nullable2.HasValue)
              {
                PX.Objects.CM.CurrencyInfo currencyInfoCopyForGl = this.GetCurrencyInfoCopyForGL(instance, currencyInfo);
                nullable2 = apRegister4.AdjCntr;
                int num3 = 0;
                if (nullable2.GetValueOrDefault() == num3 & nullable2.HasValue || apRegister4.DocType != "QCK" && apRegister4.DocType != "VQC" && apRegister4.DocType != "RQC")
                  this.ProcessPayment(instance, apRegister4, new PXResult<APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Vendor, PX.Objects.CA.CashAccount>(apPayment, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(currencyInfoCopyForGl), currency, vendor, i4));
                PXResultset<APAdjust> adjustments = this.APAdjust_AdjgDocType_RefNbr_VendorID.Select((object) apRegister4.DocType, (object) apRegister4.RefNbr, (object) this._IsIntegrityCheck, (object) apRegister4.AdjCntr);
                foreach (PXResult<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, APInvoice, APPayment, APRegisterAlias> pxResult2 in adjustments)
                {
                  APRegisterAlias apRegisterAlias = (APRegisterAlias) pxResult2;
                  APInvoice apInvoice = (APInvoice) pxResult2;
                  APAdjust apAdjust = (APAdjust) pxResult2;
                  int num4;
                  if (!(apAdjust.AdjgDocType == "VCK"))
                  {
                    nullable2 = apAdjust.VoidAdjNbr;
                    num4 = !nullable2.HasValue ? 0 : (apAdjust.AdjgDocType != "VRF" ? 1 : 0);
                  }
                  else
                    num4 = 1;
                  bool flag4 = num4 != 0;
                  if (apRegisterAlias.DocType == "PPM")
                  {
                    nullable2 = apRegisterAlias.AdjCntr;
                    int num5 = 0;
                    if (nullable2.GetValueOrDefault() == num5 & nullable2.HasValue && !flag4 && EnumerableExtensions.IsNotIn<string>(apAdjust.AdjgDocType, "REF", "VRF"))
                    {
                      apRegisterAlias.CuryDocBal = new Decimal?(0M);
                      apRegisterAlias.DocBal = new Decimal?(0M);
                      this.Caches[typeof (APRegisterAlias)].Update((object) apRegisterAlias);
                      APRegister apRegister5 = (APRegister) this.APDocument.Cache.Locate((object) apInvoice);
                      if (apRegister5 != null && apRegister5.DocType == "PPM")
                      {
                        nullable2 = apRegister5.AdjCntr;
                        int num6 = 0;
                        if (nullable2.GetValueOrDefault() == num6 & nullable2.HasValue)
                        {
                          apRegister5.CuryDocBal = new Decimal?(0M);
                          apRegister5.DocBal = new Decimal?(0M);
                          this.Caches[typeof (APRegister)].Update((object) apRegister5);
                        }
                      }
                    }
                  }
                }
                Tuple<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo> lastAdjustment = this.ProcessAdjustments(instance, adjustments, apRegister4, apPayment, vendor, currencyInfoCopyForGl, currency);
                nullable1 = apRegister4.IsMigratedRecord;
                docBal = nullable1.GetValueOrDefault() ? new ARReleaseProcess.Amount(apRegister4.CuryInitDocBal, apRegister4.InitDocBal) : new ARReleaseProcess.Amount(apRegister4.CuryDocBal, apRegister4.DocBal);
                APRegister apRegister6 = apRegister4;
                int? adjCntr = apRegister6.AdjCntr;
                apRegister6.AdjCntr = adjCntr.HasValue ? new int?(adjCntr.GetValueOrDefault() + 1) : new int?();
                this.VerifyPaymentRoundAndClose(instance, apRegister4, apPayment, vendor, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(currencyInfoCopyForGl), currency, lastAdjustment);
              }
              else
                break;
            }
            apRegister4.Prebooked = new bool?(valueOrDefault3);
            apRegister4.Released = new bool?(valueOrDefault4);
            Decimal? nullable3 = docBal.Base;
            Decimal num7 = 0M;
            if (!(nullable3.GetValueOrDefault() == num7 & nullable3.HasValue) && apRegister4.DocType == "VCK")
              this.ProcessVoidPaymentTranPost(apRegister4, docBal);
            APAdjust apAdjust1 = (APAdjust) this.APAdjust_AdjgDocType_RefNbr_VendorID.Select((object) apRegister4.DocType, (object) apRegister4.RefNbr, (object) this._IsIntegrityCheck, (object) num1);
            if (apAdjust1 != null)
            {
              nullable1 = apAdjust1.IsInitialApplication;
              if (!nullable1.GetValueOrDefault())
                this.ReopenPayment(apRegister4, apPayment);
            }
          }
          if (apRegister4.DocType == "PPM")
          {
            apRegister4.Released = new bool?(valueOrDefault4);
            if (apRegister4.Status == "V")
            {
              if (apRegister4.OpenDoc.GetValueOrDefault())
                apRegister4.OpenDoc = new bool?(false);
              int num;
              if (!(apRegister4 is APInvoice apInvoice))
              {
                num = 0;
              }
              else
              {
                nullable1 = apInvoice.Released;
                num = nullable1.GetValueOrDefault() ? 1 : 0;
              }
              if (num != 0)
              {
                if (!this.TranPost.Select((object) apRegister4.DocType, (object) apRegister4.RefNbr).Any<PXResult<APTranPost>>())
                {
                  apRegister4.CuryDocBal = new Decimal?(0M);
                  apRegister4.DocBal = new Decimal?(0M);
                }
              }
            }
          }
          this.APDocument.Cache.Update((object) apRegister4);
        }
        this.Caches[typeof (AP1099Hist)].Clear();
        foreach (PXResult<APAdjust, APTran, APInvoice, APPayment> pxResult in PXSelectBase<APAdjust, PXSelectReadonly2<APAdjust, InnerJoin<APTran, On<APTran.tranType, Equal<APAdjust.adjdDocType>, And<APTran.refNbr, Equal<APAdjust.adjdRefNbr>>>, InnerJoin<APInvoice, On<APInvoice.docType, Equal<APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>, InnerJoin<APPayment, On<APPayment.docType, Equal<APAdjust.adjgDocType>, And<APPayment.refNbr, Equal<APAdjust.adjgRefNbr>>>>>>, Where<APAdjust.vendorID, Equal<Required<APAdjust.vendorID>>, And<APAdjust.adjgDocDate, GreaterEqual<Required<APAdjust.adjgDocDate>>, And<APAdjust.released, Equal<True>, And<APAdjust.voided, Equal<False>, And<APTran.box1099, PX.Data.IsNotNull>>>>>>.Config>.Select((PXGraph) this, (object) vend.BAccountID, (object) new System.DateTime(Convert.ToInt32(str.Substring(0, 4)), 1, 1)))
          APReleaseProcess.Update1099Hist((PXGraph) this, 1M, (APAdjust) pxResult, (APTran) pxResult, (APRegister) (APInvoice) pxResult, (APRegister) (APPayment) pxResult);
        this.IntegrityCheckRecalculateValues(vend, startPeriod);
        foreach (APRegister row in this.APDocument.Cache.Updated)
          this.APDocument.Cache.PersistUpdated((object) row);
        this.APTran_TranType_RefNbr.Cache.Persist(PXDBOperation.Update);
        this.TranPost.Cache.Persist(PXDBOperation.Insert);
        this.Caches[typeof (APHist)].Persist(PXDBOperation.Insert);
        this.Caches[typeof (CuryAPHist)].Persist(PXDBOperation.Insert);
        this.Caches[typeof (AP1099Hist)].Persist(PXDBOperation.Insert);
        this.Caches[typeof (APAdjust)].Persist(PXDBOperation.Update);
        this.IntegrityCheckTransactionEnd(vend, startPeriod);
        transactionScope.Complete((PXGraph) this);
      }
      this.APDocument.Cache.Persisted(false);
      this.Caches[typeof (APHist)].Persisted(false);
      this.Caches[typeof (CuryAPHist)].Persisted(false);
      this.Caches[typeof (AP1099Hist)].Persisted(false);
      this.TranPost.Cache.Persisted(false);
      this.APTran_TranType_RefNbr.Cache.Persisted(false);
      this.Caches[typeof (APAdjust)].Persisted(false);
    }
    this.IntegrityCheckEnd(vend, startPeriod);
  }

  public virtual void IntegrityCheckCleanValues(Vendor vend, string startPeriod)
  {
  }

  public virtual void IntegrityCheckRecalculateValues(Vendor vend, string startPeriod)
  {
  }

  public virtual void IntegrityCheckTransactionEnd(Vendor vend, string startPeriod)
  {
  }

  public virtual void IntegrityCheckEnd(Vendor vend, string startPeriod)
  {
  }

  protected virtual HashedList<APRegister> GetDocumentsForIntegrityCheckProc(string minPeriod)
  {
    HashedList<APRegister> documents = new HashedList<APRegister>((IEqualityComparer<APRegister>) this.APDocument.Cache.GetComparer());
    foreach (PXResult<APRegister, APInvoice, APPayment> rec in PXSelectBase<APRegister, PXSelectJoin<APRegister, LeftJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APRegister.docType>, And<APInvoice.refNbr, Equal<APRegister.refNbr>>>, LeftJoinSingleTable<APPayment, On<APPayment.docType, Equal<APRegister.docType>, And<APPayment.refNbr, Equal<APRegister.refNbr>>>>>, Where<APRegister.vendorID, Equal<Current<Vendor.bAccountID>>, And2<Where<APRegister.released, Equal<True>, Or<APRegister.prebooked, Equal<True>>>, PX.Data.And<Where<APRegister.tranPeriodID, GreaterEqual<Required<APRegister.tranPeriodID>>, Or<APRegister.closedTranPeriodID, GreaterEqual<Required<APRegister.closedTranPeriodID>>>>>>>>.Config>.Select((PXGraph) this, (object) minPeriod, (object) minPeriod))
    {
      APRegister fullDocument = this.GetFullDocument(rec);
      if (fullDocument != null)
        documents.Add(fullDocument);
    }
    PXResultset<APRegister> integrityCheckProc = this.GetDirectAdjustmentsForIntegrityCheckProc(minPeriod);
    documents.AddRange((IEnumerable) integrityCheckProc.RowCast<APRegister>());
    this.GetAllReleasedAdjustments(documents, integrityCheckProc, minPeriod);
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>())
    {
      this.GetReleasedOriginalDocumentsWithAdjustments(documents, minPeriod);
      this.GetReleasedRetainageDocumentsWithAdjustments(documents, minPeriod);
    }
    return documents;
  }

  private APRegister GetFullDocument(PXResult<APRegister, APInvoice, APPayment> rec)
  {
    APInvoice apInvoice = (APInvoice) rec;
    APPayment apPayment = (APPayment) rec;
    APRegister fullDocument = (APRegister) null;
    if (apInvoice != null && apInvoice.RefNbr != null)
    {
      PXCache<APRegister>.RestoreCopy((APRegister) apInvoice, (APRegister) rec);
      fullDocument = (APRegister) apInvoice;
    }
    else if (apPayment != null && apPayment.RefNbr != null)
    {
      PXCache<APRegister>.RestoreCopy((APRegister) apPayment, (APRegister) rec);
      fullDocument = (APRegister) apPayment;
    }
    return fullDocument;
  }

  protected virtual void GetReleasedOriginalDocumentsWithAdjustments(
    HashedList<APRegister> documents,
    string minPeriod)
  {
    foreach ((string, string) hash in documents.RowCast<APRegister>().Where<APRegister>((Func<APRegister, bool>) (adj => adj.IsRetainageDocument.GetValueOrDefault())).Select<APRegister, (string, string)>((Func<APRegister, (string, string)>) (_ => (_.OrigDocType, _.OrigRefNbr))).ToHashSet<(string, string)>())
    {
      PXResultset<APRegister> startFrom = new FbqlSelect<SelectFromBase<APRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<APInvoice>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.refNbr, Equal<APRegister.refNbr>>>>>.And<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<APRegister.docType>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.docType, Equal<P.AsString>>>>, PX.Data.And<BqlOperand<APRegister.refNbr, IBqlString>.IsEqual<P.AsString>>>, PX.Data.And<BqlOperand<APRegister.retainageApply, IBqlBool>.IsEqual<True>>>, PX.Data.And<BqlOperand<APRegister.released, IBqlBool>.IsEqual<True>>>, PX.Data.And<BqlOperand<APRegister.tranPeriodID, IBqlString>.IsLess<P.AsString>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.closedTranPeriodID, PX.Data.IsNull>>>>.Or<BqlOperand<APRegister.closedTranPeriodID, IBqlString>.IsLess<P.AsString>>>>, APRegister>.View((PXGraph) this).Select((object) hash.Item1, (object) hash.Item2, (object) minPeriod, (object) minPeriod);
      foreach (PXResult<APRegister> row in startFrom)
      {
        APInvoice i0 = PXResult.Unwrap<APInvoice>((object) row);
        PXCache<APRegister>.RestoreCopy((APRegister) i0, PXResult.Unwrap<APRegister>((object) row));
        if (!documents.Contains((APRegister) i0))
          documents.Add((APRegister) new PXResult<APRegister>((APRegister) i0));
      }
      this.GetAllReleasedAdjustments(documents, startFrom, minPeriod);
    }
  }

  protected virtual void GetReleasedRetainageDocumentsWithAdjustments(
    HashedList<APRegister> documents,
    string minPeriod)
  {
    foreach ((string, string) hash in documents.RowCast<APRegister>().Where<APRegister>((Func<APRegister, bool>) (adj => adj.RetainageApply.GetValueOrDefault())).Select<APRegister, (string, string)>((Func<APRegister, (string, string)>) (_ => (_.DocType, _.RefNbr))).ToHashSet<(string, string)>())
    {
      PXResultset<APRegister> startFrom = new FbqlSelect<SelectFromBase<APRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<APInvoice>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.refNbr, Equal<APRegister.refNbr>>>>>.And<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<APRegister.docType>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.origDocType, Equal<P.AsString>>>>, PX.Data.And<BqlOperand<APRegister.origRefNbr, IBqlString>.IsEqual<P.AsString>>>, PX.Data.And<BqlOperand<APRegister.isRetainageDocument, IBqlBool>.IsEqual<True>>>, PX.Data.And<BqlOperand<APRegister.released, IBqlBool>.IsEqual<True>>>, PX.Data.And<BqlOperand<APRegister.tranPeriodID, IBqlString>.IsLess<P.AsString>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.closedTranPeriodID, PX.Data.IsNull>>>>.Or<BqlOperand<APRegister.closedTranPeriodID, IBqlString>.IsLess<P.AsString>>>>, APRegister>.View((PXGraph) this).Select((object) hash.Item1, (object) hash.Item2, (object) minPeriod, (object) minPeriod);
      foreach (PXResult<APRegister> row in startFrom)
      {
        APInvoice i0 = PXResult.Unwrap<APInvoice>((object) row);
        PXCache<APRegister>.RestoreCopy((APRegister) i0, PXResult.Unwrap<APRegister>((object) row));
        if (!documents.Contains((APRegister) i0))
          documents.Add((APRegister) new PXResult<APRegister>((APRegister) i0));
      }
      this.GetAllReleasedAdjustments(documents, startFrom, minPeriod);
    }
  }

  private IEnumerable<PXResult<APRegister>> GetFullDocuments(IEnumerable<PXResult<APRegister>> list)
  {
    foreach (PXResult<APRegister, APInvoice, APPayment> rec in list)
    {
      APRegister fullDocument = this.GetFullDocument(rec);
      if (fullDocument != null)
        yield return new PXResult<APRegister>(fullDocument);
    }
  }

  protected virtual PXResultset<APRegister> GetDirectAdjustmentsForIntegrityCheckProc(
    string minPeriod)
  {
    PXResult<APRegister>[] array1 = PXSelectBase<APRegister, PXSelectJoin<APRegister, LeftJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APRegister.docType>, And<APInvoice.refNbr, Equal<APRegister.refNbr>>>, LeftJoinSingleTable<APPayment, On<APPayment.docType, Equal<APRegister.docType>, And<APPayment.refNbr, Equal<APRegister.refNbr>>>>>, Where<APRegister.vendorID, Equal<Current<Vendor.bAccountID>>, And<APRegister.tranPeriodID, Less<Required<APRegister.tranPeriodID>>, And2<Where<APRegister.released, Equal<True>, Or<APRegister.prebooked, Equal<True>>>, And2<Where<APRegister.closedTranPeriodID, Less<Required<APRegister.closedTranPeriodID>>, Or<APRegister.closedTranPeriodID, PX.Data.IsNull>>, PX.Data.And<Exists<Select2<APAdjust, InnerJoin<APRegister2, On<APRegister2.docType, Equal<APAdjust.adjdDocType>, And<APRegister2.refNbr, Equal<APAdjust.adjdRefNbr>>>>, Where<APAdjust.adjgRefNbr, Equal<APRegister.refNbr>, And2<Where<APAdjust.adjgDocType, Equal<APRegister.docType>, PX.Data.Or<Where<APAdjust.adjgDocType, Equal<APDocType.prepayment>, And<APAdjust.voided, Equal<True>, And<APRegister.docType, Equal<APDocType.voidCheck>>>>>>, And2<Where<APRegister2.closedTranPeriodID, GreaterEqual<Required<APRegister2.closedTranPeriodID>>, Or<APAdjust.adjdTranPeriodID, GreaterEqual<Required<APAdjust.adjdTranPeriodID>>>>, And<APAdjust.released, Equal<True>>>>>>>>>>>>>.Config>.Select((PXGraph) this, (object) minPeriod, (object) minPeriod, (object) minPeriod, (object) minPeriod).ToArray<PXResult<APRegister>>();
    PXResult<APRegister>[] array2 = PXSelectBase<APRegister, PXSelectJoin<APRegister, LeftJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APRegister.docType>, And<APInvoice.refNbr, Equal<APRegister.refNbr>>>, LeftJoinSingleTable<APPayment, On<APPayment.docType, Equal<APRegister.docType>, And<APPayment.refNbr, Equal<APRegister.refNbr>>>>>, Where<APRegister.vendorID, Equal<Current<Vendor.bAccountID>>, And<APRegister.tranPeriodID, Less<Required<APRegister.tranPeriodID>>, And2<Where<APRegister.released, Equal<True>, Or<APRegister.prebooked, Equal<True>>>, And2<Where<APRegister.closedTranPeriodID, Less<Required<APRegister.closedTranPeriodID>>, Or<APRegister.closedTranPeriodID, PX.Data.IsNull>>, PX.Data.And<Exists<Select2<APAdjust, InnerJoin<APRegister2, On<APRegister2.docType, Equal<APAdjust.adjgDocType>, And<APRegister2.refNbr, Equal<APAdjust.adjgRefNbr>>>>, Where<APAdjust.adjdDocType, Equal<APRegister.docType>, And<APAdjust.adjdRefNbr, Equal<APRegister.refNbr>, And<APAdjust.released, Equal<True>, PX.Data.And<Where<APRegister2.closedTranPeriodID, GreaterEqual<Required<APRegister2.closedTranPeriodID>>, Or<APAdjust.adjdTranPeriodID, GreaterEqual<Required<APAdjust.adjdTranPeriodID>>>>>>>>>>>>>>>>.Config>.Select((PXGraph) this, (object) minPeriod, (object) minPeriod, (object) minPeriod, (object) minPeriod).ToArray<PXResult<APRegister>>();
    PXResultset<APRegister> integrityCheckProc = new PXResultset<APRegister>();
    integrityCheckProc.AddRange(this.GetFullDocuments((IEnumerable<PXResult<APRegister>>) array1));
    integrityCheckProc.AddRange(this.GetFullDocuments((IEnumerable<PXResult<APRegister>>) array2));
    return integrityCheckProc;
  }

  protected virtual void GetAllReleasedAdjustments(
    HashedList<APRegister> documents,
    PXResultset<APRegister> startFrom,
    string minPeriod)
  {
    List<\u003C\u003Ef__AnonymousType0<string, string>> list1 = startFrom.RowCast<APRegister>().Select(_ => new
    {
      DocType = _.DocType,
      RefNbr = _.RefNbr
    }).ToList();
    while (list1.Any())
    {
      List<\u003C\u003Ef__AnonymousType0<string, string>> list2 = list1.ToList();
      list1.Clear();
      foreach (var data in list2)
      {
        foreach (PXResult<APRegister> fullDocument in this.GetFullDocuments((IEnumerable<PXResult<APRegister>>) PXSelectBase<APRegister, PXSelectJoin<APRegister, LeftJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APRegister.docType>, And<APInvoice.refNbr, Equal<APRegister.refNbr>>>, LeftJoinSingleTable<APPayment, On<APPayment.docType, Equal<APRegister.docType>, And<APPayment.refNbr, Equal<APRegister.refNbr>>>>>, Where<APRegister.vendorID, Equal<Current<Vendor.bAccountID>>, And2<Exists<PX.Data.Select<APAdjust, Where<APAdjust.adjgRefNbr, Equal<APRegister.refNbr>, And<APAdjust.released, Equal<True>, And<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, PX.Data.And<Where<APAdjust.adjgDocType, Equal<APRegister.docType>, PX.Data.Or<Where<APAdjust.adjgDocType, Equal<APDocType.prepayment>, And<APAdjust.voided, Equal<True>, And<APRegister.docType, Equal<APDocType.voidCheck>>>>>>>>>>>>>, And<APRegister.tranPeriodID, Less<Required<APRegister.tranPeriodID>>, And2<Where<APRegister.released, Equal<True>, Or<APRegister.prebooked, Equal<True>>>, PX.Data.And<Where<APRegister.closedTranPeriodID, Less<Required<APRegister.closedTranPeriodID>>, Or<APRegister.closedTranPeriodID, PX.Data.IsNull>>>>>>>>.Config>.Select((PXGraph) this, (object) data.DocType, (object) data.RefNbr, (object) minPeriod, (object) minPeriod).Concat<PXResult<APRegister>>((IEnumerable<PXResult<APRegister>>) PXSelectBase<APRegister, PXSelectJoin<APRegister, LeftJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APRegister.docType>, And<APInvoice.refNbr, Equal<APRegister.refNbr>>>, LeftJoinSingleTable<APPayment, On<APPayment.docType, Equal<APRegister.docType>, And<APPayment.refNbr, Equal<APRegister.refNbr>>>>>, Where<APRegister.vendorID, Equal<Current<Vendor.bAccountID>>, And2<Exists<PX.Data.Select<APAdjust, Where<APAdjust.adjdDocType, Equal<APRegister.docType>, And<APAdjust.adjdRefNbr, Equal<APRegister.refNbr>, And<APAdjust.released, Equal<True>, And<APAdjust.adjgDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjdRefNbr>>>>>>>>>, And<APRegister.tranPeriodID, Less<Required<APRegister.tranPeriodID>>, And2<Where<APRegister.released, Equal<True>, Or<APRegister.prebooked, Equal<True>>>, PX.Data.And<Where<APRegister.closedTranPeriodID, Less<Required<APRegister.closedTranPeriodID>>, Or<APRegister.closedTranPeriodID, PX.Data.IsNull>>>>>>>>.Config>.Select((PXGraph) this, (object) data.DocType, (object) data.RefNbr, (object) minPeriod, (object) minPeriod))))
        {
          APRegister apRegister = (APRegister) fullDocument;
          if (apRegister != null && !documents.Contains(apRegister))
          {
            list1.Add(new
            {
              DocType = apRegister.DocType,
              RefNbr = apRegister.RefNbr
            });
            documents.Add(apRegister);
          }
        }
      }
    }
  }

  public virtual void RetrievePPVAccount(
    PXGraph aOpGraph,
    PX.Objects.PO.POReceiptLine aLine,
    ref int? aPPVAcctID,
    ref int? aPPVSubID)
  {
    aPPVAcctID = new int?();
    aPPVSubID = new int?();
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, aLine.InventoryID);
    INPostClass postclass = INPostClass.PK.Find((PXGraph) this, inventoryItem?.PostClassID);
    INSite site = INSite.PK.Find((PXGraph) this, aLine.SiteID);
    if (inventoryItem == null || postclass == null || site == null)
      return;
    aPPVAcctID = INReleaseProcess.GetAcctID<INPostClass.pPVAcctID>(aOpGraph, postclass.PPVAcctDefault, inventoryItem, site, postclass);
    try
    {
      aPPVSubID = INReleaseProcess.GetSubID<INPostClass.pPVSubID>(aOpGraph, postclass.PPVAcctDefault, postclass.PPVSubMask, inventoryItem, site, postclass);
    }
    catch (PXException ex)
    {
      throw new PXException("PPV Subaccount mask cannot be assembled correctly. Please, check settings for the Inventory Posting Class");
    }
  }

  public virtual void VoidDocProc(JournalEntry je, APRegister doc)
  {
    if (doc.Released.GetValueOrDefault() && doc.Prebooked.GetValueOrDefault())
      throw new PXException("Pre-released Documents can not be voided after they are released");
    if (doc.Prebooked.GetValueOrDefault() && string.IsNullOrEmpty(doc.PrebookBatchNbr))
      throw new PXException("The document {0} {1} is marked as pre-released, but the link to the Pre-releasing batch is missed. Void operation may not be made", new object[2]
      {
        (object) doc.DocType,
        (object) doc.RefNbr
      });
    APAdjust apAdjust = (APAdjust) PXSelectBase<APAdjust, PXSelectReadonly<APAdjust, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr);
    if (apAdjust != null && !string.IsNullOrEmpty(apAdjust.AdjgRefNbr))
      throw new PXException("Pre-released Document may not be voided if payment(s) has been applied to it");
    APTran apTran = (APTran) PXSelectBase<APTran, PXSelectReadonly<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>, PX.Data.And<Where<APTran.pONbr, PX.Data.IsNotNull, Or<APTran.receiptNbr, PX.Data.IsNotNull>>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr);
    if (apTran != null && !string.IsNullOrEmpty(apTran.RefNbr))
      throw new PXException("Document conatains details, linked to document(s) in Purchase Order Module. Void Operation is not possible");
    APTaxTran apTaxTran1 = (APTaxTran) PXSelectBase<APTaxTran, PXSelectReadonly<APTaxTran, Where<APTaxTran.tranType, Equal<Required<APTaxTran.tranType>>, And<APTaxTran.refNbr, Equal<Required<APTaxTran.refNbr>>, And<APTaxTran.taxPeriodID, PX.Data.IsNotNull>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr);
    if (apTaxTran1 != null && !string.IsNullOrEmpty(apTaxTran1.TaxID))
      throw new PXException("Tax report has been created for the document {0} {1}. Void operation is impossible.");
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.APDocument.Cache.SetStatus((object) doc, PXEntryStatus.Updated);
      PX.Objects.GL.Batch batch = (PX.Objects.GL.Batch) PXSelectBase<PX.Objects.GL.Batch, PXSelectReadonly<PX.Objects.GL.Batch, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleAP>, And<PX.Objects.GL.Batch.batchNbr, Equal<Required<PX.Objects.GL.Batch.batchNbr>>>>>.Config>.Select((PXGraph) this, (object) (doc.Prebooked.GetValueOrDefault() ? doc.PrebookBatchNbr : doc.BatchNbr));
      if (batch == null && string.IsNullOrEmpty(batch.BatchNbr))
        throw new PXException("Pre-releasing batch {0} {1} may not be found in DB. Void operation is impossible.", new object[2]
        {
          (object) "AP",
          (object) doc.PrebookBatchNbr
        });
      je.ReverseDocumentBatch(batch);
      PX.Objects.GL.Batch copy1 = (PX.Objects.GL.Batch) je.BatchModule.Cache.CreateCopy((object) je.BatchModule.Current);
      copy1.Hold = new bool?(false);
      je.BatchModule.Update(copy1);
      if (doc.OpenDoc.GetValueOrDefault())
      {
        PX.Objects.GL.GLTran glTranAp = APReleaseProcess.CreateGLTranAP(je, doc, true);
        PX.Objects.GL.GLTran tran = (PX.Objects.GL.GLTran) null;
        foreach (PXResult<PX.Objects.GL.GLTran> pxResult in je.GLTranModuleBatNbr.Select())
        {
          PX.Objects.GL.GLTran glTran = (PX.Objects.GL.GLTran) pxResult;
          if (tran == null)
          {
            int? nullable1 = glTran.AccountID;
            int? nullable2 = glTranAp.AccountID;
            if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
            {
              nullable2 = glTran.SubID;
              nullable1 = glTranAp.SubID;
              if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              {
                nullable1 = glTran.ReferenceID;
                nullable2 = glTranAp.ReferenceID;
                if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && glTran.TranType == glTranAp.TranType && glTran.RefNbr == glTranAp.RefNbr)
                {
                  nullable2 = glTran.ReferenceID;
                  nullable1 = glTranAp.ReferenceID;
                  if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                  {
                    nullable1 = glTran.BranchID;
                    nullable2 = glTranAp.BranchID;
                    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                    {
                      Decimal? nullable3 = glTran.CuryCreditAmt;
                      Decimal? nullable4 = glTran.CuryCreditAmt;
                      if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                      {
                        nullable4 = glTran.CuryDebitAmt;
                        nullable3 = glTran.CuryDebitAmt;
                        if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                          tran = glTran;
                      }
                    }
                  }
                }
              }
            }
          }
          glTran.Released = new bool?(true);
        }
        if (tran == null)
          throw new PXException("AP Transaction is not found in the reversing batch");
        this.UpdateHistory(tran, doc.VendorID);
        this.UpdateHistory(tran, doc.VendorID, doc.CuryID);
      }
      foreach (PXResult<APTaxTran> pxResult in this.APTaxTran_TranType_RefNbr.Select((object) doc.DocType, (object) doc.RefNbr))
      {
        APTaxTran apTaxTran2 = (APTaxTran) pxResult;
        PXCache cache = this.APTaxTran_TranType_RefNbr.Cache;
        PXCache<APTaxTran>.StoreOriginal((PXGraph) this, apTaxTran2);
        APTaxTran apTaxTran3 = apTaxTran2;
        APTaxTran copy2 = (APTaxTran) cache.CreateCopy((object) apTaxTran3);
        copy2.Voided = new bool?(true);
        this.APTaxTran_TranType_RefNbr.Update(copy2);
      }
      if (je.GLTranModuleBatNbr.Cache.IsInsertedUpdatedDeleted)
        je.Persist();
      if (!je.BatchModule.Cache.IsDirty && string.IsNullOrEmpty(doc.VoidBatchNbr) && (this.APInvoice_DocType_RefNbr.Current == null || this.APInvoice_DocType_RefNbr.Current.DocType != "PPM"))
        doc.VoidBatchNbr = je.BatchModule.Current.BatchNbr;
      doc.OpenDoc = new bool?(false);
      doc.Voided = new bool?(true);
      doc.CuryDocBal = new Decimal?(0M);
      doc.DocBal = new Decimal?(0M);
      doc = (APRegister) this.APDocument.Cache.Update((object) doc);
      this.ProcessVoidTranPost(doc);
      if (doc.DocType != "ADR" && this.APDocument.Cache.ObjectsEqual((object) doc, (object) this.APPayment_DocType_RefNbr.Current))
        this.APPayment_DocType_RefNbr.Cache.SetStatus((object) this.APPayment_DocType_RefNbr.Current, PXEntryStatus.Notchanged);
      this.APDocument.Cache.Persist(PXDBOperation.Update);
      this.APTran_TranType_RefNbr.Cache.Persist(PXDBOperation.Update);
      this.APTaxTran_TranType_RefNbr.Cache.Persist(PXDBOperation.Update);
      this.Caches[typeof (APHist)].Persist(PXDBOperation.Insert);
      this.Caches[typeof (CuryAPHist)].Persist(PXDBOperation.Insert);
      this.Caches[typeof (APTranPost)].Persist(PXDBOperation.Insert);
      this.AP1099Year_Select.Cache.Persist(PXDBOperation.Insert);
      this.AP1099History_Select.Cache.Persist(PXDBOperation.Insert);
      this.CurrencyInfo_CuryInfoID.Cache.Persist(PXDBOperation.Update);
      transactionScope.Complete((PXGraph) this);
    }
    this.APPayment_DocType_RefNbr.Cache.Persisted(false);
    this.APDocument.Cache.Persisted(false);
    this.APTran_TranType_RefNbr.Cache.Persisted(false);
    this.APTaxTran_TranType_RefNbr.Cache.Persisted(false);
    this.APAdjust_AdjgDocType_RefNbr_VendorID.Cache.Persisted(false);
    this.Caches[typeof (APHist)].Persisted(false);
    this.Caches[typeof (CuryAPHist)].Persisted(false);
    this.AP1099Year_Select.Cache.Persisted(false);
    this.AP1099History_Select.Cache.Persisted(false);
    this.CurrencyInfo_CuryInfoID.Cache.Persisted(false);
  }

  protected static PX.Objects.GL.GLTran CreateGLTranAP(
    JournalEntry journalEntry,
    APRegister doc,
    bool aReversed)
  {
    PX.Objects.GL.GLTran row = new PX.Objects.GL.GLTran();
    row.SummPost = new bool?(true);
    row.BranchID = doc.BranchID;
    row.AccountID = doc.APAccountID;
    row.SubID = doc.APSubID;
    row.ReclassificationProhibited = new bool?(true);
    bool flag = APInvoiceType.DrCr(doc.DocType) == "D";
    row.CuryDebitAmt = !flag || aReversed ? doc.CuryOrigDocAmt : new Decimal?(0M);
    PX.Objects.GL.GLTran glTran1 = row;
    Decimal? nullable1;
    if (!flag || aReversed)
    {
      Decimal? origDocAmt = doc.OrigDocAmt;
      Decimal? rgolAmt = doc.RGOLAmt;
      nullable1 = origDocAmt.HasValue & rgolAmt.HasValue ? new Decimal?(origDocAmt.GetValueOrDefault() - rgolAmt.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable1 = new Decimal?(0M);
    glTran1.DebitAmt = nullable1;
    row.CuryCreditAmt = !flag || aReversed ? new Decimal?(0M) : doc.CuryOrigDocAmt;
    PX.Objects.GL.GLTran glTran2 = row;
    Decimal? nullable2;
    if (!flag || aReversed)
    {
      nullable2 = new Decimal?(0M);
    }
    else
    {
      Decimal? origDocAmt = doc.OrigDocAmt;
      Decimal? rgolAmt = doc.RGOLAmt;
      nullable2 = origDocAmt.HasValue & rgolAmt.HasValue ? new Decimal?(origDocAmt.GetValueOrDefault() - rgolAmt.GetValueOrDefault()) : new Decimal?();
    }
    glTran2.CreditAmt = nullable2;
    row.TranType = doc.DocType;
    row.TranClass = doc.DocClass;
    row.RefNbr = doc.RefNbr;
    row.TranDesc = doc.DocDesc;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(journalEntry.GLTranModuleBatNbr.Cache, (object) row, doc.FinPeriodID);
    row.TranDate = doc.DocDate;
    row.ReferenceID = doc.VendorID;
    return row;
  }

  private bool IsPayByLineRetainageDebitAdj(APRegister doc)
  {
    if (doc.PaymentsByLinesAllowed.GetValueOrDefault() || !doc.IsChildRetainageDocument() && !doc.IsOriginalRetainageDocument() || !doc.IsRetainageReversing.GetValueOrDefault())
      return false;
    APRegister retainageDocument = this.GetOriginalRetainageDocument(doc);
    return retainageDocument != null && retainageDocument.PaymentsByLinesAllowed.GetValueOrDefault();
  }

  public virtual APRegister GetOriginalRetainageDocument(APRegister childRetainageDoc)
  {
    APRegister retainageDocument = (APRegister) (APInvoice) PXSelectBase<APInvoice, PXSelect<APInvoice, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>, And<APInvoice.retainageApply, Equal<True>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) childRetainageDoc.OrigDocType, (object) childRetainageDoc.OrigRefNbr);
    if (this.APDocument.Cache.Locate((object) retainageDocument) is APRegister copy && retainageDocument != null)
      this.APDocument.Cache.RestoreCopy((object) retainageDocument, (object) copy);
    return retainageDocument;
  }

  public virtual APTran GetOriginalRetainageLine(
    APRegister childRetainageDoc,
    APTran childRetainageTran)
  {
    return (APTran) PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>, And<APTran.lineNbr, Equal<Required<APTran.lineNbr>>, And<APTran.curyRetainageAmt, NotEqual<decimal0>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) childRetainageDoc.OrigDocType, (object) childRetainageDoc.OrigRefNbr, (object) childRetainageTran.OrigLineNbr);
  }

  public virtual bool IsFullyProcessedOriginalRetainageDocument(APRegister origRetainageInvoice)
  {
    bool flag = true;
    foreach (PXResult<APRegister> retainageDocument in this.SelectRetainageDocuments(origRetainageInvoice))
    {
      if (!((APRegister) retainageDocument).HasZeroBalance<APRegister.curyDocBal, APTran.curyTranBal>((PXGraph) this))
      {
        flag = false;
        break;
      }
    }
    return ((!origRetainageInvoice.HasZeroBalance<APRegister.curyDocBal, APTran.curyTranBal>((PXGraph) this) ? 0 : (origRetainageInvoice.HasZeroBalance<APRegister.curyRetainageUnreleasedAmt, APTran.curyRetainageBal>((PXGraph) this) ? 1 : 0)) & (flag ? 1 : 0)) != 0;
  }

  private PXResultset<APRegister> SelectRetainageDocuments(APRegister origRetainageInvoice)
  {
    return PXSelectBase<APRegister, PXSelect<APRegister, Where<APRegister.isRetainageDocument, Equal<True>, And<APRegister.origDocType, Equal<Required<APRegister.docType>>, And<APRegister.origRefNbr, Equal<Required<APRegister.refNbr>>, And<APRegister.released, Equal<True>>>>>>.Config>.Select((PXGraph) this, (object) origRetainageInvoice.DocType, (object) origRetainageInvoice.RefNbr);
  }

  /// <summary>
  /// Posts per-unit tax amounts to document lines' accounts.
  /// This is an extension point, actual posting is done by graph extension <see cref="T:PX.Objects.AP.APReleaseProcessPerUnitTaxPoster" />
  /// which overrides this method if the festure "Per-unit Tax Support" is turned on.
  /// </summary>
  protected virtual void PostPerUnitTaxAmounts(
    JournalEntry journalEntry,
    APInvoice invoice,
    PX.Objects.CM.Extensions.CurrencyInfo newCurrencyInfo,
    APTaxTran perUnitAggregatedTax,
    PX.Objects.TX.Tax perUnitTax,
    bool isDebitTaxTran)
  {
  }

  /// <summary>
  /// The method to insert invoice GL transactions
  /// for the <see cref="T:PX.Objects.AP.APInvoice" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.ReleaseInvoice(PX.Objects.GL.JournalEntry,PX.Objects.AP.APRegister@,PX.Data.PXResult{PX.Objects.AP.APInvoice,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.CS.Terms,PX.Objects.AP.Vendor},System.Boolean,System.Collections.Generic.List{PX.Objects.IN.INRegister}@)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert invoice tax GL transactions
  /// for the <see cref="T:PX.Objects.AP.APTaxTran" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.ReleaseInvoice(PX.Objects.GL.JournalEntry,PX.Objects.AP.APRegister@,PX.Data.PXResult{PX.Objects.AP.APInvoice,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.CS.Terms,PX.Objects.AP.Vendor},System.Boolean,System.Collections.Generic.List{PX.Objects.IN.INRegister}@)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceTaxTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert tax expense GL transactions
  /// for the <see cref="T:PX.Objects.AP.APTaxTran" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.PostTaxExpenseToItemAccounts(PX.Objects.GL.JournalEntry,PX.Objects.AP.APInvoice,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.AP.APTaxTran,PX.Objects.TX.Tax,System.Boolean)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APTranRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APTaxTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceTaxExpenseItemAccountsTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert tax expense GL transactions for the <see cref="T:PX.Objects.AP.APTaxTran" /> entity inside the
  /// <see cref="!:PerUnitTaxesPostOnRelease.PostPerUnitTaxAmountsToItemAccounts(APInvoice, CurrencyInfo, APTaxTran, Tax, bool, bool)" /> helper method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APTranRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APTaxTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoicePerUnitTaxAmountsToItemAccountsTransaction(
    JournalEntry journalEntryGraph,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    ExceptionExtensions.ThrowOnNull<JournalEntry>(journalEntryGraph, nameof (journalEntryGraph), (string) null);
    return journalEntryGraph.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert grouped by project key tax GL transactions
  /// for the <see cref="T:PX.Objects.AP.APTaxTran" /> entity inside the
  /// <see cref="!:PostTaxAmountByProjectKey(JournalEntry, APInvoice, CurrencyInfo, APTaxTran, Tax, bool)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APTranRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APTaxTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceTaxByProjectKeyTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert invoice tax expense GL transactions
  /// for the <see cref="T:PX.Objects.AP.APTaxTran" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.PostTaxExpenseToSingleAccount(PX.Objects.GL.JournalEntry,PX.Objects.AP.APInvoice,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.AP.APTaxTran,PX.Objects.TX.Tax)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APTaxTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceTaxExpenseSingeAccountTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert invoice reverse tax GL transactions
  /// for the <see cref="T:PX.Objects.AP.APTaxTran" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.PostReverseTax(PX.Objects.GL.JournalEntry,PX.Objects.AP.APInvoice,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.AP.APTaxTran,PX.Objects.TX.Tax)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APTaxTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceReverseTaxTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert invoice general tax GL transactions
  /// for the <see cref="T:PX.Objects.AP.APTaxTran" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.PostGeneralTax(PX.Objects.GL.JournalEntry,PX.Objects.AP.APInvoice,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.AP.APTaxTran,PX.Objects.TX.Tax)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APTaxTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceGeneralTaxTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert invoice rounding GL transactions
  /// for the <see cref="T:PX.Objects.AP.APInvoice" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.ReleaseInvoice(PX.Objects.GL.JournalEntry,PX.Objects.AP.APRegister@,PX.Data.PXResult{PX.Objects.AP.APInvoice,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.CS.Terms,PX.Objects.AP.Vendor},System.Boolean,System.Collections.Generic.List{PX.Objects.IN.INRegister}@)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceRoundingTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert invoice details GL transactions
  /// for the <see cref="T:PX.Objects.AP.APTran" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.ReleaseInvoice(PX.Objects.GL.JournalEntry,PX.Objects.AP.APRegister@,PX.Data.PXResult{PX.Objects.AP.APInvoice,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.CS.Terms,PX.Objects.AP.Vendor},System.Boolean,System.Collections.Generic.List{PX.Objects.IN.INRegister}@)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceDetailsTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert invoice details schedule GL transactions
  /// for the <see cref="T:PX.Objects.AP.APTran" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.ReleaseInvoice(PX.Objects.GL.JournalEntry,PX.Objects.AP.APRegister@,PX.Data.PXResult{PX.Objects.AP.APInvoice,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.CS.Terms,PX.Objects.AP.Vendor},System.Boolean,System.Collections.Generic.List{PX.Objects.IN.INRegister}@)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceDetailsScheduleTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert invoice details POReceiptLine GL transactions
  /// for the <see cref="T:PX.Objects.PO.POReceiptLine" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.ReleaseInvoice(PX.Objects.GL.JournalEntry,PX.Objects.AP.APRegister@,PX.Data.PXResult{PX.Objects.AP.APInvoice,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.CS.Terms,PX.Objects.AP.Vendor},System.Boolean,System.Collections.Generic.List{PX.Objects.IN.INRegister}@)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APTranRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.POReceiptLineRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertInvoiceDetailsPOReceiptLineTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to return PO Accrual Account for GL Tran by taxes
  /// </summary>
  protected virtual void GetItemCostTaxAccount(
    APRegister apdoc,
    PX.Objects.TX.Tax tax,
    APTran apTran,
    APTaxTran apTaxTran,
    PX.Objects.PM.Lite.PMProject project,
    PX.Objects.PM.Lite.PMTask task,
    out int? accountID,
    out int? subID)
  {
    accountID = apTran.AccountID;
    subID = apTran.SubID;
  }

  /// <summary>
  /// The method to insert payment GL transactions
  /// for the <see cref="T:PX.Objects.AP.APPayment" /> entity inside the
  /// <see cref="!:ProcessPayment(JournalEntry, APRegister, PXResult&lt;APPayment, CurrencyInfo, CM.Currency, Vendor, CashAccount&gt;)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertPaymentTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert payment charge GL transactions
  /// for the <see cref="T:PX.Objects.AP.APPaymentChargeTran" /> entity inside the
  /// <see cref="!:ProcessPayment(JournalEntry, APRegister, PXResult&lt;APPayment, CurrencyInfo, CM.Currency, Vendor, CashAccount&gt;)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APPaymentChargeTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertPaymentChargeTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert adjustments adjusting GL transactions
  /// for the <see cref="T:PX.Objects.AP.APAdjust" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.ProcessAdjustmentAdjusting(PX.Objects.GL.JournalEntry,PX.Objects.AP.APAdjust,PX.Objects.AP.APPayment,PX.Objects.AP.Vendor,PX.Objects.CM.Extensions.CurrencyInfo)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APAdjustRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertAdjustmentsAdjustingTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert adjustments adjusted GL transactions
  /// for the <see cref="T:PX.Objects.AP.APAdjust" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.ProcessAdjustmentAdjusted(PX.Objects.GL.JournalEntry,PX.Objects.AP.APAdjust,PX.Objects.AP.APPayment,PX.Objects.AP.Vendor,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.CM.Extensions.CurrencyInfo)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APAdjustRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertAdjustmentsAdjustedTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert adjustments GOL GL transactions
  /// for the <see cref="T:PX.Objects.AP.APAdjust" /> entity inside the
  /// <see cref="!:ProcessAdjustmentGOL(JournalEntry, APAdjust, APPayment, Vendor, CM.Currency, CM.Currency, CurrencyInfo, CurrencyInfo)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APAdjustRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertAdjustmentsGOLTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert adjustments cash discount GL transactions
  /// for the <see cref="T:PX.Objects.AP.APAdjust" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.ProcessAdjustmentCashDiscount(PX.Objects.GL.JournalEntry,PX.Objects.AP.APAdjust,PX.Objects.AP.APPayment,PX.Objects.AP.Vendor,PX.Objects.CM.Extensions.CurrencyInfo,PX.Objects.CM.Extensions.CurrencyInfo)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APAdjustRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertAdjustmentsCashDiscountTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert adjustments withholding tax GL transactions
  /// for the <see cref="T:PX.Objects.AP.APAdjust" /> entity inside the
  /// <see cref="M:PX.Objects.AP.APReleaseProcess.CreateGLTranForWhTaxTran(PX.Objects.GL.JournalEntry,PX.Objects.AP.APAdjust,PX.Objects.AP.APPayment,PX.Objects.AP.APTaxTran,PX.Objects.AP.Vendor,PX.Objects.CM.Extensions.CurrencyInfo,System.Boolean)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APTaxTranRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APAdjustRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertAdjustmentsWhTaxTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  /// <summary>
  /// The method to insert adjustments rounding GL transactions
  /// for the <see cref="T:PX.Objects.AP.APAdjust" /> entity inside the
  /// <see cref="!:VerifyPaymentRoundAndClose(JournalEntry, APRegister, APPayment, Vendor, CurrencyInfo, CM.Currency, Tuple&lt;APAdjust, CurrencyInfo&gt;)" /> method.
  /// <see cref="T:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APRegisterRecord" />,
  /// <see cref="P:PX.Objects.AP.APReleaseProcess.GLTranInsertionContext.APAdjustRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertAdjustmentsRoundingTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    APReleaseProcess.GLTranInsertionContext context)
  {
    return je.GLTranModuleBatNbr.Insert(tran);
  }

  public class MultiCurrency : APMultiCurrencyGraph<APReleaseProcess, APRegister>
  {
    protected override string DocumentStatus => this.Base.APDocument.Current?.Status;

    protected override CurySource CurrentSourceSelect() => (CurySource) null;

    protected override MultiCurrencyGraph<APReleaseProcess, APRegister>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<APReleaseProcess, APRegister>.DocumentMapping(typeof (APRegister))
      {
        DocumentDate = typeof (APRegister.docDate),
        BAccountID = typeof (APRegister.vendorID)
      };
    }

    protected override IEnumerable<System.Type> FieldWhichShouldBeRecalculatedAnyway
    {
      get
      {
        yield return typeof (APInvoice.curyDiscBal);
      }
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[5]
      {
        (PXSelectBase) this.Base.APTaxTran_TranType_RefNbr,
        (PXSelectBase) this.Base.APInvoice_DocType_RefNbr,
        (PXSelectBase) this.Base.APPayment_DocType_RefNbr,
        (PXSelectBase) this.Base.APTran_TranType_RefNbr,
        (PXSelectBase) this.Base.APDocument
      };
    }

    public void UpdateCurrencyInfoForPrepayment(
      APPayment prepayment,
      PX.Objects.CM.Extensions.CurrencyInfo origCuryInfoToUse)
    {
      this.TrackedItems[this.Base.APPayment_DocType_RefNbr.Cache.GetItemType()].Single<CuryField>((Func<CuryField, bool>) (f => f.CuryName.Equals("curyDocBal", StringComparison.OrdinalIgnoreCase))).BaseCalc = true;
      prepayment.CuryInfoID = origCuryInfoToUse.CuryInfoID;
    }
  }

  private class APHistBucket
  {
    public int? apAccountID;
    public int? apSubID;
    public Decimal SignPayments;
    public Decimal SignDeposits;
    public Decimal SignPurchases;
    public Decimal SignDrAdjustments;
    public Decimal SignCrAdjustments;
    public Decimal SignDiscTaken;
    public Decimal SignWhTax;
    public Decimal SignRGOL;
    public Decimal SignPtd;
    public Decimal SignRetainageWithheld;
    public Decimal SignRetainageReleased;

    public APHistBucket(PX.Objects.GL.GLTran tran, string TranType)
    {
      this.apAccountID = tran.AccountID;
      this.apSubID = tran.SubID;
      string s = TranType + tran.TranClass;
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(s))
      {
        case 78861843:
          if (!(s == "VCKN"))
            return;
          goto label_136;
        case 171876754:
          if (!(s == "REFD"))
            return;
          goto label_138;
        case 241556766:
          if (!(s == "RQCW"))
            return;
          goto label_143;
        case 246638033:
          if (!(s == "VCKD"))
            return;
          goto label_138;
        case 272542468:
          if (!(s == "REFN"))
            return;
          goto label_136;
        case 325444861:
          if (!(s == "RQCR"))
            return;
          goto label_137;
        case 339652944:
          if (!(s == "REFR"))
            return;
          goto label_137;
        case 366783721:
          if (!(s == "ACRN"))
            return;
          this.SignCrAdjustments = -1M;
          this.SignPtd = -1M;
          return;
        case 373208182:
          if (!(s == "REFP"))
            return;
          goto label_136;
        case 392555337:
          if (!(s == "RQCN"))
            return;
          break;
        case 450671816:
          if (!(s == "ACRE"))
            return;
          this.apAccountID = tran.OrigAccountID;
          this.apSubID = tran.OrigSubID;
          this.SignRetainageWithheld = -1M;
          return;
        case 457096277:
          if (!(s == "REFU"))
            return;
          goto label_142;
        case 464747080:
          if (!(s == "VCKW"))
            return;
          goto label_143;
        case 493221051:
          if (!(s == "RQCD"))
            return;
          goto label_138;
        case 498302318:
          if (!(s == "VCKU"))
            return;
          goto label_141;
        case 501004673:
          if (!(s == "ACRF"))
            return;
          this.SignCrAdjustments = -1M;
          this.SignRetainageReleased = -1M;
          this.SignPtd = -1M;
          return;
        case 548635175:
          if (!(s == "VCKR"))
            return;
          goto label_137;
        case 582190413:
          if (!(s == "VCKP"))
            return;
          goto label_136;
        case 736032930:
          if (!(s == "INVN"))
            return;
          this.SignPurchases = -1M;
          this.SignPtd = -1M;
          return;
        case 853476263:
          if (!(s == "INVE"))
            return;
          this.apAccountID = tran.OrigAccountID;
          this.apSubID = tran.OrigSubID;
          this.SignRetainageWithheld = -1M;
          return;
        case 870253882:
          if (!(s == "INVF"))
            return;
          this.SignPurchases = -1M;
          this.SignRetainageReleased = -1M;
          this.SignPtd = -1M;
          return;
        case 1030676761:
          if (!(s == "VQCR"))
            return;
          goto label_137;
        case 1081009618:
          if (!(s == "VQCW"))
            return;
          goto label_143;
        case 1105210828:
          if (!(s == "PPMD"))
            return;
          goto label_138;
        case 1272987018:
          if (!(s == "PPMN"))
            return;
          goto label_136;
        case 1273678566:
          if (!(s == "PPIN"))
            return;
          this.SignPayments = 1M;
          this.SignPtd = -1M;
          return;
        case 1306542256:
          if (!(s == "PPMP"))
            return;
          this.SignDeposits = 1M;
          return;
        case 1332673903:
          if (!(s == "VQCD"))
            return;
          goto label_138;
        case 1340097494:
          if (!(s == "PPMR"))
            return;
          goto label_137;
        case 1351463269:
          if (!(s == "ADRY"))
            return;
          goto label_145;
        case 1374344280:
          if (!(s == "PPID"))
            return;
          goto label_138;
        case 1390430351:
          if (!(s == "PPMU"))
            return;
          this.SignDeposits = 1M;
          return;
        case 1423985589:
          if (!(s == "PPMW"))
            return;
          goto label_143;
        case 1435351364:
          if (!(s == "ADRR"))
            return;
          this.apAccountID = tran.OrigAccountID;
          this.apSubID = tran.OrigSubID;
          this.SignDrAdjustments = 1M;
          this.SignRGOL = -1M;
          return;
        case 1457540827:
          if (!(s == "PPMY"))
            return;
          goto label_145;
        case 1468906602:
          if (!(s == "ADRP"))
            return;
          goto label_131;
        case 1474318446:
          int num1 = s == "PPMZ" ? 1 : 0;
          return;
        case 1500450093:
          if (!(s == "VQCN"))
            return;
          break;
        case 1502461840:
          if (!(s == "ADRN"))
            return;
          goto label_131;
        case 1592453327:
          if (!(s == "PPIY"))
            return;
          goto label_145;
        case 1636682792:
          if (!(s == "ADRF"))
            return;
          this.SignDrAdjustments = 1M;
          this.SignRetainageReleased = -1M;
          this.SignPtd = -1M;
          return;
        case 1670238030:
          if (!(s == "ADRD"))
            return;
          this.apAccountID = tran.OrigAccountID;
          this.apSubID = tran.OrigSubID;
          this.SignDrAdjustments = 1M;
          this.SignDiscTaken = -1M;
          return;
        case 1687015649:
          if (!(s == "ADRE"))
            return;
          this.apAccountID = tran.OrigAccountID;
          this.apSubID = tran.OrigSubID;
          this.SignRetainageWithheld = -1M;
          return;
        case 1709896660:
          if (!(s == "PPIP"))
            return;
          goto label_145;
        case 1743451898:
          if (!(s == "PPIR"))
            return;
          goto label_137;
        case 1961706867:
          if (!(s == "VRFD"))
            return;
          goto label_138;
        case 2129483057:
          if (!(s == "VRFN"))
            return;
          goto label_136;
        case 2213371152:
          if (!(s == "VRFU"))
            return;
          goto label_142;
        case 2297259247:
          if (!(s == "VRFP"))
            return;
          goto label_136;
        case 2330814485:
          if (!(s == "VRFR"))
            return;
          goto label_137;
        case 2595402688:
          if (!(s == "QCKR"))
            return;
          goto label_137;
        case 2642788831:
          int num2 = s == "CHKZ" ? 1 : 0;
          return;
        case 2659566450:
          if (!(s == "CHKY"))
            return;
          goto label_145;
        case 2679290783:
          if (!(s == "QCKW"))
            return;
          goto label_143;
        case 2693121688:
          if (!(s == "CHKW"))
            return;
          goto label_143;
        case 2726676926:
          if (!(s == "CHKU"))
            return;
          goto label_141;
        case 2777009783:
          if (!(s == "CHKR"))
            return;
          goto label_137;
        case 2810565021:
          if (!(s == "CHKP"))
            return;
          goto label_136;
        case 2844120259:
          if (!(s == "CHKN"))
            return;
          goto label_136;
        case 2964510306:
          if (!(s == "QCKD"))
            return;
          goto label_138;
        case 3011896449:
          if (!(s == "CHKD"))
            return;
          goto label_138;
        case 3065176020:
          if (!(s == "QCKN"))
            return;
          this.SignPurchases = 1M;
          this.SignPayments = 1M;
          this.SignPtd = 0M;
          return;
        default:
          return;
      }
      this.SignPurchases = 1M;
      this.SignPayments = 1M;
      this.SignPtd = 0M;
      return;
label_131:
      this.SignDrAdjustments = 1M;
      this.SignPtd = -1M;
      return;
label_136:
      this.SignPayments = 1M;
      this.SignPtd = -1M;
      return;
label_137:
      this.apAccountID = tran.OrigAccountID;
      this.apSubID = tran.OrigSubID;
      this.SignPayments = 1M;
      this.SignRGOL = -1M;
      return;
label_138:
      this.apAccountID = tran.OrigAccountID;
      this.apSubID = tran.OrigSubID;
      this.SignPayments = 1M;
      this.SignDiscTaken = -1M;
      return;
label_141:
      this.SignDeposits = 1M;
      return;
label_142:
      this.SignDeposits = 1M;
      return;
label_143:
      this.apAccountID = tran.OrigAccountID;
      this.apSubID = tran.OrigSubID;
      this.SignPayments = 1M;
      this.SignWhTax = -1M;
      return;
label_145:
      this.SignDeposits = 1M;
    }

    public APHistBucket()
    {
    }
  }

  public class LineBalances : 
    Tuple<ARReleaseProcess.Amount, ARReleaseProcess.Amount, ARReleaseProcess.Amount>
  {
    public ARReleaseProcess.Amount CashDiscountBalance => this.Item1;

    public ARReleaseProcess.Amount RetainageBalance => this.Item2;

    public ARReleaseProcess.Amount TranBalance => this.Item3;

    public LineBalances(Decimal? initValue)
      : base(new ARReleaseProcess.Amount(initValue, initValue), new ARReleaseProcess.Amount(initValue, initValue), new ARReleaseProcess.Amount(initValue, initValue))
    {
    }

    public LineBalances(
      ARReleaseProcess.Amount cashDiscountBalance,
      ARReleaseProcess.Amount retainageBalance,
      ARReleaseProcess.Amount tranBalance)
      : base(cashDiscountBalance, retainageBalance, tranBalance)
    {
    }

    public static APReleaseProcess.LineBalances operator +(
      APReleaseProcess.LineBalances a,
      APReleaseProcess.LineBalances b)
    {
      return new APReleaseProcess.LineBalances(a.CashDiscountBalance + b.CashDiscountBalance, a.RetainageBalance + b.RetainageBalance, a.TranBalance + b.TranBalance);
    }

    public static APReleaseProcess.LineBalances operator -(
      APReleaseProcess.LineBalances a,
      APReleaseProcess.LineBalances b)
    {
      return new APReleaseProcess.LineBalances(a.CashDiscountBalance - b.CashDiscountBalance, a.RetainageBalance - b.RetainageBalance, a.TranBalance - b.TranBalance);
    }
  }

  private class APDocTypePeriodSorting : IComparer<Tuple<string, string>>
  {
    public int Compare(Tuple<string, string> x, Tuple<string, string> y)
    {
      short? nullable = APDocType.SortOrder(y.Item1);
      int num1 = (int) nullable.Value;
      nullable = APDocType.SortOrder(x.Item1);
      int num2 = (int) nullable.Value;
      int num3 = System.Math.Sign(num1 - num2);
      if (num3 == 0)
        num3 = string.CompareOrdinal(x.Item2, y.Item2);
      return num3;
    }
  }

  public class GLTranInsertionContext
  {
    public virtual APRegister APRegisterRecord { get; set; }

    public virtual APTran APTranRecord { get; set; }

    public virtual APTaxTran APTaxTranRecord { get; set; }

    public virtual APPaymentChargeTran APPaymentChargeTranRecord { get; set; }

    public virtual APAdjust APAdjustRecord { get; set; }

    public virtual PX.Objects.PO.POReceiptLine POReceiptLineRecord { get; set; }
  }

  /// <exclude />
  public class ExtensionSort : 
    SortExtensionsBy<TypeArrayOf<PXGraphExtension<APReleaseProcess>>.FilledWith<APReleaseProcess.MultiCurrency, UpdatePOOnRelease, AffectedPOOrdersByAPRelease>>
  {
  }
}
