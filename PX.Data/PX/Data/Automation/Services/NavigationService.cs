// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.Services.NavigationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Common;
using PX.Data.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Data.Automation.Services;

internal sealed class NavigationService : INavigationService
{
  private readonly Func<string, INavigationScreenInfo> _getScreenInfoByScreenIdFunc;
  private readonly PXGraphFactory _createGraphByScreenId;
  private readonly IPXPageIndexingService _pageIndexingService;
  private readonly PXSiteMapProvider _siteMapProvider;
  private readonly bool _allowInsert;
  private readonly bool _repaintControls;
  private readonly bool _enableNewUIGraphReusing;

  public NavigationService(
    Func<string, INavigationScreenInfo> getScreenInfoByScreenIdFunc,
    PXGraphFactory createGraphByScreenId,
    IPXPageIndexingService pageIndexingService,
    PXSiteMapProvider siteMapProvider,
    IOptions<OptimizationOptions> optimizationOptions,
    bool allowInsert,
    bool repaintControls = false)
  {
    this._getScreenInfoByScreenIdFunc = getScreenInfoByScreenIdFunc;
    this._createGraphByScreenId = createGraphByScreenId;
    this._pageIndexingService = pageIndexingService;
    this._siteMapProvider = siteMapProvider;
    this._enableNewUIGraphReusing = optimizationOptions.Value.EnableNewUIGraphReusing;
    this._allowInsert = allowInsert;
    this._repaintControls = repaintControls;
  }

  public void NavigateTo(
    string destinationScreenIdOrUrl,
    IReadOnlyDictionary<string, object> evaluatedParameters,
    PXBaseRedirectException.WindowMode windowMode)
  {
    this.NavigateTo(destinationScreenIdOrUrl, evaluatedParameters, windowMode, (string) null);
  }

  public void NavigateTo(
    string destinationScreenIdOrUrl,
    IReadOnlyDictionary<string, object> evaluatedParameters,
    PXBaseRedirectException.WindowMode windowMode,
    string navigationMessage)
  {
    if (string.IsNullOrEmpty(destinationScreenIdOrUrl))
      throw new ArgumentNullException(nameof (destinationScreenIdOrUrl));
    if (NavigationTemplateHelper.IsExternalUrlOrTemplate(destinationScreenIdOrUrl))
    {
      this.NavigateToUrl(destinationScreenIdOrUrl, evaluatedParameters, windowMode, navigationMessage);
    }
    else
    {
      INavigationScreenInfo screen = this._getScreenInfoByScreenIdFunc(destinationScreenIdOrUrl);
      if (screen.IsReport)
        this.NavigateToReport(screen.ScreenId, evaluatedParameters, windowMode, navigationMessage);
      else
        this.NavigateToScreen(screen, evaluatedParameters, windowMode, navigationMessage);
    }
  }

  private void NavigateToReport(
    string screenId,
    IReadOnlyDictionary<string, object> parameters,
    PXBaseRedirectException.WindowMode windowMode,
    string navigationMessage)
  {
    Dictionary<string, string> parameters1 = new Dictionary<string, string>();
    if (parameters != null)
    {
      foreach (string key in parameters.Keys)
      {
        object parameter = parameters[key];
        parameters1[key] = !(parameter is System.DateTime dateTime) ? parameter.ToString() : dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff", (IFormatProvider) CultureInfo.InvariantCulture);
      }
    }
    PXReportRequiredException requiredException = new PXReportRequiredException(parameters1, screenId, windowMode, string.IsNullOrEmpty(navigationMessage) ? string.Empty : navigationMessage);
    requiredException.RepaintControls = this._repaintControls;
    throw requiredException;
  }

  private void NavigateToUrl(
    string urlTemplate,
    IReadOnlyDictionary<string, object> evaluatedParameters,
    PXBaseRedirectException.WindowMode windowMode,
    string navigationMessage)
  {
    string str = NavigationTemplateHelper.ProcessTemplate(urlTemplate, evaluatedParameters);
    if (!PXUrl.IsExternalUrl(str) || !Uri.IsWellFormedUriString(str, UriKind.Absolute))
      throw new PXInvalidOperationException("Unable to navigate to the '{0}' URL. It must be a valid external URL.", new object[1]
      {
        (object) str
      });
    throw new PXRedirectToUrlException(str, windowMode, navigationMessage);
  }

  private void NavigateToScreen(
    INavigationScreenInfo screen,
    IReadOnlyDictionary<string, object> evaluatedParameters,
    PXBaseRedirectException.WindowMode windowMode,
    string navigationMessage)
  {
    PXView primaryView;
    PXGraph graph = this.InstantiateGraph(screen, out primaryView);
    string selectedUi = this._siteMapProvider.FindSiteMapNodeByScreenIDUnsecure(screen.ScreenId)?.SelectedUI;
    if (WebConfig.EnableOldUIGraphReusing && selectedUi == "E" || this._enableNewUIGraphReusing && selectedUi == "T")
      PXReusableGraphFactory.ReuseGraphFromNavigation(graph, screen.ScreenId);
    using (graph.UnderRedirectStatePrefix())
    {
      graph.Clear();
      PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException(screen.Url, graph, windowMode, string.Empty);
      requiredException1.RepaintControls = this._repaintControls;
      PXRedirectRequiredException requiredException2 = requiredException1;
      if (!screen.IsInquiry || screen.IsFilteredInquiry)
      {
        object primaryRow = this.GetPrimaryRow(primaryView, evaluatedParameters);
        primaryView.Cache.Current = primaryRow;
        if (primaryRow == null)
          throw new PXException("Cannot open this record for editing: The form {0} does not contain it.", new object[1]
          {
            (object) screen.ScreenId
          });
      }
      if (screen.IsInquiry || screen.IsFilteredInquiry)
      {
        PXFilterRow[] array = (screen.IsFilteredInquiry ? evaluatedParameters.Where<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>) (np => !primaryView.Cache.Fields.Contains(np.Key))) : (IEnumerable<KeyValuePair<string, object>>) evaluatedParameters).Select<KeyValuePair<string, object>, PXFilterRow>((Func<KeyValuePair<string, object>, PXFilterRow>) (p => new PXFilterRow()
        {
          OrOperator = false,
          Condition = PXCondition.EQ,
          DataField = p.Key,
          Value = p.Value
        })).ToArray<PXFilterRow>();
        requiredException2.Filters.Add(new PXBaseRedirectException.Filter(screen.DataViewName, array));
      }
      if (!string.IsNullOrEmpty(navigationMessage))
        requiredException2.SetMessage(navigationMessage);
      if (windowMode == PXBaseRedirectException.WindowMode.Layer)
        graph.NavigationParams = evaluatedParameters.Keys;
      throw requiredException2;
    }
  }

  private PXGraph InstantiateGraph(INavigationScreenInfo screen, out PXView primaryView)
  {
    PXGraph pxGraph = this.InstantiateGraph(screen);
    string primaryView1 = this._pageIndexingService.GetPrimaryView("GraphType");
    if (string.IsNullOrEmpty(primaryView1))
      primaryView1 = pxGraph.PrimaryView;
    primaryView = pxGraph.Views[primaryView1];
    pxGraph.NavigationParams = (IEnumerable<string>) null;
    return pxGraph;
  }

  private PXGraph InstantiateGraph(INavigationScreenInfo screen)
  {
    if (screen.GraphType == (System.Type) null)
      throw new PXException("A graph cannot be created.");
    using (new PXPreserveScope())
      return this._createGraphByScreenId(screen.ScreenId);
  }

  public object GetPrimaryRow(PXView primaryView, IReadOnlyDictionary<string, object> parameters)
  {
    PXCache cache = primaryView.Cache;
    return (!this._allowInsert || this.AreKeysFilled(cache, parameters)) && !parameters.Values.All<object>((Func<object, bool>) (val => val == null)) ? this.GetExistingRow(primaryView, parameters) : this.CreateNewRow(primaryView, parameters);
  }

  private object GetExistingRow(PXView view, IReadOnlyDictionary<string, object> parameters)
  {
    bool flag = view.Cache.Keys.Count == 0;
    object obj = flag ? view.Cache.Current : view.Cache.CreateInstance();
    List<object> objectList = new List<object>();
    List<string> stringList = new List<string>();
    try
    {
      foreach (KeyValuePair<string, object> parameter in (IEnumerable<KeyValuePair<string, object>>) parameters)
      {
        if (!flag)
        {
          objectList.Add(parameter.Value);
          stringList.Add(parameter.Key);
        }
        view.Cache.SetValueExt(obj, parameter.Key, parameter.Value);
      }
    }
    catch (PXSetPropertyException ex)
    {
      return (object) null;
    }
    return this.FindExistingRow(view, obj, objectList.ToArray(), stringList.ToArray());
  }

  private object CreateNewRow(PXView view, IReadOnlyDictionary<string, object> parameters)
  {
    object instance = view.Cache.CreateInstance();
    IDictionary keyValues = this.GetKeyValues(instance, view, parameters);
    if (view.Cache.Keys.Count != keyValues.Count)
      return this.FindExistingRow(view, instance, keyValues.Values.OfType<object>().ToArray<object>(), keyValues.Keys.OfType<string>().ToArray<string>());
    view.Cache.Insert(keyValues);
    view.Cache.Update(keyValues, this.GetNotKeyValues(view, parameters));
    view.Cache.IsDirty = false;
    return view.Cache.Current;
  }

  private object FindExistingRow(
    PXView view,
    object current,
    object[] searches,
    string[] sortColumns)
  {
    int startRow = 0;
    int totalRows = 0;
    object existingRow;
    using (new PXReadThroughArchivedScope())
      existingRow = view.Select(new object[1]{ current }, (object[]) null, searches, sortColumns, new bool[1], (PXFilterRow[]) null, ref startRow, 1, ref totalRows).FirstOrDefault<object>();
    if (existingRow is PXResult pxResult)
    {
      foreach (System.Type substitutedType in PXSubstManager.GetSubstitutedTypes(view.CacheGetItemType(), view.Graph.GetType()))
      {
        if (pxResult[substitutedType] != null)
        {
          existingRow = pxResult[substitutedType];
          break;
        }
      }
    }
    return existingRow;
  }

  private IDictionary GetKeyValues(
    object row,
    PXView primaryView,
    IReadOnlyDictionary<string, object> parameters)
  {
    Dictionary<string, object> keyValues = new Dictionary<string, object>();
    foreach (string key1 in (IEnumerable<string>) primaryView.Cache.Keys)
    {
      string key = key1;
      KeyValuePair<string, object>[] array = parameters.Where<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>) (p => string.Equals(p.Key, key, StringComparison.OrdinalIgnoreCase))).ToArray<KeyValuePair<string, object>>();
      if (!((IEnumerable<KeyValuePair<string, object>>) array).Any<KeyValuePair<string, object>>())
      {
        object newValue;
        primaryView.Cache.RaiseFieldDefaulting(key, row, out newValue);
        if (newValue != null)
        {
          keyValues[key] = newValue;
          primaryView.Cache.SetValue(row, key, newValue);
        }
      }
      else
      {
        keyValues[key] = array[0].Value;
        primaryView.Cache.SetValue(row, key, array[0].Value);
      }
    }
    return (IDictionary) keyValues;
  }

  private IDictionary GetNotKeyValues(
    PXView primaryView,
    IReadOnlyDictionary<string, object> parameters)
  {
    Dictionary<string, object> notKeyValues = new Dictionary<string, object>();
    foreach (KeyValuePair<string, object> parameter in (IEnumerable<KeyValuePair<string, object>>) parameters)
    {
      if (!primaryView.Cache.Keys.Contains(parameter.Key))
        notKeyValues[parameter.Key] = parameter.Value;
    }
    return (IDictionary) notKeyValues;
  }

  private bool AreKeysFilled(PXCache cache, IReadOnlyDictionary<string, object> parameters)
  {
    return !cache.Keys.Except<string>(parameters.Keys, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).Any<string>();
  }
}
