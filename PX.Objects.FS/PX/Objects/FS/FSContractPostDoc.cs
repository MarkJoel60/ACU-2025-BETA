// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSContractPostDoc
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSContractPostDoc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? ContractPostDocID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Contract Period Nbr.")]
  public virtual int? ContractPeriodID { get; set; }

  [PXDBInt]
  public virtual int? ContractPostBatchID { get; set; }

  [PXDBString(3, IsFixed = true, InputMask = ">aaa")]
  [PXUIField(DisplayName = "Document Type")]
  public virtual 
  #nullable disable
  string PostDocType { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Posted to")]
  public virtual string PostedTO { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Document Nbr.")]
  public virtual string PostRefNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Service Contract ID")]
  public virtual int? ServiceContractID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<FSContractPostDoc>.By<FSContractPostDoc.contractPostDocID>
  {
    public static FSContractPostDoc Find(
      PXGraph graph,
      int? contractPostDocID,
      PKFindOptions options = 0)
    {
      return (FSContractPostDoc) PrimaryKeyOf<FSContractPostDoc>.By<FSContractPostDoc.contractPostDocID>.FindBy(graph, (object) contractPostDocID, options);
    }
  }

  public static class FK
  {
    public class ContractPostBatch : 
      PrimaryKeyOf<FSContractPostBatch>.By<FSContractPostBatch.contractPostBatchID>.ForeignKeyOf<FSContractPostDoc>.By<FSContractPostDoc.contractPostBatchID>
    {
    }

    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<FSContractPostDoc>.By<FSContractPostDoc.serviceContractID>
    {
    }
  }

  public abstract class contractPostDocID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPostDoc.contractPostDocID>
  {
  }

  public abstract class contractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPostDoc.contractPeriodID>
  {
  }

  public abstract class contractPostBatchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPostDoc.contractPostBatchID>
  {
  }

  public abstract class postDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPostDoc.postDocType>
  {
  }

  public abstract class postedTO : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSContractPostDoc.postedTO>
  {
  }

  public abstract class postRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSContractPostDoc.postRefNbr>
  {
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPostDoc.serviceContractID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSContractPostDoc.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPostDoc.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractPostDoc.createdDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSContractPostDoc.Tstamp>
  {
  }
}
