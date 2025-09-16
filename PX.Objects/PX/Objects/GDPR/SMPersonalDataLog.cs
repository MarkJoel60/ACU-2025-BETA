// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.SMPersonalDataLog
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.GDPR;

[Serializable]
public class SMPersonalDataLog : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  [PXUIField(DisplayName = "Key")]
  public virtual 
  #nullable disable
  string UIKey => this.CombinedKey;

  [PXDBIdentity(IsKey = true)]
  public virtual int? LogID { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Key")]
  public virtual string CombinedKey { get; set; }

  [PXDBString]
  [SMPersonalDataLog.tableName.List]
  [PXUIField(DisplayName = "Entity")]
  public virtual string TableName { get; set; }

  [PXPseudonymizationStatusField]
  [PXUIField(DisplayName = "Set Status", Visible = true)]
  public virtual int? PseudonymizationStatus { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "By User")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  public abstract class uIKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPersonalDataLog.uIKey>
  {
  }

  public abstract class logID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPersonalDataLog.logID>
  {
  }

  public abstract class combinedKey : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPersonalDataLog.combinedKey>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPersonalDataLog.tableName>
  {
    public class List : PXStringListAttribute
    {
      public List()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair(typeof (Contact).FullName, "Contact"),
          PXStringListAttribute.Pair(typeof (CRContact).FullName, "Opportunity Contact")
        })
      {
      }
    }
  }

  public abstract class pseudonymizationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMPersonalDataLog.pseudonymizationStatus>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMPersonalDataLog.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SMPersonalDataLog.createdDateTime>
  {
  }
}
