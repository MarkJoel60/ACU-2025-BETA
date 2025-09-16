// Decompiled with JetBrains decompiler
// Type: PX.Common.RequiredTypesAttribute
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;

#nullable enable
namespace PX.Common;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class RequiredTypesAttribute : Attribute
{
  private readonly Type[] \u0002;

  public RequiredTypesAttribute(params Type[] types)
  {
    this.\u0002 = ExceptionExtensions.CheckIfNull<Type[]>(types, nameof (types), (string) null);
  }

  public Type[] Types => this.\u0002;
}
