// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FASuspendPeriodSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable disable
namespace PX.Objects.FA;

public class FASuspendPeriodSelectorAttribute : FABookPeriodSelectorAttribute
{
  public FASuspendPeriodSelectorAttribute()
    : base(typeof (Search2<FABookPeriod.finPeriodID, InnerJoin<FABookBalance, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookBalance.bookID, Equal<FABookPeriod.bookID>>>>>.And<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<PX.Data.BQL.BqlField<FixedAsset.assetID, IBqlInt>.FromCurrent>>>>, Where<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>, And<FABookPeriod.finPeriodID, GreaterEqual<IsNull<FABookBalance.currDeprPeriod, IsNull<FABookBalance.lastDeprPeriod, FABookBalance.deprFromPeriod>>>>>>), isBookRequired: false, branchSourceType: typeof (FixedAsset.branchID))
  {
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (sender.Graph.IsImport && ((((PXCache) GraphHelper.Caches<FixedAsset>(sender.Graph)).Current is FixedAsset current ? current.AssetID : new int?()) ?? int.MinValue) < 0)
      return;
    base.FieldVerifying(sender, e);
  }
}
