// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSubItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

/// <summary>
/// Represents a Subitem (or subitem code).
/// Subitems allow to track different variations of a product under one <see cref="T:PX.Objects.IN.InventoryItem" />.
/// Subitems are available only if the <see cref="P:PX.Objects.CS.FeaturesSet.SubItem">Inventory Subitems</see> feature is enabled.
/// The records of this type are created and edited through the Subitems (IN205000) form
/// (which corresponds to the <see cref="T:PX.Objects.IN.INSubItemMaint" /> graph).
/// </summary>
[PXCacheName]
[Serializable]
public class INSubItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SubItemID;
  protected 
  #nullable disable
  string _SubItemCD;
  protected string _Descr;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _GroupMask;
  protected byte[] _tstamp;

  /// <summary>
  /// Database identity.
  /// Unique identifier of the Subitem.
  /// </summary>
  [PXDBIdentity]
  [PXUIField(DisplayName = "Subitem ID", Enabled = false, Visible = false)]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  /// <summary>
  /// Key field.
  /// Unique user-friendly identifier of the Subitem.
  /// </summary>
  /// <value>
  /// The structure of the identifier is defined by the <i>INSUBITEM</i> <see cref="T:PX.Objects.CS.Dimension">Segmented Key</see>
  /// and usually reflects the important properties associated with the <see cref="T:PX.Objects.IN.InventoryItem">item</see>.
  /// </value>
  [SubItemRaw(IsKey = true)]
  [PXDefault]
  public virtual string SubItemCD
  {
    get => this._SubItemCD;
    set => this._SubItemCD = value;
  }

  /// <summary>The description of the Subitem.</summary>
  [PXDBString(60, IsUnicode = true)]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
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
  /// The group mask showing which <see cref="T:PX.SM.RelationGroup">restriction groups</see> the Subitem belongs to.
  /// </summary>
  [PXDBGroupMask]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>
  {
    public static INSubItem Find(PXGraph graph, int? subItemID, PKFindOptions options = 0)
    {
      return (INSubItem) PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.FindBy(graph, (object) subItemID, options);
    }
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSubItem.subItemID>
  {
  }

  public abstract class subItemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSubItem.subItemCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSubItem.descr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INSubItem.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSubItem.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSubItem.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INSubItem.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSubItem.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSubItem.lastModifiedDateTime>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INSubItem.groupMask>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INSubItem.Tstamp>
  {
  }

  public class Zero : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INSubItem.Zero>
  {
    public Zero()
      : base("0")
    {
    }
  }
}
