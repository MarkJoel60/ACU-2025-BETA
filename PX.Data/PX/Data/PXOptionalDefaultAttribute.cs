// Decompiled with JetBrains decompiler
// Type: PX.Data.PXOptionalDefaultAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <summary>Sets the default value for a DAC field as <see cref="T:PX.Data.PXDefaultAttribute" />
/// and when field value retrieved from database is DBNULL</summary>
[PXInternalUseOnly]
public class PXOptionalDefaultAttribute : PXDefaultAttribute
{
  public PXOptionalDefaultAttribute(System.Type sourceType)
    : base(sourceType)
  {
    this._PersistingCheck = PXPersistingCheck.Nothing;
  }

  public PXOptionalDefaultAttribute(object constant)
    : base(constant)
  {
    this._PersistingCheck = PXPersistingCheck.Nothing;
  }

  public PXOptionalDefaultAttribute(object constant, System.Type sourceType)
    : base(constant, sourceType)
  {
    this._PersistingCheck = PXPersistingCheck.Nothing;
  }

  public PXOptionalDefaultAttribute() => this._PersistingCheck = PXPersistingCheck.Nothing;

  public PXOptionalDefaultAttribute(TypeCode converter, string constant)
    : base(converter, constant)
  {
    this._PersistingCheck = PXPersistingCheck.Nothing;
  }

  public PXOptionalDefaultAttribute(TypeCode converter, string constant, System.Type sourceType)
    : base(converter, constant, sourceType)
  {
    this._PersistingCheck = PXPersistingCheck.Nothing;
  }

  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row == null || sender.GetValue(e.Row, this._FieldOrdinal) != null)
      return;
    object newValue;
    if (sender.RaiseFieldDefaulting(this._FieldName, e.Row, out newValue))
      sender.RaiseFieldUpdating(this._FieldName, e.Row, ref newValue);
    sender.SetValue(e.Row, this._FieldOrdinal, newValue);
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.RowSelecting += new PXRowSelecting(this.RowSelecting);
  }
}
