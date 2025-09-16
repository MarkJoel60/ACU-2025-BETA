// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.PXRequiredExprAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

public class PXRequiredExprAttribute : PXEventSubscriberAttribute
{
  protected IBqlWhere _Where;

  public PXRequiredExprAttribute(Type where)
  {
    if (where == (Type) null)
      throw new PXArgumentException(nameof (where), "The argument cannot be null.");
    this._Where = typeof (IBqlWhere).IsAssignableFrom(where) ? (IBqlWhere) Activator.CreateInstance(where) : throw new PXArgumentException(nameof (where), "An invalid argument has been specified.");
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    // ISSUE: method pointer
    sender.Graph.RowPersisting.AddHandler(sender.GetItemType(), new PXRowPersisting((object) this, __methodptr(RowPersisting)));
  }

  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (sender.GetStatus(e.Row) != 1 && sender.GetStatus(e.Row) != 2)
      return;
    bool? nullable = new bool?();
    object obj = (object) null;
    ((IBqlUnary) this._Where).Verify(sender, e.Row, new List<object>(), ref nullable, ref obj);
    PXDefaultAttribute.SetPersistingCheck(sender, this.FieldName, e.Row, !nullable.HasValue || !nullable.Value ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
  }
}
