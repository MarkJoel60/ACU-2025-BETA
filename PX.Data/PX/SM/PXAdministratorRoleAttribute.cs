// Decompiled with JetBrains decompiler
// Type: PX.SM.PXAdministratorRoleAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public class PXAdministratorRoleAttribute : PXAggregateAttribute
{
  public PXAdministratorRoleAttribute()
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDBStringAttribute(64 /*0x40*/)
    {
      IsUnicode = true
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXAdministratorRoleAttribute.PXDefaultAdministratorRoleAttribute());
  }

  /// <exclude />
  private class PXDefaultAdministratorRoleAttribute : PXDefaultAttribute, IPXRowSelectingSubscriber
  {
    public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
    {
      if (e.NewValue == null)
        return;
      e.NewValue = (object) PXAccess.GetAdministratorRole();
    }

    public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
    {
      if (e.Row == null || sender.GetValue(e.Row, this.FieldOrdinal) != null)
        return;
      sender.SetValue(e.Row, this.FieldOrdinal, (object) PXAccess.GetAdministratorRole());
    }
  }
}
