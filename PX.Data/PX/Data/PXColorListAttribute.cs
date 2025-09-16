// Decompiled with JetBrains decompiler
// Type: PX.Data.PXColorListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

internal class PXColorListAttribute : PXStringListAttribute
{
  public PXColorListAttribute()
    : base(Drawing.GetColorNames(), Drawing.GetColorNames())
  {
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    e.ReturnState = (object) PXColorState.CreateInstance((PXFieldState) e.ReturnState);
    e.ReturnValue = (object) ColorsHelper.GetHexColor((string) e.ReturnValue);
  }
}
