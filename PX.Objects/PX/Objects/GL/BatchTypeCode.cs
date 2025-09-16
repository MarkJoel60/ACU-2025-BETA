// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BatchTypeCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL;

public class BatchTypeCode
{
  public const 
  #nullable disable
  string Normal = "H";
  public const string Consolidation = "C";
  public const string TrialBalance = "T";
  public const string Reclassification = "RCL";
  public const string Allocation = "A";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[5]{ "H", "C", "T", "RCL", "A" }, new string[5]
      {
        "Normal",
        "Consolidation",
        "Trial Balance",
        "Reclassification",
        "Allocation"
      })
    {
    }
  }

  public class normal : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchTypeCode.normal>
  {
    public normal()
      : base("H")
    {
    }
  }

  public class consolidation : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchTypeCode.consolidation>
  {
    public consolidation()
      : base("C")
    {
    }
  }

  public class trialBalance : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchTypeCode.trialBalance>
  {
    public trialBalance()
      : base("T")
    {
    }
  }

  public class reclassification : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    BatchTypeCode.reclassification>
  {
    public reclassification()
      : base("RCL")
    {
    }
  }

  public class allocation : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchTypeCode.allocation>
  {
    public allocation()
      : base("A")
    {
    }
  }
}
