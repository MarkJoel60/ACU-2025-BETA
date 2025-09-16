// Decompiled with JetBrains decompiler
// Type: PX.Api.IPXSYProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.Api;

public interface IPXSYProvider
{
  string DefaultFileExtension { get; }

  string ProviderName { get; }

  PXStringState[] GetParametersDefenition();

  PXSYParameter[] GetParameters();

  void SetParameters(PXSYParameter[] parameters);

  string[] GetSchemaObjects();

  PXFieldState[] GetSchemaFields(string objectName);

  PXSYTable Import(
    string objectName,
    string[] fieldNames,
    PXSYFilterRow[] filters,
    string lastTimeStamp,
    PXSYSyncTypes syncType);

  void Export(
    string objectName,
    PXSYTable table,
    bool breakOnError,
    System.Action<SyProviderRowResult> callback);
}
