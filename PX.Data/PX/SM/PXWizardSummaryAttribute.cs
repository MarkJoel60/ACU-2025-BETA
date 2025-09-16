// Decompiled with JetBrains decompiler
// Type: PX.SM.PXWizardSummaryAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

/// <exclude />
public class PXWizardSummaryAttribute : PXStringAttribute
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.Graph.RowSelected.AddHandler(sender.GetItemType(), new PXRowSelected(this.OnRowSelected));
  }

  protected virtual List<Tuple<string, object>> CollectValues(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    return sender.Fields.Select<string, PXFieldState>((Func<string, PXFieldState>) (field => sender.GetStateExt(e.Row, field) as PXFieldState)).Where<PXFieldState>((Func<PXFieldState, bool>) (state => state != null && state.Enabled && state.Visible)).Select(state =>
    {
      PXFieldState pxFieldState = state;
      object obj = (object) PXWizardSummaryAttribute.StateValue(state as PXStringState);
      if (obj == null)
      {
        string str = this.StateValue(state as PXIntState);
        obj = str != null ? (object) str : state.Value;
      }
      return new{ state = pxFieldState, value = obj };
    }).Where(t => t.value != null).Select(t => new Tuple<string, object>(t.state.DisplayName, t.value)).ToList<Tuple<string, object>>();
  }

  protected virtual void OnRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    sender.SetValue(e.Row, this._FieldName, (object) this.CollectValues(sender, e).Select<Tuple<string, object>, string>((Func<Tuple<string, object>, string>) (tuple => $"{tuple.Item1}: {tuple.Item2}")).JoinToString<string>(Environment.NewLine));
  }

  private static string GetLabel(PXStringState strState, string val)
  {
    int index = Array.IndexOf<string>(strState.AllowedValues, val);
    return index < 0 ? (string) null : strState.AllowedLabels[index];
  }

  protected static string StateValue(PXStringState strState)
  {
    if (strState != null && strState.AllowedValues != null && strState.AllowedValues.Length != 0 && strState.Value != null)
    {
      List<string> stringList = new List<string>();
      string label1 = PXWizardSummaryAttribute.GetLabel(strState, (string) strState.Value);
      if (label1 != null)
        stringList.Add(label1);
      else if ((strState.Value as string).Contains<char>(','))
      {
        string str = strState.Value as string;
        char[] chArray = new char[1]{ ',' };
        foreach (string val in str.Split(chArray))
        {
          string label2 = PXWizardSummaryAttribute.GetLabel(strState, val);
          if (label2 != null)
            stringList.Add(label2);
        }
      }
      if (stringList.Any<string>())
        return stringList.JoinToString<string>(", ");
    }
    return (string) null;
  }

  protected string StateValue(PXIntState strState)
  {
    if (strState != null && strState.AllowedValues != null && strState.AllowedValues.Length != 0 && strState.Value != null)
    {
      int index = Array.IndexOf<int>(strState.AllowedValues, (int) strState.Value);
      if (index >= 0)
        return strState.AllowedLabels[index];
    }
    return (string) null;
  }
}
