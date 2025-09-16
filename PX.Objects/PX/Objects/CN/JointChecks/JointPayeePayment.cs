// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.JointChecks.JointPayeePayment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Serialization;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.CN.JointChecks;

[PXSerializable]
[PXCacheName("Joint Payee Payment")]
public class JointPayeePayment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _NoteID;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  public virtual int? JointPayeePaymentId { get; set; }

  [PXDBInt]
  [PXForeignReference(typeof (JointPayeePayment.FK.JointPayee))]
  public virtual int? JointPayeeId { get; set; }

  [PXInt]
  [PXUnboundDefault(typeof (SelectFromBase<JointPayee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<JointPayee.jointPayeeId, IBqlInt>.IsEqual<BqlField<JointPayeePayment.jointPayeeId, IBqlInt>.FromCurrent>>), SourceField = typeof (JointPayee.aPLineNbr))]
  [PXUIField]
  public virtual int? BillLineNumber { get; set; }

  [PXParent(typeof (Select<APPayment, Where<APPayment.docType, Equal<Current<JointPayeePayment.paymentDocType>>, And<APPayment.refNbr, Equal<Current<JointPayeePayment.paymentRefNbr>>>>>))]
  [PXDBString]
  public virtual string PaymentRefNbr { get; set; }

  [PXDBString]
  public virtual string PaymentDocType { get; set; }

  [PXDBString]
  [PXSelector(typeof (Search<APInvoice.refNbr, Where<APInvoice.docType, Equal<APDocType.invoice>>>), SubstituteKey = typeof (APInvoice.refNbr))]
  [PXUIField(DisplayName = "AP Bill Nbr.", Enabled = false)]
  public virtual string InvoiceRefNbr { get; set; }

  [PXDBString]
  public virtual string InvoiceDocType { get; set; }

  [PXDBInt]
  public virtual int? AdjustmentNumber { get; set; }

  [PXDBCurrency(typeof (APRegister.curyInfoID), typeof (JointPayeePayment.jointAmountToPay))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Joint Amount To Pay")]
  public virtual Decimal? CuryJointAmountToPay { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? JointAmountToPay { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsVoided { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public static class FK
  {
    public class JointPayee : 
      PrimaryKeyOf<JointPayee>.By<JointPayee.jointPayeeId>.ForeignKeyOf<JointPayeePayment>.By<JointPayeePayment.jointPayeeId>
    {
    }
  }

  public abstract class jointPayeePaymentId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    JointPayeePayment.jointPayeePaymentId>
  {
  }

  public abstract class jointPayeeId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  JointPayeePayment.jointPayeeId>
  {
  }

  public abstract class billLineNumber : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    JointPayeePayment.billLineNumber>
  {
  }

  public abstract class paymentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    JointPayeePayment.paymentRefNbr>
  {
  }

  public abstract class paymentDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    JointPayeePayment.paymentDocType>
  {
  }

  public abstract class invoiceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    JointPayeePayment.invoiceRefNbr>
  {
  }

  public abstract class invoiceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    JointPayeePayment.invoiceDocType>
  {
  }

  public abstract class adjustmentNumber : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    JointPayeePayment.adjustmentNumber>
  {
  }

  public abstract class curyJointAmountToPay : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    JointPayeePayment.curyJointAmountToPay>
  {
  }

  public abstract class jointAmountToPay : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    JointPayeePayment.jointAmountToPay>
  {
  }

  public abstract class isVoided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  JointPayeePayment.isVoided>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  JointPayeePayment.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  JointPayeePayment.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  JointPayeePayment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    JointPayeePayment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    JointPayeePayment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    JointPayeePayment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    JointPayeePayment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    JointPayeePayment.lastModifiedDateTime>
  {
  }
}
