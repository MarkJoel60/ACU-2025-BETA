// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlTableMapper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>The interface that defines the mapping between the fields of DAC classes involved in a database operation (such as DAC classes that are involved in a <tt>Union</tt> operation) and the fields of a resulting or a shared DAC class.</summary>
public interface IBqlTableMapper
{
  IBqlFieldMappingResolver GetMapper();

  System.Type TableType { get; }
}
