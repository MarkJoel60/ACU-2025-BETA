// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRAttributesFieldAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class CRAttributesFieldAttribute : PXDBAttributeAttribute
{
  protected readonly string EntityType;

  public System.Type ClassIdField { get; private set; }

  public System.Type[] RelatedEntityTypes { get; private set; }

  public CRAttributesFieldAttribute(System.Type classIdField)
    : this(classIdField, (System.Type) null, (System.Type[]) null)
  {
  }

  public CRAttributesFieldAttribute(System.Type classIdField, System.Type noteIdField)
    : this(classIdField, noteIdField, (System.Type[]) null)
  {
  }

  public CRAttributesFieldAttribute(System.Type classIdField, System.Type[] relatedEntityTypes)
    : this(classIdField, (System.Type) null, relatedEntityTypes)
  {
  }

  public CRAttributesFieldAttribute(System.Type classIdField, System.Type noteIdField, System.Type[] relatedEntityTypes)
    : base(CRAttributesFieldAttribute.GetValueSearchCommand(classIdField, noteIdField), typeof (CSAnswers.attributeID), CRAttributesFieldAttribute.GetAttributesSearchCommand(classIdField))
  {
    this.ClassIdField = classIdField;
    if (classIdField != (System.Type) null && classIdField.DeclaringType != (System.Type) null)
      this.EntityType = classIdField.DeclaringType.FullName;
    this.RelatedEntityTypes = relatedEntityTypes;
  }

  protected CRAttributesFieldAttribute(
    System.Type classIdField,
    System.Type attributeID,
    System.Type valueSearch,
    System.Type attributeSearch)
  {
    System.Type type1 = valueSearch;
    System.Type type2 = attributeID;
    System.Type type3 = attributeSearch;
    if ((object) type3 == null)
      type3 = CRAttributesFieldAttribute.GetAttributesSearchCommand(classIdField);
    // ISSUE: explicit constructor call
    base.\u002Ector(type1, type2, type3);
    this.ClassIdField = classIdField;
    if (!(classIdField != (System.Type) null) || !(classIdField.DeclaringType != (System.Type) null))
      return;
    this.EntityType = classIdField.DeclaringType.FullName;
  }

  protected static System.Type GetAttributesSearchCommand(System.Type classIdField)
  {
    return BqlCommand.Compose(new System.Type[18]
    {
      typeof (Search2<,,>),
      typeof (CSAttribute.attributeID),
      typeof (InnerJoin<,>),
      typeof (CSAttributeGroup),
      typeof (On<,>),
      typeof (CSAttributeGroup.attributeID),
      typeof (Equal<>),
      typeof (CSAttribute.attributeID),
      typeof (Where<,,>),
      typeof (CSAttributeGroup.entityType),
      typeof (Equal<>),
      typeof (Required<>),
      typeof (CSAttributeGroup.entityType),
      typeof (And<,>),
      typeof (CSAttributeGroup.entityClassID),
      typeof (Equal<>),
      typeof (Current<>),
      classIdField
    });
  }

  protected static System.Type GetValueSearchCommand(System.Type classIdField, System.Type noteIdField)
  {
    System.Type declaringType = classIdField.DeclaringType;
    System.Type type = noteIdField;
    if ((object) type == null)
      type = EntityHelper.GetNoteType(declaringType);
    noteIdField = type;
    return BqlCommand.Compose(new System.Type[6]
    {
      typeof (Search<,>),
      typeof (CSAnswers.value),
      typeof (Where<,>),
      typeof (CSAnswers.refNoteID),
      typeof (Equal<>),
      noteIdField
    });
  }

  protected virtual PXFieldState[] GetSlot(
    string name,
    PXDBAttributeAttribute.DefinitionParams definitionParams,
    System.Type[] tables)
  {
    if (this.EntityType == null)
      return base.GetSlot(name, definitionParams, tables);
    return PXDatabase.GetSlot<CRAttributesFieldAttribute.CRDefinition, CRAttributesFieldAttribute.CRDefinition.Parameters>(name, new CRAttributesFieldAttribute.CRDefinition.Parameters(definitionParams.FieldName, this.EntityType), tables)?.Fields;
  }

  protected class CRDefinition : 
    IPrefetchable<CRAttributesFieldAttribute.CRDefinition.Parameters>,
    IPXCompanyDependent
  {
    public PXFieldState[] Fields;

    public void Prefetch(
      CRAttributesFieldAttribute.CRDefinition.Parameters parameters)
    {
      List<PXFieldState> pxFieldStateList = new List<PXFieldState>();
      CRAttribute.AttributeList attributeList = CRAttribute.EntityAttributes(parameters.EntityType);
      foreach (string str in (IEnumerable<string>) attributeList.Keys.OrderBy<string, string>((Func<string, string>) (s => s)))
      {
        CRAttribute.Attribute attribute = ((KList<string, CRAttribute.Attribute>) attributeList)[str];
        PXFieldState fieldState = PXDBAttributeAttribute.Definition.CreateFieldState($"{attribute.ID}_{parameters.FieldName}", attribute.Description, attribute.ControlType.GetValueOrDefault(), attribute.EntryMask, attribute.Precision, attribute.List);
        if (fieldState != null)
          pxFieldStateList.Add(fieldState);
      }
      this.Fields = pxFieldStateList.ToArray();
    }

    public class Parameters
    {
      public readonly string FieldName;
      public readonly string EntityType;

      public Parameters(string fieldName, string entityType)
      {
        this.FieldName = fieldName;
        this.EntityType = entityType;
      }
    }
  }
}
