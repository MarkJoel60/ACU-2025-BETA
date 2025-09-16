// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APOpenPeriodAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// Specialized version of the selector for AP Open Financial Periods.<br />
/// Displays a list of FinPeriods, having flags Active = true and  APClosed = false.<br />
/// </summary>
public class APOpenPeriodAttribute(
  System.Type sourceType,
  System.Type branchSourceType = null,
  System.Type branchSourceFormulaType = null,
  System.Type organizationSourceType = null,
  System.Type useMasterCalendarSourceType = null,
  System.Type defaultType = null,
  bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
  bool useMasterOrganizationIDByDefault = false,
  FinPeriodSelectorAttribute.SelectionModesWithRestrictions selectionModeWithRestrictions = FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined,
  System.Type[] sourceSpecificationTypes = null,
  System.Type masterFinPeriodIDType = null) : OpenPeriodAttribute(typeof (Search<FinPeriod.finPeriodID, Where<FinPeriod.aPClosed, Equal<False>, And<FinPeriod.status, Equal<FinPeriod.status.open>>>>), sourceType, branchSourceType, branchSourceFormulaType, organizationSourceType, useMasterCalendarSourceType, defaultType, redefaultOrRevalidateOnOrganizationSourceUpdated, useMasterOrganizationIDByDefault: useMasterOrganizationIDByDefault, selectionModeWithRestrictions: selectionModeWithRestrictions, sourceSpecificationTypes: sourceSpecificationTypes, masterFinPeriodIDType: masterFinPeriodIDType)
{
  public APOpenPeriodAttribute()
    : this((System.Type) null)
  {
  }

  protected override OpenPeriodAttribute.PeriodValidationResult ValidateOrganizationFinPeriodStatus(
    PXCache sender,
    object row,
    FinPeriod finPeriod)
  {
    OpenPeriodAttribute.PeriodValidationResult validationResult = base.ValidateOrganizationFinPeriodStatus(sender, row, finPeriod);
    if (!validationResult.HasWarningOrError && finPeriod.APClosed.GetValueOrDefault())
      validationResult = this.HandleErrorThatPeriodIsClosed(sender, finPeriod, errorMessage: "The {0} financial period of the {1} company is closed in  Accounts Payable.");
    return validationResult;
  }

  public static void DefaultFirstOpenPeriod<FinPeriodIDField>(PXCache sender) where FinPeriodIDField : IBqlField
  {
    foreach (PeriodIDAttribute periodIdAttribute in sender.GetAttributesReadonly(typeof (FinPeriodIDField).Name).OfType<PeriodIDAttribute>())
      periodIdAttribute.DefaultType = typeof (Search2<FinPeriod.finPeriodID, CrossJoin<GLSetup>, Where<FinPeriod.endDate, Greater<Current2<PeriodIDAttribute.QueryParams.sourceDate>>, And2<Where<FinPeriod.status, Equal<FinPeriod.status.open>, Or<FinPeriod.status, Equal<FinPeriod.status.closed>>>, And<Where<GLSetup.restrictAccessToClosedPeriods, NotEqual<True>, Or<FinPeriod.aPClosed, Equal<False>>>>>>, OrderBy<Asc<FinPeriod.finPeriodID>>>);
  }
}
