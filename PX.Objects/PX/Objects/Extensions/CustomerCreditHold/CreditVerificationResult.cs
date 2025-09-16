// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.CustomerCreditHold.CreditVerificationResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.Extensions.CustomerCreditHold;

public class CreditVerificationResult
{
  public bool Hold { get; set; }

  public bool Failed { get; set; }

  public CreditVerificationResult.EnforceType Enforce { get; set; }

  public string ErrorMessage { get; set; }

  public string ErrorField { get; set; } = "customerID";

  public enum EnforceType
  {
    None,
    AdminHold,
    CreditHold,
  }
}
