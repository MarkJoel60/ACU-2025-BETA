// Decompiled with JetBrains decompiler
// Type: PX.CS.RMColorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace PX.CS;

public class RMColorAttribute : PXDBStringAttribute
{
  protected static Dictionary<int, Color> _known = new Dictionary<int, Color>(141);

  public RMColorAttribute()
    : base(8)
  {
    this.IsFixed = true;
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!(e.ReturnValue is string))
      return;
    try
    {
      int int32 = Convert.ToInt32((e.ReturnValue as string).Trim(), 16 /*0x10*/);
      if (!RMColorAttribute._known.ContainsKey(int32))
        return;
      string name = RMColorAttribute._known[int32].Name;
      PXLocalizerRepository.SpecialLocalizer.LocalizeColorName(ref name);
      e.ReturnValue = (object) name;
    }
    catch
    {
    }
  }

  public override void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is string)
    {
      try
      {
        Color color = Color.FromName((string) e.NewValue);
        e.NewValue = (object) color.ToArgb().ToString("X8");
      }
      catch
      {
      }
    }
    base.FieldUpdating(sender, e);
  }

  static RMColorAttribute()
  {
    foreach (string colorName in PX.Common.Drawing.GetColorNames())
    {
      Color color = Color.FromName(colorName);
      RMColorAttribute._known[color.ToArgb()] = color;
    }
  }
}
