// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.Automation.State.ScreenTableField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.WorkflowAPI;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Process.Automation.State;

internal sealed class ScreenTableField
{
  public ScreenTableField(
    string fieldName,
    string displayName,
    bool? isRequired,
    string requiredCondition,
    string disableCondition,
    string hideCondition,
    string comboBoxValues,
    bool isFromSchema,
    string defaultValue)
  {
    this.FieldName = fieldName ?? throw new ArgumentNullException(nameof (fieldName));
    this.DisplayName = displayName;
    this.IsRequired = isRequired;
    this.RequiredCondition = requiredCondition;
    this.DisableCondition = disableCondition;
    this.HideCondition = hideCondition;
    if (!string.IsNullOrEmpty(comboBoxValues))
    {
      IEnumerable<IGrouping<string, ComboBoxItemsModification>> source = ((IEnumerable<string>) comboBoxValues.Split(';')).Select<string, string[]>((Func<string, string[]>) (it => it.Split('|'))).Select<string[], ComboBoxItemsModification>((Func<string[], ComboBoxItemsModification>) (it => new ComboBoxItemsModification()
      {
        Action = (ComboBoxItemsModificationAction) Enum.Parse(typeof (ComboBoxItemsModificationAction), it[0]),
        ID = it[1],
        Description = it[2]
      })).GroupBy<ComboBoxItemsModification, string>((Func<ComboBoxItemsModification, string>) (it => it.ID));
      EnumerableExtensions.ForEach<IGrouping<string, ComboBoxItemsModification>>(source, (System.Action<IGrouping<string, ComboBoxItemsModification>>) (it =>
      {
        if (it.Count<ComboBoxItemsModification>() <= 1)
          return;
        PXTrace.WriteError($"Duplicate combo box value definition {it.Key} in field {fieldName}.");
      }));
      this.ComboBoxValues = source.ToDictionary<IGrouping<string, ComboBoxItemsModification>, string, ComboBoxItemsModification>((Func<IGrouping<string, ComboBoxItemsModification>, string>) (it => it.Key), (Func<IGrouping<string, ComboBoxItemsModification>, ComboBoxItemsModification>) (it => it.OrderByDescending<ComboBoxItemsModification, ComboBoxItemsModificationAction>((Func<ComboBoxItemsModification, ComboBoxItemsModificationAction>) (p => p.Action)).First<ComboBoxItemsModification>()));
    }
    this.IsFromSchema = isFromSchema;
    this.DefaultValue = defaultValue;
  }

  public string FieldName { get; }

  public string DisplayName { get; }

  public bool? IsRequired { get; }

  public string RequiredCondition { get; }

  public string DisableCondition { get; }

  public string HideCondition { get; }

  public Dictionary<string, ComboBoxItemsModification> ComboBoxValues { get; }

  public bool IsFromSchema { get; }

  public string DefaultValue { get; }
}
