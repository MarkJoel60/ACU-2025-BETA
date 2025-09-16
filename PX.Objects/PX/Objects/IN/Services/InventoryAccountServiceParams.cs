// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Services.InventoryAccountServiceParams
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.PM;

#nullable disable
namespace PX.Objects.IN.Services;

public class InventoryAccountServiceParams
{
  public InventoryAccountServiceParams(
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass,
    INTran tran,
    IProjectAccountsSource project,
    IProjectTaskAccountsSource task,
    bool useTransaction)
  {
    this.Item = item;
    this.Site = site;
    this.Postclass = postclass;
    this.INTran = tran;
    this.Project = project;
    this.Task = task;
    this.UseTransaction = useTransaction;
  }

  public PX.Objects.IN.InventoryItem Item { get; set; }

  public INSite Site { get; set; }

  public INPostClass Postclass { get; set; }

  public INTran INTran { get; set; }

  public IProjectAccountsSource Project { get; set; }

  public IProjectTaskAccountsSource Task { get; set; }

  public bool UseTransaction { get; set; }

  public InventoryAccountServiceParams UsingTransaction()
  {
    return this.UseTransaction ? this : new InventoryAccountServiceParams(this.Item, this.Site, this.Postclass, this.INTran, this.Project, this.Task, true);
  }
}
