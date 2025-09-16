// Decompiled with JetBrains decompiler
// Type: PX.CS.SizeTypeListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.CS;

public class SizeTypeListAttribute : PXIntListAttribute
{
  public SizeTypeListAttribute()
    : base("1;Pixel,2;Point,3;Pica,4;Inch,5;Mm.,6;Cm.")
  {
  }
}
