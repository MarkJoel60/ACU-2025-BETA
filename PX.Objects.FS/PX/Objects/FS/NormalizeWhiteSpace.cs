// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.NormalizeWhiteSpace
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.FS;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class NormalizeWhiteSpace : PXEventSubscriberAttribute, IPXFieldUpdatingSubscriber
{
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || !(e.NewValue is string) || ((CancelEventArgs) e).Cancel)
      return;
    string newValue = (string) e.NewValue;
    int length = newValue.Length;
    e.NewValue = (object) Regex.Replace(newValue.Trim(), "\\s+", " ").PadRight(length, ' ');
  }
}
