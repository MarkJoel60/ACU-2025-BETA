// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.EmailNotificationParameters
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Data.EP;

/// <exclude />
public class EmailNotificationParameters
{
  public int? NotificationID { get; set; }

  public object? Row { get; set; }

  public int? EmailAccountID { get; set; }

  public string? To { get; set; }

  public string? Cc { get; set; }

  public string? Bcc { get; set; }

  public string? Subject { get; set; }

  public string? Body { get; set; }

  public int? BAccountID { get; set; }

  public int? ContactID { get; set; }

  public Guid? RefNoteID { get; set; }

  public bool? CreateAsDraft { get; set; } = new bool?(false);

  public IReadOnlyCollection<Tuple<string, byte[]>>? Attachments { get; set; }

  public IReadOnlyCollection<Guid>? AttachmentLinks { get; set; }
}
