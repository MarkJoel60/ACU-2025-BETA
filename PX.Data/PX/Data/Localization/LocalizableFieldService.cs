// Decompiled with JetBrains decompiler
// Type: PX.Data.Localization.LocalizableFieldService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PX.Common;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Threading;

#nullable disable
namespace PX.Data.Localization;

internal class LocalizableFieldService : ILocalizableFieldService
{
  private const string NameSeparator = ".";
  private const string BinFolder = "Bin";

  internal Lazy<ImmutableHashSet<string>> EnabledFields { get; }

  public LocalizableFieldService(
    IOptions<LocalizableFieldOptions> options,
    IHostEnvironment hostEnvironment)
  {
    this.EnabledFields = new Lazy<ImmutableHashSet<string>>((Func<ImmutableHashSet<string>>) (() => LocalizableFieldService.Initialize(options.Value.FileName, hostEnvironment.ContentRootPath)), LazyThreadSafetyMode.PublicationOnly);
  }

  private static ImmutableHashSet<string> Initialize(string fileName, string contentRootPath)
  {
    if (string.IsNullOrEmpty(fileName))
      return ImmutableHashSet.Create<string>();
    string path = Path.Combine(contentRootPath, "Bin", fileName);
    return !File.Exists(path) ? ImmutableHashSet.Create<string>() : ImmutableHashSet.ToImmutableHashSet<string>((IEnumerable<string>) File.ReadAllLines(path), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  }

  public bool IsFieldEnabled(string tableName, string fieldName)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(tableName, nameof (tableName), (string) null);
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(fieldName, nameof (fieldName), (string) null);
    if (!PXDBLocalizableStringAttribute.IsEnabled)
      return false;
    return this.EnabledFields.Value.Count == 0 || this.EnabledFields.Value.Contains($"{tableName}.{fieldName}");
  }
}
