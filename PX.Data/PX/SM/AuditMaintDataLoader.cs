// Decompiled with JetBrains decompiler
// Type: PX.SM.AuditMaintDataLoader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.CS;
using PX.Data;
using PX.Data.Description;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

public class AuditMaintDataLoader
{
  private readonly string screenIDForAudit;
  private readonly PXGraph auditGraph;
  private readonly PXCache tableCache;
  private readonly PXCache fieldCache;

  public AuditMaintDataLoader(
    string screenIDForAudit,
    PXGraph auditGraph,
    PXCache tableCache,
    PXCache fieldCache)
  {
    this.screenIDForAudit = screenIDForAudit;
    this.auditGraph = auditGraph;
    this.tableCache = tableCache;
    this.fieldCache = fieldCache;
  }

  public void LoadTablesAndFields(int? typeOfFieldsToShow)
  {
    this.LoadTables(typeOfFieldsToShow);
    foreach (object table in this.tableCache.Cached)
      this.LoadFields(table as AUAuditTable);
  }

  private void LoadTables(int? typeOfFieldsToShow)
  {
    bool isDirty = this.tableCache.IsDirty;
    this.LoadTablesFromDB();
    this.LoadTablesFromScreen(typeOfFieldsToShow);
    this.tableCache.IsDirty = isDirty;
  }

  private void LoadFields(AUAuditTable table)
  {
    if (table == null)
      return;
    bool isDirty = this.fieldCache.IsDirty;
    PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(this.screenIDForAudit);
    PXCache fieldsTableCache = (PXCache) null;
    if (screenIdUnsecure != null && screenIdUnsecure.GraphType != null)
    {
      PXGraph instance = PXGraph.CreateInstance(PXBuildManager.GetType(screenIdUnsecure.GraphType, false));
      if (table.TableType != null)
      {
        System.Type type = PXBuildManager.GetType(table.TableType, false);
        fieldsTableCache = type != (System.Type) null ? instance.Caches[type] : (PXCache) null;
      }
      else
        fieldsTableCache = instance.Caches[table.TableName];
    }
    if (fieldsTableCache == null)
      return;
    this.LoadFieldsFromDB(table.TableName, fieldsTableCache);
    this.LoadFieldsFromTable(table.TableName, fieldsTableCache);
    this.fieldCache.IsDirty = isDirty;
  }

  private void LoadTablesFromDB()
  {
    if (this.auditGraph == null || this.tableCache == null)
      return;
    foreach (PXResult<AUAuditTable> pxResult in PXSelectBase<AUAuditTable, PXSelectReadonly<AUAuditTable, Where<AUAuditTable.screenID, Equal<Current<AUAuditSetup.screenID>>>>.Config>.Select(this.auditGraph))
    {
      AUAuditTable auAuditTable = (AUAuditTable) pxResult;
      auAuditTable.IsInserted = new bool?(false);
      this.tableCache.Insert((object) auAuditTable);
      this.tableCache.SetStatus((object) auAuditTable, PXEntryStatus.Held);
    }
  }

  private void LoadFieldsFromDB(string fieldsTableName, PXCache fieldsTableCache)
  {
    if (string.IsNullOrEmpty(fieldsTableName) || this.tableCache == null)
      return;
    foreach (PXResult<AUAuditField> pxResult in PXSelectBase<AUAuditField, PXSelectReadonly<AUAuditField, Where<AUAuditField.screenID, Equal<Current<AUAuditSetup.screenID>>, And<AUAuditField.tableName, Equal<Required<AUAuditTable.tableName>>>>>.Config>.Select(this.auditGraph, (object) fieldsTableName))
    {
      AUAuditField auAuditField = (AUAuditField) pxResult;
      if (this.fieldCache.Locate((object) auAuditField) == null)
      {
        auAuditField.IsInserted = new bool?(false);
        if (fieldsTableCache != null)
          auAuditField.FieldType = new int?(TablesAndFieldsInfoExtractor.GetFieldType(fieldsTableCache, auAuditField.FieldName));
        this.fieldCache.Insert((object) auAuditField);
        this.fieldCache.SetStatus((object) auAuditField, PXEntryStatus.Held);
      }
    }
  }

  private void LoadTablesFromScreen(int? typeOfFieldsToShow)
  {
    if (string.IsNullOrEmpty(this.screenIDForAudit) || this.tableCache == null)
      return;
    PXGraph graphForAudit;
    foreach ((System.Type type, string str, string TableTypeStr) in AuditMaintDataLoader.GetTablesFromScreen(this.screenIDForAudit, out graphForAudit))
      this.PlaceAuditTableToCache(new AUAuditTable()
      {
        ScreenID = this.screenIDForAudit,
        TableName = str,
        IsInserted = new bool?(true),
        TableType = TableTypeStr,
        TableDisplayName = TablesAndFieldsInfoExtractor.GetTableDisplayName(type, str),
        ShowFieldsType = typeOfFieldsToShow
      }, type, graphForAudit);
  }

  internal static IEnumerable<(System.Type TableType, string TableName, string TableTypeStr)> GetTablesFromScreen(
    string screenId,
    out PXGraph graphForAudit)
  {
    if (!string.IsNullOrEmpty(screenId))
    {
      PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenId);
      if (screenIdUnsecure != null && screenIdUnsecure.GraphType != null)
      {
        PXGraph graphInstanceForAudit = PXGraph.CreateInstance(PXBuildManager.GetType(screenIdUnsecure.GraphType, false));
        IEnumerable<System.Type> tables = Enumerable.Empty<System.Type>();
        if (graphInstanceForAudit is IPXAuditSource pxAuditSource)
        {
          tables = pxAuditSource.GetAuditedTables();
        }
        else
        {
          PXSiteMap.ScreenInfo screenInfo = ScreenUtils.ScreenInfo.TryGet(screenId);
          if (screenInfo != null)
            tables = screenInfo.Containers.Values.Select<PXViewDescription, string>((Func<PXViewDescription, string>) (v => ScreenUtils.NormalizeViewName(v.ViewName))).Where<string>((Func<string, bool>) (vn => graphInstanceForAudit.Views.ContainsKey(vn))).Select<string, System.Type>((Func<string, System.Type>) (vn => graphInstanceForAudit.Views[vn].GetItemType())).Distinct<System.Type>();
        }
        graphForAudit = graphInstanceForAudit;
        return AuditMaintDataLoader.GetTablesFromScreenViews(tables, graphInstanceForAudit);
      }
    }
    graphForAudit = (PXGraph) null;
    return Enumerable.Empty<(System.Type, string, string)>();
  }

  internal static (Dictionary<string, (System.Type, string)> Tables, IEnumerable<string> TableDbKeys) GetTablesFromCache(
    PXCache cache)
  {
    Dictionary<string, (System.Type, string)> tablesInView = TablesAndFieldsInfoExtractor.GetTablesFromView(cache.GetItemType());
    if (cache.Interceptor != null)
    {
      if (!(cache.Interceptor is PXProjectionAttribute projectionAttribute))
        projectionAttribute = cache.Interceptor.Child as PXProjectionAttribute;
      if (projectionAttribute != null && projectionAttribute.Persistent)
      {
        foreach (System.Type table in projectionAttribute.GetTables())
        {
          foreach (KeyValuePair<string, (System.Type, string)> keyValuePair in TablesAndFieldsInfoExtractor.GetTablesFromView(table))
            tablesInView[keyValuePair.Key] = keyValuePair.Value;
        }
      }
    }
    IEnumerable<string> tablesInDB = PXDatabase.Provider.GetTables();
    List<string> list = tablesInView.Keys.Where<string>((Func<string, bool>) (table => tablesInDB.Contains<string>(tablesInView[table].Item2))).ToList<string>();
    return (tablesInView, (IEnumerable<string>) list);
  }

  private static IEnumerable<(System.Type TableType, string TableName, string TableTypeStr)> GetTablesFromScreenViews(
    IEnumerable<System.Type> tables,
    PXGraph graphForAudit)
  {
    List<(System.Type, string, string)> tablesFromScreenViews = new List<(System.Type, string, string)>();
    foreach (System.Type table in tables)
    {
      (Dictionary<string, (System.Type, string)> Tables, IEnumerable<string> TableDbKeys) tablesFromCache = AuditMaintDataLoader.GetTablesFromCache(graphForAudit.Caches[table]);
      Dictionary<string, (System.Type, string)> tables1 = tablesFromCache.Tables;
      foreach (string str in tablesFromCache.TableDbKeys)
      {
        System.Type type = PXBuildManager.GetType(str, false);
        string tableName = TablesAndFieldsInfoExtractor.GetTableName(tables1, str);
        tablesFromScreenViews.Add((type, tableName, str));
      }
    }
    return (IEnumerable<(System.Type, string, string)>) tablesFromScreenViews;
  }

  private void LoadFieldsFromTable(string fieldsTableName, PXCache fieldsTableCache)
  {
    if (string.IsNullOrEmpty(fieldsTableName) || fieldsTableCache == null)
      return;
    List<System.Type> extensionTables = fieldsTableCache.GetExtensionTables();
    foreach (System.Type bqlField in fieldsTableCache.BqlFields)
    {
      System.Type field = bqlField;
      PXDBOperation operation = PXDBOperation.Select;
      if (fieldsTableCache.IsKvExtField(field.Name))
        operation |= PXDBOperation.External;
      PXCommandPreparingEventArgs.FieldDescription description;
      fieldsTableCache.RaiseCommandPreparing(field.Name, (object) null, (object) null, operation, fieldsTableCache.GetItemType(), out description);
      if (description != null && description.BqlTable != (System.Type) null && (description.BqlTable.Name == fieldsTableName || extensionTables != null && extensionTables.Contains(description.BqlTable)) && !((IEnumerable<string>) TablesAndFieldsInfoExtractor.NotAuditableFields).Any<string>((Func<string, bool>) (f => string.Equals(f, field.Name, StringComparison.OrdinalIgnoreCase))))
        this.PlaceAuditFieldToCache(new AUAuditField()
        {
          ScreenID = this.screenIDForAudit,
          TableName = fieldsTableName,
          FieldName = field.Name,
          IsInserted = new bool?(true),
          FieldType = new int?(TablesAndFieldsInfoExtractor.GetFieldType(fieldsTableCache, field.Name)),
          FieldDisplayName = fieldsTableCache.GetField(field)
        });
    }
    foreach (KeyValueHelper.TableAttribute attribute in KeyValueHelper.Def.GetAttributes(fieldsTableCache.GetItemType()))
      this.PlaceAuditFieldToCache(new AUAuditField()
      {
        ScreenID = this.screenIDForAudit,
        TableName = fieldsTableName,
        FieldName = "Attribute" + attribute.AttributeID,
        IsInserted = new bool?(true),
        FieldType = new int?(1),
        FieldDisplayName = "Attribute" + attribute.AttributeID
      });
  }

  public static List<string> GetTableFields(PXCache tableCache, string tableNameInp)
  {
    List<string> tableFields = (List<string>) null;
    if (tableCache != null)
    {
      tableFields = new List<string>();
      string str = string.IsNullOrEmpty(tableNameInp) ? tableCache.BqlTable.Name : tableNameInp;
      List<System.Type> extensionTables = tableCache.GetExtensionTables();
      foreach (System.Type bqlField in tableCache.BqlFields)
      {
        System.Type field = bqlField;
        PXCommandPreparingEventArgs.FieldDescription description;
        tableCache.RaiseCommandPreparing(field.Name, (object) null, (object) null, PXDBOperation.Select, tableCache.GetItemType(), out description);
        if (description != null && description.BqlTable != (System.Type) null && (description.BqlTable.Name == str || extensionTables != null && extensionTables.Contains(description.BqlTable)) && !((IEnumerable<string>) TablesAndFieldsInfoExtractor.NotAuditableFields).Any<string>((Func<string, bool>) (f => string.Equals(f, field.Name, StringComparison.OrdinalIgnoreCase))))
          tableFields.Add(tableCache.GetField(field));
      }
      foreach (KeyValueHelper.TableAttribute attribute in KeyValueHelper.Def.GetAttributes(tableCache.GetItemType()))
        tableFields.Add("Attribute" + attribute.AttributeID);
    }
    return tableFields;
  }

  private void PlaceAuditTableToCache(
    AUAuditTable auditTable,
    System.Type tableType,
    PXGraph selectedGraphInstance)
  {
    if (auditTable == null || !(tableType != (System.Type) null))
      return;
    if (!(this.tableCache.Locate((object) auditTable) is AUAuditTable auAuditTable))
    {
      auditTable.Keys = TablesAndFieldsInfoExtractor.GetTableKeys(selectedGraphInstance.Caches[tableType]);
      PXEntryStatus status = PXEntryStatus.Held;
      this.tableCache.Insert((object) auditTable);
      this.tableCache.SetStatus((object) auditTable, status);
    }
    else
      auAuditTable.TableDisplayName = auditTable.TableDisplayName;
  }

  private void PlaceAuditFieldToCache(AUAuditField auditField)
  {
    if (auditField == null)
      return;
    object obj = this.fieldCache.Locate((object) auditField);
    if (obj == null)
      this.fieldCache.SetStatus(this.fieldCache.Insert((object) auditField), PXEntryStatus.Held);
    else
      ((AUAuditField) obj).FieldDisplayName = auditField.FieldDisplayName;
  }
}
