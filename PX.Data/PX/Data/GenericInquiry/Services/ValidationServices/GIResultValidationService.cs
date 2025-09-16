// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.GIResultValidationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data.Description.GI;
using PX.Data.GenericInquiry.Services.ValidationServices.Interface;
using PX.Data.Maintenance.GI;
using PX.Data.SQLTree;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.GenericInquiry.Services.ValidationServices;

internal class GIResultValidationService(
  IGenericInquiryDescriptionProvider genericInquiryDescriptionProvider,
  IDacRegistry dacRegistry) : 
  GenericInquiryDacValidationService(genericInquiryDescriptionProvider, dacRegistry),
  IGIResultValidationService,
  IRowValidationService<RowSelected, GIResult>
{
  private readonly PXGIFormulaProcessor _formulaProcessor = new PXGIFormulaProcessor();

  public IEnumerable<GIRowValidationError> ValidateRow(PXCache cache, GIResult row)
  {
    GIResultValidationService validationService = this;
    validationService.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    if (!string.IsNullOrEmpty(row.ObjectName))
    {
      foreach (GIRowValidationError rowValidationError in validationService.ValidateResult(cache, row))
        yield return rowValidationError;
    }
    string schemaField = row.SchemaField;
    if (!string.IsNullOrEmpty(schemaField))
    {
      (string str1, string str2) = GenericInquiryDacValidationService.SplitDataFieldName(schemaField);
      if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
        yield return (GIRowValidationError) new GIFieldValidationError("SchemaField", (IBqlTable) row, PXErrorLevel.Error, "A field with the name {0} cannot be found.", new string[1]
        {
          schemaField
        });
      PXGraph graph = validationService.Graph;
      object[] objArray = new object[1]
      {
        (object) validationService.DesignId
      };
      foreach (PXResult<GITable> pxResult in PXSelectBase<GITable, PXSelect<GITable, Where<GITable.designID, Equal<Required<GITable.designID>>>>.Config>.Select(graph, objArray))
      {
        GITable giTable = (GITable) pxResult;
        if (!string.IsNullOrEmpty(giTable.Name))
        {
          int? type = giTable.Type;
          int num = 1;
          if (type.GetValueOrDefault() == num & type.HasValue && string.Equals(giTable.Alias, str1, StringComparison.OrdinalIgnoreCase))
          {
            GIRowValidationError rowValidationError = validationService.ValidateSubGIField((IBqlTable) row, giTable.Name, str2, true);
            if (rowValidationError != null)
              yield return (GIRowValidationError) new GIFieldValidationError("SchemaField", rowValidationError.Row, rowValidationError.ErrorLevel, rowValidationError.Message, rowValidationError.Arguments);
          }
        }
      }
    }
  }

  private IEnumerable<GIRowValidationError> ValidateResult(PXCache cache, GIResult row)
  {
    GITable tableByAlias = this.GetTableByAlias(row.ObjectName);
    if (tableByAlias == null)
      return (IEnumerable<GIRowValidationError>) EnumerableExtensions.AsSingleEnumerable<GIFieldValidationError>(new GIFieldValidationError("ObjectName", (IBqlTable) row, PXErrorLevel.Warning, "A table with the alias {0} cannot be found.", new string[1]
      {
        row.ObjectName
      }));
    int? type = tableByAlias.Type;
    int num = 1;
    if (type.GetValueOrDefault() == num & type.HasValue)
      return this.ValidateGiTable(row, tableByAlias);
    bool flag = !string.IsNullOrEmpty(row.Field);
    if (flag)
    {
      type = tableByAlias.Type;
      flag = !type.HasValue || type.GetValueOrDefault() == 0;
    }
    return flag ? this.ValidateDacTable(cache, row, tableByAlias) : (IEnumerable<GIRowValidationError>) Array.Empty<GIRowValidationError>();
  }

  private IEnumerable<GIRowValidationError> ValidateGiTable(GIResult row, GITable table)
  {
    GIResultValidationService validationService = this;
    if (GenericInquiryDacValidationService.IsFormulaField(row.Field))
    {
      foreach (GIRowValidationError rowValidationError in validationService.ValidateFormula(row))
        yield return rowValidationError;
    }
    GIRowValidationError rowValidationError1 = validationService.ValidateSubGIField((IBqlTable) row, table.Name, row.Field);
    if (rowValidationError1 != null)
      yield return (GIRowValidationError) new GIFieldValidationError("Field", rowValidationError1.Row, rowValidationError1.ErrorLevel, rowValidationError1.Message);
  }

  private IEnumerable<GIRowValidationError> ValidateDacTable(
    PXCache cache,
    GIResult row,
    GITable table)
  {
    GIResultValidationService validationService = this;
    GIRowValidationError error;
    System.Type tableType = validationService.GetTableType(table, "objectName", cache, (IBqlTable) row, out error);
    if (error != null)
      yield return error;
    if (!(tableType == (System.Type) null))
    {
      validationService.Graph.SetAccessRightsFromAllGraphs(tableType);
      PXCache cach = validationService.Graph.Caches[tableType];
      PXCommandPreparingEventArgs.FieldDescription descr;
      cach.RaiseCommandPreparing(row.Field, (object) null, (object) null, PXDBOperation.Select, cach.GetItemType(), out descr);
      PXDbType? dataType = descr?.DataType;
      if (descr?.Expr == null)
        cache.RaiseCommandPreparing(row.Field, (object) null, (object) null, PXDBOperation.External, cach.GetItemType(), out descr);
      if (!dataType.HasValue)
        dataType = descr?.DataType;
      string field1 = PXGenericInqGrph.GetDeletedDatabaseRecord(cach).Field;
      string field2 = PXGenericInqGrph.GetDatabaseRecordStatus(cach).Field;
      System.Type bqlField = cach.GetBqlField(row.Field);
      bool flag1 = row.Field == "$<Count>";
      bool flag2 = descr?.Expr == null;
      bool flag3 = string.Equals(row.Field, field1, StringComparison.OrdinalIgnoreCase);
      bool flag4 = string.Equals(row.Field, field2, StringComparison.OrdinalIgnoreCase);
      int num = !(bqlField != (System.Type) null) ? 0 : (Attribute.IsDefined((MemberInfo) bqlField, typeof (ObsoleteAttribute), false) ? 1 : 0);
      bool isSubquery = !flag2 && descr.Expr is SubQuery;
      bool flag5 = row.Field.EndsWith("_description", StringComparison.OrdinalIgnoreCase) && cach.Fields.Contains(row.Field.RemoveFromEnd("_description", StringComparison.OrdinalIgnoreCase));
      bool isFormula = GenericInquiryDacValidationService.IsFormulaField(row.Field);
      bool isNonExistingField = !isFormula && !flag5 && !flag1 && !flag3 && !flag4 && !cach.Fields.Contains(row.Field);
      bool isFieldFromMissingTable = row.ObjectName != null && !validationService.IsTableUsedInSelect(row.ObjectName);
      bool isFieldHiddenByAccessRights = !isFormula && validationService.IsFieldHiddenByAccessRights($"{row.ObjectName}.{row.Field}");
      bool isSchemaFieldHiddenByAccessRights = validationService.IsFieldHiddenByAccessRights(row.SchemaField);
      if (num != 0)
        yield return (GIRowValidationError) new GIFieldValidationError("Field", (IBqlTable) row, PXErrorLevel.Warning, "This data field is obsolete. Select another data field.");
      if (isSubquery)
        yield return (GIRowValidationError) new GIFieldValidationError("Field", (IBqlTable) row, PXErrorLevel.Warning, "Adding this field to the generic inquiry will affect the system performance. To avoid unnecessary queries, join with the related table. The generated query for this field is ~{0}", new string[1]
        {
          descr.Expr.ToString()
        });
      if (isNonExistingField)
        yield return (GIRowValidationError) new GIFieldValidationError("Field", (IBqlTable) row, PXErrorLevel.Warning, "A field with the name {0} cannot be found.", new string[1]
        {
          row.Field
        });
      if (isFieldFromMissingTable)
        yield return (GIRowValidationError) new GIFieldValidationError("Field", (IBqlTable) row, PXErrorLevel.Warning, "The {0} field cannot be used because either the table with the {1} alias is not joined with other tables on the Relations tab or the corresponding relation is inactive.", new string[2]
        {
          row.Field,
          row.ObjectName
        });
      if (isFieldHiddenByAccessRights)
        yield return (GIRowValidationError) new GIFieldValidationError("Field", (IBqlTable) row, PXErrorLevel.Warning, "This field will not be displayed for the current user because the user has insufficient access rights or the required feature is disabled.");
      if (isSchemaFieldHiddenByAccessRights)
        yield return (GIRowValidationError) new GIFieldValidationError("SchemaField", (IBqlTable) row, PXErrorLevel.Warning, "This field will not be displayed for the current user because the user has insufficient access rights or the required feature is disabled.");
      if (!GIResultValidationService.IsValidAggregate(dataType, row.AggregateFunction))
        yield return (GIRowValidationError) new GIFieldValidationError("AggregateFunction", (IBqlTable) row, PXErrorLevel.Error, "Selected aggregate function is not supported for this data field.");
      if (!GIResultValidationService.IsValidAggregate(dataType, row.TotalAggregateFunction))
        yield return (GIRowValidationError) new GIFieldValidationError("TotalAggregateFunction", (IBqlTable) row, PXErrorLevel.Error, "Selected aggregate function is not supported for this data field.");
      if (isFormula)
      {
        foreach (GIRowValidationError rowValidationError in validationService.ValidateFormula(row))
          yield return rowValidationError;
      }
    }
  }

  private IEnumerable<GIRowValidationError> ValidateFormula(GIResult row)
  {
    List<GIRowValidationError> errors = new List<GIRowValidationError>();
    try
    {
      this._formulaProcessor.TransformToExpression(row.Field, new SyFormulaFinalDelegate(GetColumn));
    }
    catch (Exception ex)
    {
      errors.Add((GIRowValidationError) new GIFieldValidationError("Field", (IBqlTable) row, PXErrorLevel.Warning, ex.Message, new string[1]
      {
        row.Field
      }));
    }
    return (IEnumerable<GIRowValidationError>) errors;

    Column GetColumn(string[] args)
    {
      (string str1, string str2) = GenericInquiryDacValidationService.SplitDataFieldName(args[0]);
      if (str1 == null)
        return new Column(args[0]);
      GITable tableByAlias = this.GetTableByAlias(str1);
      if (tableByAlias != null)
      {
        int? type = tableByAlias.Type;
        int num = 1;
        if (type.GetValueOrDefault() == num & type.HasValue)
          return GetColumnFromGi(tableByAlias, str2);
      }
      return GetColumnFromDac(tableByAlias, str2);
    }

    Column GetColumnFromGi(GITable table, string fieldName)
    {
      if (this.FindResultInDescription(this.GenericInquiryDescriptionProvider.Get(table.Name), fieldName) == null)
        errors.Add((GIRowValidationError) new GIFieldValidationError("Field", (IBqlTable) row, PXErrorLevel.Warning, "A field with the name {0} cannot be found.", new string[1]
        {
          fieldName
        }));
      return new Column(fieldName, table.Alias);
    }

    Column GetColumnFromDac(GITable table, string fieldName)
    {
      string alias = table?.Alias;
      PXCache cach = this.Graph.Caches[PXBuildManager.GetType(table?.Name ?? alias, true)];
      if (cach.Fields.Contains(fieldName))
        return new Column(fieldName, alias);
      string field1 = PXGenericInqGrph.GetDeletedDatabaseRecord(cach).Field;
      string field2 = PXGenericInqGrph.GetDatabaseRecordStatus(cach).Field;
      if (!string.Equals(field1, fieldName, StringComparison.OrdinalIgnoreCase) && !string.Equals(field2, fieldName, StringComparison.OrdinalIgnoreCase))
        errors.Add((GIRowValidationError) new GIFieldValidationError("Field", (IBqlTable) row, PXErrorLevel.Warning, "A field with the name {0} cannot be found.", new string[1]
        {
          fieldName
        }));
      return new Column(fieldName, alias);
    }
  }

  public override IEnumerable<GIValidationError> RunConsistencyValidation(
    PXCache cache,
    IEnumerable<IBqlTable> rows)
  {
    return (IEnumerable<GIValidationError>) rows.Cast<GIResult>().SelectMany<GIResult, GIRowValidationError>((Func<GIResult, IEnumerable<GIRowValidationError>>) (row => this.ValidateRow(cache, row)));
  }

  private bool IsFieldHiddenByAccessRights(string fieldName)
  {
    PXUIVisibility? visibility = this.GetVisibility(fieldName);
    PXUIVisibility pxuiVisibility = PXUIVisibility.HiddenByAccessRights;
    return visibility.GetValueOrDefault() == pxuiVisibility & visibility.HasValue;
  }

  private PXUIVisibility? GetVisibility(string fieldName)
  {
    Dictionary<string, GITable> allTables = this.GetAllTables();
    if (string.IsNullOrEmpty(fieldName) || allTables == null)
      return new PXUIVisibility?();
    if (fieldName == typeof (CheckboxCombobox.checkbox).FullName || fieldName == typeof (CheckboxCombobox.combobox).FullName)
      return !(this.Graph.Caches[typeof (CheckboxCombobox)].GetStateExt((object) null, PXBuildManager.GetType(fieldName, true).Name) is PXFieldState stateExt) ? new PXUIVisibility?() : new PXUIVisibility?(stateExt.Visibility);
    string alias;
    string field;
    GIResultValidationService.SplitFieldName(fieldName, out alias, out field);
    return string.IsNullOrEmpty(alias) || string.IsNullOrEmpty(field) || !allTables.ContainsKey(alias) ? new PXUIVisibility?() : this.GetFieldVisibility(field, alias, allTables);
  }

  private PXUIVisibility? GetFieldVisibility(
    string fieldName,
    string tableAlias,
    Dictionary<string, GITable> allTables)
  {
    GITable tableByAlias = this.GetTableByAlias(tableAlias);
    if (tableByAlias == null)
      return new PXUIVisibility?();
    int? type1 = tableByAlias.Type;
    int num = 1;
    PXCache cach;
    if (type1.GetValueOrDefault() == num & type1.HasValue)
    {
      (cach, fieldName) = this.GetUnderlyingFieldAndCache(this.GenericInquiryDescriptionProvider.Get(tableByAlias.Name), fieldName);
      if (cach == null)
        return new PXUIVisibility?();
    }
    else
    {
      System.Type type2 = PXBuildManager.GetType(allTables[tableAlias].Name, false);
      if (type2 == (System.Type) null)
        return new PXUIVisibility?();
      cach = this.Graph.Caches[type2];
    }
    return !(cach?.GetStateExt((object) null, fieldName) is PXFieldState stateExt) ? new PXUIVisibility?() : new PXUIVisibility?(stateExt.Visibility);
  }

  private static void SplitFieldName(string fieldName, out string alias, out string field)
  {
    alias = (string) null;
    field = (string) null;
    if (string.IsNullOrEmpty(fieldName))
      return;
    string[] strArray = fieldName.Split(new char[1]{ '.' }, 2);
    if (strArray.Length < 2)
      return;
    alias = strArray[0].Trim();
    field = strArray[1].Trim();
  }

  private static bool IsValidAggregate(PXDbType? type, string aggregateFunc)
  {
    if (!type.HasValue || string.IsNullOrEmpty(aggregateFunc))
      return true;
    switch (ValFromStr.GetAggregate(aggregateFunc))
    {
      case AggregateFunction.Avg:
      case AggregateFunction.Sum:
        bool flag;
        if (type.HasValue)
        {
          switch (type.GetValueOrDefault())
          {
            case PXDbType.Binary:
            case PXDbType.Bit:
            case PXDbType.Char:
            case PXDbType.DateTime:
            case PXDbType.NChar:
            case PXDbType.NVarChar:
            case PXDbType.Text:
            case PXDbType.Timestamp:
            case PXDbType.VarChar:
            case PXDbType.SmallDateTime:
              flag = true;
              goto label_8;
          }
        }
        flag = false;
label_8:
        return !flag;
      default:
        return true;
    }
  }
}
