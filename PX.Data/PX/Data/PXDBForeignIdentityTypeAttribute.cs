// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBForeignIdentityTypeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Indicates that the target field holds the name of a DAC,
/// which would use the table as a source of its identity (see the <see cref="T:PX.Data.PXDBForeignIdentityAttribute" />).
/// </summary>
[PXDBString(IsUnicode = true, IsFixed = false)]
public class PXDBForeignIdentityTypeAttribute : PXAggregateAttribute
{
}
