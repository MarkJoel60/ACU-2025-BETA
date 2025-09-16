// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentTotals
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("AR Payment Totals")]
public class ARPaymentTotals : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [ARPaymentType.ListEx]
  [PXUIField]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDBDefault(typeof (ARRegister.refNbr), DefaultForUpdate = false)]
  [PXUIField]
  [PXParent(typeof (Select<ARRegister, Where<ARRegister.docType, Equal<Current<ARPaymentTotals.docType>>, And<ARRegister.refNbr, Equal<Current<ARPaymentTotals.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? OrderCntr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  [PXSelector(typeof (Search<SOOrderType.orderType>), CacheGlobal = true)]
  public virtual string AdjdOrderType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<ARPaymentTotals.adjdOrderType>>>>), DirtyRead = true)]
  public virtual string AdjdOrderNbr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? InvoiceCntr { get; set; }

  [PXDBString(3, IsFixed = true, InputMask = "")]
  [PXUIField]
  [ARInvoiceType.AdjList]
  public virtual string AdjdDocType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<ARRegister.refNbr, Where<ARRegister.docType, Equal<Current<ARPaymentTotals.adjdDocType>>>>), DirtyRead = true)]
  public virtual string AdjdRefNbr { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

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

  /// <exclude />
  public class PK : PrimaryKeyOf<ARPaymentTotals>.By<ARPaymentTotals.docType, ARPaymentTotals.refNbr>
  {
    public static ARPaymentTotals Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ARPaymentTotals) PrimaryKeyOf<ARPaymentTotals>.By<ARPaymentTotals.docType, ARPaymentTotals.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPaymentTotals.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPaymentTotals.refNbr>
  {
  }

  public abstract class orderCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPaymentTotals.orderCntr>
  {
  }

  public abstract class adjdOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPaymentTotals.adjdOrderType>
  {
  }

  public abstract class adjdOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPaymentTotals.adjdOrderNbr>
  {
  }

  public abstract class invoiceCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPaymentTotals.invoiceCntr>
  {
  }

  public abstract class adjdDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPaymentTotals.adjdDocType>
  {
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPaymentTotals.adjdRefNbr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARPaymentTotals.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPaymentTotals.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPaymentTotals.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARPaymentTotals.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPaymentTotals.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPaymentTotals.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARPaymentTotals.Tstamp>
  {
  }
}
