// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDunningSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>Header of Remainder notification message</summary>
[PXCacheName("AR Dunning Setup")]
[Serializable]
public class ARDunningSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _DunningLetterLevel;
  protected int? _DueDays;
  protected int? _DaysToSettle;
  protected 
  #nullable disable
  string _Descr;
  protected Decimal? _DunningFee;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Dunning Letter Level", Enabled = false)]
  [PXParent(typeof (Select<ARSetup>))]
  public virtual int? DunningLetterLevel
  {
    get => this._DunningLetterLevel;
    set => this._DunningLetterLevel = value;
  }

  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Days Past Due")]
  public virtual int? DueDays
  {
    get => this._DueDays;
    set => this._DueDays = value;
  }

  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(3)]
  [PXUIField(DisplayName = "Days to Settle")]
  public virtual int? DaysToSettle
  {
    get => this._DaysToSettle;
    set => this._DaysToSettle = value;
  }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description")]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? DunningFee
  {
    get => this._DunningFee;
    set => this._DunningFee = value;
  }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

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

  public class PK : PrimaryKeyOf<ARDunningSetup>.By<ARDunningSetup.dunningLetterLevel>
  {
    public static ARDunningSetup Find(
      PXGraph graph,
      int? dunningLetterLevel,
      PKFindOptions options = 0)
    {
      return (ARDunningSetup) PrimaryKeyOf<ARDunningSetup>.By<ARDunningSetup.dunningLetterLevel>.FindBy(graph, (object) dunningLetterLevel, options);
    }
  }

  public abstract class dunningLetterLevel : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDunningSetup.dunningLetterLevel>
  {
  }

  public abstract class dueDays : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARDunningSetup.dueDays>
  {
  }

  public abstract class daysToSettle : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARDunningSetup.daysToSettle>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDunningSetup.descr>
  {
  }

  public abstract class dunningFee : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARDunningSetup.dunningFee>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARDunningSetup.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARDunningSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARDunningSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDunningSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARDunningSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARDunningSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDunningSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARDunningSetup.lastModifiedDateTime>
  {
  }
}
