// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAttachedFileAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXAttachedFileAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  public string[] FileTypes { get; private set; }

  public FileTypesCategory Category { get; private set; }

  public PXAttachedFileAttribute(string[] fileTypes)
  {
    this.FileTypes = fileTypes;
    this.Category = FileTypesCategory.Custom;
  }

  public PXAttachedFileAttribute(FileTypesCategory category) => this.Category = category;

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(sender, e.Row);
    if (fileNotes == null || fileNotes.Length == 0)
    {
      e.ReturnValue = (object) null;
    }
    else
    {
      string returnValue = e.ReturnValue as string;
      string str = (string) null;
      bool flag1 = string.IsNullOrEmpty(returnValue);
      bool flag2 = !flag1 && FileInfo.GetShortName(returnValue) == returnValue;
      UploadFileMaintenance.PXSelectFile pxSelectFile = new UploadFileMaintenance.PXSelectFile(sender.Graph ?? new PXGraph());
      foreach (Guid guid in fileNotes)
      {
        UploadFile uploadFile = (UploadFile) pxSelectFile.Select((object) guid);
        if (uploadFile != null)
        {
          if (!flag1 && uploadFile.Name == returnValue)
            return;
          if (flag2 && FileInfo.GetShortName(uploadFile.Name) == returnValue)
          {
            str = uploadFile.Name;
            break;
          }
          if (string.IsNullOrEmpty(str) && this.GetIsAllowedType(uploadFile.Name))
          {
            str = uploadFile.Name;
            if (flag1)
              break;
          }
        }
      }
      e.ReturnValue = (object) str;
    }
  }

  private bool GetIsAllowedType(string name)
  {
    int startIndex = name.LastIndexOf(".");
    if (startIndex == -1)
      return false;
    string fileExt = name.Substring(startIndex).ToLower();
    string[] source = this.Category != FileTypesCategory.Regular ? (this.Category != FileTypesCategory.Images ? this.FileTypes : SitePolicy.AllowedImageTypesExt) : SitePolicy.AllowedFileTypesExt;
    return source != null && ((IEnumerable<string>) source).Any<string>((Func<string, bool>) (ext => ext.ToLower() == fileExt));
  }
}
