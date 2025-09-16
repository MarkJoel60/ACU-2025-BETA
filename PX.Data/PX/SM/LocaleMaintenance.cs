// Decompiled with JetBrains decompiler
// Type: PX.SM.LocaleMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Caching;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Localization;
using PX.Data.Maintenance;
using PX.Metadata;
using PX.Translation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable enable
namespace PX.SM;

public class LocaleMaintenance : PXGraph<
#nullable disable
LocaleMaintenance>
{
  protected bool doCopyLocaleSettings;
  public PXSelectOrderBy<Locale, OrderBy<Asc<Locale.number>>> Locales;
  public PXSelect<LocaleFormat, Where<LocaleFormat.formatID, Equal<Current<Locale.formatID>>>> Formats;
  public PXSavePerRow<Locale> Save;
  public PXCancel<Locale> Cancel;
  public PXAction<Locale> EditFormatCommand;
  public PXFilter<LocaleMaintenance.AlternativeFilter> AlternativeHeader;
  public PXSelect<LocaleMaintenance.AlternativeRecord> AlternativeDetails;
  public PXAction<Locale> SetUpAlternatives;

  [InjectDependency]
  protected ICacheControl<PageCache> PageCacheControl { get; set; }

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  [InjectDependency]
  private ILocalizableFieldService LocalizableFieldService { get; set; }

  public LocaleMaintenance()
  {
    this.OnAfterPersist += (System.Action<PXGraph>) (g =>
    {
      LocaleMaintenance localeMaintenance = (LocaleMaintenance) g;
      localeMaintenance.PageCacheControl.InvalidateCache();
      localeMaintenance.ScreenInfoCacheControl.InvalidateCache();
    });
  }

  public virtual List<LocaleMaintenance.AlternativeRecord> GetLanguages()
  {
    List<LocaleMaintenance.AlternativeRecord> languages = new List<LocaleMaintenance.AlternativeRecord>();
    HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (PXResult<Locale> pxResult in this.Locales.Select())
    {
      Locale locale = (Locale) pxResult;
      CultureInfo cultureInfo1 = new CultureInfo(locale.LocaleName);
      if (stringSet.Add(cultureInfo1.TwoLetterISOLanguageName))
      {
        CultureInfo cultureInfo2 = new CultureInfo(cultureInfo1.TwoLetterISOLanguageName);
        languages.Add(new LocaleMaintenance.AlternativeRecord()
        {
          LanguageName = cultureInfo2.TwoLetterISOLanguageName,
          NativeName = cultureInfo2.NativeName.Replace(Convert.ToChar(173).ToString(), string.Empty),
          IsAlternative = locale.IsAlternative
        });
      }
    }
    return languages;
  }

  [PXUIField(DisplayName = "Locale Preferences ", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton(Tooltip = "Edit locale preferences for the current locale.", IsLockedOnToolbar = true)]
  public IEnumerable editFormatCommand(PXAdapter adapter)
  {
    Locale current = this.Locales.Current;
    if (current == null)
      throw new Exception("A locale is not selected.");
    if (!(this.Formats.View.SelectSingle() is LocaleFormat localeFormat))
      localeFormat = this.Formats.Insert(new LocaleFormat());
    if (localeFormat != null && !current.FormatID.HasValue)
    {
      current.FormatID = localeFormat.FormatID;
      this.Locales.Update(current);
    }
    this.Formats.Current = localeFormat;
    int num = (int) this.Formats.AskExt(true);
    return adapter.Get();
  }

  protected void AlternativeRecord_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    e.Cancel = true;
  }

  protected void AlternativeFilter_DefaultLanguageName_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    LocaleMaintenance.AlternativeFilter row = (LocaleMaintenance.AlternativeFilter) e.Row;
    if (!(e.OldValue is string) || string.IsNullOrEmpty((string) e.OldValue))
    {
      if (!string.IsNullOrEmpty(row.DefaultLanguageName))
      {
        foreach (PXResult<LocaleMaintenance.AlternativeRecord> pxResult in this.AlternativeDetails.Select())
        {
          LocaleMaintenance.AlternativeRecord alternativeRecord = (LocaleMaintenance.AlternativeRecord) pxResult;
          alternativeRecord.IsAlternative = new bool?(!string.Equals(row.DefaultLanguageName, alternativeRecord.LanguageName, StringComparison.OrdinalIgnoreCase));
          this.AlternativeDetails.Update(alternativeRecord);
        }
      }
    }
    else if (string.IsNullOrEmpty(row.DefaultLanguageName))
    {
      foreach (PXResult<LocaleMaintenance.AlternativeRecord> pxResult in this.AlternativeDetails.Select())
      {
        LocaleMaintenance.AlternativeRecord alternativeRecord = (LocaleMaintenance.AlternativeRecord) pxResult;
        alternativeRecord.IsAlternative = new bool?(false);
        this.AlternativeDetails.Update(alternativeRecord);
      }
    }
    else
    {
      foreach (PXResult<LocaleMaintenance.AlternativeRecord> pxResult in this.AlternativeDetails.Select())
      {
        LocaleMaintenance.AlternativeRecord alternativeRecord = (LocaleMaintenance.AlternativeRecord) pxResult;
        if (string.Equals((string) e.OldValue, alternativeRecord.LanguageName, StringComparison.OrdinalIgnoreCase))
        {
          alternativeRecord.IsAlternative = new bool?(true);
          this.AlternativeDetails.Update(alternativeRecord);
        }
      }
    }
    this.AlternativeHeader.Cache.IsDirty = false;
    this.AlternativeDetails.Cache.IsDirty = false;
  }

  protected void AlternativeFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled<LocaleMaintenance.AlternativeRecord.isAlternative>(this.AlternativeDetails.Cache, (object) null, e.Row != null && !string.IsNullOrEmpty(((LocaleMaintenance.AlternativeFilter) e.Row).DefaultLanguageName));
  }

  protected IEnumerable alternativeDetails()
  {
    if (this.AlternativeDetails.Cache.Inserted.Count() == 0L)
    {
      foreach (LocaleMaintenance.AlternativeRecord language in this.GetLanguages())
        this.AlternativeDetails.Insert(language);
    }
    foreach (LocaleMaintenance.AlternativeRecord alternativeRecord in this.AlternativeDetails.Cache.Inserted)
    {
      if (string.IsNullOrEmpty(this.AlternativeHeader.Current.DefaultLanguageName) || !string.Equals(alternativeRecord.LanguageName, this.AlternativeHeader.Current.DefaultLanguageName, StringComparison.OrdinalIgnoreCase))
        yield return (object) alternativeRecord;
    }
    this.AlternativeDetails.Cache.IsDirty = false;
  }

  [PXButton(IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Set Up Languages")]
  protected IEnumerable setUpAlternatives(PXAdapter adapter)
  {
    if (this.AlternativeHeader.View.Answer == WebDialogResult.None)
    {
      this.Persist();
      foreach (PXResult<Locale> pxResult in this.Locales.Select())
      {
        Locale locale = (Locale) pxResult;
        bool? isDefault = locale.IsDefault;
        bool flag = true;
        if (isDefault.GetValueOrDefault() == flag & isDefault.HasValue)
        {
          this.AlternativeHeader.Current.DefaultLanguageName = new CultureInfo(locale.LocaleName).TwoLetterISOLanguageName;
          break;
        }
      }
      this.AlternativeDetails.Cache.Clear();
      this.AlternativeDetails.Select();
    }
    if (this.AlternativeHeader.AskExt() == WebDialogResult.OK)
    {
      string defaultLanguage = this.AlternativeHeader.Current.DefaultLanguageName;
      HashSet<string> alternativeLanguages = new HashSet<string>();
      if (defaultLanguage != null)
      {
        foreach (LocaleMaintenance.AlternativeRecord alternativeRecord in this.AlternativeDetails.Cache.Inserted)
        {
          if (alternativeRecord.LanguageName != null && !string.Equals(alternativeRecord.LanguageName, defaultLanguage, StringComparison.OrdinalIgnoreCase))
          {
            bool? isAlternative = alternativeRecord.IsAlternative;
            bool flag = true;
            if (isAlternative.GetValueOrDefault() == flag & isAlternative.HasValue)
              alternativeLanguages.Add(alternativeRecord.LanguageName);
          }
        }
      }
      this.AlternativeDetails.Cache.IsDirty = false;
      this.AlternativeHeader.Cache.IsDirty = false;
      PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() =>
      {
        LocaleMaintenance graph = PXGraph.CreateInstance<LocaleMaintenance>();
        string b = "";
        foreach (PXResult<Locale> pxResult in graph.Locales.Select())
        {
          Locale locale = (Locale) pxResult;
          if (locale.LocaleName != null)
          {
            string letterIsoLanguageName = new CultureInfo(locale.LocaleName).TwoLetterISOLanguageName;
            bool? isDefault = locale.IsDefault;
            bool flag = true;
            if (isDefault.GetValueOrDefault() == flag & isDefault.HasValue)
              b = letterIsoLanguageName;
            locale.IsDefault = new bool?(defaultLanguage != null && string.Equals(defaultLanguage, letterIsoLanguageName, StringComparison.OrdinalIgnoreCase));
            locale.IsAlternative = new bool?(alternativeLanguages.Contains(letterIsoLanguageName));
            graph.Locales.Update(locale);
          }
        }
        graph.Persist();
        PXDBLocalizableStringAttribute.Clear();
        if (defaultLanguage == null)
          return;
        KeyValueExtHelper.CopyKeyValueExtensions<PXDBLocalizableStringAttribute>((Func<string, string>) (fieldName => fieldName + defaultLanguage.ToUpper()), (Func<TypeCode, Attribute, string, string, string>) ((typeCode, attribute, tableName, fieldName) =>
        {
          if (!graph.LocalizableFieldService.IsFieldEnabled(tableName, fieldName))
            return (string) null;
          string str = (string) null;
          if (typeCode == TypeCode.String && !((PXDBLocalizableStringAttribute) attribute).NonDB && !((PXDBLocalizableStringAttribute) attribute).IsProjection)
          {
            str = "ValueString";
            if (attribute is PXDBLocalizableStringAttribute && (((PXDBStringAttribute) attribute).Length <= 0 || ((PXDBStringAttribute) attribute).Length > 256 /*0x0100*/))
              str = "ValueText";
          }
          return str;
        }), !string.Equals(defaultLanguage, b, StringComparison.OrdinalIgnoreCase));
      }));
    }
    return adapter.Get();
  }

  internal void LocaleFormat_templateLocale_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs args)
  {
    LocaleFormat row = args.Row as LocaleFormat;
    string str = args.NewValue == null ? (string) null : args.NewValue.ToString();
    this.doCopyLocaleSettings = string.IsNullOrEmpty(row.TemplateLocale);
    if (this.doCopyLocaleSettings || string.IsNullOrEmpty(str))
      return;
    this.doCopyLocaleSettings = true;
    if (this.Formats.Ask("Warning", "Please confirm that you want to update the format settings of the current locale with the system locale defaults. Otherwise, original settings will be preserved.", MessageButtons.YesNo) != WebDialogResult.Yes)
      return;
    this.doCopyLocaleSettings = true;
  }

  internal void LocaleFormat_templateLocale_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs args)
  {
    LocaleFormat row = args.Row as LocaleFormat;
    if (!this.doCopyLocaleSettings || string.IsNullOrEmpty(row.TemplateLocale))
      return;
    CultureInfo cultureInfo = CultureInfo.GetCultureInfo(row.TemplateLocale);
    row.NumberDecimalSeporator = PXNumberSeparatorListAttribute.Encode(cultureInfo.NumberFormat.NumberDecimalSeparator);
    row.NumberGroupSeparator = PXNumberSeparatorListAttribute.Encode(cultureInfo.NumberFormat.NumberGroupSeparator);
    row.DateLongPattern = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.LongDatePattern);
    row.DateShortPattern = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.ShortDatePattern);
    row.DateTimePattern = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.FullDateTimePattern);
    row.TimeLongPattern = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.LongTimePattern);
    row.TimeShortPattern = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.ShortTimePattern);
    row.AMDesignator = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.AMDesignator);
    row.PMDesignator = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.PMDesignator);
  }

  internal void LocaleFormat_NumberDecimalSeporator_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row is LocaleFormat row && e.NewValue != null && PXLocalesProvider.CollationComparer.Equals(row.NumberGroupSeparator, e.NewValue.ToString()))
      throw new PXSetPropertyException("The Number Group and Number Decimal separators have to be different.");
  }

  internal void LocaleFormat_NumberDecimalSeporator_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    LocaleFormat row = e.Row as LocaleFormat;
    if (!string.Equals(this.Locales.Current.LocaleName, PXLocalesProvider.GetCurrentLocale(), StringComparison.InvariantCultureIgnoreCase))
      return;
    this.Formats.Cache.RaiseExceptionHandling<LocaleFormat.numberDecimalSeporator>((object) row, (object) row.NumberDecimalSeporator, (Exception) new PXSetPropertyException("You have to log out and then log in again for the changes to take effect.", PXErrorLevel.Warning));
  }

  internal void LocaleFormat_NumberGroupSeparator_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row is LocaleFormat row && e.NewValue != null && PXLocalesProvider.CollationComparer.Equals(row.NumberDecimalSeporator, e.NewValue.ToString()))
      throw new PXSetPropertyException("The Number Group and Number Decimal separators have to be different.");
  }

  internal void Locale_RowSelecting(PXCache cache, PXRowSelectingEventArgs e)
  {
    if (!(e.Row is Locale row))
      return;
    this.InitCultureName(row);
  }

  internal void Locale_LocaleName_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    string localeName = e.NewValue as string;
    if (localeName != null && !PXCultureSelectorAttribute.GetLocales().Any<Locale>((Func<Locale, bool>) (l => l.LocaleName.Equals(localeName, StringComparison.Ordinal))))
      throw new PXSetPropertyException<Locale.localeName>($"{new CultureNotFoundException().Message}: {localeName}");
  }

  protected virtual void SyncLanguages(PXCache cache, Locale newrow, Locale oldrow)
  {
    if (!string.IsNullOrEmpty(newrow.LocaleName))
    {
      string letterIsoLanguageName = new CultureInfo(newrow.LocaleName).TwoLetterISOLanguageName;
      string b = (string) null;
      if (!string.IsNullOrEmpty(oldrow.LocaleName))
        b = new CultureInfo(oldrow.LocaleName).TwoLetterISOLanguageName;
      if (string.Equals(letterIsoLanguageName, b, StringComparison.OrdinalIgnoreCase))
        return;
      newrow.IsAlternative = new bool?(false);
      newrow.IsDefault = new bool?(false);
      bool flag1 = false;
      bool flag2 = false;
      foreach (PXResult<Locale> pxResult in this.Locales.Select())
      {
        Locale locale = (Locale) pxResult;
        if (locale != newrow && !string.IsNullOrEmpty(locale.LocaleName))
        {
          flag1 |= locale.IsDefault.GetValueOrDefault();
          flag2 |= locale.IsAlternative.GetValueOrDefault();
          if (string.Equals(new CultureInfo(locale.LocaleName).TwoLetterISOLanguageName, letterIsoLanguageName, StringComparison.OrdinalIgnoreCase))
          {
            newrow.IsAlternative = locale.IsAlternative;
            newrow.IsDefault = locale.IsDefault;
          }
        }
      }
      if (flag2 && !flag1)
      {
        foreach (PXResult<Locale> pxResult in this.Locales.Select())
        {
          Locale row = (Locale) pxResult;
          if (row != newrow)
          {
            bool? isAlternative = row.IsAlternative;
            bool flag3 = true;
            if (isAlternative.GetValueOrDefault() == flag3 & isAlternative.HasValue && !string.IsNullOrEmpty(row.LocaleName))
            {
              row.IsAlternative = new bool?(false);
              cache.MarkUpdated((object) row);
            }
          }
        }
        this.Locales.View.RequestRefresh();
      }
      else
      {
        if (!flag1)
          return;
        bool? isDefault = newrow.IsDefault;
        bool flag4 = true;
        if (isDefault.GetValueOrDefault() == flag4 & isDefault.HasValue)
          return;
        newrow.IsAlternative = new bool?(true);
      }
    }
    else
    {
      newrow.IsAlternative = new bool?(false);
      newrow.IsDefault = new bool?(false);
      bool flag5 = false;
      bool flag6 = false;
      foreach (PXResult<Locale> pxResult in this.Locales.Select())
      {
        Locale locale = (Locale) pxResult;
        if (locale != newrow && !string.IsNullOrEmpty(locale.LocaleName))
        {
          flag5 |= locale.IsDefault.GetValueOrDefault();
          flag6 |= locale.IsAlternative.GetValueOrDefault();
        }
      }
      if (!flag6 || flag5)
        return;
      foreach (PXResult<Locale> pxResult in this.Locales.Select())
      {
        Locale row = (Locale) pxResult;
        if (row != newrow)
        {
          bool? isAlternative = row.IsAlternative;
          bool flag7 = true;
          if (isAlternative.GetValueOrDefault() == flag7 & isAlternative.HasValue && !string.IsNullOrEmpty(row.LocaleName))
          {
            row.IsAlternative = new bool?(false);
            cache.MarkUpdated((object) row);
          }
        }
      }
      this.Locales.View.RequestRefresh();
    }
  }

  internal void Locale_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    this.SyncLanguages(cache, (Locale) e.Row, new Locale());
  }

  internal void Locale_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    this.SyncLanguages(cache, (Locale) e.Row, (Locale) e.OldRow);
  }

  internal void Locale_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    this.SyncLanguages(cache, new Locale(), (Locale) e.Row);
  }

  internal void Locale_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    Locale row = (Locale) e.Row;
    if (!row.Number.HasValue)
    {
      Locale locale = (Locale) this.Locales.Select().AsEnumerable<PXResult<Locale>>().LastOrDefault<PXResult<Locale>>();
      row.Number = new short?(locale != null ? (short) ((int) locale.Number.GetValueOrDefault() + 1) : (short) 1);
    }
    this.InitCultureName(row);
  }

  private void InitCultureName(Locale l)
  {
    if (string.IsNullOrEmpty(l.LocaleName) || !string.IsNullOrEmpty(l.CultureReadableName))
      return;
    CultureInfo cultureInfo = new CultureInfo(l.LocaleName);
    l.CultureReadableName = cultureInfo.EnglishName;
  }

  internal void Locale_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is Locale row) || string.IsNullOrEmpty(row.LocaleName))
      return;
    this.CheckTranslation(row);
    this.CheckLocaleCount(row);
  }

  private void CheckLocaleCount(Locale l)
  {
    int? rowCount = PXSelectBase<Locale, PXSelect<Locale, Where<Locale.localeName, NotEqual<Required<Locale.localeName>>>>.Config>.Select((PXGraph) this, (object) l.LocaleName).RowCount;
    int num = 0;
    if (rowCount.GetValueOrDefault() == num & rowCount.HasValue)
      throw new PXException("The locale cannot be deleted. The system must have at least one locale.");
  }

  private void CheckTranslation(Locale l)
  {
    int? rowCount = PXSelectBase<LocalizationTranslation, PXSelect<LocalizationTranslation, Where<LocalizationTranslation.locale, Equal<Required<LocalizationTranslation.locale>>>>.Config>.Select((PXGraph) this, (object) l.LocaleName).RowCount;
    int num = 0;
    if (rowCount.GetValueOrDefault() > num & rowCount.HasValue)
      throw new PXException("The locale cannot be deleted. The translation dictionaries contain translations that use this locale.");
  }

  public class AlternativeFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _DefaultLanguageName;

    [PXDBString(2, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Default Language")]
    [LanguageSelector]
    public virtual string DefaultLanguageName
    {
      get => this._DefaultLanguageName;
      set => this._DefaultLanguageName = value;
    }

    public abstract class defaultLanguageName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      LocaleMaintenance.AlternativeFilter.defaultLanguageName>
    {
    }
  }

  public class AlternativeRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _IsAlternative;
    protected string _LanguageName;
    protected string _NativeName;

    [PXDBBool]
    [PXUIField(DisplayName = "Alternative")]
    [PXDefault(false)]
    public virtual bool? IsAlternative
    {
      get => this._IsAlternative;
      set => this._IsAlternative = value;
    }

    [PXDBString(2, IsKey = true, InputMask = "")]
    [PXUIField(DisplayName = "ISO Code", Enabled = false)]
    public virtual string LanguageName
    {
      get => this._LanguageName;
      set => this._LanguageName = value;
    }

    [PXDBString]
    [PXUIField(DisplayName = "Native Name", Enabled = false)]
    public virtual string NativeName
    {
      get => this._NativeName;
      set => this._NativeName = value;
    }

    public abstract class isAlternative : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      LocaleMaintenance.AlternativeRecord.isAlternative>
    {
    }

    public abstract class languageName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      LocaleMaintenance.AlternativeRecord.languageName>
    {
    }

    public abstract class nativeName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      LocaleMaintenance.AlternativeRecord.nativeName>
    {
    }
  }
}
