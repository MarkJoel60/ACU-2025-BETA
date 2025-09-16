// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.SpecialOrderCostCenterSupport
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
using PX.Objects.SO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class SpecialOrderCostCenterSupport : 
  SpecialOrderCostCenterSupport<POReceiptEntry, POReceiptLine>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();

  public override IEnumerable<Type> GetFieldsDependOn()
  {
    yield return typeof (POReceiptLine.isSpecialOrder);
    yield return typeof (POReceiptLine.siteID);
  }

  public override bool IsSpecificCostCenter(POReceiptLine line)
  {
    return line.IsSpecialOrder.GetValueOrDefault() && line.SiteID.HasValue;
  }

  protected override SpecialOrderCostCenterSupport<POReceiptEntry, POReceiptLine>.CostCenterKeys GetCostCenterKeys(
    POReceiptLine line)
  {
    if (!(line.ReceiptType != "RX"))
      return this.GetTransferCostCenterKeys(line);
    SOLineSplit soLineSplit = PXResultset<SOLineSplit>.op_Implicit(PXSelectBase<SOLineSplit, PXViewOf<SOLineSplit>.BasedOn<SelectFromBase<SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLineSplit.pOType, Equal<BqlField<POReceiptLine.pOType, IBqlString>.FromCurrent.NoDefault>>>>, And<BqlOperand<SOLineSplit.pONbr, IBqlString>.IsEqual<BqlField<POReceiptLine.pONbr, IBqlString>.FromCurrent.NoDefault>>>>.And<BqlOperand<SOLineSplit.pOLineNbr, IBqlInt>.IsEqual<BqlField<POReceiptLine.pOLineNbr, IBqlInt>.FromCurrent.NoDefault>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) line
    }, Array.Empty<object>()));
    if (soLineSplit == null)
      throw new RowNotFoundException(((PXGraph) this.Base).Caches[typeof (SOLineSplit)], new object[3]
      {
        (object) line.POType,
        (object) line.PONbr,
        (object) line.POLineNbr
      });
    return new SpecialOrderCostCenterSupport<POReceiptEntry, POReceiptLine>.CostCenterKeys()
    {
      SiteID = line.SiteID,
      OrderType = soLineSplit.OrderType,
      OrderNbr = soLineSplit.OrderNbr,
      LineNbr = soLineSplit.LineNbr
    };
  }

  protected virtual SpecialOrderCostCenterSupport<POReceiptEntry, POReceiptLine>.CostCenterKeys GetTransferCostCenterKeys(
    POReceiptLine line)
  {
    PX.Objects.SO.SOLine soLine = PX.Objects.SO.SOLine.PK.Find((PXGraph) this.Base, line.SOOrderType, line.SOOrderNbr, line.SOOrderLineNbr);
    if (string.IsNullOrEmpty(soLine?.OrigOrderType) || string.IsNullOrEmpty(soLine.OrigOrderNbr) || !soLine.OrigLineNbr.HasValue)
      throw new PXInvalidOperationException();
    return new SpecialOrderCostCenterSupport<POReceiptEntry, POReceiptLine>.CostCenterKeys()
    {
      SiteID = line.SiteID,
      OrderType = soLine.OrigOrderType,
      OrderNbr = soLine.OrigOrderNbr,
      LineNbr = soLine.OrigLineNbr
    };
  }

  public class AlterAddTransferDialog : PXGraphExtension<AddTransferDialog, POReceiptEntry>
  {
    public static bool IsActive() => SpecialOrderCostCenterSupport.IsActive();

    /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.AddTransferDialog.GetCostStatusCommandCostSiteID(PX.Objects.PO.POReceiptLine)" />
    [PXOverride]
    public int? GetCostStatusCommandCostSiteID(
      POReceiptLine line,
      Func<POReceiptLine, int?> base_GetCostStatusCommandCostSiteID)
    {
      if (line == null || !line.IsSpecialOrder.GetValueOrDefault())
        return base_GetCostStatusCommandCostSiteID(line);
      SpecialOrderCostCenterSupport extension = ((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).GetExtension<SpecialOrderCostCenterSupport>();
      return extension.FindOrCreateCostCenter(extension.GetTransferCostCenterKeys(line)).CostCenterID;
    }
  }
}
