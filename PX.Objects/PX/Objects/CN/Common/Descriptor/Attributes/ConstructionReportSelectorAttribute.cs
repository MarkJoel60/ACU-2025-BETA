// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Descriptor.Attributes.ConstructionReportSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CN.Common.Descriptor.Attributes;

public sealed class ConstructionReportSelectorAttribute : PXSelectorAttribute
{
  public ConstructionReportSelectorAttribute()
    : base(typeof (FbqlSelect<SelectFromBase<SiteMap, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SiteMap.url, Like<PX.Objects.Common.urlReports>>>>>.Or<BqlOperand<SiteMap.url, IBqlString>.IsLike<urlReportsInNewUi>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SiteMap.screenID, Like<PXModule.ap_>>>>, Or<BqlOperand<SiteMap.screenID, IBqlString>.IsLike<PXModule.po_>>>, Or<BqlOperand<SiteMap.screenID, IBqlString>.IsLike<PXModule.sc_>>>, Or<BqlOperand<SiteMap.screenID, IBqlString>.IsLike<PXModule.rq_>>>>.Or<BqlOperand<SiteMap.screenID, IBqlString>.IsLike<PXModule.cl_>>>>.Aggregate<To<GroupBy<SiteMap.screenID>>>, SiteMap>.SearchFor<SiteMap.screenID>), new Type[2]
    {
      typeof (SiteMap.screenID),
      typeof (SiteMap.title)
    })
  {
    this.Headers = new string[2]
    {
      "Report ID",
      "Report Name"
    };
    this.DescriptionField = typeof (SiteMap.title);
  }
}
