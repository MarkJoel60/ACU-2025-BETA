// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.GITableValidationService
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

internal class GITableValidationService(
  IGenericInquiryReferenceInfoProvider genericInquiryReferenceInfoProvider,
  IDacRegistry dacRegistry,
  IGenericInquiryDescriptionProvider genericInquiryDescriptionProvider,
  IPXPageIndexingService pageIndexingService) : 
  GenericInquiryDacValidationService(genericInquiryDescriptionProvider, dacRegistry),
  IGITableValidationService,
  IRowValidationService<RowSelected, GITable>,
  IRowValidationService<RowInserted, GITable>,
  IRowValidationService<RowUpdated, GITable>,
  IRowValidationService<RowDeleted, GITable>,
  IRowValidationService<RowPersisting, GITable>
{
  IEnumerable<GIRowValidationError> IRowValidationService<RowSelected, GITable>.ValidateRow(
    PXCache cache,
    GITable row)
  {
    this.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    int? type = row.Type;
    int num = 1;
    return !(type.GetValueOrDefault() == num & type.HasValue) ? this.ValidateDacInGi(cache, row) : this.ValidateGiInGi(row);
  }

  private IEnumerable<GIRowValidationError> ValidateGiInGi(GITable row)
  {
    GITableValidationService validationService = this;
    if (row?.Name != null)
    {
      Guid designGiId;
      if (!Guid.TryParse(row.Name, out designGiId))
        yield return (GIRowValidationError) new GIFieldValidationError("Name", (IBqlTable) row, PXErrorLevel.Warning, "{0} is not recognized as a valid identifier of a generic inquiry.", new string[1]
        {
          row.Name
        });
      if (PXGenericInqGrph.Def[designGiId] == null)
        yield return (GIRowValidationError) new GIFieldValidationError("Name", (IBqlTable) row, PXErrorLevel.Warning, "This generic inquiry does not exist.");
      Guid? designId = (Guid?) validationService.GetCurrentGIDesignRecord()?.DesignID;
      if (designId.HasValue && genericInquiryReferenceInfoProvider.HasReferenceTo(designGiId, designId.Value))
        yield return (GIRowValidationError) new GIFieldValidationError("Name", (IBqlTable) row, PXErrorLevel.Error, "This generic inquiry references the generic inquiry you are modifying.");
    }
  }

  private IEnumerable<GIRowValidationError> ValidateDacInGi(PXCache cache, GITable row)
  {
    GITableValidationService validationService = this;
    IEnumerable<PXUIFieldAttribute> attributesOfType = cache.GetAttributesOfType<PXUIFieldAttribute>((object) row, "name");
    PXUIFieldAttribute pxuiFieldAttribute = attributesOfType != null ? attributesOfType.FirstOrDefault<PXUIFieldAttribute>() : (PXUIFieldAttribute) null;
    PXErrorLevel errorLevel = pxuiFieldAttribute != null ? pxuiFieldAttribute.ErrorLevel : PXErrorLevel.Undefined;
    if (errorLevel >= PXErrorLevel.Warning)
    {
      yield return new GIRowValidationError((IBqlTable) row, errorLevel, ((IPXInterfaceField) pxuiFieldAttribute).ErrorText);
    }
    else
    {
      GIRowValidationError error;
      System.Type type = validationService.GetTableType(row, "name", cache, (IBqlTable) row, out error, "This table is not found in the system.");
      if (error != null)
        yield return error;
      if (!(type == (System.Type) null) && !validationService.IsTableOfTypeVisible(type))
        yield return (GIRowValidationError) new GIFieldValidationError("Name", (IBqlTable) row, PXErrorLevel.Warning, "This table is obsolete. You cannot use it as a data source for new generic inquiries. Select another table.");
    }
  }

  public override IEnumerable<GIValidationError> RunConsistencyValidation(
    PXCache cache,
    IEnumerable<IBqlTable> rows)
  {
    GITableValidationService validationService = this;
    foreach (GITable row in rows.Cast<GITable>())
    {
      foreach (GIValidationError giValidationError in ((IRowValidationService<RowInserted, GITable>) validationService).ValidateRow(cache, row))
        yield return giValidationError;
      foreach (GIValidationError giValidationError in ((IRowValidationService<RowSelected, GITable>) validationService).ValidateRow(cache, row))
        yield return giValidationError;
      foreach (GIValidationError giValidationError in ((IRowValidationService<RowUpdated, GITable>) validationService).ValidateRow(cache, row))
        yield return giValidationError;
      foreach (GIValidationError giValidationError in ((IRowValidationService<RowPersisting, GITable>) validationService).ValidateRow(cache, row))
        yield return giValidationError;
    }
  }

  IEnumerable<GIRowValidationError> IRowValidationService<RowPersisting, GITable>.ValidateRow(
    PXCache cache,
    GITable row)
  {
    this.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    return row?.Name == null || row.Alias == null || cache.GetStatus((object) row) == PXEntryStatus.Deleted ? (IEnumerable<GIRowValidationError>) Array.Empty<GIRowValidationError>() : this.EnsureTableExists(row.Alias, "Name", cache, (IBqlTable) row, PXErrorLevel.Error, true);
  }

  IEnumerable<GIRowValidationError> IRowValidationService<RowInserted, GITable>.ValidateRow(
    PXCache cache,
    GITable row)
  {
    this.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    return this.VerifyTablesForPrimaryScreen();
  }

  IEnumerable<GIRowValidationError> IRowValidationService<RowUpdated, GITable>.ValidateRow(
    PXCache cache,
    GITable row)
  {
    this.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    return this.VerifyTablesForPrimaryScreen();
  }

  IEnumerable<GIRowValidationError> IRowValidationService<RowDeleted, GITable>.ValidateRow(
    PXCache cache,
    GITable row)
  {
    this.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    return this.VerifyTablesForPrimaryScreen();
  }

  private IEnumerable<GIRowValidationError> VerifyTablesForPrimaryScreen()
  {
    GITableValidationService validationService = this;
    GIDesign currentGiDesignRecord = validationService.GetCurrentGIDesignRecord();
    if (currentGiDesignRecord != null && currentGiDesignRecord.PrimaryScreenID != null && !GITableValidationService.IsGenericInquiryScreen(currentGiDesignRecord.PrimaryScreenID))
    {
      PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(currentGiDesignRecord.PrimaryScreenID);
      if (screenIdUnsecure != null)
      {
        string primaryView = pageIndexingService.GetPrimaryView(screenIdUnsecure.GraphType);
        if (!string.IsNullOrEmpty(primaryView))
        {
          PXCacheInfo cache = GraphHelper.GetGraphView(screenIdUnsecure.GraphType, primaryView).Cache;
          if ((GITable) PXSelectBase<GITable, PXSelect<GITable, Where<GITable.designID, Equal<Required<GIDesign.designID>>, And<GITable.name, Equal<Required<GITable.name>>>>>.Config>.SelectSingleBound(validationService.Graph, (object[]) null, (object) validationService.DesignId, (object) cache.Name) == null)
            yield return (GIRowValidationError) new GIFieldValidationError("PrimaryScreenID", (IBqlTable) currentGiDesignRecord, PXErrorLevel.Warning, "The primary screen has been reset because there are no tables defined for this primary screen.");
        }
      }
    }
  }

  private static bool IsGenericInquiryScreen(string screenID)
  {
    if (string.IsNullOrEmpty(screenID))
      return false;
    PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID);
    return screenIdUnsecure != null && !string.IsNullOrEmpty(screenIdUnsecure.Url) && PXSiteMap.IsGenericInquiry(screenIdUnsecure.Url);
  }
}
