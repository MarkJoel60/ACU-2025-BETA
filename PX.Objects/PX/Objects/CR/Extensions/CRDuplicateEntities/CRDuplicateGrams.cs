// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateGrams
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRDuplicateEntities;

/// <exclude />
[PXHidden]
[Serializable]
public class CRDuplicateGrams : CRGrams
{
  public new abstract class gramID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  CRDuplicateGrams.gramID>
  {
  }

  public new abstract class validationType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRDuplicateGrams.validationType>
  {
  }

  public new abstract class entityID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRDuplicateGrams.entityID>
  {
  }

  public new abstract class entityName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRDuplicateGrams.entityName>
  {
  }

  public new abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRDuplicateGrams.fieldName>
  {
  }

  public new abstract class fieldValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRDuplicateGrams.fieldValue>
  {
  }

  public new abstract class score : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRDuplicateGrams.score>
  {
  }
}
