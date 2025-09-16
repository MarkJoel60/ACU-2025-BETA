// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PreventRecursionCall
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// The service scope that allows you to execute a method without implicit recursion.
/// For instance, it can be used when a field event triggers an update that triggers this event once again, which leads to infinite recursion.
/// </summary>
/// <remarks>
/// The class uses slots in <see cref="T:PX.Common.PXContext" />. It gives a name of the caller member according to the source code caller info,
/// however it could be specified manually.
/// </remarks>
[PXInternalUseOnly]
public static class PreventRecursionCall
{
  /// <summary>
  /// Executes the action and prevents subsequent calls of this method inside this action.
  /// </summary>
  /// <param name="action">The action.</param>
  /// <param name="memberName">The caller member name, which can be specified manually, but is usually filled by the compiler.</param>
  /// <param name="sourceFilePath">The source file path.</param>
  /// <param name="sourceLineNumber">The source line number.</param>
  public static void Execute(
    Action action,
    [CallerMemberName] string memberName = "",
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0)
  {
    string key = memberName + sourceFilePath + sourceLineNumber.ToString();
    if (PXContext.GetSlot<bool>(key))
      return;
    try
    {
      PXContext.SetSlot<bool>(key, true);
      action();
    }
    finally
    {
      PXContext.SetSlot<bool>(key, false);
    }
  }
}
