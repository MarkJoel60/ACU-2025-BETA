// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ARDocGraphCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;

#nullable disable
namespace PX.Objects.GL;

public class ARDocGraphCreator : IDocGraphCreator
{
  public virtual PXGraph Create(GLTran tran) => this.Create(tran.TranType, tran.RefNbr, new int?());

  public virtual PXGraph Create(string aTranType, string aRefNbr, int? referenceID)
  {
    PXGraph pxGraph = (PXGraph) null;
    bool? nullable = ARDocType.Payable(aTranType);
    if ((aTranType == "CSL" ? 1 : (aTranType == "RCS" ? 1 : 0)) != 0)
    {
      ARCashSaleEntry instance = PXGraph.CreateInstance<ARCashSaleEntry>();
      ((PXSelectBase<ARCashSale>) instance.Document).Current = PXResultset<ARCashSale>.op_Implicit(((PXSelectBase<ARCashSale>) instance.Document).Search<PX.Objects.AR.ARRegister.refNbr>((object) aRefNbr, new object[1]
      {
        (object) aTranType
      }));
      pxGraph = (PXGraph) instance;
    }
    else if (nullable.HasValue)
    {
      if (nullable.Value)
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARRegister.refNbr>((object) aRefNbr, new object[1]
        {
          (object) aTranType
        }));
        pxGraph = (PXGraph) instance;
      }
      else
      {
        ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
        ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Search<PX.Objects.AR.ARRegister.refNbr>((object) aRefNbr, new object[1]
        {
          (object) aTranType
        }));
        pxGraph = (PXGraph) instance;
      }
    }
    return pxGraph;
  }
}
