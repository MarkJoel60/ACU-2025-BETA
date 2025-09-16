// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FADisposalMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("FA Disposal Method")]
[Serializable]
public class FADisposalMethod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _DisposalMethodID;
  protected 
  #nullable disable
  string _DisposalMethodCD;
  protected string _Description;
  protected int? _ProceedsAcctID;
  protected int? _ProceedsSubID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity]
  [PXUIField]
  public virtual int? DisposalMethodID
  {
    get => this._DisposalMethodID;
    set => this._DisposalMethodID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCC")]
  [PXUIField]
  public virtual string DisposalMethodCD
  {
    get => this._DisposalMethodCD;
    set => this._DisposalMethodCD = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [Account(null, DisplayName = "Proceeds Account", DescriptionField = typeof (Account.description))]
  [PXDefault(typeof (FASetup.proceedsAcctID))]
  public virtual int? ProceedsAcctID
  {
    get => this._ProceedsAcctID;
    set => this._ProceedsAcctID = value;
  }

  [SubAccount(typeof (FADisposalMethod.proceedsAcctID), DescriptionField = typeof (Sub.description), DisplayName = "Proceeds Subaccount")]
  [PXDefault(typeof (FASetup.proceedsSubID))]
  public virtual int? ProceedsSubID
  {
    get => this._ProceedsSubID;
    set => this._ProceedsSubID = value;
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

  public class PK : PrimaryKeyOf<FADisposalMethod>.By<FADisposalMethod.disposalMethodCD>
  {
    public static FADisposalMethod Find(
      PXGraph graph,
      string disposalMethodCD,
      PKFindOptions options = 0)
    {
      return (FADisposalMethod) PrimaryKeyOf<FADisposalMethod>.By<FADisposalMethod.disposalMethodCD>.FindBy(graph, (object) disposalMethodCD, options);
    }
  }

  public class UK : PrimaryKeyOf<FADisposalMethod>.By<FADisposalMethod.disposalMethodCD>
  {
    public static FADisposalMethod Find(
      PXGraph graph,
      string disposalMethodCD,
      PKFindOptions options = 0)
    {
      return (FADisposalMethod) PrimaryKeyOf<FADisposalMethod>.By<FADisposalMethod.disposalMethodCD>.FindBy(graph, (object) disposalMethodCD, options);
    }
  }

  public static class FK
  {
    public class ProceedsAccount : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<FADisposalMethod>.By<FADisposalMethod.proceedsAcctID>
    {
    }

    public class ProceedsSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<FADisposalMethod>.By<FADisposalMethod.proceedsSubID>
    {
    }
  }

  public abstract class disposalMethodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FADisposalMethod.disposalMethodID>
  {
  }

  public abstract class disposalMethodCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADisposalMethod.disposalMethodCD>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADisposalMethod.description>
  {
  }

  public abstract class proceedsAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADisposalMethod.proceedsAcctID>
  {
  }

  public abstract class proceedsSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADisposalMethod.proceedsSubID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FADisposalMethod.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FADisposalMethod.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADisposalMethod.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADisposalMethod.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FADisposalMethod.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADisposalMethod.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADisposalMethod.lastModifiedDateTime>
  {
  }
}
