// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.CacheExtensions.POLineSExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.PO;

#nullable enable
namespace PX.Objects.CN.Subcontracts.AP.CacheExtensions;

/// <inheritdoc cref="T:PX.Objects.PO.POLineS" />
public sealed class POLineSExt : PXCacheExtension<
#nullable disable
POLineS>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.construction>() && PXAccess.FeatureInstalled<FeaturesSet.projectRelatedDocumentsRecognition>();
  }

  /// <summary>The subcontract number.</summary>
  [PXString]
  [PXUIField(DisplayName = "Subcontract Nbr.")]
  public string SubcontractNbr
  {
    get => !(this.Base.OrderType == "RS") ? (string) null : this.Base.OrderNbr;
  }

  public abstract class subcontractNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineSExt.subcontractNbr>
  {
  }
}
