// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.APIResponse
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

/// <summary>A class that holds the responses returned by the processing center.</summary>
public class APIResponse
{
  /// <summary>
  /// Must be <tt>true</tt> if the request was completed without any errors and <tt>false</tt> otherwise.
  /// </summary>
  public bool isSucess;
  /// <summary>
  /// Contains the error messages received from the processing center.
  /// </summary>
  public Dictionary<string, string> Messages;
  /// <summary>Specifies the error source.</summary>
  public CCError.CCErrorSource ErrorSource;

  /// <exclude />
  public APIResponse() => this.Messages = new Dictionary<string, string>();
}
