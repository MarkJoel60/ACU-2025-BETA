// Decompiled with JetBrains decompiler
// Type: PX.SM.PreferencesEmailMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.SM;

public class PreferencesEmailMaint : PXGraph<PreferencesEmailMaint>
{
  public PXSelect<PreferencesEmail> Prefs;
  public PXSave<PreferencesEmail> Save;
  public PXCancel<PreferencesEmail> Cancel;

  protected virtual void PreferencesEmail_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    PreferencesEmail row = e.Row as PreferencesEmail;
    if (!(e.NewRow is PreferencesEmail newRow) || row == null)
      return;
    if ((newRow.EmailTagPrefix != row.EmailTagPrefix || newRow.EmailTagSuffix != row.EmailTagSuffix) && this.Prefs.View.Ask("Old emails could not be processed if you change Email Tags.", MessageButtons.YesNo) != WebDialogResult.Yes)
      e.Cancel = true;
    if (newRow.NotificationSiteUrl != null)
      return;
    sender.SetDefaultExt<PreferencesEmail.notificationSiteUrl>((object) newRow);
  }

  protected virtual void PreferencesEmail_NotificationSiteUrl_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) PXUrl.SiteUrlWithPath();
  }

  public void _(Events.RowSelected<PreferencesEmail> e)
  {
    PreferencesEmail row = e.Row;
    if (row == null)
      return;
    PXCache cache = e.Cache;
    bool? nullable = row.SuspendEmailProcessing;
    bool flag1 = false;
    int num = nullable.GetValueOrDefault() == flag1 & nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PreferencesEmail.sendUserEmailsImmediately>(cache, (object) null, num != 0);
    nullable = row.SendUserEmailsImmediately;
    bool flag2 = true;
    if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      e.Cache.RaiseExceptionHandling<PreferencesEmail.sendUserEmailsImmediately>((object) row, (object) row.SendUserEmailsImmediately, (Exception) new PXSetPropertyException("Outgoing emails that users send through the Email Activity (CR306015) form are processed and sent immediately without waiting in the processing queue.", PXErrorLevel.Warning));
    else
      e.Cache.RaiseExceptionHandling<PreferencesEmail.sendUserEmailsImmediately>((object) row, (object) null, (Exception) null);
  }

  protected virtual void _(
    Events.FieldUpdated<PreferencesEmail, PreferencesEmail.suspendEmailProcessing> e)
  {
    PreferencesEmail row = e.Row;
    if (row == null)
      return;
    e.Cache.SetValue<PreferencesEmail.sendUserEmailsImmediately>((object) row, (object) false);
  }
}
