// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.GIRelationValidationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.GenericInquiry.Services.ValidationServices.Interface;
using PX.Data.Maintenance.GI;
using PX.Metadata;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.GenericInquiry.Services.ValidationServices;

internal class GIRelationValidationService(
  IGenericInquiryDescriptionProvider genericInquiryDescriptionProvider,
  IDacRegistry dacRegistry) : 
  GenericInquiryDacValidationService(genericInquiryDescriptionProvider, dacRegistry),
  IGIRelationValidationService,
  IRowValidationService<RowSelected, GIRelation>,
  IFieldValidationService<FieldUpdating, GIRelation, GIRelation.parentTable>,
  IFieldValidationService<FieldUpdating, GIRelation, GIRelation.childTable>
{
  public IEnumerable<GIRowValidationError> ValidateRow(PXCache cache, GIRelation row)
  {
    this.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    return this.EnsureTableExists(row.ParentTable, "parentTable", cache, (IBqlTable) row).Concat<GIRowValidationError>(this.EnsureTableExists(row.ChildTable, "childTable", cache, (IBqlTable) row));
  }

  IEnumerable<GIFieldValidationError> IFieldValidationService<FieldUpdating, GIRelation, GIRelation.parentTable>.ValidateField(
    PXCache cache,
    GIRelation row,
    object newValue)
  {
    return GIRelationValidationService.CheckTablesEquality("ParentTable", row.ChildTable, (string) newValue, row);
  }

  IEnumerable<GIFieldValidationError> IFieldValidationService<FieldUpdating, GIRelation, GIRelation.childTable>.ValidateField(
    PXCache cache,
    GIRelation row,
    object newValue)
  {
    return GIRelationValidationService.CheckTablesEquality("ChildTable", row.ParentTable, (string) newValue, row);
  }

  private static IEnumerable<GIFieldValidationError> CheckTablesEquality(
    string nameOfOriginalTable,
    string valueOfRelevantTable,
    string newValue,
    GIRelation row)
  {
    if (!string.IsNullOrEmpty(valueOfRelevantTable) && newValue == valueOfRelevantTable)
      yield return new GIFieldValidationError(nameOfOriginalTable, (IBqlTable) row, PXErrorLevel.Error, "Parent and child tables cannot be equal.");
  }

  public override IEnumerable<GIValidationError> RunConsistencyValidation(
    PXCache cache,
    IEnumerable<IBqlTable> rows)
  {
    GIRelationValidationService validationService = this;
    foreach (GIRelation row in rows.Cast<GIRelation>())
    {
      cache.Current = (object) row;
      // ISSUE: explicit non-virtual call
      foreach (GIValidationError giValidationError in __nonvirtual (validationService.ValidateRow(cache, row)))
        yield return giValidationError;
      foreach (GIValidationError giValidationError in ((IFieldValidationService<FieldUpdating, GIRelation, GIRelation.parentTable>) validationService).ValidateField(cache, row, (object) row.ParentTable))
        yield return giValidationError;
      foreach (GIValidationError giValidationError in ((IFieldValidationService<FieldUpdating, GIRelation, GIRelation.childTable>) validationService).ValidateField(cache, row, (object) row.ChildTable))
        yield return giValidationError;
    }
  }
}
