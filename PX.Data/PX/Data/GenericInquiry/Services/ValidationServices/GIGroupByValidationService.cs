// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.GIGroupByValidationService
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

internal class GIGroupByValidationService(
  IGenericInquiryDescriptionProvider genericInquiryDescriptionProvider,
  IDacRegistry dacRegistry) : 
  GenericInquiryDacValidationService(genericInquiryDescriptionProvider, dacRegistry),
  IGIGroupByValidationService,
  IRowValidationService<RowSelected, GIGroupBy>
{
  public IEnumerable<GIRowValidationError> ValidateRow(PXCache cache, GIGroupBy row)
  {
    this.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    return this.ValidateDataFieldName<GIGroupBy.dataFieldName>(row.DataFieldName, (IBqlTable) row, cache);
  }

  public override IEnumerable<GIValidationError> RunConsistencyValidation(
    PXCache cache,
    IEnumerable<IBqlTable> rows)
  {
    return (IEnumerable<GIValidationError>) rows.Cast<GIGroupBy>().SelectMany<GIGroupBy, GIRowValidationError>((Func<GIGroupBy, IEnumerable<GIRowValidationError>>) (row => this.ValidateRow(cache, row)));
  }
}
