// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEventSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

public class EPEventSetupMaint : PXGraph<EPEventSetupMaint>
{
  public PXSelect<EPSetup> Setup;
  public PXSave<EPSetup> Save;
  public PXCancel<EPSetup> Cancel;

  protected virtual void EPSetup_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EPSetup row))
      return;
    bool? nullable = row.SendOnlyEventCard;
    bool valueOrDefault1 = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<EPSetup.isSimpleNotification>(cache, (object) row, !valueOrDefault1);
    nullable = row.IsSimpleNotification;
    bool valueOrDefault2 = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<EPSetup.addContactInformation>(cache, (object) row, !valueOrDefault1 & valueOrDefault2);
    bool flag = !valueOrDefault1 && !valueOrDefault2;
    PXUIFieldAttribute.SetEnabled<EPSetup.invitationTemplateID>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<EPSetup.rescheduleTemplateID>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<EPSetup.cancelInvitationTemplateID>(cache, (object) row, flag);
  }

  protected virtual void EPSetup_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is EPSetup row) || e.Operation == 3)
      return;
    bool? nullable = row.IsSimpleNotification;
    if (nullable.GetValueOrDefault())
      return;
    nullable = row.SendOnlyEventCard;
    if (nullable.GetValueOrDefault())
      return;
    EPEventSetupMaint.CheckNotificationTemplateForEmpty(cache, e.Row, typeof (EPSetup.invitationTemplateID).Name);
    EPEventSetupMaint.CheckNotificationTemplateForEmpty(cache, e.Row, typeof (EPSetup.rescheduleTemplateID).Name);
    EPEventSetupMaint.CheckNotificationTemplateForEmpty(cache, e.Row, typeof (EPSetup.cancelInvitationTemplateID).Name);
  }

  private static void CheckNotificationTemplateForEmpty(
    PXCache cache,
    object row,
    string fieldName)
  {
    if (cache.GetValue(row, fieldName) != null)
      return;
    if (cache.RaiseExceptionHandling(fieldName, row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) "[fieldName]"
    })))
      throw new PXRowPersistingException(fieldName, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) fieldName
      });
  }
}
