// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.PXMultiFactorTypeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.EP;
using PX.SM;

#nullable disable
namespace PX.Data.Maintenance;

public class PXMultiFactorTypeAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is Users row))
      return;
    int? loginTypeId = row.LoginTypeID;
    if (!loginTypeId.HasValue)
      return;
    PXSelect<EPLoginType, Where<EPLoginType.loginTypeID, Equal<Required<Users.loginTypeID>>>> pxSelect = new PXSelect<EPLoginType, Where<EPLoginType.loginTypeID, Equal<Required<Users.loginTypeID>>>>(sender.Graph);
    object[] objArray = new object[1];
    loginTypeId = row.LoginTypeID;
    objArray[0] = (object) loginTypeId.Value;
    EPLoginType epLoginType = pxSelect.SelectSingle(objArray);
    if (epLoginType == null)
      return;
    bool? disableTwoFactorAuth = epLoginType.DisableTwoFactorAuth;
    bool flag = true;
    if (!(disableTwoFactorAuth.GetValueOrDefault() == flag & disableTwoFactorAuth.HasValue) && !(epLoginType.AllowedLoginType == "A"))
      return;
    e.ReturnValue = (object) 0;
  }
}
