// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPluginRedirectException`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;

#nullable disable
namespace PX.Data;

public class PXPluginRedirectException<T>(T parameters) : PXPluginRedirectException("", PXBaseRedirectException.WindowMode.NewWindow, "plugin" + JsonConvert.SerializeObject((object) parameters))
  where T : PXPluginRedirectOptions
{
}
