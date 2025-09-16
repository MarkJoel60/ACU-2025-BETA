// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.boolTrue
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

/// <summary>
/// This constant type is deprecated and is only preserved for
/// compatibility purposes. Please use <see cref="T:PX.Data.True" /> instead.
/// </summary>
public class boolTrue : BqlType<IBqlBool, bool>.Constant<
#nullable disable
boolTrue>
{
  public boolTrue()
    : base(true)
  {
  }
}
