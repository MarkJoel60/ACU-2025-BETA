// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.GL;

public class AccountType : ILabelProvider
{
  public const 
  #nullable disable
  string Asset = "A";
  public const string Liability = "L";
  public const string Income = "I";
  public const string Expense = "E";
  public static string[] COAOrderOptions = new string[4]
  {
    "1233",
    "1234",
    "3412",
    "2311"
  };

  public IEnumerable<ValueLabelPair> ValueLabelPairs
  {
    get
    {
      return (IEnumerable<ValueLabelPair>) new ValueLabelList()
      {
        {
          "A",
          "Asset"
        },
        {
          "L",
          "Liability"
        },
        {
          "I",
          "Income"
        },
        {
          "E",
          "Expense"
        }
      };
    }
  }

  public static int Ordinal(string Type)
  {
    switch (Type)
    {
      case "A":
        return 0;
      case "L":
        return 1;
      case "I":
        return 2;
      case "E":
        return 3;
      default:
        throw new PXArgumentException();
    }
  }

  public static string Literal(short Ordinal)
  {
    switch (Ordinal)
    {
      case 0:
        return "A";
      case 1:
        return "L";
      case 2:
        return "I";
      case 3:
        return "E";
      default:
        throw new PXArgumentException();
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "A", "L", "I", "E" }, new string[4]
      {
        "Asset",
        "Liability",
        "Income",
        "Expense"
      })
    {
    }
  }

  public class asset : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AccountType.asset>
  {
    public asset()
      : base("A")
    {
    }
  }

  public class liability : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AccountType.liability>
  {
    public liability()
      : base("L")
    {
    }
  }

  public class income : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AccountType.income>
  {
    public income()
      : base("I")
    {
    }
  }

  public class expense : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AccountType.expense>
  {
    public expense()
      : base("E")
    {
    }
  }
}
