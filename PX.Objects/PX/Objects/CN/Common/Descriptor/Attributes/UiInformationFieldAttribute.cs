// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Descriptor.Attributes.UiInformationFieldAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CN.Common.Descriptor.Attributes;

/// <summary>
/// This attribute mark property as information field with dynamic colomns in grid.
/// Used in <see cref="T:PX.Objects.CN.Common.Services.CommonAttributeColumnCreator" />.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class UiInformationFieldAttribute : Attribute
{
}
