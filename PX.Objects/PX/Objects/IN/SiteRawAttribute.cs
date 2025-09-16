// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.SiteRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

[PXDBString(30, IsUnicode = true, InputMask = "", PadSpaced = true)]
[PXUIField]
public sealed class SiteRawAttribute : PXEntityAttribute
{
  public string DimensionName = "INSITE";

  public SiteRawAttribute(bool isTransitAllowed)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute(this.DimensionName, isTransitAllowed ? typeof (Search<INSite.siteCD, Where<Match<Current<AccessInfo.userName>>>>) : typeof (Search<INSite.siteCD, Where<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>, And<Match<Current<AccessInfo.userName>>>>>), typeof (INSite.siteCD))
    {
      CacheGlobal = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }
}
