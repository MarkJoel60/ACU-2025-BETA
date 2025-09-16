// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ReasonCodeUsages
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

public class ReasonCodeUsages
{
  public const 
  #nullable disable
  string Sales = "S";
  public const string CreditWriteOff = "C";
  public const string BalanceWriteOff = "B";
  public const string Issue = "I";
  public const string Receipt = "R";
  public const string Transfer = "T";
  public const string Adjustment = "A";
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public const string Disassembly = "D";
  public const string AssemblyDisassembly = "D";
  public const string VendorReturn = "N";
  public const string Production = "P";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[10]
      {
        PXStringListAttribute.Pair("S", "Sales"),
        PXStringListAttribute.Pair("C", "Credit Write-Off"),
        PXStringListAttribute.Pair("B", "Balance Write-Off"),
        PXStringListAttribute.Pair("I", "Issue"),
        PXStringListAttribute.Pair("R", "Receipt"),
        PXStringListAttribute.Pair("A", "Adjustment"),
        PXStringListAttribute.Pair("T", "Transfer"),
        PXStringListAttribute.Pair("D", "Assembly/Disassembly"),
        PXStringListAttribute.Pair("N", "Vendor Return"),
        PXStringListAttribute.Pair("P", "Production")
      })
    {
    }
  }

  public class sales : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReasonCodeUsages.sales>
  {
    public sales()
      : base("S")
    {
    }
  }

  public class creditWriteOff : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReasonCodeUsages.creditWriteOff>
  {
    public creditWriteOff()
      : base("C")
    {
    }
  }

  public class balanceWriteOff : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ReasonCodeUsages.balanceWriteOff>
  {
    public balanceWriteOff()
      : base("B")
    {
    }
  }

  public class issue : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReasonCodeUsages.issue>
  {
    public issue()
      : base("I")
    {
    }
  }

  public class receipt : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReasonCodeUsages.receipt>
  {
    public receipt()
      : base("R")
    {
    }
  }

  public class transfer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReasonCodeUsages.transfer>
  {
    public transfer()
      : base("T")
    {
    }
  }

  public class adjustment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReasonCodeUsages.adjustment>
  {
    public adjustment()
      : base("A")
    {
    }
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public class disassembly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReasonCodeUsages.disassembly>
  {
    public disassembly()
      : base("D")
    {
    }
  }

  public class assemblyDisassembly : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ReasonCodeUsages.assemblyDisassembly>
  {
    public assemblyDisassembly()
      : base("D")
    {
    }
  }

  public class vendorReturn : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReasonCodeUsages.vendorReturn>
  {
    public vendorReturn()
      : base("N")
    {
    }
  }

  public class production : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReasonCodeUsages.production>
  {
    public production()
      : base("P")
    {
    }
  }
}
