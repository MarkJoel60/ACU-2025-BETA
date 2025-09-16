// Decompiled with JetBrains decompiler
// Type: PX.SM.SMScanJob
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Scan Job")]
[Serializable]
public class SMScanJob : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DeviceHubID;

  [PXDBIdentity(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Job ID", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual int? ScanJobID { get; set; }

  [PXDBString(30)]
  [PXDefault]
  [PXUIField(DisplayName = "DeviceHub ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string DeviceHubID { get; set; }

  [PXDBString(20, IsUnicode = false)]
  [PXDefault]
  [PXSelector(typeof (Search<SMScanner.scannerName>), DescriptionField = typeof (SMScanner.description))]
  [PXUIField(DisplayName = "Scanner ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string ScannerName { get; set; }

  [PXDBString(8, IsUnicode = false, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Form ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string EntityScreenID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Entity Note ID")]
  public virtual Guid? EntityNoteID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Primary View Name")]
  public virtual string EntityPrimaryViewName { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible)]
  [ScanJobStatus.List]
  [PXDefault("S")]
  public virtual string Status { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {}, new string[] {})]
  [PXUIField(DisplayName = "Paper Source")]
  public virtual int? PaperSource { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {}, new string[] {})]
  [PXUIField(DisplayName = "Color Mode")]
  public virtual int? PixelType { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {}, new string[] {})]
  [PXUIField(DisplayName = "Resolution")]
  public virtual int? Resolution { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {}, new string[] {})]
  [PXUIField(DisplayName = "File Type")]
  public virtual int? FileType { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "File Name")]
  public virtual string FileName { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Error")]
  public virtual string Error { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Error Trace", Visible = false)]
  public virtual string ErrorTrace { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string PaperSourceList { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string PixelTypeList { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string ResolutionList { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string FileTypeList { get; set; }

  [PXString]
  [PXFormula(typeof (PXUserNameExt<SMScanJob.createdByID>))]
  [PXUIField(DisplayName = "Task Initiator", Visibility = PXUIVisibility.Invisible, IsReadOnly = true)]
  public virtual string RequestingUserName { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Creation Date and Time")]
  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  public abstract class scanJobID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMScanJob.scanJobID>
  {
  }

  public abstract class deviceHubID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanJob.deviceHubID>
  {
  }

  public abstract class scannerName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanJob.scannerName>
  {
  }

  public abstract class entityScreenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanJob.entityScreenID>
  {
  }

  public abstract class entityNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMScanJob.entityNoteID>
  {
  }

  public abstract class entityPrimaryViewName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScanJob.entityPrimaryViewName>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanJob.status>
  {
  }

  public abstract class paperSource : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMScanJob.paperSource>
  {
  }

  public abstract class pixelType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMScanJob.pixelType>
  {
  }

  public abstract class resolution : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMScanJob.resolution>
  {
  }

  public abstract class fileType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMScanJob.fileType>
  {
  }

  public abstract class fileName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanJob.fileName>
  {
  }

  public abstract class error : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanJob.error>
  {
  }

  public abstract class errorTrace : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanJob.errorTrace>
  {
  }

  public abstract class paperSourceList : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScanJob.paperSourceList>
  {
  }

  public abstract class pixelTypeList : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanJob.pixelTypeList>
  {
  }

  public abstract class resolutionList : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanJob.resolutionList>
  {
  }

  public abstract class fileTypeList : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanJob.fileTypeList>
  {
  }

  public abstract class requestingUserName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScanJob.requestingUserName>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMScanJob.lineCntr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SMScanJob.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMScanJob.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScanJob.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMScanJob.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMScanJob.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScanJob.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMScanJob.lastModifiedDateTime>
  {
  }
}
