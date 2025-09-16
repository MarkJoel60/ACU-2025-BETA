// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.CCPayLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.CC;

/// <summary>
/// DAC represents a row in the database with information about Payment Link.
/// </summary>
[PXCacheName("Payment Link")]
public class CCPayLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Acumatica specific Payment Link Id.</summary>
  [PXDBIdentity(IsKey = true)]
  [PXUIField]
  public virtual int? PayLinkID { get; set; }

  /// <summary>Payment Link delivery method (N - none, E - email).</summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PayLinkDeliveryMethod.List]
  [PXUIField(DisplayName = "Link Delivery Method")]
  public virtual 
  #nullable disable
  string DeliveryMethod { get; set; }

  /// <summary>Id of Processing Center related to Payment link.</summary>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Processing Center")]
  public virtual string ProcessingCenterID { get; set; }

  /// <summary>Currency Id of Payment Link.</summary>
  [PXDBString(5, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Currency")]
  public virtual string CuryID { get; set; }

  /// <summary>Amount to be payed by Payment Link.</summary>
  [PXDBCury(typeof (CCPayLink.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? Amount { get; set; }

  /// <summary>Due date transferred to the Payment Link webportal.</summary>
  [PXDBDate]
  [PXDefault]
  public virtual DateTime? DueDate { get; set; }

  /// <exclude />
  [PXDBString(3)]
  [PXUIField(DisplayName = "Doc. Type", Enabled = false)]
  public virtual string DocType { get; set; }

  /// <exclude />
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Doc. Reference Nbr.", Enabled = false)]
  public virtual string RefNbr { get; set; }

  /// <exclude />
  [PXDBString(2)]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  public virtual string OrderType { get; set; }

  /// <exclude />
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Order Reference Nbr.", Enabled = false)]
  public virtual string OrderNbr { get; set; }

  /// <summary>
  /// Interaction with the Payment link webportal (I - insert,  U - update, R - read, C - close).
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("I")]
  [PayLinkAction.List]
  public virtual string Action { get; set; }

  /// <summary>
  /// Result of the last interaction with the Payment Link webportal (O - open, S - success, E - error).
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("O")]
  [PayLinkActionStatus.List]
  public virtual string ActionStatus { get; set; }

  /// <summary>
  /// Date of the last interaction with the Payment Link webportal.
  /// </summary>
  [PXDefault]
  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Status Date")]
  public DateTime? StatusDate { get; set; }

  /// <summary>
  /// Link status of Payment Link (N - none, O - open, C - closed).
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PayLinkStatus.List]
  [PXUIField(DisplayName = "Link Status", Enabled = false)]
  public virtual string LinkStatus { get; set; }

  /// <summary>
  /// Payment status of Payment Link (N - none, U - unpaid, I - incomplete, P - paid).
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PayLinkPaymentStatus.List]
  [PXUIField(DisplayName = "Payment Status", Enabled = false)]
  public virtual string PaymentStatus { get; set; }

  /// <summary>Need update Payment Link after the document update.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Synchronization Required", Enabled = false)]
  public virtual bool? NeedSync { get; set; }

  /// <summary>
  /// Need update a report attached to the Payment Link after the document update.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? NeedReportSync { get; set; }

  /// <summary>
  /// External ID of a report file attached to the Payment Link.
  /// </summary>
  [PXDBString(100, IsUnicode = true)]
  public virtual string ReportAttachmentID { get; set; }

  /// <summary>URL of Payment Link.</summary>
  [PXDBString(300, IsUnicode = true)]
  [PXUIField(DisplayName = "Payment Link", Enabled = false)]
  public virtual string Url { get; set; }

  /// <summary>Payment Link webportal specific Id.</summary>
  [PXDBString(300, IsUnicode = true)]
  [PXUIField(DisplayName = "Link External ID", Enabled = false)]
  public virtual string ExternalID { get; set; }

  /// <exclude />
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Error Message")]
  public virtual string ErrorMessage { get; set; }

  /// <exclude />
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  /// <exclude />
  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  /// <exclude />
  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  /// <exclude />
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  /// <exclude />
  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  /// <exclude />
  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  /// <exclude />
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  /// <exclude />
  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<CCPayLink>.By<CCPayLink.payLinkID>
  {
    public static CCPayLink Find(PXGraph graph, int? tranNbr)
    {
      return (CCPayLink) PrimaryKeyOf<CCPayLink>.By<CCPayLink.payLinkID>.FindBy(graph, (object) tranNbr, (PKFindOptions) 0);
    }
  }

  public static class FK
  {
    public class ARInvoice : 
      PrimaryKeyOf<PX.Objects.AR.ARInvoice>.By<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>.ForeignKeyOf<CCPayLink>.By<CCPayLink.docType, CCPayLink.refNbr>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<CCPayLink>.By<CCPayLink.orderType, CCPayLink.orderNbr>
    {
    }
  }

  public abstract class payLinkID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCPayLink.payLinkID>
  {
  }

  public abstract class deliveryMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.deliveryMethod>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCPayLink.processingCenterID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.curyID>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CCPayLink.amount>
  {
  }

  public abstract class dueDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CCPayLink.dueDate>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.refNbr>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.orderNbr>
  {
  }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.action>
  {
  }

  public abstract class actionStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.actionStatus>
  {
  }

  public abstract class statusDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CCPayLink.statusDate>
  {
  }

  public abstract class linkStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.linkStatus>
  {
  }

  public abstract class paymentStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.paymentStatus>
  {
  }

  public abstract class needSync : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CCPayLink.needSync>
  {
  }

  public abstract class needReportSync : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CCPayLink.needReportSync>
  {
  }

  public abstract class reportAttachmentID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCPayLink.reportAttachmentID>
  {
  }

  public abstract class url : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.url>
  {
  }

  public abstract class externalID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.externalID>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R2.", true)]
  public abstract class erorMessage : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.erorMessage>
  {
  }

  public abstract class errorMessage : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCPayLink.errorMessage>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCPayLink.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCPayLink.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCPayLink.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCPayLink.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCPayLink.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCPayLink.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCPayLink.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CCPayLink.Tstamp>
  {
  }
}
