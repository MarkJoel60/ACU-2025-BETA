// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ExchangeMessage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CR;

public class ExchangeMessage
{
  public ExchangeMessage(string ewsUrl, string token)
  {
    this.EwsUrl = ewsUrl;
    this.Token = token;
  }

  public string Body { get; set; }

  public DateTime? StartDate { get; set; }

  public string Token { get; set; }

  public string EwsUrl { get; set; }

  public AttachmentDetails[] Attachments { get; set; }
}
