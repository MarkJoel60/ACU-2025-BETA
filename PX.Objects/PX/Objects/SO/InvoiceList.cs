// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.InvoiceList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

public class InvoiceList(PXGraph graph) : DocumentList<PX.Objects.AR.ARInvoice, SOInvoice>(graph)
{
  private Type[] _typesArray;

  protected Type[] TypesArray
  {
    get
    {
      Type[] typesArray = this._typesArray;
      if (typesArray != null)
        return typesArray;
      return this._typesArray = new Type[3]
      {
        typeof (PX.Objects.AR.ARInvoice),
        typeof (SOInvoice),
        typeof (PX.Objects.CM.Extensions.CurrencyInfo)
      };
    }
  }

  public virtual void Add(PX.Objects.AR.ARInvoice item0, SOInvoice item1, PX.Objects.CM.Extensions.CurrencyInfo curyInfo)
  {
    this.Add((PXResult<PX.Objects.AR.ARInvoice, SOInvoice>) new PXResult<PX.Objects.AR.ARInvoice, SOInvoice, PX.Objects.CM.Extensions.CurrencyInfo>(item0, item1, curyInfo));
  }

  public override PXResult<PX.Objects.AR.ARInvoice, SOInvoice> Find(object item)
  {
    Type tableType = item.GetType();
    Type appropriateType = ((IEnumerable<Type>) this.TypesArray).FirstOrDefault<Type>((Func<Type, bool>) (t => t.IsAssignableFrom(tableType)));
    if (!(appropriateType != (Type) null))
      return (PXResult<PX.Objects.AR.ARInvoice, SOInvoice>) null;
    PXCache cache = this._Graph.Caches[appropriateType];
    return this.Find((Predicate<PXResult<PX.Objects.AR.ARInvoice, SOInvoice>>) (data => cache.ObjectsEqual(((PXResult) data)[appropriateType], item)));
  }

  protected override object GetValue(object data, Type field)
  {
    Type tableType = BqlCommand.GetItemType(field);
    return this._Graph.Caches[((IEnumerable<Type>) this.TypesArray).FirstOrDefault<Type>((Func<Type, bool>) (t => t.IsAssignableFrom(tableType)))].GetValue(((PXResult) data)[tableType], field.Name);
  }
}
