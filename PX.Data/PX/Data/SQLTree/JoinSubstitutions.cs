// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.JoinSubstitutions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.SQLTree;

internal static class JoinSubstitutions
{
  private static MethodInfo _leftJoinInfo = JoinSubstitutions.FindMethodBase("__LeftJoinSubstitution__");
  private static MethodInfo _rightJoinInfo = JoinSubstitutions.FindMethodBase("__RightJoinSubstitution__");
  private static MethodInfo _fullJoinInfo = JoinSubstitutions.FindMethodBase("__FullJoinSubstitution__");
  private static MethodInfo _crossJoinInfo = JoinSubstitutions.FindMethodBase("__CrossJoinSubstitution__");
  public const string LeftJoinName = "__LeftJoinSubstitution__";
  public const string RightJoinName = "__RightJoinSubstitution__";
  public const string FullJoinName = "__FullJoinSubstitution__";
  public const string CrossJoinName = "__CrossJoinSubstitution__";

  private static MethodInfo FindMethodBase(string methodName)
  {
    return ((IEnumerable<MethodInfo>) typeof (JoinSubstitutions).GetMethods(BindingFlags.Static | BindingFlags.Public)).Where<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == methodName)).FirstOrDefault<MethodInfo>();
  }

  public static MethodInfo GetLeftJoinMethod(System.Type substitutionType)
  {
    return JoinSubstitutions._leftJoinInfo.MakeGenericMethod(substitutionType);
  }

  public static MethodInfo GetRightJoinMethod(System.Type substitutionType)
  {
    return JoinSubstitutions._rightJoinInfo.MakeGenericMethod(substitutionType);
  }

  public static MethodInfo GetFullJoinMethod(System.Type substitutionType)
  {
    return JoinSubstitutions._fullJoinInfo.MakeGenericMethod(substitutionType);
  }

  public static MethodInfo GetCrossJoinMethod(System.Type substitutionType)
  {
    return JoinSubstitutions._crossJoinInfo.MakeGenericMethod(substitutionType);
  }

  public static T __LeftJoinSubstitution__<T>(T t) => t;

  public static T __RightJoinSubstitution__<T>(T t) => t;

  public static T __FullJoinSubstitution__<T>(T t) => t;

  public static T __CrossJoinSubstitution__<T>(T t) => t;

  public static bool IsSubstitutionMethod(string methodName)
  {
    return methodName == "__LeftJoinSubstitution__" || methodName == "__RightJoinSubstitution__" || methodName == "__FullJoinSubstitution__" || methodName == "__CrossJoinSubstitution__";
  }
}
