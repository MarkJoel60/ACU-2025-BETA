// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.UpdateWizardSummaryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.MassProcess;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

public class UpdateWizardSummaryAttribute : PXWizardSummaryAttribute
{
  protected virtual List<Tuple<string, object>> CollectValues(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    PXCache<FieldValue> fvcache = GraphHelper.Caches<FieldValue>(sender.Graph);
    return ((PXCache) fvcache).Inserted.OfType<FieldValue>().Where<FieldValue>((Func<FieldValue, bool>) (f => !f.Hidden.GetValueOrDefault() && f.Selected.GetValueOrDefault())).OrderBy<FieldValue, int?>((Func<FieldValue, int?>) (f => f.Order)).Select(field => new
    {
      field = field,
      state = ((PXCache) fvcache).GetStateExt<FieldValue.value>((object) field) as PXFieldState
    }).Where(t => t.state != null).Select(t =>
    {
      string displayName = t.field.DisplayName;
      object obj = (object) PXWizardSummaryAttribute.StateValue(t.state as PXStringState);
      if (obj == null)
      {
        string str = this.StateValue(t.state as PXIntState);
        obj = str != null ? (object) str : t.state.Value;
      }
      return new Tuple<string, object>(displayName, obj);
    }).ToList<Tuple<string, object>>();
  }
}
