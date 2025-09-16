// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABonus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXPrimaryGraph(typeof (BonusMaint))]
[PXCacheName("Bonus")]
[Serializable]
public class FABonus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BonusID;
  protected 
  #nullable disable
  string _BonusCD;
  protected string _State;
  protected string _Description;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity]
  [PXUIField]
  [PXReferentialIntegrityCheck]
  public virtual int? BonusID
  {
    get => this._BonusID;
    set => this._BonusID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXUIField]
  [PXDefault]
  [PXSelector(typeof (Search<FABonus.bonusCD>))]
  [PXFieldDescription]
  public virtual string BonusCD
  {
    get => this._BonusCD;
    set => this._BonusCD = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  public virtual string State
  {
    get => this._State;
    set => this._State = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXNote(DescriptionField = typeof (FABonus.bonusCD))]
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

  public class PK : PrimaryKeyOf<FABonus>.By<FABonus.bonusID>
  {
    public static FABonus Find(PXGraph graph, int? bonusID, PKFindOptions options = 0)
    {
      return (FABonus) PrimaryKeyOf<FABonus>.By<FABonus.bonusID>.FindBy(graph, (object) bonusID, options);
    }
  }

  public abstract class bonusID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABonus.bonusID>
  {
  }

  public abstract class bonusCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABonus.bonusCD>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABonus.state>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABonus.description>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABonus.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FABonus.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABonus.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABonus.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABonus.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABonus.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABonus.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABonus.lastModifiedDateTime>
  {
  }
}
