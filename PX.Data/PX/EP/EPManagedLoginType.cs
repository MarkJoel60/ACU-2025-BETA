// Decompiled with JetBrains decompiler
// Type: PX.EP.EPManagedLoginType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.EP;

/// <exclude />
[PXCacheName("Login Type Managed")]
[Serializable]
public class EPManagedLoginType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<EPLoginType, Where<EPLoginType.loginTypeID, Equal<Current<EPManagedLoginType.loginTypeID>>>>))]
  [PXSelector(typeof (Search<EPLoginType.loginTypeID, Where<EPLoginType.entity, Equal<EPLoginType.entity.contact>>>), DescriptionField = typeof (EPLoginType.loginTypeName), SubstituteKey = typeof (EPLoginType.loginTypeName))]
  [PXUIField(DisplayName = "User Type")]
  public virtual int? LoginTypeID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDBChildIdentity(typeof (EPLoginType.loginTypeID))]
  [PXParent(typeof (Select<EPLoginType, Where<EPLoginType.loginTypeID, Equal<Current<EPManagedLoginType.parentLoginTypeID>>>>))]
  [PXDefault(typeof (EPLoginType.loginTypeID))]
  [PXUIField(DisplayName = "Parent User Type")]
  public virtual int? ParentLoginTypeID { get; set; }

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
  public virtual System.DateTime? CreatedDateTime
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
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class loginTypeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPManagedLoginType.loginTypeID>
  {
  }

  public abstract class parentLoginTypeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPManagedLoginType.parentLoginTypeID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPManagedLoginType.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPManagedLoginType.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPManagedLoginType.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPManagedLoginType.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPManagedLoginType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPManagedLoginType.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPManagedLoginType.lastModifiedDateTime>
  {
  }
}
