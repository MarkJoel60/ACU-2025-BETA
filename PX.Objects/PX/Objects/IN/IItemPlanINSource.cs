// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.IItemPlanINSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.IN;

public interface IItemPlanINSource : IItemPlanSource, IItemPlanMaster
{
  string DocType { get; }

  string TranType { get; }

  short? InvtMult { get; }

  string TransferType { get; set; }

  string OrigPlanType { get; }

  int? SubItemID { get; }

  int? LocationID { get; }

  string LotSerialNbr { get; }

  string AssignedNbr { get; }

  Decimal? BaseQty { get; }

  DateTime? TranDate { get; }

  string SOLineType { get; }

  string POLineType { get; }

  bool? Released { get; }

  int? ToSiteID { get; }

  int? ToLocationID { get; }

  bool? IsFixedInTransit { get; }
}
