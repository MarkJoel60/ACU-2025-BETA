// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCollationProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Localization;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace PX.Data;

internal class PXCollationProvider : IPrefetchable, IPXCompanyDependent
{
  private Dictionary<string, (string CS, string CI, string CSL, string CIL)> _collations;

  public (string CS, string CI, string CSL, string CIL) Collation
  {
    get
    {
      (string, string, string, string) valueTuple;
      return this._collations == null || !this._collations.TryGetValue(CultureInfo.CurrentCulture.Name, out valueTuple) ? ((string) null, (string) null, (string) null, (string) null) : valueTuple;
    }
  }

  public (string CS, string CI, string CSL, string CIL) CollationByCulture(string cultureName)
  {
    (string, string, string, string) valueTuple;
    return this._collations == null || !this._collations.TryGetValue(cultureName, out valueTuple) ? ((string) null, (string) null, (string) null, (string) null) : valueTuple;
  }

  public void Prefetch()
  {
    this._collations = new Dictionary<string, (string, string, string, string)>();
    try
    {
      EnumerableExtensions.ForEach<SystemCollation>(PXDatabase.SelectRecords<SystemCollation>((PXDataField) new PXDataFieldValue<SystemCollation.sqlDialect>((object) $"{PXDatabase.Provider.SqlDialect.GetType().Name}-{PXDatabase.Provider.SqlDialect.DbmsVersion.Major}"), (PXDataField) new PXDataFieldValue<SystemCollation.priority>((object) 0)), (System.Action<SystemCollation>) (record =>
      {
        if (record == null)
          return;
        this._collations[record.LocaleName] = (record.CollationNameCS, record.CollationName, record.CollationNameCSLatin, record.CollationNameLatin);
      }));
      EnumerableExtensions.ForEach<SystemCollation>(PXDatabase.SelectRecords<SystemCollation>((PXDataField) new PXDataFieldValue<SystemCollation.sqlDialect>((object) PXDatabase.Provider.SqlDialect.GetType().Name), (PXDataField) new PXDataFieldValue<SystemCollation.priority>((object) 0)), (System.Action<SystemCollation>) (record =>
      {
        if (record == null || this._collations.ContainsKey(record.LocaleName))
          return;
        this._collations[record.LocaleName] = (record.CollationNameCS, record.CollationName, record.CollationNameCSLatin, record.CollationNameLatin);
      }));
    }
    catch
    {
    }
  }
}
