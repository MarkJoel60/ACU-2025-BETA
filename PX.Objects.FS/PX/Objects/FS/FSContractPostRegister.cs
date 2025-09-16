// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSContractPostRegister
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSContractPostRegister : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Contract Period ID")]
  public virtual int? ContractPeriodID { get; set; }

  [PXDBInt]
  public virtual int? ContractPostBatchID { get; set; }

  [PXDBString(3, IsFixed = true, InputMask = ">aaa")]
  public virtual 
  #nullable disable
  string PostDocType { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  public virtual string PostedTO { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string PostRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Service Contract ID")]
  public virtual int? ServiceContractID { get; set; }

  public abstract class contractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPostRegister.contractPeriodID>
  {
  }

  public abstract class contractPostBatchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPostRegister.contractPostBatchID>
  {
  }

  public abstract class postDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPostRegister.postDocType>
  {
  }

  public abstract class postedTO : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSContractPostRegister.postedTO>
  {
  }

  public abstract class postRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPostRegister.postRefNbr>
  {
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPostRegister.serviceContractID>
  {
  }
}
