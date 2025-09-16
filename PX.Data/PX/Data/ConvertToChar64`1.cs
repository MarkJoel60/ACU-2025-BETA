// Decompiled with JetBrains decompiler
// Type: PX.Data.ConvertToChar64`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

internal class ConvertToChar64<Source> : ConvertTo<Source, char> where Source : IBqlOperand
{
  protected override int Length { get; } = 64 /*0x40*/;
}
