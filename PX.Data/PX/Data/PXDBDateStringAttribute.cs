// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBDateStringAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDBDateStringAttribute : PXDBDateAttribute
{
  public string DateFormat = "d";

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel == PXAttributeLevel.Item || e.IsAltered)
      e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(10), new bool?(false), this._FieldName, new bool?(this._IsKey), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
    if (e.ReturnValue == null || !(e.ReturnValue is System.DateTime))
      return;
    e.ReturnValue = (object) ((System.DateTime) e.ReturnValue).ToString(this.DateFormat, (IFormatProvider) sender.Graph.Culture);
  }
}
