// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTimeCardType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public static class EPTimeCardType
{
  public const 
  #nullable disable
  string TimeCardDefault = "D";
  public const string TimeCardSimple = "S";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "D", "S" }, new string[2]
      {
        "Default",
        "Simple"
      })
    {
    }
  }

  public class timeCardDefault : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPTimeCardType.timeCardDefault>
  {
    public timeCardDefault()
      : base("D")
    {
    }
  }

  public class timeCardSimple : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPTimeCardType.timeCardSimple>
  {
    public timeCardSimple()
      : base("S")
    {
    }
  }
}
