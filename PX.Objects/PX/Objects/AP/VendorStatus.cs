// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AP;

public class VendorStatus
{
  public const 
  #nullable disable
  string Active = "A";
  public const string Hold = "H";
  public const string HoldPayments = "P";
  public const string OneTime = "T";
  public const string Inactive = "I";
  public const string Initial = "A";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(PXStringListAttribute.Pair("A", "Active"), PXStringListAttribute.Pair("H", "On Hold"), PXStringListAttribute.Pair("P", "Hold Payments"), PXStringListAttribute.Pair("T", "One-Time"), PXStringListAttribute.Pair("I", "Inactive"))
    {
    }
  }

  public class active : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  VendorStatus.active>
  {
    public active()
      : base("A")
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  VendorStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class holdPayments : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  VendorStatus.holdPayments>
  {
    public holdPayments()
      : base("P")
    {
    }
  }

  public class oneTime : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  VendorStatus.oneTime>
  {
    public oneTime()
      : base("T")
    {
    }
  }

  public class inactive : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  VendorStatus.inactive>
  {
    public inactive()
      : base("I")
    {
    }
  }
}
