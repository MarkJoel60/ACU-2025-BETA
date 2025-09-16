// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.BaseVendorRefNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

public abstract class BaseVendorRefNbrAttribute : 
  PXEventSubscriberAttribute,
  IPXRowUpdatedSubscriber,
  IPXFieldVerifyingSubscriber,
  IPXRowDeletedSubscriber,
  IPXRowInsertedSubscriber,
  IPXRowPersistedSubscriber
{
  protected int DETAIL_DUMMY;
  protected System.Type _VendorID;
  protected PXGraph _Graph;
  protected PXCache _Cache;
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2020 R2.")]
  protected List<APVendorRefNbr> _ToCheck;
  private EntityHelper _EntityHelper;

  protected PXCache Cache
  {
    get
    {
      if (this._Cache == null)
        this._Cache = this._Graph.Caches[typeof (APVendorRefNbr)];
      return this._Cache;
    }
  }

  protected EntityHelper EntityHelper
  {
    get
    {
      if (this._EntityHelper == null)
        this._EntityHelper = new EntityHelper(this._Graph);
      return this._EntityHelper;
    }
  }

  public BaseVendorRefNbrAttribute(System.Type VendorIDField) => this._VendorID = VendorIDField;

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._Graph = sender.Graph;
    this._Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this._VendorID), this._VendorID.Name, new PXFieldUpdated(this.DataUpdatedCheck));
  }

  public void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!e.ExternalCall || string.IsNullOrEmpty(Convert.ToString(e.NewValue)))
      return;
    object copy = sender.CreateCopy(e.Row);
    sender.SetValue(copy, this._FieldName, e.NewValue);
    try
    {
      this.CheckUniqueness(sender, copy);
      sender.RaiseExceptionHandling(this._FieldName, e.Row, (object) null, (Exception) null);
    }
    catch (PXSetPropertyException ex)
    {
      if (this.IsRequiredUniqueRefNbr(sender, copy))
        sender.RaiseExceptionHandling(this._FieldName, e.Row, e.NewValue, (Exception) ex);
      else
        sender.RaiseExceptionHandling(this._FieldName, e.Row, e.NewValue, (Exception) new PXSetPropertyException(ex.Message, PXErrorLevel.Warning));
    }
  }

  protected void DataUpdatedCheck(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.FieldVerifying(sender, new PXFieldVerifyingEventArgs(e.Row, sender.GetValue(e.Row, this._FieldName), e.ExternalCall));
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (e.Row == null || sender.GetStatus(e.Row) == PXEntryStatus.Deleted || sender.GetStatus(e.Row) == PXEntryStatus.InsertedDeleted)
      return;
    APVendorRefNbr vrn = this.InitVendorRefObject(sender, e.Row);
    if (vrn == null)
      return;
    if (vrn.IsIgnored.GetValueOrDefault())
    {
      PXEntryStatus status = this.Cache.GetStatus((object) vrn);
      if (this.Cache.Locate((object) vrn) != null && status != PXEntryStatus.Deleted && status != PXEntryStatus.InsertedDeleted)
        this.Delete(vrn);
    }
    else
    {
      APVendorRefNbr apVendorRefNbr = (APVendorRefNbr) this.Cache.Update((object) vrn);
    }
    this._Graph.Caches[typeof (APVendorRefNbr)].IsDirty = false;
  }

  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (e.Row == null)
      return;
    APVendorRefNbr vrn = this.InitVendorRefObject(sender, e.Row);
    if (vrn == null)
      return;
    this.Delete(vrn);
    this._Graph.Caches[typeof (APVendorRefNbr)].IsDirty = false;
  }

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null || sender.GetStatus(e.Row) == PXEntryStatus.Deleted || sender.GetStatus(e.Row) == PXEntryStatus.InsertedDeleted)
      return;
    APVendorRefNbr apVendorRefNbr = this.InitVendorRefObject(sender, e.Row);
    if (apVendorRefNbr == null)
      return;
    this.Cache.Insert((object) apVendorRefNbr);
    APVendorRefNbr vrn = (APVendorRefNbr) this.Cache.Locate((object) apVendorRefNbr);
    if (vrn.IsIgnored ?? true)
      this.Delete(vrn);
    this._Graph.Caches[typeof (APVendorRefNbr)].IsDirty = false;
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    switch (e.TranStatus)
    {
      case PXTranStatus.Open:
        BaseVendorRefNbrAttribute.EntityKey ek = this.GetEntityKey(sender, e.Row);
        if (ek == null)
          break;
        List<(APVendorRefNbr, PXEntryStatus)> list1 = this.Cache.Cached.Cast<APVendorRefNbr>().Where<APVendorRefNbr>((Func<APVendorRefNbr, bool>) (x =>
        {
          Guid? masterId1 = x.MasterID;
          Guid? masterId2 = ek._MasterID;
          if (masterId1.HasValue != masterId2.HasValue)
            return false;
          return !masterId1.HasValue || masterId1.GetValueOrDefault() == masterId2.GetValueOrDefault();
        })).Select<APVendorRefNbr, (APVendorRefNbr, PXEntryStatus)>((Func<APVendorRefNbr, (APVendorRefNbr, PXEntryStatus)>) (x => (x, this.Cache.GetStatus((object) x)))).Where<(APVendorRefNbr, PXEntryStatus)>((Func<(APVendorRefNbr, PXEntryStatus), bool>) (x => EnumerableExtensions.IsIn<PXEntryStatus?>(new PXEntryStatus?(x.Status), new PXEntryStatus?(PXEntryStatus.Inserted), new PXEntryStatus?(PXEntryStatus.Updated), new PXEntryStatus?(PXEntryStatus.Deleted), new PXEntryStatus?(PXEntryStatus.InsertedDeleted)))).ToList<(APVendorRefNbr, PXEntryStatus)>();
        bool flag1 = false;
        if (list1.Any<(APVendorRefNbr, PXEntryStatus)>())
        {
          foreach (APVendorRefNbr vrn in list1.Where<(APVendorRefNbr, PXEntryStatus)>((Func<(APVendorRefNbr, PXEntryStatus), bool>) (x => EnumerableExtensions.IsIn<PXEntryStatus>(x.Status, PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted) || (x.Row.IsIgnored ?? true))).Select<(APVendorRefNbr, PXEntryStatus), APVendorRefNbr>((Func<(APVendorRefNbr, PXEntryStatus), APVendorRefNbr>) (x => x.Row)))
          {
            this.DeleteKey(vrn);
            flag1 = true;
          }
          List<APVendorRefNbr> list2 = list1.Where<(APVendorRefNbr, PXEntryStatus)>((Func<(APVendorRefNbr, PXEntryStatus), bool>) (x =>
          {
            if (!EnumerableExtensions.IsIn<PXEntryStatus>(x.Status, PXEntryStatus.Inserted, PXEntryStatus.Updated))
              return false;
            bool? isIgnored = x.Row.IsIgnored;
            bool flag2 = false;
            return isIgnored.GetValueOrDefault() == flag2 & isIgnored.HasValue;
          })).Select<(APVendorRefNbr, PXEntryStatus), APVendorRefNbr>((Func<(APVendorRefNbr, PXEntryStatus), APVendorRefNbr>) (x => x.Row)).ToList<APVendorRefNbr>();
          if (list2.Any<APVendorRefNbr>())
          {
            List<APVendorRefNbr> list3 = list2.Where<APVendorRefNbr>((Func<APVendorRefNbr, bool>) (x => x.IsChecked.GetValueOrDefault())).ToList<APVendorRefNbr>();
            if (list3.Any<APVendorRefNbr>())
            {
              this.Cache.ClearQueryCache();
              foreach (APVendorRefNbr vendorref in list3)
                this.CheckUniqueness(vendorref);
            }
            foreach (APVendorRefNbr vrn in list2)
              this.UpdateKey(vrn);
          }
        }
        if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Delete || flag1)
          break;
        this.DeleteKey(new APVendorRefNbr()
        {
          MasterID = ek._MasterID,
          DetailID = new int?(ek._DetailID)
        });
        break;
      case PXTranStatus.Completed:
        this.Cache.ClearQueryCache();
        this.Cache.Clear();
        break;
    }
  }

  protected virtual Guid? GetMasterNoteId(
    System.Type masterEntity,
    System.Type masterNoteField,
    object masterRow)
  {
    bool isDirty = this._Graph.Caches[typeof (Note)].IsDirty;
    Guid? noteId = PXNoteAttribute.GetNoteID<PX.Objects.PO.POReceipt.noteID>(this._Graph.Caches[masterEntity], masterRow);
    this._Graph.Caches[typeof (Note)].IsDirty = isDirty;
    return noteId;
  }

  protected virtual void UpdateKey(APVendorRefNbr vrn)
  {
    this.DeleteKey(vrn);
    vrn.CreatedByID = new Guid?(PXAccess.GetTrueUserID());
    vrn.CreatedByScreenID = this._Graph.Accessinfo?.ScreenID?.Replace(".", "") ?? "00000000";
    PXCommandPreparingEventArgs.FieldDescription description;
    this.Cache.RaiseCommandPreparing<APVendorRefNbr.createdDateTime>((object) vrn, (object) vrn.CreatedDateTime, PXDBOperation.Insert, typeof (APVendorRefNbr), out description);
    PXDatabase.Insert<APVendorRefNbr>((PXDataFieldAssign) new PXDataFieldAssign<APVendorRefNbr.masterID>((object) vrn.MasterID), (PXDataFieldAssign) new PXDataFieldAssign<APVendorRefNbr.detailID>((object) vrn.DetailID), (PXDataFieldAssign) new PXDataFieldAssign<APVendorRefNbr.vendorID>((object) vrn.VendorID), (PXDataFieldAssign) new PXDataFieldAssign<APVendorRefNbr.vendorDocumentID>((object) vrn.VendorDocumentID), (PXDataFieldAssign) new PXDataFieldAssign<APVendorRefNbr.siblingID>((object) vrn.SiblingID), (PXDataFieldAssign) new PXDataFieldAssign<APVendorRefNbr.createdByID>((object) vrn.CreatedByID), (PXDataFieldAssign) new PXDataFieldAssign<APVendorRefNbr.createdByScreenID>((object) vrn.CreatedByScreenID), (PXDataFieldAssign) new PXDataFieldAssign<APVendorRefNbr.createdDateTime>(description.DataType, description.DataLength, description.DataValue));
  }

  protected virtual void DeleteKey(APVendorRefNbr vrn)
  {
    PXDatabase.Delete<APVendorRefNbr>((PXDataFieldRestrict) new PXDataFieldRestrict<APVendorRefNbr.masterID>((object) vrn.MasterID), (PXDataFieldRestrict) new PXDataFieldRestrict<APVendorRefNbr.detailID>((object) vrn.DetailID));
  }

  protected virtual void Delete(APVendorRefNbr vrn)
  {
    PXEntryStatus status = this.Cache.GetStatus((object) vrn);
    if (EnumerableExtensions.IsIn<PXEntryStatus>(status, PXEntryStatus.InsertedDeleted, PXEntryStatus.Deleted))
      return;
    this.Cache.SetStatus((object) vrn, status == PXEntryStatus.Inserted ? PXEntryStatus.InsertedDeleted : PXEntryStatus.Deleted);
  }

  protected virtual APVendorRefNbr InitVendorRefObject(PXCache sender, object row)
  {
    APVendorRefNbr apVendorRefNbr1 = new APVendorRefNbr();
    BaseVendorRefNbrAttribute.EntityKey entityKey = this.GetEntityKey(sender, row);
    if (entityKey == null)
      return (APVendorRefNbr) null;
    apVendorRefNbr1.MasterID = entityKey._MasterID;
    apVendorRefNbr1.DetailID = new int?(entityKey._DetailID);
    APVendorRefNbr apVendorRefNbr2 = (APVendorRefNbr) PXSelectBase<APVendorRefNbr, PXSelect<APVendorRefNbr, Where<APVendorRefNbr.masterID, Equal<Current<APVendorRefNbr.masterID>>, And<APVendorRefNbr.detailID, Equal<Current<APVendorRefNbr.detailID>>>>>.Config>.SelectSingleBound(this._Graph, new object[1]
    {
      (object) apVendorRefNbr1
    });
    if (apVendorRefNbr2 == null)
      apVendorRefNbr2 = apVendorRefNbr1;
    else
      PXCache<APVendorRefNbr>.StoreOriginal(this._Graph, apVendorRefNbr2);
    apVendorRefNbr2.SiblingID = this.GetSiblingID(sender, row);
    apVendorRefNbr2.IsIgnored = new bool?(this.IsIgnored(sender, row));
    apVendorRefNbr2.IsChecked = new bool?(this.IsRequiredUniqueRefNbr(sender, row));
    apVendorRefNbr2.VendorID = (int?) sender.GetValue(row, this._VendorID.Name);
    apVendorRefNbr2.VendorDocumentID = Convert.ToString(sender.GetValue(row, this._FieldName));
    return apVendorRefNbr2;
  }

  protected virtual void CheckUniqueness(PXCache sender, object row)
  {
    this.CheckUniqueness(this.InitVendorRefObject(sender, row));
  }

  protected virtual void CheckUniqueness(APVendorRefNbr vendorref)
  {
    if (!vendorref.SiblingID.HasValue || !vendorref.MasterID.HasValue || !vendorref.DetailID.HasValue || string.IsNullOrEmpty(vendorref.VendorDocumentID) || !vendorref.VendorID.HasValue)
      return;
    APVendorRefNbr row = (APVendorRefNbr) PXSelectBase<APVendorRefNbr, PXSelect<APVendorRefNbr, Where<APVendorRefNbr.vendorID, Equal<Current<APVendorRefNbr.vendorID>>, And<APVendorRefNbr.vendorDocumentID, Equal<Current<APVendorRefNbr.vendorDocumentID>>, And<APVendorRefNbr.siblingID, NotEqual<Current<APVendorRefNbr.siblingID>>, And<APVendorRefNbr.isIgnored, NotEqual<True>, And<Where<APVendorRefNbr.masterID, NotEqual<Current<APVendorRefNbr.masterID>>, Or<APVendorRefNbr.detailID, NotEqual<Current<APVendorRefNbr.detailID>>>>>>>>>>.Config>.SelectSingleBound(this._Graph, new object[1]
    {
      (object) vendorref
    });
    if (row != null)
      throw new PXSetPropertyException(this.GetExceptionMessage(row));
  }

  protected virtual string GetExceptionMessage(APVendorRefNbr row)
  {
    int? detailId = row.DetailID;
    int detailDummy = this.DETAIL_DUMMY;
    return detailId.GetValueOrDefault() == detailDummy & detailId.HasValue ? PXMessages.LocalizeFormatNoPrefixNLA("This vendor ref. number \"{0}\" has already been used for the document \"{1}\".", (object) row.VendorDocumentID, (object) this.EntityHelper.GetEntityRowID(row.MasterID)) : PXMessages.LocalizeFormatNoPrefixNLA("This vendor ref. number \"{0}\" has already been used for the landed cost document (line number \"{1}\") linked to the purchase receipt \"{2}\".", (object) row.VendorDocumentID, (object) row.DetailID.ToString(), (object) this.EntityHelper.GetEntityRowID(row.MasterID));
  }

  public abstract Guid? GetSiblingID(PXCache sender, object row);

  protected abstract BaseVendorRefNbrAttribute.EntityKey GetEntityKey(PXCache sender, object row);

  protected virtual bool IsIgnored(PXCache sender, object row)
  {
    return string.IsNullOrEmpty(Convert.ToString(sender.GetValue(row, this._FieldName))) || !((int?) sender.GetValue(row, this._VendorID.Name)).HasValue;
  }

  protected virtual bool IsRequiredUniqueRefNbr(PXCache sender, object row)
  {
    PXCache cach = sender.Graph.Caches[typeof (APSetup)];
    return !this.IsIgnored(sender, row) && ((bool?) cach.GetValue<APSetup.raiseErrorOnDoubleInvoiceNbr>(cach.Current)).GetValueOrDefault();
  }

  protected class EntityKey
  {
    public Guid? _MasterID;
    public int _DetailID;
  }
}
