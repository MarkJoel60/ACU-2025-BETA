// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEquipmentRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXCacheName("Equipment Rate")]
[Serializable]
public class EPEquipmentRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _EquipmentID;
  protected int? _ProjectID;
  protected Decimal? _RunRate;
  protected Decimal? _SetupRate;
  protected Decimal? _SuspendRate;
  protected bool? _IsActive;
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

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (EPEquipment.equipmentID))]
  [PXParent(typeof (Select<EPEquipment, Where<EPEquipment.equipmentID, Equal<Current<EPEquipmentRate.equipmentID>>>>))]
  public virtual int? EquipmentID
  {
    get => this._EquipmentID;
    set => this._EquipmentID = value;
  }

  [PXDefault]
  [Project(IsKey = true)]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDefault(typeof (Search<EPEquipment.runRate, Where<EPEquipment.equipmentID, Equal<Current<EPEquipmentRate.equipmentID>>>>))]
  [PXDBPriceCost]
  [PXUIField(DisplayName = "Run Rate")]
  public virtual Decimal? RunRate
  {
    get => this._RunRate;
    set => this._RunRate = value;
  }

  [PXDefault(typeof (Search<EPEquipment.setupRate, Where<EPEquipment.equipmentID, Equal<Current<EPEquipmentRate.equipmentID>>>>))]
  [PXDBPriceCost]
  [PXUIField(DisplayName = "Setup Rate")]
  public virtual Decimal? SetupRate
  {
    get => this._SetupRate;
    set => this._SetupRate = value;
  }

  [PXDefault(typeof (Search<EPEquipment.suspendRate, Where<EPEquipment.equipmentID, Equal<Current<EPEquipmentRate.equipmentID>>>>))]
  [PXDBPriceCost]
  [PXUIField(DisplayName = "Suspend Rate")]
  public virtual Decimal? SuspendRate
  {
    get => this._SuspendRate;
    set => this._SuspendRate = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
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

  public abstract class equipmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentRate.equipmentID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentRate.projectID>
  {
  }

  public abstract class runRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPEquipmentRate.runRate>
  {
  }

  public abstract class setupRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPEquipmentRate.setupRate>
  {
  }

  public abstract class suspendRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPEquipmentRate.suspendRate>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEquipmentRate.isActive>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEquipmentRate.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPEquipmentRate.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEquipmentRate.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentRate.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEquipmentRate.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPEquipmentRate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentRate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEquipmentRate.lastModifiedDateTime>
  {
  }
}
