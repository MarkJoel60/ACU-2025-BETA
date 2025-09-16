// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSavePerRow`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// The Save action that you need to use instead of <see cref="T:PX.Data.PXSave`1" /> for the forms
/// with one PXGrid container where multiple records can be edited and these records do not depend on one another,
/// such as the Charts of Accounts (GL202500) and Site Map (SM200520) forms.
/// If you use <see cref="T:PX.Data.PXSavePerRow`1" />, an error that may occur during saving of one record does not prevent saving of other records.
/// </summary>
public class PXSavePerRow<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  protected string fieldName;

  protected PXSavePerRow(PXGraph graph)
    : base(graph)
  {
  }

  public PXSavePerRow(PXGraph graph, string name)
    : base(graph, name)
  {
    foreach (PXEventSubscriberAttribute attribute in graph.Caches[typeof (TNode)].GetAttributes((string) null))
    {
      if (attribute is IPXInterfaceField pxInterfaceField && (pxInterfaceField.Visibility & PXUIVisibility.SelectorVisible) == PXUIVisibility.SelectorVisible)
      {
        this.fieldName = attribute.FieldName;
        break;
      }
    }
  }

  public PXSavePerRow(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    foreach (PXEventSubscriberAttribute attribute in graph.Caches[typeof (TNode)].GetAttributes((string) null))
    {
      if (attribute is IPXInterfaceField pxInterfaceField && (pxInterfaceField.Visibility & PXUIVisibility.SelectorVisible) == PXUIVisibility.SelectorVisible)
      {
        this.fieldName = attribute.FieldName;
        break;
      }
    }
  }

  [PXUIField(DisplayName = "Save", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXSaveButton]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    PXSavePerRow<TNode> pxSavePerRow = this;
    PXCache cache = adapter.View.Cache;
    if (!cache.AllowUpdate)
      throw new PXException("The record cannot be saved.");
    pxSavePerRow._Graph.RaiseBeforePersist();
    List<System.Type> typeList = new List<System.Type>();
    bool isAborted = false;
    try
    {
      using (PXTransactionScope transactionScope1 = new PXTransactionScope())
      {
        for (int index = 0; index < pxSavePerRow._Graph.Views.Caches.Count; ++index)
        {
          System.Type cach = pxSavePerRow._Graph.Views.Caches[index];
          if (!typeList.Contains(cach))
          {
            typeList.Add(cach);
            if (pxSavePerRow.fieldName == null || cach != typeof (TNode))
            {
              pxSavePerRow._Graph.Persist(cach, PXDBOperation.Insert);
            }
            else
            {
              foreach (TNode node in cache.Inserted)
              {
                try
                {
                  cache.PersistInserted((object) node);
                }
                catch (PXLockViolationException ex)
                {
                  cache.SetStatus((object) node, PXEntryStatus.Notchanged);
                  ex.SetMessage("The record already exists in the system and cannot be added.");
                  cache.RaiseExceptionHandling(pxSavePerRow.fieldName, (object) node, cache.GetValue((object) node, pxSavePerRow.fieldName), (Exception) ex);
                }
              }
            }
          }
        }
        for (int index = 0; index < typeList.Count; ++index)
          pxSavePerRow._Graph.Persist(typeList[index], PXDBOperation.Update);
        for (int index = typeList.Count - 1; index >= 0; --index)
        {
          if (pxSavePerRow.fieldName == null || typeList[index] != typeof (TNode))
          {
            pxSavePerRow._Graph.Persist(typeList[index], PXDBOperation.Delete);
          }
          else
          {
            foreach (TNode node in cache.Deleted)
            {
              PXTransactionScope.GetTransaction();
              using (new PXConnectionScope())
              {
                using (PXTransactionScope transactionScope2 = new PXTransactionScope())
                {
                  try
                  {
                    cache.PersistDeleted((object) node);
                    transactionScope2.Complete();
                  }
                  catch (PXDatabaseException ex)
                  {
                    cache.SetStatus((object) node, PXEntryStatus.Notchanged);
                    if (ex.IsFriendlyMessage)
                      ex.SetMessage(ex.Message);
                    else
                      ex.SetMessage("The record is used elsewhere in the system and cannot be deleted.");
                    cache.RaiseExceptionHandling(pxSavePerRow.fieldName, (object) node, cache.GetValue((object) node, pxSavePerRow.fieldName), (Exception) ex);
                  }
                }
              }
            }
          }
        }
        pxSavePerRow._Graph.RaiseBeforeCommit();
        transactionScope1.Complete();
        transactionScope1.LicensePolicy.RegisterErpTransaction(pxSavePerRow._Graph, true);
      }
    }
    catch (Exception ex)
    {
      isAborted = true;
      throw;
    }
    finally
    {
      for (int index = 0; index < pxSavePerRow._Graph.Views.Caches.Count; ++index)
      {
        System.Type cach = pxSavePerRow._Graph.Views.Caches[index];
        if (typeList.Contains(cach))
        {
          typeList.Remove(cach);
          pxSavePerRow._Graph.Caches[cach].Persisted(isAborted);
        }
      }
    }
    pxSavePerRow._Graph.TimeStamp = (byte[]) null;
    foreach (PXView pxView in new List<PXView>((IEnumerable<PXView>) pxSavePerRow._Graph.Views.Values))
      pxView.Clear();
    pxSavePerRow._Graph.SelectTimeStamp();
    foreach (object obj in adapter.Get())
      yield return obj;
    pxSavePerRow._Graph.RaiseAfterPersist();
  }
}
