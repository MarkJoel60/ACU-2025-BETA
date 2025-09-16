// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAliasedDataFieldValue`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Automatically takes alias from declaring type.</summary>
public class PXAliasedDataFieldValue<Field>(object value) : PXDataFieldValue<Field>(typeof (Field).DeclaringType?.Name, value, value == null ? PXComp.ISNULL : PXComp.EQ)
  where Field : IBqlField
{
}
