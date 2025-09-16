// Decompiled with JetBrains decompiler
// Type: PX.Data.Database.MsSql.MsSqlErrorMessageParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Database.MsSql;

/// <inheritdoc />
public class MsSqlErrorMessageParser(
  Dictionary<int, Dictionary<int, string>> messageTemplates) : SqlErrorMessageParserBase(messageTemplates)
{
  private static Dictionary<int, Dictionary<int, string>> _messageTemplates = new Dictionary<int, Dictionary<int, string>>()
  {
    {
      547,
      new Dictionary<int, string>()
      {
        {
          1033,
          "The %1! statement conflicted with the %2! constraint \"%3!\". The conflict occurred in database \"%4!\", table \"%5!\"%6!%7!%8!."
        }
      }
    }
  };

  public MsSqlErrorMessageParser()
    : this(MsSqlErrorMessageParser._messageTemplates)
  {
  }
}
