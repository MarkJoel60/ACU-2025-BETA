// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.CADocGraphCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using System;

#nullable disable
namespace PX.Objects.GL;

public class CADocGraphCreator : IDocGraphCreator
{
  public virtual PXGraph Create(GLTran tran) => this.Create(tran.TranType, tran.RefNbr, new int?());

  public virtual PXGraph Create(string aTranType, string aRefNbr, int? referenceID)
  {
    if (aTranType != null && aTranType.Length == 3)
    {
      switch (aTranType[2])
      {
        case 'D':
          if (aTranType == "CVD")
            goto label_10;
          goto label_11;
        case 'E':
          switch (aTranType)
          {
            case "CTE":
              break;
            case "CAE":
              CATranEntry instance1 = PXGraph.CreateInstance<CATranEntry>();
              ((PXSelectBase<CAAdj>) instance1.CAAdjRecords).Current = PXResultset<CAAdj>.op_Implicit(((PXSelectBase<CAAdj>) instance1.CAAdjRecords).Search<CAAdj.adjRefNbr, CAAdj.adjTranType>((object) aRefNbr, (object) aTranType, Array.Empty<object>()));
              return (PXGraph) instance1;
            default:
              goto label_11;
          }
          break;
        case 'G':
          if (aTranType == "CTG")
            break;
          goto label_11;
        case 'I':
          if (aTranType == "CTI")
            break;
          goto label_11;
        case 'O':
          if (aTranType == "CTO")
            break;
          goto label_11;
        case 'T':
          if (aTranType == "CDT")
            goto label_10;
          goto label_11;
        default:
          goto label_11;
      }
      CashTransferEntry instance2 = PXGraph.CreateInstance<CashTransferEntry>();
      ((PXSelectBase<CATransfer>) instance2.Transfer).Current = PXResultset<CATransfer>.op_Implicit(((PXSelectBase<CATransfer>) instance2.Transfer).Search<CATransfer.transferNbr>((object) aRefNbr, Array.Empty<object>()));
      return (PXGraph) instance2;
label_10:
      CADepositEntry instance3 = PXGraph.CreateInstance<CADepositEntry>();
      ((PXSelectBase<CADeposit>) instance3.Document).Current = CADeposit.PK.Find((PXGraph) instance3, aTranType, aRefNbr);
      return (PXGraph) instance3;
    }
label_11:
    return (PXGraph) null;
  }
}
