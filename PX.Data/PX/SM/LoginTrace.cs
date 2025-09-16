// Decompiled with JetBrains decompiler
// Type: PX.SM.LoginTrace
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

[PXCacheName("Login Trace")]
public class LoginTrace : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _LoginTraceID;
  protected 
  #nullable disable
  string _ApplicationName;
  protected string _Host;
  protected System.DateTime? _Date;
  protected string _Username;
  protected int? _Operation;
  protected string _IPAddress;
  protected string _ScreenID;
  protected string _Comment;
  internal const int CommentMaxLength = 1000;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? LoginTraceID
  {
    get => this._LoginTraceID;
    set => this._LoginTraceID = value;
  }

  [PXDBString(32 /*0x20*/)]
  [PXDefault]
  public virtual string ApplicationName
  {
    get => this._ApplicationName;
    set => this._ApplicationName = value;
  }

  [PXUIField(DisplayName = "Host", Enabled = false)]
  [PXDBString(255 /*0xFF*/)]
  [PXDefault("")]
  public virtual string Host
  {
    get => this._Host;
    set => this._Host = value;
  }

  [PXDBDate(InputMask = "g", PreserveTime = true, UseTimeZone = true)]
  [PXUIField(DisplayName = "Date")]
  public virtual System.DateTime? Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Username")]
  [PXSelector(typeof (Users.username), new System.Type[] {typeof (Users.username)}, DescriptionField = typeof (Users.displayName))]
  public virtual string Username
  {
    get => this._Username;
    set => this._Username = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Operation")]
  [PXAuditJournal.OperationList]
  public virtual int? Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBString(50)]
  [PXUIField(DisplayName = "IP address")]
  [PXPersonalDataField]
  public virtual string IPAddress
  {
    get => this._IPAddress;
    set => this._IPAddress = value;
  }

  [PXDBString(11, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Screen ID")]
  [PXSelector(typeof (SiteMap.screenID), DescriptionField = typeof (SiteMap.title))]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBString(1000)]
  [PXUIField(DisplayName = "Comment")]
  public virtual string Comment
  {
    get => this._Comment;
    set => this._Comment = value;
  }

  public class PK : PrimaryKeyOf<LoginTrace>.By<LoginTrace.loginTraceID>
  {
    public static LoginTrace Find(PXGraph graph, int? loginTraceID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<LoginTrace>.By<LoginTrace.loginTraceID>.FindBy(graph, (object) loginTraceID, options);
    }
  }

  public static class FK
  {
    public class User : 
      PrimaryKeyOf<Users>.By<Users.username>.ForeignKeyOf<LoginTrace>.By<LoginTrace.username>
    {
    }

    public class SiteMap : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.screenID>.ForeignKeyOf<LoginTrace>.By<LoginTrace.screenID>
    {
    }

    public class PortalMap : 
      PrimaryKeyOf<PortalMap>.By<PortalMap.screenID>.ForeignKeyOf<LoginTrace>.By<LoginTrace.screenID>
    {
    }
  }

  public abstract class loginTraceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LoginTrace.loginTraceID>
  {
  }

  public abstract class applicationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LoginTrace.applicationName>
  {
  }

  public abstract class host : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LoginTrace.host>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  LoginTrace.date>
  {
  }

  public abstract class username : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LoginTrace.username>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LoginTrace.operation>
  {
  }

  public abstract class iPAddress : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LoginTrace.iPAddress>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LoginTrace.screenID>
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LoginTrace.comment>
  {
  }
}
