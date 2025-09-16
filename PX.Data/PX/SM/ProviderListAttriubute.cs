// Decompiled with JetBrains decompiler
// Type: PX.SM.ProviderListAttriubute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.SM;

internal class ProviderListAttriubute : PXStringListAttribute
{
  public ProviderListAttriubute()
  {
    PXBlobStorageProviderAttribute[] array = ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).SelectMany<Assembly, object, PXBlobStorageProviderAttribute>((Func<Assembly, IEnumerable<object>>) (a => (IEnumerable<object>) a.GetCustomAttributes(typeof (PXBlobStorageProviderAttribute), false)), (Func<Assembly, object, PXBlobStorageProviderAttribute>) ((a, p) => (PXBlobStorageProviderAttribute) p)).ToArray<PXBlobStorageProviderAttribute>();
    this._AllowedLabels = ((IEnumerable<PXBlobStorageProviderAttribute>) array).Select<PXBlobStorageProviderAttribute, string>((Func<PXBlobStorageProviderAttribute, string>) (_ => _.Title)).ToArray<string>();
    this._AllowedValues = ((IEnumerable<PXBlobStorageProviderAttribute>) array).Select<PXBlobStorageProviderAttribute, string>((Func<PXBlobStorageProviderAttribute, string>) (_ => _.Provider.FullName)).ToArray<string>();
  }
}
