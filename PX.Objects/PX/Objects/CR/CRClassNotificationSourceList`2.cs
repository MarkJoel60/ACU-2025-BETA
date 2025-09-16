// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRClassNotificationSourceList`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public class CRClassNotificationSourceList<ClassID, SourceCD> : PXSelect<NotificationSource>
  where ClassID : IBqlField
  where SourceCD : IConstant<string>, IBqlOperand
{
  private PXView setupNotifications;

  public CRClassNotificationSourceList(PXGraph graph)
    : base(graph)
  {
    ((PXSelectBase) this).View = new PXView(graph, false, BqlTemplate.OfCommand<SelectFromBase<NotificationSource, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<NotificationSetup>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetup.setupID, Equal<NotificationSource.setupID>>>>>.And<BqlOperand<NotificationSetup.sourceCD, IBqlString>.IsEqual<BqlPlaceholder.Named<BqlPlaceholder.S>.AsField>>>>>.Where<BqlOperand<NotificationSource.classID, IBqlString>.IsEqual<BqlField<BqlPlaceholder.C, BqlPlaceholder.IBqlAny>.AsOptional>>.OrderBy<BqlField<NotificationSetup.notificationCD, IBqlString>.Asc>>.Replace<BqlPlaceholder.S>(typeof (SourceCD)).Replace<BqlPlaceholder.C>(typeof (ClassID)).ToCommand());
    this.setupNotifications = new PXView(graph, false, BqlCommand.CreateInstance(new System.Type[1]
    {
      BqlCommand.Compose(new System.Type[6]
      {
        typeof (Select<,>),
        typeof (NotificationSetup),
        typeof (Where<,>),
        typeof (NotificationSetup.sourceCD),
        typeof (Equal<>),
        typeof (SourceCD)
      })
    }));
    PXGraph.RowInsertedEvents rowInserted1 = graph.RowInserted;
    System.Type itemType1 = BqlCommand.GetItemType(typeof (ClassID));
    CRClassNotificationSourceList<ClassID, SourceCD> notificationSourceList1 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted1 = new PXRowInserted((object) notificationSourceList1, __vmethodptr(notificationSourceList1, OnClassRowInserted));
    rowInserted1.AddHandler(itemType1, pxRowInserted1);
    PXGraph.RowUpdatedEvents rowUpdated = graph.RowUpdated;
    System.Type itemType2 = BqlCommand.GetItemType(typeof (ClassID));
    CRClassNotificationSourceList<ClassID, SourceCD> notificationSourceList2 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) notificationSourceList2, __vmethodptr(notificationSourceList2, OnClassRowUpdated));
    rowUpdated.AddHandler(itemType2, pxRowUpdated);
    PXGraph.RowInsertedEvents rowInserted2 = graph.RowInserted;
    CRClassNotificationSourceList<ClassID, SourceCD> notificationSourceList3 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted2 = new PXRowInserted((object) notificationSourceList3, __vmethodptr(notificationSourceList3, OnSourceRowInseted));
    rowInserted2.AddHandler<NotificationSource>(pxRowInserted2);
  }

  protected virtual void OnClassRowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (e.Row == null || cache.GetValue(e.Row, typeof (ClassID).Name) == null)
      return;
    foreach (NotificationSetup modulePreferencesItem in this.GetModulePreferencesItems())
      ((PXSelectBase) this).Cache.Insert((object) new NotificationSource()
      {
        SetupID = modulePreferencesItem.SetupID
      });
  }

  public virtual void OnClassRowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (!cache.Graph.IsCopyPasteContext)
      return;
    foreach (object obj in ((PXSelectBase<NotificationSource>) this).Select(Array.Empty<object>()))
      ((PXSelectBase) this).Cache.Delete(obj);
  }

  protected virtual void OnSourceRowInseted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (cache.Graph.IsCopyPasteContext)
      return;
    NotificationSource row = (NotificationSource) e.Row;
    PXCache cach = cache.Graph.Caches[typeof (NotificationRecipient)];
    foreach (PXResult<NotificationSetupRecipient> pxResult in PXSelectBase<NotificationSetupRecipient, PXViewOf<NotificationSetupRecipient>.BasedOn<SelectFromBase<NotificationSetupRecipient, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<NotificationSetupRecipient.setupID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) row.SetupID
    }))
    {
      NotificationSetupRecipient notificationSetupRecipient = PXResult<NotificationSetupRecipient>.op_Implicit(pxResult);
      try
      {
        NotificationRecipient instance = (NotificationRecipient) cach.CreateInstance();
        instance.SetupID = row.SetupID;
        instance.ContactType = notificationSetupRecipient.ContactType;
        instance.ContactID = notificationSetupRecipient.ContactID;
        instance.Active = notificationSetupRecipient.Active;
        instance.AddTo = notificationSetupRecipient.AddTo;
        cach.Insert((object) instance);
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
      }
    }
  }

  private IEnumerable<NotificationSetup> GetModulePreferencesItems()
  {
    foreach (object obj in this.setupNotifications.SelectMulti(Array.Empty<object>()))
    {
      NotificationSetup modulePreferencesItem = PXResult.Unwrap<NotificationSetup>(obj);
      if (modulePreferencesItem != null)
        yield return modulePreferencesItem;
    }
  }
}
