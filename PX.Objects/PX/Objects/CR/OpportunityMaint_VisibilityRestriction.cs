// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OpportunityMaint_VisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public class OpportunityMaint_VisibilityRestriction : PXGraphExtension<OpportunityMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0, BqlField = typeof (CROpportunityRevision.branchID))]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<CROpportunity.branchID>, IsNotNull>, Null, Case<Where<CROpportunity.locationID, IsNotNull, And<Selector<CROpportunity.locationID, Location.cBranchID>, IsNotNull>>, Selector<CROpportunity.locationID, Location.cBranchID>, Case<Where<CROpportunity.bAccountID, IsNotNull, And<Not<Selector<CROpportunity.bAccountID, BAccount.cOrgBAccountID>, RestrictByBranch<Current2<CROpportunity.branchID>>>>>, Null, Case<Where<Current2<CROpportunity.branchID>, IsNotNull>, Current2<CROpportunity.branchID>>>>>, Current<AccessInfo.branchID>>))]
  public virtual void _(PX.Data.Events.CacheAttached<CROpportunity.branchID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictCustomerByBranch(typeof (BAccount.cOrgBAccountID), typeof (CROpportunity.branchID))]
  public virtual void _(PX.Data.Events.CacheAttached<CROpportunity.bAccountID> e)
  {
  }

  [PXOverride]
  public void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers,
    OpportunityMaint_VisibilityRestriction.CopyPasteGetScriptDelegate baseMethod)
  {
    baseMethod(isImportSimple, script, containers);
    string str1 = "OpportunityCurrent";
    string str2 = "Opportunity";
    (string, string) firstField = ("BranchID", str1);
    PX.Objects.Common.Utilities.SetDependentFieldsAfterBranch(script, firstField, new List<(string, string)>()
    {
      ("BAccountID", str2),
      ("LocationID", str2)
    });
  }

  public delegate void CopyPasteGetScriptDelegate(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers);
}
