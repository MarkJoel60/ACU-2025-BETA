// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INOpenPeriodAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INOpenPeriodAttribute(
  Type sourceType,
  Type branchSourceType = null,
  Type branchSourceFormulaType = null,
  Type organizationSourceType = null,
  Type useMasterCalendarSourceType = null,
  Type defaultType = null,
  bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
  FinPeriodSelectorAttribute.SelectionModesWithRestrictions selectionModeWithRestrictions = FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined,
  Type masterFinPeriodIDType = null) : OpenPeriodAttribute(typeof (Search<FinPeriod.finPeriodID, Where<FinPeriod.iNClosed, Equal<False>, And<FinPeriod.status, Equal<FinPeriod.status.open>>>>), sourceType, branchSourceType, branchSourceFormulaType, organizationSourceType, useMasterCalendarSourceType, defaultType, redefaultOrRevalidateOnOrganizationSourceUpdated, selectionModeWithRestrictions: selectionModeWithRestrictions, masterFinPeriodIDType: masterFinPeriodIDType)
{
  public INOpenPeriodAttribute()
    : this((Type) null)
  {
  }

  protected override OpenPeriodAttribute.PeriodValidationResult ValidateOrganizationFinPeriodStatus(
    PXCache sender,
    object row,
    FinPeriod finPeriod)
  {
    OpenPeriodAttribute.PeriodValidationResult validationResult = base.ValidateOrganizationFinPeriodStatus(sender, row, finPeriod);
    if (!validationResult.HasWarningOrError && finPeriod.INClosed.GetValueOrDefault())
      validationResult = this.HandleErrorThatPeriodIsClosed(sender, finPeriod, errorMessage: "The {0} financial period of the {1} company is closed in Inventory.");
    return validationResult;
  }
}
