// Decompiled with JetBrains decompiler
// Type: PX.SM.SMNotificationMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.Automation;
using PX.Data.BQL;
using PX.Data.Description;
using PX.Data.Description.GI;
using PX.Data.Maintenance.GI;
using PX.Data.Maintenance.SM;
using PX.Data.Reports;
using PX.Data.Wiki.Parser.BlockParsers;
using PX.Metadata;
using PX.PushNotifications.SourceProcessors;
using PX.Reports;
using PX.Reports.Data;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable enable
namespace PX.SM;

public class SMNotificationMaint : PXGraph<
#nullable disable
SMNotificationMaint>, IEntityItemsService
{
  private const string PrevPostfix = "_prev";
  private const string EntityKey = "Entity";
  private const string UsersKey = "Users";
  internal const string ActionMenuSplitter = "->";
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (Notification.body)})]
  public PXSelect<Notification> Notifications;
  public PXSelectReadonly<Notification> NotificationsRO;
  public PXSelect<Notification, Where<Notification.notificationID, Equal<Required<Notification.notificationID>>>> CurrentNotifications;
  public PXSelect<CacheEntityItem, Where<CacheEntityItem.path, Equal<CacheEntityItem.path>>, OrderBy<Asc<CacheEntityItem.number>>> EntityItems;
  public PXSelect<CacheEntityItem, Where<CacheEntityItem.path, Equal<CacheEntityItem.path>>, OrderBy<Asc<CacheEntityItem.number>>> PreviousEntityItems;
  public PXSelect<CacheEntityItem, Where<CacheEntityItem.path, Equal<CacheEntityItem.path>>, OrderBy<Asc<CacheEntityItem.number>>> ScreenEmailItems;
  public PXSelectOrderBy<SMNotificationMaint.UserEmail, OrderBy<Asc<Users.username>>> UserEmailItems;
  [PXHidden]
  public PXSelect<Notification> NotificationTemplate;
  [PXHidden]
  public PXSelect<Notification> Message;
  [PXHidden]
  public PXSelect<Notification> PublishedNotification;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (Notification.body)})]
  public PXSelect<Notification, Where<Notification.notificationID, Equal<Current<Notification.notificationID>>>> CurrentNotification;
  public PXSelect<NotificationReport, Where<NotificationReport.notificationID, Equal<Current<Notification.notificationID>>>, OrderBy<Asc<NotificationReport.createdDateTime>>> NotificationReports;
  [PXHidden]
  public PXSelect<NotificationReport, Where<NotificationReport.reportID, Equal<Current<NotificationReport.reportID>>>> CurrentNotificationReport;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (NotificationReportParameter.fromDB)})]
  public PXSelect<NotificationReportParameter, Where<NotificationReportParameter.reportID, Equal<Current<NotificationReport.reportID>>>> NotificationReportParameters;
  public PXSave<Notification> Save;
  public PXCancel<Notification> Cancel;
  public PXInsert<Notification> Insert;
  public PXDelete<Notification> Delete;
  public PXCopyPasteAction<Notification> CopyPaste;
  public PXFirst<Notification> First;
  public PXPrevious<Notification> Prev;
  public PXNext<Notification> Next;
  public PXLast<Notification> Last;
  private Tuple<PXGraph, string, PXSiteMap.ScreenInfo> _graph;
  private const char _keySeparator = '-';
  private const char _valueSeparator = '|';
  public const char ViewNameSeparator = '_';
  public PXAction<Notification> RedirectToScreen;

  [InjectDependency]
  private IWorkflowService _workflowService { get; set; }

  public static string GetPrevName(string name)
  {
    return $"{PXLocalizer.Localize("[Previous]", typeof (Messages).FullName)} {name}";
  }

  public static string GetPrevKey(string key) => key + "_prev";

  public static bool IsPrevKey(string key) => key != null && key.EndsWith("_prev");

  public static string ConvertFromPrevKey(string key)
  {
    return key.Substring(0, key.Length - "_prev".Length);
  }

  [InjectDependency]
  protected internal IDataScreenFactory _dataScreenFactory { get; set; }

  [InjectDependency]
  protected internal IScreenInfoProvider _screenInfoProvider { get; set; }

  [InjectDependency]
  private protected IReportCachingService ReportCachingService { get; private set; }

  [InjectDependency]
  private IPXPageIndexingService PageIndexingService { get; set; }

  [InjectDependency]
  protected SettingsProvider SettingsProvider { get; private set; }

  public PXSelectBase<CacheEntityItem> EntityItemsSelect
  {
    get => (PXSelectBase<CacheEntityItem>) this.EntityItems;
  }

  public SMNotificationMaint()
  {
    PXUIFieldAttribute.SetVisible<Notification.localeName>(this.Notifications.Cache, (object) null, PXDBLocalizableStringAttribute.HasMultipleLocales);
    PXSiteMapNodeSelectorAttribute.SetRestriction<NotificationReport.screenID>(this.NotificationReports.Cache, (object) null, (Func<SiteMap, bool>) (s => !SiteMapRestrictionsTypes.IsReport(s)));
    PXSiteMapNodeSelectorAttribute.SetRestriction<Notification.screenID>(this.Notifications.Cache, (object) null, (Func<SiteMap, bool>) (s => SiteMapRestrictionsTypes.IsReport(s)));
    this.NotificationReportParameters.AllowDelete = false;
    this.NotificationReportParameters.AllowInsert = false;
  }

  [PXDependToCache(new System.Type[] {typeof (NotificationReport)})]
  public IEnumerable notificationReportParameters()
  {
    NotificationReport currentReport = this.NotificationReports.Current;
    if (currentReport?.ScreenID == null)
      return (IEnumerable) Enumerable.Empty<NotificationReportParameter>();
    HashSet<string> parameterNames = new HashSet<string>();
    List<NotificationReportParameter> notificationReportParameterList = new List<NotificationReportParameter>();
    PXView pxView = new PXView((PXGraph) this, true, this.NotificationReportParameters.View.BqlSelect);
    IEnumerable<NotificationReportParameter> source = this.NotificationReportParameters.Cache.Cached.Cast<NotificationReportParameter>().Where<NotificationReportParameter>((Func<NotificationReportParameter, bool>) (x =>
    {
      Guid? reportId1 = x.ReportID;
      Guid? reportId2 = currentReport.ReportID;
      if (reportId1.HasValue != reportId2.HasValue)
        return false;
      return !reportId1.HasValue || reportId1.GetValueOrDefault() == reportId2.GetValueOrDefault();
    }));
    List<NotificationReportParameter> list = source.Where<NotificationReportParameter>((Func<NotificationReportParameter, bool>) (x => x.ScreenID == currentReport.ScreenID || x.ScreenID == null)).ToList<NotificationReportParameter>();
    if (list.Any<NotificationReportParameter>())
      return (IEnumerable) list;
    if (!source.Any<NotificationReportParameter>())
    {
      foreach (NotificationReportParameter row in pxView.SelectMulti().Cast<NotificationReportParameter>())
      {
        row.IsOverride = new bool?(true);
        row.FromDB = new bool?(true);
        parameterNames.Add(row.Name);
        this.NotificationReportParameters.Cache.Hold((object) row);
        notificationReportParameterList.Add(row);
      }
    }
    PXStringState stateExt = this.NotificationReportParameters.Cache.GetStateExt<NotificationReportParameter.name>((object) null) as PXStringState;
    if (stateExt.AllowedValues == null)
      return (IEnumerable) notificationReportParameterList;
    foreach (string str in ((IEnumerable<string>) stateExt.AllowedValues).Where<string>((Func<string, bool>) (x => !parameterNames.Contains(x))))
    {
      NotificationReportParameter row = new NotificationReportParameter()
      {
        ReportID = currentReport.ReportID,
        Name = str,
        IsOverride = new bool?(false),
        FromSchema = new bool?(true),
        FromDB = new bool?(false)
      };
      this.NotificationReportParameters.Cache.Hold((object) row);
      notificationReportParameterList.Add(row);
    }
    notificationReportParameterList.ForEach((System.Action<NotificationReportParameter>) (x => x.ScreenID = currentReport.ScreenID));
    return (IEnumerable) notificationReportParameterList;
  }

  [PXInternalUseOnly]
  public IEnumerable entityItems(string parent)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return SMNotificationMaint.GetEntityItemsImpl(parent, this.Notifications.Current?.ScreenID, (PXGraph) this, SMNotificationMaint.\u003C\u003EO.\u003C0\u003E__EveryEntityItemsFilter ?? (SMNotificationMaint.\u003C\u003EO.\u003C0\u003E__EveryEntityItemsFilter = new Func<string, string, CacheEntityItem, CacheEntityItem>(SMNotificationMaint.EveryEntityItemsFilter)), this._workflowService);
  }

  internal static CacheEntityItem EveryEntityItemsFilter(
    string parent,
    string primaryView,
    CacheEntityItem entry)
  {
    return entry;
  }

  internal static CacheEntityItem PreviousEntityItemsFilter(
    string parent,
    string primaryView,
    CacheEntityItem entry)
  {
    return (parent != null || !entry.Key.OrdinalEquals(primaryView)) && !parent.OrdinalEquals(primaryView) ? (CacheEntityItem) null : entry;
  }

  private static IEnumerable<CacheEntityItem> GetGeneralInfoItems(
    string parent,
    PXGraph graph,
    int number = 0)
  {
    if (parent == null)
      return (IEnumerable<CacheEntityItem>) new \u003C\u003Ez__ReadOnlyArray<CacheEntityItem>(new CacheEntityItem[1]
      {
        new CacheEntityItem()
        {
          Key = "GeneralInfo",
          Name = "General Info",
          Number = new int?(number),
          Icon = Sprite.Tree.GetFullUrl("Folder")
        }
      });
    return "GeneralInfo".Equals(parent, StringComparison.Ordinal) ? EMailSourceHelper.GetGeneralInfoFields(graph, parent) : (IEnumerable<CacheEntityItem>) Array.Empty<CacheEntityItem>();
  }

  internal static IEnumerable GetEntityItemsImpl(
    string parent,
    string screenID,
    PXGraph graph,
    Func<string, string, CacheEntityItem, CacheEntityItem> transFormAndFilterEntries,
    IWorkflowService workflowService,
    Dictionary<string, HashSet<string>> allowedItems = null,
    bool addPrevious = false)
  {
    if (screenID == null)
      return (IEnumerable) SMNotificationMaint.GetGeneralInfoItems(parent, graph);
    List<CacheEntityItem> entityItemsImpl = new List<CacheEntityItem>();
    List<CacheEntityItem> prevItems = new List<CacheEntityItem>();
    bool flag = SMNotificationMaint.IsPrevKey(parent);
    if (flag)
      parent = SMNotificationMaint.ConvertFromPrevKey(parent);
    addPrevious = addPrevious && parent == null | flag;
    PXSiteMapNode screenNode;
    if (SMNotificationMaint.IsGIScreen(screenID, out screenNode))
    {
      foreach (CacheEntityItem giScreenDataField in SMNotificationMaint.GetGIScreenDataFields(parent, screenID))
      {
        if (addPrevious || !SMNotificationMaint.EntityItemIsNotAllowed(giScreenDataField, allowedItems))
        {
          entityItemsImpl.Add(giScreenDataField);
          if (addPrevious && !SMNotificationMaint.EntityItemIsNotAllowed(giScreenDataField, allowedItems))
            AddPrevItem(giScreenDataField);
        }
      }
    }
    else if (screenNode != null)
    {
      string entryScreenId = PXList.Provider.GetEntryScreenID(screenNode.ScreenID);
      if (entryScreenId != null)
        screenNode = PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(entryScreenId);
      if (screenNode != null)
      {
        string primaryView = PXPageIndexingService.GetPrimaryView(screenNode.GraphType);
        foreach (CacheEntityItem entry in EMailSourceHelper.TemplateEntity(graph, parent, (string) null, screenNode.GraphType, true, true, true, workflowService).OfType<CacheEntityItem>().Select<CacheEntityItem, CacheEntityItem>((Func<CacheEntityItem, CacheEntityItem>) (c => transFormAndFilterEntries(parent, primaryView, c))).Where<CacheEntityItem>((Func<CacheEntityItem, bool>) (c => c != null)))
        {
          if (!flag || !SMNotificationMaint.EntityItemIsNotAllowed(entry, allowedItems))
          {
            if (!flag)
              entityItemsImpl.Add(entry);
            if (addPrevious && !SMNotificationMaint.EntityItemIsNotAllowed(entry, allowedItems))
            {
              CacheEntityItem cacheEntityItem = SMNotificationMaint.PreviousEntityItemsFilter(parent, primaryView, entry);
              if (cacheEntityItem != null)
                AddPrevItem(cacheEntityItem);
            }
          }
        }
        if (flag)
        {
          foreach (CacheEntityItem selectorEmailItem in SMNotificationMaint.GetPrevSelectorEmailItems(parent, primaryView, screenNode.GraphType))
            AddPrevItem(selectorEmailItem);
        }
      }
    }
    int? nullable1 = entityItemsImpl.Count > 0 ? entityItemsImpl[entityItemsImpl.Count - 1].Number : new int?(0);
    foreach (CacheEntityItem cacheEntityItem1 in prevItems)
    {
      CacheEntityItem cacheEntityItem2 = cacheEntityItem1;
      int? nullable2 = nullable1;
      int? nullable3;
      nullable1 = nullable3 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?();
      cacheEntityItem2.Number = nullable3;
      entityItemsImpl.Add(cacheEntityItem1);
    }
    return (IEnumerable) entityItemsImpl;

    void AddPrevItem(CacheEntityItem item)
    {
      prevItems.Add(new CacheEntityItem()
      {
        Key = SMNotificationMaint.GetPrevKey(item.Key),
        SubKey = item.SubKey,
        Path = item.Path == null ? (string) null : PreviousValueHelper.GetPrevFunctionInvocationTextForPath(item.Path),
        Name = parent == null ? SMNotificationMaint.GetPrevName(item.Name) : item.Name,
        Icon = item.Icon
      });
    }
  }

  private static IEnumerable<CacheEntityItem> GetPrevSelectorEmailItems(
    string parent,
    string primaryView,
    string graphType)
  {
    string[] strArray = parent.Split(new char[1]{ '.' }, StringSplitOptions.RemoveEmptyEntries);
    if (strArray.Length != 2)
      return Enumerable.Empty<CacheEntityItem>();
    string str = strArray[0];
    string fieldName = strArray[1];
    if (!str.Equals(primaryView, StringComparison.OrdinalIgnoreCase))
      return Enumerable.Empty<CacheEntityItem>();
    PXGraph instance = PXGraph.CreateInstance(SyImportProcessor.GetGraphType(graphType));
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    List<System.Type> listoftables = new List<System.Type>();
    Dictionary<string, string> currentScreenFields = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
      {
        fieldName,
        PushNotificationsProcessorHelper.FormFieldKey(primaryView, fieldName)
      }
    };
    SMNotificationMaint.EnumEmailFieldsImpl((string) null, "!", instance.PrimaryItemType, dictionary, listoftables, instance, currentScreenFields);
    return dictionary.Select<KeyValuePair<string, string>, CacheEntityItem>((Func<KeyValuePair<string, string>, CacheEntityItem>) (f => new CacheEntityItem()
    {
      Name = f.Value.Substring(3),
      Key = f.Key,
      Path = $"(({f.Key}))"
    }));
  }

  private static IEnumerable<CacheEntityItem> GetGIScreenDataFields(string parent, string screenID)
  {
    PXGenericInqGrph geninqgraph = PXGenericInqGrph.CreateInstance(screenID);
    int i = 0;
    foreach (IGrouping<string, GIResult> grouping in geninqgraph.ResultColumns.Where<GIResult>((Func<GIResult, bool>) (c => parent == null)).GroupBy<GIResult, string>((Func<GIResult, string>) (c => c.ObjectName)))
      yield return new CacheEntityItem()
      {
        Name = grouping.Key ?? "",
        Key = grouping.Key ?? "",
        SubKey = grouping.Key ?? "",
        Path = $"(({grouping.Key}))",
        Number = new int?(i++),
        Icon = Sprite.Tree.GetFullUrl("Folder")
      };
    foreach (GIResult giResult in geninqgraph.ResultColumns.Where<GIResult>((Func<GIResult, bool>) (c => c.ObjectName == parent)))
    {
      bool flag = giResult.Field.StartsWith("=") && !string.IsNullOrEmpty(giResult.Caption);
      yield return new CacheEntityItem()
      {
        Name = (flag ? giResult.Caption : giResult.Field) ?? "",
        Key = $"{giResult.ObjectName}.{giResult.Field}",
        SubKey = (flag ? giResult.Caption : giResult.Field) ?? "",
        Path = $"(({giResult.FieldName}))",
        Number = new int?(0),
        Icon = Sprite.Tree.GetFullUrl("Field")
      };
    }
    foreach (CacheEntityItem generalInfoItem in SMNotificationMaint.GetGeneralInfoItems(parent, (PXGraph) geninqgraph, i))
      yield return generalInfoItem;
  }

  [PXInternalUseOnly]
  public virtual Dictionary<string, HashSet<string>> GetAllowedItems()
  {
    return (Dictionary<string, HashSet<string>>) null;
  }

  protected IEnumerable screenEmailItems(string parent)
  {
    SMNotificationMaint graph = this;
    if (graph.Notifications?.Current?.ScreenID == null)
    {
      if (parent == null)
        yield return (object) new CacheEntityItem()
        {
          Key = "GeneralInfo",
          Name = "General Info",
          Number = new int?(0)
        };
      else if ("GeneralInfo".Equals(parent, StringComparison.Ordinal))
      {
        foreach (object generalInfoField in EMailSourceHelper.GetGeneralInfoFields((PXGraph) graph, parent))
          yield return generalInfoField;
      }
    }
    else if (parent == null)
    {
      yield return (object) new CacheEntityItem()
      {
        Key = "Entity",
        Name = "Entity",
        Number = new int?(0)
      };
    }
    else
    {
      PXSiteMapNode screenNode;
      bool flag = SMNotificationMaint.IsGIScreen(graph.Notifications.Current.ScreenID, out screenNode);
      if (screenNode != null)
      {
        Dictionary<string, HashSet<string>> allowedItems = graph.GetAllowedItems();
        if (flag)
        {
          if (parent.OrdinalEquals("GeneralInfo"))
          {
            foreach (object generalInfoField in EMailSourceHelper.GetGeneralInfoFields((PXGraph) graph, parent))
              yield return generalInfoField;
            yield break;
          }
          if (!parent.OrdinalEquals("Entity"))
            yield break;
          int i = 0;
          foreach (CacheEntityItem cacheEntityItem in graph.GetAllEmailFieldsForGI().Select<KeyValuePair<string, string>, CacheEntityItem>((Func<KeyValuePair<string, string>, CacheEntityItem>) (c => new CacheEntityItem()
          {
            Name = c.Value,
            Key = c.Key,
            Number = new int?(i++),
            Path = $"(({c.Key}))"
          })).ToList<CacheEntityItem>())
          {
            CacheEntityItem item = cacheEntityItem;
            yield return (object) item;
            if (!SMNotificationMaint.EmailFieldItemIsNotAllowed(item, allowedItems))
            {
              yield return (object) new CacheEntityItem()
              {
                Name = SMNotificationMaint.GetPrevName(item.Name),
                Key = SMNotificationMaint.GetPrevKey(item.Key),
                Number = new int?(i++),
                Path = PreviousValueHelper.GetPrevFunctionInvocationTextForPath(item.Path)
              };
              item = (CacheEntityItem) null;
            }
          }
          yield return (object) new CacheEntityItem()
          {
            Key = "GeneralInfo",
            Name = "General Info",
            Number = new int?(i)
          };
        }
        else
        {
          foreach (object obj in SMNotificationMaint.GetEntityItemsImpl(parent.OrdinalEquals("Entity") ? (string) null : parent, graph.Notifications.Current?.ScreenID, (PXGraph) graph, new Func<string, string, CacheEntityItem, CacheEntityItem>(TransFormAndFilterEntries), graph._workflowService, allowedItems, true))
            yield return obj;
        }
        allowedItems = (Dictionary<string, HashSet<string>>) null;
      }
    }

    static CacheEntityItem TransFormAndFilterEntries(
      string par,
      string primaryView,
      CacheEntityItem entry)
    {
      return entry;
    }
  }

  private Dictionary<string, string> GetAllEmailFieldsForGI()
  {
    Dictionary<string, string> names = new Dictionary<string, string>();
    List<System.Type> listoftables = new List<System.Type>();
    PXGenericInqGrph instance = PXGenericInqGrph.CreateInstance(this.Notifications.Current.ScreenID);
    PXQueryDescription queryDescription = instance.BaseQueryDescription;
    foreach (IGrouping<string, GIResult> source in instance.ResultColumns.GroupBy<GIResult, string>((Func<GIResult, string>) (c => c.ObjectName)))
    {
      PXTable pxTable;
      if (queryDescription.UsedTables.TryGetValue(source.Key, out pxTable))
      {
        Dictionary<string, string> dictionary = source.Select<GIResult, KeyValuePair<string, string>>((Func<GIResult, KeyValuePair<string, string>>) (c => new KeyValuePair<string, string>(c.Field.ToLower(), c.FieldName))).ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (c => c.Key), (Func<KeyValuePair<string, string>, string>) (c => c.Value), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        this.EnumEmailFields((string) null, (string) null, pxTable.CacheType, names, listoftables, (PXGraph) instance, dictionary);
      }
    }
    return names;
  }

  private static void EnumEmailFieldsImpl(
    string path,
    string displayPath,
    System.Type table,
    Dictionary<string, string> names,
    List<System.Type> listoftables,
    PXGraph graph,
    Dictionary<string, string> currentScreenFields)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    TemplateGraphHelper.EnumAssigneeFields(path, displayPath, graph, table, listoftables, names, currentScreenFields, SMNotificationMaint.\u003C\u003EO.\u003C1\u003E__GetEMailFields ?? (SMNotificationMaint.\u003C\u003EO.\u003C1\u003E__GetEMailFields = new Func<System.Type, IEnumerable<string>>(PXDBEmailAttribute.GetEMailFields)));
  }

  protected virtual void EnumEmailFields(
    string path,
    string displayPath,
    System.Type table,
    Dictionary<string, string> names,
    List<System.Type> listoftables,
    PXGraph graph,
    Dictionary<string, string> currentScreenFields)
  {
    SMNotificationMaint.EnumEmailFieldsImpl(path, displayPath, table, names, listoftables, graph, currentScreenFields);
  }

  internal static bool EntityItemIsNotAllowed(
    CacheEntityItem item,
    Dictionary<string, HashSet<string>> allowedItems)
  {
    // ISSUE: explicit non-virtual call
    if (allowedItems != null && __nonvirtual (allowedItems.Count) > 0)
    {
      string tableName;
      string fieldName;
      if (SMNotificationMaint.TryGetTableAndFieldName(item, out tableName, out fieldName))
      {
        HashSet<string> stringSet;
        if (!allowedItems.TryGetValue(tableName, out stringSet) || !stringSet.Contains(fieldName))
          return true;
      }
      else if (!allowedItems.TryGetValue(item.Key, out HashSet<string> _))
        return true;
    }
    return false;
  }

  private static bool EmailFieldItemIsNotAllowed(
    CacheEntityItem item,
    Dictionary<string, HashSet<string>> allowedItems)
  {
    if (allowedItems == null || allowedItems.Count == 0)
      return false;
    string key1 = item.Key;
    int length1 = key1.IndexOf("!");
    if (length1 != -1)
      key1 = key1.Substring(0, length1);
    int length2 = key1.IndexOf('_');
    if (length2 == -1)
      return !allowedItems.TryGetValue(key1, out HashSet<string> _);
    string key2 = key1.Substring(0, length2);
    string str = key1.Substring(length2 + 1);
    HashSet<string> stringSet;
    return !allowedItems.TryGetValue(key2, out stringSet) || !stringSet.Contains(str);
  }

  private static bool TryGetTableAndFieldName(
    CacheEntityItem item,
    out string tableName,
    out string fieldName)
  {
    int length = item.Key.IndexOf(".");
    if (length != -1)
    {
      tableName = item.Key.Substring(0, length);
      fieldName = item.Key.Substring(length + 1);
      return true;
    }
    tableName = (string) null;
    fieldName = (string) null;
    return false;
  }

  [PXButton(ImageKey = "WebN", ImageSet = "control")]
  public virtual IEnumerable redirectToScreen(PXAdapter adapter)
  {
    if (this.Notifications.Current?.ScreenID != null)
    {
      PXRedirectByScreenIDException screenIdException = new PXRedirectByScreenIDException(this.Notifications.Current?.ScreenID, PXBaseRedirectException.WindowMode.New);
      screenIdException.SuppressFrameset = true;
      throw screenIdException;
    }
    return adapter.Get();
  }

  private void EnsureGraph(string screenId)
  {
    if (screenId != this._graph?.Item2)
      this._graph = (Tuple<PXGraph, string, PXSiteMap.ScreenInfo>) null;
    if (string.IsNullOrEmpty(screenId))
      return;
    if (this._graph != null)
      return;
    try
    {
      DataScreenBase dataScreen = this._dataScreenFactory.CreateDataScreen(screenId);
      PXSiteMap.ScreenInfo screenInfo = this._screenInfoProvider.TryGet(screenId);
      this._graph = Tuple.Create<PXGraph, string, PXSiteMap.ScreenInfo>(dataScreen?.DataGraph, screenId, screenInfo);
    }
    catch (Exception ex)
    {
      this._graph = Tuple.Create<PXGraph, string, PXSiteMap.ScreenInfo>((PXGraph) null, screenId, (PXSiteMap.ScreenInfo) null);
      this.Notifications.Cache.RaiseExceptionHandling<Notification.screenID>(this.Notifications.Cache.Current, (object) screenId, (Exception) new PXSetPropertyException<Notification.screenID>(ex.Message));
    }
  }

  public override void Persist()
  {
    PXCache cache = this.NotificationReportParameters.Cache;
    foreach (NotificationReportParameter notificationReportParameter1 in cache.Cached)
    {
      bool? nullable = notificationReportParameter1.IsOverride;
      bool flag1 = true;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = notificationReportParameter1.FromDB;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          cache.SetStatus((object) notificationReportParameter1, PXEntryStatus.Inserted);
          continue;
        }
      }
      nullable = notificationReportParameter1.IsOverride;
      bool flag3 = false;
      if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
      {
        PXCache pxCache = cache;
        NotificationReportParameter notificationReportParameter2 = notificationReportParameter1;
        nullable = notificationReportParameter1.FromDB;
        bool flag4 = true;
        int status = nullable.GetValueOrDefault() == flag4 & nullable.HasValue ? 3 : 0;
        pxCache.SetStatus((object) notificationReportParameter2, (PXEntryStatus) status);
      }
    }
    base.Persist();
    cache.Clear();
  }

  protected virtual void Notification_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    Notification row = (Notification) e.Row;
    PXUIFieldAttribute.SetEnabled<Notification.notificationID>(cache, (object) row, true);
    bool isVisible = !string.IsNullOrEmpty(row.ScreenID);
    PXUIFieldAttribute.SetVisible<Notification.reportAction>(cache, e.Row, isVisible);
    PXUIFieldAttribute.SetVisible<Notification.attachActivity>(cache, e.Row, isVisible);
    PXUIFieldAttribute.SetVisible<Notification.refNoteID>(cache, e.Row, isVisible);
    PXUIFieldAttribute.SetVisible<Notification.contactID>(cache, e.Row, isVisible);
    PXUIFieldAttribute.SetVisible<Notification.bAccountID>(cache, e.Row, isVisible);
    this.EnsureGraph(this.Notifications.Current.ScreenID);
    if (this._graph == null)
      return;
    bool isEnabled = this._graph.Item1 is PXGenericInqGrph;
    PXUIFieldAttribute.SetEnabled<Notification.refNoteID>(cache, e.Row, isEnabled);
    PXUIFieldAttribute.SetVisible<Notification.refNoteID>(cache, e.Row, isVisible & isEnabled);
    PXUIFieldAttribute.SetVisible<Notification.attachActivity>(cache, e.Row, isVisible && !isEnabled);
    PXUIFieldAttribute.SetEnabled<Notification.attachActivity>(cache, e.Row, !isEnabled);
    this.SetNotificationActions(cache);
  }

  public virtual void _(Events.RowPersisting<Notification> e)
  {
    PXCache reportsCache = this.NotificationReports.Cache;
    foreach (NotificationReport notificationReport in reportsCache.Cached.Cast<NotificationReport>().Where<NotificationReport>((Func<NotificationReport, bool>) (x => reportsCache.GetStatus((object) x) != PXEntryStatus.Deleted && x.ReportTemplateID.HasValue)))
    {
      PXReportSettings pxReportSettings = this.SettingsProvider.Read(notificationReport.ReportTemplateID);
      if (pxReportSettings == null || !pxReportSettings.IsShared)
      {
        e.Cancel = true;
        throw new PXException("{0} '{1}' record raised at least one error. Please review the errors.", new object[2]
        {
          (object) "Updating",
          (object) this.NotificationReports.Name
        });
      }
    }
  }

  public virtual void Notification_ScreenID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.SetNotificationActions(sender);
  }

  public virtual void Notification_NFrom_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is Notification row))
      return;
    bool flag = false;
    int? nullable1 = row.NFrom;
    if (nullable1.HasValue)
      flag = ((EMailAccount) PXSelectorAttribute.Select<Notification.nfrom>(cache, (object) row, (object) row.NFrom))?.EmailAccountType == "E";
    if (flag)
    {
      PXFieldSelectingEventArgs selectingEventArgs = e;
      object returnState = e.ReturnState;
      bool? isKey = new bool?();
      bool? nullable2 = new bool?();
      nullable1 = new int?();
      int? required = nullable1;
      nullable1 = new int?();
      int? precision = nullable1;
      nullable1 = new int?();
      int? length = nullable1;
      bool? enabled = new bool?();
      bool? visible = new bool?();
      bool? readOnly = new bool?();
      PXFieldState instance = PXFieldState.CreateInstance(returnState, (System.Type) null, isKey, nullable2, required, precision, length, error: "The email will not be processed by the Exchange Integration if the email owner is not specified.", errorLevel: PXErrorLevel.Warning, enabled: enabled, visible: visible, readOnly: readOnly);
      selectingEventArgs.ReturnState = (object) instance;
    }
    else
    {
      PXFieldSelectingEventArgs selectingEventArgs = e;
      object returnState = e.ReturnState;
      bool? isKey = new bool?();
      bool? nullable3 = new bool?();
      nullable1 = new int?();
      int? required = nullable1;
      nullable1 = new int?();
      int? precision = nullable1;
      nullable1 = new int?();
      int? length = nullable1;
      bool? enabled = new bool?();
      bool? visible = new bool?();
      bool? readOnly = new bool?();
      PXFieldState instance = PXFieldState.CreateInstance(returnState, (System.Type) null, isKey, nullable3, required, precision, length, enabled: enabled, visible: visible, readOnly: readOnly);
      selectingEventArgs.ReturnState = (object) instance;
    }
  }

  protected virtual void _(Events.FieldUpdated<Notification.nto> e)
  {
    if (e.Row == null)
      return;
    (e.Row as Notification).NTo = this.FormatToAddresses((string) e.NewValue);
  }

  protected virtual void _(Events.FieldUpdated<Notification.ncc> e)
  {
    if (e.Row == null)
      return;
    (e.Row as Notification).NCc = this.FormatToAddresses((string) e.NewValue);
  }

  protected virtual void _(Events.FieldUpdated<Notification.nBcc> e)
  {
    if (e.Row == null)
      return;
    (e.Row as Notification).NBcc = this.FormatToAddresses((string) e.NewValue);
  }

  protected virtual string FormatToAddresses(string tofields)
  {
    return tofields?.Replace("\u200B", " ").TrimEnd();
  }

  protected virtual void NotificationReportParameter_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    NotificationReportParameter row = e.Row as NotificationReportParameter;
    string screenId = this.Notifications.Current?.ScreenID;
    if (row == null || screenId == null)
      return;
    PXUIFieldAttribute.SetEnabled<NotificationReportParameter.name>(sender, (object) row, this.IsCopyPasteContext);
    string[] values = (string[]) null;
    string[] labels = (string[]) null;
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes((object) row, "value"))
    {
      if (attribute is PrimaryViewValueListAttribute valueListAttribute)
      {
        bool? fromSchema = row.FromSchema;
        bool flag = true;
        if (fromSchema.GetValueOrDefault() == flag & fromSchema.HasValue)
        {
          valueListAttribute.IsActive = true;
        }
        else
        {
          valueListAttribute.IsActive = false;
          if (values == null && !SMNotificationMaint.GetScreenFields(this._screenInfoProvider, screenId, out values, out labels))
            break;
          valueListAttribute.SetList(sender, values, labels);
        }
      }
    }
  }

  protected virtual void NotificationReport_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    bool isVisible = !string.IsNullOrEmpty(this.Notifications.Current?.ScreenID);
    PXUIFieldAttribute.SetVisible<NotificationReport.passData>(sender, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<NotificationReport.tableToPass>(sender, (object) null, isVisible);
    if (!(e.Row is NotificationReport row))
      return;
    byte? format = row.Format;
    int? nullable = format.HasValue ? new int?((int) format.GetValueOrDefault()) : new int?();
    int num = 1;
    bool isEnabled = nullable.GetValueOrDefault() == num & nullable.HasValue;
    PXUIFieldAttribute.SetEnabled<NotificationReport.embedded>(sender, (object) row, isEnabled);
    if (!isEnabled)
      sender.SetValueExt<NotificationReport.embedded>((object) row, (object) false);
    this.SetReportParamsEnability(row, sender);
  }

  protected virtual void _(
    Events.FieldSelecting<NotificationReport, NotificationReport.reportTemplateID> e)
  {
    NotificationReport row = e.Row;
    if ((row != null ? (!row.ReportTemplateID.HasValue ? 1 : 0) : 1) != 0)
      return;
    PXReportSettings pxReportSettings = this.SettingsProvider.Read(row.ReportTemplateID);
    if (pxReportSettings != null && pxReportSettings.IsShared)
      return;
    e.Cache.RaiseExceptionHandling<NotificationReport.reportTemplateID>((object) row, (object) row.ReportTemplateID, (Exception) new PXSetPropertyException("The report template cannot be used to generate attached reports. Make sure the template exists for the corresponding report and the Shared check box is selected for it."));
  }

  protected void NotificationReportParameter_FromSchema_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    NotificationReportParameter row = (NotificationReportParameter) e.Row;
    if (row == null || this.IsCopyPasteContext)
      return;
    row.Value = (string) null;
    row.IsOverride = new bool?(true);
  }

  protected void NotificationReport_ReportID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.NewGuid();
  }

  protected virtual void _(
    Events.FieldUpdated<NotificationReport, NotificationReport.screenID> e)
  {
    foreach (NotificationReportParameter notificationReportParameter in this.NotificationReportParameters.Cache.Cached.Cast<NotificationReportParameter>().Where<NotificationReportParameter>((Func<NotificationReportParameter, bool>) (x =>
    {
      Guid? reportId1 = x.ReportID;
      Guid? reportId2 = e.Row.ReportID;
      if (reportId1.HasValue != reportId2.HasValue)
        return false;
      return !reportId1.HasValue || reportId1.GetValueOrDefault() == reportId2.GetValueOrDefault();
    })))
      notificationReportParameter.IsOverride = new bool?(false);
    e.Row.ReportTemplateID = new Guid?();
  }

  protected virtual void _(
    Events.FieldUpdated<NotificationReportParameter, NotificationReportParameter.value> e)
  {
    if (!((string) e.OldValue != (string) e.NewValue))
      return;
    e.Row.IsOverride = new bool?(true);
  }

  protected virtual void _(
    Events.FieldUpdated<NotificationReportParameter, NotificationReportParameter.isOverride> e)
  {
    if (e.Row == null)
      return;
    bool? newValue = (bool?) e.NewValue;
    bool flag = false;
    if (!(newValue.GetValueOrDefault() == flag & newValue.HasValue))
      return;
    e.Row.FromSchema = new bool?(true);
  }

  protected virtual void _(
    Events.FieldSelecting<NotificationReportParameter, NotificationReportParameter.value> e)
  {
    NotificationReportParameter row = e.Row;
    if (row == null)
    {
      e.ReturnState = (object) PXStringState.CreateInstance((object) null, new int?(), new bool?(true), typeof (NotificationReportParameter.value).Name, new bool?(false), new int?(), (string) null, new string[1]
      {
        string.Empty
      }, new string[1]{ string.Empty }, new bool?(), (string) null);
    }
    else
    {
      bool? isOverride = row.IsOverride;
      bool flag1 = true;
      if (isOverride.GetValueOrDefault() == flag1 & isOverride.HasValue)
        return;
      NotificationReport current = this.NotificationReports.Current;
      if (current.ReportTemplateID.HasValue)
      {
        PXReportSettings pxReportSettings = this.SettingsProvider.Read(current.ReportTemplateID);
        if (pxReportSettings != null && pxReportSettings.IsShared)
        {
          ParameterDefault parameterDefault = ((IEnumerable<ParameterDefault>) pxReportSettings.ParameterValues).FirstOrDefault<ParameterDefault>((Func<ParameterDefault, bool>) (x => x.Name == row.Name));
          if (parameterDefault != null)
          {
            row.Value = parameterDefault.Value;
            return;
          }
        }
      }
      ReportInfo reportInfo;
      ExceptionExtensions.CheckIfNull<IReportCachingService>(this.ReportCachingService, "ReportCachingService", (string) null).ProcessAndCacheReport(current.ScreenID, (IDictionary<string, string>) null, (IPXResultset) null, false, false, out SoapNavigator _, out reportInfo);
      NotificationReportParameter notificationReportParameter = row;
      ReportSelectArguments selectArguments = reportInfo.SelectArguments;
      string str = selectArguments != null ? ((IEnumerable<ReportParameter>) selectArguments.Parameters).FirstOrDefault<ReportParameter>((Func<ReportParameter, bool>) (x =>
      {
        bool? visible = x.Visible;
        bool flag2 = true;
        return visible.GetValueOrDefault() == flag2 & visible.HasValue && !string.IsNullOrWhiteSpace(x.Prompt) && x.Name == row.Name;
      }))?.Value?.ToString() : (string) null;
      notificationReportParameter.Value = str;
    }
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Username")]
  protected virtual void _(
    Events.CacheAttached<AUReportProcess.ReportSettings.username> e)
  {
  }

  private void SetReportParamsEnability(NotificationReport row, PXCache cache)
  {
    PXUIFieldAttribute.SetEnabled<NotificationReport.passData>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<NotificationReport.tableToPass>(cache, (object) row, false);
    string screenId = this.Notifications.Current?.ScreenID;
    if (row == null || row.ScreenID == null || screenId == null)
      return;
    PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(row.ScreenID);
    if (screenIdUnsecure != null && PXSiteMap.IsARmReport(screenIdUnsecure.Url))
      return;
    bool? passData1 = row.PassData;
    ReportInfo reportInfo;
    ExceptionExtensions.CheckIfNull<IReportCachingService>(this.ReportCachingService, "ReportCachingService", (string) null).ProcessAndCacheReport(row.ScreenID, (IDictionary<string, string>) null, (IPXResultset) null, false, false, out SoapNavigator _, out reportInfo);
    string[] array;
    if (SMNotificationMaint.IsGIScreen(screenId, out PXSiteMapNode _))
    {
      PXGenericInqGrph geninqgraph = PXGenericInqGrph.CreateInstance(screenId);
      if (geninqgraph == null)
        return;
      IEnumerable<string> second1 = ((IEnumerable<ReportTable>) reportInfo.SelectArguments.Tables).Select<ReportTable, string>((Func<ReportTable, string>) (table =>
      {
        string tableAlias = geninqgraph.GetTableAlias(table.FullName);
        return !string.IsNullOrEmpty(tableAlias) ? tableAlias : table.Name;
      }));
      IEnumerable<string> first = geninqgraph.ResultColumns.Select<GIResult, string>((Func<GIResult, string>) (c => c.ObjectName)).Intersect<string>(second1, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      string[] keyNames = geninqgraph.GetKeyNames(geninqgraph.Results.Name);
      IEnumerable<string> second2 = geninqgraph.ResultColumns.Select<GIResult, string>((Func<GIResult, string>) (c => c.FieldName)).Intersect<string>((IEnumerable<string>) keyNames, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).Select<string, string>((Func<string, string>) (c => c.Substring(0, c.IndexOf("_", StringComparison.OrdinalIgnoreCase))));
      StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
      array = first.Intersect<string>(second2, (IEqualityComparer<string>) ordinalIgnoreCase).ToArray<string>();
    }
    else
    {
      PXSiteMap.ScreenInfo info = this._screenInfoProvider.TryGet(screenId);
      if (info == null)
        return;
      System.Type graphType = SyImportProcessor.GetGraphType(this.PageIndexingService.GetGraphTypeByScreenID(screenId));
      if (graphType == (System.Type) null)
        return;
      PXGraph graph = PXGraph.CreateInstance(graphType);
      IEnumerable<string> second = ((IEnumerable<ReportTable>) reportInfo.SelectArguments.Tables).Select<ReportTable, string>((Func<ReportTable, string>) (table => table.Name));
      PXView pxView;
      array = info.Containers.Keys.Where<string>((Func<string, bool>) (viewName => viewName.OrdinalEquals(info.PrimaryView))).Select<string, PXView>((Func<string, PXView>) (viewName => !graph.Views.TryGetValue(viewName, out pxView) ? (PXView) null : pxView)).Select<PXView, string>((Func<PXView, string>) (view => view?.GetItemType()?.Name)).Where<string>((Func<string, bool>) (itemName => itemName != null)).Intersect<string>(second, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).ToArray<string>();
    }
    if (array.Length == 0)
      cache.SetValueExt<NotificationReport.dataToPass>((object) row, (object) (byte) 0);
    PXUIFieldAttribute.SetEnabled<NotificationReport.passData>(cache, (object) row, array.Length != 0);
    PXCache cache1 = cache;
    NotificationReport data = row;
    bool? passData2 = row.PassData;
    bool flag1 = true;
    int num = passData2.GetValueOrDefault() == flag1 & passData2.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<NotificationReport.tableToPass>(cache1, (object) data, num != 0);
    bool? passData3 = row.PassData;
    bool flag2 = true;
    if (passData3.GetValueOrDefault() == flag2 & passData3.HasValue && array.Length != 0)
    {
      PXStringListAttribute.SetList<NotificationReport.tableToPass>(cache, (object) row, array, array);
      if (string.IsNullOrEmpty(row.TableToPass))
      {
        string defaultTableName = this.GetDefaultTable(reportInfo.SelectArguments);
        string str = ((IEnumerable<string>) array).FirstOrDefault<string>((Func<string, bool>) (c => c.Equals(defaultTableName, StringComparison.OrdinalIgnoreCase))) ?? ((IEnumerable<string>) array).FirstOrDefault<string>();
        cache.SetValueExt<NotificationReport.tableToPass>((object) row, (object) str);
      }
    }
    else
      cache.SetValueExt<NotificationReport.tableToPass>((object) row, (object) null);
    bool? nullable = passData1;
    bool? passData4 = row.PassData;
    if (nullable.GetValueOrDefault() == passData4.GetValueOrDefault() & nullable.HasValue == passData4.HasValue)
      return;
    cache.SetStatus((object) row, PXEntryStatus.Updated);
    this.Notifications.View.Cache.Update(this.Notifications.View.Cache.Current);
  }

  private string GetDefaultTable(ReportSelectArguments reportSelectArguments)
  {
    string dataField = ((IEnumerable<FilterExp>) reportSelectArguments.Filters).Where<FilterExp>((Func<FilterExp, bool>) (f => f.Condition != 11 && f.Condition != 12 && f.Value != null && !f.DataField.StartsWith("@") && f.Value.StartsWith("@"))).FirstOrDefault<FilterExp>()?.DataField;
    PXGraph graph = new PXGraph();
    BqlSoapCommand bqlSoapCommand = new BqlSoapCommand(graph, reportSelectArguments);
    return (!string.IsNullOrEmpty(dataField) ? bqlSoapCommand.tryFindBqlField(graph, ref dataField) : (Tuple<System.Type, string, System.Type>) null)?.Item1?.Name;
  }

  internal static bool GetScreenFields(
    IScreenInfoProvider screenInfoProvider,
    string screenId,
    out string[] values,
    out string[] labels)
  {
    values = labels = (string[]) null;
    if (SMNotificationMaint.IsGIScreen(screenId, out PXSiteMapNode _))
    {
      PXGenericInqGrph instance = PXGenericInqGrph.CreateInstance(screenId);
      if (instance == null)
        return false;
      labels = instance.ResultColumns.Select<GIResult, string>((Func<GIResult, string>) (field => !field.Field.StartsWith("=") || string.IsNullOrEmpty(field.Caption) ? $"{field.ObjectName}.{field.Field}" : field.Caption)).ToArray<string>();
      values = instance.ResultColumns.Select<GIResult, string>((Func<GIResult, string>) (field => field.FieldName)).ToArray<string>();
    }
    else
    {
      PXSiteMap.ScreenInfo screenInfo = screenInfoProvider.TryGet(screenId);
      PXViewDescription view;
      if (screenInfo == null || !screenInfo.Containers.TryGetValue(screenInfo.PrimaryView, out view))
        return false;
      labels = ((IEnumerable<PX.Data.Description.FieldInfo>) view.Fields).Select<PX.Data.Description.FieldInfo, string>((Func<PX.Data.Description.FieldInfo, string>) (field => field.DisplayName)).ToArray<string>();
      values = ((IEnumerable<PX.Data.Description.FieldInfo>) view.Fields).Select<PX.Data.Description.FieldInfo, string>((Func<PX.Data.Description.FieldInfo, string>) (field => $"{view.ViewName}_{field.FieldName}")).ToArray<string>();
    }
    return true;
  }

  [PXInternalUseOnly]
  public static bool IsGIScreen(string screenId, out PXSiteMapNode screenNode)
  {
    screenNode = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenId);
    return screenNode != null && PXBuildManager.GetType(screenNode.GraphType, false) == typeof (PXGenericInqGrph);
  }

  private void SetNotificationActions(PXCache notificationCache)
  {
    if (this.Notifications.Current?.ScreenID == null)
      return;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    Dictionary<string, List<KeyValuePair<string, string>>> dictionary = new Dictionary<string, List<KeyValuePair<string, string>>>();
    stringList1.Add((string) null);
    stringList2.Add("");
    this.EnsureGraph(this.Notifications.Current?.ScreenID);
    if (this._graph == null)
      return;
    PXGraph pxGraph;
    PXSiteMap.ScreenInfo screenInfo1;
    this._graph.Deconstruct<PXGraph, string, PXSiteMap.ScreenInfo>(out pxGraph, out string _, out screenInfo1);
    PXGraph _Graph = pxGraph;
    PXSiteMap.ScreenInfo screenInfo2 = screenInfo1;
    if (screenInfo2 != null)
    {
      foreach (PXSiteMap.ScreenInfo.Action action in screenInfo2.Actions)
      {
        string name = action.Name;
        if (!string.IsNullOrEmpty(name) && !name.Contains("@"))
        {
          List<KeyValuePair<string, string>> keyValuePairList = this.CollectMenuInfoByAction(name, _Graph);
          if (keyValuePairList != null && keyValuePairList.Count > 0)
          {
            dictionary[name] = keyValuePairList;
            foreach (KeyValuePair<string, string> keyValuePair in keyValuePairList)
            {
              stringList1.Add($"{name}->{keyValuePair.Key}");
              stringList2.Add($"{action.DisplayName}->{keyValuePair.Value}");
            }
          }
        }
      }
    }
    else if (_Graph != null)
    {
      foreach (string key in (IEnumerable) _Graph.Actions.Keys)
      {
        List<KeyValuePair<string, string>> keyValuePairList = this.CollectMenuInfoByAction(key, _Graph);
        if (keyValuePairList != null && keyValuePairList.Count > 1)
        {
          dictionary[key] = keyValuePairList;
          foreach (KeyValuePair<string, string> keyValuePair in keyValuePairList)
          {
            stringList1.Add($"{key}->{keyValuePair.Key}");
            stringList2.Add($"{key}->{keyValuePair.Value}");
          }
        }
      }
    }
    PXStringListAttribute.SetList<Notification.reportAction>(notificationCache, (object) null, stringList1.ToArray(), stringList2.ToArray());
  }

  private List<KeyValuePair<string, string>> CollectMenuInfoByAction(
    string actionName,
    PXGraph _Graph)
  {
    if (_Graph == null || string.IsNullOrEmpty(actionName) || !_Graph.Actions.Contains((object) actionName))
      return (List<KeyValuePair<string, string>>) null;
    List<KeyValuePair<string, string>> keyValuePairList = new List<KeyValuePair<string, string>>();
    if (_Graph.Actions[actionName].GetState((object) null) is PXButtonState state && state.Menus != null && (state.SpecialType == PXSpecialButtonType.Report || state.SpecialType == PXSpecialButtonType.ReportsFolder))
    {
      foreach (ButtonMenu menu in state.Menus)
        keyValuePairList.Add(new KeyValuePair<string, string>(menu.Command, menu.Text));
    }
    return keyValuePairList;
  }

  [PXBreakInheritance]
  public class UserEmail : Users
  {
    [PXString]
    [PXDependsOnFields(new System.Type[] {typeof (Users.username)})]
    [PXUIField(DisplayName = "Username Key")]
    public virtual string KeyUserName => $"EMAIL(({this.Username}))";

    public abstract class keyUserName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SMNotificationMaint.UserEmail.keyUserName>
    {
    }
  }
}
