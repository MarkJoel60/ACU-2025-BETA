// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectTaskRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Attribute for TaskCD field. Aggregates PXFieldAttribute, PXUIFieldAttribute and DimensionSelector without any restriction.
/// </summary>
[PXDBString(30, IsUnicode = true, InputMask = "", PadSpaced = true)]
[PXUIField]
public class ProjectTaskRawAttribute : PXEntityAttribute
{
  public ProjectTaskRawAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionAttribute("PROTASK")
    {
      ValidComboRequired = false
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }
}
