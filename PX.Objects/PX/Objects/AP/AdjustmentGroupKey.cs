// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AdjustmentGroupKey
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AP;

public class AdjustmentGroupKey
{
  public 
  #nullable disable
  string Source { get; set; }

  public string AdjdDocType { get; set; }

  public string AdjdRefNbr { get; set; }

  public long? AdjdCuryInfoID { get; set; }

  public override int GetHashCode()
  {
    return (this.Source, this.AdjdDocType, this.AdjdRefNbr).GetHashCode();
  }

  public override bool Equals(object obj)
  {
    if (!(obj is AdjustmentGroupKey adjustmentGroupKey))
      return base.Equals(obj);
    return this.Source == adjustmentGroupKey.Source && this.AdjdDocType == adjustmentGroupKey.AdjdDocType && this.AdjdRefNbr == adjustmentGroupKey.AdjdRefNbr;
  }

  public class AdjustmentType
  {
    public const string APAdjustment = "A";
    public const string POAdjustment = "P";
    public const string OutstandingBalance = "T";

    public class aPAdjustment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      AdjustmentGroupKey.AdjustmentType.aPAdjustment>
    {
      public aPAdjustment()
        : base("A")
      {
      }
    }

    public class pOAdjustment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      AdjustmentGroupKey.AdjustmentType.pOAdjustment>
    {
      public pOAdjustment()
        : base("P")
      {
      }
    }

    public class outstandingBalance : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      AdjustmentGroupKey.AdjustmentType.outstandingBalance>
    {
      public outstandingBalance()
        : base("T")
      {
      }
    }
  }
}
