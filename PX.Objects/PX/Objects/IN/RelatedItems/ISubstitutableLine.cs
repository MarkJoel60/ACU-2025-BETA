// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.ISubstitutableLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.IN.RelatedItems;

public interface ISubstitutableLine
{
  int? LineNbr { get; set; }

  int? BranchID { get; set; }

  int? CustomerID { get; set; }

  int? SiteID { get; set; }

  int? InventoryID { get; set; }

  int? SubItemID { get; set; }

  string UOM { get; set; }

  Decimal? Qty { get; set; }

  Decimal? BaseQty { get; set; }

  Decimal? UnitPrice { get; set; }

  Decimal? CuryUnitPrice { get; set; }

  Decimal? CuryExtPrice { get; set; }

  bool? ManualPrice { get; set; }

  bool? SubstitutionRequired { get; set; }

  bool? SkipLineDiscounts { get; set; }
}
