// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.InvoiceNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public class InvoiceNbrAttribute : 
  PXEventSubscriberAttribute,
  IPXRowInsertedSubscriber,
  IPXRowUpdatedSubscriber,
  IPXRowPersistedSubscriber,
  IPXFieldVerifyingSubscriber
{
  protected Type _DocType;
  protected Type _NoteID;
  protected string[] _DocTypes;

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    PXNoteAttribute.GetNoteID(sender, e.Row, this._NoteID.Name);
    sender.Graph.Caches[typeof (Note)].IsDirty = false;
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PXNoteAttribute.GetNoteID(sender, e.Row, this._NoteID.Name);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    Guid? nullable = (Guid?) sender.GetValue(e.Row, this._NoteID.Name);
    string docType = this.GetDocType(sender, e.Row);
    if (string.IsNullOrEmpty((string) e.NewValue))
      return;
    if (PXResultset<ARInvoiceNbr>.op_Implicit(PXSelectBase<ARInvoiceNbr, PXSelectReadonly<ARInvoiceNbr, Where<ARInvoiceNbr.docType, Equal<Required<ARInvoiceNbr.docType>>, And<ARInvoiceNbr.refNbr, Equal<Required<ARInvoiceNbr.refNbr>>, And<ARInvoiceNbr.refNoteID, NotEqual<Required<ARInvoiceNbr.refNoteID>>>>>>.Config>.Select(sender.Graph, new object[3]
    {
      (object) docType,
      e.NewValue,
      (object) nullable
    })) != null)
      throw new PXSetPropertyException("Document with this Invoice Nbr. already exists.");
  }

  protected virtual bool DeleteNumber(string DocType, string RefNbr, Guid? RefNoteID)
  {
    return PXDatabase.Delete<ARInvoiceNbr>(new PXDataFieldRestrict[3]
    {
      new PXDataFieldRestrict(nameof (DocType), (object) DocType),
      new PXDataFieldRestrict(nameof (RefNbr), (PXDbType) 12, new int?(15), (object) RefNbr, (PXComp) 1),
      new PXDataFieldRestrict(nameof (RefNoteID), (object) RefNoteID)
    });
  }

  protected virtual bool SelectNumber(string DocType, string RefNbr, Guid? RefNoteID)
  {
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<ARInvoiceNbr>(new PXDataField[4]
    {
      new PXDataField(nameof (RefNoteID)),
      (PXDataField) new PXDataFieldValue(nameof (DocType), (object) DocType),
      (PXDataField) new PXDataFieldValue(nameof (RefNbr), (object) RefNbr),
      (PXDataField) new PXDataFieldValue(nameof (RefNoteID), (object) RefNoteID)
    }))
      return pxDataRecord != null;
  }

  protected virtual void InsertNumber(
    PXCache sender,
    string DocType,
    string RefNbr,
    Guid? RefNoteID)
  {
    ARInvoiceNbr arInvoiceNbr = new ARInvoiceNbr();
    PXCache cach = sender.Graph.Caches[typeof (ARInvoiceNbr)];
    List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
    foreach (string field in (List<string>) cach.Fields)
    {
      object obj = (object) null;
      switch (field)
      {
        case nameof (DocType):
          obj = (object) DocType;
          break;
        case nameof (RefNbr):
          obj = (object) RefNbr;
          break;
        case nameof (RefNoteID):
          obj = (object) RefNoteID;
          break;
        case "tstamp":
          continue;
        default:
          cach.RaiseFieldDefaulting(field, (object) arInvoiceNbr, ref obj);
          if (obj == null)
          {
            cach.RaiseRowInserting((object) arInvoiceNbr);
            obj = cach.GetValue((object) arInvoiceNbr, field);
            break;
          }
          break;
      }
      pxDataFieldAssignList.Add(new PXDataFieldAssign(field, obj));
    }
    PXDatabase.Insert<ARInvoiceNbr>(pxDataFieldAssignList.ToArray());
  }

  protected virtual bool DeleteOnUpdate(PXCache sender, PXRowPersistedEventArgs e)
  {
    return (e.Operation & 3) == 3;
  }

  protected virtual Guid? GetNoteID(PXCache sender, PXRowPersistedEventArgs e)
  {
    return (Guid?) sender.GetValue(e.Row, this._NoteID.Name);
  }

  protected virtual string GetNumber(PXCache sender, PXRowPersistedEventArgs e)
  {
    return (string) sender.GetValue(e.Row, this._FieldName);
  }

  protected virtual string GetDocType(PXCache sender, object data)
  {
    string docType = (string) sender.GetValue(data, this._DocType.Name);
    switch (docType)
    {
      case "INV":
      case "CSL":
        return "INV";
      case "CRM":
      case "DRM":
        return docType;
      default:
        return (string) null;
    }
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != null)
      return;
    Guid? noteId = this.GetNoteID(sender, e);
    string docType = this.GetDocType(sender, e.Row);
    string RefNbr = this.GetNumber(sender, e);
    bool flag = true;
    if (string.IsNullOrEmpty(docType))
      return;
    if ((e.Operation & 3) == 1 || (e.Operation & 3) == 3)
    {
      if (this.DeleteOnUpdate(sender, e))
      {
        this.DeleteNumber(docType, string.Empty, noteId);
        RefNbr = string.Empty;
      }
      else
        flag = this.DeleteNumber(docType, RefNbr ?? string.Empty, noteId);
    }
    if ((e.Operation & 3) != 1 && (e.Operation & 3) != 2 || string.IsNullOrEmpty(RefNbr) || !((e.Operation & 3) == 1 & flag) && this.SelectNumber(docType, RefNbr, noteId))
      return;
    try
    {
      this.InsertNumber(sender, docType, RefNbr, noteId);
    }
    catch (PXDatabaseException ex)
    {
      throw new PXRowPersistedException(this._FieldName, (object) RefNbr, "Document with this Invoice Nbr. already exists.");
    }
  }

  public InvoiceNbrAttribute(Type DocType, Type NoteID)
  {
    this._DocType = DocType;
    this._NoteID = NoteID;
    this._DocTypes = new ARDocType.SOEntryListAttribute().AllowedValues;
  }
}
