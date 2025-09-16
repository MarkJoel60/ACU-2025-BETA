// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLocalizer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using CommonServiceLocator;
using PX.Common;
using PX.Common.Parser;
using PX.Data.Localization.Providers;
using PX.SM;
using PX.Translation;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading;

#nullable enable
namespace PX.Data;

/// <summary>
/// Provides mechanism for retrieving localized resources by key (from satellite resource assemblies).
/// </summary>
public class PXLocalizer
{
  protected const char NEW_LINE = '~';
  protected const char SPASE = ' ';
  private const 
  #nullable disable
  string LOCALIZATION_SLOT_KEY_BASE = "Localization_";
  private static IPXTranslationProvider _translationProvider;

  private static IPXTranslationProvider TranslationProvider
  {
    get
    {
      if (PXLocalizer._translationProvider != null)
        return PXLocalizer._translationProvider;
      if (PXHostingEnvironment.IsHosted)
        throw new InvalidOperationException("IPXTranslationProvider should have been initialized by now");
      return ServiceLocator.Current.GetInstance<IPXTranslationProvider>();
    }
    set
    {
      if (PXLocalizer._translationProvider != null)
        throw new InvalidOperationException("IPXTranslationProvider has been already initialized");
      PXLocalizer._translationProvider = value ?? throw new ArgumentNullException(nameof (value));
    }
  }

  private static PXDictionaryManager localizedResources
  {
    get
    {
      PXDictionaryManager localizedResources = PXContext.GetSlot<PXDictionaryManager>();
      if (localizedResources != null && localizedResources.Locale != Thread.CurrentThread.CurrentCulture.Name)
        localizedResources = PXContext.SetSlot<PXDictionaryManager>((PXDictionaryManager) null);
      if (localizedResources == null && !PXContext.GetSlot<bool>("_prefetching") && !WebConfig.DesignMode && !SuppressLocalizationScope.IsScoped)
      {
        PXContext.SetSlot<bool>("_prefetching", true);
        PXLocalizer.Definition definition = (PXLocalizer.Definition) null;
        try
        {
          definition = PXDatabase.GetSlot<PXLocalizer.Definition, IPXTranslationProvider>(PXLocalizer.LocalizationSlotKey, PXLocalizer.TranslationProvider, PXLocalizer.LocalizationSlotPars);
        }
        catch
        {
        }
        if (definition != null)
          localizedResources = PXContext.SetSlot<PXDictionaryManager>(definition.Manager);
        try
        {
          PXCommonLocalizer.Localize = new PXLocalizeCaller((object) null, __methodptr(Localize));
          PXCommonLocalizer.LocalizeFormat = new PXLocalizeFormatCaller((object) null, __methodptr(LocalizeFormat));
          PXCommonLocalizer.LocalizeFormatWithKey = new PXLocalizeFormatWithKeyCaller((object) null, __methodptr(LocalizeFormatWithKey));
          PXCommonLocalizer.LocalizeMessage = new PXLocalizeMessageCaller((object) null, __methodptr(LocalizeNoPrefix));
          ExpressionLocalizer.Localizer = new PXLocalizeConstantCaller((object) PXLocalizerRepository.ReportLocalizer, __methodptr(LocalizeFormulaConstants));
        }
        catch (DbException ex)
        {
        }
        catch (FileNotFoundException ex)
        {
        }
        PXContext.SetSlot<bool>("_prefetching", false);
      }
      return localizedResources;
    }
  }

  private static string LocalizationSlotKey
  {
    get => "Localization_" + Thread.CurrentThread.CurrentCulture.Name;
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

  /// <summary>
  /// Resets and refills the current database localization slot.
  /// </summary>
  public static void Reload()
  {
    PXLocalizer.ResetSlot();
    PXDictionaryManager localizedResources = PXLocalizer.localizedResources;
  }

  /// <summary>Resets the current database localization slot.</summary>
  public static void ResetSlot()
  {
    PXDatabase.ResetSlot<PXLocalizer.Definition>(PXLocalizer.LocalizationSlotKey, PXLocalizer.LocalizationSlotPars);
    PXContext.SetSlot<PXDictionaryManager>((PXDictionaryManager) null);
  }

  internal static IReadOnlyDictionary<string, Dictionary<string, string>> GetClientDictionary()
  {
    return PXLocalizer.localizedResources.GetClientDictionary();
  }

  internal static IReadOnlyDictionary<string, string> GetClientResourcesForScreen(string screenId)
  {
    return PXLocalizer.localizedResources.GetClientResourcesForScreen(screenId);
  }

  /// <summary>
  /// Translates the provided <paramref name="message" /> to the current
  /// locale using default translation value.
  /// The translation is taken from the currently loaded dictionary.
  /// </summary>
  /// <param name="message">An input string to translate.</param>
  /// <returns>The translated message.</returns>
  public static string Localize(string message) => PXLocalizer.Localize(message, (string) null);

  /// <summary>
  /// Translates the provided <paramref name="message" /> to the current
  /// locale using key-specific translation value.
  /// The translation is taken from the currently loaded dictionary.
  /// </summary>
  /// <param name="message">An input string to translate.</param>
  /// <param name="strMessageKey">The key of localization resource for key-specific translation.
  /// The key is used to avoid conflicts.
  /// If the key is null, default translation value will be used.
  /// The key is generated by one of the KeyGenerator classes.
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.AutomationKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXControlKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXListKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXUIFieldKeyGenerator" />
  /// </param>
  /// <returns>The translated message.</returns>
  public static string Localize(string message, string strMessageKey)
  {
    return PXLocalizer.Localize(message, strMessageKey, out bool _);
  }

  /// <summary>
  /// Translates the provided <paramref name="message" /> and all
  /// <paramref name="args" /> one by one to the current locale
  /// using default translation values and then composes the resulting
  /// string from the translations. The format items in the message
  /// are replaced with the translated arguments in the translated string.
  /// The translations are taken from the currently loaded dictionary.
  /// </summary>
  /// <param name="message">An input string to translate.</param>
  /// <param name="args">The arguments of the string.</param>
  /// <returns>The translated message.</returns>
  public static string LocalizeFormat(string message, params object[] args)
  {
    return PXLocalizer.LocalizeFormatWithKey(message, (string) null, args);
  }

  /// <summary>
  /// Translates the provided <paramref name="message" /> and all
  /// <paramref name="args" /> one by one to the current locale
  /// using key-specific translation value and then composes the resulting
  /// string from the translations. The format items in the message
  /// are replaced with the translated arguments in the translated string.
  /// The translations are taken from the currently loaded dictionary.
  /// </summary>
  /// <param name="message">An input string to translate.</param>
  /// <param name="strMessageKey">The key of localization resource for key-specific translation.
  /// The key is used to avoid conflicts.
  /// If the key is null, default translation value will be used.
  /// The key is generated by one of the KeyGenerator classes.
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.AutomationKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXControlKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXListKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXUIFieldKeyGenerator" />
  /// </param>
  /// <param name="args">The arguments of the string.</param>
  /// <returns>The translated message.</returns>
  public static string LocalizeFormatWithKey(
    string message,
    string strMessageKey,
    params object[] args)
  {
    message = PXLocalizer.Localize(message, strMessageKey);
    object[] array = ((IEnumerable<object>) args).Select<object, string>((Func<object, string>) (a => PXLocalizer.Localize(a.ToString(), strMessageKey))).Cast<object>().ToArray<object>();
    return string.Format(message, array);
  }

  /// <summary>
  /// Translates the provided <paramref name="message" /> to the current locale.
  /// The translation is taken from the currently loaded dictionary.
  /// </summary>
  /// <param name="message">An input string to translate.</param>
  /// <param name="strMessageKey">The key of localization resource for key-specific translation.
  /// The key is used to avoid conflicts.
  /// If the key is null, default translation value will be used.
  /// The key is generated by one of the KeyGenerator classes.
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.AutomationKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXControlKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXListKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXUIFieldKeyGenerator" />
  /// </param>
  /// <param name="isTranslated">Indicates whether the message has a
  /// translation to the current locale.</param>
  /// <returns>The translated message.</returns>
  public static string Localize(string message, string strMessageKey, out bool isTranslated)
  {
    return PXLocalizer.Localize(message, strMessageKey, true, out isTranslated, out bool _);
  }

  /// <summary>
  /// Translates the provided <paramref name="message" /> to the current locale.
  /// The translation is taken from the currently loaded dictionary.
  /// </summary>
  /// <param name="message">An input string to translate.</param>
  /// <param name="strMessageKey">The key of localization resource for key-specific translation.
  /// The key is used to avoid conflicts.
  /// If the key is null, default translation value will be used.
  /// The key is generated by one of the KeyGenerator classes.
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.AutomationKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXControlKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXListKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXUIFieldKeyGenerator" />
  /// </param>
  /// <param name="escapeString">
  /// </param>
  /// <param name="isTranslated">Indicates whether the message has a
  /// translation to the current locale.</param>
  /// <returns>The translated message.</returns>
  public static string Localize(
    string message,
    string strMessageKey,
    bool escapeString,
    out bool isTranslated)
  {
    return PXLocalizer.Localize(message, strMessageKey, escapeString, out isTranslated, out bool _);
  }

  /// <summary>
  /// Translates the provided <paramref name="message" /> to the current locale.
  /// The translation is taken from the currently loaded dictionary.
  /// </summary>
  /// <param name="message">An input string to translate.</param>
  /// <param name="strMessageKey">The key of localization resource for key-specific translation.
  /// The key is used to avoid conflicts.
  /// If the key is null, default translation value will be used.
  /// The key is generated by one of the KeyGenerator classes.
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.AutomationKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXControlKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXListKeyGenerator" />
  /// <see cref="T:PX.Data.LocalizationKeyGenerators.PXUIFieldKeyGenerator" />
  /// </param>
  /// <param name="escapeString">
  /// </param>
  /// <param name="isTranslated">Indicates whether the message has a
  /// translation to the current locale.</param>
  /// <param name="isNotLocalizable">Indicates whether the message was marked as not localizable
  /// on the Translation Dictionaries (SM200540) form.
  /// For details, see <see cref="P:PX.Translation.LocalizationRecord.IsNotLocalized" />.</param>
  /// <returns>The translated message.</returns>
  /// <remarks>The message is trimmed before translation.</remarks>
  /// <example>
  /// <code>
  /// string localized = PXLocalizer.Localize(Messages.ExampleMessage, typeof(Messages).FullName, isTranslated, isNotLocalizable);
  /// </code>
  /// </example>
  public static string Localize(
    string message,
    string strMessageKey,
    bool escapeString,
    out bool isTranslated,
    out bool isNotLocalizable)
  {
    string str1 = message;
    isTranslated = false;
    isNotLocalizable = false;
    if (message == null)
      return str1;
    PXDictionaryManager localizedResources = PXLocalizer.localizedResources;
    string str2;
    if (localizedResources != null)
    {
      if (!PXInvariantCultureScope.IsSet())
      {
        string message1 = message.Trim();
        if (escapeString)
          message1 = PXLocalizer.UnescapeString(message1);
        string str3 = localizedResources.Translate(message1, strMessageKey, Thread.CurrentThread.CurrentCulture.Name, out isNotLocalizable);
        if (!string.IsNullOrEmpty(str3))
        {
          isTranslated = true;
          str2 = isNotLocalizable ? PXLocalizer.EscapeString(message) : str3;
        }
        else
        {
          isTranslated = isNotLocalizable;
          str2 = PXLocalizer.EscapeString(message);
        }
      }
      else
        str2 = PXLocalizer.EscapeString(message);
    }
    else
      str2 = PXLocalizer.EscapeString(message);
    return str2;
  }

  [Obsolete("This method is obsolete and will be removed in Acumatica ERP 2018R2.")]
  public static string LocalizeCompound(string message, string compoundKey, string strMessageKey)
  {
    string message1 = $"{compoundKey} -> {message}";
    string str = PXLocalizer.Localize(message1, strMessageKey);
    return str == message1 ? message : str;
  }

  /// <summary>
  /// Replaces newline escape symbols in the provided <paramref name="message" /> by environment default newline symbol.
  /// </summary>
  /// <param name="message">An input string.</param>
  public static string EscapeString(string message)
  {
    return message != null && message.IndexOf('~') >= 0 ? message.Replace(new string('~', 1), Environment.NewLine) : message;
  }

  /// <summary>
  /// Replaces newline symbols in the provided <paramref name="message" /> by special escape symbol.
  /// </summary>
  /// <param name="message">An input string.</param>
  public static string UnescapeString(string message)
  {
    return message != null && message.Contains(Environment.NewLine) ? message.Replace(Environment.NewLine, '~'.ToString()) : message;
  }

  private class ServiceRegistration : Module
  {
    protected virtual void Load(ContainerBuilder builder)
    {
      builder.RegisterBuildCallback((System.Action<ILifetimeScope>) (lifetimeScope => PXLocalizer.TranslationProvider = ResolutionExtensions.Resolve<IPXTranslationProvider>((IComponentContext) lifetimeScope)));
    }
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
