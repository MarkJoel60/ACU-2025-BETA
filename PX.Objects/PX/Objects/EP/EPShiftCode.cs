// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPShiftCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Stores information on different shifts an employee can work.
/// For example, when employee is working during the night (night shift) or early in the morning (graveyard shift).
/// The information will be displayed on the Shift Codes (EP103000) form.
/// </summary>
[PXCacheName("Shift Code")]
[DebuggerDisplay("{GetType().Name,nq}: {ShiftCD,nq}")]
[Serializable]
public class EPShiftCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public int? ShiftID { get; set; }

  [PXDBString(IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Code")]
  [PXDefault]
  [PXUIEnabled(typeof (Where<BqlOperand<EPShiftCode.shiftCD, IBqlString>.IsNull>))]
  public virtual 
  #nullable disable
  string ShiftCD { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  [PXDefault(true)]
  public virtual bool? IsActive { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXDefault("")]
  public virtual string Description { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsManufacturingShift { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

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

  public class PK : PrimaryKeyOf<EPShiftCode>.By<EPShiftCode.shiftID>
  {
    public static EPShiftCode Find(PXGraph graph, int? shiftID, PKFindOptions options = 0)
    {
      return (EPShiftCode) PrimaryKeyOf<EPShiftCode>.By<EPShiftCode.shiftID>.FindBy(graph, (object) shiftID, options);
    }
  }

  public class UK : PrimaryKeyOf<EPShiftCode>.By<EPShiftCode.shiftCD>
  {
    public static EPShiftCode Find(PXGraph graph, string shiftCD, PKFindOptions options = 0)
    {
      return (EPShiftCode) PrimaryKeyOf<EPShiftCode>.By<EPShiftCode.shiftCD>.FindBy(graph, (object) shiftCD, options);
    }
  }

  public abstract class shiftID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPShiftCode.shiftID>
  {
  }

  public abstract class shiftCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPShiftCode.shiftCD>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPShiftCode.isActive>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPShiftCode.description>
  {
  }

  public abstract class isManufacturingShift : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPShiftCode.isManufacturingShift>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPShiftCode.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPShiftCode.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPShiftCode.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPShiftCode.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPShiftCode.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPShiftCode.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPShiftCode.lastModifiedDateTime>
  {
  }
}
