// Decompiled with JetBrains decompiler
// Type: PX.Data.ISqlDialect
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using PX.DbServices.Model.Native;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data;

public interface ISqlDialect
{
  Version DbmsVersion { get; }

  Connection GetConnection();

  SqlDialect DialectType { get; }

  string getCompanyMask(int companyId);

  string identCurrent(string tableName);

  string caseWhenThenElse(string condition, string afterThen, string afterElse);

  string concat(IEnumerable<string> parts);

  string concat(params string[] parts);

  string strlen(string inputString);

  string substr(string inputString, string startPos, string len);

  string asc(string character);

  string @char(string number);

  string bitOr(string op1, string op2);

  string bitAnd(string op1, string op2);

  string bitNot(string op1);

  string byteOfMaskAnd(string sourceStr, int index, string mask);

  string binaryMaskTest(string companyMask, int company, int rights);

  string binaryMaskSub(string companyMask, int company, int rights);

  bool isRealTable(string tableName);

  string makeDbIdentifier(string name);

  string quoteDbIdentifier(string tablename);

  string quoteTableAndColumn(string tableName, string columnName);

  bool isColumnNameQuoted(string name);

  string unquoteColumn(string name);

  string limitRowsBeingSelected(string selectClause, long topN);

  string limitRowsBeingSelected(string selectClause, long skip, long topN);

  void scriptFunctionSub(
    StringBuilder text,
    TypeCode tOp1,
    TypeCode tOp2,
    string operand1,
    string operand2);

  void scriptFunctionAdd(
    StringBuilder text,
    TypeCode tOp1,
    TypeCode tOp2,
    string operand1,
    string operand2);

  void scriptFunctionBitwiseAnd(
    StringBuilder text,
    TypeCode tOp1,
    TypeCode tOp2,
    string operand1,
    string operand2);

  void scriptFunctionDateDiff<UOM>(
    StringBuilder text,
    UOM timeUnitsConst,
    TypeCode typeCode1,
    TypeCode typeCode2,
    System.Action<StringBuilder> action1,
    System.Action<StringBuilder> action2)
    where UOM : IConstant<string>, IBqlOperand, new();

  void scriptFunctionDatePart<UOM>(
    StringBuilder text,
    UOM timeUnitsConst,
    System.Action<StringBuilder> action1)
    where UOM : IConstant<string>, IBqlOperand, new();

  void scriptFunctionIsNull(
    StringBuilder text,
    TypeCode getTypeCodeForOperand,
    TypeCode typeCode,
    System.Action<StringBuilder> typeP1,
    System.Action<StringBuilder> typeP2);

  string scriptDateAdd<T>(string dbNow, int delta) where T : IConstant<string>, IBqlOperand, new();

  void scriptFunction<T1, T2>(
    Case2<T1, T2> fn,
    StringBuilder text,
    TypeCode getTypeCodeForOperand,
    System.Action<StringBuilder> writeWhere,
    System.Action<StringBuilder> writeOp1)
    where T1 : IBqlWhere, new()
    where T2 : IBqlOperand;

  void scriptFunction<T1, T2, TNext>(
    Case2<T1, T2, TNext> fn,
    StringBuilder text,
    TypeCode getTypeCodeForOperand,
    System.Action<StringBuilder> writeWhere,
    System.Action<StringBuilder> writeOp1,
    System.Action<StringBuilder> writeNext)
    where T1 : IBqlWhere, new()
    where T2 : IBqlOperand
    where TNext : IBqlCase, new();

  void scriptFunction<TCase, TDefault>(
    Switch<TCase, TDefault> p,
    StringBuilder text,
    TypeCode typeCode,
    System.Action<StringBuilder> writeCases,
    System.Action<StringBuilder> writeElse)
    where TCase : IBqlCase, new()
    where TDefault : IBqlOperand;

  void scriptFunction<TCase>(
    Switch<TCase> fn,
    StringBuilder text,
    System.Action<StringBuilder> writeCases)
    where TCase : IBqlCase, new();

  void scriptFunction<Search1, T2>(
    Minus1<Search1, T2> minus1,
    StringBuilder text,
    TypeCode typeCode1,
    TypeCode typeCode2,
    System.Action<StringBuilder> action1,
    System.Action<StringBuilder> action2)
    where Search1 : IBqlSearch
    where T2 : IBqlOperand;

  bool isEndOfSQLStatement(string s);

  string WildcardInRange(char start, char end);

  string WildcardInSet(params char[] characters);

  string WildcardNotInRange(char start, char end);

  string WildcardNotInSet(params char[] characters);

  string WildcardAnySingle { get; }

  string WildcardAnything { get; }

  char WildcardFieldSeparatorChar { get; }

  char WildcardErrorSeparatorChar { get; }

  char WildcardParamSeparatorChar { get; }

  string WildcardFieldSeparator { get; }

  string BinaryLength { get; }

  string CharLength { get; }

  string BatchSeparator { get; }

  string GetDate { get; }

  string GetUtcDate { get; }

  string joinForEnsure(string tableName);

  bool IsPadSpaceCompatible { get; }

  int DefaultPadSpaceLength { get; }

  char LiteralToOpenIdentifier { get; }

  char LiteralToCloseIdentifier { get; }

  bool ClusterdIndexSupported { get; }

  bool IsPadSpaceColumn(System.Type table, string columnName);

  PXDbType GetConstDbType(System.Type type);

  string functions2sql(string s, string[] pars);

  string scriptUpdateJoin(string tableName, string joinedTables, string assignments, string where);

  string quoteFn(string customScalarFunctionInvocation);

  string ConvertBinarySubstringToInt(string field, int startPos, int length);

  string castAsInteger<T>(T value);

  string castAsDecimal<T>(T value, int precision, int scale);

  bool tryExtractAttributes(
    string firstColumn,
    IDictionary<string, int> fieldIndices,
    out string[] attributes);

  string enquoteValue(object value, PXDbType type = PXDbType.Unspecified);

  bool isFullJoinSupported();

  bool isStronglyTyped { get; }

  string getInsertWatchDog();

  string quoteLiteral(string p);

  string getStatementSeparator();

  string getLastInsertedIdentity();

  string getLastInsertedIdentity(string columnName);

  string getCurrentTimestamp();

  string CompanyIdEnquoted { get; }

  void scriptLikeOperand(StringBuilder text, System.Action parseOperand);

  string PrepareLikeCondition(string text);

  BqlFullTextRenderingMethod FullTextRenderingMode { get; }

  object FalseValue { get; }

  object TrueValue { get; }

  QueryHints GetHintsNeededByQuery(string query);

  string ApplyQueryHints(string query, QueryHints hints);

  IEnumerable<string> GetQueryHintsText(QueryHints hints);

  string getFormatStringToAggregateField(string function, PXDbType fieldType);

  void AppendTableOptions(StringBuilder text, IBqlQueryHintTableOptions _options);

  string unquoteTable(string tableName);

  int[] GetGuidByteOrder();

  int CompareTimestamps(byte[] first, byte[] second);

  byte[] GetLatestTimestamp(byte[] first, byte[] second);

  string GetKvExtTableName(string tableName);

  string MakeCorrectDbIdentifier(string identifier);

  string scriptUpdateJoin(
    List<System.Type> tables,
    string tableName,
    Query rawFromWhere,
    List<KeyValuePair<SQLExpression, SQLExpression>> assignments);

  PXDbType GetDBType(SQLExpression expression);

  SQLExpression SubstituteExternalColumnAliases(
    SQLExpression internalField,
    Dictionary<string, SQRenamer> aliases,
    QueryPart queryPart,
    bool insideAggregate,
    HashSet<string> excludes,
    bool isMaxAggregate,
    Func<Column, bool> isInsideGroupBy);

  string GetCompareCollation(PXDbType type);
}
