// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Interfaces.IAlternateSelectable
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.Common.Interfaces;

public interface IAlternateSelectable
{
  string AlternateID { get; set; }

  string AlternateType { get; set; }

  string AlternateDescr { get; set; }

  string BarCode { get; set; }

  string BarCodeType { get; set; }

  string BarCodeDescr { get; set; }

  string InventoryAlternateID { get; set; }

  string InventoryAlternateType { get; set; }

  string InventoryAlternateDescr { get; set; }

  int? InventoryID { get; set; }
}
