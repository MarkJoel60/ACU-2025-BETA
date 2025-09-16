// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSAnswers
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.CS;

[DebuggerDisplay("AttributeID={AttributeID} IsRequired={IsRequired}")]
[PXCacheName("Answers")]
[PXPossibleRowsList(typeof (CSAttribute.description), typeof (CSAnswers.attributeID), typeof (CSAnswers.value))]
[PXPrimaryGraph(new Type[] {typeof (CSAttributeMaint)}, new Type[] {typeof (Select<CSAttribute, Where<CSAttribute.attributeID, Equal<Current<CSAnswers.attributeID>>>>)})]
[Serializable]
public class CSAnswers : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _AttributeID;
  protected string _Value;
  protected bool? _IsRequired;

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Attribute")]
  [CSAttributeSelector]
  public virtual string AttributeID
  {
    get => this._AttributeID;
    set => this._AttributeID = value;
  }

  [PXAttributeValue]
  [PXUIField(DisplayName = "Value")]
  [CSAttributeValueValidation(typeof (CSAnswers.attributeID))]
  [PXPersonalDataFieldAttribute.Value]
  public virtual string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Required", IsReadOnly = true)]
  public virtual bool? IsRequired
  {
    get => this._IsRequired;
    set => this._IsRequired = value;
  }

  [PXString(1, IsUnicode = false, IsFixed = true)]
  [PXUIField(DisplayName = "Category", IsReadOnly = true, FieldClass = "MatrixItem")]
  [CSAttributeGroup.attributeCategory.List]
  public virtual string AttributeCategory { get; set; }

  [PXShort]
  public short? Order { get; set; }

  [PXBool]
  public bool? NotInClass { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Is Active", FieldClass = "MatrixItem", Enabled = false)]
  public bool? IsActive { get; set; }

  public class PK : PrimaryKeyOf<CSAnswers>.By<CSAnswers.refNoteID, CSAnswers.attributeID>
  {
    public static CSAnswers Find(
      PXGraph graph,
      Guid? refNoteID,
      string attributeID,
      PKFindOptions options = 0)
    {
      return (CSAnswers) PrimaryKeyOf<CSAnswers>.By<CSAnswers.refNoteID, CSAnswers.attributeID>.FindBy(graph, (object) refNoteID, (object) attributeID, options);
    }

    public static CSAnswers FindDirty(PXGraph graph, Guid? refNoteID, string attributeID)
    {
      return PXResultset<CSAnswers>.op_Implicit(PXSelectBase<CSAnswers, PXViewOf<CSAnswers>.BasedOn<SelectFromBase<CSAnswers, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAnswers.refNoteID, Equal<P.AsGuid>>>>>.And<BqlOperand<CSAnswers.attributeID, IBqlString>.IsEqual<P.AsString>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
      {
        (object) refNoteID,
        (object) attributeID
      }));
    }
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CSAnswers.refNoteID>
  {
  }

  public abstract class attributeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAnswers.attributeID>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAnswers.value>
  {
  }

  public abstract class isRequired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSAnswers.isRequired>
  {
  }

  public abstract class attributeCategory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAnswers.attributeCategory>
  {
  }

  public abstract class order : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CSAnswers.order>
  {
  }

  public abstract class notInClass : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSAnswers.notInClass>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSAnswers.isActive>
  {
  }
}
