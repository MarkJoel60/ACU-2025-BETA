// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ReasonCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXPrimaryGraph(typeof (ReasonCodeMaint))]
[PXCacheName]
[Serializable]
public class ReasonCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ReasonCodeID;
  protected string _Descr;
  protected int? _AccountID;
  protected int? _SubID;
  protected string _Usage;
  protected string _SubMask;
  protected int? _SalesAcctID;
  protected int? _SalesSubID;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDefault]
  [PXDBString(20, IsKey = true, InputMask = ">aaaaaaaaaaaaaaaaaaaa")]
  [PXUIField]
  [PXSelector(typeof (Search<ReasonCode.reasonCodeID>))]
  [PXReferentialIntegrityCheck]
  public virtual string ReasonCodeID
  {
    get => this._ReasonCodeID;
    set => this._ReasonCodeID = value;
  }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [Account]
  [PXDefault]
  [PXForeignReference(typeof (Field<ReasonCode.accountID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(typeof (ReasonCode.accountID))]
  [PXDefault]
  [PXForeignReference(typeof (Field<ReasonCode.subID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [ReasonCodeUsages.List]
  [PXDefault]
  public virtual string Usage
  {
    get => this._Usage;
    set => this._Usage = value;
  }

  [PXDefault]
  [PXDBString(IsUnicode = false, InputMask = "")]
  [PXUIField(DisplayName = "Combine Sub from Combined", Visible = false)]
  public virtual string SubMask
  {
    get => this._SubMask;
    set => this._SubMask = value;
  }

  /// <exclude />
  [PXDefault]
  [PXString(30, IsUnicode = true, InputMask = "")]
  [ReasonCodeSubAccountMask(DisplayName = "Combine Sub from")]
  public virtual string SubMaskInventory
  {
    get => this._SubMask;
    set => this._SubMask = value;
  }

  /// <exclude />
  [PXDefault]
  [PXString(30, IsUnicode = true, InputMask = "")]
  [ReasonCodeSubAccountMaskNoProj(DisplayName = "Combine Sub from")]
  public virtual string SubMaskInventoryShort
  {
    get => this._SubMask;
    set => this._SubMask = value;
  }

  /// <exclude />
  [PXDefault]
  [PXString(30, IsUnicode = true, InputMask = "")]
  [ReasonCodeSubAccountMask(DisplayName = "Combine Sub from")]
  public virtual string SubMaskFinance
  {
    get => this._SubMask;
    set => this._SubMask = value;
  }

  [Account]
  [PXForeignReference(typeof (Field<ReasonCode.salesAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? SalesAcctID
  {
    get => this._SalesAcctID;
    set => this._SalesAcctID = value;
  }

  [SubAccount(typeof (ReasonCode.salesAcctID))]
  [PXForeignReference(typeof (Field<ReasonCode.salesSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? SalesSubID
  {
    get => this._SalesSubID;
    set => this._SalesSubID = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
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

  public class PK : PrimaryKeyOf<ReasonCode>.By<ReasonCode.reasonCodeID>
  {
    public static ReasonCode Find(PXGraph graph, string reasonCodeID, PKFindOptions options = 0)
    {
      return (ReasonCode) PrimaryKeyOf<ReasonCode>.By<ReasonCode.reasonCodeID>.FindBy(graph, (object) reasonCodeID, options);
    }
  }

  public static class FK
  {
    public class Sub : PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ReasonCode>.By<ReasonCode.subID>
    {
    }

    public class SalesSub : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ReasonCode>.By<ReasonCode.salesSubID>
    {
    }
  }

  public abstract class reasonCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReasonCode.reasonCodeID>
  {
    public const int Length = 20;
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReasonCode.descr>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReasonCode.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReasonCode.subID>
  {
  }

  public abstract class usage : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReasonCode.usage>
  {
  }

  public abstract class subMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReasonCode.subMask>
  {
  }

  public abstract class subMaskInventory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReasonCode.subMaskInventory>
  {
  }

  public abstract class subMaskInventoryShort : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReasonCode.subMaskInventoryShort>
  {
  }

  public abstract class subMaskFinance : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReasonCode.subMaskFinance>
  {
  }

  public abstract class salesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReasonCode.salesAcctID>
  {
  }

  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReasonCode.salesSubID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ReasonCode.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ReasonCode.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReasonCode.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ReasonCode.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ReasonCode.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReasonCode.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ReasonCode.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ReasonCode.Tstamp>
  {
  }
}
