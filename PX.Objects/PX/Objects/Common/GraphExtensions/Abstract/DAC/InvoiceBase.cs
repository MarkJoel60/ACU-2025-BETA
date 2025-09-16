// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.DAC.InvoiceBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Common.GraphExtensions.Abstract.DAC;

public class InvoiceBase : Document
{
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  public virtual int? ModuleAccountID { get; set; }

  public virtual int? ModuleSubID { get; set; }

  public string DocType { get; set; }

  public string RefNbr { get; set; }

  public long? CuryInfoID { get; set; }

  public bool? Hold { get; set; }

  public bool? Released { get; set; }

  public bool? Printed { get; set; }

  public bool? OpenDoc { get; set; }

  public string FinPeriodID { get; set; }

  public string InvoiceNbr { get; set; }

  public string DocDesc { get; set; }

  public int? ContragentID { get; set; }

  public int? ContragentLocationID { get; set; }

  public string TaxZoneID { get; set; }

  public string TaxCalcMode { get; set; }

  public string OrigModule { get; set; }

  public string OrigDocType { get; set; }

  public string OrigRefNbr { get; set; }

  public Decimal? CuryOrigDocAmt { get; set; }

  public Decimal? CuryTaxAmt { get; set; }

  public Decimal? CuryDocBal { get; set; }

  public Decimal? CuryTaxTotal { get; set; }

  public Decimal? CuryTaxRoundDiff { get; set; }

  public Decimal? CuryRoundDiff { get; set; }

  public Decimal? TaxRoundDiff { get; set; }

  public Decimal? RoundDiff { get; set; }

  public Decimal? TaxAmt { get; set; }

  public Decimal? DocBal { get; set; }

  public bool? Approved { get; set; }

  public int? CashAccountID { get; set; }

  public string PaymentMethodID { get; set; }

  public virtual long? CATranID { get; set; }

  public virtual DateTime? ClearDate { get; set; }

  public virtual bool? Cleared { get; set; }

  public virtual string ExtRefNbr { get; set; }

  public abstract class curyID : IBqlField, IBqlOperand
  {
  }

  public abstract class moduleAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class moduleSubID : IBqlField, IBqlOperand
  {
  }

  public abstract class docType : IBqlField, IBqlOperand
  {
  }

  public abstract class refNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class curyInfoID : IBqlField, IBqlOperand
  {
  }

  public abstract class hold : IBqlField, IBqlOperand
  {
  }

  public abstract class released : IBqlField, IBqlOperand
  {
  }

  public abstract class printed : IBqlField, IBqlOperand
  {
  }

  public abstract class openDoc : IBqlField, IBqlOperand
  {
  }

  public abstract class finPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class invoiceNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class docDesc : IBqlField, IBqlOperand
  {
  }

  public abstract class contragentID : IBqlField, IBqlOperand
  {
  }

  public abstract class contragentLocationID : IBqlField, IBqlOperand
  {
  }

  public abstract class taxZoneID : IBqlField, IBqlOperand
  {
  }

  public abstract class taxCalcMode : IBqlField, IBqlOperand
  {
  }

  public abstract class origModule : IBqlField, IBqlOperand
  {
  }

  public abstract class origDocType : IBqlField, IBqlOperand
  {
  }

  public abstract class origRefNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class curyOrigDocAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyTaxAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyDocBal : IBqlField, IBqlOperand
  {
  }

  public abstract class curyTaxTotal : IBqlField, IBqlOperand
  {
  }

  public abstract class curyTaxRoundDiff : IBqlField, IBqlOperand
  {
  }

  public abstract class curyRoundDiff : IBqlField, IBqlOperand
  {
  }

  public abstract class taxRoundDiff : IBqlField, IBqlOperand
  {
  }

  public abstract class roundDiff : IBqlField, IBqlOperand
  {
  }

  public abstract class taxAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class docBal : IBqlField, IBqlOperand
  {
  }

  public abstract class approved : IBqlField, IBqlOperand
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceBase.cashAccountID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InvoiceBase.paymentMethodID>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  InvoiceBase.cATranID>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  InvoiceBase.clearDate>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InvoiceBase.cleared>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceBase.extRefNbr>
  {
  }
}
