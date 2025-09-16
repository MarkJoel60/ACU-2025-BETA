// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.DAC.SortExp
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.Reports.DAC;

[PXHidden]
public class SortExp : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt(IsKey = true)]
  [PXDefault(-1)]
  [PXUIField(Visible = false)]
  public int? LineNbr { get; set; }

  [PXString]
  [PXStringList(IsLocalizable = false)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Property")]
  public 
  #nullable disable
  string FieldName { get; set; }

  [PXInt]
  [PXIntList(typeof (PX.Reports.SortOrder), false)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Condition")]
  public int? SortOrder { get; set; }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SortExp.lineNbr>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SortExp.fieldName>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SortExp.sortOrder>
  {
  }
}
