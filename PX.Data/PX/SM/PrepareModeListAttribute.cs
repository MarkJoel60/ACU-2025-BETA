// Decompiled with JetBrains decompiler
// Type: PX.SM.PrepareModeListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public class PrepareModeListAttribute : PXStringListAttribute
{
  public PrepareModeListAttribute()
    : base(new string[2]{ "ADB", "XML" }, new string[2]
    {
      "Binary",
      "XML"
    })
  {
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!(e.ReturnState is PXFieldState) || !(e.Row is ExportSnapshotSettings))
      return;
    ((PXFieldState) e.ReturnState).Enabled = ((ExportSnapshotSettings) e.Row).Prepare.GetValueOrDefault();
  }
}
