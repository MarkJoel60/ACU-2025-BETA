// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookPeriodIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Abstractions.Periods;
using PX.Objects.Common.Extensions;
using PX.Objects.FA.Descriptor;
using PX.Objects.GL;
using PX.Objects.GL.Descriptor;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA;

public class FABookPeriodIDAttribute : OrganizationDependedPeriodIDAttribute
{
  public FABookPeriodKeyProvider FABookPeriodKeyProvider { get; set; }

  public FABookPeriodKeyProvider.FASourceSpecificationItem FAMainSpecificationItem
  {
    get => (FABookPeriodKeyProvider.FASourceSpecificationItem) this.MainSpecificationItem;
  }

  public FABookPeriodIDAttribute(
    Type bookSourceType = null,
    Type dateType = null,
    bool isBookRequired = true,
    Type assetSourceType = null,
    Type branchSourceType = null,
    Type branchSourceFormulaType = null,
    Type organizationSourceType = null,
    Type searchByDateType = null,
    Type defaultType = null)
  {
    Type dateType1 = dateType;
    Type searchByDateType1 = searchByDateType;
    if ((object) searchByDateType1 == null)
      searchByDateType1 = typeof (Search<FABookPeriod.finPeriodID, Where<FABookPeriod.startDate, LessEqual<Current2<PeriodIDAttribute.QueryParams.sourceDate>>, And<FABookPeriod.endDate, Greater<Current2<PeriodIDAttribute.QueryParams.sourceDate>>>>>);
    Type defaultType1 = defaultType;
    // ISSUE: explicit constructor call
    base.\u002Ector(dateType1, searchByDateType1, defaultType1);
    PeriodKeyProviderBase.SourcesSpecificationCollection<FABookPeriodKeyProvider.FASourceSpecificationItem> sourcesSpecification = new PeriodKeyProviderBase.SourcesSpecificationCollection<FABookPeriodKeyProvider.FASourceSpecificationItem>();
    FABookPeriodKeyProvider.FASourceSpecificationItem specificationItem = new FABookPeriodKeyProvider.FASourceSpecificationItem();
    specificationItem.AssetSourceType = assetSourceType;
    specificationItem.BookSourceType = bookSourceType;
    specificationItem.IsBookRequired = isBookRequired;
    specificationItem.BranchSourceType = branchSourceType;
    specificationItem.BranchSourceFormulaType = branchSourceFormulaType;
    specificationItem.OrganizationSourceType = organizationSourceType;
    sourcesSpecification.SpecificationItems = specificationItem.SingleToList<FABookPeriodKeyProvider.FASourceSpecificationItem>();
    this.PeriodKeyProvider = (IPeriodKeyProvider<OrganizationDependedPeriodKey, PeriodKeyProviderBase.SourcesSpecificationCollectionBase>) (this.FABookPeriodKeyProvider = new FABookPeriodKeyProvider(sourcesSpecification));
  }

  protected override bool ShouldExecuteRedefaultFinPeriodIDonRowUpdated(
    object errorValue,
    bool hasError,
    OrganizationDependedPeriodKey newPeriodKey,
    OrganizationDependedPeriodKey oldPeriodKey)
  {
    FABookPeriod.Key newPeriodKey1 = (FABookPeriod.Key) newPeriodKey;
    FABookPeriod.Key oldPeriodKey1 = (FABookPeriod.Key) oldPeriodKey;
    if (base.ShouldExecuteRedefaultFinPeriodIDonRowUpdated(errorValue, hasError, (OrganizationDependedPeriodKey) newPeriodKey1, (OrganizationDependedPeriodKey) oldPeriodKey1))
      return true;
    int? bookId1 = oldPeriodKey1.BookID;
    int? bookId2 = newPeriodKey1.BookID;
    return !(bookId1.GetValueOrDefault() == bookId2.GetValueOrDefault() & bookId1.HasValue == bookId2.HasValue);
  }

  protected override Type GetQueryWithRestrictionByOrganization(Type bqlQueryType)
  {
    return BqlCommand.CreateInstance(new Type[1]
    {
      bqlQueryType
    }).WhereAnd(typeof (Where<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>>>)).GetType();
  }

  protected override void ValidatePeriodAndSourcesImpl(
    PXCache cache,
    object oldRow,
    object newRow,
    bool externalCall)
  {
    PeriodKeyProviderBase.KeyWithSourceValuesCollection<FABookPeriodKeyProvider.FAKeyWithSourceValues, FABookPeriodKeyProvider.FASourceSpecificationItem, FABookPeriod.Key> keys1 = this.FABookPeriodKeyProvider.GetKeys(cache.Graph, cache, newRow, false);
    PeriodKeyProviderBase.KeyWithSourceValuesCollection<FABookPeriodKeyProvider.FAKeyWithSourceValues, FABookPeriodKeyProvider.FASourceSpecificationItem, FABookPeriod.Key> keys2 = this.FABookPeriodKeyProvider.GetKeys(cache.Graph, cache, oldRow, false);
    FABookPeriod.Key consolidatedKey = keys1.ConsolidatedKey;
    consolidatedKey.PeriodID = (string) cache.GetValue(newRow, ((PXEventSubscriberAttribute) this)._FieldName);
    if (!consolidatedKey.Defined || cache.Graph.GetService<IFABookPeriodRepository>().FindByKey(consolidatedKey.BookID, consolidatedKey.OrganizationID, consolidatedKey.PeriodID) != null)
      return;
    FABook byId = BookMaint.FindByID(cache.Graph, consolidatedKey.BookID);
    PXSetPropertyException exception;
    int? nullable1;
    int? nullable2;
    if (byId.UpdateGL.GetValueOrDefault())
    {
      exception = new PXSetPropertyException((IBqlTable) newRow, PXMessages.LocalizeFormatNoPrefix("The {0} period does not exist for the {1} book and the {2} company.", new object[3]
      {
        (object) PeriodIDAttribute.FormatForError(consolidatedKey.PeriodID),
        (object) byId.BookCode,
        (object) PXAccess.GetOrganizationCD(consolidatedKey.OrganizationID)
      }));
      if (this.FAMainSpecificationItem.OrganizationSourceType != (Type) null && keys1.MainItem.SourceOrganizationIDs.First<int?>().HasValue)
      {
        int? nullable3 = keys1.MainItem.SourceOrganizationIDs.First<int?>();
        nullable1 = keys2.MainItem.SourceOrganizationIDs.First<int?>();
        if (!(nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue))
          this.SetErrorAndResetToOldForField(cache, oldRow, newRow, this.FAMainSpecificationItem.OrganizationSourceType.Name, exception, externalCall);
      }
      if (this.FAMainSpecificationItem.BranchSourceType != (Type) null)
      {
        nullable1 = keys1.MainItem.SourceBranchIDs.First<int?>();
        if (nullable1.HasValue)
        {
          nullable1 = keys1.MainItem.SourceBranchIDs.First<int?>();
          nullable2 = keys2.MainItem.SourceBranchIDs.First<int?>();
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            this.SetErrorAndResetToOldForField(cache, oldRow, newRow, this.FAMainSpecificationItem.BranchSourceType.Name, exception, externalCall);
        }
      }
      if (this.FAMainSpecificationItem.AssetSourceType != (Type) null)
      {
        nullable2 = keys1.MainItem.SourceAssetIDs.First<int?>();
        if (nullable2.HasValue)
        {
          nullable2 = keys1.MainItem.SourceAssetIDs.First<int?>();
          nullable1 = keys2.MainItem.SourceAssetIDs.First<int?>();
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            this.SetErrorAndResetToOldForField(cache, oldRow, newRow, this.FAMainSpecificationItem.AssetSourceType.Name, exception, externalCall);
        }
      }
    }
    else
      exception = new PXSetPropertyException((IBqlTable) newRow, PXMessages.LocalizeFormatNoPrefix("The {0} period does not exist for the {1} book.", new object[2]
      {
        (object) PeriodIDAttribute.FormatForError(consolidatedKey.PeriodID),
        (object) byId.BookCode
      }));
    cache.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, newRow, (object) PeriodIDAttribute.FormatForDisplay(consolidatedKey.PeriodID), (Exception) exception);
    cache.SetValue(newRow, ((PXEventSubscriberAttribute) this)._FieldName, cache.GetValue(oldRow, ((PXEventSubscriberAttribute) this)._FieldName));
    if (!(this.FAMainSpecificationItem.BookSourceType != (Type) null))
      return;
    nullable1 = keys1.MainItem.SourceBookIDs.First<int?>();
    if (!nullable1.HasValue)
      return;
    nullable1 = keys1.MainItem.SourceBookIDs.First<int?>();
    nullable2 = keys2.MainItem.SourceBookIDs.First<int?>();
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    this.SetErrorAndResetToOldForField(cache, oldRow, newRow, this.FAMainSpecificationItem.BookSourceType.Name, exception, externalCall);
  }

  protected override string GetMappedPeriodID(
    PXCache cache,
    OrganizationDependedPeriodKey newPeriodKey,
    OrganizationDependedPeriodKey oldPeriodKey)
  {
    return cache.Graph.GetService<IFABookPeriodRepository>().FindMappedPeriod((FABookPeriod.Key) oldPeriodKey, (FABookPeriod.Key) newPeriodKey)?.FinPeriodID;
  }

  protected override PeriodIDAttribute.PeriodResult GetPeriod(
    PXGraph graph,
    Type searchType,
    DateTime? date,
    OrganizationDependedPeriodKey periodKey,
    List<object> currents = null)
  {
    BqlCommand bqlCommand = BqlCommand.CreateInstance(new Type[1]
    {
      searchType
    }) ?? throw new ArgumentNullException("BqlCommand.CreateInstance(searchType)");
    Type itemType = BqlCommand.GetItemType(((IBqlSearch) bqlCommand).GetField());
    List<object> objectList1 = new List<object>()
    {
      (object) new PeriodIDAttribute.QueryParams()
      {
        SourceDate = date
      }
    };
    if (currents != null)
      objectList1.AddRange((IEnumerable<object>) currents);
    PXView view = graph.TypedViews.GetView(bqlCommand, false);
    int num1 = 0;
    int num2 = 0;
    object[] array1 = objectList1.ToArray();
    object[] array2 = periodKey.ToListOfObjects(true).ToArray();
    ref int local1 = ref num1;
    ref int local2 = ref num2;
    List<object> objectList2 = view.Select(array1, array2, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref local1, 1, ref local2);
    if (objectList2 == null || objectList2.Count <= 0)
      return new PeriodIDAttribute.PeriodResult((IPeriod) null);
    object obj = objectList2[objectList2.Count - 1];
    if (obj != null && obj is PXResult)
      obj = ((PXResult) obj)[itemType];
    return new PeriodIDAttribute.PeriodResult(obj as IPeriod);
  }
}
