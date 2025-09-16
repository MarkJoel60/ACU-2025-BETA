// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.GLTranFilterMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.FA;

public sealed class GLTranFilterMultipleBaseCurrencies : PXCacheExtension<GLTranFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [BaseCurrency(typeof (GLTranFilter.branchID))]
  public string BranchBaseCuryID { get; set; }
}
