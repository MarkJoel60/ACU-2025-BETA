// Decompiled with JetBrains decompiler
// Type: PX.Data.ConstantMessage`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

public abstract class ConstantMessage<TSelf> : BqlType<IBqlString, string>.Constant<
#nullable disable
TSelf> where TSelf : ConstantMessage<TSelf>, new()
{
  public ConstantMessage(string msg)
    : base(msg)
  {
  }

  public override string Value => PXMessages.LocalizeNoPrefix(base.Value);
}
