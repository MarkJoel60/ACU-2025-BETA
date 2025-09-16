// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlFieldMapper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>The interface that defines the rules of the relationship between the fields of two DAC classes, but does not allow these rules to be read.</summary>
public interface IBqlFieldMapper
{
  IBqlFieldMapper Map<TFbqlSet>() where TFbqlSet : IFbqlSet;
}
