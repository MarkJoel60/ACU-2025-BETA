// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOMiscAcctSubDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SOMiscAcctSubDefault
{
  public const string MaskItem = "I";
  public const string MaskLocation = "L";
  public const string MaskEmployee = "E";
  public const string MaskCompany = "C";

  public class AcctListAttribute : SOCustomListAttribute
  {
    private static Tuple<string, string>[] Pairs
    {
      get
      {
        return new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("L", SOCustomListAttribute.MaskLocationLabel),
          PXStringListAttribute.Pair("I", "Non-Stock Item")
        };
      }
    }

    public AcctListAttribute()
      : base(SOMiscAcctSubDefault.AcctListAttribute.Pairs)
    {
    }

    protected override Tuple<string, string>[] GetPairs()
    {
      return SOMiscAcctSubDefault.AcctListAttribute.Pairs;
    }
  }

  public class SubListAttribute : SOCustomListAttribute
  {
    private static Tuple<string, string>[] Pairs
    {
      get
      {
        return new Tuple<string, string>[4]
        {
          PXStringListAttribute.Pair("L", SOCustomListAttribute.MaskLocationLabel),
          PXStringListAttribute.Pair("I", "Non-Stock Item"),
          PXStringListAttribute.Pair("E", "Employee"),
          PXStringListAttribute.Pair("C", "Branch")
        };
      }
    }

    public SubListAttribute()
      : base(SOMiscAcctSubDefault.SubListAttribute.Pairs)
    {
    }

    protected override Tuple<string, string>[] GetPairs()
    {
      return SOMiscAcctSubDefault.SubListAttribute.Pairs;
    }
  }
}
