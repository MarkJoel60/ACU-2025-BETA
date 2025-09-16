// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlFieldOf`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Represents a field declared in a specific <typeparamref name="TTable" />. The field is either bound or not bound to a database column.</summary>
public interface IBqlFieldOf<in TTable> : IBqlField, IBqlOperand where TTable : IBqlTable
{
}
