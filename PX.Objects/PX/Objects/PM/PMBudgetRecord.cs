// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBudgetRecord
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("Budget")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMBudgetRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _ProjectID;
  protected int? _CostCodeID;
  protected int? _AccountGroupID;
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _Type;
  protected string _Description;
  protected Decimal? _Qty;
  protected string _UOM;
  protected Decimal? _RevisedQty;
  protected Decimal? _ActualQty;
  protected Decimal? _ChangeOrderQty;
  protected Decimal? _CommittedQty;
  protected Decimal? _CommittedOpenQty;
  protected Decimal? _CommittedReceivedQty;
  protected Decimal? _CommittedInvoicedQty;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(IsKey = true)]
  [PXDefault]
  public virtual string RecordID { get; set; }

  [PXDBInt]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  public int? TaskID => this.ProjectTaskID;

  [ProjectTask(typeof (PMBudgetRecord.projectID))]
  public virtual int? ProjectTaskID { get; set; }

  [PXForeignReference(typeof (Field<PMBudgetRecord.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  [CostCode(null, typeof (PMBudgetRecord.projectTaskID), null, typeof (PMBudgetRecord.accountGroupID), true, false, Filterable = false, SkipVerification = true)]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [AccountGroup]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  [PXDBInt]
  [PXUIField]
  [PMInventorySelector]
  [PXParent(typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMBudgetRecord.inventoryID>>>>))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(1)]
  [PXDefault]
  [PMAccountType.List]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PMProject.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Quantity", Enabled = false)]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMBudget.inventoryID>>>>))]
  [PMUnit(typeof (PMBudgetRecord.inventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBCurrencyPriceCost(typeof (PMBudget.curyInfoID), typeof (PMBudget.rate))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Rate", Visible = false)]
  public virtual Decimal? CuryUnitRate { get; set; }

  [PXDBPriceCost]
  public virtual Decimal? Rate { get; set; }

  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.amount))]
  [PXFormula(typeof (Mult<PMBudgetRecord.qty, PMBudgetRecord.curyUnitRate>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount", Visible = false)]
  public virtual Decimal? CuryAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount in Base Currency", Visible = false)]
  public virtual Decimal? Amount { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Quantity", Visible = false)]
  public virtual Decimal? RevisedQty
  {
    get => this._RevisedQty;
    set => this._RevisedQty = value;
  }

  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.revisedAmount))]
  [PXFormula(typeof (Mult<PMBudgetRecord.revisedQty, PMBudgetRecord.curyUnitRate>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount", Visible = false)]
  public virtual Decimal? CuryRevisedAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount in Base Currency", Visible = false)]
  public virtual Decimal? RevisedAmount { get; set; }

  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.invoicedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Invoice Amount", Enabled = false)]
  public virtual Decimal? CuryInvoicedAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Invoices Amount in Base Currency", Enabled = false)]
  public virtual Decimal? InvoicedAmount { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Quantity", Enabled = false)]
  public virtual Decimal? ActualQty
  {
    get => this._ActualQty;
    set => this._ActualQty = value;
  }

  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.baseActualAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Amount", Enabled = false)]
  public virtual Decimal? CuryActualAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Hist. Actual Amount in Base Currency", Enabled = false, FieldClass = "ProjectMultiCurrency")]
  public virtual Decimal? ActualAmount { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Amount in Base Currency", Enabled = false, FieldClass = "ProjectMultiCurrency")]
  public virtual Decimal? BaseActualAmount { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Potential CO Quantity", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? DraftChangeOrderQty { get; set; }

  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.draftChangeOrderAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Potential CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryDraftChangeOrderAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Potential CO Amount in Base Currency", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? DraftChangeOrderAmount { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Quantity", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? ChangeOrderQty
  {
    get => this._ChangeOrderQty;
    set => this._ChangeOrderQty = value;
  }

  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.changeOrderAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryChangeOrderAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Amount in Base Currency", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? ChangeOrderAmount { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Committed Quantity", Enabled = false)]
  public virtual Decimal? CommittedQty
  {
    get => this._CommittedQty;
    set => this._CommittedQty = value;
  }

  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.committedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Committed Amount", Enabled = false)]
  public virtual Decimal? CuryCommittedAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Committed Amount in Base Currency", Enabled = false)]
  public virtual Decimal? CommittedAmount { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Committed CO Quantity", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CommittedCOQty
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudgetRecord.committedQty), typeof (PMBudgetRecord.committedOrigQty)})] get
    {
      Decimal? nullable = this.CommittedQty;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CommittedOrigQty;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  [PXCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.committedCOAmount))]
  [PXUIField(DisplayName = "Committed CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryCommittedCOAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudgetRecord.curyCommittedAmount), typeof (PMBudgetRecord.curyCommittedOrigAmount)})] get
    {
      Decimal? nullable = this.CuryCommittedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryCommittedOrigAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  [PXBaseCury]
  [PXUIField(DisplayName = "Committed CO Amount in Base Currency", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CommittedCOAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudgetRecord.committedAmount), typeof (PMBudgetRecord.committedOrigAmount)})] get
    {
      Decimal? nullable = this.CommittedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CommittedOrigAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Committed Quantity", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CommittedOrigQty { get; set; }

  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.committedOrigAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Committed Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryCommittedOrigAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Committed Amount in Base Currency", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CommittedOrigAmount { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Open Quantity", Enabled = false)]
  public virtual Decimal? CommittedOpenQty
  {
    get => this._CommittedOpenQty;
    set => this._CommittedOpenQty = value;
  }

  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.committedOpenAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Open Amount", Enabled = false)]
  public virtual Decimal? CuryCommittedOpenAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Open Amount in Base Currency", Enabled = false)]
  public virtual Decimal? CommittedOpenAmount { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Received Quantity", Enabled = false)]
  public virtual Decimal? CommittedReceivedQty
  {
    get => this._CommittedReceivedQty;
    set => this._CommittedReceivedQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Invoiced Quantity", Enabled = false)]
  public virtual Decimal? CommittedInvoicedQty
  {
    get => this._CommittedInvoicedQty;
    set => this._CommittedInvoicedQty = value;
  }

  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.committedInvoicedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Invoiced Amount", Enabled = false)]
  public virtual Decimal? CuryCommittedInvoicedAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Invoiced Amount in Base Currency", Enabled = false)]
  public virtual Decimal? CommittedInvoicedAmount { get; set; }

  [PXCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.actualPlusOpenCommittedAmount))]
  [PXUIField(DisplayName = "Actual + Open Committed Amount", Enabled = false)]
  public virtual Decimal? CuryActualPlusOpenCommittedAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudgetRecord.curyActualAmount), typeof (PMBudgetRecord.curyCommittedOpenAmount)})] get
    {
      Decimal? curyActualAmount = this.CuryActualAmount;
      Decimal? committedOpenAmount = this.CuryCommittedOpenAmount;
      return !(curyActualAmount.HasValue & committedOpenAmount.HasValue) ? new Decimal?() : new Decimal?(curyActualAmount.GetValueOrDefault() + committedOpenAmount.GetValueOrDefault());
    }
  }

  [PXBaseCury]
  [PXUIField(DisplayName = "Actual + Open Committed Amount in Base Currency", Enabled = false)]
  public virtual Decimal? ActualPlusOpenCommittedAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudgetRecord.actualAmount), typeof (PMBudgetRecord.committedOpenAmount)})] get
    {
      Decimal? actualAmount = this.ActualAmount;
      Decimal? committedOpenAmount = this.CommittedOpenAmount;
      return !(actualAmount.HasValue & committedOpenAmount.HasValue) ? new Decimal?() : new Decimal?(actualAmount.GetValueOrDefault() + committedOpenAmount.GetValueOrDefault());
    }
  }

  [PXCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.varianceAmount))]
  [PXUIField(DisplayName = "Variance Amount", Enabled = false)]
  public virtual Decimal? CuryVarianceAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudgetRecord.curyRevisedAmount), typeof (PMBudgetRecord.curyActualPlusOpenCommittedAmount)})] get
    {
      Decimal? curyRevisedAmount = this.CuryRevisedAmount;
      Decimal? openCommittedAmount = this.CuryActualPlusOpenCommittedAmount;
      return !(curyRevisedAmount.HasValue & openCommittedAmount.HasValue) ? new Decimal?() : new Decimal?(curyRevisedAmount.GetValueOrDefault() - openCommittedAmount.GetValueOrDefault());
    }
  }

  [PXBaseCury]
  [PXUIField(DisplayName = "Variance Amount in Base Currency", Enabled = false)]
  public virtual Decimal? VarianceAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudgetRecord.revisedAmount), typeof (PMBudgetRecord.actualPlusOpenCommittedAmount)})] get
    {
      Decimal? revisedAmount = this.RevisedAmount;
      Decimal? openCommittedAmount = this.ActualPlusOpenCommittedAmount;
      return !(revisedAmount.HasValue & openCommittedAmount.HasValue) ? new Decimal?() : new Decimal?(revisedAmount.GetValueOrDefault() - openCommittedAmount.GetValueOrDefault());
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMBudgetRecord.selected>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudgetRecord.recordID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetRecord.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetRecord.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetRecord.costCodeID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetRecord.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetRecord.inventoryID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudgetRecord.type>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMBudgetRecord.curyInfoID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudgetRecord.description>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudgetRecord.qty>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudgetRecord.uOM>
  {
  }

  public abstract class curyUnitRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.curyUnitRate>
  {
  }

  public abstract class rate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudgetRecord.rate>
  {
  }

  public abstract class curyAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudgetRecord.curyAmount>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudgetRecord.amount>
  {
  }

  public abstract class revisedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudgetRecord.revisedQty>
  {
  }

  public abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.curyRevisedAmount>
  {
  }

  public abstract class revisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.revisedAmount>
  {
  }

  public abstract class curyInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.curyInvoicedAmount>
  {
  }

  public abstract class invoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.invoicedAmount>
  {
  }

  public abstract class actualQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudgetRecord.actualQty>
  {
  }

  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.curyActualAmount>
  {
  }

  public abstract class actualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.actualAmount>
  {
  }

  public abstract class baseActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.baseActualAmount>
  {
  }

  public abstract class draftChangeOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.draftChangeOrderQty>
  {
  }

  public abstract class curyDraftChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.curyDraftChangeOrderAmount>
  {
  }

  public abstract class draftChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.draftChangeOrderAmount>
  {
  }

  public abstract class changeOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.changeOrderQty>
  {
  }

  public abstract class curyChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.curyChangeOrderAmount>
  {
  }

  public abstract class changeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.changeOrderAmount>
  {
  }

  public abstract class committedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.committedQty>
  {
  }

  public abstract class curyCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.curyCommittedAmount>
  {
  }

  public abstract class committedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.committedAmount>
  {
  }

  public abstract class committedCOQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.committedCOQty>
  {
  }

  public abstract class curyCommittedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.curyCommittedCOAmount>
  {
  }

  public abstract class committedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.committedCOAmount>
  {
  }

  public abstract class committedOrigQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.committedOrigQty>
  {
  }

  public abstract class curyCommittedOrigAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.curyCommittedOrigAmount>
  {
  }

  public abstract class committedOrigAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.committedOrigAmount>
  {
  }

  public abstract class committedOpenQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.committedOpenQty>
  {
  }

  public abstract class curyCommittedOpenAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.curyCommittedOpenAmount>
  {
  }

  public abstract class committedOpenAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.committedOpenAmount>
  {
  }

  public abstract class committedReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.committedReceivedQty>
  {
  }

  public abstract class committedInvoicedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.committedInvoicedQty>
  {
  }

  public abstract class curyCommittedInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.curyCommittedInvoicedAmount>
  {
  }

  public abstract class committedInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.committedInvoicedAmount>
  {
  }

  public abstract class curyActualPlusOpenCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.curyActualPlusOpenCommittedAmount>
  {
  }

  public abstract class actualPlusOpenCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.actualPlusOpenCommittedAmount>
  {
  }

  public abstract class curyVarianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.curyVarianceAmount>
  {
  }

  public abstract class varianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetRecord.varianceAmount>
  {
  }
}
