// Decompiled with JetBrains decompiler
// Type: PX.Data.AUTemplateCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class AUTemplateCache : IPrefetchable, IPXCompanyDependent
{
  public AUTemplate[] Items;

  public void Prefetch()
  {
    this.Items = PXSelectBase<AUTemplate, PXSelectReadonly<AUTemplate>.Config>.Select(new PXGraph()).FirstTableItems.OrderBy<AUTemplate, string>((Func<AUTemplate, string>) (_ => _.Description)).ToArray<AUTemplate>();
  }
}
