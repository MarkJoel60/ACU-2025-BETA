// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CountryCodes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public static class CountryCodes
{
  public const 
  #nullable disable
  string US = "US";
  public const string Canada = "CA";

  public class us : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CountryCodes.us>
  {
    public us()
      : base("US")
    {
    }
  }

  public class canada : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CountryCodes.canada>
  {
    public canada()
      : base("CA")
    {
    }
  }
}
