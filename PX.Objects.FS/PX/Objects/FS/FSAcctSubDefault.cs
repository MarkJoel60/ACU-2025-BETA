// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAcctSubDefault
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class FSAcctSubDefault
{
  public const string MaskBranchLocation = "A";
  public const string MaskCompany = "C";
  public const string MaskItem = "I";
  public const string MaskCustomerLocation = "L";
  public const string MaskPostingClass = "P";
  public const string MaskSalesPerson = "S";
  public const string MaskServiceOrderType = "T";
  public const string MaskWarehouse = "W";

  public class CustomListAttribute(string[] allowedValues, string[] allowedLabels) : 
    PXStringListAttribute(allowedValues, allowedLabels)
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;
  }

  /// <summary>
  /// Defines a list of the possible sources for the FS Documents sub-account defaulting: <br />
  /// Namely: MaskCustomerLocation, MaskItem, MaskServiceOrderType, MaskCompany, MaskBranchLocation <br />
  /// Mostly, this attribute serves as a container <br />
  /// </summary>
  public class ClassListAttribute : FSAcctSubDefault.CustomListAttribute
  {
    public ClassListAttribute()
      : base(new string[8]
      {
        "A",
        "C",
        "I",
        "L",
        "P",
        "S",
        "T",
        "W"
      }, new string[8]
      {
        "Branch Location",
        "Branch",
        "Inventory Item",
        "Customer Location",
        "Posting Class",
        "Salesperson",
        "Service Order Type",
        "Warehouse"
      })
    {
    }

    public ClassListAttribute(bool contract)
      : base(new string[4]{ "L", "I", "C", "A" }, new string[4]
      {
        "Customer Location",
        "Inventory Item",
        "Branch",
        "Branch Location"
      })
    {
    }
  }
}
