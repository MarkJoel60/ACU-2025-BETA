// Decompiled with JetBrains decompiler
// Type: PX.Data.PXMassActionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Specifies if action is availiable for the mass operations (e.g. Mass Actions in GI).
/// </summary>
/// <remarks>Marker attribute. Can be placed either on an action or on the action's delegate.</remarks>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class PXMassActionAttribute : Attribute
{
}
