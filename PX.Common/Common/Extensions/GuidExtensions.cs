// Decompiled with JetBrains decompiler
// Type: PX.Common.Extensions.GuidExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;

#nullable disable
namespace PX.Common.Extensions;

public static class GuidExtensions
{
  public static bool IsNullOrEmpty(this Guid? value)
  {
    return !value.HasValue || value.Value == Guid.Empty;
  }
}
