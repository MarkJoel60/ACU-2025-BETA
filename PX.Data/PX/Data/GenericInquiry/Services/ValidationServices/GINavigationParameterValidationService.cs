// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.GINavigationParameterValidationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.GenericInquiry.Services.ValidationServices.Interface;
using PX.Data.Maintenance.GI;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.GenericInquiry.Services.ValidationServices;

internal class GINavigationParameterValidationService(
  IGenericInquiryDescriptionProvider genericInquiryDescriptionProvider,
  IDacRegistry dacRegistry) : 
  GenericInquiryDacValidationService(genericInquiryDescriptionProvider, dacRegistry),
  IGINavigationParameterValidationService,
  IRowValidationService<RowSelected, GINavigationParameter>,
  IFieldValidationService<FieldVerifying, GINavigationParameter, GINavigationParameter.parameterName>
{
  public IEnumerable<GIRowValidationError> ValidateRow(PXCache cache, GINavigationParameter row)
  {
    this.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    bool? isExpression = row.IsExpression;
    bool flag = true;
    return isExpression.GetValueOrDefault() == flag & isExpression.HasValue ? (IEnumerable<GIRowValidationError>) Array.Empty<GIRowValidationError>() : this.ValidateDataFieldName<GINavigationParameter.parameterName>(row.ParameterName, (IBqlTable) row, cache);
  }

  public IEnumerable<GIFieldValidationError> ValidateField(
    PXCache cache,
    GINavigationParameter row,
    object newValue)
  {
    if (row != null)
    {
      bool? isExpression = row.IsExpression;
      bool flag = true;
      if (!(isExpression.GetValueOrDefault() == flag & isExpression.HasValue) && newValue is string str && !str.StartsWith("="))
      {
        string[] allowedValues = cache.GetStateExt<GINavigationParameter.parameterName>((object) row) is PXStringState stateExt ? stateExt.AllowedValues : (string[]) null;
        if (allowedValues != null && allowedValues.Length > 0 && !((IEnumerable<string>) stateExt.AllowedValues).Contains<string>(str))
          yield return new GIFieldValidationError("ParameterName", (IBqlTable) row, PXErrorLevel.Error, "Invalid value. Please enter either a field name, a formula or a value from schema.");
      }
    }
  }

  public override IEnumerable<GIValidationError> RunConsistencyValidation(
    PXCache cache,
    IEnumerable<IBqlTable> rows)
  {
    foreach (GINavigationParameter row in rows.Cast<GINavigationParameter>())
    {
      foreach (GIValidationError giValidationError in this.ValidateRow(cache, row))
        yield return giValidationError;
      foreach (GIValidationError giValidationError in this.ValidateField(cache, row, (object) row.ParameterName))
        yield return giValidationError;
    }
  }
}
