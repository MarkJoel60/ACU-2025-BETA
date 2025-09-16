// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APIntegrityCheck
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.SM;
using System.Collections;

#nullable disable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class APIntegrityCheck : PXGraph<APIntegrityCheck>
{
  public PXFilter<APIntegrityCheckFilter> Filter;
  public PXCancel<APIntegrityCheckFilter> Cancel;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<Vendor, APIntegrityCheckFilter, Where<Match<Current<AccessInfo.userName>>>> APVendorList;
  public PXSelect<Vendor, Where<Vendor.vendorClassID, Equal<Current<APIntegrityCheckFilter.vendorClassID>>, PX.Data.And<Match<Current<AccessInfo.userName>>>>> APVendorList_ByVendorClassID;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXAction<APIntegrityCheckFilter> viewVendor;

  public APIntegrityCheck()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    this.APVendorList.SetProcessTooltip("Recalculate balances of vendors and vendor documents");
    this.APVendorList.SetProcessAllTooltip("Recalculate balances of vendors and vendor documents");
  }

  protected virtual void APIntegrityCheckFilter_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    bool flag1 = PXUIFieldAttribute.GetErrors(sender, (object) null, PXErrorLevel.Error, PXErrorLevel.RowError).Count > 0;
    this.APVendorList.SetProcessEnabled(!flag1);
    this.APVendorList.SetProcessAllEnabled(!flag1);
    this.APVendorList.SuppressMerge = true;
    this.APVendorList.SuppressUpdate = true;
    APIntegrityCheckFilter filter = this.Filter.Current;
    this.APVendorList.SetProcessDelegate<APReleaseProcess>((PXProcessingBase<Vendor>.ProcessItemDelegate<APReleaseProcess>) ((re, vend) =>
    {
      re.Clear(PXClearOption.PreserveTimeStamp);
      re.IntegrityCheckProc(vend, filter.FinPeriodID);
      APIntegrityCheck.ReopenDocumentsHavingPendingApplications((PXGraph) re, vend, filter.FinPeriodID);
    }));
    this.APVendorList.SetParametersDelegate((PXProcessingBase<Vendor>.ParametersDelegate) (list =>
    {
      bool flag2 = true;
      if (PXContext.GetSlot<AUSchedule>() == null && list.Count > 5)
        flag2 = this.APVendorList.Ask("Validation of balances for multiple vendors may take a significant amount of time. We recommend that you select a particular vendor for balance validation to reduce time of processing. To proceed with the current settings, click OK. To select a particular vendor, click Cancel.", MessageButtons.OKCancel) == WebDialogResult.OK;
      return flag2;
    }));
  }

  public virtual IEnumerable apvendorlist()
  {
    return this.Filter.Current != null && this.Filter.Current.VendorClassID != null ? (IEnumerable) this.APVendorList_ByVendorClassID.Select() : (IEnumerable) null;
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable ViewVendor(PXAdapter adapter)
  {
    VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
    instance.BAccount.Current = (VendorR) PXSelectBase<VendorR, PXSelect<VendorR, Where<VendorR.bAccountID, Equal<Current<Vendor.bAccountID>>>>.Config>.Select((PXGraph) this);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Vendor");
    requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
    throw requiredException;
  }

  private static void ReopenDocumentsHavingPendingApplications(
    PXGraph graph,
    Vendor vendor,
    string finPeriod)
  {
    PXUpdate<Set<APRegister.openDoc, PX.Data.True>, APRegister, Where<APRegister.openDoc, Equal<False>, And<APRegister.vendorID, Equal<Required<APRegister.vendorID>>, And<APRegister.tranPeriodID, GreaterEqual<Required<APRegister.tranPeriodID>>, PX.Data.And<Exists<PX.Data.Select<APAdjust, Where<APAdjust.released, Equal<False>, And<APAdjust.adjgDocType, Equal<APRegister.docType>, And<APAdjust.adjgRefNbr, Equal<APRegister.refNbr>>>>>>>>>>>.Update(graph, (object) vendor.BAccountID, (object) finPeriod);
    PXUpdate<Set<APRegister.status, APDocStatus.open>, APRegister, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.status, Equal<APDocStatus.closed>>>>, PX.Data.And<BqlOperand<APRegister.vendorID, IBqlInt>.IsEqual<P.AsInt>>>, PX.Data.And<BqlOperand<APRegister.tranPeriodID, IBqlString>.IsGreaterEqual<P.AsString.ASCII>>>, PX.Data.And<BqlOperand<APRegister.openDoc, IBqlBool>.IsEqual<PX.Data.True>>>>.And<BqlOperand<APRegister.docType, IBqlString>.IsNotEqual<APDocType.prepaymentInvoice>>>>.Update(graph, (object) vendor.BAccountID, (object) finPeriod);
  }
}
