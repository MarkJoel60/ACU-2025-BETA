// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.ARInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.CA.Light;

[PXTable]
[Serializable]
public class ARInvoice : ARRegister
{
  [PXDBString(10, IsUnicode = true)]
  public virtual 
  #nullable disable
  string TermsID { get; set; }

  [PXDBString(40, IsUnicode = true)]
  public virtual string InvoiceNbr { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string PaymentMethodID { get; set; }

  [PXDBInt]
  public virtual int? PMInstanceID { get; set; }

  [PXDBInt]
  public virtual int? CashAccountID { get; set; }

  [PXString(1, IsFixed = true)]
  public virtual string DrCr
  {
    [PXDependsOnFields(new Type[] {typeof (ARInvoice.docType)})] get
    {
      return ARInvoiceType.DrCr(this.DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// The date when the cash discount can be taken in accordance with the <see cref="P:PX.Objects.CA.Light.ARInvoice.TermsID">credit terms</see>.
  /// </summary>
  [PXDBDate]
  public virtual DateTime? DiscDate { get; set; }

  public new class PK : PrimaryKeyOf<ARInvoice>.By<ARInvoice.docType, ARInvoice.refNbr>
  {
    public static ARInvoice Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ARInvoice) PrimaryKeyOf<ARInvoice>.By<ARInvoice.docType, ARInvoice.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.refNbr>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.customerID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.curyID>
  {
  }

  public abstract class termsID : IBqlField, IBqlOperand
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.invoiceNbr>
  {
  }

  public new abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARInvoice.docDate>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARInvoice.curyInfoID>
  {
  }

  public new abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyDocBal>
  {
  }

  public new abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.docBal>
  {
  }

  public new abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyDiscBal>
  {
  }

  public new abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.discBal>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.released>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.openDoc>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.voided>
  {
  }

  public new abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.paymentsByLinesAllowed>
  {
  }

  public abstract class paymentMethodID : IBqlField, IBqlOperand
  {
  }

  public abstract class pMInstanceID : IBqlField, IBqlOperand
  {
  }

  public abstract class cashAccountID : IBqlField, IBqlOperand
  {
  }

  public new abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.docDesc>
  {
  }

  public abstract class drCr : IBqlField, IBqlOperand
  {
  }

  public abstract class discDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARInvoice.discDate>
  {
  }

  public abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.origDocType>
  {
  }
}
