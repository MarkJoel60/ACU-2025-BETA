// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.BillingProcessVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM.Billing;

#nullable disable
namespace PX.Objects.PM;

public class BillingProcessVisibilityRestriction : PXGraphExtension<BillingProcess>
{
  [InjectDependency]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<Contract>) this.Base.ProjectsUnbilled).WhereAnd<Where<Contract.defaultBranchID, IsNotNull, Or<Where<Contract.defaultBranchID, IsNull, And<PX.Objects.AR.Customer.cOrgBAccountID, RestrictByBranch<Current<AccessInfo.branchID>>>>>>>();
    ((PXSelectBase<Contract>) this.Base.ProjectsUbilledCutOffDateExcluded).WhereAnd<Where<Contract.defaultBranchID, IsNotNull, Or<Where<Contract.defaultBranchID, IsNull, And<PX.Objects.AR.Customer.cOrgBAccountID, RestrictByBranch<Current<AccessInfo.branchID>>>>>>>();
    ((PXSelectBase<Contract>) this.Base.ProjectsProgressive).WhereAnd<Where<Contract.defaultBranchID, IsNotNull, Or<Where<Contract.defaultBranchID, IsNull, And<PX.Objects.AR.Customer.cOrgBAccountID, RestrictByBranch<Current<AccessInfo.branchID>>>>>>>();
    ((PXSelectBase<Contract>) this.Base.ProjectsRecurring).WhereAnd<Where<Contract.defaultBranchID, IsNotNull, Or<Where<Contract.defaultBranchID, IsNull, And<PX.Objects.AR.Customer.cOrgBAccountID, RestrictByBranch<Current<AccessInfo.branchID>>>>>>>();
  }

  public void _(PX.Data.Events.RowSelected<BillingProcess.BillingFilter> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<Contract>) this.Base.Items).SetProcessDelegate<PMBillEngine>(new PXProcessingBase<Contract>.ProcessItemDelegate<PMBillEngine>((object) new BillingProcessVisibilityRestriction.\u003C\u003Ec__DisplayClass6_0()
    {
      filter = ((PXSelectBase<BillingProcess.BillingFilter>) this.Base.Filter).Current,
      branch = this._currentUserInformationProvider.GetBranchCD()
    }, __methodptr(\u003C_\u003Eb__0)));
  }

  private string GetCustomerName(int? customerId)
  {
    if (!customerId.HasValue)
      return (string) null;
    return PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) customerId
    }))?.AcctName;
  }
}
