// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.SubCDUtils
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.GL;

public class SubCDUtils
{
  public const string CS_SUB_CD_FILL_CHAR = " ";
  public const string CS_SUB_CD_WILDCARD = "?";

  public static bool IsSubCDEmpty(string aSub)
  {
    return aSub == null || aSub == "" || new Regex("^[_\\?]*$").IsMatch(aSub);
  }

  public static string CreateSubCDWildcard(string aSub, string dimensionID)
  {
    ISqlDialect sqlDialect = PXDatabase.Provider.SqlDialect;
    if (SubCDUtils.IsSubCDEmpty(aSub))
      return sqlDialect.WildcardAnything;
    if (aSub[aSub.Length - 1] != '?' && aSub[aSub.Length - 1] != ' ' && aSub.Length == PXDimensionAttribute.GetLength(dimensionID))
      aSub += "?";
    return Regex.Replace(Regex.Replace(aSub, "[ \\?]+$", sqlDialect.WildcardAnything), "[ \\?]", sqlDialect.WildcardAnySingle);
  }
}
