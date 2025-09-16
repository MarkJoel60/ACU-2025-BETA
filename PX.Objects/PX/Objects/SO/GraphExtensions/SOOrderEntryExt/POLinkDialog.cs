// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.POLinkDialog
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class POLinkDialog : PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>
{
  public PXSelect<SOLine, Where<SOLine.orderType, Equal<Optional<SOLine.orderType>>, And<SOLine.orderNbr, Equal<Optional<SOLine.orderNbr>>, And<SOLine.lineNbr, Equal<Optional<SOLine.lineNbr>>>>>> SOLineDemand;
  [PXCopyPasteHiddenView]
  public PXSelect<SupplyPOLine> SupplyPOLines;
  public PXAction<SOOrder> pOSupplyOK;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.sOToPOLink>() || PXAccess.FeatureInstalled<FeaturesSet.dropShipments>() || PXAccess.FeatureInstalled<FeaturesSet.purchaseRequisitions>();
  }

  public virtual IEnumerable supplyPOLines()
  {
    SOLine currentSOLine = PXResultset<SOLine>.op_Implicit(((PXSelectBase<SOLine>) this.SOLineDemand).Select(Array.Empty<object>())) ?? ((PXSelectBase<SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current;
    if (currentSOLine == null || currentSOLine.IsLegacyDropShip.GetValueOrDefault())
      return (IEnumerable) new List<SupplyPOLine>(0);
    List<SupplyPOLine> supplyPOLines = new List<SupplyPOLine>();
    this.CollectSupplyPOLines(currentSOLine, (ICollection<SupplyPOLine>) supplyPOLines);
    return (IEnumerable) supplyPOLines;
  }

  public virtual void CollectSupplyPOLines(
    SOLine currentSOLine,
    ICollection<SupplyPOLine> supplyPOLines)
  {
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntryF", VisibleOnDataSource = false, CommitChanges = true)]
  protected virtual IEnumerable POSupplyOK(PXAdapter adapter)
  {
    SOLine currentSOLine = PXResultset<SOLine>.op_Implicit(((PXSelectBase<SOLine>) this.SOLineDemand).Select(Array.Empty<object>()));
    if (currentSOLine != null && currentSOLine.POCreate.GetValueOrDefault())
    {
      PXSelect<SOLine, Where<SOLine.orderType, Equal<Optional<SOLine.orderType>>, And<SOLine.orderNbr, Equal<Optional<SOLine.orderNbr>>, And<SOLine.lineNbr, Equal<Optional<SOLine.lineNbr>>>>>> soLineDemand = this.SOLineDemand;
      POLinkDialog poLinkDialog = this;
      // ISSUE: virtual method pointer
      PXView.InitializePanel initializePanel = new PXView.InitializePanel((object) poLinkDialog, __vmethodptr(poLinkDialog, POSupplyDialogInitializer));
      if (((PXSelectBase<SOLine>) soLineDemand).AskExt(initializePanel) == 1)
        this.LinkPOSupply(currentSOLine);
    }
    return adapter.Get();
  }

  public virtual void POSupplyDialogInitializer(PXGraph graph, string viewName)
  {
    foreach (SupplyPOLine supplyPoLine in ((PXSelectBase) this.SupplyPOLines).Cache.Updated)
      supplyPoLine.SelectedSOLines = supplyPoLine.LinkedSOLines.SparseArrayCopy<int>();
  }

  public virtual void LinkPOSupply(SOLine currentSOLine)
  {
  }
}
