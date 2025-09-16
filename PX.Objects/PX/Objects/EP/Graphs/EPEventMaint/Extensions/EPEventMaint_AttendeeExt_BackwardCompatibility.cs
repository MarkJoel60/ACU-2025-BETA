// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.Graphs.EPEventMaint.Extensions.EPEventMaint_AttendeeExt_BackwardCompatibility
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP.Graphs.EPEventMaint.Extensions;

public class EPEventMaint_AttendeeExt_BackwardCompatibility : 
  PXGraphExtension<EPEventMaint_AttendeeExt, PX.Objects.EP.EPEventMaint>
{
  public const int ManualAttendeeType = 0;
  public const int ContactAttendeeType = 1;
  public const string CbApi_Type_FieldName = "Attendee$Type";
  public const string CbApi_Key_FieldName = "Attendee$Key";

  public override void Initialize()
  {
    this.Base1.Attendees.Cache.Fields.Add("Attendee$Key");
    this.Base.FieldSelecting.AddHandler(typeof (EPAttendee), "Attendee$Key", (PXFieldSelecting) ((s, e) =>
    {
      if (!(e.Row is EPAttendee row2))
        return;
      PXFieldSelectingEventArgs selectingEventArgs = e;
      int? contactId = row2.ContactID;
      ref int? local1 = ref contactId;
      string str = local1.HasValue ? local1.GetValueOrDefault().ToString() : (string) null;
      if (str == null)
      {
        Guid? attendeeId = row2.AttendeeID;
        ref Guid? local2 = ref attendeeId;
        str = local2.HasValue ? local2.GetValueOrDefault().ToString() : (string) null;
      }
      selectingEventArgs.ReturnValue = (object) str;
    }));
    this.Base1.Attendees.Cache.Fields.Add("Attendee$Type");
    this.Base.FieldSelecting.AddHandler(typeof (EPAttendee), "Attendee$Type", (PXFieldSelecting) ((s, e) =>
    {
      if (!(e.Row is EPAttendee row4))
        return;
      e.ReturnValue = row4.ContactID.HasValue ? (object) 1 : (object) 0;
    }));
  }
}
