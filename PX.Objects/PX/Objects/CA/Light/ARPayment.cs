// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.ARPayment
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

[PXTable]
[Serializable]
public class ARPayment : PX.Objects.AR.ARRegister
{
  [PXDBString(40, IsUnicode = true)]
  public virtual 
  #nullable disable
  string ExtRefNbr { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CA.Light.BAccount" /> of the Customer.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.Light.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt]
  public override int? CustomerID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CA.Light.Location" /> of the Customer.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.Light.Location.LocationID" /> field.
  /// </value>
  [PXDBInt]
  public override int? CustomerLocationID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string PaymentMethodID { get; set; }

  [PXDBInt]
  public virtual int? PMInstanceID { get; set; }

  [PXDBInt]
  public virtual int? CashAccountID { get; set; }

  [PXDBLong]
  public virtual long? CATranID { get; set; }

  [PXDBBool]
  public virtual bool? DepositAsBatch { get; set; }

  [PXDBDate]
  public virtual DateTime? DepositAfter { get; set; }

  [PXDBBool]
  public virtual bool? Deposited { get; set; }

  [PXDBString(3, IsFixed = true)]
  public virtual string DepositType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string DepositNbr { get; set; }

  public new class PK : PrimaryKeyOf<ARPayment>.By<ARPayment.docType, ARPayment.refNbr>
  {
    public static ARPayment Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ARPayment) PrimaryKeyOf<ARPayment>.By<ARPayment.docType, ARPayment.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.extRefNbr>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.refNbr>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.customerID>
  {
  }

  public new abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARPayment.customerLocationID>
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

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.pMInstanceID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.cashAccountID>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.batchNbr>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.voided>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.released>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARPayment.cATranID>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPayment.curyOrigDocAmt>
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
}
