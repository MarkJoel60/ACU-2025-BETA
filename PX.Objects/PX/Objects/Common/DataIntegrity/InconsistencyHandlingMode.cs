// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DataIntegrity.InconsistencyHandlingMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.DataIntegrity;

public class InconsistencyHandlingMode : ILabelProvider
{
  /// <summary>The release integrity validator will do nothing.</summary>
  public const string None = "N";
  /// <summary>
  /// The release integrity validator will log errors into the <see cref="T:PX.Objects.Common.DataIntegrity.DataIntegrityLog" /> table.
  /// </summary>
  public const string Log = "L";
  /// <summary>
  /// The release integrity validator will prevent errors and not log them into the database.
  /// </summary>
  public const string Prevent = "P";

  public IEnumerable<ValueLabelPair> ValueLabelPairs
  {
    get
    {
      return (IEnumerable<ValueLabelPair>) new ValueLabelList()
      {
        {
          "L",
          "Log Issues"
        },
        {
          "P",
          "Prevent Release"
        }
      };
    }
  }
}
