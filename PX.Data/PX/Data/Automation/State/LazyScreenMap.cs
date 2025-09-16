// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.LazyScreenMap
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Process.Automation.State;
using PX.Data.WorkflowAPI;
using PX.SM;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Automation.State;

internal sealed class LazyScreenMap : IScreenMap
{
  private readonly IPXPageIndexingService _pageIndexingService;
  private readonly ConcurrentDictionary<string, Screen> _screenStates = new ConcurrentDictionary<string, Screen>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  private readonly ConcurrentDictionary<string, string[]> _tableToScreensIds = new ConcurrentDictionary<string, string[]>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  public LazyScreenMap(IPXPageIndexingService pageIndexingService)
  {
    this._pageIndexingService = pageIndexingService;
  }

  public Screen GetByScreen(string screenID)
  {
    Screen screen = screenID != null ? this._screenStates.GetOrAdd(screenID, new Func<string, Screen>(this.LoadForScreen)) : throw new ArgumentNullException(nameof (screenID));
    return !screen.IsEmpty() ? screen : (Screen) null;
  }

  public Screen GetByGraph(string graphName)
  {
    string screenIdFromGraphType = this._pageIndexingService.GetScreenIDFromGraphType(graphName);
    return screenIdFromGraphType == null ? (Screen) null : this.GetByScreen(screenIdFromGraphType);
  }

  public bool ContainsGraph(string graphName) => this.GetByGraph(graphName) != null;

  private string[] GetByTable(string tableName)
  {
    return tableName != null ? this._tableToScreensIds.GetOrAdd(tableName, new Func<string, string[]>(this.LoadForTable)) : throw new ArgumentNullException(nameof (tableName));
  }

  public IEnumerable<ScreenTable> GetAllScreenTables(string tableName)
  {
    string[] strArray = this.GetByTable(tableName);
    for (int index = 0; index < strArray.Length; ++index)
    {
      ScreenTable allScreenTable;
      if (this.GetByScreen(strArray[index]).Tables.TryGetValue(tableName, out allScreenTable))
        yield return allScreenTable;
    }
    strArray = (string[]) null;
  }

  public IEnumerable<ScreenTableField> GetAllScreenFields(string tableName, string field)
  {
    foreach (ScreenTable allScreenTable in this.GetAllScreenTables(tableName))
    {
      ScreenTableField allScreenField;
      if (allScreenTable.Fields.TryGetValue(field, out allScreenField))
        yield return allScreenField;
    }
  }

  private string[] LoadForTable(string tableName)
  {
    return PXSystemWorkflows.SelectTable<AUScreenFieldState>().Where<AUScreenFieldState>((Func<AUScreenFieldState, bool>) (record => string.Equals(tableName, record.TableName, StringComparison.OrdinalIgnoreCase))).Select<AUScreenFieldState, string>((Func<AUScreenFieldState, string>) (record => record.ScreenID)).ToArray<string>();
  }

  private Screen LoadForScreen(string screenID)
  {
    Screen screen = new Screen();
    bool isCustomized = false;
    foreach (AUScreenConditionState screenConditionState in PXSystemWorkflows.SelectTable<AUScreenConditionState>(screenID, ref isCustomized, true))
    {
      StateMap<ScreenCondition> conditions = screen.Conditions;
      string key = screenConditionState.ConditionID.ToString();
      ScreenCondition screenCondition = new ScreenCondition();
      bool? nullable1 = screenConditionState.AppendSystemCondition;
      bool flag1 = true;
      screenCondition.ParentCondition = !(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue) ? (string) null : screenConditionState.ParentCondition;
      nullable1 = screenConditionState.AppendSystemCondition;
      bool flag2 = true;
      bool? nullable2;
      if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
      {
        nullable2 = new bool?(screenConditionState.JoinMethod == "AND");
      }
      else
      {
        nullable1 = new bool?();
        nullable2 = nullable1;
      }
      screenCondition.ParentConditionMethodAnd = nullable2;
      screenCondition.InternalImplementation = screenConditionState.InternalImplementation;
      nullable1 = screenConditionState.InvertCondition;
      screenCondition.InvertCondition = nullable1.GetValueOrDefault();
      conditions.Add(key, screenCondition);
      screen.IsCustomized = isCustomized;
    }
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<AUScreenConditionLineState>((PXDataField) new PXDataField<AUScreenConditionLineState.screenID>(), (PXDataField) new PXDataField<AUScreenConditionLineState.conditionID>(), (PXDataField) new PXDataField<AUScreenConditionLineState.fieldName>(), (PXDataField) new PXDataField<AUScreenConditionLineState.condition>(), (PXDataField) new PXDataField<AUScreenConditionLineState.value>(), (PXDataField) new PXDataField<AUScreenConditionLineState.value2>(), (PXDataField) new PXDataField<AUScreenConditionLineState.openBrackets>(), (PXDataField) new PXDataField<AUScreenConditionLineState.closeBrackets>(), (PXDataField) new PXDataField<AUScreenConditionLineState.operatoR>(), (PXDataField) new PXDataField<AUScreenConditionLineState.isFromScheme>(), (PXDataField) new PXDataFieldValue<AUScreenConditionLineState.isActive>(PXDbType.Bit, new int?(1), (object) 1), (PXDataField) new PXDataFieldOrder<AUScreenConditionLineState.screenID>(), (PXDataField) new PXDataFieldOrder<AUScreenConditionLineState.conditionID>(), (PXDataField) new PXDataFieldOrder<AUScreenConditionLineState.lineNbr>()))
    {
      ScreenCondition screenCondition;
      if (string.Equals(pxDataRecord.GetString(0), screenID, StringComparison.OrdinalIgnoreCase) && screen.Conditions.TryGetValue(pxDataRecord.GetGuid(1).ToString(), out screenCondition))
      {
        List<ScreenConditionFilter> filters = screenCondition.Filters;
        string fieldName = pxDataRecord.GetString(2);
        int? int32 = pxDataRecord.GetInt32(3);
        int condition = int32.Value;
        string str1 = pxDataRecord.GetString(4);
        string str2 = pxDataRecord.GetString(5);
        int32 = pxDataRecord.GetInt32(6);
        int openBrackets = int32.Value;
        int32 = pxDataRecord.GetInt32(7);
        int closeBrackets = int32.Value;
        int32 = pxDataRecord.GetInt32(8);
        int oper = int32.Value;
        int num = !pxDataRecord.GetBoolean(9).Value ? 1 : 0;
        ScreenConditionFilter screenConditionFilter = new ScreenConditionFilter(fieldName, condition, str1, str2, openBrackets, closeBrackets, oper, num != 0);
        filters.Add(screenConditionFilter);
      }
    }
    this.LoadActions(screenID, screen);
    this.LoadNavigationActions(screenID, screen);
    this.LoadFields(screenID, screen);
    return screen;
  }

  private void LoadFields(string screenID, Screen screen)
  {
    bool isCustomized = false;
    foreach (AUScreenFieldState screenFieldState in PXSystemWorkflows.SelectTable<AUScreenFieldState>(screenID, ref isCustomized, true))
    {
      screen.IsCustomized |= isCustomized;
      string tableName = screenFieldState.TableName;
      ScreenTable screenTable;
      if (!screen.Tables.TryGetValue(tableName, out screenTable))
        screen.Tables.Add(tableName, screenTable = new ScreenTable(tableName));
      string fieldName = screenFieldState.FieldName;
      screenTable.Fields.Add(fieldName, new ScreenTableField(fieldName, screenFieldState.DisplayName, screenFieldState.IsRequired, screenFieldState.RequiredCondition, screenFieldState.DisableCondition, screenFieldState.HideCondition, screenFieldState.ComboBoxValues, screenFieldState.IsFromSchema.GetValueOrDefault(), screenFieldState.DefaultValue));
    }
  }

  private void LoadActions(string screenID, Screen screen)
  {
    bool isCustomized = false;
    foreach (AUScreenActionState screenActionState in PXSystemWorkflows.SelectTable<AUScreenActionState>(screenID, ref isCustomized, true))
    {
      screen.IsCustomized |= isCustomized;
      StateMap<ScreenActionBase> actions = screen.Actions;
      string actionName1 = screenActionState.ActionName;
      string actionName2 = screenActionState.ActionName;
      string dataMember = screenActionState.DataMember;
      string method = screenActionState.Method;
      string displayName = screenActionState.DisplayName;
      PXSpecialButtonType? actionFolderType = this.CastButtonType(screenActionState.ActionFolderType);
      PXSpecialButtonType? menuFolderType = this.CastButtonType(screenActionState.MenuFolderType);
      string menuFolder = screenActionState.MenuFolder;
      bool? isTopLevel = screenActionState.IsTopLevel;
      string disableCondition = screenActionState.DisableCondition;
      string hideCondition = screenActionState.HideCondition;
      string before = screenActionState.Before;
      string after = screenActionState.After;
      byte? placementInCategory = screenActionState.PlacementInCategory;
      Placement? nullable1;
      Placement? nullable2;
      if (!placementInCategory.HasValue)
      {
        nullable1 = new Placement?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new Placement?((Placement) placementInCategory.GetValueOrDefault());
      nullable1 = nullable2;
      int valueOrDefault = (int) nullable1.GetValueOrDefault();
      string afterInMenu = screenActionState.AfterInMenu;
      string category = screenActionState.Category;
      PXCacheRights? mapEnableRights = this.CastCacheRightType(screenActionState.MapEnableRights);
      PXCacheRights? mapViewRights = this.CastCacheRightType(screenActionState.MapViewRights);
      bool? isLockedOnToolbar = screenActionState.IsLockedOnToolbar;
      bool? archiveDisabling = screenActionState.IgnoresArchiveDisabling;
      string connotation = screenActionState.Connotation;
      ScreenActionExtraData extraData = screenActionState.ExtraData;
      ScreenAction screenAction = new ScreenAction(actionName2, dataMember, method, displayName, actionFolderType, menuFolderType, menuFolder, isTopLevel, disableCondition, hideCondition, before, after, (Placement) valueOrDefault, afterInMenu, category, mapEnableRights, mapViewRights, isLockedOnToolbar, archiveDisabling, connotation, extraData);
      actions.Add(actionName1, (ScreenActionBase) screenAction);
    }
  }

  private PXSpecialButtonType? CastButtonType(int? type)
  {
    PXSpecialButtonType? nullable = new PXSpecialButtonType?();
    if (type.HasValue && Enum.IsDefined(typeof (PXSpecialButtonType), (object) type))
      nullable = new PXSpecialButtonType?((PXSpecialButtonType) type.Value);
    return nullable;
  }

  private PXCacheRights? CastCacheRightType(byte? type)
  {
    PXCacheRights? nullable = new PXCacheRights?();
    if (type.HasValue && Enum.IsDefined(typeof (PXCacheRights), (object) type))
      nullable = new PXCacheRights?((PXCacheRights) type.Value);
    return nullable;
  }

  private void LoadNavigationActions(string screenID, Screen screen)
  {
    bool isCustomized = false;
    foreach (AUScreenNavigationActionState navigationActionState in PXSystemWorkflows.SelectTable<AUScreenNavigationActionState>(screenID, ref isCustomized, true))
    {
      screen.IsCustomized |= isCustomized;
      StateMap<ScreenActionBase> actions = screen.Actions;
      string actionName1 = navigationActionState.ActionName;
      string actionName2 = navigationActionState.ActionName;
      string dataMember = navigationActionState.DataMember;
      string destinationScreenId = navigationActionState.DestinationScreenID;
      int num = (int) PXWindowModeAttribute.Convert(navigationActionState.WindowMode);
      string displayName = navigationActionState.DisplayName;
      string icon = navigationActionState.Icon;
      PXSpecialButtonType? menuFolderType = this.CastButtonType(navigationActionState.MenuFolderType);
      string menuFolder = navigationActionState.MenuFolder;
      bool? isTopLevel = navigationActionState.IsTopLevel;
      string disableCondition = navigationActionState.DisableCondition;
      string hideCondition = navigationActionState.HideCondition;
      string before = navigationActionState.Before;
      string after = navigationActionState.After;
      byte? placementInCategory = navigationActionState.PlacementInCategory;
      Placement? nullable1;
      Placement? nullable2;
      if (!placementInCategory.HasValue)
      {
        nullable1 = new Placement?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new Placement?((Placement) placementInCategory.GetValueOrDefault());
      nullable1 = nullable2;
      int valueOrDefault = (int) nullable1.GetValueOrDefault();
      string afterInMenu = navigationActionState.AfterInMenu;
      string category = navigationActionState.Category;
      PXCacheRights? mapEnableRights = this.CastCacheRightType(navigationActionState.MapEnableRights);
      PXCacheRights? mapViewRights = this.CastCacheRightType(navigationActionState.MapViewRights);
      bool? isLockedOnToolbar = navigationActionState.IsLockedOnToolbar;
      bool? archiveDisabling = navigationActionState.IgnoresArchiveDisabling;
      string connotation = navigationActionState.Connotation;
      ScreenNavigationAction navigationAction = new ScreenNavigationAction(actionName2, dataMember, destinationScreenId, (PXBaseRedirectException.WindowMode) num, displayName, icon, menuFolderType, menuFolder, isTopLevel, disableCondition, hideCondition, before, after, (Placement) valueOrDefault, afterInMenu, category, mapEnableRights, mapViewRights, isLockedOnToolbar, archiveDisabling, connotation);
      actions.Add(actionName1, (ScreenActionBase) navigationAction);
    }
    this.PrefetchNavigationParameters(screenID, screen);
  }

  private void PrefetchNavigationParameters(string screenID, Screen screen)
  {
    bool isCustomized = false;
    foreach (AUScreenNavigationParameterState navigationParameterState in PXSystemWorkflows.SelectTable<AUScreenNavigationParameterState>(screenID, ref isCustomized, true))
    {
      screen.IsCustomized |= isCustomized;
      string actionName;
      ScreenActionBase screenActionBase;
      screen.Actions.TryGetValue(actionName = navigationParameterState.ActionName, out screenActionBase);
      if (!(screenActionBase is ScreenNavigationAction navigationAction))
        break;
      string fieldName = navigationParameterState.FieldName;
      navigationAction.Parameters.Add(fieldName, new ScreenNavigationParameter(actionName, fieldName, navigationParameterState.Value, navigationParameterState.IsFromSchema.Value));
    }
  }
}
