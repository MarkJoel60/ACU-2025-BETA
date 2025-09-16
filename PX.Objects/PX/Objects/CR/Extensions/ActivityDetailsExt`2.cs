// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ActivityDetailsExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.SP;

#nullable disable
namespace PX.Objects.CR.Extensions;

public abstract class ActivityDetailsExt<TGraph, TPrimaryEntity> : 
  ActivityDetailsExt<TGraph, TPrimaryEntity, CRPMTimeActivity, CRPMTimeActivity.noteID>
  where TGraph : PXGraph, new()
  where TPrimaryEntity : class, IBqlTable, new()
{
  public override System.Type GetOrderByClause()
  {
    return typeof (OrderBy<Desc<CRPMTimeActivity.createdDateTime>>);
  }

  public override System.Type GetClassConditionClause()
  {
    return typeof (Where<CRPMTimeActivity.classID, GreaterEqual<Zero>>);
  }

  public override System.Type GetPrivateConditionClause()
  {
    return !PortalHelper.IsPortalContext((PortalContexts) 3) ? (System.Type) null : typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRPMTimeActivity.isPrivate, IsNull>>>>.Or<BqlOperand<CRPMTimeActivity.isPrivate, IBqlBool>.IsEqual<False>>>);
  }
}
