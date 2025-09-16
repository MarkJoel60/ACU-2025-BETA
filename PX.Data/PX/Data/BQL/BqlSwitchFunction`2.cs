// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlSwitchFunction`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.BQL;

public abstract class BqlSwitchFunction<TSwitch, TBqlType> : BqlFunction<TSwitch, TBqlType>, ISwitch
  where TSwitch : IBqlOperand, IBqlCreator, ISwitch, new()
  where TBqlType : class, IBqlDataType
{
  System.Type ISwitch.OuterField
  {
    get => this._function.OuterField;
    set => this._function.OuterField = value;
  }
}
