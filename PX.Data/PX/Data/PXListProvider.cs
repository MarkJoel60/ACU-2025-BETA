// Decompiled with JetBrains decompiler
// Type: PX.Data.PXListProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.QueryObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXListProvider : IListProvider, IChainHandler<IListProvider>
{
  private const string SLOT_KEY = "PXListProvider.Definition";

  private PXListProvider.Definition Def
  {
    get
    {
      PXListProvider.Definition slot = PXContext.GetSlot<PXListProvider.Definition>("PXListProvider.Definition");
      if (slot == null)
      {
        slot = PXDatabase.GetSlot<PXListProvider.Definition>("PXListProvider.Definition", typeof (ListEntryPoint), typeof (PX.SM.SiteMap));
        PXContext.SetSlot<PXListProvider.Definition>("PXListProvider.Definition", slot);
      }
      return slot;
    }
  }

  public void TryRedirect(PXGraph graph, string entryScreenID)
  {
    if (this.PassThrough(entryScreenID))
    {
      this.Successor.Call<IListProvider>((System.Action<IListProvider>) (_ => _.TryRedirect(graph, entryScreenID)));
    }
    else
    {
      string listId = this.GetListID(entryScreenID);
      if (listId == null)
        return;
      PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(listId);
      if (screenIdUnsecure != null)
        throw new PXRedirectToUrlException(screenIdUnsecure.Url, string.Empty);
    }
  }

  private bool IsListInternal(string screenID)
  {
    return screenID != null && this.Def.Lists.ContainsKey(screenID);
  }

  private bool HasListInternal(string entryScreenID)
  {
    return entryScreenID != null && this.Def.EntryScreens.ContainsKey(entryScreenID);
  }

  public bool IsList(string screenID)
  {
    return this.IsListInternal(screenID) || this.Successor.With<IListProvider, bool>((Func<IListProvider, bool>) (_ => _.IsList(screenID)));
  }

  public bool HasList(string entryScreenID)
  {
    return this.HasListInternal(entryScreenID) || this.Successor.With<IListProvider, bool>((Func<IListProvider, bool>) (_ => _.HasList(entryScreenID)));
  }

  public string GetListID(string entryScreenID)
  {
    if (this.PassThrough(entryScreenID))
      return this.Successor.With<IListProvider, string>((Func<IListProvider, string>) (_ => _.GetListID(entryScreenID)));
    if (entryScreenID == null)
      return (string) null;
    string str;
    return this.Def.EntryScreens.TryGetValue(entryScreenID, out str) ? str : (string) null;
  }

  public string GetEntryScreenID(string listScreenID)
  {
    if (this.PassThrough(listScreenID))
      return this.Successor.With<IListProvider, string>((Func<IListProvider, string>) (_ => _.GetEntryScreenID(listScreenID)));
    if (listScreenID == null)
      return (string) null;
    string str;
    return this.Def.Lists.TryGetValue(listScreenID, out str) ? str : (string) null;
  }

  public bool CanAddNewRecord(string listScreenID)
  {
    if (this.Successor.With<IListProvider, bool>((Func<IListProvider, bool>) (_ => _.IsList(listScreenID))))
      return this.Successor.CanAddNewRecord(listScreenID);
    if (this.IsListInternal(listScreenID))
    {
      PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(this.GetEntryScreenID(listScreenID));
      if (screenIdUnsecure.GraphType != null)
      {
        System.Type type = PXBuildManager.GetType(screenIdUnsecure.GraphType, true);
        if (type != (System.Type) null)
        {
          System.Type cacheType = GraphHelper.GetPrimaryCache(screenIdUnsecure.GraphType)?.CacheType;
          if (cacheType != (System.Type) null)
          {
            System.Type specificInsertType = typeof (PXInsert<>).MakeGenericType(cacheType);
            if (((IEnumerable<System.Reflection.FieldInfo>) type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)).Any<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (f => f.FieldType == specificInsertType)))
              return true;
          }
        }
      }
    }
    return false;
  }

  public object[] GetCurrentSearches(string entryScreenID)
  {
    return this.PassThrough(entryScreenID) ? this.Successor.With<IListProvider, object[]>((Func<IListProvider, object[]>) (_ => _.GetCurrentSearches(entryScreenID))) : (object[]) null;
  }

  public void SetCurrentSearches(string entryScreenID, object[] keys)
  {
    if (!this.PassThrough(entryScreenID))
      return;
    this.Successor.Call<IListProvider>((System.Action<IListProvider>) (_ => _.SetCurrentSearches(entryScreenID, keys)));
  }

  private bool PassThrough(string screenID)
  {
    return !this.IsListInternal(screenID) && !this.HasListInternal(screenID);
  }

  public IListProvider Successor { get; set; }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    private readonly Dictionary<string, string> _lists = new Dictionary<string, string>();
    private readonly Dictionary<string, string> _entryScreens = new Dictionary<string, string>();

    public IReadOnlyDictionary<string, string> EntryScreens
    {
      get => (IReadOnlyDictionary<string, string>) this._entryScreens;
    }

    public IReadOnlyDictionary<string, string> Lists
    {
      get => (IReadOnlyDictionary<string, string>) this._lists;
    }

    public void Prefetch()
    {
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<ListEntryPoint>(Yaql.join<PX.SM.SiteMap>("sm1", Yaql.eq<PX.SM.SiteMap.screenID, ListEntryPoint.entryScreenID>("sm1", "<declaring_type_name>"), (YaqlJoinType) 0), Yaql.join<PX.SM.SiteMap>("sm2", Yaql.eq<PX.SM.SiteMap.screenID, ListEntryPoint.listScreenID>("sm2", "<declaring_type_name>"), (YaqlJoinType) 0), (PXDataField) new PXDataFieldValue<ListEntryPoint.isActive>((object) true), (PXDataField) new PXDataField<PX.SM.SiteMap.screenID>("sm1"), (PXDataField) new PXDataField<PX.SM.SiteMap.screenID>("sm2")))
      {
        string str1 = pxDataRecord.GetString(0);
        string str2 = pxDataRecord.GetString(1);
        if (!string.Equals(str2, str1, StringComparison.OrdinalIgnoreCase))
        {
          this._entryScreens[str1] = str2;
          this._lists[str2] = str1;
        }
      }
    }
  }
}
