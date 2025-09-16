// Decompiled with JetBrains decompiler
// Type: PX.SM.AUNotificationTemplate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class AUNotificationTemplate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _ScreenID;
  protected int? _NotificationID;
  protected System.DateTime? _ExecutionDate;
  protected string _Status;
  protected System.DateTime? _DateSent;
  protected Guid? _RefNoteID;
  protected string _FieldValues;
  protected byte[] _TStamp;

  [PXBool]
  [PXUIField(Visible = false)]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(8, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Notification ID")]
  [PXSelector(typeof (Search<AUNotification.notificationID, Where<AUNotification.screenID, Equal<Current<AUNotificationHistory.screenID>>>>), DescriptionField = typeof (AUNotification.description))]
  public virtual int? NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [PXDBDate(IsKey = true, PreserveTime = true, UseSmallDateTime = false, InputMask = "g")]
  [PXUIField(DisplayName = "Execution Date")]
  [PXDefault]
  public virtual System.DateTime? ExecutionDate
  {
    get => this._ExecutionDate;
    set => this._ExecutionDate = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [PXStringList(new string[] {"P", "S", "F"}, new string[] {"Pending", "Sent", "Failed"})]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBDate(PreserveTime = true, UseSmallDateTime = false, InputMask = "g")]
  [PXUIField(DisplayName = "Date Sent")]
  public virtual System.DateTime? DateSent
  {
    get => this._DateSent;
    set => this._DateSent = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Report ID", Required = false)]
  public virtual string ReportID { get; set; }

  [PXDBString(5)]
  [PXDefault("PDF", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXStringList(new string[] {"HTML", "PDF", "Excel"}, new string[] {"Html", "Pdf", "Excel"})]
  [PXUIField(DisplayName = "Report Format")]
  public virtual string ReportFormat { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Resultset")]
  public virtual string Resultset { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Field Values")]
  public virtual string FieldValues
  {
    get => this._FieldValues;
    set => this._FieldValues = value;
  }

  [PXLong]
  [PXUIField(Visible = false)]
  public long? Ticks
  {
    [PXDependsOnFields(new System.Type[] {typeof (AUNotificationTemplate.executionDate)})] get
    {
      return this._ExecutionDate.HasValue ? new long?(this._ExecutionDate.Value.Ticks) : new long?();
    }
    set
    {
    }
  }

  [PXDBTimestamp]
  public virtual byte[] TStamp
  {
    get => this._TStamp;
    set => this._TStamp = value;
  }

  [PXDBGuid(false)]
  public virtual Guid? EmailNoteID { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUNotificationTemplate.selected>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationTemplate.screenID>
  {
  }

  public abstract class notificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUNotificationTemplate.notificationID>
  {
  }

  public abstract class executionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUNotificationTemplate.executionDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationTemplate.status>
  {
  }

  public abstract class dateSent : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUNotificationTemplate.dateSent>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUNotificationTemplate.refNoteID>
  {
  }

  public abstract class reportID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationTemplate.reportID>
  {
  }

  public abstract class reportFormat : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationTemplate.reportFormat>
  {
  }

  public abstract class resultset : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationTemplate.resultset>
  {
  }

  public abstract class fieldValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationTemplate.fieldValues>
  {
  }

  public abstract class ticks : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  AUNotificationTemplate.ticks>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUNotificationTemplate.tStamp>
  {
  }

  public abstract class emailNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUNotificationTemplate.emailNoteID>
  {
  }
}
