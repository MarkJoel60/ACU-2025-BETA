// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMExternalTaxCalc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.TX;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace PX.Objects.PM;

public class PMExternalTaxCalc : PXGraph<PMExternalTaxCalc>
{
  [PXFilterable(new Type[] {})]
  public PXProcessingJoin<PMProforma, InnerJoin<TaxZone, On<TaxZone.taxZoneID, Equal<PMProforma.taxZoneID>>>, Where<TaxZone.isExternal, Equal<True>, And<PMProforma.isTaxValid, Equal<False>>>> Items;

  public PMExternalTaxCalc()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<PMProforma>) this.Items).SetProcessDelegate(PMExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (PMExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9__1_0 = new PXProcessingBase<PMProforma>.ProcessListDelegate((object) PMExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__1_0))));
  }

  public static void Process(PMProforma doc)
  {
    PMExternalTaxCalc.Process(new List<PMProforma>() { doc }, false);
  }

  public static void Process(List<PMProforma> list, bool isMassProcess)
  {
    Stopwatch stopwatch = new Stopwatch();
    ProformaEntry instance = PXGraph.CreateInstance<ProformaEntry>();
    instance.SuppressRowSeleted = true;
    for (int index = 0; index < list.Count; ++index)
    {
      try
      {
        ((PXSelectBase<PMProforma>) instance.Document).Current = PXResultset<PMProforma>.op_Implicit(PXSelectBase<PMProforma, PXSelect<PMProforma, Where<PMProforma.refNbr, Equal<Required<PMProforma.refNbr>>, And<PMProforma.revisionID, Equal<Required<PMProforma.revisionID>>>>>.Config>.Select((PXGraph) instance, new object[2]
        {
          (object) list[index].RefNbr,
          (object) list[index].RevisionID
        }));
        instance.CalculateExternalTax(((PXSelectBase<PMProforma>) instance.Document).Current);
        PXProcessing<PMProforma>.SetInfo(index, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        if (!isMassProcess)
          throw new PXMassProcessException(index, ex);
        PXProcessing<PMProforma>.SetError(index, ex is PXOuterException ? ex.Message + Environment.NewLine + string.Join(Environment.NewLine, ((PXOuterException) ex).InnerMessages) : ex.Message);
      }
    }
  }
}
