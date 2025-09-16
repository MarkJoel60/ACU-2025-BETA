// Decompiled with JetBrains decompiler
// Type: PX.Data.FilterHeaderSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable disable
namespace PX.Data;

public class FilterHeaderSelectorAttribute : PXSelectorAttribute
{
  public FilterHeaderSelectorAttribute()
    : base(FilterHeaderSelectorAttribute.GetSelectorType(), typeof (FilterHeader.screenID), typeof (PX.SM.SiteMap.title), typeof (FilterHeader.viewName), typeof (FilterHeader.filterName))
  {
    this.DescriptionField = typeof (FilterHeader.filterName);
  }

  private static System.Type GetSelectorType()
  {
    return BqlTemplate.OfCommand<Search2<FilterHeader.filterID, InnerJoin<BqlPlaceholder.A, On<FilterHeader.screenID, Equal<BqlPlaceholder.B>>>, Where<FilterHeader.isShared, Equal<True>, And<FilterHeader.isHidden, Equal<False>, And<FilterHeader.screenID, NotEqual<FilterHeader.selectorConst>>>>, OrderBy<Asc<PX.SM.SiteMap.title>>>>.Replace<BqlPlaceholder.A>(PXSiteMap.IsPortal ? typeof (PX.SM.PortalMap) : typeof (PX.SM.SiteMap)).Replace<BqlPlaceholder.B>(PXSiteMap.IsPortal ? typeof (PX.SM.PortalMap.screenID) : typeof (PX.SM.SiteMap.screenID)).ToType();
  }
}
