// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.QuoteMaint_VisibilityRestriction
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

public class QuoteMaint_VisibilityRestriction : PXGraphExtension<QuoteMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0, BqlField = typeof (CROpportunityRevision.branchID))]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<CRQuote.branchID>, IsNotNull>, Null, Case<Where<CRQuote.locationID, IsNotNull, And<Selector<CRQuote.locationID, Location.cBranchID>, IsNotNull>>, Selector<CRQuote.locationID, Location.cBranchID>, Case<Where<CRQuote.bAccountID, IsNotNull, And<Not<Selector<CRQuote.bAccountID, BAccount.cOrgBAccountID>, RestrictByBranch<Current2<CRQuote.branchID>>>>>, Null, Case<Where<Current2<CRQuote.branchID>, IsNotNull>, Current2<CRQuote.branchID>>>>>, Current<AccessInfo.branchID>>))]
  public virtual void _(PX.Data.Events.CacheAttached<CRQuote.branchID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictCustomerByBranch(typeof (BAccount.cOrgBAccountID), typeof (CRQuote.branchID))]
  public virtual void _(PX.Data.Events.CacheAttached<CRQuote.bAccountID> e)
  {
  }

  [PXOverride]
  public void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers,
    QuoteMaint_VisibilityRestriction.CopyPasteGetScriptDelegate baseMethod)
  {
    baseMethod(isImportSimple, script, containers);
    string str1 = "QuoteCurrent: 2";
    string str2 = "Quote";
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
