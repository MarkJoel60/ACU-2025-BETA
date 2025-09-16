// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXProjection(typeof (Select<GLTax, Where<GLTax.detailType, Equal<GLTaxDetailType.docTax>>>), Persistent = true)]
[PXCacheName("GL Tax Transaction")]
[Serializable]
public class GLTaxTran : GLTax
{
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (GLDocBatch), DefaultForUpdate = false)]
  [PXUIField(DisplayName = "Order Type", Visible = false)]
  public override 
  #nullable disable
  string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (GLDocBatch), DefaultForUpdate = false)]
  [PXUIField(DisplayName = "Order Nbr.", Visible = false)]
  public override string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (Coalesce<Search<GLTranDoc.lineNbr, Where<GLTranDoc.module, Equal<Current<GLTranDoc.module>>, And<GLTranDoc.batchNbr, Equal<Current<GLTranDoc.batchNbr>>, And<GLTranDoc.lineNbr, Equal<Current<GLTranDoc.parentLineNbr>>>>>>, Search<GLTranDoc.lineNbr, Where<GLTranDoc.module, Equal<Current<GLTranDoc.module>>, And<GLTranDoc.batchNbr, Equal<Current<GLTranDoc.batchNbr>>, And<GLTranDoc.lineNbr, Equal<Current<GLTranDoc.lineNbr>>>>>>>))]
  [PXUIField]
  [PXParent(typeof (Select<GLTranDoc, Where<GLTranDoc.module, Equal<Current<GLTaxTran.module>>, And<GLTranDoc.batchNbr, Equal<Current<GLTaxTran.batchNbr>>, And<GLTranDoc.lineNbr, Equal<Current<GLTaxTran.lineNbr>>>>>>))]
  public override int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBShort(IsKey = true)]
  [PXDefault(1)]
  [PXUIField]
  public override short? DetailType
  {
    get => this._DetailType;
    set => this._DetailType = value;
  }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (GLTranDoc.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (GLTaxTran.curyInfoID), typeof (GLTaxTran.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Switch<Case<Where<GLTaxTran.curyExpenseAmt, NotEqual<decimal0>>, Sub<Mult<GLTaxTran.curyTaxableAmt, Div<GLTax.taxRate, decimal100>>, GLTaxTran.curyExpenseAmt>>, GLTaxTran.curyTaxAmt>), null)]
  [PXUIField]
  public override Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  [PXDBCurrency(typeof (GLTaxTran.curyInfoID), typeof (GLTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  [PXDBCurrency(typeof (GLTaxTran.curyInfoID), typeof (GLTaxTran.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Mult<Mult<GLTaxTran.curyTaxableAmt, Div<GLTax.taxRate, decimal100>>, Sub<decimal1, Div<GLTaxTran.nonDeductibleTaxRate, decimal100>>>), null)]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTaxTran.module>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTaxTran.batchNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTaxTran.lineNbr>
  {
  }

  public new abstract class detailType : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  GLTaxTran.detailType>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTaxTran.taxID>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTaxTran.curyInfoID>
  {
  }

  public new abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTaxTran.curyTaxAmt>
  {
  }

  public new abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTaxTran.taxAmt>
  {
  }

  public new abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTaxTran.curyTaxableAmt>
  {
  }

  public new abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTaxTran.taxableAmt>
  {
  }

  public abstract class nonDeductibleTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTaxTran.nonDeductibleTaxRate>
  {
  }

  public new abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTaxTran.expenseAmt>
  {
  }

  public new abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTaxTran.curyExpenseAmt>
  {
  }
}
