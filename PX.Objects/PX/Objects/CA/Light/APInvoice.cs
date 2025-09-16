// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.APInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using System;

#nullable enable
namespace PX.Objects.CA.Light;

[PXTable]
[Serializable]
public class APInvoice : APRegister
{
  [PXDBString(40, IsUnicode = true)]
  public virtual 
  #nullable disable
  string InvoiceNbr { get; set; }

  [PXDBInt]
  public virtual int? PayAccountID { get; set; }

  [PXDBDate]
  public virtual DateTime? DueDate { get; set; }

  [PXDBDate]
  public virtual DateTime? PayDate { get; set; }

  [PXString(1, IsFixed = true)]
  public string DrCr
  {
    [PXDependsOnFields(new Type[] {typeof (APInvoice.docType)})] get
    {
      return APInvoiceType.DrCr(this.DocType);
    }
    set
    {
    }
  }

  [PXDBString(10, IsUnicode = true)]
  public virtual string TermsID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string PayTypeID { get; set; }

  /// <summary>
  /// The date when the cash discount can be taken in accordance with the <see cref="P:PX.Objects.CA.Light.APInvoice.TermsID">credit terms</see>.
  /// </summary>
  [PXDBDate]
  public virtual DateTime? DiscDate { get; set; }

  public new class PK : PrimaryKeyOf<APInvoice>.By<APInvoice.docType, APInvoice.refNbr>
  {
    public static APInvoice Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (APInvoice) PrimaryKeyOf<APInvoice>.By<APInvoice.docType, APInvoice.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.refNbr>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.vendorID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.curyID>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.invoiceNbr>
  {
  }

  public new abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  APInvoice.docDate>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APInvoice.curyInfoID>
  {
  }

  public new abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.curyDocBal>
  {
  }

  public new abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.docBal>
  {
  }

  public new abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.curyDiscBal>
  {
  }

  public new abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.discBal>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.released>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.openDoc>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.voided>
  {
  }

  public new abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.docDesc>
  {
  }

  public new abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoice.paymentsByLinesAllowed>
  {
  }

  public abstract class payAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class dueDate : IBqlField, IBqlOperand
  {
  }

  public abstract class payDate : IBqlField, IBqlOperand
  {
  }

  public abstract class drCr : IBqlField, IBqlOperand
  {
  }

  public abstract class termsID : IBqlField, IBqlOperand
  {
  }

  public abstract class payTypeID : IBqlField, IBqlOperand
  {
  }

  public abstract class discDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  APInvoice.discDate>
  {
  }
}
