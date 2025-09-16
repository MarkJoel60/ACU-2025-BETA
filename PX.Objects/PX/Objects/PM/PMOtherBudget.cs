// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMOtherBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("Budget")]
[PXBreakInheritance]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMOtherBudget : PMBudget
{
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMOtherBudget.projectID>>, And<PMOtherBudget.type, NotEqual<AccountType.income>, And<PMOtherBudget.type, NotEqual<AccountType.expense>>>>>))]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXForeignReference(typeof (Field<PMOtherBudget.projectID>.IsRelatedTo<PMProject.contractID>))]
  [PXDBInt(IsKey = true)]
  public override int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMOtherBudget.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [PXParent(typeof (Select<PMTask, Where<PMTask.projectID, Equal<Current<PMOtherBudget.projectID>>, And<PMTask.taskID, Equal<Current<PMOtherBudget.projectTaskID>>, And<PMOtherBudget.type, NotEqual<AccountType.income>, And<PMOtherBudget.type, NotEqual<AccountType.expense>>>>>>))]
  [ProjectTask(typeof (PMProject.contractID), IsKey = true, AlwaysEnabled = true, DirtyRead = true)]
  [PXForeignReference(typeof (CompositeKey<Field<PMOtherBudget.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMOtherBudget.projectTaskID>.IsRelatedTo<PMTask.taskID>>))]
  public override int? ProjectTaskID { get; set; }

  [CostCode(null, typeof (PMOtherBudget.projectTaskID), IsKey = true, SkipVerification = true)]
  [PXForeignReference(typeof (Field<PMOtherBudget.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public override int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXDefault]
  [AccountGroup(typeof (Where<PMAccountGroup.isExpense, Equal<False>, And<PMAccountGroup.type, NotEqual<AccountType.income>>>), IsKey = true)]
  [PXForeignReference(typeof (Field<PMOtherBudget.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public override int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Inventory ID")]
  [PXDefault]
  [PMInventorySelector]
  [PXForeignReference(typeof (Field<PMOtherBudget.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(1)]
  [PXDefault("A")]
  public override 
  #nullable disable
  string Type { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">revenue project task</see> associated with the budget line.
  /// </summary>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMOtherBudget.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.cost>>>>>))]
  [PXUIEnabled(typeof (Where<PMOtherBudget.type, NotEqual<AccountType.income>>))]
  [PXSelector(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMOtherBudget.projectID>>, And<PMTask.type, NotEqual<ProjectTaskType.cost>>>>), new System.Type[] {typeof (PMTask.taskCD), typeof (PMTask.description), typeof (PMTask.status)}, SubstituteKey = typeof (PMTask.taskCD), DirtyRead = true)]
  [PXUIField(DisplayName = "Revenue Task")]
  [PXDBInt]
  public override int? RevenueTaskID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public override string Description { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Quantity")]
  public override Decimal? Qty { get; set; }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMOtherBudget.inventoryID>>>>))]
  [PMUnit(typeof (PMOtherBudget.inventoryID))]
  public override string UOM { get; set; }

  [PXDBCurrencyPriceCost(typeof (PMBudget.curyInfoID), typeof (PMOtherBudget.rate))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Rate")]
  public override Decimal? CuryUnitRate { get; set; }

  [PXDBPriceCost]
  public override Decimal? Rate { get; set; }

  [PXDBCurrencyPriceCost(typeof (PMBudget.curyInfoID), typeof (PMOtherBudget.unitPrice))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price")]
  public override Decimal? CuryUnitPrice { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price in Base Currency")]
  public override Decimal? UnitPrice { get; set; }

  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMOtherBudget.amount))]
  [PXFormula(typeof (Mult<PMOtherBudget.qty, PMOtherBudget.curyUnitRate>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount")]
  public override Decimal? CuryAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount in Base Currency")]
  public override Decimal? Amount { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Quantity")]
  public override Decimal? RevisedQty { get; set; }

  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMOtherBudget.revisedAmount))]
  [PXFormula(typeof (Mult<PMOtherBudget.revisedQty, PMOtherBudget.curyUnitRate>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount")]
  public override Decimal? CuryRevisedAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount in Base Currency")]
  public override Decimal? RevisedAmount { get; set; }

  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMOtherBudget.invoicedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Invoices Amount", Enabled = false)]
  public override Decimal? CuryInvoicedAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Invoices Amount in Base Currency", Enabled = false)]
  public override Decimal? InvoicedAmount { get; set; }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMOtherBudget.projectID>
  {
  }

  public new abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMOtherBudget.projectTaskID>
  {
  }

  public new abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMOtherBudget.costCodeID>
  {
  }

  public new abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMOtherBudget.accountGroupID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMOtherBudget.inventoryID>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMOtherBudget.type>
  {
  }

  public new abstract class revenueTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMOtherBudget.revenueTaskID>
  {
  }

  public new abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMOtherBudget.taxCategoryID>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMOtherBudget.description>
  {
  }

  public new abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMOtherBudget.qty>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMOtherBudget.uOM>
  {
  }

  public new abstract class curyUnitRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.curyUnitRate>
  {
  }

  public new abstract class rate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMOtherBudget.rate>
  {
  }

  public new abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.curyUnitPrice>
  {
  }

  public new abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMOtherBudget.unitPrice>
  {
  }

  public new abstract class curyAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.curyAmount>
  {
  }

  public new abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMOtherBudget.amount>
  {
  }

  public new abstract class revisedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.revisedQty>
  {
  }

  public new abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.curyRevisedAmount>
  {
  }

  public new abstract class revisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.revisedAmount>
  {
  }

  public new abstract class curyInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.curyInvoicedAmount>
  {
  }

  public new abstract class invoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.invoicedAmount>
  {
  }

  public new abstract class actualQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMOtherBudget.actualQty>
  {
  }

  public new abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.curyActualAmount>
  {
  }

  public new abstract class changeOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.changeOrderQty>
  {
  }

  public new abstract class curyChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.curyChangeOrderAmount>
  {
  }

  public new abstract class committedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.committedQty>
  {
  }

  public new abstract class committedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.committedAmount>
  {
  }

  public new abstract class committedOpenQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.committedOpenQty>
  {
  }

  public new abstract class committedOpenAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.committedOpenAmount>
  {
  }

  public new abstract class committedReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.committedReceivedQty>
  {
  }

  public new abstract class committedInvoicedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.committedInvoicedQty>
  {
  }

  public new abstract class committedInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.committedInvoicedAmount>
  {
  }

  public new abstract class curyActualPlusOpenCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.curyActualPlusOpenCommittedAmount>
  {
  }

  public new abstract class curyVarianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.curyVarianceAmount>
  {
  }

  public new abstract class performance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMOtherBudget.performance>
  {
  }

  public new abstract class isProduction : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMOtherBudget.isProduction>
  {
  }

  public new abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMOtherBudget.lineCntr>
  {
  }
}
