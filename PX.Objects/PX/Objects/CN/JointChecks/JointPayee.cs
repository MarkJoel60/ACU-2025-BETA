// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.JointChecks.JointPayee
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using System;

#nullable enable
namespace PX.Objects.CN.JointChecks;

[PXCacheName("Joint Payee")]
[Serializable]
public class JointPayee : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  [PXReferentialIntegrityCheck]
  public virtual int? JointPayeeId { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? IsMainPayee { get; set; }

  [Vendor(DisplayName = "Joint Payee (Vendor)")]
  [PXUIVerify]
  public virtual int? JointPayeeInternalId { get; set; }

  [PXDBString(30)]
  [PXUIVerify]
  [PXUIField(DisplayName = "Joint Payee")]
  public virtual string JointPayeeExternalName { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the transaction.
  /// </summary>
  /// <value>
  /// Generated automatically. Corresponds to the <see cref="!:PX.Objects.CM.CurrencyInfo.CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo(typeof (APRegister.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (JointPayee.curyInfoID), typeof (JointPayee.jointAmountOwed))]
  [PXUIField(DisplayName = "Joint Amount Owed")]
  public virtual Decimal? CuryJointAmountOwed { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury]
  public virtual Decimal? JointAmountOwed { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (JointPayee.curyInfoID), typeof (JointPayee.jointAmountPaid))]
  [PXUIField(DisplayName = "Joint Amount Paid", IsReadOnly = true)]
  public virtual Decimal? CuryJointAmountPaid { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury]
  public virtual Decimal? JointAmountPaid { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (JointPayee.curyInfoID), typeof (JointPayee.jointBalance))]
  [PXFormula(typeof (Sub<JointPayee.curyJointAmountOwed, JointPayee.curyJointAmountPaid>))]
  [PXUIField(DisplayName = "Joint Balance", IsReadOnly = true)]
  public virtual Decimal? CuryJointBalance { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury]
  public virtual Decimal? JointBalance { get; set; }

  [PXDBString(3, IsFixed = true)]
  public virtual string APDocType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  public virtual string APRefNbr { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<APTran.lineNbr, Where<APTran.tranType, Equal<Current<APInvoice.docType>>, And<APTran.refNbr, Equal<Current<APInvoice.refNbr>>>>>), new Type[] {typeof (APTran.lineNbr), typeof (APTran.inventoryID), typeof (APTran.tranDesc), typeof (APTran.projectID), typeof (APTran.taskID), typeof (APTran.costCodeID), typeof (APTran.accountID), typeof (APTran.curyTranAmt)}, DirtyRead = true)]
  [PXUIField(DisplayName = "Bill Line Nbr.")]
  public virtual int? APLineNbr { get; set; }

  [PXFormula(typeof (Selector<JointPayee.aPLineNbr, APTran.curyLineAmt>))]
  [PXDecimal]
  [PXUIField]
  public virtual Decimal? BillLineAmount { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? LinkedToPayment { get; set; }

  [PXBool]
  public virtual bool? CanDelete { get; set; }

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

  public class PK : PrimaryKeyOf<JointPayee>.By<JointPayee.jointPayeeId>
  {
    public static JointPayee Find(PXGraph graph, int? jointPayeeId, PKFindOptions options = 0)
    {
      return (JointPayee) PrimaryKeyOf<JointPayee>.By<JointPayee.jointPayeeId>.FindBy(graph, (object) jointPayeeId, options);
    }
  }

  public abstract class jointPayeeId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  JointPayee.jointPayeeId>
  {
  }

  public abstract class isMainPayee : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  JointPayee.isMainPayee>
  {
  }

  public abstract class jointPayeeInternalId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    JointPayee.jointPayeeInternalId>
  {
  }

  public abstract class jointPayeeExternalName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    JointPayee.jointPayeeExternalName>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  JointPayee.curyInfoID>
  {
  }

  public abstract class curyJointAmountOwed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    JointPayee.curyJointAmountOwed>
  {
  }

  public abstract class jointAmountOwed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    JointPayee.jointAmountOwed>
  {
  }

  public abstract class curyJointAmountPaid : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    JointPayee.curyJointAmountPaid>
  {
  }

  public abstract class jointAmountPaid : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    JointPayee.jointAmountPaid>
  {
  }

  public abstract class curyJointBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    JointPayee.curyJointBalance>
  {
  }

  public abstract class jointBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  JointPayee.jointBalance>
  {
  }

  public abstract class aPDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  JointPayee.aPDocType>
  {
  }

  public abstract class aPRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  JointPayee.aPRefNbr>
  {
  }

  public abstract class aPLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  JointPayee.aPLineNbr>
  {
  }

  public abstract class billLineAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    JointPayee.billLineAmount>
  {
  }

  public abstract class linkedToPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  JointPayee.linkedToPayment>
  {
  }

  public abstract class canDelete : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  JointPayee.canDelete>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  JointPayee.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  JointPayee.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  JointPayee.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    JointPayee.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    JointPayee.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  JointPayee.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    JointPayee.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    JointPayee.lastModifiedDateTime>
  {
  }
}
