// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CacheExtensions.APRecognizedTranExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.PM.CacheExtensions;

/// <inheritdoc cref="T:PX.Objects.AP.InvoiceRecognition.DAC.APRecognizedTran" />
public sealed class APRecognizedTranExt : PXCacheExtension<
#nullable disable
APRecognizedTran>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectRelatedDocumentsRecognition>();
  }

  /// <summary>
  /// The number of the line of a recognized purchase order.
  /// </summary>
  [PXInt]
  [PXUIField(DisplayName = "PO Line", Enabled = false, IsReadOnly = true, Visible = false)]
  public int? RecognizedPOLineNbr => this.Base.POLineNbr;

  public abstract class recognizedPOLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRecognizedTranExt.recognizedPOLineNbr>
  {
  }
}
