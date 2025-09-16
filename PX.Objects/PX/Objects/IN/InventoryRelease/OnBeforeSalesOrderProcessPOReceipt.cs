// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.OnBeforeSalesOrderProcessPOReceipt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.InventoryRelease;

public delegate List<PXResult<INItemPlan, INPlanType>> OnBeforeSalesOrderProcessPOReceipt(
  PXGraph graph,
  IEnumerable<PXResult<INItemPlan, INPlanType>> list,
  string POReceiptType,
  string POReceiptNbr);
