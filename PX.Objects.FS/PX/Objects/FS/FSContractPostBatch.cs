// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSContractPostBatch
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(typeof (ContractPostBatchMaint))]
[Serializable]
public class FSContractPostBatch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXUIField(Enabled = false, Visible = false, DisplayName = "Contract Post Batch ID")]
  public virtual int? ContractPostBatchID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<FSContractPostBatch.contractPostBatchNbr>))]
  [AutoNumber(typeof (Search<FSSetup.postBatchNumberingID>), typeof (AccessInfo.businessDate))]
  public virtual 
  #nullable disable
  string ContractPostBatchNbr { get; set; }

  [FinPeriodSelector(BqlField = typeof (FSContractPostBatch.finPeriodID))]
  [PXUIField]
  public virtual string FinPeriodID { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? InvoiceDate { get; set; }

  [PXDBString(2, IsFixed = true)]
  [ListField_PostTo.ListAtrribute]
  [PXUIField]
  public virtual string PostTo { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Up to Date")]
  public virtual DateTime? UpToDate { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created By Screen ID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified By Screen ID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  public class PK : PrimaryKeyOf<FSContractPostBatch>.By<FSContractPostBatch.contractPostBatchID>
  {
    public static FSContractPostBatch Find(
      PXGraph graph,
      int? contractPostBatchID,
      PKFindOptions options = 0)
    {
      return (FSContractPostBatch) PrimaryKeyOf<FSContractPostBatch>.By<FSContractPostBatch.contractPostBatchID>.FindBy(graph, (object) contractPostBatchID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSContractPostBatch>.By<FSContractPostBatch.contractPostBatchNbr>
  {
    public static FSContractPostBatch Find(
      PXGraph graph,
      string contractPostBatchNbr,
      PKFindOptions options = 0)
    {
      return (FSContractPostBatch) PrimaryKeyOf<FSContractPostBatch>.By<FSContractPostBatch.contractPostBatchNbr>.FindBy(graph, (object) contractPostBatchNbr, options);
    }
  }

  public abstract class contractPostBatchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPostBatch.contractPostBatchID>
  {
  }

  public abstract class contractPostBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPostBatch.contractPostBatchNbr>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPostBatch.finPeriodID>
  {
  }

  public abstract class invoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractPostBatch.invoiceDate>
  {
  }

  public abstract class postTo : ListField_PostTo
  {
  }

  public abstract class upToDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractPostBatch.upToDate>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSContractPostBatch.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPostBatch.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractPostBatch.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSContractPostBatch.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPostBatch.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractPostBatch.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSContractPostBatch.Tstamp>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSContractPostBatch.selected>
  {
  }
}
