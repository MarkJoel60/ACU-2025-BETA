// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseReleaseProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using PX.SM;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CR;

[TableAndChartDashboardType]
public class CRCaseReleaseProcess : PXGraph<CRCaseReleaseProcess>
{
  public PXFilter<CaseFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (CaseFilter))]
  public PXFilteredProcessingJoin<CRCase, CaseFilter, InnerJoin<PX.Objects.AR.Customer, On<CRCase.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>, LeftJoin<PX.Objects.CT.Contract, On<PX.Objects.CT.Contract.contractID, Equal<CRCase.contractID>>, LeftJoin<CRCaseClass, On<CRCaseClass.caseClassID, Equal<CRCase.caseClassID>>>>>, Where<CRCase.isBillable, Equal<True>, And<CRCase.isActive, Equal<False>, And2<Where<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perCase>, Or<CRCaseClass.perItemBilling, IsNull>>, And2<Where<CRCase.released, NotEqual<True>, Or<CRCase.released, IsNull>>, And2<Where<Current<CaseFilter.caseClassID>, IsNull, Or<CRCase.caseClassID, Equal<Current<CaseFilter.caseClassID>>>>, And2<Where<Current<CaseFilter.customerClassID>, IsNull, Or<PX.Objects.AR.Customer.customerClassID, Equal<Current<CaseFilter.customerClassID>>>>, And2<Where<Current<CaseFilter.customerID>, IsNull, Or<PX.Objects.AR.Customer.bAccountID, Equal<Current<CaseFilter.customerID>>>>, And2<Where<Current<CaseFilter.contractID>, IsNull, Or<PX.Objects.CT.Contract.contractID, Equal<Current<CaseFilter.contractID>>>>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>>>>>> Items;
  [PXHidden]
  public PXSelect<BAccount> BaseAccounts;
  [PXHidden]
  public PXSelect<CRCase> BaseCases;
  public PXCancel<CaseFilter> Cancel;

  public CRCaseReleaseProcess()
  {
    ((PXProcessingBase<CRCase>) this.Items).SetSelected<CRCase.selected>();
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Items).Cache, (string) null, false);
    PXUIFieldAttribute.SetVisible<CRCase.selected>(((PXSelectBase) this.Items).Cache, (object) null);
    PXUIFieldAttribute.SetVisible<CRCase.caseCD>(((PXSelectBase) this.Items).Cache, (object) null);
    PXUIFieldAttribute.SetVisible<CRCase.subject>(((PXSelectBase) this.Items).Cache, (object) null);
    PXUIFieldAttribute.SetVisible<CRCase.contractID>(((PXSelectBase) this.Items).Cache, (object) null);
    PXUIFieldAttribute.SetVisible<CRCase.timeBillable>(((PXSelectBase) this.Items).Cache, (object) null);
    PXUIFieldAttribute.SetVisible<CRCase.overtimeBillable>(((PXSelectBase) this.Items).Cache, (object) null);
    PXCache cach = ((PXGraph) this).Caches[typeof (PX.Objects.AR.Customer)];
    PXUIFieldAttribute.SetVisible(cach, (string) null, false);
    PXUIFieldAttribute.SetDisplayName<PX.Objects.AR.Customer.acctName>(cach, "Business Account Name");
    PXUIFieldAttribute.SetDisplayName<BAccount.classID>(cach, "Business Account Class");
    ((PXGraph) this).Actions.Move("Process", nameof (Cancel));
  }

  [CRCaseBillableTime]
  [PXUIField(DisplayName = "Billable Time", Enabled = false)]
  [PXMergeAttributes]
  public virtual void CRCase_TimeBillable_CacheAttached(PXCache sender)
  {
  }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Billable Overtime")]
  public virtual void CRCase_OvertimeBillable_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<CaseFilter> e)
  {
    ((PXProcessingBase<CRCase>) this.Items).SetProcessWorkflowAction<CRCaseMaint>((Expression<Func<CRCaseMaint, PXAction>>) (g => g.release));
  }
}
