// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashFlowEnqVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CA;

public sealed class CashFlowEnqVisibilityRestriction : PXCacheExtension<CashFlowEnq.CashFlowFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.branch>();

  [PXRestrictor(typeof (Where2<Where<CashAccount.restrictVisibilityWithBranch, Equal<True>, And<CashAccount.branchID, InsideBranchesOf<Current<CashFlowEnq.CashFlowFilter.orgBAccountID>>>>, Or<Where<CashAccount.restrictVisibilityWithBranch, Equal<False>, And<CashAccount.baseCuryID, Equal<Current<CashFlowEnq.CashFlowFilter.organizationBaseCuryID>>>>>>), "", new Type[] {}, SuppressVerify = false)]
  [PXMergeAttributes]
  public int? CashAccountID { get; set; }

  [PXRestrictor(typeof (Where2<Where<CashAccount.restrictVisibilityWithBranch, Equal<True>, And<CashAccount.branchID, InsideBranchesOf<Current<CashFlowEnq.CashFlowFilter.orgBAccountID>>>>, Or<Where<CashAccount.restrictVisibilityWithBranch, Equal<False>, And<CashAccount.baseCuryID, Equal<Current<CashFlowEnq.CashFlowFilter.organizationBaseCuryID>>>>>>), "", new Type[] {}, SuppressVerify = false)]
  [PXMergeAttributes]
  public int? DefaultAccountID { get; set; }
}
