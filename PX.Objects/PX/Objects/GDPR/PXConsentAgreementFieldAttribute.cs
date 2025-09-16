// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.PXConsentAgreementFieldAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.MassProcess;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.GDPR;

[PXDBBool]
[PXDefault(false)]
[PXMassUpdatableField]
[PXContactInfoField]
[PXUIField(DisplayName = "Consented to the Processing of Personal Data", FieldClass = "GDPR")]
public class PXConsentAgreementFieldAttribute : 
  PXEntityAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldUpdatedSubscriber
{
  /// <summary>
  /// Set true for suppress warning message appearance. Have false value by default.
  /// </summary>
  public bool SuppressWarning { get; set; }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.gDPRCompliance>() || e.Row == null || this.SuppressWarning)
      return;
    if (!(sender.GetValue(e.Row, "ConsentAgreement") as bool?).GetValueOrDefault())
      e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (Type) null, new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, (string) null, (string) null, (string) null, "Consent to the processing of personal data has not been given or has expired.", (PXErrorLevel) 2, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    else
      e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (Type) null, new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
  }

  public virtual void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null || sender.GetValue(e.Row, "ConsentDate") != null || !(sender.GetValue(e.Row, this.FieldName) as bool?).GetValueOrDefault())
      return;
    sender.SetValue(e.Row, "ConsentDate", (object) sender.Graph.Accessinfo.BusinessDate);
  }
}
