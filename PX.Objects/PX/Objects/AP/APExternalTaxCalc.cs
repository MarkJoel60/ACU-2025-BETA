// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APExternalTaxCalc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

public class APExternalTaxCalc : PXGraph<APExternalTaxCalc>
{
  [PXFilterable(new System.Type[] {})]
  public PXProcessingJoin<APInvoice, InnerJoin<PX.Objects.TX.TaxZone, On<PX.Objects.TX.TaxZone.taxZoneID, Equal<APInvoice.taxZoneID>>>, Where<PX.Objects.TX.TaxZone.isExternal, Equal<True>, And<APInvoice.isTaxValid, Equal<False>, And<APInvoice.released, Equal<False>>>>> Items;

  public APExternalTaxCalc()
  {
    this.Items.SetProcessDelegate((PXProcessingBase<APInvoice>.ProcessListDelegate) (list =>
    {
      List<APInvoice> list1 = new List<APInvoice>(list.Count);
      foreach (APInvoice apInvoice in list)
        list1.Add(apInvoice);
      APExternalTaxCalc.Process(list1, true);
    }));
  }

  public static List<APInvoice> Process(List<APInvoice> list, bool isMassProcess)
  {
    List<APInvoice> apInvoiceList = new List<APInvoice>(list.Count);
    APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
    for (int index = 0; index < list.Count; ++index)
    {
      try
      {
        instance.Clear();
        instance.Document.Current = (APInvoice) PXSelectBase<APInvoice, PXSelect<APInvoice, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.Select((PXGraph) instance, (object) list[index].DocType, (object) list[index].RefNbr);
        apInvoiceList.Add(instance.CalculateExternalTax(instance.Document.Current));
        PXProcessing<APInvoice>.SetInfo(index, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        if (!isMassProcess)
          throw new PXMassProcessException(index, ex);
        PXProcessing<APInvoice>.SetError(index, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
      }
    }
    return apInvoiceList;
  }
}
