// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SalesTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXHidden]
[Serializable]
public class SalesTax : PX.Objects.TX.Tax
{
  protected int? _HistTaxAcctID;
  protected int? _HistTaxSubID;
  protected 
  #nullable disable
  string _TranTaxType;

  [PXDBCalced(typeof (Switch<Case<Where<SalesTax.pendingTax, Equal<True>>, SalesTax.pendingSalesTaxAcctID>, SalesTax.salesTaxAcctID>), typeof (int))]
  public virtual int? HistTaxAcctID
  {
    get => this._HistTaxAcctID;
    set => this._HistTaxAcctID = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<SalesTax.pendingTax, Equal<True>>, SalesTax.pendingSalesTaxSubID>, SalesTax.salesTaxSubID>), typeof (int))]
  public virtual int? HistTaxSubID
  {
    get => this._HistTaxSubID;
    set => this._HistTaxSubID = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<SalesTax.pendingTax, Equal<True>>, PX.Objects.TX.TaxType.pendingSales>, PX.Objects.TX.TaxType.sales>), typeof (string))]
  public virtual string TranTaxType
  {
    get => this._TranTaxType;
    set => this._TranTaxType = value;
  }

  public new class PK : PrimaryKeyOf<SalesTax>.By<SalesTax.taxID>
  {
    public static SalesTax Find(PXGraph graph, string taxID, PKFindOptions options = 0)
    {
      return (SalesTax) PrimaryKeyOf<SalesTax>.By<SalesTax.taxID>.FindBy(graph, (object) taxID, options);
    }
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesTax.taxID>
  {
  }

  public new abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesTax.taxType>
  {
  }

  public new abstract class taxVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesTax.taxVendorID>
  {
  }

  public new abstract class salesTaxAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesTax.salesTaxAcctID>
  {
  }

  public new abstract class salesTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesTax.salesTaxSubID>
  {
  }

  public new abstract class purchTaxAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesTax.purchTaxAcctID>
  {
  }

  public new abstract class purchTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesTax.purchTaxSubID>
  {
  }

  public new abstract class pendingTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SalesTax.pendingTax>
  {
  }

  public new abstract class pendingSalesTaxAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SalesTax.pendingSalesTaxAcctID>
  {
  }

  public new abstract class pendingSalesTaxSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SalesTax.pendingSalesTaxSubID>
  {
  }

  public new abstract class pendingPurchTaxAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SalesTax.pendingPurchTaxAcctID>
  {
  }

  public new abstract class pendingPurchTaxSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SalesTax.pendingPurchTaxSubID>
  {
  }

  public new abstract class onARPrepaymentTaxAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SalesTax.onARPrepaymentTaxAcctID>
  {
  }

  public new abstract class onARPrepaymentTaxSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SalesTax.onARPrepaymentTaxSubID>
  {
  }

  public abstract class histTaxAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesTax.histTaxAcctID>
  {
  }

  public abstract class histTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesTax.histTaxSubID>
  {
  }

  public abstract class tranTaxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesTax.tranTaxType>
  {
  }
}
