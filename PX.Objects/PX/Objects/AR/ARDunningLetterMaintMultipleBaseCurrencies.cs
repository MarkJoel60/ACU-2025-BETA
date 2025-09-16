// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDunningLetterMaintMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.AR;

public class ARDunningLetterMaintMultipleBaseCurrencies : PXGraphExtension<ARDunningLetterMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXDBBaseCury(typeof (ARDunningLetter.branchID), null)]
  protected virtual void _(Events.CacheAttached<ARDunningLetter.dunningFee> e)
  {
  }

  [PXMergeAttributes]
  [PXDBBaseCury(typeof (ARDunningLetter.branchID), null)]
  protected virtual void _(
    Events.CacheAttached<ARDunningLetterDetail.origDocAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXBaseCury(typeof (ARDunningLetter.branchID), null)]
  protected virtual void _(
    Events.CacheAttached<ARDunningLetterDetail.overdueBal> e)
  {
  }
}
