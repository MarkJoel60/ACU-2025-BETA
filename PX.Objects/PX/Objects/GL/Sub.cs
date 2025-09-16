// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Sub
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// A general ledger subaccount.
/// The records of this type are added and edited through the Subaccounts (GL203000) form
/// (which corresponds to the <see cref="T:PX.Objects.GL.SubAccountMaint" /> graph).
/// </summary>
[PXCacheName("Subaccount", CacheGlobal = true)]
[PXPrimaryGraph(typeof (SubAccountMaint))]
[Serializable]
public class Sub : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IIncludable, IRestricted
{
  protected int? _SubID;
  protected 
  #nullable disable
  string _SubCD;
  protected bool? _Active;
  protected string _Description;
  protected byte[] _GroupMask;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _Included;

  /// <summary>
  /// Database identity.
  /// Unique identifier of the Subaccount.
  /// </summary>
  [PXDBIdentity]
  [PXUIField]
  [PXReferentialIntegrityCheck]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>
  /// Key field.
  /// Unique user-friendly <see cref="T:PX.Objects.CS.Dimension">segmented key</see> of the Subaccount.
  /// </summary>
  [PXDefault]
  [SubAccountRaw]
  public virtual string SubCD
  {
    get => this._SubCD;
    set => this._SubCD = value;
  }

  /// <summary>
  /// Indicates whether the Subaccount is <c>active</c>.
  /// Inactive subaccounts do not appear in the lists of available subaccounts and
  /// thus can't be selected for documents, transactions and other entities.
  /// </summary>
  /// <value>
  /// Defaults to <c>true</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  /// <summary>The description of the Subaccount.</summary>
  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBInt]
  [PXUIField]
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public virtual int? ConsoSubID { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Consolidation Subaccount Code", Visible = false)]
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public virtual string ConsoSubCode { get; set; }

  /// <summary>
  /// The group mask showing which <see cref="T:PX.SM.RelationGroup">restriction groups</see> the Subaccount belongs to.
  /// </summary>
  [PXDBGroupMask]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the Subaccount.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote(DescriptionField = typeof (Sub.description))]
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

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <summary>
  /// An unbound field used in the User Interface to include the Subaccount into a <see cref="T:PX.SM.RelationGroup">restriction group</see>.
  /// </summary>
  [PXUnboundDefault(false)]
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  public virtual bool? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  public class PK : PrimaryKeyOf<Sub>.By<Sub.subID>
  {
    public static Sub Find(PXGraph graph, int? subID, PKFindOptions options = 0)
    {
      return (Sub) PrimaryKeyOf<Sub>.By<Sub.subID>.FindBy(graph, (object) subID, options);
    }
  }

  public class UK : PrimaryKeyOf<Sub>.By<Sub.subCD>
  {
    public static Sub Find(PXGraph graph, string subCD, PKFindOptions options = 0)
    {
      return (Sub) PrimaryKeyOf<Sub>.By<Sub.subCD>.FindBy(graph, (object) subCD, options);
    }
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Sub.subID>
  {
  }

  public abstract class subCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Sub.subCD>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Sub.active>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Sub.description>
  {
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public abstract class consoSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Sub.consoSubID>
  {
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public abstract class consoSubCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Sub.consoSubCode>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Sub.groupMask>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Sub.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Sub.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Sub.createdByID>
  {
  }

  public abstract class createdByScreenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Sub.createdByScreenID>
  {
  }

  public abstract class createdDateTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Sub.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Sub.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Sub.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Sub.lastModifiedDateTime>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Sub.included>
  {
  }

  public class Keys
  {
    public int? SubID { get; set; }

    public string SubCD { get; set; }
  }
}
