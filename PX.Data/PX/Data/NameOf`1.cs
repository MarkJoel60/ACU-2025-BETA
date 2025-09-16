// Decompiled with JetBrains decompiler
// Type: PX.Data.NameOf`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

/// <summary>
/// The BQL-constant that represents the name of a given type.
/// The BQL equivalent of the <see cref="P:System.Reflection.MemberInfo.Name" /> property.
/// </summary>
public sealed class NameOf<T> : BqlType<IBqlString, string>.Constant<
#nullable disable
NameOf<T>>
{
  public NameOf()
    : base(typeof (T).Name)
  {
  }
}
