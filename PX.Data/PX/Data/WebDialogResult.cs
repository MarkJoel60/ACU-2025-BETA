// Decompiled with JetBrains decompiler
// Type: PX.Data.WebDialogResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Defines values that indicate which button the user cliked in
/// the dialog box opened by the <tt>Ask()</tt> method.</summary>
public enum WebDialogResult
{
  /// <summary>None of the buttons was clicked.</summary>
  None,
  /// <summary>The user clicked OK.</summary>
  OK,
  /// <summary>The user clicked Cancel.</summary>
  Cancel,
  /// <summary>The user clicked Abort.</summary>
  Abort,
  /// <summary>The user clicked Retry.</summary>
  Retry,
  /// <summary>The user clicked Ignore.</summary>
  Ignore,
  /// <summary>The user clicked Yes.</summary>
  Yes,
  /// <summary>The user clicked No.</summary>
  No,
}
