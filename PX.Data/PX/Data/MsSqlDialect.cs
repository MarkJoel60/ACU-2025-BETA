// Decompiled with JetBrains decompiler
// Type: PX.Data.MsSqlDialect
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using PX.DbServices;
using PX.DbServices.Model.Native;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.MsSql;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class MsSqlDialect : SqlDialectBase
{
  public readonly SqlServerEngineEdition serverEdition;
  private MSSQLConnection connection = new MSSQLConnection();
  private const string FOR_XML_PATH_V = " FOR XML PATH('v')";

  public MsSqlDialect(Version dbmsVersion, SqlServerEngineEdition edition)
    : base(dbmsVersion)
  {
    this.serverEdition = edition;
  }

  public override PX.Data.SQLTree.Connection GetConnection() => (PX.Data.SQLTree.Connection) this.connection;

  public override SqlDialect DialectType { get; }

  public override string scriptDateAdd<T>(string dbNow, int delta)
  {
    string str1 = "dd";
    string str2 = new T().Value;
    if (str2 != null)
    {
      switch (str2.Length)
      {
        case 2:
          switch (str2[1])
          {
            case 'd':
              if (str2 == "dd")
              {
                str1 = "dd";
                break;
              }
              break;
            case 'h':
              if (str2 == "hh")
              {
                str1 = "hh";
                break;
              }
              break;
            case 'i':
              if (str2 == "mi")
              {
                str1 = "mi";
                break;
              }
              break;
            case 'm':
              if (str2 == "mm")
              {
                str1 = "mm";
                break;
              }
              break;
            case 'q':
              if (str2 == "qq")
              {
                str1 = "qq";
                break;
              }
              break;
            case 's':
              switch (str2)
              {
                case "ss":
                  str1 = "ss";
                  break;
                case "ms":
                  str1 = "ms";
                  break;
              }
              break;
            case 'w':
              if (str2 == "ww")
              {
                str1 = "ww";
                break;
              }
              break;
          }
          break;
        case 4:
          if (str2 == "yyyy")
          {
            str1 = "yyyy";
            break;
          }
          break;
      }
    }
    return $" DateAdd({str1}, {delta}, {dbNow})";
  }

  public override bool isColumnNameQuoted(string name)
  {
    if (name == null)
      return false;
    int length = name.Length;
    return length >= 2 && name[length - 1] == ']';
  }

  public override string limitRowsBeingSelected(string selectClause, long skip, long topN)
  {
    if (selectClause.StartsWith("DECLARE ", StringComparison.OrdinalIgnoreCase))
      return selectClause;
    if (selectClause.EndsWith(" FOR XML PATH('v')"))
      return "SELECT " + selectClause;
    string str = topN <= 0L || skip != 0L || selectClause.StartsWith("TOP ") ? "SELECT " + selectClause : $"SELECT TOP ({topN}) {selectClause}";
    if (skip > 0L)
    {
      Func<long, string> func = (Func<long, string>) (i => i != 1L ? "ROWS" : "ROW");
      str = $"{str} OFFSET ({skip}) {func(skip)}";
      if (topN > 0L)
        str = $"{str} FETCH NEXT ({topN}) {func(topN)} ONLY";
    }
    return str;
  }

  public override string quoteFn(string scalarFunctionExpression)
  {
    return "dbo." + scalarFunctionExpression;
  }

  public override bool tryExtractAttributes(
    string firstColumn,
    IDictionary<string, int> fieldIndices,
    out string[] attributes)
  {
    attributes = new string[fieldIndices.Count];
    if (string.IsNullOrEmpty(firstColumn))
      return true;
    int num1 = 0;
    int num2;
    for (int length = firstColumn.Length; num1 < length; num1 = num2 + 4)
    {
      int num3 = firstColumn.IndexOf('"', num1 + 1);
      if (num3 < 0)
      {
        if (num1 == 0)
          return false;
        break;
      }
      int startIndex = firstColumn.IndexOf('"', num3 + 1);
      int num4 = firstColumn.IndexOf('>', startIndex);
      num2 = firstColumn.IndexOf('<', num4 + 1);
      if (startIndex > num3 && num2 > num4 && num4 > startIndex)
      {
        string str1 = firstColumn.Substring(num3 + 1, startIndex - num3 - 1);
        string str2 = firstColumn.Substring(num4 + 1, num2 - num4 - 1);
        int index;
        if (fieldIndices.TryGetValue(WebUtility.HtmlDecode(str1), out index) && attributes[index] == null)
          attributes[index] = WebUtility.HtmlDecode(str2);
      }
      else
        break;
    }
    return true;
  }

  public override string quoteDbIdentifier(string tablename)
  {
    return this.isColumnNameQuoted(tablename) ? tablename : $"[{tablename}]";
  }

  public override string identCurrent(string tableName) => $"IDENT_CURRENT('{tableName}')";

  public override string caseWhenThenElse(string condition, string afterThen, string afterElse)
  {
    return $"CASE WHEN {condition} THEN {afterThen} ELSE {afterElse} END";
  }

  public override string concat(IEnumerable<string> parts) => string.Join(" + ", parts);

  public override string substr(string inputString, string startPos, string len)
  {
    return $"SUBSTRING({inputString}, {startPos}, {len})";
  }

  public override string asc(string character) => character;

  public override string @char(string number) => $"CONVERT(BINARY(1), {number})";

  public override bool isRealTable(string tableName) => tableName[0] != '@';

  public override string BinaryLength => "DATALENGTH";

  public override string CharLength => "LEN";

  public override string BatchSeparator => "GO";

  public override string GetDate => "GETDATE()";

  public override string GetUtcDate => "GETUTCDATE()";

  public override string joinForEnsure(string tableName)
  {
    return $"LEFT JOIN {tableName} WITH (READCOMMITTEDLOCK) ON ";
  }

  public override char LiteralToOpenIdentifier { get; } = '[';

  public override char LiteralToCloseIdentifier { get; } = ']';

  public override string enquoteValue(object value, PXDbType type = PXDbType.Unspecified)
  {
    if (value == null)
      return "NULL";
    System.Type type1 = value.GetType();
    if (type1.IsArray)
      return string.Join(", ", this.enumerateArray((Array) value).Select<object, string>((Func<object, string>) (v => this.enquoteValue(v, PXDbType.Unspecified))));
    if (type1 == typeof (string))
    {
      string str = ((SqlScripterBase) PointMsSqlServer.GenericScripter).quoteLiteral(value as string, new bool?());
      if ((type == PXDbType.NVarChar || type == PXDbType.NChar ? 1 : (type == PXDbType.NText ? 1 : 0)) != 0 && !str.StartsWith("N"))
        str = "N" + str;
      return str;
    }
    if (type1 == typeof (System.DateTime))
    {
      System.DateTime dateTime = (System.DateTime) value;
      if (dateTime.Year < 1753)
        dateTime = dateTime.AddYears(1899);
      return $"CONVERT(DATETIME, '{dateTime.Year:0000}{dateTime.Month:00}{dateTime.Day:00} {dateTime.Hour}:{dateTime.Minute}:{dateTime.Second}.{dateTime.Millisecond:000}')";
    }
    if (type1 == typeof (Decimal))
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0:.0########}", value);
    if (type1 == typeof (double))
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "CONVERT(FLOAT, {0:.0########})", value);
    if (type1 == typeof (short))
      return $"CONVERT(SMALLINT, {value:g})";
    if (type1 == typeof (long))
      return $"CONVERT(BIGINT, {value:g})";
    return type1 == typeof (bool) ? (!(bool) value ? "CONVERT(BIT, 0)" : "CONVERT(BIT, 1)") : (type1 == typeof (Guid) ? $"CONVERT(UNIQUEIDENTIFIER, '{value?.ToString()}')" : value.ToString());
  }

  public override string getLastInsertedIdentity() => "@@IDENTITY";

  public override string getCurrentTimestamp() => "@@DBTS";

  public override string PrepareLikeCondition(string text) => text.Replace("[", "[[]");

  public override BqlFullTextRenderingMethod FullTextRenderingMode
  {
    get => BqlFullTextRenderingMethod.MsSqlJoinFreeTextTable;
  }

  public override string ApplyQueryHints(string query, QueryHints hints)
  {
    if (hints == QueryHints.None)
      return query;
    List<string> values = new List<string>();
    foreach (string str in this.GetQueryHintsText(hints))
      values.Add(str);
    return values.Count > 0 ? $"{query} OPTION({string.Join(", ", (IEnumerable<string>) values)})" : query;
  }

  public override IEnumerable<string> GetQueryHintsText(QueryHints hints)
  {
    if (hints.HasFlag((System.Enum) QueryHints.SqlServerOptionRecompile))
      yield return "RECOMPILE";
    if (hints.HasFlag((System.Enum) QueryHints.SqlServerOptimizeForUnknown))
      yield return "OPTIMIZE FOR UNKNOWN";
    if (hints.HasFlag((System.Enum) QueryHints.SqlServerNoRecursionLimit))
      yield return "MAXRECURSION 0";
  }

  public override string getFormatStringToAggregateField(string function, PXDbType fieldType)
  {
    string toAggregateField = base.getFormatStringToAggregateField(function, fieldType);
    if (fieldType == PXDbType.UniqueIdentifier && this.serverEdition != 5 && this.DbmsVersion.Major < 11)
      return string.Format("CAST(" + toAggregateField, (object) "CAST({0} as BINARY(16))) as UNIQUEIDENTIFIER");
    return fieldType == PXDbType.Bit ? string.Format("CONVERT (BIT, " + toAggregateField, (object) "{0}+0)") : toAggregateField;
  }

  public override void AppendTableOptions(StringBuilder text, IBqlQueryHintTableOptions _options)
  {
    if (_options == null)
      return;
    int length = text.Length;
    IEnumerable<string> forcedIndexes = _options.ForcedIndexes;
    if (forcedIndexes != null)
    {
      StringBuilderExtensions.AppendJoined<string>(text, forcedIndexes, " INDEX(", ", ", (Func<string, string>) null);
      if (length != text.Length)
        text.Append(")");
    }
    text.Insert(length, " WITH(");
    text.Append(")");
  }

  public override string unquoteTable(string tableName)
  {
    int num1 = tableName.LastIndexOf('[');
    int num2 = tableName.LastIndexOf(']');
    return num2 > num1 && num1 >= 0 ? tableName.Substring(num1 + 1, num2 - num1 - 1) : tableName;
  }

  public override PXDbType GetDBType(SQLExpression expression)
  {
    switch (expression.Oper())
    {
      case SQLExpression.Operation.PLUS:
      case SQLExpression.Operation.CONCAT:
      case SQLExpression.Operation.MUL:
      case SQLExpression.Operation.DIV:
      case SQLExpression.Operation.MOD:
      case SQLExpression.Operation.MINUS:
      case SQLExpression.Operation.SEQ:
      case SQLExpression.Operation.BIT_AND:
      case SQLExpression.Operation.BIT_OR:
      case SQLExpression.Operation.BIT_NOT:
        return SQLExpression.GetMaxByPrecedence(((IEnumerable<PXDbType?>) new PXDbType?[2]
        {
          expression.LExpr()?.GetDBType(),
          expression.RExpr()?.GetDBType()
        }).Where<PXDbType?>((Func<PXDbType?, bool>) (t =>
        {
          PXDbType? nullable = t;
          PXDbType pxDbType = PXDbType.Unspecified;
          return !(nullable.GetValueOrDefault() == pxDbType & nullable.HasValue);
        })).ToArray<PXDbType?>());
      case SQLExpression.Operation.UMINUS:
      case SQLExpression.Operation.MAX:
      case SQLExpression.Operation.MIN:
      case SQLExpression.Operation.AVG:
      case SQLExpression.Operation.SUM:
      case SQLExpression.Operation.COUNT:
      case SQLExpression.Operation.COUNT_DISTINCT:
      case SQLExpression.Operation.ISNULL_FUNC:
      case SQLExpression.Operation.ABS:
      case SQLExpression.Operation.RTRIM:
      case SQLExpression.Operation.LTRIM:
      case SQLExpression.Operation.CEILING:
      case SQLExpression.Operation.FLOOR:
      case SQLExpression.Operation.REVERSE:
      case SQLExpression.Operation.LOWER:
      case SQLExpression.Operation.UPPER:
      case SQLExpression.Operation.TRIM:
      case SQLExpression.Operation.AGG_CONCAT:
        return expression.RExpr().GetDBTypeOrDefault();
      case SQLExpression.Operation.EQ:
      case SQLExpression.Operation.NE:
      case SQLExpression.Operation.GE:
      case SQLExpression.Operation.GT:
      case SQLExpression.Operation.LT:
      case SQLExpression.Operation.LE:
      case SQLExpression.Operation.AND:
      case SQLExpression.Operation.OR:
      case SQLExpression.Operation.NOT:
      case SQLExpression.Operation.IN:
      case SQLExpression.Operation.NOT_IN:
      case SQLExpression.Operation.LIKE:
      case SQLExpression.Operation.NOT_LIKE:
      case SQLExpression.Operation.IS_NOT_NULL:
      case SQLExpression.Operation.IS_NULL:
      case SQLExpression.Operation.EXISTS:
      case SQLExpression.Operation.NOT_EXISTS:
      case SQLExpression.Operation.BETWEEN:
      case SQLExpression.Operation.NOT_BETWEEN:
      case SQLExpression.Operation.CONTAINS:
      case SQLExpression.Operation.FREETEXT:
      case SQLExpression.Operation.EQUAL_O_CI:
      case SQLExpression.Operation.EQUAL_O_CS:
      case SQLExpression.Operation.EQUAL_I_CS:
      case SQLExpression.Operation.EQUAL_I_CI:
      case SQLExpression.Operation.EQUAL_CC_CS:
      case SQLExpression.Operation.EQUAL_CC_CI:
        return PXDbType.Bit;
      case SQLExpression.Operation.ASCII:
      case SQLExpression.Operation.CONVERT_BIN_TO_INT:
      case SQLExpression.Operation.CAST_AS_INTEGER:
      case SQLExpression.Operation.COMPARE_O_CI:
      case SQLExpression.Operation.COMPARE_O_CS:
      case SQLExpression.Operation.COMPARE_I_CS:
      case SQLExpression.Operation.COMPARE_I_CI:
      case SQLExpression.Operation.COMPARE_CC_CS:
      case SQLExpression.Operation.COMPARE_CC_CI:
      case SQLExpression.Operation.COMPARE_CULTUREINFO:
        return PXDbType.Int;
      case SQLExpression.Operation.SUBSTR:
      case SQLExpression.Operation.REPLACE:
      case SQLExpression.Operation.NULL_IF:
      case SQLExpression.Operation.LEFT:
      case SQLExpression.Operation.RIGHT:
      case SQLExpression.Operation.REPEAT:
        return expression.LExpr().GetDBTypeOrDefault();
      case SQLExpression.Operation.CAST_AS_DECIMAL:
        return PXDbType.Decimal;
      case SQLExpression.Operation.BINARY_LEN:
      case SQLExpression.Operation.LEN:
      case SQLExpression.Operation.IS_NUMERIC:
        switch (expression.RExpr().GetDBTypeOrDefault())
        {
          case PXDbType.NVarChar:
          case PXDbType.VarBinary:
          case PXDbType.VarChar:
            return PXDbType.BigInt;
          case PXDbType.Unspecified:
            return PXDbType.Unspecified;
          default:
            return PXDbType.Int;
        }
      case SQLExpression.Operation.ROUND:
        switch (expression.LExpr().GetDBTypeOrDefault())
        {
          case PXDbType.BigInt:
            return PXDbType.BigInt;
          case PXDbType.Decimal:
            return PXDbType.Decimal;
          case PXDbType.Float:
          case PXDbType.Real:
            return PXDbType.Float;
          case PXDbType.Money:
          case PXDbType.SmallMoney:
            return PXDbType.Money;
          case PXDbType.SmallInt:
          case PXDbType.TinyInt:
            return PXDbType.Int;
          default:
            return PXDbType.Unspecified;
        }
      case SQLExpression.Operation.DATE_NOW:
      case SQLExpression.Operation.DATE_UTC_NOW:
      case SQLExpression.Operation.TODAY:
      case SQLExpression.Operation.TODAY_UTC:
      case SQLExpression.Operation.GET_TIME:
        return PXDbType.DateTime;
      case SQLExpression.Operation.CHARINDEX:
        switch (expression.LExpr().GetDBTypeOrDefault())
        {
          case PXDbType.NVarChar:
          case PXDbType.VarBinary:
          case PXDbType.VarChar:
            return PXDbType.BigInt;
          case PXDbType.Unspecified:
            return PXDbType.Unspecified;
          default:
            return PXDbType.Int;
        }
      case SQLExpression.Operation.POWER:
        switch (expression.LExpr().GetDBTypeOrDefault())
        {
          case PXDbType.BigInt:
            return PXDbType.BigInt;
          case PXDbType.Bit:
          case PXDbType.Char:
          case PXDbType.NChar:
          case PXDbType.NVarChar:
          case PXDbType.VarChar:
            return PXDbType.Float;
          case PXDbType.Decimal:
            return PXDbType.Decimal;
          case PXDbType.Float:
          case PXDbType.Real:
            return PXDbType.Float;
          case PXDbType.Int:
          case PXDbType.SmallInt:
          case PXDbType.TinyInt:
            return PXDbType.Int;
          case PXDbType.Money:
          case PXDbType.SmallMoney:
            return PXDbType.Money;
          default:
            return PXDbType.Unspecified;
        }
      case SQLExpression.Operation.CAST_AS_VARBINARY:
        return PXDbType.VarBinary;
      case SQLExpression.Operation.CHAR:
        return PXDbType.Char;
      case SQLExpression.Operation.TO_BASE64:
        return PXDbType.NVarChar;
      default:
        return PXDbType.Unspecified;
    }
  }
}
