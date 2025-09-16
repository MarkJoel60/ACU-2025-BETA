// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProformaTransactLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Represents a pro forma invoice line with the <see cref="F:PX.Objects.PM.PMProformaLineType.Transaction">Transaction</see> type. The records of this type are edited through the <b>Time and Material</b>
/// tab of the Pro Forma Invoices (PM307000) form. The DAC is based on the <see cref="T:PX.Objects.PM.PMProformaLine" /> DAC and extends it with the fields relevant to the lines of this type.</summary>
[PXCacheName("Pro Forma Line")]
[PXBreakInheritance]
public class PMProformaTransactLine : PMProformaLine
{
  /// <summary>The reference number of the parent <see cref="T:PX.Objects.PM.PMProforma">pro forma invoice</see>.</summary>
  /// <value>
  /// The value of this field corresponds to the <see cref="P:PX.Objects.PM.PMProforma.RefNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXSelector(typeof (Search<PMProforma.refNbr>), Filterable = true)]
  [PXUIField]
  [PXDBDefault(typeof (PMProforma.refNbr))]
  [PXFormula(null, typeof (CountCalc<PMProforma.numberOfLines>))]
  [PXParent(typeof (Select<PMProforma, Where<PMProforma.refNbr, Equal<Current<PMProformaTransactLine.refNbr>>, And<PMProforma.revisionID, Equal<Current<PMProformaTransactLine.revisionID>>, And<Current<PMProformaTransactLine.type>, Equal<PMProformaLineType.transaction>>>>>))]
  public override 
  #nullable disable
  string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>
  /// The original sequence number of the line among all the pro forma invoice lines.
  /// </summary>
  /// <remarks>The sequence of line numbers of the pro forma invoice lines belonging to a single document can include gaps.</remarks>
  [PXUIField(DisplayName = "Line Number", Visible = false)]
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (PMProforma.lineCntr))]
  public override int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  /// <summary>The type of the pro forma invoice line.</summary>
  /// <value>
  /// Defaults to the <see cref="F:PX.Objects.PM.PMProformaLineType.Transaction">Transaction</see> type.
  /// </value>
  [PXDBString(1)]
  [PXDefault("T")]
  public override string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <summary>The line amount.</summary>
  /// <value>
  /// Calculated by multiplying the values of <see cref="P:PX.Objects.PM.PMProformaLine.Qty">Quantity to Invoice</see> and <see cref="P:PX.Objects.PM.PMProformaLine.CuryUnitPrice">Unit Price</see>.
  /// </value>
  [PXFormula(typeof (Mult<PMProformaLine.qty, PMProformaLine.curyUnitPrice>))]
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.amount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public override Decimal? CuryAmount { get; set; }

  /// <inheritdoc />
  [PXFormula(typeof (Sub<Add<PMProformaTransactLine.curyAmount, PMProformaLine.curyMaterialStoredAmount>, PMProformaLine.curyPrepaidAmount>), typeof (SumCalc<PMProforma.curyTransactionalTotal>))]
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.lineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount to Invoice")]
  public override Decimal? CuryLineTotal { get; set; }

  /// <inheritdoc />
  [PXFormula(typeof (Mult<PMProformaTransactLine.curyLineTotal, Div<PMProformaLine.retainagePct, decimal100>>), typeof (SumCalc<PMProforma.curyRetainageDetailTotal>))]
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.retainage))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Amount", FieldClass = "Retainage")]
  public override Decimal? CuryRetainage { get; set; }

  /// <inheritdoc />
  [Account(typeof (PMProformaTransactLine.branchID), typeof (Search2<PX.Objects.GL.Account.accountID, InnerJoin<PMAccountGroup, On<PX.Objects.GL.Account.accountGroupID, Equal<PMAccountGroup.groupID>>>, Where2<Where<Current<PMProformaTransactLine.isPrepayment>, Equal<True>, And<PX.Objects.GL.Account.accountGroupID, Equal<Current<PMProformaTransactLine.accountGroupID>>>>, Or<Where<Current<PMProformaTransactLine.isPrepayment>, Equal<False>, And<PX.Objects.GL.Account.accountGroupID, IsNotNull>>>>>), DisplayName = "Sales Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXDefault(typeof (Search2<PX.Objects.IN.InventoryItem.salesAcctID, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.IN.InventoryItem.salesAcctID, Equal<PX.Objects.GL.Account.accountID>>, InnerJoin<PMAccountGroup, On<PX.Objects.GL.Account.accountGroupID, Equal<PMAccountGroup.groupID>, And<PMAccountGroup.type, Equal<AccountType.income>>>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMProformaTransactLine.inventoryID>>>>))]
  public override int? AccountID { get; set; }

  /// <inheritdoc />
  [SubAccount(typeof (PMProformaTransactLine.accountID), typeof (PMProformaTransactLine.branchID), true)]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <inheritdoc />
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PMTax(typeof (PMProforma), typeof (PMTax), typeof (PMTaxTran))]
  [PMRetainedTax(typeof (PMProforma), typeof (PMTax), typeof (PMTaxTran))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public override string TaxCategoryID { get; set; }

  /// <inheritdoc />
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMProformaTransactLine.inventoryID>>>>))]
  [PMUnit(typeof (PMProformaLine.inventoryID))]
  public override string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  /// <summary>The reference to a revenue budget line by task.</summary>
  [ProjectTask(typeof (PMProformaLine.projectID), typeof (Where<PMTask.type, NotEqual<ProjectTaskType.cost>>), AlwaysEnabled = true, AllowNull = true, DisplayName = "Revenue Task", SkipDefaultTask = true)]
  public override int? RevenueTaskID { get; set; }

  /// <summary>
  /// The billing limit amount (<see cref="P:PX.Objects.PM.PMRevenueBudget.CuryMaxAmount">Maximum Amount</see>)
  /// of the corresponding revenue budget line of the project.
  /// If no billing limit amount is defined for the revenue budget line of the project,
  /// the Max Limit Amount of each corresponding pro forma invoice line is 0.
  /// </summary>
  [PXCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaTransactLine.maxAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Max Limit Amount", Enabled = false, Visible = false)]
  public virtual Decimal? CuryMaxAmount { get; set; }

  /// <summary>
  /// The billing limit amount (<see cref="P:PX.Objects.PM.PMRevenueBudget.CuryMaxAmount">Maximum Amount</see>)
  /// of the corresponding revenue budget line of the project in the base currency.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Max Limit Amount in Base Currency", Enabled = false, Visible = false)]
  public virtual Decimal? MaxAmount { get; set; }

  /// <summary>
  /// The maximum amount available to bill the customer based on the billing limit amount
  /// of the corresponding revenue budget line of the project.
  /// </summary>
  [PXCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaTransactLine.availableAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Max Available Amount", Enabled = false, Visible = false)]
  public virtual Decimal? CuryAvailableAmount { get; set; }

  /// <summary>
  /// The maximum amount in the base currency available to bill the customer based on the billing limit amount
  /// of the corresponding revenue budget line of the project.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Max Available Amount in Base Currency", Enabled = false, Visible = false)]
  public virtual Decimal? AvailableAmount { get; set; }

  /// <summary>The amount that exceeds the billing limit.</summary>
  /// <value>
  /// The amount is calculated as the difference between the <see cref="P:PX.Objects.PM.PMProformaTransactLine.CuryLineTotal">Amount to Invoice</see> and
  /// <see cref="P:PX.Objects.PM.PMProformaTransactLine.CuryAvailableAmount">Max Available Amount</see>.
  /// If this difference is negative - that is, if the <see cref="P:PX.Objects.PM.PMProformaTransactLine.CuryAvailableAmount">Max Available Amount</see> is greater than the
  /// <see cref="P:PX.Objects.PM.PMProformaTransactLine.CuryLineTotal">Amount to Invoice</see> - the Over-Limit Amount is 0.
  /// The invoice lines for which the Over-Limit Amount becomes nonzero exceed the limit.
  /// </value>
  [PXCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaTransactLine.overflowAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Over-Limit Amount", Enabled = false, Visible = false)]
  public virtual Decimal? CuryOverflowAmount { get; set; }

  /// <summary>The amount that exceeds the billing limit in the base currency.</summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Overflow Amount in Base Currency", Enabled = false, Visible = false)]
  public virtual Decimal? OverflowAmount { get; set; }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaTransactLine.refNbr>
  {
    public const int Length = 15;
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaTransactLine.lineNbr>
  {
  }

  public new abstract class revisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaTransactLine.revisionID>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaTransactLine.type>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaTransactLine.inventoryID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaTransactLine.branchID>
  {
  }

  public new abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaTransactLine.taskID>
  {
  }

  public new abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaTransactLine.accountGroupID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaTransactLine.vendorID>
  {
  }

  public new abstract class curyAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaTransactLine.curyAmount>
  {
  }

  public new abstract class curyLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaTransactLine.curyLineTotal>
  {
  }

  public new abstract class curyRetainage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaTransactLine.curyRetainage>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaTransactLine.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaTransactLine.subID>
  {
  }

  public new abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProformaTransactLine.taxCategoryID>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaTransactLine.uOM>
  {
  }

  public new abstract class isPrepayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMProformaTransactLine.isPrepayment>
  {
  }

  public new abstract class option : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaTransactLine.option>
  {
  }

  public new abstract class revenueTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaTransactLine.revenueTaskID>
  {
  }

  public abstract class curyMaxAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaTransactLine.curyMaxAmount>
  {
  }

  public abstract class maxAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaTransactLine.maxAmount>
  {
  }

  public abstract class curyAvailableAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaTransactLine.curyAvailableAmount>
  {
  }

  public abstract class availableAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaTransactLine.availableAmount>
  {
  }

  public abstract class curyOverflowAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaTransactLine.curyOverflowAmount>
  {
  }

  public abstract class overflowAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaTransactLine.overflowAmount>
  {
  }
}
