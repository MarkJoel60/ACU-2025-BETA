// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARExternalTaxCalc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.Standalone;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public class ARExternalTaxCalc : PXGraph<ARExternalTaxCalc>
{
  public PXCancel<ARInvoice> Cancel;
  [PXFilterable(new Type[] {})]
  [PXViewDetailsButton(typeof (ARInvoice.refNbr))]
  public PXProcessingJoin<ARInvoice, InnerJoin<PX.Objects.TX.TaxZone, On<PX.Objects.TX.TaxZone.taxZoneID, Equal<ARInvoice.taxZoneID>>>, Where<PX.Objects.TX.TaxZone.isExternal, Equal<True>, And<ARInvoice.isTaxValid, Equal<False>, And<ARInvoice.released, Equal<False>>>>> Items;

  public ARExternalTaxCalc()
  {
    // ISSUE: method pointer
    ((PXProcessingBase<ARInvoice>) this.Items).SetProcessDelegate(new PXProcessingBase<ARInvoice>.ProcessListDelegate((object) null, __methodptr(ProcessAll)));
  }

  public static List<ARCashSale> Process(List<ARCashSale> list, bool isMassProcess)
  {
    List<ARCashSale> arCashSaleList = new List<ARCashSale>(list.Count);
    ARCashSaleEntry instance = PXGraph.CreateInstance<ARCashSaleEntry>();
    for (int index = 0; index < list.Count; ++index)
    {
      try
      {
        ((PXGraph) instance).Clear();
        ((PXSelectBase<ARCashSale>) instance.Document).Current = PXResultset<ARCashSale>.op_Implicit(PXSelectBase<ARCashSale, PXSelect<ARCashSale, Where<ARCashSale.docType, Equal<Required<ARCashSale.docType>>, And<ARCashSale.refNbr, Equal<Required<ARCashSale.refNbr>>>>>.Config>.Select((PXGraph) instance, new object[2]
        {
          (object) list[index].DocType,
          (object) list[index].RefNbr
        }));
        arCashSaleList.Add(instance.CalculateExternalTax(((PXSelectBase<ARCashSale>) instance.Document).Current));
        PXProcessing<ARCashSale>.SetInfo(index, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        if (!isMassProcess)
          throw new PXMassProcessException(index, ex);
        PXProcessing<ARCashSale>.SetError(index, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
      }
    }
    return arCashSaleList;
  }

  public static void ProcessAll(List<ARInvoice> list)
  {
    List<ARInvoice> arInvoiceList = new List<ARInvoice>(list.Count);
    ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
    for (int index = 0; index < list.Count; ++index)
    {
      try
      {
        ((PXGraph) instance).Clear();
        ((PXSelectBase<ARInvoice>) instance.Document).Current = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) instance, new object[2]
        {
          (object) list[index].DocType,
          (object) list[index].RefNbr
        }));
        arInvoiceList.Add(instance.CalculateExternalTax(((PXSelectBase<ARInvoice>) instance.Document).Current));
        PXProcessing<ARInvoice>.SetInfo(index, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        PXProcessing<ARInvoice>.SetError(index, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
      }
    }
  }
}
