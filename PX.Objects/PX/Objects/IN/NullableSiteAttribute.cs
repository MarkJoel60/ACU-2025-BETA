// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.NullableSiteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

/// <summary>
/// Version of <see cref="T:PX.Objects.IN.SiteAttribute" /> that does not create default Warehouse if there are no warehouses
/// </summary>
[PXDBInt]
[PXUIField]
[PXDefault]
public class NullableSiteAttribute : SiteAttribute
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldDefaultingEvents fieldDefaulting = sender.Graph.FieldDefaulting;
    Type itemType = sender.GetItemType();
    string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
    NullableSiteAttribute nullableSiteAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) nullableSiteAttribute, __vmethodptr(nullableSiteAttribute, Feature_FieldDefaulting));
    fieldDefaulting.RemoveHandler(itemType, fieldName, pxFieldDefaulting);
  }
}
