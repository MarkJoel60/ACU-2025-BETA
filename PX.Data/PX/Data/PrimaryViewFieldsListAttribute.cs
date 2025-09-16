// Decompiled with JetBrains decompiler
// Type: PX.Data.PrimaryViewFieldsListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Automation.Services;
using PX.Data.Description;
using PX.Data.Reports;
using PX.Reports;
using PX.Reports.Controls;
using PX.Reports.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data;

/// <summary>
/// Implements PXStringList attribute with values from screen's primary view fields list.
/// </summary>
public class PrimaryViewFieldsListAttribute : ScreenInfoListAttribute
{
  private readonly HashSet<string> _ignoredFields = new HashSet<string>()
  {
    "NotePopupTextExists",
    "NotePopupText"
  };
  private bool _keysOnly;
  private bool _enabledOnly;

  /// <summary>
  /// If screen is an entry screen, shows only key fields in the list.
  /// </summary>
  public bool KeysOnly
  {
    get => this._keysOnly;
    set => this._keysOnly = value;
  }

  public bool EnabledOnly
  {
    get => this._enabledOnly;
    set => this._enabledOnly = value;
  }

  public bool VisibleOnly { get; set; }

  public bool ShowDisplayNameAsLabel { get; set; } = true;

  public bool ShowHiddenFields { get; set; }

  public bool ShowWorkflowStateField { get; set; } = true;

  public bool CopyPasteOptimization { get; set; } = true;

  [InjectDependencyOnTypeLevel]
  protected IReportLoaderService ReportLoader { get; private set; }

  public PrimaryViewFieldsListAttribute(System.Type nodeIdFieldType)
    : base(nodeIdFieldType)
  {
    this._AllowedValues = new string[1]{ string.Empty };
  }

  private IEnumerable<KeyValuePair<string, string>> CollectFieldNames(
    IEnumerable<PX.Data.Description.FieldInfo> fields,
    ISet<string> existingFieldNames)
  {
    foreach (PX.Data.Description.FieldInfo field in fields)
    {
      if (!existingFieldNames.Contains(field.FieldName) && (!this.EnabledOnly || field.IsEnabled) && (!this.VisibleOnly || !field.Invisible) && (!field.IsEditorControlDisabled || this.ShowHiddenFields))
      {
        existingFieldNames.Add(field.FieldName);
        string str = field.FieldName;
        if (this.ShowDisplayNameAsLabel)
          str = field.DisplayName;
        yield return new KeyValuePair<string, string>(field.FieldName, str);
      }
    }
  }

  private IEnumerable<KeyValuePair<string, string>> CollectFields(
    ScreenMetadata screenMetadata,
    string screenId,
    PXGraph graph)
  {
    PrimaryViewFieldsListAttribute fieldsListAttribute1 = this;
    if (screenMetadata.ScreenType != ScreenType.Unknown)
    {
      if (screenMetadata.ScreenType == ScreenType.Report)
      {
        string key = "PrimaryViewValueListAttribute$ReportParameters$" + screenId;
        ReportParameterCollection parameterCollection = PXContext.GetSlot<ReportParameterCollection>(key);
        if (parameterCollection == null)
        {
          Report report1 = fieldsListAttribute1.ReportLoader.LoadReport(screenId, (IPXResultset) null);
          PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenId);
          Report report2 = (screenIdUnsecure != null ? (PXSiteMap.IsARmReport(screenIdUnsecure.Url) ? 1 : 0) : 0) != 0 ? fieldsListAttribute1.ReportLoader.RenderARMReport(report1, (PXReportSettings) null) : report1;
          SoapNavigator soapNavigator = new SoapNavigator(graph);
          report2.DataSource = (object) soapNavigator;
          report2.Parameters.Process((IDataNavigator) soapNavigator, report2);
          parameterCollection = report2.Parameters;
          PXContext.SetSlot<ReportParameterCollection>(key, parameterCollection);
        }
        foreach (ReportParameter p in (List<ReportParameter>) parameterCollection)
        {
          if (p.IsVisible() && !string.IsNullOrEmpty(p.Prompt))
            yield return new KeyValuePair<string, string>(p.Name, fieldsListAttribute1.ShowDisplayNameAsLabel ? p.Prompt.Replace(':', ' ') : p.Name);
        }
      }
      else
      {
        PrimaryViewFieldsListAttribute fieldsListAttribute = fieldsListAttribute1;
        HashSet<string> existingNames = new HashSet<string>();
        List<PX.Data.Description.FieldInfo> fields = new List<PX.Data.Description.FieldInfo>();
        PXSiteMap.ScreenInfo screenInfo = screenMetadata.ScreenInfo;
        PXViewInfo graphView = GraphHelper.GetGraphView(screenInfo.GraphName, screenInfo.PrimaryView);
        if (graphView != null)
        {
          PXCacheInfo cacheInfo = graphView.Cache;
          IEnumerable<string> viewInfos = GraphHelper.GetGraphViews(screenInfo.GraphName, false, true).Where<PXViewInfo>((Func<PXViewInfo, bool>) (v => v.Cache.CacheType.IsAssignableFrom(cacheInfo.CacheType))).Select<PXViewInfo, string>((Func<PXViewInfo, string>) (v => v.Name));
          // ISSUE: reference to a compiler-generated method
          EnumerableExtensions.ForEach<KeyValuePair<string, PXViewDescription>>(screenInfo.Containers.Where<KeyValuePair<string, PXViewDescription>>((Func<KeyValuePair<string, PXViewDescription>, bool>) (c => viewInfos.Any<string>((Func<string, bool>) (v => Regex.IsMatch(c.Key, $"^{v}(: [\\d]+)?$"))))), (System.Action<KeyValuePair<string, PXViewDescription>>) (c => fields.AddRange(((IEnumerable<PX.Data.Description.FieldInfo>) c.Value.AllFields).Where<PX.Data.Description.FieldInfo>(new Func<PX.Data.Description.FieldInfo, bool>(fieldsListAttribute.\u003CCollectFields\u003Eb__35_8)))));
        }
        else
          fields.AddRange((IEnumerable<PX.Data.Description.FieldInfo>) screenMetadata.PrimaryView.AllFields);
        if (!fieldsListAttribute1.ShowWorkflowStateField)
        {
          string stateFieldName = fieldsListAttribute1.WorkflowService.GetWorkflowStatePropertyName(screenId);
          if (stateFieldName != null)
            fields = fields.Where<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (f => !f.FieldName.Equals(stateFieldName, StringComparison.OrdinalIgnoreCase))).ToList<PX.Data.Description.FieldInfo>();
        }
        if (fieldsListAttribute1.KeysOnly && screenMetadata.ScreenType == ScreenType.EntryScreen)
          fields = fields.Where<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (f => f.IsKey)).ToList<PX.Data.Description.FieldInfo>();
        IEnumerable<KeyValuePair<string, string>> source = (IEnumerable<KeyValuePair<string, string>>) fieldsListAttribute1.CollectFieldNames((IEnumerable<PX.Data.Description.FieldInfo>) fields, (ISet<string>) existingNames).OrderBy<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (f => f.Key));
        if (screenMetadata.ScreenType == ScreenType.InquiryWithFilter)
          source = source.Select<KeyValuePair<string, string>, KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, KeyValuePair<string, string>>) (f => new KeyValuePair<string, string>(f.Key, $"{f.Value} {PXLocalizer.Localize("(form filter)")}")));
        foreach (KeyValuePair<string, string> keyValuePair in source)
          yield return keyValuePair;
        if (screenMetadata.ScreenType == ScreenType.InquiryWithFilter)
        {
          foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) fieldsListAttribute1.CollectFieldNames((IEnumerable<PX.Data.Description.FieldInfo>) screenMetadata.DataView.AllFields, (ISet<string>) existingNames).OrderBy<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (f => f.Key)))
            yield return keyValuePair;
        }
        existingNames = (HashSet<string>) null;
      }
    }
  }

  private void SetValuesAndLabels(IEnumerable<KeyValuePair<string, string>> values)
  {
    List<string> allowedValues = new List<string>();
    List<string> allowedLabels = new List<string>();
    HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    HashSet<string> duplicatedLabels = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (KeyValuePair<string, string> keyValuePair in values)
    {
      string str = keyValuePair.Value;
      allowedValues.Add(keyValuePair.Key);
      allowedLabels.Add(str);
      if (!stringSet.Add(str))
        duplicatedLabels.Add(str);
    }
    if (duplicatedLabels.Any<string>())
      allowedLabels.Select<string, (string, int)>((Func<string, int, (string, int)>) ((label, index) => (label, index))).Where<(string, int)>((Func<(string, int), bool>) (x => duplicatedLabels.Contains(x.Label))).Select<(string, int), int>((Func<(string, int), int>) (x => x.Index)).ToList<int>().ForEach((System.Action<int>) (index =>
      {
        List<string> stringList = allowedLabels;
        int index1 = index;
        stringList[index1] = $"{stringList[index1]} ({allowedValues[index]})";
      }));
    this._AllowedValues = allowedValues.ToArray();
    this._AllowedLabels = allowedLabels.ToArray();
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    bool flag = e.Row != null;
    if (sender.Graph.IsCopyPasteContext && this.CopyPasteOptimization)
    {
      e.IsAltered = flag = false;
    }
    else
    {
      ScreenMetadata screenMetadata = this.GetScreenMetadata(sender);
      if (screenMetadata != null)
      {
        string screenId = this.GetScreenID(sender.Graph);
        this.SetValuesAndLabels(this.CollectFields(screenMetadata, screenId, PXGraph.CreateInstance<PXGraph>()));
      }
      else
      {
        string rawScreenId = this.GetRawScreenID(sender);
        if (!string.IsNullOrEmpty(rawScreenId))
          this.SetValuesAndLabels(NavigationTemplateHelper.GetTemplatePlaceholders(rawScreenId).Select<string, KeyValuePair<string, string>>((Func<string, KeyValuePair<string, string>>) (x => new KeyValuePair<string, string>(x, x))));
      }
    }
    if (flag)
      e.IsAltered = true;
    base.FieldSelecting(sender, e);
  }
}
