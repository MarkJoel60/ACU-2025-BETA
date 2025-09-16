// Decompiled with JetBrains decompiler
// Type: PX.Data.InjectDependencyOnTypeLevelAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <exclude />
/// <summary>The attribute that is used in the attribute classes derived from <see cref="T:PX.Data.PXEventSubscriberAttribute" /> class to create the properties that need to be injected via
/// dependency injection, but do not directly depend on <see cref="T:PX.Data.PXGraph" /> or <see cref="T:PX.Data.PXCache" /> instances used by the attribute class.</summary>
[AttributeUsage(AttributeTargets.Property)]
[PXInternalUseOnly]
public sealed class InjectDependencyOnTypeLevelAttribute : Attribute
{
}
