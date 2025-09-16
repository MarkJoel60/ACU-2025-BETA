// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.SMPersonalDataIndex
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GDPR;

[PXHidden]
[Serializable]
public class SMPersonalDataIndex : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Selected { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Key")]
  public virtual 
  #nullable disable
  string UIKey => this.CombinedKey;

  [PXDBString(IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Key")]
  public virtual string CombinedKey { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? IndexID { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Entity")]
  public virtual string Content { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SMPersonalDataIndex.selected>
  {
  }

  public abstract class uIKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPersonalDataIndex.uIKey>
  {
  }

  public abstract class combinedKey : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPersonalDataIndex.combinedKey>
  {
  }

  public abstract class indexID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMPersonalDataIndex.indexID>
  {
  }

  public abstract class content : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPersonalDataIndex.content>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SMPersonalDataIndex.createdDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SMPersonalDataIndex.Tstamp>
  {
  }
}
