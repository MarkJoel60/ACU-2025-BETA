// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMRevisedCOAmount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CN;

[PXHidden]
[PXProjection(typeof (Select2<PMBudget, CrossJoin<EPEarningType>, Where<PMBudget.type, Equal<AccountType.income>, And<Where<EPEarningType.typeCD, Equal<EPSetup.EarningTypeRG>, Or<EPEarningType.typeCD, Equal<EPSetup.EarningTypeHL>>>>>>))]
[Serializable]
public class PMRevisedCOAmount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.projectTaskID))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.accountGroupID))]
  public virtual int? AccountGroupID { get; set; }

  [PXString(255 /*0xFF*/, IsKey = true)]
  [PXUIField(DisplayName = "Type")]
  [PXDBCalced(typeof (Switch<Case<Where<EPEarningType.typeCD, Equal<EPSetup.EarningTypeRG>>, PMRevisedCOAmount.approvedChangeOrder>, PMRevisedCOAmount.revisedContract>), typeof (string))]
  public virtual 
  #nullable disable
  string Type { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Rev Contract And COAmt")]
  [PXDBCalced(typeof (Switch<Case<Where<EPEarningType.typeCD, Equal<EPSetup.EarningTypeRG>>, PMBudget.changeOrderAmount>, Add<PMBudget.amount, PMBudget.changeOrderAmount>>), typeof (Decimal))]
  public virtual Decimal? RevContractAndCOAmt { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRevisedCOAmount.selected>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRevisedCOAmount.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRevisedCOAmount.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRevisedCOAmount.costCodeID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRevisedCOAmount.inventoryID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMRevisedCOAmount.accountGroupID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRevisedCOAmount.type>
  {
  }

  public abstract class revContractAndCOAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevisedCOAmount.revContractAndCOAmt>
  {
  }

  public sealed class approvedChangeOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PMRevisedCOAmount.approvedChangeOrder>
  {
    public approvedChangeOrder()
      : base("Approved Change Order")
    {
    }
  }

  public sealed class revisedContract : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PMRevisedCOAmount.revisedContract>
  {
    public revisedContract()
      : base("Revised Contract")
    {
    }
  }
}
