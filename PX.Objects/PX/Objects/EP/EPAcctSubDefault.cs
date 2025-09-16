// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAcctSubDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.EP;

public class EPAcctSubDefault
{
  public const string MaskEmployee = "E";
  public const string MaskItem = "I";
  public const string MaskCompany = "C";
  public const string MaskProject = "J";
  public const string MaskTask = "T";
  public const string MaskLocation = "L";

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
  public class ClassListAttribute : EPAcctSubDefault.CustomListAttribute
  {
    public ClassListAttribute()
      : base(new string[6]{ "E", "I", "C", "J", "T", "L" }, new string[6]
      {
        "Employee",
        "Non-Stock Item",
        "Branch",
        "Project",
        "Task",
        !PXAccess.FeatureInstalled<FeaturesSet.accountLocations>() ? "Customer" : "Customer Location"
      })
    {
    }

    public virtual void CacheAttached(PXCache sender)
    {
      this._AllowedValues = new string[6]
      {
        "E",
        "I",
        "C",
        "J",
        "T",
        "L"
      };
      this._AllowedLabels = new string[6]
      {
        "Employee",
        "Non-Stock Item",
        "Branch",
        "Project",
        "Task",
        !PXAccess.FeatureInstalled<FeaturesSet.accountLocations>() ? "Customer" : "Customer Location"
      };
      this._NeutralAllowedLabels = this._AllowedLabels;
      base.CacheAttached(sender);
    }
  }
}
