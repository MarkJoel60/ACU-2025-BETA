// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLHistoryValidate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

[TableAndChartDashboardType]
public class GLHistoryValidate : PXGraph<
#nullable disable
GLHistoryValidate>
{
  public PXFilter<GLHistoryValidate.GLIntegrityCheckFilter> Filter;
  public PXCancel<GLHistoryValidate.GLIntegrityCheckFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<Ledger, GLHistoryValidate.GLIntegrityCheckFilter, Where<Ledger.balanceType, Equal<LedgerBalanceType.actual>, Or<Ledger.balanceType, Equal<LedgerBalanceType.report>, Or<Ledger.balanceType, Equal<LedgerBalanceType.statistical>>>>> LedgerList;
  public PXSetup<GLSetup> glsetup;

  public GLHistoryValidate()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GLHistoryValidate.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new GLHistoryValidate.\u003C\u003Ec__DisplayClass1_0();
    GLSetup current = ((PXSelectBase<GLSetup>) this.glsetup).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.filter = ((PXSelectBase<GLHistoryValidate.GLIntegrityCheckFilter>) this.Filter).Current;
    ((PXProcessing<Ledger>) this.LedgerList).SetProcessCaption("Process");
    ((PXProcessing<Ledger>) this.LedgerList).SetProcessAllCaption("Process All");
    ((PXProcessing<Ledger>) this.LedgerList).SetProcessTooltip("Recalculate account balances based on posted transactions");
    ((PXProcessing<Ledger>) this.LedgerList).SetProcessAllTooltip("Recalculate account balances based on posted transactions");
    ((PXProcessingBase<Ledger>) this.LedgerList).SuppressMerge = true;
    ((PXProcessingBase<Ledger>) this.LedgerList).SuppressUpdate = true;
    // ISSUE: method pointer
    ((PXProcessingBase<Ledger>) this.LedgerList).SetProcessDelegate<PostGraph>(new PXProcessingBase<Ledger>.ProcessItemDelegate<PostGraph>((object) cDisplayClass10, __methodptr(\u003C\u002Ector\u003Eb__0)));
    PXUIFieldAttribute.SetEnabled<Ledger.selected>(((PXSelectBase) this.LedgerList).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<Ledger.ledgerCD>(((PXSelectBase) this.LedgerList).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<Ledger.descr>(((PXSelectBase) this.LedgerList).Cache, (object) null, false);
  }

  protected virtual void GLIntegrityCheckFilter_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    bool flag = PXUIFieldAttribute.GetErrors(sender, (object) null, new PXErrorLevel[2]
    {
      (PXErrorLevel) 4,
      (PXErrorLevel) 5
    }).Count > 0;
    ((PXProcessing<Ledger>) this.LedgerList).SetProcessEnabled(!flag);
    ((PXProcessing<Ledger>) this.LedgerList).SetProcessAllEnabled(!flag);
  }

  private static void Validate(
    PostGraph graph,
    Ledger ledger,
    GLHistoryValidate.GLIntegrityCheckFilter filter)
  {
    if (string.IsNullOrEmpty(filter.FinPeriodID))
      throw new PXException("GL Error: You must fill in the Fin. Period box to perform validation.");
    if (RunningFlagScope<PostGraph>.IsRunning)
      throw new PXSetPropertyException((IBqlTable) ledger, "GL batch posting is in progress. Please wait until the process is completed.", (PXErrorLevel) 2);
    using (new RunningFlagScope<GLHistoryValidate>())
    {
      ((PXGraph) graph).Clear();
      graph.IntegrityCheckProc(ledger, filter.FinPeriodID);
      graph = PXGraph.CreateInstance<PostGraph>();
      graph.PostBatchesRequiredPosting();
    }
  }

  [Serializable]
  public class GLIntegrityCheckFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [FinPeriodNonLockedSelector]
    [PXUIField(DisplayName = "Fin. Period")]
    public virtual string FinPeriodID { get; set; }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GLHistoryValidate.GLIntegrityCheckFilter.finPeriodID>
    {
    }
  }
}
