// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedFieldMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>Contains the rules of mapping for Bank Feeds</summary>
[PXCacheName("CABankFeedFieldMapping")]
public class CABankFeedFieldMapping : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Specifies the parent Bank Feed ID</summary>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CABankFeed.bankFeedID))]
  [PXParent(typeof (CABankFeedFieldMapping.FK.BankFeed))]
  public virtual 
  #nullable disable
  string BankFeedID { get; set; }

  /// <summary>Specifies line number</summary>
  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (CABankFeed))]
  [PXUIField(Visible = false)]
  public virtual int? LineNbr { get; set; }

  /// <summary>Specifies the rule is active</summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  /// <summary>Specifies the target field in table CABankTran</summary>
  [PXDBString]
  [PXDefault]
  [CABankFeedMappingTarget.List]
  [PXUIField(DisplayName = "Target Field")]
  public virtual string TargetField { get; set; }

  /// <summary>
  /// Specifies the source field from table BankFeedTransaction or formula
  /// </summary>
  [PXFormulaEditor(DisplayName = "Source Field or Value", IsDBField = true)]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXDefault]
  public virtual string SourceFieldOrValue { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created Date Time")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified Date Time")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXNote]
  [PXUIField(DisplayName = "Noteid")]
  public virtual Guid? Noteid { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "Tstamp")]
  public virtual byte[] Tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<CABankFeedFieldMapping>.By<CABankFeedFieldMapping.bankFeedID, CABankFeedFieldMapping.lineNbr>
  {
    public static CABankFeedFieldMapping Find(
      PXGraph graph,
      string bankFeedID,
      int? bankFeedFieldMappingId)
    {
      return (CABankFeedFieldMapping) PrimaryKeyOf<CABankFeedFieldMapping>.By<CABankFeedFieldMapping.bankFeedID, CABankFeedFieldMapping.lineNbr>.FindBy(graph, (object) bankFeedID, (object) bankFeedFieldMappingId, (PKFindOptions) 0);
    }
  }

  public static class FK
  {
    public class BankFeed : 
      PrimaryKeyOf<CABankFeed>.By<CABankFeed.bankFeedID>.ForeignKeyOf<CABankFeedFieldMapping>.By<CABankFeedFieldMapping.bankFeedID>
    {
    }
  }

  public abstract class bankFeedID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedFieldMapping.bankFeedID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeedFieldMapping.lineNbr>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankFeedFieldMapping.active>
  {
  }

  public abstract class targetField : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedFieldMapping.targetField>
  {
  }

  public abstract class sourceFieldOrValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedFieldMapping.sourceFieldOrValue>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CABankFeedFieldMapping.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedFieldMapping.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedFieldMapping.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CABankFeedFieldMapping.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedFieldMapping.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedFieldMapping.lastModifiedDateTime>
  {
  }

  public abstract class noteid : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankFeedFieldMapping.noteid>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CABankFeedFieldMapping.tstamp>
  {
  }
}
