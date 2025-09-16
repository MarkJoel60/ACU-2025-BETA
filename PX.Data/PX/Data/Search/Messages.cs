// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.Messages
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.Search;

[PXLocalizable]
public static class Messages
{
  public const string scrSearch = "Search";
  public const string Help = "Help";
  public const string Files = "Files";
  public const string AllEntities = "All Entities";
  public const string SearchInHelp = "Search for \"{0}\" in Help";
  public const string SearchIn = "Search for \"{0}\" in {1}";
  public const string SearchInAllEntities = "Search for \"{0}\" in All Entities";
  public const string SearchInFiles = "Search for \"{0}\" in Files";
  public const string SpecifySearchRequest = "Enter your search request.";
  public const string NothingFound = "No results found for";
  public const string SearchTips = "Search tips";
  public const string CheckSpelling = "Make sure that all words are spelled correctly.";
  public const string SimplifyQuery = "Consider simplifying complex or wordy queries.";
  public const string TryOtherWords = "Try other words.";
  public const string TryFullTextSearch = "You may want to try full-text search; click the Full-text search check box and repeat your request. ";
  public const string SearchIndexIsEmpty = "Full-Text Entity Index must be built before using the search. Please rebuild the index and try again.";
  public const string IndexDisabled = "Warning! The Full-Text Search feature is not enabled in the database. Search by a phrase or a list of keywords might not work. You can contact the database administrator to enable the feature.";

  public static string GetLocal(string message)
  {
    return PXLocalizer.Localize(message, typeof (Messages).FullName);
  }
}
