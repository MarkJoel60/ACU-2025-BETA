// Decompiled with JetBrains decompiler
// Type: PX.SM.EmailAccountTypesAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class EmailAccountTypesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Standard = "S";
  public const string Exchange = "E";
  public const string Plugin = "P";

  public EmailAccountTypesAttribute()
    : base(("S", nameof (Standard)), ("E", nameof (Exchange)), ("P", "Email Service Plug-In"))
  {
  }

  public class standard : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EmailAccountTypesAttribute.standard>
  {
    public standard()
      : base("S")
    {
    }
  }

  public class exchange : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EmailAccountTypesAttribute.exchange>
  {
    public exchange()
      : base("E")
    {
    }
  }

  public class plugin : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EmailAccountTypesAttribute.plugin>
  {
    public plugin()
      : base("P")
    {
    }
  }
}
