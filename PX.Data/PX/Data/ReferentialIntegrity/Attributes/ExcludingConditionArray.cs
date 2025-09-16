// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.ExcludingConditionArray
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>
/// A <see cref="T:PX.Common.TypeArray" /> of <see cref="T:PX.Data.ReferentialIntegrity.Attributes.IByForeignTableExcludingCondition" /> types
/// (i.e. <see cref="T:PX.Data.ReferentialIntegrity.Attributes.ExcludeWhen`1.Joined`1.Satisfies`1" />
/// or <see cref="T:PX.Data.ReferentialIntegrity.Attributes.ExcludeWhen`1.JoinedAsParent.Satisfies`1" />).
/// </summary>
internal class ExcludingConditionArray : TypeArrayOf<IByForeignTableExcludingCondition>
{
}
