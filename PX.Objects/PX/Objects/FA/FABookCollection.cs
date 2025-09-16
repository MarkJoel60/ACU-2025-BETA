// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookCollection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA;

public class FABookCollection : IPrefetchable, IPXCompanyDependent
{
  public Dictionary<int, FABook> Books = new Dictionary<int, FABook>();

  public void Prefetch()
  {
    this.Books = PXDatabase.SelectMulti<FABook>(new PXDataField[4]
    {
      (PXDataField) new PXDataField<FABook.bookID>(),
      (PXDataField) new PXDataField<FABook.bookCode>(),
      (PXDataField) new PXDataField<FABook.updateGL>(),
      (PXDataField) new PXDataField<FABook.description>()
    }).Select<PXDataRecord, FABook>((Func<PXDataRecord, FABook>) (row => new FABook()
    {
      BookID = row.GetInt32(0),
      BookCode = row.GetString(1).Trim(),
      UpdateGL = row.GetBoolean(2),
      Description = row.GetString(3)?.Trim()
    })).ToDictionary<FABook, int>((Func<FABook, int>) (book => book.BookID.Value));
  }
}
