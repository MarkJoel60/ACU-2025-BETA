// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSPostRegister
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
public class FSPostRegister : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true, IsKey = true, InputMask = ">aa")]
  [PXDefault]
  public virtual 
  #nullable disable
  string EntityType { get; set; }

  [PXDBString(4, IsFixed = true, IsKey = true, InputMask = ">AAAA")]
  [PXDefault]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(20, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string RefNbr { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true, InputMask = ">aa")]
  [PXDefault]
  public virtual string PostedTO { get; set; }

  [PXDBString(5, IsFixed = true)]
  [PXDefault]
  public virtual string Type { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? ProcessID { get; set; }

  [PXDBInt]
  public virtual int? BatchID { get; set; }

  [PXDBString(3, IsFixed = true, InputMask = ">aaa")]
  public virtual string PostDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string PostRefNbr { get; set; }

  public abstract class entityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostRegister.entityType>
  {
    public abstract class Values : ListField_PostDoc_EntityType
    {
    }
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostRegister.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostRegister.refNbr>
  {
  }

  public abstract class postedTO : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostRegister.postedTO>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostRegister.type>
  {
  }

  public abstract class processID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSPostRegister.processID>
  {
  }

  public abstract class batchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostRegister.batchID>
  {
  }

  public abstract class postDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostRegister.postDocType>
  {
  }

  public abstract class postRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostRegister.postRefNbr>
  {
  }
}
