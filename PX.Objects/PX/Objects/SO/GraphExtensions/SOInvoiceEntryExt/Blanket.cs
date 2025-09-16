// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.Blanket
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.SO.Attributes;
using PX.Objects.SO.DAC.Projections;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

public class Blanket : PXGraphExtension<SOInvoiceEntry>
{
  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    GraphHelper.EnsureCachePersistence<BlanketSOOrder>((PXGraph) this.Base);
    GraphHelper.EnsureCachePersistence<BlanketSOOrderSite>((PXGraph) this.Base);
    GraphHelper.EnsureCachePersistence<BlanketSOLine>((PXGraph) this.Base);
    GraphHelper.EnsureCachePersistence<BlanketSOLineSplit>((PXGraph) this.Base);
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<BlanketSOLine, Where<BlanketSOLine.orderType, Equal<Current<ARTran.blanketType>>, And<BlanketSOLine.orderNbr, Equal<Current<ARTran.blanketNbr>>, And<BlanketSOLine.lineNbr, Equal<Current<ARTran.blanketLineNbr>>>>>>), LeaveChildren = true)]
  public virtual void _(PX.Data.Events.CacheAttached<ARTran.blanketLineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (BaseBilledQtyFormula), typeof (AddCalc<BlanketSOLine.baseBilledQty>))]
  [PXUnboundFormula(typeof (BaseBilledQtyFormula), typeof (SubCalc<BlanketSOLine.baseUnbilledQty>))]
  public virtual void _(PX.Data.Events.CacheAttached<ARTran.baseQty> e)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<BlanketSOLine.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Current<BlanketSOLine.orderNbr>>>>>))]
  public virtual void _(PX.Data.Events.CacheAttached<BlanketSOLine.orderNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (BqlOperand<BlanketSOLine.unbilledQty, IBqlDecimal>.Multiply<BlanketSOLine.lineSign>), typeof (SumCalc<PX.Objects.SO.SOOrder.unbilledOrderQty>))]
  public virtual void _(PX.Data.Events.CacheAttached<BlanketSOLine.unbilledQty> e)
  {
  }

  [PXMergeAttributes]
  [BlanketSOUnbilledTax(typeof (PX.Objects.SO.SOOrder), typeof (SOTax), typeof (SOTaxTran), Inventory = typeof (BlanketSOLine.inventoryID), UOM = typeof (BlanketSOLine.uOM), LineQty = typeof (BlanketSOLine.unbilledQty))]
  public virtual void _(
    PX.Data.Events.CacheAttached<BlanketSOLine.taxCategoryID> e)
  {
  }
}
