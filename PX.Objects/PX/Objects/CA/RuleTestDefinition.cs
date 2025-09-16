// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.RuleTestDefinition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

public class RuleTestDefinition
{
  public RuleTest TestInfo { get; set; }

  public CABankTranRule Rule { get; set; }

  public IEnumerable<CABankTran> Matches { get; set; }

  public IEnumerable<CABankTran> NotMatches { get; set; }
}
