// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PurchaseTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXHidden]
[Serializable]
public class PurchaseTax : PX.Objects.TX.Tax
{
  protected int? _HistTaxAcctID;
  protected int? _HistTaxSubID;
  protected 
  #nullable disable
  string _TranTaxType;

  [PXDBCalced(typeof (Switch<Case<Where<PurchaseTax.pendingTax, Equal<True>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>>>, PurchaseTax.pendingPurchTaxAcctID, Case<Where<PurchaseTax.pendingTax, Equal<True>, And<PX.Objects.TX.Tax.reverseTax, Equal<True>>>, PurchaseTax.pendingSalesTaxAcctID, Case<Where<PurchaseTax.taxType, Equal<CSTaxType.use>, Or<PurchaseTax.taxType, Equal<CSTaxType.withholding>, Or<PurchaseTax.taxType, Equal<CSTaxType.sales>, Or<PX.Objects.TX.Tax.reverseTax, Equal<True>>>>>, PurchaseTax.salesTaxAcctID>>>, PurchaseTax.purchTaxAcctID>), typeof (int))]
  public virtual int? HistTaxAcctID
  {
    get => this._HistTaxAcctID;
    set => this._HistTaxAcctID = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<PurchaseTax.pendingTax, Equal<True>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>>>, PurchaseTax.pendingPurchTaxSubID, Case<Where<PurchaseTax.pendingTax, Equal<True>, And<PX.Objects.TX.Tax.reverseTax, Equal<True>>>, PurchaseTax.pendingSalesTaxSubID, Case<Where<PurchaseTax.taxType, Equal<CSTaxType.use>, Or<PurchaseTax.taxType, Equal<CSTaxType.withholding>, Or<PurchaseTax.taxType, Equal<CSTaxType.perUnit>, Or<PurchaseTax.taxType, Equal<CSTaxType.sales>, Or<PX.Objects.TX.Tax.reverseTax, Equal<True>>>>>>, PurchaseTax.salesTaxSubID>>>, PurchaseTax.purchTaxSubID>), typeof (int))]
  public virtual int? HistTaxSubID
  {
    get => this._HistTaxSubID;
    set => this._HistTaxSubID = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<PurchaseTax.pendingTax, Equal<True>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>>>, PX.Objects.TX.TaxType.pendingPurchase, Case<Where<PurchaseTax.pendingTax, Equal<True>, And<PX.Objects.TX.Tax.reverseTax, Equal<True>>>, PX.Objects.TX.TaxType.pendingSales, Case<Where<PurchaseTax.taxType, Equal<CSTaxType.use>, Or<PurchaseTax.taxType, Equal<CSTaxType.withholding>, Or<PurchaseTax.taxType, Equal<CSTaxType.perUnit>, Or<PX.Objects.TX.Tax.reverseTax, Equal<True>>>>>, PX.Objects.TX.TaxType.sales>>>, PX.Objects.TX.TaxType.purchase>), typeof (string))]
  public virtual string TranTaxType
  {
    get => this._TranTaxType;
    set => this._TranTaxType = value;
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseTax.taxID>
  {
  }

  public new abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseTax.taxType>
  {
  }

  public new abstract class taxVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PurchaseTax.taxVendorID>
  {
  }

  public new abstract class salesTaxAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PurchaseTax.salesTaxAcctID>
  {
  }

  public new abstract class salesTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PurchaseTax.salesTaxSubID>
  {
  }

  public new abstract class purchTaxAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PurchaseTax.purchTaxAcctID>
  {
  }

  public new abstract class purchTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PurchaseTax.purchTaxSubID>
  {
  }

  public new abstract class pendingTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PurchaseTax.pendingTax>
  {
  }

  public new abstract class pendingSalesTaxAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PurchaseTax.pendingSalesTaxAcctID>
  {
  }

  public new abstract class pendingSalesTaxSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PurchaseTax.pendingSalesTaxSubID>
  {
  }

  public new abstract class pendingPurchTaxAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PurchaseTax.pendingPurchTaxAcctID>
  {
  }

  public new abstract class pendingPurchTaxSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PurchaseTax.pendingPurchTaxSubID>
  {
  }

  public abstract class histTaxAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PurchaseTax.histTaxAcctID>
  {
  }

  public abstract class histTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PurchaseTax.histTaxSubID>
  {
  }

  public abstract class tranTaxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseTax.tranTaxType>
  {
  }
}
