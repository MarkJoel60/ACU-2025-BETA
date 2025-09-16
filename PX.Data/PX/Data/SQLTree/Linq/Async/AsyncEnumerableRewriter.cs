// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Linq.Async.AsyncEnumerableRewriter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.SQLTree.Linq.Async;

internal class AsyncEnumerableRewriter : ExpressionVisitor
{
  private static volatile ILookup<string, MethodInfo> _methods;

  protected override Expression VisitConstant(ConstantExpression node)
  {
    if (!(node.Value is AsyncEnumerableQuery asyncEnumerableQuery))
      return (Expression) node;
    if (asyncEnumerableQuery.Enumerable == null)
      return this.Visit(asyncEnumerableQuery.Expression);
    System.Type publicType = AsyncEnumerableRewriter.GetPublicType(asyncEnumerableQuery.Enumerable.GetType());
    return (Expression) Expression.Constant(asyncEnumerableQuery.Enumerable, publicType);
  }

  protected override Expression VisitMethodCall(MethodCallExpression node)
  {
    Expression instance = this.Visit(node.Object);
    ReadOnlyCollection<Expression> readOnlyCollection = this.Visit(node.Arguments);
    if (instance == node.Object && readOnlyCollection == node.Arguments)
      return (Expression) node;
    System.Type declaringType = node.Method.DeclaringType;
    System.Type[] genericArguments = node.Method.IsGenericMethod ? node.Method.GetGenericArguments() : (System.Type[]) null;
    if ((node.Method.IsStatic || declaringType != (System.Type) null && declaringType.IsAssignableFrom(instance.Type)) && AsyncEnumerableRewriter.ArgsMatch(node.Method, readOnlyCollection, genericArguments))
      return (Expression) Expression.Call(instance, node.Method, (IEnumerable<Expression>) readOnlyCollection);
    if (declaringType == typeof (AsyncQueryable))
    {
      MethodInfo enumerableMethod = AsyncEnumerableRewriter.FindEnumerableMethod(node.Method.Name, readOnlyCollection, genericArguments);
      ReadOnlyCollection<Expression> arguments = AsyncEnumerableRewriter.FixupQuotedArgs(enumerableMethod, readOnlyCollection);
      return (Expression) Expression.Call(instance, enumerableMethod, (IEnumerable<Expression>) arguments);
    }
    if (declaringType == (System.Type) null)
      throw new NotSupportedException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Could not rewrite method with name '{0}' without a DeclaringType.", (object) node.Method.Name));
    MethodInfo method = AsyncEnumerableRewriter.FindMethod(declaringType, node.Method.Name, readOnlyCollection, genericArguments, (BindingFlags) (8 | (node.Method.IsPublic ? 16 /*0x10*/ : 32 /*0x20*/)));
    ReadOnlyCollection<Expression> arguments1 = AsyncEnumerableRewriter.FixupQuotedArgs(method, readOnlyCollection);
    return (Expression) Expression.Call(instance, method, (IEnumerable<Expression>) arguments1);
  }

  protected override Expression VisitLambda<T>(Expression<T> node) => (Expression) node;

  protected override Expression VisitParameter(ParameterExpression node) => (Expression) node;

  private static System.Type GetPublicType(System.Type type)
  {
    if (!type.GetTypeInfo().IsNestedPrivate)
      return type;
    foreach (System.Type type1 in type.GetInterfaces())
    {
      if (type1.GetTypeInfo().IsGenericType)
      {
        System.Type genericTypeDefinition = type1.GetGenericTypeDefinition();
        if (genericTypeDefinition == typeof (IAsyncEnumerable<>) || genericTypeDefinition == typeof (IAsyncGrouping<,>))
          return type1;
      }
    }
    return type;
  }

  private static bool ArgsMatch(
    MethodInfo method,
    ReadOnlyCollection<Expression> args,
    System.Type[] typeArgs)
  {
    ParameterInfo[] parameters = method.GetParameters();
    if (parameters.Length != args.Count || !method.IsGenericMethod && typeArgs != null && typeArgs.Length != 0)
      return false;
    if (!method.IsGenericMethodDefinition && method.IsGenericMethod && method.ContainsGenericParameters)
      method = method.GetGenericMethodDefinition();
    if (method.IsGenericMethodDefinition)
    {
      if (typeArgs == null || typeArgs.Length == 0 || method.GetGenericArguments().Length != typeArgs.Length)
        return false;
      method = method.MakeGenericMethod(typeArgs);
      parameters = method.GetParameters();
    }
    for (int index = 0; index < args.Count; ++index)
    {
      System.Type type = parameters[index].ParameterType;
      if (type == (System.Type) null)
        return false;
      if (type.IsByRef)
        type = type.GetElementType();
      Expression operand = args[index];
      if (!type.IsAssignableFrom(operand.Type))
      {
        if (operand.NodeType == ExpressionType.Quote)
          operand = ((UnaryExpression) operand).Operand;
        if (!type.IsAssignableFrom(operand.Type) && !type.IsAssignableFrom(AsyncEnumerableRewriter.StripExpression(operand.Type)))
          return false;
      }
    }
    return true;
  }

  private static ReadOnlyCollection<Expression> FixupQuotedArgs(
    MethodInfo method,
    ReadOnlyCollection<Expression> argList)
  {
    ParameterInfo[] parameters = method.GetParameters();
    if (parameters.Length != 0)
    {
      List<Expression> list = (List<Expression>) null;
      for (int index1 = 0; index1 < parameters.Length; ++index1)
      {
        Expression expression1 = argList[index1];
        Expression expression2 = AsyncEnumerableRewriter.FixupQuotedExpression(parameters[index1].ParameterType, expression1);
        if (list == null && expression2 != argList[index1])
        {
          list = new List<Expression>(argList.Count);
          for (int index2 = 0; index2 < index1; ++index2)
            list.Add(argList[index2]);
        }
        list?.Add(expression2);
      }
      if (list != null)
        argList = new ReadOnlyCollection<Expression>((IList<Expression>) list);
    }
    return argList;
  }

  private static Expression FixupQuotedExpression(System.Type type, Expression expression)
  {
    Expression expression1;
    for (expression1 = expression; !type.IsAssignableFrom(expression1.Type); expression1 = ((UnaryExpression) expression1).Operand)
    {
      if (expression1.NodeType != ExpressionType.Quote)
      {
        if (!type.IsAssignableFrom(expression1.Type) && type.IsArray && expression1.NodeType == ExpressionType.NewArrayInit)
        {
          System.Type c = AsyncEnumerableRewriter.StripExpression(expression1.Type);
          if (type.IsAssignableFrom(c))
          {
            NewArrayExpression newArrayExpression = (NewArrayExpression) expression1;
            int count = newArrayExpression.Expressions.Count;
            System.Type elementType = type.GetElementType();
            List<Expression> initializers = new List<Expression>(count);
            for (int index = 0; index < count; ++index)
              initializers.Add(AsyncEnumerableRewriter.FixupQuotedExpression(elementType, newArrayExpression.Expressions[index]));
            expression = (Expression) Expression.NewArrayInit(elementType, (IEnumerable<Expression>) initializers);
          }
        }
        return expression;
      }
    }
    return expression1;
  }

  private static System.Type StripExpression(System.Type type)
  {
    System.Type type1 = type.IsArray ? type.GetElementType() : type;
    System.Type genericType = AsyncEnumerableRewriter.FindGenericType(typeof (Expression<>), type1);
    if (genericType != (System.Type) null)
      type1 = genericType.GetGenericArguments()[0];
    if (!type.IsArray)
      return type;
    int arrayRank = type.GetArrayRank();
    return arrayRank != 1 ? type1.MakeArrayType(arrayRank) : type1.MakeArrayType();
  }

  private static MethodInfo FindEnumerableMethod(
    string name,
    ReadOnlyCollection<Expression> args,
    params System.Type[] typeArgs)
  {
    if (AsyncEnumerableRewriter._methods == null)
      AsyncEnumerableRewriter._methods = ((IEnumerable<MethodInfo>) typeof (AsyncEnumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)).ToLookup<MethodInfo, string>((Func<MethodInfo, string>) (m => m.Name));
    MethodInfo methodInfo = AsyncEnumerableRewriter._methods[name].FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>) (m => AsyncEnumerableRewriter.ArgsMatch(m, args, typeArgs)));
    if (methodInfo == (MethodInfo) null)
      throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Could not find method with name '{0}' on type '{1}'.", (object) name, (object) typeof (Enumerable)));
    return typeArgs != null ? methodInfo.MakeGenericMethod(typeArgs) : methodInfo;
  }

  private static MethodInfo FindMethod(
    System.Type type,
    string name,
    ReadOnlyCollection<Expression> args,
    System.Type[] typeArgs,
    BindingFlags flags)
  {
    System.Type type1 = type.GetTypeInfo().GetCustomAttribute<LocalQueryMethodImplementationTypeAttribute>()?.TargetType;
    if ((object) type1 == null)
      type1 = type;
    MethodInfo[] array = ((IEnumerable<MethodInfo>) type1.GetMethods(flags)).Where<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == name)).ToArray<MethodInfo>();
    if (array.Length == 0)
      throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Could not find method with name '{0}' on type '{1}'.", (object) name, (object) type));
    MethodInfo methodInfo = ((IEnumerable<MethodInfo>) array).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>) (m => AsyncEnumerableRewriter.ArgsMatch(m, args, typeArgs)));
    if (methodInfo == (MethodInfo) null)
      throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Could not find a matching method with name '{0}' on type '{1}'.", (object) name, (object) type));
    return typeArgs != null ? methodInfo.MakeGenericMethod(typeArgs) : methodInfo;
  }

  private static System.Type FindGenericType(System.Type definition, System.Type type)
  {
    for (; type != (System.Type) null && type != typeof (object); type = type.GetTypeInfo().BaseType)
    {
      if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == definition)
        return type;
      if (definition.GetTypeInfo().IsInterface)
      {
        foreach (System.Type type1 in type.GetInterfaces())
        {
          System.Type genericType = AsyncEnumerableRewriter.FindGenericType(definition, type1);
          if (genericType != (System.Type) null)
            return genericType;
        }
      }
    }
    return (System.Type) null;
  }
}
