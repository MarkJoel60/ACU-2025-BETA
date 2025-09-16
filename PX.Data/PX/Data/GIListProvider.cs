// Decompiled with JetBrains decompiler
// Type: PX.Data.GIListProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data.Maintenance.GI;
using PX.DbServices.QueryObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace PX.Data;

public class GIListProvider : IListProvider, IChainHandler<IListProvider>
{
  private readonly IPXPageIndexingService _pageIndexingService;
  private const string SLOT_KEY = "GIListProvider.Definition";

  public GIListProvider(IPXPageIndexingService pageIndexingService)
  {
    this._pageIndexingService = pageIndexingService;
  }

  private GIListProvider.Definition Def
  {
    get
    {
      GIListProvider.Definition slot = PXContext.GetSlot<GIListProvider.Definition>("GIListProvider.Definition");
      if (slot == null)
      {
        slot = PXDatabase.GetSlot<GIListProvider.Definition>("GIListProvider.Definition", typeof (ListEntryPoint), typeof (GIDesign), typeof (PX.SM.SiteMap));
        PXContext.SetSlot<GIListProvider.Definition>("GIListProvider.Definition", slot);
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
      GIDesign design = this.Def.Designs[entryScreenID];
      if (design != null)
      {
        string primaryView = this._pageIndexingService.GetPrimaryView(CustomizedTypeManager.GetTypeNotCustomized(graph).FullName);
        PXCache cache = graph.Views[primaryView].Cache;
        object row = cache.Current;
        if (row != null)
        {
          object[] array = cache.Keys.Select<string, object>((System.Func<string, object>) (keyName => cache.GetValue(row, keyName))).ToArray<object>();
          this.SetCurrentSearches(entryScreenID, array);
        }
        throw new PXRedirectToGIRequiredException(design);
      }
    }
  }

  private bool IsListInternal(string screenID)
  {
    return screenID != null && this.Def.Lists.ContainsKey(screenID);
  }

  public bool IsList(string screenID)
  {
    return this.IsListInternal(screenID) || this.Successor.With<IListProvider, bool>((System.Func<IListProvider, bool>) (_ => _.IsList(screenID)));
  }

  private bool HasListInternal(string entryScreenID)
  {
    return entryScreenID != null && this.Def.Designs.ContainsKey(entryScreenID);
  }

  public bool HasList(string entryScreenID)
  {
    return this.HasListInternal(entryScreenID) || this.Successor.With<IListProvider, bool>((System.Func<IListProvider, bool>) (_ => _.HasList(entryScreenID)));
  }

  public string GetListID(string entryScreenID)
  {
    if (this.PassThrough(entryScreenID))
      return this.Successor.With<IListProvider, string>((System.Func<IListProvider, string>) (_ => _.GetListID(entryScreenID)));
    if (entryScreenID == null)
      return (string) null;
    string str;
    return this.Def.EntryScreens.TryGetValue(entryScreenID, out str) ? str : (string) null;
  }

  public string GetEntryScreenID(string listScreenID)
  {
    if (this.PassThrough(listScreenID))
      return this.Successor.With<IListProvider, string>((System.Func<IListProvider, string>) (_ => _.GetEntryScreenID(listScreenID)));
    if (listScreenID == null)
      return (string) null;
    string str;
    return this.Def.Lists.TryGetValue(listScreenID, out str) ? str : (string) null;
  }

  public bool CanAddNewRecord(string listScreenID)
  {
    if (this.IsListInternal(listScreenID))
      return this.Def.Designs[this.GetEntryScreenID(listScreenID)].NewRecordCreationEnabled.GetValueOrDefault();
    IListProvider successor = this.Successor;
    return (successor != null ? (successor.IsList(listScreenID) ? 1 : 0) : 0) != 0 && this.Successor.CanAddNewRecord(listScreenID);
  }

  private string GetSessionKey(string entryScreenID, string recordKey)
  {
    return $"List${recordKey}${entryScreenID}";
  }

  public object[] GetCurrentSearches(string entryScreenID)
  {
    return this.PassThrough(entryScreenID) ? this.Successor.With<IListProvider, object[]>((System.Func<IListProvider, object[]>) (_ => _.GetCurrentSearches(entryScreenID))) : PXContext.Session[this.GetSessionKey(entryScreenID, "SavedSearches")] as object[];
  }

  public void SetCurrentSearches(string entryScreenID, object[] keys)
  {
    if (this.PassThrough(entryScreenID))
      this.Successor.Call<IListProvider>((System.Action<IListProvider>) (_ => _.SetCurrentSearches(entryScreenID, keys)));
    else if (keys == null)
      PXContext.Session.Remove(this.GetSessionKey(entryScreenID, "SavedSearches"));
    else
      PXContext.Session[this.GetSessionKey(entryScreenID, "SavedSearches")] = (object) keys;
  }

  public IListProvider Successor { get; set; }

  private bool PassThrough(string screenID)
  {
    return !this.IsListInternal(screenID) && !this.HasListInternal(screenID);
  }

  /// <exclude />
  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    private readonly Dictionary<string, GIDesign> _designs = new Dictionary<string, GIDesign>();
    private readonly Dictionary<string, string> _lists = new Dictionary<string, string>();
    private readonly Dictionary<string, string> _entryScreens = new Dictionary<string, string>();

    public IReadOnlyDictionary<string, GIDesign> Designs
    {
      get => (IReadOnlyDictionary<string, GIDesign>) this._designs;
    }

    public IReadOnlyDictionary<string, string> Lists
    {
      get => (IReadOnlyDictionary<string, string>) this._lists;
    }

    public IReadOnlyDictionary<string, string> EntryScreens
    {
      get => (IReadOnlyDictionary<string, string>) this._entryScreens;
    }

    public void Prefetch()
    {
      Func<string, System.Type, YaqlCondition> func = (Func<string, System.Type, YaqlCondition>) ((paramName, paramType) => Yaql.like<YaqlScalar>((YaqlScalar) Yaql.column(typeof (PX.SM.SiteMap.url).Name, "SMList"), Yaql.concat(Yaql.constant<string>("~/GenericInquiry/GenericInquiry.aspx", SqlDbType.Variant), new YaqlScalar[3]
      {
        Yaql.constant<string>($"?{paramName}=", SqlDbType.Variant),
        Yaql.convert((YaqlScalar) Yaql.column(paramType.Name, (string) null), SqlDbType.VarChar, -1),
        Yaql.likeWildcard
      })));
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<GIDesign>((IEnumerable<YaqlJoin>) new YaqlJoin[3]
      {
        Yaql.join<PX.SM.SiteMap>("SMList", Yaql.or(func("ID", typeof (GIDesign.designID)), func("Name", typeof (GIDesign.name))), (YaqlJoinType) 0),
        Yaql.join<ListEntryPoint>(Yaql.and(Yaql.eq<ListEntryPoint.listScreenID, PX.SM.SiteMap.screenID>("<declaring_type_name>", "SMList"), Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<ListEntryPoint.entryScreenID>((string) null), Yaql.column("PrimaryScreenIDNew", typeof (GIDesign).Name))), (YaqlJoinType) 0),
        Yaql.join<PX.SM.SiteMap>("SMEntryScreen", Yaql.eq<PX.SM.SiteMap.screenID, ListEntryPoint.entryScreenID>("SMEntryScreen", "<declaring_type_name>"), (YaqlJoinType) 0)
      }, (PXDataField) new PXDataFieldValue<ListEntryPoint.isActive>((object) true), (PXDataField) new PXDataFieldValue("PrimaryScreenIDNew", (object) null, PXComp.ISNOTNULL), (PXDataField) new PXDataField<ListEntryPoint.listScreenID>(), (PXDataField) new PXDataField<ListEntryPoint.entryScreenID>(), (PXDataField) new PXDataField<GIDesign.name>(), (PXDataField) new PXDataField<GIDesign.designID>(), (PXDataField) new PXDataField<GIDesign.newRecordCreationEnabled>()))
      {
        string str1 = pxDataRecord.GetString(0);
        string str2 = pxDataRecord.GetString(1);
        if (!string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase))
        {
          string str3 = pxDataRecord.GetString(2);
          Guid guid = pxDataRecord.GetGuid(3).Value;
          bool? boolean = pxDataRecord.GetBoolean(4);
          GIDesign giDesign = new GIDesign()
          {
            DesignID = new Guid?(guid),
            PrimaryScreenID = str2,
            Name = str3,
            NewRecordCreationEnabled = boolean
          };
          this._designs[str2] = giDesign;
          this._lists[str1] = str2;
          this._entryScreens[str2] = str1;
        }
      }
    }
  }
}
