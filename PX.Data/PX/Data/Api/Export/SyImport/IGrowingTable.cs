// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.SyImport.IGrowingTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;

#nullable disable
namespace PX.Data.Api.Export.SyImport;

internal interface IGrowingTable
{
  void MoveNextRow();

  bool IsLastRow();

  void AddSelectResults(ViewSelectResults selectResults);

  bool HasEnumValues { get; set; }

  int? CountResultsForView(string viewId);

  void ExportValues(SyImportProcessor.SyExternalValues dic);

  bool IsEmptyViewResults(string viewId);

  NativeRowWrapper GetNativeRow(string viewName);
}
