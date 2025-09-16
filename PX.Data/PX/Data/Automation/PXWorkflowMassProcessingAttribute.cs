// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.PXWorkflowMassProcessingAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Data.BQL;
using PX.Metadata;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable enable
namespace PX.Data.Automation;

/// <exclude />
[PXString]
[PXUIField(DisplayName = "Action")]
[PXUnboundDefault]
[PXStringList(IsLocalizable = false)]
public class PXWorkflowMassProcessingAttribute : PXAggregateAttribute, IPXRowSelectedSubscriber
{
  protected 
  #nullable disable
  List<string> _AllowedValues;
  public const string Undefined = "<SELECT>";

  [InjectDependency]
  protected IScreenInfoProvider ScreenInfoProvider { get; set; }

  public bool AddUndefinedState { get; set; } = true;

  /// <summary>Get, set.</summary>
  public string DisplayName
  {
    get => this.GetAttribute<PXUIFieldAttribute>().DisplayName;
    set => this.GetAttribute<PXUIFieldAttribute>().DisplayName = value;
  }

  /// <summary>Get, set.</summary>
  public bool Visible
  {
    get => this.GetAttribute<PXUIFieldAttribute>().Visible;
    set => this.GetAttribute<PXUIFieldAttribute>().Visible = value;
  }

  [InjectDependency]
  internal IAUWorkflowActionsEngine WorkflowActionsEngine { get; set; }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!string.IsNullOrEmpty(sender.GetValue(e.Row, this._FieldName) as string))
      return;
    sender.SetValue(e.Row, this._FieldName, (object) this.GetDefaultValue());
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    string val = e.ReturnValue as string;
    if (string.IsNullOrEmpty(val) || !(val != "<SELECT>") || this._AllowedValues == null || val.Contains<char>('$'))
      return;
    val += "$";
    string str = this._AllowedValues.FirstOrDefault<string>((Func<string, bool>) (s => !string.IsNullOrEmpty(s) && s.StartsWith(val)));
    if (str == null)
      return;
    e.ReturnValue = (object) str;
  }

  private string GetDefaultValue()
  {
    return this._AllowedValues != null && this._AllowedValues.Count != 0 ? (this._AllowedValues.Count != 1 && this.AddUndefinedState ? "<SELECT>" : this._AllowedValues[0]) : (!this.AddUndefinedState ? string.Empty : "<SELECT>");
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    List<string> stringList = new List<string>();
    List<(string, string)> source = new List<(string, string)>();
    IReadOnlyDictionary<string, AUScreenActionBaseState> processingActions = this.WorkflowActionsEngine.GetMassProcessingActions(sender.Graph);
    if (processingActions == null)
      return;
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    foreach (KeyValuePair<string, AUScreenActionBaseState> keyValuePair in (IEnumerable<KeyValuePair<string, AUScreenActionBaseState>>) processingActions)
    {
      KeyValuePair<string, AUScreenActionBaseState> action = keyValuePair;
      IScreenToGraphWorkflowMappingService workflowMappingService = (IScreenToGraphWorkflowMappingService) null;
      if (ServiceLocator.IsLocationProviderSet)
        workflowMappingService = ServiceLocator.Current.GetInstance<IScreenToGraphWorkflowMappingService>();
      string graphName = (string) null;
      if (!dictionary.TryGetValue(action.Value.ScreenID, out graphName))
      {
        graphName = workflowMappingService != null ? workflowMappingService.GetGraphTypeByScreenID(action.Value.ScreenID) : PXSiteMap.Provider.FindSiteMapNodeByScreenID(action.Value.ScreenID)?.GraphType;
        dictionary.Add(action.Value.ScreenID, graphName);
      }
      PXSiteMap.ScreenInfo withInvariantLocale = this.ScreenInfoProvider.GetWithInvariantLocale(action.Value.ScreenID);
      if (withInvariantLocale != null)
      {
        System.Type type = PXBuildManager.GetType(withInvariantLocale.PrimaryViewTypeName, false);
        PXCacheRights rights1;
        List<string> invisible;
        List<string> disabled;
        PXAccess.GetRights(action.Value.ScreenID, graphName, type, out rights1, out invisible, out disabled);
        byte? nullable = action.Value.MapViewRights;
        if (nullable.HasValue)
        {
          nullable = action.Value.MapViewRights;
          PXCacheRights? rights2 = PXWorkflowMassProcessingAttribute.CastCacheRightType(new byte?(nullable.Value));
          if (!PXWorkflowMassProcessingAttribute.HasRightsForMapping(rights1, rights2))
            continue;
        }
        nullable = action.Value.MapEnableRights;
        if (nullable.HasValue)
        {
          nullable = action.Value.MapEnableRights;
          PXCacheRights? rights3 = PXWorkflowMassProcessingAttribute.CastCacheRightType(new byte?(nullable.Value));
          if (!PXWorkflowMassProcessingAttribute.HasRightsForMapping(rights1, rights3))
            continue;
        }
        if ((invisible == null || invisible.Count == 0) && (disabled == null || disabled.Count == 0) || (disabled == null || !disabled.Contains<string>(action.Value.ActionName, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase)) && (invisible == null || !invisible.Contains<string>(action.Value.ActionName, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase)))
        {
          stringList.Add(action.Key);
          if (!string.IsNullOrEmpty(action.Value.DisplayName))
          {
            source.Add((graphName, action.Value.DisplayName));
          }
          else
          {
            string displayName = ((IEnumerable<PXSiteMap.ScreenInfo.Action>) withInvariantLocale.Actions).FirstOrDefault<PXSiteMap.ScreenInfo.Action>((Func<PXSiteMap.ScreenInfo.Action, bool>) (it => it.Name.Equals(action.Value.ActionName, StringComparison.OrdinalIgnoreCase)))?.DisplayName;
            source.Add((graphName, displayName));
          }
        }
      }
    }
    if (stringList.Count == 0)
      return;
    if (stringList.Count > 1 && this.AddUndefinedState)
    {
      source.Insert(0, ((string) null, PX.SM.Messages.GetLocal("<SELECT>")));
      stringList.Insert(0, "<SELECT>");
    }
    for (int index = 0; index < source.Count; ++index)
      source[index] = (source[index].Item1, PXLocalizer.Localize(source[index].Item2, source[index].Item1));
    PXStringListAttribute.SetList(sender, (object) null, this._FieldName, stringList.ToArray(), source.Select<(string, string), string>((Func<(string, string), string>) (item => item.label)).ToArray<string>());
    this._AllowedValues = stringList;
    this.GetAttribute<PXDefaultAttribute>().Constant = (object) this.GetDefaultValue();
  }

  private static bool HasRightsForMapping(PXCacheRights pXCacheRights, PXCacheRights? rights)
  {
    bool flag;
    if (rights.HasValue)
    {
      switch (rights.GetValueOrDefault())
      {
        case PXCacheRights.Update:
          flag = pXCacheRights == PXCacheRights.Delete || pXCacheRights == PXCacheRights.Update || pXCacheRights == PXCacheRights.Insert;
          goto label_6;
        case PXCacheRights.Insert:
          flag = pXCacheRights == PXCacheRights.Delete || pXCacheRights == PXCacheRights.Insert;
          goto label_6;
        case PXCacheRights.Delete:
          flag = pXCacheRights == PXCacheRights.Delete;
          goto label_6;
      }
    }
    flag = pXCacheRights != 0;
label_6:
    return flag;
  }

  private static PXCacheRights? CastCacheRightType(byte? type)
  {
    PXCacheRights? nullable = new PXCacheRights?();
    if (type.HasValue && Enum.IsDefined(typeof (PXCacheRights), (object) type))
      nullable = new PXCacheRights?((PXCacheRights) type.Value);
    return nullable;
  }

  /// <exclude />
  public class undefinded : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PXWorkflowMassProcessingAttribute.undefinded>
  {
    public undefinded()
      : base("<SELECT>")
    {
    }
  }
}
