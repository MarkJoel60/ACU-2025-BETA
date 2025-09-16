// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.ARRegister
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CA.Light;

[Serializable]
public class ARRegister : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  public virtual int? BranchID { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  public virtual string RefNbr { get; set; }

  [PXDBDate]
  public virtual DateTime? DocDate { get; set; }

  [PXDBDate]
  public virtual DateTime? DueDate { get; set; }

  [PXDBString]
  public virtual string FinPeriodID { get; set; }

  [PXDBInt]
  public virtual int? CustomerID { get; set; }

  /// <summary>Customer location ID</summary>
  [PXDBInt]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBString(5, IsUnicode = true)]
  public virtual string CuryID { get; set; }

  [PXDBLong]
  public virtual long? CuryInfoID { get; set; }

  [PXDBDecimal]
  public virtual Decimal? CuryDocBal { get; set; }

  [PXDBDecimal]
  public virtual Decimal? DocBal { get; set; }

  /// <summary>
  /// The cash discount balance of the document.
  /// Given in the <see cref="P:PX.Objects.CA.Light.ARRegister.CuryID">currency of the document</see>.
  /// </summary>
  [PXDBDecimal]
  public virtual Decimal? CuryDiscBal { get; set; }

  /// <summary>
  /// The cash discount balance of the document.
  /// Given in the <see cref="!:Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBDecimal]
  public virtual Decimal? DiscBal { get; set; }

  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  public virtual string DocDesc { get; set; }

  [PXDBBool]
  public virtual bool? Released { get; set; }

  [PXDBBool]
  public virtual bool? OpenDoc { get; set; }

  [PXDBBool]
  public virtual bool? Voided { get; set; }

  [PXDBBool]
  public virtual bool? PaymentsByLinesAllowed { get; set; }

  [PXDBBool]
  public virtual bool? Scheduled { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string ScheduleID { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the prepayment ready for payment applicaton
  /// when the feature <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAR" /> is activated.
  /// </summary>
  [PXDBBool]
  public virtual bool? PendingPayment { get; set; }

  public class PK : PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>
  {
    public static ARRegister Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ARRegister) PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.branchID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.refNbr>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARRegister.docDate>
  {
  }

  public abstract class dueDate : IBqlField, IBqlOperand
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.finPeriodID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegister.customerLocationID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARRegister.curyInfoID>
  {
  }

  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.docBal>
  {
  }

  public abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.curyDiscBal>
  {
  }

  public abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.discBal>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.docDesc>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.released>
  {
  }

  public abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.openDoc>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.voided>
  {
  }

  public abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegister.paymentsByLinesAllowed>
  {
  }

  public abstract class scheduled : IBqlField, IBqlOperand
  {
  }

  public abstract class scheduleID : IBqlField, IBqlOperand
  {
  }

  public abstract class pendingPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.pendingPayment>
  {
  }
}
