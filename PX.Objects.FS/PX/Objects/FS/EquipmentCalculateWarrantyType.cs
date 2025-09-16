// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.EquipmentCalculateWarrantyType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public static class EquipmentCalculateWarrantyType
{
  public const string SalesOrderDate = "SD";
  public const string InstallationDate = "AD";
  public const string Earliest = "ED";
  public const string Latest = "LD";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "SD", "AD", "ED", "LD" }, new string[4]
      {
        "Sales Order Date",
        "Installation Date",
        "The Earliest of Both Dates",
        "The Latest of Both Dates"
      })
    {
    }
  }
}
