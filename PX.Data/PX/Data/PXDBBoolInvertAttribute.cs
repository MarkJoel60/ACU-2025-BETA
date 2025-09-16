// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBBoolInvertAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Maps a DAC field of <tt>bool?</tt> type to the database column with invertion from UI.
/// </summary>
public class PXDBBoolInvertAttribute : PXDBBoolAttribute
{
  public override void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is bool newValue)
      e.NewValue = (object) !newValue;
    base.FieldUpdating(sender, e);
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if ((e.ReturnValue ?? (object) false) is bool flag)
      e.ReturnValue = (object) !flag;
    base.FieldSelecting(sender, e);
  }
}
