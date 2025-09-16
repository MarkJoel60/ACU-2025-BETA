// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMForecast
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CT;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a revision of a project budget forecast.
/// The records of this type are created and edited through the Project Budget Forecast (PM209600) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ForecastMaint" /> graph).
/// </summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Budget Forecast")]
[PXPrimaryGraph(typeof (ForecastMaint))]
[Serializable]
public class PMForecast : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ProjectID;
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

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> of the project budget forecast.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDefault]
  [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>>), IsKey = true, WarnIfCompleted = false)]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMForecast.projectID>>>>))]
  [PXForeignReference(typeof (Field<PMForecast.projectID>.IsRelatedTo<PMProject.contractID>))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// The revision identifier of the project budget forecast.
  /// </summary>
  [PXSelector(typeof (Search<PMForecast.revisionID, Where<PMForecast.projectID, Equal<Current<PMForecast.projectID>>>, OrderBy<Desc<PMForecast.revisionID>>>), DescriptionField = typeof (PMForecast.description))]
  [PXDBString(10, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual string RevisionID { get; set; }

  /// <summary>
  /// The description of the revision of the project budget forecast.
  /// </summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description { get; set; }

  /// <summary>
  /// The description of the corresponding <see cref="T:PX.Objects.PM.PMProject">project</see>.
  /// </summary>
  [PXString]
  [PXFormula(typeof (Selector<PMForecast.projectID, PMProject.description>))]
  public string FormCaptionDescription { get; set; }

  [PXNote(DescriptionField = typeof (PMForecast.description))]
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
  PMForecast.projectID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMForecast.revisionID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMForecast.description>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMForecast.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMForecast.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMForecast.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMForecast.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMForecast.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMForecast.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMForecast.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMForecast.lastModifiedDateTime>
  {
  }
}
