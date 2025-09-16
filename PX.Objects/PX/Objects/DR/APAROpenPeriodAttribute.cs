// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.APAROpenPeriodAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.DR;

public class APAROpenPeriodAttribute : OpenPeriodAttribute
{
  private readonly Type _origModuteField;

  public PXErrorLevel errorLevel { get; set; } = (PXErrorLevel) 4;

  public APAROpenPeriodAttribute(
    Type origModule,
    Type sourceType,
    Type branchSourceType,
    Type branchSourceFormulaType = null,
    Type organizationSourceType = null,
    Type useMasterCalendarSourceType = null,
    Type defaultType = null,
    bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
    Type masterFinPeriodIDType = null,
    bool redefaultOnDateChanged = true)
    : base(((IBqlTemplate) BqlTemplate.OfCommand<Search<FinPeriod.finPeriodID, Where<FinPeriod.status, Equal<FinPeriod.status.open>, And<Where2<Where<Current<APAROpenPeriodAttribute.OrigModulePh>, Equal<BatchModule.moduleAP>, And<FinPeriod.aPClosed, Equal<False>>>, Or2<Where<Current<APAROpenPeriodAttribute.OrigModulePh>, Equal<BatchModule.moduleAR>, And<FinPeriod.aRClosed, Equal<False>>>, Or<Current<APAROpenPeriodAttribute.OrigModulePh>, IsNull>>>>>>>.Replace<APAROpenPeriodAttribute.OrigModulePh>(origModule)).ToType(), sourceType, branchSourceType, branchSourceFormulaType, organizationSourceType, useMasterCalendarSourceType, defaultType, redefaultOrRevalidateOnOrganizationSourceUpdated, masterFinPeriodIDType: masterFinPeriodIDType, redefaultOnDateChanged: redefaultOnDateChanged)
  {
    this._origModuteField = origModule;
  }

  public static void VerifyPeriod<Field>(PXCache cache, object row) where Field : IBqlField
  {
    foreach (OpenPeriodAttribute openPeriodAttribute in cache.GetAttributesReadonly<Field>(row).OfType<APAROpenPeriodAttribute>())
      openPeriodAttribute.IsValidPeriod(cache, row, cache.GetValue<Field>(row));
  }

  protected override OpenPeriodAttribute.PeriodValidationResult ValidateOrganizationFinPeriodStatus(
    PXCache sender,
    object row,
    FinPeriod finPeriod)
  {
    OpenPeriodAttribute.PeriodValidationResult validationResult = base.ValidateOrganizationFinPeriodStatus(sender, row, finPeriod);
    if (!validationResult.HasWarningOrError)
    {
      string str = (string) sender.GetValue(row, this._origModuteField.Name);
      if (str == "AP" && finPeriod.APClosed.GetValueOrDefault() || str == "AR" && finPeriod.ARClosed.GetValueOrDefault())
        validationResult = this.HandleErrorThatPeriodIsClosed(sender, finPeriod);
    }
    return validationResult;
  }

  [PXHidden]
  public class OrigModulePh : BqlPlaceholderBase
  {
  }
}
