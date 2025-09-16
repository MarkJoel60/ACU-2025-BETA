// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCollationComparer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Localization;
using PX.Data.Update;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXCollationComparer : 
  IPrefetchable,
  IPXCompanyDependent,
  IComparer,
  IEqualityComparer,
  IComparer<string>,
  IEqualityComparer<string>
{
  internal static PXCollationComparer DefaultCollationComparer = new PXCollationComparer()
  {
    IngnoreCase = true,
    Culture = CultureInfo.InvariantCulture,
    Comparer = StringComparer.InvariantCultureIgnoreCase
  };

  public bool IngnoreCase { get; private set; }

  public CultureInfo Culture { get; private set; }

  public StringComparer Comparer { get; private set; }

  public void Prefetch()
  {
    using (new PXImpersonationContext(PXInstanceHelper.ScopeUser))
    {
      try
      {
        using (new PXConnectionScope())
        {
          using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<SystemCollation>((PXDataField) new PXDataField<SystemCollation.localeName>(), (PXDataField) new PXDataFieldValue<SystemCollation.collationName>((object) PXDatabase.SelectCollation()), (PXDataField) new PXDataFieldValue<SystemCollation.sqlDialect>((object) PXDatabase.Provider.SqlDialect.GetType().Name)))
          {
            if (pxDataRecord != null)
            {
              if (!pxDataRecord.IsDBNull(0))
              {
                CultureInfo cultureInfo = CultureInfo.GetCultureInfo(pxDataRecord.GetString(0));
                if (cultureInfo != null)
                {
                  this.Culture = cultureInfo;
                  this.IngnoreCase = true;
                  this.Comparer = StringComparer.Create(cultureInfo, true);
                }
              }
            }
          }
        }
      }
      catch
      {
      }
      if (this.Culture != null && this.Comparer != null)
        return;
      this.IngnoreCase = true;
      this.Culture = CultureInfo.InvariantCulture;
      this.Comparer = StringComparer.InvariantCultureIgnoreCase;
    }
  }

  public bool Equals(string strA, string strB) => this.Compare(strA, strB) == 0;

  public int Compare(string strA, string strB)
  {
    return string.Compare(strA, strB, this.IngnoreCase, this.Culture);
  }

  public int IndexOf(string str, string value)
  {
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    return this.Culture.CompareInfo.IndexOf(str, value);
  }

  public bool Contains(string str, string value) => this.IndexOf(str, value) >= 0;

  public bool StartsWith(string str, string value) => this.Culture.CompareInfo.IsPrefix(str, value);

  public bool EndsWith(string str, string value) => this.Culture.CompareInfo.IsSuffix(str, value);

  public bool Equals(object x, object y) => this.Comparer.Equals(x, y);

  public int Compare(object x, object y) => this.Comparer.Compare(x, y);

  public int GetHashCode(object obj) => this.Comparer.GetHashCode(obj);

  public int GetHashCode(string obj) => this.Comparer.GetHashCode(obj);
}
