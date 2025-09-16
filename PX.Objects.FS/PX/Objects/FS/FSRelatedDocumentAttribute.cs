// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSRelatedDocumentAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.Common;
using PX.Objects.FS.DAC;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO.DAC.Projections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Objects.FS;

public class FSRelatedDocumentAttribute : PXStringAttribute
{
  protected EntityHelper helper;
  protected Type _Dac;
  protected Type _EntityType;
  protected Type _DocType;
  protected Type _RefNbr;
  protected string _DocTypeS;

  public FSRelatedDocumentAttribute(Type dac, Type entityType, Type docType, Type refNbr)
  {
    this._Dac = dac;
    this._EntityType = entityType;
    this._DocType = docType;
    this._RefNbr = refNbr;
  }

  public FSRelatedDocumentAttribute(Type dac) => this._Dac = dac;

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.helper = new EntityHelper(sender.Graph);
    // ISSUE: method pointer
    PXButtonDelegate pxButtonDelegate = new PXButtonDelegate((object) this, __methodptr(InitDelegate));
    string str = $"{sender.GetItemType().Name}${((PXEventSubscriberAttribute) this)._FieldName}$Link";
    sender.Graph.Actions[str] = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(sender.BqlTable), (object) sender.Graph, (object) str, (object) pxButtonDelegate, (object) new PXEventSubscriberAttribute[2]
    {
      (PXEventSubscriberAttribute) new PXUIFieldAttribute()
      {
        MapEnableRights = (PXCacheRights) 1
      },
      (PXEventSubscriberAttribute) new PXButtonAttribute()
      {
        OnClosingPopup = (PXSpecialButtonType) 3
      }
    });
  }

  protected virtual FSRelatedDocumentAttribute.DACReference GetDACRef(PXCache cache, object row)
  {
    object[] keys = (object[]) null;
    Type type = (Type) null;
    if (this._EntityType != (Type) null)
    {
      keys = new object[2]
      {
        cache.GetValue(row, this._DocType.Name),
        cache.GetValue(row, this._RefNbr.Name)
      };
      type = this.GetDACType(cache, row);
    }
    else
    {
      string empty = string.Empty;
      IFSRelatedDoc dacRow = (IFSRelatedDoc) this.GetDACRow(cache, row);
      if (dacRow != null)
      {
        if (!string.IsNullOrEmpty(dacRow.AppointmentRefNbr))
        {
          string appointmentRefNbr = dacRow.AppointmentRefNbr;
          type = typeof (FSAppointment);
          keys = new object[2]
          {
            (object) dacRow.SrvOrdType,
            (object) appointmentRefNbr
          };
        }
        else if (!string.IsNullOrEmpty(dacRow.ServiceOrderRefNbr))
        {
          string serviceOrderRefNbr = dacRow.ServiceOrderRefNbr;
          type = typeof (FSServiceOrder);
          keys = new object[2]
          {
            (object) dacRow.SrvOrdType,
            (object) serviceOrderRefNbr
          };
        }
        else if (!string.IsNullOrEmpty(dacRow.ServiceContractRefNbr))
        {
          string serviceContractRefNbr = dacRow.ServiceContractRefNbr;
          type = typeof (FSServiceContract);
          keys = new object[1]
          {
            (object) serviceContractRefNbr
          };
        }
      }
    }
    return new FSRelatedDocumentAttribute.DACReference(type, keys);
  }

  protected virtual Type GetDACType(PXCache cache, object row)
  {
    Type dacType = (Type) null;
    object obj = cache.GetValue(row, this._EntityType.Name);
    if (obj == null)
      return (Type) null;
    if (obj.Equals((object) "PXSO"))
      dacType = typeof (PX.Objects.SO.SOOrder);
    else if (obj.Equals((object) "PXSI") || obj.Equals((object) "PXSM"))
      dacType = typeof (PX.Objects.SO.SOInvoice);
    else if (obj.Equals((object) "PXAR") || obj.Equals((object) "PXAM"))
      dacType = typeof (PX.Objects.AR.ARInvoice);
    else if (obj.Equals((object) "PXAP"))
      dacType = typeof (PX.Objects.AP.APInvoice);
    else if (obj.Equals((object) "PXPM"))
      dacType = typeof (PMRegister);
    else if (obj.Equals((object) "PXIR") || obj.Equals((object) "PXIS"))
      dacType = typeof (PX.Objects.IN.INRegister);
    return dacType;
  }

  protected virtual object GetDACRow(PXCache cache, object currentRow)
  {
    object dacRow = (object) null;
    if (this._Dac != (Type) null)
    {
      if (this._Dac == typeof (PX.Objects.SO.SOLine))
        dacRow = (object) cache.GetExtension<FSxSOLine>(currentRow);
      else if (this._Dac == typeof (INTran))
        dacRow = (object) cache.GetExtension<FSxINTran>(currentRow);
      else if (this._Dac == typeof (APTran))
        dacRow = (object) cache.GetExtension<FSxAPTran>(currentRow);
      else if (currentRow is ARTranForDirectInvoice)
        dacRow = (object) cache.GetExtension<FSxARTranForDirectInvoice>(currentRow);
      else if (this._Dac == typeof (PX.Objects.AR.ARTran))
        dacRow = (object) cache.GetExtension<FSxARTran>(currentRow);
      else if (this._Dac == typeof (FSBillHistory))
        dacRow = currentRow;
    }
    return dacRow;
  }

  protected virtual object GetReturnValue(PXCache sender, object row)
  {
    object returnValue = (object) null;
    FSRelatedDocumentAttribute.DACReference dacRef = this.GetDACRef(sender, row);
    if (dacRef._Keys != null && dacRef._Type != (Type) null)
      returnValue = this.GetEntityRowID(sender.Graph.Caches[dacRef._Type], dacRef._Keys);
    return returnValue;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    object returnValue = this.GetReturnValue(sender, e.Row);
    if (returnValue == null)
      base.FieldSelecting(sender, e);
    else
      e.ReturnValue = returnValue;
  }

  public virtual object GetEntityRowID(PXCache cache, object[] keys)
  {
    return FSRelatedDocumentAttribute.GetEntityRowID(cache, keys, ", ");
  }

  public static object GetEntityRowID(PXCache cache, object[] keys, string separator)
  {
    StringBuilder stringBuilder = new StringBuilder();
    int num = 0;
    foreach (string key1 in (IEnumerable<string>) cache.Keys)
    {
      if (num < keys.Length)
      {
        object key2 = keys[num++];
        cache.RaiseFieldSelecting(key1, (object) null, ref key2, true);
        if (key2 != null)
        {
          if (stringBuilder.Length != 0)
            stringBuilder.Append(separator);
          stringBuilder.Append(key2.ToString().TrimEnd());
        }
      }
      else
        break;
    }
    return (object) stringBuilder.ToString();
  }

  public IEnumerable InitDelegate(PXAdapter adapter)
  {
    PXCache cach = adapter.View.Graph.Caches[this._Dac];
    if (cach.Current != null)
    {
      PXRefNoteBaseAttribute.PXLinkState pxLinkState = (PXRefNoteBaseAttribute.PXLinkState) null;
      FSRelatedDocumentAttribute.DACReference dacRef = this.GetDACRef(cach, cach.Current);
      if (dacRef._Keys != null && dacRef._Type != (Type) null)
        pxLinkState = (PXRefNoteBaseAttribute.PXLinkState) PXRefNoteBaseAttribute.PXLinkState.CreateInstance((object) null, dacRef._Type, dacRef._Keys);
      if (pxLinkState != null)
        this.helper.NavigateToRow(pxLinkState.target.FullName, pxLinkState.keys, (PXRedirectHelper.WindowMode) 3);
      else
        this.helper.NavigateToRow((Guid?) cach.GetValue(cach.Current, ((PXEventSubscriberAttribute) this)._FieldName), (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  public class DACReference
  {
    public Type _Type;
    public object[] _Keys;

    public DACReference(Type type, object[] keys)
    {
      this._Type = type;
      this._Keys = keys;
    }
  }
}
