// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.GIDesignValidationService
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

internal class GIDesignValidationService(
  IGenericInquiryDescriptionProvider genericInquiryDescriptionProvider,
  IDacRegistry dacRegistry,
  IScreenInfoProvider screenInfoProvider,
  IGenericInquiryReferenceInfoProvider genericInquiryReferenceInfoProvider) : 
  GenericInquiryDacValidationService(genericInquiryDescriptionProvider, dacRegistry),
  IGIDesignValidationService,
  IRowValidationService<RowSelected, GIDesign>,
  IFieldValidationService<FieldVerifying, GIDesign, GIDesign.filterColCount>
{
  public IEnumerable<GIRowValidationError> ValidateRow(PXCache cache, GIDesign row)
  {
    GIDesignValidationService validationService = this;
    validationService.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    if (row.PrimaryScreenID != null)
    {
      foreach (GIRowValidationError rowValidationError in validationService.ValidatePrimaryScreen(row))
        yield return rowValidationError;
    }
    bool? nullable = row.ReplacePrimaryScreen;
    bool flag1 = true;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue && row.PrimaryScreenID == null)
      yield return (GIRowValidationError) new GIFieldValidationError("PrimaryScreenID", (IBqlTable) row, PXErrorLevel.Error, "You must define a primary screen.");
    nullable = row.ExposeViaOData;
    bool flag2 = true;
    if ((!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue) || !string.IsNullOrEmpty(row.SitemapScreenID) ? 1 : (!string.IsNullOrEmpty(row.SitemapTitle) ? 1 : 0)) == 0)
      yield return (GIRowValidationError) new GIFieldValidationError("ExposeViaOData", (IBqlTable) row, PXErrorLevel.Error, "You should make the screen visible in the UI first.");
    foreach (GIRowValidationError rowValidationError in validationService.CheckSortOrder(cache, row))
      yield return rowValidationError;
    GIRowValidationError rowValidationError1 = validationService.WarnWhenInquiryIsUsedByAnotherInquiry(row);
    if (rowValidationError1 != null)
      yield return rowValidationError1;
  }

  private IEnumerable<GIRowValidationError> ValidatePrimaryScreen(GIDesign row)
  {
    if (PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(row.PrimaryScreenID) == null)
      yield return (GIRowValidationError) new GIFieldValidationError("PrimaryScreenID", (IBqlTable) row, PXErrorLevel.Error, "A primary screen node for this inquiry form does not exist; it may have been moved or removed. Primary screen settings will be reset to default values.");
    else if (screenInfoProvider.TryGet(row.PrimaryScreenID) == null)
      yield return (GIRowValidationError) new GIFieldValidationError("PrimaryScreenID", (IBqlTable) row, PXErrorLevel.Warning, "This form cannot be automated.");
  }

  private IEnumerable<GIRowValidationError> CheckSortOrder(PXCache cache, GIDesign row)
  {
    GIDesignValidationService validationService = this;
    int num = PXSelectBase<GISort, PXSelect<GISort, Where<GISort.designID, Equal<Required<GIDesign.designID>>>>.Config>.Select(cache.Graph, (object) validationService.DesignId).AsEnumerable<PXResult<GISort>>().RowCast<GISort>().Any<GISort>((Func<GISort, bool>) (s =>
    {
      bool? isActive = s.IsActive;
      bool flag = true;
      return isActive.GetValueOrDefault() == flag & isActive.HasValue;
    })) ? 1 : 0;
    row.DefaultSortOrder = (string) null;
    if (num == 0)
    {
      HashSet<string> usedInRelations = validationService.GetTablesUsedInRelations();
      List<GITable> giTableList = new List<GITable>();
      IEnumerable<GITable> source = PXSelectBase<GITable, PXSelect<GITable, Where<GITable.designID, Equal<Required<GIDesign.designID>>>>.Config>.Select(cache.Graph, (object) validationService.DesignId).AsEnumerable<PXResult<GITable>>().RowCast<GITable>();
      giTableList.AddRange(source.Where<GITable>((Func<GITable, bool>) (table => usedInRelations.Count == 0 || usedInRelations.Contains(table.Alias))));
      List<string> values = new List<string>();
      bool flag = false;
      foreach (GITable table in giTableList)
      {
        foreach (string tableKey in GetTableKeys(table))
        {
          values.Add($"{table.Alias}.{tableKey} {PXLocalizer.Localize("ASC", typeof (InfoMessages).FullName)}");
          flag = flag || validationService.IsComplexSort(table, tableKey);
        }
      }
      if (values.Count > 0)
      {
        row.DefaultSortOrder = string.Join(", ", (IEnumerable<string>) values);
        yield return (GIRowValidationError) new GIFieldValidationError("DefaultSortOrder", (IBqlTable) row, PXErrorLevel.Warning, flag ? "No sort order was defined. Default sort order will be used. We recommend that you define a custom sort order because the default order may slow down the inquiry." : "No sort order was defined. Default sort order will be used.");
      }
    }

    IEnumerable<string> GetTableKeys(GITable table)
    {
      if (string.IsNullOrEmpty(table.Name))
        return (IEnumerable<string>) Array<string>.Empty;
      int? type1 = table.Type;
      int num = 1;
      if (type1.GetValueOrDefault() == num & type1.HasValue)
        return this.GetGIFields(table, false, (Func<PXCache, string, bool>) ((tableCache, fiendName) => tableCache.Keys.Contains(fiendName))).Select<GenericInquiryDacValidationService.FieldItem, string>((Func<GenericInquiryDacValidationService.FieldItem, string>) (x => x.Name));
      System.Type type2 = PXBuildManager.GetType(table.Name, false);
      return !(type2 == (System.Type) null) ? (IEnumerable<string>) this.Graph.Caches[type2].Keys : (IEnumerable<string>) new KeysCollection();
    }
  }

  private HashSet<string> GetTablesUsedInRelations()
  {
    HashSet<string> tablesUsedInRelations = new HashSet<string>();
    foreach (GIRelation giRelation in PXSelectBase<GIRelation, PXSelect<GIRelation, Where<GIRelation.designID, Equal<Required<GIDesign.designID>>>>.Config>.Select(this.Graph, (object) this.DesignId).AsEnumerable<PXResult<GIRelation>>().RowCast<GIRelation>())
    {
      bool? isActive = giRelation.IsActive;
      bool flag = true;
      if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      {
        tablesUsedInRelations.Add(giRelation.ParentTable);
        tablesUsedInRelations.Add(giRelation.ChildTable);
      }
    }
    return tablesUsedInRelations;
  }

  public override IEnumerable<GIValidationError> RunConsistencyValidation(
    PXCache cache,
    IEnumerable<IBqlTable> rows)
  {
    foreach (GIDesign row in rows.Cast<GIDesign>())
    {
      cache.Current = (object) row;
      foreach (GIValidationError giValidationError in this.ValidateRow(cache, row))
        yield return giValidationError;
      foreach (GIValidationError giValidationError in this.ValidateField(cache, row, (object) row.FilterColCount))
        yield return giValidationError;
    }
  }

  private bool IsComplexSort(GITable table, string field)
  {
    return this.IsComplexSort(table, field, out string _);
  }

  private GIRowValidationError WarnWhenInquiryIsUsedByAnotherInquiry(GIDesign row)
  {
    return !genericInquiryReferenceInfoProvider.GetReferencesTo(this.DesignId).Any<(string, Guid)>() ? (GIRowValidationError) null : (GIRowValidationError) new GIFieldValidationError("Name", (IBqlTable) row, PXErrorLevel.Warning, "This inquiry is used as a source for another generic inquiry. Changes can corrupt the data output of the parent inquiry.");
  }

  public IEnumerable<GIFieldValidationError> ValidateField(
    PXCache cache,
    GIDesign row,
    object newValue)
  {
    this.SetGraphAndDesignId(cache.Graph, row.DesignID.Value);
    int? nullable = newValue as int?;
    if (nullable.HasValue && nullable.GetValueOrDefault() <= 0)
      yield return new GIFieldValidationError("FilterColCount", (IBqlTable) row, PXErrorLevel.Error, "The value has to be greater than 0.");
  }
}
