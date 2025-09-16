// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SMCostCodeAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.FS;

public class SMCostCodeAttribute : CostCodeAttribute, IPXRowPersistingSubscriber
{
  private Type _SkipRowPersistingValidation;

  public SMCostCodeAttribute(Type account, Type task)
    : base(account, task)
  {
  }

  public SMCostCodeAttribute(Type skipRowPersistingValidation, Type account, Type task)
    : base(account, task)
  {
    this._SkipRowPersistingValidation = skipRowPersistingValidation;
  }

  public new void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    bool? nullable = this._SkipRowPersistingValidation != (Type) null ? (bool?) sender.GetValue(e.Row, this._SkipRowPersistingValidation.Name) : new bool?();
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    base.RowPersisting(sender, e);
  }
}
