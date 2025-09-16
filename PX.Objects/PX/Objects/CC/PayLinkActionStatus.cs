// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.PayLinkActionStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CC;

public static class PayLinkActionStatus
{
  public const 
  #nullable disable
  string Open = "O";
  public const string Success = "S";
  public const string Error = "E";

  public class ListAttribute : PXStringListAttribute
  {
  }

  public class error : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PayLinkActionStatus.error>
  {
    public error()
      : base("E")
    {
    }
  }
}
