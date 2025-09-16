// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.CCProcessingCenterTerminal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using System;

#nullable enable
namespace PX.Objects.CC;

/// <summary>
/// Represents a POS Terminal set up for the proceesing center
/// </summary>
[PXCacheName("Processing Center Terminal")]
[Serializable]
public class CCProcessingCenterTerminal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Processing Center ID</summary>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CCProcessingCenter.processingCenterID))]
  [PXParent(typeof (Select<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Current<CCProcessingCenterTerminal.processingCenterID>>>>))]
  [PXUIField]
  public virtual string? ProcessingCenterID { get; set; }

  /// <summary>Terminal ID</summary>
  [PXDBString(36, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual string? TerminalID { get; set; }

  /// <summary>Terminal Name</summary>
  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string? TerminalName { get; set; }

  /// <summary>Display Name</summary>
  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXCheckDistinct(new Type[] {}, ClearOnDuplicate = false, Where = typeof (Where<CCProcessingCenterTerminal.processingCenterID, Equal<Current<CCProcessingCenterTerminal.processingCenterID>>>))]
  public virtual string? DisplayName { get; set; }

  /// <summary>Indicates that the terminal is active</summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  /// <summary>Indicates that the terminal can be set active</summary>
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? CanBeEnabled { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<CCProcessingCenterTerminal>.By<CCProcessingCenterTerminal.terminalID, CCProcessingCenterTerminal.processingCenterID>
  {
    public static CCProcessingCenterTerminal Find(
      PXGraph graph,
      string terminalID,
      string processingCenterID)
    {
      return (CCProcessingCenterTerminal) PrimaryKeyOf<CCProcessingCenterTerminal>.By<CCProcessingCenterTerminal.terminalID, CCProcessingCenterTerminal.processingCenterID>.FindBy(graph, (object) terminalID, (object) processingCenterID, (PKFindOptions) 0);
    }
  }

  public abstract class processingCenterID : 
    BqlType<IBqlString, string>.Field<CCProcessingCenterTerminal.processingCenterID>
  {
  }

  public abstract class terminalID : 
    BqlType<IBqlString, string>.Field<CCProcessingCenterTerminal.terminalID>
  {
  }

  public abstract class terminalName : 
    BqlType<IBqlString, string>.Field<CCProcessingCenterTerminal.terminalName>
  {
  }

  public abstract class displayName : 
    BqlType<IBqlString, string>.Field<CCProcessingCenterTerminal.displayName>
  {
  }

  public abstract class isActive : BqlType<IBqlBool, bool>.Field<CCProcessingCenterTerminal.isActive>
  {
  }

  public abstract class canBeEnabled : 
    BqlType<IBqlBool, bool>.Field<CCProcessingCenterTerminal.canBeEnabled>
  {
  }

  public abstract class createdByID : 
    BqlType<IBqlGuid, Guid>.Field<CCProcessingCenterTerminal.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<CCProcessingCenterTerminal.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<IBqlDateTime, DateTime>.Field<CCProcessingCenterTerminal.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<IBqlGuid, Guid>.Field<CCProcessingCenterTerminal.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<IBqlString, string>.Field<CCProcessingCenterTerminal.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<IBqlDateTime, DateTime>.Field<CCProcessingCenterTerminal.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<IBqlByteArray, byte[]>.Field<CCProcessingCenterTerminal.Tstamp>
  {
  }
}
