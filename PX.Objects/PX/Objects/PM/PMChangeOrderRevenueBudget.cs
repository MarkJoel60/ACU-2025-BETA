// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMChangeOrderRevenueBudget
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

/// <summary>Represents a change order line with the Income type. The records of this type are created and edited through the <strong>Revenue Budget</strong> tab of the Change Orders
/// (PM308000) form. The DAC is based on the <see cref="T:PX.Objects.PM.PMChangeOrderBudget" /> DAC.</summary>
[PXCacheName("Budget")]
[ExcludeFromCodeCoverage]
[PXBreakInheritance]
[Serializable]
public class PMChangeOrderRevenueBudget : PMChangeOrderBudget
{
  /// <inheritdoc />
  [PXParent(typeof (Select<PMChangeOrder, Where<PMChangeOrder.refNbr, Equal<Current<PMChangeOrderRevenueBudget.refNbr>>, And<Current<PMChangeOrderRevenueBudget.type>, Equal<AccountType.income>>>>))]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (PMChangeOrder.refNbr))]
  [PXUIField(DisplayName = "Ref. Nbr.", Enabled = false)]
  public override 
  #nullable disable
  string RefNbr { get; set; }

  /// <inheritdoc />
  [PXDefault(typeof (PMChangeOrder.projectID))]
  [PXDBInt]
  public override int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <inheritdoc />
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMChangeOrderRevenueBudget.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ProjectTask(typeof (PMChangeOrderRevenueBudget.projectID), AlwaysEnabled = true)]
  public override int? ProjectTaskID { get; set; }

  /// <inheritdoc />
  [CostCode(null, typeof (PMChangeOrderRevenueBudget.projectTaskID), "I", typeof (PMChangeOrderRevenueBudget.accountGroupID), ReleasedField = typeof (PMChangeOrderBudget.released), SkipVerificationForDefault = true)]
  public override int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <inheritdoc />
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXDefault]
  [AccountGroup(typeof (Where<PMAccountGroup.type, Equal<AccountType.income>>))]
  public override int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  /// <inheritdoc />
  [PXDBInt]
  [PXUIField(DisplayName = "Inventory ID")]
  [PXDefault]
  [PMInventorySelector]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>The type of the change order line.</summary>
  /// <value>
  /// Defaults to the <see cref="F:PX.Objects.GL.AccountType.Income">Income</see> type.
  /// </value>
  [PXDBString(1)]
  [PXDefault("I")]
  [PMAccountType.List]
  [PXUIField(DisplayName = "Budget Type", Visible = false, Enabled = false)]
  public override string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <inheritdoc />
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMChangeOrderRevenueBudget.inventoryID>>>>))]
  [PMUnit(typeof (PMChangeOrderRevenueBudget.inventoryID))]
  public override string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  /// <summary>The price of the specified unit of the change order line. The value can be manually modified.</summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Rate")]
  public override Decimal? Rate
  {
    get => this._Rate;
    set => this._Rate = value;
  }

  /// <inheritdoc />
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public override string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <inheritdoc />
  [PXFormula(typeof (Mult<PMChangeOrderBudget.qty, PMChangeOrderRevenueBudget.rate>), typeof (SumCalc<PMChangeOrder.revenueTotal>))]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public override Decimal? Amount { get; set; }

  /// <summary>Primary Key</summary>
  /// <exclude />
  public new class PK : 
    PrimaryKeyOf<PMChangeOrderRevenueBudget>.By<PMChangeOrderRevenueBudget.refNbr, PMChangeOrderBudget.lineNbr>
  {
    public static PMChangeOrderRevenueBudget Find(
      PXGraph graph,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (PMChangeOrderRevenueBudget) PrimaryKeyOf<PMChangeOrderRevenueBudget>.By<PMChangeOrderRevenueBudget.refNbr, PMChangeOrderBudget.lineNbr>.FindBy(graph, (object) refNbr, (object) lineNbr, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  /// <exclude />
  public new static class FK
  {
    /// <summary>Change Order</summary>
    /// <exclude />
    public class ChangeOrder : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMChangeOrderRevenueBudget>.By<PMChangeOrderRevenueBudget.refNbr>
    {
    }

    /// <summary>Project</summary>
    /// <exclude />
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMChangeOrderRevenueBudget>.By<PMChangeOrderRevenueBudget.projectID>
    {
    }

    /// <summary>Project Task</summary>
    /// <exclude />
    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<PMChangeOrderRevenueBudget>.By<PMChangeOrderRevenueBudget.projectID, PMChangeOrderRevenueBudget.projectTaskID>
    {
    }

    /// <summary>Account Group</summary>
    /// <exclude />
    public class AccountGroup : 
      PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupID>.ForeignKeyOf<PMChangeOrderRevenueBudget>.By<PMChangeOrderRevenueBudget.accountGroupID>
    {
    }

    /// <summary>Cost Code</summary>
    /// <exclude />
    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMChangeOrderRevenueBudget>.By<PMChangeOrderRevenueBudget.costCodeID>
    {
    }

    /// <summary>Inventory Item</summary>
    /// <exclude />
    public class Item : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMChangeOrderRevenueBudget>.By<PMChangeOrderRevenueBudget.inventoryID>
    {
    }
  }

  public new abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.refNbr>
  {
  }

  public new abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.projectID>
  {
  }

  public new abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.projectTaskID>
  {
  }

  public new abstract class costCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.costCodeID>
  {
  }

  public new abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.accountGroupID>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.inventoryID>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrderRevenueBudget.type>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrderRevenueBudget.uOM>
  {
  }

  public new abstract class rate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.rate>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.description>
  {
  }

  public new abstract class amount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.amount>
  {
  }

  public new abstract class previouslyApprovedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.previouslyApprovedQty>
  {
  }

  public new abstract class previouslyApprovedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.previouslyApprovedAmount>
  {
  }

  public new abstract class committedCOQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.committedCOQty>
  {
  }

  public new abstract class committedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.committedCOAmount>
  {
  }

  public new abstract class otherDraftRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.otherDraftRevisedAmount>
  {
  }

  public new abstract class totalPotentialRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderRevenueBudget.totalPotentialRevisedAmount>
  {
  }
}
