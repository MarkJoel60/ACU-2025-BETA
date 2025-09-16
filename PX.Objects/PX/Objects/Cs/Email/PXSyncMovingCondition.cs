// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXSyncMovingCondition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.Update.ExchangeService;

#nullable disable
namespace PX.Objects.CS.Email;

public abstract class PXSyncMovingCondition
{
  public readonly DistinguishedFolderIdNameType ParentFolder;
  protected readonly BaseFolderIdType FolderId;

  public PXSyncMovingCondition(DistinguishedFolderIdNameType parent) => this.ParentFolder = parent;

  protected PXSyncMovingCondition(DistinguishedFolderIdNameType parent, BaseFolderIdType folder)
  {
    this.ParentFolder = parent;
    this.FolderId = folder;
  }

  public abstract BaseFolderIdType Evaluate(PXSyncMailbox mailbox, ItemType item);

  public abstract PXSyncMovingCondition InitialiseFolder(BaseFolderIdType folder);
}
