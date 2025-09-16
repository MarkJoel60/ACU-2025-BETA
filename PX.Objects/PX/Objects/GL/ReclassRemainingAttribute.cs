// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ReclassRemainingAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

/// <summary>Returns null field value for zero value.</summary>
public class ReclassRemainingAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    Decimal? returnValue = (Decimal?) e.ReturnValue;
    Decimal num = 0M;
    if (!(returnValue.GetValueOrDefault() == num & returnValue.HasValue))
      return;
    e.ReturnValue = (object) null;
  }
}
