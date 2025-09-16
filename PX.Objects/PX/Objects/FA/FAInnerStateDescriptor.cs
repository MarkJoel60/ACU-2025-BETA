// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAInnerStateDescriptor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FA;

public class FAInnerStateDescriptor
{
  public static bool FixedAssetHasTransactions(
    int? assetID,
    PXGraph graph,
    string tranType,
    bool isReleased,
    string origin = null)
  {
    PXSelectBase<FATran> pxSelectBase = (PXSelectBase<FATran>) new PXSelectReadonly<FATran, Where<FATran.assetID, Equal<Required<FixedAsset.assetID>>, And<FATran.tranType, Equal<Required<FATran.tranType>>>>>(graph);
    if (isReleased)
      pxSelectBase.WhereAnd<Where<FATran.released, Equal<True>>>();
    else
      pxSelectBase.WhereAnd<Where<FATran.released, NotEqual<True>>>();
    if (origin != null)
    {
      pxSelectBase.WhereAnd<Where<FATran.origin, Equal<Required<FATran.origin>>>>();
      return pxSelectBase.SelectSingle(new object[3]
      {
        (object) assetID,
        (object) tranType,
        (object) origin
      }) != null;
    }
    return pxSelectBase.SelectSingle(new object[2]
    {
      (object) assetID,
      (object) tranType
    }) != null;
  }

  /// <summary>
  /// Defines that the fixed asset has been converted from the purchase.
  /// Such fixed asset has the unreleased R+ transactions and does not have released P+ transaction.
  /// </summary>
  public static bool IsConvertedFromAP(int? assetID, PXGraph graph)
  {
    return FAInnerStateDescriptor.FixedAssetHasTransactions(assetID, graph, "R+", false) && !FAInnerStateDescriptor.FixedAssetHasTransactions(assetID, graph, "P+", true);
  }

  /// <summary>
  /// Defines that the fixed asset will be transferred.
  /// Such fixed asset has the unreleased TP transactions.
  /// </summary>
  public static bool WillBeTransferred(int? assetID, PXGraph graph)
  {
    return FAInnerStateDescriptor.FixedAssetHasTransactions(assetID, graph, "TP", false) || FAInnerStateDescriptor.FixedAssetHasTransactions(assetID, graph, "TD", false);
  }

  /// <summary>
  /// Defines that the fixed asset is acquired.
  /// Such fixed asset has the reased R+ transactions.
  /// </summary>
  public static bool IsAcquired(int? assetID, PXGraph graph)
  {
    return FAInnerStateDescriptor.FixedAssetHasTransactions(assetID, graph, "P+", true);
  }
}
