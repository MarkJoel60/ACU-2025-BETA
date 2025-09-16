// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMChangeOrderPrevioslyAmount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>The DAC, which is used in reports to calculate the previously invoiced amount.</summary>
/// <exclude />
[PXHidden]
[PXProjection(typeof (Select4<PMChangeOrderBudget, Where<PMChangeOrderBudget.refNbr, Less<Current<PMChangeOrderPrevioslyAmount.refNbr>>, And<PMChangeOrderBudget.type, Equal<AccountType.income>, And<PMChangeOrderBudget.released, Equal<True>>>>, Aggregate<GroupBy<PMChangeOrderBudget.projectID, GroupBy<PMChangeOrderBudget.projectTaskID, GroupBy<PMChangeOrderBudget.accountGroupID, GroupBy<PMChangeOrderBudget.inventoryID, GroupBy<PMChangeOrderBudget.costCodeID, Sum<PMChangeOrderBudget.amount>>>>>>>>), Persistent = false)]
[Serializable]
public class PMChangeOrderPrevioslyAmount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ProjectID;
  protected int? _TaskID;
  protected int? _InventoryID;
  protected int? _CostCodeID;
  protected int? _AccountGroupID;

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PMChangeOrderBudget.refNbr))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMChangeOrderBudget.projectID))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PMChangeOrderBudget.projectTaskID))]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PMChangeOrderBudget.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PMChangeOrderBudget.costCodeID))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PMChangeOrderBudget.accountGroupID))]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  [PXDBBaseCury(BqlField = typeof (PMChangeOrderBudget.amount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total")]
  public virtual Decimal? Amount { get; set; }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderPrevioslyAmount.refNbr>
  {
  }

  public abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeOrderPrevioslyAmount.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderPrevioslyAmount.taskID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeOrderPrevioslyAmount.inventoryID>
  {
  }

  public abstract class costCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeOrderPrevioslyAmount.costCodeID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeOrderPrevioslyAmount.accountGroupID>
  {
  }

  public abstract class amount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderPrevioslyAmount.amount>
  {
  }
}
