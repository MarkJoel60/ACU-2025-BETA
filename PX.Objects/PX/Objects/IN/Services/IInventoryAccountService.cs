// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Services.IInventoryAccountService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN.Services;

public interface IInventoryAccountService
{
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  int? GetAcctID<Field>(
    PXGraph graph,
    string AcctDefault,
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass)
    where Field : IBqlField;

  int? GetAcctID<Field>(PXGraph graph, string AcctDefault, InventoryAccountServiceParams @params) where Field : IBqlField;

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  int? GetSubID<Field>(
    PXGraph graph,
    string AcctDefault,
    string SubMask,
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass,
    INTran tran)
    where Field : IBqlField;

  int? GetSubID<Field>(
    PXGraph graph,
    string AcctDefault,
    string SubMask,
    InventoryAccountServiceParams @params)
    where Field : IBqlField;

  int? GetPOAccrualAcctID<Field>(
    PXGraph graph,
    string AcctDefault,
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass,
    PX.Objects.AP.Vendor vendor)
    where Field : IBqlField;

  int? GetPOAccrualSubID<Field>(
    PXGraph graph,
    string AcctDefault,
    string SubMask,
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass,
    PX.Objects.AP.Vendor vendor)
    where Field : IBqlField;
}
