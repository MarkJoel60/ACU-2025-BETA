// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.EmailExtracted
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.SM;
using System;
using System.Collections.Generic;
using System.Net.Mail;

#nullable disable
namespace PX.Objects.CR;

public class EmailExtracted
{
  public CRSMEmail Email { get; set; }

  public List<MailAddress> MailTo { get; set; }

  public List<MailAddress> MailCc { get; set; }

  public List<MailAddress> MailBcc { get; set; }

  public List<FileInfo> Attachments { get; set; }

  public List<Guid> AttachmentLinks { get; set; }
}
