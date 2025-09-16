// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.GIOnValidationService
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

internal class GIOnValidationService(
  IGenericInquiryDescriptionProvider genericInquiryDescriptionProvider,
  IDacRegistry dacRegistry) : 
  GenericInquiryDacValidationService(genericInquiryDescriptionProvider, dacRegistry),
  IGIOnValidationService,
  IRowValidationService<RowSelected, GIOn>,
  IFieldValidationService<FieldDefaulting, GIOn, GIOn.designID>,
  IFieldValidationService<FieldDefaulting, GIOn, GIOn.relationNbr>
{
  public IEnumerable<GIRowValidationError> ValidateRow(PXCache cache, GIOn row)
  {
    this.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    GIRelation giRelation = (GIRelation) PXSelectBase<GIRelation, PXSelect<GIRelation, Where<GIRelation.designID, Equal<Required<GIRelation.designID>>, And<GIRelation.lineNbr, Equal<Required<GIRelation.lineNbr>>>>>.Config>.SelectSingleBound(this.Graph, (object[]) null, (object) this.DesignId, (object) row.RelationNbr);
    if (giRelation == null)
      return (IEnumerable<GIRowValidationError>) Array.Empty<GIRowValidationError>();
    GITable tableByAlias1 = this.GetTableByAlias(giRelation.ChildTable);
    GITable tableByAlias2 = this.GetTableByAlias(giRelation.ParentTable);
    HashSet<string> fieldsInRelation = this.GetFieldsInRelation(tableByAlias1?.Alias ?? tableByAlias2?.Alias);
    HashSet<string> allParameters = this.GetAllParameters();
    Func<string, string, bool> func = (Func<string, string, bool>) ((t, f) =>
    {
      if (string.IsNullOrEmpty(t))
        return true;
      System.Type type = PXBuildManager.GetType(t, false);
      if (type == (System.Type) null)
        return true;
      PXCache cach = this.Graph.Caches[type];
      return string.IsNullOrEmpty(f) || f.StartsWith("=") || cach.Fields.Contains(f) || string.Equals(PXGenericInqGrph.GetDeletedDatabaseRecord(cach).Field, f, StringComparison.OrdinalIgnoreCase) || string.Equals(PXGenericInqGrph.GetDatabaseRecordStatus(cach).Field, f, StringComparison.OrdinalIgnoreCase);
    });
    List<GIRowValidationError> rowValidationErrorList = new List<GIRowValidationError>();
    if (!func(tableByAlias2?.Name, row.ParentField) && !fieldsInRelation.Contains(row.ParentField) && !allParameters.Contains(row.ParentField))
      rowValidationErrorList.Add((GIRowValidationError) new GIFieldValidationError("parentField", (IBqlTable) row, PXErrorLevel.Warning, "A field with the name {0} cannot be found.", new string[1]
      {
        row.ParentField
      }));
    if (!func(tableByAlias1?.Name, row.ChildField) && !fieldsInRelation.Contains(row.ChildField) && !allParameters.Contains(row.ChildField))
      rowValidationErrorList.Add((GIRowValidationError) new GIFieldValidationError("childField", (IBqlTable) row, PXErrorLevel.Warning, "A field with the name {0} cannot be found.", new string[1]
      {
        row.ChildField
      }));
    int? type1;
    if (tableByAlias2 != null)
    {
      type1 = tableByAlias2.Type;
      int num = 1;
      if (type1.GetValueOrDefault() == num & type1.HasValue && !fieldsInRelation.Contains(row.ParentField))
      {
        GIRowValidationError rowValidationError = this.ValidateSubGIField((IBqlTable) row, tableByAlias2.Name, row.ParentField);
        if (rowValidationError != null)
          rowValidationErrorList.Add((GIRowValidationError) new GIFieldValidationError("ParentField", rowValidationError.Row, rowValidationError.ErrorLevel, rowValidationError.Message));
      }
    }
    if (tableByAlias1 != null)
    {
      type1 = tableByAlias1.Type;
      int num = 1;
      if (type1.GetValueOrDefault() == num & type1.HasValue && !fieldsInRelation.Contains(row.ChildField))
      {
        GIRowValidationError rowValidationError = this.ValidateSubGIField((IBqlTable) row, tableByAlias1.Name, row.ChildField);
        if (rowValidationError != null)
          rowValidationErrorList.Add((GIRowValidationError) new GIFieldValidationError("ChildField", rowValidationError.Row, rowValidationError.ErrorLevel, rowValidationError.Message));
      }
    }
    return (IEnumerable<GIRowValidationError>) rowValidationErrorList;
  }

  private HashSet<string> GetFieldsInRelation(string tableName)
  {
    if (string.IsNullOrEmpty(tableName))
      return new HashSet<string>();
    IEnumerable<GIRelation> tableRelations = this.GetTableRelations(tableName);
    HashSet<string> source = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (GIRelation giRelation in tableRelations)
    {
      GIRelation relation = giRelation;
      if (relation != null)
      {
        foreach (PXResult<GITable> pxResult in PXSelectBase<GITable, PXSelect<GITable, Where<GITable.designID, Equal<Required<GITable.designID>>>>.Config>.Select(this.Graph, (object) this.DesignId).AsEnumerable<PXResult<GITable>>().Where<PXResult<GITable>>((Func<PXResult<GITable>, bool>) (x =>
        {
          if (string.IsNullOrEmpty(((GITable) x).Name))
            return false;
          return string.Equals(((GITable) x).Alias, relation.ParentTable, StringComparison.OrdinalIgnoreCase) || string.Equals(((GITable) x).Alias, relation.ChildTable, StringComparison.OrdinalIgnoreCase);
        })))
        {
          GITable table = (GITable) pxResult;
          int? type1 = table.Type;
          int num = 1;
          if (type1.GetValueOrDefault() == num & type1.HasValue)
          {
            IEnumerable<string> strings = this.GetGIFields(table, true).Select<GenericInquiryDacValidationService.FieldItem, string>((Func<GenericInquiryDacValidationService.FieldItem, string>) (x => x.Name));
            EnumerableExtensions.AddRange<string>((ISet<string>) source, strings);
          }
          else
          {
            System.Type type2 = PXBuildManager.GetType(table.Name, false);
            if (!(type2 == (System.Type) null))
            {
              PXCache cache = this.Graph.Caches[type2];
              foreach (string str in cache.Fields.Where<string>((Func<string, bool>) (f => !PXGenericInqGrph.IsVirtualField(cache, f))))
                source.Add($"{table.Alias}.{str}");
            }
          }
        }
      }
    }
    return source.ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  }

  private IEnumerable<GIRelation> GetTableRelations(string tableName)
  {
    GIRelation[] array = PXSelectBase<GIRelation, PXSelect<GIRelation, Where<GIRelation.designID, Equal<Required<GIRelation.designID>>>>.Config>.Select(this.Graph, (object) this.DesignId).RowCast<GIRelation>().ToArray<GIRelation>();
    List<GIRelation> list = ((IEnumerable<GIRelation>) array).Where<GIRelation>((Func<GIRelation, bool>) (r => string.Equals(r.ParentTable, tableName, StringComparison.OrdinalIgnoreCase) || string.Equals(r.ChildTable, tableName, StringComparison.OrdinalIgnoreCase))).ToList<GIRelation>();
    bool flag = true;
    while (flag)
    {
      flag = false;
      foreach (GIRelation giRelation in array)
      {
        string[] tablesInRelation = new string[2]
        {
          giRelation.ParentTable,
          giRelation.ChildTable
        };
        if (!list.Contains(giRelation) && list.Any<GIRelation>((Func<GIRelation, bool>) (rel => ((IEnumerable<string>) tablesInRelation).Contains<string>(rel.ParentTable, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) || ((IEnumerable<string>) tablesInRelation).Contains<string>(rel.ChildTable, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))))
        {
          list.Add(giRelation);
          flag = true;
        }
      }
    }
    return (IEnumerable<GIRelation>) list;
  }

  IEnumerable<GIFieldValidationError> IFieldValidationService<FieldDefaulting, GIOn, GIOn.designID>.ValidateField(
    PXCache cache,
    GIOn row,
    object newValue)
  {
    return this.VerifyRelation(cache, row, "DesignID");
  }

  IEnumerable<GIFieldValidationError> IFieldValidationService<FieldDefaulting, GIOn, GIOn.relationNbr>.ValidateField(
    PXCache cache,
    GIOn row,
    object newValue)
  {
    return this.VerifyRelation(cache, row, "RelationNbr");
  }

  private IEnumerable<GIFieldValidationError> VerifyRelation(
    PXCache cache,
    GIOn row,
    string fieldName)
  {
    return cache.Graph.Caches<GIRelation>().Current != null ? (IEnumerable<GIFieldValidationError>) Array.Empty<GIFieldValidationError>() : EnumerableExtensions.AsSingleEnumerable<GIFieldValidationError>(new GIFieldValidationError(fieldName, (IBqlTable) row, PXErrorLevel.Error, "Please select a committed relation first."));
  }

  public override IEnumerable<GIValidationError> RunConsistencyValidation(
    PXCache cache,
    IEnumerable<IBqlTable> rows)
  {
    GIOnValidationService validationService = this;
    foreach (GIOn row in rows.Cast<GIOn>())
    {
      foreach (GIValidationError giValidationError in ((IFieldValidationService<FieldDefaulting, GIOn, GIOn.relationNbr>) validationService).ValidateField(cache, row, (object) row.DesignID))
        yield return giValidationError;
      foreach (GIValidationError giValidationError in ((IFieldValidationService<FieldDefaulting, GIOn, GIOn.designID>) validationService).ValidateField(cache, row, (object) row.RelationNbr))
        yield return giValidationError;
      // ISSUE: explicit non-virtual call
      foreach (GIValidationError giValidationError in __nonvirtual (validationService.ValidateRow(cache, row)))
        yield return giValidationError;
    }
  }
}
