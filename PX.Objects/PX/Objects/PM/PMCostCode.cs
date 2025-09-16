// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCostCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a cost code.
/// Cost codes are used to classify project revenues and costs in construction projects and
/// can be associated with documents and document lines in which projects are referenced.
/// The records of this type are created and edited through the Cost Codes (PM209500) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.CostCodeMaint" /> graph).
/// </summary>
[DebuggerDisplay("{CostCodeCD}({CostCodeID})-{Description}")]
[PXCacheName("Cost Code")]
[PXPrimaryGraph(typeof (CostCodeMaint))]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMCostCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _CostCodeID;
  protected 
  #nullable disable
  string _CostCodeCD;
  protected bool? _IsDefault = new bool?(false);
  protected string _Description;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// Gets or sets whether the task is selected in the grid.
  /// </summary>
  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>Gets or sets unique identifier.</summary>
  [PXDBIdentity]
  [PXSelector(typeof (PMCostCode.costCodeID))]
  [PXReferentialIntegrityCheck]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>
  /// Get or sets unique identifier.
  /// This is a segmented key and format is configured under segmented key maintenance screen in CS module.
  /// </summary>
  [PXDimensionSelector("COSTCODE", typeof (Search<PMCostCode.costCodeCD>), typeof (PMCostCode.costCodeCD), DescriptionField = typeof (PMCostCode.description))]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string CostCodeCD
  {
    get => this._CostCodeCD;
    set => this._CostCodeCD = value;
  }

  /// <summary>Returns True for the Default Cost Code.</summary>
  [PXUIField(DisplayName = "Default", Enabled = false, Visible = false)]
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsDefault
  {
    get => this._IsDefault;
    set => this._IsDefault = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the cost code is available for use.
  /// </summary>
  [PXUIField(DisplayName = "Active")]
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? IsActive { get; set; }

  /// <summary>Gets or sets description</summary>
  [PXDBLocalizableString(250, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsProjectOverride { get; set; }

  [PXNote(DescriptionField = typeof (PMCostCode.costCodeCD))]
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
  public class PK : PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>
  {
    public static PMCostCode Find(PXGraph graph, int? costCodeID, PKFindOptions options = 0)
    {
      return (PMCostCode) PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.FindBy(graph, (object) costCodeID, options);
    }
  }

  /// <summary>Unique Key</summary>
  public class UK : PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeCD>
  {
    public static PMCostCode Find(PXGraph graph, string costCodeCD, PKFindOptions options = 0)
    {
      return (PMCostCode) PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeCD>.FindBy(graph, (object) costCodeCD, options);
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostCode.selected>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostCode.costCodeID>
  {
  }

  public abstract class costCodeCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostCode.costCodeCD>
  {
    public const string DimensionName = "COSTCODE";
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostCode.isDefault>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostCode.isActive>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostCode.description>
  {
  }

  public abstract class isProjectOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMCostCode.isProjectOverride>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCostCode.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMCostCode.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCostCode.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostCode.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostCode.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCostCode.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostCode.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostCode.lastModifiedDateTime>
  {
  }
}
