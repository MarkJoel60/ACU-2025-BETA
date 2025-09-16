// Decompiled with JetBrains decompiler
// Type: PX.Data.MacroVariablesManagerExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public static class MacroVariablesManagerExtensions
{
  public static object TryResolveExt(
    this IMacroVariablesManager macroVariablesManager,
    object value,
    PXCache cache,
    string fieldName,
    object row)
  {
    string definition = value as string;
    return macroVariablesManager.IsVariable(definition) ? macroVariablesManager.ResolveExt(definition, cache, fieldName, row) : value;
  }
}
