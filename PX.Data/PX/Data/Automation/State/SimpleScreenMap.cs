// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.SimpleScreenMap
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Process.Automation.State;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Automation.State;

internal class SimpleScreenMap : IScreenMap
{
  private readonly Dictionary<string, Screen> ScreenStates = new Dictionary<string, Screen>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
  private readonly Dictionary<string, string> GraphScreens = new Dictionary<string, string>();

  public Screen GetByScreen(string screenID)
  {
    Screen screen;
    return !this.ScreenStates.TryGetValue(screenID, out screen) ? (Screen) null : screen;
  }

  public Screen GetByGraph(string graphName)
  {
    string key;
    Screen screen;
    return !this.GraphScreens.TryGetValue(graphName, out key) || !this.ScreenStates.TryGetValue(key, out screen) ? (Screen) null : screen;
  }

  public void AddScreen(string screenId, Screen value)
  {
    this.ScreenStates[screenId] = value;
    string graphTypeByScreenId = PXPageIndexingService.GetGraphTypeByScreenID(screenId);
    if (string.IsNullOrEmpty(graphTypeByScreenId))
      return;
    this.GraphScreens[graphTypeByScreenId] = screenId;
  }

  public void Add(string screenId, string graphType, Screen value)
  {
    this.ScreenStates[screenId] = value;
    this.GraphScreens[graphType] = screenId;
  }

  public bool ContainsGraph(string graphName)
  {
    string key;
    return this.GraphScreens.TryGetValue(graphName, out key) && this.ScreenStates.ContainsKey(key);
  }

  public IEnumerable<ScreenTable> GetAllScreenTables(string tableName)
  {
    foreach (Screen screen in this.ScreenStates.Values)
    {
      ScreenTable allScreenTable;
      if (screen.Tables.TryGetValue(tableName, out allScreenTable))
        yield return allScreenTable;
    }
  }

  public IEnumerable<ScreenTableField> GetAllScreenFields(string tableName, string field)
  {
    foreach (ScreenTable allScreenTable in this.GetAllScreenTables(tableName))
    {
      ScreenTableField allScreenField;
      if (allScreenTable.Fields.TryGetValue(field, out allScreenField))
        yield return allScreenField;
    }
  }
}
