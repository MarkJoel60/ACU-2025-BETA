// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOpenPeriodAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.SO;

/// <summary>
/// Specialized version of the selector for SO Open Financial Periods.<br />
/// Displays a list of FinPeriods, having flags Active = true and  ARClosed = false and INClosed = false.<br />
/// </summary>
public class SOOpenPeriodAttribute : OpenPeriodAttribute
{
  public SOOpenPeriodAttribute(
    Type sourceType,
    Type branchSourceType,
    Type branchSourceFormulaType = null,
    Type organizationSourceType = null,
    Type useMasterCalendarSourceType = null,
    Type defaultType = null,
    bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
    bool useMasterOrganizationIDByDefault = false,
    Type masterFinPeriodIDType = null)
    : base(typeof (Search<FinPeriod.finPeriodID, Where2<Where<Current<SOInvoice.createINDoc>, Equal<False>, Or<FinPeriod.iNClosed, Equal<False>>>, And<FinPeriod.aRClosed, Equal<False>, And<Where<FinPeriod.status, Equal<FinPeriod.status.open>>>>>>), sourceType, branchSourceType, branchSourceFormulaType, organizationSourceType, useMasterCalendarSourceType, defaultType, redefaultOrRevalidateOnOrganizationSourceUpdated, useMasterOrganizationIDByDefault: useMasterOrganizationIDByDefault, masterFinPeriodIDType: masterFinPeriodIDType)
  {
  }

  public SOOpenPeriodAttribute(Type SourceType)
    : this(SourceType, (Type) null)
  {
  }

  public SOOpenPeriodAttribute()
    : this((Type) null)
  {
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (this._ValidatePeriod == PeriodValidation.Nothing)
      return;
    this.OpenPeriodVerifying(sender, e);
  }

  protected override OpenPeriodAttribute.PeriodValidationResult ValidateOrganizationFinPeriodStatus(
    PXCache sender,
    object row,
    FinPeriod finPeriod)
  {
    OpenPeriodAttribute.PeriodValidationResult validationResult = base.ValidateOrganizationFinPeriodStatus(sender, row, finPeriod);
    if (!validationResult.HasWarningOrError)
    {
      if (finPeriod.ARClosed.GetValueOrDefault())
        validationResult.Aggregate((ProcessingResultBase<OpenPeriodAttribute.PeriodValidationResult, object, OpenPeriodAttribute.PeriodValidationMessage>) this.HandleErrorThatPeriodIsClosed(sender, finPeriod, errorMessage: "The {0} financial period of the {1} company is closed in Accounts Receivable."));
      if (finPeriod.INClosed.GetValueOrDefault() && ((bool?) ((SOInvoice) ((PXCache) GraphHelper.Caches<SOInvoice>(sender.Graph)).Current)?.CreateINDoc).GetValueOrDefault())
        validationResult.Aggregate((ProcessingResultBase<OpenPeriodAttribute.PeriodValidationResult, object, OpenPeriodAttribute.PeriodValidationMessage>) this.HandleErrorThatPeriodIsClosed(sender, finPeriod, errorMessage: "The {0} financial period of the {1} company is closed in Inventory."));
    }
    return validationResult;
  }
}
