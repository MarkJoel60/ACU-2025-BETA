// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowForm
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUWorkflowForm : AUWorkflowBaseTable, IBqlTable, IBqlTableSystemDataStorage, IScreenItem
{
  [PXUIField(DisplayName = "Screen")]
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  public 
  #nullable disable
  string Screen { get; set; }

  string IScreenItem.ScreenID => this.Screen;

  [PXUIField(DisplayName = "Dialog Box Name")]
  [PXDBString(50, IsKey = true)]
  public string FormName { get; set; }

  [PXUIField(DisplayName = "Title")]
  [PXDBString(100, IsUnicode = true)]
  public string DisplayName { get; set; }

  [PXUIField(DisplayName = "Number of Columns")]
  [PXDBInt]
  public int? Columns { get; set; }

  [PXUIField(DisplayName = "Dac Type")]
  [PXDBString(100, IsUnicode = true)]
  public string DacType { get; set; }

  public abstract class screen : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowForm.screen>
  {
  }

  public abstract class formName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowForm.formName>
  {
  }

  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowForm.displayName>
  {
  }

  public abstract class columns : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUWorkflowForm.columns>
  {
  }

  public abstract class dacType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowForm.dacType>
  {
  }
}
