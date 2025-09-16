// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <inheritdoc cref="T:PX.Objects.AR.ARInvoice" />
public sealed class ARInvoiceVATRecognitionOnPrepayments : PXCacheExtension<
#nullable disable
ARInvoice>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>();
  }

  /// <summary>
  /// Part of the Sales Order amount on which a PPI document is creating.
  /// </summary>
  [PXDecimal]
  public Decimal? CuryPrepaymentAmt { get; set; }

  public abstract class curyPrepaymentAmt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoiceVATRecognitionOnPrepayments.curyPrepaymentAmt>
  {
  }
}
