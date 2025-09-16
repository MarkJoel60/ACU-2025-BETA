// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.OpenPeriodAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions.Periods;
using PX.Objects.Common.Extensions;
using PX.Objects.GL.Descriptor;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// FinPeriod selector that extends <see cref="T:PX.Objects.GL.FinPeriodSelectorAttribute" />.
/// Displays and accepts only Open Fin Periods.
/// When Date is supplied through SourceType parameter FinPeriod is defaulted with the FinPeriod for the given date.
/// </summary>
[PXAttributeFamily(typeof (OpenPeriodAttribute))]
public class OpenPeriodAttribute : 
  FinPeriodSelectorAttribute,
  IPXFieldSelectingSubscriber,
  IPXRowSelectedSubscriber,
  IPXFieldUpdatedSubscriber
{
  protected PeriodValidation _ValidatePeriod;
  protected bool _throwErrorExternal;
  private bool dateSourceFieldAlreadyVerified;

  /// <summary>
  /// Gets or sets how the Period validation logic is handled.
  /// </summary>
  public PeriodValidation ValidatePeriod
  {
    get => this._ValidatePeriod;
    set => this._ValidatePeriod = value;
  }

  public bool ThrowErrorExternal
  {
    get => this._throwErrorExternal;
    set => this._throwErrorExternal = value;
  }

  protected BqlCommand GetSuitableFinPeriodCountInCompaniesCmd { get; set; }

  public OpenPeriodAttribute(Type SourceType)
    : this((Type) null, SourceType)
  {
  }

  public OpenPeriodAttribute()
    : this((Type) null)
  {
  }

  public OpenPeriodAttribute(
    Type searchType,
    Type sourceType,
    Type branchSourceType = null,
    Type branchSourceFormulaType = null,
    Type organizationSourceType = null,
    Type useMasterCalendarSourceType = null,
    Type defaultType = null,
    bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
    bool takeBranchForSelectorFromQueryParams = false,
    bool takeOrganizationForSelectorFromQueryParams = false,
    bool useMasterOrganizationIDByDefault = false,
    bool masterPeriodBasedOnOrganizationPeriods = true,
    FinPeriodSelectorAttribute.SelectionModesWithRestrictions selectionModeWithRestrictions = FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined,
    Type[] sourceSpecificationTypes = null,
    Type masterFinPeriodIDType = null,
    bool redefaultOnDateChanged = true)
  {
    Type searchType1 = searchType;
    Type sourceType1 = sourceType;
    Type type = masterFinPeriodIDType;
    Type branchSourceType1 = branchSourceType;
    Type branchSourceFormulaType1 = branchSourceFormulaType;
    Type organizationSourceType1 = organizationSourceType;
    Type useMasterCalendarSourceType1 = useMasterCalendarSourceType;
    Type defaultType1 = defaultType;
    int num1 = redefaultOrRevalidateOnOrganizationSourceUpdated ? 1 : 0;
    int num2 = takeBranchForSelectorFromQueryParams ? 1 : 0;
    int num3 = takeOrganizationForSelectorFromQueryParams ? 1 : 0;
    int num4 = useMasterOrganizationIDByDefault ? 1 : 0;
    int num5 = masterPeriodBasedOnOrganizationPeriods ? 1 : 0;
    int selectionModeWithRestrictions1 = (int) selectionModeWithRestrictions;
    Type[] sourceSpecificationTypes1 = sourceSpecificationTypes;
    Type masterFinPeriodIDType1 = type;
    int num6 = redefaultOnDateChanged ? 1 : 0;
    // ISSUE: explicit constructor call
    base.\u002Ector(searchType1, sourceType1, branchSourceType1, branchSourceFormulaType1, organizationSourceType1, useMasterCalendarSourceType1, defaultType1, num1 != 0, num2 != 0, num3 != 0, num4 != 0, num5 != 0, (FinPeriodSelectorAttribute.SelectionModesWithRestrictions) selectionModeWithRestrictions1, sourceSpecificationTypes1, masterFinPeriodIDType: masterFinPeriodIDType1, redefaultOnDateChanged: num6 != 0);
    this.GetSuitableFinPeriodCountInCompaniesCmd = this.GenerateGetSuitableFinPeriodCountInCompaniesCmd(this.OrigSelectorSearchType);
  }

  protected virtual int? GetSuitableFinPeriodCountInCompanies(
    PXGraph graph,
    int?[] organizationIDs,
    string masterFinPeriodID)
  {
    List<object> source = new PXView(graph, true, this.GetSuitableFinPeriodCountInCompaniesCmd).SelectMulti(new object[2]
    {
      (object) organizationIDs,
      (object) masterFinPeriodID
    });
    return source != null && source.Any<object>() ? new int?(((PXResult) source[0]).RowCount.GetValueOrDefault()) : new int?(0);
  }

  protected virtual BqlCommand GenerateGetSuitableFinPeriodCountInCompaniesCmd(Type queryType)
  {
    return BqlCommand.CreateInstance(new Type[1]
    {
      queryType
    }).WhereAnd<Where<FinPeriod.organizationID, In<Required<FinPeriod.organizationID>>, And<FinPeriod.masterFinPeriodID, Equal<Required<FinPeriod.masterFinPeriodID>>>>>().AggregateNew<Aggregate<GroupBy<FinPeriod.masterFinPeriodID, Count<FinPeriod.organizationID>>>>();
  }

  public static void SetValidatePeriod<Field>(
    PXCache cache,
    object data,
    PeriodValidation isValidatePeriod)
    where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is OpenPeriodAttribute)
        ((OpenPeriodAttribute) attribute).ValidatePeriod = isValidatePeriod;
    }
  }

  public static void SetValidatePeriod(
    PXCache cache,
    object data,
    string name,
    PeriodValidation isValidatePeriod)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, name))
    {
      if (attribute is OpenPeriodAttribute)
        ((OpenPeriodAttribute) attribute).ValidatePeriod = isValidatePeriod;
    }
  }

  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    if (typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber) || typeof (ISubscriber) == typeof (IPXFieldDefaultingSubscriber) || typeof (ISubscriber) == typeof (IPXFieldSelectingSubscriber))
      subscribers.Add(this as ISubscriber);
    else
      base.GetSubscriber<ISubscriber>(subscribers);
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(this.SourceFieldType != (Type) null))
      return;
    Type itemType = BqlCommand.GetItemType(this.SourceFieldType);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    Type type = itemType;
    string name = this.SourceFieldType.Name;
    OpenPeriodAttribute openPeriodAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) openPeriodAttribute, __vmethodptr(openPeriodAttribute, PeriodSourceDateFieldVerifying));
    fieldVerifying.AddHandler(type, name, pxFieldVerifying);
    // ISSUE: method pointer
    sender.Graph.FieldUpdating.AddHandler(itemType, this.SourceFieldType.Name, new PXFieldUpdating((object) this, __methodptr(\u003CCacheAttached\u003Eb__20_0)));
    // ISSUE: method pointer
    sender.Graph.FieldDefaulting.AddHandler(itemType, this.SourceFieldType.Name, new PXFieldDefaulting((object) this, __methodptr(\u003CCacheAttached\u003Eb__20_1)));
  }

  protected virtual void PeriodSourceDateFieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    if (this.ValidatePeriod == PeriodValidation.Nothing || e.Row == null)
      return;
    DateTime? newValue = e.NewValue as DateTime?;
    if (!newValue.HasValue)
      return;
    if (cache.Graph.GetService<IFinPeriodRepository>().FindFinPeriodByDate(newValue, new int?(0)) == null)
      throw new PXSetPropertyException("The financial period that corresponds to the {0} date does not exist in the master financial calendar.", new object[1]
      {
        (object) newValue.Value.ToShortDateString()
      });
    if (!(cache.GetStateExt(e.Row, this.SourceFieldType.Name) is PXFieldState stateExt) || stateExt.ErrorLevel == 2)
      return;
    cache.RaiseExceptionHandling(this.SourceFieldType.Name, e.Row, (object) newValue, (Exception) null);
  }

  protected override Type GetDefaultSearchType()
  {
    return typeof (Search<FinPeriod.finPeriodID, Where<FinPeriod.status, Equal<FinPeriod.status.open>>>);
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (this._ValidatePeriod != PeriodValidation.Nothing)
      this.OpenPeriodVerifying(sender, e);
    else
      base.FieldVerifying(sender, e);
  }

  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this._ValidatePeriod != PeriodValidation.Nothing)
      this.OpenPeriodDefaulting(sender, e);
    else
      base.FieldDefaulting(sender, e);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    ((IPXFieldSelectingSubscriber) this._Attributes[1]).FieldSelecting(sender, e);
    ((IPXFieldSelectingSubscriber) this._Attributes[2]).FieldSelecting(sender, e);
    if (e.ReturnState == null || !(e.ReturnState is PXStringState))
      return;
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(((PXFieldState) e.ReturnState).Length), new bool?(), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(), new int?(1), ((PXStringState) e.ReturnState).InputMask, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
  }

  public virtual void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    object valueExt = sender.GetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (valueExt is PXFieldState)
      valueExt = ((PXFieldState) valueExt).Value;
    string str = PeriodIDAttribute.UnFormatPeriod((string) valueExt);
    if (str == null || str.Equals(sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName)))
      return;
    PXUIFieldAttribute.SetError(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (string) null, (string) null);
  }

  public virtual void OpenPeriodDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    base.FieldDefaulting(sender, e);
    if (e.NewValue == null)
      return;
    try
    {
      this.IsValidPeriod(sender, e.Row, (object) PeriodIDAttribute.UnFormatPeriod((string) e.NewValue));
    }
    catch (PXSetPropertyException ex)
    {
      if (this.ThrowErrorExternal)
        throw ex;
      if (e.Row != null)
        sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, e.NewValue, (Exception) ex);
      e.NewValue = (object) null;
    }
  }

  public static void SetThrowErrorExternal<Field>(PXCache cache, bool throwErrorExternal) where Field : IBqlField
  {
    EnumerableExtensions.ForEach<AROpenPeriodAttribute>(cache.GetAttributes<Field>().OfType<AROpenPeriodAttribute>(), (Action<AROpenPeriodAttribute>) (attr => attr.ThrowErrorExternal = throwErrorExternal));
  }

  public virtual void OpenPeriodVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    try
    {
      this.IsValidPeriod(sender, e.Row, e.NewValue);
    }
    catch (PXSetPropertyException ex)
    {
      e.NewValue = (object) PeriodIDAttribute.FormatForDisplay((string) e.NewValue);
      throw;
    }
    this.IsCurrentPeriod(sender, e.Row, e.NewValue);
  }

  protected virtual void IsCurrentPeriod(PXCache sender, object row, object value)
  {
    this.GetFields(sender, row);
    if (this._ValidatePeriod == PeriodValidation.Nothing || !(this._sourceDate != DateTime.MinValue))
      return;
    int? calendarOrganizationId = this.CalendarOrganizationIDProvider.GetCalendarOrganizationID(sender.Graph, sender, row);
    PXGraph graph = sender.Graph;
    Type searchType = this.SearchTypeRestrictedByOrganization;
    if ((object) searchType == null)
      searchType = this.SearchType;
    DateTime? date = new DateTime?(this._sourceDate);
    FinPeriod.Key periodKey = new FinPeriod.Key();
    periodKey.OrganizationID = calendarOrganizationId;
    List<object> listOrNull = row.SingleToListOrNull<object>();
    string periodId = this.GetPeriod(graph, searchType, date, (OrganizationDependedPeriodKey) periodKey, listOrNull).PeriodID;
    string objB = (string) value;
    if (object.Equals((object) periodId, (object) objB))
      return;
    PXResult<FinPeriod, PX.Objects.GL.FinPeriods.TableDefinition.FinYear> pxResult = ((IEnumerable<PXResult<FinPeriod>>) PXSelectBase<FinPeriod, PXSelectJoin<FinPeriod, InnerJoin<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, On<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, Equal<FinPeriod.finYear>, And<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<FinPeriod.organizationID>>>>, Where<FinPeriod.finPeriodID, Equal<Required<FinPeriod.finPeriodID>>>, OrderBy<Asc<FinPeriod.finPeriodID>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, new object[1]
    {
      (object) objB
    })).AsEnumerable<PXResult<FinPeriod>>().Cast<PXResult<FinPeriod, PX.Objects.GL.FinPeriods.TableDefinition.FinYear>>().SingleOrDefault<PXResult<FinPeriod, PX.Objects.GL.FinPeriods.TableDefinition.FinYear>>();
    FinPeriod finPeriod = PXResult<FinPeriod, PX.Objects.GL.FinPeriods.TableDefinition.FinYear>.op_Implicit(pxResult);
    PXResult<FinPeriod, PX.Objects.GL.FinPeriods.TableDefinition.FinYear>.op_Implicit(pxResult);
    if (finPeriod != null)
    {
      DateTime? startDate = finPeriod.StartDate;
      DateTime? endDate = finPeriod.EndDate;
      if ((startDate.HasValue == endDate.HasValue ? (startDate.HasValue ? (startDate.GetValueOrDefault() == endDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && FinPeriodUtils.FiscalYear(periodId) == finPeriod.FinYear)
        return;
    }
    if (PXUIFieldAttribute.GetError(sender, row, ((PXEventSubscriberAttribute) this)._FieldName) != null)
      return;
    PXUIFieldAttribute.SetWarning(sender, row, ((PXEventSubscriberAttribute) this)._FieldName, "The date is outside the range of the selected financial period.");
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(sender.Graph.GetType() != typeof (PXGraph)) || !(sender.Graph.GetType() != typeof (PXGenericInqGrph)) || sender.Graph.IsImport)
      return;
    if (((PXEventSubscriberAttribute) this)._AttributeLevel == 2)
    {
      ((PXEventSubscriberAttribute) this).IsDirty = true;
      this._Attributes[1].IsDirty = true;
    }
    PXGraph.RowSelectedEvents rowSelected = sender.Graph.RowSelected;
    Type itemType = sender.GetItemType();
    OpenPeriodAttribute openPeriodAttribute = this;
    // ISSUE: virtual method pointer
    // ISSUE: method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) new OpenPeriodAttribute.DynamicRowSelected(new PXRowSelected((object) openPeriodAttribute, __vmethodptr(openPeriodAttribute, Document_RowSelected))), __methodptr(RowSelected));
    rowSelected.AddHandler(itemType, pxRowSelected);
  }

  private PeriodValidation GetValidatePeriod(PXCache cache, object Row)
  {
    using (IEnumerator<OpenPeriodAttribute> enumerator = cache.GetAttributesReadonly(Row, ((PXEventSubscriberAttribute) this)._FieldName).OfType<OpenPeriodAttribute>().GetEnumerator())
    {
      if (enumerator.MoveNext())
        return enumerator.Current._ValidatePeriod;
    }
    return PeriodValidation.Nothing;
  }

  public virtual void Document_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this.GetValidatePeriod(sender, e.Row) != PeriodValidation.DefaultSelectUpdate)
      return;
    this.RiseFieldVerifyingForDocument_RowSelected(sender, e);
    object valueExt = sender.GetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (valueExt is PXFieldState)
      valueExt = ((PXFieldState) valueExt).Value;
    string str = PeriodIDAttribute.UnFormatPeriod(valueExt as string);
    try
    {
      if (!sender.AllowDelete || string.IsNullOrEmpty(str))
        return;
      this.IsValidPeriod(sender, e.Row, (object) str);
      this.IsCurrentPeriod(sender, e.Row, (object) str);
    }
    catch (PXSetPropertyException ex)
    {
      sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) PeriodIDAttribute.FormatForDisplay(str), (Exception) ex);
    }
  }

  private void RiseFieldVerifyingForDocument_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this.SourceFieldType == (Type) null || this.dateSourceFieldAlreadyVerified || !(sender.GetStateExt(e.Row, this.SourceFieldType.Name) is PXFieldState stateExt) || stateExt.Value == null || stateExt.IsReadOnly || stateExt.ErrorLevel != null)
      return;
    object obj = stateExt.Value;
    try
    {
      sender.RaiseFieldVerifying(this.SourceFieldType.Name, e.Row, ref obj);
    }
    catch (PXSetPropertyException ex)
    {
      sender.RaiseExceptionHandling(this.SourceFieldType.Name, e.Row, obj, (Exception) ex);
    }
    this.dateSourceFieldAlreadyVerified = true;
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    base.RowPersisting(sender, e);
    string str = (string) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    try
    {
      if (((e.Operation & 3) != 2 ? 0 : (this._ValidatePeriod != 0 ? 1 : 0)) == 0 && ((e.Operation & 3) != 1 || !sender.AllowDelete || this._ValidatePeriod != PeriodValidation.DefaultSelectUpdate))
        return;
      object original = sender.GetOriginal(e.Row);
      OrganizationDependedPeriodKey fullKey1 = this.GetFullKey(sender, original);
      OrganizationDependedPeriodKey fullKey2 = this.GetFullKey(sender, e.Row);
      int? organizationId1 = fullKey1.OrganizationID;
      int? organizationId2 = fullKey2.OrganizationID;
      if (organizationId1.GetValueOrDefault() == organizationId2.GetValueOrDefault() & organizationId1.HasValue == organizationId2.HasValue && !(fullKey1.PeriodID != fullKey2.PeriodID))
        return;
      this.IsValidPeriod(sender, e.Row, (object) str);
      this.GetFields(sender, e.Row);
      if (!(this._sourceDate != DateTime.MinValue) || !string.IsNullOrEmpty(str))
        return;
      if (sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) null, (Exception) new PXSetPropertyKeepPreviousException(PXMessages.LocalizeFormat("'{0}' cannot be empty.", new object[1]
      {
        (object) $"[{((PXEventSubscriberAttribute) this)._FieldName}]"
      }))))
        throw new PXRowPersistingException(((PXEventSubscriberAttribute) this)._FieldName, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) ((PXEventSubscriberAttribute) this)._FieldName
        });
    }
    catch (PXSetPropertyException ex)
    {
      if (sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) PeriodIDAttribute.FormatForDisplay(str), (Exception) ex))
        throw new PXRowPersistingException(((PXEventSubscriberAttribute) this)._FieldName, (object) PeriodIDAttribute.FormatForDisplay(str), ((Exception) ex).Message);
    }
  }

  public virtual void IsValidPeriod(PXCache sender, object Row, object NewValue)
  {
    if (NewValue == null || this._ValidatePeriod == PeriodValidation.Nothing)
      return;
    this.ValidateFinPeriodID(sender, Row, (string) NewValue);
  }

  protected virtual void ValidateFinPeriodID(PXCache sender, object row, string finPeriodID)
  {
    int? calendarOrganizationId = this.CalendarOrganizationIDProvider.GetCalendarOrganizationID(sender.Graph, sender, row);
    if (!calendarOrganizationId.HasValue)
      throw new PXSetPropertyException("The financial period cannot be specified because the branch has not been specified in the Branch box.");
    if (!ServiceLocator.IsLocationProviderSet)
      return;
    FinPeriod byId = sender.Graph.GetService<IFinPeriodRepository>().FindByID(calendarOrganizationId, finPeriodID);
    string masterFinPeriodID = finPeriodID;
    int? nullable = calendarOrganizationId;
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
    {
      if (byId == null)
        throw new PXSetPropertyException("The {0} master financial period does not exist on the Master Financial Calendar (GL201000) form.", new object[1]
        {
          (object) PeriodIDAttribute.FormatForError(finPeriodID)
        });
    }
    else
      masterFinPeriodID = byId != null ? byId.MasterFinPeriodID : throw new PXSetPropertyException("The {0} financial period does not exist for the {1} company.", new object[2]
      {
        (object) PeriodIDAttribute.FormatForError(finPeriodID),
        (object) PXAccess.GetOrganizationCD(calendarOrganizationId)
      });
    this.ValidateFinPeriodsStatus(sender, row, calendarOrganizationId, masterFinPeriodID, finPeriodID, this.CalendarOrganizationIDProvider);
  }

  protected virtual void ValidateFinPeriodsStatus(
    PXCache sender,
    object row,
    int? calendarOrganizationID,
    string masterFinPeriodID,
    string finPeriodID,
    ICalendarOrganizationIDProvider calendarOrganizationIDProvider)
  {
    int?[] array = calendarOrganizationIDProvider.GetKeysWithBasisOrganizationIDs(sender.Graph, sender, row).ConsolidatedOrganizationIDs.ToArray();
    if (this.SelectionModeWithRestrictions != FinPeriodSelectorAttribute.SelectionModesWithRestrictions.All)
    {
      if (this.SelectionModeWithRestrictions == FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined)
      {
        int? nullable = calendarOrganizationID;
        int num = 0;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
          goto label_3;
      }
      this.ValidateFinPeriodsStatusForAny(sender, row, masterFinPeriodID, array);
      return;
    }
label_3:
    this.ValidateFinPeriodsStatusForAll(sender, row, calendarOrganizationID, masterFinPeriodID, finPeriodID, array);
  }

  private void ValidateFinPeriodsStatusForAny(
    PXCache sender,
    object row,
    string masterFinPeriodID,
    int?[] organizationIDs)
  {
    int? countInCompanies = this.GetSuitableFinPeriodCountInCompanies(sender.Graph, organizationIDs, masterFinPeriodID);
    int? nullable1;
    if (sender.Graph.GetService<IFinPeriodUtils>().CanPostToClosedPeriod())
      nullable1 = PXSelectBase<OrganizationFinPeriod, PXSelectGroupBy<OrganizationFinPeriod, Where<OrganizationFinPeriod.status, NotEqual<FinPeriod.status.locked>, And<OrganizationFinPeriod.status, NotEqual<FinPeriod.status.inactive>, And<OrganizationFinPeriod.organizationID, In<Required<OrganizationFinPeriod.organizationID>>, And<OrganizationFinPeriod.masterFinPeriodID, Equal<Required<OrganizationFinPeriod.masterFinPeriodID>>>>>>, Aggregate<GroupBy<OrganizationFinPeriod.masterFinPeriodID, Count<OrganizationFinPeriod.organizationID>>>>.Config>.Select(sender.Graph, new object[2]
      {
        (object) organizationIDs,
        (object) masterFinPeriodID
      }).RowCount;
    else
      nullable1 = countInCompanies;
    int? nullable2 = nullable1;
    int num = 0;
    if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
      throw new PXSetPropertyException("The {0} financial period cannot be used for processing.", new object[1]
      {
        (object) PeriodIDAttribute.FormatForError(masterFinPeriodID)
      });
    ProcessingResult processingResult = new ProcessingResult();
    int? nullable3 = nullable1;
    int length1 = organizationIDs.Length;
    if (!(nullable3.GetValueOrDefault() == length1 & nullable3.HasValue))
      processingResult.AddMessage((PXErrorLevel) 2, "The {0} financial period cannot be used for processing for at least one company.", (object) PeriodIDAttribute.FormatForError(masterFinPeriodID));
    nullable3 = nullable1;
    int? nullable4 = countInCompanies;
    if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
    {
      nullable4 = countInCompanies;
      int length2 = organizationIDs.Length;
      if (!(nullable4.GetValueOrDefault() == length2 & nullable4.HasValue))
        processingResult.AddMessage((PXErrorLevel) 2, "The {0} financial period is closed in at least one company.", (object) PeriodIDAttribute.FormatForError(masterFinPeriodID));
    }
    if (!processingResult.HasWarningOrError || PXUIFieldAttribute.GetError(sender, row, ((PXEventSubscriberAttribute) this)._FieldName) != null)
      return;
    PXUIFieldAttribute.SetWarning(sender, row, ((PXEventSubscriberAttribute) this)._FieldName, processingResult.GeneralMessage);
  }

  private void ValidateFinPeriodsStatusForAll(
    PXCache sender,
    object row,
    int? calendarOrganizationID,
    string masterFinPeriodID,
    string finPeriodID,
    int?[] organizationIDs)
  {
    List<FinPeriod> list = GraphHelper.RowCast<FinPeriod>((IEnumerable) PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.masterFinPeriodID, Equal<Required<FinPeriod.masterFinPeriodID>>, And<FinPeriod.organizationID, In<Required<FinPeriod.organizationID>>>>>.Config>.Select(sender.Graph, new object[2]
    {
      (object) masterFinPeriodID,
      (object) organizationIDs
    })).ToList<FinPeriod>();
    OpenPeriodAttribute.PeriodValidationResult validationResult = new OpenPeriodAttribute.PeriodValidationResult();
    if (list.Count != organizationIDs.Length)
    {
      IEnumerable<int?> source = ((IEnumerable<int?>) organizationIDs).Except<int?>(list.Select<FinPeriod, int?>((Func<FinPeriod, int?>) (period => period.OrganizationID)));
      validationResult.AddMessage((PXErrorLevel) 4, OpenPeriodAttribute.ExceptionType.Custom, "The financial period corresponding to the {0} period in the calendar of the {1} company does not exist for the following companies: {2}.", (object) FinPeriodIDFormattingAttribute.FormatForError(finPeriodID), (object) PXAccess.GetOrganizationCD(calendarOrganizationID), (object) ((ICollection<string>) source.Select<int?, string>(new Func<int?, string>(PXAccess.GetOrganizationCD)).ToArray<string>()).JoinIntoStringForMessageNoQuotes<string>(5));
    }
    foreach (FinPeriod finPeriod in list)
    {
      OpenPeriodAttribute.PeriodValidationResult processingResult = this.ValidateOrganizationFinPeriodStatus(sender, row, finPeriod);
      validationResult.Aggregate((ProcessingResultBase<OpenPeriodAttribute.PeriodValidationResult, object, OpenPeriodAttribute.PeriodValidationMessage>) processingResult);
    }
    if (!validationResult.HasWarningOrError)
      return;
    OpenPeriodAttribute.ExceptionType exceptionType = validationResult.Messages.Max<OpenPeriodAttribute.PeriodValidationMessage, OpenPeriodAttribute.ExceptionType>((Func<OpenPeriodAttribute.PeriodValidationMessage, OpenPeriodAttribute.ExceptionType>) (mes => mes.ExceptionType));
    PXErrorLevel maxErrorLevel = validationResult.MaxErrorLevel;
    PXSetPropertyException propertyException1;
    switch (exceptionType)
    {
      case OpenPeriodAttribute.ExceptionType.Closed:
        propertyException1 = (PXSetPropertyException) new FiscalPeriodClosedException(validationResult.GeneralMessage, maxErrorLevel);
        break;
      case OpenPeriodAttribute.ExceptionType.Inactive:
        propertyException1 = (PXSetPropertyException) new FiscalPeriodInactiveException(validationResult.GeneralMessage, maxErrorLevel);
        break;
      case OpenPeriodAttribute.ExceptionType.Locked:
        propertyException1 = (PXSetPropertyException) new FiscalPeriodLockedException(validationResult.GeneralMessage, maxErrorLevel);
        break;
      default:
        propertyException1 = new PXSetPropertyException(validationResult.GeneralMessage, maxErrorLevel);
        break;
    }
    PXSetPropertyException propertyException2 = propertyException1;
    if (validationResult.MaxErrorLevel <= 2 || sender.GetStatus(row) == null)
    {
      sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, row, (object) PeriodIDAttribute.FormatForDisplay(finPeriodID), (Exception) propertyException2);
    }
    else
    {
      propertyException2.ErrorValue = (object) PeriodIDAttribute.FormatForDisplay(finPeriodID);
      throw propertyException2;
    }
  }

  protected virtual OpenPeriodAttribute.PeriodValidationResult ValidateOrganizationFinPeriodStatus(
    PXCache sender,
    object row,
    FinPeriod finPeriod)
  {
    OpenPeriodAttribute.PeriodValidationResult validationResult = new OpenPeriodAttribute.PeriodValidationResult();
    if (finPeriod.Status == "Locked")
    {
      validationResult.AddMessage((PXErrorLevel) 4, OpenPeriodAttribute.ExceptionType.Locked, "The {0} financial period is locked in the {1} company.", (object) PeriodIDAttribute.FormatForError(finPeriod.FinPeriodID), (object) PXAccess.GetOrganizationCD(finPeriod.OrganizationID));
      return validationResult;
    }
    if (finPeriod.Status == "Inactive")
    {
      validationResult.AddMessage((PXErrorLevel) 4, OpenPeriodAttribute.ExceptionType.Inactive, "The {0} financial period is inactive in the {1} company.", (object) PeriodIDAttribute.FormatForError(finPeriod.FinPeriodID), (object) PXAccess.GetOrganizationCD(finPeriod.OrganizationID));
      return validationResult;
    }
    return finPeriod.Status == "Closed" ? this.HandleErrorThatPeriodIsClosed(sender, finPeriod) : validationResult;
  }

  protected OpenPeriodAttribute.PeriodValidationResult HandleErrorThatPeriodIsClosed(
    PXCache sender,
    FinPeriod finPeriod,
    PXErrorLevel errorLevel = 4,
    string errorMessage = "The {0} financial period is closed in the {1} company.")
  {
    OpenPeriodAttribute.PeriodValidationResult validationResult = new OpenPeriodAttribute.PeriodValidationResult();
    validationResult.AddMessage((PXErrorLevel) (sender.Graph.GetService<IFinPeriodUtils>().CanPostToClosedPeriod() ? (object) 2 : (object) errorLevel), OpenPeriodAttribute.ExceptionType.Closed, errorMessage, (object) PeriodIDAttribute.FormatForError(finPeriod.FinPeriodID), (object) PXAccess.GetOrganizationCD(finPeriod.OrganizationID));
    return validationResult;
  }

  private class DynamicRowSelected
  {
    private PXRowSelected _del;

    public DynamicRowSelected(PXRowSelected del) => this._del = del;

    public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      try
      {
        this._del.Invoke(sender, e);
      }
      finally
      {
        // ISSUE: method pointer
        sender.Graph.RowSelected.RemoveHandler(sender.GetItemType(), new PXRowSelected((object) this, __methodptr(RowSelected)));
      }
    }
  }

  protected enum ExceptionType
  {
    Custom,
    Closed,
    Inactive,
    Locked,
  }

  protected class PeriodValidationMessage : ProcessingResultMessage
  {
    public OpenPeriodAttribute.ExceptionType ExceptionType { get; set; }

    public PeriodValidationMessage(
      PXErrorLevel errorLevel,
      OpenPeriodAttribute.ExceptionType exceptionType,
      string text)
      : base(errorLevel, text)
    {
      this.ExceptionType = exceptionType;
    }
  }

  protected class PeriodValidationResult : 
    ProcessingResultBase<OpenPeriodAttribute.PeriodValidationResult, object, OpenPeriodAttribute.PeriodValidationMessage>
  {
    public void AddMessage(
      PXErrorLevel errorLevel,
      OpenPeriodAttribute.ExceptionType exceptionType,
      string message,
      params object[] args)
    {
      this._messages.Add(new OpenPeriodAttribute.PeriodValidationMessage(errorLevel, exceptionType, PXMessages.LocalizeFormatNoPrefix(message, args)));
    }

    public void AddMessage(
      PXErrorLevel errorLevel,
      OpenPeriodAttribute.ExceptionType exceptionType,
      string message)
    {
      this._messages.Add(new OpenPeriodAttribute.PeriodValidationMessage(errorLevel, exceptionType, PXMessages.LocalizeNoPrefix(message)));
    }

    public override void AddMessage(PXErrorLevel errorLevel, string message, params object[] args)
    {
      this.AddMessage(errorLevel, OpenPeriodAttribute.ExceptionType.Custom, message, args);
    }

    public override void AddMessage(PXErrorLevel errorLevel, string message)
    {
      this.AddMessage(errorLevel, OpenPeriodAttribute.ExceptionType.Custom, message);
    }
  }
}
