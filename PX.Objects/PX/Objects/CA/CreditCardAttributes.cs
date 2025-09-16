// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CreditCardAttributes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.CA;

public static class CreditCardAttributes
{
  public const string CardNumber = "CCDNUM";
  public const string ExpirationDate = "EXPDATE";
  public const string NameOnCard = "NAMEONCC";
  public const string CVV = "CVV";
  public const string CCPID = "CCPID";
  private static string[] IDS = new string[5]
  {
    "CCDNUM",
    "EXPDATE",
    "NAMEONCC",
    nameof (CVV),
    nameof (CCPID)
  };
  private static string[] EntryMasks = new string[5]
  {
    "0000-0000-0000-0000",
    "00/0000",
    string.Empty,
    "000",
    ""
  };
  private static string[] ValidationRegexps = new string[5]
  {
    "",
    "",
    string.Empty,
    "",
    ""
  };

  public static string GetID(CreditCardAttributes.AttributeName aID)
  {
    return CreditCardAttributes.IDS[(int) aID];
  }

  public static string GetMask(CreditCardAttributes.AttributeName aID)
  {
    return CreditCardAttributes.EntryMasks[(int) aID];
  }

  public static string GetValidationRegexp(CreditCardAttributes.AttributeName aID)
  {
    return CreditCardAttributes.ValidationRegexps[(int) aID];
  }

  public enum AttributeName
  {
    CardNumber,
    ExpirationDate,
    NameOnCard,
    CCVCode,
    CCPID,
  }

  public static class MaskDefaults
  {
    public const string CardNumber = "0000-0000-0000-0000";
    public const string ExpirationDate = "00/0000";
    public const string DefaultIdentifier = "****-****-****-0000";
    public const string CVV = "000";
    public const string CCPID = "";
  }

  public static class ValidationRegexp
  {
    public const string CardNumber = "";
    public const string ExpirationDate = "";
    public const string DefaultIdentifier = "";
    public const string CVV = "";
    public const string CCPID = "";
  }
}
