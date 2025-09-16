// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.IGIResultViewProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Maintenance.GI;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.GenericInquiry.Services;

internal interface IGIResultViewProcessor
{
  string GetGIRowStyle(
    PXGenericInqGrph graph,
    GenericResult row,
    IReadOnlyDictionary<string, string> warnings);

  string GetGICellStyle(
    PXGenericInqGrph graph,
    GenericResult row,
    string rowStyle,
    string fieldFullName,
    IReadOnlyDictionary<string, string> warnings);

  IReadOnlyDictionary<string, string> GetGIRowWarnings(PXGenericInqGrph graph, GenericResult row);

  bool CanSort(PXGenericInqGrph graph, string fieldName, GIResult row);
}
