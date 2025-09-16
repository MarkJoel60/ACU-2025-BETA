// Decompiled with JetBrains decompiler
// Type: PX.Data.MessageButtons
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Defines possible sets of standard buttons that can be
/// displayed in a dialog window created by the <tt>Ask()</tt>
/// method.</summary>
public enum MessageButtons
{
  /// <summary>Only the OK button is displayed.</summary>
  OK,
  /// <summary>The OK and Cancel buttons are displayed.</summary>
  OKCancel,
  /// <summary>The Abort, Retry, and Ignore buttons are displayed.</summary>
  AbortRetryIgnore,
  /// <summary>The Yes, No, and Cancel buttons are displayed.</summary>
  YesNoCancel,
  /// <summary>The Yes and No buttons are displayed.</summary>
  YesNo,
  /// <summary>The Retry and Cancel buttons are displayed.</summary>
  RetryCancel,
  /// <summary>No buttons are displayed.</summary>
  None,
}
