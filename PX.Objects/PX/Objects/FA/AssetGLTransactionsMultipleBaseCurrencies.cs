// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AssetGLTransactionsMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.FA;

public class AssetGLTransactionsMultipleBaseCurrencies : 
  FAAccrualTranMultipleBaseCurrenciesBase<AssetGLTransactions.GLTransactionsViewExtension, AssetGLTransactions>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<FixedAsset.baseCuryID, EqualBaseCuryID<Current<FATran.branchID>>>), "The fixed asset's currency ({0}) differs from the base currency of the transaction's {2} branch.", new Type[] {typeof (FixedAsset.baseCuryID), typeof (FixedAsset.assetCD), typeof (FATran.branchID)})]
  protected virtual void _(Events.CacheAttached<FATran.targetAssetID> e)
  {
  }

  [PXOverride]
  public virtual BqlCommand GetSelectCommand(
    GLTranFilter filter,
    AssetGLTransactionsMultipleBaseCurrencies.GetSelectCommandDelegate baseDelegate)
  {
    BqlCommand query = baseDelegate(filter);
    return this.ModifySelectCommand(filter, query);
  }

  public delegate BqlCommand GetSelectCommandDelegate(GLTranFilter filter);
}
