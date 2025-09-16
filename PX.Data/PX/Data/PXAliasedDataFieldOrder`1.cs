// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAliasedDataFieldOrder`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Automatically takes alias from declaring type.</summary>
public class PXAliasedDataFieldOrder<Field> : PXDataFieldOrder<Field> where Field : IBqlField
{
  public PXAliasedDataFieldOrder()
    : base(typeof (Field).DeclaringType?.Name)
  {
  }

  public PXAliasedDataFieldOrder(bool isDesc)
    : base(typeof (Field).DeclaringType?.Name, isDesc)
  {
  }
}
