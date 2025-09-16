// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.DAC.APRecognizedInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CloudServices.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP.InvoiceRecognition.Feedback;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition.DAC;

[PXInternalUseOnly]
[PXCacheName("Recognized document")]
[PXBreakInheritance]
[PXProjection(typeof (Select2<RecognizedRecord, LeftJoin<APInvoice, On<APInvoice.noteID, Equal<RecognizedRecord.documentLink>>>, Where<RecognizedRecord.entityType, Equal<RecognizedRecordEntityTypeListAttribute.aPDocument>>>), Persistent = true)]
public class APRecognizedInvoice : APInvoice
{
  [PXUIField(DisplayName = "Recognized Record Ref. Nbr.", Visible = false)]
  [PXDefault]
  [PXDBGuid(true, IsKey = true, BqlField = typeof (RecognizedRecord.refNbr))]
  [PXExtraKey]
  public virtual Guid? RecognizedRecordRefNbr { get; set; }

  [PXUIField(DisplayName = "Entity Type")]
  [PXDefault]
  [RecognizedRecordEntityTypeList]
  [PXDBString(3, IsFixed = true, BqlField = typeof (RecognizedRecord.entityType))]
  public virtual 
  #nullable disable
  string EntityType { get; set; }

  [PXUIField(DisplayName = "File Hash")]
  [PXDBBinary(16 /*0x10*/, IsFixed = true, BqlField = typeof (RecognizedRecord.fileHash))]
  public virtual byte[] FileHash { get; set; }

  [PXUIField(DisplayName = "Status")]
  [PXDefault("N")]
  [RecognizedRecordStatusList]
  [PXDBString(1, IsFixed = true, BqlField = typeof (RecognizedRecord.status))]
  public virtual string RecognizedRecordStatus { get; set; }

  [PXUIField(DisplayName = "Recognition Started", Enabled = false)]
  [PXDefault(false)]
  [PXDBBool(BqlField = typeof (RecognizedRecord.recognitionStarted))]
  public virtual bool? RecognitionStarted { get; set; }

  [PXUIField(DisplayName = "Recognition Result", Visible = false)]
  [PXDBString(IsUnicode = true, BqlField = typeof (RecognizedRecord.recognitionResult))]
  public virtual string RecognitionResult { get; set; }

  [PXUIField(DisplayName = "Recognition Feedback", Visible = false)]
  [PXDBString(IsUnicode = true)]
  public virtual string RecognitionFeedback { get; set; }

  [PXUIField(DisplayName = "Document Link", Visible = false)]
  [PXDBGuid(false, BqlField = typeof (RecognizedRecord.documentLink))]
  public virtual Guid? DocumentLink { get; set; }

  [PXUIField(DisplayName = "Link to Duplicate File", Visible = false)]
  [PXDBGuid(false, BqlField = typeof (RecognizedRecord.duplicateLink))]
  public virtual Guid? DuplicateLink { get; set; }

  [PXDBString(500, IsUnicode = true, BqlField = typeof (RecognizedRecord.mailFrom))]
  [PXUIField(DisplayName = "From", Enabled = false)]
  public virtual string MailFrom { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (RecognizedRecord.subject))]
  [PXUIField(DisplayName = "Summary", Enabled = false)]
  public virtual string Subject { get; set; }

  [PXUIField(DisplayName = "Message ID", Enabled = false)]
  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (RecognizedRecord.messageID))]
  public virtual string MessageID { get; set; }

  [PXUIField(DisplayName = "Owner", Enabled = false)]
  [PXDBInt(BqlField = typeof (RecognizedRecord.owner))]
  public virtual int? Owner { get; set; }

  [PXUIField(DisplayName = "Custom Info", Visible = false)]
  [PXDBString(IsUnicode = true, BqlField = typeof (RecognizedRecord.customInfo))]
  public virtual string CustomInfo { get; set; }

  [PXUIField(DisplayName = "Error Message", Visible = false, Enabled = false)]
  [PXDBString(IsUnicode = true, BqlField = typeof (RecognizedRecord.errorMessage))]
  public virtual string ErrorMessage { get; set; }

  [PXNote(BqlField = typeof (RecognizedRecord.noteID))]
  public override Guid? NoteID { get; set; }

  [PXDBCreatedByID(BqlField = typeof (RecognizedRecord.createdByID))]
  public virtual Guid? RecognizedRecordCreatedByID { get; set; }

  [PXDBCreatedByScreenID(BqlField = typeof (RecognizedRecord.createdByScreenID))]
  public virtual string RecognizedRecordCreatedByScreenID { get; set; }

  [PXDBCreatedDateTime(BqlField = typeof (RecognizedRecord.createdDateTime))]
  public virtual System.DateTime? RecognizedRecordCreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (RecognizedRecord.lastModifiedByID))]
  public virtual Guid? RecognizedRecordLastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (RecognizedRecord.lastModifiedByScreenID))]
  public virtual string RecognizedRecordLastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (RecognizedRecord.lastModifiedDateTime))]
  public virtual System.DateTime? RecognizedRecordLastModifiedDateTime { get; set; }

  [PXDBTimestamp(VerifyTimestamp = VerifyTimestampOptions.FromRecord, BqlField = typeof (RecognizedRecord.tStamp))]
  public virtual byte[] RecognizedRecordTStamp { get; set; }

  internal DocumentFeedbackBuilder FeedbackBuilder { get; set; }

  internal Dictionary<string, Uri> Links { get; set; }

  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXBool]
  [PXUIField(DisplayName = "Is Redirect")]
  public virtual bool? IsRedirect { get; set; }

  [PXUIField(DisplayName = "Status", Enabled = false)]
  [PXDefault("F", PersistingCheck = PXPersistingCheck.Nothing)]
  [APRecognizedInvoiceRecognitionStatusList]
  [PXString(1, IsFixed = true)]
  public virtual string RecognitionStatus { get; set; }

  [PXDefault(".pdf", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXString]
  [PXUIField(DisplayName = "Allow Files", Visible = false)]
  public virtual string AllowFiles { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Allow File Message", Visible = false)]
  public virtual string AllowFilesMsg { get; set; }

  [PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXBool]
  [PXUIField(DisplayName = "Allow File Upload", Visible = false)]
  public virtual bool? AllowUploadFile { get; set; }

  [PXGuid]
  [PXUIField(DisplayName = "File ID", Visible = false)]
  public virtual Guid? FileID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Recognized Data", Visible = false)]
  public virtual string RecognizedDataJson { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Vendor Term", Visible = false)]
  public virtual int? VendorTermIndex { get; set; }

  [PXString(IsUnicode = true)]
  public virtual string VendorName { get; set; }

  [PXString]
  public virtual string VendorSearchError { get; set; }

  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXBool]
  [PXUIField(DisplayName = "Is Data Loaded")]
  public virtual bool? IsDataLoaded { get; set; }

  public abstract class recognizedRecordRefNbr : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APRecognizedInvoice.recognizedRecordRefNbr>
  {
  }

  public abstract class entityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.entityType>
  {
  }

  public abstract class fileHash : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APRecognizedInvoice.fileHash>
  {
  }

  public abstract class recognizedRecordStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.recognizedRecordStatus>
  {
  }

  public abstract class recognitionStarted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRecognizedInvoice.recognitionStarted>
  {
  }

  public abstract class recognitionResult : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.recognitionResult>
  {
  }

  public abstract class recognitionFeedback : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.recognitionFeedback>
  {
  }

  public abstract class documentLink : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APRecognizedInvoice.documentLink>
  {
  }

  public abstract class duplicateLink : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APRecognizedInvoice.duplicateLink>
  {
  }

  public abstract class mailFrom : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRecognizedInvoice.mailFrom>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRecognizedInvoice.subject>
  {
  }

  public abstract class messageID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRecognizedInvoice.messageID>
  {
  }

  public abstract class owner : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRecognizedInvoice.owner>
  {
  }

  public abstract class customInfo : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.customInfo>
  {
  }

  public abstract class errorMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.errorMessage>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APRecognizedInvoice.noteID>
  {
  }

  public abstract class recognizedRecordCreatedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APRecognizedInvoice.recognizedRecordCreatedByID>
  {
  }

  public abstract class recognizedRecordCreatedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.recognizedRecordCreatedByScreenID>
  {
  }

  public abstract class recognizedRecordCreatedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APRecognizedInvoice.recognizedRecordCreatedDateTime>
  {
  }

  public abstract class recognizedRecordLastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APRecognizedInvoice.recognizedRecordLastModifiedByID>
  {
  }

  public abstract class recognizedRecordLastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.recognizedRecordLastModifiedByScreenID>
  {
  }

  public abstract class recognizedRecordLastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APRecognizedInvoice.recognizedRecordLastModifiedDateTime>
  {
  }

  public abstract class recognizedRecordTStamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    APRecognizedInvoice.recognizedRecordTStamp>
  {
  }

  public abstract class isRedirect : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRecognizedInvoice.isRedirect>
  {
  }

  public abstract class recognitionStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.status>
  {
  }

  public abstract class allowFiles : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.allowFiles>
  {
  }

  public abstract class allowFilesMsg : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.allowFilesMsg>
  {
  }

  public abstract class allowUploadFile : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRecognizedInvoice.allowUploadFile>
  {
  }

  public abstract class fileID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APRecognizedInvoice.fileID>
  {
  }

  public abstract class recognizedDataJson : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.recognizedDataJson>
  {
  }

  public abstract class vendorTermIndex : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.vendorTermIndex>
  {
  }

  public abstract class vendorName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.vendorName>
  {
  }

  public abstract class vendorSearchError : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedInvoice.vendorSearchError>
  {
  }

  public abstract class isDataLoaded : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRecognizedInvoice.isDataLoaded>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRecognizedInvoice.docType>
  {
  }

  public new abstract class curyLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRecognizedInvoice.curyLineTotal>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRecognizedInvoice.refNbr>
  {
  }

  public new abstract class docDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APRecognizedInvoice.docDate>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRecognizedInvoice.vendorID>
  {
  }

  public new abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRecognizedInvoice.vendorLocationID>
  {
  }

  public new abstract class suppliedByVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRecognizedInvoice.suppliedByVendorID>
  {
  }

  public new abstract class suppliedByVendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRecognizedInvoice.suppliedByVendorLocationID>
  {
  }

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRecognizedInvoice.hasMultipleProjects>
  {
  }
}
