// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMRateDefinition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// A rate definition that is defined for a combination of a rate table and a rate type.
/// The rate definition includes the sequence number and the types of factors to which the rate is applicable.
/// The records of this type are created and edited through the Rate Lookup Rules (PM205000) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.RateDefinitionMaint" /> graph).
/// </summary>
[PXCacheName("Rate Lookup Rule")]
[PXPrimaryGraph(typeof (RateMaint))]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMRateDefinition : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The identifier of the rate definition.</summary>
  protected int? _RateDefinitionID;
  /// <summary>The rate table to which the rate definition belongs.</summary>
  protected 
  #nullable disable
  string _RateTableID;
  /// <summary>The rate type to which the rate definition belongs.</summary>
  protected string _RateTypeID;
  /// <summary>The sequence of the rate table.</summary>
  protected short? _Sequence;
  /// <summary>The description of the rate definition.</summary>
  protected string _Description;
  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the project is a factor that affects rate selection.
  /// </summary>
  protected bool? _Project;
  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the task is a factor that affects rate selection.
  /// </summary>
  protected bool? _Task;
  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the account group is a factor that affects rate selection.
  /// </summary>
  protected bool? _AccountGroup;
  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the inventory is a factor that affects rate selection.
  /// </summary>
  protected bool? _RateItem;
  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the employee is a factor that affects rate selection.
  /// </summary>
  protected bool? _Employee;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>The identifier of the rate definition.</summary>
  [PXDBIdentity(IsKey = true)]
  public virtual int? RateDefinitionID
  {
    get => this._RateDefinitionID;
    set => this._RateDefinitionID = value;
  }

  /// <summary>The rate table to which the rate definition belongs.</summary>
  [PXDefault]
  [PXDBString(15, IsUnicode = true)]
  public virtual string RateTableID
  {
    get => this._RateTableID;
    set => this._RateTableID = value;
  }

  /// <summary>The rate type to which the rate definition belongs.</summary>
  [PXDefault]
  [PXDBString(15, IsUnicode = true)]
  public virtual string RateTypeID
  {
    get => this._RateTypeID;
    set => this._RateTypeID = value;
  }

  /// <summary>The sequence of the rate table.</summary>
  [PXDefault(TypeCode.Int16, "1")]
  [PXDBShort]
  [PXUIField(DisplayName = "Rate Table Sequence")]
  public virtual short? Sequence
  {
    get => this._Sequence;
    set => this._Sequence = value;
  }

  /// <summary>The description of the rate definition.</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the project is a factor that affects rate selection.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Project")]
  public virtual bool? Project
  {
    get => this._Project;
    set => this._Project = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the task is a factor that affects rate selection.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Project Task")]
  public virtual bool? Task
  {
    get => this._Task;
    set => this._Task = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the account group is a factor that affects rate selection.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Account Group")]
  public virtual bool? AccountGroup
  {
    get => this._AccountGroup;
    set => this._AccountGroup = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the inventory item is a factor that affects rate selection.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Inventory")]
  public virtual bool? RateItem
  {
    get => this._RateItem;
    set => this._RateItem = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the employee is a factor that affects rate selection.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Employee")]
  public virtual bool? Employee
  {
    get => this._Employee;
    set => this._Employee = value;
  }

  [PXNote(DescriptionField = typeof (PMTask.taskCD))]
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

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateDefinition.RateDefinitionID" />
  public abstract class rateDefinitionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMRateDefinition.rateDefinitionID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateDefinition.RateTableID" />
  public abstract class rateTableID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRateDefinition.rateTableID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateDefinition.RateTypeID" />
  public abstract class rateTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRateDefinition.rateTypeID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateDefinition.Sequence" />
  public abstract class sequence : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  PMRateDefinition.sequence>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateDefinition.Description" />
  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRateDefinition.description>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateDefinition.Project" />
  public abstract class project : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRateDefinition.project>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateDefinition.Task" />
  public abstract class task : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRateDefinition.task>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateDefinition.AccountGroup" />
  public abstract class accountGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRateDefinition.accountGroup>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateDefinition.RateItem" />
  public abstract class rateItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRateDefinition.rateItem>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateDefinition.Employee" />
  public abstract class employee : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRateDefinition.employee>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMRateDefinition.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMRateDefinition.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMRateDefinition.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRateDefinition.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMRateDefinition.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMRateDefinition.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRateDefinition.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMRateDefinition.lastModifiedDateTime>
  {
  }
}
