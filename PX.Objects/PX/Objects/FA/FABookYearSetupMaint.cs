// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookYearSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FA;

public class FABookYearSetupMaint : 
  YearSetupGraph<FABookYearSetupMaint, FABookYearSetup, FABookPeriodSetup, Where<FABookPeriodSetup.bookID, Equal<Current<FABookYearSetup.bookID>>>>
{
  public PXAction<FABookYearSetup> DeleteGeneratedPeriods;

  protected virtual IEnumerable fiscalYearSetup()
  {
    return (IEnumerable) GraphHelper.RowCast<FABookYearSetup>((IEnumerable) PXSelectBase<FABookYearSetup, PXSelectJoin<FABookYearSetup, LeftJoin<FABook, On<FABookYearSetup.bookID, Equal<FABook.bookID>>>, Where<FABook.updateGL, NotEqual<True>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
  }

  public override void SetCurrentYearSetup(object[] key)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    if (key.Length < 1)
      throw new ArgumentOutOfRangeException(nameof (key));
    int num = key[0] != null ? (int) key[0] : throw new ArgumentNullException(nameof (key));
    PXView pxView = new PXView((PXGraph) this, false, (BqlCommand) new Select<FABookYearSetup, Where<FABookYearSetup.bookID, Equal<Required<FABook.bookID>>>>());
    pxView.Clear();
    PXSelect<FABookYearSetup> fiscalYearSetup = this.FiscalYearSetup;
    FABookYearSetup faBookYearSetup = ((PXSelectBase<FABookYearSetup>) this.FiscalYearSetup).Current;
    if (faBookYearSetup == null)
      faBookYearSetup = pxView.SelectSingle(new object[1]
      {
        (object) num
      }) as FABookYearSetup;
    ((PXSelectBase<FABookYearSetup>) fiscalYearSetup).Current = faBookYearSetup;
  }

  protected override bool IsFiscalYearSetupExists() => false;

  protected override bool IsFiscalYearExists()
  {
    return ((PXSelectBase) new PXSelect<FABookYear, Where<FABookYear.bookID, Equal<Current<FABookYearSetup.bookID>>, And<FABookYear.organizationID, Equal<FinPeriod.organizationID.masterValue>>>>((PXGraph) this)).View.SelectSingle(Array.Empty<object>()) != null;
  }

  protected override bool CheckForPartiallyDefinedYear()
  {
    FABookYear faBookYear1 = (FABookYear) null;
    foreach (PXResult<FABookYear> pxResult1 in PXSelectBase<FABookYear, PXSelect<FABookYear, Where<FABookYear.bookID, Equal<Current<FABookYearSetup.bookID>>, And<FABookYear.organizationID, Equal<FinPeriod.organizationID.masterValue>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      FABookYear faBookYear2 = PXResult<FABookYear>.op_Implicit(pxResult1);
      int num1 = 0;
      foreach (PXResult<FABookPeriod> pxResult2 in PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.finYear, Equal<Required<FABookPeriod.finYear>>, And<FABookPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) faBookYear2.Year
      }))
      {
        PXResult<FABookPeriod>.op_Implicit(pxResult2);
        ++num1;
      }
      int num2 = num1;
      short? finPeriods = faBookYear2.FinPeriods;
      int? nullable = finPeriods.HasValue ? new int?((int) finPeriods.GetValueOrDefault()) : new int?();
      int valueOrDefault = nullable.GetValueOrDefault();
      if (num2 < valueOrDefault & nullable.HasValue)
      {
        faBookYear1 = faBookYear2;
        break;
      }
    }
    return faBookYear1 != null;
  }

  protected override bool AllowDeleteYearSetup(out string errMsg)
  {
    errMsg = "You cannot delete Financial Year Setup because one or more fixed asset book periods exist in the system.";
    return !this.IsFiscalYearExists();
  }

  protected override bool AllowSetupModification(FABookYearSetup aRow)
  {
    return ((PXSelectBase<FABookYearSetup>) this.FiscalYearSetup).Current.BookID.HasValue;
  }

  [PXUIField]
  [PXButton(Category = "Period Management", DisplayOnMainToolbar = true)]
  public virtual IEnumerable deleteGeneratedPeriods(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new FABookYearSetupMaint.\u003C\u003Ec__DisplayClass8_0()
    {
      year = ((PXSelectBase<FABookYearSetup>) this.FiscalYearSetup).Current
    }, __methodptr(\u003CdeleteGeneratedPeriods\u003Eb__0)));
    return adapter.Get();
  }

  protected override void TYearSetupOnRowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    base.TYearSetupOnRowSelected(cache, e);
    FABookYearSetup row = (FABookYearSetup) e.Row;
    FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.bookID, Equal<Current<FABookYearSetup.bookID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    FABookYear faBookYear = (FABookYear) null;
    if (faBookBalance == null)
      faBookYear = PXResultset<FABookYear>.op_Implicit(PXSelectBase<FABookYear, PXSelect<FABookYear, Where<FABookYear.bookID, Equal<Current<FABookYearSetup.bookID>>, And<FABookYear.organizationID, Equal<FinPeriod.organizationID.masterValue>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) row
      }, Array.Empty<object>()));
    ((PXAction) this.DeleteGeneratedPeriods).SetEnabled(faBookBalance == null && faBookYear != null);
  }
}
