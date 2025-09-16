// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.DAC;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CR;

public class CRSetupMaint : PXGraph<CRSetupMaint>
{
  public PXSave<CRSetup> Save;
  public PXCancel<CRSetup> Cancel;
  public PXSelect<CRSetup> CRSetupRecord;
  public CRNotificationSetupList<CRNotification> Notifications;
  public PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Current<CRNotification.setupID>>>> Recipients;
  public PXSelect<CRCampaignType> CampaignType;

  [PXDBString(10)]
  [PXDefault]
  [CRMContactType.List]
  [PXUIField(DisplayName = "Contact Type")]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationSetupRecipient.contactID)}, Where = typeof (Where<NotificationSetupRecipient.setupID, Equal<Current<NotificationSetupRecipient.setupID>>>))]
  public virtual void NotificationSetupRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact ID")]
  [PXNotificationContactSelector(typeof (NotificationSetupRecipient.contactType))]
  public virtual void NotificationSetupRecipient_ContactID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(Events.RowSelected<CRSetup> e)
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
    PXUIFieldAttribute.SetVisible<CRSetup.defaultRateTypeID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRSetup>>) e).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CRSetup.allowOverrideRate>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRSetup>>) e).Cache, (object) null, flag);
  }
}
