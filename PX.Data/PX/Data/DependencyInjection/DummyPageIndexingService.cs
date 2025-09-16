// Decompiled with JetBrains decompiler
// Type: PX.Data.DependencyInjection.DummyPageIndexingService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.DependencyInjection;

public class DummyPageIndexingService : IPXPageIndexingService
{
  public void Clear()
  {
  }

  public string GetGraphType(string url) => (string) null;

  public string GetGraphTypeByScreenID(string screenID) => (string) null;

  public string GetScreenIDFromGraphType(System.Type g) => (string) null;

  public string GetScreenIDFromGraphType(string graphType) => (string) null;

  public IList<string> GetScreensIDFromGraphType(System.Type g) => (IList<string>) null;

  public string[] GetDataMembers(string graphType) => (string[]) null;

  public Dictionary<string, string> GetGraphAdditionalCollectedData(string graphType)
  {
    return (Dictionary<string, string>) null;
  }

  public string[] GetScreenUsedCommands(string graphType) => (string[]) null;

  public string GetPrimaryView(string graphType) => (string) null;

  public string GetPrimaryViewForScreen(string screenId) => (string) null;

  public string GetUDFTypeField(string graphType) => (string) null;

  public bool HasBPEventsIndicator(string screenID) => false;

  public IEnumerable<string> GetScreensByModule(string moduleID)
  {
    return (IEnumerable<string>) Array.Empty<string>();
  }
}
