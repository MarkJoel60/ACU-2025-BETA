// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOpenPeriodAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.PO;

public class POOpenPeriodAttribute : OpenPeriodAttribute
{
  public POOpenPeriodAttribute(
    Type sourceType,
    Type branchSourceType,
    Type branchSourceFormulaType = null,
    Type organizationSourceType = null,
    Type useMasterCalendarSourceType = null,
    Type defaultType = null,
    bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
    bool useMasterOrganizationIDByDefault = false,
    Type masterFinPeriodIDType = null)
    : base(typeof (Search<FinPeriod.finPeriodID, Where<FinPeriod.status, Equal<FinPeriod.status.open>, And<FinPeriod.aPClosed, Equal<False>, And<Where<FinPeriod.iNClosed, Equal<False>, Or<Not<FeatureInstalled<FeaturesSet.inventory>>>>>>>>), sourceType, branchSourceType, branchSourceFormulaType, organizationSourceType, useMasterCalendarSourceType, defaultType, redefaultOrRevalidateOnOrganizationSourceUpdated, useMasterOrganizationIDByDefault: useMasterOrganizationIDByDefault, masterFinPeriodIDType: masterFinPeriodIDType)
  {
  }

  public POOpenPeriodAttribute(Type SourceType)
    : this(SourceType, (Type) null)
  {
  }

  public POOpenPeriodAttribute()
    : this((Type) null)
  {
  }

  protected override OpenPeriodAttribute.PeriodValidationResult ValidateOrganizationFinPeriodStatus(
    PXCache sender,
    object row,
    FinPeriod finPeriod)
  {
    OpenPeriodAttribute.PeriodValidationResult validationResult = base.ValidateOrganizationFinPeriodStatus(sender, row, finPeriod);
    if (!validationResult.HasWarningOrError)
    {
      if (finPeriod.APClosed.GetValueOrDefault())
        validationResult = this.HandleErrorThatPeriodIsClosed(sender, finPeriod, errorMessage: "The {0} financial period of the {1} company is closed in  Accounts Payable.");
      if (finPeriod.INClosed.GetValueOrDefault() && PXAccess.FeatureInstalled<FeaturesSet.inventory>())
        validationResult.Aggregate((ProcessingResultBase<OpenPeriodAttribute.PeriodValidationResult, object, OpenPeriodAttribute.PeriodValidationMessage>) this.HandleErrorThatPeriodIsClosed(sender, finPeriod, errorMessage: "The {0} financial period of the {1} company is closed in Inventory."));
    }
    return validationResult;
  }
}
