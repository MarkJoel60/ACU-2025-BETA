// Decompiled with JetBrains decompiler
// Type: PX.Objects.PR.Standalone.PRSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.PR.Standalone;

[PXCacheName("Payroll Preferences")]
[Serializable]
public class PRSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _BatchNumberingID;

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Batch Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string BatchNumberingID
  {
    get => this._BatchNumberingID;
    set => this._BatchNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Payroll Batch Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string BatchNumberingCD { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Hide Employee Name on Transactions")]
  [PXDefault(false)]
  public virtual bool? HideEmployeeInfo { get; set; }

  [PXDBString(3, IsFixed = true)]
  public virtual string ProjectCostAssignment { get; set; }

  [PXDBString(1, IsUnicode = false, IsFixed = true)]
  public virtual string TimePostingOption { get; set; }

  [PXDBInt]
  public virtual int? OffBalanceAccountGroupID { get; set; }

  public abstract class batchNumberingID : IBqlField, IBqlOperand
  {
  }

  public abstract class batchNumberingCD : IBqlField, IBqlOperand
  {
  }

  public abstract class hideEmployeeInfo : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PRSetup.hideEmployeeInfo>
  {
  }

  public abstract class projectCostAssignment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PRSetup.projectCostAssignment>
  {
  }

  public abstract class timePostingOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PRSetup.timePostingOption>
  {
  }

  public abstract class offBalanceAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PRSetup.offBalanceAccountGroupID>
  {
  }
}
