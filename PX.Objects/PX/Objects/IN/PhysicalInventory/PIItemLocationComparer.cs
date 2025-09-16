// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PhysicalInventory.PIItemLocationComparer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.PhysicalInventory;

public class PIItemLocationComparer : IItemLocationComparer, IComparer<PIItemLocationInfo>
{
  private readonly INPIClass piClass;

  public PIItemLocationComparer(INPIClass piClass) => this.piClass = piClass;

  public int Compare(PIItemLocationInfo x, PIItemLocationInfo y)
  {
    string[] sortingKey1 = this.GetSortingKey(x);
    string[] sortingKey2 = this.GetSortingKey(y);
    int num = 0;
    for (int index = 0; index < sortingKey1.Length; ++index)
    {
      num = string.Compare(sortingKey1[index], sortingKey2[index], StringComparison.InvariantCultureIgnoreCase);
      if (num != 0)
        break;
    }
    return num;
  }

  public string[] GetSortColumns()
  {
    List<string> sortColumns = new List<string>();
    this.AddSortColumnToListIfSet(sortColumns, this.piClass.NAO1);
    this.AddSortColumnToListIfSet(sortColumns, this.piClass.NAO2);
    this.AddSortColumnToListIfSet(sortColumns, this.piClass.NAO3);
    this.AddSortColumnToListIfSet(sortColumns, this.piClass.NAO4);
    return sortColumns.ToArray();
  }

  private void AddSortColumnToListIfSet(List<string> sortColumns, string naoCode)
  {
    string fieldName = this.NAOCodeToFieldName(naoCode);
    if (fieldName == null)
      return;
    sortColumns.Add(fieldName);
  }

  private string NAOCodeToFieldName(string naoCode)
  {
    switch (naoCode)
    {
      case "ES":
        return (string) null;
      case "LI":
        return "INLocation__locationCD";
      case "II":
        return "InventoryItem__inventoryCD";
      case "SI":
        return "INSubItem__subItemCD";
      case "LS":
        return "INLotSerialStatus__lotSerialNbr";
      case "ID":
        return "InventoryItem__descr";
      default:
        throw new PXException("Unknown PI Tag # sort order");
    }
  }

  private string[] GetSortingKey(PIItemLocationInfo il)
  {
    List<string> sortColumns = new List<string>();
    this.AddFieldIfNAOCodeSet(sortColumns, this.piClass.NAO1, il);
    this.AddFieldIfNAOCodeSet(sortColumns, this.piClass.NAO2, il);
    this.AddFieldIfNAOCodeSet(sortColumns, this.piClass.NAO3, il);
    this.AddFieldIfNAOCodeSet(sortColumns, this.piClass.NAO4, il);
    return sortColumns.ToArray();
  }

  private void AddFieldIfNAOCodeSet(
    List<string> sortColumns,
    string naoCode,
    PIItemLocationInfo il)
  {
    string fieldValue = this.NAOCodeToFieldValue(naoCode, il);
    if (fieldValue == null)
      return;
    sortColumns.Add(fieldValue);
  }

  private string NAOCodeToFieldValue(string naoCode, PIItemLocationInfo il)
  {
    switch (naoCode)
    {
      case "ES":
        return (string) null;
      case "LI":
        return il.LocationCD ?? string.Empty;
      case "II":
        return il.InventoryCD ?? string.Empty;
      case "SI":
        return il.SubItemCD ?? string.Empty;
      case "LS":
        return il.LotSerialNbr ?? string.Empty;
      case "ID":
        return il.Description ?? string.Empty;
      default:
        throw new PXException("Unknown PI Tag # sort order");
    }
  }
}
