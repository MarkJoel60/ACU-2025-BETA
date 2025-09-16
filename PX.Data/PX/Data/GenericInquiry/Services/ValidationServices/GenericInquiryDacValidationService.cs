// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.GenericInquiryDacValidationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Maintenance.GI;
using PX.Data.SQLTree;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.GenericInquiry.Services.ValidationServices;

internal abstract class GenericInquiryDacValidationService(
  IGenericInquiryDescriptionProvider genericInquiryDescriptionProvider,
  IDacRegistry dacRegistry)
{
  protected PXGraph Graph;
  protected Guid DesignId;
  protected IGenericInquiryDescriptionProvider GenericInquiryDescriptionProvider = genericInquiryDescriptionProvider;
  protected IDacRegistry DacRegistry = dacRegistry;
  private const char SubGIFieldDelimiter = '_';

  protected static (string TableAlias, string FieldName) SplitDataFieldName(
    string dataFieldName,
    char delimiter = '.')
  {
    string[] strArray = dataFieldName != null ? dataFieldName.Split(delimiter, 2) : (string[]) null;
    return (strArray != null ? (strArray.Length != 2 ? 1 : 0) : 1) == 0 ? (strArray[0], strArray[1]) : ((string) null, (string) null);
  }

  protected GITable GetTableByAlias(string alias)
  {
    return PXSelectBase<GITable, PXSelect<GITable, Where<GITable.designID, Equal<Required<GITable.designID>>, And<GITable.alias, Equal<Required<GITable.alias>>>>>.Config>.SelectSingleBound(this.Graph, (object[]) null, (object) this.DesignId, (object) alias).FirstTableItems.FirstOrDefault<GITable>();
  }

  protected bool IsTableUsedInSelect(string tableAlias)
  {
    IEnumerable<GIRelation> allRelations = PXSelectBase<GIRelation, PXSelect<GIRelation, Where<GIRelation.designID, Equal<Required<GIDesign.designID>>>>.Config>.Select(this.Graph, (object) this.DesignId).RowCast<GIRelation>();
    IEnumerable<GIResult> allResults = PXSelectBase<GIResult, PXSelect<GIResult, Where<GIResult.designID, Equal<Required<GIDesign.designID>>>>.Config>.Select(this.Graph, (object) this.DesignId).RowCast<GIResult>();
    IEnumerable<GITable> allTables = PXSelectBase<GITable, PXSelect<GITable, Where<GITable.designID, Equal<Required<GIDesign.designID>>>>.Config>.Select(this.Graph, (object) this.DesignId).RowCast<GITable>();
    return GenericInquiryHelpers.IsTableUsedInSelect(tableAlias, allRelations, allResults, allTables);
  }

  protected GIRowValidationError ValidateSubGIField(
    IBqlTable row,
    string subGIDesignId,
    string fieldName,
    bool isSchemaField = false)
  {
    if (string.IsNullOrEmpty(fieldName) || GenericInquiryDacValidationService.IsFormulaField(fieldName))
      return (GIRowValidationError) null;
    GIDescription description = this.GenericInquiryDescriptionProvider.Get(subGIDesignId);
    if (this.FindResultInDescription(description, fieldName) == null)
    {
      if (isSchemaField)
      {
        (PXCache Cache, string fieldName) underlyingFieldAndCache = this.GetUnderlyingFieldAndCache(description, fieldName);
        if (underlyingFieldAndCache.Cache != null && underlyingFieldAndCache.fieldName != null && underlyingFieldAndCache.Cache.Fields.Contains(underlyingFieldAndCache.fieldName))
          goto label_5;
      }
      return new GIRowValidationError(row, PXErrorLevel.Warning, "This field is not returned by the generic inquiry used as the data source.");
    }
label_5:
    return (GIRowValidationError) null;
  }

  protected static bool IsFormulaField(string field) => field != null && field.StartsWith("=");

  protected GIResult FindResultInDescription(
    GIDescription description,
    string fieldName,
    bool activeOnly = true)
  {
    return description == null ? (GIResult) null : description.Results.FirstOrDefault<GIResult>((Func<GIResult, bool>) (x =>
    {
      if (activeOnly)
      {
        bool? isActive = x.IsActive;
        bool flag = true;
        if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
          return false;
      }
      return string.Equals(GenericInquiryDacValidationService.GetSubGIFieldName(x), fieldName, StringComparison.OrdinalIgnoreCase);
    }));
  }

  protected (PXCache Cache, string fieldName) GetUnderlyingFieldAndCache(
    GIDescription description,
    string fieldName)
  {
    if (description == null)
      return ();
    (string str1, string str2) = GenericInquiryDacValidationService.SplitDataFieldName(fieldName, '_');
    GITable giTable = description.Tables.FirstOrDefault<GITable>((Func<GITable, bool>) (x => string.Equals(x.Alias, str1, StringComparison.OrdinalIgnoreCase)));
    if (giTable == null)
      return ();
    int? type1 = giTable.Type;
    int num = 1;
    if (type1.GetValueOrDefault() == num & type1.HasValue)
      return this.GetUnderlyingFieldAndCache(this.GenericInquiryDescriptionProvider.Get(giTable.Name), str2);
    type1 = giTable.Type;
    if (type1.HasValue && type1.GetValueOrDefault() != 0)
      throw new NotImplementedException();
    System.Type type2 = PXBuildManager.GetType(giTable.Name, false);
    return !(type2 == (System.Type) null) ? (this.Graph.Caches[type2], str2) : ();
  }

  private static string GetSubGIFieldName(GIResult result)
  {
    return !GenericInquiryDacValidationService.IsFormulaField(result.Field) ? WithPrefix(GenericInquiryDacValidationService.IsCountField(result) ? GenericInquiryDacValidationService.GetCountFieldName(result) : (GenericInquiryDacValidationService.IsStringAggField(result) ? GenericInquiryDacValidationService.GetStringAggFieldName(result) : result.Field)) : WithPrefix(GenericInquiryDacValidationService.GetFormulaFieldName(result));

    string WithPrefix(string fieldName) => $"{result.ObjectName}_{fieldName}";
  }

  internal static string GetFormulaFieldName(GIResult result)
  {
    return "Formula" + PXGenericInqGrph.GetExtFieldId(result);
  }

  internal static string GetCountFieldName(GIResult result)
  {
    return "Count" + PXGenericInqGrph.GetExtFieldId(result);
  }

  internal static string GetStringAggFieldName(GIResult result)
  {
    return "StringAgg" + PXGenericInqGrph.GetExtFieldId(result);
  }

  internal static bool IsCountField(GIResult result)
  {
    return result.Field == "$<Count>" || result.AggregateFunction == "COUNT";
  }

  internal static bool IsStringAggField(GIResult result) => result.AggregateFunction == "STRINGAGG";

  protected bool IsComplexSort(GITable table, string field, out string query)
  {
    query = (string) null;
    if (string.IsNullOrEmpty(table.Name))
      return false;
    int? type1 = table.Type;
    int num = 1;
    if (type1.GetValueOrDefault() == num & type1.HasValue)
      return GenericInquiryDacValidationService.IsFormulaField(field);
    System.Type type2 = PXBuildManager.GetType(table.Name, false);
    if (type2 == (System.Type) null)
      throw new PXTableIsNotFoundException(table.Alias);
    PXCache cach = this.Graph.Caches[type2];
    PXCommandPreparingEventArgs.FieldDescription description;
    cach.RaiseCommandPreparing(field, (object) null, (object) null, PXDBOperation.External | PXDBOperation.OrderByClause, cach.GetItemType(), out description);
    query = description?.Expr?.SQLQuery(this.Graph.SqlDialect.GetConnection()).ToString();
    bool flag1 = !string.IsNullOrEmpty(query);
    if (flag1)
    {
      bool flag2;
      switch (description?.Expr)
      {
        case Column _:
        case SQLConst _:
          flag2 = true;
          break;
        default:
          flag2 = false;
          break;
      }
      flag1 = !flag2;
    }
    return flag1;
  }

  protected Dictionary<string, GITable> GetAllTables()
  {
    Dictionary<string, GITable> allTables = new Dictionary<string, GITable>();
    foreach (PXResult<GITable> pxResult in PXSelectBase<GITable, PXSelect<GITable, Where<GITable.designID, Equal<Required<GIDesign.designID>>>>.Config>.Select(this.Graph, (object) this.DesignId).AsEnumerable<PXResult<GITable>>())
    {
      GITable giTable = (GITable) pxResult;
      if (!string.IsNullOrEmpty(giTable.Alias))
        allTables[giTable.Alias] = giTable;
    }
    return allTables;
  }

  protected IEnumerable<GenericInquiryDacValidationService.FieldItem> GetGIFields(
    GITable table,
    bool needTableName,
    Func<PXCache, string, bool> predicate = null,
    bool forExternalReferencesOnly = false)
  {
    GIDescription description = this.GenericInquiryDescriptionProvider.Get(table.Name);
    if (description?.Results != null)
    {
      List<(GIResult, string)> list = description.Results.Where<GIResult>((Func<GIResult, bool>) (x =>
      {
        bool? isActive = x.IsActive;
        bool flag = true;
        return isActive.GetValueOrDefault() == flag & isActive.HasValue && FieldCanBeAdded(description, x, predicate);
      })).Select<GIResult, (GIResult, string)>((Func<GIResult, (GIResult, string)>) (x => (x, GetDisplayName(x)))).ToList<(GIResult, string)>();
      HashSet<string> displayNameDuplicates = list.GroupBy<(GIResult, string), string>((Func<(GIResult, string), string>) (x => x.DisplayName.ToLower())).Where<IGrouping<string, (GIResult, string)>>((Func<IGrouping<string, (GIResult, string)>, bool>) (x => x.Count<(GIResult, string)>() > 1)).Select<IGrouping<string, (GIResult, string)>, string>((Func<IGrouping<string, (GIResult, string)>, string>) (x => x.Key)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach ((GIResult, string) valueTuple in list)
        yield return new GenericInquiryDacValidationService.FieldItem()
        {
          Name = GetFieldName(valueTuple.Item1),
          DispName = displayNameDuplicates.Contains(valueTuple.Item2) ? GetDisplayName(valueTuple.Item1, true) : valueTuple.Item2
        };
    }

    string GetFieldName(GIResult result)
    {
      return (needTableName ? table.Alias + "." : string.Empty) + GenericInquiryDacValidationService.GetSubGIFieldName(result);
    }

    string GetDisplayName(GIResult result, bool fullName = false)
    {
      return (needTableName ? table.Alias + "." : string.Empty) + (fullName ? result.ObjectName + "_" : string.Empty) + (result.Caption ?? result.Field);
    }

    bool FieldCanBeAdded(
      GIDescription description,
      GIResult result,
      Func<PXCache, string, bool> predicate)
    {
      if (predicate == null && !forExternalReferencesOnly)
        return true;
      (PXCache Cache, string FieldName, System.Type OriginalCacheType) underlyingField = this.GetUnderlyingField(description, result);
      if (underlyingField.Cache == null || forExternalReferencesOnly && !GenericInquiryDacValidationService.CanFieldBeUsedInExternalReferences(underlyingField.Cache, underlyingField.FieldName, underlyingField.OriginalCacheType))
        return false;
      return predicate == null || predicate(underlyingField.Cache, underlyingField.FieldName);
    }
  }

  private static bool CanFieldBeUsedInExternalReferences(
    PXCache cache,
    string fieldName,
    System.Type originalCacheType)
  {
    return GenericInquiryDacValidationService.CanFieldBeUsedInExternalReferences(cache, fieldName, originalCacheType, out System.Type _);
  }

  protected static bool CanFieldBeUsedInExternalReferences(
    PXCache cache,
    string fieldName,
    System.Type originalCacheType,
    out System.Type bqlField)
  {
    bqlField = cache.GetBqlField(fieldName);
    return (bqlField == (System.Type) null || BqlCommand.GetItemType(bqlField).IsAssignableFrom(originalCacheType)) && (bqlField != (System.Type) null || fieldName.EndsWith("_Attributes") || fieldName.EndsWith("Signed") || cache.IsKvExtAttribute(fieldName)) && !cache.GetAttributes(fieldName).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXDBTimestampAttribute));
  }

  private (PXCache Cache, string FieldName, System.Type OriginalCacheType) GetUnderlyingField(
    GIDescription description,
    GIResult result)
  {
    GITable giTable = description.Tables.FirstOrDefault<GITable>((Func<GITable, bool>) (x => x.Alias == result.ObjectName));
    if (giTable == null)
      return ();
    int? type1 = giTable.Type;
    if (type1.HasValue)
    {
      switch (type1.GetValueOrDefault())
      {
        case 0:
          break;
        case 1:
          GIDescription description1 = this.GenericInquiryDescriptionProvider.Get(giTable.Name);
          (string TableAlias, string FieldName) field = GenericInquiryDacValidationService.SplitDataFieldName(result.Field, '_');
          if (description1 == null || field.TableAlias == null)
            return ();
          GIResult result1 = description1.Results.FirstOrDefault<GIResult>((Func<GIResult, bool>) (x => x.ObjectName == field.TableAlias && x.Field == field.FieldName));
          return result1 != null ? this.GetUnderlyingField(description1, result1) : ();
        default:
          throw new NotImplementedException();
      }
    }
    System.Type type2 = PXBuildManager.GetType(giTable.Name, false);
    return !(type2 == (System.Type) null) ? (this.Graph.Caches[type2], result.Field, type2) : ();
  }

  protected IEnumerable<GIRowValidationError> EnsureTableExists(
    string valueToCheck,
    string fieldName,
    PXCache cache,
    IBqlTable row,
    PXErrorLevel errorLevel = PXErrorLevel.Warning,
    bool checkVisibility = false)
  {
    if (!string.IsNullOrEmpty(valueToCheck))
    {
      GITable tableByAlias = this.GetTableByAlias(valueToCheck);
      if (string.IsNullOrEmpty(tableByAlias?.Name))
      {
        yield return (GIRowValidationError) new GIFieldValidationError(fieldName, row, errorLevel, "A table with the alias {0} cannot be found.", new string[1]
        {
          valueToCheck
        });
      }
      else
      {
        int? type = tableByAlias.Type;
        if (type.HasValue)
        {
          switch (type.GetValueOrDefault())
          {
            case 0:
              break;
            case 1:
              if (this.GenericInquiryDescriptionProvider.Get(tableByAlias.Name) != null)
                yield break;
              yield return (GIRowValidationError) new GIFieldValidationError(fieldName, row, errorLevel, "This generic inquiry does not exist.");
              yield break;
            default:
              yield break;
          }
        }
        GIRowValidationError error;
        System.Type tableType = this.GetTableType(tableByAlias, fieldName, cache, row, out error, errorLevel: errorLevel);
        if (error != null)
          yield return error;
        if (checkVisibility && tableType != (System.Type) null && !this.IsTableOfTypeVisible(tableType))
          yield return (GIRowValidationError) new GIFieldValidationError(fieldName, row, PXErrorLevel.Warning, "This table is obsolete. You cannot use it as a data source for new generic inquiries. Select another table.");
      }
    }
  }

  protected System.Type GetTableType(
    GITable tableToCheck,
    string fieldName,
    PXCache cache,
    IBqlTable row,
    out GIRowValidationError error,
    string warningMessage = null,
    PXErrorLevel errorLevel = PXErrorLevel.Warning)
  {
    error = (GIRowValidationError) null;
    System.Type type = PXBuildManager.GetType(tableToCheck.Name, false);
    if (type != (System.Type) null)
      return type;
    IEnumerable<PXUIFieldAttribute> attributesOfType = cache.GetAttributesOfType<PXUIFieldAttribute>((object) row, fieldName);
    if ((attributesOfType != null ? attributesOfType.FirstOrDefault<PXUIFieldAttribute>()?.ErrorLevel : new PXErrorLevel?()).GetValueOrDefault() >= errorLevel)
      return (System.Type) null;
    if (string.IsNullOrEmpty(warningMessage))
      warningMessage = "A table with the alias {0} cannot be found.";
    error = (GIRowValidationError) new GIFieldValidationError(fieldName, row, errorLevel, warningMessage, new string[1]
    {
      tableToCheck.Alias
    });
    return (System.Type) null;
  }

  protected bool IsTableOfTypeVisible(System.Type type)
  {
    return this.DacRegistry.Visible.Contains<System.Type>(type);
  }

  protected IEnumerable<GIRowValidationError> ValidateDataFieldName<Field>(
    string dataFieldName,
    IBqlTable row,
    PXCache cache)
    where Field : IBqlField
  {
    List<GIRowValidationError> rowValidationErrorList = new List<GIRowValidationError>();
    if (GenericInquiryDacValidationService.IsFormulaField(dataFieldName))
      return (IEnumerable<GIRowValidationError>) rowValidationErrorList;
    (string str1, string str2) = GenericInquiryDacValidationService.SplitDataFieldName(dataFieldName);
    rowValidationErrorList.AddRange(this.EnsureTableExists(str1, typeof (Field).Name, cache, row));
    if ((string.IsNullOrEmpty(str1) ? 1 : (this.IsTableUsedInSelect(str1) ? 1 : 0)) == 0)
    {
      rowValidationErrorList.Add((GIRowValidationError) new GIFieldValidationError(typeof (Field).Name, row, PXErrorLevel.Warning, "The {0} field cannot be used because either the table with the {1} alias is not joined with other tables on the Relations tab or the corresponding relation is inactive.", new string[2]
      {
        str2,
        str1
      }));
    }
    else
    {
      GITable tableByAlias = this.GetTableByAlias(str1);
      if (tableByAlias != null)
      {
        int? type = tableByAlias.Type;
        if (type.HasValue && type.GetValueOrDefault() == 1)
        {
          GIRowValidationError rowValidationError = this.ValidateSubGIField(row, tableByAlias.Name, str2);
          if (rowValidationError != null)
            rowValidationErrorList.Add((GIRowValidationError) new GIFieldValidationError(typeof (Field).Name, rowValidationError.Row, rowValidationError.ErrorLevel, rowValidationError.Message, rowValidationError.Arguments));
        }
      }
    }
    return (IEnumerable<GIRowValidationError>) rowValidationErrorList;
  }

  protected HashSet<string> GetAllParameters(bool inBrackets = true)
  {
    List<string> source = new List<string>();
    foreach (PXResult<GIFilter> pxResult in PXSelectBase<GIFilter, PXSelect<GIFilter, Where<GIFilter.designID, Equal<Required<GIDesign.designID>>>>.Config>.Select(this.Graph, (object) this.DesignId))
    {
      GIFilter giFilter = (GIFilter) pxResult;
      source.Add(inBrackets ? $"[{giFilter.Name}]" : giFilter.Name);
    }
    return source.ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  }

  protected GIDesign GetCurrentGIDesignRecord()
  {
    GIDesign current = (GIDesign) this.Graph.Caches<GIDesign>().Current;
    if (current != null)
      return current;
    return PXSelectBase<GIDesign, PXSelect<GIDesign, Where<GIDesign.designID, Equal<Required<GIDesign.designID>>>>.Config>.SelectSingleBound(this.Graph, (object[]) null, (object) this.DesignId).FirstTableItems.FirstOrDefault<GIDesign>();
  }

  public abstract IEnumerable<GIValidationError> RunConsistencyValidation(
    PXCache cache,
    IEnumerable<IBqlTable> rows);

  protected void SetGraphAndDesignId(PXGraph graph, Guid designId)
  {
    this.Graph = graph;
    this.DesignId = designId;
  }

  protected class FieldItem
  {
    public string Name;
    public string DispName;
  }
}
