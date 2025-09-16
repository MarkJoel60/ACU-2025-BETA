// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashTranIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

public abstract class CashTranIDAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber,
  IPXRowDeletedSubscriber
{
  protected object _KeyToAbort;
  protected Type _ChildType;
  private object _SelfKeyToAbort;
  private Dictionary<long?, object> _persisted;

  public abstract CATran DefaultValues(PXCache sender, CATran catran_Row, object orig_Row);

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._ChildType = sender.GetItemType();
    this._persisted = new Dictionary<long?, object>();
    PXGraph.RowPersistingEvents rowPersisting = sender.Graph.RowPersisting;
    CashTranIDAttribute cashTranIdAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) cashTranIdAttribute1, __vmethodptr(cashTranIdAttribute1, CATran_RowPersisting));
    rowPersisting.AddHandler<CATran>(pxRowPersisting);
    PXGraph.RowPersistedEvents rowPersisted = sender.Graph.RowPersisted;
    CashTranIDAttribute cashTranIdAttribute2 = this;
    // ISSUE: virtual method pointer
    PXRowPersisted pxRowPersisted = new PXRowPersisted((object) cashTranIdAttribute2, __vmethodptr(cashTranIdAttribute2, CATran_RowPersisted));
    rowPersisted.AddHandler<CATran>(pxRowPersisted);
  }

  public virtual void CATran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation != 2)
      return;
    this._SelfKeyToAbort = sender.GetValue<CATran.tranID>(e.Row);
  }

  public virtual void CATran_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    PXCache cach = sender.Graph.Caches[this._ChildType];
    if (e.Operation == 2 && e.TranStatus == null && this._SelfKeyToAbort != null)
    {
      long? key = (long?) sender.GetValue<CATran.tranID>(e.Row);
      if (!this._persisted.ContainsKey(key))
        this._persisted.Add(key, this._SelfKeyToAbort);
      foreach (object obj in cach.Inserted)
      {
        long? nullable = (long?) cach.GetValue(obj, this._FieldOrdinal);
        long? selfKeyToAbort = (long?) this._SelfKeyToAbort;
        if (nullable.GetValueOrDefault() == selfKeyToAbort.GetValueOrDefault() & nullable.HasValue == selfKeyToAbort.HasValue)
          cach.SetValue(obj, this._FieldOrdinal, (object) key);
      }
      foreach (object obj in cach.Updated)
      {
        long? nullable = (long?) cach.GetValue(obj, this._FieldOrdinal);
        long? selfKeyToAbort = (long?) this._SelfKeyToAbort;
        if (nullable.GetValueOrDefault() == selfKeyToAbort.GetValueOrDefault() & nullable.HasValue == selfKeyToAbort.HasValue)
          cach.SetValue(obj, this._FieldOrdinal, (object) key);
      }
      this._SelfKeyToAbort = (object) null;
    }
    if (e.Operation == 2 && e.TranStatus == 2)
    {
      foreach (object obj in cach.Inserted)
      {
        long? key;
        if ((key = (long?) cach.GetValue(obj, this._FieldOrdinal)).HasValue && this._persisted.TryGetValue(key, out this._SelfKeyToAbort))
          cach.SetValue(obj, this._FieldOrdinal, this._SelfKeyToAbort);
      }
      foreach (object obj in cach.Updated)
      {
        long? key;
        if ((key = (long?) cach.GetValue(obj, this._FieldOrdinal)).HasValue && this._persisted.TryGetValue(key, out this._SelfKeyToAbort))
          cach.SetValue(obj, this._FieldOrdinal, this._SelfKeyToAbort);
      }
    }
    if (e.TranStatus == null)
      return;
    this._KeyToAbort = (object) null;
    this._SelfKeyToAbort = (object) null;
  }

  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!sender.Graph.Views.Caches.Contains(typeof (CATran)))
      return;
    object obj = sender.GetValue(e.Row, this._FieldOrdinal);
    PXCache cach = sender.Graph.Caches[typeof (CATran)];
    if (obj == null)
      return;
    CATran caTran = new CATran() { TranID = (long?) obj };
    cach.Delete((object) caTran);
    sender.SetValue(e.Row, this._FieldOrdinal, (object) null);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object key = sender.GetValue(e.Row, this._FieldOrdinal);
    PXCache cach = sender.Graph.Caches[typeof (CATran)];
    CATran caTran1 = (CATran) null;
    object obj;
    if (key != null)
    {
      if ((caTran1 = PXResultset<CATran>.op_Implicit(CashTranIDAttribute.GetCATranByKey(sender, key))) != null)
      {
        CATran caTran2 = (CATran) cach.Locate((object) caTran1);
        if (caTran2 != null)
        {
          if (caTran2.OrigModule != null && caTran1.OrigModule != null && caTran2.OrigModule != caTran1.OrigModule || caTran2.OrigRefNbr != null && caTran1.OrigRefNbr != null && caTran2.OrigRefNbr != caTran1.OrigRefNbr || caTran2.OrigTranType != null && caTran1.OrigTranType != null && caTran2.OrigTranType != caTran1.OrigTranType)
            throw new PXException("Attempt to rewrite existing CATran was detected. Please contact support for help.");
          if (cach.GetStatus((object) caTran2) == null)
            PXCache<CATran>.RestoreCopy(caTran2, caTran1);
          caTran1 = caTran2;
        }
        else
          cach.SetStatus((object) caTran1, (PXEntryStatus) 0);
      }
      if ((long) key < 0L && caTran1 == null)
        caTran1 = (CATran) cach.Locate((object) new CATran()
        {
          TranID = new long?((long) key)
        });
      if (caTran1 == null)
      {
        obj = (object) null;
        sender.SetValue(e.Row, this._FieldOrdinal, (object) null);
      }
    }
    if ((e.Operation & 3) == 3)
    {
      if (caTran1 != null)
      {
        cach.Delete((object) caTran1);
        cach.PersistDeleted((object) caTran1);
      }
    }
    else if (caTran1 == null)
    {
      if (!this.NeedPreventCashTransactionCreation(sender, e.Row))
      {
        CATran caTran3 = this.DefaultValues(sender, new CATran(), e.Row);
        if (caTran3 != null)
        {
          CATran caTran4 = (CATran) cach.Insert((object) caTran3);
          long? tranId = caTran4.TranID;
          long num = 0;
          if (!(tranId.GetValueOrDefault() < num & tranId.HasValue))
            throw new PXException("Attempt to rewrite existing CATran was detected. Please contact support for help.");
          sender.SetValue(e.Row, this._FieldOrdinal, (object) caTran4.TranID);
          this._KeyToAbort = (object) caTran4.TranID;
          cach.PersistInserted((object) caTran4);
          tranId = caTran4.TranID;
          long valueOrDefault = tranId.GetValueOrDefault();
          if (valueOrDefault == 0L || valueOrDefault < 0L)
            throw new PXException("An error occurred while saving CATran for the table '{0}'", new object[1]
            {
              (object) sender.GetItemType().Name
            });
          sender.SetValue(e.Row, this._FieldOrdinal, (object) valueOrDefault);
          caTran4.TranID = new long?(valueOrDefault);
          cach.Normalize();
        }
      }
    }
    else
    {
      long? tranId = caTran1.TranID;
      long num = 0;
      if (tranId.GetValueOrDefault() < num & tranId.HasValue)
      {
        PXCache<CATran>.StoreOriginal(sender.Graph, caTran1);
        CATran caTran5 = this.DefaultValues(sender, PXCache<CATran>.CreateCopy(caTran1), e.Row);
        if (caTran5 != null)
        {
          CATran caTran6 = (CATran) cach.Update((object) caTran5);
          sender.SetValue(e.Row, this._FieldOrdinal, (object) caTran6.TranID);
          this._KeyToAbort = (object) caTran6.TranID;
          cach.PersistInserted((object) caTran6);
          tranId = caTran6.TranID;
          long valueOrDefault = tranId.GetValueOrDefault();
          if (valueOrDefault == 0L || valueOrDefault < 0L)
            throw new PXException("An error occurred while saving CATran for the table '{0}'", new object[1]
            {
              (object) sender.GetItemType().Name
            });
          sender.SetValue(e.Row, this._FieldOrdinal, (object) valueOrDefault);
          caTran6.TranID = new long?(valueOrDefault);
          cach.Normalize();
        }
      }
      else
      {
        CATran copy = PXCache<CATran>.CreateCopy(caTran1);
        CATran caTran7 = this.DefaultValues(sender, copy, e.Row);
        if (caTran7 != null)
        {
          if (caTran7.OrigModule != null && caTran1.OrigModule != null && caTran7.OrigModule != caTran1.OrigModule || caTran7.OrigRefNbr != null && caTran1.OrigRefNbr != null && caTran7.OrigRefNbr != caTran1.OrigRefNbr || caTran7.OrigTranType != null && caTran1.OrigTranType != null && caTran7.OrigTranType != caTran1.OrigTranType)
            throw new PXException("Attempt to rewrite existing CATran was detected. Please contact support for help.");
          CATran caTran8 = (CATran) cach.Update((object) caTran7);
          cach.PersistUpdated((object) caTran8);
        }
        else
        {
          bool? released = caTran1.Released;
          bool flag = false;
          if (released.GetValueOrDefault() == flag & released.HasValue)
          {
            obj = (object) null;
            sender.SetValue(e.Row, this._FieldOrdinal, (object) null);
            cach.Delete((object) caTran1);
          }
        }
      }
    }
    foreach (CATran caTran9 in cach.Deleted)
      cach.PersistDeleted((object) caTran9);
  }

  private static PXResultset<CATran> GetCATranByKey(PXCache sender, object key)
  {
    using (new PXReadBranchRestrictedScope())
      return PXSelectBase<CATran, PXSelectReadonly<CATran, Where<CATran.tranID, Equal<Required<CATran.tranID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        key
      });
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    PXCache cach = sender.Graph.Caches[typeof (CATran)];
    if (e.TranStatus == null)
      sender.GetValue(e.Row, this._FieldOrdinal);
    else if (e.TranStatus == 2)
    {
      object objA = sender.GetValue(e.Row, this._FieldOrdinal);
      if (this._KeyToAbort != null && (long) this._KeyToAbort < 0L)
      {
        sender.SetValue(e.Row, this._FieldOrdinal, this._KeyToAbort);
        foreach (CATran caTran in cach.Inserted)
        {
          if (object.Equals(objA, (object) caTran.TranID))
          {
            caTran.TranID = new long?((long) this._KeyToAbort);
            cach.ResetPersisted((object) caTran);
            break;
          }
        }
      }
      else
      {
        foreach (CATran caTran in cach.Updated)
        {
          if (object.Equals(objA, (object) caTran.TranID))
            cach.ResetPersisted((object) caTran);
        }
      }
      cach.Normalize();
    }
    else
    {
      object objA = sender.GetValue(e.Row, this._FieldOrdinal);
      foreach (CATran caTran in cach.Inserted)
      {
        if (object.Equals(objA, (object) caTran.TranID))
        {
          cach.RaiseRowPersisted((object) caTran, (PXDBOperation) 2, e.TranStatus, e.Exception);
          cach.SetStatus((object) caTran, (PXEntryStatus) 0);
          PXTimeStampScope.PutPersisted(cach, (object) caTran, new object[1]
          {
            (object) sender.Graph.TimeStamp
          });
          cach.ResetPersisted((object) caTran);
        }
      }
      foreach (CATran caTran in cach.Updated)
      {
        if (object.Equals(objA, (object) caTran.TranID))
        {
          cach.RaiseRowPersisted((object) caTran, (PXDBOperation) 1, e.TranStatus, e.Exception);
          cach.SetStatus((object) caTran, (PXEntryStatus) 0);
          PXTimeStampScope.PutPersisted(cach, (object) caTran, new object[1]
          {
            (object) sender.Graph.TimeStamp
          });
          cach.ResetPersisted((object) caTran);
        }
      }
      foreach (CATran caTran in cach.Deleted)
      {
        cach.RaiseRowPersisted((object) caTran, (PXDBOperation) 3, e.TranStatus, e.Exception);
        cach.SetStatus((object) caTran, (PXEntryStatus) 0);
        PXTimeStampScope.PutPersisted(cach, (object) caTran, new object[1]
        {
          (object) sender.Graph.TimeStamp
        });
        cach.ResetPersisted((object) caTran);
      }
      cach.IsDirty = false;
      cach.Normalize();
    }
  }

  protected static void SetCleared(CATran catran, CashAccount cashAccount)
  {
    if (cashAccount == null)
      return;
    bool? reconcile = cashAccount.Reconcile;
    bool flag = false;
    if (!(reconcile.GetValueOrDefault() == flag & reconcile.HasValue) || catran.Cleared.GetValueOrDefault() && catran.TranDate.HasValue)
      return;
    catran.Cleared = new bool?(true);
    catran.ClearDate = catran.TranDate;
  }

  protected virtual void SetOriginalTransactionVoided(
    PXCache sender,
    long? tranID,
    CATran voidingTran)
  {
    if (!voidingTran.Released.GetValueOrDefault())
      return;
    PXCache cach = sender.Graph.Caches[typeof (CATran)];
    CATran caTran = PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Required<CATran.tranID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) tranID
    }));
    caTran.Voided = new bool?(true);
    cach.Update((object) caTran);
    cach.PersistUpdated((object) caTran);
  }

  protected static CashAccount GetCashAccount(CATran catran, PXGraph graph)
  {
    return (CashAccount) ((PXSelectBase) new PXSelectReadonly<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>(graph)).View.SelectSingle(new object[1]
    {
      (object) catran.CashAccountID
    });
  }

  /// <summary>
  /// Returns <c>true</c> if, during parent row persist, the
  /// corresponding cash transaction row should not be created, e.g.
  /// in case of a document that is part of a recurring schedule.
  /// </summary>
  protected virtual bool NeedPreventCashTransactionCreation(PXCache sender, object row) => false;

  public static void SetPeriodsByMaster(PXCache docCache, CATran caTran, string masterPeriodID)
  {
    FinPeriodIDAttribute.SetPeriodsByMaster<CATran.finPeriodID>(docCache.Graph.Caches[typeof (CATran)], (object) caTran, masterPeriodID);
  }
}
