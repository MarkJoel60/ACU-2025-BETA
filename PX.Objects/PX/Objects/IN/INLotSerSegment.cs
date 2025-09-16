// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerSegment
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

[PXCacheName("Lot/Serial Segment")]
[Serializable]
public class INLotSerSegment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _LotSerClassID;
  protected short? _SegmentID;
  protected string _SegmentType;
  protected string _SegmentValue;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (INLotSerClass.lotSerClassID))]
  [PXParent(typeof (INLotSerSegment.FK.LotSerialClass))]
  public virtual string LotSerClassID
  {
    get => this._LotSerClassID;
    set => this._LotSerClassID = value;
  }

  [PXDBShort(IsKey = true)]
  [PXUIField(DisplayName = "Segment Number", Enabled = false)]
  [PXLineNbr(typeof (INLotSerClass))]
  [PXDefault]
  public virtual short? SegmentID
  {
    get => this._SegmentID;
    set => this._SegmentID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  [INLotSerSegmentType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual string SegmentType
  {
    get => this._SegmentType;
    set => this._SegmentType = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public virtual string SegmentValue
  {
    get => this._SegmentValue;
    set => this._SegmentValue = value;
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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INLotSerSegment>.By<INLotSerSegment.lotSerClassID, INLotSerSegment.segmentID>
  {
    public static INLotSerSegment Find(
      PXGraph graph,
      string lotSerClassID,
      long? segmentID,
      PKFindOptions options = 0)
    {
      return (INLotSerSegment) PrimaryKeyOf<INLotSerSegment>.By<INLotSerSegment.lotSerClassID, INLotSerSegment.segmentID>.FindBy(graph, (object) lotSerClassID, (object) segmentID, options);
    }
  }

  public static class FK
  {
    public class LotSerialClass : 
      PrimaryKeyOf<INLotSerClass>.By<INLotSerClass.lotSerClassID>.ForeignKeyOf<INLotSerSegment>.By<INLotSerSegment.lotSerClassID>
    {
    }
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerSegment.lotSerClassID>
  {
  }

  public abstract class segmentID : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INLotSerSegment.segmentID>
  {
  }

  public abstract class segmentType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INLotSerSegment.segmentType>
  {
  }

  public abstract class segmentValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerSegment.segmentValue>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INLotSerSegment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerSegment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerSegment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INLotSerSegment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerSegment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerSegment.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INLotSerSegment.Tstamp>
  {
  }
}
