// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public class CABankFeedType
{
  public const 
  #nullable disable
  string Plaid = "P";
  public const string MX = "M";
  public const string TestPlaid = "T";
  public const string File = "F";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(CABankFeedType.ListAttribute.GetTypes)
    {
    }

    public static (string, string)[] GetTypes
    {
      get
      {
        return new (string, string)[4]
        {
          ("P", "Plaid"),
          ("M", "MX"),
          ("F", "File"),
          ("T", "Test Plaid")
        };
      }
    }
  }

  public class mx : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankFeedType.mx>
  {
    public mx()
      : base("M")
    {
    }
  }

  public class plaid : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankFeedType.plaid>
  {
    public plaid()
      : base("P")
    {
    }
  }

  public class testPlaid : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankFeedType.testPlaid>
  {
    public testPlaid()
      : base("P")
    {
    }
  }

  public class file : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankFeedType.testPlaid>
  {
    public file()
      : base("F")
    {
    }
  }
}
