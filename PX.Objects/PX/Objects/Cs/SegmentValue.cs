// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.SegmentValue
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXPrimaryGraph(new Type[] {typeof (DimensionMaint)}, new Type[] {typeof (Select<Segment, Where<Segment.dimensionID, Equal<Current<SegmentValue.dimensionID>>, And<Segment.segmentID, Equal<Current<SegmentValue.segmentID>>>>>)})]
[PXCacheName("Segment Value")]
[Serializable]
public class SegmentValue : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IIncludable,
  IRestricted
{
  protected 
  #nullable disable
  string _DimensionID;
  protected short? _SegmentID;
  protected string _Value;
  protected string _Descr;
  protected bool? _Active;
  protected bool? _IsConsolidatedValue;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected string _MappedSegValue;
  protected byte[] _GroupMask;
  protected bool? _Included;

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (Segment.dimensionID))]
  [PXUIField]
  public virtual string DimensionID
  {
    get => this._DimensionID;
    set => this._DimensionID = value;
  }

  [PXDBShort(IsKey = true)]
  [PXDefault(typeof (Segment.segmentID))]
  [PXUIField]
  public virtual short? SegmentID
  {
    get => this._SegmentID;
    set => this._SegmentID = value;
  }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXParent(typeof (Select<Segment, Where<Segment.dimensionID, Equal<Current<SegmentValue.dimensionID>>, And<Segment.segmentID, Equal<Current<SegmentValue.segmentID>>>>>))]
  public virtual string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Aggregation")]
  public virtual bool? IsConsolidatedValue
  {
    get => this._IsConsolidatedValue;
    set => this._IsConsolidatedValue = value;
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

  [PXDBString(30, IsUnicode = true)]
  [PXUIField]
  public virtual string MappedSegValue
  {
    get => this._MappedSegValue;
    set => this._MappedSegValue = value;
  }

  [PXDBGroupMask]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  [PXUnboundDefault(false)]
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  public virtual bool? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  public abstract class dimensionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SegmentValue.dimensionID>
  {
  }

  public abstract class segmentID : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SegmentValue.segmentID>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SegmentValue.value>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SegmentValue.descr>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SegmentValue.active>
  {
  }

  public abstract class isConsolidatedValue : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SegmentValue.isConsolidatedValue>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SegmentValue.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SegmentValue.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SegmentValue.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SegmentValue.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SegmentValue.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SegmentValue.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SegmentValue.lastModifiedDateTime>
  {
  }

  public abstract class mappedSegValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SegmentValue.mappedSegValue>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SegmentValue.groupMask>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SegmentValue.included>
  {
  }
}
