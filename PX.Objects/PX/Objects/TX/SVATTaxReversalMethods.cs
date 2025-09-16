// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.SVATTaxReversalMethods
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.TX;

public class SVATTaxReversalMethods
{
  public const 
  #nullable disable
  string OnPayments = "P";
  public const string OnDocuments = "D";
  public const string OnPrepayment = "Y";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "P", "D" }, new string[2]
      {
        "On Payments",
        "On Documents"
      })
    {
    }
  }

  public class onPayments : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SVATTaxReversalMethods.onPayments>
  {
    public onPayments()
      : base("P")
    {
    }
  }

  public class onDocuments : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SVATTaxReversalMethods.onDocuments>
  {
    public onDocuments()
      : base("D")
    {
    }
  }

  public class onPrepayment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SVATTaxReversalMethods.onPrepayment>
  {
    public onPrepayment()
      : base("Y")
    {
    }
  }
}
