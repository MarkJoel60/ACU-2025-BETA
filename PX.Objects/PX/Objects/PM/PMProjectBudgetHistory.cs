// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectBudgetHistory
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
using PX.Objects.CT;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a line of the project budget history by date.
/// </summary>
[PXCacheName("Project Budget History")]
public class PMProjectBudgetHistory : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IProjectFilter,
  IQuantify
{
  /// <summary>The project transaction date.</summary>
  [PXDBDate(IsKey = true)]
  public virtual DateTime? Date { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> of the project transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXParent(typeof (SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<BqlField<PMProjectBudgetHistory.projectID, IBqlInt>.FromCurrent>>))]
  [PXDefault]
  [Project(typeof (Where<PMProject.nonProject, Equal<False>, And<PMProject.baseType, Equal<CTPRType.project>>>), IsKey = true)]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMTask">project task</see> of the project transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMTask.taskID" /> field.
  /// </value>
  [PXParent(typeof (Select<PMTask, Where<PMTask.projectID, Equal<Current<PMProjectBudgetHistory.projectID>>, And<PMTask.taskID, Equal<Current<PMProjectBudgetHistory.taskID>>>>>))]
  [ProjectTask(typeof (PMProjectBudgetHistory.projectID), IsKey = true)]
  [PXForeignReference(typeof (CompositeKey<Field<PMProjectBudgetHistory.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMProjectBudgetHistory.taskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? TaskID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMAccountGroup">project account group</see> of the project transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXDefault]
  [AccountGroup(IsKey = true)]
  [PXForeignReference(typeof (Field<PMProjectBudgetHistory.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public virtual int? AccountGroupID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> of the project transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PMInventorySelector]
  [PXDefault]
  [PXForeignReference(typeof (Field<PMProjectBudgetHistory.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> of the project transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMCostCode.CostCodeID" /> field.
  /// </value>
  [PXForeignReference(typeof (Field<PMProjectBudgetHistory.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  [CostCode(null, typeof (PMProjectBudgetHistory.taskID), null, typeof (PMProjectBudgetHistory.accountGroupID), true, false, IsKey = true, Filterable = false, SkipVerification = true)]
  [PXDefault]
  public virtual int? CostCodeID { get; set; }

  /// <summary>
  /// The reference number of the related <see cref="T:PX.Objects.PM.PMChangeOrder">change order</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMChangeOrder.RefNbr" /> field.
  /// </value>
  [PXDBString(15, IsKey = true)]
  public virtual string ChangeOrderRefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.Type" />
  [PXDBString(1, IsFixed = true)]
  [PMAccountType.List]
  public virtual string Type { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">currency info</see> object associated with the budget history line.
  /// </summary>
  [PXDBLong]
  [CurrencyInfo(typeof (PMProject.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">unit of measure</see> of the budget line.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMBudget.inventoryID>>>>))]
  [PMUnit(typeof (PMProjectBudgetHistory.inventoryID))]
  public virtual string UOM { get; set; }

  /// <summary>Revised budget quantity.</summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RevisedBudgetQty { get; set; }

  /// <exclude />
  public virtual Decimal? Qty
  {
    get => this.RevisedBudgetQty;
    set => this.RevisedBudgetQty = value;
  }

  /// <summary>Revised budget amount in the base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Enabled = false)]
  public virtual Decimal? RevisedBudgetAmt { get; set; }

  /// <summary>Revised budget amount in the project currency.</summary>
  [PXDBCurrency(typeof (PMProjectBudgetHistory.curyInfoID), typeof (PMProjectBudgetHistory.revisedBudgetAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Enabled = false)]
  public virtual Decimal? CuryRevisedBudgetAmt { get; set; }

  public class PK : 
    PrimaryKeyOf<PMProjectBudgetHistory>.By<PMProjectBudgetHistory.projectID, PMProjectBudgetHistory.date, PMProjectBudgetHistory.taskID, PMProjectBudgetHistory.accountGroupID, PMProjectBudgetHistory.costCodeID, PMProjectBudgetHistory.inventoryID, PMProjectBudgetHistory.changeOrderRefNbr>
  {
    public static PMProjectBudgetHistory Find(
      PXGraph graph,
      int? projectID,
      DateTime? date,
      int? taskID,
      int? accountGroupID,
      int? costCodeID,
      int? inventoryID,
      string changeOrderRefNbr,
      PKFindOptions options = 0)
    {
      return (PMProjectBudgetHistory) PrimaryKeyOf<PMProjectBudgetHistory>.By<PMProjectBudgetHistory.projectID, PMProjectBudgetHistory.date, PMProjectBudgetHistory.taskID, PMProjectBudgetHistory.accountGroupID, PMProjectBudgetHistory.costCodeID, PMProjectBudgetHistory.inventoryID, PMProjectBudgetHistory.changeOrderRefNbr>.FindBy(graph, (object) projectID, (object) date, (object) taskID, (object) accountGroupID, (object) costCodeID, (object) inventoryID, (object) changeOrderRefNbr, options);
    }
  }

  public static class FK
  {
    public class Project : 
      PrimaryKeyOf<
      #nullable disable
      PMProject>.By<PMProject.contractID>.ForeignKeyOf<
      #nullable enable
      PMProjectBudgetHistory>.By<PMProjectBudgetHistory.projectID>
    {
    }

    public class ProjectTask : 
      PrimaryKeyOf<
      #nullable disable
      PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<
      #nullable enable
      PMProjectBudgetHistory>.By<PMProjectBudgetHistory.projectID, PMProjectBudgetHistory.taskID>
    {
    }

    public class AccountGroup : 
      PrimaryKeyOf<
      #nullable disable
      PMAccountGroup>.By<PMAccountGroup.groupID>.ForeignKeyOf<
      #nullable enable
      PMProjectBudgetHistory>.By<PMProjectBudgetHistory.accountGroupID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<
      #nullable disable
      PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<
      #nullable enable
      PMProjectBudgetHistory>.By<PMProjectBudgetHistory.inventoryID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<
      #nullable disable
      PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<
      #nullable enable
      PMProjectBudgetHistory>.By<PMProjectBudgetHistory.costCodeID>
    {
    }
  }

  public abstract class date : BqlType<IBqlDateTime, DateTime>.Field<PMProjectBudgetHistory.date>
  {
  }

  public abstract class projectID : BqlType<IBqlInt, int>.Field<PMProjectBudgetHistory.projectID>
  {
  }

  public abstract class taskID : BqlType<IBqlInt, int>.Field<PMProjectBudgetHistory.taskID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<IBqlInt, int>.Field<PMProjectBudgetHistory.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<IBqlInt, int>.Field<PMProjectBudgetHistory.inventoryID>
  {
  }

  public abstract class costCodeID : BqlType<IBqlInt, int>.Field<PMProjectBudgetHistory.costCodeID>
  {
  }

  public abstract class changeOrderRefNbr : 
    BqlType<IBqlString, string>.Field<PMProjectBudgetHistory.changeOrderRefNbr>
  {
  }

  public abstract class type : BqlType<IBqlInt, int>.Field<PMProjectBudgetHistory.type>
  {
  }

  public abstract class curyInfoID : BqlType<IBqlLong, long>.Field<PMProjectBudgetHistory.curyInfoID>
  {
  }

  public abstract class uOM : BqlType<IBqlString, string>.Field<PMProjectBudgetHistory.uOM>
  {
  }

  public abstract class revisedBudgetQty : 
    BqlType<IBqlDecimal, Decimal>.Field<PMProjectBudgetHistory.revisedBudgetQty>
  {
  }

  public abstract class revisedBudgetAmt : 
    BqlType<IBqlDecimal, Decimal>.Field<PMProjectBudgetHistory.revisedBudgetAmt>
  {
  }

  public abstract class curyRevisedBudgetAmt : 
    BqlType<IBqlDecimal, Decimal>.Field<PMProjectBudgetHistory.curyRevisedBudgetAmt>
  {
  }
}
