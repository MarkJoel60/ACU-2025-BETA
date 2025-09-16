// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.NewPaymentMethodDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("NewPaymentMethodDetail")]
[Serializable]
public class NewPaymentMethodDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Used In")]
  public virtual 
  #nullable disable
  string UseFor { get; set; }

  [PXInt]
  public virtual int? DetailIDInt { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "ID", Visible = true)]
  public virtual string DetailID { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Entry Mask")]
  public virtual string EntryMask { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Validation Reg. Exp.")]
  public virtual string ValidRegexp { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXDefault]
  [PXUIField(DisplayName = "Display Mask", Enabled = false)]
  public virtual string DisplayMask { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Required")]
  public virtual bool? IsRequired { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Control Type")]
  [PXIntList(new int[] {0, 1}, new string[] {"Text", "Account Type List"})]
  public virtual int? ControlType { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXStringList(new string[] {"22", "32"}, new string[] {"Checking Account", "Savings Account"})]
  [PXDefault]
  [PXUIField(DisplayName = "Default Value")]
  public virtual string DefaultValue { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NewPaymentMethodDetail.selected>
  {
  }

  public abstract class useFor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NewPaymentMethodDetail.useFor>
  {
  }

  public abstract class detailIDInt : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NewPaymentMethodDetail.detailIDInt>
  {
  }

  public abstract class detailID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NewPaymentMethodDetail.detailID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NewPaymentMethodDetail.description>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NewPaymentMethodDetail.status>
  {
  }

  public abstract class entryMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NewPaymentMethodDetail.entryMask>
  {
  }

  public abstract class validRegexp : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NewPaymentMethodDetail.validRegexp>
  {
  }

  public abstract class displayMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NewPaymentMethodDetail.displayMask>
  {
  }

  public abstract class isRequired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NewPaymentMethodDetail.isRequired>
  {
  }

  public abstract class controlType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NewPaymentMethodDetail.controlType>
  {
  }

  public abstract class defaultValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NewPaymentMethodDetail.defaultValue>
  {
  }
}
