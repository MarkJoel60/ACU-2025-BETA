// Decompiled with JetBrains decompiler
// Type: PX.Data.RowConditionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Property)]
public sealed class RowConditionAttribute : PXDBByteAttribute
{
  private static string[] _values;
  private static string[] _labels;

  static RowConditionAttribute()
  {
    IDictionary<object, string> valueNamePairs = PXEnumDescriptionAttribute.GetValueNamePairs(typeof (PXCondition), false);
    RowConditionAttribute._values = new string[valueNamePairs.Count];
    RowConditionAttribute._labels = new string[valueNamePairs.Count];
    int index = 0;
    foreach (KeyValuePair<object, string> keyValuePair in (IEnumerable<KeyValuePair<object, string>>) valueNamePairs)
    {
      RowConditionAttribute._values[index] = keyValuePair.Key.ToString();
      RowConditionAttribute._labels[index] = keyValuePair.Value;
      ++index;
    }
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.ReturnValue != null)
      e.ReturnValue = (object) ((PXCondition) (byte) e.ReturnValue).ToString();
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    string[] allowedLabels = new string[RowConditionAttribute._labels.Length];
    for (int index = 0; index < RowConditionAttribute._labels.Length; ++index)
      allowedLabels[index] = PXLocalizer.Localize(RowConditionAttribute._labels[index], typeof (InfoMessages).FullName);
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), this._FieldName, new bool?(this._IsKey), new int?(), (string) null, RowConditionAttribute._values, allowedLabels, new bool?(), (string) null);
  }

  public override void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || !(e.NewValue is string))
      return;
    e.NewValue = (object) (byte) (PXCondition) Enum.Parse(typeof (PXCondition), (string) e.NewValue);
  }
}
