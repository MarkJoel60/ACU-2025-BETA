// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSProcessIdentity
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
public class FSProcessIdentity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? ProcessID { get; set; }

  [PXDBString(4, IsFixed = true)]
  public virtual 
  #nullable disable
  string ProcessType { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "From Date")]
  public virtual DateTime? FilterFromTo { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "To Date")]
  public virtual DateTime? FilterUpTo { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "LastModifiedDateTime")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "tstamp")]
  public virtual byte[] tstamp { get; set; }

  public abstract class processID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSProcessIdentity.processID>
  {
  }

  public abstract class processType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSProcessIdentity.processType>
  {
  }

  public abstract class filterFromTo : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSProcessIdentity.filterFromTo>
  {
  }

  public abstract class filterUpTo : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSProcessIdentity.filterUpTo>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSProcessIdentity.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSProcessIdentity.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSProcessIdentity.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSProcessIdentity.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSProcessIdentity.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSProcessIdentity.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSProcessIdentity.Tstamp>
  {
  }
}
