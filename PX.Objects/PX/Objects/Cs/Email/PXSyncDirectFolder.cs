// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXSyncDirectFolder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.Update.ExchangeService;
using PX.Data.Update.WebServices;
using PX.SM;

#nullable disable
namespace PX.Objects.CS.Email;

public class PXSyncDirectFolder : PXExchangeFolderID
{
  public readonly bool Categorized;
  public readonly PXEmailSyncDirection.Directions Direction;
  public readonly PXSyncMovingCondition[] MoveToFolder;

  public bool IsExport => (this.Direction & 2) == 2;

  public bool IsImport => (this.Direction & 1) == 1;

  public PXSyncDirectFolder(
    string mailbox,
    BaseFolderIdType folder,
    PXEmailSyncDirection.Directions direction,
    bool categorized,
    PXSyncMovingCondition[] moveTo)
    : base(mailbox, folder, false)
  {
    this.Direction = direction;
    this.Categorized = categorized;
    this.MoveToFolder = moveTo;
  }
}
