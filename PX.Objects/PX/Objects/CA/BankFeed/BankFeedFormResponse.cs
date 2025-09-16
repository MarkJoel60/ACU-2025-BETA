// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.BankFeedFormResponse
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA.BankFeed;

internal class BankFeedFormResponse
{
  public string AccessToken { get; set; }

  public string ItemID { get; set; }

  public string InstitutionName { get; set; }

  public string InstitutionID { get; set; }

  public IEnumerable<BankFeedAccount> Accounts { get; set; }
}
