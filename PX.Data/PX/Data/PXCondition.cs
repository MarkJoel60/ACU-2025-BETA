// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCondition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public enum PXCondition
{
  [PXEnumDescription("Equals", typeof (InfoMessages), Category = "COMMON")] EQ,
  [PXEnumDescription("Does Not Equal", typeof (InfoMessages), Category = "COMMON")] NE,
  [PXEnumDescription("Is Greater Than", typeof (InfoMessages), Category = "COMMON")] GT,
  [PXEnumDescription("Is Greater Than or Equal To", typeof (InfoMessages), Category = "COMMON")] GE,
  [PXEnumDescription("Is Less Than", typeof (InfoMessages), Category = "COMMON")] LT,
  [PXEnumDescription("Is Less Than or Equal To", typeof (InfoMessages), Category = "COMMON")] LE,
  [PXEnumDescription("Contains", typeof (InfoMessages), Category = "STRING")] LIKE,
  [PXEnumDescription("Starts With", typeof (InfoMessages), Category = "STRING")] RLIKE,
  [PXEnumDescription("Ends With", typeof (InfoMessages), Category = "STRING")] LLIKE,
  [PXEnumDescription("Does Not Contain", typeof (InfoMessages), Category = "STRING")] NOTLIKE,
  [PXEnumDescription("Is Between", typeof (InfoMessages), Category = "COMMON")] BETWEEN,
  [PXEnumDescription("Is Empty", typeof (InfoMessages), Category = "COMMON")] ISNULL,
  [PXEnumDescription("Is Not Empty", typeof (InfoMessages), Category = "COMMON")] ISNOTNULL,
  [PXEnumDescription("Today", typeof (InfoMessages), Category = "DATETIME")] TODAY,
  [PXEnumDescription("Overdue", typeof (InfoMessages), Category = "DATETIME")] OVERDUE,
  [PXEnumDescription("Today+Overdue", typeof (InfoMessages), Category = "DATETIME")] TODAY_OVERDUE,
  [PXEnumDescription("Tomorrow", typeof (InfoMessages), Category = "DATETIME")] TOMMOROW,
  [PXEnumDescription("This Week", typeof (InfoMessages), Category = "DATETIME")] THIS_WEEK,
  [PXEnumDescription("Next Week", typeof (InfoMessages), Category = "DATETIME")] NEXT_WEEK,
  [PXEnumDescription("This Month", typeof (InfoMessages), Category = "DATETIME")] THIS_MONTH,
  [PXEnumDescription("Next Month", typeof (InfoMessages), Category = "DATETIME")] NEXT_MONTH,
  [PXEnumDescription("Is In", typeof (InfoMessages), Category = "COMMON")] IN,
  [PXEnumDescription("Is Not In", typeof (InfoMessages), Category = "COMMON")] NI,
  [PXEnumDescription("External Reference", typeof (InfoMessages), Category = "HIDDEN")] ER,
  [PXEnumDescription("Nested selector filter", typeof (InfoMessages), Category = "HIDDEN")] NestedSelector,
}
