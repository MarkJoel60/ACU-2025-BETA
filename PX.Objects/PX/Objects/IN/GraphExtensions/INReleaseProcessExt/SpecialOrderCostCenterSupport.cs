// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INReleaseProcessExt.SpecialOrderCostCenterSupport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.InventoryRelease.DAC;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INReleaseProcessExt;

public class SpecialOrderCostCenterSupport : PXGraphExtension<INReleaseProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.UpdateSplitDestinationLocation(PX.Objects.IN.INTran,PX.Objects.IN.INTranSplit,System.Nullable{System.Int32})" />
  /// </summary>
  [PXOverride]
  public virtual void UpdateSplitDestinationLocation(
    INTran tran,
    INTranSplit split,
    int? value,
    Action<INTran, INTranSplit, int?> baseMethod)
  {
    baseMethod(tran, split, value);
    if (!split.SkipCostUpdate.GetValueOrDefault() || !EnumerableExtensions.IsIn<string>("S", tran.CostLayerType, tran.ToCostLayerType))
      return;
    int? costCenterId = tran.CostCenterID;
    int? toCostCenterId = tran.ToCostCenterID;
    if (costCenterId.GetValueOrDefault() == toCostCenterId.GetValueOrDefault() & costCenterId.HasValue == toCostCenterId.HasValue)
      return;
    split.SkipCostUpdate = new bool?(false);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.intransplit).Cache, (object) split, true);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.CreatePositiveOneStepTransferINTran(PX.Objects.IN.INRegister,PX.Objects.IN.INTran,PX.Objects.IN.INTranSplit)" />
  /// </summary>
  [PXOverride]
  public virtual INTran CreatePositiveOneStepTransferINTran(
    INRegister doc,
    INTran tran,
    INTranSplit split,
    Func<INRegister, INTran, INTranSplit, INTran> baseFunc)
  {
    INTran stepTransferInTran = baseFunc(doc, tran, split);
    stepTransferInTran.SpecialOrderCostCenterID = stepTransferInTran.ToCostLayerType == "S" ? stepTransferInTran.ToCostCenterID : new int?();
    stepTransferInTran.IsSpecialOrder = new bool?(stepTransferInTran.ToCostLayerType == "S");
    stepTransferInTran.ToSpecialOrderCostCenterID = new int?();
    return stepTransferInTran;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.UseStandardCost(System.String,PX.Objects.IN.INTran)" />
  /// </summary>
  [PXOverride]
  public virtual bool UseStandardCost(
    string valMethod,
    INTran tran,
    Func<string, INTran, bool> baseFunc)
  {
    return baseFunc(valMethod, tran) && tran.CostLayerType != "S";
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.AccumOversoldCostStatus(PX.Objects.IN.INTran,PX.Objects.IN.INTranSplit,PX.Objects.IN.InventoryItem)" />
  /// </summary>
  [PXOverride]
  public virtual INCostStatus AccumOversoldCostStatus(
    INTran tran,
    INTranSplit split,
    InventoryItem item,
    Func<INTran, INTranSplit, InventoryItem, INCostStatus> baseFunc)
  {
    if (tran.CostLayerType == "S")
      throw new PXException("The quantity of the {0} item allocated for the {1} sales order is not sufficient to process the document. The quantity will become negative.", new object[2]
      {
        (object) item.InventoryCD,
        (object) tran.SOOrderNbr
      });
    return baseFunc(tran, split, item);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.ThrowNegativeQtyException(PX.Objects.IN.INTran,PX.Objects.IN.INTranSplit,PX.Objects.IN.INCostStatus)" />
  /// </summary>
  [PXOverride]
  public virtual void ThrowNegativeQtyException(
    INTran tran,
    INTranSplit split,
    INCostStatus lastLayer,
    Action<INTran, INTranSplit, INCostStatus> baseImpl)
  {
    if (tran.CostLayerType == "S")
      throw new PXException("The quantity of the {0} item allocated for the {1} sales order is not sufficient to process the document. The quantity will become negative.", new object[2]
      {
        (object) InventoryItem.PK.Find((PXGraph) this.Base, tran.InventoryID)?.InventoryCD,
        (object) tran.SOOrderNbr
      });
    baseImpl(tran, split, lastLayer);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.GetTransitCostSiteID(PX.Objects.IN.INTran)" />
  /// </summary>
  [PXOverride]
  public virtual int? GetTransitCostSiteID(INTran tran, Func<INTran, int?> baseMethod)
  {
    if (tran.IsSpecialOrder.GetValueOrDefault() && !this.Base.IsOneStepTransfer())
    {
      if (this.Base.IsIngoingTransfer(tran))
        return tran.CostCenterID;
      if (tran.TranType == "TRX")
        return tran.ToCostCenterID;
    }
    return baseMethod(tran);
  }

  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.Copy(PX.Objects.IN.INTran,PX.Objects.IN.InventoryRelease.DAC.ReadOnlyCostStatus,PX.Objects.IN.InventoryItem)" />
  [PXOverride]
  public virtual INTran Copy(
    INTran tran,
    ReadOnlyCostStatus layer,
    InventoryItem item,
    Func<INTran, ReadOnlyCostStatus, InventoryItem, INTran> base_Copy)
  {
    INTran inTran = base_Copy(tran, layer, item);
    inTran.IsSpecialOrder = tran.IsSpecialOrder;
    return inTran;
  }
}
