// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXRipper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Access.ActiveDirectory;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

#nullable disable
namespace PX.Translation;

/// <exclude />
internal class PXRipper
{
  private static readonly ReadOnlyCollection<string> ExcludedDirectories = new ReadOnlyCollection<string>((IEnumerable<string>) new string[2]
  {
    "CstDesigner",
    "roslyn"
  });
  private const string PAGES_PATH = "Pages";
  protected DirectoryInfo directory;

  protected PXRipper()
  {
  }

  private bool IsDirectoryExcluded(string directory)
  {
    if (string.IsNullOrWhiteSpace(directory))
      return true;
    foreach (string excludedDirectory in PXRipper.ExcludedDirectories)
    {
      if (CultureInfo.InvariantCulture.CompareInfo.IndexOf(directory, excludedDirectory, CompareOptions.OrdinalIgnoreCase) >= 0)
        return true;
    }
    return false;
  }

  public void CollectResources(
    DirectoryInfo dir,
    string fileSearchPattern,
    List<string> processed,
    ResourceCollection result)
  {
    if (dir == null || string.IsNullOrEmpty(fileSearchPattern) || processed == null || result == null)
      return;
    this.directory = dir;
    this.OnRipStart(processed, result);
    foreach (System.IO.FileInfo file in dir.GetFiles(fileSearchPattern, SearchOption.AllDirectories))
    {
      if (!this.IsDirectoryExcluded(file.DirectoryName))
        this.Rip(file, processed, result);
    }
  }

  public void CollectTranslationSetResources(
    DirectoryInfo dir,
    string searchPattern,
    List<string> processed,
    ResourceCollection result,
    List<LocalizationTranslationSetItem> translationSetItems,
    TranslationSetMaint graph)
  {
    if (dir == null || string.IsNullOrEmpty(searchPattern) || processed == null || result == null || translationSetItems == null)
      return;
    this.directory = dir;
    this.OnRipStart(processed, result);
    LocalizationTranslationSetItem translationSetItem1 = translationSetItems.FirstOrDefault<LocalizationTranslationSetItem>((Func<LocalizationTranslationSetItem, bool>) (i => string.CompareOrdinal(i.ScreenID, "00000000") == 0));
    bool flag = translationSetItem1 != null;
    foreach (System.IO.FileInfo file1 in dir.GetFiles(searchPattern, SearchOption.AllDirectories))
    {
      System.IO.FileInfo file = file1;
      if (!this.IsDirectoryExcluded(file.DirectoryName))
      {
        LocalizationTranslationSetItem[] array = translationSetItems.Where<LocalizationTranslationSetItem>((Func<LocalizationTranslationSetItem, bool>) (i => file.Name.Equals(i.NameForStringCollection, StringComparison.OrdinalIgnoreCase) && !"00000000".Equals(i.ScreenID, StringComparison.OrdinalIgnoreCase))).ToArray<LocalizationTranslationSetItem>();
        if (array.Length != 0)
        {
          HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
          int count = processed.Count;
          foreach (LocalizationTranslationSetItem translationSetItem2 in array)
          {
            if (!stringSet.Contains(translationSetItem2.ScreenID ?? ""))
            {
              if (count < processed.Count)
                processed.RemoveRange(count, processed.Count - count);
              this.Rip(file, processed, result, translationSetItem2, graph, false);
              stringSet.Add(translationSetItem2.ScreenID ?? "");
            }
          }
        }
        else if (flag && !file.FullName.Substring(this.directory.FullName.Length).StartsWith("Pages"))
          this.Rip(file, processed, result, translationSetItem1, graph);
      }
    }
  }

  protected virtual void OnRipStart(List<string> processed, ResourceCollection result)
  {
  }

  protected virtual void Rip(
    System.IO.FileInfo file,
    List<string> processed,
    ResourceCollection result,
    LocalizationTranslationSetItem item = null,
    TranslationSetMaint graph = null,
    bool standalone = true)
  {
  }
}
