// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.accountType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL;

public sealed class accountType : BqlType<IBqlString, string>.Constant<
#nullable disable
accountType>
{
  public accountType()
    : base(typeof (Account).FullName)
  {
  }
}
