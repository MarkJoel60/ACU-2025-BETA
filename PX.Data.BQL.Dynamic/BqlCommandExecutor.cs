// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.BqlCommandExecutor
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

#nullable disable
namespace PX.Data.BQL.Dynamic;

internal class BqlCommandExecutor
{
  public BqlCommand Command { get; }

  public ParameterMap ParameterMap { get; }

  public BqlCommandExecutor(BqlCommand command, ParameterMap parameterMap)
  {
    this.Command = command;
    this.ParameterMap = parameterMap;
  }
}
