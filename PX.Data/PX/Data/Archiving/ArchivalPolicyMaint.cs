// Decompiled with JetBrains decompiler
// Type: PX.Data.Archiving.ArchivalPolicyMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Caching;
using PX.Data.Archiving.DAC;
using System.Linq;

#nullable disable
namespace PX.Data.Archiving;

public class ArchivalPolicyMaint : PXGraph<ArchivalPolicyMaint>
{
  public PXSave<ArchivalSetup> Save;
  public PXCancel<ArchivalSetup> Cancel;
  public PXSelect<ArchivalSetup> Setup;
  public PXSelectOrderBy<ArchivalPolicy, OrderBy<Asc<ArchivalPolicy.typeName>>> Policies;

  [InjectDependency]
  private ICacheControl<PageCache> PageCacheControl { get; set; }

  public override void Persist()
  {
    int num = this.Policies.Cache.Inserted.Cast<ArchivalPolicy>().Any<ArchivalPolicy>() ? 1 : (this.Policies.Cache.Deleted.Cast<ArchivalPolicy>().Any<ArchivalPolicy>() ? 1 : 0);
    base.Persist();
    if (num == 0)
      return;
    this.PageCacheControl?.InvalidateCache();
  }
}
