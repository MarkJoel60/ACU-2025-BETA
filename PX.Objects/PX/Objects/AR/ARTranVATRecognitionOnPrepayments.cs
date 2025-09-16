// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <exclude />
public sealed class ARTranVATRecognitionOnPrepayments : PXCacheExtension<
#nullable disable
ARTran>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>();
  }

  /// <summary>
  /// Part of sales order amount which shall be paid in PPI document
  /// </summary>
  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTranVATRecognitionOnPrepayments.prepaymentAmt))]
  [PXFormula(typeof (Mult<Sub<ARTran.curyExtPrice, ARTran.curyDiscAmt>, Div<ARTranVATRecognitionOnPrepayments.prepaymentPct, decimal100>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? CuryPrepaymentAmt { get; set; }

  /// <summary>
  /// Part of sales order amount which shall be paid in PPI document
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? PrepaymentAmt { get; set; }

  /// <summary>
  /// Percent of the Sales Order amount which shall be paid in PPI document
  /// </summary>
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXFormula(typeof (CalculatePrepaymentPercent<ARTranVATRecognitionOnPrepayments.prepaymentPct, ARTranVATRecognitionOnPrepayments.curyPrepaymentAmt, ARTran.curyExtPrice, ARTran.curyDiscAmt>))]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField]
  public Decimal? PrepaymentPct { get; set; }

  public abstract class curyPrepaymentAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranVATRecognitionOnPrepayments.curyPrepaymentAmt>
  {
  }

  public abstract class prepaymentAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranVATRecognitionOnPrepayments.prepaymentAmt>
  {
  }

  public abstract class prepaymentPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranVATRecognitionOnPrepayments.prepaymentPct>
  {
  }
}
