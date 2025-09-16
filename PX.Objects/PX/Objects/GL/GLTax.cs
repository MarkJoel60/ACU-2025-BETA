// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXCacheName("GL Tax Detail")]
[Serializable]
public class GLTax : 
  TaxDetail,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ITaxDetailWithLineNbr,
  ITaxDetail
{
  protected 
  #nullable disable
  string _Module;
  protected string _BatchNbr;
  protected int? _LineNbr;
  protected short? _DetailType;
  protected Decimal? _CuryTaxableAmt;
  protected Decimal? _TaxableAmt;
  protected Decimal? _CuryTaxAmt;
  protected Decimal? _TaxAmt;

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (GLDocBatch), DefaultForUpdate = false)]
  [PXUIField]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (GLDocBatch), DefaultForUpdate = false)]
  [PXUIField]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXParent(typeof (Select<GLTranDoc, Where<GLTranDoc.module, Equal<Current<GLTax.module>>, And<GLTranDoc.batchNbr, Equal<Current<GLTax.batchNbr>>, And<GLTranDoc.lineNbr, Equal<Current<GLTax.lineNbr>>, And<Current<GLTax.detailType>, Equal<GLTaxDetailType.lineTax>>>>>>))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBShort(IsKey = true)]
  [PXDefault(0)]
  [PXUIField]
  public virtual short? DetailType
  {
    get => this._DetailType;
    set => this._DetailType = value;
  }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (GLTranDoc.curyInfoID), Required = true)]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (GLTax.curyInfoID), typeof (GLTax.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt
  {
    get => this._TaxableAmt;
    set => this._TaxableAmt = value;
  }

  [PXDBCurrency(typeof (GLTax.curyInfoID), typeof (GLTax.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt
  {
    get => this._TaxAmt;
    set => this._TaxAmt = value;
  }

  [PXDBCurrency(typeof (GLTax.curyInfoID), typeof (GLTax.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  /// <summary>
  /// A Boolean value that specifies that the tax transaction is tax inclusive or not
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Inclusive")]
  public virtual bool? IsTaxInclusive { get; set; }

  public class PK : 
    PrimaryKeyOf<GLTax>.By<GLTax.module, GLTax.batchNbr, GLTax.lineNbr, GLTax.detailType, GLTax.taxID>
  {
    public static GLTax Find(
      PXGraph graph,
      string module,
      string batchNbr,
      int? lineNbr,
      short? detailType,
      string taxID,
      PKFindOptions options = 0)
    {
      return (GLTax) PrimaryKeyOf<GLTax>.By<GLTax.module, GLTax.batchNbr, GLTax.lineNbr, GLTax.detailType, GLTax.taxID>.FindBy(graph, (object) module, (object) batchNbr, (object) lineNbr, (object) detailType, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class Batch : 
      PrimaryKeyOf<GLDocBatch>.By<GLDocBatch.module, GLDocBatch.batchNbr>.ForeignKeyOf<GLTax>.By<GLTax.module, GLTax.batchNbr>
    {
    }

    public class JournalVoucher : 
      PrimaryKeyOf<GLTranDoc>.By<GLTranDoc.module, GLTranDoc.batchNbr, GLTranDoc.lineNbr>.ForeignKeyOf<GLTax>.By<GLTax.module, GLTax.batchNbr, GLTax.lineNbr>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<GLTax>.By<GLTax.curyInfoID>
    {
    }

    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<GLTax>.By<GLTax.taxID>
    {
    }
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTax.module>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTax.batchNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTax.lineNbr>
  {
  }

  public abstract class detailType : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  GLTax.detailType>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTax.taxID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTax.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTax.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTax.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTax.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTax.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTax.taxAmt>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTax.expenseAmt>
  {
  }

  public abstract class curyExpenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTax.curyExpenseAmt>
  {
  }

  public abstract class isTaxInclusive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTax.isTaxInclusive>
  {
  }
}
