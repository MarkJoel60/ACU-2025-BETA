// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPseudonymizationStatusListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

public class PXPseudonymizationStatusListAttribute : PXIntListAttribute
{
  public const int NotPseudonymized = 0;
  public const int Pseudonymized = 1;
  public const int Erased = 3;

  public PXPseudonymizationStatusListAttribute()
    : base(new int[3]{ 0, 1, 3 }, new string[3]
    {
      "Not Pseudonymized",
      nameof (Pseudonymized),
      nameof (Erased)
    })
  {
  }

  public class notPseudonymized : 
    BqlType<IBqlInt, int>.Constant<
    #nullable disable
    PXPseudonymizationStatusListAttribute.notPseudonymized>
  {
    public notPseudonymized()
      : base(0)
    {
    }
  }

  public class pseudonymized : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    PXPseudonymizationStatusListAttribute.pseudonymized>
  {
    public pseudonymized()
      : base(1)
    {
    }
  }

  public class erased : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  PXPseudonymizationStatusListAttribute.erased>
  {
    public erased()
      : base(3)
    {
    }
  }
}
