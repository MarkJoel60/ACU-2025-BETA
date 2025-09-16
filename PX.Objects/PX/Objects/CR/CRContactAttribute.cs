// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRContactAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// Abstract class is needed to handle the case when we have priorities for sources (e.g. Location -&gt; BAccount -&gt; Contact) and the source is optional
/// </summary>
public abstract class CRContactAttribute : ContactAttribute, IPXRowUpdatedSubscriber
{
  private BqlCommand _DuplicateSelect = BqlCommand.CreateInstance(new System.Type[1]
  {
    typeof (Select<CRContact, Where<CRContact.bAccountID, Equal<PX.Data.Required<CRContact.bAccountID>>, And<CRContact.bAccountContactID, Equal<PX.Data.Required<CRContact.bAccountContactID>>, And<CRContact.revisionID, Equal<PX.Data.Required<CRContact.revisionID>>, And<CRContact.isDefaultContact, Equal<boolTrue>>>>>>)
  });

  public CRContactAttribute(System.Type AddressIDType, System.Type IsDefaultAddressType, System.Type SelectType)
    : base(AddressIDType, IsDefaultAddressType, SelectType)
  {
  }

  public override void DefaultContact<TContact, TContactID>(
    PXCache sender,
    object DocumentRow,
    object ContactRow)
  {
    (PXView pxView, object[] objArray) = this.GetViewWithParameters(sender, DocumentRow, ContactRow);
    if (pxView != null)
    {
      int num1 = -1;
      int num2 = 0;
      bool flag = false;
      using (List<object>.Enumerator enumerator = pxView.Select(new object[1]
      {
        DocumentRow
      }, objArray, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref num1, 1, ref num2).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult current = (PXResult) enumerator.Current;
          TContact contact = current.GetItem<TContact>();
          if ((object) contact != null)
          {
            PXCache<TContact> pxCache = GraphHelper.Caches<TContact>(sender.Graph);
            PXEntryStatus status = pxCache.GetStatus(contact);
            if (status == 3)
            {
              ((PXCache) pxCache).SetStatus((object) contact, (PXEntryStatus) 0);
              ((PXCache) pxCache).ClearQueryCache();
            }
            if (status == 4)
            {
              ((PXCache) pxCache).SetStatus((object) contact, (PXEntryStatus) 2);
              ((PXCache) pxCache).ClearQueryCache();
            }
          }
          flag = ContactAttribute.DefaultContact<TContact, TContactID>(sender, this.FieldName, DocumentRow, ContactRow, (object) current);
        }
      }
      if (flag || this._Required)
        return;
      this.ClearRecord(sender, DocumentRow);
    }
    else
    {
      this.ClearRecord(sender, DocumentRow);
      if (!this._Required)
        return;
      using (new ReadOnlyScope(new PXCache[1]
      {
        sender.Graph.Caches[this._RecordType]
      }))
      {
        object obj1 = sender.Graph.Caches[this._RecordType].Insert();
        object obj2 = sender.Graph.Caches[this._RecordType].GetValue(obj1, this._RecordID);
        sender.SetValue(DocumentRow, this._FieldOrdinal, obj2);
      }
    }
  }

  protected abstract (PXView, object[]) GetViewWithParameters(
    PXCache sender,
    object DocumentRow,
    object ContactRow);

  public override void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    bool isDirty = sender.IsDirty;
    base.RowInserted(sender, e);
    sender.IsDirty = isDirty;
  }

  public override void Record_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 2 && ((CRContact) e.Row).IsDefaultContact.GetValueOrDefault())
    {
      PXView view = sender.Graph.TypedViews.GetView(this._DuplicateSelect, true);
      view.Clear();
      CRContact crContact = (CRContact) view.SelectSingle(new object[3]
      {
        (object) ((CRContact) e.Row).BAccountID,
        (object) ((CRContact) e.Row).BAccountContactID,
        (object) ((CRContact) e.Row).RevisionID
      });
      if (crContact != null)
      {
        this._KeyToAbort = sender.GetValue(e.Row, this._RecordID);
        object obj1 = sender.Graph.Caches[typeof (CRContact)].GetValue((object) crContact, this._RecordID);
        PXCache cach = sender.Graph.Caches[this._ItemType];
        foreach (object obj2 in cach.Updated)
        {
          if (object.Equals(this._KeyToAbort, cach.GetValue(obj2, this._FieldOrdinal)))
            cach.SetValue(obj2, this._FieldOrdinal, obj1);
        }
        this._KeyToAbort = (object) null;
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
    }
    else if ((e.Operation & 3) == 3)
    {
      Guid? nullable;
      switch (sender.Graph.Caches[this._BqlTable].Current)
      {
        case CROpportunity crOpportunity:
          nullable = crOpportunity.DefQuoteID;
          break;
        case CRQuote crQuote:
          nullable = crQuote.QuoteID;
          break;
        case CROpportunityRevision opportunityRevision:
          nullable = opportunityRevision.NoteID;
          break;
        case PMQuote pmQuote:
          nullable = pmQuote.QuoteID;
          break;
        default:
          nullable = new Guid?();
          break;
      }
      if (nullable.HasValue)
      {
        object obj = sender.GetValue(e.Row, this._RecordID);
        if (((IQueryable<PXResult<CROpportunityRevision>>) PXSelectBase<CROpportunityRevision, PXViewOf<CROpportunityRevision>.BasedOn<SelectFromBase<CROpportunityRevision, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CROpportunityRevision.noteID, NotEqual<P.AsGuid>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlOperand<CROpportunityRevision.shipContactID, IBqlInt>.IsEqual<P.AsInt>>>, Or<BqlOperand<CROpportunityRevision.billContactID, IBqlInt>.IsEqual<P.AsInt>>>>.Or<BqlOperand<CROpportunityRevision.opportunityContactID, IBqlInt>.IsEqual<P.AsInt>>>>>.ReadOnly.Config>.Select(sender.Graph, new object[4]
        {
          (object) nullable,
          obj,
          obj,
          obj
        })).Any<PXResult<CROpportunityRevision>>())
        {
          ((CancelEventArgs) e).Cancel = true;
          GraphHelper.Hold(sender, e.Row);
        }
      }
    }
    base.Record_RowPersisting(sender, e);
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object objA = sender.GetValue(e.Row, this._FieldOrdinal);
    if (objA != null)
    {
      PXCache cach = sender.Graph.Caches[this._RecordType];
      if (Convert.ToInt32(objA) < 0)
      {
        foreach (object obj1 in cach.Inserted)
        {
          object objB = cach.GetValue(obj1, this._RecordID);
          if (object.Equals(objA, objB))
          {
            if (((CRContact) obj1).IsDefaultContact.GetValueOrDefault())
            {
              PXView view = sender.Graph.TypedViews.GetView(this._DuplicateSelect, true);
              view.Clear();
              CRContact crContact = (CRContact) view.SelectSingle(new object[3]
              {
                (object) ((CRContact) obj1).BAccountID,
                (object) ((CRContact) obj1).BAccountContactID,
                (object) ((CRContact) obj1).RevisionID
              });
              if (crContact != null)
              {
                this._KeyToAbort = sender.GetValue(e.Row, this._FieldOrdinal);
                object obj2 = sender.Graph.Caches[typeof (CRContact)].GetValue((object) crContact, this._RecordID);
                sender.SetValue(e.Row, this._FieldOrdinal, obj2);
                break;
              }
              break;
            }
            break;
          }
        }
      }
    }
    base.RowPersisting(sender, e);
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!this._Required || sender.GetValue(e.Row, this._FieldOrdinal) != null)
      return;
    using (new ReadOnlyScope(new PXCache[1]
    {
      sender.Graph.Caches[this._RecordType]
    }))
    {
      object obj1 = sender.Graph.Caches[this._RecordType].Insert();
      object obj2 = sender.Graph.Caches[this._RecordType].GetValue(obj1, this._RecordID);
      sender.SetValue(e.Row, this._FieldOrdinal, obj2);
    }
  }

  protected override void Record_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    bool? nullable1 = (bool?) sender.GetValue(e.Row, this._IsDefault);
    PXUIFieldAttribute.SetVisible(sender, (object) null, this._RecordID, false);
    if (sender.GetAttributes<CRContact.overrideContact>(e.Row).OfType<PXUIFieldAttribute>().Any<PXUIFieldAttribute>((Func<PXUIFieldAttribute, bool>) (field => !field.Enabled)))
      return;
    PXCache pxCache = sender;
    object row = e.Row;
    bool? nullable2 = nullable1;
    bool flag = false;
    int num = !(nullable2.GetValueOrDefault() == flag & nullable2.HasValue) ? 0 : (sender.AllowUpdate ? 1 : 0);
    PXUIFieldAttribute.SetEnabled(pxCache, row, num != 0);
    PXUIFieldAttribute.SetEnabled(sender, e.Row, this._IsDefault, sender.AllowUpdate);
    PXUIFieldAttribute.SetEnabled<CRContact.overrideContact>(sender, e.Row, true);
  }

  public override void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
  }
}
