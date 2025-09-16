// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDbType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>This enumeration specifies the SQL Server-specific data type
/// of a field property for use in
/// <tt>Microsoft.Data.SqlClient.SqlParameter.</tt></summary>
public enum PXDbType
{
  /// <summary><tt>System.Int64</tt>. A 64-bit signed integer.</summary>
  BigInt = 0,
  /// <summary><tt>System.Array</tt> of type <tt>System.Byte</tt>. A fixed-
  /// length stream of binary data ranging between 1 and 8000
  /// bytes.</summary>
  Binary = 1,
  /// <summary><tt>System.Boolean</tt>. An unsigned numeric value that can
  /// be 0, 1, or null.</summary>
  Bit = 2,
  /// <summary><tt>System.String</tt>. A fixed-length stream of non-Unicode
  /// characters ranging between 1 and 8000 characters.</summary>
  Char = 3,
  /// <summary><tt>System.DateTime</tt>. Date and time data ranging in value
  /// from January 1, 1753 to December 31, 9999 to an accuracy of 3.33
  /// milliseconds.</summary>
  DateTime = 4,
  /// <summary><tt>System.Decimal</tt>. A fixed precision and scale numeric
  /// value between -10<sup>38</sup>-1 and 10<sup>38</sup>-1.</summary>
  Decimal = 5,
  /// <summary><tt>System.Double</tt>. A floating point number within the
  /// range of -1.79E+308 through 1.79E+308.</summary>
  Float = 6,
  /// <summary><tt>System.Array</tt> of type <tt>System.Byte</tt>. A
  /// variable-length stream of binary data ranging from 0 to
  /// 2<sup>31</sup>-1 (or 2,147,483,647) bytes.</summary>
  Image = 7,
  /// <summary><tt>System.Int32</tt>. A 32-bit signed integer.</summary>
  Int = 8,
  /// <summary><tt>System.Decimal</tt>. A currency value ranging from
  /// -2<sup>63</sup> (or -922,337,203,685,477.5808) to 2<sup>63</sup>-1 (or
  /// +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a
  /// currency unit.</summary>
  Money = 9,
  /// <summary><tt>System.String</tt>. A fixed-length stream of Unicode
  /// characters ranging between 1 and 4000 characters.</summary>
  NChar = 10, // 0x0000000A
  /// <summary><tt>System.String</tt>. A variable-length stream of Unicode
  /// data with a maximum length of 2<sup>30</sup>-1 (or 1,073,741,823)
  /// characters.</summary>
  NText = 11, // 0x0000000B
  /// <summary><tt>System.String</tt>. A variable-length stream of Unicode
  /// characters ranging between 1 and 4000 characters. Implicit conversion
  /// fails if the string is greater than 4000 characters. Explicitly set
  /// the object when you're working with strings longer than 4000
  /// characters.</summary>
  NVarChar = 12, // 0x0000000C
  /// <summary><tt>System.Single</tt>. A floating point number within the
  /// range of -3.40E+38 through 3.40E+38.</summary>
  Real = 13, // 0x0000000D
  /// <summary><tt>System.Guid</tt>. A globally unique identifier
  /// (GUID).</summary>
  UniqueIdentifier = 14, // 0x0000000E
  /// <summary><tt>System.Int16</tt>. A 16-bit signed integer.</summary>
  SmallInt = 16, // 0x00000010
  /// <summary><tt>System.Decimal</tt>. A currency value ranging from
  /// -214,748.3648 to +214,748.3647 with an accuracy to a ten-thousandth of
  /// a currency unit.</summary>
  SmallMoney = 17, // 0x00000011
  /// <summary><tt>System.String</tt>. A variable-length stream of non-
  /// Unicode data with a maximum length of 2<sup>31</sup>-1 (or
  /// 2,147,483,647) characters.</summary>
  Text = 18, // 0x00000012
  /// <summary><tt>System.Array</tt> of type <tt>System.Byte</tt>.
  /// Automatically generated binary numbers, which are guaranteed to be
  /// unique within a database. The timestamp is typically used as a
  /// mechanism for version- stamping table rows. The storage size is 8
  /// bytes.</summary>
  Timestamp = 19, // 0x00000013
  /// <summary><tt>System.Byte</tt>. An 8-bit unsigned integer.</summary>
  TinyInt = 20, // 0x00000014
  /// <summary><tt>System.Array</tt> of type <tt>System.Byte</tt>. A
  /// variable-length stream of binary data ranging between 1 and 8000
  /// bytes. Implicit conversion fails if the byte array is greater than
  /// 8000 bytes. Explicitly set the object when you are working with byte
  /// arrays larger than 8000 bytes.</summary>
  VarBinary = 21, // 0x00000015
  /// <summary><tt>System.String</tt>. A variable-length stream of non-
  /// Unicode characters ranging between 1 and 8000 characters.</summary>
  VarChar = 22, // 0x00000016
  /// <summary><tt>System.Object</tt>. A special data type that can contain
  /// numeric, string, binary, or date data, as well as the SQL Server
  /// values <tt>EMPTY</tt> and <tt>NULL</tt>, which is assumed if no other
  /// type is declared.</summary>
  Variant = 23, // 0x00000017
  /// <summary>An XML value. Obtain the XML as a string by using the
  /// <tt>Microsoft.Data.SqlClient.SqlDataReader.GetValue(System.Int32)</tt>
  /// method or the <tt>System.Data.SqlTypes.SqlXml.Value property</tt>, or
  /// as <tt>System.Xml.XmlReader</tt>—by calling the
  /// <tt>System.Data.SqlTypes.SqlXml.CreateReader()</tt> method.</summary>
  Xml = 25, // 0x00000019
  /// <summary>An SQL Server user-defined type (UDT).</summary>
  Udt = 29, // 0x0000001D
  /// <summary><tt>System.DateTime</tt>. Date and time data. Date value range is from January 1, 0001 AD through December 31, 9999
  /// with an accuracy of one second. <para> The data type for the underlying physical model is DateTime2(0) on Microsoft SQL Server and DateTime(0) on MySQL</para></summary>
  SmallDateTime = 33, // 0x00000021
  /// <summary>Unspecified value type that is implicitly converted by SQL
  /// Server into an appropriate database column type.</summary>
  Unspecified = 100, // 0x00000064
  /// <summary>A string constant containing a T-SQL statement being embedded
  /// into the final statement.</summary>
  DirectExpression = 200, // 0x000000C8
}
