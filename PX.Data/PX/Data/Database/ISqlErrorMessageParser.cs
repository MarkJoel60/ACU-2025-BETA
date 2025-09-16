// Decompiled with JetBrains decompiler
// Type: PX.Data.Database.ISqlErrorMessageParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Database;

/// <summary>Deconstructing a sql error message by template</summary>
public interface ISqlErrorMessageParser
{
  /// <summary>
  /// Parsing an error message and returns a list of patterns from template
  /// </summary>
  /// <param name="errorMessage">Sql exception message</param>
  /// <param name="template">Message template</param>
  /// <param name="messagePatterns">Message patterns</param>
  /// <returns>true when parsed successful, otherwise - false</returns>
  bool TryParse(string errorMessage, string template, out List<string> messagePatterns);

  /// <summary>Returns a message template for exception</summary>
  /// <param name="errorCode">Error message DB code</param>
  /// <param name="languageId">Language id (1033 - US-English)</param>
  /// <param name="template">Message template</param>
  /// <returns>true when the template was found, otherwise - false</returns>
  bool TryGetMessageTemplate(int errorCode, int languageId, out string template);

  /// <summary>Returns an index of pattern from template</summary>
  /// <param name="template">Message template</param>
  /// <param name="patternNumber">Pattern</param>
  /// <returns>index of pattern</returns>
  /// <remarks>
  /// Typically templates for most languages have a natural, obvious order of patterns in message template,
  /// but this order may be different for some languages. As an example:
  ///  - de-de 1031 German - (natual order 1,2,3,4,5,6,7,8):
  ///  - hu 1038 Hungarian - (order 1,3,2,4,5,6,7,8)
  /// </remarks>
  int TryGetPatternIndex(string template, int patternNumber);
}
