// Decompiled with JetBrains decompiler
// Type: PX.SM.AUTemplateData
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[Serializable]
public class AUTemplateData : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "TemplateId", Visible = false)]
  [PXDBChildIdentity(typeof (AUTemplate.templateID))]
  [PXParent(typeof (Select<AUTemplate, Where<AUTemplate.templateID, Equal<Current<AUTemplateData.templateId>>>>))]
  public int? TemplateId { get; set; }

  [PXDBInt(IsKey = true)]
  public int? OrderId { get; set; }

  [PXDBBool(IsKey = false)]
  [PXUIField(DisplayName = "Active")]
  public bool? Active { get; set; }

  [PXDBInt(IsKey = false)]
  [PXUIField(DisplayName = "Line")]
  public int? Line { get; set; }

  [PXDBString(IsKey = false, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Container")]
  public 
  #nullable disable
  string Container { get; set; }

  [PXString(IsKey = false, InputMask = "")]
  [PXUIField(DisplayName = "View")]
  public string View
  {
    [PXDependsOnFields(new System.Type[] {typeof (AUTemplateData.fieldId)})] get
    {
      return !Str.IsNullOrEmpty(this.FieldId) ? StringExtensions.FirstSegment(this.FieldId, '/') : "";
    }
  }

  [PXString(IsKey = false, InputMask = "")]
  [PXUIField(DisplayName = "Type")]
  public string RowType => (string) null;

  [PXDBString(IsKey = false, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Field")]
  public string Field { get; set; }

  [PXDBString(IsKey = false, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public string Value { get; set; }

  [PXDBString(IsKey = false, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "FieldId", Visibility = PXUIVisibility.Invisible)]
  public string FieldId { get; set; }

  /// <exclude />
  public abstract class templateId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUTemplateData.templateId>
  {
  }

  /// <exclude />
  public abstract class orderId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUTemplateData.orderId>
  {
  }

  /// <exclude />
  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUTemplateData.active>
  {
  }

  /// <exclude />
  public abstract class line : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUTemplateData.line>
  {
  }

  /// <exclude />
  public abstract class container : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTemplateData.container>
  {
  }

  /// <exclude />
  public abstract class view : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTemplateData.view>
  {
  }

  /// <exclude />
  public abstract class rowType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTemplateData.rowType>
  {
  }

  /// <exclude />
  public abstract class field : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTemplateData.field>
  {
  }

  /// <exclude />
  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTemplateData.value>
  {
  }

  /// <exclude />
  public abstract class fieldId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTemplateData.fieldId>
  {
  }
}
