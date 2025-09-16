// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCProcessingCenterFeeType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("Fee Type for Credit Card Processing Center")]
[Serializable]
public class CCProcessingCenterFeeType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CCProcessingCenter.processingCenterID))]
  [PXParent(typeof (Select<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Current<CCProcessingCenterFeeType.processingCenterID>>>>))]
  public virtual 
  #nullable disable
  string ProcessingCenterID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Fee Type")]
  public virtual string FeeType { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>, And<CashAccountETDetail.cashAccountID, Equal<Current<CCProcessingCenter.depositAccountID>>>>>, Where<CAEntryType.module, Equal<BatchModule.moduleCA>, And<CAEntryType.useToReclassifyPayments, Equal<False>>>>))]
  public virtual string EntryTypeID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] Tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<CCProcessingCenterFeeType>.By<CCProcessingCenterFeeType.processingCenterID, CCProcessingCenterFeeType.feeType>
  {
    public static CCProcessingCenterFeeType Find(
      PXGraph graph,
      string processingCenterID,
      string feeType,
      PKFindOptions options = 0)
    {
      return (CCProcessingCenterFeeType) PrimaryKeyOf<CCProcessingCenterFeeType>.By<CCProcessingCenterFeeType.processingCenterID, CCProcessingCenterFeeType.feeType>.FindBy(graph, (object) processingCenterID, (object) feeType, options);
    }
  }

  public static class FK
  {
    public class ProcessingCenter : 
      PrimaryKeyOf<CCProcessingCenter>.By<CCProcessingCenter.processingCenterID>.ForeignKeyOf<CCProcessingCenterFeeType>.By<CCProcessingCenterFeeType.processingCenterID>
    {
    }

    public class EntryType : 
      PrimaryKeyOf<CAEntryType>.By<CAEntryType.entryTypeId>.ForeignKeyOf<CCProcessingCenterFeeType>.By<CCProcessingCenterFeeType.entryTypeID>
    {
    }
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterFeeType.processingCenterID>
  {
  }

  public abstract class feeType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterFeeType.feeType>
  {
  }

  public abstract class entryTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterFeeType.entryTypeID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenterFeeType.createdDateTime>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CCProcessingCenterFeeType.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterFeeType.createdByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenterFeeType.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CCProcessingCenterFeeType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterFeeType.lastModifiedByScreenID>
  {
  }

  public abstract class tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    CCProcessingCenterFeeType.tstamp>
  {
  }
}
