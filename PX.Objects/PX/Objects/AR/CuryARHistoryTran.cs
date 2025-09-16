// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CuryARHistoryTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select<ARTranPostGL>), Persistent = false)]
[PXCacheName("AR History Currency Transaction")]
public class CuryARHistoryTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlTable = typeof (ARTranPostGL))]
  public virtual int? ID { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (ARTranPostGL))]
  [PXUIField(DisplayName = "Doc. Type")]
  [ARDocType.List]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (ARTranPostGL))]
  [PXUIField]
  public virtual string RefNbr { get; set; }

  [PXDBInt(BqlTable = typeof (ARTranPostGL))]
  [PXUIField(DisplayName = "Line Nbr.")]
  public virtual int? LineNbr { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPostGL))]
  [PXUIField(DisplayName = "Source Doc. Type")]
  [ARDocType.List]
  public virtual string SourceDocType { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPostGL))]
  [PXUIField]
  public virtual string SourceRefNbr { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPostGL))]
  public virtual string CuryID { get; set; }

  [PXDBLong(BqlTable = typeof (ARTranPostGL))]
  public virtual long? CuryInfoID { get; set; }

  [PXUIField(DisplayName = "Batch")]
  [Branch(null, null, true, true, true, BqlTable = typeof (ARTranPostGL))]
  public virtual int? BranchID { get; set; }

  [Customer(BqlTable = typeof (ARTranPostGL))]
  public virtual int? CustomerID { get; set; }

  [Account(BqlTable = typeof (ARTranPostGL))]
  public virtual int? AccountID { get; set; }

  [SubAccount(BqlTable = typeof (ARTranPostGL))]
  public virtual int? SubID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlTable = typeof (ARTranPostGL))]
  [PXUIField(DisplayName = "Application Period")]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true, BqlTable = typeof (ARTranPostGL))]
  public virtual string TranPeriodID { get; set; }

  [PXDBString(15, IsUnicode = true, BqlTable = typeof (ARTranPostGL))]
  [PXUIField]
  [PX.Objects.GL.BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAR>>>), IsMigratedRecordField = typeof (CuryARHistoryTran.isMigratedRecord), BqlTable = typeof (ARTranPostGL))]
  public virtual string BatchNbr { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPostGL))]
  [ARTranPost.type.List]
  public virtual string Type { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPostGL))]
  public virtual string TranType { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPostGL))]
  public virtual string TranRefNbr { get; set; }

  [PXDBInt(BqlTable = typeof (ARTranPostGL))]
  public virtual int? ReferenceID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summary>
  [PXDBBool(BqlTable = typeof (ARTranPostGL))]
  public virtual bool? IsMigratedRecord { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.INVN, ARTranPost.tranClass.CSLN>>, ARTranPostGL.curyDebitARAmt, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.RCSN>>, Minus<ARTranPostGL.curyCreditARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.CSLR, ARTranPost.tranClass.RCSR>>, Minus<ARTranPostGL.curyTurnDiscAmt>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdSales { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.CSLN>>, ARTranPostGL.curyDebitARAmt, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.RCSN>>, Minus<ARTranPostGL.curyCreditARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.CRMU>>, BqlOperand<ARTranPostGL.curyDebitARAmt, IBqlDecimal>.Subtract<ARTranPostGL.curyCreditARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.PMTU, ARTranPost.tranClass.RPMU>>, BqlOperand<ARTranPostGL.curyCreditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.curyDebitARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.RPMN, ARTranPost.tranClass.PMTN, ARTranPost.tranClass.PPMN, ARTranPost.tranClass.REFN, ARTranPost.tranClass.VRFN, ARTranPost.tranClass.PPIN>>, BqlOperand<ARTranPostGL.curyCreditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.curyDebitARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.PPMB, ARTranPost.tranClass.SMCB>>, BqlOperand<ARTranPostGL.curyDebitARAmt, IBqlDecimal>.Subtract<ARTranPostGL.curyCreditARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.RPMP, ARTranPost.tranClass.PMTP, ARTranPost.tranClass.REFP, ARTranPost.tranClass.VRFP>>, BqlOperand<ARTranPostGL.curyCreditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.curyDebitARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.RPMR, ARTranPost.tranClass.PMTR, ARTranPost.tranClass.PPMR, ARTranPost.tranClass.REFR, ARTranPost.tranClass.VRFR>>, BqlOperand<ARTranPostGL.curyTurnWOAmt, IBqlDecimal>.Add<ARTranPostGL.curyTurnDiscAmt>>>>>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdPayments { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.DRMN, ARTranPost.tranClass.SMCN, ARTranPost.tranClass.SMCB>>, BqlOperand<ARTranPostGL.curyDebitARAmt, IBqlDecimal>.Subtract<ARTranPostGL.curyCreditARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.PPMU, ARTranPost.tranClass.PMTU, ARTranPost.tranClass.RPMU, ARTranPost.tranClass.SMCU>>, BqlOperand<ARTranPostGL.curyCreditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.curyDebitARAmt>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdDrAdjustments { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPostGL.isMigratedRecord, Equal<True>>>>>.And<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.INVN>>>, ARTranPostGL.curyCreditARAmt, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.CRMN, ARTranPost.tranClass.CRMP, ARTranPost.tranClass.SMBN, ARTranPost.tranClass.CRMU>>, BqlOperand<ARTranPostGL.curyCreditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.curyDebitARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.RPMR, ARTranPost.tranClass.PMTR, ARTranPost.tranClass.REFR, ARTranPost.tranClass.VRFR, ARTranPost.tranClass.PPMR>>, Minus<ARTranPostGL.curyTurnWOAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.SMBR, ARTranPost.tranClass.CRMR>>, ARTranPostGL.curyTurnDiscAmt>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdCrAdjustments { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.rgol>>, Minus<ARTranPostGL.curyTurnDiscAmt>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdDiscounts { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (ARTranPostGL.turnItemDiscAmt), typeof (Decimal))]
  public virtual Decimal? CuryPtdItemDiscounts { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.FCHN>>, BqlOperand<ARTranPostGL.curyDebitARAmt, IBqlDecimal>.Subtract<ARTranPostGL.curyCreditARAmt>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdFinCharges { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.PPMB>>, BqlOperand<short2, IBqlShort>.Multiply<BqlOperand<ARTranPostGL.creditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.debitARAmt>>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.PPMP, ARTranPost.tranClass.PPMU, ARTranPost.tranClass.REFU, ARTranPost.tranClass.VRFU, ARTranPost.tranClass.SMCU, ARTranPost.tranClass.CRMU, ARTranPost.tranClass.SMCB>>, BqlOperand<ARTranPostGL.curyCreditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.curyDebitARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.CRMY, ARTranPost.tranClass.PPIP, ARTranPost.tranClass.PPIY>>, BqlOperand<ARTranPostGL.curyCreditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.curyDebitARAmt>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdDeposits { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPostGL.tranClass, In3<ARTranPost.tranClass.INVN, ARTranPost.tranClass.CSLN, ARTranPost.tranClass.CRMN, ARTranPost.tranClass.DRMN>>>>>.And<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.origin>>>, ARTranPostGL.turnRetainageAmt>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdRetainageWithheld { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.retainage>>, Minus<ARTranPostGL.turnRetainageAmt>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdRetainageReleased { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.INVN, ARTranPost.tranClass.CSLN>>, ARTranPostGL.debitARAmt, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.RCSN>>, Minus<ARTranPostGL.creditARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.CSLR, ARTranPost.tranClass.RCSR>>, Minus<ARTranPostGL.turnDiscAmt>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdSales { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.CSLN>>, ARTranPostGL.debitARAmt, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.RCSN>>, Minus<ARTranPostGL.creditARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.CRMU>>, BqlOperand<ARTranPostGL.debitARAmt, IBqlDecimal>.Subtract<ARTranPostGL.creditARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.PMTU, ARTranPost.tranClass.RPMU>>, BqlOperand<ARTranPostGL.creditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.debitARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.RPMN, ARTranPost.tranClass.PMTN, ARTranPost.tranClass.PPMN, ARTranPost.tranClass.REFN, ARTranPost.tranClass.VRFN, ARTranPost.tranClass.PPIN>>, BqlOperand<ARTranPostGL.creditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.debitARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.PPMB, ARTranPost.tranClass.SMCB>>, BqlOperand<ARTranPostGL.debitARAmt, IBqlDecimal>.Subtract<ARTranPostGL.creditARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.RPMP, ARTranPost.tranClass.PMTP, ARTranPost.tranClass.REFP, ARTranPost.tranClass.VRFP>>, BqlOperand<ARTranPostGL.creditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.debitARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.RPMR, ARTranPost.tranClass.PMTR, ARTranPost.tranClass.PPMR, ARTranPost.tranClass.REFR, ARTranPost.tranClass.VRFR, ARTranPost.tranClass.PMTX>>, BqlFunction<Add<ARTranPostGL.turnWOAmt, ARTranPostGL.turnDiscAmt>, IBqlDecimal>.Subtract<ARTranPostGL.rGOLAmt>>>>>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdPayments { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.DRMN, ARTranPost.tranClass.SMCN, ARTranPost.tranClass.SMCB>>, BqlOperand<ARTranPostGL.debitARAmt, IBqlDecimal>.Subtract<ARTranPostGL.creditARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.PPMU, ARTranPost.tranClass.PMTU, ARTranPost.tranClass.RPMU, ARTranPost.tranClass.SMCU>>, BqlOperand<ARTranPostGL.creditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.debitARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.PMTX>>, BqlFunction<Add<ARTranPostGL.turnWOAmt, ARTranPostGL.turnDiscAmt>, IBqlDecimal>.Subtract<ARTranPostGL.rGOLAmt>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdDrAdjustments { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPostGL.isMigratedRecord, Equal<True>>>>>.And<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.INVN>>>, ARTranPostGL.creditARAmt, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.CRMN, ARTranPost.tranClass.CRMP, ARTranPost.tranClass.SMBN, ARTranPost.tranClass.CRMU>>, BqlOperand<ARTranPostGL.creditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.debitARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.RPMR, ARTranPost.tranClass.PMTR, ARTranPost.tranClass.REFR, ARTranPost.tranClass.VRFR, ARTranPost.tranClass.PPMR>>, Minus<ARTranPostGL.turnWOAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.SMBR, ARTranPost.tranClass.CRMR>>, BqlOperand<ARTranPostGL.turnDiscAmt, IBqlDecimal>.Subtract<ARTranPostGL.rGOLAmt>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdCrAdjustments { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.rgol>>, Minus<ARTranPostGL.turnDiscAmt>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdDiscounts { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (ARTranPostGL.turnItemDiscAmt), typeof (Decimal))]
  public virtual Decimal? PtdItemDiscounts { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (ARTranPostGL.turnRGOLAmt), typeof (Decimal))]
  public virtual Decimal? PtdRGOL { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.FCHN>>, BqlOperand<ARTranPostGL.debitARAmt, IBqlDecimal>.Subtract<ARTranPostGL.creditARAmt>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdFinCharges { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsEqual<ARTranPost.tranClass.PPMB>>, BqlOperand<short2, IBqlShort>.Multiply<BqlOperand<ARTranPostGL.creditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.debitARAmt>>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.PPMP, ARTranPost.tranClass.PPMU, ARTranPost.tranClass.REFU, ARTranPost.tranClass.VRFU, ARTranPost.tranClass.SMCU, ARTranPost.tranClass.CRMU, ARTranPost.tranClass.SMCB>>, BqlOperand<ARTranPostGL.creditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.debitARAmt>, Case<Where<BqlOperand<ARTranPostGL.tranClass, IBqlString>.IsIn<ARTranPost.tranClass.CRMY, ARTranPost.tranClass.PPIP, ARTranPost.tranClass.PPIY>>, BqlOperand<ARTranPostGL.creditARAmt, IBqlDecimal>.Subtract<ARTranPostGL.debitARAmt>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdDeposits { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPostGL.tranClass, In3<ARTranPost.tranClass.INVN, ARTranPost.tranClass.CSLN, ARTranPost.tranClass.CRMN, ARTranPost.tranClass.DRMN>>>>>.And<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.origin>>>, ARTranPostGL.turnRetainageAmt>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdRetainageWithheld { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.retainage>>, Minus<ARTranPostGL.turnRetainageAmt>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdRetainageReleased { get; set; }

  public class PK : 
    PrimaryKeyOf<ARTran>.By<CuryARHistoryTran.docType, CuryARHistoryTran.refNbr, CuryARHistoryTran.lineNbr, CuryARHistoryTran.iD>
  {
    public static ARTran Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      int? id,
      PKFindOptions options = 0)
    {
      return (ARTran) PrimaryKeyOf<ARTran>.By<CuryARHistoryTran.docType, CuryARHistoryTran.refNbr, CuryARHistoryTran.lineNbr, CuryARHistoryTran.iD>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, (object) id, options);
    }
  }

  public static class FK
  {
    public class Document : 
      PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>.ForeignKeyOf<ARTran>.By<CuryARHistoryTran.docType, CuryARHistoryTran.refNbr>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<ARInvoice>.By<ARInvoice.docType, ARInvoice.refNbr>.ForeignKeyOf<ARTran>.By<CuryARHistoryTran.docType, CuryARHistoryTran.refNbr>
    {
    }

    public class Payment : 
      PrimaryKeyOf<ARPayment>.By<ARPayment.docType, ARPayment.refNbr>.ForeignKeyOf<ARTran>.By<CuryARHistoryTran.docType, CuryARHistoryTran.refNbr>
    {
    }

    public class CashSale : 
      PrimaryKeyOf<ARCashSale>.By<ARCashSale.docType, ARCashSale.refNbr>.ForeignKeyOf<ARTran>.By<CuryARHistoryTran.docType, CuryARHistoryTran.refNbr>
    {
    }

    public class SOInvoice : 
      PrimaryKeyOf<PX.Objects.SO.SOInvoice>.By<PX.Objects.SO.SOInvoice.docType, PX.Objects.SO.SOInvoice.refNbr>.ForeignKeyOf<ARTran>.By<CuryARHistoryTran.docType, CuryARHistoryTran.refNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARTran>.By<CuryARHistoryTran.branchID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARTran>.By<CuryARHistoryTran.curyInfoID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARTran>.By<CuryARHistoryTran.customerID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARTran>.By<CuryARHistoryTran.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARTran>.By<CuryARHistoryTran.subID>
    {
    }
  }

  public abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryTran.iD>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryARHistoryTran.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryARHistoryTran.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryTran.lineNbr>
  {
  }

  public abstract class sourceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryARHistoryTran.sourceDocType>
  {
  }

  public abstract class sourceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryARHistoryTran.sourceRefNbr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CuryARHistoryTran.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CuryARHistoryTran.curyInfoID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryTran.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryTran.customerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryTran.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryTran.subID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryARHistoryTran.finPeriodID>
  {
  }

  public abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryARHistoryTran.tranPeriodID>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryARHistoryTran.batchNbr>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryARHistoryTran.type>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryARHistoryTran.tranType>
  {
  }

  public abstract class tranRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryARHistoryTran.tranRefNbr>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryTran.referenceID>
  {
  }

  public abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CuryARHistoryTran.isMigratedRecord>
  {
  }

  public abstract class curyPtdSales : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.curyPtdSales>
  {
  }

  public abstract class curyPtdPayments : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.curyPtdPayments>
  {
  }

  public abstract class curyPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.curyPtdDrAdjustments>
  {
  }

  public abstract class curyPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.curyPtdCrAdjustments>
  {
  }

  public abstract class curyPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.curyPtdDiscounts>
  {
  }

  public abstract class curyPtdItemDiscounts : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.curyPtdItemDiscounts>
  {
  }

  public abstract class curyPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.curyPtdFinCharges>
  {
  }

  public abstract class curyPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.curyPtdDeposits>
  {
  }

  public abstract class curyPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.curyPtdRetainageWithheld>
  {
  }

  public abstract class curyPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.curyPtdRetainageReleased>
  {
  }

  public abstract class ptdSales : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CuryARHistoryTran.ptdSales>
  {
  }

  public abstract class ptdPayments : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CuryARHistoryTran.ptdPayments>
  {
  }

  public abstract class ptdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.ptdDrAdjustments>
  {
  }

  public abstract class ptdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.ptdCrAdjustments>
  {
  }

  public abstract class ptdDiscounts : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.ptdDiscounts>
  {
  }

  public abstract class ptdItemDiscounts : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.ptdItemDiscounts>
  {
  }

  public abstract class ptdRGOL : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CuryARHistoryTran.ptdRGOL>
  {
  }

  public abstract class ptdFinCharges : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.ptdFinCharges>
  {
  }

  public abstract class ptdDeposits : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CuryARHistoryTran.ptdDeposits>
  {
  }

  public abstract class ptdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.ptdRetainageWithheld>
  {
  }

  public abstract class ptdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryARHistoryTran.ptdRetainageReleased>
  {
  }
}
