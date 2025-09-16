// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDialogButtons
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
[Flags]
public enum PXDialogButtons
{
  Nothing = 0,
  OK = 1,
  Cancel = 2,
  Abort = 4,
  Retry = 8,
  Ignore = 16, // 0x00000010
  Yes = 32, // 0x00000020
  No = 64, // 0x00000040
  OkCancel = Cancel | OK, // 0x00000003
  YesNo = No | Yes, // 0x00000060
  AbortRetryIgnore = Ignore | Retry | Abort, // 0x0000001C
}
