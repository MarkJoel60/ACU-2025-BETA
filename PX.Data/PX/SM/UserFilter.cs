// Decompiled with JetBrains decompiler
// Type: PX.SM.UserFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Net;

#nullable enable
namespace PX.SM;

[Serializable]
public class UserFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Username;
  protected Guid? _PKID;
  protected string _StartIPAddress;
  protected string _EndIPAddress;

  [PXDBString(256 /*0x0100*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (Users.username))]
  [UserFilter.ParentUser]
  [PXUIField(DisplayName = "Login", Visibility = PXUIVisibility.Invisible)]
  public virtual string Username
  {
    get => this._Username;
    set => this._Username = value;
  }

  [PXDBGuid(true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual Guid? PKID
  {
    get => this._PKID;
    set => this._PKID = value;
  }

  [PXDBString(30, InputMask = "###.###.###.###")]
  [PXUIField(DisplayName = "Start IP Address")]
  public virtual string StartIPAddress
  {
    get => this._StartIPAddress;
    set => this._StartIPAddress = value;
  }

  [PXDBString(30, InputMask = "###.###.###.###")]
  [PXUIField(DisplayName = "End IP Address")]
  public virtual string EndIPAddress
  {
    get => this._EndIPAddress;
    set => this._EndIPAddress = value;
  }

  public static IPAddress DecodeIpAddress(string v)
  {
    if (string.IsNullOrEmpty(v))
      return (IPAddress) null;
    if ((v.IndexOf('.') >= 0 ? 0 : (v.Length == 12 ? 1 : 0)) != 0)
      v = $"{v.Substring(0, 3)}.{v.Substring(3, 3)}.{v.Substring(6, 3)}.{v.Substring(9, 3)}";
    IPAddress ipAddress;
    if (Net_Utils.TryParseIPAddress(v, ref ipAddress))
      return ipAddress;
    if (v.IndexOf('.') > 0)
    {
      string[] strArray = v.Split('.');
      byte[] address = new byte[strArray.Length];
      for (int index = 0; index < strArray.Length; ++index)
      {
        byte result;
        if (!byte.TryParse(strArray[index], out result))
          return (IPAddress) null;
        address[index] = result;
      }
      try
      {
        return new IPAddress(address);
      }
      catch
      {
      }
    }
    return (IPAddress) null;
  }

  private class ParentUserAttribute : PXParentAttribute
  {
    public ParentUserAttribute()
      : base(typeof (Select<Users, Where<Users.username, Equal<Current<UserFilter.username>>>>))
    {
    }
  }

  public abstract class username : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UserFilter.username>
  {
  }

  public abstract class pKID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UserFilter.pKID>
  {
  }

  public abstract class startIPAddress : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UserFilter.startIPAddress>
  {
  }

  public abstract class endIPAddress : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UserFilter.endIPAddress>
  {
  }
}
