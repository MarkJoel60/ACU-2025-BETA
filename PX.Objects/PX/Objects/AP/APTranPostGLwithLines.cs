// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTranPostGLwithLines
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>AP Document Post GL with Lines.</summary>
[PXProjection(typeof (SelectFrom<APRegisterReport, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Cross<APAROrd>>, FbqlJoins.Left<APTran>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegisterReport.docType, Equal<APTran.tranType>>>>, PX.Data.And<BqlOperand<APRegisterReport.refNbr, IBqlString>.IsEqual<APTran.refNbr>>>>.And<BqlOperand<APRegisterReport.paymentsByLinesAllowed, IBqlBool>.IsEqual<True>>>>>.LeftJoin<APTranPostGL>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegisterReport.docType, Equal<APTranPostGL.docType>>>>, PX.Data.And<BqlOperand<APRegisterReport.refNbr, IBqlString>.IsEqual<APTranPostGL.refNbr>>>, PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPostGL.lineNbr, Equal<APTran.lineNbr>>>>>.Or<BqlOperand<APTranPostGL.lineNbr, IBqlInt>.IsEqual<Zero>>>>, PX.Data.And<BqlOperand<APTranPostGL.type, IBqlString>.IsNotEqual<APTranPost.type.origin>>>>.And<BqlOperand<APAROrd.ord, IBqlShort>.IsEqual<decimal1>>>), Persistent = false)]
[PXCacheName("AP Document Post GL with Lines")]
public class APTranPostGLwithLines : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The type of the document.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <list>
  /// <item><description>INV: Invoice</description></item>
  /// <item><description>ACR: Credit Adjustment</description></item>
  /// <item><description>ADR: Debit Adjustment</description></item>
  /// <item><description>CHK: Payment</description></item>
  /// <item><description>VCK: Voided Payment</description></item>
  /// <item><description>PPM: Prepayment</description></item>
  /// <item><description>REF: Refund</description></item>
  /// <item><description>VRF: Voided Refund</description></item>
  /// <item><description>QCK: Cash Purchase</description></item>
  /// <item><description>VQC: Voided Cash Purchase</description></item>
  /// </list>
  /// </value>
  [PXDBString(IsKey = true, BqlTable = typeof (APRegisterReport))]
  [PXUIField(DisplayName = "Doc. Type")]
  [APDocType.List]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  /// <summary>Reference number of the document.</summary>
  [PXDBString(IsKey = true, BqlTable = typeof (APRegisterReport))]
  [PXUIField(DisplayName = "Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string RefNbr { get; set; }

  /// <summary>Type of the original (source) document.</summary>
  [PXDBString(3, IsFixed = true, BqlTable = typeof (APRegisterReport))]
  [APDocType.List]
  [PXUIField(DisplayName = "Orig. Doc. Type")]
  public virtual string OrigDocType { get; set; }

  /// <summary>Reference number of the original (source) document.</summary>
  [PXDBString(15, IsUnicode = true, InputMask = "", BqlTable = typeof (APRegisterReport))]
  [PXUIField(DisplayName = "Orig. Ref. Nbr.")]
  public virtual string OrigRefNbr { get; set; }

  /// <summary>The type of the transaction.</summary>
  [PXString]
  [PXDBCalced(typeof (IsNull<APTranPostGL.tranType, APTran.tranType>), typeof (string))]
  public virtual string TranType { get; set; }

  /// <summary>
  /// The field is used in reports for joining and filtering purposes.
  /// </summary>
  [PXDBShort(IsKey = true, BqlTable = typeof (APAROrd))]
  public virtual short? Ord { get; set; }

  /// <summary>
  /// Type of the document for displaying in reports.
  /// This field has the same set of possible internal values as the <see cref="P:PX.Objects.AP.APTranPostGLwithLines.DocType" /> field,
  /// but exposes different user-friendly values.
  /// </summary>
  [PXString(3, IsFixed = true)]
  [APDocType.PrintList]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.Visible, Enabled = true)]
  public virtual string PrintDocType
  {
    get => this.DocType;
    set
    {
    }
  }

  /// <summary>The number of the transaction line in the document.</summary>
  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.")]
  [PXDBCalced(typeof (IsNull<APTranPostGL.lineNbr, IsNull<APTran.lineNbr, Zero>>), typeof (int))]
  public virtual int? LineNbr { get; set; }

  /// <summary>Id of corresponding APTranPost table record.</summary>
  [PXInt(IsKey = true)]
  [PXDBCalced(typeof (IsNull<APTranPostGL.iD, Zero>), typeof (int))]
  public virtual int? ID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> with which the item is associated or the non-project code if the item is not intended for any project.
  /// The field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.ProjectAccounting">Project Accounting</see> feature is enabled.
  /// </summary>
  [PXInt(IsKey = true)]
  [PXDBCalced(typeof (IsNull<APTran.projectID, APRegisterReport.projectID>), typeof (int))]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the document was released.
  /// </summary>
  [PX.Objects.GL.Released(PreventDeletingReleased = true, BqlTable = typeof (APRegisterReport))]
  public virtual bool? Released { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the document is open.
  /// </summary>
  [PXDBBool(BqlTable = typeof (APRegisterReport))]
  public virtual bool? OpenDoc { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the document was prebooked.
  /// </summary>
  [PXDBBool(BqlTable = typeof (APRegisterReport))]
  public virtual bool? Prebooked { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the document was voided. In this case <see cref="!:VoidBatchNbr" /> field will hold the number of the voiding <see cref="T:PX.Objects.GL.Batch" />.
  /// </summary>
  [PXDBBool(BqlTable = typeof (APRegisterReport))]
  public virtual bool? Voided { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.AP.Vendor" />, whom the document belongs to.
  /// </summary>
  [VendorActive(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof (Vendor.acctName), CacheGlobal = true, Filterable = true, BqlTable = typeof (APRegisterReport))]
  [PXDefault]
  public virtual int? VendorID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the document belongs.
  /// </summary>
  [Branch(null, null, true, true, true, BqlTable = typeof (APRegisterReport))]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The <see cref="!:PX.Objects.GL.FinancialPeriod">Financial Period</see>, in which the document was closed.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AP.APTranPostGLwithLines.FinPeriodID" /> field.
  /// </value>
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlTable = typeof (APRegisterReport))]
  public virtual string ClosedFinPeriodID { get; set; }

  /// <summary>
  /// The <see cref="!:PX.Objects.GL.FinancialPeriod">Financial Period</see>, in which the document was closed.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AP.APTranPostGLwithLines.TranPeriodID" /> field.
  /// </value>
  [PeriodID(null, null, null, true, BqlTable = typeof (APRegisterReport))]
  public virtual string ClosedTranPeriodID { get; set; }

  /// <summary>Source document doc type.</summary>
  [PXDBString(BqlTable = typeof (APTranPostGL))]
  [PXUIField(DisplayName = "Source Doc. Type")]
  [APDocType.List]
  public virtual string SourceDocType { get; set; }

  /// <summary>Source document ref number.</summary>
  [PXDBString(BqlTable = typeof (APTranPostGL))]
  [PXUIField(DisplayName = "Source Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string SourceRefNbr { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.Extensions.Currency" /> of this Currency Info object.
  /// </summary>
  [PXDBString(BqlTable = typeof (PX.Objects.CM.Extensions.CurrencyInfo))]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
  /// </summary>
  [PXDBLong(BqlTable = typeof (APTranPostGL))]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>AccountID of corresponding APTranPostGL table record.</summary>
  [Account(BqlTable = typeof (APTranPostGL))]
  public virtual int? AccountID { get; set; }

  /// <summary>
  /// SubID Account ID of corresponding APTranPostGL table record.
  /// </summary>
  [SubAccount(BqlTable = typeof (APTranPostGL))]
  public virtual int? SubID { get; set; }

  /// <summary>
  /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
  /// </summary>
  [PXString]
  [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APAROrd.ord, IBqlShort>.IsEqual<Zero>>, APRegisterReport.finPeriodID>, APTranPostGL.finPeriodID>), typeof (string))]
  [PXUIField(DisplayName = "Application Period")]
  public virtual string FinPeriodID { get; set; }

  /// <summary>
  /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
  /// </summary>
  [PXString]
  [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APAROrd.ord, IBqlShort>.IsEqual<Zero>>, APRegisterReport.tranPeriodID>, APTranPostGL.tranPeriodID>), typeof (string))]
  public virtual string TranPeriodID { get; set; }

  [PeriodID(null, null, null, true, BqlField = typeof (APRegisterReport.tranPeriodID))]
  public virtual string APRegisterTranPeriodID { get; set; }

  /// <summary>Date of the document.</summary>
  [PXDBDate(BqlField = typeof (APRegisterReport.docDate))]
  [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? APRegisterDocDate { get; set; }

  /// <summary>Date of the document.</summary>
  [PXDate]
  [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APAROrd.ord, IBqlShort>.IsEqual<Zero>>, APRegisterReport.docDate>, APTranPostGL.docDate>), typeof (System.DateTime?))]
  [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? DocDate { get; set; }

  /// <summary>The date of the last application.</summary>
  [PXDBDate(BqlTable = typeof (APRegisterReport))]
  public virtual System.DateTime? ClosedDate { get; set; }

  /// <summary>Sign of the Balance.</summary>
  [PXDBShort(BqlTable = typeof (APTranPostGL))]
  public virtual short? BalanceSign { get; set; }

  /// <summary>Transaction type.</summary>
  [PXDBString(BqlTable = typeof (APTranPostGL))]
  [APTranPost.type.List]
  public virtual string Type { get; set; }

  /// <summary>Transaction class.</summary>
  [PXDBString(BqlTable = typeof (APTranPostGL))]
  public virtual string TranClass { get; set; }

  /// <summary>Transaction ref number.</summary>
  [PXDBString(BqlTable = typeof (APTranPostGL))]
  public virtual string TranRefNbr { get; set; }

  /// <summary>Transaction Reference ID.</summary>
  [PXDBInt(BqlTable = typeof (APTranPostGL))]
  public virtual int? ReferenceID { get; set; }

  /// <summary>AP document transaction note id.</summary>
  [PXDBGuid(false, BqlTable = typeof (APTranPostGL))]
  public virtual Guid? RefNoteID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summary>
  [PXDBBool(BqlTable = typeof (APTranPostGL))]
  public virtual bool? IsMigratedRecord { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// with activated <see cref="P:PX.Objects.CS.FeaturesSet.PaymentsByLines" /> feature and
  /// such document allow payments by lines.
  /// </summary>
  [PXDBBool(BqlTable = typeof (APRegisterReport))]
  [PXDefault(false)]
  public virtual bool? PaymentsByLinesAllowed { get; set; }

  /// <summary>
  /// The signed amount to be paid for the document in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APAROrd.ord, IBqlShort>.IsEqual<Zero>>, IsNull<BqlOperand<APRegisterReport.signBalance, IBqlDecimal>.Multiply<APTran.origTranAmt>, BqlOperand<APRegisterReport.signBalance, IBqlDecimal>.Multiply<APRegisterReport.origDocAmt>>>, Zero>), typeof (Decimal))]
  public virtual Decimal? OrigBalanceAmt { get; set; }

  /// <summary>Balance amtount.</summary>
  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APAROrd.ord, IBqlShort>.IsEqual<decimal1>>, APTranPostGL.balanceAmt>, Zero>), typeof (Decimal))]
  public virtual Decimal? BalanceAmt { get; set; }

  /// <summary>Debit AP amount.</summary>
  [PXCurrency(typeof (APTranPostGLwithLines.curyInfoID), typeof (APTranPostGLwithLines.debitAPAmt), BaseCalc = false, BqlTable = typeof (APTranPostGL))]
  [PXUIField(DisplayName = "Debit AP Amt.")]
  public virtual Decimal? CuryDebitAPAmt { get; set; }

  /// <summary>Debit AP amount in the base currency of the company.</summary>
  [PXBaseCury(BqlTable = typeof (APTranPostGL))]
  public virtual Decimal? DebitAPAmt { get; set; }

  /// <summary>Credit AP amount.</summary>
  [PXCurrency(typeof (APTranPostGLwithLines.curyInfoID), typeof (APTranPostGLwithLines.creditAPAmt), BaseCalc = false, BqlTable = typeof (APTranPostGL))]
  [PXUIField(DisplayName = "Credit AP Amt.")]
  public virtual Decimal? CuryCreditAPAmt { get; set; }

  /// <summary>Credit AP amount in the base currency of the company.</summary>
  [PXBaseCury(BqlTable = typeof (APTranPostGL))]
  public virtual Decimal? CreditAPAmt { get; set; }

  /// <exclude />
  [PXCurrency(typeof (APTranPostGLwithLines.curyInfoID), typeof (APTranPostGLwithLines.turnDiscAmt), BaseCalc = false, BqlTable = typeof (APTranPostGL))]
  public virtual Decimal? CuryTurnDiscAmt { get; set; }

  /// <exclude />
  [PXBaseCury(BqlTable = typeof (APTranPostGL))]
  public virtual Decimal? TurnDiscAmt { get; set; }

  /// <exclude />
  [PXCurrency(typeof (APTranPostGLwithLines.curyInfoID), typeof (APTranPostGLwithLines.turnWhTaxAmt), BaseCalc = false, BqlTable = typeof (APTranPostGL))]
  public virtual Decimal? CuryTurnWHTaxAmt { get; set; }

  /// <exclude />
  [PXBaseCury(BqlTable = typeof (APTranPostGL))]
  public virtual Decimal? TurnWHTaxAmt { get; set; }

  /// <exclude />
  [PXBaseCury(BqlTable = typeof (APTranPostGL))]
  public virtual Decimal? TurnRGOLAmt { get; set; }

  /// <summary>Realized gains or losses amount.</summary>
  [PXDBBaseCury(BqlTable = typeof (APTranPostGL))]
  public virtual Decimal? RGOLAmt { get; set; }

  /// <summary>Original retainage amount.</summary>
  [PXBaseCury]
  [PXDBCalced(typeof (Mult<APRegisterReport.signAmount, Switch<Case<PX.Data.Where<BqlOperand<APAROrd.ord, IBqlShort>.IsEqual<Zero>>, IsNull<APTran.origRetainageAmt, APRegisterReport.retainageTotal>>, Zero>>), typeof (Decimal))]
  [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
  public virtual Decimal? OrigRetainageAmt { get; set; }

  /// <summary>Released retainage amount.</summary>
  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APAROrd.ord, IBqlShort>.IsEqual<decimal1>>, BqlOperand<APTranPostGL.balanceSign, IBqlShort>.Multiply<APTranPostGL.retainageReleasedAmt>>, Zero>), typeof (Decimal))]
  public virtual Decimal? ReleasedRetainageAmt { get; set; }

  public class PK : 
    PrimaryKeyOf<APTranPostGLwithLines>.By<APTranPostGLwithLines.projectID, APTranPostGLwithLines.docType, APTranPostGLwithLines.refNbr, APTranPostGLwithLines.ord, APTranPostGLwithLines.lineNbr, APTranPostGLwithLines.iD>
  {
    public static APTranPostGLwithLines Find(
      PXGraph graph,
      int projectID,
      string docType,
      string refNbr,
      int? ord,
      int? lineNbr,
      int? iD)
    {
      return PrimaryKeyOf<APTranPostGLwithLines>.By<APTranPostGLwithLines.projectID, APTranPostGLwithLines.docType, APTranPostGLwithLines.refNbr, APTranPostGLwithLines.ord, APTranPostGLwithLines.lineNbr, APTranPostGLwithLines.iD>.FindBy(graph, (object) projectID, (object) docType, (object) refNbr, (object) ord, (object) lineNbr, (object) iD);
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGLwithLines.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGLwithLines.refNbr>
  {
  }

  public abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranPostGLwithLines.origDocType>
  {
  }

  public abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranPostGLwithLines.origRefNbr>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGLwithLines.tranType>
  {
  }

  public abstract class ord : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  APTranPostGLwithLines.ord>
  {
  }

  public abstract class printDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranPostGLwithLines.printDocType>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGLwithLines.lineNbr>
  {
  }

  public abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGLwithLines.iD>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGLwithLines.projectID>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTranPostGLwithLines.released>
  {
  }

  public abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTranPostGLwithLines.openDoc>
  {
  }

  public abstract class prebooked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTranPostGLwithLines.prebooked>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTranPostGLwithLines.voided>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGLwithLines.vendorID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGLwithLines.branchID>
  {
  }

  public abstract class closedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranPostGLwithLines.closedFinPeriodID>
  {
  }

  public abstract class closedTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranPostGLwithLines.closedTranPeriodID>
  {
  }

  public abstract class sourceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranPostGLwithLines.sourceDocType>
  {
  }

  public abstract class sourceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranPostGLwithLines.sourceRefNbr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APTranPostGLwithLines.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APTranPostGLwithLines.curyInfoID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGLwithLines.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGLwithLines.subID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranPostGLwithLines.finPeriodID>
  {
  }

  public abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranPostGLwithLines.tranPeriodID>
  {
  }

  public abstract class aPRegisterTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranPostGLwithLines.aPRegisterTranPeriodID>
  {
  }

  public abstract class aPRegisterDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APTranPostGLwithLines.aPRegisterDocDate>
  {
  }

  public abstract class docDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APTranPostGLwithLines.docDate>
  {
  }

  public abstract class closedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APTranPostGLwithLines.closedDate>
  {
  }

  public abstract class balanceSign : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    APTranPostGLwithLines.balanceSign>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGLwithLines.type>
  {
  }

  public abstract class tranClass : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranPostGLwithLines.tranClass>
  {
  }

  public abstract class tranRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranPostGLwithLines.tranRefNbr>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGLwithLines.referenceID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APTranPostGLwithLines.refNoteID>
  {
  }

  public abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APTranPostGLwithLines.isMigratedRecord>
  {
  }

  public abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APTranPostGLwithLines.paymentsByLinesAllowed>
  {
  }

  public abstract class origBalanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGLwithLines.origBalanceAmt>
  {
  }

  public abstract class balanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGLwithLines.balanceAmt>
  {
  }

  public abstract class curyDebitAPAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGLwithLines.curyDebitAPAmt>
  {
  }

  public abstract class debitAPAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGLwithLines.debitAPAmt>
  {
  }

  public abstract class curyCreditAPAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGLwithLines.curyCreditAPAmt>
  {
  }

  public abstract class creditAPAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGLwithLines.creditAPAmt>
  {
  }

  public abstract class curyTurnDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGLwithLines.curyTurnDiscAmt>
  {
  }

  public abstract class turnDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGLwithLines.turnDiscAmt>
  {
  }

  public abstract class curyTurnWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGLwithLines.curyTurnWhTaxAmt>
  {
  }

  public abstract class turnWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGLwithLines.turnWhTaxAmt>
  {
  }

  public abstract class turnRGOLAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGLwithLines.turnRGOLAmt>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostGLwithLines.rGOLAmt>
  {
  }

  public abstract class origRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGLwithLines.origRetainageAmt>
  {
  }

  public abstract class releasedRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGLwithLines.releasedRetainageAmt>
  {
  }
}
