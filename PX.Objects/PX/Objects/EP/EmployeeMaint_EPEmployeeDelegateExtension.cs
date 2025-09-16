// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EmployeeMaint_EPEmployeeDelegateExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.Extension;
using System;

#nullable disable
namespace PX.Objects.EP;

/// <exclude />
public class EmployeeMaint_EPEmployeeDelegateExtension : EPEmployeeDelegateExtension<EmployeeMaint>
{
  public static bool IsActive() => EPEmployeeDelegateExtension<EmployeeMaint>.IsExtensionActive();

  public override void Initialize()
  {
    base.Initialize();
    this.Delegates.WhereNew<Where2<Where<BqlOperand<EPWingman.employeeID, IBqlInt>.IsEqual<BqlField<EPEmployee.bAccountID, IBqlInt>.FromCurrent>>, And2<Where<EPWingman.delegationOf, NotEqual<EPDelegationOf.approvals>, Or<FeatureInstalled<FeaturesSet.approvalWorkflow>>>, And2<Where<EPWingman.delegationOf, NotEqual<EPDelegationOf.expenses>, Or<FeatureInstalled<FeaturesSet.expenseManagement>>>, And<Where<EPWingman.delegationOf, NotEqual<EPDelegationOf.timeEntries>, Or2<FeatureInstalled<FeaturesSet.financialAdvanced>, Or<FeatureInstalled<FeaturesSet.timeReportingModule>>>>>>>>>();
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRestrictor(typeof (Where<EPEmployee.bAccountID, NotEqual<Current<EPEmployee.bAccountID>>>), null, new Type[] {})]
  protected virtual void _(Events.CacheAttached<EPWingman.wingmanID> e)
  {
  }
}
