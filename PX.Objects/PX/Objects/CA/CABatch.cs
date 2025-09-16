// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABatch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// The main properties of batch payments and their classes.
/// Batch payments are edited on the Batch Payments (AP305000) form
/// (which corresponds to the <see cref="T:PX.Objects.CA.CABatchEntry" /> graph).
/// </summary>
[PXPrimaryGraph(typeof (CABatchEntry))]
[PXCacheName("CA Batch")]
[Serializable]
public class CABatch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, INotable
{
  /// <summary>
  /// The user-friendly unique identifier of the batch number.
  /// This field is the key field.
  /// </summary>
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [CABatchType.Numbering]
  [CABatchType.RefNbr(typeof (Search<CABatch.batchNbr, Where<CABatch.origModule, Equal<BatchModule.moduleAP>>>))]
  public virtual 
  #nullable disable
  string BatchNbr { get; set; }

  /// <summary>Module from which the document originates.</summary>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("AP")]
  [PXStringList(new string[] {"AP", "AR", "PR"}, new string[] {"AP", "AR", "PR"})]
  [PXUIField(DisplayName = "Module", Enabled = false)]
  public virtual string OrigModule { get; set; }

  /// <summary>
  /// The cash account used for payment.
  /// Corresponds to the <see cref="P:PX.Objects.CA.CashAccount.CashAccountID" /> field.
  /// </summary>
  [CashAccount(null, typeof (Search<CashAccount.cashAccountID, Where2<Match<Current<AccessInfo.userName>>, And<CashAccount.clearingAccount, Equal<boolFalse>>>>))]
  [PXDefault]
  public virtual int? CashAccountID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, to which the batch belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(typeof (Search<CashAccount.branchID, Where<CashAccount.cashAccountID, Equal<Current<CABatch.cashAccountID>>>>), null, true, true, true)]
  [PXFormula(typeof (Default<CABatch.cashAccountID>))]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The payment method associated with the cash account. Only payment methods that allow batch creation appear in the list.
  /// Payment methods are defined on the Payment Methods (CA204000) form.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Payment Method")]
  [PXSelector(typeof (SearchFor<PaymentMethod.paymentMethodID>.In<SelectFromBase<PaymentMethod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PaymentMethodAccount>.On<BqlOperand<PaymentMethod.paymentMethodID, IBqlString>.IsEqual<PaymentMethodAccount.paymentMethodID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PaymentMethod.aPCreateBatchPayment, Equal<True>>>>, And<BqlOperand<Current<CABatch.origModule>, IBqlString>.IsEqual<BatchModule.moduleAP>>>>.And<BqlOperand<PaymentMethodAccount.cashAccountID, IBqlInt>.IsEqual<BqlField<CABatch.cashAccountID, IBqlInt>.FromCurrent>>>>))]
  public virtual string PaymentMethodID { get; set; }

  /// <summary>
  /// The unique identifier of the bank that will process the batch of payments.
  /// </summary>
  [PXDBInt]
  [PXDefault(typeof (Search<CashAccount.referenceID, Where<CashAccount.cashAccountID, Equal<Current<CABatch.cashAccountID>>>>))]
  [PXSelector(typeof (PX.Objects.CR.BAccount.bAccountID), SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
  [PXUIField]
  public virtual int? ReferenceID { get; set; }

  /// <summary>
  /// The unique number automatically assigned to the batch.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [BatchRef(typeof (CABatch.cashAccountID), typeof (CABatch.paymentMethodID))]
  [PXUIField]
  [PXDefault]
  public virtual string BatchSeqNbr { get; set; }

  /// <summary>
  /// Any document that holds information about the batch as required by your company's internal policies.
  /// </summary>
  [PXDBString(40, IsUnicode = true)]
  [PXUIField]
  public virtual string ExtRefNbr { get; set; }

  /// <summary>The date when the batch was created.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? TranDate { get; set; }

  /// <summary>
  /// A description of the batch, which may help to identify it.
  /// </summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string TranDesc { get; set; }

  /// <summary>
  /// The unique number automatically assigned to the batch to distinguish it from other batches generated during the same day.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Seq. Number Within Day", Enabled = false)]
  public virtual short? DateSeqNbr { get; set; }

  /// <summary>
  /// Specifies what workflow is applied to the batch:
  /// if set to <c>true</c> the batch should it be exported before release..
  /// if set to <c>false</c> the batch should it be released before export.
  /// </summary>
  [PXDBBool]
  [PXDBDefault(typeof (Search<PaymentMethod.skipExport, Where<PaymentMethod.paymentMethodID, Equal<Current<CABatch.paymentMethodID>>>>))]
  [PXUIField(DisplayName = "Release Batch Payment Before Export")]
  public virtual bool? SkipExport { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the batch is on hold and cannot be exported.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (Search<CASetup.holdEntry>))]
  [PXUIField(DisplayName = "Hold")]
  public virtual bool? Hold { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the batch is exported.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Exported")]
  public virtual bool? Exported { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the batch is canceled and its details were removed.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Canceled")]
  public virtual bool? Canceled { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the batch is voided.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Voided")]
  public virtual bool? Voided { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the batch is released.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = true)]
  public virtual bool? Released { get; set; }

  /// <summary>
  /// The status of the batch, which the system assigns automatically.
  /// This is a virtual field and it has no representation in the database.
  /// A batch can have one of the following statuses:
  /// <c>"H"</c>: On Hold,
  /// <c>"B"</c>: Balanced,
  /// <c>"R"</c>: Released.
  /// </summary>
  [PXString(1, IsFixed = true)]
  [PXDefault("B")]
  [PXUIField]
  [CABatchStatus.List]
  public virtual string Status
  {
    [PXDependsOnFields(new System.Type[] {typeof (CABatch.hold), typeof (CABatch.released), typeof (CABatch.exported), typeof (CABatch.canceled), typeof (CABatch.voided)})] get
    {
      if (this.Hold.HasValue && this.Hold.GetValueOrDefault())
        return "H";
      if (this.Exported.HasValue && this.Exported.GetValueOrDefault() && this.Canceled.HasValue && !this.Canceled.GetValueOrDefault() && this.Released.HasValue && !this.Released.GetValueOrDefault())
        return "P";
      if (this.Canceled.HasValue && this.Canceled.GetValueOrDefault() && !this.Released.GetValueOrDefault())
        return "C";
      if (this.Released.HasValue && this.Released.GetValueOrDefault() && !this.Voided.GetValueOrDefault())
        return "R";
      return this.Voided.HasValue && this.Voided.GetValueOrDefault() ? "V" : "B";
    }
    set
    {
    }
  }

  /// <summary>
  /// The currency of the payment.
  /// Corresponds to the currency of the cash account.
  /// Depends on the <see cref="P:PX.Objects.CA.CABatch.CashAccountID" /> field.
  /// </summary>
  [PXString(5, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
  [PXDBScalar(typeof (Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<CABatch.cashAccountID>>>))]
  [PXUIField(DisplayName = "Currency")]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// The total amount for the batch, calculated as the sum of all payment amounts in the selected currency.
  /// </summary>
  [PXDBCury(typeof (CABatch.curyID))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDetailTotal { get; set; }

  /// <summary>
  /// The total amount for the batch, calculated as the sum of all payment amounts in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DetailTotal { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the document was cleared with the reconciliation source, generally based on preliminary information.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cleared", Visible = false)]
  public virtual bool? Cleared { get; set; }

  /// <summary>The date when the document was cleared.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Clear Date", Visible = false)]
  public virtual DateTime? ClearDate { get; set; }

  /// <summary>The date when the batch payments was voided.</summary>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Void Date", Enabled = true)]
  [PXUIVisible(typeof (Where<Current<CABatch.released>, Equal<True>>))]
  public virtual DateTime? VoidDate { get; set; }

  [PXSearchable(1, "Batch Payment: {0} - {2}", new System.Type[] {typeof (CABatch.batchNbr), typeof (CABatch.referenceID), typeof (PX.Objects.CR.BAccount.acctName)}, new System.Type[] {typeof (CABatch.paymentMethodID), typeof (CABatch.batchSeqNbr), typeof (CABatch.tranDesc), typeof (CABatch.extRefNbr), typeof (PX.Objects.CR.BAccount.acctCD)}, NumberFields = new System.Type[] {typeof (CABatch.batchNbr)}, Line1Format = "{0}{1:d}{2}", Line1Fields = new System.Type[] {typeof (CABatch.extRefNbr), typeof (CABatch.tranDate), typeof (CABatch.batchSeqNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (CABatch.tranDesc)})]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  /// <summary>The name of the exported file.</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string ExportFileName { get; set; }

  /// <summary>The time when the export was performed.</summary>
  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "File Export Time", Enabled = false)]
  public virtual DateTime? ExportTime { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField]
  public virtual int? CountOfPayments { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDecimal(4)]
  public virtual Decimal? Total { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the document is included in the reconciliation statement as a reconciled document.
  /// This field can be set to <c>true</c> only for batches for the cash accounts that have <see cref="P:PX.Objects.CA.CashAccount.MatchToBatch" /> set to true.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Reconciled { get; set; }

  /// <summary>The date when the document was reconciled.</summary>
  [PXDBDate]
  public virtual DateTime? ReconDate { get; set; }

  /// <summary>
  /// If the document was reconciled, the field contains the number of the reconciliation that includes this document.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  public virtual string ReconNbr { get; set; }

  [PXString]
  [PXFormula(typeof (SmartJoin<Space, Selector<CABatch.cashAccountID, CashAccount.cashAccountCD>, Selector<CABatch.cashAccountID, CashAccount.descr>>))]
  public string FormCaptionDescription { get; set; }

  public CATran CopyTo(CATran destination)
  {
    destination.TranDate = this.TranDate;
    destination.OrigModule = this.OrigModule;
    destination.OrigRefNbr = this.BatchNbr;
    destination.OrigTranType = "CBT";
    destination.OrigTranTypeUI = "CBT";
    destination.ExtRefNbr = this.ExtRefNbr;
    destination.TranDesc = this.TranDesc;
    CATran caTran1 = destination;
    Decimal? detailTotal = this.DetailTotal;
    Decimal? nullable1 = detailTotal.HasValue ? new Decimal?(-detailTotal.GetValueOrDefault()) : new Decimal?();
    caTran1.TranAmt = nullable1;
    CATran caTran2 = destination;
    Decimal? curyDetailTotal = this.CuryDetailTotal;
    Decimal? nullable2 = curyDetailTotal.HasValue ? new Decimal?(-curyDetailTotal.GetValueOrDefault()) : new Decimal?();
    caTran2.CuryTranAmt = nullable2;
    destination.Released = this.Released;
    destination.Hold = this.Hold;
    destination.Status = this.Status;
    destination.Cleared = this.Cleared;
    destination.ClearDate = this.ClearDate;
    destination.ReconNbr = this.ReconNbr;
    destination.ReconDate = this.ReconDate;
    destination.Reconciled = this.Reconciled;
    destination.DrCr = "C";
    destination.TranDesc = this.TranDesc;
    destination.ExtRefNbr = this.ExtRefNbr;
    destination.CashAccountID = this.CashAccountID;
    destination.CuryID = this.CuryID;
    return destination;
  }

  public class PK : PrimaryKeyOf<CABatch>.By<CABatch.batchNbr>
  {
    public static CABatch Find(PXGraph graph, string batchNbr, PKFindOptions options = 0)
    {
      return (CABatch) PrimaryKeyOf<CABatch>.By<CABatch.batchNbr>.FindBy(graph, (object) batchNbr, options);
    }
  }

  public static class FK
  {
    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CABatch>.By<CABatch.cashAccountID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.ForeignKeyOf<CABatch>.By<CABatch.paymentMethodID>
    {
    }

    public class BankBAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<CABatch>.By<CABatch.referenceID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CABatch>.By<CABatch.curyID>
    {
    }

    public class ReconciliationStatement : 
      PrimaryKeyOf<CARecon>.By<CARecon.cashAccountID, CARecon.reconNbr>.ForeignKeyOf<CABatch>.By<CABatch.cashAccountID, CABatch.reconNbr>
    {
    }
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatch.batchNbr>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatch.origModule>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABatch.cashAccountID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABatch.branchID>
  {
  }

  public abstract class paymentMethodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatch.paymentMethodID>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABatch.referenceID>
  {
  }

  public abstract class batchSeqNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatch.batchSeqNbr>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatch.extRefNbr>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CABatch.tranDate>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatch.tranDesc>
  {
  }

  public abstract class dateSeqNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CABatch.dateSeqNbr>
  {
  }

  public abstract class skipExport : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABatch.skipExport>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABatch.hold>
  {
  }

  public abstract class exported : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABatch.exported>
  {
  }

  public abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABatch.canceled>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABatch.voided>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABatch.released>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatch.status>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatch.curyID>
  {
  }

  public abstract class curyDetailTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABatch.curyDetailTotal>
  {
  }

  public abstract class detailTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABatch.detailTotal>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABatch.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CABatch.clearDate>
  {
  }

  public abstract class voidDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CABatch.voidDate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABatch.noteID>
  {
  }

  public abstract class exportFileName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatch.exportFileName>
  {
  }

  public abstract class exportTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CABatch.exportTime>
  {
  }

  public abstract class countOfPayments : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABatch.countOfPayments>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABatch.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABatch.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABatch.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABatch.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABatch.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABatch.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CABatch.Tstamp>
  {
  }

  public abstract class total : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABatch.total>
  {
  }

  public abstract class reconciled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABatch.reconciled>
  {
  }

  public abstract class reconDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CABatch.reconDate>
  {
  }

  public abstract class reconNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatch.reconNbr>
  {
  }
}
