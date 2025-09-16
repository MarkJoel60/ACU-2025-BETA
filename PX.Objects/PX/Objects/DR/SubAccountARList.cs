// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.SubAccountARList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.DR;

public class SubAccountARList
{
  public const string MaskLocation = "L";
  public const string MaskItem = "I";
  public const string MaskEmployee = "E";
  public const string MaskDeferralCode = "D";
  public const string MaskSalesPerson = "S";

  public class CustomListAttribute(string[] AllowedValues, string[] AllowedLabels) : 
    PXStringListAttribute(AllowedValues, AllowedLabels)
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;
  }

  /// <summary>
  /// Specialized version of the string list attribute which represents <br />
  /// the list of the possible sources of the segments for the sub-account <br />
  /// defaulting in the AP transactions. <br />
  /// </summary>
  public class ClassListAttribute : SubAccountARList.CustomListAttribute
  {
    public ClassListAttribute()
      : base(new string[5]{ "L", "I", "E", "D", "S" }, new string[5]
      {
        PXAccess.FeatureInstalled<FeaturesSet.accountLocations>() ? "Customer Location" : "Customer",
        "Item",
        "Employee",
        "Deferral Code",
        "Salesperson"
      })
    {
    }

    public virtual void CacheAttached(PXCache sender)
    {
      this._AllowedValues = new string[5]
      {
        "L",
        "I",
        "E",
        "D",
        "S"
      };
      this._AllowedLabels = new string[5]
      {
        PXAccess.FeatureInstalled<FeaturesSet.accountLocations>() ? "Customer Location" : "Customer",
        "Item",
        "Employee",
        "Deferral Code",
        "Salesperson"
      };
      this._NeutralAllowedLabels = this._AllowedLabels;
      base.CacheAttached(sender);
    }
  }
}
