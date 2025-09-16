// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.EvaluateVisitor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

internal class EvaluateVisitor : ISQLExpressionVisitor<object>
{
  private readonly PXCache _cache;
  private readonly object _item;
  private readonly object[] _pars;

  public EvaluateVisitor(PXCache cache, object item, object[] pars)
  {
    this._cache = cache;
    this._item = item;
    this._pars = pars;
  }

  public bool UnknownValue { get; private set; }

  public object Visit(SQLExpression exp)
  {
    this.Reset();
    switch (exp.Oper())
    {
      case SQLExpression.Operation.NONE:
      case SQLExpression.Operation.NULL:
      case SQLExpression.Operation.EMPTY:
        return (object) null;
      case SQLExpression.Operation.SUB_QUERY:
        return !(exp is SubQuery exp1) ? (object) null : this.Visit(exp1);
      case SQLExpression.Operation.PLUS:
        return this.VisitPlus(exp);
      case SQLExpression.Operation.CONCAT:
        return this.VisitConcat(exp);
      case SQLExpression.Operation.MUL:
        return this.VisitMul(exp);
      case SQLExpression.Operation.DIV:
        return this.VisitDiv(exp);
      case SQLExpression.Operation.MOD:
        return this.VisitMod(exp);
      case SQLExpression.Operation.MINUS:
        return this.VisitMinus(exp);
      case SQLExpression.Operation.UMINUS:
        return this.VisitUnaryMinus(exp);
      case SQLExpression.Operation.EQ:
        return this.VisitEqual(exp);
      case SQLExpression.Operation.NE:
        return this.VisitNotEqual(exp);
      case SQLExpression.Operation.GE:
        return this.VisitGreaterEqual(exp);
      case SQLExpression.Operation.GT:
        return this.VisitGreater(exp);
      case SQLExpression.Operation.LT:
        return this.VisitLess(exp);
      case SQLExpression.Operation.LE:
        return this.VisitLessEqual(exp);
      case SQLExpression.Operation.AND:
        return this.VisitAnd(exp);
      case SQLExpression.Operation.OR:
        return this.VisitOr(exp);
      case SQLExpression.Operation.NOT:
        return this.VisitNot(exp);
      case SQLExpression.Operation.ASCII:
        return this.VisitAscii(exp);
      case SQLExpression.Operation.IN:
        return this.VisitIn(exp);
      case SQLExpression.Operation.NOT_IN:
        return this.VisitNotIn(exp);
      case SQLExpression.Operation.LIKE:
        return this.VisitLike(exp);
      case SQLExpression.Operation.NOT_LIKE:
        return this.VisitNotLike(exp);
      case SQLExpression.Operation.MAX:
      case SQLExpression.Operation.MIN:
      case SQLExpression.Operation.AVG:
      case SQLExpression.Operation.SUM:
      case SQLExpression.Operation.COUNT:
      case SQLExpression.Operation.COUNT_DISTINCT:
        return (object) null;
      case SQLExpression.Operation.SEQ:
        return this.VisitSeq(exp);
      case SQLExpression.Operation.IS_NOT_NULL:
        return this.VisitIsNotNull(exp);
      case SQLExpression.Operation.IS_NULL:
        return this.VisitIsNull(exp);
      case SQLExpression.Operation.ISNULL_FUNC:
        return this.VisitIsNullFunc(exp);
      case SQLExpression.Operation.SUBSTR:
        return this.VisitSubstring(exp);
      case SQLExpression.Operation.CONVERT_BIN_TO_INT:
        return this.VisitConvertBinToInt(exp);
      case SQLExpression.Operation.CAST_AS_DECIMAL:
        return this.VisitCastAsDecimal(exp);
      case SQLExpression.Operation.CAST_AS_INTEGER:
        return this.VisitCastAsInt(exp);
      case SQLExpression.Operation.BINARY_LEN:
        return this.VisitBinaryLen(exp);
      case SQLExpression.Operation.REPLACE:
        return this.VisitReplace(exp);
      case SQLExpression.Operation.NULL_IF:
        return this.VisitNullIf(exp);
      case SQLExpression.Operation.LEN:
        return this.VisitLen(exp);
      case SQLExpression.Operation.ABS:
        return this.VisitAbs(exp);
      case SQLExpression.Operation.RTRIM:
        return this.VisitRightTrim(exp);
      case SQLExpression.Operation.LTRIM:
        return this.VisitLeftTrim(exp);
      case SQLExpression.Operation.ROUND:
        return this.VisitRound(exp);
      case SQLExpression.Operation.DATE_DIFF:
        return !(exp is SQLDateDiff exp2) ? (object) null : this.Visit(exp2);
      case SQLExpression.Operation.DATE_PART:
        return !(exp is SQLDatePart exp3) ? (object) null : this.Visit(exp3);
      case SQLExpression.Operation.DATE_NOW:
        return (object) System.DateTime.Now;
      case SQLExpression.Operation.DATE_UTC_NOW:
        return (object) System.DateTime.UtcNow;
      case SQLExpression.Operation.BIT_AND:
        return this.VisitBitAnd(exp);
      case SQLExpression.Operation.BIT_OR:
        return this.VisitBitOr(exp);
      case SQLExpression.Operation.EXISTS:
        return this.VisitExists(exp);
      case SQLExpression.Operation.NOT_EXISTS:
        return this.VisitNotExists(exp);
      case SQLExpression.Operation.BETWEEN:
        return this.VisitBetween(exp);
      case SQLExpression.Operation.NOT_BETWEEN:
        return this.VisitNotBetween(exp);
      case SQLExpression.Operation.CONTAINS:
      case SQLExpression.Operation.FREETEXT:
        return !(exp is SQLFullTextSearch exp4) ? (object) null : this.Visit(exp4);
      case SQLExpression.Operation.CEILING:
        return this.VisitCeiling(exp);
      case SQLExpression.Operation.FLOOR:
        return this.VisitFloor(exp);
      case SQLExpression.Operation.CHARINDEX:
        return this.VisitCharIndex(exp);
      case SQLExpression.Operation.REVERSE:
        return this.VisitReverse(exp);
      case SQLExpression.Operation.LOWER:
        return this.VisitLower(exp);
      case SQLExpression.Operation.UPPER:
        return this.VisitUpper(exp);
      case SQLExpression.Operation.LEFT:
        return this.VisitLeft(exp);
      case SQLExpression.Operation.RIGHT:
        return this.VisitRight(exp);
      case SQLExpression.Operation.REPEAT:
        return this.VisitRepeat(exp);
      case SQLExpression.Operation.POWER:
        return this.VisitPower(exp);
      case SQLExpression.Operation.TODAY:
        return (object) System.DateTime.Today;
      case SQLExpression.Operation.TODAY_UTC:
        return (object) System.DateTime.UtcNow.Date;
      case SQLExpression.Operation.TRIM:
        return this.VisitTrim(exp);
      case SQLExpression.Operation.BIT_NOT:
        return this.VisitBitNot(exp);
      case SQLExpression.Operation.GET_TIME:
        return this.VisitGetTime(exp);
      case SQLExpression.Operation.CAST_AS_VARBINARY:
        return this.VisitCastAsVarBinary(exp);
      case SQLExpression.Operation.EQUAL_O_CI:
      case SQLExpression.Operation.EQUAL_O_CS:
      case SQLExpression.Operation.EQUAL_I_CS:
      case SQLExpression.Operation.EQUAL_I_CI:
      case SQLExpression.Operation.EQUAL_CC_CS:
      case SQLExpression.Operation.EQUAL_CC_CI:
        return this.VisitStrEqual(exp);
      case SQLExpression.Operation.COMPARE_O_CI:
      case SQLExpression.Operation.COMPARE_O_CS:
      case SQLExpression.Operation.COMPARE_I_CS:
      case SQLExpression.Operation.COMPARE_I_CI:
      case SQLExpression.Operation.COMPARE_CC_CS:
      case SQLExpression.Operation.COMPARE_CC_CI:
        return this.VisitCompare(exp);
      case SQLExpression.Operation.COMPARE_CULTUREINFO:
        return this.VisitCompareWithCulture(exp);
      case SQLExpression.Operation.CHAR:
        return this.VisitChar(exp);
      case SQLExpression.Operation.TO_BASE64:
        return this.VisitToBase64(exp);
      default:
        throw new NotSupportedException();
    }
  }

  private object GetLeft(SQLExpression exp)
  {
    return exp.LExpr()?.Accept<object>((ISQLExpressionVisitor<object>) this);
  }

  private object GetRight(SQLExpression exp)
  {
    return exp.RExpr()?.Accept<object>((ISQLExpressionVisitor<object>) this);
  }

  private object VisitPlus(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    if (right == null || this.UnknownValue)
      return (object) null;
    switch (System.Type.GetTypeCode(left.GetType()))
    {
      case TypeCode.Int16:
        return (object) ((int) Convert.ToInt16(left) + (int) Convert.ToInt16(right));
      case TypeCode.Int32:
        return (object) (Convert.ToInt32(left) + Convert.ToInt32(right));
      case TypeCode.Int64:
        return (object) (Convert.ToInt64(left) + Convert.ToInt64(right));
      case TypeCode.Single:
        return (object) (float) ((double) Convert.ToSingle(left) + (double) Convert.ToSingle(right));
      case TypeCode.Double:
        return (object) (Convert.ToDouble(left) + Convert.ToDouble(right));
      case TypeCode.Decimal:
        return (object) (Convert.ToDecimal(left) + Convert.ToDecimal(right));
      case TypeCode.DateTime:
        return right is TimeSpan timeSpan ? (object) Convert.ToDateTime(left).Add(timeSpan) : (object) Convert.ToDateTime(left).AddDays((double) Convert.ToInt32(right));
      case TypeCode.String:
        return (object) (Convert.ToString(left) + Convert.ToString(right));
      default:
        return (object) null;
    }
  }

  private object VisitConcat(SQLExpression exp)
  {
    List<SQLExpression> source;
    if (exp.Oper() == SQLExpression.Operation.SEQ)
    {
      source = exp.DecomposeSequence();
    }
    else
    {
      source = new List<SQLExpression>();
      SQLExpression seq1 = exp.LExpr();
      if (seq1 != null)
        source.AddRange((IEnumerable<SQLExpression>) seq1.DecomposeSequence());
      SQLExpression seq2 = exp.RExpr();
      if (seq2 != null)
        source.AddRange((IEnumerable<SQLExpression>) seq2.DecomposeSequence());
    }
    string str = string.Join(string.Empty, source.Select<SQLExpression, string>((Func<SQLExpression, string>) (e => e?.Accept<object>((ISQLExpressionVisitor<object>) this)?.ToString())).Where<string>((Func<string, bool>) (e => e != null)));
    return !this.UnknownValue ? (object) str : (object) null;
  }

  private object VisitMul(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    if (right == null || this.UnknownValue)
      return (object) null;
    switch (System.Type.GetTypeCode(left.GetType()))
    {
      case TypeCode.Int16:
        return (object) ((int) Convert.ToInt16(left) * (int) Convert.ToInt16(right));
      case TypeCode.Int32:
        return (object) (Convert.ToInt32(left) * Convert.ToInt32(right));
      case TypeCode.Int64:
        return (object) (Convert.ToInt64(left) * Convert.ToInt64(right));
      case TypeCode.Single:
        return (object) (float) ((double) Convert.ToSingle(left) * (double) Convert.ToSingle(right));
      case TypeCode.Double:
        return (object) (Convert.ToDouble(left) * Convert.ToDouble(right));
      case TypeCode.Decimal:
        return (object) (Convert.ToDecimal(left) * Convert.ToDecimal(right));
      default:
        return (object) null;
    }
  }

  private object VisitDiv(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    if (right == null || this.UnknownValue)
      return (object) null;
    switch (System.Type.GetTypeCode(left.GetType()))
    {
      case TypeCode.Int16:
        return (object) ((int) Convert.ToInt16(left) / (int) Convert.ToInt16(right));
      case TypeCode.Int32:
        return (object) (Convert.ToInt32(left) / Convert.ToInt32(right));
      case TypeCode.Int64:
        return (object) (Convert.ToInt64(left) / Convert.ToInt64(right));
      case TypeCode.Single:
        return (object) (float) ((double) Convert.ToSingle(left) / (double) Convert.ToSingle(right));
      case TypeCode.Double:
        return (object) (Convert.ToDouble(left) / Convert.ToDouble(right));
      case TypeCode.Decimal:
        return (object) (Convert.ToDecimal(left) / Convert.ToDecimal(right));
      default:
        return (object) null;
    }
  }

  private object VisitMod(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    if (right == null || this.UnknownValue)
      return (object) null;
    switch (System.Type.GetTypeCode(left.GetType()))
    {
      case TypeCode.Int16:
        return (object) ((int) Convert.ToInt16(left) % (int) Convert.ToInt16(right));
      case TypeCode.Int32:
        return (object) (Convert.ToInt32(left) % Convert.ToInt32(right));
      case TypeCode.Int64:
        return (object) (Convert.ToInt64(left) % Convert.ToInt64(right));
      case TypeCode.Single:
        return (object) (float) ((double) Convert.ToSingle(left) % (double) Convert.ToSingle(right));
      case TypeCode.Double:
        return (object) (Convert.ToDouble(left) % Convert.ToDouble(right));
      case TypeCode.Decimal:
        return (object) (Convert.ToDecimal(left) % Convert.ToDecimal(right));
      default:
        return (object) null;
    }
  }

  private object VisitMinus(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    if (right == null || this.UnknownValue)
      return (object) null;
    switch (System.Type.GetTypeCode(left.GetType()))
    {
      case TypeCode.Int16:
        return (object) ((int) Convert.ToInt16(left) - (int) Convert.ToInt16(right));
      case TypeCode.Int32:
        return (object) (Convert.ToInt32(left) - Convert.ToInt32(right));
      case TypeCode.Int64:
        return (object) (Convert.ToInt64(left) - Convert.ToInt64(right));
      case TypeCode.Single:
        return (object) (float) ((double) Convert.ToSingle(left) - (double) Convert.ToSingle(right));
      case TypeCode.Double:
        return (object) (Convert.ToDouble(left) - Convert.ToDouble(right));
      case TypeCode.Decimal:
        return (object) (Convert.ToDecimal(left) - Convert.ToDecimal(right));
      case TypeCode.DateTime:
        return System.Type.GetTypeCode(right.GetType()) == TypeCode.DateTime ? (object) (int) Convert.ToDateTime(left).Subtract(Convert.ToDateTime(right)).TotalMinutes : (object) (right is TimeSpan timeSpan ? Convert.ToDateTime(left).Add(timeSpan) : Convert.ToDateTime(left).AddDays((double) -Convert.ToInt32(right)));
      default:
        return (object) null;
    }
  }

  private object VisitUnaryMinus(SQLExpression exp)
  {
    object right = this.GetRight(exp);
    if (right == null || this.UnknownValue)
      return (object) null;
    switch (System.Type.GetTypeCode(right.GetType()))
    {
      case TypeCode.Int16:
        return (object) (int) -Convert.ToInt16(right);
      case TypeCode.Int32:
        return (object) -Convert.ToInt32(right);
      case TypeCode.Int64:
        return (object) -Convert.ToInt64(right);
      case TypeCode.Single:
        return (object) (float) -(double) Convert.ToSingle(right);
      case TypeCode.Double:
        return (object) -Convert.ToDouble(right);
      case TypeCode.Decimal:
        return (object) -Convert.ToDecimal(right);
      default:
        return (object) null;
    }
  }

  private object VisitEqual(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    return this.UnknownValue ? (object) null : (object) object.Equals(left, right);
  }

  private object VisitNotEqual(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    return this.UnknownValue ? (object) null : (object) !object.Equals(left, right);
  }

  private object VisitGreaterEqual(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    return right == null || this.UnknownValue ? (object) null : (object) (((IComparable) left).CompareTo(right) >= 0);
  }

  private object VisitGreater(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    return right == null || this.UnknownValue ? (object) null : (object) (((IComparable) left).CompareTo(right) > 0);
  }

  private object VisitLess(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    return right == null || this.UnknownValue ? (object) null : (object) (((IComparable) left).CompareTo(right) < 0);
  }

  private object VisitLessEqual(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    return right == null || this.UnknownValue ? (object) null : (object) (((IComparable) left).CompareTo(right) <= 0);
  }

  private object VisitAnd(SQLExpression exp)
  {
    bool flag1 = false;
    bool? left = this.GetLeft(exp) as bool?;
    bool flag2 = flag1 | this.UnknownValue;
    bool? right = this.GetRight(exp) as bool?;
    bool unknownValue = flag2 | this.UnknownValue;
    return (object) this.EvaluateAnd(left, right, unknownValue);
  }

  private bool? EvaluateAnd(bool? left, bool? right, bool unknownValue)
  {
    bool? nullable1 = left;
    bool flag1 = false;
    if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
    {
      bool? nullable2 = right;
      bool flag2 = false;
      if (!(nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue))
      {
        if (left.HasValue && right.HasValue)
          return new bool?(left.Value && right.Value);
        this.UnknownValue = unknownValue;
        return new bool?();
      }
    }
    return new bool?(false);
  }

  private object VisitOr(SQLExpression exp)
  {
    bool flag1 = false;
    bool? left = this.GetLeft(exp) as bool?;
    bool flag2 = flag1 | this.UnknownValue;
    bool? right = this.GetRight(exp) as bool?;
    bool unknownValue = flag2 | this.UnknownValue;
    return (object) this.EvaluateOr(left, right, unknownValue);
  }

  private bool? EvaluateOr(bool? left, bool? right, bool unknownValue)
  {
    bool? nullable1 = left;
    bool flag1 = true;
    if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
    {
      bool? nullable2 = right;
      bool flag2 = true;
      if (!(nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue))
      {
        if (left.HasValue && right.HasValue)
          return new bool?(left.Value || right.Value);
        this.UnknownValue = unknownValue;
        return new bool?();
      }
    }
    return new bool?(true);
  }

  private object VisitNot(SQLExpression exp)
  {
    bool? right = this.GetRight(exp) as bool?;
    return (object) (right.HasValue ? new bool?(!right.GetValueOrDefault()) : new bool?());
  }

  private object VisitAscii(SQLExpression exp)
  {
    string right = this.GetRight(exp) as string;
    return string.IsNullOrEmpty(right) || this.UnknownValue ? (object) null : (object) ((IEnumerable<byte>) Encoding.ASCII.GetBytes(right.Substring(0, 1))).First<byte>();
  }

  private object VisitChar(SQLExpression exp)
  {
    int? right = this.GetRight(exp) as int?;
    return !right.HasValue || this.UnknownValue ? (object) null : (object) (char) right.Value;
  }

  private object VisitIn(SQLExpression exp)
  {
    IComparable left = this.GetLeft(exp) as IComparable;
    if (this.UnknownValue)
      return (object) null;
    if (!(this.GetRight(exp) is Array right))
      return (object) null;
    foreach (object objB in right)
    {
      if (object.Equals((object) left, objB))
        return (object) true;
    }
    return (object) this.UnknownValue;
  }

  private object VisitNotIn(SQLExpression exp)
  {
    bool? nullable = this.VisitIn(exp) as bool?;
    return (object) (nullable.HasValue ? new bool?(!nullable.GetValueOrDefault()) : new bool?());
  }

  private object VisitStrEqual(SQLExpression exp)
  {
    object left1 = this.GetLeft(exp);
    if (this.UnknownValue)
      return (object) null;
    this.GetRight(exp);
    if (this.UnknownValue)
      return (object) null;
    object left2 = this.GetLeft(exp.RExpr());
    return this.UnknownValue ? (object) null : (object) Like<BqlNone>.CheckLike(left1 as string, left2 as string);
  }

  private object VisitCompare(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    return this.UnknownValue ? (object) null : (object) string.Compare((string) left, (string) right);
  }

  private object VisitCompareWithCulture(SQLExpression exp)
  {
    object left1 = this.GetLeft(exp);
    if (this.UnknownValue)
      return (object) null;
    object left2 = this.GetLeft(exp.RExpr());
    object right = this.GetRight(exp.RExpr().RExpr());
    object left3 = this.GetLeft(exp.RExpr().RExpr());
    return this.UnknownValue ? (object) null : (object) string.Compare((string) left1, (string) left2, (bool) left3, CultureInfo.GetCultureInfo((string) right));
  }

  private object VisitLike(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    return this.UnknownValue ? (object) null : (object) Like<BqlNone>.CheckLike(left as string, right as string);
  }

  private object VisitNotLike(SQLExpression exp)
  {
    bool? nullable = this.VisitLike(exp) as bool?;
    return (object) (nullable.HasValue ? new bool?(!nullable.GetValueOrDefault()) : new bool?());
  }

  private object VisitSeq(SQLExpression exp)
  {
    bool unknownValue = false;
    object[] array = exp.DecomposeSequence().Select<SQLExpression, object>((Func<SQLExpression, object>) (e =>
    {
      object obj = e.Accept<object>((ISQLExpressionVisitor<object>) this);
      unknownValue |= this.UnknownValue;
      return obj;
    })).ToArray<object>();
    this.UnknownValue = unknownValue;
    return (object) array;
  }

  private object VisitIsNotNull(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    return this.UnknownValue ? (object) null : (object) (left != null);
  }

  private object VisitIsNull(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    return this.UnknownValue ? (object) null : (object) (left == null);
  }

  private object VisitIsNullFunc(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    return this.UnknownValue ? (object) null : left ?? this.GetRight(exp);
  }

  private object VisitSubstring(SQLExpression exp)
  {
    if (!(this.GetLeft(exp) is string left1) || this.UnknownValue)
      return (object) null;
    object left2 = this.GetLeft(exp.RExpr());
    if (left2 == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp.RExpr());
    if (right != null)
    {
      if (!this.UnknownValue)
      {
        int int32_1;
        int int32_2;
        try
        {
          int32_1 = Convert.ToInt32(left2);
          int32_2 = Convert.ToInt32(right);
        }
        catch (Exception ex)
        {
          return (object) null;
        }
        return (object) left1.Substring(int32_1 - 1, int32_2);
      }
    }
    return (object) null;
  }

  private object VisitConvertBinToInt(SQLExpression exp)
  {
    if (!(this.GetLeft(exp) is byte[] left1) || this.UnknownValue)
      return (object) null;
    object left2 = this.GetLeft(exp.RExpr());
    if (left2 == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp.RExpr());
    if (right != null)
    {
      if (!this.UnknownValue)
      {
        int int32_1;
        int int32_2;
        try
        {
          int32_1 = Convert.ToInt32(left2);
          int32_2 = Convert.ToInt32(right);
        }
        catch (Exception ex)
        {
          return (object) null;
        }
        byte[] destinationArray = new byte[int32_2];
        if (left1.Length < int32_2 + int32_1)
          return (object) null;
        Array.Copy((Array) left1, int32_1, (Array) destinationArray, 0, int32_2);
        if (BitConverter.IsLittleEndian)
          Array.Reverse((Array) destinationArray);
        return (object) BitConverter.ToInt32(destinationArray, 0);
      }
    }
    return (object) null;
  }

  private object VisitCastAsDecimal(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left != null)
    {
      if (!this.UnknownValue)
      {
        Decimal num;
        try
        {
          num = Convert.ToDecimal(left);
        }
        catch (Exception ex)
        {
          return (object) null;
        }
        return (object) num;
      }
    }
    return (object) null;
  }

  private object VisitCastAsInt(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left != null)
    {
      if (!this.UnknownValue)
      {
        int int32;
        try
        {
          int32 = Convert.ToInt32(left);
        }
        catch (Exception ex)
        {
          return (object) null;
        }
        return (object) int32;
      }
    }
    return (object) null;
  }

  private object VisitCastAsVarBinary(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    return System.Type.GetTypeCode(left.GetType()) == TypeCode.String ? (object) Encoding.Unicode.GetBytes(Convert.ToString(left)) : (object) null;
  }

  private object VisitBinaryLen(SQLExpression exp)
  {
    return (object) (this.GetRight(exp) is byte[] right ? new int?(right.Length) : new int?());
  }

  private object VisitReplace(SQLExpression exp)
  {
    object left1 = this.GetLeft(exp);
    if (left1 == null || this.UnknownValue || exp.RExpr() == null)
      return (object) null;
    object left2 = this.GetLeft(exp.RExpr());
    if (left2 == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp.RExpr());
    if (right == null || this.UnknownValue)
      return (object) null;
    int typeCode1 = (int) System.Type.GetTypeCode(left1.GetType());
    TypeCode typeCode2 = System.Type.GetTypeCode(left2.GetType());
    TypeCode typeCode3 = System.Type.GetTypeCode(right.GetType());
    return typeCode1 != 18 || typeCode2 != TypeCode.String || typeCode3 != TypeCode.String ? (object) null : (object) Convert.ToString(left1).Replace(Convert.ToString(left2), Convert.ToString(right));
  }

  private object VisitNullIf(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    if (this.UnknownValue)
      return (object) null;
    return !object.Equals(left, right) ? left : (object) null;
  }

  private object VisitLen(SQLExpression exp)
  {
    return (object) (this.GetRight(exp) is string right ? new int?(right.Length) : new int?());
  }

  private object VisitAbs(SQLExpression exp)
  {
    object right = this.GetRight(exp);
    if (right == null || this.UnknownValue)
      return (object) null;
    switch (System.Type.GetTypeCode(right.GetType()))
    {
      case TypeCode.Int16:
        return (object) System.Math.Abs(Convert.ToInt16(right));
      case TypeCode.Int32:
        return (object) System.Math.Abs(Convert.ToInt32(right));
      case TypeCode.Int64:
        return (object) System.Math.Abs(Convert.ToInt64(right));
      case TypeCode.Single:
        return (object) System.Math.Abs(Convert.ToSingle(right));
      case TypeCode.Double:
        return (object) System.Math.Abs(Convert.ToDouble(right));
      case TypeCode.Decimal:
        return (object) System.Math.Abs(Convert.ToDecimal(right));
      default:
        return (object) null;
    }
  }

  private object VisitRightTrim(SQLExpression exp)
  {
    return !(this.GetRight(exp) is string right) ? (object) null : (object) right.TrimEnd();
  }

  private object VisitLeftTrim(SQLExpression exp)
  {
    return !(this.GetRight(exp) is string right) ? (object) null : (object) right.TrimStart();
  }

  private object VisitRound(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    if (right == null || this.UnknownValue)
      return (object) null;
    switch (System.Type.GetTypeCode(left.GetType()))
    {
      case TypeCode.Int16:
        return (object) Convert.ToInt16(left);
      case TypeCode.Int32:
        return (object) Convert.ToInt32(left);
      case TypeCode.Int64:
        return (object) Convert.ToInt64(left);
      case TypeCode.Single:
        return (object) System.Math.Round((double) Convert.ToSingle(left), (int) Convert.ToInt16(right), MidpointRounding.AwayFromZero);
      case TypeCode.Double:
        return (object) System.Math.Round(Convert.ToDouble(left), (int) Convert.ToInt16(right), MidpointRounding.AwayFromZero);
      case TypeCode.Decimal:
        return (object) System.Math.Round(Convert.ToDecimal(left), (int) Convert.ToInt16(right), MidpointRounding.AwayFromZero);
      default:
        return (object) null;
    }
  }

  private object VisitBitAnd(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    if (right != null)
    {
      if (!this.UnknownValue)
      {
        int int32_1;
        int int32_2;
        try
        {
          int32_1 = Convert.ToInt32(left);
          int32_2 = Convert.ToInt32(right);
        }
        catch (Exception ex)
        {
          return (object) null;
        }
        return (object) (int32_1 & int32_2);
      }
    }
    return (object) null;
  }

  private object VisitBitOr(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    if (right != null)
    {
      if (!this.UnknownValue)
      {
        int int32_1;
        int int32_2;
        try
        {
          int32_1 = Convert.ToInt32(left);
          int32_2 = Convert.ToInt32(right);
        }
        catch (Exception ex)
        {
          return (object) null;
        }
        return (object) (int32_1 | int32_2);
      }
    }
    return (object) null;
  }

  private object VisitExists(SQLExpression exp)
  {
    return !(exp.RExpr() is SubQuery exp1) ? (object) null : this.Visit(exp1);
  }

  private object VisitNotExists(SQLExpression exp)
  {
    bool? nullable = this.VisitExists(exp) as bool?;
    return (object) (nullable.HasValue ? new bool?(!nullable.GetValueOrDefault()) : new bool?());
  }

  private object VisitBetween(SQLExpression exp)
  {
    if (!(this.GetLeft(exp) is IComparable left1) || this.UnknownValue)
      return (object) null;
    SQLExpression exp1 = exp.RExpr();
    if (exp1 == null)
      return (object) null;
    object left2 = this.GetLeft(exp1);
    if (left2 != null && left1.CompareTo(left2) < 0)
      return (object) false;
    if (left2 == null && !this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp1);
    if (right != null && left1.CompareTo(left2) > 0)
      return (object) false;
    if (right == null && !this.UnknownValue)
      return (object) null;
    if (left2 != null && right != null)
      return (object) true;
    this.UnknownValue = true;
    return (object) null;
  }

  private object VisitNotBetween(SQLExpression exp)
  {
    bool? nullable = this.VisitBetween(exp) as bool?;
    return (object) (nullable.HasValue ? new bool?(!nullable.GetValueOrDefault()) : new bool?());
  }

  private object VisitCeiling(SQLExpression exp)
  {
    object right = this.GetRight(exp);
    if (right == null || this.UnknownValue)
      return (object) null;
    switch (System.Type.GetTypeCode(right.GetType()))
    {
      case TypeCode.Double:
        return (object) System.Math.Ceiling(Convert.ToDouble(right));
      case TypeCode.Decimal:
        return (object) System.Math.Ceiling(Convert.ToDecimal(right));
      default:
        return (object) null;
    }
  }

  private object VisitFloor(SQLExpression exp)
  {
    object right = this.GetRight(exp);
    if (right == null || this.UnknownValue)
      return (object) null;
    switch (System.Type.GetTypeCode(right.GetType()))
    {
      case TypeCode.Double:
        return (object) System.Math.Floor(Convert.ToDouble(right));
      case TypeCode.Decimal:
        return (object) System.Math.Floor(Convert.ToDecimal(right));
      default:
        return (object) null;
    }
  }

  private object VisitCharIndex(SQLExpression exp)
  {
    if (!(this.GetLeft(exp) is string left) || this.UnknownValue)
      return (object) null;
    return !(this.GetRight(exp) is string right) || this.UnknownValue ? (object) null : (object) (left.IndexOf(right, StringComparison.InvariantCulture) + 1);
  }

  private object VisitReverse(SQLExpression exp)
  {
    if (!(this.GetRight(exp) is string right) || this.UnknownValue)
      return (object) null;
    char[] charArray = right.ToCharArray();
    Array.Reverse((Array) charArray);
    return (object) new string(charArray);
  }

  private object VisitLower(SQLExpression exp)
  {
    return !(this.GetRight(exp) is string right) ? (object) null : (object) right.ToLower();
  }

  private object VisitUpper(SQLExpression exp)
  {
    return !(this.GetRight(exp) is string right) ? (object) null : (object) right.ToUpper();
  }

  private object VisitLeft(SQLExpression exp)
  {
    if (!(this.GetLeft(exp) is string left) || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    if (right != null)
    {
      if (!this.UnknownValue)
      {
        int int32;
        try
        {
          int32 = Convert.ToInt32(right);
        }
        catch (Exception ex)
        {
          return (object) null;
        }
        return (object) left.Substring(0, int32);
      }
    }
    return (object) null;
  }

  private object VisitRight(SQLExpression exp)
  {
    if (!(this.GetLeft(exp) is string left) || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    if (right != null)
    {
      if (!this.UnknownValue)
      {
        int int32;
        try
        {
          int32 = Convert.ToInt32(right);
        }
        catch (Exception ex)
        {
          return (object) null;
        }
        return (object) left.Substring(left.Length - int32);
      }
    }
    return (object) null;
  }

  private object VisitRepeat(SQLExpression exp)
  {
    if (!(this.GetLeft(exp) is string left) || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    if (right != null)
    {
      if (!this.UnknownValue)
      {
        int int32;
        try
        {
          int32 = Convert.ToInt32(right);
        }
        catch (Exception ex)
        {
          return (object) null;
        }
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < int32; ++index)
          stringBuilder.Append(left);
        return (object) stringBuilder.ToString();
      }
    }
    return (object) null;
  }

  private object VisitPower(SQLExpression exp)
  {
    object left = this.GetLeft(exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight(exp);
    if (right != null)
    {
      if (!this.UnknownValue)
      {
        double x;
        double y;
        try
        {
          x = Convert.ToDouble(left);
          y = Convert.ToDouble(right);
        }
        catch (Exception ex)
        {
          return (object) null;
        }
        return (object) System.Math.Pow(x, y);
      }
    }
    return (object) null;
  }

  private object VisitTrim(SQLExpression exp)
  {
    return !(this.GetRight(exp) is string right) ? (object) null : (object) right.Trim();
  }

  private object VisitBitNot(SQLExpression exp)
  {
    object right = this.GetRight(exp);
    if (right != null)
    {
      if (!this.UnknownValue)
      {
        int int32;
        try
        {
          int32 = Convert.ToInt32(right);
        }
        catch (Exception ex)
        {
          return (object) null;
        }
        return (object) ~int32;
      }
    }
    return (object) null;
  }

  private object VisitGetTime(SQLExpression exp)
  {
    System.DateTime? right = this.GetRight(exp) as System.DateTime?;
    return !right.HasValue || this.UnknownValue ? (object) null : (object) right.Value.TimeOfDay;
  }

  private object VisitToBase64(SQLExpression exp)
  {
    return !(this.GetRight(exp) is byte[] right) || this.UnknownValue ? (object) null : (object) Convert.ToBase64String(right);
  }

  public object Visit(Asterisk exp)
  {
    this.Reset();
    this.UnknownValue = true;
    return (object) null;
  }

  public object Visit(Column exp)
  {
    this.Reset();
    object obj = this._cache.GetValue(this._item, exp.Name);
    if (obj != null || !(this._cache.GetBqlField(exp.Name) == (System.Type) null))
      return obj;
    this.UnknownValue = true;
    return obj;
  }

  public object Visit(SQLConst exp)
  {
    this.Reset();
    return exp.GetValue();
  }

  public object Visit(Literal exp)
  {
    this.Reset();
    return exp.Number >= 0 && exp.Number < this._pars.Length ? this._pars[exp.Number] : (object) null;
  }

  public object Visit(NoteIdExpression exp)
  {
    this.Reset();
    return exp.MainExpr?.Accept<object>((ISQLExpressionVisitor<object>) this);
  }

  public object Visit(SQLConvert exp)
  {
    this.Reset();
    object right = this.GetRight((SQLExpression) exp);
    if (right == null && this.UnknownValue)
      return (object) null;
    return !(exp.TargetType == typeof (char)) || exp.TargetTypeLength <= 1 ? Convert.ChangeType(right, exp.TargetType) : right;
  }

  public object Visit(SQLSmartConvert exp) => this.Visit((SQLConvert) exp);

  public object Visit(SQLDateAdd exp)
  {
    this.Reset();
    object left = this.GetLeft((SQLExpression) exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight((SQLExpression) exp);
    if (right == null || this.UnknownValue)
      return (object) null;
    System.DateTime dateTime = Convert.ToDateTime(left);
    double months = Convert.ToDouble(right);
    string datePart = exp.DatePart;
    if (datePart != null)
    {
      switch (datePart.Length)
      {
        case 2:
          switch (datePart[1])
          {
            case 'd':
              if (datePart == "dd")
                break;
              goto label_25;
            case 'h':
              if (datePart == "hh")
                return (object) dateTime.AddHours(months);
              goto label_25;
            case 'i':
              if (datePart == "mi")
                return (object) dateTime.AddMinutes(months);
              goto label_25;
            case 'm':
              if (datePart == "mm")
                return (object) dateTime.AddMonths((int) months);
              goto label_25;
            case 'q':
              if (datePart == "qq")
                return (object) dateTime.AddMonths((int) months * 3);
              goto label_25;
            case 's':
              switch (datePart)
              {
                case "ss":
                  return (object) dateTime.AddSeconds(months);
                case "ms":
                  return (object) dateTime.AddMilliseconds(months);
                default:
                  goto label_25;
              }
            case 'w':
              switch (datePart)
              {
                case "dw":
                  break;
                case "ww":
                  return (object) dateTime.AddDays(months * 7.0);
                default:
                  goto label_25;
              }
              break;
            case 'y':
              if (datePart == "dy")
                break;
              goto label_25;
            default:
              goto label_25;
          }
          return (object) dateTime.AddDays(months);
        case 4:
          if (datePart == "yyyy")
            return (object) dateTime.AddYears((int) months);
          break;
      }
    }
label_25:
    return (object) null;
  }

  public object Visit(SQLDateByTimeZone exp)
  {
    this.Reset();
    return exp.Value?.Accept<object>((ISQLExpressionVisitor<object>) this);
  }

  public object Visit(SQLDateDiff exp)
  {
    this.Reset();
    object left = this.GetLeft((SQLExpression) exp);
    if (left == null || this.UnknownValue)
      return (object) null;
    object right = this.GetRight((SQLExpression) exp);
    if (right == null || this.UnknownValue)
      return (object) null;
    System.DateTime dateTime = new System.DateTime(1, 1, 1);
    TimeSpan timeSpan = Convert.ToDateTime(right) - Convert.ToDateTime(left);
    string str = exp.UOM();
    if (str != null)
    {
      switch (str.Length)
      {
        case 2:
          switch (str[1])
          {
            case 'd':
              if (str == "dd")
                return (object) Convert.ToInt32(timeSpan.TotalDays);
              break;
            case 'h':
              if (str == "hh")
                return (object) Convert.ToInt32(timeSpan.TotalHours);
              break;
            case 'i':
              if (str == "mi")
                return (object) Convert.ToInt32(timeSpan.TotalMinutes);
              break;
            case 'm':
              if (str == "mm")
                return (object) (((dateTime + timeSpan).Year - 1) * 12 + ((dateTime + timeSpan).Month - 1));
              break;
            case 'q':
              if (str == "qq")
                return (object) (((dateTime + timeSpan).Year - 1) * 4 + ((dateTime + timeSpan).Month - 1) / 3);
              break;
            case 's':
              switch (str)
              {
                case "ss":
                  return (object) Convert.ToInt32(timeSpan.TotalSeconds);
                case "ms":
                  return (object) Convert.ToInt32(timeSpan.TotalMilliseconds);
              }
              break;
            case 'w':
              if (str == "ww")
                return (object) Convert.ToInt32(timeSpan.TotalDays / 7.0);
              break;
          }
          break;
        case 4:
          if (str == "yyyy")
            return (object) ((dateTime + timeSpan).Year - 1);
          break;
      }
    }
    return (object) null;
  }

  public object Visit(SQLDatePart exp)
  {
    this.Reset();
    object obj = exp.Date?.Accept<object>((ISQLExpressionVisitor<object>) this);
    if (obj == null || this.UnknownValue)
      return (object) null;
    System.DateTime dateTime = Convert.ToDateTime(obj);
    string str = exp.UOM();
    if (str != null)
    {
      switch (str.Length)
      {
        case 2:
          switch (str[1])
          {
            case 'd':
              if (str == "dd")
                return (object) Convert.ToInt32(dateTime.Day);
              break;
            case 'h':
              if (str == "hh")
                return (object) Convert.ToInt32(dateTime.Hour);
              break;
            case 'i':
              if (str == "mi")
                return (object) Convert.ToInt32(dateTime.Minute);
              break;
            case 'm':
              if (str == "mm")
                return (object) Convert.ToInt32(dateTime.Month);
              break;
            case 'q':
              if (str == "qq")
                return (object) Convert.ToInt32(dateTime.Month / 3 + 1);
              break;
            case 's':
              switch (str)
              {
                case "ss":
                  return (object) Convert.ToInt32(dateTime.Second);
                case "ms":
                  return (object) Convert.ToInt32(dateTime.Millisecond);
              }
              break;
            case 'w':
              switch (str)
              {
                case "dw":
                  return (object) Convert.ToInt32((object) dateTime.DayOfWeek);
                case "ww":
                  return (object) Convert.ToInt32(dateTime.Day / 7 + 1);
              }
              break;
            case 'y':
              if (str == "dy")
                return (object) Convert.ToInt32(dateTime.DayOfYear);
              break;
          }
          break;
        case 4:
          if (str == "yyyy")
            return (object) Convert.ToInt32(dateTime.Year);
          break;
      }
    }
    return (object) null;
  }

  public object Visit(SQLFullTextSearch exp)
  {
    this.Reset();
    if (!(exp.SearchField()?.Accept<object>((ISQLExpressionVisitor<object>) this) is string str1) || this.UnknownValue)
      return (object) null;
    return !(exp.SearchValue?.Accept<object>((ISQLExpressionVisitor<object>) this) is string str2) || this.UnknownValue ? (object) null : (object) str1.Contains(str2);
  }

  public object Visit(SQLRank exp)
  {
    this.Reset();
    this.UnknownValue = true;
    return (object) null;
  }

  public object Visit(SQLScalarFunction exp)
  {
    this.Reset();
    this.UnknownValue = true;
    return (object) null;
  }

  public object Visit(SQLSwitch exp)
  {
    this.Reset();
    foreach (Tuple<SQLExpression, SQLExpression> tuple in exp.GetCases())
    {
      bool? nullable1 = tuple.Item1.Accept<object>((ISQLExpressionVisitor<object>) this) as bool?;
      if (this.UnknownValue)
        return (object) null;
      bool? nullable2 = nullable1;
      bool flag = true;
      if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
        return tuple.Item2.Accept<object>((ISQLExpressionVisitor<object>) this);
    }
    return exp.GetDefault()?.Accept<object>((ISQLExpressionVisitor<object>) this);
  }

  public object Visit(SubQuery exp)
  {
    this.Reset();
    bool? nullable = exp.Query()?.Meet(this._cache, this._item, this._pars);
    if (!nullable.HasValue || !nullable.Value)
      return (object) null;
    List<SQLExpression> selection = exp.Query().GetSelection();
    if (selection == null)
      return (object) null;
    return selection.FirstOrDefault<SQLExpression>()?.Accept<object>((ISQLExpressionVisitor<object>) this);
  }

  public object Visit(SQLGroupConcat exp)
  {
    this.Reset();
    this.UnknownValue = true;
    return (object) null;
  }

  public object Visit(Md5Hash exp)
  {
    this.Reset();
    this.UnknownValue = true;
    return (object) null;
  }

  public object Visit(SQLAggConcat exp)
  {
    this.Reset();
    this.UnknownValue = true;
    return (object) null;
  }

  private void Reset() => this.UnknownValue = false;

  public virtual object Visit(SQLMultiOperation exp)
  {
    IEnumerable<SQLExpression> source = (IEnumerable<SQLExpression>) exp.Arguments;
    if (exp.Arguments.Count < 2)
      source = source.Prepend<SQLExpression>((SQLExpression) null);
    if (exp.Arguments.Count < 1)
      source = source.Prepend<SQLExpression>((SQLExpression) null);
    if (exp.Oper() != SQLExpression.Operation.AND && exp.Oper() != SQLExpression.Operation.OR)
      throw new NotSupportedException($"{"SQLMultiOperation"} with {exp.Oper()} operation");
    Func<bool?, bool?, bool, bool?> evaluateFunc = exp.Oper() == SQLExpression.Operation.AND ? new Func<bool?, bool?, bool, bool?>(this.EvaluateAnd) : new Func<bool?, bool?, bool, bool?>(this.EvaluateOr);
    return (object) source.Select<SQLExpression, (bool?, bool)>((Func<SQLExpression, (bool?, bool)>) (e => (e?.Accept<object>((ISQLExpressionVisitor<object>) this) as bool?, this.UnknownValue))).Aggregate<(bool?, bool)>((Func<(bool?, bool), (bool?, bool), (bool?, bool)>) ((accum, cur) => (evaluateFunc(accum.Value, cur.Value, accum.UnknownValue | cur.UnknownValue), this.UnknownValue))).Item1;
  }
}
