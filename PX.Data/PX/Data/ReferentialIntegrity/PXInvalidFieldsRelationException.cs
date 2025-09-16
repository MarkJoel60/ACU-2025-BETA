// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.PXInvalidFieldsRelationException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data.ReferentialIntegrity;

[Serializable]
internal class PXInvalidFieldsRelationException : PXException
{
  public IReadOnlyList<IFieldsRelation> Relations { get; }

  public ReferenceOrigin ReferenceOrigin { get; }

  public IEnumerable<System.Type> ChildTables
  {
    get
    {
      return this.Relations.Select<IFieldsRelation, System.Type>((Func<IFieldsRelation, System.Type>) (r => BqlCommand.GetItemType(r.FieldOfChildTable))).Distinct<System.Type>();
    }
  }

  public IEnumerable<System.Type> ParentTables
  {
    get
    {
      return this.Relations.Select<IFieldsRelation, System.Type>((Func<IFieldsRelation, System.Type>) (r => BqlCommand.GetItemType(r.FieldOfParentTable))).Distinct<System.Type>();
    }
  }

  public PXInvalidFieldsRelationException(
    string format,
    ReferenceOrigin referenceOrigin,
    IEnumerable<IFieldsRelation> relations,
    params object[] args)
    : base(format, args)
  {
    this.ReferenceOrigin = referenceOrigin;
    this.Relations = (IReadOnlyList<IFieldsRelation>) ((object) (relations as IReadOnlyList<IFieldsRelation>) ?? (object) relations.ToArray<IFieldsRelation>());
  }

  public PXInvalidFieldsRelationException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXInvalidFieldsRelationException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXInvalidFieldsRelationException>(this, info);
    base.GetObjectData(info, context);
  }
}
