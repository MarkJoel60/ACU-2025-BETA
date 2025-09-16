// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.PXConsentExpirationDateFieldAttribute
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

[PXDBDate]
[PXMassUpdatableField]
[PXContactInfoField]
[PXUIField(DisplayName = "Consent Expires", FieldClass = "GDPR")]
public class PXConsentExpirationDateFieldAttribute : PXEntityAttribute, IPXFieldSelectingSubscriber
{
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.gDPRCompliance>() || e.Row == null)
      return;
    object obj1 = sender.GetValue(e.Row, this.FieldName);
    if (obj1 == null)
      return;
    object obj2 = sender.GetValue(e.Row, "ConsentAgreement");
    if (obj2 == null)
      return;
    int num = (bool) obj2 ? 1 : 0;
    DateTime? nullable1 = (DateTime?) obj1;
    PXEntryStatus status = sender.GetStatus(e.Row);
    if (num != 0)
    {
      DateTime? nullable2 = nullable1;
      DateTime? businessDate = sender.Graph.Accessinfo.BusinessDate;
      if ((nullable2.HasValue & businessDate.HasValue ? (nullable2.GetValueOrDefault() < businessDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 && status != 2)
      {
        e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (Type) null, new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, (string) null, (string) null, (string) null, "The consent has expired.", (PXErrorLevel) 2, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
        return;
      }
    }
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (Type) null, new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
  }
}
