// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.TranslDefDet
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
namespace PX.Objects.CM;

/// <summary>
/// Represents details of the currency translation definitions.
/// Each record of this type specifies the way translation is performed for a particular range of accounts and subaccounts
/// (in the scope of the corresponding master record of the <see cref="T:PX.Objects.CM.TranslDef" /> type).
/// </summary>
[PXCacheName("Translation Definition Detail")]
[Serializable]
public class TranslDefDet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TranslDefId;
  protected int? _LineNbr;
  protected int? _AccountIdFrom;
  protected int? _SubIdFrom;
  protected int? _AccountIdTo;
  protected int? _SubIdTo;
  protected short? _CalcMode;
  protected string _RateTypeId;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (TranslDef))]
  [PXParent(typeof (Select<TranslDef, Where<TranslDef.translDefId, Equal<Current<TranslDefDet.translDefId>>>>))]
  public virtual string TranslDefId
  {
    get => this._TranslDefId;
    set => this._TranslDefId = value;
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (TranslDef.lineCntr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [Account(DisplayName = "Account From", DescriptionField = typeof (PX.Objects.GL.Account.description), Required = true)]
  public virtual int? AccountIdFrom
  {
    get => this._AccountIdFrom;
    set => this._AccountIdFrom = value;
  }

  [SubAccount(typeof (TranslDefDet.accountIdFrom), DisplayName = "Subaccount From")]
  public virtual int? SubIdFrom
  {
    get => this._SubIdFrom;
    set => this._SubIdFrom = value;
  }

  [Account(DisplayName = "Account To", DescriptionField = typeof (PX.Objects.GL.Account.description), Required = true)]
  public virtual int? AccountIdTo
  {
    get => this._AccountIdTo;
    set => this._AccountIdTo = value;
  }

  [SubAccount(typeof (TranslDefDet.accountIdTo), DisplayName = "Subaccount To")]
  public virtual int? SubIdTo
  {
    get => this._SubIdTo;
    set => this._SubIdTo = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Translation Method", Required = true)]
  [PXDefault(1)]
  [PXIntList("1;YTD Balance,2;PTD Balance")]
  public virtual short? CalcMode
  {
    get => this._CalcMode;
    set => this._CalcMode = value;
  }

  [PXDBString(6, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Rate Type", Required = true)]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID))]
  public virtual string RateTypeId
  {
    get => this._RateTypeId;
    set => this._RateTypeId = value;
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

  public static string GetAcct(PXGraph graph, int accId)
  {
    return PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) accId
    })).AccountCD;
  }

  public static string GetSub(PXGraph graph, int subId)
  {
    return PXResultset<Sub>.op_Implicit(PXSelectBase<Sub, PXSelect<Sub, Where<Sub.subID, Equal<Required<Sub.subID>>>>.Config>.Select(graph, new object[1]
    {
      (object) subId
    })).SubCD;
  }

  public class PK : 
    PrimaryKeyOf<TranslDefDet>.By<TranslDefDet.translDefId, TranslDefDet.lineNbr, TranslDefDet.accountIdFrom>
  {
    public static TranslDefDet Find(
      PXGraph graph,
      string translDefId,
      int? lineNbr,
      int? accountIdFrom,
      PKFindOptions options = 0)
    {
      return (TranslDefDet) PrimaryKeyOf<TranslDefDet>.By<TranslDefDet.translDefId, TranslDefDet.lineNbr, TranslDefDet.accountIdFrom>.FindBy(graph, (object) translDefId, (object) lineNbr, (object) accountIdFrom, options);
    }
  }

  public static class FK
  {
    public class Translation : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<TranslDefDet>.By<TranslDefDet.translDefId>
    {
    }

    public class AccountFrom : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<TranslDefDet>.By<TranslDefDet.accountIdFrom>
    {
    }

    public class SubaccountFrom : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<TranslDefDet>.By<TranslDefDet.subIdFrom>
    {
    }

    public class AccountTo : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<TranslDefDet>.By<TranslDefDet.accountIdTo>
    {
    }

    public class SubaccountTo : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<TranslDefDet>.By<TranslDefDet.subIdTo>
    {
    }

    public class RateType : 
      PrimaryKeyOf<CurrencyRateType>.By<CurrencyRateType.curyRateTypeID>.ForeignKeyOf<TranslDefDet>.By<TranslDefDet.rateTypeId>
    {
    }
  }

  public abstract class translDefId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TranslDefDet.translDefId>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslDefDet.lineNbr>
  {
  }

  public abstract class accountIdFrom : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslDefDet.accountIdFrom>
  {
  }

  public abstract class subIdFrom : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslDefDet.subIdFrom>
  {
  }

  public abstract class accountIdTo : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslDefDet.accountIdTo>
  {
  }

  public abstract class subIdTo : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslDefDet.subIdTo>
  {
  }

  public abstract class calcMode : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  TranslDefDet.calcMode>
  {
  }

  public abstract class rateTypeId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TranslDefDet.rateTypeId>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TranslDefDet.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TranslDefDet.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TranslDefDet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslDefDet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TranslDefDet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TranslDefDet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslDefDet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TranslDefDet.lastModifiedDateTime>
  {
  }
}
