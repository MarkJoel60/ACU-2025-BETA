// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TXImportState
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
namespace PX.Objects.TX;

[Serializable]
public class TXImportState : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _StateCode;
  protected string _StateName;
  protected int? _AccountID;
  protected int? _SubID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [Obsolete]
  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  public virtual string StateCode
  {
    get => this._StateCode;
    set => this._StateCode = value;
  }

  [Obsolete]
  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  public virtual string StateName
  {
    get => this._StateName;
    set => this._StateName = value;
  }

  [Obsolete]
  [Account(DisplayName = "Tax Payable Account")]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [Obsolete]
  [PXDefault]
  [SubAccount(typeof (TXImportState.accountID), DisplayName = "Tax Payable Subaccount")]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
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

  public class PK : PrimaryKeyOf<TXImportState>.By<TXImportState.stateCode>
  {
    public static TXImportState Find(PXGraph graph, string stateCode, PKFindOptions options = 0)
    {
      return (TXImportState) PrimaryKeyOf<TXImportState>.By<TXImportState.stateCode>.FindBy(graph, (object) stateCode, options);
    }
  }

  public static class FK
  {
    public class TaxPayableAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<TXImportState>.By<TXImportState.accountID>
    {
    }

    public class TaxPayableSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<TXImportState>.By<TXImportState.subID>
    {
    }
  }

  [Obsolete]
  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TXImportState.selected>
  {
  }

  public abstract class stateCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXImportState.stateCode>
  {
  }

  [Obsolete]
  public abstract class stateName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXImportState.stateName>
  {
  }

  [Obsolete]
  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TXImportState.accountID>
  {
  }

  [Obsolete]
  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TXImportState.subID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TXImportState.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportState.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportState.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TXImportState.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportState.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportState.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TXImportState.Tstamp>
  {
  }
}
