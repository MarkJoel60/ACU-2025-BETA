// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPRuleType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public static class EPRuleType
{
  public const 
  #nullable disable
  string Direct = "D";
  public const string Document = "E";
  public const string Filter = "F";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "D", "E", "F" }, new string[3]
      {
        "Employee",
        "Employee from Document",
        "Employees by Filter"
      })
    {
    }
  }

  public class direct : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPRuleType.direct>
  {
    public direct()
      : base("D")
    {
    }
  }

  public class document : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPRuleType.document>
  {
    public document()
      : base("E")
    {
    }
  }

  public class filter : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPRuleType.filter>
  {
    public filter()
      : base("F")
    {
    }
  }
}
