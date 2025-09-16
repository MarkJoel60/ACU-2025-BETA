// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.ParameterStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using PX.Common.Collection;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

[ImmutableObject(true)]
public class ParameterStatement : OperandStatement
{
  private readonly Type _parameterTypeSource;
  private readonly ParameterType _parameterType;
  private readonly bool _withDefaulting;
  private static readonly ReadOnlyBiDictionary<ParameterStatement.ParameterClassification, Type> Parameters = ReadOnlyBiDictionaryExt.ToBiDictionary<ParameterStatement.ParameterClassification, Type>((IReadOnlyDictionary<ParameterStatement.ParameterClassification, Type>) new Dictionary<ParameterStatement.ParameterClassification, Type>()
  {
    [new ParameterStatement.ParameterClassification(ParameterType.Current, false)] = typeof (Current2<>),
    [new ParameterStatement.ParameterClassification(ParameterType.Current, true)] = typeof (Current<>),
    [new ParameterStatement.ParameterClassification(ParameterType.Optional, false)] = typeof (Optional2<>),
    [new ParameterStatement.ParameterClassification(ParameterType.Optional, true)] = typeof (Optional<>),
    [new ParameterStatement.ParameterClassification(ParameterType.Required, false)] = typeof (Required<>)
  });

  internal ParameterStatement(
    Type parameterTypeSource,
    ParameterType parameterType,
    bool withDefaulting = false)
  {
    this._parameterTypeSource = parameterTypeSource;
    this._parameterType = parameterType;
    this._withDefaulting = withDefaulting;
  }

  public override Type Eval()
  {
    return ParameterStatement.Parameters[new ParameterStatement.ParameterClassification(this._parameterType, this._withDefaulting)].MakeGenericType(this._parameterTypeSource);
  }

  internal static ParameterStatement FromRawStatement(Type parameter)
  {
    Type genericTypeDefinition = parameter.GetGenericTypeDefinition();
    Type[] genericArguments = parameter.GetGenericArguments();
    return !ParameterStatement.Parameters.ContainsValue(genericTypeDefinition) ? (ParameterStatement) null : new ParameterStatement(genericArguments[0], ParameterStatement.Parameters[genericTypeDefinition].Type, ParameterStatement.Parameters[genericTypeDefinition].WithDefaulting);
  }

  private class ParameterClassification(ParameterType parameterType, bool withDefaulting) : 
    Tuple<ParameterType, bool>(parameterType, withDefaulting)
  {
    public ParameterType Type => this.Item1;

    public bool WithDefaulting => this.Item2;
  }
}
