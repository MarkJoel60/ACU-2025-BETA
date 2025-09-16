// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxTranReport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.TX;

[PXProjection(typeof (Select<TaxTran>))]
[PXCacheName("Tax Transaction for Report")]
public class TaxTranReport : TaxTran
{
  protected 
  #nullable disable
  string _TranTypeInvoiceDiscriminated;
  protected DateTime? _FinDate;
  protected Decimal? _Sign;

  /// Overridden from TaxTran to avoid affecting other Forms.
  ///             <inheritdoc cref="P:PX.Objects.TX.TaxTran.Module" />
  [PXDBString(2, IsFixed = true)]
  [PXDefault("GL")]
  [PXUIField(DisplayName = "Module")]
  [BatchModule.List]
  public new virtual string Module { get; set; }

  [PXString]
  [LabelList(typeof (TaxTranReport.tranTypeInvoiceDiscriminated))]
  [PXDBCalced(typeof (Switch<Case<Where<TaxTran.module, Equal<BatchModule.moduleAP>, And<TaxTran.tranType, Equal<APDocType.invoice>>>, TaxTranReport.tranTypeInvoiceDiscriminated.apInvoice, Case<Where<TaxTran.module, Equal<BatchModule.moduleAR>, And<TaxTran.tranType, Equal<ARDocType.invoice>>>, TaxTranReport.tranTypeInvoiceDiscriminated.arInvoice>>, TaxTran.tranType>), typeof (string))]
  [PXUIField(DisplayName = "Tran. Type")]
  public virtual string TranTypeInvoiceDiscriminated
  {
    get => this._TranTypeInvoiceDiscriminated;
    set => this._TranTypeInvoiceDiscriminated = value;
  }

  [PXDBCurrency(typeof (TaxTranReport.curyInfoID), typeof (TaxTranReport.taxAmtSumm))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? CuryTaxAmtSumm { get; set; }

  /// <inheritdoc cref="P:PX.Objects.TX.TaxTran.ReportTaxableAmt" />
  [PXDBVendorCury(typeof (TaxTranReport.vendorID), typeof (TaxTranReport.branchID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? ReportTaxableAmt { get; set; }

  /// <inheritdoc cref="P:PX.Objects.TX.TaxTran.ReportTaxAmt" />
  [PXDBVendorCury(typeof (TaxTranReport.vendorID), typeof (TaxTranReport.branchID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? ReportTaxAmt { get; set; }

  [PXDBDate]
  [PXUIField]
  public override DateTime? FinDate { get; set; }

  /// <summary>
  /// Sign of TaxTran with which it will adjust net tax amount.
  /// Consists of following multipliers:
  /// - Tax type of TaxTran:
  /// 	- Sales (Output): 1
  /// 	- Purchase (Input): -1
  /// - Document type and module:
  /// 	- AP
  /// 		- Debit Adjustment, Voided Cash Puchase, Refund: -1
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
    [PXDependsOnFields(new Type[] {typeof (TaxTranReport.module), typeof (TaxTranReport.tranType), typeof (TaxTranReport.taxType)})] get
    {
      return new Decimal?(ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, new short?((short) 1)));
    }
    set => this._Sign = value;
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTranReport.branchID>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranReport.refNbr>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTranReport.vendorID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTranReport.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTranReport.subID>
  {
  }

  public new abstract class taxPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranReport.taxPeriodID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranReport.finPeriodID>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxTranReport.tstamp>
  {
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranReport.module>
  {
  }

  public new abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranReport.tranType>
  {
  }

  public class tranTypeInvoiceDiscriminated : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranReport.tranTypeInvoiceDiscriminated>,
    ILabelProvider
  {
    public const string APInvoice = "INP";
    public const string ARInvoice = "INR";

    public IEnumerable<ValueLabelPair> ValueLabelPairs { get; } = (IEnumerable<ValueLabelPair>) new ValueLabelList()
    {
      {
        "INP",
        "Bill"
      },
      {
        "ADR",
        "Debit Adj."
      },
      {
        "ACR",
        "Credit Adj."
      },
      {
        "QCK",
        "Cash Purchase"
      },
      {
        "VQC",
        "Voided Cash Purchase"
      },
      {
        "RQC",
        "Cash Return"
      },
      {
        "CHK",
        "Payment"
      },
      {
        "VCK",
        "Voided Payment"
      },
      {
        "PPM",
        "Prepayment"
      },
      {
        "REF",
        "Refund"
      },
      {
        "INR",
        "Invoice"
      },
      {
        "DRM",
        "Debit Memo"
      },
      {
        "CRM",
        "Credit Memo"
      },
      {
        "CSL",
        "Cash Sale"
      },
      {
        "RCS",
        "Cash Return"
      },
      {
        "PMT",
        "Payment"
      },
      {
        "RPM",
        "Voided Payment"
      },
      {
        "PPM",
        "Prepayment"
      },
      {
        "REF",
        "Refund"
      },
      {
        "FCH",
        "Overdue Charge"
      },
      {
        "SMB",
        "Balance WO"
      },
      {
        "SMC",
        "Credit WO"
      },
      {
        "INT",
        "Adjust Output"
      },
      {
        "RET",
        "Adjust Input"
      },
      {
        "VTI",
        "Input VAT"
      },
      {
        "VTO",
        "Output VAT"
      },
      {
        "CAE",
        "Cash Entry"
      },
      {
        "CTE",
        "Expense Entry"
      },
      {
        "TRV",
        "Reversing GL Entry"
      },
      {
        "TFW",
        "GL Entry"
      }
    };

    public class apInvoice : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      TaxTranReport.tranTypeInvoiceDiscriminated.apInvoice>
    {
      public apInvoice()
        : base("INP")
      {
      }
    }

    public class arInvoice : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      TaxTranReport.tranTypeInvoiceDiscriminated.arInvoice>
    {
      public arInvoice()
        : base("INR")
      {
      }
    }
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxTranReport.released>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxTranReport.voided>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranReport.taxID>
  {
  }

  public new abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTranReport.taxRate>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  TaxTranReport.curyInfoID>
  {
  }

  public new abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTranReport.curyTaxableAmt>
  {
  }

  public new abstract class taxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTranReport.taxableAmt>
  {
  }

  public new abstract class curyTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTranReport.curyTaxAmt>
  {
  }

  public new abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTranReport.taxAmt>
  {
  }

  public new abstract class curyTaxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTranReport.curyTaxAmtSumm>
  {
  }

  public new abstract class taxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTranReport.taxAmtSumm>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranReport.curyID>
  {
  }

  public new abstract class reportCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranReport.reportCuryID>
  {
  }

  public new abstract class reportCuryRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranReport.reportCuryRateTypeID>
  {
  }

  public new abstract class reportCuryEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxTranReport.reportCuryEffDate>
  {
  }

  public new abstract class reportCuryMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranReport.reportCuryMultDiv>
  {
  }

  public new abstract class reportCuryRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTranReport.reportCuryRate>
  {
  }

  public new abstract class reportTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTranReport.reportTaxableAmt>
  {
  }

  public new abstract class reportTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTranReport.reportTaxAmt>
  {
  }

  public new abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxTranReport.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranReport.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxTranReport.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TaxTranReport.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranReport.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxTranReport.lastModifiedDateTime>
  {
  }

  public new abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxTranReport.tranDate>
  {
  }

  public new abstract class taxBucketID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTranReport.taxBucketID>
  {
  }

  public new abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranReport.taxType>
  {
  }

  public new abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranReport.taxZoneID>
  {
  }

  public new abstract class taxInvoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranReport.taxInvoiceNbr>
  {
  }

  public new abstract class taxInvoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxTranReport.taxInvoiceDate>
  {
  }

  public new abstract class origTranType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranReport.origTranType>
  {
  }

  public new abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranReport.origRefNbr>
  {
  }

  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTranReport.revisionID>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTranReport.bAccountID>
  {
  }

  public new abstract class finDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxTranReport.finDate>
  {
  }

  public abstract class sign : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTranReport.sign>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranReport.description>
  {
  }

  public new abstract class adjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTranReport.adjdDocType>
  {
  }

  public new abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranReport.adjdRefNbr>
  {
  }

  public new abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTranReport.adjNbr>
  {
  }
}
