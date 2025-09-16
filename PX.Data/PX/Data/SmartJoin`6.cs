// Decompiled with JetBrains decompiler
// Type: PX.Data.SmartJoin`6
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public class SmartJoin<Separator, Str1, Str2, Str3, Str4, Str5> : 
  SmartJoin<Separator, SmartJoin<Separator, Str1, Str2, Str3, Str4>, Str5>
  where Separator : IBqlOperand
  where Str1 : IBqlOperand
  where Str2 : IBqlOperand
  where Str3 : IBqlOperand
  where Str4 : IBqlOperand
  where Str5 : IBqlOperand
{
}
