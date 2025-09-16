// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSCreatedDoc
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSCreatedDoc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  public virtual int? BatchID { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Document", Enabled = false)]
  public virtual 
  #nullable disable
  string PostTo { get; set; }

  [PXDBString(4)]
  [PXUIField(DisplayName = "Document Type", Enabled = false)]
  public virtual string CreatedDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Document Nbr.", Enabled = false)]
  public virtual string CreatedRefNbr { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class batchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSCreatedDoc.batchID>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSCreatedDoc.recordID>
  {
  }

  public abstract class postTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSCreatedDoc.postTo>
  {
  }

  public abstract class createdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCreatedDoc.createdDocType>
  {
  }

  public abstract class createdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSCreatedDoc.createdRefNbr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSCreatedDoc.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCreatedDoc.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSCreatedDoc.createdDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSCreatedDoc.Tstamp>
  {
  }
}
