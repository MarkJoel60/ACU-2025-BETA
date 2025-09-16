// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.UnionMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections;

#nullable disable
namespace PX.Objects.PM;

public class UnionMaint : PXGraph<UnionMaint>, PXImportAttribute.IPXPrepareItems
{
  [PXImport(typeof (PMUnion))]
  public PXSelect<PMUnion> Items;
  public PXSavePerRow<PMUnion> Save;
  public PXCancel<PMUnion> Cancel;

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values) => true;

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }
}
