// Decompiled with JetBrains decompiler
// Type: PX.Data.Coalesce`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class Coalesce<TSearch1, TSearch2, TSearch3> : 
  Coalesce<TSearch1, Coalesce<TSearch2, TSearch3>>
  where TSearch1 : IBqlSearch, new()
  where TSearch2 : IBqlSearch, new()
  where TSearch3 : IBqlSearch, new()
{
}
