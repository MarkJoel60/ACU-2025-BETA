// Decompiled with JetBrains decompiler
// Type: PX.Api.PXSYMappingActiveWithoutBPMSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections;

#nullable disable
namespace PX.Api;

public class PXSYMappingActiveWithoutBPMSelectorAttribute : PXSYMappingActiveSelector
{
  protected override IEnumerable GetRecords()
  {
    return (IEnumerable) PXSYMappingSelector.GetMappings<SYMappingActive>(new PXView(this._Graph, true, PXSelectBase<SYMappingActive, PXSelect<SYMappingActive, Where<SYMappingActive.mappingType, Equal<SYMapping.mappingType.typeImport>, And<SYMapping.isActive, Equal<True>, And<SYMappingActive.providerType, NotEqual<BPEventProviderType>>>>>.Config>.GetCommand()));
  }
}
