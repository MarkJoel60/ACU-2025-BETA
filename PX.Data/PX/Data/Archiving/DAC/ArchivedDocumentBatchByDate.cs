// Decompiled with JetBrains decompiler
// Type: PX.Data.Archiving.DAC.ArchivedDocumentBatchByDate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Archiving.DAC;

[PXCacheName("Document Archival History")]
public class ArchivedDocumentBatchByDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBDate(IsKey = true)]
  [PXUIField(DisplayName = "Ready-to-Archive Date")]
  public virtual System.DateTime? DateToArchive { get; set; }

  [PXDBDate(IsKey = true, PreserveTime = true, UseTimeZone = false)]
  public virtual System.DateTime? ExecutionDate { get; set; }

  [PXDBString(1024 /*0x0400*/, IsUnicode = true, IsKey = true, InputMask = "")]
  public virtual 
  #nullable disable
  string TableName { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string TypeName { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Duration")]
  public virtual int? ExecutionTimeInSeconds { get; set; }

  [PXDBInt]
  public virtual int? ArchivedRowsCount { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBTimestamp(RecordComesFirst = true)]
  public virtual byte[] tstamp { get; set; }

  public abstract class dateToArchive : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ArchivedDocumentBatchByDate.dateToArchive>
  {
  }

  public abstract class executionDate : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ArchivedDocumentBatchByDate.executionDate>
  {
  }

  public abstract class tableName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ArchivedDocumentBatchByDate.tableName>
  {
  }

  public abstract class typeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ArchivedDocumentBatchByDate.typeName>
  {
  }

  public abstract class executionTimeInSeconds : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ArchivedDocumentBatchByDate.executionTimeInSeconds>
  {
  }

  public abstract class archivedRowsCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ArchivedDocumentBatchByDate.archivedRowsCount>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ArchivedDocumentBatchByDate.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    ArchivedDocumentBatchByDate.createdDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    ArchivedDocumentBatchByDate.Tstamp>
  {
  }
}
