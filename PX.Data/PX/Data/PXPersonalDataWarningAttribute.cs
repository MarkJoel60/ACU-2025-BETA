// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPersonalDataWarningAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class PXPersonalDataWarningAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.SetAltered(this._FieldName, true);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    object obj = sender.GetValue(e.Row, "PseudonymizationStatus");
    if (obj == null)
      return;
    if (((int) obj & 1) != 1)
      e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null);
    else
      e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, error: (int) obj == 3 ? PXLocalizer.Localize("The record has been erased due to personal data protection reasons and the related personal data is being masked.") : PXLocalizer.Localize("The processing of the record is restricted and the related personal data is being masked."), errorLevel: PXErrorLevel.Warning);
  }
}
