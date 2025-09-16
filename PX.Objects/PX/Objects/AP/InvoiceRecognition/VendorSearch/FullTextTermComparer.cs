// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.VendorSearch.FullTextTermComparer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CloudServices.DocumentRecognition;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.VendorSearch;

internal class FullTextTermComparer : IEqualityComparer<FullTextTerm>
{
  private readonly StringComparer _stringComparer = StringComparer.CurrentCultureIgnoreCase;

  public bool Equals(FullTextTerm x, FullTextTerm y)
  {
    return this._stringComparer.Equals(x?.Text, y?.Text);
  }

  public int GetHashCode(FullTextTerm obj) => this._stringComparer.GetHashCode(obj?.Text);
}
