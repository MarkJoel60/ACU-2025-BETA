// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.SiteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

/// <summary>
/// Represents Site Field
/// The Selector will return only active sites.
/// </summary>
[PXRestrictor(typeof (Where<INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new Type[] {typeof (INSite.siteCD)}, CacheGlobal = true, ShowWarning = true)]
public class SiteAttribute : SiteAnyAttribute
{
  public SiteAttribute()
  {
  }

  public SiteAttribute(bool allowTransit)
    : base(allowTransit)
  {
  }

  public SiteAttribute(Type whereType, bool allowTransit)
    : base(whereType, allowTransit)
  {
  }

  public SiteAttribute(Type whereType, bool validateAccess, bool allowTransit)
    : base(whereType, validateAccess, allowTransit)
  {
  }
}
