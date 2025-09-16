// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FormatDirection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL;

public class FormatDirection
{
  public const 
  #nullable disable
  string Display = "D";
  public const string Store = "S";
  public const string Error = "E";

  public sealed class display : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FormatDirection.display>
  {
    public display()
      : base("D")
    {
    }
  }

  public sealed class store : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FormatDirection.store>
  {
    public store()
      : base("S")
    {
    }
  }

  public sealed class error : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FormatDirection.error>
  {
    public error()
      : base("E")
    {
    }
  }
}
