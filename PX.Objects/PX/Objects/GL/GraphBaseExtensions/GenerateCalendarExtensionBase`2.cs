// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GraphBaseExtensions.GenerateCalendarExtensionBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Extensions;
using PX.Objects.FA;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.GL.GraphBaseExtensions;

public class GenerateCalendarExtensionBase<FinPeriodMaintenanceGraph, PrimaryFinYear> : 
  PXGraphExtension<FinPeriodMaintenanceGraph>
  where FinPeriodMaintenanceGraph : PXGraph, IFinPeriodMaintenanceGraph, new()
  where PrimaryFinYear : class, IBqlTable, IFinYear, new()
{
  public PXFilter<FinPeriodGenerateParameters> GenerateParams;
  public PXAction<PrimaryFinYear> GenerateYears;

  [PXButton(Category = "Period Management", DisplayOnMainToolbar = true)]
  [PXUIField]
  public virtual IEnumerable generateYears(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GenerateCalendarExtensionBase<FinPeriodMaintenanceGraph, PrimaryFinYear>.\u003C\u003Ec__DisplayClass2_0 cDisplayClass20 = new GenerateCalendarExtensionBase<FinPeriodMaintenanceGraph, PrimaryFinYear>.\u003C\u003Ec__DisplayClass2_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.\u003C\u003E4__this = this;
    IFinPeriodRepository service = this.Base.GetService<IFinPeriodRepository>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.finPeriodUtils = this.Base.GetService<IFinPeriodUtils>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.primaryYear = (PrimaryFinYear) ((PXCache) GraphHelper.Caches<PrimaryFinYear>((PXGraph) this.Base)).Current;
    // ISSUE: reference to a compiler-generated field
    if ((object) cDisplayClass20.primaryYear == null)
      throw new PXException("Select a company and create its first calendar year if needed.");
    int result1;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.firstExistingYear = int.TryParse(service.FindFirstYear(new int?(cDisplayClass20.primaryYear.OrganizationID.GetValueOrDefault()), true)?.Year, out result1) ? new int?(result1) : new int?();
    int result2;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.lastExistingYear = int.TryParse(service.FindLastYear(new int?(cDisplayClass20.primaryYear.OrganizationID.GetValueOrDefault()), true)?.Year, out result2) ? new int?(result2) : new int?();
    bool flag = true;
    if (!this.Base.IsContractBasedAPI)
    {
      // ISSUE: method pointer
      flag = this.GenerateParams.AskExtFullyValid(new PXView.InitializePanel((object) cDisplayClass20, __methodptr(\u003CgenerateYears\u003Eb__0)), (DialogAnswerType) 1, true);
    }
    if (flag)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) new GenerateCalendarExtensionBase<FinPeriodMaintenanceGraph, PrimaryFinYear>.\u003C\u003Ec__DisplayClass2_1()
      {
        CS\u0024\u003C\u003E8__locals1 = cDisplayClass20,
        fromYear = int.Parse(((PXSelectBase<FinPeriodGenerateParameters>) this.GenerateParams).Current.FromYear),
        toYear = int.Parse(((PXSelectBase<FinPeriodGenerateParameters>) this.GenerateParams).Current.ToYear),
        processingGraph = (IFinPeriodMaintenanceGraph) GraphHelper.Clone<FinPeriodMaintenanceGraph>(this.Base)
      }, __methodptr(\u003CgenerateYears\u003Eb__1)));
      PXLongOperation.WaitCompletion(this.Base.UID);
      if (!this.Base.IsContractBasedAPI && PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABook>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.bookID, Equal<FABook.bookID>>>>>.And<BqlOperand<FABook.updateGL, IBqlBool>.IsEqual<True>>>>, FbqlJoins.Inner<FinPeriod>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.organizationID, Equal<FinPeriod.organizationID>>>>>.And<BqlOperand<FABookPeriod.finPeriodID, IBqlString>.IsEqual<FinPeriod.finPeriodID>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.startDate, NotEqual<FinPeriod.startDate>>>>>.Or<BqlOperand<FABookPeriod.endDate, IBqlDateTime>.IsNotEqual<FinPeriod.endDate>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, Array.Empty<object>())) != null)
        throw new PXException("Financial periods for the selected year exist in the fixed asset posting book and do not match the periods in the general ledger. To amend the periods in the posting book based on the periods in the general ledger, on the Book Calendars (FA304000) form, click Synchronize FA Calendar with GL.");
    }
    return adapter.Get();
  }
}
