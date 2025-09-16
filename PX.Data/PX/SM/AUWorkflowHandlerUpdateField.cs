// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowHandlerUpdateField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

[PXCacheName("Workflow Handler Field")]
public class AUWorkflowHandlerUpdateField : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IWorkflowUpdateField,
  IFieldName,
  IScreenItem
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUWorkflowHandler.screenID))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Action Name")]
  public virtual string HandlerName { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Field")]
  public virtual string FieldName { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXLineNbr(typeof (AUWorkflowHandler))]
  [PXParent(typeof (Select<AUWorkflowHandler, Where2<Where<AUWorkflowHandler.screenID, Equal<Current<AUWorkflowHandlerUpdateField.screenID>>>, PX.Data.And<Where<AUWorkflowHandler.handlerName, Equal<Current<AUWorkflowHandlerUpdateField.handlerName>>>>>>))]
  public virtual int? HandlerFieldLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "From Schema")]
  public bool? IsFromScheme { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "New Value")]
  public virtual string Value { get; set; }

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowHandlerUpdateField.screenID>
  {
  }

  public abstract class handlerName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowHandlerUpdateField.handlerName>
  {
  }

  public abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowHandlerUpdateField.fieldName>
  {
  }

  public abstract class handlerFieldLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUWorkflowHandlerUpdateField.handlerFieldLineNbr>
  {
  }

  public abstract class isFromScheme : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowHandlerUpdateField.isFromScheme>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowHandlerUpdateField.value>
  {
  }
}
