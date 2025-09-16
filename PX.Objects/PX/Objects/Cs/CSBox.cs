// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSBox
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName]
[Serializable]
public class CSBox : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _BoxID;
  protected Decimal? _MaxWeight;
  protected Decimal? _BoxWeight;
  protected Decimal? _MaxVolume;
  protected Decimal? _Length;
  protected Decimal? _Width;
  protected Decimal? _Height;
  protected string _Description;
  protected bool? _ActiveByDefault;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  public virtual string BoxID
  {
    get => this._BoxID;
    set => this._BoxID = value;
  }

  [PXDBDecimal(4, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? MaxWeight
  {
    get => this._MaxWeight;
    set => this._MaxWeight = value;
  }

  [PXDBDecimal(4, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? BoxWeight
  {
    get => this._BoxWeight;
    set => this._BoxWeight = value;
  }

  [PXDBDecimal(4, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? MaxVolume
  {
    get => this._MaxVolume;
    set => this._MaxVolume = value;
  }

  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? Length
  {
    get => this._Length;
    set => this._Length = value;
  }

  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? Width
  {
    get => this._Width;
    set => this._Width = value;
  }

  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? Height
  {
    get => this._Height;
    set => this._Height = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>
  /// A Boolean value that specifies whether the <see cref="P:PX.Objects.CS.CSBox.Width" />, <see cref="P:PX.Objects.CS.CSBox.Height" />, and <see cref="P:PX.Objects.CS.CSBox.Length" />
  /// dimension values of a box can be overridden when the box is selected in a package.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Editable Dimensions")]
  public virtual bool? AllowOverrideDimension { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active by Default")]
  public virtual bool? ActiveByDefault
  {
    get => this._ActiveByDefault;
    set => this._ActiveByDefault = value;
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

  public class PK : PrimaryKeyOf<CSBox>.By<CSBox.boxID>
  {
    public static CSBox Find(PXGraph graph, string boxID, PKFindOptions options = 0)
    {
      return (CSBox) PrimaryKeyOf<CSBox>.By<CSBox.boxID>.FindBy(graph, (object) boxID, options);
    }
  }

  public abstract class boxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSBox.boxID>
  {
  }

  public abstract class maxWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CSBox.maxWeight>
  {
  }

  public abstract class boxWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CSBox.boxWeight>
  {
  }

  public abstract class maxVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CSBox.maxVolume>
  {
  }

  public abstract class length : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CSBox.length>
  {
  }

  public abstract class width : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CSBox.width>
  {
  }

  public abstract class height : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CSBox.height>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSBox.description>
  {
  }

  public abstract class allowOverrideDimension : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CSBox.allowOverrideDimension>
  {
  }

  public abstract class activeByDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSBox.activeByDefault>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CSBox.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CSBox.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSBox.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CSBox.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CSBox.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSBox.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CSBox.lastModifiedDateTime>
  {
  }
}
