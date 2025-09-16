// Decompiled with JetBrains decompiler
// Type: PX.Data.IMacroVariablesManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public interface IMacroVariablesManager
{
  bool IsVariable(string definition);

  object Resolve(string definition, System.Type dataType);

  object ResolveExt(string definition, PXCache cache, string fieldName, object row);
}
