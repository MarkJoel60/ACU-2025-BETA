// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.ReportCollectionItemAssignProcessor`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using System;

#nullable disable
namespace PX.Api.Reports;

internal class ReportCollectionItemAssignProcessor<T> : PropertyAssigmentProcessor where T : class
{
  private readonly Func<T> _targetProvider;

  public ReportCollectionItemAssignProcessor(Func<T> targetProvider)
    : base(typeof (T))
  {
    this._targetProvider = targetProvider;
  }

  protected T CollectionItem => this._targetProvider();

  protected override object GetPropertyOwner(Command cmd) => (object) this.CollectionItem;

  public override bool CanExecute(Command cmd)
  {
    return (object) this.CollectionItem != null && base.CanExecute(cmd) && cmd.Commit;
  }
}
