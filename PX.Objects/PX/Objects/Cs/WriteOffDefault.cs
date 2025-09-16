// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.WriteOffDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CS;

public class WriteOffDefault
{
  public const string ReasonCode = "R";
  public const string CustomerLocation = "L";
  public const string Branch = "C";
  public const string Employee = "E";
  public const string Salesperson = "S";
  public const string InventoryItem = "I";
  public const string PostingClass = "P";
  public const string Warehouse = "W";

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
  public class ClassListAttribute : WriteOffDefault.CustomListAttribute
  {
    public ClassListAttribute()
      : base(new string[3]{ "R", "L", "C" }, new string[3]
      {
        "Reason Code",
        !PXAccess.FeatureInstalled<FeaturesSet.accountLocations>() ? "Customer" : "Customer Location",
        "Branch"
      })
    {
    }

    public virtual void CacheAttached(PXCache sender)
    {
      this._AllowedValues = new string[3]{ "R", "L", "C" };
      this._AllowedLabels = new string[3]
      {
        "Reason Code",
        !PXAccess.FeatureInstalled<FeaturesSet.accountLocations>() ? "Customer" : "Customer Location",
        "Branch"
      };
      this._NeutralAllowedLabels = this._AllowedLabels;
      base.CacheAttached(sender);
    }
  }
}
