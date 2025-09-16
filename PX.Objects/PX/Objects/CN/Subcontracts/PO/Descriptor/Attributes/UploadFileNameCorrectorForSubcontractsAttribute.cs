// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PO.Descriptor.Attributes.UploadFileNameCorrectorForSubcontractsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PO;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PO.Descriptor.Attributes;

public class UploadFileNameCorrectorForSubcontractsAttribute : PXEventSubscriberAttribute
{
  private PXCache cache;
  private PXGraph graph;

  public virtual void CacheAttached(PXCache eventCache)
  {
    this.cache = eventCache;
    this.graph = eventCache.Graph;
    // ISSUE: method pointer
    this.graph.FieldUpdating.AddHandler(typeof (POOrder), "NoteFiles", new PXFieldUpdating((object) this, __methodptr(UpdateFileNamesForCommitment)));
    // ISSUE: method pointer
    this.graph.FieldUpdating.AddHandler(typeof (POLine), "NoteFiles", new PXFieldUpdating((object) this, __methodptr(UpdateFileNamesForCommitmentLine)));
  }

  private void UpdateFileNamesForCommitment(PXCache eventCache, PXFieldUpdatingEventArgs args)
  {
    this.UpdateFileNames<POOrder, POOrder.orderType>(eventCache, args);
  }

  private void UpdateFileNamesForCommitmentLine(PXCache eventCache, PXFieldUpdatingEventArgs args)
  {
    this.UpdateFileNames<POLine, POLine.orderType>(eventCache, args);
  }

  private void UpdateFileNames<TEntity, TField>(PXCache eventCache, PXFieldUpdatingEventArgs args)
    where TEntity : class
    where TField : IBqlField
  {
    Guid[] newValue = args.NewValue as Guid[];
    TEntity row = args.Row as TEntity;
    if (!((string) eventCache.GetValue<TField>((object) row) == "RS") || newValue == null || newValue.Length == 0)
      return;
    this.UpdateFileNames((IEnumerable<Guid>) newValue);
  }

  private void UpdateFileNames(IEnumerable<Guid> fileIds)
  {
    foreach (UploadFile uploadFile in this.GetUploadFiles((IEnumerable) fileIds))
      this.UpdateFileNameIfNeed(uploadFile);
  }

  private void UpdateFileNameIfNeed(UploadFile uploadFile)
  {
    string uploadFileName = this.GetUploadFileName(uploadFile);
    if (!(uploadFileName != uploadFile.Name))
      return;
    this.UpdateFileName(uploadFile.FileID, uploadFileName);
  }

  private string GetUploadFileName(UploadFile uploadFile)
  {
    return $"{PXSiteMap.CurrentNode.Title} ({(this.cache.Current as POOrder)?.OrderNbr})\\{Path.GetFileName(uploadFile.Name)}";
  }

  private void UpdateFileName(Guid? fileId, string name)
  {
    PXDatabase.Update<UploadFile>(new PXDataFieldParam[2]
    {
      (PXDataFieldParam) new PXDataFieldAssign("Name", (PXDbType) 12, (object) name),
      (PXDataFieldParam) new PXDataFieldRestrict("FileID", (PXDbType) 14, (object) fileId)
    });
  }

  private IEnumerable<UploadFile> GetUploadFiles(IEnumerable fileIds)
  {
    return ((PXSelectBase<UploadFile>) new PXSelect<UploadFile, Where<UploadFile.fileID, In<Required<UploadFile.fileID>>>>(this.graph)).Select(new object[1]
    {
      (object) fileIds
    }).FirstTableItems;
  }
}
