// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXSyncMailboxPreset
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CS.Email;

public class PXSyncMailboxPreset
{
  public DateTime? Date;
  public string FolderID;

  public PXSyncMailboxPreset(DateTime? date, string folder)
  {
    this.Date = date;
    this.FolderID = folder;
  }
}
