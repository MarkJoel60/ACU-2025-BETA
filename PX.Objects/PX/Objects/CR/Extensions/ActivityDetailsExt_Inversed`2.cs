// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ActivityDetailsExt_Inversed`2
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

public abstract class ActivityDetailsExt_Inversed<TGraph, TPrimaryEntity> : 
  ActivityDetailsExt<TGraph, TPrimaryEntity, PMCRActivity, PMCRActivity.noteID>
  where TGraph : PXGraph, new()
  where TPrimaryEntity : class, IBqlTable, INotable, new()
{
  public override System.Type GetOrderByClause()
  {
    return typeof (OrderBy<Desc<CRPMTimeActivity.timeActivityCreatedDateTime>>);
  }

  public override System.Type GetClassConditionClause()
  {
    return typeof (Where<PMCRActivity.classID, IsNull, Or<PMCRActivity.classID, GreaterEqual<Zero>>>);
  }

  public override System.Type GetPrivateConditionClause()
  {
    return !PortalHelper.IsPortalContext((PortalContexts) 3) ? (System.Type) null : typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCRActivity.isPrivate, IsNull>>>>.Or<BqlOperand<PMCRActivity.isPrivate, IBqlBool>.IsEqual<False>>>);
  }
}
