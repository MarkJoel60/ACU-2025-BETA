// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.RevalueAcountsBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.CM;

public class RevalueAcountsBase<THistory> : PXGraph<RevalueAcountsBase<THistory>> where THistory : class, IBqlTable, new()
{
  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  public virtual ProcessingResult CheckFinPeriod(string finPeriodID, int? branchID)
  {
    ProcessingResult processingResult = new ProcessingResult();
    FinPeriod byId = this.FinPeriodRepository.FindByID(PXAccess.GetParentOrganizationID(branchID), finPeriodID);
    if (byId == null)
      processingResult.AddErrorMessage("The {0} financial period does not exist for the {1} company.", (object) FinPeriodIDFormattingAttribute.FormatForError(finPeriodID), (object) PXAccess.GetOrganizationCD(PXAccess.GetParentOrganizationID(branchID)));
    else
      processingResult = this.FinPeriodUtils.CanPostToPeriod((IFinPeriod) byId);
    if (!processingResult.IsSuccess)
      PXProcessing<THistory>.SetError((Exception) new PXException(processingResult.GetGeneralMessage()));
    return processingResult;
  }

  public virtual void VerifyCurrencyEffectiveDate(PXCache cache, RevalueFilter filter)
  {
    cache.RaiseExceptionHandling<RevalueFilter.curyEffDate>((object) filter, (object) filter.CuryEffDate, (Exception) null);
    string finPeriodId = filter?.FinPeriodID;
    DateTime? curyEffDate = (DateTime?) filter?.CuryEffDate;
    if (!curyEffDate.HasValue || string.IsNullOrEmpty(finPeriodId))
      return;
    FinPeriod byId = this.FinPeriodRepository.FindByID(new int?(filter.OrganizationID.GetValueOrDefault()), finPeriodId);
    if (byId == null)
      return;
    bool? isAdjustment = byId.IsAdjustment;
    DateTime? nullable1;
    int num;
    if (isAdjustment.GetValueOrDefault())
    {
      nullable1 = curyEffDate;
      DateTime dateTime = byId.EndDate.Value.AddDays(-1.0);
      if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() == dateTime ? 1 : 0) : 0) != 0)
      {
        num = 1;
        goto label_11;
      }
    }
    isAdjustment = byId.IsAdjustment;
    if (!isAdjustment.GetValueOrDefault())
    {
      nullable1 = curyEffDate;
      DateTime? nullable2 = byId.StartDate;
      if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable2 = curyEffDate;
        nullable1 = byId.EndDate;
        num = nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0;
        goto label_11;
      }
    }
    num = 0;
label_11:
    if (num != 0)
      return;
    cache.RaiseExceptionHandling<RevalueFilter.curyEffDate>((object) filter, (object) filter.CuryEffDate, (Exception) new PXSetPropertyException("Currency rate effective date is outside the specified financial period.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    Events.FieldDefaulting<RevalueFilter, RevalueFilter.curyEffDate> e)
  {
    if (e.Row == null)
      return;
    FinPeriod byId = this.FinPeriodRepository.FindByID(new int?(e.Row.OrganizationID.GetValueOrDefault()), e.Row.FinPeriodID);
    if (byId == null)
      return;
    DateTime? endDate = byId.EndDate;
    if (!endDate.HasValue)
      return;
    Events.FieldDefaulting<RevalueFilter, RevalueFilter.curyEffDate> fieldDefaulting = e;
    endDate = byId.EndDate;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime> local = (ValueType) endDate.Value.AddDays(-1.0);
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<RevalueFilter, RevalueFilter.curyEffDate>, RevalueFilter, object>) fieldDefaulting).NewValue = (object) local;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<RevalueFilter, RevalueFilter.curyEffDate>>) e).Cancel = true;
  }
}
