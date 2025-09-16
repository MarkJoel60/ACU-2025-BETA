// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CaseTimeSpentAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Text;

#nullable disable
namespace PX.Objects.CR;

public class CaseTimeSpentAttribute : PXDBTimeSpanLongAttribute
{
  public CaseTimeSpentAttribute() => this.Format = (TimeSpanFormatType) 2;

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is string newValue)
    {
      int length1 = newValue.Length;
      int length2 = this._lengths[(int) this._Format];
      if (length1 < length2)
      {
        StringBuilder stringBuilder = new StringBuilder(length2);
        stringBuilder.Append(newValue);
        for (int index = length1; index < length2; ++index)
          stringBuilder.Append('0');
        e.NewValue = (object) (newValue = stringBuilder.ToString());
      }
      int result;
      if (!string.IsNullOrEmpty(newValue) && int.TryParse(newValue.Replace(" ", "0"), out result))
      {
        int minutes = result % 100;
        int hours = (result - minutes) / 100 % 100;
        int days = ((result - minutes) / 100 - hours) / 100;
        if (this.Format == 2)
        {
          hours = (result - minutes) / 100;
          days = 0;
        }
        TimeSpan timeSpan = new TimeSpan(days, hours, minutes, 0);
        e.NewValue = (object) (int) timeSpan.TotalMinutes;
      }
      else
        e.NewValue = (object) null;
    }
    if (e.NewValue != null || !this._NullIsZero)
      return;
    e.NewValue = (object) 0;
  }
}
