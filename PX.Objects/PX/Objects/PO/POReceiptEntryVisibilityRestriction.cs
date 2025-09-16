// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptEntryVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Formula;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO;

public class POReceiptEntryVisibilityRestriction : PXGraphExtension<POReceiptEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXOverride]
  public void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers,
    POReceiptEntryVisibilityRestriction.CopyPasteGetScriptDelegate baseMethod)
  {
    baseMethod(isImportSimple, script, containers);
    Utilities.SetFieldCommandToTheTop(script, containers, "CurrentDocument", "BranchID");
  }

  [PXMergeAttributes]
  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<IsCopyPasteContext, Equal<True>, And<Current2<POReceipt.branchID>, IsNotNull>>, Current2<POReceipt.branchID>, Case<Where<POReceipt.receiptType, Equal<POReceiptType.transferreceipt>>, Selector<POReceipt.siteID, INSite.branchID>, Case<Where<POReceipt.vendorLocationID, IsNotNull, And<Selector<POReceipt.vendorLocationID, PX.Objects.CR.Location.vBranchID>, IsNotNull>>, Selector<POReceipt.vendorLocationID, PX.Objects.CR.Location.vBranchID>, Case<Where<POReceipt.vendorID, IsNotNull, And<Not<Selector<POReceipt.vendorID, PX.Objects.AP.Vendor.vOrgBAccountID>, RestrictByBranch<Current2<POReceipt.branchID>>>>>, Null, Case<Where<Current2<POReceipt.branchID>, IsNotNull>, Current2<POReceipt.branchID>>>>>>, Current<AccessInfo.branchID>>))]
  public virtual void POReceipt_BranchID_CacheAttached(PXCache sender)
  {
  }

  public delegate void CopyPasteGetScriptDelegate(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers);
}
