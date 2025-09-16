// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOLineRelatedItemsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN.RelatedItems;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class SOLineRelatedItemsAttribute : RelatedItemsAttribute
{
  public SOLineRelatedItemsAttribute()
  {
  }

  public SOLineRelatedItemsAttribute(
    Type suggestRelatedItemsField,
    Type relationField,
    Type requiredField)
    : base(suggestRelatedItemsField, relationField, requiredField)
  {
  }

  protected override object[] GetRelatedItemsQueryArguments(
    PXGraph graph,
    ISubstitutableLine substitutableLine,
    DateTime? documentDate,
    bool? showOnlyAvailableItems)
  {
    object[] itemsQueryArguments = base.GetRelatedItemsQueryArguments(graph, substitutableLine, documentDate, showOnlyAvailableItems);
    string behavior = ((SOLine) substitutableLine).Behavior;
    itemsQueryArguments[itemsQueryArguments.Length - 2] = (object) behavior;
    itemsQueryArguments[itemsQueryArguments.Length - 1] = (object) behavior;
    return itemsQueryArguments;
  }
}
