// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTimeListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXTimeListAttribute : PXIntListAttribute
{
  public PXTimeListAttribute()
    : base(PXTimeListAttribute.GetValues(30, 47), PXTimeListAttribute.GetLabels(PXTimeListAttribute.GetValues(30, 47)))
  {
    this.ExclusiveValues = false;
  }

  public PXTimeListAttribute(int step, int count)
    : base(PXTimeListAttribute.GetValues(step, count), PXTimeListAttribute.GetLabels(PXTimeListAttribute.GetValues(step, count)))
  {
    this.ExclusiveValues = false;
  }

  public override bool IsLocalizable => false;

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.Graph.FieldUpdating.AddHandler(sender.GetItemType(), this._FieldName, new PXFieldUpdating(this.FieldUpdating));
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!(e.ReturnState is PXIntState returnState))
      return;
    int[] numArray = returnState.AllowedValues;
    string[] allowedLabels = returnState.AllowedLabels;
    if (e.ReturnValue != null)
    {
      int returnValue = (int) e.ReturnValue;
      if (numArray != null && !((IEnumerable<int>) numArray).Contains<int>(returnValue))
      {
        numArray = ((IEnumerable<int>) EnumerableExtensions.Append<int>(numArray, returnValue)).ToArray<int>();
        allowedLabels = ((IEnumerable<string>) EnumerableExtensions.Append<string>(allowedLabels, PXTimeListAttribute.GetString(returnValue))).ToArray<string>();
      }
    }
    e.ReturnState = (object) PXTimeState.CreateInstance(returnState, numArray, allowedLabels);
  }

  protected void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!sender.Graph.IsImportFromExcel && !sender.Graph.IsImport || e.NewValue is int || string.IsNullOrEmpty((string) e.NewValue) || !(e.NewValue is string newValue) || new Regex("^[0-9]+$", RegexOptions.Compiled | RegexOptions.Singleline).IsMatch(newValue))
      return;
    TimeSpan result1;
    if (TimeSpan.TryParse(newValue, out result1))
    {
      e.NewValue = (object) (int) result1.TotalMinutes;
    }
    else
    {
      System.DateTime result2;
      if (System.DateTime.TryParse(newValue, out result2))
        e.NewValue = (object) (int) result2.TimeOfDay.TotalMinutes;
      else
        throw new PXException("\"{0}\" cannot be converted to Time.", new object[1]
        {
          e.NewValue
        });
    }
  }

  public static int[] GetValues(int step, int count)
  {
    List<int> intList = new List<int>(100);
    for (int index = 0; index <= count; ++index)
      intList.Add(index * step);
    return intList.ToArray();
  }

  public static string[] GetLabels(int[] values)
  {
    List<string> stringList = new List<string>();
    foreach (int totalMinutes in values)
      stringList.Add(PXTimeListAttribute.GetString(totalMinutes));
    return stringList.ToArray();
  }

  public static string GetString(int totalMinutes)
  {
    TimeSpan timeSpan = TimeSpan.FromMinutes((double) System.Math.Abs(totalMinutes));
    return totalMinutes < 0 ? $"-{(int) System.Math.Truncate(timeSpan.TotalHours):d2}:{timeSpan.Minutes:d2}" : $"{(int) System.Math.Truncate(timeSpan.TotalHours):d2}:{timeSpan.Minutes:d2}";
  }
}
