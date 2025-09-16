// Decompiled with JetBrains decompiler
// Type: PX.Data.LanguageSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class LanguageSelectorAttribute : PXCustomSelectorAttribute
{
  private LocaleMaintenance _localeGraph;

  private LocaleMaintenance LocaleGraph
  {
    get
    {
      if (this._localeGraph == null)
        this._localeGraph = this._Graph is LocaleMaintenance ? (LocaleMaintenance) this._Graph : PXGraph.CreateInstance<LocaleMaintenance>();
      return this._localeGraph;
    }
  }

  public LanguageSelectorAttribute()
    : base(typeof (LocaleMaintenance.AlternativeRecord.languageName), typeof (LocaleMaintenance.AlternativeRecord.languageName), typeof (LocaleMaintenance.AlternativeRecord.nativeName))
  {
    this.DescriptionField = typeof (LocaleMaintenance.AlternativeRecord.nativeName);
  }

  public virtual IEnumerable GetRecords() => (IEnumerable) this.LocaleGraph.GetLanguages();

  public override void DescriptionFieldCommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
  }
}
