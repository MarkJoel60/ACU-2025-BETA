// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriodIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions.Periods;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL.Descriptor;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Attribute describes FinPeriod Field.
/// This attribute contains static Util functions.
/// </summary>
public class FinPeriodIDAttribute : OrganizationDependedPeriodIDAttribute
{
  public FinPeriodIDAttribute.HeaderFindingModes HeaderFindingMode { get; set; }

  public bool IsHeader { get; set; }

  public bool CalculatePeriodByHeader { get; set; }

  public bool AutoCalculateMasterPeriod { get; set; }

  public Type MasterFinPeriodIDType { get; set; }

  public Type HeaderMasterFinPeriodIDType { get; set; }

  public Type UseMasterCalendarSourceType { get; set; }

  public ICalendarOrganizationIDProvider CalendarOrganizationIDProvider { get; protected set; }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public FinPeriodIDAttribute(
    Type sourceType = null,
    Type branchSourceType = null,
    Type branchSourceFormulaType = null,
    Type organizationSourceType = null,
    Type useMasterCalendarSourceType = null,
    Type defaultType = null,
    bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
    bool useMasterOrganizationIDByDefault = false,
    Type[] sourceSpecificationTypes = null,
    Type masterFinPeriodIDType = null,
    Type headerMasterFinPeriodIDType = null,
    bool filterByOrganizationID = true,
    bool redefaultOnDateChanged = true)
    : base(sourceType, typeof (Search<FinPeriod.finPeriodID, Where<FinPeriod.startDate, LessEqual<Current2<PeriodIDAttribute.QueryParams.sourceDate>>, And<FinPeriod.endDate, Greater<Current2<PeriodIDAttribute.QueryParams.sourceDate>>>>>), defaultType, redefaultOrRevalidateOnOrganizationSourceUpdated, useMasterOrganizationIDByDefault, redefaultOnDateChanged: redefaultOnDateChanged)
  {
    this.HeaderFindingMode = FinPeriodIDAttribute.HeaderFindingModes.Current;
    this.CalculatePeriodByHeader = true;
    this.AutoCalculateMasterPeriod = true;
    this.MasterFinPeriodIDType = masterFinPeriodIDType;
    this.HeaderMasterFinPeriodIDType = headerMasterFinPeriodIDType;
    this.FilterByOrganizationID = filterByOrganizationID;
    this.UseMasterOrganizationIDByDefault = useMasterOrganizationIDByDefault;
    this.UseMasterCalendarSourceType = useMasterCalendarSourceType;
    this.RedefaultOrRevalidateOnOrganizationSourceUpdated = redefaultOrRevalidateOnOrganizationSourceUpdated;
    PeriodKeyProviderBase.SourcesSpecificationCollection sourcesSpecification = new PeriodKeyProviderBase.SourcesSpecificationCollection();
    sourcesSpecification.SpecificationItems = new PeriodKeyProviderBase.SourceSpecificationItem()
    {
      BranchSourceType = branchSourceType,
      BranchSourceFormulaType = branchSourceFormulaType,
      OrganizationSourceType = organizationSourceType
    }.SingleToList<PeriodKeyProviderBase.SourceSpecificationItem>();
    this.PeriodKeyProvider = (IPeriodKeyProvider<OrganizationDependedPeriodKey, PeriodKeyProviderBase.SourcesSpecificationCollectionBase>) (this.CalendarOrganizationIDProvider = (ICalendarOrganizationIDProvider) new PX.Objects.GL.Descriptor.CalendarOrganizationIDProvider(sourcesSpecification, sourceSpecificationTypes, useMasterCalendarSourceType, useMasterOrganizationIDByDefault));
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (this.MasterFinPeriodIDType != (Type) null)
    {
      PXGraph.FieldDefaultingEvents fieldDefaulting = sender.Graph.FieldDefaulting;
      Type itemType = sender.GetItemType();
      string name = this.MasterFinPeriodIDType.Name;
      FinPeriodIDAttribute periodIdAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) periodIdAttribute, __vmethodptr(periodIdAttribute, MasterFinPeriodIDFieldDefaulting));
      fieldDefaulting.AddHandler(itemType, name, pxFieldDefaulting);
    }
    if (!(this.UseMasterCalendarSourceType != (Type) null))
      return;
    if (this.OrganizationSourceType != (Type) null)
    {
      PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
      Type itemType = BqlCommand.GetItemType(this.OrganizationSourceType);
      string name = this.OrganizationSourceType.Name;
      FinPeriodIDAttribute periodIdAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) periodIdAttribute, __vmethodptr(periodIdAttribute, CalendarOrganizationIDSourceFieldUpdated));
      fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
    }
    if (!(this.BranchSourceType != (Type) null))
      return;
    PXGraph.FieldUpdatedEvents fieldUpdated1 = sender.Graph.FieldUpdated;
    Type itemType1 = BqlCommand.GetItemType(this.BranchSourceType);
    string name1 = this.BranchSourceType.Name;
    FinPeriodIDAttribute periodIdAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) periodIdAttribute1, __vmethodptr(periodIdAttribute1, CalendarOrganizationIDSourceFieldUpdated));
    fieldUpdated1.AddHandler(itemType1, name1, pxFieldUpdated1);
  }

  public override void SubscribeForSourceSpecificationItemImpl(
    PXCache sender,
    HashSet<PXCache> subscribedCaches,
    PeriodKeyProviderBase.SourceSpecificationItem sourceSpecification)
  {
    base.SubscribeForSourceSpecificationItemImpl(sender, subscribedCaches, sourceSpecification);
    if (!(this.MasterFinPeriodIDType != (Type) null))
      return;
    // ISSUE: method pointer
    sender.Graph.RowInserted.AddHandler(sender.GetItemType(), new PXRowInserted((object) this, __methodptr(RowInserted)));
  }

  protected override Type GetQueryWithRestrictionByOrganization(Type bqlQueryType)
  {
    return BqlCommand.CreateInstance(new Type[1]
    {
      bqlQueryType
    }).WhereAnd(typeof (Where<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>)).GetType();
  }

  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    this.SetUseMasterCalendarValue(sender, e.Row);
    if (this.HeaderMasterFinPeriodIDType != (Type) null && this.CalculatePeriodByHeader)
    {
      FinPeriod result = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(this.CalendarOrganizationIDProvider.GetCalendarOrganizationID(sender.Graph, sender, e.Row), this.GetHeaderMasterFinPeriodID(sender, e.Row)).Result;
      if (result == null)
        return;
      e.NewValue = (object) PeriodIDAttribute.FormatForDisplay(result.FinPeriodID);
    }
    else
      base.FieldDefaulting(sender, e);
  }

  public virtual void MasterFinPeriodIDFieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) PeriodIDAttribute.FormatForDisplay(this.CalcMasterPeriodID(sender, e.Row));
  }

  protected virtual void CalendarOrganizationIDSourceFieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    this.SetUseMasterCalendarValue(cache, e.Row);
  }

  public void RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (!this.AutoCalculateMasterPeriod)
      return;
    this.SetMasterPeriodID(cache, e.Row);
  }

  protected override void RowUpdatedImpl(PXCache cache, PXRowUpdatedEventArgs e)
  {
    base.RowUpdatedImpl(cache, e);
    if (!this.AutoCalculateMasterPeriod)
      return;
    this.SetMasterPeriodID(cache, e.Row);
  }

  protected override bool PeriodSourceFieldsEqual(PXCache cache, object oldRow, object newRow)
  {
    bool flag = base.PeriodSourceFieldsEqual(cache, oldRow, newRow);
    if (this.UseMasterCalendarSourceType != (Type) null)
    {
      int num1 = flag ? 1 : 0;
      bool? nullable1 = (bool?) cache.GetValue(newRow, this.UseMasterCalendarSourceType.Name);
      bool? nullable2 = (bool?) cache.GetValue(oldRow, this.UseMasterCalendarSourceType.Name);
      int num2 = nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue ? 1 : 0;
      flag = (num1 & num2) != 0;
    }
    return flag;
  }

  protected void SetErrorAndResetForMainFields(
    PXCache cache,
    object oldRow,
    object newRow,
    int? oldCalendarID,
    int? newCalendarID,
    bool externalCall,
    PXSetPropertyException exception)
  {
    cache.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, newRow, (object) PeriodIDAttribute.FormatForDisplay((string) cache.GetValue(newRow, ((PXEventSubscriberAttribute) this)._FieldName)), (Exception) exception);
    cache.SetValue(newRow, ((PXEventSubscriberAttribute) this)._FieldName, cache.GetValue(oldRow, ((PXEventSubscriberAttribute) this)._FieldName));
    int? nullable1 = oldCalendarID;
    int? nullable2 = newCalendarID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    if (this.MainSpecificationItem.BranchSourceType != (Type) null)
      this.SetErrorAndResetToOldForField(cache, oldRow, newRow, this.MainSpecificationItem.BranchSourceType.Name, exception, externalCall);
    if (!(this.MainSpecificationItem.OrganizationSourceType != (Type) null))
      return;
    this.SetErrorAndResetToOldForField(cache, oldRow, newRow, this.MainSpecificationItem.OrganizationSourceType.Name, exception, externalCall);
  }

  public static FinPeriodIDAttribute.ValidationResult ValidateRowLevelSources(
    ICalendarOrganizationIDProvider calendarOrganizationIDProvider,
    PXCache cache,
    object row,
    Func<int?, bool?> validationDelegate,
    bool skipMain = false)
  {
    FinPeriodIDAttribute.ValidationResult validationResult = new FinPeriodIDAttribute.ValidationResult();
    foreach (PX.Objects.GL.Descriptor.CalendarOrganizationIDProvider.KeyWithSourceValues withSourceValues in calendarOrganizationIDProvider.GetSourcesSpecification(cache, row).SpecificationItems.Where<PeriodKeyProviderBase.SourceSpecificationItem>((Func<PeriodKeyProviderBase.SourceSpecificationItem, bool>) (specification => !specification.IsMain || !skipMain)).Select<PeriodKeyProviderBase.SourceSpecificationItem, PX.Objects.GL.Descriptor.CalendarOrganizationIDProvider.KeyWithSourceValues>((Func<PeriodKeyProviderBase.SourceSpecificationItem, PX.Objects.GL.Descriptor.CalendarOrganizationIDProvider.KeyWithSourceValues>) (specification => calendarOrganizationIDProvider.GetBranchIDsValueFromField(cache.Graph, cache, row, specification))).ToList<PX.Objects.GL.Descriptor.CalendarOrganizationIDProvider.KeyWithSourceValues>())
    {
      bool flag1 = false;
      foreach (int? sourceBranchId in withSourceValues.SourceBranchIDs)
      {
        if (sourceBranchId.HasValue)
        {
          bool flag2 = false;
          int? parentOrganizationId = PXAccess.GetParentOrganizationID(sourceBranchId);
          if (!validationResult.OrganizationIDsWithErrors.Contains(parentOrganizationId))
          {
            bool? nullable = validationDelegate(parentOrganizationId);
            bool flag3 = false;
            if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
            {
              validationResult.OrganizationIDsWithErrors.Add(parentOrganizationId);
              flag2 = true;
              flag1 = true;
            }
          }
          else
          {
            flag2 = true;
            flag1 = true;
          }
          if (flag2 && !validationResult.BranchIDsWithErrors.Contains(sourceBranchId))
            validationResult.BranchIDsWithErrors.Add(sourceBranchId);
        }
      }
      if (flag1)
        validationResult.BranchValuesWithErrors.Add(withSourceValues);
    }
    return validationResult;
  }

  protected override string GetMappedPeriodID(
    PXCache cache,
    OrganizationDependedPeriodKey newPeriodKey,
    OrganizationDependedPeriodKey oldPeriodKey)
  {
    return this.FinPeriodRepository.GetMappedPeriod(oldPeriodKey.OrganizationID, oldPeriodKey.PeriodID, newPeriodKey.OrganizationID)?.FinPeriodID;
  }

  protected override void ValidatePeriodAndSourcesImpl(
    PXCache cache,
    object oldRow,
    object newRow,
    bool externalCall)
  {
    int? calendarOrganizationId1 = this.CalendarOrganizationIDProvider.GetCalendarOrganizationID(cache.Graph, cache, newRow);
    int? calendarOrganizationId2 = this.CalendarOrganizationIDProvider.GetCalendarOrganizationID(cache.Graph, cache, oldRow);
    if (this.HeaderMasterFinPeriodIDType != (Type) null && this.CalculatePeriodByHeader)
    {
      int? nullable1 = calendarOrganizationId1;
      int? nullable2 = calendarOrganizationId2;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        return;
      string masterFinPeriodId = this.GetHeaderMasterFinPeriodID(cache, newRow);
      if (masterFinPeriodId == null)
        return;
      ProcessingResult<FinPeriod> byMasterPeriodId = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(calendarOrganizationId1, masterFinPeriodId);
      if (byMasterPeriodId.IsSuccess)
        return;
      this.SetErrorAndResetToOldForField(cache, oldRow, newRow, this.CalendarOrganizationIDProvider.GetSourcesSpecification(cache, newRow).MainSpecificationItem.BranchSourceType.Name, new PXSetPropertyException(byMasterPeriodId.GetGeneralMessage()), externalCall);
    }
    else
    {
      FinPeriod validateMainFinPeriod = this.GetAndValidateMainFinPeriod(cache, oldRow, newRow, externalCall);
      if (validateMainFinPeriod == null)
        return;
      this.ValidateRelatedToMainFinPeriods(cache, oldRow, newRow, externalCall, calendarOrganizationId1, calendarOrganizationId2, validateMainFinPeriod);
      this.ValidateNotMainRowLevelSources(cache, oldRow, newRow, externalCall, validateMainFinPeriod);
    }
  }

  private void ValidateNotMainRowLevelSources(
    PXCache cache,
    object oldRow,
    object newRow,
    bool externalCall,
    FinPeriod newMainOrgFinPeriod)
  {
    FinPeriodIDAttribute.ValidationResult validationResult = FinPeriodIDAttribute.ValidateRowLevelSources(this.CalendarOrganizationIDProvider, cache, newRow, (Func<int?, bool?>) (organizationID => new bool?(this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(organizationID, newMainOrgFinPeriod.MasterFinPeriodID).IsSuccess)), true);
    if (!validationResult.HasErrors)
      return;
    foreach (PX.Objects.GL.Descriptor.CalendarOrganizationIDProvider.KeyWithSourceValues branchValuesWithError in validationResult.BranchValuesWithErrors)
    {
      if (branchValuesWithError.SpecificationItem.BranchSourceType != (Type) null && (branchValuesWithError.SpecificationItem.BranchSourceFormulaType != (Type) null || PXAccess.FeatureInstalled<FeaturesSet.branch>()))
      {
        PXCache cach = cache.Graph.Caches[BqlCommand.GetItemType(branchValuesWithError.SpecificationItem.BranchSourceType)];
        object newRow1 = cach.GetItemType().IsAssignableFrom(newRow.GetType()) ? newRow : cach.Current;
        object oldRow1 = cach.GetItemType().IsAssignableFrom(oldRow.GetType()) ? oldRow : cach.Current;
        string organizationCd = PXAccess.GetOrganizationCD(PXAccess.GetParentOrganizationID(branchValuesWithError.SourceBranchIDs.Single<int?>()));
        PXSetPropertyException exception = new PXSetPropertyException("The related financial period for the {0} master period does not exist for the {1} company.", new object[2]
        {
          (object) PeriodIDAttribute.FormatForError(newMainOrgFinPeriod.MasterFinPeriodID),
          (object) organizationCd
        });
        this.SetErrorAndResetToOldForField(cach, oldRow1, newRow1, branchValuesWithError.SpecificationItem.BranchSourceType.Name, exception, externalCall);
      }
    }
  }

  protected virtual FinPeriod GetAndValidateMainFinPeriod(
    PXCache cache,
    object oldRow,
    object newRow,
    bool externalCall)
  {
    int? calendarOrganizationId1 = this.CalendarOrganizationIDProvider.GetCalendarOrganizationID(cache.Graph, cache, newRow);
    int? calendarOrganizationId2 = this.CalendarOrganizationIDProvider.GetCalendarOrganizationID(cache.Graph, cache, oldRow);
    string str1 = (string) cache.GetValue(newRow, ((PXEventSubscriberAttribute) this)._FieldName);
    if (str1 == null)
      return (FinPeriod) null;
    FinPeriod byId = this.FinPeriodRepository.FindByID(calendarOrganizationId1, str1);
    if (byId != null)
      return byId;
    int? nullable = calendarOrganizationId1;
    int num = 0;
    string str2;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      str2 = PXMessages.LocalizeFormatNoPrefix("The {0} master financial period does not exist on the Master Financial Calendar (GL201000) form.", new object[1]
      {
        (object) PeriodIDAttribute.FormatForError(str1)
      });
    else
      str2 = PXMessages.LocalizeFormatNoPrefix("The {0} financial period does not exist for the {1} company.", new object[2]
      {
        (object) PeriodIDAttribute.FormatForError(str1),
        (object) PXAccess.GetOrganizationCD(calendarOrganizationId1)
      });
    this.SetErrorAndResetForMainFields(cache, oldRow, newRow, calendarOrganizationId2, calendarOrganizationId1, externalCall, new PXSetPropertyException(str2));
    return (FinPeriod) null;
  }

  private void ValidateRelatedToMainFinPeriods(
    PXCache cache,
    object oldRow,
    object newRow,
    bool externalCall,
    int? newMainCalendarOrgID,
    int? oldMainCalendarOrgID,
    FinPeriod newMainOrgFinPeriod)
  {
    string finPeriodID = (string) cache.GetValue(oldRow, ((PXEventSubscriberAttribute) this)._FieldName);
    FinPeriod byId = this.FinPeriodRepository.FindByID(oldMainCalendarOrgID, finPeriodID);
    if (!(newMainOrgFinPeriod.MasterFinPeriodID != byId?.MasterFinPeriodID))
      return;
    HashSet<int?> source = new HashSet<int?>();
    if (this.IsHeader)
      EnumerableExtensions.AddRange<int?>((ISet<int?>) source, (IEnumerable<int?>) this.CalendarOrganizationIDProvider.GetDetailOrganizationIDs(cache.Graph));
    if (!source.Any<int?>())
      return;
    ProcessingResult processingResult = this.FinPeriodRepository.FinPeriodsForMasterExist(newMainOrgFinPeriod.MasterFinPeriodID, source.ToArray<int?>());
    if (processingResult.IsSuccess)
      return;
    this.SetErrorAndResetForMainFields(cache, oldRow, newRow, oldMainCalendarOrgID, newMainCalendarOrgID, externalCall, new PXSetPropertyException(processingResult.GeneralMessage));
  }

  protected override void RedefaultPeriodID(PXCache cache, object row)
  {
    if (this.HeaderMasterFinPeriodIDType != (Type) null)
    {
      if (!this.CalculatePeriodByHeader)
        return;
      string str = (string) null;
      int? calendarOrganizationId = this.CalendarOrganizationIDProvider.GetCalendarOrganizationID(cache.Graph, cache, row);
      if (calendarOrganizationId.HasValue)
      {
        FinPeriod result = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(calendarOrganizationId, this.GetHeaderMasterFinPeriodID(cache, row)).Result;
        if (result != null)
          str = PeriodIDAttribute.FormatForDisplay(result.FinPeriodID);
      }
      cache.SetValueExt(row, ((PXEventSubscriberAttribute) this)._FieldName, (object) str);
    }
    else
    {
      base.RedefaultPeriodID(cache, row);
      if (!this.AutoCalculateMasterPeriod)
        return;
      this.SetMasterPeriodID(cache, row);
    }
  }

  protected override bool ShouldExecuteRedefaultFinPeriodIDonRowUpdated(
    object errorValue,
    bool hasError,
    OrganizationDependedPeriodKey newPeriodKey,
    OrganizationDependedPeriodKey oldPeriodKey)
  {
    FinPeriod.Key newPeriodKey1 = (FinPeriod.Key) newPeriodKey;
    FinPeriod.Key oldPeriodKey1 = (FinPeriod.Key) oldPeriodKey;
    if (base.ShouldExecuteRedefaultFinPeriodIDonRowUpdated(errorValue, hasError, (OrganizationDependedPeriodKey) newPeriodKey1, (OrganizationDependedPeriodKey) oldPeriodKey1))
      return true;
    if (!(this.HeaderMasterFinPeriodIDType != (Type) null))
      return false;
    int? organizationId1 = oldPeriodKey1.OrganizationID;
    int? organizationId2 = newPeriodKey1.OrganizationID;
    return !(organizationId1.GetValueOrDefault() == organizationId2.GetValueOrDefault() & organizationId1.HasValue == organizationId2.HasValue);
  }

  protected virtual void SetUseMasterCalendarValue(PXCache sender, object row)
  {
    if (this.UseMasterCalendarSourceType == (Type) null || (sender.GetStateExt(row, this.UseMasterCalendarSourceType.Name) is PXFieldState stateExt ? (stateExt.Visible ? 1 : 0) : 0) != 0)
      return;
    int? nullable = this.CalendarOrganizationIDProvider.GetBranchIDsValueFromField(sender.Graph, sender, row, this.CalendarOrganizationIDProvider.GetSourcesSpecification(sender, row).MainSpecificationItem).SourceBranchIDs.FirstOrDefault<int?>();
    int num;
    if (!nullable.HasValue)
    {
      nullable = this.CalendarOrganizationIDProvider.GetOrganizationIDsValueFromField(sender.Graph, sender, row, this.CalendarOrganizationIDProvider.GetSourcesSpecification(sender, row).MainSpecificationItem).SourceOrganizationIDs.FirstOrDefault<int?>();
      num = !nullable.HasValue ? 1 : 0;
    }
    else
      num = 0;
    bool flag = num != 0;
    sender.SetValueExt(row, this.UseMasterCalendarSourceType.Name, (object) flag);
  }

  public virtual string GetHeaderMasterFinPeriodID(PXCache cache, object row)
  {
    if (this.HeaderFindingMode != FinPeriodIDAttribute.HeaderFindingModes.Parent)
      return (string) BqlHelper.GetCurrentValue(cache.Graph, this.HeaderMasterFinPeriodIDType);
    object obj = PXParentAttribute.SelectParent(cache, row, BqlCommand.GetItemType(this.HeaderMasterFinPeriodIDType));
    return (string) BqlHelper.GetOperandValue(cache.Graph, obj, this.HeaderMasterFinPeriodIDType);
  }

  public virtual void SetMasterPeriodID(PXCache cache, object row)
  {
    if (this.MasterFinPeriodIDType == (Type) null)
      return;
    string str = (string) cache.GetValue(row, this.MasterFinPeriodIDType.Name);
    string period = this.CalcMasterPeriodID(cache, row);
    if (!(period != str))
      return;
    cache.SetValueExt(row, this.MasterFinPeriodIDType.Name, (object) PeriodIDAttribute.FormatForDisplay(period));
  }

  public virtual void DefaultPeriods(PXCache cache, object row)
  {
    cache.SetDefaultExt(row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
    if (!(this.MasterFinPeriodIDType != (Type) null))
      return;
    cache.SetDefaultExt(row, this.MasterFinPeriodIDType.Name, (object) null);
  }

  public static void SetMasterPeriodID<TField>(PXCache cache, object row) where TField : IBqlField
  {
    cache.GetAttributesReadonly<TField>(row).OfType<FinPeriodIDAttribute>().First<FinPeriodIDAttribute>().SetMasterPeriodID(cache, row);
  }

  public static void DefaultPeriods<TField>(PXCache cache, object row) where TField : IBqlField
  {
    cache.GetAttributesReadonly<TField>(row).OfType<FinPeriodIDAttribute>().First<FinPeriodIDAttribute>().DefaultPeriods(cache, row);
  }

  public virtual string CalcMasterPeriodID(PXCache cache, object row)
  {
    string finPeriodID = (string) cache.GetValue(row, ((PXEventSubscriberAttribute) this)._FieldName);
    int? calendarOrganizationId = this.CalendarOrganizationIDProvider.GetCalendarOrganizationID(cache.Graph, cache, row);
    int? nullable = calendarOrganizationId;
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return finPeriodID;
    string str = (string) null;
    if (finPeriodID != null)
      str = this.FinPeriodRepository.FindByID(calendarOrganizationId, finPeriodID)?.MasterFinPeriodID;
    return str;
  }

  public static string CalcMasterPeriodID<TField>(PXCache cache, object row) where TField : IBqlField
  {
    return cache.GetAttributesReadonly<TField>(row).OfType<FinPeriodIDAttribute>().First<FinPeriodIDAttribute>().CalcMasterPeriodID(cache, row);
  }

  public virtual void SetPeriodsByMaster(PXCache cache, object row, string masterFinPeriodID)
  {
    cache.SetValue(row, ((PXEventSubscriberAttribute) this)._FieldName, (object) this.CalcFinPeriodIDForMaster(cache, row, masterFinPeriodID));
    if (!(this.MasterFinPeriodIDType != (Type) null))
      return;
    cache.SetValue(row, this.MasterFinPeriodIDType.Name, (object) masterFinPeriodID);
  }

  public static void SetPeriodsByMaster<TField>(
    PXCache cache,
    object row,
    string masterFinPeriodID)
    where TField : IBqlField
  {
    cache.GetAttributesReadonly<TField>(row).OfType<FinPeriodIDAttribute>().First<FinPeriodIDAttribute>().SetPeriodsByMaster(cache, row, masterFinPeriodID);
  }

  public virtual string CalcFinPeriodIDForMaster(
    PXCache cache,
    object row,
    string masterFinPeriodID)
  {
    return this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(this.CalendarOrganizationIDProvider.GetCalendarOrganizationID(cache.Graph, cache, row), masterFinPeriodID).Result?.FinPeriodID;
  }

  public static string CalcFinPeriodIDForMaster<TField>(
    PXCache cache,
    object row,
    string masterFinPeriodID)
    where TField : IBqlField
  {
    return cache.GetAttributesReadonly<TField>(row).OfType<FinPeriodIDAttribute>().First<FinPeriodIDAttribute>().CalcFinPeriodIDForMaster(cache, row, masterFinPeriodID);
  }

  public class ValidationResult
  {
    public HashSet<int?> OrganizationIDsWithErrors;
    public HashSet<int?> BranchIDsWithErrors;
    public List<PX.Objects.GL.Descriptor.CalendarOrganizationIDProvider.KeyWithSourceValues> BranchValuesWithErrors;

    public bool HasErrors
    {
      get => this.BranchValuesWithErrors.Any<PX.Objects.GL.Descriptor.CalendarOrganizationIDProvider.KeyWithSourceValues>();
    }

    public ValidationResult()
    {
      this.OrganizationIDsWithErrors = new HashSet<int?>();
      this.BranchIDsWithErrors = new HashSet<int?>();
      this.BranchValuesWithErrors = new List<PX.Objects.GL.Descriptor.CalendarOrganizationIDProvider.KeyWithSourceValues>();
    }

    public List<string> GetOrganizationCDsWithErrors()
    {
      return this.OrganizationIDsWithErrors.Select<int?, string>(new Func<int?, string>(PXAccess.GetOrganizationCD)).ToList<string>();
    }

    public List<string> GetBranchCDsWithErrors()
    {
      return this.BranchIDsWithErrors.Select<int?, string>(new Func<int?, string>(PXAccess.GetBranchCD)).ToList<string>();
    }
  }

  public enum HeaderFindingModes
  {
    Current,
    Parent,
  }
}
