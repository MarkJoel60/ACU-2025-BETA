// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Contract_BillingPeriod
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Contract_BillingPeriod : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Contract_BillingPeriod().ID_LIST, new ID.Contract_BillingPeriod().TX_LIST)
    {
    }
  }

  public class Week : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_Contract_BillingPeriod.Week>
  {
    public Week()
      : base("W")
    {
    }
  }

  public class Month : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Contract_BillingPeriod.Month>
  {
    public Month()
      : base("M")
    {
    }
  }

  public class Quarter : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_BillingPeriod.Quarter>
  {
    public Quarter()
      : base("Q")
    {
    }
  }

  public class HalfYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_BillingPeriod.HalfYear>
  {
    public HalfYear()
      : base("H")
    {
    }
  }

  public class Year : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Contract_BillingPeriod.Year>
  {
    public Year()
      : base("Y")
    {
    }
  }
}
