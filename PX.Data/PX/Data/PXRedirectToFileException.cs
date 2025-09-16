// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRedirectToFileException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Runtime.Serialization;
using System.Text;

#nullable disable
namespace PX.Data;

public class PXRedirectToFileException : PXRedirectToUrlException
{
  protected FileInfo _FileInfo;

  protected static string BuildUrl(
    Guid? fileID,
    int? revision,
    bool forceDownload,
    bool inMemory,
    bool openInNewTab = false)
  {
    StringBuilder stringBuilder = new StringBuilder("~/Frames/GetFile.ashx?fileID=");
    stringBuilder.Append(fileID.ToString());
    if (revision.HasValue)
      stringBuilder.Append("&revision=").Append(revision.ToString());
    if (inMemory)
      stringBuilder.Append("&inmemory=1").Append("&preserveSession=true");
    if (forceDownload)
      stringBuilder.Append("&fdwnld=1");
    else if (openInNewTab)
      stringBuilder.Append("$target=_blank");
    return stringBuilder.ToString();
  }

  public static string BuildUrl(Guid? fileID)
  {
    return PXRedirectToFileException.BuildUrl(fileID, new int?(), false, false);
  }

  private PXRedirectToFileException(
    Guid? fileID,
    int? revision,
    bool forceDownload,
    bool inMemory,
    bool flag)
    : base(PXRedirectToFileException.BuildUrl(fileID, revision, forceDownload, inMemory, !forceDownload), forceDownload ? PXBaseRedirectException.WindowMode.Same : PXBaseRedirectException.WindowMode.New, true, forceDownload ? "Download File" : "View File")
  {
  }

  protected PXRedirectToFileException(
    string url,
    PXBaseRedirectException.WindowMode newWindow,
    bool suppressFrameset,
    string message)
    : base(url, newWindow, suppressFrameset, message)
  {
  }

  public PXRedirectToFileException(Guid? fileID, int revision, bool forceDownload, bool inMemory)
    : this(fileID, new int?(revision), forceDownload, inMemory, false)
  {
  }

  public PXRedirectToFileException(Guid? fileID, int revision, bool forceDownload)
    : this(fileID, revision, forceDownload, false)
  {
  }

  public PXRedirectToFileException(Guid? fileID, bool forceDownload)
    : this(fileID, new int?(), forceDownload, false, false)
  {
  }

  public PXRedirectToFileException(FileInfo fileInfo, bool forceDownload)
  {
    FileInfo fileInfo1 = fileInfo;
    Guid? nullable1 = new Guid?(fileInfo.UID ?? Guid.NewGuid());
    Guid? nullable2 = nullable1;
    fileInfo1.UID = nullable2;
    // ISSUE: explicit constructor call
    this.\u002Ector(new Guid?(nullable1.Value), 1, forceDownload, true);
    this._FileInfo = fileInfo;
  }

  public PXRedirectToFileException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2147024809;
    ReflectionSerializer.RestoreObjectProps<PXRedirectToFileException>(this, info);
  }

  public virtual FileInfo GetFileInfo() => this._FileInfo;

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXRedirectToFileException>(this, info);
    base.GetObjectData(info, context);
  }
}
