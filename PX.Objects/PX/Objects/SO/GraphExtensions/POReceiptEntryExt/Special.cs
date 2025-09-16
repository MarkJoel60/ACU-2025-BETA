// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.POReceiptEntryExt.Special
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.POReceiptEntryExt;

public class Special : PXGraphExtension<PX.Objects.PO.GraphExtensions.POReceiptEntryExt.SO2POSync, POReceiptEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.SO2POSync.UpdateSOOrderLink(PX.Objects.IN.INTran,PX.Objects.PO.POLine,PX.Objects.PO.POReceiptLine)" />
  [PXOverride]
  public void UpdateSOOrderLink(
    INTran newtran,
    PX.Objects.PO.POLine poLine,
    PX.Objects.PO.POReceiptLine line,
    Action<INTran, PX.Objects.PO.POLine, PX.Objects.PO.POReceiptLine> base_UpdateSOOrderLink)
  {
    base_UpdateSOOrderLink(newtran, poLine, line);
    if (poLine != null && poLine.IsSpecialOrder.GetValueOrDefault())
    {
      newtran.IsSpecialOrder = new bool?(true);
      PX.Objects.SO.SOLineSplit soLineSplit = PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.pOType, Equal<BqlField<PX.Objects.PO.POLine.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POLine.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.pOLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POLine.lineNbr, IBqlInt>.FromCurrent>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[1]
      {
        (object) poLine
      }, Array.Empty<object>()));
      newtran.SOOrderType = soLineSplit != null ? soLineSplit.OrderType : throw new RowNotFoundException((PXCache) this.Base1.SOSplitCache, new object[3]
      {
        (object) poLine.OrderType,
        (object) poLine.OrderNbr,
        (object) poLine.LineNbr
      });
      newtran.SOOrderNbr = soLineSplit.OrderNbr;
      newtran.SOOrderLineNbr = soLineSplit.LineNbr;
    }
    else
    {
      if (!line.IsSpecialOrder.GetValueOrDefault() || !(line.ReceiptType == "RX"))
        return;
      INCostCenter inCostCenter = INCostCenter.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, line.CostCenterID);
      if (inCostCenter == null)
        throw new RowNotFoundException((PXCache) GraphHelper.Caches<INCostCenter>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base), new object[1]
        {
          (object) line.CostCenterID
        });
      newtran.IsSpecialOrder = new bool?(true);
      newtran.SOOrderType = inCostCenter.SOOrderType;
      newtran.SOOrderNbr = inCostCenter.SOOrderNbr;
      newtran.SOOrderLineNbr = inCostCenter.SOOrderLineNbr;
    }
  }
}
