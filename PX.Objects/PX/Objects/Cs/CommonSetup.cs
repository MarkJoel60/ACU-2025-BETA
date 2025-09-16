// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CommonSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Common Setup")]
[Serializable]
public class CommonSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected short? _DecPlPrcCst;
  protected short? _DecPlQty;
  protected 
  #nullable disable
  string _WeightUOM;
  protected string _VolumeUOM;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBShort(MinValue = 0, MaxValue = 6)]
  [PXUIField(DisplayName = "Price/Cost Decimal Places")]
  [PXDefault(4)]
  public virtual short? DecPlPrcCst
  {
    get => this._DecPlPrcCst;
    set => this._DecPlPrcCst = value;
  }

  [PXDBShort(MinValue = 0, MaxValue = 6)]
  [PXUIField(DisplayName = "Quantity Decimal Places")]
  [PXDefault(2)]
  public virtual short? DecPlQty
  {
    get => this._DecPlQty;
    set => this._DecPlQty = value;
  }

  [INUnit(DisplayName = "Weight UOM")]
  [PXDefault]
  public virtual string WeightUOM
  {
    get => this._WeightUOM;
    set => this._WeightUOM = value;
  }

  [INUnit(DisplayName = "Linear UOM")]
  public virtual string LinearUOM { get; set; }

  [PXDefault]
  [INUnit(DisplayName = "Volume UOM")]
  public virtual string VolumeUOM
  {
    get => this._VolumeUOM;
    set => this._VolumeUOM = value;
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

  public abstract class decPlPrcCst : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CommonSetup.decPlPrcCst>
  {
    public const short Default = 4;
  }

  public abstract class decPlQty : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CommonSetup.decPlQty>
  {
    public const short Default = 2;
  }

  public abstract class weightUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CommonSetup.weightUOM>
  {
  }

  public abstract class linearUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CommonSetup.linearUOM>
  {
  }

  public abstract class volumeUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CommonSetup.volumeUOM>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CommonSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CommonSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CommonSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CommonSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CommonSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CommonSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CommonSetup.lastModifiedDateTime>
  {
  }
}
