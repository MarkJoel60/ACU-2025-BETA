// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Descriptor.Attributes.IgnoreUpdateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Common.Descriptor.Attributes;

public class IgnoreUpdateAttribute : PXEventSubscriberAttribute, IPXFieldUpdatingSubscriber
{
  private readonly Type[] graphTypes;

  public IgnoreUpdateAttribute(params Type[] graphTypes) => this.graphTypes = graphTypes;

  public void FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs args)
  {
    if (!this.IsGraphApplicable(cache.Graph) || this.IsAnyFieldUpdated(cache, args.Row))
      return;
    cache.SetStatus(args.Row, (PXEntryStatus) 0);
  }

  private bool IsGraphApplicable(PXGraph graph)
  {
    Type type = graph.GetType();
    return ((IEnumerable<Type>) this.graphTypes).IsEmpty<Type>() || ((IEnumerable<Type>) this.graphTypes).Contains<Type>(type);
  }

  private bool IsAnyFieldUpdated(PXCache cache, object newRow)
  {
    object original = cache.GetOriginal(newRow);
    foreach (Type bqlField in cache.BqlFields)
    {
      string field = cache.GetField(bqlField);
      string str1 = cache.GetValue(original, field)?.ToString();
      string str2 = cache.GetValue(newRow, field)?.ToString();
      if (field != this._FieldName && str1 != str2)
        return true;
    }
    return false;
  }
}
