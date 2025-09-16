// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.DAC.TaxTranForReporting
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.TX.DAC;

/// <summary>
/// The extention of the <see cref="T:PX.Objects.TX.TaxTran" /> record for reporting with selectors and some additional fields.
/// </summary>
public class TaxTranForReporting : TaxTran
{
  protected 
  #nullable disable
  string _TranTypeInvoiceDiscriminated;
  protected Decimal? _Sign;

  [Vendor(typeof (Search<PX.Objects.AP.Vendor.bAccountID, Where<PX.Objects.AP.Vendor.taxAgency, Equal<boolTrue>>>))]
  public override int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXSelector(typeof (Search<TaxPeriod.taxPeriodID, Where<TaxPeriod.vendorID, Equal<Current<TaxTranReport.vendorID>>, Or<Current<TaxTranReport.vendorID>, IsNull>>>))]
  public override string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.TX.Tax.taxID, Where<PX.Objects.TX.Tax.taxVendorID, Equal<Current<TaxTranReport.vendorID>>, Or<Current<TaxTranReport.vendorID>, IsNull>>>))]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXString]
  [LabelList(typeof (TaxTranReport.tranTypeInvoiceDiscriminated))]
  [PXDBCalced(typeof (Switch<Case<Where<TaxTran.module, Equal<BatchModule.moduleAP>, And<TaxTran.tranType, Equal<APDocType.invoice>>>, TaxTranReport.tranTypeInvoiceDiscriminated.apInvoice, Case<Where<TaxTran.module, Equal<BatchModule.moduleAR>, And<TaxTran.tranType, Equal<ARDocType.invoice>>>, TaxTranReport.tranTypeInvoiceDiscriminated.arInvoice>>, TaxTran.tranType>), typeof (string))]
  [PXUIField(DisplayName = "Tran. Type")]
  public virtual string TranTypeInvoiceDiscriminated
  {
    get => this._TranTypeInvoiceDiscriminated;
    set => this._TranTypeInvoiceDiscriminated = value;
  }

  /// <summary>
  /// Sign of TaxTran with which it will adjust net tax amount.
  /// Consists of following multipliers:
  /// - Tax type of TaxTran:
  /// 	- Sales (Output): 1
  /// 	- Purchase (Input): -1
  /// - Document type and module:
  /// 	- AP
  /// 		- Debit Adjustment, Voided Cash Purchases, Refund: -1
  /// 		- Invoice, Credit Adjustment, Cash Purchase, Voided Payment, any other not listed: 1
  /// 	- AR
  /// 		- Credit Memo, Cash Return: -1
  /// 		- Invoice, Debit Memo, Fin Charge, Cash Sale, any other not listed: 1
  /// 	- GL
  /// 		- Reversing GL Entry: -1
  /// 		- GL Entry, any other not listed: 1
  /// 	- CA: 1
  /// 	- Any other not listed combinations: -1
  /// </summary>
  [PXDecimal]
  public virtual Decimal? Sign
  {
    [PXDependsOnFields(new Type[] {typeof (TaxTran.module), typeof (TaxTran.tranType), typeof (TaxTran.taxType)})] get
    {
      return new Decimal?(ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, new short?((short) 1)));
    }
    set => this._Sign = value;
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTranForReporting.vendorID>
  {
  }

  public new abstract class taxPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranForReporting.taxPeriodID>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranForReporting.taxID>
  {
  }

  public abstract class tranTypeInvoiceDiscriminated : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranForReporting.tranTypeInvoiceDiscriminated>
  {
  }

  public abstract class sign : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTranForReporting.sign>
  {
  }
}
