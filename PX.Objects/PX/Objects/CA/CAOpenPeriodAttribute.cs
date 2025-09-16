// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAOpenPeriodAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Scopes;
using PX.Objects.GL;
using PX.Objects.GL.Descriptor;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.CA;

/// <summary>
/// Specialized for CA version of the <see cref="!:OpenPeriodAttribut" /><br />
/// Selector. Provides  a list  of the active Fin. Periods, having CAClosed flag = false <br />
/// <example>
/// [CAOpenPeriod(typeof(CATran.tranDate))]
/// </example>
/// </summary>
public class CAOpenPeriodAttribute : OpenPeriodAttribute
{
  private Type cashAccountType { get; set; }

  /// <summary>Extended Ctor.</summary>
  /// <param name="sourceType">Must be IBqlField. Refers a date, based on which "current" period will be defined</param>
  public CAOpenPeriodAttribute(
    Type sourceType,
    Type branchSourceType,
    Type branchSourceFormulaType = null,
    Type organizationSourceType = null,
    Type useMasterCalendarSourceType = null,
    Type defaultType = null,
    bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
    Type masterFinPeriodIDType = null)
    : base(typeof (Search<FinPeriod.finPeriodID, Where<FinPeriod.cAClosed, Equal<False>, And<FinPeriod.status, Equal<FinPeriod.status.open>>>>), sourceType, branchSourceType, branchSourceFormulaType, organizationSourceType, useMasterCalendarSourceType, defaultType, redefaultOrRevalidateOnOrganizationSourceUpdated, masterFinPeriodIDType: masterFinPeriodIDType)
  {
    this.cashAccountType = branchSourceType;
  }

  public CAOpenPeriodAttribute()
    : this((Type) null, (Type) null)
  {
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    Type itemType = sender.GetItemType();
    string name = this.cashAccountType.Name;
    CAOpenPeriodAttribute openPeriodAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) openPeriodAttribute, __vmethodptr(openPeriodAttribute, CashAccountVerifying));
    fieldVerifying.AddHandler(itemType, name, pxFieldVerifying);
  }

  public virtual void CashAccountVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) != null && this.cashAccountType != (Type) null && e.NewValue == null)
      throw new PXSetPropertyException("The financial period cannot be specified because the cash account has not been selected in the Cash Account box.");
  }

  protected override OpenPeriodAttribute.PeriodValidationResult ValidateOrganizationFinPeriodStatus(
    PXCache sender,
    object row,
    FinPeriod finPeriod)
  {
    OpenPeriodAttribute.PeriodValidationResult validationResult = base.ValidateOrganizationFinPeriodStatus(sender, row, finPeriod);
    if (!validationResult.HasWarningOrError && finPeriod.CAClosed.GetValueOrDefault())
      validationResult = this.HandleErrorThatPeriodIsClosed(sender, finPeriod, errorMessage: "The {0} financial period of the {1} company is closed in Cash Management.");
    return validationResult;
  }

  protected override void ValidateFinPeriodID(PXCache sender, object row, string finPeriodID)
  {
    if (this.cashAccountType != (Type) null && sender.GetValue(row, this.cashAccountType.Name) == null)
      throw new PXSetPropertyException("The financial period cannot be specified because the cash account has not been selected in the Cash Account box.");
    base.ValidateFinPeriodID(sender, row, finPeriodID);
  }

  protected override void ValidateFinPeriodsStatus(
    PXCache sender,
    object row,
    int? calendarOrganizationID,
    string masterFinPeriodID,
    string finPeriodID,
    ICalendarOrganizationIDProvider calendarOrganizationIDProvider)
  {
    try
    {
      base.ValidateFinPeriodsStatus(sender, row, calendarOrganizationID, masterFinPeriodID, finPeriodID, calendarOrganizationIDProvider);
    }
    catch (PXSetPropertyException ex)
    {
      if (FlaggedModeScopeBase<SuppressThowFinPeriodExceptionForVoucherCAEntryScope>.IsActive)
        sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, row, (object) PeriodIDAttribute.FormatForDisplay(finPeriodID), (Exception) ex);
      else
        throw;
    }
    catch (Exception ex)
    {
      throw;
    }
  }
}
