// Decompiled with JetBrains decompiler
// Type: PX.Data.PrimaryViewValueListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data.Description;
using PX.Data.Reports;
using PX.Reports;
using PX.Reports.ARm;
using PX.Reports.Controls;
using PX.Reports.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <summary>
/// Provides correct value type and list for specified field.
/// </summary>
/// <remarks>
/// This attribute inherits from PXAggregateAttribute because it have to specify strict order
/// of calls to event handlers in List and PXDBString attributes.
/// </remarks>
public class PrimaryViewValueListAttribute : PXAggregateAttribute
{
  public virtual bool IsKey
  {
    get => this.DBStringAttr.IsKey;
    set => this.DBStringAttr.IsKey = value;
  }

  protected virtual PXDBStringAttribute DBStringAttr => (PXDBStringAttribute) this._Attributes[1];

  protected PrimaryViewValueListAttribute.GIListInternal ListAttr
  {
    get => (PrimaryViewValueListAttribute.GIListInternal) this._Attributes[0];
  }

  private PrimaryViewValueListAttribute(int? length, System.Type screenIdFieldType, System.Type dataFieldType)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PrimaryViewValueListAttribute.GIListInternal(screenIdFieldType, dataFieldType));
    PXDBStringAttribute pxdbStringAttribute = !length.HasValue ? new PXDBStringAttribute() : new PXDBStringAttribute(length.Value);
    pxdbStringAttribute.InputMask = "";
    pxdbStringAttribute.IsUnicode = true;
    this._Attributes.Add((PXEventSubscriberAttribute) pxdbStringAttribute);
  }

  public PrimaryViewValueListAttribute(System.Type screenIdFieldType, System.Type dataFieldType)
    : this(new int?(), screenIdFieldType, dataFieldType)
  {
  }

  public PrimaryViewValueListAttribute(int length, System.Type screenIdFieldType, System.Type dataFieldType)
    : this(new int?(length), screenIdFieldType, dataFieldType)
  {
  }

  public virtual bool IsActive
  {
    get => this.ListAttr.IsActive;
    set => this.ListAttr.IsActive = value;
  }

  public void SetList(PXCache cache, string[] values, string[] labels)
  {
    this.ListAttr.SetList(cache, values, labels);
  }

  /// <exclude />
  protected class GIListInternal : ScreenInfoListAttribute, IPXFieldUpdatingSubscriber
  {
    private readonly System.Type _dataFieldType;
    private readonly string _dataFieldName;

    internal bool IsActive { get; set; } = true;

    [InjectDependencyOnTypeLevel]
    protected IReportLoaderService ReportLoader { get; private set; }

    /// <param name="screenIdFieldType">Screen node id.</param>
    /// <param name="dataFieldType">Field that need to be provided with value.</param>
    public GIListInternal(System.Type screenIdFieldType, System.Type dataFieldType)
      : base(screenIdFieldType)
    {
      if (dataFieldType == (System.Type) null)
        throw new PXArgumentException(nameof (dataFieldType), "The argument cannot be null.");
      this._dataFieldType = dataFieldType.IsNested && typeof (IBqlField).IsAssignableFrom(dataFieldType) ? BqlCommand.GetItemType(dataFieldType) : throw new PXArgumentException(nameof (dataFieldType), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
      {
        (object) dataFieldType
      });
      this._dataFieldName = dataFieldType.Name;
      this.ExclusiveValues = false;
    }

    public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      if (this.IsActive)
      {
        if (e.Row == null)
          return;
        string fieldName = (string) sender.Graph.Caches[this._dataFieldType].GetValue(e.Row, this._dataFieldName);
        if (string.IsNullOrEmpty(fieldName))
          return;
        ScreenMetadata screenMetadata = this.GetScreenMetadata(sender);
        if (screenMetadata == null)
          return;
        string screenId = this.GetScreenID(sender.Graph);
        PXFieldState pxFieldState = (PXFieldState) null;
        if (screenMetadata.ScreenType == ScreenType.Report)
          pxFieldState = this.GetStateForReport(sender.Graph, screenId, e.ReturnState, fieldName);
        else if (screenMetadata.ScreenType != ScreenType.Unknown)
        {
          pxFieldState = this.GetState(sender.Graph, screenMetadata, fieldName);
          if (pxFieldState == null && !string.IsNullOrEmpty(screenId))
          {
            if (screenMetadata.ScreenType == ScreenType.Inquiry || screenMetadata.ScreenType == ScreenType.InquiryWithFilter)
              pxFieldState = this.GetStateForInquiry(sender.Graph, screenMetadata, screenId, fieldName);
            else if (screenMetadata.ScreenType == ScreenType.Dashboard)
              pxFieldState = this.GetStateForDashboard(sender.Graph, screenMetadata, screenId, fieldName);
          }
        }
        if (pxFieldState == null)
          return;
        pxFieldState.Enabled = true;
        pxFieldState.Visible = true;
        if (pxFieldState.SelectorMode != PXSelectorMode.DisplayModeText)
          pxFieldState.DescriptionName = (string) null;
        pxFieldState.Value = sender.GetValue(e.Row, this.FieldName);
        if (pxFieldState.DataType == typeof (bool) && pxFieldState.Value == null)
          sender.SetValue(e.Row, this.FieldName, (object) bool.FalseString);
        e.Cancel = true;
        e.ReturnState = (object) pxFieldState;
      }
      else
      {
        base.FieldSelecting(sender, e);
        e.Cancel = true;
      }
    }

    private PXFieldState GetStateForReport(
      PXGraph graph,
      string screenId,
      object originalState,
      string fieldName)
    {
      stateForReport = (PXFieldState) null;
      if (!string.IsNullOrEmpty(screenId))
      {
        string key1 = "PrimaryViewValueListAttribute$ReportParameters$" + screenId;
        ReportParameterCollection parameterCollection = PXContext.GetSlot<ReportParameterCollection>(key1);
        if (parameterCollection == null)
        {
          Report report1 = this.ReportLoader.LoadReport(screenId, (IPXResultset) null);
          PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenId);
          Report report2 = (screenIdUnsecure != null ? (PXSiteMap.IsARmReport(screenIdUnsecure.Url) ? 1 : 0) : 0) != 0 ? this.ReportLoader.RenderARMReport(report1, (PXReportSettings) null) : report1;
          SoapNavigator soapNavigator = new SoapNavigator(graph);
          report2.DataSource = (object) soapNavigator;
          report2.Parameters.Process((IDataNavigator) soapNavigator, report2);
          parameterCollection = report2.Parameters;
          PXContext.SetSlot<ReportParameterCollection>(key1, parameterCollection);
        }
        ReportParameter reportParameter = parameterCollection[fieldName];
        if (reportParameter != null && !(reportParameter.ProcessedView is PXFieldState stateForReport))
        {
          string key2 = reportParameter.ViewName;
          if (string.IsNullOrEmpty(key2) || !graph.Views.ContainsKey(key2) && (!(reportParameter is ARmReport.ARmReportParameter) || !graph.Views.TryGetOrCreateValue(key2, out PXView _) && !graph.Views.TryGetOrCreateValue("_Cache#PX.CS.RMDataSource" + key2, out PXView _) && !graph.Views.ContainsKey(key2)))
            key2 = (string) null;
          object obj = originalState;
          bool? isKey = new bool?();
          bool? nullable = new bool?(reportParameter.Nullable);
          int? required = new int?(Convert.ToInt32((object) reportParameter.Required));
          int? precision = new int?();
          int? length = new int?();
          object processedDefault = reportParameter.ProcessedDefault;
          string fieldName1 = this.FieldName;
          bool? enabled = new bool?();
          bool? visible = reportParameter.Visible;
          bool? readOnly = new bool?();
          string viewName = key2;
          PXFieldState pxFieldState = stateForReport = PXFieldState.CreateInstance(obj, (System.Type) null, isKey, nullable, required, precision, length, processedDefault, fieldName1, enabled: enabled, visible: visible, readOnly: readOnly, viewName: viewName);
          switch ((int) reportParameter.Type)
          {
            case 0:
              stateForReport = PXFieldState.CreateInstance((object) pxFieldState, typeof (bool));
              break;
            case 1:
              stateForReport = PXDateState.CreateInstance((object) pxFieldState, (string) null, new bool?(), new int?(), reportParameter.ProcessedMask, (string) null, new System.DateTime?(), new System.DateTime?());
              break;
            case 2:
              stateForReport = PXFloatState.CreateInstance((object) pxFieldState, new int?(), (string) null, new bool?(), new int?(), new float?(), new float?());
              break;
            case 3:
              stateForReport = PXIntState.CreateInstance((object) pxFieldState, (string) null, new bool?(), new int?(), new int?(), new int?(), ((List<ParameterValue>) reportParameter.ValidValues).Count > 0 ? ((IEnumerable<ParameterValue>) reportParameter.ValidValues).Select<ParameterValue, int>((Func<ParameterValue, int>) (v => int.Parse(v.Value))).ToArray<int>() : (int[]) null, ((List<ParameterValue>) reportParameter.ValidValues).Count > 0 ? ((IEnumerable<ParameterValue>) reportParameter.ValidValues).Select<ParameterValue, string>((Func<ParameterValue, string>) (v => v.Label)).ToArray<string>() : (string[]) null, typeof (int), new int?(), (string[]) null);
              break;
            case 4:
              stateForReport = PXStringState.CreateInstance((object) pxFieldState, new int?(), new bool?(), (string) null, new bool?(), new int?(), reportParameter.ProcessedMask, ((List<ParameterValue>) reportParameter.ValidValues).Count > 0 ? ((IEnumerable<ParameterValue>) reportParameter.ValidValues).Select<ParameterValue, string>((Func<ParameterValue, string>) (v => v.Value)).ToArray<string>() : (string[]) null, ((List<ParameterValue>) reportParameter.ValidValues).Count > 0 ? ((IEnumerable<ParameterValue>) reportParameter.ValidValues).Select<ParameterValue, string>((Func<ParameterValue, string>) (v => v.Label)).ToArray<string>() : (string[]) null, new bool?(false), reportParameter.ProcessedDefault as string);
              break;
          }
          if (stateForReport != null && !string.IsNullOrEmpty(stateForReport.ViewName) && stateForReport.FieldList == null)
          {
            List<string> stringList1 = new List<string>();
            List<string> stringList2 = new List<string>();
            foreach (PXFieldState field in PXFieldState.GetFields(graph, graph.Views[stateForReport.ViewName].BqlSelect.GetTables(), false))
            {
              if (field != null && (field.Visibility & PXUIVisibility.SelectorVisible) == PXUIVisibility.SelectorVisible)
              {
                stringList1.Add(field.Name);
                stringList2.Add(field.DisplayName);
              }
            }
            stateForReport.FieldList = stringList1.ToArray();
            stateForReport.HeaderList = stringList2.ToArray();
          }
        }
      }
      return stateForReport;
    }

    private PXFieldState GetStateForInquiry(
      PXGraph graph,
      ScreenMetadata screenMetadata,
      string screenId,
      string fieldName)
    {
      PXGraph dataGraph = ServiceLocator.Current.GetInstance<IDataScreenFactory>().CreateDataScreen(screenId)?.DataGraph;
      PXFieldState state = dataGraph == null ? (PXFieldState) null : this.GetState(dataGraph, screenMetadata, fieldName);
      this.CreateViewLink(graph, dataGraph, state);
      return state;
    }

    private PXFieldState GetStateForDashboard(
      PXGraph graph,
      ScreenMetadata screenMetadata,
      string screenId,
      string fieldName)
    {
      System.Type type = PXBuildManager.GetType(screenMetadata.ScreenInfo.GraphName, false);
      using (new PXScreenIDScope(screenId))
      {
        PXGraph instance = PXGraph.CreateInstance(type);
        PXFieldState state = this.GetState(instance, screenMetadata, fieldName);
        this.CreateViewLink(graph, instance, state);
        return state;
      }
    }

    private PXFieldState GetState(PXGraph graph, ScreenMetadata screenMetadata, string fieldName)
    {
      PXCache cache = this.GetCache(graph, screenMetadata.ScreenInfo, screenMetadata.PrimaryView);
      if (cache == null || !cache.Fields.Contains(fieldName))
        cache = this.GetCache(graph, screenMetadata.ScreenInfo, screenMetadata.DataView);
      return cache?.GetStateExt((object) null, fieldName) as PXFieldState;
    }

    private void CreateViewLink(PXGraph graph, PXGraph valueGraph, PXFieldState state)
    {
      if (string.IsNullOrEmpty(state?.ViewName) || graph.Views.ContainsKey(state.ViewName) || !valueGraph.Views.ContainsKey(state.ViewName))
        return;
      string key = "Forwarded$" + state.ViewName;
      graph.Views[key] = valueGraph.Views[state.ViewName];
      state.ViewName = key;
    }

    private PXCache GetCache(
      PXGraph graph,
      PXSiteMap.ScreenInfo screenInfo,
      PXViewDescription view)
    {
      PXCacheInfo cache = GraphHelper.GetGraphView(screenInfo.GraphName, view.ViewName, true).Cache;
      return cache != null ? graph.Caches[cache.CacheType] : (PXCache) null;
    }

    public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      if (!this.IsActive || e.NewValue == null)
        return;
      e.NewValue = (object) e.NewValue.ToString();
    }

    internal void SetList(PXCache cache, string[] values, string[] labels)
    {
      cache.SetAltered(this.FieldName, true);
      PXStringListAttribute.SetListInternal((IEnumerable<PXStringListAttribute>) EnumerableExtensions.AsSingleEnumerable<PrimaryViewValueListAttribute.GIListInternal>(this), values, labels, cache);
    }
  }
}
