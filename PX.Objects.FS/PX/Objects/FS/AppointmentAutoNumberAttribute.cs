// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.AppointmentAutoNumberAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class AppointmentAutoNumberAttribute(Type setupField, Type dateField) : 
  AlternateAutoNumberAttribute(setupField, dateField),
  IPXRowInsertingSubscriber
{
  protected override string GetInitialRefNbr(string baseRefNbr) => baseRefNbr.Trim() + "-1";

  void IPXRowInsertingSubscriber.RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
  }

  protected override string GetNewNumberSymbol(string numberingID) => " <NEW>";

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.UserNumbering = new bool?(false);
  }

  /// <summary>
  /// Allows to calculate the <c>RefNbr</c> sequence when trying to insert a new register.
  /// </summary>
  protected override bool SetRefNbr(PXCache cache, object row)
  {
    FSAppointment fsAppointment = (FSAppointment) row;
    if (fsAppointment.SOID.HasValue)
    {
      int? soid = fsAppointment.SOID;
      int num = 0;
      if (!(soid.GetValueOrDefault() < num & soid.HasValue))
      {
        string refNbr = PXResultset<FSAppointment>.op_Implicit(PXSelectBase<FSAppointment, PXSelectReadonly<FSAppointment, Where<FSAppointment.sOID, Equal<Current<FSAppointment.sOID>>>, OrderBy<Desc<FSAppointment.appointmentID>>>.Config>.SelectWindowed(cache.Graph, 0, 1, Array.Empty<object>()))?.RefNbr;
        fsAppointment.RefNbr = this.GetNextRefNbr(fsAppointment.SORefNbr, refNbr);
        return true;
      }
    }
    return false;
  }
}
