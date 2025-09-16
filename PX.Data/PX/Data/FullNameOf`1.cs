// Decompiled with JetBrains decompiler
// Type: PX.Data.FullNameOf`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

/// <summary>
/// The BQL-constant that represents the fully qualified name of a given type, including its namespace but not its assembly.
/// The BQL equivalent of the <see cref="P:System.Type.FullName" /> property.
/// </summary>
public sealed class FullNameOf<T> : BqlType<IBqlString, string>.Constant<
#nullable disable
FullNameOf<T>>
{
  public FullNameOf()
    : base(typeof (T).FullName)
  {
  }
}
