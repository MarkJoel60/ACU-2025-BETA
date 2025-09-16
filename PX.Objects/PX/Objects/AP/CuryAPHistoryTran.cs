// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.CuryAPHistoryTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select<APTranPostGL>), Persistent = false)]
[PXCacheName("AP History Transaction")]
public class CuryAPHistoryTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlTable = typeof (APTranPostGL))]
  public virtual int? ID { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (APTranPostGL))]
  [PXUIField(DisplayName = "Doc. Type")]
  [APDocType.List]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (APTranPostGL))]
  [PXUIField(DisplayName = "Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string RefNbr { get; set; }

  [PXDBInt(BqlTable = typeof (APTranPostGL))]
  [PXUIField(DisplayName = "Line Nbr.")]
  public virtual int? LineNbr { get; set; }

  [PXDBString(BqlTable = typeof (APTranPostGL))]
  [PXUIField(DisplayName = "Source Doc. Type")]
  [APDocType.List]
  public virtual string SourceDocType { get; set; }

  [PXDBString(BqlTable = typeof (APTranPostGL))]
  [PXUIField(DisplayName = "Source Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string SourceRefNbr { get; set; }

  [PXDBLong(BqlTable = typeof (APTranPostGL))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBString(BqlTable = typeof (APTranPostGL))]
  public virtual string CuryID { get; set; }

  [PXUIField(DisplayName = "Branch")]
  [Branch(null, null, true, true, true, BqlTable = typeof (APTranPostGL))]
  public virtual int? BranchID { get; set; }

  [Vendor(BqlTable = typeof (APTranPostGL))]
  public virtual int? VendorID { get; set; }

  [Account(BqlTable = typeof (APTranPostGL))]
  public virtual int? AccountID { get; set; }

  [SubAccount(BqlTable = typeof (APTranPostGL))]
  public virtual int? SubID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlTable = typeof (APTranPostGL))]
  [PXUIField(DisplayName = "Application Period")]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true, BqlTable = typeof (APTranPostGL))]
  public virtual string TranPeriodID { get; set; }

  [PXDBString(15, IsUnicode = true, BqlTable = typeof (APTranPostGL))]
  [PXUIField(DisplayName = "Batch Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PX.Objects.GL.BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>), IsMigratedRecordField = typeof (CuryAPHistoryTran.isMigratedRecord), BqlTable = typeof (APTranPostGL))]
  public virtual string BatchNbr { get; set; }

  [PXDBString(BqlTable = typeof (APTranPostGL))]
  [APTranPost.type.List]
  public virtual string Type { get; set; }

  [PXDBString(BqlTable = typeof (APTranPostGL))]
  public virtual string TranType { get; set; }

  [PXDBString(BqlTable = typeof (APTranPostGL))]
  public virtual string TranRefNbr { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summary>
  [PXDBBool(BqlTable = typeof (APTranPostGL))]
  public virtual bool? IsMigratedRecord { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.INVN>>, APTranPostGL.curyCreditAPAmt, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.QCKN>>, APTranPostGL.curyDebitAPAmt, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.VQCN, APTranPost.tranClass.RQCN>>, PX.Data.BQL.Minus<APTranPostGL.curyDebitAPAmt>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdPurchases { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.QCKN>>, APTranPostGL.curyDebitAPAmt, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.VQCN, APTranPost.tranClass.RQCN>>, PX.Data.BQL.Minus<APTranPostGL.curyDebitAPAmt>, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.VCKN, APTranPost.tranClass.CHKN, APTranPost.tranClass.PPMN, APTranPost.tranClass.REFN, APTranPost.tranClass.VRFN>>, BqlOperand<APTranPostGL.curyDebitAPAmt, IBqlDecimal>.Subtract<APTranPostGL.curyCreditAPAmt>, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.VCKP, APTranPost.tranClass.CHKP, APTranPost.tranClass.REFP, APTranPost.tranClass.VRFP, APTranPost.tranClass.ADRU>>, BqlOperand<APTranPostGL.curyDebitAPAmt, IBqlDecimal>.Subtract<APTranPostGL.curyCreditAPAmt>, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.CHKR, APTranPost.tranClass.PPMR, APTranPost.tranClass.VCKR, APTranPost.tranClass.VQCR, APTranPost.tranClass.QCKR, APTranPost.tranClass.VRFR, APTranPost.tranClass.RQCR>>, PX.Data.BQL.Minus<BqlOperand<APTranPostGL.curyTurnWhTaxAmt, IBqlDecimal>.Add<APTranPostGL.curyTurnDiscAmt>>>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdPayments { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPostGL.isMigratedRecord, Equal<True>>>>>.And<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.INVN>>>, APTranPostGL.curyDebitAPAmt, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.ADRU>>, BqlOperand<APTranPostGL.curyCreditAPAmt, IBqlDecimal>.Subtract<APTranPostGL.curyDebitAPAmt>, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.ADRP, APTranPost.tranClass.ADRN>>, BqlOperand<APTranPostGL.curyDebitAPAmt, IBqlDecimal>.Subtract<APTranPostGL.curyCreditAPAmt>, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.ADRR>>, PX.Data.BQL.Minus<BqlOperand<APTranPostGL.curyTurnWhTaxAmt, IBqlDecimal>.Add<APTranPostGL.curyTurnDiscAmt>>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdDrAdjustments { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.ACRN>>, BqlOperand<APTranPostGL.curyCreditAPAmt, IBqlDecimal>.Subtract<APTranPostGL.curyDebitAPAmt>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdCrAdjustments { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (APTranPostGL.curyTurnDiscAmt), typeof (Decimal))]
  public virtual Decimal? CuryPtdDiscTaken { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (APTranPostGL.curyTurnWhTaxAmt), typeof (Decimal))]
  public virtual Decimal? CuryPtdWhTax { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.ADRU>>, BqlOperand<APTranPostGL.curyCreditAPAmt, IBqlDecimal>.Subtract<APTranPostGL.curyDebitAPAmt>, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.PPMP, APTranPost.tranClass.PPMU, APTranPost.tranClass.CHKU, APTranPost.tranClass.VCKU, APTranPost.tranClass.REFU, APTranPost.tranClass.VRFU>>, BqlOperand<APTranPostGL.curyDebitAPAmt, IBqlDecimal>.Subtract<APTranPostGL.curyCreditAPAmt>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdDeposits { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPostGL.tranClass, In3<APTranPost.tranClass.INVN, APTranPost.tranClass.QCKN, APTranPost.tranClass.ADRN>>>>>.And<BqlOperand<APTranPostGL.type, IBqlString>.IsEqual<APTranPost.type.origin>>>, APTranPostGL.curyTurnRetainageAmt>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdRetainageWithheld { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPostGL.type, IBqlString>.IsEqual<APTranPost.type.retainage>>, BqlFunction<PX.Data.Minus<APTranPostGL.curyTurnRetainageAmt>, IBqlDecimal>.Multiply<APTranPost.glSign>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryPtdRetainageReleased { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.INVN>>, APTranPostGL.creditAPAmt, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.QCKN>>, APTranPostGL.debitAPAmt, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.VQCN, APTranPost.tranClass.RQCN>>, PX.Data.BQL.Minus<APTranPostGL.debitAPAmt>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdPurchases { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.QCKN>>, APTranPostGL.debitAPAmt, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.VQCN, APTranPost.tranClass.RQCN>>, PX.Data.BQL.Minus<APTranPostGL.debitAPAmt>, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.VCKN, APTranPost.tranClass.CHKN, APTranPost.tranClass.PPMN, APTranPost.tranClass.REFN, APTranPost.tranClass.VRFN>>, BqlOperand<APTranPostGL.debitAPAmt, IBqlDecimal>.Subtract<APTranPostGL.creditAPAmt>, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.VCKP, APTranPost.tranClass.CHKP, APTranPost.tranClass.REFP, APTranPost.tranClass.VRFP, APTranPost.tranClass.ADRU>>, BqlOperand<APTranPostGL.debitAPAmt, IBqlDecimal>.Subtract<APTranPostGL.creditAPAmt>, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.REFR, APTranPost.tranClass.VRFR>>, PX.Data.BQL.Minus<APTranPostGL.turnRGOLAmt>, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.CHKR, APTranPost.tranClass.PPMR, APTranPost.tranClass.VCKR, APTranPost.tranClass.VQCR, APTranPost.tranClass.QCKR, APTranPost.tranClass.VRFR>>, PX.Data.BQL.Minus<BqlFunction<Add<APTranPostGL.turnWhTaxAmt, APTranPostGL.turnDiscAmt>, IBqlDecimal>.Add<APTranPostGL.rGOLAmt>>>>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdPayments { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPostGL.isMigratedRecord, Equal<True>>>>>.And<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.INVN>>>, APTranPostGL.debitAPAmt, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.ADRU>>, BqlOperand<APTranPostGL.creditAPAmt, IBqlDecimal>.Subtract<APTranPostGL.debitAPAmt>, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.ADRP, APTranPost.tranClass.ADRN>>, BqlOperand<APTranPostGL.debitAPAmt, IBqlDecimal>.Subtract<APTranPostGL.creditAPAmt>, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.ADRR>>, PX.Data.BQL.Minus<BqlFunction<Add<APTranPostGL.turnWhTaxAmt, APTranPostGL.turnDiscAmt>, IBqlDecimal>.Add<APTranPostGL.rGOLAmt>>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdDrAdjustments { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.ACRN>>, BqlOperand<APTranPostGL.creditAPAmt, IBqlDecimal>.Subtract<APTranPostGL.debitAPAmt>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdCrAdjustments { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (APTranPostGL.turnDiscAmt), typeof (Decimal))]
  public virtual Decimal? PtdDiscTaken { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (APTranPostGL.turnWhTaxAmt), typeof (Decimal))]
  public virtual Decimal? PtdWhTax { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (APTranPostGL.turnRGOLAmt), typeof (Decimal))]
  public virtual Decimal? PtdRGOL { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.ADRU>>, BqlOperand<APTranPostGL.creditAPAmt, IBqlDecimal>.Subtract<APTranPostGL.debitAPAmt>, Case<Where<BqlOperand<APTranPostGL.tranClass, IBqlString>.IsIn<APTranPost.tranClass.PPMP, APTranPost.tranClass.PPMU, APTranPost.tranClass.CHKU, APTranPost.tranClass.VCKU, APTranPost.tranClass.REFU, APTranPost.tranClass.VRFU>>, BqlOperand<APTranPostGL.debitAPAmt, IBqlDecimal>.Subtract<APTranPostGL.creditAPAmt>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdDeposits { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPostGL.tranClass, In3<APTranPost.tranClass.INVN, APTranPost.tranClass.QCKN, APTranPost.tranClass.ADRN>>>>>.And<BqlOperand<APTranPostGL.type, IBqlString>.IsEqual<APTranPost.type.origin>>>, APTranPostGL.turnRetainageAmt>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdRetainageWithheld { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPostGL.type, IBqlString>.IsEqual<APTranPost.type.retainage>>, BqlFunction<PX.Data.Minus<APTranPostGL.turnRetainageAmt>, IBqlDecimal>.Multiply<APTranPost.glSign>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? PtdRetainageReleased { get; set; }

  public class PK : 
    PrimaryKeyOf<CuryAPHistoryTran>.By<CuryAPHistoryTran.docType, CuryAPHistoryTran.refNbr, CuryAPHistoryTran.lineNbr, CuryAPHistoryTran.iD>
  {
    public static CuryAPHistoryTran Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      int? id,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<CuryAPHistoryTran>.By<CuryAPHistoryTran.docType, CuryAPHistoryTran.refNbr, CuryAPHistoryTran.lineNbr, CuryAPHistoryTran.iD>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, (object) id, options);
    }
  }

  public static class FK
  {
    public class Document : 
      PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<APTran>.By<CuryAPHistoryTran.docType, CuryAPHistoryTran.refNbr>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<APInvoice>.By<APInvoice.docType, APInvoice.refNbr>.ForeignKeyOf<APTran>.By<CuryAPHistoryTran.docType, CuryAPHistoryTran.refNbr>
    {
    }

    public class Payment : 
      PrimaryKeyOf<APPayment>.By<APPayment.docType, APPayment.refNbr>.ForeignKeyOf<APTran>.By<CuryAPHistoryTran.docType, CuryAPHistoryTran.refNbr>
    {
    }

    public class CashSale : 
      PrimaryKeyOf<APQuickCheck>.By<APQuickCheck.docType, APQuickCheck.refNbr>.ForeignKeyOf<APTran>.By<CuryAPHistoryTran.docType, CuryAPHistoryTran.refNbr>
    {
    }

    public class SOInvoice : 
      PrimaryKeyOf<PX.Objects.SO.SOInvoice>.By<PX.Objects.SO.SOInvoice.docType, PX.Objects.SO.SOInvoice.refNbr>.ForeignKeyOf<APTran>.By<CuryAPHistoryTran.docType, CuryAPHistoryTran.refNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APTran>.By<CuryAPHistoryTran.branchID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APTran>.By<CuryAPHistoryTran.curyInfoID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APTran>.By<CuryAPHistoryTran.vendorID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APTran>.By<CuryAPHistoryTran.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<APTran>.By<CuryAPHistoryTran.subID>
    {
    }
  }

  public abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistoryTran.iD>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryAPHistoryTran.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryAPHistoryTran.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistoryTran.lineNbr>
  {
  }

  public abstract class sourceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryAPHistoryTran.sourceDocType>
  {
  }

  public abstract class sourceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryAPHistoryTran.sourceRefNbr>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CuryAPHistoryTran.curyInfoID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CuryAPHistoryTran.curyID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistoryTran.branchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistoryTran.vendorID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistoryTran.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistoryTran.subID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryAPHistoryTran.finPeriodID>
  {
  }

  public abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryAPHistoryTran.tranPeriodID>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryAPHistoryTran.batchNbr>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryAPHistoryTran.type>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryAPHistoryTran.tranType>
  {
  }

  public abstract class tranRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryAPHistoryTran.tranRefNbr>
  {
  }

  public abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CuryAPHistoryTran.isMigratedRecord>
  {
  }

  public abstract class curyPtdPurchases : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.curyPtdPurchases>
  {
  }

  public abstract class curyPtdPayments : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.curyPtdPayments>
  {
  }

  public abstract class curyPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.curyPtdDrAdjustments>
  {
  }

  public abstract class curyPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.curyPtdCrAdjustments>
  {
  }

  public abstract class curyPtdDiscTaken : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.curyPtdDiscTaken>
  {
  }

  public abstract class curyPtdWhTax : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.curyPtdWhTax>
  {
  }

  public abstract class curyPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.curyPtdDeposits>
  {
  }

  public abstract class curyPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.curyPtdRetainageWithheld>
  {
  }

  public abstract class curyPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.curyPtdRetainageReleased>
  {
  }

  public abstract class ptdPurchases : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.ptdPurchases>
  {
  }

  public abstract class ptdPayments : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CuryAPHistoryTran.ptdPayments>
  {
  }

  public abstract class ptdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.ptdDrAdjustments>
  {
  }

  public abstract class ptdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.ptdCrAdjustments>
  {
  }

  public abstract class ptdDiscTaken : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.ptdDiscTaken>
  {
  }

  public abstract class ptdWhTax : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CuryAPHistoryTran.ptdWhTax>
  {
  }

  public abstract class ptdRGOL : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CuryAPHistoryTran.ptdRGOL>
  {
  }

  public abstract class ptdDeposits : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CuryAPHistoryTran.ptdDeposits>
  {
  }

  public abstract class ptdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.ptdRetainageWithheld>
  {
  }

  public abstract class ptdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CuryAPHistoryTran.ptdRetainageReleased>
  {
  }
}
