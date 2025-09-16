// Decompiled with JetBrains decompiler
// Type: PX.Api.PXDBDateWithTicks
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.Api;

internal class PXDBDateWithTicks : PXDBDateAttribute
{
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel == PXAttributeLevel.Item || e.IsAltered)
      e.ReturnState = (object) PXLongState.CreateInstance(e.ReturnState, this._FieldName, new bool?(this._IsKey), new int?(), new long?(long.MinValue), new long?(long.MaxValue), typeof (long));
    if (e.ReturnValue == null || !(e.ReturnValue is System.DateTime))
      return;
    System.DateTime? returnValue = e.ReturnValue as System.DateTime?;
    e.ReturnValue = (object) returnValue?.Ticks;
  }

  public override void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is long?)
    {
      System.DateTime dateTime = new System.DateTime(((long?) e.NewValue).Value);
      e.NewValue = (object) dateTime;
    }
    base.FieldUpdating(sender, e);
  }
}
