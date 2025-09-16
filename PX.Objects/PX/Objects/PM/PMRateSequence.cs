// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMRateSequence
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
/// A combination of rate table, rate type, rate code, and rate sequence for which <see cref="T:PX.Objects.PM.PMRate">rates</see> can be set.
/// The records of this type are created and edited through the Rate Tables (PM206000) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.RateMaint" /> graph).
/// </summary>
[PXCacheName("Rate Lookup Rule Sequence")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMRateSequence : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMRateTable">rate table</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMRateTable.rateTableID" /> field.
  /// </value>
  protected 
  #nullable disable
  string _RateTableID;
  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMRateTable">rate type</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMRateTable.rateTableID" /> field.
  /// </value>
  protected string _RateTypeID;
  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMRateDefinition">sequence</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMRateDefinition.RateDefinitionID" /> field.
  /// </value>
  protected int? _Sequence;
  /// <summary>The identifier of the rate code.</summary>
  protected string _RateCodeID;
  /// <summary>The description of the rate code.</summary>
  protected string _Description;
  /// <summary>The rate definition ID of the rate sequence.</summary>
  /// <value>
  /// The value of this field is set by the <see cref="T:PX.Objects.PM.PMRateDefinition.rateDefinitionID" /> DAC
  /// </value>
  protected int? _RateDefinitionID;
  protected int? _LineCntr;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMRateTable">rate table</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMRateTable.rateTableID" /> field.
  /// </value>
  [PXDefault]
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Rate Table Code")]
  [PXSelector(typeof (PMRateTable.rateTableID), new Type[] {typeof (PMRateTable.rateTableID), typeof (PMRateTable.description)}, Headers = new string[] {"Rate Table Code", "Description"}, DescriptionField = typeof (PMRateTable.description))]
  public virtual string RateTableID
  {
    get => this._RateTableID;
    set => this._RateTableID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMRateTable">rate type</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMRateTable.rateTableID" /> field.
  /// </value>
  [PXDefault]
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXSelector(typeof (PMRateType.rateTypeID), DescriptionField = typeof (PMRateType.description))]
  [PXUIField(DisplayName = "Rate Type")]
  public virtual string RateTypeID
  {
    get => this._RateTypeID;
    set => this._RateTypeID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMRateDefinition">sequence</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMRateDefinition.RateDefinitionID" /> field.
  /// </value>
  [PXDefault]
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Rate Table Sequence")]
  public virtual int? Sequence
  {
    get => this._Sequence;
    set => this._Sequence = value;
  }

  /// <summary>The identifier of the rate code.</summary>
  [PXSelector(typeof (Search<PMRateSequence.rateCodeID, Where<PMRateSequence.rateTableID, Equal<Current<PMRateSequence.rateTableID>>, And<PMRateSequence.rateTypeID, Equal<Current<PMRateSequence.rateTypeID>>, And<PMRateSequence.sequence, Equal<Current<PMRateSequence.sequence>>>>>>), DescriptionField = typeof (PMRateSequence.description))]
  [PXUIField(DisplayName = "Rate Code")]
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string RateCodeID
  {
    get => this._RateCodeID;
    set => this._RateCodeID = value;
  }

  /// <summary>The description of the rate code.</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>The rate definition ID of the rate sequence.</summary>
  /// <value>
  /// The value of this field is defined by <see cref="T:PX.Objects.PM.PMRateDefinition.rateDefinitionID" /> of the parent DAC.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (Parent<PMRateDefinition.rateDefinitionID>))]
  [PXParent(typeof (Select<PMRateDefinition, Where<PMRateDefinition.rateTableID, Equal<Current<PMRateSequence.rateTableID>>, And<PMRateDefinition.rateTypeID, Equal<Current<PMRateSequence.rateTypeID>>, And<PMRateDefinition.sequence, Equal<Current<PMRateSequence.sequence>>>>>>))]
  public virtual int? RateDefinitionID
  {
    get => this._RateDefinitionID;
    set => this._RateDefinitionID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

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

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateSequence.RateTableID" />
  public abstract class rateTableID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRateSequence.rateTableID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateSequence.RateTypeID" />
  public abstract class rateTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRateSequence.rateTypeID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateSequence.Sequence" />
  public abstract class sequence : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRateSequence.sequence>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateSequence.RateCodeID" />
  public abstract class rateCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRateSequence.rateCodeID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateSequence.Description" />
  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRateSequence.description>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateSequence.RateDefinitionID" />
  public abstract class rateDefinitionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMRateSequence.rateDefinitionID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRateSequence.LineCntr" />
  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRateSequence.lineCntr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMRateSequence.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMRateSequence.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMRateSequence.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRateSequence.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMRateSequence.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMRateSequence.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRateSequence.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMRateSequence.lastModifiedDateTime>
  {
  }
}
