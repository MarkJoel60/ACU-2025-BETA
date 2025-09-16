// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.ConversionResultExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Runtime.ExceptionServices;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateActions;

public static class ConversionResultExtensions
{
  public static TResult ThrowIfHasException<TResult>(this TResult conversionResult) where TResult : ConversionResult
  {
    if (conversionResult.Exception != null)
      ExceptionDispatchInfo.Capture(conversionResult.Exception).Throw();
    return conversionResult;
  }
}
