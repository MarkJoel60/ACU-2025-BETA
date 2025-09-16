// Decompiled with JetBrains decompiler
// Type: PX.Data.ERPTransactionsAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ERPTransactionsAttribute : Attribute
{
  private bool _Suppress;

  public bool Suppress
  {
    get => this._Suppress;
    set => this._Suppress = value;
  }
}
