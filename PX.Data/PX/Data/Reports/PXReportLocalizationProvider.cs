// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.PXReportLocalizationProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Parser;
using PX.Data.Localization.Providers;
using PX.Reports;
using PX.Reports.Controls;
using PX.SM;
using PX.Translation;
using System.Threading;

#nullable disable
namespace PX.Data.Reports;

public class PXReportLocalizationProvider : LocalizationProvider
{
  private readonly IPXTranslationProvider _translationProvider;
  private const string LOCALIZATION_SLOT_KEY_BASE = "Report_Localization_";

  public PXReportLocalizationProvider(IPXTranslationProvider translationProvider)
  {
    this._translationProvider = translationProvider;
  }

  private static string LocalizationSlotKey
  {
    get => "Report_Localization_" + Thread.CurrentThread.CurrentCulture.Name;
  }

  private static System.Type[] LocalizationSlotPars
  {
    get
    {
      return new System.Type[4]
      {
        typeof (Locale),
        typeof (LocalizationValue),
        typeof (LocalizationTranslation),
        typeof (LocalizationResource)
      };
    }
  }

  public virtual string Localize(string value, string key) => PXLocalizer.Localize(value, key);

  public virtual void LocalizeReport(Report report)
  {
    PXLocalizerRepository.ReportLocalizer.Localize(report);
  }

  public virtual void SetSlotDictionary(string locale)
  {
    PXContext.SetSlot<PXDictionaryManager>(PXDatabase.GetSlot<PXReportLocalizationProvider.Definition, IPXTranslationProvider>(PXReportLocalizationProvider.LocalizationSlotKey, this._translationProvider, PXReportLocalizationProvider.LocalizationSlotPars).Manager);
  }

  public virtual void LocalizeConstanst(ExpressionNode node, string reportName, string textBoxName)
  {
    PXLocalizerRepository.ReportLocalizer.LocalizeFormulaConstants(node, reportName, textBoxName);
  }

  public virtual string LocalizeExportFileName(Report report)
  {
    return PXLocalizerRepository.ReportLocalizer.LocalizeExportFileName(report);
  }

  private sealed class Definition : IPrefetchable<IPXTranslationProvider>, IPXCompanyDependent
  {
    public PXDictionaryManager Manager;

    public void Prefetch(IPXTranslationProvider translationProvider)
    {
      using (new PXConnectionScope())
        this.Manager = PXDictionaryManager.Load(Thread.CurrentThread.CurrentCulture.Name, false, true, translationProvider);
    }
  }
}
