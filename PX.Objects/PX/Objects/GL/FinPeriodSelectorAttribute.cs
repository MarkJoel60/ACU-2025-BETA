// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriodSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.Descriptor;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Selector for FinPeriod. Extends <see cref="T:PX.Objects.GL.FinPeriodIDAttribute" />.
/// Displays all available fin Periods.
/// </summary>
public class FinPeriodSelectorAttribute : FinPeriodIDAttribute, IPXFieldVerifyingSubscriber
{
  public Type OrigSelectorSearchType { get; private set; }

  public FinPeriodSelectorAttribute()
    : this((Type) null)
  {
  }

  public FinPeriodSelectorAttribute(Type sourceType)
    : this((Type) null, sourceType)
  {
  }

  public FinPeriodSelectorAttribute(
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
    Type[] fieldList = null,
    Type masterFinPeriodIDType = null,
    bool redefaultOnDateChanged = true)
  {
    Type masterFinPeriodIDType1 = masterFinPeriodIDType;
    // ISSUE: explicit constructor call
    base.\u002Ector(sourceType, branchSourceType, branchSourceFormulaType, organizationSourceType, useMasterCalendarSourceType, defaultType, redefaultOrRevalidateOnOrganizationSourceUpdated, useMasterOrganizationIDByDefault, sourceSpecificationTypes, masterFinPeriodIDType1, redefaultOnDateChanged: redefaultOnDateChanged);
    this.SelectionModeWithRestrictions = selectionModeWithRestrictions;
    Type[] fieldList1;
    if (fieldList != null && fieldList.Length != 0)
      fieldList1 = fieldList;
    else
      fieldList1 = new Type[4]
      {
        typeof (FinPeriod.finPeriodID),
        typeof (FinPeriod.descr),
        typeof (FinPeriod.startDateUI),
        typeof (FinPeriod.endDateUI)
      };
    this.OrigSelectorSearchType = searchType;
    if (this.OrigSelectorSearchType == (Type) null)
      this.OrigSelectorSearchType = this.GetDefaultSearchType();
    Type type = !(this.OrigSelectorSearchType == (Type) null) && typeof (IBqlSearch).IsAssignableFrom(this.OrigSelectorSearchType) ? this.OrigSelectorSearchType.GetGenericTypeDefinition() : throw new PXArgumentException("search", "An invalid argument has been specified.");
    Type[] genericArguments = this.OrigSelectorSearchType.GetGenericArguments();
    Type searchType1;
    if (type == typeof (Search<>))
      searchType1 = BqlCommand.Compose(new Type[3]
      {
        typeof (Search3<,>),
        typeof (FinPeriod.finPeriodID),
        typeof (OrderBy<Desc<FinPeriod.finPeriodID>>)
      });
    else if (type == typeof (Search<,>))
      searchType1 = BqlCommand.Compose(new Type[4]
      {
        typeof (Search<,,>),
        typeof (FinPeriod.finPeriodID),
        genericArguments[1],
        typeof (OrderBy<Asc<FinPeriod.finPeriodID>>)
      });
    else if (type == typeof (Search<,,>))
      searchType1 = this.OrigSelectorSearchType;
    else if (type == typeof (Search2<,>))
      searchType1 = BqlCommand.Compose(new Type[4]
      {
        typeof (Search3<,,>),
        typeof (FinPeriod.finPeriodID),
        genericArguments[1],
        typeof (OrderBy<Desc<FinPeriod.finPeriodID>>)
      });
    else if (type == typeof (Search2<,,>))
      searchType1 = BqlCommand.Compose(new Type[5]
      {
        typeof (Search2<,,,>),
        typeof (FinPeriod.finPeriodID),
        genericArguments[1],
        genericArguments[2],
        typeof (OrderBy<Asc<FinPeriod.finPeriodID>>)
      });
    else if (type == typeof (Search2<,,,>))
      searchType1 = this.OrigSelectorSearchType;
    else if (type == typeof (Search3<,>))
      searchType1 = this.OrigSelectorSearchType;
    else if (type == typeof (Search3<,,>))
      searchType1 = this.OrigSelectorSearchType;
    else if (type == typeof (Search5<,,>))
    {
      searchType1 = this.OrigSelectorSearchType;
    }
    else
    {
      if (!(type == typeof (Search5<,,,>)))
        throw new PXArgumentException("search", "An invalid argument has been specified.");
      searchType1 = this.OrigSelectorSearchType;
    }
    GenericFinPeriodSelectorAttribute selectorAttribute;
    if (!(genericArguments[0]?.DeclaringType == typeof (FinPeriod)))
    {
      selectorAttribute = (GenericFinPeriodSelectorAttribute) new PXSelectorAttribute(searchType1, fieldList1)
      {
        CustomMessageElementDoesntExist = "The financial period does not exist for the related branch or company."
      };
    }
    else
    {
      selectorAttribute = new GenericFinPeriodSelectorAttribute(searchType1, (Func<ICalendarOrganizationIDProvider>) (() => this.CalendarOrganizationIDProvider), takeBranchForSelectorFromQueryParams, takeOrganizationForSelectorFromQueryParams, masterPeriodBasedOnOrganizationPeriods, this.SelectionModeWithRestrictions, fieldList1);
      ((PXSelectorAttribute) selectorAttribute).CustomMessageElementDoesntExist = "The financial period does not exist for the related branch or company.";
    }
    this._Attributes.Add((PXEventSubscriberAttribute) selectorAttribute);
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXSelectorAttribute selector = this.Selector;
    selector.SelectorMode = (PXSelectorMode) (selector.SelectorMode | 1);
  }

  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    if (typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber))
      subscribers.Add(this as ISubscriber);
    else
      base.GetSubscriber<ISubscriber>(subscribers);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    try
    {
      ((List<PXEventSubscriberAttribute>) this._Attributes).ForEach((Action<PXEventSubscriberAttribute>) (_ =>
      {
        if (!(_ is IPXFieldVerifyingSubscriber verifyingSubscriber2))
          return;
        verifyingSubscriber2.FieldVerifying(sender, e);
      }));
    }
    catch (PXSetPropertyException ex)
    {
      e.NewValue = (object) PeriodIDAttribute.FormatForDisplay((string) e.NewValue);
      throw;
    }
  }

  protected virtual Type GetDefaultSearchType()
  {
    return typeof (Search3<FinPeriod.finPeriodID, OrderBy<Desc<FinPeriod.finPeriodID>>>);
  }

  protected PXSelectorAttribute Selector
  {
    get
    {
      return ((IEnumerable) this._Attributes).OfType<PXSelectorAttribute>().First<PXSelectorAttribute>();
    }
  }

  public Type DescriptionField
  {
    get => this.Selector.DescriptionField;
    set => this.Selector.DescriptionField = value;
  }

  public PXSelectorMode SelectorMode
  {
    get => this.Selector.SelectorMode;
    set => this.Selector.SelectorMode = value;
  }

  public FinPeriodSelectorAttribute.SelectionModesWithRestrictions SelectionModeWithRestrictions { get; set; }

  public enum SelectionModesWithRestrictions
  {
    Undefined,
    Any,
    All,
  }
}
