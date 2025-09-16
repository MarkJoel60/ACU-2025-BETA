// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.INDocGraphCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.GL;

public class INDocGraphCreator : IDocGraphCreator
{
  public virtual PXGraph Create(GLTran tran)
  {
    string tranType = tran.TranType;
    string refNbr = tran.RefNbr;
    if (tranType != null && tranType.Length == 3)
    {
      switch (tranType[2])
      {
        case 'A':
          if (tranType == "RCA")
            goto label_14;
          goto label_19;
        case 'C':
          if (tranType == "ASC")
            goto label_14;
          goto label_19;
        case 'I':
          if (tranType == "III")
            break;
          goto label_19;
        case 'J':
          if (tranType == "ADJ")
            goto label_14;
          goto label_19;
        case 'M':
          if (tranType == "DRM" || tranType == "CRM")
            break;
          goto label_19;
        case 'P':
          if (tranType == "RCP")
          {
            INReceiptEntry instance = PXGraph.CreateInstance<INReceiptEntry>();
            ((PXSelectBase<INRegister>) instance.receipt).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) instance.receipt).Search<INRegister.refNbr>((object) refNbr, Array.Empty<object>()));
            return (PXGraph) instance;
          }
          goto label_19;
        case 'T':
          if (tranType == "RET")
            break;
          goto label_19;
        case 'V':
          if (tranType == "INV")
            break;
          goto label_19;
        case 'X':
          if (tranType == "TRX")
          {
            INTransferEntry instance1 = PXGraph.CreateInstance<INTransferEntry>();
            INTran inTran = PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXSelectReadonly<INTran, Where<INTran.tranType, Equal<Required<INTran.tranType>>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) instance1, new object[0], new object[2]
            {
              (object) tran.TranType,
              (object) tran.RefNbr
            }));
            if (inTran != null && inTran.DocType == "R")
            {
              INReceiptEntry instance2 = PXGraph.CreateInstance<INReceiptEntry>();
              ((PXSelectBase<INRegister>) instance2.receipt).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) instance2.receipt).Search<INRegister.refNbr>((object) refNbr, Array.Empty<object>()));
              return (PXGraph) instance2;
            }
            ((PXSelectBase<INRegister>) instance1.transfer).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) instance1.transfer).Search<INRegister.refNbr>((object) refNbr, Array.Empty<object>()));
            return (PXGraph) instance1;
          }
          goto label_19;
        case 'Y':
          if (tranType == "ASY" || tranType == "DSY")
          {
            KitAssemblyEntry instance = PXGraph.CreateInstance<KitAssemblyEntry>();
            ((PXSelectBase<INKitRegister>) instance.Document).Current = PXResultset<INKitRegister>.op_Implicit(((PXSelectBase<INKitRegister>) instance.Document).Search<INKitRegister.refNbr>((object) refNbr, new object[1]
            {
              tranType == "ASY" ? (object) "P" : (object) "D"
            }));
            return (PXGraph) instance;
          }
          goto label_19;
        default:
          goto label_19;
      }
      INIssueEntry instance3 = PXGraph.CreateInstance<INIssueEntry>();
      ((PXSelectBase<INRegister>) instance3.issue).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) instance3.issue).Search<INRegister.refNbr>((object) refNbr, Array.Empty<object>()));
      return (PXGraph) instance3;
label_14:
      INAdjustmentEntry instance4 = PXGraph.CreateInstance<INAdjustmentEntry>();
      ((PXSelectBase<INRegister>) instance4.adjustment).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) instance4.adjustment).Search<INRegister.refNbr>((object) refNbr, Array.Empty<object>()));
      return (PXGraph) instance4;
    }
label_19:
    return (PXGraph) null;
  }

  public virtual PXGraph Create(string aTranType, string aRefNbr, int? referenceID)
  {
    throw new NotImplementedException();
  }
}
