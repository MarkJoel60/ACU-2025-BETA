// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeFindOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Update.WebServices;

[Flags]
public enum PXExchangeFindOptions
{
  None = 0,
  Created = 1,
  Modified = 2,
  Changed = Modified | Created, // 0x00000003
  IncludeUncategorized = 4,
  IncludeDetails = 8,
  IncludeAttachments = 16, // 0x00000010
  IncludeDraft = 32, // 0x00000020
  IncludePrivate = 64, // 0x00000040
  PlainText = 128, // 0x00000080
  HeadersOnly = 256, // 0x00000100
}
