// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.RefSafetyRulesAttribute
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using Microsoft.CodeAnalysis;

#nullable disable
namespace System.Runtime.CompilerServices;

[CompilerGenerated]
[AttributeUsage(AttributeTargets.Module, AllowMultiple = false, Inherited = false)]
[Embedded]
internal sealed class RefSafetyRulesAttribute : Attribute
{
  public readonly int Version;

  public RefSafetyRulesAttribute(int _param1) => this.Version = _param1;
}
