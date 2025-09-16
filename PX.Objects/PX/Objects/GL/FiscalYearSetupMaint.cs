// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FiscalYearSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.FA;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

public class FiscalYearSetupMaint : 
  YearSetupGraph<FiscalYearSetupMaint, FinYearSetup, FinPeriodSetup>
{
  public FiscalYearSetupMaint()
  {
    ((PXAction) this.First).SetVisible(false);
    ((PXAction) this.Last).SetVisible(false);
    ((PXAction) this.Next).SetVisible(false);
    ((PXAction) this.Previous).SetVisible(false);
  }

  protected override bool IsFiscalYearExists()
  {
    return GraphHelper.RowCast<MasterFinYear>((IEnumerable) PXSelectBase<MasterFinYear, PXSelect<MasterFinYear>.Config>.SelectSingleBound((PXGraph) this, new object[0], Array.Empty<object>())).Any<MasterFinYear>();
  }

  protected override bool IsFiscalYearSetupExists()
  {
    int? rowCount = PXSelectBase<FinYearSetup, PXSelectGroupBy<FinYearSetup, Aggregate<Count>>.Config>.Select((PXGraph) this, Array.Empty<object>()).RowCount;
    int num = 0;
    return rowCount.GetValueOrDefault() > num & rowCount.HasValue;
  }

  protected override bool CheckForPartiallyDefinedYear()
  {
    MasterFinYear masterFinYear1 = (MasterFinYear) null;
    foreach (PXResult<MasterFinYear> pxResult1 in PXSelectBase<MasterFinYear, PXSelect<MasterFinYear>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      MasterFinYear masterFinYear2 = PXResult<MasterFinYear>.op_Implicit(pxResult1);
      int num1 = 0;
      foreach (PXResult<MasterFinPeriod> pxResult2 in PXSelectBase<MasterFinPeriod, PXSelect<MasterFinPeriod, Where<MasterFinPeriod.finYear, Equal<Required<MasterFinYear.year>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) masterFinYear2.Year
      }))
      {
        PXResult<MasterFinPeriod>.op_Implicit(pxResult2);
        ++num1;
      }
      int num2 = num1;
      short? finPeriods = masterFinYear2.FinPeriods;
      int? nullable = finPeriods.HasValue ? new int?((int) finPeriods.GetValueOrDefault()) : new int?();
      int valueOrDefault = nullable.GetValueOrDefault();
      if (num2 < valueOrDefault & nullable.HasValue)
      {
        masterFinYear1 = masterFinYear2;
        break;
      }
    }
    return masterFinYear1 != null;
  }

  protected override bool AllowDeleteYearSetup(out string errMsg)
  {
    errMsg = (string) null;
    if (this.IsFiscalYearExists())
      errMsg = "You cannot delete Financial Year Setup because one or more financial periods exist in the system.";
    else if (((PXSelectBase) new PXSelect<FABookYear>((PXGraph) this)).View.SelectSingle(Array.Empty<object>()) != null)
      errMsg = "You cannot delete Financial Year Setup because one or more fixed asset book periods exist in the system.";
    return errMsg == null;
  }

  public virtual void Persist()
  {
    ((PXGraph) this).Persist();
    if (((PXSelectBase<FinYearSetup>) this.FiscalYearSetup).Current == null && !((PXGraph) this).UnattendedMode)
      throw new PXRedirectRequiredException((PXGraph) this, (string) null);
  }
}
