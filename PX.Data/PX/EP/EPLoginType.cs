// Decompiled with JetBrains decompiler
// Type: PX.EP.EPLoginType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.EP;

/// <exclude />
[PXPrimaryGraph(typeof (EPLoginTypeMaint))]
[PXCacheName("Login Type")]
public class EPLoginType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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

  [PXDBIdentity(IsKey = true)]
  public virtual int? LoginTypeID { get; set; }

  [PXDBString(50, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "User Type", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (EPLoginType.loginTypeName))]
  public virtual string LoginTypeName { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("E", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Linked Entity", Visibility = PXUIVisibility.SelectorVisible)]
  [EPLoginType.entity.List]
  public virtual string Entity { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<EPLoginType.entity, Equal<EPLoginType.entity.contact>>, True>, False>))]
  public virtual bool? IsExternal { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Email as Username")]
  [PXUIEnabled(typeof (Where<EPLoginType.entity, Equal<EPLoginType.entity.contact>>))]
  [PXFormula(typeof (Switch<Case<Where<EPLoginType.entity, Equal<EPLoginType.entity.employee>>, False>, EPLoginType.emailAsLogin>))]
  public virtual bool? EmailAsLogin { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Reset Password on First Sign-In")]
  public virtual bool? ResetPasswordOnLogin { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Username Activation")]
  public virtual bool? RequireLoginActivation { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("R")]
  [PXUIField(DisplayName = "Allowed Login Type", Visibility = PXUIVisibility.SelectorVisible)]
  [EPLoginType.allowedLoginType.List]
  public virtual string AllowedLoginType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Allowed Concurrent Sign-Ins")]
  [PXDefault(3, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual int? AllowedSessions { get; set; }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Turn Off Two-Factor Authentication")]
  [PXUIEnabled(typeof (Where<EPLoginType.allowedLoginType, NotEqual<EPLoginType.allowedLoginType.api>>))]
  public virtual bool? DisableTwoFactorAuth { get; set; }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Allow Selection of This Type on Contacts Form")]
  public virtual bool? AllowThisTypeForContacts { get; set; }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Allow Selection of This Type on Employees Form")]
  public virtual bool? AllowThisTypeForEmployees { get; set; }

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

  public class PK : PrimaryKeyOf<EPLoginType>.By<EPLoginType.loginTypeID, EPLoginType.loginTypeName>
  {
    public static EPLoginType Find(
      PXGraph graph,
      int? loginTypeID,
      string loginTypeName,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<EPLoginType>.By<EPLoginType.loginTypeID, EPLoginType.loginTypeName>.FindBy(graph, (object) loginTypeID, (object) loginTypeName, options);
    }
  }

  public class UK : PrimaryKeyOf<EPLoginType>.By<EPLoginType.loginTypeID>
  {
    public static EPLoginType Find(PXGraph graph, int? loginTypeID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<EPLoginType>.By<EPLoginType.loginTypeID>.FindBy(graph, (object) loginTypeID, options);
    }
  }

  public abstract class loginTypeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPLoginType.loginTypeID>
  {
  }

  public abstract class loginTypeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPLoginType.loginTypeName>
  {
  }

  public abstract class entity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPLoginType.entity>
  {
    public const string Contact = "C";
    public const string Employee = "E";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "C", "E" }, new string[2]
        {
          "Contact",
          "Employee"
        })
      {
      }
    }

    public class contact : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPLoginType.entity.contact>
    {
      public contact()
        : base("C")
      {
      }
    }

    public class employee : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPLoginType.entity.employee>
    {
      public employee()
        : base("E")
      {
      }
    }
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPLoginType.description>
  {
  }

  public abstract class isExternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPLoginType.isExternal>
  {
  }

  public abstract class emailAsLogin : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPLoginType.emailAsLogin>
  {
  }

  public abstract class resetPasswordOnLogin : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPLoginType.resetPasswordOnLogin>
  {
  }

  public abstract class requireLoginActivation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPLoginType.requireLoginActivation>
  {
  }

  public abstract class allowedLoginType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPLoginType.allowedLoginType>
  {
    public const string UI = "U";
    public const string API = "A";
    public const string Unrestricted = "R";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "U", "A", "R" }, new string[3]
        {
          "UI",
          "API",
          "Unrestricted"
        })
      {
      }
    }

    public class ui : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPLoginType.allowedLoginType.ui>
    {
      public ui()
        : base("U")
      {
      }
    }

    public class api : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPLoginType.allowedLoginType.api>
    {
      public api()
        : base("A")
      {
      }
    }

    public class unrestricted : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EPLoginType.allowedLoginType.unrestricted>
    {
      public unrestricted()
        : base("R")
      {
      }
    }
  }

  public abstract class allowedSessions : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPLoginType.allowedSessions>
  {
  }

  public abstract class disableTwoFactorAuth : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPLoginType.disableTwoFactorAuth>
  {
  }

  public abstract class allowThisTypeForContacts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPLoginType.allowThisTypeForContacts>
  {
  }

  public abstract class allowThisTypeForEmployees : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPLoginType.allowThisTypeForEmployees>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPLoginType.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPLoginType.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPLoginType.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPLoginType.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPLoginType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPLoginType.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPLoginType.lastModifiedDateTime>
  {
  }
}
