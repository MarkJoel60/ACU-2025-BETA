// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerClassLotSerNumVal
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
/// Represents a Auto-Incremental Value of a Lot/Serial Class.
/// Auto-Incremental Value of a Lot/Serial Class are available only if the <see cref="!:FeaturesSet.LotSerialTracking">Lot/Serial Tracking</see> feature is enabled.
/// The records of this type are created on the Lot/Serial Classes (IN207000) form
/// (which corresponds to the <see cref="T:PX.Objects.IN.INLotSerClassMaint" /> graph)
/// </summary>
[PXPrimaryGraph(typeof (INLotSerClassMaint))]
[PXCacheName("Auto-Incremental Value of a Lot/Serial Class")]
[Serializable]
public class INLotSerClassLotSerNumVal : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILotSerNumVal
{
  protected 
  #nullable disable
  string _LotSerClassID;
  protected string _LotSerNumVal;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INLotSerClass">lot/serial class</see>, to which the item is assigned.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INLotSerClass.LotSerClassID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (INLotSerClass.lotSerClassID))]
  [PXParent(typeof (INLotSerClassLotSerNumVal.FK.LotSerialClass))]
  public virtual string LotSerClassID
  {
    get => this._LotSerClassID;
    set => this._LotSerClassID = value;
  }

  [PXDBString(30, InputMask = "999999999999999999999999999999")]
  [PXUIField(DisplayName = "Auto-Incremental Value")]
  public virtual string LotSerNumVal
  {
    get => this._LotSerNumVal;
    set => this._LotSerNumVal = value;
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
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INLotSerClassLotSerNumVal>.By<INLotSerClassLotSerNumVal.lotSerClassID>
  {
    public static INLotSerClassLotSerNumVal Find(
      PXGraph graph,
      string lotSerClassID,
      PKFindOptions options = 0)
    {
      return (INLotSerClassLotSerNumVal) PrimaryKeyOf<INLotSerClassLotSerNumVal>.By<INLotSerClassLotSerNumVal.lotSerClassID>.FindBy(graph, (object) lotSerClassID, options);
    }

    public static INLotSerClassLotSerNumVal FindDirty(PXGraph graph, string lotSerClassID)
    {
      return PXResultset<INLotSerClassLotSerNumVal>.op_Implicit(PXSelectBase<INLotSerClassLotSerNumVal, PXSelect<INLotSerClassLotSerNumVal, Where<INLotSerClassLotSerNumVal.lotSerClassID, Equal<Required<INLotSerClassLotSerNumVal.lotSerClassID>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
      {
        (object) lotSerClassID
      }));
    }
  }

  public static class FK
  {
    public class LotSerialClass : 
      PrimaryKeyOf<INLotSerClass>.By<INLotSerClass.lotSerClassID>.ForeignKeyOf<INLotSerClassLotSerNumVal>.By<INLotSerClassLotSerNumVal.lotSerClassID>
    {
    }
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerClassLotSerNumVal.lotSerClassID>
  {
  }

  public abstract class lotSerNumVal : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerClassLotSerNumVal.lotSerNumVal>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INLotSerClassLotSerNumVal.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerClassLotSerNumVal.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerClassLotSerNumVal.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INLotSerClassLotSerNumVal.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerClassLotSerNumVal.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerClassLotSerNumVal.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    INLotSerClassLotSerNumVal.Tstamp>
  {
  }
}
