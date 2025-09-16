// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostDocEntryVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PO.LandedCosts;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO;

public class POLandedCostDocEntryVisibilityRestriction : PXGraphExtension<POLandedCostDocEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXOverride]
  public void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers,
    POLandedCostDocEntryVisibilityRestriction.CopyPasteGetScriptDelegate baseMethod)
  {
    baseMethod(isImportSimple, script, containers);
    Utilities.SetFieldCommandToTheTop(script, containers, "CurrentDocument", "BranchID");
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<POReceipt>) this.Base.poReceiptSelectionView).Join<InnerJoin<BAccount2, On<BqlOperand<POReceipt.vendorID, IBqlInt>.IsEqual<BAccount2.bAccountID>>>>();
    ((PXSelectBase<POReceipt>) this.Base.poReceiptSelectionView).WhereAnd<Where<BAccount2.vOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>();
    ((PXSelectBase<POReceiptLineAdd>) this.Base.poReceiptLinesSelectionView).Join<InnerJoin<BAccount2, On<BqlOperand<POReceiptLineAdd.vendorID, IBqlInt>.IsEqual<BAccount2.bAccountID>>>>();
    ((PXSelectBase<POReceiptLineAdd>) this.Base.poReceiptLinesSelectionView).WhereAnd<Where<BAccount2.vOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>();
  }

  public delegate void CopyPasteGetScriptDelegate(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers);
}
