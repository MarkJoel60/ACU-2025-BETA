// Decompiled with JetBrains decompiler
// Type: PX.Data.MultiFactorAuth.AccessCodeFactorSender
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.MultiFactorAuth;

internal class AccessCodeFactorSender : ITwoFactorSender
{
  public TwoFactorSenderType TypeId => TwoFactorSenderType.Code;

  public bool IsPersistentCode => false;

  public string Type => "AccessCode";

  public bool ShowTextBox => true;

  public string ButtonName => "Access Code";

  public string ButtonToolTip => "Enter code generated in mobile app or from the list";

  public (string message, bool isError) SendAcceptRequest(
    IEnumerable<(int companyId, Guid userId, int twoFactorLevel)> users,
    string innerCorrelation,
    Dictionary<string, string> customData)
  {
    return (PXLocalizer.Localize("Enter code generated in Acumatica mobile app (use Generate Access Code command of Edit Account menu) or from the list of access codes"), false);
  }

  public int ResendAfter => -1;
}
