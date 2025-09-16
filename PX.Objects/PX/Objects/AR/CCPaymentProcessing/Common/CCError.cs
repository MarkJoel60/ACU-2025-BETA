// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.CCError
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

/// <summary>A class that describes an error in processing a credit card payment.</summary>
public class CCError
{
  /// <summary>The error message.</summary>
  public string ErrorMessage;
  /// <summary>The source of the error.</summary>
  public CCError.CCErrorSource source;
  private static string[] _codes = new string[4]
  {
    "NON",
    "INT",
    "PRC",
    "NET"
  };
  private static string[] _descr = new string[4]
  {
    "No Error",
    "Internal Error",
    "Processing Center Error",
    "Network Error"
  };

  /// <summary>Retrieves the code of the error.</summary>
  /// <param name="aErrSrc">The error source.</param>
  public static string GetCode(CCError.CCErrorSource aErrSrc) => CCError._codes[(int) aErrSrc];

  /// <summary>Retrieves the description of the error.</summary>
  /// <param name="aErrSrc">The error source.</param>
  public static string GetDescription(CCError.CCErrorSource aErrSrc)
  {
    return CCError._descr[(int) aErrSrc];
  }

  /// <summary>Defines the error sources.</summary>
  public enum CCErrorSource
  {
    /// <summary>No error.</summary>
    None,
    /// <summary>An internal object error.</summary>
    Internal,
    /// <summary>A processing center error.</summary>
    ProcessingCenter,
    /// <summary>A network error.</summary>
    Network,
  }
}
