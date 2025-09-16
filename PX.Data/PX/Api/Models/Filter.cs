// Decompiled with JetBrains decompiler
// Type: PX.Api.Models.Filter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Xml.Serialization;

#nullable disable
namespace PX.Api.Models;

public class Filter
{
  public Field Field;
  public FilterCondition Condition;
  public object Value;
  public object Value2;
  public int OpenBrackets;
  public int CloseBrackets;
  public FilterOperator Operator;
  [XmlIgnore]
  public Field ParentField;
}
