// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPRuleBaseCondition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Diagnostics;

#nullable disable
namespace PX.Objects.EP;

[DebuggerDisplay("{OpenBrackets} {Entity}.{FieldName} {((PX.Data.PXCondition)Condition).ToString()} {Value}|{Value2} {CloseBrackets} {Operator == 0 ? \"AND\" : \"OR\"}")]
public class EPRuleBaseCondition : PXBqlTable
{
  public const int FieldLength = 128 /*0x80*/;

  public virtual Guid? RuleID { get; set; }

  public virtual int? OpenBrackets { get; set; }

  public virtual string Entity { get; set; }

  public virtual string FieldName { get; set; }

  public virtual int? Condition { get; set; }

  public virtual bool? IsRelative { get; set; }

  public virtual bool? IsField { get; set; }

  public virtual string Value { get; set; }

  public virtual string Value2 { get; set; }

  public virtual int? CloseBrackets { get; set; }

  public virtual int? Operator { get; set; }

  public virtual Guid? UiNoteID { get; set; }
}
