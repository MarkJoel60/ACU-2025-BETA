// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Standalone2.ARPayment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.AR.Standalone2;

[PXHidden]
[PXInternalUseOnly]
public class ARPayment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string RefNbr { get; set; }

  [PXDBInt]
  public virtual int? PMInstanceID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDBDefault]
  public virtual string ProcessingCenterID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string PaymentMethodID { get; set; }

  [PXDBInt]
  public virtual int? CashAccountID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  public virtual Decimal? CuryOrigTaxDiscAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  public virtual Decimal? OrigTaxDiscAmt { get; set; }

  [PXDBString(40, IsUnicode = true)]
  public virtual string ExtRefNbr { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? AdjDate { get; set; }

  [PXDBString]
  public virtual string AdjFinPeriodID { get; set; }

  [PXDBString]
  public virtual string AdjTranPeriodID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Cleared { get; set; }

  [PXDBDate]
  public virtual DateTime? ClearDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Settled { get; set; }

  [PXDBLong]
  public virtual long? CATranID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DepositAsBatch { get; set; }

  [PXDBDate]
  [PXDefault]
  public virtual DateTime? DepositAfter { get; set; }

  [PXDate]
  public virtual DateTime? DepositDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Deposited { get; set; }

  [PXDBString(3, IsFixed = true)]
  public virtual string DepositType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string DepositNbr { get; set; }

  [ProjectDefault("AR")]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInAR, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase]
  public virtual int? ProjectID { get; set; }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<ARPayment.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ActiveProjectTask(typeof (ARPayment.projectID), "AR", DisplayName = "Project Task")]
  public virtual int? TaskID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ChargeCntr { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryConsolidateChargeTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ConsolidateChargeTotal { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXDefault]
  public virtual string RefTranExtNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCCPayment { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCCAuthorized { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCCCaptured { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCCCaptureFailed { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCCRefunded { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCCUserAttention { get; set; }

  [PXDBInt]
  public virtual int? CCActualExternalTransactionID { get; set; }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.refNbr>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.pMInstanceID>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPayment.processingCenterID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPayment.paymentMethodID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.cashAccountID>
  {
  }

  public abstract class curyOrigTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPayment.curyOrigTaxDiscAmt>
  {
  }

  public abstract class origTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPayment.origTaxDiscAmt>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.extRefNbr>
  {
  }

  public abstract class adjDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARPayment.adjDate>
  {
  }

  public abstract class adjFinPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.adjFinPeriodID>
  {
  }

  public abstract class adjTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPayment.adjTranPeriodID>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARPayment.clearDate>
  {
  }

  public abstract class settled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.settled>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARPayment.cATranID>
  {
  }

  public abstract class depositAsBatch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.depositAsBatch>
  {
  }

  public abstract class depositAfter : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARPayment.depositAfter>
  {
  }

  public abstract class depositDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARPayment.depositDate>
  {
  }

  public abstract class deposited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.deposited>
  {
  }

  public abstract class depositType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.depositType>
  {
  }

  public abstract class depositNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.depositNbr>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.taskID>
  {
  }

  public abstract class chargeCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.chargeCntr>
  {
  }

  public abstract class curyConsolidateChargeTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPayment.curyConsolidateChargeTotal>
  {
  }

  public abstract class consolidateChargeTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPayment.consolidateChargeTotal>
  {
  }

  public abstract class refTranExtNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.refTranExtNbr>
  {
  }

  public abstract class isCCPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.isCCPayment>
  {
  }

  public abstract class isCCAuthorized : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.isCCAuthorized>
  {
  }

  public abstract class isCCCaptured : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.isCCCaptured>
  {
  }

  public abstract class isCCCaptureFailed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPayment.isCCCaptureFailed>
  {
  }

  public abstract class isCCRefunded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.isCCRefunded>
  {
  }

  public abstract class isCCUserAttention : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPayment.isCCUserAttention>
  {
  }

  public abstract class cCActualExternalTransactionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARPayment.cCActualExternalTransactionID>
  {
  }
}
