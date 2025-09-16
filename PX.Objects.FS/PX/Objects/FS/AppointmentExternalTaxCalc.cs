// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.AppointmentExternalTaxCalc
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

public class AppointmentExternalTaxCalc : PXGraph<AppointmentExternalTaxCalc>
{
  [PXFilterable(new Type[] {})]
  public PXProcessingJoin<FSAppointment, InnerJoin<PX.Objects.TX.TaxZone, On<PX.Objects.TX.TaxZone.taxZoneID, Equal<FSAppointment.taxZoneID>>>, Where<PX.Objects.TX.TaxZone.isExternal, Equal<True>, And<FSAppointment.isTaxValid, Equal<False>>>> Items;

  public AppointmentExternalTaxCalc()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<FSAppointment>) this.Items).SetProcessDelegate(AppointmentExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (AppointmentExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9__1_0 = new PXProcessingBase<FSAppointment>.ProcessListDelegate((object) AppointmentExternalTaxCalc.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__1_0))));
  }

  public static void Process(FSAppointment doc)
  {
    AppointmentExternalTaxCalc.Process(new List<FSAppointment>()
    {
      doc
    }, false);
  }

  public static void Process(List<FSAppointment> list, bool isMassProcess)
  {
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    for (int index = 0; index < list.Count; ++index)
    {
      try
      {
        ((PXGraph) instance).Clear();
        ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) list[index].RefNbr, new object[1]
        {
          (object) list[index].SrvOrdType
        }));
        instance.CalculateExternalTax(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current);
        PXProcessing<FSAppointment>.SetInfo(index, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        if (!isMassProcess)
          throw new PXMassProcessException(index, ex);
        PXProcessing<FSAppointment>.SetError(index, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
      }
    }
  }
}
