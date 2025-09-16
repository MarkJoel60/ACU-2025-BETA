// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.SkipNullTextWhenImportLines
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class SkipNullTextWhenImportLines : SkipNullTextWhenImportLines<POOrderEntry>
{
  protected override PXSelectBase LinesView => (PXSelectBase) this.Base.Transactions;

  protected override IEnumerable<Type> FieldsWithNullText()
  {
    yield return typeof (POLine.subItemID);
    yield return typeof (POLine.lotSerialNbr);
  }
}
