// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.BusinessDateTimeDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

/// <summary>
/// Allow determine current business date with time part on 'field default' event.
/// </summary>
/// <example>[BusinessDateTimeDefault]</example>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class BusinessDateTimeDefaultAttribute : PXDefaultAttribute
{
  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    DateTime? businessDate = sender.Graph.Accessinfo.BusinessDate;
    ref DateTime? local1 = ref businessDate;
    DateTime? nullable;
    if (!local1.HasValue)
    {
      nullable = new DateTime?();
    }
    else
    {
      DateTime valueOrDefault = local1.GetValueOrDefault();
      ref DateTime local2 = ref valueOrDefault;
      DateTime now = PXTimeZoneInfo.Now;
      int minute = now.Minute;
      now = PXTimeZoneInfo.Now;
      int num1 = now.Hour * 60;
      double num2 = (double) (minute + num1);
      nullable = new DateTime?(local2.AddMinutes(num2));
    }
    // ISSUE: variable of a boxed type
    __Boxed<DateTime?> local3 = (ValueType) nullable;
    defaultingEventArgs.NewValue = (object) local3;
  }
}
