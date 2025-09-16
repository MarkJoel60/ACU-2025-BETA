// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INClosingProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN;

public class INClosingProcess : 
  FinPeriodClosingProcessBase<
  #nullable disable
  INClosingProcess, FinPeriod.iNClosed, FeaturesSet.inventory>
{
  public PXAction<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters> ShowUnpostedDocuments;
  public PXViewOf<PX.Objects.SO.SOOrderShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrderShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AR.ARRegister>.On<PX.Objects.SO.SOOrderShipment.FK.ARRegister>>, FbqlJoins.Left<SOOrderTypeOperation>.On<PX.Objects.SO.SOOrderShipment.FK.OrderTypeOperation>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.confirmed, 
  #nullable disable
  Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.createINDoc, 
  #nullable disable
  Equal<True>>>>, And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.invtRefNbr, IBqlString>.IsNull>>, 
  #nullable disable
  And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOOrderTypeOperation.iNDocType, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  SOOrderTypeOperation.iNDocType, IBqlString>.IsNotEqual<
  #nullable disable
  INTranType.noUpdate>>>>>>.And<Where2<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.invoiceNbr, 
  #nullable disable
  IsNotNull>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.AR.ARRegister.finPeriodID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  FinPeriod.finPeriodID, IBqlString>.AsOptional>>, 
  #nullable disable
  Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.invoiceNbr, 
  #nullable disable
  IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.shipmentType, 
  #nullable disable
  NotEqual<INDocType.dropShip>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.shipDate, 
  #nullable disable
  GreaterEqual<BqlField<
  #nullable enable
  FinPeriod.startDate, IBqlDateTime>.AsOptional>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.shipDate, IBqlDateTime>.IsLess<
  #nullable disable
  BqlField<
  #nullable enable
  FinPeriod.endDate, IBqlDateTime>.AsOptional>>>>>>>>>>.ReadOnly UnpostedDocuments;

  [PXUIField]
  [PXButton(VisibleOnProcessingResults = true)]
  public virtual 
  #nullable disable
  IEnumerable showUnpostedDocuments(PXAdapter adapter)
  {
    this.ShowOpenShipments(this.SelectedItems);
    return adapter.Get();
  }

  protected virtual void ShowOpenShipments(IEnumerable<FinPeriod> periods)
  {
    ParallelQuery<string> source = periods.Select<FinPeriod, string>((Func<FinPeriod, string>) (fp => fp.FinPeriodID)).AsParallel<string>();
    throw new PXReportRequiredException(new Dictionary<string, string>()
    {
      ["FromPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(source.Min<string>()),
      ["ToPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(source.Max<string>()),
      ["OrgID"] = OrganizationMaint.FindOrganizationByID((PXGraph) this, ((PXSelectBase<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters>) this.Filter).Current.OrganizationID)?.OrganizationCD
    }, "IN656500", (PXBaseRedirectException.WindowMode) 3, "Documents Not Posted to Inventory", (CurrentLocalization) null);
  }

  protected static BqlCommand OpenDocumentsQuery { get; } = PXSelectBase<INRegister, PXSelectJoin<INRegister, LeftJoin<INTran, On<INTran.docType, Equal<INRegister.docType>, And<INTran.refNbr, Equal<INRegister.refNbr>>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<INRegister.branchID>>, LeftJoin<INSiteTo, On<INSiteTo.siteID, Equal<INRegister.toSiteID>, And<INRegister.transferType, Equal<INTransferType.oneStep>>>, LeftJoin<INSiteToBranch, On<INSiteToBranch.branchID, Equal<INSiteTo.branchID>>, LeftJoin<TranBranch, On<TranBranch.branchID, Equal<INTran.branchID>>, LeftJoin<TranINSite, On<TranINSite.siteID, Equal<INTran.siteID>>, LeftJoin<TranINSiteBranch, On<TranINSiteBranch.branchID, Equal<TranINSite.branchID>>>>>>>>>, Where<INRegister.released, NotEqual<True>, And<Where2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<INRegister.finPeriodID, PX.Objects.GL.Branch.organizationID>, Or2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<INRegister.finPeriodID, INSiteToBranch.organizationID>, Or2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<INRegister.finPeriodID, TranBranch.organizationID>, Or<FinPeriodClosingProcessBase.WhereFinPeriodInRange<INRegister.finPeriodID, TranINSiteBranch.organizationID>>>>>>>, OrderBy<Asc<INRegister.finPeriodID, Asc<INRegister.docType, Asc<INRegister.refNbr>>>>>.Config>.GetCommand();

  protected override FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule[] CheckingRules { get; } = new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule[1]
  {
    new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule()
    {
      ReportID = "IN656600",
      ErrorMessage = "Unreleased documents exist for the {0} financial period.",
      CheckCommand = INClosingProcess.OpenDocumentsQuery
    }
  };

  protected virtual void _(PX.Data.Events.RowSelected<FinPeriod> e)
  {
    ((PXAction) this.ShowUnpostedDocuments).SetEnabled(this.SelectedItems.Any<FinPeriod>());
    if (PXAccess.FeatureInstalled<FeaturesSet.branch>() && !PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>())
      return;
    FinPeriod row = e.Row;
    if (row == null)
      return;
    int num;
    if (row.Selected.GetValueOrDefault())
      num = ((PXSelectBase) this.UnpostedDocuments).View.SelectSingleBound((object[]) new FinPeriod[1]
      {
        row
      }, Array.Empty<object>()) != null ? 1 : 0;
    else
      num = 0;
    Exception exception = num != 0 ? (Exception) new PXSetPropertyException("There are documents pending posting of inventory transactions to the closed period. Review the Unposted IN report (IN656500) for details.", (PXErrorLevel) 3) : (Exception) null;
    ((PXSelectBase) this.FinPeriods).Cache.RaiseExceptionHandling<FinPeriod.selected>((object) row, (object) null, exception);
  }
}
