// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.RightsValues
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Access;

public static class RightsValues
{
  public const int Inherited = -1;
  public const int Revoked = 0;
  public const int ViewOnly = 1;
  public const int Edit = 2;
  public const int Insert = 3;
  public const int Delete = 4;
  public const int Granted = 4;
  public const int Publish = 4;
  public const int WikiDelete = 5;
  public const int MultipleRights = 6;
}
