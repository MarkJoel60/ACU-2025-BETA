// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.ILSMaster
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.IN;

public interface ILSMaster : IItemPlanMaster
{
  string TranType { get; }

  DateTime? TranDate { get; }

  short? InvtMult { get; set; }

  new int? InventoryID { get; set; }

  new int? SiteID { get; set; }

  int? LocationID { get; set; }

  int? SubItemID { get; set; }

  string LotSerialNbr { get; set; }

  DateTime? ExpireDate { get; set; }

  string UOM { get; set; }

  Decimal? Qty { get; set; }

  Decimal? BaseQty { get; set; }

  int? ProjectID { get; set; }

  int? TaskID { get; set; }

  bool? IsIntercompany { get; }
}
