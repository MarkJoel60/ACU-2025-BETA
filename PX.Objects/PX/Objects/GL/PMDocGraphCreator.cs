// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.PMDocGraphCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.GL;

public class PMDocGraphCreator : IDocGraphCreator
{
  public virtual PXGraph Create(string aTranType, string aRefNbr, int? referenceID)
  {
    throw new PXException("This interface method is not supported for this document type.");
  }

  public virtual PXGraph Create(GLTran tran)
  {
    if (tran.PMTranID.HasValue)
    {
      RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
      PMTran pmTran = PMTran.PK.Find((PXGraph) instance, tran.PMTranID);
      if (pmTran != null)
      {
        ((PXSelectBase<PMRegister>) instance.Document).Current = PXResultset<PMRegister>.op_Implicit(PXSelectBase<PMRegister, PXSelect<PMRegister, Where<PMRegister.module, Equal<Required<PMRegister.module>>, And<PMRegister.refNbr, Equal<Required<PMRegister.refNbr>>>>>.Config>.Select((PXGraph) instance, new object[2]
        {
          (object) pmTran.TranType,
          (object) pmTran.RefNbr
        }));
        return (PXGraph) instance;
      }
    }
    return (PXGraph) null;
  }
}
