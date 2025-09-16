// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSContractPostDet
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
public class FSContractPostDet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? ContractPostDetID { get; set; }

  [PXDBInt]
  public virtual int? AppDetID { get; set; }

  [PXDBInt]
  public virtual int? AppointmentID { get; set; }

  [PXDBInt]
  public virtual int? ContractPeriodDetID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Contract Period Nbr.")]
  public virtual int? ContractPeriodID { get; set; }

  [PXDBInt]
  public virtual int? ContractPostBatchID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Contract PostDoc Nbr.")]
  public virtual int? ContractPostDocID { get; set; }

  [PXDBInt]
  public virtual int? SODetID { get; set; }

  [PXDBInt]
  public virtual int? SOID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual 
  #nullable disable
  string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class contractPostDetID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPostDet.contractPostDetID>
  {
  }

  public abstract class appDetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractPostDet.appDetID>
  {
  }

  public abstract class appointmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractPostDet.appointmentID>
  {
  }

  public abstract class contractPeriodDetID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPostDet.contractPeriodDetID>
  {
  }

  public abstract class contractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPostDet.contractPeriodID>
  {
  }

  public abstract class contractPostBatchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPostDet.contractPostBatchID>
  {
  }

  public abstract class contractPostDocID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPostDet.contractPostDocID>
  {
  }

  public abstract class sODetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractPostDet.sODetID>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractPostDet.sOID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSContractPostDet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPostDet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractPostDet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSContractPostDet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPostDet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractPostDet.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSContractPostDet.Tstamp>
  {
  }
}
