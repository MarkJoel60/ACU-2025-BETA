// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOExternalTaxCalc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace PX.Objects.SO;

public class SOExternalTaxCalc : PXGraph<SOExternalTaxCalc>
{
  [PXFilterable(new Type[] {})]
  public PXProcessingJoin<SOOrder, InnerJoin<PX.Objects.TX.TaxZone, On<PX.Objects.TX.TaxZone.taxZoneID, Equal<SOOrder.taxZoneID>>>, Where<PX.Objects.TX.TaxZone.isExternal, Equal<True>, And<SOOrder.isTaxValid, Equal<False>>>> Items;

  public SOExternalTaxCalc()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<SOOrder>) this.Items).SetProcessDelegate(SOExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (SOExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9__1_0 = new PXProcessingBase<SOOrder>.ProcessListDelegate((object) SOExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__1_0))));
  }

  public static void Process(SOOrder doc)
  {
    SOExternalTaxCalc.Process(new List<SOOrder>() { doc }, false);
  }

  public static void Process(List<SOOrder> list, bool isMassProcess)
  {
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
    stopwatch.Stop();
    for (int index = 0; index < list.Count; ++index)
    {
      try
      {
        ((PXGraph) instance).Clear();
        stopwatch.Reset();
        stopwatch.Start();
        ((PXSelectBase<SOOrder>) instance.Document).Current = PXResultset<SOOrder>.op_Implicit(PXSelectBase<SOOrder, PXSelect<SOOrder, Where<SOOrder.orderType, Equal<Required<SOOrder.orderType>>, And<SOOrder.orderNbr, Equal<Required<SOOrder.orderNbr>>>>>.Config>.Select((PXGraph) instance, new object[2]
        {
          (object) list[index].OrderType,
          (object) list[index].OrderNbr
        }));
        stopwatch.Stop();
        instance.CalculateExternalTax(((PXSelectBase<SOOrder>) instance.Document).Current);
        PXProcessing<SOOrder>.SetInfo(index, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        if (!isMassProcess)
          throw new PXMassProcessException(index, ex);
        PXProcessing<SOOrder>.SetError(index, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
      }
    }
  }
}
