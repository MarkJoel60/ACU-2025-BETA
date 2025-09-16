// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SalesPriceSet
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Objects.FS;

/// <summary>
/// Contains the information related to a price for an inventory item.
/// </summary>
public class SalesPriceSet
{
  public SalesPriceSet(
    string priceCode,
    Decimal? price,
    string priceType,
    int? customerID,
    string errorCode)
  {
    this.PriceCode = priceCode;
    this.Price = price;
    this.PriceType = priceType;
    this.CustomerID = customerID;
    this.ErrorCode = errorCode;
  }

  public string PriceCode { get; set; }

  public Decimal? Price { get; set; }

  public string PriceType { get; set; }

  public int? CustomerID { get; set; }

  public string ErrorCode { get; set; }
}
