// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPReasonSettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.EP;

public static class EPReasonSettings
{
  public const 
  #nullable disable
  string Optional = "O";
  public const string Required = "R";
  public const string NotPrompted = "N";
  [Obsolete("Use NotPrompted")]
  public const string NotRequired = "N";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "O", "R", "N" }, new string[3]
      {
        "Is Optional",
        "Is Required",
        "Is Not Prompted"
      })
    {
    }
  }

  public class notPrompted : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPReasonSettings.notPrompted>
  {
    public notPrompted()
      : base("N")
    {
    }
  }

  public class optional : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPReasonSettings.optional>
  {
    public optional()
      : base("O")
    {
    }
  }

  public class required : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPReasonSettings.required>
  {
    public required()
      : base("R")
    {
    }
  }

  [Obsolete("Use notPrompted")]
  public class notRequired : Constant<string>
  {
    public notRequired()
      : base("N")
    {
    }
  }
}
