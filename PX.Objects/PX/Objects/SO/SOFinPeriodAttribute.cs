// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOFinPeriodAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SOFinPeriodAttribute : OpenPeriodAttribute
{
  public SOFinPeriodAttribute()
    : this((Type) null)
  {
  }

  public SOFinPeriodAttribute(Type SourceType)
    : this(SourceType, (Type) null)
  {
  }

  public SOFinPeriodAttribute(
    Type sourceType,
    Type branchSourceType,
    Type branchSourceFormulaType = null,
    Type organizationSourceType = null,
    Type useMasterCalendarSourceType = null,
    Type defaultType = null,
    bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
    bool useMasterOrganizationIDByDefault = false)
    : base(typeof (Search<FinPeriod.finPeriodID, Where<FinPeriod.aRClosed, Equal<False>>>), sourceType, branchSourceType, branchSourceFormulaType, organizationSourceType, useMasterCalendarSourceType, defaultType, redefaultOrRevalidateOnOrganizationSourceUpdated, useMasterOrganizationIDByDefault: useMasterOrganizationIDByDefault)
  {
  }

  public static void DefaultFirstOpenPeriod(PXCache sender, string FieldName)
  {
    AROpenPeriodAttribute.DefaultFirstOpenPeriod(sender, FieldName);
  }

  public static void DefaultFirstOpenPeriod<Field>(PXCache sender) where Field : IBqlField
  {
    SOFinPeriodAttribute.DefaultFirstOpenPeriod(sender, typeof (Field).Name);
  }

  protected override OpenPeriodAttribute.PeriodValidationResult ValidateOrganizationFinPeriodStatus(
    PXCache sender,
    object row,
    FinPeriod finPeriod)
  {
    OpenPeriodAttribute.PeriodValidationResult validationResult = new OpenPeriodAttribute.PeriodValidationResult();
    if (finPeriod.Status == "Closed" || finPeriod.ARClosed.GetValueOrDefault())
      return this.HandleErrorThatPeriodIsClosed(sender, finPeriod);
    if (finPeriod.Status == "Locked")
    {
      validationResult.AddMessage((PXErrorLevel) 2, OpenPeriodAttribute.ExceptionType.Locked, "The {0} financial period is locked in the {1} company.", (object) PeriodIDAttribute.FormatForError(finPeriod.FinPeriodID), (object) PXAccess.GetOrganizationCD(finPeriod.OrganizationID));
      return validationResult;
    }
    if (!(finPeriod.Status == "Inactive"))
      return validationResult;
    validationResult.AddMessage((PXErrorLevel) 2, OpenPeriodAttribute.ExceptionType.Inactive, "The {0} financial period is inactive in the {1} company.", (object) PeriodIDAttribute.FormatForError(finPeriod.FinPeriodID), (object) PXAccess.GetOrganizationCD(finPeriod.OrganizationID));
    return validationResult;
  }
}
