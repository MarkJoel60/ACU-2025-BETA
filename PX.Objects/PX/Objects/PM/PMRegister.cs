// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMRegister
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a batch of <see cref="T:PX.Objects.PM.PMTran">project transactions</see>.
/// The records of this type are created through the Project Transactions (PM304000) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.RegisterEntry" /> graph),
/// or can also originate from other documents.
/// </summary>
[PXCacheName("Project Register")]
[PXPrimaryGraph(typeof (RegisterEntry))]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMRegister : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _Module;
  protected string _RefNbr;
  protected DateTime? _Date;
  protected string _Description;
  protected string _Status;
  protected bool? _Released;
  protected bool? _Hold;
  protected bool? _IsAllocation;
  protected string _OrigDocType;
  protected string _OrigDocNbr;
  protected Decimal? _QtyTotal;
  protected Decimal? _BillableQtyTotal;
  protected Decimal? _AmtTotal;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>
  /// The identifier of the functional area, to which the batch belongs.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// "GL", "AP", "AR", "IN", "PM", "CA", "DR", "PR".
  /// </value>
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault("PM")]
  [PXUIField]
  [BatchModule.PMList]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  /// <summary>The reference number of the document.</summary>
  /// <value>
  /// The number is generated from the <see cref="T:PX.Objects.CS.Numbering">numbering sequence</see>,
  /// which is specified on the <see cref="T:PX.Objects.PM.PMSetup">Project Preferences</see> form.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXSelector(typeof (Search<PMRegister.refNbr, Where<PMRegister.module, Equal<Current<PMRegister.module>>>, OrderBy<Desc<PMRegister.refNbr>>>), Filterable = true)]
  [PXUIField]
  [AutoNumber(typeof (Search<PMSetup.tranNumbering>), typeof (AccessInfo.businessDate))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>The date of the document.</summary>
  /// <value>Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">business date</see>.</value>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  /// <summary>The description of the document.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>The read-only status of the document.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"H"</c>: Hold,
  /// <c>"B"</c>: Balanced,
  /// <c>"R"</c>: Released
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("B")]
  [PMRegister.status.List]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document has been released.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document is on hold.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "On Hold")]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the batch was created as a result of the allocation process.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allocation Transaction", Visible = true)]
  public virtual bool? IsAllocation
  {
    get => this._IsAllocation;
    set => this._IsAllocation = value;
  }

  /// <summary>The type of the original document.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"AL"</c>: Allocation,
  /// <c>"TC"</c>: Time Card,
  /// <c>"CS"</c>: Case,
  /// <c>"EC"</c>: Expense Claim,
  /// <c>"ET"</c>: Equipment Time Card,
  /// <c>"AR"</c>: Allocation Reversal,
  /// <c>"RV"</c>: Reversal,
  /// <c>"IN"</c>: Invoice,
  /// <c>"CR"</c>: Credit Memo,
  /// <c>"DM"</c>: Debit Memo,
  /// <c>"UR"</c>: Unbilled Remainder,
  /// <c>"RR"</c>: Unbilled Remainder Reversal,
  /// <c>"PB"</c>: Pro Forma Billing,
  /// <c>"BL"</c>: Bill,
  /// <c>"CA"</c>: Credit Adjustment,
  /// <c>"DA"</c>: Debit Adjustment,
  /// <c>"WR"</c>: WIP Reversal,
  /// <c>"AP"</c>: Service Order,
  /// <c>"SO"</c>: Appointment,
  /// <c>"PR"</c>: Regular Paycheck,
  /// <c>"PS"</c>: Special Paycheck,
  /// <c>"PA"</c>: Adjustment Paycheck,
  /// <c>"PV"</c>: Void Paycheck,
  /// <c>"PF"</c>: Final Paycheck
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PMOrigDocType.List]
  [PXUIField]
  public virtual string OrigDocType
  {
    get => this._OrigDocType;
    set => this._OrigDocType = value;
  }

  /// <summary>The reference number of the original document.</summary>
  [PXDBString]
  [PXUIField(DisplayName = "Orig. Doc. Nbr.", Visible = false, Enabled = false)]
  public virtual string OrigDocNbr
  {
    get => this._OrigDocNbr;
    set => this._OrigDocNbr = value;
  }

  /// <summary>NoteID of the original document.</summary>
  [PXRefNote(LastKeyOnly = true)]
  [PXUIField]
  public virtual Guid? OrigNoteID { get; set; }

  /// <summary>
  /// The total quantity of items in the <see cref="T:PX.Objects.PM.PMTran">project transactions</see>.
  /// </summary>
  [PXDBQuantity]
  [PXUIField(DisplayName = "Total Quantity", Enabled = false)]
  public virtual Decimal? QtyTotal
  {
    get => this._QtyTotal;
    set => this._QtyTotal = value;
  }

  /// <summary>
  /// The total billable quantity for the <see cref="T:PX.Objects.PM.PMTran">project transactions</see>.
  /// </summary>
  [PXDBQuantity]
  [PXUIField(DisplayName = "Total Billable Quantity", Enabled = false)]
  public virtual Decimal? BillableQtyTotal
  {
    get => this._BillableQtyTotal;
    set => this._BillableQtyTotal = value;
  }

  /// <summary>
  /// The total amount for the <see cref="T:PX.Objects.PM.PMTran">project transactions</see> in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXUIField(DisplayName = "Total Amount", Enabled = false)]
  public virtual Decimal? AmtTotal
  {
    get => this._AmtTotal;
    set => this._AmtTotal = value;
  }

  /// <summary>
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Migrated", Visible = false, Enabled = false)]
  [PXDefault(false)]
  public virtual bool? IsMigratedRecord { get; set; }

  [PXNote]
  [NotePersist(typeof (PMRegister.noteID))]
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

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the values in the <see cref="P:PX.Objects.PM.PMTran.Amount" /> field
  /// of project transactions are displayed in the base currency.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? IsBaseCury { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRegister.selected>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRegister.module>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRegister.refNbr>
  {
    public const int Length = 15;
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMRegister.date>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRegister.description>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRegister.status>
  {
    public const string Hold = "H";
    public const string Balanced = "B";
    public const string Released = "R";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[3]
        {
          PXStringListAttribute.Pair("H", "Hold"),
          PXStringListAttribute.Pair("B", "Balanced"),
          PXStringListAttribute.Pair("R", "Released")
        })
      {
      }
    }

    public class hold : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PMRegister.status.hold>
    {
      public hold()
        : base("H")
      {
      }
    }

    public class balanced : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PMRegister.status.balanced>
    {
      public balanced()
        : base("B")
      {
      }
    }

    public class released : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PMRegister.status.released>
    {
      public released()
        : base("R")
      {
      }
    }
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRegister.released>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRegister.hold>
  {
  }

  public abstract class isAllocation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRegister.isAllocation>
  {
  }

  public abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRegister.origDocType>
  {
  }

  public abstract class origDocNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRegister.origDocNbr>
  {
  }

  public abstract class origNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMRegister.origNoteID>
  {
  }

  public abstract class qtyTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMRegister.qtyTotal>
  {
  }

  public abstract class billableQtyTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRegister.billableQtyTotal>
  {
  }

  public abstract class amtTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMRegister.amtTotal>
  {
  }

  public abstract class isMigratedRecord : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRegister.isMigratedRecord>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMRegister.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMRegister.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMRegister.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRegister.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMRegister.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMRegister.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRegister.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMRegister.lastModifiedDateTime>
  {
  }

  public abstract class isBaseCury : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRegister.isBaseCury>
  {
  }
}
