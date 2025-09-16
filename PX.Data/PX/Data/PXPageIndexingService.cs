// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPageIndexingService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using CommonServiceLocator;
using PX.Api;
using PX.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

#nullable enable
namespace PX.Data;

[PXInternalUseOnly]
public class PXPageIndexingService : IPXPageIndexingService
{
  private static 
  #nullable disable
  IPXPageIndexingService _instance;
  private readonly Lazy<Dictionary<string, string>> _screenByGraphType;
  private readonly Lazy<Dictionary<string, IList<string>>> _screensByGraphType;
  private readonly PXPageIndexingService.FindIgnoreCase _typeNameFinder = new PXPageIndexingService.FindIgnoreCase("TypeName");
  private readonly PXPageIndexingService.FindIgnoreCase _dataMemberFinder = new PXPageIndexingService.FindIgnoreCase("DataMember");
  private readonly PXPageIndexingService.FindIgnoreCase _dataFieldFinder = new PXPageIndexingService.FindIgnoreCase("DataField");
  private readonly PXPageIndexingService.FindIgnoreCase _primaryViewFinder = new PXPageIndexingService.FindIgnoreCase("PrimaryView");
  private readonly PXPageIndexingService.FindIgnoreCase _udfTypeFieldFinder = new PXPageIndexingService.FindIgnoreCase("UDFTypeField");
  private readonly PXPageIndexingService.FindIgnoreCase _commandFinder = new PXPageIndexingService.FindIgnoreCase("Command");
  private readonly PXPageIndexingService.FindIgnoreCase _targetFinder = new PXPageIndexingService.FindIgnoreCase("Target");
  private readonly PXPageIndexingService.FindIgnoreCase _commandNameFinder = new PXPageIndexingService.FindIgnoreCase("CommandName");
  private readonly PXPageIndexingService.FindIgnoreCase _commandSourceIDFinder = new PXPageIndexingService.FindIgnoreCase("CommandSourceID");
  private readonly PXPageIndexingService.FindIgnoreCase _autoCallBackCommandFinder = new PXPageIndexingService.FindIgnoreCase("AutoCallBack-Command");
  private readonly PXPageIndexingService.FindIgnoreCase _autoCallBackTargetFinder = new PXPageIndexingService.FindIgnoreCase("AutoCallBack-Target");
  private readonly PXPageIndexingService.FindIgnoreCase _linkCommandFinder = new PXPageIndexingService.FindIgnoreCase("LinkCommand");
  private readonly PXPageIndexingService.FindIgnoreCase _popupCommandFinder = new PXPageIndexingService.FindIgnoreCase("PopupCommand");
  private readonly PXPageIndexingService.FindIgnoreCase _popupCommandTargetFinder = new PXPageIndexingService.FindIgnoreCase("PopupCommandTarget");
  private PXPageIndexingService.PXPageIndexingScreenInfo _pageInfo;

  [Obsolete]
  internal static IPXPageIndexingService Instance
  {
    get
    {
      if (PXPageIndexingService._instance != null)
        return PXPageIndexingService._instance;
      if (PXHostingEnvironment.IsHosted)
        throw new InvalidOperationException("IPXPageIndexingService should have been already initialized");
      return !ServiceLocator.IsLocationProviderSet ? (IPXPageIndexingService) new PXPageIndexingService() : ServiceLocator.Current.GetInstance<IPXPageIndexingService>();
    }
    private set
    {
      if (PXPageIndexingService._instance != null)
        throw new InvalidOperationException("IPXPageIndexingService has already been initialized");
      PXPageIndexingService._instance = value ?? throw new ArgumentNullException(nameof (value));
    }
  }

  public PXPageIndexingService()
  {
    this._pageInfo = new PXPageIndexingService.PXPageIndexingScreenInfo();
    this._screensByGraphType = new Lazy<Dictionary<string, IList<string>>>(new Func<Dictionary<string, IList<string>>>(this.InitScreenByGraphTypeCollection));
    this._screenByGraphType = new Lazy<Dictionary<string, string>>(new Func<Dictionary<string, string>>(this.InitScreenByGraphType));
  }

  void IPXPageIndexingService.Clear()
  {
    this._pageInfo = new PXPageIndexingService.PXPageIndexingScreenInfo();
  }

  [Obsolete]
  public static void Clear() => PXPageIndexingService.Instance.Clear();

  private PXPageIndexingService.PXPageIndexingScreenInfo FindPages()
  {
    PXPageIndexingService.PXPageIndexingScreenInfo pageInfo = this._pageInfo;
    if (pageInfo._isInit)
      return pageInfo;
    lock (pageInfo)
    {
      if (pageInfo._isInit)
        return pageInfo;
      try
      {
        string pages = HostingEnvironment.MapPath("~/");
        if (pages == null)
          return pageInfo;
        string[] excludedFolderPaths = new string[1]
        {
          pages + "CstDesigner\\"
        };
        List<string> source = new List<string>(((IEnumerable<string>) Directory.GetFiles(pages, "*.aspx", SearchOption.AllDirectories)).Where<string>((Func<string, bool>) (pagePath => !((IEnumerable<string>) excludedFolderPaths).Any<string>((Func<string, bool>) (ef => pagePath.StartsWith(ef, StringComparison.InvariantCultureIgnoreCase))))));
        List<string> cstPublishedPages = source.Where<string>((Func<string, bool>) (file => file.StartsWith(pages + "CstPublished\\"))).ToList<string>();
        source.RemoveAll((Predicate<string>) (file => cstPublishedPages.Contains(file)));
        source.AddRange((IEnumerable<string>) cstPublishedPages);
        foreach (string str in source)
        {
          string content;
          string graphTypeFromFile = this.GetGraphTypeFromFile(str, out content);
          pageInfo._graphTypes.Add(str, graphTypeFromFile);
          int num = str.LastIndexOf('\\');
          string key1 = num < 0 || num >= str.Length - 5 ? str.Substring(0, str.Length - 5) : str.Substring(num + 1, str.Length - 5 - num - 1);
          if (key1.Length == 8)
          {
            pageInfo._graphTypesByScreenID[key1] = graphTypeFromFile;
            if (graphTypeFromFile != null)
            {
              string key2 = key1.Substring(0, 2);
              if (!pageInfo._screensByModule.ContainsKey(key2))
                pageInfo._screensByModule.Add(key2, new List<string>());
              pageInfo._screensByModule[key2].Add(key1);
            }
            if (PXPageIndexingService.GetBPEventsIndicator(content))
              pageInfo._bpEventsByScreenID.Add(key1);
          }
          string[] dataMembers = this.FindDataMembers(content);
          if (!string.IsNullOrEmpty(graphTypeFromFile))
            pageInfo._graphDataMembers[graphTypeFromFile] = dataMembers;
          if (key1.Length == 8)
            pageInfo._screensPrimaryView[key1] = dataMembers != null ? ((IEnumerable<string>) dataMembers).FirstOrDefault<string>() : (string) null;
          Dictionary<string, string> metaDataFromScreen = this.FindGraphAdditionalMetaDataFromScreen(content);
          if (graphTypeFromFile != null && metaDataFromScreen != null)
            pageInfo._graphAdditionalCollectedData[graphTypeFromFile] = metaDataFromScreen;
          string[] commands = this.FindCommands(content, "ds");
          if (!string.IsNullOrEmpty(graphTypeFromFile))
          {
            string[] first;
            pageInfo._screenUsedCommands[graphTypeFromFile] = pageInfo._screenUsedCommands.TryGetValue(graphTypeFromFile, out first) ? ((IEnumerable<string>) first).Union<string>((IEnumerable<string>) commands).Distinct<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).ToArray<string>() : commands;
          }
        }
        pageInfo._isInit = true;
      }
      catch
      {
        ((IPXPageIndexingService) this).Clear();
        throw;
      }
    }
    return pageInfo;
  }

  private string[] FindDataMembers(string content)
  {
    List<string> stringList = new List<string>();
    HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    int pos1 = 0;
    string attribute1 = PXPageIndexingService.GetAttribute(this._primaryViewFinder, content, ref pos1);
    if (!string.IsNullOrEmpty(attribute1))
    {
      stringSet.Add(attribute1);
      stringList.Add(attribute1);
    }
    int pos2 = 0;
    while (pos2 < content.Length)
    {
      string attribute2 = PXPageIndexingService.GetAttribute(this._dataMemberFinder, content, ref pos2);
      if (!string.IsNullOrEmpty(attribute2) && !stringSet.Contains(attribute2))
      {
        stringSet.Add(attribute2);
        stringList.Add(attribute2);
      }
    }
    int pos3 = 0;
    while (pos3 < content.Length)
    {
      string attribute3 = PXPageIndexingService.GetAttribute(this._dataFieldFinder, content, ref pos3, '.', '"');
      if (!string.IsNullOrEmpty(attribute3) && !stringSet.Contains(attribute3))
      {
        stringSet.Add(attribute3);
        stringList.Add(attribute3);
      }
    }
    return stringList.ToArray();
  }

  private string[] FindCommands(string content, string dataSourceName)
  {
    List<string> result = new List<string>();
    HashSet<string> found = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this.FindCommandInternal(this._commandFinder, this._targetFinder, content, dataSourceName, result, found);
    this.FindCommandInternal(this._commandNameFinder, this._commandSourceIDFinder, content, dataSourceName, result, found);
    this.FindCommandInternal(this._autoCallBackCommandFinder, this._autoCallBackTargetFinder, content, dataSourceName, result, found);
    this.FindCommandInternal(this._popupCommandFinder, this._popupCommandTargetFinder, content, dataSourceName, result, found);
    this.FindCommandInternal(this._linkCommandFinder, (PXPageIndexingService.FindIgnoreCase) null, content, dataSourceName, result, found);
    return result.ToArray();
  }

  private Dictionary<string, string> FindGraphAdditionalMetaDataFromScreen(string content)
  {
    Dictionary<string, string> metaDataFromScreen = new Dictionary<string, string>();
    int pos = 0;
    metaDataFromScreen.Add(this._udfTypeFieldFinder.Lower, PXPageIndexingService.GetAttribute(this._udfTypeFieldFinder, content, ref pos));
    return metaDataFromScreen;
  }

  private void FindCommandInternal(
    PXPageIndexingService.FindIgnoreCase commandFinder,
    PXPageIndexingService.FindIgnoreCase targetFinder,
    string content,
    string dataSourceName,
    List<string> result,
    HashSet<string> found)
  {
    int pos1 = 0;
    while (pos1 < content.Length)
    {
      string attribute1 = PXPageIndexingService.GetAttribute(commandFinder, content, ref pos1);
      if (!string.IsNullOrEmpty(attribute1) && !found.Contains(attribute1))
      {
        int startIndex1 = pos1;
        int length = content.IndexOf('>', startIndex1);
        int startIndex2 = content.Substring(0, length).LastIndexOf('<');
        string text = content.Substring(startIndex2, length - startIndex2);
        int pos2 = 0;
        string attribute2 = targetFinder != null ? PXPageIndexingService.GetAttribute(targetFinder, text, ref pos2) : (string) null;
        if (string.IsNullOrEmpty(attribute2) || string.Equals(attribute2, dataSourceName, StringComparison.OrdinalIgnoreCase))
        {
          found.Add(attribute1);
          result.Add(attribute1);
        }
      }
    }
  }

  private static string GetAttribute(
    PXPageIndexingService.FindIgnoreCase attr,
    string text,
    ref int pos,
    char closeBraket = '"',
    char abortChar = '\n')
  {
    try
    {
      int startIndex;
      char ch;
      do
      {
        do
        {
          do
          {
            int num;
            do
            {
              int indexIn = attr.FindIndexIn(text, pos);
              if (indexIn < 0)
              {
                pos = text.Length;
                return (string) null;
              }
              num = indexIn;
              pos = indexIn + attr.StrLen;
            }
            while (!char.IsWhiteSpace(text[num - 1]));
            while (char.IsWhiteSpace(text[pos]))
              ++pos;
          }
          while (text[pos] != '=');
          ++pos;
          while (char.IsWhiteSpace(text[pos]))
            ++pos;
        }
        while (text[pos] != '"');
        ++pos;
        startIndex = pos;
        while ((int) (ch = text[pos]) != (int) closeBraket && (int) ch != (int) abortChar)
          ++pos;
      }
      while ((int) ch == (int) abortChar);
      return text.Substring(startIndex, pos - startIndex);
    }
    catch
    {
    }
    pos = text.Length;
    return (string) null;
  }

  private string GetGraphTypeFromFile(string file, out string content)
  {
    content = File.ReadAllText(file);
    int pos = 0;
    string attribute = PXPageIndexingService.GetAttribute(this._typeNameFinder, content, ref pos);
    if (attribute == null)
      return (string) null;
    return !attribute.Contains<char>(',') ? attribute : DataSourceTypeProvider.GetTypeName(attribute);
  }

  string IPXPageIndexingService.GetGraphType(string url)
  {
    if (string.IsNullOrEmpty(url))
      return (string) null;
    if (url.StartsWith("~/GenericInquiry/GenericInquiry.aspx", StringComparison.OrdinalIgnoreCase))
      return typeof (PXGenericInqGrph).FullName;
    if (url.StartsWith("~/Pages/SM/SM206036.aspx?", StringComparison.OrdinalIgnoreCase))
      return typeof (SYImportProcessSingle).FullName;
    url = PXUrl.IngoreAllQueryParameters(url);
    PXPageIndexingService.PXPageIndexingScreenInfo pages = this.FindPages();
    try
    {
      string key = HostingEnvironment.MapPath(url);
      return !pages._graphTypes.ContainsKey(key) ? (string) null : pages._graphTypes[key];
    }
    catch
    {
      return (string) null;
    }
  }

  [Obsolete]
  public static string GetGraphType(string url) => PXPageIndexingService.Instance.GetGraphType(url);

  string IPXPageIndexingService.GetGraphTypeByScreenID(string screenID)
  {
    if (string.IsNullOrEmpty(screenID))
      return (string) null;
    PXPageIndexingService.PXPageIndexingScreenInfo pages = this.FindPages();
    return !pages._graphTypesByScreenID.ContainsKey(screenID) ? (string) null : pages._graphTypesByScreenID[screenID];
  }

  [Obsolete]
  public static string GetGraphTypeByScreenID(string screenID)
  {
    return PXPageIndexingService.Instance.GetGraphTypeByScreenID(screenID);
  }

  string IPXPageIndexingService.GetScreenIDFromGraphType(System.Type g)
  {
    this.FindPages();
    return this._screenByGraphType.Value.GetOrDefault<string, string>(CustomizedTypeManager.GetTypeNotCustomized(g).FullName, (string) null);
  }

  string IPXPageIndexingService.GetScreenIDFromGraphType(string graphTypeName)
  {
    this.FindPages();
    return this._screenByGraphType.Value.GetOrDefault<string, string>(graphTypeName, (string) null);
  }

  private Dictionary<string, string> InitScreenByGraphType()
  {
    PXPageIndexingService.PXPageIndexingScreenInfo pageInfo = this._pageInfo;
    Dictionary<string, string> dictionary = new Dictionary<string, string>(pageInfo._graphTypesByScreenID.Count);
    foreach (KeyValuePair<string, string> keyValuePair in pageInfo._graphTypesByScreenID)
    {
      string key = keyValuePair.Value;
      if (key != null && !dictionary.ContainsKey(key))
        dictionary.Add(key, keyValuePair.Key);
    }
    return dictionary;
  }

  [Obsolete]
  public static string GetScreenIDFromGraphType(System.Type g)
  {
    return PXPageIndexingService.Instance.GetScreenIDFromGraphType(g);
  }

  [Obsolete]
  public static string GetScreenIDFromGraphType(string graphType)
  {
    return PXPageIndexingService.Instance.GetScreenIDFromGraphType(graphType);
  }

  IList<string> IPXPageIndexingService.GetScreensIDFromGraphType(System.Type g)
  {
    this.FindPages();
    IList<string> stringList;
    return this._screensByGraphType.Value.TryGetValue(CustomizedTypeManager.GetTypeNotCustomized(g).FullName, out stringList) ? stringList : (IList<string>) null;
  }

  private Dictionary<string, IList<string>> InitScreenByGraphTypeCollection()
  {
    PXPageIndexingService.PXPageIndexingScreenInfo pageInfo = this._pageInfo;
    Dictionary<string, IList<string>> dictionary = new Dictionary<string, IList<string>>(pageInfo._graphTypesByScreenID.Count);
    foreach (KeyValuePair<string, string> keyValuePair in pageInfo._graphTypesByScreenID)
    {
      string key1 = keyValuePair.Value;
      string key2 = keyValuePair.Key;
      if (key1 != null)
      {
        IList<string> stringList;
        if (dictionary.TryGetValue(key1, out stringList))
          stringList.Add(key2);
        else
          dictionary.Add(key1, (IList<string>) new List<string>()
          {
            key2
          });
      }
    }
    return dictionary;
  }

  [Obsolete]
  public static IList<string> GetScreensIDFromGraphType(System.Type g)
  {
    return PXPageIndexingService.Instance.GetScreensIDFromGraphType(g);
  }

  IEnumerable<string> IPXPageIndexingService.GetScreensByModule(string moduleID)
  {
    if (!string.IsNullOrEmpty(moduleID))
    {
      PXPageIndexingService.PXPageIndexingScreenInfo pages = this.FindPages();
      if (pages._screensByModule.ContainsKey(moduleID))
      {
        foreach (string str in pages._screensByModule[moduleID])
          yield return str;
      }
    }
  }

  [Obsolete]
  public static IEnumerable<string> GetScreensByModule(string moduleID)
  {
    return PXPageIndexingService.Instance.GetScreensByModule(moduleID);
  }

  string[] IPXPageIndexingService.GetDataMembers(string graphType)
  {
    string[] dataMembers;
    this.FindPages()._graphDataMembers.TryGetValue(graphType, out dataMembers);
    return dataMembers;
  }

  [Obsolete]
  public static string[] GetDataMembers(string graphType)
  {
    return PXPageIndexingService.Instance.GetDataMembers(graphType);
  }

  Dictionary<string, string> IPXPageIndexingService.GetGraphAdditionalCollectedData(string graphType)
  {
    Dictionary<string, string> additionalCollectedData;
    this.FindPages()._graphAdditionalCollectedData.TryGetValue(graphType, out additionalCollectedData);
    return additionalCollectedData;
  }

  string[] IPXPageIndexingService.GetScreenUsedCommands(string graphType)
  {
    string[] screenUsedCommands;
    this.FindPages()._screenUsedCommands.TryGetValue(graphType, out screenUsedCommands);
    return screenUsedCommands;
  }

  [Obsolete]
  public static string[] GetScreenUsedCommands(string graphType)
  {
    return PXPageIndexingService.Instance.GetScreenUsedCommands(graphType);
  }

  string IPXPageIndexingService.GetPrimaryView(string graphType)
  {
    string[] dataMembers = ((IPXPageIndexingService) this).GetDataMembers(graphType);
    return dataMembers == null ? (string) null : ((IEnumerable<string>) dataMembers).FirstOrDefault<string>();
  }

  [Obsolete]
  public static string GetPrimaryView(string graphType)
  {
    return PXPageIndexingService.Instance.GetPrimaryView(graphType);
  }

  string IPXPageIndexingService.GetPrimaryViewForScreen(string screenId)
  {
    string primaryViewForScreen;
    this.FindPages()._screensPrimaryView.TryGetValue(screenId, out primaryViewForScreen);
    return primaryViewForScreen;
  }

  string IPXPageIndexingService.GetUDFTypeField(string graphType)
  {
    return ((IPXPageIndexingService) this).GetGraphAdditionalCollectedData(graphType)?[this._udfTypeFieldFinder.Lower];
  }

  [Obsolete]
  public static string GetUDFTypeField(string graphType)
  {
    return PXPageIndexingService.Instance.GetUDFTypeField(graphType);
  }

  private static bool GetBPEventsIndicator(string content)
  {
    int pos = 0;
    bool result;
    return bool.TryParse(PXPageIndexingService.GetAttribute(new PXPageIndexingService.FindIgnoreCase("BPEventsIndicator"), content, ref pos), out result) & result;
  }

  bool IPXPageIndexingService.HasBPEventsIndicator(string screenID)
  {
    return !string.IsNullOrEmpty(screenID) && this.FindPages()._bpEventsByScreenID.Contains(screenID);
  }

  [Obsolete]
  public static bool HasBPEventsIndicator(string screenID)
  {
    return PXPageIndexingService.Instance.HasBPEventsIndicator(screenID);
  }

  private class HostedServiceRegistration : Module
  {
    protected virtual void Load(ContainerBuilder builder)
    {
      builder.RegisterBuildCallback((System.Action<ILifetimeScope>) (lifetimeScope => PXPageIndexingService.Instance = ResolutionExtensions.Resolve<IPXPageIndexingService>((IComponentContext) lifetimeScope)));
    }
  }

  private class FindIgnoreCase
  {
    public readonly string Lower;
    public readonly string Upper;
    public readonly int StrLen;

    public FindIgnoreCase(string str)
    {
      this.Lower = str.ToLowerInvariant();
      this.Upper = str.ToUpperInvariant();
      this.StrLen = str.Length;
    }

    public int FindIndexIn(string text, int start)
    {
      int num = text.Length - this.StrLen;
      for (int indexIn = start; indexIn < num; ++indexIn)
      {
        bool flag = false;
        for (int index = 0; index < this.StrLen; ++index)
        {
          char ch = text[indexIn + index];
          if ((int) ch != (int) this.Upper[index] && (int) ch != (int) this.Lower[index])
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return indexIn;
      }
      return -1;
    }
  }

  private class PXPageIndexingScreenInfo
  {
    internal bool _isInit;
    internal readonly Dictionary<string, string> _graphTypes = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    internal readonly Dictionary<string, string> _graphTypesByScreenID = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    internal readonly Dictionary<string, List<string>> _screensByModule = new Dictionary<string, List<string>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    internal readonly HashSet<string> _bpEventsByScreenID = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    internal readonly Dictionary<string, string[]> _graphDataMembers = new Dictionary<string, string[]>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    internal readonly Dictionary<string, string> _screensPrimaryView = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    internal readonly Dictionary<string, Dictionary<string, string>> _graphAdditionalCollectedData = new Dictionary<string, Dictionary<string, string>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    internal readonly Dictionary<string, string[]> _screenUsedCommands = new Dictionary<string, string[]>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  }
}
