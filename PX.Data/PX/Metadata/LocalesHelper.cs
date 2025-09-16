// Decompiled with JetBrains decompiler
// Type: PX.Metadata.LocalesHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Metadata;

internal class LocalesHelper
{
  private const string LocalesSlotKey = "DistributedScreenInfoCache$ActiveLocales";

  internal static IEnumerable<string> GetActiveLocales()
  {
    HashSet<string> slot = PXContext.GetSlot<HashSet<string>>("DistributedScreenInfoCache$ActiveLocales");
    if (slot != null)
      return (IEnumerable<string>) slot;
    return (IEnumerable<string>) PXContext.SetSlot<HashSet<string>>("DistributedScreenInfoCache$ActiveLocales", PXDatabase.Provider.GetSlot<HashSet<string>>("DistributedScreenInfoCache$ActiveLocales", (PrefetchDelegate<HashSet<string>>) (() => new HashSet<string>(PXDatabase.SelectMulti<Locale>((PXDataField) new PXDataField<Locale.localeName>(), (PXDataField) new PXDataFieldValue<Locale.isActive>(PXDbType.Bit, (object) 1)).Select<PXDataRecord, string>((Func<PXDataRecord, string>) (r => r.GetString(0))).Append<string>(PXCultureInfo.InvariantCulture.Name), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)), typeof (Locale)));
  }
}
