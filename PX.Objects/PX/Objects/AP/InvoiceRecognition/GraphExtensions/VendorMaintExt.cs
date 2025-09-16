// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.GraphExtensions.VendorMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.InvoiceRecognition.DAC;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition.GraphExtensions;

[PXInternalUseOnly]
public class VendorMaintExt : PXGraphExtension<
#nullable disable
VendorMaint>
{
  public FbqlSelect<SelectFromBase<RecognizedVendorMapping, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  RecognizedVendorMapping.vendorID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  VendorR.bAccountID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  RecognizedVendorMapping>.View RecognizedVendors;

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.apDocumentRecognition>();
}
