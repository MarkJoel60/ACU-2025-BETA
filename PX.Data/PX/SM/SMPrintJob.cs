// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPrintJob
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Print Job")]
[Serializable]
public class SMPrintJob : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _DeviceHubID;
  protected string _Status;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBIdentity(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Job ID", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual int? JobID { get; set; }

  [PXDBGuid(false)]
  [PXDefault]
  public virtual Guid? GroupID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string Description { get; set; }

  [PXDBString(30)]
  [PXDefault]
  [PXUIField(DisplayName = "DeviceHub ID", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual string DeviceHubID { get; set; }

  [PXDBString(20)]
  [PXDefault]
  [PXSelector(typeof (Search<SMPrinter.printerName, Where<SMPrinter.deviceHubID, Equal<Current<SMPrintJob.deviceHubID>>, PX.Data.And<Match<Current<AccessInfo.userName>>>>>), new System.Type[] {typeof (SMPrinter.printerName), typeof (SMPrinter.description), typeof (SMPrinter.isActive)})]
  [PXUIField(DisplayName = "Printer", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string PrinterName { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Number of Copies", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual int? NumberOfCopies { get; set; }

  [PXDBString(8, InputMask = "aa.aa.aa.aa")]
  [PXUIField(DisplayName = "Report ID", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual string ReportID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [PrintJobStatus.List]
  [PXDefault("P")]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Creation Date and Time", Enabled = false)]
  [PXDBForcedCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBForcedLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SMPrintJob.selected>
  {
  }

  public abstract class jobID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPrintJob.jobID>
  {
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMPrintJob.groupID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPrintJob.description>
  {
  }

  public abstract class deviceHubID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPrintJob.deviceHubID>
  {
  }

  public abstract class printerName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPrintJob.printerName>
  {
  }

  public abstract class numberOfCopies : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPrintJob.numberOfCopies>
  {
  }

  public abstract class reportID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPrintJob.reportID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPrintJob.status>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMPrintJob.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPrintJob.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMPrintJob.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMPrintJob.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPrintJob.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMPrintJob.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SMPrintJob.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMPrintJob.noteID>
  {
  }
}
