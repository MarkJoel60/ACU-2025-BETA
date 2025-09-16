// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRAddressAttribute
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
public abstract class CRAddressAttribute : AddressAttribute, IPXRowUpdatedSubscriber
{
  private BqlCommand _DuplicateSelect = BqlCommand.CreateInstance(new System.Type[1]
  {
    typeof (Select<CRAddress, Where<CRAddress.bAccountID, Equal<PX.Data.Required<CRAddress.bAccountID>>, And<CRAddress.bAccountAddressID, Equal<PX.Data.Required<CRAddress.bAccountAddressID>>, And<CRAddress.revisionID, Equal<PX.Data.Required<CRAddress.revisionID>>, And<CRAddress.isDefaultAddress, Equal<boolTrue>>>>>>)
  });

  public CRAddressAttribute(System.Type AddressIDType, System.Type IsDefaultAddressType, System.Type SelectType)
    : base(AddressIDType, IsDefaultAddressType, SelectType)
  {
  }

  public override void DefaultAddress<TAddress, TAddressID>(
    PXCache sender,
    object DocumentRow,
    object AddressRow)
  {
    (PXView pxView, object[] objArray) = this.GetViewWithParameters(sender, DocumentRow, AddressRow);
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
          TAddress address = current.GetItem<TAddress>();
          if ((object) address != null)
          {
            PXCache<TAddress> pxCache = GraphHelper.Caches<TAddress>(sender.Graph);
            PXEntryStatus status = pxCache.GetStatus(address);
            if (status == 3)
            {
              ((PXCache) pxCache).SetStatus((object) address, (PXEntryStatus) 0);
              ((PXCache) pxCache).ClearQueryCache();
            }
            if (status == 4)
            {
              ((PXCache) pxCache).SetStatus((object) address, (PXEntryStatus) 2);
              ((PXCache) pxCache).ClearQueryCache();
            }
          }
          flag = AddressAttribute.DefaultAddress<TAddress, TAddressID>(sender, this.FieldName, DocumentRow, AddressRow, (object) current);
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
    bool isDirty = sender.Graph.Caches[this._RecordType].IsDirty;
    base.RowInserted(sender, e);
    sender.Graph.Caches[this._RecordType].IsDirty = isDirty;
  }

  public override void Record_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 2 && ((CRAddress) e.Row).IsDefaultAddress.GetValueOrDefault())
    {
      PXView view = sender.Graph.TypedViews.GetView(this._DuplicateSelect, true);
      view.Clear();
      CRAddress crAddress = (CRAddress) view.SelectSingle(new object[3]
      {
        (object) ((CRAddress) e.Row).BAccountID,
        (object) ((CRAddress) e.Row).BAccountAddressID,
        (object) ((CRAddress) e.Row).RevisionID
      });
      if (crAddress != null)
      {
        this._KeyToAbort = sender.GetValue(e.Row, this._RecordID);
        object obj1 = sender.Graph.Caches[typeof (CRAddress)].GetValue((object) crAddress, this._RecordID);
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
        if (((IQueryable<PXResult<CROpportunityRevision>>) PXSelectBase<CROpportunityRevision, PXViewOf<CROpportunityRevision>.BasedOn<SelectFromBase<CROpportunityRevision, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CROpportunityRevision.noteID, NotEqual<P.AsGuid>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlOperand<CROpportunityRevision.shipAddressID, IBqlInt>.IsEqual<P.AsInt>>>, Or<BqlOperand<CROpportunityRevision.billAddressID, IBqlInt>.IsEqual<P.AsInt>>>>.Or<BqlOperand<CROpportunityRevision.opportunityAddressID, IBqlInt>.IsEqual<P.AsInt>>>>>.ReadOnly.Config>.Select(sender.Graph, new object[4]
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
            if (((CRAddress) obj1).IsDefaultAddress.GetValueOrDefault())
            {
              PXView view = sender.Graph.TypedViews.GetView(this._DuplicateSelect, true);
              view.Clear();
              CRAddress crAddress = (CRAddress) view.SelectSingle(new object[3]
              {
                (object) ((CRAddress) obj1).BAccountID,
                (object) ((CRAddress) obj1).BAccountAddressID,
                (object) ((CRAddress) obj1).RevisionID
              });
              if (crAddress != null)
              {
                this._KeyToAbort = sender.GetValue(e.Row, this._FieldOrdinal);
                object obj2 = sender.Graph.Caches[typeof (CRAddress)].GetValue((object) crAddress, this._RecordID);
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
    if (sender.GetAttributes<CRAddress.overrideAddress>(e.Row).OfType<PXUIFieldAttribute>().Any<PXUIFieldAttribute>((Func<PXUIFieldAttribute, bool>) (field => !field.Enabled)))
      return;
    PXCache pxCache = sender;
    object row = e.Row;
    bool? nullable2 = nullable1;
    bool flag = false;
    int num = !(nullable2.GetValueOrDefault() == flag & nullable2.HasValue) ? 0 : (sender.AllowUpdate ? 1 : 0);
    PXUIFieldAttribute.SetEnabled(pxCache, row, num != 0);
    PXUIFieldAttribute.SetEnabled(sender, e.Row, this._IsDefault, sender.AllowUpdate);
    PXUIFieldAttribute.SetEnabled<CRAddress.overrideAddress>(sender, e.Row, true);
  }

  public override void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
  }
}
