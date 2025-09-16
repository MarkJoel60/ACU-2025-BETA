// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.IUnitRateService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

public interface IUnitRateService
{
  Decimal? CalculateUnitPrice(
    PXCache sender,
    int? projectID,
    int? projectTaskID,
    int? inventoryID,
    string UOM,
    Decimal? qty,
    DateTime? date,
    long? curyInfoID);

  Decimal? CalculateUnitCost(
    PXCache sender,
    int? projectID,
    int? projectTaskID,
    int? inventoryID,
    string UOM,
    int? employeeID,
    DateTime? date,
    long? curyInfoID);
}
