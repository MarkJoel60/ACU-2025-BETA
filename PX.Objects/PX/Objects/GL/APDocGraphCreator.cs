// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.APDocGraphCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;

#nullable disable
namespace PX.Objects.GL;

public class APDocGraphCreator : IDocGraphCreator
{
  public virtual PXGraph Create(GLTran tran) => this.Create(tran.TranType, tran.RefNbr, new int?());

  public virtual PXGraph Create(string aTranType, string aRefNbr, int? referenceID)
  {
    PXGraph pxGraph = (PXGraph) null;
    bool? nullable = APDocType.Payable(aTranType);
    if ((aTranType == "QCK" || aTranType == "VQC" ? 1 : (aTranType == "RQC" ? 1 : 0)) != 0)
    {
      APQuickCheckEntry instance = PXGraph.CreateInstance<APQuickCheckEntry>();
      ((PXSelectBase<APQuickCheck>) instance.Document).Current = PXResultset<APQuickCheck>.op_Implicit(((PXSelectBase<APQuickCheck>) instance.Document).Search<PX.Objects.AP.APRegister.refNbr>((object) aRefNbr, new object[1]
      {
        (object) aTranType
      }));
      pxGraph = (PXGraph) instance;
    }
    else if (nullable.HasValue)
    {
      if (nullable.Value)
      {
        APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Search<PX.Objects.AP.APRegister.refNbr>((object) aRefNbr, new object[1]
        {
          (object) aTranType
        }));
        pxGraph = (PXGraph) instance;
      }
      else
      {
        APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
        ((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Current = PXResultset<PX.Objects.AP.APPayment>.op_Implicit(((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Search<PX.Objects.AP.APRegister.refNbr>((object) aRefNbr, new object[1]
        {
          (object) aTranType
        }));
        pxGraph = (PXGraph) instance;
      }
    }
    return pxGraph;
  }
}
