// Decompiled with JetBrains decompiler
// Type: PX.Data.DummyListProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public class DummyListProvider : IListProvider, IChainHandler<IListProvider>
{
  public IListProvider Successor { get; set; }

  public void TryRedirect(PXGraph graph, string entryScreenID)
  {
  }

  public bool IsList(string screenId) => false;

  public bool HasList(string entryScreenID) => false;

  public string GetListID(string entryScreenID) => (string) null;

  public string GetEntryScreenID(string listScreenID) => (string) null;

  public bool CanAddNewRecord(string listScreenID) => false;

  public object[] GetCurrentSearches(string entryScreenID) => (object[]) null;

  public void SetCurrentSearches(string entryScreenID, object[] keys)
  {
  }
}
