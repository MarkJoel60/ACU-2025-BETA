// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSPostBatch
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Field Service Billing Batch")]
[PXPrimaryGraph(new Type[] {typeof (PostBatchMaint), typeof (InventoryPostBatchMaint)}, new Type[] {typeof (Where<FSPostBatch.postTo, NotEqual<ListField_PostTo.IN>>), typeof (Where<FSPostBatch.postTo, Equal<ListField_PostTo.IN>>)})]
[Serializable]
public class FSPostBatch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXUIField(Enabled = false, Visible = false, DisplayName = "Batch ID")]
  public virtual int? BatchID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<FSPostBatch.batchNbr, Where<FSPostBatch.status, NotEqual<FSPostBatch.status.temporary>>>))]
  [AutoNumber(typeof (Search<FSSetup.postBatchNumberingID>), typeof (AccessInfo.businessDate))]
  public virtual 
  #nullable disable
  string BatchNbr { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("T")]
  [FSPostBatch.status.List]
  [PXUIField]
  public virtual string Status { get; set; }

  [PXDBString(2, IsFixed = true)]
  [ListField_PostTo.ListAtrribute]
  [PXUIField]
  public virtual string PostTo { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Documents Processed")]
  public virtual int? QtyDoc { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (FSPostBatch.finPeriodID))]
  [PXUIField]
  public virtual string FinPeriodID { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? InvoiceDate { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXSelector(typeof (FSBillingCycle.billingCycleID), SubstituteKey = typeof (FSBillingCycle.billingCycleCD), DescriptionField = typeof (FSBillingCycle.descr))]
  public virtual int? BillingCycleID { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Up to Date")]
  public virtual DateTime? UpToDate { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Billing Cycle Cut-Off Date")]
  public virtual DateTime? CutOffDate { get; set; }

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

  public class PK : PrimaryKeyOf<FSPostBatch>.By<FSPostBatch.batchNbr>
  {
    public static FSPostBatch Find(PXGraph graph, string batchNbr, PKFindOptions options = 0)
    {
      return (FSPostBatch) PrimaryKeyOf<FSPostBatch>.By<FSPostBatch.batchNbr>.FindBy(graph, (object) batchNbr, options);
    }
  }

  public static class FK
  {
    public class BillingCycle : 
      PrimaryKeyOf<FSBillingCycle>.By<FSBillingCycle.billingCycleID>.ForeignKeyOf<FSPostBatch>.By<FSPostBatch.billingCycleID>
    {
    }
  }

  public abstract class batchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostBatch.batchID>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostBatch.batchNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostBatch.status>
  {
    public const string Temporary = "T";
    public const string Completed = "C";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "T", "C" }, new string[2]
        {
          "Temporary",
          "Completed"
        })
      {
      }
    }

    public class temporary : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FSPostBatch.status.temporary>
    {
      public temporary()
        : base("T")
      {
      }
    }

    public class completed : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FSPostBatch.status.completed>
    {
      public completed()
        : base("C")
      {
      }
    }
  }

  public abstract class postTo : ListField_PostTo
  {
  }

  public abstract class qtyDoc : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostBatch.qtyDoc>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostBatch.finPeriodID>
  {
  }

  public abstract class invoiceDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSPostBatch.invoiceDate>
  {
  }

  public abstract class billingCycleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostBatch.billingCycleID>
  {
  }

  public abstract class upToDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSPostBatch.upToDate>
  {
  }

  public abstract class cutOffDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSPostBatch.cutOffDate>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSPostBatch.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSPostBatch.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSPostBatch.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSPostBatch.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSPostBatch.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSPostBatch.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSPostBatch.Tstamp>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSPostBatch.selected>
  {
  }
}
