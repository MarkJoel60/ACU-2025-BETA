// Decompiled with JetBrains decompiler
// Type: PX.SM.IHistoryFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.SM;

public interface IHistoryFilter
{
  [Obsolete("This field is obsolete and will be removed in future Acumatica versions")]
  bool? IsActive { get; set; }

  string FieldName { get; set; }

  string Value { get; set; }

  string Value2 { get; set; }
}
