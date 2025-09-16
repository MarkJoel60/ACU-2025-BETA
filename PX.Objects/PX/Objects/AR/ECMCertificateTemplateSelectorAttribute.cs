// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ECMCertificateTemplateSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.AR;

public class ECMCertificateTemplateSelectorAttribute : PXCustomSelectorAttribute
{
  public ECMCertificateTemplateSelectorAttribute()
    : base(typeof (CertificateTemplate.templateID), new Type[1]
    {
      typeof (CertificateTemplate.templateName)
    })
  {
  }

  public ECMCertificateTemplateSelectorAttribute(Type type)
    : base(type)
  {
  }

  protected IEnumerable GetRecords()
  {
    foreach (CertificateTemplate record in this._Graph.Caches[typeof (CertificateTemplate)].Cached)
      yield return (object) record;
  }
}
