// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.TableChangingContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.PushNotifications;

public class TableChangingContext
{
  public IReadOnlyDictionary<string, TableChange> TableChanges { get; set; }

  public IDictionary<System.Type, Tuple<PXCache, PXCommandPreparing, PXRowSelecting>> ChangedDacs { get; set; } = (IDictionary<System.Type, Tuple<PXCache, PXCommandPreparing, PXRowSelecting>>) new Dictionary<System.Type, Tuple<PXCache, PXCommandPreparing, PXRowSelecting>>();

  public Dictionary<System.Type, IEnumerable<string>> RealTableNamesForDac { get; set; } = new Dictionary<System.Type, IEnumerable<string>>();

  private TableChangingContext.CurrentTable _currentTable { get; } = new TableChangingContext.CurrentTable();

  internal List<string> _unchangedRealNames { get; set; }

  internal List<string> DACsToSkip { get; set; } = new List<string>();

  internal bool TrackAllFields { get; set; }

  internal void SetCurrentTable(string table, string currentProjectionChangedTable)
  {
    this._currentTable._previousStack.Push(table);
    this._currentTable._currentProjectionChangedTable.Push(currentProjectionChangedTable);
  }

  internal void RemoveCurrentTable()
  {
    this._currentTable._previousStack.Pop();
    this._currentTable._currentProjectionChangedTable.Pop();
  }

  internal TableChangingContext.CurrentTable GetCurrentTable() => this._currentTable;

  public void AddUnchangedRealName(string tableName)
  {
    if (this._unchangedRealNames == null)
      this._unchangedRealNames = new List<string>();
    this._unchangedRealNames.Add(tableName);
  }

  public bool ShouldSkipTransform()
  {
    return this.DACsToSkip.Any<string>((Func<string, bool>) (table => this._currentTable._previousStack.Contains(table)));
  }

  public bool ShouldSkipTransform(string tableName)
  {
    return this.DACsToSkip.Any<string>((Func<string, bool>) (table => string.Equals(table, tableName, StringComparison.OrdinalIgnoreCase))) || this.DACsToSkip.Any<string>((Func<string, bool>) (table => this._currentTable._previousStack.Contains(table)));
  }

  internal class CurrentTable
  {
    internal Stack<string> _currentProjectionChangedTable = new Stack<string>();

    public CurrentTable() => this._currentProjectionChangedTable.Push((string) null);

    internal Stack<string> _previousStack { get; } = new Stack<string>();

    internal string Current => this._previousStack.Peek();

    public string FirstChangedForProjection => this._currentProjectionChangedTable.Peek();
  }
}
