// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ECMExemptionReasonSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.AR;

public class ECMExemptionReasonSelectorAttribute : PXCustomSelectorAttribute
{
  public ECMExemptionReasonSelectorAttribute()
    : base(typeof (CertificateReason.reasonID), new Type[1]
    {
      typeof (CertificateReason.reasonName)
    })
  {
  }

  protected IEnumerable GetRecords()
  {
    foreach (CertificateReason record in this._Graph.Caches[typeof (CertificateReason)].Cached)
      yield return (object) record;
  }
}
