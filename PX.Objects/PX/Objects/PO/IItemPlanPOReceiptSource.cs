// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.IItemPlanPOReceiptSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.PO;

public interface IItemPlanPOReceiptSource : IItemPlanSource, IItemPlanMaster
{
  string ReceiptNbr { get; }

  string ReceiptType { get; }

  string LineType { get; }

  string UOM { get; }

  string OrigPlanType { get; }

  int? SubItemID { get; }

  int? LocationID { get; }

  string LotSerialNbr { get; }

  DateTime? ExpireDate { get; }

  string AssignedNbr { get; }

  string PONbr { get; }

  Decimal? Qty { get; }

  Decimal? BaseQty { get; }

  bool? IsReverse { get; }

  DateTime? ReceiptDate { get; }
}
