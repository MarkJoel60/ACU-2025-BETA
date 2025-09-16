// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAClosingProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.FA;

public class FAClosingProcess : 
  FinPeriodClosingProcessBase<FAClosingProcess, FinPeriod.fAClosed, FeaturesSet.fixedAsset>
{
  protected static BqlCommand OpenDocumentsQuery { get; } = PXSelectBase<FARegister, PXSelectJoin<FARegister, InnerJoin<FATran, On<FARegister.refNbr, Equal<FATran.refNbr>>, InnerJoin<FABook, On<FATran.bookID, Equal<FABook.bookID>, And<FABook.updateGL, Equal<True>>>, LeftJoin<PX.Objects.GL.Branch, On<FATran.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<TranBranch, On<FATran.srcBranchID, Equal<TranBranch.branchID>>>>>>, Where<FARegister.released, NotEqual<True>, And<Where2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<FATran.finPeriodID, PX.Objects.GL.Branch.organizationID>, Or<FinPeriodClosingProcessBase.WhereFinPeriodInRange<FATran.finPeriodID, TranBranch.organizationID>>>>>>.Config>.GetCommand();

  protected static BqlCommand NonDepreciatedAssetsQuery { get; } = PXSelectBase<FABookBalance, PXSelectJoin<FABookBalance, LeftJoin<FixedAsset, On<FixedAsset.assetID, Equal<FABookBalance.assetID>>, LeftJoin<PX.Objects.GL.Branch, On<FixedAsset.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FABook, On<FABookBalance.bookID, Equal<FABook.bookID>>, LeftJoin<FADetails, On<FADetails.assetID, Equal<FABookBalance.assetID>>>>>>, Where<FABookBalance.deprFromPeriod, LessEqual<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.toFinPeriodID>>, And<FABookBalance.deprToPeriod, GreaterEqual<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.fromFinPeriodID>>, And2<Where<PX.Objects.GL.Branch.organizationID, Equal<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.organizationID>>, Or<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.organizationID>, IsNull>>, And<FABookBalance.updateGL, Equal<True>, And<FixedAsset.suspended, NotEqual<True>, And<FADetails.hold, NotEqual<True>, And<FABookBalance.initPeriod, IsNotNull, And<Where<FABookBalance.currDeprPeriod, IsNull, And<FABookBalance.status, Equal<FixedAssetStatus.active>, Or<FABookBalance.currDeprPeriod, LessEqual<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.toFinPeriodID>>>>>>>>>>>>>>.Config>.GetCommand();

  protected override FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule[] CheckingRules { get; } = new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule[2]
  {
    new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule()
    {
      ReportID = "FA651100",
      ErrorMessage = "Unreleased documents exist for the {0} financial period.",
      CheckCommand = FAClosingProcess.OpenDocumentsQuery
    },
    new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule()
    {
      ReportID = "FA652100",
      ErrorMessage = "Fixed asset '{0}' not depreciated by the book '{1}' in {2} period.",
      MessageParameters = new Type[2]
      {
        typeof (FixedAsset.assetCD),
        typeof (FABook.bookCode)
      },
      CheckCommand = FAClosingProcess.NonDepreciatedAssetsQuery
    }
  };

  protected override string EmptyReportMessage
  {
    get
    {
      return "There are no unreleased documents or fixed assets to be depreciated for the selected period or periods.";
    }
  }
}
