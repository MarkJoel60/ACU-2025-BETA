// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AvoidControlAccountsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.GL;

public class AvoidControlAccountsAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldVerifyingSubscriber,
  IPXRowPersistingSubscriber
{
  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || ((CancelEventArgs) e).Cancel || e.NewValue == null)
      return;
    AccountAttribute.VerifyAccountIsNotControl(sender, this._FieldName, (EventArgs) e);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row == null || ((CancelEventArgs) e).Cancel)
      return;
    AccountAttribute.VerifyAccountIsNotControl(sender, this._FieldName, (EventArgs) e);
  }
}
