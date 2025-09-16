// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.ExcludedVendorDomainDefinition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP.InvoiceRecognition.DAC;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

internal class ExcludedVendorDomainDefinition : IPrefetchable, IPXCompanyDependent
{
  private readonly HashSet<string> _names = new HashSet<string>();

  public bool Contains(string name) => this._names.Contains(name);

  public void Prefetch()
  {
    this._names.Clear();
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<ExcludedVendorDomain>((PXDataField) new PXDataField<ExcludedVendorDomain.name>()))
      this._names.Add(pxDataRecord.GetString(0));
  }

  public static ExcludedVendorDomainDefinition GetSlot()
  {
    return PXDatabase.GetSlot<ExcludedVendorDomainDefinition>(typeof (ExcludedVendorDomainDefinition).FullName, typeof (ExcludedVendorDomain));
  }
}
