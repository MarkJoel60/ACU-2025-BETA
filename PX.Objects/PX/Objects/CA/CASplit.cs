// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CASplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// The main properties of CA transaction details.
/// CA transaction details are edited on the Cash Transactions (CA304000) form
/// (which corresponds to the <see cref="T:PX.Objects.CA.CATranEntry" /> graph).
/// </summary>
[PXCacheName("CA Transaction Details")]
[Serializable]
public class CASplit : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IDocumentTran,
  ICATranDetail
{
  protected int? _CostCodeID;

  /// <summary>The identifier of the branch of the parent document.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="T:PX.Objects.CA.CAAdj.branchID" /> field.
  /// </value>
  [Branch(typeof (CAAdj.branchID), null, true, true, true)]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The reference number of the parent document.
  /// This field is a part of the compound key of the document.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CAAdj.AdjRefNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CAAdj.adjRefNbr))]
  [PXParent(typeof (Select<CAAdj, Where<CAAdj.adjTranType, Equal<Current<CASplit.adjTranType>>, And<CAAdj.adjRefNbr, Equal<Current<CASplit.adjRefNbr>>>>>))]
  [PXUIField(Visible = false)]
  public virtual 
  #nullable disable
  string AdjRefNbr { get; set; }

  /// <summary>
  /// The type of the parent document.
  /// This field is a part of the compound key of the document.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CAAdj.AdjTranType" /> field.
  /// </value>
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (CAAdj.adjTranType))]
  [PXUIField(DisplayName = "Type", Visible = false)]
  public virtual string AdjTranType { get; set; }

  /// <summary>
  /// The number of the line in details of the <see cref="T:PX.Objects.CA.CAAdj" /> document.
  /// The value of this field affects the <see cref="P:PX.Objects.CA.CAAdj.LineCntr">counter</see> of the parent document.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXLineNbr(typeof (CAAdj.lineCntr))]
  public virtual int? LineNbr { get; set; }

  /// <summary>
  /// The user-friendly identifier of the non-stock item specified as the transaction subject.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [NonStockNonKitItem]
  [PXForeignReference(typeof (Field<CASplit.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  [PXUIField(DisplayName = "Item ID")]
  public virtual int? InventoryID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">unit of measure</see> for the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INUnit.FromUnit">INUnit.FromUnit</see> field.
  /// </value>
  [PXDefault(typeof (Coalesce<Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<CASplit.inventoryID>>, And<Current<CAAdj.drCr>, Equal<CADrCr.cACredit>>>>, Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<CASplit.inventoryID>>, And<Current<CAAdj.drCr>, Equal<CADrCr.cADebit>>>>>))]
  [INUnit(typeof (CASplit.inventoryID))]
  public virtual string UOM { get; set; }

  /// <summary>The quantity of the item.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty { get; set; }

  /// <summary>The unit price for the item in the base currency.</summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnitPrice { get; set; }

  /// <summary>The unit price for the item in the selected currency.</summary>
  [PXDBCurrencyPriceCost(typeof (CASplit.curyInfoID), typeof (CASplit.unitPrice))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Price")]
  public virtual Decimal? CuryUnitPrice { get; set; }

  /// <summary>
  /// The account to be updated by the transaction.
  /// By default, this is the offset account that is defined by the entry type selected for the cash account.
  /// </summary>
  [Account(typeof (CASplit.branchID))]
  [PXDefault]
  public virtual int? AccountID { get; set; }

  /// <summary>The subaccount to be used for the transaction.</summary>
  [SubAccount(typeof (CASplit.accountID), typeof (CASplit.branchID), false)]
  [PXDefault]
  public virtual int? SubID { get; set; }

  /// <summary>
  /// It is used only to pass ReclassificationProhibited flag to GL tran on Cash-in-Transit Account.
  /// It is not persisted.
  /// </summary>
  [PXBool]
  public virtual bool? ReclassificationProhibited { get; set; }

  /// <summary>
  /// The cash account of the line.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXRestrictor(typeof (Where<CashAccount.branchID, Equal<Current<CASplit.branchID>>>), "This Cash Account does not match selected Branch", new Type[] {})]
  [PXRestrictor(typeof (Where<CashAccount.curyID, Equal<Current<CAAdj.curyID>>>), "Offset account must be a Cash Account in the same currency as current Cash Account", new Type[] {})]
  [CashAccountScalar]
  [PXDBScalar(typeof (Search<CashAccount.cashAccountID, Where<CashAccount.accountID, Equal<CASplit.accountID>, And<CashAccount.subID, Equal<CASplit.subID>, And<CashAccount.branchID, Equal<CASplit.branchID>>>>>))]
  public virtual int? CashAccountID { get; set; }

  /// <summary>The tax category that applies to the transaction.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [CATax(typeof (CAAdj), typeof (CATax), typeof (CATaxTran), typeof (CAAdj.taxCalcMode), typeof (CAAdj.branchID), CuryOrigDocAmt = typeof (CAAdj.curyControlAmt), DocCuryTaxAmt = typeof (CAAdj.curyTaxAmt))]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.taxCategoryID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<CASplit.inventoryID>>>>))]
  public virtual string TaxCategoryID { get; set; }

  /// <summary>The description provided for the item.</summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  /// <summary>
  /// This field implements the member of the <see cref="!:IDocumentTran" /> interface.
  /// </summary>
  public DateTime? TranDate
  {
    get => new DateTime?();
    set
    {
    }
  }

  /// <summary>
  /// The identifier of the exchange rate record for the line.
  /// Corresponds to the <see cref="P:PX.Objects.CA.CAAdj.CuryInfoID" /> field.
  /// </summary>
  [PXDBLong]
  [CurrencyInfo(typeof (CAAdj.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// The total amount of this line in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CASplit.curyInfoID), typeof (CASplit.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? CuryTranAmt { get; set; }

  /// <summary>The total amount this line in the base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tran. Amount")]
  [PXFormula(null, typeof (SumCalc<CAAdj.splitTotal>))]
  public virtual Decimal? TranAmt { get; set; }

  /// <summary>
  /// The line total that is subjected to all taxes with calculation rule "Inclusive Line-Level" (see <see cref="M:PX.Objects.TX.TaxBaseAttribute.TaxSetLineDefault(PX.Data.PXCache,System.Object,System.Object)" />) in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CASplit.curyInfoID), typeof (CASplit.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  /// <summary>
  /// The line total that is subjected to all taxes with calculation rule "Inclusive Line-Level" (see <see cref="M:PX.Objects.TX.TaxBaseAttribute.TaxSetLineDefault(PX.Data.PXCache,System.Object,System.Object)" />) in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxableAmt { get; set; }

  /// <summary>
  /// The amount of tax (VAT) associated with the line in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CAAdj.curyInfoID), typeof (CAAdj.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxAmt { get; set; }

  /// <summary>
  /// The amount of tax (VAT) associated with the line in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAmt { get; set; }

  [PX.Objects.GL.FinPeriodID(null, typeof (CASplit.branchID), null, null, null, null, true, false, null, typeof (CASplit.tranPeriodID), typeof (CAAdj.tranPeriodID), true, true)]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  /// <summary>
  /// The project with which this transaction is associated, or the code indicating that this transaction is not associated with any project;
  /// the non-project code is specified on the Projects Preferences (PM101000) form.
  /// This field appears in the UI only if the Projects module has been enabled in your system and integrated with the Cash Management module.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="T:PX.Objects.PM.PMProject" />.
  /// </value>
  [ProjectDefault("CA", AccountType = typeof (CASplit.accountID))]
  [CAActiveProject]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// The particular task of the project with which this transaction is associated.
  /// This field appears in the UI only if the Projects module has been enabled in your system and integrated with the Cash Management module.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<CASplit.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ActiveProjectTask(typeof (CASplit.projectID), "CA", DisplayName = "Project Task")]
  public virtual int? TaskID { get; set; }

  [CostCode(null, typeof (CASplit.taskID), null)]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this transaction is non-billable in the project.
  /// This column appears only if the Projects module has been enabled in your system and integrated with the Cash Management module.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Non Billable", FieldClass = "PROJECT")]
  public virtual bool? NonBillable { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

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

  public string TranType
  {
    get => this.AdjTranType;
    set => this.AdjTranType = value;
  }

  public string RefNbr
  {
    get => this.AdjRefNbr;
    set => this.AdjRefNbr = value;
  }

  public Decimal? CuryCashDiscBal { get; set; }

  public Decimal? CashDiscBal { get; set; }

  public Decimal? CuryTranBal { get; set; }

  public Decimal? TranBal { get; set; }

  public class PK : PrimaryKeyOf<CASplit>.By<CASplit.adjTranType, CASplit.adjRefNbr, CASplit.lineNbr>
  {
    public static CASplit Find(
      PXGraph graph,
      string adjTranType,
      string adjRefNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (CASplit) PrimaryKeyOf<CASplit>.By<CASplit.adjTranType, CASplit.adjRefNbr, CASplit.lineNbr>.FindBy(graph, (object) adjTranType, (object) adjRefNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class CashTransaction : 
      PrimaryKeyOf<CAAdj>.By<CAAdj.adjTranType, CAAdj.adjRefNbr>.ForeignKeyOf<CASplit>.By<CASplit.adjTranType, CASplit.adjRefNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CASplit>.By<CASplit.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CASplit>.By<CASplit.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CASplit>.By<CASplit.subID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CASplit>.By<CASplit.cashAccountID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<CASplit>.By<CASplit.taxCategoryID>
    {
    }

    public class CurrenyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CASplit>.By<CASplit.curyInfoID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<CASplit>.By<CASplit.inventoryID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<CASplit>.By<CASplit.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<CASplit>.By<CASplit.projectID, CASplit.taskID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<CASplit>.By<CASplit.costCodeID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplit.branchID>
  {
  }

  public abstract class adjRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplit.adjRefNbr>
  {
  }

  public abstract class adjTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplit.adjTranType>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplit.lineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplit.inventoryID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplit.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASplit.qty>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASplit.unitPrice>
  {
  }

  public abstract class curyUnitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASplit.curyUnitPrice>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplit.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplit.subID>
  {
  }

  public abstract class reclassificationProhibited : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASplit.reclassificationProhibited>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplit.cashAccountID>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplit.taxCategoryID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplit.tranDesc>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CASplit.curyInfoID>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASplit.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASplit.tranAmt>
  {
  }

  public abstract class curyTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASplit.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASplit.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASplit.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASplit.taxAmt>
  {
  }

  public abstract class finPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class tranPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplit.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplit.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplit.costCodeID>
  {
  }

  public abstract class nonBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CASplit.nonBillable>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CASplit.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CASplit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASplit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CASplit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CASplit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASplit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CASplit.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CASplit.Tstamp>
  {
  }
}
