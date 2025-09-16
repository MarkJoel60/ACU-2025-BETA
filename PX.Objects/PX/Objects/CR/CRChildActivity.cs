// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRChildActivity
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXHidden]
[PXBreakInheritance]
[Serializable]
public class CRChildActivity : CRPMTimeActivity
{
  [PXUIField(DisplayName = "Parent")]
  [PXDBGuid(false)]
  [PXSelector(typeof (Search<CRPMTimeActivity.noteID>), DirtyRead = true)]
  [PXRestrictor(typeof (Where<CRPMTimeActivity.classID, Equal<CRActivityClass.task>, Or<CRPMTimeActivity.classID, Equal<CRActivityClass.events>>>), null, new System.Type[] {})]
  [PXDBDefault(typeof (CRPMTimeActivity.noteID))]
  public override Guid? ParentNoteID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [ActivityStatus]
  [PXUIField(DisplayName = "Status")]
  [PXDefault("OP")]
  public override 
  #nullable disable
  string UIStatus { get; set; }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRChildActivity.noteID>
  {
  }

  public new abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRChildActivity.classID>
  {
  }

  public new abstract class uistatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRChildActivity.uistatus>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRChildActivity.createdDateTime>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRChildActivity.ownerID>
  {
  }

  public new abstract class isCorrected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRChildActivity.isCorrected>
  {
  }

  public new abstract class timeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRChildActivity.timeSpent>
  {
  }

  public new abstract class overtimeSpent : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRChildActivity.overtimeSpent>
  {
  }

  public new abstract class timeBillable : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRChildActivity.timeBillable>
  {
  }

  public new abstract class overtimeBillable : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRChildActivity.overtimeBillable>
  {
  }

  public new abstract class isPrivate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRChildActivity.isPrivate>
  {
  }

  public new abstract class parentNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRChildActivity.parentNoteID>
  {
  }
}
