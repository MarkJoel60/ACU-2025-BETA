// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.APPayment
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
public class APPayment : PX.Objects.AP.APRegister
{
  [PXDBString(40, IsUnicode = true)]
  public virtual 
  #nullable disable
  string ExtRefNbr { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string PaymentMethodID { get; set; }

  [PXDBInt]
  public virtual int? CashAccountID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CA.Light.BAccount" /> of the Vendor.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.Light.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt]
  public override int? VendorID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CA.Light.Location" /> of the Vendor.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.Light.Location.LocationID" /> field.
  /// </value>
  [PXDBInt]
  public override int? VendorLocationID { get; set; }

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

  public new class PK : PrimaryKeyOf<APPayment>.By<APPayment.docType, APPayment.refNbr>
  {
    public static APPayment Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (APPayment) PrimaryKeyOf<APPayment>.By<APPayment.docType, APPayment.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.extRefNbr>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.refNbr>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.branchID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.paymentMethodID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.cashAccountID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.vendorID>
  {
  }

  public new abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APPayment.vendorLocationID>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.batchNbr>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.released>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.openDoc>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.hold>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.voided>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APPayment.cATranID>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPayment.curyOrigDocAmt>
  {
  }

  public abstract class depositAsBatch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.depositAsBatch>
  {
  }

  public abstract class depositAfter : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  APPayment.depositAfter>
  {
  }

  public abstract class deposited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.deposited>
  {
  }

  public abstract class depositType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.depositType>
  {
  }

  public abstract class depositNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.depositNbr>
  {
  }
}
