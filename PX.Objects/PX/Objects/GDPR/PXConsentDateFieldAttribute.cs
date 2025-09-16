// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.PXConsentDateFieldAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.MassProcess;
using System;

#nullable disable
namespace PX.Objects.GDPR;

[PXDBDate]
[PXMassUpdatableField]
[PXContactInfoField]
[PXUIField(DisplayName = "Date of Consent", FieldClass = "GDPR")]
public class PXConsentDateFieldAttribute : 
  PXEntityAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldVerifyingSubscriber
{
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    object obj = sender.GetValue(e.Row, "ConsentAgreement");
    if (obj == null)
      return;
    this.Required = (bool) obj;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null)
      return;
    object obj = sender.GetValue(e.Row, "ConsentAgreement");
    if (obj == null || !(bool) obj || e.NewValue != null)
      return;
    sender.RaiseExceptionHandling(this.FieldName, e.Row, e.NewValue, (Exception) new PXSetPropertyException("No consent date has been specified."));
  }
}
