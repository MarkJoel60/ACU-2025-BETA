// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXSyncMovingMessageCondition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.Update.ExchangeService;

#nullable disable
namespace PX.Objects.CS.Email;

public class PXSyncMovingMessageCondition : PXSyncMovingCondition
{
  protected bool Incomming;
  protected bool Outgoing;
  protected ExchangeEmailsSyncCommand Command;

  public PXSyncMovingMessageCondition(
    ExchangeEmailsSyncCommand command,
    DistinguishedFolderIdNameType parent,
    bool incomming,
    bool outgoing)
    : base(parent)
  {
    this.Incomming = incomming;
    this.Outgoing = outgoing;
    this.Command = command;
  }

  protected PXSyncMovingMessageCondition(
    ExchangeEmailsSyncCommand command,
    DistinguishedFolderIdNameType parent,
    BaseFolderIdType folder,
    bool incomming,
    bool outgoing)
    : base(parent, folder)
  {
    this.Incomming = incomming;
    this.Outgoing = outgoing;
    this.Command = command;
  }

  public override BaseFolderIdType Evaluate(PXSyncMailbox mailbox, ItemType item)
  {
    bool? incomming = this.Command.EvaluateIncomming(mailbox.Address, item);
    if (!incomming.HasValue)
      return (BaseFolderIdType) null;
    if (this.Incomming && incomming.GetValueOrDefault())
      return this.FolderId;
    return this.Outgoing && !incomming.GetValueOrDefault() ? this.FolderId : (BaseFolderIdType) null;
  }

  public override PXSyncMovingCondition InitialiseFolder(BaseFolderIdType folder)
  {
    return (PXSyncMovingCondition) new PXSyncMovingMessageCondition(this.Command, this.ParentFolder, folder, this.Incomming, this.Outgoing);
  }
}
