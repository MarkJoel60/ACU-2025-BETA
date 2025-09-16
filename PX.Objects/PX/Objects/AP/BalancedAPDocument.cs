// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.BalancedAPDocument
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CM.Extensions;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select2<APRegister, LeftJoin<APInvoice, On<APInvoice.docType, Equal<APRegister.docType>, And<APInvoice.refNbr, Equal<APRegister.refNbr>>>, LeftJoin<APPayment, On<APPayment.docType, Equal<APRegister.docType>, And<APPayment.refNbr, Equal<APRegister.refNbr>>>>>>))]
[Serializable]
public class BalancedAPDocument : APRegister
{
  [PXDBString(40, IsUnicode = true, BqlField = typeof (APInvoice.invoiceNbr))]
  public virtual 
  #nullable disable
  string InvoiceNbr { get; set; }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (APPayment.extRefNbr))]
  public virtual string ExtRefNbr { get; set; }

  [PXString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Vendor Ref.")]
  [PXFormula(typeof (IsNull<BalancedAPDocument.invoiceNbr, BalancedAPDocument.extRefNbr>))]
  public string VendorRefNbr { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APRegister.docType))]
  [PXDefault]
  [APDocType.DocumentReleaseList]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
  [PXFieldDescription]
  public override string DocType { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that a check must be printed for the payment represented by this record.
  /// </summary>
  [PXDBBool(BqlField = typeof (APPayment.printCheck))]
  public virtual bool? PrintCheck { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.origDocAmt))]
  [PXUIField(DisplayName = "Currency Amount", Visibility = PXUIVisibility.SelectorVisible)]
  public override Decimal? CuryOrigDocAmt { get; set; }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BalancedAPDocument.invoiceNbr>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BalancedAPDocument.extRefNbr>
  {
  }

  public abstract class vendorRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BalancedAPDocument.vendorRefNbr>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BalancedAPDocument.docType>
  {
  }

  public abstract class printCheck : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedAPDocument.printCheck>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BalancedAPDocument.curyOrigDocAmt>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BalancedAPDocument.branchID>
  {
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedAPDocument.selected>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BalancedAPDocument.refNbr>
  {
  }

  public new abstract class origModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BalancedAPDocument.origModule>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedAPDocument.openDoc>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedAPDocument.released>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedAPDocument.hold>
  {
  }

  public new abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedAPDocument.scheduled>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedAPDocument.voided>
  {
  }

  public new abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedAPDocument.printed>
  {
  }

  public new abstract class prebooked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedAPDocument.prebooked>
  {
  }

  public new abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedAPDocument.approved>
  {
  }

  public new abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    BalancedAPDocument.createdByID>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    BalancedAPDocument.lastModifiedByID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BalancedAPDocument.noteID>
  {
  }

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BalancedAPDocument.hasMultipleProjects>
  {
  }
}
