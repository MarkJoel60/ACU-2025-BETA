// Decompiled with JetBrains decompiler
// Type: PX.SM.UploadAllowedFileTypesMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Wiki.Parser;

#nullable disable
namespace PX.SM;

public class UploadAllowedFileTypesMaint : PXGraph<UploadAllowedFileTypesMaint>
{
  public PXSelect<PreferencesGeneral> Prefs;
  public PXSelect<UploadAllowedFileTypes> PrefsDetail;
  public PXSave<PreferencesGeneral> Save;
  public PXCancel<PreferencesGeneral> Cancel;

  protected void PreferencesGeneral_MaxUploadSize_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    PXPageCacheUtils.InvalidateCachedPages();
  }

  protected virtual void PreferencesGeneral_PageCacheVersion_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
      return;
    e.Cancel = true;
  }

  protected void UploadAllowedFileTypes_FileExt_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    string newValue = e.NewValue as string;
    if (string.IsNullOrEmpty(newValue) || newValue[0] == '.')
      return;
    string str = "." + newValue.Trim();
    e.NewValue = (object) str;
  }

  protected void UploadAllowedFileTypes_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is UploadAllowedFileTypes row) || !string.IsNullOrEmpty(row.DefApplication))
      return;
    row.DefApplication = MimeTypes.GetMimeType(row.FileExt);
  }
}
