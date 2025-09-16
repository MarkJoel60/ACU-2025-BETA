// Decompiled with JetBrains decompiler
// Type: PX.Data.MultiFactorAuth.ITwoFactorSender
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.MultiFactorAuth;

public interface ITwoFactorSender
{
  TwoFactorSenderType TypeId { get; }

  bool IsPersistentCode { get; }

  string Type { get; }

  bool ShowTextBox { get; }

  string ButtonName { get; }

  string ButtonToolTip { get; }

  (string message, bool isError) SendAcceptRequest(
    IEnumerable<(int companyId, Guid userId, int twoFactorLevel)> users,
    string innerCorrelation,
    Dictionary<string, string> customData);

  int ResendAfter { get; }
}
