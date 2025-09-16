// Decompiled with JetBrains decompiler
// Type: PX.Data.StartsWith`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

/// <summary>
/// Checks if the preceding string operand starts with the <typeparamref name="TOperand" /> string.
/// Equivalent to SQL operator LIKE @P0 + '%'.
/// </summary>
public class StartsWith<TOperand> : 
  Like<Concat<TypeArrayOf<IBqlOperand>.FilledWith<TOperand, WildcardAny>>>
  where TOperand : IBqlOperand
{
}
