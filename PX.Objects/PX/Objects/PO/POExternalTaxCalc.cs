// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POExternalTaxCalc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO;

public class POExternalTaxCalc : PXGraph<POExternalTaxCalc>
{
  [PXFilterable(new Type[] {})]
  public PXProcessingJoin<POOrder, InnerJoin<PX.Objects.TX.TaxZone, On<PX.Objects.TX.TaxZone.taxZoneID, Equal<POOrder.taxZoneID>>>, Where<PX.Objects.TX.TaxZone.isExternal, Equal<True>, And<POOrder.isTaxValid, Equal<False>>>> Items;

  public POExternalTaxCalc()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<POOrder>) this.Items).SetProcessDelegate(POExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (POExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9__1_0 = new PXProcessingBase<POOrder>.ProcessListDelegate((object) POExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__1_0))));
  }

  public static void Process(POOrder doc)
  {
    POExternalTaxCalc.Process(new List<POOrder>() { doc }, false);
  }

  public static void Process(List<POOrder> list, bool isMassProcess)
  {
    POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
    for (int index = 0; index < list.Count; ++index)
    {
      try
      {
        ((PXGraph) instance).Clear();
        ((PXSelectBase<POOrder>) instance.Document).Current = PXResultset<POOrder>.op_Implicit(PXSelectBase<POOrder, PXSelect<POOrder, Where<POOrder.orderType, Equal<Required<POOrder.orderType>>, And<POOrder.orderNbr, Equal<Required<POOrder.orderNbr>>>>>.Config>.Select((PXGraph) instance, new object[2]
        {
          (object) list[index].OrderType,
          (object) list[index].OrderNbr
        }));
        instance.CalculateExternalTax(((PXSelectBase<POOrder>) instance.Document).Current);
        PXProcessing<POOrder>.SetInfo(index, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        if (!isMassProcess)
          throw new PXMassProcessException(index, ex);
        PXProcessing<POOrder>.SetError(index, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
      }
    }
  }
}
