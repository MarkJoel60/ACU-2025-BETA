// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxImportZipDataMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.TX;

public class TaxImportZipDataMaint : PXGraph<TaxImportZipDataMaint>
{
  public PXSelect<TXImportZipFileData> Data;
  public PXSavePerRow<TXImportZipFileData> Save;
  public PXCancel<TXImportZipFileData> Cancel;
  private bool _importing;
  private bool _cleared;

  protected virtual void TXImportZipFileData_ZipCode_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null)
      return;
    this._importing = sender.GetValuePending(e.Row, PXImportAttribute.ImportFlag) != null;
  }

  protected virtual void TXImportZipFileData_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    if (!(e.Row is TXImportZipFileData) || !this._importing || this._cleared)
      return;
    PXDatabase.Delete<TXImportZipFileData>(Array.Empty<PXDataFieldRestrict>());
    this._cleared = true;
  }

  public virtual void Persist()
  {
    ((PXGraph) this).Persist();
    ((PXGraph) this).Clear();
  }
}
