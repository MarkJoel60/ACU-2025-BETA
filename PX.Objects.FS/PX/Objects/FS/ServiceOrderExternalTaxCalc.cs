// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceOrderExternalTaxCalc
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class ServiceOrderExternalTaxCalc : PXGraph<ServiceOrderExternalTaxCalc>
{
  [PXFilterable(new Type[] {})]
  public PXProcessingJoin<FSServiceOrder, InnerJoin<PX.Objects.TX.TaxZone, On<PX.Objects.TX.TaxZone.taxZoneID, Equal<FSServiceOrder.taxZoneID>>>, Where<PX.Objects.TX.TaxZone.isExternal, Equal<True>, And<FSServiceOrder.isTaxValid, Equal<False>>>> Items;

  public ServiceOrderExternalTaxCalc()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<FSServiceOrder>) this.Items).SetProcessDelegate(ServiceOrderExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (ServiceOrderExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9__1_0 = new PXProcessingBase<FSServiceOrder>.ProcessListDelegate((object) ServiceOrderExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__1_0))));
  }

  public static void Process(FSServiceOrder doc)
  {
    ServiceOrderExternalTaxCalc.Process(new List<FSServiceOrder>()
    {
      doc
    }, false);
  }

  public static void Process(List<FSServiceOrder> list, bool isMassProcess)
  {
    ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
    for (int index = 0; index < list.Count; ++index)
    {
      try
      {
        ((PXGraph) instance).Clear();
        ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current = list[index];
        instance.CalculateExternalTax(list[index]);
        PXProcessing<FSServiceOrder>.SetInfo(index, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        if (!isMassProcess)
          throw new PXMassProcessException(index, ex);
        PXProcessing<FSServiceOrder>.SetError(index, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
      }
    }
  }
}
