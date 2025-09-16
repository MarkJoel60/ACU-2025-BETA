// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTaxTranImported
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXVirtual]
[PXBreakInheritance]
[PXHidden]
[Serializable]
public class APTaxTranImported : APTaxTran
{
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (APRegister.docType))]
  [PXParent(typeof (Select<APRegister, Where<APRegister.docType, Equal<Current<TaxTran.tranType>>, And<APRegister.refNbr, Equal<Current<TaxTran.refNbr>>>>>))]
  [PXUIField(DisplayName = "Tran. Type", Enabled = false, Visible = false)]
  public override 
  #nullable disable
  string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APRegister.refNbr))]
  [PXUIField(DisplayName = "Ref. Nbr.", Enabled = false, Visible = false)]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (APRegister.docDate))]
  public override System.DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID", Visibility = PXUIVisibility.Visible)]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr), DirtyRead = true, ValidateValue = false)]
  public override string TaxID { get; set; }

  [PXDBLong]
  public override long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Taxable Amount", Visibility = PXUIVisibility.Visible)]
  public new Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  public new abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTranImported.tranType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTranImported.refNbr>
  {
  }

  public new abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APTaxTranImported.tranDate>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTranImported.taxID>
  {
  }

  public new abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTranImported.curyTaxableAmt>
  {
  }
}
