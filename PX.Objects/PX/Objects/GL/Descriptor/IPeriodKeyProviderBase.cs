// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Descriptor.IPeriodKeyProviderBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL.Descriptor;

public interface IPeriodKeyProviderBase
{
  object[] GetKeyAsArrayOfObjects(PXGraph graph, PXCache attributeCache, object extRow);

  bool IsKeyDefined(PXGraph graph, PXCache attributeCache, object extRow);

  IEnumerable<PeriodKeyProviderBase.SourceSpecificationItem> GetSourceSpecificationItems(
    PXCache cache,
    object row);

  PeriodKeyProviderBase.SourceSpecificationItem GetMainSourceSpecificationItem(
    PXCache cache,
    object row);

  bool IsKeySourceValuesEquals(PXCache cache, object oldRow, object newRow);
}
