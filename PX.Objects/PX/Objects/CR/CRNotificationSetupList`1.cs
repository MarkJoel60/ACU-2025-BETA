// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRNotificationSetupList`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CR;

public class CRNotificationSetupList<Table> : 
  PXSelectOrderBy<Table, OrderBy<Asc<NotificationSetup.notificationCD, Asc<NotificationSetup.nBranchID>>>>
  where Table : NotificationSetup, new()
{
  public CRNotificationSetupList(PXGraph graph)
    : base(graph)
  {
    graph.Views.Caches.Add(typeof (NotificationSource));
    graph.Views.Caches.Add(typeof (NotificationRecipient));
    PXGraph.RowDeletedEvents rowDeleted = graph.RowDeleted;
    System.Type type1 = typeof (Table);
    CRNotificationSetupList<Table> notificationSetupList1 = this;
    // ISSUE: virtual method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) notificationSetupList1, __vmethodptr(notificationSetupList1, OnRowDeleted));
    rowDeleted.AddHandler(type1, pxRowDeleted);
    PXGraph.RowPersistingEvents rowPersisting = graph.RowPersisting;
    System.Type type2 = typeof (Table);
    CRNotificationSetupList<Table> notificationSetupList2 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) notificationSetupList2, __vmethodptr(notificationSetupList2, OnRowPersisting));
    rowPersisting.AddHandler(type2, pxRowPersisting);
  }

  protected virtual void OnRowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    NotificationSetup row = (NotificationSetup) e.Row;
    PXCache cach1 = cache.Graph.Caches[typeof (NotificationSource)];
    foreach (PXResult<NotificationSource> pxResult in PXSelectBase<NotificationSource, PXSelect<NotificationSource, Where<NotificationSource.setupID, Equal<Required<NotificationSource.setupID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) row.SetupID
    }))
    {
      NotificationSource notificationSource = PXResult<NotificationSource>.op_Implicit(pxResult);
      cach1.Delete((object) notificationSource);
    }
    PXCache cach2 = cache.Graph.Caches[typeof (NotificationRecipient)];
    foreach (PXResult<NotificationRecipient> pxResult in PXSelectBase<NotificationRecipient, PXSelect<NotificationRecipient, Where<NotificationRecipient.setupID, Equal<Required<NotificationRecipient.setupID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) row.SetupID
    }))
    {
      NotificationRecipient notificationRecipient = PXResult<NotificationRecipient>.op_Implicit(pxResult);
      cach2.Delete((object) notificationRecipient);
    }
  }

  protected virtual void OnRowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    NotificationSetup row = (NotificationSetup) e.Row;
    if (row == null || row.NotificationCD != null)
      return;
    cache.RaiseExceptionHandling<NotificationSetup.notificationCD>(e.Row, (object) row.NotificationCD, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("'{0}' may not be empty.", new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<NotificationSetup.notificationCD>(cache)
    }), (PXErrorLevel) 5, new object[1]
    {
      (object) typeof (NotificationSetup.notificationCD).Name
    }));
  }
}
