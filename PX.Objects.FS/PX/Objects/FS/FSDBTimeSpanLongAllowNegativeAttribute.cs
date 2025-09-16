// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSDBTimeSpanLongAllowNegativeAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Text;

#nullable disable
namespace PX.Objects.FS;

public class FSDBTimeSpanLongAllowNegativeAttribute : FSDBTimeSpanLongAttribute
{
  protected string _TimeSpanLongHMNegativeMask = "C#### h ## m";
  protected string _OutputFormatNegative = "{0}{1,4}{2:00}";
  protected int _MaskLength = 7;

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (((PXEventSubscriberAttribute) this)._AttributeLevel == 2 || e.IsAltered)
    {
      string str = PXMessages.LocalizeNoPrefix(this._TimeSpanLongHMNegativeMask);
      e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(this._MaskLength), new bool?(), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(((PXDBFieldAttribute) this)._IsKey), new int?(), string.IsNullOrEmpty(str) ? (string) null : str, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
    }
    if (e.ReturnValue == null)
      return;
    int returnValue = (int) e.ReturnValue;
    string str1 = " ";
    if (returnValue < 0)
    {
      returnValue *= -1;
      str1 = "-";
    }
    TimeSpan timeSpan = new TimeSpan(0, 0, returnValue, 0);
    int num = timeSpan.Days * 24 + timeSpan.Hours;
    e.ReturnValue = (object) string.Format(this._OutputFormatNegative, (object) str1, (object) num, (object) timeSpan.Minutes);
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is string)
    {
      string newValue = (string) e.NewValue;
      int num1 = 1;
      if (newValue[0] == '-')
      {
        num1 = -1;
        e.NewValue = (object) newValue.Replace("-", " ");
      }
      else if (newValue[0] != ' ' && (newValue[0] < '0' || newValue[0] > '9'))
        e.NewValue = (object) newValue.Replace(newValue[0], ' ');
      int length = ((string) e.NewValue).Length;
      if (length < this._MaskLength)
      {
        StringBuilder stringBuilder = new StringBuilder(this._MaskLength);
        for (int index = length; index < this._MaskLength; ++index)
          stringBuilder.Append('0');
        stringBuilder.Append((string) e.NewValue);
        e.NewValue = (object) stringBuilder.ToString();
      }
      int result = 0;
      if (!string.IsNullOrEmpty((string) e.NewValue) && int.TryParse(((string) e.NewValue).Replace(" ", "0"), out result))
      {
        int minutes = result % 100;
        int num2 = (int) new TimeSpan(0, (result - minutes) / 100, minutes, 0).TotalMinutes;
        if (num2 >= 600000)
          num2 = 599999;
        e.NewValue = (object) (num2 * num1);
      }
      else
        e.NewValue = (object) null;
    }
    if (e.NewValue != null)
      return;
    e.NewValue = (object) 0;
  }
}
