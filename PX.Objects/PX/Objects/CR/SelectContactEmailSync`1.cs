// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.SelectContactEmailSync`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

public class SelectContactEmailSync<TWhere> : SelectContactEmailSyncBase<Select<Contact, TWhere>> where TWhere : IBqlWhere, new()
{
  public SelectContactEmailSync(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public SelectContactEmailSync(PXGraph graph)
    : base(graph)
  {
  }

  protected override void FieldUpdated<TSrcField, TDstField>(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    base.FieldUpdated<TSrcField, TDstField>(sender, e);
  }

  public new class Config : 
    PXSelectBase<Contact, SelectContactEmailSyncBase<Select<Contact, TWhere>>.Config>.IViewConfig,
    IViewConfigBase
  {
    public BqlCommand GetCommand() => (BqlCommand) new Select<Contact, TWhere>();

    public bool IsReadOnly => false;
  }
}
