// Decompiled with JetBrains decompiler
// Type: PX.Data.SqlDialectBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using PX.DbServices.Model;
using PX.DbServices.Model.Native;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.MsSql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class SqlDialectBase : ISqlDialect
{
  protected internal const string KvExtSuffix = "KvExt";
  private string companyIdEnquoted;
  public static double ticksPerMs = (double) Stopwatch.Frequency / 1000.0;
  private static readonly int[] guidOrder = new int[16 /*0x10*/]
  {
    3,
    2,
    1,
    0,
    5,
    4,
    7,
    6,
    9,
    8,
    15,
    14,
    13,
    12,
    11,
    10
  };

  public Version DbmsVersion { get; }

  protected SqlDialectBase(Version dbmsVersion) => this.DbmsVersion = dbmsVersion;

  public abstract Connection GetConnection();

  public abstract SqlDialect DialectType { get; }

  public string binaryMaskTest(string companyMask, int company, int rights)
  {
    string startPos = Convert.ToString((company + 3) / 4);
    int num = 2 * ((company + 3) % 4);
    string op2 = Convert.ToString(rights << num);
    return $"{op2} = {this.bitAnd(this.asc(this.substr(companyMask, startPos, "1")), op2)}";
  }

  public string binaryMaskSub(string companyMask, int company, int rights)
  {
    return this.quoteFn($"binaryMaskSub({companyMask}, {company}, {rights})");
  }

  public virtual string quoteFn(string scalarFunctionExpression) => scalarFunctionExpression;

  public virtual string ConvertBinarySubstringToInt(string field, int startPos, int length)
  {
    return $"SUBSTRING({field}, {startPos}, {length})";
  }

  public virtual string castAsInteger<T>(T value) => $"CAST({value} AS INTEGER)";

  public virtual string castAsDecimal<T>(T value, int precision, int scale)
  {
    return $"CAST({value} AS DECIMAL({precision}, {scale}))";
  }

  public abstract bool tryExtractAttributes(
    string firstColumn,
    IDictionary<string, int> fieldIndices,
    out string[] attributes);

  public abstract string enquoteValue(object value, PXDbType type = PXDbType.Unspecified);

  protected IEnumerable<object> enumerateArray(Array vvv)
  {
    foreach (object obj in vvv)
      yield return obj;
  }

  public string byteOfMaskAnd(string sourceStr, int index, string mask)
  {
    return this.bitAnd(this.asc(this.substr(sourceStr, index.ToString(), "1")), mask);
  }

  public virtual string makeDbIdentifier(string name) => name;

  public abstract string quoteDbIdentifier(string tablename);

  public abstract bool isColumnNameQuoted(string name);

  public string unquoteColumn(string name)
  {
    string[] strArray = name.Split('.');
    string str1 = (string) null;
    foreach (string name1 in strArray)
    {
      string str2 = this.isColumnNameQuoted(name1) ? name1.Substring(1, name1.Length - 2) : name1;
      str1 = str1 == null ? str2 : $"{str1}.{str2}";
    }
    return str1;
  }

  public abstract string limitRowsBeingSelected(string selectClause, long skip, long topN);

  public virtual string limitRowsBeingSelected(string selectClause, long topN)
  {
    return this.limitRowsBeingSelected(selectClause, 0L, topN);
  }

  public virtual void scriptFunctionSub(
    StringBuilder text,
    TypeCode tOp1,
    TypeCode tOp2,
    string operand1,
    string operand2)
  {
    text.AppendFormat(tOp1 == TypeCode.DateTime ? " DATEADD(dd, -({1}), {0})" : " ({0} - {1})", (object) operand1, (object) operand2);
  }

  public virtual void scriptFunctionAdd(
    StringBuilder text,
    TypeCode tOp1,
    TypeCode tOp2,
    string operand1,
    string operand2)
  {
    text.AppendFormat(tOp1 == TypeCode.DateTime ? " DATEADD(dd, {1}, {0})" : " ({0} + {1})", (object) operand1, (object) operand2);
  }

  public virtual void scriptFunctionBitwiseAnd(
    StringBuilder text,
    TypeCode tOp1,
    TypeCode tOp2,
    string operand1,
    string operand2)
  {
    text.AppendFormat(" ({0}&{1})", (object) operand1, (object) operand2);
  }

  public virtual void scriptFunctionDateDiff<UOM>(
    StringBuilder text,
    UOM timeunits,
    TypeCode typeCode1,
    TypeCode typeCode2,
    System.Action<StringBuilder> action1,
    System.Action<StringBuilder> action2)
    where UOM : IConstant<string>, IBqlOperand, new()
  {
    text.Append(" DATEDIFF(").Append(new UOM().Value).Append(", ");
    action1(text);
    text.Append(" , ");
    action2(text);
    text.Append(")");
  }

  public virtual void scriptFunctionDatePart<UOM>(
    StringBuilder text,
    UOM timeunits,
    System.Action<StringBuilder> action1)
    where UOM : IConstant<string>, IBqlOperand, new()
  {
    text.Append(" DATEPART(").Append(new UOM().Value).Append(", ");
    action1(text);
    text.Append(")");
  }

  public virtual void scriptFunctionIsNull(
    StringBuilder text,
    TypeCode getTypeCodeForOperand,
    TypeCode typeCode,
    System.Action<StringBuilder> typeP1,
    System.Action<StringBuilder> typeP2)
  {
    text.Append(" ISNULL(");
    typeP1(text);
    text.Append(" , ");
    typeP2(text);
    text.Append(")");
  }

  public abstract string scriptDateAdd<T>(string dbNow, int delta) where T : IConstant<string>, IBqlOperand, new();

  public virtual void scriptFunction<T1, T2>(
    Case2<T1, T2> fn,
    StringBuilder text,
    TypeCode getTypeCodeForOperand,
    System.Action<StringBuilder> writeWhere,
    System.Action<StringBuilder> writeOp1)
    where T1 : IBqlWhere, new()
    where T2 : IBqlOperand
  {
    text.Append(" WHEN ");
    writeWhere(text);
    text.Append(" THEN ");
    writeOp1(text);
  }

  public virtual void scriptFunction<T1, T2, TNext>(
    Case2<T1, T2, TNext> fn,
    StringBuilder text,
    TypeCode getTypeCodeForOperand,
    System.Action<StringBuilder> writeWhere,
    System.Action<StringBuilder> writeOp1,
    System.Action<StringBuilder> writeNext)
    where T1 : IBqlWhere, new()
    where T2 : IBqlOperand
    where TNext : IBqlCase, new()
  {
    text.Append(" WHEN ");
    writeWhere(text);
    text.Append(" THEN ");
    writeOp1(text);
    writeNext(text);
  }

  public virtual void scriptFunction<TCase, TDefault>(
    Switch<TCase, TDefault> p,
    StringBuilder text,
    TypeCode typeCode,
    System.Action<StringBuilder> writeCases,
    System.Action<StringBuilder> writeElse)
    where TCase : IBqlCase, new()
    where TDefault : IBqlOperand
  {
    text.Append(" CASE");
    writeCases(text);
    text.Append(" ELSE ");
    writeElse(text);
    text.Append(" END");
  }

  public virtual void scriptFunction<TCase>(
    Switch<TCase> fn,
    StringBuilder text,
    System.Action<StringBuilder> writeCases)
    where TCase : IBqlCase, new()
  {
    text.Append(" CASE");
    writeCases(text);
    text.Append(" END");
  }

  public virtual void scriptFunction<Search1, T2>(
    Minus1<Search1, T2> minus1,
    StringBuilder text,
    TypeCode typeCode1,
    TypeCode typeCode2,
    System.Action<StringBuilder> action1,
    System.Action<StringBuilder> action2)
    where Search1 : IBqlSearch
    where T2 : IBqlOperand
  {
    text.Append(" DATEDIFF(mi, ");
    action2(text);
    text.Append(", ");
    action1(text);
    text.Append(")");
  }

  public virtual string scriptUpdateJoin(
    string tableName,
    string joinedTables,
    string assignments,
    string where)
  {
    StringBuilder stringBuilder = new StringBuilder("UPDATE ");
    stringBuilder.Append(tableName).Append(" ");
    stringBuilder.Append(assignments).Append(" FROM ");
    stringBuilder.Append(joinedTables);
    if (where != null)
      stringBuilder.Append(" WHERE ").Append(where);
    return stringBuilder.ToString();
  }

  public abstract bool isRealTable(string tableName);

  protected virtual string BinaryPrefix { get; } = "0x";

  public virtual string getCompanyMask(int companyId)
  {
    int num1 = (companyId + 3) / 4;
    int num2 = 2 * ((companyId + 3) % 4);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(this.BinaryPrefix).Append('0', (num1 - 1) * 2).Append((3 << num2).ToString("X2"));
    return stringBuilder.ToString();
  }

  public abstract string identCurrent(string tableName);

  public abstract string caseWhenThenElse(string condition, string afterThen, string afterElse);

  public abstract string concat(IEnumerable<string> parts);

  public string concat(params string[] parts) => this.concat((IEnumerable<string>) parts);

  public string strlen(string inputString) => $"{this.CharLength}({inputString})";

  public abstract string substr(string inputString, string startPos, string len);

  public abstract string asc(string character);

  public abstract string @char(string number);

  public string bitOr(string op1, string op2) => $"{op1} | {op2}";

  public string bitAnd(string op1, string op2) => $"{op1} & {op2}";

  public string bitNot(string op) => $"~({op})";

  public virtual string WildcardInRange(char start, char end)
  {
    return new string(new char[5]
    {
      '[',
      start,
      '-',
      end,
      ']'
    });
  }

  public virtual string WildcardInSet(params char[] characters) => $"[{new string(characters)}]";

  public virtual string WildcardNotInRange(char start, char end)
  {
    return new string(new char[6]
    {
      '[',
      '^',
      start,
      '-',
      end,
      ']'
    });
  }

  public virtual string WildcardNotInSet(params char[] characters)
  {
    return $"[^{new string(characters)}]";
  }

  public virtual string WildcardAnySingle => "_";

  public virtual string WildcardAnything => "%";

  public virtual char WildcardFieldSeparatorChar { get; }

  public virtual char WildcardErrorSeparatorChar { get; } = '\u001E';

  public virtual char WildcardParamSeparatorChar { get; } = '\u001F';

  public string WildcardFieldSeparator => this.WildcardFieldSeparatorChar.ToString();

  public abstract string BinaryLength { get; }

  public abstract string BatchSeparator { get; }

  public abstract string GetDate { get; }

  public abstract string GetUtcDate { get; }

  public abstract string joinForEnsure(string tableName);

  /// <inheritdoc />
  public virtual bool IsPadSpaceCompatible { get; } = true;

  /// <inheritdoc />
  public int DefaultPadSpaceLength { get; } = 64 /*0x40*/;

  public abstract char LiteralToOpenIdentifier { get; }

  public abstract char LiteralToCloseIdentifier { get; }

  public virtual bool ClusterdIndexSupported { get; } = true;

  public virtual bool isEndOfSQLStatement(string s)
  {
    if (s == null)
      return true;
    string upper = s.Trim().ToUpper();
    string batchSeparator = this.BatchSeparator;
    return upper == batchSeparator || upper.Contains(batchSeparator + "\n") || upper.Contains(batchSeparator + "\t");
  }

  public bool IsPadSpaceColumn(System.Type table, string columnName)
  {
    if (this.IsPadSpaceCompatible)
      return true;
    MemberInfo element = ((IEnumerable<MemberInfo>) table.GetMembers(BindingFlags.Instance | BindingFlags.Public)).Where<MemberInfo>((Func<MemberInfo, bool>) (m => m.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase) && m.MemberType == MemberTypes.Property)).SingleOrDefault<MemberInfo>();
    return (object) element != null && element.GetCustomAttributes().Any<Attribute>((Func<Attribute, bool>) (a =>
    {
      switch (a)
      {
        case PXDimensionSelectorAttribute _:
          return true;
        case PXDBStringAttribute pxdbStringAttribute2:
          return pxdbStringAttribute2.PadSpaced;
        default:
          return false;
      }
    }));
  }

  public virtual PXDbType GetConstDbType(System.Type type)
  {
    System.Type type1 = type;
    PXDbType constDbType;
    if ((object) type1 != null)
    {
      if (type1 == typeof (bool))
      {
        constDbType = PXDbType.Bit;
        goto label_18;
      }
      if (type1 == typeof (int))
      {
        constDbType = PXDbType.Int;
        goto label_18;
      }
      if (type1 == typeof (Decimal))
      {
        constDbType = PXDbType.Decimal;
        goto label_18;
      }
      if (type1 == typeof (long))
      {
        constDbType = PXDbType.BigInt;
        goto label_18;
      }
      if (type1 == typeof (Guid))
      {
        constDbType = PXDbType.UniqueIdentifier;
        goto label_18;
      }
      if (type1 == typeof (System.DateTime))
      {
        constDbType = PXDbType.DateTime;
        goto label_18;
      }
      if (type1 == typeof (short))
      {
        constDbType = PXDbType.SmallInt;
        goto label_18;
      }
      if (type1 == typeof (float))
      {
        constDbType = PXDbType.Float;
        goto label_18;
      }
    }
    constDbType = PXDbType.Unspecified;
label_18:
    return constDbType;
  }

  public virtual string functions2sql(string opcode, string[] args)
  {
    string lower = opcode.ToLower();
    if (lower != null)
    {
      switch (lower.Length)
      {
        case 3:
          switch (lower[0])
          {
            case 'a':
              switch (lower)
              {
                case "abs":
                  return string.Format("ABS({0})", (object[]) args);
                case "avg":
                  return string.Format("AVG({0})", (object[]) args);
              }
              break;
            case 'd':
              if (lower == "day")
                return string.Format("DAY({0})", (object[]) args);
              break;
            case 'i':
              if (lower == "iif")
                return string.Format("CASE WHEN ({0}) THEN {1} ELSE {2} END", (object[]) args);
              break;
            case 'l':
              if (lower == "len")
                return string.Format("LEN({0})", (object[]) args);
              break;
            case 'm':
              switch (lower)
              {
                case "min":
                  return args.Length < 2 ? string.Format("MIN({0})", (object[]) args) : string.Format("CASE WHEN {0} <= {1} THEN {0} ELSE {1} END", (object[]) args);
                case "max":
                  return args.Length < 2 ? string.Format("MAX({0})", (object[]) args) : string.Format("CASE WHEN {0} >= {1} THEN {0} ELSE {1} END", (object[]) args);
              }
              break;
            case 'n':
              if (lower == "now")
                return string.Format("GETDATE()", (object[]) args);
              break;
            case 'p':
              if (lower == "pow")
                return string.Format("POWER ({0}, {1})", (object[]) args);
              break;
            case 's':
              if (lower == "sum")
                return string.Format("SUM({0})", (object[]) args);
              break;
          }
          break;
        case 4:
          switch (lower[2])
          {
            case 'a':
              if (lower == "year")
                return string.Format("YEAR({0})", (object[]) args);
              break;
            case 'b':
              if (lower == "cdbl")
                return string.Format("CONVERT(FLOAT(53), {0})", (object[]) args);
              break;
            case 'e':
              if (lower == "cdec")
                return string.Format("CONVERT(DECIMAL, {0})", (object[]) args);
              break;
            case 'f':
              if (lower == "left")
                return string.Format("LEFT ({0}, {1})", (object[]) args);
              break;
            case 'i':
              if (lower == "trim")
                return string.Format("LTRIM(RTRIM({0}))", (object[]) args);
              break;
            case 'n':
              switch (lower)
              {
                case "csng":
                  return string.Format("CONVERT(REAL, {0})", (object[]) args);
                case "cint":
                  return string.Format("CONVERT(INT, {0})", (object[]) args);
              }
              break;
            case 't':
              if (lower == "cstr")
                return string.Format("CONVERT(NVARCHAR(Max), {0})", (object[]) args);
              break;
            case 'u':
              if (lower == "hour")
                return string.Format("DATEPART(HOUR, {0})", (object[]) args);
              break;
          }
          break;
        case 5:
          switch (lower[4])
          {
            case 'd':
              if (lower == "round")
                return string.Format("ROUND ({0}, {1})", (object[]) args);
              break;
            case 'e':
              switch (lower)
              {
                case "cdate":
                  return string.Format("CONVERT(DATETIME, {0})", (object[]) args);
                case "ucase":
                  return string.Format("UPPER({0})", (object[]) args);
                case "lcase":
                  return string.Format("LOWER({0})", (object[]) args);
              }
              break;
            case 'g':
              if (lower == "clong")
                return string.Format("CONVERT(BIGINT, {0})", (object[]) args);
              break;
            case 'h':
              if (lower == "month")
                return string.Format("MONTH({0})", (object[]) args);
              break;
            case 'l':
              if (lower == "cbool")
                return string.Format("CONVERT(BIT, {0})", (object[]) args);
              break;
            case 'm':
              switch (lower)
              {
                case "ltrim":
                  return string.Format("LTRIM({0})", (object[]) args);
                case "rtrim":
                  return string.Format("RTRIM({0})", (object[]) args);
              }
              break;
            case 'r':
              switch (lower)
              {
                case "instr":
                  return string.Format("CHARINDEX({1},{0})", (object[]) args);
                case "floor":
                  return string.Format("FLOOR ({0})", (object[]) args);
              }
              break;
            case 't':
              switch (lower)
              {
                case "right":
                  return string.Format("RIGHT({0}, {1})", (object[]) args);
                case "count":
                  return string.Format("COUNT({0})", (object[]) args);
              }
              break;
            case 'y':
              if (lower == "today")
                return string.Format("CAST( DATEADD(DAY, 0, DATEDIFF(DAY, 0, GETDATE())) as smalldatetime)", (object[]) args);
              break;
          }
          break;
        case 6:
          switch (lower[2])
          {
            case 'c':
              if (lower == "second")
                return string.Format("DATENAME(second, {0})", (object[]) args);
              break;
            case 'h':
              if (lower == "cshort")
                return string.Format("CONVERT(SMALLINT, {0})", (object[]) args);
              break;
            case 'i':
              if (lower == "switch")
              {
                StringBuilder stringBuilder = new StringBuilder("CASE");
                for (int index = 0; index < args.Length - 1; index += 2)
                  stringBuilder.AppendFormat(" WHEN {0} THEN {1}", (object) args[index], (object) args[index + 1]);
                if (args.Length % 2 == 1)
                  stringBuilder.Append(" ELSE ").Append(args[args.Length - 1]);
                return stringBuilder.Append(" END").ToString();
              }
              break;
            case 'l':
              if (lower == "nullif")
                return string.Format("NULLIF({0}, {1})", (object[]) args);
              break;
            case 'n':
              switch (lower)
              {
                case "concat":
                  return string.Join(" + ", args);
                case "minute":
                  return string.Format("DATENAME(minute, {0})", (object[]) args);
                case "isnull":
                  return string.Format("COALESCE({0}, {1})", (object[]) args);
              }
              break;
            case 'r':
              if (lower == "format")
                throw new NotImplementedException();
              break;
            case 'w':
              if (lower == "nowutc")
                return string.Format("GETUTCDATE()", (object[]) args);
              break;
          }
          break;
        case 7:
          switch (lower[0])
          {
            case 'c':
              if (lower == "ceiling")
                return string.Format("CEILING ({0})", (object[]) args);
              break;
            case 'd':
              if (lower == "dateadd")
              {
                string str1;
                if (args[1] != null)
                  str1 = args[1].Trim('\'').ToLower();
                else
                  str1 = string.Empty;
                string str2 = str1;
                if (str2 != null)
                {
                  switch (str2.Length)
                  {
                    case 1:
                      switch (str2[0])
                      {
                        case 'd':
                          return string.Format("DATEADD(day, {2}, {0})", (object[]) args);
                        case 'h':
                          return string.Format("DATEADD(hour, {2}, {0})", (object[]) args);
                        case 'm':
                          return string.Format("DATEADD(month, {2}, {0})", (object[]) args);
                        case 'n':
                          return string.Format("DATEADD(minute, {2}, {0})", (object[]) args);
                        case 'q':
                          return string.Format("DATEADD(quarter, {2}, {0})", (object[]) args);
                        case 's':
                          return string.Format("DATEADD(second, {2}, {0})", (object[]) args);
                        case 'w':
                          break;
                        case 'y':
                          return string.Format("DATEADD(year, {2}, {0})", (object[]) args);
                        default:
                          goto label_126;
                      }
                      break;
                    case 2:
                      if (str2 == "ww")
                        break;
                      goto label_126;
                    default:
                      goto label_126;
                  }
                  return string.Format("DATEADD(week, {2}, {0})", (object[]) args);
                }
label_126:
                return "0";
              }
              break;
            case 'p':
              if (lower == "padleft")
                return string.Format("{0} + REPLICATE({2}, {1} - LEN({0})) ", (object[]) args);
              break;
            case 'q':
              if (lower == "quarter")
                return string.Format("DATEPART(QUARTER, {0})", (object[]) args);
              break;
            case 'r':
              if (lower == "replace")
                return string.Format("REPLACE({0}, {1}, {2})", (object[]) args);
              break;
          }
          break;
        case 8:
          switch (lower[0])
          {
            case 'd':
              if (lower == "datediff")
              {
                string str3;
                if (args[0] != null)
                  str3 = args[0].Trim('\'').ToLower();
                else
                  str3 = string.Empty;
                string str4 = str3;
                if (str4 != null)
                {
                  switch (str4.Length)
                  {
                    case 1:
                      switch (str4[0])
                      {
                        case 'd':
                          return string.Format("DATEDIFF(day, {1}, {2})", (object[]) args);
                        case 'h':
                          return string.Format("DATEDIFF(hour, {1}, {2})", (object[]) args);
                        case 'm':
                          return string.Format("DATEDIFF(month, {1}, {2})", (object[]) args);
                        case 'n':
                          return string.Format("DATEDIFF(minute, {1}, {2})", (object[]) args);
                        case 'q':
                          return string.Format("DATEDIFF(quarter, {1}, {2})", (object[]) args);
                        case 's':
                          return string.Format("DATEDIFF(second, {1}, {2})", (object[]) args);
                        case 'w':
                          break;
                        case 'y':
                          return string.Format("DATEDIFF(year, {1}, {2})", (object[]) args);
                        default:
                          goto label_110;
                      }
                      break;
                    case 2:
                      if (str4 == "ww")
                        break;
                      goto label_110;
                    default:
                      goto label_110;
                  }
                  return string.Format("DATEDIFF(week, {1}, {2})", (object[]) args);
                }
label_110:
                return "0";
              }
              break;
            case 'i':
              if (lower == "instrrev")
                return string.Format("LEN({0}) - CHARINDEX(REVERSE({1}), REVERSE({0}))", (object[]) args);
              break;
            case 'p':
              if (lower == "padright")
                return string.Format("REPLICATE({2}, {1} - LEN({0})) + {0}", (object[]) args);
              break;
            case 't':
              if (lower == "todayutc")
                return string.Format("CAST( DATEADD(DAY, 0, DATEDIFF(DAY, 0, GETUTCDATE())) as smalldatetime)", (object[]) args);
              break;
          }
          break;
        case 9:
          switch (lower[5])
          {
            case 'r':
              if (lower == "substring")
                return string.Format("SUBSTRING({0}, {1}, {2})", (object[]) args);
              break;
            case 'w':
              if (lower == "dayofweek")
                return string.Format("DATEPART(weekday, {0})", (object[]) args);
              break;
            case 'y':
              if (lower == "dayofyear")
                return string.Format("DATENAME(dayofyear, {0})", (object[]) args);
              break;
          }
          break;
      }
    }
    throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Unsupported formula operator '{0}.'", (object) opcode));
  }

  public virtual bool isFullJoinSupported() => true;

  public virtual bool isStronglyTyped { get; }

  public virtual string getInsertWatchDog() => "INSERT WatchDog";

  public virtual string quoteLiteral(string value)
  {
    return ((SqlScripterBase) PointMsSqlServer.GenericScripter).quoteLiteral(value, new bool?());
  }

  public virtual string getStatementSeparator() => " ";

  public abstract string getLastInsertedIdentity();

  public virtual string getLastInsertedIdentity(string columnName)
  {
    return $"{this.getStatementSeparator()}SELECT {this.getLastInsertedIdentity()}";
  }

  public abstract string getCurrentTimestamp();

  public string CompanyIdEnquoted
  {
    get
    {
      if (this.companyIdEnquoted == null)
        this.companyIdEnquoted = this.quoteDbIdentifier("CompanyID");
      return this.companyIdEnquoted;
    }
  }

  public virtual void scriptLikeOperand(StringBuilder text, System.Action parseOperand)
  {
    parseOperand();
  }

  public virtual string PrepareLikeCondition(string text) => text;

  public virtual BqlFullTextRenderingMethod FullTextRenderingMode
  {
    get => BqlFullTextRenderingMethod.NeutralLike;
  }

  public virtual object FalseValue { get; } = (object) "0";

  public virtual object TrueValue { get; } = (object) "1";

  public virtual IEnumerable<string> GetQueryHintsText(QueryHints hints)
  {
    return Enumerable.Empty<string>();
  }

  public virtual string getFormatStringToAggregateField(string function, PXDbType fieldType)
  {
    string toAggregateField = string.Format(function, (object) "({0})");
    if (toAggregateField == function)
      toAggregateField = function + "({0})";
    return toAggregateField;
  }

  public abstract void AppendTableOptions(StringBuilder text, IBqlQueryHintTableOptions _options);

  public abstract string unquoteTable(string tableName);

  public virtual string quoteTableAndColumn(string tableName, string columnName)
  {
    return $"{this.quoteDbIdentifier(tableName)}.{this.quoteDbIdentifier(columnName)}";
  }

  public virtual QueryHints GetHintsNeededByQuery(string query) => QueryHints.None;

  public abstract string ApplyQueryHints(string query, QueryHints hints);

  public abstract string CharLength { get; }

  public virtual int[] GetGuidByteOrder() => SqlDialectBase.guidOrder;

  public virtual int CompareTimestamps(byte[] first, byte[] second)
  {
    if (first == null && second == null)
      return 0;
    if (first == null)
      return -1;
    if (second == null)
      return 1;
    for (int index = 0; index < first.Length && index < second.Length; ++index)
    {
      if ((int) first[index] != (int) second[index])
        return (int) first[index] <= (int) second[index] ? -1 : 1;
    }
    return 0;
  }

  public virtual byte[] GetLatestTimestamp(byte[] first, byte[] second)
  {
    return this.CompareTimestamps(first, second) < 0 ? second : first;
  }

  public virtual string GetKvExtTableName(string tableName)
  {
    return AcumaticaDb.GetKvExtTableName(tableName);
  }

  /// <summary>
  /// PGSQL: The system uses no more than NAMEDATALEN-1 bytes of an identifier; longer names can be written in commands, but they will be truncated.
  /// By default, NAMEDATALEN is 64 so the maximum identifier length is 63 bytes.
  /// <see cref="!:https://www.postgresql.org/docs/current/sql-syntax-lexical.html#SQL-SYNTAX-IDENTIFIERS" />
  /// 
  /// MYSQL: The maximum length for Alias identifier - 256 (with exception).
  /// 	   The maximum length for Table identifier - 64
  /// 	   The maximum length for Column identifier - 64
  /// Aliases for column names in CREATE VIEW statements are checked against the maximum column length of 64 characters
  /// <see cref="!:https://dev.mysql.com/doc/refman/8.0/en/identifier-length.html" />
  /// 
  /// MSSQL:
  /// Regular identifiers
  /// 	Comply with the rules for the format of identifiers. Regular identifiers are not delimited when they are used in Transact-SQL statements.
  /// Delimited identifiers
  /// 	Are enclosed in double quotation marks (") or brackets ([ ]). Identifiers that comply with the rules for the format of identifiers might not be delimited.
  /// Both regular and delimited identifiers must contain from 1 through 128 characters.
  /// For local temporary tables, the identifier can have a maximum of 116 characters.
  /// <see cref="!:https://learn.microsoft.com/en-us/sql/relational-databases/databases/database-identifiers?view=sql-server-ver16" />
  /// </summary>
  public virtual string MakeCorrectDbIdentifier(string identifier) => identifier;

  /// <inheritdoc />
  public virtual string scriptUpdateJoin(
    List<System.Type> tables,
    string tableName,
    Query rawFromWhere,
    List<KeyValuePair<SQLExpression, SQLExpression>> assignments)
  {
    string where = rawFromWhere.GetWhere()?.SQLQuery(this.GetConnection()).ToString();
    string joinedTables = rawFromWhere.Where((SQLExpression) null).SQLQuery(this.GetConnection()).ToString();
    StringBuilder stringBuilder = new StringBuilder(" SET ");
    for (int index = 0; index < assignments.Count; ++index)
    {
      if (index > 0)
        stringBuilder.Append(", ");
      KeyValuePair<SQLExpression, SQLExpression> assignment = assignments[index];
      string str1 = assignment.Key.SQLQuery(this.GetConnection()).ToString();
      assignment = assignments[index];
      string str2 = assignment.Value.SQLQuery(this.GetConnection()).ToString();
      stringBuilder.Append(str1).Append(" = ").Append(str2);
    }
    return this.scriptUpdateJoin(tableName, joinedTables, stringBuilder.ToString(), where);
  }

  public abstract PXDbType GetDBType(SQLExpression expression);

  public SQLExpression SubstituteExternalColumnAliases(
    SQLExpression internalField,
    Dictionary<string, SQRenamer> aliases,
    QueryPart queryPart,
    bool insideAggregate,
    HashSet<string> excludes,
    bool isMaxAggregate,
    Func<Column, bool> isInsideGroupBy)
  {
    SQLExpression sqlExpression = this.AlternateSubstituteExternalColumnAliases(internalField, aliases, queryPart, insideAggregate, excludes, isMaxAggregate, isInsideGroupBy);
    if (sqlExpression != null)
      return sqlExpression;
    if (internalField is Column)
      return internalField;
    if (queryPart == QueryPart.GroupBy && internalField is SubQuery)
      throw new PXException("Cannot use an aggregate or a subquery in an expression used for the group by list of a GROUP BY clause");
    return internalField.substituteExternalColumnAliases(aliases, queryPart, insideAggregate, excludes, isMaxAggregate, isInsideGroupBy);
  }

  public virtual SQLExpression AlternateSubstituteExternalColumnAliases(
    SQLExpression internalField,
    Dictionary<string, SQRenamer> aliases,
    QueryPart queryPart,
    bool insideAggregate,
    HashSet<string> excludes,
    bool isMaxAggregate,
    Func<Column, bool> isInsideGroupBy)
  {
    return (SQLExpression) null;
  }

  public virtual string GetCompareCollation(PXDbType type) => "";
}
