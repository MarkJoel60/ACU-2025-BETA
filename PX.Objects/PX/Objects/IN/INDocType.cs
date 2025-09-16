// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INDocType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INDocType
{
  public const 
  #nullable disable
  string Undefined = "0";
  public const string Issue = "I";
  public const string Receipt = "R";
  public const string Transfer = "T";
  public const string Adjustment = "A";
  public const string Production = "P";
  public const string Change = "C";
  public const string Disassembly = "D";
  public const string DropShip = "H";
  public const string Invoice = "N";
  public const string Manufacturing = "M";

  public class NumberingAttribute : AutoNumberAttribute
  {
    public NumberingAttribute()
      : base(typeof (INRegister.docType), typeof (INRegister.tranDate), AutoNumberAttribute.Pair("I").To<INSetup.issueNumberingID>(), AutoNumberAttribute.Pair("R").To<INSetup.receiptNumberingID>(), AutoNumberAttribute.Pair("T").To<INSetup.receiptNumberingID>(), AutoNumberAttribute.Pair("A").To<INSetup.adjustmentNumberingID>(), AutoNumberAttribute.Pair("P").To<INSetup.kitAssemblyNumberingID>(), AutoNumberAttribute.Pair("C").To<INSetup.kitAssemblyNumberingID>(), AutoNumberAttribute.Pair("D").To<INSetup.kitAssemblyNumberingID>(), AutoNumberAttribute.Pair("M").To<INSetup.manufacturingNumberingID>())
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[7]
      {
        PXStringListAttribute.Pair("I", "Issue"),
        PXStringListAttribute.Pair("R", "Receipt"),
        PXStringListAttribute.Pair("T", "Transfer"),
        PXStringListAttribute.Pair("A", "Adjustment"),
        PXStringListAttribute.Pair("P", "Assembly"),
        PXStringListAttribute.Pair("D", "Disassembly"),
        PXStringListAttribute.Pair("M", "Manufacturing")
      })
    {
    }
  }

  public class KitListAttribute : PXStringListAttribute
  {
    public KitListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("P", "Assembly"),
        PXStringListAttribute.Pair("D", "Disassembly")
      })
    {
    }
  }

  public class SOListAttribute : PXStringListAttribute
  {
    public SOListAttribute()
      : base(new Tuple<string, string>[7]
      {
        PXStringListAttribute.Pair("I", "Issue"),
        PXStringListAttribute.Pair("R", "Receipt"),
        PXStringListAttribute.Pair("T", "Transfer"),
        PXStringListAttribute.Pair("A", "Adjustment"),
        PXStringListAttribute.Pair("P", "Assembly"),
        PXStringListAttribute.Pair("D", "Disassembly"),
        PXStringListAttribute.Pair("H", "Drop-Shipment")
      })
    {
    }
  }

  public class undefined : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INDocType.undefined>
  {
    public undefined()
      : base("0")
    {
    }
  }

  public class issue : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INDocType.issue>
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
  INDocType.receipt>
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
  INDocType.transfer>
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
  INDocType.adjustment>
  {
    public adjustment()
      : base("A")
    {
    }
  }

  public class production : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INDocType.production>
  {
    public production()
      : base("P")
    {
    }
  }

  public class change : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INDocType.change>
  {
    public change()
      : base("C")
    {
    }
  }

  public class disassembly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INDocType.disassembly>
  {
    public disassembly()
      : base("D")
    {
    }
  }

  public class dropShip : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INDocType.dropShip>
  {
    public dropShip()
      : base("H")
    {
    }
  }

  public class manufacturing : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INDocType.manufacturing>
  {
    public manufacturing()
      : base("M")
    {
    }
  }
}
