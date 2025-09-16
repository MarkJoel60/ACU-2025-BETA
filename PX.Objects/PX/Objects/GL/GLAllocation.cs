// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLAllocation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL.Constants;
using PX.Objects.GL.DAC;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXCacheName("Allocation")]
[PXPrimaryGraph(typeof (AllocationMaint))]
[Serializable]
public class GLAllocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _GLAllocationID;
  protected string _Descr;
  protected bool? _Active;
  protected string _StartFinPeriodID;
  protected string _EndFinPeriodID;
  protected bool? _Recurring;
  protected string _AllocMethod;
  protected int? _BranchID;
  protected int? _AllocLedgerID;
  protected int? _SourceLedgerID;
  protected int? _BasisLederID;
  protected short? _SortOrder;
  protected Guid? _NoteID;
  protected DateTime? _LastRevisionOn;
  protected string _AllocCollectMethod;
  protected bool? _AllocateSeparately;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [AutoNumber(typeof (GLSetup.allocationNumberingID), typeof (AccessInfo.businessDate))]
  [PXUIField]
  [PXSelector(typeof (Search<GLAllocation.gLAllocationID>))]
  [PXFieldDescription]
  public virtual string GLAllocationID
  {
    get => this._GLAllocationID;
    set => this._GLAllocationID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [FinPeriodSelector(null, null, typeof (GLAllocation.branchID), null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  [PXUIField(DisplayName = "Start Period")]
  public virtual string StartFinPeriodID
  {
    get => this._StartFinPeriodID;
    set => this._StartFinPeriodID = value;
  }

  [FinPeriodSelector(null, null, typeof (GLAllocation.branchID), null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  [PXUIField(DisplayName = "End Period")]
  public virtual string EndFinPeriodID
  {
    get => this._EndFinPeriodID;
    set => this._EndFinPeriodID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Recurring")]
  public virtual bool? Recurring
  {
    get => this._Recurring;
    set => this._Recurring = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  [PXUIField(DisplayName = "Distribution Method")]
  [AllocationMethod.List]
  public virtual string AllocMethod
  {
    get => this._AllocMethod;
    set => this._AllocMethod = value;
  }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXInt]
  [PXFormula(typeof (Selector<GLAllocation.branchID, Branch.organizationID>))]
  public virtual int? OrganizationID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<Branch.ledgerID, Where<Branch.branchID, Equal<Current<GLAllocation.branchID>>>>))]
  [PXUIField(DisplayName = "Allocation Ledger")]
  [PXSelector(typeof (Search2<Ledger.ledgerID, InnerJoin<OrganizationLedgerLink, On<Ledger.ledgerID, Equal<OrganizationLedgerLink.ledgerID>, And<OrganizationLedgerLink.organizationID, Equal<Current<GLAllocation.organizationID>>>>>, Where<Ledger.balanceType, NotEqual<LedgerBalanceType.budget>>>), SubstituteKey = typeof (Ledger.ledgerCD), DescriptionField = typeof (Ledger.descr))]
  public virtual int? AllocLedgerID
  {
    get => this._AllocLedgerID;
    set => this._AllocLedgerID = value;
  }

  /// <summary>
  /// The allocation ledger balance type that is updated whenever the allocation ledger changes. <br />
  /// It affects the range of <see cref="T:PX.Objects.GL.Branch">branches</see> available for selection as the <see cref="T:PX.Objects.GL.GLAllocationDestination">allocation destination</see>
  /// <see cref="P:PX.Objects.GL.GLAllocationDestination.BranchID"> branch</see>.
  /// </summary>
  /// <value>
  /// The <see cref="P:PX.Objects.GL.Ledger.BalanceType" /> of the ledger referenced by the <see cref="P:PX.Objects.GL.GLAllocation.AllocLedgerID" /> field.
  /// </value>
  [PXFormula(typeof (Selector<GLAllocation.allocLedgerID, Ledger.balanceType>))]
  [PXUIField]
  public virtual string AllocLedgerBalanceType { get; set; }

  /// <summary>
  /// The allocation ledger base currency that is updated whenever the allocation ledger changes. <br />
  /// It affects the range of <see cref="T:PX.Objects.GL.Branch">branches</see> available for selection as the <see cref="T:PX.Objects.GL.GLAllocationDestination">allocation destination</see>
  /// <see cref="P:PX.Objects.GL.GLAllocationDestination.BranchID"> branch</see>.
  /// </summary>
  /// <value>
  /// The <see cref="P:PX.Objects.GL.Ledger.BaseCuryID" /> of the ledger referenced by the <see cref="P:PX.Objects.GL.GLAllocation.AllocLedgerID" /> field.
  /// </value>
  [PXFormula(typeof (Selector<GLAllocation.allocLedgerID, Ledger.baseCuryID>))]
  [PXUIField]
  public virtual string AllocLedgerBaseCuryID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<Branch.ledgerID, Where<Branch.branchID, Equal<Current<GLAllocation.branchID>>>>))]
  [PXUIField(DisplayName = "Source Ledger")]
  [PXSelector(typeof (Search2<Ledger.ledgerID, InnerJoin<LedgerA, On<Ledger.baseCuryID, Equal<LedgerA.baseCuryID>>>, Where<LedgerA.ledgerID, Equal<Current<GLAllocation.allocLedgerID>>>>), SubstituteKey = typeof (Ledger.ledgerCD), DescriptionField = typeof (Ledger.descr))]
  public virtual int? SourceLedgerID
  {
    get => this._SourceLedgerID;
    set => this._SourceLedgerID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Base Ledger")]
  [PXSelector(typeof (Ledger.ledgerID), SubstituteKey = typeof (Ledger.ledgerCD), DescriptionField = typeof (Ledger.descr), CacheGlobal = true)]
  public virtual int? BasisLederID
  {
    get => this._BasisLederID;
    set => this._BasisLederID = value;
  }

  [PXDBShort]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Sort Order")]
  public virtual short? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXNote(DescriptionField = typeof (GLAllocation.gLAllocationID))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Last Revision Date")]
  public virtual DateTime? LastRevisionOn
  {
    get => this._LastRevisionOn;
    set => this._LastRevisionOn = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [PXUIField(DisplayName = "Allocation Method")]
  [AllocationCollectMethod.List]
  public virtual string AllocCollectMethod
  {
    get => this._AllocCollectMethod;
    set => this._AllocCollectMethod = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allocate Source Accounts Separately")]
  public virtual bool? AllocateSeparately
  {
    get => this._AllocateSeparately;
    set => this._AllocateSeparately = value;
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

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<GLAllocation>.By<GLAllocation.gLAllocationID>
  {
    public static GLAllocation Find(PXGraph graph, string gLAllocationID, PKFindOptions options = 0)
    {
      return (GLAllocation) PrimaryKeyOf<GLAllocation>.By<GLAllocation.gLAllocationID>.FindBy(graph, (object) gLAllocationID, options);
    }
  }

  public static class FK
  {
    public class AllocationLedger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLAllocation>.By<GLAllocation.allocLedgerID>
    {
    }

    public class SourceLedger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLAllocation>.By<GLAllocation.sourceLedgerID>
    {
    }

    public class BaseLedger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLAllocation>.By<GLAllocation.basisLederID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLAllocation>.By<GLAllocation.branchID>
    {
    }
  }

  public abstract class gLAllocationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocation.gLAllocationID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLAllocation.descr>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLAllocation.active>
  {
  }

  public abstract class startFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocation.startFinPeriodID>
  {
  }

  public abstract class endFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocation.endFinPeriodID>
  {
  }

  public abstract class recurring : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLAllocation.recurring>
  {
  }

  public abstract class allocMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLAllocation.allocMethod>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLAllocation.branchID>
  {
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLAllocation.organizationID>
  {
  }

  public abstract class allocLedgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLAllocation.allocLedgerID>
  {
  }

  public abstract class allocLedgerBalanceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocation.allocLedgerBalanceType>
  {
  }

  public abstract class allocLedgerBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocation.allocLedgerBaseCuryID>
  {
  }

  public abstract class sourceLedgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLAllocation.sourceLedgerID>
  {
  }

  public abstract class basisLederID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLAllocation.basisLederID>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  GLAllocation.sortOrder>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLAllocation.noteID>
  {
  }

  public abstract class lastRevisionOn : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLAllocation.lastRevisionOn>
  {
  }

  public abstract class allocCollectMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocation.allocCollectMethod>
  {
  }

  public abstract class allocateSeparately : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLAllocation.allocateSeparately>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLAllocation.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocation.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLAllocation.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLAllocation.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocation.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLAllocation.lastModifiedDateTime>
  {
  }
}
