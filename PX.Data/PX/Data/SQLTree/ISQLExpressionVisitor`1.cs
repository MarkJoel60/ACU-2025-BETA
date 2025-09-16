// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ISQLExpressionVisitor`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

internal interface ISQLExpressionVisitor<T>
{
  T Visit(SQLExpression exp);

  T Visit(Asterisk exp);

  T Visit(Column exp);

  T Visit(SQLConst exp);

  T Visit(Literal exp);

  T Visit(NoteIdExpression exp);

  T Visit(SQLConvert exp);

  T Visit(SQLSmartConvert exp);

  T Visit(SQLDateAdd exp);

  T Visit(SQLDateByTimeZone exp);

  T Visit(SQLDateDiff exp);

  T Visit(SQLDatePart exp);

  T Visit(SQLFullTextSearch exp);

  T Visit(SQLRank exp);

  T Visit(SQLScalarFunction exp);

  T Visit(SQLSwitch exp);

  T Visit(SubQuery exp);

  T Visit(SQLGroupConcat exp);

  T Visit(SQLMultiOperation exp);

  T Visit(Md5Hash exp);

  T Visit(SQLAggConcat exp);
}
