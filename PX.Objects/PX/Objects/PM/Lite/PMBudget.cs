// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Lite.PMBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM.Lite;

[PXCacheName("Budget")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMBudget : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IProjectFilter, IQuantify
{
  protected int? _ProjectID;
  protected int? _CostCodeID;
  protected int? _AccountGroupID;
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _Description;

  [PXDBInt(IsKey = true)]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>Get or set Project TaskID</summary>
  public int? TaskID => this.ProjectTaskID;

  [PMTaskCompleted]
  [PXDBInt(IsKey = true)]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXDBInt(IsKey = true)]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  [PXDBInt(IsKey = true)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Qty { get; set; }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
  public virtual string UOM { get; set; }

  [PXDBDecimal]
  public virtual Decimal? CuryRevisedAmount { get; set; }

  [PXDBDecimal]
  public virtual Decimal? CuryActualAmount { get; set; }

  [PXDBDecimal]
  public virtual Decimal? CuryCommittedOpenAmount { get; set; }

  [PXDBString(1)]
  [PXDefault]
  public virtual string Type { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? IsProduction { get; set; }

  [PXDBString]
  public string ProgressBillingBase { get; set; }

  /// <summary>The reference to a revenue budget line by task.</summary>
  [PXDBInt]
  public virtual int? RevenueTaskID { get; set; }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.costCodeID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.inventoryID>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.qty>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudget.uOM>
  {
  }

  public abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyRevisedAmount>
  {
  }

  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyActualAmount>
  {
  }

  public abstract class curyCommittedOpenAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyCommittedOpenAmount>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudget.type>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudget.description>
  {
  }

  public abstract class isProduction : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMBudget.isProduction>
  {
  }

  public abstract class progressBillingBase : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBudget.progressBillingBase>
  {
  }

  public abstract class revenueTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.revenueTaskID>
  {
  }
}
