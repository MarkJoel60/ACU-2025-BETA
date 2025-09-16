// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.OrganizationDependedPeriodIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Abstractions.Periods;
using PX.Objects.Common.Extensions;
using PX.Objects.GL.Descriptor;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

public abstract class OrganizationDependedPeriodIDAttribute : 
  PeriodIDAttribute,
  IPXFieldDefaultingSubscriber
{
  public override Type SearchType
  {
    get => base.SearchType;
    set
    {
      base.SearchType = value;
      if (!this.FilterByOrganizationID)
        return;
      this.SearchTypeRestrictedByOrganization = this._searchType != (Type) null ? this.GetQueryWithRestrictionByOrganization(this._searchType) : (Type) null;
    }
  }

  public Type SearchTypeRestrictedByOrganization { get; set; }

  public override Type DefaultType
  {
    get => base.DefaultType;
    set
    {
      base.DefaultType = value;
      if (!this.FilterByOrganizationID)
        return;
      this.DefaultTypeRestrictedByOrganization = this._defaultType != (Type) null ? this.GetQueryWithRestrictionByOrganization(this._defaultType) : (Type) null;
    }
  }

  public Type DefaultTypeRestrictedByOrganization { get; set; }

  public bool RedefaultOrRevalidateOnOrganizationSourceUpdated { get; set; }

  public bool IsFilterMode { get; set; }

  public bool FilterByOrganizationID { get; set; }

  public bool UseMasterOrganizationIDByDefault { get; set; }

  public IPeriodKeyProvider<OrganizationDependedPeriodKey, PeriodKeyProviderBase.SourcesSpecificationCollectionBase> PeriodKeyProvider { get; set; }

  public OrganizationDependedPeriodIDAttribute(
    Type dateType = null,
    Type searchByDateType = null,
    Type defaultType = null,
    bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
    bool useMasterOrganizationIDByDefault = false,
    bool filterByOrganizationID = true,
    bool redefaultOnDateChanged = true)
    : base(dateType, searchByDateType, defaultType, redefaultOnDateChanged)
  {
    this.FilterByOrganizationID = filterByOrganizationID;
    this.DefaultType = this.DefaultType;
    this.SearchType = this.SearchType;
    this.UseMasterOrganizationIDByDefault = useMasterOrganizationIDByDefault;
    this.RedefaultOrRevalidateOnOrganizationSourceUpdated = redefaultOrRevalidateOnOrganizationSourceUpdated;
  }

  public PeriodKeyProviderBase.SourceSpecificationItem GetMainSpecificationItem(
    PXCache cache,
    object row)
  {
    return this.PeriodKeyProvider.GetMainSourceSpecificationItem(cache, row);
  }

  public PeriodKeyProviderBase.SourceSpecificationItem MainSpecificationItem
  {
    get => this.GetMainSpecificationItem((PXCache) null, (object) null);
  }

  public Type BranchSourceType
  {
    get => this.GetMainSpecificationItem((PXCache) null, (object) null)?.BranchSourceType;
  }

  public Type BranchSourceFormulaType
  {
    get => this.GetMainSpecificationItem((PXCache) null, (object) null)?.BranchSourceFormulaType;
  }

  public Type OrganizationSourceType
  {
    get => this.GetMainSpecificationItem((PXCache) null, (object) null)?.OrganizationSourceType;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.RowPersistingEvents rowPersisting = sender.Graph.RowPersisting;
    Type itemType = sender.GetItemType();
    OrganizationDependedPeriodIDAttribute periodIdAttribute = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) periodIdAttribute, __vmethodptr(periodIdAttribute, RowPersisting));
    rowPersisting.AddHandler(itemType, pxRowPersisting);
    if (this.PeriodKeyProvider == null)
      return;
    HashSet<PXCache> subscribedCaches = new HashSet<PXCache>();
    foreach (PeriodKeyProviderBase.SourceSpecificationItem specificationItem in this.PeriodKeyProvider.GetSourceSpecificationItems(sender, (object) null))
      this.SubscribeForSourceSpecificationItem(sender, subscribedCaches, specificationItem);
  }

  public virtual void SubscribeForSourceSpecificationItem(
    PXCache sender,
    HashSet<PXCache> subscribedCaches,
    PeriodKeyProviderBase.SourceSpecificationItem sourceSpecification)
  {
    if (!sourceSpecification.IsAnySourceSpecified || subscribedCaches.Contains(sender))
      return;
    this.SubscribeForSourceSpecificationItemImpl(sender, subscribedCaches, sourceSpecification);
    subscribedCaches.Add(sender);
  }

  public virtual void SubscribeForSourceSpecificationItemImpl(
    PXCache sender,
    HashSet<PXCache> subscribedCaches,
    PeriodKeyProviderBase.SourceSpecificationItem sourceSpecification)
  {
    if (!sender.Graph.IsImport && !sender.Graph.IsContractBasedAPI)
    {
      PXGraph.RowUpdatingEvents rowUpdating = sender.Graph.RowUpdating;
      Type itemType = sender.GetItemType();
      OrganizationDependedPeriodIDAttribute periodIdAttribute = this;
      // ISSUE: virtual method pointer
      PXRowUpdating pxRowUpdating = new PXRowUpdating((object) periodIdAttribute, __vmethodptr(periodIdAttribute, RowUpdating));
      rowUpdating.AddHandler(itemType, pxRowUpdating);
    }
    PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
    Type itemType1 = sender.GetItemType();
    OrganizationDependedPeriodIDAttribute periodIdAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) periodIdAttribute1, __vmethodptr(periodIdAttribute1, RowUpdated));
    rowUpdated.AddHandler(itemType1, pxRowUpdated);
  }

  protected abstract Type GetQueryWithRestrictionByOrganization(Type bqlQueryType);

  protected override Type GetExecutableDefaultType()
  {
    Type restrictedByOrganization = this.DefaultTypeRestrictedByOrganization;
    return (object) restrictedByOrganization != null ? restrictedByOrganization : this.DefaultType;
  }

  protected override bool IsSourcesValuesDefined(PXCache cache, object row)
  {
    return base.IsSourcesValuesDefined(cache, row) && this.PeriodKeyProvider.IsKeyDefined(cache.Graph, cache, row);
  }

  protected override OrganizationDependedPeriodKey GetPeriodKey(PXCache cache, object row)
  {
    return this.PeriodKeyProvider.GetKey(cache.Graph, cache, row);
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    this.GetFields(sender, e.Row);
    if (!(this._sourceDate != DateTime.MinValue) && (!(this.SourceType != (Type) null) || !(this.BranchSourceType != (Type) null) && !(this.OrganizationSourceType != (Type) null)))
      return;
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    PXGraph graph = sender.Graph;
    Type searchType = this.DefaultTypeRestrictedByOrganization;
    if ((object) searchType == null)
      searchType = this.DefaultType;
    DateTime? date = this._sourceDate == DateTime.MinValue ? new DateTime?() : new DateTime?(this._sourceDate);
    OrganizationDependedPeriodKey key = this.PeriodKeyProvider.GetKey(sender.Graph, sender, e.Row);
    List<object> listOrNull = e.Row.SingleToListOrNull<object>();
    string periodIdForDisplay = this.GetPeriod(graph, searchType, date, key, listOrNull).PeriodIDForDisplay;
    defaultingEventArgs.NewValue = (object) periodIdForDisplay;
  }

  protected virtual void RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    if (cache.Graph.UnattendedMode)
      return;
    this.ValidatePeriodAndSources(cache, e.Row, e.NewRow, e.ExternalCall);
  }

  protected virtual void ValidatePeriodAndSources(
    PXCache cache,
    object oldRow,
    object newRow,
    bool externalCall)
  {
    if (this.PeriodSourceFieldsEqual(cache, oldRow, newRow))
      return;
    this.ValidatePeriodAndSourcesImpl(cache, oldRow, newRow, externalCall);
  }

  protected abstract void ValidatePeriodAndSourcesImpl(
    PXCache cache,
    object oldRow,
    object newRow,
    bool externalCall);

  protected virtual void RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (this.PeriodSourceFieldsEqual(cache, e.OldRow, e.Row))
      return;
    this.RowUpdatedImpl(cache, e);
  }

  protected virtual void RowUpdatedImpl(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (!this.RedefaultOrRevalidateOnOrganizationSourceUpdated)
      return;
    object obj1 = (object) null;
    bool hasError = false;
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(e.Row, ((PXEventSubscriberAttribute) this)._FieldName))
    {
      if (subscriberAttribute is IPXInterfaceField ipxInterfaceField)
      {
        hasError = ipxInterfaceField.ErrorLevel == 4 || ipxInterfaceField.ErrorLevel == 5;
        if (hasError || ipxInterfaceField.ErrorLevel == 2 || ipxInterfaceField.ErrorLevel == 3)
        {
          obj1 = ipxInterfaceField.ErrorValue;
          object obj2 = hasError ? obj1 : (object) PeriodIDAttribute.FormatForDisplay((string) cache.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName));
          cache.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, obj2, (Exception) null);
        }
      }
    }
    OrganizationDependedPeriodKey fullKey1 = this.GetFullKey(cache, e.Row);
    OrganizationDependedPeriodKey fullKey2 = this.GetFullKey(cache, e.OldRow);
    if (this.ShouldExecuteRedefaultFinPeriodIDonRowUpdated(obj1, hasError, fullKey1, fullKey2) || cache.Graph.IsImport && !fullKey1.IsNotPeriodPartsEqual((object) fullKey2))
      this.RedefaultPeriodID(cache, e.Row);
    else if (this.IsFilterMode && !fullKey1.IsNotPeriodPartsEqual((object) fullKey2) && fullKey2.PeriodID == fullKey1.PeriodID && e.ExternalCall)
    {
      OrganizationDependedPeriodKey fullKey3 = this.GetFullKey(cache, e.OldRow);
      if (hasError)
        fullKey3.PeriodID = PeriodIDAttribute.UnFormatPeriod((string) obj1);
      string mappedPeriodId = this.GetMappedPeriodID(cache, fullKey1, fullKey3);
      cache.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) PeriodIDAttribute.FormatForDisplay(mappedPeriodId));
    }
    else if (!fullKey1.IsNotPeriodPartsEqual((object) fullKey2) && fullKey2.PeriodID != null && !cache.Graph.IsContractBasedAPI && !cache.Graph.UnattendedMode && !cache.Graph.IsImport && !cache.Graph.IsExport && !this.IsFilterMode && e.ExternalCall)
    {
      OrganizationDependedPeriodKey fullKey4 = this.GetFullKey(cache, e.OldRow);
      if (hasError)
        fullKey4.PeriodID = PeriodIDAttribute.UnFormatPeriod((string) obj1);
      else if (fullKey2.PeriodID != fullKey1.PeriodID)
        fullKey4.PeriodID = fullKey1.PeriodID;
      string mappedPeriodId = this.GetMappedPeriodID(cache, fullKey1, fullKey4);
      cache.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) PeriodIDAttribute.FormatForDisplay(mappedPeriodId));
    }
    else
      cache.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) PeriodIDAttribute.FormatForDisplay(fullKey1.PeriodID));
  }

  public virtual OrganizationDependedPeriodKey GetFullKey(PXCache cache, object row)
  {
    OrganizationDependedPeriodKey key = this.PeriodKeyProvider.GetKey(cache.Graph, cache, row);
    key.PeriodID = (string) cache.GetValue(row, ((PXEventSubscriberAttribute) this)._FieldName);
    return key;
  }

  protected abstract string GetMappedPeriodID(
    PXCache cache,
    OrganizationDependedPeriodKey newPeriodKey,
    OrganizationDependedPeriodKey oldPeriodKey);

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(this.BranchSourceType != (Type) null) || !sender.Graph.IsImport && !sender.Graph.IsContractBasedAPI && !sender.Graph.UnattendedMode || !EnumerableExtensions.IsIn<PXDBOperation>((PXDBOperation) (e.Operation & 3), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    this.ValidatePeriodAndSources(sender, (e.Operation & 3) != 2 ? sender.GetOriginal(e.Row) : (object) null, e.Row, !sender.Graph.UnattendedMode || sender.Graph.IsContractBasedAPI);
  }

  protected virtual bool ShouldExecuteRedefaultFinPeriodIDonRowUpdated(
    object errorValue,
    bool hasError,
    OrganizationDependedPeriodKey newPeriodKey,
    OrganizationDependedPeriodKey oldPeriodKey)
  {
    if (errorValue == null & hasError || newPeriodKey.OrganizationID.HasValue && !oldPeriodKey.OrganizationID.HasValue || newPeriodKey.PeriodID == null && oldPeriodKey.PeriodID == null || newPeriodKey.IsMasterCalendar && !oldPeriodKey.IsMasterCalendar)
      return true;
    return !newPeriodKey.IsMasterCalendar && oldPeriodKey.IsMasterCalendar;
  }

  protected void SetErrorAndResetToOldForField(
    PXCache cache,
    object oldRow,
    object newRow,
    string fieldName,
    PXSetPropertyException exception,
    bool externalCall)
  {
    object newValueByIncomig = this.GetNewValueByIncomig(cache, newRow, fieldName, externalCall);
    cache.RaiseExceptionHandling(fieldName, newRow, newValueByIncomig, (Exception) exception);
    cache.SetValueExt(newRow, fieldName, cache.GetValue(oldRow, fieldName));
  }

  protected virtual bool PeriodSourceFieldsEqual(PXCache cache, object oldRow, object newRow)
  {
    if (oldRow != null && newRow == null || oldRow == null && newRow != null)
      return false;
    string str1 = (string) cache.GetValue(newRow, ((PXEventSubscriberAttribute) this)._FieldName);
    string str2 = (string) cache.GetValue(oldRow, ((PXEventSubscriberAttribute) this)._FieldName);
    return this.PeriodKeyProvider.IsKeySourceValuesEquals(cache, oldRow, newRow) && str1 == str2;
  }

  protected object GetNewValueByIncomig(
    PXCache cache,
    object row,
    string fieldName,
    bool externalCall)
  {
    if (externalCall)
    {
      object valuePending = cache.GetValuePending(row, fieldName);
      if (valuePending != null)
        return valuePending;
    }
    try
    {
      object valueExt = cache.GetValueExt(row, fieldName);
      if (valueExt is PXFieldState pxFieldState)
        return pxFieldState.Value;
      if (valueExt != null)
        return valueExt;
    }
    catch
    {
    }
    return cache.GetValue(row, fieldName);
  }
}
