// Decompiled with JetBrains decompiler
// Type: PX.Common.SingleCompanyIdExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common.Context;

#nullable disable
namespace PX.Common;

internal static class SingleCompanyIdExtensions
{
  internal const string SlotKey = "singleCompanyID";

  internal static int? GetSingleCompanyId(this ISlotStore _param0)
  {
    return _param0.Get<int?>("singleCompanyID");
  }

  internal static void SetSingleCompanyId(this ISlotStore _param0, int _param1)
  {
    _param0.Set("singleCompanyID", (object) _param1);
  }

  internal static void ClearSingleCompanyId(this ISlotStore _param0)
  {
    _param0.Set("singleCompanyID", (object) null);
  }
}
