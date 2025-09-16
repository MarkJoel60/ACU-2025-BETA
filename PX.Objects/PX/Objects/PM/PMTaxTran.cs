// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.TX;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("PM Tax")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMTaxTran : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _LineNbr;
  protected int? _RecordID;
  protected Decimal? _CuryTaxableAmt;
  protected Decimal? _TaxableAmt;
  protected Decimal? _CuryTaxAmt;
  protected Decimal? _TaxAmt;
  protected Decimal? _CuryRetainedTaxableAmt;
  protected Decimal? _RetainedTaxableAmt;
  protected Decimal? _CuryRetainedTaxAmt;
  protected Decimal? _RetainedTaxAmt;
  protected 
  #nullable disable
  string _JurisType;
  protected string _JurisName;

  [PXDBString(15, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXDBDefault(typeof (PMProforma.refNbr))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false, Visible = false)]
  public virtual string RefNbr { get; set; }

  [PXUIField(DisplayName = "Revision", Visible = false)]
  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (PMProforma.revisionID))]
  public virtual int? RevisionID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(2147483647 /*0x7FFFFFFF*/)]
  [PXUIField]
  [PXParent(typeof (Select<PMProforma, Where<PMProforma.refNbr, Equal<Current<PMTaxTran.refNbr>>, And<PMProforma.revisionID, Equal<Current<PMTaxTran.revisionID>>>>>))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PX.Objects.TX.TaxID]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Tax.taxID), DescriptionField = typeof (Tax.descr), DirtyRead = true)]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PMProforma.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (PMTaxTran.curyInfoID), typeof (PMTaxTran.taxableAmt))]
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

  /// <summary>The exempted amount in the record currency.</summary>
  [PXDBCurrency(typeof (PMTaxTran.curyInfoID), typeof (PMTaxTran.exemptedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? CuryExemptedAmt { get; set; }

  /// <summary>The exempted amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? ExemptedAmt { get; set; }

  [PXDBCurrency(typeof (PMTaxTran.curyInfoID), typeof (PMTaxTran.taxAmt))]
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

  /// <summary>
  /// The deductible tax rate from the related <see cref="T:PX.Objects.TX.TaxRev" /> record.
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? NonDeductibleTaxRate { get; set; }

  /// <summary>The expense amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? ExpenseAmt { get; set; }

  /// <summary>The expense amount in the record currency.</summary>
  [PXDBCurrency(typeof (PMTaxTran.curyInfoID), typeof (PMTaxTran.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (PMTaxTran.curyInfoID), typeof (PMTaxTran.retainedTaxableAmt))]
  [PXUIField]
  public virtual Decimal? CuryRetainedTaxableAmt
  {
    get => this._CuryRetainedTaxableAmt;
    set => this._CuryRetainedTaxableAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  [PXUIField]
  public virtual Decimal? RetainedTaxableAmt
  {
    get => this._RetainedTaxableAmt;
    set => this._RetainedTaxableAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (PMTaxTran.curyInfoID), typeof (PMTaxTran.retainedTaxAmt))]
  [PXUIField]
  public virtual Decimal? CuryRetainedTaxAmt
  {
    get => this._CuryRetainedTaxAmt;
    set => this._CuryRetainedTaxAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  [PXUIField]
  public virtual Decimal? RetainedTaxAmt
  {
    get => this._RetainedTaxAmt;
    set => this._RetainedTaxAmt = value;
  }

  [PXDBString(10, IsUnicode = true)]
  public virtual string TaxZoneID { get; set; }

  [PXDBString(9, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Type")]
  public virtual string JurisType
  {
    get => this._JurisType;
    set => this._JurisType = value;
  }

  [PXDBString(200, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Name")]
  public virtual string JurisName
  {
    get => this._JurisName;
    set => this._JurisName = value;
  }

  /// <summary>
  /// A Boolean value that specifies that the tax transaction is tax inclusive or not
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Inclusive")]
  public virtual bool? IsTaxInclusive { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTaxTran.refNbr>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTaxTran.revisionID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTaxTran.lineNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTaxTran.taxID>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTaxTran.recordID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaxTran.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMTaxTran.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTaxTran.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaxTran.taxableAmt>
  {
  }

  public abstract class curyExemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class exemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaxTran.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaxTran.taxAmt>
  {
  }

  public abstract class nonDeductibleTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTaxTran.nonDeductibleTaxRate>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaxTran.expenseAmt>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTaxTran.curyExpenseAmt>
  {
  }

  public abstract class curyRetainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTaxTran.curyRetainedTaxableAmt>
  {
  }

  public abstract class retainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTaxTran.retainedTaxableAmt>
  {
  }

  public abstract class curyRetainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTaxTran.curyRetainedTaxAmt>
  {
  }

  public abstract class retainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTaxTran.retainedTaxAmt>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTaxTran.taxZoneID>
  {
  }

  public abstract class jurisType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTaxTran.jurisType>
  {
  }

  public abstract class jurisName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTaxTran.jurisName>
  {
  }

  public abstract class isTaxInclusive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTaxTran.isTaxInclusive>
  {
  }
}
