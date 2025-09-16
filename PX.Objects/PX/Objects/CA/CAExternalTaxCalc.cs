// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAExternalTaxCalc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

public class CAExternalTaxCalc : PXGraph<CAExternalTaxCalc>
{
  [PXFilterable(new Type[] {})]
  public PXProcessingJoin<CAAdj, InnerJoin<PX.Objects.TX.TaxZone, On<PX.Objects.TX.TaxZone.taxZoneID, Equal<CAAdj.taxZoneID>>>, Where<PX.Objects.TX.TaxZone.isExternal, Equal<True>, And<CAAdj.isTaxValid, Equal<False>, And<CAAdj.released, Equal<False>>>>> Items;

  public CAExternalTaxCalc()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<CAAdj>) this.Items).SetProcessDelegate(CAExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (CAExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9__1_0 = new PXProcessingBase<CAAdj>.ProcessListDelegate((object) CAExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__1_0))));
  }

  public static CAAdj Process(CAAdj doc)
  {
    return CAExternalTaxCalc.Process(new List<CAAdj>()
    {
      doc
    }, false)[0];
  }

  public static List<CAAdj> Process(List<CAAdj> list, bool isMassProcess)
  {
    List<CAAdj> caAdjList = new List<CAAdj>(list.Count);
    CATranEntry instance = PXGraph.CreateInstance<CATranEntry>();
    for (int index = 0; index < list.Count; ++index)
    {
      try
      {
        ((PXGraph) instance).Clear();
        ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Current = PXResultset<CAAdj>.op_Implicit(PXSelectBase<CAAdj, PXSelect<CAAdj, Where<CAAdj.adjRefNbr, Equal<Required<CAAdj.adjRefNbr>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) list[index].AdjRefNbr
        }));
        caAdjList.Add(instance.CalculateExternalTax(((PXSelectBase<CAAdj>) instance.CAAdjRecords).Current));
        PXProcessing<CAAdj>.SetInfo(index, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        if (!isMassProcess)
          throw new PXMassProcessException(index, ex);
        PXProcessing<CAAdj>.SetError(index, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
      }
    }
    return caAdjList;
  }
}
