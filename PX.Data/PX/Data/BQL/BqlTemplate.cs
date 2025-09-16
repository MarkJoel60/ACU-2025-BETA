// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlTemplate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BQL;

public class BqlTemplate : IBqlCommandTemplate, IBqlTemplate
{
  private readonly System.Type[] _originStatements;
  private readonly Dictionary<System.Type, System.Type> _replaceMap = new Dictionary<System.Type, System.Type>();

  public static IBqlCommandTemplate FromCommand(BqlCommand command)
  {
    return (IBqlCommandTemplate) BqlTemplate.FromTypeInt(command is BqlCommandDecorator commandDecorator ? commandDecorator.GetOriginalType() : command.GetType());
  }

  public static IBqlTemplate FromType(System.Type commandType)
  {
    return (IBqlTemplate) BqlTemplate.FromTypeInt(commandType);
  }

  private static BqlTemplate FromTypeInt(System.Type commandType)
  {
    return new BqlTemplate(BqlCommand.Decompose(typeof (BqlCommandDecorator).IsAssignableFrom(commandType) ? BqlCommandDecorator.Unwrap(commandType) : (typeof (IBqlUnary).IsAssignableFrom(commandType) || typeof (IBqlBinary).IsAssignableFrom(commandType) || typeof (IBqlWhere).IsAssignableFrom(commandType) || typeof (IBqlOn).IsAssignableFrom(commandType) ? ConditionConvertor.TryConvert(commandType, true) : commandType)));
  }

  private BqlTemplate(System.Type[] statements) => this._originStatements = statements;

  private BqlTemplate Replace(System.Type placeholder, System.Type actual)
  {
    if (actual == (System.Type) null)
      throw new ArgumentNullException(nameof (actual));
    this._replaceMap.Add(placeholder, ConditionConvertor.TryConvert(actual));
    return this;
  }

  private static System.Type[] PerformReplace(System.Type[] origin, Dictionary<System.Type, System.Type> replaceMap)
  {
    System.Type[] typeArray = new System.Type[origin.Length];
    for (int index = 0; index < origin.Length; ++index)
    {
      if (typeof (IBqlPlaceholder).IsAssignableFrom(origin[index]))
      {
        System.Type key = typeof (IShouldBeReplacedWith<>).GenericIsAssignableFrom(origin[index]) ? ((IEnumerable<System.Type>) ((IEnumerable<System.Type>) origin[index].GetInterfaces()).First<System.Type>((Func<System.Type, bool>) (t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof (IShouldBeReplacedWith<>))).GetGenericArguments()).First<System.Type>() : origin[index];
        System.Type type;
        if (!replaceMap.TryGetValue(key, out type))
          throw new InvalidOperationException($"Placeholder {origin[index].ToCodeString()} was not replaced with an actual type.");
        typeArray[index] = type;
      }
      else
        typeArray[index] = origin[index];
    }
    return typeArray;
  }

  private System.Type ToType()
  {
    return BqlCommand.Compose(BqlTemplate.PerformReplace(this._originStatements, this._replaceMap));
  }

  IBqlTemplate IBqlTemplate.Replace<TPlaceholder, TActual>()
  {
    return (IBqlTemplate) this.Replace(typeof (TPlaceholder), typeof (TActual));
  }

  IBqlTemplate IBqlTemplate.Replace<TPlaceholder>(System.Type actual)
  {
    return (IBqlTemplate) this.Replace(typeof (TPlaceholder), actual);
  }

  System.Type IBqlTemplate.ToType() => this.ToType();

  IBqlCommandTemplate IBqlCommandTemplate.Replace<TPlaceholder, TActual>()
  {
    return (IBqlCommandTemplate) this.Replace(typeof (TPlaceholder), typeof (TActual));
  }

  IBqlCommandTemplate IBqlCommandTemplate.Replace<TPlaceholder>(System.Type actual)
  {
    return (IBqlCommandTemplate) this.Replace(typeof (TPlaceholder), actual);
  }

  BqlCommand IBqlCommandTemplate.ToCommand() => BqlCommand.CreateInstance(this.ToType());

  public static class OfBinary<TBqlBinary> where TBqlBinary : IBqlBinary
  {
    public static IBqlTemplate Replace<TPlaceholder>(System.Type actual) where TPlaceholder : IBqlPlaceholder
    {
      return BqlTemplate.FromType(typeof (TBqlBinary)).Replace<TPlaceholder>(actual);
    }
  }

  public static class OfCondition<TBqlWhere> where TBqlWhere : IBqlWhere
  {
    public static IBqlTemplate Replace<TPlaceholder>(System.Type actual) where TPlaceholder : IBqlPlaceholder
    {
      return BqlTemplate.FromType(typeof (TBqlWhere)).Replace<TPlaceholder>(actual);
    }
  }

  public static class OfJoin<TBqlJoin> where TBqlJoin : IBqlJoin
  {
    public static IBqlTemplate Replace<TPlaceholder>(System.Type actual) where TPlaceholder : IBqlPlaceholder
    {
      return BqlTemplate.FromType(typeof (TBqlJoin)).Replace<TPlaceholder>(actual);
    }
  }

  public static class OfCommand<TCommand> where TCommand : BqlCommand
  {
    public static IBqlCommandTemplate Replace<TPlaceholder>(System.Type actual) where TPlaceholder : IBqlPlaceholder
    {
      return ((IBqlCommandTemplate) BqlTemplate.FromTypeInt(typeof (TCommand))).Replace<TPlaceholder>(actual);
    }
  }
}
