// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAAcctSubDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FA;

public class FAAcctSubDefault
{
  public const string MaskAsset = "A";
  public const string MaskLocation = "L";
  public const string MaskDepartment = "D";
  public const string MaskClass = "C";

  public class CustomListAttribute(string[] AllowedValues, string[] AllowedLabels) : 
    PXStringListAttribute(AllowedValues, AllowedLabels)
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;
  }

  public class ClassListAttribute : FAAcctSubDefault.CustomListAttribute
  {
    public ClassListAttribute()
      : base(new string[4]{ "A", "L", "D", "C" }, new string[4]
      {
        "Fixed Asset",
        "Fixed Asset Branch",
        "Fixed Asset Department",
        "Fixed Asset Class"
      })
    {
    }
  }
}
