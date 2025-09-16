// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.Common;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.TX;

public class TaxType : ILabelProvider
{
  private static readonly 
  #nullable disable
  IEnumerable<ValueLabelPair> _valueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "S",
      "Output"
    },
    {
      "P",
      "Input"
    }
  };
  public const string Sales = "S";
  public const string Purchase = "P";
  public const string PendingSales = "A";
  public const string PendingPurchase = "B";

  public IEnumerable<ValueLabelPair> ValueLabelPairs => TaxType._valueLabelPairs;

  public class sales : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxType.sales>
  {
    public sales()
      : base("S")
    {
    }
  }

  public class purchase : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxType.purchase>
  {
    public purchase()
      : base("P")
    {
    }
  }

  public class pendingSales : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxType.pendingSales>
  {
    public pendingSales()
      : base("A")
    {
    }
  }

  public class pendingPurchase : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxType.pendingPurchase>
  {
    public pendingPurchase()
      : base("B")
    {
    }
  }
}
