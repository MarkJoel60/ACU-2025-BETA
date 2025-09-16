// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

public class POOrderType
{
  public const 
  #nullable disable
  string Blanket = "BL";
  public const string StandardBlanket = "SB";
  public const string RegularOrder = "RO";
  public const string RegularSubcontract = "RS";
  public const string DropShip = "DP";
  public const string ProjectDropShip = "PD";

  public static bool IsUseBlanket(string orderType) => orderType == "RO" || orderType == "DP";

  public static bool IsNormalType(string orderType) => orderType == "RO" || orderType == "RS";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[5]
      {
        PXStringListAttribute.Pair("RO", "Normal"),
        PXStringListAttribute.Pair("DP", "Drop-Ship"),
        PXStringListAttribute.Pair("PD", "Project Drop-Ship"),
        PXStringListAttribute.Pair("BL", "Blanket"),
        PXStringListAttribute.Pair("SB", "Standard")
      })
    {
    }

    internal bool TryGetValue(string label, out string value)
    {
      int index = Array.IndexOf<string>(this._AllowedLabels, label);
      if (index >= 0)
      {
        value = this._AllowedValues[index];
        return true;
      }
      value = (string) null;
      return false;
    }
  }

  public class PrintListAttribute : PXStringListAttribute
  {
    public PrintListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("BL", "Blanket"),
        PXStringListAttribute.Pair("SB", "Stadard"),
        PXStringListAttribute.Pair("RO", "Normal"),
        PXStringListAttribute.Pair("DP", "Drop Ship")
      })
    {
    }
  }

  public class StatdardBlanketList : PXStringListAttribute
  {
    public StatdardBlanketList()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("RO", "Normal"),
        PXStringListAttribute.Pair("BL", "Blanket"),
        PXStringListAttribute.Pair("SB", "Standard")
      })
    {
    }
  }

  public class StatdardAndRegularList : PXStringListAttribute
  {
    public StatdardAndRegularList()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("SB", "Standard"),
        PXStringListAttribute.Pair("RO", "Normal")
      })
    {
    }
  }

  public class BlanketList : PXStringListAttribute
  {
    public BlanketList()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("BL", "Blanket"),
        PXStringListAttribute.Pair("SB", "Standard")
      })
    {
    }
  }

  /// <summary>
  /// Selector. Provides list of "Regular" Purchase Orders types.
  /// Include RegularOrder, DropShip.
  /// </summary>
  public class RegularDropShipListAttribute : PXStringListAttribute
  {
    public RegularDropShipListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("RO", "Normal"),
        PXStringListAttribute.Pair("DP", "Drop-Ship"),
        PXStringListAttribute.Pair("PD", "Project Drop-Ship")
      })
    {
    }
  }

  /// <summary>
  /// Selector. Defines a list of Purchase Order types, which are allowed <br />
  /// for use in the SO module: RegularOrder, Blanket, DropShip, Transfer.<br />
  /// </summary>
  public class RBDListAttribute : PXStringListAttribute
  {
    public RBDListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("RO", "Normal"),
        PXStringListAttribute.Pair("BL", "Blanket"),
        PXStringListAttribute.Pair("DP", "Drop-Ship")
      })
    {
    }
  }

  public class RBDSListAttribute : PXStringListAttribute
  {
    public RBDSListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("RO", "Normal"),
        PXStringListAttribute.Pair("BL", "Blanket"),
        PXStringListAttribute.Pair("DP", "Drop-Ship"),
        PXStringListAttribute.Pair("RS", "Subcontract")
      })
    {
    }
  }

  /// <summary>
  /// Selector. Defines a list of Purchase Order types, which are allowed <br />
  /// for use in the RQ module: RegularOrder, DropShip, Blanket, Standard.<br />
  /// </summary>
  public class RequisitionListAttribute : PXStringListAttribute
  {
    public RequisitionListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("RO", "Normal"),
        PXStringListAttribute.Pair("DP", "Drop-Ship"),
        PXStringListAttribute.Pair("BL", "Blanket"),
        PXStringListAttribute.Pair("SB", "Standard")
      })
    {
    }
  }

  public class RPSListAttribute : PXStringListAttribute
  {
    public RPSListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("RO", "Normal Purchase Order"),
        PXStringListAttribute.Pair("PD", "Project Drop-Ship"),
        PXStringListAttribute.Pair("RS", "Subcontract")
      })
    {
    }
  }

  public class RPListAttribute : PXStringListAttribute
  {
    public RPListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("RO", "Normal Purchase Order"),
        PXStringListAttribute.Pair("PD", "Project Drop-Ship")
      })
    {
    }
  }

  public class blanket : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderType.blanket>
  {
    public blanket()
      : base("BL")
    {
    }
  }

  public class standardBlanket : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderType.standardBlanket>
  {
    public standardBlanket()
      : base("SB")
    {
    }
  }

  public class regularOrder : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderType.regularOrder>
  {
    public regularOrder()
      : base("RO")
    {
    }
  }

  public class regularSubcontract : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    POOrderType.regularSubcontract>
  {
    public regularSubcontract()
      : base("RS")
    {
    }
  }

  public class dropShip : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderType.dropShip>
  {
    public dropShip()
      : base("DP")
    {
    }
  }

  public class projectDropShip : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderType.projectDropShip>
  {
    public projectDropShip()
      : base("PD")
    {
    }
  }
}
