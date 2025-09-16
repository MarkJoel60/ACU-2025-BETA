// Decompiled with JetBrains decompiler
// Type: PX.Common.Context.ISlotStorageProvider
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Common.Context;

internal interface ISlotStorageProvider : ISlotStore
{
  IEnumerable<KeyValuePair<string, object>> Items();

  void Clear();
}
