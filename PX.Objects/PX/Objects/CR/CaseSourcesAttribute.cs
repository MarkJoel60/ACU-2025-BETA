// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CaseSourcesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public sealed class CaseSourcesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string _EMAIL = "EM";
  public const string _PHONE = "PH";
  public const string _WEB = "WB";
  public const string _CHAT = "CH";

  public CaseSourcesAttribute()
    : base(new string[4]{ "EM", "PH", "WB", "CH" }, new string[4]
    {
      "Email",
      "Phone",
      "Web",
      "Chat"
    })
  {
  }

  public sealed class Email : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CaseSourcesAttribute.Email>
  {
    public Email()
      : base("EM")
    {
    }
  }

  public sealed class Phone : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CaseSourcesAttribute.Phone>
  {
    public Phone()
      : base("PH")
    {
    }
  }

  public sealed class Web : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CaseSourcesAttribute.Web>
  {
    public Web()
      : base("WB")
    {
    }
  }

  public sealed class Chat : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CaseSourcesAttribute.Chat>
  {
    public Chat()
      : base("CH")
    {
    }
  }
}
