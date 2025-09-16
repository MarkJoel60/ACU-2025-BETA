// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.LedgerBalanceType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL;

public class LedgerBalanceType
{
  public const 
  #nullable disable
  string Actual = "A";
  public const string Report = "R";
  public const string Statistical = "S";
  public const string Budget = "B";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "A", "R", "S", "B" }, new string[4]
      {
        "Actual",
        "Reporting",
        "Statistical",
        "Budget"
      })
    {
    }
  }

  public class actual : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LedgerBalanceType.actual>
  {
    public actual()
      : base("A")
    {
    }
  }

  public class report : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LedgerBalanceType.report>
  {
    public report()
      : base("R")
    {
    }
  }

  public class statistical : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LedgerBalanceType.statistical>
  {
    public statistical()
      : base("S")
    {
    }
  }

  public class budget : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LedgerBalanceType.budget>
  {
    public budget()
      : base("B")
    {
    }
  }
}
