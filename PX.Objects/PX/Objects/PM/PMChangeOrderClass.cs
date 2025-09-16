// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMChangeOrderClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// A change order class.
/// Change order classes are used in <see cref="T:PX.Objects.PM.PMChangeOrder">change orders</see> and define
/// which project data (the revenue budget, the cost budget, or commitments) can be adjusted
/// with a change order of this class.
/// The records of this type are created and edited through the Change Order Classes (PM203000) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ChangeOrderClassMaint" /> graph).
/// </summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Change Order Class")]
[PXPrimaryGraph(typeof (ChangeOrderClassMaint))]
[Serializable]
public class PMChangeOrderClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
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

  /// <summary>The identifier of the change order class.</summary>
  [PXReferentialIntegrityCheck]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (PMChangeOrderClass.classID), DescriptionField = typeof (PMChangeOrderClass.description))]
  public virtual string ClassID { get; set; }

  /// <summary>The description of the change order class.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the user can modify existing cost budget lines and add new ones with change orders of this class.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Cost Budget")]
  public virtual bool? IsCostBudgetEnabled { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the user can modify existing revenue budget lines and add new ones with change orders of this class.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Revenue Budget")]
  public virtual bool? IsRevenueBudgetEnabled { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the user can modify existing commitments and add new ones with change orders of this class.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Commitments")]
  public virtual bool? IsPurchaseOrderEnabled { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the change order class is available for selection on the Change Orders (PM308000) form.
  /// </summary>
  [PXUIField(DisplayName = "Active")]
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? IsActive { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the change order class supports two-tier change management.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Two-Tier Change Management", FieldClass = "ChangeRequest")]
  [PXDefault(false)]
  public virtual bool? IsAdvance { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the <see cref="P:PX.Objects.PM.PMChangeOrderClass.IsRevenueBudgetEnabled" /> field is <see langword="true" />.
  /// </summary>
  /// <value>
  /// The value of this field is defined by the <see cref="P:PX.Objects.PM.PMChangeOrderClass.IsRevenueBudgetEnabled" /> field.
  /// See the attributes of the <see cref="P:PX.Objects.PM.PMChangeOrderClass.IsRevenueBudgetEnabled" /> field for details.
  /// </value>
  [PXBool]
  public virtual bool? IncrementsProjectNumber
  {
    get => new bool?(this.IsRevenueBudgetEnabled.GetValueOrDefault());
  }

  [PXNote(DescriptionField = typeof (PMChangeOrderClass.description))]
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

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<PMChangeOrderClass>.By<PMChangeOrderClass.classID>
  {
    public static PMChangeOrderClass Find(PXGraph graph, string classID, PKFindOptions options = 0)
    {
      return (PMChangeOrderClass) PrimaryKeyOf<PMChangeOrderClass>.By<PMChangeOrderClass.classID>.FindBy(graph, (object) classID, options);
    }
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderClass.ClassID" />
  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrderClass.classID>
  {
    public const int Length = 15;
    public const string InputMask = ">CCCCCCCCCCCCCCC";
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderClass.Description" />
  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderClass.description>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderClass.IsCostBudgetEnabled" />
  public abstract class isCostBudgetEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMChangeOrderClass.isCostBudgetEnabled>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderClass.IsRevenueBudgetEnabled" />
  public abstract class isRevenueBudgetEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMChangeOrderClass.isRevenueBudgetEnabled>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderClass.IsPurchaseOrderEnabled" />
  public abstract class isPurchaseOrderEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMChangeOrderClass.isPurchaseOrderEnabled>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderClass.IsActive" />
  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeOrderClass.isActive>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderClass.IsAdvance" />
  public abstract class isAdvance : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeOrderClass.isAdvance>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderClass.IncrementsProjectNumber" />
  public abstract class incrementsProjectNumber : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMChangeOrderClass.incrementsProjectNumber>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMChangeOrderClass.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMChangeOrderClass.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMChangeOrderClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeOrderClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMChangeOrderClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeOrderClass.lastModifiedDateTime>
  {
  }
}
