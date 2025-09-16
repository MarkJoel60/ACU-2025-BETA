// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRNowDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

/// <summary>Set DateTime.Now as default value</summary>
[Obsolete]
public class CRNowDefaultAttribute : PXDefaultAttribute
{
  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    base.FieldDefaulting(sender, e);
    e.NewValue = (object) PXTimeZoneInfo.Now;
  }
}
