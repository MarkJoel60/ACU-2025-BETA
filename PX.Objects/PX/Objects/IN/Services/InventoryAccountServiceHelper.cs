// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Services.InventoryAccountServiceHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.PM;

#nullable disable
namespace PX.Objects.IN.Services;

public static class InventoryAccountServiceHelper
{
  public static InventoryAccountServiceParams Params(
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass)
  {
    return InventoryAccountServiceHelper.Params(item, site, postclass, (IProjectAccountsSource) null, (IProjectTaskAccountsSource) null);
  }

  public static InventoryAccountServiceParams Params(
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass,
    INTran tran)
  {
    return InventoryAccountServiceHelper.Params(item, site, postclass, tran, (IProjectAccountsSource) null, (IProjectTaskAccountsSource) null);
  }

  public static InventoryAccountServiceParams Params(
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass,
    IProjectAccountsSource project,
    IProjectTaskAccountsSource task)
  {
    return InventoryAccountServiceHelper.Params(item, site, postclass, (INTran) null, project, task);
  }

  public static InventoryAccountServiceParams Params(
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass,
    INTran tran,
    IProjectAccountsSource project,
    IProjectTaskAccountsSource task)
  {
    return new InventoryAccountServiceParams(item, site, postclass, tran, project, task, false);
  }

  public static InventoryAccountServiceParams Params(
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass,
    INTran tran,
    IProjectAccountsSource project,
    IProjectTaskAccountsSource task,
    bool useTransaction)
  {
    return new InventoryAccountServiceParams(item, site, postclass, tran, project, task, useTransaction);
  }
}
