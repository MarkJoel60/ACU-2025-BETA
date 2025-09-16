// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.ReportCollectionInsertProcessor`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using System.Collections.Generic;

#nullable disable
namespace PX.Api.Reports;

internal class ReportCollectionInsertProcessor<T> : CommandProcessor<NewRow> where T : class, new()
{
  private T _item;
  private readonly ICollection<T> _collection;
  private readonly string _containerName;

  public ReportCollectionInsertProcessor(ICollection<T> collection, string containerName)
  {
    this._containerName = containerName;
    this._collection = collection;
  }

  public T GetItem() => this._item;

  public override bool CanExecute(Command cmd)
  {
    return base.CanExecute(cmd) && cmd.ObjectName == this._containerName;
  }

  public override void Execute(NewRow actionCmd)
  {
    this._item = new T();
    this._collection.Add(this._item);
  }
}
