// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.GainLossAcctSubDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CM;

public class GainLossAcctSubDefault
{
  public const string MaskCurrency = "N";
  public const string MaskCompany = "C";

  public class CustomListAttribute(string[] AllowedValues, string[] AllowedLabels) : 
    PXStringListAttribute(AllowedValues, AllowedLabels)
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;
  }

  public class ListAttribute : GainLossAcctSubDefault.CustomListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "N", "C" }, new string[2]
      {
        "Currency",
        "Branch"
      })
    {
    }
  }
}
