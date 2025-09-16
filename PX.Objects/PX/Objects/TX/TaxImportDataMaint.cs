// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxImportDataMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.TX;

public class TaxImportDataMaint : PXGraph<TaxImportDataMaint>
{
  public PXSelect<TXImportFileData> Data;
  public PXSavePerRow<TXImportFileData> Save;
  public PXCancel<TXImportFileData> Cancel;
  private bool _importing;
  private bool _cleared;

  protected virtual void TXImportFileData_StateCode_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null)
      return;
    this._importing = sender.GetValuePending(e.Row, PXImportAttribute.ImportFlag) != null;
  }

  protected virtual void TXImportFileData_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is TXImportFileData) || !this._importing || this._cleared)
      return;
    PXDatabase.Delete<TXImportFileData>(Array.Empty<PXDataFieldRestrict>());
    this._cleared = true;
  }

  public virtual void Persist()
  {
    ((PXGraph) this).Persist();
    ((PXGraph) this).Clear();
  }
}
