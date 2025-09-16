// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRRelation2
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
[Serializable]
public class CRRelation2 : CRRelation
{
  public new abstract class refNoteID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  CRRelation2.refNoteID>
  {
  }

  public new abstract class entityID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRRelation2.entityID>
  {
  }

  public new abstract class role : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRRelation2.role>
  {
  }
}
