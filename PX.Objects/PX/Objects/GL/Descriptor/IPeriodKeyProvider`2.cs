// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Descriptor.IPeriodKeyProvider`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Abstractions.Periods;

#nullable disable
namespace PX.Objects.GL.Descriptor;

public interface IPeriodKeyProvider<out TKey, out TSourcesSpecificationCollection> : 
  IPeriodKeyProviderBase
  where TKey : OrganizationDependedPeriodKey, new()
  where TSourcesSpecificationCollection : PeriodKeyProviderBase.SourcesSpecificationCollectionBase
{
  TKey GetKey(PXGraph graph, PXCache attributeCache, object extRow);

  TSourcesSpecificationCollection GetSourcesSpecification(PXCache cache, object row);
}
