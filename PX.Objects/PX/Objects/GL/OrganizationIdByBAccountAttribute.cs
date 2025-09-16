// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.OrganizationIdByBAccountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

public class OrganizationIdByBAccountAttribute : 
  PXEventSubscriberAttribute,
  IPXRowSelectingSubscriber
{
  protected string _SourceField;

  public OrganizationIdByBAccountAttribute(Type bAccountIdType)
  {
    if (!typeof (IBqlField).IsAssignableFrom(bAccountIdType))
      return;
    this._SourceField = bAccountIdType.Name;
  }

  public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (this._SourceField == null)
      return;
    int? nullable1 = sender.GetValue(e.Row, this._SourceField) as int?;
    int? nullable2 = new int?();
    if (nullable1.HasValue)
    {
      nullable2 = (int?) ((PXAccess.Organization) PXAccess.GetBranchByBAccountID(nullable1)?.Organization)?.OrganizationID;
      if (!nullable2.HasValue)
        nullable2 = (int?) ((PXAccess.Organization) PXAccess.GetOrganizationByBAccountID(nullable1))?.OrganizationID;
    }
    if (!nullable2.HasValue)
      return;
    sender.SetValue(e.Row, this._FieldName, (object) nullable2);
  }
}
