// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Tags.TagAccessLevel
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Tags;

public static class TagAccessLevel
{
  public const short Revoked = 0;
  public const short View = 1;
  public const short CreateVersion = 2;
  public const short Upload = 3;
  public const short Delete = 4;

  public static bool HasAccess(short accessLevel, short requiredAccessLevel)
  {
    return (int) accessLevel >= (int) requiredAccessLevel;
  }

  public static short Combine(short accessLevel1, short accessLevel2)
  {
    return System.Math.Max(accessLevel1, accessLevel2);
  }
}
