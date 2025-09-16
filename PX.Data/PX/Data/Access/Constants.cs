// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.Constants
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.Access;

public static class Constants
{
  public const 
  #nullable disable
  string RootApplicationName = "/";
  public const string UniversalRole = "*";

  public class universalRole : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  Constants.universalRole>
  {
    public universalRole()
      : base("*")
    {
    }
  }

  public class grantedAccessRights : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  Constants.grantedAccessRights>
  {
    public grantedAccessRights()
      : base(4)
    {
    }
  }

  public class revokedAccessRights : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  Constants.revokedAccessRights>
  {
    public revokedAccessRights()
      : base(0)
    {
    }
  }
}
