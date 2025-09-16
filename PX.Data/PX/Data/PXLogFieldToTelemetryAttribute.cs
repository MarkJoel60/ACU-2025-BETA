// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLogFieldToTelemetryAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Indicates that a DAC property's value is stored by telemetry
/// as the <see cref="!:PX.Telemetry.RequestInfoEx.PrimaryViewKeys" /> property of a request. The attribute is assigned to a DAC property.
/// </summary>
/// <remarks>
/// For primary items of a graph, their keys are stored in the <see cref="!:PX.Telemetry.RequestInfoEx.PrimaryViewKeys" /> telemetry property of an HTTP request.
/// However for instance, if a graph has a <see cref="T:PX.Data.PXFilter`1" /> data view, the keys of the corresponding DAC are not stored in telemetry.
/// If you need to store these keys in telemetry, you can mark
/// the respective properties of the <see cref="T:PX.Data.PXFilter`1" />'s type argument (that is, of the DAC) with PXLogFieldToTelemetryAttribute.
/// </remarks>
[PXInternalUseOnly]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class PXLogFieldToTelemetryAttribute : PXEventSubscriberAttribute
{
}
