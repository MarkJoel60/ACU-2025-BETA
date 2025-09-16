// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXSyncMailbox
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.Update.WebServices;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS.Email;

public class PXSyncMailbox : PXExchangeItemBase
{
  public string ExchangeTimeZone;
  public readonly int EmployeeID;
  public readonly int? EmailAccountID;
  public readonly bool IsIncomingProcessing;
  public bool Reinitialize;
  public bool IsReset;
  public PXSyncMailboxPreset ExportPreset;
  public PXSyncMailboxPreset ImportPreset;
  public List<PXSyncDirectFolder> Folders = new List<PXSyncDirectFolder>();

  public PXSyncMailbox(
    string mailbox,
    int employee,
    int? emailAccountID,
    PXSyncMailboxPreset exportPreset,
    PXSyncMailboxPreset importPreset,
    bool isIncomingProcessing)
    : base(mailbox)
  {
    this.EmployeeID = employee;
    this.EmailAccountID = emailAccountID;
    this.ExportPreset = exportPreset;
    this.ImportPreset = importPreset;
    this.IsIncomingProcessing = isIncomingProcessing;
  }
}
