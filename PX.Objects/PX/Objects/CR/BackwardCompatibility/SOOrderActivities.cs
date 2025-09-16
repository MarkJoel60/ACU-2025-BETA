// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.SOOrderActivities
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[Obsolete]
public sealed class SOOrderActivities(PXGraph graph) : CRActivityList<SOOrder>(graph)
{
  protected override string GetPrimaryRecipientFromContext(
    NotificationUtility utility,
    string type,
    object row,
    NotificationSource source)
  {
    if (!(((PXSelectBase) this)._Graph.Caches[typeof (SOOrder)].Current is SOOrder current))
      return (string) null;
    PX.Objects.CR.Contact parent = (PX.Objects.CR.Contact) PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<SOOrder>.By<SOOrder.contactID>.FindParent(((PXSelectBase) this)._Graph, (SOOrder.contactID) current, (PKFindOptions) 0);
    return parent == null || parent.EMail == null ? (string) null : parent.EMail;
  }
}
