// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Workflow.WorkflowCommonService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Automation.State;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.ProjectDefinition.Workflow;

internal class WorkflowCommonService
{
  private const string ActionMenuName = "Action";
  private const string InquiriesMenuName = "Inquiry";
  private const string ReportsMenuName = "Report";

  public static string FindMenuFolderNameForSpecialMenuType(
    Dictionary<PXSpecialButtonType, Dictionary<string, PXAction>> graphActions,
    ScreenActionBase settings,
    string prevMenuFolder)
  {
    string forSpecialMenuType = (string) null;
    PXSpecialButtonType? nullable = settings.MenuFolderType ?? WorkflowCommonService.GetSpecialButtonType(settings.Category);
    if (nullable.HasValue || !string.IsNullOrEmpty(settings.Category))
    {
      string str = (string) null;
      if (!nullable.HasValue && !string.IsNullOrEmpty(settings.Category))
        str = settings.Category;
      else if (nullable.HasValue)
      {
        switch (nullable.GetValueOrDefault())
        {
          case PXSpecialButtonType.ActionsFolder:
          case PXSpecialButtonType.InquiriesFolder:
          case PXSpecialButtonType.ReportsFolder:
            Dictionary<string, PXAction> dictionary;
            graphActions.TryGetValue(nullable.Value, out dictionary);
            Dictionary<string, PXAction>.KeyCollection keys = dictionary?.Keys;
            if (keys != null)
            {
              str = string.IsNullOrEmpty(settings.MenuFolder) ? keys.FirstOrDefault<string>() : keys.FirstOrDefault<string>((Func<string, bool>) (it => settings.MenuFolder.Equals(it, StringComparison.OrdinalIgnoreCase))) ?? keys.FirstOrDefault<string>();
              break;
            }
            break;
        }
      }
      if (str != null)
        forSpecialMenuType = str;
    }
    if (forSpecialMenuType == null)
    {
      if (nullable.HasValue)
      {
        switch (nullable.GetValueOrDefault())
        {
          case PXSpecialButtonType.ActionsFolder:
            forSpecialMenuType = settings.MenuFolder ?? "Action";
            goto label_16;
          case PXSpecialButtonType.InquiriesFolder:
            forSpecialMenuType = settings.MenuFolder ?? "Inquiry";
            goto label_16;
          case PXSpecialButtonType.ReportsFolder:
            forSpecialMenuType = settings.MenuFolder ?? "Report";
            goto label_16;
        }
      }
      forSpecialMenuType = settings.MenuFolder ?? prevMenuFolder;
    }
label_16:
    return forSpecialMenuType;
  }

  public static Dictionary<PXSpecialButtonType, Dictionary<string, PXAction>> GetSpecialGraphActions(
    PXGraph graph)
  {
    return graph.Actions.Cast<DictionaryEntry>().ToDictionary<DictionaryEntry, string, PXAction>((Func<DictionaryEntry, string>) (it => it.Key.ToString()), (Func<DictionaryEntry, PXAction>) (it => (PXAction) it.Value)).GroupBy<KeyValuePair<string, PXAction>, PXSpecialButtonType?>((Func<KeyValuePair<string, PXAction>, PXSpecialButtonType?>) (it => it.Value.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>()?.SpecialType)).Where<IGrouping<PXSpecialButtonType?, KeyValuePair<string, PXAction>>>((Func<IGrouping<PXSpecialButtonType?, KeyValuePair<string, PXAction>>, bool>) (it =>
    {
      PXSpecialButtonType? key1 = it.Key;
      PXSpecialButtonType specialButtonType1 = PXSpecialButtonType.ActionsFolder;
      if (!(key1.GetValueOrDefault() == specialButtonType1 & key1.HasValue))
      {
        PXSpecialButtonType? key2 = it.Key;
        PXSpecialButtonType specialButtonType2 = PXSpecialButtonType.InquiriesFolder;
        if (!(key2.GetValueOrDefault() == specialButtonType2 & key2.HasValue))
        {
          PXSpecialButtonType? key3 = it.Key;
          PXSpecialButtonType specialButtonType3 = PXSpecialButtonType.ReportsFolder;
          return key3.GetValueOrDefault() == specialButtonType3 & key3.HasValue;
        }
      }
      return true;
    })).ToDictionary<IGrouping<PXSpecialButtonType?, KeyValuePair<string, PXAction>>, PXSpecialButtonType, Dictionary<string, PXAction>>((Func<IGrouping<PXSpecialButtonType?, KeyValuePair<string, PXAction>>, PXSpecialButtonType>) (it => it.Key.Value), (Func<IGrouping<PXSpecialButtonType?, KeyValuePair<string, PXAction>>, Dictionary<string, PXAction>>) (it => it.ToDictionary<KeyValuePair<string, PXAction>, string, PXAction>((Func<KeyValuePair<string, PXAction>, string>) (pair => pair.Key), (Func<KeyValuePair<string, PXAction>, PXAction>) (pair => pair.Value))));
  }

  public static PXSpecialButtonType? GetSpecialButtonType(string name)
  {
    switch (name)
    {
      case "Action":
        return new PXSpecialButtonType?(PXSpecialButtonType.ActionsFolder);
      case "Inquiry":
        return new PXSpecialButtonType?(PXSpecialButtonType.InquiriesFolder);
      case "Report":
        return new PXSpecialButtonType?(PXSpecialButtonType.ReportsFolder);
      default:
        return new PXSpecialButtonType?();
    }
  }

  internal static string GetActionMenuDisplayName(string menuFolder)
  {
    string message;
    switch (menuFolder)
    {
      case "Action":
        message = "Actions";
        break;
      case "Report":
        message = "Reports";
        break;
      case "Inquiry":
        message = "Inquiries";
        break;
      default:
        message = menuFolder;
        break;
    }
    return PXLocalizer.Localize(message);
  }

  internal static string GetActionNameByFolderType(PXSpecialButtonType? menuFolderType)
  {
    if (menuFolderType.HasValue)
    {
      switch (menuFolderType.GetValueOrDefault())
      {
        case PXSpecialButtonType.ActionsFolder:
          return "Action";
        case PXSpecialButtonType.InquiriesFolder:
          return "Inquiry";
        case PXSpecialButtonType.ReportsFolder:
          return "Report";
      }
    }
    return "Action";
  }

  public static bool IsActionDisplayedOnMainToolbar(
    bool? displayOnMainToolBar,
    bool? isLockedOnToolbar,
    string category)
  {
    bool? nullable1 = displayOnMainToolBar;
    bool flag1 = true;
    if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
      return true;
    bool? nullable2 = displayOnMainToolBar;
    bool flag2 = false;
    if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
      return false;
    return string.IsNullOrEmpty(category) || isLockedOnToolbar.GetValueOrDefault();
  }
}
