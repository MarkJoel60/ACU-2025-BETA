// Decompiled with JetBrains decompiler
// Type: PX.CloudServices.DAC.RecognizedRecord
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.CloudServices.DAC;

[PXInternalUseOnly]
[PXCacheName("Recognized Record")]
[Serializable]
public class RecognizedRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Entity Type")]
  [PXDefault]
  [RecognizedRecordEntityTypeList]
  [PXDBString(3, IsKey = true, IsFixed = true)]
  public virtual 
  #nullable disable
  string EntityType { get; set; }

  [PXUIField(DisplayName = "Ref. Nbr.", Visible = false)]
  [PXDefault]
  [PXDBGuid(true, IsKey = true)]
  public virtual Guid? RefNbr { get; set; }

  [PXUIField(DisplayName = "File Hash")]
  [PXDBBinary(16 /*0x10*/, IsFixed = true)]
  public virtual byte[] FileHash { get; set; }

  [PXUIField(DisplayName = "Status")]
  [PXDefault("N")]
  [RecognizedRecordStatusList]
  [PXDBString(1, IsFixed = true)]
  public virtual string Status { get; set; }

  [PXUIField(DisplayName = "Recognition Started", Enabled = false)]
  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? RecognitionStarted { get; set; }

  [PXUIField(DisplayName = "Recognition Result", Visible = false)]
  [PXDBString(IsUnicode = true)]
  public virtual string RecognitionResult { get; set; }

  [PXUIField(DisplayName = "Recognition Feedback", Visible = false)]
  [PXDBString(IsUnicode = true)]
  public virtual string RecognitionFeedback { get; set; }

  [PXUIField(DisplayName = "Document Link", Visible = false)]
  [PXDBGuid(false)]
  public virtual Guid? DocumentLink { get; set; }

  [PXUIField(DisplayName = "Duplicate Link", Visible = false)]
  [PXDBGuid(false)]
  public virtual Guid? DuplicateLink { get; set; }

  [PXDBString(500, IsUnicode = true)]
  [PXUIField(DisplayName = "From", Enabled = false)]
  public virtual string MailFrom { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Summary", Enabled = false)]
  public virtual string Subject { get; set; }

  [PXUIField(DisplayName = "Message ID", Enabled = false)]
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string MessageID { get; set; }

  [PXUIField(DisplayName = "Owner", Enabled = false)]
  [PXDBInt]
  public virtual int? Owner { get; set; }

  [PXUIField(DisplayName = "Custom Info", Visible = false)]
  [PXDBString(IsUnicode = true)]
  public virtual string CustomInfo { get; set; }

  [PXUIField(DisplayName = "Error Message", Enabled = false, Visible = false)]
  [PXDBString(IsUnicode = true)]
  public virtual string ErrorMessage { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] TStamp { get; set; }

  [PXDBString(255 /*0xFF*/)]
  public string ResultUrl { get; set; }

  [PXDBGuid(false)]
  [PXDefaultCloudTenantId]
  [PXUIField(DisplayName = "CloudTenantId")]
  public virtual Guid? CloudTenantId { get; set; }

  [PXDBString]
  [PXDefault]
  [PXUIField(DisplayName = "ModelName")]
  public virtual string ModelName { get; set; }

  [PXDBGuid(false)]
  [PXDefault]
  [PXUIField(DisplayName = "CloudFileId")]
  public virtual Guid? CloudFileId { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "PageCount")]
  public virtual int? PageCount { get; set; }

  public abstract class entityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RecognizedRecord.entityType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RecognizedRecord.refNbr>
  {
  }

  public abstract class fileHash : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RecognizedRecord.fileHash>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RecognizedRecord.status>
  {
  }

  public abstract class recognitionStarted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RecognizedRecord.recognitionStarted>
  {
  }

  public abstract class recognitionResult : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecord.recognitionResult>
  {
  }

  public abstract class recognitionFeedback : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecord.recognitionFeedback>
  {
  }

  public abstract class documentLink : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RecognizedRecord.documentLink>
  {
  }

  public abstract class duplicateLink : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RecognizedRecord.duplicateLink>
  {
  }

  public abstract class mailFrom : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RecognizedRecord.mailFrom>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RecognizedRecord.subject>
  {
  }

  public abstract class messageID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RecognizedRecord.messageID>
  {
  }

  public abstract class owner : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RecognizedRecord.owner>
  {
  }

  public abstract class customInfo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RecognizedRecord.customInfo>
  {
  }

  public abstract class errorMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecord.errorMessage>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RecognizedRecord.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RecognizedRecord.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecord.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RecognizedRecord.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RecognizedRecord.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecord.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RecognizedRecord.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RecognizedRecord.tStamp>
  {
  }

  public abstract class resultUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RecognizedRecord.resultUrl>
  {
  }

  public abstract class cloudTenantId : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RecognizedRecord.cloudTenantId>
  {
  }

  public abstract class modelName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RecognizedRecord.modelName>
  {
  }

  public abstract class cloudFileId : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RecognizedRecord.cloudFileId>
  {
  }

  public abstract class pageCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RecognizedRecord.pageCount>
  {
  }
}
