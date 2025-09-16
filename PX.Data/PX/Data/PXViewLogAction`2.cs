// Decompiled with JetBrains decompiler
// Type: PX.Data.PXViewLogAction`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
internal sealed class PXViewLogAction<Primary, Table> : PXAction<Primary>, IGetSubscriber
  where Primary : class, IBqlTable, new()
  where Table : class, IBqlTable, new()
{
  private PXProcessing<Table> _Processing;

  public PXViewLogAction(PXProcessing<Table> processing, Delegate handler)
    : base(processing.View.Graph, handler)
  {
    this._Processing = processing;
  }

  object IGetSubscriber.GetSubscriber()
  {
    if (this._Processing.View.RefreshRequested != null)
    {
      Delegate[] invocationList = this._Processing.View.RefreshRequested.GetInvocationList();
      if (invocationList != null && invocationList.Length != 0)
        return invocationList[0].Target;
    }
    return (object) null;
  }

  string IGetSubscriber.GetSelected() => this._Processing._SelectedField;

  bool IGetSubscriber.GetVisible()
  {
    bool? showLog = this._Processing.ScheduleParameters.ShowLog;
    bool flag = true;
    return showLog.GetValueOrDefault() == flag & showLog.HasValue;
  }
}
