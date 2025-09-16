// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPDelegationOf
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public static class EPDelegationOf
{
  public const 
  #nullable disable
  string Approvals = "A";
  public const string Expenses = "E";
  public const string TimeEntries = "T";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "A", "E", "T" }, new string[3]
      {
        "Approvals",
        "Expense Receipts and Claims",
        "Time Activities and Employee Time Cards"
      })
    {
    }
  }

  public class approvals : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPDelegationOf.approvals>
  {
    public approvals()
      : base("A")
    {
    }
  }

  public class expenses : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPDelegationOf.expenses>
  {
    public expenses()
      : base("E")
    {
    }
  }

  public class timeEntries : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPDelegationOf.timeEntries>
  {
    public timeEntries()
      : base("T")
    {
    }
  }
}
