// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.Messages
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Objects.GDPR;

[PXLocalizable("GDPR Error")]
public static class Messages
{
  public const string Prefix = "GDPR Error";
  public const string NoConsent = "Consent to the processing of personal data has not been given or has expired.";
  public const string IsConsented = "Consented to the Processing of Personal Data";
  public const string DateOfConsent = "Date of Consent";
  public const string ConsentExpires = "Consent Expires";
  public const string ConsentExpired = "The consent has expired.";
  public const string ConsentDateNull = "No consent date has been specified.";
  public const string NotPseudonymized = "Not Pseudonymized";
  public const string Pseudonymized = "Pseudonymized";
  public const string Erased = "Erased";
  public const string NavigateDeleted = "A deleted contact cannot be opened.";
  public const string OpenContact = "Open Contact";
  public const string Pseudonymize = "Pseudonymize";
  public const string PseudonymizeAll = "Pseudonymize All";
  public const string Erase = "Erase";
  public const string EraseAll = "Erase All";
  public const string Restore = "Restore";
  public const string ContactOPTypeForIndex = "Opportunity Contact {0}";

  public static string GetLocal(string message)
  {
    return PXLocalizer.Localize(message, typeof (Messages).FullName);
  }
}
