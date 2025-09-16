// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DBoxHeader
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Objects.FS;

public class DBoxHeader
{
  public DBoxHeaderContact Contact;
  public DBoxHeaderAddress Address;
  public object sourceDocument;

  public virtual int? CustomerID { get; set; }

  public virtual int? LocationID { get; set; }

  public virtual string CuryID { get; set; }

  public virtual int? ContactID { get; set; }

  public virtual int? SalesPersonID { get; set; }

  public virtual string TaxZoneID { get; set; }

  public virtual string SrvOrdType { get; set; }

  public virtual int? BranchID { get; set; }

  public virtual int? BranchLocationID { get; set; }

  public virtual string Description { get; set; }

  public virtual string LongDescr { get; set; }

  public virtual int? ProjectID { get; set; }

  public virtual int? ProjectTaskID { get; set; }

  public virtual DateTime? OrderDate { get; set; }

  public virtual DateTime? SLAETA { get; set; }

  public virtual int? AssignedEmpID { get; set; }

  public virtual int? ProblemID { get; set; }

  public virtual DateTime? ScheduledDateTimeBegin { get; set; }

  public virtual DateTime? ScheduledDateTimeEnd { get; set; }

  public virtual bool? HandleManuallyScheduleTime { get; set; }

  public virtual bool? CreateAppointment { get; set; }

  public virtual bool? CopyNotes { get; set; }

  public virtual bool? CopyFiles { get; set; }

  public static implicit operator DBoxHeader(DBoxDocSettings docSettings)
  {
    if (docSettings == null)
      return (DBoxHeader) null;
    DBoxHeader dboxHeader = new DBoxHeader();
    dboxHeader.CustomerID = docSettings.CustomerID;
    dboxHeader.SrvOrdType = docSettings.SrvOrdType;
    dboxHeader.BranchID = docSettings.BranchID;
    dboxHeader.BranchLocationID = docSettings.BranchLocationID;
    dboxHeader.Description = docSettings.Description;
    dboxHeader.LongDescr = docSettings.LongDescr;
    dboxHeader.ProjectID = docSettings.ProjectID;
    dboxHeader.ProjectTaskID = docSettings.ProjectTaskID;
    dboxHeader.OrderDate = docSettings.OrderDate;
    dboxHeader.SLAETA = docSettings.SLAETA;
    dboxHeader.AssignedEmpID = docSettings.AssignedEmpID;
    dboxHeader.ProblemID = docSettings.ProblemID;
    dboxHeader.ContactID = docSettings.ContactID;
    dboxHeader.ScheduledDateTimeBegin = docSettings.ScheduledDateTimeBegin;
    dboxHeader.HandleManuallyScheduleTime = docSettings.HandleManuallyScheduleTime;
    dboxHeader.CreateAppointment = new bool?(docSettings.DestinationDocument == "AP");
    if (docSettings.HandleManuallyScheduleTime.GetValueOrDefault())
      dboxHeader.ScheduledDateTimeEnd = docSettings.ScheduledDateTimeEnd;
    return dboxHeader;
  }
}
