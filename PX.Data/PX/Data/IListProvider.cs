// Decompiled with JetBrains decompiler
// Type: PX.Data.IListProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public interface IListProvider : IChainHandler<IListProvider>
{
  /// <summary>
  /// Redirects to the List screen for provided entry screen.
  /// </summary>
  void TryRedirect(PXGraph graph, string entryScreenID);

  bool IsList(string screenId);

  bool HasList(string entryScreenID);

  string GetListID(string entryScreenID);

  string GetEntryScreenID(string listScreenID);

  bool CanAddNewRecord(string listScreenID);

  /// <summary>
  /// Returns key's values for current record in the Entry Screen.
  /// </summary>
  object[] GetCurrentSearches(string entryScreenID);

  /// <summary>
  /// Saves key's values for current record in the Entry Screen.
  /// </summary>
  void SetCurrentSearches(string entryScreenID, object[] keys);
}
