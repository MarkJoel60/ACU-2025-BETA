// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FADetailsMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.FA;

public sealed class FADetailsMultipleBaseCurrencies : PXCacheExtension<FADetails>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXFormula(typeof (Selector<BqlField<FixedAsset.branchID, IBqlInt>.FromCurrent, PX.Objects.GL.Branch.baseCuryID>))]
  public string BaseCuryID { get; set; }
}
