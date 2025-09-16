// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.GISortValidationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.GenericInquiry.Services.ValidationServices.Interface;
using PX.Data.Maintenance.GI;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.GenericInquiry.Services.ValidationServices;

internal class GISortValidationService(
  IGenericInquiryDescriptionProvider genericInquiryDescriptionProvider,
  IDacRegistry dacRegistry) : 
  GenericInquiryDacValidationService(genericInquiryDescriptionProvider, dacRegistry),
  IGISortValidationService,
  IRowValidationService<RowSelected, GISort>
{
  public IEnumerable<GIRowValidationError> ValidateRow(PXCache cache, GISort row)
  {
    this.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    Func<PXCache, string, bool> predicate = (Func<PXCache, string, bool>) ((c, f) => !PXGenericInqGrph.IsVirtualField(c, f));
    bool? isActive = row.IsActive;
    bool flag = true;
    if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue) || string.IsNullOrEmpty(row.DataFieldName))
      return (IEnumerable<GIRowValidationError>) Array.Empty<GIRowValidationError>();
    (string str1, string str2) = GenericInquiryDacValidationService.SplitDataFieldName(row.DataFieldName);
    if (str1 == null)
      return (IEnumerable<GIRowValidationError>) Array.Empty<GIRowValidationError>();
    GITable tableByAlias = this.GetTableByAlias(str1);
    List<GIRowValidationError> rowValidationErrorList = new List<GIRowValidationError>();
    try
    {
      if (tableByAlias != null)
      {
        if (!this.IsTableUsedInSelect(tableByAlias.Alias))
          return (IEnumerable<GIRowValidationError>) EnumerableExtensions.AsSingleEnumerable<GIFieldValidationError>(new GIFieldValidationError("dataFieldName", (IBqlTable) row, PXErrorLevel.Warning, "The {0} field cannot be used because either the table with the {1} alias is not joined with other tables on the Relations tab or the corresponding relation is inactive.", new string[2]
          {
            str2,
            tableByAlias.Alias
          }));
        int? type = tableByAlias.Type;
        int num = 1;
        if (type.GetValueOrDefault() == num & type.HasValue)
        {
          GIRowValidationError rowValidationError = this.ValidateSubGIField((IBqlTable) row, tableByAlias.Name, str2);
          return rowValidationError == null ? (IEnumerable<GIRowValidationError>) Array.Empty<GIRowValidationError>() : (IEnumerable<GIRowValidationError>) EnumerableExtensions.AsSingleEnumerable<GIFieldValidationError>(new GIFieldValidationError("DataFieldName", rowValidationError.Row, rowValidationError.ErrorLevel, rowValidationError.Message, rowValidationError.Arguments));
        }
        string query;
        if (this.IsComplexSort(tableByAlias, str2, out query))
          rowValidationErrorList.Add((GIRowValidationError) new GIFieldValidationError("IsActive", (IBqlTable) row, PXErrorLevel.Warning, "Adding this sort to your generic inquiry will impact performance. Generated query for this sort:~{0}", new string[1]
          {
            query
          }));
      }
      IEnumerable<string> fieldNames = this.GetFieldNames(tableByAlias, predicate);
      if (!row.DataFieldName.StartsWith("="))
      {
        if (!fieldNames.Contains<string>(row.DataFieldName, (IEqualityComparer<string>) StringComparer.CurrentCultureIgnoreCase))
          rowValidationErrorList.Add((GIRowValidationError) new GIFieldValidationError("DataFieldName", (IBqlTable) row, PXErrorLevel.Error, "A field with the name {0} cannot be found.", new string[1]
          {
            row.DataFieldName
          }));
      }
    }
    catch (PXTableIsNotFoundException ex)
    {
      rowValidationErrorList.Add((GIRowValidationError) new GIFieldValidationError("DataFieldName", (IBqlTable) row, PXErrorLevel.Warning, ex.MessageNoPrefix));
    }
    return (IEnumerable<GIRowValidationError>) rowValidationErrorList;
  }

  private IEnumerable<string> GetFieldNames(GITable table, Func<PXCache, string, bool> predicate)
  {
    if (table == null || string.IsNullOrEmpty(table.Name))
      return (IEnumerable<string>) Array.Empty<string>();
    int? type = table.Type;
    IEnumerable<string> fieldNames;
    if (type.HasValue)
    {
      switch (type.GetValueOrDefault())
      {
        case 0:
          break;
        case 1:
          fieldNames = this.GetGIFields(table, true, predicate, true).Select<GenericInquiryDacValidationService.FieldItem, string>((Func<GenericInquiryDacValidationService.FieldItem, string>) (x => x.Name));
          goto label_7;
        default:
          fieldNames = (IEnumerable<string>) Array.Empty<string>();
          goto label_7;
      }
    }
    fieldNames = this.GetDacFieldNames(table, predicate);
label_7:
    return fieldNames;
  }

  private IEnumerable<string> GetDacFieldNames(GITable table, Func<PXCache, string, bool> predicate)
  {
    GISortValidationService validationService = this;
    System.Type type = PXBuildManager.GetType(table.Name, false);
    if (!(type == (System.Type) null))
    {
      PXCache cache = validationService.Graph.Caches[type];
      foreach (string fieldName in validationService.GetFieldNames(cache, predicate, type, table.Alias))
        yield return fieldName;
      string field1 = PXGenericInqGrph.GetDeletedDatabaseRecord(cache).Field;
      if (field1 != null && !cache.Fields.Contains(field1))
        yield return $"{table.Alias}.{field1}";
      string field2 = PXGenericInqGrph.GetDatabaseRecordStatus(cache).Field;
      if (field2 != null && !cache.Fields.Contains(field2))
        yield return $"{table.Alias}.{field2}";
    }
  }

  private IEnumerable<string> GetFieldNames(
    PXCache cache,
    Func<PXCache, string, bool> predicate,
    System.Type t,
    string tableAlias)
  {
    Dictionary<string, string> existingNames = new Dictionary<string, string>();
    foreach (string field in (List<string>) cache.Fields)
    {
      System.Type bqlField;
      if (GenericInquiryDacValidationService.CanFieldBeUsedInExternalReferences(cache, field, t, out bqlField) && !existingNames.ContainsKey(field))
      {
        string bqlFieldName = bqlField != (System.Type) null ? bqlField.Name : field;
        existingNames[field] = field;
        PXFieldState stateExt = (PXFieldState) cache.GetStateExt((object) null, $"{tableAlias}.{field}");
        if (stateExt != null && !string.IsNullOrEmpty(stateExt.DescriptionName) && (predicate == null || predicate(cache, bqlFieldName + "_description")))
          yield return $"{tableAlias}.{bqlFieldName}_description";
        if (predicate == null || predicate(cache, bqlFieldName))
          yield return $"{tableAlias}.{bqlFieldName}";
        bqlFieldName = (string) null;
      }
    }
  }

  public override IEnumerable<GIValidationError> RunConsistencyValidation(
    PXCache cache,
    IEnumerable<IBqlTable> rows)
  {
    return (IEnumerable<GIValidationError>) rows.Cast<GISort>().SelectMany<GISort, GIRowValidationError>((Func<GISort, IEnumerable<GIRowValidationError>>) (row => this.ValidateRow(cache, row)));
  }
}
