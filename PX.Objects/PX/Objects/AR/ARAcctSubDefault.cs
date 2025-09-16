// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARAcctSubDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.AR;

public class ARAcctSubDefault
{
  public const string MaskLocation = "L";
  public const string MaskItem = "I";
  public const string MaskEmployee = "E";
  public const string MaskCompany = "C";
  public const string MaskSalesPerson = "S";

  public class CustomListAttribute(string[] AllowedValues, string[] AllowedLabels) : 
    PXStringListAttribute(AllowedValues, AllowedLabels)
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;
  }

  /// <summary>
  /// Defines a list of the possible sources for the AR Documents sub-account defaulting: <br />
  /// Namely: MaskLocation, MaskItem, MaskEmployee, MaskCompany, MaskSalesPerson <br />
  /// Mostly, this attribute serves as a container <br />
  /// </summary>
  public class ClassListAttribute : ARAcctSubDefault.CustomListAttribute
  {
    public ClassListAttribute()
      : base(new string[5]{ "L", "I", "E", "C", "S" }, new string[5]
      {
        !PXAccess.FeatureInstalled<FeaturesSet.accountLocations>() ? "Customer" : "Customer Location",
        "Non-Stock Item",
        "Employee",
        "Branch",
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
        "C",
        "S"
      };
      this._AllowedLabels = new string[5]
      {
        !PXAccess.FeatureInstalled<FeaturesSet.accountLocations>() ? "Customer" : "Customer Location",
        "Non-Stock Item",
        "Employee",
        "Branch",
        "Salesperson"
      };
      this._NeutralAllowedLabels = this._AllowedLabels;
      base.CacheAttached(sender);
    }
  }
}
