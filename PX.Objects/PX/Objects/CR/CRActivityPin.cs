// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRActivityPin
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXHidden]
[Serializable]
public class CRActivityPin : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXParent(typeof (SelectFromBase<CRActivity, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRActivity.noteID, IBqlGuid>.IsEqual<BqlField<CRActivityPin.noteID, IBqlGuid>.FromCurrent>>))]
  [PXParent(typeof (SelectFromBase<CRSMEmail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRSMEmail.noteID, IBqlGuid>.IsEqual<BqlField<CRActivityPin.noteID, IBqlGuid>.FromCurrent>>))]
  [PXParent(typeof (SelectFromBase<CRPMTimeActivity, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRPMTimeActivity.noteID, IBqlGuid>.IsEqual<BqlField<CRActivityPin.noteID, IBqlGuid>.FromCurrent>>))]
  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (CRActivity.noteID))]
  [PXUIField(Visible = false, DisplayName = "NoteID of Pinned Activity")]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByScreenID(IsKey = true)]
  public virtual 
  #nullable disable
  string CreatedByScreenID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] Tstamp { get; set; }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivityPin.noteID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRActivityPin.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivityPin.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRActivityPin.createdDateTime>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRActivityPin.tstamp>
  {
  }
}
