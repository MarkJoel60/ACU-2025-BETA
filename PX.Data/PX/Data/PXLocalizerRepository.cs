// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLocalizerRepository
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Localizers;

#nullable disable
namespace PX.Data;

/// <exclude />
public static class PXLocalizerRepository
{
  public static PXSiteMapLocalizer SiteMapLocalizer { get; private set; }

  public static PXReportItemLocalizer ReportLocalizer { get; private set; }

  public static PXSpecialLocalizer SpecialLocalizer { get; private set; }

  public static PXUIFieldLocalizer UIFieldLocalizer { get; private set; }

  public static PXListLocalizer ListLocalizer { get; private set; }

  public static PXControlLocalizer ControlLocalizer { get; private set; }

  public static PXDateTimeLocalizer DateTimeLocalizer { get; private set; }

  static PXLocalizerRepository() => PXLocalizerRepository.InitializeRepositoryItems();

  private static void InitializeRepositoryItems()
  {
    PXLocalizerRepository.SiteMapLocalizer = new PXSiteMapLocalizer();
    PXLocalizerRepository.ReportLocalizer = new PXReportItemLocalizer();
    PXLocalizerRepository.SpecialLocalizer = new PXSpecialLocalizer();
    PXLocalizerRepository.UIFieldLocalizer = new PXUIFieldLocalizer();
    PXLocalizerRepository.ListLocalizer = new PXListLocalizer();
    PXLocalizerRepository.ControlLocalizer = new PXControlLocalizer();
    PXLocalizerRepository.DateTimeLocalizer = new PXDateTimeLocalizer();
  }
}
