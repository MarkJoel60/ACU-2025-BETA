// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxReportLineType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.TX;

public class TaxReportLineType : ILabelProvider
{
  public const 
  #nullable disable
  string TaxAmount = "P";
  public const string TaxableAmount = "A";
  public const string ExemptedAmount = "E";
  protected static readonly IEnumerable<ValueLabelPair> _valueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "P",
      "Tax Amount"
    },
    {
      "A",
      "Taxable Amount"
    },
    {
      "E",
      "Exempted Amount"
    }
  };
  public static readonly string[] Values = new string[3]
  {
    "P",
    "A",
    "E"
  };
  public static readonly string[] Labels = new string[3]
  {
    "Tax Amount",
    "Taxable Amount",
    "Exempted Amount"
  };

  public IEnumerable<ValueLabelPair> ValueLabelPairs => TaxReportLineType._valueLabelPairs;

  public class ListAttribute : LabelListAttribute
  {
    public ListAttribute()
      : base(TaxReportLineType._valueLabelPairs)
    {
    }
  }

  public class taxAmount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxReportLineType.taxAmount>
  {
    public taxAmount()
      : base("P")
    {
    }
  }

  public class taxableAmount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxReportLineType.taxableAmount>
  {
    public taxableAmount()
      : base("A")
    {
    }
  }

  public class exemptedAmount : Constant<string>
  {
    public exemptedAmount()
      : base("E")
    {
    }
  }
}
