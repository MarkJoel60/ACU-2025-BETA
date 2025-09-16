// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.GINavigationScreenValidationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.GenericInquiry.Services.ValidationServices.Interface;
using PX.Data.Maintenance.GI;
using PX.Metadata;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.GenericInquiry.Services.ValidationServices;

internal class GINavigationScreenValidationService(
  IGenericInquiryDescriptionProvider genericInquiryDescriptionProvider,
  IDacRegistry dacRegistry) : 
  GenericInquiryDacValidationService(genericInquiryDescriptionProvider, dacRegistry),
  IGINavigationScreenValidationService,
  IRowValidationService<RowDeleting, GINavigationScreen>
{
  public IEnumerable<GIRowValidationError> ValidateRow(PXCache cache, GINavigationScreen row)
  {
    GINavigationScreenValidationService validationService = this;
    validationService.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    if (row != null && row.Link != null && string.Equals(row.Link, validationService.GetCurrentGIDesignRecord().PrimaryScreenID, StringComparison.OrdinalIgnoreCase))
    {
      int? lineNbr = row.LineNbr;
      int num = 1;
      if (lineNbr.GetValueOrDefault() == num & lineNbr.HasValue && row.WindowMode == "I")
        yield return new GIRowValidationError((IBqlTable) row, PXErrorLevel.Error, "The navigation settings for the entry screen cannot be deleted. Please remove the entry screen on the Entry Point tab first.");
    }
  }

  public override IEnumerable<GIValidationError> RunConsistencyValidation(
    PXCache cache,
    IEnumerable<IBqlTable> rows)
  {
    yield break;
  }
}
