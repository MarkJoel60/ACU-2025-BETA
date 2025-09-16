// Decompiled with JetBrains decompiler
// Type: PX.CS.RMShiftCodeStringAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.CS;

public class RMShiftCodeStringAttribute(int length) : PXStringAttribute(length)
{
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!(e.ReturnValue is string))
      return;
    e.ReturnValue = (object) ((string) e.ReturnValue).Trim();
  }

  public override void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    base.FieldUpdating(sender, e);
    if (!(e.NewValue is string) || ((string) e.NewValue).Length >= 3)
      return;
    e.NewValue = (object) ("   " + (string) e.NewValue).Substring(((string) e.NewValue).Length, 3);
  }
}
