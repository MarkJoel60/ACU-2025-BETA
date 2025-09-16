// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAClosingProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;

#nullable disable
namespace PX.Objects.CA;

public class CAClosingProcess : FinPeriodClosingProcessBase<CAClosingProcess, FinPeriod.cAClosed>
{
  protected static BqlCommand OpenDocumentsQuery { get; } = PXSelectBase<CATran, PXSelectJoin<CATran, LeftJoin<CashAccount, On<CATran.cashAccountID, Equal<CashAccount.cashAccountID>>, LeftJoin<CADeposit, On<CATran.origRefNbr, Equal<CADeposit.refNbr>, And<CATran.origTranType, Equal<CADeposit.tranType>>>, LeftJoin<CAAdj, On<CATran.origRefNbr, Equal<CAAdj.adjRefNbr>, And<CATran.origTranType, Equal<CAAdj.adjTranType>>>, InnerJoin<PX.Objects.GL.Branch, On<CATran.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<CAExpense, On<CATran.tranID, Equal<CAExpense.cashTranID>>, LeftJoin<CAExpenseBranch, On<CAExpense.branchID, Equal<CAExpenseBranch.branchID>>, LeftJoin<CASplit, On<CATran.origTranType, Equal<CASplit.adjTranType>, And<CATran.origRefNbr, Equal<CASplit.adjRefNbr>>>, LeftJoin<CASplitBranch, On<CASplit.branchID, Equal<CASplitBranch.branchID>>>>>>>>>>, Where2<Where2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<CATran.finPeriodID, PX.Objects.GL.Branch.organizationID>, Or2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<CATran.finPeriodID, CAExpenseBranch.organizationID>, Or<FinPeriodClosingProcessBase.WhereFinPeriodInRange<CATran.finPeriodID, CASplitBranch.organizationID>>>>, And<CATran.origModule, Equal<BatchModule.moduleCA>, And<CATran.released, NotEqual<True>, And<Where<CAAdj.rejected, NotEqual<True>, Or<CAAdj.rejected, IsNull>>>>>>, OrderBy<Asc<CATran.finPeriodID, Asc<CATran.origModule, Asc<CATran.origTranType, Asc<CATran.origRefNbr>>>>>>.Config>.GetCommand();

  protected static BqlCommand OpenVouchersQuery { get; } = PXSelectBase<GLTranDoc, PXSelectJoin<GLTranDoc, InnerJoin<PX.Objects.GL.Branch, On<GLTranDoc.branchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<CAAdj, On<GLTranDoc.refNbr, Equal<CAAdj.adjRefNbr>, And<GLTranDoc.tranType, Equal<CAAdj.adjTranType>>>, InnerJoin<CashAccount, On<IIf<Where<GLTranDoc.debitCashAccountID, IsNull>, GLTranDoc.creditCashAccountID, GLTranDoc.debitCashAccountID>, Equal<CashAccount.cashAccountID>>>>>, Where2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<GLTranDoc.finPeriodID, PX.Objects.GL.Branch.organizationID>, And<GLTranDoc.tranModule, Equal<BatchModule.moduleCA>, And<GLTranDoc.released, Equal<False>>>>, OrderBy<Asc<GLTranDoc.finPeriodID, Asc<GLTranDoc.tranType, Asc<GLTranDoc.refNbr, Asc<GLTranDoc.lineNbr>>>>>>.Config>.GetCommand();

  protected override FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule[] CheckingRules { get; } = new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule[2]
  {
    new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule()
    {
      ReportID = "CA656100",
      ErrorMessage = "Unreleased documents exist for the {0} financial period.",
      CheckCommand = CAClosingProcess.OpenDocumentsQuery
    },
    new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule()
    {
      ReportID = "CA656150",
      ErrorMessage = "Unreleased documents exist for the {0} financial period.",
      CheckCommand = CAClosingProcess.OpenVouchersQuery
    }
  };
}
