// Decompiled with JetBrains decompiler
// Type: PX.SM.AuthenticationType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
public class AuthenticationType
{
  public const int None = 0;
  public const int Main = 1;
  public const int Custom = 2;
  [Obsolete("Use OAuth2Old instead.")]
  public const int OAuth2 = 3;
  public const int OAuth2Old = 3;
  public const int PlugIn = 4;
  public const int OAuth2New = 5;

  public class ListAttribute : PXIntListAttribute
  {
    public ListAttribute()
      : base(new int[6]{ 0, 1, 2, 3, 5, 4 }, new string[6]
      {
        "None",
        "Use Main Login",
        "Use Custom Settings",
        "OAuth2",
        "OAuth2",
        "Plug-In"
      })
    {
    }
  }

  [Obsolete]
  public class MethodListAttribute : PXIntListAttribute
  {
    public MethodListAttribute()
      : base(new int[2]{ 0, 3 }, new string[2]
      {
        "Basic Authentication",
        "OAuth2"
      })
    {
    }
  }

  public class none : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  AuthenticationType.none>
  {
    public none()
      : base(0)
    {
    }
  }

  public class main : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AuthenticationType.main>
  {
    public main()
      : base(1)
    {
    }
  }

  public class custom : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AuthenticationType.custom>
  {
    public custom()
      : base(2)
    {
    }
  }

  [Obsolete("Use OAuth2Old instead.")]
  public class oAuth2 : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AuthenticationType.oAuth2>
  {
    public oAuth2()
      : base(3)
    {
    }
  }

  public class oAuth2Old : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AuthenticationType.oAuth2Old>
  {
    public oAuth2Old()
      : base(3)
    {
    }
  }

  public class oAuth2New : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AuthenticationType.oAuth2New>
  {
    public oAuth2New()
      : base(5)
    {
    }
  }

  public class plugIn : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AuthenticationType.plugIn>
  {
    public plugIn()
      : base(4)
    {
    }
  }
}
