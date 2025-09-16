// Decompiled with JetBrains decompiler
// Type: PX.SM.PXUserLoginAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public sealed class PXUserLoginAttribute : PXUIFieldAttribute
{
  public PXUserLoginAttribute() => base.DisplayName = "Login";

  public override string DisplayName
  {
    get => base.DisplayName;
    set
    {
    }
  }

  public override bool Enabled
  {
    get => base.Enabled;
    set
    {
    }
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row is Users row)
    {
      int? source = row.Source;
      int num = 0;
      this._Enabled = source.GetValueOrDefault() == num & source.HasValue && sender.GetStatus((object) row) == PXEntryStatus.Inserted;
    }
    base.FieldSelecting(sender, e);
  }
}
