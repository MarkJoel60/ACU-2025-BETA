// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostDocType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.Common;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.PO;

public class POLandedCostDocType : ILabelProvider
{
  public const 
  #nullable disable
  string LandedCost = "L";
  public const string Correction = "C";
  public const string Reversal = "R";
  protected static readonly IEnumerable<ValueLabelPair> _valueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "L",
      "Landed Cost"
    },
    {
      "C",
      nameof (Correction)
    },
    {
      "R",
      nameof (Reversal)
    }
  };

  public IEnumerable<ValueLabelPair> ValueLabelPairs => POLandedCostDocType._valueLabelPairs;

  public class ListAttribute : LabelListAttribute
  {
    public ListAttribute()
      : base(POLandedCostDocType._valueLabelPairs)
    {
    }
  }

  public class landedCost : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLandedCostDocType.landedCost>
  {
    public landedCost()
      : base("L")
    {
    }
  }

  public class correction : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLandedCostDocType.correction>
  {
    public correction()
      : base("C")
    {
    }
  }

  public class reversal : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLandedCostDocType.reversal>
  {
    public reversal()
      : base("R")
    {
    }
  }
}
