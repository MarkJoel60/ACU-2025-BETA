// Decompiled with JetBrains decompiler
// Type: PX.SM.AUConditionType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.SM;

public enum AUConditionType
{
  EqualsTo = 1,
  NotEqualsTo = 2,
  GreaterThan = 3,
  GreaterThanOrEqualsTo = 4,
  LessThan = 5,
  LessThanOrEqualsTo = 6,
  Like = 7,
  RightLike = 8,
  LeftLike = 9,
  Between = 11, // 0x0000000B
  IsNull = 12, // 0x0000000C
  IsNotNull = 13, // 0x0000000D
}
