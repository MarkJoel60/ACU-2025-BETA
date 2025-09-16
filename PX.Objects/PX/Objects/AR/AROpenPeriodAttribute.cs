// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.AROpenPeriodAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// Specialized for AR version of the <see cref="!:OpenPeriodAttribut" /><br />
/// Selector. Provides  a list  of the active Fin. Periods, having ARClosed flag = false <br />
/// <example>
/// [AROpenPeriod(typeof(ARRegister.docDate))]
/// </example>
/// </summary>
public class AROpenPeriodAttribute(
  Type sourceType,
  Type branchSourceType = null,
  Type branchSourceFormulaType = null,
  Type organizationSourceType = null,
  Type useMasterCalendarSourceType = null,
  Type defaultType = null,
  bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
  bool useMasterOrganizationIDByDefault = false,
  FinPeriodSelectorAttribute.SelectionModesWithRestrictions selectionModeWithRestrictions = FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined,
  Type[] sourceSpecificationTypes = null,
  Type masterFinPeriodIDType = null) : OpenPeriodAttribute(typeof (Search<FinPeriod.finPeriodID, Where<FinPeriod.aRClosed, Equal<False>, And<FinPeriod.status, Equal<FinPeriod.status.open>>>>), sourceType, branchSourceType, branchSourceFormulaType, organizationSourceType, useMasterCalendarSourceType, defaultType, redefaultOrRevalidateOnOrganizationSourceUpdated, useMasterOrganizationIDByDefault: useMasterOrganizationIDByDefault, selectionModeWithRestrictions: selectionModeWithRestrictions, sourceSpecificationTypes: sourceSpecificationTypes, masterFinPeriodIDType: masterFinPeriodIDType)
{
  public AROpenPeriodAttribute()
    : this((Type) null)
  {
  }

  public static void DefaultFirstOpenPeriod(PXCache sender, string FieldName)
  {
    foreach (PeriodIDAttribute periodIdAttribute in sender.GetAttributesReadonly(FieldName).OfType<PeriodIDAttribute>())
      periodIdAttribute.DefaultType = typeof (Search2<FinPeriod.finPeriodID, CrossJoin<GLSetup>, Where<FinPeriod.endDate, Greater<Current2<PeriodIDAttribute.QueryParams.sourceDate>>, And2<Where<FinPeriod.status, Equal<FinPeriod.status.open>, Or<FinPeriod.status, Equal<FinPeriod.status.closed>>>, And<Where<GLSetup.restrictAccessToClosedPeriods, NotEqual<True>, Or<FinPeriod.aRClosed, Equal<False>>>>>>, OrderBy<Asc<FinPeriod.finPeriodID>>>);
  }

  public static void DefaultFirstOpenPeriod<Field>(PXCache sender) where Field : IBqlField
  {
    AROpenPeriodAttribute.DefaultFirstOpenPeriod(sender, typeof (Field).Name);
  }

  protected override OpenPeriodAttribute.PeriodValidationResult ValidateOrganizationFinPeriodStatus(
    PXCache sender,
    object row,
    FinPeriod finPeriod)
  {
    OpenPeriodAttribute.PeriodValidationResult validationResult = base.ValidateOrganizationFinPeriodStatus(sender, row, finPeriod);
    if (!validationResult.HasWarningOrError && finPeriod.ARClosed.GetValueOrDefault())
      validationResult = this.HandleErrorThatPeriodIsClosed(sender, finPeriod, errorMessage: "The {0} financial period of the {1} company is closed in Accounts Receivable.");
    return validationResult;
  }
}
