// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ACHPlugInParameter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.ACHPlugInBase;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <exclude />
[PXCacheName("ACHPlugInParameter")]
[Serializable]
public class ACHPlugInParameter : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IACHPlugInParameter
{
  [PXDBString(10, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXDefault(typeof (PaymentMethod.paymentMethodID))]
  [PXUIField]
  [PXParent(typeof (Select<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Current<ACHPlugInParameter.paymentMethodID>>>>))]
  public virtual 
  #nullable disable
  string PaymentMethodID { get; set; }

  [PXDBString(255 /*0xFF*/, IsKey = true)]
  [PXUIField]
  [PXDefault(typeof (PaymentMethod.aPBatchExportPlugInTypeName))]
  [PXUIVisible(typeof (Where<PaymentMethod.useForAP, Equal<True>, And<PaymentMethod.aPCreateBatchPayment, Equal<True>, And<PaymentMethod.aPBatchExportMethod, Equal<ACHExportMethod.plugIn>>>>))]
  [PXUIRequired(typeof (Where<PaymentMethod.useForAP, Equal<True>, And<PaymentMethod.aPCreateBatchPayment, Equal<True>>>))]
  public virtual string PlugInTypeName { get; set; }

  [PXDBString(30, IsKey = true)]
  [PXUIField]
  public virtual string ParameterID { get; set; }

  [PXDBString(60)]
  [PXUIField]
  public virtual string ParameterCode { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField]
  public virtual string Description { get; set; }

  [PXUIField]
  [PlugInSettingsList(4000, typeof (CRTaskMaint), typeof (TaskTemplateSetting.fieldName), ExclusiveValues = false)]
  [PXDefault]
  public virtual string Value { get; set; }

  [PXDBBool]
  public bool? Required { get; set; }

  [PXDBBool]
  public bool? ReadOnly { get; set; }

  [PXDBBool]
  public virtual bool? Visible { get; set; }

  [PXDBInt]
  public virtual int? Type { get; set; }

  [PXDBInt]
  public int? Order { get; set; }

  [PXDBInt]
  public int? UsedIn { get; set; }

  [PXInt]
  public int? DetailMapping { get; set; }

  [PXInt]
  public int? ExportScenarioMapping { get; set; }

  [PXDBBool]
  public virtual bool? IsGroupHeader { get; set; }

  [PXDBBool]
  public virtual bool? IsAvailableInShortForm { get; set; }

  [PXDBBool]
  [PXUIField]
  public virtual bool? IsFormula { get; set; }

  /// <exclude />
  [PXInt]
  public int? DataElementSize { get; set; }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInParameter.paymentMethodID>
  {
  }

  public abstract class plugInTypeName : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ACHPlugInParameter.plugInTypeName>
  {
  }

  public abstract class parameterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInParameter.parameterID>
  {
  }

  public abstract class parameterCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInParameter.parameterCode>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ACHPlugInParameter.description>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ACHPlugInParameter.value>
  {
  }

  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ACHPlugInParameter.required>
  {
  }

  public abstract class readOnly : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ACHPlugInParameter.readOnly>
  {
  }

  public abstract class visible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ACHPlugInParameter.visible>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ACHPlugInParameter.type>
  {
  }

  public abstract class order : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ACHPlugInParameter.order>
  {
  }

  public abstract class usedIn : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ACHPlugInParameter.usedIn>
  {
  }

  public abstract class detailMapping : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ACHPlugInParameter.detailMapping>
  {
  }

  public abstract class exportScenarioMapping : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ACHPlugInParameter.exportScenarioMapping>
  {
  }

  public abstract class isGroupHeader : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ACHPlugInParameter.isGroupHeader>
  {
  }

  public abstract class isAvailableInShortForm : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ACHPlugInParameter.isAvailableInShortForm>
  {
  }

  public abstract class isFormula : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ACHPlugInParameter.isFormula>
  {
  }

  public abstract class dataElementSize : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ACHPlugInParameter.dataElementSize>
  {
  }
}
