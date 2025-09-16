// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.GIFilterValidationService
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

internal class GIFilterValidationService(
  IGenericInquiryDescriptionProvider genericInquiryDescriptionProvider,
  IDacRegistry dacRegistry) : 
  GenericInquiryDacValidationService(genericInquiryDescriptionProvider, dacRegistry),
  IGIFilterValidationService,
  IRowValidationService<RowSelected, GIFilter>,
  IFieldValidationService<FieldUpdating, GIFilter, GIFilter.name>
{
  public IEnumerable<GIRowValidationError> ValidateRow(PXCache cache, GIFilter row)
  {
    this.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    string[] source = new string[2]
    {
      typeof (CheckboxCombobox.checkbox).FullName,
      typeof (CheckboxCombobox.combobox).FullName
    };
    List<GIRowValidationError> rowValidationErrorList = new List<GIRowValidationError>();
    string fieldName = row.FieldName;
    if (((IEnumerable<string>) source).Contains<string>(fieldName) || string.IsNullOrEmpty(fieldName))
      return (IEnumerable<GIRowValidationError>) rowValidationErrorList;
    (string str1, string str2) = GenericInquiryDacValidationService.SplitDataFieldName(fieldName);
    if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
      rowValidationErrorList.Add((GIRowValidationError) new GIFieldValidationError("FieldName", (IBqlTable) row, PXErrorLevel.Error, "A field with the name {0} cannot be found.", new string[1]
      {
        fieldName
      }));
    rowValidationErrorList.AddRange(this.EnsureTableExists(str1, "fieldName", cache, (IBqlTable) row));
    if (!string.IsNullOrEmpty(str1))
    {
      GITable tableByAlias = this.GetTableByAlias(str1);
      if (tableByAlias != null)
      {
        int? type = tableByAlias.Type;
        if (type.HasValue && type.GetValueOrDefault() == 1)
        {
          GIRowValidationError rowValidationError = this.ValidateSubGIField((IBqlTable) row, tableByAlias.Name, str2, true);
          if (rowValidationError != null)
            rowValidationErrorList.Add((GIRowValidationError) new GIFieldValidationError("FieldName", rowValidationError.Row, rowValidationError.ErrorLevel, rowValidationError.Message, rowValidationError.Arguments));
          return (IEnumerable<GIRowValidationError>) rowValidationErrorList;
        }
      }
    }
    return (IEnumerable<GIRowValidationError>) rowValidationErrorList;
  }

  public IEnumerable<GIFieldValidationError> ValidateField(
    PXCache cache,
    GIFilter row,
    object newValue)
  {
    string str = (string) newValue;
    if (!string.IsNullOrEmpty(str) && !str.All<char>((Func<char, bool>) (x => char.IsLetterOrDigit(x) || x == '_')))
    {
      PXStringListAttribute stringListAttribute = cache.GetAttributes<GIFilter.name>((object) row).OfType<PXStringListAttribute>().FirstOrDefault<PXStringListAttribute>();
      if ((stringListAttribute != null ? (!stringListAttribute.ValueLabelDic.ContainsKey(str) ? 1 : 0) : 1) != 0)
        yield return new GIFieldValidationError("Name", (IBqlTable) row, PXErrorLevel.Error, "The names of filter parameters can contain only numbers and letters.");
    }
  }

  public override IEnumerable<GIValidationError> RunConsistencyValidation(
    PXCache cache,
    IEnumerable<IBqlTable> rows)
  {
    foreach (GIFilter row in rows.Cast<GIFilter>())
    {
      foreach (GIValidationError giValidationError in this.ValidateRow(cache, row))
        yield return giValidationError;
      foreach (GIValidationError giValidationError in this.ValidateField(cache, row, (object) row.Name))
        yield return giValidationError;
    }
  }
}
