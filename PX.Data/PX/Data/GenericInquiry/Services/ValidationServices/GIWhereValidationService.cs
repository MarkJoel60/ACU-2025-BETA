// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.GIWhereValidationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Description.GI;
using PX.Data.GenericInquiry.Services.ValidationServices.Interface;
using PX.Data.Maintenance.GI;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.GenericInquiry.Services.ValidationServices;

internal class GIWhereValidationService(
  IGenericInquiryDescriptionProvider genericInquiryDescriptionProvider,
  IDacRegistry dacRegistry) : 
  GenericInquiryDacValidationService(genericInquiryDescriptionProvider, dacRegistry),
  IGIWhereValidationService,
  IRowValidationService<RowSelected, GIWhere>,
  IFieldValidationService<FieldUpdating, GIWhere, GIWhere.condition>,
  IFieldValidationService<FieldUpdating, GIWhere, GIWhere.value1>
{
  public IEnumerable<GIRowValidationError> ValidateRow(PXCache cache, GIWhere row)
  {
    this.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    return this.GetAllParameters().Contains(row.DataFieldName) ? (IEnumerable<GIRowValidationError>) Array.Empty<GIRowValidationError>() : this.ValidateDataFieldName<GIWhere.dataFieldName>(row.DataFieldName, (IBqlTable) row, cache);
  }

  IEnumerable<GIFieldValidationError> IFieldValidationService<FieldUpdating, GIWhere, GIWhere.condition>.ValidateField(
    PXCache cache,
    GIWhere row,
    object newValue)
  {
    return this.ValidateWhere(newValue as string, row.Value1, "Condition", row);
  }

  IEnumerable<GIFieldValidationError> IFieldValidationService<FieldUpdating, GIWhere, GIWhere.value1>.ValidateField(
    PXCache cache,
    GIWhere row,
    object newValue)
  {
    string str = newValue as string;
    return this.ValidateWhere(row.Condition, str, "Value1", row);
  }

  private IEnumerable<GIFieldValidationError> ValidateWhere(
    string condition,
    string value,
    string fieldName,
    GIWhere row)
  {
    if (!FilterVariable.GetVariableType(value).HasValue || condition == null)
      return (IEnumerable<GIFieldValidationError>) Array.Empty<GIFieldValidationError>();
    string violationMessage = FilterVariable.GetConditionViolationMessage(value, ValFromStr.GetCondition(ValFromStr.GetCondition(condition)));
    return !string.IsNullOrEmpty(violationMessage) ? EnumerableExtensions.AsSingleEnumerable<GIFieldValidationError>(new GIFieldValidationError(fieldName, (IBqlTable) row, PXErrorLevel.Error, violationMessage)) : (IEnumerable<GIFieldValidationError>) Array.Empty<GIFieldValidationError>();
  }

  public override IEnumerable<GIValidationError> RunConsistencyValidation(
    PXCache cache,
    IEnumerable<IBqlTable> rows)
  {
    GIWhereValidationService validationService = this;
    foreach (GIWhere row in rows.Cast<GIWhere>())
    {
      // ISSUE: explicit non-virtual call
      foreach (GIValidationError giValidationError in __nonvirtual (validationService.ValidateRow(cache, row)))
        yield return giValidationError;
      foreach (GIValidationError giValidationError in ((IFieldValidationService<FieldUpdating, GIWhere, GIWhere.condition>) validationService).ValidateField(cache, row, (object) row.Condition))
        yield return giValidationError;
      foreach (GIValidationError giValidationError in ((IFieldValidationService<FieldUpdating, GIWhere, GIWhere.value1>) validationService).ValidateField(cache, row, (object) row.Value1))
        yield return giValidationError;
    }
  }
}
