// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBudgetProduction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("Budget Production")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMBudgetProduction : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IProjectFilter
{
  protected int? _ProjectID;
  protected int? _CostCodeID;
  protected int? _AccountGroupID;
  protected int? _InventoryID;
  protected int? _LineNbr;
  protected Guid? _NoteID;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMBudgetProduction.projectID>>>>))]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXDBInt(IsKey = true)]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>Get or set Project TaskID</summary>
  public int? TaskID => this.ProjectTaskID;

  [PXDBDefault(typeof (PMTask.taskID))]
  [PXParent(typeof (Select<PMTask, Where<PMTask.projectID, Equal<Current<PMBudgetProduction.projectID>>, And<PMTask.taskID, Equal<Current<PMBudgetProduction.projectTaskID>>>>>))]
  [PXDBInt(IsKey = true)]
  public virtual int? ProjectTaskID { get; set; }

  [CostCode(IsKey = true, SkipVerification = true)]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXDefault]
  [PXDBInt(IsKey = true)]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<PMCostBudget, Where<PMCostBudget.projectID, Equal<Current<PMBudgetProduction.projectID>>, And<PMCostBudget.projectTaskID, Equal<Current<PMBudgetProduction.projectTaskID>>, And<PMCostBudget.accountGroupID, Equal<Current<PMBudgetProduction.accountGroupID>>, And<PMCostBudget.costCodeID, Equal<Current<PMBudgetProduction.costCodeID>>, And<PMCostBudget.inventoryID, Equal<Current<PMBudgetProduction.inventoryID>>>>>>>>))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost To Complete")]
  public virtual Decimal? CuryCostToComplete { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost To Complete in Base Currency")]
  public virtual Decimal? CostToComplete { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "% Complete")]
  public virtual Decimal? PercentCompleted { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost At Completion")]
  public virtual Decimal? CuryCostAtCompletion { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost At Completion in Base Currency")]
  public virtual Decimal? CostAtCompletion { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetProduction.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetProduction.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetProduction.costCodeID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMBudgetProduction.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetProduction.inventoryID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetProduction.lineNbr>
  {
  }

  public abstract class curyCostToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetProduction.curyCostToComplete>
  {
  }

  public abstract class costToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetProduction.costToComplete>
  {
  }

  public abstract class percentCompleted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetProduction.percentCompleted>
  {
  }

  public abstract class curyCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetProduction.curyCostAtCompletion>
  {
  }

  public abstract class costAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetProduction.costAtCompletion>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMBudgetProduction.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMBudgetProduction.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMBudgetProduction.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBudgetProduction.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMBudgetProduction.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMBudgetProduction.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBudgetProduction.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMBudgetProduction.lastModifiedDateTime>
  {
  }
}
