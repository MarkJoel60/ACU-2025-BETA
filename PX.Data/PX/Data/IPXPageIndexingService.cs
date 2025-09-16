// Decompiled with JetBrains decompiler
// Type: PX.Data.IPXPageIndexingService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public interface IPXPageIndexingService
{
  void Clear();

  string GetGraphType(string url);

  string GetGraphTypeByScreenID(string screenID);

  string GetScreenIDFromGraphType(System.Type g);

  string GetScreenIDFromGraphType(string graphTypeName);

  IList<string> GetScreensIDFromGraphType(System.Type g);

  string[] GetDataMembers(string graphType);

  Dictionary<string, string> GetGraphAdditionalCollectedData(string graphType);

  string[] GetScreenUsedCommands(string graphType);

  string GetPrimaryView(string graphType);

  string GetPrimaryViewForScreen(string screenId);

  string GetUDFTypeField(string graphType);

  bool HasBPEventsIndicator(string screenID);

  IEnumerable<string> GetScreensByModule(string moduleID);
}
