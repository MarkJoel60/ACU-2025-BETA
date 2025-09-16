// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.SkipNullTextWhenImportLines`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.GraphExtensions;

public abstract class SkipNullTextWhenImportLines<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph, PXImportAttribute.IPXPrepareItems
{
  protected virtual string NullText => PXMessages.LocalizeNoPrefix("<SPLIT>");

  protected abstract PXSelectBase LinesView { get; }

  protected abstract IEnumerable<Type> FieldsWithNullText();

  /// Overrides <see cref="M:PX.Data.PXImportAttribute.IPXPrepareItems.PrepareImportRow(System.String,System.Collections.IDictionary,System.Collections.IDictionary)" />
  [PXOverride]
  public bool PrepareImportRow(
    string viewName,
    IDictionary keys,
    IDictionary values,
    Func<string, IDictionary, IDictionary, bool> baseImpl)
  {
    if (viewName.Equals(this.LinesView.Name, StringComparison.InvariantCultureIgnoreCase))
    {
      foreach (Type field in this.FieldsWithNullText())
        this.ClearNullText(values, field);
    }
    return baseImpl(viewName, keys, values);
  }

  protected virtual bool ClearNullText(IDictionary values, Type field)
  {
    string field1 = this.LinesView.Cache.GetField(field);
    if (values.Contains((object) field1))
    {
      string str1 = values[(object) field1] is string str2 ? str2.Trim() : (string) null;
      if (!string.IsNullOrEmpty(str1) && str1.Equals(this.NullText, StringComparison.InvariantCultureIgnoreCase))
      {
        values[(object) field1] = (object) null;
        return true;
      }
    }
    return false;
  }
}
