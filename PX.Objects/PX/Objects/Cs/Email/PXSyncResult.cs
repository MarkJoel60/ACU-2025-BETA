// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXSyncResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CS.Email;

public class PXSyncResult : PXSyncItemID
{
  public string DisplayKey;
  public string ActionTitle;
  public string OperationTitle;
  public PXEmailSyncDirection.Directions Direction;
  public PXSyncItemStatus ItemStatus;
  public Exception Error;
  public string Message;
  public string[] Details;
  public DateTime Date;
  public bool Reprocess;

  public bool Success => this.Error == null && string.IsNullOrEmpty(this.Message);

  public PXSyncResult(
    string operation,
    PXEmailSyncDirection.Directions direction,
    string mailbox,
    string id,
    Guid? note,
    string key)
    : base(mailbox, id, note)
  {
    this.DisplayKey = key;
    this.Date = DateTime.UtcNow;
    this.OperationTitle = operation;
    this.Direction = direction;
  }

  public PXSyncResult(
    string operation,
    PXEmailSyncDirection.Directions direction,
    string mailbox,
    string id,
    Guid? note,
    string key,
    string message,
    Exception error,
    string[] details)
    : this(operation, direction, mailbox, id, note, key)
  {
    this.Error = error;
    this.Message = message;
    this.Details = details;
  }

  public PXSyncResult(
    PXSyncResult result,
    string id = null,
    Guid? note = null,
    string key = null,
    PXSyncItemStatus? status = null)
    : this(result.OperationTitle, result.Direction, result.Address, id ?? result.ItemID, note ?? result.NoteID, key ?? result.DisplayKey)
  {
    this.ItemStatus = result.ItemStatus;
    this.DisplayKey = result.DisplayKey;
    this.ActionTitle = result.ActionTitle;
    this.OperationTitle = result.OperationTitle;
    this.Direction = result.Direction;
    this.Error = result.Error;
    this.Message = result.Message;
    this.Details = result.Details;
    this.Date = result.Date;
    this.Reprocess = result.Reprocess;
  }

  public override bool NeedProcess() => this.Reprocess;
}
