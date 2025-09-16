// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.GenericInquiryValidationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.GenericInquiry.Services.ValidationServices.Interface;
using PX.Data.Maintenance.GI;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.GenericInquiry.Services.ValidationServices;

internal class GenericInquiryValidationService(
  IGIDesignValidationService giDesignValidationService,
  IGIFilterValidationService giFilterValidationService,
  IGIGroupByValidationService giGroupByValidationService,
  IGINavigationParameterValidationService giNavigationParameterValidationService,
  IGINavigationScreenValidationService giNavigationScreenValidationService,
  IGIOnValidationService giOnValidationService,
  IGIRelationValidationService giRelationValidationService,
  IGIResultValidationService giResultValidationService,
  IGISortValidationService giSortValidationService,
  IGITableValidationService giTableValidationService,
  IGIWhereValidationService giWhereValidationService) : IGenericInquiryValidationService
{
  private readonly GenericInquiryDacValidationService _giDesignValidationService = (GenericInquiryDacValidationService) giDesignValidationService;
  private readonly GenericInquiryDacValidationService _giFilterValidationService = (GenericInquiryDacValidationService) giFilterValidationService;
  private readonly GenericInquiryDacValidationService _giGroupByValidationService = (GenericInquiryDacValidationService) giGroupByValidationService;
  private readonly GenericInquiryDacValidationService _giNavigationParameterValidationService = (GenericInquiryDacValidationService) giNavigationParameterValidationService;
  private readonly GenericInquiryDacValidationService _giNavigationScreenValidationService = (GenericInquiryDacValidationService) giNavigationScreenValidationService;
  private readonly GenericInquiryDacValidationService _giOnValidationService = (GenericInquiryDacValidationService) giOnValidationService;
  private readonly GenericInquiryDacValidationService _giRelationValidationService = (GenericInquiryDacValidationService) giRelationValidationService;
  private readonly GenericInquiryDacValidationService _giResultValidationService = (GenericInquiryDacValidationService) giResultValidationService;
  private readonly GenericInquiryDacValidationService _giSortValidationService = (GenericInquiryDacValidationService) giSortValidationService;
  private readonly GenericInquiryDacValidationService _giTableValidationService = (GenericInquiryDacValidationService) giTableValidationService;
  private readonly GenericInquiryDacValidationService _giWhereValidationService = (GenericInquiryDacValidationService) giWhereValidationService;

  public IEnumerable<GIValidationError> RunConsistencyValidation(
    Dictionary<PXCache, IEnumerable<IBqlTable>> cachesWithRecords)
  {
    foreach (KeyValuePair<PXCache, IEnumerable<IBqlTable>> cachesWithRecord in cachesWithRecords)
    {
      PXCache pxCache;
      IEnumerable<IBqlTable> bqlTables;
      EnumerableExtensions.Deconstruct<PXCache, IEnumerable<IBqlTable>>(cachesWithRecord, ref pxCache, ref bqlTables);
      PXCache cache = pxCache;
      IEnumerable<IBqlTable> rows = bqlTables;
      GenericInquiryDacValidationService serviceByType = this.GetServiceByType(cache.BqlTable);
      if (serviceByType != null)
      {
        foreach (GIValidationError giValidationError in serviceByType.RunConsistencyValidation(cache, rows))
          yield return giValidationError;
      }
    }
  }

  public IEnumerable<GIRowValidationError> RunRowValidation<E, T>(PXCache cache, T row)
    where E : RowEventType
    where T : IBqlTable
  {
    return (this.GetServiceByType<T>() is IRowValidationService<E, T> serviceByType ? serviceByType.ValidateRow(cache, row) : (IEnumerable<GIRowValidationError>) null) ?? (IEnumerable<GIRowValidationError>) Array.Empty<GIRowValidationError>();
  }

  public IEnumerable<GIFieldValidationError> RunFieldValidation<E, T, F>(
    PXCache cache,
    T row,
    object newValue)
    where E : FieldEventType
    where T : IBqlTable
    where F : IBqlField
  {
    return (this.GetServiceByType<T>() is IFieldValidationService<E, T, F> serviceByType ? serviceByType.ValidateField(cache, row, newValue) : (IEnumerable<GIFieldValidationError>) null) ?? (IEnumerable<GIFieldValidationError>) Array.Empty<GIFieldValidationError>();
  }

  private GenericInquiryDacValidationService GetServiceByType<T>()
  {
    return this.GetServiceByType(typeof (T));
  }

  private GenericInquiryDacValidationService GetServiceByType(System.Type dacType)
  {
    return (object) dacType != null ? (!(dacType == typeof (GIDesign)) ? (!(dacType == typeof (GIFilter)) ? (!(dacType == typeof (GIGroupBy)) ? (!(dacType == typeof (GINavigationParameter)) ? (!(dacType == typeof (GINavigationScreen)) ? (!(dacType == typeof (GIOn)) ? (!(dacType == typeof (GIRelation)) ? (!(dacType == typeof (GIResult)) ? (!(dacType == typeof (GISort)) ? (!(dacType == typeof (GITable)) ? (!(dacType == typeof (GIWhere)) ? (GenericInquiryDacValidationService) null : this._giWhereValidationService) : this._giTableValidationService) : this._giSortValidationService) : this._giResultValidationService) : this._giRelationValidationService) : this._giOnValidationService) : this._giNavigationScreenValidationService) : this._giNavigationParameterValidationService) : this._giGroupByValidationService) : this._giFilterValidationService) : this._giDesignValidationService) : (GenericInquiryDacValidationService) null;
  }
}
